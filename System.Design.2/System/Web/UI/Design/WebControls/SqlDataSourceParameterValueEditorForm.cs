using System;
using System.Collections;
using System.Data;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000113 RID: 275
	internal partial class SqlDataSourceParameterValueEditorForm : DesignerForm
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x0003F09C File Offset: 0x0003D29C
		public SqlDataSourceParameterValueEditorForm(IServiceProvider serviceProvider, ParameterCollection parameters) : base(serviceProvider)
		{
			this._parameterItems = new ArrayList();
			foreach (object obj in parameters)
			{
				Parameter parameter = (Parameter)obj;
				this._parameterItems.Add(new SqlDataSourceParameterValueEditorForm.ParameterItem(parameter));
			}
			this.InitializeComponent();
			this.InitializeUI();
			string[] names = Enum.GetNames(typeof(TypeCode));
			Array.Sort<string>(names);
			DataGridViewComboBoxCell.ObjectCollection items = ((DataGridViewComboBoxColumn)this._parametersDataGridView.Columns[1]).Items;
			object[] items2 = names;
			items.AddRange(items2);
			string[] names2 = Enum.GetNames(typeof(DbType));
			Array.Sort<string>(names2);
			DataGridViewComboBoxCell.ObjectCollection items3 = ((DataGridViewComboBoxColumn)this._parametersDataGridView.Columns[2]).Items;
			items2 = names2;
			items3.AddRange(items2);
			this._parametersDataGridView.DataSource = this._parameterItems;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0003F1A4 File Offset: 0x0003D3A4
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.ParameterValueEditor";
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0003F57C File Offset: 0x0003D77C
		private void InitializeUI()
		{
			this._helpLabel.Text = SR.GetString("SqlDataSourceParameterValueEditorForm_HelpLabel");
			this._parametersDataGridView.AccessibleName = SR.GetString("SqlDataSourceParameterValueEditorForm_ParametersGridAccessibleName");
			this._cancelButton.Text = SR.GetString("Cancel");
			this._okButton.Text = SR.GetString("OK");
			this.Text = SR.GetString("SqlDataSourceParameterValueEditorForm_Caption");
			this._parametersDataGridView.Columns[0].HeaderText = SR.GetString("SqlDataSourceParameterValueEditorForm_ParameterColumnHeader");
			this._parametersDataGridView.Columns[1].HeaderText = SR.GetString("SqlDataSourceParameterValueEditorForm_TypeColumnHeader");
			this._parametersDataGridView.Columns[2].HeaderText = SR.GetString("SqlDataSourceParameterValueEditorForm_DbTypeColumnHeader");
			this._parametersDataGridView.Columns[3].HeaderText = SR.GetString("SqlDataSourceParameterValueEditorForm_ValueColumnHeader");
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0003F670 File Offset: 0x0003D870
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				int num = (int)Math.Floor((double)(this._parametersDataGridView.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 2 * SystemInformation.Border3DSize.Width) / 4.5);
				this._parametersDataGridView.Columns[0].Width = (int)((double)num * 1.5);
				this._parametersDataGridView.Columns[1].Width = num;
				this._parametersDataGridView.Columns[2].Width = num;
				this._parametersDataGridView.Columns[3].Width = num;
				this._parametersDataGridView.AutoResizeColumnHeadersHeight();
				for (int i = 0; i < this._parametersDataGridView.Rows.Count; i++)
				{
					this._parametersDataGridView.AutoResizeRow(i, DataGridViewAutoSizeRowMode.AllCells);
				}
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0003F768 File Offset: 0x0003D968
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			ParameterCollection parameterCollection = new ParameterCollection();
			foreach (object obj in this._parameterItems)
			{
				SqlDataSourceParameterValueEditorForm.ParameterItem parameterItem = (SqlDataSourceParameterValueEditorForm.ParameterItem)obj;
				if (parameterItem.Parameter.DbType == DbType.Object)
				{
					parameterCollection.Add(new Parameter(parameterItem.Parameter.Name, parameterItem.Parameter.Type, parameterItem.Parameter.DefaultValue));
				}
				else
				{
					parameterCollection.Add(new Parameter(parameterItem.Parameter.Name, parameterItem.Parameter.DbType, parameterItem.Parameter.DefaultValue));
				}
			}
			try
			{
				parameterCollection.GetValues(null, null);
			}
			catch (Exception ex)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, ex, SR.GetString("SqlDataSourceParameterValueEditorForm_InvalidParameter"));
				return;
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x04000603 RID: 1539
		private ArrayList _parameterItems;

		// Token: 0x02000448 RID: 1096
		private class ParameterItem
		{
			// Token: 0x0600290F RID: 10511 RVA: 0x000F9940 File Offset: 0x000F7B40
			public ParameterItem(Parameter parameter)
			{
				this._parameter = parameter;
			}

			// Token: 0x170008A9 RID: 2217
			// (get) Token: 0x06002910 RID: 10512 RVA: 0x000F9950 File Offset: 0x000F7B50
			// (set) Token: 0x06002911 RID: 10513 RVA: 0x000F9976 File Offset: 0x000F7B76
			public string DbType
			{
				get
				{
					return this._parameter.DbType.ToString();
				}
				set
				{
					this._parameter.DbType = (DbType)Enum.Parse(typeof(DbType), value);
				}
			}

			// Token: 0x170008AA RID: 2218
			// (get) Token: 0x06002912 RID: 10514 RVA: 0x000F9998 File Offset: 0x000F7B98
			// (set) Token: 0x06002913 RID: 10515 RVA: 0x000F99A5 File Offset: 0x000F7BA5
			public string DefaultValue
			{
				get
				{
					return this._parameter.DefaultValue;
				}
				set
				{
					this._parameter.DefaultValue = value;
				}
			}

			// Token: 0x170008AB RID: 2219
			// (get) Token: 0x06002914 RID: 10516 RVA: 0x000F99B3 File Offset: 0x000F7BB3
			// (set) Token: 0x06002915 RID: 10517 RVA: 0x000F99C0 File Offset: 0x000F7BC0
			public string Name
			{
				get
				{
					return this._parameter.Name;
				}
				set
				{
					this._parameter.Name = value;
				}
			}

			// Token: 0x170008AC RID: 2220
			// (get) Token: 0x06002916 RID: 10518 RVA: 0x000F99CE File Offset: 0x000F7BCE
			public Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x170008AD RID: 2221
			// (get) Token: 0x06002917 RID: 10519 RVA: 0x000F99D8 File Offset: 0x000F7BD8
			// (set) Token: 0x06002918 RID: 10520 RVA: 0x000F99FE File Offset: 0x000F7BFE
			public string Type
			{
				get
				{
					return this._parameter.Type.ToString();
				}
				set
				{
					this._parameter.Type = (TypeCode)Enum.Parse(typeof(TypeCode), value);
				}
			}

			// Token: 0x04001D1B RID: 7451
			private Parameter _parameter;
		}
	}
}
