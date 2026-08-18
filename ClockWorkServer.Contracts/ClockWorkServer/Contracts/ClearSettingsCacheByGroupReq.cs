using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000C1 RID: 193
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearSettingsCacheByGroupReq : SettingsBaseMessageReq
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000025B2 File Offset: 0x000007B2
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x000025BA File Offset: 0x000007BA
		[DataMember]
		public Group Group { get; set; }
	}
}
