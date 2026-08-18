using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000401 RID: 1025
	public class IetfAttrSyntax : Asn1Encodable
	{
		// Token: 0x06002304 RID: 8964 RVA: 0x000D7D00 File Offset: 0x000D6D00
		public IetfAttrSyntax(Asn1Sequence seq)
		{
			int num = 0;
			if (seq[0] is Asn1TaggedObject)
			{
				this.policyAuthority = GeneralNames.GetInstance((Asn1TaggedObject)seq[0], false);
				num++;
			}
			else if (seq.Count == 2)
			{
				this.policyAuthority = GeneralNames.GetInstance(seq[0]);
				num++;
			}
			if (!(seq[num] is Asn1Sequence))
			{
				throw new ArgumentException("Non-IetfAttrSyntax encoding");
			}
			seq = (Asn1Sequence)seq[num];
			foreach (object obj in seq)
			{
				Asn1Object asn1Object = (Asn1Object)obj;
				int num2;
				if (asn1Object is DerObjectIdentifier)
				{
					num2 = 2;
				}
				else if (asn1Object is DerUtf8String)
				{
					num2 = 3;
				}
				else
				{
					if (!(asn1Object is DerOctetString))
					{
						throw new ArgumentException("Bad value type encoding IetfAttrSyntax");
					}
					num2 = 1;
				}
				if (this.valueChoice < 0)
				{
					this.valueChoice = num2;
				}
				if (num2 != this.valueChoice)
				{
					throw new ArgumentException("Mix of value types in IetfAttrSyntax");
				}
				this.values.Add(new Asn1Encodable[]
				{
					asn1Object
				});
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000D7E54 File Offset: 0x000D6E54
		public GeneralNames PolicyAuthority
		{
			get
			{
				return this.policyAuthority;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000D7E5C File Offset: 0x000D6E5C
		public int ValueType
		{
			get
			{
				return this.valueChoice;
			}
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000D7E64 File Offset: 0x000D6E64
		public object[] GetValues()
		{
			if (this.ValueType == 1)
			{
				Asn1OctetString[] array = new Asn1OctetString[this.values.Count];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = (Asn1OctetString)this.values[num];
				}
				return array;
			}
			if (this.ValueType == 2)
			{
				DerObjectIdentifier[] array2 = new DerObjectIdentifier[this.values.Count];
				for (int num2 = 0; num2 != array2.Length; num2++)
				{
					array2[num2] = (DerObjectIdentifier)this.values[num2];
				}
				return array2;
			}
			DerUtf8String[] array3 = new DerUtf8String[this.values.Count];
			for (int num3 = 0; num3 != array3.Length; num3++)
			{
				array3[num3] = (DerUtf8String)this.values[num3];
			}
			return array3;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000D7F2C File Offset: 0x000D6F2C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.policyAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.policyAuthority)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(this.values)
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040017D9 RID: 6105
		public const int ValueOctets = 1;

		// Token: 0x040017DA RID: 6106
		public const int ValueOid = 2;

		// Token: 0x040017DB RID: 6107
		public const int ValueUtf8 = 3;

		// Token: 0x040017DC RID: 6108
		internal readonly GeneralNames policyAuthority;

		// Token: 0x040017DD RID: 6109
		internal readonly Asn1EncodableVector values = new Asn1EncodableVector(new Asn1Encodable[0]);

		// Token: 0x040017DE RID: 6110
		internal int valueChoice = -1;
	}
}
