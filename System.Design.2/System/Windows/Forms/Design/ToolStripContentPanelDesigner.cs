using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000351 RID: 849
	internal class ToolStripContentPanelDesigner : PanelDesigner
	{
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x000CCF64 File Offset: 0x000CB164
		private ContextMenuStrip DesignerContextMenu
		{
			get
			{
				if (this.contextMenu == null)
				{
					this.contextMenu = new BaseContextMenuStrip(base.Component.Site, base.Component as Component);
					this.contextMenu.GroupOrdering.Clear();
					this.contextMenu.GroupOrdering.AddRange(new string[]
					{
						"Code",
						"Verbs",
						"Custom",
						"Selection",
						"Edit",
						"Properties"
					});
					this.contextMenu.Text = "CustomContextMenu";
				}
				return this.contextMenu;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000CD00C File Offset: 0x000CB20C
		public override IList SnapLines
		{
			get
			{
				ArrayList result = null;
				base.AddPaddingSnapLines(ref result);
				return result;
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool CanBeParentedTo(IDesigner parentDesigner)
		{
			return false;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000CD024 File Offset: 0x000CB224
		protected override void OnContextMenu(int x, int y)
		{
			ToolStripContentPanel toolStripContentPanel = base.Component as ToolStripContentPanel;
			if (toolStripContentPanel != null && toolStripContentPanel.Parent is ToolStripContainer)
			{
				this.DesignerContextMenu.Show(x, y);
				return;
			}
			base.OnContextMenu(x, y);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000CD064 File Offset: 0x000CB264
		protected override void PreFilterEvents(IDictionary events)
		{
			base.PreFilterEvents(events);
			string[] array = new string[]
			{
				"BindingContextChanged",
				"ChangeUICues",
				"ClientSizeChanged",
				"EnabledChanged",
				"FontChanged",
				"ForeColorChanged",
				"GiveFeedback",
				"ImeModeChanged",
				"Move",
				"QueryAccessibilityHelp",
				"Validated",
				"Validating",
				"VisibleChanged"
			};
			for (int i = 0; i < array.Length; i++)
			{
				EventDescriptor eventDescriptor = (EventDescriptor)events[array[i]];
				if (eventDescriptor != null)
				{
					events[array[i]] = TypeDescriptor.CreateEvent(eventDescriptor.ComponentType, eventDescriptor, new Attribute[]
					{
						BrowsableAttribute.No
					});
				}
			}
		}

		// Token: 0x0400194F RID: 6479
		private BaseContextMenuStrip contextMenu;
	}
}
