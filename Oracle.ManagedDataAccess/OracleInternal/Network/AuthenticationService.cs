using System;
using System.Collections.Generic;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x0200014A RID: 330
	internal class AuthenticationService : AnoService
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x0008EE04 File Offset: 0x0008D004
		internal AuthenticationService()
		{
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0008EE0C File Offset: 0x0008D00C
		internal override int Initialize(SessionContext sessCtx)
		{
			base.Initialize(sessCtx);
			this.m_service = 1;
			this.m_status = 64767;
			string[] array = SqlNetOraConfig.SqlNetAuthenticationServices;
			array = AnoService.ValidateUserChoiceDrivers(array, AuthenticationService.AUTH_ORACLE_NAME, false);
			if (array == null)
			{
				return 0;
			}
			this.m_userChoiceDriversId = new List<int>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.m_userChoiceDriversId.Add((int)base.GetDriverID(AuthenticationService.AUTH_ORACLE_NAME, array[i]));
			}
			return 1;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0008EE84 File Offset: 0x0008D084
		internal override int GetServiceDataLength()
		{
			int num = 20;
			if (this.m_userChoiceDriversId != null)
			{
				for (int i = 0; i < this.m_userChoiceDriversId.Count; i++)
				{
					num += 5;
					num += 4 + AuthenticationService.AUTH_ORACLE_NAME[this.m_userChoiceDriversId[i]].Length;
				}
			}
			return num;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0008EED4 File Offset: 0x0008D0D4
		internal override void SendServiceData()
		{
			int num = 3;
			if (this.m_userChoiceDriversId != null)
			{
				num += this.m_userChoiceDriversId.Count * 2;
			}
			base.SendHeader(num);
			this.m_anoComm.SendVersion();
			this.m_anoComm.SendUB2(57569);
			this.m_anoComm.SendStatus(this.m_status);
			if (this.m_userChoiceDriversId != null)
			{
				for (int i = 0; i < this.m_userChoiceDriversId.Count; i++)
				{
					this.m_anoComm.SendUB1((short)AuthenticationService.AUTH_ORACLE_ID[this.m_userChoiceDriversId[i]]);
					this.m_anoComm.SendString(AuthenticationService.AUTH_ORACLE_NAME[this.m_userChoiceDriversId[i]]);
				}
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0008EF88 File Offset: 0x0008D188
		internal override void ReceiveServiceData(int numSubPackets)
		{
			this.m_version = this.m_anoComm.ReceiveVersion();
			int num = this.m_anoComm.ReceiveStatus();
			if (num == 64255 && numSubPackets > 2)
			{
				this.m_anoComm.ReceiveUB1();
				string authenticationService = this.m_anoComm.ReceiveString();
				this.m_authenticationService = authenticationService;
				if (numSubPackets > 4)
				{
					this.m_anoComm.ReceiveVersion();
					this.m_anoComm.ReceiveUB4();
					this.m_anoComm.ReceiveUB4();
				}
				this.m_authenticationActivated = true;
				return;
			}
			if (num == 64511)
			{
				this.m_authenticationActivated = false;
				return;
			}
			throw new NetworkException(-6307);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0008F028 File Offset: 0x0008D228
		internal override void ValidateResponse()
		{
			bool authenticationActivated = this.m_authenticationActivated;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0008F034 File Offset: 0x0008D234
		internal override void ActivateAlgorithm()
		{
		}

		// Token: 0x04000E45 RID: 3653
		internal const int NAU_OK = 64255;

		// Token: 0x04000E46 RID: 3654
		internal const int NAU_DONT_USE_AUTH = 64511;

		// Token: 0x04000E47 RID: 3655
		internal const int NAU_AUTH_NOT_REQUIRED = 64767;

		// Token: 0x04000E48 RID: 3656
		internal const int NAU_AUTH_REQUIRED = 65023;

		// Token: 0x04000E49 RID: 3657
		internal const int NAU_NO_DRIVERS_LINKED_IN = 65279;

		// Token: 0x04000E4A RID: 3658
		internal const int NAU_USE_IMPLICIT_AUTH = 63999;

		// Token: 0x04000E4B RID: 3659
		internal const int NAU_PROXY_NO_AUTH = 63743;

		// Token: 0x04000E4C RID: 3660
		internal const int NAU_AUTH_DISABLED = 63487;

		// Token: 0x04000E4D RID: 3661
		internal const int NAUCX_CLIENT_SERVER = 57569;

		// Token: 0x04000E4E RID: 3662
		internal static string[] AUTH_ORACLE_NAME = new string[]
		{
			"",
			"NTS",
			"KERBEROS5",
			"TCPS"
		};

		// Token: 0x04000E4F RID: 3663
		internal static byte[] AUTH_ORACLE_ID = new byte[]
		{
			0,
			1,
			1,
			2
		};

		// Token: 0x04000E50 RID: 3664
		internal bool m_authenticationActivated;

		// Token: 0x04000E51 RID: 3665
		internal int m_status;

		// Token: 0x04000E52 RID: 3666
		internal string m_authenticationService;
	}
}
