using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common.Helpers;
using Telerik.Web.UI.Rotator;

namespace Telerik.Web.UI
{
	// Token: 0x020019D4 RID: 6612
	[ClientScriptResource("Telerik.Web.UI.RadRotator", "Telerik.Web.UI.Rotator.RadRotator.js")]
	[Designer("Telerik.Web.Design.RadRotatorDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	[ToolboxData("<{0}:RadRotator runat=\"server\"></{0}:RadRotator>")]
	[RequiredScript(typeof(AnimationScripts))]
	[RequiredScript(typeof(MaterialRipple))]
	[RequiredScript(typeof(IETouchActionManager))]
	[ClientScriptResource("Telerik.Web.UI.RadRotator", "Telerik.Web.UI.Common.Navigation.NavigationScripts.js")]
	[TelerikToolboxCategory("Visualization")]
	[ToolboxBitmap(typeof(RadRotator), "Telerik.Web.UI.Rotator.png")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadRotator))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[EmbeddedSkin("Rotator", "Default")]
	[EmbeddedSkin("Rotator")]
	public class RadRotator : RadDataBoundControl, IPostBackEventHandler, INamingContainer
	{
		// Token: 0x0600FF9E RID: 65438 RVA: 0x00395BE0 File Offset: 0x00393DE0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "appendClientDataBoundItems", this.AppendClientDataBoundItems, false);
			base.DescribeProperty<bool>(descriptor, "enableDragScrolling", this.EnableDragScrolling, false);
			base.DescribeProperty<bool>(descriptor, "enableRandomOrder", this.EnableRandomOrder, false);
			base.DescribeProperty<int>(descriptor, "frameDuration", this.FrameDuration, 2000);
			base.DescribeProperty<int>(descriptor, "initialItemIndex", this.InitialItemIndex, 0);
			base.DescribeProperty<bool>(descriptor, "pauseOnMouseOver", this.PauseOnMouseOver, true);
			base.DescribeProperty<bool>(descriptor, "persistCurrentItemOnPostBack", this.PersistCurrentItemOnPostBack, false);
			base.DescribeProperty<RotatorType>(descriptor, "rotatorType", this.RotatorType, RotatorType.AutomaticAdvance);
			base.DescribeProperty<RotatorScrollDirection>(descriptor, "scrollDirection", this.ScrollDirection, RotatorScrollDirection.Left | RotatorScrollDirection.Right);
			base.DescribeProperty<int>(descriptor, "scrollDuration", this.ScrollDuration, 500);
			base.DescribeProperty<bool>(descriptor, "wrapFrames", this.WrapFrames, true);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600FF9F RID: 65439 RVA: 0x00395CD0 File Offset: 0x00393ED0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "dataBound", this.OnClientDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicked", this.OnClientItemClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicking", this.OnClientItemClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDataBound", this.OnClientItemDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemShowing", this.OnClientItemShowing);
			RadDataBoundControl.DescribeEvent(descriptor, "itemShown", this.OnClientItemShown);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequested", this.OnClientItemsRequested);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequestFailed", this.OnClientItemsRequestFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequesting", this.OnClientItemsRequesting);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			RadDataBoundControl.DescribeEvent(descriptor, "templateDataBound", this.OnClientTemplateDataBound);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0600FFA0 RID: 65440 RVA: 0x00395DC1 File Offset: 0x00393FC1
		public RadRotator()
		{
			this._slideShowAnimation = new AnimationSettings("SlideShow", this.ViewState);
		}

		// Token: 0x0600FFA1 RID: 65441 RVA: 0x00395DE8 File Offset: 0x00393FE8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			descriptor.AddProperty("skin", base.RuntimeSkin);
			this.WebServiceSettings.Describe("webServiceSettings", serializer, descriptor);
			if (!this.ItemWidth.IsEmpty)
			{
				descriptor.AddScriptProperty("_itemWidth", "\"" + this.ItemWidth.ToString() + "\"");
			}
			if (!this.ItemHeight.IsEmpty)
			{
				descriptor.AddScriptProperty("_itemHeight", "\"" + this.ItemHeight.ToString() + "\"");
			}
			if (this.AutoPostBack)
			{
				descriptor.AddScriptProperty("_postBackReference", "\"" + this.GetPostbackEventReference() + "\"");
			}
			descriptor.AddScriptProperty("items", this.Items.Serialize());
			if (this.PersistCurrentItemOnPostBack)
			{
				descriptor.AddProperty("_persistedItemIndex", this._persistedItemIndex);
			}
			if (!this.Enabled)
			{
				descriptor.AddProperty("enabled", this.Enabled);
			}
			this.ControlButtons.Describe("controlButtons", descriptor, this.NamingContainer);
			this.SlideShowAnimation.Describe("slideShowAnimationSettings", descriptor);
			if (!string.IsNullOrEmpty(this.ClientTemplate))
			{
				descriptor.AddProperty("_clientTemplate", this.ClientTemplate);
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
				}
			}
		}

		// Token: 0x17004D23 RID: 19747
		// (get) Token: 0x0600FFA2 RID: 65442 RVA: 0x00395FB4 File Offset: 0x003941B4
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600FFA3 RID: 65443 RVA: 0x00395FB8 File Offset: 0x003941B8
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				foreach (object obj in this.Items)
				{
					RadRotatorItem radRotatorItem = (RadRotatorItem)obj;
					radRotatorItem.DataBind();
				}
				return;
			}
			this.PrepareForDataBinding();
			this.BindToEnumerableData(data);
		}

		// Token: 0x0600FFA4 RID: 65444 RVA: 0x0039602C File Offset: 0x0039422C
		public void BindToEnumerableData(IEnumerable dataSource)
		{
			foreach (object dataObject in dataSource)
			{
				this.BindItem(this.Items, dataObject);
			}
		}

		// Token: 0x0600FFA5 RID: 65445 RVA: 0x00396084 File Offset: 0x00394284
		private RadRotatorItem BindItem(RadRotatorItemCollection items, object dataObject)
		{
			RadRotatorItem radRotatorItem = new RadRotatorItem(dataObject);
			items.Add(radRotatorItem);
			this.RaiseItemDataBound(radRotatorItem, dataObject);
			return radRotatorItem;
		}

		// Token: 0x0600FFA6 RID: 65446 RVA: 0x003960A8 File Offset: 0x003942A8
		private void RaiseItemDataBound(RadRotatorItem item, object dataItem)
		{
			item.DataItem = dataItem;
			item.DataBind();
			this.OnItemDataBound(item);
			item.DataItem = null;
		}

		// Token: 0x0600FFA7 RID: 65447 RVA: 0x003960C5 File Offset: 0x003942C5
		protected void PrepareForDataBinding()
		{
			if (!this.AppendDataBoundItems)
			{
				this.Items.Clear();
				base.ClearChildViewState();
			}
			this.TrackViewState();
		}

		// Token: 0x0600FFA8 RID: 65448 RVA: 0x003960E8 File Offset: 0x003942E8
		private void ApplyTemplate(RadRotatorItem item)
		{
			if (item.TemplateInstantiated)
			{
				return;
			}
			if (item.ItemTemplate == null && this.ItemTemplate == null)
			{
				return;
			}
			int i = item.Controls.Count;
			if (item.ItemTemplate != null)
			{
				item.ItemTemplate.InstantiateIn(item);
			}
			else if (this.ItemTemplate != null)
			{
				this.ItemTemplate.InstantiateIn(item);
			}
			while (i > 0)
			{
				item.Controls.Add(item.Controls[0]);
				i--;
			}
			item.TemplateInstantiated = true;
		}

		// Token: 0x0600FFA9 RID: 65449 RVA: 0x0039616D File Offset: 0x0039436D
		protected internal virtual void InitializeItem(RadRotatorItem item)
		{
			this.ApplyTemplate(item);
			this.OnItemCreated(item);
		}

		// Token: 0x0600FFAA RID: 65450 RVA: 0x00396180 File Offset: 0x00394380
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(array[1]);
		}

		// Token: 0x0600FFAB RID: 65451 RVA: 0x003961BC File Offset: 0x003943BC
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600FFAC RID: 65452 RVA: 0x003961F4 File Offset: 0x003943F4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x17004D24 RID: 19748
		// (get) Token: 0x0600FFAD RID: 65453 RVA: 0x00396207 File Offset: 0x00394407
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17004D25 RID: 19749
		// (get) Token: 0x0600FFAE RID: 65454 RVA: 0x0039620B File Offset: 0x0039440B
		protected override string CssClassFormatString
		{
			get
			{
				return "RadRotator RadRotator_{0}";
			}
		}

		// Token: 0x0600FFAF RID: 65455 RVA: 0x00396214 File Offset: 0x00394414
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!this._widthSet)
			{
				writer.AddStyleAttribute("width", this.Width.ToString());
			}
			if (!this._heightSet)
			{
				writer.AddStyleAttribute("height", this.Height.ToString());
			}
			base.RenderBeginTag(writer);
		}

		// Token: 0x0600FFB0 RID: 65456 RVA: 0x00396278 File Offset: 0x00394478
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			if (!base.DesignMode)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrRelativeWrapper");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrClipRegion");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrItemsList");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				base.RenderChildren(writer);
				if (this.Items.Count == 0)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
				writer.RenderEndTag();
				RadRotator.RenderRotatorButton(writer, "rrButton rrButtonUp p-icon p-i-arrow-up");
				RadRotator.RenderRotatorButton(writer, "rrButton rrButtonLeft p-icon p-i-arrow-left");
				RadRotator.RenderRotatorButton(writer, "rrButton rrButtonRight p-icon p-i-arrow-right");
				RadRotator.RenderRotatorButton(writer, "rrButton rrButtonDown p-icon p-i-arrow-down");
				writer.RenderEndTag();
				return;
			}
			writer.Write("RadRotator design mode. In this mode you can select the rotator's DataSource and edit the ItemTemplate");
		}

		// Token: 0x0600FFB1 RID: 65457 RVA: 0x00396342 File Offset: 0x00394542
		private static void RenderRotatorButton(HtmlTextWriter writer, string className)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, className);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0);");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
		}

		// Token: 0x0600FFB2 RID: 65458 RVA: 0x00396374 File Offset: 0x00394574
		private int GetVisibleButtonsCount()
		{
			RotatorControlButtonsConfiguration controlButtons = this.ControlButtons;
			int num = 0;
			if (this.IsScrollDirectionEnabled(RotatorScrollDirection.Down) && string.IsNullOrEmpty(controlButtons.DownButtonID))
			{
				num++;
			}
			if (this.IsScrollDirectionEnabled(RotatorScrollDirection.Up) && string.IsNullOrEmpty(controlButtons.UpButtonID))
			{
				num++;
			}
			if (num == 0)
			{
				if (this.IsScrollDirectionEnabled(RotatorScrollDirection.Right) && string.IsNullOrEmpty(controlButtons.RightButtonID))
				{
					num++;
				}
				if (this.IsScrollDirectionEnabled(RotatorScrollDirection.Left) && string.IsNullOrEmpty(controlButtons.LeftButtonID))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600FFB3 RID: 65459 RVA: 0x003963F6 File Offset: 0x003945F6
		private bool IsScrollDirectionEnabled(RotatorScrollDirection scrollDirection)
		{
			return (scrollDirection & this.ScrollDirection) > (RotatorScrollDirection)0;
		}

		// Token: 0x0600FFB4 RID: 65460 RVA: 0x00396404 File Offset: 0x00394604
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			this.BindToBanners(false);
			RotatorType rotatorType = this.RotatorType;
			bool flag = false;
			Unit itemWidth = this.ItemWidth;
			Unit itemHeight = this.ItemHeight;
			if (!itemWidth.IsEmpty || !itemHeight.IsEmpty)
			{
				bool flag2 = this.IsScrollDirectionEnabled(RotatorScrollDirection.Down) || this.IsScrollDirectionEnabled(RotatorScrollDirection.Up);
				int num = 0;
				if (rotatorType == RotatorType.SlideShowButtons)
				{
					num = this.GetVisibleButtonsCount() * 20;
				}
				double num2 = (flag2 ? this.Height.Value : this.Width.Value) - (double)num;
				Unit unit = flag2 ? itemHeight : itemWidth;
				RadRotatorItemCollection items = this.Items;
				if (unit.IsEmpty || 0.0 != num2 % unit.Value || 2.0 * (num2 / unit.Value) > (double)items.Count)
				{
					flag = true;
				}
				using (IEnumerator enumerator = items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						RadRotatorItem radRotatorItem = (RadRotatorItem)obj;
						if (this.IsItemSizeEmptyOrChanged(radRotatorItem.Width, itemWidth))
						{
							radRotatorItem.Width = itemWidth;
						}
						if (this.IsItemSizeEmptyOrChanged(radRotatorItem.Height, itemHeight))
						{
							radRotatorItem.Height = itemHeight;
						}
						if ((!flag2 && radRotatorItem.Width != itemWidth) || (flag2 && radRotatorItem.Height != itemHeight))
						{
							flag = true;
						}
					}
					goto IL_16D;
				}
			}
			flag = true;
			IL_16D:
			if ((rotatorType == RotatorType.SlideShow || rotatorType == RotatorType.SlideShowButtons) && this.SlideShowAnimation.Type == AnimationType.CrossFade && flag)
			{
				this.SlideShowAnimation.Type = AnimationType.Fade;
			}
		}

		// Token: 0x0600FFB5 RID: 65461 RVA: 0x003965B4 File Offset: 0x003947B4
		private bool IsItemSizeEmptyOrChanged(Unit currentSize, Unit expectedSize)
		{
			return !expectedSize.IsEmpty && (currentSize.IsEmpty || currentSize != expectedSize);
		}

		// Token: 0x0600FFB6 RID: 65462 RVA: 0x003965D4 File Offset: 0x003947D4
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				if (this.PersistCurrentItemOnPostBack)
				{
					RadRotatorClientState radRotatorClientState = javaScriptSerializer.Deserialize<RadRotatorClientState>(text);
					this._persistedItemIndex = radRotatorClientState.CurrentItemIndex;
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			return base.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x0600FFB7 RID: 65463 RVA: 0x00396648 File Offset: 0x00394848
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument);
			postBackOptions.ClientSubmit = true;
			if (this.Page != null && !string.IsNullOrEmpty(postBackUrl))
			{
				postBackOptions.ActionUrl = postBackUrl;
			}
			return postBackOptions;
		}

		// Token: 0x0600FFB8 RID: 65464 RVA: 0x0039667C File Offset: 0x0039487C
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			string postBackUrl = string.Empty;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				postBackUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			return this.GetPostBackOptions(control, argument, postBackUrl);
		}

		// Token: 0x0600FFB9 RID: 65465 RVA: 0x003966B8 File Offset: 0x003948B8
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x0600FFBA RID: 65466 RVA: 0x003966F4 File Offset: 0x003948F4
		protected internal virtual RadRotatorItem FindItemByHierarchicalIndex(string hierarchicalIndex)
		{
			if (string.IsNullOrEmpty(hierarchicalIndex))
			{
				return null;
			}
			int num = Convert.ToInt32(hierarchicalIndex);
			if (num >= this.Items.Count)
			{
				return null;
			}
			return this.Items[num];
		}

		// Token: 0x0600FFBB RID: 65467 RVA: 0x0039672E File Offset: 0x0039492E
		void IPostBackEventHandler.RaisePostBackEvent(string itemIndex)
		{
			this.RaisePostBackEvent(itemIndex);
		}

		// Token: 0x0600FFBC RID: 65468 RVA: 0x00396738 File Offset: 0x00394938
		protected virtual void RaisePostBackEvent(string itemIndex)
		{
			RadRotatorItem radRotatorItem = this.FindItemByHierarchicalIndex(itemIndex);
			if (radRotatorItem != null)
			{
				this.OnItemClick(radRotatorItem);
			}
		}

		// Token: 0x0600FFBD RID: 65469 RVA: 0x00396758 File Offset: 0x00394958
		protected void RaiseRotatorItemEvent(RadRotatorItem item, object eventKey)
		{
			RadRotatorEventHandler radRotatorEventHandler = (RadRotatorEventHandler)base.Events[eventKey];
			if (radRotatorEventHandler != null)
			{
				radRotatorEventHandler(this, new RadRotatorEventArgs(item));
			}
		}

		// Token: 0x0600FFBE RID: 65470 RVA: 0x00396787 File Offset: 0x00394987
		protected virtual void OnItemCreated(RadRotatorItem item)
		{
			this.RaiseRotatorItemEvent(item, RadRotator.ItemCreatedEvent);
		}

		// Token: 0x0600FFBF RID: 65471 RVA: 0x00396795 File Offset: 0x00394995
		protected virtual void OnItemClick(RadRotatorItem item)
		{
			this.RaiseRotatorItemEvent(item, RadRotator.ItemClickEvent);
		}

		// Token: 0x0600FFC0 RID: 65472 RVA: 0x003967A3 File Offset: 0x003949A3
		protected virtual void OnItemDataBound(RadRotatorItem item)
		{
			this.RaiseRotatorItemEvent(item, RadRotator.ItemDataBoundEvent);
		}

		// Token: 0x0600FFC1 RID: 65473 RVA: 0x003967B4 File Offset: 0x003949B4
		public void BindToBanners(bool forceBind = false)
		{
			if ((!forceBind && this._isBoundToBanners) || string.IsNullOrEmpty(this.BannersPath))
			{
				return;
			}
			if (this.ItemTemplate == null)
			{
				this.ItemTemplate = new BannerItemTemplate();
			}
			this.DataSource = this.GetBanners(this.BannersPath, this.GetImageExtensions());
			this.DataBind();
			this._isBoundToBanners = true;
		}

		// Token: 0x0600FFC2 RID: 65474 RVA: 0x00396814 File Offset: 0x00394A14
		protected internal virtual List<BannerDataItem> GetBanners(string virtualPath, string allowedExtensions)
		{
			string path = base.MapPathSecure(virtualPath);
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (!directoryInfo.Exists)
			{
				return null;
			}
			List<BannerDataItem> list = new List<BannerDataItem>();
			FileInfo[] files = directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				if (allowedExtensions.Contains(fileInfo.Extension.ToLowerInvariant() + ","))
				{
					list.Add(new BannerDataItem
					{
						ImageUrl = virtualPath + fileInfo.Name,
						AlternateText = fileInfo.Name
					});
				}
			}
			return list;
		}

		// Token: 0x0600FFC3 RID: 65475 RVA: 0x003968B4 File Offset: 0x00394AB4
		protected internal virtual string GetImageExtensions()
		{
			return "*.jpg,*.jpeg,*.gif,*.png,*.bmp,";
		}

		// Token: 0x17004D26 RID: 19750
		// (get) Token: 0x0600FFC4 RID: 65476 RVA: 0x003968BC File Offset: 0x00394ABC
		// (set) Token: 0x0600FFC5 RID: 65477 RVA: 0x003968E5 File Offset: 0x00394AE5
		[ClientControlProperty]
		[Description("Specifies the type of rotator [how the rotator will render and what options the user will have for interacting with it on the client].")]
		[DefaultValue(RotatorType.AutomaticAdvance)]
		[Category("Behavior")]
		public RotatorType RotatorType
		{
			get
			{
				object obj = this.ViewState["RotatorType"];
				if (obj == null)
				{
					return RotatorType.AutomaticAdvance;
				}
				return (RotatorType)obj;
			}
			set
			{
				this.ViewState["RotatorType"] = value;
			}
		}

		// Token: 0x17004D27 RID: 19751
		// (get) Token: 0x0600FFC6 RID: 65478 RVA: 0x00396900 File Offset: 0x00394B00
		// (set) Token: 0x0600FFC7 RID: 65479 RVA: 0x00396929 File Offset: 0x00394B29
		[ClientControlProperty]
		[DefaultValue(RotatorScrollDirection.Left | RotatorScrollDirection.Right)]
		[Category("Behavior")]
		[Description("Specifies possible directions for scrolling rotator items.")]
		public RotatorScrollDirection ScrollDirection
		{
			get
			{
				object obj = this.ViewState["ScrollDirection"];
				if (obj == null)
				{
					return RotatorScrollDirection.Left | RotatorScrollDirection.Right;
				}
				return (RotatorScrollDirection)obj;
			}
			set
			{
				this.ViewState["ScrollDirection"] = value;
			}
		}

		// Token: 0x17004D28 RID: 19752
		// (get) Token: 0x0600FFC8 RID: 65480 RVA: 0x00396944 File Offset: 0x00394B44
		// (set) Token: 0x0600FFC9 RID: 65481 RVA: 0x00396971 File Offset: 0x00394B71
		[Category("Behavior")]
		[DefaultValue(500)]
		[ClientControlProperty]
		[Description("Specifies the speed in milliseconds for scrolling rotator items.")]
		public int ScrollDuration
		{
			get
			{
				object obj = this.ViewState["ScrollDuration"];
				if (obj == null)
				{
					return 500;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["ScrollDuration"] = value;
			}
		}

		// Token: 0x17004D29 RID: 19753
		// (get) Token: 0x0600FFCA RID: 65482 RVA: 0x0039698C File Offset: 0x00394B8C
		// (set) Token: 0x0600FFCB RID: 65483 RVA: 0x003969B8 File Offset: 0x00394BB8
		[Description("Specifies the index of the item, which will be shown first when the rotator loads.")]
		[Category("Behavior")]
		[DefaultValue(0)]
		[ClientControlProperty]
		public int InitialItemIndex
		{
			get
			{
				object obj = this.ViewState["InitialItemIndex"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				if (value >= -1)
				{
					this.ViewState["InitialItemIndex"] = value;
				}
			}
		}

		// Token: 0x17004D2A RID: 19754
		// (get) Token: 0x0600FFCC RID: 65484 RVA: 0x003969E1 File Offset: 0x00394BE1
		// (set) Token: 0x0600FFCD RID: 65485 RVA: 0x00396A02 File Offset: 0x00394C02
		[DefaultValue(false)]
		[Bindable(false)]
		[Category("Behavior")]
		[Description("Whether to persist the current item between postbacks.")]
		[ClientControlProperty]
		public bool PersistCurrentItemOnPostBack
		{
			get
			{
				return (bool)(this.ViewState["PersistCurrentItemOnPostBack"] ?? false);
			}
			set
			{
				this.ViewState["PersistCurrentItemOnPostBack"] = value;
			}
		}

		// Token: 0x17004D2B RID: 19755
		// (get) Token: 0x0600FFCE RID: 65486 RVA: 0x00396A1C File Offset: 0x00394C1C
		// (set) Token: 0x0600FFCF RID: 65487 RVA: 0x00396A49 File Offset: 0x00394C49
		[ClientControlProperty]
		[DefaultValue(2000)]
		[Category("Behavior")]
		[Description("Specifies the time in milliseconds each frame will display in automatic scrolling scenarios.")]
		public int FrameDuration
		{
			get
			{
				object obj = this.ViewState["FrameDuration"];
				if (obj == null)
				{
					return 2000;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["FrameDuration"] = value;
			}
		}

		// Token: 0x17004D2C RID: 19756
		// (get) Token: 0x0600FFD0 RID: 65488 RVA: 0x00396A64 File Offset: 0x00394C64
		// (set) Token: 0x0600FFD1 RID: 65489 RVA: 0x00396AA0 File Offset: 0x00394CA0
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		[Description("Specifies the default rotator item width.")]
		public Unit ItemWidth
		{
			get
			{
				if (this.ViewState["ItemWidth"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)this.ViewState["ItemWidth"];
			}
			set
			{
				this.ViewState["ItemWidth"] = value;
			}
		}

		// Token: 0x17004D2D RID: 19757
		// (get) Token: 0x0600FFD2 RID: 65490 RVA: 0x00396AB8 File Offset: 0x00394CB8
		// (set) Token: 0x0600FFD3 RID: 65491 RVA: 0x00396AF4 File Offset: 0x00394CF4
		[DefaultValue(typeof(Unit), "")]
		[Category("Appearance")]
		[Description("Specifies the default rotator item height.")]
		public Unit ItemHeight
		{
			get
			{
				if (this.ViewState["ItemHeight"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)this.ViewState["ItemHeight"];
			}
			set
			{
				this.ViewState["ItemHeight"] = value;
			}
		}

		// Token: 0x17004D2E RID: 19758
		// (get) Token: 0x0600FFD4 RID: 65492 RVA: 0x00396B0C File Offset: 0x00394D0C
		// (set) Token: 0x0600FFD5 RID: 65493 RVA: 0x00396B35 File Offset: 0x00394D35
		[ClientControlProperty]
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("")]
		public bool WrapFrames
		{
			get
			{
				object obj = this.ViewState["WrapFrames"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["WrapFrames"] = value;
			}
		}

		// Token: 0x17004D2F RID: 19759
		// (get) Token: 0x0600FFD6 RID: 65494 RVA: 0x00396B4D File Offset: 0x00394D4D
		// (set) Token: 0x0600FFD7 RID: 65495 RVA: 0x00396B6E File Offset: 0x00394D6E
		[DefaultValue(false)]
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

		// Token: 0x17004D30 RID: 19760
		// (get) Token: 0x0600FFD8 RID: 65496 RVA: 0x00396B86 File Offset: 0x00394D86
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public RadRotatorItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RadRotatorItemCollection(this);
					this._items.SetItemContainer(this);
				}
				return this._items;
			}
		}

		// Token: 0x17004D31 RID: 19761
		// (get) Token: 0x0600FFD9 RID: 65497 RVA: 0x00396BAE File Offset: 0x00394DAE
		// (set) Token: 0x0600FFDA RID: 65498 RVA: 0x00396BCE File Offset: 0x00394DCE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets or sets the HTML template that will be used when the Rotator is databound to ClientDataSource")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string ClientTemplate
		{
			get
			{
				return (this.ViewState["ClientTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x17004D32 RID: 19762
		// (get) Token: 0x0600FFDB RID: 65499 RVA: 0x00396BE1 File Offset: 0x00394DE1
		// (set) Token: 0x0600FFDC RID: 65500 RVA: 0x00396BE9 File Offset: 0x00394DE9
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(RadRotatorItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate ItemTemplate
		{
			get
			{
				return this._template;
			}
			set
			{
				this._template = value;
			}
		}

		// Token: 0x17004D33 RID: 19763
		// (get) Token: 0x0600FFDD RID: 65501 RVA: 0x00396BF2 File Offset: 0x00394DF2
		// (set) Token: 0x0600FFDE RID: 65502 RVA: 0x00396C12 File Offset: 0x00394E12
		[Themeable(false)]
		[Category("Behavior")]
		[Description("The URL to post to when an item is clicked.")]
		[DefaultValue("")]
		[UrlProperty("*.aspx")]
		public string PostBackUrl
		{
			get
			{
				return ((string)this.ViewState["PostBackUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x17004D34 RID: 19764
		// (get) Token: 0x0600FFDF RID: 65503 RVA: 0x00396C25 File Offset: 0x00394E25
		[Description("Slide Show mode animation settings")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		public AnimationSettings SlideShowAnimation
		{
			get
			{
				return this._slideShowAnimation;
			}
		}

		// Token: 0x17004D35 RID: 19765
		// (get) Token: 0x0600FFE0 RID: 65504 RVA: 0x00396C2D File Offset: 0x00394E2D
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The web service to be used for populating rotator items.")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				if (this._webServiceSettings == null)
				{
					this._webServiceSettings = new WebServiceSettings(this.ViewState);
				}
				return this._webServiceSettings;
			}
		}

		// Token: 0x17004D36 RID: 19766
		// (get) Token: 0x0600FFE1 RID: 65505 RVA: 0x00396C50 File Offset: 0x00394E50
		// (set) Token: 0x0600FFE2 RID: 65506 RVA: 0x00396C83 File Offset: 0x00394E83
		[DefaultValue(typeof(Unit), "200px")]
		[Description("Gets or sets the height of the Web server control. The default height is 200 pixels.")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public override Unit Height
		{
			get
			{
				if (!base.Height.IsEmpty)
				{
					return base.Height;
				}
				return Unit.Parse("200px", CultureInfo.InvariantCulture);
			}
			set
			{
				base.Height = value;
				this._heightSet = true;
			}
		}

		// Token: 0x17004D37 RID: 19767
		// (get) Token: 0x0600FFE3 RID: 65507 RVA: 0x00396C94 File Offset: 0x00394E94
		// (set) Token: 0x0600FFE4 RID: 65508 RVA: 0x00396CC7 File Offset: 0x00394EC7
		[NotifyParentProperty(true)]
		[Description("Gets or sets the width of the Web server control. The default width is 200 pixels.")]
		[DefaultValue(typeof(Unit), "200px")]
		[Category("Appearance")]
		public override Unit Width
		{
			get
			{
				if (!base.Width.IsEmpty)
				{
					return base.Width;
				}
				return Unit.Parse("200px", CultureInfo.InvariantCulture);
			}
			set
			{
				base.Width = value;
				this._widthSet = true;
			}
		}

		// Token: 0x17004D38 RID: 19768
		// (get) Token: 0x0600FFE5 RID: 65509 RVA: 0x00396CD7 File Offset: 0x00394ED7
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RotatorControlButtonsConfiguration ControlButtons
		{
			get
			{
				if (this._controlButtons == null)
				{
					this._controlButtons = new RotatorControlButtonsConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._controlButtons).TrackViewState();
					}
				}
				return this._controlButtons;
			}
		}

		// Token: 0x17004D39 RID: 19769
		// (get) Token: 0x0600FFE6 RID: 65510 RVA: 0x00396D08 File Offset: 0x00394F08
		// (set) Token: 0x0600FFE7 RID: 65511 RVA: 0x00396D31 File Offset: 0x00394F31
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether a postback to the server automatically occurs when the user interacts with the control.")]
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

		// Token: 0x17004D3A RID: 19770
		// (get) Token: 0x0600FFE8 RID: 65512 RVA: 0x00396D4C File Offset: 0x00394F4C
		// (set) Token: 0x0600FFE9 RID: 65513 RVA: 0x00396D75 File Offset: 0x00394F75
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether to pause the rotator scrolling when the mouse is over a roatator item")]
		[ClientControlProperty]
		[DefaultValue(true)]
		public bool PauseOnMouseOver
		{
			get
			{
				object obj = this.ViewState["PauseOnMouseOver"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["PauseOnMouseOver"] = value;
			}
		}

		// Token: 0x17004D3B RID: 19771
		// (get) Token: 0x0600FFEA RID: 65514 RVA: 0x00396D8D File Offset: 0x00394F8D
		// (set) Token: 0x0600FFEB RID: 65515 RVA: 0x00396DAE File Offset: 0x00394FAE
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether to randomize the order of display for the rotator items.")]
		[DefaultValue(false)]
		[ClientControlProperty]
		public bool EnableRandomOrder
		{
			get
			{
				return (bool)(this.ViewState["EnableRandomOrder"] ?? false);
			}
			set
			{
				this.ViewState["EnableRandomOrder"] = value;
			}
		}

		// Token: 0x17004D3C RID: 19772
		// (get) Token: 0x0600FFEC RID: 65516 RVA: 0x00396DC6 File Offset: 0x00394FC6
		// (set) Token: 0x0600FFED RID: 65517 RVA: 0x00396DE7 File Offset: 0x00394FE7
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Gets or sets a flag determining if drag-scrolling should be enabled.")]
		[DefaultValue(false)]
		public bool EnableDragScrolling
		{
			get
			{
				return (bool)(this.ViewState["EnableDragScrolling"] ?? false);
			}
			set
			{
				this.ViewState["EnableDragScrolling"] = value;
			}
		}

		// Token: 0x17004D3D RID: 19773
		// (get) Token: 0x0600FFEE RID: 65518 RVA: 0x00396DFF File Offset: 0x00394FFF
		// (set) Token: 0x0600FFEF RID: 65519 RVA: 0x00396E20 File Offset: 0x00395020
		[Description("Gets or sets the virtual path where the rotator will look for ads/banners (images) to play.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string BannersPath
		{
			get
			{
				return ((string)this.ViewState["BannersPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (!string.IsNullOrEmpty(text) && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["BannersPath"] = text;
			}
		}

		// Token: 0x17004D3E RID: 19774
		// (get) Token: 0x0600FFF0 RID: 65520 RVA: 0x00396E72 File Offset: 0x00395072
		// (set) Token: 0x0600FFF1 RID: 65521 RVA: 0x00396E93 File Offset: 0x00395093
		[DefaultValue(false)]
		[Description("Specifies whether the Rotator items created on the client-side should be cleared before data binding.")]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool AppendClientDataBoundItems
		{
			get
			{
				return (bool)(this.ViewState["AppendClientDataBoundItems"] ?? false);
			}
			set
			{
				this.ViewState["AppendClientDataBoundItems"] = value;
			}
		}

		// Token: 0x17004D3F RID: 19775
		// (get) Token: 0x0600FFF2 RID: 65522 RVA: 0x00396EAB File Offset: 0x003950AB
		// (set) Token: 0x0600FFF3 RID: 65523 RVA: 0x00396ECB File Offset: 0x003950CB
		[ClientPropertyName("itemClicking")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when an item is clicked.")]
		public string OnClientItemClicking
		{
			get
			{
				return ((string)this.ViewState["OnClientItemClicking"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemClicking"] = value;
			}
		}

		// Token: 0x17004D40 RID: 19776
		// (get) Token: 0x0600FFF4 RID: 65524 RVA: 0x00396EDE File Offset: 0x003950DE
		// (set) Token: 0x0600FFF5 RID: 65525 RVA: 0x00396EFE File Offset: 0x003950FE
		[ClientPropertyName("itemClicked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after an item is clicked.")]
		public string OnClientItemClicked
		{
			get
			{
				return ((string)this.ViewState["OnClientItemClicked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemClicked"] = value;
			}
		}

		// Token: 0x17004D41 RID: 19777
		// (get) Token: 0x0600FFF6 RID: 65526 RVA: 0x00396F11 File Offset: 0x00395111
		// (set) Token: 0x0600FFF7 RID: 65527 RVA: 0x00396F31 File Offset: 0x00395131
		[Description("The name of the javascript function called when the mouse hovers over an item.")]
		[ClientPropertyName("mouseOver")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientMouseOver
		{
			get
			{
				return ((string)this.ViewState["OnClientMouseOver"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientMouseOver"] = value;
			}
		}

		// Token: 0x17004D42 RID: 19778
		// (get) Token: 0x0600FFF8 RID: 65528 RVA: 0x00396F44 File Offset: 0x00395144
		// (set) Token: 0x0600FFF9 RID: 65529 RVA: 0x00396F64 File Offset: 0x00395164
		[ClientPropertyName("mouseOut")]
		[Description("The name of the javascript function called after the mouse leaves an item.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientMouseOut
		{
			get
			{
				return ((string)this.ViewState["OnClientMouseOut"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientMouseOut"] = value;
			}
		}

		// Token: 0x17004D43 RID: 19779
		// (get) Token: 0x0600FFFA RID: 65530 RVA: 0x00396F77 File Offset: 0x00395177
		// (set) Token: 0x0600FFFB RID: 65531 RVA: 0x00396F97 File Offset: 0x00395197
		[ClientPropertyName("itemShowing")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the javascript function called when an item is about to be shown.")]
		public string OnClientItemShowing
		{
			get
			{
				return ((string)this.ViewState["OnClientItemShowing"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemShowing"] = value;
			}
		}

		// Token: 0x17004D44 RID: 19780
		// (get) Token: 0x0600FFFC RID: 65532 RVA: 0x00396FAA File Offset: 0x003951AA
		// (set) Token: 0x0600FFFD RID: 65533 RVA: 0x00396FCA File Offset: 0x003951CA
		[Description("The name of the javascript function called after an item has been shown.")]
		[ClientPropertyName("itemShown")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientItemShown
		{
			get
			{
				return ((string)this.ViewState["OnClientItemShown"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemShown"] = value;
			}
		}

		// Token: 0x17004D45 RID: 19781
		// (get) Token: 0x0600FFFE RID: 65534 RVA: 0x00396FDD File Offset: 0x003951DD
		// (set) Token: 0x0600FFFF RID: 65535 RVA: 0x00396FFD File Offset: 0x003951FD
		[ClientControlEvent]
		[Description("The name of the javascript function called when the rotator is loaded on the client.")]
		[ClientPropertyName("load")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17004D46 RID: 19782
		// (get) Token: 0x06010000 RID: 65536 RVA: 0x00397010 File Offset: 0x00395210
		// (set) Token: 0x06010001 RID: 65537 RVA: 0x00397030 File Offset: 0x00395230
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("itemsRequesting")]
		[Description("The name of the javascript function called just before the request for items begins.")]
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

		// Token: 0x17004D47 RID: 19783
		// (get) Token: 0x06010002 RID: 65538 RVA: 0x00397043 File Offset: 0x00395243
		// (set) Token: 0x06010003 RID: 65539 RVA: 0x00397063 File Offset: 0x00395263
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("itemsRequested")]
		[Description("The name of the javascript function called after the request for items has completed.")]
		[Bindable(false)]
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

		// Token: 0x17004D48 RID: 19784
		// (get) Token: 0x06010004 RID: 65540 RVA: 0x00397076 File Offset: 0x00395276
		// (set) Token: 0x06010005 RID: 65541 RVA: 0x00397096 File Offset: 0x00395296
		[Description("The name of the javascript function called after the request for items has failed.")]
		[Bindable(false)]
		[Category("Client-side events")]
		[ClientPropertyName("itemsRequestFailed")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x17004D49 RID: 19785
		// (get) Token: 0x06010006 RID: 65542 RVA: 0x003970A9 File Offset: 0x003952A9
		// (set) Token: 0x06010007 RID: 65543 RVA: 0x003970C9 File Offset: 0x003952C9
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemDataBound")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when an item is databound on the client-side.")]
		[DefaultValue("")]
		public string OnClientItemDataBound
		{
			get
			{
				return ((string)this.ViewState["OnClientItemDataBound"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemDataBound"] = value;
			}
		}

		// Token: 0x17004D4A RID: 19786
		// (get) Token: 0x06010008 RID: 65544 RVA: 0x003970DC File Offset: 0x003952DC
		// (set) Token: 0x06010009 RID: 65545 RVA: 0x003970FC File Offset: 0x003952FC
		[Description("The name of the JavaScript function which is called when the Rotator has been populated with data.")]
		[DefaultValue("")]
		[ClientPropertyName("dataBound")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDataBound"] = value;
			}
		}

		// Token: 0x17004D4B RID: 19787
		// (get) Token: 0x0601000A RID: 65546 RVA: 0x0039710F File Offset: 0x0039530F
		// (set) Token: 0x0601000B RID: 65547 RVA: 0x0039712F File Offset: 0x0039532F
		[ClientPropertyName("templateDataBound")]
		[Description("The name of the JavaScript function called when the client template for an item is evaluated")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
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

		// Token: 0x140001DE RID: 478
		// (add) Token: 0x0601000C RID: 65548 RVA: 0x00397142 File Offset: 0x00395342
		// (remove) Token: 0x0601000D RID: 65549 RVA: 0x00397155 File Offset: 0x00395355
		[Category("Behavior")]
		[Description("Fired after a RadRotatorItem is created.")]
		public event RadRotatorEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadRotator.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRotator.ItemCreatedEvent, value);
			}
		}

		// Token: 0x140001DF RID: 479
		// (add) Token: 0x0601000E RID: 65550 RVA: 0x00397168 File Offset: 0x00395368
		// (remove) Token: 0x0601000F RID: 65551 RVA: 0x00397182 File Offset: 0x00395382
		[Description("Fired after an item is clicked.")]
		public event RadRotatorEventHandler ItemClick
		{
			add
			{
				this.AutoPostBack = true;
				base.Events.AddHandler(RadRotator.ItemClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRotator.ItemClickEvent, value);
			}
		}

		// Token: 0x140001E0 RID: 480
		// (add) Token: 0x06010010 RID: 65552 RVA: 0x00397195 File Offset: 0x00395395
		// (remove) Token: 0x06010011 RID: 65553 RVA: 0x003971A8 File Offset: 0x003953A8
		[Category("Behavior")]
		[Description("Fired after a RadRotatorItem is databound.")]
		public event RadRotatorEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadRotator.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRotator.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x06010012 RID: 65554 RVA: 0x003971BB File Offset: 0x003953BB
		// Note: this type is marked as 'beforefieldinit'.
		static RadRotator()
		{
			RadRotator.ItemDataBoundEvent = new object();
			RadRotator.ItemClickEvent = new object();
			RadRotator.ItemCreatedEvent = new object();
		}

		// Token: 0x04004868 RID: 18536
		private const string _defaultHeight = "200px";

		// Token: 0x04004869 RID: 18537
		private const string _defaultWidth = "200px";

		// Token: 0x0400486D RID: 18541
		private bool _isBoundToBanners;

		// Token: 0x0400486E RID: 18542
		private int _persistedItemIndex = -1;

		// Token: 0x0400486F RID: 18543
		private RadRotatorItemCollection _items;

		// Token: 0x04004870 RID: 18544
		private ITemplate _template;

		// Token: 0x04004871 RID: 18545
		private AnimationSettings _slideShowAnimation;

		// Token: 0x04004872 RID: 18546
		private WebServiceSettings _webServiceSettings;

		// Token: 0x04004873 RID: 18547
		private bool _heightSet;

		// Token: 0x04004874 RID: 18548
		private bool _widthSet;

		// Token: 0x04004875 RID: 18549
		private RotatorControlButtonsConfiguration _controlButtons;
	}
}
