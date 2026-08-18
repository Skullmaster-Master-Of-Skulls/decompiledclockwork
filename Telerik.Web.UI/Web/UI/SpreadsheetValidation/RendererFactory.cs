using System;

namespace Telerik.Web.UI.SpreadsheetValidation
{
	// Token: 0x020008E0 RID: 2272
	internal class RendererFactory
	{
		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x0600558C RID: 21900 RVA: 0x00106B17 File Offset: 0x00104D17
		public ValidationTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x00106B1F File Offset: 0x00104D1F
		public RendererFactory(ValidationTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00106B2E File Offset: 0x00104D2E
		public IValidationRenderer CreateRenderer()
		{
			return new Renderer(this.Owner.View);
		}

		// Token: 0x04001503 RID: 5379
		private readonly ValidationTemplate _owner;
	}
}
