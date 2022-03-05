using Graphs;

namespace Manager {
    public class WindowManager {
        private Dictionary<Graph.Method, WindowForm.Window> Windows;
        private int size;
        Size WorkingArea;

        public WindowManager(Control ctl) {
            Windows = new();
            WorkingArea = new(System.Windows.Forms.Screen.GetWorkingArea(ctl).Width, System.Windows.Forms.Screen.GetWorkingArea(ctl).Height);
            size = Math.Min(WorkingArea.Width / 3, WorkingArea.Height / 2);
        }

        public void Add(Graph.Method method) {
            WindowForm.Window window = new();
            window.Size = new Size(size, size);
            Windows.Add(method, window);
        }

        public void Remove(Graph.Method method) {
            if(Contains(method)) {
                Windows[method].Close();
                Windows.Remove(method);
            }
        }

        public bool Contains(Graph.Method method) {
            return Windows.ContainsKey(method);
        }

        public void Refresh(Graph.ParserFunc func, double a, double b, double delta, Graph.Method? method = null) {
            if(Windows.Count == 0) {
                MessageBox.Show("You must select at least one numerical integration method");
            }
            if(method == null) {
                foreach(var window in Windows) {
                    SetPosition(window.Key);
                    window.Value.Show(func, a, b, delta, window.Key);
                }
            } else {
                SetPosition((Graph.Method)method);
                Windows[(Graph.Method)method].Show(func, a, b, delta, (Graph.Method)method);
            }

        }

        private void SetPosition(Graph.Method method) {
            switch(method) {
                case Graph.Method.left_rectangle:
                    Windows[method].Location = new Point(0, 0);
                    break;
                case Graph.Method.right_rectangle:
                    Windows[method].Location = new Point(WorkingArea.Width / 2 - size / 2, 0);
                    break;
                case Graph.Method.midpoint_rectangle:
                    Windows[method].Location = new Point(WorkingArea.Width - size, 0);
                    break;
                case Graph.Method.trapezoid:
                    Windows[method].Location = new Point(0, size);
                    break;
                case Graph.Method.simpson:
                    Windows[method].Location = new Point(WorkingArea.Width - size, size);
                    break;
            }
        }
    }
}
