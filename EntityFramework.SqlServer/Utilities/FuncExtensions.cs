using System;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200002E RID: 46
	internal static class FuncExtensions
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000B894 File Offset: 0x00009A94
		internal static TResult NullIfNotImplemented<TResult>(this Func<TResult> func)
		{
			TResult result;
			try
			{
				result = func();
			}
			catch (NotImplementedException)
			{
				result = default(TResult);
			}
			return result;
		}
	}
}
