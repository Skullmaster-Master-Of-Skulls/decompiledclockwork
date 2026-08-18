using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox;
using UnivOleDb;

namespace DynamicScreens.AdminTools
{
	// Token: 0x02000006 RID: 6
	public partial class LookupItemEdit : Form
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000030A4 File Offset: 0x000020A4
		public LookupItemEdit(int lookupGroupId, int lookupListId, int childListLookupGroupId, string oldDescription, string oldChildItems, UnivDataAdapter da, string oldLookupValue)
		{
			this.InitializeComponent();
			this.lookupGroupId = lookupGroupId;
			this.lookupListId = lookupListId;
			this.childListLookupGroupId = childListLookupGroupId;
			this.oldDescription = oldDescription;
			this.oldChildItems = oldChildItems;
			this.da = da;
			this.oldLookupValue = oldLookupValue;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000030F8 File Offset: 0x000020F8
		public LookupItemEdit(int lookupGroupId, int lookupListId, int childListLookupGroupId, string oldDescription, string oldChildItems, UnivDataAdapter da)
		{
			this.InitializeComponent();
			this.lookupGroupId = lookupGroupId;
			this.lookupListId = lookupListId;
			this.childListLookupGroupId = childListLookupGroupId;
			this.oldDescription = oldDescription;
			this.oldChildItems = oldChildItems;
			this.da = da;
			this.oldLookupValue = "";
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003AA0 File Offset: 0x00002AA0
		private void LookupItemEdit_Load(object sender, EventArgs e)
		{
			this.txt_description.Text = this.oldDescription;
			this.txt_descriptionFrench.Text = this.oldLookupValue;
			if (this.childListLookupGroupId > 0)
			{
				this.p_children.Enabled = true;
				this.da.SelectCommand.CommandText = "SELECT lookuplistid,lookuptext,ordernum,lookupvalue FROM lookuplists WHERE lookupgroupid=" + this.childListLookupGroupId.ToString() + " AND visible=1 ORDER BY ordernum,lookuptext";
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				this.lv_childItems.BeginUpdate();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					ListViewItem listViewItem = new ListViewItem(dataRow["lookuptext"].ToString());
					listViewItem.Tag = dataRow;
					int item = (int)dataRow["lookuplistid"];
					if (this.ListContains(this.oldChildItems, item))
					{
						listViewItem.Checked = true;
					}
					this.lv_childItems.Items.Add(listViewItem);
				}
				this.lv_childItems.EndUpdate();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003C08 File Offset: 0x00002C08
		private bool ListContains(string list, int item)
		{
			string strB = item.ToString().Trim();
			string[] array = list.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				if (text.Trim().CompareTo(strB) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003C84 File Offset: 0x00002C84
		private bool AnyChanges(bool saveChanges)
		{
			bool flag = false;
			if (this.txt_description.Text.CompareTo(this.oldDescription) != 0)
			{
				flag = true;
			}
			if (this.txt_descriptionFrench.Text.CompareTo(this.oldLookupValue) != 0)
			{
				flag = true;
			}
			if (this.childListLookupGroupId > 0)
			{
				string checkedItemsInAList = this.GetCheckedItemsInAList();
				if (checkedItemsInAList.CompareTo(this.oldChildItems) != 0)
				{
					flag = true;
				}
			}
			if (saveChanges && flag)
			{
				base.DialogResult = DialogResult.OK;
			}
			else if (saveChanges)
			{
				base.DialogResult = DialogResult.Abort;
			}
			return flag;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003D40 File Offset: 0x00002D40
		public string GetLookupValue()
		{
			return this.txt_descriptionFrench.Text;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D60 File Offset: 0x00002D60
		public string GetLookupText()
		{
			return this.txt_description.Text;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D80 File Offset: 0x00002D80
		public string GetChildren()
		{
			return this.GetCheckedItemsInAList();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003D98 File Offset: 0x00002D98
		private string GetCheckedItemsInAList()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.lv_childItems.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.Checked)
				{
					DataRow dataRow = (DataRow)listViewItem.Tag;
					int num = (int)dataRow["lookuplistid"];
					if (!arrayList.Contains(num))
					{
						arrayList.Add(num);
					}
				}
			}
			string text = "";
			foreach (object obj2 in arrayList)
			{
				int num2 = (int)obj2;
				if (text.Length > 0)
				{
					text += ",";
				}
				text += num2.ToString();
			}
			return text;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003EF0 File Offset: 0x00002EF0
		private void LookupItemEdit_Closing(object sender, CancelEventArgs e)
		{
			if (base.DialogResult != DialogResult.OK && base.DialogResult != DialogResult.Abort)
			{
				if (this.AnyChanges(false))
				{
					DialogResult dialogResult = MessageBox.Show("Would you like to save your changes?", "Warning: changes will be lost", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
					if (dialogResult == DialogResult.Cancel)
					{
						e.Cancel = true;
					}
					else if (dialogResult == DialogResult.Yes)
					{
						this.AnyChanges(true);
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003F66 File Offset: 0x00002F66
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.AnyChanges(true);
			base.Close();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003F78 File Offset: 0x00002F78
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000012 RID: 18
		private int lookupListId;

		// Token: 0x04000013 RID: 19
		private int lookupGroupId;

		// Token: 0x04000014 RID: 20
		private int childListLookupGroupId;

		// Token: 0x04000015 RID: 21
		private string oldDescription;

		// Token: 0x04000016 RID: 22
		private string oldChildItems;

		// Token: 0x04000017 RID: 23
		private string oldLookupValue;

		// Token: 0x04000018 RID: 24
		private UnivDataAdapter da;
	}
}
