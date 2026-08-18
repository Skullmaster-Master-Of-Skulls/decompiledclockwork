using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	internal sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002172 File Offset: 0x00000372
		public LocalizationRequiredAttribute() : this(true)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000217B File Offset: 0x0000037B
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000218A File Offset: 0x0000038A
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002192 File Offset: 0x00000392
		public bool Required { get; private set; }
	}
}
