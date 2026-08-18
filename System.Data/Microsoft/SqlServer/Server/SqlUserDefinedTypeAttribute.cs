using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020002A0 RID: 672
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class SqlUserDefinedTypeAttribute : Attribute
	{
		// Token: 0x06002294 RID: 8852 RVA: 0x0028C458 File Offset: 0x0028B858
		public SqlUserDefinedTypeAttribute(Format format)
		{
			switch (format)
			{
			case Format.Unknown:
				throw ADP.NotSupportedUserDefinedTypeSerializationFormat(format, "format");
			case Format.Native:
			case Format.UserDefined:
				this.m_format = format;
				return;
			default:
				throw ADP.InvalidUserDefinedTypeSerializationFormat(format);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x0028C4A8 File Offset: 0x0028B8A8
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x0028C4C8 File Offset: 0x0028B8C8
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

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x0028C4F8 File Offset: 0x0028B8F8
		// (set) Token: 0x06002298 RID: 8856 RVA: 0x0028C518 File Offset: 0x0028B918
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

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x0028C538 File Offset: 0x0028B938
		// (set) Token: 0x0600229A RID: 8858 RVA: 0x0028C558 File Offset: 0x0028B958
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

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x0028C578 File Offset: 0x0028B978
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x0028C598 File Offset: 0x0028B998
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x0028C5B8 File Offset: 0x0028B9B8
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

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x0028C5D8 File Offset: 0x0028B9D8
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x0028C5F8 File Offset: 0x0028B9F8
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

		// Token: 0x04001679 RID: 5753
		internal const int YukonMaxByteSizeValue = 8000;

		// Token: 0x0400167A RID: 5754
		private int m_MaxByteSize;

		// Token: 0x0400167B RID: 5755
		private bool m_IsFixedLength;

		// Token: 0x0400167C RID: 5756
		private bool m_IsByteOrdered;

		// Token: 0x0400167D RID: 5757
		private Format m_format;

		// Token: 0x0400167E RID: 5758
		private string m_fName;

		// Token: 0x0400167F RID: 5759
		private string m_ValidationMethodName;
	}
}
