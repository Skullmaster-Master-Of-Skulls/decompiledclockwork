using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020001D0 RID: 464
	public sealed class LoadedEventArgs : EventArgs
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x0005F111 File Offset: 0x0005D311
		public LoadedEventArgs(bool succeeded, ICollection errors)
		{
			this._succeeded = succeeded;
			this._errors = errors;
			if (this._errors == null)
			{
				this._errors = new object[0];
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0005F13B File Offset: 0x0005D33B
		public ICollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0005F143 File Offset: 0x0005D343
		public bool HasSucceeded
		{
			get
			{
				return this._succeeded;
			}
		}

		// Token: 0x040009B3 RID: 2483
		private bool _succeeded;

		// Token: 0x040009B4 RID: 2484
		private ICollection _errors;
	}
}
