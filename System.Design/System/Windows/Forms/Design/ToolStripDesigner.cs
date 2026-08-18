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
	// Token: 0x020001A3 RID: 419
	internal class ToolStripDesigner : ControlDesigner
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00045C90 File Offset: 0x00044C90
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

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00045D04 File Offset: 0x00044D04
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

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00045D30 File Offset: 0x00044D30
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x00045D47 File Offset: 0x00044D47
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00045D7A File Offset: 0x00044D7A
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x00045D91 File Offset: 0x00044D91
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00045DC4 File Offset: 0x00044DC4
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00045E38 File Offset: 0x00044E38
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00045E40 File Offset: 0x00044E40
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00045E4C File Offset: 0x00044E4C
		private bool CanAddItems
		{
			get
			{
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this.ToolStrip)[typeof(InheritanceAttribute)];
				return inheritanceAttribute == null || inheritanceAttribute.InheritanceLevel == InheritanceLevel.NotInherited;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00045E88 File Offset: 0x00044E88
		internal override bool ControlSupportsSnaplines
		{
			get
			{
				return !(this.ToolStrip.Parent is ToolStripPanel);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00045E9F File Offset: 0x00044E9F
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00045EDB File Offset: 0x00044EDB
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x00045EE3 File Offset: 0x00044EE3
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

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00045EEC File Offset: 0x00044EEC
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x00045EF4 File Offset: 0x00044EF4
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00045EFD File Offset: 0x00044EFD
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x00045F0B File Offset: 0x00044F0B
		internal bool EditingCollection
		{
			get
			{
				return this._editingCollection != 0U;
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

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00045F2D File Offset: 0x00044F2D
		public ToolStripEditorManager EditManager
		{
			get
			{
				return this.editManager;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00045F35 File Offset: 0x00044F35
		internal ToolStripTemplateNode Editor
		{
			get
			{
				return this.tn;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00045F3D File Offset: 0x00044F3D
		public DesignerToolStripControlHost EditorNode
		{
			get
			{
				return this.editorNode;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00045F45 File Offset: 0x00044F45
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x00045F4D File Offset: 0x00044F4D
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

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00045F6D File Offset: 0x00044F6D
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x00045F75 File Offset: 0x00044F75
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

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00045F7E File Offset: 0x00044F7E
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00045F86 File Offset: 0x00044F86
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

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00045F8F File Offset: 0x00044F8F
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

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00045FAA File Offset: 0x00044FAA
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x00045FB2 File Offset: 0x00044FB2
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

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00045FBB File Offset: 0x00044FBB
		private bool IsToolStripOrItemSelected
		{
			get
			{
				return this.toolStripSelected;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00045FC3 File Offset: 0x00044FC3
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00045FDE File Offset: 0x00044FDE
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x00045FE6 File Offset: 0x00044FE6
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

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00045FF0 File Offset: 0x00044FF0
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

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00046029 File Offset: 0x00045029
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

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00046054 File Offset: 0x00045054
		public bool SupportEditing
		{
			get
			{
				WindowsFormsDesignerOptionService windowsFormsDesignerOptionService = this.GetService(typeof(DesignerOptionService)) as WindowsFormsDesignerOptionService;
				return windowsFormsDesignerOptionService == null || windowsFormsDesignerOptionService.CompatibilityOptions.EnableInSituEditing;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00046087 File Offset: 0x00045087
		protected ToolStrip ToolStrip
		{
			get
			{
				return (ToolStrip)base.Component;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00046094 File Offset: 0x00045094
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x000460E8 File Offset: 0x000450E8
		internal override bool SerializePerformLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000460EB File Offset: 0x000450EB
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x000460F3 File Offset: 0x000450F3
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

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004612C File Offset: 0x0004512C
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

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0004619C File Offset: 0x0004519C
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

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00046200 File Offset: 0x00045200
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

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000462F8 File Offset: 0x000452F8
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

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0004667C File Offset: 0x0004567C
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

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004671D File Offset: 0x0004571D
		internal void CancelPendingMenuItemTransaction()
		{
			if (this._insertMenuItemTransaction != null)
			{
				this._insertMenuItemTransaction.Cancel();
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00046734 File Offset: 0x00045734
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

		// Token: 0x06000FEB RID: 4075 RVA: 0x00046910 File Offset: 0x00045910
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

		// Token: 0x06000FEC RID: 4076 RVA: 0x00046A5C File Offset: 0x00045A5C
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

		// Token: 0x06000FED RID: 4077 RVA: 0x00046B84 File Offset: 0x00045B84
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

		// Token: 0x06000FEE RID: 4078 RVA: 0x00046D4C File Offset: 0x00045D4C
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

		// Token: 0x06000FEF RID: 4079 RVA: 0x00046E30 File Offset: 0x00045E30
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

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00046ED4 File Offset: 0x00045ED4
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
						component2 = this.ToolStrip;
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

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00047090 File Offset: 0x00046090
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

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00047150 File Offset: 0x00046150
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

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0004748C File Offset: 0x0004648C
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

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00047500 File Offset: 0x00046500
		protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			if (!this.ToolStrip.IsHandleCreated)
			{
				return null;
			}
			SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
			if (selectionManager != null && this.ToolStrip != null && this.CanAddItems && this.ToolStrip.Visible)
			{
				base.BehaviorService.ControlToAdornerWindow(this.ToolStrip);
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

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00047774 File Offset: 0x00046774
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

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000478E0 File Offset: 0x000468E0
		protected override bool GetHitTest(Point point)
		{
			point = this.Control.PointToClient(point);
			return (this._miniToolStrip != null && this._miniToolStrip.Visible && this.AddItemRect.Contains(point)) || this.OverFlowButtonRect.Contains(point) || base.GetHitTest(point);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00047940 File Offset: 0x00046940
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

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00047C00 File Offset: 0x00046C00
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

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00047F20 File Offset: 0x00046F20
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

		// Token: 0x06000FFA RID: 4090 RVA: 0x00047F5D File Offset: 0x00046F5D
		internal static bool IsGlyphTotallyVisible(Rectangle itemBounds, Rectangle parentBounds)
		{
			return parentBounds.Contains(itemBounds);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00047F68 File Offset: 0x00046F68
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

		// Token: 0x06000FFC RID: 4092 RVA: 0x00047FB7 File Offset: 0x00046FB7
		private void LayoutToolStrip()
		{
			if (!this.disposed)
			{
				this.ToolStrip.PerformLayout();
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00047FCC File Offset: 0x00046FCC
		internal static string NameFromText(string text, Type componentType, IServiceProvider serviceProvider, bool adjustCapitalization)
		{
			string text2 = ToolStripDesigner.NameFromText(text, componentType, serviceProvider);
			if (adjustCapitalization)
			{
				string text3 = ToolStripDesigner.NameFromText(null, typeof(ToolStripMenuItem), serviceProvider);
				if (!string.IsNullOrEmpty(text3) && char.IsUpper(text3[0]))
				{
					text2 = char.ToUpper(text2[0], CultureInfo.InvariantCulture) + text2.Substring(1);
				}
			}
			return text2;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00048034 File Offset: 0x00047034
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

		// Token: 0x06000FFF RID: 4095 RVA: 0x000481F0 File Offset: 0x000471F0
		protected override void OnContextMenu(int x, int y)
		{
			Component component = this.SelectionService.PrimarySelection as Component;
			if (component is ToolStrip)
			{
				this.DesignerContextMenu.Show(x, y);
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00048223 File Offset: 0x00047223
		protected override void OnDragEnter(DragEventArgs de)
		{
			base.OnDragEnter(de);
			this.SetDragDropEffects(de);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00048233 File Offset: 0x00047233
		protected override void OnDragOver(DragEventArgs de)
		{
			base.OnDragOver(de);
			this.SetDragDropEffects(de);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00048244 File Offset: 0x00047244
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

		// Token: 0x06001003 RID: 4099 RVA: 0x0004860C File Offset: 0x0004760C
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

		// Token: 0x06001004 RID: 4100 RVA: 0x000486C6 File Offset: 0x000476C6
		protected override void OnMouseDragMove(int x, int y)
		{
			if (!this.SelectionService.GetComponentSelected(this.ToolStrip))
			{
				base.OnMouseDragMove(x, y);
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000486E3 File Offset: 0x000476E3
		private void OnOverflowDropDownClosing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			e.Cancel = (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000486F4 File Offset: 0x000476F4
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

		// Token: 0x06001007 RID: 4103 RVA: 0x00048780 File Offset: 0x00047780
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

		// Token: 0x06001008 RID: 4104 RVA: 0x00048830 File Offset: 0x00047830
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

		// Token: 0x06001009 RID: 4105 RVA: 0x000488FC File Offset: 0x000478FC
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

		// Token: 0x0600100A RID: 4106 RVA: 0x00048960 File Offset: 0x00047960
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

		// Token: 0x0600100B RID: 4107 RVA: 0x000489A0 File Offset: 0x000479A0
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

		// Token: 0x0600100C RID: 4108 RVA: 0x00048A04 File Offset: 0x00047A04
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

		// Token: 0x0600100D RID: 4109 RVA: 0x00048AB3 File Offset: 0x00047AB3
		private void OnUndoing(object source, EventArgs e)
		{
			if (this.CheckIfItemSelected() || this.SelectionService.GetComponentSelected(this.ToolStrip))
			{
				this.undoingCalled = true;
				this.ToolStrip.SuspendLayout();
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00048AE2 File Offset: 0x00047AE2
		private void OnToolStripMove(object sender, EventArgs e)
		{
			if (this.SelectionService.GetComponentSelected(this.ToolStrip))
			{
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00048B04 File Offset: 0x00047B04
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

		// Token: 0x06001010 RID: 4112 RVA: 0x00048B98 File Offset: 0x00047B98
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

		// Token: 0x06001011 RID: 4113 RVA: 0x00048C14 File Offset: 0x00047C14
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

		// Token: 0x06001012 RID: 4114 RVA: 0x00048CD4 File Offset: 0x00047CD4
		internal void RollBack()
		{
			if (this.tn != null)
			{
				this.tn.RollBack();
				this.editorNode.Width = this.tn.EditorToolStrip.Width;
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00048D04 File Offset: 0x00047D04
		private void ResetVisible()
		{
			this.Visible = true;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00048D10 File Offset: 0x00047D10
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

		// Token: 0x06001015 RID: 4117 RVA: 0x00048D58 File Offset: 0x00047D58
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

		// Token: 0x06001016 RID: 4118 RVA: 0x00048E3C File Offset: 0x00047E3C
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
					this.currentVisible = (this.Control.Visible && this.currentVisible);
					if (!this.currentVisible)
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

		// Token: 0x06001017 RID: 4119 RVA: 0x0004900C File Offset: 0x0004800C
		private bool ShouldSerializeVisible()
		{
			return !this.Visible;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00049017 File Offset: 0x00048017
		private bool ShouldSerializeAllowDrop()
		{
			return (bool)base.ShadowProperties["AllowDrop"];
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0004902E File Offset: 0x0004802E
		private bool ShouldSerializeAllowItemReorder()
		{
			return (bool)base.ShadowProperties["AllowItemReorder"];
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00049048 File Offset: 0x00048048
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

		// Token: 0x0600101B RID: 4123 RVA: 0x00049124 File Offset: 0x00048124
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

		// Token: 0x0600101C RID: 4124 RVA: 0x000491BC File Offset: 0x000481BC
		private void ToolStrip_LayoutCompleted(object sender, EventArgs e)
		{
			if (this.FireSyncSelection)
			{
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000491D4 File Offset: 0x000481D4
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

		// Token: 0x0600101E RID: 4126 RVA: 0x00049234 File Offset: 0x00048234
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

		// Token: 0x0400101D RID: 4125
		private const int GLYPHBORDER = 2;

		// Token: 0x0400101E RID: 4126
		internal static Point LastCursorPosition = Point.Empty;

		// Token: 0x0400101F RID: 4127
		internal static bool _autoAddNewItems = true;

		// Token: 0x04001020 RID: 4128
		internal static ToolStripItem dragItem = null;

		// Token: 0x04001021 RID: 4129
		internal static bool shiftState = false;

		// Token: 0x04001022 RID: 4130
		internal static bool editTemplateNode = false;

		// Token: 0x04001023 RID: 4131
		private DesignerToolStripControlHost editorNode;

		// Token: 0x04001024 RID: 4132
		private ToolStripEditorManager editManager;

		// Token: 0x04001025 RID: 4133
		private ToolStrip _miniToolStrip;

		// Token: 0x04001026 RID: 4134
		private DesignerTransaction _insertMenuItemTransaction;

		// Token: 0x04001027 RID: 4135
		private Rectangle dragBoxFromMouseDown = Rectangle.Empty;

		// Token: 0x04001028 RID: 4136
		private int indexOfItemUnderMouseToDrag = -1;

		// Token: 0x04001029 RID: 4137
		private ToolStripTemplateNode tn;

		// Token: 0x0400102A RID: 4138
		private ISelectionService _selectionSvc;

		// Token: 0x0400102B RID: 4139
		private uint _editingCollection;

		// Token: 0x0400102C RID: 4140
		private DesignerTransaction _pendingTransaction;

		// Token: 0x0400102D RID: 4141
		private bool _addingItem;

		// Token: 0x0400102E RID: 4142
		private Rectangle boundsToInvalidate = Rectangle.Empty;

		// Token: 0x0400102F RID: 4143
		private bool currentVisible = true;

		// Token: 0x04001030 RID: 4144
		private ToolStripActionList _actionLists;

		// Token: 0x04001031 RID: 4145
		private ToolStripAdornerWindowService toolStripAdornerWindowService;

		// Token: 0x04001032 RID: 4146
		private IDesignerHost host;

		// Token: 0x04001033 RID: 4147
		private IComponentChangeService componentChangeSvc;

		// Token: 0x04001034 RID: 4148
		private UndoEngine undoEngine;

		// Token: 0x04001035 RID: 4149
		private bool undoingCalled;

		// Token: 0x04001036 RID: 4150
		private IToolboxService toolboxService;

		// Token: 0x04001037 RID: 4151
		private ContextMenuStrip toolStripContextMenu;

		// Token: 0x04001038 RID: 4152
		private bool toolStripSelected;

		// Token: 0x04001039 RID: 4153
		private bool cacheItems;

		// Token: 0x0400103A RID: 4154
		private ArrayList items;

		// Token: 0x0400103B RID: 4155
		private bool disposed;

		// Token: 0x0400103C RID: 4156
		private DesignerTransaction newItemTransaction;

		// Token: 0x0400103D RID: 4157
		private bool fireSyncSelection;

		// Token: 0x0400103E RID: 4158
		private ToolStripKeyboardHandlingService keyboardHandlingService;

		// Token: 0x0400103F RID: 4159
		private bool parentNotVisible;

		// Token: 0x04001040 RID: 4160
		private bool dontCloseOverflow;

		// Token: 0x04001041 RID: 4161
		private bool addingDummyItem;
	}
}
