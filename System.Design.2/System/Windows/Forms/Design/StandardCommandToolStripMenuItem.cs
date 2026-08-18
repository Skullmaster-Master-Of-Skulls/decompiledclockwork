using System;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000334 RID: 820
	internal class StandardCommandToolStripMenuItem : ToolStripMenuItem
	{
		// Token: 0x06002069 RID: 8297 RVA: 0x000C4690 File Offset: 0x000C2890
		public StandardCommandToolStripMenuItem(CommandID menuID, string text, string imageName, IServiceProvider serviceProvider)
		{
			this.menuID = menuID;
			this.serviceProvider = serviceProvider;
			try
			{
				this.menuCommand = this.MenuService.FindCommand(menuID);
			}
			catch
			{
				this.Enabled = false;
			}
			this.Text = text;
			this.name = imageName;
			this.RefreshItem();
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000C46F4 File Offset: 0x000C28F4
		public void RefreshItem()
		{
			if (this.menuCommand != null)
			{
				base.Visible = this.menuCommand.Visible;
				this.Enabled = this.menuCommand.Enabled;
				base.Checked = this.menuCommand.Checked;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x000C4731 File Offset: 0x000C2931
		public IMenuCommandService MenuService
		{
			get
			{
				if (this.menuCommandService == null)
				{
					this.menuCommandService = (IMenuCommandService)this.serviceProvider.GetService(typeof(IMenuCommandService));
				}
				return this.menuCommandService;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x000C4764 File Offset: 0x000C2964
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x000C47E4 File Offset: 0x000C29E4
		public override Image Image
		{
			get
			{
				if (!this._cachedImage)
				{
					this._cachedImage = true;
					try
					{
						if (this.name != null)
						{
							this._image = new Bitmap(BitmapSelector.GetResourceStream(typeof(ToolStripMenuItem), this.name + ".bmp"));
						}
						base.ImageTransparentColor = Color.Magenta;
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
				}
				return this._image;
			}
			set
			{
				this._image = value;
				this._cachedImage = true;
			}
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000C47F4 File Offset: 0x000C29F4
		protected override void OnClick(EventArgs e)
		{
			if (this.menuCommand != null)
			{
				this.menuCommand.Invoke();
				return;
			}
			if (this.MenuService != null)
			{
				this.MenuService.GlobalInvoke(this.menuID);
				return;
			}
		}

		// Token: 0x040018DD RID: 6365
		private bool _cachedImage;

		// Token: 0x040018DE RID: 6366
		private Image _image;

		// Token: 0x040018DF RID: 6367
		private CommandID menuID;

		// Token: 0x040018E0 RID: 6368
		private IMenuCommandService menuCommandService;

		// Token: 0x040018E1 RID: 6369
		private IServiceProvider serviceProvider;

		// Token: 0x040018E2 RID: 6370
		private string name;

		// Token: 0x040018E3 RID: 6371
		private MenuCommand menuCommand;
	}
}
