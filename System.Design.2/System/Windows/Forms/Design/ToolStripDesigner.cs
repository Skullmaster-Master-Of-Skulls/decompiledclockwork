using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000354 RID: 852
	internal class ToolStripDesigner : ControlDesigner
	{
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x000CE328 File Offset: 0x000CC528
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				if (this._actionLists == null)
				{
					this._actionLists = new ToolStripActionList(this);
				}
				designerActionListCollection.Add(this._actionLists);
				DesignerVerbCollection verbs = this.Verbs;
				if (verbs != null && verbs.Count != 0)
				{
					DesignerVerb[] array = new DesignerVerb[verbs.Count];
					verbs.CopyTo(array, 0);
					designerActionListCollection.Add(new DesignerActionVerbList(array));
				}
				return designerActionListCollection;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000CE39C File Offset: 0x000CC59C
		private Rectangle AddItemRect
		{
			get
			{
				Rectangle result = default(Rectangle);
				if (this._miniToolStrip == null)
				{
					return result;
				}
				return this._miniToolStrip.Bounds;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0009EF25 File Offset: 0x0009D125
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x000CE3C8 File Offset: 0x000CC5C8
		private bool AllowDrop
		{
			get
			{
				return (bool)base.ShadowProperties["AllowDrop"];
			}
			set
			{
				if (value && this.AllowItemReorder)
				{
					throw new ArgumentException(SR.GetString("ToolStripAllowItemReorderAndAllowDropCannotBeSetToTrue"));
				}
				base.ShadowProperties["AllowDrop"] = value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x000CE3FB File Offset: 0x000CC5FB
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x000CE412 File Offset: 0x000CC612
		private bool AllowItemReorder
		{
			get
			{
				return (bool)base.ShadowProperties["AllowItemReorder"];
			}
			set
			{
				if (value && this.AllowDrop)
				{
					throw new ArgumentException(SR.GetString("ToolStripAllowItemReorderAndAllowDropCannotBeSetToTrue"));
				}
				base.ShadowProperties["AllowItemReorder"] = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000CE448 File Offset: 0x000CC648
		public override ICollection AssociatedComponents
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.ToolStrip.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (!(toolStripItem is DesignerToolStripControlHost))
					{
						arrayList.Add(toolStripItem);
					}
				}
				return arrayList;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000CE4BC File Offset: 0x000CC6BC
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x000CE4C4 File Offset: 0x000CC6C4
		public bool CacheItems
		{
			get
			{
				return this.cacheItems;
			}
			set
			{
				this.cacheItems = value;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x000CE4D0 File Offset: 0x000CC6D0
		private bool CanAddItems
		{
			get
			{
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this.ToolStrip)[typeof(InheritanceAttribute)];
				return inheritanceAttribute == null || inheritanceAttribute.InheritanceLevel == InheritanceLevel.NotInherited;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000CE50C File Offset: 0x000CC70C
		internal override bool ControlSupportsSnaplines
		{
			get
			{
				return !(this.ToolStrip.Parent is ToolStripPanel);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x000CE523 File Offset: 0x000CC723
		private ContextMenuStrip DesignerContextMenu
		{
			get
			{
				if (this.toolStripContextMenu == null)
				{
					this.toolStripContextMenu = new BaseContextMenuStrip(this.ToolStrip.Site, this.ToolStrip);
					this.toolStripContextMenu.Text = "CustomContextMenu";
				}
				return this.toolStripContextMenu;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x000CE55F File Offset: 0x000CC75F
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x000CE567 File Offset: 0x000CC767
		public bool DontCloseOverflow
		{
			get
			{
				return this.dontCloseOverflow;
			}
			set
			{
				this.dontCloseOverflow = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000CE570 File Offset: 0x000CC770
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x000CE578 File Offset: 0x000CC778
		public Rectangle DragBoxFromMouseDown
		{
			get
			{
				return this.dragBoxFromMouseDown;
			}
			set
			{
				this.dragBoxFromMouseDown = value;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x000CE581 File Offset: 0x000CC781
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x000CE58C File Offset: 0x000CC78C
		internal bool EditingCollection
		{
			get
			{
				return this._editingCollection > 0U;
			}
			set
			{
				if (value)
				{
					this._editingCollection += 1U;
					return;
				}
				this._editingCollection -= 1U;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x000CE5AE File Offset: 0x000CC7AE
		public ToolStripEditorManager EditManager
		{
			get
			{
				return this.editManager;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x000CE5B6 File Offset: 0x000CC7B6
		internal ToolStripTemplateNode Editor
		{
			get
			{
				return this.tn;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x000CE5BE File Offset: 0x000CC7BE
		public DesignerToolStripControlHost EditorNode
		{
			get
			{
				return this.editorNode;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x000CE5C6 File Offset: 0x000CC7C6
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x000CE5CE File Offset: 0x000CC7CE
		internal ToolStrip EditorToolStrip
		{
			get
			{
				return this._miniToolStrip;
			}
			set
			{
				this._miniToolStrip = value;
				this._miniToolStrip.Parent = this.ToolStrip;
				this.LayoutToolStrip();
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000CE5EE File Offset: 0x000CC7EE
		// (set) Token: 0x060021E1 RID: 8673 RVA: 0x000CE5F6 File Offset: 0x000CC7F6
		public bool FireSyncSelection
		{
			get
			{
				return this.fireSyncSelection;
			}
			set
			{
				this.fireSyncSelection = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000CE5FF File Offset: 0x000CC7FF
		// (set) Token: 0x060021E3 RID: 8675 RVA: 0x000CE607 File Offset: 0x000CC807
		public int IndexOfItemUnderMouseToDrag
		{
			get
			{
				return this.indexOfItemUnderMouseToDrag;
			}
			set
			{
				this.indexOfItemUnderMouseToDrag = value;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000CE610 File Offset: 0x000CC810
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (base.InheritanceAttribute == InheritanceAttribute.Inherited)
				{
					return InheritanceAttribute.InheritedReadOnly;
				}
				return base.InheritanceAttribute;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000CE62B File Offset: 0x000CC82B
		// (set) Token: 0x060021E6 RID: 8678 RVA: 0x000CE633 File Offset: 0x000CC833
		public DesignerTransaction InsertTansaction
		{
			get
			{
				return this._insertMenuItemTransaction;
			}
			set
			{
				this._insertMenuItemTransaction = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000CE63C File Offset: 0x000CC83C
		private bool IsToolStripOrItemSelected
		{
			get
			{
				return this.toolStripSelected;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000CE644 File Offset: 0x000CC844
		public ArrayList Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ArrayList();
				}
				return this.items;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x000CE65F File Offset: 0x000CC85F
		// (set) Token: 0x060021EA RID: 8682 RVA: 0x000CE667 File Offset: 0x000CC867
		public DesignerTransaction NewItemTransaction
		{
			get
			{
				return this.newItemTransaction;
			}
			set
			{
				this.newItemTransaction = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000CE670 File Offset: 0x000CC870
		private Rectangle OverFlowButtonRect
		{
			get
			{
				Rectangle result = default(Rectangle);
				if (this.ToolStrip.OverflowButton.Visible)
				{
					return this.ToolStrip.OverflowButton.Bounds;
				}
				return result;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x000CE6A9 File Offset: 0x000CC8A9
		internal ISelectionService SelectionService
		{
			get
			{
				if (this._selectionSvc == null)
				{
					this._selectionSvc = (ISelectionService)this.GetService(typeof(ISelectionService));
				}
				return this._selectionSvc;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000CE6D4 File Offset: 0x000CC8D4
		public bool SupportEditing
		{
			get
			{
				WindowsFormsDesignerOptionService windowsFormsDesignerOptionService = this.GetService(typeof(DesignerOptionService)) as WindowsFormsDesignerOptionService;
				return windowsFormsDesignerOptionService == null || windowsFormsDesignerOptionService.CompatibilityOptions.EnableInSituEditing;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x000CE707 File Offset: 0x000CC907
		protected ToolStrip ToolStrip
		{
			get
			{
				return (ToolStrip)base.Component;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000CE714 File Offset: 0x000CC914
		private ToolStripKeyboardHandlingService KeyboardHandlingService
		{
			get
			{
				if (this.keyboardHandlingService == null)
				{
					this.keyboardHandlingService = (ToolStripKeyboardHandlingService)this.GetService(typeof(ToolStripKeyboardHandlingService));
					if (this.keyboardHandlingService == null)
					{
						this.keyboardHandlingService = new ToolStripKeyboardHandlingService(base.Component.Site);
					}
				}
				return this.keyboardHandlingService;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00003B0F File Offset: 0x00001D0F
		internal override bool SerializePerformLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000CE768 File Offset: 0x000CC968
		// (set) Token: 0x060021F2 RID: 8690 RVA: 0x000CE770 File Offset: 0x000CC970
		internal bool Visible
		{
			get
			{
				return this.currentVisible;
			}
			set
			{
				this.currentVisible = value;
				if (this.ToolStrip.Visible != value && !this.SelectionService.GetComponentSelected(this.ToolStrip))
				{
					this.Control.Visible = value;
				}
			}
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000CE7A8 File Offset: 0x000CC9A8
		private void AddBodyGlyphsForOverflow()
		{
			foreach (object obj in this.ToolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (!(toolStripItem is DesignerToolStripControlHost) && toolStripItem.Placement == ToolStripItemPlacement.Overflow)
				{
					this.AddItemBodyGlyph(toolStripItem);
				}
			}
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000CE818 File Offset: 0x000CCA18
		private void AddItemBodyGlyph(ToolStripItem item)
		{
			if (item != null)
			{
				ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)this.host.GetDesigner(item);
				if (toolStripItemDesigner != null)
				{
					Rectangle glyphBounds = toolStripItemDesigner.GetGlyphBounds();
					Behavior b = new ToolStripItemBehavior();
					ToolStripItemGlyph toolStripItemGlyph = new ToolStripItemGlyph(item, toolStripItemDesigner, glyphBounds, b);
					toolStripItemDesigner.bodyGlyph = toolStripItemGlyph;
					if (this.toolStripAdornerWindowService != null)
					{
						this.toolStripAdornerWindowService.DropDownAdorner.Glyphs.Add(toolStripItemGlyph);
					}
				}
			}
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000CE87C File Offset: 0x000CCA7C
		private ToolStripItem AddNewItem(Type t)
		{
			this.NewItemTransaction = this.host.CreateTransaction(SR.GetString("ToolStripCreatingNewItemTransaction"));
			IComponent component = null;
			try
			{
				this._addingItem = true;
				this.ToolStrip.SuspendLayout();
				ToolStripItemDesigner toolStripItemDesigner = null;
				try
				{
					component = this.host.CreateComponent(t);
					toolStripItemDesigner = (this.host.GetDesigner(component) as ToolStripItemDesigner);
					toolStripItemDesigner.InternalCreate = true;
					if (toolStripItemDesigner != null)
					{
						toolStripItemDesigner.InitializeNewComponent(null);
					}
				}
				finally
				{
					if (toolStripItemDesigner != null)
					{
						toolStripItemDesigner.InternalCreate = false;
					}
					this.ToolStrip.ResumeLayout();
				}
			}
			catch (Exception ex)
			{
				if (this.NewItemTransaction != null)
				{
					this.NewItemTransaction.Cancel();
					this.NewItemTransaction = null;
				}
				CheckoutException ex2 = ex as CheckoutException;
				if (ex2 == null || !ex2.Equals(CheckoutException.Canceled))
				{
					throw;
				}
			}
			finally
			{
				this._addingItem = false;
			}
			return component as ToolStripItem;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000CE970 File Offset: 0x000CCB70
		internal ToolStripItem AddNewItem(Type t, string text, bool enterKeyPressed, bool tabKeyPressed)
		{
			DesignerTransaction designerTransaction = this.host.CreateTransaction(SR.GetString("ToolStripAddingItem", new object[]
			{
				t.Name
			}));
			ToolStripItem toolStripItem = null;
			try
			{
				this._addingItem = true;
				this.ToolStrip.SuspendLayout();
				IComponent component = this.host.CreateComponent(t, ToolStripDesigner.NameFromText(text, t, base.Component.Site));
				ToolStripItemDesigner toolStripItemDesigner = this.host.GetDesigner(component) as ToolStripItemDesigner;
				try
				{
					if (!string.IsNullOrEmpty(text))
					{
						toolStripItemDesigner.InternalCreate = true;
					}
					if (toolStripItemDesigner != null)
					{
						toolStripItemDesigner.InitializeNewComponent(null);
					}
				}
				finally
				{
					toolStripItemDesigner.InternalCreate = false;
				}
				toolStripItem = (component as ToolStripItem);
				if (toolStripItem != null)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(toolStripItem)["Text"];
					if (propertyDescriptor != null && !string.IsNullOrEmpty(text))
					{
						propertyDescriptor.SetValue(toolStripItem, text);
					}
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
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(toolStripItem)["Image"];
						if (propertyDescriptor2 != null && image != null)
						{
							propertyDescriptor2.SetValue(toolStripItem, image);
						}
						PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(toolStripItem)["DisplayStyle"];
						if (propertyDescriptor3 != null)
						{
							propertyDescriptor3.SetValue(toolStripItem, ToolStripItemDisplayStyle.Image);
						}
						PropertyDescriptor propertyDescriptor4 = TypeDescriptor.GetProperties(toolStripItem)["ImageTransparentColor"];
						if (propertyDescriptor4 != null)
						{
							propertyDescriptor4.SetValue(toolStripItem, Color.Magenta);
						}
					}
				}
				this.ToolStrip.ResumeLayout();
				if (!tabKeyPressed)
				{
					if (enterKeyPressed)
					{
						if (!toolStripItemDesigner.SetSelection(enterKeyPressed) && this.KeyboardHandlingService != null)
						{
							this.KeyboardHandlingService.SelectedDesignerControl = this.editorNode;
							this.SelectionService.SetSelectedComponents(null, SelectionTypes.Replace);
						}
					}
					else
					{
						this.KeyboardHandlingService.SelectedDesignerControl = null;
						this.SelectionService.SetSelectedComponents(new IComponent[]
						{
							toolStripItem
						}, SelectionTypes.Replace);
						this.editorNode.RefreshSelectionGlyph();
					}
				}
				else if (this.keyboardHandlingService != null)
				{
					this.KeyboardHandlingService.SelectedDesignerControl = this.editorNode;
					this.SelectionService.SetSelectedComponents(null, SelectionTypes.Replace);
				}
				if (toolStripItemDesigner != null && toolStripItem.Placement != ToolStripItemPlacement.Overflow)
				{
					Rectangle glyphBounds = toolStripItemDesigner.GetGlyphBounds();
					SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
					Behavior b = new ToolStripItemBehavior();
					ToolStripItemGlyph value = new ToolStripItemGlyph(toolStripItem, toolStripItemDesigner, glyphBounds, b);
					selectionManager.BodyGlyphAdorner.Glyphs.Insert(0, value);
				}
				else if (toolStripItemDesigner != null && toolStripItem.Placement == ToolStripItemPlacement.Overflow)
				{
					this.RemoveBodyGlyphsForOverflow();
					this.AddBodyGlyphsForOverflow();
				}
			}
			catch (Exception ex2)
			{
				this.ToolStrip.ResumeLayout();
				if (this._pendingTransaction != null)
				{
					this._pendingTransaction.Cancel();
					this._pendingTransaction = null;
				}
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
				CheckoutException ex3 = ex2 as CheckoutException;
				if (ex3 != null && ex3 != CheckoutException.Canceled)
				{
					throw;
				}
			}
			finally
			{
				if (this._pendingTransaction != null)
				{
					this._pendingTransaction.Cancel();
					this._pendingTransaction = null;
					if (designerTransaction != null)
					{
						designerTransaction.Cancel();
					}
				}
				else if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
				this._addingItem = false;
			}
			return toolStripItem;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000CECE8 File Offset: 0x000CCEE8
		internal void AddNewTemplateNode(ToolStrip wb)
		{
			this.tn = new ToolStripTemplateNode(base.Component, SR.GetString("ToolStripDesignerTemplateNodeEnterText"), null);
			this._miniToolStrip = this.tn.EditorToolStrip;
			int width = this.tn.EditorToolStrip.Width;
			this.editorNode = new DesignerToolStripControlHost(this.tn.EditorToolStrip);
			this.tn.ControlHost = this.editorNode;
			this.editorNode.Width = width;
			this.ToolStrip.Items.Add(this.editorNode);
			this.editorNode.Visible = false;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000CED89 File Offset: 0x000CCF89
		internal void CancelPendingMenuItemTransaction()
		{
			if (this._insertMenuItemTransaction != null)
			{
				this._insertMenuItemTransaction.Cancel();
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000CEDA0 File Offset: 0x000CCFA0
		private bool CheckIfItemSelected()
		{
			bool result = false;
			object obj = this.SelectionService.PrimarySelection;
			if (obj == null)
			{
				obj = (IComponent)this.KeyboardHandlingService.SelectedDesignerControl;
			}
			ToolStripItem toolStripItem = obj as ToolStripItem;
			if (toolStripItem != null)
			{
				if (toolStripItem.Placement == ToolStripItemPlacement.Overflow && toolStripItem.Owner == this.ToolStrip)
				{
					if (this.ToolStrip.CanOverflow && !this.ToolStrip.OverflowButton.DropDown.Visible)
					{
						this.ToolStrip.OverflowButton.ShowDropDown();
					}
					result = true;
				}
				else
				{
					if (!this.ItemParentIsOverflow(toolStripItem) && this.ToolStrip.OverflowButton.DropDown.Visible)
					{
						this.ToolStrip.OverflowButton.HideDropDown();
					}
					if (toolStripItem.Owner == this.ToolStrip)
					{
						result = true;
					}
					else if (toolStripItem is DesignerToolStripControlHost)
					{
						if (toolStripItem.IsOnDropDown && toolStripItem.Placement != ToolStripItemPlacement.Overflow)
						{
							ToolStripDropDown toolStripDropDown = (ToolStripDropDown)((DesignerToolStripControlHost)obj).GetCurrentParent();
							if (toolStripDropDown != null)
							{
								ToolStripItem ownerItem = toolStripDropDown.OwnerItem;
								ToolStripMenuItemDesigner toolStripMenuItemDesigner = (ToolStripMenuItemDesigner)this.host.GetDesigner(ownerItem);
								ToolStripDropDown firstDropDown = toolStripMenuItemDesigner.GetFirstDropDown((ToolStripDropDownItem)ownerItem);
								ToolStripItem toolStripItem2 = (firstDropDown == null) ? ownerItem : firstDropDown.OwnerItem;
								if (toolStripItem2 != null && toolStripItem2.Owner == this.ToolStrip)
								{
									result = true;
								}
							}
						}
					}
					else if (toolStripItem.IsOnDropDown && toolStripItem.Placement != ToolStripItemPlacement.Overflow)
					{
						ToolStripItem ownerItem2 = ((ToolStripDropDown)toolStripItem.Owner).OwnerItem;
						if (ownerItem2 != null)
						{
							ToolStripMenuItemDesigner toolStripMenuItemDesigner2 = (ToolStripMenuItemDesigner)this.host.GetDesigner(ownerItem2);
							ToolStripDropDown toolStripDropDown2 = (toolStripMenuItemDesigner2 == null) ? null : toolStripMenuItemDesigner2.GetFirstDropDown((ToolStripDropDownItem)ownerItem2);
							ToolStripItem toolStripItem3 = (toolStripDropDown2 == null) ? ownerItem2 : toolStripDropDown2.OwnerItem;
							if (toolStripItem3 != null && toolStripItem3.Owner == this.ToolStrip)
							{
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000CEF7C File Offset: 0x000CD17C
		internal bool Commit()
		{
			if (this.tn != null && this.tn.Active)
			{
				this.tn.Commit(false, false);
				this.editorNode.Width = this.tn.EditorToolStrip.Width;
			}
			else
			{
				ToolStripDropDownItem toolStripDropDownItem = this.SelectionService.PrimarySelection as ToolStripDropDownItem;
				if (toolStripDropDownItem != null)
				{
					ToolStripMenuItemDesigner toolStripMenuItemDesigner = this.host.GetDesigner(toolStripDropDownItem) as ToolStripMenuItemDesigner;
					if (toolStripMenuItemDesigner != null && toolStripMenuItemDesigner.IsEditorActive)
					{
						toolStripMenuItemDesigner.Commit();
						return true;
					}
				}
				else if (this.KeyboardHandlingService != null)
				{
					ToolStripItem toolStripItem = this.KeyboardHandlingService.SelectedDesignerControl as ToolStripItem;
					if (toolStripItem != null && toolStripItem.IsOnDropDown)
					{
						ToolStripDropDown toolStripDropDown = toolStripItem.GetCurrentParent() as ToolStripDropDown;
						if (toolStripDropDown != null)
						{
							ToolStripDropDownItem toolStripDropDownItem2 = toolStripDropDown.OwnerItem as ToolStripDropDownItem;
							if (toolStripDropDownItem2 != null)
							{
								ToolStripMenuItemDesigner toolStripMenuItemDesigner2 = this.host.GetDesigner(toolStripDropDownItem2) as ToolStripMenuItemDesigner;
								if (toolStripMenuItemDesigner2 != null && toolStripMenuItemDesigner2.IsEditorActive)
								{
									toolStripMenuItemDesigner2.Commit();
									return true;
								}
							}
						}
					}
					else
					{
						ToolStripItem toolStripItem2 = this.SelectionService.PrimarySelection as ToolStripItem;
						if (toolStripItem2 != null)
						{
							ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)this.host.GetDesigner(toolStripItem2);
							if (toolStripItemDesigner != null && toolStripItemDesigner.IsEditorActive)
							{
								toolStripItemDesigner.Editor.Commit(false, false);
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000CF0C8 File Offset: 0x000CD2C8
		private void Control_HandleCreated(object sender, EventArgs e)
		{
			this.Control.HandleCreated -= this.Control_HandleCreated;
			this.InitializeNewItemDropDown();
			this.ToolStrip.OverflowButton.DropDown.Closing += this.OnOverflowDropDownClosing;
			this.ToolStrip.OverflowButton.DropDownOpening += this.OnOverFlowDropDownOpening;
			this.ToolStrip.OverflowButton.DropDownOpened += this.OnOverFlowDropDownOpened;
			this.ToolStrip.OverflowButton.DropDownClosed += this.OnOverFlowDropDownClosed;
			this.ToolStrip.OverflowButton.DropDown.Resize += this.OnOverflowDropDownResize;
			this.ToolStrip.OverflowButton.DropDown.Paint += this.OnOverFlowDropDownPaint;
			this.ToolStrip.Move += this.OnToolStripMove;
			this.ToolStrip.VisibleChanged += this.OnToolStripVisibleChanged;
			this.ToolStrip.ItemAdded += this.OnItemAdded;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000CF1F0 File Offset: 0x000CD3F0
		private void ComponentChangeSvc_ComponentAdded(object sender, ComponentEventArgs e)
		{
			if (this.toolStripSelected && e.Component is ToolStrip)
			{
				this.toolStripSelected = false;
			}
			ToolStripItem toolStripItem = e.Component as ToolStripItem;
			try
			{
				if (toolStripItem != null && this._addingItem && !toolStripItem.IsOnDropDown)
				{
					this._addingItem = false;
					if (this.CacheItems)
					{
						this.items.Add(toolStripItem);
					}
					else
					{
						int count = this.ToolStrip.Items.Count;
						try
						{
							base.RaiseComponentChanging(TypeDescriptor.GetProperties(base.Component)["Items"]);
							ToolStripItem toolStripItem2 = this.SelectionService.PrimarySelection as ToolStripItem;
							if (toolStripItem2 != null)
							{
								if (toolStripItem2.Owner == this.ToolStrip)
								{
									int index = this.ToolStrip.Items.IndexOf(toolStripItem2);
									this.ToolStrip.Items.Insert(index, toolStripItem);
								}
							}
							else if (count > 0)
							{
								this.ToolStrip.Items.Insert(count - 1, toolStripItem);
							}
							else
							{
								this.ToolStrip.Items.Add(toolStripItem);
							}
						}
						finally
						{
							base.RaiseComponentChanged(TypeDescriptor.GetProperties(base.Component)["Items"], null, null);
						}
					}
				}
			}
			catch
			{
				if (this._pendingTransaction != null)
				{
					this._pendingTransaction.Cancel();
					this._pendingTransaction = null;
					this._insertMenuItemTransaction = null;
				}
			}
			finally
			{
				if (this._pendingTransaction != null)
				{
					this._pendingTransaction.Commit();
					this._pendingTransaction = null;
					this._insertMenuItemTransaction = null;
				}
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000CF3B8 File Offset: 0x000CD5B8
		private void ComponentChangeSvc_ComponentAdding(object sender, ComponentEventArgs e)
		{
			if (this.KeyboardHandlingService != null && this.KeyboardHandlingService.CopyInProgress)
			{
				return;
			}
			object obj = this.SelectionService.PrimarySelection;
			if (obj == null && this.keyboardHandlingService != null)
			{
				obj = this.KeyboardHandlingService.SelectedDesignerControl;
			}
			ToolStripItem toolStripItem = obj as ToolStripItem;
			if (toolStripItem != null && toolStripItem.Owner != this.ToolStrip)
			{
				return;
			}
			ToolStripItem toolStripItem2 = e.Component as ToolStripItem;
			if (toolStripItem2 != null && toolStripItem2.Owner != null && toolStripItem2.Owner.Site == null)
			{
				return;
			}
			if (this._insertMenuItemTransaction == null && ToolStripDesigner._autoAddNewItems && toolStripItem2 != null && !this._addingItem && this.IsToolStripOrItemSelected && !this.EditingCollection)
			{
				this._addingItem = true;
				if (this._pendingTransaction == null)
				{
					this._insertMenuItemTransaction = (this._pendingTransaction = this.host.CreateTransaction(SR.GetString("ToolStripDesignerTransactionAddingItem")));
				}
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000CF49C File Offset: 0x000CD69C
		private void ComponentChangeSvc_ComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			ToolStripItem toolStripItem = e.Component as ToolStripItem;
			if (toolStripItem != null)
			{
				ToolStrip owner = toolStripItem.Owner;
				if (owner == this.ToolStrip && e.Member != null && e.Member.Name == "Overflow")
				{
					ToolStripItemOverflow toolStripItemOverflow = (ToolStripItemOverflow)e.OldValue;
					ToolStripItemOverflow toolStripItemOverflow2 = (ToolStripItemOverflow)e.NewValue;
					if (toolStripItemOverflow != ToolStripItemOverflow.Always && toolStripItemOverflow2 == ToolStripItemOverflow.Always && this.ToolStrip.CanOverflow && !this.ToolStrip.OverflowButton.DropDown.Visible)
					{
						this.ToolStrip.OverflowButton.ShowDropDown();
					}
				}
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000CF540 File Offset: 0x000CD740
		private void ComponentChangeSvc_ComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (e.Component is ToolStripItem && ((ToolStripItem)e.Component).Owner == base.Component)
			{
				ToolStripItem toolStripItem = (ToolStripItem)e.Component;
				int num = this.ToolStrip.Items.IndexOf(toolStripItem);
				try
				{
					if (num != -1)
					{
						this.ToolStrip.Items.Remove(toolStripItem);
						base.RaiseComponentChanged(TypeDescriptor.GetProperties(base.Component)["Items"], null, null);
					}
				}
				finally
				{
					if (this._pendingTransaction != null)
					{
						this._pendingTransaction.Commit();
						this._pendingTransaction = null;
					}
				}
				if (this.ToolStrip.Items.Count > 1)
				{
					num = Math.Min(this.ToolStrip.Items.Count - 1, num);
					num = Math.Max(0, num);
				}
				else
				{
					num = -1;
				}
				this.LayoutToolStrip();
				if (toolStripItem.Placement == ToolStripItemPlacement.Overflow)
				{
					this.RemoveBodyGlyphsForOverflow();
					this.AddBodyGlyphsForOverflow();
				}
				if (this.toolStripAdornerWindowService != null && this.boundsToInvalidate != Rectangle.Empty)
				{
					this.toolStripAdornerWindowService.Invalidate(this.boundsToInvalidate);
					base.BehaviorService.Invalidate(this.boundsToInvalidate);
				}
				if (this.KeyboardHandlingService.CutOrDeleteInProgress)
				{
					IComponent component2;
					if (num != -1)
					{
						IComponent component = this.ToolStrip.Items[num];
						component2 = component;
					}
					else
					{
						IComponent component = this.ToolStrip;
						component2 = component;
					}
					IComponent component3 = component2;
					if (component3 != null)
					{
						if (component3 is DesignerToolStripControlHost)
						{
							if (this.KeyboardHandlingService != null)
							{
								this.KeyboardHandlingService.SelectedDesignerControl = component3;
							}
							this.SelectionService.SetSelectedComponents(null, SelectionTypes.Replace);
							return;
						}
						this.SelectionService.SetSelectedComponents(new IComponent[]
						{
							component3
						}, SelectionTypes.Replace);
					}
				}
			}
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000CF6F8 File Offset: 0x000CD8F8
		private void ComponentChangeSvc_ComponentRemoving(object sender, ComponentEventArgs e)
		{
			if (e.Component is ToolStripItem && ((ToolStripItem)e.Component).Owner == base.Component)
			{
				try
				{
					this._pendingTransaction = this.host.CreateTransaction(SR.GetString("ToolStripDesignerTransactionRemovingItem"));
					base.RaiseComponentChanging(TypeDescriptor.GetProperties(base.Component)["Items"]);
					ToolStripDropDownItem toolStripDropDownItem = e.Component as ToolStripDropDownItem;
					if (toolStripDropDownItem != null)
					{
						toolStripDropDownItem.HideDropDown();
						this.boundsToInvalidate = toolStripDropDownItem.DropDown.Bounds;
					}
				}
				catch
				{
					if (this._pendingTransaction != null)
					{
						this._pendingTransaction.Cancel();
						this._pendingTransaction = null;
					}
				}
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000CF7B8 File Offset: 0x000CD9B8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.disposed = true;
				if (this.items != null)
				{
					this.items = null;
				}
				if (this.undoEngine != null)
				{
					this.undoEngine.Undoing -= this.OnUndoing;
					this.undoEngine.Undone -= this.OnUndone;
				}
				if (this.componentChangeSvc != null)
				{
					this.componentChangeSvc.ComponentRemoved -= this.ComponentChangeSvc_ComponentRemoved;
					this.componentChangeSvc.ComponentRemoving -= this.ComponentChangeSvc_ComponentRemoving;
					this.componentChangeSvc.ComponentAdded -= this.ComponentChangeSvc_ComponentAdded;
					this.componentChangeSvc.ComponentAdding -= this.ComponentChangeSvc_ComponentAdding;
					this.componentChangeSvc.ComponentChanged -= this.ComponentChangeSvc_ComponentChanged;
				}
				if (this._selectionSvc != null)
				{
					this._selectionSvc.SelectionChanged -= this.selSvc_SelectionChanged;
					this._selectionSvc.SelectionChanging -= this.selSvc_SelectionChanging;
					this._selectionSvc = null;
				}
				base.EnableDragDrop(false);
				if (this.editManager != null)
				{
					this.editManager.CloseManager();
					this.editManager = null;
				}
				if (this.tn != null)
				{
					this.tn.RollBack();
					this.tn.CloseEditor();
					this.tn = null;
				}
				if (this._miniToolStrip != null)
				{
					this._miniToolStrip.Dispose();
					this._miniToolStrip = null;
				}
				if (this.editorNode != null)
				{
					this.editorNode.Dispose();
					this.editorNode = null;
				}
				if (this.ToolStrip != null)
				{
					this.ToolStrip.OverflowButton.DropDown.Closing -= this.OnOverflowDropDownClosing;
					this.ToolStrip.OverflowButton.DropDownOpening -= this.OnOverFlowDropDownOpening;
					this.ToolStrip.OverflowButton.DropDownOpened -= this.OnOverFlowDropDownOpened;
					this.ToolStrip.OverflowButton.DropDownClosed -= this.OnOverFlowDropDownClosed;
					this.ToolStrip.OverflowButton.DropDown.Resize -= this.OnOverflowDropDownResize;
					this.ToolStrip.OverflowButton.DropDown.Paint -= this.OnOverFlowDropDownPaint;
					this.ToolStrip.Move -= this.OnToolStripMove;
					this.ToolStrip.VisibleChanged -= this.OnToolStripVisibleChanged;
					this.ToolStrip.ItemAdded -= this.OnItemAdded;
					this.ToolStrip.Resize -= this.ToolStrip_Resize;
					this.ToolStrip.DockChanged -= this.ToolStrip_Resize;
					this.ToolStrip.LayoutCompleted -= this.ToolStrip_LayoutCompleted;
				}
				if (this.toolStripContextMenu != null)
				{
					this.toolStripContextMenu.Dispose();
					this.toolStripContextMenu = null;
				}
				this.RemoveBodyGlyphsForOverflow();
				if (this.ToolStrip.OverflowButton.DropDown.Visible)
				{
					this.ToolStrip.OverflowButton.HideDropDown();
				}
				if (this.toolStripAdornerWindowService != null)
				{
					this.toolStripAdornerWindowService = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000CFAF4 File Offset: 0x000CDCF4
		public override void DoDefaultAction()
		{
			if (this.InheritanceAttribute != InheritanceAttribute.InheritedReadOnly)
			{
				IComponent component = this.SelectionService.PrimarySelection as IComponent;
				if (component == null && this.KeyboardHandlingService != null)
				{
					component = (IComponent)this.KeyboardHandlingService.SelectedDesignerControl;
				}
				if (component is ToolStripItem && this.host != null)
				{
					IDesigner designer = this.host.GetDesigner(component);
					if (designer != null)
					{
						designer.DoDefaultAction();
						return;
					}
				}
				base.DoDefaultAction();
			}
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000CFB68 File Offset: 0x000CDD68
		protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			if (!this.ToolStrip.IsHandleCreated)
			{
				return null;
			}
			SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
			if (selectionManager != null && this.ToolStrip != null && this.CanAddItems && this.ToolStrip.Visible)
			{
				object primarySelection = this.SelectionService.PrimarySelection;
				Behavior behavior = new ToolStripItemBehavior();
				if (this.ToolStrip.Items.Count > 0)
				{
					ToolStripItem[] array = new ToolStripItem[this.ToolStrip.Items.Count];
					this.ToolStrip.Items.CopyTo(array, 0);
					foreach (ToolStripItem toolStripItem in array)
					{
						if (toolStripItem != null)
						{
							ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)this.host.GetDesigner(toolStripItem);
							if (toolStripItem != primarySelection && toolStripItemDesigner != null && toolStripItemDesigner.IsEditorActive)
							{
								toolStripItemDesigner.Editor.Commit(false, false);
							}
						}
					}
				}
				IMenuEditorService menuEditorService = (IMenuEditorService)this.GetService(typeof(IMenuEditorService));
				if (menuEditorService == null || (menuEditorService != null && !menuEditorService.IsActive()))
				{
					foreach (object obj in this.ToolStrip.Items)
					{
						ToolStripItem toolStripItem2 = (ToolStripItem)obj;
						if (!(toolStripItem2 is DesignerToolStripControlHost) && toolStripItem2.Placement == ToolStripItemPlacement.Main)
						{
							ToolStripItemDesigner toolStripItemDesigner2 = (ToolStripItemDesigner)this.host.GetDesigner(toolStripItem2);
							if (toolStripItemDesigner2 != null)
							{
								bool flag = toolStripItem2 == primarySelection;
								if (flag)
								{
									((ToolStripItemBehavior)behavior).dragBoxFromMouseDown = this.dragBoxFromMouseDown;
								}
								if (!flag)
								{
									toolStripItem2.AutoSize = (toolStripItemDesigner2 == null || toolStripItemDesigner2.AutoSize);
								}
								Rectangle glyphBounds = toolStripItemDesigner2.GetGlyphBounds();
								Control parent = this.ToolStrip.Parent;
								Rectangle parentBounds = base.BehaviorService.ControlRectInAdornerWindow(parent);
								if (ToolStripDesigner.IsGlyphTotallyVisible(glyphBounds, parentBounds) && toolStripItem2.Visible)
								{
									ToolStripItemGlyph toolStripItemGlyph = new ToolStripItemGlyph(toolStripItem2, toolStripItemDesigner2, glyphBounds, behavior);
									toolStripItemDesigner2.bodyGlyph = toolStripItemGlyph;
									selectionManager.BodyGlyphAdorner.Glyphs.Add(toolStripItemGlyph);
								}
							}
						}
					}
				}
			}
			return base.GetControlGlyph(selectionType);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000CFDC8 File Offset: 0x000CDFC8
		public override GlyphCollection GetGlyphs(GlyphSelectionType selType)
		{
			GlyphCollection glyphCollection = new GlyphCollection();
			ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
			foreach (object obj in selectedComponents)
			{
				if (obj is ToolStrip)
				{
					GlyphCollection glyphs = base.GetGlyphs(selType);
					glyphCollection.AddRange(glyphs);
				}
				else
				{
					ToolStripItem toolStripItem = obj as ToolStripItem;
					if (toolStripItem != null && toolStripItem.Visible)
					{
						ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)this.host.GetDesigner(toolStripItem);
						if (toolStripItemDesigner != null)
						{
							toolStripItemDesigner.GetGlyphs(ref glyphCollection, this.StandardBehavior);
						}
					}
				}
			}
			if ((this.SelectionRules & SelectionRules.Moveable) != SelectionRules.None && this.InheritanceAttribute != InheritanceAttribute.InheritedReadOnly && selType != GlyphSelectionType.NotSelected)
			{
				Point location = base.BehaviorService.ControlToAdornerWindow((Control)base.Component);
				Rectangle containerBounds = new Rectangle(location, ((Control)base.Component).Size);
				int num = (int)((double)DesignerUtils.CONTAINERGRABHANDLESIZE * 0.5);
				if (containerBounds.Width < 2 * DesignerUtils.CONTAINERGRABHANDLESIZE)
				{
					num = -1 * num;
				}
				ContainerSelectorBehavior behavior = new ContainerSelectorBehavior(this.ToolStrip, base.Component.Site, true);
				ContainerSelectorGlyph value = new ContainerSelectorGlyph(containerBounds, DesignerUtils.CONTAINERGRABHANDLESIZE, num, behavior);
				glyphCollection.Insert(0, value);
			}
			return glyphCollection;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000CFF34 File Offset: 0x000CE134
		protected override bool GetHitTest(Point point)
		{
			point = this.Control.PointToClient(point);
			return (this._miniToolStrip != null && this._miniToolStrip.Visible && this.AddItemRect.Contains(point)) || this.OverFlowButtonRect.Contains(point) || base.GetHitTest(point);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000CFF94 File Offset: 0x000CE194
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			base.AutoResizeHandles = true;
			this.host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (this.host != null)
			{
				this.componentChangeSvc = (IComponentChangeService)this.host.GetService(typeof(IComponentChangeService));
			}
			if (this.undoEngine == null)
			{
				this.undoEngine = (this.GetService(typeof(UndoEngine)) as UndoEngine);
				if (this.undoEngine != null)
				{
					this.undoEngine.Undoing += this.OnUndoing;
					this.undoEngine.Undone += this.OnUndone;
				}
			}
			this.editManager = new ToolStripEditorManager(component);
			if (this.Control.IsHandleCreated)
			{
				this.InitializeNewItemDropDown();
			}
			else
			{
				this.Control.HandleCreated += this.Control_HandleCreated;
			}
			if (this.componentChangeSvc != null)
			{
				this.componentChangeSvc.ComponentRemoved += this.ComponentChangeSvc_ComponentRemoved;
				this.componentChangeSvc.ComponentRemoving += this.ComponentChangeSvc_ComponentRemoving;
				this.componentChangeSvc.ComponentAdded += this.ComponentChangeSvc_ComponentAdded;
				this.componentChangeSvc.ComponentAdding += this.ComponentChangeSvc_ComponentAdding;
				this.componentChangeSvc.ComponentChanged += this.ComponentChangeSvc_ComponentChanged;
			}
			this.toolStripAdornerWindowService = (ToolStripAdornerWindowService)this.GetService(typeof(ToolStripAdornerWindowService));
			this.SelectionService.SelectionChanging += this.selSvc_SelectionChanging;
			this.SelectionService.SelectionChanged += this.selSvc_SelectionChanged;
			this.ToolStrip.Resize += this.ToolStrip_Resize;
			this.ToolStrip.DockChanged += this.ToolStrip_Resize;
			this.ToolStrip.LayoutCompleted += this.ToolStrip_LayoutCompleted;
			this.ToolStrip.OverflowButton.DropDown.TopLevel = false;
			if (this.CanAddItems)
			{
				new EditorServiceContext(this, TypeDescriptor.GetProperties(base.Component)["Items"], SR.GetString("ToolStripItemCollectionEditorVerb"));
				this.keyboardHandlingService = (ToolStripKeyboardHandlingService)this.GetService(typeof(ToolStripKeyboardHandlingService));
				if (this.keyboardHandlingService == null)
				{
					this.keyboardHandlingService = new ToolStripKeyboardHandlingService(base.Component.Site);
				}
				if ((ISupportInSituService)this.GetService(typeof(ISupportInSituService)) == null)
				{
					ISupportInSituService supportInSituService = new ToolStripInSituService(base.Component.Site);
				}
			}
			this.toolStripSelected = true;
			if (this.keyboardHandlingService != null)
			{
				this.KeyboardHandlingService.SelectedDesignerControl = null;
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000D0254 File Offset: 0x000CE454
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			Control control = defaultValues["Parent"] as Control;
			Form form = this.host.RootComponent as Form;
			MainMenu mainMenu = null;
			FormDocumentDesigner formDocumentDesigner = null;
			if (form != null)
			{
				formDocumentDesigner = (this.host.GetDesigner(form) as FormDocumentDesigner);
				if (formDocumentDesigner != null && formDocumentDesigner.Menu != null)
				{
					mainMenu = formDocumentDesigner.Menu;
					formDocumentDesigner.Menu = null;
				}
			}
			ToolStripPanel toolStripPanel = control as ToolStripPanel;
			if (toolStripPanel == null && control is ToolStripContentPanel)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.ToolStrip)["Dock"];
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(this.ToolStrip, DockStyle.None);
				}
			}
			if (toolStripPanel == null || this.ToolStrip is MenuStrip)
			{
				base.InitializeNewComponent(defaultValues);
			}
			if (formDocumentDesigner != null)
			{
				if (mainMenu != null)
				{
					formDocumentDesigner.Menu = mainMenu;
				}
				if (this.ToolStrip is MenuStrip)
				{
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(form)["MainMenuStrip"];
					if (propertyDescriptor2 != null && propertyDescriptor2.GetValue(form) == null)
					{
						propertyDescriptor2.SetValue(form, this.ToolStrip as MenuStrip);
					}
				}
			}
			if (toolStripPanel != null)
			{
				if (!(this.ToolStrip is MenuStrip))
				{
					PropertyDescriptor member = TypeDescriptor.GetProperties(toolStripPanel)["Controls"];
					if (this.componentChangeSvc != null)
					{
						this.componentChangeSvc.OnComponentChanging(toolStripPanel, member);
					}
					toolStripPanel.Join(this.ToolStrip, toolStripPanel.Rows.Length);
					if (this.componentChangeSvc != null)
					{
						this.componentChangeSvc.OnComponentChanged(toolStripPanel, member, toolStripPanel.Controls, toolStripPanel.Controls);
					}
					PropertyDescriptor member2 = TypeDescriptor.GetProperties(this.ToolStrip)["Location"];
					if (this.componentChangeSvc != null)
					{
						this.componentChangeSvc.OnComponentChanging(this.ToolStrip, member2);
						this.componentChangeSvc.OnComponentChanged(this.ToolStrip, member2, null, null);
						return;
					}
				}
			}
			else if (control != null)
			{
				if (this.ToolStrip is MenuStrip)
				{
					int num = -1;
					foreach (object obj in control.Controls)
					{
						Control control2 = (Control)obj;
						if (control2 is ToolStrip && control2 != this.ToolStrip)
						{
							num = control.Controls.IndexOf(control2);
						}
					}
					if (num == -1)
					{
						num = control.Controls.Count - 1;
					}
					control.Controls.SetChildIndex(this.ToolStrip, num);
					return;
				}
				int num2 = -1;
				foreach (object obj2 in control.Controls)
				{
					Control control3 = (Control)obj2;
					MenuStrip menuStrip = control3 as MenuStrip;
					if (control3 is ToolStrip && menuStrip == null)
					{
						return;
					}
					if (menuStrip != null)
					{
						num2 = control.Controls.IndexOf(control3);
						break;
					}
				}
				if (num2 == -1)
				{
					num2 = control.Controls.Count;
				}
				control.Controls.SetChildIndex(this.ToolStrip, num2 - 1);
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000D0574 File Offset: 0x000CE774
		private void InitializeNewItemDropDown()
		{
			if (!this.CanAddItems || !this.SupportEditing)
			{
				return;
			}
			ToolStrip wb = (ToolStrip)base.Component;
			this.AddNewTemplateNode(wb);
			this.selSvc_SelectionChanged(null, EventArgs.Empty);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000D05B1 File Offset: 0x000CE7B1
		internal static bool IsGlyphTotallyVisible(Rectangle itemBounds, Rectangle parentBounds)
		{
			return parentBounds.Contains(itemBounds);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000D05BC File Offset: 0x000CE7BC
		private bool ItemParentIsOverflow(ToolStripItem item)
		{
			ToolStripDropDown toolStripDropDown = item.Owner as ToolStripDropDown;
			if (toolStripDropDown != null)
			{
				while (toolStripDropDown != null && !(toolStripDropDown is ToolStripOverflow))
				{
					if (toolStripDropDown.OwnerItem != null)
					{
						toolStripDropDown = (toolStripDropDown.OwnerItem.GetCurrentParent() as ToolStripDropDown);
					}
					else
					{
						toolStripDropDown = null;
					}
				}
			}
			return toolStripDropDown is ToolStripOverflow;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000D060B File Offset: 0x000CE80B
		private void LayoutToolStrip()
		{
			if (!this.disposed)
			{
				this.ToolStrip.PerformLayout();
			}
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000D0620 File Offset: 0x000CE820
		internal static string NameFromText(string text, Type componentType, IServiceProvider serviceProvider, bool adjustCapitalization)
		{
			string text2 = ToolStripDesigner.NameFromText(text, componentType, serviceProvider);
			if (adjustCapitalization)
			{
				string text3 = ToolStripDesigner.NameFromText(null, typeof(ToolStripMenuItem), serviceProvider);
				if (!string.IsNullOrEmpty(text3) && char.IsUpper(text3[0]))
				{
					text2 = char.ToUpper(text2[0], CultureInfo.InvariantCulture).ToString() + text2.Substring(1);
				}
			}
			return text2;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000D0688 File Offset: 0x000CE888
		internal static string NameFromText(string text, Type componentType, IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				return null;
			}
			INameCreationService nameCreationService = serviceProvider.GetService(typeof(INameCreationService)) as INameCreationService;
			IContainer container = (IContainer)serviceProvider.GetService(typeof(IContainer));
			if (nameCreationService == null || container == null)
			{
				return null;
			}
			string text2 = nameCreationService.CreateName(container, componentType);
			if (text == null || text.Length == 0 || text == "-")
			{
				return text2;
			}
			string name = componentType.Name;
			StringBuilder stringBuilder = new StringBuilder(text.Length + name.Length);
			bool flag = false;
			foreach (char c in text)
			{
				if (flag)
				{
					if (char.IsLower(c))
					{
						c = char.ToUpper(c, CultureInfo.CurrentCulture);
					}
					flag = false;
				}
				if (char.IsLetterOrDigit(c))
				{
					if (stringBuilder.Length == 0)
					{
						if (char.IsDigit(c))
						{
							goto IL_11D;
						}
						if (char.IsLower(c) != char.IsLower(text2[0]))
						{
							if (char.IsLower(c))
							{
								c = char.ToUpper(c, CultureInfo.CurrentCulture);
							}
							else
							{
								c = char.ToLower(c, CultureInfo.CurrentCulture);
							}
						}
					}
					stringBuilder.Append(c);
				}
				else if (char.IsWhiteSpace(c))
				{
					flag = true;
				}
				IL_11D:;
			}
			if (stringBuilder.Length == 0)
			{
				return text2;
			}
			stringBuilder.Append(name);
			string text3 = stringBuilder.ToString();
			if (container.Components[text3] != null)
			{
				string text4 = text3;
				int num = 1;
				while (!nameCreationService.IsValidName(text4) || container.Components[text4] != null)
				{
					text4 = text3 + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				return text4;
			}
			if (!nameCreationService.IsValidName(text3))
			{
				return text2;
			}
			return text3;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000D0844 File Offset: 0x000CEA44
		protected override void OnContextMenu(int x, int y)
		{
			Component component = this.SelectionService.PrimarySelection as Component;
			if (component is ToolStrip)
			{
				this.DesignerContextMenu.Show(x, y);
			}
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000D0877 File Offset: 0x000CEA77
		protected override void OnDragEnter(DragEventArgs de)
		{
			base.OnDragEnter(de);
			this.SetDragDropEffects(de);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000D0887 File Offset: 0x000CEA87
		protected override void OnDragOver(DragEventArgs de)
		{
			base.OnDragOver(de);
			this.SetDragDropEffects(de);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000D0898 File Offset: 0x000CEA98
		protected override void OnDragDrop(DragEventArgs de)
		{
			base.OnDragDrop(de);
			bool flag = false;
			ToolStrip toolStrip = this.ToolStrip;
			NativeMethods.POINT point = new NativeMethods.POINT(de.X, de.Y);
			NativeMethods.MapWindowPoints(IntPtr.Zero, toolStrip.Handle, point, 1);
			Point point2 = new Point(point.x, point.y);
			if (this.ToolStrip.Orientation == Orientation.Horizontal)
			{
				if (this.ToolStrip.RightToLeft == RightToLeft.Yes)
				{
					if (point2.X >= toolStrip.Items[0].Bounds.X)
					{
						flag = true;
					}
				}
				else if (point2.X <= toolStrip.Items[0].Bounds.X)
				{
					flag = true;
				}
			}
			else if (point2.Y <= toolStrip.Items[0].Bounds.Y)
			{
				flag = true;
			}
			ToolStripItemDataObject toolStripItemDataObject = de.Data as ToolStripItemDataObject;
			if (toolStripItemDataObject != null && toolStripItemDataObject.Owner == toolStrip)
			{
				ArrayList arrayList = toolStripItemDataObject.DragComponents;
				ToolStripItem toolStripItem = toolStripItemDataObject.PrimarySelection;
				int num = -1;
				bool flag2 = de.Effect == DragDropEffects.Copy;
				string @string;
				if (arrayList.Count == 1)
				{
					string text = TypeDescriptor.GetComponentName(arrayList[0]);
					if (text == null || text.Length == 0)
					{
						text = arrayList[0].GetType().Name;
					}
					@string = SR.GetString(flag2 ? "BehaviorServiceCopyControl" : "BehaviorServiceMoveControl", new object[]
					{
						text
					});
				}
				else
				{
					@string = SR.GetString(flag2 ? "BehaviorServiceCopyControls" : "BehaviorServiceMoveControls", new object[]
					{
						arrayList.Count
					});
				}
				DesignerTransaction designerTransaction = this.host.CreateTransaction(@string);
				try
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanging(toolStrip, TypeDescriptor.GetProperties(toolStrip)["Items"]);
					}
					if (flag2)
					{
						if (toolStripItem != null)
						{
							num = arrayList.IndexOf(toolStripItem);
						}
						if (this.KeyboardHandlingService != null)
						{
							this.KeyboardHandlingService.CopyInProgress = true;
						}
						arrayList = (DesignerUtils.CopyDragObjects(arrayList, base.Component.Site) as ArrayList);
						if (this.KeyboardHandlingService != null)
						{
							this.KeyboardHandlingService.CopyInProgress = false;
						}
						if (num != -1)
						{
							toolStripItem = (arrayList[num] as ToolStripItem);
						}
					}
					if (de.Effect == DragDropEffects.Move || flag2)
					{
						for (int i = 0; i < arrayList.Count; i++)
						{
							if (flag)
							{
								toolStrip.Items.Insert(0, arrayList[i] as ToolStripItem);
							}
							else
							{
								toolStrip.Items.Add(arrayList[i] as ToolStripItem);
							}
						}
						ToolStripDropDownItem toolStripDropDownItem = toolStripItem as ToolStripDropDownItem;
						if (toolStripDropDownItem != null)
						{
							ToolStripMenuItemDesigner toolStripMenuItemDesigner = this.host.GetDesigner(toolStripDropDownItem) as ToolStripMenuItemDesigner;
							if (toolStripMenuItemDesigner != null)
							{
								toolStripMenuItemDesigner.InitializeDropDown();
							}
						}
						this.SelectionService.SetSelectedComponents(new IComponent[]
						{
							toolStripItem
						}, SelectionTypes.Replace | SelectionTypes.Click);
					}
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(toolStrip, TypeDescriptor.GetProperties(toolStrip)["Items"], null, null);
					}
					if (flag2 && componentChangeService != null)
					{
						componentChangeService.OnComponentChanging(toolStrip, TypeDescriptor.GetProperties(toolStrip)["Items"]);
						componentChangeService.OnComponentChanged(toolStrip, TypeDescriptor.GetProperties(toolStrip)["Items"], null, null);
					}
					base.BehaviorService.SyncSelection();
				}
				catch
				{
					if (designerTransaction != null)
					{
						designerTransaction.Cancel();
						designerTransaction = null;
					}
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
						designerTransaction = null;
					}
				}
			}
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000D0C50 File Offset: 0x000CEE50
		private void OnItemAdded(object sender, ToolStripItemEventArgs e)
		{
			if (this.editorNode != null && e.Item != this.editorNode)
			{
				int num = this.ToolStrip.Items.IndexOf(this.editorNode);
				if (num == -1 || num != this.ToolStrip.Items.Count - 1)
				{
					this.ToolStrip.ItemAdded -= this.OnItemAdded;
					this.ToolStrip.SuspendLayout();
					this.ToolStrip.Items.Add(this.editorNode);
					this.ToolStrip.ResumeLayout();
					this.ToolStrip.ItemAdded += this.OnItemAdded;
				}
			}
			this.LayoutToolStrip();
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000D0D0A File Offset: 0x000CEF0A
		protected override void OnMouseDragMove(int x, int y)
		{
			if (!this.SelectionService.GetComponentSelected(this.ToolStrip))
			{
				base.OnMouseDragMove(x, y);
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000D0D27 File Offset: 0x000CEF27
		private void OnOverflowDropDownClosing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			e.Cancel = (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000D0D38 File Offset: 0x000CEF38
		private void OnOverFlowDropDownClosed(object sender, EventArgs e)
		{
			ToolStripDropDownItem toolStripDropDownItem = sender as ToolStripDropDownItem;
			if (this.toolStripAdornerWindowService != null && toolStripDropDownItem != null)
			{
				this.toolStripAdornerWindowService.Invalidate(toolStripDropDownItem.DropDown.Bounds);
				this.RemoveBodyGlyphsForOverflow();
			}
			ToolStripItem toolStripItem = this.SelectionService.PrimarySelection as ToolStripItem;
			if (toolStripItem != null && toolStripItem.IsOnOverflow)
			{
				ToolStripItem nextItem = this.ToolStrip.GetNextItem(this.ToolStrip.OverflowButton, ArrowDirection.Left);
				if (nextItem != null)
				{
					this.SelectionService.SetSelectedComponents(new IComponent[]
					{
						nextItem
					}, SelectionTypes.Replace);
				}
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000D0DC0 File Offset: 0x000CEFC0
		private void OnOverFlowDropDownOpened(object sender, EventArgs e)
		{
			if (this.editorNode != null)
			{
				this.editorNode.Control.Visible = true;
				this.editorNode.Visible = true;
			}
			ToolStripDropDownItem toolStripDropDownItem = sender as ToolStripDropDownItem;
			if (toolStripDropDownItem != null)
			{
				this.RemoveBodyGlyphsForOverflow();
				this.AddBodyGlyphsForOverflow();
			}
			ToolStripItem toolStripItem = this.SelectionService.PrimarySelection as ToolStripItem;
			if (toolStripItem == null || (toolStripItem != null && !toolStripItem.IsOnOverflow))
			{
				ToolStripItem nextItem = toolStripDropDownItem.DropDown.GetNextItem(null, ArrowDirection.Down);
				if (nextItem != null)
				{
					this.SelectionService.SetSelectedComponents(new IComponent[]
					{
						nextItem
					}, SelectionTypes.Replace);
					base.BehaviorService.Invalidate(base.BehaviorService.ControlRectInAdornerWindow(this.ToolStrip));
				}
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000D0E6C File Offset: 0x000CF06C
		private void OnOverFlowDropDownPaint(object sender, PaintEventArgs e)
		{
			foreach (object obj in this.ToolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Visible && toolStripItem.IsOnOverflow && this.SelectionService.GetComponentSelected(toolStripItem))
				{
					ToolStripItemDesigner toolStripItemDesigner = this.host.GetDesigner(toolStripItem) as ToolStripItemDesigner;
					if (toolStripItemDesigner != null)
					{
						Rectangle glyphBounds = toolStripItemDesigner.GetGlyphBounds();
						ToolStripDesignerUtils.GetAdjustedBounds(toolStripItem, ref glyphBounds);
						glyphBounds.Inflate(2, 2);
						BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
						if (behaviorService != null)
						{
							behaviorService.ProcessPaintMessage(glyphBounds);
						}
					}
				}
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000D0F38 File Offset: 0x000CF138
		private void OnOverFlowDropDownOpening(object sender, EventArgs e)
		{
			ToolStripDropDownItem toolStripDropDownItem = sender as ToolStripDropDownItem;
			if (toolStripDropDownItem.DropDown.TopLevel)
			{
				toolStripDropDownItem.DropDown.TopLevel = false;
			}
			if (this.toolStripAdornerWindowService != null)
			{
				this.ToolStrip.SuspendLayout();
				toolStripDropDownItem.DropDown.Parent = this.toolStripAdornerWindowService.ToolStripAdornerWindowControl;
				this.ToolStrip.ResumeLayout();
			}
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000D0F9C File Offset: 0x000CF19C
		private void OnOverflowDropDownResize(object sender, EventArgs e)
		{
			ToolStripDropDown toolStripDropDown = sender as ToolStripDropDown;
			if (toolStripDropDown.Visible)
			{
				this.RemoveBodyGlyphsForOverflow();
				this.AddBodyGlyphsForOverflow();
			}
			if (this.toolStripAdornerWindowService != null && toolStripDropDown != null)
			{
				this.toolStripAdornerWindowService.Invalidate();
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000D0FDC File Offset: 0x000CF1DC
		protected override void OnSetCursor()
		{
			if (this.toolboxService == null)
			{
				this.toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
			}
			if (this.toolboxService == null || !this.toolboxService.SetCursor() || this.InheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly))
			{
				Cursor.Current = Cursors.Default;
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000D1040 File Offset: 0x000CF240
		private void OnUndone(object source, EventArgs e)
		{
			if (this.editorNode != null && this.ToolStrip.Items.IndexOf(this.editorNode) == -1)
			{
				this.ToolStrip.Items.Add(this.editorNode);
			}
			if (this.undoingCalled)
			{
				this.ToolStrip.ResumeLayout(true);
				this.ToolStrip.PerformLayout();
				ToolStripDropDownItem toolStripDropDownItem = this.SelectionService.PrimarySelection as ToolStripDropDownItem;
				if (toolStripDropDownItem != null)
				{
					ToolStripMenuItemDesigner toolStripMenuItemDesigner = this.host.GetDesigner(toolStripDropDownItem) as ToolStripMenuItemDesigner;
					if (toolStripMenuItemDesigner != null)
					{
						toolStripMenuItemDesigner.InitializeBodyGlyphsForItems(false, toolStripDropDownItem);
						toolStripMenuItemDesigner.InitializeBodyGlyphsForItems(true, toolStripDropDownItem);
					}
				}
				this.undoingCalled = false;
			}
			base.BehaviorService.SyncSelection();
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000D10EF File Offset: 0x000CF2EF
		private void OnUndoing(object source, EventArgs e)
		{
			if (this.CheckIfItemSelected() || this.SelectionService.GetComponentSelected(this.ToolStrip))
			{
				this.undoingCalled = true;
				this.ToolStrip.SuspendLayout();
			}
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000D111E File Offset: 0x000CF31E
		private void OnToolStripMove(object sender, EventArgs e)
		{
			if (this.SelectionService.GetComponentSelected(this.ToolStrip))
			{
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000D1140 File Offset: 0x000CF340
		private void OnToolStripVisibleChanged(object sender, EventArgs e)
		{
			ToolStrip toolStrip = sender as ToolStrip;
			if (toolStrip != null && !toolStrip.Visible)
			{
				SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
				Glyph[] array = new Glyph[selectionManager.BodyGlyphAdorner.Glyphs.Count];
				selectionManager.BodyGlyphAdorner.Glyphs.CopyTo(array, 0);
				foreach (Glyph glyph in array)
				{
					if (glyph is ToolStripItemGlyph)
					{
						selectionManager.BodyGlyphAdorner.Glyphs.Remove(glyph);
					}
				}
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000D11D4 File Offset: 0x000CF3D4
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"Visible",
				"AllowDrop",
				"AllowItemReorder"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(ToolStripDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x000D1248 File Offset: 0x000CF448
		private void RemoveBodyGlyphsForOverflow()
		{
			foreach (object obj in this.ToolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (!(toolStripItem is DesignerToolStripControlHost) && toolStripItem.Placement == ToolStripItemPlacement.Overflow)
				{
					ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)this.host.GetDesigner(toolStripItem);
					if (toolStripItemDesigner != null)
					{
						ControlBodyGlyph bodyGlyph = toolStripItemDesigner.bodyGlyph;
						if (bodyGlyph != null && this.toolStripAdornerWindowService != null && this.toolStripAdornerWindowService.DropDownAdorner.Glyphs.Contains(bodyGlyph))
						{
							this.toolStripAdornerWindowService.DropDownAdorner.Glyphs.Remove(bodyGlyph);
						}
					}
				}
			}
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000D1308 File Offset: 0x000CF508
		internal void RollBack()
		{
			if (this.tn != null)
			{
				this.tn.RollBack();
				this.editorNode.Width = this.tn.EditorToolStrip.Width;
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000D1338 File Offset: 0x000CF538
		private void ResetVisible()
		{
			this.Visible = true;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000D1344 File Offset: 0x000CF544
		private void SetDragDropEffects(DragEventArgs de)
		{
			ToolStripItemDataObject toolStripItemDataObject = de.Data as ToolStripItemDataObject;
			if (toolStripItemDataObject != null)
			{
				if (toolStripItemDataObject.Owner != this.ToolStrip)
				{
					de.Effect = DragDropEffects.None;
					return;
				}
				de.Effect = ((Control.ModifierKeys == Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);
			}
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000D138C File Offset: 0x000CF58C
		private void selSvc_SelectionChanging(object sender, EventArgs e)
		{
			if (this.toolStripSelected && this.tn != null && this.tn.Active)
			{
				this.tn.Commit(false, false);
			}
			if (!this.CheckIfItemSelected() && !this.SelectionService.GetComponentSelected(this.ToolStrip))
			{
				this.ToolStrip.Visible = this.currentVisible;
				if (!this.currentVisible && this.parentNotVisible)
				{
					this.ToolStrip.Parent.Visible = this.currentVisible;
					this.parentNotVisible = false;
				}
				if (this.ToolStrip.OverflowButton.DropDown.Visible)
				{
					this.ToolStrip.OverflowButton.HideDropDown();
				}
				if (this.editorNode != null)
				{
					this.editorNode.Visible = false;
				}
				this.ShowHideToolStripItems(false);
				this.toolStripSelected = false;
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000D1470 File Offset: 0x000CF670
		private void selSvc_SelectionChanged(object sender, EventArgs e)
		{
			if (this._miniToolStrip != null && this.host != null)
			{
				bool flag = this.CheckIfItemSelected();
				bool flag2 = flag || this.SelectionService.GetComponentSelected(this.ToolStrip);
				if (flag2)
				{
					if (this.SelectionService.GetComponentSelected(this.ToolStrip) && !this.DontCloseOverflow && this.ToolStrip.OverflowButton.DropDown.Visible)
					{
						this.ToolStrip.OverflowButton.HideDropDown();
					}
					this.ShowHideToolStripItems(true);
					if (!this.currentVisible || !this.Control.Visible)
					{
						this.Control.Visible = true;
						if (this.ToolStrip.Parent is ToolStripPanel && !this.ToolStrip.Parent.Visible)
						{
							this.parentNotVisible = true;
							this.ToolStrip.Parent.Visible = true;
						}
						base.BehaviorService.SyncSelection();
					}
					if (this.editorNode != null && (this.SelectionService.PrimarySelection == this.ToolStrip || flag))
					{
						bool flag3 = this.FireSyncSelection;
						ToolStripPanel toolStripPanel = this.ToolStrip.Parent as ToolStripPanel;
						try
						{
							if (toolStripPanel != null)
							{
								toolStripPanel.LocationChanged += this.OnToolStripMove;
							}
							this.FireSyncSelection = true;
							this.editorNode.Visible = true;
						}
						finally
						{
							this.FireSyncSelection = flag3;
							if (toolStripPanel != null)
							{
								toolStripPanel.LocationChanged -= this.OnToolStripMove;
							}
						}
					}
					if (!(this.SelectionService.PrimarySelection is ToolStripItem) && this.KeyboardHandlingService != null)
					{
						ToolStripItem toolStripItem = this.KeyboardHandlingService.SelectedDesignerControl as ToolStripItem;
					}
					this.toolStripSelected = true;
				}
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000D1634 File Offset: 0x000CF834
		private bool ShouldSerializeVisible()
		{
			return !this.Visible;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0009EF25 File Offset: 0x0009D125
		private bool ShouldSerializeAllowDrop()
		{
			return (bool)base.ShadowProperties["AllowDrop"];
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000CE3FB File Offset: 0x000CC5FB
		private bool ShouldSerializeAllowItemReorder()
		{
			return (bool)base.ShadowProperties["AllowItemReorder"];
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000D1640 File Offset: 0x000CF840
		internal void ShowEditNode(bool clicked)
		{
			if (this.ToolStrip is MenuStrip)
			{
				if (this.KeyboardHandlingService != null)
				{
					this.KeyboardHandlingService.ResetActiveTemplateNodeSelectionState();
				}
				try
				{
					ToolStripItem toolStripItem = this.AddNewItem(typeof(ToolStripMenuItem));
					if (toolStripItem != null)
					{
						ToolStripItemDesigner toolStripItemDesigner = this.host.GetDesigner(toolStripItem) as ToolStripItemDesigner;
						if (toolStripItemDesigner != null)
						{
							toolStripItemDesigner.dummyItemAdded = true;
							((ToolStripMenuItemDesigner)toolStripItemDesigner).InitializeDropDown();
							try
							{
								this.addingDummyItem = true;
								toolStripItemDesigner.ShowEditNode(clicked);
							}
							finally
							{
								this.addingDummyItem = false;
							}
						}
					}
				}
				catch (InvalidOperationException ex)
				{
					IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
					iuiservice.ShowError(ex.Message);
					if (this.KeyboardHandlingService != null)
					{
						this.KeyboardHandlingService.ResetActiveTemplateNodeSelectionState();
					}
				}
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000D171C File Offset: 0x000CF91C
		private void ShowHideToolStripItems(bool toolStripSelected)
		{
			foreach (object obj in this.ToolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (!(toolStripItem is DesignerToolStripControlHost))
				{
					ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)this.host.GetDesigner(toolStripItem);
					if (toolStripItemDesigner != null)
					{
						toolStripItemDesigner.SetItemVisible(toolStripSelected, this);
					}
				}
			}
			if (this.FireSyncSelection)
			{
				base.BehaviorService.SyncSelection();
				this.FireSyncSelection = false;
			}
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000D17B4 File Offset: 0x000CF9B4
		private void ToolStrip_LayoutCompleted(object sender, EventArgs e)
		{
			if (this.FireSyncSelection)
			{
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000D17CC File Offset: 0x000CF9CC
		private void ToolStrip_Resize(object sender, EventArgs e)
		{
			if (!this.addingDummyItem && !this.disposed && (this.CheckIfItemSelected() || this.SelectionService.GetComponentSelected(this.ToolStrip)))
			{
				if (this._miniToolStrip != null && this._miniToolStrip.Visible)
				{
					this.LayoutToolStrip();
				}
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000D182C File Offset: 0x000CFA2C
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 123)
			{
				if (msg != 513 && msg != 516)
				{
					base.WndProc(ref m);
					return;
				}
				this.Commit();
				base.WndProc(ref m);
				return;
			}
			else
			{
				int x = NativeMethods.Util.SignedLOWORD(m.LParam);
				int y = NativeMethods.Util.SignedHIWORD(m.LParam);
				bool hitTest = this.GetHitTest(new Point(x, y));
				if (hitTest)
				{
					return;
				}
				base.WndProc(ref m);
				return;
			}
		}

		// Token: 0x04001968 RID: 6504
		private const int GLYPHBORDER = 2;

		// Token: 0x04001969 RID: 6505
		internal static Point LastCursorPosition = Point.Empty;

		// Token: 0x0400196A RID: 6506
		internal static bool _autoAddNewItems = true;

		// Token: 0x0400196B RID: 6507
		internal static ToolStripItem dragItem = null;

		// Token: 0x0400196C RID: 6508
		internal static bool shiftState = false;

		// Token: 0x0400196D RID: 6509
		internal static bool editTemplateNode = false;

		// Token: 0x0400196E RID: 6510
		private DesignerToolStripControlHost editorNode;

		// Token: 0x0400196F RID: 6511
		private ToolStripEditorManager editManager;

		// Token: 0x04001970 RID: 6512
		private ToolStrip _miniToolStrip;

		// Token: 0x04001971 RID: 6513
		private DesignerTransaction _insertMenuItemTransaction;

		// Token: 0x04001972 RID: 6514
		private Rectangle dragBoxFromMouseDown = Rectangle.Empty;

		// Token: 0x04001973 RID: 6515
		private int indexOfItemUnderMouseToDrag = -1;

		// Token: 0x04001974 RID: 6516
		private ToolStripTemplateNode tn;

		// Token: 0x04001975 RID: 6517
		private ISelectionService _selectionSvc;

		// Token: 0x04001976 RID: 6518
		private uint _editingCollection;

		// Token: 0x04001977 RID: 6519
		private DesignerTransaction _pendingTransaction;

		// Token: 0x04001978 RID: 6520
		private bool _addingItem;

		// Token: 0x04001979 RID: 6521
		private Rectangle boundsToInvalidate = Rectangle.Empty;

		// Token: 0x0400197A RID: 6522
		private bool currentVisible = true;

		// Token: 0x0400197B RID: 6523
		private ToolStripActionList _actionLists;

		// Token: 0x0400197C RID: 6524
		private ToolStripAdornerWindowService toolStripAdornerWindowService;

		// Token: 0x0400197D RID: 6525
		private IDesignerHost host;

		// Token: 0x0400197E RID: 6526
		private IComponentChangeService componentChangeSvc;

		// Token: 0x0400197F RID: 6527
		private UndoEngine undoEngine;

		// Token: 0x04001980 RID: 6528
		private bool undoingCalled;

		// Token: 0x04001981 RID: 6529
		private IToolboxService toolboxService;

		// Token: 0x04001982 RID: 6530
		private ContextMenuStrip toolStripContextMenu;

		// Token: 0x04001983 RID: 6531
		private bool toolStripSelected;

		// Token: 0x04001984 RID: 6532
		private bool cacheItems;

		// Token: 0x04001985 RID: 6533
		private ArrayList items;

		// Token: 0x04001986 RID: 6534
		private bool disposed;

		// Token: 0x04001987 RID: 6535
		private DesignerTransaction newItemTransaction;

		// Token: 0x04001988 RID: 6536
		private bool fireSyncSelection;

		// Token: 0x04001989 RID: 6537
		private ToolStripKeyboardHandlingService keyboardHandlingService;

		// Token: 0x0400198A RID: 6538
		private bool parentNotVisible;

		// Token: 0x0400198B RID: 6539
		private bool dontCloseOverflow;

		// Token: 0x0400198C RID: 6540
		private bool addingDummyItem;
	}
}
