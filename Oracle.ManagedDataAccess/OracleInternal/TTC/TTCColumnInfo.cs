using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000220 RID: 544
	internal class TTCColumnInfo
	{
		// Token: 0x06001438 RID: 5176 RVA: 0x000D67A8 File Offset: 0x000D49A8
		internal static void ReadMessage(MarshallingEngine mEngine, ColumnDescribeInfo colMetaData, bool bIgnoreMetadata)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				TTCColumnMetaData.ReadMessage(mEngine, colMetaData, bIgnoreMetadata);
				char[] array = null;
				if (!bIgnoreMetadata)
				{
					int[] array2 = new int[1];
					short num = mEngine.UnmarshalUB1(false);
					colMetaData.m_isNullAllowed = (num > 0);
					mEngine.UnmarshalUB1(false);
					byte[] array3 = mEngine.UnmarshalDALC(false, array2);
					if (array3 != null)
					{
						if (array == null)
						{
							array = mEngine.m_charArrayPooler.Dequeue();
						}
						colMetaData.pColAlias = mEngine.m_dbCharSetConv.ConvertBytesToString(array3, 0, array2[0], array, true);
					}
					array3 = mEngine.UnmarshalDALC(true, null);
					if ((array3 = mEngine.UnmarshalDALC(false, array2)) != null)
					{
						if (array == null)
						{
							array = mEngine.m_charArrayPooler.Dequeue();
						}
						string text = mEngine.m_dbCharSetConv.ConvertBytesToString(array3, 0, array2[0], array, true);
						if (text.Equals("XMLTYPE", StringComparison.InvariantCultureIgnoreCase))
						{
							colMetaData.m_dataType = 109;
							colMetaData.bIsXmlType = true;
						}
					}
					if (array != null)
					{
						mEngine.m_charArrayPooler.Enqueue(ref array);
					}
				}
				else
				{
					mEngine.UnmarshalUB1(true);
					mEngine.UnmarshalUB1(true);
					mEngine.UnmarshalDALC(true, null);
					mEngine.UnmarshalDALC(true, null);
					mEngine.UnmarshalDALC(true, null);
				}
				if (mEngine.NegotiatedTTCVersion >= 3)
				{
					mEngine.UnmarshalUB2(true);
					if (mEngine.NegotiatedTTCVersion >= 6)
					{
						mEngine.UnmarshalUB4(true);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}
	}
}
