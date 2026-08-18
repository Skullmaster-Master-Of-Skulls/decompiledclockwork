using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders
{
	// Token: 0x0200076E RID: 1902
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(CustomDataBooleanDTO))]
	[KnownType(typeof(CustomDataDateTimeDTO))]
	[KnownType(typeof(CustomDataFileDTO))]
	[KnownType(typeof(CustomDataIntDTO))]
	[KnownType(typeof(CustomDataListItemDTO))]
	[KnownType(typeof(CustomDataStringDTO))]
	public class CustomDataHolderDTO
	{
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x00012287 File Offset: 0x00010487
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x0001228F File Offset: 0x0001048F
		[DataMember]
		public Guid DataInstanceId { get; set; }

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x00012298 File Offset: 0x00010498
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x000122A0 File Offset: 0x000104A0
		[DataMember]
		public eCustomDataPrimitiveType DataType { get; set; }

		// Token: 0x06002719 RID: 10009 RVA: 0x000036BD File Offset: 0x000018BD
		public CustomDataHolderDTO()
		{
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000122A9 File Offset: 0x000104A9
		public CustomDataHolderDTO(CustomDataHolderDTO dataObj)
		{
			this.DataType = dataObj.DataType;
			this.DataInstanceId = dataObj.DataInstanceId;
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000122CD File Offset: 0x000104CD
		public CustomDataHolderDTO(Guid dataInstanceId, eCustomDataPrimitiveType dataType)
		{
			this.DataInstanceId = dataInstanceId;
			this.DataType = dataType;
		}
	}
}
