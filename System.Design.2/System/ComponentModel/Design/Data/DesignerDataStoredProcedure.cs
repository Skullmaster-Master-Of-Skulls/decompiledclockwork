using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x02000200 RID: 512
	public abstract class DesignerDataStoredProcedure
	{
		// Token: 0x06001345 RID: 4933 RVA: 0x0006F367 File Offset: 0x0006D567
		protected DesignerDataStoredProcedure(string name)
		{
			this._name = name;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0006F376 File Offset: 0x0006D576
		protected DesignerDataStoredProcedure(string name, string owner)
		{
			this._name = name;
			this._owner = owner;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0006F38C File Offset: 0x0006D58C
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0006F394 File Offset: 0x0006D594
		public string Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0006F39C File Offset: 0x0006D59C
		public ICollection Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = this.CreateParameters();
				}
				return this._parameters;
			}
		}

		// Token: 0x0600134A RID: 4938
		protected abstract ICollection CreateParameters();

		// Token: 0x04000A6E RID: 2670
		private string _name;

		// Token: 0x04000A6F RID: 2671
		private string _owner;

		// Token: 0x04000A70 RID: 2672
		private ICollection _parameters;
	}
}
