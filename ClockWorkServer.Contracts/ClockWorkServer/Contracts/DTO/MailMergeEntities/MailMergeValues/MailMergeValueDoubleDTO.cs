using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B5 RID: 1205
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueDoubleDTO : MailMergeValueBaseDTO
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		// (set) Token: 0x060019A9 RID: 6569 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		[DataMember]
		public double Value { get; set; }

		// Token: 0x060019AA RID: 6570 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
