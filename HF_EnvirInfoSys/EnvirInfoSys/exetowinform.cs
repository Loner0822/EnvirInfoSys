using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class exetowinform
	{
		private const int SWP_NOOWNERZORDER = 512;

		private const int SWP_NOREDRAW = 8;

		private const int SWP_NOZORDER = 4;

		private const int SWP_SHOWWINDOW = 64;

		private const int WS_EX_MDICHILD = 64;

		private const int SWP_FRAMECHANGED = 32;

		private const int SWP_NOACTIVATE = 16;

		private const int SWP_ASYNCWINDOWPOS = 16384;

		private const int SWP_NOMOVE = 2;

		private const int SWP_NOSIZE = 1;

		private const int GWL_STYLE = -16;

		private const int WS_VISIBLE = 268435456;

		private const int WM_CLOSE = 16;

		private const int WS_CHILD = 1073741824;

		private const int SW_HIDE = 0;

		private const int SW_SHOWNORMAL = 1;

		private const int SW_NORMAL = 1;

		private const int SW_SHOWMINIMIZED = 2;

		private const int SW_SHOWMAXIMIZED = 3;

		private const int SW_MAXIMIZE = 3;

		private const int SW_SHOWNOACTIVATE = 4;

		private const int SW_SHOW = 5;

		private const int SW_MINIMIZE = 6;

		private const int SW_SHOWMINNOACTIVE = 7;

		private const int SW_SHOWNA = 8;

		private const int SW_RESTORE = 9;

		private const int SW_SHOWDEFAULT = 10;

		private const int SW_MAX = 10;

		private const int WM_SETTEXT = 12;

		private EventHandler appIdleEvent = null;

		private Control ParentCon = null;

		private string strGUID = "";

		private Process m_AppProcess = null;

		public bool IsStarted => m_AppProcess != null;

		public exetowinform(Control C, string Titlestr)
		{
			appIdleEvent = Application_Idle;
			ParentCon = C;
			strGUID = Titlestr;
		}

		public IntPtr Start(string FileNameStr)
		{
			if (m_AppProcess != null)
			{
				Stop();
			}
			try
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo(FileNameStr);
				processStartInfo.UseShellExecute = true;
				processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;
				m_AppProcess = Process.Start(processStartInfo);
				m_AppProcess.WaitForInputIdle();
				Application.Idle += appIdleEvent;
			}
			catch
			{
				if (m_AppProcess != null)
				{
					if (!m_AppProcess.HasExited)
					{
						m_AppProcess.Kill();
					}
					m_AppProcess = null;
				}
			}
			return m_AppProcess.Handle;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			if (m_AppProcess == null || m_AppProcess.HasExited)
			{
				m_AppProcess = null;
				Application.Idle -= appIdleEvent;
				return;
			}
			while (m_AppProcess.MainWindowHandle == IntPtr.Zero)
			{
				Thread.Sleep(100);
				m_AppProcess.Refresh();
			}
			Application.Idle -= appIdleEvent;
			EmbedProcess(m_AppProcess, ParentCon);
		}

		private void m_AppProcess_Exited(object sender, EventArgs e)
		{
			m_AppProcess = null;
		}

		public void Stop()
		{
			if (m_AppProcess != null)
			{
				try
				{
					if (!m_AppProcess.HasExited)
					{
						m_AppProcess.Kill();
					}
				}
				catch (Exception)
				{
				}
				m_AppProcess = null;
			}
		}

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
		private static extern long GetWindowLong(IntPtr hwnd, int nIndex);

		public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong)
		{
			if (IntPtr.Size == 4)
			{
				return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
			}
			return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
		public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
		public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

		[DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
		private static extern bool PostMessage(IntPtr hwnd, uint Msg, uint wParam, uint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetParent(IntPtr hwnd);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		private void EmbedProcess(Process app, Control control)
		{
			if (app != null && !(app.MainWindowHandle == IntPtr.Zero) && control != null)
			{
				try
				{
					SetParent(app.MainWindowHandle, control.Handle);
				}
				catch (Exception)
				{
				}
				try
				{
					SetWindowLong(new HandleRef(this, app.MainWindowHandle), -16, 268435456);
					SendMessage(app.MainWindowHandle, 12, IntPtr.Zero, strGUID);
				}
				catch (Exception)
				{
				}
				try
				{
					MoveWindow(app.MainWindowHandle, 0, 0, control.Width, control.Height, repaint: true);
				}
				catch (Exception)
				{
				}
			}
		}

		[DllImport("User32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
	}
}
