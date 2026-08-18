using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000576 RID: 1398
	[AttributeUsage(AttributeTargets.Property)]
	public class WebDisplayNameAttribute : Attribute
	{
		// Token: 0x060046D5 RID: 18133 RVA: 0x000EA082 File Offset: 0x000E8282
		public WebDisplayNameAttribute() : this(string.Empty)
		{
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x000EA08F File Offset: 0x000E828F
		public WebDisplayNameAttribute(string displayName)
		{
			this._displayName = displayName;
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x060046D7 RID: 18135 RVA: 0x000EA09E File Offset: 0x000E829E
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x000EA0A6 File Offset: 0x000E82A6
		// (set) Token: 0x060046D9 RID: 18137 RVA: 0x000EA0AE File Offset: 0x000E82AE
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

		// Token: 0x060046DA RID: 18138 RVA: 0x000EA0B8 File Offset: 0x000E82B8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebDisplayNameAttribute webDisplayNameAttribute = obj as WebDisplayNameAttribute;
			return webDisplayNameAttribute != null && webDisplayNameAttribute.DisplayName == this.DisplayName;
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x000EA0E8 File Offset: 0x000E82E8
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x000EA0F5 File Offset: 0x000E82F5
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebDisplayNameAttribute.Default);
		}

		// Token: 0x040026C4 RID: 9924
		public static readonly WebDisplayNameAttribute Default = new WebDisplayNameAttribute();

		// Token: 0x040026C5 RID: 9925
		private string _displayName;
	}
}
