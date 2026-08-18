using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x0200017A RID: 378
	[DataContract(Namespace = "http://tpro.ca")]
	public class DoesTableExistResp
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000040EE File Offset: 0x000022EE
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x000040F6 File Offset: 0x000022F6
		[DataMember]
		public bool TableExists { get; set; }
	}
}
