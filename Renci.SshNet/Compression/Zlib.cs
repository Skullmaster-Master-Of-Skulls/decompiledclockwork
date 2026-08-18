using System;

namespace Renci.SshNet.Compression
{
	// Token: 0x020000E0 RID: 224
	internal class Zlib : Compressor
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000203B4 File Offset: 0x0001E5B4
		public override string Name
		{
			get
			{
				return "zlib";
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000203BB File Offset: 0x0001E5BB
		public override void Init(Session session)
		{
			base.Init(session);
			base.IsActive = true;
		}
	}
}
