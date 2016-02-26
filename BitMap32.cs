using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

public class BitMap32
{
	/// <summary>
	/// Изображение для обработки представленное в виде массива байтов.
	/// </summary>
	byte[] m_imageBytes;

	/// <summary>
	/// Оригинальное изображение.
	/// </summary>
	Bitmap m_imgOriginal;

	/// <summary>
	/// Изображение, куда будем сохранять цветные сектора.
	/// </summary>
	Bitmap m_colors;

	/// <summary>
	/// Копия оригинального изображения.
	/// </summary>
	BitmapData m_copyOriginalImg;

	/// <summary>
	/// Заблокировано ли изображение или нет.
	/// </summary>
	bool m_IsLocked;

	/// <summary>
	/// Списки, куда будем сохранять подходящие точки.
	/// </summary>
	List<FingerPoint> Red;
	List<FingerPoint> Green;
	List<FingerPoint> Purple;
	List<FingerPoint> Yellow;
	List<FingerPoint> Blue;
	List<List<FingerPoint>> Fingers;


	/// <summary>
	/// Прямоугольники для пальцев.
	/// </summary>
	List<DatasetItem>[] FingersForGesture = new List<DatasetItem>[5];

	/// <summary>
	/// Создает экземпляр класса <see cref="WorkWithJpg.BitMap32"/>.
	/// </summary>
	/// <param name="path">Путь к файлу для открытия.</param>
	public BitMap32(string path)
	{
		m_imgOriginal = LoadBitmap(path);

		m_BitMap32();
	}

    /// <summary>
	/// Создает экземпляр класса <see cref="WorkWithJpg.BitMap32"/>.
	/// </summary>
	/// <param name="image">Изображение, с которого взять информацию.</param>
	public BitMap32(Image image)
    {
        m_imgOriginal = new Bitmap(image);

		m_BitMap32();
    }

	/// <summary>
	/// Приватный конструктор на основе которого строятся публичные.
	/// </summary>
	private void m_BitMap32() {
		m_colors = new Bitmap(m_imgOriginal.Width, m_imgOriginal.Height);

		Red = new List<FingerPoint>();
		Green = new List<FingerPoint>();
		Blue = new List<FingerPoint>();
		Yellow = new List<FingerPoint>();
		Purple = new List<FingerPoint>();

		Fingers = new List<List<FingerPoint>>();

		Fingers.Add(Red);
		Fingers.Add(Green);
		Fingers.Add(Blue);
		Fingers.Add(Yellow);
		Fingers.Add(Purple);

		for (int i = 0; i < FingersForGesture.Length; i++) {
			FingersForGesture[i] = new List<DatasetItem>();
		}	

		m_IsLocked = false;
	}

    /// <summary>
    /// Алгоритм первичного поиска цветных секторов по всему изображению
    /// </summary>
    public void FindColoredPlots()
	{
		// Блокируем пиксели для более быстрого доступа
		LockBitmap();


		// Пробегаемся по всему изображению в двойном цикле
		for (int i = 0; i < m_copyOriginalImg.Width; i += 5)
		{
			for (int j = 0; j < m_copyOriginalImg.Height; j += 5)
			{
				Color pixel = GetPixel(m_copyOriginalImg, i, j);

				// TODO
				// Посмотреть уровни по каналам
				// Закрашивание прямоугольника с помощью сечений
				// Выделить цвета в отдельные каналы
				// 5 пальцев будет достаточно

				// Переменные, в которые сохраняем цветовые байты
				int R = pixel.R;
				int G = pixel.G;
				int B = pixel.B;
				int A = pixel.A;

//				// Case Yellow
//				if (B != 0 && R >= 100 && G >= 100
//				    && R / B >= 1.1
//				    && G / B >= 1.1)
//				{
//					Yellow.Add(new FingerPoint(i, j, pixel, hsbpixel);
//					m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//					continue;
//				}
//
//				// Case Green
//				if (B != 0 && R != 0 &&
//				    G / R >= 1.3
//				    && G / B >= 1.5)
//				{
//					Green.Add(new FingerPoint(i, j, pixel, hsbpixel));
//					m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//					continue;
//				}
//
//				// Case Red
//				if (R >= 180
//				    && G <= 110
//				    && B <= 110)
//				{
//					Red.Add(new FingerPoint(i, j, pixel, hsbpixel));
//					m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//					continue;
//				}

//				// Case White
//				if (R >= 220
//				    && G >= 220
//				    && B >= 240)
//				{
//					White.Add(new FingerPoint(i, j, pixel, hsbpixel));
//					m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//					continue;
//				}
			}
		}

		// Разблокируем изображением.
		UnlockBitmap();
	}


    /// <summary>
    /// Алгоритм первичного поиска цветных секторов по всему изображению
    /// </summary>
    public void HSBFindColoredPlots()
    {
        // Блокируем пиксели для более быстрого доступа
        LockBitmap();


        // Пробегаемся по всему изображению в двойном цикле
        for (int i = 0; i < m_copyOriginalImg.Width; i += 5)
        {
            for (int j = 0; j < m_copyOriginalImg.Height; j += 5)
            {
                Color pixel = GetPixel(m_copyOriginalImg, i, j);
                HSB hsbpixel = Conversion.RGBtoHSB(pixel.R, pixel.G, pixel.B);

				if (hsbpixel.Brightness >= 0.3 && hsbpixel.Saturation >= 0.2)
                {

                
                    // Case Yellow
					if (hsbpixel.Saturation >= 0.3 && ((hsbpixel.Hue >= 40.0) && (hsbpixel.Hue <= 70.0)))
                    {
						Yellow.Add(new FingerPoint(i, j, pixel, hsbpixel));
                        m_colors.SetPixel(i, j, pixel);
                        continue;
                    }

                    // Case Green
                    if (hsbpixel.Hue >= 70.0 && hsbpixel.Hue <= 160.0)
                    {
                        Green.Add(new FingerPoint(i, j, pixel, hsbpixel));
                        m_colors.SetPixel(i, j, pixel);
                        continue;
                    }

                    // Case Red
					if (hsbpixel.Saturation >= 0.5 && (hsbpixel.Hue >= 355.0 || hsbpixel.Hue <= 18.0))
                    {
                        Red.Add(new FingerPoint(i, j, pixel, hsbpixel));
                        m_colors.SetPixel(i, j, pixel);
                        continue;
                    }

					// Case Blue
					if (hsbpixel.Saturation >= 0.5 && hsbpixel.Hue >= 210.0 && hsbpixel.Hue <= 240.0)
					{
						Blue.Add(new FingerPoint(i, j, pixel, hsbpixel));
						m_colors.SetPixel(i, j, pixel);
						continue;
					}

					// Case Purple
					if (hsbpixel.Hue >= 300.0 && hsbpixel.Hue <= 355.0)
					{
						Purple.Add(new FingerPoint(i, j, pixel, hsbpixel));
						m_colors.SetPixel(i, j, pixel);
						continue;
					}

                }
            }
        }
		// Разблокируем изображением.
		UnlockBitmap();

		// Класстеризуем каждый из найденных секторов
		m_Optimaze_Plots();
    }

	private void m_Optimaze_Plots() {

		// Создаем новый список пальцев (цветных секторов)
		List<DatasetItem[]> NewFingers = new List<DatasetItem[]>();

		// Пробегаемся по первичным секторам
		foreach (var oldFinger in Fingers) {

			// Сюда выделенные области
			HashSet<DatasetItem[]> clusters = new HashSet<DatasetItem[]>();
			// Переводим наш список Points в список DatasetItem
			List<DatasetItem> Cluster = new List<DatasetItem>();


			// Добавляем в список всех точек наши точки одного цвета из первичного сектора
			foreach (var item in oldFinger) {
				Cluster.Add(new DatasetItem(item.Point.X, item.Point.Y, item.Color, item.HSBColor));
			}


			// Создаем алгоритм подсчитывания расстояния (метрики)
			var dbs = new DbscanAlgorithm((x, y) => Math.Sqrt(((x.X - y.X) * (x.X - y.X)) + ((x.Y - y.Y) * (x.Y - y.Y))));
			// Выделяем из первичного сектора кластеры
			dbs.ComputeClusterDbscan(allPoints: Cluster.ToArray(), epsilon: 7.5, minPts: 8, clusters: out clusters);

            if (clusters.Count != 0)
            {
                // Ищем кластер с наибольшем количеством точек
                var maxPoints = clusters.Max(item => item.Length);
                // И запоминаем его
                var finger = clusters.First(item => item.Length == maxPoints);

                NewFingers.Add(finger);

				// Смотрим, какой цвет у пальца
				var fingerSaturation = finger.Average(item => item.HSBColor.Hue);


				if ((fingerSaturation >= 40.0) && (fingerSaturation <= 70.0)) // Case Yellow
					FingersForGesture[(int) FingersColor.YELLOW] = finger.ToList();
				else if (fingerSaturation >= 70.0 && fingerSaturation <= 160.0) // Case Green
					FingersForGesture[(int) FingersColor.GREEN] = finger.ToList();
				else if (fingerSaturation >= 355.0 || fingerSaturation <= 18.0) // Case Red
					FingersForGesture[(int) FingersColor.RED] = finger.ToList();
				else if (fingerSaturation >= 210.0 && fingerSaturation <= 240.0) // Case Blue
					FingersForGesture[(int) FingersColor.BLUE] = finger.ToList();
				else if (fingerSaturation >= 300.0 && fingerSaturation <= 355.0) // Case Purple
					FingersForGesture[(int) FingersColor.PURPLE] = finger.ToList();
            }
		}

		// Рисуем квадраты на исходном изображении
		using (var graphics = Graphics.FromImage(m_imgOriginal))
		{
			foreach (var item in NewFingers) {
                
                // Считываем цвет первого пикселя из массива
                LockBitmap();
                Color pixel = GetPixel(m_copyOriginalImg, (int) item[0].X, (int) item[0].Y);
                UnlockBitmap();

                // Задаем цвет рамки, равный цвету первого пикселя
                Pen Pen = new Pen(Color.FromArgb(pixel.R, pixel.G, pixel.B), 2);

                if (item.Length != 0) {
					// Draw rect
					graphics.DrawRectangle(Pen,
						(float) item.Min(a => a.X),
						(float) item.Min(a => a.Y),
						(float) item.Max(a => a.X) - (float) item.Min(a => a.X),
						(float) item.Max(a => a.Y) - (float) item.Min(a => a.Y));
				}
			}

			//m_imgOriginal.Save("clusters/test5_4/bla.jpg");
		}
	
	}


//    /// <summary>
//    /// Алгоритм первичного поиска цветных секторов по всему изображению
//    /// </summary>
//    public void TasksFindColoredPlots()
//	{
//		// Блокируем пиксели для более быстрого доступа
//		LockBitmap();
//
//		// Создаем изображение, где будем рисовать квадраты
//		m_colors = new Bitmap(m_copyOriginalImg.Width,
//			m_copyOriginalImg.Height);
//
//
//		List<Task> tasks = new List<Task>();
//
//		for (int i = 0; i < m_copyOriginalImg.Width; i += m_copyOriginalImg.Width / 5)
//		{
//			int iMax = i + m_copyOriginalImg.Width / 5;
//			int iStart = i;
//			tasks.Add(Task.Factory.StartNew(() => TaskSearch(iStart, iMax)));
//		}
//
//		// Ждем завершения тасков
//		Task.WaitAll(tasks.ToArray());
//
//		// Разблокируем изображением.
//		UnlockBitmap();
//    }

//	public void TaskSearch(int iStart, int iMax)
//	{
//		// Выносим объявление вне цикла для оптимизации
//		Color pixel;
//		int jMax = m_copyOriginalImg.Height;
//
//		// Пробегаемся по всему изображению в двойном цикле
//		for (int i = iStart; i < iMax; i += 5)
//		{
//			for (int j = 0; j < jMax; j += 5)
//			{
//				pixel = GetPixel(m_copyOriginalImg, i, j);
//
//
//				// Переменные, в которые сохраняем цветовые байты
//				int R = pixel.R;
//				int G = pixel.G;
//				int B = pixel.B;
//				int A = pixel.A;
//
//
//				// Case Yellow
//				if (B != 0 && R >= 100 && G >= 100
//				    && R / B >= 1.1
//				    && G / B >= 1.1)
//				{
//					lock (this)
//					{
//						Yellow.Add(new FingerPoint(i, j, pixel, hsbpixel));
//					}
//
//					m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//					continue;
//				}
//
//				// Case Green
//				if (B != 0 && R != 0 &&
//				    G / R >= 1.3
//				    && G / B >= 1.5)
//				{
//					lock (this)
//					{
//						Green.Add(new FingerPoint(i, j, pixel, hsbpixel));
//					}
//					m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//					continue;
//				}
//
//				// Case Red
//				if (R >= 180
//				    && G <= 110
//				    && B <= 110)
//				{
//					lock (this)
//					{
//						Red.Add(new FingerPoint(i, j, pixel, hsbpixel));
//					}
//					m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//					continue;
//				}
//
////				// Case White
////				if (R >= 220
////				    && G >= 220
////				    && B >= 240)
////				{
////					lock (this)
////					{
////						White.Add(new FingerPoint(i, j, pixel, hsbpixel));
////					}
////					m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
////					continue;
////				}
//			}
//		}
//	}
//
//	/// <summary>
//	/// Параллельный поиск цветных секторов.
//	/// </summary>
//	public void ParallelFindColoredPlots()
//	{
//		// Блокируем пиксели для более быстрого доступа
//		LockBitmap();
//
//		// Создаем изображение, где будем рисовать квадраты
//		m_colors = new Bitmap(m_copyOriginalImg.Width,
//			m_copyOriginalImg.Height);
//
//		int xTotal = m_copyOriginalImg.Width;
//		List<int> x = new List<int>();
//		while (xTotal >= 0)
//		{
//			x.Add(xTotal);
//			xTotal -= 5;
//		}
//
//		Parallel.ForEach(x, FindColoredPlotsInColumn);
////			Parallel.For(0, m_copyOriginalImg.Width, FindColoredPlotsInColumn);
//
//		// Разблокируем изображением.
//		UnlockBitmap();
//	}
//
//	/// <summary>
//	/// Поиск в одной колонке (для параллельного поиска)
//	/// </summary>
//	/// <param name="i">Номер колонки.</param>
//	void FindColoredPlotsInColumn(int i)
//	{
//		for (int j = 0; j < m_copyOriginalImg.Height; j += 5)
//		{
//			Color pixel = GetPixel(m_copyOriginalImg, i, j);
//
//
//			// Переменные, в которые сохраняем цветовые байты
//			int R = pixel.R;
//			int G = pixel.G;
//			int B = pixel.B;
//			int A = pixel.A;
//
//
//			// Case Yellow
//			if (B != 0 && R >= 100 && G >= 100
//			    && R / B >= 1.1
//			    && G / B >= 1.1)
//			{
//				lock (this)
//				{
//					Yellow.Add(new FingerPoint(i, j, pixel, hsbpixel));
//				}
//				m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//			}
//
//			// Case Green
//			if (B != 0 && R != 0 &&
//			    G / R >= 1.3
//			    && G / B >= 1.5)
//			{
//				lock (this)
//				{
//					Green.Add(new FingerPoint(i, j, pixel, hsbpixel));
//				}
//				m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//				continue;
//			}
//
//			// Case Red
//			if (R >= 180
//			    && G <= 110
//			    && B <= 110)
//			{
//				lock (this)
//				{
//					Red.Add(new FingerPoint(i, j, pixel, hsbpixel));
//				}
//				m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
//				continue;
//			}
//
////			// Case White
////			if (R >= 220
////			    && G >= 220
////			    && B >= 240)
////			{
////				lock (this)
////				{
////					White.Add(new FingerPoint(i, j, pixel, hsbpixel));
////				}
////				m_colors.SetPixel(i, j, Color.FromArgb(A, R, G, B));
////				continue;
////			}
//		}
//	}
//
	/// <summary>
	/// Блокирует пиксели на изображении.
	/// </summary>
	void LockBitmap()
	{
		// Если уже заблокированы, то ничего не делать.
		if (m_IsLocked)
			return;

		// Создаем прямоугольник для блокирования
		Rectangle bounds = new Rectangle(0, 0,
			                   m_imgOriginal.Width, m_imgOriginal.Height);

		// Блокирует данные на оригинальном изображении (накрывая прямоугольником)
		m_copyOriginalImg = m_imgOriginal.LockBits(bounds,
			ImageLockMode.ReadWrite,
			PixelFormat.Format32bppArgb);
		
		// Распределить память для данных.
		int total_size = m_copyOriginalImg.Stride * m_copyOriginalImg.Height;

		m_imageBytes = new byte[total_size];
		// Копировать данные в массив ImageBytes.
		Marshal.Copy(m_copyOriginalImg.Scan0, m_imageBytes, 0, total_size);

		m_IsLocked = true;
	}

	/// <summary>
	/// Разблокирует пиксели: копирует данные обратно в Bitmap и освобождает ресурсы.
	/// </summary>
	void UnlockBitmap()
	{
		// Уже разблокировано, ничего не делать.
		if (!m_IsLocked)
			return;
		// Копировать данные обратно в bitmap.
		int total_size = m_copyOriginalImg.Stride * m_copyOriginalImg.Height;
		Marshal.Copy(m_imageBytes, 0, m_copyOriginalImg.Scan0, total_size);
		// Разблокировать изображение.
		m_imgOriginal.UnlockBits(m_copyOriginalImg);
		// Освободить ресурсы.
		m_imageBytes = null;
		m_copyOriginalImg = null;

		// Теперь разблокировано.
		m_IsLocked = false;
	}

	/// <summary>
	/// Возвращает цветовые характеристики (R, G, B) и степень прозрачности (A) 
	/// из указанного изображения по указанным координатам.
	/// </summary>
	/// <param name="m_BitmapData">Изображение, откуда брать пиксель.</param>
	/// <param name="x">Координата X пикселя</param>
	/// <param name="y">Координата Y пикселя.</param>
	/// <param name="red">Выходной байт для красного цвета</param>
	/// <param name="green">Выходной байт для зеленого цвета.</param>
	/// <param name="blue">Выходной байт для синего цвета.</param>
	/// <param name="alpha">Выходной байт для степени прозрачности.</param>
	public Color GetPixel(BitmapData m_BitmapData, int x, int y)
	{
		int i = y * m_BitmapData.Stride + x * 4;

		int blue = (int)m_imageBytes[i++];
		int green = (int)m_imageBytes[i++];
		int red = (int)m_imageBytes[i++];
		int alpha = (int)m_imageBytes[i];

		Color pixel = Color.FromArgb(alpha, red, green, blue);

		return pixel;
	}

	/// <summary>
	/// Рисуем цветовые сектора и сохраняем по указанному адресу.
	/// </summary>
	/// <param name="nameFile>Имя сохранённого файла.</param>
	public void DrawColoredPlots(string nameFile)
	{
        // Рисуем квадраты на дополнительном изображении
        using (var graphics = Graphics.FromImage(m_colors))
		{
			Pen Pen = new Pen(Color.Black, 1);

			foreach (var item in Fingers) {
				if (item.Count != 0) {
					// Draw rect
					graphics.DrawRectangle(Pen,
						item.Min(a => a.Point.X),
						item.Min(a => a.Point.Y),
						item.Max(a => a.Point.X) - item.Min(a => a.Point.X),
						item.Max(a => a.Point.Y) - item.Min(a => a.Point.Y));
				}
			}

            m_colors.Save(nameFile + "colors_painted.png");

			// Console.WriteLine("Results saved!");
		}

        // Рисуем квадраты на исходном изображении
        using (var graphics = Graphics.FromImage(m_imgOriginal))
        {
            Pen Pen = new Pen(Color.Black, 1);

			foreach (var item in Fingers) {
				if (item.Count != 0) {
					// Draw rect
					graphics.DrawRectangle(Pen,
						item.Min(a => a.Point.X),
						item.Min(a => a.Point.Y),
						item.Max(a => a.Point.X) - item.Min(a => a.Point.X),
						item.Max(a => a.Point.Y) - item.Min(a => a.Point.Y));
				}
			}

            m_imgOriginal.Save(nameFile + "original_colors_painted.jpg");

            
            // Console.WriteLine("Results saved!");
        }
    }


	/// <summary>
	/// Загружает изображение как Bitmap из указанного файла.
	/// </summary>
	/// <returns>The bitmap.</returns>
	/// <param name="fileName">File name.</param>
	public static Bitmap LoadBitmap(string fileName)
	{
		using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			return new Bitmap(fs);
	}

    public Bitmap ImageWithColoredPlots()
    {
        return m_imgOriginal;
    }

	public List<DatasetItem>[] GetFingers()
	{
		return FingersForGesture;
	}
}
