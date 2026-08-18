using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using OracleInternal.Common;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x02000145 RID: 325
	internal class Ano
	{
		// Token: 0x06000CC3 RID: 3267 RVA: 0x0008D6A4 File Offset: 0x0008B8A4
		public void Initialize(SessionContext sessCtx)
		{
			this.m_sessionContext = sessCtx;
			this.m_sessionContext.m_ano = this;
			this.m_listOfServices = new AnoService[5];
			this.m_anoComm = new AnoCommunication(sessCtx);
			for (Ano.ServicesSupported servicesSupported = Ano.ServicesSupported.AUTHENTICATION; servicesSupported <= Ano.ServicesSupported.SUPERVISOR; servicesSupported++)
			{
				AnoService anoService = null;
				try
				{
					switch (servicesSupported)
					{
					case Ano.ServicesSupported.AUTHENTICATION:
						anoService = new AuthenticationService();
						break;
					case Ano.ServicesSupported.ENCRYPTION:
						anoService = new EncryptionService();
						break;
					case Ano.ServicesSupported.DATAINTEGRITY:
						anoService = new DataIntegrityService();
						break;
					case Ano.ServicesSupported.SUPERVISOR:
						anoService = new SupervisorService();
						break;
					}
				}
				catch (Exception)
				{
					throw new NetworkException(-6308);
				}
				this.m_naFlags |= anoService.Initialize(sessCtx);
				this.m_listOfServices[(int)servicesSupported] = anoService;
			}
			if ((this.m_naFlags & 16) > 0 && (this.m_naFlags & 8) > 0)
			{
				this.m_naFlags &= -17;
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0008D788 File Offset: 0x0008B988
		[EnvironmentPermission(SecurityAction.Assert, Read = "USERNAME")]
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		internal void StartNegotiation()
		{
			int num = 0;
			for (int i = 1; i < 5; i++)
			{
				num += this.m_listOfServices[i].NumberOfBytesNeeded();
			}
			int pktLength = 13 + num;
			this.SendANOHeader(pktLength, 4, 0);
			this.m_listOfServices[4].SendServiceData();
			this.m_listOfServices[1].SendServiceData();
			this.m_listOfServices[2].SendServiceData();
			this.m_listOfServices[3].SendServiceData();
			this.m_anoComm.FlushData();
			int[] array = this.ReceiveANOHeader();
			for (int j = 0; j < array[2]; j++)
			{
				int[] array2 = AnoService.ReceiveHeader(this.m_anoComm);
				if (array2[2] != 0)
				{
					throw new NetworkException(array2[2]);
				}
				this.m_listOfServices[array2[0]].ReceiveSelection(array2[1]);
			}
			for (int k = 1; k < 5; k++)
			{
				this.m_listOfServices[k].ActivateAlgorithm();
			}
			AuthenticationService authenticationService = (AuthenticationService)this.m_listOfServices[1];
			bool flag = false;
			bool flag2 = false;
			if (authenticationService.m_authenticationActivated)
			{
				if (authenticationService.m_authenticationService == "KERBEROS5")
				{
					flag = true;
				}
				else if (authenticationService.m_authenticationService == "NTS")
				{
					flag2 = true;
				}
			}
			int num2 = 0;
			int num3 = 0;
			if (this.clientPK != null)
			{
				num2 += 12 + this.clientPK.Length;
				num3++;
			}
			if (flag)
			{
				num2 += 37;
				num3++;
			}
			else if (flag2)
			{
				num2 += 130;
				num3++;
			}
			if (num2 > 0)
			{
				num2 += 13;
				this.SendANOHeader(num2, num3, 0);
				if (this.clientPK != null)
				{
					this.m_listOfServices[3].SendHeader(1);
					this.m_anoComm.SendRaw(this.clientPK);
				}
				if (flag)
				{
					authenticationService.SendHeader(4);
					this.m_anoComm.SendVersion();
					this.m_anoComm.SendUB4(9L);
					this.m_anoComm.SendUB4(2L);
					this.m_anoComm.SendUB1(1);
				}
				else if (flag2)
				{
					AnoStream innerStream = new AnoStream(this.m_sessionContext.m_transportAdapter, this.m_sessionContext);
					NegotiateStream negotiateStream = new NegotiateStream(innerStream, true);
					negotiateStream.AuthenticateAsClient(CredentialCache.DefaultNetworkCredentials, "", ProtectionLevel.None, TokenImpersonationLevel.Identification);
					return;
				}
				this.m_anoComm.FlushData();
				if (flag)
				{
					SqlNetOraConfig sqlNetOraConfig = new SqlNetOraConfig();
					string krb5Conf = sqlNetOraConfig["sqlnet.kerberos5_conf"];
					string krb5CCName = sqlNetOraConfig["sqlnet.kerberos5_cc_name"];
					this.KerberosHandshake(authenticationService, krb5Conf, krb5CCName);
				}
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0008D9F8 File Offset: 0x0008BBF8
		[DnsPermission(SecurityAction.Assert, Unrestricted = true)]
		private void KerberosHandshake(AuthenticationService AS, string KRB5Conf, string KRB5CCName)
		{
			int[] array = this.ReceiveANOHeader();
			for (int i = 0; i < array[2]; i++)
			{
				int[] array2 = AnoService.ReceiveHeader(this.m_anoComm);
				if (array2[2] != 0)
				{
					throw new NetworkException(array2[2]);
				}
			}
			string text = this.m_anoComm.ReceiveString();
			string text2 = this.m_anoComm.ReceiveString();
			if (string.IsNullOrEmpty(text))
			{
				throw new NetworkException(-6330, new object[]
				{
					"Service Name not received"
				});
			}
			if (string.IsNullOrEmpty(text2))
			{
				throw new NetworkException(-6330, new object[]
				{
					"Server hostname not received"
				});
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.None, new string[]
				{
					"SN = " + text + ". Server HOSTNAME = " + text2
				});
			}
			if (string.IsNullOrEmpty(KRB5Conf))
			{
				throw new NetworkException(-6330, new object[]
				{
					"SQLNET.KERBEROS5_CONF missing"
				});
			}
			if (string.IsNullOrEmpty(KRB5CCName))
			{
				throw new NetworkException(-6330, new object[]
				{
					"SQLNET.KERBEROS5_CC_NAME missing"
				});
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.None, new string[]
				{
					"SQLNET.KERBEROS5_CONF = " + KRB5Conf + ". SQLNET.KERBEROS_CC_NAME = " + KRB5CCName
				});
			}
			byte[] array3;
			try
			{
				string assemblyString = string.Format("Oracle.ManagedDataAccessIOP, Version={0}, Culture=neutral, PublicKeyToken=89b483f429c47342", ConfigBaseClass.m_assemblyVersion);
				Assembly assembly = Assembly.Load(assemblyString);
				Type type = assembly.GetType("OracleInternal.Network.Kerberos.Kerberos5");
				ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
				try
				{
					object obj = constructor.Invoke(null);
					MethodInfo method = type.GetMethod("Authenticate");
					object[] parameters = new object[]
					{
						KRB5Conf,
						KRB5CCName,
						text,
						text2
					};
					array3 = (byte[])method.Invoke(obj, parameters);
				}
				catch (Exception ex)
				{
					if (ex.InnerException != null)
					{
						throw ex.InnerException;
					}
					throw ex;
				}
			}
			catch (Exception ex2)
			{
				throw new NetworkException(-6330, new object[]
				{
					ex2.Message
				});
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses("");
			if (hostAddresses == null)
			{
				throw new NetworkException(-6330, new object[]
				{
					"Unable to resolve local hostname"
				});
			}
			ushort num;
			if (hostAddresses[0].AddressFamily == AddressFamily.InterNetwork)
			{
				num = 2;
			}
			else
			{
				if (hostAddresses[0].AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NetworkException(-6330, new object[]
					{
						NetworkException.sprintf("Invalid Local Address Family (%d)", new object[]
						{
							hostAddresses[0].AddressFamily
						})
					});
				}
				num = 24;
			}
			byte[] addressBytes = hostAddresses[0].GetAddressBytes();
			int num2 = addressBytes.Length;
			this.SendANOHeader(array3.Length + 43 + num2, 1, 0);
			AS.SendHeader(4);
			this.m_anoComm.SendUB2((int)num);
			this.m_anoComm.SendUB4((long)num2);
			this.m_anoComm.SendRaw(addressBytes);
			this.m_anoComm.SendRaw(array3);
			this.m_anoComm.FlushData();
			array = this.ReceiveANOHeader();
			for (int j = 0; j < array[2]; j++)
			{
				int[] array4 = AnoService.ReceiveHeader(this.m_anoComm);
				if (array4[2] != 0)
				{
					throw new NetworkException(array4[2]);
				}
			}
			int size = this.m_anoComm.ReceivePacketHeader(2);
			this.m_anoComm.ReadUB1();
			size = this.m_anoComm.ReceivePacketHeader(1);
			this.m_anoComm.ReceiveByteArray(size);
			this.SendANOHeader(25, 1, 0);
			AS.SendHeader(1);
			this.m_anoComm.SendPacketHeader(0, 1);
			this.m_anoComm.FlushData();
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0008DDAC File Offset: 0x0008BFAC
		internal void SendANOHeader(int pktLength, int numServices, short errorFlags)
		{
			this.m_anoComm.WriteUB4((long)((ulong)-559038737));
			this.m_anoComm.WriteUB2(pktLength);
			this.m_anoComm.WriteVersion();
			this.m_anoComm.WriteUB2(numServices);
			this.m_anoComm.WriteUB1(errorFlags);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0008DDFC File Offset: 0x0008BFFC
		internal int[] ReceiveANOHeader()
		{
			long num = this.m_anoComm.ReadUB4();
			if (num != (long)((ulong)-559038737))
			{
				throw new NetworkException(2514);
			}
			return new int[]
			{
				this.m_anoComm.ReadUB2(),
				(int)this.m_anoComm.ReadUB4(),
				this.m_anoComm.ReadUB2(),
				(int)this.m_anoComm.ReadUB1()
			};
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0008DE6C File Offset: 0x0008C06C
		internal void setClientPK(byte[] clientPK)
		{
			this.clientPK = clientPK;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0008DE78 File Offset: 0x0008C078
		internal void setInitializationVector(byte[] iv)
		{
			this.iv = iv;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0008DE84 File Offset: 0x0008C084
		internal void setSessionKey(byte[] skey)
		{
			this.skey = skey;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0008DE90 File Offset: 0x0008C090
		internal byte[] getInitializationVector()
		{
			return this.iv;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0008DE98 File Offset: 0x0008C098
		internal byte[] getSessionKey()
		{
			return this.skey;
		}

		// Token: 0x04000DCA RID: 3530
		internal const ushort NAUZTK5_ADDRTYPE_INET = 2;

		// Token: 0x04000DCB RID: 3531
		internal const ushort NAUZTK5_ADDRTYPE_CHAOS = 5;

		// Token: 0x04000DCC RID: 3532
		internal const ushort NAUZTK5_ADDRTYPE_XNS = 6;

		// Token: 0x04000DCD RID: 3533
		internal const ushort NAUZTK5_ADDRTYPE_ISO = 7;

		// Token: 0x04000DCE RID: 3534
		internal const ushort NAUZTK5_ADDRTYPE_DDP = 16;

		// Token: 0x04000DCF RID: 3535
		internal const ushort NAUZTK5_ADDRTYPE_INET6 = 24;

		// Token: 0x04000DD0 RID: 3536
		internal const int NSINAWANTED = 1;

		// Token: 0x04000DD1 RID: 3537
		internal const int NSINAINTCHG = 2;

		// Token: 0x04000DD2 RID: 3538
		internal const int NSINADISABLEFORCONNECTION = 4;

		// Token: 0x04000DD3 RID: 3539
		internal const int NSINANOSERVICES = 8;

		// Token: 0x04000DD4 RID: 3540
		internal const int NSINAREQUIRED = 16;

		// Token: 0x04000DD5 RID: 3541
		internal const int NSINAAUTHWANTED = 32;

		// Token: 0x04000DD6 RID: 3542
		internal const int NUM_SERVICES = 5;

		// Token: 0x04000DD7 RID: 3543
		internal SessionContext m_sessionContext;

		// Token: 0x04000DD8 RID: 3544
		internal AnoCommunication m_anoComm;

		// Token: 0x04000DD9 RID: 3545
		internal int m_naFlags = 1;

		// Token: 0x04000DDA RID: 3546
		internal byte[] clientPK;

		// Token: 0x04000DDB RID: 3547
		internal byte[] iv;

		// Token: 0x04000DDC RID: 3548
		internal byte[] skey;

		// Token: 0x04000DDD RID: 3549
		internal bool foldedinkey;

		// Token: 0x04000DDE RID: 3550
		internal DataIntegrityAlgorithm dataIntegrityAlg;

		// Token: 0x04000DDF RID: 3551
		internal AnoService[] m_listOfServices = new AnoService[5];

		// Token: 0x02000146 RID: 326
		internal enum ServicesSupported
		{
			// Token: 0x04000DE1 RID: 3553
			NONE,
			// Token: 0x04000DE2 RID: 3554
			AUTHENTICATION,
			// Token: 0x04000DE3 RID: 3555
			ENCRYPTION,
			// Token: 0x04000DE4 RID: 3556
			DATAINTEGRITY,
			// Token: 0x04000DE5 RID: 3557
			SUPERVISOR
		}
	}
}
