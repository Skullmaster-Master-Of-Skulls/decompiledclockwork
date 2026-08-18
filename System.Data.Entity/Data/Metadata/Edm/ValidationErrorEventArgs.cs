using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001BE RID: 446
	internal class ValidationErrorEventArgs : EventArgs
	{
		// Token: 0x06001F0F RID: 7951 RVA: 0x0006D8C5 File Offset: 0x0006BAC5
		public ValidationErrorEventArgs(EdmItemError validationError)
		{
			this._validationError = validationError;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0006D8D4 File Offset: 0x0006BAD4
		public EdmItemError ValidationError
		{
			get
			{
				return this._validationError;
			}
		}

		// Token: 0x04000D12 RID: 3346
		private EdmItemError _validationError;
	}
}
