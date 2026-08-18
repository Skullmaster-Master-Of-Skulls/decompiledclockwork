using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000155 RID: 341
	public class DerEnumerated : Asn1Object
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x00043414 File Offset: 0x00042414
		public static DerEnumerated GetInstance(object obj)
		{
			if (obj == null || obj is DerEnumerated)
			{
				return (DerEnumerated)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerEnumerated(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerEnumerated.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0004347F File Offset: 0x0004247F
		public static DerEnumerated GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerEnumerated.GetInstance(obj.GetObject());
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0004348C File Offset: 0x0004248C
		public DerEnumerated(int value)
		{
			this.bytes = BigInteger.ValueOf((long)value).ToByteArray();
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000434A6 File Offset: 0x000424A6
		public DerEnumerated(BigInteger value)
		{
			this.bytes = value.ToByteArray();
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000434BA File Offset: 0x000424BA
		public DerEnumerated(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000434C9 File Offset: 0x000424C9
		public BigInteger Value
		{
			get
			{
				return new BigInteger(this.bytes);
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000434D6 File Offset: 0x000424D6
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(10, this.bytes);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000434E8 File Offset: 0x000424E8
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerEnumerated derEnumerated = asn1Object as DerEnumerated;
			return derEnumerated != null && Arrays.AreEqual(this.bytes, derEnumerated.bytes);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00043512 File Offset: 0x00042512
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.bytes);
		}

		// Token: 0x0400098B RID: 2443
		private readonly byte[] bytes;
	}
}
