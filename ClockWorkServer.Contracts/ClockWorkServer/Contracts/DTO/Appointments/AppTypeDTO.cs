using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000930 RID: 2352
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppTypeDTO : ICloneable<AppTypeDTO>, ICloneable
	{
		// Token: 0x06002FDD RID: 12253 RVA: 0x000036BD File Offset: 0x000018BD
		public AppTypeDTO()
		{
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x00016F6C File Offset: 0x0001516C
		public AppTypeDTO(AppTypeDTO item)
		{
			AppTypeDTO.CloneBaseAppType<AppTypeDTO, AppTypeDTO>(item, this);
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x00016F80 File Offset: 0x00015180
		public static void CloneBaseAppType<T, TU>(T sourceItem, TU destItem) where T : AppTypeDTO where TU : AppTypeDTO
		{
			bool flag = sourceItem == null;
			if (!flag)
			{
				destItem.AppTypeId = sourceItem.AppTypeId;
				destItem.Description = sourceItem.Description;
				AppTypeDTO appTypeDTO = destItem;
				AppTypeGroupDTO group = sourceItem.Group;
				appTypeDTO.Group = ((group != null) ? group.Clone() : null);
				destItem.DefaultColourArgb = sourceItem.DefaultColourArgb;
				destItem.IsTestOrExam = sourceItem.IsTestOrExam;
				destItem.IsWorkshop = sourceItem.IsWorkshop;
				destItem.IsActive = sourceItem.IsActive;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x0001704D File Offset: 0x0001524D
		// (set) Token: 0x06002FE1 RID: 12257 RVA: 0x00017055 File Offset: 0x00015255
		[DataMember]
		public int AppTypeId { get; set; }

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x0001705E File Offset: 0x0001525E
		// (set) Token: 0x06002FE3 RID: 12259 RVA: 0x00017066 File Offset: 0x00015266
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x0001706F File Offset: 0x0001526F
		// (set) Token: 0x06002FE5 RID: 12261 RVA: 0x00017077 File Offset: 0x00015277
		[DataMember]
		public AppTypeGroupDTO Group { get; set; }

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x00017080 File Offset: 0x00015280
		// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x00017088 File Offset: 0x00015288
		[DataMember]
		public int DefaultColourArgb { get; set; }

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x00017091 File Offset: 0x00015291
		// (set) Token: 0x06002FE9 RID: 12265 RVA: 0x00017099 File Offset: 0x00015299
		[DataMember]
		public bool IsTestOrExam { get; set; }

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x000170A2 File Offset: 0x000152A2
		// (set) Token: 0x06002FEB RID: 12267 RVA: 0x000170AA File Offset: 0x000152AA
		[DataMember]
		public bool IsWorkshop { get; set; }

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x000170B3 File Offset: 0x000152B3
		// (set) Token: 0x06002FED RID: 12269 RVA: 0x000170BB File Offset: 0x000152BB
		[DataMember]
		public bool? IsActive { get; set; }

		// Token: 0x06002FEE RID: 12270 RVA: 0x000170C4 File Offset: 0x000152C4
		public AppTypeDTO Clone()
		{
			return new AppTypeDTO(this);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000170DC File Offset: 0x000152DC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
