using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000527 RID: 1319
	internal class ValidationErrorEventArgs : EventArgs
	{
		// Token: 0x06003216 RID: 12822 RVA: 0x000EF1B1 File Offset: 0x000ED3B1
		public ValidationErrorEventArgs(EdmItemError validationError)
		{
			this._validationError = validationError;
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000EF1C0 File Offset: 0x000ED3C0
		public EdmItemError ValidationError
		{
			get
			{
				return this._validationError;
			}
		}

		// Token: 0x040012C8 RID: 4808
		private readonly EdmItemError _validationError;
	}
}
