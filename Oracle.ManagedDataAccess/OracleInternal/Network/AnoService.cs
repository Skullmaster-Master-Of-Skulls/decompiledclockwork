using System;
using System.Collections.Generic;

namespace OracleInternal.Network
{
	// Token: 0x02000148 RID: 328
	internal class AnoService
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x0008E520 File Offset: 0x0008C720
		internal virtual int Initialize(SessionContext sessCtx)
		{
			this.m_sessCtx = sessCtx;
			this.m_anoComm = this.m_sessCtx.m_ano.m_anoComm;
			this.m_level = 0;
			this.m_selectedDrivers = new byte[0];
			return 1;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0008E554 File Offset: 0x0008C754
		internal virtual void ReceiveServiceData(int numSubPackets)
		{
			throw new NotImplementedException("ReceiveServiceData");
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0008E560 File Offset: 0x0008C760
		internal virtual void ValidateResponse()
		{
			throw new NotImplementedException("ValidateResponse");
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0008E56C File Offset: 0x0008C76C
		internal int NumberOfBytesNeeded()
		{
			return 8 + this.GetServiceDataLength();
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0008E578 File Offset: 0x0008C778
		internal virtual int GetServiceDataLength()
		{
			return 12 + this.m_selectedDrivers.Length;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0008E588 File Offset: 0x0008C788
		internal void SendHeader(int serviceSubPackets)
		{
			this.m_anoComm.WriteUB2(this.m_service);
			this.m_anoComm.WriteUB2(serviceSubPackets);
			this.m_anoComm.WriteUB4(0L);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0008E5B4 File Offset: 0x0008C7B4
		internal virtual void SendServiceData()
		{
			this.SendHeader(2);
			this.m_anoComm.SendVersion();
			this.m_anoComm.SendRaw(this.m_selectedDrivers);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0008E5DC File Offset: 0x0008C7DC
		internal static int[] ReceiveHeader(AnoCommunication anoComm)
		{
			return new int[]
			{
				anoComm.ReadUB2(),
				anoComm.ReadUB2(),
				(int)anoComm.ReadUB4()
			};
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0008E610 File Offset: 0x0008C810
		internal void ReceiveSelection(int numSubPackets)
		{
			this.ReceiveServiceData(numSubPackets);
			this.ValidateResponse();
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0008E620 File Offset: 0x0008C820
		internal virtual void ActivateAlgorithm()
		{
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0008E624 File Offset: 0x0008C824
		internal void createDriversListWithLevel(ref List<int> userChoiceDriversId, int level)
		{
			switch (level)
			{
			case 0:
				userChoiceDriversId.Insert(0, 0);
				return;
			case 1:
				userChoiceDriversId.Clear();
				userChoiceDriversId.Add(0);
				return;
			case 2:
				userChoiceDriversId.Add(0);
				return;
			case 3:
				return;
			default:
				throw new NetworkException(-6304);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0008E67C File Offset: 0x0008C87C
		internal static int translateAnoValue(string level)
		{
			int result = 0;
			if (level != null)
			{
				if (level.Equals("ACCEPTED", StringComparison.InvariantCultureIgnoreCase))
				{
					result = 0;
				}
				else if (level.Equals("REQUESTED", StringComparison.InvariantCultureIgnoreCase))
				{
					result = 2;
				}
				else if (level.Equals("REQUIRED", StringComparison.InvariantCultureIgnoreCase))
				{
					result = 3;
				}
				else if (level.Equals("REJECTED", StringComparison.InvariantCultureIgnoreCase))
				{
					result = 1;
				}
				else
				{
					result = -1;
				}
			}
			return result;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0008E6DC File Offset: 0x0008C8DC
		internal static string[] ValidateUserChoiceDrivers(string[] userList, string[] availList, bool defaultIsAll = true)
		{
			bool flag = userList == null || userList.Length == 0;
			bool flag2 = false;
			bool flag3 = false;
			if (flag)
			{
				if (defaultIsAll)
				{
					flag2 = true;
				}
				else
				{
					flag3 = true;
				}
			}
			else if (userList.Length == 1 && string.Equals(userList[0], "ALL", StringComparison.InvariantCultureIgnoreCase))
			{
				flag2 = true;
			}
			else if (userList.Length == 1 && string.Equals(userList[0], "NONE", StringComparison.InvariantCultureIgnoreCase))
			{
				flag3 = true;
			}
			string[] array;
			if (flag2)
			{
				if (availList[0] == "")
				{
					array = new string[availList.Length - 1];
					Array.Copy(availList, 1, array, 0, array.Length);
				}
				else
				{
					array = availList;
				}
			}
			else if (flag3)
			{
				array = null;
			}
			else
			{
				for (int i = 0; i < userList.Length; i++)
				{
					if (string.IsNullOrEmpty(userList[i]))
					{
						throw new NetworkException(12649);
					}
					bool flag4 = false;
					for (int j = 0; j < availList.Length; j++)
					{
						if (string.Equals(availList[j], userList[i], StringComparison.InvariantCultureIgnoreCase))
						{
							flag4 = true;
							break;
						}
					}
					if (!flag4)
					{
						throw new NetworkException(12649);
					}
				}
				array = userList;
			}
			return array;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0008E7D4 File Offset: 0x0008C9D4
		internal byte GetDriverID(string[] driverClasses, string str)
		{
			byte b = 0;
			while ((int)b < driverClasses.Length)
			{
				if (str.Equals(driverClasses[(int)b], StringComparison.InvariantCultureIgnoreCase))
				{
					return b;
				}
				b += 1;
			}
			throw new NetworkException(-6309);
		}

		// Token: 0x04000E03 RID: 3587
		internal const int ACCEPTED = 0;

		// Token: 0x04000E04 RID: 3588
		internal const int REJECTED = 1;

		// Token: 0x04000E05 RID: 3589
		internal const int REQUESTED = 2;

		// Token: 0x04000E06 RID: 3590
		internal const int REQUIRED = 3;

		// Token: 0x04000E07 RID: 3591
		internal const string AUTHENTICATION_NTS = "NTS";

		// Token: 0x04000E08 RID: 3592
		internal const string AUTHENTICATION_RADIUS = "RADIUS";

		// Token: 0x04000E09 RID: 3593
		internal const string AUTHENTICATION_KERBEROS5 = "KERBEROS5";

		// Token: 0x04000E0A RID: 3594
		internal const string AUTHENTICATION_TCPS = "TCPS";

		// Token: 0x04000E0B RID: 3595
		internal const string ENCRYPTION_RC4_40 = "RC4_40";

		// Token: 0x04000E0C RID: 3596
		internal const string ENCRYPTION_RC4_56 = "RC4_56";

		// Token: 0x04000E0D RID: 3597
		internal const string ENCRYPTION_RC4_128 = "RC4_128";

		// Token: 0x04000E0E RID: 3598
		internal const string ENCRYPTION_RC4_256 = "RC4_256";

		// Token: 0x04000E0F RID: 3599
		internal const string ENCRYPTION_DES40C = "DES40C";

		// Token: 0x04000E10 RID: 3600
		internal const string ENCRYPTION_DES56C = "DES56C";

		// Token: 0x04000E11 RID: 3601
		internal const string ENCRYPTION_3DES112 = "3DES112";

		// Token: 0x04000E12 RID: 3602
		internal const string ENCRYPTION_3DES168 = "3DES168";

		// Token: 0x04000E13 RID: 3603
		internal const string ENCRYPTION_AES128 = "AES128";

		// Token: 0x04000E14 RID: 3604
		internal const string ENCRYPTION_AES192 = "AES192";

		// Token: 0x04000E15 RID: 3605
		internal const string ENCRYPTION_AES256 = "AES256";

		// Token: 0x04000E16 RID: 3606
		internal const string CHECKSUM_MD5 = "MD5";

		// Token: 0x04000E17 RID: 3607
		internal const string CHECKSUM_SHA1 = "SHA1";

		// Token: 0x04000E18 RID: 3608
		internal const string CHECKSUM_SHA512 = "SHA512";

		// Token: 0x04000E19 RID: 3609
		internal const string CHECKSUM_SHA256 = "SHA256";

		// Token: 0x04000E1A RID: 3610
		internal const string CHECKSUM_SHA384 = "SHA384";

		// Token: 0x04000E1B RID: 3611
		internal const string ANO_ACCEPTED = "ACCEPTED";

		// Token: 0x04000E1C RID: 3612
		internal const string ANO_REJECTED = "REJECTED";

		// Token: 0x04000E1D RID: 3613
		internal const string ANO_REQUESTED = "REQUESTED";

		// Token: 0x04000E1E RID: 3614
		internal const string ANO_REQUIRED = "REQUIRED";

		// Token: 0x04000E1F RID: 3615
		internal AnoCommunication m_anoComm;

		// Token: 0x04000E20 RID: 3616
		internal SessionContext m_sessCtx;

		// Token: 0x04000E21 RID: 3617
		internal List<int> m_userChoiceDriversId;

		// Token: 0x04000E22 RID: 3618
		internal byte[] m_selectedDrivers;

		// Token: 0x04000E23 RID: 3619
		internal int m_service;

		// Token: 0x04000E24 RID: 3620
		internal long m_version;

		// Token: 0x04000E25 RID: 3621
		internal int m_level;

		// Token: 0x04000E26 RID: 3622
		internal int m_algID = -1;
	}
}
