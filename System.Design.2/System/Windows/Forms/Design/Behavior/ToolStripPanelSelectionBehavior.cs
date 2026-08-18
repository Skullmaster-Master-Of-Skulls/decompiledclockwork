using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000395 RID: 917
	internal sealed class ToolStripPanelSelectionBehavior : Behavior
	{
		// Token: 0x0600255E RID: 9566 RVA: 0x000EA2B6 File Offset: 0x000E84B6
		internal ToolStripPanelSelectionBehavior(ToolStripPanel containerControl, IServiceProvider serviceProvider)
		{
			this.behaviorService = (BehaviorService)serviceProvider.GetService(typeof(BehaviorService));
			if (this.behaviorService == null)
			{
				return;
			}
			this.relatedControl = containerControl;
			this.serviceProvider = serviceProvider;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000EA2F0 File Offset: 0x000E84F0
		private static bool DragComponentContainsToolStrip(DropSourceBehavior.BehaviorDataObject data)
		{
			bool result = false;
			if (data != null)
			{
				ArrayList arrayList = new ArrayList(data.DragComponents);
				foreach (object obj in arrayList)
				{
					Component component = (Component)obj;
					ToolStrip toolStrip = component as ToolStrip;
					if (toolStrip != null)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000EA364 File Offset: 0x000E8564
		private void ExpandPanel(bool setSelection)
		{
			switch (this.relatedControl.Dock)
			{
			case DockStyle.Top:
				this.relatedControl.Padding = new Padding(0, 0, 0, 25);
				break;
			case DockStyle.Bottom:
				this.relatedControl.Padding = new Padding(0, 25, 0, 0);
				break;
			case DockStyle.Left:
				this.relatedControl.Padding = new Padding(0, 0, 25, 0);
				break;
			case DockStyle.Right:
				this.relatedControl.Padding = new Padding(25, 0, 0, 0);
				break;
			}
			if (setSelection)
			{
				ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						this.relatedControl
					}, SelectionTypes.Replace);
				}
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000EA428 File Offset: 0x000E8628
		public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
		{
			ToolStripPanelSelectionGlyph toolStripPanelSelectionGlyph = g as ToolStripPanelSelectionGlyph;
			if (button == MouseButtons.Left && toolStripPanelSelectionGlyph != null)
			{
				if (!toolStripPanelSelectionGlyph.IsExpanded)
				{
					this.ExpandPanel(true);
					Rectangle bounds = toolStripPanelSelectionGlyph.Bounds;
					toolStripPanelSelectionGlyph.IsExpanded = true;
					this.behaviorService.Invalidate(bounds);
					this.behaviorService.Invalidate(toolStripPanelSelectionGlyph.Bounds);
				}
				else
				{
					this.relatedControl.Padding = new Padding(0);
					Rectangle bounds2 = toolStripPanelSelectionGlyph.Bounds;
					toolStripPanelSelectionGlyph.IsExpanded = false;
					this.behaviorService.Invalidate(bounds2);
					this.behaviorService.Invalidate(toolStripPanelSelectionGlyph.Bounds);
					ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
					Component component = selectionService.PrimarySelection as Component;
					if (component != this.relatedControl.Parent)
					{
						if (selectionService != null)
						{
							selectionService.SetSelectedComponents(new object[]
							{
								this.relatedControl.Parent
							}, SelectionTypes.Replace);
						}
					}
					else
					{
						Control parent = this.relatedControl.Parent;
						parent.PerformLayout();
						SelectionManager selectionManager = (SelectionManager)this.serviceProvider.GetService(typeof(SelectionManager));
						selectionManager.Refresh();
						Point location = this.behaviorService.ControlToAdornerWindow(parent);
						Rectangle rect = new Rectangle(location, parent.Size);
						this.behaviorService.Invalidate(rect);
					}
				}
			}
			return false;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000EA588 File Offset: 0x000E8788
		private void ReParentControls(ArrayList controls, bool copy)
		{
			IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null && controls.Count > 0)
			{
				string @string;
				if (controls.Count == 1 && controls[0] is ToolStrip)
				{
					string text = TypeDescriptor.GetComponentName(controls[0]);
					if (text == null || text.Length == 0)
					{
						text = controls[0].GetType().Name;
					}
					@string = SR.GetString(copy ? "BehaviorServiceCopyControl" : "BehaviorServiceMoveControl", new object[]
					{
						text
					});
				}
				else
				{
					@string = SR.GetString(copy ? "BehaviorServiceCopyControls" : "BehaviorServiceMoveControls", new object[]
					{
						controls.Count
					});
				}
				DesignerTransaction designerTransaction = designerHost.CreateTransaction(@string);
				try
				{
					ArrayList arrayList = null;
					ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
					if (copy)
					{
						arrayList = new ArrayList();
						selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
					}
					for (int i = 0; i < controls.Count; i++)
					{
						Control control = controls[i] as Control;
						if (control is ToolStrip)
						{
							if (copy)
							{
								arrayList.Clear();
								arrayList.Add(control);
								arrayList = (DesignerUtils.CopyDragObjects(arrayList, this.serviceProvider) as ArrayList);
								if (arrayList != null)
								{
									control = (arrayList[0] as Control);
									control.Visible = true;
								}
							}
							Control control2 = this.relatedControl;
							IComponentChangeService componentChangeService = this.serviceProvider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
							PropertyDescriptor member = TypeDescriptor.GetProperties(control2)["Controls"];
							Control parent = control.Parent;
							if (parent != null && !copy)
							{
								if (componentChangeService != null)
								{
									componentChangeService.OnComponentChanging(parent, member);
								}
								parent.Controls.Remove(control);
							}
							if (componentChangeService != null)
							{
								componentChangeService.OnComponentChanging(control2, member);
							}
							control2.Controls.Add(control);
							if (componentChangeService != null && parent != null && !copy)
							{
								componentChangeService.OnComponentChanged(parent, member, null, null);
							}
							if (componentChangeService != null)
							{
								componentChangeService.OnComponentChanged(control2, member, null, null);
							}
							if (selectionService != null)
							{
								selectionService.SetSelectedComponents(new object[]
								{
									control
								}, (i == 0) ? (SelectionTypes.Replace | SelectionTypes.Click) : SelectionTypes.Add);
							}
						}
					}
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

		// Token: 0x06002563 RID: 9571 RVA: 0x000EA830 File Offset: 0x000E8A30
		public override void OnDragDrop(Glyph g, DragEventArgs e)
		{
			ToolStripPanelSelectionGlyph toolStripPanelSelectionGlyph = g as ToolStripPanelSelectionGlyph;
			bool flag = false;
			ArrayList arrayList = null;
			DropSourceBehavior.BehaviorDataObject behaviorDataObject = e.Data as DropSourceBehavior.BehaviorDataObject;
			if (behaviorDataObject != null)
			{
				arrayList = new ArrayList(behaviorDataObject.DragComponents);
				foreach (object obj in arrayList)
				{
					Component component = (Component)obj;
					ToolStrip toolStrip = component as ToolStrip;
					if (toolStrip != null && toolStrip.Parent != this.relatedControl)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					Control parent = this.relatedControl.Parent;
					if (parent != null)
					{
						try
						{
							parent.SuspendLayout();
							this.ExpandPanel(false);
							Rectangle bounds = toolStripPanelSelectionGlyph.Bounds;
							toolStripPanelSelectionGlyph.IsExpanded = true;
							this.behaviorService.Invalidate(bounds);
							this.behaviorService.Invalidate(toolStripPanelSelectionGlyph.Bounds);
							this.ReParentControls(arrayList, e.Effect == DragDropEffects.Copy);
						}
						finally
						{
							parent.ResumeLayout(true);
						}
					}
				}
				behaviorDataObject.CleanupDrag();
				return;
			}
			if (e.Data is DataObject && arrayList == null)
			{
				IToolboxService toolboxService = (IToolboxService)this.serviceProvider.GetService(typeof(IToolboxService));
				IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (toolboxService != null && designerHost != null)
				{
					ToolboxItem toolboxItem = toolboxService.DeserializeToolboxItem(e.Data, designerHost);
					if (toolboxItem.GetType(designerHost) == typeof(ToolStrip) || toolboxItem.GetType(designerHost) == typeof(MenuStrip) || toolboxItem.GetType(designerHost) == typeof(StatusStrip))
					{
						ToolStripPanelDesigner toolStripPanelDesigner = designerHost.GetDesigner(this.relatedControl) as ToolStripPanelDesigner;
						if (toolStripPanelDesigner != null)
						{
							OleDragDropHandler oleDragHandler = toolStripPanelDesigner.GetOleDragHandler();
							if (oleDragHandler != null)
							{
								oleDragHandler.CreateTool(toolboxItem, this.relatedControl, 0, 0, 0, 0, false, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000EAA44 File Offset: 0x000E8C44
		public override void OnDragEnter(Glyph g, DragEventArgs e)
		{
			e.Effect = (ToolStripPanelSelectionBehavior.DragComponentContainsToolStrip(e.Data as DropSourceBehavior.BehaviorDataObject) ? ((Control.ModifierKeys == Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move) : DragDropEffects.None);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000EAA44 File Offset: 0x000E8C44
		public override void OnDragOver(Glyph g, DragEventArgs e)
		{
			e.Effect = (ToolStripPanelSelectionBehavior.DragComponentContainsToolStrip(e.Data as DropSourceBehavior.BehaviorDataObject) ? ((Control.ModifierKeys == Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move) : DragDropEffects.None);
		}

		// Token: 0x04001B45 RID: 6981
		private ToolStripPanel relatedControl;

		// Token: 0x04001B46 RID: 6982
		private IServiceProvider serviceProvider;

		// Token: 0x04001B47 RID: 6983
		private BehaviorService behaviorService;

		// Token: 0x04001B48 RID: 6984
		private const int defaultBounds = 25;
	}
}
