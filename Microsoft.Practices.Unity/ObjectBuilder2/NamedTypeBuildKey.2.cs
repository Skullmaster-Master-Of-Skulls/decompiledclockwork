using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000075 RID: 117
	public class NamedTypeBuildKey<T> : NamedTypeBuildKey
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00007237 File Offset: 0x00005437
		public NamedTypeBuildKey() : base(typeof(T), null)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000724A File Offset: 0x0000544A
		public NamedTypeBuildKey(string name) : base(typeof(T), name)
		{
		}
	}
}
