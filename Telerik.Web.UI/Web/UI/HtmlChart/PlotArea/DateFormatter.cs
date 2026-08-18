using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020004F2 RID: 1266
	public class DateFormatter : StateManager
	{
		// Token: 0x06002D23 RID: 11555 RVA: 0x0009450D File Offset: 0x0009270D
		public DateFormatter()
		{
			this.InitSerializer();
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x0009451C File Offset: 0x0009271C
		// (set) Token: 0x06002D25 RID: 11557 RVA: 0x00094572 File Offset: 0x00092772
		[DefaultValue("HH:mm:ss")]
		public string SecondsFormat
		{
			get
			{
				if (base.ViewState["Seconds"] == null || string.IsNullOrEmpty(base.ViewState["Seconds"].ToString()))
				{
					return "HH:mm:ss";
				}
				return base.ViewState["Seconds"].ToString();
			}
			set
			{
				base.ViewState["Seconds"] = value;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x00094588 File Offset: 0x00092788
		// (set) Token: 0x06002D27 RID: 11559 RVA: 0x000945DE File Offset: 0x000927DE
		[DefaultValue("HH:mm")]
		public string MinutesFormat
		{
			get
			{
				if (base.ViewState["Minutes"] == null || string.IsNullOrEmpty(base.ViewState["Minutes"].ToString()))
				{
					return "HH:mm";
				}
				return base.ViewState["Minutes"].ToString();
			}
			set
			{
				base.ViewState["Minutes"] = value;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000945F4 File Offset: 0x000927F4
		// (set) Token: 0x06002D29 RID: 11561 RVA: 0x0009464A File Offset: 0x0009284A
		[DefaultValue("HH:mm")]
		public string HoursFormat
		{
			get
			{
				if (base.ViewState["Hours"] == null || string.IsNullOrEmpty(base.ViewState["Hours"].ToString()))
				{
					return "HH:mm";
				}
				return base.ViewState["Hours"].ToString();
			}
			set
			{
				base.ViewState["Hours"] = value;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x00094660 File Offset: 0x00092860
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x000946B6 File Offset: 0x000928B6
		[DefaultValue("M/d")]
		public string DaysFormat
		{
			get
			{
				if (base.ViewState["Days"] == null || string.IsNullOrEmpty(base.ViewState["Days"].ToString()))
				{
					return "M/d";
				}
				return base.ViewState["Days"].ToString();
			}
			set
			{
				base.ViewState["Days"] = value;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000946CC File Offset: 0x000928CC
		// (set) Token: 0x06002D2D RID: 11565 RVA: 0x00094722 File Offset: 0x00092922
		[DefaultValue("M/d")]
		public string WeeksFormat
		{
			get
			{
				if (base.ViewState["Weeks"] == null || string.IsNullOrEmpty(base.ViewState["Weeks"].ToString()))
				{
					return "M/d";
				}
				return base.ViewState["Weeks"].ToString();
			}
			set
			{
				base.ViewState["Weeks"] = value;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x00094738 File Offset: 0x00092938
		// (set) Token: 0x06002D2F RID: 11567 RVA: 0x0009478E File Offset: 0x0009298E
		[DefaultValue("MMM 'yy")]
		public string MonthsFormat
		{
			get
			{
				if (base.ViewState["Months"] == null || string.IsNullOrEmpty(base.ViewState["Months"].ToString()))
				{
					return "MMM 'yy";
				}
				return base.ViewState["Months"].ToString();
			}
			set
			{
				base.ViewState["Months"] = value;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000947A4 File Offset: 0x000929A4
		// (set) Token: 0x06002D31 RID: 11569 RVA: 0x000947FA File Offset: 0x000929FA
		[DefaultValue("yyyy")]
		public string YearsFormat
		{
			get
			{
				if (base.ViewState["Years"] == null || string.IsNullOrEmpty(base.ViewState["Years"].ToString()))
				{
					return "yyyy";
				}
				return base.ViewState["Years"].ToString();
			}
			set
			{
				base.ViewState["Years"] = value;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x00094810 File Offset: 0x00092A10
		internal bool IsDefault
		{
			get
			{
				return this.SecondsFormat == "HH:mm:ss" && this.MinutesFormat == "HH:mm" && this.HoursFormat == "HH:mm" && this.DaysFormat == "M/d" && this.WeeksFormat == "M/d" && this.MonthsFormat == "MMM 'yy" && this.YearsFormat == "yyyy";
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0009489B File Offset: 0x00092A9B
		internal string Serialize()
		{
			return string.Format("{0}", this._serializer.Serialize(this));
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000948B4 File Offset: 0x00092AB4
		private void InitSerializer()
		{
			this._serializer = new JavaScriptSerializer();
			this._serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new XAxisDateFormatterConverter()
			});
		}

		// Token: 0x04000C30 RID: 3120
		private JavaScriptSerializer _serializer;
	}
}
