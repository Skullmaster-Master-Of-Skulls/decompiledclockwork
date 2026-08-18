using System;

namespace System.ComponentModel
{
	// Token: 0x02000547 RID: 1351
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public class DisplayNameAttribute : Attribute
	{
		// Token: 0x060032D8 RID: 13016 RVA: 0x000E2C55 File Offset: 0x000E0E55
		public DisplayNameAttribute() : this(string.Empty)
		{
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000E2C62 File Offset: 0x000E0E62
		public DisplayNameAttribute(string displayName)
		{
			this._displayName = displayName;
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000E2C71 File Offset: 0x000E0E71
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000E2C79 File Offset: 0x000E0E79
		// (set) Token: 0x060032DC RID: 13020 RVA: 0x000E2C81 File Offset: 0x000E0E81
		protected string DisplayNameValue
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000E2C8C File Offset: 0x000E0E8C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DisplayNameAttribute displayNameAttribute = obj as DisplayNameAttribute;
			return displayNameAttribute != null && displayNameAttribute.DisplayName == this.DisplayName;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000E2CBC File Offset: 0x000E0EBC
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000E2CC9 File Offset: 0x000E0EC9
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DisplayNameAttribute.Default);
		}

		// Token: 0x040029A3 RID: 10659
		public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();

		// Token: 0x040029A4 RID: 10660
		private string _displayName;
	}
}
