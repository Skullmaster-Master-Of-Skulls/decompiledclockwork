using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001C5 RID: 453
	internal class CompositionCommandSet : CommandSet
	{
		// Token: 0x060011AE RID: 4526 RVA: 0x000562C4 File Offset: 0x000552C4
		public CompositionCommandSet(Control compositionUI, ISite site) : base(site)
		{
			this.compositionUI = compositionUI;
			this.commandSet = new CommandSet.CommandSetItem[]
			{
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySelect), MenuCommands.KeySelectNext),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySelect), MenuCommands.KeySelectPrevious)
			};
			if (base.MenuService != null)
			{
				for (int i = 0; i < this.commandSet.Length; i++)
				{
					base.MenuService.AddCommand(this.commandSet[i]);
				}
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00056368 File Offset: 0x00055368
		public override void Dispose()
		{
			if (base.MenuService != null)
			{
				for (int i = 0; i < this.commandSet.Length; i++)
				{
					base.MenuService.RemoveCommand(this.commandSet[i]);
				}
			}
			base.Dispose();
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000563AC File Offset: 0x000553AC
		protected override bool OnKeyCancel(object sender)
		{
			if (base.OnKeyCancel(sender))
			{
				return false;
			}
			ISelectionService selectionService = base.SelectionService;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (selectionService == null || designerHost == null)
			{
				return true;
			}
			IComponent rootComponent = designerHost.RootComponent;
			selectionService.SetSelectedComponents(new object[]
			{
				rootComponent
			}, SelectionTypes.Replace);
			return true;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00056404 File Offset: 0x00055404
		protected void OnKeySelect(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			bool backwards = menuCommand.CommandID.Equals(MenuCommands.KeySelectPrevious);
			this.RotateTabSelection(backwards);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00056430 File Offset: 0x00055430
		protected override void OnUpdateCommandStatus()
		{
			for (int i = 0; i < this.commandSet.Length; i++)
			{
				this.commandSet[i].UpdateStatus();
			}
			base.OnUpdateCommandStatus();
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00056464 File Offset: 0x00055464
		private void RotateTabSelection(bool backwards)
		{
			ComponentTray.TrayControl trayControl = null;
			ISelectionService selectionService = base.SelectionService;
			if (selectionService == null)
			{
				return;
			}
			IComponent component = selectionService.PrimarySelection as IComponent;
			IComponent component2;
			if (component != null)
			{
				component2 = component;
			}
			else
			{
				component2 = null;
				ICollection selectedComponents = selectionService.GetSelectedComponents();
				foreach (object obj in selectedComponents)
				{
					IComponent component3 = obj as IComponent;
					if (component3 != null)
					{
						component2 = component3;
						break;
					}
				}
			}
			Control control;
			if (component2 != null)
			{
				control = ComponentTray.TrayControl.FromComponent(component2);
			}
			else
			{
				control = null;
			}
			if (control != null)
			{
				for (int i = 1; i < this.compositionUI.Controls.Count; i++)
				{
					if (this.compositionUI.Controls[i] == control)
					{
						int num = i + 1;
						if (num >= this.compositionUI.Controls.Count)
						{
							num = 1;
						}
						ComponentTray.TrayControl trayControl2 = this.compositionUI.Controls[num] as ComponentTray.TrayControl;
						if (trayControl2 != null)
						{
							trayControl = trayControl2;
							break;
						}
					}
				}
			}
			else if (this.compositionUI.Controls.Count > 1)
			{
				ComponentTray.TrayControl trayControl3 = this.compositionUI.Controls[1] as ComponentTray.TrayControl;
				if (trayControl3 != null)
				{
					trayControl = trayControl3;
				}
			}
			if (trayControl != null)
			{
				selectionService.SetSelectedComponents(new object[]
				{
					trayControl.Component
				}, SelectionTypes.Replace);
			}
		}

		// Token: 0x040010DB RID: 4315
		private Control compositionUI;

		// Token: 0x040010DC RID: 4316
		private CommandSet.CommandSetItem[] commandSet;
	}
}
