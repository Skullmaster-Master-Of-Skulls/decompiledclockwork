using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004D5 RID: 1237
	internal sealed class DeviceContext : MarshalByRefObject, IDeviceContext, IDisposable
	{
		// Token: 0x14000416 RID: 1046
		// (add) Token: 0x06005107 RID: 20743 RVA: 0x0015263C File Offset: 0x0015083C
		// (remove) Token: 0x06005108 RID: 20744 RVA: 0x00152674 File Offset: 0x00150874
		public event EventHandler Disposing;

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06005109 RID: 20745 RVA: 0x001526A9 File Offset: 0x001508A9
		public DeviceContextType DeviceContextType
		{
			get
			{
				return this.dcType;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x0600510A RID: 20746 RVA: 0x001526B1 File Offset: 0x001508B1
		public IntPtr Hdc
		{
			get
			{
				if (this.hDC == IntPtr.Zero && this.dcType == DeviceContextType.Display)
				{
					this.hDC = ((IDeviceContext)this).GetHdc();
					this.CacheInitialState();
				}
				return this.hDC;
			}
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x001526E8 File Offset: 0x001508E8
		private void CacheInitialState()
		{
			this.hCurrentPen = (this.hInitialPen = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 1));
			this.hCurrentBrush = (this.hInitialBrush = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 2));
			this.hCurrentBmp = (this.hInitialBmp = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 7));
			this.hCurrentFont = (this.hInitialFont = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 6));
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x0015277C File Offset: 0x0015097C
		public void DeleteObject(IntPtr handle, GdiObjectType type)
		{
			IntPtr handle2 = IntPtr.Zero;
			if (type != GdiObjectType.Pen)
			{
				if (type != GdiObjectType.Brush)
				{
					if (type == GdiObjectType.Bitmap)
					{
						if (handle == this.hCurrentBmp)
						{
							IntPtr intPtr = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialBmp));
							this.hCurrentBmp = IntPtr.Zero;
						}
						handle2 = handle;
					}
				}
				else
				{
					if (handle == this.hCurrentBrush)
					{
						IntPtr intPtr2 = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialBrush));
						this.hCurrentBrush = IntPtr.Zero;
					}
					handle2 = handle;
				}
			}
			else
			{
				if (handle == this.hCurrentPen)
				{
					IntPtr intPtr3 = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialPen));
					this.hCurrentPen = IntPtr.Zero;
				}
				handle2 = handle;
			}
			IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, handle2));
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0015285C File Offset: 0x00150A5C
		private DeviceContext(IntPtr hWnd)
		{
			this.hWnd = hWnd;
			this.dcType = DeviceContextType.Display;
			DeviceContexts.AddDeviceContext(this);
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x00152884 File Offset: 0x00150A84
		private DeviceContext(IntPtr hDC, DeviceContextType dcType)
		{
			this.hDC = hDC;
			this.dcType = dcType;
			this.CacheInitialState();
			DeviceContexts.AddDeviceContext(this);
			if (dcType == DeviceContextType.Display)
			{
				this.hWnd = IntUnsafeNativeMethods.WindowFromDC(new HandleRef(this, this.hDC));
			}
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x001528D8 File Offset: 0x00150AD8
		public static DeviceContext CreateDC(string driverName, string deviceName, string fileName, HandleRef devMode)
		{
			IntPtr intPtr = IntUnsafeNativeMethods.CreateDC(driverName, deviceName, fileName, devMode);
			return new DeviceContext(intPtr, DeviceContextType.NamedDevice);
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x001528F8 File Offset: 0x00150AF8
		public static DeviceContext CreateIC(string driverName, string deviceName, string fileName, HandleRef devMode)
		{
			IntPtr intPtr = IntUnsafeNativeMethods.CreateIC(driverName, deviceName, fileName, devMode);
			return new DeviceContext(intPtr, DeviceContextType.Information);
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x00152918 File Offset: 0x00150B18
		public static DeviceContext FromCompatibleDC(IntPtr hdc)
		{
			IntPtr intPtr = IntUnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, hdc));
			return new DeviceContext(intPtr, DeviceContextType.Memory);
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x00152939 File Offset: 0x00150B39
		public static DeviceContext FromHdc(IntPtr hdc)
		{
			return new DeviceContext(hdc, DeviceContextType.Unknown);
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00152942 File Offset: 0x00150B42
		public static DeviceContext FromHwnd(IntPtr hwnd)
		{
			return new DeviceContext(hwnd);
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x0015294C File Offset: 0x00150B4C
		~DeviceContext()
		{
			this.Dispose(false);
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x0015297C File Offset: 0x00150B7C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x0015298C File Offset: 0x00150B8C
		internal void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (this.Disposing != null)
			{
				this.Disposing(this, EventArgs.Empty);
			}
			this.disposed = true;
			this.DisposeFont(disposing);
			switch (this.dcType)
			{
			case DeviceContextType.Unknown:
			case DeviceContextType.NCWindow:
				return;
			case DeviceContextType.Display:
				((IDeviceContext)this).ReleaseHdc();
				return;
			case DeviceContextType.NamedDevice:
			case DeviceContextType.Information:
				IntUnsafeNativeMethods.DeleteHDC(new HandleRef(this, this.hDC));
				this.hDC = IntPtr.Zero;
				return;
			case DeviceContextType.Memory:
				IntUnsafeNativeMethods.DeleteDC(new HandleRef(this, this.hDC));
				this.hDC = IntPtr.Zero;
				return;
			default:
				return;
			}
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x00152A32 File Offset: 0x00150C32
		IntPtr IDeviceContext.GetHdc()
		{
			if (this.hDC == IntPtr.Zero)
			{
				this.hDC = IntUnsafeNativeMethods.GetDC(new HandleRef(this, this.hWnd));
			}
			return this.hDC;
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x00152A64 File Offset: 0x00150C64
		void IDeviceContext.ReleaseHdc()
		{
			if (this.hDC != IntPtr.Zero && this.dcType == DeviceContextType.Display)
			{
				IntUnsafeNativeMethods.ReleaseDC(new HandleRef(this, this.hWnd), new HandleRef(this, this.hDC));
				this.hDC = IntPtr.Zero;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x00152AB5 File Offset: 0x00150CB5
		public DeviceContextGraphicsMode GraphicsMode
		{
			get
			{
				return (DeviceContextGraphicsMode)IntUnsafeNativeMethods.GetGraphicsMode(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x00152AC8 File Offset: 0x00150CC8
		public DeviceContextGraphicsMode SetGraphicsMode(DeviceContextGraphicsMode newMode)
		{
			return (DeviceContextGraphicsMode)IntUnsafeNativeMethods.SetGraphicsMode(new HandleRef(this, this.Hdc), (int)newMode);
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00152ADC File Offset: 0x00150CDC
		public void RestoreHdc()
		{
			IntUnsafeNativeMethods.RestoreDC(new HandleRef(this, this.hDC), -1);
			if (this.contextStack != null)
			{
				DeviceContext.GraphicsState graphicsState = (DeviceContext.GraphicsState)this.contextStack.Pop();
				this.hCurrentBmp = graphicsState.hBitmap;
				this.hCurrentBrush = graphicsState.hBrush;
				this.hCurrentPen = graphicsState.hPen;
				this.hCurrentFont = graphicsState.hFont;
				if (graphicsState.font != null && graphicsState.font.IsAlive)
				{
					this.selectedFont = (graphicsState.font.Target as WindowsFont);
				}
				else
				{
					WindowsFont windowsFont = this.selectedFont;
					this.selectedFont = null;
					if (windowsFont != null && MeasurementDCInfo.IsMeasurementDC(this))
					{
						windowsFont.Dispose();
					}
				}
			}
			MeasurementDCInfo.ResetIfIsMeasurementDC(this.hDC);
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x00152BA0 File Offset: 0x00150DA0
		public int SaveHdc()
		{
			HandleRef handleRef = new HandleRef(this, this.Hdc);
			int result = IntUnsafeNativeMethods.SaveDC(handleRef);
			if (this.contextStack == null)
			{
				this.contextStack = new Stack();
			}
			DeviceContext.GraphicsState graphicsState = new DeviceContext.GraphicsState();
			graphicsState.hBitmap = this.hCurrentBmp;
			graphicsState.hBrush = this.hCurrentBrush;
			graphicsState.hPen = this.hCurrentPen;
			graphicsState.hFont = this.hCurrentFont;
			graphicsState.font = new WeakReference(this.selectedFont);
			this.contextStack.Push(graphicsState);
			return result;
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x00152C2C File Offset: 0x00150E2C
		public void SetClip(WindowsRegion region)
		{
			HandleRef handleRef = new HandleRef(this, this.Hdc);
			HandleRef hRgn = new HandleRef(region, region.HRegion);
			IntUnsafeNativeMethods.SelectClipRgn(handleRef, hRgn);
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x00152C60 File Offset: 0x00150E60
		public void IntersectClip(WindowsRegion wr)
		{
			if (wr.HRegion == IntPtr.Zero)
			{
				return;
			}
			WindowsRegion windowsRegion = new WindowsRegion(0, 0, 0, 0);
			try
			{
				int clipRgn = IntUnsafeNativeMethods.GetClipRgn(new HandleRef(this, this.Hdc), new HandleRef(windowsRegion, windowsRegion.HRegion));
				if (clipRgn == 1)
				{
					wr.CombineRegion(windowsRegion, wr, RegionCombineMode.AND);
				}
				this.SetClip(wr);
			}
			finally
			{
				windowsRegion.Dispose();
			}
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x00152CD8 File Offset: 0x00150ED8
		public void TranslateTransform(int dx, int dy)
		{
			IntNativeMethods.POINT point = new IntNativeMethods.POINT();
			IntUnsafeNativeMethods.OffsetViewportOrgEx(new HandleRef(this, this.Hdc), dx, dy, point);
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x00152D00 File Offset: 0x00150F00
		public override bool Equals(object obj)
		{
			DeviceContext deviceContext = obj as DeviceContext;
			return deviceContext == this || (deviceContext != null && deviceContext.Hdc == this.Hdc);
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x00152D30 File Offset: 0x00150F30
		public override int GetHashCode()
		{
			return this.Hdc.GetHashCode();
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06005122 RID: 20770 RVA: 0x00152D4B File Offset: 0x00150F4B
		public WindowsFont ActiveFont
		{
			get
			{
				return this.selectedFont;
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06005123 RID: 20771 RVA: 0x00152D53 File Offset: 0x00150F53
		public Color BackgroundColor
		{
			get
			{
				return ColorTranslator.FromWin32(IntUnsafeNativeMethods.GetBkColor(new HandleRef(this, this.Hdc)));
			}
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x00152D6B File Offset: 0x00150F6B
		public Color SetBackgroundColor(Color newColor)
		{
			return ColorTranslator.FromWin32(IntUnsafeNativeMethods.SetBkColor(new HandleRef(this, this.Hdc), ColorTranslator.ToWin32(newColor)));
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06005125 RID: 20773 RVA: 0x00152D89 File Offset: 0x00150F89
		public DeviceContextBackgroundMode BackgroundMode
		{
			get
			{
				return (DeviceContextBackgroundMode)IntUnsafeNativeMethods.GetBkMode(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x00152D9C File Offset: 0x00150F9C
		public DeviceContextBackgroundMode SetBackgroundMode(DeviceContextBackgroundMode newMode)
		{
			return (DeviceContextBackgroundMode)IntUnsafeNativeMethods.SetBkMode(new HandleRef(this, this.Hdc), (int)newMode);
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06005127 RID: 20775 RVA: 0x00152DB0 File Offset: 0x00150FB0
		public DeviceContextBinaryRasterOperationFlags BinaryRasterOperation
		{
			get
			{
				return (DeviceContextBinaryRasterOperationFlags)IntUnsafeNativeMethods.GetROP2(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x00152DC3 File Offset: 0x00150FC3
		public DeviceContextBinaryRasterOperationFlags SetRasterOperation(DeviceContextBinaryRasterOperationFlags rasterOperation)
		{
			return (DeviceContextBinaryRasterOperationFlags)IntUnsafeNativeMethods.SetROP2(new HandleRef(this, this.Hdc), (int)rasterOperation);
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x00152DD7 File Offset: 0x00150FD7
		public Size Dpi
		{
			get
			{
				return new Size(this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsX), this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsY));
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x0600512A RID: 20778 RVA: 0x00152DEE File Offset: 0x00150FEE
		public int DpiX
		{
			get
			{
				return this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsX);
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x00152DF8 File Offset: 0x00150FF8
		public int DpiY
		{
			get
			{
				return this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsY);
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x00152E04 File Offset: 0x00151004
		public WindowsFont Font
		{
			get
			{
				if (MeasurementDCInfo.IsMeasurementDC(this))
				{
					WindowsFont lastUsedFont = MeasurementDCInfo.LastUsedFont;
					if (lastUsedFont != null && lastUsedFont.Hfont != IntPtr.Zero)
					{
						return lastUsedFont;
					}
				}
				return WindowsFont.FromHdc(this.Hdc);
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x0600512D RID: 20781 RVA: 0x00152E41 File Offset: 0x00151041
		public static DeviceContext ScreenDC
		{
			get
			{
				return DeviceContext.FromHwnd(IntPtr.Zero);
			}
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x00152E50 File Offset: 0x00151050
		internal void DisposeFont(bool disposing)
		{
			if (disposing)
			{
				DeviceContexts.RemoveDeviceContext(this);
			}
			if (this.selectedFont != null && this.selectedFont.Hfont != IntPtr.Zero)
			{
				IntPtr currentObject = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 6);
				if (currentObject == this.selectedFont.Hfont)
				{
					IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, this.hInitialFont));
					currentObject = this.hInitialFont;
				}
				this.selectedFont.Dispose(disposing);
				this.selectedFont = null;
			}
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x00152EE4 File Offset: 0x001510E4
		public IntPtr SelectFont(WindowsFont font)
		{
			if (font.Equals(this.Font))
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = this.SelectObject(font.Hfont, GdiObjectType.Font);
			WindowsFont windowsFont = this.selectedFont;
			this.selectedFont = font;
			this.hCurrentFont = font.Hfont;
			if (windowsFont != null && MeasurementDCInfo.IsMeasurementDC(this))
			{
				windowsFont.Dispose();
			}
			if (MeasurementDCInfo.IsMeasurementDC(this))
			{
				if (intPtr != IntPtr.Zero)
				{
					MeasurementDCInfo.LastUsedFont = font;
				}
				else
				{
					MeasurementDCInfo.Reset();
				}
			}
			return intPtr;
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x00152F61 File Offset: 0x00151161
		public void ResetFont()
		{
			MeasurementDCInfo.ResetIfIsMeasurementDC(this.Hdc);
			IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, this.hInitialFont));
			this.selectedFont = null;
			this.hCurrentFont = this.hInitialFont;
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x00152F9F File Offset: 0x0015119F
		public int GetDeviceCapabilities(DeviceCapabilities capabilityIndex)
		{
			return IntUnsafeNativeMethods.GetDeviceCaps(new HandleRef(this, this.Hdc), (int)capabilityIndex);
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06005132 RID: 20786 RVA: 0x00152FB3 File Offset: 0x001511B3
		public DeviceContextMapMode MapMode
		{
			get
			{
				return (DeviceContextMapMode)IntUnsafeNativeMethods.GetMapMode(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x00152FC8 File Offset: 0x001511C8
		public bool IsFontOnContextStack(WindowsFont wf)
		{
			if (this.contextStack == null)
			{
				return false;
			}
			foreach (object obj in this.contextStack)
			{
				DeviceContext.GraphicsState graphicsState = (DeviceContext.GraphicsState)obj;
				if (graphicsState.hFont == wf.Hfont)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x00153040 File Offset: 0x00151240
		public DeviceContextMapMode SetMapMode(DeviceContextMapMode newMode)
		{
			return (DeviceContextMapMode)IntUnsafeNativeMethods.SetMapMode(new HandleRef(this, this.Hdc), (int)newMode);
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x00153054 File Offset: 0x00151254
		public IntPtr SelectObject(IntPtr hObj, GdiObjectType type)
		{
			if (type != GdiObjectType.Pen)
			{
				if (type != GdiObjectType.Brush)
				{
					if (type == GdiObjectType.Bitmap)
					{
						this.hCurrentBmp = hObj;
					}
				}
				else
				{
					this.hCurrentBrush = hObj;
				}
			}
			else
			{
				this.hCurrentPen = hObj;
			}
			return IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, hObj));
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06005136 RID: 20790 RVA: 0x001530A0 File Offset: 0x001512A0
		public DeviceContextTextAlignment TextAlignment
		{
			get
			{
				return (DeviceContextTextAlignment)IntUnsafeNativeMethods.GetTextAlign(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x001530B3 File Offset: 0x001512B3
		public DeviceContextTextAlignment SetTextAlignment(DeviceContextTextAlignment newAligment)
		{
			return (DeviceContextTextAlignment)IntUnsafeNativeMethods.SetTextAlign(new HandleRef(this, this.Hdc), (int)newAligment);
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06005138 RID: 20792 RVA: 0x001530C7 File Offset: 0x001512C7
		public Color TextColor
		{
			get
			{
				return ColorTranslator.FromWin32(IntUnsafeNativeMethods.GetTextColor(new HandleRef(this, this.Hdc)));
			}
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x001530DF File Offset: 0x001512DF
		public Color SetTextColor(Color newColor)
		{
			return ColorTranslator.FromWin32(IntUnsafeNativeMethods.SetTextColor(new HandleRef(this, this.Hdc), ColorTranslator.ToWin32(newColor)));
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x0600513A RID: 20794 RVA: 0x00153100 File Offset: 0x00151300
		// (set) Token: 0x0600513B RID: 20795 RVA: 0x0015312C File Offset: 0x0015132C
		public Size ViewportExtent
		{
			get
			{
				IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
				IntUnsafeNativeMethods.GetViewportExtEx(new HandleRef(this, this.Hdc), size);
				return size.ToSize();
			}
			set
			{
				this.SetViewportExtent(value);
			}
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x00153138 File Offset: 0x00151338
		public Size SetViewportExtent(Size newExtent)
		{
			IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
			IntUnsafeNativeMethods.SetViewportExtEx(new HandleRef(this, this.Hdc), newExtent.Width, newExtent.Height, size);
			return size.ToSize();
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x0600513D RID: 20797 RVA: 0x00153174 File Offset: 0x00151374
		// (set) Token: 0x0600513E RID: 20798 RVA: 0x001531A0 File Offset: 0x001513A0
		public Point ViewportOrigin
		{
			get
			{
				IntNativeMethods.POINT point = new IntNativeMethods.POINT();
				IntUnsafeNativeMethods.GetViewportOrgEx(new HandleRef(this, this.Hdc), point);
				return point.ToPoint();
			}
			set
			{
				this.SetViewportOrigin(value);
			}
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x001531AC File Offset: 0x001513AC
		public Point SetViewportOrigin(Point newOrigin)
		{
			IntNativeMethods.POINT point = new IntNativeMethods.POINT();
			IntUnsafeNativeMethods.SetViewportOrgEx(new HandleRef(this, this.Hdc), newOrigin.X, newOrigin.Y, point);
			return point.ToPoint();
		}

		// Token: 0x0400350D RID: 13581
		private IntPtr hDC;

		// Token: 0x0400350E RID: 13582
		private DeviceContextType dcType;

		// Token: 0x04003510 RID: 13584
		private bool disposed;

		// Token: 0x04003511 RID: 13585
		private IntPtr hWnd = (IntPtr)(-1);

		// Token: 0x04003512 RID: 13586
		private IntPtr hInitialPen;

		// Token: 0x04003513 RID: 13587
		private IntPtr hInitialBrush;

		// Token: 0x04003514 RID: 13588
		private IntPtr hInitialBmp;

		// Token: 0x04003515 RID: 13589
		private IntPtr hInitialFont;

		// Token: 0x04003516 RID: 13590
		private IntPtr hCurrentPen;

		// Token: 0x04003517 RID: 13591
		private IntPtr hCurrentBrush;

		// Token: 0x04003518 RID: 13592
		private IntPtr hCurrentBmp;

		// Token: 0x04003519 RID: 13593
		private IntPtr hCurrentFont;

		// Token: 0x0400351A RID: 13594
		private Stack contextStack;

		// Token: 0x0400351B RID: 13595
		private WindowsFont selectedFont;

		// Token: 0x02000874 RID: 2164
		internal class GraphicsState
		{
			// Token: 0x04004428 RID: 17448
			internal IntPtr hBrush;

			// Token: 0x04004429 RID: 17449
			internal IntPtr hFont;

			// Token: 0x0400442A RID: 17450
			internal IntPtr hPen;

			// Token: 0x0400442B RID: 17451
			internal IntPtr hBitmap;

			// Token: 0x0400442C RID: 17452
			internal WeakReference font;
		}
	}
}
