using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000074 RID: 116
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class SqlUserDefinedTypeAttribute : Attribute
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00047A00 File Offset: 0x00046E00
		public SqlUserDefinedTypeAttribute(Format format)
		{
			if (format == Format.Unknown)
			{
				throw ADP.NotSupportedUserDefinedTypeSerializationFormat(format, "format");
			}
			if (format - Format.Native > 1)
			{
				throw ADP.InvalidUserDefinedTypeSerializationFormat(format);
			}
			this.m_format = format;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00047A38 File Offset: 0x00046E38
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00047A4C File Offset: 0x00046E4C
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < -1)
				{
					throw ADP.ArgumentOutOfRange("MaxByteSize");
				}
				this.m_MaxByteSize = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00047A70 File Offset: 0x00046E70
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00047A84 File Offset: 0x00046E84
		public bool IsFixedLength
		{
			get
			{
				return this.m_IsFixedLength;
			}
			set
			{
				this.m_IsFixedLength = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00047A98 File Offset: 0x00046E98
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00047AAC File Offset: 0x00046EAC
		public bool IsByteOrdered
		{
			get
			{
				return this.m_IsByteOrdered;
			}
			set
			{
				this.m_IsByteOrdered = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00047AC0 File Offset: 0x00046EC0
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00047AD4 File Offset: 0x00046ED4
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00047AE8 File Offset: 0x00046EE8
		public string ValidationMethodName
		{
			get
			{
				return this.m_ValidationMethodName;
			}
			set
			{
				this.m_ValidationMethodName = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00047AFC File Offset: 0x00046EFC
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00047B10 File Offset: 0x00046F10
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		// Token: 0x040001FD RID: 509
		private int m_MaxByteSize;

		// Token: 0x040001FE RID: 510
		private bool m_IsFixedLength;

		// Token: 0x040001FF RID: 511
		private bool m_IsByteOrdered;

		// Token: 0x04000200 RID: 512
		private Format m_format;

		// Token: 0x04000201 RID: 513
		private string m_fName;

		// Token: 0x04000202 RID: 514
		internal const int YukonMaxByteSizeValue = 8000;

		// Token: 0x04000203 RID: 515
		private string m_ValidationMethodName;
	}
}
