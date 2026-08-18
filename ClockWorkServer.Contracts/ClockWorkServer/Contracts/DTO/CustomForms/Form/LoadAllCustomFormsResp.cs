using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form
{
	// Token: 0x0200075A RID: 1882
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllCustomFormsResp
	{
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x00011FB7 File Offset: 0x000101B7
		// (set) Token: 0x060026C0 RID: 9920 RVA: 0x00011FBF File Offset: 0x000101BF
		[DataMember]
		public IList<CustomFormDTO> Forms { get; set; }
	}
}
