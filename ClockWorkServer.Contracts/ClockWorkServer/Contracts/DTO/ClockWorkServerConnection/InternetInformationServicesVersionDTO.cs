using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection
{
	// Token: 0x02000886 RID: 2182
	[DataContract(Namespace = "http://tpro.ca")]
	public enum InternetInformationServicesVersionDTO
	{
		// Token: 0x0400106E RID: 4206
		[EnumMember]
		NoInstalled,
		// Token: 0x0400106F RID: 4207
		[EnumMember]
		IIS4,
		// Token: 0x04001070 RID: 4208
		[EnumMember]
		IIS5,
		// Token: 0x04001071 RID: 4209
		[EnumMember]
		IIS51,
		// Token: 0x04001072 RID: 4210
		[EnumMember]
		IIS6,
		// Token: 0x04001073 RID: 4211
		[EnumMember]
		IIS7,
		// Token: 0x04001074 RID: 4212
		[EnumMember]
		IIS75,
		// Token: 0x04001075 RID: 4213
		[EnumMember]
		IIS8
	}
}
