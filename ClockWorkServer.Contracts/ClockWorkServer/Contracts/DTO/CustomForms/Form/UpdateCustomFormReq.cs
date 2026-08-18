using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form
{
	// Token: 0x02000757 RID: 1879
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCustomFormReq : BaseMessageReq
	{
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x00011FA6 File Offset: 0x000101A6
		// (set) Token: 0x060026BB RID: 9915 RVA: 0x00011FAE File Offset: 0x000101AE
		[DataMember]
		public CustomFormDTO Form { get; set; }
	}
}
