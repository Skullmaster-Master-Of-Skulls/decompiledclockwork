using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Text;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200034E RID: 846
	internal class ToolStripCollectionEditor : CollectionEditor
	{
		// Token: 0x0600216F RID: 8559 RVA: 0x000CBCE0 File Offset: 0x000C9EE0
		public ToolStripCollectionEditor() : base(typeof(ToolStripItemCollection))
		{
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000CBCF2 File Offset: 0x000C9EF2
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			return new ToolStripCollectionEditor.ToolStripItemEditorForm(this);
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x000CBCFA File Offset: 0x000C9EFA
		protected override string HelpTopic
		{
			get
			{
				return "net.ComponentModel.ToolStripCollectionEditor";
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000CBD04 File Offset: 0x000C9F04
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			ToolStripDesigner toolStripDesigner = null;
			if (provider != null)
			{
				ISelectionService selectionService = (ISelectionService)provider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					object obj = selectionService.PrimarySelection;
					if (obj is ToolStripDropDownItem)
					{
						obj = ((ToolStripDropDownItem)obj).Owner;
					}
					if (obj is ToolStrip)
					{
						IDesignerHost designerHost = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
						if (designerHost != null)
						{
							toolStripDesigner = (designerHost.GetDesigner((IComponent)obj) as ToolStripDesigner);
						}
					}
				}
			}
			object result;
			try
			{
				if (toolStripDesigner != null)
				{
					toolStripDesigner.EditingCollection = true;
				}
				result = base.EditValue(context, provider, value);
			}
			finally
			{
				if (toolStripDesigner != null)
				{
					toolStripDesigner.EditingCollection = false;
				}
			}
			return result;
		}

		// Token: 0x02000596 RID: 1430
		protected class ToolStripItemEditorForm : CollectionEditor.CollectionForm
		{
			// Token: 0x06003329 RID: 13097 RVA: 0x001167A0 File Offset: 0x001149A0
			internal ToolStripItemEditorForm(CollectionEditor parent) : base(parent)
			{
				if (!ToolStripCollectionEditor.ToolStripItemEditorForm.isScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						ToolStripCollectionEditor.ToolStripItemEditorForm.iconHeight = DpiHelper.LogicalToDeviceUnitsY(16);
						ToolStripCollectionEditor.ToolStripItemEditorForm.iconWidth = DpiHelper.LogicalToDeviceUnitsX(16);
						ToolStripCollectionEditor.ToolStripItemEditorForm.separatorHeight = DpiHelper.LogicalToDeviceUnitsY(4);
						ToolStripCollectionEditor.ToolStripItemEditorForm.textImageSpacing = DpiHelper.LogicalToDeviceUnitsX(6);
						ToolStripCollectionEditor.ToolStripItemEditorForm.indentSpacing = DpiHelper.LogicalToDeviceUnitsX(4);
						ToolStripCollectionEditor.ToolStripItemEditorForm.imagePaddingX = DpiHelper.LogicalToDeviceUnitsX(1);
						ToolStripCollectionEditor.ToolStripItemEditorForm.imagePaddingY = DpiHelper.LogicalToDeviceUnitsY(1);
					}
					ToolStripCollectionEditor.ToolStripItemEditorForm.isScalingInitialized = true;
				}
				this.editor = (ToolStripCollectionEditor)parent;
				this.InitializeComponent();
				if (DpiHelper.IsScalingRequired)
				{
					DpiHelper.ScaleButtonImageLogicalToDevice(this.btnMoveUp);
					DpiHelper.ScaleButtonImageLogicalToDevice(this.btnMoveDown);
					DpiHelper.ScaleButtonImageLogicalToDevice(this.btnRemove);
				}
				base.ActiveControl = this.listBoxItems;
				this._originalText = this.Text;
				base.SetStyle(ControlStyles.ResizeRedraw, true);
			}

			// Token: 0x17000A00 RID: 2560
			// (set) Token: 0x0600332A RID: 13098 RVA: 0x0011687C File Offset: 0x00114A7C
			internal ToolStripItemCollection Collection
			{
				set
				{
					if (value != this._targetToolStripCollection)
					{
						if (this._itemList != null)
						{
							this._itemList.Clear();
						}
						if (value != null)
						{
							if (base.Context != null)
							{
								this._itemList = new ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection(this, this.listBoxItems.Items, value);
								ToolStrip item = ToolStripCollectionEditor.ToolStripItemEditorForm.ToolStripFromObject(base.Context.Instance);
								this._itemList.Add(item);
								ToolStripItem toolStripItem = base.Context.Instance as ToolStripItem;
								if (toolStripItem != null && toolStripItem.Site != null)
								{
									this.Text = string.Concat(new string[]
									{
										this._originalText,
										" (",
										toolStripItem.Site.Name,
										".",
										base.Context.PropertyDescriptor.Name,
										")"
									});
								}
								foreach (object obj in value)
								{
									ToolStripItem toolStripItem2 = (ToolStripItem)obj;
									if (!(toolStripItem2 is DesignerToolStripControlHost))
									{
										this._itemList.Add(toolStripItem2);
									}
								}
								IComponentChangeService componentChangeService = (IComponentChangeService)base.Context.GetService(typeof(IComponentChangeService));
								if (componentChangeService != null)
								{
									componentChangeService.ComponentChanged += this.OnComponentChanged;
								}
								this.selectedItemProps.Site = new CollectionEditor.PropertyGridSite(base.Context, this.selectedItemProps);
							}
						}
						else
						{
							if (this._componentChangeSvc != null)
							{
								this._componentChangeSvc.ComponentChanged -= this.OnComponentChanged;
							}
							this._componentChangeSvc = null;
							this.selectedItemProps.Site = null;
						}
						this._targetToolStripCollection = value;
					}
				}
			}

			// Token: 0x17000A01 RID: 2561
			// (get) Token: 0x0600332B RID: 13099 RVA: 0x00116A40 File Offset: 0x00114C40
			private IComponentChangeService ComponentChangeService
			{
				get
				{
					if (this._componentChangeSvc == null && base.Context != null)
					{
						this._componentChangeSvc = (IComponentChangeService)base.Context.GetService(typeof(IComponentChangeService));
					}
					return this._componentChangeSvc;
				}
			}

			// Token: 0x0600332C RID: 13100 RVA: 0x00116A78 File Offset: 0x00114C78
			private void InitializeComponent()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolStripCollectionEditor.ToolStripItemEditorForm));
				this.btnCancel = new Button();
				this.btnOK = new Button();
				this.tableLayoutPanel = new TableLayoutPanel();
				this.addTableLayoutPanel = new TableLayoutPanel();
				this.btnAddNew = new Button();
				this.newItemTypes = new ToolStripCollectionEditor.ToolStripItemEditorForm.ImageComboBox();
				this.okCancelTableLayoutPanel = new TableLayoutPanel();
				this.lblItems = new Label();
				this.selectedItemName = new Label();
				this.selectedItemProps = new VsPropertyGrid(base.Context);
				this.lblMembers = new Label();
				this.listBoxItems = new CollectionEditor.FilterListBox();
				this.btnMoveUp = new Button();
				this.btnMoveDown = new Button();
				this.btnRemove = new Button();
				this.tableLayoutPanel.SuspendLayout();
				this.addTableLayoutPanel.SuspendLayout();
				this.okCancelTableLayoutPanel.SuspendLayout();
				base.SuspendLayout();
				componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
				this.btnCancel.DialogResult = DialogResult.Cancel;
				this.btnCancel.Margin = new Padding(3, 0, 0, 0);
				this.btnCancel.Name = "btnCancel";
				componentResourceManager.ApplyResources(this.btnOK, "btnOK");
				this.btnOK.Margin = new Padding(0, 0, 3, 0);
				this.btnOK.Name = "btnOK";
				componentResourceManager.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 274f));
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				this.tableLayoutPanel.Controls.Add(this.addTableLayoutPanel, 0, 1);
				this.tableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 0, 6);
				this.tableLayoutPanel.Controls.Add(this.lblItems, 0, 0);
				this.tableLayoutPanel.Controls.Add(this.selectedItemName, 2, 0);
				this.tableLayoutPanel.Controls.Add(this.selectedItemProps, 2, 1);
				this.tableLayoutPanel.Controls.Add(this.lblMembers, 0, 2);
				this.tableLayoutPanel.Controls.Add(this.listBoxItems, 0, 3);
				this.tableLayoutPanel.Controls.Add(this.btnMoveUp, 1, 3);
				this.tableLayoutPanel.Controls.Add(this.btnMoveDown, 1, 4);
				this.tableLayoutPanel.Controls.Add(this.btnRemove, 1, 5);
				this.tableLayoutPanel.Name = "tableLayoutPanel";
				this.tableLayoutPanel.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
				this.tableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.addTableLayoutPanel, "addTableLayoutPanel");
				this.addTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				this.addTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				this.addTableLayoutPanel.Controls.Add(this.btnAddNew, 1, 0);
				this.addTableLayoutPanel.Controls.Add(this.newItemTypes, 0, 0);
				this.addTableLayoutPanel.Margin = new Padding(0, 3, 3, 3);
				this.addTableLayoutPanel.Name = "addTableLayoutPanel";
				this.addTableLayoutPanel.AutoSize = true;
				this.addTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.btnAddNew, "btnAddNew");
				this.btnAddNew.Margin = new Padding(3, 0, 0, 0);
				this.btnAddNew.Name = "btnAddNew";
				componentResourceManager.ApplyResources(this.newItemTypes, "newItemTypes");
				this.newItemTypes.DropDownStyle = ComboBoxStyle.DropDownList;
				this.newItemTypes.FormattingEnabled = true;
				this.newItemTypes.Margin = new Padding(0, 0, 3, 0);
				this.newItemTypes.Name = "newItemTypes";
				this.newItemTypes.DrawMode = DrawMode.OwnerDrawVariable;
				componentResourceManager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
				this.tableLayoutPanel.SetColumnSpan(this.okCancelTableLayoutPanel, 3);
				this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.okCancelTableLayoutPanel.Controls.Add(this.btnOK, 0, 0);
				this.okCancelTableLayoutPanel.Controls.Add(this.btnCancel, 1, 0);
				this.okCancelTableLayoutPanel.Margin = new Padding(3, 6, 0, 0);
				this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
				this.okCancelTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.lblItems, "lblItems");
				this.lblItems.Margin = new Padding(0, 3, 3, 0);
				this.lblItems.Name = "lblItems";
				componentResourceManager.ApplyResources(this.selectedItemName, "selectedItemName");
				this.selectedItemName.Margin = new Padding(3, 3, 3, 0);
				this.selectedItemName.Name = "selectedItemName";
				this.selectedItemProps.CommandsVisibleIfAvailable = false;
				componentResourceManager.ApplyResources(this.selectedItemProps, "selectedItemProps");
				this.selectedItemProps.Margin = new Padding(3, 3, 0, 3);
				this.selectedItemProps.Name = "selectedItemProps";
				this.tableLayoutPanel.SetRowSpan(this.selectedItemProps, 5);
				componentResourceManager.ApplyResources(this.lblMembers, "lblMembers");
				this.lblMembers.Margin = new Padding(0, 3, 3, 0);
				this.lblMembers.Name = "lblMembers";
				componentResourceManager.ApplyResources(this.listBoxItems, "listBoxItems");
				this.listBoxItems.DrawMode = DrawMode.OwnerDrawVariable;
				this.listBoxItems.FormattingEnabled = true;
				this.listBoxItems.Margin = new Padding(0, 3, 3, 3);
				this.listBoxItems.Name = "listBoxItems";
				this.tableLayoutPanel.SetRowSpan(this.listBoxItems, 3);
				this.listBoxItems.SelectionMode = SelectionMode.MultiExtended;
				componentResourceManager.ApplyResources(this.btnMoveUp, "btnMoveUp");
				this.btnMoveUp.Margin = new Padding(3, 3, 18, 0);
				this.btnMoveUp.Name = "btnMoveUp";
				componentResourceManager.ApplyResources(this.btnMoveDown, "btnMoveDown");
				this.btnMoveDown.Margin = new Padding(3, 1, 18, 3);
				this.btnMoveDown.Name = "btnMoveDown";
				componentResourceManager.ApplyResources(this.btnRemove, "btnRemove");
				this.btnRemove.Margin = new Padding(3, 3, 18, 3);
				this.btnRemove.Name = "btnRemove";
				base.AutoScaleMode = AutoScaleMode.Font;
				base.AcceptButton = this.btnOK;
				componentResourceManager.ApplyResources(this, "$this");
				base.CancelButton = this.btnCancel;
				base.Controls.Add(this.tableLayoutPanel);
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.Name = "ToolStripCollectionEditor";
				base.Padding = new Padding(9);
				base.ShowIcon = false;
				base.ShowInTaskbar = false;
				base.SizeGripStyle = SizeGripStyle.Show;
				this.tableLayoutPanel.ResumeLayout(false);
				this.tableLayoutPanel.PerformLayout();
				this.addTableLayoutPanel.ResumeLayout(false);
				this.addTableLayoutPanel.PerformLayout();
				this.okCancelTableLayoutPanel.ResumeLayout(false);
				this.okCancelTableLayoutPanel.PerformLayout();
				base.ResumeLayout(false);
				base.HelpButtonClicked += this.ToolStripCollectionEditor_HelpButtonClicked;
				this.newItemTypes.DropDown += this.OnnewItemTypes_DropDown;
				this.newItemTypes.HandleCreated += this.OnComboHandleCreated;
				this.newItemTypes.SelectedIndexChanged += this.OnnewItemTypes_SelectedIndexChanged;
				this.btnAddNew.Click += this.OnnewItemTypes_SelectionChangeCommitted;
				this.btnMoveUp.Click += this.OnbtnMoveUp_Click;
				this.btnMoveDown.Click += this.OnbtnMoveDown_Click;
				this.btnRemove.Click += this.OnbtnRemove_Click;
				this.btnOK.Click += this.OnbtnOK_Click;
				this.selectedItemName.Paint += this.OnselectedItemName_Paint;
				this.listBoxItems.SelectedIndexChanged += this.OnlistBoxItems_SelectedIndexChanged;
				this.listBoxItems.DrawItem += this.OnlistBoxItems_DrawItem;
				this.listBoxItems.MeasureItem += this.OnlistBoxItems_MeasureItem;
				this.selectedItemProps.PropertyValueChanged += this.PropertyGrid_propertyValueChanged;
				base.Load += this.OnFormLoad;
			}

			// Token: 0x0600332D RID: 13101 RVA: 0x00117400 File Offset: 0x00115600
			private void OnComboHandleCreated(object sender, EventArgs e)
			{
				this.newItemTypes.HandleCreated -= this.OnComboHandleCreated;
				this.newItemTypes.MeasureItem += this.OnlistBoxItems_MeasureItem;
				this.newItemTypes.DrawItem += this.OnlistBoxItems_DrawItem;
			}

			// Token: 0x0600332E RID: 13102 RVA: 0x00117454 File Offset: 0x00115654
			private void AddItem(ToolStripItem newItem, int index)
			{
				if (index == -1)
				{
					this._itemList.Add(newItem);
				}
				else
				{
					if (index < 0 || index >= this._itemList.Count)
					{
						throw new IndexOutOfRangeException();
					}
					this._itemList.Insert(index, newItem);
				}
				ToolStrip toolStrip = (base.Context != null) ? ToolStripCollectionEditor.ToolStripItemEditorForm.ToolStripFromObject(base.Context.Instance) : null;
				if (toolStrip != null)
				{
					toolStrip.Items.Add(newItem);
				}
				this.listBoxItems.ClearSelected();
				this.listBoxItems.SelectedItem = newItem;
			}

			// Token: 0x0600332F RID: 13103 RVA: 0x001174DB File Offset: 0x001156DB
			private void MoveItem(int fromIndex, int toIndex)
			{
				this._itemList.Move(fromIndex, toIndex);
			}

			// Token: 0x06003330 RID: 13104 RVA: 0x001174EA File Offset: 0x001156EA
			private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
			{
				if (e.Component is ToolStripItem && e.Member is PropertyDescriptor && e.Member.Name == "Name")
				{
					this.lblItems.Invalidate();
				}
			}

			// Token: 0x06003331 RID: 13105 RVA: 0x00117528 File Offset: 0x00115728
			protected override void OnEditValueChanged()
			{
				this.selectedItemProps.SelectedObjects = null;
				this.Collection = (ToolStripItemCollection)base.EditValue;
			}

			// Token: 0x06003332 RID: 13106 RVA: 0x00117548 File Offset: 0x00115748
			private void OnFormLoad(object sender, EventArgs e)
			{
				this.newItemTypes.ItemHeight = Math.Max(ToolStripCollectionEditor.ToolStripItemEditorForm.iconHeight, this.Font.Height);
				Component component = base.Context.Instance as Component;
				if (component != null)
				{
					Type[] array = ToolStripDesignerUtils.GetStandardItemTypes(component);
					this.newItemTypes.Items.Clear();
					foreach (Type t in array)
					{
						this.newItemTypes.Items.Add(new ToolStripCollectionEditor.ToolStripItemEditorForm.TypeListItem(t));
					}
					this.newItemTypes.SelectedIndex = 0;
					this.customItemIndex = -1;
					array = ToolStripDesignerUtils.GetCustomItemTypes(component, component.Site);
					if (array.Length != 0)
					{
						this.customItemIndex = this.newItemTypes.Items.Count;
						foreach (Type t2 in array)
						{
							this.newItemTypes.Items.Add(new ToolStripCollectionEditor.ToolStripItemEditorForm.TypeListItem(t2));
						}
					}
					if (this.listBoxItems.Items.Count > 0)
					{
						this.listBoxItems.SelectedIndex = 0;
					}
				}
			}

			// Token: 0x06003333 RID: 13107 RVA: 0x0011765E File Offset: 0x0011585E
			private void OnbtnOK_Click(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.OK;
			}

			// Token: 0x06003334 RID: 13108 RVA: 0x00117667 File Offset: 0x00115867
			private void ToolStripCollectionEditor_HelpButtonClicked(object sender, CancelEventArgs e)
			{
				e.Cancel = true;
				this.editor.ShowHelp();
			}

			// Token: 0x06003335 RID: 13109 RVA: 0x0011767C File Offset: 0x0011587C
			private void OnbtnRemove_Click(object sender, EventArgs e)
			{
				ToolStripItem[] array = new ToolStripItem[this.listBoxItems.SelectedItems.Count];
				this.listBoxItems.SelectedItems.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					this.RemoveItem(array[i]);
				}
			}

			// Token: 0x06003336 RID: 13110 RVA: 0x001176C8 File Offset: 0x001158C8
			private void OnbtnMoveDown_Click(object sender, EventArgs e)
			{
				ToolStripItem value = (ToolStripItem)this.listBoxItems.SelectedItem;
				int num = this.listBoxItems.Items.IndexOf(value);
				this.MoveItem(num, ++num);
				this.listBoxItems.SelectedIndex = num;
			}

			// Token: 0x06003337 RID: 13111 RVA: 0x00117710 File Offset: 0x00115910
			private void OnbtnMoveUp_Click(object sender, EventArgs e)
			{
				ToolStripItem value = (ToolStripItem)this.listBoxItems.SelectedItem;
				int num = this.listBoxItems.Items.IndexOf(value);
				if (num > 1)
				{
					this.MoveItem(num, --num);
					this.listBoxItems.SelectedIndex = num;
				}
			}

			// Token: 0x06003338 RID: 13112 RVA: 0x0011775C File Offset: 0x0011595C
			private void OnnewItemTypes_DropDown(object sender, EventArgs e)
			{
				if (this.newItemTypes.Tag == null || !(bool)this.newItemTypes.Tag)
				{
					int num = this.newItemTypes.ItemHeight;
					int num2 = 0;
					using (Graphics graphics = this.newItemTypes.CreateGraphics())
					{
						foreach (object obj in this.newItemTypes.Items)
						{
							ToolStripCollectionEditor.ToolStripItemEditorForm.TypeListItem typeListItem = (ToolStripCollectionEditor.ToolStripItemEditorForm.TypeListItem)obj;
							num = (int)Math.Max((float)num, (float)(this.newItemTypes.ItemHeight + 1) + graphics.MeasureString(typeListItem.Type.Name, this.newItemTypes.Font).Width + 5f);
							num2 += this.Font.Height + ToolStripCollectionEditor.ToolStripItemEditorForm.separatorHeight + 2 * ToolStripCollectionEditor.ToolStripItemEditorForm.imagePaddingY;
						}
					}
					this.newItemTypes.DropDownWidth = num;
					this.newItemTypes.DropDownHeight = num2;
					this.newItemTypes.Tag = true;
				}
			}

			// Token: 0x06003339 RID: 13113 RVA: 0x00117894 File Offset: 0x00115A94
			private void OnnewItemTypes_SelectionChangeCommitted(object sender, EventArgs e)
			{
				ToolStripCollectionEditor.ToolStripItemEditorForm.TypeListItem typeListItem = this.newItemTypes.SelectedItem as ToolStripCollectionEditor.ToolStripItemEditorForm.TypeListItem;
				if (typeListItem != null)
				{
					ToolStripItem toolStripItem = (ToolStripItem)base.CreateInstance(typeListItem.Type);
					if (toolStripItem is ToolStripButton || toolStripItem is ToolStripSplitButton || toolStripItem is ToolStripDropDownButton)
					{
						Image image = null;
						try
						{
							image = new Bitmap(typeof(ToolStripButton), "blank.bmp");
						}
						catch (Exception ex)
						{
							if (ClientUtils.IsCriticalException(ex))
							{
								throw;
							}
						}
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(toolStripItem)["Image"];
						if (propertyDescriptor != null && image != null)
						{
							propertyDescriptor.SetValue(toolStripItem, image);
						}
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(toolStripItem)["DisplayStyle"];
						if (propertyDescriptor2 != null)
						{
							propertyDescriptor2.SetValue(toolStripItem, ToolStripItemDisplayStyle.Image);
						}
						PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(toolStripItem)["ImageTransparentColor"];
						if (propertyDescriptor3 != null)
						{
							propertyDescriptor3.SetValue(toolStripItem, Color.Magenta);
						}
					}
					this.AddItem(toolStripItem, -1);
					this.listBoxItems.Focus();
				}
			}

			// Token: 0x0600333A RID: 13114 RVA: 0x0011799C File Offset: 0x00115B9C
			private void OnnewItemTypes_SelectedIndexChanged(object sender, EventArgs e)
			{
				this.newItemTypes.Invalidate();
			}

			// Token: 0x0600333B RID: 13115 RVA: 0x001179AC File Offset: 0x00115BAC
			private void OnlistBoxItems_MeasureItem(object sender, MeasureItemEventArgs e)
			{
				int num = 0;
				if (sender is ComboBox)
				{
					bool flag = e.Index == this.customItemIndex;
					if (e.Index >= 0 && flag)
					{
						num = ToolStripCollectionEditor.ToolStripItemEditorForm.separatorHeight;
					}
				}
				Font font = this.Font;
				e.ItemHeight = Math.Max(ToolStripCollectionEditor.ToolStripItemEditorForm.iconHeight + num, font.Height + num) + 2 * ToolStripCollectionEditor.ToolStripItemEditorForm.imagePaddingY;
			}

			// Token: 0x0600333C RID: 13116 RVA: 0x00117A14 File Offset: 0x00115C14
			private void OnlistBoxItems_DrawItem(object sender, DrawItemEventArgs e)
			{
				if (e.Index == -1)
				{
					return;
				}
				bool flag = false;
				bool flag2 = false;
				bool flag3 = (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit;
				Type type;
				string text;
				if (sender is ListBox)
				{
					ListBox listBox = sender as ListBox;
					Component component = listBox.Items[e.Index] as Component;
					if (component == null)
					{
						return;
					}
					if (component is ToolStripItem)
					{
						flag = true;
					}
					type = component.GetType();
					text = ((component.Site != null) ? component.Site.Name : type.Name);
				}
				else
				{
					if (!(sender is ComboBox))
					{
						return;
					}
					flag2 = (e.Index == this.customItemIndex && !flag3);
					ToolStripCollectionEditor.ToolStripItemEditorForm.TypeListItem typeListItem = ((ComboBox)sender).Items[e.Index] as ToolStripCollectionEditor.ToolStripItemEditorForm.TypeListItem;
					if (typeListItem == null)
					{
						return;
					}
					type = typeListItem.Type;
					text = typeListItem.ToString();
				}
				if (type != null)
				{
					Color foreColor = Color.Empty;
					if (flag2)
					{
						e.Graphics.DrawLine(SystemPens.ControlDark, e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Right - 2, e.Bounds.Y + 2);
					}
					Rectangle bounds = e.Bounds;
					bounds.Size = new Size(ToolStripCollectionEditor.ToolStripItemEditorForm.iconWidth, ToolStripCollectionEditor.ToolStripItemEditorForm.iconHeight);
					int x = flag3 ? 0 : (ToolStripCollectionEditor.ToolStripItemEditorForm.imagePaddingX * 2);
					bounds.Offset(x, ToolStripCollectionEditor.ToolStripItemEditorForm.imagePaddingX);
					if (flag2)
					{
						bounds.Offset(0, ToolStripCollectionEditor.ToolStripItemEditorForm.separatorHeight);
					}
					if (flag)
					{
						bounds.X += ToolStripCollectionEditor.ToolStripItemEditorForm.iconWidth + ToolStripCollectionEditor.ToolStripItemEditorForm.indentSpacing;
					}
					if (!flag3)
					{
						bounds.Intersect(e.Bounds);
					}
					Bitmap toolboxBitmap = ToolStripDesignerUtils.GetToolboxBitmap(type);
					if (toolboxBitmap != null)
					{
						if (flag3)
						{
							e.Graphics.DrawImage(toolboxBitmap, e.Bounds.X, e.Bounds.Y, ToolStripCollectionEditor.ToolStripItemEditorForm.iconWidth, ToolStripCollectionEditor.ToolStripItemEditorForm.iconHeight);
						}
						else
						{
							e.Graphics.FillRectangle(SystemBrushes.Window, bounds);
							e.Graphics.DrawImage(toolboxBitmap, bounds);
						}
					}
					Rectangle bounds2 = e.Bounds;
					bounds2.X = bounds.Right + ToolStripCollectionEditor.ToolStripItemEditorForm.textImageSpacing;
					bounds2.Y = bounds.Top - ToolStripCollectionEditor.ToolStripItemEditorForm.imagePaddingY;
					if (!flag3)
					{
						bounds2.Y += ToolStripCollectionEditor.ToolStripItemEditorForm.imagePaddingY * 2;
					}
					bounds2.Intersect(e.Bounds);
					Rectangle bounds3 = e.Bounds;
					bounds3.X = bounds2.X - 2;
					if (flag2)
					{
						bounds3.Y += ToolStripCollectionEditor.ToolStripItemEditorForm.separatorHeight;
						bounds3.Height -= ToolStripCollectionEditor.ToolStripItemEditorForm.separatorHeight;
					}
					if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
					{
						foreColor = SystemColors.HighlightText;
						e.Graphics.FillRectangle(SystemBrushes.Highlight, bounds3);
					}
					else
					{
						foreColor = SystemColors.WindowText;
						e.Graphics.FillRectangle(SystemBrushes.Window, bounds3);
					}
					if (!string.IsNullOrEmpty(text))
					{
						TextFormatFlags flags = TextFormatFlags.Default;
						TextRenderer.DrawText(e.Graphics, text, this.Font, bounds2, foreColor, flags);
					}
					if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
					{
						bounds3.Width--;
						ControlPaint.DrawFocusRectangle(e.Graphics, bounds3, e.ForeColor, e.BackColor);
					}
				}
			}

			// Token: 0x0600333D RID: 13117 RVA: 0x00117D78 File Offset: 0x00115F78
			private void OnlistBoxItems_SelectedIndexChanged(object sender, EventArgs e)
			{
				object[] array = new object[this.listBoxItems.SelectedItems.Count];
				if (array.Length != 0)
				{
					this.listBoxItems.SelectedItems.CopyTo(array, 0);
				}
				if (array.Length == 1 && array[0] is ToolStrip)
				{
					ToolStrip toolStrip = array[0] as ToolStrip;
					if (toolStrip != null && toolStrip.Site != null)
					{
						if (this.toolStripCustomTypeDescriptor == null)
						{
							this.toolStripCustomTypeDescriptor = new ToolStripCustomTypeDescriptor((ToolStrip)array[0]);
						}
						this.selectedItemProps.SelectedObjects = new object[]
						{
							this.toolStripCustomTypeDescriptor
						};
					}
					else
					{
						this.selectedItemProps.SelectedObjects = null;
					}
				}
				else
				{
					this.selectedItemProps.SelectedObjects = array;
				}
				this.btnMoveUp.Enabled = (this.listBoxItems.SelectedItems.Count == 1 && this.listBoxItems.SelectedIndex > 1);
				this.btnMoveDown.Enabled = (this.listBoxItems.SelectedItems.Count == 1 && this.listBoxItems.SelectedIndex < this.listBoxItems.Items.Count - 1);
				this.btnRemove.Enabled = (array.Length != 0);
				foreach (object obj in this.listBoxItems.SelectedItems)
				{
					if (obj is ToolStrip)
					{
						this.btnRemove.Enabled = (this.btnMoveUp.Enabled = (this.btnMoveDown.Enabled = false));
						break;
					}
				}
				this.listBoxItems.Invalidate();
				this.selectedItemName.Invalidate();
			}

			// Token: 0x0600333E RID: 13118 RVA: 0x00117F3C File Offset: 0x0011613C
			private void PropertyGrid_propertyValueChanged(object sender, PropertyValueChangedEventArgs e)
			{
				this.listBoxItems.Invalidate();
				this.selectedItemName.Invalidate();
			}

			// Token: 0x0600333F RID: 13119 RVA: 0x00117F54 File Offset: 0x00116154
			private void OnselectedItemName_Paint(object sender, PaintEventArgs e)
			{
				using (Font font = new Font(this.selectedItemName.Font, FontStyle.Bold))
				{
					Label label = sender as Label;
					Rectangle clientRectangle = label.ClientRectangle;
					bool flag = label.RightToLeft == RightToLeft.Yes;
					StringFormat stringFormat;
					if (flag)
					{
						stringFormat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
					}
					else
					{
						stringFormat = new StringFormat();
					}
					stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
					int count = this.listBoxItems.SelectedItems.Count;
					if (count != 0)
					{
						if (count == 1)
						{
							Component component;
							if (this.listBoxItems.SelectedItem is ToolStrip)
							{
								component = (ToolStrip)this.listBoxItems.SelectedItem;
							}
							else
							{
								component = (ToolStripItem)this.listBoxItems.SelectedItem;
							}
							string text = "&" + component.GetType().Name;
							if (component.Site != null)
							{
								e.Graphics.FillRectangle(SystemBrushes.Control, clientRectangle);
								string name = component.Site.Name;
								if (label != null)
								{
									label.Text = text + name;
								}
								int num = (int)e.Graphics.MeasureString(text, font).Width;
								e.Graphics.DrawString(text, font, SystemBrushes.WindowText, clientRectangle, stringFormat);
								int num2 = (int)e.Graphics.MeasureString(name, this.selectedItemName.Font).Width;
								Rectangle bounds = new Rectangle(num + 5, 0, clientRectangle.Width - (num + 5), clientRectangle.Height);
								if (num2 > bounds.Width)
								{
									label.AutoEllipsis = true;
								}
								else
								{
									label.AutoEllipsis = false;
								}
								TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis;
								if (flag)
								{
									textFormatFlags |= TextFormatFlags.RightToLeft;
								}
								TextRenderer.DrawText(e.Graphics, name, this.selectedItemName.Font, bounds, SystemColors.WindowText, textFormatFlags);
							}
						}
						else
						{
							e.Graphics.FillRectangle(SystemBrushes.Control, clientRectangle);
							if (label != null)
							{
								label.Text = SR.GetString("ToolStripItemCollectionEditorLabelMultipleItems");
							}
							e.Graphics.DrawString(SR.GetString("ToolStripItemCollectionEditorLabelMultipleItems"), font, SystemBrushes.WindowText, clientRectangle, stringFormat);
						}
					}
					else
					{
						e.Graphics.FillRectangle(SystemBrushes.Control, clientRectangle);
						if (label != null)
						{
							label.Text = SR.GetString("ToolStripItemCollectionEditorLabelNone");
						}
						e.Graphics.DrawString(SR.GetString("ToolStripItemCollectionEditorLabelNone"), font, SystemBrushes.WindowText, clientRectangle, stringFormat);
					}
					stringFormat.Dispose();
				}
			}

			// Token: 0x06003340 RID: 13120 RVA: 0x001181E8 File Offset: 0x001163E8
			private void RemoveItem(ToolStripItem item)
			{
				int num;
				try
				{
					num = this._itemList.IndexOf(item);
					this._itemList.Remove(item);
				}
				finally
				{
					item.Dispose();
				}
				if (this._itemList.Count > 0)
				{
					this.listBoxItems.ClearSelected();
					num = Math.Max(0, Math.Min(num, this.listBoxItems.Items.Count - 1));
					this.listBoxItems.SelectedIndex = num;
				}
			}

			// Token: 0x06003341 RID: 13121 RVA: 0x0011826C File Offset: 0x0011646C
			internal static ToolStrip ToolStripFromObject(object instance)
			{
				ToolStrip result = null;
				if (instance != null)
				{
					if (instance is ToolStripDropDownItem)
					{
						result = ((ToolStripDropDownItem)instance).DropDown;
					}
					else
					{
						result = (instance as ToolStrip);
					}
				}
				return result;
			}

			// Token: 0x0400222C RID: 8748
			private ToolStripCollectionEditor editor;

			// Token: 0x0400222D RID: 8749
			private const int ICON_DIMENSION = 16;

			// Token: 0x0400222E RID: 8750
			private const int SEPARATOR_HEIGHT = 4;

			// Token: 0x0400222F RID: 8751
			private const int TEXT_IMAGE_SPACING = 6;

			// Token: 0x04002230 RID: 8752
			private const int INDENT_SPACING = 4;

			// Token: 0x04002231 RID: 8753
			private const int IMAGE_PADDING = 1;

			// Token: 0x04002232 RID: 8754
			private static bool isScalingInitialized = false;

			// Token: 0x04002233 RID: 8755
			private static int iconHeight = 16;

			// Token: 0x04002234 RID: 8756
			private static int iconWidth = 16;

			// Token: 0x04002235 RID: 8757
			private static int separatorHeight = 4;

			// Token: 0x04002236 RID: 8758
			private static int textImageSpacing = 6;

			// Token: 0x04002237 RID: 8759
			private static int indentSpacing = 4;

			// Token: 0x04002238 RID: 8760
			private static int imagePaddingX = 1;

			// Token: 0x04002239 RID: 8761
			private static int imagePaddingY = 1;

			// Token: 0x0400223A RID: 8762
			private ToolStripCustomTypeDescriptor toolStripCustomTypeDescriptor;

			// Token: 0x0400223B RID: 8763
			private const int GdiPlusFudge = 5;

			// Token: 0x0400223C RID: 8764
			private ToolStripItemCollection _targetToolStripCollection;

			// Token: 0x0400223D RID: 8765
			private ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection _itemList;

			// Token: 0x0400223E RID: 8766
			private int customItemIndex = -1;

			// Token: 0x0400223F RID: 8767
			private TableLayoutPanel tableLayoutPanel;

			// Token: 0x04002240 RID: 8768
			private TableLayoutPanel addTableLayoutPanel;

			// Token: 0x04002241 RID: 8769
			private TableLayoutPanel okCancelTableLayoutPanel;

			// Token: 0x04002242 RID: 8770
			private Button btnCancel;

			// Token: 0x04002243 RID: 8771
			private Button btnOK;

			// Token: 0x04002244 RID: 8772
			private Button btnMoveUp;

			// Token: 0x04002245 RID: 8773
			private Button btnMoveDown;

			// Token: 0x04002246 RID: 8774
			private Label lblItems;

			// Token: 0x04002247 RID: 8775
			private ComboBox newItemTypes;

			// Token: 0x04002248 RID: 8776
			private Button btnAddNew;

			// Token: 0x04002249 RID: 8777
			private CollectionEditor.FilterListBox listBoxItems;

			// Token: 0x0400224A RID: 8778
			private Label selectedItemName;

			// Token: 0x0400224B RID: 8779
			private Button btnRemove;

			// Token: 0x0400224C RID: 8780
			private VsPropertyGrid selectedItemProps;

			// Token: 0x0400224D RID: 8781
			private Label lblMembers;

			// Token: 0x0400224E RID: 8782
			private IComponentChangeService _componentChangeSvc;

			// Token: 0x0400224F RID: 8783
			private string _originalText;

			// Token: 0x020005F1 RID: 1521
			private class ImageComboBox : ComboBox
			{
				// Token: 0x17000A37 RID: 2615
				// (get) Token: 0x060034E7 RID: 13543 RVA: 0x0011F121 File Offset: 0x0011D321
				private Rectangle ImageRect
				{
					get
					{
						if (this.RightToLeft == RightToLeft.Yes)
						{
							return new Rectangle(4 + SystemInformation.HorizontalScrollBarThumbWidth, 3, ToolStripCollectionEditor.ToolStripItemEditorForm.iconWidth, ToolStripCollectionEditor.ToolStripItemEditorForm.iconHeight);
						}
						return new Rectangle(3, 3, ToolStripCollectionEditor.ToolStripItemEditorForm.iconWidth, ToolStripCollectionEditor.ToolStripItemEditorForm.iconHeight);
					}
				}

				// Token: 0x060034E8 RID: 13544 RVA: 0x0011F155 File Offset: 0x0011D355
				protected override void OnDropDownClosed(EventArgs e)
				{
					base.OnDropDownClosed(e);
					base.Invalidate(this.ImageRect);
				}

				// Token: 0x060034E9 RID: 13545 RVA: 0x0011F16A File Offset: 0x0011D36A
				protected override void OnSelectedIndexChanged(EventArgs e)
				{
					base.OnSelectedIndexChanged(e);
					base.Invalidate(this.ImageRect);
				}

				// Token: 0x060034EA RID: 13546 RVA: 0x0011F180 File Offset: 0x0011D380
				protected override void WndProc(ref Message m)
				{
					base.WndProc(ref m);
					int msg = m.Msg;
					if (msg - 7 <= 1)
					{
						base.Invalidate(this.ImageRect);
					}
				}
			}

			// Token: 0x020005F2 RID: 1522
			private class EditorItemCollection : CollectionBase
			{
				// Token: 0x060034EB RID: 13547 RVA: 0x0011F1AD File Offset: 0x0011D3AD
				internal EditorItemCollection(ToolStripCollectionEditor.ToolStripItemEditorForm owner, IList displayList, IList componentList)
				{
					this._owner = owner;
					this._listBoxList = displayList;
					this._targetCollectionList = componentList;
				}

				// Token: 0x060034EC RID: 13548 RVA: 0x0011F1CA File Offset: 0x0011D3CA
				public void Add(object item)
				{
					base.List.Add(new ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem(item));
				}

				// Token: 0x060034ED RID: 13549 RVA: 0x0011F1E0 File Offset: 0x0011D3E0
				public int IndexOf(ToolStripItem item)
				{
					for (int i = 0; i < base.List.Count; i++)
					{
						ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem editorItem = (ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem)base.List[i];
						if (editorItem.Component == item)
						{
							return i;
						}
					}
					return -1;
				}

				// Token: 0x060034EE RID: 13550 RVA: 0x0011F221 File Offset: 0x0011D421
				public void Insert(int index, ToolStripItem item)
				{
					base.List.Insert(index, new ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem(item));
				}

				// Token: 0x060034EF RID: 13551 RVA: 0x0011F238 File Offset: 0x0011D438
				public void Move(int fromIndex, int toIndex)
				{
					if (toIndex == fromIndex)
					{
						return;
					}
					ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem editorItem = (ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem)base.List[fromIndex];
					if (editorItem.Host != null)
					{
						return;
					}
					try
					{
						this._owner.Context.OnComponentChanging();
						this._listBoxList.Remove(editorItem.Component);
						this._targetCollectionList.Remove(editorItem.Component);
						base.InnerList.Remove(editorItem);
						this._listBoxList.Insert(toIndex, editorItem.Component);
						this._targetCollectionList.Insert(toIndex - 1, editorItem.Component);
						base.InnerList.Insert(toIndex, editorItem);
					}
					finally
					{
						this._owner.Context.OnComponentChanged();
					}
				}

				// Token: 0x060034F0 RID: 13552 RVA: 0x0011F2FC File Offset: 0x0011D4FC
				protected override void OnClear()
				{
					this._listBoxList.Clear();
					foreach (object obj in base.List)
					{
						ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem editorItem = (ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem)obj;
						editorItem.Dispose();
					}
					base.OnClear();
				}

				// Token: 0x060034F1 RID: 13553 RVA: 0x0011F368 File Offset: 0x0011D568
				protected override void OnInsertComplete(int index, object value)
				{
					ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem editorItem = (ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem)value;
					if (editorItem.Host != null)
					{
						this._listBoxList.Insert(index, editorItem.Host);
						base.OnInsertComplete(index, value);
						return;
					}
					if (!this._targetCollectionList.Contains(editorItem.Component))
					{
						try
						{
							this._owner.Context.OnComponentChanging();
							this._targetCollectionList.Insert(index - 1, editorItem.Component);
						}
						finally
						{
							this._owner.Context.OnComponentChanged();
						}
					}
					this._listBoxList.Insert(index, editorItem.Component);
					base.OnInsertComplete(index, value);
				}

				// Token: 0x060034F2 RID: 13554 RVA: 0x0011F414 File Offset: 0x0011D614
				protected override void OnRemove(int index, object value)
				{
					ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem editorItem = (ToolStripCollectionEditor.ToolStripItemEditorForm.EditorItemCollection.EditorItem)base.List[index];
					this._listBoxList.RemoveAt(index);
					try
					{
						this._owner.Context.OnComponentChanging();
						this._targetCollectionList.RemoveAt(index - 1);
					}
					finally
					{
						this._owner.Context.OnComponentChanged();
					}
					editorItem.Dispose();
					base.OnRemove(index, value);
				}

				// Token: 0x060034F3 RID: 13555 RVA: 0x0011F490 File Offset: 0x0011D690
				public void Remove(ToolStripItem item)
				{
					int index = this.IndexOf(item);
					base.List.RemoveAt(index);
				}

				// Token: 0x04002348 RID: 9032
				private IList _listBoxList;

				// Token: 0x04002349 RID: 9033
				private IList _targetCollectionList;

				// Token: 0x0400234A RID: 9034
				private ToolStripCollectionEditor.ToolStripItemEditorForm _owner;

				// Token: 0x020005FD RID: 1533
				private class EditorItem
				{
					// Token: 0x06003505 RID: 13573 RVA: 0x0011F91F File Offset: 0x0011DB1F
					internal EditorItem(object componentItem)
					{
						if (componentItem is ToolStrip)
						{
							this._host = (ToolStrip)componentItem;
							return;
						}
						this._component = (ToolStripItem)componentItem;
					}

					// Token: 0x17000A3A RID: 2618
					// (get) Token: 0x06003506 RID: 13574 RVA: 0x0011F948 File Offset: 0x0011DB48
					public ToolStripItem Component
					{
						get
						{
							return this._component;
						}
					}

					// Token: 0x17000A3B RID: 2619
					// (get) Token: 0x06003507 RID: 13575 RVA: 0x0011F950 File Offset: 0x0011DB50
					public ToolStrip Host
					{
						get
						{
							return this._host;
						}
					}

					// Token: 0x06003508 RID: 13576 RVA: 0x0011F958 File Offset: 0x0011DB58
					public void Dispose()
					{
						GC.SuppressFinalize(this);
						this._component = null;
					}

					// Token: 0x0400237F RID: 9087
					public ToolStripItem _component;

					// Token: 0x04002380 RID: 9088
					public ToolStrip _host;
				}
			}

			// Token: 0x020005F3 RID: 1523
			private class TypeListItem
			{
				// Token: 0x060034F4 RID: 13556 RVA: 0x0011F4B1 File Offset: 0x0011D6B1
				public TypeListItem(Type t)
				{
					this.Type = t;
				}

				// Token: 0x060034F5 RID: 13557 RVA: 0x0011F4C0 File Offset: 0x0011D6C0
				public override string ToString()
				{
					return ToolStripDesignerUtils.GetToolboxDescription(this.Type);
				}

				// Token: 0x0400234B RID: 9035
				public readonly Type Type;
			}
		}
	}
}
