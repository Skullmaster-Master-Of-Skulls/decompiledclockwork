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
	// Token: 0x0200015A RID: 346
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class DataGridGeneralPage : BaseDataListPage
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0004DD4E File Offset: 0x0004BF4E
		protected override string HelpKeyword
		{
			get
			{
				return "net.Asp.DataGridProperties.General";
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0004DD58 File Offset: 0x0004BF58
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
			base.Icon = BitmapSelector.CreateIcon(base.GetType(), "DataGridGeneralPage.ico");
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

		// Token: 0x06000C22 RID: 3106 RVA: 0x0004DFB7 File Offset: 0x0004C1B7
		private void InitPage()
		{
			this.showHeaderCheck.Checked = false;
			this.showFooterCheck.Checked = false;
			this.allowSortingCheck.Checked = false;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0004DFE0 File Offset: 0x0004C1E0
		protected override void LoadComponent()
		{
			this.InitPage();
			System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
			this.showHeaderCheck.Checked = dataGrid.ShowHeader;
			this.showFooterCheck.Checked = dataGrid.ShowFooter;
			this.allowSortingCheck.Checked = dataGrid.AllowSorting;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0004BFDC File Offset: 0x0004A1DC
		private void OnCheckChangedShowHeader(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.SetDirty();
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0004BFDC File Offset: 0x0004A1DC
		private void OnCheckChangedShowFooter(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.SetDirty();
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0004BFDC File Offset: 0x0004A1DC
		private void OnCheckChangedAllowSorting(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.SetDirty();
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0004E034 File Offset: 0x0004C234
		protected override void SaveComponent()
		{
			System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
			dataGrid.ShowHeader = this.showHeaderCheck.Checked;
			dataGrid.ShowFooter = this.showFooterCheck.Checked;
			dataGrid.AllowSorting = this.allowSortingCheck.Checked;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0004E080 File Offset: 0x0004C280
		public override void SetComponent(IComponent component)
		{
			base.SetComponent(component);
			this.InitForm();
		}

		// Token: 0x04000747 RID: 1863
		private System.Windows.Forms.CheckBox showHeaderCheck;

		// Token: 0x04000748 RID: 1864
		private System.Windows.Forms.CheckBox showFooterCheck;

		// Token: 0x04000749 RID: 1865
		private System.Windows.Forms.CheckBox allowSortingCheck;
	}
}
