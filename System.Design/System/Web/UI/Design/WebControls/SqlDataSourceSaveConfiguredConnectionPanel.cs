using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Data;
using System.Data.Common;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004DE RID: 1246
	internal class SqlDataSourceSaveConfiguredConnectionPanel : WizardPanel
	{
		// Token: 0x06002CAB RID: 11435 RVA: 0x000FC12C File Offset: 0x000FB12C
		public SqlDataSourceSaveConfiguredConnectionPanel(SqlDataSourceDesigner sqlDataSourceDesigner, IDataEnvironment dataEnvironment)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this._sqlDataSource = (SqlDataSource)this._sqlDataSourceDesigner.Component;
			this._dataEnvironment = dataEnvironment;
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000FC164 File Offset: 0x000FB164
		internal DesignerDataConnection CurrentConnection
		{
			get
			{
				return this._dataConnection;
			}
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000FC16C File Offset: 0x000FB16C
		private void CheckShouldAllowNext()
		{
			if (base.ParentWizard != null)
			{
				base.ParentWizard.NextButton.Enabled = (!this._saveCheckBox.Checked || this._nameTextBox.Text.Trim().Length > 0);
			}
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000FC1BC File Offset: 0x000FB1BC
		private string CreateDefaultConnectionName()
		{
			ICollection connections = this._dataEnvironment.Connections;
			StringDictionary stringDictionary = new StringDictionary();
			if (connections != null)
			{
				foreach (object obj in connections)
				{
					DesignerDataConnection designerDataConnection = (DesignerDataConnection)obj;
					if (designerDataConnection != null && designerDataConnection.IsConfigured)
					{
						stringDictionary.Add(designerDataConnection.Name, null);
					}
				}
			}
			int num = 2;
			string connectionName = SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.GetConnectionName(this._dataConnection);
			string text = connectionName;
			while (stringDictionary.ContainsKey(text))
			{
				text = connectionName + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			return text;
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000FC278 File Offset: 0x000FB278
		private void InitializeComponent()
		{
			this._saveLabel = new System.Windows.Forms.Label();
			this._saveCheckBox = new System.Windows.Forms.CheckBox();
			this._nameTextBox = new System.Windows.Forms.TextBox();
			this._helpLabel = new System.Windows.Forms.Label();
			this._nameHelpLabel = new System.Windows.Forms.Label();
			base.SuspendLayout();
			this._helpLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._helpLabel.Location = new Point(0, 0);
			this._helpLabel.Name = "_helpLabel";
			this._helpLabel.Size = new Size(544, 56);
			this._helpLabel.TabIndex = 10;
			this._saveLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._saveLabel.Location = new Point(0, 75);
			this._saveLabel.Name = "_saveLabel";
			this._saveLabel.Size = new Size(544, 16);
			this._saveLabel.TabIndex = 20;
			this._saveCheckBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._saveCheckBox.Location = new Point(0, 93);
			this._saveCheckBox.Name = "_saveCheckBox";
			this._saveCheckBox.Size = new Size(544, 18);
			this._saveCheckBox.TabIndex = 30;
			this._saveCheckBox.CheckedChanged += this.OnSaveCheckBoxCheckedChanged;
			this._nameHelpLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._nameHelpLabel.Location = new Point(0, 0);
			this._nameHelpLabel.Name = "_nameHelpLabel";
			this._nameHelpLabel.Size = new Size(0, 0);
			this._nameHelpLabel.TabIndex = 40;
			this._nameTextBox.Location = new Point(19, 113);
			this._nameTextBox.Name = "_nameTextBox";
			this._nameTextBox.Size = new Size(300, 20);
			this._nameTextBox.TabIndex = 50;
			this._nameTextBox.TextChanged += this.OnNameTextBoxTextChanged;
			base.Controls.Add(this._nameHelpLabel);
			base.Controls.Add(this._saveCheckBox);
			base.Controls.Add(this._saveLabel);
			base.Controls.Add(this._nameTextBox);
			base.Controls.Add(this._helpLabel);
			base.Name = "SqlDataSourceSaveConfiguredConnectionPanel";
			base.Size = new Size(544, 274);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000FC504 File Offset: 0x000FB504
		private void InitializeUI()
		{
			this._helpLabel.Text = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_HelpLabel");
			this._saveLabel.Text = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_SaveLabel");
			this._saveCheckBox.Text = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_SaveCheckBox");
			this._nameHelpLabel.Text = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_NameTextBoxDescription");
			base.Caption = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_PanelCaption");
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000FC578 File Offset: 0x000FB578
		protected internal override void OnComplete()
		{
			DesignerDataConnection designerDataConnection = this._dataConnection;
			if (this._saveCheckBox.Checked)
			{
				try
				{
					designerDataConnection = this._dataEnvironment.ConfigureConnection(this, this._dataConnection, this._nameTextBox.Text.Trim());
				}
				catch (Exception ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						UIServiceHelper.ShowError(base.ServiceProvider, ex, SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_CouldNotSaveConnection"));
					}
				}
			}
			SqlDataSourceSaveConfiguredConnectionPanel.PersistConnectionSettings(this._sqlDataSource, this._sqlDataSourceDesigner, designerDataConnection);
			this._sqlDataSourceDesigner.SaveConfiguredConnectionState = designerDataConnection.IsConfigured;
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000FC614 File Offset: 0x000FB614
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000FC623 File Offset: 0x000FB623
		private void OnSaveCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this._nameTextBox.Enabled = this._saveCheckBox.Checked;
			this.CheckShouldAllowNext();
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000FC641 File Offset: 0x000FB641
		private void OnNameTextBoxTextChanged(object sender, EventArgs e)
		{
			this.CheckShouldAllowNext();
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000FC64C File Offset: 0x000FB64C
		public override bool OnNext()
		{
			if (this._saveCheckBox.Checked)
			{
				ICollection connections = this._dataEnvironment.Connections;
				StringDictionary stringDictionary = new StringDictionary();
				foreach (object obj in connections)
				{
					DesignerDataConnection designerDataConnection = (DesignerDataConnection)obj;
					if (designerDataConnection.IsConfigured)
					{
						stringDictionary.Add(designerDataConnection.Name, null);
					}
				}
				if (stringDictionary.ContainsKey(this._nameTextBox.Text))
				{
					UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_DuplicateName", new object[]
					{
						this._nameTextBox.Text
					}));
					this._nameTextBox.Focus();
					return false;
				}
			}
			WizardPanel wizardPanel = SqlDataSourceConnectionPanel.CreateCommandPanel((SqlDataSourceWizardForm)base.ParentWizard, this._dataConnection, base.NextPanel);
			if (wizardPanel == null)
			{
				return false;
			}
			base.NextPanel = wizardPanel;
			return true;
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000FC750 File Offset: 0x000FB750
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			base.ParentWizard.FinishButton.Enabled = false;
			if (base.Visible)
			{
				this.CheckShouldAllowNext();
				return;
			}
			base.ParentWizard.NextButton.Enabled = true;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000FC78C File Offset: 0x000FB78C
		internal static void PersistConnectionSettings(SqlDataSource sqlDataSource, SqlDataSourceDesigner sqlDataSourceDesigner, DesignerDataConnection dataConnection)
		{
			if (dataConnection.IsConfigured)
			{
				ExpressionBindingCollection expressions = ((IExpressionsAccessor)sqlDataSource).Expressions;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(sqlDataSource)["ProviderName"];
				propertyDescriptor.ResetValue(sqlDataSource);
				if (dataConnection.ProviderName != "System.Data.SqlClient")
				{
					expressions.Add(new ExpressionBinding(propertyDescriptor.Name, propertyDescriptor.PropertyType, "ConnectionStrings", dataConnection.Name + ".ProviderName"));
				}
				propertyDescriptor = TypeDescriptor.GetProperties(sqlDataSource)["ConnectionString"];
				propertyDescriptor.ResetValue(sqlDataSource);
				expressions.Add(new ExpressionBinding(propertyDescriptor.Name, propertyDescriptor.PropertyType, "ConnectionStrings", dataConnection.Name));
				return;
			}
			if (sqlDataSource.ProviderName != dataConnection.ProviderName)
			{
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(sqlDataSource)["ProviderName"];
				propertyDescriptor2.ResetValue(sqlDataSource);
				propertyDescriptor2.SetValue(sqlDataSource, dataConnection.ProviderName);
			}
			if (sqlDataSource.ConnectionString != dataConnection.ConnectionString)
			{
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(sqlDataSource)["ConnectionString"];
				propertyDescriptor2.ResetValue(sqlDataSource);
				propertyDescriptor2.SetValue(sqlDataSource, dataConnection.ConnectionString);
			}
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000FC8AC File Offset: 0x000FB8AC
		public void ResetUI()
		{
			this.UpdateFonts();
			this._saveCheckBox.Checked = true;
			this._nameTextBox.Text = string.Empty;
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000FC8D0 File Offset: 0x000FB8D0
		public void SetConnectionInfo(DesignerDataConnection dataConnection)
		{
			this._dataConnection = dataConnection;
			this.ResetUI();
			bool saveConfiguredConnectionState = this._sqlDataSourceDesigner.SaveConfiguredConnectionState;
			DesignerDataConnection connection = new DesignerDataConnection(string.Empty, this._sqlDataSourceDesigner.ProviderName, this._sqlDataSourceDesigner.ConnectionString);
			if (SqlDataSourceDesigner.ConnectionsEqual(dataConnection, connection))
			{
				if (!saveConfiguredConnectionState)
				{
					this._saveCheckBox.Checked = false;
				}
				if (this._nameTextBox.Text.Length == 0)
				{
					this._nameTextBox.Text = this.CreateDefaultConnectionName();
					return;
				}
			}
			else
			{
				this._nameTextBox.Text = this.CreateDefaultConnectionName();
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000FC964 File Offset: 0x000FB964
		private void UpdateFonts()
		{
			Font font = new Font(this.Font, FontStyle.Bold);
			this._saveLabel.Font = font;
		}

		// Token: 0x04001E7F RID: 7807
		internal const string ConnectionStringExpressionPrefix = "ConnectionStrings";

		// Token: 0x04001E80 RID: 7808
		internal const string ConnectionStringExpressionConnectionSuffix = "ConnectionString";

		// Token: 0x04001E81 RID: 7809
		internal const string ConnectionStringExpressionProviderSuffix = "ProviderName";

		// Token: 0x04001E82 RID: 7810
		private System.Windows.Forms.Label _helpLabel;

		// Token: 0x04001E83 RID: 7811
		private System.Windows.Forms.Label _saveLabel;

		// Token: 0x04001E84 RID: 7812
		private System.Windows.Forms.CheckBox _saveCheckBox;

		// Token: 0x04001E85 RID: 7813
		private System.Windows.Forms.TextBox _nameTextBox;

		// Token: 0x04001E86 RID: 7814
		private System.Windows.Forms.Label _nameHelpLabel;

		// Token: 0x04001E87 RID: 7815
		private IDataEnvironment _dataEnvironment;

		// Token: 0x04001E88 RID: 7816
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04001E89 RID: 7817
		private SqlDataSource _sqlDataSource;

		// Token: 0x04001E8A RID: 7818
		private DesignerDataConnection _dataConnection;

		// Token: 0x020004DF RID: 1247
		private static class ConnectionStringHelper
		{
			// Token: 0x06002CBB RID: 11451 RVA: 0x000FC98C File Offset: 0x000FB98C
			public static string GetConnectionName(DesignerDataConnection connection)
			{
				DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(connection.ProviderName);
				DbConnectionStringBuilder dbConnectionStringBuilder = dbProviderFactory.CreateConnectionStringBuilder();
				if (dbConnectionStringBuilder == null)
				{
					dbConnectionStringBuilder = new DbConnectionStringBuilder();
				}
				string text = null;
				try
				{
					dbConnectionStringBuilder.ConnectionString = connection.ConnectionString;
					if (SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.IsLocalDbFileConnectionString(connection.ProviderName, dbConnectionStringBuilder))
					{
						string filePathKey = SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.GetFilePathKey(connection.ProviderName, dbConnectionStringBuilder);
						if (!string.IsNullOrEmpty(filePathKey))
						{
							string text2 = dbConnectionStringBuilder[filePathKey] as string;
							if (!string.IsNullOrEmpty(text2))
							{
								text = Path.GetFileNameWithoutExtension(text2) + "ConnectionString";
							}
						}
					}
					object obj;
					if (text == null && dbConnectionStringBuilder.TryGetValue("Database", out obj))
					{
						string text3 = obj as string;
						if (!SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.StringIsEmpty(text3))
						{
							text = text3 + "ConnectionString";
						}
					}
				}
				catch (Exception)
				{
				}
				if (text == null)
				{
					text = "ConnectionString";
				}
				return text.Trim();
			}

			// Token: 0x06002CBC RID: 11452 RVA: 0x000FCA64 File Offset: 0x000FBA64
			private static string GetFilePathKey(string providerName, DbConnectionStringBuilder connectionStringBuilder)
			{
				if (SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.IsAccessConnectionString(providerName, connectionStringBuilder))
				{
					return "Data Source";
				}
				if (SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.IsSqlLocalConnectionString(providerName, connectionStringBuilder))
				{
					return "AttachDbFileName";
				}
				return null;
			}

			// Token: 0x06002CBD RID: 11453 RVA: 0x000FCA88 File Offset: 0x000FBA88
			private static bool IsAccessConnectionString(string providerName, DbConnectionStringBuilder connectionStringBuilder)
			{
				if (string.Equals(providerName, "System.Data.OleDb", StringComparison.OrdinalIgnoreCase))
				{
					string text = connectionStringBuilder["provider"] as string;
					if (!string.IsNullOrEmpty(text) && text.ToUpperInvariant().StartsWith("MICROSOFT.JET", StringComparison.Ordinal))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002CBE RID: 11454 RVA: 0x000FCAD2 File Offset: 0x000FBAD2
			private static bool IsLocalDbFileConnectionString(string providerName, DbConnectionStringBuilder connectionStringBuilder)
			{
				return SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.IsSqlLocalConnectionString(providerName, connectionStringBuilder) || SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.IsAccessConnectionString(providerName, connectionStringBuilder);
			}

			// Token: 0x06002CBF RID: 11455 RVA: 0x000FCAE9 File Offset: 0x000FBAE9
			private static bool IsSqlLocalConnectionString(string providerName, DbConnectionStringBuilder connectionStringBuilder)
			{
				return string.Equals(providerName, "System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(connectionStringBuilder["AttachDbFileName"] as string);
			}

			// Token: 0x06002CC0 RID: 11456 RVA: 0x000FCB13 File Offset: 0x000FBB13
			private static bool StringIsEmpty(string s)
			{
				return string.IsNullOrEmpty(s) || s.Trim().Length == 0;
			}

			// Token: 0x04001E8B RID: 7819
			private const string DefaultConnectionName = "ConnectionString";

			// Token: 0x04001E8C RID: 7820
			private const string JetOleDbProviderName = "MICROSOFT.JET";

			// Token: 0x04001E8D RID: 7821
			private const string SqlClientProviderName = "System.Data.SqlClient";

			// Token: 0x04001E8E RID: 7822
			private const string OleDbProviderName = "System.Data.OleDb";
		}
	}
}
