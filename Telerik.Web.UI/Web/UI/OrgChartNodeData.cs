using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000C16 RID: 3094
	[DataContract]
	[Serializable]
	public class OrgChartNodeData
	{
		// Token: 0x17002658 RID: 9816
		// (get) Token: 0x060075DE RID: 30174 RVA: 0x001B66F5 File Offset: 0x001B48F5
		// (set) Token: 0x060075DF RID: 30175 RVA: 0x001B66FD File Offset: 0x001B48FD
		[DataMember]
		public string Id { get; set; }

		// Token: 0x17002659 RID: 9817
		// (get) Token: 0x060075E0 RID: 30176 RVA: 0x001B6706 File Offset: 0x001B4906
		// (set) Token: 0x060075E1 RID: 30177 RVA: 0x001B670E File Offset: 0x001B490E
		[DataMember]
		public int ColumnCount { get; set; }

		// Token: 0x1700265A RID: 9818
		// (get) Token: 0x060075E2 RID: 30178 RVA: 0x001B6717 File Offset: 0x001B4917
		// (set) Token: 0x060075E3 RID: 30179 RVA: 0x001B671F File Offset: 0x001B491F
		[DataMember]
		public string CssClass { get; set; }

		// Token: 0x1700265B RID: 9819
		// (get) Token: 0x060075E4 RID: 30180 RVA: 0x001B6728 File Offset: 0x001B4928
		// (set) Token: 0x060075E5 RID: 30181 RVA: 0x001B6743 File Offset: 0x001B4943
		[DataMember]
		public List<OrgChartRenderedFieldData> RenderedFieldsData
		{
			get
			{
				if (this._renderedFields == null)
				{
					this._renderedFields = new List<OrgChartRenderedFieldData>();
				}
				return this._renderedFields;
			}
			set
			{
				this._renderedFields = value;
			}
		}

		// Token: 0x04002053 RID: 8275
		private List<OrgChartRenderedFieldData> _renderedFields;
	}
}
