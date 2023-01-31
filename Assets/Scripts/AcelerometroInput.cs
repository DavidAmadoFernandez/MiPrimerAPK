using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AcelerometroInput : MonoBehaviour
{

	public float velocidadrotacion = 2;
	public float MoveSpeed = 10f;
	private float maxSpeed = 30f;
	public Rigidbody rb;
	public Text acelerometroText;
	void Start()
	{
		rb = GetComponent<Rigidbody>();

	}
	void FixedUpdate()
	{



		transform.Rotate(-Input.acceleration.z * velocidadrotacion, 0, -Input.acceleration.x * velocidadrotacion);

		rb.AddForce(transform.forward * MoveSpeed);


		if (rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
		//rb.AddTorque(-Input.acceleration.z*velocidadrotacion, 0, -Input.acceleration.x*velocidadrotacion);
		acelerometroText.text = ("Giroscopio = "+ Input.acceleration);

	}
}