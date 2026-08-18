using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000299 RID: 665
	internal class BaseContextMenuStrip : GroupedContextMenuStrip
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x000911A4 File Offset: 0x0008F3A4
		public BaseContextMenuStrip(IServiceProvider provider, Component component)
		{
			this.serviceProvider = provider;
			this.component = component;
			this.InitializeContextMenu();
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000911C0 File Offset: 0x0008F3C0
		private void AddCodeMenuItem()
		{
			StandardCommandToolStripMenuItem item = new StandardCommandToolStripMenuItem(StandardCommands.ViewCode, SR.GetString("ContextMenuViewCode"), "viewcode", this.serviceProvider);
			base.Groups["Code"].Items.Add(item);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00091208 File Offset: 0x0008F408
		private void AddZorderMenuItem()
		{
			StandardCommandToolStripMenuItem item = new StandardCommandToolStripMenuItem(StandardCommands.BringToFront, SR.GetString("ContextMenuBringToFront"), "bringToFront", this.serviceProvider);
			base.Groups["ZOrder"].Items.Add(item);
			item = new StandardCommandToolStripMenuItem(StandardCommands.SendToBack, SR.GetString("ContextMenuSendToBack"), "sendToBack", this.serviceProvider);
			base.Groups["ZOrder"].Items.Add(item);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0009128C File Offset: 0x0008F48C
		private void AddGridMenuItem()
		{
			StandardCommandToolStripMenuItem item = new StandardCommandToolStripMenuItem(StandardCommands.AlignToGrid, SR.GetString("ContextMenuAlignToGrid"), "alignToGrid", this.serviceProvider);
			base.Groups["Grid"].Items.Add(item);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000912D4 File Offset: 0x0008F4D4
		private void AddLockMenuItem()
		{
			StandardCommandToolStripMenuItem item = new StandardCommandToolStripMenuItem(StandardCommands.LockControls, SR.GetString("ContextMenuLockControls"), "lockControls", this.serviceProvider);
			base.Groups["Lock"].Items.Add(item);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0009131C File Offset: 0x0008F51C
		private void RefreshSelectionMenuItem()
		{
			int num = -1;
			if (this.selectionMenuItem != null)
			{
				num = this.Items.IndexOf(this.selectionMenuItem);
				base.Groups["Selection"].Items.Remove(this.selectionMenuItem);
				this.Items.Remove(this.selectionMenuItem);
			}
			ArrayList arrayList = new ArrayList();
			int num2 = 0;
			ISelectionService selectionService = this.serviceProvider.GetService(typeof(ISelectionService)) as ISelectionService;
			IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (selectionService != null && designerHost != null)
			{
				IComponent rootComponent = designerHost.RootComponent;
				Control control = selectionService.PrimarySelection as Control;
				if (control != null && rootComponent != null && control != rootComponent)
				{
					for (Control parent = control.Parent; parent != null; parent = parent.Parent)
					{
						if (parent.Site != null)
						{
							arrayList.Add(parent);
							num2++;
						}
						if (parent == rootComponent)
						{
							break;
						}
					}
				}
				else if (selectionService.PrimarySelection is ToolStripItem)
				{
					ToolStripItem toolStripItem = selectionService.PrimarySelection as ToolStripItem;
					ToolStripItemDesigner toolStripItemDesigner = designerHost.GetDesigner(toolStripItem) as ToolStripItemDesigner;
					if (toolStripItemDesigner != null)
					{
						arrayList = toolStripItemDesigner.AddParentTree();
						num2 = arrayList.Count;
					}
				}
			}
			if (num2 > 0)
			{
				this.selectionMenuItem = new ToolStripMenuItem();
				IUIService iuiservice = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
				if (iuiservice != null)
				{
					this.selectionMenuItem.DropDown.Renderer = (ToolStripProfessionalRenderer)iuiservice.Styles["VsRenderer"];
					this.selectionMenuItem.DropDown.Font = (Font)iuiservice.Styles["DialogFont"];
					if (iuiservice.Styles["VsColorPanelText"] is Color)
					{
						this.selectionMenuItem.DropDown.ForeColor = (Color)iuiservice.Styles["VsColorPanelText"];
					}
				}
				this.selectionMenuItem.Text = SR.GetString("ContextMenuSelect");
				foreach (object obj in arrayList)
				{
					Component c = (Component)obj;
					ToolStripMenuItem value = new BaseContextMenuStrip.SelectToolStripMenuItem(c, this.serviceProvider);
					this.selectionMenuItem.DropDownItems.Add(value);
				}
				base.Groups["Selection"].Items.Add(this.selectionMenuItem);
				if (num != -1)
				{
					this.Items.Insert(num, this.selectionMenuItem);
				}
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000915D0 File Offset: 0x0008F7D0
		private void AddVerbMenuItem()
		{
			IMenuCommandService menuCommandService = (IMenuCommandService)this.serviceProvider.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				DesignerVerbCollection verbs = menuCommandService.Verbs;
				foreach (object obj in verbs)
				{
					DesignerVerb verb = (DesignerVerb)obj;
					DesignerVerbToolStripMenuItem item = new DesignerVerbToolStripMenuItem(verb);
					base.Groups["Verbs"].Items.Add(item);
				}
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0009166C File Offset: 0x0008F86C
		private void AddEditMenuItem()
		{
			StandardCommandToolStripMenuItem item = new StandardCommandToolStripMenuItem(StandardCommands.Cut, SR.GetString("ContextMenuCut"), "cut", this.serviceProvider);
			base.Groups["Edit"].Items.Add(item);
			item = new StandardCommandToolStripMenuItem(StandardCommands.Copy, SR.GetString("ContextMenuCopy"), "copy", this.serviceProvider);
			base.Groups["Edit"].Items.Add(item);
			item = new StandardCommandToolStripMenuItem(StandardCommands.Paste, SR.GetString("ContextMenuPaste"), "paste", this.serviceProvider);
			base.Groups["Edit"].Items.Add(item);
			item = new StandardCommandToolStripMenuItem(StandardCommands.Delete, SR.GetString("ContextMenuDelete"), "delete", this.serviceProvider);
			base.Groups["Edit"].Items.Add(item);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00091768 File Offset: 0x0008F968
		private void AddPropertiesMenuItem()
		{
			StandardCommandToolStripMenuItem item = new StandardCommandToolStripMenuItem(StandardCommands.DocumentOutline, SR.GetString("ContextMenuDocumentOutline"), "", this.serviceProvider);
			base.Groups["Properties"].Items.Add(item);
			item = new StandardCommandToolStripMenuItem(MenuCommands.DesignerProperties, SR.GetString("ContextMenuProperties"), "properties", this.serviceProvider);
			base.Groups["Properties"].Items.Add(item);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000917EC File Offset: 0x0008F9EC
		private void InitializeContextMenu()
		{
			base.Name = "designerContextMenuStrip";
			IUIService iuiservice = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				base.Renderer = (ToolStripProfessionalRenderer)iuiservice.Styles["VsRenderer"];
				if (iuiservice.Styles["VsColorPanelText"] is Color)
				{
					base.ForeColor = (Color)iuiservice.Styles["VsColorPanelText"];
				}
			}
			base.GroupOrdering.AddRange(new string[]
			{
				"Code",
				"ZOrder",
				"Grid",
				"Lock",
				"Verbs",
				"Custom",
				"Selection",
				"Edit",
				"Properties"
			});
			this.AddCodeMenuItem();
			this.AddZorderMenuItem();
			this.AddGridMenuItem();
			this.AddLockMenuItem();
			this.AddVerbMenuItem();
			this.RefreshSelectionMenuItem();
			this.AddEditMenuItem();
			this.AddPropertiesMenuItem();
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000918FC File Offset: 0x0008FAFC
		public override void RefreshItems()
		{
			IUIService iuiservice = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				this.Font = (Font)iuiservice.Styles["DialogFont"];
			}
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				StandardCommandToolStripMenuItem standardCommandToolStripMenuItem = toolStripItem as StandardCommandToolStripMenuItem;
				if (standardCommandToolStripMenuItem != null)
				{
					standardCommandToolStripMenuItem.RefreshItem();
				}
			}
			this.RefreshSelectionMenuItem();
		}

		// Token: 0x0400159C RID: 5532
		private IServiceProvider serviceProvider;

		// Token: 0x0400159D RID: 5533
		private Component component;

		// Token: 0x0400159E RID: 5534
		private ToolStripMenuItem selectionMenuItem;

		// Token: 0x0200052B RID: 1323
		private class SelectToolStripMenuItem : ToolStripMenuItem
		{
			// Token: 0x06003043 RID: 12355 RVA: 0x0010943C File Offset: 0x0010763C
			public SelectToolStripMenuItem(Component c, IServiceProvider provider)
			{
				this.comp = c;
				this.serviceProvider = provider;
				string text = null;
				if (this.comp != null)
				{
					ISite site = this.comp.Site;
					if (site != null)
					{
						INestedSite nestedSite = site as INestedSite;
						if (nestedSite != null && !string.IsNullOrEmpty(nestedSite.FullName))
						{
							text = nestedSite.FullName;
						}
						else if (!string.IsNullOrEmpty(site.Name))
						{
							text = site.Name;
						}
					}
				}
				this.Text = SR.GetString("ToolStripSelectMenuItem", new object[]
				{
					text
				});
				this._itemType = c.GetType();
			}

			// Token: 0x1700095E RID: 2398
			// (get) Token: 0x06003044 RID: 12356 RVA: 0x001094D0 File Offset: 0x001076D0
			// (set) Token: 0x06003045 RID: 12357 RVA: 0x00109559 File Offset: 0x00107759
			public override Image Image
			{
				get
				{
					if (!this._cachedImage)
					{
						this._cachedImage = true;
						ToolboxItem toolboxItem = ToolboxService.GetToolboxItem(this._itemType);
						if (toolboxItem != null)
						{
							this._image = toolboxItem.Bitmap;
						}
						else if (this._itemType.Namespace == BaseContextMenuStrip.SelectToolStripMenuItem.systemWindowsFormsNamespace)
						{
							this._image = ToolboxBitmapAttribute.GetImageFromResource(this._itemType, null, false);
						}
						if (this._image == null)
						{
							this._image = ToolboxBitmapAttribute.GetImageFromResource(this.comp.GetType(), null, false);
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

			// Token: 0x06003046 RID: 12358 RVA: 0x0010956C File Offset: 0x0010776C
			protected override void OnClick(EventArgs e)
			{
				ISelectionService selectionService = this.serviceProvider.GetService(typeof(ISelectionService)) as ISelectionService;
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						this.comp
					}, SelectionTypes.Replace);
				}
			}

			// Token: 0x040020CF RID: 8399
			private Component comp;

			// Token: 0x040020D0 RID: 8400
			private IServiceProvider serviceProvider;

			// Token: 0x040020D1 RID: 8401
			private Type _itemType;

			// Token: 0x040020D2 RID: 8402
			private bool _cachedImage;

			// Token: 0x040020D3 RID: 8403
			private Image _image;

			// Token: 0x040020D4 RID: 8404
			private static string systemWindowsFormsNamespace = typeof(ToolStripItem).Namespace;
		}
	}
}
