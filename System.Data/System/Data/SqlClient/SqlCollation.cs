using System;
using System.Data.SqlTypes;

namespace System.Data.SqlClient
{
	// Token: 0x02000323 RID: 803
	internal sealed class SqlCollation
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x002BE4A8 File Offset: 0x002BD8A8
		// (set) Token: 0x06002A51 RID: 10833 RVA: 0x002BE4C8 File Offset: 0x002BD8C8
		internal int LCID
		{
			get
			{
				return (int)(this.info & 1048575U);
			}
			set
			{
				this.info = ((this.info & 32505856U) | (uint)(value & 1048575));
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x002BE4F8 File Offset: 0x002BD8F8
		// (set) Token: 0x06002A53 RID: 10835 RVA: 0x002BE568 File Offset: 0x002BD968
		internal SqlCompareOptions SqlCompareOptions
		{
			get
			{
				SqlCompareOptions sqlCompareOptions = SqlCompareOptions.None;
				if ((this.info & 1048576U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.IgnoreCase;
				}
				if ((this.info & 2097152U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.IgnoreNonSpace;
				}
				if ((this.info & 4194304U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.IgnoreWidth;
				}
				if ((this.info & 8388608U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.IgnoreKanaType;
				}
				if ((this.info & 16777216U) != 0U)
				{
					sqlCompareOptions |= SqlCompareOptions.BinarySort;
				}
				return sqlCompareOptions;
			}
			set
			{
				uint num = 0U;
				if ((value & SqlCompareOptions.IgnoreCase) != SqlCompareOptions.None)
				{
					num |= 1048576U;
				}
				if ((value & SqlCompareOptions.IgnoreNonSpace) != SqlCompareOptions.None)
				{
					num |= 2097152U;
				}
				if ((value & SqlCompareOptions.IgnoreWidth) != SqlCompareOptions.None)
				{
					num |= 4194304U;
				}
				if ((value & SqlCompareOptions.IgnoreKanaType) != SqlCompareOptions.None)
				{
					num |= 8388608U;
				}
				if ((value & SqlCompareOptions.BinarySort) != SqlCompareOptions.None)
				{
					num |= 16777216U;
				}
				this.info = ((this.info & 1048575U) | num);
			}
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x002BE5D8 File Offset: 0x002BD9D8
		internal string TraceString()
		{
			return string.Format(null, "(LCID={0}, Opts={1})", new object[]
			{
				this.LCID,
				(int)this.SqlCompareOptions
			});
		}

		// Token: 0x04001B91 RID: 7057
		private const uint IgnoreCase = 1048576U;

		// Token: 0x04001B92 RID: 7058
		private const uint IgnoreNonSpace = 2097152U;

		// Token: 0x04001B93 RID: 7059
		private const uint IgnoreWidth = 4194304U;

		// Token: 0x04001B94 RID: 7060
		private const uint IgnoreKanaType = 8388608U;

		// Token: 0x04001B95 RID: 7061
		private const uint BinarySort = 16777216U;

		// Token: 0x04001B96 RID: 7062
		internal const uint MaskLcid = 1048575U;

		// Token: 0x04001B97 RID: 7063
		private const uint MaskCompareOpt = 32505856U;

		// Token: 0x04001B98 RID: 7064
		internal uint info;

		// Token: 0x04001B99 RID: 7065
		internal byte sortId;
	}
}
