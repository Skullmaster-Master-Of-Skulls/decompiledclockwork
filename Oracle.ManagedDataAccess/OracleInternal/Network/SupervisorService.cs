using System;

namespace OracleInternal.Network
{
	// Token: 0x0200014D RID: 333
	internal class SupervisorService : AnoService
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x0008F9C0 File Offset: 0x0008DBC0
		internal SupervisorService()
		{
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0008F9E0 File Offset: 0x0008DBE0
		internal override int Initialize(SessionContext sessCtx)
		{
			base.Initialize(sessCtx);
			this.m_service = 4;
			this.m_servicesValidated = 0;
			this.m_servicesWanted = 2;
			this.m_servicesArray = new int[4];
			this.m_servicesArray[0] = 4;
			this.m_servicesArray[1] = 1;
			this.m_servicesArray[2] = 2;
			this.m_servicesArray[3] = 3;
			return 1;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0008FA3C File Offset: 0x0008DC3C
		internal byte[] CreateCID()
		{
			return this.m_cid;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0008FA44 File Offset: 0x0008DC44
		internal override void SendServiceData()
		{
			base.SendHeader(3);
			this.m_anoComm.SendVersion();
			this.m_anoComm.SendRaw(this.m_cid);
			this.m_anoComm.SendUB2Array(this.m_servicesArray);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0008FA7C File Offset: 0x0008DC7C
		internal override int GetServiceDataLength()
		{
			return 12 + this.m_cid.Length + 4 + 10 + this.m_servicesArray.Length * 2;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0008FA9C File Offset: 0x0008DC9C
		internal override void ReceiveServiceData(int numSubPackets)
		{
			this.m_version = this.m_anoComm.ReceiveVersion();
			int num = this.m_anoComm.ReceiveStatus();
			if (num != 31)
			{
				throw new NetworkException(-6306);
			}
			this.m_serverServices = this.m_anoComm.receiveUB2Array();
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0008FAE8 File Offset: 0x0008DCE8
		internal override void ValidateResponse()
		{
			for (int i = 0; i < this.m_serverServices.Length; i++)
			{
				int j;
				for (j = 0; j < this.m_servicesArray.Length; j++)
				{
					if (this.m_serverServices[i] == this.m_servicesArray[j])
					{
						this.m_servicesValidated++;
						break;
					}
				}
				if (j == this.m_servicesArray.Length)
				{
					throw new NetworkException(-6320);
				}
			}
			if (this.m_servicesValidated != this.m_servicesWanted)
			{
				throw new NetworkException(-6321);
			}
		}

		// Token: 0x04000E6C RID: 3692
		internal const int NAS_OK = 31;

		// Token: 0x04000E6D RID: 3693
		internal const int NAS_CLIENT_SERVICES_UNAVAILABLE = 47;

		// Token: 0x04000E6E RID: 3694
		internal const int NAS_SERVER_SERVICES_UNAVAILABLE = 63;

		// Token: 0x04000E6F RID: 3695
		internal const int NAS_NO_SERVICES_AVAILABLE = 79;

		// Token: 0x04000E70 RID: 3696
		internal const int NAS_SERVICE_REQUIRED = 95;

		// Token: 0x04000E71 RID: 3697
		internal const int NAS_REQUIRED_SERVICE_UNAVAILABL = 111;

		// Token: 0x04000E72 RID: 3698
		internal const int NAS_SERVICE_UNAVAILABLE = 127;

		// Token: 0x04000E73 RID: 3699
		private byte[] m_cid = new byte[]
		{
			0,
			0,
			16,
			28,
			102,
			236,
			40,
			234
		};

		// Token: 0x04000E74 RID: 3700
		private int[] m_servicesArray;

		// Token: 0x04000E75 RID: 3701
		private int[] m_serverServices;

		// Token: 0x04000E76 RID: 3702
		private int m_servicesValidated;

		// Token: 0x04000E77 RID: 3703
		private int m_servicesWanted;
	}
}
