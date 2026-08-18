using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000515 RID: 1301
	[Bindable(false)]
	[DefaultEvent("FinishButtonClick")]
	[Designer("System.Web.UI.Design.WebControls.WizardDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:Wizard runat=\"server\"> <WizardSteps> <asp:WizardStep title=\"Step 1\" runat=\"server\"></asp:WizardStep> <asp:WizardStep title=\"Step 2\" runat=\"server\"></asp:WizardStep> </WizardSteps> </{0}:Wizard>")]
	public class Wizard : CompositeControl
	{
		// Token: 0x06004174 RID: 16756 RVA: 0x000D6311 File Offset: 0x000D4511
		public Wizard() : this(true)
		{
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x000D631A File Offset: 0x000D451A
		internal Wizard(bool displaySideBarDefault)
		{
			this._displaySideBarDefault = displaySideBarDefault;
			this._displaySideBar = displaySideBarDefault;
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x000D6330 File Offset: 0x000D4530
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Wizard_ActiveStep")]
		public WizardStepBase ActiveStep
		{
			get
			{
				if (this.ActiveStepIndex < -1 || this.ActiveStepIndex >= this.WizardSteps.Count)
				{
					throw new InvalidOperationException(SR.GetString("Wizard_ActiveStepIndex_out_of_range"));
				}
				return this.MultiView.GetActiveView() as WizardStepBase;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000D636E File Offset: 0x000D456E
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x000D637C File Offset: 0x000D457C
		[DefaultValue(-1)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Wizard_ActiveStepIndex")]
		public virtual int ActiveStepIndex
		{
			get
			{
				return this.MultiView.ActiveViewIndex;
			}
			set
			{
				if (value < -1 || (value >= this.WizardSteps.Count && base.ControlState >= ControlState.FrameworkInitialized))
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Wizard_ActiveStepIndex_out_of_range"));
				}
				if (this.MultiView.ActiveViewIndex != value)
				{
					this.MultiView.ActiveViewIndex = value;
					this._activeStepIndexSet = true;
					if (this._sideBarList != null && this.SideBarTemplate != null)
					{
						this._sideBarList.SelectedIndex = this.ActiveStepIndex;
						this._sideBarList.DataBind();
					}
				}
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x000D6408 File Offset: 0x000D4608
		// (set) Token: 0x0600417A RID: 16762 RVA: 0x0008AF4D File Offset: 0x0008914D
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_CancelButtonImageUrl")]
		[UrlProperty]
		public virtual string CancelButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["CancelButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CancelButtonImageUrl"] = value;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x000D6435 File Offset: 0x000D4635
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_CancelButtonStyle")]
		public Style CancelButtonStyle
		{
			get
			{
				if (this._cancelButtonStyle == null)
				{
					this._cancelButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._cancelButtonStyle).TrackViewState();
					}
				}
				return this._cancelButtonStyle;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x000D6464 File Offset: 0x000D4664
		// (set) Token: 0x0600417D RID: 16765 RVA: 0x000D6496 File Offset: 0x000D4696
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_CancelButtonText")]
		[WebSysDescription("Wizard_CancelButtonText")]
		public virtual string CancelButtonText
		{
			get
			{
				string text = this.ViewState["CancelButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_CancelButtonText");
			}
			set
			{
				if (value != this.CancelButtonText)
				{
					this.ViewState["CancelButtonText"] = value;
				}
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x000D64B8 File Offset: 0x000D46B8
		// (set) Token: 0x0600417F RID: 16767 RVA: 0x000D64E1 File Offset: 0x000D46E1
		[DefaultValue(ButtonType.Button)]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_CancelButtonType")]
		public virtual ButtonType CancelButtonType
		{
			get
			{
				object obj = this.ViewState["CancelButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				Wizard.ValidateButtonType(value);
				this.ViewState["CancelButtonType"] = value;
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004180 RID: 16768 RVA: 0x000D6500 File Offset: 0x000D4700
		// (set) Token: 0x06004181 RID: 16769 RVA: 0x0008B059 File Offset: 0x00089259
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Wizard_CancelDestinationPageUrl")]
		[UrlProperty]
		public virtual string CancelDestinationPageUrl
		{
			get
			{
				string text = this.ViewState["CancelDestinationPageUrl"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CancelDestinationPageUrl"] = value;
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x000D652D File Offset: 0x000D472D
		// (set) Token: 0x06004183 RID: 16771 RVA: 0x00085688 File Offset: 0x00083888
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("Wizard_CellPadding")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 0;
				}
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x0008569B File Offset: 0x0008389B
		// (set) Token: 0x06004185 RID: 16773 RVA: 0x000856B7 File Offset: 0x000838B7
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("Wizard_CellSpacing")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 0;
				}
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x000D654C File Offset: 0x000D474C
		// (set) Token: 0x06004187 RID: 16775 RVA: 0x000D6575 File Offset: 0x000D4775
		[DefaultValue(false)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Wizard_DisplayCancelButton")]
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

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06004188 RID: 16776 RVA: 0x000D658D File Offset: 0x000D478D
		// (set) Token: 0x06004189 RID: 16777 RVA: 0x000D6595 File Offset: 0x000D4795
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Wizard_DisplaySideBar")]
		public virtual bool DisplaySideBar
		{
			get
			{
				return this._displaySideBar;
			}
			set
			{
				if (value != this._displaySideBar)
				{
					this._displaySideBar = value;
					this._sideBarTableCell = null;
					this.RequiresControlsRecreation();
				}
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x0600418A RID: 16778 RVA: 0x000D65B4 File Offset: 0x000D47B4
		// (set) Token: 0x0600418B RID: 16779 RVA: 0x000D65E1 File Offset: 0x000D47E1
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_FinishCompleteButtonImageUrl")]
		[UrlProperty]
		public virtual string FinishCompleteButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FinishCompleteButtonImageUrl"] = value;
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x000D65F4 File Offset: 0x000D47F4
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_FinishCompleteButtonStyle")]
		public Style FinishCompleteButtonStyle
		{
			get
			{
				if (this._finishCompleteButtonStyle == null)
				{
					this._finishCompleteButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._finishCompleteButtonStyle).TrackViewState();
					}
				}
				return this._finishCompleteButtonStyle;
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x000D6624 File Offset: 0x000D4824
		// (set) Token: 0x0600418E RID: 16782 RVA: 0x000D6656 File Offset: 0x000D4856
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_FinishButtonText")]
		[WebSysDescription("Wizard_FinishCompleteButtonText")]
		public virtual string FinishCompleteButtonText
		{
			get
			{
				string text = this.ViewState["FinishCompleteButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_FinishButtonText");
			}
			set
			{
				this.ViewState["FinishCompleteButtonText"] = value;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x000D666C File Offset: 0x000D486C
		// (set) Token: 0x06004190 RID: 16784 RVA: 0x000D6695 File Offset: 0x000D4895
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Wizard_FinishCompleteButtonType")]
		public virtual ButtonType FinishCompleteButtonType
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				Wizard.ValidateButtonType(value);
				this.ViewState["FinishCompleteButtonType"] = value;
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x000D66B4 File Offset: 0x000D48B4
		// (set) Token: 0x06004192 RID: 16786 RVA: 0x000D66E1 File Offset: 0x000D48E1
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Wizard_FinishDestinationPageUrl")]
		[UrlProperty]
		public virtual string FinishDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["FinishDestinationPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FinishDestinationPageUrl"] = value;
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x000D66F4 File Offset: 0x000D48F4
		// (set) Token: 0x06004194 RID: 16788 RVA: 0x000D6721 File Offset: 0x000D4921
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_FinishPreviousButtonImageUrl")]
		[UrlProperty]
		public virtual string FinishPreviousButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FinishPreviousButtonImageUrl"] = value;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x000D6734 File Offset: 0x000D4934
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_FinishPreviousButtonStyle")]
		public Style FinishPreviousButtonStyle
		{
			get
			{
				if (this._finishPreviousButtonStyle == null)
				{
					this._finishPreviousButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._finishPreviousButtonStyle).TrackViewState();
					}
				}
				return this._finishPreviousButtonStyle;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x000D6764 File Offset: 0x000D4964
		// (set) Token: 0x06004197 RID: 16791 RVA: 0x000D6796 File Offset: 0x000D4996
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_StepPreviousButtonText")]
		[WebSysDescription("Wizard_FinishPreviousButtonText")]
		public virtual string FinishPreviousButtonText
		{
			get
			{
				string text = this.ViewState["FinishPreviousButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_StepPreviousButtonText");
			}
			set
			{
				this.ViewState["FinishPreviousButtonText"] = value;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x000D67AC File Offset: 0x000D49AC
		// (set) Token: 0x06004199 RID: 16793 RVA: 0x000D67D5 File Offset: 0x000D49D5
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Wizard_FinishPreviousButtonType")]
		public virtual ButtonType FinishPreviousButtonType
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				Wizard.ValidateButtonType(value);
				this.ViewState["FinishPreviousButtonType"] = value;
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x0600419A RID: 16794 RVA: 0x000D67F3 File Offset: 0x000D49F3
		// (set) Token: 0x0600419B RID: 16795 RVA: 0x000D67FB File Offset: 0x000D49FB
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("Wizard_FinishNavigationTemplate")]
		public virtual ITemplate FinishNavigationTemplate
		{
			get
			{
				return this._finishNavigationTemplate;
			}
			set
			{
				this._finishNavigationTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x000D680A File Offset: 0x000D4A0A
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("WebControl_HeaderStyle")]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000D6838 File Offset: 0x000D4A38
		// (set) Token: 0x0600419E RID: 16798 RVA: 0x000D6840 File Offset: 0x000D4A40
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("WebControl_HeaderTemplate")]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x000D6850 File Offset: 0x000D4A50
		// (set) Token: 0x060041A0 RID: 16800 RVA: 0x000A0A1D File Offset: 0x0009EC1D
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_HeaderText")]
		public virtual string HeaderText
		{
			get
			{
				string text = this.ViewState["HeaderText"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x000D687D File Offset: 0x000D4A7D
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x000D6885 File Offset: 0x000D4A85
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("Wizard_LayoutTemplate")]
		public virtual ITemplate LayoutTemplate
		{
			get
			{
				return this._layoutTemplate;
			}
			set
			{
				this._layoutTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000D6894 File Offset: 0x000D4A94
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_NavigationButtonStyle")]
		public Style NavigationButtonStyle
		{
			get
			{
				if (this._navigationButtonStyle == null)
				{
					this._navigationButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._navigationButtonStyle).TrackViewState();
					}
				}
				return this._navigationButtonStyle;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x000D68C2 File Offset: 0x000D4AC2
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_NavigationStyle")]
		public TableItemStyle NavigationStyle
		{
			get
			{
				if (this._navigationStyle == null)
				{
					this._navigationStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._navigationStyle).TrackViewState();
					}
				}
				return this._navigationStyle;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000D68F0 File Offset: 0x000D4AF0
		// (set) Token: 0x060041A6 RID: 16806 RVA: 0x000D691D File Offset: 0x000D4B1D
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_StartNextButtonImageUrl")]
		[UrlProperty]
		public virtual string StartNextButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StartNextButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["StartNextButtonImageUrl"] = value;
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x000D6930 File Offset: 0x000D4B30
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_StartNextButtonStyle")]
		public Style StartNextButtonStyle
		{
			get
			{
				if (this._startNextButtonStyle == null)
				{
					this._startNextButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._startNextButtonStyle).TrackViewState();
					}
				}
				return this._startNextButtonStyle;
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x000D6960 File Offset: 0x000D4B60
		// (set) Token: 0x060041A9 RID: 16809 RVA: 0x000D6992 File Offset: 0x000D4B92
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_StepNextButtonText")]
		[WebSysDescription("Wizard_StartNextButtonText")]
		public virtual string StartNextButtonText
		{
			get
			{
				string text = this.ViewState["StartNextButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_StepNextButtonText");
			}
			set
			{
				this.ViewState["StartNextButtonText"] = value;
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x000D69A8 File Offset: 0x000D4BA8
		// (set) Token: 0x060041AB RID: 16811 RVA: 0x000D69D1 File Offset: 0x000D4BD1
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Wizard_StartNextButtonType")]
		public virtual ButtonType StartNextButtonType
		{
			get
			{
				object obj = this.ViewState["StartNextButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				Wizard.ValidateButtonType(value);
				this.ViewState["StartNextButtonType"] = value;
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x000D69F0 File Offset: 0x000D4BF0
		// (set) Token: 0x060041AD RID: 16813 RVA: 0x000D6A1D File Offset: 0x000D4C1D
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_StepNextButtonImageUrl")]
		[UrlProperty]
		public virtual string StepNextButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StepNextButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["StepNextButtonImageUrl"] = value;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x060041AE RID: 16814 RVA: 0x000D6A30 File Offset: 0x000D4C30
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_StepNextButtonStyle")]
		public Style StepNextButtonStyle
		{
			get
			{
				if (this._stepNextButtonStyle == null)
				{
					this._stepNextButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._stepNextButtonStyle).TrackViewState();
					}
				}
				return this._stepNextButtonStyle;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x000D6A60 File Offset: 0x000D4C60
		// (set) Token: 0x060041B0 RID: 16816 RVA: 0x000D6A92 File Offset: 0x000D4C92
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_StepNextButtonText")]
		[WebSysDescription("Wizard_StepNextButtonText")]
		public virtual string StepNextButtonText
		{
			get
			{
				string text = this.ViewState["StepNextButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_StepNextButtonText");
			}
			set
			{
				this.ViewState["StepNextButtonText"] = value;
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x000D6AA8 File Offset: 0x000D4CA8
		// (set) Token: 0x060041B2 RID: 16818 RVA: 0x000D6AD1 File Offset: 0x000D4CD1
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Wizard_StepNextButtonType")]
		public virtual ButtonType StepNextButtonType
		{
			get
			{
				object obj = this.ViewState["StepNextButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				Wizard.ValidateButtonType(value);
				this.ViewState["StepNextButtonType"] = value;
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x000D6AF0 File Offset: 0x000D4CF0
		// (set) Token: 0x060041B4 RID: 16820 RVA: 0x000D6B1D File Offset: 0x000D4D1D
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[WebSysDescription("Wizard_StepPreviousButtonImageUrl")]
		[UrlProperty]
		public virtual string StepPreviousButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["StepPreviousButtonImageUrl"] = value;
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x000D6B30 File Offset: 0x000D4D30
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_StepPreviousButtonStyle")]
		public Style StepPreviousButtonStyle
		{
			get
			{
				if (this._stepPreviousButtonStyle == null)
				{
					this._stepPreviousButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._stepPreviousButtonStyle).TrackViewState();
					}
				}
				return this._stepPreviousButtonStyle;
			}
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x000D6B60 File Offset: 0x000D4D60
		// (set) Token: 0x060041B7 RID: 16823 RVA: 0x000D6B92 File Offset: 0x000D4D92
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_StepPreviousButtonText")]
		[WebSysDescription("Wizard_StepPreviousButtonText")]
		public virtual string StepPreviousButtonText
		{
			get
			{
				string text = this.ViewState["StepPreviousButtonText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("Wizard_Default_StepPreviousButtonText");
			}
			set
			{
				this.ViewState["StepPreviousButtonText"] = value;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x060041B8 RID: 16824 RVA: 0x000D6BA8 File Offset: 0x000D4DA8
		// (set) Token: 0x060041B9 RID: 16825 RVA: 0x000D6BD1 File Offset: 0x000D4DD1
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Button)]
		[WebSysDescription("Wizard_StepPreviousButtonType")]
		public virtual ButtonType StepPreviousButtonType
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				Wizard.ValidateButtonType(value);
				this.ViewState["StepPreviousButtonType"] = value;
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x060041BA RID: 16826 RVA: 0x000D6BEF File Offset: 0x000D4DEF
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_SideBarButtonStyle")]
		public Style SideBarButtonStyle
		{
			get
			{
				if (this._sideBarButtonStyle == null)
				{
					this._sideBarButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._sideBarButtonStyle).TrackViewState();
					}
				}
				return this._sideBarButtonStyle;
			}
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x000D6C1D File Offset: 0x000D4E1D
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Wizard_SideBarStyle")]
		public TableItemStyle SideBarStyle
		{
			get
			{
				if (this._sideBarStyle == null)
				{
					this._sideBarStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._sideBarStyle).TrackViewState();
					}
				}
				return this._sideBarStyle;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x000D6C4B File Offset: 0x000D4E4B
		// (set) Token: 0x060041BD RID: 16829 RVA: 0x000D6C53 File Offset: 0x000D4E53
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("Wizard_SideBarTemplate")]
		public virtual ITemplate SideBarTemplate
		{
			get
			{
				return this._sideBarTemplate;
			}
			set
			{
				this._sideBarTemplate = value;
				this._sideBarTableCell = null;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x000D6C6C File Offset: 0x000D4E6C
		// (set) Token: 0x060041BF RID: 16831 RVA: 0x000B2546 File Offset: 0x000B0746
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("Wizard_Default_SkipToContentText")]
		[WebSysDescription("WebControl_SkipLinkText")]
		public virtual string SkipLinkText
		{
			get
			{
				string skipLinkTextInternal = this.SkipLinkTextInternal;
				if (skipLinkTextInternal != null)
				{
					return skipLinkTextInternal;
				}
				return SR.GetString("Wizard_Default_SkipToContentText");
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x000D6C8F File Offset: 0x000D4E8F
		// (set) Token: 0x060041C1 RID: 16833 RVA: 0x000D6C97 File Offset: 0x000D4E97
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("Wizard_StartNavigationTemplate")]
		public virtual ITemplate StartNavigationTemplate
		{
			get
			{
				return this._startNavigationTemplate;
			}
			set
			{
				this._startNavigationTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x000D6CA6 File Offset: 0x000D4EA6
		// (set) Token: 0x060041C3 RID: 16835 RVA: 0x000D6CAE File Offset: 0x000D4EAE
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("Wizard_StepNavigationTemplate")]
		public virtual ITemplate StepNavigationTemplate
		{
			get
			{
				return this._stepNavigationTemplate;
			}
			set
			{
				this._stepNavigationTemplate = value;
				this.RequiresControlsRecreation();
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x060041C4 RID: 16836 RVA: 0x000D6CBD File Offset: 0x000D4EBD
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Wizard_StepStyle")]
		public TableItemStyle StepStyle
		{
			get
			{
				if (this._stepStyle == null)
				{
					this._stepStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._stepStyle).TrackViewState();
					}
				}
				return this._stepStyle;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x060041C5 RID: 16837 RVA: 0x0008BDAD File Offset: 0x00089FAD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x000D6CEB File Offset: 0x000D4EEB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Web.UI.Design.WebControls.WizardStepCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Themeable(false)]
		[WebSysDescription("Wizard_WizardSteps")]
		public virtual WizardStepCollection WizardSteps
		{
			get
			{
				if (this._wizardStepCollection == null)
				{
					this._wizardStepCollection = new WizardStepCollection(this);
				}
				return this._wizardStepCollection;
			}
		}

		// Token: 0x14000106 RID: 262
		// (add) Token: 0x060041C7 RID: 16839 RVA: 0x000D6D07 File Offset: 0x000D4F07
		// (remove) Token: 0x060041C8 RID: 16840 RVA: 0x000D6D1A File Offset: 0x000D4F1A
		[WebCategory("Action")]
		[WebSysDescription("Wizard_ActiveStepChanged")]
		public event EventHandler ActiveStepChanged
		{
			add
			{
				base.Events.AddHandler(Wizard._eventActiveStepChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventActiveStepChanged, value);
			}
		}

		// Token: 0x14000107 RID: 263
		// (add) Token: 0x060041C9 RID: 16841 RVA: 0x000D6D2D File Offset: 0x000D4F2D
		// (remove) Token: 0x060041CA RID: 16842 RVA: 0x000D6D40 File Offset: 0x000D4F40
		[WebCategory("Action")]
		[WebSysDescription("Wizard_CancelButtonClick")]
		public event EventHandler CancelButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventCancelButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventCancelButtonClick, value);
			}
		}

		// Token: 0x14000108 RID: 264
		// (add) Token: 0x060041CB RID: 16843 RVA: 0x000D6D53 File Offset: 0x000D4F53
		// (remove) Token: 0x060041CC RID: 16844 RVA: 0x000D6D66 File Offset: 0x000D4F66
		[WebCategory("Action")]
		[WebSysDescription("Wizard_FinishButtonClick")]
		public event WizardNavigationEventHandler FinishButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventFinishButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventFinishButtonClick, value);
			}
		}

		// Token: 0x14000109 RID: 265
		// (add) Token: 0x060041CD RID: 16845 RVA: 0x000D6D79 File Offset: 0x000D4F79
		// (remove) Token: 0x060041CE RID: 16846 RVA: 0x000D6D8C File Offset: 0x000D4F8C
		[WebCategory("Action")]
		[WebSysDescription("Wizard_NextButtonClick")]
		public event WizardNavigationEventHandler NextButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventNextButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventNextButtonClick, value);
			}
		}

		// Token: 0x1400010A RID: 266
		// (add) Token: 0x060041CF RID: 16847 RVA: 0x000D6D9F File Offset: 0x000D4F9F
		// (remove) Token: 0x060041D0 RID: 16848 RVA: 0x000D6DB2 File Offset: 0x000D4FB2
		[WebCategory("Action")]
		[WebSysDescription("Wizard_PreviousButtonClick")]
		public event WizardNavigationEventHandler PreviousButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventPreviousButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventPreviousButtonClick, value);
			}
		}

		// Token: 0x1400010B RID: 267
		// (add) Token: 0x060041D1 RID: 16849 RVA: 0x000D6DC5 File Offset: 0x000D4FC5
		// (remove) Token: 0x060041D2 RID: 16850 RVA: 0x000D6DD8 File Offset: 0x000D4FD8
		[WebCategory("Action")]
		[WebSysDescription("Wizard_SideBarButtonClick")]
		public virtual event WizardNavigationEventHandler SideBarButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard._eventSideBarButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard._eventSideBarButtonClick, value);
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x000D6DEB File Offset: 0x000D4FEB
		internal Dictionary<WizardStepBase, Wizard.BaseNavigationTemplateContainer> CustomNavigationContainers
		{
			get
			{
				if (this._customNavigationContainers == null)
				{
					this._customNavigationContainers = new Dictionary<WizardStepBase, Wizard.BaseNavigationTemplateContainer>();
				}
				return this._customNavigationContainers;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x000D6E08 File Offset: 0x000D5008
		private ITemplate CustomNavigationTemplate
		{
			get
			{
				TemplatedWizardStep templatedWizardStep = this.ActiveStep as TemplatedWizardStep;
				if (templatedWizardStep != null)
				{
					return templatedWizardStep.CustomNavigationTemplate;
				}
				return null;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x000D6E2C File Offset: 0x000D502C
		private Stack<int> History
		{
			get
			{
				if (this._historyStack == null)
				{
					this._historyStack = new Stack<int>();
				}
				return this._historyStack;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x000D6E48 File Offset: 0x000D5048
		private bool IsMacIE5
		{
			get
			{
				if (this._isMacIE == null && !base.DesignMode)
				{
					HttpBrowserCapabilities httpBrowserCapabilities = null;
					if (this.Page != null)
					{
						httpBrowserCapabilities = this.Page.Request.Browser;
					}
					else
					{
						HttpContext httpContext = HttpContext.Current;
						if (httpContext != null)
						{
							httpBrowserCapabilities = httpContext.Request.Browser;
						}
					}
					this._isMacIE = new bool?(httpBrowserCapabilities != null && httpBrowserCapabilities.Type == "IE5" && httpBrowserCapabilities.Platform == "MacPPC");
				}
				return this._isMacIE.Value;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x000D6EDC File Offset: 0x000D50DC
		internal MultiView MultiView
		{
			get
			{
				if (this._multiView == null)
				{
					this._multiView = new MultiView();
					this._multiView.EnableTheming = true;
					this._multiView.ID = "WizardMultiView";
					this._multiView.ActiveViewChanged += this.MultiViewActiveViewChanged;
					this._multiView.IgnoreBubbleEvents();
				}
				return this._multiView;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x000D6F40 File Offset: 0x000D5140
		internal virtual bool ShowCustomNavigationTemplate
		{
			get
			{
				return this.CustomNavigationTemplate != null;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x060041D9 RID: 16857 RVA: 0x000D6F4C File Offset: 0x000D514C
		internal bool ShouldRenderChildControl
		{
			get
			{
				if (!base.DesignMode)
				{
					return true;
				}
				if (this._designModeState == null)
				{
					return true;
				}
				object obj = this._designModeState["ShouldRenderWizardSteps"];
				return obj == null || (bool)obj;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x000D6F89 File Offset: 0x000D5189
		private IWizardSideBarListControl SideBarList
		{
			get
			{
				return this._sideBarList;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x060041DB RID: 16859 RVA: 0x000D6F91 File Offset: 0x000D5191
		private bool SideBarEnabled
		{
			get
			{
				return this._sideBarList != null && this.DisplaySideBar;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x000D6FA3 File Offset: 0x000D51A3
		internal string SkipLinkTextInternal
		{
			get
			{
				return this.ViewState["SkipLinkText"] as string;
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x000D6FBA File Offset: 0x000D51BA
		internal List<TemplatedWizardStep> TemplatedSteps
		{
			get
			{
				if (this._templatedSteps == null)
				{
					this._templatedSteps = new List<TemplatedWizardStep>();
				}
				return this._templatedSteps;
			}
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x000D6FD5 File Offset: 0x000D51D5
		private void MultiViewActiveViewChanged(object source, EventArgs e)
		{
			this.OnActiveStepChanged(this, EventArgs.Empty);
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x000D6FE3 File Offset: 0x000D51E3
		private void ApplyControlProperties()
		{
			this._rendering.ApplyControlProperties();
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000D6FF0 File Offset: 0x000D51F0
		internal Wizard.BaseNavigationTemplateContainer CreateBaseNavigationTemplateContainer(string id)
		{
			return new Wizard.BaseNavigationTemplateContainer(this)
			{
				ID = id
			};
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000D7000 File Offset: 0x000D5200
		protected internal override void CreateChildControls()
		{
			using (new Wizard.WizardControlCollectionModifier(this))
			{
				this.Controls.Clear();
				this._customNavigationContainers = null;
			}
			if (this.LayoutTemplate == null)
			{
				this._rendering = this.CreateTableRendering();
			}
			else
			{
				this._rendering = this.CreateLayoutTemplateRendering();
			}
			this.CreateControlHierarchy();
			base.ClearChildViewState();
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000D7070 File Offset: 0x000D5270
		internal virtual Wizard.TableWizardRendering CreateTableRendering()
		{
			return new Wizard.TableWizardRendering(this);
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000D7078 File Offset: 0x000D5278
		internal virtual Wizard.LayoutTemplateWizardRendering CreateLayoutTemplateRendering()
		{
			return new Wizard.LayoutTemplateWizardRendering(this);
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000D7080 File Offset: 0x000D5280
		protected override ControlCollection CreateControlCollection()
		{
			return new Wizard.WizardControlCollection(this);
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000D7088 File Offset: 0x000D5288
		protected virtual void CreateControlHierarchy()
		{
			this._rendering.CreateControlHierarchy();
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000D7095 File Offset: 0x000D5295
		private void SetStepsAndDataBindSideBarList(IWizardSideBarListControl sideBarList)
		{
			if (sideBarList != null)
			{
				sideBarList.DataSource = this.WizardSteps;
				sideBarList.SelectedIndex = this.ActiveStepIndex;
				sideBarList.DataBind();
			}
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000D70B8 File Offset: 0x000D52B8
		internal virtual ITemplate CreateDefaultSideBarTemplate()
		{
			return new Wizard.DefaultSideBarTemplate(this);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000D70C0 File Offset: 0x000D52C0
		internal virtual ITemplate CreateDefaultDataListItemTemplate()
		{
			return new Wizard.DataListItemTemplate(this);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000D70C8 File Offset: 0x000D52C8
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				CellSpacing = 0,
				CellPadding = 0
			};
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x000D70EC File Offset: 0x000D52EC
		internal virtual void CreateCustomNavigationTemplates()
		{
			for (int i = 0; i < this.WizardSteps.Count; i++)
			{
				TemplatedWizardStep templatedWizardStep = this.WizardSteps[i] as TemplatedWizardStep;
				if (templatedWizardStep != null)
				{
					this.RegisterCustomNavigationContainers(templatedWizardStep);
				}
			}
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x000D712C File Offset: 0x000D532C
		internal void RegisterCustomNavigationContainers(TemplatedWizardStep step)
		{
			this.InstantiateStepContentTemplate(step);
			if (!this.CustomNavigationContainers.ContainsKey(step))
			{
				string customContainerID = Wizard.GetCustomContainerID(this.WizardSteps.IndexOf(step));
				Wizard.BaseNavigationTemplateContainer baseNavigationTemplateContainer;
				if (step.CustomNavigationTemplate != null)
				{
					baseNavigationTemplateContainer = this.CreateBaseNavigationTemplateContainer(customContainerID);
					step.CustomNavigationTemplate.InstantiateIn(baseNavigationTemplateContainer);
					step.CustomNavigationTemplateContainer = baseNavigationTemplateContainer;
					baseNavigationTemplateContainer.RegisterButtonCommandEvents();
				}
				else
				{
					baseNavigationTemplateContainer = this.CreateBaseNavigationTemplateContainer(customContainerID);
					baseNavigationTemplateContainer.RegisterButtonCommandEvents();
				}
				this.CustomNavigationContainers[step] = baseNavigationTemplateContainer;
			}
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000D71A8 File Offset: 0x000D53A8
		internal virtual void DataListItemDataBound(object sender, WizardSideBarListControlItemEventArgs e)
		{
			WizardSideBarListControlItem item = e.Item;
			if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem && item.ItemType != ListItemType.SelectedItem && item.ItemType != ListItemType.EditItem)
			{
				return;
			}
			IButtonControl buttonControl = item.FindControl(Wizard.SideBarButtonID) as IButtonControl;
			if (buttonControl != null)
			{
				Button button = buttonControl as Button;
				if (button != null)
				{
					button.UseSubmitBehavior = false;
				}
				WebControl webControl = buttonControl as WebControl;
				if (webControl != null)
				{
					webControl.TabIndex = this.TabIndex;
				}
				WizardStepBase wizardStepBase = item.DataItem as WizardStepBase;
				if (wizardStepBase != null)
				{
					if (this.GetStepType(wizardStepBase) == WizardStepType.Complete && webControl != null)
					{
						webControl.Enabled = false;
					}
					this.RegisterSideBarDataListForRender();
					if (wizardStepBase.Title.Length > 0)
					{
						buttonControl.Text = wizardStepBase.Title;
					}
					else
					{
						buttonControl.Text = wizardStepBase.ID;
					}
					int num = this.WizardSteps.IndexOf(wizardStepBase);
					buttonControl.CommandName = Wizard.MoveToCommandName;
					buttonControl.CommandArgument = num.ToString(NumberFormatInfo.InvariantInfo);
					this.RegisterCommandEvents(buttonControl);
				}
				return;
			}
			if (!base.DesignMode)
			{
				throw new InvalidOperationException(SR.GetString("Wizard_SideBar_Button_Not_Found", new object[]
				{
					Wizard.DataListID,
					Wizard.SideBarButtonID
				}));
			}
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x000D72D8 File Offset: 0x000D54D8
		internal void RegisterSideBarDataListForRender()
		{
			this._renderSideBarDataList = true;
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000D72E4 File Offset: 0x000D54E4
		private void DataListItemCommand(object sender, CommandEventArgs e)
		{
			if (!Wizard.MoveToCommandName.Equals(e.CommandName, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			int activeStepIndex = this.ActiveStepIndex;
			int num = int.Parse((string)e.CommandArgument, CultureInfo.InvariantCulture);
			WizardNavigationEventArgs wizardNavigationEventArgs = new WizardNavigationEventArgs(activeStepIndex, num);
			if (this._commandSender != null && !base.DesignMode && this.Page != null && !this.Page.IsValid)
			{
				wizardNavigationEventArgs.Cancel = true;
			}
			this._activeStepIndexSet = false;
			this.OnSideBarButtonClick(wizardNavigationEventArgs);
			if (!wizardNavigationEventArgs.Cancel)
			{
				if (!this._activeStepIndexSet && this.AllowNavigationToStep(num))
				{
					this.ActiveStepIndex = num;
					return;
				}
			}
			else
			{
				this.ActiveStepIndex = activeStepIndex;
			}
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000D738C File Offset: 0x000D558C
		internal static string GetCustomContainerID(int index)
		{
			return "__CustomNav" + index.ToString();
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000D73A0 File Offset: 0x000D55A0
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary designModeState = base.GetDesignModeState();
			this._designModeState = designModeState;
			int activeStepIndex = this.ActiveStepIndex;
			try
			{
				if (activeStepIndex == -1 && this.WizardSteps.Count > 0)
				{
					this.ActiveStepIndex = 0;
				}
				this.RequiresControlsRecreation();
				this.EnsureChildControls();
				this.ApplyControlProperties();
				this._rendering.SetDesignModeState(designModeState);
				if (this.ShowCustomNavigationTemplate)
				{
					Wizard.BaseNavigationTemplateContainer baseNavigationTemplateContainer = this.CustomNavigationContainers[this.ActiveStep];
					designModeState[Wizard.CustomNextButtonID] = baseNavigationTemplateContainer.NextButton;
					designModeState[Wizard.CustomPreviousButtonID] = baseNavigationTemplateContainer.PreviousButton;
					designModeState[Wizard.CustomFinishButtonID] = baseNavigationTemplateContainer.FinishButton;
					designModeState[Wizard.CancelButtonID] = baseNavigationTemplateContainer.CancelButton;
					designModeState["CustomNavigationControls"] = baseNavigationTemplateContainer.Controls;
				}
				if (this.SideBarTemplate == null && this._sideBarList != null)
				{
					this._sideBarList.ItemTemplate = this.CreateDefaultDataListItemTemplate();
				}
				designModeState[Wizard.DataListID] = this._sideBarList;
				designModeState["TemplatedWizardSteps"] = this.TemplatedSteps;
			}
			finally
			{
				this.ActiveStepIndex = activeStepIndex;
			}
			return designModeState;
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000D74C8 File Offset: 0x000D56C8
		public ICollection GetHistory()
		{
			ArrayList arrayList = new ArrayList();
			foreach (int index in this.History)
			{
				arrayList.Add(this.WizardSteps[index]);
			}
			return arrayList;
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000D7530 File Offset: 0x000D5730
		internal int GetPreviousStepIndex(bool popStack)
		{
			int num = -1;
			int activeStepIndex = this.ActiveStepIndex;
			if (this._historyStack == null || this._historyStack.Count == 0)
			{
				return num;
			}
			if (popStack)
			{
				num = this._historyStack.Pop();
				if (num == activeStepIndex && this._historyStack.Count > 0)
				{
					num = this._historyStack.Pop();
				}
			}
			else
			{
				num = this._historyStack.Peek();
				if (num == activeStepIndex && this._historyStack.Count > 1)
				{
					int item = this._historyStack.Pop();
					num = this._historyStack.Peek();
					this._historyStack.Push(item);
				}
			}
			if (num == activeStepIndex)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000D75D8 File Offset: 0x000D57D8
		private WizardStepType GetStepType(int index)
		{
			WizardStepBase wizardStep = this.WizardSteps[index];
			return this.GetStepType(wizardStep, index);
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000D75FC File Offset: 0x000D57FC
		private WizardStepType GetStepType(WizardStepBase step)
		{
			int index = this.WizardSteps.IndexOf(step);
			return this.GetStepType(step, index);
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000D7620 File Offset: 0x000D5820
		public WizardStepType GetStepType(WizardStepBase wizardStep, int index)
		{
			if (wizardStep.StepType != WizardStepType.Auto)
			{
				return wizardStep.StepType;
			}
			if (this.WizardSteps.Count == 1 || (index < this.WizardSteps.Count - 1 && this.WizardSteps[index + 1].StepType == WizardStepType.Complete))
			{
				return WizardStepType.Finish;
			}
			if (index == 0)
			{
				return WizardStepType.Start;
			}
			if (index == this.WizardSteps.Count - 1)
			{
				return WizardStepType.Finish;
			}
			return WizardStepType.Step;
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000D768A File Offset: 0x000D588A
		internal virtual void InstantiateStepContentTemplates()
		{
			this.TemplatedSteps.ForEach(delegate(TemplatedWizardStep step)
			{
				this.InstantiateStepContentTemplate(step);
			});
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000D76A4 File Offset: 0x000D58A4
		internal void InstantiateStepContentTemplate(TemplatedWizardStep step)
		{
			step.Controls.Clear();
			Wizard.BaseContentTemplateContainer baseContentTemplateContainer = new Wizard.BaseContentTemplateContainer(this, true);
			ITemplate contentTemplate = step.ContentTemplate;
			if (contentTemplate != null)
			{
				baseContentTemplateContainer.SetEnableTheming();
				contentTemplate.InstantiateIn(baseContentTemplateContainer.InnerCell);
			}
			step.ContentTemplateContainer = baseContentTemplateContainer;
			step.Controls.Add(baseContentTemplateContainer);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x000D76F4 File Offset: 0x000D58F4
		protected internal override void LoadControlState(object state)
		{
			Triplet triplet = state as Triplet;
			if (triplet != null)
			{
				base.LoadControlState(triplet.First);
				Array array = triplet.Second as Array;
				if (array != null)
				{
					Array.Reverse(array);
					this._historyStack = new Stack<int>(array.Cast<int>());
				}
				this.ActiveStepIndex = (int)triplet.Third;
			}
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000D7750 File Offset: 0x000D5950
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 15)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.NavigationButtonStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.SideBarButtonStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.NavigationStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.SideBarStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.StepStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.StartNextButtonStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.StepNextButtonStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.StepPreviousButtonStyle).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.FinishPreviousButtonStyle).LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				((IStateManager)this.FinishCompleteButtonStyle).LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.CancelButtonStyle).LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)base.ControlStyle).LoadViewState(array[13]);
			}
			if (array[14] != null)
			{
				this.DisplaySideBar = (bool)array[14];
			}
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000D78A8 File Offset: 0x000D5AA8
		public void MoveTo(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			int num = this.WizardSteps.IndexOf(wizardStep);
			if (num == -1)
			{
				throw new ArgumentException(SR.GetString("Wizard_Step_Not_In_Wizard"));
			}
			this.ActiveStepIndex = num;
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000D78EC File Offset: 0x000D5AEC
		protected virtual void OnActiveStepChanged(object source, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Wizard._eventActiveStepChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000D791C File Offset: 0x000D5B1C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool flag = false;
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null)
			{
				if (string.Equals(Wizard.CancelCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					this.OnCancelButtonClick(EventArgs.Empty);
					return true;
				}
				int activeStepIndex = this.ActiveStepIndex;
				int nextStepIndex = activeStepIndex;
				bool flag2 = true;
				WizardStepType wizardStepType = WizardStepType.Auto;
				WizardStepBase wizardStepBase = this.WizardSteps[activeStepIndex];
				if (wizardStepBase is TemplatedWizardStep)
				{
					flag2 = false;
				}
				else
				{
					wizardStepType = this.GetStepType(wizardStepBase);
				}
				WizardNavigationEventArgs wizardNavigationEventArgs = new WizardNavigationEventArgs(activeStepIndex, nextStepIndex);
				if (this._commandSender != null && this.Page != null && !this.Page.IsValid)
				{
					wizardNavigationEventArgs.Cancel = true;
				}
				bool flag3 = false;
				this._activeStepIndexSet = false;
				if (string.Equals(Wizard.MoveNextCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					if (flag2 && wizardStepType != WizardStepType.Start && wizardStepType != WizardStepType.Step)
					{
						throw new InvalidOperationException(SR.GetString("Wizard_InvalidBubbleEvent", new object[]
						{
							Wizard.MoveNextCommandName
						}));
					}
					if (activeStepIndex < this.WizardSteps.Count - 1)
					{
						wizardNavigationEventArgs.SetNextStepIndex(activeStepIndex + 1);
					}
					this.OnNextButtonClick(wizardNavigationEventArgs);
					flag = true;
				}
				else if (string.Equals(Wizard.MovePreviousCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					if (flag2 && wizardStepType != WizardStepType.Step && wizardStepType != WizardStepType.Finish)
					{
						throw new InvalidOperationException(SR.GetString("Wizard_InvalidBubbleEvent", new object[]
						{
							Wizard.MovePreviousCommandName
						}));
					}
					flag3 = true;
					int previousStepIndex = this.GetPreviousStepIndex(false);
					if (previousStepIndex != -1)
					{
						wizardNavigationEventArgs.SetNextStepIndex(previousStepIndex);
					}
					this.OnPreviousButtonClick(wizardNavigationEventArgs);
					flag = true;
				}
				else if (string.Equals(Wizard.MoveCompleteCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					if (flag2 && wizardStepType != WizardStepType.Finish)
					{
						throw new InvalidOperationException(SR.GetString("Wizard_InvalidBubbleEvent", new object[]
						{
							Wizard.MoveCompleteCommandName
						}));
					}
					if (activeStepIndex < this.WizardSteps.Count - 1)
					{
						wizardNavigationEventArgs.SetNextStepIndex(activeStepIndex + 1);
					}
					this.OnFinishButtonClick(wizardNavigationEventArgs);
					flag = true;
				}
				else if (string.Equals(Wizard.MoveToCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
				{
					nextStepIndex = int.Parse((string)commandEventArgs.CommandArgument, CultureInfo.InvariantCulture);
					wizardNavigationEventArgs.SetNextStepIndex(nextStepIndex);
					flag = true;
				}
				if (flag)
				{
					if (!wizardNavigationEventArgs.Cancel)
					{
						if (!this._activeStepIndexSet && this.AllowNavigationToStep(wizardNavigationEventArgs.NextStepIndex))
						{
							if (flag3)
							{
								this.GetPreviousStepIndex(true);
							}
							this.ActiveStepIndex = wizardNavigationEventArgs.NextStepIndex;
						}
					}
					else
					{
						this.ActiveStepIndex = activeStepIndex;
					}
				}
			}
			return flag;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000D7B6E File Offset: 0x000D5D6E
		internal void OnWizardStepsChanged()
		{
			this.SetStepsAndDataBindSideBarList(this._sideBarList);
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000D7B7C File Offset: 0x000D5D7C
		protected virtual bool AllowNavigationToStep(int index)
		{
			return this._historyStack == null || !this._historyStack.Contains(index) || this.WizardSteps[index].AllowReturn;
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000D7BA8 File Offset: 0x000D5DA8
		protected virtual void OnCancelButtonClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Wizard._eventCancelButtonClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			string cancelDestinationPageUrl = this.CancelDestinationPageUrl;
			if (!string.IsNullOrEmpty(cancelDestinationPageUrl))
			{
				this.Page.Response.Redirect(base.ResolveClientUrl(cancelDestinationPageUrl), false);
			}
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x000D7BFD File Offset: 0x000D5DFD
		private void OnCommand(object sender, CommandEventArgs e)
		{
			this._commandSender = (sender as IButtonControl);
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x000D7C0C File Offset: 0x000D5E0C
		protected virtual void OnFinishButtonClick(WizardNavigationEventArgs e)
		{
			WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard._eventFinishButtonClick];
			if (wizardNavigationEventHandler != null)
			{
				wizardNavigationEventHandler(this, e);
			}
			string finishDestinationPageUrl = this.FinishDestinationPageUrl;
			if (!string.IsNullOrEmpty(finishDestinationPageUrl))
			{
				this.Page.Response.Redirect(base.ResolveClientUrl(finishDestinationPageUrl), false);
			}
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x000D7C64 File Offset: 0x000D5E64
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.ActiveStepIndex == -1 && this.WizardSteps.Count > 0 && !base.DesignMode)
			{
				this.ActiveStepIndex = 0;
			}
			this.EnsureChildControls();
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000D7CB8 File Offset: 0x000D5EB8
		protected virtual void OnNextButtonClick(WizardNavigationEventArgs e)
		{
			WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard._eventNextButtonClick];
			if (wizardNavigationEventHandler != null)
			{
				wizardNavigationEventHandler(this, e);
			}
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x000D7CE8 File Offset: 0x000D5EE8
		protected virtual void OnPreviousButtonClick(WizardNavigationEventArgs e)
		{
			WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard._eventPreviousButtonClick];
			if (wizardNavigationEventHandler != null)
			{
				wizardNavigationEventHandler(this, e);
			}
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000D7D18 File Offset: 0x000D5F18
		protected virtual void OnSideBarButtonClick(WizardNavigationEventArgs e)
		{
			WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard._eventSideBarButtonClick];
			if (wizardNavigationEventHandler != null)
			{
				wizardNavigationEventHandler(this, e);
			}
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000D7D48 File Offset: 0x000D5F48
		internal void RequiresControlsRecreation()
		{
			if (base.ChildControlsCreated)
			{
				using (new Wizard.WizardControlCollectionModifier(this))
				{
					base.ChildControlsCreated = false;
				}
				this._rendering = null;
			}
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x000D7D90 File Offset: 0x000D5F90
		protected internal void RegisterCommandEvents(IButtonControl button)
		{
			if (button != null && button.CausesValidation)
			{
				button.Command += this.OnCommand;
			}
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x000D7DAF File Offset: 0x000D5FAF
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			this.ApplyControlProperties();
			if (this.ActiveStepIndex == -1 || this.WizardSteps.Count == 0)
			{
				return;
			}
			this.RenderContents(writer);
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x000D7DF0 File Offset: 0x000D5FF0
		protected internal override object SaveControlState()
		{
			int activeStepIndex = this.ActiveStepIndex;
			if (this._historyStack == null || this._historyStack.Count == 0 || this._historyStack.Peek() != activeStepIndex)
			{
				this.History.Push(this.ActiveStepIndex);
			}
			object obj = base.SaveControlState();
			bool flag = this._historyStack != null && this._historyStack.Count > 0;
			if (obj != null || flag || activeStepIndex != -1)
			{
				object y = flag ? this._historyStack.ToArray() : null;
				return new Triplet(obj, y, activeStepIndex);
			}
			return null;
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x000D7E88 File Offset: 0x000D6088
		protected override object SaveViewState()
		{
			object[] array = new object[15];
			array[0] = base.SaveViewState();
			array[1] = ((this._navigationButtonStyle != null) ? ((IStateManager)this._navigationButtonStyle).SaveViewState() : null);
			array[2] = ((this._sideBarButtonStyle != null) ? ((IStateManager)this._sideBarButtonStyle).SaveViewState() : null);
			array[3] = ((this._headerStyle != null) ? ((IStateManager)this._headerStyle).SaveViewState() : null);
			array[4] = ((this._navigationStyle != null) ? ((IStateManager)this._navigationStyle).SaveViewState() : null);
			array[5] = ((this._sideBarStyle != null) ? ((IStateManager)this._sideBarStyle).SaveViewState() : null);
			array[6] = ((this._stepStyle != null) ? ((IStateManager)this._stepStyle).SaveViewState() : null);
			array[7] = ((this._startNextButtonStyle != null) ? ((IStateManager)this._startNextButtonStyle).SaveViewState() : null);
			array[8] = ((this._stepNextButtonStyle != null) ? ((IStateManager)this._stepNextButtonStyle).SaveViewState() : null);
			array[9] = ((this._stepPreviousButtonStyle != null) ? ((IStateManager)this._stepPreviousButtonStyle).SaveViewState() : null);
			array[10] = ((this._finishPreviousButtonStyle != null) ? ((IStateManager)this._finishPreviousButtonStyle).SaveViewState() : null);
			array[11] = ((this._finishCompleteButtonStyle != null) ? ((IStateManager)this._finishCompleteButtonStyle).SaveViewState() : null);
			array[12] = ((this._cancelButtonStyle != null) ? ((IStateManager)this._cancelButtonStyle).SaveViewState() : null);
			array[13] = (base.ControlStyleCreated ? ((IStateManager)base.ControlStyle).SaveViewState() : null);
			if (this.DisplaySideBar != this._displaySideBarDefault)
			{
				array[14] = this.DisplaySideBar;
			}
			for (int i = 0; i < 15; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x000D8024 File Offset: 0x000D6224
		private void SetCancelButtonVisibility(Wizard.BaseNavigationTemplateContainer container)
		{
			Control control = container.CancelButton as Control;
			if (control != null)
			{
				Control parent = control.Parent;
				if (parent != null)
				{
					parent.Visible = this.DisplayCancelButton;
				}
				control.Visible = this.DisplayCancelButton;
			}
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x000D8064 File Offset: 0x000D6264
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._navigationButtonStyle != null)
			{
				((IStateManager)this._navigationButtonStyle).TrackViewState();
			}
			if (this._sideBarButtonStyle != null)
			{
				((IStateManager)this._sideBarButtonStyle).TrackViewState();
			}
			if (this._headerStyle != null)
			{
				((IStateManager)this._headerStyle).TrackViewState();
			}
			if (this._navigationStyle != null)
			{
				((IStateManager)this._navigationStyle).TrackViewState();
			}
			if (this._sideBarStyle != null)
			{
				((IStateManager)this._sideBarStyle).TrackViewState();
			}
			if (this._stepStyle != null)
			{
				((IStateManager)this._stepStyle).TrackViewState();
			}
			if (this._startNextButtonStyle != null)
			{
				((IStateManager)this._startNextButtonStyle).TrackViewState();
			}
			if (this._stepPreviousButtonStyle != null)
			{
				((IStateManager)this._stepPreviousButtonStyle).TrackViewState();
			}
			if (this._stepNextButtonStyle != null)
			{
				((IStateManager)this._stepNextButtonStyle).TrackViewState();
			}
			if (this._finishPreviousButtonStyle != null)
			{
				((IStateManager)this._finishPreviousButtonStyle).TrackViewState();
			}
			if (this._finishCompleteButtonStyle != null)
			{
				((IStateManager)this._finishCompleteButtonStyle).TrackViewState();
			}
			if (this._cancelButtonStyle != null)
			{
				((IStateManager)this._cancelButtonStyle).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				((IStateManager)base.ControlStyle).TrackViewState();
			}
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x000D816E File Offset: 0x000D636E
		private static void ValidateButtonType(ButtonType value)
		{
			if (value < ButtonType.Button || value > ButtonType.Link)
			{
				throw new ArgumentOutOfRangeException("value");
			}
		}

		// Token: 0x04002516 RID: 9494
		private ITemplate _finishNavigationTemplate;

		// Token: 0x04002517 RID: 9495
		private ITemplate _headerTemplate;

		// Token: 0x04002518 RID: 9496
		private ITemplate _layoutTemplate;

		// Token: 0x04002519 RID: 9497
		private ITemplate _startNavigationTemplate;

		// Token: 0x0400251A RID: 9498
		private ITemplate _stepNavigationTemplate;

		// Token: 0x0400251B RID: 9499
		private ITemplate _sideBarTemplate;

		// Token: 0x0400251C RID: 9500
		private MultiView _multiView;

		// Token: 0x0400251D RID: 9501
		private static readonly object _eventActiveStepChanged = new object();

		// Token: 0x0400251E RID: 9502
		private static readonly object _eventFinishButtonClick = new object();

		// Token: 0x0400251F RID: 9503
		private static readonly object _eventNextButtonClick = new object();

		// Token: 0x04002520 RID: 9504
		private static readonly object _eventPreviousButtonClick = new object();

		// Token: 0x04002521 RID: 9505
		private static readonly object _eventSideBarButtonClick = new object();

		// Token: 0x04002522 RID: 9506
		private static readonly object _eventCancelButtonClick = new object();

		// Token: 0x04002523 RID: 9507
		public static readonly string HeaderPlaceholderId = "headerPlaceholder";

		// Token: 0x04002524 RID: 9508
		public static readonly string NavigationPlaceholderId = "navigationPlaceholder";

		// Token: 0x04002525 RID: 9509
		public static readonly string SideBarPlaceholderId = "sideBarPlaceholder";

		// Token: 0x04002526 RID: 9510
		public static readonly string WizardStepPlaceholderId = "wizardStepPlaceholder";

		// Token: 0x04002527 RID: 9511
		public static readonly string CancelCommandName = "Cancel";

		// Token: 0x04002528 RID: 9512
		public static readonly string MoveNextCommandName = "MoveNext";

		// Token: 0x04002529 RID: 9513
		public static readonly string MovePreviousCommandName = "MovePrevious";

		// Token: 0x0400252A RID: 9514
		public static readonly string MoveToCommandName = "Move";

		// Token: 0x0400252B RID: 9515
		public static readonly string MoveCompleteCommandName = "MoveComplete";

		// Token: 0x0400252C RID: 9516
		protected static readonly string CancelButtonID = "CancelButton";

		// Token: 0x0400252D RID: 9517
		protected static readonly string StartNextButtonID = "StartNextButton";

		// Token: 0x0400252E RID: 9518
		protected static readonly string StepPreviousButtonID = "StepPreviousButton";

		// Token: 0x0400252F RID: 9519
		protected static readonly string StepNextButtonID = "StepNextButton";

		// Token: 0x04002530 RID: 9520
		protected static readonly string FinishButtonID = "FinishButton";

		// Token: 0x04002531 RID: 9521
		protected static readonly string FinishPreviousButtonID = "FinishPreviousButton";

		// Token: 0x04002532 RID: 9522
		protected static readonly string CustomPreviousButtonID = "CustomPreviousButton";

		// Token: 0x04002533 RID: 9523
		protected static readonly string CustomNextButtonID = "CustomNextButton";

		// Token: 0x04002534 RID: 9524
		protected static readonly string CustomFinishButtonID = "CustomFinishButton";

		// Token: 0x04002535 RID: 9525
		protected static readonly string DataListID = "SideBarList";

		// Token: 0x04002536 RID: 9526
		protected static readonly string SideBarButtonID = "SideBarButton";

		// Token: 0x04002537 RID: 9527
		internal const string _customNavigationControls = "CustomNavigationControls";

		// Token: 0x04002538 RID: 9528
		private const string _templatedStepsID = "TemplatedWizardSteps";

		// Token: 0x04002539 RID: 9529
		private const string _multiViewID = "WizardMultiView";

		// Token: 0x0400253A RID: 9530
		private const string _customNavigationContainerIdPrefix = "__CustomNav";

		// Token: 0x0400253B RID: 9531
		private TableCell _sideBarTableCell;

		// Token: 0x0400253C RID: 9532
		private IWizardSideBarListControl _sideBarList;

		// Token: 0x0400253D RID: 9533
		private IButtonControl _commandSender;

		// Token: 0x0400253E RID: 9534
		private Dictionary<WizardStepBase, Wizard.BaseNavigationTemplateContainer> _customNavigationContainers;

		// Token: 0x0400253F RID: 9535
		private IDictionary _designModeState;

		// Token: 0x04002540 RID: 9536
		private Stack<int> _historyStack;

		// Token: 0x04002541 RID: 9537
		private List<TemplatedWizardStep> _templatedSteps;

		// Token: 0x04002542 RID: 9538
		private WizardStepCollection _wizardStepCollection;

		// Token: 0x04002543 RID: 9539
		private Wizard.WizardRenderingBase _rendering;

		// Token: 0x04002544 RID: 9540
		private bool _activeStepIndexSet;

		// Token: 0x04002545 RID: 9541
		private bool _displaySideBarDefault;

		// Token: 0x04002546 RID: 9542
		private bool _displaySideBar;

		// Token: 0x04002547 RID: 9543
		private bool? _isMacIE;

		// Token: 0x04002548 RID: 9544
		private bool _renderSideBarDataList;

		// Token: 0x04002549 RID: 9545
		private Style _cancelButtonStyle;

		// Token: 0x0400254A RID: 9546
		private Style _finishCompleteButtonStyle;

		// Token: 0x0400254B RID: 9547
		private Style _finishPreviousButtonStyle;

		// Token: 0x0400254C RID: 9548
		private Style _navigationButtonStyle;

		// Token: 0x0400254D RID: 9549
		private Style _sideBarButtonStyle;

		// Token: 0x0400254E RID: 9550
		private Style _startNextButtonStyle;

		// Token: 0x0400254F RID: 9551
		private Style _stepNextButtonStyle;

		// Token: 0x04002550 RID: 9552
		private Style _stepPreviousButtonStyle;

		// Token: 0x04002551 RID: 9553
		private TableItemStyle _headerStyle;

		// Token: 0x04002552 RID: 9554
		private TableItemStyle _navigationStyle;

		// Token: 0x04002553 RID: 9555
		private TableItemStyle _sideBarStyle;

		// Token: 0x04002554 RID: 9556
		private TableItemStyle _stepStyle;

		// Token: 0x04002555 RID: 9557
		private const bool _displaySideBarDefaultValue = true;

		// Token: 0x04002556 RID: 9558
		private const int _viewStateArrayLength = 15;

		// Token: 0x020009CD RID: 2509
		internal abstract class WizardRenderingBase
		{
			// Token: 0x17001DEA RID: 7658
			// (get) Token: 0x06006C78 RID: 27768 RVA: 0x00184072 File Offset: 0x00182272
			// (set) Token: 0x06006C79 RID: 27769 RVA: 0x0018407A File Offset: 0x0018227A
			private protected Wizard Owner { protected get; private set; }

			// Token: 0x06006C7A RID: 27770 RVA: 0x00184083 File Offset: 0x00182283
			protected WizardRenderingBase(Wizard wizard)
			{
				this.Owner = wizard;
			}

			// Token: 0x06006C7B RID: 27771
			public abstract void ApplyControlProperties();

			// Token: 0x06006C7C RID: 27772
			public abstract void CreateControlHierarchy();

			// Token: 0x06006C7D RID: 27773 RVA: 0x00184094 File Offset: 0x00182294
			public virtual void SetDesignModeState(IDictionary dictionary)
			{
				if (this._startNavigationTemplateContainer != null)
				{
					dictionary[Wizard.StartNextButtonID] = this._startNavigationTemplateContainer.NextButton;
					dictionary[Wizard.CancelButtonID] = this._startNavigationTemplateContainer.CancelButton;
				}
				if (this._stepNavigationTemplateContainer != null)
				{
					dictionary[Wizard.StepNextButtonID] = this._stepNavigationTemplateContainer.NextButton;
					dictionary[Wizard.StepPreviousButtonID] = this._stepNavigationTemplateContainer.PreviousButton;
					dictionary[Wizard.CancelButtonID] = this._stepNavigationTemplateContainer.CancelButton;
				}
				if (this._finishNavigationTemplateContainer != null)
				{
					dictionary[Wizard.FinishPreviousButtonID] = this._finishNavigationTemplateContainer.PreviousButton;
					dictionary[Wizard.FinishButtonID] = this._finishNavigationTemplateContainer.FinishButton;
					dictionary[Wizard.CancelButtonID] = this._finishNavigationTemplateContainer.CancelButton;
				}
			}

			// Token: 0x06006C7E RID: 27774 RVA: 0x0018416C File Offset: 0x0018236C
			protected void ApplyControlProperties_Sidebar()
			{
				if (this.Owner.SideBarEnabled)
				{
					this.Owner.SetStepsAndDataBindSideBarList(this.Owner._sideBarList);
					if (this.Owner.SideBarTemplate == null)
					{
						foreach (object obj in this.Owner._sideBarList.Items)
						{
							Control control = (Control)obj;
							WebControl webControl = control.FindControl(Wizard.SideBarButtonID) as WebControl;
							if (webControl != null)
							{
								webControl.MergeStyle(this.Owner._sideBarButtonStyle);
							}
						}
					}
				}
			}

			// Token: 0x06006C7F RID: 27775 RVA: 0x00184220 File Offset: 0x00182420
			protected void ApplyNavigationTemplateProperties()
			{
				if (this._finishNavigationTemplateContainer == null || this._startNavigationTemplateContainer == null || this._stepNavigationTemplateContainer == null)
				{
					return;
				}
				if (this.Owner.ActiveStepIndex >= this.Owner.WizardSteps.Count || this.Owner.ActiveStepIndex < 0)
				{
					return;
				}
				WizardStepType wizardStepType = this.SetActiveTemplates();
				bool flag = wizardStepType != WizardStepType.Finish || this.Owner.ActiveStepIndex != 0 || this.Owner.ActiveStep.StepType > WizardStepType.Auto;
				this.ApplyDefaultStartNavigationTemplateProperties();
				bool previousImageButtonVisible = true;
				int previousStepIndex = this.Owner.GetPreviousStepIndex(false);
				if (previousStepIndex >= 0)
				{
					previousImageButtonVisible = this.Owner.WizardSteps[previousStepIndex].AllowReturn;
				}
				this.ApplyDefaultFinishNavigationTemplateProperties(previousImageButtonVisible);
				this.ApplyDefaultStepNavigationTemplateProperties(previousImageButtonVisible);
				if (!flag)
				{
					Control control = this._finishNavigationTemplateContainer.PreviousButton as Control;
					if (control != null)
					{
						if (this.Owner.FinishNavigationTemplate == null)
						{
							control.Parent.Visible = false;
							return;
						}
						control.Visible = false;
					}
				}
			}

			// Token: 0x06006C80 RID: 27776 RVA: 0x00184320 File Offset: 0x00182520
			private void ApplyDefaultStepNavigationTemplateProperties(bool previousImageButtonVisible)
			{
				if (this.Owner.StepNavigationTemplate != null)
				{
					return;
				}
				Wizard.BaseNavigationTemplateContainer stepNavigationTemplateContainer = this._stepNavigationTemplateContainer;
				Wizard.NavigationTemplate defaultStepNavigationTemplate = this._defaultStepNavigationTemplate;
				if (this.Owner.DesignMode)
				{
					defaultStepNavigationTemplate.ResetButtonsVisibility();
				}
				stepNavigationTemplateContainer.PreviousButton = defaultStepNavigationTemplate.FirstButton;
				((Control)stepNavigationTemplateContainer.PreviousButton).Visible = true;
				stepNavigationTemplateContainer.NextButton = defaultStepNavigationTemplate.SecondButton;
				((Control)stepNavigationTemplateContainer.NextButton).Visible = true;
				stepNavigationTemplateContainer.CancelButton = defaultStepNavigationTemplate.CancelButton;
				Wizard.WizardRenderingBase.ApplyButtonProperties(stepNavigationTemplateContainer.NextButton, this.Owner.StepNextButtonText, this.Owner.StepNextButtonImageUrl);
				Wizard.WizardRenderingBase.ApplyButtonProperties(stepNavigationTemplateContainer.PreviousButton, this.Owner.StepPreviousButtonText, this.Owner.StepPreviousButtonImageUrl, previousImageButtonVisible);
				Wizard.WizardRenderingBase.ApplyButtonProperties(stepNavigationTemplateContainer.CancelButton, this.Owner.CancelButtonText, this.Owner.CancelButtonImageUrl);
				int previousStepIndex = this.Owner.GetPreviousStepIndex(false);
				if (previousStepIndex != -1 && !this.Owner.WizardSteps[previousStepIndex].AllowReturn)
				{
					((Control)stepNavigationTemplateContainer.PreviousButton).Visible = false;
				}
				this.Owner.SetCancelButtonVisibility(stepNavigationTemplateContainer);
				stepNavigationTemplateContainer.ApplyButtonStyle(this.Owner.FinishCompleteButtonStyle, this.Owner.StepPreviousButtonStyle, this.Owner.StepNextButtonStyle, this.Owner.CancelButtonStyle);
			}

			// Token: 0x06006C81 RID: 27777 RVA: 0x00184480 File Offset: 0x00182680
			private void ApplyDefaultFinishNavigationTemplateProperties(bool previousImageButtonVisible)
			{
				if (this.Owner.FinishNavigationTemplate != null)
				{
					return;
				}
				Wizard.BaseNavigationTemplateContainer finishNavigationTemplateContainer = this._finishNavigationTemplateContainer;
				Wizard.NavigationTemplate defaultFinishNavigationTemplate = this._defaultFinishNavigationTemplate;
				if (this.Owner.DesignMode)
				{
					defaultFinishNavigationTemplate.ResetButtonsVisibility();
				}
				finishNavigationTemplateContainer.PreviousButton = defaultFinishNavigationTemplate.FirstButton;
				((Control)finishNavigationTemplateContainer.PreviousButton).Visible = true;
				finishNavigationTemplateContainer.FinishButton = defaultFinishNavigationTemplate.SecondButton;
				((Control)finishNavigationTemplateContainer.FinishButton).Visible = true;
				finishNavigationTemplateContainer.CancelButton = defaultFinishNavigationTemplate.CancelButton;
				finishNavigationTemplateContainer.FinishButton.CommandName = Wizard.MoveCompleteCommandName;
				Wizard.WizardRenderingBase.ApplyButtonProperties(finishNavigationTemplateContainer.FinishButton, this.Owner.FinishCompleteButtonText, this.Owner.FinishCompleteButtonImageUrl);
				Wizard.WizardRenderingBase.ApplyButtonProperties(finishNavigationTemplateContainer.PreviousButton, this.Owner.FinishPreviousButtonText, this.Owner.FinishPreviousButtonImageUrl, previousImageButtonVisible);
				Wizard.WizardRenderingBase.ApplyButtonProperties(finishNavigationTemplateContainer.CancelButton, this.Owner.CancelButtonText, this.Owner.CancelButtonImageUrl);
				int previousStepIndex = this.Owner.GetPreviousStepIndex(false);
				if (previousStepIndex != -1 && !this.Owner.WizardSteps[previousStepIndex].AllowReturn)
				{
					((Control)finishNavigationTemplateContainer.PreviousButton).Visible = false;
				}
				this.Owner.SetCancelButtonVisibility(finishNavigationTemplateContainer);
				finishNavigationTemplateContainer.ApplyButtonStyle(this.Owner.FinishCompleteButtonStyle, this.Owner.FinishPreviousButtonStyle, this.Owner.StepNextButtonStyle, this.Owner.CancelButtonStyle);
			}

			// Token: 0x06006C82 RID: 27778 RVA: 0x001845F0 File Offset: 0x001827F0
			private void ApplyDefaultStartNavigationTemplateProperties()
			{
				if (this.Owner.StartNavigationTemplate != null)
				{
					return;
				}
				Wizard.BaseNavigationTemplateContainer startNavigationTemplateContainer = this._startNavigationTemplateContainer;
				Wizard.NavigationTemplate defaultStartNavigationTemplate = this._defaultStartNavigationTemplate;
				if (this.Owner.DesignMode)
				{
					defaultStartNavigationTemplate.ResetButtonsVisibility();
				}
				startNavigationTemplateContainer.NextButton = defaultStartNavigationTemplate.SecondButton;
				((Control)startNavigationTemplateContainer.NextButton).Visible = true;
				startNavigationTemplateContainer.CancelButton = defaultStartNavigationTemplate.CancelButton;
				Wizard.WizardRenderingBase.ApplyButtonProperties(startNavigationTemplateContainer.NextButton, this.Owner.StartNextButtonText, this.Owner.StartNextButtonImageUrl);
				Wizard.WizardRenderingBase.ApplyButtonProperties(startNavigationTemplateContainer.CancelButton, this.Owner.CancelButtonText, this.Owner.CancelButtonImageUrl);
				this.Owner.SetCancelButtonVisibility(startNavigationTemplateContainer);
				startNavigationTemplateContainer.ApplyButtonStyle(this.Owner.FinishCompleteButtonStyle, this.Owner.StepPreviousButtonStyle, this.Owner.StartNextButtonStyle, this.Owner.CancelButtonStyle);
			}

			// Token: 0x06006C83 RID: 27779 RVA: 0x001846D8 File Offset: 0x001828D8
			protected virtual WizardStepType SetActiveTemplates()
			{
				WizardStepType stepType = this.Owner.GetStepType(this.Owner.ActiveStepIndex);
				this._startNavigationTemplateContainer.Visible = (stepType == WizardStepType.Start);
				this._stepNavigationTemplateContainer.Visible = (stepType == WizardStepType.Step);
				this._finishNavigationTemplateContainer.Visible = (stepType == WizardStepType.Finish);
				if (stepType == WizardStepType.Complete)
				{
					this.OnlyShowCompleteStep();
				}
				return stepType;
			}

			// Token: 0x06006C84 RID: 27780 RVA: 0x00184734 File Offset: 0x00182934
			private static void ApplyButtonProperties(IButtonControl button, string text, string imageUrl)
			{
				Wizard.WizardRenderingBase.ApplyButtonProperties(button, text, imageUrl, true);
			}

			// Token: 0x06006C85 RID: 27781 RVA: 0x00184740 File Offset: 0x00182940
			private static void ApplyButtonProperties(IButtonControl button, string text, string imageUrl, bool imageButtonVisible)
			{
				if (button == null)
				{
					return;
				}
				ImageButton imageButton = button as ImageButton;
				if (imageButton != null)
				{
					imageButton.ImageUrl = imageUrl;
					imageButton.AlternateText = text;
					imageButton.Visible = imageButtonVisible;
					return;
				}
				button.Text = text;
			}

			// Token: 0x06006C86 RID: 27782
			public abstract void OnlyShowCompleteStep();

			// Token: 0x06006C87 RID: 27783 RVA: 0x00184778 File Offset: 0x00182978
			protected void ApplyCustomNavigationTemplateProperties()
			{
				foreach (Wizard.BaseNavigationTemplateContainer baseNavigationTemplateContainer in this.Owner.CustomNavigationContainers.Values)
				{
					baseNavigationTemplateContainer.Visible = false;
				}
				if (this.Owner.ShowCustomNavigationTemplate)
				{
					Wizard.BaseNavigationTemplateContainer baseNavigationTemplateContainer2 = this.Owner._customNavigationContainers[this.Owner.ActiveStep];
					baseNavigationTemplateContainer2.Visible = true;
					this._startNavigationTemplateContainer.Visible = false;
					this._stepNavigationTemplateContainer.Visible = false;
					this._finishNavigationTemplateContainer.Visible = false;
				}
			}

			// Token: 0x06006C88 RID: 27784 RVA: 0x0018482C File Offset: 0x00182A2C
			protected void CreateControlHierarchy_CleanUpOldSideBarList(IWizardSideBarListControl sideBarList)
			{
				if (sideBarList != null)
				{
					sideBarList.ItemCommand -= this.Owner.DataListItemCommand;
					sideBarList.ItemDataBound -= this.Owner.DataListItemDataBound;
				}
			}

			// Token: 0x06006C89 RID: 27785 RVA: 0x00184860 File Offset: 0x00182A60
			protected IWizardSideBarListControl CreateControlHierarchy_SetUpSideBarList(Control sideBarContainer)
			{
				IWizardSideBarListControl wizardSideBarListControl = sideBarContainer.FindControl(Wizard.DataListID) as IWizardSideBarListControl;
				if (wizardSideBarListControl != null)
				{
					wizardSideBarListControl.ItemCommand += this.Owner.DataListItemCommand;
					wizardSideBarListControl.ItemDataBound += this.Owner.DataListItemDataBound;
					if (this.Owner.DesignMode)
					{
						((IControlDesignerAccessor)wizardSideBarListControl).GetDesignModeState()["EnableDesignTimeDataBinding"] = true;
					}
					this.Owner.SetStepsAndDataBindSideBarList(wizardSideBarListControl);
				}
				else if (!this.Owner.DesignMode)
				{
					throw new InvalidOperationException(SR.GetString("Wizard_DataList_Not_Found", new object[]
					{
						Wizard.DataListID
					}));
				}
				return wizardSideBarListControl;
			}

			// Token: 0x06006C8A RID: 27786 RVA: 0x00184914 File Offset: 0x00182B14
			protected void CreateNavigationControlHierarchy(Control container)
			{
				container.Controls.Clear();
				this.Owner.CustomNavigationContainers.Clear();
				this.Owner.CreateCustomNavigationTemplates();
				foreach (Wizard.BaseNavigationTemplateContainer child in this.Owner.CustomNavigationContainers.Values)
				{
					container.Controls.Add(child);
				}
				this.CreateStartNavigationTemplate(container);
				this.CreateFinishNavigationTemplate(container);
				this.CreateStepNavigationTemplate(container);
			}

			// Token: 0x06006C8B RID: 27787 RVA: 0x001849B4 File Offset: 0x00182BB4
			private void CreateStartNavigationTemplate(Control container)
			{
				ITemplate template = this.Owner.StartNavigationTemplate;
				this._startNavigationTemplateContainer = new Wizard.StartNavigationTemplateContainer(this.Owner);
				this._startNavigationTemplateContainer.ID = "StartNavigationTemplateContainerID";
				if (template == null)
				{
					this._startNavigationTemplateContainer.EnableViewState = false;
					this._defaultStartNavigationTemplate = Wizard.NavigationTemplate.GetDefaultStartNavigationTemplate(this.Owner);
					template = this._defaultStartNavigationTemplate;
				}
				else
				{
					this._startNavigationTemplateContainer.SetEnableTheming();
				}
				template.InstantiateIn(this._startNavigationTemplateContainer);
				container.Controls.Add(this._startNavigationTemplateContainer);
			}

			// Token: 0x06006C8C RID: 27788 RVA: 0x00184A40 File Offset: 0x00182C40
			private void CreateStepNavigationTemplate(Control container)
			{
				ITemplate template = this.Owner.StepNavigationTemplate;
				this._stepNavigationTemplateContainer = new Wizard.StepNavigationTemplateContainer(this.Owner);
				this._stepNavigationTemplateContainer.ID = "StepNavigationTemplateContainerID";
				if (template == null)
				{
					this._stepNavigationTemplateContainer.EnableViewState = false;
					this._defaultStepNavigationTemplate = Wizard.NavigationTemplate.GetDefaultStepNavigationTemplate(this.Owner);
					template = this._defaultStepNavigationTemplate;
				}
				else
				{
					this._stepNavigationTemplateContainer.SetEnableTheming();
				}
				template.InstantiateIn(this._stepNavigationTemplateContainer);
				container.Controls.Add(this._stepNavigationTemplateContainer);
			}

			// Token: 0x06006C8D RID: 27789 RVA: 0x00184ACC File Offset: 0x00182CCC
			private void CreateFinishNavigationTemplate(Control container)
			{
				ITemplate template = this.Owner.FinishNavigationTemplate;
				this._finishNavigationTemplateContainer = new Wizard.FinishNavigationTemplateContainer(this.Owner);
				this._finishNavigationTemplateContainer.ID = "FinishNavigationTemplateContainerID";
				if (template == null)
				{
					this._finishNavigationTemplateContainer.EnableViewState = false;
					this._defaultFinishNavigationTemplate = Wizard.NavigationTemplate.GetDefaultFinishNavigationTemplate(this.Owner);
					template = this._defaultFinishNavigationTemplate;
				}
				else
				{
					this._finishNavigationTemplateContainer.SetEnableTheming();
				}
				template.InstantiateIn(this._finishNavigationTemplateContainer);
				container.Controls.Add(this._finishNavigationTemplateContainer);
			}

			// Token: 0x040039BF RID: 14783
			private const string _startNavigationTemplateContainerID = "StartNavigationTemplateContainerID";

			// Token: 0x040039C0 RID: 14784
			private const string _stepNavigationTemplateContainerID = "StepNavigationTemplateContainerID";

			// Token: 0x040039C1 RID: 14785
			private const string _finishNavigationTemplateContainerID = "FinishNavigationTemplateContainerID";

			// Token: 0x040039C2 RID: 14786
			private Wizard.NavigationTemplate _defaultStartNavigationTemplate;

			// Token: 0x040039C3 RID: 14787
			private Wizard.NavigationTemplate _defaultStepNavigationTemplate;

			// Token: 0x040039C4 RID: 14788
			private Wizard.NavigationTemplate _defaultFinishNavigationTemplate;

			// Token: 0x040039C5 RID: 14789
			protected Wizard.BaseNavigationTemplateContainer _finishNavigationTemplateContainer;

			// Token: 0x040039C6 RID: 14790
			protected Wizard.BaseNavigationTemplateContainer _startNavigationTemplateContainer;

			// Token: 0x040039C7 RID: 14791
			protected Wizard.BaseNavigationTemplateContainer _stepNavigationTemplateContainer;
		}

		// Token: 0x020009CE RID: 2510
		internal class LayoutTemplateWizardRendering : Wizard.WizardRenderingBase
		{
			// Token: 0x06006C8E RID: 27790 RVA: 0x00184B57 File Offset: 0x00182D57
			public LayoutTemplateWizardRendering(Wizard wizard) : base(wizard)
			{
			}

			// Token: 0x06006C8F RID: 27791 RVA: 0x00184B60 File Offset: 0x00182D60
			public override void ApplyControlProperties()
			{
				this.ApplyControlProperties_Header();
				base.ApplyControlProperties_Sidebar();
				this.ApplyControlProperties_Navigation();
			}

			// Token: 0x06006C90 RID: 27792 RVA: 0x00184B74 File Offset: 0x00182D74
			private void ApplyControlProperties_Navigation()
			{
				base.ApplyNavigationTemplateProperties();
				base.ApplyCustomNavigationTemplateProperties();
			}

			// Token: 0x06006C91 RID: 27793 RVA: 0x00184B84 File Offset: 0x00182D84
			private void ApplyControlProperties_Header()
			{
				if (base.Owner.HeaderTemplate != null)
				{
					return;
				}
				if (this._headerLiteral != null)
				{
					this._headerLiteral.Text = base.Owner.HeaderText;
					return;
				}
				if (!string.IsNullOrEmpty(base.Owner.HeaderText))
				{
					throw new InvalidOperationException(SR.GetString("Wizard_Header_Placeholder_Must_Be_Specified_For_HeaderText", new object[]
					{
						base.Owner.ID,
						Wizard.HeaderPlaceholderId
					}));
				}
			}

			// Token: 0x06006C92 RID: 27794 RVA: 0x00184BFC File Offset: 0x00182DFC
			public override void OnlyShowCompleteStep()
			{
				this._layoutContainer.ControlToRender = base.Owner.MultiView;
			}

			// Token: 0x06006C93 RID: 27795 RVA: 0x00184C14 File Offset: 0x00182E14
			public override void CreateControlHierarchy()
			{
				this._layoutContainer = new Wizard.LayoutTemplateWizardRendering.WizardContainer();
				base.Owner.LayoutTemplate.InstantiateIn(this._layoutContainer);
				using (new Wizard.WizardControlCollectionModifier(base.Owner))
				{
					base.Owner.Controls.Add(this._layoutContainer);
				}
				this.CreateControlHierarchy_Header(this._layoutContainer);
				this.CreateControlHierarchy_SideBar(this._layoutContainer);
				this.CreateControlHierarchy_WizardStep(this._layoutContainer);
				this.CreateControlHierarchy_Navigation(this._layoutContainer);
			}

			// Token: 0x06006C94 RID: 27796 RVA: 0x00184CB0 File Offset: 0x00182EB0
			private void CreateControlHierarchy_Navigation(Control layoutContainer)
			{
				Control control = layoutContainer.FindControl(Wizard.NavigationPlaceholderId);
				if (control == null)
				{
					throw new InvalidOperationException(SR.GetString("Wizard_Navigation_Placeholder_Must_Be_Specified", new object[]
					{
						base.Owner.ID,
						Wizard.NavigationPlaceholderId
					}));
				}
				Control control2 = new Control();
				Wizard.LayoutTemplateWizardRendering.ReplacePlaceholderWithControl(layoutContainer, control, control2);
				base.CreateNavigationControlHierarchy(control2);
			}

			// Token: 0x06006C95 RID: 27797 RVA: 0x00184D10 File Offset: 0x00182F10
			private void CreateControlHierarchy_Header(Control layoutContainer)
			{
				Control control = layoutContainer.FindControl(Wizard.HeaderPlaceholderId);
				if (base.Owner.HeaderTemplate == null)
				{
					if (control != null)
					{
						this._headerLiteral = new Literal();
						Wizard.LayoutTemplateWizardRendering.ReplacePlaceholderWithControl(layoutContainer, control, this._headerLiteral);
					}
					return;
				}
				if (control == null)
				{
					throw new InvalidOperationException(SR.GetString("Wizard_Header_Placeholder_Must_Be_Specified_For_HeaderTemplate", new object[]
					{
						base.Owner.ID,
						Wizard.HeaderPlaceholderId
					}));
				}
				Wizard.LayoutTemplateWizardRendering.ReplacePlaceholderWithTemplateInstance(layoutContainer, control, base.Owner.HeaderTemplate);
			}

			// Token: 0x06006C96 RID: 27798 RVA: 0x00184D94 File Offset: 0x00182F94
			private void CreateControlHierarchy_SideBar(Control layoutContainer)
			{
				if (!base.Owner.DisplaySideBar)
				{
					return;
				}
				Control control = layoutContainer.FindControl(Wizard.SideBarPlaceholderId);
				if (control == null)
				{
					throw new InvalidOperationException(SR.GetString("Wizard_Sidebar_Placeholder_Must_Be_Specified", new object[]
					{
						base.Owner.ID,
						Wizard.SideBarPlaceholderId
					}));
				}
				ITemplate template = base.Owner.SideBarTemplate ?? base.Owner.CreateDefaultSideBarTemplate();
				Wizard.LayoutTemplateWizardRendering.ReplacePlaceholderWithTemplateInstance(layoutContainer, control, template);
				base.CreateControlHierarchy_CleanUpOldSideBarList(base.Owner.SideBarList);
				base.Owner._sideBarList = base.CreateControlHierarchy_SetUpSideBarList(layoutContainer);
			}

			// Token: 0x06006C97 RID: 27799 RVA: 0x00184E30 File Offset: 0x00183030
			private void CreateControlHierarchy_WizardStep(Control layoutContainer)
			{
				Control control = layoutContainer.FindControl(Wizard.WizardStepPlaceholderId);
				if (control == null)
				{
					throw new InvalidOperationException(SR.GetString("Wizard_Step_Placeholder_Must_Be_Specified", new object[]
					{
						base.Owner.ID,
						Wizard.WizardStepPlaceholderId
					}));
				}
				Wizard.LayoutTemplateWizardRendering.ReplacePlaceholderWithControl(layoutContainer, control, base.Owner.MultiView);
			}

			// Token: 0x06006C98 RID: 27800 RVA: 0x00184E8C File Offset: 0x0018308C
			private static void ReplacePlaceholderWithTemplateInstance(Control targetContainer, Control placeholder, ITemplate template)
			{
				Control control = new Control();
				template.InstantiateIn(control);
				Wizard.LayoutTemplateWizardRendering.ReplacePlaceholderWithControl(targetContainer, placeholder, control);
			}

			// Token: 0x06006C99 RID: 27801 RVA: 0x00184EB0 File Offset: 0x001830B0
			private static void ReplacePlaceholderWithControl(Control targetContainer, Control placeholder, Control replacement)
			{
				int index = targetContainer.Controls.IndexOf(placeholder);
				targetContainer.Controls.RemoveAt(index);
				targetContainer.Controls.AddAt(index, replacement);
			}

			// Token: 0x040039C9 RID: 14793
			private Literal _headerLiteral;

			// Token: 0x040039CA RID: 14794
			private Wizard.LayoutTemplateWizardRendering.WizardContainer _layoutContainer;

			// Token: 0x02000A92 RID: 2706
			internal class WizardContainer : WebControl
			{
				// Token: 0x17001E4E RID: 7758
				// (get) Token: 0x06006F5A RID: 28506 RVA: 0x0018C8FA File Offset: 0x0018AAFA
				// (set) Token: 0x06006F5B RID: 28507 RVA: 0x0018C902 File Offset: 0x0018AB02
				internal Control ControlToRender { get; set; }

				// Token: 0x06006F5C RID: 28508 RVA: 0x0018C90B File Offset: 0x0018AB0B
				protected internal override void Render(HtmlTextWriter writer)
				{
					if (this.ControlToRender == null)
					{
						this.RenderChildren(writer);
						return;
					}
					this.ControlToRender.Render(writer);
				}
			}
		}

		// Token: 0x020009CF RID: 2511
		internal class TableWizardRendering : Wizard.WizardRenderingBase
		{
			// Token: 0x06006C9A RID: 27802 RVA: 0x00184B57 File Offset: 0x00182D57
			public TableWizardRendering(Wizard wizard) : base(wizard)
			{
			}

			// Token: 0x17001DEB RID: 7659
			// (get) Token: 0x06006C9B RID: 27803 RVA: 0x00184EE3 File Offset: 0x001830E3
			private TableCell NavigationTableCell
			{
				get
				{
					if (this._navigationTableCell == null)
					{
						this._navigationTableCell = new TableCell();
					}
					return this._navigationTableCell;
				}
			}

			// Token: 0x06006C9C RID: 27804 RVA: 0x00184F00 File Offset: 0x00183100
			public override void ApplyControlProperties()
			{
				if (!base.Owner.DesignMode && (base.Owner.ActiveStepIndex < 0 || base.Owner.ActiveStepIndex >= base.Owner.WizardSteps.Count || base.Owner.WizardSteps.Count == 0))
				{
					return;
				}
				if (base.Owner.SideBarEnabled && base.Owner._sideBarStyle != null)
				{
					base.Owner._sideBarTableCell.ApplyStyle(base.Owner._sideBarStyle);
				}
				this.ApplyControlProperties_Header();
				this.ApplyControlProperties_WizardSteps();
				this.ApplyControlProperties_Navigation();
				base.ApplyControlProperties_Sidebar();
				if (this._renderTable != null)
				{
					Util.CopyBaseAttributesToInnerControl(base.Owner, this._renderTable);
					if (base.Owner.ControlStyleCreated)
					{
						this._renderTable.ApplyStyle(base.Owner.ControlStyle);
					}
					else
					{
						this._renderTable.CellSpacing = 0;
						this._renderTable.CellPadding = 0;
					}
					if (!base.Owner.DesignMode && base.Owner.IsMacIE5 && (!base.Owner.ControlStyleCreated || base.Owner.ControlStyle.Height == Unit.Empty))
					{
						this._renderTable.ControlStyle.Height = Unit.Pixel(1);
					}
				}
				if (!base.Owner.DesignMode && this._navigationTableCell != null && base.Owner.IsMacIE5)
				{
					this._navigationTableCell.ControlStyle.Height = Unit.Pixel(1);
				}
			}

			// Token: 0x06006C9D RID: 27805 RVA: 0x00185090 File Offset: 0x00183290
			private void ApplyControlProperties_Navigation()
			{
				base.ApplyNavigationTemplateProperties();
				base.ApplyCustomNavigationTemplateProperties();
				if (this._navigationTableCell != null)
				{
					this.NavigationTableCell.HorizontalAlign = HorizontalAlign.Right;
					if (base.Owner._navigationStyle != null)
					{
						if (!base.Owner.DesignMode && base.Owner.IsMacIE5 && base.Owner._navigationStyle.Height == Unit.Empty)
						{
							base.Owner._navigationStyle.Height = Unit.Pixel(1);
						}
						this._navigationTableCell.ApplyStyle(base.Owner._navigationStyle);
					}
				}
				if (base.Owner.ShowCustomNavigationTemplate)
				{
					this._navigationRow.Visible = true;
				}
			}

			// Token: 0x06006C9E RID: 27806 RVA: 0x00185148 File Offset: 0x00183348
			private void ApplyControlProperties_WizardSteps()
			{
				if (this._stepTableCell != null && base.Owner._stepStyle != null)
				{
					if (!base.Owner.DesignMode && base.Owner.IsMacIE5 && base.Owner._stepStyle.Height == Unit.Empty)
					{
						base.Owner._stepStyle.Height = Unit.Pixel(1);
					}
					this._stepTableCell.ApplyStyle(base.Owner._stepStyle);
				}
			}

			// Token: 0x06006C9F RID: 27807 RVA: 0x001851CC File Offset: 0x001833CC
			private void ApplyControlProperties_Header()
			{
				if (this._headerTableRow != null)
				{
					if (base.Owner.HeaderTemplate == null && string.IsNullOrEmpty(base.Owner.HeaderText))
					{
						this._headerTableRow.Visible = false;
						return;
					}
					this._headerTableCell.ApplyStyle(base.Owner._headerStyle);
					if (base.Owner.HeaderTemplate != null)
					{
						if (this._titleLiteral != null)
						{
							this._titleLiteral.Visible = false;
							return;
						}
					}
					else if (this._titleLiteral != null)
					{
						this._titleLiteral.Text = base.Owner.HeaderText;
					}
				}
			}

			// Token: 0x06006CA0 RID: 27808 RVA: 0x00185268 File Offset: 0x00183468
			protected override WizardStepType SetActiveTemplates()
			{
				WizardStepType wizardStepType = base.SetActiveTemplates();
				if (wizardStepType != WizardStepType.Complete && base.Owner._sideBarTableCell != null)
				{
					base.Owner._sideBarTableCell.Visible = (base.Owner.SideBarEnabled && base.Owner._renderSideBarDataList);
				}
				return wizardStepType;
			}

			// Token: 0x06006CA1 RID: 27809 RVA: 0x001852B9 File Offset: 0x001834B9
			public override void OnlyShowCompleteStep()
			{
				if (this._headerTableRow != null)
				{
					this._headerTableRow.Visible = false;
				}
				if (base.Owner._sideBarTableCell != null)
				{
					base.Owner._sideBarTableCell.Visible = false;
				}
				this._navigationRow.Visible = false;
			}

			// Token: 0x06006CA2 RID: 27810 RVA: 0x001852FC File Offset: 0x001834FC
			public override void CreateControlHierarchy()
			{
				Table mainContentTable;
				if (base.Owner.DisplaySideBar)
				{
					mainContentTable = this.CreateControlHierarchy_CreateLayoutWithSideBar();
				}
				else
				{
					mainContentTable = this.CreateControlHierarchy_CreateLayoutWithoutSideBar();
				}
				this.CreateControlHierarchy_CreateHeaderArea(mainContentTable);
				this.CreateControlHierarchy_CreateStepArea(mainContentTable);
				this.CreateControlHierarchy_CreateNavigationArea(mainContentTable);
			}

			// Token: 0x06006CA3 RID: 27811 RVA: 0x0018533D File Offset: 0x0018353D
			private void CreateControlHierarchy_CreateNavigationArea(Table mainContentTable)
			{
				this._navigationRow = new TableRow();
				mainContentTable.Controls.Add(this._navigationRow);
				this._navigationRow.Controls.Add(this.NavigationTableCell);
				base.CreateNavigationControlHierarchy(this.NavigationTableCell);
			}

			// Token: 0x06006CA4 RID: 27812 RVA: 0x00185380 File Offset: 0x00183580
			private void CreateControlHierarchy_CreateStepArea(Table mainContentTable)
			{
				TableRow tableRow = new TableRow
				{
					Height = Unit.Percentage(100.0)
				};
				mainContentTable.Controls.Add(tableRow);
				this._stepTableCell = new TableCell();
				tableRow.Controls.Add(this._stepTableCell);
				this._stepTableCell.Controls.Add(base.Owner.MultiView);
				base.Owner.InstantiateStepContentTemplates();
			}

			// Token: 0x06006CA5 RID: 27813 RVA: 0x001853F8 File Offset: 0x001835F8
			private void CreateControlHierarchy_CreateHeaderArea(Table mainContentTable)
			{
				this._headerTableRow = new TableRow();
				mainContentTable.Controls.Add(this._headerTableRow);
				this._headerTableCell = new Wizard.InternalTableCell(base.Owner)
				{
					ID = "HeaderContainer"
				};
				if (base.Owner.HeaderTemplate != null)
				{
					this._headerTableCell.EnableTheming = base.Owner.EnableTheming;
					base.Owner.HeaderTemplate.InstantiateIn(this._headerTableCell);
				}
				else
				{
					this._titleLiteral = new LiteralControl();
					this._headerTableCell.Controls.Add(this._titleLiteral);
				}
				this._headerTableRow.Controls.Add(this._headerTableCell);
			}

			// Token: 0x06006CA6 RID: 27814 RVA: 0x001854B0 File Offset: 0x001836B0
			private Table CreateControlHierarchy_CreateLayoutWithoutSideBar()
			{
				Wizard.WizardChildTable wizardChildTable = new Wizard.WizardChildTable(base.Owner)
				{
					EnableTheming = false
				};
				using (new Wizard.WizardControlCollectionModifier(base.Owner))
				{
					base.Owner.Controls.Add(wizardChildTable);
				}
				this._renderTable = wizardChildTable;
				return wizardChildTable;
			}

			// Token: 0x06006CA7 RID: 27815 RVA: 0x00185514 File Offset: 0x00183714
			private Table CreateControlHierarchy_CreateLayoutWithSideBar()
			{
				Table table = new Wizard.WizardChildTable(base.Owner)
				{
					EnableTheming = false
				};
				TableRow tableRow = new TableRow();
				table.Controls.Add(tableRow);
				TableCell tableCell = base.Owner._sideBarTableCell ?? this.CreateControlHierarchy_CreateSideBarTableCell();
				tableRow.Controls.Add(tableCell);
				base.Owner._sideBarTableCell = tableCell;
				base.Owner._renderSideBarDataList = false;
				TableCell tableCell2 = new TableCell
				{
					Height = Unit.Percentage(100.0)
				};
				tableRow.Controls.Add(tableCell2);
				WizardDefaultInnerTable wizardDefaultInnerTable = new WizardDefaultInnerTable
				{
					CellSpacing = 0,
					Height = Unit.Percentage(100.0),
					Width = Unit.Percentage(100.0)
				};
				tableCell2.Controls.Add(wizardDefaultInnerTable);
				if (!base.Owner.DesignMode && base.Owner.IsMacIE5)
				{
					tableCell2.Height = Unit.Pixel(1);
				}
				using (new Wizard.WizardControlCollectionModifier(base.Owner))
				{
					base.Owner.Controls.Add(table);
				}
				base.CreateControlHierarchy_CleanUpOldSideBarList(base.Owner.SideBarList);
				base.Owner._sideBarList = base.CreateControlHierarchy_SetUpSideBarList(base.Owner._sideBarTableCell);
				this._renderTable = table;
				return wizardDefaultInnerTable;
			}

			// Token: 0x06006CA8 RID: 27816 RVA: 0x00185684 File Offset: 0x00183884
			private TableCell CreateControlHierarchy_CreateSideBarTableCell()
			{
				TableCell tableCell = new Wizard.AccessibleTableCell(base.Owner)
				{
					ID = "SideBarContainer",
					Height = Unit.Percentage(100.0)
				};
				ITemplate template = base.Owner.SideBarTemplate;
				if (template == null)
				{
					tableCell.EnableViewState = false;
					template = base.Owner.CreateDefaultSideBarTemplate();
				}
				else
				{
					tableCell.EnableTheming = base.Owner.EnableTheming;
				}
				template.InstantiateIn(tableCell);
				return tableCell;
			}

			// Token: 0x06006CA9 RID: 27817 RVA: 0x001856F9 File Offset: 0x001838F9
			public override void SetDesignModeState(IDictionary dictionary)
			{
				base.SetDesignModeState(dictionary);
				dictionary["StepTableCell"] = this._stepTableCell;
			}

			// Token: 0x040039CB RID: 14795
			private const string _headerCellID = "HeaderContainer";

			// Token: 0x040039CC RID: 14796
			private const string _sideBarCellID = "SideBarContainer";

			// Token: 0x040039CD RID: 14797
			private const string _stepTableCellID = "StepTableCell";

			// Token: 0x040039CE RID: 14798
			private TableCell _headerTableCell;

			// Token: 0x040039CF RID: 14799
			private TableRow _headerTableRow;

			// Token: 0x040039D0 RID: 14800
			private TableRow _navigationRow;

			// Token: 0x040039D1 RID: 14801
			private TableCell _navigationTableCell;

			// Token: 0x040039D2 RID: 14802
			private Table _renderTable;

			// Token: 0x040039D3 RID: 14803
			private TableCell _stepTableCell;

			// Token: 0x040039D4 RID: 14804
			private LiteralControl _titleLiteral;
		}

		// Token: 0x020009D0 RID: 2512
		private class WizardControlCollection : ControlCollection
		{
			// Token: 0x06006CAA RID: 27818 RVA: 0x00185713 File Offset: 0x00183913
			public WizardControlCollection(Wizard wizard) : base(wizard)
			{
				if (!wizard.DesignMode)
				{
					base.SetCollectionReadOnly("Wizard_Cannot_Modify_ControlCollection");
				}
			}
		}

		// Token: 0x020009D1 RID: 2513
		private class WizardControlCollectionModifier : IDisposable
		{
			// Token: 0x06006CAB RID: 27819 RVA: 0x00185730 File Offset: 0x00183930
			public WizardControlCollectionModifier(Wizard wizard)
			{
				this._wizard = wizard;
				if (!this._wizard.DesignMode)
				{
					this._controls = this._wizard.Controls;
					this._originalError = this._controls.SetCollectionReadOnly(null);
				}
			}

			// Token: 0x06006CAC RID: 27820 RVA: 0x0018576F File Offset: 0x0018396F
			void IDisposable.Dispose()
			{
				if (!this._wizard.DesignMode)
				{
					this._controls.SetCollectionReadOnly(this._originalError);
				}
			}

			// Token: 0x040039D5 RID: 14805
			private Wizard _wizard;

			// Token: 0x040039D6 RID: 14806
			private ControlCollection _controls;

			// Token: 0x040039D7 RID: 14807
			private string _originalError;
		}

		// Token: 0x020009D2 RID: 2514
		[SupportsEventValidation]
		private class WizardChildTable : ChildTable
		{
			// Token: 0x06006CAD RID: 27821 RVA: 0x00185790 File Offset: 0x00183990
			internal WizardChildTable(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006CAE RID: 27822 RVA: 0x0018579F File Offset: 0x0018399F
			protected override bool OnBubbleEvent(object source, EventArgs args)
			{
				return this._owner.OnBubbleEvent(source, args);
			}

			// Token: 0x040039D8 RID: 14808
			private Wizard _owner;
		}

		// Token: 0x020009D3 RID: 2515
		private enum WizardTemplateType
		{
			// Token: 0x040039DA RID: 14810
			StartNavigationTemplate,
			// Token: 0x040039DB RID: 14811
			StepNavigationTemplate,
			// Token: 0x040039DC RID: 14812
			FinishNavigationTemplate
		}

		// Token: 0x020009D4 RID: 2516
		private sealed class NavigationTemplate : ITemplate
		{
			// Token: 0x06006CAF RID: 27823 RVA: 0x001857AE File Offset: 0x001839AE
			internal static Wizard.NavigationTemplate GetDefaultStartNavigationTemplate(Wizard wizard)
			{
				return new Wizard.NavigationTemplate(wizard, Wizard.WizardTemplateType.StartNavigationTemplate, true, null, "StartNext", "Cancel");
			}

			// Token: 0x06006CB0 RID: 27824 RVA: 0x001857C3 File Offset: 0x001839C3
			internal static Wizard.NavigationTemplate GetDefaultStepNavigationTemplate(Wizard wizard)
			{
				return new Wizard.NavigationTemplate(wizard, Wizard.WizardTemplateType.StepNavigationTemplate, false, "StepPrevious", "StepNext", "Cancel");
			}

			// Token: 0x06006CB1 RID: 27825 RVA: 0x001857DC File Offset: 0x001839DC
			internal static Wizard.NavigationTemplate GetDefaultFinishNavigationTemplate(Wizard wizard)
			{
				return new Wizard.NavigationTemplate(wizard, Wizard.WizardTemplateType.FinishNavigationTemplate, false, "FinishPrevious", "Finish", "Cancel");
			}

			// Token: 0x06006CB2 RID: 27826 RVA: 0x001857F8 File Offset: 0x001839F8
			internal void ResetButtonsVisibility()
			{
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						Control control = this._buttons[i][j] as Control;
						if (control != null)
						{
							control.Visible = false;
						}
					}
				}
			}

			// Token: 0x06006CB3 RID: 27827 RVA: 0x00185838 File Offset: 0x00183A38
			private NavigationTemplate(Wizard wizard, Wizard.WizardTemplateType templateType, bool button1CausesValidation, string label1ID, string label2ID, string label3ID)
			{
				this._wizard = wizard;
				this._button1ID = label1ID;
				this._button2ID = label2ID;
				this._button3ID = label3ID;
				this._templateType = templateType;
				this._buttons = new IButtonControl[3][];
				this._buttons[0] = new IButtonControl[3];
				this._buttons[1] = new IButtonControl[3];
				this._buttons[2] = new IButtonControl[3];
				this._button1CausesValidation = button1CausesValidation;
			}

			// Token: 0x06006CB4 RID: 27828 RVA: 0x001858B0 File Offset: 0x00183AB0
			void ITemplate.InstantiateIn(Control container)
			{
				Table table = new WizardDefaultInnerTable();
				table.CellSpacing = 5;
				table.CellPadding = 5;
				container.Controls.Add(table);
				this._row = new TableRow();
				table.Rows.Add(this._row);
				if (this._button1ID != null)
				{
					this.CreateButtonControl(this._buttons[0], this._button1ID, this._button1CausesValidation, Wizard.MovePreviousCommandName);
				}
				if (this._button2ID != null)
				{
					this.CreateButtonControl(this._buttons[1], this._button2ID, true, (this._templateType == Wizard.WizardTemplateType.FinishNavigationTemplate) ? Wizard.MoveCompleteCommandName : Wizard.MoveNextCommandName);
				}
				this.CreateButtonControl(this._buttons[2], this._button3ID, false, Wizard.CancelCommandName);
			}

			// Token: 0x06006CB5 RID: 27829 RVA: 0x0017E0D5 File Offset: 0x0017C2D5
			private void OnPreRender(object source, EventArgs e)
			{
				((ImageButton)source).Visible = false;
			}

			// Token: 0x06006CB6 RID: 27830 RVA: 0x00185970 File Offset: 0x00183B70
			private void CreateButtonControl(IButtonControl[] buttons, string id, bool causesValidation, string commandName)
			{
				LinkButton linkButton = new LinkButton();
				linkButton.CausesValidation = causesValidation;
				linkButton.ID = id + "LinkButton";
				linkButton.Visible = false;
				linkButton.CommandName = commandName;
				linkButton.TabIndex = this._wizard.TabIndex;
				this._wizard.RegisterCommandEvents(linkButton);
				buttons[0] = linkButton;
				ImageButton imageButton = new ImageButton();
				imageButton.CausesValidation = causesValidation;
				imageButton.ID = id + "ImageButton";
				imageButton.Visible = true;
				imageButton.CommandName = commandName;
				imageButton.TabIndex = this._wizard.TabIndex;
				this._wizard.RegisterCommandEvents(imageButton);
				imageButton.PreRender += this.OnPreRender;
				buttons[1] = imageButton;
				Button button = new Button();
				button.CausesValidation = causesValidation;
				button.ID = id + "Button";
				button.Visible = false;
				button.CommandName = commandName;
				button.TabIndex = this._wizard.TabIndex;
				this._wizard.RegisterCommandEvents(button);
				buttons[2] = button;
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				this._row.Cells.Add(tableCell);
				tableCell.Controls.Add(linkButton);
				tableCell.Controls.Add(imageButton);
				tableCell.Controls.Add(button);
			}

			// Token: 0x17001DEC RID: 7660
			// (get) Token: 0x06006CB7 RID: 27831 RVA: 0x00185ABC File Offset: 0x00183CBC
			internal IButtonControl FirstButton
			{
				get
				{
					ButtonType type = ButtonType.Button;
					switch (this._templateType)
					{
					case Wizard.WizardTemplateType.StartNavigationTemplate:
						goto IL_37;
					case Wizard.WizardTemplateType.StepNavigationTemplate:
						type = this._wizard.StepPreviousButtonType;
						goto IL_37;
					}
					type = this._wizard.FinishPreviousButtonType;
					IL_37:
					return this.GetButtonBasedOnType(0, type);
				}
			}

			// Token: 0x17001DED RID: 7661
			// (get) Token: 0x06006CB8 RID: 27832 RVA: 0x00185B08 File Offset: 0x00183D08
			internal IButtonControl SecondButton
			{
				get
				{
					ButtonType type;
					switch (this._templateType)
					{
					case Wizard.WizardTemplateType.StartNavigationTemplate:
						type = this._wizard.StartNextButtonType;
						goto IL_45;
					case Wizard.WizardTemplateType.StepNavigationTemplate:
						type = this._wizard.StepNextButtonType;
						goto IL_45;
					}
					type = this._wizard.FinishCompleteButtonType;
					IL_45:
					return this.GetButtonBasedOnType(1, type);
				}
			}

			// Token: 0x17001DEE RID: 7662
			// (get) Token: 0x06006CB9 RID: 27833 RVA: 0x00185B64 File Offset: 0x00183D64
			internal IButtonControl CancelButton
			{
				get
				{
					ButtonType cancelButtonType = this._wizard.CancelButtonType;
					return this.GetButtonBasedOnType(2, cancelButtonType);
				}
			}

			// Token: 0x06006CBA RID: 27834 RVA: 0x00185B85 File Offset: 0x00183D85
			private IButtonControl GetButtonBasedOnType(int pos, ButtonType type)
			{
				switch (type)
				{
				case ButtonType.Button:
					return this._buttons[pos][2];
				case ButtonType.Image:
					return this._buttons[pos][1];
				case ButtonType.Link:
					return this._buttons[pos][0];
				default:
					return null;
				}
			}

			// Token: 0x040039DD RID: 14813
			private Wizard _wizard;

			// Token: 0x040039DE RID: 14814
			private Wizard.WizardTemplateType _templateType;

			// Token: 0x040039DF RID: 14815
			private string _button1ID;

			// Token: 0x040039E0 RID: 14816
			private string _button2ID;

			// Token: 0x040039E1 RID: 14817
			private string _button3ID;

			// Token: 0x040039E2 RID: 14818
			private const string _startNextButtonID = "StartNext";

			// Token: 0x040039E3 RID: 14819
			private const string _stepNextButtonID = "StepNext";

			// Token: 0x040039E4 RID: 14820
			private const string _stepPreviousButtonID = "StepPrevious";

			// Token: 0x040039E5 RID: 14821
			private const string _finishPreviousButtonID = "FinishPrevious";

			// Token: 0x040039E6 RID: 14822
			private const string _finishButtonID = "Finish";

			// Token: 0x040039E7 RID: 14823
			private const string _cancelButtonID = "Cancel";

			// Token: 0x040039E8 RID: 14824
			private TableRow _row;

			// Token: 0x040039E9 RID: 14825
			private IButtonControl[][] _buttons;

			// Token: 0x040039EA RID: 14826
			private bool _button1CausesValidation;
		}

		// Token: 0x020009D5 RID: 2517
		private class DataListItemTemplate : ITemplate
		{
			// Token: 0x06006CBB RID: 27835 RVA: 0x00185BBD File Offset: 0x00183DBD
			internal DataListItemTemplate(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006CBC RID: 27836 RVA: 0x00185BCC File Offset: 0x00183DCC
			public void InstantiateIn(Control container)
			{
				LinkButton linkButton = new LinkButton();
				container.Controls.Add(linkButton);
				linkButton.ID = Wizard.SideBarButtonID;
				if (this._owner.DesignMode)
				{
					linkButton.MergeStyle(this._owner.SideBarButtonStyle);
				}
			}

			// Token: 0x040039EB RID: 14827
			private Wizard _owner;
		}

		// Token: 0x020009D6 RID: 2518
		private class DefaultSideBarTemplate : ITemplate
		{
			// Token: 0x06006CBD RID: 27837 RVA: 0x00185C14 File Offset: 0x00183E14
			internal DefaultSideBarTemplate(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006CBE RID: 27838 RVA: 0x00185C24 File Offset: 0x00183E24
			public void InstantiateIn(Control container)
			{
				Control child;
				if (this._owner.SideBarList == null)
				{
					child = new DataList
					{
						ID = Wizard.DataListID,
						SelectedItemStyle = 
						{
							Font = 
							{
								Bold = true
							}
						},
						ItemTemplate = this._owner.CreateDefaultDataListItemTemplate()
					};
				}
				else
				{
					child = (Control)this._owner.SideBarList;
				}
				container.Controls.Add(child);
			}

			// Token: 0x040039EC RID: 14828
			private Wizard _owner;
		}

		// Token: 0x020009D7 RID: 2519
		internal abstract class BlockControl : WebControl, INamingContainer, INonBindingContainer
		{
			// Token: 0x06006CBF RID: 27839 RVA: 0x00185C94 File Offset: 0x00183E94
			internal BlockControl(Wizard owner)
			{
				this._owner = owner;
				this._table = new WizardDefaultInnerTable();
				this._table.EnableTheming = false;
				this.Controls.Add(this._table);
				TableRow tableRow = new TableRow();
				this._table.Controls.Add(tableRow);
				this._cell = new TableCell();
				this._cell.Height = Unit.Percentage(100.0);
				this._cell.Width = Unit.Percentage(100.0);
				tableRow.Controls.Add(this._cell);
				this.HandleMacIECellHeight();
				base.PreventAutoID();
			}

			// Token: 0x17001DEF RID: 7663
			// (get) Token: 0x06006CC0 RID: 27840 RVA: 0x00185D47 File Offset: 0x00183F47
			protected Table Table
			{
				get
				{
					return this._table;
				}
			}

			// Token: 0x17001DF0 RID: 7664
			// (get) Token: 0x06006CC1 RID: 27841 RVA: 0x00185D4F File Offset: 0x00183F4F
			internal TableCell InnerCell
			{
				get
				{
					return this._cell;
				}
			}

			// Token: 0x06006CC2 RID: 27842 RVA: 0x000C9AE5 File Offset: 0x000C7CE5
			protected override Style CreateControlStyle()
			{
				return new TableItemStyle(this.ViewState);
			}

			// Token: 0x06006CC3 RID: 27843 RVA: 0x00061169 File Offset: 0x0005F369
			public override void Focus()
			{
				throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
				{
					base.GetType().Name
				}));
			}

			// Token: 0x06006CC4 RID: 27844 RVA: 0x00185D57 File Offset: 0x00183F57
			internal void HandleMacIECellHeight()
			{
				if (!this._owner.DesignMode && this._owner.IsMacIE5)
				{
					this._cell.Height = Unit.Pixel(1);
				}
			}

			// Token: 0x06006CC5 RID: 27845 RVA: 0x000B0C65 File Offset: 0x000AEE65
			protected internal override void Render(HtmlTextWriter writer)
			{
				this.RenderContents(writer);
			}

			// Token: 0x06006CC6 RID: 27846 RVA: 0x00185D84 File Offset: 0x00183F84
			internal void SetEnableTheming()
			{
				this._cell.EnableTheming = this._owner.EnableTheming;
			}

			// Token: 0x040039ED RID: 14829
			private Table _table;

			// Token: 0x040039EE RID: 14830
			internal TableCell _cell;

			// Token: 0x040039EF RID: 14831
			internal Wizard _owner;
		}

		// Token: 0x020009D8 RID: 2520
		private class InternalTableCell : TableCell, INamingContainer, INonBindingContainer
		{
			// Token: 0x06006CC7 RID: 27847 RVA: 0x00185D9C File Offset: 0x00183F9C
			internal InternalTableCell(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006CC8 RID: 27848 RVA: 0x00185DAB File Offset: 0x00183FAB
			protected override void AddAttributesToRender(HtmlTextWriter writer)
			{
				if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
				{
					base.ControlStyle.AddAttributesToRender(writer, this);
				}
			}

			// Token: 0x040039F0 RID: 14832
			protected Wizard _owner;
		}

		// Token: 0x020009D9 RID: 2521
		private class AccessibleTableCell : Wizard.InternalTableCell
		{
			// Token: 0x06006CC9 RID: 27849 RVA: 0x00185DCF File Offset: 0x00183FCF
			internal AccessibleTableCell(Wizard owner) : base(owner)
			{
			}

			// Token: 0x06006CCA RID: 27850 RVA: 0x00185DD8 File Offset: 0x00183FD8
			protected internal override void RenderChildren(HtmlTextWriter writer)
			{
				ControlRenderingHelper.WriteSkipLinkStart(writer, this.RenderingCompatibility, this._owner.DesignMode, this._owner.SkipLinkText, base.SpacerImageUrl, this._owner.ClientID);
				base.RenderChildren(writer);
				ControlRenderingHelper.WriteSkipLinkEnd(writer, this._owner.DesignMode, this._owner.SkipLinkText, this._owner.ClientID);
			}
		}

		// Token: 0x020009DA RID: 2522
		internal class BaseContentTemplateContainer : Wizard.BlockControl
		{
			// Token: 0x06006CCB RID: 27851 RVA: 0x00185E48 File Offset: 0x00184048
			internal BaseContentTemplateContainer(Wizard owner, bool useInnerTable) : base(owner)
			{
				this._useInnerTable = useInnerTable;
				if (useInnerTable)
				{
					base.Table.Width = Unit.Percentage(100.0);
					base.Table.Height = Unit.Percentage(100.0);
					return;
				}
				this.Controls.Clear();
			}

			// Token: 0x06006CCC RID: 27852 RVA: 0x00185EA4 File Offset: 0x001840A4
			internal void AddChildControl(Control c)
			{
				this.Container.Controls.Add(c);
			}

			// Token: 0x17001DF1 RID: 7665
			// (get) Token: 0x06006CCD RID: 27853 RVA: 0x00185EB7 File Offset: 0x001840B7
			internal Control Container
			{
				get
				{
					if (!this._useInnerTable)
					{
						return this;
					}
					return base.InnerCell;
				}
			}

			// Token: 0x040039F1 RID: 14833
			private bool _useInnerTable;
		}

		// Token: 0x020009DB RID: 2523
		internal class BaseNavigationTemplateContainer : WebControl, INamingContainer, INonBindingContainer
		{
			// Token: 0x06006CCE RID: 27854 RVA: 0x00185EC9 File Offset: 0x001840C9
			internal BaseNavigationTemplateContainer(Wizard owner)
			{
				this._owner = owner;
			}

			// Token: 0x17001DF2 RID: 7666
			// (get) Token: 0x06006CCF RID: 27855 RVA: 0x00185ED8 File Offset: 0x001840D8
			internal Wizard Owner
			{
				get
				{
					return this._owner;
				}
			}

			// Token: 0x06006CD0 RID: 27856 RVA: 0x00185EE0 File Offset: 0x001840E0
			internal void ApplyButtonStyle(Style finishStyle, Style prevStyle, Style nextStyle, Style cancelStyle)
			{
				if (this.FinishButton != null)
				{
					this.ApplyButtonStyleInternal(this.FinishButton, finishStyle);
				}
				if (this.PreviousButton != null)
				{
					this.ApplyButtonStyleInternal(this.PreviousButton, prevStyle);
				}
				if (this.NextButton != null)
				{
					this.ApplyButtonStyleInternal(this.NextButton, nextStyle);
				}
				if (this.CancelButton != null)
				{
					this.ApplyButtonStyleInternal(this.CancelButton, cancelStyle);
				}
			}

			// Token: 0x06006CD1 RID: 27857 RVA: 0x00185F44 File Offset: 0x00184144
			protected void ApplyButtonStyleInternal(IButtonControl control, Style buttonStyle)
			{
				WebControl webControl = control as WebControl;
				if (webControl != null)
				{
					webControl.ApplyStyle(buttonStyle);
					webControl.ControlStyle.MergeWith(this.Owner.NavigationButtonStyle);
				}
			}

			// Token: 0x06006CD2 RID: 27858 RVA: 0x00061169 File Offset: 0x0005F369
			public override void Focus()
			{
				throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
				{
					base.GetType().Name
				}));
			}

			// Token: 0x06006CD3 RID: 27859 RVA: 0x00185F78 File Offset: 0x00184178
			internal void RegisterButtonCommandEvents()
			{
				this.Owner.RegisterCommandEvents(this.NextButton);
				this.Owner.RegisterCommandEvents(this.FinishButton);
				this.Owner.RegisterCommandEvents(this.PreviousButton);
				this.Owner.RegisterCommandEvents(this.CancelButton);
			}

			// Token: 0x17001DF3 RID: 7667
			// (get) Token: 0x06006CD4 RID: 27860 RVA: 0x00185FC9 File Offset: 0x001841C9
			// (set) Token: 0x06006CD5 RID: 27861 RVA: 0x00185FF6 File Offset: 0x001841F6
			internal IButtonControl CancelButton
			{
				get
				{
					if (this._cancelButton != null)
					{
						return this._cancelButton;
					}
					this._cancelButton = (this.FindControl(Wizard.CancelButtonID) as IButtonControl);
					return this._cancelButton;
				}
				set
				{
					this._cancelButton = value;
				}
			}

			// Token: 0x17001DF4 RID: 7668
			// (get) Token: 0x06006CD6 RID: 27862 RVA: 0x00185FFF File Offset: 0x001841FF
			// (set) Token: 0x06006CD7 RID: 27863 RVA: 0x0018602C File Offset: 0x0018422C
			internal virtual IButtonControl NextButton
			{
				get
				{
					if (this._nextButton != null)
					{
						return this._nextButton;
					}
					this._nextButton = (this.FindControl(Wizard.StepNextButtonID) as IButtonControl);
					return this._nextButton;
				}
				set
				{
					this._nextButton = value;
				}
			}

			// Token: 0x17001DF5 RID: 7669
			// (get) Token: 0x06006CD8 RID: 27864 RVA: 0x00186035 File Offset: 0x00184235
			// (set) Token: 0x06006CD9 RID: 27865 RVA: 0x00186062 File Offset: 0x00184262
			internal virtual IButtonControl PreviousButton
			{
				get
				{
					if (this._previousButton != null)
					{
						return this._previousButton;
					}
					this._previousButton = (this.FindControl(Wizard.StepPreviousButtonID) as IButtonControl);
					return this._previousButton;
				}
				set
				{
					this._previousButton = value;
				}
			}

			// Token: 0x17001DF6 RID: 7670
			// (get) Token: 0x06006CDA RID: 27866 RVA: 0x0018606B File Offset: 0x0018426B
			// (set) Token: 0x06006CDB RID: 27867 RVA: 0x00186098 File Offset: 0x00184298
			internal IButtonControl FinishButton
			{
				get
				{
					if (this._finishButton != null)
					{
						return this._finishButton;
					}
					this._finishButton = (this.FindControl(Wizard.FinishButtonID) as IButtonControl);
					return this._finishButton;
				}
				set
				{
					this._finishButton = value;
				}
			}

			// Token: 0x06006CDC RID: 27868 RVA: 0x001860A1 File Offset: 0x001842A1
			internal void SetEnableTheming()
			{
				this.EnableTheming = this._owner.EnableTheming;
			}

			// Token: 0x06006CDD RID: 27869 RVA: 0x000B0C65 File Offset: 0x000AEE65
			protected internal override void Render(HtmlTextWriter writer)
			{
				this.RenderContents(writer);
			}

			// Token: 0x040039F2 RID: 14834
			private IButtonControl _finishButton;

			// Token: 0x040039F3 RID: 14835
			private IButtonControl _previousButton;

			// Token: 0x040039F4 RID: 14836
			private IButtonControl _nextButton;

			// Token: 0x040039F5 RID: 14837
			private IButtonControl _cancelButton;

			// Token: 0x040039F6 RID: 14838
			private Wizard _owner;
		}

		// Token: 0x020009DC RID: 2524
		private class FinishNavigationTemplateContainer : Wizard.BaseNavigationTemplateContainer
		{
			// Token: 0x06006CDE RID: 27870 RVA: 0x001860B4 File Offset: 0x001842B4
			internal FinishNavigationTemplateContainer(Wizard owner) : base(owner)
			{
			}

			// Token: 0x17001DF7 RID: 7671
			// (get) Token: 0x06006CDF RID: 27871 RVA: 0x001860BD File Offset: 0x001842BD
			// (set) Token: 0x06006CE0 RID: 27872 RVA: 0x001860EA File Offset: 0x001842EA
			internal override IButtonControl PreviousButton
			{
				get
				{
					if (this._previousButton != null)
					{
						return this._previousButton;
					}
					this._previousButton = (this.FindControl(Wizard.FinishPreviousButtonID) as IButtonControl);
					return this._previousButton;
				}
				set
				{
					this._previousButton = value;
				}
			}

			// Token: 0x040039F7 RID: 14839
			private IButtonControl _previousButton;
		}

		// Token: 0x020009DD RID: 2525
		private class StartNavigationTemplateContainer : Wizard.BaseNavigationTemplateContainer
		{
			// Token: 0x06006CE1 RID: 27873 RVA: 0x001860B4 File Offset: 0x001842B4
			internal StartNavigationTemplateContainer(Wizard owner) : base(owner)
			{
			}

			// Token: 0x17001DF8 RID: 7672
			// (get) Token: 0x06006CE2 RID: 27874 RVA: 0x001860F3 File Offset: 0x001842F3
			// (set) Token: 0x06006CE3 RID: 27875 RVA: 0x00186120 File Offset: 0x00184320
			internal override IButtonControl NextButton
			{
				get
				{
					if (this._nextButton != null)
					{
						return this._nextButton;
					}
					this._nextButton = (this.FindControl(Wizard.StartNextButtonID) as IButtonControl);
					return this._nextButton;
				}
				set
				{
					this._nextButton = value;
				}
			}

			// Token: 0x040039F8 RID: 14840
			private IButtonControl _nextButton;
		}

		// Token: 0x020009DE RID: 2526
		private class StepNavigationTemplateContainer : Wizard.BaseNavigationTemplateContainer
		{
			// Token: 0x06006CE4 RID: 27876 RVA: 0x001860B4 File Offset: 0x001842B4
			internal StepNavigationTemplateContainer(Wizard owner) : base(owner)
			{
			}
		}
	}
}
