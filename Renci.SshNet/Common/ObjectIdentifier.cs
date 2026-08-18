using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F7 RID: 247
	public struct ObjectIdentifier
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00024793 File Offset: 0x00022993
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0002479B File Offset: 0x0002299B
		public ulong[] Identifiers { get; private set; }

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000247A4 File Offset: 0x000229A4
		public ObjectIdentifier(params ulong[] identifiers)
		{
			this = default(ObjectIdentifier);
			if (identifiers.Length < 2)
			{
				throw new ArgumentException("identifiers");
			}
			this.Identifiers = identifiers;
		}
	}
}
