using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using AutoComboBox;
using ClockWorkAPI.Properties;
using DevComponents.DotNetBar;
using Microsoft.Win32;

namespace ClockWorkAPI
{
	// Token: 0x0200007B RID: 123
	public partial class ClockWorkLoginDialog : Form
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x00022C9C File Offset: 0x00021C9C
		public ClockWorkLoginDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002390A File Offset: 0x0002290A
		private void txt_pass_KeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00023910 File Offset: 0x00022910
		private void Save()
		{
			if (this.txt_pass.Text.Trim().Length < 1)
			{
				this.ShowMessage("Please enter a password first.");
				base.ActiveControl = null;
				base.ActiveControl = this.txt_pass;
			}
			else if (this.txt_user.Text.Trim().Length < 1)
			{
				this.ShowMessage("Please enter a username first.");
				base.ActiveControl = null;
				base.ActiveControl = this.txt_user;
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000239B8 File Offset: 0x000229B8
		private void ClockWorkLoginDialog_Load(object sender, EventArgs e)
		{
			if (this.txt_user.Text.Length > 0)
			{
				base.ActiveControl = this.txt_pass;
			}
			else
			{
				base.ActiveControl = this.txt_user;
			}
			if (ClockWorkCore.UseAccessibleColoursForToolstrip)
			{
				this.toolStrip1.Renderer = new ToolStripProfessionalRenderer(new AccessibleProfessionalColours());
			}
			base.Activate();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00023A2B File Offset: 0x00022A2B
		public void DisableUsernameField()
		{
			this.txt_user.Enabled = false;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00023A3B File Offset: 0x00022A3B
		public void ShowMessage(string msg)
		{
			base.Height = 328;
			this.lbl_message.Text = msg;
			this.lbl_message.Visible = true;
			this.Text = "ClockWork Login " + msg;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00023A76 File Offset: 0x00022A76
		private void txt_user_KeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00023A79 File Offset: 0x00022A79
		private void btn_fake_Click(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00023A83 File Offset: 0x00022A83
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00023A90 File Offset: 0x00022A90
		public string UserOriginal
		{
			get
			{
				return this.txt_user.Text;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00023AB0 File Offset: 0x00022AB0
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00023AD2 File Offset: 0x00022AD2
		public string User
		{
			get
			{
				return this.txt_user.Text.ToUpper();
			}
			set
			{
				this.txt_user.Text = value;
				base.ActiveControl = this.txt_pass;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00023AF0 File Offset: 0x00022AF0
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x00023B0D File Offset: 0x00022B0D
		public string Pass
		{
			get
			{
				return this.txt_pass.Text;
			}
			set
			{
				this.txt_pass.Text = value;
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00023B1D File Offset: 0x00022B1D
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00023B27 File Offset: 0x00022B27
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00023B31 File Offset: 0x00022B31
		private void btn_changePassword_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Retry;
			base.Close();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00023B44 File Offset: 0x00022B44
		private void txt_pass_Enter(object sender, EventArgs e)
		{
			if (this.txt_pass.Text.Trim().Length > 0)
			{
				this.txt_pass.SelectionStart = 0;
				this.txt_pass.SelectionLength = this.txt_pass.Text.Length;
			}
			if (!AutomationInteropProvider.ClientsAreListening && Console.CapsLock)
			{
				this.balloonTip_capsLock.Enabled = true;
				this.balloonTip_capsLock.ShowBalloon(this.txt_pass);
			}
			else
			{
				this.balloonTip_capsLock.Enabled = false;
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00023BE4 File Offset: 0x00022BE4
		private void txt_user_Enter(object sender, EventArgs e)
		{
			if (this.txt_user.Text.Trim().Length > 0)
			{
				this.txt_user.SelectionStart = 0;
				this.txt_user.SelectionLength = this.txt_user.Text.Length;
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00023C3C File Offset: 0x00022C3C
		private void balloonTip_capsLock_BalloonDisplaying(object sender, EventArgs e)
		{
			if (!Console.CapsLock)
			{
				this.balloonTip_capsLock.CloseBalloon();
				this.balloonTip_capsLock.Enabled = false;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00023C70 File Offset: 0x00022C70
		private void lbl_message_Enter(object sender, EventArgs e)
		{
			if (!this.lbl_message.Visible)
			{
				base.ActiveControl = this.toolStrip1;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00023CC8 File Offset: 0x00022CC8
		private void lbl_banner_DoubleClick(object sender, EventArgs e)
		{
			List<ConnectionFavourite> connectionFavourites = this.GetConnectionFavourites();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Favourite");
			for (int i = 0; i < connectionFavourites.Count; i++)
			{
				ConnectionFavourite connectionFavourite = connectionFavourites[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = connectionFavourite.Name;
				dataTable.Rows.Add(dataRow);
			}
			DataView dataView = new DataView();
			dataTable.TableName = "Favourites";
			dataView.Table = dataTable;
			dataView.Sort = "Favourite";
			InputListView inputListView = new InputListView("Change ClockWork Instance", "Please select a ClockWork Instance", dataView, -1, false, false);
			DialogResult dialogResult = inputListView.ShowDialog(this);
			if (dialogResult == DialogResult.OK && inputListView.LV.SelectedItems.Count > 0)
			{
				DataRow dataRow2 = (DataRow)inputListView.LV.SelectedItems[0].Tag;
				string name = dataRow2[0].ToString();
				ConnectionFavourite connectionFavourite2 = connectionFavourites.Find((ConnectionFavourite fv) => fv.Name.Equals(name));
				if (connectionFavourite2 != null)
				{
					this.ConnectionFavourite = connectionFavourite2;
					base.Close();
				}
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00023E44 File Offset: 0x00022E44
		private List<ConnectionFavourite> GetConnectionFavourites()
		{
			RegistryKey registryKey = ClockWorkCore.GetRegistryKey(Registry.CurrentUser, ClockWorkCore.registryBreakdown, true, true);
			if (registryKey != null)
			{
				registryKey = registryKey.OpenSubKey("mc");
				if (registryKey != null)
				{
					string[] array = new string[ClockWorkCore.registryBreakdown.Length + 1];
					for (int i = 0; i < array.Length - 1; i++)
					{
						array[i] = ClockWorkCore.registryBreakdown[i];
					}
					array[array.Length - 1] = "mc";
					string[] valueNames = registryKey.GetValueNames();
					if (valueNames != null && valueNames.Length > 0)
					{
						List<ConnectionFavourite> list = new List<ConnectionFavourite>(Convert.ToInt32(valueNames.Length / 2) + 1);
						foreach (string text in valueNames)
						{
							int num = text.LastIndexOf('_');
							if (num > 0)
							{
								string name = text.Substring(0, num);
								ConnectionFavourite connectionFavourite = list.Find((ConnectionFavourite f) => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
								if (connectionFavourite == null)
								{
									string registryValueString = ClockWorkCore.GetRegistryValueString(Registry.CurrentUser, array, name + "_cs", true);
									string registryValueString2 = ClockWorkCore.GetRegistryValueString(Registry.CurrentUser, array, name + "_k", true);
									ConnectionFavourite item = new ConnectionFavourite(name, registryValueString, registryValueString2);
									list.Add(item);
								}
							}
						}
						return list;
					}
				}
			}
			return new List<ConnectionFavourite>();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00023FF2 File Offset: 0x00022FF2
		private void lbl_banner_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00023FF5 File Offset: 0x00022FF5
		private void link_help_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		// Token: 0x0400033D RID: 829
		public ConnectionFavourite ConnectionFavourite = null;
	}
}
