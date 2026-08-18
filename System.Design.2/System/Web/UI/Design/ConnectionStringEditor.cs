using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Data;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.UI.Design.Util;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ConnectionStringEditor : UITypeEditor
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000367C File Offset: 0x0000187C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			Control control = context.Instance as Control;
			if (provider != null)
			{
				IDataEnvironment dataEnvironment = (IDataEnvironment)provider.GetService(typeof(IDataEnvironment));
				if (dataEnvironment != null)
				{
					IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
					if (windowsFormsEditorService != null && context.Instance != null)
					{
						if (this._connectionStringPicker == null)
						{
							this._connectionStringPicker = new ConnectionStringEditor.ConnectionStringPicker();
						}
						string connectionString = (string)value;
						ExpressionEditor expressionEditor = ExpressionEditor.GetExpressionEditor(typeof(ConnectionStringsExpressionBuilder), provider);
						if (expressionEditor != null)
						{
							string expressionPrefix = expressionEditor.ExpressionPrefix;
							DesignerDataConnection currentConnection = ConnectionStringEditor.GetCurrentConnection(control, context.PropertyDescriptor.Name, connectionString, expressionPrefix);
							this._connectionStringPicker.Start(windowsFormsEditorService, dataEnvironment.Connections, currentConnection);
							windowsFormsEditorService.DropDownControl(this._connectionStringPicker);
							if (this._connectionStringPicker.SelectedItem != null)
							{
								DesignerDataConnection designerDataConnection = this._connectionStringPicker.SelectedConnection;
								if (designerDataConnection == null)
								{
									designerDataConnection = dataEnvironment.BuildConnection(UIServiceHelper.GetDialogOwnerWindow(provider), null);
								}
								if (designerDataConnection != null)
								{
									if (designerDataConnection.IsConfigured)
									{
										ExpressionBindingCollection expressions = ((IExpressionsAccessor)control).Expressions;
										expressions.Add(new ExpressionBinding(context.PropertyDescriptor.Name, context.PropertyDescriptor.PropertyType, expressionPrefix, designerDataConnection.Name));
										this.SetProviderName(context.Instance, designerDataConnection);
										IComponentChangeService componentChangeService = (IComponentChangeService)provider.GetService(typeof(IComponentChangeService));
										if (componentChangeService != null)
										{
											componentChangeService.OnComponentChanged(control, null, null, null);
										}
									}
									else
									{
										value = designerDataConnection.ConnectionString;
										this.SetProviderName(context.Instance, designerDataConnection);
									}
								}
							}
							this._connectionStringPicker.End();
						}
					}
					return value;
				}
			}
			string providerName = this.GetProviderName(context.Instance);
			ConnectionStringEditor.ConnectionStringEditorDialog connectionStringEditorDialog = new ConnectionStringEditor.ConnectionStringEditorDialog(provider, providerName);
			connectionStringEditorDialog.ConnectionString = (string)value;
			DialogResult dialogResult = UIServiceHelper.ShowDialog(provider, connectionStringEditorDialog);
			if (dialogResult == DialogResult.OK)
			{
				value = connectionStringEditorDialog.ConnectionString;
			}
			return value;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003860 File Offset: 0x00001A60
		private static DesignerDataConnection GetCurrentConnection(Control control, string propertyName, string connectionString, string expressionPrefix)
		{
			ExpressionBindingCollection expressions = ((IExpressionsAccessor)control).Expressions;
			ExpressionBinding expressionBinding = expressions[propertyName];
			string text = "." + "ConnectionString".ToLowerInvariant();
			DesignerDataConnection result;
			if (expressionBinding != null && string.Equals(expressionBinding.ExpressionPrefix, expressionPrefix, StringComparison.OrdinalIgnoreCase))
			{
				string expression = expressionBinding.Expression;
				if (expression.ToLowerInvariant().EndsWith(text, StringComparison.Ordinal))
				{
					string text2 = expression.Substring(0, expression.Length - text.Length);
				}
				result = new DesignerDataConnection(expressionBinding.Expression, string.Empty, connectionString, true);
			}
			else
			{
				result = new DesignerDataConnection(string.Empty, string.Empty, connectionString, false);
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003904 File Offset: 0x00001B04
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null)
			{
				IDataEnvironment dataEnvironment = (IDataEnvironment)context.GetService(typeof(IDataEnvironment));
				if (dataEnvironment != null)
				{
					return UITypeEditorEditStyle.DropDown;
				}
			}
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003930 File Offset: 0x00001B30
		protected virtual string GetProviderName(object instance)
		{
			return string.Empty;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void SetProviderName(object instance, DesignerDataConnection connection)
		{
		}

		// Token: 0x040000BD RID: 189
		private ConnectionStringEditor.ConnectionStringPicker _connectionStringPicker;

		// Token: 0x0200039F RID: 927
		private sealed class ConnectionStringPicker : ListBox
		{
			// Token: 0x06002597 RID: 9623 RVA: 0x000EB632 File Offset: 0x000E9832
			public ConnectionStringPicker()
			{
				base.BorderStyle = BorderStyle.None;
			}

			// Token: 0x170007E9 RID: 2025
			// (get) Token: 0x06002598 RID: 9624 RVA: 0x000EB644 File Offset: 0x000E9844
			public DesignerDataConnection SelectedConnection
			{
				get
				{
					ConnectionStringEditor.ConnectionStringPicker.DataConnectionItem dataConnectionItem = base.SelectedItem as ConnectionStringEditor.ConnectionStringPicker.DataConnectionItem;
					if (dataConnectionItem != null)
					{
						return dataConnectionItem.DesignerDataConnection;
					}
					return null;
				}
			}

			// Token: 0x06002599 RID: 9625 RVA: 0x000EB668 File Offset: 0x000E9868
			public void End()
			{
				base.Items.Clear();
				this._edSvc = null;
			}

			// Token: 0x0600259A RID: 9626 RVA: 0x000EB67C File Offset: 0x000E987C
			protected override void OnKeyUp(KeyEventArgs e)
			{
				base.OnKeyUp(e);
				this._keyDown = true;
				this._mouseClicked = false;
				if (e.KeyData == Keys.Return)
				{
					this._keyDown = false;
					this._edSvc.CloseDropDown();
				}
			}

			// Token: 0x0600259B RID: 9627 RVA: 0x000EB6AF File Offset: 0x000E98AF
			protected override void OnMouseDown(MouseEventArgs e)
			{
				base.OnMouseDown(e);
				this._mouseClicked = true;
			}

			// Token: 0x0600259C RID: 9628 RVA: 0x000EB6BF File Offset: 0x000E98BF
			protected override void OnMouseUp(MouseEventArgs e)
			{
				base.OnMouseUp(e);
				this._mouseClicked = false;
			}

			// Token: 0x0600259D RID: 9629 RVA: 0x000EB6CF File Offset: 0x000E98CF
			protected override void OnSelectedIndexChanged(EventArgs e)
			{
				base.OnSelectedIndexChanged(e);
				if (this._mouseClicked && !this._keyDown)
				{
					this._mouseClicked = false;
					this._keyDown = false;
					this._edSvc.CloseDropDown();
				}
			}

			// Token: 0x0600259E RID: 9630 RVA: 0x000EB704 File Offset: 0x000E9904
			public void Start(IWindowsFormsEditorService edSvc, ICollection connections, DesignerDataConnection currentConnection)
			{
				this._edSvc = edSvc;
				base.Items.Clear();
				object selectedItem = null;
				foreach (object obj in connections)
				{
					DesignerDataConnection designerDataConnection = (DesignerDataConnection)obj;
					ConnectionStringEditor.ConnectionStringPicker.DataConnectionItem dataConnectionItem = new ConnectionStringEditor.ConnectionStringPicker.DataConnectionItem(designerDataConnection);
					if (designerDataConnection.ConnectionString == currentConnection.ConnectionString && designerDataConnection.IsConfigured == currentConnection.IsConfigured)
					{
						selectedItem = dataConnectionItem;
					}
					base.Items.Add(dataConnectionItem);
				}
				base.Items.Add(new ConnectionStringEditor.ConnectionStringPicker.DataConnectionItem());
				base.SelectedItem = selectedItem;
			}

			// Token: 0x04001B76 RID: 7030
			private IWindowsFormsEditorService _edSvc;

			// Token: 0x04001B77 RID: 7031
			private bool _keyDown;

			// Token: 0x04001B78 RID: 7032
			private bool _mouseClicked;

			// Token: 0x020005BA RID: 1466
			private sealed class DataConnectionItem
			{
				// Token: 0x060033D1 RID: 13265 RVA: 0x0000362F File Offset: 0x0000182F
				public DataConnectionItem()
				{
				}

				// Token: 0x060033D2 RID: 13266 RVA: 0x0011B5E3 File Offset: 0x001197E3
				public DataConnectionItem(DesignerDataConnection designerDataConnection)
				{
					this._designerDataConnection = designerDataConnection;
				}

				// Token: 0x17000A19 RID: 2585
				// (get) Token: 0x060033D3 RID: 13267 RVA: 0x0011B5F2 File Offset: 0x001197F2
				public DesignerDataConnection DesignerDataConnection
				{
					get
					{
						return this._designerDataConnection;
					}
				}

				// Token: 0x060033D4 RID: 13268 RVA: 0x0011B5FA File Offset: 0x001197FA
				public override string ToString()
				{
					if (this._designerDataConnection == null)
					{
						return SR.GetString("ConnectionStringEditor_NewConnection");
					}
					return this._designerDataConnection.Name;
				}

				// Token: 0x040022BC RID: 8892
				private DesignerDataConnection _designerDataConnection;
			}
		}

		// Token: 0x020003A0 RID: 928
		private sealed class ConnectionStringEditorDialog : DesignerForm
		{
			// Token: 0x0600259F RID: 9631 RVA: 0x000EB7B8 File Offset: 0x000E99B8
			public ConnectionStringEditorDialog(IServiceProvider serviceProvider, string providerName) : base(serviceProvider)
			{
				this.InitializeComponent();
				this.InitializeUI();
				this._providerName = providerName;
			}

			// Token: 0x170007EA RID: 2026
			// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000EB7D4 File Offset: 0x000E99D4
			// (set) Token: 0x060025A1 RID: 9633 RVA: 0x000EB7E4 File Offset: 0x000E99E4
			public string ConnectionString
			{
				get
				{
					return this._connectionStringTextBox.Text;
				}
				set
				{
					if (!string.IsNullOrEmpty(value))
					{
						this._connectionStringTextBox.Text = value;
						return;
					}
					if (string.IsNullOrEmpty(this._providerName))
					{
						this._connectionStringTextBox.Text = this.DefaultConnectionStrings["System.Data.SqlClient"];
						return;
					}
					this._connectionStringTextBox.Text = this.DefaultConnectionStrings[this._providerName];
				}
			}

			// Token: 0x170007EB RID: 2027
			// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000EB84C File Offset: 0x000E9A4C
			private NameValueCollection DefaultConnectionStrings
			{
				get
				{
					if (this._defaultConnectionStrings == null)
					{
						this._defaultConnectionStrings = new NameValueCollection();
						this._defaultConnectionStrings.Add("System.Data.SqlClient", "server=(local); trusted_connection=true; database=[database]");
						this._defaultConnectionStrings.Add("System.Data.Odbc", "Driver=[driver]; Server=[server]; Database=[database]; Uid=[username]; Pwd=[password]");
						this._defaultConnectionStrings.Add("System.Data.OleDb", "Provider=[provider]; Data Source=[server]; Initial Catalog=[database]; User Id=[username]; Password=[password]");
						this._defaultConnectionStrings.Add("System.Data.OracleClient", "Data Source=Oracle8i; Integrated Security=SSPI");
					}
					return this._defaultConnectionStrings;
				}
			}

			// Token: 0x170007EC RID: 2028
			// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000EB8C6 File Offset: 0x000E9AC6
			protected override string HelpTopic
			{
				get
				{
					return "net.Asp.ConnectionStrings.Editor";
				}
			}

			// Token: 0x060025A4 RID: 9636 RVA: 0x000EB8D0 File Offset: 0x000E9AD0
			private void InitializeComponent()
			{
				this._helpLabel = new Label();
				this._okButton = new Button();
				this._cancelButton = new Button();
				this._connectionStringTextBox = new TextBox();
				base.SuspendLayout();
				this._helpLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._helpLabel.Location = new Point(12, 12);
				this._helpLabel.Name = "_helpLabel";
				this._helpLabel.Size = new Size(369, 16);
				this._helpLabel.TabIndex = 10;
				this._okButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				this._okButton.Location = new Point(228, 233);
				this._okButton.Name = "_okButton";
				this._okButton.TabIndex = 30;
				this._okButton.Click += this.OnOkButtonClick;
				this._cancelButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				this._cancelButton.DialogResult = DialogResult.Cancel;
				this._cancelButton.Location = new Point(310, 233);
				this._cancelButton.Name = "_cancelButton";
				this._cancelButton.TabIndex = 40;
				this._cancelButton.Click += this.OnCancelButtonClick;
				this._connectionStringTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this._connectionStringTextBox.Location = new Point(12, 36);
				this._connectionStringTextBox.Multiline = true;
				this._connectionStringTextBox.Name = "_connectionStringTextBox";
				this._connectionStringTextBox.Size = new Size(369, 190);
				this._connectionStringTextBox.TabIndex = 20;
				base.AcceptButton = this._okButton;
				this.AutoSize = true;
				base.CancelButton = this._cancelButton;
				base.ClientSize = new Size(392, 266);
				base.Controls.Add(this._connectionStringTextBox);
				base.Controls.Add(this._cancelButton);
				base.Controls.Add(this._okButton);
				base.Controls.Add(this._helpLabel);
				this.MinimumSize = new Size(400, 300);
				base.Name = "Form1";
				base.SizeGripStyle = SizeGripStyle.Hide;
				base.InitializeForm();
				base.ResumeLayout(false);
				base.PerformLayout();
			}

			// Token: 0x060025A5 RID: 9637 RVA: 0x000EBB3C File Offset: 0x000E9D3C
			private void InitializeUI()
			{
				this._helpLabel.Text = SR.GetString("ConnectionStringEditor_HelpLabel");
				this._okButton.Text = SR.GetString("OK");
				this._cancelButton.Text = SR.GetString("Cancel");
				this.Text = SR.GetString("ConnectionStringEditor_Title");
			}

			// Token: 0x060025A6 RID: 9638 RVA: 0x0002AF61 File Offset: 0x00029161
			private void OnCancelButtonClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Cancel;
				base.Close();
			}

			// Token: 0x060025A7 RID: 9639 RVA: 0x000357ED File Offset: 0x000339ED
			private void OnOkButtonClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}

			// Token: 0x04001B79 RID: 7033
			private Label _helpLabel;

			// Token: 0x04001B7A RID: 7034
			private Button _okButton;

			// Token: 0x04001B7B RID: 7035
			private Button _cancelButton;

			// Token: 0x04001B7C RID: 7036
			private TextBox _connectionStringTextBox;

			// Token: 0x04001B7D RID: 7037
			private NameValueCollection _defaultConnectionStrings;

			// Token: 0x04001B7E RID: 7038
			private string _providerName;
		}
	}
}
