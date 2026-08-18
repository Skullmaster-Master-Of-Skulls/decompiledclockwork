using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200031A RID: 794
	internal class NewItemsContextMenuStrip : GroupedContextMenuStrip
	{
		// Token: 0x06001F40 RID: 8000 RVA: 0x000BBC0C File Offset: 0x000B9E0C
		public NewItemsContextMenuStrip(IComponent component, ToolStripItem currentItem, EventHandler onClick, bool convertTo, IServiceProvider serviceProvider)
		{
			this.component = component;
			this.onClick = onClick;
			this.convertTo = convertTo;
			this.serviceProvider = serviceProvider;
			this.currentItem = currentItem;
			IUIService iuiservice = serviceProvider.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				base.Renderer = (ToolStripProfessionalRenderer)iuiservice.Styles["VsRenderer"];
			}
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000BBC7C File Offset: 0x000B9E7C
		protected override void OnOpening(CancelEventArgs e)
		{
			base.Groups["StandardList"].Items.Clear();
			base.Groups["CustomList"].Items.Clear();
			base.Populated = false;
			foreach (ToolStripItem toolStripItem in ToolStripDesignerUtils.GetStandardItemMenuItems(this.component, this.onClick, this.convertTo))
			{
				base.Groups["StandardList"].Items.Add(toolStripItem);
				if (this.convertTo)
				{
					ItemTypeToolStripMenuItem itemTypeToolStripMenuItem = toolStripItem as ItemTypeToolStripMenuItem;
					if (itemTypeToolStripMenuItem != null && this.currentItem != null && itemTypeToolStripMenuItem.ItemType == this.currentItem.GetType())
					{
						itemTypeToolStripMenuItem.Enabled = false;
					}
				}
			}
			foreach (ToolStripItem toolStripItem2 in ToolStripDesignerUtils.GetCustomItemMenuItems(this.component, this.onClick, this.convertTo, this.serviceProvider))
			{
				base.Groups["CustomList"].Items.Add(toolStripItem2);
				if (this.convertTo)
				{
					ItemTypeToolStripMenuItem itemTypeToolStripMenuItem2 = toolStripItem2 as ItemTypeToolStripMenuItem;
					if (itemTypeToolStripMenuItem2 != null && this.currentItem != null && itemTypeToolStripMenuItem2.ItemType == this.currentItem.GetType())
					{
						itemTypeToolStripMenuItem2.Enabled = false;
					}
				}
			}
			base.OnOpening(e);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000BBDDC File Offset: 0x000B9FDC
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys == Keys.Left || keys == Keys.Right)
			{
				base.Close();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x04001852 RID: 6226
		private IComponent component;

		// Token: 0x04001853 RID: 6227
		private EventHandler onClick;

		// Token: 0x04001854 RID: 6228
		private bool convertTo;

		// Token: 0x04001855 RID: 6229
		private IServiceProvider serviceProvider;

		// Token: 0x04001856 RID: 6230
		private ToolStripItem currentItem;
	}
}
