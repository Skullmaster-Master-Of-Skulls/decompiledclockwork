using System;
using System.Collections.Generic;
using System.Text;

namespace Renci.SshNet.Common
{
	// Token: 0x02000103 RID: 259
	public abstract class SshData
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00024E22 File Offset: 0x00023022
		protected SshDataStream DataStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00024E2A File Offset: 0x0002302A
		protected bool IsEndOfData
		{
			get
			{
				return this._stream.Position >= this._stream.Length;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		protected virtual int ZeroReaderIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		protected virtual int BufferCapacity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00024E48 File Offset: 0x00023048
		public byte[] GetBytes()
		{
			int bufferCapacity = this.BufferCapacity;
			SshDataStream sshDataStream = new SshDataStream((bufferCapacity != -1) ? bufferCapacity : 64);
			this.WriteBytes(sshDataStream);
			return sshDataStream.ToArray();
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00024E78 File Offset: 0x00023078
		protected virtual void WriteBytes(SshDataStream stream)
		{
			this._stream = stream;
			this.SaveData();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00024E87 File Offset: 0x00023087
		internal T OfType<T>() where T : SshData, new()
		{
			T t = Activator.CreateInstance<T>();
			t.LoadBytes(this._loadedData, this._offset);
			t.LoadData();
			return t;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00024EB0 File Offset: 0x000230B0
		public void Load(byte[] value)
		{
			this.Load(value, 0);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00024EBA File Offset: 0x000230BA
		public void Load(byte[] value, int offset)
		{
			this.LoadBytes(value, offset);
			this.LoadData();
		}

		// Token: 0x06000B07 RID: 2823
		protected abstract void LoadData();

		// Token: 0x06000B08 RID: 2824
		protected abstract void SaveData();

		// Token: 0x06000B09 RID: 2825 RVA: 0x00024ECA File Offset: 0x000230CA
		protected void LoadBytes(byte[] bytes)
		{
			this.LoadBytes(bytes, 0);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00024ED4 File Offset: 0x000230D4
		protected void LoadBytes(byte[] bytes, int offset)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			this._loadedData = bytes;
			this._offset = offset;
			this._stream = new SshDataStream(bytes);
			this.ResetReader();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00024F04 File Offset: 0x00023104
		protected void ResetReader()
		{
			this._stream.Position = (long)(this.ZeroReaderIndex + this._offset);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00024F20 File Offset: 0x00023120
		protected byte[] ReadBytes()
		{
			int num = (int)(this._stream.Length - this._stream.Position);
			byte[] array = new byte[num];
			this._stream.Read(array, 0, num);
			return array;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00024F60 File Offset: 0x00023160
		protected byte[] ReadBytes(int length)
		{
			byte[] array = new byte[length];
			if (this._stream.Read(array, 0, length) < length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			return array;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00024F91 File Offset: 0x00023191
		protected byte ReadByte()
		{
			int num = this._stream.ReadByte();
			if (num == -1)
			{
				throw new InvalidOperationException("Attempt to read past the end of the SSH data stream.");
			}
			return (byte)num;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00024FAE File Offset: 0x000231AE
		protected bool ReadBoolean()
		{
			return this.ReadByte() > 0;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00024FBC File Offset: 0x000231BC
		protected ushort ReadUInt16()
		{
			byte[] array = this.ReadBytes(2);
			return (ushort)((int)array[0] << 8 | (int)array[1]);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00024FDC File Offset: 0x000231DC
		protected uint ReadUInt32()
		{
			byte[] array = this.ReadBytes(4);
			return (uint)((int)array[0] << 24 | (int)array[1] << 16 | (int)array[2] << 8 | (int)array[3]);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00025008 File Offset: 0x00023208
		protected ulong ReadUInt64()
		{
			byte[] array = this.ReadBytes(8);
			return (ulong)array[0] << 56 | (ulong)array[1] << 48 | (ulong)array[2] << 40 | (ulong)array[3] << 32 | (ulong)array[4] << 24 | (ulong)array[5] << 16 | (ulong)array[6] << 8 | (ulong)array[7];
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00025058 File Offset: 0x00023258
		protected string ReadString(Encoding encoding)
		{
			return this._stream.ReadString(encoding);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00025066 File Offset: 0x00023266
		protected byte[] ReadBinary()
		{
			return this._stream.ReadBinary();
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00025073 File Offset: 0x00023273
		protected string[] ReadNamesList()
		{
			return this.ReadString(SshData.Ascii).Split(new char[]
			{
				','
			});
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00025090 File Offset: 0x00023290
		protected IDictionary<string, string> ReadExtensionPair()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			while (!this.IsEndOfData)
			{
				string key = this.ReadString(SshData.Ascii);
				string value = this.ReadString(SshData.Ascii);
				dictionary.Add(key, value);
			}
			return dictionary;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000250CE File Offset: 0x000232CE
		protected void Write(byte[] data)
		{
			this._stream.Write(data);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000250DC File Offset: 0x000232DC
		protected void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(buffer, offset, count);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000250EC File Offset: 0x000232EC
		protected void Write(byte data)
		{
			this._stream.WriteByte(data);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000250FA File Offset: 0x000232FA
		protected void Write(bool data)
		{
			this.Write(data ? 1 : 0);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00025109 File Offset: 0x00023309
		protected void Write(uint data)
		{
			this._stream.Write(data);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00025117 File Offset: 0x00023317
		protected void Write(ulong data)
		{
			this._stream.Write(data);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00025125 File Offset: 0x00023325
		protected void Write(string data)
		{
			this.Write(data, SshData.Utf8);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00025133 File Offset: 0x00023333
		protected void Write(string data, Encoding encoding)
		{
			this._stream.Write(data, encoding);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00025142 File Offset: 0x00023342
		protected void WriteBinaryString(byte[] buffer)
		{
			this._stream.WriteBinary(buffer);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00025150 File Offset: 0x00023350
		protected void WriteBinary(byte[] buffer, int offset, int count)
		{
			this._stream.WriteBinary(buffer, offset, count);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00025160 File Offset: 0x00023360
		protected void Write(BigInteger data)
		{
			this._stream.Write(data);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002516E File Offset: 0x0002336E
		protected void Write(string[] data)
		{
			this.Write(string.Join(",", data), SshData.Ascii);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00025188 File Offset: 0x00023388
		protected void Write(IDictionary<string, string> data)
		{
			foreach (KeyValuePair<string, string> keyValuePair in data)
			{
				this.Write(keyValuePair.Key, SshData.Ascii);
				this.Write(keyValuePair.Value, SshData.Ascii);
			}
		}

		// Token: 0x0400040E RID: 1038
		internal const int DefaultCapacity = 64;

		// Token: 0x0400040F RID: 1039
		internal static readonly Encoding Ascii = Encoding.ASCII;

		// Token: 0x04000410 RID: 1040
		internal static readonly Encoding Utf8 = Encoding.UTF8;

		// Token: 0x04000411 RID: 1041
		private SshDataStream _stream;

		// Token: 0x04000412 RID: 1042
		private byte[] _loadedData;

		// Token: 0x04000413 RID: 1043
		private int _offset;
	}
}
