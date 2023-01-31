using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

	public float speed = 6.0F;
	public float Airspeed = 4.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	//Usaremos un vector3 llamado moveDirection para hacer los calculos del movimiento y aplicarlo mas tarde a charactercontroller.move;
	private Vector3 moveDirection = Vector3.zero;
	//NEcesito declarar mi Character controller para acceder a él y poder usarlo;
	public CharacterController controller;
	public Transform Camara;
	public bool IsGrounded;
	public Animator miAnim;
	public LayerMask layerWall;
	void Start()
	{
		//BUSCA EL COMPONENTE CHARACTERCONTROLLER EN MI GAMEOBJECT PARA PODER USAR ESTE SCRIPT
		controller = GetComponent<CharacterController>();
	}
	void Update()
	{
		Locomotion();
		AnimationValues();
		grounded();

	}
	void Locomotion()
    {
		//CALCULOS PARA EL CONTROL DE CAMERA
		//guardamos el vector 3 de la camara hacia delante y hacia un lado (hacia arriba no, vamos a omitir la y)
		Vector3 forwardCam = Camara.transform.forward;
		Vector3 rigthCam = Camara.transform.right;
		//borramos las y
		forwardCam.y = 0;
		rigthCam.y = 0;
		//y normalizamos(quiere decir que los valores tienen de máximo 1 o 0)
		forwardCam = forwardCam.normalized;
		rigthCam = rigthCam.normalized;

		//creamos vectores 3 con los inputs aplicados a las direcciones
		Vector3 forwarRelativeVertical = SimpleInput.GetAxis("Vertical") * forwardCam;
		Vector3 RightRelativeHorizontal = SimpleInput.GetAxis("Horizontal") * rigthCam;
		Vector3 cameraRelativeMovement = forwarRelativeVertical + RightRelativeHorizontal;//USAREMOS ESTE VECTOR CUANDO NECESITEMOS SABER LA DIRECCION
		//FIN DE CALCULO PARA ELCONTROL DE CAMARA;


		if (controller.isGrounded == true)
		{
			//En esta linea le damos los valores de horizontal y vertical al moveDirection(que es un Vector3);
			moveDirection = new Vector3(cameraRelativeMovement.x, 0, cameraRelativeMovement.z);

			

			//MULTIPLICAMOS POR LA VELOCIDAD
			moveDirection *= speed;


			//Condicion de salto, si pulso jump
			if (SimpleInput.GetButton("Jump"))
			{
				//si pulso jump ejerzo fuerza en Y;
				moveDirection.y = jumpSpeed;
			}

		}

		//AIR CONTROL
		if (controller.isGrounded == false)
		{
			moveDirection.x = (cameraRelativeMovement.x * Airspeed);
			moveDirection.z = (cameraRelativeMovement.z * Airspeed);
			;
		}



		//ROTACION del Cuerpo
		if (cameraRelativeMovement.magnitude > 0.1f)
		{
			transform.forward = cameraRelativeMovement.normalized;
		}


		//Aplicar la gravedad,simplement restando en Y
		moveDirection.y -= gravity * Time.deltaTime;


		//Finalmente aplcamos todos los calculos al movimiento SIEMPRE LO APLICAMOS AL FINAL
		controller.Move(moveDirection * Time.deltaTime);
	}

	void AnimationValues()
    {

		miAnim.SetFloat("HV_magnitud", new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical")).magnitude);


    }
	void grounded()
    {
		RaycastHit hit;
		IsGrounded = Physics.SphereCast(transform.position, 0.5f, -transform.up , out hit, 1.3f, layerWall);
		miAnim.SetBool("isGrounded", IsGrounded);
        

    }

}

