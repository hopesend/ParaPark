using UnityEngine;
using System.Collections;
using Parapark.Enumeraciones;

public class ControlTeclado : MonoBehaviour 
{
	public ControlTiempo Reloj;
	public GameObject relojGrande;
	public GameObject luz;
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
			} 
			else 
			{
				OcultarReloj ();
				MostrarModelo ();
			}
		}
	}

	void Awake()
	{
		EstadoAplicacion = estado.enReloj;
		modeloRenderer = modelo.GetComponentInChildren<Renderer> ();
		modeloRenderer.material.color = new Color (modeloRenderer.material.color.r, modeloRenderer.material.color.g, modeloRenderer.material.color.b, 0);
		//modelo.SetActive (false);
	}

	void Update () 
	{
		if (Input.anyKey) 
		{
			if (Input.GetKeyDown (KeyCode.Tab)) 
			{
				EstadoAplicacion = EstadoAplicacion.Equals (estado.enReloj) ? estado.enModelo : estado.enReloj;
				return;
			}

			if(estadoAplicacion.Equals(estado.enReloj))
				TecladoEnReloj();
			else
				TecladoEnModelo();
		}
	}

	private void TecladoEnReloj()
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

	private void TecladoEnModelo()
	{
	}

	private void MostrarModelo()
	{
		//StartCoroutine(Mover3D(modelo.transform, modelo.transform.position, 0.2f));
		//StartCoroutine(Mover3D(camara.transform, new Vector3(camara.transform.position.y, camara.transform.position.y, -1.1f), 0.2f));
		//StartCoroutine(Mover3D(luz.transform, new Vector3(luz.transform.position.y, luz.transform.position.y, -11f), 0.2f));
	}

	private void OcultarModelo()
	{
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
}
