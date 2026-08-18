using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using EncryptionClassLibrary;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace ClockWorkWebAPI
{
	// Token: 0x02000011 RID: 17
	public class Core
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00006ED8 File Offset: 0x000050D8
		public static string ListAppend(string list, string item)
		{
			bool flag = list.Trim().Length > 0;
			string result;
			if (flag)
			{
				result = list + "," + item;
			}
			else
			{
				result = item;
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006F10 File Offset: 0x00005110
		public static List<int> StringToIntList(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			foreach (string text in array)
			{
				bool flag = !string.IsNullOrEmpty(text);
				if (flag)
				{
					int item;
					bool flag2 = int.TryParse(text, out item);
					if (flag2)
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006F84 File Offset: 0x00005184
		public static DateTime[] MakeDateTimesEqual(DateTime dateTime1, DateTime dateTime2)
		{
			return new DateTime[]
			{
				dateTime1,
				new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day, dateTime2.Hour, dateTime2.Minute, dateTime2.Second)
			};
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006FDC File Offset: 0x000051DC
		public static int ParseIntWithDefaultValue(string s, int defaultValue)
		{
			bool flag = !string.IsNullOrEmpty(s);
			int result;
			if (flag)
			{
				int num;
				bool flag2 = !int.TryParse(s, out num);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = num;
				}
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007014 File Offset: 0x00005214
		public static int ParseIntMinusOneIfInvalid(string s)
		{
			return Core.ParseIntWithDefaultValue(s, -1);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007030 File Offset: 0x00005230
		public static int GetSettingInt(string vs, int defaultValue)
		{
			bool flag = vs == null || vs.Trim().Length < 1;
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				try
				{
					result = int.Parse(vs);
				}
				catch
				{
					result = defaultValue;
				}
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000707C File Offset: 0x0000527C
		public static string EncodeUrlVariable(string varValue, bool encrypted, IEncryption tripleDES)
		{
			string arg = DateTime.Now.ToString("yyyy-MM-dd H:mm");
			string plainText = string.Format("{0}`{1}", varValue, arg);
			byte[] inArray = tripleDES.Encrypt(plainText);
			string str = Convert.ToBase64String(inArray);
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000070CC File Offset: 0x000052CC
		public static string UrlEncodeByteArray(byte[] bytes)
		{
			string str = Core.ByteArrayToHexString(bytes);
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000070EC File Offset: 0x000052EC
		public static string ByteArrayToHexString(byte[] Bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "0123456789ABCDEF";
			foreach (byte b in Bytes)
			{
				stringBuilder.Append(text[b >> 4]);
				stringBuilder.Append(text[(int)(b & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007150 File Offset: 0x00005350
		public static string ArrayListToString(ArrayList list, string delimiter)
		{
			bool flag = list == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = "";
				for (int i = 0; i < list.Count; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						text += delimiter;
					}
					text += list[i].ToString();
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000071B8 File Offset: 0x000053B8
		public static string IntListToCommaDelimiteredString(List<int> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				int num = list[i];
				bool flag = i > 0;
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(num.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000721C File Offset: 0x0000541C
		public static string IntArrayToCommaDelimiteredString(int[] list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Length; i++)
			{
				int num = list[i];
				bool flag = i > 0;
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(num.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007278 File Offset: 0x00005478
		public static string DataTableToString(DataTable t)
		{
			bool flag = t == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				string str = "";
				for (int i = 0; i < t.Columns.Count; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						str += " | ";
					}
					str += t.Columns[i].ColumnName;
				}
				str = str + Environment.NewLine + "==============================================";
				string text = "";
				for (int j = 0; j < t.Rows.Count; j++)
				{
					bool flag3 = j > 0;
					if (flag3)
					{
						text += Environment.NewLine;
					}
					for (int k = 0; k < t.Columns.Count; k++)
					{
						text += t.Rows[j][k].ToString().Trim().Replace("\n", " NEWLINE ");
					}
				}
				result = str + Environment.NewLine + text;
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000073AC File Offset: 0x000055AC
		public static void AssignIconToAppointment(db conn, int appId, int iconId)
		{
			conn.Da.SelectCommand.CommandText = "INSERT INTO appointmenticons ( appointmentid,screennum,iconnum) SELECT @appid AS appointmentid,-1 AS screennum,@iconnum AS iconnum  WHERE NOT EXISTS( SELECT appiconid FROM appointmenticons WHERE appointmentid=@appid AND iconnum=@iconnum)";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.AddWithValue("@appid", appId);
			conn.Da.SelectCommand.Parameters.AddWithValue("@iconnum", iconId);
			conn.Da.Fill(new DataTable());
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000743C File Offset: 0x0000563C
		public static byte[] StringToBytes(string txt, bool encrypt, IEncryption encryption)
		{
			byte[] result;
			if (encrypt)
			{
				result = encryption.Encrypt(txt);
			}
			else
			{
				Encoding encoding = (encryption != null) ? encryption.Encoder : new UTF8Encoding();
				result = encoding.GetBytes(txt);
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007478 File Offset: 0x00005678
		public static string BytesToString(byte[] bytes, bool decrypt, IEncryption encryption)
		{
			if (decrypt)
			{
				try
				{
					return encryption.Decrypt(bytes);
				}
				catch
				{
				}
			}
			Encoding encoding = (encryption != null) ? encryption.Encoder : new UTF8Encoding();
			return encoding.GetString(bytes);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000074CC File Offset: 0x000056CC
		public static int ObjectToInt(object o, int defaultIntValue)
		{
			bool flag = o == null;
			int result;
			if (flag)
			{
				result = defaultIntValue;
			}
			else
			{
				bool flag2 = o is string;
				if (flag2)
				{
					string text = (string)o;
					bool flag3 = text.Trim().Length < 1;
					if (flag3)
					{
						return defaultIntValue;
					}
					try
					{
						return int.Parse(text);
					}
					catch
					{
						return defaultIntValue;
					}
				}
				result = defaultIntValue;
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000753C File Offset: 0x0000573C
		public static int ObjectToInt(NameValueCollection appSettings, string keyName, int defaultIntValue)
		{
			object obj = appSettings[keyName];
			bool flag = obj == null;
			int result;
			if (flag)
			{
				result = defaultIntValue;
			}
			else
			{
				bool flag2 = obj is string;
				if (flag2)
				{
					string text = (string)obj;
					bool flag3 = text.Trim().Length < 1;
					if (flag3)
					{
						return defaultIntValue;
					}
					try
					{
						return int.Parse(text);
					}
					catch
					{
						return defaultIntValue;
					}
				}
				result = defaultIntValue;
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000075B4 File Offset: 0x000057B4
		public static string GenerateGuid(int uniqueNumber)
		{
			return uniqueNumber.ToString() + "-" + Guid.NewGuid().ToString().Substring(0, 9);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000075F4 File Offset: 0x000057F4
		public static bool IsEmailValid(string email)
		{
			Regex regex = new Regex("(?<user>[^@]+)@(?<host>.+)");
			Match match = regex.Match(email);
			return match.Success;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007620 File Offset: 0x00005820
		public static void GetSchoolYearStartEndDates(out DateTime startDate, out DateTime endDate)
		{
			DateTime now = DateTime.Now;
			int year = now.Year;
			int month = now.Month;
			int day = now.Day;
			bool flag = month >= 5;
			if (flag)
			{
				startDate = new DateTime(year, 5, 1);
				endDate = new DateTime(year + 1, 4, 30);
			}
			else
			{
				startDate = new DateTime(year - 1, 5, 1);
				endDate = new DateTime(year, 4, 30);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000769D File Offset: 0x0000589D
		public static void GetTermStartEndDates(out DateTime startDate, out DateTime endDate)
		{
			Core.GetTermStartEndDates(DateTime.Now, out startDate, out endDate);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000076B0 File Offset: 0x000058B0
		public static void GetTermStartEndDates(DateTime targetDate, out DateTime startDate, out DateTime endDate)
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			SessionView session = sessionClientManager.GetSession(targetDate);
			startDate = session.StartDate;
			endDate = session.EndDate;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000076E4 File Offset: 0x000058E4
		public static string GetLoginUrl()
		{
			return "~/custom/login/LoginS.aspx";
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000076FC File Offset: 0x000058FC
		public static string MinutesToTimeDescription(object minutes)
		{
			bool flag = minutes == null || minutes == DBNull.Value;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = Core.MinutesToTimeDescription((int)minutes);
			}
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007734 File Offset: 0x00005934
		public static string MinutesToTimeDescription(int minutes)
		{
			bool flag = minutes < 60;
			string result;
			if (flag)
			{
				result = minutes.ToString() + " minutes";
			}
			else
			{
				int num = minutes / 60;
				int num2 = minutes - num * 60;
				string text = num.ToString() + " hour";
				bool flag2 = num > 1;
				if (flag2)
				{
					text += "s";
				}
				bool flag3 = num2 > 0;
				if (flag3)
				{
					text = text + " " + num2.ToString() + " minutes";
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000077C4 File Offset: 0x000059C4
		public static string TruncateAndEncodeString(string s, int targetLength)
		{
			string s2 = Core.TruncateString(s, targetLength);
			return HttpUtility.HtmlEncode(s2);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000077E4 File Offset: 0x000059E4
		public static string TruncateString(string s, int targetLength)
		{
			bool flag = s.Length <= targetLength;
			string result;
			if (flag)
			{
				result = s;
			}
			else
			{
				result = s.Substring(0, targetLength) + "...";
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007820 File Offset: 0x00005A20
		public static db GetConn()
		{
			return new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007844 File Offset: 0x00005A44
		public static bool ParseBooleanAttribute(string s, bool defaultValue)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				bool flag2 = s.Equals("0");
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = s.Equals("1");
					if (flag3)
					{
						result = true;
					}
					else
					{
						bool flag5;
						bool flag4 = bool.TryParse(s, out flag5);
						if (flag4)
						{
							result = flag5;
						}
						else
						{
							result = defaultValue;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x02000088 RID: 136
		public enum TestBookingWizardStep
		{
			// Token: 0x04000364 RID: 868
			Unknown,
			// Token: 0x04000365 RID: 869
			Welcome,
			// Token: 0x04000366 RID: 870
			SelectCourse,
			// Token: 0x04000367 RID: 871
			ConfirmInstructorInfo,
			// Token: 0x04000368 RID: 872
			ChooseAccommodations,
			// Token: 0x04000369 RID: 873
			SelectDateTime,
			// Token: 0x0400036A RID: 874
			ConfirmBooking,
			// Token: 0x0400036B RID: 875
			BookingSummary,
			// Token: 0x0400036C RID: 876
			Cancel
		}
	}
}
