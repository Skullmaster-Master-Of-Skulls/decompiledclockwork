using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000336 RID: 822
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportReportFromXmlForUserReq : BaseReportMessageReq
	{
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00008995 File Offset: 0x00006B95
		// (set) Token: 0x0600127A RID: 4730 RVA: 0x0000899D File Offset: 0x00006B9D
		[DataMember]
		public string Xml { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x000089A6 File Offset: 0x00006BA6
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x000089AE File Offset: 0x00006BAE
		[DataMember]
		public int ParentGroupId { get; set; }
	}
}
