using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000204 RID: 516
	internal class Shared<T>
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x0003980F File Offset: 0x00037A0F
		internal Shared(T value)
		{
			this.Value = value;
		}

		// Token: 0x04000945 RID: 2373
		internal T Value;
	}
}
