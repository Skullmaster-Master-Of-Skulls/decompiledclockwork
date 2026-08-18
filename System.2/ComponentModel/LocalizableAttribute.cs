using System;

namespace System.ComponentModel
{
	// Token: 0x0200058B RID: 1419
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizableAttribute : Attribute
	{
		// Token: 0x06003459 RID: 13401 RVA: 0x000E4EC6 File Offset: 0x000E30C6
		public LocalizableAttribute(bool isLocalizable)
		{
			this.isLocalizable = isLocalizable;
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000E4ED5 File Offset: 0x000E30D5
		public bool IsLocalizable
		{
			get
			{
				return this.isLocalizable;
			}
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000E4EDD File Offset: 0x000E30DD
		public override bool IsDefaultAttribute()
		{
			return this.IsLocalizable == LocalizableAttribute.Default.IsLocalizable;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000E4EF4 File Offset: 0x000E30F4
		public override bool Equals(object obj)
		{
			LocalizableAttribute localizableAttribute = obj as LocalizableAttribute;
			return localizableAttribute != null && localizableAttribute.IsLocalizable == this.isLocalizable;
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000E4F1B File Offset: 0x000E311B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040029EF RID: 10735
		private bool isLocalizable;

		// Token: 0x040029F0 RID: 10736
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);

		// Token: 0x040029F1 RID: 10737
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		// Token: 0x040029F2 RID: 10738
		public static readonly LocalizableAttribute Default = LocalizableAttribute.No;
	}
}
