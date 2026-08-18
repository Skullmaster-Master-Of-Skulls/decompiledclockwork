using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders
{
	// Token: 0x02000771 RID: 1905
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataStringDTO : CustomDataHolderDTO
	{
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x00012353 File Offset: 0x00010553
		// (set) Token: 0x06002725 RID: 10021 RVA: 0x0001235B File Offset: 0x0001055B
		[DataMember]
		public string Value { get; set; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x00012364 File Offset: 0x00010564
		// (set) Token: 0x06002727 RID: 10023 RVA: 0x0001236C File Offset: 0x0001056C
		[DataMember]
		public eCustomDataStringTextType TextType { get; set; }

		// Token: 0x06002728 RID: 10024 RVA: 0x00012375 File Offset: 0x00010575
		public CustomDataStringDTO()
		{
			base.DataType = eCustomDataPrimitiveType.String;
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x00012387 File Offset: 0x00010587
		public CustomDataStringDTO(CustomDataHolderDTO dataObj) : base(dataObj)
		{
			base.DataType = eCustomDataPrimitiveType.String;
		}
	}
}
