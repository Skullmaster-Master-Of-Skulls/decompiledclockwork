using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200069F RID: 1695
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllFormsResp
	{
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0000FBE9 File Offset: 0x0000DDE9
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x0000FBF1 File Offset: 0x0000DDF1
		[DataMember]
		public Forest<DynamicFormOrGroupOrFormTypeDTO> Forms { get; set; }
	}
}
