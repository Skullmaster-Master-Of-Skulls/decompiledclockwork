using System;

namespace System.Linq
{
	// Token: 0x02000151 RID: 337
	internal class IdentityFunction<TElement>
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002C746 File Offset: 0x0002A946
		public static Func<TElement, TElement> Instance
		{
			get
			{
				return (TElement x) => x;
			}
		}
	}
}
