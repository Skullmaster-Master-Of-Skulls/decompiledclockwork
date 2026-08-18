using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x02000568 RID: 1384
	public sealed class LoadedEventArgs : EventArgs
	{
		// Token: 0x060030D9 RID: 12505 RVA: 0x00114133 File Offset: 0x00113133
		public LoadedEventArgs(bool succeeded, ICollection errors)
		{
			this._succeeded = succeeded;
			this._errors = errors;
			if (this._errors == null)
			{
				this._errors = new object[0];
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x0011415D File Offset: 0x0011315D
		public ICollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x00114165 File Offset: 0x00113165
		public bool HasSucceeded
		{
			get
			{
				return this._succeeded;
			}
		}

		// Token: 0x040020BB RID: 8379
		private bool _succeeded;

		// Token: 0x040020BC RID: 8380
		private ICollection _errors;
	}
}
