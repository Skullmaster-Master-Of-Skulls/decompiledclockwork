using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox
{
	// Token: 0x02000068 RID: 104
	public partial class InputMultipleOrderedItems : Form
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0001D8FC File Offset: 0x0001C8FC
		public InputMultipleOrderedItems(string title, string caption, object datasource, string DisplayMember, string ValueMember)
		{
			this.InitializeComponent();
			this.Init(title, caption);
			if (datasource is DataView)
			{
				DataView dataView = (DataView)datasource;
				if (dataView.Count > 0)
				{
					this.items = new ListBoxItem[dataView.Count];
					bool flag = ValueMember.Length > 0 && dataView.Table.Columns.Contains(ValueMember);
					for (int i = 0; i < dataView.Count; i++)
					{
						int id;
						if (flag)
						{
							id = (int)dataView[i].Row[ValueMember];
						}
						else
						{
							id = i;
						}
						this.items[i] = new ListBoxItem(id, dataView[i].Row[DisplayMember].ToString());
					}
				}
			}
			else if (datasource is DataTable)
			{
				DataTable dataTable = (DataTable)datasource;
				if (dataTable.Rows.Count > 0)
				{
					this.items = new ListBoxItem[dataTable.Rows.Count];
					bool flag = ValueMember.Length > 0 && dataTable.Columns.Contains(ValueMember);
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						int id;
						if (flag)
						{
							id = (int)dataTable.Rows[i][ValueMember];
						}
						else
						{
							id = i;
						}
						this.items[i] = new ListBoxItem(id, dataTable.Rows[i][DisplayMember].ToString());
					}
				}
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001DAE4 File Offset: 0x0001CAE4
		public InputMultipleOrderedItems(string title, string caption, DataColumnCollection dataColumnCollection)
		{
			this.InitializeComponent();
			this.Init(title, caption);
			if (dataColumnCollection.Count > 0)
			{
				this.items = new ListBoxItem[dataColumnCollection.Count];
				for (int i = 0; i < dataColumnCollection.Count; i++)
				{
					this.items[i] = new ListBoxItem(i, dataColumnCollection[i].ColumnName);
				}
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001DB70 File Offset: 0x0001CB70
		public InputMultipleOrderedItems(string title, string caption, DataColumnCollection dataColumnCollection, string selectedItems)
		{
			this.InitializeComponent();
			this.selectedItems = selectedItems;
			this.Init(title, caption);
			if (dataColumnCollection.Count > 0)
			{
				this.items = new ListBoxItem[dataColumnCollection.Count];
				for (int i = 0; i < dataColumnCollection.Count; i++)
				{
					this.items[i] = new ListBoxItem(i, dataColumnCollection[i].ColumnName);
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001DC04 File Offset: 0x0001CC04
		private void Init(string title, string caption)
		{
			this.Text = title;
			this.lbl_caption.Text = caption;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001EA08 File Offset: 0x0001DA08
		private void btn_moveDown_Click(object sender, EventArgs e)
		{
			this.lv_to.BeginUpdate();
			int num = this.lv_to.Items.Count - 2;
			foreach (object obj in this.lv_to.SelectedIndices)
			{
				int num2 = (int)obj;
				if (num2 <= num)
				{
					ListViewItem listViewItem = this.lv_to.Items[num2];
					ListViewItem listViewItem2 = this.lv_to.Items[num2 + 1];
					ListViewEx.SwapListViewItems(ref listViewItem, ref listViewItem2);
				}
			}
			this.lv_to.EndUpdate();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001EAE0 File Offset: 0x0001DAE0
		private void btn_moveUp_Click(object sender, EventArgs e)
		{
			this.lv_to.BeginUpdate();
			foreach (object obj in this.lv_to.SelectedIndices)
			{
				int num = (int)obj;
				if (num > 0)
				{
					ListViewItem listViewItem = this.lv_to.Items[num - 1];
					ListViewItem listViewItem2 = this.lv_to.Items[num];
					ListViewEx.SwapListViewItems(ref listViewItem, ref listViewItem2);
				}
			}
			this.lv_to.EndUpdate();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001EBA4 File Offset: 0x0001DBA4
		private void btn_moveRight_Click(object sender, EventArgs e)
		{
			this.lv_to.BeginUpdate();
			foreach (object obj in this.lv_from.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				int itemInd = (int)listViewItem.Tag;
				ListViewItem listViewItem2 = this.FindListViewItem(this.lv_to, itemInd);
				if (listViewItem2 == null)
				{
					listViewItem2 = new ListViewItem(listViewItem.Text);
					listViewItem2.Tag = listViewItem.Tag;
					this.lv_to.Items.Add(listViewItem2);
					listViewItem2.Selected = true;
				}
			}
			this.lv_to.EndUpdate();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001EC84 File Offset: 0x0001DC84
		private ListViewItem FindListViewItem(ListViewEx lv, int itemInd)
		{
			foreach (object obj in lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				int num = (int)listViewItem.Tag;
				if (itemInd == num)
				{
					return listViewItem;
				}
			}
			return null;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001ED10 File Offset: 0x0001DD10
		private void btn_moveAllRight_Click(object sender, EventArgs e)
		{
			this.lv_to.BeginUpdate();
			foreach (object obj in this.lv_from.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				int itemInd = (int)listViewItem.Tag;
				ListViewItem listViewItem2 = this.FindListViewItem(this.lv_to, itemInd);
				if (listViewItem2 == null)
				{
					listViewItem2 = new ListViewItem(listViewItem.Text);
					listViewItem2.Tag = listViewItem.Tag;
					this.lv_to.Items.Add(listViewItem2);
					listViewItem2.Selected = true;
				}
			}
			this.lv_to.EndUpdate();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001EDF0 File Offset: 0x0001DDF0
		private void btn_moveLeft_Click(object sender, EventArgs e)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.lv_to.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				arrayList.Add(listViewItem);
			}
			this.lv_to.BeginUpdate();
			foreach (object obj2 in arrayList)
			{
				ListViewItem listViewItem = (ListViewItem)obj2;
				this.lv_to.Items.Remove(listViewItem);
			}
			this.lv_to.EndUpdate();
			arrayList.Clear();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001EEE4 File Offset: 0x0001DEE4
		private void btn_moveAllLeft_Click(object sender, EventArgs e)
		{
			this.lv_to.BeginUpdate();
			this.lv_to.Items.Clear();
			this.lv_to.EndUpdate();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001EF10 File Offset: 0x0001DF10
		private void InputMultipleOrderedItems_Load(object sender, EventArgs e)
		{
			if (this.items != null)
			{
				this.lv_from.BeginUpdate();
				for (int i = 0; i < this.items.Length; i++)
				{
					ListViewItem listViewItem = new ListViewItem(this.items[i].Name);
					listViewItem.Tag = this.items[i].Id;
					this.lv_from.Items.Add(listViewItem);
				}
				this.lv_from.EndUpdate();
				if (this.selectedItems.Length > 0)
				{
					string[] array = this.selectedItems.Split(new char[]
					{
						','
					});
					ArrayList arrayList = new ArrayList();
					foreach (string text in array)
					{
						string text2 = text.Trim().ToLower();
						ListViewItem listViewItem2 = this.GetListViewItem(this.lv_from, text2);
						if (listViewItem2 != null)
						{
							arrayList.Add(listViewItem2);
						}
					}
					if (arrayList.Count > 0)
					{
						foreach (object obj in arrayList)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							listViewItem.Selected = arrayList.Contains(listViewItem);
							this.btn_moveRight_Click(this.btn_moveRight, new EventArgs());
						}
					}
				}
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001F0C4 File Offset: 0x0001E0C4
		private ListViewItem GetListViewItem(ListViewEx lv, string text)
		{
			for (int i = 0; i < lv.Items.Count; i++)
			{
				string text2 = lv.Items[i].Text.ToLower().Trim();
				if (text2.CompareTo(text) == 0)
				{
					return lv.Items[i];
				}
			}
			return null;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001F130 File Offset: 0x0001E130
		public string ChosenItems_string
		{
			get
			{
				string text = "";
				foreach (object obj in this.lv_to.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					if (text.Length > 0)
					{
						text += ",";
					}
					text += listViewItem.Text;
				}
				return text;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001F1D4 File Offset: 0x0001E1D4
		public string[] ChosenItems_string_array
		{
			get
			{
				string[] result;
				if (this.lv_to.Items.Count > 0)
				{
					string[] array = new string[this.lv_to.Items.Count];
					for (int i = 0; i < this.lv_to.Items.Count; i++)
					{
						array[i] = this.lv_to.Items[i].Text;
					}
					result = array;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001F254 File Offset: 0x0001E254
		private void btn_ok_Click(object sender, EventArgs e)
		{
			if (this.lv_to.Items.Count > 0)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001F28D File Offset: 0x0001E28D
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000391 RID: 913
		private ListBoxItem[] items = null;

		// Token: 0x04000392 RID: 914
		private string selectedItems = "";
	}
}
