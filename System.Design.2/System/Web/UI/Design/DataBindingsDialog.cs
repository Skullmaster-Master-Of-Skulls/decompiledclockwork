using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Web.UI.Design.Util;
using System.Windows.Forms;

namespace System.Web.UI.Design
{
	// Token: 0x02000022 RID: 34
	internal sealed partial class DataBindingsDialog : DesignerForm
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00009D4F File Offset: 0x00007F4F
		public DataBindingsDialog(IServiceProvider serviceProvider, Control control) : base(serviceProvider)
		{
			this._controlID = control.ID;
			this.InitializeComponent();
			this.InitializeUserInterface();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00009D70 File Offset: 0x00007F70
		private Control Control
		{
			get
			{
				IServiceProvider serviceProvider = base.ServiceProvider;
				if (serviceProvider != null)
				{
					ISite site = serviceProvider as ISite;
					IContainer container = null;
					if (site != null)
					{
						container = site.Container;
					}
					IContainer container2;
					if (container != null && container is NestedContainer)
					{
						container2 = container;
					}
					else
					{
						container2 = (IContainer)serviceProvider.GetService(typeof(IContainer));
					}
					if (container2 != null)
					{
						return container2.Components[this._controlID] as Control;
					}
				}
				return null;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00009DE0 File Offset: 0x00007FE0
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.DataBinding.BindingsDialog";
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009DE8 File Offset: 0x00007FE8
		private bool ContainingTemplateIsBindable(ControlDesigner designer)
		{
			bool result = false;
			IControlDesignerView view = designer.View;
			if (view != null)
			{
				TemplatedEditableDesignerRegion templatedEditableDesignerRegion = view.ContainingRegion as TemplatedEditableDesignerRegion;
				if (templatedEditableDesignerRegion != null)
				{
					TemplateDefinition templateDefinition = templatedEditableDesignerRegion.TemplateDefinition;
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(templateDefinition.TemplatedObject)[templateDefinition.TemplatePropertyName];
					if (propertyDescriptor != null)
					{
						TemplateContainerAttribute templateContainerAttribute = propertyDescriptor.Attributes[typeof(TemplateContainerAttribute)] as TemplateContainerAttribute;
						if (templateContainerAttribute != null && templateContainerAttribute.BindingDirection == BindingDirection.TwoWay)
						{
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009E64 File Offset: 0x00008064
		private void ExtractFields(IDataSourceViewSchema schema, ArrayList fields)
		{
			if (schema != null)
			{
				IDataSourceFieldSchema[] fields2 = schema.GetFields();
				if (fields2 != null)
				{
					for (int i = 0; i < fields2.Length; i++)
					{
						fields.Add(new DataBindingsDialog.FieldItem(fields2[i].Name, fields2[i].DataType));
					}
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00009EA8 File Offset: 0x000080A8
		private void ExtractFields(IDataSourceProvider dataSourceProvider, ArrayList fields)
		{
			IEnumerable resolvedSelectedDataSource = dataSourceProvider.GetResolvedSelectedDataSource();
			if (resolvedSelectedDataSource != null)
			{
				PropertyDescriptorCollection dataFields = DesignTimeData.GetDataFields(resolvedSelectedDataSource);
				if (dataFields != null && dataFields.Count != 0)
				{
					foreach (object obj in dataFields)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
						fields.Add(new DataBindingsDialog.FieldItem(propertyDescriptor.Name, propertyDescriptor.PropertyType));
					}
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00009F2C File Offset: 0x0000812C
		private IDesigner GetNamingContainerDesigner(ControlDesigner designer)
		{
			IControlDesignerView view = designer.View;
			if (view == null)
			{
				return null;
			}
			return view.NamingContainerDesigner;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		private void InitializeUserInterface()
		{
			this.Text = SR.GetString("DBDlg_Text", new object[]
			{
				this.Control.Site.Name
			});
			this._instructionLabel.Text = SR.GetString("DBDlg_Inst");
			this._bindablePropsLabels.Text = SR.GetString("DBDlg_BindableProps");
			this._allPropsCheckBox.Text = SR.GetString("DBDlg_ShowAll");
			this._fieldBindingRadio.Text = SR.GetString("DBDlg_FieldBinding");
			this._fieldLabel.Text = SR.GetString("DBDlg_Field");
			this._formatLabel.Text = SR.GetString("DBDlg_Format");
			this._sampleLabel.Text = SR.GetString("DBDlg_Sample");
			this._exprBindingRadio.Text = SR.GetString("DBDlg_CustomBinding");
			this._okButton.Text = SR.GetString("DBDlg_OK");
			this._cancelButton.Text = SR.GetString("DBDlg_Cancel");
			this._refreshSchemaLink.Text = SR.GetString("DBDlg_RefreshSchema");
			this._exprLabel.Text = SR.GetString("DBDlg_Expr");
			this._twoWayBindingCheckBox.Text = SR.GetString("DBDlg_TwoWay");
			ImageList imageList = new ImageList();
			imageList.TransparentColor = Color.Magenta;
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList.Images.AddStrip(BitmapSelector.CreateBitmap(typeof(DataBindingsDialog), "BindableProperties.bmp"));
			this._bindablePropsTree.ImageList = imageList;
			bool visible = false;
			IDesignerHost designerHost = (IDesignerHost)this.Control.Site.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				ControlDesigner controlDesigner = designerHost.GetDesigner(this.Control) as ControlDesigner;
				if (controlDesigner != null)
				{
					visible = this.ContainingTemplateIsBindable(controlDesigner);
				}
			}
			this._twoWayBindingCheckBox.Visible = visible;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000AC7C File Offset: 0x00008E7C
		private void LoadBindableProperties(bool showAll)
		{
			string value = string.Empty;
			if (this._bindablePropsTree.SelectedNode != null)
			{
				value = this._bindablePropsTree.SelectedNode.Text;
			}
			this._bindablePropsTree.Nodes.Clear();
			PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(this.Control.GetType(), DataBindingsDialog.BindablePropertiesFilter);
			if (showAll)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.Control.GetType(), DataBindingsDialog.BrowsablePropertiesFilter);
				if (properties != null && properties.Count > 0)
				{
					int count = propertyDescriptorCollection.Count;
					int count2 = properties.Count;
					PropertyDescriptor[] array = new PropertyDescriptor[count + count2];
					propertyDescriptorCollection.CopyTo(array, 0);
					int num = count;
					foreach (object obj in properties)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
						if (!propertyDescriptorCollection.Contains(propertyDescriptor) && !string.Equals(propertyDescriptor.Name, "id", StringComparison.OrdinalIgnoreCase))
						{
							array[num++] = propertyDescriptor;
						}
					}
					PropertyDescriptor[] array2 = new PropertyDescriptor[num];
					Array.Copy(array, array2, num);
					propertyDescriptorCollection = new PropertyDescriptorCollection(array2);
				}
			}
			string value2 = null;
			ControlValuePropertyAttribute controlValuePropertyAttribute = TypeDescriptor.GetAttributes(this.Control)[typeof(ControlValuePropertyAttribute)] as ControlValuePropertyAttribute;
			if (controlValuePropertyAttribute != null)
			{
				value2 = controlValuePropertyAttribute.Name;
			}
			else
			{
				PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(this.Control);
				if (defaultProperty != null)
				{
					value2 = defaultProperty.Name;
				}
			}
			TreeNodeCollection nodes = this._bindablePropsTree.Nodes;
			TreeNode treeNode = null;
			TreeNode treeNode2 = null;
			this._bindablePropsTree.BeginUpdate();
			foreach (object obj2 in propertyDescriptorCollection)
			{
				PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)obj2;
				bool flag = this._bindings[propertyDescriptor2.Name] != null;
				DataBindingsDialog.BindingMode bindingMode = DataBindingsDialog.BindingMode.NotSet;
				if (flag)
				{
					if (((DesignTimeDataBinding)this._bindings[propertyDescriptor2.Name]).IsTwoWayBound)
					{
						bindingMode = DataBindingsDialog.BindingMode.TwoWay;
					}
					else
					{
						bindingMode = DataBindingsDialog.BindingMode.OneWay;
					}
				}
				TreeNode treeNode3 = new DataBindingsDialog.BindablePropertyNode(propertyDescriptor2, bindingMode);
				if (propertyDescriptor2.Name.Equals(value2))
				{
					treeNode = treeNode3;
				}
				if (propertyDescriptor2.Name.Equals(value))
				{
					treeNode2 = treeNode3;
				}
				nodes.Add(treeNode3);
			}
			this._bindablePropsTree.EndUpdate();
			if (treeNode2 == null && treeNode == null && nodes.Count != 0)
			{
				int count3 = nodes.Count;
				for (int i = 0; i < count3; i++)
				{
					DataBindingsDialog.BindablePropertyNode bindablePropertyNode = (DataBindingsDialog.BindablePropertyNode)nodes[i];
					if (bindablePropertyNode.IsBound)
					{
						treeNode2 = bindablePropertyNode;
						break;
					}
				}
				if (treeNode2 == null)
				{
					treeNode2 = nodes[0];
				}
			}
			if (treeNode2 != null)
			{
				this._bindablePropsTree.SelectedNode = treeNode2;
			}
			else if (treeNode != null)
			{
				this._bindablePropsTree.SelectedNode = treeNode;
			}
			this.UpdateUIState();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000AF78 File Offset: 0x00009178
		private void LoadCurrentDataBinding()
		{
			this._internalChange = true;
			try
			{
				this._fieldBindingRadio.Checked = this._fieldsAvailable;
				this._bindingLabel.Text = string.Empty;
				this._fieldCombo.SelectedIndex = -1;
				this._formatCombo.Text = string.Empty;
				this._sampleTextBox.Text = string.Empty;
				this._exprBindingRadio.Checked = !this._fieldsAvailable;
				this._exprTextBox.Text = string.Empty;
				this._twoWayBindingCheckBox.Checked = false;
				this._formatDirty = false;
				if (this._currentNode != null)
				{
					this._bindingLabel.Text = SR.GetString("DBDlg_BindingGroup", new object[]
					{
						this._currentNode.PropertyDescriptor.Name
					});
					this._twoWayBindingCheckBox.Checked = (this._currentNode.TwoWayBoundByDefault && this._twoWayBindingCheckBox.Visible);
					if (this._currentDataBinding != null)
					{
						bool flag = true;
						if (this._fieldsAvailable && !this._currentDataBinding.IsCustom)
						{
							string text = this._currentDataBinding.Field;
							string format = this._currentDataBinding.Format;
							text = text.TrimStart(new char[]
							{
								'['
							});
							text = text.TrimEnd(new char[]
							{
								']'
							});
							int num = this._fieldCombo.FindStringExact(text, 1);
							if (num != -1)
							{
								flag = false;
								this._fieldCombo.SelectedIndex = num;
								this.UpdateFormatItems();
								bool flag2 = false;
								foreach (object obj in this._formatCombo.Items)
								{
									DataBindingsDialog.FormatItem formatItem = (DataBindingsDialog.FormatItem)obj;
									if (formatItem.Format.Equals(format))
									{
										flag2 = true;
										this._formatCombo.SelectedItem = formatItem;
									}
								}
								if (!flag2)
								{
									this._formatCombo.Text = format;
								}
								this.UpdateFormatSample();
								if (this._currentNode.BindingMode == DataBindingsDialog.BindingMode.TwoWay)
								{
									this._twoWayBindingCheckBox.Checked = true;
								}
								else if (this._currentNode.BindingMode == DataBindingsDialog.BindingMode.OneWay)
								{
									this._twoWayBindingCheckBox.Checked = false;
								}
							}
						}
						if (flag)
						{
							this._exprBindingRadio.Checked = true;
							this._exprTextBox.Text = this._currentDataBinding.Expression;
						}
						else
						{
							this.UpdateExpression();
						}
					}
				}
			}
			finally
			{
				this._internalChange = false;
				this.UpdateUIState();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000B220 File Offset: 0x00009420
		private void LoadDataBindings()
		{
			this._bindings = new Hashtable();
			DataBindingCollection dataBindings = ((IDataBindingsAccessor)this.Control).DataBindings;
			foreach (object obj in dataBindings)
			{
				DataBinding dataBinding = (DataBinding)obj;
				this._bindings[dataBinding.PropertyName] = new DesignTimeDataBinding(dataBinding);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000B29C File Offset: 0x0000949C
		private void LoadFields()
		{
			this._fieldCombo.Items.Clear();
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new DataBindingsDialog.FieldItem());
			IDesigner designer = null;
			IDesignerHost designerHost = (IDesignerHost)this.Control.Site.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				ControlDesigner controlDesigner = designerHost.GetDesigner(this.Control) as ControlDesigner;
				if (controlDesigner != null)
				{
					designer = this.GetNamingContainerDesigner(controlDesigner);
				}
			}
			if (designer != null)
			{
				IDataBindingSchemaProvider dataBindingSchemaProvider = designer as IDataBindingSchemaProvider;
				if (dataBindingSchemaProvider != null)
				{
					if (dataBindingSchemaProvider.CanRefreshSchema)
					{
						this._refreshSchemaLink.Visible = true;
					}
					IDataSourceViewSchema schema = null;
					try
					{
						schema = dataBindingSchemaProvider.Schema;
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
					this.ExtractFields(schema, arrayList);
				}
				else if (designer is IDataSourceProvider)
				{
					this.ExtractFields((IDataSourceProvider)designer, arrayList);
				}
			}
			this._fieldCombo.Items.AddRange(arrayList.ToArray());
			this._fieldsAvailable = (arrayList.Count > 1);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000B3E4 File Offset: 0x000095E4
		private void OnBindablePropsTreeAfterSelect(object sender, TreeViewEventArgs e)
		{
			if (this._currentDataBindingDirty)
			{
				this.SaveCurrentDataBinding();
			}
			this._currentDataBinding = null;
			this._currentNode = (DataBindingsDialog.BindablePropertyNode)this._bindablePropsTree.SelectedNode;
			if (this._currentNode != null)
			{
				this._currentDataBinding = (DesignTimeDataBinding)this._bindings[this._currentNode.PropertyDescriptor.Name];
			}
			this.LoadCurrentDataBinding();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000B450 File Offset: 0x00009650
		private void OnExprBindingRadioCheckedChanged(object sender, EventArgs e)
		{
			if (this._internalChange)
			{
				return;
			}
			this._currentDataBindingDirty = true;
			this.UpdateUIState();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000B468 File Offset: 0x00009668
		private void OnExprTextBoxTextChanged(object sender, EventArgs e)
		{
			if (this._internalChange)
			{
				return;
			}
			this._currentDataBindingDirty = true;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000B47A File Offset: 0x0000967A
		private void OnFieldBindingRadioCheckedChanged(object sender, EventArgs e)
		{
			if (this._internalChange)
			{
				return;
			}
			this._currentDataBindingDirty = true;
			if (this._fieldBindingRadio.Checked)
			{
				this.UpdateExpression();
			}
			this.UpdateUIState();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000B4A5 File Offset: 0x000096A5
		private void OnFieldComboSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this._internalChange)
			{
				return;
			}
			this._currentDataBindingDirty = true;
			this.UpdateFormatItems();
			this.UpdateExpression();
			this.UpdateUIState();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000B4C9 File Offset: 0x000096C9
		private void OnFormatComboLostFocus(object sender, EventArgs e)
		{
			if (this._formatDirty)
			{
				this._formatDirty = false;
				this.UpdateFormatSample();
				this.UpdateExpression();
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000B4E6 File Offset: 0x000096E6
		private void OnFormatComboTextChanged(object sender, EventArgs e)
		{
			if (this._internalChange)
			{
				return;
			}
			this._formatDirty = true;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000B4F8 File Offset: 0x000096F8
		private void OnFormatComboSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this._internalChange)
			{
				return;
			}
			this._formatDirty = true;
			this.UpdateFormatSample();
			this.UpdateExpression();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000B516 File Offset: 0x00009716
		protected override void OnInitialActivated(EventArgs e)
		{
			base.OnInitialActivated(e);
			this.LoadDataBindings();
			this.LoadFields();
			this.LoadBindableProperties(false);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000B532 File Offset: 0x00009732
		private void OnOKButtonClick(object sender, EventArgs e)
		{
			if (this._currentDataBindingDirty)
			{
				this.SaveCurrentDataBinding();
			}
			if (this._bindingsDirty)
			{
				this.SaveDataBindings();
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000B560 File Offset: 0x00009760
		private void OnRefreshSchemaLinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (this._currentDataBindingDirty)
			{
				this.SaveCurrentDataBinding();
			}
			IDesigner designer = null;
			IDesignerHost designerHost = (IDesignerHost)this.Control.Site.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				ControlDesigner controlDesigner = designerHost.GetDesigner(this.Control) as ControlDesigner;
				if (controlDesigner != null)
				{
					designer = this.GetNamingContainerDesigner(controlDesigner);
				}
			}
			if (designer != null)
			{
				IDataBindingSchemaProvider dataBindingSchemaProvider = designer as IDataBindingSchemaProvider;
				if (dataBindingSchemaProvider != null)
				{
					dataBindingSchemaProvider.RefreshSchema(false);
				}
			}
			this.LoadFields();
			if (this._currentNode != null)
			{
				this._currentDataBinding = (DesignTimeDataBinding)this._bindings[this._currentNode.PropertyDescriptor.Name];
			}
			this.LoadCurrentDataBinding();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000B60B File Offset: 0x0000980B
		private void OnShowAllCheckedChanged(object sender, EventArgs e)
		{
			this.LoadBindableProperties(this._allPropsCheckBox.Checked);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000B61E File Offset: 0x0000981E
		private void OnTwoWayBindingChecked(object sender, EventArgs e)
		{
			if (this._internalChange)
			{
				return;
			}
			this._currentDataBindingDirty = true;
			this.UpdateExpression();
			this.UpdateUIState();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000B63C File Offset: 0x0000983C
		private void SaveCurrentDataBinding()
		{
			DesignTimeDataBinding designTimeDataBinding = null;
			if (this._fieldBindingRadio.Checked)
			{
				if (this._fieldCombo.SelectedIndex > 0)
				{
					string text = this._fieldCombo.Text;
					string format = this.SaveFormat();
					designTimeDataBinding = new DesignTimeDataBinding(this._currentNode.PropertyDescriptor, text, format, this._twoWayBindingCheckBox.Checked);
				}
			}
			else
			{
				string text2 = this._exprTextBox.Text.Trim();
				if (text2.Length != 0)
				{
					designTimeDataBinding = new DesignTimeDataBinding(this._currentNode.PropertyDescriptor, text2);
				}
			}
			if (designTimeDataBinding == null)
			{
				this._currentNode.BindingMode = DataBindingsDialog.BindingMode.NotSet;
				this._bindings.Remove(this._currentNode.PropertyDescriptor.Name);
			}
			else
			{
				if (this._fieldBindingRadio.Checked)
				{
					if (this._twoWayBindingCheckBox.Checked && this._twoWayBindingCheckBox.Visible)
					{
						this._currentNode.BindingMode = DataBindingsDialog.BindingMode.TwoWay;
					}
					else
					{
						this._currentNode.BindingMode = DataBindingsDialog.BindingMode.OneWay;
					}
				}
				else if (designTimeDataBinding.IsTwoWayBound)
				{
					this._currentNode.BindingMode = DataBindingsDialog.BindingMode.TwoWay;
				}
				else
				{
					this._currentNode.BindingMode = DataBindingsDialog.BindingMode.OneWay;
				}
				this._bindings[this._currentNode.PropertyDescriptor.Name] = designTimeDataBinding;
			}
			this._currentDataBindingDirty = false;
			this._bindingsDirty = true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000B784 File Offset: 0x00009984
		private void SaveDataBindings()
		{
			DataBindingCollection dataBindings = ((IDataBindingsAccessor)this.Control).DataBindings;
			ExpressionBindingCollection expressions = ((IExpressionsAccessor)this.Control).Expressions;
			dataBindings.Clear();
			foreach (object obj in this._bindings.Values)
			{
				DesignTimeDataBinding designTimeDataBinding = (DesignTimeDataBinding)obj;
				dataBindings.Add(designTimeDataBinding.RuntimeDataBinding);
				expressions.Remove(designTimeDataBinding.RuntimeDataBinding.PropertyName);
			}
			this._bindingsDirty = false;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000B820 File Offset: 0x00009A20
		private string SaveFormat()
		{
			string text = string.Empty;
			DataBindingsDialog.FormatItem formatItem = this._formatCombo.SelectedItem as DataBindingsDialog.FormatItem;
			if (formatItem != null)
			{
				text = formatItem.Format;
			}
			else
			{
				text = this._formatCombo.Text;
				string text2 = text.Trim();
				if (text2.Length == 0)
				{
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000B870 File Offset: 0x00009A70
		private void UpdateExpression()
		{
			string text = string.Empty;
			if (this._fieldCombo.SelectedIndex > 0)
			{
				string text2 = this._fieldCombo.Text;
				string format = this.SaveFormat();
				if (this._twoWayBindingCheckBox.Checked)
				{
					text = DesignTimeDataBinding.CreateBindExpression(text2, format);
				}
				else
				{
					text = DesignTimeDataBinding.CreateEvalExpression(text2, format);
				}
			}
			this._exprTextBox.Text = text;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		private void UpdateFormatItems()
		{
			DataBindingsDialog.FormatItem[] array = DataBindingsDialog.FormatItem.DefaultFormats;
			this._formatSampleObject = null;
			this._formatCombo.SelectedIndex = -1;
			this._formatCombo.Text = string.Empty;
			DataBindingsDialog.FieldItem fieldItem = (DataBindingsDialog.FieldItem)this._fieldCombo.SelectedItem;
			if (fieldItem != null && fieldItem.Type != null)
			{
				switch (Type.GetTypeCode(fieldItem.Type))
				{
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					array = DataBindingsDialog.FormatItem.NumericFormats;
					this._formatSampleObject = 1;
					break;
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					array = DataBindingsDialog.FormatItem.DecimalFormats;
					this._formatSampleObject = 1;
					break;
				case TypeCode.DateTime:
					array = DataBindingsDialog.FormatItem.DateTimeFormats;
					this._formatSampleObject = DateTime.Today;
					break;
				case TypeCode.String:
					this._formatSampleObject = "abc";
					break;
				}
			}
			this._formatCombo.Items.Clear();
			ComboBox.ObjectCollection items = this._formatCombo.Items;
			object[] items2 = array;
			items.AddRange(items2);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000B9FC File Offset: 0x00009BFC
		private void UpdateFormatSample()
		{
			string text = string.Empty;
			if (this._formatSampleObject != null)
			{
				string text2 = this.SaveFormat();
				if (text2.Length != 0)
				{
					try
					{
						text = string.Format(CultureInfo.CurrentCulture, text2, new object[]
						{
							this._formatSampleObject
						});
					}
					catch
					{
						text = SR.GetString("DBDlg_InvalidFormat");
					}
				}
			}
			this._sampleTextBox.Text = text;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000BA70 File Offset: 0x00009C70
		private void UpdateUIState()
		{
			if (this._currentNode == null)
			{
				this._fieldBindingRadio.Enabled = false;
				this._fieldCombo.Enabled = false;
				this._formatCombo.Enabled = false;
				this._sampleTextBox.Enabled = false;
				this._fieldLabel.Enabled = false;
				this._formatLabel.Enabled = false;
				this._sampleLabel.Enabled = false;
				this._twoWayBindingCheckBox.Enabled = false;
				this._exprBindingRadio.Enabled = false;
				this._exprTextBox.Enabled = false;
				return;
			}
			this._fieldBindingRadio.Enabled = this._fieldsAvailable;
			this._exprBindingRadio.Enabled = true;
			bool @checked = this._fieldBindingRadio.Checked;
			bool flag = @checked && this._fieldCombo.SelectedIndex > 0;
			bool enabled = flag && this._currentNode.PropertyDescriptor.PropertyType == typeof(string);
			this._fieldCombo.Enabled = @checked;
			this._fieldLabel.Enabled = @checked;
			this._formatCombo.Enabled = enabled;
			this._formatLabel.Enabled = enabled;
			this._sampleTextBox.Enabled = enabled;
			this._sampleLabel.Enabled = enabled;
			this._twoWayBindingCheckBox.Enabled = flag;
			this._exprTextBox.Enabled = !@checked;
		}

		// Token: 0x040000E8 RID: 232
		private static readonly Attribute[] BrowsablePropertiesFilter = new Attribute[]
		{
			BrowsableAttribute.Yes,
			ReadOnlyAttribute.No
		};

		// Token: 0x040000E9 RID: 233
		private static readonly Attribute[] BindablePropertiesFilter = new Attribute[]
		{
			BindableAttribute.Yes,
			ReadOnlyAttribute.No
		};

		// Token: 0x040000EA RID: 234
		private const int UnboundImageIndex = 0;

		// Token: 0x040000EB RID: 235
		private const int BoundImageIndex = 1;

		// Token: 0x040000EC RID: 236
		private const int TwoWayBoundImageIndex = 2;

		// Token: 0x040000ED RID: 237
		private const int UnboundItemIndex = 0;

		// Token: 0x04000104 RID: 260
		private string _controlID;

		// Token: 0x04000105 RID: 261
		private IDictionary _bindings;

		// Token: 0x04000106 RID: 262
		private bool _bindingsDirty;

		// Token: 0x04000107 RID: 263
		private bool _fieldsAvailable;

		// Token: 0x04000108 RID: 264
		private DataBindingsDialog.BindablePropertyNode _currentNode;

		// Token: 0x04000109 RID: 265
		private DesignTimeDataBinding _currentDataBinding;

		// Token: 0x0400010A RID: 266
		private bool _currentDataBindingDirty;

		// Token: 0x0400010B RID: 267
		private bool _internalChange;

		// Token: 0x0400010C RID: 268
		private bool _formatDirty;

		// Token: 0x0400010D RID: 269
		private object _formatSampleObject;

		// Token: 0x020003A9 RID: 937
		private sealed class BindablePropertyNode : TreeNode
		{
			// Token: 0x060025E5 RID: 9701 RVA: 0x000EC000 File Offset: 0x000EA200
			public BindablePropertyNode(PropertyDescriptor propDesc, DataBindingsDialog.BindingMode bindingMode)
			{
				this._propDesc = propDesc;
				this._bindingMode = bindingMode;
				base.Text = propDesc.Name;
				int selectedImageIndex = 0;
				if (bindingMode == DataBindingsDialog.BindingMode.OneWay)
				{
					selectedImageIndex = 1;
				}
				else if (bindingMode == DataBindingsDialog.BindingMode.TwoWay)
				{
					selectedImageIndex = 2;
				}
				base.ImageIndex = (base.SelectedImageIndex = selectedImageIndex);
			}

			// Token: 0x170007F7 RID: 2039
			// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000EC04D File Offset: 0x000EA24D
			// (set) Token: 0x060025E7 RID: 9703 RVA: 0x000EC058 File Offset: 0x000EA258
			public DataBindingsDialog.BindingMode BindingMode
			{
				get
				{
					return this._bindingMode;
				}
				set
				{
					this._bindingMode = value;
					int selectedImageIndex = 0;
					if (this._bindingMode == DataBindingsDialog.BindingMode.OneWay)
					{
						selectedImageIndex = 1;
					}
					else if (this._bindingMode == DataBindingsDialog.BindingMode.TwoWay)
					{
						selectedImageIndex = 2;
					}
					base.ImageIndex = (base.SelectedImageIndex = selectedImageIndex);
				}
			}

			// Token: 0x170007F8 RID: 2040
			// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000EC096 File Offset: 0x000EA296
			public bool IsBound
			{
				get
				{
					return this._bindingMode == DataBindingsDialog.BindingMode.OneWay || this._bindingMode == DataBindingsDialog.BindingMode.TwoWay;
				}
			}

			// Token: 0x170007F9 RID: 2041
			// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000EC0AC File Offset: 0x000EA2AC
			public bool TwoWayBoundByDefault
			{
				get
				{
					if (!this._twoWayBoundByDefaultValid)
					{
						BindableAttribute bindableAttribute = this._propDesc.Attributes[typeof(BindableAttribute)] as BindableAttribute;
						if (bindableAttribute != null)
						{
							this._twoWayBoundByDefault = (bindableAttribute.Direction == BindingDirection.TwoWay);
						}
						this._twoWayBoundByDefaultValid = true;
					}
					return this._twoWayBoundByDefault;
				}
			}

			// Token: 0x170007FA RID: 2042
			// (get) Token: 0x060025EA RID: 9706 RVA: 0x000EC100 File Offset: 0x000EA300
			public PropertyDescriptor PropertyDescriptor
			{
				get
				{
					return this._propDesc;
				}
			}

			// Token: 0x04001B8C RID: 7052
			private PropertyDescriptor _propDesc;

			// Token: 0x04001B8D RID: 7053
			private DataBindingsDialog.BindingMode _bindingMode;

			// Token: 0x04001B8E RID: 7054
			private bool _twoWayBoundByDefault;

			// Token: 0x04001B8F RID: 7055
			private bool _twoWayBoundByDefaultValid;
		}

		// Token: 0x020003AA RID: 938
		private enum BindingMode
		{
			// Token: 0x04001B91 RID: 7057
			NotSet,
			// Token: 0x04001B92 RID: 7058
			OneWay,
			// Token: 0x04001B93 RID: 7059
			TwoWay
		}

		// Token: 0x020003AB RID: 939
		private sealed class FieldItem
		{
			// Token: 0x060025EB RID: 9707 RVA: 0x000EC108 File Offset: 0x000EA308
			public FieldItem() : this(SR.GetString("DBDlg_Unbound"), null)
			{
			}

			// Token: 0x060025EC RID: 9708 RVA: 0x000EC11B File Offset: 0x000EA31B
			public FieldItem(string name, Type type)
			{
				this._name = name;
				this._type = type;
			}

			// Token: 0x170007FB RID: 2043
			// (get) Token: 0x060025ED RID: 9709 RVA: 0x000EC131 File Offset: 0x000EA331
			public Type Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x060025EE RID: 9710 RVA: 0x000EC139 File Offset: 0x000EA339
			public override string ToString()
			{
				return this._name;
			}

			// Token: 0x04001B94 RID: 7060
			private string _name;

			// Token: 0x04001B95 RID: 7061
			private Type _type;
		}

		// Token: 0x020003AC RID: 940
		private class FormatItem
		{
			// Token: 0x060025EF RID: 9711 RVA: 0x000EC141 File Offset: 0x000EA341
			private FormatItem(string displayText, string format)
			{
				this._displayText = string.Format(CultureInfo.CurrentCulture, displayText, new object[]
				{
					format
				});
				this._format = format;
			}

			// Token: 0x170007FC RID: 2044
			// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000EC16B File Offset: 0x000EA36B
			public string Format
			{
				get
				{
					return this._format;
				}
			}

			// Token: 0x060025F1 RID: 9713 RVA: 0x000EC173 File Offset: 0x000EA373
			public override string ToString()
			{
				return this._displayText;
			}

			// Token: 0x04001B96 RID: 7062
			private static readonly DataBindingsDialog.FormatItem nullFormat = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_None"), string.Empty);

			// Token: 0x04001B97 RID: 7063
			private static readonly DataBindingsDialog.FormatItem generalFormat = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_General"), "{0}");

			// Token: 0x04001B98 RID: 7064
			private static readonly DataBindingsDialog.FormatItem dtShortTime = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_ShortTime"), "{0:t}");

			// Token: 0x04001B99 RID: 7065
			private static readonly DataBindingsDialog.FormatItem dtLongTime = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_LongTime"), "{0:T}");

			// Token: 0x04001B9A RID: 7066
			private static readonly DataBindingsDialog.FormatItem dtShortDate = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_ShortDate"), "{0:d}");

			// Token: 0x04001B9B RID: 7067
			private static readonly DataBindingsDialog.FormatItem dtLongDate = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_LongDate"), "{0:D}");

			// Token: 0x04001B9C RID: 7068
			private static readonly DataBindingsDialog.FormatItem dtDateTime = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_DateTime"), "{0:g}");

			// Token: 0x04001B9D RID: 7069
			private static readonly DataBindingsDialog.FormatItem dtFullDate = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_FullDate"), "{0:G}");

			// Token: 0x04001B9E RID: 7070
			private static readonly DataBindingsDialog.FormatItem numNumber = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_Numeric"), "{0:N}");

			// Token: 0x04001B9F RID: 7071
			private static readonly DataBindingsDialog.FormatItem numDecimal = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_Decimal"), "{0:D}");

			// Token: 0x04001BA0 RID: 7072
			private static readonly DataBindingsDialog.FormatItem numFixed = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_Fixed"), "{0:F}");

			// Token: 0x04001BA1 RID: 7073
			private static readonly DataBindingsDialog.FormatItem numCurrency = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_Currency"), "{0:C}");

			// Token: 0x04001BA2 RID: 7074
			private static readonly DataBindingsDialog.FormatItem numScientific = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_Scientific"), "{0:E}");

			// Token: 0x04001BA3 RID: 7075
			private static readonly DataBindingsDialog.FormatItem numHex = new DataBindingsDialog.FormatItem(SR.GetString("DBDlg_Fmt_Hexadecimal"), "0x{0:X}");

			// Token: 0x04001BA4 RID: 7076
			public static readonly DataBindingsDialog.FormatItem[] DefaultFormats = new DataBindingsDialog.FormatItem[]
			{
				DataBindingsDialog.FormatItem.nullFormat,
				DataBindingsDialog.FormatItem.generalFormat
			};

			// Token: 0x04001BA5 RID: 7077
			public static readonly DataBindingsDialog.FormatItem[] DateTimeFormats = new DataBindingsDialog.FormatItem[]
			{
				DataBindingsDialog.FormatItem.nullFormat,
				DataBindingsDialog.FormatItem.generalFormat,
				DataBindingsDialog.FormatItem.dtShortTime,
				DataBindingsDialog.FormatItem.dtLongTime,
				DataBindingsDialog.FormatItem.dtShortDate,
				DataBindingsDialog.FormatItem.dtLongDate,
				DataBindingsDialog.FormatItem.dtDateTime,
				DataBindingsDialog.FormatItem.dtFullDate
			};

			// Token: 0x04001BA6 RID: 7078
			public static readonly DataBindingsDialog.FormatItem[] NumericFormats = new DataBindingsDialog.FormatItem[]
			{
				DataBindingsDialog.FormatItem.nullFormat,
				DataBindingsDialog.FormatItem.generalFormat,
				DataBindingsDialog.FormatItem.numNumber,
				DataBindingsDialog.FormatItem.numDecimal,
				DataBindingsDialog.FormatItem.numFixed,
				DataBindingsDialog.FormatItem.numCurrency,
				DataBindingsDialog.FormatItem.numScientific,
				DataBindingsDialog.FormatItem.numHex
			};

			// Token: 0x04001BA7 RID: 7079
			public static readonly DataBindingsDialog.FormatItem[] DecimalFormats = new DataBindingsDialog.FormatItem[]
			{
				DataBindingsDialog.FormatItem.nullFormat,
				DataBindingsDialog.FormatItem.generalFormat,
				DataBindingsDialog.FormatItem.numNumber,
				DataBindingsDialog.FormatItem.numDecimal,
				DataBindingsDialog.FormatItem.numFixed,
				DataBindingsDialog.FormatItem.numCurrency,
				DataBindingsDialog.FormatItem.numScientific
			};

			// Token: 0x04001BA8 RID: 7080
			private readonly string _displayText;

			// Token: 0x04001BA9 RID: 7081
			private readonly string _format;
		}
	}
}
