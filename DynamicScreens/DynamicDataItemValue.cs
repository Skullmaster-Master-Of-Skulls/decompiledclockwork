using System;

namespace DynamicScreens
{
	// Token: 0x0200003C RID: 60
	public class DynamicDataItemValue
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00032FCC File Offset: 0x00031FCC
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00032FE3 File Offset: 0x00031FE3
		public int ControlId { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00032FEC File Offset: 0x00031FEC
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00033003 File Offset: 0x00032003
		public string ControlCaption { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0003300C File Offset: 0x0003200C
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00033023 File Offset: 0x00032023
		public int ValInt { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0003302C File Offset: 0x0003202C
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00033043 File Offset: 0x00032043
		public DateTime? ValDateTime { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0003304C File Offset: 0x0003204C
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00033063 File Offset: 0x00032063
		public byte[] ValBytes { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0003306C File Offset: 0x0003206C
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00033083 File Offset: 0x00032083
		public eDynamicDataItemValueType ValType { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0003308C File Offset: 0x0003208C
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x000330A3 File Offset: 0x000320A3
		public bool IsEncryptedData { get; set; }
	}
}
