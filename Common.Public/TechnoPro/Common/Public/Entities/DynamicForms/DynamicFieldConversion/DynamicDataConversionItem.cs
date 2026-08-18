using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicFieldConversion
{
	// Token: 0x0200037E RID: 894
	public class DynamicDataConversionItem
	{
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0001F709 File Offset: 0x0001D909
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x0001F711 File Offset: 0x0001D911
		public int ControlId { get; set; }

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0001F71A File Offset: 0x0001D91A
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x0001F722 File Offset: 0x0001D922
		public eDynamicFormType DataFormType { get; set; }

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0001F72B File Offset: 0x0001D92B
		// (set) Token: 0x06001BB0 RID: 7088 RVA: 0x0001F733 File Offset: 0x0001D933
		public DynamicDataConversionItemData OriginalData { get; set; }

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x0001F73C File Offset: 0x0001D93C
		// (set) Token: 0x06001BB2 RID: 7090 RVA: 0x0001F744 File Offset: 0x0001D944
		public DynamicDataConversionItemData NewData { get; set; }

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0001F74D File Offset: 0x0001D94D
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x0001F755 File Offset: 0x0001D955
		public bool? ConversionCompletedSuccessfully { get; set; }

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0001F75E File Offset: 0x0001D95E
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x0001F766 File Offset: 0x0001D966
		public string ErrorMessage { get; set; }
	}
}
