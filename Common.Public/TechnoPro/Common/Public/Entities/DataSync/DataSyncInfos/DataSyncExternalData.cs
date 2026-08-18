using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos
{
	// Token: 0x020003E3 RID: 995
	public class DataSyncExternalData
	{
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00022125 File Offset: 0x00020325
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0002212D File Offset: 0x0002032D
		public string FieldName { get; set; }

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x00022136 File Offset: 0x00020336
		// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0002213E File Offset: 0x0002033E
		public string FieldValue { get; set; }

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x00022147 File Offset: 0x00020347
		// (set) Token: 0x06001EAD RID: 7853 RVA: 0x0002214F File Offset: 0x0002034F
		public string Student_no { get; set; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x00022158 File Offset: 0x00020358
		// (set) Token: 0x06001EAF RID: 7855 RVA: 0x00022160 File Offset: 0x00020360
		public int ClockWorkPersonId { get; set; }

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x00022169 File Offset: 0x00020369
		// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x00022171 File Offset: 0x00020371
		public DynamicData MatchingClockWorkData { get; set; }

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0002217A File Offset: 0x0002037A
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x00022182 File Offset: 0x00020382
		public DataSyncInfoMapItem MapItem { get; set; }
	}
}
