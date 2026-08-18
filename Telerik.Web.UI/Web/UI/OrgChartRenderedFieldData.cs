using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000C1A RID: 3098
	[DataContract]
	public class OrgChartRenderedFieldData
	{
		// Token: 0x17002662 RID: 9826
		// (get) Token: 0x060075F6 RID: 30198 RVA: 0x001B680B File Offset: 0x001B4A0B
		// (set) Token: 0x060075F7 RID: 30199 RVA: 0x001B6813 File Offset: 0x001B4A13
		[DataMember]
		public string Text { get; set; }

		// Token: 0x17002663 RID: 9827
		// (get) Token: 0x060075F8 RID: 30200 RVA: 0x001B681C File Offset: 0x001B4A1C
		// (set) Token: 0x060075F9 RID: 30201 RVA: 0x001B6824 File Offset: 0x001B4A24
		[DataMember]
		public string Label { get; set; }
	}
}
