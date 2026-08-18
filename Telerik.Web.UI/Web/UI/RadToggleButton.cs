using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonBase;
using Telerik.Web.UI.ButtonJavaScriptSerialization;
using Telerik.Web.UI.ButtonNS.JavaScriptSerialization;
using Telerik.Web.UI.ButtonRendering;

namespace Telerik.Web.UI
{
	// Token: 0x020000F1 RID: 241
	[TelerikToolboxCategory("Navigation")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxBitmap(typeof(RadToggleButton), "Telerik.Web.UI.Button.png")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(PostBackButtonBase))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadButton))]
	[ToolboxData("<{0}:RadToggleButton runat=\"server\" Text=\"RadToggleButton\"></{0}:RadToggleButton>")]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[EmbeddedSkin("Button", "Default")]
	[ClientScriptResource("Telerik.Web.UI.RadToggleButton", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[EmbeddedSkin("Button")]
	[SupportsEventValidation]
	[Designer("Telerik.Web.Design.RadToggleButtonDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadToggleButton : PostBackButtonBase, IButtonControl, IPostBackEventHandler, IJavaScriptConverterProvider
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x00024A0C File Offset: 0x00022C0C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = JavaScriptSerializeProvider.CreateSerializer(this);
			descriptor.AddProperty("_hasImage", this.HasImageInState);
			descriptor.AddProperty("_hasIcon", this.HasIconInState);
			descriptor.AddScriptProperty("toggleStatesData", javaScriptSerializer.Serialize(this.ToggleStates));
			descriptor.AddScriptProperty("confirmSettings", javaScriptSerializer.Serialize(this.ConfirmSettings));
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00024A8C File Offset: 0x00022C8C
		public virtual IEnumerable<JavaScriptConverter> GetJsConverters()
		{
			return new JavaScriptConverter[]
			{
				new ButtonToggleStateConverter
				{
					ResolveUrl = ((string url) => base.ResolveUrl(url))
				},
				new RadButtonConfirmSettingsConverter()
			};
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00024AC5 File Offset: 0x00022CC5
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00024ACD File Offset: 0x00022CCD
		protected override void Render(HtmlTextWriter writer)
		{
			this.RegisterForEventValidation();
			base.Render(writer);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00024ADC File Offset: 0x00022CDC
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.SelectedToggleStateIndex = (int)clientState["selectedToggleStateIndex"];
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00024AFC File Offset: 0x00022CFC
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(text) as Dictionary<string, object>;
				if (dictionary != null)
				{
					int num = 0;
					if (dictionary["selectedToggleStateIndex"] != null)
					{
						num = (int)dictionary["selectedToggleStateIndex"];
					}
					this._toggleStateFlag = (num != this.SelectedToggleStateIndex);
				}
			}
			return base.LoadPostData(postDataKey, postCollection) || this._toggleStateFlag;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00024B7C File Offset: 0x00022D7C
		protected override void RaisePostDataChangedEvent()
		{
			if (this._toggleStateFlag)
			{
				string commandName = base.CommandName;
				string commandArgument = base.CommandArgument;
				if (this.SelectedToggleState != null)
				{
					commandName = this.SelectedToggleState.CommandName;
					commandArgument = this.SelectedToggleState.CommandArgument;
				}
				this.OnToggleStateChanged(new ToggleButtonStateChangedEventArgs(commandName, commandArgument, this.SelectedToggleStateIndex, this.SelectedToggleState));
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00024BD8 File Offset: 0x00022DD8
		protected virtual void OnToggleStateChanged(ToggleButtonStateChangedEventArgs e)
		{
			ToggleButtonStateChangedEventHandler toggleButtonStateChangedEventHandler = (ToggleButtonStateChangedEventHandler)base.Events[RadToggleButton.eventToggleStatechanged];
			if (toggleButtonStateChangedEventHandler != null)
			{
				toggleButtonStateChangedEventHandler(this, e);
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00024C08 File Offset: 0x00022E08
		private ButtonToggleState GetSelectedState()
		{
			foreach (object obj in this.ToggleStates)
			{
				ButtonToggleState buttonToggleState = (ButtonToggleState)obj;
				if (buttonToggleState.Selected)
				{
					return buttonToggleState;
				}
			}
			return null;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00024C6C File Offset: 0x00022E6C
		public virtual void ClearSelection()
		{
			int count = this.ToggleStates.Count;
			for (int i = 0; i < count; i++)
			{
				this.ToggleStates[i].Selected = false;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00024CA4 File Offset: 0x00022EA4
		public virtual void SetSelectedToggleStateByValue(string value)
		{
			ButtonToggleState buttonToggleState = this.FindToggleStateByValue(value);
			if (buttonToggleState != null)
			{
				buttonToggleState.Selected = true;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00024CC4 File Offset: 0x00022EC4
		public virtual void SetSelectedToggleStateByText(string text)
		{
			ButtonToggleState buttonToggleState = this.FindToggleStateByText(text);
			if (buttonToggleState != null)
			{
				buttonToggleState.Selected = true;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00024CE4 File Offset: 0x00022EE4
		public virtual ButtonToggleState FindToggleStateByValue(string value)
		{
			for (int i = 0; i < this.ToggleStates.Count; i++)
			{
				if (this.ToggleStates[i].Value == value)
				{
					return this.ToggleStates[i];
				}
			}
			return null;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00024D30 File Offset: 0x00022F30
		public virtual ButtonToggleState FindToggleStateByText(string text)
		{
			for (int i = 0; i < this.ToggleStates.Count; i++)
			{
				if (this.ToggleStates[i].Text == text)
				{
					return this.ToggleStates[i];
				}
			}
			return null;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00024D7C File Offset: 0x00022F7C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.ToggleStates).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.ConfirmSettings).LoadViewState(array[2]);
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00024DC0 File Offset: 0x00022FC0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ToggleStates).SaveViewState(),
				((IStateManager)this.ConfirmSettings).SaveViewState()
			};
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00024DFC File Offset: 0x00022FFC
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ToggleStates).TrackViewState();
			((IStateManager)this.ConfirmSettings).TrackViewState();
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00024E1A File Offset: 0x0002301A
		public override string ButtonName
		{
			get
			{
				return "RadToggleButton";
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00024E21 File Offset: 0x00023021
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		[Description("Gets the object that controls the built-in confirmation dialog properties.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public RadButtonConfirmSettings ConfirmSettings
		{
			get
			{
				if (this._confirmSettings == null)
				{
					this._confirmSettings = new RadButtonConfirmSettings();
				}
				return this._confirmSettings;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00024E3C File Offset: 0x0002303C
		[Themeable(false)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets a collection of ButtonToggleState objects that belong to the RadToggleButton control.")]
		[DefaultValue(null)]
		public ButtonToggleStateCollection ToggleStates
		{
			get
			{
				if (this._toggleStates == null)
				{
					this._toggleStates = new ButtonToggleStateCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._toggleStates).TrackViewState();
					}
				}
				return this._toggleStates;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00024E6C File Offset: 0x0002306C
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00024EBC File Offset: 0x000230BC
		[SimplePersistenceSetting]
		[Themeable(false)]
		[Category("ToggleButton")]
		[DefaultValue(0)]
		[Description(" Gets or sets the index of the currently selected ToggleState of the RadToggleButton control.")]
		[ClientControlProperty]
		[ClientPropertyName("selectedToggleStateIndex")]
		public virtual int SelectedToggleStateIndex
		{
			get
			{
				int count = this.ToggleStates.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.ToggleStates[i].Selected)
					{
						return i;
					}
				}
				if (count != 0)
				{
					this.ToggleStates[0].Selected = true;
				}
				return 0;
			}
			set
			{
				int count = this.ToggleStates.Count;
				if (count == 0)
				{
					return;
				}
				if (value >= 0 && value < count)
				{
					this.ClearSelection();
					this.ToggleStates[value].Selected = true;
					return;
				}
				throw new ArgumentOutOfRangeException("value");
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00024F04 File Offset: 0x00023104
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		[Description("Gets the currently selected ToggleState of the RadToggleButton control.")]
		[Themeable(false)]
		[Category("ToggleButton")]
		[DefaultValue(null)]
		public virtual ButtonToggleState SelectedToggleState
		{
			get
			{
				int selectedToggleStateIndex = this.SelectedToggleStateIndex;
				int count = this.ToggleStates.Count;
				if (selectedToggleStateIndex >= 0 && count != 0)
				{
					return this.ToggleStates[selectedToggleStateIndex];
				}
				return null;
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000A2C RID: 2604 RVA: 0x00024F39 File Offset: 0x00023139
		// (remove) Token: 0x06000A2D RID: 2605 RVA: 0x00024F4C File Offset: 0x0002314C
		[Category("Action")]
		[Description("Fired when the value of the SelectedToggleStateIndex property changes between posts to the server.")]
		public event ToggleButtonStateChangedEventHandler ToggleStateChanged
		{
			add
			{
				base.Events.AddHandler(RadToggleButton.eventToggleStatechanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadToggleButton.eventToggleStatechanged, value);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00024F60 File Offset: 0x00023160
		internal bool HasImageInState
		{
			get
			{
				if (this._hasImageInState == null)
				{
					this._hasImageInState = new bool?(false);
					foreach (object obj in this.ToggleStates)
					{
						ButtonToggleState buttonToggleState = (ButtonToggleState)obj;
						if (!string.IsNullOrEmpty(buttonToggleState.Image.Url))
						{
							this._hasImageInState = new bool?(true);
							break;
						}
					}
				}
				return this._hasImageInState.Value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00024FF8 File Offset: 0x000231F8
		internal bool HasIconInState
		{
			get
			{
				if (this._hasIconInState == null)
				{
					this._hasIconInState = new bool?(false);
					foreach (object obj in this.ToggleStates)
					{
						ButtonToggleState buttonToggleState = (ButtonToggleState)obj;
						if (!string.IsNullOrEmpty(buttonToggleState.Icon.Url) || !string.IsNullOrEmpty(buttonToggleState.Icon.CssClass))
						{
							this._hasIconInState = new bool?(true);
							break;
						}
					}
				}
				return this._hasIconInState.Value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000250A0 File Offset: 0x000232A0
		internal bool HasStateWithPrimaryIcon
		{
			get
			{
				foreach (object obj in this.ToggleStates)
				{
					ButtonToggleState buttonToggleState = (ButtonToggleState)obj;
					this._hasStateWithPrimaryIcon |= (!string.IsNullOrEmpty(buttonToggleState.Icon.CssClass) || !string.IsNullOrEmpty(buttonToggleState.Icon.Url));
					if (this._hasStateWithPrimaryIcon)
					{
						break;
					}
				}
				return this._hasStateWithPrimaryIcon;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00025138 File Offset: 0x00023338
		internal bool HasStateWithSecondaryIcon
		{
			get
			{
				return this._hasStateWithSecondaryIcon;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00025140 File Offset: 0x00023340
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x00025160 File Offset: 0x00023360
		[Category("Client-side events")]
		[ClientPropertyName("toggleStateChanging")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("Gets or sets the name of the JavaScript function that will be called when the SelectedToggleStateIndex property of the RadToggleButton control is about to be changed.")]
		public string OnClientToggleStateChanging
		{
			get
			{
				return ((string)this.ViewState["OnClientToggleStateChanging"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientToggleStateChanging"] = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00025173 File Offset: 0x00023373
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x00025193 File Offset: 0x00023393
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called after the SelectedToggleStateIndex property of the RadToggleButton control is changed.")]
		[ClientPropertyName("toggleStateChanged")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientToggleStateChanged
		{
			get
			{
				return ((string)this.ViewState["OnClientToggleStateChanged"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientToggleStateChanged"] = value;
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000251A6 File Offset: 0x000233A6
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int>(descriptor, "selectedToggleStateIndex", this.SelectedToggleStateIndex, 0);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000251C2 File Offset: 0x000233C2
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "toggleStateChanged", this.OnClientToggleStateChanged);
			RadWebControl.DescribeEvent(descriptor, "toggleStateChanging", this.OnClientToggleStateChanging);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04000279 RID: 633
		private bool _toggleStateFlag;

		// Token: 0x0400027A RID: 634
		private RadButtonConfirmSettings _confirmSettings;

		// Token: 0x0400027B RID: 635
		private ButtonToggleStateCollection _toggleStates;

		// Token: 0x0400027C RID: 636
		private static readonly object eventToggleStatechanged = new object();

		// Token: 0x0400027D RID: 637
		private bool? _hasImageInState = null;

		// Token: 0x0400027E RID: 638
		private bool? _hasIconInState = null;

		// Token: 0x0400027F RID: 639
		private bool _hasStateWithPrimaryIcon;

		// Token: 0x04000280 RID: 640
		private bool _hasStateWithSecondaryIcon;
	}
}
