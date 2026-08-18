using System;
using System.Collections;
using System.Security.Permissions;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;
using OracleInternal.Secure.Logon;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC
{
	// Token: 0x02000219 RID: 537
	internal class TTCAuthenticate : TTCFunction
	{
		// Token: 0x06001400 RID: 5120 RVA: 0x000D2484 File Offset: 0x000D0684
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		internal TTCAuthenticate(MarshallingEngine marshallingEngine, int lcid) : base(marshallingEngine, 118, 0)
		{
			this.m_authTerminal = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_TERMINAL", 0, "AUTH_TERMINAL".Length, true);
			this.m_authProgramName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_PROGRAM_NM", 0, "AUTH_PROGRAM_NM".Length, true);
			this.m_authMachine = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_MACHINE", 0, "AUTH_MACHINE".Length, true);
			this.m_authPid = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_PID", 0, "AUTH_PID".Length, true);
			this.m_auth_pbkdf2_csk_salt = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_PBKDF2_CSK_SALT", 0, "AUTH_PBKDF2_CSK_SALT".Length, true);
			this.m_auth_pbkdf2_sder_count = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_PBKDF2_SDER_COUNT", 0, "AUTH_PBKDF2_SDER_COUNT".Length, true);
			this.m_auth_pbkdf2_vgen_count = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_PBKDF2_VGEN_COUNT", 0, "AUTH_PBKDF2_VGEN_COUNT".Length, true);
			this.m_auth_pbkdf2_speedy_key = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_PBKDF2_SPEEDY_KEY", 0, "AUTH_PBKDF2_SPEEDY_KEY".Length, true);
			this.m_authSid = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_SID", 0, "AUTH_SID".Length, true);
			this.m_authPassword = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_PASSWORD", 0, "AUTH_PASSWORD".Length, true);
			this.m_authNewPassword = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_NEWPASSWORD", 0, "AUTH_NEWPASSWORD".Length, true);
			this.m_authDebugJDWPValue = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_ORA_DEBUG_JDWP", 0, "AUTH_ORA_DEBUG_JDWP".Length, true);
			this.m_authSessionKey = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_SESSKEY", 0, "AUTH_SESSKEY".Length, true);
			this.m_authVerifierData = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_VFR_DATA", 0, "AUTH_VFR_DATA".Length, true);
			this.m_authAlterSession = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_ALTER_SESSION", 0, "AUTH_ALTER_SESSION".Length, true);
			this.m_authProxyClientName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("PROXY_CLIENT_NAME", 0, "PROXY_CLIENT_NAME".Length, true);
			this.m_authSessionId = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_SESSION_ID", 0, "AUTH_SESSION_ID".Length, true);
			this.m_authSerialNum = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_SERIAL_NUM", 0, "AUTH_SERIAL_NUM".Length, true);
			this.m_authConnectString = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_CONNECT_STRING", 0, "AUTH_CONNECT_STRING".Length, true);
			this.m_sessionClientCharSet = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("SESSION_CLIENT_CHARSET", 0, "SESSION_CLIENT_CHARSET".Length, true);
			this.m_sessionClientLibType = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("SESSION_CLIENT_LIB_TYPE", 0, "SESSION_CLIENT_LIB_TYPE".Length, true);
			this.m_sessionClientDriverName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("SESSION_CLIENT_DRIVER_NAME", 0, "SESSION_CLIENT_DRIVER_NAME".Length, true);
			this.m_sessionClientVersion = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("SESSION_CLIENT_VERSION", 0, "SESSION_CLIENT_VERSION".Length, true);
			this.m_sessionClientLobAttr = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("SESSION_CLIENT_LOBATTR", 0, "SESSION_CLIENT_LOBATTR".Length, true);
			this.m_authOraEditionAttr = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_ORA_EDITION", 0, "AUTH_ORA_EDITION".Length, true);
			this.m_drcpConnectionClass = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_KPPL_CONN_CLASS", 0, "AUTH_KPPL_CONN_CLASS".Length, true);
			this.m_drcpTag = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_KPPL_TAG", 0, "AUTH_KPPL_TAG".Length, true);
			this.m_drcpMultipropTag = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_KPPL_IS_MULTIPROP_TAG", 0, "AUTH_KPPL_IS_MULTIPROP_TAG".Length, true);
			this.m_drcpSessionPurity = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_KPPL_PURITY", 0, "AUTH_KPPL_PURITY".Length, true);
			this.m_drcpFixupCB = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("AUTH_KPPL_FIXUP_CB", 0, "AUTH_KPPL_FIXUP_CB".Length, true);
			this.m_terminalName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("unknown", 0, "unknown".Length, true);
			this.m_programName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(ConfigBaseClass.CurrentProcess.ProcessName + ".exe", 0, (ConfigBaseClass.CurrentProcess.ProcessName + ".exe").Length, true);
			string text = Environment.MachineName;
			string userDomainName = Environment.UserDomainName;
			this.m_terminalName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text, 0, Environment.MachineName.Length, true);
			if (!string.IsNullOrWhiteSpace(userDomainName))
			{
				text = userDomainName + "\\" + text;
			}
			this.m_hostName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text, 0, text.Length, true);
			this.m_userName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(Environment.UserName, 0, Environment.UserName.Length, true);
			string text2 = ConfigBaseClass.CurrentProcess.Id.ToString() + ":" + AppDomain.CurrentDomain.Id;
			this.m_processId = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text2, 0, text2.Length, true);
			string connectDescriptor = this.m_marshallingEngine.m_oracleCommunication.ConnectDescriptor;
			this.m_connectstring = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(connectDescriptor, 0, connectDescriptor.Length, true);
			int num = this.ClientVersionStringToInt(ConfigBaseClass.m_assemblyVersion.ToString());
			string text3 = num.ToString();
			this.m_clientVersion = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text3, 0, text3.Length, true);
			int num2 = (int)(((long)num & (long)((ulong)-16777216)) >> 24) & 255;
			int num3 = (num & 15728640) >> 20 & 255;
			int num4 = (num & 1044480) >> 12 & 255;
			int num5 = (num & 3840) >> 8 & 255;
			int num6 = num & 255;
			string str = string.Concat(new object[]
			{
				num2,
				".",
				num3,
				".",
				num4,
				".",
				num5,
				".",
				num6
			});
			string text4 = "ODPM.NET" + " : " + str;
			this.m_clientDriverName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text4, 0, text4.Length, true);
			string text5 = 2002.ToString();
			this.m_clientCharSet = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text5, 0, text5.Length, true);
			this.m_clientLibType = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("0", 0, "0".Length, true);
			this.m_marshallingEngine.m_connImplReference.m_sessionTimeZone.initialZoneId = 0;
			string text6 = OracleGlobalizationImpl.CreateAlterSessionBlockForOAUTH(lcid, ref this.m_marshallingEngine.m_connImplReference.m_sessionTimeZone.initialZoneId);
			this.m_alterSessionSql = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text6, 0, text6.Length, true);
			this.m_alterSessionSql[this.m_alterSessionSql.Length - 1] = 0;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000D2C94 File Offset: 0x000D0E94
		internal void ReInit(MarshallingEngine marshallingEngine, int lcid)
		{
			base.ReInit(marshallingEngine);
			this.m_encryptedSK = null;
			this.m_verifierType = 0;
			this.m_salt = null;
			this.m_encryptedKB = null;
			this.m_xoredKaAndKb = null;
			this.m_conFounder = null;
			this.m_encryptedPassword = null;
			this.m_newEncryptedPassword = null;
			this.m_marshallingEngine.m_connImplReference.m_sessionTimeZone.initialZoneId = 0;
			OracleGlobalizationImpl.CreateAlterSessionBlockForOAUTH(lcid, ref this.m_marshallingEngine.m_connImplReference.m_sessionTimeZone.initialZoneId);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x000D2D14 File Offset: 0x000D0F14
		internal void WriteOSessKeyMessage(string userName, long logonMode)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				byte[] array = null;
				if (!string.IsNullOrEmpty(userName))
				{
					userName = HelperClass.RemoveSingleQuotes(userName);
					array = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(userName, 0, userName.Length, true);
				}
				this.m_functionCode = 118;
				base.WriteFunctionHeader();
				this.m_marshallingEngine.MarshalPointer();
				if (array != null)
				{
					this.m_marshallingEngine.MarshalSB4(array.Length);
				}
				else
				{
					this.m_marshallingEngine.MarshalSB4(0);
				}
				this.m_marshallingEngine.MarshalUB4(logonMode | 1L);
				this.m_marshallingEngine.MarshalPointer();
				byte[][] array2 = new byte[5][];
				byte[][] array3 = new byte[5][];
				byte[] array4 = new byte[5];
				byte[] kvalflg = array4;
				array2[0] = this.m_authTerminal;
				array3[0] = this.m_terminalName;
				array2[1] = this.m_authProgramName;
				array3[1] = this.m_programName;
				array2[2] = this.m_authMachine;
				array3[2] = this.m_hostName;
				array2[3] = this.m_authPid;
				array3[3] = this.m_processId;
				array2[4] = this.m_authSid;
				array3[4] = this.m_userName;
				this.m_marshallingEngine.MarshalUB4((long)array2.Length);
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalPointer();
				if (array != null)
				{
					this.m_marshallingEngine.MarshalCHR(array);
				}
				this.m_marshallingEngine.MarshalKEYVAL(array2, array3, kvalflg, array2.Length);
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

		// Token: 0x06001403 RID: 5123 RVA: 0x000D2ED4 File Offset: 0x000D10D4
		internal void ReadOSessKeyResponse()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			byte[][] array = null;
			byte[][] array2 = null;
			int[] array3 = new int[0];
			try
			{
				this.m_marshallingEngine.TTCErrorObject.Initialize();
				bool flag = false;
				while (!flag)
				{
					try
					{
						byte b = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
						byte b2 = b;
						if (b2 != 4)
						{
							if (b2 != 8)
							{
								if (b2 != 23)
								{
									throw new Exception("ReadOSessKeyResponse: TTC Error");
								}
								base.ProcessServerSidePiggybackFunction();
							}
							else
							{
								int num = this.m_marshallingEngine.UnmarshalUB2(false);
								if (num > 0)
								{
									array = new byte[num][];
									array2 = new byte[num][];
									array3 = this.m_marshallingEngine.UnmarshalKEYVAL(array, array2, num);
								}
							}
						}
						else
						{
							this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
							if (this.m_marshallingEngine.TTCErrorObject.ErrorCode != 0)
							{
								return;
							}
							flag = true;
						}
					}
					catch (NetworkException ex)
					{
						if (ex.ErrorCode != 3111)
						{
							throw;
						}
						this.m_marshallingEngine.ProcessReset();
						return;
					}
					catch (Exception)
					{
						if (this.m_marshallingEngine.m_oraBufRdr != null)
						{
							this.m_marshallingEngine.m_oraBufRdr.ClearState();
						}
						this.m_marshallingEngine.m_oracleCommunication.Break();
						this.m_marshallingEngine.ProcessReset();
						throw;
					}
				}
				if (array == null || array.Length < 1)
				{
					throw new Exception("ReadOSessKeyResponse: TTC Error");
				}
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				for (int i = 0; i < array.Length; i++)
				{
					if (!flag3 && HelperClass.CompareBytes(array[i], this.m_authVerifierData) == 0)
					{
						this.m_salt = array2[i];
						this.m_verifierType = array3[i];
						flag3 = true;
					}
					else if (!flag2 && HelperClass.CompareBytes(array[i], this.m_authSessionKey) == 0)
					{
						this.m_encryptedSK = array2[i];
						flag2 = true;
					}
					else if (!flag4 && HelperClass.CompareBytes(array[i], this.m_auth_pbkdf2_csk_salt) == 0)
					{
						byte[] array4 = array2[i];
						flag4 = true;
						if (array4.Length != 32)
						{
							throw new OracleException(28041, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(28041, new string[0]));
						}
						this.m_pbkdf2_cskSalt = array4;
					}
					else if (!flag5 && HelperClass.CompareBytes(array[i], this.m_auth_pbkdf2_vgen_count) == 0)
					{
						byte[] bytes = array2[i];
						flag5 = true;
						this.m_pbkdf2_vgen_count = int.Parse(Encoding.ASCII.GetString(bytes));
						if (this.m_pbkdf2_vgen_count < 4096 || this.m_pbkdf2_vgen_count > 100000000)
						{
							this.m_pbkdf2_vgen_count = 4096;
						}
					}
					else if (!flag6 && HelperClass.CompareBytes(array[i], this.m_auth_pbkdf2_sder_count) == 0)
					{
						byte[] bytes2 = array2[i];
						flag6 = true;
						this.m_pbkdf2_sder_count = int.Parse(Encoding.ASCII.GetString(bytes2));
						if (this.m_pbkdf2_sder_count < 3 || this.m_pbkdf2_sder_count > 100000000)
						{
							this.m_pbkdf2_sder_count = 3;
						}
					}
				}
				if (this.m_encryptedSK == null || (this.m_encryptedSK.Length != 64 && this.m_encryptedSK.Length != 96))
				{
					throw new Exception("ReadOSessKeyResponse: TTC Error: SessionKey should be either 64 or 96 bytes long.");
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex2, null);
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

		// Token: 0x06001404 RID: 5124 RVA: 0x000D3270 File Offset: 0x000D1470
		internal void EncryptNewPassword(byte[] newPasswordNet)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				string pwdEncStr = "AES/CBC/PKCS5Padding";
				if (newPasswordNet != null)
				{
					this.m_newEncryptedPassword = new byte[256];
					for (int i = 0; i < 256; i++)
					{
						this.m_newEncryptedPassword[i] = 0;
					}
				}
				byte[] array = null;
				O5LogonHelper.ProcessNewPassword(pwdEncStr, newPasswordNet, this.m_xoredKaAndKb, ref this.m_newEncryptedPassword, ref array);
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

		// Token: 0x06001405 RID: 5125 RVA: 0x000D332C File Offset: 0x000D152C
		internal void WriteOAuthMessage(string userStr, string passwordStr, string proxyClientName, bool bProxyAuth, int sessionId, int serialNum, long logonMode, string newPasswordStr, byte logonCompatibility, bool bExternalAuth, bool bSendJDWP, string clientCharSet)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			byte[] userName = null;
			byte[] array = null;
			byte[] array2 = null;
			try
			{
				if (userStr != null && userStr.Length > 0)
				{
					userStr = HelperClass.RemoveSingleQuotes(userStr);
					userName = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(userStr, 0, userStr.Length, true);
					string strInput = userStr.Trim();
					string text = (passwordStr != null) ? passwordStr.Trim() : null;
					string strInput2 = (newPasswordStr != null) ? newPasswordStr.Trim() : null;
					newPasswordStr = (passwordStr = null);
					bool flag = false;
					string noQuotesUser = HelperClass.RemoveSingleAndDoubleQuotes(strInput);
					if (!string.IsNullOrEmpty(text))
					{
						flag = true;
					}
					string text2 = HelperClass.RemoveDoubleQuotes(text);
					if (flag)
					{
						array = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text2, 0, text2.Length, true);
					}
					string text3 = HelperClass.RemoveDoubleQuotes(strInput2);
					if (!string.IsNullOrEmpty(text3))
					{
						array2 = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text3, 0, text3.Length, true);
					}
					if (array == null)
					{
						this.m_encryptedPassword = null;
					}
					else
					{
						O5LogonHelper.DoLogonProcessing(this.m_verifierType, this.m_salt, logonCompatibility, noQuotesUser, text2, array, this.m_encryptedSK, this.m_pbkdf2_cskSalt, this.m_pbkdf2_vgen_count, this.m_pbkdf2_sder_count, this.m_marshallingEngine.m_bSvrCSMultibyte, out this.m_encryptedKB, out this.m_encryptedPassword, out this.m_xoredKaAndKb, out this.m_conFounder, out this.m_pbkdf2_speedy_key);
						if (!bExternalAuth && this.m_encryptedPassword == null)
						{
							this.m_encryptedPassword = new byte[64];
						}
						if (array2 != null)
						{
							this.EncryptNewPassword(array2);
						}
					}
				}
				this.DoMarshalOauth(userName, logonMode, proxyClientName, bProxyAuth, sessionId, serialNum, bExternalAuth, bSendJDWP, clientCharSet);
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

		// Token: 0x06001406 RID: 5126 RVA: 0x000D3534 File Offset: 0x000D1734
		private void DoMarshalOauth(byte[] userName, long logonMode, string proxyClientName, bool bProxyAuth, int sessionId, int serialNum, bool bExternalAuth, bool bSendJDWP, string clientCharSet)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.m_functionCode = 115;
				base.WriteFunctionHeader();
				if (userName != null && userName.Length > 0)
				{
					this.m_marshallingEngine.MarshalPointer();
					this.m_marshallingEngine.MarshalSB4(userName.Length);
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalSB4(0);
				}
				if (bProxyAuth)
				{
					logonMode |= 1024L;
				}
				if (userName != null && userName.Length != 0 && this.m_encryptedPassword != null)
				{
					logonMode |= 256L;
				}
				this.m_marshallingEngine.MarshalUB4(logonMode);
				this.m_marshallingEngine.MarshalPointer();
				int num = 30;
				byte[][] array = new byte[num][];
				byte[][] array2 = new byte[num][];
				byte[] array3 = new byte[num];
				int num2 = 0;
				if (this.m_encryptedKB != null)
				{
					array[num2] = this.m_authSessionKey;
					array2[num2] = this.m_encryptedKB;
					array3[num2++] = 1;
				}
				if (this.m_encryptedPassword != null)
				{
					array[num2] = this.m_authPassword;
					array2[num2++] = this.m_encryptedPassword;
				}
				if (this.m_newEncryptedPassword != null)
				{
					array[num2] = this.m_authNewPassword;
					byte[] array4 = new byte[64];
					Array.Copy(this.m_newEncryptedPassword, array4, 64);
					array2[num2++] = array4;
				}
				if (this.m_pbkdf2_speedy_key != null)
				{
					array[num2] = this.m_auth_pbkdf2_speedy_key;
					array2[num2++] = this.m_pbkdf2_speedy_key;
				}
				string text = ConfigBaseClass.OraDebugJDWP();
				if (bSendJDWP && !string.IsNullOrEmpty(text))
				{
					byte[] valueToEncrypt = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text, 0, text.Length, true);
					byte[] array5 = O5LogonHelper.EncryptOraAuthJDWPValue(bExternalAuth, this.m_marshallingEngine.m_oracleCommunication.m_sessionCtx.m_hisone, this.m_xoredKaAndKb, valueToEncrypt);
					array[num2] = this.m_authDebugJDWPValue;
					array2[num2++] = array5;
				}
				array[num2] = this.m_authTerminal;
				array2[num2++] = this.m_terminalName;
				array[num2] = this.m_authProgramName;
				array2[num2++] = this.m_programName;
				array[num2] = this.m_authMachine;
				array2[num2++] = this.m_hostName;
				array[num2] = this.m_authPid;
				array2[num2++] = this.m_processId;
				array[num2] = this.m_authSid;
				array2[num2++] = this.m_userName;
				array[num2] = this.m_authConnectString;
				array2[num2++] = this.m_connectstring;
				array[num2] = this.m_sessionClientCharSet;
				array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(clientCharSet, 0, clientCharSet.Length, true);
				array[num2] = this.m_sessionClientLibType;
				array2[num2++] = this.m_clientLibType;
				array[num2] = this.m_sessionClientDriverName;
				array2[num2++] = this.m_clientDriverName;
				array[num2] = this.m_sessionClientVersion;
				array2[num2++] = this.m_clientVersion;
				array[num2] = this.m_sessionClientLobAttr;
				array2[num2++] = TTCAuthenticate.m_clientLobAttr;
				string editionName = this.m_marshallingEngine.m_connImplReference.m_editionName;
				if (!string.IsNullOrEmpty(editionName))
				{
					array[num2] = this.m_authOraEditionAttr;
					array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(editionName, 0, editionName.Length, true);
				}
				if (this.m_marshallingEngine.m_bDRCPConnection)
				{
					string drcpconnectionClass = this.m_marshallingEngine.m_connImplReference.DRCPConnectionClass;
					if (!string.IsNullOrEmpty(drcpconnectionClass))
					{
						array[num2] = this.m_drcpConnectionClass;
						array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(drcpconnectionClass, 0, drcpconnectionClass.Length, true);
					}
					string drcptagName = this.m_marshallingEngine.m_connImplReference.DRCPtagName;
					if (!string.IsNullOrEmpty(drcptagName))
					{
						array[num2] = this.m_drcpTag;
						array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(drcptagName, 0, drcptagName.Length, true);
						if (this.m_marshallingEngine.m_connImplReference.m_bDRCPUseMultitag)
						{
							array[num2] = this.m_drcpMultipropTag;
							array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes("TRUE", 0, "TRUE".Length, true);
						}
					}
					string drcpSessionPurity = this.m_marshallingEngine.m_connImplReference.m_drcpSessionPurity;
					if (!string.IsNullOrEmpty(drcpSessionPurity))
					{
						array[num2] = this.m_drcpSessionPurity;
						array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(drcpSessionPurity, 0, drcpSessionPurity.Length, true);
					}
					string drcpPLSQLCallback = this.m_marshallingEngine.m_connImplReference.m_drcpPLSQLCallback;
					if (!string.IsNullOrEmpty(drcpPLSQLCallback) && this.m_marshallingEngine.NegotiatedTTCVersion >= 8)
					{
						array[num2] = this.m_drcpFixupCB;
						array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(drcpPLSQLCallback, 0, drcpPLSQLCallback.Length, true);
					}
				}
				if (this.m_authAlterSession != null)
				{
					array[num2] = this.m_authAlterSession;
					array2[num2] = this.m_alterSessionSql;
					array3[num2++] = 1;
				}
				if (!string.IsNullOrEmpty(proxyClientName))
				{
					array[num2] = this.m_authProxyClientName;
					array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(proxyClientName, 0, proxyClientName.Length, true);
				}
				if (sessionId != -1)
				{
					array[num2] = this.m_authSessionId;
					array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(sessionId.ToString(), 0, sessionId.ToString().Length, true);
				}
				if (serialNum != -1)
				{
					array[num2] = this.m_authSerialNum;
					array2[num2++] = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(serialNum.ToString(), 0, serialNum.ToString().Length, true);
				}
				this.m_marshallingEngine.MarshalUB4((long)num2);
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalPointer();
				if (userName != null && userName.Length > 0)
				{
					this.m_marshallingEngine.MarshalCHR(userName);
				}
				this.m_marshallingEngine.MarshalKEYVAL(array, array2, array3, num2);
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

		// Token: 0x06001407 RID: 5127 RVA: 0x000D3B80 File Offset: 0x000D1D80
		internal bool ReceiveOAuthResponse()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			byte[][] array = null;
			byte[][] array2 = null;
			int num = 0;
			bool flag = false;
			bool result;
			try
			{
				this.m_marshallingEngine.TTCErrorObject.Initialize();
				bool flag2 = false;
				while (!flag2)
				{
					try
					{
						byte b = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
						byte b2 = b;
						if (b2 <= 8)
						{
							if (b2 != 4)
							{
								if (b2 == 8)
								{
									num = this.m_marshallingEngine.UnmarshalUB2(false);
									if (num > 0)
									{
										array = new byte[num][];
										array2 = new byte[num][];
										this.m_marshallingEngine.UnmarshalKEYVAL(array, array2, num);
										continue;
									}
									continue;
								}
							}
							else
							{
								this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
								if (this.m_marshallingEngine.TTCErrorObject.ErrorCode != 0)
								{
									return false;
								}
								flag2 = true;
								continue;
							}
						}
						else
						{
							if (b2 == 15)
							{
								this.m_marshallingEngine.TTCErrorObject.ReadWarning();
								continue;
							}
							if (b2 == 23)
							{
								base.ProcessServerSidePiggybackFunction();
								continue;
							}
						}
						throw new Exception("ReceiveOAuthResponse: TTC Error");
					}
					catch (NetworkException ex)
					{
						if (ex.ErrorCode != 3111)
						{
							throw;
						}
						this.m_marshallingEngine.ProcessReset();
						return false;
					}
					catch (Exception)
					{
						if (this.m_marshallingEngine.m_oraBufRdr != null)
						{
							this.m_marshallingEngine.m_oraBufRdr.ClearState();
						}
						this.m_marshallingEngine.m_oracleCommunication.Break();
						this.m_marshallingEngine.ProcessReset();
						throw;
					}
				}
				if (num > 0)
				{
					this.m_sessionProperties = new Hashtable();
					string text = string.Empty;
					string value = string.Empty;
					for (int i = 0; i < num; i++)
					{
						if (array[i] != null)
						{
							text = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(array[i], 0, array[i].Length, null, true);
							if (text != "AUTH_SVR_RESPONSE")
							{
								if (array2[i] != null)
								{
									value = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(array2[i], 0, array2[i].Length, null, true);
								}
								else
								{
									value = string.Empty;
								}
								this.m_sessionProperties.Add(text.Trim(), value);
							}
							else
							{
								this.m_sessionProperties.Add(text.Trim(), array2[i]);
							}
						}
					}
				}
				byte[] msgHex = (byte[])this.m_sessionProperties["AUTH_SVR_RESPONSE"];
				byte[] array3 = O5LogonHelper.EvaluateServerResponse("AES/CBC/PKCS5Padding", this.m_xoredKaAndKb, msgHex);
				byte[] array4 = null;
				if (array3 != null && array3.Length >= 16)
				{
					array4 = new byte[16];
					Array.Copy(array3, 16, array4, 0, 16);
				}
				if (array4 == null || HelperClass.CompareBytes(TTCAuthenticate.KZSR_SVR_RESPONSE, array4) != 0)
				{
					flag = true;
				}
				result = flag;
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000D3EA8 File Offset: 0x000D20A8
		internal static string ConvertVersionIntToString(int versionInt, ref int dbMajorVer, ref int dbMinorVer, ref int dbPatchsetVer)
		{
			dbMajorVer = ((int)(((long)versionInt & (long)((ulong)-16777216)) >> 24) & 255);
			dbMinorVer = ((versionInt & 15728640) >> 20 & 255);
			int num = (versionInt & 1044480) >> 12 & 255;
			dbPatchsetVer = ((versionInt & 3840) >> 8 & 255);
			int num2 = versionInt & 255;
			return string.Concat(new object[]
			{
				dbMajorVer,
				".",
				dbMinorVer,
				".",
				num,
				".",
				dbPatchsetVer,
				".",
				num2
			});
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x000D3F68 File Offset: 0x000D2168
		internal int ClientVersionStringToInt(string driverVersion)
		{
			int result = 0;
			try
			{
				string[] array = driverVersion.Split(TTCAuthenticate.m_versionSeparator);
				int num = int.Parse(array[1]) / 10;
				int num2 = int.Parse(array[1]) % 10;
				int num3 = int.Parse(array[2]) / 10;
				int num4 = int.Parse(array[2]) % 10;
				int num5 = int.Parse(array[3]);
				result = (num << 24 | num2 << 20 | num3 << 12 | num4 << 8 | num5);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0400154F RID: 5455
		private const int TTCC_LNG = 254;

		// Token: 0x04001550 RID: 5456
		internal const int KPZ_LOGON = 1;

		// Token: 0x04001551 RID: 5457
		internal const int KPZ_CPW = 2;

		// Token: 0x04001552 RID: 5458
		private const int KPZ_PROXY_AUTH = 1024;

		// Token: 0x04001553 RID: 5459
		private const int KPZ_PASSWD_ENCRYPTED = 256;

		// Token: 0x04001554 RID: 5460
		internal const int KPZ_LOGON_SYSDBA = 32;

		// Token: 0x04001555 RID: 5461
		internal const int KPZ_LOGON_SYSOPER = 64;

		// Token: 0x04001556 RID: 5462
		internal const int KPZ_LOGON_SYSASM = 4194304;

		// Token: 0x04001557 RID: 5463
		internal const int KPZ_CPW_AUTH = 16;

		// Token: 0x04001558 RID: 5464
		internal const int KPZ_OPASSWD_ENCRYPTED = 32;

		// Token: 0x04001559 RID: 5465
		internal const int KPZ_NPASSWD_ENCRYPTED = 64;

		// Token: 0x0400155A RID: 5466
		internal const int KPZ_NPASSWD_OBFUSCATE = 65536;

		// Token: 0x0400155B RID: 5467
		private const int KPZ_LOGON_PRELIMAUTH = 128;

		// Token: 0x0400155C RID: 5468
		internal const int KOLRUG_ENABLE = 1;

		// Token: 0x0400155D RID: 5469
		internal const int KOLRUG_NSURWT = 16;

		// Token: 0x0400155E RID: 5470
		internal const int KOLRUG_LOCMAP = 32;

		// Token: 0x0400155F RID: 5471
		internal const int KOLRUG_CLNRCE = 64;

		// Token: 0x04001560 RID: 5472
		internal const int KZTV_MAX_SALTL = 16;

		// Token: 0x04001561 RID: 5473
		internal const int PBKDF2_VGEN_COUNT_MIN = 4096;

		// Token: 0x04001562 RID: 5474
		internal const int PBKDF2_VGEN_COUNT_DEFAULT = 4096;

		// Token: 0x04001563 RID: 5475
		internal const int PBKDF2_VGEN_COUNT_MAX = 100000000;

		// Token: 0x04001564 RID: 5476
		internal const int PBKDF2_SDER_COUNT_MIN = 3;

		// Token: 0x04001565 RID: 5477
		internal const int PBKDF2_SDER_COUNT_DEFAULT = 3;

		// Token: 0x04001566 RID: 5478
		internal const int PBKDF2_SDER_COUNT_MAX = 100000000;

		// Token: 0x04001567 RID: 5479
		private const string AUTH_PBKDF2_CSK_SALT = "AUTH_PBKDF2_CSK_SALT";

		// Token: 0x04001568 RID: 5480
		private const string AUTH_PBKDF2_SDER_COUNT = "AUTH_PBKDF2_SDER_COUNT";

		// Token: 0x04001569 RID: 5481
		private const string AUTH_PBKDF2_VGEN_COUNT = "AUTH_PBKDF2_VGEN_COUNT";

		// Token: 0x0400156A RID: 5482
		private const string AUTH_PBKDF2_SPEEDY_KEY = "AUTH_PBKDF2_SPEEDY_KEY";

		// Token: 0x0400156B RID: 5483
		private const string DOUBLE_QUOTE = "\"";

		// Token: 0x0400156C RID: 5484
		internal const string AUTH_VERSION_NO = "AUTH_VERSION_NO";

		// Token: 0x0400156D RID: 5485
		private const string AUTH_TERMINAL = "AUTH_TERMINAL";

		// Token: 0x0400156E RID: 5486
		private const string AUTH_PROGRAM_NM = "AUTH_PROGRAM_NM";

		// Token: 0x0400156F RID: 5487
		private const string AUTH_MACHINE = "AUTH_MACHINE";

		// Token: 0x04001570 RID: 5488
		private const string AUTH_PID = "AUTH_PID";

		// Token: 0x04001571 RID: 5489
		private const string AUTH_SID = "AUTH_SID";

		// Token: 0x04001572 RID: 5490
		private const string AUTH_PASSWORD = "AUTH_PASSWORD";

		// Token: 0x04001573 RID: 5491
		private const string AUTH_NEWPASSWORD = "AUTH_NEWPASSWORD";

		// Token: 0x04001574 RID: 5492
		private const string AUTH_SESSKEY = "AUTH_SESSKEY";

		// Token: 0x04001575 RID: 5493
		private const string AUTH_VFR_DATA = "AUTH_VFR_DATA";

		// Token: 0x04001576 RID: 5494
		private const string AUTH_SVR_RESPONSE = "AUTH_SVR_RESPONSE";

		// Token: 0x04001577 RID: 5495
		private const string AUTH_ALTER_SESSION = "AUTH_ALTER_SESSION";

		// Token: 0x04001578 RID: 5496
		private const string AUTH_PROXY_CLIENT_NAME = "PROXY_CLIENT_NAME";

		// Token: 0x04001579 RID: 5497
		private const string AUTH_CONNECT_STRING = "AUTH_CONNECT_STRING";

		// Token: 0x0400157A RID: 5498
		internal const string AUTH_SERIAL_NUM = "AUTH_SERIAL_NUM";

		// Token: 0x0400157B RID: 5499
		internal const string AUTH_SESSION_ID = "AUTH_SESSION_ID";

		// Token: 0x0400157C RID: 5500
		internal const string AUTH_ORA_DEBUG_JDWP = "AUTH_ORA_DEBUG_JDWP";

		// Token: 0x0400157D RID: 5501
		internal const string AUTH_ORA_EDITION = "AUTH_ORA_EDITION";

		// Token: 0x0400157E RID: 5502
		internal const string AUTH_INSTANCENAME = "AUTH_INSTANCENAME";

		// Token: 0x0400157F RID: 5503
		internal const string AUTH_DBNAME = "AUTH_DBNAME";

		// Token: 0x04001580 RID: 5504
		internal const string AUTH_INSTANCE_NO = "AUTH_INSTANCE_NO";

		// Token: 0x04001581 RID: 5505
		internal const string AUTH_DB_MOUNT_ID = "AUTH_DB_MOUNT_ID";

		// Token: 0x04001582 RID: 5506
		internal const string AUTH_DB_MOUNT_ID2 = "AUTH_DB_MOUNT_ID\0";

		// Token: 0x04001583 RID: 5507
		internal const string AUTH_DB_ID = "AUTH_DB_ID";

		// Token: 0x04001584 RID: 5508
		internal const string AUTH_DB_ID2 = "AUTH_DB_ID\0";

		// Token: 0x04001585 RID: 5509
		internal const string AUTH_PDB_UID = "AUTH_PDB_UID";

		// Token: 0x04001586 RID: 5510
		internal const string AUTH_PDB_UID2 = "AUTH_PDB_UID\0";

		// Token: 0x04001587 RID: 5511
		internal const string AUTH_GLOBALLY_UNIQUE_DBID = "AUTH_GLOBALLY_UNIQUE_DBID";

		// Token: 0x04001588 RID: 5512
		internal const string AUTH_GLOBALLY_UNIQUE_DBID2 = "AUTH_GLOBALLY_UNIQUE_DBID\0";

		// Token: 0x04001589 RID: 5513
		internal const string AUTH_SC_SERVER_HOST = "AUTH_SC_SERVER_HOST";

		// Token: 0x0400158A RID: 5514
		internal const string AUTH_SC_INSTANCE_NAME = "AUTH_SC_INSTANCE_NAME";

		// Token: 0x0400158B RID: 5515
		internal const string AUTH_SC_INSTANCE_ID = "AUTH_SC_INSTANCE_ID";

		// Token: 0x0400158C RID: 5516
		internal const string AUTH_SC_INSTANCE_START_TIME = "AUTH_SC_INSTANCE_START_TIME";

		// Token: 0x0400158D RID: 5517
		internal const string AUTH_SC_DBUNIQUE_NAME = "AUTH_SC_DBUNIQUE_NAME";

		// Token: 0x0400158E RID: 5518
		internal const string AUTH_SC_DB_DOMAIN = "AUTH_SC_DB_DOMAIN";

		// Token: 0x0400158F RID: 5519
		internal const string AUTH_SC_SERVICE_NAME = "AUTH_SC_SERVICE_NAME";

		// Token: 0x04001590 RID: 5520
		internal const string AUTH_SC_SVC_FLAGS = "AUTH_SC_SVC_FLAGS";

		// Token: 0x04001591 RID: 5521
		internal const string AUTH_ONS_CONFIG = "AUTH_ONS_CONFIG";

		// Token: 0x04001592 RID: 5522
		internal const string AUTH_ONS_RLB_SUBSCR_PATTERN = "AUTH_ONS_RLB_SUBSCR_PATTERN";

		// Token: 0x04001593 RID: 5523
		internal const string AUTH_ONS_HA_SUBSCR_PATTERN = "AUTH_ONS_HA_SUBSCR_PATTERN";

		// Token: 0x04001594 RID: 5524
		internal const string AUTH_MAX_OPEN_CURSORS = "AUTH_MAX_OPEN_CURSORS";

		// Token: 0x04001595 RID: 5525
		internal const string AUTH_MAX_IDEN_LENGTH = "AUTH_MAX_IDEN_LENGTH";

		// Token: 0x04001596 RID: 5526
		internal const string AUTH_SESSION_CLIENT_CSET = "SESSION_CLIENT_CHARSET";

		// Token: 0x04001597 RID: 5527
		internal const string AUTH_SESSION_CLIENT_LTYPE = "SESSION_CLIENT_LIB_TYPE";

		// Token: 0x04001598 RID: 5528
		internal const string AUTH_SESSION_CLIENT_DRVNM = "SESSION_CLIENT_DRIVER_NAME";

		// Token: 0x04001599 RID: 5529
		internal const string AUTH_SESSION_CLIENT_VSN = "SESSION_CLIENT_VERSION";

		// Token: 0x0400159A RID: 5530
		private const string SESSION_CLIENT_LOBATTR = "SESSION_CLIENT_LOBATTR";

		// Token: 0x0400159B RID: 5531
		internal const string AUTH_NLS_LXLAN = "AUTH_NLS_LXLAN";

		// Token: 0x0400159C RID: 5532
		internal const string AUTH_NLS_LXCTERRITORY = "AUTH_NLS_LXCTERRITORY";

		// Token: 0x0400159D RID: 5533
		internal const string AUTH_NLS_LXCCURRENCY = "AUTH_NLS_LXCCURRENCY";

		// Token: 0x0400159E RID: 5534
		internal const string AUTH_NLS_LXCISOCURR = "AUTH_NLS_LXCISOCURR";

		// Token: 0x0400159F RID: 5535
		internal const string AUTH_NLS_LXCNUMERICS = "AUTH_NLS_LXCNUMERICS";

		// Token: 0x040015A0 RID: 5536
		internal const string AUTH_NLS_LXCDATEFM = "AUTH_NLS_LXCDATEFM";

		// Token: 0x040015A1 RID: 5537
		internal const string AUTH_NLS_LXCDATELANG = "AUTH_NLS_LXCDATELANG";

		// Token: 0x040015A2 RID: 5538
		internal const string AUTH_NLS_LXCSORT = "AUTH_NLS_LXCSORT";

		// Token: 0x040015A3 RID: 5539
		internal const string AUTH_NLS_LXCCALENDAR = "AUTH_NLS_LXCCALENDAR";

		// Token: 0x040015A4 RID: 5540
		internal const string AUTH_NLS_LXCUNIONCUR = "AUTH_NLS_LXCUNIONCUR";

		// Token: 0x040015A5 RID: 5541
		internal const string AUTH_NLS_LXCTIMEFM = "AUTH_NLS_LXCTIMEFM";

		// Token: 0x040015A6 RID: 5542
		internal const string AUTH_NLS_LXCSTMPFM = "AUTH_NLS_LXCSTMPFM";

		// Token: 0x040015A7 RID: 5543
		internal const string AUTH_NLS_LXCTTZNFM = "AUTH_NLS_LXCTTZNFM";

		// Token: 0x040015A8 RID: 5544
		internal const string AUTH_NLS_LXCSTZNFM = "AUTH_NLS_LXCSTZNFM";

		// Token: 0x040015A9 RID: 5545
		private const string AUTH_KPPL_CONN_CLASS = "AUTH_KPPL_CONN_CLASS";

		// Token: 0x040015AA RID: 5546
		private const string AUTH_KPPL_PURITY = "AUTH_KPPL_PURITY";

		// Token: 0x040015AB RID: 5547
		private const string AUTH_KPPL_TAG = "AUTH_KPPL_TAG";

		// Token: 0x040015AC RID: 5548
		private const string AUTH_KPPL_IS_MULTIPROP_TAG = "AUTH_KPPL_IS_MULTIPROP_TAG";

		// Token: 0x040015AD RID: 5549
		private const string AUTH_KPPL_FIXUP_CB = "AUTH_KPPL_FIXUP_CB";

		// Token: 0x040015AE RID: 5550
		private const string AUTH_KPPL_WAIT = "AUTH_KPPL_WAIT";

		// Token: 0x040015AF RID: 5551
		private const string KPPL_PURITY_DEFAULT = "0";

		// Token: 0x040015B0 RID: 5552
		private const string KPPL_PURITY_NEW = "1";

		// Token: 0x040015B1 RID: 5553
		private const string KPPL_PURITY_SELF = "2";

		// Token: 0x040015B2 RID: 5554
		private const string SESS_PURITY_DEFAULT = "DEFAULT";

		// Token: 0x040015B3 RID: 5555
		private const string SESS_PURITY_NEW = "NEW";

		// Token: 0x040015B4 RID: 5556
		private const string SESS_PURITY_SELF = "SELF";

		// Token: 0x040015B5 RID: 5557
		private const string SESSION_CLIENT_LIB_TYPE_VALUE = "0";

		// Token: 0x040015B6 RID: 5558
		internal const string SESSION_TIME_ZONE = "SESSION_TIME_ZONE";

		// Token: 0x040015B7 RID: 5559
		internal const string SESSION_NLS_LXCCHARSET = "SESSION_NLS_LXCCHARSET";

		// Token: 0x040015B8 RID: 5560
		internal const string SESSION_NLS_LXCNLSLENSEM = "SESSION_NLS_LXCNLSLENSEM";

		// Token: 0x040015B9 RID: 5561
		internal const string SESSION_NLS_LXCNCHAREXCP = "SESSION_NLS_LXCNCHAREXCP";

		// Token: 0x040015BA RID: 5562
		internal const string SESSION_NLS_LXCNCHARIMP = "SESSION_NLS_LXCNCHARIMP";

		// Token: 0x040015BB RID: 5563
		private const string CLIENT_DRIVER_NAME = "ODPM.NET";

		// Token: 0x040015BC RID: 5564
		private byte[] m_auth_pbkdf2_csk_salt;

		// Token: 0x040015BD RID: 5565
		private byte[] m_auth_pbkdf2_sder_count;

		// Token: 0x040015BE RID: 5566
		private byte[] m_auth_pbkdf2_vgen_count;

		// Token: 0x040015BF RID: 5567
		private byte[] m_auth_pbkdf2_speedy_key;

		// Token: 0x040015C0 RID: 5568
		private byte[] m_pbkdf2_cskSalt;

		// Token: 0x040015C1 RID: 5569
		private int m_pbkdf2_vgen_count;

		// Token: 0x040015C2 RID: 5570
		private int m_pbkdf2_sder_count;

		// Token: 0x040015C3 RID: 5571
		private static char[] m_versionSeparator = new char[]
		{
			'.'
		};

		// Token: 0x040015C4 RID: 5572
		private byte[] m_authTerminal;

		// Token: 0x040015C5 RID: 5573
		private byte[] m_authProgramName;

		// Token: 0x040015C6 RID: 5574
		private byte[] m_authMachine;

		// Token: 0x040015C7 RID: 5575
		private byte[] m_authPid;

		// Token: 0x040015C8 RID: 5576
		private byte[] m_authSid;

		// Token: 0x040015C9 RID: 5577
		private byte[] m_authPassword;

		// Token: 0x040015CA RID: 5578
		private byte[] m_authNewPassword;

		// Token: 0x040015CB RID: 5579
		private byte[] m_authSessionKey;

		// Token: 0x040015CC RID: 5580
		private byte[] m_authVerifierData;

		// Token: 0x040015CD RID: 5581
		private byte[] m_authAlterSession;

		// Token: 0x040015CE RID: 5582
		private byte[] m_authProxyClientName;

		// Token: 0x040015CF RID: 5583
		private byte[] m_authSessionId;

		// Token: 0x040015D0 RID: 5584
		private byte[] m_authSerialNum;

		// Token: 0x040015D1 RID: 5585
		private byte[] m_authDebugJDWPValue;

		// Token: 0x040015D2 RID: 5586
		private byte[] m_authConnectString;

		// Token: 0x040015D3 RID: 5587
		private byte[] m_sessionClientCharSet;

		// Token: 0x040015D4 RID: 5588
		private byte[] m_sessionClientLibType;

		// Token: 0x040015D5 RID: 5589
		private byte[] m_sessionClientDriverName;

		// Token: 0x040015D6 RID: 5590
		private byte[] m_sessionClientVersion;

		// Token: 0x040015D7 RID: 5591
		private byte[] m_sessionClientLobAttr;

		// Token: 0x040015D8 RID: 5592
		private byte[] m_authOraEditionAttr;

		// Token: 0x040015D9 RID: 5593
		private byte[] m_drcpConnectionClass;

		// Token: 0x040015DA RID: 5594
		private byte[] m_drcpTag;

		// Token: 0x040015DB RID: 5595
		private byte[] m_drcpMultipropTag;

		// Token: 0x040015DC RID: 5596
		private byte[] m_drcpSessionPurity;

		// Token: 0x040015DD RID: 5597
		private byte[] m_drcpFixupCB;

		// Token: 0x040015DE RID: 5598
		private static byte[] KZSR_SVR_RESPONSE = new byte[]
		{
			83,
			69,
			82,
			86,
			69,
			82,
			95,
			84,
			79,
			95,
			67,
			76,
			73,
			69,
			78,
			84
		};

		// Token: 0x040015DF RID: 5599
		private static byte[] KZSR_CLI_RESPONSE = new byte[]
		{
			67,
			76,
			73,
			69,
			78,
			84,
			95,
			84,
			79,
			95,
			83,
			69,
			82,
			86,
			69,
			82
		};

		// Token: 0x040015E0 RID: 5600
		private static byte[] m_clientLobAttr = new byte[]
		{
			49
		};

		// Token: 0x040015E1 RID: 5601
		internal Hashtable m_sessionProperties;

		// Token: 0x040015E2 RID: 5602
		private byte[] m_terminalName;

		// Token: 0x040015E3 RID: 5603
		private byte[] m_programName;

		// Token: 0x040015E4 RID: 5604
		private byte[] m_hostName;

		// Token: 0x040015E5 RID: 5605
		private byte[] m_userName;

		// Token: 0x040015E6 RID: 5606
		private byte[] m_processId;

		// Token: 0x040015E7 RID: 5607
		private byte[] m_connectstring;

		// Token: 0x040015E8 RID: 5608
		private byte[] m_clientCharSet;

		// Token: 0x040015E9 RID: 5609
		private byte[] m_clientLibType;

		// Token: 0x040015EA RID: 5610
		private byte[] m_clientDriverName;

		// Token: 0x040015EB RID: 5611
		private byte[] m_clientVersion;

		// Token: 0x040015EC RID: 5612
		private byte[] m_encryptedSK;

		// Token: 0x040015ED RID: 5613
		private byte[] m_alterSessionSql;

		// Token: 0x040015EE RID: 5614
		private int m_verifierType;

		// Token: 0x040015EF RID: 5615
		private byte[] m_salt;

		// Token: 0x040015F0 RID: 5616
		private byte[] m_encryptedKB;

		// Token: 0x040015F1 RID: 5617
		internal byte[] m_xoredKaAndKb;

		// Token: 0x040015F2 RID: 5618
		private byte[] m_conFounder;

		// Token: 0x040015F3 RID: 5619
		private byte[] m_encryptedPassword;

		// Token: 0x040015F4 RID: 5620
		private byte[] m_newEncryptedPassword;

		// Token: 0x040015F5 RID: 5621
		private byte[] m_pbkdf2_speedy_key;
	}
}
