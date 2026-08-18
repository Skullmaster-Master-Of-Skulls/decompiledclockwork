using System;
using System.Collections;
using Org.BouncyCastle.Bcpg.Sig;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020002E8 RID: 744
	public class PgpSignatureSubpacketVector
	{
		// Token: 0x06001B87 RID: 7047 RVA: 0x000A55C4 File Offset: 0x000A45C4
		internal PgpSignatureSubpacketVector(SignatureSubpacket[] packets)
		{
			this.packets = packets;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000A55D4 File Offset: 0x000A45D4
		public SignatureSubpacket GetSubpacket(SignatureSubpacketTag type)
		{
			for (int num = 0; num != this.packets.Length; num++)
			{
				if (this.packets[num].SubpacketType == type)
				{
					return this.packets[num];
				}
			}
			return null;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x000A560E File Offset: 0x000A460E
		public bool HasSubpacket(SignatureSubpacketTag type)
		{
			return this.GetSubpacket(type) != null;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000A5620 File Offset: 0x000A4620
		public SignatureSubpacket[] GetSubpackets(SignatureSubpacketTag type)
		{
			ArrayList arrayList = new ArrayList();
			for (int num = 0; num != this.packets.Length; num++)
			{
				if (this.packets[num].SubpacketType == type)
				{
					arrayList.Add(this.packets[num]);
				}
			}
			return (SignatureSubpacket[])arrayList.ToArray(typeof(SignatureSubpacket));
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000A567C File Offset: 0x000A467C
		public NotationData[] GetNotationDataOccurences()
		{
			SignatureSubpacket[] subpackets = this.GetSubpackets(SignatureSubpacketTag.NotationData);
			NotationData[] array = new NotationData[subpackets.Length];
			for (int i = 0; i < subpackets.Length; i++)
			{
				array[i] = (NotationData)subpackets[i];
			}
			return array;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x000A56B8 File Offset: 0x000A46B8
		public long GetIssuerKeyId()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.IssuerKeyId);
			if (subpacket != null)
			{
				return ((IssuerKeyId)subpacket).KeyId;
			}
			return 0L;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x000A56DF File Offset: 0x000A46DF
		public bool HasSignatureCreationTime()
		{
			return this.GetSubpacket(SignatureSubpacketTag.CreationTime) != null;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x000A56F0 File Offset: 0x000A46F0
		public DateTime GetSignatureCreationTime()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.CreationTime);
			if (subpacket == null)
			{
				throw new PgpException("SignatureCreationTime not available");
			}
			return ((SignatureCreationTime)subpacket).GetTime();
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000A5720 File Offset: 0x000A4720
		public long GetSignatureExpirationTime()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.ExpireTime);
			if (subpacket != null)
			{
				return ((SignatureExpirationTime)subpacket).Time;
			}
			return 0L;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x000A5748 File Offset: 0x000A4748
		public long GetKeyExpirationTime()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.KeyExpireTime);
			if (subpacket != null)
			{
				return ((KeyExpirationTime)subpacket).Time;
			}
			return 0L;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000A5770 File Offset: 0x000A4770
		public int[] GetPreferredHashAlgorithms()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.PreferredHashAlgorithms);
			if (subpacket != null)
			{
				return ((PreferredAlgorithms)subpacket).GetPreferences();
			}
			return null;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000A5798 File Offset: 0x000A4798
		public int[] GetPreferredSymmetricAlgorithms()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.PreferredSymmetricAlgorithms);
			if (subpacket != null)
			{
				return ((PreferredAlgorithms)subpacket).GetPreferences();
			}
			return null;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000A57C0 File Offset: 0x000A47C0
		public int[] GetPreferredCompressionAlgorithms()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.PreferredCompressionAlgorithms);
			if (subpacket != null)
			{
				return ((PreferredAlgorithms)subpacket).GetPreferences();
			}
			return null;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000A57E8 File Offset: 0x000A47E8
		public int GetKeyFlags()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.KeyFlags);
			if (subpacket != null)
			{
				return ((KeyFlags)subpacket).Flags;
			}
			return 0;
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000A5810 File Offset: 0x000A4810
		public string GetSignerUserId()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.SignerUserId);
			if (subpacket != null)
			{
				return ((SignerUserId)subpacket).GetId();
			}
			return null;
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000A5838 File Offset: 0x000A4838
		public bool IsPrimaryUserId()
		{
			PrimaryUserId primaryUserId = (PrimaryUserId)this.GetSubpacket(SignatureSubpacketTag.PrimaryUserId);
			return primaryUserId != null && primaryUserId.IsPrimaryUserId();
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000A5860 File Offset: 0x000A4860
		public SignatureSubpacketTag[] GetCriticalTags()
		{
			int num = 0;
			for (int num2 = 0; num2 != this.packets.Length; num2++)
			{
				if (this.packets[num2].IsCritical())
				{
					num++;
				}
			}
			SignatureSubpacketTag[] array = new SignatureSubpacketTag[num];
			num = 0;
			for (int num3 = 0; num3 != this.packets.Length; num3++)
			{
				if (this.packets[num3].IsCritical())
				{
					array[num++] = this.packets[num3].SubpacketType;
				}
			}
			return array;
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000A58D5 File Offset: 0x000A48D5
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.packets.Length;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000A58DF File Offset: 0x000A48DF
		public int Count
		{
			get
			{
				return this.packets.Length;
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000A58E9 File Offset: 0x000A48E9
		internal SignatureSubpacket[] ToSubpacketArray()
		{
			return this.packets;
		}

		// Token: 0x040012F9 RID: 4857
		private readonly SignatureSubpacket[] packets;
	}
}
