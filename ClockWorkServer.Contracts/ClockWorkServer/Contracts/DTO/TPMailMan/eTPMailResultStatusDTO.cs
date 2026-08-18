using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B3 RID: 435
	[DataContract(Namespace = "http://tpro.ca")]
	[Serializable]
	public enum eTPMailResultStatusDTO
	{
		// Token: 0x0400023C RID: 572
		[EnumMember]
		Unknown,
		// Token: 0x0400023D RID: 573
		[EnumMember]
		Pending,
		// Token: 0x0400023E RID: 574
		[EnumMember]
		CompletedSuccess,
		// Token: 0x0400023F RID: 575
		[EnumMember]
		CompletedWithWarnings,
		// Token: 0x04000240 RID: 576
		[EnumMember]
		Failed,
		// Token: 0x04000241 RID: 577
		[EnumMember]
		NotSentBecauseTemplateIsDisabled,
		// Token: 0x04000242 RID: 578
		[EnumMember]
		NotSentBecausePreviewMode
	}
}
