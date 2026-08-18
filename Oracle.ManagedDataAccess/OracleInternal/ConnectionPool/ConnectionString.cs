using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x02000043 RID: 67
	internal class ConnectionString : ICloneable
	{
		// Token: 0x06000301 RID: 769 RVA: 0x000135E8 File Offset: 0x000117E8
		static ConnectionString()
		{
			ConnectionString.m_boolMapping = new Dictionary<string, bool>();
			ConnectionString.m_boolMapping.Add("TRUE", true);
			ConnectionString.m_boolMapping.Add("YES", true);
			ConnectionString.m_boolMapping.Add("FALSE", false);
			ConnectionString.m_boolMapping.Add("NO", false);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00013714 File Offset: 0x00011914
		private void Initialize(string connectionString)
		{
			this.m_key = connectionString.GetHashCode();
			this.m_dataSource = string.Empty;
			this.m_dbaPrivilege = DBAPrivilege.None;
			this.m_enlist = Enlist.True;
			this.m_connectionLifetime = 0;
			this.m_incrPoolSize = 5;
			this.m_decrPoolSize = 5;
			this.m_maxPoolSize = 100;
			this.m_minPoolSize = 1;
			this.m_password = null;
			this.m_persistSecurityInfo = false;
			this.m_pooling = true;
			this.m_connectionTimeout = 15;
			this.m_userId = null;
			this.m_promotableTransaction = ConfigBaseClass.m_PromotableTransaction;
			this.m_proxyUserId = null;
			this.m_proxyPassword = null;
			this.m_validateConnection = false;
			this.m_stmtCacheSize = ConfigBaseClass.m_StatementCacheSize;
			this.m_stmtCachePurge = false;
			this.m_haEvents = true;
			this.m_loadBalancing = true;
			this.m_metadataPooling = true;
			this.m_contextConnection = false;
			this.m_selfTuning = ConfigBaseClass.m_SelfTuning;
			this.m_poolRegulator = 180;
			this.m_connectionPoolTimeout = 15;
			this.m_passwordlessConString = connectionString;
			this.m_secPwdList = new SyncQueueList<SecureString>(int.MaxValue);
			this.m_secPxyPwdList = new SyncQueueList<SecureString>(int.MaxValue);
			this.m_sepsSecPwdList = new SyncQueueList<SecureString>(int.MaxValue);
			this.m_sepsSecPxyPwdList = new SyncQueueList<SecureString>(int.MaxValue);
			this.m_connectionLifetimeTimeSpan = default(TimeSpan);
			this.m_applicationContinuity = false;
			float num = 0f;
			float.TryParse(ConfigBaseClass.m_cpversion, out num);
			this.m_connectionPoolType = ConnectionPoolType.CCP;
			this.m_drcpEnabled = DrcpType.None;
			this.m_bInitilialized = false;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001387C File Offset: 0x00011A7C
		public ConnectionString(string constring)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				this.Initialize(constring);
				this.Parse(constring);
				this.Validate();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00013964 File Offset: 0x00011B64
		internal string ServerID
		{
			get
			{
				return this.m_dataSource;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001396C File Offset: 0x00011B6C
		public void Validate()
		{
			if (this.m_maxPoolSize < this.m_minPoolSize)
			{
				throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
				{
					"max pool size",
					this.m_maxPoolSize.ToString()
				}));
			}
			if (!this.m_pooling)
			{
				this.m_maxPoolSize = int.MaxValue;
				this.m_minPoolSize = 0;
				this.m_incrPoolSize = int.MaxValue;
				this.m_decrPoolSize = 0;
				this.m_poolRegulator = 0;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000139E8 File Offset: 0x00011BE8
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		public void SetProperty(string key, string value, string quotedValue, string originalKey)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				try
				{
					bool flag = false;
					this.m_bInitilialized = true;
					if (value != null)
					{
						if (key != null)
						{
							if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x60002fb-1 == null)
							{
								<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x60002fb-1 = new Dictionary<string, int>(26)
								{
									{
										"DATA SOURCE",
										0
									},
									{
										"DBA PRIVILEGE",
										1
									},
									{
										"ENLIST",
										2
									},
									{
										"CONNECTION LIFETIME",
										3
									},
									{
										"INCR POOL SIZE",
										4
									},
									{
										"DECR POOL SIZE",
										5
									},
									{
										"MAX POOL SIZE",
										6
									},
									{
										"MIN POOL SIZE",
										7
									},
									{
										"PASSWORD",
										8
									},
									{
										"PERSIST SECURITY INFO",
										9
									},
									{
										"POOLING",
										10
									},
									{
										"CONNECTION TIMEOUT",
										11
									},
									{
										"CONNECT TIMEOUT",
										12
									},
									{
										"USER ID",
										13
									},
									{
										"PROMOTABLE TRANSACTION",
										14
									},
									{
										"PROXY USER ID",
										15
									},
									{
										"PROXY PASSWORD",
										16
									},
									{
										"VALIDATE CONNECTION",
										17
									},
									{
										"STATEMENT CACHE SIZE",
										18
									},
									{
										"STATEMENT CACHE PURGE",
										19
									},
									{
										"HA EVENTS",
										20
									},
									{
										"LOAD BALANCING",
										21
									},
									{
										"METADATA POOLING",
										22
									},
									{
										"SELF TUNING",
										23
									},
									{
										"APPLICATION CONTINUITY",
										24
									},
									{
										"POOL REGULATOR",
										25
									}
								};
							}
							int num;
							if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x60002fb-1.TryGetValue(key, out num))
							{
								switch (num)
								{
								case 0:
									this.m_dataSource = value;
									goto IL_B35;
								case 1:
								{
									string text = value.ToUpperInvariant();
									string a;
									if ((a = text) != null)
									{
										if (a == "SYSDBA")
										{
											this.m_dbaPrivilege = DBAPrivilege.SYSDBA;
											goto IL_B35;
										}
										if (a == "SYSOPER")
										{
											this.m_dbaPrivilege = DBAPrivilege.SYSOPER;
											goto IL_B35;
										}
									}
									flag = true;
									goto IL_B35;
								}
								case 2:
									try
									{
										if (value.ToUpperInvariant() == "DYNAMIC")
										{
											this.m_enlist = Enlist.Dynamic;
										}
										else if (ConnectionString.m_boolMapping[value.ToUpperInvariant()])
										{
											this.m_enlist = Enlist.True;
										}
										else
										{
											this.m_enlist = Enlist.False;
										}
										goto IL_B35;
									}
									catch
									{
										flag = true;
										goto IL_B35;
									}
									break;
								case 3:
									break;
								case 4:
								{
									int num2 = 0;
									if (int.TryParse(value, out num2) && num2 > 0)
									{
										this.m_incrPoolSize = num2;
										goto IL_B35;
									}
									flag = true;
									goto IL_B35;
								}
								case 5:
								{
									int num3 = 0;
									if (int.TryParse(value, out num3) && num3 > 0)
									{
										this.m_decrPoolSize = num3;
										goto IL_B35;
									}
									flag = true;
									goto IL_B35;
								}
								case 6:
								{
									int num4 = 0;
									if (int.TryParse(value, out num4) && num4 > 0)
									{
										this.m_maxPoolSize = num4;
										goto IL_B35;
									}
									flag = true;
									goto IL_B35;
								}
								case 7:
								{
									int num5 = 0;
									if (int.TryParse(value, out num5) && num5 >= 0)
									{
										this.m_minPoolSize = num5;
										goto IL_B35;
									}
									flag = true;
									goto IL_B35;
								}
								case 8:
									if (quotedValue == string.Empty)
									{
										return;
									}
									this.m_password = quotedValue;
									this.m_bPasswordSet = true;
									if (this.m_valStartPos > 0 && this.m_valEndPos > 0)
									{
										if (this.m_compPassword == null)
										{
											this.m_compPassword = new ComparisonInfo(ComparisonType.Password, this.m_pwdlessOffset + this.m_valStartPos, this.m_valStartPos, this.m_valEndPos - this.m_valStartPos + 1);
										}
										else
										{
											this.m_compPassword.m_oriStartPos = this.m_pwdlessOffset + this.m_valStartPos;
											this.m_compPassword.m_newStartPos = this.m_valStartPos;
											this.m_compPassword.m_length = this.m_valEndPos - this.m_valStartPos + 1;
										}
									}
									if (this.m_currentPos != this.m_length)
									{
										if (this.m_prevSemiPos >= 0)
										{
											this.m_constring = this.m_constring.Substring(0, this.m_prevSemiPos + 1) + this.m_constring.Substring(this.m_currentPos + 1);
											this.m_pwdlessOffset += this.m_currentPos + 1 - (this.m_prevSemiPos + 1);
										}
										else
										{
											this.m_constring = this.m_constring.Substring(this.m_currentPos + 1);
											this.m_pwdlessOffset += this.m_currentPos + 1;
										}
									}
									else
									{
										this.m_constring = this.m_constring.Substring(0, this.m_prevSemiPos + 1);
										this.m_pwdlessOffset += this.m_currentPos - (this.m_prevSemiPos + 1);
									}
									this.m_length = this.m_constring.Length;
									this.m_currentPos = this.m_prevSemiPos;
									goto IL_B35;
								case 9:
									try
									{
										this.m_persistSecurityInfo = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
										goto IL_B35;
									}
									catch
									{
										flag = true;
										goto IL_B35;
									}
									goto Block_30;
								case 10:
									goto IL_54B;
								case 11:
								case 12:
									goto IL_56E;
								case 13:
									if (quotedValue == string.Empty)
									{
										return;
									}
									this.m_userId = quotedValue;
									this.m_bUserIdSet = true;
									if (this.m_valStartPos > 0 && this.m_valEndPos > 0)
									{
										if (this.m_compUserId == null)
										{
											this.m_compUserId = new ComparisonInfo(ComparisonType.UserId, this.m_pwdlessOffset + this.m_valStartPos, this.m_valStartPos, this.m_valEndPos - this.m_valStartPos + 1);
										}
										else
										{
											this.m_compUserId.m_oriStartPos = this.m_pwdlessOffset + this.m_valStartPos;
											this.m_compUserId.m_newStartPos = this.m_valStartPos;
											this.m_compUserId.m_length = this.m_valEndPos - this.m_valStartPos + 1;
										}
									}
									this.m_useridPos = this.m_valStartPos;
									if (this.m_currentPos != this.m_length)
									{
										this.m_useridLength = this.m_currentPos - this.m_prevSemiPos;
									}
									else
									{
										this.m_useridLength = this.m_currentPos - (this.m_prevSemiPos + 1);
									}
									if (this.m_valStartPos <= 0)
									{
										goto IL_B35;
									}
									if (this.m_constring[this.m_valStartPos] == '/' && this.m_valEndPos - this.m_valStartPos == 0 && this.m_osUser == null)
									{
										this.m_userId = "/";
										this.m_osUser = WindowsIdentity.GetCurrent();
										this.m_osUserName = this.m_osUser.Name;
										goto IL_B35;
									}
									if (this.m_constring.Substring(this.m_valStartPos, this.m_valEndPos - this.m_valStartPos + 1).Trim() == "/")
									{
										this.m_userId = "/";
										this.m_osUser = WindowsIdentity.GetCurrent();
										this.m_osUserName = this.m_osUser.Name;
										goto IL_B35;
									}
									goto IL_B35;
								case 14:
									this.m_promotableTransactionString = value;
									if (this.m_promotableTransactionString.ToUpperInvariant() == "PROMOTABLE")
									{
										this.m_promotableTransaction = PromotableTransaction.Promotable;
										goto IL_B35;
									}
									if (this.m_promotableTransactionString.ToUpperInvariant() == "LOCAL")
									{
										this.m_promotableTransaction = PromotableTransaction.Local;
										goto IL_B35;
									}
									flag = true;
									goto IL_B35;
								case 15:
									if (quotedValue == string.Empty)
									{
										return;
									}
									this.m_proxyUserId = quotedValue;
									this.m_bProxyUserIdSet = true;
									if (this.m_valStartPos > 0 && this.m_constring[this.m_valStartPos] == '/' && this.m_valEndPos - this.m_valStartPos == 0 && this.m_osUser == null)
									{
										this.m_proxyUserId = "/";
										this.m_osUser = WindowsIdentity.GetCurrent();
										this.m_osUserName = this.m_osUser.Name;
										goto IL_B35;
									}
									goto IL_B35;
								case 16:
									if (quotedValue == string.Empty)
									{
										return;
									}
									this.m_proxyPassword = quotedValue;
									this.m_bProxyPasswordSet = true;
									if (this.m_valStartPos > 0 && this.m_valEndPos > 0)
									{
										if (this.m_compProxyPassword == null)
										{
											this.m_compProxyPassword = new ComparisonInfo(ComparisonType.ProxyPassword, this.m_pwdlessOffset + this.m_valStartPos, this.m_valStartPos, this.m_valEndPos - this.m_valStartPos + 1);
										}
										else
										{
											this.m_compProxyPassword.m_oriStartPos = this.m_pwdlessOffset + this.m_valStartPos;
											this.m_compProxyPassword.m_newStartPos = this.m_valStartPos;
											this.m_compProxyPassword.m_length = this.m_valEndPos - this.m_valStartPos + 1;
										}
									}
									if (this.m_currentPos != this.m_length)
									{
										if (this.m_prevSemiPos >= 0)
										{
											this.m_constring = this.m_constring.Substring(0, this.m_prevSemiPos + 1) + this.m_constring.Substring(this.m_currentPos + 1);
											this.m_pwdlessOffset += this.m_currentPos + 1 - (this.m_prevSemiPos + 1);
										}
										else
										{
											this.m_constring = this.m_constring.Substring(this.m_currentPos + 1);
											this.m_pwdlessOffset += this.m_currentPos + 1;
										}
									}
									else
									{
										this.m_constring = this.m_constring.Substring(0, this.m_prevSemiPos + 1);
										this.m_pwdlessOffset += this.m_currentPos - (this.m_prevSemiPos + 1);
									}
									this.m_length = this.m_constring.Length;
									this.m_currentPos = this.m_prevSemiPos;
									goto IL_B35;
								case 17:
									try
									{
										this.m_validateConnection = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
										goto IL_B35;
									}
									catch
									{
										flag = true;
										goto IL_B35;
									}
									goto IL_9F4;
								case 18:
									goto IL_9F4;
								case 19:
									try
									{
										this.m_stmtCachePurge = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
										goto IL_B35;
									}
									catch
									{
										flag = true;
										goto IL_B35;
									}
									goto Block_60;
								case 20:
									goto IL_A3D;
								case 21:
									goto IL_A67;
								case 22:
									goto IL_A91;
								case 23:
									goto IL_AB4;
								case 24:
									goto IL_AD1;
								case 25:
									goto IL_AF5;
								default:
									goto IL_B15;
								}
								int num6 = 0;
								if (int.TryParse(value, out num6) && num6 >= 0)
								{
									this.m_connectionLifetime = num6;
									this.m_connectionLifetimeTimeSpan = new TimeSpan(0, 0, num6);
									goto IL_B35;
								}
								flag = true;
								goto IL_B35;
								Block_30:
								try
								{
									IL_54B:
									this.m_pooling = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
									goto IL_B35;
								}
								catch
								{
									flag = true;
									goto IL_B35;
								}
								IL_56E:
								int num7 = 0;
								if (int.TryParse(value, out num7) && num7 >= 0)
								{
									this.m_connectionTimeout = num7;
									goto IL_B35;
								}
								flag = true;
								goto IL_B35;
								IL_9F4:
								int num8 = 0;
								if (int.TryParse(value, out num8) && num8 >= 0)
								{
									this.m_stmtCacheSize = num8;
									goto IL_B35;
								}
								flag = true;
								goto IL_B35;
								Block_60:
								try
								{
									IL_A3D:
									this.m_haEvents = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
									this.m_haEventsPresentInConnString = true;
									goto IL_B35;
								}
								catch
								{
									flag = true;
									goto IL_B35;
								}
								try
								{
									IL_A67:
									this.m_loadBalancing = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
									this.m_loadBalancingPresentInConnString = true;
									goto IL_B35;
								}
								catch
								{
									flag = true;
									goto IL_B35;
								}
								try
								{
									IL_A91:
									this.m_metadataPooling = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
									goto IL_B35;
								}
								catch
								{
									flag = true;
									goto IL_B35;
								}
								try
								{
									IL_AB4:
									this.m_selfTuning = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
									goto IL_B35;
								}
								catch
								{
									flag = true;
									goto IL_B35;
								}
								try
								{
									IL_AD1:
									this.m_applicationContinuity = ConnectionString.m_boolMapping[value.ToUpperInvariant()];
									this.m_applicationContinuityPresentInConnString = true;
									goto IL_B35;
								}
								catch
								{
									flag = true;
									goto IL_B35;
								}
								IL_AF5:
								int num9 = 0;
								if (int.TryParse(value, out num9) && num9 >= 0)
								{
									this.m_poolRegulator = num9;
								}
								else
								{
									flag = true;
								}
								IL_B35:
								if (flag && quotedValue != string.Empty)
								{
									throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
									{
										originalKey,
										value
									}));
								}
								goto IL_B7E;
							}
						}
						IL_B15:
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_ATTRIB, new string[]
						{
							originalKey
						}));
					}
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
					throw;
				}
				IL_B7E:;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000146B4 File Offset: 0x000128B4
		public ConnectionString Clone()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			ConnectionString result;
			try
			{
				ConnectionString connectionString = (ConnectionString)base.MemberwiseClone();
				result = connectionString;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00014730 File Offset: 0x00012930
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00014738 File Offset: 0x00012938
		public void SecureWithNewPassword(string newPassword)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				if (!string.IsNullOrEmpty(newPassword))
				{
					string pattern = "password\\ *=";
					if (this.m_bProxyPasswordSet)
					{
						this.m_proxyPassword = newPassword;
						pattern = "proxy password\\ *=";
					}
					else
					{
						this.m_password = newPassword;
					}
					string compString = this.m_compString;
					Match match = Regex.Match(compString, pattern, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (match.Success)
					{
						int index = match.Index;
						string value = match.Value;
						this.m_key = compString.Insert(index + value.Length, newPassword).GetHashCode();
					}
					this.m_newPassword = null;
				}
				this.Secure();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00014824 File Offset: 0x00012A24
		public void SecureSEPSPassword()
		{
			if (!string.IsNullOrEmpty(this.m_sepsPassword))
			{
				SecureString secureString = new SecureString();
				for (int i = 0; i < this.m_sepsPassword.Length; i++)
				{
					secureString.AppendChar(this.m_sepsPassword[i]);
				}
				this.m_sepsSecuredPassword = secureString;
				this.m_sepsPassword = null;
			}
			if (!string.IsNullOrEmpty(this.m_sepsProxyPassword))
			{
				SecureString secureString2 = new SecureString();
				for (int j = 0; j < this.m_sepsProxyPassword.Length; j++)
				{
					secureString2.AppendChar(this.m_sepsProxyPassword[j]);
				}
				this.m_sepsSecuredProxyPassword = secureString2;
				this.m_sepsProxyPassword = null;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000148C4 File Offset: 0x00012AC4
		public void Secure()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				if (this.m_password != null && this.m_password != string.Empty)
				{
					SecureString secureString = new SecureString();
					for (int i = 0; i < this.m_password.Length; i++)
					{
						secureString.AppendChar(this.m_password[i]);
					}
					this.m_securedPassword = secureString;
					this.m_password = null;
				}
				if (this.m_proxyPassword != null)
				{
					SecureString secureString2 = new SecureString();
					foreach (char c in this.m_proxyPassword)
					{
						secureString2.AppendChar(c);
					}
					this.m_securedProxyPassword = secureString2;
					this.m_proxyPassword = null;
				}
				this.m_bSecured = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000149DC File Offset: 0x00012BDC
		public static ConnectionString GetCS(string constr)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			ConnectionString result;
			try
			{
				ConnectionString connectionString = null;
				bool flag = false;
				List<ConnectionString> list = ConnectionString.m_conStringPool.Get(constr.GetHashCode());
				if (list != null)
				{
					int num = 0;
					while (num < list.Count && !flag)
					{
						connectionString = list[num];
						if (connectionString.Compare(constr))
						{
							flag = true;
						}
						num++;
					}
				}
				if (!flag)
				{
					connectionString = new ConnectionString(constr);
				}
				result = connectionString;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00014A98 File Offset: 0x00012C98
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		public bool Compare(string constr)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			bool result;
			try
			{
				int num = 0;
				char[] array = constr.ToCharArray();
				int num2 = 0;
				while (num2 < this.m_compTypes.Length && num == 0)
				{
					if (this.m_compTypes[num2] == ComparisonType.Default)
					{
						for (int i = 0; i < this.m_compLength[num2]; i++)
						{
							if (array[this.m_compOriStartPos[num2] + i] != this.m_compStringChars[this.m_compNewStartPos[num2] + i])
							{
								num = -1;
								break;
							}
						}
					}
					else if (this.m_compTypes[num2] == ComparisonType.Password)
					{
						string password = this.Password;
						if (password.Length != this.m_compLength[num2])
						{
							num = -1;
							break;
						}
						char[] array2 = password.ToCharArray();
						for (int j = 0; j < this.m_compLength[num2]; j++)
						{
							if (array[this.m_compOriStartPos[num2] + j] != array2[j])
							{
								num = -1;
								break;
							}
						}
					}
					else if (this.m_compTypes[num2] == ComparisonType.ProxyPassword)
					{
						string proxyPassword = this.ProxyPassword;
						if (proxyPassword.Length != this.m_compLength[num2])
						{
							num = -1;
							break;
						}
						char[] array3 = this.ProxyPassword.ToCharArray();
						for (int k = 0; k < this.m_compLength[num2]; k++)
						{
							if (array[this.m_compOriStartPos[num2] + k] != array3[k])
							{
								num = -1;
								break;
							}
						}
					}
					num2++;
				}
				if (num == 0 && this.m_osUserName != null)
				{
					if (this.m_osUserName == WindowsIdentity.GetCurrent().Name)
					{
						num = 0;
					}
					else
					{
						num = 1;
					}
				}
				if (num == 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00014C8C File Offset: 0x00012E8C
		public string Parse(string constr)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			string pmId;
			try
			{
				string text = constr.ToUpperInvariant();
				char c = '\0';
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = true;
				bool flag5 = false;
				bool flag6 = false;
				this.m_constring = constr;
				this.m_length = constr.Length;
				this.m_currentPos = 0;
				while (this.m_currentPos <= this.m_length)
				{
					char c2;
					if (this.m_currentPos < this.m_length)
					{
						c2 = this.m_constring[this.m_currentPos];
					}
					else
					{
						c2 = ';';
					}
					if (!flag5 || c2 == c)
					{
						if (c2 == ConnectionString.s_equalSign)
						{
							if (!flag5 && !flag)
							{
								flag = true;
								flag3 = true;
								flag6 = false;
								this.m_equalPos = this.m_currentPos;
							}
						}
						else if (c2 == ConnectionString.s_singleQuote)
						{
							if (flag3)
							{
								if (!flag6)
								{
									c = ConnectionString.s_singleQuote;
									flag6 = true;
									flag5 = true;
								}
								this.m_valStartPos = this.m_currentPos;
							}
							else
							{
								if (!flag6 || c != ConnectionString.s_singleQuote)
								{
									throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_NOT_WELL_FORMED, new string[0]));
								}
								flag5 = false;
								this.m_valEndPos = this.m_currentPos;
							}
							flag3 = false;
						}
						else if (c2 == ConnectionString.s_doubleQuote)
						{
							if (flag3)
							{
								if (!flag6)
								{
									c = ConnectionString.s_doubleQuote;
									flag6 = true;
									flag5 = true;
								}
								this.m_valStartPos = this.m_currentPos;
							}
							else
							{
								if (!flag6 || c != ConnectionString.s_doubleQuote)
								{
									throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_NOT_WELL_FORMED, new string[0]));
								}
								flag5 = false;
								this.m_valEndPos = this.m_currentPos;
							}
							flag3 = false;
						}
						else if (c2 == ConnectionString.s_semiColon)
						{
							if (!flag5 && flag2 && flag)
							{
								if (this.m_attrStartPos >= 0 && this.m_attrEndPos >= 0)
								{
									string key = text.Substring(this.m_attrStartPos + this.m_pwdlessOffset, this.m_attrEndPos - this.m_attrStartPos + 1);
									string originalKey = constr.Substring(this.m_attrStartPos + this.m_pwdlessOffset, this.m_attrEndPos - this.m_attrStartPos + 1);
									string text2 = string.Empty;
									string quotedValue = string.Empty;
									if (this.m_valStartPos >= 0 && this.m_valEndPos >= 0)
									{
										if (c != '\0')
										{
											this.m_valStartPos++;
											this.m_valEndPos--;
										}
										text2 = this.m_constring.Substring(this.m_valStartPos, this.m_valEndPos - this.m_valStartPos + 1);
										if (c != '\0')
										{
											quotedValue = c + text2.Replace(c.ToString() + c.ToString(), c.ToString()) + c;
										}
										else
										{
											quotedValue = text2;
										}
									}
									this.SetProperty(key, text2, quotedValue, originalKey);
								}
							}
							else if ((flag2 || flag) && (this.m_attrStartPos == -1 || this.m_valStartPos == -1))
							{
								throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_NOT_WELL_FORMED, new string[0]));
							}
							this.m_prevSemiPos = this.m_currentPos;
							flag4 = true;
							flag = false;
							flag2 = false;
							flag5 = false;
							flag6 = false;
							c = '\0';
							this.m_equalPos = -1;
							this.m_attrStartPos = -1;
							this.m_attrEndPos = -1;
							this.m_valStartPos = -1;
							this.m_valEndPos = -1;
							flag3 = false;
						}
						else if (this.m_constring[this.m_currentPos] != ConnectionString.s_space)
						{
							flag2 = true;
							if (this.m_equalPos > 0 && flag6 && !flag5)
							{
								throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_NOT_WELL_FORMED, new string[0]));
							}
							if (flag4)
							{
								this.m_attrStartPos = this.m_currentPos;
								flag4 = false;
							}
							else if (this.m_equalPos == -1)
							{
								this.m_attrEndPos = this.m_currentPos;
							}
							if (flag3)
							{
								this.m_valStartPos = this.m_currentPos;
								this.m_valEndPos = this.m_currentPos;
								flag3 = false;
							}
							else if (this.m_equalPos > 0)
							{
								this.m_valEndPos = this.m_currentPos;
							}
						}
					}
					this.m_currentPos++;
				}
				this.m_passwordlessConString = this.m_constring;
				this.m_poolName = this.GetPoolNameFromConfig(this.m_passwordlessConString);
				this.m_compList = new List<ComparisonInfo>();
				if (!this.m_bProxyUserIdSet)
				{
					this.m_compUserId = null;
				}
				else if (this.m_compUserId != null)
				{
					this.m_compList.Add(this.m_compUserId);
				}
				if (this.m_compPassword != null)
				{
					this.m_compList.Add(this.m_compPassword);
				}
				if (this.m_compProxyPassword != null)
				{
					this.m_compList.Add(this.m_compProxyPassword);
				}
				if (this.m_compList.Count > 1)
				{
					this.m_compList.Sort(this.m_compList[0]);
				}
				int num = 0;
				int newStartPos = 0;
				for (int i = 0; i < this.m_compList.Count; i++)
				{
					if (this.m_compList[i].m_compType != ComparisonType.UserId && num < this.m_compList[i].m_oriStartPos)
					{
						this.m_compList.Insert(i, new ComparisonInfo(ComparisonType.Default, num, newStartPos, this.m_compList[i].m_oriStartPos - num));
						i++;
						num = this.m_compList[i].m_oriStartPos + this.m_compList[i].m_length;
						newStartPos = this.m_compList[i].m_newStartPos;
					}
				}
				if (this.m_compList.Count > 0 && this.m_compList[this.m_compList.Count - 1].m_oriStartPos + this.m_compList[this.m_compList.Count - 1].m_length < constr.Length)
				{
					this.m_compList.Add(new ComparisonInfo(ComparisonType.Default, num, newStartPos, constr.Length - num));
				}
				if (this.m_compList.Count == 0)
				{
					this.m_compList.Add(new ComparisonInfo(ComparisonType.Default, 0, 0, constr.Length));
				}
				int count = this.m_compList.Count;
				this.m_compTypes = new ComparisonType[count];
				this.m_compOriStartPos = new int[count];
				this.m_compNewStartPos = new int[count];
				this.m_compLength = new int[count];
				this.m_compSubString = new string[count];
				this.m_compString = constr;
				for (int j = 0; j < this.m_compList.Count; j++)
				{
					this.m_compTypes[j] = this.m_compList[j].m_compType;
					this.m_compOriStartPos[j] = this.m_compList[j].m_oriStartPos;
					this.m_compNewStartPos[j] = this.m_compList[j].m_newStartPos;
					this.m_compLength[j] = this.m_compList[j].m_length;
					this.m_compSubString[j] = this.m_compString.Substring(this.m_compNewStartPos[j], this.m_compLength[j]);
				}
				for (int k = this.m_compList.Count - 1; k >= 0; k--)
				{
					if (this.m_compTypes[k] == ComparisonType.UserId)
					{
						if (this.m_pmId == null)
						{
							this.m_pmId = this.m_compString.Substring(0, this.m_compOriStartPos[k]) + this.m_compString.Substring(this.m_compOriStartPos[k] + this.m_compLength[k]);
						}
					}
					else if (this.m_compTypes[k] != ComparisonType.Default)
					{
						this.m_compString = this.m_compString.Substring(0, this.m_compOriStartPos[k]) + this.m_compString.Substring(this.m_compOriStartPos[k] + this.m_compLength[k]);
					}
				}
				this.m_compStringChars = this.m_compString.ToCharArray();
				if (this.m_pmId == null)
				{
					this.m_pmId = this.m_compString;
				}
				if (this.m_osUser != null)
				{
					this.m_pmId = this.m_pmId + ";osUser=" + this.m_osUserName;
				}
				pmId = this.m_pmId;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return pmId;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00015500 File Offset: 0x00013700
		private string GetPoolNameFromConfig(string passwordlessConString)
		{
			if (ConfigBaseClass.m_connectionPoolNameMapping.ContainsKey(passwordlessConString))
			{
				return ConfigBaseClass.m_connectionPoolNameMapping[passwordlessConString] as string;
			}
			return passwordlessConString;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00015524 File Offset: 0x00013724
		public static string GetStringFromSecureString(SecureString ss)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			string result;
			try
			{
				IntPtr intPtr = Marshal.SecureStringToBSTR(ss);
				try
				{
					result = Marshal.PtrToStringBSTR(intPtr);
				}
				finally
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000155B4 File Offset: 0x000137B4
		internal string Password
		{
			get
			{
				if (this.m_password != null)
				{
					return this.m_password;
				}
				if (this.m_securedPassword == null)
				{
					return string.Empty;
				}
				SecureString secureString = null;
				bool flag = this.m_secPwdList.Dequeue(out secureString);
				if (flag)
				{
					string stringFromSecureString = ConnectionString.GetStringFromSecureString(secureString);
					this.m_secPwdList.Enqueue(secureString);
					return stringFromSecureString;
				}
				string stringFromSecureString2 = ConnectionString.GetStringFromSecureString(this.m_securedPassword);
				secureString = new SecureString();
				for (int i = 0; i < stringFromSecureString2.Length; i++)
				{
					secureString.AppendChar(stringFromSecureString2[i]);
				}
				this.m_secPwdList.Enqueue(secureString);
				return stringFromSecureString2;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00015648 File Offset: 0x00013848
		internal string SEPSPassword
		{
			get
			{
				if (this.m_sepsPassword != null)
				{
					return this.m_sepsPassword;
				}
				if (this.m_sepsSecuredPassword == null)
				{
					return null;
				}
				SecureString secureString = null;
				bool flag = this.m_sepsSecPwdList.Dequeue(out secureString);
				if (flag)
				{
					string stringFromSecureString = ConnectionString.GetStringFromSecureString(secureString);
					this.m_sepsSecPwdList.Enqueue(secureString);
					return stringFromSecureString;
				}
				string stringFromSecureString2 = ConnectionString.GetStringFromSecureString(this.m_sepsSecuredPassword);
				secureString = new SecureString();
				for (int i = 0; i < stringFromSecureString2.Length; i++)
				{
					secureString.AppendChar(stringFromSecureString2[i]);
				}
				this.m_sepsSecPwdList.Enqueue(secureString);
				return stringFromSecureString2;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000156D8 File Offset: 0x000138D8
		internal string SEPSProxyPassword
		{
			get
			{
				if (this.m_sepsProxyPassword != null)
				{
					return this.m_sepsProxyPassword;
				}
				if (this.m_sepsSecuredProxyPassword == null)
				{
					return null;
				}
				SecureString secureString = null;
				bool flag = this.m_sepsSecPxyPwdList.Dequeue(out secureString);
				if (flag)
				{
					string stringFromSecureString = ConnectionString.GetStringFromSecureString(secureString);
					this.m_sepsSecPwdList.Enqueue(secureString);
					return stringFromSecureString;
				}
				string stringFromSecureString2 = ConnectionString.GetStringFromSecureString(this.m_sepsSecuredProxyPassword);
				secureString = new SecureString();
				for (int i = 0; i < stringFromSecureString2.Length; i++)
				{
					secureString.AppendChar(stringFromSecureString2[i]);
				}
				this.m_sepsSecPxyPwdList.Enqueue(secureString);
				return stringFromSecureString2;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00015768 File Offset: 0x00013968
		internal string UserAuthenticationString
		{
			get
			{
				if (this.m_authString == string.Empty)
				{
					this.m_authString = "USER ID=" + this.m_userId;
					if (this.m_dbaPrivilege != DBAPrivilege.None)
					{
						this.m_authString = this.m_authString + ";DBA PRIVILEGE=" + this.m_dbaPrivilege;
					}
					if (this.m_bProxyUserIdSet)
					{
						this.m_authString = this.m_authString + ";PROXY USER ID=" + this.m_proxyUserId;
						if (this.m_proxyUserId == "/")
						{
							this.m_authString = this.m_authString + ";osuser=" + this.m_osUserName;
						}
					}
					else if (this.m_userId == "/")
					{
						this.m_authString = this.m_authString + ";osuser=" + this.m_osUserName;
					}
				}
				return this.m_authString;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00015854 File Offset: 0x00013A54
		internal string ProxyPassword
		{
			get
			{
				if (this.m_proxyPassword != null)
				{
					return this.m_proxyPassword;
				}
				if (this.m_securedProxyPassword == null)
				{
					return string.Empty;
				}
				SecureString secureString = null;
				bool flag = this.m_secPxyPwdList.Dequeue(out secureString);
				if (flag)
				{
					string stringFromSecureString = ConnectionString.GetStringFromSecureString(secureString);
					this.m_secPxyPwdList.Enqueue(secureString);
					return stringFromSecureString;
				}
				string stringFromSecureString2 = ConnectionString.GetStringFromSecureString(this.m_securedProxyPassword);
				secureString = new SecureString();
				for (int i = 0; i < stringFromSecureString2.Length; i++)
				{
					secureString.AppendChar(stringFromSecureString2[i]);
				}
				this.m_secPxyPwdList.Enqueue(secureString);
				return stringFromSecureString2;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000158E8 File Offset: 0x00013AE8
		internal string ConstructConString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append("datasrc=");
			stringBuilder.Append(this.m_dataSource);
			stringBuilder.Append(";enlist=");
			stringBuilder.Append(this.m_enlist);
			stringBuilder.Append(";lifetime=");
			stringBuilder.Append(this.m_connectionLifetime);
			stringBuilder.Append(";maxsize=");
			stringBuilder.Append(this.m_maxPoolSize);
			stringBuilder.Append(";minsize=");
			stringBuilder.Append(this.m_minPoolSize);
			stringBuilder.Append(";incsize=");
			stringBuilder.Append(this.m_incrPoolSize);
			stringBuilder.Append(";decsize=");
			stringBuilder.Append(this.m_decrPoolSize);
			stringBuilder.Append(";timeout=");
			stringBuilder.Append(this.m_connectionTimeout);
			stringBuilder.Append(";dbapriv=");
			stringBuilder.Append(this.m_dbaPrivilege);
			stringBuilder.Append(";validcon=");
			stringBuilder.Append(this.m_validateConnection);
			stringBuilder.Append(";pooling=");
			stringBuilder.Append(this.m_pooling);
			stringBuilder.Append(";stmtcache=");
			stringBuilder.Append(this.m_stmtCacheSize);
			if (this.m_stmtCacheSize > 0)
			{
				stringBuilder.Append(";stmtcachepurge=");
				stringBuilder.Append(this.m_stmtCachePurge);
			}
			else
			{
				stringBuilder.Append(";stmtcachepurge=0");
			}
			stringBuilder.Append(";metapool=");
			stringBuilder.Append(this.m_metadataPooling);
			stringBuilder.Append(";pspe=");
			stringBuilder.Append(this.m_promotableTransaction);
			stringBuilder.Append(";ha=");
			stringBuilder.Append(this.m_haEvents ? 1 : 0);
			stringBuilder.Append(";rlb=");
			stringBuilder.Append(this.m_loadBalancing ? 1 : 0);
			stringBuilder.Append(";ac=");
			stringBuilder.Append(this.m_applicationContinuity ? 1 : 0);
			if (this.m_proxyUserId != null && this.m_proxyUserId.Length > 0)
			{
				stringBuilder.Append(";pxyusr=");
				stringBuilder.Append(this.m_proxyUserId);
			}
			else
			{
				bool flag = false;
				if (this.m_userId != null && this.m_userId.Trim(ConnectionString.delim) == "/")
				{
					flag = true;
				}
				if (flag)
				{
					stringBuilder.Append(";osuserid=");
					stringBuilder.Append(WindowsIdentity.GetCurrent().Name);
				}
				else
				{
					stringBuilder.Append(";userid=");
					stringBuilder.Append(this.m_userId);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000444 RID: 1092
		public const string s_sysDba = "SYSDBA";

		// Token: 0x04000445 RID: 1093
		public const string s_sysOper = "SYSOPER";

		// Token: 0x04000446 RID: 1094
		private const string s_dataSource = "DATA SOURCE";

		// Token: 0x04000447 RID: 1095
		private const string s_dbaPrivilege = "DBA PRIVILEGE";

		// Token: 0x04000448 RID: 1096
		private const string s_enlist = "ENLIST";

		// Token: 0x04000449 RID: 1097
		private const string s_connectionLifetime = "CONNECTION LIFETIME";

		// Token: 0x0400044A RID: 1098
		private const string s_incrPoolSize = "INCR POOL SIZE";

		// Token: 0x0400044B RID: 1099
		private const string s_decrPoolSize = "DECR POOL SIZE";

		// Token: 0x0400044C RID: 1100
		private const string s_maxPoolSize = "MAX POOL SIZE";

		// Token: 0x0400044D RID: 1101
		private const string s_minPoolSize = "MIN POOL SIZE";

		// Token: 0x0400044E RID: 1102
		private const string s_password = "PASSWORD";

		// Token: 0x0400044F RID: 1103
		private const string s_persistSecurityInfo = "PERSIST SECURITY INFO";

		// Token: 0x04000450 RID: 1104
		private const string s_pooling = "POOLING";

		// Token: 0x04000451 RID: 1105
		private const string s_connectionTimeout = "CONNECTION TIMEOUT";

		// Token: 0x04000452 RID: 1106
		private const string s_connectTimeout = "CONNECT TIMEOUT";

		// Token: 0x04000453 RID: 1107
		private const string s_userId = "USER ID";

		// Token: 0x04000454 RID: 1108
		private const string s_poolRegulator = "POOL REGULATOR";

		// Token: 0x04000455 RID: 1109
		private const string s_promotableTransaction = "PROMOTABLE TRANSACTION";

		// Token: 0x04000456 RID: 1110
		private const string s_proxyUserId = "PROXY USER ID";

		// Token: 0x04000457 RID: 1111
		private const string s_proxyPassword = "PROXY PASSWORD";

		// Token: 0x04000458 RID: 1112
		private const string s_validateConnection = "VALIDATE CONNECTION";

		// Token: 0x04000459 RID: 1113
		private const string s_stmtCacheSize = "STATEMENT CACHE SIZE";

		// Token: 0x0400045A RID: 1114
		private const string s_stmtCachePurge = "STATEMENT CACHE PURGE";

		// Token: 0x0400045B RID: 1115
		private const string s_haEvents = "HA EVENTS";

		// Token: 0x0400045C RID: 1116
		private const string s_loadBalancing = "LOAD BALANCING";

		// Token: 0x0400045D RID: 1117
		private const string s_metadataPooling = "METADATA POOLING";

		// Token: 0x0400045E RID: 1118
		private const string s_contextConnection = "CONTEXT CONNECTION";

		// Token: 0x0400045F RID: 1119
		private const string s_selftuning = "SELF TUNING";

		// Token: 0x04000460 RID: 1120
		private const string s_applicationContinuity = "APPLICATION CONTINUITY";

		// Token: 0x04000461 RID: 1121
		private const string s_connectionPoolTimeout = "CONNECTION POOL TIMEOUT";

		// Token: 0x04000462 RID: 1122
		private const string s_lowercase_userid = "user id";

		// Token: 0x04000463 RID: 1123
		private const string s_lowercase_password = "password";

		// Token: 0x04000464 RID: 1124
		private const string s_lowercase_proxyuserid = "proxy userid";

		// Token: 0x04000465 RID: 1125
		private const string s_lowercase_proxypassword = "proxy password";

		// Token: 0x04000466 RID: 1126
		private const string s_dynamic = "DYNAMIC";

		// Token: 0x04000467 RID: 1127
		private static char s_space = ' ';

		// Token: 0x04000468 RID: 1128
		private static char s_singleQuote = '\'';

		// Token: 0x04000469 RID: 1129
		private static char s_doubleQuote = '"';

		// Token: 0x0400046A RID: 1130
		private static char s_semiColon = ';';

		// Token: 0x0400046B RID: 1131
		private static char s_equalSign = '=';

		// Token: 0x0400046C RID: 1132
		private static char[] s_whiteSpace = new char[]
		{
			' ',
			'\n',
			'\r',
			'\t',
			'\v',
			'\f'
		};

		// Token: 0x0400046D RID: 1133
		private static char[] s_ignore = new char[]
		{
			' ',
			'\n',
			'\r',
			'\t',
			'\v',
			'\f',
			';'
		};

		// Token: 0x0400046E RID: 1134
		private static char[] s_quotes = new char[]
		{
			ConnectionString.s_singleQuote,
			ConnectionString.s_doubleQuote
		};

		// Token: 0x0400046F RID: 1135
		private static char[] s_separator = new char[]
		{
			ConnectionString.s_semiColon
		};

		// Token: 0x04000470 RID: 1136
		private static char[] s_equal = new char[]
		{
			ConnectionString.s_equalSign
		};

		// Token: 0x04000471 RID: 1137
		private static char[] delim = new char[]
		{
			' ',
			'\t',
			'"'
		};

		// Token: 0x04000472 RID: 1138
		private static char[] delim2 = new char[]
		{
			' ',
			'\''
		};

		// Token: 0x04000473 RID: 1139
		public string m_dataSource;

		// Token: 0x04000474 RID: 1140
		public DBAPrivilege m_dbaPrivilege;

		// Token: 0x04000475 RID: 1141
		public Enlist m_enlist;

		// Token: 0x04000476 RID: 1142
		public int m_connectionLifetime;

		// Token: 0x04000477 RID: 1143
		public int m_incrPoolSize;

		// Token: 0x04000478 RID: 1144
		public int m_decrPoolSize;

		// Token: 0x04000479 RID: 1145
		public int m_maxPoolSize;

		// Token: 0x0400047A RID: 1146
		public int m_minPoolSize;

		// Token: 0x0400047B RID: 1147
		public string m_password;

		// Token: 0x0400047C RID: 1148
		public string m_newPassword;

		// Token: 0x0400047D RID: 1149
		public bool m_persistSecurityInfo;

		// Token: 0x0400047E RID: 1150
		public bool m_pooling;

		// Token: 0x0400047F RID: 1151
		public int m_connectionTimeout;

		// Token: 0x04000480 RID: 1152
		public string m_userId;

		// Token: 0x04000481 RID: 1153
		private string m_promotableTransactionString;

		// Token: 0x04000482 RID: 1154
		public PromotableTransaction m_promotableTransaction;

		// Token: 0x04000483 RID: 1155
		public string m_proxyUserId;

		// Token: 0x04000484 RID: 1156
		public string m_proxyPassword;

		// Token: 0x04000485 RID: 1157
		public bool m_validateConnection;

		// Token: 0x04000486 RID: 1158
		public int m_stmtCacheSize;

		// Token: 0x04000487 RID: 1159
		public bool m_stmtCachePurge;

		// Token: 0x04000488 RID: 1160
		public bool m_haEvents;

		// Token: 0x04000489 RID: 1161
		public bool m_haEventsPresentInConnString;

		// Token: 0x0400048A RID: 1162
		public bool m_loadBalancing;

		// Token: 0x0400048B RID: 1163
		public bool m_loadBalancingPresentInConnString;

		// Token: 0x0400048C RID: 1164
		public bool m_applicationContinuity;

		// Token: 0x0400048D RID: 1165
		public bool m_applicationContinuityPresentInConnString;

		// Token: 0x0400048E RID: 1166
		public bool m_metadataPooling;

		// Token: 0x0400048F RID: 1167
		public bool m_contextConnection;

		// Token: 0x04000490 RID: 1168
		public bool m_selfTuning;

		// Token: 0x04000491 RID: 1169
		public ConnectionPoolType m_connectionPoolType;

		// Token: 0x04000492 RID: 1170
		public object m_drcpSyncObj = new object();

		// Token: 0x04000493 RID: 1171
		public object m_connectionPoolTypeSyncObj = new object();

		// Token: 0x04000494 RID: 1172
		public int m_poolRegulator;

		// Token: 0x04000495 RID: 1173
		public int m_connectionPoolTimeout;

		// Token: 0x04000496 RID: 1174
		internal string m_poolName;

		// Token: 0x04000497 RID: 1175
		internal string m_sepsUserId;

		// Token: 0x04000498 RID: 1176
		internal string m_sepsProxyUserId;

		// Token: 0x04000499 RID: 1177
		internal string m_sepsPassword;

		// Token: 0x0400049A RID: 1178
		internal string m_sepsProxyPassword;

		// Token: 0x0400049B RID: 1179
		public SecureString m_sepsSecuredPassword;

		// Token: 0x0400049C RID: 1180
		public SecureString m_sepsSecuredProxyPassword;

		// Token: 0x0400049D RID: 1181
		public SyncQueueList<SecureString> m_sepsSecPwdList;

		// Token: 0x0400049E RID: 1182
		public SyncQueueList<SecureString> m_sepsSecPxyPwdList;

		// Token: 0x0400049F RID: 1183
		public string m_passwordlessConString;

		// Token: 0x040004A0 RID: 1184
		public string m_useridKeyValue;

		// Token: 0x040004A1 RID: 1185
		public string m_pmId;

		// Token: 0x040004A2 RID: 1186
		public bool m_bPasswordSet;

		// Token: 0x040004A3 RID: 1187
		public bool m_bProxyPasswordSet;

		// Token: 0x040004A4 RID: 1188
		public bool m_bUserIdSet;

		// Token: 0x040004A5 RID: 1189
		public bool m_bProxyUserIdSet;

		// Token: 0x040004A6 RID: 1190
		public string m_osUserName;

		// Token: 0x040004A7 RID: 1191
		public TimeSpan m_connectionLifetimeTimeSpan = default(TimeSpan);

		// Token: 0x040004A8 RID: 1192
		private static Dictionary<string, bool> m_boolMapping;

		// Token: 0x040004A9 RID: 1193
		public bool m_bInitilialized;

		// Token: 0x040004AA RID: 1194
		public bool m_bSecured;

		// Token: 0x040004AB RID: 1195
		public bool m_bPooled;

		// Token: 0x040004AC RID: 1196
		public DrcpType m_drcpEnabled;

		// Token: 0x040004AD RID: 1197
		public bool m_bModifiedAfterParsing;

		// Token: 0x040004AE RID: 1198
		public bool m_bDBStartup;

		// Token: 0x040004AF RID: 1199
		public bool m_bPrelimAuthSession;

		// Token: 0x040004B0 RID: 1200
		public WindowsIdentity m_osUser;

		// Token: 0x040004B1 RID: 1201
		public static ConStringPool m_conStringPool = new ConStringPool(256);

		// Token: 0x040004B2 RID: 1202
		public ComparisonType[] m_compTypes;

		// Token: 0x040004B3 RID: 1203
		public int[] m_compOriStartPos;

		// Token: 0x040004B4 RID: 1204
		public int[] m_compNewStartPos;

		// Token: 0x040004B5 RID: 1205
		public int[] m_compLength;

		// Token: 0x040004B6 RID: 1206
		public string[] m_compSubString;

		// Token: 0x040004B7 RID: 1207
		public string m_compString;

		// Token: 0x040004B8 RID: 1208
		public char[] m_compStringChars;

		// Token: 0x040004B9 RID: 1209
		public OraclePoolManager m_pm;

		// Token: 0x040004BA RID: 1210
		public SecureString m_securedPassword;

		// Token: 0x040004BB RID: 1211
		public SecureString m_securedProxyPassword;

		// Token: 0x040004BC RID: 1212
		public SyncQueueList<SecureString> m_secPwdList;

		// Token: 0x040004BD RID: 1213
		public SyncQueueList<SecureString> m_secPxyPwdList;

		// Token: 0x040004BE RID: 1214
		public string m_constring;

		// Token: 0x040004BF RID: 1215
		internal string m_authString = string.Empty;

		// Token: 0x040004C0 RID: 1216
		private int m_attrStartPos = -1;

		// Token: 0x040004C1 RID: 1217
		private int m_attrEndPos = -1;

		// Token: 0x040004C2 RID: 1218
		private int m_valStartPos = -1;

		// Token: 0x040004C3 RID: 1219
		private int m_valEndPos = -1;

		// Token: 0x040004C4 RID: 1220
		private int m_useridPos;

		// Token: 0x040004C5 RID: 1221
		private int m_useridLength;

		// Token: 0x040004C6 RID: 1222
		private ComparisonInfo m_compUserId;

		// Token: 0x040004C7 RID: 1223
		private ComparisonInfo m_compPassword;

		// Token: 0x040004C8 RID: 1224
		private ComparisonInfo m_compProxyPassword;

		// Token: 0x040004C9 RID: 1225
		private List<ComparisonInfo> m_compList;

		// Token: 0x040004CA RID: 1226
		private int m_currentPos;

		// Token: 0x040004CB RID: 1227
		private int m_equalPos = -1;

		// Token: 0x040004CC RID: 1228
		private int m_prevSemiPos = -1;

		// Token: 0x040004CD RID: 1229
		private int m_pwdlessOffset;

		// Token: 0x040004CE RID: 1230
		public int m_key;

		// Token: 0x040004CF RID: 1231
		public int m_length;

		// Token: 0x040004D0 RID: 1232
		public bool m_fetchPdbNameFromDb = true;
	}
}
