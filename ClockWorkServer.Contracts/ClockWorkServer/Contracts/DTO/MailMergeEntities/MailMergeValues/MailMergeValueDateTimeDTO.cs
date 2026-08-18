using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B3 RID: 1203
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueDateTimeDTO : MailMergeValueBaseDTO
	{
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0000BD70 File Offset: 0x00009F70
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x0000BD78 File Offset: 0x00009F78
		[DataMember]
		public DateTime Value { get; set; }

		// Token: 0x060019A2 RID: 6562 RVA: 0x0000BD84 File Offset: 0x00009F84
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
