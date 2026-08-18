using System;

namespace System.Data.Objects
{
	// Token: 0x0200012D RID: 301
	public class ObjectMaterializedEventArgs : EventArgs
	{
		// Token: 0x060015F6 RID: 5622 RVA: 0x0004A1E0 File Offset: 0x000483E0
		internal ObjectMaterializedEventArgs(object entity)
		{
			this._entity = entity;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0004A1EF File Offset: 0x000483EF
		public object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x04000A44 RID: 2628
		private readonly object _entity;
	}
}
