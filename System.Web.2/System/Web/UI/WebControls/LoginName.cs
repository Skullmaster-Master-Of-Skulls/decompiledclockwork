using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000462 RID: 1122
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.LoginNameDesigner,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("FormatString")]
	public class LoginName : WebControl
	{
		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000B0604 File Offset: 0x000AE804
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x000B0631 File Offset: 0x000AE831
		[WebCategory("Appearance")]
		[DefaultValue("{0}")]
		[Localizable(true)]
		[WebSysDescription("LoginName_FormatString")]
		public virtual string FormatString
		{
			get
			{
				object obj = this.ViewState["FormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "{0}";
			}
			set
			{
				this.ViewState["FormatString"] = value;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06003682 RID: 13954 RVA: 0x000B0644 File Offset: 0x000AE844
		internal string UserName
		{
			get
			{
				if (base.DesignMode)
				{
					return SR.GetString("LoginName_DesignModeUserName");
				}
				return LoginUtil.GetUserName(this);
			}
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000B065F File Offset: 0x000AE85F
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.UserName))
			{
				base.Render(writer);
			}
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000B0675 File Offset: 0x000AE875
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.UserName))
			{
				base.RenderBeginTag(writer);
			}
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000B068B File Offset: 0x000AE88B
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.UserName))
			{
				base.RenderEndTag(writer);
			}
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000B06A4 File Offset: 0x000AE8A4
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			string text = this.UserName;
			if (!string.IsNullOrEmpty(text))
			{
				text = HttpUtility.HtmlEncode(text);
				string formatString = this.FormatString;
				if (formatString.Length == 0)
				{
					writer.Write(text);
					return;
				}
				try
				{
					writer.Write(string.Format(CultureInfo.CurrentCulture, formatString, new object[]
					{
						text
					}));
				}
				catch (FormatException innerException)
				{
					throw new FormatException(SR.GetString("LoginName_InvalidFormatString"), innerException);
				}
			}
		}

		// Token: 0x0400220B RID: 8715
		private const string _defaultFormatString = "{0}";
	}
}
