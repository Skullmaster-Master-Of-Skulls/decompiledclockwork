using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ClockWorkAPI.Properties;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace ClockWorkAPI.AT2
{
	// Token: 0x02000078 RID: 120
	public partial class ItemEdit : Form
	{
		// Token: 0x0600061F RID: 1567 RVA: 0x00020FA5 File Offset: 0x0001FFA5
		public ItemEdit()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00020FBE File Offset: 0x0001FFBE
		public ItemEdit(Item item)
		{
			this.item = item;
			this.InitializeComponent();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00020FDE File Offset: 0x0001FFDE
		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00020FE8 File Offset: 0x0001FFE8
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			this.ScreenToItem();
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00021001 File Offset: 0x00020001
		private void ItemEdit_Load(object sender, EventArgs e)
		{
			this.ItemToScreen();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002100C File Offset: 0x0002000C
		private void ItemToScreen()
		{
			if (this.item != null)
			{
				this.oldCategory = this.item.Category;
				this.oldTitle = this.item.Title;
				this.oldVendor = this.item.Vendor;
				this.oldCost = this.item.Cost;
				this.oldDescription = this.item.Description;
				this.txt_category.Text = this.oldCategory;
				this.txt_item.Text = this.oldTitle;
				this.txt_vendor.Text = this.oldVendor;
				this.txt_cost.Text = this.oldCost.ToString();
				this.txt_description.Text = this.oldDescription;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000210E0 File Offset: 0x000200E0
		private void ScreenToItem()
		{
			if (this.item != null)
			{
				string text = this.txt_category.Text;
				string text2 = this.txt_item.Text;
				string text3 = this.txt_vendor.Text;
				decimal num;
				try
				{
					num = Math.Round(Convert.ToDecimal(this.txt_cost.Text), 2);
				}
				catch
				{
					num = 0m;
				}
				string text4 = this.txt_description.Text;
				if (text != this.oldCategory || text2 != this.oldTitle || text3 != this.oldVendor || num != this.oldCost || text4 != this.oldDescription)
				{
					this.item.Title = text2;
					this.item.Category = text;
					this.item.Vendor = text3;
					this.item.Cost = num;
					this.item.Description = text4;
				}
			}
		}

		// Token: 0x04000321 RID: 801
		private Item item;

		// Token: 0x04000322 RID: 802
		private string oldCategory;

		// Token: 0x04000323 RID: 803
		private string oldTitle;

		// Token: 0x04000324 RID: 804
		private string oldVendor;

		// Token: 0x04000325 RID: 805
		private string oldDescription;

		// Token: 0x04000326 RID: 806
		private decimal oldCost;
	}
}
