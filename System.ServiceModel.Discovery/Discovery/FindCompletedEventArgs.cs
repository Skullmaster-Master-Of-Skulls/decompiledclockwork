using System;
using System.ComponentModel;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200002F RID: 47
	public class FindCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000286 RID: 646 RVA: 0x00007E30 File Offset: 0x00006030
		internal FindCompletedEventArgs(Exception error, bool cancelled, object userState, FindResponse result) : base(error, cancelled, userState)
		{
			this.result = result;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00007E43 File Offset: 0x00006043
		public FindResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.result;
			}
		}

		// Token: 0x04000093 RID: 147
		private FindResponse result;
	}
}
