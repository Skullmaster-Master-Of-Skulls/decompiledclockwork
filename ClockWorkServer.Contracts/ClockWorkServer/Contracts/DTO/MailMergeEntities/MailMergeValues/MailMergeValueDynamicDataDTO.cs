using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B6 RID: 1206
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueDynamicDataDTO : MailMergeValueBaseDTO
	{
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x0000BE01 File Offset: 0x0000A001
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x0000BE09 File Offset: 0x0000A009
		[DataMember]
		public DynamicDataDTO Value { get; set; }

		// Token: 0x060019AE RID: 6574 RVA: 0x0000BE14 File Offset: 0x0000A014
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
