using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x0200014C RID: 332
	internal class EncryptionService : AnoService
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x0008F560 File Offset: 0x0008D760
		internal override int Initialize(SessionContext sessCtx)
		{
			base.Initialize(sessCtx);
			this.m_service = 2;
			this.m_level = AnoService.translateAnoValue(SqlNetOraConfig.SqlNetEncryptionClient);
			if (this.m_level == -1)
			{
				throw new NetworkException(-6304);
			}
			string[] array = SqlNetOraConfig.SqlNetEncryptionTypesClient;
			array = AnoService.ValidateUserChoiceDrivers(array, EncryptionService.ENCRYPTION_ANO_ID, true);
			if (array == null)
			{
				return 0;
			}
			this.m_userChoiceDriversId = new List<int>(array.Length);
			this.i = 0;
			while (this.i < array.Length)
			{
				this.m_userChoiceDriversId.Add((int)base.GetDriverID(EncryptionService.ENCRYPTION_ANO_ID, array[this.i]));
				this.i++;
			}
			base.createDriversListWithLevel(ref this.m_userChoiceDriversId, this.m_level);
			this.m_selectedDrivers = new byte[this.m_userChoiceDriversId.Count];
			this.i = 0;
			while (this.i < this.m_selectedDrivers.Length)
			{
				this.m_selectedDrivers[this.i] = this.ENCRYPTION_ORACLE_ID[this.m_userChoiceDriversId[this.i]];
				this.i++;
			}
			int num = 1;
			if (this.m_userChoiceDriversId.Count == 0)
			{
				if (this.m_level == 3)
				{
					throw new NetworkException(-6304);
				}
				num |= 8;
			}
			else if (this.m_level == 3)
			{
				num |= 16;
			}
			return num;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0008F6B4 File Offset: 0x0008D8B4
		internal EncryptionService()
		{
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0008F6EC File Offset: 0x0008D8EC
		internal override void SendServiceData()
		{
			base.SendHeader(3);
			this.m_anoComm.SendVersion();
			this.m_anoComm.SendRaw(this.m_selectedDrivers);
			this.m_anoComm.SendUB1(1);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0008F720 File Offset: 0x0008D920
		internal override int GetServiceDataLength()
		{
			return 17 + this.m_selectedDrivers.Length;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0008F730 File Offset: 0x0008D930
		internal override void ReceiveServiceData(int numSubPackets)
		{
			this.m_version = this.m_anoComm.ReceiveVersion();
			this.m_resp = (int)this.m_anoComm.ReceiveUB1();
			int num = this.m_selectedDrivers.Length;
			if (num <= 0)
			{
				return;
			}
			this.i = 0;
			while (this.i < num)
			{
				if ((int)this.m_selectedDrivers[this.i] == this.m_resp)
				{
					this.m_algID = this.m_resp;
					return;
				}
				this.i++;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0008F7B0 File Offset: 0x0008D9B0
		internal override void ValidateResponse()
		{
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0008F7B4 File Offset: 0x0008D9B4
		internal override void ActivateAlgorithm()
		{
			Ano ano = this.m_sessCtx.m_ano;
			if (this.m_algID != 0)
			{
				switch (this.m_algID)
				{
				case 1:
					this.m_sessCtx.encryptionAlg = new RC4(true, 40);
					break;
				case 6:
					this.m_sessCtx.encryptionAlg = new RC4(true, 256);
					break;
				case 8:
					this.m_sessCtx.encryptionAlg = new RC4(true, 56);
					break;
				case 10:
					this.m_sessCtx.encryptionAlg = new RC4(true, 128);
					break;
				case 11:
					this.m_sessCtx.encryptionAlg = new DES112();
					break;
				case 12:
					this.m_sessCtx.encryptionAlg = new DES168();
					break;
				case 15:
					this.m_sessCtx.encryptionAlg = new AES(1, 1);
					break;
				case 16:
					this.m_sessCtx.encryptionAlg = new AES(1, 2);
					break;
				case 17:
					this.m_sessCtx.encryptionAlg = new AES(1, 3);
					break;
				}
				if (this.m_sessCtx.encryptionAlg == null)
				{
					throw new NetworkException(12649);
				}
				this.m_sessCtx.cryptoBlockSize = 16;
				this.m_sessCtx.encryptionAlg.init(ano.skey, ano.getInitializationVector());
				this.m_sessCtx.cryptoNeeded = true;
			}
		}

		// Token: 0x04000E59 RID: 3673
		internal const int ENCRYPTION_NULL_ID = 0;

		// Token: 0x04000E5A RID: 3674
		internal const int ENCRYPTION_RC4_BAS_ID = 1;

		// Token: 0x04000E5B RID: 3675
		internal const int ENCRYPTION_RC4__56_ID = 8;

		// Token: 0x04000E5C RID: 3676
		internal const int ENCRYPTION_RC4_128_ID = 10;

		// Token: 0x04000E5D RID: 3677
		internal const int ENCRYPTION_RC4_256_ID = 6;

		// Token: 0x04000E5E RID: 3678
		internal const int ENCRYPTION_DES__40_ID = 3;

		// Token: 0x04000E5F RID: 3679
		internal const int ENCRYPTION_DES__56_ID = 2;

		// Token: 0x04000E60 RID: 3680
		internal const int ENCRYPTION_DES_112_ID = 11;

		// Token: 0x04000E61 RID: 3681
		internal const int ENCRYPTION_DES_168_ID = 12;

		// Token: 0x04000E62 RID: 3682
		internal const int ENCRYPTION_AES_128_ID = 15;

		// Token: 0x04000E63 RID: 3683
		internal const int ENCRYPTION_AES_192_ID = 16;

		// Token: 0x04000E64 RID: 3684
		internal const int ENCRYPTION_AES_256_ID = 17;

		// Token: 0x04000E65 RID: 3685
		internal byte[] drivers = new byte[]
		{
			0,
			17,
			6,
			16,
			12,
			15,
			10,
			11,
			8,
			2,
			1,
			3
		};

		// Token: 0x04000E66 RID: 3686
		private int m_resp;

		// Token: 0x04000E67 RID: 3687
		private int i;

		// Token: 0x04000E68 RID: 3688
		internal static string[] ENCRYPTION_ANO_ID = new string[]
		{
			"",
			"RC4_40",
			"RC4_56",
			"RC4_128",
			"RC4_256",
			"DES40C",
			"DES56C",
			"3DES112",
			"3DES168",
			"AES128",
			"AES192",
			"AES256"
		};

		// Token: 0x04000E69 RID: 3689
		private byte[] ENCRYPTION_ORACLE_ID = new byte[]
		{
			0,
			1,
			8,
			10,
			6,
			3,
			2,
			11,
			12,
			15,
			16,
			17
		};

		// Token: 0x04000E6A RID: 3690
		private bool encryptionActivated;

		// Token: 0x04000E6B RID: 3691
		private static int NUM_ENCRYPTION_SUBPACKETS = 2;
	}
}
