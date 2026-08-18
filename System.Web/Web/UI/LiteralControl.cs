using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x02000420 RID: 1056
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LiteralControl : Control, ITextControl
	{
		// Token: 0x060032E2 RID: 13026 RVA: 0x000DD8B4 File Offset: 0x000DC8B4
		public LiteralControl()
		{
			base.PreventAutoID();
			base.SetEnableViewStateInternal(false);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000DD8C9 File Offset: 0x000DC8C9
		public LiteralControl(string text) : this()
		{
			this._text = ((text != null) ? text : string.Empty);
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000DD8E2 File Offset: 0x000DC8E2
		// (set) Token: 0x060032E5 RID: 13029 RVA: 0x000DD8EA File Offset: 0x000DC8EA
		public virtual string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = ((value != null) ? value : string.Empty);
			}
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000DD8FD File Offset: 0x000DC8FD
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000DD905 File Offset: 0x000DC905
		protected internal override void Render(HtmlTextWriter output)
		{
			output.Write(this._text);
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000DD913 File Offset: 0x000DC913
		internal override void InitRecursive(Control namingContainer)
		{
			this.ResolveAdapter();
			if (this._adapter != null)
			{
				this._adapter.OnInit(EventArgs.Empty);
				return;
			}
			this.OnInit(EventArgs.Empty);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000DD940 File Offset: 0x000DC940
		internal override void LoadRecursive()
		{
			if (this._adapter != null)
			{
				this._adapter.OnLoad(EventArgs.Empty);
				return;
			}
			this.OnLoad(EventArgs.Empty);
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000DD966 File Offset: 0x000DC966
		internal override void PreRenderRecursiveInternal()
		{
			if (this._adapter != null)
			{
				this._adapter.OnPreRender(EventArgs.Empty);
				return;
			}
			this.OnPreRender(EventArgs.Empty);
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000DD98C File Offset: 0x000DC98C
		internal override void UnloadRecursive(bool dispose)
		{
			if (this._adapter != null)
			{
				this._adapter.OnUnload(EventArgs.Empty);
			}
			else
			{
				this.OnUnload(EventArgs.Empty);
			}
			if (dispose)
			{
				this.Dispose();
			}
		}

		// Token: 0x040023D5 RID: 9173
		internal string _text;
	}
}
