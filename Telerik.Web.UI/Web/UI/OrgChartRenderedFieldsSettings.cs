using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000C0C RID: 3084
	public class OrgChartRenderedFieldsSettings
	{
		// Token: 0x17002647 RID: 9799
		// (get) Token: 0x060075AE RID: 30126 RVA: 0x001B61F0 File Offset: 0x001B43F0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartRenderedFieldCollection NodeFields
		{
			get
			{
				if (this._nodeFields == null)
				{
					this._nodeFields = new OrgChartRenderedFieldCollection();
				}
				return this._nodeFields;
			}
		}

		// Token: 0x17002648 RID: 9800
		// (get) Token: 0x060075AF RID: 30127 RVA: 0x001B620B File Offset: 0x001B440B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartRenderedFieldCollection ItemFields
		{
			get
			{
				if (this._itemsFields == null)
				{
					this._itemsFields = new OrgChartRenderedFieldCollection();
				}
				return this._itemsFields;
			}
		}

		// Token: 0x04002044 RID: 8260
		private OrgChartRenderedFieldCollection _nodeFields;

		// Token: 0x04002045 RID: 8261
		private OrgChartRenderedFieldCollection _itemsFields;
	}
}
