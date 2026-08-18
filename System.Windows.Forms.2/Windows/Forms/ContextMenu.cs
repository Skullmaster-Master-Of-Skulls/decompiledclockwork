using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000168 RID: 360
	[DefaultEvent("Popup")]
	public class ContextMenu : Menu
	{
		// Token: 0x06000F40 RID: 3904 RVA: 0x0002E8E9 File Offset: 0x0002CAE9
		public ContextMenu() : base(null)
		{
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0002E8F9 File Offset: 0x0002CAF9
		public ContextMenu(MenuItem[] menuItems) : base(menuItems)
		{
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0002E909 File Offset: 0x0002CB09
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ContextMenuSourceControlDescr")]
		public Control SourceControl
		{
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return this.sourceControl;
			}
		}

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06000F43 RID: 3907 RVA: 0x0002E911 File Offset: 0x0002CB11
		// (remove) Token: 0x06000F44 RID: 3908 RVA: 0x0002E92A File Offset: 0x0002CB2A
		[SRDescription("MenuItemOnInitDescr")]
		public event EventHandler Popup
		{
			add
			{
				this.onPopup = (EventHandler)Delegate.Combine(this.onPopup, value);
			}
			remove
			{
				this.onPopup = (EventHandler)Delegate.Remove(this.onPopup, value);
			}
		}

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06000F45 RID: 3909 RVA: 0x0002E943 File Offset: 0x0002CB43
		// (remove) Token: 0x06000F46 RID: 3910 RVA: 0x0002E95C File Offset: 0x0002CB5C
		[SRDescription("ContextMenuCollapseDescr")]
		public event EventHandler Collapse
		{
			add
			{
				this.onCollapse = (EventHandler)Delegate.Combine(this.onCollapse, value);
			}
			remove
			{
				this.onCollapse = (EventHandler)Delegate.Remove(this.onCollapse, value);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0002E975 File Offset: 0x0002CB75
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x0002E99C File Offset: 0x0002CB9C
		[Localizable(true)]
		[DefaultValue(RightToLeft.No)]
		[SRDescription("MenuRightToLeftDescr")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				if (RightToLeft.Inherit != this.rightToLeft)
				{
					return this.rightToLeft;
				}
				if (this.sourceControl != null)
				{
					return this.sourceControl.RightToLeft;
				}
				return RightToLeft.No;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("RightToLeft", (int)value, typeof(RightToLeft));
				}
				if (this.RightToLeft != value)
				{
					this.rightToLeft = value;
					base.UpdateRtl(value == RightToLeft.Yes);
				}
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0002E9E9 File Offset: 0x0002CBE9
		internal override bool RenderIsRightToLeft
		{
			get
			{
				return this.rightToLeft == RightToLeft.Yes;
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0002E9F4 File Offset: 0x0002CBF4
		protected internal virtual void OnPopup(EventArgs e)
		{
			if (this.onPopup != null)
			{
				this.onPopup(this, e);
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0002EA0B File Offset: 0x0002CC0B
		protected internal virtual void OnCollapse(EventArgs e)
		{
			if (this.onCollapse != null)
			{
				this.onCollapse(this, e);
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002EA22 File Offset: 0x0002CC22
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData, Control control)
		{
			this.sourceControl = control;
			return this.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002EA33 File Offset: 0x0002CC33
		private void ResetRightToLeft()
		{
			this.RightToLeft = RightToLeft.No;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002EA3C File Offset: 0x0002CC3C
		internal virtual bool ShouldSerializeRightToLeft()
		{
			return RightToLeft.Inherit != this.rightToLeft;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002EA4A File Offset: 0x0002CC4A
		public void Show(Control control, Point pos)
		{
			this.Show(control, pos, 66);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002EA56 File Offset: 0x0002CC56
		public void Show(Control control, Point pos, LeftRightAlignment alignment)
		{
			if (alignment == LeftRightAlignment.Left)
			{
				this.Show(control, pos, 74);
				return;
			}
			this.Show(control, pos, 66);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002EA70 File Offset: 0x0002CC70
		private void Show(Control control, Point pos, int flags)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!control.IsHandleCreated || !control.Visible)
			{
				throw new ArgumentException(SR.GetString("ContextMenuInvalidParent"), "control");
			}
			this.sourceControl = control;
			this.OnPopup(EventArgs.Empty);
			pos = control.PointToScreen(pos);
			SafeNativeMethods.TrackPopupMenuEx(new HandleRef(this, base.Handle), flags, pos.X, pos.Y, new HandleRef(control, control.Handle), null);
		}

		// Token: 0x04000820 RID: 2080
		private EventHandler onPopup;

		// Token: 0x04000821 RID: 2081
		private EventHandler onCollapse;

		// Token: 0x04000822 RID: 2082
		internal Control sourceControl;

		// Token: 0x04000823 RID: 2083
		private RightToLeft rightToLeft = RightToLeft.Inherit;
	}
}
