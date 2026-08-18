using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010D5 RID: 4309
	public class GridColumnsReorderEventArgs : EventArgs
	{
		// Token: 0x0600B0B1 RID: 45233 RVA: 0x00263574 File Offset: 0x00261774
		public GridColumnsReorderEventArgs(GridColumn source, GridColumn target)
		{
			this._source = source;
			this._target = target;
		}

		// Token: 0x17003940 RID: 14656
		// (get) Token: 0x0600B0B2 RID: 45234 RVA: 0x0026358A File Offset: 0x0026178A
		// (set) Token: 0x0600B0B3 RID: 45235 RVA: 0x00263592 File Offset: 0x00261792
		public bool Canceled
		{
			get
			{
				return this._canceled;
			}
			set
			{
				this._canceled = value;
			}
		}

		// Token: 0x17003941 RID: 14657
		// (get) Token: 0x0600B0B4 RID: 45236 RVA: 0x0026359B File Offset: 0x0026179B
		public GridColumn Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17003942 RID: 14658
		// (get) Token: 0x0600B0B5 RID: 45237 RVA: 0x002635A3 File Offset: 0x002617A3
		public GridColumn Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x04002E67 RID: 11879
		private bool _canceled;

		// Token: 0x04002E68 RID: 11880
		private GridColumn _source;

		// Token: 0x04002E69 RID: 11881
		private GridColumn _target;
	}
}
