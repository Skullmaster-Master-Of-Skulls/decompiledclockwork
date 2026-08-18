using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DynamicScreens;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x020000A3 RID: 163
	public class Accommodation
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00030AEC File Offset: 0x0002FAEC
		public int DataId
		{
			get
			{
				return this.dataId;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00030B04 File Offset: 0x0002FB04
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00030B1C File Offset: 0x0002FB1C
		public int Lucid
		{
			get
			{
				return this.lucid;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00030B34 File Offset: 0x0002FB34
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00030B4C File Offset: 0x0002FB4C
		public string ControlCaption
		{
			get
			{
				return this.controlCaption;
			}
			set
			{
				this.controlCaption = value;
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00030B58 File Offset: 0x0002FB58
		public string ToStringHtml(bool useAccommodationsApprovalSystem, bool includePrivateNote)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (useAccommodationsApprovalSystem)
			{
				if (this.approved)
				{
					stringBuilder.Append("[Approved] ");
				}
				else
				{
					stringBuilder.Append("[Awaiting approval] ");
				}
			}
			string valueString = this.GetValueString(includePrivateNote);
			stringBuilder.Append(this.ControlCaptionForDisplay);
			if (!string.IsNullOrEmpty(valueString))
			{
				stringBuilder.Append(": ");
				stringBuilder.Append(valueString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00030BDC File Offset: 0x0002FBDC
		public string ToString(bool useAccommodationsApprovalSystem, bool includePrivateNote)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (useAccommodationsApprovalSystem)
			{
				if (this.approved)
				{
					stringBuilder.Append("[Approved] ");
				}
				else
				{
					stringBuilder.Append("[Awaiting approval] ");
				}
			}
			string valueString = this.GetValueString(includePrivateNote);
			if (!string.IsNullOrEmpty(valueString))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(": ");
				}
				stringBuilder.Append(valueString);
			}
			else
			{
				stringBuilder.Append(this.ControlCaptionForDisplay);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00030C74 File Offset: 0x0002FC74
		public string ControlCaptionForDisplay
		{
			get
			{
				string text;
				if (!string.IsNullOrEmpty(this.longDescription.Trim()))
				{
					text = this.longDescription;
				}
				else if (this.controlCaption.IndexOf("~~") >= 0)
				{
					int length = this.controlCaption.IndexOf("~~");
					text = this.controlCaption.Substring(0, length);
				}
				else
				{
					text = this.controlCaption;
				}
				string result;
				if (string.IsNullOrEmpty(this.altLongDescription.Trim()))
				{
					result = text;
				}
				else
				{
					result = string.Format("{0}: {1}", text, this.altLongDescription);
				}
				return result;
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00030D14 File Offset: 0x0002FD14
		private string GetValueString(bool includePrivateNote)
		{
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.bbi != null && this.bbi.Length > 0)
			{
				string text = tripleDES.Decrypt(this.bbi);
				if (text.StartsWith("{\\rtf1"))
				{
					using (RichTextBox richTextBox = new RichTextBox())
					{
						richTextBox.Rtf = text;
						stringBuilder.Append(richTextBox.Text);
					}
				}
				else
				{
					stringBuilder.Append(text);
				}
			}
			else if (this.bb != null && this.bb.Length > 0)
			{
				stringBuilder.Append(ClockWorkCore.BytesToString(this.bb, this.valBytesIsEncrypted, tripleDES));
			}
			else if (this.controlCode != 2 && this.controlCode != 700)
			{
				stringBuilder.Append(this.valText);
			}
			if (this.expiryDate != null)
			{
				stringBuilder.Append(" [expires on ");
				stringBuilder.Append(this.expiryDate.Value.ToString("yyyy-MM-dd"));
				stringBuilder.Append("]");
			}
			if (this.recommendedButDeclined)
			{
				stringBuilder.Append(" [recommended but declined]");
			}
			if (!string.IsNullOrEmpty(this.rationale))
			{
				stringBuilder.Append(" [rationale: ");
				stringBuilder.Append(this.rationale);
				stringBuilder.Append("]");
			}
			if (includePrivateNote && !string.IsNullOrEmpty(this.note))
			{
				stringBuilder.Append(" [private note: ");
				stringBuilder.Append(this.note);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00030F20 File Offset: 0x0002FF20
		public Accommodation(int dataId, int controlId, string controlCaption, int controlCode, int lucid, string valText, bool valBytesIsEncrypted, byte[] bb, byte[] bbi, string longDescription, bool approved, int showOnLetter, DateTime? expiryDate, string altLongDescription, string note, bool recommendedButDeclined, string rationale)
		{
			this.dataId = dataId;
			this.controlId = controlId;
			this.controlCaption = controlCaption;
			this.longDescription = longDescription;
			this.controlCode = controlCode;
			this.lucid = lucid;
			this.valText = valText;
			this.valBytesIsEncrypted = valBytesIsEncrypted;
			this.bb = bb;
			this.bbi = bbi;
			this.approved = approved;
			this.showOnLetter = showOnLetter;
			this.expiryDate = expiryDate;
			this.altLongDescription = altLongDescription;
			this.note = note;
			this.recommendedButDeclined = recommendedButDeclined;
			this.rationale = rationale;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00030FBC File Offset: 0x0002FFBC
		public Accommodation(DataRow dr)
		{
			this.controlId = (int)dr["controlid"];
			this.controlCaption = dr["controlcaption"].ToString();
			this.dataId = ((dr["dataid"] == DBNull.Value) ? 0 : ((int)dr["dataid"]));
			this.controlCode = ((dr["controlcode"] == DBNull.Value) ? 0 : ((int)dr["controlcode"]));
			this.valBytesIsEncrypted = (dr["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dr["valbytesisencrypted"]));
			this.bb = ((dr["valbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dr["valbytes"]));
			this.bbi = ((dr["valimage"] == DBNull.Value) ? new byte[0] : ((byte[])dr["valimage"]));
			this.lucid = ((dr["courseid"] == DBNull.Value) ? 0 : ((int)dr["courseid"]));
			this.valText = dr["valtext"].ToString();
			this.longDescription = dr["longdescription"].ToString();
			DataTable table = dr.Table;
			this.approved = (table.Columns.Contains("approved") && dr["approved"] != DBNull.Value && Convert.ToBoolean(dr["approved"]));
			this.showOnLetter = ((!table.Columns.Contains("showonletter") || dr["showonletter"] == DBNull.Value) ? 0 : ((int)dr["showonletter"]));
			this.expiryDate = ((!table.Columns.Contains("expirydate") || dr["expirydate"] == DBNull.Value) ? null : ((DateTime?)dr["expirydate"]));
			this.altLongDescription = (table.Columns.Contains("altlongdescription") ? dr["altlongdescription"].ToString() : "");
			this.note = ((!table.Columns.Contains("note") || dr["note"] == DBNull.Value) ? "" : dr["note"].ToString());
			this.recommendedButDeclined = (table.Columns.Contains("recommendedbutdeclined") && dr["recommendedbutdeclined"] != DBNull.Value && Convert.ToBoolean(dr["recommendedbutdeclined"]));
			this.rationale = ((!table.Columns.Contains("rationale") || dr["rationale"] == DBNull.Value) ? "" : dr["rationale"].ToString());
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000312EC File Offset: 0x000302EC
		public static double CalculateTotalDurationInMinutes(double originalDurationMinutes, double extraTimePercent)
		{
			double result;
			if (extraTimePercent == 0.0)
			{
				result = originalDurationMinutes;
			}
			else if (extraTimePercent < 0.0)
			{
				result = -extraTimePercent;
			}
			else
			{
				result = originalDurationMinutes + originalDurationMinutes * extraTimePercent;
			}
			return result;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00031334 File Offset: 0x00030334
		public static string GetAccommodationsAsString(int personid, int lucourseid)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			string commandText = "SELECT m.personid,m.controlid,m.controlvalue AS intval,NULL AS bytesval,NULL AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM maininfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid\r\n    UNION SELECT m.personid,m.controlid,0 AS intval,m.controlvalue AS bytesval,NULL AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM otherinfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid\r\n    UNION SELECT m.personid,m.controlid,0 AS intval,NULL AS bytesval,m.controlvalue AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM datetimeinfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", personid);
			da.SelectCommand.Parameters.Add("@lucid", lucourseid);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			if (dataTable.Rows.Count < 1)
			{
				commandText = "SELECT m.personid,m.controlid,m.controlvalue AS intval,NULL AS bytesval,NULL AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM maininfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid\r\n    UNION SELECT m.personid,m.controlid,0 AS intval,m.controlvalue AS bytesval,NULL AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM otherinfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid\r\n    UNION SELECT m.personid,m.controlid,0 AS intval,NULL AS bytesval,m.controlvalue AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM datetimeinfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pid", personid);
				da.SelectCommand.Parameters.Add("@lucid", 0);
				da.Fill(new DataTable());
			}
			string text2 = "";
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["controlid"];
				DynamicControl dc = new DynamicControl(dataRow);
				string text3 = Reports.DynamicDataToString(dataRow, dc, "intval", "bytesval", "datetimeval", "", "");
				if (text3.Trim().Length > 0)
				{
					if (text2.Length > 0)
					{
						text2 += " • ";
					}
					text2 += text3;
				}
			}
			return text2;
		}

		// Token: 0x04000411 RID: 1041
		private int dataId;

		// Token: 0x04000412 RID: 1042
		private int controlId;

		// Token: 0x04000413 RID: 1043
		private string controlCaption;

		// Token: 0x04000414 RID: 1044
		private string longDescription;

		// Token: 0x04000415 RID: 1045
		private int controlCode;

		// Token: 0x04000416 RID: 1046
		private int lucid;

		// Token: 0x04000417 RID: 1047
		private string valText;

		// Token: 0x04000418 RID: 1048
		private bool valBytesIsEncrypted;

		// Token: 0x04000419 RID: 1049
		private byte[] bb;

		// Token: 0x0400041A RID: 1050
		private byte[] bbi;

		// Token: 0x0400041B RID: 1051
		private bool approved;

		// Token: 0x0400041C RID: 1052
		private int showOnLetter;

		// Token: 0x0400041D RID: 1053
		private DateTime? expiryDate;

		// Token: 0x0400041E RID: 1054
		private string altLongDescription;

		// Token: 0x0400041F RID: 1055
		private string note;

		// Token: 0x04000420 RID: 1056
		private bool recommendedButDeclined;

		// Token: 0x04000421 RID: 1057
		private string rationale;
	}
}
