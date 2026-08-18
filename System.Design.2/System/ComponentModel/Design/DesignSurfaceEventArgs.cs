using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001C8 RID: 456
	public class DesignSurfaceEventArgs : EventArgs
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x0005E702 File Offset: 0x0005C902
		public DesignSurfaceEventArgs(DesignSurface surface)
		{
			if (surface == null)
			{
				throw new ArgumentNullException("surface");
			}
			this._surface = surface;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0005E71F File Offset: 0x0005C91F
		public DesignSurface Surface
		{
			get
			{
				return this._surface;
			}
		}

		// Token: 0x040009A3 RID: 2467
		private DesignSurface _surface;
	}
}
