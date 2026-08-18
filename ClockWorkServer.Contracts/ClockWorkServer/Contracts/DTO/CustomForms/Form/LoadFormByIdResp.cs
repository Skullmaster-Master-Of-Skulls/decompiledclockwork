using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form
{
	// Token: 0x02000752 RID: 1874
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormByIdResp
	{
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x00011F62 File Offset: 0x00010162
		// (set) Token: 0x060026AE RID: 9902 RVA: 0x00011F6A File Offset: 0x0001016A
		[DataMember]
		public CustomFormDTO Form { get; set; }
	}
}
