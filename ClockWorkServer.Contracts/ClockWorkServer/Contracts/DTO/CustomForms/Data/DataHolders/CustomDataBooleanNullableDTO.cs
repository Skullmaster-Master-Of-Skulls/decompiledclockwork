using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders
{
	// Token: 0x0200076B RID: 1899
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataBooleanNullableDTO : CustomDataHolderDTO
	{
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x000121C3 File Offset: 0x000103C3
		// (set) Token: 0x06002706 RID: 9990 RVA: 0x000121CB File Offset: 0x000103CB
		[DataMember]
		public bool? Value { get; set; }

		// Token: 0x06002707 RID: 9991 RVA: 0x000121D4 File Offset: 0x000103D4
		public CustomDataBooleanNullableDTO()
		{
			base.DataType = eCustomDataPrimitiveType.BooleanNullable;
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000121E6 File Offset: 0x000103E6
		public CustomDataBooleanNullableDTO(CustomDataHolderDTO dataObj) : base(dataObj)
		{
			base.DataType = eCustomDataPrimitiveType.BooleanNullable;
		}
	}
}
