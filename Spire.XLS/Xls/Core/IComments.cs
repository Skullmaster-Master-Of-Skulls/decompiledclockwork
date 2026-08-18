using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020001DF RID: 479
	public interface IComments : IEnumerable
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001A7F RID: 6783
		int Count { get; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001A80 RID: 6784
		object Parent { get; }

		// Token: 0x170009D5 RID: 2517
		ICommentShape this[int Index]
		{
			get;
		}

		// Token: 0x170009D6 RID: 2518
		ICommentShape this[int iRow, int iColumn]
		{
			get;
		}
	}
}
