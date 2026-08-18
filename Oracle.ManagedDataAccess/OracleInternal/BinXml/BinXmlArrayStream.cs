using System;
using System.IO;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.BinXml
{
	// Token: 0x02000028 RID: 40
	internal class BinXmlArrayStream : IDisposable
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		internal BinXmlArrayStream()
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		internal BinXmlArrayStream(byte[] buffer, long size)
		{
			this.m_BinReader = new BinaryReader(new MemoryStream(buffer));
			this.m_ReadOnlyStream = true;
			this.m_bInit = true;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000C818 File Offset: 0x0000AA18
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_Disposed)
			{
				if (disposing && this.m_BinReader != null)
				{
					this.m_BinReader.Dispose();
					this.m_BinReader = null;
				}
				this.m_Disposed = true;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000C848 File Offset: 0x0000AA48
		internal virtual void Init()
		{
			if (this.m_ReadOnlyStream)
			{
				this.m_bInit = true;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C85C File Offset: 0x0000AA5C
		internal void Reset()
		{
			this.Dispose();
			this.m_bInit = false;
			this.Init();
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000C874 File Offset: 0x0000AA74
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000C888 File Offset: 0x0000AA88
		internal long Position
		{
			get
			{
				return this.m_BinReader.BaseStream.Position;
			}
			set
			{
				this.m_BinReader.BaseStream.Position = value;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C89C File Offset: 0x0000AA9C
		internal int[] ReadIntegers(int count)
		{
			int[] array = new int[count];
			this.m_BinReader.BaseStream.Position = 0L;
			for (int i = 0; i < count; i++)
			{
				array[i] = this.m_BinReader.ReadInt32();
			}
			return array;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
		internal byte[] ReadBytes(int count, int startPos = -1)
		{
			long position = this.Position;
			if (startPos != -1)
			{
				this.m_BinReader.BaseStream.Position = (long)startPos;
			}
			byte[] result = this.m_BinReader.ReadBytes(count);
			if (startPos != -1)
			{
				this.Position = position;
			}
			return result;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000C924 File Offset: 0x0000AB24
		internal void ReadAndCopyBytes(byte[] destination, long offset, long count)
		{
			if (destination == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.BufferInsufficient, null, ObxmlOpcode.OpcodeIds.None));
			}
			byte[] array = this.ReadBytes((int)count, (int)offset);
			int num = 0;
			while ((long)num < count)
			{
				destination[num] = array[num];
				num++;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000C970 File Offset: 0x0000AB70
		internal byte[] ReadBytes(int count)
		{
			return this.m_BinReader.ReadBytes(count);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000C980 File Offset: 0x0000AB80
		internal byte ReadByte()
		{
			return this.m_BinReader.ReadByte();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000C990 File Offset: 0x0000AB90
		internal byte[] ReadAllBytes(long startPos = -1L)
		{
			if (this.m_ReadOnlyStream)
			{
				long num = this.m_BinReader.BaseStream.Length;
				if (startPos != -1L)
				{
					num -= startPos;
					this.m_BinReader.BaseStream.Position = startPos;
				}
				if (num > 0L)
				{
					return this.m_BinReader.ReadBytes((int)num);
				}
			}
			return null;
		}

		// Token: 0x040002D7 RID: 727
		protected BinaryReader m_BinReader;

		// Token: 0x040002D8 RID: 728
		protected bool m_bInit;

		// Token: 0x040002D9 RID: 729
		protected bool m_ReadOnlyStream;

		// Token: 0x040002DA RID: 730
		protected bool m_Disposed;
	}
}
