using System;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.UI;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000ED RID: 237
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.ImageButton", "HtmlEditor.ToolbarButtons.ImageButton")]
	public abstract class ImageButton : CommonButton
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x00012F96 File Offset: 0x00011196
		public ImageButton() : base(HtmlTextWriterTag.Img)
		{
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00012FA0 File Offset: 0x000111A0
		protected virtual Type BaseImageButtonType
		{
			get
			{
				return typeof(ImageButton);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00012FAC File Offset: 0x000111AC
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00012FCC File Offset: 0x000111CC
		[ClientPropertyName("normalSrc")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		public string NormalSrc
		{
			get
			{
				return (string)(this.ViewState["NormalSrc"] ?? string.Empty);
			}
			set
			{
				this.ViewState["NormalSrc"] = value;
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00012FDF File Offset: 0x000111DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeNormalSrc()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00012FE7 File Offset: 0x000111E7
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00013007 File Offset: 0x00011207
		[ClientPropertyName("hoverSrc")]
		[DefaultValue("")]
		[Category("Appearance")]
		[ExtenderControlProperty]
		public string HoverSrc
		{
			get
			{
				return (string)(this.ViewState["HoverSrc"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HoverSrc"] = value;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001301A File Offset: 0x0001121A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeHoverSrc()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00013022 File Offset: 0x00011222
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00013042 File Offset: 0x00011242
		[Category("Appearance")]
		[ClientPropertyName("downSrc")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string DownSrc
		{
			get
			{
				return (string)(this.ViewState["DownSrc"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DownSrc"] = value;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00013055 File Offset: 0x00011255
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeDownSrc()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001305D File Offset: 0x0001125D
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001307D File Offset: 0x0001127D
		[DefaultValue("")]
		[Category("Appearance")]
		[ExtenderControlProperty]
		[ClientPropertyName("activeSrc")]
		public string ActiveSrc
		{
			get
			{
				return (string)(this.ViewState["ActiveSrc"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ActiveSrc"] = value;
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00013090 File Offset: 0x00011290
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeActiveSrc()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00013098 File Offset: 0x00011298
		protected void RegisterButtonImages(string name, string ext)
		{
			Type type = base.GetType();
			Toolbar toolbar = null;
			for (Control parent = this.Parent; parent != null; parent = parent.Parent)
			{
				toolbar = (parent as Toolbar);
				if (toolbar != null)
				{
					break;
				}
			}
			if (toolbar == null)
			{
				throw new NotSupportedException("Toolbar's ImageButton can be inside Toolbar control only");
			}
			bool flag = false;
			Type type2 = type;
			while (type2 != typeof(CommonButton))
			{
				if (type2 == typeof(HorizontalSeparator))
				{
					flag = true;
					break;
				}
				type2 = type2.BaseType;
			}
			if (flag)
			{
				this.NormalSrc = this.getImagePath(this.BaseImageButtonType, name, ext, toolbar);
			}
			else
			{
				this.NormalSrc = this.getImagePath(this.BaseImageButtonType, name + "-Inactive", ext, toolbar);
				this.DownSrc = this.getImagePath(this.BaseImageButtonType, name + "-Active", ext, toolbar);
			}
			bool flag2 = false;
			type2 = type.BaseType;
			while (type2 != typeof(CommonButton))
			{
				if (type2 == typeof(EditorToggleButton) || type2 == typeof(ModeButton))
				{
					flag2 = true;
					break;
				}
				type2 = type2.BaseType;
			}
			if (flag2)
			{
				this.ActiveSrc = this.DownSrc;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000131CF File Offset: 0x000113CF
		protected void RegisterButtonImages(string name)
		{
			this.RegisterButtonImages(name, "gif");
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000131DD File Offset: 0x000113DD
		internal void InternalRegisterButtonImages(string name)
		{
			this.RegisterButtonImages(name, "gif");
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000131EC File Offset: 0x000113EC
		private string getImagePath(Type type, string name, string ext, Toolbar toolbar)
		{
			string buttonImagesFolder = toolbar.ButtonImagesFolder;
			string result = ToolkitResourceManager.GetImageHref("HtmlEditor." + name + "." + ext, this, true);
			if (buttonImagesFolder.Length > 0)
			{
				string text = buttonImagesFolder + name + "." + ext;
				string text2 = string.Empty;
				if (base.IsDesign && this._designer != null)
				{
					text2 = this._designer.MapPath(text);
				}
				else
				{
					text2 = HttpContext.Current.Server.MapPath(text);
				}
				if (text2 != null && File.Exists(text2))
				{
					result = text;
				}
			}
			return result;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00013275 File Offset: 0x00011475
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute("src", this.NormalSrc);
			writer.AddAttribute("alt", string.Empty);
			base.AddAttributesToRender(writer);
		}
	}
}
