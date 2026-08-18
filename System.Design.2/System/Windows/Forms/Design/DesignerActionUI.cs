using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D1 RID: 721
	internal class DesignerActionUI : IDisposable
	{
		// Token: 0x06001C92 RID: 7314 RVA: 0x000AC4D0 File Offset: 0x000AA6D0
		public DesignerActionUI(IServiceProvider serviceProvider, Adorner containerAdorner)
		{
			this.serviceProvider = serviceProvider;
			this.designerActionAdorner = containerAdorner;
			this.behaviorService = (BehaviorService)serviceProvider.GetService(typeof(BehaviorService));
			this.menuCommandService = (IMenuCommandService)serviceProvider.GetService(typeof(IMenuCommandService));
			this.selSvc = (ISelectionService)serviceProvider.GetService(typeof(ISelectionService));
			if (this.behaviorService == null || this.selSvc == null)
			{
				return;
			}
			this.designerActionService = (DesignerActionService)serviceProvider.GetService(typeof(DesignerActionService));
			if (this.designerActionService == null)
			{
				this.designerActionService = new DesignerActionService(serviceProvider);
				this.disposeActionService = true;
			}
			this.designerActionUIService = (DesignerActionUIService)serviceProvider.GetService(typeof(DesignerActionUIService));
			if (this.designerActionUIService == null)
			{
				this.designerActionUIService = new DesignerActionUIService(serviceProvider);
				this.disposeActionUIService = true;
			}
			this.designerActionUIService.DesignerActionUIStateChange += this.OnDesignerActionUIStateChange;
			this.designerActionService.DesignerActionListsChanged += this.OnDesignerActionsChanged;
			this.lastPanelComponent = null;
			IComponentChangeService componentChangeService = (IComponentChangeService)serviceProvider.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged += this.OnComponentChanged;
			}
			if (this.menuCommandService != null)
			{
				this.cmdShowDesignerActions = new MenuCommand(new EventHandler(this.OnKeyShowDesignerActions), MenuCommands.KeyInvokeSmartTag);
				this.menuCommandService.AddCommand(this.cmdShowDesignerActions);
			}
			this.uiService = (IUIService)serviceProvider.GetService(typeof(IUIService));
			if (this.uiService != null)
			{
				this.mainParentWindow = this.uiService.GetDialogOwnerWindow();
			}
			this.componentToGlyph = new Hashtable();
			this.marshalingControl = new Control();
			this.marshalingControl.CreateControl();
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000AC6AC File Offset: 0x000AA8AC
		public void Dispose()
		{
			if (this.marshalingControl != null)
			{
				this.marshalingControl.Dispose();
				this.marshalingControl = null;
			}
			if (this.serviceProvider != null)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.serviceProvider.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentChanged -= this.OnComponentChanged;
				}
				if (this.cmdShowDesignerActions != null)
				{
					IMenuCommandService menuCommandService = (IMenuCommandService)this.serviceProvider.GetService(typeof(IMenuCommandService));
					if (menuCommandService != null)
					{
						menuCommandService.RemoveCommand(this.cmdShowDesignerActions);
					}
				}
			}
			this.serviceProvider = null;
			this.behaviorService = null;
			this.selSvc = null;
			if (this.designerActionService != null)
			{
				this.designerActionService.DesignerActionListsChanged -= this.OnDesignerActionsChanged;
				if (this.disposeActionService)
				{
					this.designerActionService.Dispose();
				}
			}
			this.designerActionService = null;
			if (this.designerActionUIService != null)
			{
				this.designerActionUIService.DesignerActionUIStateChange -= this.OnDesignerActionUIStateChange;
				if (this.disposeActionUIService)
				{
					this.designerActionUIService.Dispose();
				}
			}
			this.designerActionUIService = null;
			this.designerActionAdorner = null;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x000AC7CB File Offset: 0x000AA9CB
		public DesignerActionGlyph GetDesignerActionGlyph(IComponent comp)
		{
			return this.GetDesignerActionGlyph(comp, null);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000AC7D8 File Offset: 0x000AA9D8
		internal DesignerActionGlyph GetDesignerActionGlyph(IComponent comp, DesignerActionListCollection dalColl)
		{
			InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(comp)[typeof(InheritanceAttribute)];
			if (inheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
			{
				return null;
			}
			if (dalColl == null)
			{
				dalColl = this.designerActionService.GetComponentActions(comp);
			}
			if (dalColl != null && dalColl.Count > 0)
			{
				DesignerActionGlyph designerActionGlyph = null;
				if (this.componentToGlyph[comp] == null)
				{
					DesignerActionBehavior behavior = new DesignerActionBehavior(this.serviceProvider, comp, dalColl, this);
					if (!(comp is Control) || comp is ToolStripDropDown)
					{
						ComponentTray componentTray = this.serviceProvider.GetService(typeof(ComponentTray)) as ComponentTray;
						if (componentTray != null)
						{
							ComponentTray.TrayControl trayControlFromComponent = componentTray.GetTrayControlFromComponent(comp);
							if (trayControlFromComponent != null)
							{
								Rectangle bounds = trayControlFromComponent.Bounds;
								designerActionGlyph = new DesignerActionGlyph(behavior, bounds, componentTray);
							}
						}
					}
					if (designerActionGlyph == null)
					{
						designerActionGlyph = new DesignerActionGlyph(behavior, this.designerActionAdorner);
					}
					if (designerActionGlyph != null)
					{
						this.componentToGlyph.Add(comp, designerActionGlyph);
					}
				}
				else
				{
					designerActionGlyph = (this.componentToGlyph[comp] as DesignerActionGlyph);
					if (designerActionGlyph != null)
					{
						DesignerActionBehavior designerActionBehavior = designerActionGlyph.Behavior as DesignerActionBehavior;
						if (designerActionBehavior != null)
						{
							designerActionBehavior.ActionLists = dalColl;
						}
						designerActionGlyph.Invalidate();
					}
				}
				return designerActionGlyph;
			}
			this.RemoveActionGlyph(comp);
			return null;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000AC8FC File Offset: 0x000AAAFC
		private void OnComponentChanged(object source, ComponentChangedEventArgs ce)
		{
			if (ce.Component == null || ce.Member == null || !this.IsDesignerActionPanelVisible)
			{
				return;
			}
			if (this.lastPanelComponent != null && !this.lastPanelComponent.Equals(ce.Component))
			{
				return;
			}
			DesignerActionGlyph designerActionGlyph = this.componentToGlyph[ce.Component] as DesignerActionGlyph;
			if (designerActionGlyph != null)
			{
				designerActionGlyph.Invalidate();
				if (ce.Member.Name.Equals("Dock"))
				{
					this.RecreatePanel(ce.Component as IComponent);
				}
				if (ce.Member.Name.Equals("Location") || ce.Member.Name.Equals("Width") || ce.Member.Name.Equals("Height"))
				{
					this.UpdateDAPLocation(ce.Component as IComponent, designerActionGlyph);
				}
			}
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000AC9E4 File Offset: 0x000AABE4
		private void RecreatePanel(IComponent comp)
		{
			if (this.inTransaction || comp != this.selSvc.PrimarySelection)
			{
				return;
			}
			IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				bool flag = false;
				IDesignerHostTransactionState designerHostTransactionState = designerHost as IDesignerHostTransactionState;
				if (designerHostTransactionState != null)
				{
					flag = designerHostTransactionState.IsClosingTransaction;
				}
				if (designerHost.InTransaction && !flag)
				{
					designerHost.TransactionClosed += this.DesignerTransactionClosed;
					this.inTransaction = true;
					this.relatedComponentTransaction = comp;
					return;
				}
			}
			this.RecreateInternal(comp);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000ACA6C File Offset: 0x000AAC6C
		private void DesignerTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (e.LastTransaction && this.relatedComponentTransaction != null)
			{
				this.inTransaction = false;
				IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
				designerHost.TransactionClosed -= this.DesignerTransactionClosed;
				this.RecreateInternal(this.relatedComponentTransaction);
				this.relatedComponentTransaction = null;
			}
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000ACAD0 File Offset: 0x000AACD0
		private void RecreateInternal(IComponent comp)
		{
			DesignerActionGlyph designerActionGlyph = this.GetDesignerActionGlyph(comp);
			if (designerActionGlyph != null)
			{
				this.VerifyGlyphIsInAdorner(designerActionGlyph);
				this.RecreatePanel(designerActionGlyph);
				this.UpdateDAPLocation(comp, designerActionGlyph);
			}
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000ACB00 File Offset: 0x000AAD00
		private void RecreatePanel(Glyph glyphWithPanelToRegen)
		{
			if (!this.IsDesignerActionPanelVisible)
			{
				return;
			}
			if (glyphWithPanelToRegen != null)
			{
				DesignerActionBehavior designerActionBehavior = glyphWithPanelToRegen.Behavior as DesignerActionBehavior;
				if (designerActionBehavior != null)
				{
					DesignerActionPanel currentPanel = this.designerActionHost.CurrentPanel;
					currentPanel.UpdateTasks(designerActionBehavior.ActionLists, new DesignerActionListCollection(), SR.GetString("DesignerActionPanel_DefaultPanelTitle", new object[]
					{
						designerActionBehavior.RelatedComponent.GetType().Name
					}), null);
					this.designerActionHost.UpdateContainerSize();
				}
			}
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000ACB74 File Offset: 0x000AAD74
		private void VerifyGlyphIsInAdorner(DesignerActionGlyph glyph)
		{
			if (glyph.IsInComponentTray)
			{
				ComponentTray componentTray = this.serviceProvider.GetService(typeof(ComponentTray)) as ComponentTray;
				if (componentTray.SelectionGlyphs != null && !componentTray.SelectionGlyphs.Contains(glyph))
				{
					componentTray.SelectionGlyphs.Insert(0, glyph);
				}
			}
			else if (this.designerActionAdorner != null && this.designerActionAdorner.Glyphs != null && !this.designerActionAdorner.Glyphs.Contains(glyph))
			{
				this.designerActionAdorner.Glyphs.Insert(0, glyph);
			}
			glyph.InvalidateOwnerLocation();
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000ACC09 File Offset: 0x000AAE09
		private void OnDesignerActionsChanged(object sender, DesignerActionListsChangedEventArgs e)
		{
			if (this.marshalingControl != null && this.marshalingControl.IsHandleCreated)
			{
				this.marshalingControl.BeginInvoke(new DesignerActionUI.ActionChangedEventHandler(this.OnInvokedDesignerActionChanged), new object[]
				{
					sender,
					e
				});
			}
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000ACC48 File Offset: 0x000AAE48
		private void OnDesignerActionUIStateChange(object sender, DesignerActionUIStateChangeEventArgs e)
		{
			IComponent component = e.RelatedObject as IComponent;
			if (component != null)
			{
				DesignerActionGlyph designerActionGlyph = this.GetDesignerActionGlyph(component);
				if (designerActionGlyph != null)
				{
					if (e.ChangeType == DesignerActionUIStateChangeType.Show)
					{
						DesignerActionBehavior designerActionBehavior = designerActionGlyph.Behavior as DesignerActionBehavior;
						if (designerActionBehavior != null)
						{
							designerActionBehavior.ShowUI(designerActionGlyph);
							return;
						}
					}
					else if (e.ChangeType == DesignerActionUIStateChangeType.Hide)
					{
						DesignerActionBehavior designerActionBehavior2 = designerActionGlyph.Behavior as DesignerActionBehavior;
						if (designerActionBehavior2 != null)
						{
							designerActionBehavior2.HideUI();
							return;
						}
					}
					else if (e.ChangeType == DesignerActionUIStateChangeType.Refresh)
					{
						designerActionGlyph.Invalidate();
						this.RecreatePanel((IComponent)e.RelatedObject);
						return;
					}
				}
			}
			else if (e.ChangeType == DesignerActionUIStateChangeType.Hide)
			{
				this.HideDesignerActionPanel();
			}
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000ACCE0 File Offset: 0x000AAEE0
		private void OnInvokedDesignerActionChanged(object sender, DesignerActionListsChangedEventArgs e)
		{
			IComponent component = e.RelatedObject as IComponent;
			DesignerActionGlyph designerActionGlyph = null;
			if (e.ChangeType == DesignerActionListsChangedType.ActionListsAdded)
			{
				if (component == null)
				{
					return;
				}
				IComponent component2 = this.selSvc.PrimarySelection as IComponent;
				if (component2 == e.RelatedObject)
				{
					designerActionGlyph = this.GetDesignerActionGlyph(component, e.ActionLists);
					if (designerActionGlyph != null)
					{
						this.VerifyGlyphIsInAdorner(designerActionGlyph);
					}
					else
					{
						this.RemoveActionGlyph(e.RelatedObject);
					}
				}
			}
			if (e.ChangeType == DesignerActionListsChangedType.ActionListsRemoved && e.ActionLists.Count == 0)
			{
				this.RemoveActionGlyph(e.RelatedObject);
				return;
			}
			if (designerActionGlyph != null)
			{
				this.RecreatePanel(component);
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000ACD74 File Offset: 0x000AAF74
		private void OnKeyShowDesignerActions(object sender, EventArgs e)
		{
			this.ShowDesignerActionPanelForPrimarySelection();
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000ACD80 File Offset: 0x000AAF80
		internal bool ShowDesignerActionPanelForPrimarySelection()
		{
			if (this.selSvc == null)
			{
				return false;
			}
			object primarySelection = this.selSvc.PrimarySelection;
			if (primarySelection == null || !this.componentToGlyph.Contains(primarySelection))
			{
				return false;
			}
			DesignerActionGlyph designerActionGlyph = (DesignerActionGlyph)this.componentToGlyph[primarySelection];
			if (designerActionGlyph != null && designerActionGlyph.Behavior is DesignerActionBehavior)
			{
				DesignerActionBehavior designerActionBehavior = designerActionGlyph.Behavior as DesignerActionBehavior;
				if (designerActionBehavior != null)
				{
					if (!this.IsDesignerActionPanelVisible)
					{
						designerActionBehavior.ShowUI(designerActionGlyph);
						return true;
					}
					designerActionBehavior.HideUI();
					return false;
				}
			}
			return false;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000ACE04 File Offset: 0x000AB004
		internal void RemoveActionGlyph(object relatedObject)
		{
			if (relatedObject == null)
			{
				return;
			}
			if (this.IsDesignerActionPanelVisible && relatedObject == this.lastPanelComponent)
			{
				this.HideDesignerActionPanel();
			}
			DesignerActionGlyph designerActionGlyph = (DesignerActionGlyph)this.componentToGlyph[relatedObject];
			if (designerActionGlyph != null)
			{
				ComponentTray componentTray = this.serviceProvider.GetService(typeof(ComponentTray)) as ComponentTray;
				if (componentTray != null && componentTray.SelectionGlyphs != null && componentTray != null && componentTray.SelectionGlyphs.Contains(designerActionGlyph))
				{
					componentTray.SelectionGlyphs.Remove(designerActionGlyph);
				}
				if (this.designerActionAdorner.Glyphs.Contains(designerActionGlyph))
				{
					this.designerActionAdorner.Glyphs.Remove(designerActionGlyph);
				}
				this.componentToGlyph.Remove(relatedObject);
				IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null && designerHost.InTransaction)
				{
					designerHost.TransactionClosed += this.InvalidateGlyphOnLastTransaction;
					this.relatedGlyphTransaction = designerActionGlyph;
				}
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000ACEF8 File Offset: 0x000AB0F8
		private void InvalidateGlyphOnLastTransaction(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (e.LastTransaction)
			{
				IDesignerHost designerHost = (this.serviceProvider != null) ? (this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost) : null;
				if (designerHost != null)
				{
					designerHost.TransactionClosed -= this.InvalidateGlyphOnLastTransaction;
				}
				if (this.relatedGlyphTransaction != null)
				{
					this.relatedGlyphTransaction.InvalidateOwnerLocation();
				}
				this.relatedGlyphTransaction = null;
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x000ACF62 File Offset: 0x000AB162
		internal void HideDesignerActionPanel()
		{
			if (this.IsDesignerActionPanelVisible)
			{
				this.designerActionHost.Close();
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x000ACF77 File Offset: 0x000AB177
		internal bool IsDesignerActionPanelVisible
		{
			get
			{
				return this.designerActionHost != null && this.designerActionHost.Visible;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x000ACF8E File Offset: 0x000AB18E
		internal IComponent LastPanelComponent
		{
			get
			{
				if (!this.IsDesignerActionPanelVisible)
				{
					return null;
				}
				return this.lastPanelComponent;
			}
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000ACFA0 File Offset: 0x000AB1A0
		private void toolStripDropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if (this.cancelClose || e.Cancel)
			{
				e.Cancel = true;
				return;
			}
			if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
			{
				e.Cancel = true;
			}
			if (e.CloseReason == ToolStripDropDownCloseReason.Keyboard)
			{
				e.Cancel = false;
			}
			if (!e.Cancel)
			{
				if (this.lastPanelComponent == null)
				{
					return;
				}
				Point lastCursorPoint = DesignerUtils.LastCursorPoint;
				DesignerActionGlyph designerActionGlyph = this.componentToGlyph[this.lastPanelComponent] as DesignerActionGlyph;
				if (designerActionGlyph != null)
				{
					Point glyphLocationScreenCoord = this.GetGlyphLocationScreenCoord(this.lastPanelComponent, designerActionGlyph);
					if (new Rectangle(glyphLocationScreenCoord, new Size(designerActionGlyph.Bounds.Width, designerActionGlyph.Bounds.Height)).Contains(lastCursorPoint))
					{
						DesignerActionBehavior designerActionBehavior = designerActionGlyph.Behavior as DesignerActionBehavior;
						designerActionBehavior.IgnoreNextMouseUp = true;
					}
					designerActionGlyph.InvalidateOwnerLocation();
				}
				this.lastPanelComponent = null;
				Behavior behavior = this.behaviorService.PopBehavior(this.dapkb);
			}
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x000AD090 File Offset: 0x000AB290
		internal Point UpdateDAPLocation(IComponent component, DesignerActionGlyph glyph)
		{
			if (component == null)
			{
				component = this.lastPanelComponent;
			}
			if (this.designerActionHost == null)
			{
				return Point.Empty;
			}
			if (component == null || glyph == null)
			{
				return this.designerActionHost.Location;
			}
			if (this.behaviorService != null && !this.behaviorService.AdornerWindowControl.DisplayRectangle.IntersectsWith(glyph.Bounds))
			{
				this.HideDesignerActionPanel();
				return this.designerActionHost.Location;
			}
			Point glyphLocationScreenCoord = this.GetGlyphLocationScreenCoord(component, glyph);
			Rectangle rectangleAnchor = new Rectangle(glyphLocationScreenCoord, glyph.Bounds.Size);
			DockStyle dockEdge;
			Point point = DesignerActionPanel.ComputePreferredDesktopLocation(rectangleAnchor, this.designerActionHost.Size, out dockEdge);
			glyph.DockEdge = dockEdge;
			this.designerActionHost.Location = point;
			return point;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000AD14C File Offset: 0x000AB34C
		private Point GetGlyphLocationScreenCoord(IComponent relatedComponent, Glyph glyph)
		{
			Point result = new Point(0, 0);
			if (relatedComponent is Control && !(relatedComponent is ToolStripDropDown))
			{
				result = this.behaviorService.AdornerWindowPointToScreen(glyph.Bounds.Location);
			}
			else if (relatedComponent is ToolStripItem)
			{
				ToolStripItem toolStripItem = relatedComponent as ToolStripItem;
				if (toolStripItem != null && toolStripItem.Owner != null)
				{
					result = this.behaviorService.AdornerWindowPointToScreen(glyph.Bounds.Location);
				}
			}
			else if (relatedComponent != null)
			{
				ComponentTray componentTray = this.serviceProvider.GetService(typeof(ComponentTray)) as ComponentTray;
				if (componentTray != null)
				{
					result = componentTray.PointToScreen(glyph.Bounds.Location);
				}
			}
			return result;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000AD1FC File Offset: 0x000AB3FC
		internal void ShowDesignerActionPanel(IComponent relatedComponent, DesignerActionPanel panel, DesignerActionGlyph glyph)
		{
			if (this.designerActionHost == null)
			{
				this.designerActionHost = new DesignerActionToolStripDropDown(this, this.mainParentWindow);
				this.designerActionHost.AutoSize = false;
				this.designerActionHost.Padding = Padding.Empty;
				this.designerActionHost.Renderer = new NoBorderRenderer();
				this.designerActionHost.Text = "DesignerActionTopLevelForm";
				this.designerActionHost.Closing += this.toolStripDropDown_Closing;
			}
			this.designerActionHost.AccessibleName = SR.GetString("DesignerActionPanel_DefaultPanelTitle", new object[]
			{
				relatedComponent.GetType().Name
			});
			panel.AccessibleName = SR.GetString("DesignerActionPanel_DefaultPanelTitle", new object[]
			{
				relatedComponent.GetType().Name
			});
			this.designerActionHost.SetDesignerActionPanel(panel, glyph);
			Point screenLocation = this.UpdateDAPLocation(relatedComponent, glyph);
			if (this.behaviorService != null && this.behaviorService.AdornerWindowControl.DisplayRectangle.IntersectsWith(glyph.Bounds))
			{
				if (this.mainParentWindow != null && this.mainParentWindow.Handle != IntPtr.Zero)
				{
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this.designerActionHost, this.designerActionHost.Handle), -8, new HandleRef(this.mainParentWindow, this.mainParentWindow.Handle));
				}
				this.cancelClose = true;
				this.designerActionHost.Show(screenLocation);
				this.designerActionHost.Focus();
				this.designerActionHost.BeginInvoke(new EventHandler(this.OnShowComplete));
				glyph.InvalidateOwnerLocation();
				this.lastPanelComponent = relatedComponent;
				this.dapkb = new DesignerActionKeyboardBehavior(this.designerActionHost.CurrentPanel, this.serviceProvider, this.behaviorService);
				this.behaviorService.PushBehavior(this.dapkb);
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x000AD3D4 File Offset: 0x000AB5D4
		private void OnShowComplete(object sender, EventArgs e)
		{
			this.cancelClose = false;
			if (this.designerActionHost != null && this.designerActionHost.Handle != IntPtr.Zero && this.designerActionHost.Visible)
			{
				UnsafeNativeMethods.SetActiveWindow(new HandleRef(this, this.designerActionHost.Handle));
				this.designerActionHost.CheckFocusIsRight();
			}
		}

		// Token: 0x040016F5 RID: 5877
		private static TraceSwitch DesigneActionPanelTraceSwitch = new TraceSwitch("DesigneActionPanelTrace", "DesignerActionPanel tracing");

		// Token: 0x040016F6 RID: 5878
		private Adorner designerActionAdorner;

		// Token: 0x040016F7 RID: 5879
		private IServiceProvider serviceProvider;

		// Token: 0x040016F8 RID: 5880
		private ISelectionService selSvc;

		// Token: 0x040016F9 RID: 5881
		private DesignerActionService designerActionService;

		// Token: 0x040016FA RID: 5882
		private DesignerActionUIService designerActionUIService;

		// Token: 0x040016FB RID: 5883
		private BehaviorService behaviorService;

		// Token: 0x040016FC RID: 5884
		private IMenuCommandService menuCommandService;

		// Token: 0x040016FD RID: 5885
		private DesignerActionKeyboardBehavior dapkb;

		// Token: 0x040016FE RID: 5886
		private Hashtable componentToGlyph;

		// Token: 0x040016FF RID: 5887
		private Control marshalingControl;

		// Token: 0x04001700 RID: 5888
		private IComponent lastPanelComponent;

		// Token: 0x04001701 RID: 5889
		private IUIService uiService;

		// Token: 0x04001702 RID: 5890
		private IWin32Window mainParentWindow;

		// Token: 0x04001703 RID: 5891
		internal DesignerActionToolStripDropDown designerActionHost;

		// Token: 0x04001704 RID: 5892
		private MenuCommand cmdShowDesignerActions;

		// Token: 0x04001705 RID: 5893
		private bool inTransaction;

		// Token: 0x04001706 RID: 5894
		private IComponent relatedComponentTransaction;

		// Token: 0x04001707 RID: 5895
		private DesignerActionGlyph relatedGlyphTransaction;

		// Token: 0x04001708 RID: 5896
		private bool disposeActionService;

		// Token: 0x04001709 RID: 5897
		private bool disposeActionUIService;

		// Token: 0x0400170A RID: 5898
		internal static readonly TraceSwitch DropDownVisibilityDebug;

		// Token: 0x0400170B RID: 5899
		private bool cancelClose;

		// Token: 0x02000568 RID: 1384
		// (Invoke) Token: 0x060031B3 RID: 12723
		private delegate void ActionChangedEventHandler(object sender, DesignerActionListsChangedEventArgs e);
	}
}
