using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000701 RID: 1793
	[AttributeUsage(AttributeTargets.Property)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebBrowsableAttribute : Attribute
	{
		// Token: 0x06005774 RID: 22388 RVA: 0x00160F93 File Offset: 0x0015FF93
		public WebBrowsableAttribute() : this(true)
		{
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x00160F9C File Offset: 0x0015FF9C
		public WebBrowsableAttribute(bool browsable)
		{
			this._browsable = browsable;
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06005776 RID: 22390 RVA: 0x00160FAB File Offset: 0x0015FFAB
		public bool Browsable
		{
			get
			{
				return this._browsable;
			}
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x00160FB4 File Offset: 0x0015FFB4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebBrowsableAttribute webBrowsableAttribute = obj as WebBrowsableAttribute;
			return webBrowsableAttribute != null && webBrowsableAttribute.Browsable == this.Browsable;
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x00160FE1 File Offset: 0x0015FFE1
		public override int GetHashCode()
		{
			return this._browsable.GetHashCode();
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00160FEE File Offset: 0x0015FFEE
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebBrowsableAttribute.Default);
		}

		// Token: 0x04002F9F RID: 12191
		public static readonly WebBrowsableAttribute Yes = new WebBrowsableAttribute(true);

		// Token: 0x04002FA0 RID: 12192
		public static readonly WebBrowsableAttribute No = new WebBrowsableAttribute(false);

		// Token: 0x04002FA1 RID: 12193
		public static readonly WebBrowsableAttribute Default = WebBrowsableAttribute.No;

		// Token: 0x04002FA2 RID: 12194
		private bool _browsable;
	}
}
