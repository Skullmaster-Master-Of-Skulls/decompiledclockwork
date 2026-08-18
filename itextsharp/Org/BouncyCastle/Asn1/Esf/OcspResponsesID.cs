using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x020000AC RID: 172
	public class OcspResponsesID : Asn1Encodable
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0001C31C File Offset: 0x0001B31C
		public static OcspResponsesID GetInstance(object obj)
		{
			if (obj == null || obj is OcspResponsesID)
			{
				return (OcspResponsesID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspResponsesID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspResponsesID' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001C370 File Offset: 0x0001B370
		private OcspResponsesID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspIdentifier = OcspIdentifier.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.ocspRepHash = OtherHash.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001C3FA File Offset: 0x0001B3FA
		public OcspResponsesID(OcspIdentifier ocspIdentifier) : this(ocspIdentifier, null)
		{
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001C404 File Offset: 0x0001B404
		public OcspResponsesID(OcspIdentifier ocspIdentifier, OtherHash ocspRepHash)
		{
			if (ocspIdentifier == null)
			{
				throw new ArgumentNullException("ocspIdentifier");
			}
			this.ocspIdentifier = ocspIdentifier;
			this.ocspRepHash = ocspRepHash;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001C428 File Offset: 0x0001B428
		public OcspIdentifier OcspIdentifier
		{
			get
			{
				return this.ocspIdentifier;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001C430 File Offset: 0x0001B430
		public OtherHash OcspRepHash
		{
			get
			{
				return this.ocspRepHash;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001C438 File Offset: 0x0001B438
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.ocspIdentifier.ToAsn1Object()
			});
			if (this.ocspRepHash != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.ocspRepHash.ToAsn1Object()
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040002A9 RID: 681
		private readonly OcspIdentifier ocspIdentifier;

		// Token: 0x040002AA RID: 682
		private readonly OtherHash ocspRepHash;
	}
}
