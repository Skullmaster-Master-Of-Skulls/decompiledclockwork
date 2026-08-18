using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class AspMvcAreaAttribute : PathReferenceAttribute
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000022F7 File Offset: 0x000004F7
		public AspMvcAreaAttribute()
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000022FF File Offset: 0x000004FF
		public AspMvcAreaAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000230E File Offset: 0x0000050E
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002316 File Offset: 0x00000516
		[NotNull]
		public string AnonymousProperty { get; private set; }
	}
}
