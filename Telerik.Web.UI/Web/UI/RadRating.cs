using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020019CD RID: 6605
	[ClientScriptResource("Telerik.Web.UI.RadRating", "Telerik.Web.UI.Rating.RadRating.js")]
	[ToolboxData("<{0}:RadRating Runat=server></{0}:RadRating>")]
	[EmbeddedSkin("Rating")]
	[EmbeddedSkin("Rating", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadRating))]
	[DefaultEvent("OnRate")]
	[ValidationProperty("Value")]
	[Designer("Telerik.Web.Design.RadRatingDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxBitmap(typeof(RadRating), "Telerik.Web.UI.Rating.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[LightweightRendering]
	[RequiredScript(typeof(jQueryPlugins))]
	public class RadRating : RadDataBoundControl, IPostBackEventHandler
	{
		// Token: 0x17004CF3 RID: 19699
		// (get) Token: 0x0600FF03 RID: 65283 RVA: 0x00393C7D File Offset: 0x00391E7D
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17004CF4 RID: 19700
		// (get) Token: 0x0600FF04 RID: 65284 RVA: 0x00393C80 File Offset: 0x00391E80
		// (set) Token: 0x0600FF05 RID: 65285 RVA: 0x00393CA1 File Offset: 0x00391EA1
		[ClientPropertyName("_itemCount")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(5)]
		public int ItemCount
		{
			get
			{
				return (int)(this.ViewState["ItemCount"] ?? 5);
			}
			set
			{
				this.ViewState["ItemCount"] = value;
			}
		}

		// Token: 0x17004CF5 RID: 19701
		// (get) Token: 0x0600FF06 RID: 65286 RVA: 0x00393CB9 File Offset: 0x00391EB9
		// (set) Token: 0x0600FF07 RID: 65287 RVA: 0x00393CDE File Offset: 0x00391EDE
		[ClientPropertyName("_itemWidth")]
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		[Category("Appearance")]
		public Unit ItemWidth
		{
			get
			{
				return (Unit)(this.ViewState["ItemWidth"] ?? Unit.Empty);
			}
			set
			{
				if (value.Type == UnitType.Pixel)
				{
					this.ViewState["ItemWidth"] = value;
				}
			}
		}

		// Token: 0x17004CF6 RID: 19702
		// (get) Token: 0x0600FF08 RID: 65288 RVA: 0x00393D00 File Offset: 0x00391F00
		// (set) Token: 0x0600FF09 RID: 65289 RVA: 0x00393D25 File Offset: 0x00391F25
		[ClientControlProperty]
		[Category("Appearance")]
		[ClientPropertyName("_itemHeight")]
		[DefaultValue(typeof(Unit), "")]
		public Unit ItemHeight
		{
			get
			{
				return (Unit)(this.ViewState["ItemHeight"] ?? Unit.Empty);
			}
			set
			{
				if (value.Type == UnitType.Pixel)
				{
					this.ViewState["ItemHeight"] = value;
				}
			}
		}

		// Token: 0x17004CF7 RID: 19703
		// (get) Token: 0x0600FF0A RID: 65290 RVA: 0x00393D47 File Offset: 0x00391F47
		// (set) Token: 0x0600FF0B RID: 65291 RVA: 0x00393D6D File Offset: 0x00391F6D
		[Category("Behavior")]
		[DefaultValue(typeof(decimal), "0")]
		public decimal Value
		{
			get
			{
				return (decimal)(this.ViewState["Value"] ?? 0m);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17004CF8 RID: 19704
		// (get) Token: 0x0600FF0C RID: 65292 RVA: 0x00393D85 File Offset: 0x00391F85
		// (set) Token: 0x0600FF0D RID: 65293 RVA: 0x00393D92 File Offset: 0x00391F92
		[Bindable(true, BindingDirection.TwoWay)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[TypeConverter(typeof(DecimalConverter))]
		[Description("The current value, boxed in object")]
		public object DbValue
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = RadRating.ConvertDataValueToDecimal(value);
			}
		}

		// Token: 0x17004CF9 RID: 19705
		// (get) Token: 0x0600FF0E RID: 65294 RVA: 0x00393DA0 File Offset: 0x00391FA0
		// (set) Token: 0x0600FF0F RID: 65295 RVA: 0x00393DC1 File Offset: 0x00391FC1
		[DefaultValue(RatingSelectionMode.Continuous)]
		[Category("Behavior")]
		[ClientControlProperty]
		public RatingSelectionMode SelectionMode
		{
			get
			{
				return (RatingSelectionMode)(this.ViewState["SelectionMode"] ?? RatingSelectionMode.Continuous);
			}
			set
			{
				this.ViewState["SelectionMode"] = value;
			}
		}

		// Token: 0x17004CFA RID: 19706
		// (get) Token: 0x0600FF10 RID: 65296 RVA: 0x00393DD9 File Offset: 0x00391FD9
		// (set) Token: 0x0600FF11 RID: 65297 RVA: 0x00393DFA File Offset: 0x00391FFA
		[DefaultValue(RatingPrecision.Item)]
		[Category("Behavior")]
		[ClientControlProperty]
		public RatingPrecision Precision
		{
			get
			{
				return (RatingPrecision)(this.ViewState["Precision"] ?? RatingPrecision.Item);
			}
			set
			{
				this.ViewState["Precision"] = value;
			}
		}

		// Token: 0x17004CFB RID: 19707
		// (get) Token: 0x0600FF12 RID: 65298 RVA: 0x00393E12 File Offset: 0x00392012
		// (set) Token: 0x0600FF13 RID: 65299 RVA: 0x00393E33 File Offset: 0x00392033
		[Category("Layout")]
		[DefaultValue(Orientation.Horizontal)]
		[ClientControlProperty]
		public Orientation Orientation
		{
			get
			{
				return (Orientation)(this.ViewState["Orientation"] ?? Orientation.Horizontal);
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x17004CFC RID: 19708
		// (get) Token: 0x0600FF14 RID: 65300 RVA: 0x00393E4B File Offset: 0x0039204B
		// (set) Token: 0x0600FF15 RID: 65301 RVA: 0x00393E6C File Offset: 0x0039206C
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Layout")]
		public bool IsDirectionReversed
		{
			get
			{
				return (bool)(this.ViewState["IsDirectionReversed"] ?? false);
			}
			set
			{
				this.ViewState["IsDirectionReversed"] = value;
			}
		}

		// Token: 0x17004CFD RID: 19709
		// (get) Token: 0x0600FF16 RID: 65302 RVA: 0x00393E84 File Offset: 0x00392084
		// (set) Token: 0x0600FF17 RID: 65303 RVA: 0x00393EA5 File Offset: 0x003920A5
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool EnableToolTips
		{
			get
			{
				return (bool)(this.ViewState["EnableToolTips"] ?? true);
			}
			set
			{
				this.ViewState["EnableToolTips"] = value;
			}
		}

		// Token: 0x17004CFE RID: 19710
		// (get) Token: 0x0600FF18 RID: 65304 RVA: 0x00393EBD File Offset: 0x003920BD
		// (set) Token: 0x0600FF19 RID: 65305 RVA: 0x00393EDE File Offset: 0x003920DE
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[ClientPropertyName("_autoPostBack")]
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

		// Token: 0x17004CFF RID: 19711
		// (get) Token: 0x0600FF1A RID: 65306 RVA: 0x00393EF6 File Offset: 0x003920F6
		// (set) Token: 0x0600FF1B RID: 65307 RVA: 0x00393F17 File Offset: 0x00392117
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientPropertyName("readOnly")]
		public bool ReadOnly
		{
			get
			{
				return (bool)(this.ViewState["ReadOnly"] ?? false);
			}
			set
			{
				this.ViewState["ReadOnly"] = value;
			}
		}

		// Token: 0x17004D00 RID: 19712
		// (get) Token: 0x0600FF1C RID: 65308 RVA: 0x00393F2F File Offset: 0x0039212F
		// (set) Token: 0x0600FF1D RID: 65309 RVA: 0x00393F50 File Offset: 0x00392150
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets/Sets a value indicating whether the DataBound items should be appended to the Rating Items collection, or the collection should be cleared before creating the DataBound items.")]
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

		// Token: 0x17004D01 RID: 19713
		// (get) Token: 0x0600FF1E RID: 65310 RVA: 0x00393F68 File Offset: 0x00392168
		[DefaultValue(null)]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RatingItemBinding ItemBinding
		{
			get
			{
				if (this._itemBinding == null)
				{
					this._itemBinding = new RatingItemBinding();
				}
				return this._itemBinding;
			}
		}

		// Token: 0x17004D02 RID: 19714
		// (get) Token: 0x0600FF1F RID: 65311 RVA: 0x00393F83 File Offset: 0x00392183
		// (set) Token: 0x0600FF20 RID: 65312 RVA: 0x00393FA3 File Offset: 0x003921A3
		[ClientPropertyName("load")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
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

		// Token: 0x17004D03 RID: 19715
		// (get) Token: 0x0600FF21 RID: 65313 RVA: 0x00393FB6 File Offset: 0x003921B6
		// (set) Token: 0x0600FF22 RID: 65314 RVA: 0x00393FD6 File Offset: 0x003921D6
		[ClientPropertyName("rating")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientRating
		{
			get
			{
				return ((string)this.ViewState["OnClientRating"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientRating"] = value;
			}
		}

		// Token: 0x17004D04 RID: 19716
		// (get) Token: 0x0600FF23 RID: 65315 RVA: 0x00393FE9 File Offset: 0x003921E9
		// (set) Token: 0x0600FF24 RID: 65316 RVA: 0x00394009 File Offset: 0x00392209
		[ClientControlEvent]
		[ClientPropertyName("rated")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnClientRated
		{
			get
			{
				return ((string)this.ViewState["OnClientRated"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientRated"] = value;
			}
		}

		// Token: 0x140001DB RID: 475
		// (add) Token: 0x0600FF25 RID: 65317 RVA: 0x0039401C File Offset: 0x0039221C
		// (remove) Token: 0x0600FF26 RID: 65318 RVA: 0x0039402F File Offset: 0x0039222F
		[Description("Fired after a rating item (star) is clicked.")]
		[Category("Action")]
		public virtual event EventHandler Rate
		{
			add
			{
				base.Events.AddHandler(RadRating.RateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRating.RateEvent, value);
			}
		}

		// Token: 0x140001DC RID: 476
		// (add) Token: 0x0600FF27 RID: 65319 RVA: 0x00394042 File Offset: 0x00392242
		// (remove) Token: 0x0600FF28 RID: 65320 RVA: 0x00394055 File Offset: 0x00392255
		[Description("Fired after a rating item (star) is data bound.")]
		[Category("Action")]
		public virtual event EventHandler<RatingEventArgs> ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadRating.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRating.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x140001DD RID: 477
		// (add) Token: 0x0600FF29 RID: 65321 RVA: 0x00394068 File Offset: 0x00392268
		// (remove) Token: 0x0600FF2A RID: 65322 RVA: 0x0039407B File Offset: 0x0039227B
		[Category("Action")]
		[Description("Fired after a rating item (star) is data bound.")]
		public virtual event EventHandler<RatingEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadRating.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRating.ItemCreatedEvent, value);
			}
		}

		// Token: 0x0600FF2B RID: 65323 RVA: 0x00394090 File Offset: 0x00392290
		[Category("Action")]
		protected virtual void OnRate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadRating.RateEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600FF2C RID: 65324 RVA: 0x003940C0 File Offset: 0x003922C0
		[Category("Action")]
		protected virtual void OnItemDataBound(RatingEventArgs e)
		{
			EventHandler<RatingEventArgs> eventHandler = (EventHandler<RatingEventArgs>)base.Events[RadRating.ItemDataBoundEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600FF2D RID: 65325 RVA: 0x003940F0 File Offset: 0x003922F0
		protected virtual void OnItemCreated(RatingEventArgs e)
		{
			EventHandler<RatingEventArgs> eventHandler = (EventHandler<RatingEventArgs>)base.Events[RadRating.ItemCreatedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x17004D05 RID: 19717
		// (get) Token: 0x0600FF2E RID: 65326 RVA: 0x0039411E File Offset: 0x0039231E
		[MergableProperty(false)]
		[DefaultValue(null)]
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadRatingItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new RadRatingItemCollection(this);
				}
				return this.items;
			}
		}

		// Token: 0x17004D06 RID: 19718
		// (get) Token: 0x0600FF2F RID: 65327 RVA: 0x0039413C File Offset: 0x0039233C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadRatingItem SelectedItem
		{
			get
			{
				RadRatingItemCollection selectedItems = this.SelectedItems;
				if (selectedItems.Count > 0)
				{
					return selectedItems[0];
				}
				return null;
			}
		}

		// Token: 0x17004D07 RID: 19719
		// (get) Token: 0x0600FF30 RID: 65328 RVA: 0x00394164 File Offset: 0x00392364
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadRatingItemCollection SelectedItems
		{
			get
			{
				RadRatingItemCollection radRatingItemCollection = new RadRatingItemCollection(this);
				RadRatingItemCollection radRatingItemCollection2 = this.Items;
				int count = radRatingItemCollection2.Count;
				if (count > 0)
				{
					RadRatingItem radRatingItem = this.FindItemByValue(this.Value);
					if (radRatingItem != null)
					{
						int index = radRatingItem.Index;
						if (this.SelectionMode == RatingSelectionMode.Continuous)
						{
							bool isDirectionReversed = this.IsDirectionReversed;
							for (int i = 0; i <= index; i++)
							{
								radRatingItemCollection.Add(radRatingItemCollection2[isDirectionReversed ? (count - 1 - i) : i]);
							}
						}
						else
						{
							radRatingItemCollection.Add(radRatingItem);
						}
					}
				}
				return radRatingItemCollection;
			}
		}

		// Token: 0x0600FF31 RID: 65329 RVA: 0x003941E8 File Offset: 0x003923E8
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				return;
			}
			if (!base.DesignMode)
			{
				this.PrepareForDataBinding();
				this.BindToEnumerableData(data);
			}
		}

		// Token: 0x0600FF32 RID: 65330 RVA: 0x0039420C File Offset: 0x0039240C
		public void BindToEnumerableData(IEnumerable dataSource)
		{
			foreach (object dataObject in dataSource)
			{
				this.BindItem(this.Items, dataObject);
			}
		}

		// Token: 0x0600FF33 RID: 65331 RVA: 0x00394264 File Offset: 0x00392464
		protected void PrepareForDataBinding()
		{
			if (!this.AppendDataBoundItems)
			{
				this.Items.Clear();
				base.ClearChildViewState();
			}
			this.TrackViewState();
		}

		// Token: 0x0600FF34 RID: 65332 RVA: 0x00394288 File Offset: 0x00392488
		private RadRatingItem BindItem(RadRatingItemCollection items, object dataObject)
		{
			RadRatingItem radRatingItem = new RadRatingItem();
			if (this.ItemBinding.ValueField.Length > 0)
			{
				object obj = DataBinder.Eval(dataObject, this.ItemBinding.ValueField);
				if (obj != DBNull.Value && obj != null)
				{
					radRatingItem.Value = RadRating.ConvertDataValueToDecimal(obj);
				}
			}
			else if (dataObject != DBNull.Value && dataObject != null)
			{
				decimal value = 0m;
				if (RadRating.TryParseDecimalFromNumber(dataObject, out value) || RadRating.TryParseDecimalFromString(dataObject.ToString(), out value))
				{
					radRatingItem.Value = value;
				}
			}
			if (this.ItemBinding.ToolTipField.Length > 0)
			{
				object obj2 = DataBinder.Eval(dataObject, this.ItemBinding.ToolTipField);
				if (obj2 != DBNull.Value && obj2 != null)
				{
					if (string.IsNullOrEmpty(this.ItemBinding.ToolTipFormatString))
					{
						radRatingItem.ToolTip = obj2.ToString();
					}
					else
					{
						radRatingItem.ToolTip = string.Format(this.ItemBinding.ToolTipFormatString, obj2);
					}
				}
			}
			items.Add(radRatingItem);
			this.RaiseItemDataBound(radRatingItem);
			return radRatingItem;
		}

		// Token: 0x0600FF35 RID: 65333 RVA: 0x00394381 File Offset: 0x00392581
		private void RaiseItemDataBound(RadRatingItem item)
		{
			this.OnItemDataBound(new RatingEventArgs(item));
		}

		// Token: 0x0600FF36 RID: 65334 RVA: 0x0039438F File Offset: 0x0039258F
		internal void InitializeItem(RadRatingItem item)
		{
			this.OnItemCreated(new RatingEventArgs(item));
		}

		// Token: 0x17004D08 RID: 19720
		// (get) Token: 0x0600FF37 RID: 65335 RVA: 0x0039439D File Offset: 0x0039259D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17004D09 RID: 19721
		// (get) Token: 0x0600FF38 RID: 65336 RVA: 0x003943A1 File Offset: 0x003925A1
		protected override string CssClassFormatString
		{
			get
			{
				return "RadRating RadRating_{0}" + ((!base.IsEnabled) ? " rrtDisabled" : "");
			}
		}

		// Token: 0x0600FF39 RID: 65337 RVA: 0x003943C4 File Offset: 0x003925C4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.originalEnabled = this.Enabled;
			this.Enabled = true;
			base.AddAttributesToRender(writer);
			this.Enabled = this.originalEnabled;
			if (base.DesignMode)
			{
				bool flag = this.Orientation == Orientation.Horizontal;
				int count = this.Items.Count;
				int num = (count > 0) ? count : this.ItemCount;
				int num2 = this.ItemWidth.IsEmpty ? 20 : ((int)this.ItemWidth.Value);
				int num3 = this.ItemHeight.IsEmpty ? 20 : ((int)this.ItemHeight.Value);
				int num4 = (flag ? (num * num2) : num2) + 4;
				int num5 = ((!flag) ? (num * num3) : num3) + 4;
				writer.AddStyleAttribute("width", string.Format("{0}px", num4));
				writer.AddStyleAttribute("height", string.Format("{0}px", num5));
			}
		}

		// Token: 0x0600FF3A RID: 65338 RVA: 0x003944C8 File Offset: 0x003926C8
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			bool flag = this.Orientation == Orientation.Vertical;
			bool isDirectionReversed = this.IsDirectionReversed;
			bool flag2 = this.SelectionMode == RatingSelectionMode.Continuous;
			RatingPrecision precision = this.Precision;
			bool enableToolTips = this.EnableToolTips;
			bool flag3 = base.IsEnabled && !this.ReadOnly;
			string text = string.Format("{0} {1}", flag ? "rrtVertical" : "", isDirectionReversed ? "rrtReversed" : "");
			text = string.Format("{0} {1}", text.Trim(), (precision == RatingPrecision.Exact) ? "rrtExact" : ((precision == RatingPrecision.Half) ? "rrtHalf" : "rrtItem"));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text.Trim());
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			Unit itemHeight = this.ItemHeight;
			bool flag4 = !itemHeight.IsEmpty;
			string value = flag4 ? string.Format("{0}px", itemHeight.Value) : string.Empty;
			Unit itemWidth = this.ItemWidth;
			bool flag5 = !itemWidth.IsEmpty;
			string value2 = flag5 ? string.Format("{0}px", itemWidth.Value) : string.Empty;
			HtmlTextWriterStyle key = flag ? HtmlTextWriterStyle.Height : HtmlTextWriterStyle.Width;
			decimal value3 = this.Value;
			decimal value4 = Math.Ceiling(value3);
			RadRatingItem radRatingItem = this.FindItemByValue(value4);
			int num = (radRatingItem != null) ? radRatingItem.Index : ((int)value4 - 1);
			decimal d = value3 - Math.Floor(value3);
			int value5 = flag ? ((flag4 ? ((int)itemHeight.Value) : 20) - 2) : ((flag5 ? ((int)itemWidth.Value) : 20) - 2);
			int num2 = (d > 0m) ? ((int)(Math.Abs(d - (isDirectionReversed ? 1 : 0)) * value5)) : -1;
			RadRatingItemCollection radRatingItemCollection = this.Items;
			bool flag6 = radRatingItemCollection.Count > 0;
			int num3 = flag6 ? radRatingItemCollection.Count : this.ItemCount;
			int num4 = isDirectionReversed ? (num3 - 1) : 0;
			int num5 = isDirectionReversed ? -1 : 1;
			int num6 = num4;
			while (num6 >= 0 && num6 < num3)
			{
				bool flag7 = (flag2 && num6 < num) || num6 == num;
				string text2 = string.Format("{0} {1}", flag7 ? "rrtSelected" : string.Empty, flag6 ? radRatingItemCollection[num6].CssClass : string.Empty).Trim();
				if (flag6)
				{
					RadRatingItem radRatingItem2 = radRatingItemCollection[num6];
					if (!string.IsNullOrEmpty(radRatingItem2.ImageUrl))
					{
						text2 = string.Format("{0} {1}", text2, "rrtCustomItemImages");
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, text2);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				if (flag4)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, value);
				}
				if (flag5)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, value2);
				}
				decimal num7 = ++num6;
				string value6 = string.Empty;
				if (flag6)
				{
					RadRatingItem radRatingItem3 = radRatingItemCollection[num6];
					num7 = radRatingItem3.Value;
					string value7;
					value6 = (value7 = radRatingItem3.ImageUrl);
					string selectedImageUrl = radRatingItem3.SelectedImageUrl;
					if (!string.IsNullOrEmpty(selectedImageUrl))
					{
						if (isDirectionReversed)
						{
							value7 = selectedImageUrl;
						}
						else
						{
							value6 = selectedImageUrl;
						}
					}
					if (!string.IsNullOrEmpty(value7))
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, value7);
						writer.AddStyleAttribute("background-position", "0px 0px");
					}
				}
				if (enableToolTips)
				{
					string text3 = value3.ToString();
					if (flag3)
					{
						text3 = num7.ToString();
						if (flag6)
						{
							string toolTip = radRatingItemCollection[num6].ToolTip;
							text3 = ((!string.IsNullOrEmpty(toolTip)) ? toolTip : text3);
						}
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Title, text3);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				bool flag8 = (flag7 && !isDirectionReversed) || (!flag7 && isDirectionReversed);
				if (flag4 && (!flag || flag8))
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, value);
				}
				if (flag5 && (flag || flag8))
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, value2);
				}
				if (num6 == num && num2 > -1)
				{
					writer.AddStyleAttribute(key, string.Format("{0}px", num2));
				}
				if (!string.IsNullOrEmpty(value6))
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, value6);
					writer.AddStyleAttribute("background-position", "0px 0px");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(num7.ToString());
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
				num6 += num5;
			}
			writer.RenderEndTag();
			this.RenderTrialMessage(writer);
		}

		// Token: 0x0600FF3B RID: 65339 RVA: 0x0039495C File Offset: 0x00392B5C
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			decimal num = this.Value;
			if (this.Items.Count == 0)
			{
				int itemCount = this.ItemCount;
				if (num < 0m)
				{
					num = 0m;
				}
				else if (num > itemCount)
				{
					num = itemCount;
				}
			}
			this.Value = Math.Round(num, 1, MidpointRounding.AwayFromZero);
			if (base.ScriptManager.LoadScriptsBeforeUI && this.Page.Form != null && this.RegisterWithScriptManager)
			{
				string text = string.Format("Telerik.Web.UI.RadRating._preInitialize(\"{0}\",\"{1}\");", this.ClientID, (int)this.Orientation);
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadRating), this.ClientID + text, text, true);
			}
		}

		// Token: 0x0600FF3C RID: 65340 RVA: 0x00394A25 File Offset: 0x00392C25
		protected override void RenderTrialMessage(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600FF3D RID: 65341 RVA: 0x00394A28 File Offset: 0x00392C28
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			((IStateManager)this.ItemBinding).LoadViewState(array[1]);
			if (array[2] == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(array[2]);
		}

		// Token: 0x0600FF3E RID: 65342 RVA: 0x00394A74 File Offset: 0x00392C74
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ItemBinding).SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			};
		}

		// Token: 0x0600FF3F RID: 65343 RVA: 0x00394AB0 File Offset: 0x00392CB0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ItemBinding).TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x0600FF40 RID: 65344 RVA: 0x00394AD0 File Offset: 0x00392CD0
		public RadRatingItem FindItemByValue(decimal value)
		{
			foreach (object obj in this.Items)
			{
				RadRatingItem radRatingItem = (RadRatingItem)obj;
				if (radRatingItem.Value == value)
				{
					return radRatingItem;
				}
			}
			return null;
		}

		// Token: 0x0600FF41 RID: 65345 RVA: 0x00394B38 File Offset: 0x00392D38
		private static decimal ConvertDataValueToDecimal(object value)
		{
			decimal result = 0m;
			if (RadRating.TryParseDecimalFromNumber(value, out result))
			{
				return result;
			}
			if (!RadRating.TryParseDecimalFromString(value.ToString(), out result))
			{
				throw new FormatException("The string was not recognized as a valid format.");
			}
			return result;
		}

		// Token: 0x0600FF42 RID: 65346 RVA: 0x00394B74 File Offset: 0x00392D74
		private static bool TryParseDecimalFromString(string sValue, out decimal ratingValue)
		{
			ratingValue = 0m;
			bool flag = false;
			if (!string.IsNullOrEmpty(sValue))
			{
				flag = decimal.TryParse(sValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out ratingValue);
				if (!flag)
				{
					flag = decimal.TryParse(sValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out ratingValue);
				}
			}
			return flag;
		}

		// Token: 0x0600FF43 RID: 65347 RVA: 0x00394BC0 File Offset: 0x00392DC0
		private static bool TryParseDecimalFromNumber(object value, out decimal parsedValue)
		{
			parsedValue = 0m;
			if (value == null || value == DBNull.Value)
			{
				return true;
			}
			if (value is double || value is float || value is decimal || value is int || value is short || value is ushort || value is byte || value is sbyte || value is long || value is ulong || value is uint)
			{
				parsedValue = Convert.ToDecimal(value, NumberFormatInfo.InvariantInfo);
				return true;
			}
			if (value is bool)
			{
				parsedValue = (((bool)value) ? 1m : 0m);
				return true;
			}
			return false;
		}

		// Token: 0x0600FF44 RID: 65348 RVA: 0x00394C78 File Offset: 0x00392E78
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			decimal value = 0m;
			bool flag = decimal.TryParse((string)clientState["value"], NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out value);
			if (flag)
			{
				this.Value = value;
			}
			this.ReadOnly = (bool)clientState["readOnly"];
		}

		// Token: 0x17004D0A RID: 19722
		// (get) Token: 0x0600FF45 RID: 65349 RVA: 0x00394CD8 File Offset: 0x00392ED8
		// (set) Token: 0x0600FF46 RID: 65350 RVA: 0x00394D18 File Offset: 0x00392F18
		private bool raisePostDataChangedEvent
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					return (bool)httpContext.Items["postDataChangedFlag" + this.UniqueID];
				}
				return this._postDataChangedFlag;
			}
			set
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					this._postDataChangedFlag = value;
					return;
				}
				httpContext.Items["postDataChangedFlag" + this.UniqueID] = value;
			}
		}

		// Token: 0x0600FF47 RID: 65351 RVA: 0x00394D58 File Offset: 0x00392F58
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			decimal value = this.Value;
			bool flag = base.LoadPostData(postDataKey, postCollection);
			if (value != this.Value)
			{
				flag = true;
			}
			this.raisePostDataChangedEvent = flag;
			return flag;
		}

		// Token: 0x0600FF48 RID: 65352 RVA: 0x00394D8D File Offset: 0x00392F8D
		protected override void RaisePostDataChangedEvent()
		{
			this.OnRate(EventArgs.Empty);
		}

		// Token: 0x0600FF49 RID: 65353 RVA: 0x00394D9C File Offset: 0x00392F9C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			if (this.Items.Count > 0)
			{
				this.ItemCount = this.Items.Count;
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				List<JavaScriptConverter> converters = new List<JavaScriptConverter>
				{
					new RatingItemConverter()
				};
				javaScriptSerializer.RegisterConverters(converters);
				descriptor.AddScriptProperty("itemData", javaScriptSerializer.Serialize(this.Items));
			}
			base.DescribeComponent(descriptor);
			base.DescribeRenderingMode(descriptor);
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			decimal value = this.Value;
			if (value != 0m)
			{
				descriptor.AddProperty("value", value);
			}
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			descriptor.AddProperty("_preInitializeComplete", base.ScriptManager.LoadScriptsBeforeUI && this.Page.Form != null && this.RegisterWithScriptManager);
		}

		// Token: 0x0600FF4A RID: 65354 RVA: 0x00394E90 File Offset: 0x00393090
		public void RaisePostBackEvent(string eventArgument)
		{
			if (!this.raisePostDataChangedEvent)
			{
				this.OnRate(EventArgs.Empty);
			}
		}

		// Token: 0x0600FF4B RID: 65355 RVA: 0x00394EA8 File Offset: 0x003930A8
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "_autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<bool>(descriptor, "enableToolTips", this.EnableToolTips, true);
			base.DescribeProperty<bool>(descriptor, "isDirectionReversed", this.IsDirectionReversed, false);
			base.DescribeProperty<int>(descriptor, "_itemCount", this.ItemCount, 5);
			base.DescribeProperty<string>(descriptor, "_itemHeight", this.ItemHeight.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "_itemWidth", this.ItemWidth.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<Orientation>(descriptor, "orientation", this.Orientation, Orientation.Horizontal);
			base.DescribeProperty<RatingPrecision>(descriptor, "precision", this.Precision, RatingPrecision.Item);
			base.DescribeProperty<bool>(descriptor, "readOnly", this.ReadOnly, false);
			base.DescribeProperty<RatingSelectionMode>(descriptor, "selectionMode", this.SelectionMode, RatingSelectionMode.Continuous);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600FF4C RID: 65356 RVA: 0x00394F9C File Offset: 0x0039319C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "rated", this.OnClientRated);
			RadDataBoundControl.DescribeEvent(descriptor, "rating", this.OnClientRating);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0600FF4E RID: 65358 RVA: 0x00394FD8 File Offset: 0x003931D8
		// Note: this type is marked as 'beforefieldinit'.
		static RadRating()
		{
			RadRating.RateEvent = new object();
			RadRating.ItemDataBoundEvent = new object();
			RadRating.ItemCreatedEvent = new object();
		}

		// Token: 0x04004853 RID: 18515
		internal const int ItemOuterWidth = 20;

		// Token: 0x04004854 RID: 18516
		internal const int ItemOuterHeight = 20;

		// Token: 0x04004855 RID: 18517
		internal const int ItemVerticalMargin = 2;

		// Token: 0x04004856 RID: 18518
		internal const int ItemHorizontalMargin = 2;

		// Token: 0x04004857 RID: 18519
		private RadRatingItemCollection items;

		// Token: 0x04004858 RID: 18520
		private RatingItemBinding _itemBinding;

		// Token: 0x0400485C RID: 18524
		private bool originalEnabled = true;

		// Token: 0x0400485D RID: 18525
		private bool _postDataChangedFlag;
	}
}
