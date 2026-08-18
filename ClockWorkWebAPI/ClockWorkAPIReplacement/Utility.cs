using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EncryptionClassLibrary;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000075 RID: 117
	public class Utility
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x00028068 File Offset: 0x00026268
		public static List<int> IntListFromString(string commaSeparatedNumbers)
		{
			List<int> list = new List<int>();
			bool flag = commaSeparatedNumbers == null;
			List<int> result;
			if (flag)
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
					bool flag2 = !string.IsNullOrEmpty(text2);
					if (flag2)
					{
						int item;
						bool flag3 = int.TryParse(text2, out item);
						if (flag3)
						{
							list.Add(item);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000280F4 File Offset: 0x000262F4
		public static bool IntToBool(int i)
		{
			return i != 0;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002810C File Offset: 0x0002630C
		public static int BoolToInt(bool b)
		{
			int result;
			if (b)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00028128 File Offset: 0x00026328
		public static StringDictionary ParseArgs(string args, char delimiter)
		{
			return Utility.ParseArgs(args, new char[]
			{
				delimiter
			});
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0002814C File Offset: 0x0002634C
		public static StringDictionary ParseArgs(string args, char[] delimiter)
		{
			string[] array = args.Split(delimiter);
			StringDictionary stringDictionary = new StringDictionary();
			foreach (string text in array)
			{
				bool flag = text.Trim().Length > 0;
				if (flag)
				{
					int num = text.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						stringDictionary.Add(text.Substring(0, num), text.Substring(num + 1));
					}
					else
					{
						stringDictionary.Add(text, "");
					}
				}
			}
			return stringDictionary;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000281E0 File Offset: 0x000263E0
		public static byte[] ExtractImageBytes(byte[] dbBytes, out string fileName)
		{
			byte[] result;
			try
			{
				int num = 6;
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = dbBytes[i];
				}
				string s = Utility.BytesToString(array, false, null);
				int num2 = int.Parse(s);
				byte[] array2 = new byte[num2];
				for (int j = 0; j < num2; j++)
				{
					array2[j] = dbBytes[j + num];
				}
				string args = Utility.BytesToString(array2, false, null);
				StringDictionary stringDictionary = Utility.ParseArgs(args, ';');
				fileName = stringDictionary["filename"];
				string text = fileName;
				int num3 = dbBytes.Length - num - num2;
				byte[] array3 = new byte[num3];
				for (int k = 0; k < array3.Length; k++)
				{
					array3[k] = dbBytes[k + num2 + num];
				}
				result = array3;
			}
			catch (Exception ex)
			{
				fileName = "";
				result = new byte[0];
			}
			return result;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000282E4 File Offset: 0x000264E4
		public static string BytesToString(byte[] bytes, bool decrypt, IEncryption tripleDES)
		{
			string result;
			if (decrypt)
			{
				result = tripleDES.Decrypt(bytes);
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetString(bytes);
			}
			return result;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00028314 File Offset: 0x00026514
		public static string BytesToString(DataRow dr, string colName, bool decrypt, IEncryption tripleDES)
		{
			object obj = dr[colName];
			bool flag = obj == DBNull.Value;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = obj is byte[];
				if (flag2)
				{
					result = Utility.BytesToString((byte[])obj, decrypt, tripleDES);
				}
				else
				{
					result = "";
				}
			}
			return result;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00028368 File Offset: 0x00026568
		public static string BytesToPlainText(byte[] bytes, IEncryption tripleDES)
		{
			string text = tripleDES.Decrypt(bytes);
			bool flag = text.StartsWith("{rtf");
			if (flag)
			{
				using (RichTextBox richTextBox = new RichTextBox())
				{
					richTextBox.Rtf = text;
					text = richTextBox.Text;
				}
			}
			return text;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000283C8 File Offset: 0x000265C8
		public static void GetTermStartEndDates(out DateTime startDate, out DateTime endDate)
		{
			Utility.GetTermStartEndDates(DateTime.Now, out startDate, out endDate);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000283D8 File Offset: 0x000265D8
		public static void GetTermStartEndDates(DateTime middleDate, out DateTime startDate, out DateTime endDate)
		{
			int year = middleDate.Year;
			int month = middleDate.Month;
			int day = middleDate.Day;
			bool flag = month <= 3 || (month == 4 && day < 20) || (month == 12 && day > 20);
			if (flag)
			{
				startDate = new DateTime(year, 1, 1);
				endDate = new DateTime(year, 4, 30);
			}
			else
			{
				bool flag2 = month <= 7 || (month == 8 && day < 20);
				if (flag2)
				{
					startDate = new DateTime(year, 5, 1);
					endDate = new DateTime(year, 8, 30);
				}
				else
				{
					startDate = new DateTime(year, 9, 1);
					endDate = new DateTime(year, 12, 31);
				}
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0002849C File Offset: 0x0002669C
		public static string ListToString(List<int> numbers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < numbers.Count; i++)
			{
				bool flag = i > 0;
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(numbers[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00028500 File Offset: 0x00026700
		public static string ListToString(List<DateTime> dates)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < dates.Count; i++)
			{
				DateTime dateTime = dates[i];
				bool flag = i > 0;
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(dateTime.ToString("dddd MMMM d, yyyy"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00028568 File Offset: 0x00026768
		public static string ListToString(List<string> strings)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < strings.Count; i++)
			{
				string value = strings[i];
				bool flag = i > 0;
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000285C8 File Offset: 0x000267C8
		public static string base64Encode(byte[] binaryData)
		{
			string result;
			try
			{
				string text = Convert.ToBase64String(binaryData);
				result = text;
			}
			catch (Exception innerException)
			{
				throw new Exception("Error in base64Encode", innerException);
			}
			return result;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00028604 File Offset: 0x00026804
		public static byte[] base64Decode(string data)
		{
			byte[] result;
			try
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				Decoder decoder = utf8Encoding.GetDecoder();
				byte[] array = Convert.FromBase64String(data);
				result = array;
			}
			catch (Exception ex)
			{
				throw new Exception("Error in base64Decode" + ex.Message);
			}
			return result;
		}

		// Token: 0x020000AC RID: 172
		public struct Cols
		{
			// Token: 0x040003EE RID: 1006
			public const int CONTROLLIST_controlID = 0;

			// Token: 0x040003EF RID: 1007
			public const int CONTROLLIST_screenNum = 1;

			// Token: 0x040003F0 RID: 1008
			public const int CONTROLLIST_controlCode = 2;

			// Token: 0x040003F1 RID: 1009
			public const int CONTROLLIST_controlCaption = 3;

			// Token: 0x040003F2 RID: 1010
			public const int CONTROLLIST_setting1 = 4;

			// Token: 0x040003F3 RID: 1011
			public const int CONTROLLIST_setting2 = 5;

			// Token: 0x040003F4 RID: 1012
			public const int CONTROLLIST_setting3 = 6;

			// Token: 0x040003F5 RID: 1013
			public const int CONTROLLIST_defaultValue = 7;

			// Token: 0x040003F6 RID: 1014
			public const int CONTROLLIST_ControlName = 8;

			// Token: 0x040003F7 RID: 1015
			public const int CONTROLLIST_ControlGroup = 9;

			// Token: 0x040003F8 RID: 1016
			public const int CONTROLLIST_HelpText = 10;

			// Token: 0x040003F9 RID: 1017
			public const int CONTROLLIST_HelpTextDisplayMethod = 11;

			// Token: 0x040003FA RID: 1018
			public const int CONTROLLIST_Mask = 12;

			// Token: 0x040003FB RID: 1019
			public const int CONTROLLIST_Enforce = 13;

			// Token: 0x040003FC RID: 1020
			public const int CONTROLLIST_ActionHandlers = 14;

			// Token: 0x040003FD RID: 1021
			public const int CONTROLLIST_DefaultValueString = 15;

			// Token: 0x040003FE RID: 1022
			public const int CONTROLLIST_Setting4String = 16;

			// Token: 0x040003FF RID: 1023
			public const int CONTROLLIST_enabled = 17;

			// Token: 0x04000400 RID: 1024
			public const int CONTROLLIST_readOnly = 18;

			// Token: 0x04000401 RID: 1025
			public const int CONTROLLIST_hideCaption = 19;

			// Token: 0x04000402 RID: 1026
			public const int CONTROLLIST_Setting4 = 20;

			// Token: 0x04000403 RID: 1027
			public const int CONTROLLIST_FontSize = 21;

			// Token: 0x04000404 RID: 1028
			public const int CONTROLLIST_DontWrapToNextLine = 22;

			// Token: 0x04000405 RID: 1029
			public const int DATATABLES_dataID = 0;

			// Token: 0x04000406 RID: 1030
			public const int DATATABLES_screenNum = 1;

			// Token: 0x04000407 RID: 1031
			public const int DATATABLES_personID = 2;

			// Token: 0x04000408 RID: 1032
			public const int DATATABLES_controlID = 3;

			// Token: 0x04000409 RID: 1033
			public const int DATATABLES_controlValue = 4;

			// Token: 0x0400040A RID: 1034
			public const int DATATABLES_appointmentID = 5;

			// Token: 0x0400040B RID: 1035
			public const int DATATABLES_courseid = 5;

			// Token: 0x0400040C RID: 1036
			public const int DATATABLES_flavour = 6;

			// Token: 0x0400040D RID: 1037
			public const int LOOKUPLIST_lookupListID = 0;

			// Token: 0x0400040E RID: 1038
			public const int LOOKUPLIST_lookupGroupID = 1;

			// Token: 0x0400040F RID: 1039
			public const int LOOKUPLIST_lookupText = 2;
		}

		// Token: 0x020000AD RID: 173
		public struct ControlCodes
		{
			// Token: 0x04000410 RID: 1040
			public const int _textBox = 1;

			// Token: 0x04000411 RID: 1041
			public const int _checkBox = 2;

			// Token: 0x04000412 RID: 1042
			public const int _comboBox = 3;

			// Token: 0x04000413 RID: 1043
			public const int _radioButton = 4;

			// Token: 0x04000414 RID: 1044
			public const int _label = 5;

			// Token: 0x04000415 RID: 1045
			public const int _date = 6;

			// Token: 0x04000416 RID: 1046
			public const int _time = 7;

			// Token: 0x04000417 RID: 1047
			public const int _horizontalRule = 8;

			// Token: 0x04000418 RID: 1048
			public const int _blankSpace = 9;

			// Token: 0x04000419 RID: 1049
			public const int _listView = 10;

			// Token: 0x0400041A RID: 1050
			public const int _myCheckBox = 12;

			// Token: 0x0400041B RID: 1051
			public const int _myTextBox = 11;

			// Token: 0x0400041C RID: 1052
			public const int _indent = 13;

			// Token: 0x0400041D RID: 1053
			public const int _radioGroup = 14;

			// Token: 0x0400041E RID: 1054
			public const int _fileList = 20;

			// Token: 0x0400041F RID: 1055
			public const int _picture = 21;

			// Token: 0x04000420 RID: 1056
			public const int _dynamicTable = 25;

			// Token: 0x04000421 RID: 1057
			public const int _panelStart = 30;

			// Token: 0x04000422 RID: 1058
			public const int _panelClose = 31;

			// Token: 0x04000423 RID: 1059
			public const int _tabControlStart = 32;

			// Token: 0x04000424 RID: 1060
			public const int _tabPageStart = 33;

			// Token: 0x04000425 RID: 1061
			public const int _tabPageClose = 34;

			// Token: 0x04000426 RID: 1062
			public const int _tabControlClose = 35;

			// Token: 0x04000427 RID: 1063
			public const int _tableControl = 40;

			// Token: 0x04000428 RID: 1064
			public const int _columnBreak = 50;

			// Token: 0x04000429 RID: 1065
			public const int _staffComboBox = 100;

			// Token: 0x0400042A RID: 1066
			public const int _schoolYearChooser = 200;

			// Token: 0x0400042B RID: 1067
			public const int _maskedTextBox = 300;

			// Token: 0x0400042C RID: 1068
			public const int _listSelect = 301;

			// Token: 0x0400042D RID: 1069
			public const int _file = 400;

			// Token: 0x0400042E RID: 1070
			public const int _multiCheckBox = 500;

			// Token: 0x0400042F RID: 1071
			public const int _multiCheckBoxText = 510;

			// Token: 0x04000430 RID: 1072
			public const int _multiCheckBoxDropList = 520;

			// Token: 0x04000431 RID: 1073
			public const int _multiLabelHeader = 530;

			// Token: 0x04000432 RID: 1074
			public const int _rtfTextBox = 600;

			// Token: 0x04000433 RID: 1075
			public const int _multiLineTextBox = 620;

			// Token: 0x04000434 RID: 1076
			public const int _accommodationCheckbox = 700;

			// Token: 0x04000435 RID: 1077
			public const int _accommodationTextbox = 701;

			// Token: 0x04000436 RID: 1078
			public const int _accommodationDatePicker = 702;

			// Token: 0x04000437 RID: 1079
			public const int _accommodationDropList = 703;

			// Token: 0x04000438 RID: 1080
			public const int _FormSettings = 800;

			// Token: 0x04000439 RID: 1081
			public const int _dynamicControlsChooser = 801;

			// Token: 0x0400043A RID: 1082
			public const int _multiDatabaseItemChooser = 802;

			// Token: 0x0400043B RID: 1083
			public const int _infoDisplayBox = 803;

			// Token: 0x0400043C RID: 1084
			public const int _calcButton = 804;

			// Token: 0x0400043D RID: 1085
			public const int _PMTable = 805;

			// Token: 0x0400043E RID: 1086
			public const int _caseComboBox = 806;

			// Token: 0x0400043F RID: 1087
			public const int _emailHistory = 807;

			// Token: 0x04000440 RID: 1088
			public const int _appointmentHistory = 808;

			// Token: 0x04000441 RID: 1089
			public const int VerticalPadTextBox = 6;

			// Token: 0x04000442 RID: 1090
			public const int VerticalPadComboBox = 6;

			// Token: 0x04000443 RID: 1091
			public const int VerticalPadRadioButton = 2;

			// Token: 0x04000444 RID: 1092
			public const int VerticalPadCheckBox = 2;

			// Token: 0x04000445 RID: 1093
			public const int VerticalPadLabel = 2;

			// Token: 0x04000446 RID: 1094
			public const int VerticalPadDateTimePicker = 4;

			// Token: 0x04000447 RID: 1095
			public const int _unknown = 999999;
		}
	}
}
