using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000358 RID: 856
	internal class ToolStripEditorManager
	{
		// Token: 0x0600226A RID: 8810 RVA: 0x000D338C File Offset: 0x000D158C
		public ToolStripEditorManager(IComponent comp)
		{
			this.comp = comp;
			this.behaviorService = (BehaviorService)comp.Site.GetService(typeof(BehaviorService));
			this.designerHost = (IDesignerHost)comp.Site.GetService(typeof(IDesignerHost));
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000D33F4 File Offset: 0x000D15F4
		internal void ActivateEditor(ToolStripItem item, bool clicked)
		{
			if (item != this.currentItem)
			{
				if (this.editor != null)
				{
					this.behaviorService.AdornerWindowControl.Controls.Remove(this.editor);
					this.behaviorService.Invalidate(this.editor.Bounds);
					this.editorUI = null;
					this.editor = null;
					this.currentItem = null;
					this.itemDesigner.IsEditorActive = false;
					if (this.currentItem != null)
					{
						this.currentItem = null;
					}
				}
				if (item != null)
				{
					this.currentItem = item;
					if (this.designerHost != null)
					{
						this.itemDesigner = (ToolStripItemDesigner)this.designerHost.GetDesigner(this.currentItem);
					}
					this.editorUI = this.itemDesigner.Editor;
					if (this.editorUI != null)
					{
						this.itemDesigner.IsEditorActive = true;
						this.editor = new ToolStripEditorManager.ToolStripEditorControl(this.editorUI.EditorToolStrip, this.editorUI.Bounds);
						this.behaviorService.AdornerWindowControl.Controls.Add(this.editor);
						this.lastKnownEditorBounds = this.editor.Bounds;
						this.editor.BringToFront();
						this.editorUI.ignoreFirstKeyUp = true;
						this.editorUI.FocusEditor(this.currentItem);
					}
				}
			}
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00003937 File Offset: 0x00001B37
		internal void CloseManager()
		{
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000D3544 File Offset: 0x000D1744
		private void OnEditorResize(object sender, EventArgs e)
		{
			this.behaviorService.Invalidate(this.lastKnownEditorBounds);
			if (this.editor != null)
			{
				this.lastKnownEditorBounds = this.editor.Bounds;
			}
		}

		// Token: 0x040019A5 RID: 6565
		private BehaviorService behaviorService;

		// Token: 0x040019A6 RID: 6566
		private IDesignerHost designerHost;

		// Token: 0x040019A7 RID: 6567
		private IComponent comp;

		// Token: 0x040019A8 RID: 6568
		private Rectangle lastKnownEditorBounds = Rectangle.Empty;

		// Token: 0x040019A9 RID: 6569
		private ToolStripEditorManager.ToolStripEditorControl editor;

		// Token: 0x040019AA RID: 6570
		private ToolStripTemplateNode editorUI;

		// Token: 0x040019AB RID: 6571
		private ToolStripItem currentItem;

		// Token: 0x040019AC RID: 6572
		private ToolStripItemDesigner itemDesigner;

		// Token: 0x0200059A RID: 1434
		private class ToolStripEditorControl : Panel
		{
			// Token: 0x0600334E RID: 13134 RVA: 0x00118478 File Offset: 0x00116678
			public ToolStripEditorControl(Control editorToolStrip, Rectangle bounds)
			{
				this.wrappedEditor = editorToolStrip;
				this.bounds = bounds;
				this.wrappedEditor.Resize += this.OnWrappedEditorResize;
				base.Controls.Add(editorToolStrip);
				base.Location = new Point(bounds.X, bounds.Y);
				this.Text = "InSituEditorWrapper";
				this.UpdateSize();
			}

			// Token: 0x0600334F RID: 13135 RVA: 0x00003937 File Offset: 0x00001B37
			private void OnWrappedEditorResize(object sender, EventArgs e)
			{
			}

			// Token: 0x06003350 RID: 13136 RVA: 0x001184E8 File Offset: 0x001166E8
			private void UpdateSize()
			{
				base.Size = new Size(this.wrappedEditor.Size.Width, this.wrappedEditor.Size.Height);
			}

			// Token: 0x0400225C RID: 8796
			private Control wrappedEditor;

			// Token: 0x0400225D RID: 8797
			private Rectangle bounds;
		}
	}
}
