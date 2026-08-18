using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x02000343 RID: 835
	internal interface IEntityAdapter
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001DC6 RID: 7622
		// (set) Token: 0x06001DC7 RID: 7623
		DbConnection Connection { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001DC8 RID: 7624
		// (set) Token: 0x06001DC9 RID: 7625
		bool AcceptChangesDuringUpdate { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001DCA RID: 7626
		// (set) Token: 0x06001DCB RID: 7627
		int? CommandTimeout { get; set; }

		// Token: 0x06001DCC RID: 7628
		int Update();

		// Token: 0x06001DCD RID: 7629
		Task<int> UpdateAsync(CancellationToken cancellationToken);
	}
}
