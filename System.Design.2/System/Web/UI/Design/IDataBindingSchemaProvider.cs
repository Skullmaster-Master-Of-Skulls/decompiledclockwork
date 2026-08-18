using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200004A RID: 74
	public interface IDataBindingSchemaProvider
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000280 RID: 640
		bool CanRefreshSchema { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000281 RID: 641
		IDataSourceViewSchema Schema { get; }

		// Token: 0x06000282 RID: 642
		void RefreshSchema(bool preferSilent);
	}
}
