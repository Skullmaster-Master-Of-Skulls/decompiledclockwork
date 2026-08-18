using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004DC RID: 1244
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicDataDecryptedPreviewItemDTO
	{
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0000C284 File Offset: 0x0000A484
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x0000C28C File Offset: 0x0000A48C
		[DataMember]
		public int DataId { get; set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0000C295 File Offset: 0x0000A495
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x0000C29D File Offset: 0x0000A49D
		[DataMember]
		public byte[] ControlValue { get; set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0000C2A6 File Offset: 0x0000A4A6
		// (set) Token: 0x06001A56 RID: 6742 RVA: 0x0000C2AE File Offset: 0x0000A4AE
		[DataMember]
		public string ControlValuePlainText { get; set; }
	}
}
