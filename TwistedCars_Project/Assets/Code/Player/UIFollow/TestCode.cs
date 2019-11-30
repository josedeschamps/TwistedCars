using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour {

	//joystick properties
	[SerializeField]
	private GameObject joystickCircle, joystickDot;
	private Touch oneTouch;
	private Vector2 touchPosition;
	private Vector2 movePosition;
	private bool hasTouch;

	//player properties
	private Rigidbody RB;
	private string movementAxisName;
	private string turnAxisName;
	private string boostInputName;
	private string shootInputName;

	public int m_PlayerNumber = 1;
	public float movementSpeed = 12f;
	public float shootRate = 5.0f;
	public float boostRate = 0.5f;
	public float speedBoostPower = 100;
	public float turnSpeed = 180f;

	private float movementInputValue;
	private float turnInputValue;


	private void Start()
	{
		SetUp();
	}

	private void Update()
	{
		JoyStickSystem();
		movementInputValue = Input.GetAxis(movementAxisName);
		turnInputValue = Input.GetAxis(turnAxisName);
	}

	void FixedUpdate()
	{

		Move();
		Turn();
	}

	private void Move()
	{


		Vector3 movement = transform.forward * movementInputValue * movementSpeed * Time.deltaTime;
		RB.MovePosition(RB.position + movement);

	}


	private void Turn()
	{

		float turn = turnInputValue * turnSpeed * Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		RB.MoveRotation(RB.rotation * turnRotation);
	}

	private void SetUp()
	{
		RB = GetComponent<Rigidbody>();
		shootInputName = "Shoot" + m_PlayerNumber;
		boostInputName = "Boost" + m_PlayerNumber;
		movementAxisName = "Vertical";
		turnAxisName = "Horizontal";
		joystickCircle.SetActive(false);
		joystickDot.SetActive(false);
	}

	private void JoyStickSystem()
	{
		if (Input.touchCount > 0)
		{
			oneTouch = Input.GetTouch(0);
			Ray ray = Camera.main.ScreenPointToRay(oneTouch.position);
			RaycastHit hit = new RaycastHit();
			hasTouch = Physics.Raycast(ray, out hit);

			switch (oneTouch.phase)
			{
				case TouchPhase.Began:
					joystickCircle.SetActive(true);
					joystickDot.SetActive(true);
					joystickCircle.transform.localPosition = touchPosition;
					joystickDot.transform.localPosition = touchPosition;
					break;

				case TouchPhase.Stationary:
					PlayerMovement();
					Move();
					Turn();
					break;

				case TouchPhase.Moved:
					PlayerMovement();
					Move();
					Turn();
					break;

				case TouchPhase.Ended:
					joystickCircle.SetActive(false);
					joystickDot.SetActive(false);
					RB.velocity = Vector3.zero;
					hasTouch = false;
					break;

			}
		}
	}


	private void PlayerMovement()
	{
		joystickDot.transform.position = touchPosition;
		joystickDot.transform.position = new Vector2(Mathf.Clamp(joystickDot.transform.position.x,
			joystickCircle.transform.position.x - 30f, joystickCircle.transform.position.x + 30f),
			Mathf.Clamp(joystickDot.transform.position.y, joystickCircle.transform.position.y - 30f,
			joystickCircle.transform.position.y + 30f));
		movePosition = (joystickDot.transform.position - joystickCircle.transform.position).normalized;

		//rb.velocity = movePosition * speed;
	}
}
