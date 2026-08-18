using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.Analytics;

namespace Telerik.Web.UI
{
	// Token: 0x02000B21 RID: 2849
	[XmlRoot("Items")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(jQueryPlugins), 1)]
	[RequiredScript(typeof(DropDown), 2)]
	[RequiredScript(typeof(TouchScrollExtender), 3)]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadDropDownList", "Telerik.Web.UI.DropDownList.RadDropDownListScripts.js")]
	[EmbeddedSkin("DropDownList", typeof(RadDropDownList))]
	[ValidationProperty("SelectedText")]
	[ControlValueProperty("SelectedValue")]
	[TelerikToolboxCategory("Data Editing")]
	[Designer("Telerik.Web.Design.RadDropDownListDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadDropDownList), "Telerik.Web.UI.DropDownList.png")]
	[ToolboxData("<{0}:RadDropDownList runat=\"server\"></{0}:RadDropDownList>")]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("DropDownList", "Default", typeof(RadDropDownList))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadDropDownList))]
	public class RadDropDownList : ControlItemContainer, ICallbackEventHandler, IPostBackEventHandler, IFlatBoundContainer
	{
		// Token: 0x170022D3 RID: 8915
		// (get) Token: 0x06006A6F RID: 27247 RVA: 0x0018ED7D File Offset: 0x0018CF7D
		[ClientControlProperty]
		[ClientPropertyName("_uniqueId")]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x170022D4 RID: 8916
		// (get) Token: 0x06006A70 RID: 27248 RVA: 0x0018ED85 File Offset: 0x0018CF85
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The items of the dropdownlist")]
		[DefaultValue(null)]
		public DropDownListItemCollection Items
		{
			[DebuggerStepThrough]
			get
			{
				return (DropDownListItemCollection)base.Children;
			}
		}

		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x06006A71 RID: 27249 RVA: 0x0018ED92 File Offset: 0x0018CF92
		// (set) Token: 0x06006A72 RID: 27250 RVA: 0x0018EDB3 File Offset: 0x0018CFB3
		[ClientPropertyName("expandDirection")]
		[DefaultValue(DropDownListExpandDirection.Down)]
		[Description("The expand direction of the RadDropDownList dropdown.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public DropDownListExpandDirection ExpandDirection
		{
			get
			{
				return (DropDownListExpandDirection)(this.ViewState["ExpandDirection"] ?? DropDownListExpandDirection.Down);
			}
			set
			{
				this.ViewState["ExpandDirection"] = value;
			}
		}

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x06006A73 RID: 27251 RVA: 0x0018EDCC File Offset: 0x0018CFCC
		// (set) Token: 0x06006A74 RID: 27252 RVA: 0x0018EE2C File Offset: 0x0018D02C
		[Description("SelectedIndex")]
		[Browsable(false)]
		[SimplePersistenceSetting]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(-1)]
		[ClientControlProperty]
		[ClientPropertyName("_selectedIndex")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected)
					{
						return i;
					}
				}
				if (string.IsNullOrEmpty(this.DefaultMessage))
				{
					DropDownListItem dropDownListItem = this.FindFirstAvailableItem();
					if (dropDownListItem != null)
					{
						dropDownListItem.Selected = true;
						return dropDownListItem.Index;
					}
				}
				return -1;
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
				if ((this.Items.Count != 0 && value < this.Items.Count) || value == -1)
				{
					this.ClearSelection();
					if (value >= 0)
					{
						this.Items[value].Selected = true;
					}
				}
				this._CachedSelectedIndex = value;
			}
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x06006A75 RID: 27253 RVA: 0x0018EEA8 File Offset: 0x0018D0A8
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		[Bindable(false)]
		public virtual DropDownListItem SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0)
				{
					return this.Items[selectedIndex];
				}
				return null;
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x06006A76 RID: 27254 RVA: 0x0018EECE File Offset: 0x0018D0CE
		// (set) Token: 0x06006A77 RID: 27255 RVA: 0x0018EEEC File Offset: 0x0018D0EC
		[DefaultValue("")]
		[Bindable(true, BindingDirection.TwoWay)]
		[ClientPropertyName("_selectedText")]
		[Browsable(false)]
		[Category("Setup")]
		[Description("The Text of the DropDownList Selected Item")]
		[ClientControlProperty]
		public virtual string SelectedText
		{
			get
			{
				if (this.SelectedItem != null)
				{
					return this.SelectedItem.Text;
				}
				return this._SelectedText;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					DropDownListItem dropDownListItem = base.FindChildByText<DropDownListItem>(value);
					if (dropDownListItem != null)
					{
						this.ClearSelection();
						dropDownListItem.Selected = true;
						this._SelectedText = value;
					}
					this._CachedSelectedText = value;
				}
			}
		}

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x06006A78 RID: 27256 RVA: 0x0018EF27 File Offset: 0x0018D127
		// (set) Token: 0x06006A79 RID: 27257 RVA: 0x0018EF44 File Offset: 0x0018D144
		[Bindable(true, BindingDirection.TwoWay)]
		[Description("The Value of the DropDownList Selected Item")]
		[ClientControlProperty]
		[DefaultValue("")]
		[Browsable(false)]
		[Category("Setup")]
		[ClientPropertyName("_selectedValue")]
		public virtual string SelectedValue
		{
			get
			{
				if (this.SelectedItem != null)
				{
					return this.SelectedItem.Value;
				}
				return this._SelectedValue;
			}
			set
			{
				if (value != null)
				{
					DropDownListItem dropDownListItem = this.FindChildByValue<DropDownListItem>(value);
					if (dropDownListItem != null)
					{
						this.ClearSelection();
						dropDownListItem.Selected = true;
						this._SelectedValue = value;
					}
				}
				this._CachedSelectedValue = value;
			}
		}

		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x06006A7A RID: 27258 RVA: 0x0018EF7A File Offset: 0x0018D17A
		// (set) Token: 0x06006A7B RID: 27259 RVA: 0x0018EF9A File Offset: 0x0018D19A
		[DefaultValue("")]
		[ClientControlProperty]
		[Bindable(true)]
		[Category("Setup")]
		[Description("The text shown when there is no item selected in the DropDownList")]
		public virtual string DefaultMessage
		{
			get
			{
				return (this.ViewState["DefaultMessage"] as string) ?? string.Empty;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.ViewState["DefaultMessage"] = value;
				}
			}
		}

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x06006A7C RID: 27260 RVA: 0x0018EFB5 File Offset: 0x0018D1B5
		// (set) Token: 0x06006A7D RID: 27261 RVA: 0x0018EFD5 File Offset: 0x0018D1D5
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets or sets the HTML template of a DropDownListItem when added on the client.")]
		[Browsable(false)]
		[Category("Client")]
		[DefaultValue("")]
		public string ClientItemTemplate
		{
			get
			{
				return (this.ViewState["ClientItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientItemTemplate"] = value;
			}
		}

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x06006A7E RID: 27262 RVA: 0x0018EFE8 File Offset: 0x0018D1E8
		[Browsable(false)]
		public IList<ClientOperation<DropDownListItem>> ClientChanges
		{
			get
			{
				return this._clientChanges;
			}
		}

		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x06006A7F RID: 27263 RVA: 0x0018EFF0 File Offset: 0x0018D1F0
		// (set) Token: 0x06006A80 RID: 27264 RVA: 0x0018F011 File Offset: 0x0018D211
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Whether to postback after an item is selected")]
		[Bindable(false)]
		public bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x06006A81 RID: 27265 RVA: 0x0018F029 File Offset: 0x0018D229
		// (set) Token: 0x06006A82 RID: 27266 RVA: 0x0018F04A File Offset: 0x0018D24A
		[ClientPropertyName("enableAriaSupport")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
		[ClientControlProperty]
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

		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x06006A83 RID: 27267 RVA: 0x0018F062 File Offset: 0x0018D262
		// (set) Token: 0x06006A84 RID: 27268 RVA: 0x0018F06A File Offset: 0x0018D26A
		[TemplateContainer(typeof(DropDownListItem))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(false)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return base.Template;
			}
			set
			{
				base.Template = value;
			}
		}

		// Token: 0x170022E0 RID: 8928
		// (get) Token: 0x06006A85 RID: 27269 RVA: 0x0018F073 File Offset: 0x0018D273
		// (set) Token: 0x06006A86 RID: 27270 RVA: 0x0018F098 File Offset: 0x0018D298
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[ClientControlProperty]
		public Unit DropDownHeight
		{
			get
			{
				return (Unit)(this.ViewState["DropDownHeight"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownHeight"] = value;
			}
		}

		// Token: 0x170022E1 RID: 8929
		// (get) Token: 0x06006A87 RID: 27271 RVA: 0x0018F0CD File Offset: 0x0018D2CD
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
		}

		// Token: 0x170022E2 RID: 8930
		// (get) Token: 0x06006A88 RID: 27272 RVA: 0x0018F0D5 File Offset: 0x0018D2D5
		// (set) Token: 0x06006A89 RID: 27273 RVA: 0x0018F0FA File Offset: 0x0018D2FA
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit DropDownWidth
		{
			get
			{
				return (Unit)(this.ViewState["DropDownWidth"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownWidth"] = value;
			}
		}

		// Token: 0x170022E3 RID: 8931
		// (get) Token: 0x06006A8A RID: 27274 RVA: 0x0018F12F File Offset: 0x0018D32F
		// (set) Token: 0x06006A8B RID: 27275 RVA: 0x0018F154 File Offset: 0x0018D354
		[Category("Behavior")]
		[Bindable(false)]
		[DefaultValue(7000)]
		public int ZIndex
		{
			get
			{
				return (int)(this.ViewState["ZIndex"] ?? 7000);
			}
			set
			{
				this.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x170022E4 RID: 8932
		// (get) Token: 0x06006A8C RID: 27276 RVA: 0x0018F16C File Offset: 0x0018D36C
		// (set) Token: 0x06006A8D RID: 27277 RVA: 0x0018F18D File Offset: 0x0018D38D
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Whether to postback after an item is selected")]
		[Bindable(false)]
		[DefaultValue(false)]
		public bool EnableVirtualScrolling
		{
			get
			{
				return (bool)(this.ViewState["EnableVirtualScrolling"] ?? false);
			}
			set
			{
				this.ViewState["EnableVirtualScrolling"] = value;
			}
		}

		// Token: 0x170022E5 RID: 8933
		// (get) Token: 0x06006A8E RID: 27278 RVA: 0x0018F1A5 File Offset: 0x0018D3A5
		// (set) Token: 0x06006A8F RID: 27279 RVA: 0x0018F1C5 File Offset: 0x0018D3C5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[TypeConverter("Telerik.Web.Design.AjaxLoadingPanelIDConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[ClientPropertyName("_loadingPanelID")]
		public string LoadingPanelID
		{
			get
			{
				return (string)(this.ViewState["LoadingPanelID"] ?? "");
			}
			set
			{
				this.ViewState["LoadingPanelID"] = value;
			}
		}

		// Token: 0x170022E6 RID: 8934
		// (get) Token: 0x06006A90 RID: 27280 RVA: 0x0018F1D8 File Offset: 0x0018D3D8
		// (set) Token: 0x06006A91 RID: 27281 RVA: 0x0018F1F9 File Offset: 0x0018D3F9
		[Description("The number of Item loaded upon request when VirtualScolling functionality is enabled.")]
		[ClientControlProperty]
		[Bindable(false)]
		[DefaultValue(-1)]
		[Category("Behavior")]
		public int ItemCountPerRequest
		{
			get
			{
				return (int)(this.ViewState["ItemCountPerRequest"] ?? -1);
			}
			set
			{
				if (value > 0)
				{
					this.ViewState["ItemCountPerRequest"] = value;
					return;
				}
				throw new ArgumentException("ItemCountPerRequest value should be more than zero.", value.ToString());
			}
		}

		// Token: 0x170022E7 RID: 8935
		// (get) Token: 0x06006A92 RID: 27282 RVA: 0x0018F227 File Offset: 0x0018D427
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("Gets the settings(service path and method name)for the web service used to populate items.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x170022E8 RID: 8936
		// (get) Token: 0x06006A93 RID: 27283 RVA: 0x0018F22F File Offset: 0x0018D42F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The animation played when the dropdown is opened")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x170022E9 RID: 8937
		// (get) Token: 0x06006A94 RID: 27284 RVA: 0x0018F237 File Offset: 0x0018D437
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("The animation played when the dropdown is closed")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x170022EA RID: 8938
		// (get) Token: 0x06006A95 RID: 27285 RVA: 0x0018F23F File Offset: 0x0018D43F
		// (set) Token: 0x06006A96 RID: 27286 RVA: 0x0018F260 File Offset: 0x0018D460
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientPropertyName("_enableScreenBoundaryDetection")]
		[Bindable(false)]
		[ClientControlProperty]
		public bool EnableScreenBoundaryDetection
		{
			get
			{
				return (bool)(this.ViewState["EnableScreenBoundaryDetection"] ?? true);
			}
			set
			{
				this.ViewState["EnableScreenBoundaryDetection"] = value;
			}
		}

		// Token: 0x170022EB RID: 8939
		// (get) Token: 0x06006A97 RID: 27287 RVA: 0x0018F278 File Offset: 0x0018D478
		// (set) Token: 0x06006A98 RID: 27288 RVA: 0x0018F299 File Offset: 0x0018D499
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_enableDirectionDetection")]
		public bool EnableDirectionDetection
		{
			get
			{
				return (bool)(this.ViewState["EnableDirectionDetection"] ?? false);
			}
			set
			{
				this.ViewState["EnableDirectionDetection"] = value;
			}
		}

		// Token: 0x170022EC RID: 8940
		// (get) Token: 0x06006A99 RID: 27289 RVA: 0x0018F2B4 File Offset: 0x0018D4B4
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06006A9A RID: 27290 RVA: 0x0018F2D9 File Offset: 0x0018D4D9
		// (remove) Token: 0x06006A9B RID: 27291 RVA: 0x0018F2EC File Offset: 0x0018D4EC
		public event DropDownListEventHandler ItemSelected
		{
			add
			{
				base.Events.AddHandler(RadDropDownList.ItemSelectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownList.ItemSelectedEvent, value);
			}
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x0018F2FF File Offset: 0x0018D4FF
		protected void OnItemSelected(DropDownListEventArgs e)
		{
			this.RaiseEvent(RadDropDownList.ItemSelectedEvent, e);
		}

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06006A9D RID: 27293 RVA: 0x0018F30D File Offset: 0x0018D50D
		// (remove) Token: 0x06006A9E RID: 27294 RVA: 0x0018F320 File Offset: 0x0018D520
		public event DropDownListEventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadDropDownList.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownList.SelectedIndexChangedEvent, value);
			}
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x0018F333 File Offset: 0x0018D533
		protected void OnSelectedIndexChanged(DropDownListEventArgs e)
		{
			this.RaiseEvent(RadDropDownList.SelectedIndexChangedEvent, e);
			this.TrackSelectedIndexChanged(e);
		}

		// Token: 0x06006AA0 RID: 27296 RVA: 0x0018F364 File Offset: 0x0018D564
		protected virtual void TrackSelectedIndexChanged(DropDownListEventArgs e)
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "SelectionChanged").OfValue(() => e.Value).OfClass(FeatureClass.Selection));
		}

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06006AA1 RID: 27297 RVA: 0x0018F3C7 File Offset: 0x0018D5C7
		// (remove) Token: 0x06006AA2 RID: 27298 RVA: 0x0018F3DA File Offset: 0x0018D5DA
		public event DropDownListItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadDropDownList.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownList.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x06006AA3 RID: 27299 RVA: 0x0018F3ED File Offset: 0x0018D5ED
		protected void OnItemDataBound(DropDownListItemEventArgs e)
		{
			this.RaiseItemEvent(RadDropDownList.ItemDataBoundEvent, e);
		}

		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x06006AA4 RID: 27300 RVA: 0x0018F3FB File Offset: 0x0018D5FB
		// (remove) Token: 0x06006AA5 RID: 27301 RVA: 0x0018F40E File Offset: 0x0018D60E
		public event DropDownListItemEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadDropDownList.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownList.TemplateNeededEvent, value);
			}
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x0018F421 File Offset: 0x0018D621
		protected void OnTemplateNeeded(DropDownListItemEventArgs e)
		{
			this.RaiseItemEvent(RadDropDownList.TemplateNeededEvent, e);
		}

		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x06006AA7 RID: 27303 RVA: 0x0018F42F File Offset: 0x0018D62F
		// (remove) Token: 0x06006AA8 RID: 27304 RVA: 0x0018F442 File Offset: 0x0018D642
		public event DropDownListItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadDropDownList.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownList.ItemCreatedEvent, value);
			}
		}

		// Token: 0x06006AA9 RID: 27305 RVA: 0x0018F455 File Offset: 0x0018D655
		protected virtual void OnItemCreated(DropDownListItemEventArgs e)
		{
			this.RaiseItemEvent(RadDropDownList.ItemCreatedEvent, e);
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x0018F464 File Offset: 0x0018D664
		private void RaiseEvent(object eventKey, DropDownListEventArgs e)
		{
			DropDownListEventHandler dropDownListEventHandler = (DropDownListEventHandler)base.Events[eventKey];
			if (dropDownListEventHandler != null)
			{
				dropDownListEventHandler(this, e);
			}
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x0018F490 File Offset: 0x0018D690
		private void RaiseItemEvent(object eventKey, DropDownListItemEventArgs e)
		{
			DropDownListItemEventHandler dropDownListItemEventHandler = (DropDownListItemEventHandler)base.Events[eventKey];
			if (dropDownListItemEventHandler != null)
			{
				dropDownListItemEventHandler(this, e);
			}
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x0018F4BC File Offset: 0x0018D6BC
		public void ClearSelection()
		{
			foreach (object obj in this.Items)
			{
				DropDownListItem dropDownListItem = (DropDownListItem)obj;
				dropDownListItem.Selected = false;
			}
			this._SelectedText = string.Empty;
			this._SelectedValue = string.Empty;
		}

		// Token: 0x06006AAD RID: 27309 RVA: 0x0018F52C File Offset: 0x0018D72C
		public void LoadContentFile(string xmlFileName)
		{
			base.LoadXml(File.ReadAllText(this.Context.Server.MapPath(xmlFileName)));
		}

		// Token: 0x06006AAE RID: 27310 RVA: 0x0018F54A File Offset: 0x0018D74A
		public DropDownListItem FindItemByText(string text)
		{
			return this.FindItemByText(text, false);
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x0018F554 File Offset: 0x0018D754
		public DropDownListItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<DropDownListItem>(text, ignoreCase);
		}

		// Token: 0x06006AB0 RID: 27312 RVA: 0x0018F55E File Offset: 0x0018D75E
		public DropDownListItem FindItemByValue(string value)
		{
			return this.FindItemByValue(value, false);
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x0018F568 File Offset: 0x0018D768
		public DropDownListItem FindItemByValue(string value, bool ignoreCase)
		{
			return this.FindChildByValue<DropDownListItem>(value, ignoreCase);
		}

		// Token: 0x170022ED RID: 8941
		// (get) Token: 0x06006AB2 RID: 27314 RVA: 0x0018F572 File Offset: 0x0018D772
		// (set) Token: 0x06006AB3 RID: 27315 RVA: 0x0018F592 File Offset: 0x0018D792
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		[Description("The JavaScript function executed when RadDropDownList is initialized")]
		[DefaultValue("")]
		public string OnClientLoad
		{
			get
			{
				return (string)(this.ViewState["OnClientLoad"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x170022EE RID: 8942
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x0018F5A5 File Offset: 0x0018D7A5
		// (set) Token: 0x06006AB5 RID: 27317 RVA: 0x0018F5C5 File Offset: 0x0018D7C5
		[Description("The client-side event this is fired when the drop down is about to be opened.")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Bindable(false)]
		[ClientPropertyName("dropDownOpening")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientDropDownOpening
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownOpening"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownOpening"] = value;
			}
		}

		// Token: 0x170022EF RID: 8943
		// (get) Token: 0x06006AB6 RID: 27318 RVA: 0x0018F5D8 File Offset: 0x0018D7D8
		// (set) Token: 0x06006AB7 RID: 27319 RVA: 0x0018F5F8 File Offset: 0x0018D7F8
		[Description("The client-side event this is fired when the drop down is being opened.")]
		[ClientPropertyName("dropDownOpened")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Bindable(false)]
		public string OnClientDropDownOpened
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownOpened"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownOpened"] = value;
			}
		}

		// Token: 0x170022F0 RID: 8944
		// (get) Token: 0x06006AB8 RID: 27320 RVA: 0x0018F60B File Offset: 0x0018D80B
		// (set) Token: 0x06006AB9 RID: 27321 RVA: 0x0018F62B File Offset: 0x0018D82B
		[ClientPropertyName("dropDownClosing")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(false)]
		[Description("The client-side event that is fired when the drop down is about to be closed.")]
		public string OnClientDropDownClosing
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownClosing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownClosing"] = value;
			}
		}

		// Token: 0x170022F1 RID: 8945
		// (get) Token: 0x06006ABA RID: 27322 RVA: 0x0018F63E File Offset: 0x0018D83E
		// (set) Token: 0x06006ABB RID: 27323 RVA: 0x0018F65E File Offset: 0x0018D85E
		[ClientControlEvent]
		[ClientPropertyName("dropDownClosed")]
		[Category("Client-side events")]
		[Description("The client-side event that is fired when the drop down is being closed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Bindable(false)]
		public string OnClientDropDownClosed
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownClosed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownClosed"] = value;
			}
		}

		// Token: 0x170022F2 RID: 8946
		// (get) Token: 0x06006ABC RID: 27324 RVA: 0x0018F671 File Offset: 0x0018D871
		// (set) Token: 0x06006ABD RID: 27325 RVA: 0x0018F691 File Offset: 0x0018D891
		[ClientPropertyName("selectedIndexChanged")]
		[DefaultValue("")]
		[Bindable(false)]
		[ClientControlEvent]
		[Description("The client-side event that is fired after the selected index of the dropDownList has changed.")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientSelectedIndexChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientSelectedIndexChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSelectedIndexChanged"] = value;
			}
		}

		// Token: 0x170022F3 RID: 8947
		// (get) Token: 0x06006ABE RID: 27326 RVA: 0x0018F6A4 File Offset: 0x0018D8A4
		// (set) Token: 0x06006ABF RID: 27327 RVA: 0x0018F6C4 File Offset: 0x0018D8C4
		[ClientPropertyName("itemSelecting")]
		[Description("The client-side event that is fired when a dropDownListItem is about to be selected.")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(false)]
		public string OnClientItemSelecting
		{
			get
			{
				return (string)(this.ViewState["OnClientItemSelecting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemSelecting"] = value;
			}
		}

		// Token: 0x170022F4 RID: 8948
		// (get) Token: 0x06006AC0 RID: 27328 RVA: 0x0018F6D7 File Offset: 0x0018D8D7
		// (set) Token: 0x06006AC1 RID: 27329 RVA: 0x0018F6F7 File Offset: 0x0018D8F7
		[Description("The client-side event that is fired when a dropDownListItem is selected.")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Bindable(false)]
		[ClientPropertyName("itemSelected")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientItemSelected
		{
			get
			{
				return (string)(this.ViewState["OnClientItemSelected"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemSelected"] = value;
			}
		}

		// Token: 0x170022F5 RID: 8949
		// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x0018F70A File Offset: 0x0018D90A
		// (set) Token: 0x06006AC3 RID: 27331 RVA: 0x0018F72A File Offset: 0x0018D92A
		[ClientPropertyName("templateDataBound")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when the client template for a node is evaluated")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnClientTemplateDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientTemplateDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTemplateDataBound"] = value;
			}
		}

		// Token: 0x170022F6 RID: 8950
		// (get) Token: 0x06006AC4 RID: 27332 RVA: 0x0018F73D File Offset: 0x0018D93D
		// (set) Token: 0x06006AC5 RID: 27333 RVA: 0x0018F75D File Offset: 0x0018D95D
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the JavaScript function called when an item is created during Web Service binding.")]
		[ClientPropertyName("itemDataBound")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientItemDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientItemDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemDataBound"] = value;
			}
		}

		// Token: 0x170022F7 RID: 8951
		// (get) Token: 0x06006AC6 RID: 27334 RVA: 0x0018F770 File Offset: 0x0018D970
		// (set) Token: 0x06006AC7 RID: 27335 RVA: 0x0018F790 File Offset: 0x0018D990
		[ClientControlEvent]
		[ClientPropertyName("itemsRequesting")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The client-side event that is fired before the items are requested.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientItemsRequesting
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequesting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequesting"] = value;
			}
		}

		// Token: 0x170022F8 RID: 8952
		// (get) Token: 0x06006AC8 RID: 27336 RVA: 0x0018F7A3 File Offset: 0x0018D9A3
		// (set) Token: 0x06006AC9 RID: 27337 RVA: 0x0018F7C3 File Offset: 0x0018D9C3
		[Description("The name of the javascript function called after the request for items has completed.")]
		[ClientControlEvent]
		[ClientPropertyName("itemsRequested")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientItemsRequested
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequested"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequested"] = value;
			}
		}

		// Token: 0x170022F9 RID: 8953
		// (get) Token: 0x06006ACA RID: 27338 RVA: 0x0018F7D6 File Offset: 0x0018D9D6
		// (set) Token: 0x06006ACB RID: 27339 RVA: 0x0018F7F6 File Offset: 0x0018D9F6
		[DefaultValue("")]
		[ClientPropertyName("itemsRequestFailed")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the javascript function called after the request for items has failed.")]
		[Category("Client-side events")]
		public string OnClientItemsRequestFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequestFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequestFailed"] = value;
			}
		}

		// Token: 0x170022FA RID: 8954
		// (get) Token: 0x06006ACC RID: 27340 RVA: 0x0018F809 File Offset: 0x0018DA09
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170022FB RID: 8955
		// (get) Token: 0x06006ACD RID: 27341 RVA: 0x0018F80C File Offset: 0x0018DA0C
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x0018F80F File Offset: 0x0018DA0F
		protected internal override IRenderer CreateControlRenderer()
		{
			return new DropDownListRenderer(this);
		}

		// Token: 0x170022FC RID: 8956
		// (get) Token: 0x06006ACF RID: 27343 RVA: 0x0018F817 File Offset: 0x0018DA17
		// (set) Token: 0x06006AD0 RID: 27344 RVA: 0x0018F842 File Offset: 0x0018DA42
		[ClientControlProperty]
		[DefaultValue(-1)]
		[ClientPropertyName("_cachedSelectedIndex")]
		internal int _CachedSelectedIndex
		{
			get
			{
				if (this.ViewState["_CachedSelectedIndex"] == null)
				{
					return -1;
				}
				return (int)this.ViewState["_CachedSelectedIndex"];
			}
			set
			{
				this.ViewState["_CachedSelectedIndex"] = value;
			}
		}

		// Token: 0x170022FD RID: 8957
		// (get) Token: 0x06006AD1 RID: 27345 RVA: 0x0018F85A File Offset: 0x0018DA5A
		// (set) Token: 0x06006AD2 RID: 27346 RVA: 0x0018F889 File Offset: 0x0018DA89
		[DefaultValue("")]
		internal string _CachedSelectedText
		{
			get
			{
				if (this.ViewState["_CachedSelectedText"] == null)
				{
					return string.Empty;
				}
				return this.ViewState["_CachedSelectedText"] as string;
			}
			set
			{
				this.ViewState["_CachedSelectedText"] = value;
			}
		}

		// Token: 0x170022FE RID: 8958
		// (get) Token: 0x06006AD3 RID: 27347 RVA: 0x0018F89C File Offset: 0x0018DA9C
		// (set) Token: 0x06006AD4 RID: 27348 RVA: 0x0018F8B3 File Offset: 0x0018DAB3
		[DefaultValue("")]
		internal string _CachedSelectedValue
		{
			get
			{
				return this.ViewState["_CachedSelectedValue"] as string;
			}
			set
			{
				this.ViewState["_CachedSelectedValue"] = value;
			}
		}

		// Token: 0x170022FF RID: 8959
		// (get) Token: 0x06006AD5 RID: 27349 RVA: 0x0018F8C6 File Offset: 0x0018DAC6
		// (set) Token: 0x06006AD6 RID: 27350 RVA: 0x0018F8E6 File Offset: 0x0018DAE6
		internal string _SelectedText
		{
			get
			{
				return (this.ViewState["_SelectedText"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["_SelectedText"] = value;
			}
		}

		// Token: 0x17002300 RID: 8960
		// (get) Token: 0x06006AD7 RID: 27351 RVA: 0x0018F8F9 File Offset: 0x0018DAF9
		// (set) Token: 0x06006AD8 RID: 27352 RVA: 0x0018F919 File Offset: 0x0018DB19
		internal string _SelectedValue
		{
			get
			{
				return (this.ViewState["_SelectedValue"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["_SelectedValue"] = value;
			}
		}

		// Token: 0x17002301 RID: 8961
		// (get) Token: 0x06006AD9 RID: 27353 RVA: 0x0018F92C File Offset: 0x0018DB2C
		internal bool IsUsingWebServiceBinding
		{
			get
			{
				return !string.IsNullOrEmpty(this.WebServiceSettings.Path);
			}
		}

		// Token: 0x17002302 RID: 8962
		// (get) Token: 0x06006ADA RID: 27354 RVA: 0x0018F941 File Offset: 0x0018DB41
		internal override bool SupportsOData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002303 RID: 8963
		// (get) Token: 0x06006ADB RID: 27355 RVA: 0x0018F944 File Offset: 0x0018DB44
		internal bool IsControlEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x0018F94C File Offset: 0x0018DB4C
		public RadDropDownList()
		{
			this._webServiceSettings = new WebServiceSettings(this.ViewState);
			this._expandAnimation = new AnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new AnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x0018F9B2 File Offset: 0x0018DBB2
		protected internal override ControlItem CreateItem()
		{
			return new DropDownListItem();
		}

		// Token: 0x06006ADE RID: 27358 RVA: 0x0018F9B9 File Offset: 0x0018DBB9
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnItemDataBound(new DropDownListItemEventArgs((DropDownListItem)item));
		}

		// Token: 0x06006ADF RID: 27359 RVA: 0x0018F9CC File Offset: 0x0018DBCC
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnItemCreated(new DropDownListItemEventArgs((DropDownListItem)item));
		}

		// Token: 0x06006AE0 RID: 27360 RVA: 0x0018F9DF File Offset: 0x0018DBDF
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new DropDownListItemEventArgs((DropDownListItem)item));
		}

		// Token: 0x06006AE1 RID: 27361 RVA: 0x0018F9F2 File Offset: 0x0018DBF2
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new DropDownListItemCollection(this);
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x0018F9FC File Offset: 0x0018DBFC
		protected override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			if (!string.IsNullOrEmpty(this._CachedSelectedText))
			{
				DropDownListItem dropDownListItem = base.FindChildByText<DropDownListItem>(this._CachedSelectedText);
				if (dropDownListItem != null)
				{
					this.ClearSelection();
					dropDownListItem.Selected = true;
					this._SelectedText = this._CachedSelectedText;
					return;
				}
			}
			else if (this._CachedSelectedValue != null)
			{
				DropDownListItem dropDownListItem2 = this.FindChildByValue<DropDownListItem>(this._CachedSelectedValue);
				if (dropDownListItem2 != null)
				{
					this.ClearSelection();
					dropDownListItem2.Selected = true;
					this._SelectedValue = this._CachedSelectedValue;
					return;
				}
			}
			else if (this._CachedSelectedIndex != -1)
			{
				this.SelectedIndex = this._CachedSelectedIndex;
			}
		}

		// Token: 0x17002304 RID: 8964
		// (get) Token: 0x06006AE3 RID: 27363 RVA: 0x0018FA8E File Offset: 0x0018DC8E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x17002305 RID: 8965
		// (get) Token: 0x06006AE4 RID: 27364 RVA: 0x0018FA9B File Offset: 0x0018DC9B
		protected override string CssClassFormatString
		{
			get
			{
				return "RadDropDownList RadDropDownList_{0}";
			}
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x0018FAA2 File Offset: 0x0018DCA2
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x0018FAB0 File Offset: 0x0018DCB0
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x0018FAB9 File Offset: 0x0018DCB9
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06006AE8 RID: 27368 RVA: 0x0018FAD0 File Offset: 0x0018DCD0
		string ICallbackEventHandler.GetCallbackResult()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			DropDownListCallbackArguments dropDownListCallbackArguments = javaScriptSerializer.Deserialize<DropDownListCallbackArguments>(this.callbackArguments);
			List<DropDownListItem> list = new List<DropDownListItem>();
			StringWriter stringWriter = new StringWriter();
			if (this.Items == null)
			{
				return string.Empty;
			}
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new DropDownListItemConverter()
			});
			int num = Math.Min(Math.Max(dropDownListCallbackArguments.StartIndex, 0), this.Items.Count);
			int num2 = Math.Min(num + dropDownListCallbackArguments.Count, this.Items.Count);
			for (int i = num; i < num2; i++)
			{
				DropDownListItem dropDownListItem = this.Items[i];
				dropDownListItem.RenderControl(new HtmlTextWriter(stringWriter));
				list.Add(dropDownListItem);
			}
			string arg = javaScriptSerializer.Serialize(list);
			return arg + "_$$_" + stringWriter;
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x0018FBB3 File Offset: 0x0018DDB3
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.callbackArguments = eventArgument;
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x0018FBBC File Offset: 0x0018DDBC
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			RadDropDownListClientState radDropDownListClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				radDropDownListClientState = javaScriptSerializer.Deserialize<RadDropDownListClientState>(text);
				radDropDownListClientState.SelectedText = this.DecodeText(radDropDownListClientState.SelectedText);
				radDropDownListClientState.SelectedValue = this.DecodeText(radDropDownListClientState.SelectedValue);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (radDropDownListClientState == null)
			{
				return false;
			}
			this.LoadClientState(radDropDownListClientState);
			return false;
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x0018FC4C File Offset: 0x0018DE4C
		private void LoadClientState(RadDropDownListClientState clientState)
		{
			this.Enabled = clientState.Enabled;
			if (clientState.LogEntries != null)
			{
				this.LoadLogEntries(clientState);
			}
			this.SelectedIndex = clientState.SelectedIndex;
			this._SelectedText = clientState.SelectedText;
			this._SelectedValue = clientState.SelectedValue;
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x0018FC98 File Offset: 0x0018DE98
		private void LoadLogEntries(RadDropDownListClientState clientState)
		{
			ClientStateLogPlayer<DropDownListItem> clientStateLogPlayer = new ClientStateLogPlayer<DropDownListItem>(this);
			this._clientChanges = clientStateLogPlayer.Play(clientState.LogEntries);
		}

		// Token: 0x06006AED RID: 27373 RVA: 0x0018FCBE File Offset: 0x0018DEBE
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06006AEE RID: 27374 RVA: 0x0018FCC8 File Offset: 0x0018DEC8
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			DropDownListPostBackCommand dropDownListPostBackCommand = null;
			try
			{
				dropDownListPostBackCommand = new JavaScriptSerializer().Deserialize<DropDownListPostBackCommand>(eventArgument);
				dropDownListPostBackCommand.Text = this.DecodeText(dropDownListPostBackCommand.Text);
				dropDownListPostBackCommand.Value = this.DecodeText(dropDownListPostBackCommand.Value);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (dropDownListPostBackCommand == null)
			{
				return;
			}
			DropDownListEventArgs dropDownListEventArgs = new DropDownListEventArgs(dropDownListPostBackCommand.Index, dropDownListPostBackCommand.Text, dropDownListPostBackCommand.Value);
			switch (dropDownListPostBackCommand.Type)
			{
			case DropDownListCommand.ItemSelected:
				this.OnItemSelected(dropDownListEventArgs);
				return;
			case DropDownListCommand.SelectedIndexChanged:
				if (dropDownListEventArgs.Index != -1)
				{
					this.OnItemSelected(dropDownListEventArgs);
				}
				this.OnSelectedIndexChanged(dropDownListEventArgs);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006AEF RID: 27375 RVA: 0x0018FD7C File Offset: 0x0018DF7C
		private string DecodeText(string text)
		{
			if (text != null)
			{
				text = HttpUtility.UrlDecode(text).Replace("&squote", "'");
			}
			return text;
		}

		// Token: 0x06006AF0 RID: 27376 RVA: 0x0018FD9C File Offset: 0x0018DF9C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new DropDownListItemConverter(),
				new AttributeCollectionConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			if (!this.EnableVirtualScrolling)
			{
				descriptor.AddScriptProperty("itemData", javaScriptSerializer.Serialize(this.Items));
			}
			else if (!this.IsUsingWebServiceBinding)
			{
				descriptor.AddProperty("_inlineCount", javaScriptSerializer.Serialize(this.Items.Count));
				this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			}
			if (this.IsUsingWebServiceBinding)
			{
				this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
			}
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			if (this.AutoPostBack && base.Events[RadDropDownList.ItemSelectedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnSelect", true);
			}
			if (!string.IsNullOrEmpty(this.ClientItemTemplate))
			{
				descriptor.AddProperty("_clientTemplate", this.ClientItemTemplate);
			}
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
			this.AriaSettings.Describe(descriptor);
		}

		// Token: 0x06006AF1 RID: 27377 RVA: 0x0018FF06 File Offset: 0x0018E106
		internal void Describe(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x06006AF2 RID: 27378 RVA: 0x0018FF10 File Offset: 0x0018E110
		private DropDownListItem FindFirstAvailableItem()
		{
			foreach (object obj in this.Items)
			{
				DropDownListItem dropDownListItem = (DropDownListItem)obj;
				if (dropDownListItem.Enabled)
				{
					return dropDownListItem;
				}
			}
			return null;
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x0018FF74 File Offset: 0x0018E174
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int>(descriptor, "_cachedSelectedIndex", this._CachedSelectedIndex, -1);
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<string>(descriptor, "defaultMessage", this.DefaultMessage, "");
			base.DescribeProperty<string>(descriptor, "dropDownHeight", this.DropDownHeight.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "dropDownWidth", this.DropDownWidth.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "_enableDirectionDetection", this.EnableDirectionDetection, false);
			base.DescribeProperty<bool>(descriptor, "_enableScreenBoundaryDetection", this.EnableScreenBoundaryDetection, true);
			base.DescribeProperty<bool>(descriptor, "enableVirtualScrolling", this.EnableVirtualScrolling, false);
			base.DescribeProperty<DropDownListExpandDirection>(descriptor, "expandDirection", this.ExpandDirection, DropDownListExpandDirection.Down);
			base.DescribeProperty<int>(descriptor, "itemCountPerRequest", this.ItemCountPerRequest, -1);
			base.DescribeProperty<string>(descriptor, "_loadingPanelID", this.LoadingPanelID, "");
			base.DescribeProperty<int>(descriptor, "_selectedIndex", this.SelectedIndex, -1);
			base.DescribeProperty<string>(descriptor, "_selectedText", this.SelectedText, "");
			base.DescribeProperty<string>(descriptor, "_selectedValue", this.SelectedValue, "");
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06006AF4 RID: 27380 RVA: 0x001900EC File Offset: 0x0018E2EC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosed", this.OnClientDropDownClosed);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosing", this.OnClientDropDownClosing);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpened", this.OnClientDropDownOpened);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpening", this.OnClientDropDownOpening);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDataBound", this.OnClientItemDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemSelected", this.OnClientItemSelected);
			RadDataBoundControl.DescribeEvent(descriptor, "itemSelecting", this.OnClientItemSelecting);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequested", this.OnClientItemsRequested);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequestFailed", this.OnClientItemsRequestFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequesting", this.OnClientItemsRequesting);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "selectedIndexChanged", this.OnClientSelectedIndexChanged);
			RadDataBoundControl.DescribeEvent(descriptor, "templateDataBound", this.OnClientTemplateDataBound);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x001901DD File Offset: 0x0018E3DD
		// Note: this type is marked as 'beforefieldinit'.
		static RadDropDownList()
		{
			RadDropDownList.ItemSelectedEvent = new object();
			RadDropDownList.SelectedIndexChangedEvent = new object();
			RadDropDownList.ItemDataBoundEvent = new object();
			RadDropDownList.TemplateNeededEvent = new object();
			RadDropDownList.ItemCreatedEvent = new object();
		}

		// Token: 0x04001CD0 RID: 7376
		private WaiAriaSettings _ariaSettings;

		// Token: 0x04001CD6 RID: 7382
		private IList<ClientOperation<DropDownListItem>> _clientChanges = new List<ClientOperation<DropDownListItem>>();

		// Token: 0x04001CD7 RID: 7383
		private readonly WebServiceSettings _webServiceSettings;

		// Token: 0x04001CD8 RID: 7384
		private readonly AnimationSettings _expandAnimation;

		// Token: 0x04001CD9 RID: 7385
		private readonly AnimationSettings _collapseAnimation;

		// Token: 0x04001CDA RID: 7386
		private string callbackArguments = string.Empty;
	}
}
