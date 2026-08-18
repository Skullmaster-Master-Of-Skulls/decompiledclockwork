using System;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200162A RID: 5674
	public class GdiDeviceContent : IDisposable
	{
		// Token: 0x0600DC9E RID: 56478 RVA: 0x00303681 File Offset: 0x00301881
		public GdiDeviceContent()
		{
			this.hDC = NativeMethods.GetDC(IntPtr.Zero);
		}

		// Token: 0x0600DC9F RID: 56479 RVA: 0x0030369C File Offset: 0x0030189C
		~GdiDeviceContent()
		{
			this.Dispose(false);
		}

		// Token: 0x0600DCA0 RID: 56480 RVA: 0x003036CC File Offset: 0x003018CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600DCA1 RID: 56481 RVA: 0x003036DB File Offset: 0x003018DB
		protected virtual void Dispose(bool disposing)
		{
			if (this.hDC != IntPtr.Zero)
			{
				NativeMethods.DeleteDC(this.hDC);
				this.hDC = IntPtr.Zero;
			}
		}

		// Token: 0x0600DCA2 RID: 56482 RVA: 0x00303706 File Offset: 0x00301906
		public IntPtr SelectFont(GdiFont font)
		{
			return NativeMethods.SelectObject(this.hDC, font.Handle);
		}

		// Token: 0x0600DCA3 RID: 56483 RVA: 0x00303719 File Offset: 0x00301919
		public IntPtr GetCurrentObject(GdiDcObject objectType)
		{
			return NativeMethods.GetCurrentObject(this.hDC, objectType);
		}

		// Token: 0x1700438E RID: 17294
		// (get) Token: 0x0600DCA4 RID: 56484 RVA: 0x00303727 File Offset: 0x00301927
		internal IntPtr Handle
		{
			get
			{
				return this.hDC;
			}
		}

		// Token: 0x04003E3F RID: 15935
		private IntPtr hDC;
	}
}
