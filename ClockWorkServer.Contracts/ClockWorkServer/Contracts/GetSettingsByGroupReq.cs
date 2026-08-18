using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000BA RID: 186
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSettingsByGroupReq : SettingsBaseMessageReq
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00002521 File Offset: 0x00000721
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00002529 File Offset: 0x00000729
		[DataMember]
		public Group SettingGroup { get; set; }
	}
}
