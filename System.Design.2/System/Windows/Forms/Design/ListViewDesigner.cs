using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200030A RID: 778
	internal class ListViewDesigner : ControlDesigner
	{
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x000B83EC File Offset: 0x000B65EC
		public override ICollection AssociatedComponents
		{
			get
			{
				ListView listView = this.Control as ListView;
				if (listView != null)
				{
					return listView.Columns;
				}
				return base.AssociatedComponents;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x000B8415 File Offset: 0x000B6615
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x000B842C File Offset: 0x000B662C
		private bool OwnerDraw
		{
			get
			{
				return (bool)base.ShadowProperties["OwnerDraw"];
			}
			set
			{
				base.ShadowProperties["OwnerDraw"] = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000B8444 File Offset: 0x000B6644
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x000B8456 File Offset: 0x000B6656
		private View View
		{
			get
			{
				return ((ListView)base.Component).View;
			}
			set
			{
				((ListView)base.Component).View = value;
				if (value == View.Details)
				{
					base.HookChildHandles(this.Control.Handle);
				}
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000B8480 File Offset: 0x000B6680
		protected override bool GetHitTest(Point point)
		{
			ListView listView = (ListView)base.Component;
			if (listView.View == View.Details)
			{
				Point point2 = this.Control.PointToClient(point);
				IntPtr handle = listView.Handle;
				IntPtr value = NativeMethods.ChildWindowFromPointEx(handle, point2.X, point2.Y, 1);
				if (value != IntPtr.Zero && value != handle)
				{
					IntPtr intPtr = NativeMethods.SendMessage(handle, 4127, IntPtr.Zero, IntPtr.Zero);
					if (value == intPtr)
					{
						NativeMethods.POINT point3 = new NativeMethods.POINT();
						point3.x = point.X;
						point3.y = point.Y;
						NativeMethods.MapWindowPoints(IntPtr.Zero, intPtr, point3, 1);
						this.hdrhit.pt_x = point3.x;
						this.hdrhit.pt_y = point3.y;
						NativeMethods.SendMessage(intPtr, 4614, IntPtr.Zero, this.hdrhit);
						if (this.hdrhit.flags == 4)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000B858C File Offset: 0x000B678C
		public override void Initialize(IComponent component)
		{
			ListView listView = (ListView)component;
			this.OwnerDraw = listView.OwnerDraw;
			listView.OwnerDraw = false;
			listView.UseCompatibleStateImageBehavior = false;
			base.AutoResizeHandles = true;
			base.Initialize(component);
			if (listView.View == View.Details)
			{
				base.HookChildHandles(this.Control.Handle);
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000B85E4 File Offset: 0x000B67E4
		protected override void PreFilterProperties(IDictionary properties)
		{
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["OwnerDraw"];
			if (propertyDescriptor != null)
			{
				properties["OwnerDraw"] = TypeDescriptor.CreateProperty(typeof(ListViewDesigner), propertyDescriptor, new Attribute[0]);
			}
			PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)properties["View"];
			if (propertyDescriptor2 != null)
			{
				properties["View"] = TypeDescriptor.CreateProperty(typeof(ListViewDesigner), propertyDescriptor2, new Attribute[0]);
			}
			base.PreFilterProperties(properties);
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x000B8664 File Offset: 0x000B6864
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 78 || msg == 8270)
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMHDR));
				if (nmhdr.code == NativeMethods.HDN_ENDTRACK)
				{
					try
					{
						IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
						componentChangeService.OnComponentChanged(base.Component, null, null, null);
					}
					catch (InvalidOperationException ex)
					{
						if (this.inShowErrorDialog)
						{
							return;
						}
						IUIService uiService = (IUIService)base.Component.Site.GetService(typeof(IUIService));
						this.inShowErrorDialog = true;
						try
						{
							DataGridViewDesigner.ShowErrorDialog(uiService, ex, (ListView)base.Component);
						}
						finally
						{
							this.inShowErrorDialog = false;
						}
						return;
					}
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000B874C File Offset: 0x000B694C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.Add(new ListViewActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x040017DB RID: 6107
		private DesignerActionListCollection _actionLists;

		// Token: 0x040017DC RID: 6108
		private NativeMethods.HDHITTESTINFO hdrhit = new NativeMethods.HDHITTESTINFO();

		// Token: 0x040017DD RID: 6109
		private bool inShowErrorDialog;
	}
}
