using System;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x02000203 RID: 515
	public abstract class DesignerDataView : DesignerDataTableBase
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x0006F3B8 File Offset: 0x0006D5B8
		protected DesignerDataView(string name) : base(name)
		{
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0006F3C1 File Offset: 0x0006D5C1
		protected DesignerDataView(string name, string owner) : base(name, owner)
		{
		}
	}
}
