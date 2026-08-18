using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000147 RID: 327
	[ToolboxBitmap(typeof(Accessor), "MultiHandleSlider.bmp")]
	[ClientScriptResource("Sys.Extended.UI.MultiHandleSliderBehavior", "MultiHandleSlider")]
	[TargetControlType(typeof(TextBox))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(DragDropScripts))]
	[RequiredScript(typeof(AnimationScripts))]
	[RequiredScript(typeof(TimerScript))]
	[Designer(typeof(MultiHandleSliderExtenderDesigner))]
	[ClientCssResource("MultiHandleSlider")]
	public class MultiHandleSliderExtender : ExtenderControlBase
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00016DF0 File Offset: 0x00014FF0
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x00016DFE File Offset: 0x00014FFE
		[ExtenderControlProperty]
		[DefaultValue("0")]
		[ClientPropertyName("minimum")]
		[Description("The lowest value on the slider.")]
		public int Minimum
		{
			get
			{
				return base.GetPropertyValue<int>("Minimum", 0);
			}
			set
			{
				base.SetPropertyValue<int>("Minimum", value);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00016E0C File Offset: 0x0001500C
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00016E1B File Offset: 0x0001501B
		[ClientPropertyName("maximum")]
		[DefaultValue("100")]
		[Description("The highest value on the slider.")]
		[ExtenderControlProperty]
		public int Maximum
		{
			get
			{
				return base.GetPropertyValue<int>("Maximum", 100);
			}
			set
			{
				base.SetPropertyValue<int>("Maximum", value);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00016E29 File Offset: 0x00015029
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x00016E3B File Offset: 0x0001503B
		[ClientPropertyName("length")]
		[ExtenderControlProperty]
		[DefaultValue(150)]
		[Description("The length of the slider rail in pixels.")]
		public int Length
		{
			get
			{
				return base.GetPropertyValue<int>("Length", 150);
			}
			set
			{
				base.SetPropertyValue<int>("Length", value);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00016E49 File Offset: 0x00015049
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x00016E57 File Offset: 0x00015057
		[ExtenderControlProperty]
		[Description("Determines number of discrete locations on the slider; otherwise, the slider is continous.")]
		[DefaultValue(0)]
		[ClientPropertyName("steps")]
		public int Steps
		{
			get
			{
				return base.GetPropertyValue<int>("Steps", 0);
			}
			set
			{
				base.SetPropertyValue<int>("Steps", value);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00016E65 File Offset: 0x00015065
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x00016E73 File Offset: 0x00015073
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("showInnerRail")]
		[Description("Determines if the slider will show an inner selected range rail; otherwise, it will display as a uniform rail.")]
		public bool ShowInnerRail
		{
			get
			{
				return base.GetPropertyValue<bool>("ShowInnerRail", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ShowInnerRail", value);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00016E81 File Offset: 0x00015081
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x00016E8F File Offset: 0x0001508F
		[ExtenderControlProperty]
		[ClientPropertyName("innerRailStyle")]
		[Description("Determines how the inner rail style is handled.")]
		[DefaultValue(MultiHandleInnerRailStyle.AsIs)]
		public MultiHandleInnerRailStyle InnerRailStyle
		{
			get
			{
				return base.GetPropertyValue<MultiHandleInnerRailStyle>("InnerRailStyle", MultiHandleInnerRailStyle.AsIs);
			}
			set
			{
				base.SetPropertyValue<MultiHandleInnerRailStyle>("InnerRailStyle", value);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00016E9D File Offset: 0x0001509D
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x00016EAB File Offset: 0x000150AB
		[DefaultValue(SliderOrientation.Horizontal)]
		[ExtenderControlProperty]
		[ClientPropertyName("orientation")]
		[Description("Determines if the slider's orientation is horizontal or vertical.")]
		public SliderOrientation Orientation
		{
			get
			{
				return base.GetPropertyValue<SliderOrientation>("Orientation", SliderOrientation.Horizontal);
			}
			set
			{
				base.SetPropertyValue<SliderOrientation>("Orientation", value);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00016EB9 File Offset: 0x000150B9
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x00016EC7 File Offset: 0x000150C7
		[ClientPropertyName("raiseChangeOnlyOnMouseUp")]
		[Description("Determines if changes to the slider's values are raised as an event when dragging; otherwise, they are raised on drag end.")]
		[DefaultValue(true)]
		[ExtenderControlProperty]
		public bool RaiseChangeOnlyOnMouseUp
		{
			get
			{
				return base.GetPropertyValue<bool>("RaiseChangeOnlyOnMouseUp", true);
			}
			set
			{
				base.SetPropertyValue<bool>("RaiseChangeOnlyOnMouseUp", value);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00016ED5 File Offset: 0x000150D5
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x00016EE3 File Offset: 0x000150E3
		[ClientPropertyName("enableInnerRangeDrag")]
		[Description("Determines if the inner rail range can be dragged as a whole, moving both handles defining it.")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool EnableInnerRangeDrag
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableInnerRangeDrag", false);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableInnerRangeDrag", value);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00016EF1 File Offset: 0x000150F1
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x00016EFF File Offset: 0x000150FF
		[Description("Determines if clicking on the rail will detect and move the closest handle.")]
		[ExtenderControlProperty]
		[DefaultValue(true)]
		[ClientPropertyName("enableRailClick")]
		public bool EnableRailClick
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableRailClick", true);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableRailClick", value);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00016F0D File Offset: 0x0001510D
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x00016F1B File Offset: 0x0001511B
		[ExtenderControlProperty]
		[ClientPropertyName("isReadOnly")]
		[DefaultValue(false)]
		[Description("Determines if the slider and its values can be manipulated.")]
		public bool IsReadOnly
		{
			get
			{
				return base.GetPropertyValue<bool>("IsReadOnly", false);
			}
			set
			{
				base.SetPropertyValue<bool>("IsReadOnly", value);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00016F29 File Offset: 0x00015129
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00016F37 File Offset: 0x00015137
		[Description("Determines if the slider will respond to arrow keys when it has focus.")]
		[ExtenderControlProperty]
		[DefaultValue(true)]
		[ClientPropertyName("enableKeyboard")]
		public bool EnableKeyboard
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableKeyboard", true);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableKeyboard", value);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00016F45 File Offset: 0x00015145
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x00016F53 File Offset: 0x00015153
		[ExtenderControlProperty]
		[DefaultValue(true)]
		[ClientPropertyName("enableMouseWheel")]
		[Description("Determines if the slider will respond to the mouse wheel when it has focus.")]
		public bool EnableMouseWheel
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableMouseWheel", true);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableMouseWheel", value);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00016F61 File Offset: 0x00015161
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x00016F6F File Offset: 0x0001516F
		[Description("Determines the number of points to increment or decrement the slider using the keyboard or mousewheel; ignored if steps is used.")]
		[DefaultValue(1)]
		[ClientPropertyName("increment")]
		[ExtenderControlProperty]
		public int Increment
		{
			get
			{
				return base.GetPropertyValue<int>("Increment", 1);
			}
			set
			{
				base.SetPropertyValue<int>("Increment", value);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00016F7D File Offset: 0x0001517D
		[ClientPropertyName("_isServerControl")]
		[ExtenderControlProperty(true, true)]
		public bool IsServerControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00016F80 File Offset: 0x00015180
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The list of controls used to bind slider handle values. These should be Label or TextBox controls.")]
		[NotifyParentProperty(true)]
		[Editor(typeof(MultiHandleSliderTargetsEditor), typeof(UITypeEditor))]
		[DefaultValue(null)]
		public Collection<MultiHandleSliderTarget> MultiHandleSliderTargets
		{
			get
			{
				if (base.DesignMode)
				{
					return new Collection<MultiHandleSliderTarget>();
				}
				if (this.ClientMultiHandleSliderTargets == null)
				{
					this.ClientMultiHandleSliderTargets = new Collection<MultiHandleSliderTarget>();
				}
				return this.ClientMultiHandleSliderTargets;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00016FA9 File Offset: 0x000151A9
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x00016FB7 File Offset: 0x000151B7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExtenderControlProperty(true, true)]
		[ClientPropertyName("multiHandleSliderTargets")]
		[Description("The list of controls used to bind slider handle values. These should be Label or TextBox controls.")]
		public Collection<MultiHandleSliderTarget> ClientMultiHandleSliderTargets
		{
			get
			{
				return base.GetPropertyValue<Collection<MultiHandleSliderTarget>>("MultiHandleSliderTargets", null);
			}
			set
			{
				base.SetPropertyValue<Collection<MultiHandleSliderTarget>>("MultiHandleSliderTargets", value);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00016FC5 File Offset: 0x000151C5
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x00016FD3 File Offset: 0x000151D3
		[ClientPropertyName("enableHandleAnimation")]
		[Description("Determines if the slider handles display an animation effect when changing position.")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool EnableHandleAnimation
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableHandleAnimation", false);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableHandleAnimation", value);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00016FE1 File Offset: 0x000151E1
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x00016FEF File Offset: 0x000151EF
		[ClientPropertyName("showHandleHoverStyle")]
		[Description("Determines if the slider handles will show a style effect when they are hovered over.")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool ShowHandleHoverStyle
		{
			get
			{
				return base.GetPropertyValue<bool>("ShowHandleHoverStyle", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ShowHandleHoverStyle", value);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00016FFD File Offset: 0x000151FD
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0001700B File Offset: 0x0001520B
		[ExtenderControlProperty]
		[Description("Determines if the slider handles will show a style effect when they are being dragged.")]
		[ClientPropertyName("showHandleDragStyle")]
		[DefaultValue(false)]
		public bool ShowHandleDragStyle
		{
			get
			{
				return base.GetPropertyValue<bool>("ShowHandleDragStyle", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ShowHandleDragStyle", value);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00017019 File Offset: 0x00015219
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0001702B File Offset: 0x0001522B
		[Description("Determines the total duration of the animation effect, in seconds.")]
		[ClientPropertyName("handleAnimationDuration")]
		[ExtenderControlProperty]
		[DefaultValue(0.02f)]
		public float HandleAnimationDuration
		{
			get
			{
				return base.GetPropertyValue<float>("HandleAnimationDuration", 0.1f);
			}
			set
			{
				base.SetPropertyValue<float>("HandleAnimationDuration", value);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00017039 File Offset: 0x00015239
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0001704B File Offset: 0x0001524B
		[Description("Determines the text to display as the tooltip; {0} denotes the current handle's value in the format string.")]
		[ClientPropertyName("tooltipText")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string TooltipText
		{
			get
			{
				return base.GetPropertyValue<string>("TooltipText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("TooltipText", value);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00017059 File Offset: 0x00015259
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0001706B File Offset: 0x0001526B
		[ClientPropertyName("cssClass")]
		[Description("The master style to apply to slider graphical elements.")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string CssClass
		{
			get
			{
				return base.GetPropertyValue<string>("CssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CssClass", value);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00017079 File Offset: 0x00015279
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x0001708B File Offset: 0x0001528B
		[Description("The event raised when the slider is completely loaded on the page.")]
		[ExtenderControlEvent]
		[ClientPropertyName("load")]
		[DefaultValue("")]
		public string OnClientLoad
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientLoad", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientLoad", value);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00017099 File Offset: 0x00015299
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x000170AB File Offset: 0x000152AB
		[Description("The event raised when the user initiates a drag operation on the slider.")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		[ClientPropertyName("dragStart")]
		public string OnClientDragStart
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientDragStart", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientDragStart", value);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x000170B9 File Offset: 0x000152B9
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x000170CB File Offset: 0x000152CB
		[ExtenderControlEvent]
		[DefaultValue("")]
		[Description("The event raised when the user drags the slider.")]
		[ClientPropertyName("drag")]
		public string OnClientDrag
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientDrag", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientDrag", value);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x000170D9 File Offset: 0x000152D9
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x000170EB File Offset: 0x000152EB
		[ClientPropertyName("dragEnd")]
		[Description("The event raised when the user drops the slider.")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		public string OnClientDragEnd
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientDragEnd", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientDragEnd", value);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000170F9 File Offset: 0x000152F9
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0001710B File Offset: 0x0001530B
		[DefaultValue("")]
		[ExtenderControlEvent]
		[ClientPropertyName("valueChanged")]
		[Description("The event raised when the slider changes its state.")]
		public string OnClientValueChanged
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientValueChanged", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientValueChanged", value);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00017119 File Offset: 0x00015319
		public MultiHandleSliderExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00017128 File Offset: 0x00015328
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0001713A File Offset: 0x0001533A
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(WebControl))]
		[DefaultValue("")]
		[ClientPropertyName("boundControlID")]
		public string BoundControlID
		{
			get
			{
				return base.GetPropertyValue<string>("BoundControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("BoundControlID", value);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00017148 File Offset: 0x00015348
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00017156 File Offset: 0x00015356
		[ClientPropertyName("decimals")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int Decimals
		{
			get
			{
				return base.GetPropertyValue<int>("Decimals", 0);
			}
			set
			{
				base.SetPropertyValue<int>("Decimals", value);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00017164 File Offset: 0x00015364
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00017176 File Offset: 0x00015376
		[ExtenderControlProperty]
		[ClientPropertyName("handleCssClass")]
		[DefaultValue("")]
		public string HandleCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("HandleCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HandleCssClass", value);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00017184 File Offset: 0x00015384
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x00017196 File Offset: 0x00015396
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("railCssClass")]
		public string RailCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("RailCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("RailCssClass", value);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x000171A4 File Offset: 0x000153A4
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x000171B6 File Offset: 0x000153B6
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[ExtenderControlProperty]
		[ClientPropertyName("handleImageUrl")]
		public string HandleImageUrl
		{
			get
			{
				return base.GetPropertyValue<string>("HandleImageUrl", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HandleImageUrl", value);
			}
		}
	}
}
