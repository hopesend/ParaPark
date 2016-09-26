using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ControlTiempo : MonoBehaviour 
{
	public Text segundos;
	public Text minutos;
	public Text horas;
	public bool pausa;
	public float tiempo;

	private float s;
	private float m;
	private float h;

	void Start()
	{
		pausa = true;
	}

	void Update () 
	{
		if (!pausa) 
		{
			tiempo -= Time.deltaTime;
			MostrarTiempo ();
		}
	}

	public void MostrarTiempo()
	{
		horas.text = TimeSpan.FromSeconds(tiempo).Hours.ToString("00");
		minutos.text = TimeSpan.FromSeconds(tiempo).Minutes.ToString("00");
		segundos.text = TimeSpan.FromSeconds(tiempo).Seconds.ToString("00");
	}
}
