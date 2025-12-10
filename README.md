# 🔎 Buscador de Coincidencias Aproximadas (BK-Tree)

Este proyecto es una implementación de un motor de búsqueda eficiente basado en estructuras de datos métricas, desarrollado como trabajo final para la materia **Complejidad Temporal, Estructuras de Datos y Algoritmos** en la **UNAJ**

## 📋 Descripción
El objetivo del sistema es indexar datos provenientes de archivos CSV y permitir búsquedas de texto aproximadas (fuzzy matching) de manera eficiente. 
A diferencia de una búsqueda exacta, este sistema permite encontrar resultados que se "parecen" al término buscado basándose en un umbral de tolerancia configurable.

## ⚙️ Arquitectura y Tecnologías
* **Lenguaje:** C# (.NET)
* **Estructura de Datos Principal:** Árbol BK (Burkhard-Keller Tree), un árbol métrico diseñado para indexación y búsqueda rápida.
* **Algoritmo de Medición:** Distancia de Levenshtein (calcula el número mínimo de operaciones requeridas para transformar una cadena en otra).

## 🚀 Funcionalidades
1.  **Indexación de Datos:** Carga masiva de términos desde archivos externos.
2.  **Búsqueda Configurable:**
    * Entrada de término a buscar.
    * Barra deslizante para ajustar la **Distancia** (nivel de tolerancia de la búsqueda).
3.  **Consultas de Estructura:**
    * Visualización de **Caminos** y **Profundidad** del árbol.
    * Reporte de nodos hoja y distribución por niveles.
4.  **Predicciones:** Sugerencia de términos basados en la métrica de distancia implementada.

## 🧠 Desafíos Técnicos
* Implementación recursiva para la construcción del árbol BK, agrupando nodos según su distancia discreta a la raíz ($d(raiz, b) = k$).
* Optimización de la búsqueda para descartar ramas completas del árbol que no cumplen con el criterio de la desigualdad triangular, mejorando la complejidad temporal respecto a una búsqueda lineal.

---
*Proyecto desarrollado por Alex Ortiz.*
