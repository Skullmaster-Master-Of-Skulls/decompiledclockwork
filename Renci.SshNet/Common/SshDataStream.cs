using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Renci.SshNet.Common
{
	// Token: 0x02000108 RID: 264
	public class SshDataStream : MemoryStream
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x0002522B File Offset: 0x0002342B
		public SshDataStream(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00025234 File Offset: 0x00023434
		public SshDataStream(byte[] buffer) : base(buffer)
		{
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002523D File Offset: 0x0002343D
		public bool IsEndOfData
		{
			get
			{
				return this.Position >= this.Length;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00025250 File Offset: 0x00023450
		public void Write(uint value)
		{
			byte[] bytes = value.GetBytes();
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00025270 File Offset: 0x00023470
		public void Write(ulong value)
		{
			byte[] bytes = value.GetBytes();
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00025290 File Offset: 0x00023490
		public void Write(BigInteger data)
		{
			byte[] array = data.ToByteArray().Reverse<byte>();
			this.WriteBinary(array, 0, array.Length);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000252B5 File Offset: 0x000234B5
		public void Write(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.Write(data, 0, data.Length);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000252D0 File Offset: 0x000234D0
		public byte[] ReadBinary()
		{
			uint num = this.ReadUInt32();
			if (num > 2147483647U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Data longer than {0} is not supported.", new object[]
				{
					int.MaxValue
				}));
			}
			return this.ReadBytes((int)num);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002531B File Offset: 0x0002351B
		public void WriteBinary(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.WriteBinary(buffer, 0, buffer.Length);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00025336 File Offset: 0x00023536
		public void WriteBinary(byte[] buffer, int offset, int count)
		{
			this.Write((uint)count);
			this.Write(buffer, offset, count);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00025348 File Offset: 0x00023548
		public void Write(string s, Encoding encoding)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			byte[] bytes = encoding.GetBytes(s);
			this.WriteBinary(bytes, 0, bytes.Length);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00025378 File Offset: 0x00023578
		public BigInteger ReadBigInt()
		{
			uint length = this.ReadUInt32();
			return new BigInteger(this.ReadBytes((int)length).Reverse<byte>());
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000253A0 File Offset: 0x000235A0
		public uint ReadUInt32()
		{
			byte[] array = this.ReadBytes(4);
			return (uint)((int)array[0] << 24 | (int)array[1] << 16 | (int)array[2] << 8 | (int)array[3]);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000253CC File Offset: 0x000235CC
		public ulong ReadUInt64()
		{
			byte[] array = this.ReadBytes(8);
			return (ulong)array[0] << 56 | (ulong)array[1] << 48 | (ulong)array[2] << 40 | (ulong)array[3] << 32 | (ulong)array[4] << 24 | (ulong)array[5] << 16 | (ulong)array[6] << 8 | (ulong)array[7];
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002541C File Offset: 0x0002361C
		public string ReadString(Encoding encoding)
		{
			uint num = this.ReadUInt32();
			if (num > 2147483647U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Strings longer than {0} is not supported.", new object[]
				{
					int.MaxValue
				}));
			}
			byte[] array = this.ReadBytes((int)num);
			return encoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00025474 File Offset: 0x00023674
		private byte[] ReadBytes(int length)
		{
			byte[] array = new byte[length];
			if (this.Read(array, 0, length) < length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			return array;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000254A0 File Offset: 0x000236A0
		public override byte[] ToArray()
		{
			if ((long)this.Capacity == this.Length)
			{
				return this.GetBuffer();
			}
			return base.ToArray();
		}
	}
}
