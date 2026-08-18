using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020004D9 RID: 1241
	public class KeyAgreeRecipientIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06002A3F RID: 10815 RVA: 0x00100893 File Offset: 0x000FF893
		public static KeyAgreeRecipientIdentifier GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return KeyAgreeRecipientIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x001008A4 File Offset: 0x000FF8A4
		public static KeyAgreeRecipientIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is KeyAgreeRecipientIdentifier)
			{
				return (KeyAgreeRecipientIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyAgreeRecipientIdentifier(IssuerAndSerialNumber.GetInstance(obj));
			}
			if (obj is Asn1TaggedObject && ((Asn1TaggedObject)obj).TagNo == 0)
			{
				return new KeyAgreeRecipientIdentifier(RecipientKeyIdentifier.GetInstance((Asn1TaggedObject)obj, false));
			}
			throw new ArgumentException("Invalid KeyAgreeRecipientIdentifier: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x0010091D File Offset: 0x000FF91D
		public KeyAgreeRecipientIdentifier(IssuerAndSerialNumber issuerSerial)
		{
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x0010092C File Offset: 0x000FF92C
		public KeyAgreeRecipientIdentifier(RecipientKeyIdentifier rKeyID)
		{
			this.rKeyID = rKeyID;
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x0010093B File Offset: 0x000FF93B
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x00100943 File Offset: 0x000FF943
		public RecipientKeyIdentifier RKeyID
		{
			get
			{
				return this.rKeyID;
			}
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x0010094B File Offset: 0x000FF94B
		public override Asn1Object ToAsn1Object()
		{
			if (this.issuerSerial != null)
			{
				return this.issuerSerial.ToAsn1Object();
			}
			return new DerTaggedObject(false, 0, this.rKeyID);
		}

		// Token: 0x04001D76 RID: 7542
		private readonly IssuerAndSerialNumber issuerSerial;

		// Token: 0x04001D77 RID: 7543
		private readonly RecipientKeyIdentifier rKeyID;
	}
}
