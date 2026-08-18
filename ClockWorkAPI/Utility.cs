using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClockWorkAPI.EntityExtensions;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.People;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.People;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000094 RID: 148
	public class Utility
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x000298D0 File Offset: 0x000288D0
		public static string ListToString(List<int> numbers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < numbers.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(numbers[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00029934 File Offset: 0x00028934
		public static string ListToString(List<DateTime> dates)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < dates.Count; i++)
			{
				DateTime dateTime = dates[i];
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(dateTime.ToString("dddd MMMM d, yyyy"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000299A0 File Offset: 0x000289A0
		public static string ListToString(List<string> strings)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < strings.Count; i++)
			{
				string value = strings[i];
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00029A00 File Offset: 0x00028A00
		public static List<int> IntListFromString(string commaSeparatedNumbers)
		{
			List<int> list = new List<int>();
			List<int> result;
			if (commaSeparatedNumbers == null)
			{
				result = list;
			}
			else
			{
				string[] array = commaSeparatedNumbers.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					if (!string.IsNullOrEmpty(text2))
					{
						int item;
						if (int.TryParse(text2, out item))
						{
							list.Add(item);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00029A98 File Offset: 0x00028A98
		public static string MemoAndLocationToMemoWithLocation(bool useLocation, string memo, string location)
		{
			string result;
			if (!useLocation || location.Trim().Length < 1)
			{
				result = memo;
			}
			else if (memo.Trim().Length < 1)
			{
				result = location;
			}
			else
			{
				result = location + Environment.NewLine + memo;
			}
			return result;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00029AF0 File Offset: 0x00028AF0
		public static string RtfToString(string rtf)
		{
			if (Utility.richTextBox == null)
			{
				Utility.richTextBox = new RichTextBox();
			}
			string text;
			try
			{
				int num = rtf.IndexOf("{\\rtf");
				if (num > 0)
				{
					string rtf2;
					string location;
					Utility.MemoToMemoAndLocation(true, rtf, out rtf2, out location);
					Utility.richTextBox.Rtf = rtf2;
					text = Utility.MemoAndLocationToMemoWithLocation(true, Utility.richTextBox.Text, location);
				}
				else
				{
					if (num < 0)
					{
						return rtf;
					}
					Utility.richTextBox.Rtf = rtf;
					text = Utility.richTextBox.Text;
				}
			}
			catch
			{
				Utility.richTextBox.Dispose();
				Utility.richTextBox = null;
				text = "!%!%" + rtf;
			}
			text = text.Replace("%20", " ");
			text = text.Replace("file:///", "");
			return text;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00029BF0 File Offset: 0x00028BF0
		public string StringToRtf(string str)
		{
			if (Utility.richTextBox == null)
			{
				Utility.richTextBox = new RichTextBox();
			}
			Utility.richTextBox.Text = str;
			return Utility.richTextBox.Rtf;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00029C34 File Offset: 0x00028C34
		public static string StringToRtf2(string str)
		{
			return "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Arial;}} \\viewkind4\\uc1\\pard\\fs20 " + str + "\\par }";
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00029C58 File Offset: 0x00028C58
		public static void CopyToClipboard(List<AppointmentDTO> appointments)
		{
			DataFormats.Format format = DataFormats.GetFormat(typeof(List<AppointmentDTO>).FullName);
			IDataObject dataObject = new DataObject();
			dataObject.SetData(format.Name, false, appointments);
			Clipboard.SetDataObject(dataObject, false);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00029C98 File Offset: 0x00028C98
		public static List<AppointmentDTO> FromClipboardAppointments()
		{
			List<AppointmentDTO> result = new List<AppointmentDTO>();
			IDataObject dataObject = Clipboard.GetDataObject();
			string fullName = typeof(List<AppointmentDTO>).FullName;
			if (dataObject.GetDataPresent(fullName))
			{
				result = (dataObject.GetData(fullName) as List<AppointmentDTO>);
			}
			return result;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00029CE8 File Offset: 0x00028CE8
		public static AppointmentDTO FromClipboard()
		{
			AppointmentDTO result = null;
			IDataObject dataObject = Clipboard.GetDataObject();
			string fullName = typeof(AppointmentDTO).FullName;
			if (dataObject.GetDataPresent(fullName))
			{
				result = (dataObject.GetData(fullName) as AppointmentDTO);
			}
			return result;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00029D32 File Offset: 0x00028D32
		public static void CopyToClipboard(AppointmentDTO app)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00029D3C File Offset: 0x00028D3C
		public static bool DeleteAppointment(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, PersonBaseDTO whoAmI, int appointmentID)
		{
			bool result;
			if (appointmentID >= 0)
			{
				DataTable dataTable = new DataTable();
				da.SelectCommand.CommandText = "SELECT * FROM perappdata2 WHERE appointmentid=@appid";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@appid", appointmentID);
				da.Fill(dataTable);
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@appointmentid", appointmentID);
				da.Connection.Open();
				try
				{
					da.SelectCommand.CommandText = "DELETE FROM appointmenticons WHERE appointmentid=@appointmentid";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@appointmentid", appointmentID);
					da.SelectCommand.ExecuteNonQuery();
					da.SelectCommand.CommandText = "DELETE FROM appointmentmemos WHERE appointmentid=@appointmentid";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@appointmentid", appointmentID);
					da.SelectCommand.ExecuteNonQuery();
					da.SelectCommand.CommandText = "DELETE FROM appointmentsmodifieddates WHERE appointmentid=@appointmentid";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@appointmentid", appointmentID);
					da.SelectCommand.ExecuteNonQuery();
					da.SelectCommand.CommandText = "DELETE FROM attendees WHERE appointmentid=@appointmentid";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@appointmentid", appointmentID);
					da.SelectCommand.ExecuteNonQuery();
					da.SelectCommand.CommandText = "DELETE FROM appointmentworkshops WHERE appointmentid=@appointmentid";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@appointmentid", appointmentID);
					da.SelectCommand.ExecuteNonQuery();
					da.SelectCommand.CommandText = "DELETE FROM appointmentcourses WHERE appointmentid=@appointmentid";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@appointmentid", appointmentID);
					da.SelectCommand.ExecuteNonQuery();
					da.SelectCommand.CommandText = "DELETE FROM appointments WHERE appointmentid=@appointmentid";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@appointmentid", appointmentID);
					da.SelectCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
				}
				finally
				{
					da.Connection.Close();
				}
				Utility.LogAppModification(whoAmI, da, appointmentID, 2, false, false, false, false, false, false, false, false, false, false, false, false, dataTable);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0002A054 File Offset: 0x00029054
		public static void LogAppModification(PersonBaseDTO whoAmI, UnivDataAdapter da, int appID, int howModifiedCode, bool deleteOldLogData, bool changed_datetime, bool changed_description, bool changed_room, bool changed_memo, bool changed_attendees, bool changed_cancelled, bool changed_noshow, bool changed_course, bool changed_other1, bool changed_other2, bool changed_icons)
		{
			Utility.LogAppModification(whoAmI, da, appID, howModifiedCode, deleteOldLogData, changed_datetime, changed_description, changed_room, changed_memo, changed_attendees, changed_cancelled, changed_noshow, changed_course, changed_other1, changed_other2, changed_icons, null);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0002A088 File Offset: 0x00029088
		public static void LogAppModification(PersonBaseDTO whoAmI, UnivDataAdapter da, int appID, int howModifiedCode, bool deleteOldLogData, bool changed_datetime, bool changed_description, bool changed_room, bool changed_memo, bool changed_attendees, bool changed_cancelled, bool changed_noshow, bool changed_course, bool changed_other1, bool changed_other2, bool changed_icons, DataTable data_pa)
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.AppointmentModificationsTrackingEnhancement);
			if (howModifiedCode == 2)
			{
				da.SelectCommand.CommandText = "INSERT INTO appointmentsdeleteddates (appointmentid,datedeleted,personid) VALUES (@appid,getdate(),@pid)";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@appid", appID);
				da.SelectCommand.Parameters.Add("@pid", whoAmI.PersonId);
				da.Fill(new DataTable());
			}
			else
			{
				da.SelectCommand.CommandText = "INSERT INTO appointmentsmodifieddates (appointmentid,datemodified,personid,howmodifiedcode";
				if (flag)
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.CommandText += ",changed_datetime,changed_description,changed_room,changed_memo,changed_attendees,changed_cancelled,changed_noshow,changed_course,changed_other1,changed_other2,changed_icons";
				}
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText += ") VALUES (@appointmentid,@datemodified,@personid,@howmodifiedcode";
				if (flag)
				{
					UnivCommand selectCommand3 = da.SelectCommand;
					selectCommand3.CommandText += ",@changed_datetime,@changed_description,@changed_room,@changed_memo,@changed_attendees,@changed_cancelled,@changed_noshow,@changed_course,@changed_other1,@changed_other2,@changed_icons";
				}
				UnivCommand selectCommand4 = da.SelectCommand;
				selectCommand4.CommandText += ")";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@appointmentid", appID);
				da.SelectCommand.Parameters.Add("@datemodified", DateTime.Now);
				da.SelectCommand.Parameters.Add("@personid", whoAmI.PersonId);
				da.SelectCommand.Parameters.Add("@howmodifiedcode", howModifiedCode);
				if (flag)
				{
					da.SelectCommand.Parameters.Add("@changed_datetime", changed_datetime);
					da.SelectCommand.Parameters.Add("@changed_description", changed_description);
					da.SelectCommand.Parameters.Add("@changed_room", changed_room);
					da.SelectCommand.Parameters.Add("@changed_memo", changed_memo);
					da.SelectCommand.Parameters.Add("@changed_attendees", changed_attendees);
					da.SelectCommand.Parameters.Add("@changed_cancelled", changed_cancelled);
					da.SelectCommand.Parameters.Add("@changed_noshow", changed_noshow);
					da.SelectCommand.Parameters.Add("@changed_course", changed_course);
					da.SelectCommand.Parameters.Add("@changed_other1", changed_other1);
					da.SelectCommand.Parameters.Add("@changed_other2", changed_other2);
					da.SelectCommand.Parameters.Add("@changed_icons", changed_icons);
				}
				da.Fill(new DataTable());
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002A414 File Offset: 0x00029414
		public static string ToHtml(AppointmentDTO app)
		{
			string value = "<tr><td colspan='2'>&nbsp;</td></tr>";
			StringBuilder stringBuilder = new StringBuilder();
			string appTypeDescription = app.GetAppTypeDescription();
			stringBuilder.AppendFormat("<h2>{0}</h2>", string.IsNullOrEmpty(appTypeDescription) ? "{un-titled}" : appTypeDescription);
			stringBuilder.Append("<table width='100%' border=0 cellspacing=0 cellpadding=0>");
			string text = app.SubTitle;
			if (text == null)
			{
				text = "";
			}
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat("<tr><td width='100'><b>Sub-title:</b></td><td>{0}</td></tr>", (text == null) ? "" : text);
				stringBuilder.Append(value);
			}
			stringBuilder.AppendFormat("<tr><td width='100'><b>Date:</b></td><td>{0}</td></tr>", app.StartDateTime.ToString("dddd MMMM d, yyyy"));
			stringBuilder.AppendFormat("<tr><td><b>Time:</b></td><td>{0} to {1}</td></tr>", app.StartDateTime.ToString("h:mm tt"), app.EndDateTime.ToString("h:mm tt"));
			stringBuilder.Append(value);
			stringBuilder.AppendFormat("<tr><td width='100'><b>Status:</b></td><td>{0}</td></tr>", Utility.Status(app));
			stringBuilder.Append(value);
			stringBuilder.Append("<tr><td width='100'><b>Attendees:</b></td><td>");
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Students || a.Person.CoreGroup == eCoreGroupDTO.Staff || a.Person.CoreGroup == eCoreGroupDTO.Admin);
			stringBuilder.Append(string.Join("<br />", list.ConvertAll<string>((AttendeeDTO att) => string.Format("{0} {1} ({2})", att.Person.FirstName, att.Person.LastName, att.Person.Student_no)).ToArray()));
			stringBuilder.Append("</td></tr>");
			stringBuilder.Append(value);
			stringBuilder.Append("</table>");
			string memoPlainText = Utility.GetMemoPlainText(app);
			if (!string.IsNullOrEmpty(memoPlainText))
			{
				stringBuilder.Append(memoPlainText.Replace(Environment.NewLine, "<br />"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0002A5E8 File Offset: 0x000295E8
		public static string GetMemoPlainText(AppointmentDTO Appointment)
		{
			string result;
			try
			{
				if (Appointment.Memo == null || Appointment.Memo.Trim().Length < 1)
				{
					result = string.Empty;
				}
				else
				{
					int num = Appointment.Memo.IndexOf("{\\rtf1\\", StringComparison.OrdinalIgnoreCase);
					string input;
					if (num == 0)
					{
						input = Appointment.Memo;
					}
					else
					{
						if (num <= 0)
						{
							return Appointment.Memo;
						}
						input = Appointment.Memo.Substring(num);
					}
					string pattern = "\\\\\\w+|\\{.*?\\}|}";
					result = Regex.Replace(input, pattern, string.Empty).Trim();
				}
			}
			catch (Exception ex)
			{
				string text = ex.ToString();
				result = (ex.Message ?? "");
			}
			return result;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002A6C4 File Offset: 0x000296C4
		public static string Status(AppointmentDTO app)
		{
			List<string> list = new List<string>();
			if (app.IsCancelled)
			{
				list.Add("Cancelled");
			}
			if (app.GetAppCode() == -1)
			{
				list.Add("Tentative");
			}
			if (app.IsPrivate)
			{
				list.Add("Private");
			}
			if (list.Count < 1)
			{
				list.Add("-");
			}
			return string.Join(", ", list.ToArray());
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0002A854 File Offset: 0x00029854
		public static void GetExportMemoAndLocation(AppointmentDTO app, out string memo, out string location, out string appType, bool includeStudentNames)
		{
			appType = app.GetAppTypeDescription();
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Rooms);
			List<AttendeeDTO> list2 = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Staff || a.Person.CoreGroup == eCoreGroupDTO.Admin);
			string text = string.Join(", ", list.ConvertAll<string>((AttendeeDTO att) => att.Person.FirstName).ToArray());
			string text2 = string.Join(", ", list2.ConvertAll<string>((AttendeeDTO att) => att.Person.GetName()).ToArray());
			int num = app.Memo.IndexOf("{\\rtf");
			if (num > 0)
			{
				string rtf;
				string text3;
				Utility.MemoToMemoAndLocation(true, app.Memo, out rtf, out text3);
				if (text3.Trim().Length > 0)
				{
					if (text.Length > 0)
					{
						location = text + "\n" + text3;
					}
					else
					{
						location = text3;
					}
				}
				else
				{
					location = text;
				}
				memo = Utility.RtfToString(rtf);
			}
			else
			{
				location = text;
				memo = Utility.GetMemoPlainText(app);
			}
			string text4 = "";
			if (includeStudentNames)
			{
				List<AttendeeDTO> list3 = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Students);
				text4 = string.Join(", ", list.ConvertAll<string>((AttendeeDTO att) => string.Format("{0} ({1}){2}", att.Person.GetName(), att.Person.Student_no, att.IsNoShow ? " [No-show]" : "")).ToArray());
			}
			if (text2.Length > 0 || text4.Length > 0)
			{
				memo += "\n==================";
			}
			if (text2.Length > 0)
			{
				memo = memo + "\n" + text2;
			}
			if (text4.Length > 0)
			{
				memo = memo + "\n" + text4;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0002AAA4 File Offset: 0x00029AA4
		public static void MemoToMemoAndLocation(bool useLocation, string memoWithLocation, out string memo, out string location)
		{
			if (!useLocation)
			{
				memo = memoWithLocation;
				location = "";
			}
			else if (memoWithLocation.IndexOf("{\\rtf") == 0)
			{
				memo = memoWithLocation;
				location = "";
			}
			else
			{
				int num = memoWithLocation.IndexOf(Environment.NewLine);
				int length = Environment.NewLine.Length;
				if (num == 0)
				{
					location = "";
					memo = memoWithLocation.Substring(num + length);
				}
				else if (num > 0)
				{
					memo = memoWithLocation.Substring(num + length);
					location = memoWithLocation.Substring(0, num);
				}
				else
				{
					memo = "";
					location = memoWithLocation;
				}
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0002AB58 File Offset: 0x00029B58
		public static void AddAppStringToStringListArray(DataRow dr, ref ArrayList stringsToPrint, int personID)
		{
			string text = ((DateTime)dr[0]).ToString("MMMM dd, yyyy (dddd)");
			text = string.Concat(new string[]
			{
				text,
				"   [",
				dr[1].ToString(),
				" - ",
				dr[2].ToString(),
				"]"
			});
			stringsToPrint.Add(text);
			stringsToPrint.Add("Type of appointment: " + dr[3].ToString());
			string text2 = dr[4].ToString();
			if (text2.Length > 0)
			{
				stringsToPrint.Add(text2);
			}
			string text3 = dr[5].ToString();
			if (text3.Length > 0)
			{
				stringsToPrint.Add("Attendees: {" + text3 + "}");
			}
			text = dr[7].ToString().Trim();
			if (text.Length > 0)
			{
				stringsToPrint.Add("Memo: " + text);
			}
			string text4 = dr[6].ToString();
			if (text4.Length > 0)
			{
				stringsToPrint.Add("Room: " + text4);
			}
			stringsToPrint.Add("");
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0002ACD0 File Offset: 0x00029CD0
		public static void CancelAppointment(int appId, UnivDataAdapter da, int whoAmIId)
		{
			da.SelectCommand.CommandText = "UPDATE appointments SET cancelled=1 WHERE appointmentid=@appid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@appid", appId);
			da.Fill(new DataTable());
			Utility.LogAppModification(Utility.Personx(whoAmIId, "", "", "", "", 0), da, appId, 1, false, false, false, false, false, false, true, false, false, false, false, false);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0002AD58 File Offset: 0x00029D58
		public static ArrayList AppsListToArrayList(List<AppointmentDTO> apps)
		{
			ArrayList arrayList = new ArrayList();
			foreach (AppointmentDTO value in apps)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0002ADBC File Offset: 0x00029DBC
		public static AppointmentDTO Appointmentx(int _appointmentID, DateTime _startDateTime, DateTime _endDateTime, bool _isCancelled, bool _isNoShow, string _description, int _descriptionCode, List<AttendeeDTO> _attendees, string memoText, bool _memoEncrypted, bool _isHidden, bool _isLocked, int _overrideColourArgb, bool _overrideColourActive, List<AppointmentIconDTO> _IconIDs, int _WhoAdded, DateTime _DateAdded, DateTime _OriginalStartDateTime, DateTime _OriginalEndDateTime, string _TestNote, string _StudentNote, string _Instructor, string _InstructorEmail, string _InstructorPhone)
		{
			AppointmentDTO appointmentDTO = new AppointmentDTO();
			Utility.SetValues(appointmentDTO, _appointmentID, _startDateTime, _endDateTime, _isCancelled, _isNoShow, _description, _descriptionCode, _attendees, memoText, _memoEncrypted, _isHidden, _isLocked, _overrideColourArgb, _overrideColourActive, _IconIDs, _WhoAdded, _DateAdded, "", "", "", "", -1, -1, 0, 0, -1, _OriginalStartDateTime, _OriginalEndDateTime, _TestNote, _StudentNote, _Instructor, _InstructorEmail, _InstructorPhone);
			return appointmentDTO;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0002AE24 File Offset: 0x00029E24
		public static AppointmentDTO Appointmentx(int _appointmentID, DateTime _startDateTime, DateTime _endDateTime, bool _isCancelled, bool _isNoShow, string _description, int _descriptionCode, List<AttendeeDTO> _attendees, string memoText, bool _memoEncrypted, bool _isHidden, bool _isLocked, int _overrideColourArgb, bool _overrideColourActive, List<AppointmentIconDTO> _IconIDs, int _WhoAdded, DateTime _DateAdded, DateTime _OriginalStartDateTime, DateTime _OriginalEndDateTime, string _TestNote, string _StudentNote, string _Instructor, string _InstructorEmail, string _InstructorPhone, object ctrl)
		{
			AppointmentDTO appointmentDTO = new AppointmentDTO();
			Utility.SetValues(appointmentDTO, _appointmentID, _startDateTime, _endDateTime, _isCancelled, _isNoShow, _description, _descriptionCode, _attendees, memoText, _memoEncrypted, _isHidden, _isLocked, _overrideColourArgb, _overrideColourActive, _IconIDs, _WhoAdded, _DateAdded, "", "", "", "", -1, -1, 0, 0, -1, _OriginalStartDateTime, _OriginalEndDateTime, _TestNote, _StudentNote, _Instructor, _InstructorEmail, _InstructorPhone);
			return appointmentDTO;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0002AE8C File Offset: 0x00029E8C
		public static AppointmentDTO Appointmentx(int _appointmentID, DateTime _startDateTime, DateTime _endDateTime, bool _isCancelled, bool _isNoShow, string _description, int _descriptionCode, List<AttendeeDTO> _attendees, string memoText, bool _memoEncrypted, bool _isHidden, bool _isLocked, int _overrideColourArgb, bool _overrideColourActive, List<AppointmentIconDTO> _IconIDs, int _WhoAdded, DateTime _DateAdded, string _WorkshopDescription, string _SubjectDescription, string _CourseDescription, string _SectionDescription, int _WorkshopID, int _LuCourseID, int _ExtraAttendeesCount, int _AppCode, int _GroupCode, DateTime _OriginalStartDateTime, DateTime _OriginalEndDateTime, string _TestNote, string _StudentNote, string _Instructor, string _InstructorEmail, string _InstructorPhone)
		{
			AppointmentDTO appointmentDTO = new AppointmentDTO();
			Utility.SetValues(appointmentDTO, _appointmentID, _startDateTime, _endDateTime, _isCancelled, _isNoShow, _description, _descriptionCode, _attendees, memoText, _memoEncrypted, _isHidden, _isLocked, _overrideColourArgb, _overrideColourActive, _IconIDs, _WhoAdded, _DateAdded, _WorkshopDescription, _SubjectDescription, _CourseDescription, _SectionDescription, _WorkshopID, _LuCourseID, _ExtraAttendeesCount, _AppCode, _GroupCode, _OriginalStartDateTime, _OriginalEndDateTime, _TestNote, _StudentNote, _Instructor, _InstructorEmail, _InstructorPhone);
			return appointmentDTO;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0002AEEC File Offset: 0x00029EEC
		public static AppointmentDTO Appointmentx(int _appointmentID, DateTime _startDateTime, DateTime _endDateTime, bool _isCancelled, bool _isNoShow, string _description, int _descriptionCode, List<AttendeeDTO> _attendees, string memoText, bool _memoEncrypted, bool _isHidden, bool _isLocked, int _overrideColourArgb, bool _overrideColourActive, List<AppointmentIconDTO> _IconIDs, int _WhoAdded, DateTime _DateAdded, string _WorkshopDescription, string _SubjectDescription, string _CourseDescription, string _SectionDescription, int _WorkshopID, int _LuCourseID, int _ExtraAttendeesCount, int _AppCode, int _GroupCode, DateTime _OriginalStartDateTime, DateTime _OriginalEndDateTime, string _TestNote, string _StudentNote, string _Instructor, string _InstructorEmail, string _InstructorPhone, object ctrl)
		{
			AppointmentDTO appointmentDTO = new AppointmentDTO();
			Utility.SetValues(appointmentDTO, _appointmentID, _startDateTime, _endDateTime, _isCancelled, _isNoShow, _description, _descriptionCode, _attendees, memoText, _memoEncrypted, _isHidden, _isLocked, _overrideColourArgb, _overrideColourActive, _IconIDs, _WhoAdded, _DateAdded, _WorkshopDescription, _SubjectDescription, _CourseDescription, _SectionDescription, _WorkshopID, _LuCourseID, _ExtraAttendeesCount, _AppCode, _GroupCode, _OriginalStartDateTime, _OriginalEndDateTime, _TestNote, _StudentNote, _Instructor, _InstructorEmail, _InstructorPhone);
			return appointmentDTO;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002AF4C File Offset: 0x00029F4C
		public static AppointmentDTO Appointmentx(int _appointmentID, DateTime _startDateTime, DateTime _endDateTime, bool _isCancelled, bool _isNoShow, string _description, int _descriptionCode, List<AttendeeDTO> _attendees, string memoText, bool _memoEncrypted, bool _isHidden, bool _isLocked, int _overrideColourArgb, bool _overrideColourActive, List<AppointmentIconDTO> _IconIDs, int _WhoAdded, DateTime _DateAdded, string _WorkshopDescription, string _SubjectDescription, string _CourseDescription, string _SectionDescription, int _WorkshopID, int _LuCourseID, int _ExtraAttendeesCount, int _AppCode, int _GroupCode, DateTime _OriginalStartDateTime, DateTime _OriginalEndDateTime, string _TestNote, string _StudentNote, string _Instructor, string _InstructorEmail, string _InstructorPhone, object ctrl, DateTime actualStartTime, DateTime actualEndTime)
		{
			AppointmentDTO appointmentDTO = new AppointmentDTO();
			Utility.SetValues(appointmentDTO, _appointmentID, _startDateTime, _endDateTime, _isCancelled, _isNoShow, _description, _descriptionCode, _attendees, memoText, _memoEncrypted, _isHidden, _isLocked, _overrideColourArgb, _overrideColourActive, _IconIDs, _WhoAdded, _DateAdded, _WorkshopDescription, _SubjectDescription, _CourseDescription, _SectionDescription, _WorkshopID, _LuCourseID, _ExtraAttendeesCount, _AppCode, _GroupCode, _OriginalStartDateTime, _OriginalEndDateTime, _TestNote, _StudentNote, _Instructor, _InstructorEmail, _InstructorPhone, actualStartTime, actualEndTime);
			return appointmentDTO;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0002AFB0 File Offset: 0x00029FB0
		private static void SetValues(AppointmentDTO app, int _appointmentID, DateTime _startDateTime, DateTime _endDateTime, bool _isCancelled, bool _isNoShow, string _description, int _descriptionCode, List<AttendeeDTO> _attendees, string memoText, bool _memoEncrypted, bool _isHidden, bool _isLocked, int _overrideColourArgb, bool _overrideColourActive, List<AppointmentIconDTO> _IconIDs, int _WhoAdded, DateTime _DateAdded, string _WorkshopDescription, string _SubjectDescription, string _CourseDescription, string _SectionDescription, int _WorkshopID, int _LuCourseID, int _ExtraAttendeesCount, int _AppCode, int _GroupCode, DateTime _OriginalStartDateTime, DateTime _OriginalEndDateTime, string _TestNote, string _StudentNote, string _Instructor, string _InstructorEmail, string _InstructorPhone)
		{
			Utility.SetValues(app, _appointmentID, _startDateTime, _endDateTime, _isCancelled, _isNoShow, _description, _descriptionCode, _attendees, memoText, _memoEncrypted, _isHidden, _isLocked, _overrideColourArgb, _overrideColourActive, _IconIDs, _WhoAdded, _DateAdded, _WorkshopDescription, _SubjectDescription, _CourseDescription, _SectionDescription, _WorkshopID, _LuCourseID, _ExtraAttendeesCount, _AppCode, _GroupCode, _OriginalStartDateTime, _OriginalEndDateTime, _TestNote, _StudentNote, _Instructor, _InstructorEmail, _InstructorPhone, DateTime.MinValue, DateTime.MinValue);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0002B038 File Offset: 0x0002A038
		private static void SetValues(AppointmentDTO app, int _appointmentID, DateTime _startDateTime, DateTime _endDateTime, bool _isCancelled, bool _isNoShow, string _description, int _descriptionCode, List<AttendeeDTO> _attendees, string memoText, bool _memoEncrypted, bool _isHidden, bool _isLocked, int _overrideColourArgb, bool _overrideColourActive, List<AppointmentIconDTO> _IconIDs, int _WhoAdded, DateTime _DateAdded, string _WorkshopDescription, string _SubjectDescription, string _CourseDescription, string _SectionDescription, int _WorkshopID, int _LuCourseID, int _ExtraAttendeesCount, int _AppCode, int _GroupCode, DateTime _OriginalStartDateTime, DateTime _OriginalEndDateTime, string _TestNote, string _StudentNote, string _Instructor, string _InstructorEmail, string _InstructorPhone, DateTime actualStartTime, DateTime actualEndTime)
		{
			if (_startDateTime.Hour == 0 && _endDateTime.Hour == 1 && _startDateTime.Minute == 0 && _endDateTime.Minute == 0)
			{
				app.StartDateTime = _startDateTime.Date;
				app.EndDateTime = _endDateTime.Date;
			}
			else
			{
				app.StartDateTime = _startDateTime;
				app.EndDateTime = _endDateTime;
			}
			app.AppointmentId = _appointmentID;
			DateTime? dateTime = null;
			app.ActualStartDateTime = ((actualStartTime == DateTime.MinValue) ? dateTime : new DateTime?(actualStartTime));
			app.ActualEndDateTime = ((actualEndTime == DateTime.MinValue) ? dateTime : new DateTime?(actualEndTime));
			app.AppType = new AppTypeDTO();
			app.AppType.AppTypeId = _descriptionCode;
			app.AppType.Description = _description;
			AppointmentExt appointmentExt = new AppointmentExt(app);
			app.Tag = appointmentExt;
			appointmentExt.AppointmentRectangle = new Rectangle(-1, -1, -1, -1);
			appointmentExt.Ctrl = null;
			appointmentExt.IsSelected = false;
			app.IsCancelled = _isCancelled;
			app.Memo = memoText;
			if (_attendees != null)
			{
				app.Attendees = _attendees.ConvertAll<AttendeeDTO>((AttendeeDTO at) => at);
			}
			app.IsPrivate = _isHidden;
			app.IsLocked = _isLocked;
			if (_overrideColourActive)
			{
				app.OverrideColour = new int?(_overrideColourArgb);
			}
			if (app.AppType == null)
			{
				app.AppType = new AppTypeDTO();
			}
			app.AppType.DefaultColourArgb = Color.Black.ToArgb();
			appointmentExt.DefaultColourSet = false;
			if (_IconIDs != null)
			{
				app.Icons = _IconIDs.ConvertAll<AppointmentIconDTO>((AppointmentIconDTO ai) => ai);
			}
			app.WhoBooked = new PersonBaseDTO();
			app.WhoBooked.PersonId = _WhoAdded;
			app.DateBooked = _DateAdded;
			if (_WorkshopID > 0)
			{
				app.WorkshopInfo = new AppointmentWorkshopInfoDTO
				{
					WorkshopId = _WorkshopID,
					WorkshopTitle = _WorkshopDescription
				};
			}
			if (_LuCourseID > 0)
			{
				if (!string.IsNullOrEmpty(_Instructor))
				{
				}
				if (_OriginalStartDateTime != DateTime.MinValue && _OriginalEndDateTime != DateTime.MinValue)
				{
				}
			}
			app.ExtraAttendeesCount = _ExtraAttendeesCount;
			app.ShowTimeAs = new AppShowTimeAsTypeDTO();
			app.ShowTimeAs.AppCode = _AppCode;
			if (_GroupCode > 0)
			{
				app.GroupCode = _GroupCode;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0002B32C File Offset: 0x0002A32C
		public static void Attendees_MakeDatabaseMatchList(int appointmentId, List<AttendeeDTO> usersRoomsAndResources)
		{
			string parameterValue = string.Join(",", usersRoomsAndResources.ConvertAll<string>((AttendeeDTO x) => x.Person.PersonId.ToString()).ToArray());
			string commandText = "DELETE FROM attendees WHERE appointmentid=@appid AND NOT personid IN (SELECT orderid AS personid FROM splitorderids( @pids,','))";
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@appid", appointmentId);
			da.SelectCommand.Parameters.Add("@pids", parameterValue);
			try
			{
				da.Fill(new DataTable());
			}
			catch (Exception ex)
			{
				throw ex;
			}
			foreach (AttendeeDTO attendeeDTO in usersRoomsAndResources)
			{
				commandText = "IF EXISTS(SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid)\r\n    UPDATE attendees SET noshow=@noshow WHERE appointmentid=@appid AND personid=@pid\r\nELSE\r\n    INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@pid,@appid,@noshow,-1)";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@appid", appointmentId);
				da.SelectCommand.Parameters.Add("@pid", attendeeDTO.Person.PersonId);
				da.SelectCommand.Parameters.Add("@noshow", attendeeDTO.IsNoShow);
				try
				{
					da.Fill(new DataTable());
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002B514 File Offset: 0x0002A514
		private static AttendeeDTO FindAppPerson(List<AttendeeDTO> list, int personIDToFind)
		{
			AttendeeDTO result;
			if (list == null)
			{
				result = null;
			}
			else
			{
				result = list.Find((AttendeeDTO att) => att.Person.PersonId == personIDToFind);
			}
			return result;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002B580 File Offset: 0x0002A580
		private static AppointmentIconDTO FindIcon(List<AppointmentIconDTO> list, int iconNumToFind)
		{
			AppointmentIconDTO result;
			if (list == null)
			{
				result = null;
			}
			else
			{
				result = list.Find((AppointmentIconDTO icon) => icon.Icon.IconNum == iconNumToFind);
			}
			return result;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0002B5C0 File Offset: 0x0002A5C0
		private static void AddToSQLUpdate(UnivDataAdapter da, object o, string colName, ref bool firstTime)
		{
			if (o != null)
			{
				if (firstTime)
				{
					firstTime = false;
				}
				else
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.CommandText += ", ";
				}
				string text = "@" + colName;
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText = selectCommand2.CommandText + colName + "=" + text;
				da.SelectCommand.Parameters.Add(text, o);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0002B640 File Offset: 0x0002A640
		private static int ExecuteDaSelectCommand2(UnivDataAdapter da)
		{
			bool flag = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
			bool flag2 = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			DataTable dataTable = new DataTable();
			int result;
			try
			{
				result = da.SelectCommand.ExecuteNonQuery2();
			}
			catch (Exception ex)
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0002B6A4 File Offset: 0x0002A6A4
		public static PersonBaseDTO LoadPerson(string snum)
		{
			if (!string.IsNullOrEmpty(snum))
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
				byte[] array = tripleDES.Encrypt(snum);
				string commandText = "SELECT * FROM people WHERE isactive=1 AND student_no=@snume";
				DataTable dataTable = new DataTable();
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@snume", tripleDES.Encrypt(snum));
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					return Utility.ParsePerson(dataTable.Rows[0], tripleDES);
				}
			}
			return null;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0002B770 File Offset: 0x0002A770
		public static PersonBaseDTO ParsePerson(DataRow dr, TripleDESEncryptionClass tripleDES)
		{
			int personId = (dr["personid"] == DBNull.Value) ? 0 : ((int)dr["personid"]);
			string student_no;
			string firstName;
			string lastName;
			string middleName;
			if (dr.Table.Columns["student_no"].DataType == typeof(string))
			{
				student_no = dr["student_no"].ToString();
				firstName = dr["firstname"].ToString();
				lastName = dr["lastname"].ToString();
				middleName = ((!dr.Table.Columns.Contains("middlename")) ? "" : dr["middlename"].ToString());
			}
			else
			{
				student_no = ((dr["student_no"] == DBNull.Value) ? "??" : tripleDES.Decrypt((byte[])dr["student_no"]));
				firstName = ((dr["firstname"] == DBNull.Value) ? "??" : tripleDES.Decrypt((byte[])dr["firstname"]));
				lastName = ((dr["lastname"] == DBNull.Value) ? "??" : tripleDES.Decrypt((byte[])dr["lastname"]));
				middleName = ((!dr.Table.Columns.Contains("middlename") || dr["middlename"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dr["middlename"]));
			}
			return new PersonBaseDTO
			{
				PersonId = personId,
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				Student_no = student_no,
				CoreGroup = eCoreGroupDTO.Unknown
			};
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0002B964 File Offset: 0x0002A964
		public static int LookupPersonId(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string snum)
		{
			int result;
			if (!string.IsNullOrEmpty(snum))
			{
				byte[] parameterValue = tripleDES.Encrypt(snum);
				string commandText = "SELECT personid FROM people WHERE isactive=1 AND student_no=@snume";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Add("@snume", parameterValue);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					result = (int)dataTable.Rows[0][0];
				}
				else
				{
					result = 0;
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002B9FC File Offset: 0x0002A9FC
		public static int GetPrimaryGroupIdFromCoreGroup(eCoreGroupDTO CoreGroup)
		{
			int result;
			if ((CoreGroup & eCoreGroupDTO.Students) > eCoreGroupDTO.Unknown)
			{
				result = 1;
			}
			else if ((CoreGroup & eCoreGroupDTO.Staff) > eCoreGroupDTO.Unknown)
			{
				result = 2;
			}
			else if ((CoreGroup & eCoreGroupDTO.Admin) > eCoreGroupDTO.Unknown)
			{
				result = 2;
			}
			else if ((CoreGroup & eCoreGroupDTO.Rooms) > eCoreGroupDTO.Unknown)
			{
				result = 3;
			}
			else if ((CoreGroup & eCoreGroupDTO.Resources) > eCoreGroupDTO.Unknown)
			{
				result = 4;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0002BA68 File Offset: 0x0002AA68
		public static string GetAppDescription(DataTable appTypes, int AppTypeID, DataSet appLookupTables)
		{
			string appDescription = Utility.GetAppDescription2(appLookupTables, appTypes, AppTypeID, 1);
			if (appDescription.Length < 1)
			{
				DataTable dataTable = appLookupTables.Tables["poc"];
				if (dataTable != null)
				{
					appDescription = Utility.GetAppDescription2(appLookupTables, dataTable, AppTypeID, 0);
				}
			}
			return appDescription;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0002BAC4 File Offset: 0x0002AAC4
		private static string GetAppDescription2(DataSet appLookupTables, DataTable appTypes, int AppTypeID, int startInd)
		{
			string result = "";
			if (appTypes != null)
			{
				for (int i = startInd; i < appTypes.Rows.Count; i++)
				{
					DataRow dataRow = appTypes.Rows[i];
					int num = (int)dataRow[0];
					if (num == AppTypeID)
					{
						int num2 = (int)dataRow[8];
						if (num2 > -1)
						{
							string appGroupTitle = Utility.GetAppGroupTitle(appLookupTables, num2);
							if (appGroupTitle.Length > 0)
							{
								return appGroupTitle + " - " + (string)dataRow[1];
							}
						}
						result = (string)dataRow[1];
					}
				}
			}
			return result;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0002BBA0 File Offset: 0x0002ABA0
		public static string GetAppGroupTitle(DataSet appLookupTables, int appointmentTypeGroupID)
		{
			DataTable dataTable = appLookupTables.Tables["apptypegroups"];
			if (dataTable != null)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow[0] != DBNull.Value)
					{
						int num = (int)dataRow[0];
						if (num == appointmentTypeGroupID)
						{
							return (string)dataRow[1];
						}
					}
				}
			}
			return "";
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0002BCA0 File Offset: 0x0002ACA0
		public static bool PersonInAppOrBookedApp(AppointmentDTO app, int personid)
		{
			bool result;
			if (app.GetWhoBookedPersonId() == personid)
			{
				result = true;
			}
			else
			{
				AttendeeDTO attendeeDTO = app.Attendees.Find((AttendeeDTO att) => att.Person.PersonId == personid);
				result = (attendeeDTO != null);
			}
			return result;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0002BD04 File Offset: 0x0002AD04
		public static List<PersonBaseDTO> StringToPeople(string peopleString, char delimiterBetweenPeople, char delimeterInternalPeople)
		{
			string[] array = peopleString.Split(new char[]
			{
				delimiterBetweenPeople
			});
			List<PersonBaseDTO> list = new List<PersonBaseDTO>(array.Length);
			foreach (string personString in array)
			{
				PersonBaseDTO item = Utility.ParsePerson(personString, delimeterInternalPeople);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0002BD70 File Offset: 0x0002AD70
		public static PersonBaseDTO ParsePerson(string personString, char delimeterInternalPeople)
		{
			NameValueCollection nameValueCollection = ClockWorkCore.ParseParameters(personString, delimeterInternalPeople);
			string text = nameValueCollection["personid"];
			int personId;
			if (text != null)
			{
				personId = int.Parse(text);
			}
			else
			{
				personId = -1;
			}
			string text2 = nameValueCollection["name"];
			if (text2 == null)
			{
			}
			string text3 = nameValueCollection["student_no"];
			if (text3 == null)
			{
				text3 = "";
			}
			string text4 = nameValueCollection["primarygroupid"];
			int groupId;
			if (text4 != null)
			{
				groupId = int.Parse(text4);
			}
			else
			{
				groupId = -1;
			}
			GroupDTO group = new GroupDTO
			{
				GroupId = groupId
			};
			string text5 = nameValueCollection["firstname"];
			if (text5 == null)
			{
				text5 = "";
			}
			string text6 = nameValueCollection["lastname"];
			if (text6 == null)
			{
				text6 = "";
			}
			return new PersonBaseDTO
			{
				PersonId = personId,
				FirstName = text5,
				LastName = text6,
				Student_no = text3,
				CoreGroup = group.GetCoreGroupFromGroup()
			};
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0002BF10 File Offset: 0x0002AF10
		public static string GetAttendeesString(AppointmentDTO app)
		{
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO att) => att.Person.CoreGroup == eCoreGroupDTO.Staff || att.Person.CoreGroup == eCoreGroupDTO.Students || att.Person.CoreGroup == eCoreGroupDTO.Admin);
			return string.Join(", ", list.ConvertAll<string>((AttendeeDTO att) => att.Person.GetName()).ToArray());
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002C000 File Offset: 0x0002B000
		public static string GetAttendeesStringWithNoshowMarked(AppointmentDTO app)
		{
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Students || a.Person.CoreGroup == eCoreGroupDTO.Staff || a.Person.CoreGroup == eCoreGroupDTO.Admin);
			return string.Join(", ", list.ConvertAll<string>((AttendeeDTO att) => string.Format("{0}{1}", att.Person.GetName(), att.IsNoShow ? " [No-show]" : "")).ToArray());
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0002C0B4 File Offset: 0x0002B0B4
		public static string GetRoomsString(AppointmentDTO app)
		{
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Rooms);
			return string.Join(", ", list.ConvertAll<string>((AttendeeDTO att) => att.Person.FirstName).ToArray());
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0002C160 File Offset: 0x0002B160
		public static List<PersonBaseDTO> GetStudents(AppointmentDTO app)
		{
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Students);
			return list.ConvertAll<PersonBaseDTO>((AttendeeDTO att) => att.Person);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0002C1FC File Offset: 0x0002B1FC
		public static List<PersonBaseDTO> GetRooms(AppointmentDTO app)
		{
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Rooms);
			return list.ConvertAll<PersonBaseDTO>((AttendeeDTO att) => att.Person);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0002C260 File Offset: 0x0002B260
		public static bool IsOverlappingTimeWith(AppointmentDTO app1, AppointmentDTO app2)
		{
			bool result;
			if (app1.AppointmentId != app2.AppointmentId)
			{
				bool flag = app1.StartDateTime >= app2.StartDateTime && app1.StartDateTime <= app2.EndDateTime;
				bool flag2 = app1.StartDateTime <= app2.StartDateTime && app1.EndDateTime >= app2.EndDateTime;
				bool flag3 = app1.EndDateTime >= app2.StartDateTime && app1.EndDateTime <= app2.EndDateTime;
				result = ((app1.StartDateTime >= app2.StartDateTime && app1.StartDateTime <= app2.EndDateTime) || (app1.StartDateTime <= app2.StartDateTime && app1.EndDateTime >= app2.EndDateTime) || (app1.EndDateTime >= app2.StartDateTime && app1.EndDateTime <= app2.EndDateTime));
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0002C3A8 File Offset: 0x0002B3A8
		public static void AddAppStringToStringListArray(AppointmentDTO app, ref ArrayList stringsToPrint, int personID, out string statusString, out string attendeesList, out string roomsList)
		{
			string text = app.StartDateTime.ToString("MMMM dd, yyyy (dddd)");
			text = string.Concat(new string[]
			{
				text,
				"   [",
				app.StartDateTime.ToString("hh:mm tt"),
				" - ",
				app.EndDateTime.ToString("hh:mm tt"),
				"]"
			});
			stringsToPrint.Add(text);
			stringsToPrint.Add("Type of appointment: " + app.GetAppTypeDescription());
			string text2 = "";
			if (app.IsCancelled)
			{
				text2 = "Cancelled ";
				stringsToPrint.Add("Appointment Was Cancelled");
			}
			AttendeeDTO attendeeDTO = app.Attendees.Find((AttendeeDTO att) => att.Person.PersonId == personID);
			if (attendeeDTO != null)
			{
				if (attendeeDTO.IsNoShow)
				{
					text2 += "No-show";
				}
			}
			string roomsString = Utility.GetRoomsString(app);
			string attendeesStringWithNoshowMarked = Utility.GetAttendeesStringWithNoshowMarked(app);
			statusString = text2;
			attendeesList = text;
			if (!string.IsNullOrEmpty(attendeesStringWithNoshowMarked))
			{
				text = "Attendees: { " + text + " }";
				stringsToPrint.Add(text);
			}
			text = Utility.GetMemoPlainText(app);
			if (text.Length > 0)
			{
				text = "Memo: " + text;
				stringsToPrint.Add(text);
			}
			if (!string.IsNullOrEmpty(roomsString))
			{
				text = "Room: " + roomsString;
				stringsToPrint.Add(text);
			}
			roomsList = roomsString;
			stringsToPrint.Add("");
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0002C570 File Offset: 0x0002B570
		public static bool IsOverlappingRoomOrPersonWith(AppointmentDTO app1, AppointmentDTO app2)
		{
			bool result;
			if (app2.AppointmentId != app1.AppointmentId)
			{
				foreach (AttendeeDTO attendeeDTO in app2.Attendees)
				{
					if (Utility.ContainsRoomResourcePerson(app1, attendeeDTO.Person.PersonId))
					{
						return true;
					}
				}
				foreach (AttendeeDTO attendeeDTO in app2.Attendees)
				{
					if (Utility.ContainsRoomResourcePerson(app1, attendeeDTO.Person.PersonId))
					{
						return true;
					}
				}
				result = false;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002C68C File Offset: 0x0002B68C
		public static bool ContainsRoomResourcePerson(AppointmentDTO app, int personID)
		{
			AttendeeDTO attendeeDTO = app.Attendees.Find((AttendeeDTO att) => att.Person.PersonId == personID);
			return attendeeDTO != null;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0002C700 File Offset: 0x0002B700
		public static string IconLetterIdentifiers(IList<AppointmentIconDTO> Icons)
		{
			return string.Join(",", Icons.ToList<AppointmentIconDTO>().ConvertAll<string>((AppointmentIconDTO icon) => (icon.Icon.IconLetterIdentifier == null) ? "" : icon.Icon.IconLetterIdentifier).ToArray());
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002C7A8 File Offset: 0x0002B7A8
		public static List<PersonBaseDTO> GetNonStudentsNonRoomsNonResources(AppointmentDTO app)
		{
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO att) => att.Person.CoreGroup != eCoreGroupDTO.Students && att.Person.CoreGroup != eCoreGroupDTO.Resources && att.Person.CoreGroup != eCoreGroupDTO.Rooms);
			return list.ConvertAll<PersonBaseDTO>((AttendeeDTO ns) => ns.Person);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002C860 File Offset: 0x0002B860
		public static string GetStudentsString(AppointmentDTO app)
		{
			IPersonBaseClientManager personBaseClientManager = new PersonBaseClientManager();
			List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Students);
			return string.Join(", ", list.ConvertAll<string>((AttendeeDTO att) => string.Format("{0}{1}", att.Person.GetName(), att.Person.Student_no)).ToArray());
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0002C8D8 File Offset: 0x0002B8D8
		public static PersonBaseDTO Personx(int personID, string _name, string _FirstName, string _LastName, string _student_no, int _PrimaryGroupID)
		{
			return Utility.Personx(personID, _name, _FirstName, "", _LastName, _student_no, _PrimaryGroupID);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0002C8FC File Offset: 0x0002B8FC
		public static PersonBaseDTO Personx(int personID, string _name, string _FirstName, string _MiddleName, string _LastName, string _student_no, int _PrimaryGroupID)
		{
			GroupDTO group = new GroupDTO
			{
				GroupId = _PrimaryGroupID
			};
			eCoreGroupDTO coreGroupFromGroup = group.GetCoreGroupFromGroup();
			return new PersonBaseDTO
			{
				PersonId = personID,
				FirstName = _FirstName,
				LastName = _LastName,
				MiddleName = _MiddleName,
				Student_no = _student_no,
				CoreGroup = coreGroupFromGroup
			};
		}

		// Token: 0x040003BF RID: 959
		private static RichTextBox richTextBox;
	}
}
