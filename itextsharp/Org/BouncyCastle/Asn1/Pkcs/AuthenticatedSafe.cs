using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000519 RID: 1305
	public class AuthenticatedSafe : Asn1Encodable
	{
		// Token: 0x06002C99 RID: 11417 RVA: 0x0010F280 File Offset: 0x0010E280
		public AuthenticatedSafe(Asn1Sequence seq)
		{
			this.info = new ContentInfo[seq.Count];
			for (int num = 0; num != this.info.Length; num++)
			{
				this.info[num] = ContentInfo.GetInstance(seq[num]);
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x0010F2CB File Offset: 0x0010E2CB
		public AuthenticatedSafe(ContentInfo[] info)
		{
			this.info = (ContentInfo[])info.Clone();
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x0010F2E4 File Offset: 0x0010E2E4
		public ContentInfo[] GetContentInfo()
		{
			return (ContentInfo[])this.info.Clone();
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x0010F2F6 File Offset: 0x0010E2F6
		public override Asn1Object ToAsn1Object()
		{
			return new BerSequence(this.info);
		}

		// Token: 0x04001EAC RID: 7852
		private readonly ContentInfo[] info;
	}
}
