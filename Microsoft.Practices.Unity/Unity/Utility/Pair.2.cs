using System;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x020000AB RID: 171
	public static class Pair
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000AEDA File Offset: 0x000090DA
		public static Pair<TFirstParameter, TSecondParameter> Make<TFirstParameter, TSecondParameter>(TFirstParameter first, TSecondParameter second)
		{
			return new Pair<TFirstParameter, TSecondParameter>(first, second);
		}
	}
}
