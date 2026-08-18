using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200038F RID: 911
	public class RecipientEncryptedKey : Asn1Encodable
	{
		// Token: 0x06001FB3 RID: 8115 RVA: 0x000BCBF0 File Offset: 0x000BBBF0
		private RecipientEncryptedKey(Asn1Sequence seq)
		{
			this.identifier = KeyAgreeRecipientIdentifier.GetInstance(seq[0]);
			this.encryptedKey = (Asn1OctetString)seq[1];
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000BCC1C File Offset: 0x000BBC1C
		public static RecipientEncryptedKey GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return RecipientEncryptedKey.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000BCC2C File Offset: 0x000BBC2C
		public static RecipientEncryptedKey GetInstance(object obj)
		{
			if (obj == null || obj is RecipientEncryptedKey)
			{
				return (RecipientEncryptedKey)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RecipientEncryptedKey((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RecipientEncryptedKey: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000BCC7E File Offset: 0x000BBC7E
		public RecipientEncryptedKey(KeyAgreeRecipientIdentifier id, Asn1OctetString encryptedKey)
		{
			this.identifier = id;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x000BCC94 File Offset: 0x000BBC94
		public KeyAgreeRecipientIdentifier Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x000BCC9C File Offset: 0x000BBC9C
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000BCCA4 File Offset: 0x000BBCA4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.identifier,
				this.encryptedKey
			});
		}

		// Token: 0x040015DF RID: 5599
		private readonly KeyAgreeRecipientIdentifier identifier;

		// Token: 0x040015E0 RID: 5600
		private readonly Asn1OctetString encryptedKey;
	}
}
