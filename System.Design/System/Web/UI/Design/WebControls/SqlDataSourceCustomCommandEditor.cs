using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Data;
using System.Data;
using System.Data.Common;
using System.Design;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004C6 RID: 1222
	internal class SqlDataSourceCustomCommandEditor : UserControl
	{
		// Token: 0x06002C32 RID: 11314 RVA: 0x000F7A92 File Offset: 0x000F6A92
		public SqlDataSourceCustomCommandEditor()
		{
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000F7AA8 File Offset: 0x000F6AA8
		public bool HasQuery
		{
			get
			{
				if (this._sqlRadioButton.Checked)
				{
					return this._commandTextBox.Text.Trim().Length > 0;
				}
				SqlDataSourceCustomCommandEditor.StoredProcedureItem storedProcedureItem = this._storedProcedureComboBox.SelectedItem as SqlDataSourceCustomCommandEditor.StoredProcedureItem;
				return storedProcedureItem != null;
			}
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06002C34 RID: 11316 RVA: 0x000F7AF3 File Offset: 0x000F6AF3
		// (remove) Token: 0x06002C35 RID: 11317 RVA: 0x000F7B06 File Offset: 0x000F6B06
		public event EventHandler CommandChanged
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceCustomCommandEditor.EventCommandChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceCustomCommandEditor.EventCommandChanged, value);
			}
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000F7B1C File Offset: 0x000F6B1C
		private void InitializeComponent()
		{
			this._commandTextBox = new System.Windows.Forms.TextBox();
			this._queryBuilderButton = new System.Windows.Forms.Button();
			this._sqlRadioButton = new System.Windows.Forms.RadioButton();
			this._storedProcedureRadioButton = new System.Windows.Forms.RadioButton();
			this._storedProcedureComboBox = new AutoSizeComboBox();
			this._storedProcedurePanel = new System.Windows.Forms.Panel();
			this._sqlPanel = new System.Windows.Forms.Panel();
			this._storedProcedurePanel.SuspendLayout();
			this._sqlPanel.SuspendLayout();
			base.SuspendLayout();
			this._sqlRadioButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._sqlRadioButton.Location = new Point(12, 12);
			this._sqlRadioButton.Name = "_sqlRadioButton";
			this._sqlRadioButton.Size = new Size(489, 20);
			this._sqlRadioButton.TabIndex = 10;
			this._sqlRadioButton.CheckedChanged += this.OnSqlRadioButtonCheckedChanged;
			this._sqlPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._sqlPanel.Controls.Add(this._queryBuilderButton);
			this._sqlPanel.Controls.Add(this._commandTextBox);
			this._sqlPanel.Location = new Point(28, 32);
			this._sqlPanel.Name = "_sqlPanel";
			this._sqlPanel.Size = new Size(480, 121);
			this._sqlPanel.TabIndex = 20;
			this._storedProcedureRadioButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._storedProcedureRadioButton.Location = new Point(12, 160);
			this._storedProcedureRadioButton.Name = "_storedProcedureRadioButton";
			this._storedProcedureRadioButton.Size = new Size(489, 20);
			this._storedProcedureRadioButton.TabIndex = 30;
			this._storedProcedurePanel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._storedProcedurePanel.Controls.Add(this._storedProcedureComboBox);
			this._storedProcedurePanel.Location = new Point(28, 180);
			this._storedProcedurePanel.Name = "_storedProcedurePanel";
			this._storedProcedurePanel.Size = new Size(265, 21);
			this._storedProcedurePanel.TabIndex = 40;
			this._commandTextBox.AcceptsReturn = true;
			this._commandTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._commandTextBox.Location = new Point(0, 0);
			this._commandTextBox.Multiline = true;
			this._commandTextBox.Name = "_commandTextBox";
			this._commandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._commandTextBox.Size = new Size(480, 93);
			this._commandTextBox.TabIndex = 20;
			this._commandTextBox.TextChanged += this.OnCommandTextBoxTextChanged;
			this._queryBuilderButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this._queryBuilderButton.Location = new Point(330, 98);
			this._queryBuilderButton.Name = "_queryBuilderButton";
			this._queryBuilderButton.Size = new Size(150, 23);
			this._queryBuilderButton.TabIndex = 30;
			this._queryBuilderButton.Click += this.OnQueryBuilderButtonClick;
			this._storedProcedureComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._storedProcedureComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this._storedProcedureComboBox.Location = new Point(0, 0);
			this._storedProcedureComboBox.Name = "_storedProcedureComboBox";
			this._storedProcedureComboBox.Size = new Size(265, 21);
			this._storedProcedureComboBox.TabIndex = 10;
			this._storedProcedureComboBox.SelectedIndexChanged += this.OnStoredProcedureComboBoxSelectedIndexChanged;
			base.Controls.Add(this._sqlRadioButton);
			base.Controls.Add(this._sqlPanel);
			base.Controls.Add(this._storedProcedureRadioButton);
			base.Controls.Add(this._storedProcedurePanel);
			base.Name = "SqlDataSourceCustomCommandEditor";
			base.Size = new Size(522, 230);
			this._storedProcedurePanel.ResumeLayout(false);
			this._sqlPanel.ResumeLayout(false);
			this._sqlPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000F7F48 File Offset: 0x000F6F48
		public SqlDataSourceQuery GetQuery()
		{
			Cursor value = Cursor.Current;
			SqlDataSourceQuery result;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (this._sqlRadioButton.Checked)
				{
					if (this._commandTextBox.Text.Trim().Length > 0)
					{
						SqlDataSourceCommandType commandType;
						if (string.Equals(this._commandTextBox.Text, this._originalCommand, StringComparison.OrdinalIgnoreCase))
						{
							commandType = this._commandType;
						}
						else
						{
							commandType = SqlDataSourceCommandType.Text;
						}
						DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this._dataConnection.ProviderName);
						ICollection parameters;
						if (this._editorMode == QueryBuilderMode.Select || SqlDataSourceDesigner.SupportsNamedParameters(dbProviderFactory))
						{
							Parameter[] array = this._sqlDataSourceDesigner.InferParameterNames(this._dataConnection, this._commandTextBox.Text, commandType);
							if (array == null)
							{
								return null;
							}
							ArrayList newParameters = new ArrayList(array);
							parameters = this.MergeParameters(this._parameters, newParameters, SqlDataSourceDesigner.SupportsNamedParameters(dbProviderFactory));
						}
						else
						{
							parameters = this._parameters;
						}
						result = new SqlDataSourceQuery(this._commandTextBox.Text, commandType, parameters);
					}
					else
					{
						result = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
					}
				}
				else
				{
					SqlDataSourceCustomCommandEditor.StoredProcedureItem storedProcedureItem = this._storedProcedureComboBox.SelectedItem as SqlDataSourceCustomCommandEditor.StoredProcedureItem;
					if (storedProcedureItem == null)
					{
						result = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						ICollection collection = null;
						try
						{
							collection = storedProcedureItem.DesignerDataStoredProcedure.Parameters;
						}
						catch (Exception ex)
						{
							UIServiceHelper.ShowError(this._sqlDataSourceDesigner.Component.Site, ex, SR.GetString("SqlDataSourceCustomCommandEditor_CouldNotGetStoredProcedureSchema"));
							return null;
						}
						DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this._dataConnection.ProviderName);
						if (collection != null && collection.Count > 0)
						{
							foreach (object obj in collection)
							{
								DesignerDataParameter designerDataParameter = (DesignerDataParameter)obj;
								string name = SqlDataSourceDesigner.StripParameterPrefix(designerDataParameter.Name);
								Parameter parameter = SqlDataSourceDesigner.CreateParameter(dbProviderFactory, name, designerDataParameter.DataType);
								parameter.Direction = designerDataParameter.Direction;
								arrayList.Add(parameter);
							}
						}
						ICollection parameters2 = this.MergeParameters(this._parameters, arrayList, SqlDataSourceDesigner.SupportsNamedParameters(dbProviderFactory));
						result = new SqlDataSourceQuery(storedProcedureItem.DesignerDataStoredProcedure.Name, SqlDataSourceCommandType.StoredProcedure, parameters2);
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
			return result;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000F81E0 File Offset: 0x000F71E0
		private void InitializeUI()
		{
			this._queryBuilderButton.Text = SR.GetString("SqlDataSourceCustomCommandEditor_QueryBuilderButton");
			this._sqlRadioButton.Text = SR.GetString("SqlDataSourceCustomCommandEditor_SqlLabel");
			this._storedProcedureRadioButton.Text = SR.GetString("SqlDataSourceCustomCommandEditor_StoredProcedureLabel");
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000F822C File Offset: 0x000F722C
		private ICollection MergeParameters(ICollection originalParameters, ArrayList newParameters, bool useNamedParameters)
		{
			List<Parameter> list = new List<Parameter>();
			foreach (object obj in originalParameters)
			{
				Parameter item = (Parameter)obj;
				list.Add(item);
			}
			List<Parameter> list2 = new List<Parameter>();
			for (int i = 0; i < newParameters.Count; i++)
			{
				Parameter parameter = (Parameter)newParameters[i];
				Parameter parameter2 = null;
				foreach (Parameter parameter3 in list)
				{
					bool flag = useNamedParameters ? (string.Equals(parameter3.Name, parameter.Name, StringComparison.OrdinalIgnoreCase) && parameter3.Direction == parameter.Direction) : (parameter3.Direction == parameter.Direction);
					bool flag2 = parameter3.Direction == ParameterDirection.ReturnValue && parameter.Direction == ParameterDirection.ReturnValue;
					if (flag || flag2)
					{
						parameter2 = parameter3;
						break;
					}
				}
				if (parameter2 != null)
				{
					list2.Add(parameter2);
					list.Remove(parameter2);
				}
				else if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
				{
					list2.Add(parameter);
				}
			}
			return list2;
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000F8390 File Offset: 0x000F7390
		private void OnCommandChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[SqlDataSourceCustomCommandEditor.EventCommandChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000F83BE File Offset: 0x000F73BE
		private void OnCommandTextBoxTextChanged(object sender, EventArgs e)
		{
			this.OnCommandChanged(EventArgs.Empty);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000F83CC File Offset: 0x000F73CC
		private void OnQueryBuilderButtonClick(object sender, EventArgs e)
		{
			IServiceProvider site = this._sqlDataSourceDesigner.Component.Site;
			if (this._dataConnection.ConnectionString != null && this._dataConnection.ConnectionString.Trim().Length == 0)
			{
				UIServiceHelper.ShowError(site, SR.GetString("SqlDataSourceCustomCommandEditor_NoConnectionString"));
				return;
			}
			DesignerDataConnection connection = this._dataConnection;
			if (string.IsNullOrEmpty(this._dataConnection.ProviderName))
			{
				connection = new DesignerDataConnection(this._dataConnection.Name, "System.Data.SqlClient", this._dataConnection.ConnectionString, this._dataConnection.IsConfigured);
			}
			string text = this._dataEnvironment.BuildQuery(this, connection, this._editorMode, this._commandTextBox.Text);
			if (text != null && text.Length > 0)
			{
				this._commandTextBox.Text = text;
				this._commandTextBox.Focus();
				this._commandTextBox.Select(0, 0);
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000F84B4 File Offset: 0x000F74B4
		private void OnSqlRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			this.UpdateEnabledState();
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000F84BC File Offset: 0x000F74BC
		private void OnStoredProcedureComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnCommandChanged(EventArgs.Empty);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000F84CC File Offset: 0x000F74CC
		public void SetCommandData(SqlDataSourceDesigner sqlDataSourceDesigner, QueryBuilderMode editorMode)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this._editorMode = editorMode;
			this._queryBuilderButton.Enabled = false;
			IServiceProvider site = this._sqlDataSourceDesigner.Component.Site;
			if (site != null)
			{
				this._dataEnvironment = (IDataEnvironment)site.GetService(typeof(IDataEnvironment));
			}
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000F8522 File Offset: 0x000F7522
		public void SetConnection(DesignerDataConnection dataConnection)
		{
			this._dataConnection = dataConnection;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000F852C File Offset: 0x000F752C
		public void SetQuery(SqlDataSourceQuery query)
		{
			this._storedProcedureComboBox.SelectedIndex = -1;
			if (this._storedProcedures != null)
			{
				foreach (object obj in this._storedProcedureComboBox.Items)
				{
					SqlDataSourceCustomCommandEditor.StoredProcedureItem storedProcedureItem = (SqlDataSourceCustomCommandEditor.StoredProcedureItem)obj;
					if (storedProcedureItem.DesignerDataStoredProcedure.Name == query.Command)
					{
						this._storedProcedureComboBox.SelectedItem = storedProcedureItem;
						break;
					}
				}
			}
			if (this._storedProcedureComboBox.SelectedIndex != -1)
			{
				this._sqlRadioButton.Checked = false;
				this._storedProcedureRadioButton.Checked = true;
			}
			else
			{
				this._sqlRadioButton.Checked = true;
				this._storedProcedureRadioButton.Checked = false;
				if (this._storedProcedureComboBox.Items.Count > 0)
				{
					this._storedProcedureComboBox.SelectedIndex = 0;
				}
			}
			if (!this._queryInitialized)
			{
				this._commandTextBox.Text = query.Command;
				this._originalCommand = query.Command;
				this._commandType = query.CommandType;
				this._parameters = query.Parameters;
				this._queryInitialized = true;
			}
			this.UpdateEnabledState();
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000F8688 File Offset: 0x000F7688
		public void SetStoredProcedures(ICollection storedProcedures)
		{
			this._storedProcedures = storedProcedures;
			bool flag = this._storedProcedures != null && this._storedProcedures.Count > 0;
			this._storedProcedureRadioButton.Enabled = flag;
			this._storedProcedureComboBox.Items.Clear();
			if (flag)
			{
				List<SqlDataSourceCustomCommandEditor.StoredProcedureItem> list = new List<SqlDataSourceCustomCommandEditor.StoredProcedureItem>();
				foreach (object obj in this._storedProcedures)
				{
					DesignerDataStoredProcedure designerDataStoredProcedure = (DesignerDataStoredProcedure)obj;
					list.Add(new SqlDataSourceCustomCommandEditor.StoredProcedureItem(designerDataStoredProcedure));
				}
				list.Sort((SqlDataSourceCustomCommandEditor.StoredProcedureItem a, SqlDataSourceCustomCommandEditor.StoredProcedureItem b) => string.Compare(a.DesignerDataStoredProcedure.Name, b.DesignerDataStoredProcedure.Name, StringComparison.InvariantCultureIgnoreCase));
				this._storedProcedureComboBox.Items.AddRange(list.ToArray());
				this._storedProcedureComboBox.InvalidateDropDownWidth();
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000F8778 File Offset: 0x000F7778
		private void UpdateEnabledState()
		{
			bool @checked = this._sqlRadioButton.Checked;
			this._commandTextBox.Enabled = @checked;
			this._queryBuilderButton.Enabled = @checked;
			this._storedProcedureComboBox.Enabled = !@checked;
			this.OnCommandChanged(EventArgs.Empty);
		}

		// Token: 0x04001E18 RID: 7704
		private static readonly object EventCommandChanged = new object();

		// Token: 0x04001E19 RID: 7705
		private System.Windows.Forms.TextBox _commandTextBox;

		// Token: 0x04001E1A RID: 7706
		private System.Windows.Forms.Button _queryBuilderButton;

		// Token: 0x04001E1B RID: 7707
		private System.Windows.Forms.RadioButton _sqlRadioButton;

		// Token: 0x04001E1C RID: 7708
		private System.Windows.Forms.RadioButton _storedProcedureRadioButton;

		// Token: 0x04001E1D RID: 7709
		private AutoSizeComboBox _storedProcedureComboBox;

		// Token: 0x04001E1E RID: 7710
		private System.Windows.Forms.Panel _sqlPanel;

		// Token: 0x04001E1F RID: 7711
		private System.Windows.Forms.Panel _storedProcedurePanel;

		// Token: 0x04001E20 RID: 7712
		private QueryBuilderMode _editorMode;

		// Token: 0x04001E21 RID: 7713
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04001E22 RID: 7714
		private DesignerDataConnection _dataConnection;

		// Token: 0x04001E23 RID: 7715
		private ICollection _storedProcedures;

		// Token: 0x04001E24 RID: 7716
		private IDataEnvironment _dataEnvironment;

		// Token: 0x04001E25 RID: 7717
		private ICollection _parameters;

		// Token: 0x04001E26 RID: 7718
		private string _originalCommand;

		// Token: 0x04001E27 RID: 7719
		private SqlDataSourceCommandType _commandType;

		// Token: 0x04001E28 RID: 7720
		private bool _queryInitialized;

		// Token: 0x04001E29 RID: 7721
		[CompilerGenerated]
		private static Comparison<SqlDataSourceCustomCommandEditor.StoredProcedureItem> <>9__CachedAnonymousMethodDelegate1;

		// Token: 0x020004C7 RID: 1223
		private sealed class StoredProcedureItem
		{
			// Token: 0x06002C46 RID: 11334 RVA: 0x000F87CF File Offset: 0x000F77CF
			public StoredProcedureItem(DesignerDataStoredProcedure designerDataStoredProcedure)
			{
				this._designerDataStoredProcedure = designerDataStoredProcedure;
			}

			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000F87DE File Offset: 0x000F77DE
			public DesignerDataStoredProcedure DesignerDataStoredProcedure
			{
				get
				{
					return this._designerDataStoredProcedure;
				}
			}

			// Token: 0x06002C48 RID: 11336 RVA: 0x000F87E6 File Offset: 0x000F77E6
			public override string ToString()
			{
				return this._designerDataStoredProcedure.Name;
			}

			// Token: 0x04001E2A RID: 7722
			private DesignerDataStoredProcedure _designerDataStoredProcedure;
		}
	}
}
