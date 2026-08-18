using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonBase;

namespace Telerik.Web.UI
{
	// Token: 0x020000AE RID: 174
	[LightweightRendering]
	[ValidationProperty("SelectedItem")]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.RadButtonList", "Telerik.Web.UI.Common.ButtonList.RadButtonListScripts.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public abstract class RadButtonList : RadDataBoundControl, IPostBackDataHandler, INamingContainer
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x0001AD00 File Offset: 0x00018F00
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadButtonList.eventSelectedIndexChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001AD30 File Offset: 0x00018F30
		protected virtual void OnItemDataBound(ButtonListEventArgs e)
		{
			EventHandler<ButtonListEventArgs> eventHandler = (EventHandler<ButtonListEventArgs>)base.Events[RadButtonList.eventItemDataBound];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001AD60 File Offset: 0x00018F60
		protected virtual void OnItemCreated(ButtonListEventArgs e)
		{
			EventHandler<ButtonListEventArgs> eventHandler = (EventHandler<ButtonListEventArgs>)base.Events[RadButtonList.eventItemCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001AD8E File Offset: 0x00018F8E
		protected internal override IRenderer CreateControlRenderer()
		{
			return new ButtonListRenderer(this);
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001AD96 File Offset: 0x00018F96
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001ADA3 File Offset: 0x00018FA3
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			base.AddAttributesToRender(writer);
			this.AccessKey = accessKey;
			this.TabIndex = tabIndex;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001ADF2 File Offset: 0x00018FF2
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.Controls.Clear();
				this.CreateCheckableButtons();
				this.SetCheckableButtonsProperties();
			}
			this.RenderTrialMessage(writer);
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001AE26 File Offset: 0x00019026
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateCheckableButtons();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001AE34 File Offset: 0x00019034
		private void CreateCheckableButtons()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.Controls.Add(this.CreateCheckableButton());
			}
		}

		// Token: 0x060006B8 RID: 1720
		protected abstract CheckableButton CreateCheckableButton();

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001AE68 File Offset: 0x00019068
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (this.Items.Count != this.Controls.Count)
			{
				this.Controls.Clear();
				this.CreateCheckableButtons();
			}
			this.SetCheckableButtonsProperties();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001AEA0 File Offset: 0x000190A0
		private void SetCheckableButtonsProperties()
		{
			bool flag = false;
			for (int i = 0; i < this.Controls.Count; i++)
			{
				CheckableButton checkableButton = (CheckableButton)this.Controls[i];
				this.SetCheckableButtonProperties(checkableButton, this.Items[i]);
				if (this.AccessKey != string.Empty && checkableButton.Enabled && !flag)
				{
					checkableButton.AccessKey = this.AccessKey;
					flag = true;
				}
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001AF18 File Offset: 0x00019118
		protected virtual void SetCheckableButtonProperties(CheckableButton checkableButton, ButtonListItem item)
		{
			checkableButton.Text = item.Text;
			checkableButton.Value = item.Value;
			checkableButton.Enabled = item.Enabled;
			checkableButton.AutoPostBack = this.AutoPostBack;
			checkableButton.CausesValidation = this.CausesValidation;
			checkableButton.TabIndex = this.TabIndex;
			checkableButton.ValidationGroup = this.ValidationGroup;
			checkableButton.EnableAriaSupport = this.EnableAriaSupport;
			checkableButton.ToolTip = item.ToolTip;
			checkableButton.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
			checkableButton.EnableEmbeddedSkins = this.AreEmbeddedSkinsEnalbed;
			if (base.IsSkinSet)
			{
				checkableButton.Skin = base.RuntimeSkin;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001AFC0 File Offset: 0x000191C0
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			((IStateManager)this.ClientEvents).LoadViewState(array[1]);
			((IStateManager)this.DataBindings).LoadViewState(array[2]);
			((IStateManager)this.AriaSettings).LoadViewState(array[3]);
			if (array[4] == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(array[4]);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001B028 File Offset: 0x00019228
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.DataBindings).SaveViewState(),
				((IStateManager)this.AriaSettings).SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			};
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001B080 File Offset: 0x00019280
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.DataBindings).TrackViewState();
			((IStateManager)this.AriaSettings).TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001B0B4 File Offset: 0x000192B4
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderingMode(descriptor);
			this.AriaSettings.Describe(descriptor);
			descriptor.AddProperty("toolTip", this.ToolTip);
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			if (this._keyboardNavigationSettings != null)
			{
				this.KeyboardNavigationSettings.Describe(descriptor);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001B11C File Offset: 0x0001931C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			ButtonListClientState buttonListClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				buttonListClientState = javaScriptSerializer.Deserialize<ButtonListClientState>(text);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (buttonListClientState == null)
			{
				return false;
			}
			bool result = false;
			if (this.Visible != buttonListClientState.Visible)
			{
				this.Visible = buttonListClientState.Visible;
				result = true;
			}
			if (base.IsEnabled && this.Enabled != buttonListClientState.Enabled)
			{
				this.Enabled = buttonListClientState.Enabled;
				result = true;
			}
			if (this.SelectedIndex != buttonListClientState.SelectedIndex)
			{
				this.SelectedIndex = buttonListClientState.SelectedIndex;
				result = (this.isSelectedIndexChanged = true);
			}
			if (this.ToolTip != buttonListClientState.ToolTip)
			{
				this.ToolTip = buttonListClientState.ToolTip;
				result = true;
			}
			if (this.Height != buttonListClientState.Height)
			{
				this.Height = buttonListClientState.Height;
				result = true;
			}
			if (this.Width != buttonListClientState.Width)
			{
				this.Width = buttonListClientState.Width;
				result = true;
			}
			if (this.ValidationGroup != buttonListClientState.ValidationGroup)
			{
				this.ValidationGroup = buttonListClientState.ValidationGroup;
				result = true;
			}
			return result;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001B270 File Offset: 0x00019470
		protected override void RaisePostDataChangedEvent()
		{
			if (this.isSelectedIndexChanged)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001B288 File Offset: 0x00019488
		private int GetSelectedIndex()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Selected)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001B2C1 File Offset: 0x000194C1
		private ButtonListItem GetSelectedItem()
		{
			if (this.SelectedIndex >= 0)
			{
				return this.Items[this.SelectedIndex];
			}
			return null;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001B2DF File Offset: 0x000194DF
		protected virtual void SetSelectedItem(ButtonListItem item)
		{
			item.Selected = true;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001B2E8 File Offset: 0x000194E8
		private string GetSelectedValue()
		{
			if (this.SelectedItem != null)
			{
				return this.SelectedItem.Value;
			}
			return string.Empty;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001B303 File Offset: 0x00019503
		private ButtonListItemCollection CreateItemCollection()
		{
			return new ButtonListItemCollection(this);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001B30B File Offset: 0x0001950B
		protected virtual void SetSelectedIndex(int value)
		{
			if (((this.Items.Count != 0 && value < this.Items.Count) || value == -1) && value >= 0)
			{
				this.Items[value].Selected = true;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001B344 File Offset: 0x00019544
		protected virtual void SetSelectedValue(string value)
		{
			ButtonListItem item = this.GetItem(value);
			if (item != null)
			{
				item.Selected = true;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001B364 File Offset: 0x00019564
		protected ButtonListItem GetItem(string value)
		{
			ButtonListItem result = null;
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Value == value)
				{
					result = this.Items[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001B3B2 File Offset: 0x000195B2
		protected override void PerformDataBinding(IEnumerable data)
		{
			if ((data == null && this.DataSource == null) || base.DesignMode)
			{
				return;
			}
			this.PrepareForDataBinding();
			this.BindToEnumerableData(data);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001B3D5 File Offset: 0x000195D5
		private void PrepareForDataBinding()
		{
			if (!this.AppendDataBoundItems)
			{
				this.Items.Clear();
				base.ClearChildViewState();
			}
			this.TrackViewState();
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001B3F8 File Offset: 0x000195F8
		private void BindToEnumerableData(IEnumerable dataSource)
		{
			foreach (object dataObject in dataSource)
			{
				this.BindItem(this.Items, dataObject);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001B450 File Offset: 0x00019650
		private void BindItem(ButtonListItemCollection items, object dataObject)
		{
			ButtonListItem item = new ButtonListItem();
			this.SetItemPropertiesFromData(item, dataObject);
			this.Items.Add(item);
			this.RaiseItemDataBound(item);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001B480 File Offset: 0x00019680
		private void SetItemPropertiesFromData(ButtonListItem item, object dataObject)
		{
			if (this.DataBindings.DataTextField.Length > 0)
			{
				this.SetItemTextFieldFromData(item, dataObject);
			}
			if (this.DataBindings.DataValueField.Length > 0)
			{
				this.SetItemValueFieldFromData(item, dataObject);
			}
			if (this.DataBindings.DataSelectedField.Length > 0)
			{
				this.SetItemSelectedFieldFromData(item, dataObject);
			}
			if (this.DataBindings.DataEnabledField.Length > 0)
			{
				this.SetItemEnabledFieldFromData(item, dataObject);
			}
			if (this.DataBindings.DataToolTipField.Length > 0)
			{
				this.SetItemToolTipFieldFromData(item, dataObject);
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001B514 File Offset: 0x00019714
		private void SetItemTextFieldFromData(ButtonListItem item, object dataObject)
		{
			object obj = DataBinder.Eval(dataObject, this.DataBindings.DataTextField);
			if (obj != DBNull.Value && obj != null)
			{
				item.Text = obj.ToString();
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001B54C File Offset: 0x0001974C
		private void SetItemValueFieldFromData(ButtonListItem item, object dataObject)
		{
			object obj = DataBinder.Eval(dataObject, this.DataBindings.DataValueField);
			if (obj != DBNull.Value && obj != null)
			{
				item.Value = obj.ToString();
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001B584 File Offset: 0x00019784
		private void SetItemSelectedFieldFromData(ButtonListItem item, object dataObject)
		{
			object obj = DataBinder.Eval(dataObject, this.DataBindings.DataSelectedField);
			if (obj != DBNull.Value && obj != null)
			{
				item.Selected = (bool)obj;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001B5BC File Offset: 0x000197BC
		private void SetItemEnabledFieldFromData(ButtonListItem item, object dataObject)
		{
			object obj = DataBinder.Eval(dataObject, this.DataBindings.DataEnabledField);
			if (obj != DBNull.Value && obj != null)
			{
				item.Enabled = (bool)obj;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001B5F4 File Offset: 0x000197F4
		private void SetItemToolTipFieldFromData(ButtonListItem item, object dataObject)
		{
			object obj = DataBinder.Eval(dataObject, this.DataBindings.DataToolTipField);
			if (obj != DBNull.Value && obj != null)
			{
				item.ToolTip = obj.ToString();
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001B62A File Offset: 0x0001982A
		private void RaiseItemDataBound(ButtonListItem item)
		{
			this.OnItemDataBound(new ButtonListEventArgs(item));
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001B638 File Offset: 0x00019838
		internal virtual void RaiseItemCreated(ButtonListItem item)
		{
			this.OnItemCreated(new ButtonListEventArgs(item));
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001B646 File Offset: 0x00019846
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001B64C File Offset: 0x0001984C
		[MergableProperty(false)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WaiAriaSettings AriaSettings
		{
			get
			{
				WaiAriaSettings result;
				if ((result = this._ariaSettings) == null)
				{
					result = (this._ariaSettings = new WaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001B671 File Offset: 0x00019871
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0001B692 File Offset: 0x00019892
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets a value that indicates whether list items are cleared before data binding.")]
		public bool AppendDataBoundItems
		{
			get
			{
				return (bool)(this.ViewState["AppendDataBoundItems"] ?? false);
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001B6AA File Offset: 0x000198AA
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0001B6CB File Offset: 0x000198CB
		[DefaultValue(true)]
		[Description("Specifies if change of the selected item should postback.")]
		[Category("Behavior")]
		public bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? true);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001B6E3 File Offset: 0x000198E3
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0001B704 File Offset: 0x00019904
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether validation is performed when the selected item is changed.")]
		[DefaultValue(true)]
		public bool CausesValidation
		{
			get
			{
				return (bool)(this.ViewState["CausesValidation"] ?? true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001B71C File Offset: 0x0001991C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Defines the client events handlers.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ButtonListClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new ButtonListClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001B737 File Offset: 0x00019937
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Defines the data binding configuration of the control ")]
		public ButtonListDataBinding DataBindings
		{
			get
			{
				if (this._databindings == null)
				{
					this._databindings = new ButtonListDataBinding();
				}
				return this._databindings;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001B752 File Offset: 0x00019952
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0001B773 File Offset: 0x00019973
		[ClientPropertyName("enableAriaSupport")]
		[Description("Gets or sets a value indicating whether support for WAI-ARIA is enabled.")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
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

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001B78B File Offset: 0x0001998B
		[Description("This control features no skins, so this property must be set to false.")]
		[DefaultValue(false)]
		[Browsable(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001B78E File Offset: 0x0001998E
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0001B791 File Offset: 0x00019991
		[DefaultValue(false)]
		[Browsable(false)]
		[Description("This control features no skins, so this property must be set to false.")]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
			set
			{
				this.ViewState["EnableEmbeddedSkins"] = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001B7A9 File Offset: 0x000199A9
		private bool AreEmbeddedSkinsEnalbed
		{
			get
			{
				return base.EnableEmbeddedSkins;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001B7B4 File Offset: 0x000199B4
		[DefaultValue(null)]
		[Description("Define keyboard shortcut to focus the first list item.")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public KeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				KeyboardNavigationSettings result;
				if ((result = this._keyboardNavigationSettings) == null)
				{
					result = (this._keyboardNavigationSettings = new KeyboardNavigationSettings());
				}
				return result;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001B7D9 File Offset: 0x000199D9
		[Description("Gets the collection of items in the list control. An item has Text, Value, Selected and Enabled properties.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ButtonListItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = this.CreateItemCollection();
				}
				return this.items;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060006E8 RID: 1768 RVA: 0x0001B7F5 File Offset: 0x000199F5
		// (remove) Token: 0x060006E9 RID: 1769 RVA: 0x0001B808 File Offset: 0x00019A08
		[Category("Action")]
		[Description("Adds or removes an event handler method from the SelectedIndexChanged event, fired when the selected item changes between posts to the server.")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadButtonList.eventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadButtonList.eventSelectedIndexChanged, value);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060006EA RID: 1770 RVA: 0x0001B81B File Offset: 0x00019A1B
		// (remove) Token: 0x060006EB RID: 1771 RVA: 0x0001B82E File Offset: 0x00019A2E
		[Description("Adds or removes an event handler method from the ItemDataBound event. The event is fired when an item is data bound on the server.")]
		[Category("Action")]
		public event EventHandler<ButtonListEventArgs> ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadButtonList.eventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadButtonList.eventItemDataBound, value);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060006EC RID: 1772 RVA: 0x0001B841 File Offset: 0x00019A41
		// (remove) Token: 0x060006ED RID: 1773 RVA: 0x0001B854 File Offset: 0x00019A54
		[Category("Action")]
		[Description("Adds or removes an event handler method from the ItemCreated event, fired when an item is created on the server.")]
		public event EventHandler<ButtonListEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadButtonList.eventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadButtonList.eventItemCreated, value);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001B867 File Offset: 0x00019A67
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0001B888 File Offset: 0x00019A88
		[Category("Layout")]
		[DefaultValue(ButtonListLayout.Flow)]
		[Description("Gets or sets the layout in which the items are rendered. Possible values are Flow, OrderedList, UnorderedList. OrderedList and UnorderedList do not support Horizontal Direction and Columns.")]
		public ButtonListLayout Layout
		{
			get
			{
				return (ButtonListLayout)(this.ViewState["Layout"] ?? ButtonListLayout.Flow);
			}
			set
			{
				this.ViewState["Layout"] = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001B8A0 File Offset: 0x00019AA0
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001B8C1 File Offset: 0x00019AC1
		[DefaultValue(0)]
		[Description("Gets or sets the number of columns to display in the control when the Layout is Flow.")]
		[Category("Behavior")]
		public int Columns
		{
			get
			{
				return (int)(this.ViewState["Columns"] ?? 0);
			}
			set
			{
				this.ViewState["Columns"] = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001B8D9 File Offset: 0x00019AD9
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0001B8FA File Offset: 0x00019AFA
		[Category("Layout")]
		[Description("Gets or sets the direction in which the items within the group are displayed. ")]
		[DefaultValue(ButtonListDirection.Vertical)]
		public ButtonListDirection Direction
		{
			get
			{
				return (ButtonListDirection)(this.ViewState["Direction"] ?? ButtonListDirection.Vertical);
			}
			set
			{
				this.ViewState["Direction"] = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001B912 File Offset: 0x00019B12
		[Description("Gets the number of list items in the control.")]
		[Category("Behavior")]
		[DefaultValue(0)]
		public int ItemCount
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001B91F File Offset: 0x00019B1F
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0001B927 File Offset: 0x00019B27
		[Browsable(false)]
		[ClientPropertyName("selectedIndex")]
		[Description("Gets or sets the lowest ordinal index of the selected items in the control.")]
		[Category("Behavior")]
		[DefaultValue(-1)]
		[ClientControlProperty]
		public int SelectedIndex
		{
			get
			{
				return this.GetSelectedIndex();
			}
			set
			{
				if (value < -1)
				{
					if (this.Items.Count != 0)
					{
						throw new ArgumentOutOfRangeException("value", value, "The index was set to less than -1, or greater than or equal to the number of items on the list at the time the list is rendered.");
					}
					value = -1;
				}
				this.SetSelectedIndex(value);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001B95A File Offset: 0x00019B5A
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001B962 File Offset: 0x00019B62
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Gets the selected item with the lowest index in the list control or sets a selected item.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(null)]
		[Browsable(false)]
		public ButtonListItem SelectedItem
		{
			get
			{
				return this.GetSelectedItem();
			}
			set
			{
				if (this.Items.IndexOf(value) > -1)
				{
					this.SetSelectedItem(value);
				}
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001B97A File Offset: 0x00019B7A
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001B982 File Offset: 0x00019B82
		[Browsable(false)]
		[Description("Gets the value of the selected item in the list control, or selects the item in the list control that contains the specified value.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string SelectedValue
		{
			get
			{
				return this.GetSelectedValue();
			}
			set
			{
				if (value != null)
				{
					this.SetSelectedValue(value);
				}
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001B98E File Offset: 0x00019B8E
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001B9AE File Offset: 0x00019BAE
		[DefaultValue("")]
		[Description(" Gets or sets the group of controls for which an item from the list causes validation when it posts back to the server.")]
		[Category("Behavior")]
		[ClientPropertyName("validationGroup")]
		[ClientControlProperty]
		public string ValidationGroup
		{
			get
			{
				return ((string)this.ViewState["ValidationGroup"]) ?? "";
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001B9C1 File Offset: 0x00019BC1
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001B9C9 File Offset: 0x00019BC9
		[Description("Gets or sets the height of the control.")]
		[ClientPropertyName("height")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
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

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001B9D2 File Offset: 0x00019BD2
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001B9DA File Offset: 0x00019BDA
		[NotifyParentProperty(true)]
		[ClientPropertyName("width")]
		[Description("Gets or sets the width of the control.")]
		[ClientControlProperty]
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

		// Token: 0x06000701 RID: 1793 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<int>(descriptor, "selectedIndex", this.SelectedIndex, -1);
			base.DescribeProperty<string>(descriptor, "validationGroup", this.ValidationGroup, "");
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001BA80 File Offset: 0x00019C80
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "itemCheckedChanged", this.ClientEvents.OnItemCheckedChanged);
			RadDataBoundControl.DescribeEvent(descriptor, "itemCheckedChanging", this.ClientEvents.OnItemCheckedChanging);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicked", this.ClientEvents.OnItemClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicking", this.ClientEvents.OnItemClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "itemLoad", this.ClientEvents.OnItemLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "itemMouseOut", this.ClientEvents.OnItemMouseOut);
			RadDataBoundControl.DescribeEvent(descriptor, "itemMouseOver", this.ClientEvents.OnItemMouseOver);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "selectedIndexChanged", this.ClientEvents.OnSelectedIndexChanged);
			RadDataBoundControl.DescribeEvent(descriptor, "selectedIndexChanging", this.ClientEvents.OnSelectedIndexChanging);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400016D RID: 365
		private ButtonListItemCollection items;

		// Token: 0x0400016E RID: 366
		protected bool isSelectedIndexChanged;

		// Token: 0x0400016F RID: 367
		private static readonly object eventSelectedIndexChanged = new object();

		// Token: 0x04000170 RID: 368
		private static readonly object eventItemDataBound = new object();

		// Token: 0x04000171 RID: 369
		private static readonly object eventItemCreated = new object();

		// Token: 0x04000172 RID: 370
		private WaiAriaSettings _ariaSettings;

		// Token: 0x04000173 RID: 371
		private ButtonListClientEvents _clientEvents;

		// Token: 0x04000174 RID: 372
		private ButtonListDataBinding _databindings;

		// Token: 0x04000175 RID: 373
		private KeyboardNavigationSettings _keyboardNavigationSettings;
	}
}
