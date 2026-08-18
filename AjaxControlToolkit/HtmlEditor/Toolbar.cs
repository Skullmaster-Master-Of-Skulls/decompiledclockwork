using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.UI;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;

namespace AjaxControlToolkit.HtmlEditor
{
	// Token: 0x020000EB RID: 235
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Toolbar", "HtmlEditor.Toolbar")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class Toolbar : ScriptControlBase
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x000129DF File Offset: 0x00010BDF
		protected Toolbar() : base(false, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x000129EC File Offset: 0x00010BEC
		protected bool IsDesign
		{
			get
			{
				bool result;
				try
				{
					bool flag = this.Context == null || (base.Site != null && base.Site.DesignMode);
					result = flag;
				}
				catch
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00012A3C File Offset: 0x00010C3C
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00012A5D File Offset: 0x00010C5D
		[ClientPropertyName("alwaysVisible")]
		[Category("Behavior")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool AlwaysVisible
		{
			get
			{
				return (bool)(this.ViewState["AlwaysVisible"] ?? false);
			}
			set
			{
				this.ViewState["AlwaysVisible"] = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00012A75 File Offset: 0x00010C75
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00012A90 File Offset: 0x00010C90
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Collection<CommonButton> Buttons
		{
			get
			{
				if (this._buttons == null)
				{
					this._buttons = new Collection<CommonButton>();
				}
				return this._buttons;
			}
			internal set
			{
				this._buttons = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00012A9C File Offset: 0x00010C9C
		[Browsable(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("buttonIds")]
		public string ButtonIds
		{
			get
			{
				string text = string.Empty;
				for (int i = 0; i < this.Buttons.Count; i++)
				{
					if (i > 0)
					{
						text += ";";
					}
					text += this.Buttons[i].ClientID;
				}
				return text;
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00012AEE File Offset: 0x00010CEE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeButtonIds()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00012AF6 File Offset: 0x00010CF6
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x00012B18 File Offset: 0x00010D18
		[DefaultValue("")]
		[Description("Folder used for toolbar's buttons' images")]
		[Category("Appearance")]
		public string ButtonImagesFolder
		{
			get
			{
				return (string)(this.ViewState["ButtonImagesFolder"] ?? string.Empty);
			}
			set
			{
				string text = this.LocalResolveUrl(value);
				if (text.Length > 0)
				{
					string a = text.Substring(text.Length - 1, 1);
					if (a != "\\" && a != "/")
					{
						text += "/";
					}
					this.ViewState["ButtonImagesFolder"] = text;
				}
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00012B80 File Offset: 0x00010D80
		protected string LocalResolveUrl(string path)
		{
			string input = base.ResolveUrl(path);
			Regex regex = new Regex("(\\(S\\([A-Za-z0-9_]+\\)\\)/)", RegexOptions.Compiled);
			return regex.Replace(input, string.Empty);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00012BB0 File Offset: 0x00010DB0
		protected override void CreateChildControls()
		{
			for (int i = 0; i < this.Buttons.Count; i++)
			{
				this.Controls.Add(this.Buttons[i]);
				if (!this.AlwaysVisible && !this.IsDesign)
				{
					if (!this.Buttons[i].PreservePlace)
					{
						this.Buttons[i].Style[HtmlTextWriterStyle.Display] = "none";
					}
					else
					{
						this.Buttons[i].Style[HtmlTextWriterStyle.Visibility] = "hidden";
					}
				}
				for (int j = 0; j < this.Buttons[i].ExportedControls.Count; j++)
				{
					this.Controls.Add(this.Buttons[i].ExportedControls[j]);
				}
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00012C94 File Offset: 0x00010E94
		protected override void OnPreRender(EventArgs e)
		{
			try
			{
				base.OnPreRender(e);
			}
			catch
			{
			}
			this._wasPreRender = true;
			for (int i = 0; i < this.Controls.Count; i++)
			{
				CommonButton commonButton = this.Controls[i] as CommonButton;
				if (commonButton != null)
				{
					if (!this.IsDesign)
					{
						if (!commonButton.PreservePlace)
						{
							commonButton.Style[HtmlTextWriterStyle.Display] = "none";
						}
						else
						{
							commonButton.Style[HtmlTextWriterStyle.Visibility] = "hidden";
						}
					}
					else
					{
						commonButton.Style.Remove(HtmlTextWriterStyle.Display);
						commonButton.Style.Remove(HtmlTextWriterStyle.Visibility);
					}
				}
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00012D44 File Offset: 0x00010F44
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this._wasPreRender)
			{
				this.OnPreRender(new EventArgs());
			}
			base.Render(writer);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00012D60 File Offset: 0x00010F60
		internal void CreateChilds(DesignerWithMapPath designer)
		{
			this.Controls.Clear();
			this.CreateChildControls();
			for (int i = 0; i < this.Controls.Count; i++)
			{
				CommonButton commonButton = this.Controls[i] as CommonButton;
				if (commonButton != null)
				{
					commonButton.CreateChilds(designer);
				}
			}
		}

		// Token: 0x040002FE RID: 766
		private Collection<CommonButton> _buttons;

		// Token: 0x040002FF RID: 767
		private bool _wasPreRender;
	}
}
