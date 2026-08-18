using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	internal sealed class AspMvcControllerAttribute : Attribute
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000231F File Offset: 0x0000051F
		public AspMvcControllerAttribute()
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002327 File Offset: 0x00000527
		public AspMvcControllerAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002336 File Offset: 0x00000536
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000233E File Offset: 0x0000053E
		[NotNull]
		public string AnonymousProperty { get; private set; }
	}
}
