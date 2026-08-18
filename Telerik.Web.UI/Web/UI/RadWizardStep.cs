using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200099E RID: 2462
	[Designer("Telerik.Web.Design.RadWizardStepDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ParseChildren(false)]
	[PersistChildren(true)]
	[ToolboxItem(false)]
	public class RadWizardStep : WebControl
	{
		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x0011EFF2 File Offset: 0x0011D1F2
		internal virtual RadWizard Owner
		{
			get
			{
				return this.Wizard;
			}
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0011EFFA File Offset: 0x0011D1FA
		internal string TitleInternal
		{
			get
			{
				return (string)this.ViewState["Title"];
			}
		}

		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x0011F014 File Offset: 0x0011D214
		internal string CurrentImageUrl
		{
			get
			{
				if (!this.Enabled && !string.IsNullOrEmpty(this.DisabledImageUrl))
				{
					return base.ResolveClientUrl(this.DisabledImageUrl);
				}
				if (this.Active && !string.IsNullOrEmpty(this.ActiveImageUrl))
				{
					return base.ResolveClientUrl(this.ActiveImageUrl);
				}
				if (!string.IsNullOrEmpty(this.ImageUrl))
				{
					return base.ResolveClientUrl(this.ImageUrl);
				}
				return null;
			}
		}

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x0011F080 File Offset: 0x0011D280
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x0011F084 File Offset: 0x0011D284
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.EnsureID();
			if (this.Owner == null)
			{
				throw new NotSupportedException("RadWizardStep must be added in a RadWizard control");
			}
			string text = "rwzStep";
			if (this.Active)
			{
				text = RadWizard.Styles.Combine(new string[]
				{
					text,
					"rwzActive"
				});
			}
			if (!this.Enabled)
			{
				text = RadWizard.Styles.Combine(new string[]
				{
					text,
					"rwzDisabled"
				});
			}
			if (this.CssClass != string.Empty)
			{
				text = RadWizard.Styles.Combine(new string[]
				{
					text,
					this.CssClass
				});
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			string toolTip = this.ToolTip;
			this.ToolTip = string.Empty;
			base.AddAttributesToRender(writer);
			this.ToolTip = toolTip;
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x0011F14E File Offset: 0x0011D34E
		protected internal bool ResolvedDisplayCancelButton
		{
			get
			{
				if (this.ViewState["DisplayCancelButton"] == null && this.Owner != null)
				{
					return this.Owner.DisplayCancelButton;
				}
				return this.DisplayCancelButton;
			}
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x0011F17C File Offset: 0x0011D37C
		public RadWizard Wizard
		{
			get
			{
				return this.Parent as RadWizard;
			}
		}

		// Token: 0x17001EE8 RID: 7912
		// (get) Token: 0x06005DD9 RID: 24025 RVA: 0x0011F189 File Offset: 0x0011D389
		// (set) Token: 0x06005DDA RID: 24026 RVA: 0x0011F1A8 File Offset: 0x0011D3A8
		[DefaultValue(false)]
		[Description("Specifies if current RadWizardStep is Active")]
		public bool Active
		{
			get
			{
				return this.Wizard != null && this.Wizard.ActiveStepIndex == this.Index;
			}
			set
			{
				if (this.Wizard == null)
				{
					this.cashedActive = value;
					return;
				}
				this.Wizard.ActiveStepIndex = (value ? this.Index : -1);
			}
		}

		// Token: 0x17001EE9 RID: 7913
		// (get) Token: 0x06005DDB RID: 24027 RVA: 0x0011F1D1 File Offset: 0x0011D3D1
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
		}

		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x06005DDC RID: 24028 RVA: 0x0011F1D9 File Offset: 0x0011D3D9
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
		}

		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x06005DDD RID: 24029 RVA: 0x0011F1E4 File Offset: 0x0011D3E4
		// (set) Token: 0x06005DDE RID: 24030 RVA: 0x0011F20D File Offset: 0x0011D40D
		[DefaultValue(RadWizardStepType.Auto)]
		public virtual RadWizardStepType StepType
		{
			get
			{
				object obj = this.ViewState["StepType"];
				if (obj != null)
				{
					return (RadWizardStepType)obj;
				}
				return RadWizardStepType.Auto;
			}
			set
			{
				if (value < RadWizardStepType.Auto || value > RadWizardStepType.Step)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.StepType != value)
				{
					this.ViewState["StepType"] = value;
				}
			}
		}

		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x0011F244 File Offset: 0x0011D444
		// (set) Token: 0x06005DE0 RID: 24032 RVA: 0x0011F271 File Offset: 0x0011D471
		[DefaultValue("")]
		public virtual string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (this.Title != value)
				{
					this.ViewState["Title"] = value;
				}
			}
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x06005DE1 RID: 24033 RVA: 0x0011F292 File Offset: 0x0011D492
		// (set) Token: 0x06005DE2 RID: 24034 RVA: 0x0011F2B2 File Offset: 0x0011D4B2
		[Description("The URL of the image displayed for the step.")]
		[Localizable(true)]
		[DefaultValue("")]
		[UrlProperty]
		[Category("Appearance")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string ImageUrl
		{
			get
			{
				return (string)(this.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x0011F2C5 File Offset: 0x0011D4C5
		// (set) Token: 0x06005DE4 RID: 24036 RVA: 0x0011F2E5 File Offset: 0x0011D4E5
		[UrlProperty]
		[Description("The URL of the image displayed for the step when it is hovered.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		public string HoveredImageUrl
		{
			get
			{
				return (string)(this.ViewState["HoveredImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x06005DE5 RID: 24037 RVA: 0x0011F2F8 File Offset: 0x0011D4F8
		// (set) Token: 0x06005DE6 RID: 24038 RVA: 0x0011F318 File Offset: 0x0011D518
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Localizable(true)]
		[Description("The URL of the image displayed for the step when it is active.")]
		[UrlProperty]
		[Category("Appearance")]
		[DefaultValue("")]
		public string ActiveImageUrl
		{
			get
			{
				return (string)(this.ViewState["ActiveImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ActiveImageUrl"] = value;
			}
		}

		// Token: 0x17001EF0 RID: 7920
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x0011F32B File Offset: 0x0011D52B
		// (set) Token: 0x06005DE8 RID: 24040 RVA: 0x0011F34B File Offset: 0x0011D54B
		[Category("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[UrlProperty]
		[Description("The URL of the image displayed for the step when it is disabled.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string DisabledImageUrl
		{
			get
			{
				return (string)(this.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x17001EF1 RID: 7921
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x0011F35E File Offset: 0x0011D55E
		// (set) Token: 0x06005DEA RID: 24042 RVA: 0x0011F37E File Offset: 0x0011D57E
		[Category("Appearance")]
		[Description("The CSS that is used in sprite image scenarios.")]
		[DefaultValue("")]
		public string SpriteCssClass
		{
			get
			{
				return (string)(this.ViewState["SpriteCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SpriteCssClass"] = value;
			}
		}

		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x06005DEB RID: 24043 RVA: 0x0011F394 File Offset: 0x0011D594
		// (set) Token: 0x06005DEC RID: 24044 RVA: 0x0011F3BD File Offset: 0x0011D5BD
		[DefaultValue(true)]
		public virtual bool AllowReturn
		{
			get
			{
				object obj = this.ViewState["AllowReturn"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AllowReturn"] = value;
			}
		}

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x06005DED RID: 24045 RVA: 0x0011F3D5 File Offset: 0x0011D5D5
		// (set) Token: 0x06005DEE RID: 24046 RVA: 0x0011F3F5 File Offset: 0x0011D5F5
		[DefaultValue("")]
		public override string ToolTip
		{
			get
			{
				return (string)(this.ViewState["ToolTip"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x06005DEF RID: 24047 RVA: 0x0011F408 File Offset: 0x0011D608
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.Wizard == null)
				{
					return -1;
				}
				return this.Wizard.WizardSteps.IndexOf(this);
			}
		}

		// Token: 0x17001EF5 RID: 7925
		// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x0011F428 File Offset: 0x0011D628
		// (set) Token: 0x06005DF1 RID: 24049 RVA: 0x0011F451 File Offset: 0x0011D651
		[NotifyParentProperty(true)]
		[Description("Specifies whether Cancel button should be displayed in the RadWizardStep.")]
		[DefaultValue(false)]
		public virtual bool DisplayCancelButton
		{
			get
			{
				object obj = this.ViewState["DisplayCancelButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DisplayCancelButton"] = value;
			}
		}

		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x06005DF2 RID: 24050 RVA: 0x0011F46C File Offset: 0x0011D66C
		// (set) Token: 0x06005DF3 RID: 24051 RVA: 0x0011F499 File Offset: 0x0011D699
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				string text = (string)this.ViewState["ValidationGroup"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x0011F4AC File Offset: 0x0011D6AC
		// (set) Token: 0x06005DF5 RID: 24053 RVA: 0x0011F4D5 File Offset: 0x0011D6D5
		[DefaultValue(true)]
		[Category("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = this.ViewState["CausesValidation"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x040016A9 RID: 5801
		internal bool cashedActive;
	}
}
