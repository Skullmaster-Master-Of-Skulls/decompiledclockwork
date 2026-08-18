using System;
using System.Data;
using System.Windows.Forms;
using EncryptionClassLibrary;

namespace ClockWorkAPI.DynamicDataItem
{
	// Token: 0x02000016 RID: 22
	public class DynamicDataValue
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000052A4 File Offset: 0x000042A4
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000052BC File Offset: 0x000042BC
		public int PersonId
		{
			get
			{
				return this.personId;
			}
			set
			{
				this.personId = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000052C8 File Offset: 0x000042C8
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000052E0 File Offset: 0x000042E0
		public int AppointmentId
		{
			get
			{
				return this.appointmentId;
			}
			set
			{
				this.appointmentId = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000052EC File Offset: 0x000042EC
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005304 File Offset: 0x00004304
		public object DataValue
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005310 File Offset: 0x00004310
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005328 File Offset: 0x00004328
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
			set
			{
				this.controlId = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005334 File Offset: 0x00004334
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000534C File Offset: 0x0000434C
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005358 File Offset: 0x00004358
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005370 File Offset: 0x00004370
		public int ControlCode
		{
			get
			{
				return this.controlCode;
			}
			set
			{
				this.controlCode = value;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000537A File Offset: 0x0000437A
		public DynamicDataValue()
		{
			this.personId = 0;
			this.appointmentId = 0;
			this.dataValue = null;
			this.controlId = 0;
			this.controlCaption = "";
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000053AC File Offset: 0x000043AC
		public DynamicDataValue(int controlId, string controlCaption, int personId, object dataValue)
		{
			this.controlId = controlId;
			this.controlCaption = controlCaption;
			this.personId = personId;
			this.dataValue = dataValue;
			this.appointmentId = 0;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000053DB File Offset: 0x000043DB
		public DynamicDataValue(int controlId, string controlCaption, int personId, int appointmentId, object dataValue)
		{
			this.controlId = controlId;
			this.controlCaption = controlCaption;
			this.personId = personId;
			this.dataValue = dataValue;
			this.appointmentId = appointmentId;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000540C File Offset: 0x0000440C
		public DynamicDataValue(DataRow dr, TripleDESEncryptionClass tripleDES)
		{
			this.controlId = ((dr["controlid"] == DBNull.Value) ? 0 : ((int)dr["controlid"]));
			this.controlCaption = dr["controlcaption"].ToString();
			if (dr == null)
			{
				this.personId = 0;
				this.dataValue = 0;
				this.appointmentId = 0;
			}
			else
			{
				DataTable table = dr.Table;
				this.personId = (table.Columns.Contains("personid") ? ((dr["personid"] == DBNull.Value) ? 0 : ((int)dr["personid"])) : 0);
				this.appointmentId = (table.Columns.Contains("appointmentid") ? ((dr["appointmentid"] == DBNull.Value) ? 0 : ((int)dr["appointmentid"])) : 0);
				this.dataValue = DynamicDataValue.ParseDataValue(dr, tripleDES);
				this.controlCode = ((dr["controlcode"] == DBNull.Value) ? 0 : ((int)dr["controlcode"]));
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005550 File Offset: 0x00004550
		public static RichTextBox Rtb
		{
			get
			{
				if (DynamicDataValue.rtb == null)
				{
					DynamicDataValue.rtb = new RichTextBox();
				}
				return DynamicDataValue.rtb;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005580 File Offset: 0x00004580
		private static object ParseDataValue(DataRow dr, TripleDESEncryptionClass tripleDES)
		{
			int num = (dr["controlcode"] == DBNull.Value) ? 0 : ((int)dr["controlcode"]);
			object result;
			if (num == 2 || num == 700)
			{
				result = (dr["valint"] != DBNull.Value && (int)dr["valint"] == 1);
			}
			else if (dr["valdate"] != DBNull.Value && (DateTime)dr["valdate"] != DateTime.MinValue)
			{
				result = (DateTime)dr["valdate"];
			}
			else
			{
				bool flag = dr["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dr["valbytesisencrypted"]);
				object obj;
				if (flag)
				{
					byte[] array = (dr["valbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dr["valbytes"]);
					if (array.Length > 0)
					{
						obj = tripleDES.Decrypt(array);
					}
					else
					{
						byte[] array2 = (dr["valimage"] == DBNull.Value) ? new byte[0] : ((byte[])dr["valimage"]);
						if (array2.Length > 0)
						{
							string rtf = tripleDES.Decrypt(array2);
							obj = DynamicDataValue.GetRtfPlain(rtf);
						}
						else
						{
							obj = dr["valtext"].ToString();
						}
					}
				}
				else
				{
					byte[] array2 = (dr["valimage"] == DBNull.Value) ? new byte[0] : ((byte[])dr["valimage"]);
					if (array2.Length > 0)
					{
						string rtf = tripleDES.Decrypt(array2);
						obj = DynamicDataValue.GetRtfPlain(rtf);
					}
					else
					{
						obj = dr["valtext"].ToString();
					}
				}
				result = obj;
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000057A0 File Offset: 0x000047A0
		private static string GetRtfPlain(string rtf)
		{
			if (rtf.StartsWith("{\\rtf"))
			{
				RichTextBox richTextBox = DynamicDataValue.Rtb;
				try
				{
					richTextBox.Rtf = rtf;
					return richTextBox.Text;
				}
				catch (Exception ex)
				{
					return rtf;
				}
			}
			return rtf;
		}

		// Token: 0x04000065 RID: 101
		private int personId;

		// Token: 0x04000066 RID: 102
		private int appointmentId;

		// Token: 0x04000067 RID: 103
		private int controlId;

		// Token: 0x04000068 RID: 104
		private string controlCaption;

		// Token: 0x04000069 RID: 105
		private object dataValue;

		// Token: 0x0400006A RID: 106
		private int controlCode;

		// Token: 0x0400006B RID: 107
		private static RichTextBox rtb = null;
	}
}
