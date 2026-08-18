using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000F6 RID: 246
	internal class MonthYearPickerStrings : LocalizationStrings
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x00025535 File Offset: 0x00023735
		public MonthYearPickerStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00025545 File Offset: 0x00023745
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0002555E File Offset: 0x0002375E
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x0002556B File Offset: 0x0002376B
		[DefaultValue("Open the monthyear view popup.")]
		[NotifyParentProperty(true)]
		public string PopupButtonToolTip
		{
			get
			{
				return this.GetString("PopupButtonToolTip");
			}
			set
			{
				this.SetString("PopupButtonToolTip", value);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00025579 File Offset: 0x00023779
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00025586 File Offset: 0x00023786
		[DefaultValue("Table holding time picker for selecting time of day.")]
		[NotifyParentProperty(true)]
		public string MonthYearViewSummary
		{
			get
			{
				return this.GetString("MonthYearViewSummary");
			}
			set
			{
				this.SetString("MonthYearViewSummary", value);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00025594 File Offset: 0x00023794
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x000255A1 File Offset: 0x000237A1
		[DefaultValue("Month year picker")]
		[NotifyParentProperty(true)]
		public string MonthYearViewCaptionText
		{
			get
			{
				return this.GetString("MonthYearViewCaptionText");
			}
			set
			{
				this.SetString("MonthYearViewCaptionText", value);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x000255AF File Offset: 0x000237AF
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x000255BC File Offset: 0x000237BC
		[NotifyParentProperty(true)]
		[DefaultValue("&gt;")]
		public string MonthYearNavigationNextText
		{
			get
			{
				return this.GetString("MonthYearNavigationNextText");
			}
			set
			{
				this.SetString("MonthYearNavigationNextText", value);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x000255CA File Offset: 0x000237CA
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x000255D7 File Offset: 0x000237D7
		[NotifyParentProperty(true)]
		[DefaultValue(">")]
		public string MonthYearNavigationNextToolTip
		{
			get
			{
				return this.GetString("MonthYearNavigationNextToolTip");
			}
			set
			{
				this.SetString("MonthYearNavigationNextToolTip", value);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x000255E5 File Offset: 0x000237E5
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x000255F2 File Offset: 0x000237F2
		[NotifyParentProperty(true)]
		[DefaultValue("&lt;")]
		public string MonthYearNavigationPrevText
		{
			get
			{
				return this.GetString("MonthYearNavigationPrevText");
			}
			set
			{
				this.SetString("MonthYearNavigationPrevText", value);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00025600 File Offset: 0x00023800
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0002560D File Offset: 0x0002380D
		[DefaultValue("<")]
		[NotifyParentProperty(true)]
		public string MonthYearNavigationPrevToolTip
		{
			get
			{
				return this.GetString("MonthYearNavigationPrevToolTip");
			}
			set
			{
				this.SetString("MonthYearNavigationPrevToolTip", value);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002561B File Offset: 0x0002381B
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00025628 File Offset: 0x00023828
		[DefaultValue("Today")]
		[NotifyParentProperty(true)]
		public string MonthYearNavigationTodayButtonCaption
		{
			get
			{
				return this.GetString("MonthYearNavigationTodayButtonCaption");
			}
			set
			{
				this.SetString("MonthYearNavigationTodayButtonCaption", value);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00025636 File Offset: 0x00023836
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00025643 File Offset: 0x00023843
		[DefaultValue("OK")]
		[NotifyParentProperty(true)]
		public string MonthYearNavigationOkButtonCaption
		{
			get
			{
				return this.GetString("MonthYearNavigationOkButtonCaption");
			}
			set
			{
				this.SetString("MonthYearNavigationOkButtonCaption", value);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00025651 File Offset: 0x00023851
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0002565E File Offset: 0x0002385E
		[DefaultValue("Cancel")]
		[NotifyParentProperty(true)]
		public string MonthYearNavigationCancelButtonCaption
		{
			get
			{
				return this.GetString("MonthYearNavigationCancelButtonCaption");
			}
			set
			{
				this.SetString("MonthYearNavigationCancelButtonCaption", value);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002566C File Offset: 0x0002386C
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00025679 File Offset: 0x00023879
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		public string MonthYearNavigationDateIsOutOfRangeMessage
		{
			get
			{
				return this.GetString("MonthYearNavigationCancelButtonCaption");
			}
			set
			{
				this.SetString("MonthYearNavigationCancelButtonCaption", value);
			}
		}

		// Token: 0x04000286 RID: 646
		private readonly LocalizationProvider _localizationProvider;
	}
}
