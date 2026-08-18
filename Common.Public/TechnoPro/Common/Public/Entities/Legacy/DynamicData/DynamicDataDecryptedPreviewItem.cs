using System;

namespace TechnoPro.Common.Public.Entities.Legacy.DynamicData
{
	// Token: 0x020002F9 RID: 761
	public class DynamicDataDecryptedPreviewItem
	{
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0001C42D File Offset: 0x0001A62D
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x0001C435 File Offset: 0x0001A635
		public int DataId { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0001C43E File Offset: 0x0001A63E
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x0001C446 File Offset: 0x0001A646
		public byte[] ControlValue { get; set; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0001C44F File Offset: 0x0001A64F
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x0001C457 File Offset: 0x0001A657
		public string ControlValuePlainText { get; set; }
	}
}
