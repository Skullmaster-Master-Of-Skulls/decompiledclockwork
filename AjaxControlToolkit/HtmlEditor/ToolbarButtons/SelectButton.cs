using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000101 RID: 257
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.SelectButton", "HtmlEditor.ToolbarButtons.SelectButton")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class SelectButton : CommonButton
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x000136E8 File Offset: 0x000118E8
		public SelectButton() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x000136F2 File Offset: 0x000118F2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Collection<SelectOption> Options
		{
			get
			{
				if (this._options == null)
				{
					this._options = new Collection<SelectOption>();
				}
				return this._options;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001370D File Offset: 0x0001190D
		[DefaultValue("")]
		[Category("Appearance")]
		public virtual string SelectWidth
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00013714 File Offset: 0x00011914
		[DefaultValue("")]
		[Category("Appearance")]
		public virtual string DefaultValue
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001371B File Offset: 0x0001191B
		[DefaultValue(true)]
		[Category("Appearance")]
		public virtual bool UseDefaultValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00013720 File Offset: 0x00011920
		protected override void CreateChildControls()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("nobr");
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			htmlGenericControl2.Attributes.Add("class", "ajax__htmleditor_toolbar_selectlable");
			htmlGenericControl2.ID = "label";
			htmlGenericControl2.Controls.Add(new LiteralControl(base.GetFromResource("label") + "&nbsp;"));
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("select");
			htmlGenericControl3.Attributes.Add("class", "ajax__htmleditor_toolbar_selectbutton");
			htmlGenericControl3.ID = "select";
			if (!string.IsNullOrEmpty(this.SelectWidth))
			{
				htmlGenericControl3.Style[HtmlTextWriterStyle.Width] = this.SelectWidth;
			}
			if (base.IgnoreTab)
			{
				htmlGenericControl3.Attributes.Add("tabindex", "-1");
			}
			htmlGenericControl.Controls.Add(htmlGenericControl3);
			if (this.UseDefaultValue)
			{
				htmlGenericControl3.Controls.Add(new LiteralControl(string.Concat(new string[]
				{
					"<option value=\"",
					this.DefaultValue,
					"\">",
					base.GetFromResource("defaultValue"),
					"</option>"
				})));
			}
			for (int i = 0; i < this.Options.Count; i++)
			{
				htmlGenericControl3.Controls.Add(new LiteralControl(string.Concat(new string[]
				{
					"<option value=\"",
					this.Options[i].Value,
					"\">",
					this.Options[i].Text,
					"</option>"
				})));
			}
			this.Controls.Add(htmlGenericControl);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000138E9 File Offset: 0x00011AE9
		protected override Style CreateControlStyle()
		{
			return new SelectButton.SelectButtonStyle(this.ViewState);
		}

		// Token: 0x0400030A RID: 778
		private Collection<SelectOption> _options;

		// Token: 0x02000102 RID: 258
		private sealed class SelectButtonStyle : Style
		{
			// Token: 0x0600070C RID: 1804 RVA: 0x000138F6 File Offset: 0x00011AF6
			public SelectButtonStyle(StateBag state) : base(state)
			{
			}

			// Token: 0x0600070D RID: 1805 RVA: 0x000138FF File Offset: 0x00011AFF
			protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
			{
				base.FillStyleAttributes(attributes, urlResolver);
				attributes.Add("background-color", "transparent");
				attributes.Add("cursor", "text");
			}
		}
	}
}
