using UnityEngine;
using System.Collections;

public class ControlTeclado : MonoBehaviour 
{
	public ControlTiempo Reloj;

	void Update () 
	{
		if (Input.anyKey) 
		{
			// (Barra Espaciadora) Comienzo y pausa 
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				Reloj.pausa = !Reloj.pausa;
				return;
			}

			// (Intro) Reinicio de contador
			if (Input.GetKeyDown (KeyCode.Return)) 
			{
				Reloj.tiempo = 0;
				Reloj.MostrarTiempo ();
				return;
			}

			// (F5) aumenta de 5 en 5 el tiempo
			if (Input.GetKeyDown (KeyCode.F5)) 
			{
				Reloj.tiempo += 300;
				Reloj.MostrarTiempo ();
				return;
			}

			// (F10) aumenta de 10 en 10 el tiempo
			if (Input.GetKeyDown (KeyCode.F10)) 
			{
				Reloj.tiempo += 600;
				Reloj.MostrarTiempo ();
				return;
			}
		}
	}
}
