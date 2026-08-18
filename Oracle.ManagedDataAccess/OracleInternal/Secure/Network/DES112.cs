using System;
using \u0005;

namespace OracleInternal.Secure.Network
{
	// Token: 0x02000349 RID: 841
	public class DES112 : DES168
	{
		// Token: 0x06001DA2 RID: 7586 RVA: 0x00123054 File Offset: 0x00121254
		public override void init(byte[] key, byte[] iv)
		{
			if (key == null && iv == null)
			{
				throw new Exception(global::\u0005.\u0001.\u0001(528));
			}
			if (key.Length < 16)
			{
				throw new Exception(global::\u0005.\u0001.\u0001(528));
			}
			Array.Copy(key, 0, this.\u0006, 0, 8);
			Array.Copy(key, 8, this.\u0007, 0, 8);
			Array.Copy(this.\u0006, 0, this.\u0008, 0, 8);
			this.\u0015 = true;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x001230C8 File Offset: 0x001212C8
		public override void setSessionKey(byte[] key, byte[] iv)
		{
			this.\u0015 = true;
			if (key == null && iv == null)
			{
				if (this.\u0006 == null)
				{
					throw new Exception(global::\u0005.\u0001.\u0001(528));
				}
				return;
			}
			else
			{
				if (key.Length < 16)
				{
					throw new Exception(global::\u0005.\u0001.\u0001(528));
				}
				Array.Copy(key, 0, this.\u0006, 0, 8);
				Array.Copy(key, 8, this.\u0007, 0, 8);
				Array.Copy(this.\u0006, 0, this.\u0008, 0, 8);
				return;
			}
		}
	}
}
