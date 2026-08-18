using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004FD RID: 1277
	[Designer("System.Web.UI.Design.WebControls.ViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(false)]
	[ToolboxData("<{0}:View runat=\"server\"></{0}:View>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class View : Control
	{
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x00104893 File Offset: 0x00103893
		// (set) Token: 0x06003E68 RID: 15976 RVA: 0x0010489B File Offset: 0x0010389B
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

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x001048AB File Offset: 0x001038AB
		// (set) Token: 0x06003E6A RID: 15978 RVA: 0x001048B3 File Offset: 0x001038B3
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

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06003E6B RID: 15979 RVA: 0x001048BC File Offset: 0x001038BC
		// (remove) Token: 0x06003E6C RID: 15980 RVA: 0x001048CF File Offset: 0x001038CF
		[WebSysDescription("View_Activate")]
		[WebCategory("Action")]
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

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06003E6D RID: 15981 RVA: 0x001048E2 File Offset: 0x001038E2
		// (remove) Token: 0x06003E6E RID: 15982 RVA: 0x001048F5 File Offset: 0x001038F5
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

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x00104908 File Offset: 0x00103908
		// (set) Token: 0x06003E70 RID: 15984 RVA: 0x0010492E File Offset: 0x0010392E
		[Browsable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Control_Visible")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x06003E71 RID: 15985 RVA: 0x00104948 File Offset: 0x00103948
		protected internal virtual void OnActivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[View._eventActivate];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00104978 File Offset: 0x00103978
		protected internal virtual void OnDeactivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[View._eventDeactivate];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x04002796 RID: 10134
		private static readonly object _eventActivate = new object();

		// Token: 0x04002797 RID: 10135
		private static readonly object _eventDeactivate = new object();

		// Token: 0x04002798 RID: 10136
		private bool _active;
	}
}
