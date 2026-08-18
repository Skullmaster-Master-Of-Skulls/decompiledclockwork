using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000183 RID: 387
	internal interface IMergeHelper<TInputOutput>
	{
		// Token: 0x06000DFD RID: 3581
		void Execute();

		// Token: 0x06000DFE RID: 3582
		IEnumerator<TInputOutput> GetEnumerator();

		// Token: 0x06000DFF RID: 3583
		TInputOutput[] GetResultsAsArray();
	}
}
