using System;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D9A RID: 3482
	public class XmlaFieldDescriptionProvider : OlapFieldDescriptionsProviderBase
	{
		// Token: 0x060081AA RID: 33194 RVA: 0x001D917D File Offset: 0x001D737D
		public XmlaFieldDescriptionProvider(XmlaConnectionSettings connectionSettings)
		{
			this.connectionSettings = connectionSettings;
		}

		// Token: 0x060081AB RID: 33195 RVA: 0x001D918C File Offset: 0x001D738C
		internal override OlapMetadataLoader GetLoader()
		{
			return new XmlaMetadataLoader(this.connectionSettings);
		}

		// Token: 0x040023C4 RID: 9156
		private XmlaConnectionSettings connectionSettings;
	}
}
