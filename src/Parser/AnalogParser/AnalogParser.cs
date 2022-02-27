using System;
using System.Drawing;
using System.IO;

namespace Parser {
    namespace Analog {
        public class AnalogParser {

            public class Node {
                public PointF point { get; private set; }

                public Node next { get; set; }

                public Node(PointF point) {
                    this.point = point;
                }
            }

            private Node First = null, Last = null;
            public int Count { get; private set; }

            public float LeftBorder { get { return First.point.X; } }
            public float RightBorder { get { return Last.point.X; } }
            public float BottomBorder { get; private set; }
            public float UpperBorder { get; private set; }

            public AnalogParser(string src) {
                using(StreamReader SR = new(src)) {
                    string[] str = SR.ReadToEnd().Replace('.', ',').Split();
                    foreach(var item in str) {
                        if(item != "") {
                            string[] parts = item.Split(';');
                            Add(new(float.Parse(parts[0]), float.Parse(parts[1])));
                        }
                    }
                }
            }

            public AnalogParser(AnalogParser cloneable) {
                for(int i = 0; i < cloneable.Count; i++) {
                    Add(cloneable[i].point);
                }
            }

            private void Add(PointF point) {
                if(First == null)
                    First = Last = new(point);
                else {
                    if(Last.point.X > point.X)
                        throw new Exception("X coordinate must always increase");

                    Last = Last.next = new Node(point);
                }

                if(BottomBorder >= point.Y)
                    BottomBorder = point.Y;
                else if(UpperBorder <= point.Y)
                    UpperBorder = point.Y;

                Count++;
            }

            public void Interpolation() {
                for(int i = 0; i < Count - 1; i += 2) {
                    Node tmp = this[i].next;
                    this[i].next = new(new((this[i].point.X + tmp.point.X) / 2,
                                           (this[i].point.Y + tmp.point.Y) / 2));
                    this[i].next.next = tmp;

                    Count++;
                }
            }

            public Node this[int index] {
                get {
                    if(index < 0 || index >= Count)
                        throw new IndexOutOfRangeException();

                    Node tmp = First;
                    for(int i = 0; i < index; i++) {
                        tmp = tmp.next;
                    }
                    return tmp;
                }
            }

            override public string ToString() {
                string str = $"Count: {Count}\n";
                for(int i = 0; i < Count; i++)
                    str = this[i].point + "\n" + str;

                return str.Trim();
            }
        }
    }
}
