using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders
{
	// Token: 0x02000770 RID: 1904
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataListItemDTO : CustomDataHolderDTO
	{
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x0001231D File Offset: 0x0001051D
		// (set) Token: 0x06002721 RID: 10017 RVA: 0x00012325 File Offset: 0x00010525
		[DataMember]
		public CustomListItemDTO ListItem { get; set; }

		// Token: 0x06002722 RID: 10018 RVA: 0x0001232E File Offset: 0x0001052E
		public CustomDataListItemDTO()
		{
			base.DataType = eCustomDataPrimitiveType.ListItem;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00012340 File Offset: 0x00010540
		public CustomDataListItemDTO(CustomDataHolderDTO dataObj) : base(dataObj)
		{
			base.DataType = eCustomDataPrimitiveType.ListItem;
		}
	}
}
