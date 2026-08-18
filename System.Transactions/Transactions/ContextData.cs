using System;

namespace System.Transactions
{
	// Token: 0x0200000F RID: 15
	internal class ContextData
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0002A634 File Offset: 0x00029A34
		internal static ContextData CurrentData
		{
			get
			{
				ContextData contextData = ContextData.staticData;
				if (contextData == null)
				{
					contextData = new ContextData();
					ContextData.staticData = contextData;
				}
				return contextData;
			}
		}

		// Token: 0x0400009A RID: 154
		internal TransactionScope CurrentScope;

		// Token: 0x0400009B RID: 155
		internal Transaction CurrentTransaction;

		// Token: 0x0400009C RID: 156
		internal DefaultComContextState DefaultComContextState;

		// Token: 0x0400009D RID: 157
		internal WeakReference WeakDefaultComContext;

		// Token: 0x0400009E RID: 158
		[ThreadStatic]
		private static ContextData staticData;
	}
}
