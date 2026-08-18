using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000144 RID: 324
	public class EncryptedData : Asn1Encodable
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x00041408 File Offset: 0x00040408
		public static EncryptedData GetInstance(object obj)
		{
			if (obj is EncryptedData)
			{
				return (EncryptedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00041458 File Offset: 0x00040458
		private EncryptedData(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			int intValue = ((DerInteger)seq[0]).Value.IntValue;
			if (intValue != 0)
			{
				throw new ArgumentException("sequence not version 0");
			}
			this.data = (Asn1Sequence)seq[1];
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000414BC File Offset: 0x000404BC
		public EncryptedData(DerObjectIdentifier contentType, AlgorithmIdentifier encryptionAlgorithm, Asn1Encodable content)
		{
			this.data = new BerSequence(new Asn1Encodable[]
			{
				contentType,
				encryptionAlgorithm.ToAsn1Object(),
				new BerTaggedObject(false, 0, content)
			});
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000414FA File Offset: 0x000404FA
		public DerObjectIdentifier ContentType
		{
			get
			{
				return (DerObjectIdentifier)this.data[0];
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0004150D File Offset: 0x0004050D
		public AlgorithmIdentifier EncryptionAlgorithm
		{
			get
			{
				return AlgorithmIdentifier.GetInstance(this.data[1]);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00041520 File Offset: 0x00040520
		public Asn1OctetString Content
		{
			get
			{
				if (this.data.Count == 3)
				{
					DerTaggedObject derTaggedObject = (DerTaggedObject)this.data[2];
					return Asn1OctetString.GetInstance(derTaggedObject.GetObject());
				}
				return null;
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0004155C File Offset: 0x0004055C
		public override Asn1Object ToAsn1Object()
		{
			return new BerSequence(new Asn1Encodable[]
			{
				new DerInteger(0),
				this.data
			});
		}

		// Token: 0x0400092A RID: 2346
		private readonly Asn1Sequence data;
	}
}
