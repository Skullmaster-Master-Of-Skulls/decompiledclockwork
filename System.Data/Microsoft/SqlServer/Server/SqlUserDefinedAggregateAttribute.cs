using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200029E RID: 670
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class SqlUserDefinedAggregateAttribute : Attribute
	{
		// Token: 0x06002286 RID: 8838 RVA: 0x0028C248 File Offset: 0x0028B648
		public SqlUserDefinedAggregateAttribute(Format format)
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

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0028C298 File Offset: 0x0028B698
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x0028C2B8 File Offset: 0x0028B6B8
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < 0 || value > 8000)
				{
					throw ADP.ArgumentOutOfRange(Res.GetString("SQLUDT_MaxByteSizeValue"), "MaxByteSize", value);
				}
				this.m_MaxByteSize = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x0028C2F8 File Offset: 0x0028B6F8
		// (set) Token: 0x0600228A RID: 8842 RVA: 0x0028C318 File Offset: 0x0028B718
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

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x0028C338 File Offset: 0x0028B738
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x0028C358 File Offset: 0x0028B758
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

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x0028C378 File Offset: 0x0028B778
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x0028C398 File Offset: 0x0028B798
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

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x0028C3B8 File Offset: 0x0028B7B8
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x0028C3D8 File Offset: 0x0028B7D8
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

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x0028C3F8 File Offset: 0x0028B7F8
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x0028C418 File Offset: 0x0028B818
		// (set) Token: 0x06002293 RID: 8851 RVA: 0x0028C438 File Offset: 0x0028B838
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

		// Token: 0x0400166D RID: 5741
		public const int MaxByteSizeValue = 8000;

		// Token: 0x0400166E RID: 5742
		private int m_MaxByteSize;

		// Token: 0x0400166F RID: 5743
		private bool m_fInvariantToDup;

		// Token: 0x04001670 RID: 5744
		private bool m_fInvariantToNulls;

		// Token: 0x04001671 RID: 5745
		private bool m_fInvariantToOrder = true;

		// Token: 0x04001672 RID: 5746
		private bool m_fNullIfEmpty;

		// Token: 0x04001673 RID: 5747
		private Format m_format;

		// Token: 0x04001674 RID: 5748
		private string m_fName;
	}
}
