using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200033C RID: 828
	internal class TabControlDesigner : ParentControlDesigner
	{
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool AllowControlLasso
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000C5A0C File Offset: 0x000C3C0C
		protected override bool DrawGrid
		{
			get
			{
				return !this.disableDrawGrid && base.DrawGrid;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x000C5A20 File Offset: 0x000C3C20
		public override bool ParticipatesWithSnapLines
		{
			get
			{
				if (!this.forwardOnDrag)
				{
					return false;
				}
				TabPageDesigner selectedTabPageDesigner = this.GetSelectedTabPageDesigner();
				return selectedTabPageDesigner == null || selectedTabPageDesigner.ParticipatesWithSnapLines;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x000C5A49 File Offset: 0x000C3C49
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x000C5A51 File Offset: 0x000C3C51
		private int SelectedIndex
		{
			get
			{
				return this.persistedSelectedIndex;
			}
			set
			{
				this.persistedSelectedIndex = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x000C5A5C File Offset: 0x000C3C5C
		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (this.verbs == null)
				{
					this.removeVerb = new DesignerVerb(SR.GetString("TabControlRemove"), new EventHandler(this.OnRemove));
					this.verbs = new DesignerVerbCollection();
					this.verbs.Add(new DesignerVerb(SR.GetString("TabControlAdd"), new EventHandler(this.OnAdd)));
					this.verbs.Add(this.removeVerb);
				}
				if (this.Control != null)
				{
					this.removeVerb.Enabled = (this.Control.Controls.Count > 0);
				}
				return this.verbs;
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000C5B04 File Offset: 0x000C3D04
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			try
			{
				this.addingOnInitialize = true;
				this.OnAdd(this, EventArgs.Empty);
				this.OnAdd(this, EventArgs.Empty);
			}
			finally
			{
				this.addingOnInitialize = false;
			}
			MemberDescriptor member = TypeDescriptor.GetProperties(base.Component)["Controls"];
			base.RaiseComponentChanging(member);
			base.RaiseComponentChanged(member, null, null);
			TabControl tabControl = (TabControl)base.Component;
			if (tabControl != null)
			{
				tabControl.SelectedIndex = 0;
			}
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000C5B90 File Offset: 0x000C3D90
		public override bool CanParent(Control control)
		{
			return control is TabPage && !this.Control.Contains(control);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000C5BAB File Offset: 0x000C3DAB
		private void CheckVerbStatus()
		{
			if (this.removeVerb != null)
			{
				this.removeVerb.Enabled = (this.Control.Controls.Count > 0);
			}
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000C5BD4 File Offset: 0x000C3DD4
		protected override IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			TabControl tabControl = (TabControl)this.Control;
			if (tabControl.SelectedTab == null)
			{
				throw new ArgumentException(SR.GetString("TabControlInvalidTabPageType", new object[]
				{
					tool.DisplayName
				}));
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				TabPageDesigner toInvoke = (TabPageDesigner)designerHost.GetDesigner(tabControl.SelectedTab);
				ParentControlDesigner.InvokeCreateTool(toInvoke, tool);
			}
			return null;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000C5C48 File Offset: 0x000C3E48
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SelectionChanged -= this.OnSelectionChanged;
				}
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentChanged -= this.OnComponentChanged;
				}
				TabControl tabControl = this.Control as TabControl;
				if (tabControl != null)
				{
					tabControl.SelectedIndexChanged -= this.OnTabSelectedIndexChanged;
					tabControl.GotFocus -= this.OnGotFocus;
					tabControl.RightToLeftLayoutChanged -= this.OnRightToLeftLayoutChanged;
					tabControl.ControlAdded -= this.OnControlAdded;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000C5D10 File Offset: 0x000C3F10
		protected override bool GetHitTest(Point point)
		{
			TabControl tabControl = (TabControl)this.Control;
			if (this.tabControlSelected)
			{
				Point pt = this.Control.PointToClient(point);
				return !tabControl.DisplayRectangle.Contains(pt);
			}
			return false;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000C5D54 File Offset: 0x000C3F54
		internal static TabPage GetTabPageOfComponent(TabControl parent, object comp)
		{
			if (!(comp is Control))
			{
				return null;
			}
			for (Control control = (Control)comp; control != null; control = control.Parent)
			{
				TabPage tabPage = control as TabPage;
				if (tabPage != null && tabPage.Parent == parent)
				{
					return tabPage;
				}
			}
			return null;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000C5D94 File Offset: 0x000C3F94
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			base.AutoResizeHandles = true;
			TabControl tabControl = component as TabControl;
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SelectionChanged += this.OnSelectionChanged;
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged += this.OnComponentChanged;
			}
			if (tabControl != null)
			{
				tabControl.SelectedIndexChanged += this.OnTabSelectedIndexChanged;
				tabControl.GotFocus += this.OnGotFocus;
				tabControl.RightToLeftLayoutChanged += this.OnRightToLeftLayoutChanged;
				tabControl.ControlAdded += this.OnControlAdded;
			}
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000C5E58 File Offset: 0x000C4058
		private void OnAdd(object sender, EventArgs eevent)
		{
			TabControl tabControl = (TabControl)base.Component;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				DesignerTransaction designerTransaction = null;
				try
				{
					try
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("TabControlAddTab", new object[]
						{
							base.Component.Site.Name
						}));
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
					MemberDescriptor member = TypeDescriptor.GetProperties(tabControl)["Controls"];
					TabPage tabPage = (TabPage)designerHost.CreateComponent(typeof(TabPage));
					if (!this.addingOnInitialize)
					{
						base.RaiseComponentChanging(member);
					}
					tabPage.Padding = new Padding(3);
					string text = null;
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(tabPage)["Name"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
					{
						text = (string)propertyDescriptor.GetValue(tabPage);
					}
					if (text != null)
					{
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(tabPage)["Text"];
						if (propertyDescriptor2 != null)
						{
							propertyDescriptor2.SetValue(tabPage, text);
						}
					}
					PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(tabPage)["UseVisualStyleBackColor"];
					if (propertyDescriptor3 != null && propertyDescriptor3.PropertyType == typeof(bool) && !propertyDescriptor3.IsReadOnly && propertyDescriptor3.IsBrowsable)
					{
						propertyDescriptor3.SetValue(tabPage, true);
					}
					tabControl.Controls.Add(tabPage);
					tabControl.SelectedIndex = tabControl.TabCount - 1;
					if (!this.addingOnInitialize)
					{
						base.RaiseComponentChanged(member, null, null);
					}
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000C6038 File Offset: 0x000C4238
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			this.CheckVerbStatus();
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000C6040 File Offset: 0x000C4240
		private void OnGotFocus(object sender, EventArgs e)
		{
			IEventHandlerService eventHandlerService = (IEventHandlerService)this.GetService(typeof(IEventHandlerService));
			if (eventHandlerService != null)
			{
				Control focusWindow = eventHandlerService.FocusWindow;
				if (focusWindow != null)
				{
					focusWindow.Focus();
				}
			}
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000C6078 File Offset: 0x000C4278
		private void OnRemove(object sender, EventArgs eevent)
		{
			TabControl tabControl = (TabControl)base.Component;
			if (tabControl == null || tabControl.TabPages.Count == 0)
			{
				return;
			}
			MemberDescriptor member = TypeDescriptor.GetProperties(base.Component)["Controls"];
			TabPage selectedTab = tabControl.SelectedTab;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				DesignerTransaction designerTransaction = null;
				try
				{
					try
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("TabControlRemoveTab", new object[]
						{
							((IComponent)selectedTab).Site.Name,
							base.Component.Site.Name
						}));
						base.RaiseComponentChanging(member);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
					designerHost.DestroyComponent(selectedTab);
					base.RaiseComponentChanged(member, null, null);
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000C6168 File Offset: 0x000C4368
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			try
			{
				this.disableDrawGrid = true;
				base.OnPaintAdornments(pe);
			}
			finally
			{
				this.disableDrawGrid = false;
			}
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000C61A0 File Offset: 0x000C43A0
		private void OnControlAdded(object sender, ControlEventArgs e)
		{
			if (e.Control != null && !e.Control.IsHandleCreated)
			{
				IntPtr handle = e.Control.Handle;
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000948BA File Offset: 0x00092ABA
		private void OnRightToLeftLayoutChanged(object sender, EventArgs e)
		{
			if (base.BehaviorService != null)
			{
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000C61D0 File Offset: 0x000C43D0
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			this.tabControlSelected = false;
			if (selectionService != null)
			{
				ICollection selectedComponents = selectionService.GetSelectedComponents();
				TabControl tabControl = (TabControl)base.Component;
				foreach (object obj in selectedComponents)
				{
					if (obj == tabControl)
					{
						this.tabControlSelected = true;
					}
					TabPage tabPageOfComponent = TabControlDesigner.GetTabPageOfComponent(tabControl, obj);
					if (tabPageOfComponent != null && tabPageOfComponent.Parent == tabControl)
					{
						this.tabControlSelected = false;
						tabControl.SelectedTab = tabPageOfComponent;
						SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
						selectionManager.Refresh();
						break;
					}
				}
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000C62A8 File Offset: 0x000C44A8
		private void OnTabSelectedIndexChanged(object sender, EventArgs e)
		{
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				ICollection selectedComponents = selectionService.GetSelectedComponents();
				TabControl tabControl = (TabControl)base.Component;
				bool flag = false;
				foreach (object comp in selectedComponents)
				{
					TabPage tabPageOfComponent = TabControlDesigner.GetTabPageOfComponent(tabControl, comp);
					if (tabPageOfComponent != null && tabPageOfComponent.Parent == tabControl && tabPageOfComponent == tabControl.SelectedTab)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						base.Component
					});
				}
			}
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000C6368 File Offset: 0x000C4568
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"SelectedIndex"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(TabControlDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000C63CC File Offset: 0x000C45CC
		private TabPageDesigner GetSelectedTabPageDesigner()
		{
			TabPageDesigner result = null;
			TabPage selectedTab = ((TabControl)base.Component).SelectedTab;
			if (selectedTab != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					result = (designerHost.GetDesigner(selectedTab) as TabPageDesigner);
				}
			}
			return result;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000C6418 File Offset: 0x000C4618
		protected override void OnDragEnter(DragEventArgs de)
		{
			this.forwardOnDrag = false;
			DropSourceBehavior.BehaviorDataObject behaviorDataObject = de.Data as DropSourceBehavior.BehaviorDataObject;
			if (behaviorDataObject != null)
			{
				int num = -1;
				ArrayList sortedDragControls = behaviorDataObject.GetSortedDragControls(ref num);
				if (sortedDragControls != null)
				{
					for (int i = 0; i < sortedDragControls.Count; i++)
					{
						if (!(sortedDragControls[i] is Control) || (sortedDragControls[i] is Control && !(sortedDragControls[i] is TabPage)))
						{
							this.forwardOnDrag = true;
							break;
						}
					}
				}
			}
			else
			{
				this.forwardOnDrag = true;
			}
			if (this.forwardOnDrag)
			{
				TabPageDesigner selectedTabPageDesigner = this.GetSelectedTabPageDesigner();
				if (selectedTabPageDesigner != null)
				{
					selectedTabPageDesigner.OnDragEnterInternal(de);
					return;
				}
			}
			else
			{
				base.OnDragEnter(de);
			}
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000C64BC File Offset: 0x000C46BC
		protected override void OnDragDrop(DragEventArgs de)
		{
			if (this.forwardOnDrag)
			{
				TabPageDesigner selectedTabPageDesigner = this.GetSelectedTabPageDesigner();
				if (selectedTabPageDesigner != null)
				{
					selectedTabPageDesigner.OnDragDropInternal(de);
				}
			}
			else
			{
				base.OnDragDrop(de);
			}
			this.forwardOnDrag = false;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000C64F4 File Offset: 0x000C46F4
		protected override void OnDragLeave(EventArgs e)
		{
			if (this.forwardOnDrag)
			{
				TabPageDesigner selectedTabPageDesigner = this.GetSelectedTabPageDesigner();
				if (selectedTabPageDesigner != null)
				{
					selectedTabPageDesigner.OnDragLeaveInternal(e);
				}
			}
			else
			{
				base.OnDragLeave(e);
			}
			this.forwardOnDrag = false;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000C652C File Offset: 0x000C472C
		protected override void OnDragOver(DragEventArgs de)
		{
			if (this.forwardOnDrag)
			{
				TabControl tabControl = (TabControl)this.Control;
				Point pt = this.Control.PointToClient(new Point(de.X, de.Y));
				if (!tabControl.DisplayRectangle.Contains(pt))
				{
					de.Effect = DragDropEffects.None;
					return;
				}
				TabPageDesigner selectedTabPageDesigner = this.GetSelectedTabPageDesigner();
				if (selectedTabPageDesigner != null)
				{
					selectedTabPageDesigner.OnDragOverInternal(de);
					return;
				}
			}
			else
			{
				base.OnDragOver(de);
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000C659C File Offset: 0x000C479C
		protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
			if (this.forwardOnDrag)
			{
				TabPageDesigner selectedTabPageDesigner = this.GetSelectedTabPageDesigner();
				if (selectedTabPageDesigner != null)
				{
					selectedTabPageDesigner.OnGiveFeedbackInternal(e);
					return;
				}
			}
			else
			{
				base.OnGiveFeedback(e);
			}
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000C65CC File Offset: 0x000C47CC
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 123)
			{
				if (msg != 132)
				{
					if (msg - 276 <= 1)
					{
						base.BehaviorService.Invalidate(base.BehaviorService.ControlRectInAdornerWindow(this.Control));
						base.WndProc(ref m);
						return;
					}
					base.WndProc(ref m);
				}
				else
				{
					base.WndProc(ref m);
					if ((int)((long)m.Result) == -1)
					{
						m.Result = (IntPtr)1;
						return;
					}
				}
				return;
			}
			int num = NativeMethods.Util.SignedLOWORD((int)((long)m.LParam));
			int num2 = NativeMethods.Util.SignedHIWORD((int)((long)m.LParam));
			if (num == -1 && num2 == -1)
			{
				Point position = Cursor.Position;
				num = position.X;
				num2 = position.Y;
			}
			this.OnContextMenu(num, num2);
		}

		// Token: 0x040018F6 RID: 6390
		private bool tabControlSelected;

		// Token: 0x040018F7 RID: 6391
		private DesignerVerbCollection verbs;

		// Token: 0x040018F8 RID: 6392
		private DesignerVerb removeVerb;

		// Token: 0x040018F9 RID: 6393
		private bool disableDrawGrid;

		// Token: 0x040018FA RID: 6394
		private int persistedSelectedIndex;

		// Token: 0x040018FB RID: 6395
		private bool addingOnInitialize;

		// Token: 0x040018FC RID: 6396
		private bool forwardOnDrag;
	}
}
