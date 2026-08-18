using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020000A0 RID: 160
	public class Pbkdf2Params : Asn1Encodable
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0001B494 File Offset: 0x0001A494
		public static Pbkdf2Params GetInstance(object obj)
		{
			if (obj == null || obj is Pbkdf2Params)
			{
				return (Pbkdf2Params)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Pbkdf2Params((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001B4E8 File Offset: 0x0001A4E8
		public Pbkdf2Params(Asn1Sequence seq)
		{
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.octStr = (Asn1OctetString)seq[0];
			this.iterationCount = (DerInteger)seq[1];
			if (seq.Count > 2)
			{
				this.keyLength = (DerInteger)seq[2];
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001B55C File Offset: 0x0001A55C
		public Pbkdf2Params(byte[] salt, int iterationCount)
		{
			this.octStr = new DerOctetString(salt);
			this.iterationCount = new DerInteger(iterationCount);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001B57C File Offset: 0x0001A57C
		public byte[] GetSalt()
		{
			return this.octStr.GetOctets();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001B589 File Offset: 0x0001A589
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount.Value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001B596 File Offset: 0x0001A596
		public BigInteger KeyLength
		{
			get
			{
				if (this.keyLength != null)
				{
					return this.keyLength.Value;
				}
				return null;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001B5B0 File Offset: 0x0001A5B0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.octStr,
				this.iterationCount
			});
			if (this.keyLength != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.keyLength
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400028D RID: 653
		private readonly Asn1OctetString octStr;

		// Token: 0x0400028E RID: 654
		private readonly DerInteger iterationCount;

		// Token: 0x0400028F RID: 655
		private readonly DerInteger keyLength;
	}
}
