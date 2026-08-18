using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B4 RID: 1204
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueDateTimeNullableDTO : MailMergeValueBaseDTO
	{
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x0000BDA1 File Offset: 0x00009FA1
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x0000BDA9 File Offset: 0x00009FA9
		[DataMember]
		public DateTime? Value { get; set; }

		// Token: 0x060019A6 RID: 6566 RVA: 0x0000BDB4 File Offset: 0x00009FB4
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
