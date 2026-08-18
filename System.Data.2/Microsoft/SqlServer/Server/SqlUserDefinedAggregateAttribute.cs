using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000072 RID: 114
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class SqlUserDefinedAggregateAttribute : Attribute
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x00047894 File Offset: 0x00046C94
		public SqlUserDefinedAggregateAttribute(Format format)
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x000478D4 File Offset: 0x00046CD4
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x000478E8 File Offset: 0x00046CE8
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < -1 || value > 8000)
				{
					throw ADP.ArgumentOutOfRange(Res.GetString("SQLUDT_MaxByteSizeValue"), "MaxByteSize", value);
				}
				this.m_MaxByteSize = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00047924 File Offset: 0x00046D24
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00047938 File Offset: 0x00046D38
		public bool IsInvariantToDuplicates
		{
			get
			{
				return this.m_fInvariantToDup;
			}
			set
			{
				this.m_fInvariantToDup = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0004794C File Offset: 0x00046D4C
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00047960 File Offset: 0x00046D60
		public bool IsInvariantToNulls
		{
			get
			{
				return this.m_fInvariantToNulls;
			}
			set
			{
				this.m_fInvariantToNulls = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00047974 File Offset: 0x00046D74
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00047988 File Offset: 0x00046D88
		public bool IsInvariantToOrder
		{
			get
			{
				return this.m_fInvariantToOrder;
			}
			set
			{
				this.m_fInvariantToOrder = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0004799C File Offset: 0x00046D9C
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x000479B0 File Offset: 0x00046DB0
		public bool IsNullIfEmpty
		{
			get
			{
				return this.m_fNullIfEmpty;
			}
			set
			{
				this.m_fNullIfEmpty = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000479C4 File Offset: 0x00046DC4
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000479D8 File Offset: 0x00046DD8
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x000479EC File Offset: 0x00046DEC
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

		// Token: 0x040001F1 RID: 497
		private int m_MaxByteSize;

		// Token: 0x040001F2 RID: 498
		private bool m_fInvariantToDup;

		// Token: 0x040001F3 RID: 499
		private bool m_fInvariantToNulls;

		// Token: 0x040001F4 RID: 500
		private bool m_fInvariantToOrder = true;

		// Token: 0x040001F5 RID: 501
		private bool m_fNullIfEmpty;

		// Token: 0x040001F6 RID: 502
		private Format m_format;

		// Token: 0x040001F7 RID: 503
		private string m_fName;

		// Token: 0x040001F8 RID: 504
		public const int MaxByteSizeValue = 8000;
	}
}
