﻿using Graphs;

namespace Manager;
public class WindowManager {
	private readonly Dictionary<Graph.Method, WindowForm.Window> Windows;
	private readonly CheckedListBox checkedListBox;
	private Size WorkingArea;
	public readonly int size;
	public bool Reverse { get; set; }

	public WindowManager(Control ctl, CheckedListBox checkedListBox) {
		this.checkedListBox = checkedListBox;
		Windows = new();

		WorkingArea = new(Screen.GetWorkingArea(ctl).Width, Screen.GetWorkingArea(ctl).Height);
		size = Math.Min(WorkingArea.Width / 3, WorkingArea.Height / 2);
		Reverse = false;
	}

	public void Add(Graph.Method method) {
		WindowForm.Window window = new(method);
		window.FormClosing += new(WindowClosing);
		window.SetSize(new(size, size));
		Windows.Add(method, window);
	}

	public void Remove(Graph.Method method) {
		if(Windows.ContainsKey(method)) {
			Windows[method].Close();
			Windows.Remove(method);
		}
	}

	public void Refresh(Graph.ParserFunc func, double a, double b, double upperBorder, double bottomBorder, double delta, Graph.Method? method = null) {
		if(Windows.Count == 0)
			MessageBox.Show("In order to see the result of numerical integration,\nyou must select at least one method");

		if(method == null) {
			foreach(var window in Windows) {
				if(!window.Value.IsShown)
					SetLocation(window.Key);

				window.Value.Show(func, a, b, upperBorder, bottomBorder, delta, Reverse);
			}
		} else {
			SetLocation((Graph.Method)method);
			Windows[(Graph.Method)method].Show(func, a, b, upperBorder, bottomBorder, delta, Reverse);
		}
	}

	private void SetLocation(Graph.Method method) {
		switch(method) {
			case Graph.Method.left_rectangle:
				Windows[method].SetLocation(new Point(0, 0));
				break;
			case Graph.Method.right_rectangle:
				Windows[method].SetLocation(new Point(WorkingArea.Width / 2 - size / 2, 0));
				break;
			case Graph.Method.midpoint_rectangle:
				Windows[method].SetLocation(new Point(WorkingArea.Width - size, 0));
				break;
			case Graph.Method.trapezoid:
				Windows[method].SetLocation(new Point(0, size));
				break;
			case Graph.Method.simpson:
				Windows[method].SetLocation(new Point(WorkingArea.Width - size, size));
				break;
		}
	}

	public void ResetAll() {
		foreach(var window in Windows)
			window.Value.Reset();
	}

	private void WindowClosing(object? sender, FormClosingEventArgs e) {
		Windows.Remove((sender as WindowForm.Window).method);
		checkedListBox.SetItemChecked((int)(sender as WindowForm.Window).method, false);
	}
}
