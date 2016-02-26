using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

public class Gesture
{
	List<DatasetItem>[] _fingers;
	DateTime _timeStamp;

	public Gesture ()
	{
		_timeStamp = DateTime.Now;
	}

	public int CheckGesture(List<DatasetItem>[] fingers)
	{
		if (_timeStamp == DateTime.Now)
			return (int) GestureNumber.NOTHING_HAS_DETECTED;

		// Обновляем время срабатывания
		_timeStamp = DateTime.Now;
		_fingers = fingers;


		Rectangle[] FingerRectangles = new Rectangle[5];

		for (int i = 0; i < _fingers.Length; i++)
		{
			var item = _fingers[i];
			Rectangle ThumbRect = new Rectangle((int) item.Min(a => a.X),
											(int) item.Min(a => a.Y),
											(int) item.Max(a => a.X) - (int) item.Min(a => a.X),
											(int) item.Max(a => a.Y) - (int) item.Min(a => a.Y));
			FingerRectangles[i] = ThumbRect;
		}

		for (int i = 1; i < _fingers.Length; i++) {
            int Distance = GetDistance(Center(FingerRectangles[(int)FingersName.THUMB]), Center(FingerRectangles[i]));

            if (Distance <= 50)
				return i;
		}

		return (int) GestureNumber.NOTHING_HAS_DETECTED;
	}

	int GetDistance(Point x, Point y)
	{
        int x1, x2, y1, y2;
        x1 = x.X;
        x2 = y.X;
        y1 = x.Y;
        y2 = y.Y;

		return (int) Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
	}

	Point Center(Rectangle rect)
	{
		return new Point(rect.Left + rect.Width/2,
			rect.Top + rect.Height / 2);
	}

}

