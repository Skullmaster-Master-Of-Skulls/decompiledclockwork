using System;
using System.Collections;
using System.ComponentModel.Design.Data;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004C9 RID: 1225
	internal class SqlDataSourceDataConnectionChooserPanel : SqlDataSourceConnectionPanel
	{
		// Token: 0x06002C53 RID: 11347 RVA: 0x000F91BC File Offset: 0x000F81BC
		public SqlDataSourceDataConnectionChooserPanel(SqlDataSourceDesigner sqlDataSourceDesigner, IDataEnvironment dataEnvironment) : base(sqlDataSourceDesigner)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this._sqlDataSource = (SqlDataSource)this._sqlDataSourceDesigner.Component;
			this._dataEnvironment = dataEnvironment;
			this.InitializeComponent();
			this.InitializeUI();
			DesignerDataConnection connectionSettings = new DesignerDataConnection(SR.GetString("SqlDataSourceDataConnectionChooserPanel_CustomConnectionName"), this._sqlDataSource.ProviderName, this._sqlDataSource.ConnectionString);
			ExpressionBindingCollection expressions = ((IExpressionsAccessor)this._sqlDataSource).Expressions;
			ExpressionBinding expressionBinding = expressions["ConnectionString"];
			if (expressionBinding != null && string.Equals(expressionBinding.ExpressionPrefix, "ConnectionStrings", StringComparison.OrdinalIgnoreCase))
			{
				string text = expressionBinding.Expression;
				string text2 = "." + "ConnectionString".ToLowerInvariant();
				if (text.ToLowerInvariant().EndsWith(text2, StringComparison.Ordinal))
				{
					text = text.Substring(0, text.Length - text2.Length);
				}
				ICollection connections = this._dataEnvironment.Connections;
				if (connections != null)
				{
					foreach (object obj in connections)
					{
						DesignerDataConnection designerDataConnection = (DesignerDataConnection)obj;
						if (designerDataConnection.IsConfigured && string.Equals(designerDataConnection.Name, text, StringComparison.OrdinalIgnoreCase))
						{
							connectionSettings = designerDataConnection;
							break;
						}
					}
				}
			}
			this.SetConnectionSettings(connectionSettings);
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000F931C File Offset: 0x000F831C
		public override DesignerDataConnection DataConnection
		{
			get
			{
				return ((SqlDataSourceDataConnectionChooserPanel.DataConnectionItem)this._connectionsComboBox.SelectedItem).DesignerDataConnection;
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000F9333 File Offset: 0x000F8333
		private void CheckShouldAllowNext()
		{
			if (base.ParentWizard != null)
			{
				base.ParentWizard.NextButton.Enabled = (this._connectionsComboBox.SelectedItem != null);
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000F9360 File Offset: 0x000F8360
		private void InitializeComponent()
		{
			this._chooseLabel = new System.Windows.Forms.Label();
			this._connectionsComboBox = new AutoSizeComboBox();
			this._newConnectionButton = new System.Windows.Forms.Button();
			this._connectionTableLayoutPanel = new TableLayoutPanel();
			this._detailsButton = new SqlDataSourceDataConnectionChooserPanel.DetailsButton();
			this._connectionStringLabel = new System.Windows.Forms.Label();
			this._dividerLabel = new System.Windows.Forms.Label();
			this._connectionStringTextBox = new System.Windows.Forms.TextBox();
			this._connectionTableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this._chooseLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._chooseLabel.Location = new Point(0, 0);
			this._chooseLabel.Name = "_chooseLabel";
			this._chooseLabel.Size = new Size(544, 16);
			this._chooseLabel.TabIndex = 10;
			this._connectionTableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._connectionTableLayoutPanel.ColumnCount = 2;
			this._connectionTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this._connectionTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this._connectionTableLayoutPanel.Controls.Add(this._newConnectionButton, 1, 0);
			this._connectionTableLayoutPanel.Controls.Add(this._connectionsComboBox, 0, 0);
			this._connectionTableLayoutPanel.Location = new Point(0, 18);
			this._connectionTableLayoutPanel.Name = "_connectionTableLayoutPanel";
			this._connectionTableLayoutPanel.RowCount = 1;
			this._connectionTableLayoutPanel.RowStyles.Add(new RowStyle());
			this._connectionTableLayoutPanel.Size = new Size(544, 23);
			this._connectionTableLayoutPanel.TabIndex = 20;
			this._connectionsComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._connectionsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this._connectionsComboBox.Location = new Point(0, 0);
			this._connectionsComboBox.Margin = new Padding(0, 0, 3, 0);
			this._connectionsComboBox.Name = "_connectionsComboBox";
			this._connectionsComboBox.Size = new Size(463, 21);
			this._connectionsComboBox.Sorted = true;
			this._connectionsComboBox.TabIndex = 10;
			this._connectionsComboBox.SelectedIndexChanged += this.OnConnectionsComboBoxSelectedIndexChanged;
			this._newConnectionButton.AutoSize = true;
			this._newConnectionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this._newConnectionButton.Location = new Point(469, 0);
			this._newConnectionButton.Margin = new Padding(3, 0, 0, 0);
			this._newConnectionButton.MinimumSize = new Size(75, 23);
			this._newConnectionButton.Name = "_newConnectionButton";
			this._newConnectionButton.Padding = new Padding(10, 0, 10, 0);
			this._newConnectionButton.Size = new Size(75, 23);
			this._newConnectionButton.TabIndex = 20;
			this._newConnectionButton.Click += this.OnNewConnectionButtonClick;
			this._detailsButton.Location = new Point(0, 51);
			this._detailsButton.Name = "_detailsButton";
			this._detailsButton.Size = new Size(15, 15);
			this._detailsButton.TabIndex = 30;
			this._detailsButton.Click += this.OnDetailsButtonClick;
			this._connectionStringLabel.AutoSize = true;
			this._connectionStringLabel.Location = new Point(21, 51);
			this._connectionStringLabel.Name = "_connectionStringLabel";
			this._connectionStringLabel.Padding = new Padding(0, 0, 6, 0);
			this._connectionStringLabel.Size = new Size(92, 16);
			this._connectionStringLabel.TabIndex = 40;
			this._dividerLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._dividerLabel.BackColor = SystemColors.ControlDark;
			this._dividerLabel.Location = new Point(30, 57);
			this._dividerLabel.Name = "_dividerLabel";
			this._dividerLabel.Size = new Size(514, 1);
			this._dividerLabel.TabIndex = 50;
			this._connectionStringTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._connectionStringTextBox.BackColor = SystemColors.Control;
			this._connectionStringTextBox.Location = new Point(21, 71);
			this._connectionStringTextBox.Multiline = true;
			this._connectionStringTextBox.Name = "_connectionStringTextBox";
			this._connectionStringTextBox.ReadOnly = true;
			this._connectionStringTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._connectionStringTextBox.Size = new Size(523, 90);
			this._connectionStringTextBox.TabIndex = 60;
			this._connectionStringTextBox.Text = "";
			this._connectionStringTextBox.Visible = false;
			base.Controls.Add(this._connectionStringLabel);
			base.Controls.Add(this._dividerLabel);
			base.Controls.Add(this._detailsButton);
			base.Controls.Add(this._connectionStringTextBox);
			base.Controls.Add(this._chooseLabel);
			base.Controls.Add(this._connectionTableLayoutPanel);
			base.Name = "SqlDataSourceDataConnectionChooserPanel";
			base.Size = new Size(544, 274);
			this._connectionTableLayoutPanel.ResumeLayout(false);
			this._connectionTableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000F98CC File Offset: 0x000F88CC
		private void InitializeUI()
		{
			this._newConnectionButton.Text = SR.GetString("SqlDataSourceDataConnectionChooserPanel_NewConnectionButton");
			this._chooseLabel.Text = SR.GetString("SqlDataSourceDataConnectionChooserPanel_ChooseLabel");
			this._connectionStringLabel.Text = SR.GetString("SqlDataSourceDataConnectionChooserPanel_ConnectionStringLabel");
			this._detailsButton.AccessibleName = SR.GetString("SqlDataSourceDataConnectionChooserPanel_DetailsButtonName");
			this._detailsButton.AccessibleDescription = SR.GetString("SqlDataSourceDataConnectionChooserPanel_DetailsButtonDesc");
			ICollection connections = this._dataEnvironment.Connections;
			foreach (object obj in connections)
			{
				DesignerDataConnection designerDataConnection = (DesignerDataConnection)obj;
				this._connectionsComboBox.Items.Add(new SqlDataSourceDataConnectionChooserPanel.DataConnectionItem(designerDataConnection));
			}
			this._connectionsComboBox.InvalidateDropDownWidth();
			base.Caption = SR.GetString("SqlDataSourceDataConnectionChooserPanel_PanelCaption");
			this.UpdateFonts();
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000F99C8 File Offset: 0x000F89C8
		protected internal override void OnComplete()
		{
			if (this._needsToPersistConnectionInfo)
			{
				SqlDataSourceSaveConfiguredConnectionPanel.PersistConnectionSettings(this._sqlDataSource, this._sqlDataSourceDesigner, this.DataConnection);
			}
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000F99EC File Offset: 0x000F89EC
		private void OnConnectionsComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.CheckShouldAllowNext();
			SqlDataSourceDataConnectionChooserPanel.DataConnectionItem dataConnectionItem = this._connectionsComboBox.SelectedItem as SqlDataSourceDataConnectionChooserPanel.DataConnectionItem;
			if (dataConnectionItem == null)
			{
				return;
			}
			this._connectionStringTextBox.Text = dataConnectionItem.DesignerDataConnection.ConnectionString;
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000F9A2A File Offset: 0x000F8A2A
		private void OnDetailsButtonClick(object sender, EventArgs e)
		{
			this._connectionStringTextBox.Visible = !this._connectionStringTextBox.Visible;
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000F9A45 File Offset: 0x000F8A45
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000F9A54 File Offset: 0x000F8A54
		private void OnNewConnectionButtonClick(object sender, EventArgs e)
		{
			DesignerDataConnection designerDataConnection = this._dataEnvironment.BuildConnection(this, null);
			if (designerDataConnection != null && !this.SelectConnection(designerDataConnection))
			{
				SqlDataSourceDataConnectionChooserPanel.DataConnectionItem dataConnectionItem = new SqlDataSourceDataConnectionChooserPanel.DataConnectionItem(designerDataConnection);
				this._connectionsComboBox.Items.Add(dataConnectionItem);
				this._connectionsComboBox.SelectedItem = dataConnectionItem;
				this._connectionsComboBox.InvalidateDropDownWidth();
			}
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000F9AB0 File Offset: 0x000F8AB0
		public override bool OnNext()
		{
			if (!base.CheckValidProvider())
			{
				return false;
			}
			DesignerDataConnection dataConnection = this.DataConnection;
			if (!dataConnection.IsConfigured)
			{
				this._needsToPersistConnectionInfo = false;
				SqlDataSourceSaveConfiguredConnectionPanel sqlDataSourceSaveConfiguredConnectionPanel = base.NextPanel as SqlDataSourceSaveConfiguredConnectionPanel;
				if (sqlDataSourceSaveConfiguredConnectionPanel == null)
				{
					sqlDataSourceSaveConfiguredConnectionPanel = ((SqlDataSourceWizardForm)base.ParentWizard).GetSaveConfiguredConnectionPanel();
					base.NextPanel = sqlDataSourceSaveConfiguredConnectionPanel;
				}
				if (!SqlDataSourceDesigner.ConnectionsEqual(dataConnection, sqlDataSourceSaveConfiguredConnectionPanel.CurrentConnection))
				{
					sqlDataSourceSaveConfiguredConnectionPanel.SetConnectionInfo(dataConnection);
				}
				return true;
			}
			this._needsToPersistConnectionInfo = true;
			return base.OnNext();
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000F9B28 File Offset: 0x000F8B28
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				this.CheckShouldAllowNext();
			}
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000F9B40 File Offset: 0x000F8B40
		private bool SelectConnection(DesignerDataConnection conn)
		{
			if (conn.IsConfigured)
			{
				using (IEnumerator enumerator = this._connectionsComboBox.Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SqlDataSourceDataConnectionChooserPanel.DataConnectionItem dataConnectionItem = (SqlDataSourceDataConnectionChooserPanel.DataConnectionItem)obj;
						DesignerDataConnection designerDataConnection = dataConnectionItem.DesignerDataConnection;
						if (designerDataConnection.IsConfigured && designerDataConnection.Name == conn.Name)
						{
							this._connectionsComboBox.SelectedItem = dataConnectionItem;
							return true;
						}
					}
					return false;
				}
			}
			foreach (object obj2 in this._connectionsComboBox.Items)
			{
				SqlDataSourceDataConnectionChooserPanel.DataConnectionItem dataConnectionItem2 = (SqlDataSourceDataConnectionChooserPanel.DataConnectionItem)obj2;
				DesignerDataConnection designerDataConnection2 = dataConnectionItem2.DesignerDataConnection;
				if (!designerDataConnection2.IsConfigured && SqlDataSourceDesigner.ConnectionsEqual(designerDataConnection2, conn))
				{
					this._connectionsComboBox.SelectedItem = dataConnectionItem2;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000F9C58 File Offset: 0x000F8C58
		private void SetConnectionSettings(DesignerDataConnection conn)
		{
			bool flag = this.SelectConnection(conn);
			string text = conn.ProviderName;
			string connectionString = conn.ConnectionString;
			if (!flag && (text.Length > 0 || connectionString.Length > 0))
			{
				if (text.Length == 0)
				{
					text = "System.Data.SqlClient";
				}
				this._connectionsComboBox.Items.Insert(0, new SqlDataSourceDataConnectionChooserPanel.DataConnectionItem(new DesignerDataConnection(conn.Name, text, connectionString)));
				this._connectionsComboBox.SelectedIndex = 0;
				this._connectionsComboBox.InvalidateDropDownWidth();
			}
			this._connectionStringTextBox.Text = connectionString;
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000F9CE4 File Offset: 0x000F8CE4
		private void UpdateFonts()
		{
			this._chooseLabel.Font = new Font(this.Font, FontStyle.Bold);
		}

		// Token: 0x04001E37 RID: 7735
		private AutoSizeComboBox _connectionsComboBox;

		// Token: 0x04001E38 RID: 7736
		private System.Windows.Forms.Label _chooseLabel;

		// Token: 0x04001E39 RID: 7737
		private System.Windows.Forms.Button _newConnectionButton;

		// Token: 0x04001E3A RID: 7738
		private System.Windows.Forms.TextBox _connectionStringTextBox;

		// Token: 0x04001E3B RID: 7739
		private System.Windows.Forms.Label _connectionStringLabel;

		// Token: 0x04001E3C RID: 7740
		private TableLayoutPanel _connectionTableLayoutPanel;

		// Token: 0x04001E3D RID: 7741
		private System.Windows.Forms.Label _dividerLabel;

		// Token: 0x04001E3E RID: 7742
		private SqlDataSourceDataConnectionChooserPanel.DetailsButton _detailsButton;

		// Token: 0x04001E3F RID: 7743
		private SqlDataSource _sqlDataSource;

		// Token: 0x04001E40 RID: 7744
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04001E41 RID: 7745
		private IDataEnvironment _dataEnvironment;

		// Token: 0x04001E42 RID: 7746
		private bool _needsToPersistConnectionInfo;

		// Token: 0x020004CA RID: 1226
		private sealed class DataConnectionItem
		{
			// Token: 0x06002C62 RID: 11362 RVA: 0x000F9CFD File Offset: 0x000F8CFD
			public DataConnectionItem(DesignerDataConnection designerDataConnection)
			{
				this._designerDataConnection = designerDataConnection;
			}

			// Token: 0x1700084F RID: 2127
			// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000F9D0C File Offset: 0x000F8D0C
			public DesignerDataConnection DesignerDataConnection
			{
				get
				{
					return this._designerDataConnection;
				}
			}

			// Token: 0x06002C64 RID: 11364 RVA: 0x000F9D14 File Offset: 0x000F8D14
			public override string ToString()
			{
				return this._designerDataConnection.Name;
			}

			// Token: 0x04001E43 RID: 7747
			private DesignerDataConnection _designerDataConnection;
		}

		// Token: 0x020004CB RID: 1227
		private sealed class DetailsButton : System.Windows.Forms.Button
		{
			// Token: 0x06002C66 RID: 11366 RVA: 0x000F9D29 File Offset: 0x000F8D29
			protected override void OnClick(EventArgs e)
			{
				this._details = !this._details;
				base.OnClick(e);
				base.Invalidate();
			}

			// Token: 0x06002C67 RID: 11367 RVA: 0x000F9D48 File Offset: 0x000F8D48
			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint(e);
				e.Graphics.DrawLine(SystemPens.ControlText, base.Width / 2 - 3, base.Height / 2, base.Width / 2 + 3, base.Height / 2);
				if (!this._details)
				{
					e.Graphics.DrawLine(SystemPens.ControlText, base.Width / 2, base.Height / 2 - 3, base.Width / 2, base.Height / 2 + 3);
				}
			}

			// Token: 0x04001E44 RID: 7748
			private const int PlusLineLength = 3;

			// Token: 0x04001E45 RID: 7749
			private bool _details;
		}
	}
}
