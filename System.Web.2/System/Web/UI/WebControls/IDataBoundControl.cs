using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200043D RID: 1085
	public interface IDataBoundControl
	{
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003486 RID: 13446
		// (set) Token: 0x06003487 RID: 13447
		string DataSourceID { get; set; }

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003488 RID: 13448
		IDataSource DataSourceObject { get; }

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003489 RID: 13449
		// (set) Token: 0x0600348A RID: 13450
		object DataSource { get; set; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x0600348B RID: 13451
		// (set) Token: 0x0600348C RID: 13452
		string[] DataKeyNames { get; set; }

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x0600348D RID: 13453
		// (set) Token: 0x0600348E RID: 13454
		string DataMember { get; set; }
	}
}
