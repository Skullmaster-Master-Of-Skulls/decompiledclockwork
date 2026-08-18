using System;
using System.Threading;

namespace System.Data.Common.Utils
{
	// Token: 0x0200038B RID: 907
	internal sealed class Singleton<TValue> where TValue : class
	{
		// Token: 0x06003273 RID: 12915 RVA: 0x000C529E File Offset: 0x000C349E
		internal Singleton(Func<TValue> function)
		{
			EntityUtil.CheckArgumentNull<Func<TValue>>(function, "function");
			this.valueProvider = function;
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000C52BC File Offset: 0x000C34BC
		internal TValue Value
		{
			get
			{
				TValue tvalue = this.value;
				if (tvalue == null)
				{
					TValue tvalue2 = this.valueProvider();
					Interlocked.CompareExchange<TValue>(ref this.value, tvalue2, default(TValue));
					tvalue = this.value;
				}
				return tvalue;
			}
		}

		// Token: 0x04001650 RID: 5712
		private readonly Func<TValue> valueProvider;

		// Token: 0x04001651 RID: 5713
		private TValue value;
	}
}
