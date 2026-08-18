using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020001AC RID: 428
	public class TstInfo : Asn1Encodable
	{
		// Token: 0x06001043 RID: 4163 RVA: 0x0005DCCC File Offset: 0x0005CCCC
		public static TstInfo GetInstance(object o)
		{
			if (o == null || o is TstInfo)
			{
				return (TstInfo)o;
			}
			if (o is Asn1Sequence)
			{
				return new TstInfo((Asn1Sequence)o);
			}
			if (o is Asn1OctetString)
			{
				try
				{
					byte[] octets = ((Asn1OctetString)o).GetOctets();
					return TstInfo.GetInstance(Asn1Object.FromByteArray(octets));
				}
				catch (IOException)
				{
					throw new ArgumentException("Bad object format in 'TstInfo' factory.");
				}
			}
			throw new ArgumentException("Unknown object in 'TstInfo' factory: " + o.GetType().FullName);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0005DD5C File Offset: 0x0005CD5C
		private TstInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.tsaPolicyId = DerObjectIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.messageImprint = MessageImprint.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.serialNumber = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.genTime = DerGeneralizedTime.GetInstance(enumerator.Current);
			this.ordering = DerBoolean.False;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1Object asn1Object = (Asn1Object)obj;
				if (asn1Object is Asn1TaggedObject)
				{
					DerTaggedObject derTaggedObject = (DerTaggedObject)asn1Object;
					switch (derTaggedObject.TagNo)
					{
					case 0:
						this.tsa = GeneralName.GetInstance(derTaggedObject, true);
						break;
					case 1:
						this.extensions = X509Extensions.GetInstance(derTaggedObject, false);
						break;
					default:
						throw new ArgumentException("Unknown tag value " + derTaggedObject.TagNo);
					}
				}
				if (asn1Object is DerSequence)
				{
					this.accuracy = Accuracy.GetInstance(asn1Object);
				}
				if (asn1Object is DerBoolean)
				{
					this.ordering = DerBoolean.GetInstance(asn1Object);
				}
				if (asn1Object is DerInteger)
				{
					this.nonce = DerInteger.GetInstance(asn1Object);
				}
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0005DEB0 File Offset: 0x0005CEB0
		public TstInfo(DerObjectIdentifier tsaPolicyId, MessageImprint messageImprint, DerInteger serialNumber, DerGeneralizedTime genTime, Accuracy accuracy, DerBoolean ordering, DerInteger nonce, GeneralName tsa, X509Extensions extensions)
		{
			this.version = new DerInteger(1);
			this.tsaPolicyId = tsaPolicyId;
			this.messageImprint = messageImprint;
			this.serialNumber = serialNumber;
			this.genTime = genTime;
			this.accuracy = accuracy;
			this.ordering = ordering;
			this.nonce = nonce;
			this.tsa = tsa;
			this.extensions = extensions;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0005DF14 File Offset: 0x0005CF14
		public MessageImprint MessageImprint
		{
			get
			{
				return this.messageImprint;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0005DF1C File Offset: 0x0005CF1C
		public DerObjectIdentifier Policy
		{
			get
			{
				return this.tsaPolicyId;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0005DF24 File Offset: 0x0005CF24
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0005DF2C File Offset: 0x0005CF2C
		public Accuracy Accuracy
		{
			get
			{
				return this.accuracy;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0005DF34 File Offset: 0x0005CF34
		public DerGeneralizedTime GenTime
		{
			get
			{
				return this.genTime;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0005DF3C File Offset: 0x0005CF3C
		public DerBoolean Ordering
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0005DF44 File Offset: 0x0005CF44
		public DerInteger Nonce
		{
			get
			{
				return this.nonce;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0005DF4C File Offset: 0x0005CF4C
		public GeneralName Tsa
		{
			get
			{
				return this.tsa;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0005DF54 File Offset: 0x0005CF54
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0005DF5C File Offset: 0x0005CF5C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.tsaPolicyId,
				this.messageImprint,
				this.serialNumber,
				this.genTime
			});
			if (this.accuracy != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.accuracy
				});
			}
			if (this.ordering != null && this.ordering.IsTrue)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.ordering
				});
			}
			if (this.nonce != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.nonce
				});
			}
			if (this.tsa != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.tsa)
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.extensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000BFB RID: 3067
		private readonly DerInteger version;

		// Token: 0x04000BFC RID: 3068
		private readonly DerObjectIdentifier tsaPolicyId;

		// Token: 0x04000BFD RID: 3069
		private readonly MessageImprint messageImprint;

		// Token: 0x04000BFE RID: 3070
		private readonly DerInteger serialNumber;

		// Token: 0x04000BFF RID: 3071
		private readonly DerGeneralizedTime genTime;

		// Token: 0x04000C00 RID: 3072
		private readonly Accuracy accuracy;

		// Token: 0x04000C01 RID: 3073
		private readonly DerBoolean ordering;

		// Token: 0x04000C02 RID: 3074
		private readonly DerInteger nonce;

		// Token: 0x04000C03 RID: 3075
		private readonly GeneralName tsa;

		// Token: 0x04000C04 RID: 3076
		private readonly X509Extensions extensions;
	}
}
