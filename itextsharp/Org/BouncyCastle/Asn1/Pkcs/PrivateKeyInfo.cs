using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000577 RID: 1399
	public class PrivateKeyInfo : Asn1Encodable
	{
		// Token: 0x06002FBE RID: 12222 RVA: 0x001273CC File Offset: 0x001263CC
		public static PrivateKeyInfo GetInstance(object obj)
		{
			if (obj is PrivateKeyInfo || obj == null)
			{
				return (PrivateKeyInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PrivateKeyInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x0012741E File Offset: 0x0012641E
		public PrivateKeyInfo(AlgorithmIdentifier algID, Asn1Object privateKey) : this(algID, privateKey, null)
		{
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x00127429 File Offset: 0x00126429
		public PrivateKeyInfo(AlgorithmIdentifier algID, Asn1Object privateKey, Asn1Set attributes)
		{
			this.privKey = privateKey;
			this.algID = algID;
			this.attributes = attributes;
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00127448 File Offset: 0x00126448
		private PrivateKeyInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			BigInteger value = ((DerInteger)enumerator.Current).Value;
			if (value.IntValue != 0)
			{
				throw new ArgumentException("wrong version for private key info");
			}
			enumerator.MoveNext();
			this.algID = AlgorithmIdentifier.GetInstance(enumerator.Current);
			try
			{
				enumerator.MoveNext();
				Asn1OctetString asn1OctetString = (Asn1OctetString)enumerator.Current;
				this.privKey = Asn1Object.FromByteArray(asn1OctetString.GetOctets());
			}
			catch (IOException)
			{
				throw new ArgumentException("Error recoverying private key from sequence");
			}
			if (enumerator.MoveNext())
			{
				this.attributes = Asn1Set.GetInstance((Asn1TaggedObject)enumerator.Current, false);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x00127508 File Offset: 0x00126508
		public AlgorithmIdentifier AlgorithmID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x00127510 File Offset: 0x00126510
		public Asn1Object PrivateKey
		{
			get
			{
				return this.privKey;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x00127518 File Offset: 0x00126518
		public Asn1Set Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00127520 File Offset: 0x00126520
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(0),
				this.algID,
				new DerOctetString(this.privKey)
			});
			if (this.attributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.attributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040020D3 RID: 8403
		private readonly Asn1Object privKey;

		// Token: 0x040020D4 RID: 8404
		private readonly AlgorithmIdentifier algID;

		// Token: 0x040020D5 RID: 8405
		private readonly Asn1Set attributes;
	}
}
