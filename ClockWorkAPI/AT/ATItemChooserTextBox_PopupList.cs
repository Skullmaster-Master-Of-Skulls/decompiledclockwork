using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace ClockWorkAPI.AT
{
	// Token: 0x02000071 RID: 113
	public partial class ATItemChooserTextBox_PopupList : Form
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0001EC7C File Offset: 0x0001DC7C
		public ATItemChooserTextBox_PopupList()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001EC98 File Offset: 0x0001DC98
		public ListBox LB
		{
			get
			{
				return this.lb;
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060005DB RID: 1499 RVA: 0x0001ECB0 File Offset: 0x0001DCB0
		// (remove) Token: 0x060005DC RID: 1500 RVA: 0x0001ECEC File Offset: 0x0001DCEC
		public event ATItemChooserTextBox_PopupList.UserSelectedItemHandler OnUserSelectedItem;

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001ED28 File Offset: 0x0001DD28
		public bool IsShowing
		{
			get
			{
				return Form.ActiveForm == this;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001ED44 File Offset: 0x0001DD44
		public void ShowBest(string text)
		{
			int selectedIndex = this.lb.FindString(text);
			this.lb.SelectedIndex = selectedIndex;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001ED6C File Offset: 0x0001DD6C
		private void FireUserSelectedItem(string selectedText)
		{
			if (this.OnUserSelectedItem != null)
			{
				this.OnUserSelectedItem(this, selectedText);
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001ED98 File Offset: 0x0001DD98
		public void SimulateUpDown(int direction)
		{
			int selectedIndex = this.lb.SelectedIndex;
			int num = selectedIndex + direction;
			if (num < 0)
			{
				num = 0;
			}
			if (num >= this.lb.Items.Count)
			{
				num = this.lb.Items.Count - 1;
			}
			this.lb.SelectedIndex = num;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001EDFA File Offset: 0x0001DDFA
		public void SimulateEnter()
		{
			this.UserSelectedItem();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001EE04 File Offset: 0x0001DE04
		private void lb_DoubleClick(object sender, EventArgs e)
		{
			this.UserSelectedItem();
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001EE10 File Offset: 0x0001DE10
		private void UserSelectedItem()
		{
			if (this.lb.SelectedIndex >= 0)
			{
				ItemCategory itemCategory = (ItemCategory)this.lb.SelectedItem;
				this.FireUserSelectedItem(itemCategory.Title);
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001EE50 File Offset: 0x0001DE50
		private void lb_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lb.SelectedItem != null)
			{
				ItemCategory itemCategory = (ItemCategory)this.lb.SelectedItem;
			}
		}

		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x060005E8 RID: 1512
		public delegate void UserSelectedItemHandler(object sender, string selectedText);
	}
}
