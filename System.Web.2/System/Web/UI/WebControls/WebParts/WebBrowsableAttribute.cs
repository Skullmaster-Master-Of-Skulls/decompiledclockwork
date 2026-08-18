using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000574 RID: 1396
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class WebBrowsableAttribute : Attribute
	{
		// Token: 0x060046C5 RID: 18117 RVA: 0x000E9F6C File Offset: 0x000E816C
		public WebBrowsableAttribute() : this(true)
		{
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x000E9F75 File Offset: 0x000E8175
		public WebBrowsableAttribute(bool browsable)
		{
			this._browsable = browsable;
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x000E9F84 File Offset: 0x000E8184
		public bool Browsable
		{
			get
			{
				return this._browsable;
			}
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x000E9F8C File Offset: 0x000E818C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebBrowsableAttribute webBrowsableAttribute = obj as WebBrowsableAttribute;
			return webBrowsableAttribute != null && webBrowsableAttribute.Browsable == this.Browsable;
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x000E9FB9 File Offset: 0x000E81B9
		public override int GetHashCode()
		{
			return this._browsable.GetHashCode();
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x000E9FC6 File Offset: 0x000E81C6
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebBrowsableAttribute.Default);
		}

		// Token: 0x040026BE RID: 9918
		public static readonly WebBrowsableAttribute Yes = new WebBrowsableAttribute(true);

		// Token: 0x040026BF RID: 9919
		public static readonly WebBrowsableAttribute No = new WebBrowsableAttribute(false);

		// Token: 0x040026C0 RID: 9920
		public static readonly WebBrowsableAttribute Default = WebBrowsableAttribute.No;

		// Token: 0x040026C1 RID: 9921
		private bool _browsable;
	}
}
