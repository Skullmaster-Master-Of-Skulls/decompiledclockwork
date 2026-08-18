using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;
using UnivOleDb;

namespace AutoComboBox
{
	// Token: 0x0200006A RID: 106
	public partial class ImportSettingsDialog : Form
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0001F2FC File Offset: 0x0001E2FC
		public ImportSettingsDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000208D0 File Offset: 0x0001F8D0
		private void btn_findServer_Click(object sender, EventArgs e)
		{
			this.RefreshServers();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000208DC File Offset: 0x0001F8DC
		private void RefreshServers()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				string[] servers = SqlLocator.GetServers();
				this.cmb_server.Items.Clear();
				foreach (string item in servers)
				{
					this.cmb_server.Items.Add(item);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
			this.Cursor = Cursors.Default;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00020980 File Offset: 0x0001F980
		private void btn_testConnection_Click(object sender, EventArgs e)
		{
			this.TestConnection(true);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0002098C File Offset: 0x0001F98C
		private UnivDataAdapter TestConnection(bool showSuccess)
		{
			this.Cursor = Cursors.WaitCursor;
			UnivDataAdapter univDataAdapter = null;
			UnivConnection univConnection = null;
			string connectionStringOnscreen = this.GetConnectionStringOnscreen();
			ArrayList arrayList = new ArrayList();
			if (connectionStringOnscreen != null && connectionStringOnscreen.Length > 0)
			{
				try
				{
					univConnection = UnivOleDbFactory.CreateConnection(connectionStringOnscreen);
				}
				catch (Exception ex)
				{
					univConnection = null;
					arrayList.Add("Invalid connectionstring! [" + ex.Message + "]");
				}
				if (univConnection != null)
				{
					univDataAdapter = univConnection.CreateDataAdapter();
					univDataAdapter.SelectCommand.CommandText = "SELECT * FROM people WHERE 1=0";
					DataTable dataTable = new DataTable();
					try
					{
						univDataAdapter.Fill(dataTable);
					}
					catch (Exception ex2)
					{
						dataTable = null;
						arrayList.Add("Able to open a connection to the database, but not able to select data from a table (possible database permissions problem?) [" + ex2.Message + "]");
					}
					if (dataTable == null || dataTable.Columns.Count <= 0)
					{
						arrayList.Add("Empty results table (no columns) from a sql select command.");
					}
				}
			}
			else
			{
				arrayList.Add("Empty connection string!  Please ensure all fields are filled in.");
			}
			UnivDataAdapter result;
			if (arrayList.Count > 0)
			{
				string text = "";
				foreach (object obj in arrayList)
				{
					string str = (string)obj;
					text += Environment.NewLine;
					text += str;
				}
				MessageBox.Show("Database Connection Failed!" + text, "Connection Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.Cursor = Cursors.Default;
				result = univDataAdapter;
			}
			else
			{
				if (showSuccess)
				{
					MessageBox.Show("Database Connection Succeeded!", "ClockWork Connection Settings", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				this.Cursor = Cursors.Default;
				result = univDataAdapter;
			}
			return result;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00020BA0 File Offset: 0x0001FBA0
		private string GetConnectionStringOnscreen()
		{
			if (this.cmb_databaseTypes.SelectedIndex < 0)
			{
				this.cmb_databaseTypes.SelectedIndex = 0;
			}
			int selectedIndex = this.cmb_databaseTypes.SelectedIndex;
			string text;
			if (selectedIndex != 0)
			{
				text = null;
			}
			else
			{
				text = "Provider=SQLOLEDB.1;Data Source=" + this.cmb_server.Text.Trim();
				text = text + ";Initial Catalog=" + this.txt_databaseName.Text.Trim();
				if (this.chk_useIntegratedSecurity.Checked)
				{
					text += ";Integrated Security=SSPI;Persist Security Info=False";
				}
				else
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						";Persist Security Info=True;User ID=",
						this.txt_username.Text.Trim(),
						";Password=",
						this.txt_password.Text
					});
				}
			}
			return text;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00020C9C File Offset: 0x0001FC9C
		private void MENU_viewConnectionString_Click(object sender, EventArgs e)
		{
			string text = this.GetConnectionStringOnscreen();
			if (text == null)
			{
				text = "";
			}
			MessageBox.Show(text);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00020CC9 File Offset: 0x0001FCC9
		private void menuItem5_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00020CD3 File Offset: 0x0001FCD3
		private void btn_save_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00020CD6 File Offset: 0x0001FCD6
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
