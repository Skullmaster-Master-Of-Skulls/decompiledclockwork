using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000169 RID: 361
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("Opening")]
	[SRDescription("DescriptionContextMenuStrip")]
	public class ContextMenuStrip : ToolStripDropDownMenu
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x0002EAF9 File Offset: 0x0002CCF9
		public ContextMenuStrip(IContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002EB16 File Offset: 0x0002CD16
		public ContextMenuStrip()
		{
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002EB1E File Offset: 0x0002CD1E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0002EB27 File Offset: 0x0002CD27
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ContextMenuStripSourceControlDescr")]
		public Control SourceControl
		{
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return base.SourceControlInternal;
			}
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002EB30 File Offset: 0x0002CD30
		internal ContextMenuStrip Clone()
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			contextMenuStrip.Events.AddHandlers(base.Events);
			contextMenuStrip.AutoClose = base.AutoClose;
			contextMenuStrip.AutoSize = this.AutoSize;
			contextMenuStrip.Bounds = base.Bounds;
			contextMenuStrip.ImageList = base.ImageList;
			contextMenuStrip.ShowCheckMargin = base.ShowCheckMargin;
			contextMenuStrip.ShowImageMargin = base.ShowImageMargin;
			for (int i = 0; i < this.Items.Count; i++)
			{
				ToolStripItem toolStripItem = this.Items[i];
				if (toolStripItem is ToolStripSeparator)
				{
					contextMenuStrip.Items.Add(new ToolStripSeparator());
				}
				else if (toolStripItem is ToolStripMenuItem)
				{
					ToolStripMenuItem toolStripMenuItem = toolStripItem as ToolStripMenuItem;
					contextMenuStrip.Items.Add(toolStripMenuItem.Clone());
				}
			}
			return contextMenuStrip;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002EBFC File Offset: 0x0002CDFC
		internal void ShowInternal(Control source, Point location, bool isKeyboardActivated)
		{
			base.Show(source, location);
			if (isKeyboardActivated)
			{
				ToolStripManager.ModalMenuFilter.Instance.ShowUnderlines = true;
			}
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002EC14 File Offset: 0x0002CE14
		internal void ShowInTaskbar(int x, int y)
		{
			base.WorkingAreaConstrained = false;
			Rectangle rectangle = base.CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.AboveLeft);
			Rectangle bounds = Screen.FromRectangle(rectangle).Bounds;
			if (rectangle.Y < bounds.Y)
			{
				rectangle = base.CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.BelowLeft);
			}
			else if (rectangle.X < bounds.X)
			{
				rectangle = base.CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.AboveRight);
			}
			rectangle = WindowsFormsUtils.ConstrainToBounds(bounds, rectangle);
			base.Show(rectangle.X, rectangle.Y);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002EC9F File Offset: 0x0002CE9F
		protected override void SetVisibleCore(bool visible)
		{
			if (!visible)
			{
				base.WorkingAreaConstrained = true;
			}
			base.SetVisibleCore(visible);
		}
	}
}
