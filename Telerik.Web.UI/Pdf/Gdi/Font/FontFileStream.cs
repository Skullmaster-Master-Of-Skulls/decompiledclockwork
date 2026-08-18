using System;
using System.Collections;
using System.IO;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200160E RID: 5646
	internal class FontFileStream : IDisposable
	{
		// Token: 0x0600DBE1 RID: 56289 RVA: 0x00301074 File Offset: 0x002FF274
		public FontFileStream(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data array cannot be null.");
			}
			if (data.Length == 0)
			{
				throw new ArgumentException("data array is empty.", "data");
			}
			this.stream = new MemoryStream(data);
		}

		// Token: 0x0600DBE2 RID: 56290 RVA: 0x003010C6 File Offset: 0x002FF2C6
		public FontFileStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "stream parameter cannot be null");
			}
			this.stream = stream;
		}

		// Token: 0x0600DBE3 RID: 56291 RVA: 0x003010F3 File Offset: 0x002FF2F3
		public byte ReadByte()
		{
			return (byte)this.stream.ReadByte();
		}

		// Token: 0x0600DBE4 RID: 56292 RVA: 0x00301101 File Offset: 0x002FF301
		public void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x0600DBE5 RID: 56293 RVA: 0x0030110F File Offset: 0x002FF30F
		public sbyte ReadChar()
		{
			return (sbyte)this.stream.ReadByte();
		}

		// Token: 0x0600DBE6 RID: 56294 RVA: 0x0030111D File Offset: 0x002FF31D
		public void WriteChar(sbyte value)
		{
			this.stream.WriteByte((byte)((int)value & 255));
		}

		// Token: 0x0600DBE7 RID: 56295 RVA: 0x00301132 File Offset: 0x002FF332
		public short ReadShort()
		{
			return (short)(((int)this.ReadByte() << 8) + (int)this.ReadByte());
		}

		// Token: 0x0600DBE8 RID: 56296 RVA: 0x00301144 File Offset: 0x002FF344
		public void WriteShort(int value)
		{
			this.stream.WriteByte((byte)(value >> 8 & 255));
			this.stream.WriteByte((byte)(value & 255));
		}

		// Token: 0x0600DBE9 RID: 56297 RVA: 0x0030116E File Offset: 0x002FF36E
		public short ReadFWord()
		{
			return this.ReadShort();
		}

		// Token: 0x0600DBEA RID: 56298 RVA: 0x00301176 File Offset: 0x002FF376
		public void WriteFWord(int value)
		{
			this.WriteShort(value);
		}

		// Token: 0x0600DBEB RID: 56299 RVA: 0x0030117F File Offset: 0x002FF37F
		public int ReadUShort()
		{
			return ((int)this.ReadByte() << 8) + (int)this.ReadByte();
		}

		// Token: 0x0600DBEC RID: 56300 RVA: 0x00301190 File Offset: 0x002FF390
		public void WriteUShort(int value)
		{
			this.stream.WriteByte((byte)(value >> 8 & 255));
			this.stream.WriteByte((byte)(value & 255));
		}

		// Token: 0x0600DBED RID: 56301 RVA: 0x003011BA File Offset: 0x002FF3BA
		public int ReadUFWord()
		{
			return this.ReadUShort();
		}

		// Token: 0x0600DBEE RID: 56302 RVA: 0x003011C2 File Offset: 0x002FF3C2
		public void WriteUFWord(int value)
		{
			this.WriteUShort(value);
		}

		// Token: 0x0600DBEF RID: 56303 RVA: 0x003011CC File Offset: 0x002FF3CC
		public int ReadLong()
		{
			int num = (int)this.ReadByte();
			num = (num << 8) + (int)this.ReadByte();
			num = (num << 8) + (int)this.ReadByte();
			return (num << 8) + (int)this.ReadByte();
		}

		// Token: 0x0600DBF0 RID: 56304 RVA: 0x00301204 File Offset: 0x002FF404
		public void WriteLong(int value)
		{
			this.stream.WriteByte((byte)(value >> 24 & 255));
			this.stream.WriteByte((byte)(value >> 16 & 255));
			this.stream.WriteByte((byte)(value >> 8 & 255));
			this.stream.WriteByte((byte)(value & 255));
		}

		// Token: 0x0600DBF1 RID: 56305 RVA: 0x00301268 File Offset: 0x002FF468
		public int ReadULong()
		{
			int num = (int)this.ReadByte();
			num = (num << 8) + (int)this.ReadByte();
			num = (num << 8) + (int)this.ReadByte();
			return (num << 8) + (int)this.ReadByte();
		}

		// Token: 0x0600DBF2 RID: 56306 RVA: 0x003012A0 File Offset: 0x002FF4A0
		public void WriteULong(long value)
		{
			this.stream.WriteByte((byte)(value >> 24 & 255L));
			this.stream.WriteByte((byte)(value >> 16 & 255L));
			this.stream.WriteByte((byte)(value >> 8 & 255L));
			this.stream.WriteByte((byte)((int)value & 255));
		}

		// Token: 0x0600DBF3 RID: 56307 RVA: 0x00301305 File Offset: 0x002FF505
		public int ReadFixed()
		{
			return this.ReadLong();
		}

		// Token: 0x0600DBF4 RID: 56308 RVA: 0x0030130D File Offset: 0x002FF50D
		public void WriteFixed(int value)
		{
			this.WriteLong(value);
		}

		// Token: 0x0600DBF5 RID: 56309 RVA: 0x00301318 File Offset: 0x002FF518
		public long ReadLongDateTime()
		{
			long num = (long)((ulong)this.ReadByte());
			num = (num << 8) + (long)((ulong)this.ReadByte());
			num = (num << 8) + (long)((ulong)this.ReadByte());
			num = (num << 8) + (long)((ulong)this.ReadByte());
			num = (num << 8) + (long)((ulong)this.ReadByte());
			num = (num << 8) + (long)((ulong)this.ReadByte());
			num = (num << 8) + (long)((ulong)this.ReadByte());
			return (num << 8) + (long)((ulong)this.ReadByte());
		}

		// Token: 0x0600DBF6 RID: 56310 RVA: 0x00301384 File Offset: 0x002FF584
		public void WriteDateTime(long value)
		{
			this.stream.WriteByte((byte)(value >> 56 & 255L));
			this.stream.WriteByte((byte)(value >> 48 & 255L));
			this.stream.WriteByte((byte)(value >> 40 & 255L));
			this.stream.WriteByte((byte)(value >> 32 & 255L));
			this.stream.WriteByte((byte)(value >> 24 & 255L));
			this.stream.WriteByte((byte)(value >> 16 & 255L));
			this.stream.WriteByte((byte)(value >> 8 & 255L));
			this.stream.WriteByte((byte)((int)value & 255));
		}

		// Token: 0x0600DBF7 RID: 56311 RVA: 0x00301448 File Offset: 0x002FF648
		public byte[] ReadTag()
		{
			return new byte[]
			{
				this.ReadByte(),
				this.ReadByte(),
				this.ReadByte(),
				this.ReadByte()
			};
		}

		// Token: 0x0600DBF8 RID: 56312 RVA: 0x00301481 File Offset: 0x002FF681
		public void WriteTag(byte[] value)
		{
			this.stream.WriteByte(value[0]);
			this.stream.WriteByte(value[1]);
			this.stream.WriteByte(value[2]);
			this.stream.WriteByte(value[3]);
		}

		// Token: 0x0600DBF9 RID: 56313 RVA: 0x003014BC File Offset: 0x002FF6BC
		public int Pad()
		{
			int num = (int)(this.stream.Position % 4L);
			for (int i = 0; i < num; i++)
			{
				this.stream.WriteByte(0);
			}
			return num;
		}

		// Token: 0x0600DBFA RID: 56314 RVA: 0x003014F2 File Offset: 0x002FF6F2
		public void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x0600DBFB RID: 56315 RVA: 0x00301502 File Offset: 0x002FF702
		public int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x17004358 RID: 17240
		// (get) Token: 0x0600DBFC RID: 56316 RVA: 0x00301512 File Offset: 0x002FF712
		// (set) Token: 0x0600DBFD RID: 56317 RVA: 0x0030151F File Offset: 0x002FF71F
		public long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x17004359 RID: 17241
		// (get) Token: 0x0600DBFE RID: 56318 RVA: 0x0030152D File Offset: 0x002FF72D
		public long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x0600DBFF RID: 56319 RVA: 0x0030153A File Offset: 0x002FF73A
		public void Skip(long offset)
		{
			this.stream.Seek(offset, SeekOrigin.Current);
		}

		// Token: 0x0600DC00 RID: 56320 RVA: 0x0030154A File Offset: 0x002FF74A
		public long SetRestorePoint()
		{
			this.markers.Push(this.Position);
			return this.Position;
		}

		// Token: 0x0600DC01 RID: 56321 RVA: 0x00301568 File Offset: 0x002FF768
		public long Restore()
		{
			if (this.markers.Count == 0)
			{
				throw new InvalidOperationException("There are no stream markers.");
			}
			long position = this.Position;
			this.Position = Convert.ToInt64(this.markers.Pop());
			return position;
		}

		// Token: 0x0600DC02 RID: 56322 RVA: 0x003015AB File Offset: 0x002FF7AB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600DC03 RID: 56323 RVA: 0x003015BA File Offset: 0x002FF7BA
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Close();
			}
		}

		// Token: 0x04003D71 RID: 15729
		private Stream stream;

		// Token: 0x04003D72 RID: 15730
		private Stack markers = new Stack();
	}
}
