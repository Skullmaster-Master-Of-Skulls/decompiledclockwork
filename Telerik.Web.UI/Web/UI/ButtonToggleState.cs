using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.ButtonBase;
using Telerik.Web.UI.ButtonNS;

namespace Telerik.Web.UI
{
	// Token: 0x020000EF RID: 239
	[ToolboxItem(false)]
	public class ButtonToggleState : StateManager
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x00024570 File Offset: 0x00022770
		public ButtonToggleState()
		{
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00024578 File Offset: 0x00022778
		public ButtonToggleState(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00024587 File Offset: 0x00022787
		public ButtonToggleState(string text, string cssClass) : this()
		{
			this.Text = text;
			this.CssClass = cssClass;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002459D File Offset: 0x0002279D
		public ButtonToggleState(string text, string cssClass, string value) : this()
		{
			this.Text = text;
			this.CssClass = cssClass;
			this.Value = value;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000245BC File Offset: 0x000227BC
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.Image).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.Icon).LoadViewState(array[2]);
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00024600 File Offset: 0x00022800
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Image).SaveViewState(),
				((IStateManager)this.Icon).SaveViewState()
			};
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002463C File Offset: 0x0002283C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Image).TrackViewState();
			((IStateManager)this.Icon).TrackViewState();
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002465A File Offset: 0x0002285A
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Image.SetDirty();
			this.Icon.SetDirty();
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00024678 File Offset: 0x00022878
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x00024680 File Offset: 0x00022880
		internal RadToggleButton Container
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00024689 File Offset: 0x00022889
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x000246A9 File Offset: 0x000228A9
		[DefaultValue("")]
		[Localizable(true)]
		[Description("Gets or sets the text displayed in the RadButton control.")]
		[Category("Appearance")]
		public string Text
		{
			get
			{
				return ((string)base.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x000246BC File Offset: 0x000228BC
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x000246DC File Offset: 0x000228DC
		[Category("Behavior")]
		[DefaultValue("")]
		[Localizable(true)]
		[Description("Gets or sets optional Value.")]
		public string Value
		{
			get
			{
				return ((string)base.ViewState["Value"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x000246EF File Offset: 0x000228EF
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x00024701 File Offset: 0x00022901
		[Category("Behavior")]
		[Bindable(true)]
		[Description("Gets or sets an optional parameter passed to the Command event along with the associated CommandName.")]
		[Themeable(false)]
		[ClientPropertyName("commandArgument")]
		[DefaultValue("")]
		[ClientControlProperty]
		public string CommandArgument
		{
			get
			{
				return base.GetViewStateValue<string>("CommandArgument", string.Empty);
			}
			set
			{
				base.ViewState["CommandArgument"] = value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00024714 File Offset: 0x00022914
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x00024726 File Offset: 0x00022926
		[DefaultValue("")]
		[ClientPropertyName("commandName")]
		[Themeable(false)]
		[ClientControlProperty]
		[Description("Gets or sets the command name associated with the Button control that is passed to the Command event.")]
		[Category("Behavior")]
		public string CommandName
		{
			get
			{
				return base.GetViewStateValue<string>("CommandName", string.Empty);
			}
			set
			{
				base.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00024739 File Offset: 0x00022939
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0002475A File Offset: 0x0002295A
		[DefaultValue(false)]
		[Description("Gets or sets a bool value indicating whether the ToggleState is selected or not.")]
		[Category("Behavior")]
		[Themeable(false)]
		public bool Selected
		{
			get
			{
				return (bool)(base.ViewState["Selected"] ?? false);
			}
			set
			{
				if (value && this.Container != null)
				{
					this.Container.ClearSelection();
				}
				base.ViewState["Selected"] = value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00024788 File Offset: 0x00022988
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x000247A8 File Offset: 0x000229A8
		[Category("Appearance")]
		[DefaultValue("")]
		[CssClassProperty]
		[Description("Gets or sets the CSS class applied to the RadButton control.")]
		public string CssClass
		{
			get
			{
				return (base.ViewState["CssClass"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x000247BB File Offset: 0x000229BB
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x000247DB File Offset: 0x000229DB
		[DefaultValue("")]
		[Description("Gets or sets the CSS class applied to the RadButton control when the mouse pointer is over the control.")]
		[CssClassProperty]
		[Category("Appearance")]
		public string HoveredCssClass
		{
			get
			{
				return (base.ViewState["HoveredCssClass"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x000247EE File Offset: 0x000229EE
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0002480E File Offset: 0x00022A0E
		[Description("Gets or sets the CSS class applied to the RadButton control when the control is pressed.")]
		[DefaultValue("")]
		[Category("Appearance")]
		[CssClassProperty]
		public string PressedCssClass
		{
			get
			{
				return (base.ViewState["PressedCssClass"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["PressedCssClass"] = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00024821 File Offset: 0x00022A21
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x00024846 File Offset: 0x00022A46
		[Description("Gets or sets the width of the RadButton control.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002485E File Offset: 0x00022A5E
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x00024883 File Offset: 0x00022A83
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the height of the RadButton control.")]
		[Category("Layout")]
		public Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002489B File Offset: 0x00022A9B
		[DefaultValue(null)]
		[Description("Gets the object that controls the Primary and Secondary Icon related properties.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		public ButtonIcon Icon
		{
			get
			{
				if (this._icon == null)
				{
					this._icon = new ButtonIcon();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._icon).TrackViewState();
					}
				}
				return this._icon;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000248C9 File Offset: 0x00022AC9
		[Description("Gets the object that controls the Primary and Secondary Icon related properties.")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ButtonImage Image
		{
			get
			{
				if (this._image == null)
				{
					this._image = new ButtonImage();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._image).TrackViewState();
					}
				}
				return this._image;
			}
		}

		// Token: 0x04000275 RID: 629
		private RadToggleButton _container;

		// Token: 0x04000276 RID: 630
		private ButtonIcon _icon;

		// Token: 0x04000277 RID: 631
		private ButtonImage _image;
	}
}
