using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A4 RID: 164
	[ToolboxItem(false)]
	[Obsolete("Use of this type is not recommended because the AutoFormat dialog is launched by the designer host. The list of available AutoFormats is exposed on the ControlDesigner in the AutoFormats property. http://go.microsoft.com/fwlink/?linkid=14202")]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public partial class CalendarAutoFormatDialog : Form
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x00017AC0 File Offset: 0x00015CC0
		public CalendarAutoFormatDialog(Calendar calendar)
		{
			this.calendar = calendar;
			this.InitForm();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00017ADC File Offset: 0x00015CDC
		protected void DoDelayLoadActions()
		{
			this.schemePreview.CreateTrident();
			this.schemePreview.ActivateTrident();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00017AF8 File Offset: 0x00015CF8
		private Calendar GetPreviewCalendar()
		{
			Calendar calendar = new Calendar();
			calendar.ShowTitle = this.calendar.ShowTitle;
			calendar.ShowNextPrevMonth = this.calendar.ShowNextPrevMonth;
			calendar.ShowDayHeader = this.calendar.ShowDayHeader;
			calendar.SelectionMode = this.calendar.SelectionMode;
			CalendarAutoFormatDialog.WCScheme wcscheme = (CalendarAutoFormatDialog.WCScheme)this.schemeNameList.SelectedItem;
			wcscheme.Apply(calendar);
			return calendar;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00017B68 File Offset: 0x00015D68
		private void InitForm()
		{
			this.schemeNameLabel = new System.Windows.Forms.Label();
			this.schemeNameList = new System.Windows.Forms.ListBox();
			this.schemePreviewLabel = new System.Windows.Forms.Label();
			this.schemePreview = new MSHTMLHost();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			System.Windows.Forms.Button button = new System.Windows.Forms.Button();
			this.schemeNameLabel.SetBounds(8, 10, 154, 16);
			this.schemeNameLabel.Text = SR.GetString("CalAFmt_SchemeName");
			this.schemeNameLabel.TabStop = false;
			this.schemeNameLabel.TabIndex = 1;
			this.schemeNameList.TabIndex = 2;
			this.schemeNameList.SetBounds(8, 26, 150, 100);
			this.schemeNameList.UseTabStops = true;
			this.schemeNameList.IntegralHeight = false;
			this.schemeNameList.Items.AddRange(new object[]
			{
				new CalendarAutoFormatDialog.WCSchemeNone(),
				new CalendarAutoFormatDialog.WCSchemeStandard(),
				new CalendarAutoFormatDialog.WCSchemeProfessional1(),
				new CalendarAutoFormatDialog.WCSchemeProfessional2(),
				new CalendarAutoFormatDialog.WCSchemeClassic(),
				new CalendarAutoFormatDialog.WCSchemeColorful1(),
				new CalendarAutoFormatDialog.WCSchemeColorful2()
			});
			this.schemeNameList.SelectedIndexChanged += this.OnSelChangedScheme;
			this.schemePreviewLabel.SetBounds(165, 10, 92, 16);
			this.schemePreviewLabel.Text = SR.GetString("CalAFmt_Preview");
			this.schemePreviewLabel.TabStop = false;
			this.schemePreviewLabel.TabIndex = 3;
			this.schemePreview.SetBounds(165, 26, 270, 240);
			this.schemePreview.TabIndex = 4;
			this.schemePreview.TabStop = false;
			button.Location = new Point(360, 276);
			button.Size = new Size(75, 23);
			button.TabIndex = 7;
			button.Text = SR.GetString("CalAFmt_Help");
			button.FlatStyle = FlatStyle.System;
			button.Click += this.OnClickHelp;
			this.okButton.Location = new Point(198, 276);
			this.okButton.Size = new Size(75, 23);
			this.okButton.TabIndex = 5;
			this.okButton.Text = SR.GetString("CalAFmt_OK");
			this.okButton.DialogResult = DialogResult.OK;
			this.okButton.FlatStyle = FlatStyle.System;
			this.okButton.Click += this.OnOKClicked;
			this.cancelButton.Location = new Point(279, 276);
			this.cancelButton.Size = new Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = SR.GetString("CalAFmt_Cancel");
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.Text = SR.GetString("CalAFmt_Title");
			base.Size = new Size(450, 336);
			base.AcceptButton = this.okButton;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.CancelButton = this.cancelButton;
			base.Icon = null;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Activated += this.OnActivated;
			base.HelpRequested += this.OnHelpRequested;
			Font dialogFont = UIServiceHelper.GetDialogFont(this.calendar.Site);
			if (dialogFont != null)
			{
				this.Font = dialogFont;
			}
			base.Controls.Clear();
			base.Controls.AddRange(new Control[]
			{
				this.schemePreview,
				this.schemePreviewLabel,
				this.schemeNameList,
				this.schemeNameLabel,
				this.okButton,
				this.cancelButton,
				button
			});
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00017F49 File Offset: 0x00016149
		protected void OnActivated(object source, EventArgs e)
		{
			if (!this.firstActivate)
			{
				return;
			}
			this.schemeDirty = false;
			this.DoDelayLoadActions();
			this.schemeNameList.SelectedIndex = 0;
			this.firstActivate = false;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00017F74 File Offset: 0x00016174
		private void ShowHelp()
		{
			ISite site = this.calendar.Site;
			IHelpService helpService = (IHelpService)site.GetService(typeof(IHelpService));
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("net.Asp.Calendar.AutoFormat");
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00017FB1 File Offset: 0x000161B1
		private void OnClickHelp(object sender, EventArgs e)
		{
			this.ShowHelp();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00017FB1 File Offset: 0x000161B1
		private void OnHelpRequested(object sender, HelpEventArgs e)
		{
			this.ShowHelp();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00017FB9 File Offset: 0x000161B9
		protected void OnSelChangedScheme(object source, EventArgs e)
		{
			this.schemeDirty = true;
			this.UpdateSchemePreview();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00017FC8 File Offset: 0x000161C8
		protected void OnOKClicked(object source, EventArgs e)
		{
			this.SaveComponent();
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00017FD0 File Offset: 0x000161D0
		protected void SaveComponent()
		{
			if (this.schemeDirty)
			{
				CalendarAutoFormatDialog.WCScheme wcscheme = (CalendarAutoFormatDialog.WCScheme)this.schemeNameList.SelectedItem;
				wcscheme.Apply(this.calendar);
				this.schemeDirty = false;
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001800C File Offset: 0x0001620C
		private void UpdateSchemePreview()
		{
			Calendar previewCalendar = this.GetPreviewCalendar();
			IDesigner designer = TypeDescriptor.CreateDesigner(previewCalendar, typeof(IDesigner));
			designer.Initialize(previewCalendar);
			CalendarDesigner calendarDesigner = (CalendarDesigner)designer;
			string designTimeHtml = calendarDesigner.GetDesignTimeHtml();
			NativeMethods.IHTMLDocument2 document = this.schemePreview.GetDocument();
			NativeMethods.IHTMLElement body = document.GetBody();
			body.SetInnerHTML(designTimeHtml);
		}

		// Token: 0x0400025A RID: 602
		private System.Windows.Forms.Label schemeNameLabel;

		// Token: 0x0400025B RID: 603
		private System.Windows.Forms.ListBox schemeNameList;

		// Token: 0x0400025C RID: 604
		private System.Windows.Forms.Label schemePreviewLabel;

		// Token: 0x0400025D RID: 605
		private System.Windows.Forms.Button cancelButton;

		// Token: 0x0400025E RID: 606
		private System.Windows.Forms.Button okButton;

		// Token: 0x0400025F RID: 607
		private MSHTMLHost schemePreview;

		// Token: 0x04000260 RID: 608
		private Calendar calendar;

		// Token: 0x04000261 RID: 609
		private bool schemeDirty;

		// Token: 0x04000262 RID: 610
		private bool firstActivate = true;

		// Token: 0x020003CD RID: 973
		private abstract class WCScheme
		{
			// Token: 0x060026E8 RID: 9960
			public abstract string GetDescription();

			// Token: 0x060026E9 RID: 9961
			public abstract void Apply(Calendar wc);

			// Token: 0x060026EA RID: 9962 RVA: 0x000EFF45 File Offset: 0x000EE145
			public override string ToString()
			{
				return this.GetDescription();
			}

			// Token: 0x060026EB RID: 9963 RVA: 0x000EFF50 File Offset: 0x000EE150
			public static void ClearCalendar(Calendar wc)
			{
				wc.TitleStyle.Reset();
				wc.NextPrevStyle.Reset();
				wc.DayHeaderStyle.Reset();
				wc.SelectorStyle.Reset();
				wc.DayStyle.Reset();
				wc.OtherMonthDayStyle.Reset();
				wc.WeekendDayStyle.Reset();
				wc.TodayDayStyle.Reset();
				wc.SelectedDayStyle.Reset();
				wc.ControlStyle.Reset();
			}
		}

		// Token: 0x020003CE RID: 974
		private class WCSchemeNone : CalendarAutoFormatDialog.WCScheme
		{
			// Token: 0x060026ED RID: 9965 RVA: 0x000EFFCB File Offset: 0x000EE1CB
			public override string GetDescription()
			{
				return SR.GetString("CalAFmt_Scheme_Default");
			}

			// Token: 0x060026EE RID: 9966 RVA: 0x000EFFD7 File Offset: 0x000EE1D7
			public override void Apply(Calendar wc)
			{
				CalendarAutoFormatDialog.WCScheme.ClearCalendar(wc);
				wc.DayNameFormat = DayNameFormat.Short;
				wc.NextPrevFormat = NextPrevFormat.CustomText;
				wc.TitleFormat = TitleFormat.MonthYear;
				wc.CellPadding = 2;
				wc.CellSpacing = 0;
				wc.ShowGridLines = false;
			}
		}

		// Token: 0x020003CF RID: 975
		private class WCSchemeStandard : CalendarAutoFormatDialog.WCScheme
		{
			// Token: 0x060026F0 RID: 9968 RVA: 0x000F0011 File Offset: 0x000EE211
			public override string GetDescription()
			{
				return SR.GetString("CalAFmt_Scheme_Simple");
			}

			// Token: 0x060026F1 RID: 9969 RVA: 0x000F0020 File Offset: 0x000EE220
			public override void Apply(Calendar wc)
			{
				CalendarAutoFormatDialog.WCScheme.ClearCalendar(wc);
				wc.DayNameFormat = DayNameFormat.FirstLetter;
				wc.NextPrevFormat = NextPrevFormat.CustomText;
				wc.TitleFormat = TitleFormat.MonthYear;
				wc.CellPadding = 4;
				wc.CellSpacing = 0;
				wc.ShowGridLines = false;
				wc.Height = Unit.Pixel(180);
				wc.Width = Unit.Pixel(200);
				wc.BorderColor = Color.FromArgb(153, 153, 153);
				wc.ForeColor = Color.Black;
				wc.BackColor = Color.White;
				wc.Font.Name = "Verdana";
				wc.Font.Size = FontUnit.Point(8);
				wc.TitleStyle.Font.Bold = true;
				wc.TitleStyle.BorderColor = Color.Black;
				wc.TitleStyle.BackColor = Color.FromArgb(153, 153, 153);
				wc.NextPrevStyle.VerticalAlign = VerticalAlign.Bottom;
				wc.DayHeaderStyle.Font.Bold = true;
				wc.DayHeaderStyle.Font.Size = FontUnit.Point(7);
				wc.DayHeaderStyle.BackColor = Color.FromArgb(204, 204, 204);
				wc.SelectorStyle.BackColor = Color.FromArgb(204, 204, 204);
				wc.TodayDayStyle.BackColor = Color.FromArgb(204, 204, 204);
				wc.TodayDayStyle.ForeColor = Color.Black;
				wc.SelectedDayStyle.BackColor = Color.FromArgb(102, 102, 102);
				wc.SelectedDayStyle.ForeColor = Color.White;
				wc.SelectedDayStyle.Font.Bold = true;
				wc.OtherMonthDayStyle.ForeColor = Color.FromArgb(128, 128, 128);
				wc.WeekendDayStyle.BackColor = Color.FromArgb(255, 255, 204);
			}
		}

		// Token: 0x020003D0 RID: 976
		private class WCSchemeProfessional1 : CalendarAutoFormatDialog.WCScheme
		{
			// Token: 0x060026F3 RID: 9971 RVA: 0x000F0223 File Offset: 0x000EE423
			public override string GetDescription()
			{
				return SR.GetString("CalAFmt_Scheme_Professional1");
			}

			// Token: 0x060026F4 RID: 9972 RVA: 0x000F0230 File Offset: 0x000EE430
			public override void Apply(Calendar wc)
			{
				CalendarAutoFormatDialog.WCScheme.ClearCalendar(wc);
				wc.DayNameFormat = DayNameFormat.Short;
				wc.NextPrevFormat = NextPrevFormat.FullMonth;
				wc.TitleFormat = TitleFormat.MonthYear;
				wc.CellPadding = 2;
				wc.CellSpacing = 0;
				wc.ShowGridLines = false;
				wc.Height = Unit.Pixel(190);
				wc.Width = Unit.Pixel(350);
				wc.BorderColor = Color.White;
				wc.BorderWidth = Unit.Pixel(1);
				wc.ForeColor = Color.Black;
				wc.BackColor = Color.White;
				wc.Font.Name = "Verdana";
				wc.Font.Size = FontUnit.Point(9);
				wc.TitleStyle.Font.Bold = true;
				wc.TitleStyle.BorderColor = Color.Black;
				wc.TitleStyle.BorderWidth = Unit.Pixel(4);
				wc.TitleStyle.ForeColor = Color.FromArgb(51, 51, 153);
				wc.TitleStyle.BackColor = Color.White;
				wc.TitleStyle.Font.Size = FontUnit.Point(12);
				wc.NextPrevStyle.Font.Bold = true;
				wc.NextPrevStyle.Font.Size = FontUnit.Point(8);
				wc.NextPrevStyle.VerticalAlign = VerticalAlign.Bottom;
				wc.NextPrevStyle.ForeColor = Color.FromArgb(51, 51, 51);
				wc.DayHeaderStyle.Font.Bold = true;
				wc.DayHeaderStyle.Font.Size = FontUnit.Point(8);
				wc.TodayDayStyle.BackColor = Color.FromArgb(204, 204, 204);
				wc.SelectedDayStyle.BackColor = Color.FromArgb(51, 51, 153);
				wc.SelectedDayStyle.ForeColor = Color.White;
				wc.OtherMonthDayStyle.ForeColor = Color.FromArgb(153, 153, 153);
			}
		}

		// Token: 0x020003D1 RID: 977
		private class WCSchemeProfessional2 : CalendarAutoFormatDialog.WCScheme
		{
			// Token: 0x060026F6 RID: 9974 RVA: 0x000F0425 File Offset: 0x000EE625
			public override string GetDescription()
			{
				return SR.GetString("CalAFmt_Scheme_Professional2");
			}

			// Token: 0x060026F7 RID: 9975 RVA: 0x000F0434 File Offset: 0x000EE634
			public override void Apply(Calendar wc)
			{
				CalendarAutoFormatDialog.WCScheme.ClearCalendar(wc);
				wc.DayNameFormat = DayNameFormat.Short;
				wc.NextPrevFormat = NextPrevFormat.ShortMonth;
				wc.TitleFormat = TitleFormat.MonthYear;
				wc.CellPadding = 2;
				wc.CellSpacing = 1;
				wc.ShowGridLines = false;
				wc.Height = Unit.Pixel(250);
				wc.Width = Unit.Pixel(330);
				wc.BackColor = Color.White;
				wc.BorderColor = Color.Black;
				wc.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
				wc.ForeColor = Color.Black;
				wc.Font.Name = "Verdana";
				wc.Font.Size = FontUnit.Point(9);
				wc.TitleStyle.Font.Bold = true;
				wc.TitleStyle.ForeColor = Color.White;
				wc.TitleStyle.BackColor = Color.FromArgb(51, 51, 153);
				wc.TitleStyle.Font.Size = FontUnit.Point(12);
				wc.TitleStyle.Height = Unit.Point(12);
				wc.NextPrevStyle.Font.Bold = true;
				wc.NextPrevStyle.Font.Size = FontUnit.Point(8);
				wc.NextPrevStyle.ForeColor = Color.White;
				wc.DayHeaderStyle.ForeColor = Color.FromArgb(51, 51, 51);
				wc.DayHeaderStyle.Font.Bold = true;
				wc.DayHeaderStyle.Font.Size = FontUnit.Point(8);
				wc.DayHeaderStyle.Height = Unit.Point(8);
				wc.DayStyle.BackColor = Color.FromArgb(204, 204, 204);
				wc.TodayDayStyle.BackColor = Color.FromArgb(153, 153, 153);
				wc.TodayDayStyle.ForeColor = Color.White;
				wc.SelectedDayStyle.BackColor = Color.FromArgb(51, 51, 153);
				wc.SelectedDayStyle.ForeColor = Color.White;
				wc.OtherMonthDayStyle.ForeColor = Color.FromArgb(153, 153, 153);
			}
		}

		// Token: 0x020003D2 RID: 978
		private class WCSchemeClassic : CalendarAutoFormatDialog.WCScheme
		{
			// Token: 0x060026F9 RID: 9977 RVA: 0x000F0659 File Offset: 0x000EE859
			public override string GetDescription()
			{
				return SR.GetString("CalAFmt_Scheme_Classic");
			}

			// Token: 0x060026FA RID: 9978 RVA: 0x000F0668 File Offset: 0x000EE868
			public override void Apply(Calendar wc)
			{
				CalendarAutoFormatDialog.WCScheme.ClearCalendar(wc);
				wc.DayNameFormat = DayNameFormat.FirstLetter;
				wc.NextPrevFormat = NextPrevFormat.FullMonth;
				wc.TitleFormat = TitleFormat.Month;
				wc.CellPadding = 2;
				wc.CellSpacing = 0;
				wc.ShowGridLines = false;
				wc.Height = Unit.Pixel(220);
				wc.Width = Unit.Pixel(400);
				wc.BackColor = Color.White;
				wc.BorderColor = Color.Black;
				wc.ForeColor = Color.Black;
				wc.Font.Name = "Times New Roman";
				wc.Font.Size = FontUnit.Point(10);
				wc.TitleStyle.Font.Bold = true;
				wc.TitleStyle.ForeColor = Color.White;
				wc.TitleStyle.BackColor = Color.Black;
				wc.TitleStyle.Font.Size = FontUnit.Point(13);
				wc.TitleStyle.Height = Unit.Point(14);
				wc.NextPrevStyle.ForeColor = Color.White;
				wc.NextPrevStyle.Font.Size = FontUnit.Point(8);
				wc.DayHeaderStyle.Font.Bold = true;
				wc.DayHeaderStyle.Font.Size = FontUnit.Point(7);
				wc.DayHeaderStyle.Font.Name = "Verdana";
				wc.DayHeaderStyle.BackColor = Color.FromArgb(204, 204, 204);
				wc.DayHeaderStyle.ForeColor = Color.FromArgb(51, 51, 51);
				wc.DayHeaderStyle.Height = Unit.Pixel(10);
				wc.SelectorStyle.BackColor = Color.FromArgb(204, 204, 204);
				wc.SelectorStyle.ForeColor = Color.FromArgb(51, 51, 51);
				wc.SelectorStyle.Font.Bold = true;
				wc.SelectorStyle.Font.Size = FontUnit.Point(8);
				wc.SelectorStyle.Font.Name = "Verdana";
				wc.SelectorStyle.Width = Unit.Percentage(1.0);
				wc.DayStyle.Width = Unit.Percentage(14.0);
				wc.TodayDayStyle.BackColor = Color.FromArgb(204, 204, 153);
				wc.SelectedDayStyle.BackColor = Color.FromArgb(204, 51, 51);
				wc.SelectedDayStyle.ForeColor = Color.White;
				wc.OtherMonthDayStyle.ForeColor = Color.FromArgb(153, 153, 153);
			}
		}

		// Token: 0x020003D3 RID: 979
		private class WCSchemeColorful1 : CalendarAutoFormatDialog.WCScheme
		{
			// Token: 0x060026FC RID: 9980 RVA: 0x000F0915 File Offset: 0x000EEB15
			public override string GetDescription()
			{
				return SR.GetString("CalAFmt_Scheme_Colorful1");
			}

			// Token: 0x060026FD RID: 9981 RVA: 0x000F0924 File Offset: 0x000EEB24
			public override void Apply(Calendar wc)
			{
				CalendarAutoFormatDialog.WCScheme.ClearCalendar(wc);
				wc.DayNameFormat = DayNameFormat.FirstLetter;
				wc.NextPrevFormat = NextPrevFormat.CustomText;
				wc.TitleFormat = TitleFormat.MonthYear;
				wc.CellPadding = 2;
				wc.CellSpacing = 0;
				wc.ShowGridLines = true;
				wc.Height = Unit.Pixel(200);
				wc.Width = Unit.Pixel(220);
				wc.BackColor = Color.FromArgb(255, 255, 204);
				wc.BorderColor = Color.FromArgb(255, 204, 102);
				wc.BorderWidth = Unit.Pixel(1);
				wc.ForeColor = Color.FromArgb(102, 51, 153);
				wc.Font.Name = "Verdana";
				wc.Font.Size = FontUnit.Point(8);
				wc.TitleStyle.Font.Bold = true;
				wc.TitleStyle.Font.Size = FontUnit.Point(9);
				wc.TitleStyle.BackColor = Color.FromArgb(153, 0, 0);
				wc.TitleStyle.ForeColor = Color.FromArgb(255, 255, 204);
				wc.NextPrevStyle.ForeColor = Color.FromArgb(255, 255, 204);
				wc.NextPrevStyle.Font.Size = FontUnit.Point(9);
				wc.DayHeaderStyle.BackColor = Color.FromArgb(255, 204, 102);
				wc.DayHeaderStyle.Height = Unit.Pixel(1);
				wc.SelectorStyle.BackColor = Color.FromArgb(255, 204, 102);
				wc.SelectedDayStyle.BackColor = Color.FromArgb(204, 204, 255);
				wc.SelectedDayStyle.Font.Bold = true;
				wc.OtherMonthDayStyle.ForeColor = Color.FromArgb(204, 153, 102);
				wc.TodayDayStyle.ForeColor = Color.White;
				wc.TodayDayStyle.BackColor = Color.FromArgb(255, 204, 102);
			}
		}

		// Token: 0x020003D4 RID: 980
		private class WCSchemeColorful2 : CalendarAutoFormatDialog.WCScheme
		{
			// Token: 0x060026FF RID: 9983 RVA: 0x000F0B48 File Offset: 0x000EED48
			public override string GetDescription()
			{
				return SR.GetString("CalAFmt_Scheme_Colorful2");
			}

			// Token: 0x06002700 RID: 9984 RVA: 0x000F0B54 File Offset: 0x000EED54
			public override void Apply(Calendar wc)
			{
				CalendarAutoFormatDialog.WCScheme.ClearCalendar(wc);
				wc.DayNameFormat = DayNameFormat.FirstLetter;
				wc.NextPrevFormat = NextPrevFormat.CustomText;
				wc.TitleFormat = TitleFormat.MonthYear;
				wc.CellPadding = 1;
				wc.CellSpacing = 0;
				wc.ShowGridLines = false;
				wc.Height = Unit.Pixel(200);
				wc.Width = Unit.Pixel(220);
				wc.BackColor = Color.White;
				wc.BorderColor = Color.FromArgb(51, 102, 204);
				wc.BorderWidth = Unit.Pixel(1);
				wc.ForeColor = Color.FromArgb(0, 51, 153);
				wc.Font.Name = "Verdana";
				wc.Font.Size = FontUnit.Point(8);
				wc.TitleStyle.Font.Bold = true;
				wc.TitleStyle.Font.Size = FontUnit.Point(10);
				wc.TitleStyle.BackColor = Color.FromArgb(0, 51, 153);
				wc.TitleStyle.ForeColor = Color.FromArgb(204, 204, 255);
				wc.TitleStyle.BorderColor = Color.FromArgb(51, 102, 204);
				wc.TitleStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
				wc.TitleStyle.BorderWidth = Unit.Pixel(1);
				wc.TitleStyle.Height = Unit.Pixel(25);
				wc.NextPrevStyle.ForeColor = Color.FromArgb(204, 204, 255);
				wc.NextPrevStyle.Font.Size = FontUnit.Point(8);
				wc.DayHeaderStyle.BackColor = Color.FromArgb(153, 204, 204);
				wc.DayHeaderStyle.ForeColor = Color.FromArgb(51, 102, 102);
				wc.DayHeaderStyle.Height = Unit.Pixel(1);
				wc.SelectorStyle.BackColor = Color.FromArgb(153, 204, 204);
				wc.SelectorStyle.ForeColor = Color.FromArgb(51, 102, 102);
				wc.SelectedDayStyle.BackColor = Color.FromArgb(0, 153, 153);
				wc.SelectedDayStyle.ForeColor = Color.FromArgb(204, 255, 153);
				wc.SelectedDayStyle.Font.Bold = true;
				wc.OtherMonthDayStyle.ForeColor = Color.FromArgb(153, 153, 153);
				wc.TodayDayStyle.ForeColor = Color.White;
				wc.TodayDayStyle.BackColor = Color.FromArgb(153, 204, 204);
				wc.WeekendDayStyle.BackColor = Color.FromArgb(204, 204, 255);
			}
		}
	}
}
