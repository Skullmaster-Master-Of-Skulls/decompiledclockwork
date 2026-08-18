using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x0200093A RID: 2362
	[DataContract(Namespace = "http://tpro.ca")]
	public class IconInfoDTO : ICloneable<IconInfoDTO>, ICloneable
	{
		// Token: 0x06003078 RID: 12408 RVA: 0x000036BD File Offset: 0x000018BD
		public IconInfoDTO()
		{
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x00017AAD File Offset: 0x00015CAD
		public IconInfoDTO(IconInfoDTO item)
		{
			this.IconInfoId = item.IconInfoId;
			this.IconText = item.IconText;
			this.IconLetterIdentifier = item.IconLetterIdentifier;
			this.IconNum = item.IconNum;
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x00017AEB File Offset: 0x00015CEB
		// (set) Token: 0x0600307B RID: 12411 RVA: 0x00017AF3 File Offset: 0x00015CF3
		[DataMember]
		public int IconInfoId { get; set; }

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x00017AFC File Offset: 0x00015CFC
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x00017B04 File Offset: 0x00015D04
		[DataMember]
		public string IconText { get; set; }

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x00017B0D File Offset: 0x00015D0D
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x00017B15 File Offset: 0x00015D15
		[DataMember]
		public string IconLetterIdentifier { get; set; }

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x00017B1E File Offset: 0x00015D1E
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x00017B26 File Offset: 0x00015D26
		[DataMember]
		public int IconNum { get; set; }

		// Token: 0x06003082 RID: 12418 RVA: 0x00017B30 File Offset: 0x00015D30
		public IconInfoDTO Clone()
		{
			return new IconInfoDTO(this);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x00017B48 File Offset: 0x00015D48
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
