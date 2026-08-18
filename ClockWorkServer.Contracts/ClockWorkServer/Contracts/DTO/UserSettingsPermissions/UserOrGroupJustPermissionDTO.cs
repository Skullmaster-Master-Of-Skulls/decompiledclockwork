using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200013F RID: 319
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserOrGroupJustPermissionDTO : ICloneable<UserOrGroupJustPermissionDTO>, ICloneable
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x000036BD File Offset: 0x000018BD
		public UserOrGroupJustPermissionDTO()
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000036C7 File Offset: 0x000018C7
		public UserOrGroupJustPermissionDTO(UserOrGroupJustPermissionDTO item)
		{
			this.Id = item.Id;
			this.Permission = item.Permission;
			this.IsAllowed = item.IsAllowed;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000036F8 File Offset: 0x000018F8
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x00003700 File Offset: 0x00001900
		[DataMember]
		public int Id { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00003709 File Offset: 0x00001909
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00003711 File Offset: 0x00001911
		[DataMember]
		public UserPermissionEnum Permission { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0000371A File Offset: 0x0000191A
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00003722 File Offset: 0x00001922
		[DataMember]
		public bool IsAllowed { get; set; }

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000372C File Offset: 0x0000192C
		public UserOrGroupJustPermissionDTO Clone()
		{
			return new UserOrGroupJustPermissionDTO(this);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00003744 File Offset: 0x00001944
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
