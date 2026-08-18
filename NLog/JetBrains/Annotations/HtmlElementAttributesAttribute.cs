using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = true)]
	internal sealed class HtmlElementAttributesAttribute : Attribute
	{
		// Token: 0x06000048 RID: 72 RVA: 0x0000238F File Offset: 0x0000058F
		public HtmlElementAttributesAttribute()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002397 File Offset: 0x00000597
		public HtmlElementAttributesAttribute([NotNull] string name)
		{
			this.Name = name;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000023A6 File Offset: 0x000005A6
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000023AE File Offset: 0x000005AE
		[NotNull]
		public string Name { get; private set; }
	}
}
