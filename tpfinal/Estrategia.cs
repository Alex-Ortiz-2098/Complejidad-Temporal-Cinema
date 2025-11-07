
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

		public string Consulta1(ArbolGeneral<DatoDistancia> arbol)
		{
			if (arbol == null || arbol.getDatoRaiz() == null)
            {
                return "";
            }	

			List<string> hojas = new List<string>();
			Queue<ArbolGeneral<DatoDistancia>> cola = new Queue<ArbolGeneral<DatoDistancia>>();
			cola.Enqueue(arbol);

			while (cola.Count > 0)
			{
				var nodo = cola.Dequeue();

				// Si no tiene hijos, es hoja
				if (nodo.getHijos().Count == 0)
				{
					hojas.Add(nodo.getDatoRaiz().texto);
				}
				else
				{
					// Encolamos todos los hijos
					foreach (var hijo in nodo.getHijos())
					{
						cola.Enqueue(hijo);
					}
				}
			}

			return string.Join(", ", hojas);
		}



		public string Consulta2(ArbolGeneral<DatoDistancia> arbol)
		{
			//Revisamos si el arbol no esta vacio
			if (arbol == null || arbol.getDatoRaiz() == null)
				return "";
			//Creamos la lista donde se guardan el camino que se recorre hasta la hoja
			List<string> caminos = new List<string>();

			// Cola que guarda el camino desde el nodo hasta ese nodo hoja
			Queue<(ArbolGeneral<DatoDistancia> nodo, List<string> camino)> cola = new Queue<(ArbolGeneral<DatoDistancia>, List<string>)>();
			
			cola.Enqueue((arbol, new List<string> { arbol.getDatoRaiz().texto }));

			while (cola.Count > 0) // mientras la cola tenga objetos
			{
				var (nodo, caminoActual) = cola.Dequeue();

				if (nodo.getHijos().Count == 0) // SI el nodo encontrado es Hoja
				{
					// Nodo hoja: agregamos el camino completo
					caminos.Add(string.Join(" -> ", caminoActual));
				}
				else
				{
					foreach (var hijo in nodo.getHijos())// Si no es hoja, seguimos iterando buscando los hijos del nodo
					{
						// Creamos una copia del camino actual y le agregamos el hijo
						List<string> nuevoCamino = new List<string>(caminoActual);
						nuevoCamino.Add(hijo.getDatoRaiz().texto);
						cola.Enqueue((hijo, nuevoCamino));
					}
				}
			}

			// Devolver todos los caminos concatenados, separados por saltos de línea
			return string.Join("\n", caminos);
		}



		public String Consulta3(ArbolGeneral<DatoDistancia> arbol)
		{
			string result = "Implementar";
		
			return result;
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