using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000AE RID: 174
	[DataContract(Namespace = "http://tpro.ca")]
	public enum MessageType
	{
		// Token: 0x04000031 RID: 49
		[EnumMember]
		Private,
		// Token: 0x04000032 RID: 50
		[EnumMember]
		Public
	}
}
