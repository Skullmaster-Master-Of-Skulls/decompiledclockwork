using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000931 RID: 2353
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppTypeGroupDTO : ICloneable<AppTypeGroupDTO>, ICloneable
	{
		// Token: 0x06002FF0 RID: 12272 RVA: 0x000036BD File Offset: 0x000018BD
		public AppTypeGroupDTO()
		{
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000170F4 File Offset: 0x000152F4
		public AppTypeGroupDTO(AppTypeGroupDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.AppointmentTypeGroupId = item.AppointmentTypeGroupId;
				this.Description = item.Description;
				this.ClientGroupId = item.ClientGroupId;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x0001713A File Offset: 0x0001533A
		// (set) Token: 0x06002FF3 RID: 12275 RVA: 0x00017142 File Offset: 0x00015342
		[DataMember]
		public int AppointmentTypeGroupId { get; set; }

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x0001714B File Offset: 0x0001534B
		// (set) Token: 0x06002FF5 RID: 12277 RVA: 0x00017153 File Offset: 0x00015353
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x0001715C File Offset: 0x0001535C
		// (set) Token: 0x06002FF7 RID: 12279 RVA: 0x00017164 File Offset: 0x00015364
		[DataMember]
		public int ClientGroupId { get; set; }

		// Token: 0x06002FF8 RID: 12280 RVA: 0x00017170 File Offset: 0x00015370
		public AppTypeGroupDTO Clone()
		{
			return new AppTypeGroupDTO(this);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x00017188 File Offset: 0x00015388
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
