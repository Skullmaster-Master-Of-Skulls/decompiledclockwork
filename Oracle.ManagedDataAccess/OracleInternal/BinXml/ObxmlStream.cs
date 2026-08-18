using System;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.I18N;

namespace OracleInternal.BinXml
{
	// Token: 0x02000027 RID: 39
	internal class ObxmlStream : IDisposable
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000C540 File Offset: 0x0000A740
		internal ObxmlStream(ObxmlContentObject contentObj)
		{
			if (!contentObj.IsContentValid())
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.RequestInputInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
			if (contentObj.InputType == InputOutputTypes.ByteArray)
			{
				this.m_DataStream = new BinXmlArrayStream((byte[])contentObj.ContentObject, contentObj.InputLength);
			}
			this.m_DataStream.Init();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_Disposed)
			{
				if (disposing && this.m_DataStream != null)
				{
					this.m_DataStream.Dispose();
					this.m_DataStream = null;
				}
				this.m_Disposed = true;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		internal long Position
		{
			get
			{
				return this.m_DataStream.Position;
			}
			set
			{
				this.m_DataStream.Position = value;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C604 File Offset: 0x0000A804
		internal Stream Open(string filePath)
		{
			return null;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000C608 File Offset: 0x0000A808
		internal Stream Open(OracleBlob blob)
		{
			return null;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C60C File Offset: 0x0000A80C
		internal void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C61C File Offset: 0x0000A81C
		internal byte[] ReadBytes(long offset, long count)
		{
			return this.m_DataStream.ReadBytes((int)count, (int)offset);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C630 File Offset: 0x0000A830
		internal void ReadAndCopyBytes(byte[] destination, long offset, long count)
		{
			this.m_DataStream.ReadAndCopyBytes(destination, (long)((int)offset), (long)((int)count));
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C644 File Offset: 0x0000A844
		internal byte[] ReadBytes(int count)
		{
			return this.m_DataStream.ReadBytes(count);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C654 File Offset: 0x0000A854
		internal byte[] ReadAllBytes(int offset)
		{
			return this.m_DataStream.ReadAllBytes((long)offset);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C664 File Offset: 0x0000A864
		internal short ReadShortIntFromByte()
		{
			return (short)this.m_DataStream.ReadByte();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C680 File Offset: 0x0000A880
		internal short ReadShortInt()
		{
			byte[] array = this.m_DataStream.ReadBytes(2);
			return BitConverter.ToInt16(ObxmlStream.Reverse(array, 2), 0);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		internal int ReadInt4()
		{
			byte[] array = this.m_DataStream.ReadBytes(4);
			return BitConverter.ToInt32(ObxmlStream.Reverse(array, 4), 0);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		internal long ReadInt8()
		{
			byte[] array = this.m_DataStream.ReadBytes(8);
			return BitConverter.ToInt64(ObxmlStream.Reverse(array, 8), 0);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		internal char ReadChar()
		{
			byte[] array = this.m_DataStream.ReadBytes(2);
			return BitConverter.ToChar(ObxmlStream.Reverse(array, 2), 0);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000C720 File Offset: 0x0000A920
		internal char[] ReadChar(int count)
		{
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				byte[] array2 = this.m_DataStream.ReadBytes(2);
				array[i] = BitConverter.ToChar(ObxmlStream.Reverse(array2, 2), 0);
			}
			return array;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000C760 File Offset: 0x0000A960
		internal string ReadUtf8String(ulong length)
		{
			if (length <= 0UL)
			{
				return null;
			}
			byte[] array = this.ReadBytes((int)length);
			return Conv.GetInstance(873).ConvertBytesToString(array, 0, array.Length, null, true);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000C794 File Offset: 0x0000A994
		internal static byte[] Reverse(byte[] array, int count)
		{
			int num = 0;
			int num2 = count - 1;
			while (num < count / 2 && num2 > num)
			{
				byte b = array[num];
				array[num] = array[num2];
				array[num2] = b;
				num++;
				num2--;
			}
			return array;
		}

		// Token: 0x040002D4 RID: 724
		internal static readonly long sUseCurrentOffset = -1L;

		// Token: 0x040002D5 RID: 725
		private BinXmlArrayStream m_DataStream;

		// Token: 0x040002D6 RID: 726
		private bool m_Disposed;
	}
}
