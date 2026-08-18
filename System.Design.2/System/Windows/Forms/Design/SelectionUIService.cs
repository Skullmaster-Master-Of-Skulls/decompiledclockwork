using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using Microsoft.Win32;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200032E RID: 814
	internal sealed class SelectionUIService : Control, ISelectionUIService
	{
		// Token: 0x06001FFD RID: 8189 RVA: 0x000C1C88 File Offset: 0x000BFE88
		public SelectionUIService(IDesignerHost host)
		{
			base.SetStyle(ControlStyles.Opaque | ControlStyles.StandardClick | ControlStyles.OptimizedDoubleBuffer, true);
			this.host = host;
			this.dragHandler = null;
			this.dragComponents = null;
			this.selectionItems = new Hashtable();
			this.selectionHandlers = new Hashtable();
			this.AllowDrop = true;
			this.Text = "SelectionUIOverlay";
			this.selSvc = (ISelectionService)host.GetService(typeof(ISelectionService));
			if (this.selSvc != null)
			{
				this.selSvc.SelectionChanged += this.OnSelectionChanged;
			}
			host.TransactionOpened += this.OnTransactionOpened;
			host.TransactionClosed += this.OnTransactionClosed;
			if (host.InTransaction)
			{
				this.OnTransactionOpened(host, EventArgs.Empty);
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)host.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRemoved += this.OnComponentRemove;
				componentChangeService.ComponentChanged += this.OnComponentChanged;
			}
			SystemEvents.DisplaySettingsChanged += this.OnSystemSettingChanged;
			SystemEvents.InstalledFontsChanged += this.OnSystemSettingChanged;
			SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x000C1DE8 File Offset: 0x000BFFE8
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style &= -100663297;
				return createParams;
			}
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000C1E0F File Offset: 0x000C000F
		private void BeginMouseDrag(Point anchor, int hitTest)
		{
			base.Capture = true;
			this.ignoreCaptureChanged = false;
			this.mouseDragAnchor = anchor;
			this.mouseDragging = true;
			this.mouseDragHitTest = hitTest;
			this.mouseDragOffset = default(Rectangle);
			this.savedVisible = base.Visible;
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000C1E4C File Offset: 0x000C004C
		private void DisplayError(Exception e)
		{
			IUIService iuiservice = (IUIService)this.host.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				iuiservice.ShowError(e);
				return;
			}
			string text = e.Message;
			if (text == null || text.Length == 0)
			{
				text = e.ToString();
			}
			RTLAwareMessageBox.Show(null, text, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000C1EA8 File Offset: 0x000C00A8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.selSvc != null)
				{
					this.selSvc.SelectionChanged -= this.OnSelectionChanged;
				}
				if (this.host != null)
				{
					this.host.TransactionOpened -= this.OnTransactionOpened;
					this.host.TransactionClosed -= this.OnTransactionClosed;
					if (this.host.InTransaction)
					{
						this.OnTransactionClosed(this.host, new DesignerTransactionCloseEventArgs(true, true));
					}
					IComponentChangeService componentChangeService = (IComponentChangeService)this.host.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentRemoved -= this.OnComponentRemove;
						componentChangeService.ComponentChanged -= this.OnComponentChanged;
					}
				}
				foreach (object obj in this.selectionItems.Values)
				{
					SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)obj;
					selectionUIItem.Dispose();
				}
				this.selectionHandlers.Clear();
				this.selectionItems.Clear();
				SystemEvents.DisplaySettingsChanged -= this.OnSystemSettingChanged;
				SystemEvents.InstalledFontsChanged -= this.OnSystemSettingChanged;
				SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000C2018 File Offset: 0x000C0218
		private void EndMouseDrag(Point position)
		{
			if (base.IsDisposed)
			{
				return;
			}
			this.ignoreCaptureChanged = true;
			base.Capture = false;
			this.mouseDragAnchor = SelectionUIService.InvalidPoint;
			this.mouseDragOffset = Rectangle.Empty;
			this.mouseDragHitTest = 0;
			this.dragMoved = false;
			this.SetSelectionCursor(position);
			this.mouseDragging = (this.ctrlSelect = false);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000C2078 File Offset: 0x000C0278
		private SelectionUIService.HitTestInfo GetHitTest(Point value, int flags)
		{
			Point pt = base.PointToClient(value);
			foreach (object obj in this.selectionItems.Values)
			{
				SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)obj;
				if ((flags & 1) != 0 && selectionUIItem is SelectionUIService.ContainerSelectionUIItem && (selectionUIItem.GetRules() & SelectionRules.Visible) != SelectionRules.None)
				{
					int hitTest = selectionUIItem.GetHitTest(pt);
					if ((hitTest & 512) != 0)
					{
						return new SelectionUIService.HitTestInfo(hitTest, selectionUIItem, true);
					}
				}
				if ((flags & 2) != 0 && !(selectionUIItem is SelectionUIService.ContainerSelectionUIItem) && (selectionUIItem.GetRules() & SelectionRules.Visible) != SelectionRules.None)
				{
					int hitTest2 = selectionUIItem.GetHitTest(pt);
					if (hitTest2 != 256)
					{
						if (hitTest2 != 0)
						{
							return new SelectionUIService.HitTestInfo(hitTest2, selectionUIItem);
						}
						return new SelectionUIService.HitTestInfo(256, selectionUIItem);
					}
				}
			}
			return new SelectionUIService.HitTestInfo(256, null);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x000C2174 File Offset: 0x000C0374
		private ISelectionUIHandler GetHandler(object component)
		{
			return (ISelectionUIHandler)this.selectionHandlers[component];
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000C2188 File Offset: 0x000C0388
		public static string GetTransactionName(SelectionRules rules, object[] objects)
		{
			string @string;
			if ((rules & SelectionRules.Moveable) != SelectionRules.None)
			{
				if (objects.Length > 1)
				{
					@string = SR.GetString("DragDropMoveComponents", new object[]
					{
						objects.Length
					});
				}
				else
				{
					string text = string.Empty;
					if (objects.Length != 0)
					{
						IComponent component = objects[0] as IComponent;
						if (component != null && component.Site != null)
						{
							text = component.Site.Name;
						}
						else
						{
							text = objects[0].GetType().Name;
						}
					}
					@string = SR.GetString("DragDropMoveComponent", new object[]
					{
						text
					});
				}
			}
			else if ((rules & SelectionRules.AllSizeable) != SelectionRules.None)
			{
				if (objects.Length > 1)
				{
					@string = SR.GetString("DragDropSizeComponents", new object[]
					{
						objects.Length
					});
				}
				else
				{
					string text2 = string.Empty;
					if (objects.Length != 0)
					{
						IComponent component2 = objects[0] as IComponent;
						if (component2 != null && component2.Site != null)
						{
							text2 = component2.Site.Name;
						}
						else
						{
							text2 = objects[0].GetType().Name;
						}
					}
					@string = SR.GetString("DragDropSizeComponent", new object[]
					{
						text2
					});
				}
			}
			else
			{
				@string = SR.GetString("DragDropDragComponents", new object[]
				{
					objects.Length
				});
			}
			return @string;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000C22B5 File Offset: 0x000C04B5
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (e.LastTransaction)
			{
				this.batchMode = false;
				if (this.batchChanged)
				{
					this.batchChanged = false;
					((ISelectionUIService)this).SyncSelection();
				}
				if (this.batchSync)
				{
					this.batchSync = false;
					((ISelectionUIService)this).SyncComponent(null);
				}
			}
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000C22F1 File Offset: 0x000C04F1
		private void OnTransactionOpened(object sender, EventArgs e)
		{
			this.batchMode = true;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000C22FA File Offset: 0x000C04FA
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateWindowRegion();
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000C2309 File Offset: 0x000C0509
		private void OnComponentChanged(object sender, ComponentChangedEventArgs ccevent)
		{
			if (!this.batchMode)
			{
				((ISelectionUIService)this).SyncSelection();
				return;
			}
			this.batchChanged = true;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000C2321 File Offset: 0x000C0521
		private void OnComponentRemove(object sender, ComponentEventArgs ce)
		{
			this.selectionHandlers.Remove(ce.Component);
			this.selectionItems.Remove(ce.Component);
			((ISelectionUIService)this).SyncComponent(ce.Component);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000C2351 File Offset: 0x000C0551
		private void OnContainerSelectorActive(ContainerSelectorActiveEventArgs e)
		{
			if (this.containerSelectorActive != null)
			{
				this.containerSelectorActive(this, e);
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000C2368 File Offset: 0x000C0568
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			ICollection selectedComponents = this.selSvc.GetSelectedComponents();
			Hashtable hashtable = new Hashtable(selectedComponents.Count);
			bool flag = false;
			foreach (object obj in selectedComponents)
			{
				object obj2 = this.selectionItems[obj];
				bool flag2 = true;
				if (obj2 != null)
				{
					SelectionUIService.ContainerSelectionUIItem containerSelectionUIItem = obj2 as SelectionUIService.ContainerSelectionUIItem;
					if (containerSelectionUIItem != null)
					{
						containerSelectionUIItem.Dispose();
						flag = true;
					}
					else
					{
						hashtable[obj] = obj2;
						flag2 = false;
					}
				}
				if (flag2)
				{
					flag = true;
					hashtable[obj] = new SelectionUIService.SelectionUIItem(this, obj);
				}
			}
			if (!flag)
			{
				flag = (this.selectionItems.Keys.Count != hashtable.Keys.Count);
			}
			this.selectionItems = hashtable;
			if (flag)
			{
				this.UpdateWindowRegion();
			}
			base.Invalidate();
			base.Update();
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000C2460 File Offset: 0x000C0660
		private void OnSystemSettingChanged(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000C2460 File Offset: 0x000C0660
		private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000C2468 File Offset: 0x000C0668
		protected override void OnDragEnter(DragEventArgs devent)
		{
			base.OnDragEnter(devent);
			if (this.dragHandler != null)
			{
				this.dragHandler.OleDragEnter(devent);
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000C2485 File Offset: 0x000C0685
		protected override void OnDragOver(DragEventArgs devent)
		{
			base.OnDragOver(devent);
			if (this.dragHandler != null)
			{
				this.dragHandler.OleDragOver(devent);
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000C24A2 File Offset: 0x000C06A2
		protected override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);
			if (this.dragHandler != null)
			{
				this.dragHandler.OleDragLeave();
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000C24BE File Offset: 0x000C06BE
		protected override void OnDragDrop(DragEventArgs devent)
		{
			base.OnDragDrop(devent);
			if (this.dragHandler != null)
			{
				this.dragHandler.OleDragDrop(devent);
			}
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000C24DC File Offset: 0x000C06DC
		protected override void OnDoubleClick(EventArgs devent)
		{
			base.OnDoubleClick(devent);
			if (this.selSvc != null)
			{
				object primarySelection = this.selSvc.PrimarySelection;
				if (primarySelection != null)
				{
					ISelectionUIHandler handler = this.GetHandler(primarySelection);
					if (handler != null)
					{
						handler.OnSelectionDoubleClick((IComponent)primarySelection);
					}
				}
			}
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000C2520 File Offset: 0x000C0720
		protected override void OnMouseDown(MouseEventArgs me)
		{
			if (this.dragHandler == null && this.selSvc != null)
			{
				try
				{
					Point point = base.PointToScreen(new Point(me.X, me.Y));
					SelectionUIService.HitTestInfo hitTest = this.GetHitTest(point, 3);
					int hitTest2 = hitTest.hitTest;
					if ((hitTest2 & 512) != 0)
					{
						this.selSvc.SetSelectedComponents(new object[]
						{
							hitTest.selectionUIHit.component
						}, SelectionTypes.Auto);
						SelectionRules rules = SelectionRules.Moveable;
						if (((ISelectionUIService)this).BeginDrag(rules, point.X, point.Y))
						{
							base.Visible = false;
							this.containerDrag = hitTest.selectionUIHit.component;
							this.BeginMouseDrag(point, hitTest2);
						}
					}
					else if (hitTest2 != 256 && me.Button == MouseButtons.Left)
					{
						SelectionRules selectionRules = SelectionRules.None;
						this.ctrlSelect = ((Control.ModifierKeys & Keys.Control) > Keys.None);
						if (!this.ctrlSelect)
						{
							this.selSvc.SetSelectedComponents(new object[]
							{
								hitTest.selectionUIHit.component
							}, SelectionTypes.Click);
						}
						if ((hitTest2 & 12) != 0)
						{
							selectionRules |= SelectionRules.Moveable;
						}
						if ((hitTest2 & 3) != 0)
						{
							if ((hitTest2 & 65) == 65)
							{
								selectionRules |= SelectionRules.RightSizeable;
							}
							if ((hitTest2 & 17) == 17)
							{
								selectionRules |= SelectionRules.LeftSizeable;
							}
							if ((hitTest2 & 34) == 34)
							{
								selectionRules |= SelectionRules.TopSizeable;
							}
							if ((hitTest2 & 130) == 130)
							{
								selectionRules |= SelectionRules.BottomSizeable;
							}
							if (((ISelectionUIService)this).BeginDrag(selectionRules, point.X, point.Y))
							{
								this.BeginMouseDrag(point, hitTest2);
							}
						}
						else
						{
							this.dragRules = selectionRules;
							this.BeginMouseDrag(point, hitTest2);
						}
					}
					else if (hitTest2 == 256)
					{
						this.dragRules = SelectionRules.None;
						this.mouseDragAnchor = SelectionUIService.InvalidPoint;
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
					if (ex != CheckoutException.Canceled)
					{
						this.DisplayError(ex);
					}
				}
			}
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x000C2714 File Offset: 0x000C0914
		protected override void OnMouseMove(MouseEventArgs me)
		{
			base.OnMouseMove(me);
			Point point = base.PointToScreen(new Point(me.X, me.Y));
			SelectionUIService.HitTestInfo hitTest = this.GetHitTest(point, 1);
			int hitTest2 = hitTest.hitTest;
			if (hitTest2 != 512 && hitTest.selectionUIHit != null)
			{
				this.OnContainerSelectorActive(new ContainerSelectorActiveEventArgs(hitTest.selectionUIHit.component));
			}
			if (this.lastMoveScreenCoord == point)
			{
				return;
			}
			if (!this.mouseDragging)
			{
				this.SetSelectionCursor(point);
				return;
			}
			if (!((ISelectionUIService)this).Dragging && (this.mouseDragHitTest & 12) != 0)
			{
				Size dragSize = SystemInformation.DragSize;
				if (Math.Abs(point.X - this.mouseDragAnchor.X) < dragSize.Width && Math.Abs(point.Y - this.mouseDragAnchor.Y) < dragSize.Height)
				{
					return;
				}
				this.ignoreCaptureChanged = true;
				if (!((ISelectionUIService)this).BeginDrag(this.dragRules, this.mouseDragAnchor.X, this.mouseDragAnchor.Y))
				{
					this.EndMouseDrag(Control.MousePosition);
					return;
				}
				this.ctrlSelect = false;
			}
			Rectangle rectangle = this.mouseDragOffset;
			if ((this.mouseDragHitTest & 4) != 0)
			{
				this.mouseDragOffset.X = point.X - this.mouseDragAnchor.X;
			}
			if ((this.mouseDragHitTest & 8) != 0)
			{
				this.mouseDragOffset.Y = point.Y - this.mouseDragAnchor.Y;
			}
			if ((this.mouseDragHitTest & 1) != 0)
			{
				if ((this.mouseDragHitTest & 16) != 0)
				{
					this.mouseDragOffset.X = point.X - this.mouseDragAnchor.X;
					this.mouseDragOffset.Width = this.mouseDragAnchor.X - point.X;
				}
				else
				{
					this.mouseDragOffset.Width = point.X - this.mouseDragAnchor.X;
				}
			}
			if ((this.mouseDragHitTest & 2) != 0)
			{
				if ((this.mouseDragHitTest & 32) != 0)
				{
					this.mouseDragOffset.Y = point.Y - this.mouseDragAnchor.Y;
					this.mouseDragOffset.Height = this.mouseDragAnchor.Y - point.Y;
				}
				else
				{
					this.mouseDragOffset.Height = point.Y - this.mouseDragAnchor.Y;
				}
			}
			if (!rectangle.Equals(this.mouseDragOffset))
			{
				Rectangle offset = this.mouseDragOffset;
				offset.X -= rectangle.X;
				offset.Y -= rectangle.Y;
				offset.Width -= rectangle.Width;
				offset.Height -= rectangle.Height;
				if (offset.X != 0 || offset.Y != 0 || offset.Width != 0 || offset.Height != 0)
				{
					if ((this.mouseDragHitTest & 4) != 0 || (this.mouseDragHitTest & 8) != 0)
					{
						this.Cursor = Cursors.Default;
					}
					((ISelectionUIService)this).DragMoved(offset);
				}
			}
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000C2A38 File Offset: 0x000C0C38
		protected override void OnMouseUp(MouseEventArgs me)
		{
			try
			{
				Point point = base.PointToScreen(new Point(me.X, me.Y));
				if (this.ctrlSelect && !this.mouseDragging && this.selSvc != null)
				{
					SelectionUIService.HitTestInfo hitTest = this.GetHitTest(point, 3);
					this.selSvc.SetSelectedComponents(new object[]
					{
						hitTest.selectionUIHit.component
					}, SelectionTypes.Click);
				}
				if (this.mouseDragging)
				{
					object obj = this.containerDrag;
					bool flag = this.dragMoved;
					this.EndMouseDrag(point);
					if (((ISelectionUIService)this).Dragging)
					{
						((ISelectionUIService)this).EndDrag(false);
					}
					if (me.Button == MouseButtons.Right && obj != null && !flag)
					{
						this.OnContainerSelectorActive(new ContainerSelectorActiveEventArgs(obj, ContainerSelectorActiveEventArgsType.Contextmenu));
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
				if (ex != CheckoutException.Canceled)
				{
					this.DisplayError(ex);
				}
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000C2B20 File Offset: 0x000C0D20
		protected override void OnMove(EventArgs e)
		{
			base.OnMove(e);
			base.Invalidate();
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000C2B30 File Offset: 0x000C0D30
		protected override void OnPaint(PaintEventArgs e)
		{
			foreach (object obj in this.selectionItems.Values)
			{
				SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)obj;
				if (!(selectionUIItem is SelectionUIService.ContainerSelectionUIItem))
				{
					selectionUIItem.DoPaint(e.Graphics);
				}
			}
			foreach (object obj2 in this.selectionItems.Values)
			{
				SelectionUIService.SelectionUIItem selectionUIItem2 = (SelectionUIService.SelectionUIItem)obj2;
				if (selectionUIItem2 is SelectionUIService.ContainerSelectionUIItem)
				{
					selectionUIItem2.DoPaint(e.Graphics);
				}
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000C2BF8 File Offset: 0x000C0DF8
		private void SetSelectionCursor(Point pt)
		{
			Point pt2 = base.PointToClient(pt);
			foreach (object obj in this.selectionItems.Values)
			{
				SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)obj;
				if (!(selectionUIItem is SelectionUIService.ContainerSelectionUIItem))
				{
					Cursor cursorAtPoint = selectionUIItem.GetCursorAtPoint(pt2);
					if (cursorAtPoint != null)
					{
						if (cursorAtPoint == Cursors.Default)
						{
							this.Cursor = null;
							return;
						}
						this.Cursor = cursorAtPoint;
						return;
					}
				}
			}
			foreach (object obj2 in this.selectionItems.Values)
			{
				SelectionUIService.SelectionUIItem selectionUIItem2 = (SelectionUIService.SelectionUIItem)obj2;
				if (selectionUIItem2 is SelectionUIService.ContainerSelectionUIItem)
				{
					Cursor cursorAtPoint2 = selectionUIItem2.GetCursorAtPoint(pt2);
					if (cursorAtPoint2 != null)
					{
						if (cursorAtPoint2 == Cursors.Default)
						{
							this.Cursor = null;
							return;
						}
						this.Cursor = cursorAtPoint2;
						return;
					}
				}
			}
			this.Cursor = null;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000C2D2C File Offset: 0x000C0F2C
		private void UpdateWindowRegion()
		{
			Region region = new Region(new Rectangle(0, 0, 0, 0));
			foreach (object obj in this.selectionItems.Values)
			{
				SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)obj;
				region.Union(selectionUIItem.GetRegion());
			}
			base.Region = region;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x000C2DA8 File Offset: 0x000C0FA8
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 514 && msg != 517)
			{
				if (msg == 533)
				{
					if (!this.ignoreCaptureChanged && this.mouseDragAnchor != SelectionUIService.InvalidPoint)
					{
						this.EndMouseDrag(Control.MousePosition);
						if (((ISelectionUIService)this).Dragging)
						{
							((ISelectionUIService)this).EndDrag(true);
						}
					}
					this.ignoreCaptureChanged = false;
				}
			}
			else if (this.mouseDragAnchor != SelectionUIService.InvalidPoint)
			{
				this.ignoreCaptureChanged = true;
			}
			base.WndProc(ref m);
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x000C2E33 File Offset: 0x000C1033
		bool ISelectionUIService.Dragging
		{
			get
			{
				return this.dragHandler != null;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x000C2E3E File Offset: 0x000C103E
		// (set) Token: 0x0600201E RID: 8222 RVA: 0x000C2E46 File Offset: 0x000C1046
		bool ISelectionUIService.Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600201F RID: 8223 RVA: 0x000C2E4F File Offset: 0x000C104F
		// (remove) Token: 0x06002020 RID: 8224 RVA: 0x000C2E68 File Offset: 0x000C1068
		event ContainerSelectorActiveEventHandler ISelectionUIService.ContainerSelectorActive
		{
			add
			{
				this.containerSelectorActive = (ContainerSelectorActiveEventHandler)Delegate.Combine(this.containerSelectorActive, value);
			}
			remove
			{
				this.containerSelectorActive = (ContainerSelectorActiveEventHandler)Delegate.Remove(this.containerSelectorActive, value);
			}
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000C2E84 File Offset: 0x000C1084
		void ISelectionUIService.AssignSelectionUIHandler(object component, ISelectionUIHandler handler)
		{
			ISelectionUIHandler selectionUIHandler = (ISelectionUIHandler)this.selectionHandlers[component];
			if (selectionUIHandler == null)
			{
				this.selectionHandlers[component] = handler;
				if (this.selSvc != null && this.selSvc.GetComponentSelected(component))
				{
					SelectionUIService.SelectionUIItem selectionUIItem = new SelectionUIService.SelectionUIItem(this, component);
					this.selectionItems[component] = selectionUIItem;
					this.UpdateWindowRegion();
					selectionUIItem.Invalidate();
				}
				return;
			}
			if (handler == selectionUIHandler)
			{
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000C2EF8 File Offset: 0x000C10F8
		void ISelectionUIService.ClearSelectionUIHandler(object component, ISelectionUIHandler handler)
		{
			ISelectionUIHandler selectionUIHandler = (ISelectionUIHandler)this.selectionHandlers[component];
			if (selectionUIHandler == handler)
			{
				this.selectionHandlers[component] = null;
			}
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000C2F28 File Offset: 0x000C1128
		bool ISelectionUIService.BeginDrag(SelectionRules rules, int initialX, int initialY)
		{
			if (this.dragHandler != null)
			{
				return false;
			}
			if (rules == SelectionRules.None)
			{
				return false;
			}
			if (this.selSvc == null)
			{
				return false;
			}
			this.savedVisible = base.Visible;
			ICollection selectedComponents = this.selSvc.GetSelectedComponents();
			object[] array = new object[selectedComponents.Count];
			selectedComponents.CopyTo(array, 0);
			array = ((ISelectionUIService)this).FilterSelection(array, rules);
			if (array.Length == 0)
			{
				return false;
			}
			ISelectionUIHandler selectionUIHandler = null;
			object primarySelection = this.selSvc.PrimarySelection;
			if (primarySelection != null)
			{
				selectionUIHandler = this.GetHandler(primarySelection);
			}
			if (selectionUIHandler == null)
			{
				return false;
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array.Length; i++)
			{
				if (this.GetHandler(array[i]) == selectionUIHandler)
				{
					SelectionRules componentRules = selectionUIHandler.GetComponentRules(array[i]);
					if ((componentRules & rules) == rules)
					{
						arrayList.Add(array[i]);
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return false;
			}
			array = arrayList.ToArray();
			bool flag = false;
			this.dragComponents = array;
			this.dragRules = rules;
			this.dragHandler = selectionUIHandler;
			string transactionName = SelectionUIService.GetTransactionName(rules, array);
			this.dragTransaction = this.host.CreateTransaction(transactionName);
			try
			{
				if (selectionUIHandler.QueryBeginDrag(array, rules, initialX, initialY) && this.dragHandler != null)
				{
					try
					{
						flag = selectionUIHandler.BeginDrag(array, rules, initialX, initialY);
					}
					catch (Exception ex)
					{
						flag = false;
					}
				}
			}
			finally
			{
				if (!flag)
				{
					this.dragComponents = null;
					this.dragRules = SelectionRules.None;
					this.dragHandler = null;
					if (this.dragTransaction != null)
					{
						this.dragTransaction.Commit();
						this.dragTransaction = null;
					}
				}
			}
			return flag;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000C30B0 File Offset: 0x000C12B0
		void ISelectionUIService.DragMoved(Rectangle offset)
		{
			Rectangle empty = Rectangle.Empty;
			if (this.dragHandler == null)
			{
				throw new Exception(SR.GetString("DesignerBeginDragNotCalled"));
			}
			if ((this.dragRules & SelectionRules.Moveable) == SelectionRules.None && (this.dragRules & (SelectionRules.TopSizeable | SelectionRules.LeftSizeable)) == SelectionRules.None)
			{
				empty = new Rectangle(0, 0, offset.Width, offset.Height);
			}
			if ((this.dragRules & SelectionRules.AllSizeable) == SelectionRules.None)
			{
				if (empty.IsEmpty)
				{
					empty = new Rectangle(offset.X, offset.Y, 0, 0);
				}
				else
				{
					empty.Width = (empty.Height = 0);
				}
			}
			if (!empty.IsEmpty)
			{
				offset = empty;
			}
			base.Visible = false;
			this.dragMoved = true;
			this.dragHandler.DragMoved(this.dragComponents, offset);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000C3178 File Offset: 0x000C1378
		void ISelectionUIService.EndDrag(bool cancel)
		{
			this.containerDrag = null;
			ISelectionUIHandler selectionUIHandler = this.dragHandler;
			object[] array = this.dragComponents;
			this.dragHandler = null;
			this.dragComponents = null;
			this.dragRules = SelectionRules.None;
			if (selectionUIHandler == null)
			{
				throw new InvalidOperationException();
			}
			DesignerTransaction designerTransaction = null;
			try
			{
				IComponent component = array[0] as IComponent;
				if (array.Length > 1 || (array.Length == 1 && component != null && component.Site == null))
				{
					designerTransaction = this.host.CreateTransaction(SR.GetString("DragDropMoveComponents", new object[]
					{
						array.Length
					}));
				}
				else if (array.Length == 1 && component != null)
				{
					designerTransaction = this.host.CreateTransaction(SR.GetString("DragDropMoveComponent", new object[]
					{
						component.Site.Name
					}));
				}
				try
				{
					selectionUIHandler.EndDrag(array, cancel);
				}
				catch (Exception ex)
				{
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
				base.Visible = this.savedVisible;
				((ISelectionUIService)this).SyncSelection();
				if (this.dragTransaction != null)
				{
					this.dragTransaction.Commit();
					this.dragTransaction = null;
				}
				this.EndMouseDrag(Control.MousePosition);
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000C32A4 File Offset: 0x000C14A4
		object[] ISelectionUIService.FilterSelection(object[] components, SelectionRules selectionRules)
		{
			object[] array = null;
			if (components == null)
			{
				return new object[0];
			}
			if (selectionRules != SelectionRules.None)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in components)
				{
					SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)this.selectionItems[obj];
					if (selectionUIItem != null && !(selectionUIItem is SelectionUIService.ContainerSelectionUIItem) && (selectionUIItem.GetRules() & selectionRules) == selectionRules)
					{
						arrayList.Add(obj);
					}
				}
				array = arrayList.ToArray();
			}
			if (array != null)
			{
				return array;
			}
			return new object[0];
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000C3323 File Offset: 0x000C1523
		Size ISelectionUIService.GetAdornmentDimensions(AdornmentType adornmentType)
		{
			if (adornmentType == AdornmentType.GrabHandle)
			{
				return new Size(7, 7);
			}
			if (adornmentType - AdornmentType.ContainerSelector > 1)
			{
				return new Size(0, 0);
			}
			return new Size(13, 13);
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000C334A File Offset: 0x000C154A
		bool ISelectionUIService.GetAdornmentHitTest(object component, Point value)
		{
			return this.GetHitTest(value, 3).hitTest != 256;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000C3363 File Offset: 0x000C1563
		bool ISelectionUIService.GetContainerSelected(object component)
		{
			return component != null && this.selectionItems[component] is SelectionUIService.ContainerSelectionUIItem;
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000C3380 File Offset: 0x000C1580
		SelectionRules ISelectionUIService.GetSelectionRules(object component)
		{
			SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)this.selectionItems[component];
			if (selectionUIItem == null)
			{
				throw new InvalidOperationException();
			}
			return selectionUIItem.GetRules();
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000C33B0 File Offset: 0x000C15B0
		SelectionStyles ISelectionUIService.GetSelectionStyle(object component)
		{
			SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)this.selectionItems[component];
			if (selectionUIItem == null)
			{
				return SelectionStyles.None;
			}
			return selectionUIItem.Style;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000C33DC File Offset: 0x000C15DC
		void ISelectionUIService.SetContainerSelected(object component, bool selected)
		{
			if (selected)
			{
				SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)this.selectionItems[component];
				if (!(selectionUIItem is SelectionUIService.ContainerSelectionUIItem))
				{
					if (selectionUIItem != null)
					{
						selectionUIItem.Dispose();
					}
					SelectionUIService.SelectionUIItem selectionUIItem2 = new SelectionUIService.ContainerSelectionUIItem(this, component);
					this.selectionItems[component] = selectionUIItem2;
					this.UpdateWindowRegion();
					if (selectionUIItem != null)
					{
						selectionUIItem.Invalidate();
					}
					selectionUIItem2.Invalidate();
					return;
				}
			}
			else
			{
				SelectionUIService.SelectionUIItem selectionUIItem3 = (SelectionUIService.SelectionUIItem)this.selectionItems[component];
				if (selectionUIItem3 == null || selectionUIItem3 is SelectionUIService.ContainerSelectionUIItem)
				{
					this.selectionItems.Remove(component);
					if (selectionUIItem3 != null)
					{
						selectionUIItem3.Dispose();
					}
					this.UpdateWindowRegion();
					selectionUIItem3.Invalidate();
				}
			}
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000C3478 File Offset: 0x000C1678
		void ISelectionUIService.SetSelectionStyle(object component, SelectionStyles style)
		{
			SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)this.selectionItems[component];
			if (this.selSvc != null && this.selSvc.GetComponentSelected(component))
			{
				selectionUIItem = new SelectionUIService.SelectionUIItem(this, component);
				this.selectionItems[component] = selectionUIItem;
			}
			if (selectionUIItem != null)
			{
				selectionUIItem.Style = style;
				this.UpdateWindowRegion();
				selectionUIItem.Invalidate();
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000C34D8 File Offset: 0x000C16D8
		void ISelectionUIService.SyncSelection()
		{
			if (this.batchMode)
			{
				this.batchChanged = true;
				return;
			}
			if (base.IsHandleCreated)
			{
				bool flag = false;
				foreach (object obj in this.selectionItems.Values)
				{
					SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)obj;
					flag |= selectionUIItem.UpdateSize();
					selectionUIItem.UpdateRules();
				}
				if (flag)
				{
					this.UpdateWindowRegion();
					base.Update();
				}
			}
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000C3568 File Offset: 0x000C1768
		void ISelectionUIService.SyncComponent(object component)
		{
			if (this.batchMode)
			{
				this.batchSync = true;
				return;
			}
			if (base.IsHandleCreated)
			{
				foreach (object obj in this.selectionItems.Values)
				{
					SelectionUIService.SelectionUIItem selectionUIItem = (SelectionUIService.SelectionUIItem)obj;
					selectionUIItem.UpdateRules();
					selectionUIItem.Dispose();
				}
				this.UpdateWindowRegion();
				base.Invalidate();
				base.Update();
			}
		}

		// Token: 0x040018B0 RID: 6320
		private static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

		// Token: 0x040018B1 RID: 6321
		private const int HITTEST_CONTAINER_SELECTOR = 1;

		// Token: 0x040018B2 RID: 6322
		private const int HITTEST_NORMAL_SELECTION = 2;

		// Token: 0x040018B3 RID: 6323
		private const int HITTEST_DEFAULT = 3;

		// Token: 0x040018B4 RID: 6324
		private ISelectionUIHandler dragHandler;

		// Token: 0x040018B5 RID: 6325
		private object[] dragComponents;

		// Token: 0x040018B6 RID: 6326
		private SelectionRules dragRules;

		// Token: 0x040018B7 RID: 6327
		private bool dragMoved;

		// Token: 0x040018B8 RID: 6328
		private object containerDrag;

		// Token: 0x040018B9 RID: 6329
		private bool ignoreCaptureChanged;

		// Token: 0x040018BA RID: 6330
		private int mouseDragHitTest;

		// Token: 0x040018BB RID: 6331
		private Point mouseDragAnchor = SelectionUIService.InvalidPoint;

		// Token: 0x040018BC RID: 6332
		private Rectangle mouseDragOffset = Rectangle.Empty;

		// Token: 0x040018BD RID: 6333
		private Point lastMoveScreenCoord = Point.Empty;

		// Token: 0x040018BE RID: 6334
		private bool ctrlSelect;

		// Token: 0x040018BF RID: 6335
		private bool mouseDragging;

		// Token: 0x040018C0 RID: 6336
		private ContainerSelectorActiveEventHandler containerSelectorActive;

		// Token: 0x040018C1 RID: 6337
		private Hashtable selectionItems;

		// Token: 0x040018C2 RID: 6338
		private Hashtable selectionHandlers;

		// Token: 0x040018C3 RID: 6339
		private bool savedVisible;

		// Token: 0x040018C4 RID: 6340
		private bool batchMode;

		// Token: 0x040018C5 RID: 6341
		private bool batchChanged;

		// Token: 0x040018C6 RID: 6342
		private bool batchSync;

		// Token: 0x040018C7 RID: 6343
		private ISelectionService selSvc;

		// Token: 0x040018C8 RID: 6344
		private IDesignerHost host;

		// Token: 0x040018C9 RID: 6345
		private DesignerTransaction dragTransaction;

		// Token: 0x0200058A RID: 1418
		private class SelectionUIItem
		{
			// Token: 0x060032A2 RID: 12962 RVA: 0x00111E30 File Offset: 0x00110030
			public SelectionUIItem(SelectionUIService selUIsvc, object component)
			{
				this.selUIsvc = selUIsvc;
				this.component = component;
				this.selectionStyle = SelectionStyles.Selected;
				this.handler = selUIsvc.GetHandler(component);
				this.sizes = SelectionUIService.SelectionUIItem.inactiveSizeArray;
				this.cursors = SelectionUIService.SelectionUIItem.inactiveCursorArray;
				IComponent component2 = component as IComponent;
				if (component2 != null)
				{
					ControlDesigner controlDesigner = selUIsvc.host.GetDesigner(component2) as ControlDesigner;
					if (controlDesigner != null)
					{
						this.control = controlDesigner.Control;
					}
				}
				this.UpdateRules();
				this.UpdateGrabSettings();
				this.UpdateSize();
			}

			// Token: 0x170009F3 RID: 2547
			// (get) Token: 0x060032A3 RID: 12963 RVA: 0x00111ECF File Offset: 0x001100CF
			// (set) Token: 0x060032A4 RID: 12964 RVA: 0x00111ED7 File Offset: 0x001100D7
			public virtual SelectionStyles Style
			{
				get
				{
					return this.selectionStyle;
				}
				set
				{
					if (value != this.selectionStyle)
					{
						this.selectionStyle = value;
						if (this.region != null)
						{
							this.region.Dispose();
							this.region = null;
						}
					}
				}
			}

			// Token: 0x060032A5 RID: 12965 RVA: 0x00111F04 File Offset: 0x00110104
			public virtual void DoPaint(Graphics gr)
			{
				if ((this.GetRules() & SelectionRules.Visible) == SelectionRules.None)
				{
					return;
				}
				bool flag = false;
				if (this.selUIsvc.selSvc != null)
				{
					flag = (this.component == this.selUIsvc.selSvc.PrimarySelection);
					flag = (flag == this.selUIsvc.selSvc.SelectionCount <= 1);
				}
				Rectangle rectangle = new Rectangle(this.outerRect.X, this.outerRect.Y, 7, 7);
				Rectangle rectangle2 = this.innerRect;
				Rectangle rectangle3 = this.outerRect;
				Region clip = gr.Clip;
				Color backColor = SystemColors.Control;
				if (this.control != null && this.control.Parent != null)
				{
					Control parent = this.control.Parent;
					backColor = parent.BackColor;
				}
				Brush brush = new SolidBrush(backColor);
				gr.ExcludeClip(rectangle2);
				gr.FillRectangle(brush, rectangle3);
				brush.Dispose();
				gr.Clip = clip;
				ControlPaint.DrawSelectionFrame(gr, false, rectangle3, rectangle2, backColor);
				if ((this.GetRules() & SelectionRules.Locked) == SelectionRules.None && (this.GetRules() & SelectionRules.AllSizeable) != SelectionRules.None)
				{
					ControlPaint.DrawGrabHandle(gr, rectangle, flag, this.sizes[0] != 0);
					rectangle.X = rectangle2.X + rectangle2.Width;
					ControlPaint.DrawGrabHandle(gr, rectangle, flag, this.sizes[2] != 0);
					rectangle.Y = rectangle2.Y + rectangle2.Height;
					ControlPaint.DrawGrabHandle(gr, rectangle, flag, this.sizes[7] != 0);
					rectangle.X = rectangle3.X;
					ControlPaint.DrawGrabHandle(gr, rectangle, flag, this.sizes[5] != 0);
					rectangle.X += (rectangle3.Width - 7) / 2;
					ControlPaint.DrawGrabHandle(gr, rectangle, flag, this.sizes[6] != 0);
					rectangle.Y = rectangle3.Y;
					ControlPaint.DrawGrabHandle(gr, rectangle, flag, this.sizes[1] != 0);
					rectangle.X = rectangle3.X;
					rectangle.Y = rectangle2.Y + (rectangle2.Height - 7) / 2;
					ControlPaint.DrawGrabHandle(gr, rectangle, flag, this.sizes[3] != 0);
					rectangle.X = rectangle2.X + rectangle2.Width;
					ControlPaint.DrawGrabHandle(gr, rectangle, flag, this.sizes[4] != 0);
					return;
				}
				ControlPaint.DrawLockedFrame(gr, rectangle3, flag);
			}

			// Token: 0x060032A6 RID: 12966 RVA: 0x0011215C File Offset: 0x0011035C
			public virtual Cursor GetCursorAtPoint(Point pt)
			{
				Cursor result = null;
				if (this.PointWithinSelection(pt))
				{
					int num = -1;
					if ((this.GetRules() & SelectionRules.AllSizeable) != SelectionRules.None)
					{
						num = this.GetHandleIndexOfPoint(pt);
					}
					if (-1 == num)
					{
						if ((this.GetRules() & SelectionRules.Moveable) == SelectionRules.None)
						{
							result = Cursors.Default;
						}
						else
						{
							result = Cursors.SizeAll;
						}
					}
					else
					{
						result = this.cursors[num];
					}
				}
				return result;
			}

			// Token: 0x060032A7 RID: 12967 RVA: 0x001121B8 File Offset: 0x001103B8
			public virtual int GetHitTest(Point pt)
			{
				if (!this.PointWithinSelection(pt))
				{
					return 256;
				}
				int handleIndexOfPoint = this.GetHandleIndexOfPoint(pt);
				if (-1 != handleIndexOfPoint && this.sizes[handleIndexOfPoint] != 0)
				{
					return this.sizes[handleIndexOfPoint];
				}
				if ((this.GetRules() & SelectionRules.Moveable) != SelectionRules.None)
				{
					return 12;
				}
				return 0;
			}

			// Token: 0x060032A8 RID: 12968 RVA: 0x00112208 File Offset: 0x00110408
			private int GetHandleIndexOfPoint(Point pt)
			{
				if (pt.X >= this.outerRect.X && pt.X <= this.innerRect.X)
				{
					if (pt.Y >= this.outerRect.Y && pt.Y <= this.innerRect.Y)
					{
						return 0;
					}
					if (pt.Y >= this.innerRect.Y + this.innerRect.Height && pt.Y <= this.outerRect.Y + this.outerRect.Height)
					{
						return 5;
					}
					if (pt.Y >= this.outerRect.Y + (this.outerRect.Height - 7) / 2 && pt.Y <= this.outerRect.Y + (this.outerRect.Height + 7) / 2)
					{
						return 3;
					}
					return -1;
				}
				else if (pt.Y >= this.outerRect.Y && pt.Y <= this.innerRect.Y)
				{
					if (pt.X >= this.innerRect.X + this.innerRect.Width && pt.X <= this.outerRect.X + this.outerRect.Width)
					{
						return 2;
					}
					if (pt.X >= this.outerRect.X + (this.outerRect.Width - 7) / 2 && pt.X <= this.outerRect.X + (this.outerRect.Width + 7) / 2)
					{
						return 1;
					}
					return -1;
				}
				else if (pt.X >= this.innerRect.X + this.innerRect.Width && pt.X <= this.outerRect.X + this.outerRect.Width)
				{
					if (pt.Y >= this.innerRect.Y + this.innerRect.Height && pt.Y <= this.outerRect.Y + this.outerRect.Height)
					{
						return 7;
					}
					if (pt.Y >= this.outerRect.Y + (this.outerRect.Height - 7) / 2 && pt.Y <= this.outerRect.Y + (this.outerRect.Height + 7) / 2)
					{
						return 4;
					}
					return -1;
				}
				else
				{
					if (pt.Y < this.innerRect.Y + this.innerRect.Height || pt.Y > this.outerRect.Y + this.outerRect.Height)
					{
						return -1;
					}
					if (pt.X >= this.outerRect.X + (this.outerRect.Width - 7) / 2 && pt.X <= this.outerRect.X + (this.outerRect.Width + 7) / 2)
					{
						return 6;
					}
					return -1;
				}
			}

			// Token: 0x060032A9 RID: 12969 RVA: 0x00112518 File Offset: 0x00110718
			public virtual Region GetRegion()
			{
				if (this.region == null)
				{
					if ((this.GetRules() & SelectionRules.Visible) != SelectionRules.None && !this.outerRect.IsEmpty)
					{
						this.region = new Region(this.outerRect);
						this.region.Exclude(this.innerRect);
					}
					else
					{
						this.region = new Region(new Rectangle(0, 0, 0, 0));
					}
					if (this.handler != null)
					{
						Rectangle selectionClipRect = this.handler.GetSelectionClipRect(this.component);
						if (!selectionClipRect.IsEmpty)
						{
							this.region.Intersect(this.selUIsvc.RectangleToClient(selectionClipRect));
						}
					}
				}
				return this.region;
			}

			// Token: 0x060032AA RID: 12970 RVA: 0x001125C3 File Offset: 0x001107C3
			public SelectionRules GetRules()
			{
				return this.selectionRules;
			}

			// Token: 0x060032AB RID: 12971 RVA: 0x001125CB File Offset: 0x001107CB
			public void Dispose()
			{
				if (this.region != null)
				{
					this.region.Dispose();
					this.region = null;
				}
			}

			// Token: 0x060032AC RID: 12972 RVA: 0x001125E7 File Offset: 0x001107E7
			public void Invalidate()
			{
				if (!this.outerRect.IsEmpty && !this.selUIsvc.Disposing)
				{
					this.selUIsvc.Invalidate(this.outerRect);
				}
			}

			// Token: 0x060032AD RID: 12973 RVA: 0x00112614 File Offset: 0x00110814
			protected bool PointWithinSelection(Point pt)
			{
				return (this.GetRules() & SelectionRules.Visible) != SelectionRules.None && !this.outerRect.IsEmpty && !this.innerRect.IsEmpty && pt.X >= this.outerRect.X && pt.X <= this.outerRect.X + this.outerRect.Width && pt.Y >= this.outerRect.Y && pt.Y <= this.outerRect.Y + this.outerRect.Height && (pt.X <= this.innerRect.X || pt.X >= this.innerRect.X + this.innerRect.Width || pt.Y <= this.innerRect.Y || pt.Y >= this.innerRect.Y + this.innerRect.Height);
			}

			// Token: 0x060032AE RID: 12974 RVA: 0x00112724 File Offset: 0x00110924
			private void UpdateGrabSettings()
			{
				SelectionRules rules = this.GetRules();
				if ((rules & SelectionRules.AllSizeable) == SelectionRules.None)
				{
					this.sizes = SelectionUIService.SelectionUIItem.inactiveSizeArray;
					this.cursors = SelectionUIService.SelectionUIItem.inactiveCursorArray;
					return;
				}
				this.sizes = new int[8];
				this.cursors = new Cursor[8];
				Array.Copy(SelectionUIService.SelectionUIItem.activeCursorArrays, this.cursors, this.cursors.Length);
				Array.Copy(SelectionUIService.SelectionUIItem.activeSizeArray, this.sizes, this.sizes.Length);
				if ((rules & SelectionRules.TopSizeable) != SelectionRules.TopSizeable)
				{
					this.sizes[0] = 0;
					this.sizes[1] = 0;
					this.sizes[2] = 0;
					this.cursors[0] = Cursors.Arrow;
					this.cursors[1] = Cursors.Arrow;
					this.cursors[2] = Cursors.Arrow;
				}
				if ((rules & SelectionRules.LeftSizeable) != SelectionRules.LeftSizeable)
				{
					this.sizes[0] = 0;
					this.sizes[3] = 0;
					this.sizes[5] = 0;
					this.cursors[0] = Cursors.Arrow;
					this.cursors[3] = Cursors.Arrow;
					this.cursors[5] = Cursors.Arrow;
				}
				if ((rules & SelectionRules.BottomSizeable) != SelectionRules.BottomSizeable)
				{
					this.sizes[5] = 0;
					this.sizes[6] = 0;
					this.sizes[7] = 0;
					this.cursors[5] = Cursors.Arrow;
					this.cursors[6] = Cursors.Arrow;
					this.cursors[7] = Cursors.Arrow;
				}
				if ((rules & SelectionRules.RightSizeable) != SelectionRules.RightSizeable)
				{
					this.sizes[2] = 0;
					this.sizes[4] = 0;
					this.sizes[7] = 0;
					this.cursors[2] = Cursors.Arrow;
					this.cursors[4] = Cursors.Arrow;
					this.cursors[7] = Cursors.Arrow;
				}
			}

			// Token: 0x060032AF RID: 12975 RVA: 0x001128C0 File Offset: 0x00110AC0
			public void UpdateRules()
			{
				if (this.handler == null)
				{
					this.selectionRules = SelectionRules.None;
					return;
				}
				SelectionRules selectionRules = this.selectionRules;
				this.selectionRules = this.handler.GetComponentRules(this.component);
				if (this.selectionRules != selectionRules)
				{
					this.UpdateGrabSettings();
					this.Invalidate();
				}
			}

			// Token: 0x060032B0 RID: 12976 RVA: 0x00112910 File Offset: 0x00110B10
			public virtual bool UpdateSize()
			{
				bool result = false;
				if (this.handler == null)
				{
					return false;
				}
				if ((this.GetRules() & SelectionRules.Visible) == SelectionRules.None)
				{
					return false;
				}
				this.innerRect = this.handler.GetComponentBounds(this.component);
				if (!this.innerRect.IsEmpty)
				{
					this.innerRect = this.selUIsvc.RectangleToClient(this.innerRect);
					Rectangle rectangle = new Rectangle(this.innerRect.X - 7, this.innerRect.Y - 7, this.innerRect.Width + 14, this.innerRect.Height + 14);
					if (this.outerRect.IsEmpty || !this.outerRect.Equals(rectangle))
					{
						if (!this.outerRect.IsEmpty)
						{
							this.Invalidate();
						}
						this.outerRect = rectangle;
						this.Invalidate();
						if (this.region != null)
						{
							this.region.Dispose();
							this.region = null;
						}
						result = true;
					}
				}
				else
				{
					Rectangle rectangle2 = new Rectangle(0, 0, 0, 0);
					result = (this.outerRect.IsEmpty || !this.outerRect.Equals(rectangle2));
					this.innerRect = (this.outerRect = rectangle2);
				}
				return result;
			}

			// Token: 0x040021BC RID: 8636
			public const int SIZE_X = 1;

			// Token: 0x040021BD RID: 8637
			public const int SIZE_Y = 2;

			// Token: 0x040021BE RID: 8638
			public const int SIZE_MASK = 3;

			// Token: 0x040021BF RID: 8639
			public const int MOVE_X = 4;

			// Token: 0x040021C0 RID: 8640
			public const int MOVE_Y = 8;

			// Token: 0x040021C1 RID: 8641
			public const int MOVE_MASK = 12;

			// Token: 0x040021C2 RID: 8642
			public const int POS_LEFT = 16;

			// Token: 0x040021C3 RID: 8643
			public const int POS_TOP = 32;

			// Token: 0x040021C4 RID: 8644
			public const int POS_RIGHT = 64;

			// Token: 0x040021C5 RID: 8645
			public const int POS_BOTTOM = 128;

			// Token: 0x040021C6 RID: 8646
			public const int POS_MASK = 240;

			// Token: 0x040021C7 RID: 8647
			public const int NOHIT = 256;

			// Token: 0x040021C8 RID: 8648
			public const int CONTAINER_SELECTOR = 512;

			// Token: 0x040021C9 RID: 8649
			public const int GRABHANDLE_WIDTH = 7;

			// Token: 0x040021CA RID: 8650
			public const int GRABHANDLE_HEIGHT = 7;

			// Token: 0x040021CB RID: 8651
			internal static readonly int[] activeSizeArray = new int[]
			{
				51,
				34,
				99,
				17,
				65,
				147,
				130,
				195
			};

			// Token: 0x040021CC RID: 8652
			internal static readonly Cursor[] activeCursorArrays = new Cursor[]
			{
				Cursors.SizeNWSE,
				Cursors.SizeNS,
				Cursors.SizeNESW,
				Cursors.SizeWE,
				Cursors.SizeWE,
				Cursors.SizeNESW,
				Cursors.SizeNS,
				Cursors.SizeNWSE
			};

			// Token: 0x040021CD RID: 8653
			internal static readonly int[] inactiveSizeArray = new int[8];

			// Token: 0x040021CE RID: 8654
			internal static readonly Cursor[] inactiveCursorArray = new Cursor[]
			{
				Cursors.Arrow,
				Cursors.Arrow,
				Cursors.Arrow,
				Cursors.Arrow,
				Cursors.Arrow,
				Cursors.Arrow,
				Cursors.Arrow,
				Cursors.Arrow
			};

			// Token: 0x040021CF RID: 8655
			internal int[] sizes;

			// Token: 0x040021D0 RID: 8656
			internal Cursor[] cursors;

			// Token: 0x040021D1 RID: 8657
			internal SelectionUIService selUIsvc;

			// Token: 0x040021D2 RID: 8658
			internal Rectangle innerRect = Rectangle.Empty;

			// Token: 0x040021D3 RID: 8659
			internal Rectangle outerRect = Rectangle.Empty;

			// Token: 0x040021D4 RID: 8660
			internal Region region;

			// Token: 0x040021D5 RID: 8661
			internal object component;

			// Token: 0x040021D6 RID: 8662
			private Control control;

			// Token: 0x040021D7 RID: 8663
			private SelectionStyles selectionStyle;

			// Token: 0x040021D8 RID: 8664
			private SelectionRules selectionRules;

			// Token: 0x040021D9 RID: 8665
			private ISelectionUIHandler handler;
		}

		// Token: 0x0200058B RID: 1419
		private class ContainerSelectionUIItem : SelectionUIService.SelectionUIItem
		{
			// Token: 0x060032B2 RID: 12978 RVA: 0x00112B28 File Offset: 0x00110D28
			public ContainerSelectionUIItem(SelectionUIService selUIsvc, object component) : base(selUIsvc, component)
			{
			}

			// Token: 0x060032B3 RID: 12979 RVA: 0x00112B32 File Offset: 0x00110D32
			public override Cursor GetCursorAtPoint(Point pt)
			{
				if ((this.GetHitTest(pt) & 512) != 0 && (base.GetRules() & SelectionRules.Moveable) != SelectionRules.None)
				{
					return Cursors.SizeAll;
				}
				return null;
			}

			// Token: 0x060032B4 RID: 12980 RVA: 0x00112B58 File Offset: 0x00110D58
			public override int GetHitTest(Point pt)
			{
				int num = 256;
				if ((base.GetRules() & SelectionRules.Visible) != SelectionRules.None && !this.outerRect.IsEmpty)
				{
					Rectangle rectangle = new Rectangle(this.outerRect.X, this.outerRect.Y, 13, 13);
					if (rectangle.Contains(pt))
					{
						num = 512;
						if ((base.GetRules() & SelectionRules.Moveable) != SelectionRules.None)
						{
							num |= 12;
						}
					}
				}
				return num;
			}

			// Token: 0x060032B5 RID: 12981 RVA: 0x00112BCC File Offset: 0x00110DCC
			public override void DoPaint(Graphics gr)
			{
				if ((base.GetRules() & SelectionRules.Visible) == SelectionRules.None)
				{
					return;
				}
				Rectangle bounds = new Rectangle(this.outerRect.X, this.outerRect.Y, 13, 13);
				ControlPaint.DrawContainerGrabHandle(gr, bounds);
			}

			// Token: 0x060032B6 RID: 12982 RVA: 0x00112C10 File Offset: 0x00110E10
			public override Region GetRegion()
			{
				if (this.region == null)
				{
					if ((base.GetRules() & SelectionRules.Visible) != SelectionRules.None && !this.outerRect.IsEmpty)
					{
						Rectangle rect = new Rectangle(this.outerRect.X, this.outerRect.Y, 13, 13);
						this.region = new Region(rect);
					}
					else
					{
						this.region = new Region(new Rectangle(0, 0, 0, 0));
					}
				}
				return this.region;
			}

			// Token: 0x040021DA RID: 8666
			public const int CONTAINER_WIDTH = 13;

			// Token: 0x040021DB RID: 8667
			public const int CONTAINER_HEIGHT = 13;
		}

		// Token: 0x0200058C RID: 1420
		private struct HitTestInfo
		{
			// Token: 0x060032B7 RID: 12983 RVA: 0x00112C89 File Offset: 0x00110E89
			public HitTestInfo(int hitTest, SelectionUIService.SelectionUIItem selectionUIHit)
			{
				this.hitTest = hitTest;
				this.selectionUIHit = selectionUIHit;
				this.containerSelector = false;
			}

			// Token: 0x060032B8 RID: 12984 RVA: 0x00112CA0 File Offset: 0x00110EA0
			public HitTestInfo(int hitTest, SelectionUIService.SelectionUIItem selectionUIHit, bool containerSelector)
			{
				this.hitTest = hitTest;
				this.selectionUIHit = selectionUIHit;
				this.containerSelector = containerSelector;
			}

			// Token: 0x060032B9 RID: 12985 RVA: 0x00112CB8 File Offset: 0x00110EB8
			public override bool Equals(object obj)
			{
				try
				{
					SelectionUIService.HitTestInfo hitTestInfo = (SelectionUIService.HitTestInfo)obj;
					return this.hitTest == hitTestInfo.hitTest && this.selectionUIHit == hitTestInfo.selectionUIHit && this.containerSelector == hitTestInfo.containerSelector;
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				return false;
			}

			// Token: 0x060032BA RID: 12986 RVA: 0x00112D1C File Offset: 0x00110F1C
			public static bool operator ==(SelectionUIService.HitTestInfo left, SelectionUIService.HitTestInfo right)
			{
				return left.hitTest == right.hitTest && left.selectionUIHit == right.selectionUIHit && left.containerSelector == right.containerSelector;
			}

			// Token: 0x060032BB RID: 12987 RVA: 0x00112D4A File Offset: 0x00110F4A
			public static bool operator !=(SelectionUIService.HitTestInfo left, SelectionUIService.HitTestInfo right)
			{
				return !(left == right);
			}

			// Token: 0x060032BC RID: 12988 RVA: 0x00112D58 File Offset: 0x00110F58
			public override int GetHashCode()
			{
				int num = this.hitTest | this.selectionUIHit.GetHashCode();
				if (this.containerSelector)
				{
					num |= 65536;
				}
				return num;
			}

			// Token: 0x040021DC RID: 8668
			public readonly int hitTest;

			// Token: 0x040021DD RID: 8669
			public readonly SelectionUIService.SelectionUIItem selectionUIHit;

			// Token: 0x040021DE RID: 8670
			public readonly bool containerSelector;
		}
	}
}
