using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200069C RID: 1692
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindFormByTitleSubstringMatchReq : BaseMessageReq
	{
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x0000FBA5 File Offset: 0x0000DDA5
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x0000FBAD File Offset: 0x0000DDAD
		[DataMember]
		public string SubstringToMatch { get; set; }

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x0000FBB6 File Offset: 0x0000DDB6
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x0000FBBE File Offset: 0x0000DDBE
		[DataMember]
		public bool SearchPrimaryTitle { get; set; }

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x0000FBC7 File Offset: 0x0000DDC7
		// (set) Token: 0x0600226C RID: 8812 RVA: 0x0000FBCF File Offset: 0x0000DDCF
		[DataMember]
		public bool SearchSecondaryTitle { get; set; }
	}
}
