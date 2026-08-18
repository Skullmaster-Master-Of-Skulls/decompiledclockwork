using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ClockWorkAPI.Properties;
using EncryptionClassLibrary;
using SettingsPermissions;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using UnivOleDb;

namespace ClockWorkAPI.DynamicDataForms
{
	// Token: 0x02000026 RID: 38
	public partial class VariableValuesEditByForm : Form
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000BF3A File Offset: 0x0000AF3A
		public VariableValuesEditByForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000BF54 File Offset: 0x0000AF54
		public VariableValuesEditByForm(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, ref DataSet comboBoxData, ref DataSet lookupTablesForControls, PersonBaseDTO whoAmI, Settings settings, Permissions permissions, Dictionary<string, string> codes)
		{
			this.codes = codes;
			this.da = da;
			this.tripleDES = tripleDES;
			this.screenNum = screenNum;
			this.InitializeComponent();
			this.dataPerStudent1.Init(da, tripleDES, 1, screenNum, false, ref comboBoxData, ref lookupTablesForControls, new ArrayList(), "", whoAmI, settings, permissions);
			this.dataPerStudent1.RenderForm();
			this.dataPerStudent1.FillInData(codes);
			if (this.dataPerStudent1.Screen.Args != null)
			{
				string text = this.dataPerStudent1.Screen.Args["width"];
				string text2 = this.dataPerStudent1.Screen.Args["height"];
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
				{
					int num;
					int num2;
					if (int.TryParse(text, out num) && int.TryParse(text2, out num2))
					{
						if (num > 10 && num2 > 10)
						{
							base.Height = num2;
							base.Width = num;
						}
					}
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C08C File Offset: 0x0000B08C
		public Dictionary<string, string> GetFilledInValues()
		{
			this.da.SelectCommand.CommandText = "SELECT controlid,controlcaption FROM dynamiccontrols";
			this.da.SelectCommand.Parameters.Clear();
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			this.dataPerStudent1.SaveChanges(false);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in this.dataPerStudent1.Data.Tables)
			{
				DataTable dataTable2 = (DataTable)obj;
				bool flag = dataTable2.Columns["controlvalue"].DataType == typeof(int);
				bool flag2 = dataTable2.Columns["controlvalue"].DataType == typeof(byte[]);
				bool flag3 = dataTable2.Columns["controlvalue"].DataType == typeof(DateTime);
				foreach (object obj2 in dataTable2.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					DataRow[] array = dataTable.Select("controlid=" + ((int)dataRow["controlid"]).ToString());
					string text = array[0]["controlcaption"].ToString();
					if (flag)
					{
						dictionary.Add(text, text);
					}
					else if (flag2)
					{
						string text2 = this.tripleDES.Decrypt((byte[])dataRow["controlvalue"]);
						if (text2.StartsWith("{\\rtf1"))
						{
							using (RichTextBox richTextBox = new RichTextBox())
							{
								richTextBox.Rtf = text2;
								text2 = richTextBox.Text.Replace("\n", "\r\n");
							}
						}
						dictionary.Add(text, text2);
					}
					else if (flag3)
					{
						dictionary.Add(text, ((DateTime)dataRow["controlvalue"]).ToString("yyyy-MM-dd"));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C37C File Offset: 0x0000B37C
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C386 File Offset: 0x0000B386
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x040000FC RID: 252
		private UnivDataAdapter da;

		// Token: 0x040000FD RID: 253
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040000FE RID: 254
		private int screenNum;

		// Token: 0x040000FF RID: 255
		private Dictionary<string, string> codes;
	}
}
