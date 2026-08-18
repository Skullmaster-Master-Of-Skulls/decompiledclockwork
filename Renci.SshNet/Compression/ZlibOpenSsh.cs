using System;
using Renci.SshNet.Messages.Authentication;

namespace Renci.SshNet.Compression
{
	// Token: 0x020000E1 RID: 225
	public class ZlibOpenSsh : Compressor
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x000203D3 File Offset: 0x0001E5D3
		public override string Name
		{
			get
			{
				return "zlib@openssh.org";
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000203DA File Offset: 0x0001E5DA
		public override void Init(Session session)
		{
			base.Init(session);
			session.UserAuthenticationSuccessReceived += this.Session_UserAuthenticationSuccessReceived;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000203F5 File Offset: 0x0001E5F5
		private void Session_UserAuthenticationSuccessReceived(object sender, MessageEventArgs<SuccessMessage> e)
		{
			base.IsActive = true;
			base.Session.UserAuthenticationSuccessReceived -= this.Session_UserAuthenticationSuccessReceived;
		}
	}
}
