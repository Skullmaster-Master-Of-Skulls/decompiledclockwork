using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C3 RID: 707
	internal class DataGridViewDesigner : ControlDesigner
	{
		// Token: 0x06001C0A RID: 7178 RVA: 0x00093E53 File Offset: 0x00092053
		public DataGridViewDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000A908C File Offset: 0x000A728C
		public override ICollection AssociatedComponents
		{
			get
			{
				DataGridView dataGridView = base.Component as DataGridView;
				if (dataGridView != null)
				{
					return dataGridView.Columns;
				}
				return base.AssociatedComponents;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x000A90B8 File Offset: 0x000A72B8
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x000A90D8 File Offset: 0x000A72D8
		public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
		{
			get
			{
				DataGridView dataGridView = base.Component as DataGridView;
				return dataGridView.AutoSizeColumnsMode;
			}
			set
			{
				DataGridView dataGridView = base.Component as DataGridView;
				IComponentChangeService componentChangeService = base.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				PropertyDescriptor member = TypeDescriptor.GetProperties(typeof(DataGridViewColumn))["Width"];
				for (int i = 0; i < dataGridView.Columns.Count; i++)
				{
					componentChangeService.OnComponentChanging(dataGridView.Columns[i], member);
				}
				dataGridView.AutoSizeColumnsMode = value;
				for (int j = 0; j < dataGridView.Columns.Count; j++)
				{
					componentChangeService.OnComponentChanged(dataGridView.Columns[j], member, null, null);
				}
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x000A918B File Offset: 0x000A738B
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x000A91A0 File Offset: 0x000A73A0
		public object DataSource
		{
			get
			{
				return ((DataGridView)base.Component).DataSource;
			}
			set
			{
				DataGridView dataGridView = base.Component as DataGridView;
				if (dataGridView.AutoGenerateColumns && dataGridView.DataSource == null && value != null)
				{
					dataGridView.AutoGenerateColumns = false;
				}
				((DataGridView)base.Component).DataSource = value;
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000A91E4 File Offset: 0x000A73E4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				DataGridView dataGridView = base.Component as DataGridView;
				dataGridView.DataSourceChanged -= this.dataGridViewChanged;
				dataGridView.DataMemberChanged -= this.dataGridViewChanged;
				dataGridView.BindingContextChanged -= this.dataGridViewChanged;
				dataGridView.ColumnRemoved -= this.dataGridView_ColumnRemoved;
				if (this.cm != null)
				{
					this.cm.MetaDataChanged -= this.dataGridViewMetaDataChanged;
				}
				this.cm = null;
				if (base.Component.Site != null)
				{
					IComponentChangeService componentChangeService = base.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
					if (componentChangeService != null)
					{
						componentChangeService.ComponentRemoving -= this.DataGridViewDesigner_ComponentRemoving;
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000A92BC File Offset: 0x000A74BC
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			if (component.Site != null)
			{
				IComponentChangeService componentChangeService = component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRemoving += this.DataGridViewDesigner_ComponentRemoving;
				}
			}
			DataGridView dataGridView = (DataGridView)component;
			dataGridView.AutoGenerateColumns = (dataGridView.DataSource == null);
			dataGridView.DataSourceChanged += this.dataGridViewChanged;
			dataGridView.DataMemberChanged += this.dataGridViewChanged;
			dataGridView.BindingContextChanged += this.dataGridViewChanged;
			this.dataGridViewChanged(base.Component, EventArgs.Empty);
			dataGridView.ColumnRemoved += this.dataGridView_ColumnRemoved;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x000A9377 File Offset: 0x000A7577
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			((DataGridView)base.Component).ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x000A9391 File Offset: 0x000A7591
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (base.InheritanceAttribute == InheritanceAttribute.Inherited || base.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
				{
					return InheritanceAttribute.InheritedReadOnly;
				}
				return base.InheritanceAttribute;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x000A93BC File Offset: 0x000A75BC
		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (this.designerVerbs == null)
				{
					this.designerVerbs = new DesignerVerbCollection();
					this.designerVerbs.Add(new DesignerVerb(SR.GetString("DataGridViewEditColumnsVerb"), new EventHandler(this.OnEditColumns)));
					this.designerVerbs.Add(new DesignerVerb(SR.GetString("DataGridViewAddColumnVerb"), new EventHandler(this.OnAddColumn)));
				}
				return this.designerVerbs;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x000A9430 File Offset: 0x000A7630
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this.actionLists == null)
				{
					this.BuildActionLists();
				}
				return this.actionLists;
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000A9448 File Offset: 0x000A7648
		private void BuildActionLists()
		{
			this.actionLists = new DesignerActionListCollection();
			this.actionLists.Add(new DataGridViewDesigner.DataGridViewChooseDataSourceActionList(this));
			this.actionLists.Add(new DataGridViewDesigner.DataGridViewColumnEditingActionList(this));
			this.actionLists.Add(new DataGridViewDesigner.DataGridViewPropertiesActionList(this));
			this.actionLists[0].AutoShow = true;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000A94A8 File Offset: 0x000A76A8
		private void dataGridViewChanged(object sender, EventArgs e)
		{
			DataGridView dataGridView = (DataGridView)base.Component;
			CurrencyManager currencyManager = null;
			if (dataGridView.DataSource != null && dataGridView.BindingContext != null)
			{
				currencyManager = (CurrencyManager)dataGridView.BindingContext[dataGridView.DataSource, dataGridView.DataMember];
			}
			if (currencyManager != this.cm)
			{
				if (this.cm != null)
				{
					this.cm.MetaDataChanged -= this.dataGridViewMetaDataChanged;
				}
				this.cm = currencyManager;
				if (this.cm != null)
				{
					this.cm.MetaDataChanged += this.dataGridViewMetaDataChanged;
				}
			}
			if (dataGridView.BindingContext == null)
			{
				DataGridViewDesigner.MakeSureColumnsAreSited(dataGridView);
				return;
			}
			if (dataGridView.AutoGenerateColumns && dataGridView.DataSource != null)
			{
				dataGridView.AutoGenerateColumns = false;
				DataGridViewDesigner.MakeSureColumnsAreSited(dataGridView);
				return;
			}
			if (dataGridView.DataSource == null)
			{
				if (dataGridView.AutoGenerateColumns)
				{
					DataGridViewDesigner.MakeSureColumnsAreSited(dataGridView);
					return;
				}
				dataGridView.AutoGenerateColumns = true;
			}
			else
			{
				dataGridView.AutoGenerateColumns = false;
			}
			this.RefreshColumnCollection();
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000A9598 File Offset: 0x000A7798
		private void DataGridViewDesigner_ComponentRemoving(object sender, ComponentEventArgs e)
		{
			DataGridView dataGridView = base.Component as DataGridView;
			if (e.Component != null && e.Component == dataGridView.DataSource)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				string dataMember = dataGridView.DataMember;
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataGridView);
				PropertyDescriptor propertyDescriptor = (properties != null) ? properties["DataMember"] : null;
				if (componentChangeService != null && propertyDescriptor != null)
				{
					componentChangeService.OnComponentChanging(dataGridView, propertyDescriptor);
				}
				dataGridView.DataSource = null;
				if (componentChangeService != null && propertyDescriptor != null)
				{
					componentChangeService.OnComponentChanged(dataGridView, propertyDescriptor, dataMember, "");
				}
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000A962B File Offset: 0x000A782B
		private void dataGridView_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
		{
			if (e.Column != null && !e.Column.IsDataBound)
			{
				e.Column.DisplayIndex = -1;
			}
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000A9650 File Offset: 0x000A7850
		private static void MakeSureColumnsAreSited(DataGridView dataGridView)
		{
			IContainer container = (dataGridView.Site != null) ? dataGridView.Site.Container : null;
			for (int i = 0; i < dataGridView.Columns.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = dataGridView.Columns[i];
				IContainer container2 = (dataGridViewColumn.Site != null) ? dataGridViewColumn.Site.Container : null;
				if (container != container2)
				{
					if (container2 != null)
					{
						container2.Remove(dataGridViewColumn);
					}
					if (container != null)
					{
						container.Add(dataGridViewColumn);
					}
				}
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000A96C8 File Offset: 0x000A78C8
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"AutoSizeColumnsMode",
				"DataSource"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(DataGridViewDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000A9734 File Offset: 0x000A7934
		private bool ProcessSimilarSchema(DataGridView dataGridView)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (this.cm != null)
			{
				try
				{
					propertyDescriptorCollection = this.cm.GetItemProperties();
				}
				catch (ArgumentException innerException)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewDataSourceNoLongerValid"), innerException);
				}
			}
			IContainer container = (dataGridView.Site != null) ? dataGridView.Site.Container : null;
			bool flag = false;
			for (int i = 0; i < dataGridView.Columns.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = dataGridView.Columns[i];
				if (!string.IsNullOrEmpty(dataGridViewColumn.DataPropertyName))
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataGridViewColumn)["UserAddedColumn"];
					if (propertyDescriptor == null || !(bool)propertyDescriptor.GetValue(dataGridViewColumn))
					{
						PropertyDescriptor propertyDescriptor2 = (propertyDescriptorCollection != null) ? propertyDescriptorCollection[dataGridViewColumn.DataPropertyName] : null;
						bool flag2 = false;
						if (propertyDescriptor2 == null)
						{
							flag2 = true;
						}
						else if (DataGridViewDesigner.typeofIList.IsAssignableFrom(propertyDescriptor2.PropertyType))
						{
							TypeConverter converter = TypeDescriptor.GetConverter(typeof(Image));
							if (!converter.CanConvertFrom(propertyDescriptor2.PropertyType))
							{
								flag2 = true;
							}
						}
						flag = !flag2;
						if (flag)
						{
							break;
						}
					}
				}
			}
			if (flag)
			{
				IComponentChangeService componentChangeService = base.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Columns"];
				try
				{
					componentChangeService.OnComponentChanging(base.Component, member);
				}
				catch (InvalidOperationException)
				{
					return flag;
				}
				int j = 0;
				while (j < dataGridView.Columns.Count)
				{
					DataGridViewColumn dataGridViewColumn2 = dataGridView.Columns[j];
					if (string.IsNullOrEmpty(dataGridViewColumn2.DataPropertyName))
					{
						j++;
					}
					else
					{
						PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(dataGridViewColumn2)["UserAddedColumn"];
						if (propertyDescriptor3 != null && (bool)propertyDescriptor3.GetValue(dataGridViewColumn2))
						{
							j++;
						}
						else
						{
							PropertyDescriptor propertyDescriptor4 = (propertyDescriptorCollection != null) ? propertyDescriptorCollection[dataGridViewColumn2.DataPropertyName] : null;
							bool flag3 = false;
							if (propertyDescriptor4 == null)
							{
								flag3 = true;
							}
							else if (DataGridViewDesigner.typeofIList.IsAssignableFrom(propertyDescriptor4.PropertyType))
							{
								TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(Image));
								if (!converter2.CanConvertFrom(propertyDescriptor4.PropertyType))
								{
									flag3 = true;
								}
							}
							if (flag3)
							{
								dataGridView.Columns.Remove(dataGridViewColumn2);
								if (container != null)
								{
									container.Remove(dataGridViewColumn2);
								}
							}
							else
							{
								j++;
							}
						}
					}
				}
				componentChangeService.OnComponentChanged(base.Component, member, null, null);
				return flag;
			}
			return flag;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000A99C4 File Offset: 0x000A7BC4
		private void RefreshColumnCollection()
		{
			DataGridView dataGridView = (DataGridView)base.Component;
			ISupportInitializeNotification supportInitializeNotification = dataGridView.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && !supportInitializeNotification.IsInitialized)
			{
				return;
			}
			IDesignerHost designerHost = base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (this.ProcessSimilarSchema(dataGridView))
			{
				return;
			}
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (this.cm != null)
			{
				try
				{
					propertyDescriptorCollection = this.cm.GetItemProperties();
				}
				catch (ArgumentException innerException)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewDataSourceNoLongerValid"), innerException);
				}
			}
			IContainer container = (dataGridView.Site != null) ? dataGridView.Site.Container : null;
			IComponentChangeService componentChangeService = base.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Columns"];
			componentChangeService.OnComponentChanging(base.Component, member);
			DataGridViewColumn[] array = new DataGridViewColumn[dataGridView.Columns.Count];
			int num = 0;
			for (int i = 0; i < dataGridView.Columns.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = dataGridView.Columns[i];
				if (!string.IsNullOrEmpty(dataGridViewColumn.DataPropertyName))
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataGridViewColumn)["UserAddedColumn"];
					if (propertyDescriptor == null || !(bool)propertyDescriptor.GetValue(dataGridViewColumn))
					{
						array[num] = dataGridViewColumn;
						num++;
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				dataGridView.Columns.Remove(array[j]);
			}
			componentChangeService.OnComponentChanged(base.Component, member, null, null);
			if (container != null)
			{
				for (int k = 0; k < num; k++)
				{
					container.Remove(array[k]);
				}
			}
			DataGridViewColumn[] array2 = null;
			int num2 = 0;
			if (dataGridView.DataSource != null)
			{
				array2 = new DataGridViewColumn[propertyDescriptorCollection.Count];
				num2 = 0;
				int l = 0;
				while (l < propertyDescriptorCollection.Count)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(Image));
					Type propertyType = propertyDescriptorCollection[l].PropertyType;
					Type type;
					if (typeof(IList).IsAssignableFrom(propertyType))
					{
						if (converter.CanConvertFrom(propertyType))
						{
							type = DataGridViewDesigner.typeofDataGridViewImageColumn;
							goto IL_282;
						}
					}
					else
					{
						if (propertyType == typeof(bool) || propertyType == typeof(CheckState))
						{
							type = DataGridViewDesigner.typeofDataGridViewCheckBoxColumn;
							goto IL_282;
						}
						if (typeof(Image).IsAssignableFrom(propertyType) || converter.CanConvertFrom(propertyType))
						{
							type = DataGridViewDesigner.typeofDataGridViewImageColumn;
							goto IL_282;
						}
						type = DataGridViewDesigner.typeofDataGridViewTextBoxColumn;
						goto IL_282;
					}
					IL_361:
					l++;
					continue;
					IL_282:
					string name = ToolStripDesigner.NameFromText(propertyDescriptorCollection[l].Name, type, base.Component.Site);
					DataGridViewColumn dataGridViewColumn2 = TypeDescriptor.CreateInstance(designerHost, type, null, null) as DataGridViewColumn;
					dataGridViewColumn2.DataPropertyName = propertyDescriptorCollection[l].Name;
					dataGridViewColumn2.HeaderText = ((!string.IsNullOrEmpty(propertyDescriptorCollection[l].DisplayName)) ? propertyDescriptorCollection[l].DisplayName : propertyDescriptorCollection[l].Name);
					dataGridViewColumn2.Name = propertyDescriptorCollection[l].Name;
					dataGridViewColumn2.ValueType = propertyDescriptorCollection[l].PropertyType;
					dataGridViewColumn2.ReadOnly = propertyDescriptorCollection[l].IsReadOnly;
					designerHost.Container.Add(dataGridViewColumn2, name);
					array2[num2] = dataGridViewColumn2;
					num2++;
					goto IL_361;
				}
			}
			componentChangeService.OnComponentChanging(base.Component, member);
			for (int m = 0; m < num2; m++)
			{
				array2[m].DisplayIndex = -1;
				dataGridView.Columns.Add(array2[m]);
			}
			componentChangeService.OnComponentChanged(base.Component, member, null, null);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000A9DA0 File Offset: 0x000A7FA0
		private bool ShouldSerializeAutoSizeColumnsMode()
		{
			DataGridView dataGridView = base.Component as DataGridView;
			return dataGridView != null && dataGridView.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.None;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000A9DCA File Offset: 0x000A7FCA
		private bool ShouldSerializeDataSource()
		{
			return ((DataGridView)base.Component).DataSource != null;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000A9DE0 File Offset: 0x000A7FE0
		internal static void ShowErrorDialog(IUIService uiService, Exception ex, Control dataGridView)
		{
			if (uiService != null)
			{
				uiService.ShowError(ex);
				return;
			}
			string text = ex.Message;
			if (text == null || text.Length == 0)
			{
				text = ex.ToString();
			}
			RTLAwareMessageBox.Show(dataGridView, text, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000A9E1F File Offset: 0x000A801F
		internal static void ShowErrorDialog(IUIService uiService, string errorString, Control dataGridView)
		{
			if (uiService != null)
			{
				uiService.ShowError(errorString);
				return;
			}
			RTLAwareMessageBox.Show(dataGridView, errorString, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000A9E3A File Offset: 0x000A803A
		private void dataGridViewMetaDataChanged(object sender, EventArgs e)
		{
			this.RefreshColumnCollection();
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000A9E44 File Offset: 0x000A8044
		public void OnEditColumns(object sender, EventArgs e)
		{
			IDesignerHost designerHost = base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
			DataGridViewColumnCollectionDialog dataGridViewColumnCollectionDialog = DpiHelper.CreateInstanceInSystemAwareContext<DataGridViewColumnCollectionDialog>(() => new DataGridViewColumnCollectionDialog(((DataGridView)base.Component).Site));
			dataGridViewColumnCollectionDialog.SetLiveDataGridView((DataGridView)base.Component);
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewEditColumnsTransactionString"));
			DialogResult dialogResult = DialogResult.Cancel;
			try
			{
				dialogResult = this.ShowDialog(dataGridViewColumnCollectionDialog);
			}
			finally
			{
				if (dialogResult == DialogResult.OK)
				{
					designerTransaction.Commit();
				}
				else
				{
					designerTransaction.Cancel();
				}
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000A9ED4 File Offset: 0x000A80D4
		public void OnAddColumn(object sender, EventArgs e)
		{
			IDesignerHost designerHost = base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewAddColumnTransactionString"));
			DialogResult dialogResult = DialogResult.Cancel;
			DataGridViewAddColumnDialog dataGridViewAddColumnDialog = DpiHelper.CreateInstanceInSystemAwareContext<DataGridViewAddColumnDialog>(() => new DataGridViewAddColumnDialog(((DataGridView)base.Component).Columns, (DataGridView)base.Component));
			dataGridViewAddColumnDialog.Start(((DataGridView)base.Component).Columns.Count, true);
			try
			{
				dialogResult = this.ShowDialog(dataGridViewAddColumnDialog);
			}
			finally
			{
				if (dialogResult == DialogResult.OK)
				{
					designerTransaction.Commit();
				}
				else
				{
					designerTransaction.Cancel();
				}
			}
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000A9F70 File Offset: 0x000A8170
		private DialogResult ShowDialog(Form dialog)
		{
			IUIService iuiservice = base.Component.Site.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				return iuiservice.ShowDialog(dialog);
			}
			return dialog.ShowDialog(base.Component as IWin32Window);
		}

		// Token: 0x040016C4 RID: 5828
		protected DesignerVerbCollection designerVerbs;

		// Token: 0x040016C5 RID: 5829
		private DesignerActionListCollection actionLists;

		// Token: 0x040016C6 RID: 5830
		private CurrencyManager cm;

		// Token: 0x040016C7 RID: 5831
		private static Type typeofIList = typeof(IList);

		// Token: 0x040016C8 RID: 5832
		private static Type typeofDataGridViewImageColumn = typeof(DataGridViewImageColumn);

		// Token: 0x040016C9 RID: 5833
		private static Type typeofDataGridViewTextBoxColumn = typeof(DataGridViewTextBoxColumn);

		// Token: 0x040016CA RID: 5834
		private static Type typeofDataGridViewCheckBoxColumn = typeof(DataGridViewCheckBoxColumn);

		// Token: 0x02000556 RID: 1366
		[ComplexBindingProperties("DataSource", "DataMember")]
		private class DataGridViewChooseDataSourceActionList : DesignerActionList
		{
			// Token: 0x06003161 RID: 12641 RVA: 0x0010D0F4 File Offset: 0x0010B2F4
			public DataGridViewChooseDataSourceActionList(DataGridViewDesigner owner) : base(owner.Component)
			{
				this.owner = owner;
			}

			// Token: 0x06003162 RID: 12642 RVA: 0x0010D10C File Offset: 0x0010B30C
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionPropertyItem("DataSource", SR.GetString("DataGridViewChooseDataSource"))
					{
						RelatedComponent = this.owner.Component
					}
				};
			}

			// Token: 0x17000990 RID: 2448
			// (get) Token: 0x06003163 RID: 12643 RVA: 0x0010D14E File Offset: 0x0010B34E
			// (set) Token: 0x06003164 RID: 12644 RVA: 0x0010D15C File Offset: 0x0010B35C
			[AttributeProvider(typeof(IListSource))]
			public object DataSource
			{
				get
				{
					return this.owner.DataSource;
				}
				set
				{
					DataGridView dataGridView = (DataGridView)this.owner.Component;
					IDesignerHost designerHost = this.owner.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
					PropertyDescriptor member = TypeDescriptor.GetProperties(dataGridView)["DataSource"];
					IComponentChangeService componentChangeService = this.owner.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
					DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewChooseDataSourceTransactionString", new object[]
					{
						dataGridView.Name
					}));
					try
					{
						componentChangeService.OnComponentChanging(this.owner.Component, member);
						this.owner.DataSource = value;
						componentChangeService.OnComponentChanged(this.owner.Component, member, null, null);
						designerTransaction.Commit();
						designerTransaction = null;
					}
					finally
					{
						if (designerTransaction != null)
						{
							designerTransaction.Cancel();
						}
					}
				}
			}

			// Token: 0x0400212F RID: 8495
			private DataGridViewDesigner owner;
		}

		// Token: 0x02000557 RID: 1367
		private class DataGridViewColumnEditingActionList : DesignerActionList
		{
			// Token: 0x06003165 RID: 12645 RVA: 0x0010D250 File Offset: 0x0010B450
			public DataGridViewColumnEditingActionList(DataGridViewDesigner owner) : base(owner.Component)
			{
				this.owner = owner;
			}

			// Token: 0x06003166 RID: 12646 RVA: 0x0010D268 File Offset: 0x0010B468
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionMethodItem(this, "EditColumns", SR.GetString("DataGridViewEditColumnsVerb"), true),
					new DesignerActionMethodItem(this, "AddColumn", SR.GetString("DataGridViewAddColumnVerb"), true)
				};
			}

			// Token: 0x06003167 RID: 12647 RVA: 0x0010D2B6 File Offset: 0x0010B4B6
			public void EditColumns()
			{
				this.owner.OnEditColumns(this, EventArgs.Empty);
			}

			// Token: 0x06003168 RID: 12648 RVA: 0x0010D2C9 File Offset: 0x0010B4C9
			public void AddColumn()
			{
				this.owner.OnAddColumn(this, EventArgs.Empty);
			}

			// Token: 0x04002130 RID: 8496
			private DataGridViewDesigner owner;
		}

		// Token: 0x02000558 RID: 1368
		private class DataGridViewPropertiesActionList : DesignerActionList
		{
			// Token: 0x06003169 RID: 12649 RVA: 0x0010D2DC File Offset: 0x0010B4DC
			public DataGridViewPropertiesActionList(DataGridViewDesigner owner) : base(owner.Component)
			{
				this.owner = owner;
			}

			// Token: 0x0600316A RID: 12650 RVA: 0x0010D2F4 File Offset: 0x0010B4F4
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionPropertyItem("AllowUserToAddRows", SR.GetString("DataGridViewEnableAdding")),
					new DesignerActionPropertyItem("ReadOnly", SR.GetString("DataGridViewEnableEditing")),
					new DesignerActionPropertyItem("AllowUserToDeleteRows", SR.GetString("DataGridViewEnableDeleting")),
					new DesignerActionPropertyItem("AllowUserToOrderColumns", SR.GetString("DataGridViewEnableColumnReordering"))
				};
			}

			// Token: 0x17000991 RID: 2449
			// (get) Token: 0x0600316B RID: 12651 RVA: 0x0010D374 File Offset: 0x0010B574
			// (set) Token: 0x0600316C RID: 12652 RVA: 0x0010D38C File Offset: 0x0010B58C
			public bool AllowUserToAddRows
			{
				get
				{
					return ((DataGridView)this.owner.Component).AllowUserToAddRows;
				}
				set
				{
					if (value != this.AllowUserToAddRows)
					{
						IDesignerHost designerHost = this.owner.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
						DesignerTransaction designerTransaction;
						if (value)
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewEnableAddingTransactionString"));
						}
						else
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewDisableAddingTransactionString"));
						}
						try
						{
							IComponentChangeService componentChangeService = this.owner.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
							PropertyDescriptor member = TypeDescriptor.GetProperties(this.owner.Component)["AllowUserToAddRows"];
							componentChangeService.OnComponentChanging(this.owner.Component, member);
							((DataGridView)this.owner.Component).AllowUserToAddRows = value;
							componentChangeService.OnComponentChanged(this.owner.Component, member, null, null);
							designerTransaction.Commit();
							designerTransaction = null;
						}
						finally
						{
							if (designerTransaction != null)
							{
								designerTransaction.Cancel();
							}
						}
					}
				}
			}

			// Token: 0x17000992 RID: 2450
			// (get) Token: 0x0600316D RID: 12653 RVA: 0x0010D494 File Offset: 0x0010B694
			// (set) Token: 0x0600316E RID: 12654 RVA: 0x0010D4AC File Offset: 0x0010B6AC
			public bool AllowUserToDeleteRows
			{
				get
				{
					return ((DataGridView)this.owner.Component).AllowUserToDeleteRows;
				}
				set
				{
					if (value != this.AllowUserToDeleteRows)
					{
						IDesignerHost designerHost = this.owner.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
						DesignerTransaction designerTransaction;
						if (value)
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewEnableDeletingTransactionString"));
						}
						else
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewDisableDeletingTransactionString"));
						}
						try
						{
							IComponentChangeService componentChangeService = this.owner.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
							PropertyDescriptor member = TypeDescriptor.GetProperties(this.owner.Component)["AllowUserToDeleteRows"];
							componentChangeService.OnComponentChanging(this.owner.Component, member);
							((DataGridView)this.owner.Component).AllowUserToDeleteRows = value;
							componentChangeService.OnComponentChanged(this.owner.Component, member, null, null);
							designerTransaction.Commit();
							designerTransaction = null;
						}
						finally
						{
							if (designerTransaction != null)
							{
								designerTransaction.Cancel();
							}
						}
					}
				}
			}

			// Token: 0x17000993 RID: 2451
			// (get) Token: 0x0600316F RID: 12655 RVA: 0x0010D5B4 File Offset: 0x0010B7B4
			// (set) Token: 0x06003170 RID: 12656 RVA: 0x0010D5CC File Offset: 0x0010B7CC
			public bool AllowUserToOrderColumns
			{
				get
				{
					return ((DataGridView)this.owner.Component).AllowUserToOrderColumns;
				}
				set
				{
					if (value != this.AllowUserToOrderColumns)
					{
						IDesignerHost designerHost = this.owner.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
						DesignerTransaction designerTransaction;
						if (value)
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewEnableColumnReorderingTransactionString"));
						}
						else
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewDisableColumnReorderingTransactionString"));
						}
						try
						{
							IComponentChangeService componentChangeService = this.owner.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
							PropertyDescriptor member = TypeDescriptor.GetProperties(this.owner.Component)["AllowUserToReorderColumns"];
							componentChangeService.OnComponentChanging(this.owner.Component, member);
							((DataGridView)this.owner.Component).AllowUserToOrderColumns = value;
							componentChangeService.OnComponentChanged(this.owner.Component, member, null, null);
							designerTransaction.Commit();
							designerTransaction = null;
						}
						finally
						{
							if (designerTransaction != null)
							{
								designerTransaction.Cancel();
							}
						}
					}
				}
			}

			// Token: 0x17000994 RID: 2452
			// (get) Token: 0x06003171 RID: 12657 RVA: 0x0010D6D4 File Offset: 0x0010B8D4
			// (set) Token: 0x06003172 RID: 12658 RVA: 0x0010D6F0 File Offset: 0x0010B8F0
			public bool ReadOnly
			{
				get
				{
					return !((DataGridView)this.owner.Component).ReadOnly;
				}
				set
				{
					if (value != this.ReadOnly)
					{
						IDesignerHost designerHost = this.owner.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
						DesignerTransaction designerTransaction;
						if (value)
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewEnableEditingTransactionString"));
						}
						else
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewDisableEditingTransactionString"));
						}
						try
						{
							IComponentChangeService componentChangeService = this.owner.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
							PropertyDescriptor member = TypeDescriptor.GetProperties(this.owner.Component)["ReadOnly"];
							componentChangeService.OnComponentChanging(this.owner.Component, member);
							((DataGridView)this.owner.Component).ReadOnly = !value;
							componentChangeService.OnComponentChanged(this.owner.Component, member, null, null);
							designerTransaction.Commit();
							designerTransaction = null;
						}
						finally
						{
							if (designerTransaction != null)
							{
								designerTransaction.Cancel();
							}
						}
					}
				}
			}

			// Token: 0x04002131 RID: 8497
			private DataGridViewDesigner owner;
		}
	}
}
