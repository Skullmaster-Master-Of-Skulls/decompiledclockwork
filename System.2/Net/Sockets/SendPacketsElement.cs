using System;

namespace System.Net.Sockets
{
	// Token: 0x0200037A RID: 890
	public class SendPacketsElement
	{
		// Token: 0x06002134 RID: 8500 RVA: 0x0009F3C5 File Offset: 0x0009D5C5
		private SendPacketsElement()
		{
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0009F3CD File Offset: 0x0009D5CD
		public SendPacketsElement(string filepath) : this(filepath, 0, 0, false)
		{
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0009F3D9 File Offset: 0x0009D5D9
		public SendPacketsElement(string filepath, int offset, int count) : this(filepath, offset, count, false)
		{
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0009F3E8 File Offset: 0x0009D5E8
		public SendPacketsElement(string filepath, int offset, int count, bool endOfPacket)
		{
			if (filepath == null)
			{
				throw new ArgumentNullException("filepath");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(filepath, null, offset, count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.File, endOfPacket);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0009F434 File Offset: 0x0009D634
		public SendPacketsElement(byte[] buffer) : this(buffer, 0, (buffer != null) ? buffer.Length : 0, false)
		{
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0009F448 File Offset: 0x0009D648
		public SendPacketsElement(byte[] buffer, int offset, int count) : this(buffer, offset, count, false)
		{
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0009F454 File Offset: 0x0009D654
		public SendPacketsElement(byte[] buffer, int offset, int count, bool endOfPacket)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(null, buffer, offset, count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.Memory, endOfPacket);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0009F4AE File Offset: 0x0009D6AE
		private void Initialize(string filePath, byte[] buffer, int offset, int count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags flags, bool endOfPacket)
		{
			this.m_FilePath = filePath;
			this.m_Buffer = buffer;
			this.m_Offset = offset;
			this.m_Count = count;
			this.m_Flags = flags;
			if (endOfPacket)
			{
				this.m_Flags |= UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.EndOfPacket;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x0009F4E7 File Offset: 0x0009D6E7
		public string FilePath
		{
			get
			{
				return this.m_FilePath;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x0009F4EF File Offset: 0x0009D6EF
		public byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0009F4F7 File Offset: 0x0009D6F7
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0009F4FF File Offset: 0x0009D6FF
		public int Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0009F507 File Offset: 0x0009D707
		public bool EndOfPacket
		{
			get
			{
				return (this.m_Flags & UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.EndOfPacket) > UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.None;
			}
		}

		// Token: 0x04001E74 RID: 7796
		internal string m_FilePath;

		// Token: 0x04001E75 RID: 7797
		internal byte[] m_Buffer;

		// Token: 0x04001E76 RID: 7798
		internal int m_Offset;

		// Token: 0x04001E77 RID: 7799
		internal int m_Count;

		// Token: 0x04001E78 RID: 7800
		internal UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags m_Flags;
	}
}
