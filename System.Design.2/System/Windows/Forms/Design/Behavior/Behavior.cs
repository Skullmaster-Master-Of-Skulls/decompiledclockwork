using System;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000372 RID: 882
	public abstract class Behavior
	{
		// Token: 0x06002416 RID: 9238 RVA: 0x0000362F File Offset: 0x0000182F
		protected Behavior()
		{
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000E069D File Offset: 0x000DE89D
		protected Behavior(bool callParentBehavior, BehaviorService behaviorService)
		{
			if (callParentBehavior && behaviorService == null)
			{
				throw new ArgumentException("behaviorService");
			}
			this.callParentBehavior = callParentBehavior;
			this.bhvSvc = behaviorService;
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x000E06C4 File Offset: 0x000DE8C4
		private Behavior GetNextBehavior
		{
			get
			{
				if (this.bhvSvc != null)
				{
					return this.bhvSvc.GetNextBehavior(this);
				}
				return null;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000E06DC File Offset: 0x000DE8DC
		public virtual Cursor Cursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000E06E3 File Offset: 0x000DE8E3
		public virtual bool DisableAllCommands
		{
			get
			{
				return this.callParentBehavior && this.GetNextBehavior != null && this.GetNextBehavior.DisableAllCommands;
			}
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000E0704 File Offset: 0x000DE904
		public virtual MenuCommand FindCommand(CommandID commandId)
		{
			MenuCommand result;
			try
			{
				if (this.callParentBehavior && this.GetNextBehavior != null)
				{
					result = this.GetNextBehavior.FindCommand(commandId);
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000E074C File Offset: 0x000DE94C
		private bool GlyphIsValid(Glyph g)
		{
			return g != null && g.Behavior != null && g.Behavior != this;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000E0767 File Offset: 0x000DE967
		public virtual void OnLoseCapture(Glyph g, EventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnLoseCapture(g, e);
				return;
			}
			if (this.GlyphIsValid(g))
			{
				g.Behavior.OnLoseCapture(g, e);
			}
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000E079D File Offset: 0x000DE99D
		public virtual bool OnMouseDoubleClick(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				return this.GetNextBehavior.OnMouseDoubleClick(g, button, mouseLoc);
			}
			return this.GlyphIsValid(g) && g.Behavior.OnMouseDoubleClick(g, button, mouseLoc);
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000E07D7 File Offset: 0x000DE9D7
		public virtual bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				return this.GetNextBehavior.OnMouseDown(g, button, mouseLoc);
			}
			return this.GlyphIsValid(g) && g.Behavior.OnMouseDown(g, button, mouseLoc);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000E0811 File Offset: 0x000DEA11
		public virtual bool OnMouseEnter(Glyph g)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				return this.GetNextBehavior.OnMouseEnter(g);
			}
			return this.GlyphIsValid(g) && g.Behavior.OnMouseEnter(g);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000E0847 File Offset: 0x000DEA47
		public virtual bool OnMouseHover(Glyph g, Point mouseLoc)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				return this.GetNextBehavior.OnMouseHover(g, mouseLoc);
			}
			return this.GlyphIsValid(g) && g.Behavior.OnMouseHover(g, mouseLoc);
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000E087F File Offset: 0x000DEA7F
		public virtual bool OnMouseLeave(Glyph g)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				return this.GetNextBehavior.OnMouseLeave(g);
			}
			return this.GlyphIsValid(g) && g.Behavior.OnMouseLeave(g);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000E08B5 File Offset: 0x000DEAB5
		public virtual bool OnMouseMove(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				return this.GetNextBehavior.OnMouseMove(g, button, mouseLoc);
			}
			return this.GlyphIsValid(g) && g.Behavior.OnMouseMove(g, button, mouseLoc);
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000E08EF File Offset: 0x000DEAEF
		public virtual bool OnMouseUp(Glyph g, MouseButtons button)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				return this.GetNextBehavior.OnMouseUp(g, button);
			}
			return this.GlyphIsValid(g) && g.Behavior.OnMouseUp(g, button);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000E0927 File Offset: 0x000DEB27
		public virtual void OnDragDrop(Glyph g, DragEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnDragDrop(g, e);
				return;
			}
			if (this.GlyphIsValid(g))
			{
				g.Behavior.OnDragDrop(g, e);
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000E095D File Offset: 0x000DEB5D
		public virtual void OnDragEnter(Glyph g, DragEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnDragEnter(g, e);
				return;
			}
			if (this.GlyphIsValid(g))
			{
				g.Behavior.OnDragEnter(g, e);
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000E0993 File Offset: 0x000DEB93
		public virtual void OnDragLeave(Glyph g, EventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnDragLeave(g, e);
				return;
			}
			if (this.GlyphIsValid(g))
			{
				g.Behavior.OnDragLeave(g, e);
			}
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000E09CC File Offset: 0x000DEBCC
		public virtual void OnDragOver(Glyph g, DragEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnDragOver(g, e);
				return;
			}
			if (this.GlyphIsValid(g))
			{
				g.Behavior.OnDragOver(g, e);
				return;
			}
			if (e.Effect != DragDropEffects.None)
			{
				e.Effect = ((Control.ModifierKeys == Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000E0A2C File Offset: 0x000DEC2C
		public virtual void OnGiveFeedback(Glyph g, GiveFeedbackEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnGiveFeedback(g, e);
				return;
			}
			if (this.GlyphIsValid(g))
			{
				g.Behavior.OnGiveFeedback(g, e);
			}
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000E0A62 File Offset: 0x000DEC62
		public virtual void OnQueryContinueDrag(Glyph g, QueryContinueDragEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnQueryContinueDrag(g, e);
				return;
			}
			if (this.GlyphIsValid(g))
			{
				g.Behavior.OnQueryContinueDrag(g, e);
			}
		}

		// Token: 0x04001A4E RID: 6734
		private bool callParentBehavior;

		// Token: 0x04001A4F RID: 6735
		private BehaviorService bhvSvc;
	}
}
