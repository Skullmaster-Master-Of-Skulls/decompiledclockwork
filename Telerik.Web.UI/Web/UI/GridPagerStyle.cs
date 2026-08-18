using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200115A RID: 4442
	public class GridPagerStyle : GridTableItemStyle
	{
		// Token: 0x0600B4B0 RID: 46256 RVA: 0x0027CB4E File Offset: 0x0027AD4E
		internal GridPagerStyle(RadGrid owner) : this(owner, null)
		{
		}

		// Token: 0x0600B4B1 RID: 46257 RVA: 0x0027CB58 File Offset: 0x0027AD58
		internal GridPagerStyle(RadGrid owner, GridTableView ownerTableView)
		{
			this.owner = owner;
			this._ownerTableView = ownerTableView;
		}

		// Token: 0x17003A55 RID: 14933
		// (get) Token: 0x0600B4B2 RID: 46258 RVA: 0x0027CB70 File Offset: 0x0027AD70
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool IsDefault
		{
			get
			{
				bool flag = base.IsDefault && base.ViewState["_m"] == null && base.ViewState["NextPageText"] == null && base.ViewState["NextPageToolTip"] == null && base.ViewState["NextPagesToolTip"] == null && base.ViewState["FirstPageToolTip"] == null && base.ViewState["LastPageToolTip"] == null && base.ViewState["PrevPageText"] == null && base.ViewState["PrevPageToolTip"] == null && base.ViewState["PrevPagesToolTip"] == null && base.ViewState["GoToPageTextBoxToolTip"] == null && base.ViewState["ChangePageSizeTextBoxToolTip"] == null && base.ViewState["ChangePageSizeButtonToolTip"] == null && base.ViewState["PageButtonCount"] == null && base.ViewState["Position"] == null && base.ViewState["PagerVisible"] == null && base.ViewState["PagerAlwaysVisible"] == null && base.ViewState["PagerEnableSEOPaging"] == null && base.ViewState["SEOPagingQueryStringKey"] == null && base.ViewState["_shpt"] == null && base.ViewState["_ptf"] == null && base.ViewState["PrevPageImageUrl"] == null && base.ViewState["NextPageImageUrl"] == null && base.ViewState["FirstPageImageUrl"] == null && base.ViewState["PageSizeLabelText"] == null && base.ViewState["PageSizes"] == null && base.ViewState["EnableAllOptionInPagerComboBox"] == null && base.ViewState["LastPageImageUrl"] == null;
				return flag && base.ViewState["UseRouting"] == null && base.ViewState["SEOPageIndexRouteParameterName"] == null && base.ViewState["SEORouteName"] == null;
			}
		}

		// Token: 0x0600B4B3 RID: 46259 RVA: 0x0027CDF0 File Offset: 0x0027AFF0
		public override void CopyFrom(Style s)
		{
			if (s != null)
			{
				base.CopyFrom(s);
				GridPagerStyle gridPagerStyle = (GridPagerStyle)s;
				if (gridPagerStyle != null)
				{
					if (gridPagerStyle.ViewState["PageSizeControlType"] != null)
					{
						this.PageSizeControlType = gridPagerStyle.PageSizeControlType;
					}
					if (gridPagerStyle.ViewState["_m"] != null)
					{
						this.Mode = gridPagerStyle.Mode;
					}
					if (gridPagerStyle.ViewState["NextPageText"] != null)
					{
						this.NextPageText = gridPagerStyle.NextPageText;
					}
					if (gridPagerStyle.ViewState["NextPageToolTip"] != null)
					{
						this.NextPageToolTip = gridPagerStyle.NextPageToolTip;
					}
					if (gridPagerStyle.ViewState["NextPagesToolTip"] != null)
					{
						this.NextPagesToolTip = gridPagerStyle.NextPagesToolTip;
					}
					if (gridPagerStyle.ViewState["FirstPageToolTip"] != null)
					{
						this.FirstPageToolTip = gridPagerStyle.FirstPageToolTip;
					}
					if (gridPagerStyle.ViewState["LastPageToolTip"] != null)
					{
						this.LastPageToolTip = gridPagerStyle.LastPageToolTip;
					}
					if (gridPagerStyle.ViewState["PrevPageToolTip"] != null)
					{
						this.PrevPageToolTip = gridPagerStyle.PrevPageToolTip;
					}
					if (gridPagerStyle.ViewState["PrevPagesToolTip"] != null)
					{
						this.PrevPagesToolTip = gridPagerStyle.PrevPagesToolTip;
					}
					if (gridPagerStyle.ViewState["GoToPageTextBoxToolTip"] != null)
					{
						this.GoToPageTextBoxToolTip = gridPagerStyle.GoToPageTextBoxToolTip;
					}
					if (gridPagerStyle.ViewState["ChangePageSizeTextBoxToolTip"] != null)
					{
						this.ChangePageSizeTextBoxToolTip = gridPagerStyle.ChangePageSizeTextBoxToolTip;
					}
					if (gridPagerStyle.ViewState["ChangePageSizeButtonToolTip"] != null)
					{
						this.ChangePageSizeButtonToolTip = gridPagerStyle.ChangePageSizeButtonToolTip;
					}
					if (gridPagerStyle.ViewState["PrevPageText"] != null)
					{
						this.PrevPageText = gridPagerStyle.PrevPageText;
					}
					if (gridPagerStyle.ViewState["PageButtonCount"] != null)
					{
						this.PageButtonCount = gridPagerStyle.PageButtonCount;
					}
					if (gridPagerStyle.ViewState["Position"] != null)
					{
						this.Position = gridPagerStyle.Position;
					}
					if (gridPagerStyle.ViewState["PagerVisible"] != null)
					{
						this.Visible = gridPagerStyle.Visible;
					}
					if (gridPagerStyle.ViewState["PagerAlwaysVisible"] != null)
					{
						this.AlwaysVisible = gridPagerStyle.AlwaysVisible;
					}
					if (gridPagerStyle.ViewState["PagerEnableSEOPaging"] != null)
					{
						this.EnableSEOPaging = gridPagerStyle.EnableSEOPaging;
					}
					if (gridPagerStyle.ViewState["SEOPagingQueryStringKey"] != null)
					{
						this.SEOPagingQueryStringKey = gridPagerStyle.SEOPagingQueryStringKey;
					}
					if (gridPagerStyle.ViewState["_shpt"] != null)
					{
						this.ShowPagerText = gridPagerStyle.ShowPagerText;
					}
					if (gridPagerStyle.ViewState["_ptf"] != null)
					{
						this.PagerTextFormat = gridPagerStyle.PagerTextFormat;
					}
					if (gridPagerStyle.ViewState["PrevPageImageUrl"] != null)
					{
						this.PrevPageImageUrl = gridPagerStyle.PrevPageImageUrl;
					}
					if (gridPagerStyle.ViewState["NextPageImageUrl"] != null)
					{
						this.NextPageImageUrl = gridPagerStyle.NextPageImageUrl;
					}
					if (gridPagerStyle.ViewState["FirstPageImageUrl"] != null)
					{
						this.FirstPageImageUrl = gridPagerStyle.FirstPageImageUrl;
					}
					if (gridPagerStyle.ViewState["LastPageImageUrl"] != null)
					{
						this.LastPageImageUrl = gridPagerStyle.LastPageImageUrl;
					}
					if (gridPagerStyle.ViewState["PageSizeLabelText"] != null)
					{
						this.PageSizeLabelText = gridPagerStyle.PageSizeLabelText;
					}
					if (gridPagerStyle.ViewState["EnableAllOptionInPagerComboBox"] != null)
					{
						this.EnableAllOptionInPagerComboBox = gridPagerStyle.EnableAllOptionInPagerComboBox;
					}
					if (gridPagerStyle.ViewState["PageSizes"] != null)
					{
						this.PageSizes = gridPagerStyle.PageSizes;
					}
					if (gridPagerStyle.ViewState["UseRouting"] != null)
					{
						this.UseRouting = gridPagerStyle.UseRouting;
					}
					if (gridPagerStyle.ViewState["SEOPageIndexRouteParameterName"] != null)
					{
						this.SEOPageIndexRouteParameterName = gridPagerStyle.SEOPageIndexRouteParameterName;
					}
					if (gridPagerStyle.ViewState["SEORouteName"] != null)
					{
						this.SEORouteName = gridPagerStyle.SEORouteName;
					}
				}
			}
		}

		// Token: 0x0600B4B4 RID: 46260 RVA: 0x0027D1BC File Offset: 0x0027B3BC
		public override void MergeWith(Style s)
		{
			if (s != null)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				GridPagerStyle gridPagerStyle = (GridPagerStyle)s;
				if (gridPagerStyle != null)
				{
					if (gridPagerStyle.ViewState["_m"] != null && base.ViewState["_m"] == null)
					{
						this.Mode = gridPagerStyle.Mode;
					}
					if (gridPagerStyle.ViewState["NextPageText"] != null && base.ViewState["NextPageText"] == null)
					{
						this.NextPageText = gridPagerStyle.NextPageText;
					}
					if (gridPagerStyle.ViewState["NextPageToolTip"] != null && base.ViewState["NextPageToolTip"] == null)
					{
						this.NextPageToolTip = gridPagerStyle.NextPageToolTip;
					}
					if (gridPagerStyle.ViewState["FirstPageToolTip"] != null && base.ViewState["FirstPageToolTip"] == null)
					{
						this.FirstPageToolTip = gridPagerStyle.FirstPageToolTip;
					}
					if (gridPagerStyle.ViewState["LastPageToolTip"] != null && base.ViewState["LastPageToolTip"] == null)
					{
						this.LastPageToolTip = gridPagerStyle.LastPageToolTip;
					}
					if (gridPagerStyle.ViewState["NextPagesToolTip"] != null && base.ViewState["NextPagesToolTip"] == null)
					{
						this.NextPagesToolTip = gridPagerStyle.NextPagesToolTip;
					}
					if (gridPagerStyle.ViewState["PrevPageToolTip"] != null && base.ViewState["PrevPageToolTip"] == null)
					{
						this.PrevPageToolTip = gridPagerStyle.PrevPageToolTip;
					}
					if (gridPagerStyle.ViewState["PrevPagesToolTip"] != null && base.ViewState["PrevPagesToolTip"] == null)
					{
						this.PrevPagesToolTip = gridPagerStyle.PrevPagesToolTip;
					}
					if (gridPagerStyle.ViewState["GoToPageTextBoxToolTip"] != null && base.ViewState["GoToPageTextBoxToolTip"] == null)
					{
						this.GoToPageTextBoxToolTip = gridPagerStyle.GoToPageTextBoxToolTip;
					}
					if (gridPagerStyle.ViewState["ChangePageSizeTextBoxToolTip"] != null && base.ViewState["ChangePageSizeTextBoxToolTip"] == null)
					{
						this.ChangePageSizeTextBoxToolTip = gridPagerStyle.ChangePageSizeTextBoxToolTip;
					}
					if (gridPagerStyle.ViewState["ChangePageSizeButtonToolTip"] != null && base.ViewState["ChangePageSizeButtonToolTip"] == null)
					{
						this.ChangePageSizeButtonToolTip = gridPagerStyle.ChangePageSizeButtonToolTip;
					}
					if (gridPagerStyle.ViewState["PrevPageText"] != null && base.ViewState["PrevPageText"] == null)
					{
						this.PrevPageText = gridPagerStyle.PrevPageText;
					}
					if (gridPagerStyle.ViewState["PageButtonCount"] != null && base.ViewState["PageButtonCount"] == null)
					{
						this.PageButtonCount = gridPagerStyle.PageButtonCount;
					}
					if (gridPagerStyle.ViewState["Position"] != null && base.ViewState["Position"] == null)
					{
						this.Position = gridPagerStyle.Position;
					}
					if (gridPagerStyle.ViewState["PagerVisible"] != null && base.ViewState["PagerVisible"] == null)
					{
						this.Visible = gridPagerStyle.Visible;
					}
					if (gridPagerStyle.ViewState["PagerAlwaysVisible"] != null && base.ViewState["PagerAlwaysVisible"] == null)
					{
						this.AlwaysVisible = gridPagerStyle.AlwaysVisible;
					}
					if (gridPagerStyle.ViewState["PagerEnableSEOPaging"] != null && base.ViewState["PagerEnableSEOPaging"] == null)
					{
						this.EnableSEOPaging = gridPagerStyle.EnableSEOPaging;
					}
					if (gridPagerStyle.ViewState["SEOPagingQueryStringKey"] != null && base.ViewState["SEOPagingQueryStringKey"] == null)
					{
						this.SEOPagingQueryStringKey = gridPagerStyle.SEOPagingQueryStringKey;
					}
					if (gridPagerStyle.ViewState["_shpt"] != null && base.ViewState["_shpt"] == null)
					{
						this.ShowPagerText = gridPagerStyle.ShowPagerText;
					}
					if (gridPagerStyle.ViewState["_ptf"] != null && base.ViewState["_ptf"] == null)
					{
						this.PagerTextFormat = gridPagerStyle.PagerTextFormat;
					}
					if (gridPagerStyle.ViewState["PrevPageImageUrl"] != null && base.ViewState["PrevPageImageUrl"] == null)
					{
						this.PrevPageImageUrl = gridPagerStyle.PrevPageImageUrl;
					}
					if (gridPagerStyle.ViewState["NextPageImageUrl"] != null && base.ViewState["NextPageImageUrl"] == null)
					{
						this.NextPageImageUrl = gridPagerStyle.NextPageImageUrl;
					}
					if (gridPagerStyle.ViewState["FirstPageImageUrl"] != null && base.ViewState["FirstPageImageUrl"] == null)
					{
						this.FirstPageImageUrl = gridPagerStyle.FirstPageImageUrl;
					}
					if (gridPagerStyle.ViewState["LastPageImageUrl"] != null && base.ViewState["LastPageImageUrl"] == null)
					{
						this.LastPageImageUrl = gridPagerStyle.LastPageImageUrl;
					}
					if (gridPagerStyle.ViewState["EnableAllOptionInPagerComboBox"] != null && base.ViewState["EnableAllOptionInPagerComboBox"] == null)
					{
						this.EnableAllOptionInPagerComboBox = gridPagerStyle.EnableAllOptionInPagerComboBox;
					}
					if (gridPagerStyle.ViewState["PageSizes"] != null && base.ViewState["PageSizes"] == null)
					{
						this.PageSizes = gridPagerStyle.PageSizes;
					}
					if (gridPagerStyle.ViewState["UseRouting"] != null && base.ViewState["UseRouting"] == null)
					{
						this.UseRouting = gridPagerStyle.UseRouting;
					}
					if (gridPagerStyle.ViewState["SEOPageIndexRouteParameterName"] != null && base.ViewState["SEOPageIndexRouteParameterName"] == null)
					{
						this.SEOPageIndexRouteParameterName = gridPagerStyle.SEOPageIndexRouteParameterName;
					}
					if (gridPagerStyle.ViewState["SEORouteName"] != null && base.ViewState["SEORouteName"] == null)
					{
						this.SEORouteName = gridPagerStyle.SEORouteName;
					}
				}
			}
		}

		// Token: 0x0600B4B5 RID: 46261 RVA: 0x0027D764 File Offset: 0x0027B964
		public override void Reset()
		{
			if (base.ViewState["_m"] != null)
			{
				base.ViewState.Remove("_m");
			}
			if (base.ViewState["NextPageText"] != null)
			{
				base.ViewState.Remove("NextPageText");
			}
			if (base.ViewState["NextPageToolTip"] != null)
			{
				base.ViewState.Remove("NextPageToolTip");
			}
			if (base.ViewState["PrevPageToolTip"] != null)
			{
				base.ViewState.Remove("PrevPageToolTip");
			}
			if (base.ViewState["NextPagesToolTip"] != null)
			{
				base.ViewState.Remove("NextPagesToolTip");
			}
			if (base.ViewState["FirstPageToolTip"] != null)
			{
				base.ViewState.Remove("FirstPageToolTip");
			}
			if (base.ViewState["LastPageToolTip"] != null)
			{
				base.ViewState.Remove("LastPageToolTip");
			}
			if (base.ViewState["PrevPagesToolTip"] != null)
			{
				base.ViewState.Remove("PrevPagesToolTip");
			}
			if (base.ViewState["GoToPageTextBoxToolTip"] != null)
			{
				base.ViewState.Remove("GoToPageTextBoxToolTip");
			}
			if (base.ViewState["ChangePageSizeTextBoxToolTip"] != null)
			{
				base.ViewState.Remove("ChangePageSizeTextBoxToolTip");
			}
			if (base.ViewState["ChangePageSizeButtonToolTip"] != null)
			{
				base.ViewState.Remove("ChangePageSizeButtonToolTip");
			}
			if (base.ViewState["PrevPageText"] != null)
			{
				base.ViewState.Remove("PrevPageText");
			}
			if (base.ViewState["PageButtonCount"] != null)
			{
				base.ViewState.Remove("PageButtonCount");
			}
			if (base.ViewState["Position"] != null)
			{
				base.ViewState.Remove("Position");
			}
			if (base.ViewState["PagerVisible"] != null)
			{
				base.ViewState.Remove("PagerVisible");
			}
			if (base.ViewState["PagerAlwaysVisible"] != null)
			{
				base.ViewState.Remove("PagerAlwaysVisible");
			}
			if (base.ViewState["PagerEnableSEOPaging"] != null)
			{
				base.ViewState.Remove("PagerEnableSEOPaging");
			}
			if (base.ViewState["SEOPagingQueryStringKey"] != null)
			{
				base.ViewState.Remove("SEOPagingQueryStringKey");
			}
			if (base.ViewState["_shpt"] != null)
			{
				base.ViewState.Remove("_shpt");
			}
			if (base.ViewState["_ptf"] != null)
			{
				base.ViewState.Remove("_ptf");
			}
			if (base.ViewState["PrevPageImageUrl"] != null)
			{
				base.ViewState.Remove("PrevPageImageUrl");
			}
			if (base.ViewState["NextPageImageUrl"] != null)
			{
				base.ViewState.Remove("NextPageImageUrl");
			}
			if (base.ViewState["FirstPageImageUrl"] != null)
			{
				base.ViewState.Remove("FirstPageImageUrl");
			}
			if (base.ViewState["LastPageImageUrl"] != null)
			{
				base.ViewState.Remove("LastPageImageUrl");
			}
			if (base.ViewState["EnableAllOptionInPagerComboBox"] != null)
			{
				base.ViewState.Remove("EnableAllOptionInPagerComboBox");
			}
			if (base.ViewState["PageSizes"] != null)
			{
				base.ViewState.Remove("PageSizes");
			}
			if (base.ViewState["UseRouting"] != null)
			{
				base.ViewState.Remove("UseRouting");
			}
			if (base.ViewState["SEOPageIndexRouteParameterName"] != null)
			{
				base.ViewState.Remove("SEOPageIndexRouteParameterName");
			}
			if (base.ViewState["SEORouteName"] != null)
			{
				base.ViewState.Remove("SEORouteName");
			}
			base.Reset();
		}

		// Token: 0x17003A56 RID: 14934
		// (get) Token: 0x0600B4B6 RID: 46262 RVA: 0x0027DB54 File Offset: 0x0027BD54
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPagerOnBottom
		{
			get
			{
				GridPagerPosition position = this.Position;
				return position == GridPagerPosition.Bottom || position == GridPagerPosition.TopAndBottom;
			}
		}

		// Token: 0x17003A57 RID: 14935
		// (get) Token: 0x0600B4B7 RID: 46263 RVA: 0x0027DB74 File Offset: 0x0027BD74
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsPagerOnTop
		{
			get
			{
				GridPagerPosition position = this.Position;
				return position == GridPagerPosition.Top || position == GridPagerPosition.TopAndBottom;
			}
		}

		// Token: 0x17003A58 RID: 14936
		// (get) Token: 0x0600B4B8 RID: 46264 RVA: 0x0027DB94 File Offset: 0x0027BD94
		// (set) Token: 0x0600B4B9 RID: 46265 RVA: 0x0027DBBD File Offset: 0x0027BDBD
		[DefaultValue(typeof(GridPagerMode), "NextPrevAndNumeric")]
		[Description("RadGridPagerStyle_Mode")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Bindable(true)]
		public GridPagerMode Mode
		{
			get
			{
				object obj = base.ViewState["_m"];
				if (obj == null)
				{
					return GridPagerMode.NextPrevAndNumeric;
				}
				return (GridPagerMode)obj;
			}
			set
			{
				if (value < GridPagerMode.NextPrev || value > GridPagerMode.Slider)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["_m"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A59 RID: 14937
		// (get) Token: 0x0600B4BA RID: 46266 RVA: 0x0027DBF0 File Offset: 0x0027BDF0
		// (set) Token: 0x0600B4BB RID: 46267 RVA: 0x0027DC38 File Offset: 0x0027BE38
		[DefaultValue(null)]
		[Description("Gets/sets a comma/semicolon delimited list of page size values.")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(IntegerArrayConverter))]
		[Category("Data")]
		public int[] PageSizes
		{
			get
			{
				int[] array = base.ViewState["PageSizes"] as int[];
				if (this.EnableAllOptionInPagerComboBox && array != null)
				{
					int num = array.Length;
					Array.Resize<int>(ref array, num + 1);
					array[num] = int.MaxValue;
				}
				return array;
			}
			set
			{
				if (value != null && value.Length > 0)
				{
					List<int> list = new List<int>();
					Array.Sort<int>(value);
					list.Add(value[0]);
					for (int i = 1; i < value.Length; i++)
					{
						if (value[i] != value[i - 1])
						{
							list.Add(value[i]);
						}
					}
					base.ViewState["PageSizes"] = list.ToArray();
					return;
				}
				base.ViewState["PageSizes"] = null;
			}
		}

		// Token: 0x0600B4BC RID: 46268 RVA: 0x0027DCAE File Offset: 0x0027BEAE
		private string GetLocalizationString(TFunc<GridStrings, string> extractor)
		{
			return this.GetLocalizationString(extractor, string.Empty);
		}

		// Token: 0x0600B4BD RID: 46269 RVA: 0x0027DCBC File Offset: 0x0027BEBC
		private string GetLocalizationString(TFunc<GridStrings, string> extractor, string defaultValue)
		{
			if (this.Owner != null)
			{
				return extractor(this.Owner.Localization);
			}
			return defaultValue;
		}

		// Token: 0x17003A5A RID: 14938
		// (get) Token: 0x0600B4BE RID: 46270 RVA: 0x0027DCE4 File Offset: 0x0027BEE4
		// (set) Token: 0x0600B4BF RID: 46271 RVA: 0x0027DD2F File Offset: 0x0027BF2F
		[Category("Appearance")]
		[Bindable(true)]
		[Description("RadGridPagerStyle_NextPageText")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("")]
		public string NextPageText
		{
			get
			{
				object obj = base.ViewState["NextPageText"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.NextPageText);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["NextPageText"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A5B RID: 14939
		// (get) Token: 0x0600B4C0 RID: 46272 RVA: 0x0027DD54 File Offset: 0x0027BF54
		// (set) Token: 0x0600B4C1 RID: 46273 RVA: 0x0027DD9F File Offset: 0x0027BF9F
		[Bindable(true)]
		[Category("Appearance")]
		[Description("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string LastPageText
		{
			get
			{
				object obj = base.ViewState["LastPageText"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.LastPageText);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["LastPageText"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x0600B4C2 RID: 46274 RVA: 0x0027DDBC File Offset: 0x0027BFBC
		private string GetSkinSpecificImageFileExtension()
		{
			string runtimeSkin;
			if ((runtimeSkin = this.owner.RuntimeSkin) != null && runtimeSkin == "Transparent")
			{
				return "png";
			}
			return "gif";
		}

		// Token: 0x17003A5C RID: 14940
		// (get) Token: 0x0600B4C3 RID: 46275 RVA: 0x0027DDF0 File Offset: 0x0027BFF0
		// (set) Token: 0x0600B4C4 RID: 46276 RVA: 0x0027DEBE File Offset: 0x0027C0BE
		[UrlProperty]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public virtual string PrevPageImageUrl
		{
			get
			{
				object obj = base.ViewState["PrevPageImageUrl"];
				if (obj != null && this.owner != null)
				{
					return this.owner.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return "";
				}
				string text = "PagingPrev." + this.GetSkinSpecificImageFileExtension();
				if (!this.EnableSEOPaging || !string.IsNullOrEmpty(this.owner.ImagesPath.Trim()))
				{
					return this.owner.ResolveGridImageUrl(text);
				}
				if (this.owner.EmptySkin())
				{
					return SkinRegistrar.GetWebResourceUrl(this.Owner, string.Format("Telerik.Web.UI.Skins.Default.Grid.{0}", text));
				}
				return SkinRegistrar.GetWebResourceUrl(this.Owner, string.Format("Telerik.Web.UI.Skins.{0}.Grid.{1}", this.owner.RuntimeSkin, text));
			}
			set
			{
				base.ViewState["PrevPageImageUrl"] = value;
			}
		}

		// Token: 0x0600B4C5 RID: 46277 RVA: 0x0027DED1 File Offset: 0x0027C0D1
		protected virtual bool ShouldSerializePrevPageImageUrl()
		{
			return this.owner != null && this.owner.ShouldSerializeImageUrl(this.PrevPageImageUrl);
		}

		// Token: 0x17003A5D RID: 14941
		// (get) Token: 0x0600B4C6 RID: 46278 RVA: 0x0027DEF0 File Offset: 0x0027C0F0
		// (set) Token: 0x0600B4C7 RID: 46279 RVA: 0x0027DFBE File Offset: 0x0027C1BE
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[UrlProperty]
		public virtual string NextPageImageUrl
		{
			get
			{
				object obj = base.ViewState["NextPageImageUrl"];
				if (obj != null && this.owner != null)
				{
					return this.owner.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return "";
				}
				string text = "PagingNext." + this.GetSkinSpecificImageFileExtension();
				if (!this.EnableSEOPaging || !string.IsNullOrEmpty(this.owner.ImagesPath.Trim()))
				{
					return this.owner.ResolveGridImageUrl(text);
				}
				if (this.owner.EmptySkin())
				{
					return SkinRegistrar.GetWebResourceUrl(this.Owner, string.Format("Telerik.Web.UI.Skins.Default.Grid.{0}", text));
				}
				return SkinRegistrar.GetWebResourceUrl(this.Owner, string.Format("Telerik.Web.UI.Skins.{0}.Grid.{1}", this.owner.RuntimeSkin, text));
			}
			set
			{
				base.ViewState["NextPageImageUrl"] = value;
			}
		}

		// Token: 0x0600B4C8 RID: 46280 RVA: 0x0027DFD1 File Offset: 0x0027C1D1
		protected virtual bool ShouldSerializeNextPageImageUrl()
		{
			return this.owner != null && this.owner.ShouldSerializeImageUrl(this.NextPageImageUrl);
		}

		// Token: 0x17003A5E RID: 14942
		// (get) Token: 0x0600B4C9 RID: 46281 RVA: 0x0027DFF0 File Offset: 0x0027C1F0
		// (set) Token: 0x0600B4CA RID: 46282 RVA: 0x0027E0BE File Offset: 0x0027C2BE
		[UrlProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public virtual string FirstPageImageUrl
		{
			get
			{
				object obj = base.ViewState["FirstPageImageUrl"];
				if (obj != null && this.owner != null)
				{
					return this.owner.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return "";
				}
				string text = "PagingFirst." + this.GetSkinSpecificImageFileExtension();
				if (!this.EnableSEOPaging || !string.IsNullOrEmpty(this.owner.ImagesPath.Trim()))
				{
					return this.owner.ResolveGridImageUrl(text);
				}
				if (this.owner.EmptySkin())
				{
					return SkinRegistrar.GetWebResourceUrl(this.Owner, string.Format("Telerik.Web.UI.Skins.Default.Grid.{0}", text));
				}
				return SkinRegistrar.GetWebResourceUrl(this.Owner, string.Format("Telerik.Web.UI.Skins.{0}.Grid.{1}", this.owner.RuntimeSkin, text));
			}
			set
			{
				base.ViewState["FirstPageImageUrl"] = value;
			}
		}

		// Token: 0x0600B4CB RID: 46283 RVA: 0x0027E0D1 File Offset: 0x0027C2D1
		protected virtual bool ShouldSerializeFirstPageImageUrl()
		{
			return this.owner != null && this.owner.ShouldSerializeImageUrl(this.FirstPageImageUrl);
		}

		// Token: 0x17003A5F RID: 14943
		// (get) Token: 0x0600B4CC RID: 46284 RVA: 0x0027E0F0 File Offset: 0x0027C2F0
		// (set) Token: 0x0600B4CD RID: 46285 RVA: 0x0027E1C9 File Offset: 0x0027C3C9
		[UrlProperty]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		public virtual string LastPageImageUrl
		{
			get
			{
				object obj = base.ViewState["LastPageImageUrl"];
				if (obj != null && this.owner != null)
				{
					return this.owner.ResolveUrl((string)obj);
				}
				if (this.owner == null || this.owner == null)
				{
					return "";
				}
				string text = "PagingLast." + this.GetSkinSpecificImageFileExtension();
				if (!this.EnableSEOPaging || !string.IsNullOrEmpty(this.owner.ImagesPath.Trim()))
				{
					return this.owner.ResolveGridImageUrl(text);
				}
				if (this.owner.EmptySkin())
				{
					return SkinRegistrar.GetWebResourceUrl(this.Owner, string.Format("Telerik.Web.UI.Skins.Default.Grid.{0}", text));
				}
				return SkinRegistrar.GetWebResourceUrl(this.Owner, string.Format("Telerik.Web.UI.Skins.{0}.Grid.{1}", this.owner.RuntimeSkin, text));
			}
			set
			{
				base.ViewState["LastPageImageUrl"] = value;
			}
		}

		// Token: 0x0600B4CE RID: 46286 RVA: 0x0027E1DC File Offset: 0x0027C3DC
		protected virtual bool ShouldSerializeLastPageImageUrl()
		{
			return this.owner != null && this.owner.ShouldSerializeImageUrl(this.LastPageImageUrl);
		}

		// Token: 0x17003A60 RID: 14944
		// (get) Token: 0x0600B4CF RID: 46287 RVA: 0x0027E204 File Offset: 0x0027C404
		// (set) Token: 0x0600B4D0 RID: 46288 RVA: 0x0027E254 File Offset: 0x0027C454
		[Bindable(true)]
		[Localizable(true)]
		[Category("Appearance")]
		[Description("FirstPageToolTip")]
		[DefaultValue("First Page")]
		[NotifyParentProperty(true)]
		public string FirstPageToolTip
		{
			get
			{
				object obj = base.ViewState["FirstPageToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.FirstPageToolTip, "First Page");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["FirstPageToolTip"] = value;
			}
		}

		// Token: 0x17003A61 RID: 14945
		// (get) Token: 0x0600B4D1 RID: 46289 RVA: 0x0027E270 File Offset: 0x0027C470
		// (set) Token: 0x0600B4D2 RID: 46290 RVA: 0x0027E2C0 File Offset: 0x0027C4C0
		[DefaultValue("Next Page")]
		[Localizable(true)]
		[Category("Appearance")]
		[Description("NextPageToolTip")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		public string NextPageToolTip
		{
			get
			{
				object obj = base.ViewState["NextPageToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.NextPageToolTip, "Next Page");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["NextPageToolTip"] = value;
			}
		}

		// Token: 0x17003A62 RID: 14946
		// (get) Token: 0x0600B4D3 RID: 46291 RVA: 0x0027E2DC File Offset: 0x0027C4DC
		// (set) Token: 0x0600B4D4 RID: 46292 RVA: 0x0027E32C File Offset: 0x0027C52C
		[DefaultValue("Last Page")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("LastPageToolTip")]
		[Category("Appearance")]
		[Bindable(true)]
		public string LastPageToolTip
		{
			get
			{
				object obj = base.ViewState["LastPageToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.LastPageToolTip, "Last Page");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["LastPageToolTip"] = value;
			}
		}

		// Token: 0x17003A63 RID: 14947
		// (get) Token: 0x0600B4D5 RID: 46293 RVA: 0x0027E348 File Offset: 0x0027C548
		// (set) Token: 0x0600B4D6 RID: 46294 RVA: 0x0027E398 File Offset: 0x0027C598
		[Category("Appearance")]
		[DefaultValue("Previous Page")]
		[NotifyParentProperty(true)]
		[Description("PrevPageToolTip")]
		[Bindable(true)]
		[Localizable(true)]
		public string PrevPageToolTip
		{
			get
			{
				object obj = base.ViewState["PrevPageToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.PrevPageToolTip, "Previous Page");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PrevPageToolTip"] = value;
			}
		}

		// Token: 0x17003A64 RID: 14948
		// (get) Token: 0x0600B4D7 RID: 46295 RVA: 0x0027E3B4 File Offset: 0x0027C5B4
		// (set) Token: 0x0600B4D8 RID: 46296 RVA: 0x0027E404 File Offset: 0x0027C604
		[Bindable(true)]
		[Description("NextPagesToolTip")]
		[DefaultValue("Next Pages")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string NextPagesToolTip
		{
			get
			{
				object obj = base.ViewState["NextPagesToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.NextPagesToolTip, "Next Pages");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["NextPagesToolTip"] = value;
			}
		}

		// Token: 0x17003A65 RID: 14949
		// (get) Token: 0x0600B4D9 RID: 46297 RVA: 0x0027E420 File Offset: 0x0027C620
		// (set) Token: 0x0600B4DA RID: 46298 RVA: 0x0027E470 File Offset: 0x0027C670
		[Description("PrevPagesToolTip")]
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("Previous Pages")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string PrevPagesToolTip
		{
			get
			{
				object obj = base.ViewState["PrevPagesToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.PrevPagesToolTip, "Previous Pages");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PrevPagesToolTip"] = value;
			}
		}

		// Token: 0x17003A66 RID: 14950
		// (get) Token: 0x0600B4DB RID: 46299 RVA: 0x0027E48C File Offset: 0x0027C68C
		// (set) Token: 0x0600B4DC RID: 46300 RVA: 0x0027E4D7 File Offset: 0x0027C6D7
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("The ToolTip that will be applied to the GoToPage TextBox control")]
		[DefaultValue("")]
		[Bindable(true)]
		public string GoToPageTextBoxToolTip
		{
			get
			{
				object obj = base.ViewState["GoToPageTextBoxToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.GoToPageTextBoxToolTip);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageTextBoxToolTip"] = value;
			}
		}

		// Token: 0x17003A67 RID: 14951
		// (get) Token: 0x0600B4DD RID: 46301 RVA: 0x0027E4F4 File Offset: 0x0027C6F4
		// (set) Token: 0x0600B4DE RID: 46302 RVA: 0x0027E544 File Offset: 0x0027C744
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[DefaultValue("Go to Page")]
		[Localizable(true)]
		[Description("The ToolTip that will be applied to the GoToPage input element")]
		[Category("Appearance")]
		public string GoToPageButtonToolTip
		{
			get
			{
				object obj = base.ViewState["GoToPageButtonToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.GoToPageButtonToolTip, "Go to Page");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageButtonToolTip"] = value;
			}
		}

		// Token: 0x17003A68 RID: 14952
		// (get) Token: 0x0600B4DF RID: 46303 RVA: 0x0027E560 File Offset: 0x0027C760
		// (set) Token: 0x0600B4E0 RID: 46304 RVA: 0x0027E5AB File Offset: 0x0027C7AB
		[Bindable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("The ToolTip that will be applied to the ChangePageSize TextBox control")]
		[Category("Appearance")]
		public string ChangePageSizeTextBoxToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeTextBoxToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.ChangePageSizeTextBoxToolTip);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeTextBoxToolTip"] = value;
			}
		}

		// Token: 0x17003A69 RID: 14953
		// (get) Token: 0x0600B4E1 RID: 46305 RVA: 0x0027E5C8 File Offset: 0x0027C7C8
		// (set) Token: 0x0600B4E2 RID: 46306 RVA: 0x0027E618 File Offset: 0x0027C818
		[Description("The ToolTip that will be applied to the ChangePageSize Button control")]
		[Bindable(true)]
		[DefaultValue("Change Page Size")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Appearance")]
		public string ChangePageSizeButtonToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeButtonToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.ChangePageSizeButtonToolTip, "Change Page Size");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeButtonToolTip"] = value;
			}
		}

		// Token: 0x17003A6A RID: 14954
		// (get) Token: 0x0600B4E3 RID: 46307 RVA: 0x0027E634 File Offset: 0x0027C834
		// (set) Token: 0x0600B4E4 RID: 46308 RVA: 0x0027E684 File Offset: 0x0027C884
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Localizable(true)]
		[Description("The summary attribute that will be applied to the table which holds the ChangePageSize RadComboBox control")]
		[Category("Appearance")]
		public string ChangePageSizeComboBoxTableSummary
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeComboBoxTableSummary"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.ChangePageSizeComboBoxTableSummary, "");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeComboBoxTableSummary"] = value;
			}
		}

		// Token: 0x17003A6B RID: 14955
		// (get) Token: 0x0600B4E5 RID: 46309 RVA: 0x0027E6A0 File Offset: 0x0027C8A0
		// (set) Token: 0x0600B4E6 RID: 46310 RVA: 0x0027E6F0 File Offset: 0x0027C8F0
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Localizable(true)]
		[Description("The ToolTip that will be applied to the input element in the ChangePageSize RadComboBox control")]
		[Category("Appearance")]
		public string ChangePageSizeComboBoxToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeComboBoxToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.ChangePageSizeComboBoxToolTip, "");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeComboBoxToolTip"] = value;
			}
		}

		// Token: 0x17003A6C RID: 14956
		// (get) Token: 0x0600B4E7 RID: 46311 RVA: 0x0027E70C File Offset: 0x0027C90C
		// (set) Token: 0x0600B4E8 RID: 46312 RVA: 0x0027E75C File Offset: 0x0027C95C
		[Description("PageSizeLabelText")]
		[Localizable(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("Page size:")]
		[NotifyParentProperty(true)]
		public string PageSizeLabelText
		{
			get
			{
				object obj = base.ViewState["PageSizeLabelText"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.PageSizeLabelText, "Page size:");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSizeLabelText"] = value;
			}
		}

		// Token: 0x17003A6D RID: 14957
		// (get) Token: 0x0600B4E9 RID: 46313 RVA: 0x0027E770 File Offset: 0x0027C970
		// (set) Token: 0x0600B4EA RID: 46314 RVA: 0x0027E79A File Offset: 0x0027C99A
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(10)]
		[Description("RadGridPagerStyle_PageButtonCount")]
		[Category("Behavior")]
		public int PageButtonCount
		{
			get
			{
				object obj = base.ViewState["PageButtonCount"];
				if (obj == null)
				{
					return 10;
				}
				return (int)obj;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["PageButtonCount"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A6E RID: 14958
		// (get) Token: 0x0600B4EB RID: 46315 RVA: 0x0027E7C8 File Offset: 0x0027C9C8
		// (set) Token: 0x0600B4EC RID: 46316 RVA: 0x0027E7F1 File Offset: 0x0027C9F1
		[DefaultValue(typeof(GridPagerPosition), "Bottom")]
		[Category("Layout")]
		[Description("RadGridPagerStyle_Position")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		public GridPagerPosition Position
		{
			get
			{
				object obj = base.ViewState["Position"];
				if (obj == null)
				{
					return GridPagerPosition.Bottom;
				}
				return (GridPagerPosition)obj;
			}
			set
			{
				if (value < GridPagerPosition.Bottom || value > GridPagerPosition.TopAndBottom)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Position"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A6F RID: 14959
		// (get) Token: 0x0600B4ED RID: 46317 RVA: 0x0027E82C File Offset: 0x0027CA2C
		// (set) Token: 0x0600B4EE RID: 46318 RVA: 0x0027E877 File Offset: 0x0027CA77
		[DefaultValue("")]
		[Bindable(true)]
		[Category("Appearance")]
		[Localizable(true)]
		[Description("RadGridPagerStyle_PrevPageText")]
		[NotifyParentProperty(true)]
		public string PrevPageText
		{
			get
			{
				object obj = base.ViewState["PrevPageText"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.PrevPageText);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PrevPageText"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A70 RID: 14960
		// (get) Token: 0x0600B4EF RID: 46319 RVA: 0x0027E89C File Offset: 0x0027CA9C
		// (set) Token: 0x0600B4F0 RID: 46320 RVA: 0x0027E8E7 File Offset: 0x0027CAE7
		[Bindable(true)]
		[DefaultValue("")]
		[Description("")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Localizable(true)]
		public string FirstPageText
		{
			get
			{
				object obj = base.ViewState["FirstPageText"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.FirstPageText);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["FirstPageText"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A71 RID: 14961
		// (get) Token: 0x0600B4F1 RID: 46321 RVA: 0x0027E904 File Offset: 0x0027CB04
		// (set) Token: 0x0600B4F2 RID: 46322 RVA: 0x0027E94E File Offset: 0x0027CB4E
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[DefaultValue(true)]
		[Description("RadGridPagerStyle_Visible")]
		[Category("Appearance")]
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["PagerVisible"];
				if (obj == null)
				{
					return this._ownerTableView == null || this.owner == null || this.owner.PagerStyle.Visible;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["PagerVisible"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A72 RID: 14962
		// (get) Token: 0x0600B4F3 RID: 46323 RVA: 0x0027E970 File Offset: 0x0027CB70
		// (set) Token: 0x0600B4F4 RID: 46324 RVA: 0x0027E9C0 File Offset: 0x0027CBC0
		[DefaultValue(false)]
		[Description("RadGridPagerStyle_Visible")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Appearance")]
		public bool AlwaysVisible
		{
			get
			{
				object obj = base.ViewState["PagerAlwaysVisible"];
				bool result;
				if (obj == null)
				{
					result = (this._ownerTableView != null && this.Owner != null && this.Owner.PagerStyle.AlwaysVisible);
				}
				else
				{
					result = (bool)obj;
				}
				return result;
			}
			set
			{
				base.ViewState["PagerAlwaysVisible"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A73 RID: 14963
		// (get) Token: 0x0600B4F5 RID: 46325 RVA: 0x0027E9E0 File Offset: 0x0027CBE0
		// (set) Token: 0x0600B4F6 RID: 46326 RVA: 0x0027EA09 File Offset: 0x0027CC09
		[DefaultValue(false)]
		public bool EnableAllOptionInPagerComboBox
		{
			get
			{
				object obj = base.ViewState["EnableAllOptionInPagerComboBox"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableAllOptionInPagerComboBox"] = value;
			}
		}

		// Token: 0x17003A74 RID: 14964
		// (get) Token: 0x0600B4F7 RID: 46327 RVA: 0x0027EA24 File Offset: 0x0027CC24
		// (set) Token: 0x0600B4F8 RID: 46328 RVA: 0x0027EA4D File Offset: 0x0027CC4D
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("Get or set is SEO paging enabled")]
		[DefaultValue(false)]
		[Bindable(true)]
		public bool EnableSEOPaging
		{
			get
			{
				object obj = base.ViewState["PagerEnableSEOPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["PagerEnableSEOPaging"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A75 RID: 14965
		// (get) Token: 0x0600B4F9 RID: 46329 RVA: 0x0027EA6C File Offset: 0x0027CC6C
		// (set) Token: 0x0600B4FA RID: 46330 RVA: 0x0027EA99 File Offset: 0x0027CC99
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Description("Get or set is SEO paging QueryString key")]
		public string SEOPagingQueryStringKey
		{
			get
			{
				object obj = base.ViewState["SEOPagingQueryStringKey"];
				if (obj == null)
				{
					return "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["SEOPagingQueryStringKey"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A76 RID: 14966
		// (get) Token: 0x0600B4FB RID: 46331 RVA: 0x0027EAB3 File Offset: 0x0027CCB3
		// (set) Token: 0x0600B4FC RID: 46332 RVA: 0x0027EAD4 File Offset: 0x0027CCD4
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether URL Routing is enabled for the current web application.")]
		[Bindable(true)]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public bool UseRouting
		{
			get
			{
				return (bool)(base.ViewState["UseRouting"] ?? false);
			}
			set
			{
				base.ViewState["UseRouting"] = value;
			}
		}

		// Token: 0x17003A77 RID: 14967
		// (get) Token: 0x0600B4FD RID: 46333 RVA: 0x0027EAEC File Offset: 0x0027CCEC
		// (set) Token: 0x0600B4FE RID: 46334 RVA: 0x0027EB0C File Offset: 0x0027CD0C
		[Category("Data")]
		[DefaultValue("")]
		[Bindable(true)]
		[Description("Gets or sets the name of the URL parameter that specifies the page number when SEO paging and routing are enabled.")]
		[NotifyParentProperty(true)]
		public string SEOPageIndexRouteParameterName
		{
			get
			{
				return (string)(base.ViewState["SEOPageIndexRouteParameterName"] ?? "");
			}
			set
			{
				base.ViewState["SEOPageIndexRouteParameterName"] = value;
			}
		}

		// Token: 0x17003A78 RID: 14968
		// (get) Token: 0x0600B4FF RID: 46335 RVA: 0x0027EB1F File Offset: 0x0027CD1F
		// (set) Token: 0x0600B500 RID: 46336 RVA: 0x0027EB3F File Offset: 0x0027CD3F
		[DefaultValue("")]
		[Category("Data")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of the route that is used when SEO paging and routing are enabled.")]
		public string SEORouteName
		{
			get
			{
				return (string)(base.ViewState["SEORouteName"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SEORouteName"] = value;
			}
		}

		// Token: 0x17003A79 RID: 14969
		// (get) Token: 0x0600B501 RID: 46337 RVA: 0x0027EB52 File Offset: 0x0027CD52
		// (set) Token: 0x0600B502 RID: 46338 RVA: 0x0027EB5A File Offset: 0x0027CD5A
		[NotifyParentProperty(true)]
		public override HorizontalAlign HorizontalAlign
		{
			get
			{
				return base.HorizontalAlign;
			}
			set
			{
				base.HorizontalAlign = value;
			}
		}

		// Token: 0x17003A7A RID: 14970
		// (get) Token: 0x0600B503 RID: 46339 RVA: 0x0027EB64 File Offset: 0x0027CD64
		// (set) Token: 0x0600B504 RID: 46340 RVA: 0x0027EB8D File Offset: 0x0027CD8D
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ShowPagerText
		{
			get
			{
				object obj = base.ViewState["_shpt"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["_shpt"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A7B RID: 14971
		// (get) Token: 0x0600B505 RID: 46341 RVA: 0x0027EBB4 File Offset: 0x0027CDB4
		// (set) Token: 0x0600B506 RID: 46342 RVA: 0x0027EC04 File Offset: 0x0027CE04
		[Localizable(true)]
		[DefaultValue("Change page: {4} &nbsp;Page <strong>{0}</strong> of <strong>{1}</strong>, items <strong>{2}</strong> to <strong>{3}</strong> of <strong>{5}</strong>.")]
		[NotifyParentProperty(true)]
		[Description("Pager description text format. The parameters {0) - {5} are mandatory. See API reference for details.")]
		public string PagerTextFormat
		{
			get
			{
				object obj = base.ViewState["_ptf"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.PagerTextFormat, "Change page: {4} &nbsp;Page <strong>{0}</strong> of <strong>{1}</strong>, items <strong>{2}</strong> to <strong>{3}</strong> of <strong>{5}</strong>.");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["_ptf"] = value;
				RadGrid radGrid = this.owner;
			}
		}

		// Token: 0x17003A7C RID: 14972
		// (get) Token: 0x0600B507 RID: 46343 RVA: 0x0027EC1E File Offset: 0x0027CE1E
		internal bool IsPagerTextFormatChanged
		{
			get
			{
				return this.PagerTextFormat != "Change page: {4} &nbsp;Page <strong>{0}</strong> of <strong>{1}</strong>, items <strong>{2}</strong> to <strong>{3}</strong> of <strong>{5}</strong>.";
			}
		}

		// Token: 0x17003A7D RID: 14973
		// (get) Token: 0x0600B508 RID: 46344 RVA: 0x0027EC30 File Offset: 0x0027CE30
		// (set) Token: 0x0600B509 RID: 46345 RVA: 0x0027EC38 File Offset: 0x0027CE38
		internal RadGrid Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		// Token: 0x17003A7E RID: 14974
		// (get) Token: 0x0600B50A RID: 46346 RVA: 0x0027EC44 File Offset: 0x0027CE44
		// (set) Token: 0x0600B50B RID: 46347 RVA: 0x0027EC92 File Offset: 0x0027CE92
		[DefaultValue(typeof(PagerDropDownControlType), "RadComboBox")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the type of the page size drop down control")]
		public PagerDropDownControlType PageSizeControlType
		{
			get
			{
				PagerDropDownControlType result = PagerDropDownControlType.RadComboBox;
				object obj = base.ViewState["PageSizeControlType"];
				if (obj == null)
				{
					if (this._ownerTableView != null && this.Owner != null)
					{
						result = this.Owner.PagerStyle.PageSizeControlType;
					}
				}
				else
				{
					result = (PagerDropDownControlType)obj;
				}
				return result;
			}
			set
			{
				base.ViewState["PageSizeControlType"] = value;
			}
		}

		// Token: 0x04002FA8 RID: 12200
		private const string defTextFormat = "Change page: {4} &nbsp;Page <strong>{0}</strong> of <strong>{1}</strong>, items <strong>{2}</strong> to <strong>{3}</strong> of <strong>{5}</strong>.";

		// Token: 0x04002FA9 RID: 12201
		private GridTableView _ownerTableView;

		// Token: 0x04002FAA RID: 12202
		private RadGrid owner;
	}
}
