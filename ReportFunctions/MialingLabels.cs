using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoComboBox;
using DynamicScreens.Dialogs;
using EncryptionClassLibrary;
using ReportFunctions.Properties;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000037 RID: 55
	public partial class MialingLabels : Form
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0003E724 File Offset: 0x0003D724
		public MialingLabels()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0003E73D File Offset: 0x0003D73D
		public MialingLabels(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, Report report)
		{
			this._report = report;
			this.da = da;
			this.tripleDES = tripleDES;
			this.InitializeComponent();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0003E76B File Offset: 0x0003D76B
		public MialingLabels(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataView dv)
		{
			this.dv = dv;
			this.da = da;
			this.tripleDES = tripleDES;
			this.InitializeComponent();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0003E799 File Offset: 0x0003D799
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0003E7A3 File Offset: 0x0003D7A3
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0003E7B8 File Offset: 0x0003D7B8
		private void btn_add_Click(object sender, EventArgs e)
		{
			DynamicControlChooserForm dynamicControlChooserForm = new DynamicControlChooserForm(this.da, this.tripleDES, 0);
			DialogResult dialogResult = dynamicControlChooserForm.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				List<string> selectedCidCommaDescriptions = dynamicControlChooserForm.GetSelectedCidCommaDescriptions();
				foreach (string text in selectedCidCommaDescriptions)
				{
					int num = text.IndexOf(',');
					if (num > 0)
					{
						int num2 = int.Parse(text.Substring(0, num));
						string text2 = text.Substring(num + 1);
						ListViewItem listViewItem = new ListViewItem(text2);
						listViewItem.Tag = num2;
						this.lv.Items.Add(listViewItem);
					}
				}
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0003E8A0 File Offset: 0x0003D8A0
		private void btn_remove_Click(object sender, EventArgs e)
		{
			ListViewItem[] array = new ListViewItem[this.lv.SelectedItems.Count];
			this.lv.SelectedItems.CopyTo(array, 0);
			foreach (ListViewItem item in array)
			{
				this.lv.Items.Remove(item);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0003E908 File Offset: 0x0003D908
		private void textBox1_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				string text = (string)e.Data.GetData(DataFormats.Text);
				if (text != null)
				{
					text = "#<" + text + ">#";
					try
					{
						this.textBox1.Text = this.textBox1.Text.Insert(this.textBox1.SelectionStart, text);
					}
					catch
					{
						TextBox textBox = this.textBox1;
						textBox.Text += text;
					}
					this.textBox1.SelectionStart += text.Length;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0003E9D0 File Offset: 0x0003D9D0
		private void textBox1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0003EA08 File Offset: 0x0003DA08
		private void textBox2_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0003EA40 File Offset: 0x0003DA40
		public string Chk1Text
		{
			get
			{
				DataRow dataRow = this.cmb_chk1.SelectedDataRow();
				return (dataRow == null) ? "" : dataRow[this.cmb_chk1.DisplayMember].ToString();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0003EA80 File Offset: 0x0003DA80
		public string Chk2Text
		{
			get
			{
				DataRow dataRow = this.cmb_chk2.SelectedDataRow();
				return (dataRow == null) ? "" : dataRow[this.cmb_chk2.DisplayMember].ToString();
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0003EAC0 File Offset: 0x0003DAC0
		private void textBox2_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				string text = (string)e.Data.GetData(DataFormats.Text);
				if (text != null)
				{
					text = "#<" + text + ">#";
					try
					{
						this.textBox2.Text = this.textBox2.Text.Insert(this.textBox1.SelectionStart, text);
					}
					catch
					{
						TextBox textBox = this.textBox2;
						textBox.Text += text;
					}
					this.textBox2.SelectionStart += text.Length;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0003EB88 File Offset: 0x0003DB88
		public int Chk1Cid
		{
			get
			{
				return this.GetSelectedInd(this.cmb_chk1);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0003EBA8 File Offset: 0x0003DBA8
		public int Chk2Cid
		{
			get
			{
				return this.GetSelectedInd(this.cmb_chk2);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0003EBC8 File Offset: 0x0003DBC8
		public string Template1
		{
			get
			{
				return this.textBox1.Text;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0003EBE8 File Offset: 0x0003DBE8
		public string Template2
		{
			get
			{
				return this.textBox2.Text;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0003EC05 File Offset: 0x0003DC05
		private void cmb_chk1_OnTooltipPopup(object sender, EventArgs e, string text)
		{
			this.toolTip1.SetToolTip(this.cmb_chk1, text);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0003EC1B File Offset: 0x0003DC1B
		private void cmb_chk2_OnTooltipPopup(object sender, EventArgs e, string text)
		{
			this.toolTip1.SetToolTip(this.cmb_chk2, text);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0003EC34 File Offset: 0x0003DC34
		private void MialingLabels_Load(object sender, EventArgs e)
		{
			this.da.SelectCommand.CommandText = "SELECT dsc.controlid,dc.controlcaption FROM screens s LEFT JOIN dynamicscreencontrols dsc ON dsc.screennum=s.screennum LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE s.typecode=0 ORDER BY dc.controlcaption";
			this.da.SelectCommand.Parameters.Clear();
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			this.cmb_chk1.DataSource = new DataView(dataTable);
			this.cmb_chk1.DisplayMember = "controlcaption";
			this.cmb_chk1.ValueMember = "controlid";
			this.cmb_chk2.DataSource = new DataView(dataTable);
			this.cmb_chk2.DisplayMember = "controlcaption";
			this.cmb_chk2.ValueMember = "controlid";
			if (this.dv != null)
			{
				foreach (object obj in this.dv.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					ListViewItem listViewItem = new ListViewItem(dataColumn.ColumnName);
					listViewItem.Tag = dataColumn;
					this.lv.Items.Add(listViewItem);
				}
			}
			this.da.SelectCommand.CommandText = "SELECT settingcode,settingvalue,settingstringvalue FROM settingsgroups WHERE groupid=-1";
			DataTable dataTable2 = new DataTable();
			this.da.SelectCommand.Parameters.Clear();
			this.da.Fill(dataTable2);
			foreach (object obj2 in dataTable2.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				int num = (int)dataRow[0];
				string text = dataRow["settingstringvalue"].ToString();
				int value = (dataRow["settingvalue"] == DBNull.Value) ? 0 : ((int)dataRow["settingvalue"]);
				int num2 = num;
				if (num2 != 495)
				{
					switch (num2)
					{
					case 99655:
						this.textBox1.Text = text;
						break;
					case 99656:
						this.textBox2.Text = text;
						break;
					case 99657:
						this.cmb_chk2.SelectIndexByValueMember(value);
						break;
					case 99658:
						this.cmb_chk1.SelectIndexByValueMember(value);
						break;
					case 99659:
					{
						string parameterValue = text;
						this.da.SelectCommand.CommandText = "SELECT controlid,controlcaption FROM dynamiccontrols WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) ORDER BY controlcaption";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@cids", parameterValue);
						DataTable dataTable3 = new DataTable();
						this.da.Fill(dataTable3);
						foreach (object obj3 in dataTable3.Rows)
						{
							DataRow dataRow2 = (DataRow)obj3;
							ListViewItem listViewItem = new ListViewItem(dataRow2[1].ToString());
							listViewItem.Tag = (int)dataRow2[0];
							this.lv.Items.Add(listViewItem);
						}
						break;
					}
					}
				}
				else
				{
					this.txt_mailingLabelType.Text = text;
				}
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0003F018 File Offset: 0x0003E018
		private void lv_ItemDrag(object sender, ItemDragEventArgs e)
		{
			base.DoDragDrop(((ListViewItem)e.Item).Text, DragDropEffects.Copy);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0003F034 File Offset: 0x0003E034
		private void SaveSetting(int settingCode, int settingValue, string settingStringValue)
		{
			this.da.SelectCommand.CommandText = "DELETE FROM settingsgroups WHERE settingcode=@sc";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@sc", settingCode);
			this.da.Fill(new DataTable());
			this.da.SelectCommand.CommandText = "INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (-1,@sc,@sv,@ssv)";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@sc", settingCode);
			this.da.SelectCommand.Parameters.Add("@sv", settingValue);
			this.da.SelectCommand.Parameters.Add("@ssv", settingStringValue);
			this.da.Fill(new DataTable());
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0003F13C File Offset: 0x0003E13C
		private int GetSelectedInd(AutoComboBox cmb)
		{
			DataRow dataRow = cmb.SelectedDataRow();
			int result;
			if (dataRow == null)
			{
				result = 0;
			}
			else
			{
				result = ((dataRow[cmb.ValueMember] == DBNull.Value) ? 0 : ((int)dataRow[cmb.ValueMember]));
			}
			return result;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0003F18C File Offset: 0x0003E18C
		public string GetCids()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.Tag is int)
				{
					int num = (int)listViewItem.Tag;
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(num.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0003F25C File Offset: 0x0003E25C
		public string AveryType
		{
			get
			{
				return this.txt_mailingLabelType.Text;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0003F27C File Offset: 0x0003E27C
		private void MialingLabels_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveSetting(495, 0, this.txt_mailingLabelType.Text);
			this.SaveSetting(99659, 0, this.GetCids());
			this.SaveSetting(99658, this.GetSelectedInd(this.cmb_chk1), "");
			this.SaveSetting(99657, this.GetSelectedInd(this.cmb_chk2), "");
			this.SaveSetting(99656, 0, this.textBox2.Text);
			this.SaveSetting(99655, 0, this.textBox1.Text);
		}

		// Token: 0x0400018C RID: 396
		private UnivDataAdapter da;

		// Token: 0x0400018D RID: 397
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400018E RID: 398
		private Report _report;

		// Token: 0x0400018F RID: 399
		private DataView dv;
	}
}
