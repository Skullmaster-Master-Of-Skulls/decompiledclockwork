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
	// Token: 0x02000119 RID: 281
	internal class SqlDataSourceSaveConfiguredConnectionPanel : WizardPanel
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x00040D3D File Offset: 0x0003EF3D
		public SqlDataSourceSaveConfiguredConnectionPanel(SqlDataSourceDesigner sqlDataSourceDesigner, IDataEnvironment dataEnvironment)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this._sqlDataSource = (SqlDataSource)this._sqlDataSourceDesigner.Component;
			this._dataEnvironment = dataEnvironment;
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00040D75 File Offset: 0x0003EF75
		internal DesignerDataConnection CurrentConnection
		{
			get
			{
				return this._dataConnection;
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00040D80 File Offset: 0x0003EF80
		private void CheckShouldAllowNext()
		{
			if (base.ParentWizard != null)
			{
				base.ParentWizard.NextButton.Enabled = (!this._saveCheckBox.Checked || this._nameTextBox.Text.Trim().Length > 0);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00040DD0 File Offset: 0x0003EFD0
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

		// Token: 0x06000A37 RID: 2615 RVA: 0x00040E8C File Offset: 0x0003F08C
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

		// Token: 0x06000A38 RID: 2616 RVA: 0x00041118 File Offset: 0x0003F318
		private void InitializeUI()
		{
			this._helpLabel.Text = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_HelpLabel");
			this._saveLabel.Text = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_SaveLabel");
			this._saveCheckBox.Text = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_SaveCheckBox");
			this._nameHelpLabel.Text = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_NameTextBoxDescription");
			base.Caption = SR.GetString("SqlDataSourceSaveConfiguredConnectionPanel_PanelCaption");
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0004118C File Offset: 0x0003F38C
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

		// Token: 0x06000A3A RID: 2618 RVA: 0x00041228 File Offset: 0x0003F428
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00041237 File Offset: 0x0003F437
		private void OnSaveCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this._nameTextBox.Enabled = this._saveCheckBox.Checked;
			this.CheckShouldAllowNext();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00041255 File Offset: 0x0003F455
		private void OnNameTextBoxTextChanged(object sender, EventArgs e)
		{
			this.CheckShouldAllowNext();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00041260 File Offset: 0x0003F460
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

		// Token: 0x06000A3E RID: 2622 RVA: 0x00041360 File Offset: 0x0003F560
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

		// Token: 0x06000A3F RID: 2623 RVA: 0x0004139C File Offset: 0x0003F59C
		internal static void PersistConnectionSettings(SqlDataSource sqlDataSource, SqlDataSourceDesigner sqlDataSourceDesigner, DesignerDataConnection dataConnection)
		{
			if (dataConnection.IsConfigured)
			{
				ExpressionBindingCollection expressions = ((IExpressionsAccessor)sqlDataSource).Expressions;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(sqlDataSource)["ProviderName"];
				propertyDescriptor.ResetValue(sqlDataSource);
				if (dataConnection.ProviderName == "System.Data.SqlClient")
				{
					expressions.Remove(propertyDescriptor.Name);
				}
				else
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

		// Token: 0x06000A40 RID: 2624 RVA: 0x000414CA File Offset: 0x0003F6CA
		public void ResetUI()
		{
			this.UpdateFonts();
			this._saveCheckBox.Checked = true;
			this._nameTextBox.Text = string.Empty;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000414F0 File Offset: 0x0003F6F0
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

		// Token: 0x06000A42 RID: 2626 RVA: 0x00041584 File Offset: 0x0003F784
		private void UpdateFonts()
		{
			Font font = new Font(this.Font, FontStyle.Bold);
			this._saveLabel.Font = font;
		}

		// Token: 0x04000621 RID: 1569
		internal const string ConnectionStringExpressionPrefix = "ConnectionStrings";

		// Token: 0x04000622 RID: 1570
		internal const string ConnectionStringExpressionConnectionSuffix = "ConnectionString";

		// Token: 0x04000623 RID: 1571
		internal const string ConnectionStringExpressionProviderSuffix = "ProviderName";

		// Token: 0x04000624 RID: 1572
		private System.Windows.Forms.Label _helpLabel;

		// Token: 0x04000625 RID: 1573
		private System.Windows.Forms.Label _saveLabel;

		// Token: 0x04000626 RID: 1574
		private System.Windows.Forms.CheckBox _saveCheckBox;

		// Token: 0x04000627 RID: 1575
		private System.Windows.Forms.TextBox _nameTextBox;

		// Token: 0x04000628 RID: 1576
		private System.Windows.Forms.Label _nameHelpLabel;

		// Token: 0x04000629 RID: 1577
		private IDataEnvironment _dataEnvironment;

		// Token: 0x0400062A RID: 1578
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x0400062B RID: 1579
		private SqlDataSource _sqlDataSource;

		// Token: 0x0400062C RID: 1580
		private DesignerDataConnection _dataConnection;

		// Token: 0x0200044C RID: 1100
		private static class ConnectionStringHelper
		{
			// Token: 0x06002925 RID: 10533 RVA: 0x000F9AE4 File Offset: 0x000F7CE4
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
				catch
				{
				}
				if (text == null)
				{
					text = "ConnectionString";
				}
				return text.Trim();
			}

			// Token: 0x06002926 RID: 10534 RVA: 0x000F9BBC File Offset: 0x000F7DBC
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

			// Token: 0x06002927 RID: 10535 RVA: 0x000F9BE0 File Offset: 0x000F7DE0
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

			// Token: 0x06002928 RID: 10536 RVA: 0x000F9C2A File Offset: 0x000F7E2A
			private static bool IsLocalDbFileConnectionString(string providerName, DbConnectionStringBuilder connectionStringBuilder)
			{
				return SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.IsSqlLocalConnectionString(providerName, connectionStringBuilder) || SqlDataSourceSaveConfiguredConnectionPanel.ConnectionStringHelper.IsAccessConnectionString(providerName, connectionStringBuilder);
			}

			// Token: 0x06002929 RID: 10537 RVA: 0x000F9C41 File Offset: 0x000F7E41
			private static bool IsSqlLocalConnectionString(string providerName, DbConnectionStringBuilder connectionStringBuilder)
			{
				return string.Equals(providerName, "System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(connectionStringBuilder["AttachDbFileName"] as string);
			}

			// Token: 0x0600292A RID: 10538 RVA: 0x000F9C6B File Offset: 0x000F7E6B
			private static bool StringIsEmpty(string s)
			{
				return string.IsNullOrEmpty(s) || s.Trim().Length == 0;
			}

			// Token: 0x04001D20 RID: 7456
			private const string DefaultConnectionName = "ConnectionString";

			// Token: 0x04001D21 RID: 7457
			private const string JetOleDbProviderName = "MICROSOFT.JET";

			// Token: 0x04001D22 RID: 7458
			private const string SqlClientProviderName = "System.Data.SqlClient";

			// Token: 0x04001D23 RID: 7459
			private const string OleDbProviderName = "System.Data.OleDb";
		}
	}
}
