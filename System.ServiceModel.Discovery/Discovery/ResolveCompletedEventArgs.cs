using System;
using System.ComponentModel;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000046 RID: 70
	public class ResolveCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600036C RID: 876 RVA: 0x00009DED File Offset: 0x00007FED
		internal ResolveCompletedEventArgs(Exception error, bool cancelled, object userState, ResolveResponse result) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00009E00 File Offset: 0x00008000
		public ResolveResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.result;
			}
		}

		// Token: 0x040000E9 RID: 233
		private ResolveResponse result;
	}
}
