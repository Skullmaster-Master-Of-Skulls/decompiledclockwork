using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000140 RID: 320
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserOrGroupJustPermissionSetDTO : ICloneable<UserOrGroupJustPermissionSetDTO>, ICloneable
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x000036BD File Offset: 0x000018BD
		public UserOrGroupJustPermissionSetDTO()
		{
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0000375C File Offset: 0x0000195C
		public UserOrGroupJustPermissionSetDTO(UserOrGroupJustPermissionSetDTO item)
		{
			this.PermissionType = item.PermissionType;
			this.PersonOrGroupId = item.PersonOrGroupId;
			IList<UserOrGroupJustPermissionDTO> generalPermissions;
			if (item.GeneralPermissions != null)
			{
				generalPermissions = (from g in item.GeneralPermissions
				select g.Clone()).ToList<UserOrGroupJustPermissionDTO>();
			}
			else
			{
				generalPermissions = null;
			}
			this.GeneralPermissions = generalPermissions;
			this.ScreenNumsAllowedViewScreen = new List<int>(item.ScreenNumsAllowedViewScreen);
			this.ScreenNumsAllowedModifyScreen = new List<int>(item.ScreenNumsAllowedModifyScreen);
			this.ScreenNumsAllowedCreateScreen = new List<int>(item.ScreenNumsAllowedCreateScreen);
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00003802 File Offset: 0x00001A02
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0000380A File Offset: 0x00001A0A
		[DataMember]
		public eUserPermissionType PermissionType { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00003813 File Offset: 0x00001A13
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0000381B File Offset: 0x00001A1B
		[DataMember]
		public int PersonOrGroupId { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00003824 File Offset: 0x00001A24
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0000382C File Offset: 0x00001A2C
		[DataMember]
		public IList<UserOrGroupJustPermissionDTO> GeneralPermissions { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00003835 File Offset: 0x00001A35
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0000383D File Offset: 0x00001A3D
		[DataMember]
		public IList<int> ScreenNumsAllowedViewScreen { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00003846 File Offset: 0x00001A46
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0000384E File Offset: 0x00001A4E
		[DataMember]
		public IList<int> ScreenNumsAllowedModifyScreen { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00003857 File Offset: 0x00001A57
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0000385F File Offset: 0x00001A5F
		[DataMember]
		public IList<int> ScreenNumsAllowedCreateScreen { get; set; }

		// Token: 0x060007F2 RID: 2034 RVA: 0x00003868 File Offset: 0x00001A68
		public UserOrGroupJustPermissionSetDTO Clone()
		{
			return new UserOrGroupJustPermissionSetDTO(this);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00003880 File Offset: 0x00001A80
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
