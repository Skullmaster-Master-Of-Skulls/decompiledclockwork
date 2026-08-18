using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000295 RID: 661
	internal class AxHostDesigner : ControlDesigner
	{
		// Token: 0x06001919 RID: 6425 RVA: 0x0008C292 File Offset: 0x0008A492
		public AxHostDesigner()
		{
			this.handler = new EventHandler(this.OnVerb);
			base.AutoResizeHandles = true;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0000445B File Offset: 0x0000265B
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x00003937 File Offset: 0x00001B37
		private int SelectionStyle
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0008C2C0 File Offset: 0x0008A4C0
		public override DesignerVerbCollection Verbs
		{
			get
			{
				DesignerVerbCollection designerVerbCollection = new DesignerVerbCollection();
				this.GetOleVerbs(designerVerbCollection);
				if (!this.foundAbout && this.axHost.HasAboutBox)
				{
					designerVerbCollection.Add(new AxHostDesigner.HostVerb(AxHostDesigner.AboutVerbData, this.handler));
				}
				return designerVerbCollection;
			}
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0008C308 File Offset: 0x0008A508
		private static Size GetDefaultSize(IComponent component)
		{
			Size size = Size.Empty;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["AutoSize"];
			if (propertyDescriptor != null && !propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden) && !propertyDescriptor.Attributes.Contains(BrowsableAttribute.No))
			{
				bool flag = (bool)propertyDescriptor.GetValue(component);
				if (flag)
				{
					propertyDescriptor = TypeDescriptor.GetProperties(component)["PreferredSize"];
					if (propertyDescriptor != null)
					{
						size = (Size)propertyDescriptor.GetValue(component);
						if (size != Size.Empty)
						{
							return size;
						}
					}
				}
			}
			propertyDescriptor = TypeDescriptor.GetProperties(component)["Size"];
			if (propertyDescriptor != null)
			{
				size = (Size)propertyDescriptor.GetValue(component);
				if (size.Width > 0 && size.Height > 0)
				{
					return size;
				}
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)propertyDescriptor.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute != null)
				{
					return (Size)defaultValueAttribute.Value;
				}
			}
			return new Size(75, 23);
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0008C400 File Offset: 0x0008A600
		public virtual void GetOleVerbs(DesignerVerbCollection rval)
		{
			NativeMethods.IEnumOLEVERB enumOLEVERB = null;
			NativeMethods.IOleObject oleObject = this.axHost.GetOcx() as NativeMethods.IOleObject;
			if (oleObject == null || NativeMethods.Failed(oleObject.EnumVerbs(out enumOLEVERB)))
			{
				return;
			}
			if (enumOLEVERB == null)
			{
				return;
			}
			int[] array = new int[1];
			NativeMethods.tagOLEVERB tagOLEVERB = new NativeMethods.tagOLEVERB();
			this.foundEdit = false;
			this.foundAbout = false;
			this.foundProperties = false;
			for (;;)
			{
				array[0] = 0;
				tagOLEVERB.lpszVerbName = null;
				int num = enumOLEVERB.Next(1, tagOLEVERB, array);
				if (num == 1 || NativeMethods.Failed(num))
				{
					break;
				}
				if ((tagOLEVERB.grfAttribs & 2) != 0)
				{
					this.foundEdit = (this.foundEdit || tagOLEVERB.lVerb == -4);
					this.foundAbout = (this.foundAbout || tagOLEVERB.lVerb == 2);
					this.foundProperties = (this.foundProperties || tagOLEVERB.lVerb == 1);
					rval.Add(new AxHostDesigner.HostVerb(new AxHostDesigner.OleVerbData(tagOLEVERB), this.handler));
				}
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0008C4F3 File Offset: 0x0008A6F3
		protected override bool GetHitTest(Point p)
		{
			return this.axHost.EditMode;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0008C500 File Offset: 0x0008A700
		protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selType)
		{
			Cursor cursor = Cursors.Default;
			if (selType != GlyphSelectionType.NotSelected && (this.SelectionRules & SelectionRules.Moveable) != SelectionRules.None)
			{
				cursor = Cursors.SizeAll;
			}
			Point location = base.BehaviorService.ControlToAdornerWindow((Control)base.Component);
			Rectangle bounds = new Rectangle(location, ((Control)base.Component).Size);
			return new ControlBodyGlyph(bounds, cursor, this.Control, this);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0008C569 File Offset: 0x0008A769
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.axHost = (AxHost)component;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0008C57E File Offset: 0x0008A77E
		private void OnControlAdded(object sender, ControlEventArgs e)
		{
			if (e.Control == this.axHost)
			{
				this.defaultSize = AxHostDesigner.GetDefaultSize(this.axHost);
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0008C59F File Offset: 0x0008A79F
		protected override void OnCreateHandle()
		{
			base.OnCreateHandle();
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0008C5A8 File Offset: 0x0008A7A8
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			try
			{
				Control control = defaultValues["Parent"] as Control;
				if (control != null)
				{
					control.ControlAdded += this.OnControlAdded;
				}
				base.InitializeNewComponent(defaultValues);
				if (control != null)
				{
					control.ControlAdded -= this.OnControlAdded;
				}
				if ((defaultValues == null || !defaultValues.Contains("Size")) && this.defaultSize != Size.Empty)
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.axHost);
					if (properties != null)
					{
						PropertyDescriptor propertyDescriptor = properties["Size"];
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(this.axHost, new Size(this.defaultSize.Width, this.defaultSize.Height));
						}
					}
				}
			}
			catch (NotSupportedException)
			{
				throw;
			}
			catch (InvalidOperationException ex)
			{
				throw new NotSupportedException(ex.Message, ex);
			}
			catch
			{
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0008C6AC File Offset: 0x0008A8AC
		public virtual void OnVerb(object sender, EventArgs evevent)
		{
			if (sender != null && sender is AxHostDesigner.HostVerb)
			{
				AxHostDesigner.HostVerb hostVerb = (AxHostDesigner.HostVerb)sender;
				hostVerb.Invoke(this.axHost);
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0008C6D8 File Offset: 0x0008A8D8
		protected override void PreFilterProperties(IDictionary properties)
		{
			object obj = properties["Enabled"];
			base.PreFilterProperties(properties);
			if (obj != null)
			{
				properties["Enabled"] = obj;
			}
			properties["SelectionStyle"] = TypeDescriptor.CreateProperty(typeof(AxHostDesigner), "SelectionStyle", typeof(int), new Attribute[]
			{
				BrowsableAttribute.No,
				DesignerSerializationVisibilityAttribute.Hidden,
				DesignOnlyAttribute.Yes
			});
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0008C750 File Offset: 0x0008A950
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 132)
			{
				if (msg == 528)
				{
					if ((int)((long)m.WParam) == 1)
					{
						base.HookChildHandles(m.LParam);
					}
					base.WndProc(ref m);
					return;
				}
				base.WndProc(ref m);
			}
			else
			{
				if (!this.dragdropRevoked)
				{
					int num = NativeMethods.RevokeDragDrop(this.Control.Handle);
					this.dragdropRevoked = (num == 0);
				}
				base.WndProc(ref m);
				if ((int)((long)m.Result) == -1 || (int)((long)m.Result) > 1)
				{
					m.Result = (IntPtr)1;
					return;
				}
			}
		}

		// Token: 0x0400155C RID: 5468
		private AxHost axHost;

		// Token: 0x0400155D RID: 5469
		private EventHandler handler;

		// Token: 0x0400155E RID: 5470
		private bool foundEdit;

		// Token: 0x0400155F RID: 5471
		private bool foundAbout;

		// Token: 0x04001560 RID: 5472
		private bool foundProperties;

		// Token: 0x04001561 RID: 5473
		private bool dragdropRevoked;

		// Token: 0x04001562 RID: 5474
		private Size defaultSize = Size.Empty;

		// Token: 0x04001563 RID: 5475
		private const int OLEIVERB_UIACTIVATE = -4;

		// Token: 0x04001564 RID: 5476
		private const int HOSTVERB_ABOUT = 2;

		// Token: 0x04001565 RID: 5477
		private const int HOSTVERB_PROPERTIES = 1;

		// Token: 0x04001566 RID: 5478
		private const int HOSTVERB_EDIT = 3;

		// Token: 0x04001567 RID: 5479
		private static readonly AxHostDesigner.HostVerbData EditVerbData = new AxHostDesigner.HostVerbData(SR.GetString("AXEdit"), 3);

		// Token: 0x04001568 RID: 5480
		private static readonly AxHostDesigner.HostVerbData PropertiesVerbData = new AxHostDesigner.HostVerbData(SR.GetString("AXProperties"), 1);

		// Token: 0x04001569 RID: 5481
		private static readonly AxHostDesigner.HostVerbData AboutVerbData = new AxHostDesigner.HostVerbData(SR.GetString("AXAbout"), 2);

		// Token: 0x0400156A RID: 5482
		private static TraceSwitch AxHostDesignerSwitch = new TraceSwitch("AxHostDesigner", "ActiveX Designer Trace");

		// Token: 0x0200051F RID: 1311
		private class HostVerb : DesignerVerb
		{
			// Token: 0x0600300B RID: 12299 RVA: 0x00107E3C File Offset: 0x0010603C
			public HostVerb(AxHostDesigner.HostVerbData data, EventHandler handler) : base(data.ToString(), handler)
			{
				this.data = data;
			}

			// Token: 0x0600300C RID: 12300 RVA: 0x00107E52 File Offset: 0x00106052
			public void Invoke(AxHost host)
			{
				this.data.Execute(host);
			}

			// Token: 0x0400209C RID: 8348
			private AxHostDesigner.HostVerbData data;
		}

		// Token: 0x02000520 RID: 1312
		private class HostVerbData
		{
			// Token: 0x0600300D RID: 12301 RVA: 0x00107E60 File Offset: 0x00106060
			internal HostVerbData(string name, int id)
			{
				this.name = name;
				this.id = id;
			}

			// Token: 0x0600300E RID: 12302 RVA: 0x00107E76 File Offset: 0x00106076
			public override string ToString()
			{
				return this.name;
			}

			// Token: 0x0600300F RID: 12303 RVA: 0x00107E80 File Offset: 0x00106080
			internal virtual void Execute(AxHost ctl)
			{
				switch (this.id)
				{
				case 1:
					ctl.ShowPropertyPages();
					return;
				case 2:
					ctl.ShowAboutBox();
					return;
				case 3:
					ctl.InvokeEditMode();
					return;
				default:
					return;
				}
			}

			// Token: 0x0400209D RID: 8349
			internal readonly string name;

			// Token: 0x0400209E RID: 8350
			internal readonly int id;
		}

		// Token: 0x02000521 RID: 1313
		private class OleVerbData : AxHostDesigner.HostVerbData
		{
			// Token: 0x06003010 RID: 12304 RVA: 0x00107EBD File Offset: 0x001060BD
			internal OleVerbData(NativeMethods.tagOLEVERB oleVerb) : base(SR.GetString("AXVerbPrefix") + oleVerb.lpszVerbName, oleVerb.lVerb)
			{
				this.dirties = ((oleVerb.grfAttribs & 1) == 0);
			}

			// Token: 0x06003011 RID: 12305 RVA: 0x00107EF1 File Offset: 0x001060F1
			internal override void Execute(AxHost ctl)
			{
				if (this.dirties)
				{
					ctl.MakeDirty();
				}
				ctl.DoVerb(this.id);
			}

			// Token: 0x0400209F RID: 8351
			private readonly bool dirties;
		}

		// Token: 0x02000522 RID: 1314
		internal class AxHostDesignerBehavior : Behavior
		{
			// Token: 0x06003012 RID: 12306 RVA: 0x00107F0D File Offset: 0x0010610D
			internal AxHostDesignerBehavior(AxHostDesigner designer)
			{
				this.designer = designer;
			}

			// Token: 0x06003013 RID: 12307 RVA: 0x00107F1C File Offset: 0x0010611C
			internal bool IsTransparent(Point p)
			{
				return this.designer.GetHitTest(p);
			}

			// Token: 0x06003014 RID: 12308 RVA: 0x00107F2C File Offset: 0x0010612C
			private Point AdornerToControl(Point ptAdorner)
			{
				if (this.bs == null)
				{
					this.bs = (BehaviorService)this.designer.GetService(typeof(BehaviorService));
				}
				if (this.bs != null)
				{
					Point point = this.bs.AdornerWindowToScreen();
					point.X += ptAdorner.X;
					point.Y += ptAdorner.Y;
					point = this.designer.Control.PointToClient(point);
					return point;
				}
				return ptAdorner;
			}

			// Token: 0x06003015 RID: 12309 RVA: 0x00107FB4 File Offset: 0x001061B4
			public override void OnDragDrop(Glyph g, DragEventArgs e)
			{
				this.designer.OnDragDrop(e);
			}

			// Token: 0x06003016 RID: 12310 RVA: 0x00107FC2 File Offset: 0x001061C2
			public override void OnDragEnter(Glyph g, DragEventArgs e)
			{
				this.designer.OnDragEnter(e);
			}

			// Token: 0x06003017 RID: 12311 RVA: 0x00107FD0 File Offset: 0x001061D0
			public override void OnDragLeave(Glyph g, EventArgs e)
			{
				this.designer.OnDragLeave(e);
			}

			// Token: 0x06003018 RID: 12312 RVA: 0x00107FDE File Offset: 0x001061DE
			public override void OnDragOver(Glyph g, DragEventArgs e)
			{
				this.designer.OnDragOver(e);
			}

			// Token: 0x06003019 RID: 12313 RVA: 0x00107FEC File Offset: 0x001061EC
			public override void OnGiveFeedback(Glyph g, GiveFeedbackEventArgs e)
			{
				this.designer.OnGiveFeedback(e);
			}

			// Token: 0x0600301A RID: 12314 RVA: 0x00107FFC File Offset: 0x001061FC
			public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
			{
				int num = 0;
				if (button == MouseButtons.Left)
				{
					num = 513;
				}
				else if (button == MouseButtons.Right)
				{
					num = 516;
				}
				if (num != 0)
				{
					Point point = this.AdornerToControl(mouseLoc);
					Message message = default(Message);
					message.HWnd = this.designer.Control.Handle;
					message.Msg = num;
					message.WParam = IntPtr.Zero;
					message.LParam = (IntPtr)(point.Y << 16 | point.X);
					this.designer.WndProc(ref message);
					return true;
				}
				return false;
			}

			// Token: 0x0600301B RID: 12315 RVA: 0x00108098 File Offset: 0x00106298
			public override bool OnMouseUp(Glyph g, MouseButtons button)
			{
				int num = 0;
				if (button == MouseButtons.Left)
				{
					num = 514;
				}
				else if (button == MouseButtons.Right)
				{
					num = 517;
				}
				if (num != 0)
				{
					Point point = this.designer.Control.PointToClient(Control.MousePosition);
					Message message = default(Message);
					message.HWnd = this.designer.Control.Handle;
					message.Msg = num;
					message.WParam = IntPtr.Zero;
					message.LParam = (IntPtr)(point.Y << 16 | point.X);
					this.designer.WndProc(ref message);
					return true;
				}
				return false;
			}

			// Token: 0x040020A0 RID: 8352
			private AxHostDesigner designer;

			// Token: 0x040020A1 RID: 8353
			private BehaviorService bs;
		}
	}
}
