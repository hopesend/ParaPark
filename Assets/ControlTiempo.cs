using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
			tiempo += Time.deltaTime;
			MostrarTiempo ();
		}
	}

	public void MostrarTiempo()
	{
		minutos.text = Mathf.Floor (tiempo / 60) >= 60 ? ((tiempo/60)-60).ToString("00") : Mathf.Floor (tiempo / 60).ToString ("00");
		segundos.text = Mathf.Floor (tiempo % 60) >= 60 ? "00" : Mathf.Floor (tiempo % 60).ToString ("00");
		horas.text = Mathf.Floor (tiempo / 3600) >= 60 ? ((tiempo/3600)-60).ToString("00") : Mathf.Floor (tiempo / 3600).ToString ("00");
	}
}
