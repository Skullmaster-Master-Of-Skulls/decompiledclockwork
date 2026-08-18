using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001DC RID: 476
	internal partial class DataGridViewAddColumnDialog : Form
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x0005B034 File Offset: 0x0005A034
		public DataGridViewAddColumnDialog(DataGridViewColumnCollection dataGridViewColumns, DataGridView liveDataGridView)
		{
			this.dataGridViewColumns = dataGridViewColumns;
			this.liveDataGridView = liveDataGridView;
			Font font = Control.DefaultFont;
			IUIService iuiservice = (IUIService)this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iUIServiceType);
			if (iuiservice != null)
			{
				font = (Font)iuiservice.Styles["DialogFont"];
			}
			this.Font = font;
			this.InitializeComponent();
			this.EnableDataBoundSection();
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0005B0B0 File Offset: 0x0005A0B0
		private void AddColumn()
		{
			Type columnType = ((DataGridViewAddColumnDialog.ComboBoxItem)this.columnTypesCombo.SelectedItem).ColumnType;
			DataGridViewColumn dataGridViewColumn = Activator.CreateInstance(columnType) as DataGridViewColumn;
			bool flag = this.dataGridViewColumns.Count > this.insertAtPosition && this.dataGridViewColumns[this.insertAtPosition].Frozen;
			dataGridViewColumn.Frozen = flag;
			if (!this.persistChangesToDesigner)
			{
				dataGridViewColumn.HeaderText = this.headerTextBox.Text;
				dataGridViewColumn.Name = this.nameTextBox.Text;
				dataGridViewColumn.DisplayIndex = -1;
				this.dataGridViewColumns.Insert(this.insertAtPosition, dataGridViewColumn);
				this.insertAtPosition++;
			}
			dataGridViewColumn.HeaderText = this.headerTextBox.Text;
			dataGridViewColumn.Name = this.nameTextBox.Text;
			dataGridViewColumn.Visible = this.visibleCheckBox.Checked;
			dataGridViewColumn.Frozen = (this.frozenCheckBox.Checked || flag);
			dataGridViewColumn.ReadOnly = this.readOnlyCheckBox.Checked;
			if (this.dataBoundColumnRadioButton.Checked && this.dataColumns.SelectedIndex > -1)
			{
				dataGridViewColumn.DataPropertyName = ((DataGridViewAddColumnDialog.ListBoxItem)this.dataColumns.SelectedItem).PropertyName;
			}
			if (this.persistChangesToDesigner)
			{
				try
				{
					dataGridViewColumn.DisplayIndex = -1;
					this.dataGridViewColumns.Insert(this.insertAtPosition, dataGridViewColumn);
					this.insertAtPosition++;
					this.liveDataGridView.Site.Container.Add(dataGridViewColumn, dataGridViewColumn.Name);
				}
				catch (InvalidOperationException ex)
				{
					IUIService uiService = (IUIService)this.liveDataGridView.Site.GetService(typeof(IUIService));
					DataGridViewDesigner.ShowErrorDialog(uiService, ex, this.liveDataGridView);
					return;
				}
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataGridViewColumn);
			PropertyDescriptor propertyDescriptor = properties["UserAddedColumn"];
			if (propertyDescriptor != null)
			{
				propertyDescriptor.SetValue(dataGridViewColumn, true);
			}
			this.nameTextBox.Text = (this.headerTextBox.Text = this.AssignName());
			this.nameTextBox.Focus();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0005B2DC File Offset: 0x0005A2DC
		private string AssignName()
		{
			int num = 1;
			string text = "Column" + num.ToString(CultureInfo.InvariantCulture);
			IContainer container = null;
			IDesignerHost designerHost = this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iDesignerHostType) as IDesignerHost;
			if (designerHost != null)
			{
				container = designerHost.Container;
			}
			while (!DataGridViewAddColumnDialog.ValidName(text, this.dataGridViewColumns, container, null, this.liveDataGridView.Columns, !this.persistChangesToDesigner))
			{
				num++;
				text = "Column" + num.ToString(CultureInfo.InvariantCulture);
			}
			return text;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0005B38C File Offset: 0x0005A38C
		private void EnableDataBoundSection()
		{
			bool flag = this.dataColumns.Items.Count > 0;
			if (flag)
			{
				this.dataBoundColumnRadioButton.Enabled = true;
				this.dataBoundColumnRadioButton.Checked = true;
				this.dataBoundColumnRadioButton.Focus();
				this.headerTextBox.Text = (this.nameTextBox.Text = this.AssignName());
				return;
			}
			this.dataBoundColumnRadioButton.Enabled = false;
			this.unboundColumnRadioButton.Checked = true;
			this.nameTextBox.Focus();
			this.headerTextBox.Text = (this.nameTextBox.Text = this.AssignName());
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0005B438 File Offset: 0x0005A438
		public static ComponentDesigner GetComponentDesignerForType(ITypeResolutionService tr, Type type)
		{
			ComponentDesigner result = null;
			DesignerAttribute designerAttribute = null;
			AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
			for (int i = 0; i < attributes.Count; i++)
			{
				DesignerAttribute designerAttribute2 = attributes[i] as DesignerAttribute;
				if (designerAttribute2 != null)
				{
					Type type2 = Type.GetType(designerAttribute2.DesignerBaseTypeName);
					if (type2 != null && type2 == DataGridViewAddColumnDialog.iDesignerType)
					{
						designerAttribute = designerAttribute2;
						break;
					}
				}
			}
			if (designerAttribute != null)
			{
				Type type3;
				if (tr != null)
				{
					type3 = tr.GetType(designerAttribute.DesignerTypeName);
				}
				else
				{
					type3 = Type.GetType(designerAttribute.DesignerTypeName);
				}
				if (type3 != null && typeof(ComponentDesigner).IsAssignableFrom(type3))
				{
					result = (ComponentDesigner)Activator.CreateInstance(type3);
				}
			}
			return result;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0005BF89 File Offset: 0x0005AF89
		private void dataBoundColumnRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.columnInDataSourceLabel.Enabled = this.dataBoundColumnRadioButton.Checked;
			this.dataColumns.Enabled = this.dataBoundColumnRadioButton.Checked;
			this.dataColumns_SelectedIndexChanged(null, EventArgs.Empty);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0005BFC4 File Offset: 0x0005AFC4
		private void dataColumns_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dataColumns.SelectedItem == null)
			{
				return;
			}
			this.headerTextBox.Text = (this.nameTextBox.Text = ((DataGridViewAddColumnDialog.ListBoxItem)this.dataColumns.SelectedItem).PropertyName);
			this.SetDefaultDataGridViewColumnType(((DataGridViewAddColumnDialog.ListBoxItem)this.dataColumns.SelectedItem).PropertyType);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0005C028 File Offset: 0x0005B028
		private void unboundColumnRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.unboundColumnRadioButton.Checked)
			{
				this.nameTextBox.Text = (this.headerTextBox.Text = this.AssignName());
				this.nameTextBox.Focus();
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0005C070 File Offset: 0x0005B070
		private void DataGridViewAddColumnDialog_Closed(object sender, EventArgs e)
		{
			if (this.persistChangesToDesigner)
			{
				try
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iComponentChangeServiceType);
					if (componentChangeService == null)
					{
						return;
					}
					DataGridViewColumn[] array = new DataGridViewColumn[this.liveDataGridView.Columns.Count - this.initialDataGridViewColumnsCount];
					for (int i = this.initialDataGridViewColumnsCount; i < this.liveDataGridView.Columns.Count; i++)
					{
						array[i - this.initialDataGridViewColumnsCount] = this.liveDataGridView.Columns[i];
					}
					int j = this.initialDataGridViewColumnsCount;
					while (j < this.liveDataGridView.Columns.Count)
					{
						this.liveDataGridView.Columns.RemoveAt(this.initialDataGridViewColumnsCount);
					}
					PropertyDescriptor member = TypeDescriptor.GetProperties(this.liveDataGridView)["Columns"];
					componentChangeService.OnComponentChanging(this.liveDataGridView, member);
					for (int k = 0; k < array.Length; k++)
					{
						array[k].DisplayIndex = -1;
					}
					this.liveDataGridView.Columns.AddRange(array);
					componentChangeService.OnComponentChanged(this.liveDataGridView, member, null, null);
				}
				catch (InvalidOperationException)
				{
				}
			}
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0005C1C0 File Offset: 0x0005B1C0
		private void DataGridViewAddColumnDialog_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			this.DataGridViewAddColumnDialog_HelpRequestHandled();
			e.Cancel = true;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0005C1CF File Offset: 0x0005B1CF
		private void DataGridViewAddColumnDialog_HelpRequested(object sender, HelpEventArgs e)
		{
			this.DataGridViewAddColumnDialog_HelpRequestHandled();
			e.Handled = true;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0005C1E0 File Offset: 0x0005B1E0
		private void DataGridViewAddColumnDialog_HelpRequestHandled()
		{
			IHelpService helpService = this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iHelpServiceType) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("vs.DataGridViewAddColumnDialog");
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0005C218 File Offset: 0x0005B218
		private void DataGridViewAddColumnDialog_Load(object sender, EventArgs e)
		{
			if (this.dataBoundColumnRadioButton.Checked)
			{
				this.headerTextBox.Text = (this.nameTextBox.Text = this.AssignName());
			}
			else
			{
				string text = this.AssignName();
				this.headerTextBox.Text = (this.nameTextBox.Text = text);
			}
			this.PopulateColumnTypesCombo();
			this.PopulateDataColumns();
			this.EnableDataBoundSection();
			this.cancelButton.Text = SR.GetString("DataGridView_Cancel");
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0005C29B File Offset: 0x0005B29B
		private void DataGridViewAddColumnDialog_VisibleChanged(object sender, EventArgs e)
		{
			if (base.Visible && base.IsHandleCreated)
			{
				if (this.dataBoundColumnRadioButton.Checked)
				{
					this.dataColumns.Select();
					return;
				}
				this.nameTextBox.Select();
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0005C2D4 File Offset: 0x0005B2D4
		private void nameTextBox_Validating(object sender, CancelEventArgs e)
		{
			IContainer container = null;
			IDesignerHost designerHost = this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iDesignerHostType) as IDesignerHost;
			if (designerHost != null)
			{
				container = designerHost.Container;
			}
			INameCreationService nameCreationService = this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iNameCreationServiceType) as INameCreationService;
			string empty = string.Empty;
			if (!DataGridViewAddColumnDialog.ValidName(this.nameTextBox.Text, this.dataGridViewColumns, container, nameCreationService, this.liveDataGridView.Columns, !this.persistChangesToDesigner, out empty))
			{
				IUIService uiService = (IUIService)this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iUIServiceType);
				DataGridViewDesigner.ShowErrorDialog(uiService, empty, this.liveDataGridView);
				e.Cancel = true;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0005C390 File Offset: 0x0005B390
		private void PopulateColumnTypesCombo()
		{
			this.columnTypesCombo.Items.Clear();
			IDesignerHost designerHost = (IDesignerHost)this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iDesignerHostType);
			if (designerHost == null)
			{
				return;
			}
			ITypeDiscoveryService typeDiscoveryService = (ITypeDiscoveryService)designerHost.GetService(DataGridViewAddColumnDialog.iTypeDiscoveryServiceType);
			if (typeDiscoveryService == null)
			{
				return;
			}
			ICollection collection = DesignerUtils.FilterGenericTypes(typeDiscoveryService.GetTypes(DataGridViewAddColumnDialog.dataGridViewColumnType, false));
			foreach (object obj in collection)
			{
				Type type = (Type)obj;
				if (type != DataGridViewAddColumnDialog.dataGridViewColumnType && !type.IsAbstract && (type.IsPublic || type.IsNestedPublic))
				{
					DataGridViewColumnDesignTimeVisibleAttribute dataGridViewColumnDesignTimeVisibleAttribute = TypeDescriptor.GetAttributes(type)[DataGridViewAddColumnDialog.dataGridViewColumnDesignTimeVisibleAttributeType] as DataGridViewColumnDesignTimeVisibleAttribute;
					if (dataGridViewColumnDesignTimeVisibleAttribute == null || dataGridViewColumnDesignTimeVisibleAttribute.Visible)
					{
						this.columnTypesCombo.Items.Add(new DataGridViewAddColumnDialog.ComboBoxItem(type));
					}
				}
			}
			this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(DataGridViewTextBoxColumn));
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0005C4B0 File Offset: 0x0005B4B0
		private void PopulateDataColumns()
		{
			int selectedIndex = this.dataColumns.SelectedIndex;
			this.dataColumns.SelectedIndex = -1;
			this.dataColumns.Items.Clear();
			if (this.liveDataGridView.DataSource != null)
			{
				CurrencyManager currencyManager = null;
				try
				{
					currencyManager = (this.BindingContext[this.liveDataGridView.DataSource, this.liveDataGridView.DataMember] as CurrencyManager);
				}
				catch (ArgumentException)
				{
					currencyManager = null;
				}
				PropertyDescriptorCollection propertyDescriptorCollection = (currencyManager != null) ? currencyManager.GetItemProperties() : null;
				if (propertyDescriptorCollection != null)
				{
					int i = 0;
					while (i < propertyDescriptorCollection.Count)
					{
						if (!typeof(IList).IsAssignableFrom(propertyDescriptorCollection[i].PropertyType))
						{
							goto IL_C2;
						}
						TypeConverter converter = TypeDescriptor.GetConverter(typeof(Image));
						if (converter.CanConvertFrom(propertyDescriptorCollection[i].PropertyType))
						{
							goto IL_C2;
						}
						IL_F0:
						i++;
						continue;
						IL_C2:
						this.dataColumns.Items.Add(new DataGridViewAddColumnDialog.ListBoxItem(propertyDescriptorCollection[i].PropertyType, propertyDescriptorCollection[i].Name));
						goto IL_F0;
					}
				}
			}
			if (selectedIndex != -1 && selectedIndex < this.dataColumns.Items.Count)
			{
				this.dataColumns.SelectedIndex = selectedIndex;
				return;
			}
			this.dataColumns.SelectedIndex = ((this.dataColumns.Items.Count > 0) ? 0 : -1);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0005C610 File Offset: 0x0005B610
		private void addButton_Click(object sender, EventArgs e)
		{
			this.cancelButton.Text = SR.GetString("DataGridView_Close");
			this.AddColumn();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0005C62D File Offset: 0x0005B62D
		private void cancelButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0005C638 File Offset: 0x0005B638
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & Keys.Modifiers) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys == Keys.Return)
				{
					IContainer container = null;
					IDesignerHost designerHost = this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iDesignerHostType) as IDesignerHost;
					if (designerHost != null)
					{
						container = designerHost.Container;
					}
					INameCreationService nameCreationService = this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iNameCreationServiceType) as INameCreationService;
					string empty = string.Empty;
					if (DataGridViewAddColumnDialog.ValidName(this.nameTextBox.Text, this.dataGridViewColumns, container, nameCreationService, this.liveDataGridView.Columns, !this.persistChangesToDesigner, out empty))
					{
						this.AddColumn();
						base.Close();
					}
					else
					{
						IUIService uiService = (IUIService)this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iUIServiceType);
						DataGridViewDesigner.ShowErrorDialog(uiService, empty, this.liveDataGridView);
					}
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0005C720 File Offset: 0x0005B720
		internal void Start(int insertAtPosition, bool persistChangesToDesigner)
		{
			this.insertAtPosition = insertAtPosition;
			this.persistChangesToDesigner = persistChangesToDesigner;
			if (this.persistChangesToDesigner)
			{
				this.initialDataGridViewColumnsCount = this.liveDataGridView.Columns.Count;
				return;
			}
			this.initialDataGridViewColumnsCount = -1;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0005C758 File Offset: 0x0005B758
		private void SetDefaultDataGridViewColumnType(Type type)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Image));
			if (type == typeof(bool) || type == typeof(CheckState))
			{
				this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(DataGridViewCheckBoxColumn));
				return;
			}
			if (typeof(Image).IsAssignableFrom(type) || converter.CanConvertFrom(type))
			{
				this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(DataGridViewImageColumn));
				return;
			}
			this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(DataGridViewTextBoxColumn));
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0005C800 File Offset: 0x0005B800
		private int TypeToSelectedIndex(Type type)
		{
			for (int i = 0; i < this.columnTypesCombo.Items.Count; i++)
			{
				if (type == ((DataGridViewAddColumnDialog.ComboBoxItem)this.columnTypesCombo.Items[i]).ColumnType)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0005C84C File Offset: 0x0005B84C
		public static bool ValidName(string name, DataGridViewColumnCollection columns, IContainer container, INameCreationService nameCreationService, DataGridViewColumnCollection liveColumns, bool allowDuplicateNameInLiveColumnCollection)
		{
			return !columns.Contains(name) && (container == null || container.Components[name] == null || (allowDuplicateNameInLiveColumnCollection && liveColumns != null && liveColumns.Contains(name))) && (nameCreationService == null || nameCreationService.IsValidName(name) || (allowDuplicateNameInLiveColumnCollection && liveColumns != null && liveColumns.Contains(name)));
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0005C8AC File Offset: 0x0005B8AC
		public static bool ValidName(string name, DataGridViewColumnCollection columns, IContainer container, INameCreationService nameCreationService, DataGridViewColumnCollection liveColumns, bool allowDuplicateNameInLiveColumnCollection, out string errorString)
		{
			if (columns.Contains(name))
			{
				errorString = SR.GetString("DataGridViewDuplicateColumnName", new object[]
				{
					name
				});
				return false;
			}
			if (container != null && container.Components[name] != null && (!allowDuplicateNameInLiveColumnCollection || liveColumns == null || !liveColumns.Contains(name)))
			{
				errorString = SR.GetString("DesignerHostDuplicateName", new object[]
				{
					name
				});
				return false;
			}
			if (nameCreationService != null && !nameCreationService.IsValidName(name) && (!allowDuplicateNameInLiveColumnCollection || liveColumns == null || !liveColumns.Contains(name)))
			{
				errorString = SR.GetString("CodeDomDesignerLoaderInvalidIdentifier", new object[]
				{
					name
				});
				return false;
			}
			errorString = string.Empty;
			return true;
		}

		// Token: 0x04001123 RID: 4387
		private DataGridViewColumnCollection dataGridViewColumns;

		// Token: 0x04001124 RID: 4388
		private DataGridView liveDataGridView;

		// Token: 0x04001125 RID: 4389
		private int insertAtPosition = -1;

		// Token: 0x04001126 RID: 4390
		private int initialDataGridViewColumnsCount = -1;

		// Token: 0x04001127 RID: 4391
		private bool persistChangesToDesigner;

		// Token: 0x04001128 RID: 4392
		private static Type dataGridViewColumnType = typeof(DataGridViewColumn);

		// Token: 0x04001129 RID: 4393
		private static Type iDesignerType = typeof(IDesigner);

		// Token: 0x0400112A RID: 4394
		private static Type iTypeResolutionServiceType = typeof(ITypeResolutionService);

		// Token: 0x0400112B RID: 4395
		private static Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);

		// Token: 0x0400112C RID: 4396
		private static Type iComponentChangeServiceType = typeof(IComponentChangeService);

		// Token: 0x0400112D RID: 4397
		private static Type iHelpServiceType = typeof(IHelpService);

		// Token: 0x0400112E RID: 4398
		private static Type iUIServiceType = typeof(IUIService);

		// Token: 0x0400112F RID: 4399
		private static Type iDesignerHostType = typeof(IDesignerHost);

		// Token: 0x04001130 RID: 4400
		private static Type iNameCreationServiceType = typeof(INameCreationService);

		// Token: 0x04001131 RID: 4401
		private static Type dataGridViewColumnDesignTimeVisibleAttributeType = typeof(DataGridViewColumnDesignTimeVisibleAttribute);

		// Token: 0x04001132 RID: 4402
		private static Type[] columnTypes = new Type[]
		{
			typeof(DataGridViewButtonColumn),
			typeof(DataGridViewCheckBoxColumn),
			typeof(DataGridViewComboBoxColumn),
			typeof(DataGridViewImageColumn),
			typeof(DataGridViewLinkColumn),
			typeof(DataGridViewTextBoxColumn)
		};

		// Token: 0x020001DD RID: 477
		private class ListBoxItem
		{
			// Token: 0x06001269 RID: 4713 RVA: 0x0005CA5E File Offset: 0x0005BA5E
			public ListBoxItem(Type propertyType, string propertyName)
			{
				this.propertyType = propertyType;
				this.propertyName = propertyName;
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x0600126A RID: 4714 RVA: 0x0005CA74 File Offset: 0x0005BA74
			public Type PropertyType
			{
				get
				{
					return this.propertyType;
				}
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x0600126B RID: 4715 RVA: 0x0005CA7C File Offset: 0x0005BA7C
			public string PropertyName
			{
				get
				{
					return this.propertyName;
				}
			}

			// Token: 0x0600126C RID: 4716 RVA: 0x0005CA84 File Offset: 0x0005BA84
			public override string ToString()
			{
				return this.propertyName;
			}

			// Token: 0x04001137 RID: 4407
			private Type propertyType;

			// Token: 0x04001138 RID: 4408
			private string propertyName;
		}

		// Token: 0x020001DE RID: 478
		private class ComboBoxItem
		{
			// Token: 0x0600126D RID: 4717 RVA: 0x0005CA8C File Offset: 0x0005BA8C
			public ComboBoxItem(Type columnType)
			{
				this.columnType = columnType;
			}

			// Token: 0x0600126E RID: 4718 RVA: 0x0005CA9B File Offset: 0x0005BA9B
			public override string ToString()
			{
				return this.columnType.Name;
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x0600126F RID: 4719 RVA: 0x0005CAA8 File Offset: 0x0005BAA8
			public Type ColumnType
			{
				get
				{
					return this.columnType;
				}
			}

			// Token: 0x04001139 RID: 4409
			private Type columnType;
		}
	}
}
