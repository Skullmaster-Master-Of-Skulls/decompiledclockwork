using System;
using System.Collections.Generic;
using OracleInternal.Common;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x0200020B RID: 523
	internal class TTCPLSQLBooleanAccessor : Accessor
	{
		// Token: 0x0600136B RID: 4971 RVA: 0x000CE7F8 File Offset: 0x000CC9F8
		internal TTCPLSQLBooleanAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (!bForBind)
			{
				this.InitForDataAccess(colMetaData.m_maxLength);
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000CE814 File Offset: 0x000CCA14
		internal object GetBooleanValue(int currentRow)
		{
			object result = null;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					byte b = Accessor.GetValueAt(list, 0);
					if (b == 0)
					{
						result = Convert.ToBoolean(b);
					}
					else if (1 == b)
					{
						result = Convert.ToBoolean(Accessor.GetValueAt(list, 1));
					}
					else
					{
						bool flag = false;
						if ((b & 128) > 0)
						{
							b &= 127;
							flag = true;
						}
						int num2 = (int)Accessor.GetValueAt(list, 1);
						if (flag)
						{
							num2 = -num2;
						}
						if (-1 == num2)
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000CE8A8 File Offset: 0x000CCAA8
		// Note: this type is marked as 'beforefieldinit'.
		static TTCPLSQLBooleanAccessor()
		{
			byte[] false_VAL_BYTES = new byte[1];
			TTCPLSQLBooleanAccessor.FALSE_VAL_BYTES = false_VAL_BYTES;
		}

		// Token: 0x0400149B RID: 5275
		internal const int MAX_BOOLEAN_LENGTH = 2;

		// Token: 0x0400149C RID: 5276
		internal static byte[] TRUE_VAL_BYTES = new byte[]
		{
			1,
			1
		};

		// Token: 0x0400149D RID: 5277
		internal static byte[] FALSE_VAL_BYTES;
	}
}
