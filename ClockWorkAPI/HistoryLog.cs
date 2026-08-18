using System;
using System.Data;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000006 RID: 6
	public class HistoryLog
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000023B4 File Offset: 0x000013B4
		public static DataTable GetAppointmentModifiedHistory(AppointmentDTO app, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out string errstr)
		{
			da.SelectCommand.CommandText = "SELECT 'Created' AS action,app.dateadded AS action_date,'' AS changed_description,'' AS changed_room,'' AS changed_memo,'' AS changed_attendees,'' AS changed_cancelled,'' AS changed_noshow,'' AS changed_course,'' AS changed_other1,'' AS changed_other2,'' AS changed_icons,'' AS changed_datetime,p.firstname,p.lastname FROM appointments app LEFT JOIN people p ON p.personid=app.personid WHERE app.appointmentid=@appid UNION SELECT x.action,m.datemodified AS action_date,m.changed_description,m.changed_room,m.changed_memo,m.changed_attendees,m.changed_cancelled,m.changed_noshow,m.changed_course,m.changed_other1,m.changed_other2,m.changed_icons,m.changed_datetime,p.firstname,p.lastname FROM appointmentsmodifieddates m LEFT JOIN (SELECT 1 AS howmodifiedcode,'Modified' AS action UNION SELECT 2 AS howmodifiedcode,'Deleted' AS action) x ON x.howmodifiedcode=m.howmodifiedcode LEFT JOIN people p ON p.personid=m.personid WHERE m.appointmentid=@appid ORDER BY action_date";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@appid", app.AppointmentId);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable, out errstr);
			DataTable result;
			if (dataTable.Rows.Count > 0)
			{
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("action");
				dataTable2.Columns.Add("action_date");
				dataTable2.Columns.Add("action_by");
				dataTable2.Columns.Add("action_details");
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					DataRow dataRow2 = dataTable2.NewRow();
					dataRow2["action"] = dataRow["action"];
					dataRow2["action_date"] = dataRow["action_date"];
					byte[] inputInBytes = (dataRow["firstname"] != DBNull.Value) ? ((byte[])dataRow["firstname"]) : null;
					byte[] inputInBytes2 = (dataRow["lastname"] != DBNull.Value) ? ((byte[])dataRow["lastname"]) : null;
					string str = tripleDES.Decrypt(inputInBytes);
					string str2 = tripleDES.Decrypt(inputInBytes2);
					dataRow2["action_by"] = str + " " + str2;
					string text = "";
					for (int i = 2; i < 13; i++)
					{
						if (dataRow[i] != DBNull.Value && Convert.ToBoolean(dataRow[i]))
						{
							if (text.Length > 0)
							{
								text += ", ";
							}
							text += dataTable.Columns[i].ColumnName;
						}
					}
					dataRow2["action_details"] = text;
					dataTable2.Rows.Add(dataRow2);
				}
				result = dataTable2;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
