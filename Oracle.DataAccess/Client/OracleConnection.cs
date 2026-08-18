using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Xml;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000E6 RID: 230
	[DefaultEvent("InfoMessage")]
	[ToolboxBitmap(typeof(resfinder), "Oracle.DataAccess.src.Client.Icons.OracleConnectionToolBox_hc.bmp")]
	[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
	public sealed class OracleConnection : DbConnection, ICloneable
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x000512BC File Offset: 0x000502BC
		internal void AcceptStatementData(string stmtText)
		{
			try
			{
				StatementDetails statementDetails = this.m_opoConCtx.m_statementData[stmtText] as StatementDetails;
				if (statementDetails == null)
				{
					lock (this.m_tuningLock)
					{
						statementDetails = (this.m_opoConCtx.m_statementData[stmtText] as StatementDetails);
						if (statementDetails == null)
						{
							statementDetails = new StatementDetails();
							this.m_opoConCtx.m_statementData[stmtText] = statementDetails;
						}
					}
				}
				Interlocked.Increment(ref this.m_opoConCtx.m_totalDataAvailable);
				Interlocked.Increment(ref statementDetails.m_executionsIfNotSelect);
				if (this.m_opoConCtx.pool != null && this.m_opoConCtx.m_totalDataAvailable >= this.m_opoConCtx.pool.m_stmtSamplesLimit)
				{
					lock (this.m_tuningLock)
					{
						if (this.m_opoConCtx.m_totalDataAvailable >= this.m_opoConCtx.pool.m_stmtSamplesLimit)
						{
							this.m_opoConCtx.m_totalDataAvailable = 0;
							bool flag3 = this.m_opoConCtx.pool.m_clonedCtx.gridCR == 1 || this.m_opoConCtx.pool.m_clonedCtx.gridRLB == 1;
							OracleTuningAgent.AddData(this.m_opoConCtx.pool.m_agentKey, flag3 ? this.m_opoConCtx.pool.m_cpCtx.m_counter.total : this.m_opoConCtx.pool.m_counter.total, this.m_opoConCtx.pool.m_scsRecommendations, this.m_opoConCtx.m_statementData);
							this.m_opoConCtx.m_statementData = new Hashtable();
							if (this.m_opoConCtx.pooledConCtx != null)
							{
								this.m_opoConCtx.pooledConCtx.m_statementData = this.m_opoConCtx.m_statementData;
								this.m_opoConCtx.pooledConCtx.m_totalDataAvailable = 0;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						" (ERROR) OracleConnection::AcceptStatementData(): Error: " + ex.ToString() + " \n"
					});
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00051534 File Offset: 0x00050534
		[DefaultValue("")]
		[Category("Data")]
		[System.ComponentModel.Description("")]
		public string ClientId
		{
			set
			{
				int num = 0;
				if (this.State == ConnectionState.Closed)
				{
					OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
				}
				this.m_opoConCtx.opoConRefCtx.clientID = value;
				if (this.m_opoConCtx.opoConRefCtx.clientID == null)
				{
					this.m_opoConCtx.opoConRefCtx.clientID = "";
				}
				try
				{
					num = OpsCon.SetClientId(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.opsErrCtx, this.m_opoConCtx.opoConRefCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
		}

		// Token: 0x17000151 RID: 337
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00051608 File Offset: 0x00050608
		[System.ComponentModel.Description("")]
		[Category("Data")]
		[DefaultValue("")]
		public string ModuleName
		{
			set
			{
				int num = 0;
				if (this.State == ConnectionState.Closed)
				{
					OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
				}
				this.m_opoConCtx.opoConRefCtx.moduleName = value;
				if (this.m_opoConCtx.opoConRefCtx.moduleName == null)
				{
					this.m_opoConCtx.opoConRefCtx.moduleName = "";
				}
				try
				{
					num = OpsCon.SetModuleName(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.opsErrCtx, this.m_opoConCtx.opoConRefCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
		}

		// Token: 0x17000152 RID: 338
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x000516DC File Offset: 0x000506DC
		[System.ComponentModel.Description("")]
		[Category("Data")]
		[DefaultValue("")]
		public string ActionName
		{
			set
			{
				int num = 0;
				if (this.State == ConnectionState.Closed)
				{
					OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
				}
				this.m_opoConCtx.opoConRefCtx.actionName = value;
				if (this.m_opoConCtx.opoConRefCtx.actionName == null)
				{
					this.m_opoConCtx.opoConRefCtx.actionName = "";
				}
				try
				{
					num = OpsCon.SetActionName(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.opsErrCtx, this.m_opoConCtx.opoConRefCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000517B0 File Offset: 0x000507B0
		[Category("Data")]
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		public string ServiceName
		{
			get
			{
				return this.m_serviceName;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x000517B8 File Offset: 0x000507B8
		[DefaultValue("")]
		[Category("Data")]
		[System.ComponentModel.Description("")]
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x000517C0 File Offset: 0x000507C0
		[Category("Data")]
		[DefaultValue("")]
		[System.ComponentModel.Description("")]
		public string DatabaseDomainName
		{
			get
			{
				return this.m_databaseDomainName;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x000517C8 File Offset: 0x000507C8
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		[Category("Data")]
		public string HostName
		{
			get
			{
				return this.m_hostName;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000517D0 File Offset: 0x000507D0
		[Category("Data")]
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		public string InstanceName
		{
			get
			{
				return this.m_instanceName;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000517D8 File Offset: 0x000507D8
		[DefaultValue(0)]
		[System.ComponentModel.Description("")]
		[Browsable(false)]
		public int StatementCacheSize
		{
			get
			{
				return this.m_stmtCacheSize;
			}
		}

		// Token: 0x17000159 RID: 345
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x000517E0 File Offset: 0x000507E0
		[Category("Data")]
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		public string ClientInfo
		{
			set
			{
				int num = 0;
				if (this.State == ConnectionState.Closed)
				{
					OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
				}
				this.m_opoConCtx.opoConRefCtx.clientInfo = value;
				if (this.m_opoConCtx.opoConRefCtx.clientInfo == null)
				{
					this.m_opoConCtx.opoConRefCtx.clientInfo = "";
				}
				try
				{
					num = OpsCon.SetClientInfo(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.opsErrCtx, this.m_opoConCtx.opoConRefCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000518B4 File Offset: 0x000508B4
		private string GetPasswordLessStringEx(string conString, out string password, out string proxyPassword)
		{
			password = "";
			proxyPassword = "";
			this.m_pwdOSLessString = "";
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = null;
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = conString.IndexOf('"', num);
			if (num2 != -1)
			{
				int num3 = 0;
				array = new string[64];
				while (num2 != -1)
				{
					int num4 = conString.IndexOf(';', num2 + 1);
					if (num4 == -1)
					{
						num4 = conString.Length;
					}
					int num5 = conString.LastIndexOf('"', num4 - 1, num4 - num2);
					if (num2 == num5)
					{
						if (num4 == conString.Length)
						{
							num4--;
						}
						num5 = conString.IndexOf('"', num4);
						num4 = conString.IndexOf(';', num4 + 1);
					}
					if (num5 != -1)
					{
						int num6 = conString.IndexOf(';', num2, num5 - num2 + 1);
						if (num6 != -1)
						{
							flag = true;
							string text = conString.Substring(num2, num5 - num2 + 1);
							array[num3] = text;
							string text2 = conString.Substring(0, num5 + 1).Replace(text, "*" + num3.ToString() + "*");
							if (num5 + 1 < conString.Length)
							{
								conString = text2 + conString.Substring(num5 + 1, conString.Length - 1 - (num5 + 1) + 1);
							}
							else
							{
								conString = text2;
							}
							num3++;
							num = text2.Length;
						}
						else
						{
							num = num5 + 1;
						}
					}
					else
					{
						num = conString.Length;
					}
					if (num < conString.Length)
					{
						num2 = conString.IndexOf('"', num);
					}
					else
					{
						num2 = -1;
					}
				}
			}
			string[] array2 = conString.Split(OracleConnection.semiColon);
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(OracleConnection.equalSign, 2);
				string a = array3[0].Trim(OracleConnection.trimSpaces).ToLower();
				if (array3.Length != 2)
				{
					stringBuilder.Append(array2[i]);
					if (i != array2.Length - 1)
					{
						stringBuilder.Append(";");
					}
				}
				else if (a == "password")
				{
					password = array3[1].Trim(OracleConnection.trimSpaces);
					if (flag)
					{
						num2 = password.IndexOf("*");
						if (num2 != -1)
						{
							int num7 = password.IndexOf("*", num2 + 1);
							if (num7 == -1)
							{
								num7 = password.Length - 1;
							}
							int num8 = int.Parse(password.Substring(num2 + 1, num7 - 1 - num2));
							password = password.Replace("*" + num8.ToString() + "*", array[num8]);
						}
					}
					if (password != null && password.Length > 0)
					{
						int num9 = password.IndexOf('\'', 0);
						int num10 = password.IndexOf('"', 0);
						if (num9 != -1 && num10 != -1 && num9 < num10)
						{
							int num11 = password.IndexOf('"', num10 + 1);
							int num12 = password.IndexOf('\'', num11 + 1);
							if (num12 != -1 && (num11 != -1 & num11 < num12))
							{
								password = password.Trim(OracleConnection.delim2);
							}
						}
					}
				}
				else if (a == "proxy password")
				{
					proxyPassword = array3[1].Trim(OracleConnection.trimSpaces);
					if (flag)
					{
						num2 = proxyPassword.IndexOf("*");
						if (num2 != -1)
						{
							int num13 = proxyPassword.IndexOf("*", num2 + 1);
							if (num13 == -1)
							{
								num13 = proxyPassword.Length - 1;
							}
							int num14 = int.Parse(proxyPassword.Substring(num2 + 1, num13 - 1 - num2));
							password = proxyPassword.Replace("*" + num14.ToString() + "*", array[num14]);
						}
					}
					if (proxyPassword != null && proxyPassword.Length > 0)
					{
						int num9 = proxyPassword.IndexOf('\'', 0);
						int num10 = proxyPassword.IndexOf('"', 0);
						if (num9 != -1 && num10 != -1 && num9 < num10)
						{
							int num11 = proxyPassword.IndexOf('"', num10 + 1);
							int num12 = proxyPassword.IndexOf('\'', num11 + 1);
							if (num12 != -1 && (num11 != -1 & num11 < num12))
							{
								proxyPassword = proxyPassword.Trim(OracleConnection.delim2);
							}
						}
					}
				}
				else if (a == "user id" || a == "proxy user id")
				{
					string text3 = array3[1];
					if (flag)
					{
						text3 = text3.Trim(OracleConnection.trimSpaces);
						num2 = text3.IndexOf("*");
						if (num2 != -1)
						{
							int num15 = text3.IndexOf("*", num2 + 1);
							if (num15 == -1)
							{
								num15 = text3.Length - 1;
							}
							int num16 = int.Parse(text3.Substring(num2 + 1, num15 - 1 - num2));
							if (num16 != -1)
							{
								text3 = text3.Replace("*" + num16.ToString() + "*", array[num16]);
							}
						}
					}
					if (a == "user id" && text3 == "/")
					{
						flag2 = true;
					}
					stringBuilder.Append(array3[0]);
					stringBuilder.Append("=");
					stringBuilder.Append(text3);
					if (i != array2.Length - 1)
					{
						stringBuilder.Append(";");
					}
				}
				else
				{
					stringBuilder.Append(array2[i]);
					if (i != array2.Length - 1)
					{
						stringBuilder.Append(";");
					}
				}
			}
			this.m_pwdOSLessString = stringBuilder.ToString();
			if (flag2)
			{
				stringBuilder.Append(";osuserid=");
				stringBuilder.Append(WindowsIdentity.GetCurrent().Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00051E80 File Offset: 0x00050E80
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00051EE8 File Offset: 0x00050EE8
		[System.ComponentModel.Description("")]
		[Category("Data")]
		[DefaultValue("")]
		[Editor("Oracle.VsDevTools.OracleVSGConnStringEditor, Oracle.VsDevTools, Version=4.112.3.0, Culture=neutral, PublicKeyToken=89b483f429c47342, processorArchitecture=X86", "System.Drawing.Design.UITypeEditor")]
		public override string ConnectionString
		{
			get
			{
				if (this.m_conString == null || this.m_conString.Length == 0)
				{
					return string.Empty;
				}
				if (this.m_persist || !this.m_pwdValidated)
				{
					return this.m_conString;
				}
				if (this.m_pwdOSLessString == null)
				{
					this.m_pwdOSLessString = this.GetPasswordLessString(this.m_conString);
					return this.m_pwdOSLessString;
				}
				return this.m_pwdOSLessString;
			}
			set
			{
				if (this.m_state == ConnectionState.Open)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_NOT_UPDATABLE, new string[0]));
				}
				if (value != this.m_conString)
				{
					if (this.m_validConString)
					{
						this.m_tmpConString = this.m_conString;
					}
					this.m_validConString = false;
					if (value != null)
					{
						this.m_conString = value;
					}
					else
					{
						this.m_conString = string.Empty;
					}
					this.m_pwdLessString = this.GetPasswordLessStringEx(this.m_conString, out this.m_password, out this.m_proxyPassword);
					object[] array = MetaData.m_connDataPooler.Get(OracleConnection.ConStrAtrribs, this.m_pwdLessString) as object[];
					if (array != null)
					{
						this.m_conStrVals = array;
						this.m_internalConStr = (this.m_conStrVals[OracleConnection.IndexInternalConStr] as string);
						this.m_conStrValsFromPool = true;
					}
					else
					{
						if (this.m_conStrVals == null || this.m_conStrValsFromPool)
						{
							this.m_conStrVals = new object[29];
							this.m_conStrValsFromPool = false;
						}
						this.ResetAttribsToDefaults();
						this.ParseConnectionString();
					}
					if ((int)this.m_conStrVals[OracleConnection.IndexCtxConn] == 1)
					{
						this.m_contextConnection = true;
						this.m_conStrVals[OracleConnection.IndexLifetime] = 0;
						this.m_conTimeout = 0;
					}
					else
					{
						this.m_contextConnection = false;
						this.m_conTimeout = (int)this.m_conStrVals[OracleConnection.IndexTimeout];
					}
					this.m_dataSource = (string)this.m_conStrVals[OracleConnection.IndexDataSrc];
					this.m_stmtCacheSize = (int)this.m_conStrVals[OracleConnection.IndexStmtCache];
					this.m_tmpConString = null;
					this.m_validConString = true;
					this.m_pwdValidated = false;
					this.m_conSignature = 0;
					if ((int)this.m_conStrVals[OracleConnection.IndexPersist] == 1)
					{
						this.m_persist = true;
					}
					else
					{
						this.m_persist = false;
					}
					if (1 == OraTrace.m_demandOrclPermission)
					{
						if (this.m_orclPermission == null)
						{
							this.m_orclPermission = new OraclePermission(PermissionState.None);
						}
						this.m_orclPermission.Clear();
						this.m_orclPermission.Add(value, "", KeyRestrictionBehavior.AllowOnly);
					}
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000520E5 File Offset: 0x000510E5
		[DefaultValue(15)]
		[System.ComponentModel.Description("")]
		[Browsable(false)]
		public override int ConnectionTimeout
		{
			get
			{
				return this.m_conTimeout;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000520ED File Offset: 0x000510ED
		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000520F4 File Offset: 0x000510F4
		[DefaultValue("")]
		[System.ComponentModel.Description("")]
		public override string DataSource
		{
			get
			{
				if (this.m_dataSource != null)
				{
					return this.m_dataSource;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0005210A File Offset: 0x0005110A
		[Browsable(false)]
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		public override string ServerVersion
		{
			get
			{
				if (this.m_state != ConnectionState.Open)
				{
					throw new InvalidOperationException();
				}
				if (this.m_opoConCtx.pooledConCtx != null)
				{
					return this.m_opoConCtx.pooledConCtx.opoConRefCtx.serverVersion;
				}
				return this.m_serverVersion;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00052144 File Offset: 0x00051144
		[System.ComponentModel.Description("")]
		[DefaultValue(ConnectionState.Closed)]
		[Browsable(false)]
		public override ConnectionState State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0005214C File Offset: 0x0005114C
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0005215E File Offset: 0x0005115E
		internal unsafe int TxnHndAllocated
		{
			get
			{
				return this.m_opoConCtx.pOpoConValCtx->TxnHndAllocated;
			}
			set
			{
				this.m_opoConCtx.pOpoConValCtx->TxnHndAllocated = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00052174 File Offset: 0x00051174
		internal static bool IsCtxConnAvailable
		{
			get
			{
				return OracleConnection.m_oraThreadDataSlot != null && Thread.GetData(OracleConnection.m_oraThreadDataSlot) != null;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0005219B File Offset: 0x0005119B
		internal bool IsDBVer10gR2OrHigher
		{
			get
			{
				return this.m_majorVersion > 10 || (this.m_majorVersion == 10 && this.m_minorVersion >= 2);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x000521BD File Offset: 0x000511BD
		internal bool IsDBVer11gR1OrHigher
		{
			get
			{
				return this.m_majorVersion > 11 || (this.m_majorVersion == 11 && this.m_minorVersion >= 1);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x000521DF File Offset: 0x000511DF
		internal bool IsDBVer11gR2OrHigher
		{
			get
			{
				return this.m_majorVersion > 11 || (this.m_majorVersion == 11 && this.m_minorVersion >= 2);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00052201 File Offset: 0x00051201
		internal bool IsDBVer_11_1_0_7_OrHigher
		{
			get
			{
				return this.m_majorVersion > 11 || (this.m_majorVersion == 11 && this.m_minorVersion > 1) || (this.m_majorVersion == 11 && this.m_minorVersion == 1 && this.m_PatchSetVersion >= 7);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0005223F File Offset: 0x0005123F
		public unsafe OracleConnectionType ConnectionType
		{
			get
			{
				if (this.State == ConnectionState.Closed || this.m_opoConCtx.pOpoConValCtx == null)
				{
					return OracleConnectionType.Undefined;
				}
				if (this.m_opoConCtx.pOpoConValCtx->bIsTimesTen != 0)
				{
					return OracleConnectionType.TimesTen;
				}
				return OracleConnectionType.Oracle;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0005226F File Offset: 0x0005126F
		public static bool IsAvailable
		{
			get
			{
				if (!OracleConnection.m_extprocFlagRead)
				{
					if (OpsCom.GetExtProcFlag() == 1)
					{
						OracleConnection.m_extproc = true;
					}
					else
					{
						OracleConnection.m_extproc = false;
					}
					OracleConnection.m_extprocFlagRead = true;
				}
				return OracleConnection.m_extproc;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0005229C File Offset: 0x0005129C
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x000522C0 File Offset: 0x000512C0
		private static OracleConnection ExternalContextConnection
		{
			get
			{
				OracleConnection.ThreadData threadData = Thread.GetData(OracleConnection.m_oraThreadDataSlot) as OracleConnection.ThreadData;
				return threadData.m_externalExtprocConn;
			}
			set
			{
				OracleConnection.ThreadData threadData = Thread.GetData(OracleConnection.m_oraThreadDataSlot) as OracleConnection.ThreadData;
				threadData.m_externalExtprocConn = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x000522E4 File Offset: 0x000512E4
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00052308 File Offset: 0x00051308
		private static OracleConnection InternalContextConnection
		{
			get
			{
				OracleConnection.ThreadData threadData = Thread.GetData(OracleConnection.m_oraThreadDataSlot) as OracleConnection.ThreadData;
				return threadData.m_internalExtprocConn;
			}
			set
			{
				OracleConnection.ThreadData threadData = Thread.GetData(OracleConnection.m_oraThreadDataSlot) as OracleConnection.ThreadData;
				threadData.m_internalExtprocConn = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0005232C File Offset: 0x0005132C
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return OracleClientFactory.Instance;
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00052348 File Offset: 0x00051348
		static OracleConnection()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
			OracleConnection.m_pspePrimaryResourceEntry = Hashtable.Synchronized(new Hashtable());
			OracleConnection.m_boolMapping = new Hashtable(4);
			OracleConnection.m_boolMapping["true"] = 1;
			OracleConnection.m_boolMapping["false"] = 0;
			OracleConnection.m_boolMapping["yes"] = 1;
			OracleConnection.m_boolMapping["no"] = 0;
			OracleConnection.m_AttribToIndex = new SortedList(new CaseInsensitiveComparer(CultureInfo.InvariantCulture), 29);
			OracleConnection.m_AttribToIndex["USER ID"] = OracleConnection.IndexUserID;
			OracleConnection.m_AttribToIndex["PASSWORD"] = OracleConnection.IndexPasswd;
			OracleConnection.m_AttribToIndex["CONNECTION LIFETIME"] = OracleConnection.IndexLifetime;
			OracleConnection.m_AttribToIndex["INCR POOL SIZE"] = OracleConnection.IndexPoolInc;
			OracleConnection.m_AttribToIndex["DECR POOL SIZE"] = OracleConnection.IndexPoolDec;
			OracleConnection.m_AttribToIndex["CONNECTION TIMEOUT"] = OracleConnection.IndexTimeout;
			OracleConnection.m_AttribToIndex["DATA SOURCE"] = OracleConnection.IndexDataSrc;
			OracleConnection.m_AttribToIndex["ENLIST"] = OracleConnection.IndexEnlist;
			OracleConnection.m_AttribToIndex["MAX POOL SIZE"] = OracleConnection.IndexMaxPool;
			OracleConnection.m_AttribToIndex["MIN POOL SIZE"] = OracleConnection.IndexMinPool;
			OracleConnection.m_AttribToIndex["POOL REGULATOR"] = OracleConnection.IndexPoolReg;
			OracleConnection.m_AttribToIndex["PERSIST SECURITY INFO"] = OracleConnection.IndexPersist;
			OracleConnection.m_AttribToIndex["POOLING"] = OracleConnection.IndexPooling;
			OracleConnection.m_AttribToIndex["PROXY USER ID"] = OracleConnection.IndexProxyUsr;
			OracleConnection.m_AttribToIndex["PROXY PASSWORD"] = OracleConnection.IndexProxyPwd;
			OracleConnection.m_AttribToIndex["DBA PRIVILEGE"] = OracleConnection.IndexDBAPriv;
			OracleConnection.m_AttribToIndex["VALIDATE CONNECTION"] = OracleConnection.IndexValidCon;
			OracleConnection.m_AttribToIndex["METADATA POOLING"] = OracleConnection.IndexMetaPool;
			OracleConnection.m_AttribToIndex["STATEMENT CACHE PURGE"] = OracleConnection.IndexStmtCachePurge;
			OracleConnection.m_AttribToIndex["STATEMENT CACHE SIZE"] = OracleConnection.IndexStmtCache;
			OracleConnection.m_AttribToIndex["HA EVENTS"] = OracleConnection.IndexGridCR;
			OracleConnection.m_AttribToIndex["LOAD BALANCING"] = OracleConnection.IndexGridRLB;
			OracleConnection.m_AttribToIndex["CONTEXT CONNECTION"] = OracleConnection.IndexCtxConn;
			OracleConnection.m_AttribToIndex["PROMOTABLE TRANSACTION"] = OracleConnection.IndexPSPE;
			OracleConnection.m_AttribToIndex["SELF TUNING"] = OracleConnection.IndexSelfTuning;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000527D4 File Offset: 0x000517D4
		public OracleConnection()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::OracleConnection(1)\n"
				});
			}
			this.Init();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::OracleConnection(1)\n"
				});
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00052848 File Offset: 0x00051848
		public OracleConnection(string connectionString)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::OracleConnection(2)\n"
				});
			}
			this.Init();
			this.ConnectionString = connectionString;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::OracleConnection(2)\n"
				});
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000528C4 File Offset: 0x000518C4
		public unsafe override void Open()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::Open()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_state == ConnectionState.Open)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_ALREADY_OPEN, new string[0]));
			}
			if (this.m_conString == null || this.m_conString.Length == 0)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"OracleConnection.ConnectionString"
				}));
			}
			if (1 == OraTrace.m_demandOrclPermission && this.m_orclPermission != null)
			{
				this.m_orclPermission.Demand();
			}
			this.m_bLocalTxnStartedForSysTxn = false;
			this.m_promoteTxnMgr = null;
			if (!this.m_contextConnection)
			{
				if (this.m_conStrVals == null)
				{
					object[] array = MetaData.m_connDataPooler.Get(OracleConnection.ConStrAtrribs, this.m_pwdLessString) as object[];
					if (array != null)
					{
						this.m_conStrVals = array;
						this.m_internalConStr = (string)this.m_conStrVals[OracleConnection.IndexInternalConStr];
						this.m_conStrValsFromPool = true;
					}
					else if (this.m_conStrVals == null)
					{
						this.m_conStrVals = new object[29];
						this.m_conStrValsFromPool = false;
						this.ResetAttribsToDefaults();
					}
				}
				if (this.m_opoConCtx == null)
				{
					this.m_opoConCtx = new OpoConCtx();
				}
				int num;
				if (this.m_opoConCtx.pOpoConValCtx == null)
				{
					try
					{
						num = OpsCon.AllocValCtx(ref this.m_opoConCtx.pOpoConValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
				}
				string text = (string)this.m_conStrVals[OracleConnection.IndexDBAPriv];
				this.m_opoConCtx.pOpoConValCtx->DBAPrivilege = 0;
				if (text != null && text.Length > 0)
				{
					if (text.ToLower() == "sysdba")
					{
						this.m_opoConCtx.pOpoConValCtx->DBAPrivilege = 2;
					}
					else if (text.ToLower() == "sysoper")
					{
						this.m_opoConCtx.pOpoConValCtx->DBAPrivilege = 4;
					}
				}
				else if (this.m_bStartupShutdown)
				{
					OracleException.HandleError(1031, null, this.m_opoConCtx.opsErrCtx, null);
				}
				if (!this.m_bStartupShutdown)
				{
					this.m_enlist = (int)this.m_conStrVals[OracleConnection.IndexEnlist];
					if (this.m_enlist == 2)
					{
						this.m_opoConCtx.pOpoConValCtx->Enlist = 0;
						this.m_opoConCtx.pOpoConValCtx->SetIntAndExtName = 1;
					}
					else
					{
						this.m_opoConCtx.pOpoConValCtx->Enlist = this.m_enlist;
						this.m_opoConCtx.pOpoConValCtx->SetIntAndExtName = this.m_enlist;
					}
					this.m_opoConCtx.pOpoConValCtx->Pooling = (int)this.m_conStrVals[OracleConnection.IndexPooling];
					string text2 = (string)this.m_conStrVals[OracleConnection.IndexPSPE];
					this.m_opoConCtx.pOpoConValCtx->PSPE = 1;
					if (text2 != null && text2.Length > 0 && text2.ToLower() == "local")
					{
						this.m_opoConCtx.pOpoConValCtx->PSPE = 0;
					}
					this.m_opoConCtx.m_bSelfTuning = Convert.ToBoolean(this.m_conStrVals[OracleConnection.IndexSelfTuning]);
					if (this.m_opoConCtx.m_bSelfTuning && (this.m_opoConCtx.pOpoConValCtx->Pooling == 0 || OracleConnection.IsAvailable))
					{
						this.m_opoConCtx.m_bSelfTuning = false;
					}
					if (this.m_opoConCtx.m_bSelfTuning)
					{
						this.m_opoConCtx.pOpoConValCtx->StmtCacheSize = this.m_opoConCtx.m_defaultStmtCacheSize;
						if (this.m_opoConCtx.pOpoConValCtx->StmtCacheSize > OraTrace.MaxStatementCacheSize)
						{
							this.m_opoConCtx.pOpoConValCtx->StmtCacheSize = OraTrace.MaxStatementCacheSize;
						}
					}
					else
					{
						this.m_opoConCtx.pOpoConValCtx->StmtCacheSize = (int)this.m_conStrVals[OracleConnection.IndexStmtCache];
					}
					this.m_opoConCtx.pOpoConValCtx->StmtCachePurge = (int)this.m_conStrVals[OracleConnection.IndexStmtCachePurge];
					this.m_opoConCtx.poolRegulator = (int)this.m_conStrVals[OracleConnection.IndexPoolReg];
					this.m_opoConCtx.maxPoolSize = (int)this.m_conStrVals[OracleConnection.IndexMaxPool];
					this.m_opoConCtx.minPoolSize = (int)this.m_conStrVals[OracleConnection.IndexMinPool];
					this.m_opoConCtx.origMinPoolSize = this.m_opoConCtx.minPoolSize;
					this.m_opoConCtx.poolIncSize = (int)this.m_conStrVals[OracleConnection.IndexPoolInc];
					this.m_opoConCtx.poolDecSize = (int)this.m_conStrVals[OracleConnection.IndexPoolDec];
					this.m_opoConCtx.origPoolDecSize = this.m_opoConCtx.poolDecSize;
					this.m_opoConCtx.lifeTime = new TimeSpan(0, 0, (int)this.m_conStrVals[OracleConnection.IndexLifetime]);
					this.m_opoConCtx.origLifeTime = this.m_opoConCtx.lifeTime;
					this.m_opoConCtx.timeOut = new TimeSpan(0, 0, this.m_conTimeout);
					this.m_opoConCtx.validateCon = (int)this.m_conStrVals[OracleConnection.IndexValidCon];
					this.m_opoConCtx.gridCR = (int)this.m_conStrVals[OracleConnection.IndexGridCR];
					this.m_opoConCtx.gridRLB = (int)this.m_conStrVals[OracleConnection.IndexGridRLB];
					this.m_opoConCtx.bGridRac = (this.m_opoConCtx.gridCR == 1 || this.m_opoConCtx.gridRLB == 1);
					this.m_opoConCtx.metaPool = (int)this.m_conStrVals[OracleConnection.IndexMetaPool];
				}
				else if (this.m_bPrelimAuthSession)
				{
					this.m_opoConCtx.pOpoConValCtx->DBStartup = 1;
					this.m_opoConCtx.pOpoConValCtx->Pooling = 0;
					this.m_opoConCtx.pOpoConValCtx->StmtCacheSize = 0;
				}
				else
				{
					this.m_opoConCtx.pOpoConValCtx->DBStartup = 0;
				}
				this.m_opoConCtx.conString = this.m_internalConStr;
				if (this.m_password == null && this.m_proxyPassword == null)
				{
					this.m_pwdLessString = this.GetPasswordLessStringEx(this.m_conString, out this.m_password, out this.m_proxyPassword);
				}
				if (OraTrace.m_TraceLevel != 0U && OraTrace.m_TraceLevel != 8U)
				{
					if (this.m_pwdLessString != null)
					{
						this.m_opoConCtx.poolName = this.m_pwdLessString;
					}
					else
					{
						this.m_opoConCtx.poolName = this.GetPasswordLessStringEx(this.m_conString, out this.m_password, out this.m_proxyPassword);
						this.m_pwdLessString = this.m_opoConCtx.poolName;
					}
				}
				if (this.m_opoConCtx.opoConRefCtx == null)
				{
					this.m_opoConCtx.opoConRefCtx = new OpoConRefCtx();
				}
				if (((string)this.m_conStrVals[OracleConnection.IndexUserID]).Trim(OracleConnection.delim) == "/")
				{
					if (OracleConnection.IsAvailable)
					{
						throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_DOTNET_SP, new string[0]));
					}
					this.m_opoConCtx.opoConRefCtx.userID = "";
					this.m_opoConCtx.opoConRefCtx.password = "";
					this.m_opoConCtx.pOpoConValCtx->OSAuthent = 1;
					this.m_password = null;
				}
				else
				{
					this.m_opoConCtx.opoConRefCtx.userID = (string)this.m_conStrVals[OracleConnection.IndexUserID];
					this.m_opoConCtx.opoConRefCtx.password = this.m_password;
					this.m_password = null;
					this.m_opoConCtx.pOpoConValCtx->OSAuthent = 0;
				}
				this.m_opoConCtx.opoConRefCtx.dataSource = (string)this.m_conStrVals[OracleConnection.IndexDataSrc];
				if ((string)this.m_conStrVals[OracleConnection.IndexProxyUsr] == "/")
				{
					if (OracleConnection.IsAvailable)
					{
						throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_DOTNET_SP, new string[0]));
					}
					this.m_opoConCtx.opoConRefCtx.proxyUserId = "";
					this.m_opoConCtx.opoConRefCtx.proxyPassword = "";
					this.m_opoConCtx.pOpoConValCtx->OSAuthent = 2;
					this.m_proxyPassword = null;
				}
				else
				{
					this.m_opoConCtx.opoConRefCtx.proxyUserId = (string)this.m_conStrVals[OracleConnection.IndexProxyUsr];
					this.m_opoConCtx.opoConRefCtx.proxyPassword = this.m_proxyPassword;
					this.m_proxyPassword = null;
				}
				if (this.m_bStartupShutdown)
				{
					this.m_opoConCtx.opoConRefCtx.proxyUserId = "";
					this.m_opoConCtx.opoConRefCtx.proxyPassword = "";
				}
				if (!this.m_openWithNewPwd)
				{
					this.m_opoConCtx.opoConRefCtx.newPassword = string.Empty;
				}
				this.m_openWithNewPwd = false;
				this.m_opoConCtx.opoConRefCtx.appEdition = (string)this.m_conStrVals[OracleConnection.IndexAppEdition];
				this.m_opoConCtx.opoConRefCtx.ttOpsConOpenErrMssg = string.Empty;
				if (this.m_opoConCtx.bGridRac)
				{
					if (OraTrace.m_DBNotificationPort >= 0)
					{
						this.m_opoConCtx.pOpoConValCtx->DbNtfPort = OraTrace.m_DBNotificationPort;
					}
					else
					{
						this.m_opoConCtx.pOpoConValCtx->DbNtfPort = -1;
					}
				}
				else
				{
					this.m_opoConCtx.pOpoConValCtx->DbNtfPort = -2;
				}
				bool flag = false;
				string key = string.Empty;
				int enlist = this.m_opoConCtx.pOpoConValCtx->Enlist;
				if (this.m_opoConCtx.pOpoConValCtx->Enlist == 1)
				{
					if ((this.m_opoConCtx.m_systemTransaction = Transaction.Current) != null)
					{
						Guid distributedIdentifier = this.m_opoConCtx.m_systemTransaction.TransactionInformation.DistributedIdentifier;
						key = this.m_opoConCtx.m_systemTransaction.TransactionInformation.LocalIdentifier;
						object obj = OracleConnection.m_pspePrimaryResourceEntry[key];
						if (Guid.Empty == distributedIdentifier)
						{
							if (obj == null)
							{
								flag = true;
								this.m_opoConCtx.pOpoConValCtx->Enlist = 0;
								this.m_opoConCtx.opoConRefCtx.pITransaction = null;
								this.m_opoConCtx.m_txnType = TxnType.None;
							}
							else
							{
								OracleConnection.PSPEPrimaryConnectionInfo pspeprimaryConnectionInfo = obj as OracleConnection.PSPEPrimaryConnectionInfo;
								if (pspeprimaryConnectionInfo.m_pspeAttributeValue == 0 || this.m_opoConCtx.pOpoConValCtx->PSPE == 0 || !pspeprimaryConnectionInfo.m_dbSupportPromotion)
								{
									throw new TransactionPromotionException(OpoErrResManager.GetErrorMesg(ErrRes.CON_PSPE_RULE_VIOLATION, new string[0]));
								}
								TransactionInterop.GetTransmitterPropagationToken(this.m_opoConCtx.m_systemTransaction);
							}
						}
						if (!flag)
						{
							if (this.m_opoConCtx.pOpoConValCtx->PSPE == 0)
							{
								throw new TransactionPromotionException(OpoErrResManager.GetErrorMesg(ErrRes.CON_PSPE_RULE_VIOLATION, new string[0]));
							}
							this.m_opoConCtx.opoConRefCtx.pITransaction = (ITransaction)TransactionInterop.GetDtcTransaction(this.m_opoConCtx.m_systemTransaction);
							this.m_opoConCtx.m_txnType = TxnType.SystemTxn;
						}
					}
					else
					{
						this.m_opoConCtx.pOpoConValCtx->Enlist = 0;
						this.m_opoConCtx.opoConRefCtx.pITransaction = null;
					}
				}
				else
				{
					this.m_opoConCtx.pOpoConValCtx->Enlist = 0;
					this.m_opoConCtx.opoConRefCtx.pITransaction = null;
				}
				num = ConnectionDispenser.Open(this.m_opoConCtx);
				this.m_bPrelimAuthSession = false;
				if (num != 0 || this.m_opoConCtx.pOpoConValCtx->SessionBegin != 1)
				{
					this.m_opoConCtx.bErrorOnOpen = true;
					if (num == 0 && this.m_opoConCtx.pOpoConValCtx->SessionBegin != 1)
					{
						OracleException.HandleError(this.m_opoConCtx.pOpoConValCtx->SessionBegin, null, IntPtr.Zero, null);
					}
					else
					{
						if (num != 0 && this.m_opoConCtx.pOpoConValCtx->bIsTimesTen != 0 && !this.m_opoConCtx.opoConRefCtx.ttOpsConOpenErrMssg.Equals(string.Empty))
						{
							throw new OracleException(num, null, "OpsConOpen", this.m_opoConCtx.opoConRefCtx.ttOpsConOpenErrMssg);
						}
						if (num == ErrRes.INT_ERR)
						{
							OracleException.HandleError(num, null, IntPtr.Zero, this.m_opoConCtx.exceptMsg);
						}
						else
						{
							OracleException.HandleError(num, null, IntPtr.Zero, null);
						}
					}
				}
				else
				{
					this.m_opoConCtx.bErrorOnOpen = false;
					ConnectionState state = this.m_state;
					this.m_serverVersion = this.m_opoConCtx.opoConRefCtx.serverVersion;
					this.m_majorVersion = this.m_opoConCtx.pOpoConValCtx->MajorVersion;
					this.m_minorVersion = this.m_opoConCtx.pOpoConValCtx->MinorVersion;
					this.m_PatchSetVersion = this.m_opoConCtx.pOpoConValCtx->PatchSetVersion;
					this.m_state = ConnectionState.Open;
					if (this.m_opoConCtx.pOpoConValCtx->ConSignature == 2147483647)
					{
						this.m_opoConCtx.pOpoConValCtx->ConSignature = 0;
					}
					else
					{
						this.m_opoConCtx.pOpoConValCtx->ConSignature = this.m_opoConCtx.pOpoConValCtx->ConSignature + 1;
					}
					double num2 = (double)this.m_opoConCtx.pOpoConValCtx->ConSignature + (double)((long)this.m_opoConCtx.opsConCtx) / 10000000000.0;
					if (this.m_opoConCtx.m_bSelfTuning)
					{
						this.m_stmtCacheSize = this.m_opoConCtx.pOpoConValCtx->StmtCacheSize;
						if (this.m_opoConCtx.m_statementData == null)
						{
							this.m_opoConCtx.m_statementData = new Hashtable();
						}
					}
					this.m_conSignature = num2.GetHashCode();
					if (1 == this.m_opoConCtx.pOpoConValCtx->InMtsTxn)
					{
						if (null != Transaction.Current)
						{
							Transaction.Current.TransactionCompleted += this.TransactionComplete;
						}
					}
					else if (flag)
					{
						this.m_opoConCtx.pOpoConValCtx->Enlist = enlist;
						if (Transaction.Current != null && (int)this.m_conStrVals[OracleConnection.IndexEnlist] == 1)
						{
							bool flag2 = false;
							if (!OraTrace.m_NoPSPESupport)
							{
								flag2 = this.IsDBVer_11_1_0_7_OrHigher;
							}
							OracleConnection.m_pspePrimaryResourceEntry.Add(key, new OracleConnection.PSPEPrimaryConnectionInfo(flag2, this.m_opoConCtx.pOpoConValCtx->PSPE));
							if (this.m_opoConCtx.pOpoConValCtx->PSPE == 0 || (flag2 && this.m_opoConCtx.pOpoConValCtx->PSPE == 1))
							{
								if (this.m_promoteTxnMgr == null)
								{
									this.m_promoteTxnMgr = new PromotableTxnMgr();
								}
								bool flag3 = Transaction.Current.EnlistPromotableSinglePhase(this.m_promoteTxnMgr);
								if (flag3)
								{
									this.m_promoteTxnMgr.m_oraTransaction = this.BeginTransaction();
									this.m_bLocalTxnStartedForSysTxn = true;
									this.m_opoConCtx.m_promotableTxnManager = this.m_promoteTxnMgr;
									this.m_opoConCtx.m_txnType = TxnType.LocalTxnForSysTxn;
									this.m_promoteTxnMgr.m_localTxnIdentifier = Transaction.Current.TransactionInformation.LocalIdentifier;
									this.m_promoteTxnMgr.m_opsConCtx = this.m_opoConCtx.opsConCtx;
									this.m_promoteTxnMgr.m_opsErrCtx = this.m_opoConCtx.opsErrCtx;
									this.m_promoteTxnMgr.m_opoConRefCtx = this.m_opoConCtx.opoConRefCtx;
									ConnectionDispenser.CopyPooledConCtx(ref this.m_promoteTxnMgr.m_pOpoConValCtx, this.m_opoConCtx.pOpoConValCtx);
								}
								else
								{
									this.m_opoConCtx.opoConRefCtx.pITransaction = (ITransaction)TransactionInterop.GetDtcTransaction(this.m_opoConCtx.m_systemTransaction);
									this.m_opoConCtx.m_promotableTxnManager = new PromotableTxnMgr();
									this.m_opoConCtx.m_promotableTxnManager.m_localTxnIdentifier = Transaction.Current.TransactionInformation.LocalIdentifier;
									this.m_opoConCtx.m_txnType = TxnType.SystemTxn;
									num = ConnectionDispenser.Enlist(this.m_opoConCtx);
									if (num != 0)
									{
										OracleException.HandleError(num, null, IntPtr.Zero, null);
									}
								}
							}
							else if (this.m_opoConCtx.opoConRefCtx.proxyUserId == null || this.m_opoConCtx.opoConRefCtx.proxyUserId.Length == 0)
							{
								this.m_opoConCtx.opoConRefCtx.pITransaction = (ITransaction)TransactionInterop.GetDtcTransaction(this.m_opoConCtx.m_systemTransaction);
								this.m_opoConCtx.m_promotableTxnManager = new PromotableTxnMgr();
								this.m_opoConCtx.m_promotableTxnManager.m_localTxnIdentifier = Transaction.Current.TransactionInformation.LocalIdentifier;
								this.m_opoConCtx.m_txnType = TxnType.SystemTxn;
								num = ConnectionDispenser.Enlist(this.m_opoConCtx);
								if (num != 0)
								{
									OracleException.HandleError(num, null, IntPtr.Zero, null);
								}
							}
							Transaction.Current.TransactionCompleted += this.TransactionComplete;
						}
					}
					else
					{
						this.m_promoteTxnMgr = null;
					}
					if (OraTrace.m_fetchArrayPooling != 0 && this.m_opoConCtx.m_fetchArrayPooler == null && this.m_opoConCtx.opsConCtx != IntPtr.Zero)
					{
						this.m_opoConCtx.m_fetchArrayPooler = new FetchArrayPooler();
						try
						{
							OpsCon.SetFetchArrayGetFuncPtr(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.m_fetchArrayPooler.m_pFetchArrayGet);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
							throw;
						}
					}
					if (this.m_stateChangeEventHandler != null)
					{
						this.RaiseStateChange(state, this.m_state);
					}
					this.m_pwdValidated = true;
					this.m_serviceName = this.m_opoConCtx.opoConRefCtx.serviceName;
					this.m_databaseName = this.m_opoConCtx.opoConRefCtx.dbName;
					this.m_databaseDomainName = this.m_opoConCtx.opoConRefCtx.dbDomainName;
					this.m_hostName = this.m_opoConCtx.opoConRefCtx.hostName;
					this.m_instanceName = this.m_opoConCtx.opoConRefCtx.instanceName;
					if (!this.m_conStrValsFromPool)
					{
						try
						{
							this.m_conStrVals[OracleConnection.IndexInternalConStr] = this.m_internalConStr;
							MetaData.m_connDataPooler.Put(OracleConnection.ConStrAtrribs, this.m_pwdLessString, this.m_conStrVals);
							this.m_conStrValsFromPool = false;
							this.m_conStrVals = null;
							goto IL_1236;
						}
						catch
						{
							goto IL_1236;
						}
					}
					this.m_conStrVals = null;
				}
			}
			else
			{
				if (!OracleConnection.IsCtxConnAvailable)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_NONORACLR_THREAD, new string[0]));
				}
				ConnectionState state2 = this.m_state;
				this.OpenExtprocConnection();
				if (OraTrace.m_fetchArrayPooling != 0 && this.m_opoConCtx.m_fetchArrayPooler == null && this.m_opoConCtx.opsConCtx != IntPtr.Zero)
				{
					this.m_opoConCtx.m_fetchArrayPooler = new FetchArrayPooler();
					try
					{
						OpsCon.SetFetchArrayGetFuncPtr(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.m_fetchArrayPooler.m_pFetchArrayGet);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
						throw;
					}
				}
				if (this.m_stateChangeEventHandler != null)
				{
					this.RaiseStateChange(state2, this.m_state);
				}
			}
			IL_1236:
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::Open()\n"
				});
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00053B5C File Offset: 0x00052B5C
		public void OpenWithNewPassword(string newPassword)
		{
			if (this.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::OpenWithNewPassword()\n"
				});
			}
			if (this.m_opoConCtx == null)
			{
				this.m_opoConCtx = new OpoConCtx();
			}
			if (this.m_opoConCtx.opoConRefCtx == null)
			{
				this.m_opoConCtx.opoConRefCtx = new OpoConRefCtx();
			}
			this.m_opoConCtx.opoConRefCtx.newPassword = newPassword;
			this.m_openWithNewPwd = true;
			this.Open();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::OpenWithNewPassword()\n"
				});
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00053C10 File Offset: 0x00052C10
		internal int OnFailoverCallback_fn(IntPtr svchp, IntPtr envhp, IntPtr fo_ctx, int fo_type, int fo_event)
		{
			OracleFailoverEventArgs eventArgs = new OracleFailoverEventArgs(svchp, envhp, fo_ctx, fo_type, fo_event);
			return this.OnFailover(eventArgs);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00053C34 File Offset: 0x00052C34
		public new unsafe OracleTransaction BeginTransaction()
		{
			if (this.m_contextConnection)
			{
				throw new NotSupportedException();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::BeginTransaction()\n"
				});
			}
			this.m_oraTransaction = this.GetTransaction();
			if (this.m_oraTransaction != null || this.m_opoConCtx.pOpoConValCtx->InMtsTxn == 1)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_ALREADY_TXNED, new string[0]));
			}
			this.m_oraTransaction = new OracleTransaction(this, System.Data.IsolationLevel.ReadCommitted, this.TxnHndAllocated);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::BeginTransaction()\n"
				});
			}
			return this.m_oraTransaction;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00053CE4 File Offset: 0x00052CE4
		public new unsafe OracleTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel)
		{
			if (this.m_contextConnection)
			{
				throw new NotSupportedException();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::BeginTransaction()\n"
				});
			}
			this.m_oraTransaction = this.GetTransaction();
			if (this.m_oraTransaction != null || this.m_opoConCtx.pOpoConValCtx->InMtsTxn == 1)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_ALREADY_TXNED, new string[0]));
			}
			if (isolationLevel != System.Data.IsolationLevel.ReadCommitted && isolationLevel != System.Data.IsolationLevel.Serializable)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_INVALID_ISO_LEVEL, new string[0]), "isolationLevel");
			}
			this.m_oraTransaction = new OracleTransaction(this, isolationLevel, this.TxnHndAllocated);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::BeginTransaction()\n"
				});
			}
			return this.m_oraTransaction;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00053DBA File Offset: 0x00052DBA
		public override void ChangeDatabase(string databaseName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00053DC4 File Offset: 0x00052DC4
		public override void Close()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::Close()\n"
				});
			}
			try
			{
				if (this.m_state == ConnectionState.Open)
				{
					OracleDataReader[] array = null;
					lock (this.m_DataReaderList.SyncRoot)
					{
						if (this.m_DataReaderList.Count > 0)
						{
							array = new OracleDataReader[this.m_DataReaderList.Count];
							for (int i = 0; i < this.m_DataReaderList.Count; i++)
							{
								array[i] = (OracleDataReader)this.m_DataReaderList[i];
							}
							this.m_DataReaderList.Clear();
						}
					}
					if (array != null)
					{
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j] != null)
							{
								array[j].Close();
								array[j] = null;
							}
						}
						array = null;
					}
					if (this.m_state == ConnectionState.Open)
					{
						if (!this.m_contextConnection)
						{
							if (this.m_oraTransaction != null && this.m_oraTransaction.Completed)
							{
								this.m_oraTransaction = null;
							}
							if (this.m_oraTransaction != null)
							{
								try
								{
									if (!this.m_bLocalTxnStartedForSysTxn)
									{
										this.m_oraTransaction.Rollback();
									}
								}
								finally
								{
									this.m_bLocalTxnStartedForSysTxn = false;
									this.m_oraTransaction = null;
								}
							}
						}
						ConnectionState state = this.m_state;
						lock (this.m_syncTxnComplete)
						{
							this.m_state = ConnectionState.Closed;
						}
						if (this.m_opoConCtx.opoConRefCtx.proxyUserId != null && this.m_opoConCtx.opoConRefCtx.proxyUserId.Length > 0)
						{
							if (this.m_opoConCtx.m_udtDescPoolerByName != null)
							{
								this.m_opoConCtx.m_udtDescPoolerByName.Clear();
							}
							if (this.m_opoConCtx.m_udtDescPoolerByTDO != null)
							{
								this.m_opoConCtx.m_udtDescPoolerByTDO.Clear();
							}
						}
						ConnectionDispenser.Close(ref this.m_opoConCtx, this.m_contextConnection);
						if (this.m_stateChangeEventHandler != null)
						{
							this.RaiseStateChange(state, this.m_state);
						}
						if (this.m_opoConCtx != null)
						{
							this.m_opoConCtx.opoConRefCtx.clientID = "";
							this.m_opoConCtx.opoConRefCtx.moduleName = "";
							this.m_opoConCtx.opoConRefCtx.actionName = "";
							this.m_opoConCtx.opoConRefCtx.clientInfo = "";
						}
						if (this.m_metaDataCollectionDS != null)
						{
							this.m_metaDataCollectionDS.Clear();
							this.m_metaDataCollectionDS.Dispose();
							this.m_metaDataCollectionDS = null;
						}
					}
				}
			}
			finally
			{
				if (this.m_opoConCtx != null)
				{
					this.m_opoConCtx.m_fetchArrayPooler = null;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::Close()\n"
				});
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000540D0 File Offset: 0x000530D0
		public new OracleCommand CreateCommand()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::CreateCommand()\n"
				});
			}
			OracleCommand result = new OracleCommand("", this);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::CreateCommand()\n"
				});
			}
			return result;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00054124 File Offset: 0x00053124
		public object Clone()
		{
			if (this.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::Clone()\n"
				});
			}
			OracleConnection oracleConnection = new OracleConnection();
			if (this.m_conString != null && this.m_conString.Length != 0)
			{
				oracleConnection.ConnectionString = this.m_conString;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::Clone()\n"
				});
			}
			return oracleConnection;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000541B4 File Offset: 0x000531B4
		protected unsafe override void Dispose(bool disposing)
		{
			this.m_password = null;
			this.m_proxyPassword = null;
			bool flag = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::Dispose()\n"
				});
			}
			if (!this.m_disposed)
			{
				try
				{
					if (this.m_opoConCtx != null && this.m_state == ConnectionState.Open)
					{
						flag = false;
						if (!disposing && this.m_opoConCtx.m_udtDescPoolerByName != null)
						{
							this.m_opoConCtx.m_udtDescPoolerByName.Clear();
						}
						if (!disposing && this.m_opoConCtx.m_udtDescPoolerByTDO != null)
						{
							this.m_opoConCtx.m_udtDescPoolerByTDO.Clear();
						}
						if (!disposing && this.m_opoConCtx.m_conPooler != null)
						{
							this.m_opoConCtx.m_conPooler.Clear();
						}
						try
						{
							this.Close();
						}
						catch
						{
						}
					}
					try
					{
						if (this.m_opoConCtx != null && this.m_opoConCtx.pOpoConValCtx != null)
						{
							if (this.m_opoConCtx.pOpoConValCtx->Pooling != 0)
							{
								if (this.m_opoConCtx.pOpoConValCtx->OSAuthent == 0)
								{
									goto IL_102;
								}
							}
							try
							{
								ConnectionDispenser.Dispose(ref this.m_opoConCtx);
							}
							catch
							{
							}
						}
						IL_102:;
					}
					catch
					{
					}
					if (this.m_opoConCtx != null)
					{
						try
						{
							if (this.m_opoConCtx.pOpoConValCtx != null)
							{
								try
								{
									OpsCon.FreeValCtx(ref this.m_opoConCtx.pOpoConValCtx);
								}
								catch (Exception ex)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex);
									}
								}
							}
						}
						catch
						{
						}
					}
					this.m_disposed = true;
				}
				finally
				{
					if (!disposing && (OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfReclaimedConnections) == PerfCounterLevel.NumberOfReclaimedConnections && !this.m_contextConnection && !flag)
					{
						OraclePerfCounterCollection.NumberOfReclaimedConnections.Increment();
					}
					try
					{
						base.Dispose(disposing);
					}
					catch
					{
					}
					try
					{
						GC.SuppressFinalize(this);
					}
					catch
					{
					}
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleConnection::Dispose()\n"
					});
				}
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00054434 File Offset: 0x00053434
		public OracleGlobalization GetSessionInfo()
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::GetSessionInfo(1)\n"
				});
			}
			if (this.State == ConnectionState.Closed)
			{
				OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
			}
			IntPtr zero = IntPtr.Zero;
			OracleGlobalization oracleGlobalization = new OracleGlobalization();
			try
			{
				num = OpsCon.GetSessionInfo(this.m_opoConCtx.opsConCtx, ref zero);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
				}
			}
			Marshal.PtrToStructure(zero, oracleGlobalization.m_oraGlob);
			oracleGlobalization.TimeZone = "";
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::GetSessionInfo(1)\n"
				});
			}
			return oracleGlobalization;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00054520 File Offset: 0x00053520
		public void GetSessionInfo(OracleGlobalization oraGlob)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::GetSessionInfo(2)\n"
				});
			}
			if (this.State == ConnectionState.Closed)
			{
				OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsCon.GetSessionInfo(this.m_opoConCtx.opsConCtx, ref zero);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
				}
			}
			Marshal.PtrToStructure(zero, oraGlob.m_oraGlob);
			oraGlob.TimeZone = "";
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::GetSessionInfo(2)\n"
				});
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00054604 File Offset: 0x00053604
		public void SetSessionInfo(OracleGlobalization oraGlob)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::SetSessionInfo()\n"
				});
			}
			if (this.State == ConnectionState.Closed)
			{
				OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
			}
			StringBuilder stringBuilder = new StringBuilder("ALTER SESSION SET", 512);
			int majorVersion = this.m_majorVersion;
			int minorVersion = this.m_minorVersion;
			if (majorVersion >= 8)
			{
				stringBuilder.AppendFormat(" NLS_TERRITORY=\"{0}\"", oraGlob.Territory);
				stringBuilder.AppendFormat(" NLS_LANGUAGE=\"{0}\"", oraGlob.Language);
				stringBuilder.AppendFormat(" NLS_CALENDAR=\"{0}\"", oraGlob.Calendar);
				stringBuilder.AppendFormat(" NLS_DATE_LANGUAGE=\"{0}\"", oraGlob.DateLanguage);
				stringBuilder.AppendFormat(" NLS_CURRENCY=\"{0}\"", oraGlob.Currency);
				stringBuilder.AppendFormat(" NLS_DATE_FORMAT='{0}'", oraGlob.DateFormat);
				stringBuilder.AppendFormat(" NLS_ISO_CURRENCY=\"{0}\"", oraGlob.ISOCurrency);
				stringBuilder.AppendFormat(" NLS_NUMERIC_CHARACTERS=\"{0}\"", oraGlob.NumericCharacters);
				stringBuilder.AppendFormat(" NLS_SORT=\"{0}\"", oraGlob.Sort);
				if (minorVersion >= 1)
				{
					stringBuilder.AppendFormat(" NLS_COMP=\"{0}\"", oraGlob.Comparison);
					stringBuilder.AppendFormat(" NLS_DUAL_CURRENCY=\"{0}\"", oraGlob.DualCurrency);
				}
			}
			if (majorVersion >= 9)
			{
				stringBuilder.AppendFormat(" NLS_LENGTH_SEMANTICS=\"{0}\"", oraGlob.LengthSemantics);
				stringBuilder.AppendFormat(" NLS_NCHAR_CONV_EXCP=\"{0}\"", oraGlob.NCharConversionException);
				stringBuilder.AppendFormat(" NLS_TIMESTAMP_FORMAT='{0}'", oraGlob.TimeStampFormat);
				stringBuilder.AppendFormat(" NLS_TIMESTAMP_TZ_FORMAT='{0}'", oraGlob.TimeStampTZFormat);
			}
			if (oraGlob.TimeZone != null && oraGlob.TimeZone.Length != 0)
			{
				if (oraGlob.TimeZone.ToLower() == "local")
				{
					stringBuilder.AppendFormat(" TIME_ZONE=local", new object[0]);
				}
				else if (oraGlob.TimeZone.ToLower(CultureInfo.InvariantCulture) == "dbtimezone")
				{
					stringBuilder.AppendFormat(" TIME_ZONE=DBTIMEZONE", new object[0]);
				}
				else if (oraGlob.TimeZone.Length > 0)
				{
					stringBuilder.AppendFormat(" TIME_ZONE='{0}'", oraGlob.TimeZone);
				}
			}
			GCHandle gchandle = default(GCHandle);
			try
			{
				string value = stringBuilder.ToString();
				gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
				IntPtr pSql = gchandle.AddrOfPinnedObject();
				num = OpsCon.SetSessionInfo(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.opsErrCtx, pSql);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (num != 0)
				{
					if (num == -1)
					{
						OracleException.HandleError(num, null, this.m_opoConCtx.opsErrCtx, null);
					}
					else
					{
						OracleException.HandleError(12705, null, this.m_opoConCtx.opsErrCtx, null);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::SetSessionInfo()\n"
				});
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00054900 File Offset: 0x00053900
		public unsafe void PurgeStatementCache()
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::PurgeStatementCache()\n"
				});
			}
			try
			{
				if (this.m_opoConCtx.pOpoConValCtx != null && this.m_opoConCtx.pOpoConValCtx->StmtCacheSize > 0)
				{
					num = OpsCon.PurgeStatementCache(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.opsErrCtx, this.m_opoConCtx.pOpoConValCtx);
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::PurgeStatementCache()\n"
				});
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000549DC File Offset: 0x000539DC
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000549EA File Offset: 0x000539EA
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000549F4 File Offset: 0x000539F4
		public override DataTable GetSchema(string collectionName, string[] restrictionsArray)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::GetSchema(string, string[])\n"
				});
			}
			if (collectionName == null || collectionName.Length == 0)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_COLL_NOT_DEFINED, new string[]
				{
					collectionName
				}));
			}
			if (this.m_state == ConnectionState.Closed)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_metaDataCollectionDS == null)
			{
				this.LoadMetaDataXmlDS();
			}
			DataTable dataTable = null;
			if (this.m_metaDataCollectionDS != null)
			{
				dataTable = new DataTable();
				string text = this.NormalizeDBVersion(this.m_serverVersion);
				string text2 = collectionName.ToUpperInvariant();
				int num = 0;
				if (restrictionsArray != null)
				{
					num = restrictionsArray.Length;
				}
				string a;
				if ((a = text2) != null)
				{
					if (!(a == "METADATACOLLECTIONS"))
					{
						if (!(a == "DATATYPES"))
						{
							if (!(a == "RESTRICTIONS"))
							{
								if (!(a == "RESERVEDWORDS"))
								{
									if (a == "DATASOURCEINFORMATION")
									{
										if (num > 0)
										{
											throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_MORE_RESTRICTIONS, new string[]
											{
												collectionName,
												"0"
											}));
										}
										dataTable = this.m_metaDataCollectionDS.Tables[collectionName].Copy();
										dataTable.Rows[0][DbMetaDataColumnNames.DataSourceProductVersion] = this.m_serverVersion;
										dataTable.Rows[0][DbMetaDataColumnNames.DataSourceProductVersionNormalized] = text;
										dataTable.TableName = DbMetaDataCollectionNames.DataSourceInformation;
										dataTable.AcceptChanges();
										goto IL_6DD;
									}
								}
								else
								{
									if (num > 0)
									{
										throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_MORE_RESTRICTIONS, new string[]
										{
											collectionName,
											"0"
										}));
									}
									this.PopulateSupportedDataRows(dataTable, collectionName, text);
									dataTable.TableName = DbMetaDataCollectionNames.ReservedWords;
									dataTable.AcceptChanges();
									goto IL_6DD;
								}
							}
							else
							{
								if (num > 0)
								{
									throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_MORE_RESTRICTIONS, new string[]
									{
										collectionName,
										"0"
									}));
								}
								this.PopulateSupportedDataRows(dataTable, collectionName, text);
								dataTable.TableName = DbMetaDataCollectionNames.Restrictions;
								dataTable.AcceptChanges();
								goto IL_6DD;
							}
						}
						else
						{
							if (num > 0)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_MORE_RESTRICTIONS, new string[]
								{
									collectionName,
									"0"
								}));
							}
							this.PopulateSupportedDataRows(dataTable, collectionName, text);
							dataTable.TableName = DbMetaDataCollectionNames.DataTypes;
							dataTable.AcceptChanges();
							goto IL_6DD;
						}
					}
					else
					{
						if (num > 0)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_MORE_RESTRICTIONS, new string[]
							{
								collectionName,
								"0"
							}));
						}
						this.PopulateSupportedDataRows(dataTable, collectionName, text);
						dataTable.TableName = DbMetaDataCollectionNames.MetaDataCollections;
						dataTable.AcceptChanges();
						goto IL_6DD;
					}
				}
				string text3 = null;
				int num2 = 0;
				string text4 = null;
				bool flag = false;
				bool flag2 = false;
				DataRowCollection rows = this.m_metaDataCollectionDS.Tables[DbMetaDataCollectionNames.MetaDataCollections].Rows;
				for (int i = 0; i < rows.Count; i++)
				{
					if (((string)rows[i][DbMetaDataColumnNames.CollectionName]).ToUpperInvariant() == text2 && ((string)rows[i]["PopulationMechanism"]).ToUpperInvariant() == "ORACLECOMMAND")
					{
						flag2 = true;
						if (text4 == null)
						{
							text4 = (string)rows[i][DbMetaDataColumnNames.CollectionName];
						}
						if (this.SupportedInCurrentVersion(rows[i], text))
						{
							num2 = (int)rows[i][DbMetaDataColumnNames.NumberOfRestrictions];
							text3 = (string)rows[i]["PopulationString"];
							flag = false;
							break;
						}
						flag = true;
					}
					else if (((string)rows[i][DbMetaDataColumnNames.CollectionName]).ToUpperInvariant() == text2 && ((string)rows[i]["PopulationMechanism"]).ToUpperInvariant() == "DATATABLE")
					{
						dataTable = this.m_metaDataCollectionDS.Tables[collectionName].Copy();
						dataTable.TableName = collectionName.ToString();
						dataTable.AcceptChanges();
						return dataTable;
					}
				}
				if (!flag2)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_COLL_NOT_DEFINED, new string[]
					{
						collectionName
					}));
				}
				if (flag)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_COLL_NOT_SUPPORTED, new string[]
					{
						collectionName
					}));
				}
				if (num > num2)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_MORE_RESTRICTIONS, new string[]
					{
						collectionName,
						num2.ToString()
					}));
				}
				DataRowCollection rows2 = this.m_metaDataCollectionDS.Tables[DbMetaDataCollectionNames.Restrictions].Rows;
				int num3 = 0;
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < rows2.Count; j++)
				{
					if (((string)rows2[j][DbMetaDataColumnNames.CollectionName]).ToUpperInvariant() == text2)
					{
						OracleParameter oracleParameter = new OracleParameter();
						if (restrictionsArray != null)
						{
							if (num3 >= restrictionsArray.Length)
							{
								oracleParameter.Value = null;
							}
							else
							{
								oracleParameter.Value = restrictionsArray[num3];
							}
						}
						else
						{
							oracleParameter.Value = null;
						}
						oracleParameter.ParameterName = (string)rows2[j]["ParameterName"];
						arrayList.Add(oracleParameter);
						num3++;
						if (num3 >= num2)
						{
							break;
						}
					}
				}
				if (text3 == null)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_NO_POPULATION_STRING, new string[]
					{
						collectionName
					}));
				}
				OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(text3, this);
				oracleDataAdapter.SelectCommand.InitialLONGFetchSize = -1;
				oracleDataAdapter.SelectCommand.InitialLOBFetchSize = -1;
				foreach (object obj in arrayList)
				{
					OracleParameter param = (OracleParameter)obj;
					oracleDataAdapter.SelectCommand.Parameters.Add(param);
				}
				oracleDataAdapter.SelectCommand.BindByName = true;
				try
				{
					oracleDataAdapter.Fill(dataTable);
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_QUERY_FAILED, new string[]
					{
						collectionName
					}), innerException);
				}
				if (text4 != null)
				{
					dataTable.TableName = text4;
				}
				dataTable.AcceptChanges();
				foreach (object obj2 in arrayList)
				{
					OracleParameter oracleParameter2 = (OracleParameter)obj2;
					oracleParameter2.Dispose();
				}
				arrayList.Clear();
				arrayList = null;
				oracleDataAdapter.Dispose();
				oracleDataAdapter = null;
				IL_6DD:
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleConnection::GetSchema(string, string[])\n"
					});
				}
				return dataTable;
			}
			throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_NO_METADATA_STREAM, new string[]
			{
				collectionName
			}));
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00055128 File Offset: 0x00054128
		private void PopulateSupportedDataRows(DataTable dt, string collectionName, string normalizedDBVersion)
		{
			int count = this.m_metaDataCollectionDS.Tables[collectionName].Columns.Count;
			for (int i = 0; i < count; i++)
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.ColumnName = this.m_metaDataCollectionDS.Tables[collectionName].Columns[i].ColumnName;
				dataColumn.DataType = this.m_metaDataCollectionDS.Tables[collectionName].Columns[i].DataType;
				dt.Columns.Add(dataColumn);
			}
			DataRowCollection rows = this.m_metaDataCollectionDS.Tables[collectionName].Rows;
			foreach (object obj in rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.SupportedInCurrentVersion(dataRow, normalizedDBVersion))
				{
					DataRow dataRow2 = dt.NewRow();
					for (int j = 0; j < count; j++)
					{
						dataRow2[j] = dataRow[j];
					}
					dt.Rows.Add(dataRow2);
				}
			}
			dt.Columns.Remove("MaximumVersion");
			dt.Columns.Remove("MinimumVersion");
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00055280 File Offset: 0x00054280
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		private void LoadMetaDataXmlDS()
		{
			Stream stream = null;
			try
			{
				string metaDataXml = OraTrace.m_MetaDataXml;
				if (metaDataXml != null)
				{
					try
					{
						Configuration configuration = ConfigurationManager.OpenMachineConfiguration();
						stream = new FileStream(configuration.FilePath.Replace("machine.config", metaDataXml), FileMode.Open);
					}
					catch (FileNotFoundException)
					{
						throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.CON_GS_NO_CUSTOM_FILE, new string[]
						{
							metaDataXml
						}));
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			if (stream == null)
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				if (this.ConnectionType == OracleConnectionType.TimesTen)
				{
					stream = executingAssembly.GetManifestResourceStream("Oracle.DataAccess.src.Client.Resources.TimesTenMetaData.xml");
				}
				else
				{
					stream = executingAssembly.GetManifestResourceStream("Oracle.DataAccess.src.Client.Resources.OracleMetaData.xml");
				}
			}
			if (stream != null)
			{
				XmlTextReader xmlTextReader = new XmlTextReader(stream);
				this.m_metaDataCollectionDS = new DataSet("DocumentElement");
				this.m_metaDataCollectionDS.ReadXml(xmlTextReader);
				xmlTextReader.Close();
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00055358 File Offset: 0x00054358
		private string NormalizeDBVersion(string str)
		{
			string text = null;
			int num = 0;
			int num2 = 0;
			int length = str.Length;
			while (num <= length && num2 > -1)
			{
				num2 = str.IndexOf(".", num);
				if (num2 == -1)
				{
					if (length - num == 1)
					{
						text += "0";
					}
					text += str.Substring(num, length - num);
					break;
				}
				if (num2 - num == 1)
				{
					text += "0";
				}
				text += str.Substring(num, num2 - num + 1);
				num = num2 + 1;
			}
			return text;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000553DC File Offset: 0x000543DC
		private bool SupportedInCurrentVersion(DataRow row, string normalizedDBVersion)
		{
			string xmlnormalizedDBVersion = row["MaximumVersion"].ToString();
			string xmlnormalizedDBVersion2 = row["MinimumVersion"].ToString();
			return this.ComparenormalizedDBVersions(normalizedDBVersion, xmlnormalizedDBVersion2) >= 0 && this.ComparenormalizedDBVersions(normalizedDBVersion, xmlnormalizedDBVersion) <= 0;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00055424 File Offset: 0x00054424
		private int ComparenormalizedDBVersions(string normalizedDBVersion, string xmlnormalizedDBVersion)
		{
			int result = 0;
			int i = 0;
			int length = normalizedDBVersion.Length;
			NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
			numberFormatInfo.NumberDecimalSeparator = ".";
			if (xmlnormalizedDBVersion.Length > 0)
			{
				while (i <= length)
				{
					if (int.Parse(normalizedDBVersion.Substring(i, 2), numberFormatInfo) > int.Parse(xmlnormalizedDBVersion.Substring(i, 2), numberFormatInfo))
					{
						return 1;
					}
					if (int.Parse(normalizedDBVersion.Substring(i, 2), numberFormatInfo) < int.Parse(xmlnormalizedDBVersion.Substring(i, 2), numberFormatInfo))
					{
						return -1;
					}
					i += 3;
				}
			}
			return result;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000554A8 File Offset: 0x000544A8
		public unsafe void EnlistDistributedTransaction(ITransaction itrans)
		{
			if (OracleConnection.IsAvailable)
			{
				throw new NotSupportedException();
			}
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::EnlistDistributedTransaction()\n"
				});
			}
			if (this.State == ConnectionState.Closed)
			{
				OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
			}
			if (this.m_bLocalTxnStartedForSysTxn)
			{
				if (this.m_promoteTxnMgr == null || !this.m_promoteTxnMgr.m_bLocalTxnPromoted)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_MTS_ENLIST_FAIL, new string[0]));
				}
			}
			else
			{
				this.m_oraTransaction = this.GetTransaction();
				if (this.m_oraTransaction != null)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_ALREADY_TXNED, new string[0]));
				}
			}
			if (this.m_opoConCtx.pOpoConValCtx->InMtsTxn == 1)
			{
				try
				{
					this.m_opoConCtx.m_systemTransaction = null;
					this.m_opoConCtx.m_txnType = TxnType.None;
					this.m_opoConCtx.opoConRefCtx.pITransaction = null;
					num = OpsCon.Enlist(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.pOpoConValCtx, this.m_opoConCtx.opoConRefCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					this.m_opoConCtx.pOpoConValCtx->InMtsTxn = 0;
				}
				if (num != 0)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_MTS_ENLIST_FAIL, new string[0]));
				}
			}
			if (itrans != null)
			{
				try
				{
					this.m_opoConCtx.opoConRefCtx.pITransaction = itrans;
					num = OpsCon.Enlist(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.pOpoConValCtx, this.m_opoConCtx.opoConRefCtx);
					if (num == 0)
					{
						this.m_opoConCtx.m_systemTransaction = TransactionInterop.GetTransactionFromDtcTransaction(itrans as IDtcTransaction);
						if (this.m_opoConCtx.m_systemTransaction == null)
						{
							this.m_opoConCtx.opoConRefCtx.pITransaction = null;
							num = OpsCon.Enlist(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.pOpoConValCtx, this.m_opoConCtx.opoConRefCtx);
							this.m_opoConCtx.pOpoConValCtx->InMtsTxn = 0;
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_MTS_ENLIST_FAIL, new string[0]));
						}
						this.m_opoConCtx.m_txnType = TxnType.SystemTxn;
						this.m_opoConCtx.pOpoConValCtx->InMtsTxn = 1;
					}
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					throw;
				}
			}
			if (num != 0)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_MTS_ENLIST_FAIL, new string[0]));
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleConnection::EnlistDistributedTransaction()\n"
				});
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00055760 File Offset: 0x00054760
		public unsafe override void EnlistTransaction(Transaction transaction)
		{
			if (OracleConnection.IsAvailable)
			{
				throw new NotSupportedException();
			}
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::EnlistTransaction()\n"
				});
			}
			if (this.State == ConnectionState.Closed)
			{
				OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
			}
			string text = null;
			if (this.m_promoteTxnMgr != null && !string.IsNullOrEmpty(this.m_promoteTxnMgr.m_localTxnIdentifier))
			{
				text = this.m_promoteTxnMgr.m_localTxnIdentifier;
			}
			else if (null != this.m_opoConCtx.m_systemTransaction)
			{
				text = this.m_opoConCtx.m_systemTransaction.TransactionInformation.LocalIdentifier;
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (!text.Equals(transaction.TransactionInformation.LocalIdentifier))
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_ALREADY_TXNED, new string[0]));
				}
			}
			else
			{
				this.m_oraTransaction = this.GetTransaction();
				if (this.m_oraTransaction != null || this.m_opoConCtx.pOpoConValCtx->InMtsTxn == 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_ALREADY_TXNED, new string[0]));
				}
				if (null == transaction && this.m_opoConCtx.pOpoConValCtx->InMtsTxn == 0)
				{
					return;
				}
				bool flag = true;
				if (transaction != null)
				{
					Guid distributedIdentifier = transaction.TransactionInformation.DistributedIdentifier;
					string localIdentifier = transaction.TransactionInformation.LocalIdentifier;
					object obj = OracleConnection.m_pspePrimaryResourceEntry[localIdentifier];
					if (distributedIdentifier == Guid.Empty)
					{
						if (obj == null)
						{
							bool isDBVer_11_1_0_7_OrHigher = this.IsDBVer_11_1_0_7_OrHigher;
							OracleConnection.m_pspePrimaryResourceEntry.Add(localIdentifier, new OracleConnection.PSPEPrimaryConnectionInfo(isDBVer_11_1_0_7_OrHigher, this.m_opoConCtx.pOpoConValCtx->PSPE));
							if (this.m_opoConCtx.pOpoConValCtx->PSPE == 0 || (isDBVer_11_1_0_7_OrHigher && this.m_opoConCtx.pOpoConValCtx->PSPE == 1))
							{
								flag = false;
								if (this.m_promoteTxnMgr == null)
								{
									this.m_promoteTxnMgr = new PromotableTxnMgr();
								}
								bool flag2 = transaction.EnlistPromotableSinglePhase(this.m_promoteTxnMgr);
								if (flag2)
								{
									this.m_promoteTxnMgr.m_oraTransaction = this.BeginTransaction();
									this.m_bLocalTxnStartedForSysTxn = true;
									this.m_opoConCtx.m_promotableTxnManager = this.m_promoteTxnMgr;
									this.m_opoConCtx.m_systemTransaction = transaction;
									this.m_opoConCtx.m_txnType = TxnType.LocalTxnForSysTxn;
									this.m_promoteTxnMgr.m_localTxnIdentifier = transaction.TransactionInformation.LocalIdentifier;
									this.m_promoteTxnMgr.m_opsConCtx = this.m_opoConCtx.opsConCtx;
									this.m_promoteTxnMgr.m_opsErrCtx = this.m_opoConCtx.opsErrCtx;
									this.m_promoteTxnMgr.m_opoConRefCtx = this.m_opoConCtx.opoConRefCtx;
									ConnectionDispenser.CopyPooledConCtx(ref this.m_promoteTxnMgr.m_pOpoConValCtx, this.m_opoConCtx.pOpoConValCtx);
									transaction.TransactionCompleted += this.TransactionComplete;
								}
								else
								{
									this.m_opoConCtx.opoConRefCtx.pITransaction = (ITransaction)TransactionInterop.GetDtcTransaction(this.m_opoConCtx.m_systemTransaction);
									this.m_opoConCtx.m_promotableTxnManager = new PromotableTxnMgr();
									this.m_opoConCtx.m_promotableTxnManager.m_localTxnIdentifier = Transaction.Current.TransactionInformation.LocalIdentifier;
									this.m_opoConCtx.m_txnType = TxnType.SystemTxn;
									num = ConnectionDispenser.Enlist(this.m_opoConCtx);
									if (num != 0)
									{
										OracleException.HandleError(num, null, IntPtr.Zero, null);
									}
								}
							}
							else
							{
								this.m_opoConCtx.m_promotableTxnManager = new PromotableTxnMgr();
								this.m_opoConCtx.m_promotableTxnManager.m_localTxnIdentifier = transaction.TransactionInformation.LocalIdentifier;
								this.m_opoConCtx.m_txnType = TxnType.SystemTxn;
							}
						}
						else
						{
							OracleConnection.PSPEPrimaryConnectionInfo pspeprimaryConnectionInfo = obj as OracleConnection.PSPEPrimaryConnectionInfo;
							if (pspeprimaryConnectionInfo.m_pspeAttributeValue == 0 || this.m_opoConCtx.pOpoConValCtx->PSPE == 0 || !pspeprimaryConnectionInfo.m_dbSupportPromotion)
							{
								throw new TransactionPromotionException(OpoErrResManager.GetErrorMesg(ErrRes.CON_PSPE_RULE_VIOLATION, new string[0]));
							}
							TransactionInterop.GetTransmitterPropagationToken(transaction);
						}
					}
					else if (this.m_opoConCtx.pOpoConValCtx->PSPE == 0)
					{
						throw new TransactionPromotionException(OpoErrResManager.GetErrorMesg(ErrRes.CON_PSPE_RULE_VIOLATION, new string[0]));
					}
					if (flag)
					{
						this.m_opoConCtx.m_txnType = TxnType.SystemTxn;
						ITransaction transaction2 = (ITransaction)TransactionInterop.GetDtcTransaction(transaction);
						if (transaction2 != null)
						{
							try
							{
								this.m_opoConCtx.opoConRefCtx.pITransaction = transaction2;
								num = OpsCon.Enlist(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.pOpoConValCtx, this.m_opoConCtx.opoConRefCtx);
								if (num == 0)
								{
									this.m_opoConCtx.m_systemTransaction = transaction;
									this.m_opoConCtx.m_txnType = TxnType.SystemTxn;
									this.m_opoConCtx.pOpoConValCtx->InMtsTxn = 1;
									transaction.TransactionCompleted += this.TransactionComplete;
								}
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
								throw;
							}
						}
					}
				}
				if (num != 0)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_MTS_ENLIST_FAIL, new string[0]));
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleConnection::EnlistTransaction()\n"
				});
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00055C64 File Offset: 0x00054C64
		internal unsafe void TransactionComplete(object sender, TransactionEventArgs e)
		{
			if (this.m_state != ConnectionState.Closed)
			{
				lock (this.m_syncTxnComplete)
				{
					if (this.m_state != ConnectionState.Closed)
					{
						if (this.m_opoConCtx != null && this.m_opoConCtx.m_txnid != null)
						{
							this.m_opoConCtx.pool.m_cpCtx.m_htTxnIdToIntance.Remove(this.m_opoConCtx.m_txnid);
							this.m_opoConCtx.m_txnid = null;
						}
						try
						{
							if (!this.m_bLocalTxnStartedForSysTxn)
							{
								if (this.m_opoConCtx == null || null == this.m_opoConCtx.pOpoConValCtx || 1 != this.m_opoConCtx.pOpoConValCtx->InMtsTxn)
								{
									goto IL_12B;
								}
								try
								{
									this.m_opoConCtx.opoConRefCtx.pITransaction = null;
									OpsCon.Enlist(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.pOpoConValCtx, this.m_opoConCtx.opoConRefCtx);
									goto IL_12B;
								}
								catch (Exception ex)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex);
									}
									throw;
								}
							}
							try
							{
								if (this.m_promoteTxnMgr != null && this.m_promoteTxnMgr.m_bLocalTxnPromoted)
								{
									OpsCon.DelistPromotedTxn(this.m_opoConCtx.opsConCtx);
								}
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
								throw;
							}
							IL_12B:;
						}
						finally
						{
							this.m_bLocalTxnStartedForSysTxn = false;
							this.m_promoteTxnMgr = null;
							this.m_oraTransaction = null;
							if (this.m_opoConCtx != null)
							{
								if (this.m_opoConCtx.m_promotableTxnManager != null)
								{
									string localTxnIdentifier = this.m_opoConCtx.m_promotableTxnManager.m_localTxnIdentifier;
									if (!string.IsNullOrEmpty(localTxnIdentifier))
									{
										OracleConnection.m_pspePrimaryResourceEntry.Remove(localTxnIdentifier);
									}
									this.m_opoConCtx.m_promotableTxnManager.m_localTxnIdentifier = null;
									this.m_opoConCtx.m_promotableTxnManager = null;
								}
								this.m_opoConCtx.m_txnType = TxnType.None;
								this.m_opoConCtx.m_systemTransaction = null;
							}
						}
					}
					if (sender is Transaction)
					{
						(sender as Transaction).TransactionCompleted -= this.TransactionComplete;
					}
				}
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00055EB8 File Offset: 0x00054EB8
		public void FlushCache()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::FlushCache()\n"
				});
			}
			if (this.State == ConnectionState.Closed)
			{
				OracleException.HandleError(ErrRes.CON_CLOSED, this, this.m_opoConCtx.opsErrCtx, this);
			}
			int num = 0;
			try
			{
				num = OpsCon.FlushCache(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.opsErrCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::FlushCache()\n"
				});
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00055F84 File Offset: 0x00054F84
		private void ParseConnectionString()
		{
			int i = 0;
			string text = this.m_conString.Trim();
			int length = text.Length;
			while (i < length)
			{
				int num = 0;
				bool flag = false;
				bool flag2 = false;
				while (!flag2)
				{
					char c = text[i];
					if (c != ';' && c != '\t' && c != ' ')
					{
						flag2 = true;
					}
					else
					{
						if (i == length - 1)
						{
							return;
						}
						if (i < length)
						{
							i++;
						}
					}
				}
				int num2 = i;
				int num3 = text.IndexOf('=', i);
				if (num3 == -1)
				{
					this.RestoreConStrVals();
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_NOT_WELL_FORMED, new string[0]));
				}
				string text2 = text.Substring(num2, num3 - num2).Trim();
				int num4 = text.IndexOf(';', num3);
				if (num4 == -1)
				{
					num4 = length;
				}
				int num5 = text.IndexOf('"', num3);
				int num6 = text.IndexOf('\'', num3);
				if (num5 < num4 && num5 != -1)
				{
					int num7 = text.IndexOf('"', num5 + 1);
					if (num7 > num4)
					{
						num4 = text.IndexOf(';', num7 + 1);
						if (num4 == -1)
						{
							num4 = length;
						}
					}
					int num8 = text.IndexOf('"', num7 + 1);
					if (num8 == -1)
					{
						num8 = length;
					}
					if (num8 < num4)
					{
						if (string.Compare(text2, "PASSWORD", true) != 0 && string.Compare(text2, "PROXY PASSWORD", true) != 0)
						{
							throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_NOT_WELL_FORMED, new string[0]));
						}
						num = num8;
						while (num < num4 && num != -1)
						{
							num8 = num;
							num = text.IndexOf('"', num + 1);
						}
					}
					string text3 = text.Substring(num3 + 1, num4 - (num3 + 1));
					if (num6 != -1 && num6 < num5)
					{
						int num9 = text.IndexOf('\'', num7 + 1);
						if (num9 != -1 && num9 < num4)
						{
							text3 = text3.Trim(OracleConnection.delim2);
							flag = true;
						}
					}
					string text4;
					if (num == 0)
					{
						text4 = text.Substring(num5, num7 - (num5 - 1));
					}
					else
					{
						text4 = text.Substring(num5, num8 - (num5 - 1));
					}
					if (string.Compare(text3.Trim(OracleConnection.delim), text4.Trim(OracleConnection.delim)) != 0)
					{
						throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_NOT_WELL_FORMED, new string[0]));
					}
				}
				if (string.Compare(text2, "CONNECT TIMEOUT", true, CultureInfo.InvariantCulture) == 0)
				{
					text2 = "CONNECTION TIMEOUT";
				}
				if (!OracleConnection.m_AttribToIndex.ContainsKey(text2))
				{
					this.RestoreConStrVals();
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_ATTRIB, new string[]
					{
						text2
					}));
				}
				string text5 = text.Substring(++num3, num4 - num3).Trim();
				if (flag)
				{
					text5 = text5.Trim(OracleConnection.delim2);
				}
				if (text5 != null && text5.Length != 0)
				{
					if (text5[0] == '"')
					{
						if (text5[text5.Length - 1] != '"')
						{
							this.RestoreConStrVals();
							throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_NOT_WELL_FORMED, new string[0]));
						}
						if (string.Compare(text5, "\"/\"", true) == 0)
						{
							text5 = "/";
						}
						else if (string.Compare(text2, "USER ID", true, CultureInfo.InvariantCulture) != 0 && string.Compare(text2, "PROXY USER ID", true, CultureInfo.InvariantCulture) != 0 && string.Compare(text2, "PASSWORD", true) != 0 && string.Compare(text2, "PROXY PASSWORD", true) != 0)
						{
							text5 = text5.Trim(OracleConnection.delim1).ToLower();
						}
						else
						{
							text5 = text5.Trim(new char[]
							{
								' ',
								'\t'
							});
						}
						int num10 = (int)OracleConnection.m_AttribToIndex[text2];
						if (num10 <= OracleConnection.IndexStrAttribMax)
						{
							if (num10 != OracleConnection.IndexPasswd && num10 != OracleConnection.IndexProxyPwd)
							{
								this.m_conStrVals[num10] = text5;
							}
						}
						else if (num10 <= OracleConnection.IndexIntAttribMax)
						{
							this.m_conStrVals[num10] = int.Parse(text5, NumberStyles.AllowLeadingSign);
						}
						else if (num10 <= OracleConnection.IndexBoolAttribMax)
						{
							object obj;
							if (num10 == OracleConnection.IndexEnlist)
							{
								if (text5.ToLower(CultureInfo.InvariantCulture) == "dynamic")
								{
									obj = 2;
								}
								else
								{
									obj = OracleConnection.m_boolMapping[text5.ToLower()];
								}
							}
							else
							{
								obj = OracleConnection.m_boolMapping[text5.ToLower()];
							}
							if (obj == null)
							{
								this.RestoreConStrVals();
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
								{
									text2,
									text5
								}));
							}
							this.m_conStrVals[num10] = (int)obj;
						}
					}
					else
					{
						int num10 = (int)OracleConnection.m_AttribToIndex[text2];
						if (num10 <= OracleConnection.IndexStrAttribMax)
						{
							if (num10 != OracleConnection.IndexPasswd && num10 != OracleConnection.IndexProxyPwd)
							{
								this.m_conStrVals[num10] = text5;
							}
						}
						else if (num10 <= OracleConnection.IndexIntAttribMax)
						{
							this.m_conStrVals[num10] = int.Parse(text5, NumberStyles.AllowLeadingSign);
						}
						else if (num10 <= OracleConnection.IndexBoolAttribMax)
						{
							object obj2;
							if (num10 == OracleConnection.IndexEnlist)
							{
								if (text5.ToLower(CultureInfo.InvariantCulture) == "dynamic")
								{
									obj2 = 2;
								}
								else
								{
									obj2 = OracleConnection.m_boolMapping[text5.ToLower()];
								}
							}
							else
							{
								obj2 = OracleConnection.m_boolMapping[text5.ToLower()];
							}
							if (obj2 == null)
							{
								this.RestoreConStrVals();
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
								{
									text2,
									text5
								}));
							}
							this.m_conStrVals[num10] = (int)obj2;
						}
					}
				}
				else
				{
					int num10 = (int)OracleConnection.m_AttribToIndex[text2];
					if ((int)OracleConnection.m_AttribToIndex[text2] <= OracleConnection.IndexStrAttribMax)
					{
						this.m_conStrVals[num10] = string.Empty;
					}
				}
				i = num4 + 1;
			}
			if (this.m_conString != null && this.m_conString.Length != 0)
			{
				this.ValidateValues();
			}
			this.m_internalConStr = this.ConstructConString();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000565C0 File Offset: 0x000555C0
		private string GetPasswordLessString(string conString)
		{
			string value = "password";
			string value2 = "proxy password";
			string[] array = conString.Split(new char[]
			{
				';'
			});
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i].ToLower();
				if (text.IndexOf(value) == -1 && text.IndexOf(value2) == -1)
				{
					stringBuilder.Append(array[i]);
					goto IL_A5;
				}
				string[] array2 = text.Split(new char[]
				{
					'='
				});
				string text2 = array2[0].Trim();
				if (!text2.Equals(value) && !text2.Equals(value2))
				{
					stringBuilder.Append(array[i]);
					goto IL_A5;
				}
				IL_BA:
				i++;
				continue;
				IL_A5:
				if (i < array.Length - 1)
				{
					stringBuilder.Append(";");
					goto IL_BA;
				}
				goto IL_BA;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000566A0 File Offset: 0x000556A0
		private void ResetAttribsToDefaults()
		{
			this.m_conStrVals[OracleConnection.IndexUserID] = "";
			this.m_conStrVals[OracleConnection.IndexPasswd] = "";
			this.m_conStrVals[OracleConnection.IndexLifetime] = 0;
			this.m_conStrVals[OracleConnection.IndexPoolInc] = 5;
			this.m_conStrVals[OracleConnection.IndexPoolDec] = 1;
			this.m_conStrVals[OracleConnection.IndexTimeout] = 15;
			this.m_conStrVals[OracleConnection.IndexDataSrc] = "";
			this.m_conStrVals[OracleConnection.IndexEnlist] = 1;
			this.m_conStrVals[OracleConnection.IndexMaxPool] = 100;
			this.m_conStrVals[OracleConnection.IndexMinPool] = 1;
			this.m_conStrVals[OracleConnection.IndexPoolReg] = 180;
			this.m_conStrVals[OracleConnection.IndexPersist] = 0;
			this.m_conStrVals[OracleConnection.IndexPooling] = 1;
			this.m_conStrVals[OracleConnection.IndexProxyUsr] = "";
			this.m_conStrVals[OracleConnection.IndexProxyPwd] = "";
			this.m_conStrVals[OracleConnection.IndexDBAPriv] = "";
			this.m_conStrVals[OracleConnection.IndexValidCon] = 0;
			if (OraTrace.m_MetadataPooling != 0)
			{
				this.m_conStrVals[OracleConnection.IndexMetaPool] = 1;
			}
			else
			{
				this.m_conStrVals[OracleConnection.IndexMetaPool] = 0;
			}
			this.m_conStrVals[OracleConnection.IndexGridCR] = 0;
			this.m_conStrVals[OracleConnection.IndexGridRLB] = 0;
			this.m_conStrVals[OracleConnection.IndexCtxConn] = 0;
			this.m_conStrVals[OracleConnection.IndexStmtCachePurge] = 0;
			this.m_conStrVals[OracleConnection.IndexAppEdition] = OraTrace.m_appEdition;
			if (OraTrace.m_StmtCacheSize > 0)
			{
				this.m_conStrVals[OracleConnection.IndexStmtCache] = OraTrace.m_StmtCacheSize;
			}
			else
			{
				this.m_conStrVals[OracleConnection.IndexStmtCache] = 0;
			}
			if (OraTrace.m_PSPE > 0)
			{
				this.m_conStrVals[OracleConnection.IndexPSPE] = "promotable";
			}
			else
			{
				this.m_conStrVals[OracleConnection.IndexPSPE] = "local";
			}
			if (OraTrace.m_selfTuning)
			{
				this.m_conStrVals[OracleConnection.IndexSelfTuning] = 1;
				return;
			}
			this.m_conStrVals[OracleConnection.IndexSelfTuning] = 0;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000568F0 File Offset: 0x000558F0
		private void Init()
		{
			this.m_conString = string.Empty;
			this.m_conTimeout = 15;
			this.m_dataSource = string.Empty;
			this.m_serverVersion = string.Empty;
			this.m_state = ConnectionState.Closed;
			this.m_tmpConString = string.Empty;
			this.m_serviceName = string.Empty;
			this.m_databaseName = string.Empty;
			this.m_databaseDomainName = string.Empty;
			this.m_hostName = string.Empty;
			this.m_instanceName = string.Empty;
			this.m_DataReaderList = new ArrayList();
			this.m_opoConCtx = new OpoConCtx();
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00056988 File Offset: 0x00055988
		private void ValidateValues()
		{
			int num = 0;
			int num2 = 0;
			string text = (string)this.m_conStrVals[OracleConnection.IndexDBAPriv];
			if (text != null && text.Length != 0 && text.ToLower() != "sysdba" && text.ToLower() != "sysoper")
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"DBA Privilege",
					text
				}));
			}
			text = (string)this.m_conStrVals[OracleConnection.IndexPSPE];
			if (text != null && text.Length != 0 && text.ToLower() != "local" && text.ToLower() != "promotable")
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Promotable Transaction",
					text
				}));
			}
			try
			{
				num = (int)this.m_conStrVals[OracleConnection.IndexTimeout];
			}
			catch
			{
				string errorMesg = OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Connection Timeout",
					num.ToString()
				});
				this.RestoreConStrVals();
				throw new ArgumentException(errorMesg);
			}
			if (num < 0)
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Connection Timeout",
					num.ToString()
				}));
			}
			try
			{
				num = (int)this.m_conStrVals[OracleConnection.IndexLifetime];
			}
			catch
			{
				string errorMesg2 = OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Connection Lifetime",
					num.ToString()
				});
				this.RestoreConStrVals();
				throw new ArgumentException(errorMesg2);
			}
			if (num < 0)
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Connection Lifetime",
					num.ToString()
				}));
			}
			try
			{
				num = (int)this.m_conStrVals[OracleConnection.IndexMaxPool];
			}
			catch
			{
				string errorMesg3 = OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Max Pool Size",
					num.ToString()
				});
				this.RestoreConStrVals();
				throw new ArgumentException(errorMesg3);
			}
			if (num <= 0)
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Max Pool Size",
					num.ToString()
				}));
			}
			try
			{
				num2 = (int)this.m_conStrVals[OracleConnection.IndexMinPool];
			}
			catch
			{
				string errorMesg4 = OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Min Pool Size",
					num2.ToString()
				});
				this.RestoreConStrVals();
				throw new ArgumentException(errorMesg4);
			}
			if (num2 < 0)
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Min Pool Size",
					num2.ToString()
				}));
			}
			if (num < num2)
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Max Pool Size",
					num.ToString()
				}));
			}
			try
			{
				num = (int)this.m_conStrVals[OracleConnection.IndexPoolInc];
			}
			catch
			{
				string errorMesg5 = OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Connection Increment",
					num.ToString()
				});
				this.RestoreConStrVals();
				throw new ArgumentException(errorMesg5);
			}
			if (num <= 0)
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Connection Increment",
					num.ToString()
				}));
			}
			try
			{
				num = (int)this.m_conStrVals[OracleConnection.IndexPoolDec];
			}
			catch
			{
				string errorMesg6 = OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Connection Decrement",
					num.ToString()
				});
				this.RestoreConStrVals();
				throw new ArgumentException(errorMesg6);
			}
			if (num <= 0)
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Connection Decrement",
					num.ToString()
				}));
			}
			try
			{
				num = (int)this.m_conStrVals[OracleConnection.IndexStmtCache];
			}
			catch
			{
				string errorMesg7 = OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Statement Cache Size",
					num.ToString()
				});
				this.RestoreConStrVals();
				throw new ArgumentException(errorMesg7);
			}
			if (num < 0)
			{
				this.RestoreConStrVals();
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Statement Cache Size",
					num.ToString()
				}));
			}
			bool flag = false;
			try
			{
				flag = Convert.ToBoolean(this.m_conStrVals[OracleConnection.IndexSelfTuning]);
			}
			catch
			{
				string errorMesg8 = OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Self Tuning",
					flag.ToString()
				});
				this.RestoreConStrVals();
				throw new ArgumentException(errorMesg8);
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00056F08 File Offset: 0x00055F08
		private void RestoreConStrVals()
		{
			if (this.m_tmpConString == null)
			{
				this.m_conString = string.Empty;
				return;
			}
			this.ConnectionString = this.m_tmpConString;
			this.m_tmpConString = string.Empty;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00056F38 File Offset: 0x00055F38
		internal string ConstructConString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append("datasrc=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexDataSrc]);
			stringBuilder.Append(";enlist=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexEnlist]);
			stringBuilder.Append(";lifetime=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexLifetime]);
			stringBuilder.Append(";maxsize=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexMaxPool]);
			stringBuilder.Append(";minsize=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexMinPool]);
			stringBuilder.Append(";incsize=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexPoolInc]);
			stringBuilder.Append(";decsize=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexPoolDec]);
			stringBuilder.Append(";timeout=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexTimeout]);
			stringBuilder.Append(";dbapriv=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexDBAPriv]);
			stringBuilder.Append(";validcon=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexValidCon]);
			if (!Convert.ToBoolean(this.m_conStrVals[OracleConnection.IndexSelfTuning]))
			{
				stringBuilder.Append(";stmtcache=");
				stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexStmtCache]);
			}
			else
			{
				stringBuilder.Append(";stmtcache=0");
			}
			if ((int)this.m_conStrVals[OracleConnection.IndexStmtCache] > 0 && !Convert.ToBoolean(this.m_conStrVals[OracleConnection.IndexSelfTuning]))
			{
				stringBuilder.Append(";stmtcachepurge=");
				stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexStmtCachePurge]);
			}
			else
			{
				stringBuilder.Append(";stmtcachepurge=0");
			}
			stringBuilder.Append(";metapool=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexMetaPool]);
			stringBuilder.Append(";selftuning=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexSelfTuning]);
			stringBuilder.Append(";pspe=");
			stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexPSPE]);
			int num = (int)this.m_conStrVals[OracleConnection.IndexGridCR];
			int num2 = (int)this.m_conStrVals[OracleConnection.IndexGridRLB];
			if (num == 1)
			{
				stringBuilder.Append(";gridrac=");
				stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexGridCR]);
			}
			else
			{
				stringBuilder.Append(";gridrac=");
				stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexGridRLB]);
			}
			if (this.m_conStrVals[OracleConnection.IndexProxyUsr].ToString().Length > 0)
			{
				stringBuilder.Append(";pxyusr=");
				stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexProxyUsr]);
			}
			else
			{
				bool flag = false;
				if (((string)this.m_conStrVals[OracleConnection.IndexUserID]).Trim(OracleConnection.delim) == "/")
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
					stringBuilder.Append(this.m_conStrVals[OracleConnection.IndexUserID]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0005728B File Offset: 0x0005628B
		internal OracleTransaction GetTransaction()
		{
			if (this.m_oraTransaction != null && this.m_oraTransaction.Completed)
			{
				this.m_oraTransaction = null;
			}
			return this.m_oraTransaction;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000572AF File Offset: 0x000562AF
		internal unsafe bool IsInMtsTxn()
		{
			return this.m_opoConCtx != null && this.m_opoConCtx.pOpoConValCtx != null && this.m_opoConCtx.pOpoConValCtx->InMtsTxn == 1;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000572DE File Offset: 0x000562DE
		internal void EndTransaction()
		{
			this.m_oraTransaction = null;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000572E8 File Offset: 0x000562E8
		internal void OnInfoMessage(object obj, OracleInfoMessageEventArgs eventArgs)
		{
			if (this.m_infoMessageEventHandler != null)
			{
				try
				{
					this.m_infoMessageEventHandler(obj, eventArgs);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00057320 File Offset: 0x00056320
		protected override void OnStateChange(StateChangeEventArgs eventArgs)
		{
			if (this.m_stateChangeEventHandler != null)
			{
				this.m_stateChangeEventHandler(this, eventArgs);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00057338 File Offset: 0x00056338
		internal void RaiseStateChange(ConnectionState originalState, ConnectionState currentState)
		{
			StateChangeEventArgs stateChange = new StateChangeEventArgs(originalState, currentState);
			this.OnStateChange(stateChange);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00057354 File Offset: 0x00056354
		internal int OnFailover(OracleFailoverEventArgs eventArgs)
		{
			int result = 0;
			if (this.m_failoverEventHandler != null)
			{
				try
				{
					return (int)this.m_failoverEventHandler(this, eventArgs);
				}
				catch
				{
					return result;
				}
			}
			if (eventArgs.FailoverEvent == FailoverEvent.Error)
			{
				return 25410;
			}
			return result;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000573A0 File Offset: 0x000563A0
		internal static void OnHAEvent(object state)
		{
			OracleHAEventArgs eventArgs = (OracleHAEventArgs)state;
			if (OracleConnection.m_haEventHandler != null)
			{
				OracleConnection.m_haEventHandler(eventArgs);
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000573C8 File Offset: 0x000563C8
		internal Type GetCustomUdt(string udtName)
		{
			Type result = null;
			ConnectionState state = this.m_state;
			return result;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000573E1 File Offset: 0x000563E1
		internal static void SetOdtConnection(bool bIsOdtConnection)
		{
			OracleConnection.s_bIsOdtConnection = bIsOdtConnection;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000573EC File Offset: 0x000563EC
		private unsafe void OpenExtprocConnection()
		{
			IntPtr ociExtProcContext = OracleConnection.GetOciExtProcContext();
			this.m_extProcEnv = (Thread.GetData(OracleConnection.m_oraThreadDataSlot) as OracleConnection.ThreadData).m_extProcEnv;
			if (!this.m_internalUse && OracleConnection.ExternalContextConnection != null && OracleConnection.ExternalContextConnection.State != ConnectionState.Closed)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_CTX_CONN_OPENED_ALREADY, new string[0]));
			}
			int num = 0;
			try
			{
				if (this.m_opoConCtx.opoConRefCtx == null)
				{
					this.m_opoConCtx.opoConRefCtx = new OpoConRefCtx();
				}
				this.m_opoConCtx.opoConRefCtx.pITransaction = null;
				if (this.m_opoConCtx.pOpoConValCtx == null)
				{
					OpsCon.AllocValCtx(ref this.m_opoConCtx.pOpoConValCtx);
				}
				this.m_opoConCtx.pOpoConValCtx->Enlist = 0;
				this.m_opoConCtx.m_bSelfTuning = false;
				this.m_opoConCtx.pOpoConValCtx->StmtCacheSize = (int)this.m_conStrVals[OracleConnection.IndexStmtCache];
				num = OpsCon.OpenUsingExtProcContext(ociExtProcContext, ref this.m_opoConCtx.opsConCtx, ref this.m_opoConCtx.opsErrCtx, this.m_opoConCtx.pOpoConValCtx, ref this.m_opoConCtx.opoConRefCtx);
				this.m_majorVersion = this.m_opoConCtx.pOpoConValCtx->MajorVersion;
				this.m_minorVersion = this.m_opoConCtx.pOpoConValCtx->MinorVersion;
				this.m_PatchSetVersion = this.m_opoConCtx.pOpoConValCtx->PatchSetVersion;
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				OracleException.HandleError(ErrRes.INT_ERR, null, IntPtr.Zero, this);
			}
			if (this.m_internalUse)
			{
				if (OracleConnection.ExternalContextConnection != null && OracleConnection.ExternalContextConnection.State == ConnectionState.Open)
				{
					this.m_conSignature = OracleConnection.ExternalContextConnection.m_conSignature;
				}
				else
				{
					if (this.m_opoConCtx.pOpoConValCtx->ConSignature == 2147483647)
					{
						this.m_opoConCtx.pOpoConValCtx->ConSignature = 0;
					}
					else
					{
						this.m_opoConCtx.pOpoConValCtx->ConSignature = this.m_opoConCtx.pOpoConValCtx->ConSignature + 1;
					}
					this.m_conSignature = ((double)this.m_opoConCtx.pOpoConValCtx->ConSignature + (double)((long)this.m_opoConCtx.opsConCtx) / 10000000000.0).GetHashCode();
				}
				if (OracleConnection.InternalContextConnection != null && OracleConnection.InternalContextConnection.State == ConnectionState.Open)
				{
					OracleConnection.InternalContextConnection.Close();
				}
				OracleConnection.InternalContextConnection = this;
			}
			else
			{
				if (OracleConnection.InternalContextConnection != null && OracleConnection.InternalContextConnection.State == ConnectionState.Open)
				{
					this.m_conSignature = OracleConnection.InternalContextConnection.m_conSignature;
				}
				else
				{
					if (this.m_opoConCtx.pOpoConValCtx->ConSignature == 2147483647)
					{
						this.m_opoConCtx.pOpoConValCtx->ConSignature = 0;
					}
					else
					{
						this.m_opoConCtx.pOpoConValCtx->ConSignature = this.m_opoConCtx.pOpoConValCtx->ConSignature + 1;
					}
					this.m_conSignature = ((double)this.m_opoConCtx.pOpoConValCtx->ConSignature + (double)((long)this.m_opoConCtx.opsConCtx) / 10000000000.0).GetHashCode();
				}
				if (OracleConnection.ExternalContextConnection != null && OracleConnection.ExternalContextConnection.State == ConnectionState.Open)
				{
					OracleConnection.ExternalContextConnection.Close();
				}
				OracleConnection.ExternalContextConnection = this;
			}
			this.m_state = ConnectionState.Open;
			this.m_serverVersion = this.m_opoConCtx.opoConRefCtx.serverVersion;
			this.m_pwdValidated = true;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0005776C File Offset: 0x0005676C
		internal static OracleConnection GetInternalConnection()
		{
			OracleConnection.ThreadData threadData = Thread.GetData(OracleConnection.m_oraThreadDataSlot) as OracleConnection.ThreadData;
			if (threadData == null)
			{
				throw new InvalidOperationException();
			}
			if (threadData.m_internalExtprocConn == null)
			{
				threadData.m_internalExtprocConn = new OracleConnection("context connection=true");
				threadData.m_internalExtprocConn.m_internalUse = true;
				threadData.m_internalExtprocConn.Open();
			}
			return threadData.m_internalExtprocConn;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000577C8 File Offset: 0x000567C8
		private static IntPtr GetOciExtProcContext()
		{
			OracleConnection.ThreadData threadData = Thread.GetData(OracleConnection.m_oraThreadDataSlot) as OracleConnection.ThreadData;
			if (threadData == null)
			{
				return IntPtr.Zero;
			}
			return threadData.m_ociExtProcContext;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060008A3 RID: 2211 RVA: 0x000577F4 File Offset: 0x000567F4
		// (remove) Token: 0x060008A4 RID: 2212 RVA: 0x0005780D File Offset: 0x0005680D
		public event OracleInfoMessageEventHandler InfoMessage
		{
			add
			{
				this.m_infoMessageEventHandler = (OracleInfoMessageEventHandler)Delegate.Combine(this.m_infoMessageEventHandler, value);
			}
			remove
			{
				this.m_infoMessageEventHandler = (OracleInfoMessageEventHandler)Delegate.Remove(this.m_infoMessageEventHandler, value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060008A5 RID: 2213 RVA: 0x00057826 File Offset: 0x00056826
		// (remove) Token: 0x060008A6 RID: 2214 RVA: 0x0005783F File Offset: 0x0005683F
		public override event StateChangeEventHandler StateChange
		{
			add
			{
				this.m_stateChangeEventHandler = (StateChangeEventHandler)Delegate.Combine(this.m_stateChangeEventHandler, value);
			}
			remove
			{
				this.m_stateChangeEventHandler = (StateChangeEventHandler)Delegate.Remove(this.m_stateChangeEventHandler, value);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060008A7 RID: 2215 RVA: 0x00057858 File Offset: 0x00056858
		// (remove) Token: 0x060008A8 RID: 2216 RVA: 0x0005786F File Offset: 0x0005686F
		public static event OracleHAEventHandler HAEvent
		{
			add
			{
				OracleConnection.m_haEventHandler = (OracleHAEventHandler)Delegate.Combine(OracleConnection.m_haEventHandler, value);
			}
			remove
			{
				OracleConnection.m_haEventHandler = (OracleHAEventHandler)Delegate.Remove(OracleConnection.m_haEventHandler, value);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060008A9 RID: 2217 RVA: 0x00057888 File Offset: 0x00056888
		// (remove) Token: 0x060008AA RID: 2218 RVA: 0x0005796C File Offset: 0x0005696C
		public event OracleFailoverEventHandler Failover
		{
			add
			{
				if (this.m_contextConnection)
				{
					throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_CTX_CONN, new string[0]));
				}
				if (this.m_state != ConnectionState.Open)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				int num = 0;
				bool failoverEventHandler = this.m_failoverEventHandler != null;
				this.m_failoverEventHandler = value;
				if (!failoverEventHandler)
				{
					this.cb = new OraFailoverCallback_FPtr(this.OnFailoverCallback_fn);
					try
					{
						num = OpsCon.RegisterFailoverCallback(this.m_opoConCtx.opsConCtx, this.m_opoConCtx.opsErrCtx, this.cb);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					finally
					{
						if (num != 0)
						{
							OracleException.HandleError(num, this, this.m_opoConCtx.opsErrCtx, this);
						}
					}
				}
			}
			remove
			{
				if (this.m_contextConnection)
				{
					throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_CTX_CONN, new string[0]));
				}
				this.m_failoverEventHandler = (OracleFailoverEventHandler)Delegate.Remove(this.m_failoverEventHandler, value);
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000579A4 File Offset: 0x000569A4
		internal static void ValidateAdminValues(OracleConnection conn)
		{
			if (conn.m_internalConStr == null)
			{
				throw new InvalidOperationException();
			}
			object[] array;
			if (conn.m_conStrVals != null)
			{
				array = conn.m_conStrVals;
			}
			else
			{
				array = (object[])MetaData.m_connDataPooler.Get(OracleConnection.ConStrAtrribs, conn.m_pwdLessString);
			}
			if (array == null)
			{
				throw new InvalidOperationException();
			}
			conn.m_opoConCtx.gridCR = (int)array[OracleConnection.IndexGridCR];
			conn.m_opoConCtx.gridRLB = (int)array[OracleConnection.IndexGridRLB];
			conn.m_opoConCtx.bGridRac = (conn.m_opoConCtx.gridCR == 1 || conn.m_opoConCtx.gridRLB == 1);
			conn.m_opoConCtx.conString = conn.m_internalConStr;
			conn.m_opoConCtx.dataSrc = conn.m_dataSource;
			if (conn.m_opoConCtx.bGridRac)
			{
				if (ConnectionDispenser.m_htTnsToSvc == null || ConnectionDispenser.m_htSvcToRLB == null)
				{
					throw new InvalidOperationException();
				}
				string text = (string)ConnectionDispenser.m_htTnsToSvc[conn.m_opoConCtx.dataSrc];
				if (text == null)
				{
					throw new InvalidOperationException();
				}
				RLBCtx rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[text];
				if (rlbctx == null)
				{
					throw new InvalidOperationException();
				}
				if ((CPCtx)rlbctx.htConToInst[conn.m_opoConCtx.conString] == null)
				{
					throw new InvalidOperationException();
				}
			}
			else
			{
				if (ConnectionDispenser.m_ConnectionPools == null)
				{
					throw new InvalidOperationException();
				}
				if (ConnectionDispenser.m_ConnectionPools[conn.m_internalConStr] == null)
				{
					throw new InvalidOperationException();
				}
				if (conn.m_opoConCtx.pool == null)
				{
					conn.m_opoConCtx.pool = (ConnectionPool)ConnectionDispenser.m_ConnectionPools[conn.m_internalConStr];
				}
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00057B48 File Offset: 0x00056B48
		internal static void CloseExtprocConnection()
		{
			if (OracleConnection.m_oraThreadDataSlot == null)
			{
				return;
			}
			OracleConnection.ThreadData threadData = Thread.GetData(OracleConnection.m_oraThreadDataSlot) as OracleConnection.ThreadData;
			if (threadData != null)
			{
				lock (threadData.m_extProcEnv)
				{
					threadData.m_extProcEnv.m_status = false;
				}
				if (threadData.m_externalExtprocConn != null && threadData.m_externalExtprocConn.State == ConnectionState.Open)
				{
					threadData.m_externalExtprocConn.Close();
					threadData.m_externalExtprocConn = null;
				}
				if (threadData.m_internalExtprocConn != null && threadData.m_internalExtprocConn.State == ConnectionState.Open)
				{
					threadData.m_internalExtprocConn.Close();
					threadData.m_internalExtprocConn = null;
				}
				threadData.m_ociExtProcContext = IntPtr.Zero;
				Thread.SetData(OracleConnection.m_oraThreadDataSlot, null);
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00057C14 File Offset: 0x00056C14
		internal static void CreateExtprocTDS()
		{
			if (OracleConnection.m_oraThreadDataSlot == null)
			{
				OracleConnection.m_oraThreadDataSlot = Thread.AllocateDataSlot();
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00057C28 File Offset: 0x00056C28
		internal static void SetExtProcContext(IntPtr extProcContext)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::SetExtProcContext()\n"
				});
			}
			OracleConnection.ThreadData data = new OracleConnection.ThreadData(extProcContext);
			Thread.SetData(OracleConnection.m_oraThreadDataSlot, data);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleConnection::SetExtProcContext()\n"
				});
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00057C81 File Offset: 0x00056C81
		internal static void SetExtProcFlag()
		{
			OpsCom.Exf();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00057C88 File Offset: 0x00056C88
		public static void ClearPool(OracleConnection conn)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection.ClearPool()\n"
				});
			}
			if (conn == null)
			{
				throw new ArgumentNullException();
			}
			if (conn.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			if (1 == OraTrace.m_demandOrclPermission && conn.m_orclPermission != null)
			{
				conn.m_orclPermission.Demand();
			}
			try
			{
				OracleConnection.ValidateAdminValues(conn);
				ConnectionDispenser.ClearPool(conn.m_opoConCtx, false, false);
			}
			catch
			{
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::ClearPool()\n"
				});
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00057D3C File Offset: 0x00056D3C
		public static void ClearAllPools()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::ClearAllPools()\n"
				});
			}
			if (1 == OraTrace.m_demandOrclPermission)
			{
				new OraclePermission(PermissionState.Unrestricted).Demand();
			}
			if ((ConnectionDispenser.m_ConnectionPools == null || ConnectionDispenser.m_ConnectionPools.Count == 0) && (ConnectionDispenser.m_htSvcToRLB == null || ConnectionDispenser.m_htSvcToRLB.Count == 0))
			{
				throw new InvalidOperationException();
			}
			ConnectionDispenser.ClearAllPools();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::ClearAllPools()\n"
				});
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00057DC8 File Offset: 0x00056DC8
		protected override DbCommand CreateDbCommand()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					"(ENTRY) OracleConnection::CreateDbCommand()\n"
				});
			}
			DbCommand result = new OracleCommand("", this);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::CreateDbCommand()\n"
				});
			}
			return result;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00057E1C File Offset: 0x00056E1C
		protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnection::BeginDbTransaction()\n"
				});
			}
			if (System.Data.IsolationLevel.Unspecified == isolationLevel)
			{
				isolationLevel = System.Data.IsolationLevel.ReadCommitted;
			}
			DbTransaction result = this.BeginTransaction(isolationLevel);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnection::BeginDbTransaction()\n"
				});
			}
			return result;
		}

		// Token: 0x0400070B RID: 1803
		private const string METADATA_COLLECTION = "METADATACOLLECTIONS";

		// Token: 0x0400070C RID: 1804
		private const string DATA_TYPES = "DATATYPES";

		// Token: 0x0400070D RID: 1805
		private const string RESTRICTIONS = "RESTRICTIONS";

		// Token: 0x0400070E RID: 1806
		private const string RESERVED_WORDS = "RESERVEDWORDS";

		// Token: 0x0400070F RID: 1807
		private const string DATA_SOURCE_INFORMATION = "DATASOURCEINFORMATION";

		// Token: 0x04000710 RID: 1808
		private const string ORCL_COMMAND = "ORACLECOMMAND";

		// Token: 0x04000711 RID: 1809
		private const string DATA_TABLE = "DATATABLE";

		// Token: 0x04000712 RID: 1810
		internal const string m_sDynamic = "dynamic";

		// Token: 0x04000713 RID: 1811
		internal const string m_sSysdba = "sysdba";

		// Token: 0x04000714 RID: 1812
		internal const string m_sSysoper = "sysoper";

		// Token: 0x04000715 RID: 1813
		internal const string m_sLocal = "local";

		// Token: 0x04000716 RID: 1814
		internal const string m_sPromotable = "promotable";

		// Token: 0x04000717 RID: 1815
		private static LocalDataStoreSlot m_oraThreadDataSlot;

		// Token: 0x04000718 RID: 1816
		private static bool m_extproc;

		// Token: 0x04000719 RID: 1817
		private static bool m_extprocFlagRead;

		// Token: 0x0400071A RID: 1818
		internal bool m_bPrelimAuthSession;

		// Token: 0x0400071B RID: 1819
		internal bool m_bStartupShutdown;

		// Token: 0x0400071C RID: 1820
		private static object s_lockObj = new object();

		// Token: 0x0400071D RID: 1821
		private static char[] trimSpaces = new char[]
		{
			' ',
			'\r',
			'\t',
			'\n'
		};

		// Token: 0x0400071E RID: 1822
		private static char[] doubleQuotes = new char[]
		{
			'"'
		};

		// Token: 0x0400071F RID: 1823
		private static char[] semiColon = new char[]
		{
			';'
		};

		// Token: 0x04000720 RID: 1824
		private static char[] equalSign = new char[]
		{
			'='
		};

		// Token: 0x04000721 RID: 1825
		private DataSet m_metaDataCollectionDS;

		// Token: 0x04000722 RID: 1826
		internal string m_conString;

		// Token: 0x04000723 RID: 1827
		private string m_dataSource;

		// Token: 0x04000724 RID: 1828
		private string m_serverVersion;

		// Token: 0x04000725 RID: 1829
		private int m_conTimeout;

		// Token: 0x04000726 RID: 1830
		internal ConnectionState m_state;

		// Token: 0x04000727 RID: 1831
		private object[] m_conStrVals;

		// Token: 0x04000728 RID: 1832
		private string m_tmpConString;

		// Token: 0x04000729 RID: 1833
		private static Hashtable m_boolMapping;

		// Token: 0x0400072A RID: 1834
		private static SortedList m_AttribToIndex;

		// Token: 0x0400072B RID: 1835
		internal OpoConCtx m_opoConCtx;

		// Token: 0x0400072C RID: 1836
		internal bool m_disposed;

		// Token: 0x0400072D RID: 1837
		internal OracleTransaction m_oraTransaction;

		// Token: 0x0400072E RID: 1838
		private bool m_validConString;

		// Token: 0x0400072F RID: 1839
		internal int m_conSignature;

		// Token: 0x04000730 RID: 1840
		private bool m_openWithNewPwd;

		// Token: 0x04000731 RID: 1841
		private string m_pwdLessString;

		// Token: 0x04000732 RID: 1842
		private string m_pwdOSLessString;

		// Token: 0x04000733 RID: 1843
		private bool m_pwdValidated;

		// Token: 0x04000734 RID: 1844
		internal string m_internalConStr;

		// Token: 0x04000735 RID: 1845
		internal bool m_conStrValsFromPool;

		// Token: 0x04000736 RID: 1846
		internal bool m_persist;

		// Token: 0x04000737 RID: 1847
		internal bool m_contextConnection;

		// Token: 0x04000738 RID: 1848
		internal bool m_internalUse;

		// Token: 0x04000739 RID: 1849
		internal OracleConnection.ExtProcEnv m_extProcEnv;

		// Token: 0x0400073A RID: 1850
		internal int m_majorVersion;

		// Token: 0x0400073B RID: 1851
		internal int m_minorVersion;

		// Token: 0x0400073C RID: 1852
		internal int m_PatchSetVersion;

		// Token: 0x0400073D RID: 1853
		internal static char[] delim = new char[]
		{
			' ',
			'\t',
			'"'
		};

		// Token: 0x0400073E RID: 1854
		internal static char[] delim1 = new char[]
		{
			'"'
		};

		// Token: 0x0400073F RID: 1855
		internal static char[] delim2 = new char[]
		{
			' ',
			'\''
		};

		// Token: 0x04000740 RID: 1856
		internal string m_password;

		// Token: 0x04000741 RID: 1857
		internal string m_proxyPassword;

		// Token: 0x04000742 RID: 1858
		internal int m_stmtCacheSize = OraTrace.m_StmtCacheSize;

		// Token: 0x04000743 RID: 1859
		internal OraFailoverCallback_FPtr cb;

		// Token: 0x04000744 RID: 1860
		internal OracleInfoMessageEventHandler m_infoMessageEventHandler;

		// Token: 0x04000745 RID: 1861
		internal StateChangeEventHandler m_stateChangeEventHandler;

		// Token: 0x04000746 RID: 1862
		internal OracleFailoverEventHandler m_failoverEventHandler;

		// Token: 0x04000747 RID: 1863
		internal static OracleHAEventHandler m_haEventHandler;

		// Token: 0x04000748 RID: 1864
		internal int m_enlist;

		// Token: 0x04000749 RID: 1865
		internal static Hashtable m_pspePrimaryResourceEntry = null;

		// Token: 0x0400074A RID: 1866
		internal bool m_bLocalTxnStartedForSysTxn;

		// Token: 0x0400074B RID: 1867
		internal PromotableTxnMgr m_promoteTxnMgr;

		// Token: 0x0400074C RID: 1868
		internal object m_syncTxnComplete = new object();

		// Token: 0x0400074D RID: 1869
		internal static string ConStrAtrribs = "attribs";

		// Token: 0x0400074E RID: 1870
		internal static int IndexUserID = 0;

		// Token: 0x0400074F RID: 1871
		internal static int IndexPasswd = 1;

		// Token: 0x04000750 RID: 1872
		internal static int IndexDataSrc = 2;

		// Token: 0x04000751 RID: 1873
		internal static int IndexProxyUsr = 3;

		// Token: 0x04000752 RID: 1874
		internal static int IndexProxyPwd = 4;

		// Token: 0x04000753 RID: 1875
		internal static int IndexDBAPriv = 5;

		// Token: 0x04000754 RID: 1876
		internal static int IndexPSPE = 6;

		// Token: 0x04000755 RID: 1877
		internal static int IndexAppEdition = 7;

		// Token: 0x04000756 RID: 1878
		internal static int IndexStrAttribMax = 7;

		// Token: 0x04000757 RID: 1879
		internal static int IndexLifetime = 8;

		// Token: 0x04000758 RID: 1880
		internal static int IndexPoolInc = 9;

		// Token: 0x04000759 RID: 1881
		internal static int IndexPoolDec = 10;

		// Token: 0x0400075A RID: 1882
		internal static int IndexTimeout = 11;

		// Token: 0x0400075B RID: 1883
		internal static int IndexMaxPool = 12;

		// Token: 0x0400075C RID: 1884
		internal static int IndexMinPool = 13;

		// Token: 0x0400075D RID: 1885
		internal static int IndexPoolReg = 14;

		// Token: 0x0400075E RID: 1886
		internal static int IndexStmtCache = 15;

		// Token: 0x0400075F RID: 1887
		internal static int IndexIntAttribMax = 15;

		// Token: 0x04000760 RID: 1888
		internal static int IndexEnlist = 16;

		// Token: 0x04000761 RID: 1889
		internal static int IndexPersist = 17;

		// Token: 0x04000762 RID: 1890
		internal static int IndexPooling = 18;

		// Token: 0x04000763 RID: 1891
		internal static int IndexValidCon = 19;

		// Token: 0x04000764 RID: 1892
		internal static int IndexMetaPool = 20;

		// Token: 0x04000765 RID: 1893
		internal static int IndexStmtCachePurge = 21;

		// Token: 0x04000766 RID: 1894
		internal static int IndexGridCR = 22;

		// Token: 0x04000767 RID: 1895
		internal static int IndexGridRLB = 23;

		// Token: 0x04000768 RID: 1896
		internal static int IndexCtxConn = 24;

		// Token: 0x04000769 RID: 1897
		internal static int IndexSelfTuning = 25;

		// Token: 0x0400076A RID: 1898
		internal static int IndexBoolAttribMax = 25;

		// Token: 0x0400076B RID: 1899
		internal static int IndexInternalConStr = 26;

		// Token: 0x0400076C RID: 1900
		internal static int IndexConStrHashCode = 27;

		// Token: 0x0400076D RID: 1901
		private OraclePermission m_orclPermission;

		// Token: 0x0400076E RID: 1902
		internal string m_databaseName;

		// Token: 0x0400076F RID: 1903
		internal string m_databaseDomainName;

		// Token: 0x04000770 RID: 1904
		internal string m_serviceName;

		// Token: 0x04000771 RID: 1905
		internal string m_instanceName;

		// Token: 0x04000772 RID: 1906
		internal string m_hostName;

		// Token: 0x04000773 RID: 1907
		internal ArrayList m_DataReaderList;

		// Token: 0x04000774 RID: 1908
		internal static bool s_bIsOdtConnection = false;

		// Token: 0x04000775 RID: 1909
		internal object m_tuningLock = new object();

		// Token: 0x020000E7 RID: 231
		internal class ExtProcEnv
		{
			// Token: 0x04000776 RID: 1910
			internal bool m_status = true;
		}

		// Token: 0x020000E8 RID: 232
		private class ThreadData
		{
			// Token: 0x060008B5 RID: 2229 RVA: 0x00057E86 File Offset: 0x00056E86
			internal ThreadData(IntPtr ociExtProcContext)
			{
				this.m_ociExtProcContext = ociExtProcContext;
				this.m_extProcEnv = new OracleConnection.ExtProcEnv();
			}

			// Token: 0x04000777 RID: 1911
			internal OracleConnection m_externalExtprocConn;

			// Token: 0x04000778 RID: 1912
			internal OracleConnection m_internalExtprocConn;

			// Token: 0x04000779 RID: 1913
			internal IntPtr m_ociExtProcContext;

			// Token: 0x0400077A RID: 1914
			internal OracleConnection.ExtProcEnv m_extProcEnv;
		}

		// Token: 0x020000E9 RID: 233
		private class PSPEPrimaryConnectionInfo
		{
			// Token: 0x060008B6 RID: 2230 RVA: 0x00057EA0 File Offset: 0x00056EA0
			internal PSPEPrimaryConnectionInfo(bool bSupportPromotion, int pspeAttrVal)
			{
				this.m_dbSupportPromotion = bSupportPromotion;
				this.m_pspeAttributeValue = pspeAttrVal;
			}

			// Token: 0x0400077B RID: 1915
			internal bool m_dbSupportPromotion;

			// Token: 0x0400077C RID: 1916
			internal int m_pspeAttributeValue;
		}
	}
}
