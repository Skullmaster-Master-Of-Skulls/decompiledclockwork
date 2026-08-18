using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200038C RID: 908
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindUserGroupObjectBySearchStringReq : BaseMessageReq
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00009AA5 File Offset: 0x00007CA5
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00009AAD File Offset: 0x00007CAD
		[DataMember]
		public string SearchString { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00009AB6 File Offset: 0x00007CB6
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00009ABE File Offset: 0x00007CBE
		[DataMember]
		public eUserGroupObjectType[] ObjectTypesToExclude { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00009AC7 File Offset: 0x00007CC7
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x00009ACF File Offset: 0x00007CCF
		[DataMember]
		public int StartIndex { get; set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x00009AD8 File Offset: 0x00007CD8
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x00009AE0 File Offset: 0x00007CE0
		[DataMember]
		public int MaxResultsCount { get; set; }
	}
}
