using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000562 RID: 1378
	public class BasicConstraints : Asn1Encodable
	{
		// Token: 0x06002F5B RID: 12123 RVA: 0x00126000 File Offset: 0x00125000
		public static BasicConstraints GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return BasicConstraints.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x00126010 File Offset: 0x00125010
		public static BasicConstraints GetInstance(object obj)
		{
			if (obj == null || obj is BasicConstraints)
			{
				return (BasicConstraints)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new BasicConstraints((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return BasicConstraints.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x0012607C File Offset: 0x0012507C
		private BasicConstraints(Asn1Sequence seq)
		{
			if (seq.Count > 0)
			{
				if (seq[0] is DerBoolean)
				{
					this.cA = DerBoolean.GetInstance(seq[0]);
				}
				else
				{
					this.pathLenConstraint = DerInteger.GetInstance(seq[0]);
				}
				if (seq.Count > 1)
				{
					if (this.cA == null)
					{
						throw new ArgumentException("wrong sequence in constructor", "seq");
					}
					this.pathLenConstraint = DerInteger.GetInstance(seq[1]);
				}
			}
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x001260FF File Offset: 0x001250FF
		public BasicConstraints(bool cA)
		{
			if (cA)
			{
				this.cA = DerBoolean.True;
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x00126115 File Offset: 0x00125115
		public BasicConstraints(int pathLenConstraint)
		{
			this.cA = DerBoolean.True;
			this.pathLenConstraint = new DerInteger(pathLenConstraint);
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x00126134 File Offset: 0x00125134
		public bool IsCA()
		{
			return this.cA != null && this.cA.IsTrue;
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x0012614B File Offset: 0x0012514B
		public BigInteger PathLenConstraint
		{
			get
			{
				if (this.pathLenConstraint != null)
				{
					return this.pathLenConstraint.Value;
				}
				return null;
			}
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x00126164 File Offset: 0x00125164
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.cA != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.cA
				});
			}
			if (this.pathLenConstraint != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.pathLenConstraint
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x001261C4 File Offset: 0x001251C4
		public override string ToString()
		{
			if (this.pathLenConstraint == null)
			{
				return "BasicConstraints: isCa(" + this.IsCA() + ")";
			}
			return string.Concat(new object[]
			{
				"BasicConstraints: isCa(",
				this.IsCA(),
				"), pathLenConstraint = ",
				this.pathLenConstraint.Value
			});
		}

		// Token: 0x040020A9 RID: 8361
		private readonly DerBoolean cA;

		// Token: 0x040020AA RID: 8362
		private readonly DerInteger pathLenConstraint;
	}
}
