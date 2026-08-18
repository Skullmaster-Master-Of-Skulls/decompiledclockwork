using System;

namespace Telerik.Web.UI.SpreadsheetValidation
{
	// Token: 0x020008E6 RID: 2278
	internal class ViewFactory : IViewFactory
	{
		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x0600561C RID: 22044 RVA: 0x00107917 File Offset: 0x00105B17
		public ValidationTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x0010791F File Offset: 0x00105B1F
		public ViewFactory(ValidationTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x0010792E File Offset: 0x00105B2E
		public IValidationView CreateView()
		{
			return new View(this.Owner);
		}

		// Token: 0x04001520 RID: 5408
		private readonly ValidationTemplate _owner;
	}
}
