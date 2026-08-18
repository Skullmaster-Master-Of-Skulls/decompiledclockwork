using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000C13 RID: 3091
	[DataContract]
	public class OrgChartGroupItemData
	{
		// Token: 0x1700264E RID: 9806
		// (get) Token: 0x060075C6 RID: 30150 RVA: 0x001B64E7 File Offset: 0x001B46E7
		// (set) Token: 0x060075C7 RID: 30151 RVA: 0x001B64EF File Offset: 0x001B46EF
		[DataMember]
		public string Text { get; set; }

		// Token: 0x1700264F RID: 9807
		// (get) Token: 0x060075C8 RID: 30152 RVA: 0x001B64F8 File Offset: 0x001B46F8
		// (set) Token: 0x060075C9 RID: 30153 RVA: 0x001B6500 File Offset: 0x001B4700
		[DataMember]
		public string Id { get; set; }

		// Token: 0x17002650 RID: 9808
		// (get) Token: 0x060075CA RID: 30154 RVA: 0x001B6509 File Offset: 0x001B4709
		// (set) Token: 0x060075CB RID: 30155 RVA: 0x001B6511 File Offset: 0x001B4711
		[DataMember]
		public string ImageUrl { get; set; }

		// Token: 0x17002651 RID: 9809
		// (get) Token: 0x060075CC RID: 30156 RVA: 0x001B651A File Offset: 0x001B471A
		// (set) Token: 0x060075CD RID: 30157 RVA: 0x001B6522 File Offset: 0x001B4722
		[DataMember]
		public string ImageAltText { get; set; }

		// Token: 0x17002652 RID: 9810
		// (get) Token: 0x060075CE RID: 30158 RVA: 0x001B652B File Offset: 0x001B472B
		// (set) Token: 0x060075CF RID: 30159 RVA: 0x001B6533 File Offset: 0x001B4733
		[DataMember]
		public string CssClass { get; set; }

		// Token: 0x17002653 RID: 9811
		// (get) Token: 0x060075D0 RID: 30160 RVA: 0x001B653C File Offset: 0x001B473C
		// (set) Token: 0x060075D1 RID: 30161 RVA: 0x001B6557 File Offset: 0x001B4757
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

		// Token: 0x0400204A RID: 8266
		private List<OrgChartRenderedFieldData> _renderedFields;
	}
}
