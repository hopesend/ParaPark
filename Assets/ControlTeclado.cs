using UnityEngine;
using System;
using System.Collections;
using Parapark.Enumeraciones;

public class ControlTeclado : MonoBehaviour 
{
	public ControlTiempo Reloj;
	public GameObject relojGrande;
	public Light luz;
	public GameObject modelo;
	public GameObject camara;

	private Renderer modeloRenderer;
	private estado estadoAplicacion;
	public estado EstadoAplicacion
	{
		get { return estadoAplicacion; }
		set 
		{
			estadoAplicacion = value;
			if (estadoAplicacion.Equals (estado.enReloj)) 
			{
				MostrarReloj ();
				OcultarModelo ();
				return;
			} 

			if	(estadoAplicacion.Equals (estado.enModelo)) 
			{
				OcultarReloj ();
				MostrarModelo ();
				return;
			}

			if(estadoAplicacion.Equals(estado.enReproduccionVideo))
			{
				OcultarReloj ();
				OcultarModelo ();
				return;
			}
		}
	}

	void Awake()
	{
		luz.intensity = 0f;
		modelo.SetActive (false);
		EstadoAplicacion = estado.enConfiguracion;
	}

	void Update () 
	{
		if (Input.anyKey) 
		{
			switch (estadoAplicacion) 
			{
				case estado.enConfiguracion:
					{
						TecladoEnReloj ();
						if (Input.GetKeyDown (KeyCode.Space)) 
						{
							if (Reloj.tiempo > 0) 
							{
								Reloj.pausa = false;
								EstadoAplicacion = estado.enReloj;
							}
						}
						break;
					}

				case estado.enModelo:
					{
						TecladoEnModelo ();
						if (Input.GetKeyDown (KeyCode.Tab)) 
							EstadoAplicacion = estado.enReloj;
						break;
					}

				case estado.enReloj:
					{
						
						if (Input.GetKeyDown (KeyCode.Tab)) 
							EstadoAplicacion = estado.enModelo;
						break;
					}
			}
		}
	}

	private void TecladoEnReloj()
	{
		// (Intro) Reinicio de contador
		if (Input.GetKeyDown (KeyCode.Return)) 
			Reloj.tiempo = 0;

		// (F1) aumenta 60 minutos el tiempo
		if (Input.GetKeyDown (KeyCode.F1)) 
			Reloj.tiempo += 3600;

		// (F2) aumenta 1 mintuo el tiempo
		if (Input.GetKeyDown (KeyCode.F2)) 
			Reloj.tiempo += 60;

		Reloj.MostrarTiempo ();
	}

	private void TecladoEnModelo()
	{
		// (F3) Video 1
		if (Input.GetKeyDown (KeyCode.F3)) 
		{
			ReproducirVideo (1);
			return;
		}

		// (F4) Video 2
		if (Input.GetKeyDown (KeyCode.F4)) 
		{
			ReproducirVideo (2);
			return;
		}

		// (F5) Video 3
		if (Input.GetKeyDown (KeyCode.F5)) 
		{
			ReproducirVideo (3);
			return;
		}

		// (F6) Video 4
		if (Input.GetKeyDown (KeyCode.F6)) 
		{
			ReproducirVideo (4);
			return;
		}

		// (F7) Video 5
		if (Input.GetKeyDown (KeyCode.F7)) 
		{
			ReproducirVideo (5);
			return;
		}

		// (F8) Video 6
		if (Input.GetKeyDown (KeyCode.F8)) 
		{
			ReproducirVideo (6);
			return;
		}

		// (F9) Video 7
		if (Input.GetKeyDown (KeyCode.F9)) 
		{
			ReproducirVideo (7);
			return;
		}

		// (F10) Video 8
		if (Input.GetKeyDown (KeyCode.F10)) 
		{
			ReproducirVideo (8);
			return;
		}

		// (F11) Video 9
		if (Input.GetKeyDown (KeyCode.F11)) 
		{
			ReproducirVideo (9);
			return;
		}
	}

	private void MostrarModelo()
	{
		modelo.SetActive (true);
		StartCoroutine(IntensidadLuz(luz, 4f, 0.4f));
		//StartCoroutine(Mover3D(modelo.transform, modelo.transform.position, 0.2f));
		//StartCoroutine(Mover3D(camara.transform, new Vector3(camara.transform.position.y, camara.transform.position.y, -1.1f), 0.2f));
		//StartCoroutine(Mover3D(luz.transform, new Vector3(luz.transform.position.y, luz.transform.position.y, -11f), 0.2f));
	}

	private void OcultarModelo()
	{
		StartCoroutine(IntensidadLuz(luz, 0, 0.4f));
		modelo.SetActive (false);
		//StartCoroutine(Mover3D(modelo.transform, modelo.transform.position, 0.2f));
		//StartCoroutine(Mover3D(camara.transform, new Vector3(camara.transform.position.y, camara.transform.position.y, -30.7f), 0.2f));
		//StartCoroutine(Mover3D(luz.transform, new Vector3(luz.transform.position.y, luz.transform.position.y, -2.5f), 0.2f));
	}

	private void MostrarReloj()
	{
		StartCoroutine (Mover2D(relojGrande.transform, new Vector2(0, 0), new Vector2(1, 1), 0));
	}

	private void OcultarReloj()
	{
		StartCoroutine (Mover2D(relojGrande.transform, new Vector2(330, 190), new Vector2(0.5f, 0.5f), 0));
	}

	public IEnumerator Mover2D(Transform objeto, Vector2 destino, Vector2 escala, float suavizado)
	{
		while (true)
		{
			float tiempo = 0f;
			tiempo += Time.deltaTime;
			objeto.localPosition = Vector2.Lerp(objeto.localPosition, destino, (tiempo / suavizado));
			objeto.localScale = Vector2.Lerp (objeto.localScale, escala, (tiempo / suavizado));

			if ((objeto.localPosition.x == destino.x) && (objeto.localPosition.y == destino.y)) 
				yield break;

			yield return null;
		}
	}

	public IEnumerator Mover3D(Transform objeto, Vector3 destinoPosicion, float suavizado)
	{
		while (true) {
			float tiempo = 0f;
			tiempo += Time.deltaTime;
			objeto.position = Vector3.Lerp (objeto.position, destinoPosicion, (tiempo / suavizado));

			if (objeto.position.ToString ().Equals (destinoPosicion.ToString ())) 
				yield break;

			yield return null;
		}
	}

	public IEnumerator CambiarTransparencia(Renderer modelRenderer, bool effect,  float suavizado)
	{
		//effect true mostras objecto, false ocultar modelo
		while (true) 
		{
			float tiempo = 0f;
			tiempo += Time.deltaTime;
			if(effect)
				modelRenderer.material.color = Color.Lerp(modelRenderer.material.color, new Color(modelRenderer.material.color.r, modelRenderer.material.color.g, modelRenderer.material.color.b, 1), (tiempo / suavizado));
			else
				modelRenderer.material.color = Color.Lerp(modelRenderer.material.color, new Color(modelRenderer.material.color.r, modelRenderer.material.color.g, modelRenderer.material.color.b, 0), (tiempo / suavizado));

			if(effect)
				if (modelRenderer.material.color.a.Equals (1))
					yield break;
			else
				if (modelRenderer.material.color.a.Equals (0))
					yield break;

			yield return null;
		}
	}

	public IEnumerator IntensidadLuz(Light foco, float intensidad, float suavizado)
	{
		while (true) 
		{
			float tiempo = 0f;
			tiempo += Time.deltaTime;
			foco.intensity = Mathf.Lerp (foco.intensity, intensidad, suavizado);

			if (foco.intensity.Equals(intensidad)) 
				yield break;

			yield return null;
		}
	}

	public void ReproducirVideo(int numero)
	{
		//Cargar los videos
		//reproducir video
	}
}
