using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using Telerik.Web.UI.HtmlChart;

namespace Telerik.Web.UI
{
	// Token: 0x020004D2 RID: 1234
	public class PlotBand : StateManager
	{
		// Token: 0x06002CE0 RID: 11488 RVA: 0x00093926 File Offset: 0x00091B26
		public PlotBand()
		{
			this.InitSerializer();
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x00093934 File Offset: 0x00091B34
		public PlotBand(decimal from, decimal to, Color color, byte alpha) : this()
		{
			this.From = new decimal?(from);
			this.To = new decimal?(to);
			this.Color = color;
			this.Alpha = alpha;
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x00093963 File Offset: 0x00091B63
		public PlotBand(DateTime from, DateTime to, Color color, byte alpha) : this()
		{
			this.FromDate = new DateTime?(from);
			this.ToDate = new DateTime?(to);
			this.Color = color;
			this.Alpha = alpha;
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x00093992 File Offset: 0x00091B92
		// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x000939AE File Offset: 0x00091BAE
		public decimal? From
		{
			get
			{
				return (decimal?)(base.ViewState["From"] ?? null);
			}
			set
			{
				base.ViewState["From"] = value;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x000939C6 File Offset: 0x00091BC6
		// (set) Token: 0x06002CE6 RID: 11494 RVA: 0x000939E2 File Offset: 0x00091BE2
		public decimal? To
		{
			get
			{
				return (decimal?)(base.ViewState["To"] ?? null);
			}
			set
			{
				base.ViewState["To"] = value;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000939FA File Offset: 0x00091BFA
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x00093A16 File Offset: 0x00091C16
		public DateTime? FromDate
		{
			get
			{
				return (DateTime?)(base.ViewState["FromDate"] ?? null);
			}
			set
			{
				base.ViewState["FromDate"] = value;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x00093A2E File Offset: 0x00091C2E
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x00093A4A File Offset: 0x00091C4A
		public DateTime? ToDate
		{
			get
			{
				return (DateTime?)(base.ViewState["ToDate"] ?? null);
			}
			set
			{
				base.ViewState["ToDate"] = value;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x00093A62 File Offset: 0x00091C62
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x00093A87 File Offset: 0x00091C87
		public Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x00093A9F File Offset: 0x00091C9F
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x00093AC0 File Offset: 0x00091CC0
		[DefaultValue(0)]
		public byte Alpha
		{
			get
			{
				return Convert.ToByte(base.ViewState["Alpha"] ?? 0);
			}
			set
			{
				base.ViewState["Alpha"] = value;
			}
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x00093AD8 File Offset: 0x00091CD8
		internal string Serialize()
		{
			return this._serializer.Serialize(this);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x00093AE8 File Offset: 0x00091CE8
		private void InitSerializer()
		{
			this._serializer = new AdvancedJavaScriptSerializer();
			this._serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new HtmlChartTypeConverters()
			});
		}

		// Token: 0x04000B8C RID: 2956
		private AdvancedJavaScriptSerializer _serializer;
	}
}
