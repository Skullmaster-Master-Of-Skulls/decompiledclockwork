using System;

namespace System.Transactions
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	public sealed class SubordinateTransaction : Transaction
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00033224 File Offset: 0x00032624
		public SubordinateTransaction(IsolationLevel isoLevel, ISimpleTransactionSuperior superior) : base(isoLevel, superior)
		{
		}
	}
}
