using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Parser {
    namespace Analog {
        public class AnalogParser {
            private List<PointF> list;

            public int Count { get { return list.Count; } }
            public float LeftBorder { get { return list[0].X; } }
            public float RightBorder { get { return list[^1].X; } }
            public float BottomBorder { get; private set; }
            public float UpperBorder { get; private set; }

            public AnalogParser(string src) {
                list = new List<PointF>();

                using(StreamReader SR = new(src)) {
                    string[] str = SR.ReadToEnd().Replace('.', ',').Split();
                    foreach(var item in str) {
                        if(item != "") {
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

            public PointF this[int index] {
                get {
                    return list[index];
                }
            }

            public double Interpolate(double x) {
                double lagrangePol = 0;

                for(int i = 0; i < Count; i++) {
                    double basicsPol = 1;
                    for(int j = 0; j < Count; j++) {
                        if(j != i) {
                            basicsPol *= (x - list[j].X) / (list[i].X -list[j].X);
                        }
                    }
                    lagrangePol += basicsPol * list[i].Y;
                }

                return lagrangePol;
            }
        }
    }
}
