using System;

namespace System.ComponentModel
{
	// Token: 0x02000591 RID: 1425
	[AttributeUsage(AttributeTargets.All)]
	public sealed class MergablePropertyAttribute : Attribute
	{
		// Token: 0x060034FF RID: 13567 RVA: 0x000E7722 File Offset: 0x000E5922
		public MergablePropertyAttribute(bool allowMerge)
		{
			this.allowMerge = allowMerge;
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000E7731 File Offset: 0x000E5931
		public bool AllowMerge
		{
			get
			{
				return this.allowMerge;
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000E773C File Offset: 0x000E593C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			MergablePropertyAttribute mergablePropertyAttribute = obj as MergablePropertyAttribute;
			return mergablePropertyAttribute != null && mergablePropertyAttribute.AllowMerge == this.allowMerge;
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000E7769 File Offset: 0x000E5969
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000E7771 File Offset: 0x000E5971
		public override bool IsDefaultAttribute()
		{
			return this.Equals(MergablePropertyAttribute.Default);
		}

		// Token: 0x04002A34 RID: 10804
		public static readonly MergablePropertyAttribute Yes = new MergablePropertyAttribute(true);

		// Token: 0x04002A35 RID: 10805
		public static readonly MergablePropertyAttribute No = new MergablePropertyAttribute(false);

		// Token: 0x04002A36 RID: 10806
		public static readonly MergablePropertyAttribute Default = MergablePropertyAttribute.Yes;

		// Token: 0x04002A37 RID: 10807
		private bool allowMerge;
	}
}
