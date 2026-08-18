using System;

namespace System.ComponentModel
{
	// Token: 0x02000595 RID: 1429
	[AttributeUsage(AttributeTargets.All)]
	public sealed class PasswordPropertyTextAttribute : Attribute
	{
		// Token: 0x06003520 RID: 13600 RVA: 0x000E7CB2 File Offset: 0x000E5EB2
		public PasswordPropertyTextAttribute() : this(false)
		{
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000E7CBB File Offset: 0x000E5EBB
		public PasswordPropertyTextAttribute(bool password)
		{
			this._password = password;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x000E7CCA File Offset: 0x000E5ECA
		public bool Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000E7CD2 File Offset: 0x000E5ED2
		public override bool Equals(object o)
		{
			return o is PasswordPropertyTextAttribute && ((PasswordPropertyTextAttribute)o).Password == this._password;
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000E7CF1 File Offset: 0x000E5EF1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000E7CF9 File Offset: 0x000E5EF9
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PasswordPropertyTextAttribute.Default);
		}

		// Token: 0x04002A3C RID: 10812
		public static readonly PasswordPropertyTextAttribute Yes = new PasswordPropertyTextAttribute(true);

		// Token: 0x04002A3D RID: 10813
		public static readonly PasswordPropertyTextAttribute No = new PasswordPropertyTextAttribute(false);

		// Token: 0x04002A3E RID: 10814
		public static readonly PasswordPropertyTextAttribute Default = PasswordPropertyTextAttribute.No;

		// Token: 0x04002A3F RID: 10815
		private bool _password;
	}
}
