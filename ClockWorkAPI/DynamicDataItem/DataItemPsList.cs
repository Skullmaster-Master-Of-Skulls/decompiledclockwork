using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI.DynamicDataItem
{
	// Token: 0x0200006F RID: 111
	public class DataItemPsList : List<DataItemPs>
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x0001E7D0 File Offset: 0x0001D7D0
		public DataItemPsList(int personId, int screenNum, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.personId = personId;
			this.screenNum = screenNum;
			string commandText = "SELECT    ad.dataid,ad.personid,ad.controlid,ad.controlcaption,ad.valtext\r\n            ,ad.valint,ad.valbytes,ad.valdate,ad.valimage\r\n            ,ad.setting1,ad.setting2,ad.setting3,ad.setting4,ad.defaultvalue\r\n            ,ad.controlcode,ad.valbytesisencrypted\r\n            ,dsc.ordernum\r\nFROM        perstudentdata2 ad LEFT JOIN dynamiccontrols dc ON dc.controlid=ad.controlid\r\n            LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=dc.controlid\r\nWHERE       ad.personid=@pid\r\n            AND dsc.screennum=@screennum\r\nORDER BY ordernum";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", personId);
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int controlId = (int)dataRow["controlid"];
				string controlCaption = dataRow["controlcaption"].ToString();
				int dataId = (int)dataRow["dataid"];
				int controlCode = (int)dataRow["controlcode"];
				bool valBytesIsEncrypted = Convert.ToBoolean(dataRow["valbytesisencrypted"]);
				byte[] bb = (dataRow["valbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dataRow["valbytes"]);
				byte[] bbi = (dataRow["valimage"] == DBNull.Value) ? new byte[0] : ((byte[])dataRow["valimage"]);
				string valText = dataRow["valtext"].ToString();
				DataItemPs item = new DataItemPs(da, tripleDES, dataId, controlId, controlCaption, controlCode, valText, valBytesIsEncrypted, bb, bbi);
				base.Add(item);
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001E9B8 File Offset: 0x0001D9B8
		public string GetSummaryHtml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<ul>");
			foreach (DataItemPs dataItemPs in this)
			{
				stringBuilder.Append("<li>");
				stringBuilder.Append(dataItemPs.ToStringHtml());
				stringBuilder.Append("</li>");
			}
			stringBuilder.Append("</ul>");
			return stringBuilder.ToString();
		}

		// Token: 0x040002FB RID: 763
		private int personId;

		// Token: 0x040002FC RID: 764
		private int screenNum = 0;
	}
}
