using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000F3 RID: 243
	internal class CalendarStrings : LocalizationStrings
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x00025325 File Offset: 0x00023525
		public CalendarStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00025335 File Offset: 0x00023535
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002534E File Offset: 0x0002354E
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0002535B File Offset: 0x0002355B
		[DefaultValue("&lt;")]
		[NotifyParentProperty(true)]
		public string NavigationPrevText
		{
			get
			{
				return this.GetString("NavigationPrevText");
			}
			set
			{
				this.SetString("NavigationPrevText", value);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00025369 File Offset: 0x00023569
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x00025376 File Offset: 0x00023576
		[NotifyParentProperty(true)]
		[DefaultValue("&gt;")]
		public string NavigationNextText
		{
			get
			{
				return this.GetString("NavigationNextText");
			}
			set
			{
				this.SetString("NavigationNextText", value);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00025384 File Offset: 0x00023584
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00025391 File Offset: 0x00023591
		[NotifyParentProperty(true)]
		[DefaultValue("&lt;&lt;")]
		public string FastNavigationPrevText
		{
			get
			{
				return this.GetString("FastNavigationPrevText");
			}
			set
			{
				this.SetString("FastNavigationPrevText", value);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002539F File Offset: 0x0002359F
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x000253AC File Offset: 0x000235AC
		[DefaultValue("&lt;&lt;")]
		[NotifyParentProperty(true)]
		public string FastNavigationNextText
		{
			get
			{
				return this.GetString("FastNavigationNextText");
			}
			set
			{
				this.SetString("FastNavigationNextText", value);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000253BA File Offset: 0x000235BA
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x000253C7 File Offset: 0x000235C7
		[DefaultValue("<")]
		[NotifyParentProperty(true)]
		public string NavigationPrevToolTip
		{
			get
			{
				return this.GetString("NavigationPrevToolTip");
			}
			set
			{
				this.SetString("NavigationPrevToolTip", value);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000253D5 File Offset: 0x000235D5
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x000253E2 File Offset: 0x000235E2
		[NotifyParentProperty(true)]
		[DefaultValue(">")]
		public string NavigationNextToolTip
		{
			get
			{
				return this.GetString("NavigationNextToolTip");
			}
			set
			{
				this.SetString("NavigationNextToolTip", value);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000253F0 File Offset: 0x000235F0
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x000253FD File Offset: 0x000235FD
		[DefaultValue("<<")]
		[NotifyParentProperty(true)]
		public string FastNavigationPrevToolTip
		{
			get
			{
				return this.GetString("FastNavigationPrevToolTip");
			}
			set
			{
				this.SetString("FastNavigationPrevToolTip", value);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0002540B File Offset: 0x0002360B
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00025418 File Offset: 0x00023618
		[DefaultValue(">>")]
		[NotifyParentProperty(true)]
		public string FastNavigationNextToolTip
		{
			get
			{
				return this.GetString("FastNavigationNextToolTip");
			}
			set
			{
				this.SetString("FastNavigationNextToolTip", value);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00025426 File Offset: 0x00023626
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00025433 File Offset: 0x00023633
		[NotifyParentProperty(true)]
		[DefaultValue("Today")]
		public string FastNavigationTodayButtonCaption
		{
			get
			{
				return this.GetString("FastNavigationTodayButtonCaption");
			}
			set
			{
				this.SetString("FastNavigationTodayButtonCaption", value);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00025441 File Offset: 0x00023641
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0002544E File Offset: 0x0002364E
		[NotifyParentProperty(true)]
		[DefaultValue("OK")]
		public string FastNavigationOkButtonCaption
		{
			get
			{
				return this.GetString("FastNavigationOkButtonCaption");
			}
			set
			{
				this.SetString("FastNavigationOkButtonCaption", value);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002545C File Offset: 0x0002365C
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00025469 File Offset: 0x00023669
		[DefaultValue("Cancel")]
		[NotifyParentProperty(true)]
		public string FastNavigationCancelButtonCaption
		{
			get
			{
				return this.GetString("FastNavigationCancelButtonCaption");
			}
			set
			{
				this.SetString("FastNavigationCancelButtonCaption", value);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00025477 File Offset: 0x00023677
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00025484 File Offset: 0x00023684
		[NotifyParentProperty(true)]
		[DefaultValue("Date is out of range.")]
		public string FastNavigationDateIsOutOfRangeMessage
		{
			get
			{
				return this.GetString("FastNavigationDateIsOutOfRangeMessage");
			}
			set
			{
				this.SetString("FastNavigationDateIsOutOfRangeMessage", value);
			}
		}

		// Token: 0x04000283 RID: 643
		private readonly LocalizationProvider _localizationProvider;
	}
}
