using System;
using System.IO;
using Org.BouncyCastle.Asn1.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020001BA RID: 442
	public class DerOutputStream : FilterStream
	{
		// Token: 0x060010A4 RID: 4260 RVA: 0x0005F01B File Offset: 0x0005E01B
		public DerOutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0005F024 File Offset: 0x0005E024
		private void WriteLength(int length)
		{
			if (length > 127)
			{
				int num = 1;
				uint num2 = (uint)length;
				while ((num2 >>= 8) != 0U)
				{
					num++;
				}
				this.WriteByte((byte)(num | 128));
				for (int i = (num - 1) * 8; i >= 0; i -= 8)
				{
					this.WriteByte((byte)(length >> i));
				}
				return;
			}
			this.WriteByte((byte)length);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0005F07B File Offset: 0x0005E07B
		internal void WriteEncoded(int tag, byte[] bytes)
		{
			this.WriteByte((byte)tag);
			this.WriteLength(bytes.Length);
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0005F099 File Offset: 0x0005E099
		internal void WriteEncoded(int tag, byte[] bytes, int offset, int length)
		{
			this.WriteByte((byte)tag);
			this.WriteLength(length);
			this.Write(bytes, offset, length);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0005F0B8 File Offset: 0x0005E0B8
		internal void WriteTag(int flags, int tagNo)
		{
			if (tagNo < 31)
			{
				this.WriteByte((byte)(flags | tagNo));
				return;
			}
			this.WriteByte((byte)(flags | 31));
			if (tagNo < 128)
			{
				this.WriteByte((byte)tagNo);
				return;
			}
			byte[] array = new byte[5];
			int num = array.Length;
			array[--num] = (byte)(tagNo & 127);
			do
			{
				tagNo >>= 7;
				array[--num] = (byte)((tagNo & 127) | 128);
			}
			while (tagNo > 127);
			this.Write(array, num, array.Length - num);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0005F131 File Offset: 0x0005E131
		internal void WriteEncoded(int flags, int tagNo, byte[] bytes)
		{
			this.WriteTag(flags, tagNo);
			this.WriteLength(bytes.Length);
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0005F14F File Offset: 0x0005E14F
		protected void WriteNull()
		{
			this.WriteByte(5);
			this.WriteByte(0);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0005F160 File Offset: 0x0005E160
		[Obsolete("Use version taking an Asn1Encodable arg instead")]
		public virtual void WriteObject(object obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			if (obj is Asn1Object)
			{
				((Asn1Object)obj).Encode(this);
				return;
			}
			if (obj is Asn1Encodable)
			{
				((Asn1Encodable)obj).ToAsn1Object().Encode(this);
				return;
			}
			throw new IOException("object not Asn1Object");
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0005F1B0 File Offset: 0x0005E1B0
		public virtual void WriteObject(Asn1Encodable obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			obj.ToAsn1Object().Encode(this);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0005F1C8 File Offset: 0x0005E1C8
		public virtual void WriteObject(Asn1Object obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			obj.Encode(this);
		}
	}
}
