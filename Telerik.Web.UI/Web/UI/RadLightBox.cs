using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02000571 RID: 1393
	[ToolboxBitmap(typeof(RadLightBox), "Telerik.Web.UI.LightBox.png")]
	[RequiredScript(typeof(jQueryPlugins), 2)]
	[RequiredScript(typeof(ImageAnimations), 3)]
	[Designer("Telerik.Web.Design.RadLightBoxDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[AdaptiveRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadLightBox))]
	[TelerikToolboxCategory("Data")]
	[RequiredScript(typeof(Core), 1)]
	[Description("Telerik RadLightBox")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadGrid))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadLightBox))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadLightBox", "Telerik.Web.UI.LightBox.RadLightBoxScripts.js")]
	[ToolboxData("<{0}:RadLightBox runat=server></{0}:RadLightBox>")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadLightBox))]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("LightBox")]
	[EmbeddedSkin("LightBox", "Default")]
	public class RadLightBox : RadDataBoundControl, INamingContainer, IPostBackEventHandler, ILocalizableControl
	{
		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000A413E File Offset: 0x000A233E
		// (set) Token: 0x0600320F RID: 12815 RVA: 0x000A4146 File Offset: 0x000A2346
		internal bool VisibleOnLoad { get; set; }

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000A414F File Offset: 0x000A234F
		internal LightBoxControlStateManager ControlState
		{
			get
			{
				if (this.controlStateManager == null)
				{
					this.controlStateManager = new LightBoxControlStateManager();
				}
				return this.controlStateManager;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000A416A File Offset: 0x000A236A
		internal RadLightBoxItem CurrentItem
		{
			get
			{
				if (this.CurrentItemIndex < this.Items.Count)
				{
					return this.Items[this.CurrentItemIndex];
				}
				return null;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x000A4192 File Offset: 0x000A2392
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000A4195 File Offset: 0x000A2395
		// (set) Token: 0x06003214 RID: 12820 RVA: 0x000A419D File Offset: 0x000A239D
		internal Panel LightBoxToolbarContainer { get; set; }

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000A41A6 File Offset: 0x000A23A6
		// (set) Token: 0x06003216 RID: 12822 RVA: 0x000A41AE File Offset: 0x000A23AE
		internal Panel LightBoxContainer { get; set; }

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000A41B7 File Offset: 0x000A23B7
		// (set) Token: 0x06003218 RID: 12824 RVA: 0x000A41BF File Offset: 0x000A23BF
		internal Panel LightBoxDescriptionTemplateWrapper { get; set; }

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000A41C8 File Offset: 0x000A23C8
		// (set) Token: 0x0600321A RID: 12826 RVA: 0x000A41D0 File Offset: 0x000A23D0
		internal Panel LightBoxItemTemplateWrapper { get; set; }

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000A41DC File Offset: 0x000A23DC
		internal System.Web.UI.WebControls.Image LightBoxImageControl
		{
			get
			{
				if (this.lightBoxImageControl == null)
				{
					this.lightBoxImageControl = new System.Web.UI.WebControls.Image();
					this.lightBoxImageControl.CssClass = "rltbActiveImage";
					this.lightBoxImageControl.AlternateText = this.Localization.ActiveImageAltText;
					this.lightBoxImageControl.ImageUrl = RadLightBox.emptyImageData;
				}
				return this.lightBoxImageControl;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000A4238 File Offset: 0x000A2438
		internal HtmlGenericControl LightBoxTitleControl
		{
			get
			{
				if (this.lightBoxTitleControl == null)
				{
					this.lightBoxTitleControl = new HtmlGenericControl("h4");
					this.LightBoxTitleControl.Attributes.Add("class", "rltbTitle");
				}
				return this.lightBoxTitleControl;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000A4272 File Offset: 0x000A2472
		internal HtmlGenericControl LightBoxDescriptionControl
		{
			get
			{
				if (this.lightBoxDescriptionControl == null)
				{
					this.lightBoxDescriptionControl = new HtmlGenericControl("div");
					this.lightBoxDescriptionControl.Attributes.Add("class", "rltbDescription");
				}
				return this.lightBoxDescriptionControl;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000A42AC File Offset: 0x000A24AC
		internal Button PrevButton
		{
			get
			{
				if (this.prevButton == null)
				{
					this.prevButton = new Button();
					this.prevButton.CssClass = "rltbActionButton rltbPrevButton";
					this.prevButton.Style.Add("display", "none");
					this.prevButton.Text = this.Localization.PrevButtonText;
					this.prevButton.ToolTip = this.Localization.PrevButtonText;
					this.prevButton.ID = "PrevButton";
				}
				return this.prevButton;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x000A4338 File Offset: 0x000A2538
		internal Button NextButton
		{
			get
			{
				if (this.nextButton == null)
				{
					this.nextButton = new Button();
					this.nextButton.CssClass = "rltbActionButton rltbNextButton";
					this.nextButton.Style.Add("display", "none");
					this.nextButton.Text = this.Localization.NextButtonText;
					this.nextButton.ID = "NextButton";
				}
				return this.nextButton;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000A43B0 File Offset: 0x000A25B0
		internal RadAjaxLoadingPanel LoadingPanel
		{
			get
			{
				if (this.loadingPanel == null)
				{
					this.loadingPanel = new RadAjaxLoadingPanel();
					this.loadingPanel.ID = "LoadingPanel";
					this.loadingPanel.ZIndex = this.ZIndex + 2;
					if (!this.EnableEmbeddedSkins)
					{
						this.loadingPanel.EnableEmbeddedSkins = false;
					}
				}
				return this.loadingPanel;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000A440D File Offset: 0x000A260D
		// (set) Token: 0x06003222 RID: 12834 RVA: 0x000A442D File Offset: 0x000A262D
		internal string LoadingPanelID
		{
			get
			{
				return ((string)this.ViewState["LoadingPanelID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["LoadingPanelID"] = value;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000A4440 File Offset: 0x000A2640
		internal bool UsesControlState
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000A444B File Offset: 0x000A264B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal LightBoxStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new LightBoxStrings(new LocalizationProvider("RadLightBox.Main", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x000A448A File Offset: 0x000A268A
		protected override string CssClassFormatString
		{
			get
			{
				return "RadLightBox RadLightBox_{0}";
			}
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000A4494 File Offset: 0x000A2694
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(base.SaveViewState());
			arrayList.Add(((IStateManager)this.ClientSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.Items).SaveViewState());
			if (!this.UsesControlState)
			{
				this.SaveControlStateObject(arrayList);
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000A44F8 File Offset: 0x000A26F8
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int currentStateIndex = 0;
				base.LoadViewState(array[currentStateIndex++]);
				((IStateManager)this.ClientSettings).LoadViewState(array[currentStateIndex++]);
				((IStateManager)this.Items).LoadViewState(array[currentStateIndex++]);
				if (!this.UsesControlState)
				{
					this.LoadControlStateObject(array, currentStateIndex);
				}
			}
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000A4552 File Offset: 0x000A2752
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
			((IStateManager)this.ClientSettings).TrackViewState();
			((IStateManager)this.ControlState).TrackViewState();
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000A457B File Offset: 0x000A277B
		protected virtual void SaveControlStateObject(IList state)
		{
			state.Add(((IStateManager)this.ControlState).SaveViewState());
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000A458F File Offset: 0x000A278F
		protected virtual void LoadControlStateObject(object[] stateArray, int currentStateIndex)
		{
			((IStateManager)this.ControlState).LoadViewState(stateArray[currentStateIndex++]);
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000A45A4 File Offset: 0x000A27A4
		protected override object SaveControlState()
		{
			object value = base.SaveControlState();
			ArrayList arrayList = new ArrayList();
			arrayList.Add(value);
			this.SaveControlStateObject(arrayList);
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000A45E0 File Offset: 0x000A27E0
		protected override void LoadControlState(object savedState)
		{
			object[] array = savedState as object[];
			if (array != null)
			{
				base.LoadControlState(array);
				this.LoadControlStateObject(array, 1);
				return;
			}
			base.LoadControlState(savedState);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000A4628 File Offset: 0x000A2828
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.DescribeProperties(descriptor);
			this.RegisterClientSideEvents(delegate(string eventName, string eventValue)
			{
				RadDataBoundControl.DescribeEvent(descriptor, eventName, eventValue);
			});
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000A4680 File Offset: 0x000A2880
		private void RegisterClientSideEvents(TAction<string, string> eventData)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ClientSettings.ClientEvents);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.DisplayName == "ViewState"))
				{
					string text = propertyDescriptor.DisplayName.Replace("On", string.Empty);
					text = Regex.Replace(text, "^[A-Z]", (Match match) => match.ToString().ToLower(CultureInfo.InvariantCulture));
					string text2 = propertyDescriptor.GetValue(this.ClientSettings.ClientEvents).ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						eventData(text, text2);
					}
				}
			}
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000A4764 File Offset: 0x000A2964
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new LightBoxJavaScriptConverter()
			});
			if (!this.ShowCloseButton)
			{
				descriptor.AddProperty("_removeCloseButton", !this.ShowCloseButton);
			}
			if (!this.ShowPrevButton)
			{
				descriptor.AddProperty("_removePrevButton", !this.ShowPrevButton);
			}
			if (!this.ShowNextButton)
			{
				descriptor.AddProperty("_removeNextButton", !this.ShowNextButton);
			}
			if (!this.ShowMaximizeButton)
			{
				descriptor.AddProperty("_removeMaximizeButton", !this.ShowMaximizeButton);
			}
			if (!this.ShowRestoreButton)
			{
				descriptor.AddProperty("_removeRestoreButton", !this.ShowRestoreButton);
			}
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			if (this.CurrentItemIndex > 0)
			{
				descriptor.AddProperty("_currentItemIndex", this.CurrentItemIndex);
			}
			if (!string.IsNullOrEmpty(this.ItemsCounterFormatString) && this.ItemsCounterFormatString != "Image {0} of {1}")
			{
				descriptor.AddProperty("_itemsCounterFormatString", this.ItemsCounterFormatString);
			}
			if (this.Modal || this.ResolvedRenderMode == RenderMode.Mobile)
			{
				descriptor.AddProperty("_modal", true);
			}
			if (this.LoopItems)
			{
				descriptor.AddProperty("_loopItems", this.LoopItems);
			}
			if (this.ZIndex != 3500)
			{
				descriptor.AddProperty("_zIndex", this.ZIndex);
			}
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			if (this.VisibleOnLoad)
			{
				descriptor.AddProperty("_visibleOnLoad", this.VisibleOnLoad);
			}
			if (this.DescriptionPosition != LightBoxDescriptionPosition.Bottom)
			{
				descriptor.AddProperty("_descriptionPosition", this.DescriptionPosition);
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
				}
				catch (GridException)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
				}
			}
			if (this.ResolvedRenderMode != RenderMode.Classic)
			{
				descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			}
			if (this.PreserveCurrentItemTemplates)
			{
				descriptor.AddProperty("_preserveTemplates", this.PreserveCurrentItemTemplates);
			}
			if (this.TabIndex != 101)
			{
				descriptor.AddProperty("_tabIndex", this.TabIndex);
			}
			if (!this.Width.IsEmpty)
			{
				descriptor.AddProperty("_width", this.Width.ToString());
			}
			if (!this.Height.IsEmpty)
			{
				descriptor.AddProperty("_height", this.Height.ToString());
			}
			if (this.ShowLoadingPanel)
			{
				descriptor.AddProperty("_loadingPanelID", this.LoadingPanelID);
			}
			if (!string.IsNullOrEmpty(this.DataDescriptionField))
			{
				descriptor.AddProperty("_descriptionField", this.DataDescriptionField);
			}
			if (!string.IsNullOrEmpty(this.DataImageUrlField))
			{
				descriptor.AddProperty("_imageUrlField", this.DataImageUrlField);
			}
			if (!string.IsNullOrEmpty(this.DataNavigateUrlField))
			{
				descriptor.AddProperty("_navigateUrlField", this.DataNavigateUrlField);
			}
			if (!string.IsNullOrEmpty(this.DataTargetControlIDField))
			{
				descriptor.AddProperty("_targetControlIDField", this.DataTargetControlIDField);
			}
			if (!string.IsNullOrEmpty(this.DataTitleField))
			{
				descriptor.AddProperty("_titleField", this.DataTitleField);
			}
			string text = javaScriptSerializer.Serialize(this.ClientSettings);
			if (text != "{}")
			{
				descriptor.AddScriptProperty("_clientSettings", text);
			}
			if (this.Items.Count > 0)
			{
				descriptor.AddProperty("_data", this.DescribeClientData());
			}
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000A4B54 File Offset: 0x000A2D54
		private Dictionary<string, object> DescribeClientData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Items)
			{
				RadLightBoxItem item = (RadLightBoxItem)obj;
				arrayList.Add(this.DescribeItemData(item));
			}
			dictionary.Add("_itemsData", arrayList);
			return dictionary;
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000A4BD0 File Offset: 0x000A2DD0
		private Dictionary<string, object> DescribeItemData(RadLightBoxItem item)
		{
			return new Dictionary<string, object>
			{
				{
					"Description",
					item.Description
				},
				{
					"ImageUrl",
					base.ResolveUrl(item.ImageUrl)
				},
				{
					"NavigateUrl",
					base.ResolveUrl(item.NavigateUrl)
				},
				{
					"TargetControlID",
					this.ResolveClientID(item)
				},
				{
					"Title",
					item.Title
				},
				{
					"Width",
					item.Width.ToString()
				},
				{
					"Height",
					item.Height.ToString()
				},
				{
					"HasItemTemplate",
					item.ItemTemplate != null
				},
				{
					"HasDescriptionTemplate",
					item.DescriptionTemplate != null
				},
				{
					"IsRendered",
					item.HasTemplates && item == this.CurrentItem
				}
			};
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000A4CE0 File Offset: 0x000A2EE0
		private string ResolveClientID(RadLightBoxItem item)
		{
			if (!string.IsNullOrEmpty(item.TargetControlID))
			{
				if (item.IsClientID)
				{
					return item.TargetControlID;
				}
				Control control = ChildControlHelper.FindControlRecursive(this, item.TargetControlID, null);
				if (control != null)
				{
					return control.ClientID;
				}
			}
			return string.Empty;
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000A4D26 File Offset: 0x000A2F26
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.EnsureChildControls();
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000A4D38 File Offset: 0x000A2F38
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.Attributes.Add("style", string.Format("position: fixed; left: 50%; top: 50%; display:none; z-index: {0}", this.ZIndex + 1));
			this.NextButton.UseSubmitBehavior = true;
			this.PrevButton.UseSubmitBehavior = true;
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000A4D8C File Offset: 0x000A2F8C
		private void InstantiateItemTemplate(bool isClientTemplate)
		{
			this.LightBoxItemTemplateWrapper = new Panel();
			this.LightBoxItemTemplateWrapper.CssClass = "rltbItemTemplate";
			if (!isClientTemplate)
			{
				this.CurrentItem.ItemTemplate.InstantiateIn(this.LightBoxItemTemplateWrapper);
			}
			this.LightBoxItemContainer.Controls.Add(this.LightBoxItemTemplateWrapper);
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000A4DE4 File Offset: 0x000A2FE4
		private void InstantiateDescriptionTemplate(bool isClientTemplate)
		{
			this.LightBoxDescriptionTemplateWrapper = new Panel();
			this.LightBoxDescriptionTemplateWrapper.CssClass = "rltbDescriptionTemplate";
			if (!isClientTemplate)
			{
				this.CurrentItem.DescriptionTemplate.InstantiateIn(this.LightBoxDescriptionTemplateWrapper);
			}
			this.LightBoxDescriptionContainer.Controls.Add(this.LightBoxDescriptionTemplateWrapper);
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000A4E3C File Offset: 0x000A303C
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.LightBoxContainer = new Panel();
			this.LightBoxDescriptionContainer = new PlaceHolder();
			this.LightBoxTitleContainer = new PlaceHolder();
			this.LightBoxItemContainer = new PlaceHolder();
			this.LightBoxToolbarContainer = new Panel();
			this.Controls.Add(this.LightBoxContainer);
			this.LightBoxContainer.Controls.Add(this.LightBoxToolbarContainer);
			this.LightBoxContainer.Controls.Add(this.LightBoxTitleContainer);
			this.LightBoxContainer.Controls.Add(this.LightBoxDescriptionContainer);
			this.LightBoxContainer.Controls.Add(this.LightBoxItemContainer);
			if (this.CurrentItem != null)
			{
				if (this.CurrentItem.HasTemplates)
				{
					if (this.CurrentItem.ItemTemplate != null)
					{
						this.InstantiateItemTemplate(false);
					}
					if (this.CurrentItem.DescriptionTemplate != null)
					{
						this.InstantiateDescriptionTemplate(false);
					}
				}
			}
			else if (!string.IsNullOrEmpty(this.ClientSettings.DataBinding.ItemTemplate) || !string.IsNullOrEmpty(this.ClientSettings.DataBinding.DescriptionTemplate))
			{
				this.InstantiateItemTemplate(true);
				this.InstantiateDescriptionTemplate(true);
			}
			this.LightBoxTitleContainer.Controls.Add(this.LightBoxTitleControl);
			this.LightBoxItemContainer.Controls.Add(this.LightBoxImageControl);
			this.LightBoxDescriptionContainer.Controls.Add(this.LightBoxDescriptionControl);
			this.LightBoxContainer.Controls.Add(this.PrevButton);
			this.LightBoxContainer.Controls.Add(this.NextButton);
			if (this.ShowLoadingPanel)
			{
				this.Controls.Add(this.LoadingPanel);
				this.LoadingPanelID = this.loadingPanel.ClientID;
				this.LoadingPanel.Skin = base.RuntimeSkin;
			}
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000A5018 File Offset: 0x000A3218
		private string GetItemsCounterString()
		{
			string message = "ItemsCounterFormatString should have one or two format placeholders!";
			if (this.ItemsCounterFormatString.Contains("{0}"))
			{
				try
				{
					if (this.ItemsCounterFormatString.Contains("{1}"))
					{
						return string.Format(this.ItemsCounterFormatString, this.CurrentItemIndex + 1, this.ItemsCount);
					}
					return string.Format(this.ItemsCounterFormatString, this.CurrentItemIndex + 1);
				}
				catch (Exception ex)
				{
					if (ex is FormatException || ex is ArgumentNullException)
					{
						throw new ArgumentException(message);
					}
					throw;
				}
			}
			throw new ArgumentException(message);
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000A50C4 File Offset: 0x000A32C4
		internal virtual void InitializeStaticContent(RadLightBoxItem item)
		{
			if (item.ImageUrl != null)
			{
				this.LightBoxImageControl.ImageUrl = item.ImageUrl;
				this.LightBoxTitleControl.InnerHtml = item.Title;
				this.LightBoxDescriptionControl.InnerHtml = item.Description;
			}
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000A5104 File Offset: 0x000A3304
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbOverlay");
			writer.AddStyleAttribute("z-index", this.ZIndex.ToString());
			if (!this.VisibleOnLoad || !this.Modal)
			{
				writer.AddStyleAttribute("display", "none");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			base.RenderBeginTag(writer);
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000A516C File Offset: 0x000A336C
		private void RenderItemBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbItemBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderPrevButton(writer);
			this.LightBoxItemContainer.RenderControl(writer);
			this.RenderNextButton(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000A51A4 File Offset: 0x000A33A4
		private void RenderNextButton(HtmlTextWriter writer)
		{
			if (this.ResolvedRenderMode == RenderMode.Lightweight || this.ResolvedRenderMode == RenderMode.Mobile)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbButtonWrapper rltbNextButtonWrapper");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Localization.NextButtonText);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			this.NextButton.RenderControl(writer);
			if (this.ResolvedRenderMode == RenderMode.Lightweight || this.ResolvedRenderMode == RenderMode.Mobile)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000A5210 File Offset: 0x000A3410
		private void RenderPrevButton(HtmlTextWriter writer)
		{
			if (this.ResolvedRenderMode == RenderMode.Lightweight || this.ResolvedRenderMode == RenderMode.Mobile)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbButtonWrapper rltbPrevButtonWrapper");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Localization.PrevButtonText);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			this.PrevButton.RenderControl(writer);
			if (this.ResolvedRenderMode == RenderMode.Lightweight || this.ResolvedRenderMode == RenderMode.Mobile)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000A527C File Offset: 0x000A347C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			BaseClass.RenderVersionStamp(writer);
			if (this.EnableGlowEffect && this.ResolvedRenderMode == RenderMode.Classic)
			{
				this.RenderGlowEffectFrame(writer);
			}
			if (this.ShowCloseButton)
			{
				this.RenderCloseButton(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbWrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.DescriptionPosition == LightBoxDescriptionPosition.Bottom || this.DescriptionPosition == LightBoxDescriptionPosition.OverlayBottom)
			{
				this.RenderItemBox(writer);
				this.RenderDescriptionBox(writer);
			}
			else if (this.DescriptionPosition == LightBoxDescriptionPosition.Top || this.DescriptionPosition == LightBoxDescriptionPosition.OverlayTop)
			{
				this.RenderDescriptionBox(writer);
				this.RenderItemBox(writer);
			}
			writer.RenderEndTag();
			if (this.ShowLoadingPanel)
			{
				this.LoadingPanel.RenderControl(writer);
			}
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000A5326 File Offset: 0x000A3526
		private void RenderGlowEffectFrame(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbGlowEffect");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000A5343 File Offset: 0x000A3543
		private void RenderToolbar(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbToolbar");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderMaximizeButton(writer);
			this.RenderRestoreButton(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000A5370 File Offset: 0x000A3570
		private void RenderDescriptionBox(HtmlTextWriter writer)
		{
			string text = "rltbDescriptionBox";
			switch (this.DescriptionPosition)
			{
			case LightBoxDescriptionPosition.Bottom:
				text += " rltbDescBottom";
				break;
			case LightBoxDescriptionPosition.Top:
				text += " rltbDescTop";
				break;
			case LightBoxDescriptionPosition.OverlayTop:
				text += " rltbDescOverlayTop";
				break;
			case LightBoxDescriptionPosition.OverlayBottom:
				text += " rltbDescOverlayBottom";
				break;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.DescriptionPosition == LightBoxDescriptionPosition.Top || this.DescriptionPosition == LightBoxDescriptionPosition.OverlayTop)
			{
				this.RenderToolbar(writer);
			}
			this.LightBoxTitleContainer.RenderControl(writer);
			this.LightBoxDescriptionContainer.RenderControl(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbPager");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.GetItemsCounterString());
			writer.RenderEndTag();
			if (this.DescriptionPosition == LightBoxDescriptionPosition.Bottom || this.DescriptionPosition == LightBoxDescriptionPosition.OverlayBottom)
			{
				this.RenderToolbar(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000A5460 File Offset: 0x000A3660
		private void RenderMaximizeButton(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbActionButton rltbMaximizeButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Localization.MaximizeButtonText);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbIcon rltbMaximizeIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("Maximize image");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000A54EC File Offset: 0x000A36EC
		private void RenderRestoreButton(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbActionButton rltbRestoreButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Localization.RestoreButtonText);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbIcon rltbRestoreIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("Restore image");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000A5578 File Offset: 0x000A3778
		private void RenderCloseButton(HtmlTextWriter writer)
		{
			if (!this.EnableGlowEffect)
			{
				writer.AddStyleAttribute("background", "none");
			}
			if (this.ResolvedRenderMode != RenderMode.Mobile)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbClose");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Localization.CloseButtonText);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbActionButton rltbCloseButton");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbIcon rltbCloseIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rltbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.Localization.CloseButtonText);
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (this.ResolvedRenderMode != RenderMode.Mobile)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000A564C File Offset: 0x000A384C
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.RenderDesignTimeHtml(writer);
				return;
			}
			base.Render(writer);
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000A5665 File Offset: 0x000A3865
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			writer.Write(string.Format("<div style='width: 300px; height: 220px; position:relative; margin-top:10px; display:block' class='RadLightBox RadLightBox_{0}'><div class='rltbClose'><button class='rltbActionButton rltbCloseButton' type='button'><span class='rltbIcon rltbCloseIcon'></span><span class='rltbButtonText'>Close</span></button></div><div class='rltbWrapper'><div class='rltbItemBox'><div style='width:280px;height:120px;background-color:#eee'></div></div><div class='rltbDescriptionBox'><h4>Title text</h4><div class=rltbDescription'>Description text</div><span class='rltbPager'>Image 1 of 1</span></div></div><div class='rltbGlowEffect'></div></div>", this.Skin));
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000A5689 File Offset: 0x000A3889
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000A5690 File Offset: 0x000A3890
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (!this.AppendDataBoundItems && this.DataSource != null)
			{
				this.Items.Clear();
			}
			if (data != null)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = null;
				foreach (object obj in data)
				{
					if (propertyDescriptorCollection == null)
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
					}
					if (propertyDescriptorCollection.Count == 1 && propertyDescriptorCollection.Find("Length", false) != null)
					{
						if (!(obj is string))
						{
							throw new ArgumentException("There are no bindable properties suitable for RadLightBox control");
						}
						RadLightBoxItem radLightBoxItem = new RadLightBoxItem();
						radLightBoxItem.ImageUrl = obj.ToString();
						this.Items.Add(radLightBoxItem);
					}
					else
					{
						this.Items.Add(this.BindLightBoxItem(obj, propertyDescriptorCollection));
					}
				}
			}
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000A5768 File Offset: 0x000A3968
		private RadLightBoxItem BindLightBoxItem(object dataItem, PropertyDescriptorCollection props)
		{
			RadLightBoxItem radLightBoxItem = new RadLightBoxItem();
			string format = string.Empty;
			if (!string.IsNullOrEmpty(this.DataTitleField))
			{
				radLightBoxItem.Title = this.ResolveLightBoxItemField(this.DataTitleField, dataItem, props).ToString();
			}
			if (!string.IsNullOrEmpty(this.DataDescriptionField))
			{
				radLightBoxItem.Description = this.ResolveLightBoxItemField(this.DataDescriptionField, dataItem, props).ToString();
			}
			if (!string.IsNullOrEmpty(this.DataImageUrlField))
			{
				format = (string.IsNullOrEmpty(this.DataImageUrlFormatString) ? "{0}" : this.DataImageUrlFormatString);
				radLightBoxItem.ImageUrl = string.Format(format, this.ResolveLightBoxItemField(this.DataImageUrlField, dataItem, props).ToString());
			}
			if (!string.IsNullOrEmpty(this.DataNavigateUrlField))
			{
				radLightBoxItem.NavigateUrl = this.ResolveLightBoxItemField(this.DataNavigateUrlField, dataItem, props).ToString();
			}
			return radLightBoxItem;
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000A583C File Offset: 0x000A3A3C
		private object ResolveLightBoxItemField(string field, object dataItem, PropertyDescriptorCollection props)
		{
			if (string.IsNullOrEmpty(field))
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = props.Find(field, false);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(string.Format("A field with name '{0}' specified as a data field was not found in the datasource", field));
			}
			return DataBinder.GetPropertyValue(dataItem, propertyDescriptor.Name);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000A587C File Offset: 0x000A3A7C
		private void HandleFireCommand(string commandName, string commandArgument)
		{
			if (string.IsNullOrEmpty(commandArgument))
			{
				return;
			}
			int num;
			if (int.TryParse(commandArgument, out num))
			{
				LightBoxCommandEventArgs lightBoxCommandEventArgs = new LightBoxCommandEventArgs(num, this, commandName, commandArgument);
				this.OnCommand(lightBoxCommandEventArgs);
				if (!lightBoxCommandEventArgs.Canceled && commandName != null)
				{
					if (!(commandName == "NavigateTo"))
					{
						return;
					}
					this.CurrentItemIndex = num;
					this.VisibleOnLoad = true;
					this.CreateChildControls();
				}
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000A58DC File Offset: 0x000A3ADC
		// (set) Token: 0x0600324D RID: 12877 RVA: 0x000A58E4 File Offset: 0x000A3AE4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public PlaceHolder LightBoxItemContainer { get; set; }

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x000A58ED File Offset: 0x000A3AED
		// (set) Token: 0x0600324F RID: 12879 RVA: 0x000A58F5 File Offset: 0x000A3AF5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public PlaceHolder LightBoxTitleContainer { get; set; }

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x000A58FE File Offset: 0x000A3AFE
		// (set) Token: 0x06003251 RID: 12881 RVA: 0x000A5906 File Offset: 0x000A3B06
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public PlaceHolder LightBoxDescriptionContainer { get; set; }

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000A590F File Offset: 0x000A3B0F
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Misc")]
		public LightBoxClientSettings ClientSettings
		{
			get
			{
				if (this.clientSettings == null)
				{
					this.clientSettings = new LightBoxClientSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.clientSettings).TrackViewState();
					}
				}
				return this.clientSettings;
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000A593E File Offset: 0x000A3B3E
		// (set) Token: 0x06003254 RID: 12884 RVA: 0x000A596D File Offset: 0x000A3B6D
		[ClientPropertyName("_zIndex")]
		[Description("Gets or sets the z-index of the RadLightBox popup")]
		[DefaultValue(3500)]
		[Category("Appearance")]
		[ClientControlProperty]
		public int ZIndex
		{
			get
			{
				if (this.ViewState["ZIndex"] == null)
				{
					return 3500;
				}
				return (int)this.ViewState["ZIndex"];
			}
			set
			{
				this.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000A5988 File Offset: 0x000A3B88
		// (set) Token: 0x06003256 RID: 12886 RVA: 0x000A59B1 File Offset: 0x000A3BB1
		[Browsable(true)]
		[Description("Determines whether the RadLightBox will display loading panel")]
		[DefaultValue(true)]
		[Bindable(false)]
		[Category("Appearance")]
		public bool ShowLoadingPanel
		{
			get
			{
				object obj = this.ViewState["ShowLoadingPanel"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowLoadingPanel"] = value;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000A59CC File Offset: 0x000A3BCC
		// (set) Token: 0x06003258 RID: 12888 RVA: 0x000A59F5 File Offset: 0x000A3BF5
		[DefaultValue(false)]
		[Description("Determines if the control will have WAI-ARIA support enabled")]
		[Category("Behavior")]
		public bool EnableAriaSupport
		{
			get
			{
				object obj = this.ViewState["EnableAriaSupport"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000A5A10 File Offset: 0x000A3C10
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x000A5A39 File Offset: 0x000A3C39
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool EnableGlowEffect
		{
			get
			{
				object obj = this.ViewState["EnableGlowEffect"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableGlowEffect"] = value;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000A5A54 File Offset: 0x000A3C54
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x000A5A7E File Offset: 0x000A3C7E
		[NotifyParentProperty(true)]
		[DefaultValue(101)]
		public override short TabIndex
		{
			get
			{
				object obj = this.ViewState["TabIndex"];
				if (obj != null)
				{
					return (short)obj;
				}
				return 101;
			}
			set
			{
				this.ViewState["TabIndex"] = value;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000A5A98 File Offset: 0x000A3C98
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x000A5ACB File Offset: 0x000A3CCB
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Determines whether the RadLightBox is modal")]
		[Category("Appearance")]
		[Browsable(true)]
		[Bindable(true)]
		public bool Modal
		{
			get
			{
				object obj = this.ViewState["Modal"];
				if (obj != null)
				{
					return (bool)obj;
				}
				return this.EnableAriaSupport;
			}
			set
			{
				this.ViewState["Modal"] = value;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000A5AE3 File Offset: 0x000A3CE3
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Category("Layout")]
		[DefaultValue(null)]
		[Description("RadLightBox Items Collection")]
		[NotifyParentProperty(true)]
		public RadLightBoxItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new RadLightBoxItemCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.items).TrackViewState();
					}
				}
				return this.items;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000A5B14 File Offset: 0x000A3D14
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x000A5B41 File Offset: 0x000A3D41
		[DefaultValue("")]
		public string DataImageUrlFormatString
		{
			get
			{
				object obj = this.ViewState["DataImageUrlFormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["DataImageUrlFormatString"] = value;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000A5B54 File Offset: 0x000A3D54
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x000A5B7D File Offset: 0x000A3D7D
		[DefaultValue(false)]
		[Description("Gets/sets a value that indicates whether list items are cleared before databinding")]
		[NotifyParentProperty(true)]
		public bool AppendDataBoundItems
		{
			get
			{
				object obj = this.ViewState["AppendDataBoundItems"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000A5B98 File Offset: 0x000A3D98
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x000A5BC6 File Offset: 0x000A3DC6
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Description("Gets/sets the current item index")]
		public int CurrentItemIndex
		{
			get
			{
				object obj = this.ControlState["CurrentItemIndex"];
				if (obj == null)
				{
					obj = 0;
				}
				return (int)obj;
			}
			set
			{
				this.ControlState["CurrentItemIndex"] = value;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000A5BE0 File Offset: 0x000A3DE0
		// (set) Token: 0x06003267 RID: 12903 RVA: 0x000A5C09 File Offset: 0x000A3E09
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("If enabled, this will loop the items when the last/first one is reached")]
		public bool LoopItems
		{
			get
			{
				object obj = this.ViewState["LoopItems"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["LoopItems"] = value;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x000A5C21 File Offset: 0x000A3E21
		// (set) Token: 0x06003269 RID: 12905 RVA: 0x000A5C56 File Offset: 0x000A3E56
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Returns the items count")]
		public int ItemsCount
		{
			get
			{
				if (this.ControlState["ItemsCount"] == null)
				{
					return this.Items.Count;
				}
				return (int)this.ControlState["ItemsCount"];
			}
			internal set
			{
				this.ControlState["ItemsCount"] = value;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x000A5C70 File Offset: 0x000A3E70
		// (set) Token: 0x0600326B RID: 12907 RVA: 0x000A5C99 File Offset: 0x000A3E99
		public bool PreserveCurrentItemTemplates
		{
			get
			{
				object obj = this.ViewState["PreserveCurrentItemTemplates"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["PreserveCurrentItemTemplates"] = value;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x000A5CB4 File Offset: 0x000A3EB4
		// (set) Token: 0x0600326D RID: 12909 RVA: 0x000A5CE7 File Offset: 0x000A3EE7
		[NotifyParentProperty(true)]
		[Description("Gets/sets the items counter format string. The first parameter is the item index and the second is the total number of items. The second parameter is optional.")]
		[DefaultValue("Image {0} of {1}")]
		public string ItemsCounterFormatString
		{
			get
			{
				object obj = this.ViewState["ItemsCounterFormatString"];
				if (obj == null)
				{
					return this.Localization.PagerFormatString;
				}
				return obj.ToString();
			}
			set
			{
				this.ViewState["ItemsCounterFormatString"] = value;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000A5CFC File Offset: 0x000A3EFC
		// (set) Token: 0x0600326F RID: 12911 RVA: 0x000A5D25 File Offset: 0x000A3F25
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Determines whether the Close button will be visible")]
		public bool ShowCloseButton
		{
			get
			{
				object obj = this.ViewState["ShowCloseButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowCloseButton"] = value;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x000A5D40 File Offset: 0x000A3F40
		// (set) Token: 0x06003271 RID: 12913 RVA: 0x000A5D69 File Offset: 0x000A3F69
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Determines whether the Maximize button will be visible")]
		public bool ShowMaximizeButton
		{
			get
			{
				object obj = this.ViewState["ShowMaximizeButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowMaximizeButton"] = value;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x000A5D84 File Offset: 0x000A3F84
		// (set) Token: 0x06003273 RID: 12915 RVA: 0x000A5DAD File Offset: 0x000A3FAD
		[NotifyParentProperty(true)]
		[Description("Determines whether the Restore button will be visible")]
		[DefaultValue(true)]
		public bool ShowRestoreButton
		{
			get
			{
				object obj = this.ViewState["ShowRestoreButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowRestoreButton"] = value;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000A5DC8 File Offset: 0x000A3FC8
		// (set) Token: 0x06003275 RID: 12917 RVA: 0x000A5DFC File Offset: 0x000A3FFC
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Determines whether the Prev button will be visible")]
		public bool ShowPrevButton
		{
			get
			{
				object obj = this.ViewState["ShowPrevButton"];
				if (obj == null)
				{
					return this.ResolvedRenderMode != RenderMode.Mobile;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["ShowPrevButton"] = value;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x000A5E14 File Offset: 0x000A4014
		// (set) Token: 0x06003277 RID: 12919 RVA: 0x000A5E48 File Offset: 0x000A4048
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Determines whether the Next button will be visible")]
		public bool ShowNextButton
		{
			get
			{
				object obj = this.ViewState["ShowNextButton"];
				if (obj == null)
				{
					return this.ResolvedRenderMode != RenderMode.Mobile;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["ShowNextButton"] = value;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000A5E60 File Offset: 0x000A4060
		// (set) Token: 0x06003279 RID: 12921 RVA: 0x000A5E89 File Offset: 0x000A4089
		[NotifyParentProperty(true)]
		[DefaultValue(LightBoxDescriptionPosition.Bottom)]
		public LightBoxDescriptionPosition DescriptionPosition
		{
			get
			{
				object obj = this.ViewState["DescriptionPosition"];
				if (obj == null)
				{
					return LightBoxDescriptionPosition.Bottom;
				}
				return (LightBoxDescriptionPosition)obj;
			}
			set
			{
				this.ViewState["DescriptionPosition"] = value;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000A5EA1 File Offset: 0x000A40A1
		// (set) Token: 0x0600327B RID: 12923 RVA: 0x000A5EC1 File Offset: 0x000A40C1
		[Description("The field in the data source which provides the title text.")]
		[Category("Data")]
		[DefaultValue("")]
		public string DataTitleField
		{
			get
			{
				return ((string)this.ViewState["DataTitleField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataTitleField"] = value;
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x000A5ED4 File Offset: 0x000A40D4
		// (set) Token: 0x0600327D RID: 12925 RVA: 0x000A5EF4 File Offset: 0x000A40F4
		[DefaultValue("")]
		[Category("Data")]
		[Description("The field in the data source which provides the description text.")]
		public string DataDescriptionField
		{
			get
			{
				return ((string)this.ViewState["DataDescriptionField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataDescriptionField"] = value;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000A5F07 File Offset: 0x000A4107
		// (set) Token: 0x0600327F RID: 12927 RVA: 0x000A5F27 File Offset: 0x000A4127
		[DefaultValue("")]
		[Category("Data")]
		[Description("The field in the data source which provides the ImageUrl.")]
		public string DataImageUrlField
		{
			get
			{
				return ((string)this.ViewState["DataImageUrlField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataImageUrlField"] = value;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x000A5F3A File Offset: 0x000A413A
		// (set) Token: 0x06003281 RID: 12929 RVA: 0x000A5F5A File Offset: 0x000A415A
		[Category("Data")]
		[DefaultValue("")]
		[Description("The field in the data source which provides the NavigateUrl.")]
		public string DataNavigateUrlField
		{
			get
			{
				return ((string)this.ViewState["DataNavigateUrlField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataNavigateUrlField"] = value;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000A5F6D File Offset: 0x000A416D
		// (set) Token: 0x06003283 RID: 12931 RVA: 0x000A5F8D File Offset: 0x000A418D
		[DefaultValue("")]
		[Category("Data")]
		[Description("The field in the data source which provides the target control's ID.")]
		public string DataTargetControlIDField
		{
			get
			{
				return ((string)this.ViewState["DataTargetControlIDField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataTargetControlIDField"] = value;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x000A5FA0 File Offset: 0x000A41A0
		// (set) Token: 0x06003285 RID: 12933 RVA: 0x000A5FC0 File Offset: 0x000A41C0
		[DefaultValue("")]
		[Category("Misc")]
		[Description("Gets or sets a value indicating where RadLightBox will look for its .resx localization files.")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06003286 RID: 12934 RVA: 0x000A6013 File Offset: 0x000A4213
		// (set) Token: 0x06003287 RID: 12935 RVA: 0x000A6033 File Offset: 0x000A4233
		[Category("Appearance")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				if (value != this.ViewState["Culture"])
				{
					this._localization = null;
				}
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000A6060 File Offset: 0x000A4260
		public void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Contains("FireCommand:"))
			{
				this.HandleFireCommand(RadLightBox.parseFireCommandEventName(eventArgument), RadLightBox.parseFireCommandArgs(eventArgument));
			}
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000A608C File Offset: 0x000A428C
		protected virtual void OnCommand(LightBoxCommandEventArgs e)
		{
			EventHandler<LightBoxCommandEventArgs> eventHandler = base.Events[RadLightBox.EventCommand] as EventHandler<LightBoxCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x0600328A RID: 12938 RVA: 0x000A60BA File Offset: 0x000A42BA
		// (remove) Token: 0x0600328B RID: 12939 RVA: 0x000A60CD File Offset: 0x000A42CD
		[Description("RadLightBox Command event")]
		[Category("Action")]
		public event EventHandler<LightBoxCommandEventArgs> Command
		{
			add
			{
				base.Events.AddHandler(RadLightBox.EventCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadLightBox.EventCommand, value);
			}
		}

		// Token: 0x04000DBB RID: 3515
		public const string NavigateToCommandName = "NavigateTo";

		// Token: 0x04000DBC RID: 3516
		private RadLightBoxItemCollection items;

		// Token: 0x04000DBD RID: 3517
		private LightBoxControlStateManager controlStateManager;

		// Token: 0x04000DBE RID: 3518
		private LightBoxClientSettings clientSettings;

		// Token: 0x04000DBF RID: 3519
		private HtmlGenericControl lightBoxTitleControl;

		// Token: 0x04000DC0 RID: 3520
		private HtmlGenericControl lightBoxDescriptionControl;

		// Token: 0x04000DC1 RID: 3521
		private System.Web.UI.WebControls.Image lightBoxImageControl;

		// Token: 0x04000DC2 RID: 3522
		private Button prevButton;

		// Token: 0x04000DC3 RID: 3523
		private Button nextButton;

		// Token: 0x04000DC4 RID: 3524
		private RadAjaxLoadingPanel loadingPanel;

		// Token: 0x04000DC5 RID: 3525
		private static readonly string emptyImageData = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs%3D";

		// Token: 0x04000DC6 RID: 3526
		private static readonly object EventCommand = new object();

		// Token: 0x04000DC7 RID: 3527
		private static TFunc<string, string> parseFireCommandEventName = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[0];
		};

		// Token: 0x04000DC8 RID: 3528
		private static TFunc<string, string> parseFireCommandArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[2];
		};

		// Token: 0x04000DC9 RID: 3529
		private LightBoxStrings _localization;
	}
}
