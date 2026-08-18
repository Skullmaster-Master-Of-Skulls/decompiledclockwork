using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.TTC
{
	// Token: 0x02000221 RID: 545
	internal class TTCColumnMetaData
	{
		// Token: 0x0600143A RID: 5178 RVA: 0x000D6958 File Offset: 0x000D4B58
		internal static void ReadMessage(MarshallingEngine mEngine, ColumnDescribeInfo colMetaData, bool bIgnoreMetadata)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				if (!bIgnoreMetadata)
				{
					colMetaData.m_dataType = mEngine.UnmarshalUB1(false);
					colMetaData.m_flag = mEngine.UnmarshalUB1(false);
					colMetaData.m_precision = mEngine.UnmarshalUB1(false);
					if (colMetaData.m_dataType == 2 || colMetaData.m_dataType == 187 || colMetaData.m_dataType == 180 || colMetaData.m_dataType == 188 || colMetaData.m_dataType == 181 || colMetaData.m_dataType == 232 || colMetaData.m_dataType == 231 || colMetaData.m_dataType == 190 || colMetaData.m_dataType == 183)
					{
						colMetaData.m_scale = (short)mEngine.UnmarshalUB2(false);
					}
					else
					{
						colMetaData.m_scale = mEngine.UnmarshalUB1(false);
					}
					if (colMetaData.m_scale == -127)
					{
						colMetaData.m_precision = (short)Math.Ceiling((double)colMetaData.m_precision * 0.30103);
						colMetaData.m_scale = 127;
					}
					if (colMetaData.m_dataType == 2 && colMetaData.m_precision == 0 && (colMetaData.m_scale == 0 || colMetaData.m_scale == 127))
					{
						colMetaData.m_precision = 38;
						colMetaData.m_scale = 127;
					}
					colMetaData.m_maxLength = mEngine.UnmarshalSB4();
					if (colMetaData.m_dataType == 101)
					{
						colMetaData.m_maxLength = TTCBinaryDoubleAccessor.BINARY_DOUBLE_MAX_LENGTH;
					}
					else if (colMetaData.m_dataType == 100)
					{
						colMetaData.m_maxLength = TTCBinaryFloatAccessor.BINARY_FLOAT_MAX_LENGTH;
					}
					else if (colMetaData.m_dataType == 12)
					{
						colMetaData.m_maxLength = 7;
					}
					else if (colMetaData.m_dataType == 190 || colMetaData.m_dataType == 183 || colMetaData.m_dataType == 189 || colMetaData.m_dataType == 182)
					{
						colMetaData.m_maxLength = TTCIntervalTypeAccessor.INTERVALTYPE_MAX_LENGTH;
					}
					else if (colMetaData.m_dataType == 11)
					{
						colMetaData.m_maxLength = 128;
					}
					else if (colMetaData.m_dataType == 181)
					{
						colMetaData.m_maxLength = 13;
					}
					colMetaData.m_maxNoOfArrayElements = mEngine.UnmarshalSB4();
					colMetaData.m_contFlag = mEngine.UnmarshalSB4();
					byte[] array = mEngine.UnmarshalDALC(false, mEngine.retLen);
					if (array != null)
					{
						colMetaData.m_toid = new byte[mEngine.retLen[0]];
						Buffer.BlockCopy(array, 0, colMetaData.m_toid, 0, mEngine.retLen[0]);
					}
					colMetaData.m_version = mEngine.UnmarshalUB2(false);
					colMetaData.m_characterSetId = mEngine.UnmarshalUB2(false);
					colMetaData.m_characterSetForm = mEngine.UnmarshalUB1(false);
					colMetaData.m_maxLengthOfChars = (int)mEngine.UnmarshalUB4(false);
					if (mEngine.NegotiatedTTCVersion >= 8)
					{
						colMetaData.m_oaccollid = (int)mEngine.UnmarshalUB4(false);
					}
				}
				else
				{
					mEngine.UnmarshalUB1(true);
					mEngine.UnmarshalUB1(true);
					mEngine.UnmarshalUB1(true);
					if (colMetaData.m_dataType == 2 || colMetaData.m_dataType == 187 || colMetaData.m_dataType == 180 || colMetaData.m_dataType == 188 || colMetaData.m_dataType == 181 || colMetaData.m_dataType == 232 || colMetaData.m_dataType == 231 || colMetaData.m_dataType == 190 || colMetaData.m_dataType == 183)
					{
						mEngine.UnmarshalUB2(true);
					}
					else
					{
						mEngine.UnmarshalUB1(true);
					}
					mEngine.UnmarshalUB4(true);
					mEngine.UnmarshalUB4(true);
					mEngine.UnmarshalUB4(true);
					mEngine.UnmarshalDALC(true, null);
					mEngine.UnmarshalUB2(true);
					mEngine.UnmarshalUB2(true);
					mEngine.UnmarshalUB1(false);
					mEngine.UnmarshalUB4(true);
					if (mEngine.NegotiatedTTCVersion >= 8)
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

		// Token: 0x0600143B RID: 5179 RVA: 0x000D6D50 File Offset: 0x000D4F50
		internal static void WriteMessage(MarshallingEngine mEngine, ColumnDescribeInfo colMetaData)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				mEngine.MarshalUB1(colMetaData.m_dataType);
				mEngine.MarshalUB1(colMetaData.m_flag);
				mEngine.MarshalUB1(colMetaData.m_precision);
				mEngine.MarshalUB1(colMetaData.m_scale);
				mEngine.MarshalUB4((long)colMetaData.m_maxLength);
				mEngine.MarshalSB4(colMetaData.m_maxNoOfArrayElements);
				mEngine.MarshalSB4(colMetaData.m_contFlag);
				mEngine.MarshalDALC(colMetaData.m_toid);
				mEngine.MarshalUB2(colMetaData.m_version);
				mEngine.MarshalUB2(colMetaData.m_characterSetId);
				mEngine.MarshalUB1(colMetaData.m_characterSetForm);
				mEngine.MarshalUB4((long)colMetaData.m_maxLengthOfChars);
				if (mEngine.NegotiatedTTCVersion >= 8)
				{
					mEngine.MarshalUB4((long)colMetaData.m_oaccollid);
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

		// Token: 0x04001655 RID: 5717
		internal const short UACFIND = 1;

		// Token: 0x04001656 RID: 5718
		internal const short UACFALN = 2;

		// Token: 0x04001657 RID: 5719
		internal const short UACFRCP = 4;

		// Token: 0x04001658 RID: 5720
		internal const short UACFBBV = 8;

		// Token: 0x04001659 RID: 5721
		internal const short UACFNCP = 16;

		// Token: 0x0400165A RID: 5722
		internal const short UACFBLP = 32;

		// Token: 0x0400165B RID: 5723
		internal const short UACFARR = 64;

		// Token: 0x0400165C RID: 5724
		internal const short UACFIGN = 128;

		// Token: 0x0400165D RID: 5725
		internal const int UACFNSCL = 1;

		// Token: 0x0400165E RID: 5726
		internal const int UACFBUC = 2;

		// Token: 0x0400165F RID: 5727
		internal const int UACFSKP = 4;

		// Token: 0x04001660 RID: 5728
		internal const int UACFCHRCNT = 8;

		// Token: 0x04001661 RID: 5729
		internal const int UACFNOADJ = 16;

		// Token: 0x04001662 RID: 5730
		internal const int UACFCUS = 4096;

		// Token: 0x04001663 RID: 5731
		internal const int UACFLSZ = 33554432;
	}
}
