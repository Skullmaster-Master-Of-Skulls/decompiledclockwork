using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E24 RID: 3620
	public class PivotGridFieldRenderingControl : CompositeControl
	{
		// Token: 0x17002B6E RID: 11118
		// (get) Token: 0x06008934 RID: 35124 RVA: 0x001F4C09 File Offset: 0x001F2E09
		// (set) Token: 0x06008935 RID: 35125 RVA: 0x001F4C11 File Offset: 0x001F2E11
		public PivotGridField OwnerField { get; internal set; }

		// Token: 0x17002B6F RID: 11119
		// (get) Token: 0x06008936 RID: 35126 RVA: 0x001F4C1A File Offset: 0x001F2E1A
		// (set) Token: 0x06008937 RID: 35127 RVA: 0x001F4C22 File Offset: 0x001F2E22
		public LinkButton SortLinkButton { get; set; }

		// Token: 0x17002B70 RID: 11120
		// (get) Token: 0x06008938 RID: 35128 RVA: 0x001F4C2B File Offset: 0x001F2E2B
		// (set) Token: 0x06008939 RID: 35129 RVA: 0x001F4C33 File Offset: 0x001F2E33
		public Button ContextMenuButton { get; set; }

		// Token: 0x17002B71 RID: 11121
		// (get) Token: 0x0600893A RID: 35130 RVA: 0x001F4C3C File Offset: 0x001F2E3C
		// (set) Token: 0x0600893B RID: 35131 RVA: 0x001F4C44 File Offset: 0x001F2E44
		public Button SortIcon { get; set; }

		// Token: 0x17002B72 RID: 11122
		// (get) Token: 0x0600893C RID: 35132 RVA: 0x001F4C4D File Offset: 0x001F2E4D
		// (set) Token: 0x0600893D RID: 35133 RVA: 0x001F4C55 File Offset: 0x001F2E55
		public LinkButton SortLinkIcon { get; set; }

		// Token: 0x17002B73 RID: 11123
		// (get) Token: 0x0600893E RID: 35134 RVA: 0x001F4C5E File Offset: 0x001F2E5E
		// (set) Token: 0x0600893F RID: 35135 RVA: 0x001F4C66 File Offset: 0x001F2E66
		internal bool AllowShowHide { get; set; }

		// Token: 0x17002B74 RID: 11124
		// (get) Token: 0x06008940 RID: 35136 RVA: 0x001F4C6F File Offset: 0x001F2E6F
		// (set) Token: 0x06008941 RID: 35137 RVA: 0x001F4C77 File Offset: 0x001F2E77
		internal bool IsConfigurationPanelField { get; set; }

		// Token: 0x17002B75 RID: 11125
		// (get) Token: 0x06008942 RID: 35138 RVA: 0x001F4C80 File Offset: 0x001F2E80
		// (set) Token: 0x06008943 RID: 35139 RVA: 0x001F4C88 File Offset: 0x001F2E88
		public CheckBox ShowHideCheckBox { get; set; }

		// Token: 0x06008944 RID: 35140 RVA: 0x001F4C91 File Offset: 0x001F2E91
		public PivotGridFieldRenderingControl(PivotGridField ownerField, int childIndex = -1)
		{
			this.OwnerField = ownerField;
			this.ChildIndex = childIndex;
		}

		// Token: 0x17002B76 RID: 11126
		// (get) Token: 0x06008945 RID: 35141 RVA: 0x001F4CAE File Offset: 0x001F2EAE
		// (set) Token: 0x06008946 RID: 35142 RVA: 0x001F4CB6 File Offset: 0x001F2EB6
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = this.GetXhtmlValidId(value);
			}
		}

		// Token: 0x06008947 RID: 35143 RVA: 0x001F4CC8 File Offset: 0x001F2EC8
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.IsConfigurationPanelField)
			{
				if (string.IsNullOrEmpty(this.OwnerField.Caption))
				{
					this.ToolTip = this.OwnerField.DataField;
				}
				else
				{
					this.ToolTip = this.OwnerField.Caption;
				}
			}
			if (this.AllowShowHide)
			{
				this.ShowHideCheckBox = new CheckBox();
				this.ShowHideCheckBox.ID = string.Format(CultureInfo.InvariantCulture, "ShowHide", new object[0]);
				this.ShowHideCheckBox.Checked = !this.OwnerField.IsHidden;
				this.Controls.Add(this.ShowHideCheckBox);
				this.ShowHideCheckBox.Attributes["onclick"] = string.Format("$find('{0}').{1}();", this.ClientID, this.OwnerField.IsHidden ? "show" : "hide");
				AccessibilityHelper.AddToolTip(this.ShowHideCheckBox, this.OwnerField.Owner.ConfigurationPanelSettings.ShowHideCheckBoxToolTip);
			}
			if (this.ShoudCreateSortControls())
			{
				this.SortLinkButton = new LinkButton();
				this.SortLinkButton.ID = this.GetXhtmlValidId("LinkButton_" + this.OwnerField.UniqueName);
				string s = string.IsNullOrEmpty(this.OwnerField.Caption) ? this.OwnerField.DataField : this.OwnerField.Caption;
				this.SortLinkButton.Text = HttpUtility.HtmlEncode(s);
				this.SortLinkButton.CausesValidation = false;
				this.SortLinkButton.CommandName = "Sort";
				this.SortLinkButton.CommandArgument = this.OwnerField.UniqueName;
				this.Controls.Add(this.SortLinkButton);
				if (this.OwnerField.Owner.ResolvedRenderMode == RenderMode.Lightweight)
				{
					this.SortLinkIcon = new LinkButton();
					this.SortLinkIcon.ID = this.GetXhtmlValidId(string.Format(CultureInfo.InvariantCulture, "{0}_SortLinkButton", new object[]
					{
						this.OwnerField.UniqueName
					}));
					this.SortLinkIcon.CausesValidation = false;
					this.SortLinkIcon.CommandName = "Sort";
					this.SortLinkIcon.CommandArgument = this.OwnerField.UniqueName;
					if (this.OwnerField.Owner.EnableAriaSupport)
					{
						this.SortLinkIcon.Attributes.Add("aria-label", this.SortLinkIcon.ToolTip);
					}
					this.Controls.Add(this.SortLinkIcon);
				}
				else
				{
					this.SortIcon = new Button();
					this.SortIcon.ID = this.GetXhtmlValidId(string.Format(CultureInfo.InvariantCulture, "{0}_SortIconButton", new object[]
					{
						this.OwnerField.UniqueName
					}));
					this.SortIcon.CausesValidation = false;
					this.SortIcon.CommandName = "Sort";
					this.SortIcon.CommandArgument = this.OwnerField.UniqueName;
					this.Controls.Add(this.SortIcon);
				}
			}
			else if (this.OwnerField.Owner.ResolvedRenderMode == RenderMode.Lightweight)
			{
				Label label = new Label();
				string s2 = (this.OwnerField.Caption != string.Empty) ? this.OwnerField.Caption : this.OwnerField.DataField;
				label.Text = HttpUtility.HtmlEncode(s2);
				label.ToolTip = label.Text;
				if (this.OwnerField.Owner.EnableAriaSupport)
				{
					label.Attributes.Add("aria-label", label.ToolTip);
				}
				this.Controls.Add(label);
			}
			else
			{
				Literal literal = new Literal
				{
					Text = ((this.OwnerField.Caption != string.Empty) ? this.OwnerField.Caption : this.OwnerField.DataField)
				};
				literal.Text = HttpUtility.HtmlEncode(literal.Text);
				this.Controls.Add(literal);
			}
			if (this.ShouldCreateContextMenuIcon())
			{
				if (this.OwnerField.Owner.ResolvedRenderMode == RenderMode.Lightweight)
				{
					LinkButton linkButton = new LinkButton();
					linkButton.ID = this.GetXhtmlValidId(string.Format(CultureInfo.InvariantCulture, "{0}_ContextMenuButton", new object[]
					{
						this.OwnerField.UniqueName
					}));
					linkButton.CssClass = "rpgIcon rpgItemContextMenuIcon";
					linkButton.OnClientClick = "return false;";
					if (this.OwnerField.Owner.EnableAriaSupport)
					{
						linkButton.Attributes.Add("aria-label", linkButton.ToolTip);
					}
					this.Controls.Add(linkButton);
				}
				else
				{
					this.ContextMenuButton = new Button();
					this.ContextMenuButton.ID = this.GetXhtmlValidId(string.Format(CultureInfo.InvariantCulture, "{0}_ContextMenuButton", new object[]
					{
						this.OwnerField.UniqueName
					}));
					this.ContextMenuButton.CssClass = "rpgItemContextMenu";
					this.ContextMenuButton.OnClientClick = "return false;";
					this.Controls.Add(this.ContextMenuButton);
				}
			}
			if (this.ShouldCreateFilterControls() && !(this.OwnerField is PivotGridAggregateField) && !this.OwnerField.IsHidden)
			{
				if (this.OwnerField.Owner.ResolvedRenderMode == RenderMode.Lightweight)
				{
					LinkButton linkButton2 = new LinkButton();
					linkButton2.ID = this.GetXhtmlValidId(string.Format(CultureInfo.InvariantCulture, "{0}_FilterPictButton", new object[]
					{
						this.OwnerField.UniqueName
					}));
					linkButton2.CssClass = "rpgIcon rpgFilterIcon";
					if (this.OwnerField.UniqueName == this.OwnerField.Owner.FilteringManager.FieldUniqueName)
					{
						linkButton2.OnClientClick = string.Format("$find('{0}')._filtering.displayWindowOverFieldNoPostBack(event); return false;", this.OwnerField.Owner.ClientID, this.OwnerField.UniqueName);
					}
					else
					{
						linkButton2.CommandName = "InitFilterDialogue";
						linkButton2.CommandArgument = this.OwnerField.UniqueName;
						if (this.ChildIndex >= 0)
						{
							linkButton2.CommandArgument = this.OwnerField.FlatChildOlapInfoNames[this.ChildIndex].Replace(" ", string.Empty);
						}
					}
					linkButton2.ToolTip = this.OwnerField.Owner.Localization.OpenFilterWindowTooltip;
					if (this.OwnerField.Owner.EnableAriaSupport)
					{
						linkButton2.Attributes.Add("aria-label", linkButton2.ToolTip);
					}
					this.Controls.Add(linkButton2);
					return;
				}
				Button button = new Button();
				button.ID = this.GetXhtmlValidId(string.Format(CultureInfo.InvariantCulture, "{0}_FilterPictButton", new object[]
				{
					this.OwnerField.UniqueName
				}));
				button.CssClass = "rpgFilter";
				if (this.OwnerField.UniqueName == this.OwnerField.Owner.FilteringManager.FieldUniqueName)
				{
					button.OnClientClick = string.Format("$find('{0}')._filtering.displayWindowOverFieldNoPostBack(event); return false;", this.OwnerField.Owner.ClientID, this.OwnerField.UniqueName);
				}
				else
				{
					button.CommandName = "InitFilterDialogue";
					button.CommandArgument = this.OwnerField.UniqueName;
					if (this.ChildIndex >= 0)
					{
						button.CommandArgument = this.OwnerField.FlatChildOlapInfoNames[this.ChildIndex].Replace(" ", string.Empty);
					}
				}
				button.ToolTip = this.OwnerField.Owner.Localization.OpenFilterWindowTooltip;
				button.Text = this.OwnerField.Owner.Localization.OpenFilterWindowTooltip;
				this.Controls.Add(button);
			}
		}

		// Token: 0x06008948 RID: 35144 RVA: 0x001F54CC File Offset: 0x001F36CC
		private bool ShoudCreateSortControls()
		{
			return this.OwnerField.Owner.AllowSorting && (this.OwnerField is PivotGridRowField || this.OwnerField is PivotGridColumnField) && ((!this.OwnerField.IsHidden && !this.AllowShowHide) || base.Style[HtmlTextWriterStyle.Display] == "none");
		}

		// Token: 0x06008949 RID: 35145 RVA: 0x001F5536 File Offset: 0x001F3736
		private bool ShouldCreateFilterControls()
		{
			return this.OwnerField.Owner.AllowFiltering;
		}

		// Token: 0x0600894A RID: 35146 RVA: 0x001F5548 File Offset: 0x001F3748
		private bool ShouldCreateContextMenuIcon()
		{
			return this.OwnerField.Owner.ConfigurationPanelSettings.EnableFieldsContextMenu && this.IsConfigurationPanelField && !this.AllowShowHide;
		}

		// Token: 0x0600894B RID: 35147 RVA: 0x001F5574 File Offset: 0x001F3774
		private string GetXhtmlValidId(string id)
		{
			string[] array = id.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = Regex.Replace(array[i], "[^a-zA-Z0-9_]", string.Empty);
				if (string.IsNullOrEmpty(text))
				{
					text = string.Format("{0:X8}", array[i].GetHashCode());
				}
				array[i] = text;
			}
			return string.Join("_", array);
		}

		// Token: 0x0600894C RID: 35148 RVA: 0x001F55E5 File Offset: 0x001F37E5
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		// Token: 0x0600894D RID: 35149 RVA: 0x001F55F0 File Offset: 0x001F37F0
		public void PrepareFieldRenderingControlStyle()
		{
			PivotGridFieldDecorator pivotGridFieldDecorator = new PivotGridFieldDecorator(this);
			pivotGridFieldDecorator.DecorateControl();
		}

		// Token: 0x0400265A RID: 9818
		internal int ChildIndex = -1;
	}
}
