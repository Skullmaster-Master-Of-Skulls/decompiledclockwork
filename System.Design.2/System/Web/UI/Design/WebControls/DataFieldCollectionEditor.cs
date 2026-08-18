using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B8 RID: 184
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class DataFieldCollectionEditor : StringCollectionEditor
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0001EFCE File Offset: 0x0001D1CE
		public DataFieldCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		private bool HasSchema
		{
			get
			{
				ITypeDescriptorContext context = base.Context;
				bool result = false;
				if (context != null && context.Instance != null)
				{
					Control control = context.Instance as Control;
					if (control != null)
					{
						ISite site = control.Site;
						if (site != null)
						{
							IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
							if (designerHost != null)
							{
								IDesigner designer = designerHost.GetDesigner(control);
								DataBoundControlDesigner dataBoundControlDesigner = designer as DataBoundControlDesigner;
								if (dataBoundControlDesigner != null)
								{
									DesignerDataSourceView designerView = dataBoundControlDesigner.DesignerView;
									if (designerView != null)
									{
										IDataSourceViewSchema dataSourceViewSchema = null;
										try
										{
											dataSourceViewSchema = designerView.Schema;
										}
										catch (Exception ex)
										{
											IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)site.GetService(typeof(IComponentDesignerDebugService));
											if (componentDesignerDebugService != null)
											{
												componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
												{
													"DesignerDataSourceView.Schema",
													ex.Message
												}));
											}
										}
										if (dataSourceViewSchema != null)
										{
											result = true;
										}
									}
								}
							}
						}
					}
					else
					{
						IDataSourceViewSchemaAccessor dataSourceViewSchemaAccessor = context.Instance as IDataSourceViewSchemaAccessor;
						if (dataSourceViewSchemaAccessor != null && dataSourceViewSchemaAccessor.DataSourceViewSchema != null)
						{
							result = true;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			if (this.HasSchema)
			{
				ITypeDescriptorContext context = base.Context;
				if (context != null && context.Instance != null)
				{
					Control control = context.Instance as Control;
					if (control != null)
					{
						ISite site = control.Site;
						return new DataFieldCollectionEditor.DataFieldCollectionForm(site, this);
					}
				}
			}
			return base.CreateCollectionForm();
		}

		// Token: 0x04000325 RID: 805
		private const int SC_CONTEXTHELP = 61824;

		// Token: 0x04000326 RID: 806
		private const int WM_SYSCOMMAND = 274;

		// Token: 0x020003FA RID: 1018
		private class DataFieldCollectionForm : CollectionEditor.CollectionForm
		{
			// Token: 0x06002784 RID: 10116 RVA: 0x000F2E2C File Offset: 0x000F102C
			public DataFieldCollectionForm(IServiceProvider serviceProvider, CollectionEditor editor) : base(editor)
			{
				this.editor = (DataFieldCollectionEditor)editor;
				this._serviceProvider = serviceProvider;
				string @string = SR.GetString("RTL");
				if (!string.Equals(@string, "RTL_False", StringComparison.Ordinal))
				{
					this.RightToLeft = RightToLeft.Yes;
					this.RightToLeftLayout = true;
				}
				this.InitializeComponent();
				this._dataFields = this.GetControlDataFieldNames();
			}

			// Token: 0x06002785 RID: 10117 RVA: 0x000F2F1C File Offset: 0x000F111C
			private void AddFieldToSelectedList()
			{
				int selectedIndex = this.fieldsList.SelectedIndex;
				object selectedItem = this.fieldsList.SelectedItem;
				if (selectedIndex >= 0)
				{
					this.fieldsList.Items.RemoveAt(selectedIndex);
					this.selectedFieldsList.SelectedIndex = this.selectedFieldsList.Items.Add(selectedItem);
					if (this.fieldsList.Items.Count > 0)
					{
						this.fieldsList.SelectedIndex = ((this.fieldsList.Items.Count > selectedIndex) ? selectedIndex : (this.fieldsList.Items.Count - 1));
					}
				}
			}

			// Token: 0x06002786 RID: 10118 RVA: 0x000F2FB8 File Offset: 0x000F11B8
			private string[] GetControlDataFieldNames()
			{
				if (this._dataFields == null)
				{
					ITypeDescriptorContext context = this.editor.Context;
					IDataSourceViewSchema dataSourceViewSchema = null;
					if (context != null && context.Instance != null)
					{
						Control control = context.Instance as Control;
						if (control != null)
						{
							ISite site = control.Site;
							if (site == null)
							{
								goto IL_101;
							}
							IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
							if (designerHost == null)
							{
								goto IL_101;
							}
							IDesigner designer = designerHost.GetDesigner(control);
							DataBoundControlDesigner dataBoundControlDesigner = designer as DataBoundControlDesigner;
							if (dataBoundControlDesigner == null)
							{
								goto IL_101;
							}
							DesignerDataSourceView designerView = dataBoundControlDesigner.DesignerView;
							if (designerView == null)
							{
								goto IL_101;
							}
							try
							{
								dataSourceViewSchema = designerView.Schema;
								goto IL_101;
							}
							catch (Exception ex)
							{
								IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)site.GetService(typeof(IComponentDesignerDebugService));
								if (componentDesignerDebugService != null)
								{
									componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
									{
										"DesignerDataSourceView.Schema",
										ex.Message
									}));
								}
								goto IL_101;
							}
						}
						IDataSourceViewSchemaAccessor dataSourceViewSchemaAccessor = context.Instance as IDataSourceViewSchemaAccessor;
						if (dataSourceViewSchemaAccessor != null)
						{
							dataSourceViewSchema = (dataSourceViewSchemaAccessor.DataSourceViewSchema as IDataSourceViewSchema);
						}
					}
					IL_101:
					if (dataSourceViewSchema != null)
					{
						IDataSourceFieldSchema[] array = dataSourceViewSchema.GetFields();
						if (array != null)
						{
							int num = array.Length;
							this._dataFields = new string[num];
							for (int i = 0; i < num; i++)
							{
								this._dataFields[i] = array[i].Name;
							}
						}
					}
				}
				return this._dataFields;
			}

			// Token: 0x06002787 RID: 10119 RVA: 0x000F3120 File Offset: 0x000F1320
			private void InitializeComponent()
			{
				int num = 217;
				int num2 = 364;
				base.SuspendLayout();
				this.fieldLabel.AutoSize = true;
				this.fieldLabel.TabStop = false;
				this.fieldLabel.TabIndex = 0;
				this.fieldLabel.Text = SR.GetString("DataFieldCollectionAvailableFields");
				this.fieldLabel.MinimumSize = new Size(135, 15);
				this.fieldLabel.MaximumSize = new Size(135, 30);
				this.fieldLabel.SetBounds(0, 0, 135, 15);
				this.selectedFieldsLabel.AutoSize = true;
				this.selectedFieldsLabel.TabStop = false;
				this.selectedFieldsLabel.Text = SR.GetString("DataFieldCollectionSelectedFields");
				this.selectedFieldsLabel.MinimumSize = new Size(135, 15);
				this.selectedFieldsLabel.MaximumSize = new Size(135, 30);
				this.selectedFieldsLabel.SetBounds(173, 0, 135, 15);
				this.fieldsList.TabIndex = 1;
				this.fieldsList.AllowDrop = false;
				this.fieldsList.SelectedIndexChanged += this.OnFieldsSelectedIndexChanged;
				this.fieldsList.MouseDoubleClick += this.OnDoubleClickField;
				this.fieldsList.KeyPress += this.OnKeyPressField;
				this.fieldsList.SetBounds(0, 0, 135, 130);
				this.selectedFieldsList.TabIndex = 3;
				this.selectedFieldsList.AllowDrop = false;
				this.selectedFieldsList.SelectedIndexChanged += this.OnSelectedFieldsSelectedIndexChanged;
				this.selectedFieldsList.MouseDoubleClick += this.OnDoubleClickSelectedField;
				this.selectedFieldsList.KeyPress += this.OnKeyPressSelectedField;
				this.selectedFieldsList.SetBounds(0, 0, 135, 130);
				this.moveRight.TabIndex = 100;
				this.moveRight.Text = ">";
				this.moveRight.AccessibleName = SR.GetString("DataFieldCollection_MoveRight");
				this.moveRight.AccessibleDescription = SR.GetString("DataFieldCollection_MoveRightDesc");
				this.moveRight.Click += this.OnMoveRight;
				this.moveRight.Location = new Point(0, 42);
				this.moveRight.Size = new Size(26, 23);
				this.moveLeft.TabIndex = 101;
				this.moveLeft.Text = "<";
				this.moveLeft.AccessibleName = SR.GetString("DataFieldCollection_MoveLeft");
				this.moveLeft.AccessibleDescription = SR.GetString("DataFieldCollection_MoveLeftDesc");
				this.moveLeft.Click += this.OnMoveLeft;
				this.moveLeft.Location = new Point(0, 65);
				this.moveLeft.Size = new Size(26, 23);
				this.moveLeftRightPanel.TabIndex = 2;
				this.moveLeftRightPanel.Location = new Point(6, 0);
				this.moveLeftRightPanel.Size = new Size(28, 130);
				this.moveLeftRightPanel.Controls.Add(this.moveLeft);
				this.moveLeftRightPanel.Controls.Add(this.moveRight);
				this.moveUp.TabIndex = 200;
				Bitmap bitmap = BitmapSelector.CreateIcon(base.GetType(), "SortUp.ico").ToBitmap();
				bitmap.MakeTransparent();
				this.moveUp.Image = bitmap;
				this.moveUp.AccessibleName = SR.GetString("DataFieldCollection_MoveUp");
				this.moveUp.AccessibleDescription = SR.GetString("DataFieldCollection_MoveUpDesc");
				this.moveUp.Click += this.OnMoveUp;
				this.moveUp.Location = new Point(0, 0);
				this.moveUp.Size = new Size(26, 23);
				this.moveDown.TabIndex = 201;
				Bitmap bitmap2 = BitmapSelector.CreateIcon(base.GetType(), "SortDown.ico").ToBitmap();
				bitmap2.MakeTransparent();
				this.moveDown.Image = bitmap2;
				this.moveDown.AccessibleName = SR.GetString("DataFieldCollection_MoveDown");
				this.moveDown.AccessibleDescription = SR.GetString("DataFieldCollection_MoveDownDesc");
				this.moveDown.Click += this.OnMoveDown;
				this.moveDown.Location = new Point(0, 24);
				this.moveDown.Size = new Size(26, 23);
				this.moveUpDownPanel.TabIndex = 4;
				this.moveUpDownPanel.Location = new Point(6, 0);
				this.moveUpDownPanel.Size = new Size(26, 47);
				this.moveUpDownPanel.Controls.Add(this.moveUp);
				this.moveUpDownPanel.Controls.Add(this.moveDown);
				this.okButton.TabIndex = 5;
				this.okButton.Text = SR.GetString("OKCaption");
				this.okButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				this.okButton.DialogResult = DialogResult.OK;
				this.okButton.Click += this.OKButton_click;
				this.okButton.SetBounds(num2 - 12 - 150 - 6, num - 12 - 23, 75, 23);
				this.cancelButton.TabIndex = 6;
				this.cancelButton.Text = SR.GetString("CancelCaption");
				this.cancelButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				this.cancelButton.DialogResult = DialogResult.Cancel;
				this.cancelButton.SetBounds(num2 - 12 - 75, num - 12 - 23, 75, 23);
				this.layoutPanel.AutoSize = true;
				this.layoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				this.layoutPanel.ColumnCount = 4;
				this.layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135f));
				this.layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38f));
				this.layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135f));
				this.layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32f));
				this.layoutPanel.Location = new Point(12, 12);
				this.layoutPanel.Size = new Size(340, 147);
				this.layoutPanel.RowCount = 2;
				this.layoutPanel.RowStyles.Add(new RowStyle());
				this.layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 130f));
				this.layoutPanel.Controls.Add(this.fieldLabel, 0, 0);
				this.layoutPanel.Controls.Add(this.selectedFieldsLabel, 2, 0);
				this.layoutPanel.Controls.Add(this.fieldsList, 0, 1);
				this.layoutPanel.Controls.Add(this.selectedFieldsList, 2, 1);
				this.layoutPanel.Controls.Add(this.moveLeftRightPanel, 1, 1);
				this.layoutPanel.Controls.Add(this.moveUpDownPanel, 3, 1);
				Font dialogFont = UIServiceHelper.GetDialogFont(this._serviceProvider);
				if (dialogFont != null)
				{
					this.Font = dialogFont;
				}
				this.Text = SR.GetString("DataFieldCollectionEditorTitle");
				base.AcceptButton = this.okButton;
				this.AutoScaleBaseSize = new Size(5, 14);
				base.CancelButton = this.cancelButton;
				base.ClientSize = new Size(num2, num);
				base.FormBorderStyle = FormBorderStyle.FixedDialog;
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				string @string = SR.GetString("RTL");
				if (!string.Equals(@string, "RTL_False", StringComparison.Ordinal))
				{
					this.RightToLeft = RightToLeft.Yes;
					this.RightToLeftLayout = true;
				}
				base.ShowIcon = false;
				base.ShowInTaskbar = false;
				base.StartPosition = FormStartPosition.CenterParent;
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.layoutPanel,
					this.okButton,
					this.cancelButton
				});
				base.ResumeLayout(false);
				base.PerformLayout();
			}

			// Token: 0x06002788 RID: 10120 RVA: 0x000F3974 File Offset: 0x000F1B74
			private void OKButton_click(object sender, EventArgs e)
			{
				int count = this.selectedFieldsList.Items.Count;
				object[] array = new object[count];
				this.selectedFieldsList.Items.CopyTo(array, 0);
				base.Items = array;
			}

			// Token: 0x06002789 RID: 10121 RVA: 0x000F39B2 File Offset: 0x000F1BB2
			private void OnDoubleClickField(object sender, MouseEventArgs e)
			{
				if (this.fieldsList.IndexFromPoint(e.Location) != -1 && e.Button == MouseButtons.Left)
				{
					this.AddFieldToSelectedList();
				}
			}

			// Token: 0x0600278A RID: 10122 RVA: 0x000F39DB File Offset: 0x000F1BDB
			private void OnDoubleClickSelectedField(object sender, MouseEventArgs e)
			{
				if (this.selectedFieldsList.IndexFromPoint(e.Location) != -1 && e.Button == MouseButtons.Left)
				{
					this.RemoveFieldFromSelectedList();
				}
			}

			// Token: 0x0600278B RID: 10123 RVA: 0x000F3A04 File Offset: 0x000F1C04
			protected override void OnEditValueChanged()
			{
				this.fields = null;
				this.fieldsList.Items.Clear();
				this.selectedFieldsList.Items.Clear();
				this.fields = new ArrayList();
				foreach (string text in this.GetControlDataFieldNames())
				{
					this.fields.Add(text);
					if (Array.IndexOf<object>(base.Items, text) < 0)
					{
						this.fieldsList.Items.Add(text);
					}
				}
				foreach (string item in base.Items)
				{
					this.selectedFieldsList.Items.Add(item);
				}
				if (this.fieldsList.Items.Count > 0)
				{
					this.fieldsList.SelectedIndex = 0;
				}
				this.SetButtonsEnabled();
			}

			// Token: 0x0600278C RID: 10124 RVA: 0x000F3AE6 File Offset: 0x000F1CE6
			private void OnFieldsSelectedIndexChanged(object sender, EventArgs e)
			{
				if (this.fieldsList.SelectedIndex > -1)
				{
					this.selectedFieldsList.SelectedIndex = -1;
				}
				this.SetButtonsEnabled();
			}

			// Token: 0x0600278D RID: 10125 RVA: 0x000F3B08 File Offset: 0x000F1D08
			private void OnKeyPressField(object sender, KeyPressEventArgs e)
			{
				if (e.KeyChar == '\r')
				{
					this.AddFieldToSelectedList();
					e.Handled = true;
				}
			}

			// Token: 0x0600278E RID: 10126 RVA: 0x000F3B21 File Offset: 0x000F1D21
			private void OnKeyPressSelectedField(object sender, KeyPressEventArgs e)
			{
				if (e.KeyChar == '\r')
				{
					this.RemoveFieldFromSelectedList();
					e.Handled = true;
				}
			}

			// Token: 0x0600278F RID: 10127 RVA: 0x000F3B3C File Offset: 0x000F1D3C
			private void OnMoveDown(object sender, EventArgs e)
			{
				int selectedIndex = this.selectedFieldsList.SelectedIndex;
				object selectedItem = this.selectedFieldsList.SelectedItem;
				this.selectedFieldsList.Items.RemoveAt(selectedIndex);
				this.selectedFieldsList.Items.Insert(selectedIndex + 1, selectedItem);
				this.selectedFieldsList.SelectedIndex = selectedIndex + 1;
			}

			// Token: 0x06002790 RID: 10128 RVA: 0x000F3B94 File Offset: 0x000F1D94
			private void OnMoveLeft(object sender, EventArgs e)
			{
				this.RemoveFieldFromSelectedList();
			}

			// Token: 0x06002791 RID: 10129 RVA: 0x000F3B9C File Offset: 0x000F1D9C
			private void OnMoveRight(object sender, EventArgs e)
			{
				this.AddFieldToSelectedList();
			}

			// Token: 0x06002792 RID: 10130 RVA: 0x000F3BA4 File Offset: 0x000F1DA4
			private void OnMoveUp(object sender, EventArgs e)
			{
				int selectedIndex = this.selectedFieldsList.SelectedIndex;
				object selectedItem = this.selectedFieldsList.SelectedItem;
				this.selectedFieldsList.Items.RemoveAt(selectedIndex);
				this.selectedFieldsList.Items.Insert(selectedIndex - 1, selectedItem);
				this.selectedFieldsList.SelectedIndex = selectedIndex - 1;
			}

			// Token: 0x06002793 RID: 10131 RVA: 0x000F3BFC File Offset: 0x000F1DFC
			private void OnSelectedFieldsSelectedIndexChanged(object sender, EventArgs e)
			{
				if (this.selectedFieldsList.SelectedIndex > -1)
				{
					this.fieldsList.SelectedIndex = -1;
				}
				this.SetButtonsEnabled();
			}

			// Token: 0x06002794 RID: 10132 RVA: 0x000F3C20 File Offset: 0x000F1E20
			private void RemoveFieldFromSelectedList()
			{
				int selectedIndex = this.selectedFieldsList.SelectedIndex;
				int num = 0;
				if (selectedIndex >= 0)
				{
					string text = this.selectedFieldsList.SelectedItem.ToString();
					int num2 = this.fields.IndexOf(text);
					int num3 = 0;
					while (num3 < this.fieldsList.Items.Count && this.fields.IndexOf(this.fieldsList.Items[num3]) <= num2)
					{
						num++;
						num3++;
					}
					this.fieldsList.Items.Insert(num, text);
					this.selectedFieldsList.Items.RemoveAt(selectedIndex);
					this.fieldsList.SelectedIndex = num;
					if (this.selectedFieldsList.Items.Count > 0)
					{
						this.selectedFieldsList.SelectedIndex = ((this.selectedFieldsList.Items.Count > selectedIndex) ? selectedIndex : (this.selectedFieldsList.Items.Count - 1));
					}
				}
			}

			// Token: 0x06002795 RID: 10133 RVA: 0x000F3D1C File Offset: 0x000F1F1C
			private void SetButtonsEnabled()
			{
				int count = this.selectedFieldsList.Items.Count;
				int selectedIndex = this.selectedFieldsList.SelectedIndex;
				bool enabled = false;
				bool enabled2 = false;
				bool enabled3 = false;
				bool enabled4 = false;
				if (this.fieldsList.SelectedIndex > -1)
				{
					enabled3 = true;
				}
				if (selectedIndex > -1)
				{
					enabled4 = true;
					if (count > 0)
					{
						if (selectedIndex > 0)
						{
							enabled = true;
						}
						if (selectedIndex < count - 1)
						{
							enabled2 = true;
						}
					}
				}
				this.moveRight.Enabled = enabled3;
				this.moveLeft.Enabled = enabled4;
				this.moveUp.Enabled = enabled;
				this.moveDown.Enabled = enabled2;
			}

			// Token: 0x06002796 RID: 10134 RVA: 0x000F3DAC File Offset: 0x000F1FAC
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 274 && (int)m.WParam == 61824)
				{
					if (this._serviceProvider != null)
					{
						IHelpService helpService = (IHelpService)this._serviceProvider.GetService(typeof(IHelpService));
						if (helpService != null)
						{
							helpService.ShowHelpFromKeyword("net.Asp.DataFieldCollectionEditor");
							return;
						}
					}
				}
				else
				{
					base.WndProc(ref m);
				}
			}

			// Token: 0x04001C44 RID: 7236
			private Label fieldLabel = new Label();

			// Token: 0x04001C45 RID: 7237
			private DataFieldCollectionEditor.DataFieldCollectionForm.ListBoxWithEnter fieldsList = new DataFieldCollectionEditor.DataFieldCollectionForm.ListBoxWithEnter();

			// Token: 0x04001C46 RID: 7238
			private Label selectedFieldsLabel = new Label();

			// Token: 0x04001C47 RID: 7239
			private DataFieldCollectionEditor.DataFieldCollectionForm.ListBoxWithEnter selectedFieldsList = new DataFieldCollectionEditor.DataFieldCollectionForm.ListBoxWithEnter();

			// Token: 0x04001C48 RID: 7240
			private Button moveLeft = new Button();

			// Token: 0x04001C49 RID: 7241
			private Button moveRight = new Button();

			// Token: 0x04001C4A RID: 7242
			private Button moveUp = new Button();

			// Token: 0x04001C4B RID: 7243
			private Button moveDown = new Button();

			// Token: 0x04001C4C RID: 7244
			private Button okButton = new Button();

			// Token: 0x04001C4D RID: 7245
			private Button cancelButton = new Button();

			// Token: 0x04001C4E RID: 7246
			private TableLayoutPanel layoutPanel = new TableLayoutPanel();

			// Token: 0x04001C4F RID: 7247
			private Panel moveUpDownPanel = new Panel();

			// Token: 0x04001C50 RID: 7248
			private Panel moveLeftRightPanel = new Panel();

			// Token: 0x04001C51 RID: 7249
			private DataFieldCollectionEditor editor;

			// Token: 0x04001C52 RID: 7250
			private ArrayList fields;

			// Token: 0x04001C53 RID: 7251
			private string[] _dataFields;

			// Token: 0x04001C54 RID: 7252
			private IServiceProvider _serviceProvider;

			// Token: 0x020005C0 RID: 1472
			private class ListBoxWithEnter : ListBox
			{
				// Token: 0x060033F6 RID: 13302 RVA: 0x0011C0D0 File Offset: 0x0011A2D0
				protected override bool IsInputKey(Keys keyData)
				{
					return keyData == Keys.Return || base.IsInputKey(keyData);
				}
			}
		}
	}
}
