using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200004F RID: 79
	public interface IDataSourceViewSchema
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600029C RID: 668
		string Name { get; }

		// Token: 0x0600029D RID: 669
		IDataSourceViewSchema[] GetChildren();

		// Token: 0x0600029E RID: 670
		IDataSourceFieldSchema[] GetFields();
	}
}
