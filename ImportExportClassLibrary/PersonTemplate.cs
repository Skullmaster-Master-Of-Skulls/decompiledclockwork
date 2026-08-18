using System;
using System.Collections;
using System.Data;
using System.IO;
using ClockWorkAPI;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x0200004B RID: 75
	public class PersonTemplate
	{
		// Token: 0x06000302 RID: 770 RVA: 0x0001EC80 File Offset: 0x0001DC80
		public static string ExportToEmailTemplate(string templateDbPrefix, PersonBaseDTO student, TemplateCodeCollection staticCodes, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string title, string caption, out string emsg, bool allowedToEditTemplates)
		{
			Type typeFromHandle = typeof(string);
			staticCodes.Add("Firstname", student.FirstName, typeFromHandle, new string[]
			{
				"First name"
			});
			staticCodes.Add("Lastname", student.LastName, typeFromHandle, new string[]
			{
				"Last name"
			});
			staticCodes.Add("Student_no", student.Student_no, typeFromHandle, new string[]
			{
				"StudentNo",
				"StudentNum",
				"Student Num",
				"StudentNo",
				"Student No"
			});
			staticCodes.Add("Middlename", student.MiddleName, typeFromHandle, new string[]
			{
				"Middle name"
			});
			staticCodes.Add("Name", student.GetName(), typeFromHandle, new string[0]);
			staticCodes.Add("Date", DateTime.Now.ToString("yyyy-MM-dd"), typeFromHandle, new string[0]);
			staticCodes.Add("Time", DateTime.Now.ToString("hh:mm tt"), typeFromHandle, new string[0]);
			TemplateInDatabase templateInDatabase = TemplateInDatabase.AskUserWhichTemplate(TemplateInDatabase.TemplateDialogType.Generic, da, templateDbPrefix, "", title, caption, allowedToEditTemplates, new DataTable());
			string text;
			if (templateInDatabase == null)
			{
				text = TemplatesClass.GetTempFilename(".txt");
				StreamWriter streamWriter = new StreamWriter(text);
				streamWriter.WriteLine();
				streamWriter.Close();
				emsg = "";
				return text;
			}
			text = templateInDatabase.Filename;
			if (text.Length <= 0)
			{
				emsg = "";
				return "";
			}
			string text2;
			ArrayList codes = TemplatesClass.GetCodes(text, '#', '<', '>', '#', out text2);
			if (text2 != null)
			{
				emsg = "errmsg=" + text2;
				return "";
			}
			new DataSet();
			ArrayList arrayList = new ArrayList();
			da.SelectCommand.CommandText = "SELECT controlid,controlcaption,controlname,controlcode,setting1,setting2,setting3,setting4,defaultvalue FROM dynamiccontrols ORDER BY controlcaption";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			foreach (object obj in codes)
			{
				Code code = (Code)obj;
				string text3 = code.codeText.ToLower();
				bool flag = false;
				foreach (object obj2 in staticCodes)
				{
					TemplateCode templateCode = (TemplateCode)obj2;
					if (templateCode.CodeName_lcase.CompareTo(text3) == 0)
					{
						code.codeValue = templateCode.CodeValue.ToString();
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (text3.CompareTo("accommodations") != 0 && text3.CompareTo("accommodationsprof") != 0 && text3.CompareTo("accommodationsexam") != 0)
					{
						text3.CompareTo("accommodationsother");
					}
					flag = false;
					foreach (object obj3 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj3;
						int num = (int)dataRow[0];
						string text4 = dataRow[1].ToString().ToLower().Trim();
						if (text3.CompareTo(num.ToString()) == 0)
						{
							string codeValue = PersonTemplate.LookupDynamicData(student, dataRow, da, tripleDES);
							staticCodes.Add(num.ToString(), codeValue, typeFromHandle, new string[0]);
							code.codeValue = codeValue;
							flag = true;
							break;
						}
						if (text4.CompareTo(text3) == 0)
						{
							string codeValue = PersonTemplate.LookupDynamicData(student, dataRow, da, tripleDES);
							staticCodes.Add(text3, codeValue, typeFromHandle, new string[0]);
							code.codeValue = codeValue;
							flag = true;
							break;
						}
						string text5 = dataRow[2].ToString().Trim().ToLower();
						if (text5.CompareTo(text3) == 0)
						{
							string codeValue = PersonTemplate.LookupDynamicData(student, dataRow, da, tripleDES);
							staticCodes.Add(text3, codeValue, typeFromHandle, new string[0]);
							code.codeValue = codeValue;
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						arrayList.Add(text3);
					}
				}
			}
			bool flag2 = Path.GetExtension(text).ToLower().CompareTo(".doc") == 0;
			string tempFilename = TemplatesClass.GetTempFilename(Path.GetExtension(text));
			File.Copy(text, tempFilename);
			ArrayList arrayList2 = new ArrayList();
			arrayList2.Add(codes);
			bool hideTheWordFile = true;
			string text6;
			if (!flag2)
			{
				text6 = TemplatesClass.WriteCodes(text, tempFilename, arrayList2);
			}
			else
			{
				object[] array = TemplatesClass.ToWordFileKeepItOpen(text, tempFilename, arrayList2, false, null, out text6, hideTheWordFile);
				object obj4 = array[1];
				object obj5 = array[0];
			}
			if (text6 != null && text6.Length > 0)
			{
				emsg = "GenerateLetters: errmsg2=" + text6;
				return "";
			}
			emsg = "";
			return tempFilename;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001F19C File Offset: 0x0001E19C
		private static string LookupDynamicData(PersonBaseDTO student, DataRow drControl, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			int num = (int)drControl[0];
			int num2 = (int)drControl["controlcode"];
			int num3 = (int)drControl["setting3"];
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@cid", num);
			da.SelectCommand.Parameters.Add("@pid", student.PersonId);
			DataTable dataTable = new DataTable();
			int num4 = num2;
			switch (num4)
			{
			case 1:
			case 11:
				break;
			case 2:
			case 4:
			case 12:
				da.SelectCommand.CommandText = "SELECT controlvalue FROM maininfops WHERE personid=@pid AND controlid=@cid";
				da.Fill(dataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return "";
				}
				if ((int)dataTable.Rows[0][0] != 0)
				{
					return "Yes";
				}
				return "";
			case 3:
				if (num3 == 0)
				{
					da.SelectCommand.CommandText = "SELECT m.controlvalue,l.lookuptext FROM maininfops m LEFT JOIN lookuplists l ON l.lookuplistid=m.controlvalue WHERE m.personid=@pid AND m.controlid=@cid";
					da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						return dataTable.Rows[0][0].ToString();
					}
					return "";
				}
				else
				{
					da.SelectCommand.CommandText = "SELECT controlvalue FROM otherinfops WHERE personid=@pid AND controlid=@cid";
					da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						byte[] bytes = (byte[])dataTable.Rows[0][0];
						return ClockWorkCore.BytesToString(bytes, num3 == -1, tripleDES);
					}
					return "";
				}
				break;
			case 5:
			case 7:
			case 8:
			case 9:
			case 10:
				goto IL_271;
			case 6:
				da.SelectCommand.CommandText = "SELECT controlvalue FROM datetimeinfops WHERE personid=@pid AND controlid=@cid";
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					return ((DateTime)dataTable.Rows[0][0]).ToString("yyyy-MM-dd");
				}
				return "";
			default:
				if (num4 != 300)
				{
					goto IL_271;
				}
				break;
			}
			da.SelectCommand.CommandText = "SELECT controlvalue FROM otherinfops WHERE personid=@pid AND controlid=@cid";
			string text;
			da.Fill(dataTable, out text);
			if (text != null && text.Length > 0)
			{
				return "";
			}
			if (dataTable.Rows.Count > 0)
			{
				byte[] bytes2 = (byte[])dataTable.Rows[0][0];
				return ClockWorkCore.BytesToString(bytes2, num3 != 0, tripleDES);
			}
			return "";
			IL_271:
			return "";
		}
	}
}
