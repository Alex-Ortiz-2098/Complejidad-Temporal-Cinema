# 🔎 Buscador de Coincidencias Aproximadas (BK-Tree)

[cite_start]Este proyecto es una implementación de un motor de búsqueda eficiente basado en estructuras de datos métricas, desarrollado como trabajo final para la materia **Complejidad Temporal, Estructuras de Datos y Algoritmos** en la **UNAJ**[cite: 35, 37].

## 📋 Descripción
[cite_start]El objetivo del sistema es indexar datos provenientes de archivos CSV y permitir búsquedas de texto aproximadas (fuzzy matching) de manera eficiente[cite: 79]. [cite_start]A diferencia de una búsqueda exacta, este sistema permite encontrar resultados que se "parecen" al término buscado basándose en un umbral de tolerancia configurable[cite: 93, 99].

## ⚙️ Arquitectura y Tecnologías
* **Lenguaje:** C# (.NET)
* [cite_start]**Estructura de Datos Principal:** Árbol BK (Burkhard-Keller Tree), un árbol métrico diseñado para indexación y búsqueda rápida[cite: 53, 54].
* [cite_start]**Algoritmo de Medición:** Distancia de Levenshtein (calcula el número mínimo de operaciones requeridas para transformar una cadena en otra)[cite: 60, 107].

## 🚀 Funcionalidades
1.  [cite_start]**Indexación de Datos:** Carga masiva de términos desde archivos externos[cite: 80].
2.  **Búsqueda Configurable:**
    * Entrada de término a buscar.
    * [cite_start]Barra deslizante para ajustar la **Distancia** (nivel de tolerancia de la búsqueda)[cite: 93].
3.  **Consultas de Estructura:**
    * [cite_start]Visualización de **Caminos** y **Profundidad** del árbol[cite: 89, 90].
    * [cite_start]Reporte de nodos hoja y distribución por niveles[cite: 100, 102].
4.  [cite_start]**Predicciones:** Sugerencia de términos basados en la métrica de distancia implementada[cite: 88].

## 🧠 Desafíos Técnicos
* [cite_start]Implementación recursiva para la construcción del árbol BK, agrupando nodos según su distancia discreta a la raíz ($d(raiz, b) = k$)[cite: 58].
* Optimización de la búsqueda para descartar ramas completas del árbol que no cumplen con el criterio de la desigualdad triangular, mejorando la complejidad temporal respecto a una búsqueda lineal.

---
*Proyecto desarrollado por Alex Ortiz.*
