using System;
using System.ComponentModel;

namespace System.Net.Security
{
	// Token: 0x02000537 RID: 1335
	internal class ProtocolToken
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x000A9998 File Offset: 0x000A8998
		internal bool Failed
		{
			get
			{
				return this.Status != SecurityStatus.OK && this.Status != SecurityStatus.ContinueNeeded;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x000A99B4 File Offset: 0x000A89B4
		internal bool Done
		{
			get
			{
				return this.Status == SecurityStatus.OK;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x000A99BF File Offset: 0x000A89BF
		internal bool Renegotiate
		{
			get
			{
				return this.Status == SecurityStatus.Renegotiate;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060028DB RID: 10459 RVA: 0x000A99CE File Offset: 0x000A89CE
		internal bool CloseConnection
		{
			get
			{
				return this.Status == SecurityStatus.ContextExpired;
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000A99DD File Offset: 0x000A89DD
		internal ProtocolToken(byte[] data, SecurityStatus errorCode)
		{
			this.Status = errorCode;
			this.Payload = data;
			this.Size = ((data != null) ? data.Length : 0);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000A9A02 File Offset: 0x000A8A02
		internal Win32Exception GetException()
		{
			if (!this.Done)
			{
				return new Win32Exception((int)this.Status);
			}
			return null;
		}

		// Token: 0x040027C6 RID: 10182
		internal SecurityStatus Status;

		// Token: 0x040027C7 RID: 10183
		internal byte[] Payload;

		// Token: 0x040027C8 RID: 10184
		internal int Size;
	}
}
