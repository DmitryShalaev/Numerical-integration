﻿using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Parser.Analog;
public class AnalogParser {
	private readonly List<PointF> list;

	public float LeftBorder => list[0].X;
	public float RightBorder => list[^1].X;
	public float BottomBorder { get; private set; }
	public float UpperBorder { get; private set; }

	private double prevX = double.MaxValue;
	private int lastIndex = 0;

	public AnalogParser(string src) {
		list = new List<PointF>();

		using(StreamReader SR = new(src)) {
			string[] str = SR.ReadToEnd().Replace('.', ',').Split();
			foreach(var item in str) {
				if(!string.IsNullOrWhiteSpace(item)) {
					string[] parts = item.Split(';');
					list.Add(new(float.Parse(parts[0]), float.Parse(parts[1])));

					if(BottomBorder >= list[^1].Y)
						BottomBorder = list[^1].Y;
					else if(UpperBorder <= list[^1].Y)
						UpperBorder = list[^1].Y;
				}
			}
		}
	}

	public double Interpolate(double x) {
		if(x < prevX)
			lastIndex = 0;

		prevX = x;

		for(int i = lastIndex; i < list.Count - 1; i++) {
			if(list[i + 1].X >= x) {
				lastIndex = i;
				return list[i].Y + (x - list[i].X) / (list[i + 1].X - list[i].X) * ((list[i + 1].Y - list[i].Y) / 1);
			}
		}
		return 0;
	}
}