using System;
using System.Configuration;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.MTS
{
	// Token: 0x02000127 RID: 295
	internal class TransXID
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x0008AB84 File Offset: 0x00088D84
		private TransXID(int oraMTSFormatID, int MSDTCFormatID, Guid txnGuid, Guid rmGuid, int branchNum, string machineName, ushort port)
		{
			this.m_opoDTCTxnXID.m_formatID = oraMTSFormatID;
			this.m_txnGuid = txnGuid;
			this.m_rmGuid = rmGuid;
			this.m_branchNum = branchNum;
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			byte[] bytes = utf8Encoding.GetBytes(machineName);
			byte[] array = txnGuid.ToByteArray();
			byte[] array2 = rmGuid.ToByteArray();
			byte[] bytes2 = BitConverter.GetBytes(port);
			byte[] bytes3 = BitConverter.GetBytes(this.m_branchNum);
			byte[] bytes4 = BitConverter.GetBytes(MSDTCFormatID);
			this.m_opoDTCTxnXID.m_gtrid_length = array.Length + 4;
			this.m_opoDTCTxnXID.m_bqual_length = array2.Length + bytes3.Length + bytes4.Length + 1 + bytes2.Length + bytes.Length;
			if (this.m_opoDTCTxnXID.m_bqual_length + this.m_opoDTCTxnXID.m_gtrid_length > 128)
			{
				int num = 128 - this.m_opoDTCTxnXID.m_gtrid_length - TransXID.BRANCHREQ_LEN;
				if (bytes.Length > num)
				{
					throw new ConfigurationErrorsException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.MTS_INVALID_CONFIG_VALUES, new string[0]));
				}
			}
			int num2 = 0;
			this.m_opoDTCTxnXID.m_data = new byte[128];
			array.CopyTo(this.m_opoDTCTxnXID.m_data, num2);
			num2 += this.m_opoDTCTxnXID.m_gtrid_length;
			array2.CopyTo(this.m_opoDTCTxnXID.m_data, num2);
			num2 += array2.Length;
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes3);
			}
			bytes3.CopyTo(this.m_opoDTCTxnXID.m_data, num2);
			num2 += bytes3.Length;
			bytes4.CopyTo(this.m_opoDTCTxnXID.m_data, num2);
			num2 += bytes4.Length;
			num2++;
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes2);
			}
			bytes2.CopyTo(this.m_opoDTCTxnXID.m_data, num2);
			num2 += bytes2.Length;
			bytes.CopyTo(this.m_opoDTCTxnXID.m_data, num2);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0008AD6C File Offset: 0x00088F6C
		~TransXID()
		{
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0008AD94 File Offset: 0x00088F94
		internal static TransXID CreateOracleXID(Guid txnGuid, Guid rmGuid, int branchNum)
		{
			return new TransXID(1145324612, 21255235, txnGuid, rmGuid, branchNum, ConfigBaseClass.m_recoveryServiceHost, ConfigBaseClass.m_recoveryServicePort);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0008ADB4 File Offset: 0x00088FB4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.m_txnGuid,
				"==",
				this.m_rmGuid,
				"==",
				this.m_branchNum
			});
		}

		// Token: 0x04000D80 RID: 3456
		private const int ORAMTSTXNFORMAT = 1145324612;

		// Token: 0x04000D81 RID: 3457
		private const int MSDTCTXNFORMAT = 21255235;

		// Token: 0x04000D82 RID: 3458
		internal const int ORAXIDSIZE = 128;

		// Token: 0x04000D83 RID: 3459
		internal const int GUIDLEN = 16;

		// Token: 0x04000D84 RID: 3460
		internal const int MAXBRANCHGUILD_LEN = 64;

		// Token: 0x04000D85 RID: 3461
		internal static int BRANCHREQ_LEN = 27;

		// Token: 0x04000D86 RID: 3462
		internal static int MAXRRECOHOSTNAME_LEN = 64 - TransXID.BRANCHREQ_LEN;

		// Token: 0x04000D87 RID: 3463
		internal OpoDTCTxnXIDRefCtx m_opoDTCTxnXID = new OpoDTCTxnXIDRefCtx();

		// Token: 0x04000D88 RID: 3464
		private Guid m_rmGuid;

		// Token: 0x04000D89 RID: 3465
		private Guid m_txnGuid;

		// Token: 0x04000D8A RID: 3466
		private int m_branchNum;
	}
}
