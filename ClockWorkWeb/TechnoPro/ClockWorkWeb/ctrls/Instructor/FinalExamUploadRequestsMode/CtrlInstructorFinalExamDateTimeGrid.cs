using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ClockWorkController;
using ClockWorkLogger;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity.Instructor.FinalExamRequest;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Instructor.FinalExamUploadRequestsMode
{
	// Token: 0x0200014A RID: 330
	public class CtrlInstructorFinalExamDateTimeGrid : UserControl
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x00046554 File Offset: 0x00044754
		[WebMethod]
		public static CourseExamSelection LoadCourseExamPreviousSelectionByLucid(int lucid)
		{
			return CtrlInstructorFinalExamDateTimeGrid.LoadCourseExamPreviousSelection(2463);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00046570 File Offset: 0x00044770
		private static IList<DateTime> ParseDateTimesFromDescription(string description, int year)
		{
			bool flag = string.IsNullOrEmpty(description);
			IList<DateTime> result;
			if (flag)
			{
				result = new List<DateTime>();
			}
			else
			{
				result = (from g in description.Replace("<br />", "`").Split(new char[]
				{
					'`'
				})
				select g.Trim() into h
				where h.Length > 0
				select h into q
				select CtrlInstructorFinalExamDateTimeGrid.ParseDateTimeFromDescriptionItem(q, year) into n
				where n != null
				select n into nn
				select nn.Value).ToList<DateTime>();
			}
			return result;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0004666C File Offset: 0x0004486C
		private static DateTime? ParseDateTimeFromDescriptionItem(string s, int year)
		{
			bool flag = string.IsNullOrEmpty(s);
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = s.Length < 3;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string s2 = s.Substring(2).Replace(".", " " + year.ToString() + " ");
					DateTime value;
					result = ((!DateTime.TryParse(s2, out value)) ? null : new DateTime?(value));
				}
			}
			return result;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000466F8 File Offset: 0x000448F8
		private static CourseExamSelection LoadCourseExamPreviousSelection(int examid)
		{
			DataTable dataTable = ClockWorkController.Appointment.LoadInstructorTestInfo(examid);
			bool flag = dataTable.Rows.Count <= 0;
			CourseExamSelection result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DataRow dataRow = dataTable.Rows[0];
				DateTime testDate = (DateTime)dataRow["dateoftest"];
				IList<DateTime> list = CtrlInstructorFinalExamDateTimeGrid.ParseDateTimesFromDescription(HttpUtility.HtmlDecode(dataRow["description"].ToString()), testDate.Year);
				bool flag2 = list.Count != 3;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new CourseExamSelection
					{
						ExamId = examid,
						TestDate = testDate,
						DateTime1ValueString = CtrlInstructorFinalExamDateTimeGrid.AvailabilityToString(list[0], 1),
						DateTime2ValueString = CtrlInstructorFinalExamDateTimeGrid.AvailabilityToString(list[1], 2),
						DateTime3ValueString = CtrlInstructorFinalExamDateTimeGrid.AvailabilityToString(list[2], 3)
					};
				}
			}
			return result;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000467DC File Offset: 0x000449DC
		public List<FinalExamDay> GetSelectedExamDates()
		{
			List<FinalExamDay> list = new List<FinalExamDay>();
			int num = this.t_dates.Rows.Count - 1;
			int num2 = this.t_dates.Rows[0].Cells.Count - 1;
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					TableCell tableCell = this.t_dates.Rows[i + 1].Cells[j + 1];
					bool flag = tableCell.Controls.Count <= 0;
					if (!flag)
					{
						RadComboBox radComboBox = (RadComboBox)tableCell.Controls[0];
						int selectedIndex = radComboBox.SelectedIndex;
						bool flag2 = selectedIndex <= 0;
						if (!flag2)
						{
							DateTime? dateTime = CtrlInstructorFinalExamDateTimeGrid.ParseAvailability(radComboBox.SelectedValue);
							bool flag3 = dateTime == null;
							if (!flag3)
							{
								FinalExamDay item = new FinalExamDay(selectedIndex, dateTime.Value);
								list.Add(item);
							}
						}
					}
				}
			}
			list.Sort((FinalExamDay f1, FinalExamDay f2) => f1.Level.CompareTo(f2.Level));
			for (int k = 0; k < list.Count; k++)
			{
				list[k].Level = k + 1;
			}
			return list;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00046958 File Offset: 0x00044B58
		private void SetupTable()
		{
			List<TimeSpan> dateChooserTimes = this.GetDateChooserTimes();
			List<DateTime> dateChooserDates = this.GetDateChooserDates();
			List<DateTime> dateChooserClosedDateTimes = this.GetDateChooserClosedDateTimes();
			for (int i = 0; i < dateChooserDates.Count; i++)
			{
				DateTime dateTime = dateChooserDates[i];
				TableCell cell = new TableCell
				{
					ID = "t_dates_cell_" + i.ToString(),
					CssClass = "aspTableHeaderCell",
					Text = "<b>" + dateTime.ToString("ddd MM/dd").ToUpper() + "</b>",
					HorizontalAlign = HorizontalAlign.Center
				};
				this.t_dates_header.Cells.Add(cell);
			}
			DateTime date = DateTime.Now.Date;
			for (int j = 0; j < dateChooserTimes.Count; j++)
			{
				string text = j.ToString();
				TableRow tableRow = new TableRow
				{
					ID = "t_dates_row_" + text
				};
				this.t_dates.Rows.Add(tableRow);
				DateTime dateTime2 = date.Add(dateChooserTimes[j]);
				TableCell cell2 = new TableCell
				{
					ID = string.Format("t_dates_row_{0}_cell_time", text),
					HorizontalAlign = HorizontalAlign.Right,
					Text = dateTime2.ToString("h:mm tt"),
					CssClass = "aspTableHeaderCell"
				};
				tableRow.Cells.Add(cell2);
				for (int k = 0; k < dateChooserDates.Count; k++)
				{
					string arg = k.ToString();
					TableCell tableCell = new TableCell
					{
						ID = string.Format("t_dates_row_{0}_cell_{1}", text, arg),
						HorizontalAlign = HorizontalAlign.Center
					};
					DateTime dateTime3 = dateChooserDates[k].Date.Add(dateChooserTimes[j]);
					bool flag = dateChooserClosedDateTimes.Contains(dateTime3);
					if (flag)
					{
						tableCell.Text = "CLOSED";
						tableCell.BackColor = Color.Black;
						tableCell.ForeColor = Color.Yellow;
					}
					else
					{
						RadComboBox radComboBox = new RadComboBox
						{
							Width = 60,
							OnClientDropDownOpening = "OnClientDropDownOpening"
						};
						radComboBox.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
						radComboBox.Items.Add(new RadComboBoxItem("", CtrlInstructorFinalExamDateTimeGrid.AvailabilityToString(dateTime3, 0)));
						radComboBox.Items.Add(new RadComboBoxItem("1", CtrlInstructorFinalExamDateTimeGrid.AvailabilityToString(dateTime3, 1)));
						radComboBox.Items.Add(new RadComboBoxItem("2", CtrlInstructorFinalExamDateTimeGrid.AvailabilityToString(dateTime3, 2)));
						radComboBox.Items.Add(new RadComboBoxItem("3", CtrlInstructorFinalExamDateTimeGrid.AvailabilityToString(dateTime3, 3)));
						tableCell.Controls.Add(radComboBox);
					}
					tableRow.Cells.Add(tableCell);
				}
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00046C5C File Offset: 0x00044E5C
		private void LoadChooserDatesAndTimes()
		{
			bool flag = this._chooserDates != null;
			if (!flag)
			{
				WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_ExamRequestRules);
				bool flag2 = !string.IsNullOrEmpty(settingValue);
				if (flag2)
				{
					try
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(settingValue);
						XmlNode xmlNode = xmlDocument.SelectSingleNode("//examrequestrules/dates");
						this._chooserDates = ((xmlNode == null) ? new List<DateTime>() : CtrlInstructorFinalExamDateTimeGrid.ParseDates(xmlNode.InnerText));
						xmlNode = xmlDocument.SelectSingleNode("//examrequestrules/closeddates");
						this._chooserClosedDates = ((xmlNode == null) ? new List<DateTime>() : CtrlInstructorFinalExamDateTimeGrid.ParseDates(xmlNode.InnerText));
						xmlNode = xmlDocument.SelectSingleNode("//examrequestrules/times");
						this._chooserTimes = ((xmlNode == null) ? new List<TimeSpan>() : CtrlInstructorFinalExamDateTimeGrid.ParseTimes(xmlNode.InnerText));
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("FinalExamUpload.aspx.cs:LoadChooserDatesAndTimes:Error={0}", ex.ToString());
					}
				}
				bool flag3 = this._chooserDates != null;
				if (!flag3)
				{
					this._chooserDates = new List<DateTime>();
					this._chooserClosedDates = new List<DateTime>();
					this._chooserTimes = new List<TimeSpan>();
				}
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00046D94 File Offset: 0x00044F94
		private List<DateTime> GetDateChooserDates()
		{
			this.LoadChooserDatesAndTimes();
			return this._chooserDates;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00046DB4 File Offset: 0x00044FB4
		private List<TimeSpan> GetDateChooserTimes()
		{
			this.LoadChooserDatesAndTimes();
			return this._chooserTimes;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00046DD4 File Offset: 0x00044FD4
		private List<DateTime> GetDateChooserClosedDateTimes()
		{
			this.LoadChooserDatesAndTimes();
			return this._chooserClosedDates;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00046DF4 File Offset: 0x00044FF4
		private static bool AreDateTimesEqual(DateTime? dt1, DateTime dt2)
		{
			bool flag = dt1 == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DateTime value = dt1.Value;
				result = (value.Year == dt2.Year && value.Month == dt2.Month && value.Day == dt2.Day && value.Hour == dt2.Hour && value.Minute == dt2.Minute);
			}
			return result;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00046E74 File Offset: 0x00045074
		public void SelectDate(string dateString, int level)
		{
			bool flag = string.IsNullOrEmpty(dateString);
			if (!flag)
			{
				try
				{
					string text = dateString.Trim().Substring(3);
					string text2 = text.Substring(4).Replace(".", "");
					string[] array = text2.Split(new char[]
					{
						' '
					}, StringSplitOptions.RemoveEmptyEntries);
					string s = array[0] + " " + array[1];
					string s2 = array[2] + " " + array[3];
					DateTime dateTime;
					DateTime dt;
					bool flag2 = !DateTime.TryParse(s, out dt) || !DateTime.TryParse(s2, out dateTime);
					if (!flag2)
					{
						dt = new DateTime(dt.Year, dt.Month, dt.Day, dateTime.Hour, dateTime.Minute, 0);
						Func<RadComboBox, bool> <>9__2;
						for (int i = 0; i < this.t_dates.Rows.Count; i++)
						{
							IEnumerable<RadComboBox> source = from TableCell cell in this.t_dates.Rows[i].Cells
							where cell.Controls.Count > 0 && cell.Controls[0] is RadComboBox
							select (RadComboBox)cell.Controls[0];
							Func<RadComboBox, bool> predicate;
							if ((predicate = <>9__2) == null)
							{
								predicate = (<>9__2 = ((RadComboBox g) => CtrlInstructorFinalExamDateTimeGrid.AreDateTimesEqual(CtrlInstructorFinalExamDateTimeGrid.ParseAvailability(g.SelectedValue), dt)));
							}
							RadComboBox radComboBox = source.FirstOrDefault(predicate);
							bool flag3 = radComboBox == null;
							if (!flag3)
							{
								radComboBox.SelectedIndex = level;
								break;
							}
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00047054 File Offset: 0x00045254
		private static List<DateTime> ParseDates(string datesStr)
		{
			string[] array = datesStr.Split(new char[]
			{
				','
			});
			List<DateTime> list = new List<DateTime>();
			foreach (string s in array)
			{
				DateTime item;
				bool flag = DateTime.TryParse(s, out item);
				if (flag)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000470B4 File Offset: 0x000452B4
		private static List<TimeSpan> ParseTimes(string datesStr)
		{
			string[] array = datesStr.Split(new char[]
			{
				','
			});
			List<TimeSpan> list = new List<TimeSpan>();
			foreach (string s in array)
			{
				TimeSpan item;
				bool flag = TimeSpan.TryParse(s, out item);
				if (flag)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00047114 File Offset: 0x00045314
		private static string AvailabilityToString(DateTime dateAndTime, int level)
		{
			string str = dateAndTime.ToString("yyyy-MM-dd H:mm");
			return str + "`" + level.ToString();
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00047148 File Offset: 0x00045348
		private static DateTime? ParseAvailability(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = s.IndexOf('`');
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string s2 = s.Substring(0, num);
					DateTime value;
					result = ((!DateTime.TryParse(s2, out value)) ? null : new DateTime?(value));
				}
			}
			return result;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000471BA File Offset: 0x000453BA
		private void Page_Init(object sender, EventArgs e)
		{
			this.SetupTable();
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00003E0A File Offset: 0x0000200A
		public new void Init(int lucid, int examId)
		{
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000471C4 File Offset: 0x000453C4
		public bool IsSelectionOk()
		{
			List<FinalExamDay> selectedExamDates = this.GetSelectedExamDates();
			return selectedExamDates.Count == 3;
		}

		// Token: 0x040007D8 RID: 2008
		private List<DateTime> _chooserDates;

		// Token: 0x040007D9 RID: 2009
		private List<DateTime> _chooserClosedDates;

		// Token: 0x040007DA RID: 2010
		private List<TimeSpan> _chooserTimes;

		// Token: 0x040007DB RID: 2011
		protected Panel p_dates;

		// Token: 0x040007DC RID: 2012
		protected Label lbl_dateinstructions;

		// Token: 0x040007DD RID: 2013
		protected Table t_dates;

		// Token: 0x040007DE RID: 2014
		protected TableHeaderRow t_dates_header;

		// Token: 0x040007DF RID: 2015
		protected TableHeaderCell t_dates_cell_times;
	}
}
