using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000120 RID: 288
	[RequiredScript(typeof(RadColorPickerScripts))]
	[LightweightRendering]
	[ParseChildren(true, "Items")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("ColorPicker", typeof(RadColorPicker))]
	[EmbeddedSkin("ColorPicker", "Default", typeof(RadColorPicker))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadColorPicker))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[ClientScriptResource("Telerik.Web.UI.RadColorPicker", "Telerik.Web.UI.Common.Core.js")]
	[DefaultEvent("OnColorChanged")]
	[DefaultProperty("Items")]
	[ToolboxData("<{0}:RadColorPicker Runat=server></{0}:RadColorPicker>")]
	[TelerikToolboxCategory("Date/Color Picker")]
	[ToolboxBitmap(typeof(RadColorPicker), "Telerik.Web.UI.ColorPicker.png")]
	[Designer("Telerik.Web.Design.RadColorPickerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadColorPicker : RadWebControl, IPostBackEventHandler
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00028638 File Offset: 0x00026838
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002863B File Offset: 0x0002683B
		[Browsable(false)]
		[Description("Collection of the color picker items.")]
		public ColorPickerItemCollection Items
		{
			get
			{
				return this.colors;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00028643 File Offset: 0x00026843
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x00028664 File Offset: 0x00026864
		[Category("Layout")]
		[DefaultValue(ColorPreset.Default)]
		[Description("Get/Set the preset colors of the color picker.")]
		public ColorPreset Preset
		{
			get
			{
				return (ColorPreset)(this.ViewState["Preset"] ?? ColorPreset.Default);
			}
			set
			{
				this.ViewState["Preset"] = value;
				this.isItemsSynchronized = false;
				this.SyncronizeItemsAndPreset();
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00028689 File Offset: 0x00026889
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x000286BD File Offset: 0x000268BD
		[SimplePersistenceSetting]
		[Description("Get/Set the selected color of the ColorPicker.")]
		[Category("Behavior")]
		public Color SelectedColor
		{
			get
			{
				if (this.ViewState["SelectedColor"] == null)
				{
					return Color.Empty;
				}
				return ColorTranslator.FromHtml((string)this.ViewState["SelectedColor"]);
			}
			set
			{
				this.ViewState["SelectedColor"] = ColorTranslator.ToHtml(value);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x000286D8 File Offset: 0x000268D8
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x0002872B File Offset: 0x0002692B
		[DefaultValue(18)]
		[Description("Get/Set the number of the columns in the palette.")]
		[Category("Appearance")]
		[ClientControlProperty]
		[ClientPropertyName("_columns")]
		public int Columns
		{
			get
			{
				if (this.ViewState["Columns"] == null || (int)this.ViewState["Columns"] < 1)
				{
					return this.GetColumnsForPreset();
				}
				return (int)this.ViewState["Columns"];
			}
			set
			{
				this.ViewState["Columns"] = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00028743 File Offset: 0x00026943
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x00028764 File Offset: 0x00026964
		[Description("Determines whether the control causes a postback on value change.")]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_autoPostBack")]
		[DefaultValue(false)]
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

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002877C File Offset: 0x0002697C
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0002879D File Offset: 0x0002699D
		[ClientPropertyName("_showEmptyColor")]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Determines whether to show the None color selection.")]
		[ClientControlProperty]
		public bool ShowEmptyColor
		{
			get
			{
				return (bool)(this.ViewState["ShowEmptyColor"] ?? true);
			}
			set
			{
				this.ViewState["ShowEmptyColor"] = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x000287B5 File Offset: 0x000269B5
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x000287D6 File Offset: 0x000269D6
		[ClientControlProperty]
		[ClientPropertyName("_showIcon")]
		[DefaultValue(false)]
		[Description("Determines whether to show the color picker as an icon, which when clicked opens the palette.")]
		[Category("Appearance")]
		public bool ShowIcon
		{
			get
			{
				return (bool)(this.ViewState["ShowIcon"] ?? false);
			}
			set
			{
				this.ViewState["ShowIcon"] = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x000287EE File Offset: 0x000269EE
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x0002880F File Offset: 0x00026A0F
		[DefaultValue(true)]
		[Category("Appearance")]
		[ClientPropertyName("_previewColor")]
		[Description("Determines whether to preview the color which has been selected.")]
		[ClientControlProperty]
		public bool PreviewColor
		{
			get
			{
				return (bool)(this.ViewState["PreviewColor"] ?? true);
			}
			set
			{
				this.ViewState["PreviewColor"] = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00028827 File Offset: 0x00026A27
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x00028855 File Offset: 0x00026A55
		[Description("Gets or sets the localization strings for the RadColorPicker.")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadColorPickerLocalization Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new RadColorPickerLocalization();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
			set
			{
				this._localization = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002885E File Offset: 0x00026A5E
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0002886B File Offset: 0x00026A6B
		[Localizable(true)]
		[Obsolete("This property is obsolete. Please use the Localization property instead.")]
		[Description("Gets or sets the tooltip of the icon.")]
		[DefaultValue("Pick Color")]
		public string PickColorText
		{
			get
			{
				return this.Localization.PickColorText;
			}
			set
			{
				this.Localization.PickColorText = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00028879 File Offset: 0x00026A79
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x00028886 File Offset: 0x00026A86
		[Obsolete("This property is obsolete. Please use the Localization property instead.")]
		[DefaultValue("(Current Color is {0})")]
		[Description("Gets or sets the text in the icon.")]
		[Localizable(true)]
		public string CurrentColorText
		{
			get
			{
				return this.Localization.CurrentColorText;
			}
			set
			{
				this.Localization.CurrentColorText = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00028894 File Offset: 0x00026A94
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x000288A1 File Offset: 0x00026AA1
		[Description("Gets or sets the text for the no color box.")]
		[DefaultValue("No Color")]
		[Localizable(true)]
		[Obsolete("This property is obsolete. Please use the Localization property instead.")]
		public string NoColorText
		{
			get
			{
				return this.Localization.NoColorText;
			}
			set
			{
				this.Localization.NoColorText = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x000288AF File Offset: 0x00026AAF
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x000288D0 File Offset: 0x00026AD0
		[Description("Gets or sets a value indicating the visible modes of the RadColorPicker's palette.")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(PaletteModes.WebPalette)]
		public PaletteModes PaletteModes
		{
			get
			{
				return (PaletteModes)(this.ViewState["PaletteModes"] ?? PaletteModes.WebPalette);
			}
			set
			{
				this.ViewState["PaletteModes"] = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000288E8 File Offset: 0x00026AE8
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x00028909 File Offset: 0x00026B09
		[DefaultValue(false)]
		[Browsable(true)]
		[Bindable(true)]
		[Description("Specifies whether the RadColorPicker will create an overlay element to ensure it will be displayed over a flash element.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool Overlay
		{
			get
			{
				return (bool)(this.ViewState["Overlay"] ?? false);
			}
			set
			{
				this.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00028921 File Offset: 0x00026B21
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x00028942 File Offset: 0x00026B42
		[ClientControlProperty]
		[Bindable(true)]
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Specifies whether the RadColorPicker popup will stay in the visible viewport of the browser window.")]
		public bool KeepInScreenBounds
		{
			get
			{
				return (bool)(this.ViewState["KeepInScreenBounds"] ?? true);
			}
			set
			{
				this.ViewState["KeepInScreenBounds"] = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002895A File Offset: 0x00026B5A
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0002897B File Offset: 0x00026B7B
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientPropertyName("_showRecentColors")]
		[Description("Specifies whether the RadColorPicker will display an array of recently used colors.")]
		[Browsable(true)]
		[Bindable(true)]
		public bool ShowRecentColors
		{
			get
			{
				return (bool)(this.ViewState["ShowRecentColors"] ?? false);
			}
			set
			{
				this.ViewState["ShowRecentColors"] = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00028993 File Offset: 0x00026B93
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x000289B4 File Offset: 0x00026BB4
		[Description("Specifies whether the RadColorPicker will display a button for choosing a custom color in the WebPalette tab.")]
		[ClientPropertyName("_enableCustomColor")]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Browsable(true)]
		[Bindable(true)]
		public bool EnableCustomColor
		{
			get
			{
				return (bool)(this.ViewState["EnableCustomColor"] ?? false);
			}
			set
			{
				this.ViewState["EnableCustomColor"] = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x000289CC File Offset: 0x00026BCC
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x000289EC File Offset: 0x00026BEC
		[DefaultValue("")]
		[Description("Gets or sets the client-side event handler that is called when the RadColorPicker control is initialized.")]
		[Category("Client-side events")]
		[ClientPropertyName("load")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x000289FF File Offset: 0x00026BFF
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x00028A1F File Offset: 0x00026C1F
		[ClientPropertyName("colorPreview")]
		[Category("Client-side events")]
		[Description("Gets or sets the client-side event handler that is called when a user previews a color.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public virtual string OnClientColorPreview
		{
			get
			{
				return ((string)this.ViewState["OnClientColorPreview"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientColorPreview"] = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00028A32 File Offset: 0x00026C32
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x00028A61 File Offset: 0x00026C61
		[ClientPropertyName("colorChanging")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the client-side event handler that is called just before the value of the color picker is changed.")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnClientColorChanging
		{
			get
			{
				if (this.ViewState["OnClientColorChanging"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientColorChanging"];
			}
			set
			{
				this.ViewState["OnClientColorChanging"] = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00028A74 File Offset: 0x00026C74
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x00028AA3 File Offset: 0x00026CA3
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("colorChange")]
		[DefaultValue("")]
		[Description("Gets or sets the client-side event handler that is called while the value of the color picker has been changed.")]
		public virtual string OnClientColorChange
		{
			get
			{
				if (this.ViewState["OnClientColorChange"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientColorChange"];
			}
			set
			{
				this.ViewState["OnClientColorChange"] = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00028AB6 File Offset: 0x00026CB6
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x00028AE5 File Offset: 0x00026CE5
		[Category("Client-side events")]
		[Description("Gets or sets the client-side event handler that is called when the popup element of the RadColorPicker (in case ShowIcon=true) shows.")]
		[DefaultValue("")]
		[ClientPropertyName("popUpShow")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnClientPopUpShow
		{
			get
			{
				if (this.ViewState["OnClientPopUpShow"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientPopUpShow"];
			}
			set
			{
				this.ViewState["OnClientPopUpShow"] = value;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000BAF RID: 2991 RVA: 0x00028AF8 File Offset: 0x00026CF8
		// (remove) Token: 0x06000BB0 RID: 2992 RVA: 0x00028B0B File Offset: 0x00026D0B
		[Description("Fires when the value of the ColorPicker has been changed.")]
		public event EventHandler ColorChanged
		{
			add
			{
				base.Events.AddHandler(RadColorPicker.EventColorChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadColorPicker.EventColorChanged, value);
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00028B1E File Offset: 0x00026D1E
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.OnColorChanged(EventArgs.Empty);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00028B2C File Offset: 0x00026D2C
		[Category("Action")]
		protected virtual void OnColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadColorPicker.EventColorChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00028B5A File Offset: 0x00026D5A
		public ColorPickerItemCollection GetDefaultColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetDefaultColors(false);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00028B74 File Offset: 0x00026D74
		private static ColorPickerItemCollection GetDefaultColors(bool getPresetColors)
		{
			ColorPickerItemCollection colorPickerItemCollection = new ColorPickerItemCollection();
			colorPickerItemCollection.AddRange(RadColorPicker.GetWeb216ColorsDefaultPresetOrder(getPresetColors));
			colorPickerItemCollection.AddRange(RadColorPicker.GetGrayscaleColorsDefaultPresetOrder(getPresetColors));
			return colorPickerItemCollection;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00028BA0 File Offset: 0x00026DA0
		public ColorPickerItemCollection GetStandardColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetStandardColors(false);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00028BB8 File Offset: 0x00026DB8
		private static ColorPickerItemCollection GetStandardColors(bool getPresetColors)
		{
			return new ColorPickerItemCollection
			{
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC00000"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFF0000"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFFC000"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFFFF00"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF92D050"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF00B050"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF00B0F0"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF0070C0"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF002060"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF7030A0"), getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(Color.White, getPresetColors)
			};
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00028CFC File Offset: 0x00026EFC
		private static ColorPickerItemCollection GetPalleteFromBaseColors(ColorPickerItemCollection baseColors, bool getPresetColors)
		{
			int[] array = new int[]
			{
				-6,
				-15,
				-30,
				-45,
				-60
			};
			int[] array2 = new int[]
			{
				50,
				35,
				25,
				-15,
				-30
			};
			int num = array.Length;
			ColorPickerItemCollection colorPickerItemCollection = new ColorPickerItemCollection();
			colorPickerItemCollection.AddRange(baseColors);
			Color[] array3 = new Color[]
			{
				ColorTranslator.FromHtml("#FFF2F2F2"),
				ColorTranslator.FromHtml("#FFD8D8D8"),
				ColorTranslator.FromHtml("#FFBFBFBF"),
				ColorTranslator.FromHtml("#FFA5A5A5"),
				ColorTranslator.FromHtml("#FF7F7F7F")
			};
			Color[] array4 = new Color[]
			{
				ColorTranslator.FromHtml("#FF7F7F7F"),
				ColorTranslator.FromHtml("#FF595959"),
				ColorTranslator.FromHtml("#FF3F3F3F"),
				ColorTranslator.FromHtml("#FF262626"),
				ColorTranslator.FromHtml("#FF0C0C0C")
			};
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < baseColors.Count; j++)
				{
					Color value = baseColors[j].Value;
					if (value.Equals(Color.White))
					{
						colorPickerItemCollection.Add(new ColorPickerItem(array3[i], getPresetColors));
					}
					else if (value.Equals(Color.Black))
					{
						colorPickerItemCollection.Add(new ColorPickerItem(array4[i], getPresetColors));
					}
					else
					{
						int[] array5;
						if (RadColorPicker.IsLightColor(value))
						{
							array5 = array;
						}
						else
						{
							array5 = array2;
						}
						int modification = array5[i];
						colorPickerItemCollection.Add(new ColorPickerItem(RadColorPicker.ModifyColorBrightness(value, modification), getPresetColors));
					}
				}
			}
			return colorPickerItemCollection;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00028F14 File Offset: 0x00027114
		private static bool IsLightColor(Color color)
		{
			return (double)color.GetBrightness() > 0.5;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00028F2C File Offset: 0x0002712C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private static Color ModifyColorBrightness(Color color, int modification)
		{
			int num = (int)color.R;
			int num2 = (int)color.G;
			int num3 = (int)color.B;
			int num4 = 150 * modification / 100;
			num += num4;
			if (num < 0)
			{
				num = 0;
			}
			if (num > 255)
			{
				num = 255;
			}
			num2 += num4;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			num3 += num4;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			return Color.FromArgb(255, num, num2, num3);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00028FB1 File Offset: 0x000271B1
		public ColorPickerItemCollection GetOfficeColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetOfficeColors(false);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00028FC8 File Offset: 0x000271C8
		private static ColorPickerItemCollection GetOfficeColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFEEECE1"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF1F497D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF4F81BD"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC0504D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF9BBB59"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF8064A2"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF4BACC6"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFF79646"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000290B4 File Offset: 0x000272B4
		public ColorPickerItemCollection GetApexColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetApexColors(false);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000290CC File Offset: 0x000272CC
		private static ColorPickerItemCollection GetApexColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC9C2D1"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF69676D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFCEB966"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF9CB084"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF6BB1C9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF6585CF"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF7E6BC9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA379BB"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000291B8 File Offset: 0x000273B8
		public ColorPickerItemCollection GetAspectColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetAspectColors(false);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000291D0 File Offset: 0x000273D0
		private static ColorPickerItemCollection GetAspectColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE3DED1"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF323232"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFF07F09"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF9F2936"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF1B587C"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF4E8542"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF604878"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC19859"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000292BC File Offset: 0x000274BC
		public ColorPickerItemCollection GetCivicColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetCivicColors(false);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000292D4 File Offset: 0x000274D4
		private static ColorPickerItemCollection GetCivicColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC5D1D7"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF646B86"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD16349"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFCCB400"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF8CADAE"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF8C7B70"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF8FB08C"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD19049"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000293C0 File Offset: 0x000275C0
		public ColorPickerItemCollection GetConcourseColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetConcourseColors(false);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000293D8 File Offset: 0x000275D8
		private static ColorPickerItemCollection GetConcourseColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFDEF5FA"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF464646"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF2DA2BF"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFDA1F28"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFEB641B"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF39639D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF474B78"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF7D3C4A"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000294C4 File Offset: 0x000276C4
		public ColorPickerItemCollection GetEquityColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetEquityColors(false);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000294DC File Offset: 0x000276DC
		private static ColorPickerItemCollection GetEquityColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE9E5DC"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF696464"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD34817"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF9B2D1F"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA28E6A"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF956251"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF918485"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF855D5D"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000295C8 File Offset: 0x000277C8
		public ColorPickerItemCollection GetFlowColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetFlowColors(false);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000295E0 File Offset: 0x000277E0
		private static ColorPickerItemCollection GetFlowColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFDBF5F9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF04617B"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF0F6FC6"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF009DD9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF0BD0D9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF10CF9B"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF7CCA62"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA5C249"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000296CC File Offset: 0x000278CC
		public ColorPickerItemCollection GetFoundryColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetFoundryColors(false);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000296E4 File Offset: 0x000278E4
		private static ColorPickerItemCollection GetFoundryColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFEAEBDE"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF676A55"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF72A376"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFB0CCB0"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA8CDD7"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC0BEAF"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFCEC597"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE8B7B7"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000297D0 File Offset: 0x000279D0
		public ColorPickerItemCollection GetMedianColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetMedianColors(false);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000297E8 File Offset: 0x000279E8
		private static ColorPickerItemCollection GetMedianColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFEBDDC3"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF775F55"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF94B6D2"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFDD8047"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA5AB81"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD8B25C"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF7BA79D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF968C8C"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000298D4 File Offset: 0x00027AD4
		public ColorPickerItemCollection GetMetroColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetMetroColors(false);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000298EC File Offset: 0x00027AEC
		private static ColorPickerItemCollection GetMetroColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD6ECFF"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF4E5B6F"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF7FD13B"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFEA157A"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFEB80A"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF00ADDC"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF738AC8"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF1AB39F"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000299D8 File Offset: 0x00027BD8
		public ColorPickerItemCollection GetModuleColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetModuleColors(false);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000299F0 File Offset: 0x00027BF0
		private static ColorPickerItemCollection GetModuleColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD4D4D6"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF5A6378"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFF0AD00"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF60B5CC"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE66C7D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF6BB76D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE88651"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC64847"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00029ADC File Offset: 0x00027CDC
		public ColorPickerItemCollection GetOpulentColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetOpulentColors(false);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00029AF4 File Offset: 0x00027CF4
		private static ColorPickerItemCollection GetOpulentColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFF4E7ED"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFB13F9A"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFB83D68"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFAC66BB"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFDE6C36"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFF9B639"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFCF6DA4"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFA8D3D"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00029BE0 File Offset: 0x00027DE0
		public ColorPickerItemCollection GetOrielColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetOrielColors(false);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00029BF8 File Offset: 0x00027DF8
		private static ColorPickerItemCollection GetOrielColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFFF39D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF575F6D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFE8637"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF7598D9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFB32C16"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFF5CD2D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFAEBAD5"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF777C84"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00029CE4 File Offset: 0x00027EE4
		public ColorPickerItemCollection GetOriginColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetOriginColors(false);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00029CFC File Offset: 0x00027EFC
		private static ColorPickerItemCollection GetOriginColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFDDE9EC"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF464653"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF727CA3"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF9FB8CD"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD2DA7A"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFADA7A"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFB88472"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF8E736A"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00029DE8 File Offset: 0x00027FE8
		public ColorPickerItemCollection GetPaperColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetPaperColors(false);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00029E00 File Offset: 0x00028000
		private static ColorPickerItemCollection GetPaperColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFEFAC9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF444D26"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA5B592"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFF3A447"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE7BC29"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD092A7"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF9C85C0"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF809EC2"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00029EEC File Offset: 0x000280EC
		public ColorPickerItemCollection GetSolsticeColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetSolsticeColors(false);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00029F04 File Offset: 0x00028104
		private static ColorPickerItemCollection GetSolsticeColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE7DEC9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF4F271C"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF4F271C"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFEB80A"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE7BC29"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF84AA33"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF964305"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF475A8D"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00029FF0 File Offset: 0x000281F0
		public ColorPickerItemCollection GetTechnicColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetTechnicColors(false);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002A008 File Offset: 0x00028208
		private static ColorPickerItemCollection GetTechnicColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD4D2D0"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF3B3B3B"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF6EA0B0"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF6EA0B0"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF8D89A4"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF748560"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF9E9273"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF7E848D"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002A0F4 File Offset: 0x000282F4
		public ColorPickerItemCollection GetTrekColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetTrekColors(false);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002A10C File Offset: 0x0002830C
		private static ColorPickerItemCollection GetTrekColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFBEEC9"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF4E3B30"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFF0A22E"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA5644E"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFB58B80"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC3986D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA19574"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC17529"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002A1F8 File Offset: 0x000283F8
		public ColorPickerItemCollection GetUrbanColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetUrbanColors(false);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002A210 File Offset: 0x00028410
		private static ColorPickerItemCollection GetUrbanColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFDEDEDE"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF424456"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF53548A"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF438086"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFA04DA3"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFC4652D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF8B5D3D"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF5C92B5"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002A2FC File Offset: 0x000284FC
		public ColorPickerItemCollection GetVerveColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetVerveColors(false);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002A314 File Offset: 0x00028514
		private static ColorPickerItemCollection GetVerveColors(bool getPresetColors)
		{
			return RadColorPicker.GetPalleteFromBaseColors(new ColorPickerItemCollection
			{
				new ColorPickerItem(Color.White, getPresetColors),
				new ColorPickerItem(Color.Black, getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFD2D2D2"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF666666"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFFF388C"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFE40059"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF9C007F"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF68007F"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF005BD3"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF00349E"), getPresetColors)
			}, getPresetColors);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002A400 File Offset: 0x00028600
		public ColorPickerItemCollection GetGrayscaleColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetGrayscaleColors(false);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002A418 File Offset: 0x00028618
		private static ColorPickerItemCollection GetGrayscaleColors(bool getPresetColors)
		{
			ColorPickerItemCollection colorPickerItemCollection = new ColorPickerItemCollection();
			for (int i = 0; i <= 255; i += 17)
			{
				colorPickerItemCollection.Add(new ColorPickerItem(Color.FromArgb(255, i, i, i), getPresetColors));
			}
			return colorPickerItemCollection;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002A456 File Offset: 0x00028656
		public ColorPickerItemCollection GetGrayscaleColorsDefaultPresetOrder()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetGrayscaleColors(false);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002A470 File Offset: 0x00028670
		private static ColorPickerItemCollection GetGrayscaleColorsDefaultPresetOrder(bool getPresetColors)
		{
			ColorPickerItemCollection colorPickerItemCollection = new ColorPickerItemCollection();
			for (int i = 255; i >= 0; i -= 17)
			{
				colorPickerItemCollection.Add(new ColorPickerItem(Color.FromArgb(255, i, i, i), getPresetColors));
			}
			return colorPickerItemCollection;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002A4AE File Offset: 0x000286AE
		public ColorPickerItemCollection GetReallyWebSafeColors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetReallyWebSafeColors(false);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002A4C8 File Offset: 0x000286C8
		private static ColorPickerItemCollection GetReallyWebSafeColors(bool getPresetColors)
		{
			return new ColorPickerItemCollection
			{
				new ColorPickerItem(ColorTranslator.FromHtml("#FF000000"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFff0000"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF000033"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFff0033"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF0000ff"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFff00ff"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF00ff00"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF66ff00"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFffff00"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF33ff33"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF66ff33"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFffff33"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF00ff66"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF33ff66"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFccff66"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFffff66"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF00ffcc"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF33ffcc"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF00ffff"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF33ffff"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FF66ffff"), getPresetColors),
				new ColorPickerItem(ColorTranslator.FromHtml("#FFffffff"), getPresetColors)
			};
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002A6C0 File Offset: 0x000288C0
		public ColorPickerItemCollection GetWeb216Colors()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetWeb216Colors(false);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002A6D8 File Offset: 0x000288D8
		private static ColorPickerItemCollection GetWeb216Colors(bool getPresetColors)
		{
			ColorPickerItemCollection colorPickerItemCollection = new ColorPickerItemCollection();
			for (int i = 0; i <= 255; i += 51)
			{
				for (int j = 0; j <= 255; j += 51)
				{
					for (int k = 0; k <= 255; k += 51)
					{
						colorPickerItemCollection.Add(new ColorPickerItem(Color.FromArgb(255, i, j, k), getPresetColors));
					}
				}
			}
			return colorPickerItemCollection;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002A738 File Offset: 0x00028938
		public ColorPickerItemCollection GetWeb216ColorsDefaultPresetOrder()
		{
			if (!this._palleteRead)
			{
				this._palleteRead = true;
			}
			return RadColorPicker.GetWeb216ColorsDefaultPresetOrder(false);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002A750 File Offset: 0x00028950
		private static ColorPickerItemCollection GetWeb216ColorsDefaultPresetOrder(bool getPresetColors)
		{
			ColorPickerItemCollection colorPickerItemCollection = new ColorPickerItemCollection();
			int num = 0;
			int num2 = 18;
			int num3 = 255;
			for (int i = 255; i >= 0; i -= 51)
			{
				int j;
				for (j = num3; j >= 0; j -= 51)
				{
					if (num == num2 && num3 != 102)
					{
						num = 0;
						break;
					}
					for (int k = 255; k >= 0; k -= 51)
					{
						colorPickerItemCollection.Add(new ColorPickerItem(Color.FromArgb(255, j, k, i), getPresetColors));
						num++;
					}
				}
				if (i == 0 && j != -51 && num3 != 102)
				{
					i = 306;
					num3 = 102;
				}
			}
			return colorPickerItemCollection;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0002A7F0 File Offset: 0x000289F0
		private int WebColorBoxSize
		{
			get
			{
				if (!this.IsLargeSkin())
				{
					return 12;
				}
				return this.webColorBoxSizesByLargeSkin[base.RuntimeSkin];
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002A80E File Offset: 0x00028A0E
		internal bool IsLargeSkin()
		{
			return this.EnableEmbeddedSkins && this.webColorBoxSizesByLargeSkin.ContainsKey(base.RuntimeSkin);
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002A82B File Offset: 0x00028A2B
		private int RgbWidth
		{
			get
			{
				if (!this.IsTouchSkin())
				{
					return 270;
				}
				return 300;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002A840 File Offset: 0x00028A40
		private int HsbWidth
		{
			get
			{
				if (!this.IsTouchSkin())
				{
					return 345;
				}
				return 380;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0002A855 File Offset: 0x00028A55
		private int HsvWidth
		{
			get
			{
				if (!this.IsTouchSkin() && !this.IsSkin("Silk") && !this.IsSkin("Glow"))
				{
					return 249;
				}
				return 380;
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002A884 File Offset: 0x00028A84
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Unit width = this.offsetWidth = this.Width;
			if (this.ShowIcon)
			{
				this.Width = Unit.Empty;
			}
			else
			{
				bool flag = this.IsModeEnabled(PaletteModes.WebPalette);
				bool flag2 = this.IsModeEnabled(PaletteModes.RGBSliders);
				bool flag3 = this.IsModeEnabled(PaletteModes.HSB);
				bool flag4 = this.IsModeEnabled(PaletteModes.HSV);
				if (width.IsEmpty)
				{
					if (flag)
					{
						this.offsetWidth = this.paletteWrapperBox + this.pageViewBox + this.Columns * (this.WebColorBoxSize + this.colorBox);
					}
					else if (flag2)
					{
						this.offsetWidth = this.RgbWidth;
					}
					else if (flag3)
					{
						this.offsetWidth = this.HsbWidth;
					}
					else if (flag4)
					{
						this.offsetWidth = this.HsvWidth;
					}
					this.Width = this.offsetWidth;
				}
				if ((flag && flag2) || (flag && flag3) || (flag && flag4) || (flag2 && flag3) || (flag2 && flag4) || (flag3 && flag4))
				{
					base.Style[HtmlTextWriterStyle.PaddingBottom] = string.Format("{0}px", this.tabStripHeight);
				}
			}
			Unit height = this.Height;
			this.Height = Unit.Empty;
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			base.AddAttributesToRender(writer);
			this.Width = width;
			this.Height = height;
			this.AccessKey = accessKey;
			this.TabIndex = tabIndex;
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002AA08 File Offset: 0x00028C08
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002AA0C File Offset: 0x00028C0C
		protected override string CssClassFormatString
		{
			get
			{
				return "RadColorPicker RadColorPicker_{0}" + ((!base.IsEnabled) ? " rcpDisabled" : "");
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002AA2C File Offset: 0x00028C2C
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			this.SyncronizeItemsAndPreset();
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002AA3A File Offset: 0x00028C3A
		private void SetRenderModeToChildControl(ISkinnableControl control)
		{
			if (control != null)
			{
				control.RenderMode = this.RenderMode;
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002AA4C File Offset: 0x00028C4C
		protected override void CreateChildControls()
		{
			bool flag = this.IsModeEnabled(PaletteModes.RGBSliders);
			bool flag2 = this.IsModeEnabled(PaletteModes.HSB) || this.IsModeEnabled(PaletteModes.HSV);
			bool flag3 = this.IsModeEnabled(PaletteModes.WebPalette);
			if (flag || flag2 || (flag3 && this.EnableCustomColor))
			{
				if (flag3 || this.ShowIcon)
				{
					if (!base.DesignMode)
					{
						this.hiddenSlider = new RadSlider();
						this.hiddenSlider.ID = this.ClientID + "_hiddenSlider";
						this.SetRenderModeToChildControl(this.hiddenSlider);
						this.hiddenSlider.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
						this.hiddenSlider.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
						this.hiddenSlider.Skin = base.RuntimeSkin;
						this.hiddenSlider.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
						this.hiddenSlider.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
						this.hiddenSlider.EnableTheming = this.EnableTheming;
						this.hiddenSlider.EnableViewState = false;
						this.hiddenSlider.TabIndex = -1;
						this.hiddenSlider.ShowDecreaseHandle = false;
						this.hiddenSlider.ShowIncreaseHandle = false;
						this.Controls.Add(this.hiddenSlider);
					}
				}
				else if (flag)
				{
					this.redSlider = this.CreateRGBSlider(this.ClientID + "_redSlider", "rcpSlider rcpRedSlider", int.Parse(this.SelectedColor.R.ToString()));
					this.SetRenderModeToChildControl(this.redSlider);
					this.Controls.Add(this.redSlider);
					this.greenSlider = this.CreateRGBSlider(this.ClientID + "_greenSlider", "rcpSlider rcpGreenSlider", int.Parse(this.SelectedColor.G.ToString()));
					this.SetRenderModeToChildControl(this.greenSlider);
					this.Controls.Add(this.greenSlider);
					this.blueSlider = this.CreateRGBSlider(this.ClientID + "_blueSlider", "rcpSlider rcpBlueSlider", int.Parse(this.SelectedColor.B.ToString()));
					this.SetRenderModeToChildControl(this.blueSlider);
					this.Controls.Add(this.blueSlider);
				}
				else if (flag2)
				{
					this.millionColorsSlider = this.CreateMillionColorsSlider(100, this.ClientID + "_millionColorsSlider", "rcpMillionColorsSlider", 0);
					this.SetRenderModeToChildControl(this.millionColorsSlider);
					this.Controls.Add(this.millionColorsSlider);
				}
			}
			base.CreateChildControls();
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002ACF0 File Offset: 0x00028EF0
		private RadSlider CreateRGBSlider(string sliderID, string sliderClassName, int slValue)
		{
			RadSlider radSlider = new RadSlider();
			radSlider.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
			radSlider.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
			radSlider.Skin = base.RuntimeSkin;
			radSlider.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
			radSlider.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
			radSlider.EnableTheming = this.EnableTheming;
			radSlider.MaximumValue = this.rgbSliderValue;
			radSlider.ID = sliderID;
			radSlider.CssClass = sliderClassName;
			radSlider.EnableViewState = false;
			radSlider.IncreaseText = this.Localization.RGBSlidersIncreaseText;
			radSlider.DecreaseText = this.Localization.RGBSlidersDecreaseText;
			radSlider.DragText = this.Localization.RGBSlidersDragText;
			radSlider.TabIndex = -1;
			radSlider.ShowDecreaseHandle = false;
			radSlider.ShowIncreaseHandle = false;
			int n = this.rgbSliderSize - this.paletteWrapperBox - this.pageViewBox;
			if (!this.Width.IsEmpty)
			{
				n = int.Parse(this.Width.Value.ToString()) - this.paletteWrapperBox - this.pageViewBox - this.rgbInputSize - this.rgbLabelSize;
			}
			radSlider.Width = n;
			int value = this.rgbSliderValue;
			if (!this.SelectedColor.IsEmpty)
			{
				value = slValue;
			}
			radSlider.Value = value;
			return radSlider;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002AE4C File Offset: 0x0002904C
		private RadSlider CreateMillionColorsSlider(int maxValue, string sliderID, string sliderClassName, int slValue)
		{
			RadSlider radSlider = new RadSlider();
			radSlider.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
			radSlider.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
			radSlider.Skin = base.RuntimeSkin;
			radSlider.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
			radSlider.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
			radSlider.EnableTheming = this.EnableTheming;
			radSlider.Orientation = Orientation.Vertical;
			radSlider.MaximumValue = maxValue;
			radSlider.ID = sliderID;
			radSlider.CssClass = sliderClassName;
			radSlider.EnableViewState = false;
			radSlider.Height = Unit.Pixel(this.millionColorsSliderHeight);
			radSlider.Width = Unit.Pixel(this.millionColorsSliderWidth);
			radSlider.Value = slValue;
			radSlider.TabIndex = -1;
			radSlider.ShowIncreaseHandle = false;
			radSlider.ShowDecreaseHandle = false;
			if (this.IsModeEnabled(PaletteModes.HSB))
			{
				radSlider.DragText = this.Localization.HSBSliderDragText;
			}
			if (this.IsModeEnabled(PaletteModes.HSV))
			{
				radSlider.DragText = this.Localization.HSVSliderDragText;
			}
			return radSlider;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002AF48 File Offset: 0x00029148
		private void RenderHeader(HtmlTextWriter writer)
		{
			string text = "rcpHeader";
			bool flag = this.IsModeEnabled(PaletteModes.WebPalette);
			bool flag2 = this.IsModeEnabled(PaletteModes.RGBSliders) || this.IsModeEnabled(PaletteModes.HSB) || this.IsModeEnabled(PaletteModes.HSV);
			if (!this.PreviewColor && !this.ShowEmptyColor && (!flag || !this.EnableCustomColor) && flag)
			{
				text = string.Format("{0} {1}", text, "rcpEmptyHeader");
			}
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "22px");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.PreviewColor)
			{
				Color color = this.SelectedColor;
				if (color.Equals(Color.Empty))
				{
					color = Color.White;
				}
				string value = RadColorPicker.ColorToHex(color);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_preview", this.ClientID));
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpColorPreview");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, value);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write("<!-- / -->");
				writer.RenderEndTag();
				if (this.TabIndex > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpHexInput");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Localization.HexInputTitle);
				writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_hexInput", this.ClientID));
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
				writer.AddAttribute(HtmlTextWriterAttribute.Dir, "ltr");
				if (this.IsModeEnabled(PaletteModes.WebPalette))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
				}
				if (!base.IsEnabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Input);
				writer.RenderEndTag();
			}
			if (this.ShowEmptyColor)
			{
				if (this.TabIndex > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_emptycolor", this.ClientID));
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Localization.NoColorText);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpImageButton rcpEmptyColor");
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(this.Localization.NoColorText);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			if (flag && this.EnableCustomColor)
			{
				if (this.TabIndex > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Localization.CustomColor);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpImageButton rcpCustomColorButton");
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(this.Localization.CustomColor);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			if (!flag && flag2)
			{
				if (this.TabIndex > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpButton rcpApplyButton");
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(this.Localization.ApplyButtonText);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002B2A0 File Offset: 0x000294A0
		private void RenderTab(HtmlTextWriter writer, string tabText, string tabContainerID, bool isSelected, bool isAccessKeySet)
		{
			if (isSelected)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpSelectedTab");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			if (!isAccessKeySet)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (this.TabIndex > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Title, tabText);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			this.RenderTabContent(writer, tabText);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002B327 File Offset: 0x00029527
		public void RenderTabContent(HtmlTextWriter writer, string tabText)
		{
			if (this.ResolvedRenderMode.Equals(RenderMode.Classic))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(tabText);
				writer.RenderEndTag();
				return;
			}
			writer.Write(tabText);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002B35E File Offset: 0x0002955E
		private static void RenderPageView(HtmlTextWriter writer, bool showPageView, string pageViewID, string pageViewClass)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, pageViewClass);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, pageViewID);
			if (!showPageView)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002B38C File Offset: 0x0002958C
		private void RenderWebPalette(HtmlTextWriter writer, bool isAccessKeySet)
		{
			if (base.DesignMode)
			{
				this.SyncronizeItemsAndPreset();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpWebPalette");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			int columns = this.Columns;
			int num = this.WebColorBoxSize;
			int num2 = num + this.colorBox;
			bool flag = !base.DesignMode && this.Context.Request.Browser.IsBrowser("Opera");
			if (!this.Width.IsEmpty)
			{
				int num3 = int.Parse(this.Width.Value.ToString()) - this.paletteWrapperBox - this.pageViewBox;
				if (num3 < 1)
				{
					num3 = 1;
				}
				num = num3 / columns;
				if (num < 1)
				{
					num = 1;
				}
				if (flag)
				{
					num2 = num;
				}
				num -= this.colorBox;
				if (num < 1)
				{
					num = 1;
				}
			}
			int num4 = 0;
			bool flag2 = true;
			foreach (object obj in this.Items)
			{
				ColorPickerItem colorPickerItem = (ColorPickerItem)obj;
				if (num4 == columns)
				{
					num4 = 0;
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpBreakLine");
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					writer.Write("<!-- -->");
					writer.RenderEndTag();
				}
				num4++;
				string value = this.SelectedColor.ToArgb().Equals(colorPickerItem.Value.ToArgb()) ? "rcpSelectedColor" : "rcpColorBox";
				if (flag)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, string.Format("{0}px", num2));
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				if (flag2)
				{
					if (!isAccessKeySet)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
					}
					if (this.TabIndex > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
					}
					flag2 = false;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, colorPickerItem.Title);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, RadColorPicker.ColorToHex(colorPickerItem.Value));
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				if (this.ResolvedRenderMode == RenderMode.Classic)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, string.Format("{0}px", num));
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, string.Format("{0}px", num));
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(RadColorPicker.ColorToHex(colorPickerItem.Value));
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002B634 File Offset: 0x00029834
		private void RenderRgbSliders(HtmlTextWriter writer, bool isAccessKeySet)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			this.RenderRgbSliderWithInput(writer, this.ClientID + "_redInput", "rcpInput rcpRedInput", "R:", this.redSlider, isAccessKeySet);
			this.RenderRgbSliderWithInput(writer, this.ClientID + "_greenInput", "rcpInput rcpGreenInput", "G:", this.greenSlider, true);
			this.RenderRgbSliderWithInput(writer, this.ClientID + "_blueInput", "rcpInput rcpBlueInput", "B:", this.blueSlider, true);
			writer.RenderEndTag();
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002B6C8 File Offset: 0x000298C8
		private void RenderRgbSliderWithInput(HtmlTextWriter writer, string inputID, string inputClassName, string labelText, RadSlider slider, bool isAccessKeySet)
		{
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, string.Format("{0}px", this.offsetWidth.Value));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			RadColorPicker.RenderLabel(writer, inputID, labelText);
			slider.RenderControl(writer);
			this.RenderInput(writer, inputID, inputClassName, slider.Value.ToString(), isAccessKeySet);
			writer.RenderEndTag();
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002B738 File Offset: 0x00029938
		private void RenderLabelWithInput(HtmlTextWriter writer, string id, string className, string value, string labelText, bool isAccessKeySet)
		{
			RadColorPicker.RenderLabel(writer, id, labelText);
			this.RenderInput(writer, id, className, value, isAccessKeySet);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002B750 File Offset: 0x00029950
		private static void RenderLabel(HtmlTextWriter writer, string id, string labelText)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.For, id);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.Write(labelText);
			writer.RenderEndTag();
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002B770 File Offset: 0x00029970
		private void RenderInput(HtmlTextWriter writer, string id, string className, string value, bool isAccessKeySet)
		{
			if (!isAccessKeySet)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (this.TabIndex > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, className);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			if (!base.IsEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002B7FC File Offset: 0x000299FC
		private void RenderMillionColors(HtmlTextWriter writer, bool isAccessKeySet)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_millionColorsPalette", this.ClientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpMillionColorsPalette");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_millionColorsHandle", this.ClientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpMillionColorsHandle");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "0px");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write("<!-- / -->");
			writer.RenderEndTag();
			writer.RenderEndTag();
			this.millionColorsSlider.RenderControl(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpInputsWrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			this.RenderListItemWithInput(writer, this.ClientID + "_rInput", "rcpInput rcpRedInput", "0", "R:", isAccessKeySet);
			this.RenderListItemWithInput(writer, this.ClientID + "_gInput", "rcpInput rcpGreenInput", "0", "G:", true);
			this.RenderListItemWithInput(writer, this.ClientID + "_bInput", "rcpInput rcpBlueInput", "0", "B:", true);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpMillionColorsInputs");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			this.RenderListItemWithInput(writer, this.ClientID + "_hInput", "rcpInput rcpHInput", "0", "H:", true);
			this.RenderListItemWithInput(writer, this.ClientID + "_sInput", "rcpInput rcpSInput", "0", "S:", true);
			if (this.IsModeEnabled(PaletteModes.HSB))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpLInputWrapper");
				this.RenderListItemWithInput(writer, this.ClientID + "_lInput", "rcpInput rcpLInput", "0", "B:", true);
			}
			if (this.IsModeEnabled(PaletteModes.HSV))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpVInputWrapper");
				this.RenderListItemWithInput(writer, this.ClientID + "_vInput", "rcpInput rcpVInput", "0", "V:", true);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002BA21 File Offset: 0x00029C21
		private void RenderListItemWithInput(HtmlTextWriter writer, string id, string className, string value, string labelText, bool isAccessKeySet)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.RenderLabelWithInput(writer, id, className, value, labelText, isAccessKeySet);
			writer.RenderEndTag();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002BA40 File Offset: 0x00029C40
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			if (base.DesignMode)
			{
				base.ChildControlsCreated = false;
				this.Controls.Clear();
			}
			this.EnsureChildControls();
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			string accessKey = this.AccessKey;
			HttpBrowserCapabilities httpBrowserCapabilities = (!base.DesignMode) ? this.Context.Request.Browser : null;
			bool flag = string.IsNullOrEmpty(accessKey) || httpBrowserCapabilities.IsBrowser("Safari") || httpBrowserCapabilities.IsBrowser("Chrome");
			if (this.ShowIcon)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_label", this.ClientID));
				string text = RadColorPicker.ColorToHex(this.SelectedColor);
				string text2 = string.Format(this.Localization.CurrentColorText, string.IsNullOrEmpty(text) ? this.Localization.BlankColorText : text);
				string pickColorText = this.Localization.PickColorText;
				string value = string.Format("{0} {1}", pickColorText, text2).Trim();
				if (!string.IsNullOrEmpty(value))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, value);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpIcon");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				if (!flag)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
				}
				if (this.TabIndex > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write((pickColorText.Trim().Length == 0) ? "&nbsp;" : pickColorText);
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_icon", this.ClientID));
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Em);
				writer.Write((text2.Trim().Length == 0) ? "&nbsp;" : text2);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			else
			{
				if (base.DesignMode)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, string.Format("{0}px", this.offsetWidth.Value));
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_palette", this.ClientID));
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpPalette");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				bool flag2 = this.IsModeEnabled(PaletteModes.WebPalette);
				bool flag3 = this.IsModeEnabled(PaletteModes.RGBSliders);
				bool flag4 = this.IsModeEnabled(PaletteModes.HSB);
				bool flag5 = this.IsModeEnabled(PaletteModes.HSV);
				this.RenderRoundedCornersElements(writer);
				if ((flag2 && flag3) || (flag2 && flag4) || (flag2 && flag5) || (flag3 && flag4) || (flag3 && flag5) || (flag4 && flag5))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpTabs");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.RenderBeginTag(HtmlTextWriterTag.Ul);
					bool isSelected = true;
					if (flag2)
					{
						this.RenderTab(writer, this.Localization.WebPaletteTabText, this.ClientID + "_webPalette", isSelected, flag);
						isSelected = false;
						flag = true;
					}
					if (flag3)
					{
						this.RenderTab(writer, this.Localization.RGBSlidersTabText, this.ClientID + "_rgbSliders", isSelected, flag);
						flag = true;
						isSelected = false;
					}
					if (flag4)
					{
						this.RenderTab(writer, this.Localization.HSBTabText, this.ClientID + "_millionColors", isSelected, flag);
						flag = true;
						isSelected = false;
					}
					if (flag5)
					{
						this.RenderTab(writer, this.Localization.HSVTabText, this.ClientID + "_millionColors", isSelected, flag);
						flag = true;
					}
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				this.RenderHeader(writer);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpViews");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				bool flag6 = true;
				if (flag2)
				{
					RadColorPicker.RenderPageView(writer, flag6, this.ClientID + "_webPalette", "rcpPageView");
					this.RenderWebPalette(writer, flag);
					writer.RenderEndTag();
					flag6 = false;
				}
				if (flag3)
				{
					RadColorPicker.RenderPageView(writer, flag6, this.ClientID + "_rgbSliders", "rcpPageView rcpRGBPageView");
					if (flag6)
					{
						this.RenderRgbSliders(writer, flag);
					}
					writer.RenderEndTag();
					flag6 = false;
				}
				if (flag4 || flag5)
				{
					string pageViewClass = "rcpPageView rcpMillionColorsPageView " + (flag4 ? "rcpHsbPageView" : "rcpHsvPageView");
					RadColorPicker.RenderPageView(writer, flag6, this.ClientID + "_millionColors", pageViewClass);
					if (flag6)
					{
						this.RenderMillionColors(writer, flag);
					}
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			if (this.hiddenSlider != null)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "-9999px");
				this.hiddenSlider.RenderControl(writer);
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002BED8 File Offset: 0x0002A0D8
		public void RenderRoundedCornersElements(HtmlTextWriter writer)
		{
			if (this.ResolvedRenderMode.Equals(RenderMode.Classic))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpRoundedRight");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpRoundedBottomRight");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcpRoundedBottomLeft");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002BF4E File Offset: 0x0002A14E
		protected override void RenderTrialMessage(HtmlTextWriter writer)
		{
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002BF50 File Offset: 0x0002A150
		internal bool IsTouchSkin()
		{
			return this.EnableEmbeddedSkins && base.RuntimeSkin.EndsWith("Touch");
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002BF6C File Offset: 0x0002A16C
		internal bool IsSkin(string targetSkin)
		{
			return base.RuntimeSkin.Equals(targetSkin);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002BF7C File Offset: 0x0002A17C
		private static string ColorToHex(Color color)
		{
			if (color.Equals(Color.Empty))
			{
				return string.Empty;
			}
			return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002BFD6 File Offset: 0x0002A1D6
		private bool IsModeEnabled(PaletteModes paletteMode)
		{
			return (this.PaletteModes & paletteMode) > (PaletteModes)0;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002BFE4 File Offset: 0x0002A1E4
		private object GetSerializedItems(JavaScriptSerializer serializer)
		{
			List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
			foreach (object obj in this.Items)
			{
				ColorPickerItem colorPickerItem = (ColorPickerItem)obj;
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary["value"] = RadColorPicker.ColorToHex(colorPickerItem.Value);
				dictionary["title"] = colorPickerItem.Title;
				list.Add(dictionary);
			}
			return serializer.Serialize(list);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002C07C File Offset: 0x0002A27C
		private void SyncronizeItemsAndPreset()
		{
			if (this.isItemsSynchronized)
			{
				return;
			}
			this.isItemsSynchronized = true;
			ColorPickerItemCollection customColors = this.GetCustomColors();
			this.Items.Clear();
			switch (this.Preset)
			{
			case ColorPreset.None:
				goto IL_2AB;
			case ColorPreset.Standard:
				this.Items.AddRange(RadColorPicker.GetStandardColors(true));
				goto IL_2AB;
			case ColorPreset.Grayscale:
				this.Items.AddRange(RadColorPicker.GetGrayscaleColors(true));
				goto IL_2AB;
			case ColorPreset.Web216:
				this.Items.AddRange(RadColorPicker.GetWeb216Colors(true));
				goto IL_2AB;
			case ColorPreset.ReallyWebSafe:
				this.Items.AddRange(RadColorPicker.GetReallyWebSafeColors(true));
				goto IL_2AB;
			case ColorPreset.Office:
				this.Items.AddRange(RadColorPicker.GetOfficeColors(true));
				goto IL_2AB;
			case ColorPreset.Apex:
				this.Items.AddRange(RadColorPicker.GetApexColors(true));
				goto IL_2AB;
			case ColorPreset.Aspect:
				this.Items.AddRange(RadColorPicker.GetAspectColors(true));
				goto IL_2AB;
			case ColorPreset.Civic:
				this.Items.AddRange(RadColorPicker.GetCivicColors(true));
				goto IL_2AB;
			case ColorPreset.Concourse:
				this.Items.AddRange(RadColorPicker.GetConcourseColors(true));
				goto IL_2AB;
			case ColorPreset.Equity:
				this.Items.AddRange(RadColorPicker.GetEquityColors(true));
				goto IL_2AB;
			case ColorPreset.Flow:
				this.Items.AddRange(RadColorPicker.GetFlowColors(true));
				goto IL_2AB;
			case ColorPreset.Foundry:
				this.Items.AddRange(RadColorPicker.GetFoundryColors(true));
				goto IL_2AB;
			case ColorPreset.Median:
				this.Items.AddRange(RadColorPicker.GetMedianColors(true));
				goto IL_2AB;
			case ColorPreset.Metro:
				this.Items.AddRange(RadColorPicker.GetMetroColors(true));
				goto IL_2AB;
			case ColorPreset.Module:
				this.Items.AddRange(RadColorPicker.GetModuleColors(true));
				goto IL_2AB;
			case ColorPreset.Opulent:
				this.Items.AddRange(RadColorPicker.GetOpulentColors(true));
				goto IL_2AB;
			case ColorPreset.Oriel:
				this.Items.AddRange(RadColorPicker.GetOrielColors(true));
				goto IL_2AB;
			case ColorPreset.Origin:
				this.Items.AddRange(RadColorPicker.GetOriginColors(true));
				goto IL_2AB;
			case ColorPreset.Paper:
				this.Items.AddRange(RadColorPicker.GetPaperColors(true));
				goto IL_2AB;
			case ColorPreset.Solstice:
				this.Items.AddRange(RadColorPicker.GetSolsticeColors(true));
				goto IL_2AB;
			case ColorPreset.Technic:
				this.Items.AddRange(RadColorPicker.GetTechnicColors(true));
				goto IL_2AB;
			case ColorPreset.Trek:
				this.Items.AddRange(RadColorPicker.GetTrekColors(true));
				goto IL_2AB;
			case ColorPreset.Urban:
				this.Items.AddRange(RadColorPicker.GetUrbanColors(true));
				goto IL_2AB;
			case ColorPreset.Verve:
				this.Items.AddRange(RadColorPicker.GetVerveColors(true));
				goto IL_2AB;
			}
			this.Items.AddRange(RadColorPicker.GetDefaultColors(true));
			IL_2AB:
			this.Items.AddRange(customColors);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002C340 File Offset: 0x0002A540
		private int GetColumnsForPreset()
		{
			switch (this.Preset)
			{
			case ColorPreset.None:
			case ColorPreset.Office:
			case ColorPreset.Apex:
			case ColorPreset.Aspect:
			case ColorPreset.Civic:
			case ColorPreset.Concourse:
			case ColorPreset.Equity:
			case ColorPreset.Flow:
			case ColorPreset.Foundry:
			case ColorPreset.Median:
			case ColorPreset.Metro:
			case ColorPreset.Module:
			case ColorPreset.Opulent:
			case ColorPreset.Oriel:
			case ColorPreset.Origin:
			case ColorPreset.Paper:
			case ColorPreset.Solstice:
			case ColorPreset.Technic:
			case ColorPreset.Trek:
			case ColorPreset.Urban:
			case ColorPreset.Verve:
				return 10;
			case ColorPreset.Standard:
				return 12;
			case ColorPreset.Grayscale:
				return 16;
			case ColorPreset.Web216:
				return 18;
			case ColorPreset.ReallyWebSafe:
				return 8;
			}
			return 18;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002C3E0 File Offset: 0x0002A5E0
		private ColorPickerItemCollection GetCustomColors()
		{
			ColorPickerItemCollection colorPickerItemCollection = new ColorPickerItemCollection();
			ColorPickerItemCollection items = this.Items;
			foreach (object obj in items)
			{
				ColorPickerItem colorPickerItem = (ColorPickerItem)obj;
				if (!colorPickerItem.IsPresetColor)
				{
					colorPickerItemCollection.Add(colorPickerItem);
				}
			}
			return colorPickerItemCollection;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002C450 File Offset: 0x0002A650
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			try
			{
				this.SelectedColor = ColorTranslator.FromHtml(clientState["selectedColor"].ToString());
			}
			catch
			{
				this.SelectedColor = Color.Empty;
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002C4A0 File Offset: 0x0002A6A0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.colors).LoadViewState(array[1]);
				this.isItemsSynchronized = true;
			}
			((IStateManager)this.Localization).LoadViewState(array[2]);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002C4E8 File Offset: 0x0002A6E8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				RadColorPicker.SaveState(this.colors),
				((IStateManager)this.Localization).SaveViewState()
			};
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002C524 File Offset: 0x0002A724
		private static object SaveState(IStateManager obj)
		{
			if (obj != null)
			{
				return obj.SaveViewState();
			}
			return null;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002C531 File Offset: 0x0002A731
		protected override void TrackViewState()
		{
			base.TrackViewState();
			RadColorPicker.TrackState(this.colors);
			((IStateManager)this.Localization).TrackViewState();
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002C54F File Offset: 0x0002A74F
		private static void TrackState(IStateManager obj)
		{
			if (obj != null)
			{
				obj.TrackViewState();
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002C55C File Offset: 0x0002A75C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_selectedColor", RadColorPicker.ColorToHex(this.SelectedColor));
			string value = this.SelectedColor.IsNamedColor ? this.SelectedColor.Name : ColorTranslator.ToHtml(this.SelectedColor);
			descriptor.AddProperty("_selectedColorName", value);
			string[] obj = new string[]
			{
				this.Localization.PickColorText,
				this.Localization.CurrentColorText,
				this.Localization.NoColorText,
				this.Localization.WebPaletteTabText,
				this.Localization.RGBSlidersTabText,
				this.Localization.HSBTabText,
				this.Localization.HSVTabText,
				this.Localization.ApplyButtonText,
				this.Localization.RGBSlidersIncreaseText,
				this.Localization.RGBSlidersDecreaseText,
				this.Localization.RGBSlidersDragText,
				this.Localization.HSBSliderDragText,
				this.Localization.HSVSliderDragText,
				this.Localization.BlankColorText,
				this.Localization.CustomColor,
				this.Localization.RecentColors,
				this.Localization.OkButtonText,
				this.Localization.CancelButtonText,
				this.Localization.HexInputTitle
			};
			descriptor.AddProperty("_localization", new JavaScriptSerializer().Serialize(obj));
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			descriptor.AddProperty("_width", this.Width.Value);
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			descriptor.AddProperty("_accessKey", this.AccessKey);
			descriptor.AddProperty("_tabIndex", this.TabIndex);
			descriptor.AddProperty("_isTouchSkin", this.IsTouchSkin());
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			if (this.ShowIcon)
			{
				JavaScriptSerializer serializer = new JavaScriptSerializer();
				descriptor.AddProperty("_items", this.GetSerializedItems(serializer));
			}
			if (this.hiddenSlider != null)
			{
				descriptor.AddComponentProperty("hiddenSlider", this.hiddenSlider.ClientID);
			}
			else
			{
				if (this.redSlider != null)
				{
					descriptor.AddComponentProperty("redSlider", this.redSlider.ClientID);
					descriptor.AddComponentProperty("greenSlider", this.greenSlider.ClientID);
					descriptor.AddComponentProperty("blueSlider", this.blueSlider.ClientID);
				}
				if (this.millionColorsSlider != null)
				{
					descriptor.AddComponentProperty("millionColorsSlider", this.millionColorsSlider.ClientID);
				}
			}
			descriptor.AddProperty("renderMode", this.ResolvedRenderMode);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002C848 File Offset: 0x0002AA48
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "_autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<int>(descriptor, "_columns", this.Columns, 18);
			base.DescribeProperty<bool>(descriptor, "_enableCustomColor", this.EnableCustomColor, false);
			base.DescribeProperty<bool>(descriptor, "keepInScreenBounds", this.KeepInScreenBounds, true);
			base.DescribeProperty<bool>(descriptor, "overlay", this.Overlay, false);
			base.DescribeProperty<PaletteModes>(descriptor, "paletteModes", this.PaletteModes, PaletteModes.WebPalette);
			base.DescribeProperty<bool>(descriptor, "_previewColor", this.PreviewColor, true);
			base.DescribeProperty<bool>(descriptor, "_showEmptyColor", this.ShowEmptyColor, true);
			base.DescribeProperty<bool>(descriptor, "_showIcon", this.ShowIcon, false);
			base.DescribeProperty<bool>(descriptor, "_showRecentColors", this.ShowRecentColors, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002C91C File Offset: 0x0002AB1C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "colorChange", this.OnClientColorChange);
			RadWebControl.DescribeEvent(descriptor, "colorChanging", this.OnClientColorChanging);
			RadWebControl.DescribeEvent(descriptor, "colorPreview", this.OnClientColorPreview);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "popUpShow", this.OnClientPopUpShow);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040002D7 RID: 727
		private static readonly object EventColorChanged = new object();

		// Token: 0x040002D8 RID: 728
		private bool _palleteRead;

		// Token: 0x040002D9 RID: 729
		private ColorPickerItemCollection colors = new ColorPickerItemCollection();

		// Token: 0x040002DA RID: 730
		private bool isItemsSynchronized;

		// Token: 0x040002DB RID: 731
		private RadSlider redSlider;

		// Token: 0x040002DC RID: 732
		private RadSlider greenSlider;

		// Token: 0x040002DD RID: 733
		private RadSlider blueSlider;

		// Token: 0x040002DE RID: 734
		private RadSlider millionColorsSlider;

		// Token: 0x040002DF RID: 735
		private RadSlider hiddenSlider;

		// Token: 0x040002E0 RID: 736
		private RadColorPickerLocalization _localization;

		// Token: 0x040002E1 RID: 737
		private int paletteWrapperBox = 3;

		// Token: 0x040002E2 RID: 738
		private int pageViewBox = 2;

		// Token: 0x040002E3 RID: 739
		private int tabStripHeight = 25;

		// Token: 0x040002E4 RID: 740
		private Dictionary<string, int> webColorBoxSizesByLargeSkin = new Dictionary<string, int>
		{
			{
				"Bootstrap",
				18
			},
			{
				"BlackMetroTouch",
				28
			},
			{
				"MetroTouch",
				28
			}
		};

		// Token: 0x040002E5 RID: 741
		private int colorBox = 2;

		// Token: 0x040002E6 RID: 742
		private int rgbSliderSize = 217;

		// Token: 0x040002E7 RID: 743
		private int rgbInputSize = 38;

		// Token: 0x040002E8 RID: 744
		private int rgbLabelSize = 15;

		// Token: 0x040002E9 RID: 745
		private int millionColorsSliderWidth = 22;

		// Token: 0x040002EA RID: 746
		private int millionColorsSliderHeight = 162;

		// Token: 0x040002EB RID: 747
		private Unit offsetWidth = Unit.Empty;

		// Token: 0x040002EC RID: 748
		private int rgbSliderValue = 255;
	}
}
