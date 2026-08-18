using System;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D58 RID: 3416
	public class AdomdFieldDescriptionProvider : OlapFieldDescriptionsProviderBase
	{
		// Token: 0x06007F7D RID: 32637 RVA: 0x001D1F19 File Offset: 0x001D0119
		public AdomdFieldDescriptionProvider(AdomdConnectionSettings connectionSettings)
		{
			this.connectionSettings = connectionSettings;
		}

		// Token: 0x06007F7E RID: 32638 RVA: 0x001D1F28 File Offset: 0x001D0128
		internal override OlapMetadataLoader GetLoader()
		{
			return new AdomdMetadataLoader(this.connectionSettings);
		}

		// Token: 0x04002318 RID: 8984
		private AdomdConnectionSettings connectionSettings;
	}
}
