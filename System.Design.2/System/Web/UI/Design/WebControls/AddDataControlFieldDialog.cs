using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000098 RID: 152
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed partial class AddDataControlFieldDialog : DesignerForm
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x000146C0 File Offset: 0x000128C0
		public AddDataControlFieldDialog(DataBoundControlDesigner controlDesigner) : base(controlDesigner.Component.Site)
		{
			this._controlDesigner = controlDesigner;
			this.IgnoreRefreshSchemaEvents();
			this.InitForm();
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000146EE File Offset: 0x000128EE
		private DataBoundControl Control
		{
			get
			{
				return this._controlDesigner.Component as DataBoundControl;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00014700 File Offset: 0x00012900
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.DataControlField.AddDataControlFieldDialog";
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00014707 File Offset: 0x00012907
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00014746 File Offset: 0x00012946
		private bool IgnoreRefreshSchema
		{
			get
			{
				if (this._controlDesigner is GridViewDesigner)
				{
					return ((GridViewDesigner)this._controlDesigner)._ignoreSchemaRefreshedEvent;
				}
				return this._controlDesigner is DetailsViewDesigner && ((DetailsViewDesigner)this._controlDesigner)._ignoreSchemaRefreshedEvent;
			}
			set
			{
				if (this._controlDesigner is GridViewDesigner)
				{
					((GridViewDesigner)this._controlDesigner)._ignoreSchemaRefreshedEvent = value;
				}
				if (this._controlDesigner is DetailsViewDesigner)
				{
					((DetailsViewDesigner)this._controlDesigner)._ignoreSchemaRefreshedEvent = value;
				}
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00014784 File Offset: 0x00012984
		private void AddControls()
		{
			this._okButton.SetBounds(162, 475, 75, 23);
			this._okButton.Click += this.OnClickOKButton;
			this._okButton.Text = SR.GetString("OKCaption");
			this._okButton.TabIndex = 201;
			this._okButton.FlatStyle = FlatStyle.System;
			this._okButton.DialogResult = DialogResult.OK;
			this._cancelButton.SetBounds(243, 475, 75, 23);
			this._cancelButton.DialogResult = DialogResult.Cancel;
			this._cancelButton.Text = SR.GetString("CancelCaption");
			this._cancelButton.FlatStyle = FlatStyle.System;
			this._cancelButton.TabIndex = 202;
			this._cancelButton.DialogResult = DialogResult.Cancel;
			this._fieldLabel.Text = SR.GetString("DCFAdd_ChooseField");
			this._fieldLabel.TabStop = false;
			this._fieldLabel.TextAlign = ContentAlignment.BottomLeft;
			this._fieldLabel.SetBounds(12, 12, 306, 17);
			this._fieldLabel.TabIndex = 0;
			this._fieldList.DropDownStyle = ComboBoxStyle.DropDownList;
			this._fieldList.TabIndex = 1;
			this._controlsPanel.SetBounds(12, this.fieldControlTop, 330, 510 - this.fieldControlTop - 12 - 23 - 4);
			this._controlsPanel.TabIndex = 100;
			for (int i = 0; i < this.GetDataControlFieldControls().Length; i++)
			{
				AddDataControlFieldDialog.DataControlFieldControl dataControlFieldControl = this.GetDataControlFieldControls()[i];
				this._fieldList.Items.Add(dataControlFieldControl.FieldName);
				dataControlFieldControl.Visible = false;
				dataControlFieldControl.TabStop = false;
				dataControlFieldControl.SetBounds(0, 0, 330, 510 - this.fieldControlTop - 12 - 23 - 4);
				this._controlsPanel.Controls.Add(dataControlFieldControl);
			}
			this._fieldList.SelectedIndex = 0;
			this._fieldList.SelectedIndexChanged += this.OnSelectedFieldTypeChanged;
			this.SetSelectedFieldControlVisible();
			this._fieldList.SetBounds(12, 31, 150, 20);
			this._refreshSchemaLink.SetBounds(12, 475, 100, 42);
			this._refreshSchemaLink.TabIndex = 200;
			this._refreshSchemaLink.Visible = false;
			this._refreshSchemaLink.Text = SR.GetString("DataSourceDesigner_RefreshSchemaNoHotkey");
			this._refreshSchemaLink.UseMnemonic = true;
			this._refreshSchemaLink.LinkClicked += this.OnClickRefreshSchema;
			base.AcceptButton = this._okButton;
			base.CancelButton = this._cancelButton;
			base.Controls.AddRange(new Control[]
			{
				this._cancelButton,
				this._okButton,
				this._fieldLabel,
				this._fieldList,
				this._controlsPanel,
				this._refreshSchemaLink
			});
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00014A80 File Offset: 0x00012C80
		private IDataSourceFieldSchema[] GetBooleanFieldSchemas()
		{
			IDataSourceFieldSchema[] fieldSchemas = this.GetFieldSchemas();
			ArrayList arrayList = new ArrayList();
			IDataSourceFieldSchema[] array = null;
			if (fieldSchemas != null)
			{
				foreach (IDataSourceFieldSchema dataSourceFieldSchema in fieldSchemas)
				{
					if (dataSourceFieldSchema.DataType == typeof(bool))
					{
						arrayList.Add(dataSourceFieldSchema);
					}
				}
				array = new IDataSourceFieldSchema[arrayList.Count];
				arrayList.CopyTo(array);
			}
			return array;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00014AF0 File Offset: 0x00012CF0
		private List<AddDataControlFieldDialog.DataControlFieldControl> GetDesignerDataControlFieldControls()
		{
			if (this._customFieldDesigners == null)
			{
				this._customFieldDesigners = DataControlFieldHelper.GetCustomFieldDesigners(this, this.Control);
			}
			Type type = this.Control.GetType();
			List<AddDataControlFieldDialog.DataControlFieldControl> list = new List<AddDataControlFieldDialog.DataControlFieldControl>();
			foreach (KeyValuePair<Type, DataControlFieldDesigner> keyValuePair in this._customFieldDesigners)
			{
				DataControlFieldDesigner value = keyValuePair.Value;
				list.Add(new AddDataControlFieldDialog.DataControlFieldDesignerControl(this._controlDesigner, base.ServiceProvider, value, null, type));
			}
			return list;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014B88 File Offset: 0x00012D88
		private AddDataControlFieldDialog.DataControlFieldControl[] GetDataControlFieldControls()
		{
			Type type = this.Control.GetType();
			if (this._dataControlFieldControls == null)
			{
				List<AddDataControlFieldDialog.DataControlFieldControl> designerDataControlFieldControls = this.GetDesignerDataControlFieldControls();
				DataControlFieldDesigner dynamicFieldDesigner = null;
				AddDataControlFieldDialog.DataControlFieldControl item = null;
				foreach (AddDataControlFieldDialog.DataControlFieldControl dataControlFieldControl in designerDataControlFieldControls)
				{
					AddDataControlFieldDialog.DataControlFieldDesignerControl dataControlFieldDesignerControl = dataControlFieldControl as AddDataControlFieldDialog.DataControlFieldDesignerControl;
					if (dataControlFieldDesignerControl != null && dataControlFieldDesignerControl.Designer.GetType().FullName == "System.Web.DynamicData.Design.DynamicFieldDesigner")
					{
						item = dataControlFieldDesignerControl;
						dynamicFieldDesigner = dataControlFieldDesignerControl.Designer;
						this._dynamicDataEnabled = true;
						break;
					}
				}
				if (this._dynamicDataEnabled)
				{
					designerDataControlFieldControls.Remove(item);
				}
				int num = this._dynamicDataEnabled ? 8 : 7;
				this._dataControlFieldControls = new AddDataControlFieldDialog.DataControlFieldControl[num + designerDataControlFieldControls.Count];
				this._dataControlFieldControls[0] = new AddDataControlFieldDialog.BoundFieldControl(this.GetFieldSchemas(), type);
				this._dataControlFieldControls[1] = new AddDataControlFieldDialog.CheckBoxFieldControl(this.GetBooleanFieldSchemas(), type);
				this._dataControlFieldControls[2] = new AddDataControlFieldDialog.HyperLinkFieldControl(this.GetFieldSchemas(), type);
				this._dataControlFieldControls[3] = new AddDataControlFieldDialog.ButtonFieldControl(null, type);
				this._dataControlFieldControls[4] = new AddDataControlFieldDialog.CommandFieldControl(null, type);
				this._dataControlFieldControls[5] = new AddDataControlFieldDialog.ImageFieldControl(this.GetFieldSchemas(), type);
				this._dataControlFieldControls[6] = new AddDataControlFieldDialog.TemplateFieldControl(null, type);
				if (this._dynamicDataEnabled)
				{
					this._dataControlFieldControls[7] = new AddDataControlFieldDialog.DynamicDataFieldControl(dynamicFieldDesigner, this.GetFieldSchemas(), type);
				}
				int num2 = num;
				foreach (AddDataControlFieldDialog.DataControlFieldControl dataControlFieldControl2 in designerDataControlFieldControls)
				{
					this._dataControlFieldControls[num2++] = dataControlFieldControl2;
				}
			}
			return this._dataControlFieldControls;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014D50 File Offset: 0x00012F50
		private IDataSourceFieldSchema[] GetFieldSchemas()
		{
			if (this._fieldSchemas == null)
			{
				IDataSourceViewSchema dataSourceViewSchema = null;
				if (this._controlDesigner != null)
				{
					DesignerDataSourceView designerView = this._controlDesigner.DesignerView;
					if (designerView != null)
					{
						try
						{
							dataSourceViewSchema = designerView.Schema;
						}
						catch (Exception ex)
						{
							IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)base.ServiceProvider.GetService(typeof(IComponentDesignerDebugService));
							if (componentDesignerDebugService != null)
							{
								componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
								{
									"DesignerDataSourceView.Schema",
									ex.Message
								}));
							}
						}
					}
				}
				if (dataSourceViewSchema != null)
				{
					this._fieldSchemas = dataSourceViewSchema.GetFields();
				}
			}
			return this._fieldSchemas;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00014DF4 File Offset: 0x00012FF4
		private void IgnoreRefreshSchemaEvents()
		{
			this._initialIgnoreRefreshSchemaValue = this.IgnoreRefreshSchema;
			this.IgnoreRefreshSchema = true;
			IDataSourceDesigner dataSourceDesigner = this._controlDesigner.DataSourceDesigner;
			if (dataSourceDesigner != null)
			{
				dataSourceDesigner.SuppressDataSourceEvents();
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014E2C File Offset: 0x0001302C
		private void InitForm()
		{
			base.SuspendLayout();
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._fieldLabel = new System.Windows.Forms.Label();
			this._fieldList = new ComboBox();
			this._refreshSchemaLink = new LinkLabel();
			this._controlsPanel = new System.Windows.Forms.Panel();
			this.AddControls();
			IDataSourceDesigner dataSourceDesigner = this._controlDesigner.DataSourceDesigner;
			if (dataSourceDesigner != null && dataSourceDesigner.CanRefreshSchema)
			{
				this._refreshSchemaLink.Visible = true;
			}
			this.Text = SR.GetString("DCFAdd_Title");
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.ClientSize = new Size(330, 510);
			base.AcceptButton = this._okButton;
			base.CancelButton = this._cancelButton;
			base.Icon = null;
			base.InitializeForm();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00014F08 File Offset: 0x00013108
		private void OnClickOKButton(object sender, EventArgs e)
		{
			AddDataControlFieldDialog.DataControlFieldControl dataControlFieldControl = this.GetDataControlFieldControls()[this._fieldList.SelectedIndex];
			DataBoundControl control = this.Control;
			if (control is GridView)
			{
				((GridView)control).Columns.Add(dataControlFieldControl.SaveValues());
				return;
			}
			if (control is DetailsView)
			{
				((DetailsView)control).Fields.Add(dataControlFieldControl.SaveValues());
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00014F6C File Offset: 0x0001316C
		private void OnClickRefreshSchema(object source, LinkLabelLinkClickedEventArgs e)
		{
			if (this._controlDesigner != null)
			{
				IDataSourceDesigner dataSourceDesigner = this._controlDesigner.DataSourceDesigner;
				if (dataSourceDesigner != null && dataSourceDesigner.CanRefreshSchema)
				{
					IDictionary table = this.GetDataControlFieldControls()[this._fieldList.SelectedIndex].PreserveFields();
					dataSourceDesigner.RefreshSchema(false);
					this._fieldSchemas = this.GetFieldSchemas();
					this.GetDataControlFieldControls()[0].RefreshSchema(this._fieldSchemas);
					this.GetDataControlFieldControls()[1].RefreshSchema(this.GetBooleanFieldSchemas());
					this.GetDataControlFieldControls()[2].RefreshSchema(this._fieldSchemas);
					this.GetDataControlFieldControls()[5].RefreshSchema(this._fieldSchemas);
					if (this._dynamicDataEnabled)
					{
						this._dataControlFieldControls[7].RefreshSchema(this._fieldSchemas);
					}
					this.GetDataControlFieldControls()[this._fieldList.SelectedIndex].RestoreFields(table);
				}
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001504C File Offset: 0x0001324C
		protected override void OnClosed(EventArgs e)
		{
			IDataSourceDesigner dataSourceDesigner = this._controlDesigner.DataSourceDesigner;
			if (dataSourceDesigner != null)
			{
				dataSourceDesigner.ResumeDataSourceEvents();
			}
			this.IgnoreRefreshSchema = this._initialIgnoreRefreshSchemaValue;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001507A File Offset: 0x0001327A
		private void OnSelectedFieldTypeChanged(object sender, EventArgs e)
		{
			this.SetSelectedFieldControlVisible();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00015084 File Offset: 0x00013284
		private void SetSelectedFieldControlVisible()
		{
			foreach (AddDataControlFieldDialog.DataControlFieldControl dataControlFieldControl in this.GetDataControlFieldControls())
			{
				dataControlFieldControl.Visible = false;
			}
			this.GetDataControlFieldControls()[this._fieldList.SelectedIndex].Visible = true;
			this.Refresh();
		}

		// Token: 0x040001CC RID: 460
		private bool _dynamicDataEnabled;

		// Token: 0x040001CD RID: 461
		private DataBoundControlDesigner _controlDesigner;

		// Token: 0x040001CE RID: 462
		private AddDataControlFieldDialog.DataControlFieldControl[] _dataControlFieldControls;

		// Token: 0x040001CF RID: 463
		private IDataSourceFieldSchema[] _fieldSchemas;

		// Token: 0x040001D0 RID: 464
		private bool _initialIgnoreRefreshSchemaValue;

		// Token: 0x040001D1 RID: 465
		private System.Windows.Forms.Button _okButton;

		// Token: 0x040001D2 RID: 466
		private System.Windows.Forms.Button _cancelButton;

		// Token: 0x040001D3 RID: 467
		private System.Windows.Forms.Label _fieldLabel;

		// Token: 0x040001D4 RID: 468
		private ComboBox _fieldList;

		// Token: 0x040001D5 RID: 469
		private LinkLabel _refreshSchemaLink;

		// Token: 0x040001D6 RID: 470
		private System.Windows.Forms.Panel _controlsPanel;

		// Token: 0x040001D7 RID: 471
		private const int buttonWidth = 75;

		// Token: 0x040001D8 RID: 472
		private const int buttonHeight = 23;

		// Token: 0x040001D9 RID: 473
		private const int formHeight = 510;

		// Token: 0x040001DA RID: 474
		private const int formWidth = 330;

		// Token: 0x040001DB RID: 475
		private const int labelLeft = 12;

		// Token: 0x040001DC RID: 476
		private const int labelHeight = 17;

		// Token: 0x040001DD RID: 477
		private const int labelPadding = 2;

		// Token: 0x040001DE RID: 478
		private const int labelWidth = 270;

		// Token: 0x040001DF RID: 479
		private const int controlLeft = 12;

		// Token: 0x040001E0 RID: 480
		private const int controlHeight = 20;

		// Token: 0x040001E1 RID: 481
		private const int fieldChooserWidth = 150;

		// Token: 0x040001E2 RID: 482
		private const int textBoxWidth = 270;

		// Token: 0x040001E3 RID: 483
		private const int vertPadding = 4;

		// Token: 0x040001E4 RID: 484
		private const int horizPadding = 6;

		// Token: 0x040001E5 RID: 485
		private const int topPadding = 12;

		// Token: 0x040001E6 RID: 486
		private const int bottomPadding = 12;

		// Token: 0x040001E7 RID: 487
		private const int rightPadding = 12;

		// Token: 0x040001E8 RID: 488
		private const int linkWidth = 100;

		// Token: 0x040001E9 RID: 489
		private const int checkBoxWidth = 125;

		// Token: 0x040001EA RID: 490
		private int fieldControlTop = 51;

		// Token: 0x040001EB RID: 491
		private IDictionary<Type, DataControlFieldDesigner> _customFieldDesigners;

		// Token: 0x020003C3 RID: 963
		private abstract class DataControlFieldControl : Control
		{
			// Token: 0x0600269B RID: 9883 RVA: 0x000ED718 File Offset: 0x000EB918
			public DataControlFieldControl(IDataSourceFieldSchema[] fieldSchemas, Type controlType)
			{
				this._fieldSchemas = fieldSchemas;
				if (fieldSchemas != null && fieldSchemas.Length != 0)
				{
					this._haveSchema = true;
				}
				this._controlType = controlType;
				this.InitializeComponent();
			}

			// Token: 0x1700082B RID: 2091
			// (get) Token: 0x0600269C RID: 9884
			public abstract string FieldName { get; }

			// Token: 0x0600269D RID: 9885 RVA: 0x000ED744 File Offset: 0x000EB944
			protected string[] GetFieldSchemaNames()
			{
				if (this._fieldSchemaNames == null && this._fieldSchemas != null)
				{
					int num = this._fieldSchemas.Length;
					this._fieldSchemaNames = new string[num];
					for (int i = 0; i < num; i++)
					{
						this._fieldSchemaNames[i] = this._fieldSchemas[i].Name;
					}
				}
				return this._fieldSchemaNames;
			}

			// Token: 0x0600269E RID: 9886 RVA: 0x000ED7A0 File Offset: 0x000EB9A0
			protected virtual void InitializeComponent()
			{
				this._headerTextLabel = new System.Windows.Forms.Label();
				this._headerTextBox = new System.Windows.Forms.TextBox();
				this._headerTextLabel.Text = SR.GetString("DCFAdd_HeaderText");
				this._headerTextLabel.TextAlign = ContentAlignment.BottomLeft;
				this._headerTextLabel.SetBounds(0, 0, 270, 17);
				this._headerTextBox.TabIndex = 0;
				this._headerTextBox.SetBounds(0, 19, 270, 20);
				base.Controls.AddRange(new Control[]
				{
					this._headerTextLabel,
					this._headerTextBox
				});
			}

			// Token: 0x0600269F RID: 9887 RVA: 0x000ED840 File Offset: 0x000EBA40
			public IDictionary PreserveFields()
			{
				Hashtable hashtable = new Hashtable();
				hashtable["HeaderText"] = this._headerTextBox.Text;
				this.PreserveFields(hashtable);
				return hashtable;
			}

			// Token: 0x060026A0 RID: 9888
			protected abstract void PreserveFields(IDictionary table);

			// Token: 0x060026A1 RID: 9889 RVA: 0x000ED871 File Offset: 0x000EBA71
			public void RefreshSchema(IDataSourceFieldSchema[] fieldSchemas)
			{
				this._fieldSchemas = fieldSchemas;
				this._fieldSchemaNames = null;
				if (fieldSchemas != null && fieldSchemas.Length != 0)
				{
					this._haveSchema = true;
				}
				this.RefreshSchemaFields();
			}

			// Token: 0x060026A2 RID: 9890 RVA: 0x00003937 File Offset: 0x00001B37
			protected virtual void RefreshSchemaFields()
			{
			}

			// Token: 0x060026A3 RID: 9891 RVA: 0x000ED895 File Offset: 0x000EBA95
			public void RestoreFields(IDictionary table)
			{
				this._headerTextBox.Text = table["HeaderText"].ToString();
				this.RestoreFieldsInternal(table);
			}

			// Token: 0x060026A4 RID: 9892
			protected abstract void RestoreFieldsInternal(IDictionary table);

			// Token: 0x060026A5 RID: 9893
			protected abstract DataControlField SaveValues(string headerText);

			// Token: 0x060026A6 RID: 9894 RVA: 0x000ED8BC File Offset: 0x000EBABC
			public DataControlField SaveValues()
			{
				string headerText = (this._headerTextBox == null) ? string.Empty : this._headerTextBox.Text;
				return this.SaveValues(headerText);
			}

			// Token: 0x060026A7 RID: 9895 RVA: 0x000ED8EB File Offset: 0x000EBAEB
			protected string StripAccelerators(string text)
			{
				return text.Replace("&", string.Empty);
			}

			// Token: 0x04001BD7 RID: 7127
			protected string[] _fieldSchemaNames;

			// Token: 0x04001BD8 RID: 7128
			protected Type _controlType;

			// Token: 0x04001BD9 RID: 7129
			protected bool _haveSchema;

			// Token: 0x04001BDA RID: 7130
			protected IDataSourceFieldSchema[] _fieldSchemas;

			// Token: 0x04001BDB RID: 7131
			private System.Windows.Forms.Label _headerTextLabel;

			// Token: 0x04001BDC RID: 7132
			private System.Windows.Forms.TextBox _headerTextBox;
		}

		// Token: 0x020003C4 RID: 964
		private class BoundFieldControl : AddDataControlFieldDialog.DataControlFieldControl
		{
			// Token: 0x060026A8 RID: 9896 RVA: 0x000ED8FD File Offset: 0x000EBAFD
			public BoundFieldControl(IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
			}

			// Token: 0x1700082C RID: 2092
			// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000ED907 File Offset: 0x000EBB07
			public override string FieldName
			{
				get
				{
					return "BoundField";
				}
			}

			// Token: 0x060026AA RID: 9898 RVA: 0x000ED910 File Offset: 0x000EBB10
			protected override void InitializeComponent()
			{
				base.InitializeComponent();
				this._dataFieldList = new ComboBox();
				this._dataFieldBox = new System.Windows.Forms.TextBox();
				this._dataFieldLabel = new System.Windows.Forms.Label();
				this._readOnlyCheckBox = new System.Windows.Forms.CheckBox();
				this._dataFieldLabel.Text = SR.GetString("DCFAdd_DataField");
				this._dataFieldLabel.TextAlign = ContentAlignment.BottomLeft;
				this._dataFieldLabel.SetBounds(0, 43, 270, 17);
				this._dataFieldList.DropDownStyle = ComboBoxStyle.DropDownList;
				this._dataFieldList.TabIndex = 1;
				this._dataFieldList.SetBounds(0, 62, 270, 20);
				this._dataFieldList.SelectedIndexChanged += this.OnSelectedDataFieldChanged;
				this._dataFieldBox.TabIndex = 1;
				this._dataFieldBox.SetBounds(0, 62, 270, 20);
				this._readOnlyCheckBox.TabIndex = 2;
				this._readOnlyCheckBox.Text = SR.GetString("DCFAdd_ReadOnly");
				this._readOnlyCheckBox.SetBounds(0, 86, 270, 20);
				this.RefreshSchemaFields();
				base.Controls.AddRange(new Control[]
				{
					this._dataFieldLabel,
					this._dataFieldBox,
					this._dataFieldList,
					this._readOnlyCheckBox
				});
			}

			// Token: 0x060026AB RID: 9899 RVA: 0x000EDA60 File Offset: 0x000EBC60
			private void OnSelectedDataFieldChanged(object sender, EventArgs e)
			{
				if (this._haveSchema)
				{
					int num = Array.IndexOf<string>(base.GetFieldSchemaNames(), this._dataFieldList.Text);
					if (num >= 0 && this._fieldSchemas[num].PrimaryKey)
					{
						this._readOnlyCheckBox.Checked = true;
						return;
					}
				}
				this._readOnlyCheckBox.Checked = false;
			}

			// Token: 0x060026AC RID: 9900 RVA: 0x000EDAB8 File Offset: 0x000EBCB8
			protected override void PreserveFields(IDictionary table)
			{
				if (this._haveSchema)
				{
					table["DataField"] = this._dataFieldList.Text;
				}
				else
				{
					table["DataField"] = this._dataFieldBox.Text;
				}
				table["ReadOnly"] = this._readOnlyCheckBox.Checked;
			}

			// Token: 0x060026AD RID: 9901 RVA: 0x000EDB18 File Offset: 0x000EBD18
			protected override void RefreshSchemaFields()
			{
				if (this._haveSchema)
				{
					this._dataFieldList.Items.Clear();
					ComboBox.ObjectCollection items = this._dataFieldList.Items;
					object[] fieldSchemaNames = base.GetFieldSchemaNames();
					items.AddRange(fieldSchemaNames);
					this._dataFieldList.SelectedIndex = 0;
					this._dataFieldList.Visible = true;
					this._dataFieldBox.Visible = false;
					return;
				}
				this._dataFieldList.Visible = false;
				this._dataFieldBox.Visible = true;
			}

			// Token: 0x060026AE RID: 9902 RVA: 0x000EDB94 File Offset: 0x000EBD94
			protected override void RestoreFieldsInternal(IDictionary table)
			{
				string text = table["DataField"].ToString();
				if (this._haveSchema)
				{
					if (text.Length > 0)
					{
						bool flag = false;
						foreach (object obj in this._dataFieldList.Items)
						{
							if (string.Compare(text, obj.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
							{
								this._dataFieldList.SelectedItem = obj;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this._dataFieldList.Items.Insert(0, text);
							this._dataFieldList.SelectedIndex = 0;
						}
					}
				}
				else
				{
					this._dataFieldBox.Text = text;
				}
				this._readOnlyCheckBox.Checked = (bool)table["ReadOnly"];
			}

			// Token: 0x060026AF RID: 9903 RVA: 0x000EDC7C File Offset: 0x000EBE7C
			protected override DataControlField SaveValues(string headerText)
			{
				BoundField boundField = new BoundField();
				boundField.HeaderText = headerText;
				if (this._haveSchema)
				{
					boundField.DataField = this._dataFieldList.Text;
				}
				else
				{
					boundField.DataField = this._dataFieldBox.Text;
				}
				boundField.ReadOnly = this._readOnlyCheckBox.Checked;
				boundField.SortExpression = boundField.DataField;
				return boundField;
			}

			// Token: 0x04001BDD RID: 7133
			private System.Windows.Forms.Label _dataFieldLabel;

			// Token: 0x04001BDE RID: 7134
			protected ComboBox _dataFieldList;

			// Token: 0x04001BDF RID: 7135
			protected System.Windows.Forms.TextBox _dataFieldBox;

			// Token: 0x04001BE0 RID: 7136
			protected System.Windows.Forms.CheckBox _readOnlyCheckBox;
		}

		// Token: 0x020003C5 RID: 965
		private class DynamicDataFieldControl : AddDataControlFieldDialog.BoundFieldControl
		{
			// Token: 0x060026B0 RID: 9904 RVA: 0x000EDCE0 File Offset: 0x000EBEE0
			public DynamicDataFieldControl(DataControlFieldDesigner dynamicFieldDesigner, IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
				this._designer = dynamicFieldDesigner;
			}

			// Token: 0x1700082D RID: 2093
			// (get) Token: 0x060026B1 RID: 9905 RVA: 0x000EDCF1 File Offset: 0x000EBEF1
			public override string FieldName
			{
				get
				{
					return "DynamicField";
				}
			}

			// Token: 0x060026B2 RID: 9906 RVA: 0x000EDCF8 File Offset: 0x000EBEF8
			protected override void InitializeComponent()
			{
				base.InitializeComponent();
				this._readOnlyCheckBox.Visible = false;
			}

			// Token: 0x060026B3 RID: 9907 RVA: 0x000EDD0C File Offset: 0x000EBF0C
			protected override DataControlField SaveValues(string headerText)
			{
				DataControlField dataControlField = this._designer.CreateField();
				dataControlField.HeaderText = headerText;
				string value = this._haveSchema ? this._dataFieldList.Text : this._dataFieldBox.Text;
				AddDataControlFieldDialog.DynamicDataFieldControl.SetProperty(dataControlField, "DataField", value);
				return dataControlField;
			}

			// Token: 0x060026B4 RID: 9908 RVA: 0x000EDD5A File Offset: 0x000EBF5A
			private static void SetProperty(DataControlField target, string propertyName, object value)
			{
				target.GetType().GetProperty(propertyName).SetValue(target, value, null);
			}

			// Token: 0x04001BE1 RID: 7137
			private DataControlFieldDesigner _designer;
		}

		// Token: 0x020003C6 RID: 966
		private class CheckBoxFieldControl : AddDataControlFieldDialog.DataControlFieldControl
		{
			// Token: 0x060026B5 RID: 9909 RVA: 0x000ED8FD File Offset: 0x000EBAFD
			public CheckBoxFieldControl(IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
			}

			// Token: 0x1700082E RID: 2094
			// (get) Token: 0x060026B6 RID: 9910 RVA: 0x000EDD70 File Offset: 0x000EBF70
			public override string FieldName
			{
				get
				{
					return "CheckBoxField";
				}
			}

			// Token: 0x060026B7 RID: 9911 RVA: 0x000EDD78 File Offset: 0x000EBF78
			protected override void InitializeComponent()
			{
				base.InitializeComponent();
				this._dataFieldList = new ComboBox();
				this._dataFieldBox = new System.Windows.Forms.TextBox();
				this._dataFieldLabel = new System.Windows.Forms.Label();
				this._readOnlyCheckBox = new System.Windows.Forms.CheckBox();
				this._dataFieldLabel.Text = SR.GetString("DCFAdd_DataField");
				this._dataFieldLabel.TextAlign = ContentAlignment.BottomLeft;
				this._dataFieldLabel.SetBounds(0, 43, 270, 17);
				this._dataFieldList.DropDownStyle = ComboBoxStyle.DropDownList;
				this._dataFieldList.TabIndex = 1;
				this._dataFieldList.SetBounds(0, 62, 270, 20);
				this._dataFieldBox.TabIndex = 1;
				this._dataFieldBox.SetBounds(0, 62, 270, 20);
				this._readOnlyCheckBox.TabIndex = 2;
				this._readOnlyCheckBox.Text = SR.GetString("DCFAdd_ReadOnly");
				this._readOnlyCheckBox.SetBounds(0, 86, 270, 20);
				this.RefreshSchemaFields();
				base.Controls.AddRange(new Control[]
				{
					this._dataFieldLabel,
					this._dataFieldBox,
					this._dataFieldList,
					this._readOnlyCheckBox
				});
			}

			// Token: 0x060026B8 RID: 9912 RVA: 0x000EDEB0 File Offset: 0x000EC0B0
			protected override void PreserveFields(IDictionary table)
			{
				if (this._haveSchema)
				{
					table["DataField"] = this._dataFieldList.Text;
				}
				else
				{
					table["DataField"] = this._dataFieldBox.Text;
				}
				table["ReadOnly"] = this._readOnlyCheckBox.Checked;
			}

			// Token: 0x060026B9 RID: 9913 RVA: 0x000EDF10 File Offset: 0x000EC110
			protected override void RefreshSchemaFields()
			{
				if (this._haveSchema)
				{
					this._dataFieldList.Items.Clear();
					ComboBox.ObjectCollection items = this._dataFieldList.Items;
					object[] fieldSchemaNames = base.GetFieldSchemaNames();
					items.AddRange(fieldSchemaNames);
					this._dataFieldList.SelectedIndex = 0;
					this._dataFieldList.Visible = true;
					this._dataFieldBox.Visible = false;
					return;
				}
				this._dataFieldList.Visible = false;
				this._dataFieldBox.Visible = true;
			}

			// Token: 0x060026BA RID: 9914 RVA: 0x000EDF8C File Offset: 0x000EC18C
			protected override void RestoreFieldsInternal(IDictionary table)
			{
				string text = table["DataField"].ToString();
				if (this._haveSchema)
				{
					if (text.Length > 0)
					{
						bool flag = false;
						foreach (object obj in this._dataFieldList.Items)
						{
							if (string.Compare(text, obj.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
							{
								this._dataFieldList.SelectedItem = obj;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this._dataFieldList.Items.Insert(0, text);
							this._dataFieldList.SelectedIndex = 0;
						}
					}
				}
				else
				{
					this._dataFieldBox.Text = text;
				}
				this._readOnlyCheckBox.Checked = (bool)table["ReadOnly"];
			}

			// Token: 0x060026BB RID: 9915 RVA: 0x000EE074 File Offset: 0x000EC274
			protected override DataControlField SaveValues(string headerText)
			{
				CheckBoxField checkBoxField = new CheckBoxField();
				checkBoxField.HeaderText = headerText;
				if (this._haveSchema)
				{
					checkBoxField.DataField = this._dataFieldList.Text;
				}
				else
				{
					checkBoxField.DataField = this._dataFieldBox.Text;
				}
				checkBoxField.ReadOnly = this._readOnlyCheckBox.Checked;
				checkBoxField.SortExpression = checkBoxField.DataField;
				return checkBoxField;
			}

			// Token: 0x04001BE2 RID: 7138
			private System.Windows.Forms.Label _dataFieldLabel;

			// Token: 0x04001BE3 RID: 7139
			private ComboBox _dataFieldList;

			// Token: 0x04001BE4 RID: 7140
			private System.Windows.Forms.TextBox _dataFieldBox;

			// Token: 0x04001BE5 RID: 7141
			private System.Windows.Forms.CheckBox _readOnlyCheckBox;
		}

		// Token: 0x020003C7 RID: 967
		private class ButtonFieldControl : AddDataControlFieldDialog.DataControlFieldControl
		{
			// Token: 0x060026BC RID: 9916 RVA: 0x000ED8FD File Offset: 0x000EBAFD
			public ButtonFieldControl(IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
			}

			// Token: 0x1700082F RID: 2095
			// (get) Token: 0x060026BD RID: 9917 RVA: 0x000EE0D8 File Offset: 0x000EC2D8
			public override string FieldName
			{
				get
				{
					return "ButtonField";
				}
			}

			// Token: 0x060026BE RID: 9918 RVA: 0x000EE0E0 File Offset: 0x000EC2E0
			protected override void InitializeComponent()
			{
				base.InitializeComponent();
				this._buttonTypeLabel = new System.Windows.Forms.Label();
				this._commandNameLabel = new System.Windows.Forms.Label();
				this._textLabel = new System.Windows.Forms.Label();
				this._buttonTypeList = new ComboBox();
				this._commandNameList = new ComboBox();
				this._textBox = new System.Windows.Forms.TextBox();
				this._buttonTypeLabel.Text = SR.GetString("DCFAdd_ButtonType");
				this._buttonTypeLabel.TextAlign = ContentAlignment.BottomLeft;
				this._buttonTypeLabel.SetBounds(0, 43, 270, 17);
				this._buttonTypeList.Items.Add(ButtonType.Link.ToString());
				this._buttonTypeList.Items.Add(ButtonType.Button.ToString());
				this._buttonTypeList.SelectedIndex = 0;
				this._buttonTypeList.DropDownStyle = ComboBoxStyle.DropDownList;
				this._buttonTypeList.TabIndex = 1;
				this._buttonTypeList.SetBounds(0, 62, 270, 20);
				this._commandNameLabel.Text = SR.GetString("DCFAdd_CommandName");
				this._commandNameLabel.TextAlign = ContentAlignment.BottomLeft;
				this._commandNameLabel.SetBounds(0, 86, 270, 17);
				this._commandNameList.Items.Add("Cancel");
				this._commandNameList.Items.Add("Delete");
				this._commandNameList.Items.Add("Edit");
				this._commandNameList.Items.Add("Update");
				if (this._controlType == typeof(DetailsView))
				{
					this._commandNameList.Items.Insert(3, "Insert");
					this._commandNameList.Items.Insert(4, "New");
				}
				else if (this._controlType == typeof(GridView))
				{
					this._commandNameList.Items.Insert(3, "Select");
				}
				this._commandNameList.SelectedIndex = 0;
				this._commandNameList.DropDownStyle = ComboBoxStyle.DropDownList;
				this._commandNameList.TabIndex = 2;
				this._commandNameList.SetBounds(0, 105, 270, 20);
				this._textLabel.Text = SR.GetString("DCFAdd_Text");
				this._textLabel.TextAlign = ContentAlignment.BottomLeft;
				this._textLabel.SetBounds(0, 129, 270, 17);
				this._textBox.TabIndex = 3;
				this._textBox.Text = SR.GetString("DCFEditor_Button");
				this._textBox.SetBounds(0, 148, 270, 20);
				base.Controls.AddRange(new Control[]
				{
					this._buttonTypeLabel,
					this._commandNameLabel,
					this._textLabel,
					this._buttonTypeList,
					this._commandNameList,
					this._textBox
				});
			}

			// Token: 0x060026BF RID: 9919 RVA: 0x000EE3E4 File Offset: 0x000EC5E4
			protected override void PreserveFields(IDictionary table)
			{
				table["ButtonType"] = this._buttonTypeList.SelectedIndex;
				table["CommandName"] = this._commandNameList.SelectedIndex;
				table["Text"] = this._textBox.Text;
			}

			// Token: 0x060026C0 RID: 9920 RVA: 0x000EE440 File Offset: 0x000EC640
			protected override void RestoreFieldsInternal(IDictionary table)
			{
				this._buttonTypeList.SelectedIndex = (int)table["ButtonType"];
				this._commandNameList.SelectedIndex = (int)table["CommandName"];
				this._textBox.Text = table["Text"].ToString();
			}

			// Token: 0x060026C1 RID: 9921 RVA: 0x000EE4A0 File Offset: 0x000EC6A0
			protected override DataControlField SaveValues(string headerText)
			{
				ButtonField buttonField = new ButtonField();
				if (headerText != null && headerText.Length > 0)
				{
					buttonField.HeaderText = headerText;
					buttonField.ShowHeader = true;
				}
				buttonField.CommandName = this._commandNameList.Text;
				buttonField.Text = this._textBox.Text;
				if (this._buttonTypeList.SelectedIndex == 0)
				{
					buttonField.ButtonType = ButtonType.Link;
				}
				else
				{
					buttonField.ButtonType = ButtonType.Button;
				}
				return buttonField;
			}

			// Token: 0x04001BE6 RID: 7142
			private System.Windows.Forms.Label _buttonTypeLabel;

			// Token: 0x04001BE7 RID: 7143
			private System.Windows.Forms.Label _commandNameLabel;

			// Token: 0x04001BE8 RID: 7144
			private System.Windows.Forms.Label _textLabel;

			// Token: 0x04001BE9 RID: 7145
			private ComboBox _buttonTypeList;

			// Token: 0x04001BEA RID: 7146
			private ComboBox _commandNameList;

			// Token: 0x04001BEB RID: 7147
			private System.Windows.Forms.TextBox _textBox;
		}

		// Token: 0x020003C8 RID: 968
		private class HyperLinkFieldControl : AddDataControlFieldDialog.DataControlFieldControl
		{
			// Token: 0x060026C2 RID: 9922 RVA: 0x000ED8FD File Offset: 0x000EBAFD
			public HyperLinkFieldControl(IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
			}

			// Token: 0x17000830 RID: 2096
			// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000EE50D File Offset: 0x000EC70D
			public override string FieldName
			{
				get
				{
					return "HyperLinkField";
				}
			}

			// Token: 0x060026C4 RID: 9924 RVA: 0x000EE514 File Offset: 0x000EC714
			protected override void InitializeComponent()
			{
				base.InitializeComponent();
				this._dataTextFieldBox = new System.Windows.Forms.TextBox();
				this._dataNavFieldBox = new System.Windows.Forms.TextBox();
				this._dataNavFSBox = new System.Windows.Forms.TextBox();
				this._linkBox = new System.Windows.Forms.TextBox();
				this._textBox = new System.Windows.Forms.TextBox();
				this._textFSBox = new System.Windows.Forms.TextBox();
				this._dataTextFieldList = new ComboBox();
				this._dataNavFieldList = new ComboBox();
				this._staticTextRadio = new System.Windows.Forms.RadioButton();
				this._bindTextRadio = new System.Windows.Forms.RadioButton();
				this._staticUrlRadio = new System.Windows.Forms.RadioButton();
				this._bindUrlRadio = new System.Windows.Forms.RadioButton();
				this._linkTextFormatStringLabel = new System.Windows.Forms.Label();
				this._linkUrlFormatStringLabel = new System.Windows.Forms.Label();
				this._linkTextFormatStringExampleLabel = new System.Windows.Forms.Label();
				this._linkUrlFormatStringExampleLabel = new System.Windows.Forms.Label();
				this._textGroupBox = new GroupBox();
				this._linkGroupBox = new GroupBox();
				this._staticTextPanel = new System.Windows.Forms.Panel();
				this._bindTextPanel = new System.Windows.Forms.Panel();
				this._staticUrlPanel = new System.Windows.Forms.Panel();
				this._bindUrlPanel = new System.Windows.Forms.Panel();
				this._textGroupBox.SetBounds(0, 47, 290, 169);
				this._textGroupBox.Text = SR.GetString("DCFAdd_HyperlinkText");
				this._textGroupBox.TabIndex = 1;
				this._staticTextRadio.TabIndex = 0;
				this._staticTextRadio.Text = SR.GetString("DCFAdd_SpecifyText");
				this._staticTextRadio.CheckedChanged += this.OnTextRadioChanged;
				this._staticTextRadio.Checked = true;
				this._staticTextRadio.SetBounds(9, 19, 261, 20);
				this._textBox.TabIndex = 0;
				this._textBox.SetBounds(0, 0, 246, 20);
				this._textBox.AccessibleName = base.StripAccelerators(SR.GetString("DCFAdd_SpecifyText"));
				this._staticTextPanel.TabIndex = 1;
				this._staticTextPanel.SetBounds(24, 39, 246, 24);
				this._staticTextPanel.Controls.Add(this._textBox);
				this._bindTextRadio.TabIndex = 2;
				this._bindTextRadio.Text = SR.GetString("DCFAdd_BindText");
				this._bindTextRadio.SetBounds(9, 63, 261, 20);
				this._dataTextFieldList.TabIndex = 0;
				this._dataTextFieldList.SetBounds(0, 0, 246, 20);
				this._dataTextFieldList.AccessibleName = base.StripAccelerators(SR.GetString("DCFAdd_BindText"));
				this._dataTextFieldBox.TabIndex = 1;
				this._dataTextFieldBox.SetBounds(0, 0, 246, 20);
				this._dataTextFieldBox.AccessibleName = base.StripAccelerators(SR.GetString("DCFAdd_BindText"));
				this._linkTextFormatStringLabel.Text = SR.GetString("DCFAdd_TextFormatString");
				this._linkTextFormatStringLabel.TabIndex = 2;
				this._linkTextFormatStringLabel.TextAlign = ContentAlignment.BottomLeft;
				this._linkTextFormatStringLabel.SetBounds(0, 20, 246, 17);
				this._textFSBox.TabIndex = 3;
				this._textFSBox.SetBounds(0, 39, 246, 20);
				this._linkTextFormatStringExampleLabel.Text = SR.GetString("DCFAdd_TextFormatStringExample");
				this._linkTextFormatStringExampleLabel.Enabled = false;
				this._linkTextFormatStringExampleLabel.TextAlign = ContentAlignment.BottomLeft;
				this._linkTextFormatStringExampleLabel.SetBounds(0, 59, 246, 17);
				this._bindTextPanel.TabIndex = 3;
				this._bindTextPanel.SetBounds(24, 83, 246, 78);
				this._bindTextPanel.Controls.AddRange(new Control[]
				{
					this._bindTextRadio,
					this._dataTextFieldList,
					this._dataTextFieldBox,
					this._linkTextFormatStringLabel,
					this._textFSBox,
					this._linkTextFormatStringExampleLabel
				});
				this._textGroupBox.Controls.AddRange(new Control[]
				{
					this._staticTextRadio,
					this._staticTextPanel,
					this._bindTextRadio,
					this._bindTextPanel
				});
				this._linkGroupBox.SetBounds(0, 220, 290, 173);
				this._linkGroupBox.Text = SR.GetString("DCFAdd_HyperlinkURL");
				this._linkGroupBox.TabIndex = 2;
				this._staticUrlRadio.TabIndex = 0;
				this._staticUrlRadio.Text = SR.GetString("DCFAdd_SpecifyURL");
				this._staticUrlRadio.CheckedChanged += this.OnUrlRadioChanged;
				this._staticUrlRadio.Checked = true;
				this._staticUrlRadio.SetBounds(9, 19, 261, 20);
				this._linkBox.TabIndex = 0;
				this._linkBox.SetBounds(0, 0, 246, 20);
				this._linkBox.AccessibleName = base.StripAccelerators(SR.GetString("DCFAdd_SpecifyURL"));
				this._staticUrlPanel.TabIndex = 1;
				this._staticUrlPanel.SetBounds(24, 39, 246, 24);
				this._staticUrlPanel.Controls.Add(this._linkBox);
				this._bindUrlRadio.TabIndex = 2;
				this._bindUrlRadio.Text = SR.GetString("DCFAdd_BindURL");
				this._bindUrlRadio.SetBounds(9, 63, 261, 20);
				this._dataNavFieldList.TabIndex = 0;
				this._dataNavFieldList.SetBounds(0, 0, 246, 20);
				this._dataNavFieldList.AccessibleName = base.StripAccelerators(SR.GetString("DCFAdd_BindURL"));
				this._dataNavFieldBox.TabIndex = 1;
				this._dataNavFieldBox.SetBounds(0, 0, 246, 20);
				this._dataNavFieldBox.AccessibleName = base.StripAccelerators(SR.GetString("DCFAdd_BindURL"));
				this._linkUrlFormatStringLabel.Text = SR.GetString("DCFAdd_URLFormatString");
				this._linkUrlFormatStringLabel.TabIndex = 2;
				this._linkUrlFormatStringLabel.TextAlign = ContentAlignment.BottomLeft;
				this._linkUrlFormatStringLabel.SetBounds(0, 20, 246, 17);
				this._dataNavFSBox.TabIndex = 3;
				this._dataNavFSBox.SetBounds(0, 39, 246, 20);
				this._linkUrlFormatStringExampleLabel.Text = SR.GetString("DCFAdd_URLFormatStringExample");
				this._linkUrlFormatStringExampleLabel.Enabled = false;
				this._linkUrlFormatStringExampleLabel.TextAlign = ContentAlignment.BottomLeft;
				this._linkUrlFormatStringExampleLabel.SetBounds(0, 59, 246, 17);
				this._bindUrlPanel.TabIndex = 3;
				this._bindUrlPanel.SetBounds(24, 83, 246, 78);
				this._bindUrlPanel.Controls.AddRange(new Control[]
				{
					this._dataNavFieldList,
					this._dataNavFieldBox,
					this._linkUrlFormatStringLabel,
					this._dataNavFSBox,
					this._linkUrlFormatStringExampleLabel
				});
				this._linkGroupBox.Controls.AddRange(new Control[]
				{
					this._staticUrlRadio,
					this._staticUrlPanel,
					this._bindUrlRadio,
					this._bindUrlPanel
				});
				this.RefreshSchemaFields();
				base.Controls.AddRange(new Control[]
				{
					this._textGroupBox,
					this._linkGroupBox
				});
			}

			// Token: 0x060026C5 RID: 9925 RVA: 0x000EEC4C File Offset: 0x000ECE4C
			private void OnTextRadioChanged(object sender, EventArgs e)
			{
				if (this._staticTextRadio.Checked)
				{
					this._textBox.Enabled = true;
					this._dataTextFieldList.Enabled = false;
					this._dataTextFieldBox.Enabled = false;
					this._textFSBox.Enabled = false;
					this._linkTextFormatStringLabel.Enabled = false;
					return;
				}
				this._textBox.Enabled = false;
				this._dataTextFieldList.Enabled = true;
				this._dataTextFieldBox.Enabled = true;
				this._textFSBox.Enabled = true;
				this._linkTextFormatStringLabel.Enabled = true;
			}

			// Token: 0x060026C6 RID: 9926 RVA: 0x000EECE0 File Offset: 0x000ECEE0
			private void OnUrlRadioChanged(object sender, EventArgs e)
			{
				if (this._staticUrlRadio.Checked)
				{
					this._linkBox.Enabled = true;
					this._dataNavFieldList.Enabled = false;
					this._dataNavFieldBox.Enabled = false;
					this._dataNavFSBox.Enabled = false;
					this._linkUrlFormatStringLabel.Enabled = false;
					return;
				}
				this._linkBox.Enabled = false;
				this._dataNavFieldList.Enabled = true;
				this._dataNavFieldBox.Enabled = true;
				this._dataNavFSBox.Enabled = true;
				this._linkUrlFormatStringLabel.Enabled = true;
			}

			// Token: 0x060026C7 RID: 9927 RVA: 0x000EED74 File Offset: 0x000ECF74
			protected override void PreserveFields(IDictionary table)
			{
				if (this._haveSchema)
				{
					table["DataTextField"] = this._dataTextFieldList.Text;
					table["DataNavigateUrlField"] = this._dataNavFieldList.Text;
				}
				else
				{
					table["DataTextField"] = this._dataTextFieldBox.Text;
					table["DataNavigateUrlField"] = this._dataNavFieldBox.Text;
				}
				table["DataNavigateUrlFormatString"] = this._dataNavFSBox.Text;
				table["DataTextFormatString"] = this._textFSBox.Text;
				table["NavigateUrl"] = this._linkBox.Text;
				table["linkMode"] = this._staticUrlRadio.Checked;
				table["textMode"] = this._staticTextRadio.Checked;
				table["Text"] = this._textBox.Text;
			}

			// Token: 0x060026C8 RID: 9928 RVA: 0x000EEE74 File Offset: 0x000ED074
			protected override void RefreshSchemaFields()
			{
				if (this._haveSchema)
				{
					this._dataTextFieldList.Items.Clear();
					ComboBox.ObjectCollection items = this._dataTextFieldList.Items;
					object[] fieldSchemaNames = base.GetFieldSchemaNames();
					items.AddRange(fieldSchemaNames);
					this._dataTextFieldList.Items.Insert(0, string.Empty);
					this._dataTextFieldList.SelectedIndex = 0;
					this._dataTextFieldList.Visible = true;
					this._dataTextFieldBox.Visible = false;
					this._dataNavFieldList.Items.Clear();
					ComboBox.ObjectCollection items2 = this._dataNavFieldList.Items;
					fieldSchemaNames = base.GetFieldSchemaNames();
					items2.AddRange(fieldSchemaNames);
					this._dataNavFieldList.Items.Insert(0, string.Empty);
					this._dataNavFieldList.SelectedIndex = 0;
					this._dataNavFieldList.Visible = true;
					this._dataNavFieldBox.Visible = false;
					return;
				}
				this._dataTextFieldList.Visible = false;
				this._dataTextFieldBox.Visible = true;
				this._dataNavFieldList.Visible = false;
				this._dataNavFieldBox.Visible = true;
			}

			// Token: 0x060026C9 RID: 9929 RVA: 0x000EEF84 File Offset: 0x000ED184
			protected override void RestoreFieldsInternal(IDictionary table)
			{
				string text = table["DataTextField"].ToString();
				string text2 = table["DataNavigateUrlField"].ToString();
				if (this._haveSchema)
				{
					bool flag = false;
					if (text.Length > 0)
					{
						foreach (object obj in this._dataTextFieldList.Items)
						{
							if (string.Compare(text, obj.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
							{
								this._dataTextFieldList.SelectedItem = obj;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this._dataTextFieldList.Items.Insert(0, text);
							this._dataTextFieldList.SelectedIndex = 0;
						}
					}
					if (text2.Length > 0)
					{
						flag = false;
						foreach (object obj2 in this._dataNavFieldList.Items)
						{
							if (string.Compare(text2, obj2.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
							{
								this._dataNavFieldList.SelectedItem = obj2;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this._dataNavFieldList.Items.Insert(0, text2);
							this._dataNavFieldList.SelectedIndex = 0;
						}
					}
				}
				else
				{
					this._dataTextFieldBox.Text = text;
					this._dataNavFieldBox.Text = text2;
				}
				this._dataNavFSBox.Text = table["DataNavigateUrlFormatString"].ToString();
				this._textFSBox.Text = table["DataTextFormatString"].ToString();
				this._linkBox.Text = table["NavigateUrl"].ToString();
				this._textBox.Text = table["Text"].ToString();
				this._staticUrlRadio.Checked = (bool)table["linkMode"];
				this._staticTextRadio.Checked = (bool)table["textMode"];
			}

			// Token: 0x060026CA RID: 9930 RVA: 0x000EF1AC File Offset: 0x000ED3AC
			protected override DataControlField SaveValues(string headerText)
			{
				HyperLinkField hyperLinkField = new HyperLinkField();
				hyperLinkField.HeaderText = headerText;
				if (this._staticTextRadio.Checked)
				{
					hyperLinkField.Text = this._textBox.Text;
				}
				else
				{
					hyperLinkField.DataTextFormatString = this._textFSBox.Text;
					if (this._haveSchema)
					{
						hyperLinkField.DataTextField = this._dataTextFieldList.Text;
					}
					else
					{
						hyperLinkField.DataTextField = this._dataTextFieldBox.Text;
					}
				}
				if (this._staticUrlRadio.Checked)
				{
					hyperLinkField.NavigateUrl = this._linkBox.Text;
				}
				else
				{
					hyperLinkField.DataNavigateUrlFormatString = this._dataNavFSBox.Text;
					if (this._haveSchema)
					{
						hyperLinkField.DataNavigateUrlFields = new string[]
						{
							this._dataNavFieldList.Text
						};
					}
					else
					{
						hyperLinkField.DataNavigateUrlFields = new string[]
						{
							this._dataNavFieldBox.Text
						};
					}
				}
				return hyperLinkField;
			}

			// Token: 0x04001BEC RID: 7148
			private System.Windows.Forms.TextBox _dataTextFieldBox;

			// Token: 0x04001BED RID: 7149
			private System.Windows.Forms.TextBox _dataNavFieldBox;

			// Token: 0x04001BEE RID: 7150
			private System.Windows.Forms.TextBox _dataNavFSBox;

			// Token: 0x04001BEF RID: 7151
			private System.Windows.Forms.TextBox _textBox;

			// Token: 0x04001BF0 RID: 7152
			private System.Windows.Forms.TextBox _textFSBox;

			// Token: 0x04001BF1 RID: 7153
			private System.Windows.Forms.TextBox _linkBox;

			// Token: 0x04001BF2 RID: 7154
			private ComboBox _dataTextFieldList;

			// Token: 0x04001BF3 RID: 7155
			private ComboBox _dataNavFieldList;

			// Token: 0x04001BF4 RID: 7156
			private System.Windows.Forms.RadioButton _staticTextRadio;

			// Token: 0x04001BF5 RID: 7157
			private System.Windows.Forms.RadioButton _bindTextRadio;

			// Token: 0x04001BF6 RID: 7158
			private System.Windows.Forms.RadioButton _staticUrlRadio;

			// Token: 0x04001BF7 RID: 7159
			private System.Windows.Forms.RadioButton _bindUrlRadio;

			// Token: 0x04001BF8 RID: 7160
			private System.Windows.Forms.Label _linkTextFormatStringLabel;

			// Token: 0x04001BF9 RID: 7161
			private System.Windows.Forms.Label _linkUrlFormatStringLabel;

			// Token: 0x04001BFA RID: 7162
			private System.Windows.Forms.Label _linkTextFormatStringExampleLabel;

			// Token: 0x04001BFB RID: 7163
			private System.Windows.Forms.Label _linkUrlFormatStringExampleLabel;

			// Token: 0x04001BFC RID: 7164
			private GroupBox _textGroupBox;

			// Token: 0x04001BFD RID: 7165
			private GroupBox _linkGroupBox;

			// Token: 0x04001BFE RID: 7166
			private System.Windows.Forms.Panel _staticTextPanel;

			// Token: 0x04001BFF RID: 7167
			private System.Windows.Forms.Panel _bindTextPanel;

			// Token: 0x04001C00 RID: 7168
			private System.Windows.Forms.Panel _staticUrlPanel;

			// Token: 0x04001C01 RID: 7169
			private System.Windows.Forms.Panel _bindUrlPanel;
		}

		// Token: 0x020003C9 RID: 969
		private class CommandFieldControl : AddDataControlFieldDialog.DataControlFieldControl
		{
			// Token: 0x060026CB RID: 9931 RVA: 0x000ED8FD File Offset: 0x000EBAFD
			public CommandFieldControl(IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
			}

			// Token: 0x17000831 RID: 2097
			// (get) Token: 0x060026CC RID: 9932 RVA: 0x000EF293 File Offset: 0x000ED493
			public override string FieldName
			{
				get
				{
					return "CommandField";
				}
			}

			// Token: 0x060026CD RID: 9933 RVA: 0x000EF29C File Offset: 0x000ED49C
			protected override void InitializeComponent()
			{
				base.InitializeComponent();
				this._buttonTypeLabel = new System.Windows.Forms.Label();
				this._buttonTypeList = new ComboBox();
				this._commandButtonsLabel = new System.Windows.Forms.Label();
				this._deleteBox = new System.Windows.Forms.CheckBox();
				this._selectBox = new System.Windows.Forms.CheckBox();
				this._cancelBox = new System.Windows.Forms.CheckBox();
				this._updateBox = new System.Windows.Forms.CheckBox();
				this._insertBox = new System.Windows.Forms.CheckBox();
				this._buttonTypeLabel.Text = SR.GetString("DCFAdd_ButtonType");
				this._buttonTypeLabel.TextAlign = ContentAlignment.BottomLeft;
				this._buttonTypeLabel.SetBounds(0, 43, 270, 17);
				this._buttonTypeList.Items.Add(ButtonType.Link.ToString());
				this._buttonTypeList.Items.Add(ButtonType.Button.ToString());
				this._buttonTypeList.SelectedIndex = 0;
				this._buttonTypeList.DropDownStyle = ComboBoxStyle.DropDownList;
				this._buttonTypeList.TabIndex = 1;
				this._buttonTypeList.SetBounds(0, 62, 270, 20);
				this._commandButtonsLabel.Text = SR.GetString("DCFAdd_CommandButtons");
				this._commandButtonsLabel.TextAlign = ContentAlignment.BottomLeft;
				this._commandButtonsLabel.SetBounds(0, 86, 270, 17);
				this._deleteBox.Text = SR.GetString("DCFAdd_Delete");
				this._deleteBox.AccessibleDescription = SR.GetString("DCFAdd_DeleteDesc");
				this._deleteBox.TextAlign = ContentAlignment.TopLeft;
				this._deleteBox.CheckAlign = ContentAlignment.TopLeft;
				this._deleteBox.TabIndex = 2;
				this._deleteBox.SetBounds(8, 105, 125, 20);
				this._selectBox.Text = SR.GetString("DCFAdd_Select");
				this._selectBox.AccessibleDescription = SR.GetString("DCFAdd_SelectDesc");
				this._selectBox.TextAlign = ContentAlignment.TopLeft;
				this._selectBox.CheckAlign = ContentAlignment.TopLeft;
				this._selectBox.TabIndex = 4;
				this._selectBox.SetBounds(8, 125, 125, 20);
				this._cancelBox.Text = SR.GetString("DCFAdd_ShowCancel");
				this._cancelBox.AccessibleDescription = SR.GetString("DCFAdd_ShowCancelDesc");
				this._cancelBox.TextAlign = ContentAlignment.TopLeft;
				this._cancelBox.CheckAlign = ContentAlignment.TopLeft;
				this._cancelBox.Enabled = false;
				this._cancelBox.Checked = true;
				this._cancelBox.TabIndex = 6;
				this._cancelBox.SetBounds(8, 145, 270, 44);
				this._updateBox.Text = SR.GetString("DCFAdd_EditUpdate");
				this._updateBox.AccessibleDescription = SR.GetString("DCFAdd_EditUpdateDesc");
				this._updateBox.TextAlign = ContentAlignment.TopLeft;
				this._updateBox.CheckAlign = ContentAlignment.TopLeft;
				this._updateBox.TabIndex = 3;
				this._updateBox.CheckedChanged += this.OnCheckedChanged;
				this._updateBox.SetBounds(139, 105, 125, 20);
				this._insertBox.Text = SR.GetString("DCFAdd_NewInsert");
				this._insertBox.AccessibleDescription = SR.GetString("DCFAdd_NewInsertDesc");
				this._insertBox.TextAlign = ContentAlignment.TopLeft;
				this._insertBox.CheckAlign = ContentAlignment.TopLeft;
				this._insertBox.TabIndex = 5;
				this._insertBox.CheckedChanged += this.OnCheckedChanged;
				this._insertBox.SetBounds(8, 125, 125, 20);
				if (this._controlType == typeof(GridView))
				{
					this._insertBox.Visible = false;
				}
				else if (this._controlType == typeof(DetailsView))
				{
					this._selectBox.Visible = false;
				}
				base.Controls.AddRange(new Control[]
				{
					this._buttonTypeLabel,
					this._buttonTypeList,
					this._commandButtonsLabel,
					this._deleteBox,
					this._selectBox,
					this._cancelBox,
					this._updateBox,
					this._insertBox
				});
			}

			// Token: 0x060026CE RID: 9934 RVA: 0x000EF6C5 File Offset: 0x000ED8C5
			private void OnCheckedChanged(object sender, EventArgs e)
			{
				this._cancelBox.Enabled = (this._updateBox.Checked || this._insertBox.Checked);
			}

			// Token: 0x060026CF RID: 9935 RVA: 0x000EF6F0 File Offset: 0x000ED8F0
			protected override void PreserveFields(IDictionary table)
			{
				table["ButtonType"] = this._buttonTypeList.SelectedIndex;
				table["ShowDeleteButton"] = this._deleteBox.Checked;
				table["ShowSelectButton"] = this._selectBox.Checked;
				table["ShowCancelButton"] = this._cancelBox.Checked;
				table["ShowEditButton"] = this._updateBox.Checked;
				table["ShowInsertButton"] = this._insertBox.Checked;
			}

			// Token: 0x060026D0 RID: 9936 RVA: 0x000EF7A0 File Offset: 0x000ED9A0
			protected override void RestoreFieldsInternal(IDictionary table)
			{
				this._buttonTypeList.SelectedIndex = (int)table["ButtonType"];
				this._deleteBox.Checked = (bool)table["ShowDeleteButton"];
				this._selectBox.Checked = (bool)table["ShowSelectButton"];
				this._cancelBox.Checked = (bool)table["ShowCancelButton"];
				this._updateBox.Checked = (bool)table["ShowEditButton"];
				this._insertBox.Checked = (bool)table["ShowInsertButton"];
			}

			// Token: 0x060026D1 RID: 9937 RVA: 0x000EF850 File Offset: 0x000EDA50
			protected override DataControlField SaveValues(string headerText)
			{
				CommandField commandField = new CommandField();
				if (headerText != null && headerText.Length > 0)
				{
					commandField.HeaderText = headerText;
					commandField.ShowHeader = true;
				}
				if (this._buttonTypeList.SelectedIndex == 0)
				{
					commandField.ButtonType = ButtonType.Link;
				}
				else
				{
					commandField.ButtonType = ButtonType.Button;
				}
				commandField.ShowDeleteButton = this._deleteBox.Checked;
				commandField.ShowSelectButton = this._selectBox.Checked;
				if (this._cancelBox.Enabled)
				{
					commandField.ShowCancelButton = this._cancelBox.Checked;
				}
				commandField.ShowEditButton = this._updateBox.Checked;
				commandField.ShowInsertButton = this._insertBox.Checked;
				return commandField;
			}

			// Token: 0x04001C02 RID: 7170
			private System.Windows.Forms.Label _buttonTypeLabel;

			// Token: 0x04001C03 RID: 7171
			private System.Windows.Forms.Label _commandButtonsLabel;

			// Token: 0x04001C04 RID: 7172
			private ComboBox _buttonTypeList;

			// Token: 0x04001C05 RID: 7173
			private System.Windows.Forms.CheckBox _deleteBox;

			// Token: 0x04001C06 RID: 7174
			private System.Windows.Forms.CheckBox _selectBox;

			// Token: 0x04001C07 RID: 7175
			private System.Windows.Forms.CheckBox _cancelBox;

			// Token: 0x04001C08 RID: 7176
			private System.Windows.Forms.CheckBox _updateBox;

			// Token: 0x04001C09 RID: 7177
			private System.Windows.Forms.CheckBox _insertBox;

			// Token: 0x04001C0A RID: 7178
			private const int checkBoxLeft = 8;
		}

		// Token: 0x020003CA RID: 970
		private class TemplateFieldControl : AddDataControlFieldDialog.DataControlFieldControl
		{
			// Token: 0x060026D2 RID: 9938 RVA: 0x000ED8FD File Offset: 0x000EBAFD
			public TemplateFieldControl(IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
			}

			// Token: 0x17000832 RID: 2098
			// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000EF8FD File Offset: 0x000EDAFD
			public override string FieldName
			{
				get
				{
					return "TemplateField";
				}
			}

			// Token: 0x060026D4 RID: 9940 RVA: 0x000EF904 File Offset: 0x000EDB04
			protected override void InitializeComponent()
			{
				base.InitializeComponent();
			}

			// Token: 0x060026D5 RID: 9941 RVA: 0x00003937 File Offset: 0x00001B37
			protected override void PreserveFields(IDictionary table)
			{
			}

			// Token: 0x060026D6 RID: 9942 RVA: 0x00003937 File Offset: 0x00001B37
			protected override void RestoreFieldsInternal(IDictionary table)
			{
			}

			// Token: 0x060026D7 RID: 9943 RVA: 0x000EF90C File Offset: 0x000EDB0C
			protected override DataControlField SaveValues(string headerText)
			{
				return new TemplateField
				{
					HeaderText = headerText
				};
			}
		}

		// Token: 0x020003CB RID: 971
		private class ImageFieldControl : AddDataControlFieldDialog.DataControlFieldControl
		{
			// Token: 0x060026D8 RID: 9944 RVA: 0x000ED8FD File Offset: 0x000EBAFD
			public ImageFieldControl(IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
			}

			// Token: 0x17000833 RID: 2099
			// (get) Token: 0x060026D9 RID: 9945 RVA: 0x000EF927 File Offset: 0x000EDB27
			public override string FieldName
			{
				get
				{
					return "ImageField";
				}
			}

			// Token: 0x060026DA RID: 9946 RVA: 0x000EF930 File Offset: 0x000EDB30
			protected override void InitializeComponent()
			{
				base.InitializeComponent();
				this._imageUrlFieldList = new ComboBox();
				this._imageUrlFieldBox = new System.Windows.Forms.TextBox();
				this._imageUrlFieldLabel = new System.Windows.Forms.Label();
				this._readOnlyCheckBox = new System.Windows.Forms.CheckBox();
				this._urlFormatBox = new System.Windows.Forms.TextBox();
				this._urlFormatBoxLabel = new System.Windows.Forms.Label();
				this._urlFormatExampleLabel = new System.Windows.Forms.Label();
				this._imageUrlFieldLabel.Text = SR.GetString("DCFAdd_DataField");
				this._imageUrlFieldLabel.TextAlign = ContentAlignment.BottomLeft;
				this._imageUrlFieldLabel.SetBounds(0, 43, 270, 17);
				this._imageUrlFieldList.DropDownStyle = ComboBoxStyle.DropDownList;
				this._imageUrlFieldList.TabIndex = 1;
				this._imageUrlFieldList.SetBounds(0, 62, 270, 20);
				this._imageUrlFieldBox.TabIndex = 2;
				this._imageUrlFieldBox.SetBounds(0, 62, 270, 20);
				this._urlFormatBoxLabel.TabIndex = 3;
				this._urlFormatBoxLabel.Text = SR.GetString("DCFAdd_LinkFormatString");
				this._urlFormatBoxLabel.TextAlign = ContentAlignment.BottomLeft;
				this._urlFormatBoxLabel.SetBounds(0, 86, 270, 17);
				this._urlFormatBox.TabIndex = 4;
				this._urlFormatBox.SetBounds(0, 105, 270, 20);
				this._urlFormatExampleLabel.Enabled = false;
				this._urlFormatExampleLabel.Text = SR.GetString("DCFAdd_ExampleFormatString");
				this._urlFormatExampleLabel.TextAlign = ContentAlignment.BottomLeft;
				this._urlFormatExampleLabel.SetBounds(0, 125, 270, 17);
				this._readOnlyCheckBox.TabIndex = 5;
				this._readOnlyCheckBox.Text = SR.GetString("DCFAdd_ReadOnly");
				this._readOnlyCheckBox.SetBounds(0, 144, 270, 20);
				if (this._haveSchema)
				{
					ComboBox.ObjectCollection items = this._imageUrlFieldList.Items;
					object[] fieldSchemaNames = base.GetFieldSchemaNames();
					items.AddRange(fieldSchemaNames);
					this._imageUrlFieldList.SelectedIndex = 0;
					this._imageUrlFieldList.Visible = true;
					this._imageUrlFieldBox.Visible = false;
				}
				else
				{
					this._imageUrlFieldList.Visible = false;
					this._imageUrlFieldBox.Visible = true;
				}
				base.Controls.AddRange(new Control[]
				{
					this._imageUrlFieldLabel,
					this._imageUrlFieldBox,
					this._imageUrlFieldList,
					this._readOnlyCheckBox,
					this._urlFormatBoxLabel,
					this._urlFormatBox,
					this._urlFormatExampleLabel
				});
			}

			// Token: 0x060026DB RID: 9947 RVA: 0x000EFBAC File Offset: 0x000EDDAC
			protected override void PreserveFields(IDictionary table)
			{
				if (this._haveSchema)
				{
					table["ImageUrlField"] = this._imageUrlFieldList.Text;
				}
				else
				{
					table["ImageUrlField"] = this._imageUrlFieldBox.Text;
				}
				table["ReadOnly"] = this._readOnlyCheckBox.Checked;
				table["FormatString"] = this._urlFormatBox.Text;
			}

			// Token: 0x060026DC RID: 9948 RVA: 0x000EFC20 File Offset: 0x000EDE20
			protected override void RefreshSchemaFields()
			{
				if (this._haveSchema)
				{
					this._imageUrlFieldList.Items.Clear();
					ComboBox.ObjectCollection items = this._imageUrlFieldList.Items;
					object[] fieldSchemaNames = base.GetFieldSchemaNames();
					items.AddRange(fieldSchemaNames);
					this._imageUrlFieldList.SelectedIndex = 0;
					this._imageUrlFieldList.Visible = true;
					this._imageUrlFieldBox.Visible = false;
					return;
				}
				this._imageUrlFieldList.Visible = false;
				this._imageUrlFieldBox.Visible = true;
			}

			// Token: 0x060026DD RID: 9949 RVA: 0x000EFC9C File Offset: 0x000EDE9C
			protected override void RestoreFieldsInternal(IDictionary table)
			{
				string text = table["ImageUrlField"].ToString();
				if (this._haveSchema)
				{
					if (text.Length > 0)
					{
						bool flag = false;
						foreach (object obj in this._imageUrlFieldList.Items)
						{
							if (string.Compare(text, obj.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
							{
								this._imageUrlFieldList.SelectedItem = obj;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this._imageUrlFieldList.Items.Insert(0, text);
							this._imageUrlFieldList.SelectedIndex = 0;
						}
					}
				}
				else
				{
					this._imageUrlFieldBox.Text = text;
				}
				this._readOnlyCheckBox.Checked = (bool)table["ReadOnly"];
				this._urlFormatBox.Text = (string)table["FormatString"];
			}

			// Token: 0x060026DE RID: 9950 RVA: 0x000EFDA0 File Offset: 0x000EDFA0
			protected override DataControlField SaveValues(string headerText)
			{
				ImageField imageField = new ImageField();
				imageField.HeaderText = headerText;
				if (this._haveSchema)
				{
					imageField.DataImageUrlField = this._imageUrlFieldList.Text;
				}
				else
				{
					imageField.DataImageUrlField = this._imageUrlFieldBox.Text;
				}
				imageField.ReadOnly = this._readOnlyCheckBox.Checked;
				imageField.DataImageUrlFormatString = this._urlFormatBox.Text;
				return imageField;
			}

			// Token: 0x04001C0B RID: 7179
			private System.Windows.Forms.Label _imageUrlFieldLabel;

			// Token: 0x04001C0C RID: 7180
			private ComboBox _imageUrlFieldList;

			// Token: 0x04001C0D RID: 7181
			private System.Windows.Forms.TextBox _imageUrlFieldBox;

			// Token: 0x04001C0E RID: 7182
			private System.Windows.Forms.CheckBox _readOnlyCheckBox;

			// Token: 0x04001C0F RID: 7183
			private System.Windows.Forms.TextBox _urlFormatBox;

			// Token: 0x04001C10 RID: 7184
			private System.Windows.Forms.Label _urlFormatBoxLabel;

			// Token: 0x04001C11 RID: 7185
			private System.Windows.Forms.Label _urlFormatExampleLabel;
		}

		// Token: 0x020003CC RID: 972
		private class DataControlFieldDesignerControl : AddDataControlFieldDialog.DataControlFieldControl
		{
			// Token: 0x060026DF RID: 9951 RVA: 0x000EFE09 File Offset: 0x000EE009
			public DataControlFieldDesignerControl(DataBoundControlDesigner controlDesigner, IServiceProvider serviceProvider, DataControlFieldDesigner designer, IDataSourceFieldSchema[] fieldSchemas, Type controlType) : base(fieldSchemas, controlType)
			{
				this._controlDesigner = controlDesigner;
				this._serviceProvider = serviceProvider;
				this._designer = designer;
				this.Initialize();
			}

			// Token: 0x17000834 RID: 2100
			// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000EFE30 File Offset: 0x000EE030
			public override string FieldName
			{
				get
				{
					return this._designer.DefaultNodeText;
				}
			}

			// Token: 0x17000835 RID: 2101
			// (get) Token: 0x060026E1 RID: 9953 RVA: 0x000EFE3D File Offset: 0x000EE03D
			public DataControlFieldDesigner Designer
			{
				get
				{
					return this._designer;
				}
			}

			// Token: 0x060026E2 RID: 9954 RVA: 0x00003937 File Offset: 0x00001B37
			protected override void InitializeComponent()
			{
			}

			// Token: 0x060026E3 RID: 9955 RVA: 0x000EFE48 File Offset: 0x000EE048
			private void Initialize()
			{
				this._field = this._designer.CreateField();
				this._fieldProps = new VsPropertyGrid(this._serviceProvider);
				this._fieldProps.SelectedObject = this._field;
				this._fieldProps.CommandsVisibleIfAvailable = true;
				this._fieldProps.LargeButtons = false;
				this._fieldProps.LineColor = SystemColors.ScrollBar;
				this._fieldProps.Name = "_fieldProps";
				this._fieldProps.Size = new Size(248, 281);
				this._fieldProps.ToolbarVisible = true;
				this._fieldProps.ViewBackColor = SystemColors.Window;
				this._fieldProps.ViewForeColor = SystemColors.WindowText;
				this._fieldProps.Site = this._controlDesigner.Component.Site;
				base.Controls.Add(this._fieldProps);
			}

			// Token: 0x060026E4 RID: 9956 RVA: 0x00003937 File Offset: 0x00001B37
			protected override void PreserveFields(IDictionary table)
			{
			}

			// Token: 0x060026E5 RID: 9957 RVA: 0x00003937 File Offset: 0x00001B37
			protected override void RefreshSchemaFields()
			{
			}

			// Token: 0x060026E6 RID: 9958 RVA: 0x00003937 File Offset: 0x00001B37
			protected override void RestoreFieldsInternal(IDictionary table)
			{
			}

			// Token: 0x060026E7 RID: 9959 RVA: 0x000EFF32 File Offset: 0x000EE132
			protected override DataControlField SaveValues(string headerText)
			{
				this._fieldProps.Refresh();
				return this._field;
			}

			// Token: 0x04001C12 RID: 7186
			private DataBoundControlDesigner _controlDesigner;

			// Token: 0x04001C13 RID: 7187
			private DataControlFieldDesigner _designer;

			// Token: 0x04001C14 RID: 7188
			private DataControlField _field;

			// Token: 0x04001C15 RID: 7189
			private PropertyGrid _fieldProps;

			// Token: 0x04001C16 RID: 7190
			private IServiceProvider _serviceProvider;
		}
	}
}
