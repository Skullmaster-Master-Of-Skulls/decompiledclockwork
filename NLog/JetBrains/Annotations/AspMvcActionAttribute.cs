using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	internal sealed class AspMvcActionAttribute : Attribute
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000022CF File Offset: 0x000004CF
		public AspMvcActionAttribute()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000022D7 File Offset: 0x000004D7
		public AspMvcActionAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000022E6 File Offset: 0x000004E6
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000022EE File Offset: 0x000004EE
		[NotNull]
		public string AnonymousProperty { get; private set; }
	}
}
