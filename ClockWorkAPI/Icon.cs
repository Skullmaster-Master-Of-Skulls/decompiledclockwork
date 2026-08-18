using System;
using System.Collections;
using System.Data;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class Icon : IComparable
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002ED0 File Offset: 0x00001ED0
		public Icon(int _IconID, int _ScreenNum, DataTable _iconInfo)
		{
			this.iconID = _IconID;
			this.screenNum = _ScreenNum;
			this.iconInfo = _iconInfo;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002F04 File Offset: 0x00001F04
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002F1C File Offset: 0x00001F1C
		public int IconID
		{
			get
			{
				return this.iconID;
			}
			set
			{
				this.iconID = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002F28 File Offset: 0x00001F28
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002F40 File Offset: 0x00001F40
		public int ScreenNum
		{
			get
			{
				return this.screenNum;
			}
			set
			{
				this.screenNum = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002F4C File Offset: 0x00001F4C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003024 File Offset: 0x00002024
		public string IconText
		{
			get
			{
				if (this.iconInfo != null)
				{
					foreach (object obj in this.iconInfo.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int num = (int)dataRow[1];
						if (num == this.iconID)
						{
							if (dataRow[2] == DBNull.Value)
							{
								return "";
							}
							return dataRow[2].ToString().Trim();
						}
					}
				}
				return this.iconDescription;
			}
			set
			{
				this.iconDescription = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003030 File Offset: 0x00002030
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000312C File Offset: 0x0000212C
		public char IconLetterIdentifier
		{
			get
			{
				if (this.iconInfo != null)
				{
					foreach (object obj in this.iconInfo.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int num = (int)dataRow[1];
						if (num == this.iconID)
						{
							if (dataRow[3] == DBNull.Value)
							{
								return '?';
							}
							string text = (string)dataRow[3];
							if (text.Length > 0)
							{
								return text[0];
							}
							return '?';
						}
					}
				}
				return this.iconLetter;
			}
			set
			{
				this.iconLetter = value;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003138 File Offset: 0x00002138
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003154 File Offset: 0x00002154
		public static Icon GetNewIcon(DataTable iconInfo, char _IconLetterIdentifier)
		{
			if (iconInfo != null)
			{
				foreach (object obj in iconInfo.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Deleted && dataRow[3] != DBNull.Value)
					{
						string text = (string)dataRow[3];
						if (text.IndexOf(_IconLetterIdentifier) >= 0)
						{
							int num = (int)dataRow[1];
							return new Icon(num, -1, iconInfo);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000322C File Offset: 0x0000222C
		public static bool ContainsIcon(ArrayList icons, char _IconLetterIdentifier)
		{
			foreach (object obj in icons)
			{
				Icon icon = (Icon)obj;
				if (icon.IconLetterIdentifier == _IconLetterIdentifier)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000032A4 File Offset: 0x000022A4
		public static string GetDescription(DataTable iconInfo, int imageIndex)
		{
			foreach (object obj in iconInfo.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					int num = (int)dataRow[1];
					if (num == imageIndex)
					{
						string str;
						if (dataRow[3] != DBNull.Value)
						{
							str = "[" + dataRow[3].ToString().Trim() + "] ";
						}
						else
						{
							str = "";
						}
						return str + dataRow[2].ToString().Trim();
					}
				}
			}
			return "?";
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000033A4 File Offset: 0x000023A4
		public static DataRow GetIconInfoDataRow(DataTable iconInfo, int imageIndex)
		{
			DataRow result;
			if (iconInfo == null)
			{
				result = null;
			}
			else
			{
				foreach (object obj in iconInfo.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Deleted)
					{
						int num = (int)dataRow[1];
						if (num == imageIndex)
						{
							return dataRow;
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000344C File Offset: 0x0000244C
		public static bool AddIconToAppointment(UnivDataAdapter da, int appId, int iconNum)
		{
			bool result;
			if (appId > 0 && iconNum > 0)
			{
				string commandText = "INSERT INTO appointmenticons (appointmentid,screennum,iconnum) SELECT @appid,-1,@iconnum WHERE NOT EXISTS(SELECT appiconid FROM appointmenticons WHERE appointmentid=@appid AND iconnum=@iconnum)";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@appid", appId);
				da.SelectCommand.Parameters.Add("@iconnum", iconNum);
				string value;
				da.Fill(new DataTable(), out value);
				result = string.IsNullOrEmpty(value);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000034E8 File Offset: 0x000024E8
		public int CompareTo(object obj)
		{
			if (obj is Icon)
			{
				Icon icon = (Icon)obj;
				int result;
				if (icon.IconID == this.IconID)
				{
					result = 0;
				}
				else
				{
					result = this.IconID.CompareTo(icon.IconID);
				}
				return result;
			}
			throw new Exception("Trying to compare to an object that is not type=Scheduler.Icon!");
		}

		// Token: 0x04000027 RID: 39
		private int iconID;

		// Token: 0x04000028 RID: 40
		private int screenNum;

		// Token: 0x04000029 RID: 41
		private DataTable iconInfo;

		// Token: 0x0400002A RID: 42
		private string iconDescription = "";

		// Token: 0x0400002B RID: 43
		private char iconLetter = ' ';
	}
}
