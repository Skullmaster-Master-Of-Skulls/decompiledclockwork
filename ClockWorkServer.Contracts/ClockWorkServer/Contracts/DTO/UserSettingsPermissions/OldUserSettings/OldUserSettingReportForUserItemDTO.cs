using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000146 RID: 326
	[DataContract(Namespace = "http://tpro.ca")]
	public class OldUserSettingReportForUserItemDTO
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x000039CA File Offset: 0x00001BCA
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x000039D2 File Offset: 0x00001BD2
		[DataMember]
		public int PersonOrGroupId { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x000039DB File Offset: 0x00001BDB
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x000039E3 File Offset: 0x00001BE3
		[DataMember]
		public eOldUserSettingType SettingType { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x000039EC File Offset: 0x00001BEC
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x000039F4 File Offset: 0x00001BF4
		[DataMember]
		public int IntVal { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000039FD File Offset: 0x00001BFD
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00003A05 File Offset: 0x00001C05
		[DataMember]
		public string StringVal { get; set; }
	}
}
