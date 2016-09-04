# BitMap32
## Что это?
Данная библиотека была написана в рамках курсовой работы на тему "Распознавание пальцев на основе цветойо дифференциации".
Весь код написан преимущественно на C# с использованием небольших вставок на C++.

##  Краткий алгоритм работы
<ol>
<li>Данная библиотека осуществляет захват изображений с веб-камеры</li>
<li>Переход к HSB-цветовой модели и первичная фильтрация пикселей на основе эмпирически подобранного цветового фильтра</li>
<li>Кластерный анализ при помощи алгоритма DBSCAN (плотностный алгоритм кластеризации пространственных данных с присутствием шума),
дабы отсеять ненужные шумы</li>
<li>Вывод положений пальцев</li>
</ol>

С <b>более подробным алгоритмом</b> можно ознакомиться в отчёте (report.docx).

##  Краткий алгоритм работы
 
 https://youtu.be/tDaC6rs7YH8
