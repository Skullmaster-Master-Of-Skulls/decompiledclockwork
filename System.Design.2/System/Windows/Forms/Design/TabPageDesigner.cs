using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000343 RID: 835
	internal class TabPageDesigner : PanelDesigner
	{
		// Token: 0x06002129 RID: 8489 RVA: 0x000CAE30 File Offset: 0x000C9030
		public override bool CanBeParentedTo(IDesigner parentDesigner)
		{
			return parentDesigner != null && parentDesigner.Component is TabControl;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x000CAE48 File Offset: 0x000C9048
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				Control control = this.Control;
				if (control != null && control.Parent is TabControl)
				{
					selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
				}
				return selectionRules;
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000CAE79 File Offset: 0x000C9079
		internal void OnDragDropInternal(DragEventArgs de)
		{
			this.OnDragDrop(de);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000CAE82 File Offset: 0x000C9082
		internal void OnDragEnterInternal(DragEventArgs de)
		{
			this.OnDragEnter(de);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000CAE8B File Offset: 0x000C908B
		internal void OnDragLeaveInternal(EventArgs e)
		{
			this.OnDragLeave(e);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000CAE94 File Offset: 0x000C9094
		internal void OnDragOverInternal(DragEventArgs e)
		{
			this.OnDragOver(e);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000CAE9D File Offset: 0x000C909D
		internal void OnGiveFeedbackInternal(GiveFeedbackEventArgs e)
		{
			this.OnGiveFeedback(e);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000CAEA8 File Offset: 0x000C90A8
		protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			this.OnSetCursor();
			Rectangle empty = Rectangle.Empty;
			return new ControlBodyGlyph(empty, Cursor.Current, this.Control, this);
		}
	}
}
