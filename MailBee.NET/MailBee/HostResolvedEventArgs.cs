using System;
using System.Net;
using a;

namespace MailBee
{
	// Token: 0x02000039 RID: 57
	public class HostResolvedEventArgs : CommonEventArgs
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00007EE0 File Offset: 0x00006EE0
		internal HostResolvedEventArgs(IPHostEntry A_0, bc A_1) : base(A_1)
		{
			this.a = A_0;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007EF0 File Offset: 0x00006EF0
		public IPHostEntry RemoteHost
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x04000159 RID: 345
		private IPHostEntry a;
	}
}
