using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000E5 RID: 229
	public static class OperationContextExtensions
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0000E414 File Offset: 0x0000C614
		public static T ConvertTo<T>(this OperationContext opCxt) where T : OperationContext
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			bool flag = t == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				t.WhoAmI = opCxt.WhoAmI;
				t.AppContext = opCxt.AppContext;
				t.TenantId = opCxt.TenantId;
				result = t;
			}
			return result;
		}
	}
}
