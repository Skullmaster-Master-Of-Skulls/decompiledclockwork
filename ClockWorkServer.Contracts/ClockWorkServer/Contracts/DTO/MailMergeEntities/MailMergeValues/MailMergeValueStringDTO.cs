using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B8 RID: 1208
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueStringDTO : MailMergeValueBaseDTO
	{
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0000BE5D File Offset: 0x0000A05D
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x0000BE65 File Offset: 0x0000A065
		[DataMember]
		public string Value { get; set; }

		// Token: 0x060019B6 RID: 6582 RVA: 0x0000BE70 File Offset: 0x0000A070
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
