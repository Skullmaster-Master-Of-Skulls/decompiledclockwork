using System;

namespace System.ComponentModel
{
	// Token: 0x02000565 RID: 1381
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		// Token: 0x060033AD RID: 13229 RVA: 0x000E40CB File Offset: 0x000E22CB
		public ImmutableObjectAttribute(bool immutable)
		{
			this.immutable = immutable;
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000E40E1 File Offset: 0x000E22E1
		public bool Immutable
		{
			get
			{
				return this.immutable;
			}
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000E40EC File Offset: 0x000E22EC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ImmutableObjectAttribute immutableObjectAttribute = obj as ImmutableObjectAttribute;
			return immutableObjectAttribute != null && immutableObjectAttribute.Immutable == this.immutable;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000E4119 File Offset: 0x000E2319
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000E4121 File Offset: 0x000E2321
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ImmutableObjectAttribute.Default);
		}

		// Token: 0x040029C1 RID: 10689
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);

		// Token: 0x040029C2 RID: 10690
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		// Token: 0x040029C3 RID: 10691
		public static readonly ImmutableObjectAttribute Default = ImmutableObjectAttribute.No;

		// Token: 0x040029C4 RID: 10692
		private bool immutable = true;
	}
}
