using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02000557 RID: 1367
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadImageGallery))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadGrid))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadImageGallery))]
	[EmbeddedSkin("ImageGallery")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadImageGallery))]
	[RequiredScript(typeof(KeyboardNavigation))]
	[RequiredScript(typeof(GestureFramework))]
	[RequiredScript(typeof(PinchZoomUtility))]
	[RequiredScript(typeof(MaterialRipple))]
	[ToolboxBitmap(typeof(RadImageGallery), "Telerik.Web.UI.ImageGallery.png")]
	[LightweightRendering]
	[EmbeddedSkin("ImageGallery", "Default")]
	[Designer("Telerik.Web.Design.RadImageGalleryDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadImageGallery runat=server></{0}:RadImageGallery>")]
	[AdaptiveRendering]
	[ClientScriptResource("Telerik.Web.UI.RadImageGallery", "Telerik.Web.UI.ImageGallery.RadImageGalleryScripts.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	[System.EnterpriseServices.Description("Telerik RadImageGallery")]
	[RequiredScript(typeof(ImageAnimations), 1)]
	public class RadImageGallery : RadCompositeDataBoundControl, INamingContainer, IPostBackEventHandler, ILocalizableControl, ICallbackEventHandler
	{
		// Token: 0x06003076 RID: 12406 RVA: 0x0009EDF8 File Offset: 0x0009CFF8
		internal static HtmlGenericControl CreateButton(string name, string text)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("button");
			htmlGenericControl.Attributes.Add("title", text);
			htmlGenericControl.Attributes.Add("class", string.Format("rigActionButton rig{0}Button", name));
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			htmlGenericControl2.Attributes.Add("class", "rigIcon rig" + name + "Icon");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			return htmlGenericControl;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0009EE74 File Offset: 0x0009D074
		internal static HtmlInputControl CreatePostbackButton(string name, string text, bool display = true)
		{
			HtmlInputControl htmlInputControl = new HtmlInputSubmit();
			htmlInputControl.Attributes.Add("class", string.Format("rigActionButton rig{0}Button", name));
			htmlInputControl.Attributes.Add("title", text);
			htmlInputControl.Value = text;
			if (!display)
			{
				htmlInputControl.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			return htmlInputControl;
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x0009EED0 File Offset: 0x0009D0D0
		// (set) Token: 0x06003079 RID: 12409 RVA: 0x0009EED8 File Offset: 0x0009D0D8
		private bool ShouldFocus { get; set; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x0009EEE1 File Offset: 0x0009D0E1
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x0009EEE5 File Offset: 0x0009D0E5
		protected override string CssClassFormatString
		{
			get
			{
				return "RadImageGallery RadImageGallery_{0}";
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x0009EEEC File Offset: 0x0009D0EC
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x0009EEF4 File Offset: 0x0009D0F4
		public override bool EnableEmbeddedScripts
		{
			get
			{
				return base.EnableEmbeddedScripts;
			}
			set
			{
				base.EnableEmbeddedScripts = value;
				this.LightBox.EnableEmbeddedScripts = value;
				this.RadToolTip.EnableEmbeddedScripts = value;
				this.ThumbnailListView.EnableEmbeddedScripts = value;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x0009EF21 File Offset: 0x0009D121
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x0009EF29 File Offset: 0x0009D129
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return base.EnableEmbeddedSkins;
			}
			set
			{
				base.EnableEmbeddedSkins = value;
				this.LightBox.EnableEmbeddedSkins = value;
				this.RadToolTip.EnableEmbeddedSkins = value;
				this.ThumbnailListView.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x0009EF56 File Offset: 0x0009D156
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x0009EF5E File Offset: 0x0009D15E
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return base.EnableEmbeddedBaseStylesheet;
			}
			set
			{
				base.EnableEmbeddedBaseStylesheet = value;
				this.LightBox.EnableEmbeddedBaseStylesheet = value;
				this.RadToolTip.EnableEmbeddedBaseStylesheet = value;
				this.ThumbnailListView.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x0009EF8B File Offset: 0x0009D18B
		// (set) Token: 0x06003083 RID: 12419 RVA: 0x0009EF94 File Offset: 0x0009D194
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				string skin = (value == "Sitefinity") ? "Default" : value;
				base.Skin = skin;
				this.LightBox.Skin = value;
				this.RadToolTip.Skin = value;
				this.ThumbnailListView.Skin = value;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x0009EFE2 File Offset: 0x0009D1E2
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x0009EFEA File Offset: 0x0009D1EA
		internal bool IsDataBinding { get; set; }

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x0009EFF3 File Offset: 0x0009D1F3
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x0009EFFB File Offset: 0x0009D1FB
		internal int ActiveItemIndex
		{
			get
			{
				return this.activeItemIndex;
			}
			set
			{
				this.activeItemIndex = value;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x0009F004 File Offset: 0x0009D204
		internal ImageGalleryThumbnailsArea ThumbnailsArea
		{
			get
			{
				if (this.thumbnailsArea == null)
				{
					this.thumbnailsArea = new ImageGalleryThumbnailsArea(this);
					this.thumbnailsArea.ID = "ThumbnailsArea";
				}
				return this.thumbnailsArea;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x0009F030 File Offset: 0x0009D230
		internal ImageGalleryImageArea ImageArea
		{
			get
			{
				if (this.imageArea == null)
				{
					this.imageArea = new ImageGalleryImageArea(this);
					this.imageArea.ID = "ImageArea";
					this.imageArea.CssClass = "rigItemBox";
				}
				return this.imageArea;
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x0009F06C File Offset: 0x0009D26C
		internal ImageGalleryStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new ImageGalleryStrings(new LocalizationProvider("RadImageGallery.Main", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x0009F0AC File Offset: 0x0009D2AC
		internal int PageCount
		{
			get
			{
				int dataSourceCount = this.ThumbnailListView.DataSourceCount;
				if (dataSourceCount != 0)
				{
					return (dataSourceCount + this.PageSize - 1) / this.PageSize;
				}
				return 1;
			}
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x0009F0DC File Offset: 0x0009D2DC
		internal int GetPagersHeight()
		{
			int num = 46;
			int num2 = 0;
			if (this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left)
			{
				using (IEnumerator enumerator = this.ImageArea.Controls.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Control control = (Control)obj;
						ImageGalleryPagerItem imageGalleryPagerItem = control as ImageGalleryPagerItem;
						if (imageGalleryPagerItem != null && imageGalleryPagerItem.Visible)
						{
							num2 += num;
						}
					}
					return num2;
				}
			}
			foreach (object obj2 in this.Controls)
			{
				Control control2 = (Control)obj2;
				ImageGalleryPagerItem imageGalleryPagerItem2 = control2 as ImageGalleryPagerItem;
				if (imageGalleryPagerItem2 != null && imageGalleryPagerItem2.Visible)
				{
					num2 += num;
				}
			}
			return num2;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0009F1D8 File Offset: 0x0009D3D8
		internal void CreateProgressBarWrapper(Control parentControl)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "rigProgressBar");
			htmlGenericControl.Style.Add(HtmlTextWriterStyle.Display, "none");
			HtmlGenericControl child = new HtmlGenericControl("div");
			htmlGenericControl.Controls.Add(child);
			parentControl.Controls.Add(htmlGenericControl);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x0009F23C File Offset: 0x0009D43C
		internal void CreateItemsCounter(Control parentControl)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			htmlGenericControl.InnerText = string.Format(this.ToolbarSettings.ItemsCounterFormat, this.CurrentItemIndex + 1, this.Items.Count);
			htmlGenericControl.Attributes.Add("class", "rigItemsCount");
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				htmlGenericControl.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			parentControl.Controls.Add(htmlGenericControl);
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0009F2C4 File Offset: 0x0009D4C4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image && this.Height.IsEmpty && ((this.ThumbnailsAreaSettings.Height.Type == UnitType.Pixel && this.ImageAreaSettings.IsHeightSet && this.ImageAreaSettings.Height.Type == UnitType.Pixel) || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right))
			{
				double num = this.ImageAreaSettings.Height.Value + (double)this.GetPagersHeight();
				if (this.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.Thumbnails && (this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Top || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Bottom))
				{
					num += this.ThumbnailsAreaSettings.Height.Value;
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, Unit.Pixel((int)num).ToString());
			}
			if (this.DisplayAreaMode == ImageGalleryDisplayAreaMode.ToolTip)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "visible");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x0009F3E0 File Offset: 0x0009D5E0
		protected internal override RenderMode PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			RenderMode renderMode = base.PreferredRenderMode(browser);
			if (renderMode == RenderMode.Lightweight || renderMode == RenderMode.Native)
			{
				return RenderMode.Classic;
			}
			return renderMode;
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x0009F400 File Offset: 0x0009D600
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0009F403 File Offset: 0x0009D603
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.CreateControlHierarchy(dataBinding);
			return 1;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0009F40D File Offset: 0x0009D60D
		protected override void CreateChildControls()
		{
			this.CreateControlHierarchy(false);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0009F418 File Offset: 0x0009D618
		private void CreateControlHierarchy(bool dataBinding)
		{
			if (dataBinding)
			{
				this.Controls.Clear();
				this.imageArea = null;
				this.thumbnailsArea = null;
			}
			if (this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Top || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left)
			{
				this.Controls.Add(this.ThumbnailsArea);
			}
			if (this.ToolbarSettings.Position == ImageGalleryToolbarPosition.Top && (this.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image || this.DisplayAreaMode == ImageGalleryDisplayAreaMode.Thumbnails))
			{
				this.Controls.Add(new ImageGalleryToolbar(this));
			}
			this.Controls.Add(this.ImageArea);
			switch (this.DisplayAreaMode)
			{
			case ImageGalleryDisplayAreaMode.LightBox:
				this.Controls.Add(this.LightBox);
				break;
			case ImageGalleryDisplayAreaMode.ToolTip:
				this.Controls.Add(this.RadToolTip);
				break;
			}
			if (this.ToolbarSettings.Position == ImageGalleryToolbarPosition.Bottom && (this.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image || this.DisplayAreaMode == ImageGalleryDisplayAreaMode.Thumbnails))
			{
				this.Controls.Add(new ImageGalleryToolbar(this));
			}
			if (this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Bottom || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right)
			{
				this.Controls.Add(this.ThumbnailsArea);
			}
			this.CreatePager();
			if (this.ShowLoadingPanel)
			{
				this.Controls.Add(this.LoadingPanel);
			}
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0009F568 File Offset: 0x0009D768
		private void CreatePager()
		{
			if (this.AllowPaging)
			{
				if (this.PagerStyle.Position == ImageGalleryPagerPosition.Top)
				{
					if (this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left)
					{
						this.ImageArea.Controls.AddAt(0, new ImageGalleryPagerItem(this));
					}
					else
					{
						this.Controls.AddAt(0, new ImageGalleryPagerItem(this));
					}
				}
				if (this.PagerStyle.Position == ImageGalleryPagerPosition.Bottom)
				{
					if (this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left)
					{
						this.ImageArea.Controls.Add(new ImageGalleryPagerItem(this));
					}
					else
					{
						this.Controls.Add(new ImageGalleryPagerItem(this));
					}
				}
				if (this.PagerStyle.Position == ImageGalleryPagerPosition.TopAndBottom)
				{
					if (this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left)
					{
						this.ImageArea.Controls.AddAt(0, new ImageGalleryPagerItem(this));
						this.ImageArea.Controls.Add(new ImageGalleryPagerItem(this));
						return;
					}
					this.Controls.AddAt(0, new ImageGalleryPagerItem(this));
					this.Controls.Add(new ImageGalleryPagerItem(this));
				}
			}
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0009F6A4 File Offset: 0x0009D8A4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.RenderDesignTimeHtml(writer);
				return;
			}
			this.ImageArea.PopulateItem(this.CurrentItemIndex);
			if (this.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image && this.Items.Count > 0 && this.ThumbnailsAreaSettings.Mode != ImageGalleryThumbnailsAreaMode.ImageSlider)
			{
				HtmlGenericControl htmlGenericControl = this.ThumbnailListView.Items[this.CurrentItemIndex].Controls[0] as HtmlGenericControl;
				htmlGenericControl.Attributes.Add("class", "rigThumbnailActive");
				HtmlGenericControl htmlGenericControl2 = htmlGenericControl.Controls[0] as HtmlGenericControl;
				htmlGenericControl2.Style.Add(HtmlTextWriterStyle.Width, this.ThumbnailsAreaSettings.ThumbnailWidth.ToString());
				htmlGenericControl2.Style.Add(HtmlTextWriterStyle.Height, this.ThumbnailsAreaSettings.ThumbnailHeight.ToString());
			}
			base.RenderContents(writer);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0009F7A1 File Offset: 0x0009D9A1
		public void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.IndexOf("FireCommand:") != -1)
			{
				this.HandleFireCommand(RadImageGallery.parseFireCommandEventName(eventArgument), RadImageGallery.parseFireCommandArgs(eventArgument));
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0009F7D0 File Offset: 0x0009D9D0
		private void HandleFireCommand(string commandName, string commandArgument)
		{
			ImageGalleryCommandEventArgs imageGalleryCommandEventArgs = ImageGalleryCommandEventArgsFactory.CreateCommandEventArgs(commandName, commandArgument);
			imageGalleryCommandEventArgs.ExecuteCommand(this);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x0009F7EC File Offset: 0x0009D9EC
		public void RaiseCallbackEvent(string eventArgument)
		{
			this.callbackArguments = eventArgument.Split(new string[]
			{
				"$$"
			}, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0009F818 File Offset: 0x0009DA18
		public string GetCallbackResult()
		{
			string a;
			if ((a = this.callbackArguments[0]) != null && a == "getImageUrl")
			{
				return this.GetImageUrl(int.Parse(this.callbackArguments[1]));
			}
			return string.Empty;
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0009F858 File Offset: 0x0009DA58
		internal string GetImageUrl(int itemIndex)
		{
			if (this.AllowPaging && this.DataSource == this.Items)
			{
				itemIndex += this.CurrentPageIndex * this.PageSize;
			}
			if (itemIndex >= this.Items.Count)
			{
				return string.Empty;
			}
			ImageGalleryItem imageGalleryItem = this.Items[itemIndex] as ImageGalleryItem;
			if (!string.IsNullOrEmpty(imageGalleryItem.ImageUrl))
			{
				return imageGalleryItem.ImageUrl;
			}
			Hashtable hashtable = null;
			if (this.ThumbnailListView.DataKeyValues.Count > itemIndex)
			{
				hashtable = this.ThumbnailListView.DataKeyValues[itemIndex];
			}
			ImageGalleryImageRequestedEventArgs imageGalleryImageRequestedEventArgs = new ImageGalleryImageRequestedEventArgs(this.Items[itemIndex], hashtable);
			this.CallOnImageRequested(imageGalleryImageRequestedEventArgs);
			if (imageGalleryImageRequestedEventArgs.ImageData != null)
			{
				RadBinaryImage radBinaryImage = new RadBinaryImage();
				radBinaryImage.DataValue = imageGalleryImageRequestedEventArgs.ImageData;
				radBinaryImage.ProcessImageData();
				return radBinaryImage.ImageUrl;
			}
			if (!string.IsNullOrEmpty(imageGalleryImageRequestedEventArgs.ImageUrl))
			{
				return imageGalleryImageRequestedEventArgs.ImageUrl;
			}
			RadBinaryImage radBinaryImage2 = new RadBinaryImage();
			if (string.IsNullOrEmpty(this.ImagesFolderPath))
			{
				if (hashtable != null && hashtable.ContainsKey(this.DataImageField) && hashtable[this.DataImageField] != null && hashtable[this.DataImageField] is byte[])
				{
					radBinaryImage2.DataValue = (hashtable[this.DataImageField] as byte[]);
					goto IL_1B6;
				}
				object dataItem = this.ThumbnailListView.Items[itemIndex].DataItem;
				if (dataItem == null)
				{
					this.ThumbnailListView.Rebind();
					dataItem = this.ThumbnailListView.Items[itemIndex].DataItem;
				}
				try
				{
					radBinaryImage2.DataValue = (DataBinder.Eval(dataItem, this.DataImageField) as byte[]);
					goto IL_1B6;
				}
				catch
				{
					goto IL_1B6;
				}
			}
			radBinaryImage2.DataValue = this.GetImageData(this.FileNames[itemIndex]);
			IL_1B6:
			radBinaryImage2.ProcessImageData();
			return radBinaryImage2.ImageUrl;
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x0009FA3C File Offset: 0x0009DC3C
		private byte[] GetImageData(string path)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(path);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				image.Save(memoryStream, ImageFormat.Jpeg);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x0009FAA0 File Offset: 0x0009DCA0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
			this.RegisterClientSideEvents(delegate(string eventName, string eventValue)
			{
				RadCompositeDataBoundControl.DescribeEvent(descriptor, eventName, eventValue);
			});
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0009FAEC File Offset: 0x0009DCEC
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

		// Token: 0x0600309F RID: 12447 RVA: 0x0009FBD0 File Offset: 0x0009DDD0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new ImageGalleryItemConverter(),
				new ImageGalleryThumbnailsAreaSettingsConverter(),
				new ImageGalleryImageAreaSettingsConverter(),
				new ImageGalleryToolbarSettingsConverter(),
				new ImageGalleryAnimationSettingsConverter(),
				new ImageGalleryKeyboardNavigationSettingsConverter(),
				new KeyboardNavigationShortcutsJavaScriptConverter<ImageGalleryShortcut>()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			this.Page.ClientScript.GetCallbackEventReference(this, "arg", "Telerik.Web.UI.RadImageGallery.DummyCallbackReference", "");
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			if (this.DataSource == this.Items && this.AllowPaging)
			{
				List<ImageGalleryItemBase> list = new List<ImageGalleryItemBase>();
				foreach (object obj in this.Items)
				{
					ImageGalleryItemBase item = (ImageGalleryItemBase)obj;
					list.Add(item);
				}
				descriptor.AddScriptProperty("_itemsData", javaScriptSerializer.Serialize(list.Skip(this.CurrentPageIndex * this.PageSize).Take(this.PageSize)));
			}
			else
			{
				descriptor.AddScriptProperty("_itemsData", javaScriptSerializer.Serialize(this.Items));
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
			if (!this.ThumbnailsAreaSettings.IsDefault())
			{
				descriptor.AddScriptProperty("_thumbnailsSettings", javaScriptSerializer.Serialize(this.ThumbnailsAreaSettings));
			}
			if (!this.ImageAreaSettings.IsDefault())
			{
				descriptor.AddScriptProperty("_imageAreaSettings", javaScriptSerializer.Serialize(this.ImageAreaSettings));
			}
			descriptor.AddScriptProperty("_toolbarSettings", javaScriptSerializer.Serialize(this.ToolbarSettings));
			if (!this.ClientSettings.AnimationSettings.IsDefault())
			{
				descriptor.AddScriptProperty("_animationSettings", javaScriptSerializer.Serialize(this.ClientSettings.AnimationSettings));
			}
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			if (this.DisplayAreaMode != ImageGalleryDisplayAreaMode.Image)
			{
				descriptor.AddProperty("_displayAreaMode", this.DisplayAreaMode);
			}
			if (this.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image)
			{
				if (this.CurrentItemIndex != 0)
				{
					descriptor.AddProperty("_selectedIndex", this.CurrentItemIndex);
				}
			}
			else
			{
				descriptor.AddProperty("_selectedIndex", -1);
			}
			if (this.ActiveItemIndex != -1)
			{
				descriptor.AddProperty("_activeIndex", this.ActiveItemIndex);
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				descriptor.AddProperty("_titleField", this.DataTitleField);
				descriptor.AddProperty("_imageUrlField", this.DataImageField);
				descriptor.AddProperty("_descriptionField", this.DataDescriptionField);
				descriptor.AddProperty("_thumbnailUrlField", this.DataThumbnailField);
			}
			if (this.AllowPaging)
			{
				descriptor.AddProperty("_allowPaging", true);
				if (this.ThumbnailListView.DataSourceCount > 0)
				{
					descriptor.AddProperty("_pageCount", (this.ThumbnailListView.DataSourceCount + this.PageSize - 1) / this.PageSize);
				}
				if (this.CurrentPageIndex != 0)
				{
					descriptor.AddProperty("_currentPageIndex", this.CurrentPageIndex);
				}
				if (this.PageSize != 10)
				{
					descriptor.AddProperty("_pageSize", this.PageSize);
				}
			}
			if (this.LoopItems)
			{
				descriptor.AddProperty("_loopItems", true);
			}
			if (this.ClientSettings.AllowKeyboardNavigation)
			{
				descriptor.AddProperty("_allowKeyboardNavigation", true);
				descriptor.AddScriptProperty("_keyboardNavigationSettings", javaScriptSerializer.Serialize(this.ClientSettings.KeyboardNavigationSettings));
			}
			if (this.ShouldFocus)
			{
				descriptor.AddProperty("_shouldFocus", true);
			}
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000A0000 File Offset: 0x0009E200
		protected override bool LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.CurrentItemIndex = int.Parse(clientState["selectedIndex"].ToString());
			if (clientState.ContainsKey("shouldFocus") && (bool)clientState["shouldFocus"])
			{
				this.ShouldFocus = true;
			}
			return true;
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000A0058 File Offset: 0x0009E258
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			arrayList.Add(((IStateManager)this.Items).SaveViewState());
			arrayList.Add(((IStateManager)this.ThumbnailsAreaSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.ImageAreaSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.ToolbarSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.PagerStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.ClientSettings).SaveViewState());
			if (!this.UsesControlState)
			{
				this.SaveControlStateObject(arrayList);
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000A0108 File Offset: 0x0009E308
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int currentStateIndex = 0;
				base.LoadViewState(array[currentStateIndex++]);
				((IStateManager)this.Items).LoadViewState(array[currentStateIndex++]);
				((IStateManager)this.ThumbnailsAreaSettings).LoadViewState(array[currentStateIndex++]);
				((IStateManager)this.ImageAreaSettings).LoadViewState(array[currentStateIndex++]);
				((IStateManager)this.ToolbarSettings).LoadViewState(array[currentStateIndex++]);
				((IStateManager)this.PagerStyle).LoadViewState(array[currentStateIndex++]);
				((IStateManager)this.ClientSettings).LoadViewState(array[currentStateIndex++]);
				if (!this.UsesControlState)
				{
					this.LoadControlStateObject(array, currentStateIndex);
				}
			}
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000A01B0 File Offset: 0x0009E3B0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (base.IsTrackingViewState)
			{
				return;
			}
			((IStateManager)this.Items).TrackViewState();
			((IStateManager)this.ThumbnailsAreaSettings).TrackViewState();
			((IStateManager)this.ImageAreaSettings).TrackViewState();
			((IStateManager)this.ToolbarSettings).TrackViewState();
			((IStateManager)this.PagerStyle).TrackViewState();
			((IStateManager)this.ClientSettings).TrackViewState();
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000A020E File Offset: 0x0009E40E
		internal ControlStateManager ControlState
		{
			get
			{
				if (this.controlStateManager == null)
				{
					this.controlStateManager = new ControlStateManager();
				}
				return this.controlStateManager;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000A0229 File Offset: 0x0009E429
		private List<string> FileNames
		{
			get
			{
				if (this.ControlState["FileNames"] == null)
				{
					this.ControlState["FileNames"] = new List<string>();
				}
				return (List<string>)this.ControlState["FileNames"];
			}
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000A0280 File Offset: 0x0009E480
		protected override void OnInit(EventArgs e)
		{
			if (this.Page != null)
			{
				if (this.UsesControlState)
				{
					this.Page.RegisterRequiresControlState(this);
				}
				else
				{
					this.Page.InitComplete += delegate(object sender, EventArgs args)
					{
						if (this.UsesControlState)
						{
							this.Page.RegisterRequiresControlState(this);
						}
					};
				}
			}
			base.OnInit(e);
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000A02D0 File Offset: 0x0009E4D0
		private bool UsesControlState
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000A02DC File Offset: 0x0009E4DC
		protected virtual void SaveControlStateObject(IList state)
		{
			foreach (object obj in this.Items)
			{
				ImageGalleryItemBase imageGalleryItemBase = (ImageGalleryItemBase)obj;
				ImageGalleryItem imageGalleryItem = imageGalleryItemBase as ImageGalleryItem;
				this.FileNames.Add((imageGalleryItem == null) ? null : imageGalleryItem.FileName);
			}
			state.Add(((IStateManager)this.ControlState).SaveViewState());
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000A0360 File Offset: 0x0009E560
		protected virtual void LoadControlStateObject(object[] stateArray, int currentStateIndex)
		{
			((IStateManager)this.ControlState).LoadViewState(stateArray[currentStateIndex++]);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000A0378 File Offset: 0x0009E578
		protected override object SaveControlState()
		{
			object value = base.SaveControlState();
			ArrayList arrayList = new ArrayList();
			arrayList.Add(value);
			this.SaveControlStateObject(arrayList);
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000A03B4 File Offset: 0x0009E5B4
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

		// Token: 0x060030AC RID: 12460 RVA: 0x000A03E2 File Offset: 0x0009E5E2
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "static");
			}
			base.Render(writer);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000A0400 File Offset: 0x0009E600
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			writer.Write(string.Format("\r\n\t\t\t<div style='postion: relative;'>\r\n\t\t\t\t<div id='imageGallery_ContentArea' class='rigItemBox' style='height: 200px;'>\r\n\t\t\t\t\t<input type='submit' class='rigActionButton rigPrevButton' value='' title='' style='display: block;' />\r\n\t\t\t\t\t<input type='submit' class='rigActionButton rigNextButton' value='' title='' style='display: block;' />\r\n\t\t\t\t\t<div id='imageGallery_ImageWrapper' class='rigActiveImage'>\r\n\t\t\t\t\t\t<img alt='Content Image' src='image' style='width: 100%; height: 100%; position: absolute;' />\r\n\t\t\t\t\t\t<div class='rigToolsWrapper'>\r\n\t\t\t\t\t\t\t<div class='rigDescriptionBox' style='display: block; background: #4B4B4B; width: 45%; margin-left: 20%;'>\r\n\t\t\t\t\t\t\t\t<h4 class='rigTitle'>Title</h4>\r\n\t\t\t\t\t\t\t\t<p class='rigDescription'>Description Box</p>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t<div class='rigToolbar rigToolbarBottomInside' style='height: 50px; line-height: 30px; background: #ccc;'>\r\n\t\t\t\t\t\t\t\t<span class='rigItemsCount'>Item 1 of 5</span>\r\n\t\t\t\t\t\t\t\t<div class='rigControlsSet'>\r\n\t\t\t\t\t\t\t\t\t<button title='Play Slideshow' class='rigActionButton rigPlayButton'><span class='rigIcon rigPlayIcon' style='vertical-align: top;'></span></button>\r\n\t\t\t\t\t\t\t\t\t<button title='Enter FullScreen' class='rigActionButton rigFullScrButton'><span class='rigIcon rigFullScrIcon' style='vertical-align: top;'></span></button>\r\n\t\t\t\t\t\t\t\t\t<button title='Hide Thumbnails' class='rigActionButton rigHideThumbnButton'><span class='rigIcon rigHideThumbnIcon' style='vertical-align: top;'></span></button>\r\n\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t\t\t<div class='rigThumbnailsBoxHorizontal rigPositionBottom' style='width: 100%; height: 100px;'>\r\n\t\t\t\t\t<button title='Scroll Prev' class='rigActionButton rigScrollPrevButton'><span class='rigIcon rigScrollPrevIcon'></span></button>\r\n\t\t\t\t\t<button title='Scroll Next' class='rigActionButton rigScrollNextButton'><span class='rigIcon rigScrollNextIcon'></span></button>\r\n\t\t\t\t\t<div class='rigThumbnailsBox'>\r\n\t\t\t\t\t\t<div>\r\n\t\t\t\t\t\t\t<ul class='rigThumbnailsList' style='width: 2000px;'>\r\n\t\t\t\t\t\t\t\t<li style='vertical-align: bottom;'><a href='#'><img src='img' style='height: 100px; width: 100px;'></a></li>\r\n\t\t\t\t\t\t\t\t<li style='vertical-align: bottom;'><a href='#'><img src='img' style='height: 100px; width: 100px;'></a></li>\r\n\t\t\t\t\t\t\t\t<li style='vertical-align: bottom;'><a href='#'><img src='img' style='height: 100px; width: 100px;'></a></li>\r\n\t\t\t\t\t\t\t\t<li style='vertical-align: bottom;'><a href='#'><img src='img' style='height: 100px; width: 100px;'></a></li>\r\n\t\t\t\t\t\t\t\t<li style='vertical-align: bottom;'><a href='#'><img src='img' style='height: 100px; width: 100px;'></a></li>\r\n\t\t\t\t\t\t\t\t<li style='vertical-align: bottom;'><a href='#'><img src='img' style='height: 100px; width: 100px;'></a></li>\r\n\t\t\t\t\t\t\t\t<li style='vertical-align: bottom;'><a href='#'><img src='img' style='height: 100px; width: 100px;'></a></li>\r\n\t\t\t\t\t\t\t\t<li style='vertical-align: bottom;'><a href='#'><img src='img' style='height: 100px; width: 100px;'></a></li>\r\n\t\t\t\t\t\t\t</ul>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t", new object[0]));
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000A0424 File Offset: 0x0009E624
		// (set) Token: 0x060030AF RID: 12463 RVA: 0x000A042C File Offset: 0x0009E62C
		public override object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
				this.ThumbnailsArea.ListView.DataSource = value;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x000A0448 File Offset: 0x0009E648
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x000A0471 File Offset: 0x0009E671
		[System.ComponentModel.Description("Determines if the control will have WAI-ARIA support enabled")]
		[Category("Behavior")]
		[DefaultValue(false)]
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

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000A0489 File Offset: 0x0009E689
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x000A04A9 File Offset: 0x0009E6A9
		public override string DataSourceID
		{
			get
			{
				return (this.ViewState["DataSourceID"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataSourceID"] = value;
				this.ThumbnailsArea.ListView.DataSourceID = value;
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x000A04CD File Offset: 0x0009E6CD
		// (set) Token: 0x060030B5 RID: 12469 RVA: 0x000A04ED File Offset: 0x0009E6ED
		[Category("Data")]
		[System.ComponentModel.Description("Gets or sets the ID of the ClientDataSource control which is used for client-side databinding scenarios.")]
		[DefaultValue("")]
		public string ClientDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ClientDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientDataSourceID"] = value;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000A0500 File Offset: 0x0009E700
		[System.ComponentModel.Description("Gets the items collection which holds the data associated with the items that will be populated in the ThumbnailArea and the items that will be shown in the current content view mode.")]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public ImageGalleryItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ImageGalleryItemCollection(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this.items).TrackViewState();
				}
				return this.items;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000A052F File Offset: 0x0009E72F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[System.ComponentModel.Description("Gets the settings for the RadImageGallery ThumbnailArea")]
		public ImageGalleryThumbnailsAreaSettings ThumbnailsAreaSettings
		{
			get
			{
				if (this.thumbnailsAreaSettings == null)
				{
					this.thumbnailsAreaSettings = new ImageGalleryThumbnailsAreaSettings(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this.thumbnailsAreaSettings).TrackViewState();
				}
				return this.thumbnailsAreaSettings;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000A055E File Offset: 0x0009E75E
		[Category("Behavior")]
		[System.ComponentModel.Description("Gets the settings for the RadImageGallery ImageArea")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public ImageGalleryImageAreaSettings ImageAreaSettings
		{
			get
			{
				if (this.imageAreaSettings == null)
				{
					this.imageAreaSettings = new ImageGalleryImageAreaSettings(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this.imageAreaSettings).TrackViewState();
				}
				return this.imageAreaSettings;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000A058D File Offset: 0x0009E78D
		[System.ComponentModel.Description("Gets the settings associated with the RadImageGallery Toolbar")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public ImageGalleryToolbarSettings ToolbarSettings
		{
			get
			{
				if (this.toolbarSettings == null)
				{
					this.toolbarSettings = new ImageGalleryToolbarSettings(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this.toolbarSettings).TrackViewState();
				}
				return this.toolbarSettings;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000A05BC File Offset: 0x0009E7BC
		[System.ComponentModel.Description("Gets the settings associated with the RadImageGallery Toolbar")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ImageGalleryClientSettings ClientSettings
		{
			get
			{
				if (this.clientSettings == null)
				{
					this.clientSettings = new ImageGalleryClientSettings(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this.clientSettings).TrackViewState();
				}
				return this.clientSettings;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000A05EB File Offset: 0x0009E7EB
		[NotifyParentProperty(true)]
		[Category("Client-side events")]
		[System.ComponentModel.Description("Gets a reference to the ImageGalleryPagerStyle, which holds properties for controlling the behavior of the ImageGallery pager item.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ImageGalleryPagerStyle PagerStyle
		{
			get
			{
				if (this.pagerStyle == null)
				{
					this.pagerStyle = new ImageGalleryPagerStyle(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this.pagerStyle).TrackViewState();
				}
				return this.pagerStyle;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000A061A File Offset: 0x0009E81A
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x000A063B File Offset: 0x0009E83B
		[Category("Data")]
		[DefaultValue(null)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(StringArrayConverter))]
		[NotifyParentProperty(true)]
		[System.ComponentModel.Description("Gets or sets an array of data-field names that will be used to populate the DataKeyValues collection, when the RadImageGallery control is databinding")]
		public virtual string[] DataKeyNames
		{
			get
			{
				return (this.ViewState["DataKeyNames"] as string[]) ?? new string[0];
			}
			set
			{
				this.ViewState["DataKeyNames"] = value;
				this.ThumbnailsArea.ListView.DataKeyNames = value;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000A065F File Offset: 0x0009E85F
		[Browsable(false)]
		public RadListView ThumbnailListView
		{
			get
			{
				return this.ThumbnailsArea.ListView;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000A0680 File Offset: 0x0009E880
		[Browsable(false)]
		public RadAjaxLoadingPanel LoadingPanel
		{
			get
			{
				if (this.loadingPanel == null)
				{
					this.loadingPanel = new RadAjaxLoadingPanel();
					this.loadingPanel.ID = "LoadingPanel";
					this.loadingPanel.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
					this.loadingPanel.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
					this.loadingPanel.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
					this.loadingPanel.PreRender += delegate(object sender, EventArgs e)
					{
						((ISkinnableControl)sender).Skin = base.RuntimeSkin;
					};
				}
				return this.loadingPanel;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x000A071C File Offset: 0x0009E91C
		[Browsable(false)]
		public RadLightBox LightBox
		{
			get
			{
				if (this.lightBox == null)
				{
					this.lightBox = new RadLightBox();
					this.lightBox.ID = "LightBox";
					this.lightBox.PreRender += delegate(object sender, EventArgs e)
					{
						((ISkinnableControl)sender).Skin = base.RuntimeSkin;
					};
				}
				return this.lightBox;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000A0784 File Offset: 0x0009E984
		[Browsable(false)]
		public RadToolTip RadToolTip
		{
			get
			{
				if (this.toolTip == null)
				{
					this.toolTip = new RadToolTip();
					this.toolTip.ID = "ToolTip";
					this.toolTip.Width = 200;
					this.toolTip.Height = 200;
					this.toolTip.AutoCloseDelay = 0;
					this.toolTip.Position = ToolTipPosition.TopCenter;
					this.toolTip.HideEvent = ToolTipHideEvent.LeaveTargetAndToolTip;
					this.toolTip.ShowEvent = ToolTipShowEvent.FromCode;
					this.toolTip.RelativeTo = ToolTipRelativeDisplay.Element;
					HtmlGenericControl htmlGenericControl = new HtmlGenericControl("img");
					htmlGenericControl.Style.Add(HtmlTextWriterStyle.Display, "none");
					this.toolTip.Controls.Add(htmlGenericControl);
					this.toolTip.PreRender += delegate(object sender, EventArgs e)
					{
						((ISkinnableControl)sender).Skin = base.RuntimeSkin;
					};
				}
				return this.toolTip;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x000A0874 File Offset: 0x0009EA74
		// (set) Token: 0x060030C3 RID: 12483 RVA: 0x000A089D File Offset: 0x0009EA9D
		[NotifyParentProperty(true)]
		[System.ComponentModel.Description("Gets or sets a value indicating the index of the current page the RadImageGallery is paged on")]
		[Category("Data")]
		[DefaultValue(0)]
		public int CurrentItemIndex
		{
			get
			{
				object obj = this.ControlState["CurrentItemIndex"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				this.ControlState["CurrentItemIndex"] = value;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000A08B5 File Offset: 0x0009EAB5
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x000A08D5 File Offset: 0x0009EAD5
		[DefaultValue("")]
		[System.ComponentModel.Description("A relative or absolute path to a folder from which the RadImageGallery items will be populated.")]
		[Category("Data")]
		public string ImagesFolderPath
		{
			get
			{
				return ((string)this.ControlState["ImagesFolderPath"]) ?? string.Empty;
			}
			set
			{
				this.ControlState["ImagesFolderPath"] = value;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x000A08E8 File Offset: 0x0009EAE8
		// (set) Token: 0x060030C7 RID: 12487 RVA: 0x000A0908 File Offset: 0x0009EB08
		[Category("Data")]
		[System.ComponentModel.Description("Gets or sets a string value determining the field in the RadImageGallery.DataSource which the RadImageGallery control will bind its items' ImageGalleryItem.ImageUrl or ImageGalleryItem.ImageDataValue. The field could be of type String for specifying the ImageGalleryItem.ImageUrl value. Or byte array(byte[]) for specifying the ImageGalleryItem.ImageDataValue")]
		[DefaultValue("")]
		public string DataImageField
		{
			get
			{
				return ((string)this.ViewState["DataImageField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataImageField"] = value;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000A091B File Offset: 0x0009EB1B
		// (set) Token: 0x060030C9 RID: 12489 RVA: 0x000A093B File Offset: 0x0009EB3B
		[System.ComponentModel.Description("Gets or sets a string value determining the field in the RadImageGallery.DataSource which the RadImageGallery control will bind its items' ImageGalleryItem.ImageUrl or ImageGalleryItem.ThumbnailDataValue property. The field could be of type String for specifying the ImageGalleryItem.ImageUrl value. Or byte array(byte[]) for specifying the ImageGalleryItem.ImageDataValue.")]
		[Category("Data")]
		[DefaultValue("")]
		public string DataThumbnailField
		{
			get
			{
				return ((string)this.ViewState["DataThumbnailField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataThumbnailField"] = value;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x000A094E File Offset: 0x0009EB4E
		// (set) Token: 0x060030CB RID: 12491 RVA: 0x000A096E File Offset: 0x0009EB6E
		[Category("Data")]
		[DefaultValue("")]
		[System.ComponentModel.Description("Gets or sets a string value determining the field in the RadImageGallery.DataSource which the RadImageGallery control will bind its items' ImageGalleryItemBase.Title")]
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

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x000A0981 File Offset: 0x0009EB81
		// (set) Token: 0x060030CD RID: 12493 RVA: 0x000A09A1 File Offset: 0x0009EBA1
		[Category("Data")]
		[System.ComponentModel.Description("Gets or sets a string value determining the field in the RadImageGallery.DataSource which the RadImageGallery control will bind its items' ImageGalleryItemBase.Description property.")]
		[DefaultValue("")]
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

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000A09B4 File Offset: 0x0009EBB4
		// (set) Token: 0x060030CF RID: 12495 RVA: 0x000A09DD File Offset: 0x0009EBDD
		[System.ComponentModel.Description("Gets or sets a value that indicates whether RadImageGallery.Items collection is cleared before DataBinding.")]
		[Category("Data")]
		[DefaultValue(false)]
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

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000A09F8 File Offset: 0x0009EBF8
		// (set) Token: 0x060030D1 RID: 12497 RVA: 0x000A0A21 File Offset: 0x0009EC21
		[System.ComponentModel.Description("Gets or sets a value indicating whether the automatic paging feature is enabled.")]
		[Category("Data")]
		[DefaultValue(false)]
		public bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
				this.ThumbnailsArea.ListView.AllowPaging = value;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000A0A4C File Offset: 0x0009EC4C
		// (set) Token: 0x060030D3 RID: 12499 RVA: 0x000A0A75 File Offset: 0x0009EC75
		[Category("Data")]
		[System.ComponentModel.Description("Gets or sets an integer value representing the current page index")]
		[DefaultValue(0)]
		public int CurrentPageIndex
		{
			get
			{
				object obj = this.ViewState["CurrentPageIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["CurrentPageIndex"] = value;
				this.ThumbnailsArea.ListView.CurrentPageIndex = value;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000A0AA0 File Offset: 0x0009ECA0
		// (set) Token: 0x060030D5 RID: 12501 RVA: 0x000A0ACA File Offset: 0x0009ECCA
		[DefaultValue(10)]
		[System.ComponentModel.Description("Gets or sets an integer value indicating the number of items that will be populated in the RadImageGallery ThumbnailArea")]
		[Category("Data")]
		public int PageSize
		{
			get
			{
				object obj = this.ViewState["PageSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				this.ViewState["PageSize"] = value;
				this.ThumbnailsArea.ListView.PageSize = value;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x000A0AF4 File Offset: 0x0009ECF4
		// (set) Token: 0x060030D7 RID: 12503 RVA: 0x000A0B1D File Offset: 0x0009ED1D
		[DefaultValue(ImageGalleryDisplayAreaMode.Image)]
		[System.ComponentModel.Description("Gets or sets a value indicating the mode the RadImageGalllery control will operate in. The mode determines the appearance and the way the entire control will work.")]
		[Category("Appearance")]
		public ImageGalleryDisplayAreaMode DisplayAreaMode
		{
			get
			{
				object obj = this.ViewState["DisplayAreaMode"];
				if (obj != null)
				{
					return (ImageGalleryDisplayAreaMode)obj;
				}
				return ImageGalleryDisplayAreaMode.Image;
			}
			set
			{
				if (value == ImageGalleryDisplayAreaMode.LightBox)
				{
					this.Items.RefreshLightBoxItems();
				}
				this.ViewState["DisplayAreaMode"] = value;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000A0B44 File Offset: 0x0009ED44
		// (set) Token: 0x060030D9 RID: 12505 RVA: 0x000A0B6D File Offset: 0x0009ED6D
		[DefaultValue(false)]
		[System.ComponentModel.Description("If enabled, this will loop the items when the last/first one is reached.")]
		[NotifyParentProperty(true)]
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
				this.LightBox.LoopItems = value;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000A0B94 File Offset: 0x0009ED94
		// (set) Token: 0x060030DB RID: 12507 RVA: 0x000A0BBD File Offset: 0x0009EDBD
		[System.ComponentModel.Description("Determines if the RadImageGallery control will display loading panel when image is being loaded.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool ShowLoadingPanel
		{
			get
			{
				object obj = this.ViewState["ShowLoadingPanel"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ShowLoadingPanel"] = value;
				this.LightBox.ShowLoadingPanel = value;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x000A0BE1 File Offset: 0x0009EDE1
		// (set) Token: 0x060030DD RID: 12509 RVA: 0x000A0C04 File Offset: 0x0009EE04
		[System.ComponentModel.Description("Gets or sets a value indicating where RadImageGallery will look for its .resx localization files.")]
		[Category("Misc")]
		[DefaultValue("")]
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

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x000A0C57 File Offset: 0x0009EE57
		// (set) Token: 0x060030DF RID: 12511 RVA: 0x000A0C77 File Offset: 0x0009EE77
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[System.ComponentModel.Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Appearance")]
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

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x060030E1 RID: 12513 RVA: 0x000A0DAB File Offset: 0x0009EFAB
		// (remove) Token: 0x060030E2 RID: 12514 RVA: 0x000A0DBE File Offset: 0x0009EFBE
		[Category("Action")]
		[System.ComponentModel.Description("Event is fired when RadImageGallery control fires a command event or by manyally calling the fireCommand client-side method available in the Telerik.Web.UI.RadImageGallery client-side object.")]
		public event EventHandler<ImageGalleryCommandEventArgs> Command
		{
			add
			{
				base.Events.AddHandler(RadImageGallery.EventCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageGallery.EventCommand, value);
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000A0DD4 File Offset: 0x0009EFD4
		internal void CallOnCommand(ImageGalleryCommandEventArgs e)
		{
			EventHandler<ImageGalleryCommandEventArgs> eventHandler = base.Events[RadImageGallery.EventCommand] as EventHandler<ImageGalleryCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x060030E4 RID: 12516 RVA: 0x000A0E02 File Offset: 0x0009F002
		// (remove) Token: 0x060030E5 RID: 12517 RVA: 0x000A0E15 File Offset: 0x0009F015
		[Category("Action")]
		[System.ComponentModel.Description("The event is raised before RadImageGallery is data-bound and its purpose is to specify the RadImageGallery.DataSource property to which the control will be bound to.")]
		public event EventHandler<ImageGalleryNeedDataSourceEventArgs> NeedDataSource
		{
			add
			{
				base.Events.AddHandler(RadImageGallery.EventNeedDataSource, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageGallery.EventNeedDataSource, value);
			}
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000A0E28 File Offset: 0x0009F028
		internal void CallOnNeedDataSource(ImageGalleryNeedDataSourceEventArgs e)
		{
			EventHandler<ImageGalleryNeedDataSourceEventArgs> eventHandler = base.Events[RadImageGallery.EventNeedDataSource] as EventHandler<ImageGalleryNeedDataSourceEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x060030E7 RID: 12519 RVA: 0x000A0E56 File Offset: 0x0009F056
		// (remove) Token: 0x060030E8 RID: 12520 RVA: 0x000A0E69 File Offset: 0x0009F069
		[System.ComponentModel.Description("The event is raised when a RadImageGallery item and its associated RadListView item are created. The event arguments provide access to both items for further configuration or access to controls.")]
		[Category("Action")]
		public event EventHandler<ImageGalleryItemEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadImageGallery.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageGallery.EventItemCreated, value);
			}
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000A0E7C File Offset: 0x0009F07C
		internal void CallOnItemCreated(ImageGalleryItemEventArgs e)
		{
			EventHandler<ImageGalleryItemEventArgs> eventHandler = base.Events[RadImageGallery.EventItemCreated] as EventHandler<ImageGalleryItemEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x060030EA RID: 12522 RVA: 0x000A0EAA File Offset: 0x0009F0AA
		// (remove) Token: 0x060030EB RID: 12523 RVA: 0x000A0EBD File Offset: 0x0009F0BD
		[Category("Action")]
		[System.ComponentModel.Description("The event is raised when a RadImageGallery item and its associated RadListView item are data-bound. The event arguments provide access to both items for further configuration or access to controls.")]
		public event EventHandler<ImageGalleryItemEventArgs> ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadImageGallery.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageGallery.EventItemDataBound, value);
			}
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000A0ED0 File Offset: 0x0009F0D0
		internal void CallOnItemDataBound(ImageGalleryItemEventArgs e)
		{
			EventHandler<ImageGalleryItemEventArgs> eventHandler = base.Events[RadImageGallery.EventItemDataBound] as EventHandler<ImageGalleryItemEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x060030ED RID: 12525 RVA: 0x000A0EFE File Offset: 0x0009F0FE
		// (remove) Token: 0x060030EE RID: 12526 RVA: 0x000A0F11 File Offset: 0x0009F111
		[System.ComponentModel.Description("The event is raised when the RadImageGallery changes its CurrentPageIndex and goes to a different page.")]
		[Category("Action")]
		public event EventHandler<ImageGalleryPageIndexChangedEventArgs> PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadImageGallery.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageGallery.EventPageIndexChanged, value);
			}
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000A0F24 File Offset: 0x0009F124
		internal void CallOnPageIndexChanged(ImageGalleryPageIndexChangedEventArgs e)
		{
			EventHandler<ImageGalleryPageIndexChangedEventArgs> eventHandler = base.Events[RadImageGallery.EventPageIndexChanged] as EventHandler<ImageGalleryPageIndexChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x060030F0 RID: 12528 RVA: 0x000A0F52 File Offset: 0x0009F152
		// (remove) Token: 0x060030F1 RID: 12529 RVA: 0x000A0F65 File Offset: 0x0009F165
		[Category("Action")]
		[System.ComponentModel.Description("The event is raised when the RadImageGallery.PageSize changes.")]
		public event EventHandler<ImageGalleryPageSizeChangedEventArgs> PageSizeChanged
		{
			add
			{
				base.Events.AddHandler(RadImageGallery.EventPageSizeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageGallery.EventPageSizeChanged, value);
			}
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000A0F78 File Offset: 0x0009F178
		internal void CallOnPageSizeChanged(ImageGalleryPageSizeChangedEventArgs e)
		{
			EventHandler<ImageGalleryPageSizeChangedEventArgs> eventHandler = base.Events[RadImageGallery.EventPageSizeChanged] as EventHandler<ImageGalleryPageSizeChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x060030F3 RID: 12531 RVA: 0x000A0FA6 File Offset: 0x0009F1A6
		// (remove) Token: 0x060030F4 RID: 12532 RVA: 0x000A0FB9 File Offset: 0x0009F1B9
		public event EventHandler<ImageGalleryImageRequestedEventArgs> ImageRequested
		{
			add
			{
				base.Events.AddHandler(RadImageGallery.EventImageRequested, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageGallery.EventImageRequested, value);
			}
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000A0FCC File Offset: 0x0009F1CC
		internal void CallOnImageRequested(ImageGalleryImageRequestedEventArgs e)
		{
			EventHandler<ImageGalleryImageRequestedEventArgs> eventHandler = base.Events[RadImageGallery.EventImageRequested] as EventHandler<ImageGalleryImageRequestedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000A0FFC File Offset: 0x0009F1FC
		public void Rebind()
		{
			this.DataBind();
			if (this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right || this.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left)
			{
				using (IEnumerator enumerator = this.ImageArea.Controls.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Control control = (Control)obj;
						ImageGalleryPagerItem imageGalleryPagerItem = control as ImageGalleryPagerItem;
						if (imageGalleryPagerItem != null)
						{
							imageGalleryPagerItem.Recreate();
						}
					}
					return;
				}
			}
			foreach (object obj2 in this.Controls)
			{
				Control control2 = (Control)obj2;
				ImageGalleryPagerItem imageGalleryPagerItem2 = control2 as ImageGalleryPagerItem;
				if (imageGalleryPagerItem2 != null)
				{
					imageGalleryPagerItem2.Recreate();
				}
			}
		}

		// Token: 0x04000D1C RID: 3356
		internal const string ThumbnailAreaClassName = "rigThumbnailsBox";

		// Token: 0x04000D1D RID: 3357
		internal const string ThumbnailsListClassName = "rigThumbnailsList";

		// Token: 0x04000D1E RID: 3358
		internal const string ActiveThumbnailClassName = "rigThumbnailActive";

		// Token: 0x04000D1F RID: 3359
		internal const string ImageAreaClassName = "rigItemBox";

		// Token: 0x04000D20 RID: 3360
		internal const string ActiveImageClassName = "rigActiveImage";

		// Token: 0x04000D21 RID: 3361
		internal const string ToolsWrapperClassName = "rigToolsWrapper";

		// Token: 0x04000D22 RID: 3362
		internal const string DescriptionBoxClasName = "rigDescriptionBox";

		// Token: 0x04000D23 RID: 3363
		internal const string TitleClassName = "rigTitle";

		// Token: 0x04000D24 RID: 3364
		internal const string DescriptionClassName = "rigDescription";

		// Token: 0x04000D25 RID: 3365
		internal const string DotListClassName = "rigDotList";

		// Token: 0x04000D26 RID: 3366
		internal const string CurrentItemClassName = "rigCurrentItem";

		// Token: 0x04000D27 RID: 3367
		internal const string TooltipClassName = "rigTooltip";

		// Token: 0x04000D28 RID: 3368
		internal const string ToolbarClassName = "rigToolbar";

		// Token: 0x04000D29 RID: 3369
		internal const string ProgressBarClassName = "rigProgressBar";

		// Token: 0x04000D2A RID: 3370
		internal const string ItemsCounterClassName = "rigItemsCount";

		// Token: 0x04000D2B RID: 3371
		internal const string ControlSetClassName = "rigControlsSet";

		// Token: 0x04000D2C RID: 3372
		internal const string DummyClassName = "rigDummy";

		// Token: 0x04000D2D RID: 3373
		public const string RebindImageGalleryCommandName = "RebindImageGallery";

		// Token: 0x04000D2E RID: 3374
		public const string PageCommandName = "Page";

		// Token: 0x04000D2F RID: 3375
		public const string FirstPageCommandArgument = "First";

		// Token: 0x04000D30 RID: 3376
		public const string LastPageCommandArgument = "Last";

		// Token: 0x04000D31 RID: 3377
		public const string NextPageCommandArgument = "Next";

		// Token: 0x04000D32 RID: 3378
		public const string PrevPageCommandArgument = "Prev";

		// Token: 0x04000D33 RID: 3379
		public const string ChangePageSizeCommandName = "ChangePageSize";

		// Token: 0x04000D34 RID: 3380
		public const string ChangeItemIndexCommandName = "ChangeItemIndex";

		// Token: 0x04000D35 RID: 3381
		private int activeItemIndex = -1;

		// Token: 0x04000D36 RID: 3382
		private ImageGalleryThumbnailsArea thumbnailsArea;

		// Token: 0x04000D37 RID: 3383
		private ImageGalleryImageArea imageArea;

		// Token: 0x04000D38 RID: 3384
		private ImageGalleryStrings _localization;

		// Token: 0x04000D39 RID: 3385
		private static Func<string, string> parseFireCommandArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[2];
		};

		// Token: 0x04000D3A RID: 3386
		private static Func<string, string> parseFireCommandEventName = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[0];
		};

		// Token: 0x04000D3B RID: 3387
		private string[] callbackArguments;

		// Token: 0x04000D3C RID: 3388
		private ControlStateManager controlStateManager;

		// Token: 0x04000D3D RID: 3389
		private object dataSource;

		// Token: 0x04000D3E RID: 3390
		private ImageGalleryItemCollection items;

		// Token: 0x04000D3F RID: 3391
		private ImageGalleryThumbnailsAreaSettings thumbnailsAreaSettings;

		// Token: 0x04000D40 RID: 3392
		private ImageGalleryImageAreaSettings imageAreaSettings;

		// Token: 0x04000D41 RID: 3393
		private ImageGalleryToolbarSettings toolbarSettings;

		// Token: 0x04000D42 RID: 3394
		private ImageGalleryClientSettings clientSettings;

		// Token: 0x04000D43 RID: 3395
		private ImageGalleryPagerStyle pagerStyle;

		// Token: 0x04000D44 RID: 3396
		private RadAjaxLoadingPanel loadingPanel;

		// Token: 0x04000D45 RID: 3397
		private RadLightBox lightBox;

		// Token: 0x04000D46 RID: 3398
		private RadToolTip toolTip;

		// Token: 0x04000D47 RID: 3399
		private static readonly object EventCommand = new object();

		// Token: 0x04000D48 RID: 3400
		private static readonly object EventNeedDataSource = new object();

		// Token: 0x04000D49 RID: 3401
		private static readonly object EventItemCreated = new object();

		// Token: 0x04000D4A RID: 3402
		private static readonly object EventItemDataBound = new object();

		// Token: 0x04000D4B RID: 3403
		private static readonly object EventPageIndexChanged = new object();

		// Token: 0x04000D4C RID: 3404
		private static readonly object EventPageSizeChanged = new object();

		// Token: 0x04000D4D RID: 3405
		private static readonly object EventImageRequested = new object();
	}
}
