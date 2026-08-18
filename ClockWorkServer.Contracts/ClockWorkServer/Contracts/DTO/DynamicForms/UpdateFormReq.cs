using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006AC RID: 1708
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateFormReq : BaseMessageReq
	{
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x0000FCC6 File Offset: 0x0000DEC6
		// (set) Token: 0x0600229A RID: 8858 RVA: 0x0000FCCE File Offset: 0x0000DECE
		[DataMember]
		public DynamicFormWithExtendedInfoDTO Form { get; set; }
	}
}
