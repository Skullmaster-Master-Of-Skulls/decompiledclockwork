using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = true)]
	internal sealed class HtmlAttributeValueAttribute : Attribute
	{
		// Token: 0x0600004C RID: 76 RVA: 0x000023B7 File Offset: 0x000005B7
		public HtmlAttributeValueAttribute([NotNull] string name)
		{
			this.Name = name;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000023C6 File Offset: 0x000005C6
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000023CE File Offset: 0x000005CE
		[NotNull]
		public string Name { get; private set; }
	}
}
