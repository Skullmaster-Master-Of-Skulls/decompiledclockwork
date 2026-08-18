using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Design;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI.Design.Util;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design
{
	// Token: 0x0200003C RID: 60
	internal sealed partial class ExpressionBindingsDialog : DesignerForm
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		public ExpressionBindingsDialog(IServiceProvider serviceProvider, Control control) : base(serviceProvider)
		{
			this._control = control;
			this._controlID = control.ID;
			this.InitializeComponent();
			this.InitializeUserInterface();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000DC74 File Offset: 0x0000BE74
		private ExpressionBindingsDialog.ExpressionItem NoneItem
		{
			get
			{
				if (this._noneItem == null)
				{
					this._noneItem = new ExpressionBindingsDialog.ExpressionItem(SR.GetString("ExpressionBindingsDialog_None"));
				}
				return this._noneItem;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000DC99 File Offset: 0x0000BE99
		private Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000DCA1 File Offset: 0x0000BEA1
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.Expressions.BindingsDialog";
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000E22C File Offset: 0x0000C42C
		private void InitializeUserInterface()
		{
			string text = string.Empty;
			if (this.Control != null && this.Control.Site != null)
			{
				text = this.Control.Site.Name;
			}
			this.Text = SR.GetString("ExpressionBindingsDialog_Text", new object[]
			{
				text
			});
			this._instructionLabel.Text = SR.GetString("ExpressionBindingsDialog_Inst");
			this._bindablePropsLabels.Text = SR.GetString("ExpressionBindingsDialog_BindableProps");
			this._okButton.Text = SR.GetString("ExpressionBindingsDialog_OK");
			this._cancelButton.Text = SR.GetString("ExpressionBindingsDialog_Cancel");
			this._expressionBuilderLabel.Text = SR.GetString("ExpressionBindingsDialog_ExpressionType");
			this._propertyGridLabel.Text = SR.GetString("ExpressionBindingsDialog_Properties");
			this._generatedHelpLabel.Text = SR.GetString("ExpressionBindingsDialog_GeneratedExpression");
			ImageList imageList = new ImageList();
			imageList.TransparentColor = Color.Fuchsia;
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList.Images.AddStrip(BitmapSelector.CreateBitmap(typeof(ExpressionBindingsDialog), "ExpressionBindableProperties.bmp"));
			this._bindablePropsTree.ImageList = imageList;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000E358 File Offset: 0x0000C558
		private void LoadBindableProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.Control, ExpressionBindingsDialog.BindablePropertiesFilter);
			string value = null;
			PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(this.Control);
			if (defaultProperty != null)
			{
				value = defaultProperty.Name;
			}
			TreeNodeCollection nodes = this._bindablePropsTree.Nodes;
			ExpressionBindingCollection expressions = ((IExpressionsAccessor)this.Control).Expressions;
			Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in expressions)
			{
				ExpressionBinding expressionBinding = (ExpressionBinding)obj;
				hashtable[expressionBinding.PropertyName] = expressionBinding;
			}
			TreeNode treeNode = null;
			foreach (object obj2 in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				if (string.Compare(propertyDescriptor.Name, "ID", StringComparison.OrdinalIgnoreCase) != 0)
				{
					ExpressionBinding binding = null;
					if (hashtable.Contains(propertyDescriptor.Name))
					{
						binding = (ExpressionBinding)hashtable[propertyDescriptor.Name];
						hashtable.Remove(propertyDescriptor.Name);
					}
					TreeNode treeNode2 = new ExpressionBindingsDialog.BindablePropertyNode(propertyDescriptor, binding);
					if (propertyDescriptor.Name.Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						treeNode = treeNode2;
					}
					nodes.Add(treeNode2);
				}
			}
			this._complexBindings = hashtable;
			if (treeNode == null && nodes.Count != 0)
			{
				int count = nodes.Count;
				for (int i = 0; i < count; i++)
				{
					ExpressionBindingsDialog.BindablePropertyNode bindablePropertyNode = (ExpressionBindingsDialog.BindablePropertyNode)nodes[i];
					if (bindablePropertyNode.IsBound)
					{
						treeNode = bindablePropertyNode;
						break;
					}
				}
				if (treeNode == null)
				{
					treeNode = nodes[0];
				}
			}
			if (treeNode != null)
			{
				this._bindablePropsTree.SelectedNode = treeNode;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000E534 File Offset: 0x0000C734
		private void LoadExpressionEditors()
		{
			this._expressionEditors = new HybridDictionary(true);
			IWebApplication webApplication = (IWebApplication)base.ServiceProvider.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				try
				{
					Configuration configuration = webApplication.OpenWebConfiguration(true);
					if (configuration != null)
					{
						CompilationSection compilationSection = (CompilationSection)configuration.GetSection("system.web/compilation");
						ExpressionBuilderCollection expressionBuilders = compilationSection.ExpressionBuilders;
						foreach (object obj in expressionBuilders)
						{
							ExpressionBuilder expressionBuilder = (ExpressionBuilder)obj;
							string expressionPrefix = expressionBuilder.ExpressionPrefix;
							ExpressionEditor expressionEditor = ExpressionEditor.GetExpressionEditor(expressionPrefix, base.ServiceProvider);
							if (expressionEditor != null)
							{
								this._expressionEditors[expressionPrefix] = expressionEditor;
								this._expressionBuilderComboBox.Items.Add(new ExpressionBindingsDialog.ExpressionItem(expressionPrefix));
							}
						}
					}
				}
				catch
				{
				}
				this._expressionBuilderComboBox.InvalidateDropDownWidth();
			}
			this._expressionBuilderComboBox.Items.Add(this.NoneItem);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000E654 File Offset: 0x0000C854
		private void OnBindablePropsTreeAfterSelect(object sender, TreeViewEventArgs e)
		{
			ExpressionBindingsDialog.BindablePropertyNode bindablePropertyNode = (ExpressionBindingsDialog.BindablePropertyNode)this._bindablePropsTree.SelectedNode;
			if (this._currentNode != bindablePropertyNode)
			{
				this._currentNode = bindablePropertyNode;
				if (this._currentNode != null && this._currentNode.IsBound)
				{
					ExpressionBinding binding = this._currentNode.Binding;
					if (this._currentNode.IsGenerated)
					{
						goto IL_18E;
					}
					ExpressionEditor expressionEditor = (ExpressionEditor)this._expressionEditors[binding.ExpressionPrefix];
					if (expressionEditor == null)
					{
						UIServiceHelper.ShowMessage(base.ServiceProvider, SR.GetString("ExpressionBindingsDialog_UndefinedExpressionPrefix", new object[]
						{
							binding.ExpressionPrefix
						}), SR.GetString("ExpressionBindingsDialog_Text", new object[]
						{
							this.Control.Site.Name
						}), MessageBoxButtons.OK);
						expressionEditor = new ExpressionBindingsDialog.GenericExpressionEditor();
					}
					this._currentEditor = expressionEditor;
					this._currentSheet = this._currentEditor.GetExpressionEditorSheet(binding.Expression, base.ServiceProvider);
					this._internalChange = true;
					try
					{
						foreach (object obj in this._expressionBuilderComboBox.Items)
						{
							ExpressionBindingsDialog.ExpressionItem expressionItem = (ExpressionBindingsDialog.ExpressionItem)obj;
							if (string.Equals(expressionItem.ToString(), binding.ExpressionPrefix, StringComparison.OrdinalIgnoreCase))
							{
								this._expressionBuilderComboBox.SelectedItem = expressionItem;
							}
						}
						this._currentNode.IsValid = this._currentSheet.IsValid;
						goto IL_18E;
					}
					finally
					{
						this._internalChange = false;
					}
				}
				this._expressionBuilderComboBox.SelectedItem = this.NoneItem;
				this._currentEditor = null;
				this._currentSheet = null;
				IL_18E:
				this._expressionBuilderPropertyGrid.SelectedObject = this._currentSheet;
				this.UpdateUIState();
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000E824 File Offset: 0x0000CA24
		private void OnExpressionBuilderComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this._internalChange)
			{
				return;
			}
			this._currentSheet = null;
			if (this._expressionBuilderComboBox.SelectedItem != this.NoneItem)
			{
				this._currentEditor = (ExpressionEditor)this._expressionEditors[this._expressionBuilderComboBox.SelectedItem.ToString()];
				if (this._currentNode != null)
				{
					if (this._currentNode.IsBound)
					{
						ExpressionBinding binding = this._currentNode.Binding;
						if (this._expressionEditors[binding.ExpressionPrefix] == this._currentEditor)
						{
							this._currentSheet = this._currentEditor.GetExpressionEditorSheet(binding.Expression, base.ServiceProvider);
						}
					}
					if (this._currentSheet == null)
					{
						this._currentSheet = this._currentEditor.GetExpressionEditorSheet(string.Empty, base.ServiceProvider);
					}
					this._currentNode.IsValid = this._currentSheet.IsValid;
				}
			}
			this.SaveCurrentExpressionBinding();
			this._expressionBuilderPropertyGrid.SelectedObject = this._currentSheet;
			this.UpdateUIState();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000E92E File Offset: 0x0000CB2E
		private void OnExpressionBuilderPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			this.SaveCurrentExpressionBinding();
			this.UpdateUIState();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000E93C File Offset: 0x0000CB3C
		protected override void OnInitialActivated(EventArgs e)
		{
			base.OnInitialActivated(e);
			this.LoadExpressionEditors();
			this.LoadBindableProperties();
			this.UpdateUIState();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000E958 File Offset: 0x0000CB58
		private void OnOKButtonClick(object sender, EventArgs e)
		{
			if (this._bindingsDirty)
			{
				ExpressionBindingCollection expressions = ((IExpressionsAccessor)this.Control).Expressions;
				DataBindingCollection dataBindings = ((IDataBindingsAccessor)this.Control).DataBindings;
				expressions.Clear();
				foreach (object obj in this._bindablePropsTree.Nodes)
				{
					ExpressionBindingsDialog.BindablePropertyNode bindablePropertyNode = (ExpressionBindingsDialog.BindablePropertyNode)obj;
					if (bindablePropertyNode.IsBound)
					{
						expressions.Add(bindablePropertyNode.Binding);
						if (dataBindings.Contains(bindablePropertyNode.Binding.PropertyName))
						{
							dataBindings.Remove(bindablePropertyNode.Binding.PropertyName);
						}
					}
				}
				foreach (object obj2 in this._complexBindings.Values)
				{
					ExpressionBinding binding = (ExpressionBinding)obj2;
					expressions.Add(binding);
				}
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000EA78 File Offset: 0x0000CC78
		private void SaveCurrentExpressionBinding()
		{
			if (this._expressionBuilderComboBox.SelectedItem == this.NoneItem)
			{
				this._currentNode.Binding = null;
				this._currentNode.IsValid = true;
			}
			else
			{
				string expression = this._currentSheet.GetExpression();
				PropertyDescriptor propertyDescriptor = this._currentNode.PropertyDescriptor;
				string name = propertyDescriptor.Name;
				ExpressionBinding binding = new ExpressionBinding(name, propertyDescriptor.PropertyType, this._expressionBuilderComboBox.SelectedItem.ToString(), expression);
				this._currentNode.Binding = binding;
				this._currentNode.IsValid = this._currentSheet.IsValid;
			}
			this._bindingsDirty = true;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000EB18 File Offset: 0x0000CD18
		private void UpdateUIState()
		{
			if (this._currentNode == null)
			{
				this._expressionBuilderComboBox.Enabled = false;
				this._expressionBuilderPropertyGrid.Enabled = false;
				this._propertiesPanel.Visible = true;
				this._generatedHelpLabel.Visible = false;
			}
			else
			{
				this._expressionBuilderComboBox.Enabled = true;
				bool flag = this._expressionBuilderComboBox.SelectedItem == this.NoneItem;
				this._expressionBuilderPropertyGrid.Enabled = !flag;
				this._propertyGridLabel.Enabled = !flag;
				this._propertiesPanel.Visible = !this._currentNode.IsGenerated;
				this._generatedHelpLabel.Visible = this._currentNode.IsGenerated;
			}
			this._okButton.Enabled = true;
			foreach (object obj in this._bindablePropsTree.Nodes)
			{
				ExpressionBindingsDialog.BindablePropertyNode bindablePropertyNode = (ExpressionBindingsDialog.BindablePropertyNode)obj;
				if (!bindablePropertyNode.IsValid)
				{
					this._okButton.Enabled = false;
					break;
				}
			}
		}

		// Token: 0x0400013B RID: 315
		private static readonly Attribute[] BindablePropertiesFilter = new Attribute[]
		{
			BrowsableAttribute.Yes,
			ReadOnlyAttribute.No
		};

		// Token: 0x0400013C RID: 316
		private const int UnboundImageIndex = 0;

		// Token: 0x0400013D RID: 317
		private const int BoundImageIndex = 1;

		// Token: 0x0400013E RID: 318
		private const int ImplicitBoundImageIndex = 2;

		// Token: 0x0400014B RID: 331
		private string _controlID;

		// Token: 0x0400014C RID: 332
		private bool _bindingsDirty;

		// Token: 0x0400014D RID: 333
		private ExpressionBindingsDialog.ExpressionItem _noneItem;

		// Token: 0x0400014E RID: 334
		private ExpressionBindingsDialog.BindablePropertyNode _currentNode;

		// Token: 0x0400014F RID: 335
		private ExpressionEditor _currentEditor;

		// Token: 0x04000150 RID: 336
		private ExpressionEditorSheet _currentSheet;

		// Token: 0x04000151 RID: 337
		private IDictionary _expressionEditors;

		// Token: 0x04000152 RID: 338
		private bool _internalChange;

		// Token: 0x04000153 RID: 339
		private IDictionary _complexBindings;

		// Token: 0x020003AF RID: 943
		private sealed class ExpressionItem
		{
			// Token: 0x060025FA RID: 9722 RVA: 0x000EC4CE File Offset: 0x000EA6CE
			public ExpressionItem(string prefix)
			{
				this._prefix = prefix;
			}

			// Token: 0x060025FB RID: 9723 RVA: 0x000EC4DD File Offset: 0x000EA6DD
			public override string ToString()
			{
				return this._prefix;
			}

			// Token: 0x04001BAB RID: 7083
			private string _prefix;
		}

		// Token: 0x020003B0 RID: 944
		private sealed class BindablePropertyNode : TreeNode
		{
			// Token: 0x060025FC RID: 9724 RVA: 0x000EC4E8 File Offset: 0x000EA6E8
			public BindablePropertyNode(PropertyDescriptor propDesc, ExpressionBinding binding)
			{
				this._binding = binding;
				this._propDesc = propDesc;
				this._isValid = true;
				base.Text = propDesc.Name;
				base.ImageIndex = (base.SelectedImageIndex = (this.IsBound ? (this.IsGenerated ? 2 : 1) : 0));
			}

			// Token: 0x170007FE RID: 2046
			// (get) Token: 0x060025FD RID: 9725 RVA: 0x000EC542 File Offset: 0x000EA742
			public bool IsBound
			{
				get
				{
					return this._binding != null;
				}
			}

			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x060025FE RID: 9726 RVA: 0x000EC54D File Offset: 0x000EA74D
			public bool IsGenerated
			{
				get
				{
					return this._binding != null && this._binding.Generated;
				}
			}

			// Token: 0x17000800 RID: 2048
			// (get) Token: 0x060025FF RID: 9727 RVA: 0x000EC564 File Offset: 0x000EA764
			// (set) Token: 0x06002600 RID: 9728 RVA: 0x000EC56C File Offset: 0x000EA76C
			public bool IsValid
			{
				get
				{
					return this._isValid;
				}
				set
				{
					this._isValid = value;
				}
			}

			// Token: 0x17000801 RID: 2049
			// (get) Token: 0x06002601 RID: 9729 RVA: 0x000EC575 File Offset: 0x000EA775
			// (set) Token: 0x06002602 RID: 9730 RVA: 0x000EC580 File Offset: 0x000EA780
			public ExpressionBinding Binding
			{
				get
				{
					return this._binding;
				}
				set
				{
					this._binding = value;
					base.ImageIndex = (base.SelectedImageIndex = (this.IsBound ? 1 : 0));
				}
			}

			// Token: 0x17000802 RID: 2050
			// (get) Token: 0x06002603 RID: 9731 RVA: 0x000EC5AF File Offset: 0x000EA7AF
			public PropertyDescriptor PropertyDescriptor
			{
				get
				{
					return this._propDesc;
				}
			}

			// Token: 0x04001BAC RID: 7084
			private PropertyDescriptor _propDesc;

			// Token: 0x04001BAD RID: 7085
			private ExpressionBinding _binding;

			// Token: 0x04001BAE RID: 7086
			private bool _isValid;
		}

		// Token: 0x020003B1 RID: 945
		private sealed class GenericExpressionEditor : ExpressionEditor
		{
			// Token: 0x06002604 RID: 9732 RVA: 0x00003930 File Offset: 0x00001B30
			public override object EvaluateExpression(string expression, object parsedExpressionData, Type propertyType, IServiceProvider serviceProvider)
			{
				return string.Empty;
			}
		}
	}
}
