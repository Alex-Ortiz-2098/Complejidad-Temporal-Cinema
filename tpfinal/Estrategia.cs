
using System;
using System.Collections.Generic;
using tp1;

namespace tpfinal
{

	public class Estrategia
	{
		private int CalcularDistancia(string str1, string str2)
		{
			// using the method
			String[] strlist1 = str1.ToLower().Split(' ');
			String[] strlist2 = str2.ToLower().Split(' ');
			int distance = 1000;
			foreach (String s1 in strlist1)
			{
				foreach (String s2 in strlist2)
				{
					distance = Math.Min(distance, Utils.calculateLevenshteinDistance(s1, s2));
				}
			}

			return distance;
		}

		public string Consulta1(ArbolGeneral<DatoDistancia> arbol) // MUESTRA TODA LAS HOJAS
		{
			if (arbol == null || arbol.getDatoRaiz() == null)// revisamos que el arbol no este vacio
			{
				return "";
			}

			List<string> hojas = new List<string>(); //Lista donde guardamos las HOJAS
			Queue<ArbolGeneral<DatoDistancia>> cola = new Queue<ArbolGeneral<DatoDistancia>>(); //Cola donde guardamos el ARBOL
			cola.Enqueue(arbol); //encolamos el arbol

			while (cola.Count > 0)
			{
				ArbolGeneral<DatoDistancia> nodo = cola.Dequeue(); //Desencolamos parte del arbol

				// Si no tiene hijos, es hoja
				if (nodo.getHijos().Count == 0)
				{
					hojas.Add(nodo.getDatoRaiz().texto);
				}
				else
				{
					// Encolamos todos los hijos
					foreach (ArbolGeneral<DatoDistancia> hijo in nodo.getHijos())// Encolamos los HIJOS 
					{
						cola.Enqueue(hijo);
					}
				}
			}

			return string.Join(", ", hojas);
		}




		public string Consulta2(ArbolGeneral<DatoDistancia> arbol) //MUESTRA TODOS LOS CAMINOS A UNA HOJA
		{
			if (arbol == null || arbol.getDatoRaiz() == null)
				return "";

			List<string> caminos = new List<string>();// Lista donde guardamos los caminos a una Hoja

			// Cola para nodos
			Queue<ArbolGeneral<DatoDistancia>> colaNodos = new Queue<ArbolGeneral<DatoDistancia>>();
			// Cola para caminos
			Queue<List<string>> colaCaminos = new Queue<List<string>>();

			colaNodos.Enqueue(arbol);// Encolamos el Arbol
			colaCaminos.Enqueue(new List<string> { arbol.getDatoRaiz().texto });// encolamos la RAIZ DEL ARBOL

			while (colaNodos.Count > 0)//hasta que se acabe
			{
				ArbolGeneral<DatoDistancia> nodo = colaNodos.Dequeue(); //Desconlamos el nodo
				List<string> caminoActual = colaCaminos.Dequeue();// Desencolamos el camino para corroborar si tenemos hijsos

				if (nodo.getHijos().Count == 0) //Revisamos si es HOJA
				{
					// Nodo hoja: agregamos el camino completo
					caminos.Add(string.Join(" -> ", caminoActual));
				}
				else
				{
					foreach (ArbolGeneral<DatoDistancia> hijo in nodo.getHijos())// Recorremos la cola dentro de los nodos para agregar los HIJOS a la cola
					{
						List<string> nuevoCamino = new List<string>(caminoActual);//Guardamos el camino hasta el punto Actual
						nuevoCamino.Add(hijo.getDatoRaiz().texto);//Guardamos el Hijo del NODO

						colaNodos.Enqueue(hijo); //Encolamos a al hijo
						colaCaminos.Enqueue(nuevoCamino); // Encolamos el camino
					}
				}
			}

			return string.Join("\n", caminos); // UNE TODOS LOS CAMINOS EN LINEAS SEPARADASA
		}



		public string Consulta3(ArbolGeneral<DatoDistancia> arbol)//MUESTRA POR NIVEL LOS DATOS
		{
			if (arbol == null || arbol.getDatoRaiz() == null)
			{
				return "";
			}

			//Cola para recorrido en anchura
			Queue<ArbolGeneral<DatoDistancia>> cola = new Queue<ArbolGeneral<DatoDistancia>>();
			cola.Enqueue(arbol);//Iniciamos encolando el arbol

			string resultado = ""; //Donde se guarda el resultado del nivel
			int nivel = 0; //Guarda el nivel(comenzamos en la raiz)

			while (cola.Count > 0)//minetras haya nodos para procesar
			{
				int nodosEnNivel = cola.Count; // cantidad de nodos en este nivel
				List<string> datosNivel = new List<string>(); //Guarda los datos almacenados en el nivel

				for (int i = 0; i < nodosEnNivel; i++)
				{
					ArbolGeneral<DatoDistancia> nodo = cola.Dequeue(); // desencolamos por nodo
					datosNivel.Add(nodo.getDatoRaiz().texto);//Ingresamos los datos a la lista

					foreach (ArbolGeneral<DatoDistancia> hijo in nodo.getHijos())
					{
						cola.Enqueue(hijo); //Encolamos los hijos
					}
				}

				//Guardamos el resultado de este nivel
				resultado += "Nivel " + nivel.ToString() + ": " + string.Join(", ", datosNivel) + "\n";
				nivel++;
			}

			return resultado;
		}


		public void AgregarDato(ArbolGeneral<DatoDistancia> arbol, DatoDistancia dato)
		{
			// Calculamos la distancia entre el texto de la raíz y el nuevo dato
			int distancia = CalcularDistancia(arbol.getDatoRaiz().texto, dato.texto);

			if(distancia == 0) //Caso Base
            {
				return;
            }

			// Asignar la distancia al dato por parametro
			dato.distancia = distancia;

			//realizamos la busqueda para corroborar si ya existe un hijo con esa distancia
			foreach (var hijo in arbol.getHijos())
			{
				if (hijo.getDatoRaiz().distancia == distancia)
				{
					// Si existe un hijo que iguale la distancia del dato por parametro, se llama a la recursion
					AgregarDato(hijo, dato); // hijo (nuevo padre/raiz)
					return;
				}
			}

			//De no encontrar un dato con la misma distacia, creamos un nuevo nodo hijo
			arbol.agregarHijo(new ArbolGeneral<DatoDistancia>(dato)); 
		}

		public void Buscar(ArbolGeneral<DatoDistancia> arbol, string elementoABuscar, int umbral, List<DatoDistancia> collected)
		{
			if (arbol == null || arbol.getDatoRaiz() == null)
			{
				return;
			}
			
			//Calculamos la distancia de la raiz con el ElemnetoABuscar
			int distancia = CalcularDistancia(arbol.getDatoRaiz().texto, elementoABuscar);

			//Si la distancia es menor se agrega a la lista
			if (distancia <= umbral)
			{
				collected.Add(new DatoDistancia(distancia, arbol.getDatoRaiz().texto, arbol.getDatoRaiz().descripcion));
			}
			
			//Recorremos el arbo en busqueda de llenar el arbol con datos dentro del Umbral
			foreach(var hijo in arbol.getHijos())
			{
				//Guardamos la distancia del hijo de la raiz
				int distHijo = hijo.getDatoRaiz().distancia;
				
				//Usamos la formula de DESIGUALDAD TRIANGULAR, copara las distancias generando un rango de busqueda para evitar revisar ramas fuera del umbral
				if (distHijo >= distancia - umbral && distHijo <= distancia + umbral)
				{

					Buscar(hijo, elementoABuscar, umbral, collected);
				}
			}

        }
    }
}