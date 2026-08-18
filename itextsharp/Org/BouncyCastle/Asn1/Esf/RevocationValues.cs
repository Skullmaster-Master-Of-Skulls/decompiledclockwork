using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200014D RID: 333
	public class RevocationValues : Asn1Encodable
	{
		// Token: 0x06000BEF RID: 3055 RVA: 0x0004235C File Offset: 0x0004135C
		public static RevocationValues GetInstance(object obj)
		{
			if (obj == null || obj is RevocationValues)
			{
				return (RevocationValues)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevocationValues((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'RevocationValues' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000423B0 File Offset: 0x000413B0
		private RevocationValues(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			bool flag = false;
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				Asn1Object @object = asn1TaggedObject.GetObject();
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)@object;
					foreach (object obj2 in asn1Sequence)
					{
						Asn1Encodable asn1Encodable = (Asn1Encodable)obj2;
						CertificateList.GetInstance(asn1Encodable.ToAsn1Object());
					}
					this.crlVals = asn1Sequence;
					break;
				}
				case 1:
				{
					Asn1Sequence asn1Sequence2 = (Asn1Sequence)@object;
					foreach (object obj3 in asn1Sequence2)
					{
						Asn1Encodable asn1Encodable2 = (Asn1Encodable)obj3;
						BasicOcspResponse.GetInstance(asn1Encodable2.ToAsn1Object());
					}
					this.ocspVals = asn1Sequence2;
					break;
				}
				case 2:
					this.otherRevVals = OtherRevVals.GetInstance(@object);
					flag = true;
					break;
				default:
					throw new ArgumentException("Illegal tag in RevocationValues", "seq");
				}
			}
			if (!flag)
			{
				throw new ArgumentException("No otherRevVals found", "seq");
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00042598 File Offset: 0x00041598
		public RevocationValues(CertificateList[] crlVals, BasicOcspResponse[] ocspVals, OtherRevVals otherRevVals)
		{
			if (otherRevVals == null)
			{
				throw new ArgumentNullException("otherRevVals");
			}
			if (crlVals != null)
			{
				this.crlVals = new DerSequence(crlVals);
			}
			if (ocspVals != null)
			{
				this.ocspVals = new DerSequence(ocspVals);
			}
			this.otherRevVals = otherRevVals;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000425D4 File Offset: 0x000415D4
		public RevocationValues(IEnumerable crlVals, IEnumerable ocspVals, OtherRevVals otherRevVals)
		{
			if (otherRevVals == null)
			{
				throw new ArgumentNullException("otherRevVals");
			}
			if (crlVals != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(crlVals, typeof(CertificateList)))
				{
					throw new ArgumentException("Must contain only 'CertificateList' objects", "crlVals");
				}
				this.crlVals = new DerSequence(Asn1EncodableVector.FromEnumerable(crlVals));
			}
			if (ocspVals != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(ocspVals, typeof(BasicOcspResponse)))
				{
					throw new ArgumentException("Must contain only 'BasicOcspResponse' objects", "ocspVals");
				}
				this.ocspVals = new DerSequence(Asn1EncodableVector.FromEnumerable(ocspVals));
			}
			this.otherRevVals = otherRevVals;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00042668 File Offset: 0x00041668
		public CertificateList[] GetCrlVals()
		{
			CertificateList[] array = new CertificateList[this.crlVals.Count];
			for (int i = 0; i < this.crlVals.Count; i++)
			{
				array[i] = CertificateList.GetInstance(this.crlVals[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000426B8 File Offset: 0x000416B8
		public BasicOcspResponse[] GetOcspVals()
		{
			BasicOcspResponse[] array = new BasicOcspResponse[this.ocspVals.Count];
			for (int i = 0; i < this.ocspVals.Count; i++)
			{
				array[i] = BasicOcspResponse.GetInstance(this.ocspVals[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00042708 File Offset: 0x00041708
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.crlVals != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.crlVals)
				});
			}
			if (this.ocspVals != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.ocspVals)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerTaggedObject(true, 2, this.otherRevVals.ToAsn1Object())
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400097F RID: 2431
		private readonly Asn1Sequence crlVals;

		// Token: 0x04000980 RID: 2432
		private readonly Asn1Sequence ocspVals;

		// Token: 0x04000981 RID: 2433
		private readonly OtherRevVals otherRevVals;
	}
}
