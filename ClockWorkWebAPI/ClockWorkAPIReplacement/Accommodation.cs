using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Windows.Forms;
using Databases;
using EncryptionClassLibrary;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200004D RID: 77
	public class Accommodation
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0001A840 File Offset: 0x00018A40
		public int DataId
		{
			get
			{
				return this.dataId;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001A858 File Offset: 0x00018A58
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0001A870 File Offset: 0x00018A70
		public int Lucid
		{
			get
			{
				return this.lucid;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001A888 File Offset: 0x00018A88
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0001A8A0 File Offset: 0x00018AA0
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

		// Token: 0x060003B5 RID: 949 RVA: 0x0001A8AC File Offset: 0x00018AAC
		public string ToStringHtml(bool useAccommodationsApprovalSystem, bool includePrivateNote)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (useAccommodationsApprovalSystem)
			{
				bool flag = this.approved;
				if (flag)
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
			bool flag2 = !string.IsNullOrEmpty(valueString);
			if (flag2)
			{
				stringBuilder.Append(": ");
				stringBuilder.Append(valueString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001A934 File Offset: 0x00018B34
		public string ToString(bool useAccommodationsApprovalSystem, bool includePrivateNote)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (useAccommodationsApprovalSystem)
			{
				bool flag = this.approved;
				if (flag)
				{
					stringBuilder.Append("[Approved] ");
				}
				else
				{
					stringBuilder.Append("[Awaiting approval] ");
				}
			}
			string valueString = this.GetValueString(includePrivateNote);
			bool flag2 = !string.IsNullOrEmpty(valueString);
			if (flag2)
			{
				bool flag3 = stringBuilder.Length > 0;
				if (flag3)
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001A9CC File Offset: 0x00018BCC
		public string ControlCaptionForDisplay
		{
			get
			{
				bool flag = !string.IsNullOrEmpty(this.longDescription.Trim());
				string text;
				if (flag)
				{
					text = this.longDescription;
				}
				else
				{
					bool flag2 = this.controlCaption.IndexOf("~~") >= 0;
					if (flag2)
					{
						int length = this.controlCaption.IndexOf("~~");
						text = this.controlCaption.Substring(0, length);
					}
					else
					{
						text = this.controlCaption;
					}
				}
				bool flag3 = string.IsNullOrEmpty(this.altLongDescription.Trim());
				string result;
				if (flag3)
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

		// Token: 0x060003B8 RID: 952 RVA: 0x0001AA74 File Offset: 0x00018C74
		private string GetValueString(bool includePrivateNote)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = this.bbi != null && this.bbi.Length != 0;
			if (flag)
			{
				string text = encryption.Decrypt(this.bbi);
				bool flag2 = text.StartsWith("{\\rtf1");
				if (flag2)
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
			else
			{
				bool flag3 = this.bb != null && this.bb.Length != 0;
				if (flag3)
				{
					stringBuilder.Append(Utility.BytesToString(this.bb, this.valBytesIsEncrypted, encryption));
				}
				else
				{
					bool flag4 = this.controlCode == 2 || this.controlCode == 700;
					if (!flag4)
					{
						stringBuilder.Append(this.valText);
					}
				}
			}
			bool flag5 = this.expiryDate != null;
			if (flag5)
			{
				stringBuilder.Append(" [expires on ");
				stringBuilder.Append(this.expiryDate.Value.ToString("yyyy-MM-dd"));
				stringBuilder.Append("]");
			}
			bool flag6 = this.recommendedButDeclined;
			if (flag6)
			{
				stringBuilder.Append(" [recommended but declined]");
			}
			bool flag7 = !string.IsNullOrEmpty(this.rationale);
			if (flag7)
			{
				stringBuilder.Append(" [rationale: ");
				stringBuilder.Append(this.rationale);
				stringBuilder.Append("]");
			}
			bool flag8 = includePrivateNote && !string.IsNullOrEmpty(this.note);
			if (flag8)
			{
				stringBuilder.Append(" [private note: ");
				stringBuilder.Append(this.note);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001AC74 File Offset: 0x00018E74
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

		// Token: 0x060003BA RID: 954 RVA: 0x0001AD10 File Offset: 0x00018F10
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

		// Token: 0x060003BB RID: 955 RVA: 0x0001B040 File Offset: 0x00019240
		public static double CalculateTotalDurationInMinutes(double originalDurationMinutes, double extraTimePercent)
		{
			bool flag = extraTimePercent == 0.0;
			double result;
			if (flag)
			{
				result = originalDurationMinutes;
			}
			else
			{
				bool flag2 = extraTimePercent < 0.0;
				if (flag2)
				{
					result = -extraTimePercent;
				}
				else
				{
					result = originalDurationMinutes + originalDurationMinutes * extraTimePercent;
				}
			}
			return result;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001B084 File Offset: 0x00019284
		public static string GetAccommodationsAsString(int personid, int lucourseid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT m.personid,m.controlid,m.controlvalue AS intval,NULL AS bytesval,NULL AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM maininfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid\r\n    UNION SELECT m.personid,m.controlid,0 AS intval,m.controlvalue AS bytesval,NULL AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM otherinfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid\r\n    UNION SELECT m.personid,m.controlid,0 AS intval,NULL AS bytesval,m.controlvalue AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM datetimeinfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, personid),
				clockWork.GetParameter("@lucid", DbType.Int32, lucourseid)
			});
			bool flag = dataTable.Rows.Count < 1;
			if (flag)
			{
				query = "SELECT m.personid,m.controlid,m.controlvalue AS intval,NULL AS bytesval,NULL AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM maininfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid\r\n    UNION SELECT m.personid,m.controlid,0 AS intval,m.controlvalue AS bytesval,NULL AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM otherinfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid\r\n    UNION SELECT m.personid,m.controlid,0 AS intval,NULL AS bytesval,m.controlvalue AS datetimeval,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM datetimeinfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@lucid";
				dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@pid", DbType.Int32, personid),
					clockWork.GetParameter("@lucid", DbType.Int32, 0)
				});
			}
			string text = "";
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["controlid"];
				DynamicControl dc = new DynamicControl(dataRow);
				string text2 = DynamicScreenReports.DynamicDataToString(dataRow, dc, "intval", "bytesval", "datetimeval", "", "");
				bool flag2 = text2.Trim().Length > 0;
				if (flag2)
				{
					bool flag3 = text.Length > 0;
					if (flag3)
					{
						text += " • ";
					}
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x040001DA RID: 474
		private int dataId;

		// Token: 0x040001DB RID: 475
		private int controlId;

		// Token: 0x040001DC RID: 476
		private string controlCaption;

		// Token: 0x040001DD RID: 477
		private string longDescription;

		// Token: 0x040001DE RID: 478
		private int controlCode;

		// Token: 0x040001DF RID: 479
		private int lucid;

		// Token: 0x040001E0 RID: 480
		private string valText;

		// Token: 0x040001E1 RID: 481
		private bool valBytesIsEncrypted;

		// Token: 0x040001E2 RID: 482
		private byte[] bb;

		// Token: 0x040001E3 RID: 483
		private byte[] bbi;

		// Token: 0x040001E4 RID: 484
		private bool approved;

		// Token: 0x040001E5 RID: 485
		private int showOnLetter;

		// Token: 0x040001E6 RID: 486
		private DateTime? expiryDate;

		// Token: 0x040001E7 RID: 487
		private string altLongDescription;

		// Token: 0x040001E8 RID: 488
		private string note;

		// Token: 0x040001E9 RID: 489
		private bool recommendedButDeclined;

		// Token: 0x040001EA RID: 490
		private string rationale;
	}
}
