using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000143 RID: 323
	public class RC2CbcParameter : Asn1Encodable
	{
		// Token: 0x06000BC1 RID: 3009 RVA: 0x000412C9 File Offset: 0x000402C9
		public static RC2CbcParameter GetInstance(object obj)
		{
			if (obj is Asn1Sequence)
			{
				return new RC2CbcParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000412FE File Offset: 0x000402FE
		public RC2CbcParameter(byte[] iv)
		{
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00041312 File Offset: 0x00040312
		public RC2CbcParameter(int parameterVersion, byte[] iv)
		{
			this.version = new DerInteger(parameterVersion);
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00041334 File Offset: 0x00040334
		private RC2CbcParameter(Asn1Sequence seq)
		{
			if (seq.Count == 1)
			{
				this.iv = (Asn1OctetString)seq[0];
				return;
			}
			this.version = (DerInteger)seq[0];
			this.iv = (Asn1OctetString)seq[1];
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00041387 File Offset: 0x00040387
		public BigInteger RC2ParameterVersion
		{
			get
			{
				if (this.version != null)
				{
					return this.version.Value;
				}
				return null;
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0004139E File Offset: 0x0004039E
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv.GetOctets());
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000413B0 File Offset: 0x000403B0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.version != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.version
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.iv
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000928 RID: 2344
		internal DerInteger version;

		// Token: 0x04000929 RID: 2345
		internal Asn1OctetString iv;
	}
}
