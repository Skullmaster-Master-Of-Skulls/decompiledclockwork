using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000304 RID: 772
	public class X9Curve : Asn1Encodable
	{
		// Token: 0x06001C3B RID: 7227 RVA: 0x000A8C5B File Offset: 0x000A7C5B
		public X9Curve(ECCurve curve) : this(curve, null)
		{
			this.curve = curve;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000A8C6C File Offset: 0x000A7C6C
		public X9Curve(ECCurve curve, byte[] seed)
		{
			if (curve == null)
			{
				throw new ArgumentNullException("curve");
			}
			this.curve = curve;
			this.seed = Arrays.Clone(seed);
			if (curve is FpCurve)
			{
				this.fieldIdentifier = X9ObjectIdentifiers.PrimeField;
				return;
			}
			if (curve is F2mCurve)
			{
				this.fieldIdentifier = X9ObjectIdentifiers.CharacteristicTwoField;
				return;
			}
			throw new ArgumentException("This type of ECCurve is not implemented");
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000A8CD4 File Offset: 0x000A7CD4
		public X9Curve(X9FieldID fieldID, Asn1Sequence seq)
		{
			if (fieldID == null)
			{
				throw new ArgumentNullException("fieldID");
			}
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			this.fieldIdentifier = fieldID.Identifier;
			if (this.fieldIdentifier.Equals(X9ObjectIdentifiers.PrimeField))
			{
				BigInteger value = ((DerInteger)fieldID.Parameters).Value;
				X9FieldElement x9FieldElement = new X9FieldElement(value, (Asn1OctetString)seq[0]);
				X9FieldElement x9FieldElement2 = new X9FieldElement(value, (Asn1OctetString)seq[1]);
				this.curve = new FpCurve(value, x9FieldElement.Value.ToBigInteger(), x9FieldElement2.Value.ToBigInteger());
			}
			else if (this.fieldIdentifier.Equals(X9ObjectIdentifiers.CharacteristicTwoField))
			{
				DerSequence derSequence = (DerSequence)fieldID.Parameters;
				int intValue = ((DerInteger)derSequence[0]).Value.IntValue;
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)derSequence[1];
				int k = 0;
				int k2 = 0;
				int intValue2;
				if (derObjectIdentifier.Equals(X9ObjectIdentifiers.TPBasis))
				{
					intValue2 = ((DerInteger)derSequence[2]).Value.IntValue;
				}
				else
				{
					DerSequence derSequence2 = (DerSequence)derSequence[2];
					intValue2 = ((DerInteger)derSequence2[0]).Value.IntValue;
					k = ((DerInteger)derSequence2[1]).Value.IntValue;
					k2 = ((DerInteger)derSequence2[2]).Value.IntValue;
				}
				X9FieldElement x9FieldElement3 = new X9FieldElement(intValue, intValue2, k, k2, (Asn1OctetString)seq[0]);
				X9FieldElement x9FieldElement4 = new X9FieldElement(intValue, intValue2, k, k2, (Asn1OctetString)seq[1]);
				this.curve = new F2mCurve(intValue, intValue2, k, k2, x9FieldElement3.Value.ToBigInteger(), x9FieldElement4.Value.ToBigInteger());
			}
			if (seq.Count == 3)
			{
				this.seed = ((DerBitString)seq[2]).GetBytes();
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x000A8ED1 File Offset: 0x000A7ED1
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000A8ED9 File Offset: 0x000A7ED9
		public byte[] GetSeed()
		{
			return Arrays.Clone(this.seed);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000A8EE8 File Offset: 0x000A7EE8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.fieldIdentifier.Equals(X9ObjectIdentifiers.PrimeField) || this.fieldIdentifier.Equals(X9ObjectIdentifiers.CharacteristicTwoField))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new X9FieldElement(this.curve.A).ToAsn1Object()
				});
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new X9FieldElement(this.curve.B).ToAsn1Object()
				});
			}
			if (this.seed != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerBitString(this.seed)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400135D RID: 4957
		private readonly ECCurve curve;

		// Token: 0x0400135E RID: 4958
		private readonly byte[] seed;

		// Token: 0x0400135F RID: 4959
		private readonly DerObjectIdentifier fieldIdentifier;
	}
}
