using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000319 RID: 793
	public class PaintEventArgs : EventArgs, IDisposable
	{
		// Token: 0x06003260 RID: 12896 RVA: 0x000E258E File Offset: 0x000E078E
		public PaintEventArgs(Graphics graphics, Rectangle clipRect)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			this.graphics = graphics;
			this.clipRect = clipRect;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000E25C8 File Offset: 0x000E07C8
		internal PaintEventArgs(IntPtr dc, Rectangle clipRect)
		{
			this.dc = dc;
			this.clipRect = clipRect;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000E25F4 File Offset: 0x000E07F4
		~PaintEventArgs()
		{
			this.Dispose(false);
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000E2624 File Offset: 0x000E0824
		public Rectangle ClipRectangle
		{
			get
			{
				return this.clipRect;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000E262C File Offset: 0x000E082C
		internal IntPtr HDC
		{
			get
			{
				if (this.graphics == null)
				{
					return this.dc;
				}
				return IntPtr.Zero;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000E2644 File Offset: 0x000E0844
		public Graphics Graphics
		{
			get
			{
				if (this.graphics == null && this.dc != IntPtr.Zero)
				{
					this.oldPal = Control.SetUpPalette(this.dc, false, false);
					this.graphics = Graphics.FromHdcInternal(this.dc);
					this.graphics.PageUnit = GraphicsUnit.Pixel;
					this.savedGraphicsState = this.graphics.Save();
				}
				return this.graphics;
			}
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000E26B2 File Offset: 0x000E08B2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000E26C4 File Offset: 0x000E08C4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.graphics != null && this.dc != IntPtr.Zero)
			{
				this.graphics.Dispose();
			}
			if (this.oldPal != IntPtr.Zero && this.dc != IntPtr.Zero)
			{
				SafeNativeMethods.SelectPalette(new HandleRef(this, this.dc), new HandleRef(this, this.oldPal), 0);
				this.oldPal = IntPtr.Zero;
			}
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000E2747 File Offset: 0x000E0947
		internal void ResetGraphics()
		{
			if (this.graphics != null && this.savedGraphicsState != null)
			{
				this.graphics.Restore(this.savedGraphicsState);
				this.savedGraphicsState = null;
			}
		}

		// Token: 0x04001E78 RID: 7800
		private Graphics graphics;

		// Token: 0x04001E79 RID: 7801
		private GraphicsState savedGraphicsState;

		// Token: 0x04001E7A RID: 7802
		private readonly IntPtr dc = IntPtr.Zero;

		// Token: 0x04001E7B RID: 7803
		private IntPtr oldPal = IntPtr.Zero;

		// Token: 0x04001E7C RID: 7804
		private readonly Rectangle clipRect;
	}
}
