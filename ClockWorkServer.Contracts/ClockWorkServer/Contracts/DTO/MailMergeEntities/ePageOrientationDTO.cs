using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200046C RID: 1132
	[DataContract(Namespace = "http://tpro.ca")]
	public enum ePageOrientationDTO
	{
		// Token: 0x04000838 RID: 2104
		[EnumMember]
		Portrait,
		// Token: 0x04000839 RID: 2105
		[EnumMember]
		Landscape
	}
}
