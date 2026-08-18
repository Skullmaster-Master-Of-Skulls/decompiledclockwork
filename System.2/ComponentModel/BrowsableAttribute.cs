using System;

namespace System.ComponentModel
{
	// Token: 0x0200051F RID: 1311
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		// Token: 0x060031CD RID: 12749 RVA: 0x000E0450 File Offset: 0x000DE650
		public BrowsableAttribute(bool browsable)
		{
			this.browsable = browsable;
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x000E0466 File Offset: 0x000DE666
		public bool Browsable
		{
			get
			{
				return this.browsable;
			}
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000E0470 File Offset: 0x000DE670
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BrowsableAttribute browsableAttribute = obj as BrowsableAttribute;
			return browsableAttribute != null && browsableAttribute.Browsable == this.browsable;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000E049D File Offset: 0x000DE69D
		public override int GetHashCode()
		{
			return this.browsable.GetHashCode();
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000E04AA File Offset: 0x000DE6AA
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BrowsableAttribute.Default);
		}

		// Token: 0x04002941 RID: 10561
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);

		// Token: 0x04002942 RID: 10562
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		// Token: 0x04002943 RID: 10563
		public static readonly BrowsableAttribute Default = BrowsableAttribute.Yes;

		// Token: 0x04002944 RID: 10564
		private bool browsable = true;
	}
}
