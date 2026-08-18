using System;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000032 RID: 50
	public class FindRequestContext
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000866D File Offset: 0x0000686D
		protected FindRequestContext(FindCriteria criteria)
		{
			this.criteria = criteria;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000867C File Offset: 0x0000687C
		public FindCriteria Criteria
		{
			get
			{
				return this.criteria;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00008684 File Offset: 0x00006884
		public void AddMatchingEndpoint(EndpointDiscoveryMetadata matchingEndpoint)
		{
			if (matchingEndpoint == null)
			{
				throw FxTrace.Exception.ArgumentNull("matchingEndpoint");
			}
			this.OnAddMatchingEndpoint(matchingEndpoint);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000030E1 File Offset: 0x000012E1
		protected virtual void OnAddMatchingEndpoint(EndpointDiscoveryMetadata matchingEndpoint)
		{
		}

		// Token: 0x040000A1 RID: 161
		private readonly FindCriteria criteria;
	}
}
