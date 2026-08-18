using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls.ListControls
{
	// Token: 0x02000522 RID: 1314
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class DataGridGeneralPage : BaseDataListPage
	{
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x00109D84 File Offset: 0x00108D84
		protected override string HelpKeyword
		{
			get
			{
				return "net.Asp.DataGridProperties.General";
			}
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x00109D8C File Offset: 0x00108D8C
		private void InitForm()
		{
			GroupLabel groupLabel = new GroupLabel();
			this.showHeaderCheck = new System.Windows.Forms.CheckBox();
			this.showFooterCheck = new System.Windows.Forms.CheckBox();
			GroupLabel groupLabel2 = new GroupLabel();
			this.allowSortingCheck = new System.Windows.Forms.CheckBox();
			groupLabel.SetBounds(4, 4, 431, 16);
			groupLabel.Text = SR.GetString("DGGen_HeaderFooterGroup");
			groupLabel.TabIndex = 8;
			groupLabel.TabStop = false;
			this.showHeaderCheck.SetBounds(12, 24, 160, 16);
			this.showHeaderCheck.TabIndex = 9;
			this.showHeaderCheck.Text = SR.GetString("DGGen_ShowHeader");
			this.showHeaderCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.showHeaderCheck.FlatStyle = FlatStyle.System;
			this.showHeaderCheck.CheckedChanged += this.OnCheckChangedShowHeader;
			this.showFooterCheck.SetBounds(12, 44, 160, 16);
			this.showFooterCheck.TabIndex = 10;
			this.showFooterCheck.Text = SR.GetString("DGGen_ShowFooter");
			this.showFooterCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.showFooterCheck.FlatStyle = FlatStyle.System;
			this.showFooterCheck.CheckedChanged += this.OnCheckChangedShowFooter;
			groupLabel2.SetBounds(4, 70, 431, 16);
			groupLabel2.Text = SR.GetString("DGGen_BehaviorGroup");
			groupLabel2.TabIndex = 11;
			groupLabel2.TabStop = false;
			this.allowSortingCheck.SetBounds(12, 88, 160, 16);
			this.allowSortingCheck.Text = SR.GetString("DGGen_AllowSorting");
			this.allowSortingCheck.TabIndex = 12;
			this.allowSortingCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.allowSortingCheck.FlatStyle = FlatStyle.System;
			this.allowSortingCheck.CheckedChanged += this.OnCheckChangedAllowSorting;
			this.Text = SR.GetString("DGGen_Text");
			base.AccessibleDescription = SR.GetString("DGGen_Desc");
			base.Size = new Size(464, 272);
			base.CommitOnDeactivate = true;
			base.Icon = new Icon(base.GetType(), "DataGridGeneralPage.ico");
			base.Controls.Clear();
			base.Controls.AddRange(new Control[]
			{
				this.allowSortingCheck,
				groupLabel2,
				this.showFooterCheck,
				this.showHeaderCheck,
				groupLabel
			});
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x00109FED File Offset: 0x00108FED
		private void InitPage()
		{
			this.showHeaderCheck.Checked = false;
			this.showFooterCheck.Checked = false;
			this.allowSortingCheck.Checked = false;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x0010A014 File Offset: 0x00109014
		protected override void LoadComponent()
		{
			this.InitPage();
			System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
			this.showHeaderCheck.Checked = dataGrid.ShowHeader;
			this.showFooterCheck.Checked = dataGrid.ShowFooter;
			this.allowSortingCheck.Checked = dataGrid.AllowSorting;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x0010A066 File Offset: 0x00109066
		private void OnCheckChangedShowHeader(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.SetDirty();
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x0010A077 File Offset: 0x00109077
		private void OnCheckChangedShowFooter(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.SetDirty();
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x0010A088 File Offset: 0x00109088
		private void OnCheckChangedAllowSorting(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.SetDirty();
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x0010A09C File Offset: 0x0010909C
		protected override void SaveComponent()
		{
			System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
			dataGrid.ShowHeader = this.showHeaderCheck.Checked;
			dataGrid.ShowFooter = this.showFooterCheck.Checked;
			dataGrid.AllowSorting = this.allowSortingCheck.Checked;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0010A0E8 File Offset: 0x001090E8
		public override void SetComponent(IComponent component)
		{
			base.SetComponent(component);
			this.InitForm();
		}

		// Token: 0x04001FC1 RID: 8129
		private System.Windows.Forms.CheckBox showHeaderCheck;

		// Token: 0x04001FC2 RID: 8130
		private System.Windows.Forms.CheckBox showFooterCheck;

		// Token: 0x04001FC3 RID: 8131
		private System.Windows.Forms.CheckBox allowSortingCheck;
	}
}
