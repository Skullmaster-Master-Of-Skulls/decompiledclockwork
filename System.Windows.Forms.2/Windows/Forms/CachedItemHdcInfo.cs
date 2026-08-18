using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020003B1 RID: 945
	internal class CachedItemHdcInfo : IDisposable
	{
		// Token: 0x06003ECE RID: 16078 RVA: 0x0011069A File Offset: 0x0010E89A
		internal CachedItemHdcInfo()
		{
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x001106C4 File Offset: 0x0010E8C4
		~CachedItemHdcInfo()
		{
			this.Dispose();
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x001106F0 File Offset: 0x0010E8F0
		public HandleRef GetCachedItemDC(HandleRef toolStripHDC, Size bitmapSize)
		{
			if (this.cachedHDCSize.Width < bitmapSize.Width || this.cachedHDCSize.Height < bitmapSize.Height)
			{
				if (this.cachedItemHDC.Handle == IntPtr.Zero)
				{
					IntPtr handle = UnsafeNativeMethods.CreateCompatibleDC(toolStripHDC);
					this.cachedItemHDC = new HandleRef(this, handle);
				}
				this.cachedItemBitmap = new HandleRef(this, SafeNativeMethods.CreateCompatibleBitmap(toolStripHDC, bitmapSize.Width, bitmapSize.Height));
				IntPtr intPtr = SafeNativeMethods.SelectObject(this.cachedItemHDC, this.cachedItemBitmap);
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.ExternalDeleteObject(new HandleRef(null, intPtr));
					intPtr = IntPtr.Zero;
				}
				this.cachedHDCSize = bitmapSize;
			}
			return this.cachedItemHDC;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x001107B4 File Offset: 0x0010E9B4
		private void DeleteCachedItemHDC()
		{
			if (this.cachedItemHDC.Handle != IntPtr.Zero)
			{
				if (this.cachedItemBitmap.Handle != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(this.cachedItemBitmap);
					this.cachedItemBitmap = NativeMethods.NullHandleRef;
				}
				UnsafeNativeMethods.DeleteCompatibleDC(this.cachedItemHDC);
			}
			this.cachedItemHDC = NativeMethods.NullHandleRef;
			this.cachedItemBitmap = NativeMethods.NullHandleRef;
			this.cachedHDCSize = Size.Empty;
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x00110833 File Offset: 0x0010EA33
		public void Dispose()
		{
			this.DeleteCachedItemHDC();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04002492 RID: 9362
		private HandleRef cachedItemHDC = NativeMethods.NullHandleRef;

		// Token: 0x04002493 RID: 9363
		private Size cachedHDCSize = Size.Empty;

		// Token: 0x04002494 RID: 9364
		private HandleRef cachedItemBitmap = NativeMethods.NullHandleRef;
	}
}
