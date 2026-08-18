using System;
using System.Collections;
using Org.BouncyCastle.Bcpg.Sig;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020005A1 RID: 1441
	public class PgpSignatureSubpacketGenerator
	{
		// Token: 0x06003184 RID: 12676 RVA: 0x00135450 File Offset: 0x00134450
		public void SetRevocable(bool isCritical, bool isRevocable)
		{
			this.list.Add(new Revocable(isCritical, isRevocable));
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x00135465 File Offset: 0x00134465
		public void SetExportable(bool isCritical, bool isExportable)
		{
			this.list.Add(new Exportable(isCritical, isExportable));
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x0013547A File Offset: 0x0013447A
		public void SetTrust(bool isCritical, int depth, int trustAmount)
		{
			this.list.Add(new TrustSignature(isCritical, depth, trustAmount));
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x00135490 File Offset: 0x00134490
		public void SetKeyExpirationTime(bool isCritical, long seconds)
		{
			this.list.Add(new KeyExpirationTime(isCritical, seconds));
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x001354A5 File Offset: 0x001344A5
		public void SetSignatureExpirationTime(bool isCritical, long seconds)
		{
			this.list.Add(new SignatureExpirationTime(isCritical, seconds));
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x001354BA File Offset: 0x001344BA
		public void SetSignatureCreationTime(bool isCritical, DateTime date)
		{
			this.list.Add(new SignatureCreationTime(isCritical, date));
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x001354CF File Offset: 0x001344CF
		public void SetPreferredHashAlgorithms(bool isCritical, int[] algorithms)
		{
			this.list.Add(new PreferredAlgorithms(SignatureSubpacketTag.PreferredHashAlgorithms, isCritical, algorithms));
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x001354E6 File Offset: 0x001344E6
		public void SetPreferredSymmetricAlgorithms(bool isCritical, int[] algorithms)
		{
			this.list.Add(new PreferredAlgorithms(SignatureSubpacketTag.PreferredSymmetricAlgorithms, isCritical, algorithms));
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x001354FD File Offset: 0x001344FD
		public void SetPreferredCompressionAlgorithms(bool isCritical, int[] algorithms)
		{
			this.list.Add(new PreferredAlgorithms(SignatureSubpacketTag.PreferredCompressionAlgorithms, isCritical, algorithms));
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x00135514 File Offset: 0x00134514
		public void SetKeyFlags(bool isCritical, int flags)
		{
			this.list.Add(new KeyFlags(isCritical, flags));
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x00135529 File Offset: 0x00134529
		public void SetSignerUserId(bool isCritical, string userId)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("userId");
			}
			this.list.Add(new SignerUserId(isCritical, userId));
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0013554C File Offset: 0x0013454C
		public void SetEmbeddedSignature(bool isCritical, PgpSignature pgpSignature)
		{
			byte[] encoded = pgpSignature.GetEncoded();
			byte[] array;
			if (encoded.Length - 1 > 256)
			{
				array = new byte[encoded.Length - 3];
			}
			else
			{
				array = new byte[encoded.Length - 2];
			}
			Array.Copy(encoded, encoded.Length - array.Length, array, 0, array.Length);
			this.list.Add(new EmbeddedSignature(isCritical, array));
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x001355A9 File Offset: 0x001345A9
		public void SetPrimaryUserId(bool isCritical, bool isPrimaryUserId)
		{
			this.list.Add(new PrimaryUserId(isCritical, isPrimaryUserId));
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x001355BE File Offset: 0x001345BE
		public void SetNotationData(bool isCritical, bool isHumanReadable, string notationName, string notationValue)
		{
			this.list.Add(new NotationData(isCritical, isHumanReadable, notationName, notationValue));
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x001355D6 File Offset: 0x001345D6
		public PgpSignatureSubpacketVector Generate()
		{
			return new PgpSignatureSubpacketVector((SignatureSubpacket[])this.list.ToArray(typeof(SignatureSubpacket)));
		}

		// Token: 0x04002220 RID: 8736
		private ArrayList list = new ArrayList();
	}
}
