using UnityEngine;
using System;
using System.Collections;
using System.IO;
using Parapark.Enumeraciones;

public class ControlTeclado : MonoBehaviour 
{
	public ControlTiempo Reloj;
	public GameObject relojGrande;
	public Light luz;
	public GameObject modelo;
	public GameObject camara;


	private string[] pathVideos = new string[6];
	private MovieTexture[] videos = new MovieTexture[6];
	private string[] pathAudios = new string[6];
	private AudioClip[] audios = new AudioClip[6];
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
		cXML nuevoXML = new cXML ();
		pathVideos = nuevoXML.Cargar_Clase_Serializable<string[]>(Path.Combine(Environment.CurrentDirectory, "videos.xml"), pathVideos);
		cargarVideos ();
		pathAudios = nuevoXML.Cargar_Clase_Serializable<string[]>(Path.Combine(Environment.CurrentDirectory, "audios.xml"), pathAudios);
		cargarAudios ();
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

		// (Q) aumenta 60 minutos el tiempo
		if (Input.GetKeyDown (KeyCode.Q)) 
			Reloj.tiempo += 3600;

		// (W) aumenta 1 mintuo el tiempo
		if (Input.GetKeyDown (KeyCode.W)) 
			Reloj.tiempo += 60;

		Reloj.MostrarTiempo ();
	}

	private void TecladoEnModelo()
	{
		// (F1) Video 1
		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			ReproducirVideo (1);
			return;
		}

		// (F2) Video 2
		if (Input.GetKeyDown (KeyCode.F2)) 
		{
			ReproducirVideo (2);
			return;
		}

		// (F3) Video 3
		if (Input.GetKeyDown (KeyCode.F3)) 
		{
			ReproducirVideo (3);
			return;
		}

		// (F4) Video 4
		if (Input.GetKeyDown (KeyCode.F4)) 
		{
			ReproducirVideo (4);
			return;
		}

		// (F5) Video 5
		if (Input.GetKeyDown (KeyCode.F5)) 
		{
			ReproducirVideo (5);
			return;
		}

		// (F6) Video 6
		if (Input.GetKeyDown (KeyCode.F6)) 
		{
			ReproducirVideo (6);
			return;
		}

		// (F7) Audio 1
		if (Input.GetKeyDown (KeyCode.F7)) 
		{
			ReproducirAudio (1);
			return;
		}

		// (F8) Audio 2
		if (Input.GetKeyDown (KeyCode.F8)) 
		{
			ReproducirAudio (2);
			return;
		}

		// (F9) Audio 3
		if (Input.GetKeyDown (KeyCode.F9)) 
		{
			ReproducirAudio (3);
			return;
		}

		// (F10) Audio 4
		if (Input.GetKeyDown (KeyCode.F10)) 
		{
			ReproducirAudio (4);
			return;
		}

		// (F11) Audio 5
		if (Input.GetKeyDown (KeyCode.F11)) 
		{
			ReproducirAudio (5);
			return;
		}

		// (F12) Audio 6
		if (Input.GetKeyDown (KeyCode.F12)) 
		{
			ReproducirAudio (6);
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

	/*public IEnumerator cargaVideos(string path)
	{
		WWW pelicula = new WWW(path);

		//Wait for file finish loading
		while(!pelicula.isDone)
		{
			yield return null;
		}

		//Save the loaded movie from WWW to movetexture
		MovieTexture movieToPlay = pelicula.movie;

		//Hook the movie texture to the current renderer
		MeshRenderer ren = container.GetComponent<MeshRenderer>();
		ren.material.mainTexture = movieToPlay ;    

		movieToPlay.play();
	}*/

	public void cargarVideos()
	{
		for(int cont = 0; cont < 5; cont++)
		{
			if(pathVideos[cont] != null)
				videos[cont] = new WWW("file:///" + pathVideos[cont]).movie;
		}
	}

	public void cargarAudios()
	{
		for(int cont = 0; cont < 5; cont++)
		{
			if(pathAudios[cont] != null)
				audios[cont] = new WWW("file:///" + pathAudios[cont]).audioClip;
		}
	}

	public void ReproducirVideo(int numero)
	{
		//Cargar los videos
		//reproducir video
	}

	public void ReproducirAudio(int numero)
	{
		//Cargar los audios
		//reproducir audio
	}
}
