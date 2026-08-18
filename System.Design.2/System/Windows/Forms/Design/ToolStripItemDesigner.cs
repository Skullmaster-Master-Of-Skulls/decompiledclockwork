using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200035C RID: 860
	internal class ToolStripItemDesigner : ComponentDesigner
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x000D4EAA File Offset: 0x000D30AA
		// (set) Token: 0x06002292 RID: 8850 RVA: 0x000D4EC4 File Offset: 0x000D30C4
		internal bool AutoSize
		{
			get
			{
				return (bool)base.ShadowProperties["AutoSize"];
			}
			set
			{
				bool flag = (bool)base.ShadowProperties["AutoSize"];
				base.ShadowProperties["AutoSize"] = value;
				if (value != flag)
				{
					this.ToolStripItem.AutoSize = value;
				}
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000D4F0D File Offset: 0x000D310D
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x000D4F24 File Offset: 0x000D3124
		private string AccessibleName
		{
			get
			{
				return (string)base.ShadowProperties["AccessibleName"];
			}
			set
			{
				base.ShadowProperties["AccessibleName"] = value;
			}
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000D4F37 File Offset: 0x000D3137
		internal override bool CanBeAssociatedWith(IDesigner parentDesigner)
		{
			return parentDesigner is ToolStripDesigner;
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x000D4F44 File Offset: 0x000D3144
		private ContextMenuStrip DesignerContextMenu
		{
			get
			{
				BaseContextMenuStrip baseContextMenuStrip = new BaseContextMenuStrip(base.Component.Site, this.ToolStripItem);
				if (this.selSvc.SelectionCount > 1)
				{
					baseContextMenuStrip.GroupOrdering.Clear();
					baseContextMenuStrip.GroupOrdering.AddRange(new string[]
					{
						"Code",
						"Selection",
						"Edit",
						"Properties"
					});
				}
				else
				{
					baseContextMenuStrip.GroupOrdering.Clear();
					baseContextMenuStrip.GroupOrdering.AddRange(new string[]
					{
						"Code",
						"Custom",
						"Selection",
						"Edit",
						"Properties"
					});
					baseContextMenuStrip.Text = "CustomContextMenu";
					if (this.toolStripItemCustomMenuItemCollection == null)
					{
						this.toolStripItemCustomMenuItemCollection = new ToolStripItemCustomMenuItemCollection(base.Component.Site, this.ToolStripItem);
					}
					foreach (object obj in this.toolStripItemCustomMenuItemCollection)
					{
						ToolStripItem item = (ToolStripItem)obj;
						baseContextMenuStrip.Groups["Custom"].Items.Add(item);
					}
				}
				if (this.toolStripItemCustomMenuItemCollection != null)
				{
					this.toolStripItemCustomMenuItemCollection.RefreshItems();
				}
				baseContextMenuStrip.Populated = false;
				return baseContextMenuStrip;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x000D50A8 File Offset: 0x000D32A8
		// (set) Token: 0x06002298 RID: 8856 RVA: 0x000D50B0 File Offset: 0x000D32B0
		internal virtual ToolStripTemplateNode Editor
		{
			get
			{
				return this._editorNode;
			}
			set
			{
				this._editorNode = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x000D23F0 File Offset: 0x000D05F0
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

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x000D50B9 File Offset: 0x000D32B9
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x000D50C1 File Offset: 0x000D32C1
		internal bool IsEditorActive
		{
			get
			{
				return this.isEditorActive;
			}
			set
			{
				this.isEditorActive = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x000D50CA File Offset: 0x000D32CA
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x000D50D2 File Offset: 0x000D32D2
		internal bool InternalCreate
		{
			get
			{
				return this.internalCreate;
			}
			set
			{
				this.internalCreate = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x000D50DC File Offset: 0x000D32DC
		protected IComponent ImmediateParent
		{
			get
			{
				if (this.ToolStripItem == null)
				{
					return null;
				}
				ToolStrip currentParent = this.ToolStripItem.GetCurrentParent();
				if (currentParent == null)
				{
					return this.ToolStripItem.Owner;
				}
				return currentParent;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x000D510F File Offset: 0x000D330F
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x000D5128 File Offset: 0x000D3328
		private ToolStripItemOverflow Overflow
		{
			get
			{
				return (ToolStripItemOverflow)base.ShadowProperties["Overflow"];
			}
			set
			{
				if (this.ToolStripItem.IsOnOverflow)
				{
					ToolStrip owner = this.ToolStripItem.Owner;
					if (owner.OverflowButton.DropDown.Visible)
					{
						owner.OverflowButton.HideDropDown();
					}
				}
				if (this.ToolStripItem is ToolStripDropDownItem)
				{
					ToolStripDropDownItem toolStripDropDownItem = this.ToolStripItem as ToolStripDropDownItem;
					toolStripDropDownItem.HideDropDown();
				}
				if (value != this.ToolStripItem.Overflow)
				{
					this.ToolStripItem.Overflow = value;
					base.ShadowProperties["Overflow"] = value;
				}
				BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
				if (behaviorService != null)
				{
					behaviorService.SyncSelection();
				}
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000D51DC File Offset: 0x000D33DC
		protected override IComponent ParentComponent
		{
			get
			{
				if (this.ToolStripItem != null)
				{
					if (this.ToolStripItem.IsOnDropDown && !this.ToolStripItem.IsOnOverflow)
					{
						ToolStripDropDown toolStripDropDown = this.ImmediateParent as ToolStripDropDown;
						if (toolStripDropDown != null)
						{
							if (toolStripDropDown.IsAutoGenerated)
							{
								return toolStripDropDown.OwnerItem;
							}
							return toolStripDropDown;
						}
					}
					return this.GetMainToolStrip();
				}
				return null;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000D5233 File Offset: 0x000D3433
		public ToolStripItem ToolStripItem
		{
			get
			{
				return (ToolStripItem)base.Component;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x0009F679 File Offset: 0x0009D879
		// (set) Token: 0x060022A4 RID: 8868 RVA: 0x000D5240 File Offset: 0x000D3440
		protected bool Visible
		{
			get
			{
				return (bool)base.ShadowProperties["Visible"];
			}
			set
			{
				base.ShadowProperties["Visible"] = value;
				this.currentVisible = value;
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000D5260 File Offset: 0x000D3460
		internal ArrayList AddParentTree()
		{
			ArrayList arrayList = new ArrayList();
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				IComponent rootComponent = designerHost.RootComponent;
				Component component = this.ToolStripItem;
				if (component != null && rootComponent != null)
				{
					while (component != rootComponent)
					{
						if (component is ToolStripItem)
						{
							ToolStripItem toolStripItem = component as ToolStripItem;
							if (toolStripItem.IsOnDropDown)
							{
								if (toolStripItem.IsOnOverflow)
								{
									arrayList.Add(toolStripItem.Owner);
									component = toolStripItem.Owner;
								}
								else
								{
									ToolStripDropDown toolStripDropDown = toolStripItem.Owner as ToolStripDropDown;
									if (toolStripDropDown != null)
									{
										ToolStripItem ownerItem = toolStripDropDown.OwnerItem;
										if (ownerItem != null)
										{
											arrayList.Add(ownerItem);
											component = ownerItem;
										}
									}
								}
							}
							else
							{
								if (toolStripItem.Owner.Site != null)
								{
									arrayList.Add(toolStripItem.Owner);
								}
								component = toolStripItem.Owner;
							}
						}
						else if (component is Control)
						{
							Control control = component as Control;
							Control parent = control.Parent;
							if (parent.Site != null)
							{
								arrayList.Add(parent);
							}
							component = parent;
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000D5377 File Offset: 0x000D3577
		private void CreateDummyNode()
		{
			this._editorNode = new ToolStripTemplateNode(this.ToolStripItem, this.ToolStripItem.Text, this.ToolStripItem.Image);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000D53A0 File Offset: 0x000D35A0
		internal virtual void CommitEdit(Type type, string text, bool commit, bool enterKeyPressed, bool tabKeyPressed)
		{
			ToolStripItem toolStripItem = null;
			SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
			BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
			ToolStrip toolStrip = this.ImmediateParent as ToolStrip;
			toolStrip.SuspendLayout();
			this.HideDummyNode();
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			ToolStripDesigner toolStripDesigner = (ToolStripDesigner)designerHost.GetDesigner(this.ToolStripItem.Owner);
			if (toolStripDesigner != null && toolStripDesigner.EditManager != null)
			{
				toolStripDesigner.EditManager.ActivateEditor(null, false);
			}
			if (toolStrip is MenuStrip && type == typeof(ToolStripSeparator))
			{
				IDesignerHost designerHost2 = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost2 != null)
				{
					IUIService iuiservice = (IUIService)designerHost2.GetService(typeof(IUIService));
					if (iuiservice != null)
					{
						iuiservice.ShowError(SR.GetString("ToolStripSeparatorError"));
						commit = false;
						if (this.selSvc != null)
						{
							this.selSvc.SetSelectedComponents(new object[]
							{
								toolStrip
							});
						}
					}
				}
			}
			if (commit)
			{
				if (this.dummyItemAdded)
				{
					try
					{
						this.RemoveItem();
						toolStripItem = toolStripDesigner.AddNewItem(type, text, enterKeyPressed, false);
						goto IL_204;
					}
					finally
					{
						if (toolStripDesigner.NewItemTransaction != null)
						{
							toolStripDesigner.NewItemTransaction.Commit();
							toolStripDesigner.NewItemTransaction = null;
						}
					}
				}
				DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("ToolStripItemPropertyChangeTransaction"));
				try
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.ToolStripItem)["Text"];
					string b = (string)propertyDescriptor.GetValue(this.ToolStripItem);
					if (propertyDescriptor != null && text != b)
					{
						propertyDescriptor.SetValue(this.ToolStripItem, text);
					}
					if (enterKeyPressed && this.selSvc != null)
					{
						this.SelectNextItem(this.selSvc, enterKeyPressed, toolStripDesigner);
					}
				}
				catch (Exception ex)
				{
					if (designerTransaction != null)
					{
						designerTransaction.Cancel();
						designerTransaction = null;
					}
					if (selectionManager != null)
					{
						selectionManager.Refresh();
					}
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
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
				IL_204:
				this.dummyItemAdded = false;
			}
			else if (this.dummyItemAdded)
			{
				this.dummyItemAdded = false;
				this.RemoveItem();
				if (toolStripDesigner.NewItemTransaction != null)
				{
					toolStripDesigner.NewItemTransaction.Cancel();
					toolStripDesigner.NewItemTransaction = null;
				}
			}
			toolStrip.ResumeLayout();
			if (toolStripItem != null && !toolStripItem.IsOnDropDown)
			{
				ToolStripDropDownItem toolStripDropDownItem = toolStripItem as ToolStripDropDownItem;
				if (toolStripDropDownItem != null)
				{
					ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)designerHost.GetDesigner(toolStripItem);
					Rectangle glyphBounds = toolStripItemDesigner.GetGlyphBounds();
					Control control = designerHost.RootComponent as Control;
					if (control != null && behaviorService != null)
					{
						Rectangle parentBounds = behaviorService.ControlRectInAdornerWindow(control);
						if (!ToolStripDesigner.IsGlyphTotallyVisible(glyphBounds, parentBounds))
						{
							toolStripDropDownItem.HideDropDown();
						}
					}
				}
			}
			if (selectionManager != null)
			{
				selectionManager.Refresh();
			}
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000D5684 File Offset: 0x000D3884
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._editorNode != null)
				{
					this._editorNode.CloseEditor();
					this._editorNode = null;
				}
				if (this.ToolStripItem != null)
				{
					this.ToolStripItem.Paint -= this.OnItemPaint;
				}
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRename -= this.OnComponentRename;
				}
				if (this.selSvc != null)
				{
					this.selSvc.SelectionChanged -= this.OnSelectionChanged;
				}
				if (this.bodyGlyph != null)
				{
					ToolStripAdornerWindowService toolStripAdornerWindowService = (ToolStripAdornerWindowService)this.GetService(typeof(ToolStripAdornerWindowService));
					if (toolStripAdornerWindowService != null && toolStripAdornerWindowService.DropDownAdorner.Glyphs.Contains(this.bodyGlyph))
					{
						toolStripAdornerWindowService.DropDownAdorner.Glyphs.Remove(this.bodyGlyph);
					}
				}
				if (this.toolStripItemCustomMenuItemCollection != null && this.toolStripItemCustomMenuItemCollection.Count > 0)
				{
					foreach (object obj in this.toolStripItemCustomMenuItemCollection)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj;
						toolStripItem.Dispose();
					}
					this.toolStripItemCustomMenuItemCollection.Clear();
				}
				this.toolStripItemCustomMenuItemCollection = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000D57E8 File Offset: 0x000D39E8
		protected virtual Component GetOwnerForActionList()
		{
			if (this.ToolStripItem.Placement != ToolStripItemPlacement.Main)
			{
				return this.ToolStripItem.Owner;
			}
			return this.ToolStripItem.GetCurrentParent();
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000D580E File Offset: 0x000D3A0E
		internal virtual ToolStrip GetMainToolStrip()
		{
			return this.ToolStripItem.Owner;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000D581C File Offset: 0x000D3A1C
		public Rectangle GetGlyphBounds()
		{
			BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
			Rectangle result = Rectangle.Empty;
			if (behaviorService != null && this.ImmediateParent != null)
			{
				Point pos = behaviorService.ControlToAdornerWindow((Control)this.ImmediateParent);
				result = this.ToolStripItem.Bounds;
				result.Offset(pos);
			}
			return result;
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000D5878 File Offset: 0x000D3A78
		private void FireComponentChanging(ToolStripDropDownItem parent)
		{
			if (parent != null)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null && parent.Site != null)
				{
					componentChangeService.OnComponentChanging(parent, TypeDescriptor.GetProperties(parent)["DropDownItems"]);
				}
				foreach (object obj in parent.DropDownItems)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					ToolStripDropDownItem toolStripDropDownItem = toolStripItem as ToolStripDropDownItem;
					if (toolStripDropDownItem != null && toolStripDropDownItem.DropDownItems.Count > 1)
					{
						this.FireComponentChanging(toolStripDropDownItem);
					}
				}
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000D592C File Offset: 0x000D3B2C
		private void FireComponentChanged(ToolStripDropDownItem parent)
		{
			if (parent != null)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null && parent.Site != null)
				{
					componentChangeService.OnComponentChanged(parent, TypeDescriptor.GetProperties(parent)["DropDownItems"], null, null);
				}
				foreach (object obj in parent.DropDownItems)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					ToolStripDropDownItem toolStripDropDownItem = toolStripItem as ToolStripDropDownItem;
					if (toolStripDropDownItem != null && toolStripDropDownItem.DropDownItems.Count > 1)
					{
						this.FireComponentChanged(toolStripDropDownItem);
					}
				}
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000D59E0 File Offset: 0x000D3BE0
		public void GetGlyphs(ref GlyphCollection glyphs, Behavior standardBehavior)
		{
			if (this.ImmediateParent != null)
			{
				Rectangle glyphBounds = this.GetGlyphBounds();
				ToolStripDesignerUtils.GetAdjustedBounds(this.ToolStripItem, ref glyphBounds);
				BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
				if (behaviorService.ControlRectInAdornerWindow((Control)this.ImmediateParent).Contains(glyphBounds.Left, glyphBounds.Top))
				{
					if (this.ToolStripItem.IsOnDropDown)
					{
						ToolStrip toolStrip = this.ToolStripItem.GetCurrentParent();
						if (toolStrip == null)
						{
							toolStrip = this.ToolStripItem.Owner;
						}
						if (toolStrip != null && toolStrip.Visible)
						{
							glyphs.Add(new MiniLockedBorderGlyph(glyphBounds, SelectionBorderGlyphType.Top, standardBehavior, true));
							glyphs.Add(new MiniLockedBorderGlyph(glyphBounds, SelectionBorderGlyphType.Bottom, standardBehavior, true));
							glyphs.Add(new MiniLockedBorderGlyph(glyphBounds, SelectionBorderGlyphType.Left, standardBehavior, true));
							glyphs.Add(new MiniLockedBorderGlyph(glyphBounds, SelectionBorderGlyphType.Right, standardBehavior, true));
							return;
						}
					}
					else
					{
						glyphs.Add(new MiniLockedBorderGlyph(glyphBounds, SelectionBorderGlyphType.Top, standardBehavior, true));
						glyphs.Add(new MiniLockedBorderGlyph(glyphBounds, SelectionBorderGlyphType.Bottom, standardBehavior, true));
						glyphs.Add(new MiniLockedBorderGlyph(glyphBounds, SelectionBorderGlyphType.Left, standardBehavior, true));
						glyphs.Add(new MiniLockedBorderGlyph(glyphBounds, SelectionBorderGlyphType.Right, standardBehavior, true));
					}
				}
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000D5B10 File Offset: 0x000D3D10
		internal ToolStripDropDown GetFirstDropDown(ToolStripItem currentItem)
		{
			if (currentItem.Owner is ToolStripDropDown)
			{
				ToolStripDropDown toolStripDropDown = currentItem.Owner as ToolStripDropDown;
				while (toolStripDropDown.OwnerItem != null && toolStripDropDown.OwnerItem.Owner is ToolStripDropDown)
				{
					toolStripDropDown = (toolStripDropDown.OwnerItem.Owner as ToolStripDropDown);
				}
				return toolStripDropDown;
			}
			return null;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000D5B66 File Offset: 0x000D3D66
		private void HideDummyNode()
		{
			this.ToolStripItem.AutoSize = this.AutoSize;
			if (this._editorNode != null)
			{
				this._editorNode.CloseEditor();
				this._editorNode = null;
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000D5B94 File Offset: 0x000D3D94
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.AutoSize = this.ToolStripItem.AutoSize;
			this.Visible = true;
			this.currentVisible = this.Visible;
			this.AccessibleName = this.ToolStripItem.AccessibleName;
			this.ToolStripItem.Paint += this.OnItemPaint;
			this.ToolStripItem.AccessibleName = this.ToolStripItem.Name;
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRename += this.OnComponentRename;
			}
			this.selSvc = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (this.selSvc != null)
			{
				this.selSvc.SelectionChanged += this.OnSelectionChanged;
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000D5C70 File Offset: 0x000D3E70
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			if (!this.internalCreate)
			{
				ISite site = base.Component.Site;
				if (site != null && base.Component is ToolStripDropDownItem)
				{
					if (defaultValues == null)
					{
						defaultValues = new Hashtable();
					}
					defaultValues["Text"] = site.Name;
					IComponent component = base.Component;
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.ToolStripItem)["Text"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType.Equals(typeof(string)))
					{
						string text = (string)propertyDescriptor.GetValue(component);
						if (text == null || text.Length == 0)
						{
							propertyDescriptor.SetValue(component, site.Name);
						}
					}
				}
			}
			base.InitializeNewComponent(defaultValues);
			if (base.Component is ToolStripTextBox || base.Component is ToolStripComboBox)
			{
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(base.Component)["Text"];
				if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType == typeof(string) && !propertyDescriptor2.IsReadOnly && propertyDescriptor2.IsBrowsable)
				{
					propertyDescriptor2.SetValue(base.Component, "");
				}
			}
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000D5D98 File Offset: 0x000D3F98
		internal virtual ToolStripItem MorphCurrentItem(Type t)
		{
			ToolStripItem toolStripItem = null;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return toolStripItem;
			}
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("ToolStripMorphingItemTransaction"));
			ToolStrip toolStrip = (ToolStrip)this.ImmediateParent;
			if (toolStrip is ToolStripOverflow)
			{
				toolStrip = this.ToolStripItem.Owner;
			}
			ToolStripMenuItemDesigner toolStripMenuItemDesigner = null;
			int index = toolStrip.Items.IndexOf(this.ToolStripItem);
			string name = this.ToolStripItem.Name;
			ToolStripItem toolStripItem2 = null;
			if (this.ToolStripItem.IsOnDropDown)
			{
				ToolStripDropDown toolStripDropDown = this.ImmediateParent as ToolStripDropDown;
				if (toolStripDropDown != null)
				{
					toolStripItem2 = toolStripDropDown.OwnerItem;
					if (toolStripItem2 != null)
					{
						toolStripMenuItemDesigner = (ToolStripMenuItemDesigner)designerHost.GetDesigner(toolStripItem2);
					}
				}
			}
			try
			{
				ToolStripDesigner._autoAddNewItems = false;
				ComponentSerializationService componentSerializationService = this.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
				if (componentSerializationService != null)
				{
					SerializationStore serializationStore = componentSerializationService.CreateStore();
					componentSerializationService.Serialize(serializationStore, base.Component);
					SerializationStore serializationStore2 = null;
					ToolStripDropDownItem toolStripDropDownItem = this.ToolStripItem as ToolStripDropDownItem;
					if (toolStripDropDownItem != null && typeof(ToolStripDropDownItem).IsAssignableFrom(t))
					{
						toolStripDropDownItem.HideDropDown();
						serializationStore2 = componentSerializationService.CreateStore();
						this.SerializeDropDownItems(toolStripDropDownItem, ref serializationStore2, componentSerializationService);
						serializationStore2.Close();
					}
					serializationStore.Close();
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						if (toolStrip.Site != null)
						{
							componentChangeService.OnComponentChanging(toolStrip, TypeDescriptor.GetProperties(toolStrip)["Items"]);
						}
						else if (toolStripItem2 != null)
						{
							componentChangeService.OnComponentChanging(toolStripItem2, TypeDescriptor.GetProperties(toolStripItem2)["DropDownItems"]);
							componentChangeService.OnComponentChanged(toolStripItem2, TypeDescriptor.GetProperties(toolStripItem2)["DropDownItems"], null, null);
						}
					}
					this.FireComponentChanging(toolStripDropDownItem);
					toolStrip.Items.Remove(this.ToolStripItem);
					designerHost.DestroyComponent(this.ToolStripItem);
					ToolStripItem toolStripItem3 = (ToolStripItem)designerHost.CreateComponent(t, name);
					if (toolStripItem3 is ToolStripDropDownItem && serializationStore2 != null)
					{
						componentSerializationService.Deserialize(serializationStore2);
					}
					componentSerializationService.DeserializeTo(serializationStore, designerHost.Container, false, true);
					toolStripItem = (ToolStripItem)designerHost.Container.Components[name];
					if (toolStripItem.Image == null && toolStripItem is ToolStripButton)
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
					toolStrip.Items.Insert(index, toolStripItem);
					if (componentChangeService != null)
					{
						if (toolStrip.Site != null)
						{
							componentChangeService.OnComponentChanged(toolStrip, TypeDescriptor.GetProperties(toolStrip)["Items"], null, null);
						}
						else if (toolStripItem2 != null)
						{
							componentChangeService.OnComponentChanging(toolStripItem2, TypeDescriptor.GetProperties(toolStripItem2)["DropDownItems"]);
							componentChangeService.OnComponentChanged(toolStripItem2, TypeDescriptor.GetProperties(toolStripItem2)["DropDownItems"], null, null);
						}
					}
					this.FireComponentChanged(toolStripDropDownItem);
					if (toolStripItem.IsOnDropDown && toolStripMenuItemDesigner != null)
					{
						toolStripMenuItemDesigner.RemoveItemBodyGlyph(toolStripItem);
						toolStripMenuItemDesigner.AddItemBodyGlyph(toolStripItem);
					}
					ToolStripDesigner._autoAddNewItems = true;
					if (toolStripItem != null)
					{
						if (toolStripItem is ToolStripSeparator)
						{
							toolStrip.PerformLayout();
						}
						BehaviorService behaviorService = (BehaviorService)toolStripItem.Site.GetService(typeof(BehaviorService));
						if (behaviorService != null)
						{
							behaviorService.Invalidate();
						}
						ISelectionService selectionService = (ISelectionService)toolStripItem.Site.GetService(typeof(ISelectionService));
						if (selectionService != null)
						{
							selectionService.SetSelectedComponents(new object[]
							{
								toolStripItem
							}, SelectionTypes.Replace);
						}
					}
				}
			}
			catch
			{
				designerHost.Container.Add(this.ToolStripItem);
				toolStrip.Items.Insert(index, this.ToolStripItem);
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
			return toolStripItem;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000D6210 File Offset: 0x000D4410
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (e.Component == this.ToolStripItem)
			{
				this.ToolStripItem.AccessibleName = e.NewName;
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000D6234 File Offset: 0x000D4434
		private void OnItemPaint(object sender, PaintEventArgs e)
		{
			ToolStripDropDown toolStripDropDown = this.ToolStripItem.GetCurrentParent() as ToolStripDropDown;
			if (toolStripDropDown != null && this.selSvc != null && !this.IsEditorActive && this.ToolStripItem.Equals(this.selSvc.PrimarySelection))
			{
				BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
				if (behaviorService != null)
				{
					Point pos = behaviorService.ControlToAdornerWindow((Control)this.ImmediateParent);
					Rectangle bounds = this.ToolStripItem.Bounds;
					bounds.Offset(pos);
					bounds.Inflate(2, 2);
					behaviorService.ProcessPaintMessage(bounds);
				}
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000D62CC File Offset: 0x000D44CC
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			ISelectionService selectionService = sender as ISelectionService;
			if (selectionService == null)
			{
				return;
			}
			ToolStripItem toolStripItem = selectionService.PrimarySelection as ToolStripItem;
			ToolStripItem.ToolStripItemAccessibleObject toolStripItemAccessibleObject = this.ToolStripItem.AccessibilityObject as ToolStripItem.ToolStripItemAccessibleObject;
			if (toolStripItemAccessibleObject != null)
			{
				toolStripItemAccessibleObject.AddState(AccessibleStates.None);
				ToolStrip mainToolStrip = this.GetMainToolStrip();
				if (selectionService.GetComponentSelected(this.ToolStripItem))
				{
					ToolStrip toolStrip = this.ImmediateParent as ToolStrip;
					int num = 0;
					if (toolStrip != null)
					{
						num = toolStrip.Items.IndexOf(toolStripItem);
					}
					toolStripItemAccessibleObject.AddState(AccessibleStates.Selected);
					if (mainToolStrip != null)
					{
						UnsafeNativeMethods.NotifyWinEvent(32775, new HandleRef(toolStrip, toolStrip.Handle), -4, num + 1);
					}
					if (toolStripItem == this.ToolStripItem)
					{
						toolStripItemAccessibleObject.AddState(AccessibleStates.Focused);
						if (mainToolStrip != null)
						{
							UnsafeNativeMethods.NotifyWinEvent(32773, new HandleRef(toolStrip, toolStrip.Handle), -4, num + 1);
						}
					}
				}
			}
			if (toolStripItem != null && toolStripItem.Equals(this.ToolStripItem) && !(this.ToolStripItem is ToolStripMenuItem))
			{
				if (toolStripItem.IsOnDropDown)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						ToolStripDropDown toolStripDropDown = toolStripItem.Owner as ToolStripDropDown;
						if (toolStripDropDown != null)
						{
							ToolStripDropDownItem toolStripDropDownItem = toolStripDropDown.OwnerItem as ToolStripDropDownItem;
							bool flag = false;
							if (toolStripDropDownItem != null)
							{
								ToolStripMenuItemDesigner toolStripMenuItemDesigner = (ToolStripMenuItemDesigner)designerHost.GetDesigner(toolStripDropDownItem);
								if (toolStripMenuItemDesigner != null)
								{
									toolStripMenuItemDesigner.InitializeDropDown();
								}
								flag = true;
							}
							else if (toolStripDropDown is ContextMenuStrip)
							{
								ToolStripDropDownDesigner toolStripDropDownDesigner = (ToolStripDropDownDesigner)designerHost.GetDesigner(toolStripDropDown);
								if (toolStripDropDownDesigner != null)
								{
									toolStripDropDownDesigner.ShowMenu(toolStripItem);
								}
								flag = true;
							}
							if (flag)
							{
								SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
								if (selectionManager != null)
								{
									selectionManager.Refresh();
								}
								BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
								if (behaviorService != null)
								{
									behaviorService.Invalidate(toolStripDropDown.Bounds);
									return;
								}
							}
						}
					}
				}
				else if (toolStripItem.Owner != null)
				{
					BehaviorService behaviorService2 = (BehaviorService)this.GetService(typeof(BehaviorService));
					if (behaviorService2 != null)
					{
						behaviorService2.Invalidate(behaviorService2.ControlRectInAdornerWindow(toolStripItem.Owner));
					}
				}
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000D64EC File Offset: 0x000D46EC
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"AutoSize",
				"AccessibleName",
				"Visible",
				"Overflow"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(ToolStripItemDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000D6568 File Offset: 0x000D4768
		public void RemoveItem()
		{
			this.dummyItemAdded = false;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return;
			}
			ToolStrip toolStrip = (ToolStrip)this.ImmediateParent;
			if (toolStrip is ToolStripOverflow)
			{
				toolStrip = (this.ParentComponent as ToolStrip);
			}
			toolStrip.Items.Remove(this.ToolStripItem);
			designerHost.DestroyComponent(this.ToolStripItem);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000D65D3 File Offset: 0x000D47D3
		private void ResetAutoSize()
		{
			base.ShadowProperties["AutoSize"] = false;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000D65EB File Offset: 0x000D47EB
		private void RestoreAutoSize()
		{
			this.ToolStripItem.AutoSize = (bool)base.ShadowProperties["AutoSize"];
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000D660D File Offset: 0x000D480D
		private void ResetVisible()
		{
			this.Visible = true;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000D6616 File Offset: 0x000D4816
		private void RestoreOverflow()
		{
			this.ToolStripItem.Overflow = (ToolStripItemOverflow)base.ShadowProperties["Overflow"];
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000D6638 File Offset: 0x000D4838
		private void ResetOverflow()
		{
			this.ToolStripItem.Overflow = ToolStripItemOverflow.AsNeeded;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000D6646 File Offset: 0x000D4846
		private void ResetAccessibleName()
		{
			base.ShadowProperties["AccessibleName"] = null;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000D6659 File Offset: 0x000D4859
		private void RestoreAccessibleName()
		{
			this.ToolStripItem.AccessibleName = (string)base.ShadowProperties["AccessibleName"];
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000D667C File Offset: 0x000D487C
		internal void SelectNextItem(ISelectionService service, bool enterKeyPressed, ToolStripDesigner designer)
		{
			ToolStripDropDownItem toolStripDropDownItem = this.ToolStripItem as ToolStripDropDownItem;
			if (toolStripDropDownItem != null)
			{
				this.SetSelection(enterKeyPressed);
				return;
			}
			ToolStrip toolStrip = (ToolStrip)this.ImmediateParent;
			if (toolStrip is ToolStripOverflow)
			{
				toolStrip = this.ToolStripItem.Owner;
			}
			int num = toolStrip.Items.IndexOf(this.ToolStripItem);
			ToolStripItem toolStripItem = toolStrip.Items[num + 1];
			ToolStripKeyboardHandlingService toolStripKeyboardHandlingService = (ToolStripKeyboardHandlingService)this.GetService(typeof(ToolStripKeyboardHandlingService));
			if (toolStripKeyboardHandlingService != null)
			{
				if (toolStripItem == designer.EditorNode)
				{
					toolStripKeyboardHandlingService.SelectedDesignerControl = toolStripItem;
					this.selSvc.SetSelectedComponents(null, SelectionTypes.Replace);
					return;
				}
				toolStripKeyboardHandlingService.SelectedDesignerControl = null;
				this.selSvc.SetSelectedComponents(new object[]
				{
					toolStripItem
				});
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000D673C File Offset: 0x000D493C
		private void SerializeDropDownItems(ToolStripDropDownItem parent, ref SerializationStore _serializedDataForDropDownItems, ComponentSerializationService _serializationService)
		{
			foreach (object obj in parent.DropDownItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (!(toolStripItem is DesignerToolStripControlHost))
				{
					_serializationService.Serialize(_serializedDataForDropDownItems, toolStripItem);
					ToolStripDropDownItem toolStripDropDownItem = toolStripItem as ToolStripDropDownItem;
					if (toolStripDropDownItem != null)
					{
						this.SerializeDropDownItems(toolStripDropDownItem, ref _serializedDataForDropDownItems, _serializationService);
					}
				}
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000D67B4 File Offset: 0x000D49B4
		internal void SetItemVisible(bool toolStripSelected, ToolStripDesigner designer)
		{
			if (toolStripSelected)
			{
				if (!this.currentVisible)
				{
					this.ToolStripItem.Visible = true;
					if (designer != null && !designer.FireSyncSelection)
					{
						designer.FireSyncSelection = true;
						return;
					}
				}
			}
			else if (!this.currentVisible)
			{
				this.ToolStripItem.Visible = this.currentVisible;
			}
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000D6804 File Offset: 0x000D4A04
		private bool ShouldSerializeVisible()
		{
			return !this.Visible;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000D680F File Offset: 0x000D4A0F
		private bool ShouldSerializeAutoSize()
		{
			return base.ShadowProperties.Contains("AutoSize");
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000D6821 File Offset: 0x000D4A21
		private bool ShouldSerializeAccessibleName()
		{
			return base.ShadowProperties["AccessibleName"] != null;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000D6836 File Offset: 0x000D4A36
		private bool ShouldSerializeOverflow()
		{
			return base.ShadowProperties["Overflow"] != null;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000D684C File Offset: 0x000D4A4C
		internal virtual void ShowEditNode(bool clicked)
		{
			if (this.ToolStripItem is ToolStripMenuItem)
			{
				if (this._editorNode == null)
				{
					this.CreateDummyNode();
				}
				IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
				ToolStrip toolStrip = this.ImmediateParent as ToolStrip;
				if (toolStrip != null)
				{
					ToolStripDesigner toolStripDesigner = (ToolStripDesigner)designerHost.GetDesigner(toolStrip);
					BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
					Point pos = behaviorService.ControlToAdornerWindow(toolStrip);
					Rectangle bounds = this.ToolStripItem.Bounds;
					bounds.Offset(pos);
					this.ToolStripItem.AutoSize = false;
					this._editorNode.SetWidth(this.ToolStripItem.Text);
					if (toolStrip.Orientation == Orientation.Horizontal)
					{
						this.ToolStripItem.Width = this._editorNode.EditorToolStrip.Width + 2;
					}
					else
					{
						this.ToolStripItem.Height = this._editorNode.EditorToolStrip.Height;
					}
					if (!this.dummyItemAdded)
					{
						behaviorService.SyncSelection();
					}
					if (this.ToolStripItem.Placement != ToolStripItemPlacement.None)
					{
						Rectangle rectangle = this.ToolStripItem.Bounds;
						rectangle.Offset(pos);
						if (toolStrip.Orientation == Orientation.Horizontal)
						{
							int num = rectangle.X;
							rectangle.X = num + 1;
							rectangle.Y += (this.ToolStripItem.Height - this._editorNode.EditorToolStrip.Height) / 2;
							num = rectangle.Y;
							rectangle.Y = num + 1;
						}
						else
						{
							rectangle.X += (this.ToolStripItem.Width - this._editorNode.EditorToolStrip.Width) / 2;
							int num = rectangle.X;
							rectangle.X = num + 1;
						}
						this._editorNode.Bounds = rectangle;
						rectangle = Rectangle.Union(bounds, rectangle);
						behaviorService.Invalidate(rectangle);
						if (toolStripDesigner != null && toolStripDesigner.EditManager != null)
						{
							toolStripDesigner.EditManager.ActivateEditor(this.ToolStripItem, clicked);
						}
						SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
						if (this.bodyGlyph != null)
						{
							selectionManager.BodyGlyphAdorner.Glyphs.Remove(this.bodyGlyph);
							return;
						}
					}
					else
					{
						this.ToolStripItem.AutoSize = this.AutoSize;
						if (this.ToolStripItem is ToolStripDropDownItem)
						{
							ToolStripDropDownItem toolStripDropDownItem = this.ToolStripItem as ToolStripDropDownItem;
							if (toolStripDropDownItem != null)
							{
								toolStripDropDownItem.HideDropDown();
							}
							this.selSvc.SetSelectedComponents(new object[]
							{
								this.ImmediateParent
							});
						}
					}
				}
			}
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0000445B File Offset: 0x0000265B
		internal virtual bool SetSelection(bool enterKeyPressed)
		{
			return false;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000D6AE4 File Offset: 0x000D4CE4
		internal override void ShowContextMenu(int x, int y)
		{
			ToolStripKeyboardHandlingService toolStripKeyboardHandlingService = (ToolStripKeyboardHandlingService)this.GetService(typeof(ToolStripKeyboardHandlingService));
			if (toolStripKeyboardHandlingService != null)
			{
				if (!toolStripKeyboardHandlingService.ContextMenuShownByKeyBoard)
				{
					BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
					Point pt = Point.Empty;
					if (behaviorService != null)
					{
						pt = behaviorService.ScreenToAdornerWindow(new Point(x, y));
					}
					if (this.GetGlyphBounds().Contains(pt))
					{
						this.DesignerContextMenu.Show(x, y);
						return;
					}
				}
				else
				{
					toolStripKeyboardHandlingService.ContextMenuShownByKeyBoard = false;
					this.DesignerContextMenu.Show(x, y);
				}
			}
		}

		// Token: 0x040019BF RID: 6591
		private const int GLYPHBORDER = 1;

		// Token: 0x040019C0 RID: 6592
		private const int GLYPHINSET = 2;

		// Token: 0x040019C1 RID: 6593
		private ToolStripTemplateNode _editorNode;

		// Token: 0x040019C2 RID: 6594
		private bool isEditorActive;

		// Token: 0x040019C3 RID: 6595
		private bool internalCreate;

		// Token: 0x040019C4 RID: 6596
		private ISelectionService selSvc;

		// Token: 0x040019C5 RID: 6597
		private bool currentVisible;

		// Token: 0x040019C6 RID: 6598
		private Rectangle lastInsertionMarkRect = Rectangle.Empty;

		// Token: 0x040019C7 RID: 6599
		internal ControlBodyGlyph bodyGlyph;

		// Token: 0x040019C8 RID: 6600
		internal bool dummyItemAdded;

		// Token: 0x040019C9 RID: 6601
		internal Rectangle dragBoxFromMouseDown = Rectangle.Empty;

		// Token: 0x040019CA RID: 6602
		internal int indexOfItemUnderMouseToDrag = -1;

		// Token: 0x040019CB RID: 6603
		private ToolStripItemCustomMenuItemCollection toolStripItemCustomMenuItemCollection;
	}
}
