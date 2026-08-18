using System;
using System.Data;
using System.Text;
using EncryptionClassLibrary;
using UnivOleDb;

namespace DynamicScreens
{
	// Token: 0x0200000B RID: 11
	public class Accommodation
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00007556 File Offset: 0x00006556
		public Accommodation()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007561 File Offset: 0x00006561
		public Accommodation(DataRow AccommodationsDR)
		{
			this.dr = AccommodationsDR;
			this.SetPrivateVariables(this.dr);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007580 File Offset: 0x00006580
		public Accommodation(DataTable accommodationsTable, int controlID)
		{
			DataRow privateVariables = null;
			foreach (object obj in accommodationsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[1];
				if (num == controlID)
				{
					privateVariables = dataRow;
					break;
				}
			}
			this.SetPrivateVariables(privateVariables);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007618 File Offset: 0x00006618
		public Accommodation(int _courseID, DataRow dynamicDataRow, DataTable accommodationInfo, DataSet ComboBoxData, UnivDataAdapter DA, TripleDESEncryptionClass TripleDES)
		{
			this.comboBoxData = ComboBoxData;
			this.da = DA;
			this.tripleDES = TripleDES;
			this.controlID = (int)dynamicDataRow[3];
			DataRow privateVariables = null;
			foreach (object obj in accommodationInfo.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[1];
				if (num == this.controlID)
				{
					privateVariables = dataRow;
					break;
				}
			}
			this.SetPrivateVariables(privateVariables);
			this.dataValue = this.GetAccommodationValue(dynamicDataRow);
			this.controlID = (int)dynamicDataRow[3];
			this.courseID = _courseID;
			this.description = (string)dynamicDataRow[1];
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00007718 File Offset: 0x00006718
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00007730 File Offset: 0x00006730
		public bool ShowOnLetter
		{
			get
			{
				return this.showOnLetter;
			}
			set
			{
				this.showOnLetter = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000773C File Offset: 0x0000673C
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00007754 File Offset: 0x00006754
		public bool ShowOnEmail
		{
			get
			{
				return this.showOnEmail;
			}
			set
			{
				this.showOnEmail = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00007760 File Offset: 0x00006760
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00007778 File Offset: 0x00006778
		public bool ExtraTime
		{
			get
			{
				return this.extraTime;
			}
			set
			{
				this.extraTime = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00007784 File Offset: 0x00006784
		public int ExtraTimeMinutesPerHour
		{
			get
			{
				int result;
				if (this.extraTime)
				{
					result = Accommodation.ExtractNumber(this.dataValue);
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000077B4 File Offset: 0x000067B4
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000077CC File Offset: 0x000067CC
		public bool IsAlone
		{
			get
			{
				return this.isAlone;
			}
			set
			{
				this.isAlone = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000077D8 File Offset: 0x000067D8
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000077F0 File Offset: 0x000067F0
		public bool NeedsComputer
		{
			get
			{
				return this.needsComputer;
			}
			set
			{
				this.needsComputer = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000077FC File Offset: 0x000067FC
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00007814 File Offset: 0x00006814
		public bool NeedsReaderScribe
		{
			get
			{
				return this.needsReaderScribe;
			}
			set
			{
				this.needsReaderScribe = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00007820 File Offset: 0x00006820
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00007838 File Offset: 0x00006838
		public bool AvailableInAllRooms
		{
			get
			{
				return this.availableInAllRooms;
			}
			set
			{
				this.availableInAllRooms = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00007844 File Offset: 0x00006844
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000785C File Offset: 0x0000685C
		public int GroupID
		{
			get
			{
				return this.groupID;
			}
			set
			{
				this.groupID = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00007868 File Offset: 0x00006868
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00007880 File Offset: 0x00006880
		public bool IsGroup
		{
			get
			{
				return this.isGroup;
			}
			set
			{
				this.isGroup = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000788C File Offset: 0x0000688C
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000078A4 File Offset: 0x000068A4
		public bool TapedExams
		{
			get
			{
				return this.tapedExams;
			}
			set
			{
				this.tapedExams = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000078B0 File Offset: 0x000068B0
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000078C8 File Offset: 0x000068C8
		public bool Other
		{
			get
			{
				return this.other;
			}
			set
			{
				this.other = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000078D4 File Offset: 0x000068D4
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000078EC File Offset: 0x000068EC
		public bool Enlarged
		{
			get
			{
				return this.enlarged;
			}
			set
			{
				this.enlarged = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000078F8 File Offset: 0x000068F8
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00007910 File Offset: 0x00006910
		public string DataValue
		{
			get
			{
				return this.dataValue;
			}
			set
			{
				this.dataValue = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000791C File Offset: 0x0000691C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00007934 File Offset: 0x00006934
		public string LongDescription
		{
			get
			{
				return this.longDescription;
			}
			set
			{
				this.longDescription = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00007940 File Offset: 0x00006940
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00007958 File Offset: 0x00006958
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00007964 File Offset: 0x00006964
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000797C File Offset: 0x0000697C
		public string ShortCode
		{
			get
			{
				return this.shortCode;
			}
			set
			{
				this.shortCode = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00007988 File Offset: 0x00006988
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000079A0 File Offset: 0x000069A0
		public int CourseID
		{
			get
			{
				return this.courseID;
			}
			set
			{
				this.courseID = value;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000079AC File Offset: 0x000069AC
		public string GetAccommodationValue(DataRow dr)
		{
			int num = (int)dr[0];
			string result;
			if (num == 3)
			{
				result = this.GetComboText(dr);
			}
			else if (num == 14)
			{
				result = this.GetRadioGroupText(dr);
			}
			else if (num == 6)
			{
				if (dr[4] != DBNull.Value)
				{
					result = ((DateTime)dr[4]).ToLongDateString();
				}
				else
				{
					result = "";
				}
			}
			else if (num == 100)
			{
				result = "";
			}
			else if (num == 10)
			{
				result = "";
			}
			else if (num == 1)
			{
				result = this.GetTextBoxText(dr);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007A7C File Offset: 0x00006A7C
		private string GetRadioGroupText(DataRow dr)
		{
			int lookupGroupID = (int)dr[7];
			DataTable lookupList = DynamicScreen.GetLookupList(lookupGroupID, false, -1, ref this.comboBoxData, this.da, false);
			int lookupListID = (int)dr[4];
			return DynamicScreen.GetLookupListValue(lookupList, lookupListID);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007ACC File Offset: 0x00006ACC
		private string GetComboText(DataRow dr)
		{
			int num = (int)dr[9];
			string result;
			if (num == 0)
			{
				int lookupGroupID = (int)dr[7];
				DataTable lookupList = DynamicScreen.GetLookupList(lookupGroupID, false, -1, ref this.comboBoxData, this.da, false);
				int lookupListID = (int)dr[4];
				string lookupListValue = DynamicScreen.GetLookupListValue(lookupList, lookupListID);
				result = lookupListValue;
			}
			else if (num < 0)
			{
				byte[] inputInBytes = (byte[])dr[4];
				string text = this.tripleDES.Decrypt(inputInBytes);
				result = text;
			}
			else
			{
				byte[] bytes = (byte[])dr[4];
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				string text = utf8Encoding.GetString(bytes);
				result = text;
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007B90 File Offset: 0x00006B90
		private string GetTextBoxText(DataRow dr)
		{
			int num = (int)dr[9];
			bool flag = num != 0;
			byte[] array = (byte[])dr[4];
			string result;
			if (!flag)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				string text = utf8Encoding.GetString(array);
				result = text;
			}
			else
			{
				string text = this.tripleDES.Decrypt(array);
				result = text;
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007BF8 File Offset: 0x00006BF8
		public static int ExtractNumber(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				if (char.IsNumber(c))
				{
					text += c;
				}
			}
			if (text.Length < 1)
			{
				text = "0";
			}
			return int.Parse(text);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007C74 File Offset: 0x00006C74
		private void SetPrivateVariables(DataRow dr)
		{
			if (dr == null)
			{
				this.SetDefaultVariables();
			}
			else
			{
				this.accommodationID = (int)dr[0];
				this.controlID = (int)dr[1];
				this.longDescription = dr[2].ToString().Trim();
				this.shortCode = dr[3].ToString().Trim();
				this.showOnLetter = Convert.ToBoolean(dr[4]);
				this.showOnEmail = Convert.ToBoolean(dr[5]);
				this.extraTime = Convert.ToBoolean(dr[6]);
				this.isAlone = Convert.ToBoolean(dr[7]);
				this.needsComputer = Convert.ToBoolean(dr[8]);
				this.needsReaderScribe = Convert.ToBoolean(dr[9]);
				this.availableInAllRooms = Convert.ToBoolean(dr[10]);
				this.groupID = -1;
				this.isGroup = Convert.ToBoolean(dr[12]);
				this.tapedExams = Convert.ToBoolean(dr[13]);
				this.other = Convert.ToBoolean(dr[14]);
				this.enlarged = Convert.ToBoolean(dr[15]);
				this.dataValue = "";
				this.courseID = -1;
				this.description = "";
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007DE0 File Offset: 0x00006DE0
		private void SetDefaultVariables()
		{
			this.accommodationID = -1;
			this.controlID = -1;
			this.longDescription = "";
			this.shortCode = "";
			this.showOnLetter = false;
			this.showOnEmail = false;
			this.extraTime = false;
			this.isAlone = false;
			this.needsComputer = false;
			this.needsReaderScribe = false;
			this.availableInAllRooms = false;
			this.groupID = -1;
			this.isGroup = false;
			this.tapedExams = false;
			this.other = false;
			this.enlarged = false;
			this.dataValue = "";
			this.courseID = -1;
		}

		// Token: 0x0400005B RID: 91
		private DataSet comboBoxData;

		// Token: 0x0400005C RID: 92
		private UnivDataAdapter da;

		// Token: 0x0400005D RID: 93
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400005E RID: 94
		private DataRow dr;

		// Token: 0x0400005F RID: 95
		private int courseID;

		// Token: 0x04000060 RID: 96
		private int controlID;

		// Token: 0x04000061 RID: 97
		private int accommodationID;

		// Token: 0x04000062 RID: 98
		private int groupID;

		// Token: 0x04000063 RID: 99
		private string longDescription;

		// Token: 0x04000064 RID: 100
		private string shortCode;

		// Token: 0x04000065 RID: 101
		private string description;

		// Token: 0x04000066 RID: 102
		private bool showOnLetter;

		// Token: 0x04000067 RID: 103
		private bool showOnEmail;

		// Token: 0x04000068 RID: 104
		private bool isAlone;

		// Token: 0x04000069 RID: 105
		private bool isGroup;

		// Token: 0x0400006A RID: 106
		private bool needsComputer;

		// Token: 0x0400006B RID: 107
		private bool needsReaderScribe;

		// Token: 0x0400006C RID: 108
		private bool extraTime;

		// Token: 0x0400006D RID: 109
		private bool tapedExams;

		// Token: 0x0400006E RID: 110
		private bool enlarged;

		// Token: 0x0400006F RID: 111
		private bool other;

		// Token: 0x04000070 RID: 112
		private bool availableInAllRooms;

		// Token: 0x04000071 RID: 113
		private string dataValue;
	}
}
