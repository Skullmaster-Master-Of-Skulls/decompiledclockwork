using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders
{
	// Token: 0x0200076A RID: 1898
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataBooleanDTO : CustomDataHolderDTO
	{
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x0001218D File Offset: 0x0001038D
		// (set) Token: 0x06002702 RID: 9986 RVA: 0x00012195 File Offset: 0x00010395
		[DataMember]
		public bool Value { get; set; }

		// Token: 0x06002703 RID: 9987 RVA: 0x0001219E File Offset: 0x0001039E
		public CustomDataBooleanDTO()
		{
			base.DataType = eCustomDataPrimitiveType.Boolean;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000121B0 File Offset: 0x000103B0
		public CustomDataBooleanDTO(CustomDataHolderDTO dataObj) : base(dataObj)
		{
			base.DataType = eCustomDataPrimitiveType.Boolean;
		}
	}
}
