using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001C1 RID: 449
	public class ActiveDesignSurfaceChangedEventArgs : EventArgs
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x0005B7E7 File Offset: 0x000599E7
		public ActiveDesignSurfaceChangedEventArgs(DesignSurface oldSurface, DesignSurface newSurface)
		{
			this._oldSurface = oldSurface;
			this._newSurface = newSurface;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0005B7FD File Offset: 0x000599FD
		public DesignSurface OldSurface
		{
			get
			{
				return this._oldSurface;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0005B805 File Offset: 0x00059A05
		public DesignSurface NewSurface
		{
			get
			{
				return this._newSurface;
			}
		}

		// Token: 0x04000962 RID: 2402
		private DesignSurface _oldSurface;

		// Token: 0x04000963 RID: 2403
		private DesignSurface _newSurface;
	}
}
