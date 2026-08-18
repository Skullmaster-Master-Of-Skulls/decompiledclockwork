using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200037A RID: 890
	internal sealed class DesignerActionBehavior : Behavior
	{
		// Token: 0x06002497 RID: 9367 RVA: 0x000E248F File Offset: 0x000E068F
		internal DesignerActionBehavior(IServiceProvider serviceProvider, IComponent relatedComponent, DesignerActionListCollection actionLists, DesignerActionUI parentUI)
		{
			this.actionLists = actionLists;
			this.serviceProvider = serviceProvider;
			this.relatedComponent = relatedComponent;
			this.parentUI = parentUI;
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x000E24B4 File Offset: 0x000E06B4
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x000E24BC File Offset: 0x000E06BC
		internal DesignerActionListCollection ActionLists
		{
			get
			{
				return this.actionLists;
			}
			set
			{
				this.actionLists = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000E24C5 File Offset: 0x000E06C5
		internal DesignerActionUI ParentUI
		{
			get
			{
				return this.parentUI;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x000E24CD File Offset: 0x000E06CD
		internal IComponent RelatedComponent
		{
			get
			{
				return this.relatedComponent;
			}
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000E24D5 File Offset: 0x000E06D5
		internal void HideUI()
		{
			this.ParentUI.HideDesignerActionPanel();
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000E24E4 File Offset: 0x000E06E4
		internal DesignerActionPanel CreateDesignerActionPanel(IComponent relatedComponent)
		{
			DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
			designerActionListCollection.AddRange(this.ActionLists);
			DesignerActionPanel designerActionPanel = new DesignerActionPanel(this.serviceProvider);
			designerActionPanel.UpdateTasks(designerActionListCollection, new DesignerActionListCollection(), SR.GetString("DesignerActionPanel_DefaultPanelTitle", new object[]
			{
				relatedComponent.GetType().Name
			}), null);
			return designerActionPanel;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000E253C File Offset: 0x000E073C
		internal void ShowUI(Glyph g)
		{
			DesignerActionGlyph designerActionGlyph = g as DesignerActionGlyph;
			if (designerActionGlyph == null)
			{
				return;
			}
			DesignerActionPanel panel = this.CreateDesignerActionPanel(this.RelatedComponent);
			this.ParentUI.ShowDesignerActionPanel(this.RelatedComponent, panel, designerActionGlyph);
		}

		// Token: 0x170007C1 RID: 1985
		// (set) Token: 0x0600249F RID: 9375 RVA: 0x000E2574 File Offset: 0x000E0774
		internal bool IgnoreNextMouseUp
		{
			set
			{
				this.ignoreNextMouseUp = value;
			}
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000E257D File Offset: 0x000E077D
		public override bool OnMouseDoubleClick(Glyph g, MouseButtons button, Point mouseLoc)
		{
			this.ignoreNextMouseUp = true;
			return true;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000E2587 File Offset: 0x000E0787
		public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
		{
			return !this.ParentUI.IsDesignerActionPanelVisible;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000E2598 File Offset: 0x000E0798
		public override bool OnMouseUp(Glyph g, MouseButtons button)
		{
			if (button != MouseButtons.Left || this.ParentUI == null)
			{
				return true;
			}
			bool result = true;
			if (this.ParentUI.IsDesignerActionPanelVisible)
			{
				this.HideUI();
			}
			else if (!this.ignoreNextMouseUp)
			{
				if (this.serviceProvider != null)
				{
					ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
					if (selectionService != null && selectionService.PrimarySelection != this.RelatedComponent)
					{
						selectionService.SetSelectedComponents(new List<IComponent>
						{
							this.RelatedComponent
						}, SelectionTypes.Click);
					}
				}
				this.ShowUI(g);
			}
			else
			{
				result = false;
			}
			this.ignoreNextMouseUp = false;
			return result;
		}

		// Token: 0x04001A79 RID: 6777
		private IComponent relatedComponent;

		// Token: 0x04001A7A RID: 6778
		private DesignerActionUI parentUI;

		// Token: 0x04001A7B RID: 6779
		private DesignerActionListCollection actionLists;

		// Token: 0x04001A7C RID: 6780
		private IServiceProvider serviceProvider;

		// Token: 0x04001A7D RID: 6781
		private bool ignoreNextMouseUp;
	}
}
