using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005A2 RID: 1442
	public class ObjectMaterializedEventArgs : EventArgs
	{
		// Token: 0x06003913 RID: 14611 RVA: 0x0010FEDE File Offset: 0x0010E0DE
		internal ObjectMaterializedEventArgs(object entity)
		{
			this._entity = entity;
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x0010FEED File Offset: 0x0010E0ED
		public object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x040015D7 RID: 5591
		private readonly object _entity;
	}
}
