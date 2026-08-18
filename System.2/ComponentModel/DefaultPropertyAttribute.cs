using System;

namespace System.ComponentModel
{
	// Token: 0x0200053D RID: 1341
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultPropertyAttribute : Attribute
	{
		// Token: 0x0600328B RID: 12939 RVA: 0x000E253F File Offset: 0x000E073F
		public DefaultPropertyAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000E254E File Offset: 0x000E074E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000E2558 File Offset: 0x000E0758
		public override bool Equals(object obj)
		{
			DefaultPropertyAttribute defaultPropertyAttribute = obj as DefaultPropertyAttribute;
			return defaultPropertyAttribute != null && defaultPropertyAttribute.Name == this.name;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000E2582 File Offset: 0x000E0782
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002983 RID: 10627
		private readonly string name;

		// Token: 0x04002984 RID: 10628
		public static readonly DefaultPropertyAttribute Default = new DefaultPropertyAttribute(null);
	}
}
