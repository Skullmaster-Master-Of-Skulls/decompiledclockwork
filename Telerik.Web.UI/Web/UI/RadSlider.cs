using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FBB RID: 4027
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadSlider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxBitmap(typeof(RadSlider), "Telerik.Web.UI.Slider.png")]
	[LightweightRendering]
	[DefaultProperty("Value")]
	[Designer("Telerik.Web.Design.RadSliderDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[DefaultEvent("OnValueChanged")]
	[RequiredScript(typeof(ResizeExtender))]
	[RequiredScript(typeof(AnimationFramework))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadSlider", "Telerik.Web.UI.Slider.RadSliderScripts.js")]
	[EmbeddedSkin("Slider", "Default")]
	[RequiredScript(typeof(jQueryPlugins))]
	[EmbeddedSkin("Slider")]
	public class RadSlider : ControlItemContainer, IPostBackEventHandler
	{
		// Token: 0x17003124 RID: 12580
		// (get) Token: 0x06009B40 RID: 39744 RVA: 0x00228DC4 File Offset: 0x00226FC4
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003125 RID: 12581
		// (get) Token: 0x06009B41 RID: 39745 RVA: 0x00228DC7 File Offset: 0x00226FC7
		// (set) Token: 0x06009B42 RID: 39746 RVA: 0x00228DED File Offset: 0x00226FED
		[Category("Behavior")]
		[DefaultValue(typeof(decimal), "0")]
		[SimplePersistenceSetting]
		public decimal Value
		{
			get
			{
				return (decimal)(this.ViewState["Value"] ?? this.MinimumValue);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17003126 RID: 12582
		// (get) Token: 0x06009B43 RID: 39747 RVA: 0x00228E05 File Offset: 0x00227005
		// (set) Token: 0x06009B44 RID: 39748 RVA: 0x00228E2B File Offset: 0x0022702B
		[Category("Behavior")]
		[DefaultValue(typeof(decimal), "0")]
		public decimal SelectedRegionStartValue
		{
			get
			{
				return (decimal)(this.ViewState["SelectedRegionStartValue"] ?? this.MinimumValue);
			}
			set
			{
				this.ViewState["SelectedRegionStartValue"] = value;
			}
		}

		// Token: 0x17003127 RID: 12583
		// (get) Token: 0x06009B45 RID: 39749 RVA: 0x00228E43 File Offset: 0x00227043
		// (set) Token: 0x06009B46 RID: 39750 RVA: 0x00228E50 File Offset: 0x00227050
		[Description("The current value, boxed in object")]
		[Bindable(true, BindingDirection.TwoWay)]
		[Browsable(false)]
		[TypeConverter(typeof(DecimalConverter))]
		[NotifyParentProperty(true)]
		public object DbValue
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = RadSlider.ConvertDataValueToDecimal(value);
			}
		}

		// Token: 0x17003128 RID: 12584
		// (get) Token: 0x06009B47 RID: 39751 RVA: 0x00228E5E File Offset: 0x0022705E
		// (set) Token: 0x06009B48 RID: 39752 RVA: 0x00228E84 File Offset: 0x00227084
		[DefaultValue(typeof(decimal), "0")]
		[Category("Behavior")]
		[SimplePersistenceSetting]
		public decimal SelectionStart
		{
			get
			{
				return (decimal)(this.ViewState["SelectionStart"] ?? this.MinimumValue);
			}
			set
			{
				this.ViewState["SelectionStart"] = value;
			}
		}

		// Token: 0x17003129 RID: 12585
		// (get) Token: 0x06009B49 RID: 39753 RVA: 0x00228E9C File Offset: 0x0022709C
		// (set) Token: 0x06009B4A RID: 39754 RVA: 0x00228EC2 File Offset: 0x002270C2
		[Category("Behavior")]
		[SimplePersistenceSetting]
		[DefaultValue(typeof(decimal), "0")]
		public decimal SelectionEnd
		{
			get
			{
				return (decimal)(this.ViewState["SelectionEnd"] ?? this.MinimumValue);
			}
			set
			{
				this.ViewState["SelectionEnd"] = value;
			}
		}

		// Token: 0x1700312A RID: 12586
		// (get) Token: 0x06009B4B RID: 39755 RVA: 0x00228EDA File Offset: 0x002270DA
		// (set) Token: 0x06009B4C RID: 39756 RVA: 0x00228EFB File Offset: 0x002270FB
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool IsSelectionRangeEnabled
		{
			get
			{
				return (bool)(this.ViewState["IsSelectionRangeEnabled"] ?? false);
			}
			set
			{
				this.ViewState["IsSelectionRangeEnabled"] = value;
			}
		}

		// Token: 0x1700312B RID: 12587
		// (get) Token: 0x06009B4D RID: 39757 RVA: 0x00228F13 File Offset: 0x00227113
		// (set) Token: 0x06009B4E RID: 39758 RVA: 0x00228F34 File Offset: 0x00227134
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool EnableDragRange
		{
			get
			{
				return (bool)(this.ViewState["EnableDragRange"] ?? false);
			}
			set
			{
				this.ViewState["EnableDragRange"] = value;
			}
		}

		// Token: 0x1700312C RID: 12588
		// (get) Token: 0x06009B4F RID: 39759 RVA: 0x00228F4C File Offset: 0x0022714C
		// (set) Token: 0x06009B50 RID: 39760 RVA: 0x00228F6D File Offset: 0x0022716D
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientControlProperty]
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

		// Token: 0x1700312D RID: 12589
		// (get) Token: 0x06009B51 RID: 39761 RVA: 0x00228F85 File Offset: 0x00227185
		// (set) Token: 0x06009B52 RID: 39762 RVA: 0x00228FA6 File Offset: 0x002271A6
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool LiveDrag
		{
			get
			{
				return (bool)(this.ViewState["LiveDrag"] ?? true);
			}
			set
			{
				this.ViewState["LiveDrag"] = value;
			}
		}

		// Token: 0x1700312E RID: 12590
		// (get) Token: 0x06009B53 RID: 39763 RVA: 0x00228FBE File Offset: 0x002271BE
		// (set) Token: 0x06009B54 RID: 39764 RVA: 0x00228FDF File Offset: 0x002271DF
		[ClientControlProperty]
		[DefaultValue(SliderItemType.None)]
		[Category("Behavior")]
		[Description("Specifies the type of the RadSliderItems in the slider.")]
		public new SliderItemType ItemType
		{
			get
			{
				return (SliderItemType)(this.ViewState["ItemType"] ?? SliderItemType.None);
			}
			set
			{
				this.ViewState["ItemType"] = value;
			}
		}

		// Token: 0x1700312F RID: 12591
		// (get) Token: 0x06009B55 RID: 39765 RVA: 0x00228FF7 File Offset: 0x002271F7
		// (set) Token: 0x06009B56 RID: 39766 RVA: 0x00229018 File Offset: 0x00227218
		[Description("Specifies the position of the track in the slider.")]
		[DefaultValue(SliderTrackPosition.Center)]
		[Category("Layout")]
		[ClientControlProperty]
		public SliderTrackPosition TrackPosition
		{
			get
			{
				return (SliderTrackPosition)(this.ViewState["TrackPosition"] ?? SliderTrackPosition.Center);
			}
			set
			{
				this.ViewState["TrackPosition"] = value;
			}
		}

		// Token: 0x17003130 RID: 12592
		// (get) Token: 0x06009B57 RID: 39767 RVA: 0x00229030 File Offset: 0x00227230
		// (set) Token: 0x06009B58 RID: 39768 RVA: 0x00229051 File Offset: 0x00227251
		[Category("Layout")]
		[ClientControlProperty]
		[DefaultValue(Orientation.Horizontal)]
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

		// Token: 0x17003131 RID: 12593
		// (get) Token: 0x06009B59 RID: 39769 RVA: 0x00229069 File Offset: 0x00227269
		// (set) Token: 0x06009B5A RID: 39770 RVA: 0x0022908F File Offset: 0x0022728F
		[DefaultValue(typeof(decimal), "1")]
		[Category("Behavior")]
		public decimal SmallChange
		{
			get
			{
				return (decimal)(this.ViewState["SmallChange"] ?? 1m);
			}
			set
			{
				this.ViewState["SmallChange"] = value;
			}
		}

		// Token: 0x17003132 RID: 12594
		// (get) Token: 0x06009B5B RID: 39771 RVA: 0x002290A7 File Offset: 0x002272A7
		// (set) Token: 0x06009B5C RID: 39772 RVA: 0x002290CD File Offset: 0x002272CD
		[Category("Behavior")]
		[DefaultValue(typeof(decimal), "0")]
		public decimal LargeChange
		{
			get
			{
				return (decimal)(this.ViewState["LargeChange"] ?? 0m);
			}
			set
			{
				this.ViewState["LargeChange"] = value;
			}
		}

		// Token: 0x17003133 RID: 12595
		// (get) Token: 0x06009B5D RID: 39773 RVA: 0x002290E5 File Offset: 0x002272E5
		// (set) Token: 0x06009B5E RID: 39774 RVA: 0x00229107 File Offset: 0x00227307
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(100)]
		public int AnimationDuration
		{
			get
			{
				return (int)(this.ViewState["AnimationDuration"] ?? 100);
			}
			set
			{
				this.ViewState["AnimationDuration"] = value;
			}
		}

		// Token: 0x17003134 RID: 12596
		// (get) Token: 0x06009B5F RID: 39775 RVA: 0x00229120 File Offset: 0x00227320
		// (set) Token: 0x06009B60 RID: 39776 RVA: 0x00229158 File Offset: 0x00227358
		[Obsolete("Please use the Width and Height properties of the RadSlider.")]
		[Browsable(false)]
		public int Length
		{
			get
			{
				return int.Parse(((this.Orientation == Orientation.Horizontal) ? this.Width : this.Height).Value.ToString());
			}
			set
			{
				this.length = value;
			}
		}

		// Token: 0x17003135 RID: 12597
		// (get) Token: 0x06009B61 RID: 39777 RVA: 0x00229161 File Offset: 0x00227361
		// (set) Token: 0x06009B62 RID: 39778 RVA: 0x0022919C File Offset: 0x0022739C
		[ClientPropertyName("_width")]
		[Category("Layout")]
		[ClientControlProperty]
		public override Unit Width
		{
			get
			{
				object obj;
				if ((obj = this.ViewState["Width"]) == null)
				{
					obj = ((this.Orientation == Orientation.Horizontal) ? Unit.Pixel(200) : Unit.Pixel(22));
				}
				return (Unit)obj;
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17003136 RID: 12598
		// (get) Token: 0x06009B63 RID: 39779 RVA: 0x002291B4 File Offset: 0x002273B4
		// (set) Token: 0x06009B64 RID: 39780 RVA: 0x002291F0 File Offset: 0x002273F0
		[Category("Layout")]
		[ClientControlProperty]
		[ClientPropertyName("_height")]
		public override Unit Height
		{
			get
			{
				object obj;
				if ((obj = this.ViewState["Height"]) == null)
				{
					obj = ((this.Orientation == Orientation.Vertical) ? Unit.Pixel(200) : Unit.Pixel(22));
				}
				return (Unit)obj;
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17003137 RID: 12599
		// (get) Token: 0x06009B65 RID: 39781 RVA: 0x00229208 File Offset: 0x00227408
		// (set) Token: 0x06009B66 RID: 39782 RVA: 0x00229229 File Offset: 0x00227429
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
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

		// Token: 0x17003138 RID: 12600
		// (get) Token: 0x06009B67 RID: 39783 RVA: 0x00229241 File Offset: 0x00227441
		// (set) Token: 0x06009B68 RID: 39784 RVA: 0x00229267 File Offset: 0x00227467
		[Category("Behavior")]
		[DefaultValue(typeof(decimal), "0")]
		public decimal MinimumValue
		{
			get
			{
				return (decimal)(this.ViewState["MinimumValue"] ?? 0m);
			}
			set
			{
				this.ViewState["MinimumValue"] = value;
			}
		}

		// Token: 0x17003139 RID: 12601
		// (get) Token: 0x06009B69 RID: 39785 RVA: 0x0022927F File Offset: 0x0022747F
		// (set) Token: 0x06009B6A RID: 39786 RVA: 0x002292A6 File Offset: 0x002274A6
		[DefaultValue(typeof(decimal), "100")]
		[Category("Behavior")]
		public decimal MaximumValue
		{
			get
			{
				return (decimal)(this.ViewState["MaximumValue"] ?? 100m);
			}
			set
			{
				this.ViewState["MaximumValue"] = value;
			}
		}

		// Token: 0x1700313A RID: 12602
		// (get) Token: 0x06009B6B RID: 39787 RVA: 0x002292BE File Offset: 0x002274BE
		// (set) Token: 0x06009B6C RID: 39788 RVA: 0x002292DF File Offset: 0x002274DF
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(true)]
		public bool TrackMouseWheel
		{
			get
			{
				return (bool)(this.ViewState["TrackMouseWheel"] ?? true);
			}
			set
			{
				this.ViewState["TrackMouseWheel"] = value;
			}
		}

		// Token: 0x1700313B RID: 12603
		// (get) Token: 0x06009B6D RID: 39789 RVA: 0x002292F7 File Offset: 0x002274F7
		// (set) Token: 0x06009B6E RID: 39790 RVA: 0x00229318 File Offset: 0x00227518
		[Category("Appearance")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool ShowDragHandle
		{
			get
			{
				return (bool)(this.ViewState["ShowDragHandle"] ?? true);
			}
			set
			{
				this.ViewState["ShowDragHandle"] = value;
			}
		}

		// Token: 0x1700313C RID: 12604
		// (get) Token: 0x06009B6F RID: 39791 RVA: 0x00229330 File Offset: 0x00227530
		// (set) Token: 0x06009B70 RID: 39792 RVA: 0x00229351 File Offset: 0x00227551
		[DefaultValue(true)]
		[Category("Appearance")]
		[ClientControlProperty]
		public bool ShowDecreaseHandle
		{
			get
			{
				return (bool)(this.ViewState["ShowDecreaseHandle"] ?? true);
			}
			set
			{
				this.ViewState["ShowDecreaseHandle"] = value;
			}
		}

		// Token: 0x1700313D RID: 12605
		// (get) Token: 0x06009B71 RID: 39793 RVA: 0x00229369 File Offset: 0x00227569
		// (set) Token: 0x06009B72 RID: 39794 RVA: 0x0022938A File Offset: 0x0022758A
		[Category("Appearance")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool ShowIncreaseHandle
		{
			get
			{
				return (bool)(this.ViewState["ShowIncreaseHandle"] ?? true);
			}
			set
			{
				this.ViewState["ShowIncreaseHandle"] = value;
			}
		}

		// Token: 0x1700313E RID: 12606
		// (get) Token: 0x06009B73 RID: 39795 RVA: 0x002293A2 File Offset: 0x002275A2
		// (set) Token: 0x06009B74 RID: 39796 RVA: 0x002293C2 File Offset: 0x002275C2
		[Localizable(true)]
		[ClientControlProperty]
		[DefaultValue("Decrease")]
		[ClientPropertyName("_decreaseText")]
		public string DecreaseText
		{
			get
			{
				return ((string)this.ViewState["DecreaseText"]) ?? "Decrease";
			}
			set
			{
				this.ViewState["DecreaseText"] = value;
			}
		}

		// Token: 0x1700313F RID: 12607
		// (get) Token: 0x06009B75 RID: 39797 RVA: 0x002293D5 File Offset: 0x002275D5
		// (set) Token: 0x06009B76 RID: 39798 RVA: 0x002293F5 File Offset: 0x002275F5
		[ClientControlProperty]
		[ClientPropertyName("_increaseText")]
		[Localizable(true)]
		[DefaultValue("Increase")]
		public string IncreaseText
		{
			get
			{
				return ((string)this.ViewState["IncreaseText"]) ?? "Increase";
			}
			set
			{
				this.ViewState["IncreaseText"] = value;
			}
		}

		// Token: 0x17003140 RID: 12608
		// (get) Token: 0x06009B77 RID: 39799 RVA: 0x00229408 File Offset: 0x00227608
		// (set) Token: 0x06009B78 RID: 39800 RVA: 0x00229428 File Offset: 0x00227628
		[ClientPropertyName("_dragText")]
		[DefaultValue("Drag")]
		[Localizable(true)]
		[ClientControlProperty]
		public string DragText
		{
			get
			{
				return ((string)this.ViewState["DragText"]) ?? "Drag";
			}
			set
			{
				this.ViewState["DragText"] = value;
			}
		}

		// Token: 0x17003141 RID: 12609
		// (get) Token: 0x06009B79 RID: 39801 RVA: 0x0022943B File Offset: 0x0022763B
		// (set) Token: 0x06009B7A RID: 39802 RVA: 0x0022945C File Offset: 0x0022765C
		[ClientPropertyName("_enableServerSideRendering")]
		[DefaultValue(false)]
		[ClientControlProperty]
		public bool EnableServerSideRendering
		{
			get
			{
				return (bool)(this.ViewState["EnableServerSideRendering"] ?? false);
			}
			set
			{
				this.ViewState["EnableServerSideRendering"] = value;
			}
		}

		// Token: 0x17003142 RID: 12610
		// (get) Token: 0x06009B7B RID: 39803 RVA: 0x00229474 File Offset: 0x00227674
		// (set) Token: 0x06009B7C RID: 39804 RVA: 0x00229495 File Offset: 0x00227695
		[Description("Specifies the interaction mode of the slider thumbs.")]
		[DefaultValue(SliderThumbsInteractionMode.Free)]
		[Category("Behavior")]
		[ClientControlProperty]
		public SliderThumbsInteractionMode ThumbsInteractionMode
		{
			get
			{
				return (SliderThumbsInteractionMode)(this.ViewState["ThumbsInteractionMode"] ?? SliderThumbsInteractionMode.Free);
			}
			set
			{
				this.ViewState["ThumbsInteractionMode"] = value;
			}
		}

		// Token: 0x17003143 RID: 12611
		// (get) Token: 0x06009B7D RID: 39805 RVA: 0x002294AD File Offset: 0x002276AD
		// (set) Token: 0x06009B7E RID: 39806 RVA: 0x002294CE File Offset: 0x002276CE
		[Category("Behavior")]
		[Description("Gets/Sets a value indicating whether the DataBound items should be appended to the Slider Items collection, or the collection should be cleared before creating the DataBound items.")]
		[DefaultValue(false)]
		public override bool AppendDataBoundItems
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

		// Token: 0x17003144 RID: 12612
		// (get) Token: 0x06009B7F RID: 39807 RVA: 0x002294E6 File Offset: 0x002276E6
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SliderItemBinding ItemBinding
		{
			get
			{
				if (this._itemBinding == null)
				{
					this._itemBinding = new SliderItemBinding();
				}
				return this._itemBinding;
			}
		}

		// Token: 0x17003145 RID: 12613
		// (get) Token: 0x06009B80 RID: 39808 RVA: 0x00229501 File Offset: 0x00227701
		// (set) Token: 0x06009B81 RID: 39809 RVA: 0x00229509 File Offset: 0x00227709
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientLoad property instead.", false)]
		[Browsable(false)]
		public virtual string OnClientLoaded
		{
			get
			{
				return this.OnClientLoad;
			}
			set
			{
				this.OnClientLoad = value;
			}
		}

		// Token: 0x17003146 RID: 12614
		// (get) Token: 0x06009B82 RID: 39810 RVA: 0x00229512 File Offset: 0x00227712
		// (set) Token: 0x06009B83 RID: 39811 RVA: 0x00229532 File Offset: 0x00227732
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnClientLoad
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

		// Token: 0x17003147 RID: 12615
		// (get) Token: 0x06009B84 RID: 39812 RVA: 0x00229545 File Offset: 0x00227745
		// (set) Token: 0x06009B85 RID: 39813 RVA: 0x00229565 File Offset: 0x00227765
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("slideStart")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public virtual string OnClientSlideStart
		{
			get
			{
				return ((string)this.ViewState["OnClientSlideStart"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSlideStart"] = value;
			}
		}

		// Token: 0x17003148 RID: 12616
		// (get) Token: 0x06009B86 RID: 39814 RVA: 0x00229578 File Offset: 0x00227778
		// (set) Token: 0x06009B87 RID: 39815 RVA: 0x00229598 File Offset: 0x00227798
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("slide")]
		[Category("Client-side events")]
		[ClientControlEvent]
		public virtual string OnClientSlide
		{
			get
			{
				return ((string)this.ViewState["OnClientSlide"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSlide"] = value;
			}
		}

		// Token: 0x17003149 RID: 12617
		// (get) Token: 0x06009B88 RID: 39816 RVA: 0x002295AB File Offset: 0x002277AB
		// (set) Token: 0x06009B89 RID: 39817 RVA: 0x002295CB File Offset: 0x002277CB
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("slideEnd")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public virtual string OnClientSlideEnd
		{
			get
			{
				return ((string)this.ViewState["OnClientSlideEnd"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSlideEnd"] = value;
			}
		}

		// Token: 0x1700314A RID: 12618
		// (get) Token: 0x06009B8A RID: 39818 RVA: 0x002295DE File Offset: 0x002277DE
		// (set) Token: 0x06009B8B RID: 39819 RVA: 0x002295FE File Offset: 0x002277FE
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("slideRangeStart")]
		[Category("Client-side events")]
		public virtual string OnClientSlideRangeStart
		{
			get
			{
				return ((string)this.ViewState["OnClientSlideRangeStart"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSlideRangeStart"] = value;
			}
		}

		// Token: 0x1700314B RID: 12619
		// (get) Token: 0x06009B8C RID: 39820 RVA: 0x00229611 File Offset: 0x00227811
		// (set) Token: 0x06009B8D RID: 39821 RVA: 0x00229631 File Offset: 0x00227831
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("slideRange")]
		public virtual string OnClientSlideRange
		{
			get
			{
				return ((string)this.ViewState["OnClientSlideRange"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSlideRange"] = value;
			}
		}

		// Token: 0x1700314C RID: 12620
		// (get) Token: 0x06009B8E RID: 39822 RVA: 0x00229644 File Offset: 0x00227844
		// (set) Token: 0x06009B8F RID: 39823 RVA: 0x00229664 File Offset: 0x00227864
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("slideRangeEnd")]
		public virtual string OnClientSlideRangeEnd
		{
			get
			{
				return ((string)this.ViewState["OnClientSlideRangeEnd"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSlideRangeEnd"] = value;
			}
		}

		// Token: 0x1700314D RID: 12621
		// (get) Token: 0x06009B90 RID: 39824 RVA: 0x00229677 File Offset: 0x00227877
		// (set) Token: 0x06009B91 RID: 39825 RVA: 0x0022967F File Offset: 0x0022787F
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientValueChanged property instead.", false)]
		[Browsable(false)]
		public virtual string OnClientValueChange
		{
			get
			{
				return this.OnClientValueChanged;
			}
			set
			{
				this.OnClientValueChanged = value;
			}
		}

		// Token: 0x1700314E RID: 12622
		// (get) Token: 0x06009B92 RID: 39826 RVA: 0x00229688 File Offset: 0x00227888
		// (set) Token: 0x06009B93 RID: 39827 RVA: 0x002296A8 File Offset: 0x002278A8
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("valueChanged")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnClientValueChanged
		{
			get
			{
				return ((string)this.ViewState["OnClientValueChanged"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientValueChanged"] = value;
			}
		}

		// Token: 0x1700314F RID: 12623
		// (get) Token: 0x06009B94 RID: 39828 RVA: 0x002296BB File Offset: 0x002278BB
		// (set) Token: 0x06009B95 RID: 39829 RVA: 0x002296C3 File Offset: 0x002278C3
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientValueChanging property instead.", false)]
		[Browsable(false)]
		public virtual string OnClientBeforeValueChange
		{
			get
			{
				return this.OnClientValueChanging;
			}
			set
			{
				this.OnClientValueChanging = value;
			}
		}

		// Token: 0x17003150 RID: 12624
		// (get) Token: 0x06009B96 RID: 39830 RVA: 0x002296CC File Offset: 0x002278CC
		// (set) Token: 0x06009B97 RID: 39831 RVA: 0x002296EC File Offset: 0x002278EC
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("valueChanging")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public virtual string OnClientValueChanging
		{
			get
			{
				return ((string)this.ViewState["OnClientValueChanging"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientValueChanging"] = value;
			}
		}

		// Token: 0x17003151 RID: 12625
		// (get) Token: 0x06009B98 RID: 39832 RVA: 0x002296FF File Offset: 0x002278FF
		// (set) Token: 0x06009B99 RID: 39833 RVA: 0x0022971F File Offset: 0x0022791F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemsCreated")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public virtual string OnClientItemsCreated
		{
			get
			{
				return ((string)this.ViewState["OnClientItemsCreated"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemsCreated"] = value;
			}
		}

		// Token: 0x17003152 RID: 12626
		// (get) Token: 0x06009B9A RID: 39834 RVA: 0x00229732 File Offset: 0x00227932
		// (set) Token: 0x06009B9B RID: 39835 RVA: 0x00229752 File Offset: 0x00227952
		[ClientPropertyName("itemDataBinding")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnClientItemDataBinding
		{
			get
			{
				return ((string)this.ViewState["OnClientItemDataBinding"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemDataBinding"] = value;
			}
		}

		// Token: 0x17003153 RID: 12627
		// (get) Token: 0x06009B9C RID: 39836 RVA: 0x00229765 File Offset: 0x00227965
		// (set) Token: 0x06009B9D RID: 39837 RVA: 0x00229785 File Offset: 0x00227985
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemDataBound")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public virtual string OnClientItemDataBound
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

		// Token: 0x17003154 RID: 12628
		// (get) Token: 0x06009B9E RID: 39838 RVA: 0x00229798 File Offset: 0x00227998
		// (set) Token: 0x06009B9F RID: 39839 RVA: 0x002297B8 File Offset: 0x002279B8
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("dataBound")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public virtual string OnClientDataBound
		{
			get
			{
				return ((string)this.ViewState["OnClientDataBound"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientDataBound"] = value;
			}
		}

		// Token: 0x14000174 RID: 372
		// (add) Token: 0x06009BA0 RID: 39840 RVA: 0x002297CB File Offset: 0x002279CB
		// (remove) Token: 0x06009BA1 RID: 39841 RVA: 0x002297DE File Offset: 0x002279DE
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(RadSlider.eventValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadSlider.eventValueChanged, value);
			}
		}

		// Token: 0x06009BA2 RID: 39842 RVA: 0x002297F1 File Offset: 0x002279F1
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
		}

		// Token: 0x06009BA3 RID: 39843 RVA: 0x002297F4 File Offset: 0x002279F4
		[Category("Action")]
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadSlider.eventValueChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06009BA4 RID: 39844 RVA: 0x00229822 File Offset: 0x00227A22
		private void PerformValidation()
		{
			if (!this.CausesValidation)
			{
				return;
			}
			this.Page.Validate(this.ValidationGroup);
		}

		// Token: 0x06009BA5 RID: 39845 RVA: 0x0022983E File Offset: 0x00227A3E
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnItemDataBound(new RadSliderItemEventArgs((RadSliderItem)item));
		}

		// Token: 0x06009BA6 RID: 39846 RVA: 0x00229854 File Offset: 0x00227A54
		protected virtual void OnItemDataBound(RadSliderItemEventArgs e)
		{
			RadSliderItemEventHandler radSliderItemEventHandler = (RadSliderItemEventHandler)base.Events[RadSlider.itemDataBoundEvent];
			if (radSliderItemEventHandler != null)
			{
				radSliderItemEventHandler(this, e);
			}
		}

		// Token: 0x17003155 RID: 12629
		// (get) Token: 0x06009BA7 RID: 39847 RVA: 0x00229882 File Offset: 0x00227A82
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public RadSliderItemCollection Items
		{
			get
			{
				return (RadSliderItemCollection)base.Children;
			}
		}

		// Token: 0x17003156 RID: 12630
		// (get) Token: 0x06009BA8 RID: 39848 RVA: 0x00229890 File Offset: 0x00227A90
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadSliderItem SelectedItem
		{
			get
			{
				RadSliderItemCollection selectedItems = this.SelectedItems;
				if (selectedItems.Count > 0)
				{
					return selectedItems[0];
				}
				return null;
			}
		}

		// Token: 0x17003157 RID: 12631
		// (get) Token: 0x06009BA9 RID: 39849 RVA: 0x002298B8 File Offset: 0x00227AB8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadSliderItemCollection SelectedItems
		{
			get
			{
				RadSliderItemCollection radSliderItemCollection = new RadSliderItemCollection(this, false);
				if (this.ItemType == SliderItemType.Item)
				{
					if (this.IsSelectionRangeEnabled)
					{
						radSliderItemCollection.Add(this.Items[Convert.ToInt32(this.SelectionStart, NumberFormatInfo.InvariantInfo)]);
						radSliderItemCollection.Add(this.Items[Convert.ToInt32(this.SelectionEnd, NumberFormatInfo.InvariantInfo)]);
					}
					else
					{
						radSliderItemCollection.Add(this.Items[Convert.ToInt32(this.Value, NumberFormatInfo.InvariantInfo)]);
					}
				}
				return radSliderItemCollection;
			}
		}

		// Token: 0x17003158 RID: 12632
		// (get) Token: 0x06009BAA RID: 39850 RVA: 0x00229954 File Offset: 0x00227B54
		// (set) Token: 0x06009BAB RID: 39851 RVA: 0x00229978 File Offset: 0x00227B78
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SelectedValue
		{
			get
			{
				RadSliderItem selectedItem = this.SelectedItem;
				if (selectedItem != null)
				{
					return selectedItem.Value;
				}
				return string.Empty;
			}
			set
			{
				if (this.ItemType != SliderItemType.Item)
				{
					return;
				}
				RadSliderItem selectedItem = this.SelectedItem;
				if (selectedItem != null && selectedItem.Value == value)
				{
					return;
				}
				bool isSelectionRangeEnabled = this.IsSelectionRangeEnabled;
				foreach (object obj in this.Items)
				{
					RadSliderItem radSliderItem = (RadSliderItem)obj;
					if (radSliderItem.Value == value)
					{
						if (isSelectionRangeEnabled)
						{
							this.SelectionStart = radSliderItem.Index;
							break;
						}
						this.Value = radSliderItem.Index;
						break;
					}
				}
			}
		}

		// Token: 0x17003159 RID: 12633
		// (get) Token: 0x06009BAC RID: 39852 RVA: 0x00229A30 File Offset: 0x00227C30
		// (set) Token: 0x06009BAD RID: 39853 RVA: 0x00229A50 File Offset: 0x00227C50
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{
				RadSliderItem selectedItem = this.SelectedItem;
				if (selectedItem != null)
				{
					return selectedItem.Index;
				}
				return -1;
			}
			set
			{
				if (this.ItemType != SliderItemType.Item)
				{
					return;
				}
				RadSliderItem selectedItem = this.SelectedItem;
				if (selectedItem != null && selectedItem.Index == value)
				{
					return;
				}
				if (this.IsSelectionRangeEnabled)
				{
					this.SelectionStart = value;
					return;
				}
				this.Value = value;
			}
		}

		// Token: 0x06009BAE RID: 39854 RVA: 0x00229A9C File Offset: 0x00227C9C
		private void RenderItems(HtmlTextWriter writer)
		{
			foreach (object obj in this.Items)
			{
				RadSliderItem radSliderItem = (RadSliderItem)obj;
				radSliderItem.RenderControl(writer);
			}
		}

		// Token: 0x06009BAF RID: 39855 RVA: 0x00229AF8 File Offset: 0x00227CF8
		private RadSliderItem FindItemByIndex(string itemIndex)
		{
			if (string.IsNullOrEmpty(itemIndex))
			{
				return null;
			}
			int index = Convert.ToInt32(itemIndex);
			return this.Items[index];
		}

		// Token: 0x06009BB0 RID: 39856 RVA: 0x00229B24 File Offset: 0x00227D24
		protected override void RaiseItemCreated(ControlItem item)
		{
			RadSliderItemEventHandler radSliderItemEventHandler = (RadSliderItemEventHandler)base.Events[RadSlider.itemCreatedEvent];
			if (radSliderItemEventHandler != null)
			{
				radSliderItemEventHandler(this, new RadSliderItemEventArgs((RadSliderItem)item));
			}
		}

		// Token: 0x06009BB1 RID: 39857 RVA: 0x00229B5C File Offset: 0x00227D5C
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadSliderItemCollection(this);
		}

		// Token: 0x06009BB2 RID: 39858 RVA: 0x00229B64 File Offset: 0x00227D64
		protected internal override ControlItem CreateItem()
		{
			return new RadSliderItem();
		}

		// Token: 0x06009BB3 RID: 39859 RVA: 0x00229B6B File Offset: 0x00227D6B
		protected override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			if ((data == null && this.DataSource == null) || this.ItemType != SliderItemType.Item)
			{
				return;
			}
			if (!base.DesignMode)
			{
				base.PrepareForDataBinding();
				this.BindToEnumerableData(data);
			}
		}

		// Token: 0x06009BB4 RID: 39860 RVA: 0x00229BA0 File Offset: 0x00227DA0
		public void BindToEnumerableData(IEnumerable dataSource)
		{
			foreach (object dataObject in dataSource)
			{
				this.BindItem(this.Items, dataObject);
			}
		}

		// Token: 0x06009BB5 RID: 39861 RVA: 0x00229BF8 File Offset: 0x00227DF8
		private RadSliderItem BindItem(RadSliderItemCollection items, object dataObject)
		{
			RadSliderItem radSliderItem = new RadSliderItem();
			if (this.ItemBinding.ValueField.Length > 0)
			{
				object obj = DataBinder.Eval(dataObject, this.ItemBinding.ValueField);
				radSliderItem.Value = obj.ToString();
			}
			if (this.ItemBinding.ToolTipField.Length > 0)
			{
				object obj2 = DataBinder.Eval(dataObject, this.ItemBinding.ToolTipField);
				radSliderItem.ToolTip = obj2.ToString();
			}
			if (this.ItemBinding.TextField.Length > 0)
			{
				object obj3 = DataBinder.Eval(dataObject, this.ItemBinding.TextField);
				radSliderItem.Text = obj3.ToString();
			}
			items.Add(radSliderItem);
			this.RaiseItemDataBound(radSliderItem);
			return radSliderItem;
		}

		// Token: 0x06009BB6 RID: 39862 RVA: 0x00229CB0 File Offset: 0x00227EB0
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (this.Items.Count > 0)
			{
				if (this.ItemType == SliderItemType.Item)
				{
					this.MinimumValue = 0m;
					this.SmallChange = 1m;
					this.MaximumValue = 2 * this.Items.VisibleItems.Count;
				}
				else
				{
					this.Items.Clear();
				}
			}
			if (this.length > -1)
			{
				if (this.Orientation == Orientation.Horizontal)
				{
					this.Width = this.length;
				}
				else
				{
					this.Height = this.length;
				}
			}
			decimal maximumValue = this.MaximumValue;
			decimal minimumValue = this.MinimumValue;
			if (this.IsSelectionRangeEnabled)
			{
				this.Value = this.GetPropertyDefaultValue("Value");
				this.SelectionStart = Math.Max(minimumValue, Math.Min(maximumValue, this.SelectionStart));
				this.SelectionEnd = Math.Max(minimumValue, Math.Min(maximumValue, this.SelectionEnd));
				return;
			}
			this.SelectionStart = this.GetPropertyDefaultValue("SelectionStart");
			this.SelectionEnd = this.GetPropertyDefaultValue("SelectionEnd");
			this.Value = Math.Max(minimumValue, Math.Min(maximumValue, this.Value));
		}

		// Token: 0x06009BB7 RID: 39863 RVA: 0x00229DE4 File Offset: 0x00227FE4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.originalEnabled = this.Enabled;
			this.Enabled = true;
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			base.AddAttributesToRender(writer);
			this.Enabled = this.originalEnabled;
			this.AccessKey = accessKey;
			this.TabIndex = tabIndex;
		}

		// Token: 0x06009BB8 RID: 39864 RVA: 0x00229E48 File Offset: 0x00228048
		protected override Style CreateControlStyle()
		{
			Style style = base.CreateControlStyle();
			style.Width = this.Width;
			style.Height = this.Height;
			return style;
		}

		// Token: 0x1700315A RID: 12634
		// (get) Token: 0x06009BB9 RID: 39865 RVA: 0x00229E75 File Offset: 0x00228075
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700315B RID: 12635
		// (get) Token: 0x06009BBA RID: 39866 RVA: 0x00229E79 File Offset: 0x00228079
		protected override string CssClassFormatString
		{
			get
			{
				return "RadSlider RadSlider_{0}";
			}
		}

		// Token: 0x06009BBB RID: 39867 RVA: 0x00229E80 File Offset: 0x00228080
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			this.RenderTrialMessage(writer);
			BaseClass.RenderVersionStamp(writer);
			if (this.EnableServerSideRendering || base.DesignMode)
			{
				if (base.DesignMode)
				{
					writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				}
				this.RenderWrapper(writer);
			}
		}

		// Token: 0x06009BBC RID: 39868 RVA: 0x00229EC0 File Offset: 0x002280C0
		protected void RenderWrapper(HtmlTextWriter writer)
		{
			bool flag = this.Orientation == Orientation.Horizontal;
			bool showDecreaseHandle = this.ShowDecreaseHandle;
			string clientID = this.ClientID;
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("RadSliderWrapper_{0}", clientID));
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, Unit.Pixel((int)this.Height.Value).ToString());
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetSliderCssClass());
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderIncreaseDecreaseHandle(true, writer);
			this.RenderItemsHTML(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("RadSliderTrack_{0}", clientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rslTrack");
			if (!showDecreaseHandle)
			{
				writer.AddStyleAttribute(flag ? HtmlTextWriterStyle.Left : HtmlTextWriterStyle.Top, "0px");
			}
			writer.AddStyleAttribute(flag ? HtmlTextWriterStyle.Width : HtmlTextWriterStyle.Height, Unit.Pixel(this.GetTrackLength()).ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("RadSliderSelected_{0}", clientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rslSelectedregion");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write("<!-- -->");
			writer.RenderEndTag();
			if (this.ShowDragHandle)
			{
				this.RenderDragHandle(false, writer);
				if (this.IsSelectionRangeEnabled)
				{
					this.RenderDragHandle(true, writer);
				}
			}
			writer.RenderEndTag();
			this.RenderIncreaseDecreaseHandle(false, writer);
			writer.RenderEndTag();
		}

		// Token: 0x06009BBD RID: 39869 RVA: 0x0022A020 File Offset: 0x00228220
		protected void RenderItemsHTML(HtmlTextWriter writer)
		{
			SliderItemType itemType = this.ItemType;
			if (itemType == SliderItemType.None)
			{
				return;
			}
			bool flag = this.Orientation == Orientation.Horizontal;
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rslItemsWrapper");
			if (!this.ShowDecreaseHandle)
			{
				writer.AddStyleAttribute(flag ? HtmlTextWriterStyle.Left : HtmlTextWriterStyle.Top, "0px");
			}
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(flag ? HtmlTextWriterStyle.Width : HtmlTextWriterStyle.Height, Unit.Pixel(this.GetTrackLength()).ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			if (itemType == SliderItemType.Item)
			{
				bool isDirectionReversed = this.IsDirectionReversed;
				RadSliderItemCollection items = this.Items;
				int count = items.Count;
				int i = 0;
				int count2 = items.Count;
				while (i < count2)
				{
					items[isDirectionReversed ? (count - 1 - i) : i].RenderControl(writer);
					i++;
				}
			}
			else
			{
				this.RenderTicksHTML(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009BBE RID: 39870 RVA: 0x0022A104 File Offset: 0x00228304
		protected void RenderIncreaseDecreaseHandle(bool decrease, HtmlTextWriter writer)
		{
			bool flag = decrease ? this.ShowDecreaseHandle : this.ShowIncreaseHandle;
			if (flag)
			{
				bool isDirectionReversed = this.IsDirectionReversed;
				string className = string.Format("{0} {1}", "rslHandle", this.GetHandleCssClasses(decrease));
				string handleTitle = ((decrease && !isDirectionReversed) || (!decrease && isDirectionReversed)) ? this.DecreaseText : this.IncreaseText;
				string handleId = string.Format(decrease ? "RadSliderDecrease_{0}" : "RadSliderIncrease_{0}", this.ClientID);
				RadSlider.RenderHandle(writer, handleId, handleTitle, className);
			}
		}

		// Token: 0x06009BBF RID: 39871 RVA: 0x0022A188 File Offset: 0x00228388
		private string GetHandleCssClasses(bool decrease)
		{
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				if (this.Orientation == Orientation.Horizontal)
				{
					if (!decrease)
					{
						return string.Format("{0} {1} {2}", "rslIncrease", "p-icon", "p-i-arrow-right");
					}
					return string.Format("{0} {1} {2}", "rslDecrease", "p-icon", "p-i-arrow-left");
				}
				else
				{
					if (!decrease)
					{
						return string.Format("{0} {1} {2}", "rslIncrease", "p-icon", "p-i-arrow-down");
					}
					return string.Format("{0} {1} {2}", "rslDecrease", "p-icon", "p-i-arrow-up");
				}
			}
			else
			{
				if (!decrease)
				{
					return "rslIncrease";
				}
				return "rslDecrease";
			}
		}

		// Token: 0x06009BC0 RID: 39872 RVA: 0x0022A224 File Offset: 0x00228424
		protected void RenderDragHandle(bool isEndDragHandle, HtmlTextWriter writer)
		{
			string text = "rslDraghandle";
			string dragText = this.DragText;
			string text2 = string.Format(isEndDragHandle ? "RadSliderEndDrag_{0}" : "RadSliderDrag_{0}", this.ClientID);
			RadSlider.RenderHandle(writer, text2, dragText, text);
			if (!this.LiveDrag)
			{
				RadSlider.RenderHandle(writer, string.Format("liveDrag_{0}", text2), dragText, string.Format("{0} {1}", text, "rslLiveDragHandle"));
			}
		}

		// Token: 0x06009BC1 RID: 39873 RVA: 0x0022A28C File Offset: 0x0022848C
		private static void RenderHandle(HtmlTextWriter writer, string handleId, string handleTitle, string className)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, handleId);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, className);
			writer.AddAttribute(HtmlTextWriterAttribute.Title, handleTitle);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(handleTitle);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009BC2 RID: 39874 RVA: 0x0022A2E4 File Offset: 0x002284E4
		protected void RenderTicksHTML(HtmlTextWriter writer)
		{
			List<Dictionary<string, decimal>> tickData = this.GetTickData();
			int count = tickData.Count;
			if (count == 0)
			{
				return;
			}
			bool flag = this.TrackPosition == SliderTrackPosition.Center;
			bool flag2 = this.Orientation == Orientation.Horizontal;
			bool isDirectionReversed = this.IsDirectionReversed;
			Dictionary<string, int> itemsWrapperSize = this.GetItemsWrapperSize();
			int asymmetricAddOn = this.GetAsymmetricAddOn(tickData[count - 1]["isLargeTick"] == 0m, flag2 ? itemsWrapperSize["width"] : itemsWrapperSize["height"]);
			for (int i = 0; i < count; i++)
			{
				Dictionary<string, decimal> dictionary = tickData[isDirectionReversed ? (count - 1 - i) : i];
				string value = dictionary["tickData"].ToString("0.##");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, value);
				bool flag3 = dictionary["isLargeTick"] == 1m;
				string text = flag3 ? "rslLargeTick" : "rslSmallTick";
				if (i == 0)
				{
					text = string.Format("{0} {1}", text, flag3 ? "rslLargeTickFirst" : "rslSmallTickFirst");
				}
				else if (i == count - 1)
				{
					text = string.Format("{0} {1}", text, flag3 ? "rslLargeTickLast" : "rslSmallTickLast");
				}
				int itemLength = this.GetItemLength(i, asymmetricAddOn, count);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, Unit.Pixel(flag2 ? itemsWrapperSize["height"] : itemLength).ToString());
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, Unit.Pixel(flag2 ? itemLength : itemsWrapperSize["width"]).ToString());
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				if (flag3)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.Write(value);
					writer.RenderEndTag();
					if (flag)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "rslBRItemText");
						writer.RenderBeginTag(HtmlTextWriterTag.Span);
						writer.Write(value);
						writer.RenderEndTag();
					}
				}
				else
				{
					writer.Write("&nbsp;");
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x06009BC3 RID: 39875 RVA: 0x0022A500 File Offset: 0x00228700
		private string GetSliderCssClass()
		{
			string arg = (this.Orientation == Orientation.Horizontal) ? "rslHorizontal" : "rslVertical";
			string text = string.Format("{0} {1}", arg, this.GetTrackPositionClass());
			if (!base.IsEnabled)
			{
				text = string.Format("{0} {1}", text, "rslDisabled");
			}
			return text;
		}

		// Token: 0x06009BC4 RID: 39876 RVA: 0x0022A550 File Offset: 0x00228750
		private string GetTrackPositionClass()
		{
			if (this.ItemType == SliderItemType.None)
			{
				return string.Empty;
			}
			bool flag = this.Orientation == Orientation.Horizontal;
			string result = flag ? "rslMiddle" : "rslCenter";
			if (this.TrackPosition == SliderTrackPosition.TopLeft)
			{
				result = (flag ? "rslTop" : "rslLeft");
			}
			else if (this.TrackPosition == SliderTrackPosition.BottomRight)
			{
				result = (flag ? "rslBottom" : "rslRight");
			}
			return result;
		}

		// Token: 0x06009BC5 RID: 39877 RVA: 0x0022A5BC File Offset: 0x002287BC
		private int GetTrackLength()
		{
			int num = this.ShowIncreaseHandle ? 25 : 0;
			int num2 = this.ShowDecreaseHandle ? 25 : 0;
			return (int)((this.Orientation == Orientation.Horizontal) ? this.Width.Value : this.Height.Value) - num - num2;
		}

		// Token: 0x06009BC6 RID: 39878 RVA: 0x0022A618 File Offset: 0x00228818
		private int GetItemsWrapperFixedSize()
		{
			double num = (this.Orientation == Orientation.Horizontal) ? this.Height.Value : this.Width.Value;
			SliderTrackPosition trackPosition = this.TrackPosition;
			if (trackPosition != SliderTrackPosition.Center)
			{
				num -= 6.0;
				num -= 8.0;
			}
			if (num < 0.0)
			{
				num = 0.0;
			}
			return (int)num;
		}

		// Token: 0x06009BC7 RID: 39879 RVA: 0x0022A688 File Offset: 0x00228888
		private bool CheckRenderSmallTicks()
		{
			decimal num = (this.MaximumValue - this.MinimumValue) / this.SmallChange;
			if (num < 1m)
			{
				return false;
			}
			decimal d = Math.Floor(this.GetTrackLength() / num);
			return d >= 1m;
		}

		// Token: 0x06009BC8 RID: 39880 RVA: 0x0022A6E8 File Offset: 0x002288E8
		private bool CheckRenderLargeTicks()
		{
			decimal largeChange = this.LargeChange;
			decimal num = (this.MaximumValue - this.MinimumValue) / this.SmallChange;
			if (num < 1m)
			{
				return false;
			}
			decimal d = this.GetTrackLength() / num;
			decimal d2 = Math.Floor(d * largeChange);
			return largeChange > 0m && d2 >= 1m;
		}

		// Token: 0x06009BC9 RID: 39881 RVA: 0x0022A764 File Offset: 0x00228964
		private List<Dictionary<string, decimal>> GetTickData()
		{
			decimal d = this.LargeChange;
			decimal smallChange = this.SmallChange;
			decimal maximumValue = this.MaximumValue;
			decimal minimumValue = this.MinimumValue;
			bool flag = this.CheckRenderSmallTicks();
			bool flag2 = this.CheckRenderLargeTicks();
			if (!flag2)
			{
				d = Math.Abs(minimumValue) + maximumValue + smallChange;
			}
			List<Dictionary<string, decimal>> list = new List<Dictionary<string, decimal>>();
			decimal num = minimumValue;
			while (num <= maximumValue)
			{
				if (flag2)
				{
					list.Add(new Dictionary<string, decimal>
					{
						{
							"tickData",
							num
						},
						{
							"isLargeTick",
							1m
						}
					});
				}
				if (flag)
				{
					decimal num2 = num + (flag2 ? smallChange : 0m);
					decimal d2 = (flag2 && num + d <= maximumValue) ? (num + d) : (maximumValue + smallChange);
					while (num2 < d2)
					{
						list.Add(new Dictionary<string, decimal>
						{
							{
								"tickData",
								num2
							},
							{
								"isLargeTick",
								0m
							}
						});
						num2 += smallChange;
					}
				}
				num += d;
			}
			return list;
		}

		// Token: 0x06009BCA RID: 39882 RVA: 0x0022A89C File Offset: 0x00228A9C
		internal Dictionary<string, int> GetItemsWrapperSize()
		{
			int trackLength = this.GetTrackLength();
			int itemsWrapperFixedSize = this.GetItemsWrapperFixedSize();
			bool flag = this.Orientation == Orientation.Horizontal;
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary["width"] = (flag ? trackLength : itemsWrapperFixedSize);
			dictionary["height"] = (flag ? itemsWrapperFixedSize : trackLength);
			return dictionary;
		}

		// Token: 0x06009BCB RID: 39883 RVA: 0x0022A8EC File Offset: 0x00228AEC
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private int GetAsymmetricAddOn(bool isLastItemSmall, int trackLength)
		{
			if (this.ItemType != SliderItemType.Tick)
			{
				return 0;
			}
			bool isDirectionReversed = this.IsDirectionReversed;
			decimal maximumValue = this.MaximumValue;
			decimal minimumValue = this.MinimumValue;
			trackLength -= 8;
			int result = 0;
			decimal num = (maximumValue - minimumValue) % (isLastItemSmall ? this.SmallChange : this.LargeChange);
			if (num != 0m)
			{
				decimal num2 = isDirectionReversed ? (minimumValue + num) : (maximumValue - num);
				decimal d = maximumValue - minimumValue;
				decimal num3 = (num2 - minimumValue) / d;
				if (isDirectionReversed)
				{
					num3 = 1m - num3;
					num2 = maximumValue + minimumValue - num2;
				}
				int num4 = (num2 == minimumValue) ? 0 : ((num2 == maximumValue) ? trackLength : ((int)(num3 * trackLength)));
				result = trackLength - num4;
			}
			return result;
		}

		// Token: 0x06009BCC RID: 39884 RVA: 0x0022A9D8 File Offset: 0x00228BD8
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal int GetItemLength(int itemIndex, int asymmetricAddOn, int extent)
		{
			int num = this.GetTrackLength();
			if (extent < 1)
			{
				return num;
			}
			int num2 = 0;
			if (this.ItemType == SliderItemType.Tick)
			{
				double num3 = 4.0;
				double num4 = num3 + Math.Floor(((double)num - 2.0 * num3) / (double)(2 * (extent - 1)));
				int num5 = (int)num4;
				num2 = (int)num4;
				if (this.IsDirectionReversed)
				{
					num2 += asymmetricAddOn;
				}
				else
				{
					num5 += asymmetricAddOn;
				}
				if (itemIndex == 0)
				{
					return num2;
				}
				if (itemIndex == extent - 1)
				{
					return num5;
				}
				num -= num2 + num5;
				extent -= 2;
				itemIndex--;
				if (num < 0 || extent <= 0)
				{
					return num;
				}
			}
			int num6 = (int)((double)itemIndex / (double)extent * (double)num) + num2;
			int num7 = (int)((double)(itemIndex + 1) / (double)extent * (double)num) + num2;
			return num7 - num6;
		}

		// Token: 0x06009BCD RID: 39885 RVA: 0x0022AA8C File Offset: 0x00228C8C
		private static decimal ConvertDataValueToDecimal(object value)
		{
			if (value == null || value == DBNull.Value)
			{
				return 0m;
			}
			if (value is double || value is float || value is decimal || value is int || value is short || value is ushort || value is byte || value is sbyte || value is long || value is ulong || value is uint)
			{
				return Convert.ToDecimal(value, NumberFormatInfo.InvariantInfo);
			}
			if (!(value is bool))
			{
				return RadSlider.ParseDecimalFromString(value.ToString());
			}
			if (!(bool)value)
			{
				return 0m;
			}
			return 1m;
		}

		// Token: 0x06009BCE RID: 39886 RVA: 0x0022AB38 File Offset: 0x00228D38
		private static decimal ParseDecimalFromString(string sValue)
		{
			decimal result = 0m;
			if (!string.IsNullOrEmpty(sValue))
			{
				bool flag = decimal.TryParse(sValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
				if (!flag)
				{
					flag = decimal.TryParse(sValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result);
				}
				if (!flag)
				{
					throw new FormatException("The string was not recognized as a valid format.");
				}
			}
			return result;
		}

		// Token: 0x06009BCF RID: 39887 RVA: 0x0022AB8C File Offset: 0x00228D8C
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			if (!this.IsSelectionRangeEnabled)
			{
				this.Value = RadSlider.ConvertDataValueToDecimal(clientState["value"]);
			}
			else
			{
				this.SelectionStart = RadSlider.ConvertDataValueToDecimal(clientState["selectionStart"]);
				this.SelectionEnd = RadSlider.ConvertDataValueToDecimal(clientState["selectionEnd"]);
			}
			this.IsSelectionRangeEnabled = (bool)clientState["isSelectionRangeEnabled"];
			this.Orientation = (Orientation)clientState["orientation"];
			this.SmallChange = RadSlider.ConvertDataValueToDecimal(clientState["smallChange"]);
			this.LargeChange = RadSlider.ConvertDataValueToDecimal(clientState["largeChange"]);
			this.TrackMouseWheel = (bool)clientState["trackMouseWheel"];
			this.ShowDragHandle = (bool)clientState["showDragHandle"];
			this.ShowDecreaseHandle = (bool)clientState["showDecreaseHandle"];
			this.ShowIncreaseHandle = (bool)clientState["showIncreaseHandle"];
			this.length = -1;
			this.Width = Unit.Parse(clientState["width"].ToString());
			this.Height = Unit.Parse(clientState["height"].ToString());
			this.AnimationDuration = (int)clientState["animationDuration"];
			this.MinimumValue = RadSlider.ConvertDataValueToDecimal(clientState["minimumValue"]);
			this.MaximumValue = RadSlider.ConvertDataValueToDecimal(clientState["maximumValue"]);
			this.TrackPosition = (SliderTrackPosition)clientState["trackPosition"];
			this.LiveDrag = (bool)clientState["liveDrag"];
			this.DragText = (string)clientState["dragText"];
		}

		// Token: 0x06009BD0 RID: 39888 RVA: 0x0022AD60 File Offset: 0x00228F60
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			decimal d = this.IsSelectionRangeEnabled ? this.SelectionStart : this.Value;
			decimal selectionEnd = this.SelectionEnd;
			base.LoadPostData(postDataKey, postCollection);
			return (!this.IsSelectionRangeEnabled && d != this.Value) || (this.IsSelectionRangeEnabled && (selectionEnd != this.SelectionEnd || d != this.SelectionStart));
		}

		// Token: 0x06009BD1 RID: 39889 RVA: 0x0022ADD1 File Offset: 0x00228FD1
		protected override void RaisePostDataChangedEvent()
		{
			this.PerformValidation();
			this.OnValueChanged(EventArgs.Empty);
		}

		// Token: 0x06009BD2 RID: 39890 RVA: 0x0022ADE4 File Offset: 0x00228FE4
		private decimal GetPropertyDefaultValue(string propertyName)
		{
			System.ComponentModel.AttributeCollection attributes = TypeDescriptor.GetProperties(this)[propertyName].Attributes;
			DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
			return RadSlider.ConvertDataValueToDecimal(defaultValueAttribute.Value);
		}

		// Token: 0x06009BD3 RID: 39891 RVA: 0x0022AE24 File Offset: 0x00229024
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new SliderItemConverter(),
				new SliderItemBindingConverter()
			};
			javaScriptSerializer.RegisterConverters(converters);
			descriptor.AddScriptProperty("itemData", javaScriptSerializer.Serialize(this.Items.VisibleItems));
			if (this.EnableServerSideRendering)
			{
				descriptor.AddProperty("_renderLargeTicks", this.CheckRenderLargeTicks());
				descriptor.AddProperty("_renderSmallTicks", this.CheckRenderSmallTicks());
			}
			base.DescribeRenderingMode(descriptor);
			decimal num = this.LargeChange;
			if (num != 0m)
			{
				descriptor.AddProperty("largeChange", num);
			}
			num = this.MaximumValue;
			if (num != 100m)
			{
				descriptor.AddProperty("maximumValue", num);
			}
			num = this.MinimumValue;
			if (num != 0m)
			{
				descriptor.AddProperty("minimumValue", num);
			}
			num = this.SelectionEnd;
			if (num != 0m)
			{
				descriptor.AddProperty("selectionEnd", num);
			}
			num = this.SelectionStart;
			if (num != 0m)
			{
				descriptor.AddProperty("selectionStart", num);
			}
			num = this.SmallChange;
			if (num != 1m)
			{
				descriptor.AddProperty("smallChange", num);
			}
			num = this.Value;
			if (num != 0m)
			{
				descriptor.AddProperty("value", num);
			}
			descriptor.AddProperty("_accessKey", this.AccessKey);
			descriptor.AddProperty("_tabIndex", this.TabIndex);
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			descriptor.AddProperty("_selectedRegionStartValue", this.SelectedRegionStartValue);
			descriptor.AddScriptProperty("_itemBinding", javaScriptSerializer.Serialize(this.ItemBinding));
			descriptor.AddProperty("_appendDataBoundItems", this.AppendDataBoundItems);
		}

		// Token: 0x06009BD4 RID: 39892 RVA: 0x0022B054 File Offset: 0x00229254
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int>(descriptor, "animationDuration", this.AnimationDuration, 100);
			base.DescribeProperty<bool>(descriptor, "_autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<string>(descriptor, "_decreaseText", this.DecreaseText, "Decrease");
			base.DescribeProperty<string>(descriptor, "_dragText", this.DragText, "Drag");
			base.DescribeProperty<bool>(descriptor, "enableDragRange", this.EnableDragRange, false);
			base.DescribeProperty<bool>(descriptor, "_enableServerSideRendering", this.EnableServerSideRendering, false);
			base.DescribeProperty<string>(descriptor, "_height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "_increaseText", this.IncreaseText, "Increase");
			base.DescribeProperty<bool>(descriptor, "isDirectionReversed", this.IsDirectionReversed, false);
			base.DescribeProperty<bool>(descriptor, "isSelectionRangeEnabled", this.IsSelectionRangeEnabled, false);
			base.DescribeProperty<SliderItemType>(descriptor, "itemType", this.ItemType, SliderItemType.None);
			base.DescribeProperty<bool>(descriptor, "liveDrag", this.LiveDrag, true);
			base.DescribeProperty<Orientation>(descriptor, "orientation", this.Orientation, Orientation.Horizontal);
			base.DescribeProperty<bool>(descriptor, "showDecreaseHandle", this.ShowDecreaseHandle, true);
			base.DescribeProperty<bool>(descriptor, "showDragHandle", this.ShowDragHandle, true);
			base.DescribeProperty<bool>(descriptor, "showIncreaseHandle", this.ShowIncreaseHandle, true);
			base.DescribeProperty<SliderThumbsInteractionMode>(descriptor, "thumbsInteractionMode", this.ThumbsInteractionMode, SliderThumbsInteractionMode.Free);
			base.DescribeProperty<bool>(descriptor, "trackMouseWheel", this.TrackMouseWheel, true);
			base.DescribeProperty<SliderTrackPosition>(descriptor, "trackPosition", this.TrackPosition, SliderTrackPosition.Center);
			base.DescribeProperty<string>(descriptor, "_width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009BD5 RID: 39893 RVA: 0x0022B214 File Offset: 0x00229414
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "dataBound", this.OnClientDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDataBinding", this.OnClientItemDataBinding);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDataBound", this.OnClientItemDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsCreated", this.OnClientItemsCreated);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "slide", this.OnClientSlide);
			RadDataBoundControl.DescribeEvent(descriptor, "slideEnd", this.OnClientSlideEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "slideRange", this.OnClientSlideRange);
			RadDataBoundControl.DescribeEvent(descriptor, "slideRangeEnd", this.OnClientSlideRangeEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "slideRangeStart", this.OnClientSlideRangeStart);
			RadDataBoundControl.DescribeEvent(descriptor, "slideStart", this.OnClientSlideStart);
			RadDataBoundControl.DescribeEvent(descriptor, "valueChanged", this.OnClientValueChanged);
			RadDataBoundControl.DescribeEvent(descriptor, "valueChanging", this.OnClientValueChanging);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002BE8 RID: 11240
		internal const int TrackSize = 6;

		// Token: 0x04002BE9 RID: 11241
		internal const int TrackPositionOffset = 8;

		// Token: 0x04002BEA RID: 11242
		internal const int HandleSize = 25;

		// Token: 0x04002BEB RID: 11243
		internal const int DragHandleSize = 8;

		// Token: 0x04002BEC RID: 11244
		internal const int SmallTickMinSize = 1;

		// Token: 0x04002BED RID: 11245
		internal const int LargeTickMinSize = 1;

		// Token: 0x04002BEE RID: 11246
		internal const string SliderHorizontalClass = "rslHorizontal";

		// Token: 0x04002BEF RID: 11247
		internal const string SliderVerticalClass = "rslVertical";

		// Token: 0x04002BF0 RID: 11248
		internal const string SliderDisabledClass = "rslDisabled";

		// Token: 0x04002BF1 RID: 11249
		internal const string SliderTrackClass = "rslTrack";

		// Token: 0x04002BF2 RID: 11250
		internal const string SliderSelectedRegionClass = "rslSelectedregion";

		// Token: 0x04002BF3 RID: 11251
		internal const string ItemsWrapperClass = "rslItemsWrapper";

		// Token: 0x04002BF4 RID: 11252
		internal const string HandleClass = "rslHandle";

		// Token: 0x04002BF5 RID: 11253
		internal const string DecreaseHandleClass = "rslDecrease";

		// Token: 0x04002BF6 RID: 11254
		internal const string IncreaseHandleClass = "rslIncrease";

		// Token: 0x04002BF7 RID: 11255
		internal const string DragHandleClass = "rslDraghandle";

		// Token: 0x04002BF8 RID: 11256
		internal const string LiveResizeDragHandleClass = "rslLiveDragHandle";

		// Token: 0x04002BF9 RID: 11257
		internal const string HandleIconClass = "p-icon";

		// Token: 0x04002BFA RID: 11258
		internal const string HorizontalIncreaseIconClass = "p-i-arrow-right";

		// Token: 0x04002BFB RID: 11259
		internal const string HorizontalDecreaseIconClass = "p-i-arrow-left";

		// Token: 0x04002BFC RID: 11260
		internal const string VerticalIncreaseIconClass = "p-i-arrow-down";

		// Token: 0x04002BFD RID: 11261
		internal const string VerticalDecreaseIconClass = "p-i-arrow-up";

		// Token: 0x04002BFE RID: 11262
		internal const string SliderMiddlePositionClass = "rslMiddle";

		// Token: 0x04002BFF RID: 11263
		internal const string SliderCenterPositionClass = "rslCenter";

		// Token: 0x04002C00 RID: 11264
		internal const string SliderTopPositionClass = "rslTop";

		// Token: 0x04002C01 RID: 11265
		internal const string SliderLeftPositionClass = "rslLeft";

		// Token: 0x04002C02 RID: 11266
		internal const string SliderBottomPositionClass = "rslBottom";

		// Token: 0x04002C03 RID: 11267
		internal const string SliderRightPositionClass = "rslRight";

		// Token: 0x04002C04 RID: 11268
		private static readonly object eventValueChanged = new object();

		// Token: 0x04002C05 RID: 11269
		private static readonly object itemCreatedEvent = new object();

		// Token: 0x04002C06 RID: 11270
		private static readonly object itemDataBoundEvent = new object();

		// Token: 0x04002C07 RID: 11271
		private int length = -1;

		// Token: 0x04002C08 RID: 11272
		private SliderItemBinding _itemBinding;

		// Token: 0x04002C09 RID: 11273
		private bool originalEnabled = true;
	}
}
