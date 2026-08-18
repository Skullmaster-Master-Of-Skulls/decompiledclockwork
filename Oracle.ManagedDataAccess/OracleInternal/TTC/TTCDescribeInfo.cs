using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000224 RID: 548
	internal class TTCDescribeInfo
	{
		// Token: 0x0600144F RID: 5199 RVA: 0x000D8DF4 File Offset: 0x000D6FF4
		internal static void ReadMessage(bool bForDescribe, bool bForRefCursor, MarshallingEngine mEngine, SQLMetaData sqlMetaData, bool bIgnoreMetadata)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bIgnoreMetadata = false;
			try
			{
				if (!bForDescribe)
				{
					int len = (int)mEngine.UnmarshalUB1(false);
					if (!bForRefCursor)
					{
						mEngine.GetNBytes_ScanOnly(len);
					}
					if (bIgnoreMetadata)
					{
						mEngine.UnmarshalUB4(true);
					}
					else
					{
						sqlMetaData.m_maxRowSize = (int)mEngine.UnmarshalUB4(false);
					}
				}
				if (bForDescribe)
				{
					if (bIgnoreMetadata)
					{
						mEngine.UnmarshalUB2(true);
					}
					else
					{
						sqlMetaData.m_noOfColumns = (short)mEngine.UnmarshalUB2(false);
					}
				}
				else
				{
					if (bIgnoreMetadata)
					{
						mEngine.UnmarshalUB4(true);
					}
					else
					{
						sqlMetaData.m_noOfColumns = (short)mEngine.UnmarshalUB4(false);
					}
					if (sqlMetaData.m_noOfColumns > 0)
					{
						mEngine.UnmarshalUB1(true);
					}
				}
				if (!bIgnoreMetadata)
				{
					if (sqlMetaData.m_columnDescribeInfo == null)
					{
						sqlMetaData.m_columnDescribeInfo = new ColumnDescribeInfo[(int)sqlMetaData.m_noOfColumns];
					}
					else if (sqlMetaData.m_columnDescribeInfo.Length != (int)sqlMetaData.m_noOfColumns)
					{
						throw new Exception("Internal Error");
					}
				}
				for (int i = 0; i < (int)sqlMetaData.m_noOfColumns; i++)
				{
					if (!bIgnoreMetadata && sqlMetaData.m_columnDescribeInfo[i] == null)
					{
						sqlMetaData.m_columnDescribeInfo[i] = new ColumnDescribeInfo();
					}
					TTCColumnInfo.ReadMessage(mEngine, sqlMetaData.m_columnDescribeInfo[i], bIgnoreMetadata);
				}
				if (!bForDescribe)
				{
					mEngine.UnmarshalDALC(true, null);
					if (mEngine.NegotiatedTTCVersion >= 3)
					{
						mEngine.UnmarshalUB4(true);
						mEngine.UnmarshalUB4(true);
						if (mEngine.NegotiatedTTCVersion >= 4)
						{
							mEngine.UnmarshalUB4(true);
							mEngine.UnmarshalUB4(true);
							if (mEngine.NegotiatedTTCVersion >= 5)
							{
								mEngine.UnmarshalDALC(true, null);
							}
						}
					}
				}
				sqlMetaData.bGotDescribeInfoFromDB = true;
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
