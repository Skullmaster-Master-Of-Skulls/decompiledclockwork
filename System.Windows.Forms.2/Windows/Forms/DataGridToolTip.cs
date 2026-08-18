using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200018F RID: 399
	internal class DataGridToolTip : MarshalByRefObject
	{
		// Token: 0x0600186E RID: 6254 RVA: 0x00057E7D File Offset: 0x0005607D
		public DataGridToolTip(DataGrid dataGrid)
		{
			this.dataGrid = dataGrid;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00057E8C File Offset: 0x0005608C
		public void CreateToolTipHandle()
		{
			if (this.tipWindow == null || this.tipWindow.Handle == IntPtr.Zero)
			{
				NativeMethods.INITCOMMONCONTROLSEX initcommoncontrolsex = new NativeMethods.INITCOMMONCONTROLSEX();
				initcommoncontrolsex.dwICC = 8;
				initcommoncontrolsex.dwSize = Marshal.SizeOf(initcommoncontrolsex);
				SafeNativeMethods.InitCommonControlsEx(initcommoncontrolsex);
				CreateParams createParams = new CreateParams();
				createParams.Parent = this.dataGrid.Handle;
				createParams.ClassName = "tooltips_class32";
				createParams.Style = 1;
				this.tipWindow = new NativeWindow();
				this.tipWindow.CreateHandle(createParams);
				UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
				SafeNativeMethods.SetWindowPos(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.HWND_NOTOPMOST, 0, 0, 0, 0, 19);
				UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 1027, 3, 0);
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00057F94 File Offset: 0x00056194
		public void AddToolTip(string toolTipString, IntPtr toolTipId, Rectangle iconBounds)
		{
			if (toolTipString == null)
			{
				throw new ArgumentNullException("toolTipString");
			}
			if (iconBounds.IsEmpty)
			{
				throw new ArgumentNullException("iconBounds", SR.GetString("DataGridToolTipEmptyIcon"));
			}
			NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
			toolinfo_T.cbSize = Marshal.SizeOf(toolinfo_T);
			toolinfo_T.hwnd = this.dataGrid.Handle;
			toolinfo_T.uId = toolTipId;
			toolinfo_T.lpszText = toolTipString;
			toolinfo_T.rect = NativeMethods.RECT.FromXYWH(iconBounds.X, iconBounds.Y, iconBounds.Width, iconBounds.Height);
			toolinfo_T.uFlags = 16;
			UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.TTM_ADDTOOL, 0, toolinfo_T);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00058050 File Offset: 0x00056250
		public void RemoveToolTip(IntPtr toolTipId)
		{
			NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
			toolinfo_T.cbSize = Marshal.SizeOf(toolinfo_T);
			toolinfo_T.hwnd = this.dataGrid.Handle;
			toolinfo_T.uId = toolTipId;
			UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.TTM_DELTOOL, 0, toolinfo_T);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000580AA File Offset: 0x000562AA
		public void Destroy()
		{
			this.tipWindow.DestroyHandle();
			this.tipWindow = null;
		}

		// Token: 0x04000ADE RID: 2782
		private NativeWindow tipWindow;

		// Token: 0x04000ADF RID: 2783
		private DataGrid dataGrid;
	}
}
