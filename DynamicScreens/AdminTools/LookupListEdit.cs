using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox;
using DynamicScreens.Properties;
using UnivOleDb;

namespace DynamicScreens.AdminTools
{
	// Token: 0x02000048 RID: 72
	public partial class LookupListEdit : Form
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x00035040 File Offset: 0x00034040
		public LookupListEdit(UnivDataAdapter Da, int LookupGroupID)
		{
			this.InitializeComponent();
			this.da = Da;
			this.lookupGroupID = LookupGroupID;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0003606C File Offset: 0x0003506C
		private void LookupListEdit_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			if (this.lookupGroupID > 0)
			{
				this.da.SelectCommand.CommandText = "SELECT TOP 1 dataid FROM accommodationdata \r\nWHERE   controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n        AND controlid IN (SELECT controlid FROM dynamiccontrols WHERE (controlcode=10 AND setting1=@id) OR (controlcode=20 AND setting1=@id))";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@id", this.lookupGroupID);
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					MessageBox.Show("It appears this list is being used for a table control or file list control.  If you make changes to this list it could cause problems with the existing data.  If you need to make changes please contact the ClockWork support team for assistance.");
				}
			}
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.ChildrenInLookupLists);
			bool flag2 = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.LookupListsExtended);
			bool flag3 = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.MiscUpgrades_Nov_2007);
			if (flag2)
			{
				this.btn2_down.Visible = true;
				this.btn2_up.Visible = true;
				this.separator_multiple.Visible = true;
			}
			if (flag3)
			{
				this.p_childList.Visible = true;
				this.da.SelectCommand.CommandText = "SELECT -1 AS lookupgroupid,'' AS description UNION SELECT lookupgroupid,description FROM lookupgroups ORDER BY description";
				DataTable dataTable2 = new DataTable();
				this.da.Fill(dataTable2);
				this.cmb_lists.DataSource = dataTable2;
				this.cmb_lists.DisplayMember = "description";
				this.cmb_lists.ValueMember = "lookupgroupid";
				this.da.SelectCommand.CommandText = "SELECT childlist FROM lookupgroups WHERE lookupgroupid=" + this.lookupGroupID.ToString();
				DataTable dataTable3 = new DataTable();
				this.da.Fill(dataTable3);
				if (dataTable3.Rows.Count > 0)
				{
					this.childList = ((dataTable3.Rows[0][0] == DBNull.Value) ? 0 : ((int)dataTable3.Rows[0][0]));
					if (this.childList > 0)
					{
						this.cmb_lists.SelectIndexByValueMember(this.childList);
					}
				}
			}
			if (flag)
			{
				this.da.SelectCommand.CommandText = "SELECT lookuplistid,lookupgroupid,lookuptext,ordernum,lookupvalue,children";
			}
			else
			{
				this.da.SelectCommand.CommandText = "SELECT lookuplistid,lookupgroupid,lookuptext,ordernum,lookupvalue,'' AS children";
			}
			UnivCommand selectCommand = this.da.SelectCommand;
			selectCommand.CommandText += " FROM lookuplists WHERE ";
			if (this.lookupGroupID >= 0)
			{
				UnivCommand selectCommand2 = this.da.SelectCommand;
				selectCommand2.CommandText += "lookupgroupid=@lookupgroupid";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@lookupgroupid", this.lookupGroupID);
			}
			else
			{
				UnivCommand selectCommand3 = this.da.SelectCommand;
				selectCommand3.CommandText += "1=0";
			}
			UnivCommand selectCommand4 = this.da.SelectCommand;
			selectCommand4.CommandText += " ORDER BY ";
			if (flag2)
			{
				UnivCommand selectCommand5 = this.da.SelectCommand;
				selectCommand5.CommandText += "ordernum,lookuptext";
			}
			else
			{
				UnivCommand selectCommand6 = this.da.SelectCommand;
				selectCommand6.CommandText += "lookuptext";
			}
			this.lookupListsTable = new DataTable();
			this.da.Fill(this.lookupListsTable);
			this.FillScreen();
			this.Cursor = Cursors.Default;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00036438 File Offset: 0x00035438
		private void FillScreen()
		{
			foreach (object obj in this.lookupListsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ListViewItem listViewItem = new ListViewItem(dataRow[2].ToString());
				if (dataRow.Table.Columns.Contains("children"))
				{
					listViewItem.SubItems.Add(dataRow["children"].ToString());
				}
				else
				{
					listViewItem.SubItems.Add("");
				}
				listViewItem.Tag = dataRow;
				this.listView1.Items.Add(listViewItem);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0003651C File Offset: 0x0003551C
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			this.EditSelectedItem();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00036528 File Offset: 0x00035528
		private void EditSelectedItem()
		{
			if (this.listView1.SelectedIndices != null && this.listView1.SelectedIndices.Count > 0)
			{
				ListViewItem listViewItem = this.listView1.Items[this.listView1.SelectedIndices[0]];
				DataRow dataRow = (DataRow)listViewItem.Tag;
				int lookupListId = (dataRow["lookuplistid"] == DBNull.Value) ? 0 : ((int)dataRow["lookuplistid"]);
				int selectedChildLookupGroupId = this.GetSelectedChildLookupGroupId();
				LookupItemEdit lookupItemEdit = new LookupItemEdit(this.lookupGroupID, lookupListId, selectedChildLookupGroupId, listViewItem.Text, dataRow.Table.Columns.Contains("children") ? ((dataRow["children"] == DBNull.Value) ? "" : ((string)dataRow["children"])) : "", this.da, dataRow["lookupvalue"].ToString());
				DialogResult dialogResult = lookupItemEdit.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					string lookupText = lookupItemEdit.GetLookupText();
					dataRow["lookuptext"] = lookupText;
					listViewItem.Text = lookupText;
					dataRow["lookupvalue"] = lookupItemEdit.GetLookupValue();
					if (dataRow.Table.Columns.Contains("children"))
					{
						string children = lookupItemEdit.GetChildren();
						dataRow["children"] = children;
						listViewItem.SubItems[1].Text = children;
					}
				}
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000366D0 File Offset: 0x000356D0
		private void listView1_SizeChanged(object sender, EventArgs e)
		{
			int num = this.listView1.Width - 40;
			int num2 = Convert.ToInt32(0.75 * (double)num);
			this.listView1.Columns[0].Width = num2;
			this.listView1.Columns[1].Width = num - num2;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00036734 File Offset: 0x00035734
		private bool AnyRowsFound(bool anyRowsFoundSoFar, string tableName, int controlID)
		{
			bool result;
			if (anyRowsFoundSoFar)
			{
				result = true;
			}
			else
			{
				this.da.SelectCommand.CommandText = "SELECT controlid FROM " + tableName + " WHERE controlid=@controlid";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@controlid", controlID);
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				result = (dataTable.Rows.Count > 0);
			}
			return result;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000367CC File Offset: 0x000357CC
		private bool AnyChanges()
		{
			bool result;
			if (this.ValidateOrderNums(true))
			{
				result = true;
			}
			else
			{
				foreach (object obj in this.lookupListsTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Unchanged)
					{
						return true;
					}
				}
				if (this.p_childList.Visible)
				{
					DataRow dataRow = this.cmb_lists.SelectedDataRow();
					int num = (dataRow == null) ? 0 : ((int)dataRow[0]);
					if (num != this.childList)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000368A8 File Offset: 0x000358A8
		private bool ValidateOrderNums(bool justCheckForChanges)
		{
			if (this.listView1.Items.Count > 0)
			{
				DataRow dataRow = (DataRow)this.listView1.Items[0].Tag;
				int num = (int)dataRow[3];
				for (int i = 1; i < this.listView1.Items.Count; i++)
				{
					DataRow dataRow2 = (DataRow)this.listView1.Items[i].Tag;
					int num2 = (int)dataRow2[3];
					if (num2 > num)
					{
						num = num2;
					}
					else
					{
						if (justCheckForChanges)
						{
							return true;
						}
						dataRow2[3] = ++num;
					}
				}
			}
			return false;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0003698C File Offset: 0x0003598C
		private void Save()
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.ChildrenInLookupLists);
			this.ValidateOrderNums(false);
			try
			{
				foreach (object obj in this.lookupListsTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState == DataRowState.Added)
					{
						if (flag)
						{
							this.da.SelectCommand.CommandText = "INSERT INTO lookuplists (lookupgroupid,lookuptext,ordernum,children,lookupvalue,visible) VALUES (@lookupgroupid,@lookuptext,@ordernum,@children,@lookupvalue,1)";
						}
						else
						{
							this.da.SelectCommand.CommandText = "INSERT INTO lookuplists (lookupgroupid,lookuptext,ordernum,lookupvalue,visible) VALUES (@lookupgroupid,@lookuptext,@ordernum,@lookupvalue,1)";
						}
						this.da.SelectCommand.Parameters.Clear();
						if (flag)
						{
							this.da.SelectCommand.Parameters.Add("@children", dataRow["children"]);
						}
						this.da.SelectCommand.Parameters.Add("@lookupgroupid", this.lookupGroupID);
						this.da.SelectCommand.Parameters.Add("@lookuptext", dataRow[2]);
						this.da.SelectCommand.Parameters.Add("@ordernum", dataRow[3]);
						this.da.SelectCommand.Parameters.Add("@lookupvalue", (dataRow["lookupvalue"] == DBNull.Value) ? "" : dataRow["lookupvalue"]);
						string text;
						this.da.Fill(new DataTable(), out text);
						if (text != null && text.Length > 0)
						{
							MessageBox.Show(text);
							this.cancelled = true;
							return;
						}
						dataRow.AcceptChanges();
					}
					else if (dataRow.RowState == DataRowState.Modified)
					{
						if (flag)
						{
							this.da.SelectCommand.CommandText = "UPDATE lookuplists SET lookuptext=@lookuptext,ordernum=@ordernum,children=@children,lookupvalue=@lookupvalue";
						}
						else
						{
							this.da.SelectCommand.CommandText = "UPDATE lookuplists SET lookuptext=@lookuptext,ordernum=@ordernum,lookupvalue=@lookupvalue";
						}
						this.da.SelectCommand.Parameters.Clear();
						if (flag)
						{
							this.da.SelectCommand.Parameters.Add("@children", dataRow["children"]);
						}
						UnivCommand selectCommand = this.da.SelectCommand;
						selectCommand.CommandText += " WHERE lookuplistid=@lookuplistid";
						this.da.SelectCommand.Parameters.Add("@lookuptext", dataRow[2]);
						this.da.SelectCommand.Parameters.Add("@ordernum", dataRow[3]);
						this.da.SelectCommand.Parameters.Add("@lookuplistid", dataRow[0]);
						this.da.SelectCommand.Parameters.Add("@lookupvalue", (dataRow["lookupvalue"] == DBNull.Value) ? "" : dataRow["lookupvalue"]);
						string text;
						this.da.Fill(new DataTable(), out text);
						if (text != null && text.Length > 0)
						{
							MessageBox.Show(text);
							this.cancelled = true;
							return;
						}
						dataRow.AcceptChanges();
					}
					else if (dataRow.RowState == DataRowState.Deleted)
					{
						dataRow.RejectChanges();
						int num = (int)dataRow[0];
						this.da.SelectCommand.CommandText = "DELETE FROM lookuplists WHERE lookuplistid=@lookuplistid";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@lookuplistid", num);
						this.da.Fill(new DataTable());
						dataRow.Delete();
					}
				}
				if (this.p_childList.Visible)
				{
					int selectedChildLookupGroupId = this.GetSelectedChildLookupGroupId();
					if (selectedChildLookupGroupId != this.childList)
					{
						this.da.SelectCommand.CommandText = "UPDATE lookupgroups SET childlist=@childlist WHERE lookupgroupid=@lookupgroupid";
						this.da.SelectCommand.Parameters.Clear();
						if (selectedChildLookupGroupId > 0)
						{
							this.da.SelectCommand.Parameters.Add("@childlist", selectedChildLookupGroupId);
						}
						else
						{
							this.da.SelectCommand.Parameters.Add("@childlist", DBNull.Value);
						}
						this.da.SelectCommand.Parameters.Add("@lookupgroupid", this.lookupGroupID);
						this.da.Fill(new DataTable());
					}
				}
				this.cancelled = false;
			}
			catch (Exception ex)
			{
				this.cancelled = true;
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00036F0C File Offset: 0x00035F0C
		private int GetSelectedChildLookupGroupId()
		{
			int result;
			if (!this.p_childList.Visible)
			{
				result = 0;
			}
			else
			{
				DataRow dataRow = this.cmb_lists.SelectedDataRow();
				int num = (dataRow == null) ? 0 : ((int)dataRow[0]);
				result = num;
			}
			return result;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00036F54 File Offset: 0x00035F54
		private void LookupListEdit_Closing(object sender, CancelEventArgs e)
		{
			if (this.cancelled)
			{
				bool flag = this.AnyChanges();
				if (flag)
				{
					DialogResult dialogResult = MessageBox.Show("Do you want to save your changes?", "Changes Will be Lost!", MessageBoxButtons.YesNoCancel);
					if (dialogResult == DialogResult.Yes)
					{
						this.Save();
						if (!this.cancelled)
						{
							base.Close();
						}
					}
					else if (dialogResult == DialogResult.Cancel)
					{
						e.Cancel = true;
					}
				}
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00036FD0 File Offset: 0x00035FD0
		private void listView1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Subtract)
			{
				this.MoveCurrentItemUp();
			}
			else if (e.KeyCode == Keys.Add)
			{
				this.MoveCurrentItemDown();
			}
			else if (e.KeyCode == Keys.Return)
			{
				this.EditSelectedItem();
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00037030 File Offset: 0x00036030
		private void MoveCurrentItemUp()
		{
			if (this.listView1.SelectedIndices != null && this.listView1.SelectedIndices.Count > 0)
			{
				int num = this.listView1.SelectedIndices[0];
				if (num > 0)
				{
					this.SwapListViewItems(this.listView1.Items[num], this.listView1.Items[num - 1]);
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000370B4 File Offset: 0x000360B4
		private void MoveCurrentItemDown()
		{
			if (this.listView1.SelectedIndices != null && this.listView1.SelectedIndices.Count > 0)
			{
				int num = this.listView1.SelectedIndices[0];
				if (num < this.listView1.SelectedIndices.Count - 1)
				{
					this.SwapListViewItems(this.listView1.Items[num], this.listView1.Items[num + 1]);
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00037148 File Offset: 0x00036148
		private void SwapListViewItems(ListViewItem lvi1, ListViewItem lvi2)
		{
			string[] array = new string[lvi1.SubItems.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = lvi1.SubItems[i].Text;
			}
			object tag = lvi1.Tag;
			int imageIndex = lvi1.ImageIndex;
			bool selected = lvi1.Selected;
			for (int i = 0; i < array.Length; i++)
			{
				lvi1.SubItems[i].Text = lvi2.SubItems[i].Text;
			}
			lvi1.Tag = lvi2.Tag;
			lvi1.ImageIndex = lvi1.ImageIndex;
			for (int i = 0; i < array.Length; i++)
			{
				lvi2.SubItems[i].Text = array[i];
			}
			lvi2.Tag = tag;
			lvi2.ImageIndex = imageIndex;
			lvi1.Selected = lvi2.Selected;
			lvi2.Selected = selected;
			lvi1.Selected = false;
			lvi2.Selected = true;
			this.listView1.EnsureVisible(lvi2.Index);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00037268 File Offset: 0x00036268
		private void MoveItem(int direction)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listView1.SelectedItems[0];
				int index = listViewItem.Index;
				int num = index + direction;
				if (num >= 0 && num < this.listView1.Items.Count)
				{
					ListViewItem listViewItem2 = this.listView1.Items[num];
					DataRow dataRow = (DataRow)listViewItem.Tag;
					DataRow dataRow2 = (DataRow)listViewItem2.Tag;
					int num2 = (int)dataRow["ordernum"];
					int num3 = (int)dataRow2["ordernum"];
					dataRow["ordernum"] = num3;
					dataRow2["ordernum"] = num2;
					listViewItem.Tag = dataRow2;
					listViewItem2.Tag = dataRow;
					string text = listViewItem.Text;
					listViewItem.Text = listViewItem2.Text;
					listViewItem2.Text = text;
					bool selected = listViewItem.Selected;
					listViewItem.Selected = listViewItem2.Selected;
					listViewItem2.Selected = selected;
					this.listView1.Refresh();
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000373AD File Offset: 0x000363AD
		private void btn2_up_Click(object sender, EventArgs e)
		{
			this.MoveItem(-1);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000373B8 File Offset: 0x000363B8
		private void btn2_down_Click(object sender, EventArgs e)
		{
			this.MoveItem(1);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000373C4 File Offset: 0x000363C4
		private void btn2_addItem_Click(object sender, EventArgs e)
		{
			int num;
			if (this.listView1.SelectedIndices != null && this.listView1.SelectedIndices.Count > 0)
			{
				num = this.listView1.SelectedIndices[0];
			}
			else
			{
				num = -1;
			}
			int selectedChildLookupGroupId = this.GetSelectedChildLookupGroupId();
			LookupItemEdit lookupItemEdit = new LookupItemEdit(this.lookupGroupID, 0, selectedChildLookupGroupId, "", "", this.da, "");
			DialogResult dialogResult = lookupItemEdit.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DataRow dataRow = this.lookupListsTable.NewRow();
				dataRow[1] = this.lookupGroupID;
				string lookupText = lookupItemEdit.GetLookupText();
				dataRow[2] = lookupText;
				dataRow["lookupvalue"] = lookupItemEdit.GetLookupValue();
				string text;
				if (this.p_childList.Visible)
				{
					text = lookupItemEdit.GetChildren();
					dataRow["children"] = text;
				}
				else
				{
					text = "";
				}
				int num2 = 0;
				if (num >= 0)
				{
					num2 = (int)((DataRow)this.listView1.Items[num].Tag)[3];
					num2--;
				}
				dataRow[3] = num2;
				this.lookupListsTable.Rows.Add(dataRow);
				ListViewItem listViewItem = new ListViewItem(lookupText);
				listViewItem.SubItems.Add(text);
				listViewItem.Tag = dataRow;
				if (num >= 0)
				{
					this.listView1.Items.Insert(num, listViewItem);
				}
				else
				{
					this.listView1.Items.Add(listViewItem);
				}
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0003758C File Offset: 0x0003658C
		private void btn2_addMultiple_Click(object sender, EventArgs e)
		{
			string userInput = InputBox.GetUserInput(this, "New Lookup List Item", "Please enter the text of the item(s) you would like to add.  Use a comma or <newline> to separate multiple items, use \\, to escape comma:", "", 450, false);
			if (userInput != null)
			{
				this.AddMultiple(userInput);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000375C8 File Offset: 0x000365C8
		private void AddMultiple(string s)
		{
			int num;
			if (this.listView1.SelectedIndices != null && this.listView1.SelectedIndices.Count > 0)
			{
				num = this.listView1.SelectedIndices[0];
			}
			else
			{
				num = -1;
			}
			bool flag = s.IndexOf("\\,") >= 0;
			if (flag)
			{
				s = s.Replace("\\,", "``````");
			}
			string[] array = (s.IndexOf(Environment.NewLine) >= 0) ? s.Split(Environment.NewLine.ToCharArray()) : s.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				string text2 = flag ? text.Replace("``````", ",") : text;
				if (text2.Length > 0)
				{
					DataRow dataRow = this.lookupListsTable.NewRow();
					dataRow[1] = this.lookupGroupID;
					dataRow[2] = text2;
					int num2 = 0;
					if (num >= 0)
					{
						num2 = (int)((DataRow)this.listView1.Items[num].Tag)[3];
						num2--;
					}
					dataRow[3] = num2;
					this.lookupListsTable.Rows.Add(dataRow);
					ListViewItem listViewItem = new ListViewItem(text2);
					listViewItem.SubItems.Add("");
					listViewItem.Tag = dataRow;
					if (num >= 0)
					{
						this.listView1.Items.Insert(num, listViewItem);
						num++;
					}
					else
					{
						this.listView1.Items.Add(listViewItem);
					}
				}
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000377C4 File Offset: 0x000367C4
		private void btn2_removeItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedIndices != null && this.listView1.SelectedIndices.Count > 0)
			{
				this.Cursor = Cursors.WaitCursor;
				ListViewItem listViewItem = this.listView1.Items[this.listView1.SelectedIndices[0]];
				DataRow dataRow = (DataRow)listViewItem.Tag;
				this.da.SelectCommand.CommandText = "SELECT controlid FROM dynamiccontrols WHERE controlcode=@controlcode AND setting1=@setting1";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@controlcode", 3);
				this.da.SelectCommand.Parameters.Add("@setting1", dataRow[1]);
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				int num;
				if (dataTable.Rows.Count > 0)
				{
					num = (int)dataTable.Rows[0][0];
				}
				else
				{
					num = -1;
				}
				if (num >= 0)
				{
					bool flag = false;
					flag = this.AnyRowsFound(flag, "maininfopa", num);
					flag = this.AnyRowsFound(flag, "maininfops", num);
					flag = this.AnyRowsFound(flag, "otherinfopa", num);
					flag = this.AnyRowsFound(flag, "otherinfops", num);
					flag = this.AnyRowsFound(flag, "datetimeinfopa", num);
					flag = this.AnyRowsFound(flag, "datetimeinfops", num);
					if (flag)
					{
						DialogResult dialogResult = MessageBox.Show("There is data currently in the database that uses this specific lookup list item.  If you delete this, the existing data will remain and be available for reports, but no further data can be entered using this item." + Environment.NewLine + "Are you sure you want to delete this item?", "Delete Lookup List Item", MessageBoxButtons.YesNoCancel);
						if (dialogResult != DialogResult.Yes)
						{
							this.Cursor = Cursors.Default;
							return;
						}
					}
				}
				DialogResult dialogResult2 = MessageBox.Show("Are you absolutely sure you want to delete this item?", "Delete Lookup List Item", MessageBoxButtons.YesNoCancel);
				if (dialogResult2 == DialogResult.Yes)
				{
					dataRow.Delete();
					this.listView1.Items.Remove(listViewItem);
				}
				this.Cursor = Cursors.Default;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000379F3 File Offset: 0x000369F3
		private void btn2_print_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000379F8 File Offset: 0x000369F8
		private void btn2_export_Click(object sender, EventArgs e)
		{
			string text = "";
			foreach (object obj in this.lookupListsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					if (text.Length > 0)
					{
						text += Environment.NewLine;
					}
					text += dataRow["lookuptext"].ToString();
				}
			}
			InputBox.GetUserInput(this, "View list items", "View list items; click 'close' when you're done.", text, 450, false);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00037AC0 File Offset: 0x00036AC0
		private void btn2_save_Click(object sender, EventArgs e)
		{
			this.Save();
			if (!this.cancelled)
			{
				base.Close();
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00037AE6 File Offset: 0x00036AE6
		private void btn2_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00037AF0 File Offset: 0x00036AF0
		private void addprovincesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string s = "AB\r\nBC\r\nMB\r\nNB\r\nNL\r\nNT\r\nNS\r\nNU\r\nON\r\nPE\r\nQC\r\nSK\r\nYT";
			this.AddMultiple(s);
		}

		// Token: 0x040002D2 RID: 722
		private UnivDataAdapter da;

		// Token: 0x040002E8 RID: 744
		private int lookupGroupID;

		// Token: 0x040002E9 RID: 745
		private DataTable lookupListsTable;

		// Token: 0x040002EA RID: 746
		public bool cancelled = true;

		// Token: 0x040002EB RID: 747
		private int childList = 0;
	}
}
