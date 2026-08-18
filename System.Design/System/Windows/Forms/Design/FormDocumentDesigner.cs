using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000247 RID: 583
	internal class FormDocumentDesigner : DocumentDesigner
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0007361C File Offset: 0x0007261C
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x00073633 File Offset: 0x00072633
		private IButtonControl AcceptButton
		{
			get
			{
				return base.ShadowProperties["AcceptButton"] as IButtonControl;
			}
			set
			{
				((Form)base.Component).AcceptButton = value;
				base.ShadowProperties["AcceptButton"] = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00073657 File Offset: 0x00072657
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x0007366E File Offset: 0x0007266E
		private IButtonControl CancelButton
		{
			get
			{
				return base.ShadowProperties["CancelButton"] as IButtonControl;
			}
			set
			{
				((Form)base.Component).CancelButton = value;
				base.ShadowProperties["CancelButton"] = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x00073694 File Offset: 0x00072694
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x000736D8 File Offset: 0x000726D8
		private Size AutoScaleBaseSize
		{
			get
			{
				SizeF autoScaleSize = Form.GetAutoScaleSize(((Form)base.Component).Font);
				return new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
			}
			set
			{
				this.autoScaleBaseSize = value;
				base.ShadowProperties["AutoScaleBaseSize"] = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x000736F7 File Offset: 0x000726F7
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x000736FF File Offset: 0x000726FF
		private bool AutoSize
		{
			get
			{
				return this.autoSize;
			}
			set
			{
				this.autoSize = value;
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00073708 File Offset: 0x00072708
		private bool ShouldSerializeAutoScaleBaseSize()
		{
			return !this.initializing && ((Form)base.Component).AutoScale && base.ShadowProperties.Contains("AutoScaleBaseSize");
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00073738 File Offset: 0x00072738
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x000737B8 File Offset: 0x000727B8
		private Size ClientSize
		{
			get
			{
				if (this.initializing)
				{
					return new Size(-1, -1);
				}
				Size clientSize = new Size(-1, -1);
				Form form = base.Component as Form;
				if (form != null)
				{
					clientSize = form.ClientSize;
					if (form.HorizontalScroll.Visible)
					{
						clientSize.Height += SystemInformation.HorizontalScrollBarHeight;
					}
					if (form.VerticalScroll.Visible)
					{
						clientSize.Width += SystemInformation.VerticalScrollBarWidth;
					}
				}
				return clientSize;
			}
			set
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null && designerHost.Loading)
				{
					this.heightDelta = this.GetMenuHeight();
				}
				((Form)base.Component).ClientSize = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00073803 File Offset: 0x00072803
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x00073815 File Offset: 0x00072815
		private bool IsMdiContainer
		{
			get
			{
				return ((Form)this.Control).IsMdiContainer;
			}
			set
			{
				if (!value)
				{
					base.UnhookChildControls(this.Control);
				}
				((Form)this.Control).IsMdiContainer = value;
				if (value)
				{
					base.HookChildControls(this.Control);
				}
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00073848 File Offset: 0x00072848
		private bool IsMenuInherited
		{
			get
			{
				if (this.inheritanceAttribute == null && this.Menu != null)
				{
					this.inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this.Menu)[typeof(InheritanceAttribute)];
					if (this.inheritanceAttribute.Equals(InheritanceAttribute.NotInherited))
					{
						this.isMenuInherited = false;
					}
					else
					{
						this.isMenuInherited = true;
					}
				}
				return this.isMenuInherited;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x000738B2 File Offset: 0x000728B2
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x000738CC File Offset: 0x000728CC
		internal MainMenu Menu
		{
			get
			{
				return (MainMenu)base.ShadowProperties["Menu"];
			}
			set
			{
				if (value == base.ShadowProperties["Menu"])
				{
					return;
				}
				base.ShadowProperties["Menu"] = value;
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null && !designerHost.Loading)
				{
					this.EnsureMenuEditorService(value);
					if (this.menuEditorService != null)
					{
						this.menuEditorService.SetMenu(value);
					}
				}
				if (this.heightDelta == 0)
				{
					this.heightDelta = this.GetMenuHeight();
				}
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0007394E File Offset: 0x0007294E
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x00073968 File Offset: 0x00072968
		private double Opacity
		{
			get
			{
				return (double)base.ShadowProperties["Opacity"];
			}
			set
			{
				if (value < 0.0 || value > 1.0)
				{
					throw new ArgumentException(SR.GetString("InvalidBoundArgument", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture),
						0f.ToString(CultureInfo.CurrentCulture),
						1f.ToString(CultureInfo.CurrentCulture)
					}), "value");
				}
				base.ShadowProperties["Opacity"] = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00073A04 File Offset: 0x00072A04
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = null;
				base.AddPaddingSnapLines(ref arrayList);
				if (arrayList == null)
				{
					arrayList = new ArrayList(4);
				}
				if (this.Control.Padding == Padding.Empty && arrayList != null)
				{
					int num = 0;
					for (int i = 0; i < arrayList.Count; i++)
					{
						SnapLine snapLine = arrayList[i] as SnapLine;
						if (snapLine != null && snapLine.Filter != null && snapLine.Filter.StartsWith("Padding"))
						{
							if (snapLine.Filter.Equals("Padding.Left") || snapLine.Filter.Equals("Padding.Top"))
							{
								snapLine.AdjustOffset(DesignerUtils.DEFAULTFORMPADDING);
								num++;
							}
							if (snapLine.Filter.Equals("Padding.Right") || snapLine.Filter.Equals("Padding.Bottom"))
							{
								snapLine.AdjustOffset(-DesignerUtils.DEFAULTFORMPADDING);
								num++;
							}
							if (num == 4)
							{
								break;
							}
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00073AF7 File Offset: 0x00072AF7
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x00073B04 File Offset: 0x00072B04
		private Size Size
		{
			get
			{
				return this.Control.Size;
			}
			set
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.Component);
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanging(base.Component, properties["ClientSize"]);
				}
				this.Control.Size = value;
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(base.Component, properties["ClientSize"], null, null);
				}
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00073B75 File Offset: 0x00072B75
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x00073B8C File Offset: 0x00072B8C
		private bool ShowInTaskbar
		{
			get
			{
				return (bool)base.ShadowProperties["ShowInTaskbar"];
			}
			set
			{
				base.ShadowProperties["ShowInTaskbar"] = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00073BA4 File Offset: 0x00072BA4
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x00073BBB File Offset: 0x00072BBB
		private FormWindowState WindowState
		{
			get
			{
				return (FormWindowState)base.ShadowProperties["WindowState"];
			}
			set
			{
				base.ShadowProperties["WindowState"] = value;
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00073BD4 File Offset: 0x00072BD4
		private void ApplyAutoScaling(SizeF baseVar, Form form)
		{
			if (!baseVar.IsEmpty)
			{
				SizeF autoScaleSize = Form.GetAutoScaleSize(form.Font);
				Size size = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
				if (baseVar.Equals(size))
				{
					return;
				}
				float dy = (float)size.Height / baseVar.Height;
				float dx = (float)size.Width / baseVar.Width;
				try
				{
					this.inAutoscale = true;
					form.Scale(dx, dy);
				}
				finally
				{
					this.inAutoscale = false;
				}
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00073C80 File Offset: 0x00072C80
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					designerHost.LoadComplete -= this.OnLoadComplete;
					designerHost.Activated -= this.OnDesignerActivate;
					designerHost.Deactivated -= this.OnDesignerDeactivate;
				}
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded -= this.OnComponentAdded;
					componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00073D28 File Offset: 0x00072D28
		internal override void DoProperMenuSelection(ICollection selComponents)
		{
			foreach (object obj in selComponents)
			{
				Menu menu = obj as Menu;
				if (menu != null)
				{
					MenuItem menuItem = menu as MenuItem;
					if (menuItem != null)
					{
						Menu menu2 = this.menuEditorService.GetMenu();
						MenuItem menuItem2 = menuItem;
						while (menuItem2.Parent is MenuItem)
						{
							menuItem2 = (MenuItem)menuItem2.Parent;
						}
						if (menu2 != menuItem2.Parent)
						{
							this.menuEditorService.SetMenu(menuItem2.Parent);
						}
						if (selComponents.Count == 1)
						{
							this.menuEditorService.SetSelection(menuItem);
						}
					}
					else
					{
						this.menuEditorService.SetMenu(menu);
					}
					break;
				}
				if (this.Menu != null && this.Menu.MenuItems.Count == 0)
				{
					this.menuEditorService.SetMenu(null);
				}
				else
				{
					this.menuEditorService.SetMenu(this.Menu);
				}
				NativeMethods.SendMessage(this.Control.Handle, 134, 1, 0);
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00073E50 File Offset: 0x00072E50
		protected override void EnsureMenuEditorService(IComponent c)
		{
			if (this.menuEditorService == null && c is Menu)
			{
				this.menuEditorService = (IMenuEditorService)this.GetService(typeof(IMenuEditorService));
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00073E7D File Offset: 0x00072E7D
		private void EnsureToolStripWindowAdornerService()
		{
			if (this.toolStripAdornerWindowService == null)
			{
				this.toolStripAdornerWindowService = (ToolStripAdornerWindowService)this.GetService(typeof(ToolStripAdornerWindowService));
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00073EA4 File Offset: 0x00072EA4
		private int GetMenuHeight()
		{
			if (this.Menu == null || (this.IsMenuInherited && this.initializing))
			{
				return 0;
			}
			if (this.menuEditorService != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.menuEditorService)["MenuHeight"];
				if (propertyDescriptor != null)
				{
					return (int)propertyDescriptor.GetValue(this.menuEditorService);
				}
			}
			return SystemInformation.MenuHeight;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00073F08 File Offset: 0x00072F08
		public override void Initialize(IComponent component)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component.GetType())["WindowState"];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(FormWindowState))
			{
				this.WindowState = (FormWindowState)propertyDescriptor.GetValue(component);
			}
			this.initializing = true;
			base.Initialize(component);
			this.initializing = false;
			base.AutoResizeHandles = true;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				designerHost.LoadComplete += this.OnLoadComplete;
				designerHost.Activated += this.OnDesignerActivate;
				designerHost.Deactivated += this.OnDesignerDeactivate;
			}
			Form form = (Form)this.Control;
			form.WindowState = FormWindowState.Normal;
			base.ShadowProperties["AcceptButton"] = form.AcceptButton;
			base.ShadowProperties["CancelButton"] = form.CancelButton;
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded += this.OnComponentAdded;
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
			}
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0007403C File Offset: 0x0007303C
		private void OnComponentAdded(object source, ComponentEventArgs ce)
		{
			if (ce.Component is Menu)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null && !designerHost.Loading && ce.Component is MainMenu && !this.hasMenu)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Menu"];
					propertyDescriptor.SetValue(base.Component, ce.Component);
					this.hasMenu = true;
				}
			}
			if (ce.Component is ToolStrip && this.toolStripAdornerWindowService == null)
			{
				IDesignerHost designerHost2 = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost2 != null)
				{
					this.EnsureToolStripWindowAdornerService();
				}
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000740F0 File Offset: 0x000730F0
		private void OnComponentRemoved(object source, ComponentEventArgs ce)
		{
			if (ce.Component is Menu)
			{
				if (ce.Component == this.Menu)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Menu"];
					propertyDescriptor.SetValue(base.Component, null);
					this.hasMenu = false;
				}
				else if (this.menuEditorService != null && ce.Component == this.menuEditorService.GetMenu())
				{
					this.menuEditorService.SetMenu(this.Menu);
				}
			}
			if (ce.Component is ToolStrip && this.toolStripAdornerWindowService != null)
			{
				this.toolStripAdornerWindowService = null;
			}
			if (ce.Component is IButtonControl)
			{
				if (ce.Component == base.ShadowProperties["AcceptButton"])
				{
					this.AcceptButton = null;
				}
				if (ce.Component == base.ShadowProperties["CancelButton"])
				{
					this.CancelButton = null;
				}
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000741D8 File Offset: 0x000731D8
		protected override void OnCreateHandle()
		{
			if (this.Menu != null && this.menuEditorService != null)
			{
				this.menuEditorService.SetMenu(null);
				this.menuEditorService.SetMenu(this.Menu);
			}
			if (this.heightDelta != 0)
			{
				((Form)base.Component).Height += this.heightDelta;
				this.heightDelta = 0;
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00074240 File Offset: 0x00073240
		private void OnDesignerActivate(object source, EventArgs evevent)
		{
			Control control = this.Control;
			if (control != null && control.IsHandleCreated)
			{
				NativeMethods.SendMessage(control.Handle, 134, 1, 0);
				SafeNativeMethods.RedrawWindow(control.Handle, null, IntPtr.Zero, 1024);
			}
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0007428C File Offset: 0x0007328C
		private void OnDesignerDeactivate(object sender, EventArgs e)
		{
			Control control = this.Control;
			if (control != null && control.IsHandleCreated)
			{
				NativeMethods.SendMessage(control.Handle, 134, 0, 0);
				SafeNativeMethods.RedrawWindow(control.Handle, null, IntPtr.Zero, 1024);
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000742D8 File Offset: 0x000732D8
		private void OnLoadComplete(object source, EventArgs evevent)
		{
			Form form = this.Control as Form;
			if (form != null)
			{
				int num = form.ClientSize.Width;
				int num2 = form.ClientSize.Height;
				if (form.HorizontalScroll.Visible && form.AutoScroll)
				{
					num2 += SystemInformation.HorizontalScrollBarHeight;
				}
				if (form.VerticalScroll.Visible && form.AutoScroll)
				{
					num += SystemInformation.VerticalScrollBarWidth;
				}
				this.ApplyAutoScaling(this.autoScaleBaseSize, form);
				this.ClientSize = new Size(num, num2);
				BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
				if (behaviorService != null)
				{
					behaviorService.SyncSelection();
				}
				if (this.heightDelta == 0)
				{
					this.heightDelta = this.GetMenuHeight();
				}
				if (this.heightDelta != 0)
				{
					form.Height += this.heightDelta;
					this.heightDelta = 0;
				}
				if (!form.ControlBox && !form.ShowInTaskbar && !string.IsNullOrEmpty(form.Text) && this.Menu != null && !this.IsMenuInherited)
				{
					form.Height += SystemInformation.CaptionHeight + 1;
				}
				form.PerformLayout();
			}
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0007440C File Offset: 0x0007340C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"Opacity",
				"Menu",
				"IsMdiContainer",
				"Size",
				"ShowInTaskBar",
				"WindowState",
				"AutoSize",
				"AcceptButton",
				"CancelButton"
			};
			Attribute[] attributes = new Attribute[0];
			PropertyDescriptor propertyDescriptor;
			for (int i = 0; i < array.Length; i++)
			{
				propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(FormDocumentDesigner), propertyDescriptor, attributes);
				}
			}
			propertyDescriptor = (PropertyDescriptor)properties["AutoScaleBaseSize"];
			if (propertyDescriptor != null)
			{
				properties["AutoScaleBaseSize"] = TypeDescriptor.CreateProperty(typeof(FormDocumentDesigner), propertyDescriptor, new Attribute[]
				{
					DesignerSerializationVisibilityAttribute.Visible
				});
			}
			propertyDescriptor = (PropertyDescriptor)properties["ClientSize"];
			if (propertyDescriptor != null)
			{
				properties["ClientSize"] = TypeDescriptor.CreateProperty(typeof(FormDocumentDesigner), propertyDescriptor, new Attribute[]
				{
					new DefaultValueAttribute(new Size(-1, -1))
				});
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00074550 File Offset: 0x00073550
		private unsafe void WmWindowPosChanging(ref Message m)
		{
			NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)((void*)m.LParam);
			bool loading = this.inAutoscale;
			if (!loading)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					loading = designerHost.Loading;
				}
			}
			if (loading && this.Menu != null && (ptr->flags & 1) == 0 && (this.IsMenuInherited || this.inAutoscale))
			{
				this.heightDelta = this.GetMenuHeight();
			}
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000745C4 File Offset: 0x000735C4
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 70)
			{
				this.WmWindowPosChanging(ref m);
			}
			base.WndProc(ref m);
		}

		// Token: 0x040012DF RID: 4831
		private Size autoScaleBaseSize = Size.Empty;

		// Token: 0x040012E0 RID: 4832
		private bool inAutoscale;

		// Token: 0x040012E1 RID: 4833
		private int heightDelta;

		// Token: 0x040012E2 RID: 4834
		private bool isMenuInherited;

		// Token: 0x040012E3 RID: 4835
		private bool hasMenu;

		// Token: 0x040012E4 RID: 4836
		private InheritanceAttribute inheritanceAttribute;

		// Token: 0x040012E5 RID: 4837
		private bool initializing;

		// Token: 0x040012E6 RID: 4838
		private bool autoSize;

		// Token: 0x040012E7 RID: 4839
		private ToolStripAdornerWindowService toolStripAdornerWindowService;
	}
}
