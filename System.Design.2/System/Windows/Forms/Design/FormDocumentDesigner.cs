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
	// Token: 0x020002E8 RID: 744
	internal class FormDocumentDesigner : DocumentDesigner
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x000B5325 File Offset: 0x000B3525
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x000B533C File Offset: 0x000B353C
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

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x000B5360 File Offset: 0x000B3560
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x000B5377 File Offset: 0x000B3577
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

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x000B539C File Offset: 0x000B359C
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x000B53E0 File Offset: 0x000B35E0
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

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000B53FF File Offset: 0x000B35FF
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x000B5407 File Offset: 0x000B3607
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

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000B5410 File Offset: 0x000B3610
		private bool ShouldSerializeAutoScaleBaseSize()
		{
			return !this.initializing && ((Form)base.Component).AutoScale && base.ShadowProperties.Contains("AutoScaleBaseSize");
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000B5440 File Offset: 0x000B3640
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x000B54C0 File Offset: 0x000B36C0
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

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000B550B File Offset: 0x000B370B
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x000B551D File Offset: 0x000B371D
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

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000B5550 File Offset: 0x000B3750
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

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x000B55BA File Offset: 0x000B37BA
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x000B55D4 File Offset: 0x000B37D4
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

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x000B5656 File Offset: 0x000B3856
		// (set) Token: 0x06001DE1 RID: 7649 RVA: 0x000B5670 File Offset: 0x000B3870
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

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000B5708 File Offset: 0x000B3908
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

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000B57FB File Offset: 0x000B39FB
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x000B5808 File Offset: 0x000B3A08
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

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x000B5879 File Offset: 0x000B3A79
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x000B5890 File Offset: 0x000B3A90
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

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x000B58A8 File Offset: 0x000B3AA8
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x000B58BF File Offset: 0x000B3ABF
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

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000B58D8 File Offset: 0x000B3AD8
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

		// Token: 0x06001DEA RID: 7658 RVA: 0x000B5984 File Offset: 0x000B3B84
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

		// Token: 0x06001DEB RID: 7659 RVA: 0x000B5A2C File Offset: 0x000B3C2C
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

		// Token: 0x06001DEC RID: 7660 RVA: 0x000B5B58 File Offset: 0x000B3D58
		protected override void EnsureMenuEditorService(IComponent c)
		{
			if (this.menuEditorService == null && c is Menu)
			{
				this.menuEditorService = (IMenuEditorService)this.GetService(typeof(IMenuEditorService));
			}
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x000B5B85 File Offset: 0x000B3D85
		private void EnsureToolStripWindowAdornerService()
		{
			if (this.toolStripAdornerWindowService == null)
			{
				this.toolStripAdornerWindowService = (ToolStripAdornerWindowService)this.GetService(typeof(ToolStripAdornerWindowService));
			}
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000B5BAC File Offset: 0x000B3DAC
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

		// Token: 0x06001DEF RID: 7663 RVA: 0x000B5C10 File Offset: 0x000B3E10
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

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000B5D48 File Offset: 0x000B3F48
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

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000B5DFC File Offset: 0x000B3FFC
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

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000B5EE4 File Offset: 0x000B40E4
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

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000B5F4C File Offset: 0x000B414C
		private void OnDesignerActivate(object source, EventArgs evevent)
		{
			Control control = this.Control;
			if (control != null && control.IsHandleCreated)
			{
				NativeMethods.SendMessage(control.Handle, 134, 1, 0);
				SafeNativeMethods.RedrawWindow(control.Handle, null, IntPtr.Zero, 1024);
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000B5F98 File Offset: 0x000B4198
		private void OnDesignerDeactivate(object sender, EventArgs e)
		{
			Control control = this.Control;
			if (control != null && control.IsHandleCreated)
			{
				NativeMethods.SendMessage(control.Handle, 134, 0, 0);
				SafeNativeMethods.RedrawWindow(control.Handle, null, IntPtr.Zero, 1024);
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000B5FE4 File Offset: 0x000B41E4
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

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000B6118 File Offset: 0x000B4318
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

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000B6244 File Offset: 0x000B4444
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

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000B62B8 File Offset: 0x000B44B8
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 70)
			{
				this.WmWindowPosChanging(ref m);
			}
			base.WndProc(ref m);
		}

		// Token: 0x040017AA RID: 6058
		private Size autoScaleBaseSize = Size.Empty;

		// Token: 0x040017AB RID: 6059
		private bool inAutoscale;

		// Token: 0x040017AC RID: 6060
		private int heightDelta;

		// Token: 0x040017AD RID: 6061
		private bool isMenuInherited;

		// Token: 0x040017AE RID: 6062
		private bool hasMenu;

		// Token: 0x040017AF RID: 6063
		private InheritanceAttribute inheritanceAttribute;

		// Token: 0x040017B0 RID: 6064
		private bool initializing;

		// Token: 0x040017B1 RID: 6065
		private bool autoSize;

		// Token: 0x040017B2 RID: 6066
		private ToolStripAdornerWindowService toolStripAdornerWindowService;
	}
}
