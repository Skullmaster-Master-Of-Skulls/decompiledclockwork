using System;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x020002C0 RID: 704
	[ToolboxItem(false)]
	public class LiteralControl : Control, ITextControl
	{
		// Token: 0x06001FE1 RID: 8161 RVA: 0x000657A4 File Offset: 0x000639A4
		public LiteralControl()
		{
			base.PreventAutoID();
			base.SetEnableViewStateInternal(false);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x000657B9 File Offset: 0x000639B9
		public LiteralControl(string text) : this()
		{
			this._text = ((text != null) ? text : string.Empty);
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x000657D2 File Offset: 0x000639D2
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x000657DA File Offset: 0x000639DA
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

		// Token: 0x06001FE5 RID: 8165 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x000657ED File Offset: 0x000639ED
		protected internal override void Render(HtmlTextWriter output)
		{
			output.Write(this._text);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000657FB File Offset: 0x000639FB
		internal override void InitRecursive(Control namingContainer)
		{
			this.ResolveAdapter();
			if (base.AdapterInternal != null)
			{
				base.AdapterInternal.OnInit(EventArgs.Empty);
				return;
			}
			this.OnInit(EventArgs.Empty);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00065828 File Offset: 0x00063A28
		internal override void LoadRecursive()
		{
			if (base.AdapterInternal != null)
			{
				base.AdapterInternal.OnLoad(EventArgs.Empty);
				return;
			}
			this.OnLoad(EventArgs.Empty);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0006584E File Offset: 0x00063A4E
		internal override void PreRenderRecursiveInternal()
		{
			if (base.AdapterInternal != null)
			{
				base.AdapterInternal.OnPreRender(EventArgs.Empty);
				return;
			}
			this.OnPreRender(EventArgs.Empty);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x00065874 File Offset: 0x00063A74
		internal override void UnloadRecursive(bool dispose)
		{
			if (base.AdapterInternal != null)
			{
				base.AdapterInternal.OnUnload(EventArgs.Empty);
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

		// Token: 0x04001ABA RID: 6842
		internal string _text;
	}
}
