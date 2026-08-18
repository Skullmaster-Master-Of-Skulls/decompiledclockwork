using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.Utils;

namespace Telerik.Web.UI.Calendar.View
{
	// Token: 0x02001019 RID: 4121
	public class MonthView : CalendarView
	{
		// Token: 0x0600A28A RID: 41610 RVA: 0x002414C8 File Offset: 0x0023F6C8
		public MonthView(RadCalendar parent) : base(parent)
		{
			base.ParentCalendar = parent;
			this.DefaultCalendar = parent.DateTimeFormat.Calendar;
		}

		// Token: 0x0600A28B RID: 41611 RVA: 0x00241520 File Offset: 0x0023F720
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public MonthView(RadCalendar parent, DateTime inMonthDate) : base(parent)
		{
			this._ViewInMonthDate = inMonthDate;
			this.Initialize();
		}

		// Token: 0x0600A28C RID: 41612 RVA: 0x00241570 File Offset: 0x0023F770
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public MonthView(RadCalendar parent, DateTime inMonthDate, CalendarView parentView) : base(parent, parentView)
		{
			this._ViewInMonthDate = inMonthDate;
			this.Initialize();
		}

		// Token: 0x0600A28D RID: 41613 RVA: 0x002415C0 File Offset: 0x0023F7C0
		internal override string GetTitleContent()
		{
			if (!base.IsTopView)
			{
				return this.EffectiveVisibleDate().ToString(this.TitleFormat, base.ParentCalendar.DateTimeFormat);
			}
			if (base.IsSingleView)
			{
				return this.EffectiveVisibleDate().ToString(base.ParentCalendar.ResolvedTitleFormat(), base.ParentCalendar.CultureInfo);
			}
			string str = base.ViewStartDate.ToString(base.ParentCalendar.ResolvedTitleFormat(), base.ParentCalendar.CultureInfo);
			string str2 = base.ViewEndDate.ToString(base.ParentCalendar.ResolvedTitleFormat(), base.ParentCalendar.CultureInfo);
			return str + base.ParentCalendar.DateRangeSeparator + str2;
		}

		// Token: 0x0600A28E RID: 41614 RVA: 0x00241690 File Offset: 0x0023F890
		private TableRow RenderViewTitle()
		{
			int num = 0;
			int columnSpan = 0;
			base.GetViewRowsAndColumns(out num, out columnSpan);
			TableCell tableCell = new TableCell();
			TableRow tableRow = new TableRow();
			tableRow.Cells.Add(tableCell);
			tableCell.ColumnSpan = columnSpan;
			tableCell.Style["text-align"] = base.TitleAlign.ToString().ToLower();
			tableCell.Text = base.Title;
			tableCell.ApplyStyle(base.HeaderSettings);
			tableCell.ID = base.ID + "_hd";
			tableCell.CssClass = "rcTitle";
			return tableRow;
		}

		// Token: 0x0600A28F RID: 41615 RVA: 0x0024172A File Offset: 0x0023F92A
		public override CalendarView CreateCalendarView()
		{
			return new MonthView(base.ParentCalendar);
		}

		// Token: 0x0600A290 RID: 41616 RVA: 0x00241738 File Offset: 0x0023F938
		internal void SetViewProperties(Table viewMatrix)
		{
			viewMatrix.ID = base.ID;
			if (base.IsTopView)
			{
				viewMatrix.Width = base.ParentCalendar.Width;
				viewMatrix.Height = base.ParentCalendar.Height;
			}
			else
			{
				viewMatrix.Width = base.SingleViewWidth;
				viewMatrix.Height = base.SingleViewHeight;
			}
			TableItemStyle calendarTableStyle = base.ParentCalendar.CalendarTableStyle;
			viewMatrix.ApplyStyle(calendarTableStyle);
			viewMatrix.CellPadding = base.ParentCalendar.DefaultCellPadding;
			if (base.ParentCalendar.ResolvedRenderMode == RenderMode.Classic)
			{
				viewMatrix.Attributes["cellspacing"] = base.ParentCalendar.DefaultCellSpacing.ToString();
			}
			if (base.IsHidden)
			{
				viewMatrix.Style["display"] = "none";
			}
			if (!string.IsNullOrEmpty(base.ParentCalendar.DaysViewSummary) && base.ParentCalendar.EnableAriaSupport)
			{
				viewMatrix.Attributes["summary"] = base.ParentCalendar.DaysViewSummary;
			}
			if (!string.IsNullOrEmpty(base.Title))
			{
				viewMatrix.Caption = string.Format("<span style='display:none;'>{0}</span>", base.Title);
			}
		}

		// Token: 0x0600A291 RID: 41617 RVA: 0x00241864 File Offset: 0x0023FA64
		internal void GetMultiViewCounts(out int previousViews, out int nextViews)
		{
			int multiViewRows = base.MultiViewRows;
			int multiViewColumns = base.MultiViewColumns;
			int defaultRow = base.DefaultRow;
			int defaultColumn = base.DefaultColumn;
			int num = defaultColumn + defaultRow * multiViewColumns;
			int num2 = multiViewColumns - (defaultColumn + 1) + (multiViewRows - (defaultRow + 1)) * multiViewColumns;
			previousViews = num;
			nextViews = num2;
		}

		// Token: 0x0600A292 RID: 41618 RVA: 0x002418AC File Offset: 0x0023FAAC
		internal Table CreateMultiViewMatrix()
		{
			int rows = 0;
			int columns = 0;
			base.GetViewRowsAndColumns(out rows, out columns);
			Table table = this.CreateBaseTable(rows, columns);
			if (base.ShowRowHeaders || base.EnableViewSelector || base.ShowColumnHeaders)
			{
				this.SetMultiViewHeaders(table, base.Orientation);
			}
			this.RenderViews(table, this.FirstCalendarDay(this.EffectiveVisibleDate()), this.EffectiveVisibleDate(), base.Orientation);
			if (base.ShowCalendarViewHeader)
			{
				TableRow row = this.RenderViewTitle();
				table.Rows.AddAt(0, row);
			}
			this.SetMultiViewProperties(table);
			return table;
		}

		// Token: 0x0600A293 RID: 41619 RVA: 0x0024193A File Offset: 0x0023FB3A
		internal void SetMultiViewProperties(Table viewMatrix)
		{
			this.SetViewProperties(viewMatrix);
		}

		// Token: 0x0600A294 RID: 41620 RVA: 0x00241944 File Offset: 0x0023FB44
		internal void InitializeMultiViewData()
		{
			base.ChildViews.Clear();
			int num = 0;
			int num2 = 0;
			this.GetMultiViewCounts(out num, out num2);
			MonthView[] array = new MonthView[num];
			MonthView[] array2 = new MonthView[num2];
			DateTime dateTime = this.EffectiveVisibleDate();
			MonthView monthView = new MonthView(base.ParentCalendar, dateTime, this);
			for (int i = 0; i < num2; i++)
			{
				DateTime inMonthDate = base.ParentCalendar.Calendar.AddMonths(dateTime, i + 1);
				array2[i] = new MonthView(base.ParentCalendar, inMonthDate, this);
			}
			int num3 = 1;
			int num4 = num - 1;
			while (0 <= num4)
			{
				DateTime inMonthDate2 = base.ParentCalendar.Calendar.AddMonths(dateTime, -num3);
				array2[num4] = new MonthView(base.ParentCalendar, inMonthDate2, this);
				num3++;
				num4--;
			}
			for (int j = 0; j < num; j++)
			{
				base.ChildViews.Add(array[j]);
			}
			monthView.Initialize();
			base.ChildViews.Add(monthView);
			for (int k = 0; k < num2; k++)
			{
				base.ChildViews.Add(array2[k]);
			}
			for (int l = 0; l < base.ChildViews.Count; l++)
			{
				base.ChildViews[l].ID = base.ID + "_" + l.ToString();
				base.ShowCalendarViewHeader = true;
			}
		}

		// Token: 0x0600A295 RID: 41621 RVA: 0x00241AB4 File Offset: 0x0023FCB4
		private void RenderViews(Table singleViewMatrix, DateTime firstDay, DateTime visibleDate, Orientation orientation)
		{
			System.Globalization.Calendar calendar = base.ParentCalendar.Calendar;
			int num = 0;
			int num2 = 0;
			if (base.ShowRowHeaders || base.EnableViewSelector)
			{
				num++;
			}
			if (base.ShowColumnHeaders || base.EnableViewSelector)
			{
				num2++;
			}
			int num3 = 0;
			int num4 = 0;
			base.GetViewRowsAndColumns(out num3, out num4);
			if (base.ParentCalendar.IsDesignMode)
			{
				this.InitializeMultiViewData();
			}
			int num5 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			HtmlTextWriter htmlTextWriter = CalendarRenderer.CreateHtmlWriter(stringBuilder);
			if (orientation == Orientation.RenderInRows)
			{
				for (int i = num2; i < num3; i++)
				{
					for (int j = num; j < num4; j++)
					{
						base.ChildViews[num5].Render(htmlTextWriter);
						singleViewMatrix.Rows[i].Cells[j].Text = stringBuilder.ToString();
						htmlTextWriter.Flush();
						stringBuilder.Length = 0;
						num5++;
					}
				}
				return;
			}
			if (orientation == Orientation.RenderInColumns)
			{
				for (int k = num; k < num4; k++)
				{
					for (int l = num2; l < num3; l++)
					{
						base.ChildViews[num5].Render(htmlTextWriter);
						singleViewMatrix.Rows[l].Cells[k].Text = stringBuilder.ToString();
						htmlTextWriter.Flush();
						stringBuilder.Length = 0;
						num5++;
					}
				}
			}
		}

		// Token: 0x0600A296 RID: 41622 RVA: 0x00241C20 File Offset: 0x0023FE20
		internal void SetMultiViewHeaders(Table viewMatrix, Orientation orientation)
		{
			string prefixOfID = base.ID + "_vs";
			if (base.EnableViewSelector)
			{
				this.SetHeaderCell(viewMatrix.Rows[0].Cells[0], HeaderType.View, base.ParentCalendar.ViewSelectorText, base.ParentCalendar.ViewSelectorImage, Utility.SetCellID(prefixOfID, 0));
			}
			DateTime visibleDate = this.EffectiveVisibleDate();
			this.FirstCalendarDay(visibleDate);
			this.RenderMultiviewColumnHeader(viewMatrix);
			this.RenderMultiviewRowHeader(viewMatrix);
			if ((base.ShowColumnHeaders && base.ShowRowHeaders) || base.EnableViewSelector)
			{
				this.SetCellCss(viewMatrix, -1, 0, base.ParentCalendar.HeaderStyle);
				this.SetCellCss(viewMatrix, 0, -1, base.ParentCalendar.HeaderStyle);
				return;
			}
			if (base.ShowColumnHeaders)
			{
				this.SetCellCss(viewMatrix, -1, 0, base.ParentCalendar.HeaderStyle);
				return;
			}
			if (base.ShowRowHeaders)
			{
				this.SetCellCss(viewMatrix, 0, -1, base.ParentCalendar.HeaderStyle);
			}
		}

		// Token: 0x0600A297 RID: 41623 RVA: 0x00241D18 File Offset: 0x0023FF18
		private void RenderMultiviewColumnHeader(Table singleViewMatrix)
		{
			int num = 0;
			string prefixOfID = base.ID + "_cs";
			if (base.ShowRowHeaders)
			{
				num++;
			}
			if (base.ShowColumnHeaders)
			{
				for (int i = num; i < singleViewMatrix.Rows[0].Cells.Count; i++)
				{
					this.SetHeaderCell(singleViewMatrix.Rows[0].Cells[i], HeaderType.Column, base.ColumnHeaderText, base.ColumnHeaderImage, Utility.SetCellID(prefixOfID, i));
				}
			}
		}

		// Token: 0x0600A298 RID: 41624 RVA: 0x00241DA0 File Offset: 0x0023FFA0
		private void RenderMultiviewRowHeader(Table singleViewMatrix)
		{
			string prefixOfID = base.ID + "_rs";
			int num = 0;
			if (base.ShowColumnHeaders)
			{
				num++;
			}
			if (base.ShowRowHeaders)
			{
				for (int i = num; i < singleViewMatrix.Rows.Count; i++)
				{
					this.SetHeaderCell(singleViewMatrix.Rows[i].Cells[0], HeaderType.Row, base.RowSelectorText, base.RowHeaderImage, Utility.SetCellID(prefixOfID, i));
				}
			}
		}

		// Token: 0x0600A299 RID: 41625 RVA: 0x00241E1C File Offset: 0x0024001C
		internal Table CreateSingleViewMatrix()
		{
			int rows = 0;
			int columns = 0;
			base.GetViewRowsAndColumns(out rows, out columns);
			Table table = this.CreateBaseTable(rows, columns);
			if (base.ShowRowHeaders || base.EnableViewSelector || base.ShowColumnHeaders)
			{
				this.SetSingleViewHeaders(table, base.Orientation);
			}
			this.RenderDays(table, this.FirstCalendarDay(this.EffectiveVisibleDate()), this.EffectiveVisibleDate(), base.Orientation);
			if (base.ShowCalendarViewHeader)
			{
				TableRow row = this.RenderViewTitle();
				table.Rows.AddAt(0, row);
			}
			this.SetSingleViewProperties(table);
			return table;
		}

		// Token: 0x0600A29A RID: 41626 RVA: 0x00241EAA File Offset: 0x002400AA
		internal void SetSingleViewProperties(Table singleviewMatrix)
		{
			this.SetViewProperties(singleviewMatrix);
		}

		// Token: 0x0600A29B RID: 41627 RVA: 0x00241EB4 File Offset: 0x002400B4
		private void RenderDays(Table singleViewMatrix, DateTime firstDay, DateTime visibleDate, Orientation orientation)
		{
			DateTime dateTime = firstDay;
			System.Globalization.Calendar calendar = base.ParentCalendar.Calendar;
			int num = 0;
			int num2 = 0;
			if (base.ShowRowHeaders || base.EnableViewSelector)
			{
				num++;
			}
			if (base.ShowColumnHeaders || base.EnableViewSelector)
			{
				num2++;
			}
			int num3 = 0;
			int num4 = 0;
			base.GetViewRowsAndColumns(out num3, out num4);
			if (orientation == Orientation.RenderInRows)
			{
				for (int i = num2; i < num3; i++)
				{
					singleViewMatrix.Rows[i].CssClass = "rcRow";
					for (int j = num; j < num4; j++)
					{
						TableCell processedCell = singleViewMatrix.Rows[i].Cells[j];
						this.SetCalendarCell(processedCell, dateTime);
						TimeSpan timeSpan = new TimeSpan(1, 0, 0, 0);
						if (DateTime.MaxValue.Ticks > timeSpan.Ticks + dateTime.Ticks)
						{
							dateTime = base.ParentCalendar.Calendar.AddDays(dateTime, 1);
						}
						else
						{
							dateTime = DateTime.MaxValue;
						}
					}
				}
			}
			else if (orientation == Orientation.RenderInColumns)
			{
				for (int k = num; k < num4; k++)
				{
					for (int l = num2; l < num3; l++)
					{
						singleViewMatrix.Rows[l].CssClass = "rcRow";
						TableCell processedCell2 = singleViewMatrix.Rows[l].Cells[k];
						this.SetCalendarCell(processedCell2, dateTime);
						dateTime = base.ParentCalendar.Calendar.AddDays(dateTime, 1);
					}
				}
			}
			if (!base.ShowColumnHeaders)
			{
				bool enableViewSelector = base.EnableViewSelector;
			}
		}

		// Token: 0x0600A29C RID: 41628 RVA: 0x00242040 File Offset: 0x00240240
		private void RenderColumnHeader(Table singleViewMatrix, DateTime firstDay, DateTime visibleDate, Orientation orientation)
		{
			int num = 0;
			string prefixOfID = base.ID + "_cs";
			if (base.ShowRowHeaders || base.EnableViewSelector)
			{
				num++;
			}
			if (base.ShowColumnHeaders)
			{
				if (orientation == Orientation.RenderInRows)
				{
					int num2 = (int)base.ParentCalendar.Calendar.GetDayOfWeek(firstDay);
					singleViewMatrix.Rows[0].TableSection = TableRowSection.TableHeader;
					singleViewMatrix.Rows[0].CssClass = "rcWeek";
					for (int i = num; i < singleViewMatrix.Rows[0].Cells.Count; i++)
					{
						int num3 = num2 % 7;
						string dayHeaderString = this.GetDayHeaderString(num3);
						if (!string.IsNullOrEmpty(base.ColumnHeaderText) || !string.IsNullOrEmpty(base.ColumnHeaderImage))
						{
							this.SetHeaderCell(singleViewMatrix.Rows[0].Cells[i], HeaderType.Column, base.ColumnHeaderText, base.ColumnHeaderImage, Utility.SetCellID(prefixOfID, i));
						}
						else
						{
							this.SetHeaderCell(singleViewMatrix.Rows[0].Cells[i], HeaderType.Column, dayHeaderString, string.Empty, Utility.SetCellID(prefixOfID, i));
						}
						string dayName = base.ParentCalendar.DateTimeFormat.GetDayName((DayOfWeek)num3);
						singleViewMatrix.Rows[0].Cells[i].ToolTip = dayName;
						if (base.ParentCalendar.EnableAriaSupport)
						{
							singleViewMatrix.Rows[0].Cells[i].Attributes.Add("abbr", base.ParentCalendar.DateTimeFormat.AbbreviatedDayNames[num3]);
						}
						singleViewMatrix.Rows[0].Cells[i].Attributes.Add("scope", "col");
						num2++;
					}
					return;
				}
				if (orientation == Orientation.RenderInColumns)
				{
					singleViewMatrix.Rows[0].TableSection = TableRowSection.TableHeader;
					singleViewMatrix.Rows[0].CssClass = "rcWeek";
					singleViewMatrix.Rows[0].Cells[0].CssClass = "rcViewSel qwecolumn";
					this.SetHeaderCellAriaText(singleViewMatrix.Rows[0].Cells[0], "day");
					DayOfWeek firstDayOfWeek;
					if (base.ParentCalendar.FirstDayOfWeek == FirstDayOfWeek.Default)
					{
						firstDayOfWeek = DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
					}
					else
					{
						firstDayOfWeek = (DayOfWeek)base.ParentCalendar.FirstDayOfWeek;
					}
					int num4 = base.ParentCalendar.Calendar.GetWeekOfYear(firstDay, base.ParentCalendar.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
					for (int j = num; j < singleViewMatrix.Rows[0].Cells.Count; j++)
					{
						singleViewMatrix.Rows[0].Cells[j].Attributes["scope"] = "col";
						if (!string.IsNullOrEmpty(base.ColumnHeaderText) || !string.IsNullOrEmpty(base.ColumnHeaderImage))
						{
							this.SetHeaderCell(singleViewMatrix.Rows[0].Cells[j], HeaderType.Column, base.ColumnHeaderText, base.ColumnHeaderImage, Utility.SetCellID(prefixOfID, j));
						}
						else
						{
							this.SetHeaderCell(singleViewMatrix.Rows[0].Cells[j], HeaderType.Column, num4.ToString(), string.Empty, Utility.SetCellID(prefixOfID, j));
						}
						num4++;
					}
					return;
				}
			}
			else if (base.EnableViewSelector)
			{
				singleViewMatrix.Rows[0].CssClass = "rcWeek";
			}
		}

		// Token: 0x0600A29D RID: 41629 RVA: 0x002423D8 File Offset: 0x002405D8
		private void RenderRowHeader(Table singleViewMatrix, DateTime firstDay, DateTime visibleDate, Orientation orientation)
		{
			string prefixOfID = base.ID + "_rs";
			int num = 0;
			if (base.ShowColumnHeaders || base.EnableViewSelector)
			{
				num++;
			}
			if (base.ShowRowHeaders)
			{
				if (orientation == Orientation.RenderInRows)
				{
					singleViewMatrix.Rows[0].Cells[0].CssClass = "rcViewSel";
					this.SetHeaderCellAriaText(singleViewMatrix.Rows[0].Cells[0], "week");
					DayOfWeek firstDayOfWeek;
					if (base.ParentCalendar.FirstDayOfWeek == FirstDayOfWeek.Default)
					{
						if (base.ParentCalendar.DateTimeFormat != null)
						{
							firstDayOfWeek = base.ParentCalendar.DateTimeFormat.FirstDayOfWeek;
						}
						else
						{
							firstDayOfWeek = DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
						}
					}
					else
					{
						firstDayOfWeek = (DayOfWeek)base.ParentCalendar.FirstDayOfWeek;
					}
					DateTime time = firstDay;
					for (int i = num; i < singleViewMatrix.Rows.Count; i++)
					{
						int weekOfYear = base.ParentCalendar.Calendar.GetWeekOfYear(time, base.ParentCalendar.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
						TimeSpan timeSpan = new TimeSpan(base.SingleViewColumns, 0, 0, 0);
						if (DateTime.MaxValue.Ticks > timeSpan.Ticks + time.Ticks)
						{
							time = base.ParentCalendar.Calendar.AddDays(time, base.SingleViewColumns);
						}
						else
						{
							time = DateTime.MaxValue;
						}
						if (!string.IsNullOrEmpty(base.RowSelectorText) || !string.IsNullOrEmpty(base.RowHeaderImage))
						{
							this.SetHeaderCell(singleViewMatrix.Rows[i].Cells[0], HeaderType.Row, base.RowSelectorText, base.RowHeaderImage, Utility.SetCellID(prefixOfID, i));
						}
						else
						{
							this.SetHeaderCell(singleViewMatrix.Rows[i].Cells[0], HeaderType.Row, weekOfYear.ToString(), string.Empty, Utility.SetCellID(prefixOfID, i));
						}
					}
					return;
				}
				if (orientation == Orientation.RenderInColumns)
				{
					int num2 = (int)base.ParentCalendar.Calendar.GetDayOfWeek(firstDay);
					for (int j = num; j < singleViewMatrix.Rows.Count; j++)
					{
						int num3 = num2 % 7;
						string dayHeaderString = this.GetDayHeaderString(num3);
						if (!string.IsNullOrEmpty(base.RowSelectorText) || !string.IsNullOrEmpty(base.RowHeaderImage))
						{
							this.SetHeaderCell(singleViewMatrix.Rows[j].Cells[0], HeaderType.Row, base.RowSelectorText, base.RowHeaderImage, Utility.SetCellID(prefixOfID, j));
						}
						else
						{
							this.SetHeaderCell(singleViewMatrix.Rows[j].Cells[0], HeaderType.Row, dayHeaderString, string.Empty, Utility.SetCellID(prefixOfID, j));
						}
						string dayName = base.ParentCalendar.DateTimeFormat.GetDayName((DayOfWeek)num3);
						singleViewMatrix.Rows[j].Cells[0].ToolTip = dayName;
						singleViewMatrix.Rows[j].Cells[0].Attributes.Add("abbr", base.ParentCalendar.DateTimeFormat.AbbreviatedDayNames[num3]);
						singleViewMatrix.Rows[j].Cells[0].Attributes.Add("scope", "row");
						num2++;
					}
				}
			}
		}

		// Token: 0x0600A29E RID: 41630 RVA: 0x0024272B File Offset: 0x0024092B
		private void SetHeaderCellAriaText(TableCell cell, string text)
		{
			if (base.ParentCalendar.EnableAriaSupport)
			{
				cell.Text = string.Format("<span style='display:none'>{0}</span>", text);
			}
		}

		// Token: 0x0600A29F RID: 41631 RVA: 0x0024274C File Offset: 0x0024094C
		internal void SetSingleViewHeaders(Table viewMatrix, Orientation orientation)
		{
			string prefixOfID = base.ID + "_vs";
			if (base.EnableViewSelector)
			{
				this.SetHeaderCell(viewMatrix.Rows[0].Cells[0], HeaderType.View, base.ParentCalendar.ViewSelectorText, base.ParentCalendar.ViewSelectorImage, Utility.SetCellID(prefixOfID, 0));
			}
			DateTime visibleDate = this.EffectiveVisibleDate();
			DateTime firstDay = this.FirstCalendarDay(visibleDate);
			this.RenderColumnHeader(viewMatrix, firstDay, visibleDate, orientation);
			this.RenderRowHeader(viewMatrix, firstDay, visibleDate, orientation);
			TableItemStyle headerStyle = base.ParentCalendar.HeaderStyle;
			if ((base.ShowColumnHeaders && base.ShowRowHeaders) || base.EnableViewSelector)
			{
				this.SetCellCss(viewMatrix, -1, 0, headerStyle);
				this.SetCellCss(viewMatrix, 0, -1, headerStyle);
				return;
			}
			if (base.ShowColumnHeaders)
			{
				this.SetCellCss(viewMatrix, -1, 0, headerStyle);
				return;
			}
			if (base.ShowRowHeaders)
			{
				this.SetCellCss(viewMatrix, 0, -1, headerStyle);
			}
		}

		// Token: 0x0600A2A0 RID: 41632 RVA: 0x00242830 File Offset: 0x00240A30
		internal override void Initialize()
		{
			if (base.IsTopView)
			{
				if (base.IsSingleView)
				{
					base.UseRowHeadersAsSelectors = base.ParentCalendar.UseRowHeadersAsSelectors;
					base.UseColumnHeadersAsSelectors = base.ParentCalendar.UseColumnHeadersAsSelectors;
				}
				else
				{
					base.ShowColumnHeaders = false;
					base.ShowRowHeaders = false;
					base.EnableViewSelector = false;
				}
			}
			else
			{
				base.ShowColumnHeaders = base.ParentCalendar.ShowColumnHeaders;
				base.ShowRowHeaders = base.ParentCalendar.ShowRowHeaders;
				base.EnableViewSelector = base.ParentCalendar.EnableViewSelector;
				base.UseColumnHeadersAsSelectors = base.ParentCalendar.UseColumnHeadersAsSelectors;
				base.UseRowHeadersAsSelectors = base.ParentCalendar.UseRowHeadersAsSelectors;
			}
			if (!base.IsSingleView)
			{
				this.InitializeMultiViewData();
			}
			this.SetViewDateRange();
			base.Initialize();
		}

		// Token: 0x0600A2A1 RID: 41633 RVA: 0x002428F7 File Offset: 0x00240AF7
		internal void EnsureRenderSettingsMultiView()
		{
			if (!this.Equals(base.ParentCalendar.CalendarView))
			{
				throw new FormatException("Multiview mode is allowed only for top calendar views (not for their descendants).");
			}
		}

		// Token: 0x0600A2A2 RID: 41634 RVA: 0x00242918 File Offset: 0x00240B18
		internal void EnsureRenderSettingsSingleView()
		{
			int num = 42;
			string text = "<span style=\"font:normal 8pt MS Sans Serif\">";
			if (num % base.SingleViewColumns != 0 || num % base.SingleViewRows != 0)
			{
				text += "The product of (MonthColumns x MonthRows) differs from 42 which is the correct value.";
			}
			if (base.SingleViewColumns < 7 && base.ParentCalendar.ShowColumnHeaders && base.ParentCalendar.UseColumnHeadersAsSelectors && base.ParentCalendar.Orientation == Orientation.RenderInRows)
			{
				text += "The current combination of the properties: \n[ShowColumnHeaders, UseColumnHeadersAsSelectors, Orientation] and MonthColumns < 7 does not allow proper rendering of Telerik RadCalendar. Please correct.";
			}
			if (base.SingleViewRows < 7 && base.ParentCalendar.ShowRowHeaders && base.ParentCalendar.UseRowHeadersAsSelectors && base.ParentCalendar.Orientation == Orientation.RenderInColumns)
			{
				text += "The current combination of the properties: \n[ShowRowHeaders, UseRowHeadersAsSelectors, Orientation] and MonthRows < 7 does not allow proper rendering of Telerik RadCalendar. Please correct.";
			}
			text += "</span>";
			base.ConditionsErrorMessage = text;
		}

		// Token: 0x0600A2A3 RID: 41635 RVA: 0x002429DB File Offset: 0x00240BDB
		public override void EnsureRenderSettings()
		{
			if (base.IsSingleView)
			{
				this.EnsureRenderSettingsSingleView();
				return;
			}
			this.EnsureRenderSettingsMultiView();
		}

		// Token: 0x0600A2A4 RID: 41636 RVA: 0x002429F4 File Offset: 0x00240BF4
		internal override Table GetCalendarViewStructure()
		{
			Table table = null;
			if (base.IsSingleView)
			{
				if (base.ParentCalendar.MultiViewColumns > 1)
				{
					base.ParentCalendar.OnChildViewRender(this);
				}
				table = this.CreateSingleViewMatrix();
			}
			else
			{
				table = this.CreateMultiViewMatrix();
			}
			int num = 0;
			bool flag = false;
			foreach (object obj in table.Rows)
			{
				TableRow tableRow = (TableRow)obj;
				num++;
				if (tableRow.TableSection == TableRowSection.TableHeader && num > 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				foreach (object obj2 in table.Rows)
				{
					TableRow tableRow2 = (TableRow)obj2;
					if (tableRow2.TableSection == TableRowSection.TableHeader)
					{
						break;
					}
					if (tableRow2.TableSection == TableRowSection.TableBody)
					{
						tableRow2.TableSection = TableRowSection.TableHeader;
					}
				}
			}
			return table;
		}

		// Token: 0x0600A2A5 RID: 41637 RVA: 0x00242B04 File Offset: 0x00240D04
		internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			Table table = null;
			if (base.IsSingleView)
			{
				if (base.ParentCalendar.MultiViewColumns > 1)
				{
					base.ParentCalendar.OnChildViewRender(this);
				}
				table = this.CreateSingleViewMatrix();
			}
			else
			{
				table = this.CreateMultiViewMatrix();
			}
			int num = 0;
			bool flag = false;
			foreach (object obj in table.Rows)
			{
				TableRow tableRow = (TableRow)obj;
				num++;
				if (tableRow.TableSection == TableRowSection.TableHeader && num > 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				foreach (object obj2 in table.Rows)
				{
					TableRow tableRow2 = (TableRow)obj2;
					if (tableRow2.TableSection == TableRowSection.TableHeader)
					{
						break;
					}
					if (tableRow2.TableSection == TableRowSection.TableBody)
					{
						tableRow2.TableSection = TableRowSection.TableHeader;
					}
				}
			}
			table.RenderControl(writer);
		}

		// Token: 0x0600A2A6 RID: 41638 RVA: 0x00242C20 File Offset: 0x00240E20
		internal Table CreateBaseTable(int rows, int columns)
		{
			Table table = new Table();
			int i = 0;
			if (base.ShowColumnHeaders || base.EnableViewSelector)
			{
				TableHeaderRow tableHeaderRow = new TableHeaderRow();
				tableHeaderRow.TableSection = TableRowSection.TableHeader;
				for (int j = 0; j < columns; j++)
				{
					TableHeaderCell tableHeaderCell = new TableHeaderCell();
					tableHeaderCell.Attributes["scope"] = "col";
					if (string.IsNullOrEmpty(base.ParentCalendar.CalendarCaption))
					{
						Label label = new Label();
						label.Text = "Week";
						label.Style.Add("display", "none");
						tableHeaderCell.Controls.Add(label);
					}
					else
					{
						tableHeaderCell.Text = "&nbsp;";
					}
					tableHeaderRow.Cells.Add(tableHeaderCell);
				}
				if (!base.IsSingleView)
				{
					tableHeaderRow.Style["vertical-align"] = "top";
				}
				table.Rows.Add(tableHeaderRow);
				i++;
			}
			while (i < rows)
			{
				TableRow tableRow = new TableRow();
				for (int k = 0; k < columns; k++)
				{
					TableCell tableCell = (k == 0 && base.IsSingleView && base.ParentCalendar.ShowRowHeaders) ? new TableHeaderCell() : new TableCell();
					if (k == 0 && base.IsSingleView && base.ParentCalendar.ShowRowHeaders)
					{
						tableCell.Attributes["scope"] = "row";
					}
					if (!base.IsSingleView)
					{
						string text = (i == 0) ? " rcFirstRow" : "";
						string text2 = (i == rows - 1) ? " rcLastRow" : "";
						string text3 = (k == 0) ? " rcFirstCol" : "";
						string text4 = (k == columns - 1) ? " rcLastCol" : "";
						tableCell.CssClass = string.Format("rcCalendar{0}{1}{2}{3}", new object[]
						{
							text,
							text3,
							text2,
							text4
						});
					}
					tableCell.Text = "&nbsp;";
					tableRow.Cells.Add(tableCell);
				}
				table.Rows.Add(tableRow);
				i++;
			}
			return table;
		}

		// Token: 0x0600A2A7 RID: 41639 RVA: 0x00242E4C File Offset: 0x0024104C
		internal void SetCellAttributes(Table viewMatrix, int rows, int columns, string attribute, string attributeValue)
		{
			if (rows > 0 && columns > 0)
			{
				for (int i = 0; i < rows; i++)
				{
					TableRow tableRow = viewMatrix.Rows[i];
					for (int j = 0; j < columns; j++)
					{
						tableRow.Cells[j].Attributes.Add(attribute, attributeValue);
					}
				}
				return;
			}
			if (rows > 0)
			{
				TableRow tableRow2 = viewMatrix.Rows[rows];
				for (int k = 0; k < columns; k++)
				{
					tableRow2.Cells[k].Attributes.Add(attribute, attributeValue);
				}
				return;
			}
			if (columns > 0)
			{
				for (int l = 0; l < rows; l++)
				{
					TableRow tableRow3 = viewMatrix.Rows[l];
					tableRow3.Cells[columns].Attributes.Add(attribute, attributeValue);
				}
			}
		}

		// Token: 0x0600A2A8 RID: 41640 RVA: 0x00242F1C File Offset: 0x0024111C
		internal void SetCellCss(Table viewMatrix, int rows, int columns, TableItemStyle cellStyle)
		{
			int num = 0;
			int num2 = 0;
			if (base.IsSingleView)
			{
				base.GetViewRowsAndColumns(out num, out num2);
			}
			else
			{
				base.GetViewRowsAndColumns(out num, out num2);
			}
			if (columns >= 0)
			{
				for (int i = 0; i < num2; i++)
				{
					TableItemStyle tableItemStyle = new TableItemStyle();
					if (i == 0 && viewMatrix.Rows[0].Cells[i].Text == base.ParentCalendar.ViewSelectorText)
					{
						tableItemStyle.CopyFrom(base.ParentCalendar.ViewSelectorStyle);
					}
					else
					{
						tableItemStyle.CopyFrom(cellStyle);
					}
					if (viewMatrix.Rows[0].Cells[i].CssClass != tableItemStyle.CssClass && tableItemStyle.CssClass.IndexOf(" ") != -1)
					{
						tableItemStyle.CssClass = tableItemStyle.CssClass + " " + viewMatrix.Rows[0].Cells[i].CssClass;
					}
					viewMatrix.Rows[0].Cells[i].ApplyStyle(tableItemStyle);
				}
				return;
			}
			if (rows >= 0)
			{
				for (int j = 0; j < num; j++)
				{
					TableItemStyle tableItemStyle2 = new TableItemStyle();
					tableItemStyle2.CopyFrom(cellStyle);
					if (viewMatrix.Rows[j].Cells[0].CssClass != tableItemStyle2.CssClass && tableItemStyle2.CssClass.IndexOf(" ") != -1)
					{
						tableItemStyle2.CssClass = tableItemStyle2.CssClass + " " + viewMatrix.Rows[j].Cells[0].CssClass;
					}
					viewMatrix.Rows[j].Cells[0].ApplyStyle(tableItemStyle2);
				}
			}
		}

		// Token: 0x0600A2A9 RID: 41641 RVA: 0x00243104 File Offset: 0x00241304
		internal void SetHeaderCell(TableCell cell, HeaderType type, string headerText, string headerImagePath, string cellID)
		{
			if (!string.IsNullOrEmpty(headerImagePath))
			{
				string src = headerImagePath;
				if (!VirtualPathUtility.IsAbsolute(headerImagePath) && VirtualPathUtility.IsAppRelative(headerImagePath))
				{
					src = VirtualPathUtility.ToAbsolute(headerImagePath);
				}
				HtmlImage htmlImage = new HtmlImage();
				htmlImage.Src = src;
				htmlImage.Attributes["alt"] = headerText;
				cell.Controls.Add(htmlImage);
			}
			else if (!string.IsNullOrEmpty(headerText))
			{
				cell.Text = headerText;
			}
			else
			{
				cell.Text = base.ParentCalendar.ViewSelectorText;
			}
			if (!string.IsNullOrEmpty(cellID))
			{
				cell.ID = cellID;
			}
			base.ParentCalendar.OnHeaderCellRender(cell, type);
		}

		// Token: 0x0600A2AA RID: 41642 RVA: 0x002431A4 File Offset: 0x002413A4
		private void SetCalendarCell(TableCell processedCell, DateTime processedDate)
		{
			CultureInfo cultureInfo = base.ParentCalendar.CultureInfo;
			processedCell.Text = HttpUtility.HtmlEncode(processedDate.ToString(base.ParentCalendar.CellDayFormat, cultureInfo));
			bool flag = base.ParentCalendar.SelectedDates.Contains(new RadDate(processedDate));
			int dayOfWeek = (int)base.ParentCalendar.Calendar.GetDayOfWeek(processedDate);
			bool flag2 = dayOfWeek == 0 || dayOfWeek == 6;
			bool flag3 = processedDate == DateTime.Today;
			bool flag4 = processedDate < base.ParentCalendar.RangeMinDate || processedDate > base.ParentCalendar.RangeMaxDate;
			bool flag5 = processedDate < this.MonthStartDate || this.MonthEndDate < processedDate;
			TableItemStyle tableItemStyle;
			if (!base.ParentCalendar.Enabled)
			{
				tableItemStyle = base.ParentCalendar.DisabledDayStyle;
			}
			else if (flag4)
			{
				tableItemStyle = base.ParentCalendar.OutOfRangeDayStyle;
			}
			else if (flag && this.ShouldApplyStyle(processedDate))
			{
				tableItemStyle = base.ParentCalendar.SelectedDayStyle;
			}
			else if (flag5)
			{
				tableItemStyle = base.ParentCalendar.OtherMonthDayStyle;
			}
			else if (flag2)
			{
				tableItemStyle = base.ParentCalendar.WeekendDayStyle;
			}
			else
			{
				tableItemStyle = base.ParentCalendar.DayStyle;
			}
			processedCell.ApplyStyle(tableItemStyle);
			if (!flag4)
			{
				this.ProcessCalendarDays(processedCell, processedDate);
			}
			RadCalendarDay specialDay = this.GetSpecialDay(processedDate);
			bool flag6 = specialDay != null && !string.IsNullOrEmpty(specialDay.TemplateID);
			bool flag7 = specialDay == null || !specialDay.IsDisabled || specialDay.IsSelectable;
			if (flag6)
			{
				processedCell.Text = string.Concat(new string[]
				{
					"<div class=\"radTemplateDay_",
					base.ParentCalendar.Skin,
					"\" >",
					processedCell.Text,
					"</div>"
				});
			}
			else if (!flag4 && (!flag5 || base.ParentCalendar.ShowOtherMonthsDays) && flag7)
			{
				processedCell.Text = "<a href=\"#\">" + processedCell.Text + "</a>";
			}
			else
			{
				processedCell.Text = "<span>" + processedCell.Text + "</span>";
			}
			if (base.IsSingleView && !base.ParentCalendar.ShowOtherMonthsDays && flag5)
			{
				processedCell.Text = "&#160;";
			}
			RadCalendarDay radCalendarDay = new RadCalendarDay(base.ParentCalendar);
			radCalendarDay.Date = processedDate;
			if (flag3)
			{
				radCalendarDay.IsToday = true;
			}
			if (flag2)
			{
				radCalendarDay.IsWeekend = true;
			}
			if (!string.IsNullOrEmpty(processedCell.ID))
			{
				radCalendarDay.IsSelectable = true;
			}
			if (base.ParentCalendar.SelectedDates.Contains(new RadDate(processedDate)))
			{
				radCalendarDay.IsSelected = true;
			}
			base.ParentCalendar.OnDayRender(processedCell, radCalendarDay, this);
			if (!string.IsNullOrEmpty(processedCell.Style.Value) || processedCell.CssClass != tableItemStyle.CssClass)
			{
				base.ParentCalendar.AddDayRenderChangedDay(processedDate.ToString("yyyy_M_d"), string.Format("[\"{0}\",\"{1}\"]", processedCell.Style.Value, processedCell.CssClass));
				processedCell.CssClass = base.ParentCalendar.FormatCssClass(tableItemStyle.CssClass.Split(new char[]
				{
					'_'
				})[0], processedCell.CssClass);
				if (!base.ParentCalendar.Enabled || flag || flag4)
				{
					processedCell.Style.Value = "";
					processedCell.CssClass = tableItemStyle.CssClass;
				}
			}
		}

		// Token: 0x0600A2AB RID: 41643 RVA: 0x0024351E File Offset: 0x0024171E
		private bool ShouldApplyStyle(DateTime processedDate)
		{
			return base.ParentCalendar.ShowOtherMonthsDays || (processedDate >= this.MonthStartDate && processedDate <= this.MonthEndDate);
		}

		// Token: 0x0600A2AC RID: 41644 RVA: 0x0024354C File Offset: 0x0024174C
		private string GetDayHeaderString(int weekDay)
		{
			DateTimeFormatInfo dateTimeFormat = base.ParentCalendar.DateTimeFormat;
			DayNameFormat dayNameFormat = base.ParentCalendar.DayNameFormat;
			string result = string.Empty;
			switch (dayNameFormat)
			{
			case DayNameFormat.Full:
				result = dateTimeFormat.GetDayName((DayOfWeek)weekDay);
				break;
			case DayNameFormat.Short:
				result = dateTimeFormat.GetAbbreviatedDayName((DayOfWeek)weekDay);
				break;
			case DayNameFormat.FirstLetter:
			{
				string str = dateTimeFormat.ShortestDayNames[weekDay];
				TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator(str);
				textElementEnumerator.MoveNext();
				result = textElementEnumerator.Current.ToString();
				break;
			}
			case DayNameFormat.FirstTwoLetters:
			{
				string str2 = dateTimeFormat.ShortestDayNames[weekDay];
				TextElementEnumerator textElementEnumerator2 = StringInfo.GetTextElementEnumerator(str2);
				textElementEnumerator2.MoveNext();
				StringBuilder stringBuilder = new StringBuilder(textElementEnumerator2.Current.ToString());
				if (textElementEnumerator2.MoveNext())
				{
					stringBuilder.Append(textElementEnumerator2.Current.ToString());
				}
				result = stringBuilder.ToString();
				break;
			}
			default:
				result = dateTimeFormat.ShortestDayNames[weekDay];
				break;
			}
			return result;
		}

		// Token: 0x0600A2AD RID: 41645 RVA: 0x00243634 File Offset: 0x00241834
		internal override void ProcessCalendarDays(TableCell processedCell, DateTime processedDate)
		{
			RadCalendarDay specialDay = this.GetSpecialDay(processedDate);
			if (specialDay != null && this.ShouldApplyStyle(processedDate))
			{
				this.SetCellStyle(specialDay, processedDate, processedCell);
				this.ProcessSpecialDayTemplate(specialDay, processedDate, processedCell);
			}
			if (this.ShouldApplyStyle(processedDate))
			{
				this.SetCellTitle(specialDay, processedCell, processedDate);
			}
			base.ProcessCalendarDays(processedCell, processedDate);
		}

		// Token: 0x0600A2AE RID: 41646 RVA: 0x00243684 File Offset: 0x00241884
		private RadCalendarDay GetSpecialDay(DateTime processedDate)
		{
			RadCalendarDay radCalendarDay = base.ParentCalendar.SpecialDays[processedDate];
			if (radCalendarDay == null)
			{
				for (int i = 0; i < base.ParentCalendar.SpecialDays.Count; i++)
				{
					RecurringEvents recurringEvents = base.ParentCalendar.SpecialDays[i].IsRecurring(processedDate, base.ParentCalendar);
					if (recurringEvents != RecurringEvents.None)
					{
						radCalendarDay = base.ParentCalendar.SpecialDays[i];
						base.ParentCalendar.AddViewRepeatableDay(processedDate.ToString("yyyy_M_d", base.ParentCalendar.CultureInfo), radCalendarDay.Date.ToString("yyyy_M_d", base.ParentCalendar.CultureInfo));
						break;
					}
				}
			}
			return radCalendarDay;
		}

		// Token: 0x0600A2AF RID: 41647 RVA: 0x00243754 File Offset: 0x00241954
		private void SetCellStyle(RadCalendarDay calendarDay, DateTime processedDate, TableCell processedCell)
		{
			bool flag = base.ParentCalendar.SelectedDates.Contains(new RadDate(processedDate));
			TableItemStyle s;
			if (!base.ParentCalendar.Enabled)
			{
				s = base.ParentCalendar.DisabledDayStyle;
			}
			else if (flag)
			{
				s = base.ParentCalendar.SelectedDayStyle;
			}
			else if (calendarDay.IsDisabled)
			{
				s = base.ParentCalendar.DisabledDayStyle;
			}
			else
			{
				s = calendarDay.ItemStyle;
			}
			processedCell.ApplyStyle(s);
			if (!string.IsNullOrEmpty(calendarDay.ItemStyle.CssClass))
			{
				processedCell.CssClass += string.Format(" {0}", calendarDay.ItemStyle.CssClass);
			}
		}

		// Token: 0x0600A2B0 RID: 41648 RVA: 0x00243800 File Offset: 0x00241A00
		private void ProcessSpecialDayTemplate(RadCalendarDay calendarDay, DateTime processedDate, TableCell processedCell)
		{
			if (!string.IsNullOrEmpty(calendarDay.TemplateID))
			{
				string id = string.Empty;
				if (calendarDay.IsRecurring(processedDate, base.ParentCalendar) != RecurringEvents.Today)
				{
					id = Utility.SetCellID("dt", calendarDay.Date);
				}
				else
				{
					id = Utility.SetCellID("dt", DateTime.Today);
				}
				Control control = base.ParentCalendar.FindControl(id);
				if (control != null)
				{
					control.Visible = true;
					StringBuilder stringBuilder = new StringBuilder();
					control.RenderControl(CalendarRenderer.CreateHtmlWriter(stringBuilder));
					processedCell.Text = stringBuilder.ToString();
					control.Visible = false;
				}
			}
		}

		// Token: 0x0600A2B1 RID: 41649 RVA: 0x00243890 File Offset: 0x00241A90
		private void SetCellTitle(RadCalendarDay calendarDay, TableCell processedCell, DateTime processedDate)
		{
			string value = string.Empty;
			if (calendarDay != null && !string.IsNullOrEmpty(calendarDay.ToolTip))
			{
				value = calendarDay.ToolTip;
			}
			else if (!string.IsNullOrEmpty(base.ParentCalendar.DayCellToolTipFormat))
			{
				value = processedDate.ToString(base.ParentCalendar.DayCellToolTipFormat, base.ParentCalendar.CultureInfo);
			}
			if (base.ParentCalendar.ShowDayCellToolTips)
			{
				processedCell.Attributes["title"] = value;
			}
		}

		// Token: 0x0600A2B2 RID: 41650 RVA: 0x0024390C File Offset: 0x00241B0C
		internal override CalendarView GetPreviousView()
		{
			DateTime previousViewDate = this.GetPreviousViewDate();
			return this.CreateViewForDate(previousViewDate);
		}

		// Token: 0x0600A2B3 RID: 41651 RVA: 0x00243928 File Offset: 0x00241B28
		internal override CalendarView GetNextView()
		{
			DateTime nextViewDate = this.GetNextViewDate();
			return this.CreateViewForDate(nextViewDate);
		}

		// Token: 0x0600A2B4 RID: 41652 RVA: 0x00243944 File Offset: 0x00241B44
		private DateTime GetPreviousViewDate()
		{
			DateTime startOfThisMonth = this.StartOfThisMonth;
			return this.AddViewPeriods(startOfThisMonth, -1);
		}

		// Token: 0x0600A2B5 RID: 41653 RVA: 0x00243964 File Offset: 0x00241B64
		private DateTime GetNextViewDate()
		{
			DateTime startOfThisMonth = this.StartOfThisMonth;
			return this.AddViewPeriods(startOfThisMonth, 1);
		}

		// Token: 0x0600A2B6 RID: 41654 RVA: 0x00243982 File Offset: 0x00241B82
		private DateTime AddViewPeriods(DateTime newViewDate, int periods)
		{
			return base.ParentCalendar.Calendar.AddMonths(newViewDate, periods * base.MonthsInView);
		}

		// Token: 0x17003375 RID: 13173
		// (get) Token: 0x0600A2B7 RID: 41655 RVA: 0x002439A0 File Offset: 0x00241BA0
		internal DateTime StartOfThisMonth
		{
			get
			{
				DateTime monthStartDate;
				if (base.IsSingleView)
				{
					monthStartDate = this.MonthStartDate;
				}
				else
				{
					monthStartDate = ((MonthView)base.ChildViews[0]).MonthStartDate;
				}
				return monthStartDate;
			}
		}

		// Token: 0x0600A2B8 RID: 41656 RVA: 0x002439DC File Offset: 0x00241BDC
		internal CalendarView CreateViewForDate(DateTime newViewDate)
		{
			MonthView monthView = new MonthView(base.ParentCalendar, newViewDate);
			if (base.IsTopView)
			{
				CalendarView calendarView = base.ParentCalendar.CalendarView;
				base.ParentCalendar.SetCalendarView(monthView);
				monthView.Initialize();
				monthView.SetColumns(base.MultiViewColumns);
				monthView.SetRows(base.MultiViewRows);
				base.ParentCalendar.SetCalendarView(calendarView);
			}
			else
			{
				monthView.Initialize();
			}
			return monthView;
		}

		// Token: 0x0600A2B9 RID: 41657 RVA: 0x00243A4C File Offset: 0x00241C4C
		internal override CalendarView GetPreviousView(int months)
		{
			DateTime dateTime = this.StartOfThisMonth.AddMonths(-months);
			if (dateTime < base.ParentCalendar.RangeMinDate)
			{
				return this.CreateViewForDate(base.ParentCalendar.RangeMinDate);
			}
			return this.CreateViewForDate(dateTime);
		}

		// Token: 0x0600A2BA RID: 41658 RVA: 0x00243A98 File Offset: 0x00241C98
		internal override CalendarView GetNextView(int months)
		{
			DateTime dateTime = this.StartOfThisMonth.AddMonths(months);
			if (dateTime > base.ParentCalendar.RangeMaxDate)
			{
				return this.CreateViewForDate(base.ParentCalendar.RangeMaxDate);
			}
			return this.CreateViewForDate(dateTime);
		}

		// Token: 0x17003376 RID: 13174
		// (get) Token: 0x0600A2BB RID: 41659 RVA: 0x00243AE1 File Offset: 0x00241CE1
		public DateTime MonthStartDate
		{
			get
			{
				return this._MonthStartDate;
			}
		}

		// Token: 0x17003377 RID: 13175
		// (get) Token: 0x0600A2BC RID: 41660 RVA: 0x00243AE9 File Offset: 0x00241CE9
		public DateTime MonthEndDate
		{
			get
			{
				return this._MonthEndDate;
			}
		}

		// Token: 0x0600A2BD RID: 41661 RVA: 0x00243AF4 File Offset: 0x00241CF4
		protected override void SetViewDateRange()
		{
			if (base.IsSingleView)
			{
				this._MonthStartDate = this.EffectiveVisibleDate();
				this._MonthDays = base.ParentCalendar.Calendar.GetDaysInMonth(this._MonthStartDate.Year, this._MonthStartDate.Month);
				this._MonthEndDate = base.ParentCalendar.Calendar.AddDays(this._MonthStartDate, this._MonthDays - 1);
				base.ViewStartDate = this.FirstCalendarDay(this._MonthStartDate);
				int num = 0;
				int num2 = 0;
				base.GetContentRowsAndColumns(out num, out num2);
				TimeSpan timeSpan = new TimeSpan(num * num2 - 1, 0, 0, 0);
				if (DateTime.MaxValue.Ticks > timeSpan.Ticks + base.ViewStartDate.Ticks)
				{
					base.ViewEndDate = base.ParentCalendar.Calendar.AddDays(base.ViewStartDate, num * num2 - 1);
					return;
				}
				base.ViewEndDate = DateTime.MaxValue;
				return;
			}
			else
			{
				if (base.ChildViews[0] is MonthView)
				{
					base.ViewStartDate = ((MonthView)base.ChildViews[0]).MonthStartDate;
				}
				else
				{
					base.ViewStartDate = base.ChildViews[0].ViewStartDate;
				}
				if (base.ChildViews[base.ChildViews.Count - 1] is MonthView)
				{
					base.ViewEndDate = ((MonthView)base.ChildViews[base.ChildViews.Count - 1]).MonthEndDate;
					return;
				}
				base.ViewEndDate = base.ChildViews[base.ChildViews.Count - 1].ViewEndDate;
				return;
			}
		}

		// Token: 0x0600A2BE RID: 41662 RVA: 0x00243CBC File Offset: 0x00241EBC
		private DateTime EffectiveVisibleDate()
		{
			DateTime time;
			if (this._ViewInMonthDate != DateTime.MinValue)
			{
				time = this._ViewInMonthDate;
			}
			else if (base.ParentCalendar.IsDesignMode && base.ParentCalendar.FocusedDate == new DateTime(1980, 1, 1))
			{
				time = DateTime.Today;
			}
			else
			{
				time = base.ParentCalendar.FocusedDate;
			}
			return base.ParentCalendar.Calendar.AddDays(time, -(base.ParentCalendar.Calendar.GetDayOfMonth(time) - 1));
		}

		// Token: 0x0600A2BF RID: 41663 RVA: 0x00243D48 File Offset: 0x00241F48
		private DateTime FirstCalendarDay(DateTime visibleDate)
		{
			int num = base.ParentCalendar.Calendar.GetDayOfWeek(visibleDate) - (DayOfWeek)this.NumericFirstDayOfWeek();
			if (num <= 0)
			{
				num += 7;
			}
			return base.ParentCalendar.Calendar.AddDays(visibleDate, -num);
		}

		// Token: 0x0600A2C0 RID: 41664 RVA: 0x00243D8B File Offset: 0x00241F8B
		private string GetMonthName(int m, bool bFull)
		{
			if (bFull)
			{
				return base.ParentCalendar.DateTimeFormat.GetMonthName(m);
			}
			return base.ParentCalendar.DateTimeFormat.GetAbbreviatedMonthName(m);
		}

		// Token: 0x0600A2C1 RID: 41665 RVA: 0x00243DB3 File Offset: 0x00241FB3
		private int NumericFirstDayOfWeek()
		{
			if (base.ParentCalendar.FirstDayOfWeek != FirstDayOfWeek.Default)
			{
				return (int)base.ParentCalendar.FirstDayOfWeek;
			}
			return (int)base.ParentCalendar.DateTimeFormat.FirstDayOfWeek;
		}

		// Token: 0x17003378 RID: 13176
		// (get) Token: 0x0600A2C2 RID: 41666 RVA: 0x00243DDF File Offset: 0x00241FDF
		// (set) Token: 0x0600A2C3 RID: 41667 RVA: 0x00243DE7 File Offset: 0x00241FE7
		[Localizable(true)]
		public string TitleFormat
		{
			get
			{
				return this.titleFormat;
			}
			set
			{
				this.titleFormat = value;
			}
		}

		// Token: 0x0600A2C4 RID: 41668 RVA: 0x00243DF0 File Offset: 0x00241FF0
		public override DateTime GetEffectiveVisibleDate()
		{
			return this.EffectiveVisibleDate();
		}

		// Token: 0x04002D44 RID: 11588
		private DateTime _MonthStartDate = DateTime.MinValue;

		// Token: 0x04002D45 RID: 11589
		private DateTime _MonthEndDate = DateTime.MinValue;

		// Token: 0x04002D46 RID: 11590
		private int _MonthDays;

		// Token: 0x04002D47 RID: 11591
		private DateTime _ViewInMonthDate = DateTime.MinValue;

		// Token: 0x04002D48 RID: 11592
		private string titleFormat = "MMMM";
	}
}
