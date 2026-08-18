using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B8 RID: 184
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupSettingDTO
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000024F6 File Offset: 0x000006F6
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x000024FE File Offset: 0x000006FE
		[DataMember]
		public Setting Setting { get; set; }
	}
}
