using System;
using System.Drawing;
using System.Internal;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004EC RID: 1260
	internal sealed class WindowsRegion : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06005225 RID: 21029 RVA: 0x0001E607 File Offset: 0x0001C807
		private WindowsRegion()
		{
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x0015546B File Offset: 0x0015366B
		public WindowsRegion(Rectangle rect)
		{
			this.CreateRegion(rect);
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x0015547A File Offset: 0x0015367A
		public WindowsRegion(int x, int y, int width, int height)
		{
			this.CreateRegion(new Rectangle(x, y, width, height));
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x00155494 File Offset: 0x00153694
		public static WindowsRegion FromHregion(IntPtr hRegion, bool takeOwnership)
		{
			WindowsRegion windowsRegion = new WindowsRegion();
			if (hRegion != IntPtr.Zero)
			{
				windowsRegion.nativeHandle = hRegion;
				if (takeOwnership)
				{
					windowsRegion.ownHandle = true;
					System.Internal.HandleCollector.Add(hRegion, IntSafeNativeMethods.CommonHandles.GDI);
				}
			}
			return windowsRegion;
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x001554D2 File Offset: 0x001536D2
		public static WindowsRegion FromRegion(Region region, Graphics g)
		{
			if (region.IsInfinite(g))
			{
				return new WindowsRegion();
			}
			return WindowsRegion.FromHregion(region.GetHrgn(g), true);
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x001554F0 File Offset: 0x001536F0
		public object Clone()
		{
			if (!this.IsInfinite)
			{
				return new WindowsRegion(this.ToRectangle());
			}
			return new WindowsRegion();
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0015550B File Offset: 0x0015370B
		public IntNativeMethods.RegionFlags CombineRegion(WindowsRegion region1, WindowsRegion region2, RegionCombineMode mode)
		{
			return IntUnsafeNativeMethods.CombineRgn(new HandleRef(this, this.HRegion), new HandleRef(region1, region1.HRegion), new HandleRef(region2, region2.HRegion), mode);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x00155537 File Offset: 0x00153737
		private void CreateRegion(Rectangle rect)
		{
			this.nativeHandle = IntSafeNativeMethods.CreateRectRgn(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
			this.ownHandle = true;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00155577 File Offset: 0x00153777
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x00155580 File Offset: 0x00153780
		public void Dispose(bool disposing)
		{
			if (this.nativeHandle != IntPtr.Zero)
			{
				if (this.ownHandle)
				{
					IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, this.nativeHandle));
				}
				this.nativeHandle = IntPtr.Zero;
				if (disposing)
				{
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x001555D0 File Offset: 0x001537D0
		~WindowsRegion()
		{
			this.Dispose(false);
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06005230 RID: 21040 RVA: 0x00155600 File Offset: 0x00153800
		public IntPtr HRegion
		{
			get
			{
				return this.nativeHandle;
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06005231 RID: 21041 RVA: 0x00155608 File Offset: 0x00153808
		public bool IsInfinite
		{
			get
			{
				return this.nativeHandle == IntPtr.Zero;
			}
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x0015561C File Offset: 0x0015381C
		public Rectangle ToRectangle()
		{
			if (this.IsInfinite)
			{
				return new Rectangle(-2147483647, -2147483647, int.MaxValue, int.MaxValue);
			}
			IntNativeMethods.RECT rect = default(IntNativeMethods.RECT);
			IntUnsafeNativeMethods.GetRgnBox(new HandleRef(this, this.nativeHandle), ref rect);
			return new Rectangle(new Point(rect.left, rect.top), rect.Size);
		}

		// Token: 0x0400361A RID: 13850
		private IntPtr nativeHandle;

		// Token: 0x0400361B RID: 13851
		private bool ownHandle;
	}
}
