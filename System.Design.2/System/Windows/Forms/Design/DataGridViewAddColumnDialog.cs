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
	// Token: 0x020002B8 RID: 696
	internal partial class DataGridViewAddColumnDialog : Form
	{
		// Token: 0x06001B83 RID: 7043 RVA: 0x000A3754 File Offset: 0x000A1954
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

		// Token: 0x06001B84 RID: 7044 RVA: 0x000A37D0 File Offset: 0x000A19D0
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

		// Token: 0x06001B85 RID: 7045 RVA: 0x000A39F8 File Offset: 0x000A1BF8
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

		// Token: 0x06001B87 RID: 7047 RVA: 0x000A3AA8 File Offset: 0x000A1CA8
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

		// Token: 0x06001B88 RID: 7048 RVA: 0x000A3B54 File Offset: 0x000A1D54
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

		// Token: 0x06001B8A RID: 7050 RVA: 0x000A46D4 File Offset: 0x000A28D4
		private void dataBoundColumnRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.columnInDataSourceLabel.Enabled = this.dataBoundColumnRadioButton.Checked;
			this.dataColumns.Enabled = this.dataBoundColumnRadioButton.Checked;
			this.dataColumns_SelectedIndexChanged(null, EventArgs.Empty);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000A4710 File Offset: 0x000A2910
		private void dataColumns_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dataColumns.SelectedItem == null)
			{
				return;
			}
			this.headerTextBox.Text = (this.nameTextBox.Text = ((DataGridViewAddColumnDialog.ListBoxItem)this.dataColumns.SelectedItem).PropertyName);
			this.SetDefaultDataGridViewColumnType(((DataGridViewAddColumnDialog.ListBoxItem)this.dataColumns.SelectedItem).PropertyType);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x000A4774 File Offset: 0x000A2974
		private void unboundColumnRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.unboundColumnRadioButton.Checked)
			{
				this.nameTextBox.Text = (this.headerTextBox.Text = this.AssignName());
				this.nameTextBox.Focus();
			}
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x000A47BC File Offset: 0x000A29BC
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

		// Token: 0x06001B8E RID: 7054 RVA: 0x000A490C File Offset: 0x000A2B0C
		private void DataGridViewAddColumnDialog_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			this.DataGridViewAddColumnDialog_HelpRequestHandled();
			e.Cancel = true;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000A491B File Offset: 0x000A2B1B
		private void DataGridViewAddColumnDialog_HelpRequested(object sender, HelpEventArgs e)
		{
			this.DataGridViewAddColumnDialog_HelpRequestHandled();
			e.Handled = true;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x000A492C File Offset: 0x000A2B2C
		private void DataGridViewAddColumnDialog_HelpRequestHandled()
		{
			IHelpService helpService = this.liveDataGridView.Site.GetService(DataGridViewAddColumnDialog.iHelpServiceType) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("vs.DataGridViewAddColumnDialog");
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000A4964 File Offset: 0x000A2B64
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

		// Token: 0x06001B92 RID: 7058 RVA: 0x000A49E7 File Offset: 0x000A2BE7
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

		// Token: 0x06001B93 RID: 7059 RVA: 0x000A4A20 File Offset: 0x000A2C20
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

		// Token: 0x06001B94 RID: 7060 RVA: 0x000A4ADC File Offset: 0x000A2CDC
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
				if (!(type == DataGridViewAddColumnDialog.dataGridViewColumnType) && !type.IsAbstract && (type.IsPublic || type.IsNestedPublic))
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

		// Token: 0x06001B95 RID: 7061 RVA: 0x000A4C04 File Offset: 0x000A2E04
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

		// Token: 0x06001B96 RID: 7062 RVA: 0x000A4D64 File Offset: 0x000A2F64
		private void addButton_Click(object sender, EventArgs e)
		{
			this.cancelButton.Text = SR.GetString("DataGridView_Close");
			this.AddColumn();
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000465E8 File Offset: 0x000447E8
		private void cancelButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000A4D84 File Offset: 0x000A2F84
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

		// Token: 0x06001B99 RID: 7065 RVA: 0x000A4E6C File Offset: 0x000A306C
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

		// Token: 0x06001B9A RID: 7066 RVA: 0x000A4EA4 File Offset: 0x000A30A4
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

		// Token: 0x06001B9B RID: 7067 RVA: 0x000A4F54 File Offset: 0x000A3154
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

		// Token: 0x06001B9C RID: 7068 RVA: 0x000A4FA4 File Offset: 0x000A31A4
		public static bool ValidName(string name, DataGridViewColumnCollection columns, IContainer container, INameCreationService nameCreationService, DataGridViewColumnCollection liveColumns, bool allowDuplicateNameInLiveColumnCollection)
		{
			return !columns.Contains(name) && (container == null || container.Components[name] == null || (allowDuplicateNameInLiveColumnCollection && liveColumns != null && liveColumns.Contains(name))) && (nameCreationService == null || nameCreationService.IsValidName(name) || (allowDuplicateNameInLiveColumnCollection && liveColumns != null && liveColumns.Contains(name)));
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000A5004 File Offset: 0x000A3204
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

		// Token: 0x04001662 RID: 5730
		private DataGridViewColumnCollection dataGridViewColumns;

		// Token: 0x04001663 RID: 5731
		private DataGridView liveDataGridView;

		// Token: 0x04001664 RID: 5732
		private int insertAtPosition = -1;

		// Token: 0x04001665 RID: 5733
		private int initialDataGridViewColumnsCount = -1;

		// Token: 0x04001666 RID: 5734
		private bool persistChangesToDesigner;

		// Token: 0x04001667 RID: 5735
		private static Type dataGridViewColumnType = typeof(DataGridViewColumn);

		// Token: 0x04001668 RID: 5736
		private static Type iDesignerType = typeof(IDesigner);

		// Token: 0x04001669 RID: 5737
		private static Type iTypeResolutionServiceType = typeof(ITypeResolutionService);

		// Token: 0x0400166A RID: 5738
		private static Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);

		// Token: 0x0400166B RID: 5739
		private static Type iComponentChangeServiceType = typeof(IComponentChangeService);

		// Token: 0x0400166C RID: 5740
		private static Type iHelpServiceType = typeof(IHelpService);

		// Token: 0x0400166D RID: 5741
		private static Type iUIServiceType = typeof(IUIService);

		// Token: 0x0400166E RID: 5742
		private static Type iDesignerHostType = typeof(IDesignerHost);

		// Token: 0x0400166F RID: 5743
		private static Type iNameCreationServiceType = typeof(INameCreationService);

		// Token: 0x04001670 RID: 5744
		private static Type dataGridViewColumnDesignTimeVisibleAttributeType = typeof(DataGridViewColumnDesignTimeVisibleAttribute);

		// Token: 0x04001671 RID: 5745
		private static Type[] columnTypes = new Type[]
		{
			typeof(DataGridViewButtonColumn),
			typeof(DataGridViewCheckBoxColumn),
			typeof(DataGridViewComboBoxColumn),
			typeof(DataGridViewImageColumn),
			typeof(DataGridViewLinkColumn),
			typeof(DataGridViewTextBoxColumn)
		};

		// Token: 0x0200054D RID: 1357
		private class ListBoxItem
		{
			// Token: 0x0600312D RID: 12589 RVA: 0x0010CBDC File Offset: 0x0010ADDC
			public ListBoxItem(Type propertyType, string propertyName)
			{
				this.propertyType = propertyType;
				this.propertyName = propertyName;
			}

			// Token: 0x17000983 RID: 2435
			// (get) Token: 0x0600312E RID: 12590 RVA: 0x0010CBF2 File Offset: 0x0010ADF2
			public Type PropertyType
			{
				get
				{
					return this.propertyType;
				}
			}

			// Token: 0x17000984 RID: 2436
			// (get) Token: 0x0600312F RID: 12591 RVA: 0x0010CBFA File Offset: 0x0010ADFA
			public string PropertyName
			{
				get
				{
					return this.propertyName;
				}
			}

			// Token: 0x06003130 RID: 12592 RVA: 0x0010CBFA File Offset: 0x0010ADFA
			public override string ToString()
			{
				return this.propertyName;
			}

			// Token: 0x04002123 RID: 8483
			private Type propertyType;

			// Token: 0x04002124 RID: 8484
			private string propertyName;
		}

		// Token: 0x0200054E RID: 1358
		private class ComboBoxItem
		{
			// Token: 0x06003131 RID: 12593 RVA: 0x0010CC02 File Offset: 0x0010AE02
			public ComboBoxItem(Type columnType)
			{
				this.columnType = columnType;
			}

			// Token: 0x06003132 RID: 12594 RVA: 0x0010CC11 File Offset: 0x0010AE11
			public override string ToString()
			{
				return this.columnType.Name;
			}

			// Token: 0x17000985 RID: 2437
			// (get) Token: 0x06003133 RID: 12595 RVA: 0x0010CC1E File Offset: 0x0010AE1E
			public Type ColumnType
			{
				get
				{
					return this.columnType;
				}
			}

			// Token: 0x04002125 RID: 8485
			private Type columnType;
		}
	}
}
