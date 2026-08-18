using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field
{
	// Token: 0x0200075D RID: 1885
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateDataInstanceResp
	{
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x0001202E File Offset: 0x0001022E
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x00012036 File Offset: 0x00010236
		[DataMember]
		public Guid DataInstanceId { get; set; }
	}
}
