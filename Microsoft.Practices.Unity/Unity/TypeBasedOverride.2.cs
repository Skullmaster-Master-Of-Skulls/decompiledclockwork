using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200007A RID: 122
	public class TypeBasedOverride<T> : TypeBasedOverride
	{
		// Token: 0x06000204 RID: 516 RVA: 0x000074A4 File Offset: 0x000056A4
		public TypeBasedOverride(ResolverOverride innerOverride) : base(typeof(T), innerOverride)
		{
		}
	}
}
