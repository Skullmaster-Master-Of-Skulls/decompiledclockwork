using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x0200014B RID: 331
	internal class DataIntegrityService : AnoService
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x0008F088 File Offset: 0x0008D288
		internal DataIntegrityService()
		{
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0008F090 File Offset: 0x0008D290
		internal override int Initialize(SessionContext sessCtx)
		{
			base.Initialize(sessCtx);
			this.m_service = 3;
			this.m_level = AnoService.translateAnoValue(SqlNetOraConfig.SqlNetCryptoChecksumClient);
			if (this.m_level == -1)
			{
				throw new NetworkException(-6322);
			}
			string[] array = SqlNetOraConfig.SqlNetCryptoChecksumTypesClient;
			array = AnoService.ValidateUserChoiceDrivers(array, DataIntegrityService.DATAINTEGRITY_ANO_ID, true);
			if (array == null)
			{
				return 0;
			}
			this.m_userChoiceDriversId = new List<int>(array.Length);
			this.i = 0;
			while (this.i < array.Length)
			{
				this.m_userChoiceDriversId.Add((int)base.GetDriverID(DataIntegrityService.DATAINTEGRITY_ANO_ID, array[this.i]));
				this.i++;
			}
			base.createDriversListWithLevel(ref this.m_userChoiceDriversId, this.m_level);
			this.m_selectedDrivers = new byte[this.m_userChoiceDriversId.Count];
			this.i = 0;
			while (this.i < this.m_selectedDrivers.Length)
			{
				this.m_selectedDrivers[this.i] = DataIntegrityService.DATAINTEGRITY_ORACLE_ID[this.m_userChoiceDriversId[this.i]];
				this.i++;
			}
			int num = 1;
			if (this.m_userChoiceDriversId.Count == 0)
			{
				if (this.m_level == 3)
				{
					throw new NetworkException(-6322);
				}
				num |= 8;
			}
			else if (this.m_level == 3)
			{
				num |= 16;
			}
			return num;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0008F1E4 File Offset: 0x0008D3E4
		internal override int GetServiceDataLength()
		{
			return 12 + this.m_selectedDrivers.Length;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0008F200 File Offset: 0x0008D400
		internal override void ReceiveServiceData(int numSubPackets)
		{
			this.m_version = this.m_anoComm.ReceiveVersion();
			int num = (int)this.m_anoComm.ReceiveUB1();
			this.m_algID = -1;
			for (int i = 0; i < DataIntegrityService.DATAINTEGRITY_ANO_ID.Length; i++)
			{
				if ((int)DataIntegrityService.DATAINTEGRITY_ORACLE_ID[i] == num)
				{
					this.m_algID = i;
				}
			}
			this.checkSummingActivated = (this.m_algID > 0);
			if (numSubPackets == 8)
			{
				ushort num2 = (ushort)this.m_anoComm.ReceiveUB2();
				ushort num3 = (ushort)this.m_anoComm.ReceiveUB2();
				byte[] base_ora = this.m_anoComm.ReceiveRaw();
				byte[] array = this.m_anoComm.ReceiveRaw();
				byte[] array2 = this.m_anoComm.ReceiveRaw();
				byte[] initializationVector = this.m_anoComm.ReceiveRaw();
				if (num2 <= 0 || num3 <= 0)
				{
					throw new Exception("Bad parameters from server");
				}
				int num4 = (int)((num3 + 7) / 8);
				if (array2.Length != num4 || array.Length != num4)
				{
					throw new Exception("DiffieHellman negotiation out of synch");
				}
				DiffieHellman diffieHellman = new DiffieHellman(base_ora, array, num2, num3);
				this.clientPK = diffieHellman.getPublicKey();
				this.m_sessCtx.m_ano.setClientPK(this.clientPK);
				this.m_sessCtx.m_ano.setInitializationVector(initializationVector);
				this.m_sessCtx.m_ano.setSessionKey(diffieHellman.getSessionKey(array2, array2.Length));
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0008F350 File Offset: 0x0008D550
		internal override void ActivateAlgorithm()
		{
			Ano ano = this.m_sessCtx.m_ano;
			if (this.checkSummingActivated)
			{
				if (DataIntegrityService.DATAINTEGRITY_ANO_ID[this.m_algID].Equals("MD5", StringComparison.InvariantCultureIgnoreCase))
				{
					try
					{
						ano.dataIntegrityAlg = new MD5();
						goto IL_12E;
					}
					catch (Exception inner)
					{
						throw new NetworkException(12649, inner);
					}
				}
				if (DataIntegrityService.DATAINTEGRITY_ANO_ID[this.m_algID].Equals("SHA1", StringComparison.InvariantCultureIgnoreCase))
				{
					try
					{
						ano.dataIntegrityAlg = new SHA1();
						goto IL_12E;
					}
					catch (Exception inner2)
					{
						throw new NetworkException(12649, inner2);
					}
				}
				if (DataIntegrityService.DATAINTEGRITY_ANO_ID[this.m_algID].Equals("SHA256", StringComparison.InvariantCultureIgnoreCase))
				{
					try
					{
						ano.dataIntegrityAlg = new SHA256();
						goto IL_12E;
					}
					catch (Exception inner3)
					{
						throw new NetworkException(12649, inner3);
					}
				}
				if (DataIntegrityService.DATAINTEGRITY_ANO_ID[this.m_algID].Equals("SHA384", StringComparison.InvariantCultureIgnoreCase))
				{
					try
					{
						ano.dataIntegrityAlg = new SHA384();
						goto IL_12E;
					}
					catch (Exception inner4)
					{
						throw new NetworkException(12649, inner4);
					}
				}
				if (DataIntegrityService.DATAINTEGRITY_ANO_ID[this.m_algID].Equals("SHA512", StringComparison.InvariantCultureIgnoreCase))
				{
					try
					{
						ano.dataIntegrityAlg = new SHA512();
						goto IL_12E;
					}
					catch (Exception inner5)
					{
						throw new NetworkException(12649, inner5);
					}
				}
				throw new NetworkException(12649);
				IL_12E:
				ano.dataIntegrityAlg.init(ano.getSessionKey(), ano.getInitializationVector(), DataIntegrityService.DATAINTEGRITY_ANO_ID[this.m_algID]);
				this.m_sessCtx.cryptoNeeded = true;
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0008F4FC File Offset: 0x0008D6FC
		internal override void ValidateResponse()
		{
		}

		// Token: 0x04000E53 RID: 3667
		private static readonly string[] DATAINTEGRITY_ANO_ID = new string[]
		{
			"",
			"MD5",
			"SHA1",
			"SHA512",
			"SHA256",
			"SHA384"
		};

		// Token: 0x04000E54 RID: 3668
		private static readonly byte[] DATAINTEGRITY_ORACLE_ID = new byte[]
		{
			0,
			1,
			3,
			4,
			5,
			6
		};

		// Token: 0x04000E55 RID: 3669
		private int m_resp;

		// Token: 0x04000E56 RID: 3670
		private int i;

		// Token: 0x04000E57 RID: 3671
		private bool checkSummingActivated;

		// Token: 0x04000E58 RID: 3672
		private byte[] clientPK;
	}
}
