using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000A3D RID: 2621
	public class CaptchaValidateEventArgs : EventArgs
	{
		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x060063F0 RID: 25584 RVA: 0x00177A12 File Offset: 0x00175C12
		// (set) Token: 0x060063F1 RID: 25585 RVA: 0x00177A1A File Offset: 0x00175C1A
		public bool CancelDefaultValidation
		{
			get
			{
				return this.cancelDefaultValidation;
			}
			set
			{
				this.cancelDefaultValidation = value;
			}
		}

		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x060063F2 RID: 25586 RVA: 0x00177A23 File Offset: 0x00175C23
		// (set) Token: 0x060063F3 RID: 25587 RVA: 0x00177A2B File Offset: 0x00175C2B
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x0400184C RID: 6220
		private bool cancelDefaultValidation;

		// Token: 0x0400184D RID: 6221
		private bool isValid;
	}
}
