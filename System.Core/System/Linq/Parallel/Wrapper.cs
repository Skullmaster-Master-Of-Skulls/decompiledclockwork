using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000209 RID: 521
	internal struct Wrapper<T>
	{
		// Token: 0x0600106E RID: 4206 RVA: 0x0003A1B8 File Offset: 0x000383B8
		internal Wrapper(T value)
		{
			this.Value = value;
		}

		// Token: 0x04000955 RID: 2389
		internal T Value;
	}
}
