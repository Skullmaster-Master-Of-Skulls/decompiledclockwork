using System;

namespace System.Net.Sockets
{
	// Token: 0x020005C0 RID: 1472
	public class SendPacketsElement
	{
		// Token: 0x06002DEE RID: 11758 RVA: 0x000CA391 File Offset: 0x000C9391
		private SendPacketsElement()
		{
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000CA399 File Offset: 0x000C9399
		public SendPacketsElement(string filepath) : this(filepath, null, 0, 0, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.File)
		{
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000CA3A6 File Offset: 0x000C93A6
		public SendPacketsElement(string filepath, int offset, int count) : this(filepath, null, offset, count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.File)
		{
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000CA3B3 File Offset: 0x000C93B3
		public SendPacketsElement(string filepath, int offset, int count, bool endOfPacket) : this(filepath, null, offset, count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.File | UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.EndOfPacket)
		{
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000CA3C0 File Offset: 0x000C93C0
		public SendPacketsElement(byte[] buffer) : this(null, buffer, 0, buffer.Length, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.Memory)
		{
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000CA3CF File Offset: 0x000C93CF
		public SendPacketsElement(byte[] buffer, int offset, int count) : this(null, buffer, offset, count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.Memory)
		{
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000CA3DC File Offset: 0x000C93DC
		public SendPacketsElement(byte[] buffer, int offset, int count, bool endOfPacket) : this(null, buffer, offset, count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.Memory | UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.EndOfPacket)
		{
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000CA3E9 File Offset: 0x000C93E9
		private SendPacketsElement(string filepath, byte[] buffer, int offset, int count, UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags flags)
		{
			this.m_FilePath = filepath;
			this.m_Buffer = buffer;
			this.m_Offset = offset;
			this.m_Count = count;
			this.m_Flags = flags;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000CA416 File Offset: 0x000C9416
		public string FilePath
		{
			get
			{
				return this.m_FilePath;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000CA41E File Offset: 0x000C941E
		public byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000CA426 File Offset: 0x000C9426
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x000CA42E File Offset: 0x000C942E
		public int Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000CA436 File Offset: 0x000C9436
		public bool EndOfPacket
		{
			get
			{
				return (this.m_Flags & UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.EndOfPacket) != UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags.None;
			}
		}

		// Token: 0x04002B65 RID: 11109
		internal string m_FilePath;

		// Token: 0x04002B66 RID: 11110
		internal byte[] m_Buffer;

		// Token: 0x04002B67 RID: 11111
		internal int m_Offset;

		// Token: 0x04002B68 RID: 11112
		internal int m_Count;

		// Token: 0x04002B69 RID: 11113
		internal UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags m_Flags;
	}
}
