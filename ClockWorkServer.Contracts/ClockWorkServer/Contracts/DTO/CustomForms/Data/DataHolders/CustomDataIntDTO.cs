using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders
{
	// Token: 0x0200076F RID: 1903
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataIntDTO : CustomDataHolderDTO
	{
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000122E7 File Offset: 0x000104E7
		// (set) Token: 0x0600271D RID: 10013 RVA: 0x000122EF File Offset: 0x000104EF
		[DataMember]
		public int Value { get; set; }

		// Token: 0x0600271E RID: 10014 RVA: 0x000122F8 File Offset: 0x000104F8
		public CustomDataIntDTO()
		{
			base.DataType = eCustomDataPrimitiveType.Int;
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0001230A File Offset: 0x0001050A
		public CustomDataIntDTO(CustomDataHolderDTO dataObj) : base(dataObj)
		{
			base.DataType = eCustomDataPrimitiveType.Int;
		}
	}
}
