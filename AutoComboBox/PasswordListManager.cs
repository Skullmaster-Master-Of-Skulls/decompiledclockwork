using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;
using EncryptionClassLibrary;
using UnivOleDb;

namespace AutoComboBox
{
	// Token: 0x02000105 RID: 261
	public partial class PasswordListManager : Form
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x0004F520 File Offset: 0x0004E520
		public PasswordListManager(UnivDataAdapter _Da, TripleDESEncryptionClass _TripleDES, int _PersonID, string _Username)
		{
			this.InitializeComponent();
			this.da = _Da;
			this.tripleDES = _TripleDES;
			this.personID = _PersonID;
			this.username = _Username;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0004FB9D File Offset: 0x0004EB9D
		private void PasswordListManager_Load(object sender, EventArgs e)
		{
			this.LoadUsers();
			this.UsersToScreen();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0004FBB0 File Offset: 0x0004EBB0
		private void LoadUsers()
		{
			this.da.SelectCommand.CommandText = "SELECT username,pass,personid FROM userinfo WHERE personid=@personid";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@personid", this.personID);
			this.usersTable = new DataTable();
			this.da.Fill(this.usersTable);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0004FC30 File Offset: 0x0004EC30
		private void UsersToScreen()
		{
			this.listView1.BeginUpdate();
			this.listView1.Items.Clear();
			foreach (object obj in this.usersTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					byte[] inputInBytes = (byte[])dataRow[0];
					string text = this.tripleDES.Decrypt(inputInBytes);
					ListViewItem listViewItem = new ListViewItem(text);
					listViewItem.Tag = dataRow;
					this.listView1.Items.Add(listViewItem);
				}
			}
			this.listView1.EndUpdate();
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0004FD14 File Offset: 0x0004ED14
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listView1.SelectedItems[0];
				DataRow dataRow = (DataRow)listViewItem.Tag;
				string userInputPassword = InputBox.GetUserInputPassword(this, "Change Password for " + listViewItem.Text, "Please enter a new password for " + listViewItem.Text + ":", "");
				if (userInputPassword != null && userInputPassword.Trim().Length > 0)
				{
					string userInputPassword2 = InputBox.GetUserInputPassword(this, "Change Password for " + listViewItem.Text, "Please enter your new password again for verification:", "");
					if (userInputPassword2 != null)
					{
						if (userInputPassword2.CompareTo(userInputPassword) == 0)
						{
							this.LoadUsers();
							this.UsersToScreen();
							MessageBox.Show("Password changed successfully.", "Password changed successfully.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else
						{
							MessageBox.Show("The passwords you just entered were different!  Nothing was done.", "Invalid password!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0004FE34 File Offset: 0x0004EE34
		private void btn_addUsername_Click(object sender, EventArgs e)
		{
			this.username = InputBox.GetUserInput(this, "New Username", "Please enter a username that can access your account:", this.username);
			if (this.username != null && this.username.Trim().Length > 0)
			{
				this.username = this.username.ToUpper();
				string text = InputBox.GetUserInputPassword(this, "New Username", "Please enter a password for this username:", "");
				if (text != null && text.Trim().Length > 0)
				{
					text = text.Trim();
					this.username = this.username.Trim();
					DataRow dataRow = this.usersTable.NewRow();
					byte[] array = this.tripleDES.Encrypt(this.username);
					byte[] array2 = this.tripleDES.Encrypt(text);
					this.da.SelectCommand.CommandText = "SELECT personid FROM userinfo WHERE username=@username UNION SELECT personid FROM people WHERE student_no=@username AND NOT personid=@personid";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@username", array);
					this.da.SelectCommand.Parameters.Add("@personid", this.personID);
					DataTable dataTable = new DataTable();
					this.da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						MessageBox.Show("This username already exists!  Please select another username.");
					}
					else
					{
						dataRow[0] = array;
						dataRow[1] = array2;
						dataRow[2] = this.personID;
						this.usersTable.Rows.Add(dataRow);
						dataRow.AcceptChanges();
						this.da.SelectCommand.CommandText = "INSERT INTO userinfo (username,pass,personid) VALUES (@username,@pass,@personid)";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@username", array);
						this.da.SelectCommand.Parameters.Add("@pass", array2);
						this.da.SelectCommand.Parameters.Add("@personid", this.personID);
						this.da.Fill(new DataTable());
						this.LoadUsers();
						this.UsersToScreen();
					}
				}
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000500AC File Offset: 0x0004F0AC
		private void btn_deleteUsername_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this username?", "Delete Username", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					DataRow dataRow = (DataRow)this.listView1.SelectedItems[0].Tag;
					int num = (int)dataRow[2];
					byte[] parameterValue = (byte[])dataRow[0];
					this.da.SelectCommand.CommandText = "DELETE FROM userinfo WHERE personid=@personid AND username=@ub";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@personid", num);
					this.da.SelectCommand.Parameters.Add("@ub", parameterValue);
					this.da.Fill(new DataTable());
					this.LoadUsers();
					this.UsersToScreen();
				}
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000501BE File Offset: 0x0004F1BE
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0400078E RID: 1934
		private UnivDataAdapter da;

		// Token: 0x0400078F RID: 1935
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000790 RID: 1936
		private int personID;

		// Token: 0x04000791 RID: 1937
		private string username;

		// Token: 0x04000798 RID: 1944
		private DataTable usersTable;
	}
}
