using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020001BB RID: 443
	public class DerInteger : Asn1Object
	{
		// Token: 0x060010AE RID: 4270 RVA: 0x0005F1DC File Offset: 0x0005E1DC
		public static DerInteger GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			DerInteger derInteger = obj as DerInteger;
			if (derInteger != null)
			{
				return derInteger;
			}
			Asn1OctetString asn1OctetString = obj as Asn1OctetString;
			if (asn1OctetString != null)
			{
				return new DerInteger(asn1OctetString.GetOctets());
			}
			Asn1TaggedObject asn1TaggedObject = obj as Asn1TaggedObject;
			if (asn1TaggedObject != null)
			{
				return DerInteger.GetInstance(asn1TaggedObject.GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0005F240 File Offset: 0x0005E240
		public static DerInteger GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return DerInteger.GetInstance(obj.GetObject());
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0005F25B File Offset: 0x0005E25B
		public DerInteger(int value)
		{
			this.bytes = BigInteger.ValueOf((long)value).ToByteArray();
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0005F275 File Offset: 0x0005E275
		public DerInteger(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.bytes = value.ToByteArray();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0005F297 File Offset: 0x0005E297
		public DerInteger(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0005F2A6 File Offset: 0x0005E2A6
		public BigInteger Value
		{
			get
			{
				return new BigInteger(this.bytes);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0005F2B3 File Offset: 0x0005E2B3
		public BigInteger PositiveValue
		{
			get
			{
				return new BigInteger(1, this.bytes);
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0005F2C1 File Offset: 0x0005E2C1
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(2, this.bytes);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0005F2D0 File Offset: 0x0005E2D0
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.bytes);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0005F2E0 File Offset: 0x0005E2E0
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerInteger derInteger = asn1Object as DerInteger;
			return derInteger != null && Arrays.AreEqual(this.bytes, derInteger.bytes);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0005F30A File Offset: 0x0005E30A
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x04000C30 RID: 3120
		private readonly byte[] bytes;
	}
}
