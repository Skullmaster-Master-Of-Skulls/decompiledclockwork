using System;

namespace System.ComponentModel
{
	// Token: 0x0200053B RID: 1339
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultBindingPropertyAttribute : Attribute
	{
		// Token: 0x06003280 RID: 12928 RVA: 0x000E2481 File Offset: 0x000E0681
		public DefaultBindingPropertyAttribute()
		{
			this.name = null;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000E2490 File Offset: 0x000E0690
		public DefaultBindingPropertyAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000E249F File Offset: 0x000E069F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000E24A8 File Offset: 0x000E06A8
		public override bool Equals(object obj)
		{
			DefaultBindingPropertyAttribute defaultBindingPropertyAttribute = obj as DefaultBindingPropertyAttribute;
			return defaultBindingPropertyAttribute != null && defaultBindingPropertyAttribute.Name == this.name;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000E24D2 File Offset: 0x000E06D2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400297F RID: 10623
		private readonly string name;

		// Token: 0x04002980 RID: 10624
		public static readonly DefaultBindingPropertyAttribute Default = new DefaultBindingPropertyAttribute();
	}
}
