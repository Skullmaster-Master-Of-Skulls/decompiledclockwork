using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001EF RID: 495
	internal class DataGridViewDesigner : ControlDesigner
	{
		// Token: 0x06001306 RID: 4870 RVA: 0x00060E40 File Offset: 0x0005FE40
		public DataGridViewDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00060E50 File Offset: 0x0005FE50
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

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00060E7C File Offset: 0x0005FE7C
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00060E9C File Offset: 0x0005FE9C
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00060F4F File Offset: 0x0005FF4F
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x00060F64 File Offset: 0x0005FF64
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

		// Token: 0x0600130C RID: 4876 RVA: 0x00060FA8 File Offset: 0x0005FFA8
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

		// Token: 0x0600130D RID: 4877 RVA: 0x00061080 File Offset: 0x00060080
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

		// Token: 0x0600130E RID: 4878 RVA: 0x0006113B File Offset: 0x0006013B
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			((DataGridView)base.Component).ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x00061155 File Offset: 0x00060155
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

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00061180 File Offset: 0x00060180
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x000611F4 File Offset: 0x000601F4
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

		// Token: 0x06001312 RID: 4882 RVA: 0x0006120C File Offset: 0x0006020C
		private void BuildActionLists()
		{
			this.actionLists = new DesignerActionListCollection();
			this.actionLists.Add(new DataGridViewDesigner.DataGridViewChooseDataSourceActionList(this));
			this.actionLists.Add(new DataGridViewDesigner.DataGridViewColumnEditingActionList(this));
			this.actionLists.Add(new DataGridViewDesigner.DataGridViewPropertiesActionList(this));
			this.actionLists[0].AutoShow = true;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0006126C File Offset: 0x0006026C
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

		// Token: 0x06001314 RID: 4884 RVA: 0x0006135C File Offset: 0x0006035C
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

		// Token: 0x06001315 RID: 4885 RVA: 0x000613EF File Offset: 0x000603EF
		private void dataGridView_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
		{
			if (e.Column != null && !e.Column.IsDataBound)
			{
				e.Column.DisplayIndex = -1;
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00061414 File Offset: 0x00060414
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

		// Token: 0x06001317 RID: 4887 RVA: 0x0006148C File Offset: 0x0006048C
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

		// Token: 0x06001318 RID: 4888 RVA: 0x00061500 File Offset: 0x00060500
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

		// Token: 0x06001319 RID: 4889 RVA: 0x00061790 File Offset: 0x00060790
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
							goto IL_278;
						}
					}
					else
					{
						if (propertyType == typeof(bool) || propertyType == typeof(CheckState))
						{
							type = DataGridViewDesigner.typeofDataGridViewCheckBoxColumn;
							goto IL_278;
						}
						if (typeof(Image).IsAssignableFrom(propertyType) || converter.CanConvertFrom(propertyType))
						{
							type = DataGridViewDesigner.typeofDataGridViewImageColumn;
							goto IL_278;
						}
						type = DataGridViewDesigner.typeofDataGridViewTextBoxColumn;
						goto IL_278;
					}
					IL_357:
					l++;
					continue;
					IL_278:
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
					goto IL_357;
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

		// Token: 0x0600131A RID: 4890 RVA: 0x00061B64 File Offset: 0x00060B64
		private bool ShouldSerializeAutoSizeColumnsMode()
		{
			DataGridView dataGridView = base.Component as DataGridView;
			return dataGridView != null && dataGridView.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.None;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00061B8E File Offset: 0x00060B8E
		private bool ShouldSerializeDataSource()
		{
			return ((DataGridView)base.Component).DataSource != null;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00061BA8 File Offset: 0x00060BA8
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

		// Token: 0x0600131D RID: 4893 RVA: 0x00061BE7 File Offset: 0x00060BE7
		internal static void ShowErrorDialog(IUIService uiService, string errorString, Control dataGridView)
		{
			if (uiService != null)
			{
				uiService.ShowError(errorString);
				return;
			}
			RTLAwareMessageBox.Show(dataGridView, errorString, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00061C02 File Offset: 0x00060C02
		private void dataGridViewMetaDataChanged(object sender, EventArgs e)
		{
			this.RefreshColumnCollection();
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00061C0C File Offset: 0x00060C0C
		public void OnEditColumns(object sender, EventArgs e)
		{
			IDesignerHost designerHost = base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
			DataGridViewColumnCollectionDialog dataGridViewColumnCollectionDialog = new DataGridViewColumnCollectionDialog();
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

		// Token: 0x06001320 RID: 4896 RVA: 0x00061C90 File Offset: 0x00060C90
		public void OnAddColumn(object sender, EventArgs e)
		{
			IDesignerHost designerHost = base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewAddColumnTransactionString"));
			DialogResult dialogResult = DialogResult.Cancel;
			DataGridViewAddColumnDialog dataGridViewAddColumnDialog = new DataGridViewAddColumnDialog(((DataGridView)base.Component).Columns, (DataGridView)base.Component);
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

		// Token: 0x06001321 RID: 4897 RVA: 0x00061D3C File Offset: 0x00060D3C
		private DialogResult ShowDialog(Form dialog)
		{
			IUIService iuiservice = base.Component.Site.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				return iuiservice.ShowDialog(dialog);
			}
			return dialog.ShowDialog(base.Component as IWin32Window);
		}

		// Token: 0x0400118F RID: 4495
		protected DesignerVerbCollection designerVerbs;

		// Token: 0x04001190 RID: 4496
		private DesignerActionListCollection actionLists;

		// Token: 0x04001191 RID: 4497
		private CurrencyManager cm;

		// Token: 0x04001192 RID: 4498
		private static Type typeofIList = typeof(IList);

		// Token: 0x04001193 RID: 4499
		private static Type typeofDataGridViewImageColumn = typeof(DataGridViewImageColumn);

		// Token: 0x04001194 RID: 4500
		private static Type typeofDataGridViewTextBoxColumn = typeof(DataGridViewTextBoxColumn);

		// Token: 0x04001195 RID: 4501
		private static Type typeofDataGridViewCheckBoxColumn = typeof(DataGridViewCheckBoxColumn);

		// Token: 0x020001F0 RID: 496
		[ComplexBindingProperties("DataSource", "DataMember")]
		private class DataGridViewChooseDataSourceActionList : DesignerActionList
		{
			// Token: 0x06001323 RID: 4899 RVA: 0x00061DC3 File Offset: 0x00060DC3
			public DataGridViewChooseDataSourceActionList(DataGridViewDesigner owner) : base(owner.Component)
			{
				this.owner = owner;
			}

			// Token: 0x06001324 RID: 4900 RVA: 0x00061DD8 File Offset: 0x00060DD8
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

			// Token: 0x1700030E RID: 782
			// (get) Token: 0x06001325 RID: 4901 RVA: 0x00061E1A File Offset: 0x00060E1A
			// (set) Token: 0x06001326 RID: 4902 RVA: 0x00061E28 File Offset: 0x00060E28
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

			// Token: 0x04001196 RID: 4502
			private DataGridViewDesigner owner;
		}

		// Token: 0x020001F1 RID: 497
		private class DataGridViewColumnEditingActionList : DesignerActionList
		{
			// Token: 0x06001327 RID: 4903 RVA: 0x00061F24 File Offset: 0x00060F24
			public DataGridViewColumnEditingActionList(DataGridViewDesigner owner) : base(owner.Component)
			{
				this.owner = owner;
			}

			// Token: 0x06001328 RID: 4904 RVA: 0x00061F3C File Offset: 0x00060F3C
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionMethodItem(this, "EditColumns", SR.GetString("DataGridViewEditColumnsVerb"), true),
					new DesignerActionMethodItem(this, "AddColumn", SR.GetString("DataGridViewAddColumnVerb"), true)
				};
			}

			// Token: 0x06001329 RID: 4905 RVA: 0x00061F8A File Offset: 0x00060F8A
			public void EditColumns()
			{
				this.owner.OnEditColumns(this, EventArgs.Empty);
			}

			// Token: 0x0600132A RID: 4906 RVA: 0x00061F9D File Offset: 0x00060F9D
			public void AddColumn()
			{
				this.owner.OnAddColumn(this, EventArgs.Empty);
			}

			// Token: 0x04001197 RID: 4503
			private DataGridViewDesigner owner;
		}

		// Token: 0x020001F2 RID: 498
		private class DataGridViewPropertiesActionList : DesignerActionList
		{
			// Token: 0x0600132B RID: 4907 RVA: 0x00061FB0 File Offset: 0x00060FB0
			public DataGridViewPropertiesActionList(DataGridViewDesigner owner) : base(owner.Component)
			{
				this.owner = owner;
			}

			// Token: 0x0600132C RID: 4908 RVA: 0x00061FC8 File Offset: 0x00060FC8
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

			// Token: 0x1700030F RID: 783
			// (get) Token: 0x0600132D RID: 4909 RVA: 0x00062048 File Offset: 0x00061048
			// (set) Token: 0x0600132E RID: 4910 RVA: 0x00062060 File Offset: 0x00061060
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

			// Token: 0x17000310 RID: 784
			// (get) Token: 0x0600132F RID: 4911 RVA: 0x00062168 File Offset: 0x00061168
			// (set) Token: 0x06001330 RID: 4912 RVA: 0x00062180 File Offset: 0x00061180
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

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06001331 RID: 4913 RVA: 0x00062288 File Offset: 0x00061288
			// (set) Token: 0x06001332 RID: 4914 RVA: 0x000622A0 File Offset: 0x000612A0
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

			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06001333 RID: 4915 RVA: 0x000623A8 File Offset: 0x000613A8
			// (set) Token: 0x06001334 RID: 4916 RVA: 0x000623C4 File Offset: 0x000613C4
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

			// Token: 0x04001198 RID: 4504
			private DataGridViewDesigner owner;
		}
	}
}
