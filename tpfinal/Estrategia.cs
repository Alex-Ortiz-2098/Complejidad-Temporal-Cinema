
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

		public String Consulta1(ArbolGeneral<DatoDistancia> arbol)
		{
			string resutl = "Implementar";
			return resutl;
		}


		public String Consulta2(ArbolGeneral<DatoDistancia> arbol)
		{
			string result = "Implementar";

            return result;
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
			//Calculamos la distancia de la raiz con el ElemnetoABuscar
			int distancia = CalcularDistancia(arbol.getDatoRaiz().texto, elementoABuscar);

			//Si la distancia es menor se agrega a la lista
			if (distancia <= umbral)
			{
				collected.Add(new DatoDistancia(arbol.getDatoRaiz().distancia, arbol.getDatoRaiz().texto, arbol.getDatoRaiz().descripcion));
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