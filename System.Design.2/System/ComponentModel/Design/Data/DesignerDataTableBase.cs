using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x02000202 RID: 514
	public abstract class DesignerDataTableBase
	{
		// Token: 0x0600134F RID: 4943 RVA: 0x0006F3E7 File Offset: 0x0006D5E7
		protected DesignerDataTableBase(string name)
		{
			this._name = name;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0006F3F6 File Offset: 0x0006D5F6
		protected DesignerDataTableBase(string name, string owner)
		{
			this._name = name;
			this._owner = owner;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0006F40C File Offset: 0x0006D60C
		public ICollection Columns
		{
			get
			{
				if (this._columns == null)
				{
					this._columns = this.CreateColumns();
				}
				return this._columns;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0006F428 File Offset: 0x0006D628
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0006F430 File Offset: 0x0006D630
		public string Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06001354 RID: 4948
		protected abstract ICollection CreateColumns();

		// Token: 0x04000A72 RID: 2674
		private ICollection _columns;

		// Token: 0x04000A73 RID: 2675
		private string _name;

		// Token: 0x04000A74 RID: 2676
		private string _owner;
	}
}
