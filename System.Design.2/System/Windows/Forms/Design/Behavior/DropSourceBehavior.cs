using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Imaging;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200037E RID: 894
	internal sealed class DropSourceBehavior : Behavior, IComparer
	{
		// Token: 0x060024D7 RID: 9431 RVA: 0x000E438C File Offset: 0x000E258C
		internal DropSourceBehavior(ICollection dragComponents, Control source, Point initialMouseLocation)
		{
			this.serviceProviderSource = source.Site;
			if (this.serviceProviderSource == null)
			{
				return;
			}
			this.behaviorServiceSource = (BehaviorService)this.serviceProviderSource.GetService(typeof(BehaviorService));
			if (this.behaviorServiceSource == null)
			{
				return;
			}
			if (dragComponents == null || dragComponents.Count <= 0)
			{
				return;
			}
			this.srcHost = (IDesignerHost)this.serviceProviderSource.GetService(typeof(IDesignerHost));
			if (this.srcHost == null)
			{
				return;
			}
			this.data = new DropSourceBehavior.BehaviorDataObject(dragComponents, source, this);
			this.allowedEffects = (DragDropEffects.Copy | DragDropEffects.Move);
			this.dragComponents = new DropSourceBehavior.DragComponent[dragComponents.Count];
			this.parentGridSize = Size.Empty;
			this.lastEffect = DragDropEffects.None;
			this.lastFeedbackLocation = new Point(-1, -1);
			this.lastSnapOffset = Point.Empty;
			this.dragImageRect = Rectangle.Empty;
			this.clearDragImageRect = Rectangle.Empty;
			this.InitiateDrag(initialMouseLocation, dragComponents);
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x000E4496 File Offset: 0x000E2696
		internal DragDropEffects AllowedEffects
		{
			get
			{
				return this.allowedEffects;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x000E449E File Offset: 0x000E269E
		internal DataObject DataObject
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000E44A8 File Offset: 0x000E26A8
		private Point AdjustToGrid(Point dragLoc)
		{
			Point point = new Point(dragLoc.X - this.parentLocation.X, dragLoc.Y - this.parentLocation.Y);
			Point empty = Point.Empty;
			int num = point.X % this.parentGridSize.Width;
			int num2 = point.Y % this.parentGridSize.Height;
			if (num > this.parentGridSize.Width / 2)
			{
				empty.X = this.parentGridSize.Width - num;
			}
			else
			{
				empty.X = -num;
			}
			if (num2 > this.parentGridSize.Height / 2)
			{
				empty.Y = this.parentGridSize.Height - num2;
			}
			else
			{
				empty.Y = -num2;
			}
			return empty;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000E456F File Offset: 0x000E276F
		private Point MapPointFromSourceToTarget(Point pt)
		{
			if (this.srcHost != this.destHost && this.destHost != null)
			{
				pt = this.behaviorServiceSource.AdornerWindowPointToScreen(pt);
				return this.behaviorServiceTarget.MapAdornerWindowPoint(IntPtr.Zero, pt);
			}
			return pt;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000E45A8 File Offset: 0x000E27A8
		private Point MapPointFromTargetToSource(Point pt)
		{
			if (this.srcHost != this.destHost && this.destHost != null)
			{
				pt = this.behaviorServiceTarget.AdornerWindowPointToScreen(pt);
				return this.behaviorServiceSource.MapAdornerWindowPoint(IntPtr.Zero, pt);
			}
			return pt;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000E45E4 File Offset: 0x000E27E4
		private void ClearAllDragImages()
		{
			if (this.dragImageRect != Rectangle.Empty)
			{
				Rectangle rectangle = this.dragImageRect;
				rectangle.Location = this.MapPointFromSourceToTarget(rectangle.Location);
				if (this.graphicsTarget != null)
				{
					this.graphicsTarget.SetClip(rectangle);
				}
				if (this.behaviorServiceTarget != null)
				{
					this.behaviorServiceTarget.Invalidate(rectangle);
				}
				if (this.graphicsTarget != null)
				{
					this.graphicsTarget.ResetClip();
				}
			}
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000E465C File Offset: 0x000E285C
		private void SetDesignerHost(Control c)
		{
			foreach (object obj in c.Controls)
			{
				Control designerHost = (Control)obj;
				this.SetDesignerHost(designerHost);
			}
			if (c.Site != null && !(c.Site is INestedSite) && this.destHost != null)
			{
				this.destHost.Container.Add(c);
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000E46E4 File Offset: 0x000E28E4
		private void DropControl(int dragComponentIndex, Control dragTarget, Control dragSource, bool localDrag)
		{
			Control control = this.dragComponents[dragComponentIndex].dragComponent as Control;
			if (this.lastEffect == DragDropEffects.Copy || (this.srcHost != this.destHost && this.destHost != null))
			{
				control.Visible = true;
				bool flag = true;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["Visible"];
				if (propertyDescriptor != null)
				{
					flag = (bool)propertyDescriptor.GetValue(control);
				}
				this.SetDesignerHost(control);
				control.Parent = dragTarget;
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(control, flag);
					return;
				}
			}
			else if (!localDrag && control.Parent.Equals(dragSource))
			{
				dragSource.Controls.Remove(control);
				control.Visible = true;
				dragTarget.Controls.Add(control);
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000E47A4 File Offset: 0x000E29A4
		private void SetLocationPropertyAndChildIndex(int dragComponentIndex, Control dragTarget, Point dropPoint, int newIndex, bool allowSetChildIndexOnDrop)
		{
			Control control = this.dragComponents[dragComponentIndex].dragComponent as Control;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.dragComponents[dragComponentIndex].dragComponent)["Location"];
			if (propertyDescriptor != null && control != null)
			{
				Point point = new Point(dropPoint.X, dropPoint.Y);
				ScrollableControl scrollableControl = control.Parent as ScrollableControl;
				if (scrollableControl != null)
				{
					Point autoScrollPosition = scrollableControl.AutoScrollPosition;
					point.Offset(-autoScrollPosition.X, -autoScrollPosition.Y);
				}
				propertyDescriptor.SetValue(control, point);
				if (allowSetChildIndexOnDrop)
				{
					dragTarget.Controls.SetChildIndex(control, newIndex);
				}
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000E4854 File Offset: 0x000E2A54
		private void EndDragDrop(bool allowSetChildIndexOnDrop)
		{
			Control control = this.data.Target as Control;
			if (control == null)
			{
				return;
			}
			if (this.serviceProviderTarget == null)
			{
				this.serviceProviderTarget = control.Site;
				if (this.serviceProviderTarget == null)
				{
					return;
				}
			}
			if (this.destHost == null)
			{
				this.destHost = (IDesignerHost)this.serviceProviderTarget.GetService(typeof(IDesignerHost));
				if (this.destHost == null)
				{
					return;
				}
			}
			if (this.behaviorServiceTarget == null)
			{
				this.behaviorServiceTarget = (BehaviorService)this.serviceProviderTarget.GetService(typeof(BehaviorService));
				if (this.behaviorServiceTarget == null)
				{
					return;
				}
			}
			ArrayList arrayList = null;
			bool flag = this.lastEffect == DragDropEffects.Copy;
			Control source = this.data.Source;
			bool flag2 = source.Equals(control);
			PropertyDescriptor member = TypeDescriptor.GetProperties(control)["Controls"];
			PropertyDescriptor member2 = TypeDescriptor.GetProperties(source)["Controls"];
			IComponentChangeService componentChangeService = (IComponentChangeService)this.serviceProviderSource.GetService(typeof(IComponentChangeService));
			IComponentChangeService componentChangeService2 = (IComponentChangeService)this.serviceProviderTarget.GetService(typeof(IComponentChangeService));
			if (this.dragAssistanceManager != null)
			{
				this.dragAssistanceManager.OnMouseUp();
			}
			ISelectionService selectionService = null;
			if (flag || (this.srcHost != this.destHost && this.destHost != null))
			{
				selectionService = (ISelectionService)this.serviceProviderTarget.GetService(typeof(ISelectionService));
			}
			try
			{
				if (this.dragComponents != null && this.dragComponents.Length != 0)
				{
					DesignerTransaction designerTransaction = null;
					DesignerTransaction designerTransaction2 = null;
					string @string;
					if (this.dragComponents.Length == 1)
					{
						string text = TypeDescriptor.GetComponentName(this.dragComponents[0].dragComponent);
						if (text == null || text.Length == 0)
						{
							text = this.dragComponents[0].dragComponent.GetType().Name;
						}
						@string = SR.GetString(flag ? "BehaviorServiceCopyControl" : "BehaviorServiceMoveControl", new object[]
						{
							text
						});
					}
					else
					{
						@string = SR.GetString(flag ? "BehaviorServiceCopyControls" : "BehaviorServiceMoveControls", new object[]
						{
							this.dragComponents.Length
						});
					}
					if (this.srcHost != null && (this.srcHost == this.destHost || this.destHost == null || !flag))
					{
						designerTransaction = this.srcHost.CreateTransaction(@string);
					}
					if (this.srcHost != this.destHost && this.destHost != null)
					{
						designerTransaction2 = this.destHost.CreateTransaction(@string);
					}
					try
					{
						ComponentTray componentTray = null;
						int num = 0;
						if (flag)
						{
							componentTray = (this.serviceProviderTarget.GetService(typeof(ComponentTray)) as ComponentTray);
							num = ((componentTray != null) ? componentTray.Controls.Count : 0);
							ArrayList arrayList2 = new ArrayList();
							for (int i = 0; i < this.dragComponents.Length; i++)
							{
								arrayList2.Add(this.dragComponents[i].dragComponent);
							}
							arrayList2 = (DesignerUtils.CopyDragObjects(arrayList2, this.serviceProviderTarget) as ArrayList);
							if (arrayList2 == null)
							{
								return;
							}
							arrayList = new ArrayList();
							for (int j = 0; j < arrayList2.Count; j++)
							{
								arrayList.Add(this.dragComponents[j].dragComponent);
								this.dragComponents[j].dragComponent = arrayList2[j];
							}
						}
						if ((!flag2 || flag) && componentChangeService != null && componentChangeService2 != null)
						{
							componentChangeService2.OnComponentChanging(control, member);
							if (!flag)
							{
								componentChangeService.OnComponentChanging(source, member2);
							}
						}
						this.DropControl(this.primaryComponentIndex, control, source, flag2);
						Point point = this.behaviorServiceSource.AdornerWindowPointToScreen(this.dragComponents[this.primaryComponentIndex].draggedLocation);
						point = ((Control)this.dragComponents[this.primaryComponentIndex].dragComponent).Parent.PointToClient(point);
						if (((Control)this.dragComponents[this.primaryComponentIndex].dragComponent).Parent.IsMirrored)
						{
							point.Offset(-((Control)this.dragComponents[this.primaryComponentIndex].dragComponent).Width, 0);
						}
						Control control2 = this.dragComponents[this.primaryComponentIndex].dragComponent as Control;
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control2)["Location"];
						if (control2 != null && propertyDescriptor != null)
						{
							try
							{
								componentChangeService2.OnComponentChanging(control2, propertyDescriptor);
							}
							catch (CheckoutException ex)
							{
								if (ex == CheckoutException.Canceled)
								{
									return;
								}
								throw;
							}
						}
						this.SetLocationPropertyAndChildIndex(this.primaryComponentIndex, control, point, this.shareParent ? this.dragComponents[this.primaryComponentIndex].zorderIndex : 0, allowSetChildIndexOnDrop);
						if (selectionService != null)
						{
							selectionService.SetSelectedComponents(new object[]
							{
								this.dragComponents[this.primaryComponentIndex].dragComponent
							}, SelectionTypes.Replace | SelectionTypes.Click);
						}
						for (int k = 0; k < this.dragComponents.Length; k++)
						{
							if (k != this.primaryComponentIndex)
							{
								this.DropControl(k, control, source, flag2);
								Point dropPoint = new Point(point.X + this.dragComponents[k].positionOffset.X, point.Y + this.dragComponents[k].positionOffset.Y);
								this.SetLocationPropertyAndChildIndex(k, control, dropPoint, this.shareParent ? this.dragComponents[k].zorderIndex : 0, allowSetChildIndexOnDrop);
								if (selectionService != null)
								{
									selectionService.SetSelectedComponents(new object[]
									{
										this.dragComponents[k].dragComponent
									}, SelectionTypes.Add);
								}
							}
						}
						if ((!flag2 || flag) && componentChangeService != null && componentChangeService2 != null)
						{
							componentChangeService2.OnComponentChanged(control, member, control.Controls, control.Controls);
							if (!flag)
							{
								componentChangeService.OnComponentChanged(source, member2, source.Controls, source.Controls);
							}
						}
						if (arrayList != null)
						{
							for (int l = 0; l < arrayList.Count; l++)
							{
								this.dragComponents[l].dragComponent = arrayList[l];
							}
							arrayList = null;
						}
						if (flag)
						{
							if (componentTray == null)
							{
								componentTray = (this.serviceProviderTarget.GetService(typeof(ComponentTray)) as ComponentTray);
							}
							if (componentTray != null)
							{
								int num2 = componentTray.Controls.Count - num;
								if (num2 > 0)
								{
									ArrayList arrayList3 = new ArrayList();
									for (int m = 0; m < num2; m++)
									{
										arrayList3.Add(componentTray.Controls[num + m]);
									}
									componentTray.UpdatePastePositions(arrayList3);
								}
							}
						}
						this.CleanupDrag(false);
						if (designerTransaction != null)
						{
							designerTransaction.Commit();
							designerTransaction = null;
						}
						if (designerTransaction2 != null)
						{
							designerTransaction2.Commit();
							designerTransaction2 = null;
						}
					}
					finally
					{
						if (designerTransaction != null)
						{
							designerTransaction.Cancel();
						}
						if (designerTransaction2 != null)
						{
							designerTransaction2.Cancel();
						}
					}
				}
			}
			finally
			{
				if (arrayList != null)
				{
					for (int n = 0; n < arrayList.Count; n++)
					{
						this.dragComponents[n].dragComponent = arrayList[n];
					}
				}
				this.CleanupDrag(false);
				if (this.statusCommandUITarget != null)
				{
					this.statusCommandUITarget.SetStatusInformation((selectionService == null) ? (this.dragComponents[this.primaryComponentIndex].dragComponent as Component) : (selectionService.PrimarySelection as Component));
				}
			}
			this.lastFeedbackLocation = new Point(-1, -1);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000E5008 File Offset: 0x000E3208
		internal void GiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			this.lastEffect = e.Effect;
			if (this.data.Target == null || e.Effect == DragDropEffects.None)
			{
				if (this.clearDragImageRect != this.dragImageRect)
				{
					this.ClearAllDragImages();
					this.clearDragImageRect = this.dragImageRect;
				}
				if (this.dragAssistanceManager != null)
				{
					this.dragAssistanceManager.EraseSnapLines();
				}
				return;
			}
			bool flag = false;
			Point mousePosition = Control.MousePosition;
			bool flag2 = Control.ModifierKeys == Keys.Alt;
			if (flag2 && this.dragAssistanceManager != null)
			{
				this.dragAssistanceManager.EraseSnapLines();
			}
			if (this.data.Target.Equals(this.data.Source) && this.lastEffect != DragDropEffects.Copy)
			{
				e.UseDefaultCursors = false;
				Cursor.Current = Cursors.Default;
			}
			else
			{
				e.UseDefaultCursors = true;
			}
			Control control = this.data.Target as Control;
			if (mousePosition != this.lastFeedbackLocation || (flag2 && this.dragAssistanceManager != null))
			{
				if (!this.data.Target.Equals(this.lastDropTarget))
				{
					this.serviceProviderTarget = control.Site;
					if (this.serviceProviderTarget == null)
					{
						return;
					}
					IDesignerHost designerHost = (IDesignerHost)this.serviceProviderTarget.GetService(typeof(IDesignerHost));
					if (designerHost == null)
					{
						return;
					}
					this.targetAllowsSnapLines = true;
					ControlDesigner controlDesigner = designerHost.GetDesigner(control) as ControlDesigner;
					if (controlDesigner != null && !controlDesigner.ParticipatesWithSnapLines)
					{
						this.targetAllowsSnapLines = false;
					}
					this.statusCommandUITarget = new StatusCommandUI(this.serviceProviderTarget);
					if (this.lastDropTarget == null || designerHost != this.destHost)
					{
						if (this.destHost != null && this.destHost != this.srcHost)
						{
							this.behaviorServiceTarget.EnableAllAdorners(true);
						}
						this.behaviorServiceTarget = (BehaviorService)this.serviceProviderTarget.GetService(typeof(BehaviorService));
						if (this.behaviorServiceTarget == null)
						{
							return;
						}
						this.GetParentSnapInfo(control, this.behaviorServiceTarget);
						if (designerHost != this.srcHost)
						{
							this.DisableAdorners(this.serviceProviderTarget, this.behaviorServiceTarget, true);
						}
						this.ClearAllDragImages();
						if (this.lastDropTarget != null)
						{
							for (int i = 0; i < this.dragObjects.Count; i++)
							{
								Control c = (Control)this.dragObjects[i];
								Rectangle rect = this.behaviorServiceSource.ControlRectInAdornerWindow(c);
								rect.Location = this.behaviorServiceSource.AdornerWindowPointToScreen(rect.Location);
								rect.Location = this.behaviorServiceTarget.MapAdornerWindowPoint(IntPtr.Zero, rect.Location);
								if (i == 0)
								{
									if (this.dragImageRegion != null)
									{
										this.dragImageRegion.Dispose();
									}
									this.dragImageRegion = new Region(rect);
								}
								else
								{
									this.dragImageRegion.Union(rect);
								}
							}
						}
						if (this.graphicsTarget != null)
						{
							this.graphicsTarget.Dispose();
						}
						this.graphicsTarget = this.behaviorServiceTarget.AdornerWindowGraphics;
						flag = true;
						this.destHost = designerHost;
					}
					this.lastDropTarget = this.data.Target;
				}
				if (this.ShowHideDragControls(this.lastEffect == DragDropEffects.Copy) && !flag)
				{
					flag = true;
				}
				if (flag && this.behaviorServiceTarget.UseSnapLines)
				{
					if (this.dragAssistanceManager != null)
					{
						this.dragAssistanceManager.EraseSnapLines();
					}
					this.dragAssistanceManager = new DragAssistanceManager(this.serviceProviderTarget, this.graphicsTarget, this.dragObjects, null, this.lastEffect == DragDropEffects.Copy);
				}
				Point point = new Point(mousePosition.X - this.initialMouseLoc.X + this.dragComponents[this.primaryComponentIndex].originalControlLocation.X, mousePosition.Y - this.initialMouseLoc.Y + this.dragComponents[this.primaryComponentIndex].originalControlLocation.Y);
				point = this.MapPointFromSourceToTarget(point);
				Rectangle dragBounds = new Rectangle(point.X, point.Y, this.dragComponents[this.primaryComponentIndex].dragImage.Width, this.dragComponents[this.primaryComponentIndex].dragImage.Height);
				if (this.dragAssistanceManager != null)
				{
					if (this.targetAllowsSnapLines && !flag2)
					{
						this.lastSnapOffset = this.dragAssistanceManager.OnMouseMove(dragBounds);
					}
					else
					{
						this.dragAssistanceManager.OnMouseMove(new Rectangle(-100, -100, 0, 0));
					}
				}
				else if (!this.parentGridSize.IsEmpty)
				{
					this.lastSnapOffset = this.AdjustToGrid(point);
				}
				point.X += this.lastSnapOffset.X;
				point.Y += this.lastSnapOffset.Y;
				this.dragComponents[this.primaryComponentIndex].draggedLocation = this.MapPointFromTargetToSource(point);
				Rectangle b = this.dragImageRect;
				point = new Point(mousePosition.X - this.initialMouseLoc.X + this.originalDragImageLocation.X, mousePosition.Y - this.initialMouseLoc.Y + this.originalDragImageLocation.Y);
				point.X += this.lastSnapOffset.X;
				point.Y += this.lastSnapOffset.Y;
				this.dragImageRect.Location = point;
				b.Location = this.MapPointFromSourceToTarget(b.Location);
				Rectangle rectangle = this.dragImageRect;
				rectangle.Location = this.MapPointFromSourceToTarget(rectangle.Location);
				Rectangle rect2 = Rectangle.Union(rectangle, b);
				Region region = new Region(rect2);
				region.Exclude(rectangle);
				using (Region region2 = this.dragImageRegion.Clone())
				{
					region2.Translate(mousePosition.X - this.initialMouseLoc.X + this.lastSnapOffset.X, mousePosition.Y - this.initialMouseLoc.Y + this.lastSnapOffset.Y);
					region2.Complement(rectangle);
					region2.Union(region);
					this.behaviorServiceTarget.Invalidate(region2);
				}
				region.Dispose();
				if (this.graphicsTarget != null)
				{
					this.graphicsTarget.SetClip(rectangle);
					this.graphicsTarget.DrawImage(this.dragImage, rectangle.X, rectangle.Y);
					this.graphicsTarget.ResetClip();
				}
				Control control2 = this.dragComponents[this.primaryComponentIndex].dragComponent as Control;
				if (control2 != null)
				{
					Point point2 = this.behaviorServiceSource.AdornerWindowPointToScreen(this.dragComponents[this.primaryComponentIndex].draggedLocation);
					point2 = control.PointToClient(point2);
					if (control.IsMirrored && control2.IsMirrored)
					{
						point2.Offset(-control2.Width, 0);
					}
					if (this.statusCommandUITarget != null)
					{
						this.statusCommandUITarget.SetStatusInformation(control2, point2);
					}
				}
				if (this.dragAssistanceManager != null && !flag2 && this.targetAllowsSnapLines)
				{
					this.dragAssistanceManager.RenderSnapLinesInternal();
				}
				this.lastFeedbackLocation = mousePosition;
			}
			this.data.Target = null;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000E5748 File Offset: 0x000E3948
		int IComparer.Compare(object x, object y)
		{
			DropSourceBehavior.DragComponent dragComponent = (DropSourceBehavior.DragComponent)x;
			DropSourceBehavior.DragComponent dragComponent2 = (DropSourceBehavior.DragComponent)y;
			if (dragComponent.zorderIndex > dragComponent2.zorderIndex)
			{
				return -1;
			}
			if (dragComponent.zorderIndex < dragComponent2.zorderIndex)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000E5784 File Offset: 0x000E3984
		private void GetParentSnapInfo(Control parentControl, BehaviorService bhvSvc)
		{
			this.parentGridSize = Size.Empty;
			if (bhvSvc != null && !bhvSvc.UseSnapLines)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(parentControl)["SnapToGrid"];
				if (propertyDescriptor != null && (bool)propertyDescriptor.GetValue(parentControl))
				{
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(parentControl)["GridSize"];
					if (propertyDescriptor2 != null)
					{
						Control control = this.dragComponents[this.primaryComponentIndex].dragComponent as Control;
						if (control != null)
						{
							this.parentGridSize = (Size)propertyDescriptor2.GetValue(parentControl);
							this.parentLocation = bhvSvc.MapAdornerWindowPoint(parentControl.Handle, Point.Empty);
							if (parentControl.Parent != null && parentControl.Parent.IsMirrored)
							{
								this.parentLocation.Offset(-parentControl.Width, 0);
							}
						}
					}
				}
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000E585C File Offset: 0x000E3A5C
		private void DisableAdorners(IServiceProvider serviceProvider, BehaviorService behaviorService, bool hostChange)
		{
			Adorner adorner = null;
			SelectionManager selectionManager = (SelectionManager)serviceProvider.GetService(typeof(SelectionManager));
			if (selectionManager != null)
			{
				adorner = selectionManager.BodyGlyphAdorner;
			}
			foreach (Adorner adorner2 in behaviorService.Adorners)
			{
				if (adorner == null || !adorner2.Equals(adorner))
				{
					adorner2.EnabledInternal = false;
				}
			}
			behaviorService.Invalidate();
			if (hostChange)
			{
				selectionManager.OnBeginDrag(new BehaviorDragDropEventArgs(this.dragObjects));
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000E58FC File Offset: 0x000E3AFC
		private void InitiateDrag(Point initialMouseLocation, ICollection dragComps)
		{
			this.dragObjects = new ArrayList(dragComps);
			this.DisableAdorners(this.serviceProviderSource, this.behaviorServiceSource, false);
			Control control = this.dragObjects[0] as Control;
			Control control2 = (control != null) ? control.Parent : null;
			Color backColor = (control2 != null) ? control2.BackColor : Color.Empty;
			this.dragImageRect = Rectangle.Empty;
			this.clearDragImageRect = Rectangle.Empty;
			this.initialMouseLoc = initialMouseLocation;
			for (int i = 0; i < this.dragObjects.Count; i++)
			{
				Control control3 = (Control)this.dragObjects[i];
				this.dragComponents[i].dragComponent = this.dragObjects[i];
				this.dragComponents[i].positionOffset = new Point(control3.Location.X - control.Location.X, control3.Location.Y - control.Location.Y);
				Rectangle rectangle = this.behaviorServiceSource.ControlRectInAdornerWindow(control3);
				if (this.dragImageRect.IsEmpty)
				{
					this.dragImageRect = rectangle;
					this.dragImageRegion = new Region(rectangle);
				}
				else
				{
					this.dragImageRect = Rectangle.Union(this.dragImageRect, rectangle);
					this.dragImageRegion.Union(rectangle);
				}
				this.dragComponents[i].draggedLocation = rectangle.Location;
				this.dragComponents[i].originalControlLocation = this.dragComponents[i].draggedLocation;
				DesignerUtils.GenerateSnapShot(control3, ref this.dragComponents[i].dragImage, (i == 0) ? 2 : 1, 1.0, backColor);
				if (control2 != null && this.shareParent)
				{
					this.dragComponents[i].zorderIndex = control2.Controls.GetChildIndex(control3, false);
					if (this.dragComponents[i].zorderIndex == -1)
					{
						this.shareParent = false;
					}
				}
			}
			if (this.shareParent)
			{
				Array.Sort(this.dragComponents, this);
			}
			for (int j = 0; j < this.dragComponents.Length; j++)
			{
				if (control.Equals(this.dragComponents[j].dragComponent as Control))
				{
					this.primaryComponentIndex = j;
					break;
				}
			}
			if (control2 != null)
			{
				this.suspendedParent = control2;
				this.suspendedParent.SuspendLayout();
				this.GetParentSnapInfo(this.suspendedParent, this.behaviorServiceSource);
			}
			int num = this.dragImageRect.Width;
			if (num == 0)
			{
				num = 1;
			}
			int num2 = this.dragImageRect.Height;
			if (num2 == 0)
			{
				num2 = 1;
			}
			this.dragImage = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
			using (Graphics graphics = Graphics.FromImage(this.dragImage))
			{
				graphics.Clear(Color.Chartreuse);
			}
			((Bitmap)this.dragImage).MakeTransparent(Color.Chartreuse);
			using (Graphics graphics2 = Graphics.FromImage(this.dragImage))
			{
				using (SolidBrush solidBrush = new SolidBrush(control.BackColor))
				{
					for (int k = 0; k < this.dragComponents.Length; k++)
					{
						Rectangle rectangle2 = new Rectangle(this.dragComponents[k].draggedLocation.X - this.dragImageRect.X, this.dragComponents[k].draggedLocation.Y - this.dragImageRect.Y, this.dragComponents[k].dragImage.Width, this.dragComponents[k].dragImage.Height);
						graphics2.FillRectangle(solidBrush, rectangle2);
						graphics2.DrawImage(this.dragComponents[k].dragImage, rectangle2, new Rectangle(0, 0, this.dragComponents[k].dragImage.Width, this.dragComponents[k].dragImage.Height), GraphicsUnit.Pixel);
					}
				}
			}
			this.originalDragImageLocation = new Point(this.dragImageRect.X, this.dragImageRect.Y);
			this.ShowHideDragControls(false);
			this.cleanedUpDrag = false;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000E5DB4 File Offset: 0x000E3FB4
		internal ArrayList GetSortedDragControls(ref int primaryControlIndex)
		{
			ArrayList arrayList = new ArrayList();
			primaryControlIndex = -1;
			if (this.dragComponents != null && this.dragComponents.Length != 0)
			{
				primaryControlIndex = this.primaryComponentIndex;
				for (int i = 0; i < this.dragComponents.Length; i++)
				{
					arrayList.Add(this.dragComponents[i].dragComponent);
				}
			}
			return arrayList;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000E5E10 File Offset: 0x000E4010
		internal void QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			if (this.behaviorServiceSource != null && this.behaviorServiceSource.CancelDrag)
			{
				e.Action = DragAction.Cancel;
				this.CleanupDrag(true);
				return;
			}
			if (e.Action == DragAction.Continue)
			{
				return;
			}
			if (e.Action == DragAction.Cancel || this.lastEffect == DragDropEffects.None)
			{
				this.CleanupDrag(true);
				e.Action = DragAction.Cancel;
			}
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000E5E6C File Offset: 0x000E406C
		internal bool ShowHideDragControls(bool show)
		{
			if (this.currentShowState == show)
			{
				return false;
			}
			this.currentShowState = show;
			if (this.dragComponents != null)
			{
				for (int i = 0; i < this.dragComponents.Length; i++)
				{
					Control control = this.dragComponents[i].dragComponent as Control;
					if (control != null)
					{
						control.Visible = show;
					}
				}
			}
			return true;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000E5EC8 File Offset: 0x000E40C8
		internal void CleanupDrag()
		{
			this.CleanupDrag(true);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000E5ED4 File Offset: 0x000E40D4
		internal void CleanupDrag(bool clearImages)
		{
			if (!this.cleanedUpDrag)
			{
				if (clearImages)
				{
					this.ClearAllDragImages();
				}
				this.ShowHideDragControls(true);
				try
				{
					if (this.suspendedParent != null)
					{
						this.suspendedParent.ResumeLayout();
					}
				}
				finally
				{
					this.suspendedParent = null;
					this.behaviorServiceSource.EnableAllAdorners(true);
					if (this.destHost != this.srcHost && this.destHost != null)
					{
						this.behaviorServiceTarget.EnableAllAdorners(true);
						this.behaviorServiceTarget.SyncSelection();
					}
					if (this.behaviorServiceSource != null)
					{
						this.behaviorServiceSource.SyncSelection();
					}
					if (this.dragImageRegion != null)
					{
						this.dragImageRegion.Dispose();
						this.dragImageRegion = null;
					}
					if (this.dragImage != null)
					{
						this.dragImage.Dispose();
						this.dragImage = null;
					}
					if (this.dragComponents != null)
					{
						for (int i = 0; i < this.dragComponents.Length; i++)
						{
							if (this.dragComponents[i].dragImage != null)
							{
								this.dragComponents[i].dragImage.Dispose();
								this.dragComponents[i].dragImage = null;
							}
						}
					}
					if (this.graphicsTarget != null)
					{
						this.graphicsTarget.Dispose();
						this.graphicsTarget = null;
					}
					this.cleanedUpDrag = true;
				}
			}
		}

		// Token: 0x04001AAA RID: 6826
		private DropSourceBehavior.DragComponent[] dragComponents;

		// Token: 0x04001AAB RID: 6827
		private ArrayList dragObjects;

		// Token: 0x04001AAC RID: 6828
		private DropSourceBehavior.BehaviorDataObject data;

		// Token: 0x04001AAD RID: 6829
		private DragDropEffects allowedEffects;

		// Token: 0x04001AAE RID: 6830
		private DragDropEffects lastEffect;

		// Token: 0x04001AAF RID: 6831
		private bool targetAllowsSnapLines;

		// Token: 0x04001AB0 RID: 6832
		private IComponent lastDropTarget;

		// Token: 0x04001AB1 RID: 6833
		private Point lastSnapOffset;

		// Token: 0x04001AB2 RID: 6834
		private BehaviorService behaviorServiceSource;

		// Token: 0x04001AB3 RID: 6835
		private BehaviorService behaviorServiceTarget;

		// Token: 0x04001AB4 RID: 6836
		private DragAssistanceManager dragAssistanceManager;

		// Token: 0x04001AB5 RID: 6837
		private Graphics graphicsTarget;

		// Token: 0x04001AB6 RID: 6838
		private IServiceProvider serviceProviderSource;

		// Token: 0x04001AB7 RID: 6839
		private IServiceProvider serviceProviderTarget;

		// Token: 0x04001AB8 RID: 6840
		private Point initialMouseLoc;

		// Token: 0x04001AB9 RID: 6841
		private Image dragImage;

		// Token: 0x04001ABA RID: 6842
		private Rectangle dragImageRect;

		// Token: 0x04001ABB RID: 6843
		private Rectangle clearDragImageRect;

		// Token: 0x04001ABC RID: 6844
		private Point originalDragImageLocation;

		// Token: 0x04001ABD RID: 6845
		private Region dragImageRegion;

		// Token: 0x04001ABE RID: 6846
		private Point lastFeedbackLocation;

		// Token: 0x04001ABF RID: 6847
		private Control suspendedParent;

		// Token: 0x04001AC0 RID: 6848
		private Size parentGridSize;

		// Token: 0x04001AC1 RID: 6849
		private Point parentLocation;

		// Token: 0x04001AC2 RID: 6850
		private bool shareParent = true;

		// Token: 0x04001AC3 RID: 6851
		private bool cleanedUpDrag;

		// Token: 0x04001AC4 RID: 6852
		private StatusCommandUI statusCommandUITarget;

		// Token: 0x04001AC5 RID: 6853
		private IDesignerHost srcHost;

		// Token: 0x04001AC6 RID: 6854
		private IDesignerHost destHost;

		// Token: 0x04001AC7 RID: 6855
		private bool currentShowState = true;

		// Token: 0x04001AC8 RID: 6856
		private int primaryComponentIndex = -1;

		// Token: 0x020005A7 RID: 1447
		private struct DragComponent
		{
			// Token: 0x0400229D RID: 8861
			public object dragComponent;

			// Token: 0x0400229E RID: 8862
			public int zorderIndex;

			// Token: 0x0400229F RID: 8863
			public Point originalControlLocation;

			// Token: 0x040022A0 RID: 8864
			public Point draggedLocation;

			// Token: 0x040022A1 RID: 8865
			public Image dragImage;

			// Token: 0x040022A2 RID: 8866
			public Point positionOffset;
		}

		// Token: 0x020005A8 RID: 1448
		internal class BehaviorDataObject : DataObject
		{
			// Token: 0x060033BB RID: 13243 RVA: 0x0011B20F File Offset: 0x0011940F
			public BehaviorDataObject(ICollection dragComponents, Control source, DropSourceBehavior sourceBehavior)
			{
				this.dragComponents = dragComponents;
				this.source = source;
				this.sourceBehavior = sourceBehavior;
				this.target = null;
			}

			// Token: 0x17000A16 RID: 2582
			// (get) Token: 0x060033BC RID: 13244 RVA: 0x0011B233 File Offset: 0x00119433
			public Control Source
			{
				get
				{
					return this.source;
				}
			}

			// Token: 0x17000A17 RID: 2583
			// (get) Token: 0x060033BD RID: 13245 RVA: 0x0011B23B File Offset: 0x0011943B
			public ICollection DragComponents
			{
				get
				{
					return this.dragComponents;
				}
			}

			// Token: 0x17000A18 RID: 2584
			// (get) Token: 0x060033BE RID: 13246 RVA: 0x0011B243 File Offset: 0x00119443
			// (set) Token: 0x060033BF RID: 13247 RVA: 0x0011B24B File Offset: 0x0011944B
			public IComponent Target
			{
				get
				{
					return this.target;
				}
				set
				{
					this.target = value;
				}
			}

			// Token: 0x060033C0 RID: 13248 RVA: 0x0011B254 File Offset: 0x00119454
			internal void EndDragDrop(bool allowSetChildIndexOnDrop)
			{
				this.sourceBehavior.EndDragDrop(allowSetChildIndexOnDrop);
			}

			// Token: 0x060033C1 RID: 13249 RVA: 0x0011B262 File Offset: 0x00119462
			internal void CleanupDrag()
			{
				this.sourceBehavior.CleanupDrag();
			}

			// Token: 0x060033C2 RID: 13250 RVA: 0x0011B26F File Offset: 0x0011946F
			internal ArrayList GetSortedDragControls(ref int primaryControlIndex)
			{
				return this.sourceBehavior.GetSortedDragControls(ref primaryControlIndex);
			}

			// Token: 0x040022A3 RID: 8867
			private ICollection dragComponents;

			// Token: 0x040022A4 RID: 8868
			private Control source;

			// Token: 0x040022A5 RID: 8869
			private IComponent target;

			// Token: 0x040022A6 RID: 8870
			private DropSourceBehavior sourceBehavior;
		}
	}
}
