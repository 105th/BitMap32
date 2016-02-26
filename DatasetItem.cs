using System;
using System.Drawing;

//Each items in your dataset, preferences, features, points, etc.
public class DatasetItem
{
	public double X;
	public double Y;
	public Color Color;
	public HSB HSBColor;

	public DatasetItem(double x, double y, Color color, HSB hsbColor)
	{
		X = x;
		Y = y;
		Color = color;
		HSBColor = hsbColor;
	}
}

//	//Each items in your dataset, preferences, features, points, etc.
//	public class DatasetItem
//	{
//		public double X;
//		public double Y;
//		public double H;
//		public double S;
//		public double B;
//
//		public DatasetItem(double x,
//			double y,
//			double hue,
//			double saturation,
//			double brightness)
//		{
//			X = x;
//			Y = y;
//			H = hue;
//			S = saturation;
//			B =  brightness;
//		}
//	}
