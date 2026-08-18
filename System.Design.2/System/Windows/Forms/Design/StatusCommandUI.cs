using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000338 RID: 824
	internal class StatusCommandUI
	{
		// Token: 0x06002078 RID: 8312 RVA: 0x000C57B1 File Offset: 0x000C39B1
		public StatusCommandUI(IServiceProvider provider)
		{
			this.serviceProvider = provider;
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x000C57C0 File Offset: 0x000C39C0
		private IMenuCommandService MenuService
		{
			get
			{
				if (this.menuService == null)
				{
					this.menuService = (IMenuCommandService)this.serviceProvider.GetService(typeof(IMenuCommandService));
				}
				return this.menuService;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x000C57F0 File Offset: 0x000C39F0
		private MenuCommand StatusRectCommand
		{
			get
			{
				if (this.statusRectCommand == null && this.MenuService != null)
				{
					this.statusRectCommand = this.MenuService.FindCommand(MenuCommands.SetStatusRectangle);
				}
				return this.statusRectCommand;
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000C5820 File Offset: 0x000C3A20
		public void SetStatusInformation(Component selectedComponent, Point location)
		{
			if (selectedComponent == null)
			{
				return;
			}
			Rectangle rectangle = Rectangle.Empty;
			Control control = selectedComponent as Control;
			if (control != null)
			{
				rectangle = control.Bounds;
			}
			else
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(selectedComponent)["Bounds"];
				if (propertyDescriptor != null && typeof(Rectangle).IsAssignableFrom(propertyDescriptor.PropertyType))
				{
					rectangle = (Rectangle)propertyDescriptor.GetValue(selectedComponent);
				}
			}
			if (location != Point.Empty)
			{
				rectangle.X = location.X;
				rectangle.Y = location.Y;
			}
			if (this.StatusRectCommand != null)
			{
				this.StatusRectCommand.Invoke(rectangle);
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000C58C4 File Offset: 0x000C3AC4
		public void SetStatusInformation(Component selectedComponent)
		{
			if (selectedComponent == null)
			{
				return;
			}
			Rectangle rectangle = Rectangle.Empty;
			Control control = selectedComponent as Control;
			if (control != null)
			{
				rectangle = control.Bounds;
			}
			else
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(selectedComponent)["Bounds"];
				if (propertyDescriptor != null && typeof(Rectangle).IsAssignableFrom(propertyDescriptor.PropertyType))
				{
					rectangle = (Rectangle)propertyDescriptor.GetValue(selectedComponent);
				}
			}
			if (this.StatusRectCommand != null)
			{
				this.StatusRectCommand.Invoke(rectangle);
			}
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000C593F File Offset: 0x000C3B3F
		public void SetStatusInformation(Rectangle bounds)
		{
			if (this.StatusRectCommand != null)
			{
				this.StatusRectCommand.Invoke(bounds);
			}
		}

		// Token: 0x040018F1 RID: 6385
		private MenuCommand statusRectCommand;

		// Token: 0x040018F2 RID: 6386
		private IMenuCommandService menuService;

		// Token: 0x040018F3 RID: 6387
		private IServiceProvider serviceProvider;
	}
}
