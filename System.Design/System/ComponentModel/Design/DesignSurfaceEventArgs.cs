using System;

namespace System.ComponentModel.Design
{
	// Token: 0x0200055C RID: 1372
	public class DesignSurfaceEventArgs : EventArgs
	{
		// Token: 0x0600307D RID: 12413 RVA: 0x00112FD4 File Offset: 0x00111FD4
		public DesignSurfaceEventArgs(DesignSurface surface)
		{
			if (surface == null)
			{
				throw new ArgumentNullException("surface");
			}
			this._surface = surface;
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x00112FF1 File Offset: 0x00111FF1
		public DesignSurface Surface
		{
			get
			{
				return this._surface;
			}
		}

		// Token: 0x040020A2 RID: 8354
		private DesignSurface _surface;
	}
}
