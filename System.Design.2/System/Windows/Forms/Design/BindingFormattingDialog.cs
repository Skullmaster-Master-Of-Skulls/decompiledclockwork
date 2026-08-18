using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200029A RID: 666
	internal partial class BindingFormattingDialog : Form
	{
		// Token: 0x06001996 RID: 6550 RVA: 0x000919A0 File Offset: 0x0008FBA0
		public BindingFormattingDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x170005A9 RID: 1449
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x000919AE File Offset: 0x0008FBAE
		public ControlBindingsCollection Bindings
		{
			set
			{
				this.bindings = value;
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000919B8 File Offset: 0x0008FBB8
		private static Bitmap ScaleBitmapIfNeeded(string resourceName)
		{
			Bitmap bitmap = new Bitmap(BitmapSelector.GetResourceStream(typeof(BindingFormattingDialog), resourceName));
			bitmap.MakeTransparent(Color.Red);
			if (DpiHelper.IsScalingRequired)
			{
				int num = bitmap.Size.Height;
				num = Math.Min(256, DpiHelper.LogicalToDeviceUnitsY(num));
				int num2 = bitmap.Size.Width;
				num2 = Math.Min(256, DpiHelper.LogicalToDeviceUnitsX(num2));
				Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, new Size(num2, num));
				if (bitmap2 != null)
				{
					bitmap.Dispose();
					bitmap = bitmap2;
				}
			}
			return bitmap;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x00091A49 File Offset: 0x0008FC49
		private static Bitmap BoundBitmap
		{
			get
			{
				if (BindingFormattingDialog.boundBitmap == null)
				{
					BindingFormattingDialog.boundBitmap = BindingFormattingDialog.ScaleBitmapIfNeeded("BindingFormattingDialog.Bound.bmp");
				}
				return BindingFormattingDialog.boundBitmap;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00091A66 File Offset: 0x0008FC66
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x00091A6E File Offset: 0x0008FC6E
		public ITypeDescriptorContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
				this.dataSourcePicker.Context = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x00091A83 File Offset: 0x0008FC83
		public bool Dirty
		{
			get
			{
				return this.dirty || this.formatControl1.Dirty;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x00091A9A File Offset: 0x0008FC9A
		public IDesignerHost Host
		{
			set
			{
				this.host = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x00091AA3 File Offset: 0x0008FCA3
		private static Bitmap UnboundBitmap
		{
			get
			{
				if (BindingFormattingDialog.unboundBitmap == null)
				{
					BindingFormattingDialog.unboundBitmap = BindingFormattingDialog.ScaleBitmapIfNeeded("BindingFormattingDialog.Unbound.bmp");
				}
				return BindingFormattingDialog.unboundBitmap;
			}
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00091AC0 File Offset: 0x0008FCC0
		private void BindingFormattingDialog_Closing(object sender, CancelEventArgs e)
		{
			this.currentBindingTreeNode = null;
			this.dataSourcePicker.OwnerComponent = null;
			this.formatControl1.ResetFormattingInfo();
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00091AE0 File Offset: 0x0008FCE0
		private void BindingFormattingDialog_HelpRequested(object sender, HelpEventArgs e)
		{
			this.BindingFormattingDialog_HelpRequestHandled();
			e.Handled = true;
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00091AEF File Offset: 0x0008FCEF
		private void BindingFormattingDialog_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			this.BindingFormattingDialog_HelpRequestHandled();
			e.Cancel = true;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00091B00 File Offset: 0x0008FD00
		private void BindingFormattingDialog_HelpRequestHandled()
		{
			IHelpService helpService = this.context.GetService(typeof(IHelpService)) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("vs.BindingFormattingDialog");
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00091B38 File Offset: 0x0008FD38
		private void BindingFormattingDialog_Load(object sender, EventArgs e)
		{
			this.inLoad = true;
			try
			{
				this.dirty = false;
				Font font = Control.DefaultFont;
				IUIService iuiservice = null;
				if (this.bindings.BindableComponent.Site != null)
				{
					iuiservice = (IUIService)this.bindings.BindableComponent.Site.GetService(typeof(IUIService));
				}
				if (iuiservice != null)
				{
					font = (Font)iuiservice.Styles["DialogFont"];
				}
				this.Font = font;
				DesignerUtils.ApplyTreeViewThemeStyles(this.propertiesTreeView);
				if (this.propertiesTreeView.ImageList == null)
				{
					ImageList imageList = new ImageList();
					imageList.Images.Add(BindingFormattingDialog.BoundBitmap);
					imageList.Images.Add(BindingFormattingDialog.UnboundBitmap);
					if (DpiHelper.IsScalingRequired)
					{
						imageList.ImageSize = BindingFormattingDialog.BoundBitmap.Size;
					}
					this.propertiesTreeView.ImageList = imageList;
				}
				BindingFormattingDialog.BindingTreeNode bindingTreeNode = null;
				BindingFormattingDialog.BindingTreeNode bindingTreeNode2 = null;
				string text = null;
				string text2 = null;
				AttributeCollection attributes = TypeDescriptor.GetAttributes(this.bindings.BindableComponent);
				foreach (object obj in attributes)
				{
					Attribute attribute = (Attribute)obj;
					if (attribute is DefaultBindingPropertyAttribute)
					{
						text = ((DefaultBindingPropertyAttribute)attribute).Name;
						break;
					}
					if (attribute is DefaultPropertyAttribute)
					{
						text2 = ((DefaultPropertyAttribute)attribute).Name;
					}
				}
				this.propertiesTreeView.Nodes.Clear();
				TreeNode treeNode = new TreeNode(SR.GetString("BindingFormattingDialogCommonTreeNode"));
				TreeNode treeNode2 = new TreeNode(SR.GetString("BindingFormattingDialogAllTreeNode"));
				this.propertiesTreeView.Nodes.Add(treeNode);
				this.propertiesTreeView.Nodes.Add(treeNode2);
				IBindableComponent bindableComponent = this.bindings.BindableComponent;
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(bindableComponent);
				for (int i = 0; i < properties.Count; i++)
				{
					if (!properties[i].IsReadOnly)
					{
						BindableAttribute bindableAttribute = (BindableAttribute)properties[i].Attributes[typeof(BindableAttribute)];
						BrowsableAttribute browsableAttribute = (BrowsableAttribute)properties[i].Attributes[typeof(BrowsableAttribute)];
						if (browsableAttribute == null || browsableAttribute.Browsable || (bindableAttribute != null && bindableAttribute.Bindable))
						{
							BindingFormattingDialog.BindingTreeNode bindingTreeNode3 = new BindingFormattingDialog.BindingTreeNode(properties[i].Name);
							bindingTreeNode3.Binding = this.FindBinding(properties[i].Name);
							if (bindingTreeNode3.Binding != null)
							{
								bindingTreeNode3.FormatType = FormatControl.FormatTypeStringFromFormatString(bindingTreeNode3.Binding.FormatString);
							}
							else
							{
								bindingTreeNode3.FormatType = SR.GetString("BindingFormattingDialogFormatTypeNoFormatting");
							}
							if (bindableAttribute != null && bindableAttribute.Bindable)
							{
								treeNode.Nodes.Add(bindingTreeNode3);
							}
							else
							{
								treeNode2.Nodes.Add(bindingTreeNode3);
							}
							if (bindingTreeNode == null && !string.IsNullOrEmpty(text) && string.Compare(properties[i].Name, text, false, CultureInfo.CurrentCulture) == 0)
							{
								bindingTreeNode = bindingTreeNode3;
							}
							else if (bindingTreeNode2 == null && !string.IsNullOrEmpty(text2) && string.Compare(properties[i].Name, text2, false, CultureInfo.CurrentCulture) == 0)
							{
								bindingTreeNode2 = bindingTreeNode3;
							}
						}
					}
				}
				treeNode.Expand();
				treeNode2.Expand();
				this.propertiesTreeView.Sort();
				BindingFormattingDialog.BindingTreeNode bindingTreeNode4;
				if (bindingTreeNode != null)
				{
					bindingTreeNode4 = bindingTreeNode;
				}
				else if (bindingTreeNode2 != null)
				{
					bindingTreeNode4 = bindingTreeNode2;
				}
				else if (treeNode.Nodes.Count > 0)
				{
					bindingTreeNode4 = (BindingFormattingDialog.FirstNodeInAlphabeticalOrder(treeNode.Nodes) as BindingFormattingDialog.BindingTreeNode);
				}
				else if (treeNode2.Nodes.Count > 0)
				{
					bindingTreeNode4 = (BindingFormattingDialog.FirstNodeInAlphabeticalOrder(treeNode2.Nodes) as BindingFormattingDialog.BindingTreeNode);
				}
				else
				{
					bindingTreeNode4 = null;
				}
				this.propertiesTreeView.SelectedNode = bindingTreeNode4;
				if (bindingTreeNode4 != null)
				{
					bindingTreeNode4.EnsureVisible();
				}
				this.dataSourcePicker.PropertyName = bindingTreeNode4.Text;
				this.dataSourcePicker.Binding = ((bindingTreeNode4 != null) ? bindingTreeNode4.Binding : null);
				this.dataSourcePicker.Enabled = true;
				this.dataSourcePicker.OwnerComponent = this.bindings.BindableComponent;
				this.dataSourcePicker.DefaultDataSourceUpdateMode = this.bindings.DefaultDataSourceUpdateMode;
				if (bindingTreeNode4 != null && bindingTreeNode4.Binding != null)
				{
					this.bindingUpdateDropDown.Enabled = true;
					this.bindingUpdateDropDown.SelectedItem = bindingTreeNode4.Binding.DataSourceUpdateMode;
					this.updateModeLabel.Enabled = true;
					this.formatControl1.Enabled = true;
					this.formatControl1.FormatType = bindingTreeNode4.FormatType;
					FormatControl.FormatTypeClass formatTypeItem = this.formatControl1.FormatTypeItem;
					formatTypeItem.PushFormatStringIntoFormatType(bindingTreeNode4.Binding.FormatString);
					if (bindingTreeNode4.Binding.NullValue != null)
					{
						this.formatControl1.NullValue = bindingTreeNode4.Binding.NullValue.ToString();
					}
					else
					{
						this.formatControl1.NullValue = string.Empty;
					}
				}
				else
				{
					this.bindingUpdateDropDown.Enabled = false;
					this.bindingUpdateDropDown.SelectedItem = this.bindings.DefaultDataSourceUpdateMode;
					this.updateModeLabel.Enabled = false;
					this.formatControl1.Enabled = false;
					this.formatControl1.FormatType = string.Empty;
				}
				this.formatControl1.Dirty = false;
				this.currentBindingTreeNode = (this.propertiesTreeView.SelectedNode as BindingFormattingDialog.BindingTreeNode);
			}
			finally
			{
				this.inLoad = false;
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000920F0 File Offset: 0x000902F0
		private Binding FindBinding(string propertyName)
		{
			for (int i = 0; i < this.bindings.Count; i++)
			{
				if (string.Equals(propertyName, this.bindings[i].PropertyName, StringComparison.OrdinalIgnoreCase))
				{
					return this.bindings[i];
				}
			}
			return null;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0009213C File Offset: 0x0009033C
		private static TreeNode FirstNodeInAlphabeticalOrder(TreeNodeCollection nodes)
		{
			if (nodes.Count == 0)
			{
				return null;
			}
			TreeNode treeNode = nodes[0];
			for (int i = 1; i < nodes.Count; i++)
			{
				if (string.Compare(treeNode.Text, nodes[i].Text, false, CultureInfo.CurrentCulture) > 0)
				{
					treeNode = nodes[i];
				}
			}
			return treeNode;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00092872 File Offset: 0x00090A72
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.dirty = false;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0009287C File Offset: 0x00090A7C
		private void ConsolidateBindingInformation()
		{
			Binding binding = this.dataSourcePicker.Binding;
			if (binding == null)
			{
				return;
			}
			binding.FormattingEnabled = true;
			this.currentBindingTreeNode.Binding = binding;
			this.currentBindingTreeNode.FormatType = this.formatControl1.FormatType;
			FormatControl.FormatTypeClass formatTypeItem = this.formatControl1.FormatTypeItem;
			if (formatTypeItem != null)
			{
				binding.FormatString = formatTypeItem.FormatString;
				binding.NullValue = this.formatControl1.NullValue;
			}
			binding.DataSourceUpdateMode = (DataSourceUpdateMode)this.bindingUpdateDropDown.SelectedItem;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00092904 File Offset: 0x00090B04
		private void dataSourcePicker_PropertyValueChanged(object sender, EventArgs e)
		{
			if (this.inLoad)
			{
				return;
			}
			BindingFormattingDialog.BindingTreeNode bindingTreeNode = this.propertiesTreeView.SelectedNode as BindingFormattingDialog.BindingTreeNode;
			if (this.dataSourcePicker.Binding == bindingTreeNode.Binding)
			{
				return;
			}
			Binding binding = this.dataSourcePicker.Binding;
			if (binding != null)
			{
				binding.FormattingEnabled = true;
				Binding binding2 = bindingTreeNode.Binding;
				if (binding2 != null)
				{
					binding.FormatString = binding2.FormatString;
					binding.NullValue = binding2.NullValue;
					binding.FormatInfo = binding2.FormatInfo;
				}
			}
			bindingTreeNode.Binding = binding;
			if (binding != null)
			{
				this.formatControl1.Enabled = true;
				this.updateModeLabel.Enabled = true;
				this.bindingUpdateDropDown.Enabled = true;
				this.bindingUpdateDropDown.SelectedItem = binding.DataSourceUpdateMode;
				if (!string.IsNullOrEmpty(this.formatControl1.FormatType))
				{
					this.formatControl1.FormatType = this.formatControl1.FormatType;
				}
				else
				{
					this.formatControl1.FormatType = SR.GetString("BindingFormattingDialogFormatTypeNoFormatting");
				}
			}
			else
			{
				this.formatControl1.Enabled = false;
				this.updateModeLabel.Enabled = false;
				this.bindingUpdateDropDown.Enabled = false;
				this.bindingUpdateDropDown.SelectedItem = this.bindings.DefaultDataSourceUpdateMode;
				this.formatControl1.FormatType = SR.GetString("BindingFormattingDialogFormatTypeNoFormatting");
			}
			this.dirty = true;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00092A63 File Offset: 0x00090C63
		private void okButton_Click(object sender, EventArgs e)
		{
			if (this.currentBindingTreeNode != null)
			{
				this.ConsolidateBindingInformation();
			}
			this.PushChanges();
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00092A7C File Offset: 0x00090C7C
		private void propertiesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (this.inLoad)
			{
				return;
			}
			BindingFormattingDialog.BindingTreeNode bindingTreeNode = e.Node as BindingFormattingDialog.BindingTreeNode;
			if (bindingTreeNode == null)
			{
				this.dataSourcePicker.Binding = null;
				this.bindingLabel.Enabled = (this.dataSourcePicker.Enabled = false);
				this.updateModeLabel.Enabled = (this.bindingUpdateDropDown.Enabled = false);
				this.formatControl1.Enabled = false;
				return;
			}
			this.bindingLabel.Enabled = (this.dataSourcePicker.Enabled = true);
			this.dataSourcePicker.PropertyName = bindingTreeNode.Text;
			this.updateModeLabel.Enabled = (this.bindingUpdateDropDown.Enabled = false);
			this.formatControl1.Enabled = false;
			if (bindingTreeNode.Binding != null)
			{
				this.formatControl1.Enabled = true;
				this.formatControl1.FormatType = bindingTreeNode.FormatType;
				FormatControl.FormatTypeClass formatTypeItem = this.formatControl1.FormatTypeItem;
				this.dataSourcePicker.Binding = bindingTreeNode.Binding;
				formatTypeItem.PushFormatStringIntoFormatType(bindingTreeNode.Binding.FormatString);
				if (bindingTreeNode.Binding.NullValue != null)
				{
					this.formatControl1.NullValue = bindingTreeNode.Binding.NullValue.ToString();
				}
				else
				{
					this.formatControl1.NullValue = string.Empty;
				}
				this.bindingUpdateDropDown.SelectedItem = bindingTreeNode.Binding.DataSourceUpdateMode;
				this.updateModeLabel.Enabled = (this.bindingUpdateDropDown.Enabled = true);
			}
			else
			{
				bool flag = this.dirty;
				this.dataSourcePicker.Binding = null;
				this.formatControl1.FormatType = bindingTreeNode.FormatType;
				this.bindingUpdateDropDown.SelectedItem = this.bindings.DefaultDataSourceUpdateMode;
				this.formatControl1.NullValue = null;
				this.dirty = flag;
			}
			this.formatControl1.Dirty = false;
			this.currentBindingTreeNode = bindingTreeNode;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00092C6C File Offset: 0x00090E6C
		private void propertiesTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (this.inLoad)
			{
				return;
			}
			if (this.currentBindingTreeNode == null)
			{
				return;
			}
			if (this.dataSourcePicker.Binding == null)
			{
				return;
			}
			if (!this.formatControl1.Enabled)
			{
				return;
			}
			this.ConsolidateBindingInformation();
			this.dirty = (this.dirty || this.formatControl1.Dirty);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00092CCC File Offset: 0x00090ECC
		private void PushChanges()
		{
			if (!this.Dirty)
			{
				return;
			}
			IComponentChangeService componentChangeService = this.host.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			PropertyDescriptor propertyDescriptor = null;
			IBindableComponent bindableComponent = this.bindings.BindableComponent;
			if (componentChangeService != null && bindableComponent != null)
			{
				propertyDescriptor = TypeDescriptor.GetProperties(bindableComponent)["DataBindings"];
				if (propertyDescriptor != null)
				{
					componentChangeService.OnComponentChanging(bindableComponent, propertyDescriptor);
				}
			}
			this.bindings.Clear();
			TreeNode treeNode = this.propertiesTreeView.Nodes[0];
			for (int i = 0; i < treeNode.Nodes.Count; i++)
			{
				BindingFormattingDialog.BindingTreeNode bindingTreeNode = treeNode.Nodes[i] as BindingFormattingDialog.BindingTreeNode;
				if (bindingTreeNode.Binding != null)
				{
					this.bindings.Add(bindingTreeNode.Binding);
				}
			}
			TreeNode treeNode2 = this.propertiesTreeView.Nodes[1];
			for (int j = 0; j < treeNode2.Nodes.Count; j++)
			{
				BindingFormattingDialog.BindingTreeNode bindingTreeNode2 = treeNode2.Nodes[j] as BindingFormattingDialog.BindingTreeNode;
				if (bindingTreeNode2.Binding != null)
				{
					this.bindings.Add(bindingTreeNode2.Binding);
				}
			}
			if (componentChangeService != null && bindableComponent != null && propertyDescriptor != null)
			{
				componentChangeService.OnComponentChanged(bindableComponent, propertyDescriptor, null, null);
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00092E04 File Offset: 0x00091004
		private void bindingUpdateDropDown_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.inLoad)
			{
				return;
			}
			this.dirty = true;
		}

		// Token: 0x0400159F RID: 5535
		private ITypeDescriptorContext context;

		// Token: 0x040015A0 RID: 5536
		private ControlBindingsCollection bindings;

		// Token: 0x040015AD RID: 5549
		private bool inLoad;

		// Token: 0x040015AE RID: 5550
		private bool dirty;

		// Token: 0x040015AF RID: 5551
		private const int BOUNDIMAGEINDEX = 0;

		// Token: 0x040015B0 RID: 5552
		private const int UNBOUNDIMAGEINDEX = 1;

		// Token: 0x040015B1 RID: 5553
		private static Bitmap boundBitmap;

		// Token: 0x040015B2 RID: 5554
		private static Bitmap unboundBitmap;

		// Token: 0x040015B3 RID: 5555
		private BindingFormattingDialog.BindingTreeNode currentBindingTreeNode;

		// Token: 0x040015B4 RID: 5556
		private IDesignerHost host;

		// Token: 0x0200052C RID: 1324
		private class BindingTreeNode : TreeNode
		{
			// Token: 0x06003048 RID: 12360 RVA: 0x001095C3 File Offset: 0x001077C3
			public BindingTreeNode(string name) : base(name)
			{
			}

			// Token: 0x1700095F RID: 2399
			// (get) Token: 0x06003049 RID: 12361 RVA: 0x001095CC File Offset: 0x001077CC
			// (set) Token: 0x0600304A RID: 12362 RVA: 0x001095D4 File Offset: 0x001077D4
			public Binding Binding
			{
				get
				{
					return this.binding;
				}
				set
				{
					this.binding = value;
					base.ImageIndex = ((this.binding != null) ? 0 : 1);
					base.SelectedImageIndex = ((this.binding != null) ? 0 : 1);
				}
			}

			// Token: 0x17000960 RID: 2400
			// (get) Token: 0x0600304B RID: 12363 RVA: 0x00109601 File Offset: 0x00107801
			// (set) Token: 0x0600304C RID: 12364 RVA: 0x00109609 File Offset: 0x00107809
			public string FormatType
			{
				get
				{
					return this.formatType;
				}
				set
				{
					this.formatType = value;
				}
			}

			// Token: 0x040020D5 RID: 8405
			private Binding binding;

			// Token: 0x040020D6 RID: 8406
			private string formatType;
		}

		// Token: 0x0200052D RID: 1325
		private class TreeNodeComparer : IComparer
		{
			// Token: 0x0600304E RID: 12366 RVA: 0x00109614 File Offset: 0x00107814
			int IComparer.Compare(object o1, object o2)
			{
				TreeNode treeNode = o1 as TreeNode;
				TreeNode treeNode2 = o2 as TreeNode;
				BindingFormattingDialog.BindingTreeNode bindingTreeNode = treeNode as BindingFormattingDialog.BindingTreeNode;
				BindingFormattingDialog.BindingTreeNode bindingTreeNode2 = treeNode2 as BindingFormattingDialog.BindingTreeNode;
				if (bindingTreeNode != null)
				{
					return string.Compare(bindingTreeNode.Text, bindingTreeNode2.Text, false, CultureInfo.CurrentCulture);
				}
				if (string.Compare(treeNode.Text, SR.GetString("BindingFormattingDialogAllTreeNode"), false, CultureInfo.CurrentCulture) == 0)
				{
					if (string.Compare(treeNode2.Text, SR.GetString("BindingFormattingDialogAllTreeNode"), false, CultureInfo.CurrentCulture) == 0)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					if (string.Compare(treeNode2.Text, SR.GetString("BindingFormattingDialogCommonTreeNode"), false, CultureInfo.CurrentCulture) == 0)
					{
						return 0;
					}
					return -1;
				}
			}
		}
	}
}
