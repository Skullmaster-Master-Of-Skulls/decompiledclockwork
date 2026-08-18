using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.LookupCourses;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.LookupCourses;

namespace AutoComboBox.HelperForms
{
	// Token: 0x02000085 RID: 133
	public partial class FrmAccommodationExpiryPresetDatesEdit : Form
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x0002C1F9 File Offset: 0x0002B1F9
		public FrmAccommodationExpiryPresetDatesEdit()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002C214 File Offset: 0x0002B214
		public void Init(IList<DateTime> presetDates)
		{
			this.listView1.BeginUpdate();
			try
			{
				this.listView1.Items.Clear();
				foreach (DateTime dateTime in presetDates)
				{
					ListViewItem value = new ListViewItem(dateTime.ToString("MMMM d"))
					{
						Tag = dateTime.Date
					};
					this.listView1.Items.Add(value);
				}
			}
			finally
			{
				this.listView1.EndUpdate();
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002C2DC File Offset: 0x0002B2DC
		private void btn_save_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0002C2EE File Offset: 0x0002B2EE
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0002C2F8 File Offset: 0x0002B2F8
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0002C302 File Offset: 0x0002B302
		private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.RemoveSelectedDates();
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0002C30C File Offset: 0x0002B30C
		private void addDateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AddDate();
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0002C316 File Offset: 0x0002B316
		private void btn_addNewDate_Click(object sender, EventArgs e)
		{
			this.AddDate();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0002C320 File Offset: 0x0002B320
		private void btn_removeSelectedDate_Click(object sender, EventArgs e)
		{
			this.RemoveSelectedDates();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0002C32C File Offset: 0x0002B32C
		private void RemoveSelectedDates()
		{
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (object obj in this.listView1.SelectedItems)
			{
				ListViewItem item = (ListViewItem)obj;
				list.Add(item);
			}
			this.listView1.BeginUpdate();
			try
			{
				foreach (ListViewItem item in list)
				{
					this.listView1.Items.Remove(item);
				}
			}
			finally
			{
				this.listView1.EndUpdate();
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0002C41C File Offset: 0x0002B41C
		private void AddDate()
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			SessionDTO currentSession = sessionClientManager.GetCurrentSession();
			DateRangeInput dateRangeInput = new DateRangeInput("Please enter a new preset expiry date:", "Add new preset expiry date", currentSession.EndDate.Date, currentSession.EndDate.Date);
			DialogResult dialogResult = dateRangeInput.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DateTime date = dateRangeInput.StartDate.Date;
				IList<DateTime> selectedDates = this.SelectedDates;
				DateTime? dateTime = null;
				foreach (DateTime dateTime2 in selectedDates)
				{
					if (dateTime2.Date == date)
					{
						dateTime = new DateTime?(dateTime2.Date);
						break;
					}
				}
				if (dateTime == null)
				{
					ListViewItem listViewItem = new ListViewItem(date.ToString("MMMM d"))
					{
						Tag = date
					};
					this.listView1.Items.Add(listViewItem);
					listViewItem.Selected = true;
					listViewItem.EnsureVisible();
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0002C588 File Offset: 0x0002B588
		public IList<DateTime> SelectedDates
		{
			get
			{
				List<DateTime> list = new List<DateTime>();
				foreach (object obj in this.listView1.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					list.Add((DateTime)listViewItem.Tag);
				}
				list.Sort((DateTime d1, DateTime d2) => d1.CompareTo(d2));
				return list;
			}
		}
	}
}
