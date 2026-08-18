using System;

namespace System.ComponentModel
{
	// Token: 0x02000546 RID: 1350
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class DesignTimeVisibleAttribute : Attribute
	{
		// Token: 0x060032D1 RID: 13009 RVA: 0x000E2BB4 File Offset: 0x000E0DB4
		public DesignTimeVisibleAttribute(bool visible)
		{
			this.visible = visible;
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000E2BC3 File Offset: 0x000E0DC3
		public DesignTimeVisibleAttribute()
		{
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000E2BCB File Offset: 0x000E0DCB
		public bool Visible
		{
			get
			{
				return this.visible;
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000E2BD4 File Offset: 0x000E0DD4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignTimeVisibleAttribute designTimeVisibleAttribute = obj as DesignTimeVisibleAttribute;
			return designTimeVisibleAttribute != null && designTimeVisibleAttribute.Visible == this.visible;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000E2C01 File Offset: 0x000E0E01
		public override int GetHashCode()
		{
			return typeof(DesignTimeVisibleAttribute).GetHashCode() ^ (this.visible ? -1 : 0);
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000E2C1F File Offset: 0x000E0E1F
		public override bool IsDefaultAttribute()
		{
			return this.Visible == DesignTimeVisibleAttribute.Default.Visible;
		}

		// Token: 0x0400299F RID: 10655
		private bool visible;

		// Token: 0x040029A0 RID: 10656
		public static readonly DesignTimeVisibleAttribute Yes = new DesignTimeVisibleAttribute(true);

		// Token: 0x040029A1 RID: 10657
		public static readonly DesignTimeVisibleAttribute No = new DesignTimeVisibleAttribute(false);

		// Token: 0x040029A2 RID: 10658
		public static readonly DesignTimeVisibleAttribute Default = DesignTimeVisibleAttribute.Yes;
	}
}
