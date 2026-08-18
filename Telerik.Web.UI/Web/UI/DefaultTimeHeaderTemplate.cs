using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200100D RID: 4109
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal sealed class DefaultTimeHeaderTemplate : ITemplate
	{
		// Token: 0x170032F8 RID: 13048
		// (get) Token: 0x0600A127 RID: 41255 RVA: 0x0023D9DA File Offset: 0x0023BBDA
		// (set) Token: 0x0600A128 RID: 41256 RVA: 0x0023D9E2 File Offset: 0x0023BBE2
		public string HeaderText
		{
			get
			{
				return this.headerText;
			}
			set
			{
				this.headerText = value;
			}
		}

		// Token: 0x0600A12A RID: 41258 RVA: 0x0023D9F3 File Offset: 0x0023BBF3
		void ITemplate.InstantiateIn(Control owner)
		{
			this.literal = new LiteralControl(this.HeaderText);
			owner.Controls.Add(this.literal);
		}

		// Token: 0x04002CFA RID: 11514
		private LiteralControl literal;

		// Token: 0x04002CFB RID: 11515
		private string headerText;
	}
}
