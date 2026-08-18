using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D7 RID: 727
	public abstract class ToolAdapter
	{
		// Token: 0x06001943 RID: 6467 RVA: 0x000530F7 File Offset: 0x000512F7
		protected ToolAdapter()
		{
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x000530FF File Offset: 0x000512FF
		protected ToolAdapter(RadEditor editor)
		{
			this.Editor = editor;
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x0005310E File Offset: 0x0005130E
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x00053116 File Offset: 0x00051316
		public RadEditor Editor
		{
			get
			{
				return this._editor;
			}
			set
			{
				this._editor = value;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001947 RID: 6471
		public abstract string ClientType { get; }

		// Token: 0x06001948 RID: 6472
		public abstract void PreRender();

		// Token: 0x06001949 RID: 6473
		public abstract void Render(HtmlTextWriter writer);

		// Token: 0x0600194A RID: 6474 RVA: 0x00053120 File Offset: 0x00051320
		public virtual List<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>();
		}

		// Token: 0x0400068F RID: 1679
		private RadEditor _editor;
	}
}
