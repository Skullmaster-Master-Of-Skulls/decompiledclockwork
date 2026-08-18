using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FEB RID: 4075
	[RequiredScript(typeof(MaterialRipple))]
	[EmbeddedSkin("Input", typeof(RadInputControl))]
	[RequiredScript(typeof(Core))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadInputControl))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadInputControl))]
	[EmbeddedSkin("Input", "Default", typeof(RadInputControl))]
	public abstract class RadInputControl : RadWebControl, IPostBackEventHandler, INamingContainer, IEditableTextControl, ITextControl
	{
		// Token: 0x17003216 RID: 12822
		// (get) Token: 0x06009EBD RID: 40637 RVA: 0x002357BE File Offset: 0x002339BE
		// (set) Token: 0x06009EBE RID: 40638 RVA: 0x002357E9 File Offset: 0x002339E9
		[DefaultValue(true)]
		[Description("Enable or Disable the single input rendering mode, 'true' by default")]
		[NotifyParentProperty(true)]
		public virtual bool EnableSingleInputRendering
		{
			get
			{
				return this.ViewState["EnableSingleInputRendering"] == null || (bool)this.ViewState["EnableSingleInputRendering"];
			}
			set
			{
				this.ViewState["EnableSingleInputRendering"] = value;
			}
		}

		// Token: 0x14000179 RID: 377
		// (add) Token: 0x06009EBF RID: 40639 RVA: 0x00235801 File Offset: 0x00233A01
		// (remove) Token: 0x06009EC0 RID: 40640 RVA: 0x00235814 File Offset: 0x00233A14
		[Description("Occurs after all child controls of the RadDateInput control have been created.")]
		public event EventHandler ChildrenCreated
		{
			add
			{
				base.Events.AddHandler(RadInputControl.EventChildrenCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadInputControl.EventChildrenCreated, value);
			}
		}

		// Token: 0x06009EC1 RID: 40641 RVA: 0x00235828 File Offset: 0x00233A28
		protected virtual void OnChildrenCreated()
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadInputControl.EventChildrenCreated];
			if (eventHandler != null)
			{
				eventHandler(this, new EventArgs());
			}
		}

		// Token: 0x17003217 RID: 12823
		// (get) Token: 0x06009EC2 RID: 40642 RVA: 0x0023585A File Offset: 0x00233A5A
		// (set) Token: 0x06009EC3 RID: 40643 RVA: 0x0023588E File Offset: 0x00233A8E
		[NotifyParentProperty(true)]
		[Themeable(true)]
		[Description("The label of the control.")]
		[DefaultValue("")]
		[Category("Appearance")]
		[Localizable(true)]
		public virtual string Label
		{
			get
			{
				if (this.ViewState["Label"] == null)
				{
					return "";
				}
				return HttpUtility.HtmlEncode((string)this.ViewState["Label"]);
			}
			set
			{
				this.ViewState["Label"] = HttpUtility.HtmlDecode(value);
			}
		}

		// Token: 0x17003218 RID: 12824
		// (get) Token: 0x06009EC4 RID: 40644 RVA: 0x002358A6 File Offset: 0x00233AA6
		[NotifyParentProperty(true)]
		protected override string TagName
		{
			get
			{
				if (this.isOnlyInputRendered() || this.EnableSingleInputRendering)
				{
					return "span";
				}
				return "div";
			}
		}

		// Token: 0x17003219 RID: 12825
		// (get) Token: 0x06009EC5 RID: 40645 RVA: 0x002358C3 File Offset: 0x00233AC3
		// (set) Token: 0x06009EC6 RID: 40646 RVA: 0x002358F2 File Offset: 0x00233AF2
		[Category("Appearance")]
		[Description("Css class of the label")]
		[Themeable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("riLabel")]
		public virtual string LabelCssClass
		{
			get
			{
				if (this.ViewState["LabelCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["LabelCssClass"];
			}
			set
			{
				this.ViewState["LabelCssClass"] = value;
			}
		}

		// Token: 0x1700321A RID: 12826
		// (get) Token: 0x06009EC7 RID: 40647 RVA: 0x00235908 File Offset: 0x00233B08
		// (set) Token: 0x06009EC8 RID: 40648 RVA: 0x00235931 File Offset: 0x00233B31
		[DefaultValue(false)]
		[Category("Behavior")]
		[Themeable(false)]
		[ClientControlProperty]
		[Description("Automatically post back to the server after text is modified.")]
		[NotifyParentProperty(true)]
		public virtual bool AutoPostBack
		{
			get
			{
				object obj = this.ViewState["AutoPostBack"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x1700321B RID: 12827
		// (get) Token: 0x06009EC9 RID: 40649 RVA: 0x00235949 File Offset: 0x00233B49
		// (set) Token: 0x06009ECA RID: 40650 RVA: 0x00235974 File Offset: 0x00233B74
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Themeable(false)]
		[DefaultValue(typeof(AutoCompleteType), "None")]
		[Description("AutoCompleteType")]
		public virtual AutoCompleteType AutoCompleteType
		{
			get
			{
				if (this.ViewState["AutoCompleteType"] == null)
				{
					return AutoCompleteType.None;
				}
				return (AutoCompleteType)this.ViewState["AutoCompleteType"];
			}
			set
			{
				if (value < AutoCompleteType.None || value > AutoCompleteType.Search)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["AutoCompleteType"] = value;
			}
		}

		// Token: 0x1700321C RID: 12828
		// (get) Token: 0x06009ECB RID: 40651 RVA: 0x002359A0 File Offset: 0x00233BA0
		// (set) Token: 0x06009ECC RID: 40652 RVA: 0x002359BC File Offset: 0x00233BBC
		protected internal string TextElementID
		{
			get
			{
				if (string.IsNullOrEmpty(this._textElementId))
				{
					return this.ClientID;
				}
				return this._textElementId;
			}
			protected set
			{
				this._textElementId = value;
			}
		}

		// Token: 0x1700321D RID: 12829
		// (get) Token: 0x06009ECD RID: 40653 RVA: 0x002359C8 File Offset: 0x00233BC8
		// (set) Token: 0x06009ECE RID: 40654 RVA: 0x002359F1 File Offset: 0x00233BF1
		[DefaultValue(false)]
		[Themeable(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Whether the control causes validation to fire.")]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = this.ViewState["CausesValidation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x1700321E RID: 12830
		// (get) Token: 0x06009ECF RID: 40655 RVA: 0x00235A0C File Offset: 0x00233C0C
		// (set) Token: 0x06009ED0 RID: 40656 RVA: 0x00235A35 File Offset: 0x00233C35
		[Category("Behavior")]
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Description("The maximum number of characters that can be entered.")]
		public virtual int MaxLength
		{
			get
			{
				object obj = this.ViewState["MaxLength"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["MaxLength"] = value;
			}
		}

		// Token: 0x1700321F RID: 12831
		// (get) Token: 0x06009ED1 RID: 40657 RVA: 0x00235A5C File Offset: 0x00233C5C
		// (set) Token: 0x06009ED2 RID: 40658 RVA: 0x00235A85 File Offset: 0x00233C85
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "ReadOnly")]
		[Themeable(false)]
		[Category("Behavior")]
		[Description("Whether the text in the control can be changed or not.")]
		public virtual bool ReadOnly
		{
			get
			{
				object obj = this.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ReadOnly"] = value;
			}
		}

		// Token: 0x17003220 RID: 12832
		// (get) Token: 0x06009ED3 RID: 40659 RVA: 0x00235A9D File Offset: 0x00233C9D
		// (set) Token: 0x06009ED4 RID: 40660 RVA: 0x00235ACC File Offset: 0x00233CCC
		[DefaultValue("")]
		[ClientControlProperty]
		[Description("Message shown when the text is empty.")]
		[NotifyParentProperty(true)]
		[Themeable(true)]
		[Category("Behavior")]
		public virtual string EmptyMessage
		{
			get
			{
				if (this.ViewState["EmptyMessage"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["EmptyMessage"];
			}
			set
			{
				this.ViewState["EmptyMessage"] = value;
			}
		}

		// Token: 0x17003221 RID: 12833
		// (get) Token: 0x06009ED5 RID: 40661 RVA: 0x00235ADF File Offset: 0x00233CDF
		// (set) Token: 0x06009ED6 RID: 40662 RVA: 0x00235B0A File Offset: 0x00233D0A
		[Description("Whether the text in the control selected on focus and how.")]
		[DefaultValue(typeof(SelectionOnFocus), "None")]
		[Themeable(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		public virtual SelectionOnFocus SelectionOnFocus
		{
			get
			{
				if (this.ViewState["SelectionOnFocus"] == null)
				{
					return SelectionOnFocus.None;
				}
				return (SelectionOnFocus)this.ViewState["SelectionOnFocus"];
			}
			set
			{
				this.ViewState["SelectionOnFocus"] = value;
			}
		}

		// Token: 0x17003222 RID: 12834
		// (get) Token: 0x06009ED7 RID: 40663 RVA: 0x00235B22 File Offset: 0x00233D22
		// (set) Token: 0x06009ED8 RID: 40664 RVA: 0x00235B4E File Offset: 0x00233D4E
		[DefaultValue(100)]
		[Description("Time, in milliseconds, the InvalidStyle should be displayd. Must be a positive integer.")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Themeable(true)]
		[NotifyParentProperty(true)]
		public virtual int InvalidStyleDuration
		{
			get
			{
				if (this.ViewState["InvalidStyleDuration"] == null)
				{
					return 100;
				}
				return (int)this.ViewState["InvalidStyleDuration"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("InvalidStyleDuration", "Must be a positive integer.");
				}
				this.ViewState["InvalidStyleDuration"] = value;
			}
		}

		// Token: 0x17003223 RID: 12835
		// (get) Token: 0x06009ED9 RID: 40665 RVA: 0x00235B7A File Offset: 0x00233D7A
		private bool SaveTextViewState
		{
			get
			{
				return base.Events[RadInputControl.EventTextChanged] != null || !base.Enabled || !this.Visible || !(base.GetType() == typeof(RadInputControl));
			}
		}

		// Token: 0x17003224 RID: 12836
		// (get) Token: 0x06009EDA RID: 40666 RVA: 0x00235BB8 File Offset: 0x00233DB8
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x17003225 RID: 12837
		// (get) Token: 0x06009EDB RID: 40667 RVA: 0x00235BBC File Offset: 0x00233DBC
		protected HttpBrowserCapabilities Browser
		{
			get
			{
				return this.Context.Request.Browser;
			}
		}

		// Token: 0x17003226 RID: 12838
		// (get) Token: 0x06009EDC RID: 40668 RVA: 0x00235BCE File Offset: 0x00233DCE
		[Category("Client-side events")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public InputClientEvents ClientEvents
		{
			get
			{
				if (this._events == null)
				{
					this._events = new InputClientEvents(this.ViewState);
				}
				return this._events;
			}
		}

		// Token: 0x17003227 RID: 12839
		// (get) Token: 0x06009EDD RID: 40669 RVA: 0x00235BEF File Offset: 0x00233DEF
		// (set) Token: 0x06009EDE RID: 40670 RVA: 0x00235C1A File Offset: 0x00233E1A
		[Category("Appearance")]
		[Description("Whether the button is displayed")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		public virtual bool ShowButton
		{
			get
			{
				return this.ViewState["ShowButton"] != null && (bool)this.ViewState["ShowButton"];
			}
			set
			{
				this.ViewState["ShowButton"] = value;
			}
		}

		// Token: 0x17003228 RID: 12840
		// (get) Token: 0x06009EDF RID: 40671 RVA: 0x00235C32 File Offset: 0x00233E32
		// (set) Token: 0x06009EE0 RID: 40672 RVA: 0x00235C5D File Offset: 0x00233E5D
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(InputButtonsPosition), "Right")]
		[Category("Appearance")]
		public virtual InputButtonsPosition ButtonsPosition
		{
			get
			{
				if (this.ViewState["ButtonsPosition"] == null)
				{
					return InputButtonsPosition.Right;
				}
				return (InputButtonsPosition)this.ViewState["ButtonsPosition"];
			}
			set
			{
				this.ViewState["ButtonsPosition"] = value;
			}
		}

		// Token: 0x17003229 RID: 12841
		// (get) Token: 0x06009EE1 RID: 40673 RVA: 0x00235C75 File Offset: 0x00233E75
		// (set) Token: 0x06009EE2 RID: 40674 RVA: 0x00235CA4 File Offset: 0x00233EA4
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		public virtual string ButtonCssClass
		{
			get
			{
				if (this.ViewState["ButtonCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["ButtonCssClass"];
			}
			set
			{
				this.ViewState["ButtonCssClass"] = value;
			}
		}

		// Token: 0x1700322A RID: 12842
		// (get) Token: 0x06009EE3 RID: 40675 RVA: 0x00235CB7 File Offset: 0x00233EB7
		// (set) Token: 0x06009EE4 RID: 40676 RVA: 0x00235CE6 File Offset: 0x00233EE6
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public virtual string WrapperCssClass
		{
			get
			{
				if (this.ViewState["WrapperCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["WrapperCssClass"];
			}
			set
			{
				this.ViewState["WrapperCssClass"] = value;
			}
		}

		// Token: 0x1700322B RID: 12843
		// (get) Token: 0x06009EE5 RID: 40677 RVA: 0x00235CFC File Offset: 0x00233EFC
		// (set) Token: 0x06009EE6 RID: 40678 RVA: 0x00235D29 File Offset: 0x00233F29
		[Category("Appearance")]
		[Bindable(true, BindingDirection.TwoWay)]
		[Description("The text value.")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string Text
		{
			get
			{
				string text = (string)this.ViewState["Text"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x1700322C RID: 12844
		// (get) Token: 0x06009EE7 RID: 40679 RVA: 0x00235D3C File Offset: 0x00233F3C
		public virtual string ValidationText
		{
			get
			{
				if (string.IsNullOrEmpty(this.Text))
				{
					return string.Empty;
				}
				return this.Text;
			}
		}

		// Token: 0x1700322D RID: 12845
		// (get) Token: 0x06009EE8 RID: 40680 RVA: 0x00235D58 File Offset: 0x00233F58
		// (set) Token: 0x06009EE9 RID: 40681 RVA: 0x00235DAE File Offset: 0x00233FAE
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string DisplayText
		{
			get
			{
				if (!string.IsNullOrEmpty(this._displayText))
				{
					return this._displayText;
				}
				if (!base.DesignMode)
				{
					if (!string.IsNullOrEmpty(this.Text))
					{
						return this.Text;
					}
					if (!string.IsNullOrEmpty(this.EmptyMessage))
					{
						return this.EmptyMessage;
					}
				}
				return "";
			}
			set
			{
				this._displayText = value;
			}
		}

		// Token: 0x1700322E RID: 12846
		// (get) Token: 0x06009EEA RID: 40682 RVA: 0x00235DB8 File Offset: 0x00233FB8
		// (set) Token: 0x06009EEB RID: 40683 RVA: 0x00235DE5 File Offset: 0x00233FE5
		[NotifyParentProperty(true)]
		[Themeable(false)]
		[Description("The group that should be validated when the control causes a postback.")]
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

		// Token: 0x1400017A RID: 378
		// (add) Token: 0x06009EEC RID: 40684 RVA: 0x00235DF8 File Offset: 0x00233FF8
		// (remove) Token: 0x06009EED RID: 40685 RVA: 0x00235E0B File Offset: 0x0023400B
		[Category("Action")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(RadInputControl.EventTextChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadInputControl.EventTextChanged, value);
			}
		}

		// Token: 0x1700322F RID: 12847
		// (get) Token: 0x06009EEE RID: 40686 RVA: 0x00235E1E File Offset: 0x0023401E
		// (set) Token: 0x06009EEF RID: 40687 RVA: 0x00235E49 File Offset: 0x00234049
		[Category("Appearance")]
		[DefaultValue(true)]
		public bool Display
		{
			get
			{
				return this.ViewState["Display"] == null || (bool)this.ViewState["Display"];
			}
			set
			{
				this.ViewState["Display"] = value;
			}
		}

		// Token: 0x17003230 RID: 12848
		// (get) Token: 0x06009EF0 RID: 40688 RVA: 0x00235E61 File Offset: 0x00234061
		// (set) Token: 0x06009EF1 RID: 40689 RVA: 0x00235E82 File Offset: 0x00234082
		[ClientPropertyName("enableAriaSupport")]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
		[DefaultValue(false)]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17003231 RID: 12849
		// (get) Token: 0x06009EF2 RID: 40690 RVA: 0x00235E9A File Offset: 0x0023409A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Description("The style applied to control when text property is empty.")]
		[Category("Appearance")]
		public InputStyle EmptyMessageStyle
		{
			get
			{
				if (this.emptyMessageStyle == null)
				{
					this.emptyMessageStyle = new InputStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.emptyMessageStyle).TrackViewState();
					}
				}
				return this.emptyMessageStyle;
			}
		}

		// Token: 0x17003232 RID: 12850
		// (get) Token: 0x06009EF3 RID: 40691 RVA: 0x00235EC8 File Offset: 0x002340C8
		[Description("The style applied to control when is read only.")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public InputStyle ReadOnlyStyle
		{
			get
			{
				if (this.readOnlyStyle == null)
				{
					this.readOnlyStyle = new InputStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.readOnlyStyle).TrackViewState();
					}
				}
				return this.readOnlyStyle;
			}
		}

		// Token: 0x17003233 RID: 12851
		// (get) Token: 0x06009EF4 RID: 40692 RVA: 0x00235EF6 File Offset: 0x002340F6
		[Description("The style applied to control when is focused.")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public InputStyle FocusedStyle
		{
			get
			{
				if (this.focusedStyle == null)
				{
					this.focusedStyle = new InputStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.focusedStyle).TrackViewState();
					}
				}
				return this.focusedStyle;
			}
		}

		// Token: 0x17003234 RID: 12852
		// (get) Token: 0x06009EF5 RID: 40693 RVA: 0x00235F24 File Offset: 0x00234124
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[Description("The style applied to control when is disabled.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public InputStyle DisabledStyle
		{
			get
			{
				if (this.disabledStyle == null)
				{
					this.disabledStyle = new InputStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.disabledStyle).TrackViewState();
					}
				}
				return this.disabledStyle;
			}
		}

		// Token: 0x17003235 RID: 12853
		// (get) Token: 0x06009EF6 RID: 40694 RVA: 0x00235F52 File Offset: 0x00234152
		[NotifyParentProperty(true)]
		[Description("The style applied ti control when the text is invalid.")]
		[Category("Appearance")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public InputStyle InvalidStyle
		{
			get
			{
				if (this.invalidStyle == null)
				{
					this.invalidStyle = new InputStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.invalidStyle).TrackViewState();
					}
				}
				return this.invalidStyle;
			}
		}

		// Token: 0x17003236 RID: 12854
		// (get) Token: 0x06009EF7 RID: 40695 RVA: 0x00235F80 File Offset: 0x00234180
		// (set) Token: 0x06009EF8 RID: 40696 RVA: 0x00235F88 File Offset: 0x00234188
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Set to true if you like the input to be rendered in invalid state.")]
		public bool Invalid
		{
			get
			{
				return this._Invalid;
			}
			set
			{
				this._Invalid = value;
			}
		}

		// Token: 0x17003237 RID: 12855
		// (get) Token: 0x06009EF9 RID: 40697 RVA: 0x00235F91 File Offset: 0x00234191
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The style applied to control when is hovered.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		public InputStyle HoveredStyle
		{
			get
			{
				if (this.hoveredStyle == null)
				{
					this.hoveredStyle = new InputStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.hoveredStyle).TrackViewState();
					}
				}
				return this.hoveredStyle;
			}
		}

		// Token: 0x17003238 RID: 12856
		// (get) Token: 0x06009EFA RID: 40698 RVA: 0x00235FBF File Offset: 0x002341BF
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("The style applied to control when is enabled.")]
		[DefaultValue(null)]
		public InputStyle EnabledStyle
		{
			get
			{
				if (this.enabledStyle == null)
				{
					this.enabledStyle = new InputStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.enabledStyle).TrackViewState();
					}
				}
				return this.enabledStyle;
			}
		}

		// Token: 0x17003239 RID: 12857
		// (get) Token: 0x06009EFB RID: 40699 RVA: 0x00235FED File Offset: 0x002341ED
		// (set) Token: 0x06009EFC RID: 40700 RVA: 0x00235FF5 File Offset: 0x002341F5
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x1700323A RID: 12858
		// (get) Token: 0x06009EFD RID: 40701 RVA: 0x00236000 File Offset: 0x00234200
		// (set) Token: 0x06009EFE RID: 40702 RVA: 0x002360AF File Offset: 0x002342AF
		[Category("Appearance")]
		[Description("Gets or sets width of the Label")]
		public Unit LabelWidth
		{
			get
			{
				if (this.ViewState["LabelWidth"] == null)
				{
					if (!this.EnableSingleInputRendering || this.ResolvedRenderMode == RenderMode.Lightweight)
					{
						return Unit.Empty;
					}
					Unit unit = this.CalculateWrapperWidth();
					if (unit.Type == UnitType.Pixel)
					{
						return Unit.Pixel((int)(unit.Value * 0.4));
					}
					if (unit.Type == UnitType.Em)
					{
						return new Unit(unit.Value * 0.4, UnitType.Em);
					}
					if (unit.Type == UnitType.Percentage)
					{
						return Unit.Percentage(40.0);
					}
				}
				return (Unit)this.ViewState["LabelWidth"];
			}
			set
			{
				this.ViewState["LabelWidth"] = value;
			}
		}

		// Token: 0x1700323B RID: 12859
		// (get) Token: 0x06009EFF RID: 40703 RVA: 0x002360C7 File Offset: 0x002342C7
		// (set) Token: 0x06009F00 RID: 40704 RVA: 0x002360F2 File Offset: 0x002342F2
		[Description("Gets or sets whether the textbox width should include the textbox paddings and borders. The default value is FALSE, i.e. the textbox will actually be wider than expected.")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public bool EnableOldBoxModel
		{
			get
			{
				return this.ViewState["EnableOldBoxModel"] != null && (bool)this.ViewState["EnableOldBoxModel"];
			}
			set
			{
				this.ViewState["EnableOldBoxModel"] = value;
			}
		}

		// Token: 0x1700323C RID: 12860
		// (get) Token: 0x06009F01 RID: 40705 RVA: 0x0023610A File Offset: 0x0023430A
		// (set) Token: 0x06009F02 RID: 40706 RVA: 0x00236135 File Offset: 0x00234335
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Gets or sets whether the textbox width should be recalculated and reset in pixels on the client. This prevents textbox expansion in Internet Explorer if the textbox content is too long, but can cause unexpected side effects, depending on the particular scenario. The default value is TRUE.")]
		[Category("Appearance")]
		public bool ShouldResetWidthInPixels
		{
			get
			{
				return this.ViewState["ShouldResetWidthInPixels"] == null || (bool)this.ViewState["ShouldResetWidthInPixels"];
			}
			set
			{
				this.ViewState["ShouldResetWidthInPixels"] = value;
			}
		}

		// Token: 0x1700323D RID: 12861
		// (get) Token: 0x06009F03 RID: 40707 RVA: 0x0023614D File Offset: 0x0023434D
		// (set) Token: 0x06009F04 RID: 40708 RVA: 0x00236155 File Offset: 0x00234355
		[NotifyParentProperty(true)]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x1700323E RID: 12862
		// (get) Token: 0x06009F05 RID: 40709 RVA: 0x0023615E File Offset: 0x0023435E
		// (set) Token: 0x06009F06 RID: 40710 RVA: 0x00236166 File Offset: 0x00234366
		[NotifyParentProperty(true)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x1700323F RID: 12863
		// (get) Token: 0x06009F07 RID: 40711 RVA: 0x0023616F File Offset: 0x0023436F
		// (set) Token: 0x06009F08 RID: 40712 RVA: 0x00236178 File Offset: 0x00234378
		[NotifyParentProperty(true)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				if (value == Color.Empty)
				{
					this.EnabledStyle.BackColor = Color.Empty;
					this.HoveredStyle.BackColor = Color.Empty;
					this.FocusedStyle.BackColor = Color.Empty;
					this.EmptyMessageStyle.BackColor = Color.Empty;
					this.ReadOnlyStyle.BackColor = Color.Empty;
					this.DisabledStyle.BackColor = Color.Empty;
					this.InvalidStyle.BackColor = Color.Empty;
				}
			}
		}

		// Token: 0x17003240 RID: 12864
		// (get) Token: 0x06009F09 RID: 40713 RVA: 0x00236209 File Offset: 0x00234409
		// (set) Token: 0x06009F0A RID: 40714 RVA: 0x00236214 File Offset: 0x00234414
		[NotifyParentProperty(true)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
				if (value == Color.Empty)
				{
					this.EnabledStyle.BorderColor = Color.Empty;
					this.HoveredStyle.BorderColor = Color.Empty;
					this.FocusedStyle.BorderColor = Color.Empty;
					this.EmptyMessageStyle.BorderColor = Color.Empty;
					this.ReadOnlyStyle.BorderColor = Color.Empty;
					this.DisabledStyle.BorderColor = Color.Empty;
					this.InvalidStyle.BorderColor = Color.Empty;
				}
			}
		}

		// Token: 0x17003241 RID: 12865
		// (get) Token: 0x06009F0B RID: 40715 RVA: 0x002362A5 File Offset: 0x002344A5
		// (set) Token: 0x06009F0C RID: 40716 RVA: 0x002362AD File Offset: 0x002344AD
		[NotifyParentProperty(true)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x17003242 RID: 12866
		// (get) Token: 0x06009F0D RID: 40717 RVA: 0x002362B6 File Offset: 0x002344B6
		// (set) Token: 0x06009F0E RID: 40718 RVA: 0x002362BE File Offset: 0x002344BE
		[NotifyParentProperty(true)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x17003243 RID: 12867
		// (get) Token: 0x06009F0F RID: 40719 RVA: 0x002362C7 File Offset: 0x002344C7
		// (set) Token: 0x06009F10 RID: 40720 RVA: 0x002362CF File Offset: 0x002344CF
		[NotifyParentProperty(true)]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x17003244 RID: 12868
		// (get) Token: 0x06009F11 RID: 40721 RVA: 0x002362D8 File Offset: 0x002344D8
		// (set) Token: 0x06009F12 RID: 40722 RVA: 0x002362E0 File Offset: 0x002344E0
		[NotifyParentProperty(true)]
		public override bool EnableAjaxSkinRendering
		{
			get
			{
				return base.EnableAjaxSkinRendering;
			}
			set
			{
				base.EnableAjaxSkinRendering = value;
			}
		}

		// Token: 0x17003245 RID: 12869
		// (get) Token: 0x06009F13 RID: 40723 RVA: 0x002362E9 File Offset: 0x002344E9
		// (set) Token: 0x06009F14 RID: 40724 RVA: 0x002362F1 File Offset: 0x002344F1
		[NotifyParentProperty(true)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17003246 RID: 12870
		// (get) Token: 0x06009F15 RID: 40725 RVA: 0x002362FA File Offset: 0x002344FA
		// (set) Token: 0x06009F16 RID: 40726 RVA: 0x00236302 File Offset: 0x00234502
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return base.EnableEmbeddedBaseStylesheet;
			}
			set
			{
				base.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x17003247 RID: 12871
		// (get) Token: 0x06009F17 RID: 40727 RVA: 0x0023630B File Offset: 0x0023450B
		// (set) Token: 0x06009F18 RID: 40728 RVA: 0x00236313 File Offset: 0x00234513
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedScripts
		{
			get
			{
				return base.EnableEmbeddedScripts;
			}
			set
			{
				base.EnableEmbeddedScripts = value;
			}
		}

		// Token: 0x17003248 RID: 12872
		// (get) Token: 0x06009F19 RID: 40729 RVA: 0x0023631C File Offset: 0x0023451C
		// (set) Token: 0x06009F1A RID: 40730 RVA: 0x00236324 File Offset: 0x00234524
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return base.EnableEmbeddedSkins;
			}
			set
			{
				base.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x17003249 RID: 12873
		// (get) Token: 0x06009F1B RID: 40731 RVA: 0x0023632D File Offset: 0x0023452D
		// (set) Token: 0x06009F1C RID: 40732 RVA: 0x00236335 File Offset: 0x00234535
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x1700324A RID: 12874
		// (get) Token: 0x06009F1D RID: 40733 RVA: 0x0023633E File Offset: 0x0023453E
		// (set) Token: 0x06009F1E RID: 40734 RVA: 0x00236346 File Offset: 0x00234546
		[NotifyParentProperty(true)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}

		// Token: 0x1700324B RID: 12875
		// (get) Token: 0x06009F1F RID: 40735 RVA: 0x0023634F File Offset: 0x0023454F
		// (set) Token: 0x06009F20 RID: 40736 RVA: 0x00236357 File Offset: 0x00234557
		[NotifyParentProperty(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x1700324C RID: 12876
		// (get) Token: 0x06009F21 RID: 40737 RVA: 0x00236360 File Offset: 0x00234560
		// (set) Token: 0x06009F22 RID: 40738 RVA: 0x00236368 File Offset: 0x00234568
		[NotifyParentProperty(true)]
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
			set
			{
				base.EnableViewState = value;
			}
		}

		// Token: 0x1700324D RID: 12877
		// (get) Token: 0x06009F23 RID: 40739 RVA: 0x00236371 File Offset: 0x00234571
		// (set) Token: 0x06009F24 RID: 40740 RVA: 0x00236379 File Offset: 0x00234579
		[NotifyParentProperty(true)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x1700324E RID: 12878
		// (get) Token: 0x06009F25 RID: 40741 RVA: 0x00236382 File Offset: 0x00234582
		// (set) Token: 0x06009F26 RID: 40742 RVA: 0x0023638A File Offset: 0x0023458A
		[NotifyParentProperty(true)]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
			}
		}

		// Token: 0x1700324F RID: 12879
		// (get) Token: 0x06009F27 RID: 40743 RVA: 0x00236393 File Offset: 0x00234593
		// (set) Token: 0x06009F28 RID: 40744 RVA: 0x0023639B File Offset: 0x0023459B
		[NotifyParentProperty(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x06009F29 RID: 40745 RVA: 0x002363A4 File Offset: 0x002345A4
		public RadInputControl()
		{
		}

		// Token: 0x06009F2B RID: 40747 RVA: 0x002363E4 File Offset: 0x002345E4
		protected virtual bool isOnlyInputRendered()
		{
			return !this.ShowButton && string.IsNullOrEmpty(this.Label) && !base.DesignMode;
		}

		// Token: 0x17003250 RID: 12880
		// (get) Token: 0x06009F2C RID: 40748 RVA: 0x00236406 File Offset: 0x00234606
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009F2D RID: 40749 RVA: 0x00236409 File Offset: 0x00234609
		protected virtual bool shouldRenderWhiteSpace()
		{
			return !base.DesignMode;
		}

		// Token: 0x06009F2E RID: 40750 RVA: 0x00236414 File Offset: 0x00234614
		protected virtual bool IsMultiLine()
		{
			return false;
		}

		// Token: 0x17003251 RID: 12881
		// (get) Token: 0x06009F2F RID: 40751 RVA: 0x00236417 File Offset: 0x00234617
		protected internal bool EmptySkin
		{
			get
			{
				return string.IsNullOrEmpty(base.RuntimeSkin);
			}
		}

		// Token: 0x06009F30 RID: 40752 RVA: 0x00236424 File Offset: 0x00234624
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.MaxLength > 0 && !this.IsMultiLine())
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, this.MaxLength.ToString(NumberFormatInfo.InvariantInfo));
			}
			if (!base.DesignMode)
			{
				this.ApplyCurrentCssClass(writer);
			}
			if (this.ResolvedRenderMode != RenderMode.Lightweight)
			{
				if (this.isOnlyInputRendered() || (this.EnableSingleInputRendering && !base.DesignMode))
				{
					if (!string.IsNullOrEmpty(this.setStyle))
					{
						writer.AddAttribute("style", this.setStyle);
					}
					if (!this.EnableSingleInputRendering)
					{
						if (!this.EnabledStyle.Width.IsEmpty)
						{
							writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.EnabledStyle.Width.ToString(CultureInfo.InvariantCulture));
						}
						else if (!this.setWidth.IsEmpty)
						{
							writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.setWidth.ToString(CultureInfo.InvariantCulture));
						}
						else if (!this.defaultWidth.IsEmpty)
						{
							writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.defaultWidth.ToString(CultureInfo.InvariantCulture));
						}
					}
					if (!this.setHeight.IsEmpty && this.setHeight.Type != UnitType.Percentage)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.setHeight.ToString(CultureInfo.InvariantCulture));
					}
				}
				else
				{
					writer.AddStyleAttribute("width", "100%");
					if (!this.setHeight.IsEmpty && this.setHeight.Type != UnitType.Percentage)
					{
						writer.AddStyleAttribute("height", this.setHeight.ToString(CultureInfo.InvariantCulture));
					}
				}
			}
			if (!this.AutoPostBack && this.Page != null && this.RegisterWithScriptManager)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.UniqueID, string.Empty);
			}
			if (this.ReadOnly)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
			}
			if (!base.IsEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			if (this.AccessKey.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (this.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.ToolTip.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			if (base.ControlStyleCreated && !base.DesignMode)
			{
				base.ControlStyle.AddAttributesToRender(writer, this);
			}
			System.Web.UI.AttributeCollection attributes = base.Attributes;
			foreach (object obj in attributes.Keys)
			{
				string text = (string)obj;
				if (text.ToLower() != "style")
				{
					writer.AddAttribute(text, attributes[text]);
				}
			}
			if (base.DesignMode)
			{
				this.SetDesignTimeAttributes(writer);
			}
			if (this.AutoCompleteType != AutoCompleteType.None && this.Context != null && this.Context.Request.Browser["supportsVCard"] == "true")
			{
				if (this.AutoCompleteType == AutoCompleteType.Search)
				{
					writer.AddAttribute("vcard_name", "search");
				}
				else if (this.AutoCompleteType == AutoCompleteType.HomeCountryRegion)
				{
					writer.AddAttribute("vcard_name", "HomeCountry");
				}
				else if (this.AutoCompleteType == AutoCompleteType.BusinessCountryRegion)
				{
					writer.AddAttribute("vcard_name", "BusinessCountry");
				}
				else
				{
					string text2 = Enum.Format(typeof(AutoCompleteType), this.AutoCompleteType, "G");
					if (text2.StartsWith("Business"))
					{
						text2 = text2.Insert(8, ".");
					}
					else if (text2.StartsWith("Home"))
					{
						text2 = text2.Insert(4, ".");
					}
					writer.AddAttribute("vcard_name", "vCard." + text2);
				}
			}
			if (this.AutoCompleteType == AutoCompleteType.Disabled)
			{
				writer.AddAttribute("autocomplete", "off");
			}
		}

		// Token: 0x06009F31 RID: 40753 RVA: 0x0023683C File Offset: 0x00234A3C
		protected virtual void ApplyCurrentCssClass(HtmlTextWriter writer)
		{
			if (this.Invalid && !string.IsNullOrEmpty(this.InvalidStyle.CssClass.Trim()))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.InvalidStyle.CssClass);
				return;
			}
			if (this.ReadOnly && !string.IsNullOrEmpty(this.ReadOnlyStyle.CssClass.Trim()))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.ReadOnlyStyle.CssClass);
				return;
			}
			if (!this.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.DisabledStyle.CssClass);
				return;
			}
			if (string.IsNullOrEmpty(this.EmptyMessage) && !string.IsNullOrEmpty(this.EnabledStyle.CssClass.Trim()))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.EnabledStyle.CssClass);
				return;
			}
			if (!string.IsNullOrEmpty(this.EmptyMessageStyle.CssClass.Trim()))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.EmptyMessageStyle.CssClass);
			}
		}

		// Token: 0x06009F32 RID: 40754 RVA: 0x00236930 File Offset: 0x00234B30
		public override void Focus()
		{
			if (this.Page != null)
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				if (current != null)
				{
					current.SetFocus(this.ClientID);
				}
			}
			this._focused = true;
		}

		// Token: 0x06009F33 RID: 40755 RVA: 0x00236968 File Offset: 0x00234B68
		protected virtual void SetStyleClasses()
		{
			if (!string.IsNullOrEmpty(base.RuntimeSkin))
			{
				this.HoveredStyle.CssClass = this.FormatCssClass("riTextBox riHover", this.HoveredStyle.CssClass);
				this.InvalidStyle.CssClass = this.FormatCssClass("riTextBox riError", this.InvalidStyle.CssClass);
				this.DisabledStyle.CssClass = this.FormatCssClass("riTextBox riDisabled", this.DisabledStyle.CssClass);
				this.EnabledStyle.CssClass = this.FormatCssClass("riTextBox riEnabled", this.EnabledStyle.CssClass);
				this.FocusedStyle.CssClass = this.FormatCssClass("riTextBox riFocused", this.FocusedStyle.CssClass);
				this.EmptyMessageStyle.CssClass = this.FormatCssClass("riTextBox riEmpty", this.EmptyMessageStyle.CssClass);
				this.ReadOnlyStyle.CssClass = this.FormatCssClass("riTextBox riRead", this.ReadOnlyStyle.CssClass);
				this.LabelCssClass = this.FormatCssClass("riLabel", this.LabelCssClass);
			}
		}

		// Token: 0x06009F34 RID: 40756 RVA: 0x00236A84 File Offset: 0x00234C84
		protected virtual string StylesToClient()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(InputUtil.GetStyle("HoveredStyle", this.HoveredStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("InvalidStyle", this.InvalidStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("DisabledStyle", this.DisabledStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("FocusedStyle", this.FocusedStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("EmptyMessageStyle", this.EmptyMessageStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("ReadOnlyStyle", this.ReadOnlyStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("EnabledStyle", this.EnabledStyle, base.Style));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x17003252 RID: 12882
		// (get) Token: 0x06009F35 RID: 40757 RVA: 0x00236BBC File Offset: 0x00234DBC
		protected virtual bool Resizable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009F36 RID: 40758 RVA: 0x00236BC0 File Offset: 0x00234DC0
		protected string FormatCssClass(string prefix, string userDefined)
		{
			string text;
			if (prefix == "RadInput")
			{
				text = (this.EmptySkin ? prefix : string.Format("{0} {0}_{1}", prefix, base.RuntimeSkin));
				if (this.IsMultiLine())
				{
					text = text + " " + string.Format("{0} {0}_{1}", prefix + "Multiline", base.RuntimeSkin);
				}
				if (this.Resizable)
				{
					text += " riResizable";
				}
				if (this.WrapperCssClass.Length > 0)
				{
					text = text + " " + this.WrapperCssClass;
				}
			}
			else
			{
				text = prefix;
			}
			userDefined = Regex.Replace(userDefined, prefix + "_\\S+\\s?", "");
			if (userDefined.IndexOf(text) >= 0)
			{
				return userDefined;
			}
			if (string.IsNullOrEmpty(userDefined))
			{
				return text;
			}
			return string.Format("{0} {1}", text, userDefined);
		}

		// Token: 0x06009F37 RID: 40759 RVA: 0x00236CA1 File Offset: 0x00234EA1
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			this.MergeStyles();
			this.SetStyleClasses();
		}

		// Token: 0x17003253 RID: 12883
		// (get) Token: 0x06009F38 RID: 40760 RVA: 0x00236CB8 File Offset: 0x00234EB8
		// (set) Token: 0x06009F39 RID: 40761 RVA: 0x00236D07 File Offset: 0x00234F07
		protected virtual Unit defaultWidth
		{
			get
			{
				if (this._defaultWidthIsNull)
				{
					if (this.ResolvedRenderMode != RenderMode.Lightweight)
					{
						this._defaultWidth = Unit.Pixel(160);
						this._defaultWidthIsNull = false;
					}
					else
					{
						this._defaultWidth = Unit.Empty;
						this._defaultWidthIsNull = false;
					}
				}
				return this._defaultWidth;
			}
			set
			{
				this._defaultWidth = value;
				this._defaultWidthIsNull = false;
			}
		}

		// Token: 0x06009F3A RID: 40762 RVA: 0x00236D18 File Offset: 0x00234F18
		protected virtual void SetDefaultSize()
		{
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return;
			}
			if (!base.ControlStyle.Width.IsEmpty)
			{
				this.setWidth = base.ControlStyle.Width;
			}
			else if (this.EnabledStyle.Width.IsEmpty)
			{
				base.ControlStyle.Width = this.defaultWidth;
			}
			if (!base.ControlStyle.Height.IsEmpty)
			{
				this.setHeight = base.ControlStyle.Height;
			}
			if (!this.EnabledStyle.Height.IsEmpty)
			{
				this.setHeight = this.EnabledStyle.Height;
			}
		}

		// Token: 0x06009F3B RID: 40763 RVA: 0x00236DC9 File Offset: 0x00234FC9
		protected virtual void MergeStyles()
		{
			this.SetDefaultSize();
			base.ControlStyle.MergeWith(this.EnabledStyle);
			this.EnabledStyle.CopyFrom(base.ControlStyle);
			this.MergeAuxiliaryStyles();
			base.ControlStyle.Reset();
		}

		// Token: 0x06009F3C RID: 40764 RVA: 0x00236E04 File Offset: 0x00235004
		protected virtual void MergeAuxiliaryStyles()
		{
			this.HoveredStyle.MergeWith(this.EnabledStyle);
			this.EmptyMessageStyle.MergeWith(this.EnabledStyle);
			this.ReadOnlyStyle.MergeWith(this.EnabledStyle);
			this.DisabledStyle.MergeWith(this.EnabledStyle);
			this.FocusedStyle.MergeWith(this.EnabledStyle);
			this.InvalidStyle.MergeWith(this.EnabledStyle);
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.HoveredStyle.Font.MergeWith(this.EnabledStyle.Font);
				this.EmptyMessageStyle.Font.MergeWith(this.EnabledStyle.Font);
				this.ReadOnlyStyle.Font.MergeWith(this.EnabledStyle.Font);
				this.DisabledStyle.Font.MergeWith(this.EnabledStyle.Font);
				this.FocusedStyle.Font.MergeWith(this.EnabledStyle.Font);
				this.InvalidStyle.Font.MergeWith(this.EnabledStyle.Font);
			}
		}

		// Token: 0x06009F3D RID: 40765 RVA: 0x00236F28 File Offset: 0x00235128
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadInputControl.EventTextChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06009F3E RID: 40766 RVA: 0x00236F58 File Offset: 0x00235158
		protected Unit CalculateWrapperWidth()
		{
			if (!this.EnabledStyle.Width.IsEmpty)
			{
				return this.EnabledStyle.Width;
			}
			if (!this.setWidth.IsEmpty)
			{
				return this.setWidth;
			}
			if (!this.defaultWidth.IsEmpty)
			{
				return this.defaultWidth;
			}
			return Unit.Empty;
		}

		// Token: 0x06009F3F RID: 40767 RVA: 0x00236FB8 File Offset: 0x002351B8
		protected virtual Unit CalculateInputWidth()
		{
			Unit result = this.CalculateWrapperWidth();
			if (!string.IsNullOrEmpty(this.Label))
			{
				if (result.Type == UnitType.Pixel)
				{
					if (this.LabelWidth.Type == UnitType.Pixel)
					{
						float num = (float)result.Value - (float)this.LabelWidth.Value;
						return new Unit((double)num, this.LabelWidth.Type);
					}
					if (this.LabelWidth.Type == UnitType.Percentage)
					{
						double n = 100.0 - this.LabelWidth.Value;
						return Unit.Percentage(n);
					}
				}
				else if (result.Type == UnitType.Em)
				{
					if (this.LabelWidth.Type == UnitType.Em)
					{
						float num2 = (float)result.Value - (float)this.LabelWidth.Value;
						return new Unit((double)num2, this.LabelWidth.Type);
					}
					if (this.LabelWidth.Type == UnitType.Percentage)
					{
						double n2 = 100.0 - this.LabelWidth.Value;
						return Unit.Percentage(n2);
					}
				}
				else if (result.Type == UnitType.Percentage && this.LabelWidth.Type == UnitType.Percentage)
				{
					double n3 = 100.0 - this.LabelWidth.Value;
					return Unit.Percentage(n3);
				}
			}
			return result;
		}

		// Token: 0x06009F40 RID: 40768 RVA: 0x00237124 File Offset: 0x00235324
		protected virtual void RenderBeginTagSingleInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_wrapper");
			if (!this.Display)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			if (this.ToolTip.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			string absolutePositionValue = InputUtil.GetAbsolutePositionValue(base.Style);
			if (!string.IsNullOrEmpty(absolutePositionValue))
			{
				writer.AddAttribute("style", absolutePositionValue);
			}
			string str = (this.ResolvedRenderMode == RenderMode.Lightweight) ? string.Empty : "riSingle ";
			writer.AddAttribute(HtmlTextWriterAttribute.Class, str + this.GetOffsetAdditionalClasses() + this.FormatCssClass("RadInput", this.CssClass));
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "static");
				this.SetDefaultSize();
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.CalculateWrapperWidth().ToString(CultureInfo.InvariantCulture));
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString(CultureInfo.InvariantCulture));
				writer.AddStyleAttribute(HtmlTextWriterStyle.MarginRight, "15px");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				return;
			}
			if (this.ResolvedRenderMode != RenderMode.Lightweight)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.CalculateWrapperWidth().ToString(CultureInfo.InvariantCulture));
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				return;
			}
			if (!this.EnabledStyle.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.EnabledStyle.Width.ToString(CultureInfo.InvariantCulture));
			}
			if (!this.EnabledStyle.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.EnabledStyle.Height.ToString(CultureInfo.InvariantCulture));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06009F41 RID: 40769 RVA: 0x002372EC File Offset: 0x002354EC
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				BaseClass.RenderVersionStamp(writer);
			}
			if (this.EnableSingleInputRendering && !base.DesignMode)
			{
				this.RenderBeginTagSingleInput(writer);
				return;
			}
			writer.AddAttribute("id", this.ClientID + "_wrapper");
			if (!this.Display)
			{
				writer.AddStyleAttribute("display", "none");
			}
			writer.AddAttribute("class", this.FormatCssClass("RadInput", this.CssClass));
			if (base.DesignMode)
			{
				this.SetDefaultSize();
			}
			if (base.Style["width"] != null && this.setWidth.IsEmpty)
			{
				this.setWidth = new Unit(base.Style["width"].Replace("!important", "").Trim());
			}
			if (base.Style["height"] != null && this.setHeight.IsEmpty)
			{
				this.setHeight = new Unit(base.Style["height"].Replace("!important", "").Trim());
			}
			this.setStyle = InputUtil.GetAbsolutePositionValue(base.Style);
			if (base.DesignMode && this.setStyle != null)
			{
				string[] array = this.setStyle.Split(new char[]
				{
					';'
				});
				foreach (string text in array)
				{
					if (text.IndexOf(":") != -1)
					{
						writer.AddStyleAttribute(text.Split(new char[]
						{
							':'
						})[0], text.Split(new char[]
						{
							':'
						})[1]);
					}
				}
			}
			if (this.isOnlyInputRendered())
			{
				if (this.shouldRenderWhiteSpace() && !this.Browser.IsBrowser("Safari") && !this.Browser.IsBrowser("Chrome"))
				{
					writer.AddStyleAttribute("white-space", "nowrap");
				}
				else if (!base.DesignMode && (this.Browser.IsBrowser("Safari") || this.Browser.IsBrowser("Chrome")))
				{
					writer.AddStyleAttribute("white-space", "normal");
				}
				if (base.DesignMode)
				{
					writer.AddStyleAttribute("display", "inline");
					writer.AddStyleAttribute("zoom", "1");
					if (!this.setWidth.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.setWidth.ToString(CultureInfo.InvariantCulture));
					}
					else if (!this.defaultWidth.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.defaultWidth.ToString(CultureInfo.InvariantCulture));
					}
					if (!this.setHeight.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.setHeight.ToString(CultureInfo.InvariantCulture));
					}
				}
			}
			else
			{
				if (this.Display || base.DesignMode)
				{
					this.RenderBrowserSpecificStyles(writer);
				}
				if (!string.IsNullOrEmpty(this.setStyle))
				{
					writer.AddAttribute("style", this.setStyle);
				}
				if (this.ResolvedRenderMode != RenderMode.Lightweight)
				{
					if (!this.EnabledStyle.Width.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.EnabledStyle.Width.ToString(CultureInfo.InvariantCulture));
					}
					else if (!this.setWidth.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.setWidth.ToString(CultureInfo.InvariantCulture));
					}
					else if (!this.defaultWidth.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.defaultWidth.ToString(CultureInfo.InvariantCulture));
					}
					if (!this.setHeight.IsEmpty && this.setHeight.Type == UnitType.Percentage)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.setHeight.ToString(CultureInfo.InvariantCulture));
					}
					else if (!base.DesignMode && (this.Browser.IsBrowser("Gecko") || this.Browser.IsBrowser("Firefox")) && !this.Browser.IsBrowser("Safari") && !this.Browser.IsBrowser("Chrome"))
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "20px");
					}
				}
			}
			writer.RenderBeginTag(this.TagName);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
		}

		// Token: 0x06009F42 RID: 40770 RVA: 0x00237760 File Offset: 0x00235960
		protected virtual void RenderBrowserSpecificStyles(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.AddStyleAttribute("display", "inline-block");
				writer.AddStyleAttribute("_display", "inline");
				writer.AddStyleAttribute("_zoom", "1");
				return;
			}
			if (this.Browser.IsBrowser("IE") && this.Context.Request.Browser.MajorVersion < 8)
			{
				writer.AddStyleAttribute("display", "inline");
				writer.AddStyleAttribute("zoom", "1");
				return;
			}
			if (this.Browser.IsBrowser("IE") || this.Browser.IsBrowser("Opera") || this.Browser.IsBrowser("Safari") || this.Browser.IsBrowser("Chrome"))
			{
				writer.AddStyleAttribute("display", "inline-block");
				return;
			}
			if (this.Browser.IsBrowser("Gecko") || this.Browser.IsBrowser("Firefox"))
			{
				writer.AddStyleAttribute("display", "-moz-inline-stack");
				return;
			}
			if (this.Browser.IsBrowser("Safari") || this.Browser.IsBrowser("Chrome"))
			{
				writer.AddStyleAttribute("white-space", "normal");
			}
		}

		// Token: 0x06009F43 RID: 40771 RVA: 0x002378B4 File Offset: 0x00235AB4
		protected virtual void RenderContentsSingleInputFields(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			if (!this.IsMultiLine())
			{
				if (!string.IsNullOrEmpty(this.DisplayText) && !base.DesignMode)
				{
					base.Attributes[HtmlTextWriterAttribute.Value.ToString().ToLower()] = this.DisplayText;
				}
				base.Attributes[HtmlTextWriterAttribute.Type.ToString().ToLower()] = "text";
			}
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(this.TagKey.ToString().ToLower(CultureInfo.InvariantCulture));
			if (this.IsMultiLine() && !string.IsNullOrEmpty(this.DisplayText))
			{
				writer.Write(Environment.NewLine);
				HttpUtility.HtmlEncode(this.DisplayText, writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009F44 RID: 40772 RVA: 0x00237996 File Offset: 0x00235B96
		protected virtual void RenderInnerWrapperContent(HtmlTextWriter writer)
		{
			if (this.ButtonsPosition == InputButtonsPosition.Left)
			{
				base.RenderContents(writer);
			}
			this.RenderContentsSingleInputFields(writer);
			if (this.ButtonsPosition == InputButtonsPosition.Right)
			{
				base.RenderContents(writer);
			}
		}

		// Token: 0x06009F45 RID: 40773 RVA: 0x002379C0 File Offset: 0x00235BC0
		protected virtual void RenderContentsSingleInput(HtmlTextWriter writer)
		{
			this.RenderLabel(writer, this.ClientID);
			if (!string.IsNullOrEmpty(this.Label))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "riContentWrapper");
				if (this.ResolvedRenderMode != RenderMode.Lightweight)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.CalculateInputWidth().ToString(CultureInfo.InvariantCulture));
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			this.RenderInnerWrapperContent(writer);
			if (!string.IsNullOrEmpty(this.Label))
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06009F46 RID: 40774 RVA: 0x00237A3C File Offset: 0x00235C3C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.EnableSingleInputRendering && !base.DesignMode)
			{
				this.RenderContentsSingleInput(writer);
				return;
			}
			if (this.isOnlyInputRendered())
			{
				this.RenderInputElements(writer);
				this.RenderPasswordStrengthIndicator(writer, false);
				return;
			}
			writer.AddAttribute("cellpadding", "0");
			writer.AddAttribute("cellspacing", "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderCollapse, "collapse");
			if (base.DesignMode || (!this.Browser.IsBrowser("Gecko") && !this.Browser.IsBrowser("Firefox")) || ((this.Browser.IsBrowser("Gecko") || this.Browser.IsBrowser("Firefox")) && !this.setWidth.IsEmpty && this.setWidth.Type == UnitType.Percentage))
			{
				writer.AddStyleAttribute("width", "100%");
			}
			else
			{
				writer.AddStyleAttribute("width", this.setWidth.IsEmpty ? this.defaultWidth.ToString() : this.setWidth.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "riTable");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (!string.IsNullOrEmpty(this.Label))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderLabel(writer, this.ClientID);
				writer.RenderEndTag();
			}
			if (this.ButtonsPosition == InputButtonsPosition.Left)
			{
				base.RenderContents(writer);
			}
			writer.AddAttribute("class", "riCell");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			if (!base.DesignMode && !this.Browser.IsBrowser("Safari") && !this.Browser.IsBrowser("Chrome"))
			{
				writer.AddStyleAttribute("white-space", "nowrap");
			}
			if (!base.DesignMode && (this.Browser.IsBrowser("Safari") || this.Browser.IsBrowser("Chrome")))
			{
				writer.AddStyleAttribute("white-space", "normal");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderInputElements(writer);
			writer.RenderEndTag();
			if (this.ButtonsPosition == InputButtonsPosition.Right)
			{
				base.RenderContents(writer);
			}
			this.RenderPasswordStrengthIndicator(writer, true);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009F47 RID: 40775 RVA: 0x00237C90 File Offset: 0x00235E90
		private void RenderInputElements(HtmlTextWriter writer)
		{
			string tagName = this.TagKey.ToString().ToLower(CultureInfo.InvariantCulture);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			if (!this.IsMultiLine() && !base.DesignMode)
			{
				base.Attributes[HtmlTextWriterAttribute.Value.ToString().ToLower()] = this.DisplayText;
				base.Attributes[HtmlTextWriterAttribute.Type.ToString().ToLower()] = "text";
			}
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(tagName);
			if (this.IsMultiLine())
			{
				this.RenderTextAreaContents(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009F48 RID: 40776 RVA: 0x00237D4A File Offset: 0x00235F4A
		protected virtual void RenderTextAreaContents(HtmlTextWriter writer)
		{
			HttpUtility.HtmlEncode(this.DisplayText, writer);
		}

		// Token: 0x06009F49 RID: 40777 RVA: 0x00237D58 File Offset: 0x00235F58
		protected virtual void RenderPasswordStrengthIndicator(HtmlTextWriter writer, bool inTable)
		{
		}

		// Token: 0x06009F4A RID: 40778 RVA: 0x00237D5A File Offset: 0x00235F5A
		protected virtual string GetOffsetAdditionalClasses()
		{
			if (!this.ShowButton)
			{
				return "";
			}
			if (this.ButtonsPosition == InputButtonsPosition.Left)
			{
				return " riContButton riButtonSwap ";
			}
			return " riContButton ";
		}

		// Token: 0x06009F4B RID: 40779 RVA: 0x00237D80 File Offset: 0x00235F80
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (!this.ShowButton)
			{
				base.RenderChildren(writer);
				return;
			}
			this.EnsureChildControls();
			if (this.EnableSingleInputRendering && !base.DesignMode)
			{
				base.RenderChildren(writer);
				return;
			}
			writer.AddAttribute("class", "riBtn");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			base.RenderChildren(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06009F4C RID: 40780 RVA: 0x00237DE0 File Offset: 0x00235FE0
		protected virtual void RenderLabel(HtmlTextWriter writer, string forID)
		{
			if (string.IsNullOrEmpty(this.Label))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.LabelCssClass))
			{
				writer.AddAttribute("class", this.LabelCssClass);
			}
			if (this.LabelWidth != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.LabelWidth.ToString(CultureInfo.InvariantCulture));
			}
			writer.AddAttribute("for", forID);
			writer.AddAttribute("id", this.ClientID + "_Label");
			writer.RenderBeginTag("label");
			writer.Write(this.Label);
			writer.RenderEndTag();
		}

		// Token: 0x06009F4D RID: 40781 RVA: 0x00237E8A File Offset: 0x0023608A
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			this.ClientEvents.DescribeEvents(descriptor);
			this.DescribeStyles(descriptor);
			this.DescribeProperties(descriptor);
		}

		// Token: 0x06009F4E RID: 40782 RVA: 0x00237EBE File Offset: 0x002360BE
		private void DescribeStyles(IScriptDescriptor descriptor)
		{
			descriptor.AddScriptProperty("styles", this.StylesToClient());
		}

		// Token: 0x06009F4F RID: 40783 RVA: 0x00237ED4 File Offset: 0x002360D4
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			string postBackEventReference = this.GetPostBackEventReference();
			if (!string.IsNullOrEmpty(postBackEventReference))
			{
				descriptor.AddProperty("_postBackEventReferenceScript", postBackEventReference);
			}
			descriptor.AddProperty("enabled", base.IsEnabled);
			descriptor.AddProperty("_focused", this._focused);
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("enableAriaSupport", this.EnableAriaSupport);
			}
			if (this.CausesValidation)
			{
				descriptor.AddProperty("_causesValidation", this.CausesValidation);
			}
			if (!string.IsNullOrEmpty(this.ValidationGroup))
			{
				descriptor.AddProperty("_validationGroup", this.ValidationGroup);
			}
			if (this.Invalid)
			{
				descriptor.AddProperty("_holdsValidValue", false);
			}
			if (this.ResolvedRenderMode != RenderMode.Classic)
			{
				descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			}
			this.DescribeValueAndTextProperties(descriptor);
		}

		// Token: 0x06009F50 RID: 40784 RVA: 0x00237FC1 File Offset: 0x002361C1
		protected virtual void DescribeValueAndTextProperties(IScriptDescriptor descriptor)
		{
			descriptor.AddProperty("_validationText", this.ValidationText);
			descriptor.AddProperty("_displayText", this.DisplayText);
			descriptor.AddProperty("_initialValueAsText", this.Text);
		}

		// Token: 0x06009F51 RID: 40785 RVA: 0x00237FF8 File Offset: 0x002361F8
		protected virtual string GetPostBackEventReference()
		{
			if (this.Page != null)
			{
				string text = string.Empty;
				PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
				if (this.CausesValidation)
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = this.ValidationGroup;
				}
				if (this.Page.Form != null)
				{
					postBackOptions.PerformValidation = false;
					postBackOptions.AutoPostBack = true;
				}
				text = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, false);
				text = text.Replace("('", "(\"");
				return text.Replace(")'", ")\"");
			}
			return string.Empty;
		}

		// Token: 0x06009F52 RID: 40786 RVA: 0x00238090 File Offset: 0x00236290
		protected virtual void SetDesignTimeAttributes(HtmlTextWriter writer)
		{
			if (this.Invalid)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.FormatCssClass("riTextBox riError", this.CssClass));
				writer.AddAttribute(HtmlTextWriterAttribute.Style, InputUtil.GetStyle(this.InvalidStyle, base.Style));
			}
			else if (this.ReadOnly && this.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.FormatCssClass("riTextBox riRead", this.CssClass));
				writer.AddAttribute(HtmlTextWriterAttribute.Style, InputUtil.GetStyle(this.ReadOnlyStyle, base.Style));
			}
			else if (this.Enabled && string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.EmptyMessage))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.FormatCssClass("riTextBox riEmpty", this.CssClass));
				writer.AddAttribute(HtmlTextWriterAttribute.Style, InputUtil.GetStyle(this.EmptyMessageStyle, base.Style));
			}
			else if (!this.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.FormatCssClass("riTextBox riDisabled", this.CssClass));
				writer.AddAttribute(HtmlTextWriterAttribute.Style, InputUtil.GetStyle(this.DisabledStyle, base.Style));
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.FormatCssClass("riTextBox riEnabled", this.CssClass));
				writer.AddAttribute(HtmlTextWriterAttribute.Style, InputUtil.GetStyle(this.EnabledStyle, base.Style));
			}
			if (!base.DesignMode)
			{
				base.Attributes[HtmlTextWriterAttribute.Value.ToString().ToLower()] = this.DisplayText;
			}
			this.LabelCssClass = this.FormatCssClass("riLabel", this.LabelCssClass);
		}

		// Token: 0x17003254 RID: 12884
		// (get) Token: 0x06009F53 RID: 40787 RVA: 0x00238228 File Offset: 0x00236428
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual HtmlGenericControl ButtonContainer
		{
			get
			{
				if (this.buttonContainer == null)
				{
					this.buttonContainer = new HtmlGenericControl("a");
				}
				return this.buttonContainer;
			}
		}

		// Token: 0x17003255 RID: 12885
		// (get) Token: 0x06009F54 RID: 40788 RVA: 0x00238248 File Offset: 0x00236448
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual HtmlGenericControl ButtonsLightContainer
		{
			get
			{
				if (this.buttonsLightContainer == null)
				{
					this.buttonsLightContainer = new HtmlGenericControl("span");
					this.buttonsLightContainer.Attributes["class"] = "riSelect";
				}
				return this.buttonsLightContainer;
			}
		}

		// Token: 0x06009F55 RID: 40789 RVA: 0x00238284 File Offset: 0x00236484
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.ShowButton)
			{
				if (!string.IsNullOrEmpty(this.ButtonCssClass))
				{
					this.ButtonContainer.Attributes["class"] = "riButton " + HttpUtility.HtmlEncode(this.ButtonCssClass);
				}
				else
				{
					this.ButtonContainer.Attributes["class"] = "riButton";
				}
				this.ButtonContainer.Attributes["href"] = "#";
				this.ButtonContainer.Attributes["onclick"] = "return false;";
				this.ButtonContainer.Attributes["id"] = this.ClientID + "_GoButton";
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
				htmlGenericControl.InnerHtml = "Button";
				this.ButtonContainer.Controls.Add(htmlGenericControl);
				if (this.ResolvedRenderMode == RenderMode.Lightweight)
				{
					this.ButtonsLightContainer.Controls.Add(this.ButtonContainer);
					this.Controls.Add(this.ButtonsLightContainer);
				}
				else
				{
					this.Controls.Add(this.ButtonContainer);
				}
			}
			base.CreateChildControls();
			this.OnChildrenCreated();
		}

		// Token: 0x06009F56 RID: 40790 RVA: 0x002383CC File Offset: 0x002365CC
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = this.Text;
			string text2 = postCollection[base.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text2))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(text2) as Dictionary<string, object>;
				if (dictionary != null)
				{
					this.LoadClientState(dictionary);
					string text3 = postCollection[postDataKey];
					if (text3 != this.LastSetTextBoxValue)
					{
						RadNumericTextBox radNumericTextBox = this as RadNumericTextBox;
						if (radNumericTextBox != null)
						{
							this.Text = text3.Replace(radNumericTextBox.NumberFormat.DecimalSeparator, ".");
						}
						else
						{
							this.Text = text3;
						}
					}
				}
			}
			return (!this.IsMultiLine() || !(text.Replace("\r\n", "\n") == this.Text.Replace("\r\n", "\n"))) && !text.Equals(this.Text);
		}

		// Token: 0x06009F57 RID: 40791 RVA: 0x002384A3 File Offset: 0x002366A3
		protected override void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack && this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnTextChanged(EventArgs.Empty);
		}

		// Token: 0x06009F58 RID: 40792 RVA: 0x002384D1 File Offset: 0x002366D1
		protected override void RegisterScriptControl()
		{
			if (this.RegisterWithScriptManager)
			{
				base.ScriptManager.RegisterScriptControl<RadWebControl>(this);
			}
		}

		// Token: 0x06009F59 RID: 40793 RVA: 0x002384E8 File Offset: 0x002366E8
		protected override void LoadViewState(object savedState)
		{
			if (!this.EnableViewState)
			{
				return;
			}
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.HoveredStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.InvalidStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.FocusedStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.EnabledStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.DisabledStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.EmptyMessageStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.ReadOnlyStyle).LoadViewState(array[7]);
			}
		}

		// Token: 0x06009F5A RID: 40794 RVA: 0x002385A0 File Offset: 0x002367A0
		protected override object SaveViewState()
		{
			object[] array = new object[8];
			if (!this.SaveTextViewState)
			{
				this.ViewState.SetItemDirty("Text", false);
			}
			array[0] = base.SaveViewState();
			array[1] = ((this.hoveredStyle != null) ? ((IStateManager)this.hoveredStyle).SaveViewState() : null);
			array[2] = ((this.invalidStyle != null) ? ((IStateManager)this.invalidStyle).SaveViewState() : null);
			array[3] = ((this.focusedStyle != null) ? ((IStateManager)this.focusedStyle).SaveViewState() : null);
			array[4] = ((this.enabledStyle != null) ? ((IStateManager)this.enabledStyle).SaveViewState() : null);
			array[5] = ((this.disabledStyle != null) ? ((IStateManager)this.disabledStyle).SaveViewState() : null);
			array[6] = ((this.emptyMessageStyle != null) ? ((IStateManager)this.emptyMessageStyle).SaveViewState() : null);
			array[7] = ((this.readOnlyStyle != null) ? ((IStateManager)this.readOnlyStyle).SaveViewState() : null);
			return array;
		}

		// Token: 0x06009F5B RID: 40795 RVA: 0x00238688 File Offset: 0x00236888
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.hoveredStyle != null)
			{
				((IStateManager)this.hoveredStyle).TrackViewState();
			}
			if (this.invalidStyle != null)
			{
				((IStateManager)this.invalidStyle).TrackViewState();
			}
			if (this.focusedStyle != null)
			{
				((IStateManager)this.focusedStyle).TrackViewState();
			}
			if (this.enabledStyle != null)
			{
				((IStateManager)this.enabledStyle).TrackViewState();
			}
			if (this.disabledStyle != null)
			{
				((IStateManager)this.disabledStyle).TrackViewState();
			}
		}

		// Token: 0x06009F5C RID: 40796 RVA: 0x002386FC File Offset: 0x002368FC
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			if (base.IsEnabled && clientState.ContainsKey("enabled"))
			{
				this.Enabled = (bool)clientState["enabled"];
			}
			if (clientState.ContainsKey("emptyMessage"))
			{
				this.EmptyMessage = (string)clientState["emptyMessage"];
			}
			if (clientState.ContainsKey("valueAsString"))
			{
				this.Text = ((string)clientState["valueAsString"]).Replace("\r\n", "\n").Replace("\n", "\r\n");
			}
			if (clientState.ContainsKey("lastSetTextBoxValue"))
			{
				this.LastSetTextBoxValue = (string)clientState["lastSetTextBoxValue"];
			}
		}

		// Token: 0x17003256 RID: 12886
		// (get) Token: 0x06009F5D RID: 40797 RVA: 0x002387BB File Offset: 0x002369BB
		// (set) Token: 0x06009F5E RID: 40798 RVA: 0x002387C3 File Offset: 0x002369C3
		protected string LastSetTextBoxValue { get; set; }

		// Token: 0x06009F5F RID: 40799 RVA: 0x002387CC File Offset: 0x002369CC
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
		}

		// Token: 0x06009F60 RID: 40800 RVA: 0x002387D0 File Offset: 0x002369D0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<string>(descriptor, "emptyMessage", this.EmptyMessage, "");
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableOldBoxModel", this.EnableOldBoxModel, false);
			base.DescribeProperty<int>(descriptor, "invalidStyleDuration", this.InvalidStyleDuration, 100);
			base.DescribeProperty<object>(descriptor, "selectionOnFocus", this.SelectionOnFocus, Enum.Parse(typeof(SelectionOnFocus), "None"));
			base.DescribeProperty<bool>(descriptor, "shouldResetWidthInPixels", this.ShouldResetWidthInPixels, true);
			base.DescribeProperty<bool>(descriptor, "showButton", this.ShowButton, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009F61 RID: 40801 RVA: 0x00238899 File Offset: 0x00236A99
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002C7F RID: 11391
		private InputClientEvents _events;

		// Token: 0x04002C80 RID: 11392
		private static readonly object EventChildrenCreated = new object();

		// Token: 0x04002C81 RID: 11393
		private static readonly object EventTextChanged = new object();

		// Token: 0x04002C82 RID: 11394
		private InputStyle focusedStyle;

		// Token: 0x04002C83 RID: 11395
		private InputStyle invalidStyle;

		// Token: 0x04002C84 RID: 11396
		private InputStyle hoveredStyle;

		// Token: 0x04002C85 RID: 11397
		private InputStyle enabledStyle;

		// Token: 0x04002C86 RID: 11398
		private InputStyle disabledStyle;

		// Token: 0x04002C87 RID: 11399
		private InputStyle emptyMessageStyle;

		// Token: 0x04002C88 RID: 11400
		private InputStyle readOnlyStyle;

		// Token: 0x04002C89 RID: 11401
		internal bool _focused;

		// Token: 0x04002C8A RID: 11402
		private string _textElementId;

		// Token: 0x04002C8B RID: 11403
		private string _displayText = string.Empty;

		// Token: 0x04002C8C RID: 11404
		private bool _Invalid;

		// Token: 0x04002C8D RID: 11405
		private Unit _defaultWidth = Unit.Pixel(160);

		// Token: 0x04002C8E RID: 11406
		private bool _defaultWidthIsNull = true;

		// Token: 0x04002C8F RID: 11407
		protected Unit setWidth;

		// Token: 0x04002C90 RID: 11408
		protected Unit setHeight;

		// Token: 0x04002C91 RID: 11409
		protected string setStyle;

		// Token: 0x04002C92 RID: 11410
		private HtmlGenericControl buttonContainer;

		// Token: 0x04002C93 RID: 11411
		private HtmlGenericControl buttonsLightContainer;
	}
}
