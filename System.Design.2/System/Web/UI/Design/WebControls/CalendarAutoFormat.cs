using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A3 RID: 163
	internal sealed class CalendarAutoFormat : BaseAutoFormat<System.Web.UI.WebControls.Calendar>
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x00016DE3 File Offset: 0x00014FE3
		public CalendarAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 430;
			base.Style.Height = 280;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00016E18 File Offset: 0x00015018
		protected override void Apply(System.Web.UI.WebControls.Calendar calendar)
		{
			calendar.Width = this.Width;
			calendar.Height = this.Height;
			calendar.Font.Name = this.FontName;
			calendar.Font.Size = this.FontSize;
			calendar.ForeColor = this.ForeColor;
			calendar.BackColor = this.BackColor;
			calendar.BorderColor = this.BorderColor;
			calendar.BorderWidth = this.BorderWidth;
			calendar.BorderStyle = this.BorderStyle;
			calendar.ShowGridLines = this.ShowGridLines;
			calendar.CellPadding = this.CellPadding;
			calendar.CellSpacing = this.CellSpacing;
			calendar.DayNameFormat = this.DayNameFormat;
			calendar.TitleFormat = this.TitleFormat;
			calendar.NextPrevFormat = this.NextPrevFormat;
			calendar.Font.ClearDefaults();
			calendar.NextPrevStyle.BackColor = this.NextPrevBackColor;
			calendar.NextPrevStyle.Font.Bold = ((this.NextPrevFont & 1) != 0);
			calendar.NextPrevStyle.Font.Italic = ((this.NextPrevFont & 2) != 0);
			calendar.NextPrevStyle.Font.Underline = ((this.NextPrevFont & 4) != 0);
			calendar.NextPrevStyle.Font.Size = this.NextPrevFontSize;
			calendar.NextPrevStyle.ForeColor = this.NextPrevForeColor;
			calendar.NextPrevStyle.VerticalAlign = this.NextPrevVerticalAlign;
			calendar.NextPrevStyle.Font.ClearDefaults();
			calendar.TitleStyle.BackColor = this.TitleBackColor;
			calendar.TitleStyle.BorderColor = this.TitleBorderColor;
			calendar.TitleStyle.BorderStyle = this.TitleBorderStyle;
			calendar.TitleStyle.BorderWidth = this.TitleBorderWidth;
			calendar.TitleStyle.Font.Bold = ((this.TitleFont & 1) != 0);
			calendar.TitleStyle.Font.Italic = ((this.TitleFont & 2) != 0);
			calendar.TitleStyle.Font.Underline = ((this.TitleFont & 4) != 0);
			calendar.TitleStyle.Font.Size = this.TitleFontSize;
			calendar.TitleStyle.ForeColor = this.TitleForeColor;
			calendar.TitleStyle.Height = this.TitleHeight;
			calendar.TitleStyle.Font.ClearDefaults();
			calendar.DayStyle.BackColor = this.DayBackColor;
			calendar.DayStyle.Font.Bold = ((this.DayFont & 1) != 0);
			calendar.DayStyle.Font.Italic = ((this.DayFont & 2) != 0);
			calendar.DayStyle.Font.Underline = ((this.DayFont & 4) != 0);
			calendar.DayStyle.Font.Size = this.DayFontSize;
			calendar.DayStyle.ForeColor = this.DayForeColor;
			calendar.DayStyle.Width = this.DayWidth;
			calendar.DayStyle.Font.ClearDefaults();
			calendar.DayHeaderStyle.BackColor = this.DayHeaderBackColor;
			calendar.DayHeaderStyle.Font.Bold = ((this.DayHeaderFont & 1) != 0);
			calendar.DayHeaderStyle.Font.Italic = ((this.DayHeaderFont & 2) != 0);
			calendar.DayHeaderStyle.Font.Underline = ((this.DayHeaderFont & 4) != 0);
			calendar.DayHeaderStyle.Font.Size = this.DayHeaderFontSize;
			calendar.DayHeaderStyle.ForeColor = this.DayHeaderForeColor;
			calendar.DayHeaderStyle.Height = this.DayHeaderHeight;
			calendar.DayHeaderStyle.Font.ClearDefaults();
			calendar.TodayDayStyle.BackColor = this.TodayDayBackColor;
			calendar.TodayDayStyle.Font.Bold = ((this.TodayDayFont & 1) != 0);
			calendar.TodayDayStyle.Font.Italic = ((this.TodayDayFont & 2) != 0);
			calendar.TodayDayStyle.Font.Underline = ((this.TodayDayFont & 4) != 0);
			calendar.TodayDayStyle.Font.Size = this.TodayDayFontSize;
			calendar.TodayDayStyle.ForeColor = this.TodayDayForeColor;
			calendar.TodayDayStyle.Font.ClearDefaults();
			calendar.SelectedDayStyle.BackColor = this.SelectedDayBackColor;
			calendar.SelectedDayStyle.Font.Bold = ((this.SelectedDayFont & 1) != 0);
			calendar.SelectedDayStyle.Font.Italic = ((this.SelectedDayFont & 2) != 0);
			calendar.SelectedDayStyle.Font.Underline = ((this.SelectedDayFont & 4) != 0);
			calendar.SelectedDayStyle.Font.Size = this.SelectedDayFontSize;
			calendar.SelectedDayStyle.ForeColor = this.SelectedDayForeColor;
			calendar.SelectedDayStyle.Font.ClearDefaults();
			calendar.OtherMonthDayStyle.BackColor = this.OtherMonthDayBackColor;
			calendar.OtherMonthDayStyle.Font.Bold = ((this.OtherMonthDayFont & 1) != 0);
			calendar.OtherMonthDayStyle.Font.Italic = ((this.OtherMonthDayFont & 2) != 0);
			calendar.OtherMonthDayStyle.Font.Underline = ((this.OtherMonthDayFont & 4) != 0);
			calendar.OtherMonthDayStyle.Font.Size = this.OtherMonthDayFontSize;
			calendar.OtherMonthDayStyle.ForeColor = this.OtherMonthDayForeColor;
			calendar.OtherMonthDayStyle.Font.ClearDefaults();
			calendar.WeekendDayStyle.BackColor = this.WeekendDayBackColor;
			calendar.WeekendDayStyle.Font.Bold = ((this.WeekendDayFont & 1) != 0);
			calendar.WeekendDayStyle.Font.Italic = ((this.WeekendDayFont & 2) != 0);
			calendar.WeekendDayStyle.Font.Underline = ((this.WeekendDayFont & 4) != 0);
			calendar.WeekendDayStyle.Font.Size = this.WeekendDayFontSize;
			calendar.WeekendDayStyle.ForeColor = this.WeekendDayForeColor;
			calendar.WeekendDayStyle.Font.ClearDefaults();
			calendar.SelectorStyle.BackColor = this.SelectorBackColor;
			calendar.SelectorStyle.Font.Bold = ((this.SelectorFont & 1) != 0);
			calendar.SelectorStyle.Font.Italic = ((this.SelectorFont & 2) != 0);
			calendar.SelectorStyle.Font.Underline = ((this.SelectorFont & 4) != 0);
			calendar.SelectorStyle.Font.Name = this.SelectorFontName;
			calendar.SelectorStyle.Font.Size = this.SelectorFontSize;
			calendar.SelectorStyle.ForeColor = this.SelectorForeColor;
			calendar.SelectorStyle.Width = this.SelectorWidth;
			calendar.SelectorStyle.Font.ClearDefaults();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000174F0 File Offset: 0x000156F0
		protected override void Initialize(DataRow schemeData)
		{
			if (schemeData == null)
			{
				return;
			}
			this.Width = new Unit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("Width", schemeData), CultureInfo.InvariantCulture);
			this.Height = new Unit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("Height", schemeData), CultureInfo.InvariantCulture);
			this.FontName = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("FontName", schemeData);
			this.FontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("FontSize", schemeData), CultureInfo.InvariantCulture);
			this.ForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("ForeColor", schemeData));
			this.BackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("BackColor", schemeData));
			this.BorderColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("BorderColor", schemeData));
			this.BorderWidth = new Unit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("BorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("BorderStyle", schemeData, "NotSet"));
			this.ShowGridLines = bool.Parse(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("ShowGridLines", schemeData, "false"));
			this.CellPadding = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("CellPadding", schemeData, 2);
			this.CellSpacing = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("CellSpacing", schemeData);
			this.DayNameFormat = (DayNameFormat)Enum.Parse(typeof(DayNameFormat), BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayNameFormat", schemeData, "Short"));
			this.NextPrevBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("NextPrevBackColor", schemeData));
			this.NextPrevFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("NextPrevFont", schemeData);
			this.NextPrevFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("NextPrevFontSize", schemeData), CultureInfo.InvariantCulture);
			this.NextPrevForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("NextPrevForeColor", schemeData));
			this.NextPrevFormat = (NextPrevFormat)Enum.Parse(typeof(NextPrevFormat), BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("NextPrevFormat", schemeData, "CustomText"));
			this.NextPrevVerticalAlign = (VerticalAlign)Enum.Parse(typeof(VerticalAlign), BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("NextPrevVerticalAlign", schemeData, "NotSet"));
			this.TitleFormat = (TitleFormat)Enum.Parse(typeof(TitleFormat), BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TitleFormat", schemeData, "MonthYear"));
			this.TitleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TitleBackColor", schemeData));
			this.TitleBorderColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TitleBorderColor", schemeData));
			this.TitleBorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("BorderStyle", schemeData, "NotSet"));
			this.TitleBorderWidth = new Unit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TitleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.TitleFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("TitleFont", schemeData);
			this.TitleFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TitleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.TitleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TitleForeColor", schemeData));
			this.TitleHeight = new Unit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TitleHeight", schemeData), CultureInfo.InvariantCulture);
			this.DayBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayBackColor", schemeData));
			this.DayFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("DayFont", schemeData);
			this.DayFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayFontSize", schemeData), CultureInfo.InvariantCulture);
			this.DayForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayForeColor", schemeData));
			this.DayWidth = new Unit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayWidth", schemeData), CultureInfo.InvariantCulture);
			this.DayHeaderBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayHeaderBackColor", schemeData));
			this.DayHeaderFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("DayHeaderFont", schemeData);
			this.DayHeaderFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayHeaderFontSize", schemeData), CultureInfo.InvariantCulture);
			this.DayHeaderForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayHeaderForeColor", schemeData));
			this.DayHeaderHeight = new Unit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("DayHeaderHeight", schemeData), CultureInfo.InvariantCulture);
			this.TodayDayBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TodayDayBackColor", schemeData));
			this.TodayDayFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("TodayDayFont", schemeData);
			this.TodayDayFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TodayDayFontSize", schemeData), CultureInfo.InvariantCulture);
			this.TodayDayForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("TodayDayForeColor", schemeData));
			this.SelectedDayBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("SelectedDayBackColor", schemeData));
			this.SelectedDayFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("SelectedDayFont", schemeData);
			this.SelectedDayFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("SelectedDayFontSize", schemeData), CultureInfo.InvariantCulture);
			this.SelectedDayForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("SelectedDayForeColor", schemeData));
			this.OtherMonthDayBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("OtherMonthDayBackColor", schemeData));
			this.OtherMonthDayFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("OtherMonthDayFont", schemeData);
			this.OtherMonthDayFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("OtherMonthDayFontSize", schemeData), CultureInfo.InvariantCulture);
			this.OtherMonthDayForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("OtherMonthDayForeColor", schemeData));
			this.WeekendDayBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("WeekendDayBackColor", schemeData));
			this.WeekendDayFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("WeekendDayFont", schemeData);
			this.WeekendDayFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("WeekendDayFontSize", schemeData), CultureInfo.InvariantCulture);
			this.WeekendDayForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("WeekendDayForeColor", schemeData));
			this.SelectorBackColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("SelectorBackColor", schemeData));
			this.SelectorFont = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetIntProperty("SelectorFont", schemeData);
			this.SelectorFontName = BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("SelectorFontName", schemeData);
			this.SelectorFontSize = new FontUnit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("SelectorFontSize", schemeData), CultureInfo.InvariantCulture);
			this.SelectorForeColor = ColorTranslator.FromHtml(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("SelectorForeColor", schemeData));
			this.SelectorWidth = new Unit(BaseAutoFormat<System.Web.UI.WebControls.Calendar>.GetStringProperty("SelectorWidth", schemeData), CultureInfo.InvariantCulture);
		}

		// Token: 0x0400021B RID: 539
		private Unit Width;

		// Token: 0x0400021C RID: 540
		private Unit Height;

		// Token: 0x0400021D RID: 541
		private string FontName;

		// Token: 0x0400021E RID: 542
		private FontUnit FontSize;

		// Token: 0x0400021F RID: 543
		private Color ForeColor;

		// Token: 0x04000220 RID: 544
		private Color BackColor;

		// Token: 0x04000221 RID: 545
		private Color BorderColor;

		// Token: 0x04000222 RID: 546
		private Unit BorderWidth;

		// Token: 0x04000223 RID: 547
		private BorderStyle BorderStyle;

		// Token: 0x04000224 RID: 548
		private bool ShowGridLines;

		// Token: 0x04000225 RID: 549
		private int CellPadding;

		// Token: 0x04000226 RID: 550
		private int CellSpacing;

		// Token: 0x04000227 RID: 551
		private DayNameFormat DayNameFormat;

		// Token: 0x04000228 RID: 552
		private Color NextPrevBackColor;

		// Token: 0x04000229 RID: 553
		private int NextPrevFont;

		// Token: 0x0400022A RID: 554
		private FontUnit NextPrevFontSize;

		// Token: 0x0400022B RID: 555
		private Color NextPrevForeColor;

		// Token: 0x0400022C RID: 556
		private NextPrevFormat NextPrevFormat;

		// Token: 0x0400022D RID: 557
		private VerticalAlign NextPrevVerticalAlign;

		// Token: 0x0400022E RID: 558
		private TitleFormat TitleFormat;

		// Token: 0x0400022F RID: 559
		private Color TitleBackColor;

		// Token: 0x04000230 RID: 560
		private Color TitleBorderColor;

		// Token: 0x04000231 RID: 561
		private BorderStyle TitleBorderStyle;

		// Token: 0x04000232 RID: 562
		private Unit TitleBorderWidth;

		// Token: 0x04000233 RID: 563
		private int TitleFont;

		// Token: 0x04000234 RID: 564
		private FontUnit TitleFontSize;

		// Token: 0x04000235 RID: 565
		private Color TitleForeColor;

		// Token: 0x04000236 RID: 566
		private Unit TitleHeight;

		// Token: 0x04000237 RID: 567
		private Color DayBackColor;

		// Token: 0x04000238 RID: 568
		private int DayFont;

		// Token: 0x04000239 RID: 569
		private FontUnit DayFontSize;

		// Token: 0x0400023A RID: 570
		private Color DayForeColor;

		// Token: 0x0400023B RID: 571
		private Unit DayWidth;

		// Token: 0x0400023C RID: 572
		private Color DayHeaderBackColor;

		// Token: 0x0400023D RID: 573
		private int DayHeaderFont;

		// Token: 0x0400023E RID: 574
		private FontUnit DayHeaderFontSize;

		// Token: 0x0400023F RID: 575
		private Color DayHeaderForeColor;

		// Token: 0x04000240 RID: 576
		private Unit DayHeaderHeight;

		// Token: 0x04000241 RID: 577
		private Color TodayDayBackColor;

		// Token: 0x04000242 RID: 578
		private int TodayDayFont;

		// Token: 0x04000243 RID: 579
		private FontUnit TodayDayFontSize;

		// Token: 0x04000244 RID: 580
		private Color TodayDayForeColor;

		// Token: 0x04000245 RID: 581
		private Color SelectedDayBackColor;

		// Token: 0x04000246 RID: 582
		private int SelectedDayFont;

		// Token: 0x04000247 RID: 583
		private FontUnit SelectedDayFontSize;

		// Token: 0x04000248 RID: 584
		private Color SelectedDayForeColor;

		// Token: 0x04000249 RID: 585
		private Color OtherMonthDayBackColor;

		// Token: 0x0400024A RID: 586
		private int OtherMonthDayFont;

		// Token: 0x0400024B RID: 587
		private FontUnit OtherMonthDayFontSize;

		// Token: 0x0400024C RID: 588
		private Color OtherMonthDayForeColor;

		// Token: 0x0400024D RID: 589
		private Color WeekendDayBackColor;

		// Token: 0x0400024E RID: 590
		private int WeekendDayFont;

		// Token: 0x0400024F RID: 591
		private FontUnit WeekendDayFontSize;

		// Token: 0x04000250 RID: 592
		private Color WeekendDayForeColor;

		// Token: 0x04000251 RID: 593
		private Color SelectorBackColor;

		// Token: 0x04000252 RID: 594
		private int SelectorFont;

		// Token: 0x04000253 RID: 595
		private string SelectorFontName;

		// Token: 0x04000254 RID: 596
		private FontUnit SelectorFontSize;

		// Token: 0x04000255 RID: 597
		private Color SelectorForeColor;

		// Token: 0x04000256 RID: 598
		private Unit SelectorWidth;

		// Token: 0x04000257 RID: 599
		private const int FONT_BOLD = 1;

		// Token: 0x04000258 RID: 600
		private const int FONT_ITALIC = 2;

		// Token: 0x04000259 RID: 601
		private const int FONT_UNDERLINE = 4;
	}
}
