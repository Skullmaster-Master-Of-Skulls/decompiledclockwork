using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020005BF RID: 1471
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class MediaPlayerBanner : StateManager, INamingContainer
	{
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000ADB48 File Offset: 0x000ABD48
		// (set) Token: 0x06003477 RID: 13431 RVA: 0x000ADB75 File Offset: 0x000ABD75
		[UrlProperty]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				object obj = base.ViewState["ImageUrl"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x000ADB88 File Offset: 0x000ABD88
		// (set) Token: 0x06003479 RID: 13433 RVA: 0x000ADBB5 File Offset: 0x000ABDB5
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		public string NavigateURL
		{
			get
			{
				object obj = base.ViewState["NavigateUrl"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000ADBC8 File Offset: 0x000ABDC8
		// (set) Token: 0x0600347B RID: 13435 RVA: 0x000ADC08 File Offset: 0x000ABE08
		[DefaultValue(0.0)]
		public double StartTime
		{
			get
			{
				double result = 0.0;
				if (base.ViewState["StartTime"] != null)
				{
					result = Convert.ToDouble(base.ViewState["StartTime"]);
				}
				return result;
			}
			set
			{
				base.ViewState["StartTime"] = value;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x000ADC20 File Offset: 0x000ABE20
		// (set) Token: 0x0600347D RID: 13437 RVA: 0x000ADC60 File Offset: 0x000ABE60
		[DefaultValue(0.0)]
		public double EndTime
		{
			get
			{
				double result = 0.0;
				if (base.ViewState["EndTime"] != null)
				{
					result = Convert.ToDouble(base.ViewState["EndTime"]);
				}
				return result;
			}
			set
			{
				base.ViewState["EndTime"] = value;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000ADC78 File Offset: 0x000ABE78
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000ADCA3 File Offset: 0x000ABEA3
		[DefaultValue(false)]
		public bool ShowCloseButton
		{
			get
			{
				return base.ViewState["ShowCloseButton"] != null && (bool)base.ViewState["ShowCloseButton"];
			}
			set
			{
				base.ViewState["ShowCloseButton"] = value;
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000ADCBC File Offset: 0x000ABEBC
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000ADCE9 File Offset: 0x000ABEE9
		[DefaultValue("")]
		[Description("The alternate text displayed in the banner when the image is unavailable")]
		public virtual string AlternateText
		{
			get
			{
				string text = (string)base.ViewState["AlternateText"];
				return text ?? string.Empty;
			}
			set
			{
				base.ViewState["AlternateText"] = value;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000ADCFC File Offset: 0x000ABEFC
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x000ADD29 File Offset: 0x000ABF29
		[DefaultValue("")]
		[Description("The ToolTip of the image shown as banner")]
		public virtual string ToolTip
		{
			get
			{
				string text = (string)base.ViewState["ToolTip"];
				return text ?? string.Empty;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000ADD3C File Offset: 0x000ABF3C
		// (set) Token: 0x06003485 RID: 13445 RVA: 0x000ADD5C File Offset: 0x000ABF5C
		[TypeConverter(typeof(TargetConverter))]
		[DefaultValue("")]
		[Description("Gets or sets the target window or frame in which to display the Web page content linked to when the Banner is clicked.")]
		public string Target
		{
			get
			{
				return ((string)base.ViewState["Target"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Target"] = value;
			}
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000ADD70 File Offset: 0x000ABF70
		internal IDictionary Describe(Page page)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string text = this.ImageUrl;
			if (VirtualPathUtility.IsAppRelative(text))
			{
				text = page.ResolveClientUrl(text);
			}
			dictionary.Add("imageUrl", text);
			dictionary.Add("navigateUrl", this.NavigateURL);
			dictionary.Add("startTime", this.StartTime);
			dictionary.Add("endTime", this.EndTime);
			dictionary.Add("showCloseButton", this.ShowCloseButton);
			dictionary.Add("alternateText", this.AlternateText);
			dictionary.Add("toolTip", this.ToolTip);
			dictionary.Add("target", this.Target);
			return dictionary;
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000ADE2D File Offset: 0x000AC02D
		protected override void TrackViewState()
		{
			if (this.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000ADE44 File Offset: 0x000AC044
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
			}
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000ADE6C File Offset: 0x000AC06C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState()
			}.ToArray(typeof(object));
		}
	}
}
