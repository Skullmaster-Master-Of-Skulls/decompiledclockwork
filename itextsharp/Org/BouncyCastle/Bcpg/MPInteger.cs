using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200019E RID: 414
	public class MPInteger : BcpgObject
	{
		// Token: 0x06000FF8 RID: 4088 RVA: 0x0005C704 File Offset: 0x0005B704
		public MPInteger(BcpgInputStream bcpgIn)
		{
			if (bcpgIn == null)
			{
				throw new ArgumentNullException("bcpgIn");
			}
			int num = bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte();
			byte[] array = new byte[(num + 7) / 8];
			bcpgIn.ReadFully(array);
			this.val = new BigInteger(1, array);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0005C754 File Offset: 0x0005B754
		public MPInteger(BigInteger val)
		{
			if (val == null)
			{
				throw new ArgumentNullException("val");
			}
			if (val.SignValue < 0)
			{
				throw new ArgumentException("Values must be positive", "val");
			}
			this.val = val;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0005C78A File Offset: 0x0005B78A
		public BigInteger Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0005C792 File Offset: 0x0005B792
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteShort((short)this.val.BitLength);
			bcpgOut.Write(this.val.ToByteArrayUnsigned());
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0005C7B7 File Offset: 0x0005B7B7
		internal static void Encode(BcpgOutputStream bcpgOut, BigInteger val)
		{
			bcpgOut.WriteShort((short)val.BitLength);
			bcpgOut.Write(val.ToByteArrayUnsigned());
		}

		// Token: 0x04000B91 RID: 2961
		private readonly BigInteger val;
	}
}
