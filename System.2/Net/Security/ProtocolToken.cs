using System;
using System.ComponentModel;

namespace System.Net.Security
{
	// Token: 0x02000351 RID: 849
	internal class ProtocolToken
	{
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0008FC2B File Offset: 0x0008DE2B
		internal bool Failed
		{
			get
			{
				return this.Status != SecurityStatus.OK && this.Status != SecurityStatus.ContinueNeeded;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0008FC47 File Offset: 0x0008DE47
		internal bool Done
		{
			get
			{
				return this.Status == SecurityStatus.OK;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0008FC52 File Offset: 0x0008DE52
		internal bool Renegotiate
		{
			get
			{
				return this.Status == SecurityStatus.Renegotiate;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0008FC61 File Offset: 0x0008DE61
		internal bool CloseConnection
		{
			get
			{
				return this.Status == SecurityStatus.ContextExpired;
			}
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0008FC70 File Offset: 0x0008DE70
		internal ProtocolToken(byte[] data, SecurityStatus errorCode)
		{
			this.Status = errorCode;
			this.Payload = data;
			this.Size = ((data != null) ? data.Length : 0);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x0008FC95 File Offset: 0x0008DE95
		internal Win32Exception GetException()
		{
			if (!this.Done)
			{
				return new Win32Exception((int)this.Status);
			}
			return null;
		}

		// Token: 0x04001CE9 RID: 7401
		internal SecurityStatus Status;

		// Token: 0x04001CEA RID: 7402
		internal byte[] Payload;

		// Token: 0x04001CEB RID: 7403
		internal int Size;
	}
}
