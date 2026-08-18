using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001106 RID: 4358
	internal class ValidateFieldEventArgs : EventArgs
	{
		// Token: 0x0600B248 RID: 45640 RVA: 0x0026D1B5 File Offset: 0x0026B3B5
		public ValidateFieldEventArgs(GridGroupByField newField)
		{
			this._newField = newField;
		}

		// Token: 0x170039BD RID: 14781
		// (get) Token: 0x0600B249 RID: 45641 RVA: 0x0026D1C4 File Offset: 0x0026B3C4
		public GridGroupByField NewField
		{
			get
			{
				return this._newField;
			}
		}

		// Token: 0x04002EFB RID: 12027
		private GridGroupByField _newField;
	}
}
