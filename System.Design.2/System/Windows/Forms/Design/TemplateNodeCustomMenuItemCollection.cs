using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000344 RID: 836
	internal class TemplateNodeCustomMenuItemCollection : CustomMenuItemCollection
	{
		// Token: 0x06002132 RID: 8498 RVA: 0x000CAED5 File Offset: 0x000C90D5
		public TemplateNodeCustomMenuItemCollection(IServiceProvider provider, Component currentItem)
		{
			this.serviceProvider = provider;
			this.currentItem = (currentItem as ToolStripItem);
			this.PopulateList();
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x000CAEF6 File Offset: 0x000C90F6
		private ToolStrip ParentTool
		{
			get
			{
				return this.currentItem.Owner;
			}
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000CAF04 File Offset: 0x000C9104
		private void PopulateList()
		{
			this.insertToolStripMenuItem = new ToolStripMenuItem();
			this.insertToolStripMenuItem.Text = SR.GetString("ToolStripItemContextMenuInsert");
			this.insertToolStripMenuItem.DropDown = ToolStripDesignerUtils.GetNewItemDropDown(this.ParentTool, this.currentItem, new EventHandler(this.AddNewItemClick), false, this.serviceProvider, true);
			base.Add(this.insertToolStripMenuItem);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000CAF70 File Offset: 0x000C9170
		private void AddNewItemClick(object sender, EventArgs e)
		{
			ItemTypeToolStripMenuItem itemTypeToolStripMenuItem = (ItemTypeToolStripMenuItem)sender;
			Type itemType = itemTypeToolStripMenuItem.ItemType;
			this.InsertItem(itemType);
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000CAF92 File Offset: 0x000C9192
		private void InsertItem(Type t)
		{
			this.InsertToolStripItem(t);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000CAF9C File Offset: 0x000C919C
		private void InsertToolStripItem(Type t)
		{
			IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			ToolStrip parentTool = this.ParentTool;
			int index = parentTool.Items.IndexOf(this.currentItem);
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("ToolStripAddingItem"));
			try
			{
				ToolStripDesigner._autoAddNewItems = false;
				IComponent component = designerHost.CreateComponent(t);
				IDesigner designer = designerHost.GetDesigner(component);
				if (designer is ComponentDesigner)
				{
					((ComponentDesigner)designer).InitializeNewComponent(null);
				}
				if (component is ToolStripButton || component is ToolStripSplitButton || component is ToolStripDropDownButton)
				{
					Image image = null;
					try
					{
						image = new Bitmap(typeof(ToolStripButton), "blank.bmp");
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Image"];
					if (propertyDescriptor != null && image != null)
					{
						propertyDescriptor.SetValue(component, image);
					}
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(component)["DisplayStyle"];
					if (propertyDescriptor2 != null)
					{
						propertyDescriptor2.SetValue(component, ToolStripItemDisplayStyle.Image);
					}
					PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(component)["ImageTransparentColor"];
					if (propertyDescriptor3 != null)
					{
						propertyDescriptor3.SetValue(component, Color.Magenta);
					}
				}
				parentTool.Items.Insert(index, (ToolStripItem)component);
				ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						component
					}, SelectionTypes.Replace);
				}
			}
			catch (Exception ex2)
			{
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
				if (ClientUtils.IsCriticalException(ex2))
				{
					throw;
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
				ToolStripDesigner._autoAddNewItems = true;
				ToolStripDropDown toolStripDropDown = parentTool as ToolStripDropDown;
				if (toolStripDropDown != null && toolStripDropDown.Visible)
				{
					ToolStripDropDownItem toolStripDropDownItem = toolStripDropDown.OwnerItem as ToolStripDropDownItem;
					if (toolStripDropDownItem != null)
					{
						ToolStripMenuItemDesigner toolStripMenuItemDesigner = designerHost.GetDesigner(toolStripDropDownItem) as ToolStripMenuItemDesigner;
						if (toolStripMenuItemDesigner != null)
						{
							toolStripMenuItemDesigner.ResetGlyphs(toolStripDropDownItem);
						}
					}
				}
			}
		}

		// Token: 0x04001925 RID: 6437
		private ToolStripItem currentItem;

		// Token: 0x04001926 RID: 6438
		private IServiceProvider serviceProvider;

		// Token: 0x04001927 RID: 6439
		private ToolStripMenuItem insertToolStripMenuItem;
	}
}
