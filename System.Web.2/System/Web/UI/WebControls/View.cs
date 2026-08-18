using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000512 RID: 1298
	[ParseChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.ViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:View runat=\"server\"></{0}:View>")]
	public class View : Control
	{
		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x000D538A File Offset: 0x000D358A
		// (set) Token: 0x06004127 RID: 16679 RVA: 0x000D5392 File Offset: 0x000D3592
		internal bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
				base.Visible = true;
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x00075E05 File Offset: 0x00074005
		// (set) Token: 0x06004129 RID: 16681 RVA: 0x00075E0D File Offset: 0x0007400D
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x14000104 RID: 260
		// (add) Token: 0x0600412A RID: 16682 RVA: 0x000D53A2 File Offset: 0x000D35A2
		// (remove) Token: 0x0600412B RID: 16683 RVA: 0x000D53B5 File Offset: 0x000D35B5
		[WebCategory("Action")]
		[WebSysDescription("View_Activate")]
		public event EventHandler Activate
		{
			add
			{
				base.Events.AddHandler(View._eventActivate, value);
			}
			remove
			{
				base.Events.RemoveHandler(View._eventActivate, value);
			}
		}

		// Token: 0x14000105 RID: 261
		// (add) Token: 0x0600412C RID: 16684 RVA: 0x000D53C8 File Offset: 0x000D35C8
		// (remove) Token: 0x0600412D RID: 16685 RVA: 0x000D53DB File Offset: 0x000D35DB
		[WebCategory("Action")]
		[WebSysDescription("View_Deactivate")]
		public event EventHandler Deactivate
		{
			add
			{
				base.Events.AddHandler(View._eventDeactivate, value);
			}
			remove
			{
				base.Events.RemoveHandler(View._eventDeactivate, value);
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x000D53EE File Offset: 0x000D35EE
		// (set) Token: 0x0600412F RID: 16687 RVA: 0x000D5414 File Offset: 0x000D3614
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Behavior")]
		[WebSysDescription("Control_Visible")]
		public override bool Visible
		{
			get
			{
				if (this.Parent == null)
				{
					return this.Active;
				}
				return this.Active && this.Parent.Visible;
			}
			set
			{
				if (base.DesignMode)
				{
					return;
				}
				throw new InvalidOperationException(SR.GetString("View_CannotSetVisible"));
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000D5430 File Offset: 0x000D3630
		protected internal virtual void OnActivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[View._eventActivate];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000D5460 File Offset: 0x000D3660
		protected internal virtual void OnDeactivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[View._eventDeactivate];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x04002506 RID: 9478
		private static readonly object _eventActivate = new object();

		// Token: 0x04002507 RID: 9479
		private static readonly object _eventDeactivate = new object();

		// Token: 0x04002508 RID: 9480
		private bool _active;
	}
}
