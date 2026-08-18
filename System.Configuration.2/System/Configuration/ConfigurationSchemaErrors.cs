using System;
using System.Collections.Generic;

namespace System.Configuration
{
	// Token: 0x02000037 RID: 55
	internal class ConfigurationSchemaErrors
	{
		// Token: 0x06000289 RID: 649 RVA: 0x000115BE File Offset: 0x0000F7BE
		internal ConfigurationSchemaErrors()
		{
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000115C6 File Offset: 0x0000F7C6
		internal bool HasLocalErrors
		{
			get
			{
				return ErrorsHelper.GetHasErrors(this._errorsLocal);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000115D3 File Offset: 0x0000F7D3
		internal bool HasGlobalErrors
		{
			get
			{
				return ErrorsHelper.GetHasErrors(this._errorsGlobal);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000115E0 File Offset: 0x0000F7E0
		private bool HasAllErrors
		{
			get
			{
				return ErrorsHelper.GetHasErrors(this._errorsAll);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000115ED File Offset: 0x0000F7ED
		internal int GlobalErrorCount
		{
			get
			{
				return ErrorsHelper.GetErrorCount(this._errorsGlobal);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000115FC File Offset: 0x0000F7FC
		internal void AddError(ConfigurationException ce, ExceptionAction action)
		{
			switch (action)
			{
			case ExceptionAction.NonSpecific:
				ErrorsHelper.AddError(ref this._errorsAll, ce);
				return;
			case ExceptionAction.Local:
				ErrorsHelper.AddError(ref this._errorsLocal, ce);
				return;
			case ExceptionAction.Global:
				ErrorsHelper.AddError(ref this._errorsAll, ce);
				ErrorsHelper.AddError(ref this._errorsGlobal, ce);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001164E File Offset: 0x0000F84E
		internal void SetSingleGlobalError(ConfigurationException ce)
		{
			this._errorsAll = null;
			this._errorsLocal = null;
			this._errorsGlobal = null;
			this.AddError(ce, ExceptionAction.Global);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001166D File Offset: 0x0000F86D
		internal bool HasErrors(bool ignoreLocal)
		{
			if (ignoreLocal)
			{
				return this.HasGlobalErrors;
			}
			return this.HasAllErrors;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001167F File Offset: 0x0000F87F
		internal void ThrowIfErrors(bool ignoreLocal)
		{
			if (!this.HasErrors(ignoreLocal))
			{
				return;
			}
			if (this.HasGlobalErrors)
			{
				throw new ConfigurationErrorsException(this._errorsGlobal);
			}
			throw new ConfigurationErrorsException(this._errorsAll);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000116AC File Offset: 0x0000F8AC
		internal List<ConfigurationException> RetrieveAndResetLocalErrors(bool keepLocalErrors)
		{
			List<ConfigurationException> errorsLocal = this._errorsLocal;
			this._errorsLocal = null;
			if (keepLocalErrors)
			{
				ErrorsHelper.AddErrors(ref this._errorsAll, errorsLocal);
			}
			return errorsLocal;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000116D7 File Offset: 0x0000F8D7
		internal void AddSavedLocalErrors(ICollection<ConfigurationException> coll)
		{
			ErrorsHelper.AddErrors(ref this._errorsAll, coll);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000116E5 File Offset: 0x0000F8E5
		internal void ResetLocalErrors()
		{
			this.RetrieveAndResetLocalErrors(false);
		}

		// Token: 0x04000205 RID: 517
		private List<ConfigurationException> _errorsLocal;

		// Token: 0x04000206 RID: 518
		private List<ConfigurationException> _errorsGlobal;

		// Token: 0x04000207 RID: 519
		private List<ConfigurationException> _errorsAll;
	}
}
