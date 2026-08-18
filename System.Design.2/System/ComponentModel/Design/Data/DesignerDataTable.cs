using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x02000201 RID: 513
	public abstract class DesignerDataTable : DesignerDataTableBase
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x0006F3B8 File Offset: 0x0006D5B8
		protected DesignerDataTable(string name) : base(name)
		{
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0006F3C1 File Offset: 0x0006D5C1
		protected DesignerDataTable(string name, string owner) : base(name, owner)
		{
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0006F3CB File Offset: 0x0006D5CB
		public ICollection Relationships
		{
			get
			{
				if (this._relationships == null)
				{
					this._relationships = this.CreateRelationships();
				}
				return this._relationships;
			}
		}

		// Token: 0x0600134E RID: 4942
		protected abstract ICollection CreateRelationships();

		// Token: 0x04000A71 RID: 2673
		private ICollection _relationships;
	}
}
