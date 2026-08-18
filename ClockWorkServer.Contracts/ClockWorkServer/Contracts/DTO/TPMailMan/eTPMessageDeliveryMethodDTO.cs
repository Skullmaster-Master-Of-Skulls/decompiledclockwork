using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B1 RID: 433
	[DataContract(Namespace = "http://tpro.ca")]
	[Serializable]
	public enum eTPMessageDeliveryMethodDTO
	{
		// Token: 0x04000230 RID: 560
		[EnumMember]
		Unknown,
		// Token: 0x04000231 RID: 561
		[EnumMember]
		PlainText,
		// Token: 0x04000232 RID: 562
		[EnumMember]
		Html,
		// Token: 0x04000233 RID: 563
		[EnumMember]
		HtmlAndPlainText = 4
	}
}
