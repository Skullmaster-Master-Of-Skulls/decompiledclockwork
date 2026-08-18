using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000051 RID: 81
	[RequiredScript(typeof(AnimationExtender))]
	[RequiredScript(typeof(PopupExtender))]
	[Designer(typeof(AutoCompleteExtenderDesigner))]
	[TargetControlType(typeof(TextBox))]
	[ToolboxBitmap(typeof(Accessor), "AutoComplete.bmp")]
	[RequiredScript(typeof(TimerScript))]
	[ClientScriptResource("Sys.Extended.UI.AutoCompleteBehavior", "AutoComplete")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class AutoCompleteExtender : AnimationExtenderControlBase
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000096D5 File Offset: 0x000078D5
		// (set) Token: 0x060002AB RID: 683 RVA: 0x000096E3 File Offset: 0x000078E3
		[ClientPropertyName("minimumPrefixLength")]
		[ExtenderControlProperty]
		[DefaultValue(3)]
		public virtual int MinimumPrefixLength
		{
			get
			{
				return base.GetPropertyValue<int>("MinimumPrefixLength", 3);
			}
			set
			{
				base.SetPropertyValue<int>("MinimumPrefixLength", value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000096F1 File Offset: 0x000078F1
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00009703 File Offset: 0x00007903
		[ClientPropertyName("completionInterval")]
		[DefaultValue(1000)]
		[ExtenderControlProperty]
		public virtual int CompletionInterval
		{
			get
			{
				return base.GetPropertyValue<int>("CompletionInterval", 1000);
			}
			set
			{
				base.SetPropertyValue<int>("CompletionInterval", value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00009711 File Offset: 0x00007911
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00009720 File Offset: 0x00007920
		[ClientPropertyName("completionSetCount")]
		[DefaultValue(10)]
		[ExtenderControlProperty]
		public virtual int CompletionSetCount
		{
			get
			{
				return base.GetPropertyValue<int>("CompletionSetCount", 10);
			}
			set
			{
				base.SetPropertyValue<int>("CompletionSetCount", value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000972E File Offset: 0x0000792E
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00009740 File Offset: 0x00007940
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("completionListElementID")]
		[IDReferenceProperty(typeof(WebControl))]
		[Obsolete("Instead of passing in CompletionListElementID, use the default flyout and style that using the CssClass properties.")]
		public virtual string CompletionListElementID
		{
			get
			{
				return base.GetPropertyValue<string>("CompletionListElementID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CompletionListElementID", value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000974E File Offset: 0x0000794E
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00009760 File Offset: 0x00007960
		[DefaultValue("")]
		[ExtenderControlProperty]
		[RequiredProperty]
		[ClientPropertyName("serviceMethod")]
		public virtual string ServiceMethod
		{
			get
			{
				return base.GetPropertyValue<string>("ServiceMethod", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ServiceMethod", value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000976E File Offset: 0x0000796E
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00009780 File Offset: 0x00007980
		[UrlProperty]
		[TypeConverter(typeof(ServicePathConverter))]
		[ClientPropertyName("servicePath")]
		[ExtenderControlProperty]
		public virtual string ServicePath
		{
			get
			{
				return base.GetPropertyValue<string>("ServicePath", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ServicePath", value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000978E File Offset: 0x0000798E
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000979C File Offset: 0x0000799C
		[ClientPropertyName("contextKey")]
		[DefaultValue(null)]
		[ExtenderControlProperty]
		public string ContextKey
		{
			get
			{
				return base.GetPropertyValue<string>("ContextKey", null);
			}
			set
			{
				base.SetPropertyValue<string>("ContextKey", value);
				this.UseContextKey = true;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000097B1 File Offset: 0x000079B1
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000097BF File Offset: 0x000079BF
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("useContextKey")]
		public bool UseContextKey
		{
			get
			{
				return base.GetPropertyValue<bool>("UseContextKey", false);
			}
			set
			{
				base.SetPropertyValue<bool>("UseContextKey", value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000097CD File Offset: 0x000079CD
		// (set) Token: 0x060002BB RID: 699 RVA: 0x000097DF File Offset: 0x000079DF
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("completionListCssClass")]
		public string CompletionListCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("CompletionListCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CompletionListCssClass", value);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000097ED File Offset: 0x000079ED
		// (set) Token: 0x060002BD RID: 701 RVA: 0x000097FF File Offset: 0x000079FF
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("completionListItemCssClass")]
		public string CompletionListItemCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("CompletionListItemCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CompletionListItemCssClass", value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000980D File Offset: 0x00007A0D
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000981F File Offset: 0x00007A1F
		[DefaultValue("")]
		[ClientPropertyName("highlightedItemCssClass")]
		[ExtenderControlProperty]
		public string CompletionListHighlightedItemCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("CompletionListHighlightedItemCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CompletionListHighlightedItemCssClass", value);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000982D File Offset: 0x00007A2D
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000983B File Offset: 0x00007A3B
		[ClientPropertyName("enableCaching")]
		[DefaultValue(true)]
		[ExtenderControlProperty]
		public virtual bool EnableCaching
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableCaching", true);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableCaching", value);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00009849 File Offset: 0x00007A49
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000985B File Offset: 0x00007A5B
		[ExtenderControlProperty]
		[ClientPropertyName("delimiterCharacters")]
		public virtual string DelimiterCharacters
		{
			get
			{
				return base.GetPropertyValue<string>("DelimiterCharacters", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("DelimiterCharacters", value);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00009869 File Offset: 0x00007A69
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00009877 File Offset: 0x00007A77
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("firstRowSelected")]
		public virtual bool FirstRowSelected
		{
			get
			{
				return base.GetPropertyValue<bool>("FirstRowSelected", false);
			}
			set
			{
				base.SetPropertyValue<bool>("FirstRowSelected", value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00009885 File Offset: 0x00007A85
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00009893 File Offset: 0x00007A93
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("showOnlyCurrentWordInCompletionListItem")]
		public bool ShowOnlyCurrentWordInCompletionListItem
		{
			get
			{
				return base.GetPropertyValue<bool>("ShowOnlyCurrentWordInCompletionListItem", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ShowOnlyCurrentWordInCompletionListItem", value);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000098A1 File Offset: 0x00007AA1
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x000098B4 File Offset: 0x00007AB4
		[ExtenderControlProperty]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("onShow")]
		public Animation OnShow
		{
			get
			{
				return base.GetAnimation(ref this._onShow, "OnShow");
			}
			set
			{
				base.SetAnimation(ref this._onShow, "OnShow", value);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000098C8 File Offset: 0x00007AC8
		// (set) Token: 0x060002CB RID: 715 RVA: 0x000098DB File Offset: 0x00007ADB
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ExtenderControlProperty]
		[ClientPropertyName("onHide")]
		public Animation OnHide
		{
			get
			{
				return base.GetAnimation(ref this._onHide, "OnHide");
			}
			set
			{
				base.SetAnimation(ref this._onHide, "OnHide", value);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000098EF File Offset: 0x00007AEF
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00009901 File Offset: 0x00007B01
		[DefaultValue("")]
		[ExtenderControlEvent]
		[ClientPropertyName("populating")]
		public string OnClientPopulating
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientPopulating", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientPopulating", value);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000990F File Offset: 0x00007B0F
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00009921 File Offset: 0x00007B21
		[ClientPropertyName("populated")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public string OnClientPopulated
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientPopulated", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientPopulated", value);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000992F File Offset: 0x00007B2F
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00009941 File Offset: 0x00007B41
		[ExtenderControlEvent]
		[ClientPropertyName("showing")]
		[DefaultValue("")]
		public string OnClientShowing
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientShowing", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientShowing", value);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000994F File Offset: 0x00007B4F
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00009961 File Offset: 0x00007B61
		[ClientPropertyName("shown")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		public string OnClientShown
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientShown", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientShown", value);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000996F File Offset: 0x00007B6F
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00009981 File Offset: 0x00007B81
		[ClientPropertyName("hiding")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		public string OnClientHiding
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientHiding", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientHiding", value);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000998F File Offset: 0x00007B8F
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000099A1 File Offset: 0x00007BA1
		[DefaultValue("")]
		[ExtenderControlEvent]
		[ClientPropertyName("hidden")]
		public string OnClientHidden
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientHidden", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientHidden", value);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000099AF File Offset: 0x00007BAF
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x000099C1 File Offset: 0x00007BC1
		[ExtenderControlEvent]
		[ClientPropertyName("itemSelected")]
		[DefaultValue("")]
		public string OnClientItemSelected
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientItemSelected", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientItemSelected", value);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000099CF File Offset: 0x00007BCF
		// (set) Token: 0x060002DB RID: 731 RVA: 0x000099E1 File Offset: 0x00007BE1
		[DefaultValue("")]
		[ExtenderControlEvent]
		[ClientPropertyName("itemOver")]
		public string OnClientItemOver
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientItemOver", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientItemOver", value);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000099EF File Offset: 0x00007BEF
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00009A01 File Offset: 0x00007C01
		[DefaultValue("")]
		[ClientPropertyName("itemOut")]
		[ExtenderControlEvent]
		public string OnClientItemOut
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientItemOut", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientItemOut", value);
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00009A0F File Offset: 0x00007C0F
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.ResolveControlIDs(this._onShow);
			base.ResolveControlIDs(this._onHide);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009A30 File Offset: 0x00007C30
		public static string CreateAutoCompleteItem(string text, string value)
		{
			return new JavaScriptSerializer().Serialize(new Pair(text, value));
		}

		// Token: 0x040000F1 RID: 241
		private Animation _onShow;

		// Token: 0x040000F2 RID: 242
		private Animation _onHide;
	}
}
