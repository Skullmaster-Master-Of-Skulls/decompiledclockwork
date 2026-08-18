using System;

namespace System.ComponentModel
{
	// Token: 0x02000545 RID: 1349
	[AttributeUsage(AttributeTargets.All)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		// Token: 0x060032CB RID: 13003 RVA: 0x000E2B2B File Offset: 0x000E0D2B
		public DesignOnlyAttribute(bool isDesignOnly)
		{
			this.isDesignOnly = isDesignOnly;
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000E2B3A File Offset: 0x000E0D3A
		public bool IsDesignOnly
		{
			get
			{
				return this.isDesignOnly;
			}
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000E2B42 File Offset: 0x000E0D42
		public override bool IsDefaultAttribute()
		{
			return this.IsDesignOnly == DesignOnlyAttribute.Default.IsDesignOnly;
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000E2B58 File Offset: 0x000E0D58
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignOnlyAttribute designOnlyAttribute = obj as DesignOnlyAttribute;
			return designOnlyAttribute != null && designOnlyAttribute.isDesignOnly == this.isDesignOnly;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000E2B85 File Offset: 0x000E0D85
		public override int GetHashCode()
		{
			return this.isDesignOnly.GetHashCode();
		}

		// Token: 0x0400299B RID: 10651
		private bool isDesignOnly;

		// Token: 0x0400299C RID: 10652
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);

		// Token: 0x0400299D RID: 10653
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		// Token: 0x0400299E RID: 10654
		public static readonly DesignOnlyAttribute Default = DesignOnlyAttribute.No;
	}
}
