using System;

namespace System.ComponentModel
{
	// Token: 0x0200053C RID: 1340
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultEventAttribute : Attribute
	{
		// Token: 0x06003286 RID: 12934 RVA: 0x000E24E6 File Offset: 0x000E06E6
		public DefaultEventAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000E24F5 File Offset: 0x000E06F5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000E2500 File Offset: 0x000E0700
		public override bool Equals(object obj)
		{
			DefaultEventAttribute defaultEventAttribute = obj as DefaultEventAttribute;
			return defaultEventAttribute != null && defaultEventAttribute.Name == this.name;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000E252A File Offset: 0x000E072A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002981 RID: 10625
		private readonly string name;

		// Token: 0x04002982 RID: 10626
		public static readonly DefaultEventAttribute Default = new DefaultEventAttribute(null);
	}
}
