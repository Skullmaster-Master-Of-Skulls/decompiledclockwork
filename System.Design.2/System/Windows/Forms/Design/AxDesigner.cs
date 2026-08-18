using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Diagnostics;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000294 RID: 660
	internal class AxDesigner : ControlDesigner
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0000445B File Offset: 0x0000265B
		// (set) Token: 0x06001912 RID: 6418 RVA: 0x00003937 File Offset: 0x00001B37
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

		// Token: 0x06001913 RID: 6419 RVA: 0x0008C0EF File Offset: 0x0008A2EF
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			base.AutoResizeHandles = true;
			this.webBrowserBase = (WebBrowserBase)component;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0008C10C File Offset: 0x0008A30C
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			try
			{
				base.InitializeNewComponent(defaultValues);
			}
			catch
			{
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0008C138 File Offset: 0x0008A338
		protected override void PreFilterProperties(IDictionary properties)
		{
			object obj = properties["Enabled"];
			base.PreFilterProperties(properties);
			if (obj != null)
			{
				properties["Enabled"] = obj;
			}
			properties["SelectionStyle"] = TypeDescriptor.CreateProperty(typeof(AxDesigner), "SelectionStyle", typeof(int), new Attribute[]
			{
				BrowsableAttribute.No,
				DesignerSerializationVisibilityAttribute.Hidden,
				DesignOnlyAttribute.Yes
			});
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0008C1B0 File Offset: 0x0008A3B0
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
					IntPtr intPtr = this.Control.Handle;
					this.dragdropRevoked = true;
					while (intPtr != IntPtr.Zero && this.dragdropRevoked)
					{
						NativeMethods.RevokeDragDrop(intPtr);
						intPtr = NativeMethods.GetWindow(intPtr, 5);
					}
				}
				base.WndProc(ref m);
				if ((int)((long)m.Result) == -1 || (int)((long)m.Result) > 1)
				{
					m.Result = (IntPtr)1;
					return;
				}
			}
		}

		// Token: 0x04001559 RID: 5465
		private WebBrowserBase webBrowserBase;

		// Token: 0x0400155A RID: 5466
		private bool dragdropRevoked;

		// Token: 0x0400155B RID: 5467
		private static TraceSwitch AxDesignerSwitch = new TraceSwitch("AxDesigner", "ActiveX Designer Trace");
	}
}
