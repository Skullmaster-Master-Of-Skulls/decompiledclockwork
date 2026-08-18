using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000054 RID: 84
	public interface IHierarchicalDataSourceDesigner
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002A6 RID: 678
		bool CanConfigure { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002A7 RID: 679
		bool CanRefreshSchema { get; }

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060002A8 RID: 680
		// (remove) Token: 0x060002A9 RID: 681
		event EventHandler DataSourceChanged;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060002AA RID: 682
		// (remove) Token: 0x060002AB RID: 683
		event EventHandler SchemaRefreshed;

		// Token: 0x060002AC RID: 684
		void Configure();

		// Token: 0x060002AD RID: 685
		DesignerHierarchicalDataSourceView GetView(string viewPath);

		// Token: 0x060002AE RID: 686
		void RefreshSchema(bool preferSilent);

		// Token: 0x060002AF RID: 687
		void ResumeDataSourceEvents();

		// Token: 0x060002B0 RID: 688
		void SuppressDataSourceEvents();
	}
}
