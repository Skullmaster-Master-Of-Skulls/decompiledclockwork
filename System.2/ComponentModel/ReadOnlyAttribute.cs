using System;

namespace System.ComponentModel
{
	// Token: 0x0200059F RID: 1439
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ReadOnlyAttribute : Attribute
	{
		// Token: 0x06003593 RID: 13715 RVA: 0x000E8D60 File Offset: 0x000E6F60
		public ReadOnlyAttribute(bool isReadOnly)
		{
			this.isReadOnly = isReadOnly;
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x000E8D6F File Offset: 0x000E6F6F
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000E8D78 File Offset: 0x000E6F78
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			ReadOnlyAttribute readOnlyAttribute = value as ReadOnlyAttribute;
			return readOnlyAttribute != null && readOnlyAttribute.IsReadOnly == this.IsReadOnly;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000E8DA5 File Offset: 0x000E6FA5
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000E8DAD File Offset: 0x000E6FAD
		public override bool IsDefaultAttribute()
		{
			return this.IsReadOnly == ReadOnlyAttribute.Default.IsReadOnly;
		}

		// Token: 0x04002A56 RID: 10838
		private bool isReadOnly;

		// Token: 0x04002A57 RID: 10839
		public static readonly ReadOnlyAttribute Yes = new ReadOnlyAttribute(true);

		// Token: 0x04002A58 RID: 10840
		public static readonly ReadOnlyAttribute No = new ReadOnlyAttribute(false);

		// Token: 0x04002A59 RID: 10841
		public static readonly ReadOnlyAttribute Default = ReadOnlyAttribute.No;
	}
}
