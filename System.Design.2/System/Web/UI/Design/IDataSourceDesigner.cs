using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200004B RID: 75
	public interface IDataSourceDesigner
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000283 RID: 643
		bool CanConfigure { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000284 RID: 644
		bool CanRefreshSchema { get; }

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000285 RID: 645
		// (remove) Token: 0x06000286 RID: 646
		event EventHandler DataSourceChanged;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000287 RID: 647
		// (remove) Token: 0x06000288 RID: 648
		event EventHandler SchemaRefreshed;

		// Token: 0x06000289 RID: 649
		void Configure();

		// Token: 0x0600028A RID: 650
		DesignerDataSourceView GetView(string viewName);

		// Token: 0x0600028B RID: 651
		string[] GetViewNames();

		// Token: 0x0600028C RID: 652
		void RefreshSchema(bool preferSilent);

		// Token: 0x0600028D RID: 653
		void ResumeDataSourceEvents();

		// Token: 0x0600028E RID: 654
		void SuppressDataSourceEvents();
	}
}
