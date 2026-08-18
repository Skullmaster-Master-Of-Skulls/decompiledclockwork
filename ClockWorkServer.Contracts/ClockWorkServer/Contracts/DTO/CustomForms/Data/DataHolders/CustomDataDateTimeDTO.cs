using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders
{
	// Token: 0x0200076C RID: 1900
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataDateTimeDTO : CustomDataHolderDTO
	{
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x000121F9 File Offset: 0x000103F9
		// (set) Token: 0x0600270A RID: 9994 RVA: 0x00012201 File Offset: 0x00010401
		[DataMember]
		public DateTime Value { get; set; }

		// Token: 0x0600270B RID: 9995 RVA: 0x0001220A File Offset: 0x0001040A
		public CustomDataDateTimeDTO()
		{
			base.DataType = eCustomDataPrimitiveType.DateTime;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0001221C File Offset: 0x0001041C
		public CustomDataDateTimeDTO(CustomDataHolderDTO dataObj) : base(dataObj)
		{
			base.DataType = eCustomDataPrimitiveType.DateTime;
		}
	}
}
