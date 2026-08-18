using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x0200020E RID: 526
	internal sealed class TdsParser
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x000DE5DC File Offset: 0x000DD9DC
		private static Task CompletedTask
		{
			get
			{
				if (TdsParser.completedTask == null)
				{
					TdsParser.completedTask = Task.FromResult<object>(null);
				}
				return TdsParser.completedTask;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x000DE600 File Offset: 0x000DDA00
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x000DE614 File Offset: 0x000DDA14
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x000DE628 File Offset: 0x000DDA28
		internal bool IsColumnEncryptionSupported
		{
			get
			{
				return this._serverSupportsColumnEncryption;
			}
			set
			{
				this._serverSupportsColumnEncryption = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x000DE63C File Offset: 0x000DDA3C
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x000DE650 File Offset: 0x000DDA50
		internal byte TceVersionSupported { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x000DE664 File Offset: 0x000DDA64
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x000DE678 File Offset: 0x000DDA78
		internal string EnclaveType { get; set; }

		// Token: 0x06002104 RID: 8452 RVA: 0x000DE68C File Offset: 0x000DDA8C
		internal TdsParser(bool MARS, bool fAsynchronous)
		{
			this._fMARS = MARS;
			this._physicalStateObj = new TdsParserStateObject(this);
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x000DE6D8 File Offset: 0x000DDAD8
		internal SqlInternalConnectionTds Connection
		{
			get
			{
				return this._connHandler;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x000DE6EC File Offset: 0x000DDAEC
		// (set) Token: 0x06002107 RID: 8455 RVA: 0x000DE700 File Offset: 0x000DDB00
		internal SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._currentTransaction;
			}
			set
			{
				if ((this._currentTransaction == null && value != null) || (this._currentTransaction != null && value == null))
				{
					this._currentTransaction = value;
				}
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x000DE72C File Offset: 0x000DDB2C
		internal int DefaultLCID
		{
			get
			{
				return this._defaultLCID;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x000DE740 File Offset: 0x000DDB40
		// (set) Token: 0x0600210A RID: 8458 RVA: 0x000DE754 File Offset: 0x000DDB54
		internal EncryptionOptions EncryptionOptions
		{
			get
			{
				return this._encryptionOption;
			}
			set
			{
				this._encryptionOption = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x000DE768 File Offset: 0x000DDB68
		internal bool IsYukonOrNewer
		{
			get
			{
				return this._isYukon;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000DE77C File Offset: 0x000DDB7C
		internal bool IsKatmaiOrNewer
		{
			get
			{
				return this._isKatmai;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x000DE790 File Offset: 0x000DDB90
		internal bool MARSOn
		{
			get
			{
				return this._fMARS;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x000DE7A4 File Offset: 0x000DDBA4
		// (set) Token: 0x0600210F RID: 8463 RVA: 0x000DE7B8 File Offset: 0x000DDBB8
		internal SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this._pendingTransaction;
			}
			set
			{
				this._pendingTransaction = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x000DE7CC File Offset: 0x000DDBCC
		internal string Server
		{
			get
			{
				return this._server;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000DE7E0 File Offset: 0x000DDBE0
		// (set) Token: 0x06002112 RID: 8466 RVA: 0x000DE7F4 File Offset: 0x000DDBF4
		internal TdsParserState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x000DE808 File Offset: 0x000DDC08
		// (set) Token: 0x06002114 RID: 8468 RVA: 0x000DE81C File Offset: 0x000DDC1C
		internal SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
			set
			{
				this._statistics = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000DE830 File Offset: 0x000DDC30
		private bool IncludeTraceHeader
		{
			get
			{
				return this._isDenali && Bid.TraceOn && Bid.IsOn(Bid.ApiGroup.Correlation);
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000DE858 File Offset: 0x000DDC58
		internal int IncrementNonTransactedOpenResultCount()
		{
			return Interlocked.Increment(ref this._nonTransactedOpenResultCount);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000DE874 File Offset: 0x000DDC74
		internal void DecrementNonTransactedOpenResultCount()
		{
			Interlocked.Decrement(ref this._nonTransactedOpenResultCount);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000DE890 File Offset: 0x000DDC90
		internal void ProcessPendingAck(TdsParserStateObject stateObj)
		{
			if (stateObj._attentionSent)
			{
				this.ProcessAttention(stateObj);
			}
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000DE8B0 File Offset: 0x000DDCB0
		internal void Connect(ServerInfo serverInfo, SqlInternalConnectionTds connHandler, bool ignoreSniOpenTimeout, long timerExpire, bool encrypt, bool trustServerCert, bool integratedSecurity, bool withFailover, bool isFirstTransparentAttempt, SqlAuthenticationMethod authType, bool disableTnir, SqlAuthenticationProviderManager sqlAuthProviderManager)
		{
			if (this._state != TdsParserState.Closed)
			{
				return;
			}
			this._connHandler = connHandler;
			this._loginWithFailover = withFailover;
			uint snistatus = SNILoadHandle.SingletonInstance.SNIStatus;
			if (snistatus != 0U)
			{
				this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
				this._physicalStateObj.Dispose();
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			if (connHandler.ConnectionOptions.LocalDBInstance != null)
			{
				LocalDBAPI.CreateLocalDBInstance(connHandler.ConnectionOptions.LocalDBInstance);
			}
			if (integratedSecurity || authType == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
			{
				this.LoadSSPILibrary();
				this._sniSpnBuffer = new byte[SNINativeMethodWrapper.SniMaxComposedSpnLength];
				Bid.Trace("<sc.TdsParser.Connect|SEC> SSPI or Active Directory Authentication Library for SQL Server based integrated authentication\n");
			}
			else
			{
				this._sniSpnBuffer = null;
				if (authType == SqlAuthenticationMethod.ActiveDirectoryPassword)
				{
					Bid.Trace("<sc.TdsParser.Connect|SEC> Active Directory Password authentication\n");
				}
				else if (authType == SqlAuthenticationMethod.SqlPassword)
				{
					Bid.Trace("<sc.TdsParser.Connect|SEC> SQL Password authentication\n");
				}
				else if (authType == SqlAuthenticationMethod.ActiveDirectoryInteractive)
				{
					Bid.Trace("<sc.TdsParser.Connect|SEC> Active Directory Interactive authentication\n");
				}
				else
				{
					Bid.Trace("<sc.TdsParser.Connect|SEC> SQL authentication\n");
				}
			}
			byte[] instanceName = null;
			this._connHandler.TimeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			this._connHandler.TimeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.InitializeConnection);
			bool multiSubnetFailover = this._connHandler.ConnectionOptions.MultiSubnetFailover;
			TransparentNetworkResolutionState transparentNetworkResolutionState;
			if (this._connHandler.ConnectionOptions.TransparentNetworkIPResolution && !disableTnir)
			{
				if (isFirstTransparentAttempt)
				{
					transparentNetworkResolutionState = TransparentNetworkResolutionState.SequentialMode;
				}
				else
				{
					transparentNetworkResolutionState = TransparentNetworkResolutionState.ParallelMode;
				}
			}
			else
			{
				transparentNetworkResolutionState = TransparentNetworkResolutionState.DisabledMode;
			}
			int connectTimeout = this._connHandler.ConnectionOptions.ConnectTimeout;
			this._physicalStateObj.CreatePhysicalSNIHandle(serverInfo.ExtendedServerName, ignoreSniOpenTimeout, timerExpire, out instanceName, this._sniSpnBuffer, false, true, multiSubnetFailover, transparentNetworkResolutionState, connectTimeout);
			if (this._physicalStateObj.Status != 0U)
			{
				this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
				this._physicalStateObj.Dispose();
				Bid.Trace("<sc.TdsParser.Connect|ERR|SEC> Login failure\n");
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			this._server = serverInfo.ResolvedServerName;
			if (connHandler.PoolGroupProviderInfo != null)
			{
				connHandler.PoolGroupProviderInfo.AliasCheck((serverInfo.PreRoutingServerName == null) ? serverInfo.ResolvedServerName : serverInfo.PreRoutingServerName);
			}
			this._state = TdsParserState.OpenNotLoggedIn;
			this._physicalStateObj.SniContext = SniContext.Snix_PreLoginBeforeSuccessfullWrite;
			this._physicalStateObj.TimeoutTime = timerExpire;
			bool flag = false;
			this._connHandler.TimeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.InitializeConnection);
			this._connHandler.TimeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake);
			uint num = SNINativeMethodWrapper.SniGetConnectionId(this._physicalStateObj.Handle, ref this._connHandler._clientConnectionId);
			Bid.Trace("<sc.TdsParser.Connect|SEC> Sending prelogin handshake\n");
			this.SendPreLoginHandshake(instanceName, encrypt);
			this._connHandler.TimeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake);
			this._connHandler.TimeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake);
			this._physicalStateObj.SniContext = SniContext.Snix_PreLogin;
			Bid.Trace("<sc.TdsParser.Connect|SEC> Consuming prelogin handshake\n");
			PreLoginHandshakeStatus preLoginHandshakeStatus = this.ConsumePreLoginHandshake(authType, encrypt, trustServerCert, integratedSecurity, out flag, out this._connHandler._fedAuthRequired);
			if (preLoginHandshakeStatus == PreLoginHandshakeStatus.InstanceFailure)
			{
				Bid.Trace("<sc.TdsParser.Connect|SEC> Prelogin handshake unsuccessful. Reattempting prelogin handshake\n");
				this._physicalStateObj.Dispose();
				this._physicalStateObj.SniContext = SniContext.Snix_Connect;
				this._physicalStateObj.CreatePhysicalSNIHandle(serverInfo.ExtendedServerName, ignoreSniOpenTimeout, timerExpire, out instanceName, this._sniSpnBuffer, true, true, multiSubnetFailover, transparentNetworkResolutionState, connectTimeout);
				if (this._physicalStateObj.Status != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					Bid.Trace("<sc.TdsParser.Connect|ERR|SEC> Login failure\n");
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				uint num2 = SNINativeMethodWrapper.SniGetConnectionId(this._physicalStateObj.Handle, ref this._connHandler._clientConnectionId);
				Bid.Trace("<sc.TdsParser.Connect|SEC> Sending prelogin handshake\n");
				this.SendPreLoginHandshake(instanceName, encrypt);
				preLoginHandshakeStatus = this.ConsumePreLoginHandshake(authType, encrypt, trustServerCert, integratedSecurity, out flag, out this._connHandler._fedAuthRequired);
				if (preLoginHandshakeStatus == PreLoginHandshakeStatus.InstanceFailure)
				{
					Bid.Trace("<sc.TdsParser.Connect|ERR|SEC> Prelogin handshake unsuccessful. Login failure\n");
					throw SQL.InstanceFailure();
				}
			}
			Bid.Trace("<sc.TdsParser.Connect|SEC> Prelogin handshake successful\n");
			if (this._fMARS && flag)
			{
				this._sessionPool = new TdsParserSessionPool(this);
			}
			else
			{
				this._fMARS = false;
			}
			if (authType == SqlAuthenticationMethod.ActiveDirectoryPassword || (authType == SqlAuthenticationMethod.ActiveDirectoryIntegrated && this._connHandler._fedAuthRequired))
			{
				SqlAuthenticationProvider provider = sqlAuthProviderManager.GetProvider(authType);
				if (provider != null && provider.GetType() == typeof(ActiveDirectoryNativeAuthenticationProvider))
				{
					this.LoadADALLibrary();
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.TdsParser.Connect|SEC> Active directory authentication.Loaded Active Directory Authentication Library for SQL Server\n");
					}
				}
			}
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000DECD8 File Offset: 0x000DE0D8
		internal void RemoveEncryption()
		{
			uint num = SNINativeMethodWrapper.SNIRemoveProvider(this._physicalStateObj.Handle, SNINativeMethodWrapper.ProviderEnum.SSL_PROV);
			if (num != 0U)
			{
				this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			try
			{
			}
			finally
			{
				this._physicalStateObj.ClearAllWritePackets();
			}
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000DED4C File Offset: 0x000DE14C
		internal void EnableMars()
		{
			if (this._fMARS)
			{
				this._pMarsPhysicalConObj = this._physicalStateObj;
				uint num = 0U;
				uint num2 = 0U;
				num = SNINativeMethodWrapper.SNIAddProvider(this._pMarsPhysicalConObj.Handle, SNINativeMethodWrapper.ProviderEnum.SMUX_PROV, ref num2);
				if (num != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				IntPtr zero = IntPtr.Zero;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this._pMarsPhysicalConObj.IncrementPendingCallbacks();
					num = SNINativeMethodWrapper.SNIReadAsync(this._pMarsPhysicalConObj.Handle, ref zero);
					if (zero != IntPtr.Zero)
					{
						SNINativeMethodWrapper.SNIPacketRelease(zero);
					}
				}
				if (997U != num)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				this._physicalStateObj = this.CreateSession();
			}
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000DEE48 File Offset: 0x000DE248
		internal TdsParserStateObject CreateSession()
		{
			TdsParserStateObject tdsParserStateObject = new TdsParserStateObject(this, this._pMarsPhysicalConObj.Handle, true);
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParser.CreateSession|ADV> %d# created session %d\n", this.ObjectID, tdsParserStateObject.ObjectID);
			}
			return tdsParserStateObject;
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000DEE88 File Offset: 0x000DE288
		internal TdsParserStateObject GetSession(object owner)
		{
			TdsParserStateObject tdsParserStateObject;
			if (this.MARSOn)
			{
				tdsParserStateObject = this._sessionPool.GetSession(owner);
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.TdsParser.GetSession|ADV> %d# getting session %d from pool\n", this.ObjectID, tdsParserStateObject.ObjectID);
				}
			}
			else
			{
				tdsParserStateObject = this._physicalStateObj;
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.TdsParser.GetSession|ADV> %d# getting physical session %d\n", this.ObjectID, tdsParserStateObject.ObjectID);
				}
			}
			return tdsParserStateObject;
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000DEEF0 File Offset: 0x000DE2F0
		internal void PutSession(TdsParserStateObject session)
		{
			if (this.MARSOn)
			{
				this._sessionPool.PutSession(session);
				return;
			}
			if (this._state == TdsParserState.Closed || this._state == TdsParserState.Broken)
			{
				this._physicalStateObj.SniContext = SniContext.Snix_Close;
				this._physicalStateObj.Dispose();
				return;
			}
			this._physicalStateObj.Owner = null;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000DEF48 File Offset: 0x000DE348
		internal void BestEffortCleanup()
		{
			this._state = TdsParserState.Broken;
			TdsParserStateObject physicalStateObj = this._physicalStateObj;
			if (physicalStateObj != null)
			{
				SNIHandle handle = physicalStateObj.Handle;
				if (handle != null)
				{
					handle.Dispose();
				}
			}
			if (this._fMARS)
			{
				TdsParserSessionPool sessionPool = this._sessionPool;
				if (sessionPool != null)
				{
					sessionPool.BestEffortCleanup();
				}
				TdsParserStateObject pMarsPhysicalConObj = this._pMarsPhysicalConObj;
				if (pMarsPhysicalConObj != null)
				{
					SNIHandle handle2 = pMarsPhysicalConObj.Handle;
					if (handle2 != null)
					{
						handle2.Dispose();
					}
				}
			}
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000DEFAC File Offset: 0x000DE3AC
		private void SendPreLoginHandshake(byte[] instanceName, bool encrypt)
		{
			this._physicalStateObj._outputMessageType = 18;
			int num = 36;
			byte[] array = new byte[1059];
			int num2 = 0;
			for (int i = 0; i < 7; i++)
			{
				int num3 = 0;
				this._physicalStateObj.WriteByte((byte)i);
				this._physicalStateObj.WriteByte((byte)((num & 65280) >> 8));
				this._physicalStateObj.WriteByte((byte)(num & 255));
				switch (i)
				{
				case 0:
				{
					Version assemblyVersion = ADP.GetAssemblyVersion();
					array[num2++] = (byte)(assemblyVersion.Major & 255);
					array[num2++] = (byte)(assemblyVersion.Minor & 255);
					array[num2++] = (byte)((assemblyVersion.Build & 65280) >> 8);
					array[num2++] = (byte)(assemblyVersion.Build & 255);
					array[num2++] = (byte)(assemblyVersion.Revision & 255);
					array[num2++] = (byte)((assemblyVersion.Revision & 65280) >> 8);
					num += 6;
					num3 = 6;
					break;
				}
				case 1:
					if (this._encryptionOption == EncryptionOptions.NOT_SUP)
					{
						array[num2] = 2;
					}
					else if (encrypt)
					{
						array[num2] = 1;
						this._encryptionOption = EncryptionOptions.ON;
					}
					else
					{
						array[num2] = 0;
						this._encryptionOption = EncryptionOptions.OFF;
					}
					num2++;
					num++;
					num3 = 1;
					break;
				case 2:
				{
					int num4 = 0;
					while (instanceName[num4] != 0)
					{
						array[num2] = instanceName[num4];
						num2++;
						num4++;
					}
					array[num2] = 0;
					num2++;
					num4++;
					num += num4;
					num3 = num4;
					break;
				}
				case 3:
				{
					int currentThreadIdForTdsLoginOnly = TdsParserStaticMethods.GetCurrentThreadIdForTdsLoginOnly();
					array[num2++] = (byte)(((ulong)-16777216 & (ulong)((long)currentThreadIdForTdsLoginOnly)) >> 24);
					array[num2++] = (byte)((16711680 & currentThreadIdForTdsLoginOnly) >> 16);
					array[num2++] = (byte)((65280 & currentThreadIdForTdsLoginOnly) >> 8);
					array[num2++] = (byte)(255 & currentThreadIdForTdsLoginOnly);
					num += 4;
					num3 = 4;
					break;
				}
				case 4:
					array[num2++] = (this._fMARS ? 1 : 0);
					num++;
					num3++;
					break;
				case 5:
				{
					byte[] src = this._connHandler._clientConnectionId.ToByteArray();
					Buffer.BlockCopy(src, 0, array, num2, 16);
					num2 += 16;
					num += 16;
					num3 = 16;
					ActivityCorrelator.ActivityId activityId = ActivityCorrelator.Next();
					src = activityId.Id.ToByteArray();
					Buffer.BlockCopy(src, 0, array, num2, 16);
					num2 += 16;
					array[num2++] = (byte)(255U & activityId.Sequence);
					array[num2++] = (byte)((65280U & activityId.Sequence) >> 8);
					array[num2++] = (byte)((16711680U & activityId.Sequence) >> 16);
					array[num2++] = (byte)((4278190080U & activityId.Sequence) >> 24);
					int num5 = 20;
					num += num5;
					num3 += num5;
					Bid.Trace("<sc.TdsParser.SendPreLoginHandshake|INFO> ClientConnectionID %ls, ActivityID %ls\n", this._connHandler._clientConnectionId.ToString(), activityId.ToString());
					break;
				}
				case 6:
					array[num2++] = 1;
					num++;
					num3++;
					break;
				}
				this._physicalStateObj.WriteByte((byte)((num3 & 65280) >> 8));
				this._physicalStateObj.WriteByte((byte)(num3 & 255));
			}
			this._physicalStateObj.WriteByte(byte.MaxValue);
			this._physicalStateObj.WriteByteArray(array, num2, 0, true, null);
			this._physicalStateObj.WritePacket(1, false);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000DF31C File Offset: 0x000DE71C
		private PreLoginHandshakeStatus ConsumePreLoginHandshake(SqlAuthenticationMethod authType, bool encrypt, bool trustServerCert, bool integratedSecurity, out bool marsCapable, out bool fedAuthRequired)
		{
			marsCapable = this._fMARS;
			fedAuthRequired = false;
			bool flag = false;
			if (!this._physicalStateObj.TryReadNetworkPacket())
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			if (this._physicalStateObj._inBytesRead == 0)
			{
				this._physicalStateObj.AddError(new SqlError(0, 0, 20, this._server, SQLMessage.PreloginError(), "", 0));
				this._physicalStateObj.Dispose();
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			if (!this._physicalStateObj.TryProcessHeader())
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			if (this._physicalStateObj._inBytesPacket > 32768 || this._physicalStateObj._inBytesPacket <= 0)
			{
				throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
			}
			byte[] array = new byte[this._physicalStateObj._inBytesPacket];
			if (!this._physicalStateObj.TryReadByteArray(array, 0, array.Length))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			if (array[0] == 170)
			{
				throw SQL.InvalidSQLServerVersionUnknown();
			}
			int num = 0;
			int num2 = (int)array[num++];
			bool flag2 = false;
			while (num2 != 255)
			{
				switch (num2)
				{
				case 0:
				{
					int num3 = (int)array[num++] << 8 | (int)array[num++];
					int num4 = (int)array[num++] << 8 | (int)array[num++];
					byte b = array[num3];
					byte b2 = array[num3 + 1];
					int num5 = (int)array[num3 + 2] << 8 | (int)array[num3 + 3];
					flag = (b >= 9);
					if (!flag)
					{
						marsCapable = false;
					}
					break;
				}
				case 1:
				{
					int num3 = (int)array[num++] << 8 | (int)array[num++];
					int num4 = (int)array[num++] << 8 | (int)array[num++];
					EncryptionOptions encryptionOptions = (EncryptionOptions)array[num3];
					flag2 = (encryptionOptions != EncryptionOptions.NOT_SUP);
					EncryptionOptions encryptionOption = this._encryptionOption;
					if (encryptionOption != EncryptionOptions.OFF)
					{
						if (encryptionOption != EncryptionOptions.NOT_SUP)
						{
							if (encryptionOptions == EncryptionOptions.NOT_SUP)
							{
								this._physicalStateObj.AddError(new SqlError(20, 0, 20, this._server, SQLMessage.EncryptionNotSupportedByServer(), "", 0));
								this._physicalStateObj.Dispose();
								this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
							}
						}
						else if (encryptionOptions == EncryptionOptions.REQ)
						{
							this._physicalStateObj.AddError(new SqlError(20, 0, 20, this._server, SQLMessage.EncryptionNotSupportedByClient(), "", 0));
							this._physicalStateObj.Dispose();
							this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
						}
					}
					else if (encryptionOptions == EncryptionOptions.OFF)
					{
						this._encryptionOption = EncryptionOptions.LOGIN;
					}
					else if (encryptionOptions == EncryptionOptions.REQ)
					{
						this._encryptionOption = EncryptionOptions.ON;
					}
					break;
				}
				case 2:
				{
					int num3 = (int)array[num++] << 8 | (int)array[num++];
					int num4 = (int)array[num++] << 8 | (int)array[num++];
					byte b3 = 1;
					byte b4 = array[num3];
					if (b4 == b3)
					{
						return PreLoginHandshakeStatus.InstanceFailure;
					}
					break;
				}
				case 3:
					num += 4;
					break;
				case 4:
				{
					int num3 = (int)array[num++] << 8 | (int)array[num++];
					int num4 = (int)array[num++] << 8 | (int)array[num++];
					marsCapable = (array[num3] != 0);
					break;
				}
				case 5:
					num += 4;
					break;
				case 6:
				{
					int num3 = (int)array[num++] << 8 | (int)array[num++];
					int num4 = (int)array[num++] << 8 | (int)array[num++];
					if (array[num3] != 0 && array[num3] != 1)
					{
						Bid.Trace("<sc.TdsParser.ConsumePreLoginHandshake|ERR> %d#, Server sent an unexpected value for FedAuthRequired PreLogin Option. Value was %d.\n", this.ObjectID, (int)array[num3]);
						throw SQL.ParsingErrorValue(ParsingErrorState.FedAuthRequiredPreLoginResponseInvalidValue, (int)array[num3]);
					}
					if ((this._connHandler.ConnectionOptions != null && this._connHandler.ConnectionOptions.Authentication != SqlAuthenticationMethod.NotSpecified) || this._connHandler._accessTokenInBytes != null)
					{
						fedAuthRequired = (array[num3] == 1);
					}
					break;
				}
				default:
					num += 4;
					break;
				}
				if (num >= array.Length)
				{
					break;
				}
				num2 = (int)array[num++];
			}
			if (this._encryptionOption == EncryptionOptions.ON || this._encryptionOption == EncryptionOptions.LOGIN)
			{
				if (!flag2)
				{
					this._physicalStateObj.AddError(new SqlError(20, 0, 20, this._server, SQLMessage.EncryptionNotSupportedByServer(), "", 0));
					this._physicalStateObj.Dispose();
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				uint num6 = (((encrypt && !trustServerCert) || ((authType != SqlAuthenticationMethod.NotSpecified || this._connHandler._accessTokenInBytes != null) && !trustServerCert)) ? 1U : 0U) | (flag ? 2U : 0U);
				if (encrypt && !integratedSecurity)
				{
					num6 |= 16U;
				}
				uint num7 = SNINativeMethodWrapper.SNIAddProvider(this._physicalStateObj.Handle, SNINativeMethodWrapper.ProviderEnum.SSL_PROV, ref num6);
				if (num7 != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				num7 = SNINativeMethodWrapper.SNIWaitForSSLHandshakeToComplete(this._physicalStateObj.Handle, this._physicalStateObj.GetTimeoutRemaining());
				if (num7 != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				try
				{
				}
				finally
				{
					this._physicalStateObj.ClearAllWritePackets();
				}
			}
			return PreLoginHandshakeStatus.Successful;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000DF824 File Offset: 0x000DEC24
		internal void Deactivate(bool connectionIsDoomed)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParser.Deactivate|ADV> %d# deactivating\n", this.ObjectID);
			}
			if (Bid.IsOn(Bid.ApiGroup.StateDump))
			{
				Bid.Trace("<sc.TdsParser.Deactivate|STATE> %d#, %ls\n", this.ObjectID, this.TraceString());
			}
			if (this.MARSOn)
			{
				this._sessionPool.Deactivate();
			}
			if (!connectionIsDoomed && this._physicalStateObj != null)
			{
				if (this._physicalStateObj._pendingData)
				{
					this.DrainData(this._physicalStateObj);
				}
				if (this._physicalStateObj.HasOpenResult)
				{
					this._physicalStateObj.DecrementOpenResultCount();
				}
			}
			SqlInternalTransaction currentTransaction = this.CurrentTransaction;
			if (currentTransaction != null && currentTransaction.HasParentTransaction)
			{
				currentTransaction.CloseFromConnection();
			}
			this.Statistics = null;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000DF8D8 File Offset: 0x000DECD8
		internal void Disconnect()
		{
			if (this._sessionPool != null)
			{
				this._sessionPool.Dispose();
			}
			if (this._state != TdsParserState.Closed)
			{
				this._state = TdsParserState.Closed;
				try
				{
					if (!this._physicalStateObj.HasOwner)
					{
						this._physicalStateObj.SniContext = SniContext.Snix_Close;
						this._physicalStateObj.Dispose();
					}
					else
					{
						this._physicalStateObj.DecrementPendingCallbacks(false);
					}
					if (this._pMarsPhysicalConObj != null)
					{
						this._pMarsPhysicalConObj.Dispose();
					}
				}
				finally
				{
					this._pMarsPhysicalConObj = null;
				}
			}
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000DF974 File Offset: 0x000DED74
		private void FireInfoMessageEvent(SqlConnection connection, TdsParserStateObject stateObj, SqlError error)
		{
			string serverVersion = null;
			if (this._state == TdsParserState.OpenLoggedIn)
			{
				serverVersion = this._connHandler.ServerVersion;
			}
			SqlException exception = SqlException.CreateException(new SqlErrorCollection
			{
				error
			}, serverVersion, this._connHandler, null);
			bool flag;
			connection.OnInfoMessage(new SqlInfoMessageEventArgs(exception), out flag);
			if (flag)
			{
				stateObj._syncOverAsync = true;
			}
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000DF9CC File Offset: 0x000DEDCC
		internal void DisconnectTransaction(SqlInternalTransaction internalTransaction)
		{
			if (this._currentTransaction != null && this._currentTransaction == internalTransaction)
			{
				this._currentTransaction = null;
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000DF9F4 File Offset: 0x000DEDF4
		internal void RollbackOrphanedAPITransactions()
		{
			SqlInternalTransaction currentTransaction = this.CurrentTransaction;
			if (currentTransaction != null && currentTransaction.HasParentTransaction && currentTransaction.IsOrphaned)
			{
				currentTransaction.CloseFromConnection();
			}
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000DFA24 File Offset: 0x000DEE24
		internal void ThrowExceptionAndWarning(TdsParserStateObject stateObj, bool callerHasConnectionLock = false, bool asyncClose = false)
		{
			SqlException ex = null;
			bool flag;
			SqlErrorCollection fullErrorAndWarningCollection = stateObj.GetFullErrorAndWarningCollection(out flag);
			if (fullErrorAndWarningCollection.Count == 0)
			{
				Bid.Trace("<sc.TdsParser.ThrowExceptionAndWarning|ERR> Potential multi-threaded misuse of connection, unexpectedly empty warnings/errors under lock %d#\n", this.ObjectID);
			}
			flag &= (this._state > TdsParserState.Closed);
			if (flag)
			{
				if (this._state == TdsParserState.OpenNotLoggedIn && (this._connHandler.ConnectionOptions.TransparentNetworkIPResolution || this._connHandler.ConnectionOptions.MultiSubnetFailover || this._loginWithFailover) && fullErrorAndWarningCollection.Count == 1 && (fullErrorAndWarningCollection[0].Number == -2 || (long)fullErrorAndWarningCollection[0].Number == 258L))
				{
					flag = false;
					this.Disconnect();
				}
				else
				{
					this._state = TdsParserState.Broken;
				}
			}
			if (fullErrorAndWarningCollection != null && fullErrorAndWarningCollection.Count > 0)
			{
				string serverVersion = null;
				if (this._state == TdsParserState.OpenLoggedIn)
				{
					serverVersion = this._connHandler.ServerVersion;
				}
				ex = SqlException.CreateException(fullErrorAndWarningCollection, serverVersion, this._connHandler, null);
				if (ex.Procedure == "InitADALPackage" || ex.Procedure == "InitSSPIPackage")
				{
					ex._doNotReconnect = true;
				}
			}
			if (ex != null)
			{
				if (flag)
				{
					TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
					if (networkPacketTaskSource != null)
					{
						networkPacketTaskSource.TrySetException(ADP.ExceptionWithStackTrace(ex));
					}
				}
				if (asyncClose)
				{
					SqlInternalConnectionTds connHandler = this._connHandler;
					Action<Action> wrapCloseInAction = delegate(Action closeAction)
					{
						Task.Factory.StartNew(delegate()
						{
							connHandler._parserLock.Wait(false);
							connHandler.ThreadHasParserLockForClose = true;
							try
							{
								closeAction();
							}
							finally
							{
								connHandler.ThreadHasParserLockForClose = false;
								connHandler._parserLock.Release();
							}
						});
					};
					this._connHandler.OnError(ex, flag, wrapCloseInAction);
					return;
				}
				bool threadHasParserLockForClose = this._connHandler.ThreadHasParserLockForClose;
				if (callerHasConnectionLock)
				{
					this._connHandler.ThreadHasParserLockForClose = true;
				}
				try
				{
					this._connHandler.OnError(ex, flag, null);
				}
				finally
				{
					if (callerHasConnectionLock)
					{
						this._connHandler.ThreadHasParserLockForClose = threadHasParserLockForClose;
					}
				}
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000DFBEC File Offset: 0x000DEFEC
		internal SqlError ProcessSNIError(TdsParserStateObject stateObj)
		{
			SNINativeMethodWrapper.SNI_Error sni_Error = new SNINativeMethodWrapper.SNI_Error();
			SNINativeMethodWrapper.SNIGetLastError(sni_Error);
			if (sni_Error.sniError != 0U)
			{
				switch (sni_Error.sniError)
				{
				case 47U:
					throw SQL.MultiSubnetFailoverWithMoreThan64IPs();
				case 48U:
					throw SQL.MultiSubnetFailoverWithInstanceSpecified();
				case 49U:
					throw SQL.MultiSubnetFailoverWithNonTcpProtocol();
				}
			}
			int num = Array.IndexOf<char>(sni_Error.errorMessage, '\0');
			string text;
			if (num == -1)
			{
				text = string.Empty;
			}
			else
			{
				text = new string(sni_Error.errorMessage, 0, num);
			}
			string @string = Res.GetString(Enum.GetName(typeof(SniContext), stateObj.SniContext));
			string name = string.Format(null, "SNI_PN{0}", new object[]
			{
				(int)sni_Error.provider
			});
			string string2 = Res.GetString(name);
			uint win32ErrorCode = sni_Error.nativeError;
			if (sni_Error.sniError == 0U)
			{
				int num2 = text.IndexOf(':');
				if (0 <= num2)
				{
					int num3 = text.Length;
					num3 -= 2;
					num2 += 2;
					num3 -= num2;
					if (num3 > 0)
					{
						text = text.Substring(num2, num3);
					}
				}
			}
			else
			{
				text = SQL.GetSNIErrorMessage((int)sni_Error.sniError);
				if (sni_Error.sniError == 50U)
				{
					text += LocalDBAPI.GetLocalDBMessage((int)sni_Error.nativeError);
					win32ErrorCode = 0U;
				}
			}
			text = string.Format(null, "{0} (provider: {1}, error: {2} - {3})", new object[]
			{
				@string,
				string2,
				(int)sni_Error.sniError,
				text
			});
			return new SqlError((int)sni_Error.nativeError, 0, 20, this._server, text, sni_Error.function, (int)sni_Error.lineNumber, win32ErrorCode);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000DFD70 File Offset: 0x000DF170
		internal void CheckResetConnection(TdsParserStateObject stateObj)
		{
			if (this._fResetConnection && !stateObj._fResetConnectionSent)
			{
				try
				{
					if (this._fMARS && !stateObj._fResetEventOwned)
					{
						stateObj._fResetEventOwned = this._resetConnectionEvent.WaitOne(stateObj.GetTimeoutRemaining(), false);
						if (stateObj._fResetEventOwned && stateObj.TimeoutHasExpired)
						{
							stateObj._fResetEventOwned = !this._resetConnectionEvent.Set();
							stateObj.TimeoutTime = 0L;
						}
						if (!stateObj._fResetEventOwned)
						{
							stateObj.ResetBuffer();
							stateObj.AddError(new SqlError(-2, 0, 11, this._server, this._connHandler.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U));
							this.ThrowExceptionAndWarning(stateObj, true, false);
						}
					}
					if (this._fResetConnection)
					{
						if (this._fPreserveTransaction)
						{
							stateObj._outBuff[1] = (stateObj._outBuff[1] | 16);
						}
						else
						{
							stateObj._outBuff[1] = (stateObj._outBuff[1] | 8);
						}
						if (!this._fMARS)
						{
							this._fResetConnection = false;
							this._fPreserveTransaction = false;
						}
						else
						{
							stateObj._fResetConnectionSent = true;
						}
					}
					else if (this._fMARS && stateObj._fResetEventOwned)
					{
						stateObj._fResetEventOwned = !this._resetConnectionEvent.Set();
					}
				}
				catch (Exception)
				{
					if (this._fMARS && stateObj._fResetEventOwned)
					{
						stateObj._fResetConnectionSent = false;
						stateObj._fResetEventOwned = !this._resetConnectionEvent.Set();
					}
					throw;
				}
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000DFF24 File Offset: 0x000DF324
		internal byte[] SerializeShort(int v, TdsParserStateObject stateObj)
		{
			if (stateObj._bShortBytes == null)
			{
				stateObj._bShortBytes = new byte[2];
			}
			byte[] bShortBytes = stateObj._bShortBytes;
			int num = 0;
			bShortBytes[num++] = (byte)(v & 255);
			bShortBytes[num++] = (byte)(v >> 8 & 255);
			return bShortBytes;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000DFF70 File Offset: 0x000DF370
		internal void WriteShort(int v, TdsParserStateObject stateObj)
		{
			if (stateObj._outBytesUsed + 2 > stateObj._outBuff.Length)
			{
				stateObj.WriteByte((byte)(v & 255));
				stateObj.WriteByte((byte)(v >> 8 & 255));
				return;
			}
			stateObj._outBuff[stateObj._outBytesUsed] = (byte)(v & 255);
			stateObj._outBuff[stateObj._outBytesUsed + 1] = (byte)(v >> 8 & 255);
			stateObj._outBytesUsed += 2;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000DFFEC File Offset: 0x000DF3EC
		internal void WriteUnsignedShort(ushort us, TdsParserStateObject stateObj)
		{
			this.WriteShort((int)((short)us), stateObj);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000E0004 File Offset: 0x000DF404
		internal byte[] SerializeUnsignedInt(uint i, TdsParserStateObject stateObj)
		{
			return this.SerializeInt((int)i, stateObj);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000E001C File Offset: 0x000DF41C
		internal void WriteUnsignedInt(uint i, TdsParserStateObject stateObj)
		{
			this.WriteInt((int)i, stateObj);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000E0034 File Offset: 0x000DF434
		internal byte[] SerializeInt(int v, TdsParserStateObject stateObj)
		{
			if (stateObj._bIntBytes == null)
			{
				stateObj._bIntBytes = new byte[4];
			}
			int num = 0;
			byte[] bIntBytes = stateObj._bIntBytes;
			bIntBytes[num++] = (byte)(v & 255);
			bIntBytes[num++] = (byte)(v >> 8 & 255);
			bIntBytes[num++] = (byte)(v >> 16 & 255);
			bIntBytes[num++] = (byte)(v >> 24 & 255);
			return bIntBytes;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000E00A4 File Offset: 0x000DF4A4
		internal void WriteInt(int v, TdsParserStateObject stateObj)
		{
			if (stateObj._outBytesUsed + 4 > stateObj._outBuff.Length)
			{
				for (int i = 0; i < 32; i += 8)
				{
					stateObj.WriteByte((byte)(v >> i & 255));
				}
				return;
			}
			stateObj._outBuff[stateObj._outBytesUsed] = (byte)(v & 255);
			stateObj._outBuff[stateObj._outBytesUsed + 1] = (byte)(v >> 8 & 255);
			stateObj._outBuff[stateObj._outBytesUsed + 2] = (byte)(v >> 16 & 255);
			stateObj._outBuff[stateObj._outBytesUsed + 3] = (byte)(v >> 24 & 255);
			stateObj._outBytesUsed += 4;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000E0154 File Offset: 0x000DF554
		internal byte[] SerializeFloat(float v)
		{
			if (float.IsInfinity(v) || float.IsNaN(v))
			{
				throw ADP.ParameterValueOutOfRange(v.ToString());
			}
			return BitConverter.GetBytes(v);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000E0184 File Offset: 0x000DF584
		internal void WriteFloat(float v, TdsParserStateObject stateObj)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			stateObj.WriteByteArray(bytes, bytes.Length, 0, true, null);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000E01A8 File Offset: 0x000DF5A8
		internal byte[] SerializeLong(long v, TdsParserStateObject stateObj)
		{
			int num = 0;
			if (stateObj._bLongBytes == null)
			{
				stateObj._bLongBytes = new byte[8];
			}
			byte[] bLongBytes = stateObj._bLongBytes;
			bLongBytes[num++] = (byte)(v & 255L);
			bLongBytes[num++] = (byte)(v >> 8 & 255L);
			bLongBytes[num++] = (byte)(v >> 16 & 255L);
			bLongBytes[num++] = (byte)(v >> 24 & 255L);
			bLongBytes[num++] = (byte)(v >> 32 & 255L);
			bLongBytes[num++] = (byte)(v >> 40 & 255L);
			bLongBytes[num++] = (byte)(v >> 48 & 255L);
			bLongBytes[num++] = (byte)(v >> 56 & 255L);
			return bLongBytes;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000E0268 File Offset: 0x000DF668
		internal void WriteLong(long v, TdsParserStateObject stateObj)
		{
			if (stateObj._outBytesUsed + 8 > stateObj._outBuff.Length)
			{
				for (int i = 0; i < 64; i += 8)
				{
					stateObj.WriteByte((byte)(v >> i & 255L));
				}
				return;
			}
			stateObj._outBuff[stateObj._outBytesUsed] = (byte)(v & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 1] = (byte)(v >> 8 & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 2] = (byte)(v >> 16 & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 3] = (byte)(v >> 24 & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 4] = (byte)(v >> 32 & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 5] = (byte)(v >> 40 & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 6] = (byte)(v >> 48 & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 7] = (byte)(v >> 56 & 255L);
			stateObj._outBytesUsed += 8;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000E038C File Offset: 0x000DF78C
		internal byte[] SerializePartialLong(long v, int length)
		{
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (byte)(v >> i * 8 & 255L);
			}
			return array;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000E03C0 File Offset: 0x000DF7C0
		internal void WritePartialLong(long v, int length, TdsParserStateObject stateObj)
		{
			if (stateObj._outBytesUsed + length > stateObj._outBuff.Length)
			{
				for (int i = 0; i < length * 8; i += 8)
				{
					stateObj.WriteByte((byte)(v >> i & 255L));
				}
				return;
			}
			for (int j = 0; j < length; j++)
			{
				stateObj._outBuff[stateObj._outBytesUsed + j] = (byte)(v >> j * 8 & 255L);
			}
			stateObj._outBytesUsed += length;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000E043C File Offset: 0x000DF83C
		internal void WriteUnsignedLong(ulong uv, TdsParserStateObject stateObj)
		{
			this.WriteLong((long)uv, stateObj);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000E0454 File Offset: 0x000DF854
		internal byte[] SerializeDouble(double v)
		{
			if (double.IsInfinity(v) || double.IsNaN(v))
			{
				throw ADP.ParameterValueOutOfRange(v.ToString());
			}
			return BitConverter.GetBytes(v);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000E0484 File Offset: 0x000DF884
		internal void WriteDouble(double v, TdsParserStateObject stateObj)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			stateObj.WriteByteArray(bytes, bytes.Length, 0, true, null);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000E04A8 File Offset: 0x000DF8A8
		internal void PrepareResetConnection(bool preserveTransaction)
		{
			this._fResetConnection = true;
			this._fPreserveTransaction = preserveTransaction;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000E04C8 File Offset: 0x000DF8C8
		internal bool RunReliably(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
				result = this.Run(runBehavior, cmdHandler, dataStream, bulkCopyHandler, stateObj);
			}
			catch (OutOfMemoryException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			return result;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000E0568 File Offset: 0x000DF968
		internal bool Run(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj)
		{
			bool syncOverAsync = stateObj._syncOverAsync;
			bool result;
			try
			{
				stateObj._syncOverAsync = true;
				bool flag2;
				bool flag = this.TryRun(runBehavior, cmdHandler, dataStream, bulkCopyHandler, stateObj, out flag2);
				result = flag2;
			}
			finally
			{
				stateObj._syncOverAsync = syncOverAsync;
			}
			return result;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000E05C0 File Offset: 0x000DF9C0
		internal static bool IsValidTdsToken(byte token)
		{
			return token == 170 || token == 171 || token == 173 || token == 227 || token == 172 || token == 121 || token == 160 || token == 161 || token == 129 || token == 136 || token == 164 || token == 165 || token == 169 || token == 211 || token == 209 || token == 210 || token == 253 || token == 254 || token == byte.MaxValue || token == 57 || token == 237 || token == 124 || token == 120 || token == 237 || token == 174 || token == 228 || token == 238;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000E06BC File Offset: 0x000DFABC
		internal bool TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, out bool dataReady)
		{
			if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
			{
				dataReady = true;
				return true;
			}
			dataReady = false;
			byte b;
			for (;;)
			{
				bool flag;
				if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
				{
					flag = stateObj._internalTimeout;
				}
				else
				{
					flag = stateObj.IsTimeoutStateExpired;
				}
				if (flag)
				{
					runBehavior = RunBehavior.Attention;
				}
				if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
				{
					goto IL_9CE;
				}
				if (!stateObj._accumulateInfoEvents && stateObj._pendingInfoEvents != null)
				{
					if (RunBehavior.Clean != (RunBehavior.Clean & runBehavior))
					{
						SqlConnection sqlConnection = null;
						if (this._connHandler != null)
						{
							sqlConnection = this._connHandler.Connection;
						}
						if (sqlConnection != null && sqlConnection.FireInfoMessageEventOnUserErrors)
						{
							using (List<SqlError>.Enumerator enumerator = stateObj._pendingInfoEvents.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									SqlError error = enumerator.Current;
									this.FireInfoMessageEvent(sqlConnection, stateObj, error);
								}
								goto IL_142;
							}
						}
						foreach (SqlError error2 in stateObj._pendingInfoEvents)
						{
							stateObj.AddWarning(error2);
						}
					}
					IL_142:
					stateObj._pendingInfoEvents = null;
				}
				if (!stateObj.TryReadByte(out b))
				{
					break;
				}
				if (!TdsParser.IsValidTdsToken(b))
				{
					goto Block_15;
				}
				int num;
				if (!this.TryGetTokenLength(b, stateObj, out num))
				{
					return false;
				}
				if (b <= 210)
				{
					if (b <= 129)
					{
						if (b != 121)
						{
							if (b == 129)
							{
								if (num != 65535)
								{
									_SqlMetaDataSet cleanupMetaData;
									if (!this.TryProcessMetaData(num, stateObj, out cleanupMetaData, (cmdHandler != null) ? cmdHandler.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting))
									{
										return false;
									}
									stateObj._cleanupMetaData = cleanupMetaData;
								}
								else if (cmdHandler != null)
								{
									stateObj._cleanupMetaData = cmdHandler.MetaData;
								}
								if (dataStream != null)
								{
									byte b2;
									if (!stateObj.TryPeekByte(out b2))
									{
										return false;
									}
									if (!dataStream.TrySetMetaData(stateObj._cleanupMetaData, 164 == b2 || 165 == b2))
									{
										return false;
									}
								}
								else if (bulkCopyHandler != null)
								{
									bulkCopyHandler.SetMetaData(stateObj._cleanupMetaData);
								}
							}
						}
						else
						{
							int status;
							if (!stateObj.TryReadInt32(out status))
							{
								return false;
							}
							if (cmdHandler != null)
							{
								cmdHandler.OnReturnStatus(status);
							}
						}
					}
					else if (b != 136)
					{
						switch (b)
						{
						case 164:
							if (dataStream != null)
							{
								MultiPartTableName[] tableNames;
								if (!this.TryProcessTableName(num, stateObj, out tableNames))
								{
									return false;
								}
								dataStream.TableNames = tableNames;
							}
							else if (!stateObj.TrySkipBytes(num))
							{
								return false;
							}
							break;
						case 165:
							if (dataStream != null)
							{
								_SqlMetaDataSet metaData;
								if (!this.TryProcessColInfo(dataStream.MetaData, dataStream, stateObj, out metaData))
								{
									return false;
								}
								if (!dataStream.TrySetMetaData(metaData, false))
								{
									return false;
								}
								dataStream.BrowseModeInfoConsumed = true;
							}
							else if (!stateObj.TrySkipBytes(num))
							{
								return false;
							}
							break;
						case 166:
						case 167:
						case 168:
							break;
						case 169:
							if (!stateObj.TrySkipBytes(num))
							{
								return false;
							}
							break;
						case 170:
						case 171:
						{
							if (b == 170)
							{
								stateObj._errorTokenReceived = true;
							}
							SqlError sqlError;
							if (!this.TryProcessError(b, stateObj, out sqlError))
							{
								return false;
							}
							if (b == 171 && stateObj._accumulateInfoEvents)
							{
								if (stateObj._pendingInfoEvents == null)
								{
									stateObj._pendingInfoEvents = new List<SqlError>();
								}
								stateObj._pendingInfoEvents.Add(sqlError);
								stateObj._syncOverAsync = true;
							}
							else if (RunBehavior.Clean != (RunBehavior.Clean & runBehavior))
							{
								SqlConnection sqlConnection2 = null;
								if (this._connHandler != null)
								{
									sqlConnection2 = this._connHandler.Connection;
								}
								if (sqlConnection2 != null && sqlConnection2.FireInfoMessageEventOnUserErrors && sqlError.Class <= 16)
								{
									this.FireInfoMessageEvent(sqlConnection2, stateObj, sqlError);
								}
								else if (sqlError.Class < 11)
								{
									stateObj.AddWarning(sqlError);
								}
								else if (sqlError.Class < 20)
								{
									stateObj.AddError(sqlError);
									if (dataStream != null && !dataStream.IsInitialized)
									{
										runBehavior = RunBehavior.UntilDone;
									}
								}
								else
								{
									stateObj.AddError(sqlError);
									runBehavior = RunBehavior.UntilDone;
								}
							}
							else if (sqlError.Class >= 20)
							{
								stateObj.AddError(sqlError);
							}
							break;
						}
						case 172:
						{
							SqlReturnValue rec;
							if (!this.TryProcessReturnValue(num, stateObj, out rec, (cmdHandler != null) ? cmdHandler.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting))
							{
								return false;
							}
							if (cmdHandler != null)
							{
								cmdHandler.OnReturnValue(rec, stateObj);
							}
							break;
						}
						case 173:
						{
							Bid.Trace("<sc.TdsParser.TryRun|SEC> Received login acknowledgement token\n");
							SqlLoginAck rec2;
							if (!this.TryProcessLoginAck(stateObj, out rec2))
							{
								return false;
							}
							this._connHandler.OnLoginAck(rec2);
							break;
						}
						case 174:
							if (!this.TryProcessFeatureExtAck(stateObj))
							{
								return false;
							}
							break;
						default:
							if (b - 209 <= 1)
							{
								if (b == 210)
								{
									if (!stateObj.TryStartNewRow(true, stateObj._cleanupMetaData.Length))
									{
										return false;
									}
								}
								else if (!stateObj.TryStartNewRow(false, 0))
								{
									return false;
								}
								if (bulkCopyHandler != null)
								{
									if (!this.TryProcessRow(stateObj._cleanupMetaData, bulkCopyHandler.CreateRowBuffer(), bulkCopyHandler.CreateIndexMap(), stateObj))
									{
										return false;
									}
								}
								else if (RunBehavior.ReturnImmediately != (RunBehavior.ReturnImmediately & runBehavior))
								{
									if (!this.TrySkipRow(stateObj._cleanupMetaData, stateObj))
									{
										return false;
									}
								}
								else
								{
									dataReady = true;
								}
								if (this._statistics != null)
								{
									this._statistics.WaitForDoneAfterRow = true;
								}
							}
							break;
						}
					}
					else
					{
						stateObj.CloneCleanupAltMetaDataSetArray();
						if (stateObj._cleanupAltMetaDataSetArray == null)
						{
							stateObj._cleanupAltMetaDataSetArray = new _SqlMetaDataSetCollection();
						}
						_SqlMetaDataSet sqlMetaDataSet;
						if (!this.TryProcessAltMetaData(num, stateObj, out sqlMetaDataSet))
						{
							return false;
						}
						stateObj._cleanupAltMetaDataSetArray.SetAltMetaData(sqlMetaDataSet);
						if (dataStream != null)
						{
							byte b3;
							if (!stateObj.TryPeekByte(out b3))
							{
								return false;
							}
							if (!dataStream.TrySetAltMetaDataSet(sqlMetaDataSet, 136 != b3))
							{
								return false;
							}
						}
					}
				}
				else if (b <= 228)
				{
					if (b != 211)
					{
						if (b != 227)
						{
							if (b == 228)
							{
								if (!this.TryProcessSessionState(stateObj, num, this._connHandler._currentSessionData))
								{
									return false;
								}
							}
						}
						else
						{
							stateObj._syncOverAsync = true;
							SqlEnvChange[] array;
							if (!this.TryProcessEnvChange(num, stateObj, out array))
							{
								return false;
							}
							for (int i = 0; i < array.Length; i++)
							{
								if (array[i] != null && !this.Connection.IgnoreEnvChange)
								{
									switch (array[i].type)
									{
									case 8:
									case 11:
										this._currentTransaction = this._pendingTransaction;
										this._pendingTransaction = null;
										if (this._currentTransaction != null)
										{
											this._currentTransaction.TransactionId = array[i].newLongValue;
										}
										else
										{
											TransactionType type = (8 == array[i].type) ? TransactionType.LocalFromTSQL : TransactionType.Distributed;
											this._currentTransaction = new SqlInternalTransaction(this._connHandler, type, null, array[i].newLongValue);
										}
										if (this._statistics != null && !this._statisticsIsInTransaction)
										{
											this._statistics.SafeIncrement(ref this._statistics._transactions);
										}
										this._statisticsIsInTransaction = true;
										this._retainedTransactionId = 0L;
										goto IL_6F3;
									case 9:
									case 12:
									case 17:
										this._retainedTransactionId = 0L;
										break;
									case 10:
										break;
									case 13:
									case 14:
									case 15:
									case 16:
										goto IL_6E4;
									default:
										goto IL_6E4;
									}
									if (this._currentTransaction != null)
									{
										if (9 == array[i].type)
										{
											this._currentTransaction.Completed(TransactionState.Committed);
										}
										else if (10 == array[i].type)
										{
											if (this._currentTransaction.IsDistributed && this._currentTransaction.IsActive)
											{
												this._retainedTransactionId = array[i].oldLongValue;
											}
											this._currentTransaction.Completed(TransactionState.Aborted);
										}
										else
										{
											this._currentTransaction.Completed(TransactionState.Unknown);
										}
										this._currentTransaction = null;
									}
									this._statisticsIsInTransaction = false;
									goto IL_6F3;
									IL_6E4:
									this._connHandler.OnEnvChange(array[i]);
								}
								IL_6F3:;
							}
						}
					}
					else
					{
						if (!stateObj.TryStartNewRow(false, 0))
						{
							return false;
						}
						if (RunBehavior.ReturnImmediately != (RunBehavior.ReturnImmediately & runBehavior))
						{
							ushort id;
							if (!stateObj.TryReadUInt16(out id))
							{
								return false;
							}
							if (!this.TrySkipRow(stateObj._cleanupAltMetaDataSetArray.GetAltMetaData((int)id), stateObj))
							{
								return false;
							}
						}
						else
						{
							dataReady = true;
						}
					}
				}
				else if (b != 237)
				{
					if (b != 238)
					{
						if (b - 253 <= 2)
						{
							if (!this.TryProcessDone(cmdHandler, dataStream, ref runBehavior, stateObj))
							{
								return false;
							}
							if (b == 254 && cmdHandler != null)
							{
								if (cmdHandler.IsDescribeParameterEncryptionRPCCurrentlyInProgress)
								{
									cmdHandler.OnDoneDescribeParameterEncryptionProc(stateObj);
								}
								else
								{
									cmdHandler.OnDoneProc();
								}
							}
						}
					}
					else
					{
						this._connHandler._federatedAuthenticationInfoReceived = true;
						Bid.Trace("<sc.TdsParser.TryRun|SEC> Received federated authentication info token\n");
						SqlFedAuthInfo fedAuthInfo;
						if (!this.TryProcessFedAuthInfo(stateObj, num, out fedAuthInfo))
						{
							return false;
						}
						this._connHandler.OnFedAuthInfo(fedAuthInfo);
					}
				}
				else
				{
					stateObj._syncOverAsync = true;
					this.ProcessSSPI(num);
				}
				if ((!stateObj._pendingData || RunBehavior.ReturnImmediately == (RunBehavior.ReturnImmediately & runBehavior)) && (stateObj._pendingData || !stateObj._attentionSent || stateObj._attentionReceived))
				{
					goto IL_9CE;
				}
			}
			return false;
			Block_15:
			this._state = TdsParserState.Broken;
			this._connHandler.BreakConnection();
			Bid.Trace("<sc.TdsParser.Run|ERR> Potential multi-threaded misuse of connection, unexpected TDS token found %d#\n", this.ObjectID);
			throw SQL.ParsingErrorToken(ParsingErrorState.InvalidTdsTokenReceived, (int)b);
			IL_9CE:
			if (!stateObj._pendingData && this.CurrentTransaction != null)
			{
				this.CurrentTransaction.Activate();
			}
			if (stateObj._attentionReceived)
			{
				SpinWait.SpinUntil(() => !stateObj._attentionSending);
				if (stateObj._attentionSent)
				{
					stateObj._attentionSent = false;
					stateObj._attentionReceived = false;
					bool flag2;
					if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
					{
						flag2 = stateObj._internalTimeout;
					}
					else
					{
						flag2 = stateObj.IsTimeoutStateExpired;
					}
					if (RunBehavior.Clean != (RunBehavior.Clean & runBehavior) && !flag2)
					{
						stateObj.AddError(new SqlError(0, 0, 11, this._server, SQLMessage.OperationCancelled(), "", 0));
					}
				}
			}
			if (stateObj.HasErrorOrWarning)
			{
				this.ThrowExceptionAndWarning(stateObj, false, false);
			}
			return true;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000E11A4 File Offset: 0x000E05A4
		private bool TryProcessEnvChange(int tokenLength, TdsParserStateObject stateObj, out SqlEnvChange[] sqlEnvChange)
		{
			int num = 0;
			int num2 = 0;
			SqlEnvChange[] array = new SqlEnvChange[3];
			sqlEnvChange = null;
			while (tokenLength > num)
			{
				if (num2 >= array.Length)
				{
					SqlEnvChange[] array2 = new SqlEnvChange[array.Length + 3];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = array[i];
					}
					array = array2;
				}
				SqlEnvChange sqlEnvChange2 = new SqlEnvChange();
				if (!stateObj.TryReadByte(out sqlEnvChange2.type))
				{
					return false;
				}
				array[num2] = sqlEnvChange2;
				num2++;
				switch (sqlEnvChange2.type)
				{
				case 1:
				case 2:
					if (!this.TryReadTwoStringFields(sqlEnvChange2, stateObj))
					{
						return false;
					}
					break;
				case 3:
					if (!this.TryReadTwoStringFields(sqlEnvChange2, stateObj))
					{
						return false;
					}
					if (sqlEnvChange2.newValue == "iso_1")
					{
						this._defaultCodePage = 1252;
						this._defaultEncoding = Encoding.GetEncoding(this._defaultCodePage);
					}
					else
					{
						string s = sqlEnvChange2.newValue.Substring(2);
						this._defaultCodePage = int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture);
						this._defaultEncoding = Encoding.GetEncoding(this._defaultCodePage);
					}
					break;
				case 4:
				{
					if (!this.TryReadTwoStringFields(sqlEnvChange2, stateObj))
					{
						throw SQL.SynchronousCallMayNotPend();
					}
					int num3 = int.Parse(sqlEnvChange2.newValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
					if (this._physicalStateObj.SetPacketSize(num3))
					{
						this._physicalStateObj.ClearAllWritePackets();
						uint num4 = (uint)num3;
						uint num5 = SNINativeMethodWrapper.SNISetInfo(this._physicalStateObj.Handle, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_BUFSIZE, ref num4);
					}
					break;
				}
				case 5:
					if (!this.TryReadTwoStringFields(sqlEnvChange2, stateObj))
					{
						return false;
					}
					this._defaultLCID = int.Parse(sqlEnvChange2.newValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
					break;
				case 6:
					if (!this.TryReadTwoStringFields(sqlEnvChange2, stateObj))
					{
						return false;
					}
					break;
				case 7:
				{
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange2.newLength = (int)b;
					if (sqlEnvChange2.newLength == 5)
					{
						if (!this.TryProcessCollation(stateObj, out sqlEnvChange2.newCollation))
						{
							return false;
						}
						this._defaultCollation = sqlEnvChange2.newCollation;
						int codePage = this.GetCodePage(sqlEnvChange2.newCollation, stateObj);
						if (codePage != this._defaultCodePage)
						{
							this._defaultCodePage = codePage;
							this._defaultEncoding = Encoding.GetEncoding(this._defaultCodePage);
						}
						this._defaultLCID = sqlEnvChange2.newCollation.LCID;
					}
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange2.oldLength = b;
					if (sqlEnvChange2.oldLength == 5 && !this.TryProcessCollation(stateObj, out sqlEnvChange2.oldCollation))
					{
						return false;
					}
					sqlEnvChange2.length = 3 + sqlEnvChange2.newLength + (int)sqlEnvChange2.oldLength;
					break;
				}
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 17:
				{
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange2.newLength = (int)b;
					if (sqlEnvChange2.newLength > 0)
					{
						if (!stateObj.TryReadInt64(out sqlEnvChange2.newLongValue))
						{
							return false;
						}
					}
					else
					{
						sqlEnvChange2.newLongValue = 0L;
					}
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange2.oldLength = b;
					if (sqlEnvChange2.oldLength > 0)
					{
						if (!stateObj.TryReadInt64(out sqlEnvChange2.oldLongValue))
						{
							return false;
						}
					}
					else
					{
						sqlEnvChange2.oldLongValue = 0L;
					}
					sqlEnvChange2.length = 3 + sqlEnvChange2.newLength + (int)sqlEnvChange2.oldLength;
					break;
				}
				case 13:
					if (!this.TryReadTwoStringFields(sqlEnvChange2, stateObj))
					{
						return false;
					}
					break;
				case 15:
				{
					if (!stateObj.TryReadInt32(out sqlEnvChange2.newLength))
					{
						return false;
					}
					sqlEnvChange2.newBinValue = new byte[sqlEnvChange2.newLength];
					if (!stateObj.TryReadByteArray(sqlEnvChange2.newBinValue, 0, sqlEnvChange2.newLength))
					{
						return false;
					}
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange2.oldLength = b;
					sqlEnvChange2.length = 5 + sqlEnvChange2.newLength;
					break;
				}
				case 16:
				case 18:
					if (!this.TryReadTwoBinaryFields(sqlEnvChange2, stateObj))
					{
						return false;
					}
					break;
				case 19:
					if (!this.TryReadTwoStringFields(sqlEnvChange2, stateObj))
					{
						return false;
					}
					break;
				case 20:
				{
					ushort newLength;
					if (!stateObj.TryReadUInt16(out newLength))
					{
						return false;
					}
					sqlEnvChange2.newLength = (int)newLength;
					byte protocol;
					if (!stateObj.TryReadByte(out protocol))
					{
						return false;
					}
					ushort port;
					if (!stateObj.TryReadUInt16(out port))
					{
						return false;
					}
					ushort length;
					if (!stateObj.TryReadUInt16(out length))
					{
						return false;
					}
					string servername;
					if (!stateObj.TryReadString((int)length, out servername))
					{
						return false;
					}
					sqlEnvChange2.newRoutingInfo = new RoutingInfo(protocol, port, servername);
					ushort num6;
					if (!stateObj.TryReadUInt16(out num6))
					{
						return false;
					}
					if (!stateObj.TrySkipBytes((int)num6))
					{
						return false;
					}
					sqlEnvChange2.length = sqlEnvChange2.newLength + (int)num6 + 5;
					break;
				}
				}
				num += sqlEnvChange2.length;
			}
			sqlEnvChange = array;
			return true;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000E15FC File Offset: 0x000E09FC
		private bool TryReadTwoBinaryFields(SqlEnvChange env, TdsParserStateObject stateObj)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			env.newLength = (int)b;
			env.newBinValue = new byte[env.newLength];
			if (!stateObj.TryReadByteArray(env.newBinValue, 0, env.newLength))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			env.oldLength = b;
			env.oldBinValue = new byte[(int)env.oldLength];
			if (!stateObj.TryReadByteArray(env.oldBinValue, 0, (int)env.oldLength))
			{
				return false;
			}
			env.length = 3 + env.newLength + (int)env.oldLength;
			return true;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000E1698 File Offset: 0x000E0A98
		private bool TryReadTwoStringFields(SqlEnvChange env, TdsParserStateObject stateObj)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			string newValue;
			if (!stateObj.TryReadString((int)b, out newValue))
			{
				return false;
			}
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			string oldValue;
			if (!stateObj.TryReadString((int)b2, out oldValue))
			{
				return false;
			}
			env.newLength = (int)b;
			env.newValue = newValue;
			env.oldLength = b2;
			env.oldValue = oldValue;
			env.length = 3 + env.newLength * 2 + (int)(env.oldLength * 2);
			return true;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000E1710 File Offset: 0x000E0B10
		private bool TryProcessDone(SqlCommand cmd, SqlDataReader reader, ref RunBehavior run, TdsParserStateObject stateObj)
		{
			if (LocalAppContextSwitches.MakeReadAsyncBlocking)
			{
				stateObj._syncOverAsync = true;
			}
			ushort num;
			if (!stateObj.TryReadUInt16(out num))
			{
				return false;
			}
			ushort num2;
			if (!stateObj.TryReadUInt16(out num2))
			{
				return false;
			}
			int num4;
			if (this._isYukon)
			{
				long num3;
				if (!stateObj.TryReadInt64(out num3))
				{
					return false;
				}
				num4 = (int)num3;
			}
			else
			{
				if (!stateObj.TryReadInt32(out num4))
				{
					return false;
				}
				if (this._state == TdsParserState.OpenNotLoggedIn && stateObj._inBytesRead > stateObj._inBytesUsed)
				{
					byte b;
					if (!stateObj.TryPeekByte(out b))
					{
						return false;
					}
					if (b == 0 && !stateObj.TryReadInt32(out num4))
					{
						return false;
					}
				}
			}
			if (32 == (num & 32))
			{
				stateObj._attentionReceived = true;
			}
			if (cmd != null && 16 == (num & 16))
			{
				if (num2 != 193)
				{
					if (cmd.IsDescribeParameterEncryptionRPCCurrentlyInProgress)
					{
						cmd.RowsAffectedByDescribeParameterEncryption = num4;
					}
					else
					{
						cmd.InternalRecordsAffected = num4;
					}
				}
				if (stateObj._receivedColMetaData || num2 != 193)
				{
					cmd.OnStatementCompleted(num4);
				}
			}
			stateObj._receivedColMetaData = false;
			if (2 == (2 & num) && stateObj.ErrorCount == 0 && !stateObj._errorTokenReceived && RunBehavior.Clean != (RunBehavior.Clean & run))
			{
				stateObj.AddError(new SqlError(0, 0, 11, this._server, SQLMessage.SevereError(), "", 0));
				if (reader != null && !reader.IsInitialized)
				{
					run = RunBehavior.UntilDone;
				}
			}
			if (256 == (256 & num) && RunBehavior.Clean != (RunBehavior.Clean & run))
			{
				stateObj.AddError(new SqlError(0, 0, 20, this._server, SQLMessage.SevereError(), "", 0));
				if (reader != null && !reader.IsInitialized)
				{
					run = RunBehavior.UntilDone;
				}
			}
			this.ProcessSqlStatistics(num2, num, num4);
			if (1 != (num & 1))
			{
				stateObj._errorTokenReceived = false;
				if (stateObj._inBytesUsed >= stateObj._inBytesRead)
				{
					stateObj._pendingData = false;
				}
			}
			if (!stateObj._pendingData && stateObj._hasOpenResult)
			{
				stateObj.DecrementOpenResultCount();
			}
			return true;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000E18DC File Offset: 0x000E0CDC
		private void ProcessSqlStatistics(ushort curCmd, ushort status, int count)
		{
			if (this._statistics != null)
			{
				if (this._statistics.WaitForDoneAfterRow)
				{
					this._statistics.SafeIncrement(ref this._statistics._sumResultSets);
					this._statistics.WaitForDoneAfterRow = false;
				}
				if (16 != (status & 16))
				{
					count = 0;
				}
				if (curCmd <= 193)
				{
					if (curCmd == 32)
					{
						this._statistics.SafeIncrement(ref this._statistics._cursorOpens);
						return;
					}
					if (curCmd != 193)
					{
						return;
					}
					this._statistics.SafeIncrement(ref this._statistics._selectCount);
					this._statistics.SafeAdd(ref this._statistics._selectRows, (long)count);
					return;
				}
				else
				{
					if (curCmd - 195 > 2)
					{
						switch (curCmd)
						{
						case 210:
							this._statisticsIsInTransaction = false;
							return;
						case 211:
							return;
						case 212:
							if (!this._statisticsIsInTransaction)
							{
								this._statistics.SafeIncrement(ref this._statistics._transactions);
							}
							this._statisticsIsInTransaction = true;
							return;
						case 213:
							this._statisticsIsInTransaction = false;
							return;
						default:
							if (curCmd != 279)
							{
								return;
							}
							break;
						}
					}
					this._statistics.SafeIncrement(ref this._statistics._iduCount);
					this._statistics.SafeAdd(ref this._statistics._iduRows, (long)count);
					if (!this._statisticsIsInTransaction)
					{
						this._statistics.SafeIncrement(ref this._statistics._transactions);
						return;
					}
				}
			}
			else
			{
				switch (curCmd)
				{
				case 210:
				case 213:
					this._statisticsIsInTransaction = false;
					break;
				case 211:
					break;
				case 212:
					this._statisticsIsInTransaction = true;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000E1A7C File Offset: 0x000E0E7C
		private bool TryProcessFeatureExtAck(TdsParserStateObject stateObj)
		{
			byte b;
			while (stateObj.TryReadByte(out b))
			{
				if (b != 255)
				{
					uint num;
					if (!stateObj.TryReadUInt32(out num))
					{
						return false;
					}
					byte[] array = new byte[num];
					if (num > 0U && !stateObj.TryReadByteArray(array, 0, checked((int)num)))
					{
						return false;
					}
					this._connHandler.OnFeatureExtAck((int)b, array);
				}
				if (b == 255)
				{
					if (this.Connection.RoutingInfo == null && this._connHandler.ConnectionOptions.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled && !this.IsColumnEncryptionSupported)
					{
						throw SQL.TceNotSupported();
					}
					if (this.Connection.RoutingInfo == null && !string.IsNullOrWhiteSpace(this._connHandler.ConnectionOptions.EnclaveAttestationUrl) && this.TceVersionSupported < 2)
					{
						throw SQL.EnclaveComputationsNotSupported();
					}
					if (this.Connection.RoutingInfo == null && !string.IsNullOrWhiteSpace(this._connHandler.ConnectionOptions.EnclaveAttestationUrl) && string.IsNullOrWhiteSpace(this.EnclaveType))
					{
						throw SQL.EnclaveTypeNotReturned();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000E1B70 File Offset: 0x000E0F70
		private bool TryProcessSessionState(TdsParserStateObject stateObj, int length, SessionData sdata)
		{
			if (length < 5)
			{
				throw SQL.ParsingErrorLength(ParsingErrorState.SessionStateLengthTooShort, length);
			}
			uint num;
			if (!stateObj.TryReadUInt32(out num))
			{
				return false;
			}
			if (num == 4294967295U)
			{
				this._connHandler.DoNotPoolThisConnection();
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			if (b > 1)
			{
				throw SQL.ParsingErrorStatus(ParsingErrorState.SessionStateInvalidStatus, (int)b);
			}
			bool flag = b > 0;
			length -= 5;
			while (length > 0)
			{
				byte b2;
				if (!stateObj.TryReadByte(out b2))
				{
					return false;
				}
				byte b3;
				if (!stateObj.TryReadByte(out b3))
				{
					return false;
				}
				int num2;
				if (b3 < 255)
				{
					num2 = (int)b3;
				}
				else if (!stateObj.TryReadInt32(out num2))
				{
					return false;
				}
				byte[] array = null;
				SessionStateRecord[] delta = sdata._delta;
				checked
				{
					lock (delta)
					{
						if (sdata._delta[(int)b2] == null)
						{
							array = new byte[num2];
							sdata._delta[(int)b2] = new SessionStateRecord
							{
								_version = num,
								_dataLength = num2,
								_data = array,
								_recoverable = flag
							};
							sdata._deltaDirty = true;
							if (!flag)
							{
								sdata._unrecoverableStatesCount += 1;
							}
						}
						else if (sdata._delta[(int)b2]._version <= num)
						{
							SessionStateRecord sessionStateRecord = sdata._delta[(int)b2];
							sessionStateRecord._version = num;
							sessionStateRecord._dataLength = num2;
							if (sessionStateRecord._recoverable != flag)
							{
								if (flag)
								{
									unchecked
									{
										sdata._unrecoverableStatesCount -= 1;
									}
								}
								else
								{
									sdata._unrecoverableStatesCount += 1;
								}
								sessionStateRecord._recoverable = flag;
							}
							array = sessionStateRecord._data;
							if (array.Length < num2)
							{
								array = new byte[num2];
								sessionStateRecord._data = array;
							}
						}
					}
					if (array != null)
					{
						if (!stateObj.TryReadByteArray(array, 0, num2))
						{
							return false;
						}
					}
					else if (!stateObj.TrySkipBytes(num2))
					{
						return false;
					}
				}
				if (b3 < 255)
				{
					length -= 2 + num2;
				}
				else
				{
					length -= 6 + num2;
				}
			}
			return true;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000E1D58 File Offset: 0x000E1158
		private bool TryProcessLoginAck(TdsParserStateObject stateObj, out SqlLoginAck sqlLoginAck)
		{
			SqlLoginAck sqlLoginAck2 = new SqlLoginAck();
			sqlLoginAck = null;
			if (!stateObj.TrySkipBytes(1))
			{
				return false;
			}
			byte[] array = new byte[4];
			if (!stateObj.TryReadByteArray(array, 0, array.Length))
			{
				return false;
			}
			sqlLoginAck2.tdsVersion = (uint)((((int)array[0] << 8 | (int)array[1]) << 8 | (int)array[2]) << 8 | (int)array[3]);
			uint num = sqlLoginAck2.tdsVersion & 4278255615U;
			uint num2 = sqlLoginAck2.tdsVersion >> 16 & 255U;
			if (num <= 1895825409U)
			{
				if (num != 117440512U)
				{
					if (num == 1895825409U)
					{
						if (num2 != 0U)
						{
							throw SQL.InvalidTDSVersion();
						}
						this._isShilohSP1 = true;
						goto IL_101;
					}
				}
				else
				{
					if (num2 == 0U)
					{
						goto IL_101;
					}
					if (num2 == 1U)
					{
						this._isShiloh = true;
						goto IL_101;
					}
					throw SQL.InvalidTDSVersion();
				}
			}
			else if (num != 1912602626U)
			{
				if (num != 1929379843U)
				{
					if (num == 1946157060U)
					{
						if (num2 != 0U)
						{
							throw SQL.InvalidTDSVersion();
						}
						this._isDenali = true;
						goto IL_101;
					}
				}
				else
				{
					if (num2 != 11U)
					{
						throw SQL.InvalidTDSVersion();
					}
					this._isKatmai = true;
					goto IL_101;
				}
			}
			else
			{
				if (num2 != 9U)
				{
					throw SQL.InvalidTDSVersion();
				}
				this._isYukon = true;
				goto IL_101;
			}
			throw SQL.InvalidTDSVersion();
			IL_101:
			this._isKatmai |= this._isDenali;
			this._isYukon |= this._isKatmai;
			this._isShilohSP1 |= this._isYukon;
			this._isShiloh |= this._isShilohSP1;
			sqlLoginAck2.isVersion8 = this._isShiloh;
			stateObj._outBytesUsed = stateObj._outputHeaderLen;
			byte length;
			if (!stateObj.TryReadByte(out length))
			{
				return false;
			}
			if (!stateObj.TryReadString((int)length, out sqlLoginAck2.programName))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out sqlLoginAck2.majorVersion))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out sqlLoginAck2.minorVersion))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			sqlLoginAck2.buildNum = (short)(((int)b << 8) + (int)b2);
			this._state = TdsParserState.OpenLoggedIn;
			if (this._isYukon && this._fMARS)
			{
				this._resetConnectionEvent = new AutoResetEvent(true);
			}
			if (this._connHandler.ConnectionOptions.UserInstance && ADP.IsEmpty(this._connHandler.InstanceName))
			{
				stateObj.AddError(new SqlError(0, 0, 20, this.Server, SQLMessage.UserInstanceFailure(), "", 0));
				this.ThrowExceptionAndWarning(stateObj, false, false);
			}
			sqlLoginAck = sqlLoginAck2;
			return true;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000E1FA4 File Offset: 0x000E13A4
		private bool TryProcessFedAuthInfo(TdsParserStateObject stateObj, int tokenLen, out SqlFedAuthInfo sqlFedAuthInfo)
		{
			sqlFedAuthInfo = null;
			SqlFedAuthInfo sqlFedAuthInfo2 = new SqlFedAuthInfo();
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo> FEDAUTHINFO token stream length = %d\n", tokenLen);
			}
			if (tokenLen < 4)
			{
				Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FEDAUTHINFO token stream length too short for CountOfInfoIDs.\n");
				throw SQL.ParsingErrorLength(ParsingErrorState.FedAuthInfoLengthTooShortForCountOfInfoIds, tokenLen);
			}
			uint num;
			if (!stateObj.TryReadUInt32(out num))
			{
				Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo|ERR> Failed to read CountOfInfoIDs in FEDAUTHINFO token stream.\n");
				throw SQL.ParsingError(ParsingErrorState.FedAuthInfoFailedToReadCountOfInfoIds);
			}
			tokenLen -= 4;
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo> CountOfInfoIDs = %ls\n", num.ToString(CultureInfo.InvariantCulture));
			}
			if (tokenLen <= 0)
			{
				Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FEDAUTHINFO token stream is not long enough to contain the data it claims to.\n");
				throw SQL.ParsingErrorLength(ParsingErrorState.FedAuthInfoLengthTooShortForData, tokenLen);
			}
			byte[] array = new byte[tokenLen];
			int num2 = 0;
			bool flag = stateObj.TryReadByteArray(array, 0, tokenLen, out num2);
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo> Read rest of FEDAUTHINFO token stream: %ls\n", BitConverter.ToString(array, 0, num2));
			}
			if (!flag || num2 != tokenLen)
			{
				Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo|ERR> Failed to read FEDAUTHINFO token stream. Attempted to read %d bytes, actually read %d\n", tokenLen, num2);
				throw SQL.ParsingError(ParsingErrorState.FedAuthInfoFailedToReadTokenStream);
			}
			uint num3 = checked(num * 9U);
			for (uint num4 = 0U; num4 < num; num4 += 1U)
			{
				checked
				{
					uint num5 = num4 * 9U;
					byte b = array[(int)num5];
					uint num6 = BitConverter.ToUInt32(array, (int)(num5 + 1U));
					uint num7 = BitConverter.ToUInt32(array, (int)(num5 + 5U));
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo> FedAuthInfoOpt: ID=%d, DataLen=%ls, Offset=%ls\n", (int)b, num6.ToString(CultureInfo.InvariantCulture), num7.ToString(CultureInfo.InvariantCulture));
					}
					num7 -= 4U;
					if (num7 < num3 || unchecked((ulong)num7 >= (ulong)((long)tokenLen)))
					{
						Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FedAuthInfoDataOffset points to an invalid location.\n");
						throw SQL.ParsingErrorOffset(ParsingErrorState.FedAuthInfoInvalidOffset, (int)num7);
					}
					string @string;
					try
					{
						@string = Encoding.Unicode.GetString(array, (int)num7, (int)num6);
					}
					catch (ArgumentOutOfRangeException innerException)
					{
						Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo|ERR> Failed to read FedAuthInfoData.\n");
						throw SQL.ParsingError(ParsingErrorState.FedAuthInfoFailedToReadData, innerException);
					}
					catch (ArgumentException innerException2)
					{
						Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FedAuthInfoData is not in unicode format.\n");
						throw SQL.ParsingError(ParsingErrorState.FedAuthInfoDataNotUnicode, innerException2);
					}
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo> FedAuthInfoData: %ls\n", @string);
					}
					TdsEnums.FedAuthInfoId fedAuthInfoId = (TdsEnums.FedAuthInfoId)b;
					if (fedAuthInfoId != TdsEnums.FedAuthInfoId.Stsurl)
					{
						if (fedAuthInfoId == TdsEnums.FedAuthInfoId.Spn)
						{
							sqlFedAuthInfo2.spn = @string;
						}
						else if (Bid.AdvancedOn)
						{
							Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo> Ignoring unknown federated authentication info option: %d\n", (int)b);
						}
					}
					else
					{
						sqlFedAuthInfo2.stsurl = @string;
					}
				}
			}
			Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo> Processed FEDAUTHINFO token stream: %ls\n", sqlFedAuthInfo2.ToString());
			if (string.IsNullOrWhiteSpace(sqlFedAuthInfo2.stsurl) || string.IsNullOrWhiteSpace(sqlFedAuthInfo2.spn))
			{
				Bid.Trace("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FEDAUTHINFO token stream does not contain both STSURL and SPN.\n");
				throw SQL.ParsingError(ParsingErrorState.FedAuthInfoDoesNotContainStsurlAndSpn);
			}
			sqlFedAuthInfo = sqlFedAuthInfo2;
			return true;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000E2214 File Offset: 0x000E1614
		internal bool TryProcessError(byte token, TdsParserStateObject stateObj, out SqlError error)
		{
			error = null;
			int infoNumber;
			if (!stateObj.TryReadInt32(out infoNumber))
			{
				return false;
			}
			byte errorState;
			if (!stateObj.TryReadByte(out errorState))
			{
				return false;
			}
			byte errorClass;
			if (!stateObj.TryReadByte(out errorClass))
			{
				return false;
			}
			ushort length;
			if (!stateObj.TryReadUInt16(out length))
			{
				return false;
			}
			string errorMessage;
			if (!stateObj.TryReadString((int)length, out errorMessage))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			string server;
			if (b == 0)
			{
				server = this._server;
			}
			else if (!stateObj.TryReadString((int)b, out server))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			string procedure;
			if (!stateObj.TryReadString((int)b, out procedure))
			{
				return false;
			}
			int num;
			if (this._isYukon)
			{
				if (!stateObj.TryReadInt32(out num))
				{
					return false;
				}
			}
			else
			{
				ushort num2;
				if (!stateObj.TryReadUInt16(out num2))
				{
					return false;
				}
				num = (int)num2;
				if (this._state == TdsParserState.OpenNotLoggedIn)
				{
					byte b2;
					if (!stateObj.TryPeekByte(out b2))
					{
						return false;
					}
					if (b2 == 0)
					{
						ushort num3;
						if (!stateObj.TryReadUInt16(out num3))
						{
							return false;
						}
						num = (num << 16) + (int)num3;
					}
				}
			}
			error = new SqlError(infoNumber, errorState, errorClass, this._server, errorMessage, procedure, num);
			return true;
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000E2308 File Offset: 0x000E1708
		internal bool TryProcessReturnValue(int length, TdsParserStateObject stateObj, out SqlReturnValue returnValue, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			returnValue = null;
			SqlReturnValue sqlReturnValue = new SqlReturnValue();
			sqlReturnValue.length = length;
			if (this._isYukon && !stateObj.TryReadUInt16(out sqlReturnValue.parmIndex))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			sqlReturnValue.parameter = null;
			if (b > 0 && !stateObj.TryReadString((int)b, out sqlReturnValue.parameter))
			{
				return false;
			}
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			uint userType;
			if (this.IsYukonOrNewer)
			{
				if (!stateObj.TryReadUInt32(out userType))
				{
					return false;
				}
			}
			else
			{
				ushort num;
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				userType = (uint)num;
			}
			byte b3;
			if (!stateObj.TryReadByte(out b3))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out b3))
			{
				return false;
			}
			if (this._serverSupportsColumnEncryption)
			{
				sqlReturnValue.isEncrypted = (8 == (b3 & 8));
			}
			byte b4;
			if (!stateObj.TryReadByte(out b4))
			{
				return false;
			}
			int num2;
			if (b4 == 241)
			{
				num2 = 65535;
			}
			else if (this.IsVarTimeTds(b4))
			{
				num2 = 0;
			}
			else if (b4 == 40)
			{
				num2 = 3;
			}
			else if (!this.TryGetTokenLength(b4, stateObj, out num2))
			{
				return false;
			}
			sqlReturnValue.metaType = MetaType.GetSqlDataType((int)b4, userType, num2);
			sqlReturnValue.type = sqlReturnValue.metaType.SqlDbType;
			if (this._isShiloh)
			{
				sqlReturnValue.tdsType = sqlReturnValue.metaType.NullableType;
				sqlReturnValue.isNullable = true;
				if (num2 == 65535)
				{
					sqlReturnValue.metaType = MetaType.GetMaxMetaTypeFromMetaType(sqlReturnValue.metaType);
				}
			}
			else
			{
				if (sqlReturnValue.metaType.NullableType == b4)
				{
					sqlReturnValue.isNullable = true;
				}
				sqlReturnValue.tdsType = b4;
			}
			if (sqlReturnValue.type == SqlDbType.Decimal)
			{
				if (!stateObj.TryReadByte(out sqlReturnValue.precision))
				{
					return false;
				}
				if (!stateObj.TryReadByte(out sqlReturnValue.scale))
				{
					return false;
				}
			}
			if (sqlReturnValue.metaType.IsVarTime && !stateObj.TryReadByte(out sqlReturnValue.scale))
			{
				return false;
			}
			if (b4 == 240 && !this.TryProcessUDTMetaData(sqlReturnValue, stateObj))
			{
				return false;
			}
			if (sqlReturnValue.type == SqlDbType.Xml)
			{
				byte b5;
				if (!stateObj.TryReadByte(out b5))
				{
					return false;
				}
				if ((b5 & 1) != 0)
				{
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					if (b != 0 && !stateObj.TryReadString((int)b, out sqlReturnValue.xmlSchemaCollectionDatabase))
					{
						return false;
					}
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					if (b != 0 && !stateObj.TryReadString((int)b, out sqlReturnValue.xmlSchemaCollectionOwningSchema))
					{
						return false;
					}
					short num3;
					if (!stateObj.TryReadInt16(out num3))
					{
						return false;
					}
					if (num3 != 0 && !stateObj.TryReadString((int)num3, out sqlReturnValue.xmlSchemaCollectionName))
					{
						return false;
					}
				}
			}
			else if (this._isShiloh && sqlReturnValue.metaType.IsCharType)
			{
				if (!this.TryProcessCollation(stateObj, out sqlReturnValue.collation))
				{
					return false;
				}
				int codePage = this.GetCodePage(sqlReturnValue.collation, stateObj);
				if (codePage == this._defaultCodePage)
				{
					sqlReturnValue.codePage = this._defaultCodePage;
					sqlReturnValue.encoding = this._defaultEncoding;
				}
				else
				{
					sqlReturnValue.codePage = codePage;
					sqlReturnValue.encoding = Encoding.GetEncoding(sqlReturnValue.codePage);
				}
			}
			if (this._serverSupportsColumnEncryption && sqlReturnValue.isEncrypted && !this.TryProcessTceCryptoMetadata(stateObj, sqlReturnValue, null, columnEncryptionSetting, true))
			{
				return false;
			}
			bool flag = false;
			ulong num4;
			if (!this.TryProcessColumnHeaderNoNBC(sqlReturnValue, stateObj, out flag, out num4))
			{
				return false;
			}
			int length2 = (num4 > 2147483647UL) ? int.MaxValue : ((int)num4);
			if (sqlReturnValue.metaType.IsPlp)
			{
				length2 = int.MaxValue;
			}
			if (flag)
			{
				TdsParser.GetNullSqlValue(sqlReturnValue.value, sqlReturnValue, SqlCommandColumnEncryptionSetting.Disabled, this._connHandler);
			}
			else if (!this.TryReadSqlValue(sqlReturnValue.value, sqlReturnValue, length2, stateObj, SqlCommandColumnEncryptionSetting.Disabled, null))
			{
				return false;
			}
			returnValue = sqlReturnValue;
			return true;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000E266C File Offset: 0x000E1A6C
		internal bool TryProcessTceCryptoMetadata(TdsParserStateObject stateObj, SqlMetaDataPriv col, SqlTceCipherInfoTable? cipherTable, SqlCommandColumnEncryptionSetting columnEncryptionSetting, bool isReturnValue)
		{
			ushort num = 0;
			if (cipherTable != null)
			{
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				if ((int)num >= cipherTable.Value.Size)
				{
					Bid.Trace("<sc.TdsParser.TryProcessTceCryptoMetadata|TCE> Incorrect ordinal received %d, max tab size: %d\n", (int)num, cipherTable.Value.Size);
					throw SQL.ParsingErrorValue(ParsingErrorState.TceInvalidOrdinalIntoCipherInfoTable, (int)num);
				}
			}
			uint userType;
			if (!stateObj.TryReadUInt32(out userType))
			{
				return false;
			}
			col.baseTI = new SqlMetaDataPriv();
			if (!this.TryProcessTypeInfo(stateObj, col.baseTI, userType))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			string cipherAlgorithmName = null;
			if (b == 0)
			{
				byte length;
				if (!stateObj.TryReadByte(out length))
				{
					return false;
				}
				if (!stateObj.TryReadString((int)length, out cipherAlgorithmName))
				{
					return false;
				}
			}
			byte encryptionType;
			if (!stateObj.TryReadByte(out encryptionType))
			{
				return false;
			}
			byte normalizationRuleVersion;
			if (!stateObj.TryReadByte(out normalizationRuleVersion))
			{
				return false;
			}
			if (columnEncryptionSetting == SqlCommandColumnEncryptionSetting.Enabled || (columnEncryptionSetting == SqlCommandColumnEncryptionSetting.ResultSetOnly && !isReturnValue) || (columnEncryptionSetting == SqlCommandColumnEncryptionSetting.UseConnectionSetting && this._connHandler != null && this._connHandler.ConnectionOptions != null && this._connHandler.ConnectionOptions.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled))
			{
				col.cipherMD = new SqlCipherMetadata((cipherTable != null) ? new SqlTceCipherInfoEntry?(cipherTable.Value[(int)num]) : null, num, b, cipherAlgorithmName, encryptionType, normalizationRuleVersion);
			}
			else
			{
				col.isEncrypted = false;
			}
			return true;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000E27B4 File Offset: 0x000E1BB4
		internal bool TryProcessCollation(TdsParserStateObject stateObj, out SqlCollation collation)
		{
			SqlCollation sqlCollation = new SqlCollation();
			if (!stateObj.TryReadUInt32(out sqlCollation.info))
			{
				collation = null;
				return false;
			}
			if (!stateObj.TryReadByte(out sqlCollation.sortId))
			{
				collation = null;
				return false;
			}
			collation = sqlCollation;
			return true;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000E27F4 File Offset: 0x000E1BF4
		private void WriteCollation(SqlCollation collation, TdsParserStateObject stateObj)
		{
			if (collation == null)
			{
				this._physicalStateObj.WriteByte(0);
				return;
			}
			this._physicalStateObj.WriteByte(5);
			this.WriteUnsignedInt(collation.info, this._physicalStateObj);
			this._physicalStateObj.WriteByte(collation.sortId);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000E2840 File Offset: 0x000E1C40
		internal int GetCodePage(SqlCollation collation, TdsParserStateObject stateObj)
		{
			int num = 0;
			if (collation.sortId != 0)
			{
				num = (int)TdsEnums.CODE_PAGE_FROM_SORT_ID[(int)collation.sortId];
			}
			else
			{
				int num2 = collation.LCID;
				bool flag = false;
				try
				{
					num = CultureInfo.GetCultureInfo(num2).TextInfo.ANSICodePage;
					flag = true;
				}
				catch (ArgumentException e)
				{
					ADP.TraceExceptionWithoutRethrow(e);
				}
				if (!flag || num == 0)
				{
					CultureInfo cultureInfo = null;
					if (num2 <= 66578)
					{
						if (num2 <= 2087)
						{
							if (num2 == 1087)
							{
								goto IL_EF;
							}
							if (num2 != 2087)
							{
								goto IL_FC;
							}
							goto IL_D4;
						}
						else if (num2 != 66564 && num2 - 66577 > 1)
						{
							goto IL_FC;
						}
					}
					else if (num2 <= 68612)
					{
						if (num2 != 67588 && num2 != 68612)
						{
							goto IL_FC;
						}
					}
					else if (num2 != 69636 && num2 != 70660)
					{
						goto IL_FC;
					}
					num2 &= 16383;
					try
					{
						cultureInfo = new CultureInfo(num2);
						flag = true;
						goto IL_FC;
					}
					catch (ArgumentException e2)
					{
						ADP.TraceExceptionWithoutRethrow(e2);
						goto IL_FC;
					}
					IL_D4:
					try
					{
						cultureInfo = new CultureInfo(1063);
						flag = true;
						goto IL_FC;
					}
					catch (ArgumentException e3)
					{
						ADP.TraceExceptionWithoutRethrow(e3);
						goto IL_FC;
					}
					IL_EF:
					if (!LocalAppContextSwitches.UseCultureInfoKazakhCodePage)
					{
						num = 1251;
					}
					IL_FC:
					if (!flag)
					{
						this.ThrowUnsupportedCollationEncountered(stateObj);
					}
					if (cultureInfo != null)
					{
						num = cultureInfo.TextInfo.ANSICodePage;
					}
				}
			}
			return num;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000E29B0 File Offset: 0x000E1DB0
		internal void DrainData(TdsParserStateObject stateObj)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				try
				{
					SqlDataReader.SharedState readerState = stateObj._readerState;
					if (readerState != null && readerState._dataReady)
					{
						_SqlMetaDataSet cleanupMetaData = stateObj._cleanupMetaData;
						if (stateObj._partialHeaderBytesRead > 0 && !stateObj.TryProcessHeader())
						{
							throw SQL.SynchronousCallMayNotPend();
						}
						if (readerState._nextColumnHeaderToRead == 0)
						{
							if (!stateObj.Parser.TrySkipRow(stateObj._cleanupMetaData, stateObj))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
						}
						else
						{
							if (readerState._nextColumnDataToRead < readerState._nextColumnHeaderToRead)
							{
								if (readerState._nextColumnHeaderToRead > 0 && cleanupMetaData[readerState._nextColumnHeaderToRead - 1].metaType.IsPlp)
								{
									ulong num;
									if (stateObj._longlen != 0UL && !this.TrySkipPlpValue(18446744073709551615UL, stateObj, out num))
									{
										throw SQL.SynchronousCallMayNotPend();
									}
								}
								else if (0L < readerState._columnDataBytesRemaining && !stateObj.TrySkipLongBytes(readerState._columnDataBytesRemaining))
								{
									throw SQL.SynchronousCallMayNotPend();
								}
							}
							if (!stateObj.Parser.TrySkipRow(cleanupMetaData, readerState._nextColumnHeaderToRead, stateObj))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
						}
					}
					this.Run(RunBehavior.Clean, null, null, null, stateObj);
				}
				catch
				{
					this._connHandler.DoomThisConnection();
					throw;
				}
			}
			catch (OutOfMemoryException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000E2B58 File Offset: 0x000E1F58
		internal void ThrowUnsupportedCollationEncountered(TdsParserStateObject stateObj)
		{
			stateObj.AddError(new SqlError(0, 0, 11, this._server, SQLMessage.CultureIdError(), "", 0));
			if (stateObj != null)
			{
				this.DrainData(stateObj);
				stateObj._pendingData = false;
			}
			this.ThrowExceptionAndWarning(stateObj, false, false);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000E2BA0 File Offset: 0x000E1FA0
		internal bool TryProcessAltMetaData(int cColumns, TdsParserStateObject stateObj, out _SqlMetaDataSet metaData)
		{
			metaData = null;
			_SqlMetaDataSet sqlMetaDataSet = new _SqlMetaDataSet(cColumns, null);
			int[] array = new int[cColumns];
			if (!stateObj.TryReadUInt16(out sqlMetaDataSet.id))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			while (b > 0)
			{
				if (!stateObj.TrySkipBytes(2))
				{
					return false;
				}
				b -= 1;
			}
			for (int i = 0; i < cColumns; i++)
			{
				_SqlMetaData sqlMetaData = sqlMetaDataSet[i];
				if (!stateObj.TryReadByte(out sqlMetaData.op))
				{
					return false;
				}
				if (!stateObj.TryReadUInt16(out sqlMetaData.operand))
				{
					return false;
				}
				if (!this.TryCommonProcessMetaData(stateObj, sqlMetaData, null, false, SqlCommandColumnEncryptionSetting.Disabled))
				{
					return false;
				}
				if (ADP.IsEmpty(sqlMetaData.column))
				{
					byte op = sqlMetaData.op;
					if (op != 9)
					{
						switch (op)
						{
						case 48:
							sqlMetaData.column = "stdev";
							break;
						case 49:
							sqlMetaData.column = "stdevp";
							break;
						case 50:
							sqlMetaData.column = "var";
							break;
						case 51:
							sqlMetaData.column = "varp";
							break;
						default:
							switch (op)
							{
							case 75:
								sqlMetaData.column = "cnt";
								break;
							case 77:
								sqlMetaData.column = "sum";
								break;
							case 79:
								sqlMetaData.column = "avg";
								break;
							case 81:
								sqlMetaData.column = "min";
								break;
							case 82:
								sqlMetaData.column = "max";
								break;
							case 83:
								sqlMetaData.column = "any";
								break;
							case 86:
								sqlMetaData.column = "noop";
								break;
							}
							break;
						}
					}
					else
					{
						sqlMetaData.column = "cntb";
					}
				}
				array[i] = i;
			}
			sqlMetaDataSet.indexMap = array;
			sqlMetaDataSet.visibleColumns = cColumns;
			metaData = sqlMetaDataSet;
			return true;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000E2D7C File Offset: 0x000E217C
		internal bool TryReadCipherInfoEntry(TdsParserStateObject stateObj, out SqlTceCipherInfoEntry entry)
		{
			byte b = 0;
			entry = new SqlTceCipherInfoEntry(0);
			int databaseId;
			if (!stateObj.TryReadInt32(out databaseId))
			{
				return false;
			}
			int cekId;
			if (!stateObj.TryReadInt32(out cekId))
			{
				return false;
			}
			int cekVersion;
			if (!stateObj.TryReadInt32(out cekVersion))
			{
				return false;
			}
			byte[] array = new byte[8];
			if (!stateObj.TryReadByteArray(array, 0, 8))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			for (int i = 0; i < (int)b; i++)
			{
				ushort num;
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				int num2 = (int)num;
				byte[] array2 = new byte[num2];
				if (!stateObj.TryReadByteArray(array2, 0, num2))
				{
					return false;
				}
				byte b2;
				if (!stateObj.TryReadByte(out b2))
				{
					return false;
				}
				num2 = (int)b2;
				string keyStoreName;
				if (!stateObj.TryReadString(num2, out keyStoreName))
				{
					return false;
				}
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				num2 = (int)num;
				string keyPath;
				if (!stateObj.TryReadString(num2, out keyPath))
				{
					return false;
				}
				byte b3;
				if (!stateObj.TryReadByte(out b3))
				{
					return false;
				}
				num2 = (int)b3;
				string algorithmName;
				if (!stateObj.TryReadString(num2, out algorithmName))
				{
					return false;
				}
				entry.Add(array2, databaseId, cekId, cekVersion, array, keyPath, keyStoreName, algorithmName);
			}
			return true;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000E2E7C File Offset: 0x000E227C
		internal bool TryProcessCipherInfoTable(TdsParserStateObject stateObj, out SqlTceCipherInfoTable? cipherTable)
		{
			short num = 0;
			cipherTable = null;
			if (!stateObj.TryReadInt16(out num))
			{
				return false;
			}
			if (num != 0)
			{
				SqlTceCipherInfoTable value = new SqlTceCipherInfoTable((int)num);
				for (int i = 0; i < (int)num; i++)
				{
					SqlTceCipherInfoEntry value2;
					if (!this.TryReadCipherInfoEntry(stateObj, out value2))
					{
						return false;
					}
					value[i] = value2;
				}
				cipherTable = new SqlTceCipherInfoTable?(value);
			}
			return true;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000E2ED8 File Offset: 0x000E22D8
		internal bool TryProcessMetaData(int cColumns, TdsParserStateObject stateObj, out _SqlMetaDataSet metaData, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			SqlTceCipherInfoTable? cipherTable = null;
			if (this._serverSupportsColumnEncryption && !this.TryProcessCipherInfoTable(stateObj, out cipherTable))
			{
				metaData = null;
				return false;
			}
			_SqlMetaDataSet sqlMetaDataSet = new _SqlMetaDataSet(cColumns, cipherTable);
			for (int i = 0; i < cColumns; i++)
			{
				if (!this.TryCommonProcessMetaData(stateObj, sqlMetaDataSet[i], cipherTable, true, columnEncryptionSetting))
				{
					metaData = null;
					return false;
				}
			}
			metaData = sqlMetaDataSet;
			return true;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000E2F38 File Offset: 0x000E2338
		private bool IsVarTimeTds(byte tdsType)
		{
			return tdsType == 41 || tdsType == 42 || tdsType == 43;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000E2F58 File Offset: 0x000E2358
		private bool TryProcessTypeInfo(TdsParserStateObject stateObj, SqlMetaDataPriv col, uint userType)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			if (b == 241)
			{
				col.length = 65535;
			}
			else if (this.IsVarTimeTds(b))
			{
				col.length = 0;
			}
			else if (b == 40)
			{
				col.length = 3;
			}
			else if (!this.TryGetTokenLength(b, stateObj, out col.length))
			{
				return false;
			}
			col.metaType = MetaType.GetSqlDataType((int)b, userType, col.length);
			col.type = col.metaType.SqlDbType;
			if (this._isShiloh)
			{
				col.tdsType = (col.isNullable ? col.metaType.NullableType : col.metaType.TDSType);
			}
			else
			{
				col.tdsType = b;
			}
			if (this._isYukon)
			{
				if (240 == b && !this.TryProcessUDTMetaData(col, stateObj))
				{
					return false;
				}
				if (col.length == 65535)
				{
					col.metaType = MetaType.GetMaxMetaTypeFromMetaType(col.metaType);
					col.length = int.MaxValue;
					if (b == 241)
					{
						byte b2;
						if (!stateObj.TryReadByte(out b2))
						{
							return false;
						}
						if ((b2 & 1) != 0)
						{
							byte b3;
							if (!stateObj.TryReadByte(out b3))
							{
								return false;
							}
							if (b3 != 0 && !stateObj.TryReadString((int)b3, out col.xmlSchemaCollectionDatabase))
							{
								return false;
							}
							if (!stateObj.TryReadByte(out b3))
							{
								return false;
							}
							if (b3 != 0 && !stateObj.TryReadString((int)b3, out col.xmlSchemaCollectionOwningSchema))
							{
								return false;
							}
							short length;
							if (!stateObj.TryReadInt16(out length))
							{
								return false;
							}
							if (b3 != 0 && !stateObj.TryReadString((int)length, out col.xmlSchemaCollectionName))
							{
								return false;
							}
						}
					}
				}
			}
			if (col.type == SqlDbType.Decimal)
			{
				if (!stateObj.TryReadByte(out col.precision))
				{
					return false;
				}
				if (!stateObj.TryReadByte(out col.scale))
				{
					return false;
				}
			}
			if (col.metaType.IsVarTime)
			{
				if (!stateObj.TryReadByte(out col.scale))
				{
					return false;
				}
				switch (col.metaType.SqlDbType)
				{
				case SqlDbType.Time:
					col.length = MetaType.GetTimeSizeFromScale(col.scale);
					break;
				case SqlDbType.DateTime2:
					col.length = 3 + MetaType.GetTimeSizeFromScale(col.scale);
					break;
				case SqlDbType.DateTimeOffset:
					col.length = 5 + MetaType.GetTimeSizeFromScale(col.scale);
					break;
				}
			}
			if (this._isShiloh && col.metaType.IsCharType && b != 241)
			{
				if (!this.TryProcessCollation(stateObj, out col.collation))
				{
					return false;
				}
				int codePage = this.GetCodePage(col.collation, stateObj);
				if (codePage == this._defaultCodePage)
				{
					col.codePage = this._defaultCodePage;
					col.encoding = this._defaultEncoding;
				}
				else
				{
					col.codePage = codePage;
					col.encoding = Encoding.GetEncoding(col.codePage);
				}
			}
			return true;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000E31F8 File Offset: 0x000E25F8
		private bool TryCommonProcessMetaData(TdsParserStateObject stateObj, _SqlMetaData col, SqlTceCipherInfoTable? cipherTable, bool fColMD, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			uint userType;
			if (this.IsYukonOrNewer)
			{
				if (!stateObj.TryReadUInt32(out userType))
				{
					return false;
				}
			}
			else
			{
				ushort num;
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				userType = (uint)num;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			col.updatability = (byte)((b & 11) >> 2);
			col.isNullable = (1 == (b & 1));
			col.isIdentity = (16 == (b & 16));
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			col.isColumnSet = (4 == (b & 4));
			if (fColMD && this._serverSupportsColumnEncryption)
			{
				col.isEncrypted = (8 == (b & 8));
			}
			if (!this.TryProcessTypeInfo(stateObj, col, userType))
			{
				return false;
			}
			if (col.metaType.IsLong && !col.metaType.IsPlp)
			{
				if (this._isYukon)
				{
					int num2 = 65535;
					if (!this.TryProcessOneTable(stateObj, ref num2, out col.multiPartTableName))
					{
						return false;
					}
				}
				else
				{
					ushort length;
					if (!stateObj.TryReadUInt16(out length))
					{
						return false;
					}
					string multipartName;
					if (!stateObj.TryReadString((int)length, out multipartName))
					{
						return false;
					}
					col.multiPartTableName = new MultiPartTableName(multipartName);
				}
			}
			if (fColMD && this._serverSupportsColumnEncryption && col.isEncrypted && cipherTable != null && !this.TryProcessTceCryptoMetadata(stateObj, col, new SqlTceCipherInfoTable?(cipherTable.Value), columnEncryptionSetting, false))
			{
				return false;
			}
			byte length2;
			if (!stateObj.TryReadByte(out length2))
			{
				return false;
			}
			if (!stateObj.TryReadString((int)length2, out col.column))
			{
				return false;
			}
			stateObj._receivedColMetaData = true;
			return true;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000E3358 File Offset: 0x000E2758
		private bool TryProcessUDTMetaData(SqlMetaDataPriv metaData, TdsParserStateObject stateObj)
		{
			ushort num;
			if (!stateObj.TryReadUInt16(out num))
			{
				return false;
			}
			metaData.length = (int)num;
			byte b;
			return stateObj.TryReadByte(out b) && (b == 0 || stateObj.TryReadString((int)b, out metaData.udtDatabaseName)) && stateObj.TryReadByte(out b) && (b == 0 || stateObj.TryReadString((int)b, out metaData.udtSchemaName)) && stateObj.TryReadByte(out b) && (b == 0 || stateObj.TryReadString((int)b, out metaData.udtTypeName)) && stateObj.TryReadUInt16(out num) && (num == 0 || stateObj.TryReadString((int)num, out metaData.udtAssemblyQualifiedName));
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000E33FC File Offset: 0x000E27FC
		private void WriteUDTMetaData(object value, string database, string schema, string type, TdsParserStateObject stateObj)
		{
			if (ADP.IsEmpty(database))
			{
				stateObj.WriteByte(0);
			}
			else
			{
				stateObj.WriteByte((byte)database.Length);
				this.WriteString(database, stateObj, true);
			}
			if (ADP.IsEmpty(schema))
			{
				stateObj.WriteByte(0);
			}
			else
			{
				stateObj.WriteByte((byte)schema.Length);
				this.WriteString(schema, stateObj, true);
			}
			if (ADP.IsEmpty(type))
			{
				stateObj.WriteByte(0);
				return;
			}
			stateObj.WriteByte((byte)type.Length);
			this.WriteString(type, stateObj, true);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000E348C File Offset: 0x000E288C
		internal bool TryProcessTableName(int length, TdsParserStateObject stateObj, out MultiPartTableName[] multiPartTableNames)
		{
			int num = 0;
			MultiPartTableName[] array = new MultiPartTableName[1];
			while (length > 0)
			{
				MultiPartTableName multiPartTableName;
				if (!this.TryProcessOneTable(stateObj, ref length, out multiPartTableName))
				{
					multiPartTableNames = null;
					return false;
				}
				if (num == 0)
				{
					array[num] = multiPartTableName;
				}
				else
				{
					MultiPartTableName[] array2 = new MultiPartTableName[array.Length + 1];
					Array.Copy(array, 0, array2, 0, array.Length);
					array2[array.Length] = multiPartTableName;
					array = array2;
				}
				num++;
			}
			multiPartTableNames = array;
			return true;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000E34F4 File Offset: 0x000E28F4
		private bool TryProcessOneTable(TdsParserStateObject stateObj, ref int length, out MultiPartTableName multiPartTableName)
		{
			multiPartTableName = default(MultiPartTableName);
			MultiPartTableName multiPartTableName2;
			if (this._isShilohSP1)
			{
				multiPartTableName2 = default(MultiPartTableName);
				byte b;
				if (!stateObj.TryReadByte(out b))
				{
					return false;
				}
				length--;
				if (b == 4)
				{
					ushort num;
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					length -= 2;
					string text;
					if (!stateObj.TryReadString((int)num, out text))
					{
						return false;
					}
					multiPartTableName2.ServerName = text;
					b -= 1;
					length -= (int)(num * 2);
				}
				if (b == 3)
				{
					ushort num;
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					length -= 2;
					string text;
					if (!stateObj.TryReadString((int)num, out text))
					{
						return false;
					}
					multiPartTableName2.CatalogName = text;
					length -= (int)(num * 2);
					b -= 1;
				}
				if (b == 2)
				{
					ushort num;
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					length -= 2;
					string text;
					if (!stateObj.TryReadString((int)num, out text))
					{
						return false;
					}
					multiPartTableName2.SchemaName = text;
					length -= (int)(num * 2);
					b -= 1;
				}
				if (b == 1)
				{
					ushort num;
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					length -= 2;
					string text;
					if (!stateObj.TryReadString((int)num, out text))
					{
						return false;
					}
					multiPartTableName2.TableName = text;
					length -= (int)(num * 2);
					b -= 1;
				}
			}
			else
			{
				ushort num;
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				length -= 2;
				string text;
				if (!stateObj.TryReadString((int)num, out text))
				{
					return false;
				}
				string name = text;
				length -= (int)(num * 2);
				multiPartTableName2 = new MultiPartTableName(MultipartIdentifier.ParseMultipartIdentifier(name, "[\"", "]\"", "SQL_TDSParserTableName", false));
			}
			multiPartTableName = multiPartTableName2;
			return true;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000E3664 File Offset: 0x000E2A64
		private bool TryProcessColInfo(_SqlMetaDataSet columns, SqlDataReader reader, TdsParserStateObject stateObj, out _SqlMetaDataSet metaData)
		{
			metaData = null;
			for (int i = 0; i < columns.Length; i++)
			{
				_SqlMetaData sqlMetaData = columns[i];
				byte b;
				if (!stateObj.TryReadByte(out b))
				{
					return false;
				}
				if (!stateObj.TryReadByte(out sqlMetaData.tableNum))
				{
					return false;
				}
				byte b2;
				if (!stateObj.TryReadByte(out b2))
				{
					return false;
				}
				sqlMetaData.isDifferentName = (32 == (b2 & 32));
				sqlMetaData.isExpression = (4 == (b2 & 4));
				sqlMetaData.isKey = (8 == (b2 & 8));
				sqlMetaData.isHidden = (16 == (b2 & 16));
				if (sqlMetaData.isDifferentName)
				{
					byte length;
					if (!stateObj.TryReadByte(out length))
					{
						return false;
					}
					if (!stateObj.TryReadString((int)length, out sqlMetaData.baseColumn))
					{
						return false;
					}
				}
				if (reader.TableNames != null && sqlMetaData.tableNum > 0)
				{
					sqlMetaData.multiPartTableName = reader.TableNames[(int)(sqlMetaData.tableNum - 1)];
				}
				if (sqlMetaData.isExpression)
				{
					sqlMetaData.updatability = 0;
				}
			}
			metaData = columns;
			return true;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000E3754 File Offset: 0x000E2B54
		internal bool TryProcessColumnHeader(SqlMetaDataPriv col, TdsParserStateObject stateObj, int columnOrdinal, out bool isNull, out ulong length)
		{
			if (stateObj.IsNullCompressionBitSet(columnOrdinal))
			{
				isNull = true;
				length = 0UL;
				return true;
			}
			return this.TryProcessColumnHeaderNoNBC(col, stateObj, out isNull, out length);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000E3784 File Offset: 0x000E2B84
		private bool TryProcessColumnHeaderNoNBC(SqlMetaDataPriv col, TdsParserStateObject stateObj, out bool isNull, out ulong length)
		{
			if (col.metaType.IsLong && !col.metaType.IsPlp)
			{
				byte b;
				if (!stateObj.TryReadByte(out b))
				{
					isNull = false;
					length = 0UL;
					return false;
				}
				if (b == 0)
				{
					isNull = true;
					length = 0UL;
					return true;
				}
				if (!stateObj.TrySkipBytes((int)b))
				{
					isNull = false;
					length = 0UL;
					return false;
				}
				if (!stateObj.TrySkipBytes(8))
				{
					isNull = false;
					length = 0UL;
					return false;
				}
				isNull = false;
				return this.TryGetDataLength(col, stateObj, out length);
			}
			else
			{
				ulong num;
				if (!this.TryGetDataLength(col, stateObj, out num))
				{
					isNull = false;
					length = 0UL;
					return false;
				}
				isNull = this.IsNull(col.metaType, num);
				length = (isNull ? 0UL : num);
				return true;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000E3834 File Offset: 0x000E2C34
		internal bool TryGetAltRowId(TdsParserStateObject stateObj, out int id)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				id = 0;
				return false;
			}
			if (!stateObj.TryStartNewRow(false, 0))
			{
				id = 0;
				return false;
			}
			ushort num;
			if (!stateObj.TryReadUInt16(out num))
			{
				id = 0;
				return false;
			}
			id = (int)num;
			return true;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000E3874 File Offset: 0x000E2C74
		private bool TryProcessRow(_SqlMetaDataSet columns, object[] buffer, int[] map, TdsParserStateObject stateObj)
		{
			SqlBuffer sqlBuffer = new SqlBuffer();
			for (int i = 0; i < columns.Length; i++)
			{
				_SqlMetaData sqlMetaData = columns[i];
				bool flag;
				ulong num;
				if (!this.TryProcessColumnHeader(sqlMetaData, stateObj, i, out flag, out num))
				{
					return false;
				}
				if (flag)
				{
					TdsParser.GetNullSqlValue(sqlBuffer, sqlMetaData, SqlCommandColumnEncryptionSetting.Disabled, this._connHandler);
					buffer[map[i]] = sqlBuffer.SqlValue;
				}
				else
				{
					if (!this.TryReadSqlValue(sqlBuffer, sqlMetaData, sqlMetaData.metaType.IsPlp ? 2147483647 : ((int)num), stateObj, SqlCommandColumnEncryptionSetting.Disabled, sqlMetaData.column))
					{
						return false;
					}
					buffer[map[i]] = sqlBuffer.SqlValue;
					if (stateObj._longlen != 0UL)
					{
						throw new SqlTruncateException(Res.GetString("SqlMisc_TruncationMaxDataMessage"));
					}
				}
				sqlBuffer.Clear();
			}
			return true;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000E3930 File Offset: 0x000E2D30
		internal static bool ShouldHonorTceForRead(SqlCommandColumnEncryptionSetting columnEncryptionSetting, SqlInternalConnectionTds connection)
		{
			switch (columnEncryptionSetting)
			{
			case SqlCommandColumnEncryptionSetting.Enabled:
				return true;
			case SqlCommandColumnEncryptionSetting.ResultSetOnly:
				return true;
			case SqlCommandColumnEncryptionSetting.Disabled:
				return false;
			default:
				return connection != null && connection.ConnectionOptions != null && connection.ConnectionOptions.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled;
			}
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000E3974 File Offset: 0x000E2D74
		internal static object GetNullSqlValue(SqlBuffer nullVal, SqlMetaDataPriv md, SqlCommandColumnEncryptionSetting columnEncryptionSetting, SqlInternalConnectionTds connection)
		{
			SqlDbType type = md.type;
			if (type == SqlDbType.VarBinary && md.isEncrypted && TdsParser.ShouldHonorTceForRead(columnEncryptionSetting, connection))
			{
				type = md.baseTI.type;
			}
			switch (type)
			{
			case SqlDbType.BigInt:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Int64);
				break;
			case SqlDbType.Binary:
			case SqlDbType.Image:
			case SqlDbType.VarBinary:
			case SqlDbType.Udt:
				nullVal.SqlBinary = SqlBinary.Null;
				break;
			case SqlDbType.Bit:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Boolean);
				break;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NText:
			case SqlDbType.NVarChar:
			case SqlDbType.Text:
			case SqlDbType.VarChar:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.String);
				break;
			case SqlDbType.DateTime:
			case SqlDbType.SmallDateTime:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.DateTime);
				break;
			case SqlDbType.Decimal:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Decimal);
				break;
			case SqlDbType.Float:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Double);
				break;
			case SqlDbType.Int:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Int32);
				break;
			case SqlDbType.Money:
			case SqlDbType.SmallMoney:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Money);
				break;
			case SqlDbType.Real:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Single);
				break;
			case SqlDbType.UniqueIdentifier:
				nullVal.SqlGuid = SqlGuid.Null;
				break;
			case SqlDbType.SmallInt:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Int16);
				break;
			case SqlDbType.TinyInt:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Byte);
				break;
			case SqlDbType.Variant:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Empty);
				break;
			case SqlDbType.Xml:
				nullVal.SqlCachedBuffer = SqlCachedBuffer.Null;
				break;
			case SqlDbType.Date:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Date);
				break;
			case SqlDbType.Time:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Time);
				break;
			case SqlDbType.DateTime2:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.DateTime2);
				break;
			case SqlDbType.DateTimeOffset:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.DateTimeOffset);
				break;
			}
			return nullVal;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000E3B14 File Offset: 0x000E2F14
		internal bool TrySkipRow(_SqlMetaDataSet columns, TdsParserStateObject stateObj)
		{
			return this.TrySkipRow(columns, 0, stateObj);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000E3B2C File Offset: 0x000E2F2C
		internal bool TrySkipRow(_SqlMetaDataSet columns, int startCol, TdsParserStateObject stateObj)
		{
			for (int i = startCol; i < columns.Length; i++)
			{
				_SqlMetaData md = columns[i];
				if (!this.TrySkipValue(md, i, stateObj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000E3B60 File Offset: 0x000E2F60
		internal bool TrySkipValue(SqlMetaDataPriv md, int columnOrdinal, TdsParserStateObject stateObj)
		{
			if (stateObj.IsNullCompressionBitSet(columnOrdinal))
			{
				return true;
			}
			if (md.metaType.IsPlp)
			{
				ulong num;
				if (!this.TrySkipPlpValue(18446744073709551615UL, stateObj, out num))
				{
					return false;
				}
			}
			else if (md.metaType.IsLong)
			{
				byte b;
				if (!stateObj.TryReadByte(out b))
				{
					return false;
				}
				if (b != 0)
				{
					if (!stateObj.TrySkipBytes((int)(b + 8)))
					{
						return false;
					}
					int num2;
					if (!this.TryGetTokenLength(md.tdsType, stateObj, out num2))
					{
						return false;
					}
					if (!stateObj.TrySkipBytes(num2))
					{
						return false;
					}
				}
			}
			else
			{
				int num3;
				if (!this.TryGetTokenLength(md.tdsType, stateObj, out num3))
				{
					return false;
				}
				if (!this.IsNull(md.metaType, (ulong)((long)num3)) && !stateObj.TrySkipBytes(num3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000E3C10 File Offset: 0x000E3010
		private bool IsNull(MetaType mt, ulong length)
		{
			if (mt.IsPlp)
			{
				return ulong.MaxValue == length;
			}
			return (65535UL == length && !mt.IsLong) || (length == 0UL && !mt.IsCharType && !mt.IsBinType);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000E3C54 File Offset: 0x000E3054
		private bool TryReadSqlStringValue(SqlBuffer value, byte type, int length, Encoding encoding, bool isPlp, TdsParserStateObject stateObj)
		{
			if (type <= 99)
			{
				if (type <= 39)
				{
					if (type != 35 && type != 39)
					{
						return true;
					}
				}
				else if (type != 47)
				{
					if (type != 99)
					{
						return true;
					}
					goto IL_7E;
				}
			}
			else if (type <= 175)
			{
				if (type != 167 && type != 175)
				{
					return true;
				}
			}
			else
			{
				if (type != 231 && type != 239)
				{
					return true;
				}
				goto IL_7E;
			}
			if (encoding == null)
			{
				encoding = this._defaultEncoding;
			}
			string toString;
			if (!stateObj.TryReadStringWithEncoding(length, encoding, isPlp, out toString))
			{
				return false;
			}
			value.SetToString(toString);
			return true;
			IL_7E:
			string toString2 = null;
			if (isPlp)
			{
				char[] value2 = null;
				if (!this.TryReadPlpUnicodeChars(ref value2, 0, length >> 1, stateObj, out length))
				{
					return false;
				}
				if (length > 0)
				{
					toString2 = new string(value2, 0, length);
				}
				else
				{
					toString2 = ADP.StrEmpty;
				}
			}
			else if (!stateObj.TryReadString(length >> 1, out toString2))
			{
				return false;
			}
			value.SetToString(toString2);
			return true;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000E3D2C File Offset: 0x000E312C
		internal bool DeserializeUnencryptedValue(SqlBuffer value, byte[] unencryptedBytes, SqlMetaDataPriv md, TdsParserStateObject stateObj, byte normalizationVersion)
		{
			if (normalizationVersion != 1)
			{
				throw SQL.UnsupportedNormalizationVersion(normalizationVersion);
			}
			byte tdsType = md.baseTI.tdsType;
			int num = unencryptedBytes.Length;
			int length = md.baseTI.length;
			byte scale = md.baseTI.scale;
			if (tdsType <= 165)
			{
				if (tdsType <= 111)
				{
					switch (tdsType)
					{
					case 34:
					case 37:
					case 45:
						goto IL_2D1;
					case 35:
					case 39:
					case 47:
						goto IL_37C;
					case 36:
						value.SqlGuid = new SqlGuid(unencryptedBytes, true);
						return true;
					case 38:
					case 48:
					case 50:
					case 52:
					case 56:
						goto IL_16F;
					case 40:
						value.SetToDate(unencryptedBytes);
						return true;
					case 41:
						value.SetToTime(unencryptedBytes, num, 7, scale);
						return true;
					case 42:
						value.SetToDateTime2(unencryptedBytes, num, 7, scale);
						return true;
					case 43:
						value.SetToDateTimeOffset(unencryptedBytes, num, 7, scale);
						return true;
					case 44:
					case 46:
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
						goto IL_442;
					case 58:
						goto IL_261;
					case 59:
						break;
					case 60:
						goto IL_22A;
					case 61:
						goto IL_296;
					case 62:
						goto IL_20C;
					default:
						switch (tdsType)
						{
						case 99:
							goto IL_3DB;
						case 100:
						case 101:
						case 102:
						case 103:
						case 105:
						case 107:
							goto IL_442;
						case 104:
							goto IL_16F;
						case 106:
						case 108:
						{
							int num2 = 0;
							byte b = unencryptedBytes[num2++];
							bool positive = 1 == b;
							int[] array;
							int num3;
							checked
							{
								num--;
								array = new int[4];
								num3 = num >> 2;
							}
							for (int i = 0; i < num3; i++)
							{
								array[i] = BitConverter.ToInt32(unencryptedBytes, num2);
								num2 += 4;
							}
							value.SetToDecimal(md.baseTI.precision, md.baseTI.scale, positive, array);
							return true;
						}
						case 109:
							if (num != 4)
							{
								goto IL_20C;
							}
							break;
						case 110:
							goto IL_22A;
						case 111:
							if (num == 4)
							{
								goto IL_261;
							}
							goto IL_296;
						default:
							goto IL_442;
						}
						break;
					}
					if (unencryptedBytes.Length != 4)
					{
						return false;
					}
					float single = BitConverter.ToSingle(unencryptedBytes, 0);
					value.Single = single;
					return true;
					IL_20C:
					if (unencryptedBytes.Length != 8)
					{
						return false;
					}
					double @double = BitConverter.ToDouble(unencryptedBytes, 0);
					value.Double = @double;
					return true;
					IL_261:
					if (unencryptedBytes.Length != 4)
					{
						return false;
					}
					ushort daypart = (ushort)(((int)unencryptedBytes[1] << 8) + (int)unencryptedBytes[0]);
					ushort num4 = (ushort)(((int)unencryptedBytes[3] << 8) + (int)unencryptedBytes[2]);
					value.SetToDateTime((int)daypart, (int)num4 * SqlDateTime.SQLTicksPerMinute);
					return true;
					IL_296:
					if (unencryptedBytes.Length != 8)
					{
						return false;
					}
					int daypart2 = BitConverter.ToInt32(unencryptedBytes, 0);
					uint timepart = BitConverter.ToUInt32(unencryptedBytes, 4);
					value.SetToDateTime(daypart2, (int)timepart);
					return true;
				}
				else
				{
					if (tdsType == 122)
					{
						goto IL_22A;
					}
					if (tdsType != 127)
					{
						if (tdsType != 165)
						{
							goto IL_442;
						}
						goto IL_2D1;
					}
				}
				IL_16F:
				if (unencryptedBytes.Length != 8)
				{
					return false;
				}
				long num5 = BitConverter.ToInt64(unencryptedBytes, 0);
				if (tdsType == 50 || tdsType == 104)
				{
					value.Boolean = (num5 != 0L);
					return true;
				}
				if (tdsType == 48 || length == 1)
				{
					value.Byte = (byte)num5;
					return true;
				}
				if (tdsType == 52 || length == 2)
				{
					value.Int16 = (short)num5;
					return true;
				}
				if (tdsType == 56 || length == 4)
				{
					value.Int32 = (int)num5;
					return true;
				}
				value.Int64 = num5;
				return true;
				IL_22A:
				if (unencryptedBytes.Length != 8)
				{
					return false;
				}
				int num6 = BitConverter.ToInt32(unencryptedBytes, 0);
				uint num7 = BitConverter.ToUInt32(unencryptedBytes, 4);
				long toMoney = ((long)num6 << 32) + (long)((ulong)num7);
				value.SetToMoney(toMoney);
				return true;
			}
			else if (tdsType <= 173)
			{
				if (tdsType == 167)
				{
					goto IL_37C;
				}
				if (tdsType != 173)
				{
					goto IL_442;
				}
			}
			else
			{
				if (tdsType == 175)
				{
					goto IL_37C;
				}
				if (tdsType != 231 && tdsType != 239)
				{
					goto IL_442;
				}
				goto IL_3DB;
			}
			IL_2D1:
			if (tdsType == 45 || tdsType == 173)
			{
				byte[] array2 = new byte[md.baseTI.length];
				Buffer.BlockCopy(unencryptedBytes, 0, array2, 0, unencryptedBytes.Length);
				unencryptedBytes = array2;
			}
			value.SqlBinary = new SqlBinary(unencryptedBytes, true);
			return true;
			IL_37C:
			Encoding encoding = md.baseTI.encoding;
			if (encoding == null)
			{
				encoding = this._defaultEncoding;
			}
			if (encoding == null)
			{
				this.ThrowUnsupportedCollationEncountered(stateObj);
			}
			string text = encoding.GetString(unencryptedBytes, 0, num);
			if (tdsType == 47 || tdsType == 175)
			{
				text = text.PadRight(md.baseTI.length);
			}
			value.SetToString(text);
			return true;
			IL_3DB:
			string text2 = Encoding.Unicode.GetString(unencryptedBytes, 0, num);
			if (tdsType == 239)
			{
				text2 = text2.PadRight(md.baseTI.length / 2);
			}
			value.SetToString(text2);
			return true;
			IL_442:
			MetaType metaType = md.baseTI.metaType;
			if (metaType == null)
			{
				metaType = MetaType.GetSqlDataType((int)tdsType, 0U, num);
			}
			throw SQL.UnsupportedDatatypeEncryption(metaType.TypeName);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000E41A4 File Offset: 0x000E35A4
		internal bool TryReadSqlValue(SqlBuffer value, SqlMetaDataPriv md, int length, TdsParserStateObject stateObj, SqlCommandColumnEncryptionSetting columnEncryptionOverride, string columnName)
		{
			bool isPlp = md.metaType.IsPlp;
			byte tdsType = md.tdsType;
			if (isPlp)
			{
				length = int.MaxValue;
			}
			if (tdsType <= 165)
			{
				if (tdsType <= 99)
				{
					switch (tdsType)
					{
					case 34:
					case 37:
					case 45:
						break;
					case 35:
					case 39:
					case 47:
						goto IL_1BE;
					case 36:
					case 38:
					case 44:
					case 46:
						goto IL_202;
					case 40:
					case 41:
					case 42:
					case 43:
						if (!this.TryReadSqlDateTime(value, tdsType, length, md.scale, stateObj))
						{
							return false;
						}
						return true;
					default:
						if (tdsType != 99)
						{
							goto IL_202;
						}
						goto IL_1BE;
					}
				}
				else if (tdsType != 106 && tdsType != 108)
				{
					if (tdsType != 165)
					{
						goto IL_202;
					}
				}
				else
				{
					if (!this.TryReadSqlDecimal(value, length, md.precision, md.scale, stateObj))
					{
						return false;
					}
					return true;
				}
			}
			else if (tdsType <= 173)
			{
				if (tdsType == 167)
				{
					goto IL_1BE;
				}
				if (tdsType != 173)
				{
					goto IL_202;
				}
			}
			else
			{
				if (tdsType == 175 || tdsType == 231)
				{
					goto IL_1BE;
				}
				switch (tdsType)
				{
				case 239:
					goto IL_1BE;
				case 240:
					break;
				case 241:
				{
					SqlCachedBuffer sqlCachedBuffer;
					if (!SqlCachedBuffer.TryCreate(md, this, stateObj, out sqlCachedBuffer))
					{
						return false;
					}
					value.SqlCachedBuffer = sqlCachedBuffer;
					return true;
				}
				default:
					goto IL_202;
				}
			}
			byte[] array = null;
			if (isPlp)
			{
				int num;
				if (!stateObj.TryReadPlpBytes(ref array, 0, length, out num))
				{
					return false;
				}
			}
			else
			{
				array = new byte[length];
				if (!stateObj.TryReadByteArray(array, 0, length))
				{
					return false;
				}
			}
			if (md.isEncrypted && (columnEncryptionOverride == SqlCommandColumnEncryptionSetting.Enabled || columnEncryptionOverride == SqlCommandColumnEncryptionSetting.ResultSetOnly || (columnEncryptionOverride == SqlCommandColumnEncryptionSetting.UseConnectionSetting && this._connHandler != null && this._connHandler.ConnectionOptions != null && this._connHandler.ConnectionOptions.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled)))
			{
				try
				{
					byte[] array2 = SqlSecurityUtility.DecryptWithKey(array, md.cipherMD, this._connHandler.ConnectionOptions.DataSource);
					if (array2 != null)
					{
						this.DeserializeUnencryptedValue(value, array2, md, stateObj, md.NormalizationRuleVersion);
					}
					return true;
				}
				catch (Exception e)
				{
					throw SQL.ColumnDecryptionFailed(columnName, null, e);
				}
			}
			value.SqlBinary = new SqlBinary(array, true);
			return true;
			IL_1BE:
			if (!this.TryReadSqlStringValue(value, tdsType, length, md.encoding, isPlp, stateObj))
			{
				return false;
			}
			return true;
			IL_202:
			if (!this.TryReadSqlValueInternal(value, tdsType, length, stateObj))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000E43E0 File Offset: 0x000E37E0
		private bool TryReadSqlDateTime(SqlBuffer value, byte tdsType, int length, byte scale, TdsParserStateObject stateObj)
		{
			byte[] array = new byte[length];
			if (!stateObj.TryReadByteArray(array, 0, length))
			{
				return false;
			}
			switch (tdsType)
			{
			case 40:
				value.SetToDate(array);
				break;
			case 41:
				value.SetToTime(array, length, scale, scale);
				break;
			case 42:
				value.SetToDateTime2(array, length, scale, scale);
				break;
			case 43:
				value.SetToDateTimeOffset(array, length, scale, scale);
				break;
			}
			return true;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000E4450 File Offset: 0x000E3850
		internal bool TryReadSqlValueInternal(SqlBuffer value, byte tdsType, int length, TdsParserStateObject stateObj)
		{
			if (tdsType <= 104)
			{
				byte b;
				if (tdsType <= 62)
				{
					switch (tdsType)
					{
					case 34:
					case 37:
						goto IL_272;
					case 35:
						return true;
					case 36:
					{
						byte[] array = new byte[length];
						if (!stateObj.TryReadByteArray(array, 0, length))
						{
							return false;
						}
						value.SqlGuid = new SqlGuid(array, true);
						return true;
					}
					case 38:
						if (length != 1)
						{
							if (length == 2)
							{
								goto IL_11F;
							}
							if (length == 4)
							{
								goto IL_139;
							}
							goto IL_152;
						}
						break;
					default:
						switch (tdsType)
						{
						case 45:
							goto IL_272;
						case 46:
						case 47:
						case 49:
						case 51:
						case 53:
						case 54:
						case 55:
						case 57:
							return true;
						case 48:
							break;
						case 50:
							goto IL_DC;
						case 52:
							goto IL_11F;
						case 56:
							goto IL_139;
						case 58:
							goto IL_1F9;
						case 59:
							goto IL_170;
						case 60:
							goto IL_1A8;
						case 61:
							goto IL_228;
						case 62:
							goto IL_18A;
						default:
							return true;
						}
						break;
					}
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					value.Byte = b;
					return true;
					IL_11F:
					short @int;
					if (!stateObj.TryReadInt16(out @int))
					{
						return false;
					}
					value.Int16 = @int;
					return true;
					IL_139:
					int num;
					if (!stateObj.TryReadInt32(out num))
					{
						return false;
					}
					value.Int32 = num;
					return true;
				}
				else if (tdsType != 98)
				{
					if (tdsType != 104)
					{
						return true;
					}
				}
				else
				{
					if (!this.TryReadSqlVariant(value, length, stateObj))
					{
						return false;
					}
					return true;
				}
				IL_DC:
				if (!stateObj.TryReadByte(out b))
				{
					return false;
				}
				value.Boolean = (b > 0);
				return true;
			}
			else if (tdsType <= 122)
			{
				switch (tdsType)
				{
				case 109:
					if (length == 4)
					{
						goto IL_170;
					}
					goto IL_18A;
				case 110:
					if (length != 4)
					{
						goto IL_1A8;
					}
					break;
				case 111:
					if (length == 4)
					{
						goto IL_1F9;
					}
					goto IL_228;
				default:
					if (tdsType != 122)
					{
						return true;
					}
					break;
				}
				int num;
				if (!stateObj.TryReadInt32(out num))
				{
					return false;
				}
				value.SetToMoney((long)num);
				return true;
			}
			else if (tdsType != 127)
			{
				if (tdsType != 165 && tdsType != 173)
				{
					return true;
				}
				goto IL_272;
			}
			IL_152:
			long int2;
			if (!stateObj.TryReadInt64(out int2))
			{
				return false;
			}
			value.Int64 = int2;
			return true;
			IL_170:
			float single;
			if (!stateObj.TryReadSingle(out single))
			{
				return false;
			}
			value.Single = single;
			return true;
			IL_18A:
			double @double;
			if (!stateObj.TryReadDouble(out @double))
			{
				return false;
			}
			value.Double = @double;
			return true;
			IL_1A8:
			int num2;
			if (!stateObj.TryReadInt32(out num2))
			{
				return false;
			}
			uint num3;
			if (!stateObj.TryReadUInt32(out num3))
			{
				return false;
			}
			long toMoney = ((long)num2 << 32) + (long)((ulong)num3);
			value.SetToMoney(toMoney);
			return true;
			IL_1F9:
			ushort daypart;
			if (!stateObj.TryReadUInt16(out daypart))
			{
				return false;
			}
			ushort num4;
			if (!stateObj.TryReadUInt16(out num4))
			{
				return false;
			}
			value.SetToDateTime((int)daypart, (int)num4 * SqlDateTime.SQLTicksPerMinute);
			return true;
			IL_228:
			int daypart2;
			if (!stateObj.TryReadInt32(out daypart2))
			{
				return false;
			}
			uint timepart;
			if (!stateObj.TryReadUInt32(out timepart))
			{
				return false;
			}
			value.SetToDateTime(daypart2, (int)timepart);
			return true;
			IL_272:
			byte[] array2 = new byte[length];
			if (!stateObj.TryReadByteArray(array2, 0, length))
			{
				return false;
			}
			value.SqlBinary = new SqlBinary(array2, true);
			return true;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000E4704 File Offset: 0x000E3B04
		internal bool TryReadSqlVariant(SqlBuffer value, int lenTotal, TdsParserStateObject stateObj)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			ushort num = 0;
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			MetaType sqlDataType = MetaType.GetSqlDataType((int)b, 0U, 0);
			byte propBytes = sqlDataType.PropBytes;
			int num2 = (int)(2 + b2);
			int length = lenTotal - num2;
			if (b <= 127)
			{
				if (b <= 106)
				{
					switch (b)
					{
					case 36:
					case 48:
					case 50:
					case 52:
					case 56:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
						goto IL_125;
					case 37:
					case 38:
					case 39:
					case 44:
					case 45:
					case 46:
					case 47:
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
						return true;
					case 40:
						if (!this.TryReadSqlDateTime(value, b, length, 0, stateObj))
						{
							return false;
						}
						return true;
					case 41:
					case 42:
					case 43:
					{
						byte scale;
						if (!stateObj.TryReadByte(out scale))
						{
							return false;
						}
						if (b2 > propBytes && !stateObj.TrySkipBytes((int)(b2 - propBytes)))
						{
							return false;
						}
						if (!this.TryReadSqlDateTime(value, b, length, scale, stateObj))
						{
							return false;
						}
						return true;
					}
					default:
						if (b != 106)
						{
							return true;
						}
						break;
					}
				}
				else if (b != 108)
				{
					if (b != 122 && b != 127)
					{
						return true;
					}
					goto IL_125;
				}
				byte precision;
				if (!stateObj.TryReadByte(out precision))
				{
					return false;
				}
				byte scale2;
				if (!stateObj.TryReadByte(out scale2))
				{
					return false;
				}
				if (b2 > propBytes && !stateObj.TrySkipBytes((int)(b2 - propBytes)))
				{
					return false;
				}
				if (!this.TryReadSqlDecimal(value, 17, precision, scale2, stateObj))
				{
					return false;
				}
				return true;
			}
			else
			{
				if (b <= 173)
				{
					if (b != 165)
					{
						if (b == 167)
						{
							goto IL_191;
						}
						if (b != 173)
						{
							return true;
						}
					}
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					if (b2 > propBytes && !stateObj.TrySkipBytes((int)(b2 - propBytes)))
					{
						return false;
					}
					goto IL_125;
				}
				else if (b != 175 && b != 231 && b != 239)
				{
					return true;
				}
				IL_191:
				SqlCollation collation;
				if (!this.TryProcessCollation(stateObj, out collation))
				{
					return false;
				}
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				if (b2 > propBytes && !stateObj.TrySkipBytes((int)(b2 - propBytes)))
				{
					return false;
				}
				Encoding encoding = Encoding.GetEncoding(this.GetCodePage(collation, stateObj));
				if (!this.TryReadSqlStringValue(value, b, length, encoding, false, stateObj))
				{
					return false;
				}
				return true;
			}
			IL_125:
			if (!this.TryReadSqlValueInternal(value, b, length, stateObj))
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000E492C File Offset: 0x000E3D2C
		internal Task WriteSqlVariantValue(object value, int length, int offset, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			if (ADP.IsNull(value))
			{
				this.WriteInt(0, stateObj);
				this.WriteInt(0, stateObj);
				return null;
			}
			MetaType metaTypeFromValue = MetaType.GetMetaTypeFromValue(value, true);
			if (108 == metaTypeFromValue.TDSType && 8 == length)
			{
				metaTypeFromValue = MetaType.GetMetaTypeFromValue(new SqlMoney((decimal)value), true);
			}
			if (metaTypeFromValue.IsAnsiType)
			{
				length = this.GetEncodingCharLength((string)value, length, 0, this._defaultEncoding);
			}
			this.WriteInt((int)(2 + metaTypeFromValue.PropBytes) + length, stateObj);
			this.WriteInt((int)(2 + metaTypeFromValue.PropBytes) + length, stateObj);
			stateObj.WriteByte(metaTypeFromValue.TDSType);
			stateObj.WriteByte(metaTypeFromValue.PropBytes);
			byte tdstype = metaTypeFromValue.TDSType;
			if (tdstype <= 62)
			{
				if (tdstype <= 41)
				{
					if (tdstype != 36)
					{
						if (tdstype == 41)
						{
							stateObj.WriteByte(metaTypeFromValue.Scale);
							this.WriteTime((TimeSpan)value, metaTypeFromValue.Scale, length, stateObj);
						}
					}
					else
					{
						byte[] b = ((Guid)value).ToByteArray();
						stateObj.WriteByteArray(b, length, 0, true, null);
					}
				}
				else if (tdstype != 43)
				{
					switch (tdstype)
					{
					case 48:
						stateObj.WriteByte((byte)value);
						break;
					case 50:
						if ((bool)value)
						{
							stateObj.WriteByte(1);
						}
						else
						{
							stateObj.WriteByte(0);
						}
						break;
					case 52:
						this.WriteShort((int)((short)value), stateObj);
						break;
					case 56:
						this.WriteInt((int)value, stateObj);
						break;
					case 59:
						this.WriteFloat((float)value, stateObj);
						break;
					case 60:
						this.WriteCurrency((decimal)value, 8, stateObj);
						break;
					case 61:
					{
						TdsDateTime tdsDateTime = MetaType.FromDateTime((DateTime)value, 8);
						this.WriteInt(tdsDateTime.days, stateObj);
						this.WriteInt(tdsDateTime.time, stateObj);
						break;
					}
					case 62:
						this.WriteDouble((double)value, stateObj);
						break;
					}
				}
				else
				{
					stateObj.WriteByte(metaTypeFromValue.Scale);
					this.WriteDateTimeOffset((DateTimeOffset)value, metaTypeFromValue.Scale, length, stateObj);
				}
			}
			else if (tdstype <= 127)
			{
				if (tdstype != 108)
				{
					if (tdstype == 127)
					{
						this.WriteLong((long)value, stateObj);
					}
				}
				else
				{
					stateObj.WriteByte(metaTypeFromValue.Precision);
					stateObj.WriteByte((byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16));
					this.WriteDecimal((decimal)value, stateObj);
				}
			}
			else
			{
				if (tdstype == 165)
				{
					byte[] b2 = (byte[])value;
					this.WriteShort(length, stateObj);
					return stateObj.WriteByteArray(b2, length, offset, canAccumulate, null);
				}
				if (tdstype == 167)
				{
					string s = (string)value;
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					this.WriteShort(length, stateObj);
					return this.WriteEncodingChar(s, this._defaultEncoding, stateObj, canAccumulate);
				}
				if (tdstype == 231)
				{
					string s2 = (string)value;
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					this.WriteShort(length, stateObj);
					length >>= 1;
					return this.WriteString(s2, length, offset, stateObj, canAccumulate);
				}
			}
			return null;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000E4CC0 File Offset: 0x000E40C0
		internal Task WriteSqlVariantDataRowValue(object value, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			if (value == null || DBNull.Value == value)
			{
				this.WriteInt(0, stateObj);
				return null;
			}
			MetaType metaTypeFromValue = MetaType.GetMetaTypeFromValue(value, true);
			int num = 0;
			if (metaTypeFromValue.IsAnsiType)
			{
				num = this.GetEncodingCharLength((string)value, num, 0, this._defaultEncoding);
			}
			byte tdstype = metaTypeFromValue.TDSType;
			if (tdstype <= 62)
			{
				if (tdstype <= 41)
				{
					if (tdstype != 36)
					{
						if (tdstype == 41)
						{
							this.WriteSqlVariantHeader(8, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
							stateObj.WriteByte(metaTypeFromValue.Scale);
							this.WriteTime((TimeSpan)value, metaTypeFromValue.Scale, 5, stateObj);
						}
					}
					else
					{
						byte[] array = ((Guid)value).ToByteArray();
						num = array.Length;
						this.WriteSqlVariantHeader(18, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						stateObj.WriteByteArray(array, num, 0, true, null);
					}
				}
				else if (tdstype != 43)
				{
					switch (tdstype)
					{
					case 48:
						this.WriteSqlVariantHeader(3, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						stateObj.WriteByte((byte)value);
						break;
					case 50:
						this.WriteSqlVariantHeader(3, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						if ((bool)value)
						{
							stateObj.WriteByte(1);
						}
						else
						{
							stateObj.WriteByte(0);
						}
						break;
					case 52:
						this.WriteSqlVariantHeader(4, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteShort((int)((short)value), stateObj);
						break;
					case 56:
						this.WriteSqlVariantHeader(6, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteInt((int)value, stateObj);
						break;
					case 59:
						this.WriteSqlVariantHeader(6, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteFloat((float)value, stateObj);
						break;
					case 60:
						this.WriteSqlVariantHeader(10, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteCurrency((decimal)value, 8, stateObj);
						break;
					case 61:
					{
						TdsDateTime tdsDateTime = MetaType.FromDateTime((DateTime)value, 8);
						this.WriteSqlVariantHeader(10, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteInt(tdsDateTime.days, stateObj);
						this.WriteInt(tdsDateTime.time, stateObj);
						break;
					}
					case 62:
						this.WriteSqlVariantHeader(10, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteDouble((double)value, stateObj);
						break;
					}
				}
				else
				{
					this.WriteSqlVariantHeader(13, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					stateObj.WriteByte(metaTypeFromValue.Scale);
					this.WriteDateTimeOffset((DateTimeOffset)value, metaTypeFromValue.Scale, 10, stateObj);
				}
			}
			else if (tdstype <= 127)
			{
				if (tdstype != 108)
				{
					if (tdstype == 127)
					{
						this.WriteSqlVariantHeader(10, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteLong((long)value, stateObj);
					}
				}
				else
				{
					this.WriteSqlVariantHeader(21, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					stateObj.WriteByte(metaTypeFromValue.Precision);
					stateObj.WriteByte((byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16));
					this.WriteDecimal((decimal)value, stateObj);
				}
			}
			else
			{
				if (tdstype == 165)
				{
					byte[] array2 = (byte[])value;
					num = array2.Length;
					this.WriteSqlVariantHeader(4 + num, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					this.WriteShort(num, stateObj);
					return stateObj.WriteByteArray(array2, num, 0, canAccumulate, null);
				}
				if (tdstype == 167)
				{
					string text = (string)value;
					num = text.Length;
					this.WriteSqlVariantHeader(9 + num, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					this.WriteShort(num, stateObj);
					return this.WriteEncodingChar(text, this._defaultEncoding, stateObj, canAccumulate);
				}
				if (tdstype == 231)
				{
					string text2 = (string)value;
					num = text2.Length * 2;
					this.WriteSqlVariantHeader(9 + num, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					this.WriteShort(num, stateObj);
					num >>= 1;
					return this.WriteString(text2, num, 0, stateObj, canAccumulate);
				}
			}
			return null;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000E5138 File Offset: 0x000E4538
		internal void WriteSqlVariantHeader(int length, byte tdstype, byte propbytes, TdsParserStateObject stateObj)
		{
			this.WriteInt(length, stateObj);
			stateObj.WriteByte(tdstype);
			stateObj.WriteByte(propbytes);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000E5160 File Offset: 0x000E4560
		internal void WriteSqlVariantDateTime2(DateTime value, TdsParserStateObject stateObj)
		{
			SmiMetaData defaultDateTime = SmiMetaData.DefaultDateTime2;
			this.WriteSqlVariantHeader((int)(defaultDateTime.MaxLength + 3L), 42, 1, stateObj);
			stateObj.WriteByte(defaultDateTime.Scale);
			this.WriteDateTime2(value, defaultDateTime.Scale, (int)defaultDateTime.MaxLength, stateObj);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000E51A8 File Offset: 0x000E45A8
		internal void WriteSqlVariantDate(DateTime value, TdsParserStateObject stateObj)
		{
			SmiMetaData defaultDate = SmiMetaData.DefaultDate;
			this.WriteSqlVariantHeader((int)(defaultDate.MaxLength + 2L), 40, 0, stateObj);
			this.WriteDate(value, stateObj);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000E51D8 File Offset: 0x000E45D8
		private byte[] SerializeSqlMoney(SqlMoney value, int length, TdsParserStateObject stateObj)
		{
			return this.SerializeCurrency(value.Value, length, stateObj);
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000E51F4 File Offset: 0x000E45F4
		private void WriteSqlMoney(SqlMoney value, int length, TdsParserStateObject stateObj)
		{
			int[] bits = decimal.GetBits(value.Value);
			bool flag = (bits[3] & int.MinValue) != 0;
			long num = (long)((ulong)bits[1] << 32 | (ulong)bits[0]);
			if (flag)
			{
				num = -num;
			}
			if (length != 4)
			{
				this.WriteInt((int)(num >> 32), stateObj);
				this.WriteInt((int)num, stateObj);
				return;
			}
			decimal value2 = value.Value;
			if (value2 < TdsEnums.SQL_SMALL_MONEY_MIN || value2 > TdsEnums.SQL_SMALL_MONEY_MAX)
			{
				throw SQL.MoneyOverflow(value2.ToString(CultureInfo.InvariantCulture));
			}
			this.WriteInt((int)num, stateObj);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000E5288 File Offset: 0x000E4688
		private byte[] SerializeCurrency(decimal value, int length, TdsParserStateObject stateObj)
		{
			SqlMoney sqlMoney = new SqlMoney(value);
			int[] bits = decimal.GetBits(sqlMoney.Value);
			bool flag = (bits[3] & int.MinValue) != 0;
			long num = (long)((ulong)bits[1] << 32 | (ulong)bits[0]);
			if (flag)
			{
				num = -num;
			}
			if (length == 4)
			{
				if (value < TdsEnums.SQL_SMALL_MONEY_MIN || value > TdsEnums.SQL_SMALL_MONEY_MAX)
				{
					throw SQL.MoneyOverflow(value.ToString(CultureInfo.InvariantCulture));
				}
				length = 8;
			}
			if (stateObj._bLongBytes == null)
			{
				stateObj._bLongBytes = new byte[8];
			}
			byte[] bLongBytes = stateObj._bLongBytes;
			int num2 = 0;
			byte[] src = this.SerializeInt((int)(num >> 32), stateObj);
			Buffer.BlockCopy(src, 0, bLongBytes, num2, 4);
			num2 += 4;
			src = this.SerializeInt((int)num, stateObj);
			Buffer.BlockCopy(src, 0, bLongBytes, num2, 4);
			return bLongBytes;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000E5350 File Offset: 0x000E4750
		private void WriteCurrency(decimal value, int length, TdsParserStateObject stateObj)
		{
			SqlMoney sqlMoney = new SqlMoney(value);
			int[] bits = decimal.GetBits(sqlMoney.Value);
			bool flag = (bits[3] & int.MinValue) != 0;
			long num = (long)((ulong)bits[1] << 32 | (ulong)bits[0]);
			if (flag)
			{
				num = -num;
			}
			if (length != 4)
			{
				this.WriteInt((int)(num >> 32), stateObj);
				this.WriteInt((int)num, stateObj);
				return;
			}
			if (value < TdsEnums.SQL_SMALL_MONEY_MIN || value > TdsEnums.SQL_SMALL_MONEY_MAX)
			{
				throw SQL.MoneyOverflow(value.ToString(CultureInfo.InvariantCulture));
			}
			this.WriteInt((int)num, stateObj);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000E53E4 File Offset: 0x000E47E4
		private byte[] SerializeDate(DateTime value)
		{
			long v = (long)value.Subtract(DateTime.MinValue).Days;
			return this.SerializePartialLong(v, 3);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000E5410 File Offset: 0x000E4810
		private void WriteDate(DateTime value, TdsParserStateObject stateObj)
		{
			long v = (long)value.Subtract(DateTime.MinValue).Days;
			this.WritePartialLong(v, 3, stateObj);
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000E543C File Offset: 0x000E483C
		private byte[] SerializeTime(TimeSpan value, byte scale, int length)
		{
			if (0L > value.Ticks || value.Ticks >= 864000000000L)
			{
				throw SQL.TimeOverflow(value.ToString());
			}
			long num = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			num *= TdsEnums.TICKS_FROM_SCALE[(int)scale];
			length = 5;
			return this.SerializePartialLong(num, length);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000E54A0 File Offset: 0x000E48A0
		private void WriteTime(TimeSpan value, byte scale, int length, TdsParserStateObject stateObj)
		{
			if (0L > value.Ticks || value.Ticks >= 864000000000L)
			{
				throw SQL.TimeOverflow(value.ToString());
			}
			long v = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			this.WritePartialLong(v, length, stateObj);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000E54F8 File Offset: 0x000E48F8
		private byte[] SerializeDateTime2(DateTime value, byte scale, int length)
		{
			long num = value.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			num *= TdsEnums.TICKS_FROM_SCALE[(int)scale];
			length = 8;
			byte[] array = new byte[length];
			int num2 = 0;
			byte[] src = this.SerializePartialLong(num, length - 3);
			Buffer.BlockCopy(src, 0, array, num2, length - 3);
			num2 += length - 3;
			src = this.SerializeDate(value);
			Buffer.BlockCopy(src, 0, array, num2, 3);
			return array;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000E5564 File Offset: 0x000E4964
		private void WriteDateTime2(DateTime value, byte scale, int length, TdsParserStateObject stateObj)
		{
			long v = value.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			this.WritePartialLong(v, length - 3, stateObj);
			this.WriteDate(value, stateObj);
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000E55A0 File Offset: 0x000E49A0
		private byte[] SerializeDateTimeOffset(DateTimeOffset value, byte scale, int length)
		{
			int num = 0;
			byte[] array = this.SerializeDateTime2(value.UtcDateTime, scale, length - 2);
			length = array.Length + 2;
			byte[] array2 = new byte[length];
			Buffer.BlockCopy(array, 0, array2, num, length - 2);
			num += length - 2;
			short num2 = (short)value.Offset.TotalMinutes;
			array2[num++] = (byte)(num2 & 255);
			array2[num++] = (byte)(num2 >> 8 & 255);
			return array2;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000E5614 File Offset: 0x000E4A14
		private void WriteDateTimeOffset(DateTimeOffset value, byte scale, int length, TdsParserStateObject stateObj)
		{
			this.WriteDateTime2(value.UtcDateTime, scale, length - 2, stateObj);
			short num = (short)value.Offset.TotalMinutes;
			stateObj.WriteByte((byte)(num & 255));
			stateObj.WriteByte((byte)(num >> 8 & 255));
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000E5668 File Offset: 0x000E4A68
		private bool TryReadSqlDecimal(SqlBuffer value, int length, byte precision, byte scale, TdsParserStateObject stateObj)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			bool positive = 1 == b;
			checked
			{
				length--;
				int[] bits;
				if (!this.TryReadDecimalBits(length, stateObj, out bits))
				{
					return false;
				}
				value.SetToDecimal(precision, scale, positive, bits);
				return true;
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000E56A8 File Offset: 0x000E4AA8
		private bool TryReadDecimalBits(int length, TdsParserStateObject stateObj, out int[] bits)
		{
			bits = stateObj._decimalBits;
			if (bits == null)
			{
				bits = new int[4];
			}
			else
			{
				for (int i = 0; i < bits.Length; i++)
				{
					bits[i] = 0;
				}
			}
			int num = length >> 2;
			for (int i = 0; i < num; i++)
			{
				if (!stateObj.TryReadInt32(out bits[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000E5704 File Offset: 0x000E4B04
		internal static SqlDecimal AdjustSqlDecimalScale(SqlDecimal d, int newScale)
		{
			if ((int)d.Scale != newScale)
			{
				return SqlDecimal.AdjustScale(d, newScale - (int)d.Scale, false);
			}
			return d;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000E5730 File Offset: 0x000E4B30
		internal static decimal AdjustDecimalScale(decimal value, int newScale)
		{
			int num = (decimal.GetBits(value)[3] & 16711680) >> 16;
			if (newScale != num)
			{
				SqlDecimal n = new SqlDecimal(value);
				n = SqlDecimal.AdjustScale(n, newScale - num, false);
				return n.Value;
			}
			return value;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000E5770 File Offset: 0x000E4B70
		internal byte[] SerializeSqlDecimal(SqlDecimal d, TdsParserStateObject stateObj)
		{
			if (stateObj._bDecimalBytes == null)
			{
				stateObj._bDecimalBytes = new byte[17];
			}
			byte[] bDecimalBytes = stateObj._bDecimalBytes;
			int num = 0;
			if (d.IsPositive)
			{
				bDecimalBytes[num++] = 1;
			}
			else
			{
				bDecimalBytes[num++] = 0;
			}
			byte[] src = this.SerializeUnsignedInt(d.m_data1, stateObj);
			Buffer.BlockCopy(src, 0, bDecimalBytes, num, 4);
			num += 4;
			src = this.SerializeUnsignedInt(d.m_data2, stateObj);
			Buffer.BlockCopy(src, 0, bDecimalBytes, num, 4);
			num += 4;
			src = this.SerializeUnsignedInt(d.m_data3, stateObj);
			Buffer.BlockCopy(src, 0, bDecimalBytes, num, 4);
			num += 4;
			src = this.SerializeUnsignedInt(d.m_data4, stateObj);
			Buffer.BlockCopy(src, 0, bDecimalBytes, num, 4);
			return bDecimalBytes;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000E5824 File Offset: 0x000E4C24
		internal void WriteSqlDecimal(SqlDecimal d, TdsParserStateObject stateObj)
		{
			if (d.IsPositive)
			{
				stateObj.WriteByte(1);
			}
			else
			{
				stateObj.WriteByte(0);
			}
			this.WriteUnsignedInt(d.m_data1, stateObj);
			this.WriteUnsignedInt(d.m_data2, stateObj);
			this.WriteUnsignedInt(d.m_data3, stateObj);
			this.WriteUnsignedInt(d.m_data4, stateObj);
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000E5880 File Offset: 0x000E4C80
		private byte[] SerializeDecimal(decimal value, TdsParserStateObject stateObj)
		{
			int[] bits = decimal.GetBits(value);
			if (stateObj._bDecimalBytes == null)
			{
				stateObj._bDecimalBytes = new byte[17];
			}
			byte[] bDecimalBytes = stateObj._bDecimalBytes;
			int num = 0;
			if ((ulong)-2147483648 == (ulong)((long)bits[3] & (long)((ulong)-2147483648)))
			{
				bDecimalBytes[num++] = 0;
			}
			else
			{
				bDecimalBytes[num++] = 1;
			}
			byte[] src = this.SerializeInt(bits[0], stateObj);
			Buffer.BlockCopy(src, 0, bDecimalBytes, num, 4);
			num += 4;
			src = this.SerializeInt(bits[1], stateObj);
			Buffer.BlockCopy(src, 0, bDecimalBytes, num, 4);
			num += 4;
			src = this.SerializeInt(bits[2], stateObj);
			Buffer.BlockCopy(src, 0, bDecimalBytes, num, 4);
			num += 4;
			src = this.SerializeInt(0, stateObj);
			Buffer.BlockCopy(src, 0, bDecimalBytes, num, 4);
			return bDecimalBytes;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000E5938 File Offset: 0x000E4D38
		private void WriteDecimal(decimal value, TdsParserStateObject stateObj)
		{
			stateObj._decimalBits = decimal.GetBits(value);
			if ((ulong)-2147483648 == (ulong)((long)stateObj._decimalBits[3] & (long)((ulong)-2147483648)))
			{
				stateObj.WriteByte(0);
			}
			else
			{
				stateObj.WriteByte(1);
			}
			this.WriteInt(stateObj._decimalBits[0], stateObj);
			this.WriteInt(stateObj._decimalBits[1], stateObj);
			this.WriteInt(stateObj._decimalBits[2], stateObj);
			this.WriteInt(0, stateObj);
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000E59B0 File Offset: 0x000E4DB0
		private void WriteIdentifier(string s, TdsParserStateObject stateObj)
		{
			if (s != null)
			{
				stateObj.WriteByte(checked((byte)s.Length));
				this.WriteString(s, stateObj, true);
				return;
			}
			stateObj.WriteByte(0);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000E59E0 File Offset: 0x000E4DE0
		private void WriteIdentifierWithShortLength(string s, TdsParserStateObject stateObj)
		{
			if (s != null)
			{
				this.WriteShort((int)(checked((short)s.Length)), stateObj);
				this.WriteString(s, stateObj, true);
				return;
			}
			this.WriteShort(0, stateObj);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000E5A14 File Offset: 0x000E4E14
		private Task WriteString(string s, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			return this.WriteString(s, s.Length, 0, stateObj, canAccumulate);
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000E5A34 File Offset: 0x000E4E34
		internal byte[] SerializeCharArray(char[] carr, int length, int offset)
		{
			int num = 2 * length;
			byte[] array = new byte[num];
			TdsParser.CopyCharsToBytes(carr, offset, array, 0, length);
			return array;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000E5A58 File Offset: 0x000E4E58
		internal Task WriteCharArray(char[] carr, int length, int offset, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			int num = 2 * length;
			if (num < stateObj._outBuff.Length - stateObj._outBytesUsed)
			{
				TdsParser.CopyCharsToBytes(carr, offset, stateObj._outBuff, stateObj._outBytesUsed, length);
				stateObj._outBytesUsed += num;
				return null;
			}
			if (stateObj._bTmp == null || stateObj._bTmp.Length < num)
			{
				stateObj._bTmp = new byte[num];
			}
			TdsParser.CopyCharsToBytes(carr, offset, stateObj._bTmp, 0, length);
			return stateObj.WriteByteArray(stateObj._bTmp, num, 0, canAccumulate, null);
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000E5AEC File Offset: 0x000E4EEC
		internal byte[] SerializeString(string s, int length, int offset)
		{
			int num = 2 * length;
			byte[] array = new byte[num];
			TdsParser.CopyStringToBytes(s, offset, array, 0, length);
			return array;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000E5B10 File Offset: 0x000E4F10
		internal Task WriteString(string s, int length, int offset, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			int num = 2 * length;
			if (num < stateObj._outBuff.Length - stateObj._outBytesUsed)
			{
				TdsParser.CopyStringToBytes(s, offset, stateObj._outBuff, stateObj._outBytesUsed, length);
				stateObj._outBytesUsed += num;
				return null;
			}
			if (stateObj._bTmp == null || stateObj._bTmp.Length < num)
			{
				stateObj._bTmp = new byte[num];
			}
			TdsParser.CopyStringToBytes(s, offset, stateObj._bTmp, 0, length);
			return stateObj.WriteByteArray(stateObj._bTmp, num, 0, canAccumulate, null);
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000E5BA4 File Offset: 0x000E4FA4
		private unsafe static void CopyCharsToBytes(char[] source, int sourceOffset, byte[] dest, int destOffset, int charLength)
		{
			if (charLength < 0)
			{
				throw ADP.InvalidDataLength((long)charLength);
			}
			int num;
			checked
			{
				if (sourceOffset + charLength > source.Length || sourceOffset < 0)
				{
					throw ADP.IndexOutOfRange(sourceOffset);
				}
				num = charLength * 2;
				if (destOffset + num > dest.Length || destOffset < 0)
				{
					throw ADP.IndexOutOfRange(destOffset);
				}
			}
			fixed (char[] array = source)
			{
				char* ptr;
				if (source == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				char* ptr2 = ptr;
				ptr2 += sourceOffset;
				fixed (byte[] array2 = dest)
				{
					byte* ptr3;
					if (dest == null || array2.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array2[0];
					}
					byte* ptr4 = ptr3;
					ptr4 += destOffset;
					NativeOledbWrapper.MemoryCopy((IntPtr)((void*)ptr4), (IntPtr)((void*)ptr2), num);
				}
			}
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000E5C4C File Offset: 0x000E504C
		private unsafe static void CopyStringToBytes(string source, int sourceOffset, byte[] dest, int destOffset, int charLength)
		{
			if (charLength < 0)
			{
				throw ADP.InvalidDataLength((long)charLength);
			}
			int num;
			checked
			{
				if (sourceOffset + charLength > source.Length || sourceOffset < 0)
				{
					throw ADP.IndexOutOfRange(sourceOffset);
				}
				num = charLength * 2;
				if (destOffset + num > dest.Length || destOffset < 0)
				{
					throw ADP.IndexOutOfRange(destOffset);
				}
			}
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				ptr2 += sourceOffset;
				fixed (byte[] array = dest)
				{
					byte* ptr3;
					if (dest == null || array.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array[0];
					}
					byte* ptr4 = ptr3;
					ptr4 += destOffset;
					NativeOledbWrapper.MemoryCopy((IntPtr)((void*)ptr4), (IntPtr)((void*)ptr2), num);
				}
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000E5CEC File Offset: 0x000E50EC
		private Task WriteEncodingChar(string s, Encoding encoding, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			return this.WriteEncodingChar(s, s.Length, 0, encoding, stateObj, canAccumulate);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000E5D0C File Offset: 0x000E510C
		private byte[] SerializeEncodingChar(string s, int numChars, int offset, Encoding encoding)
		{
			if (encoding == null)
			{
				encoding = this._defaultEncoding;
			}
			char[] array = s.ToCharArray(offset, numChars);
			byte[] array2 = new byte[encoding.GetByteCount(array, 0, array.Length)];
			encoding.GetBytes(array, 0, array.Length, array2, 0);
			return array2;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000E5D54 File Offset: 0x000E5154
		private Task WriteEncodingChar(string s, int numChars, int offset, Encoding encoding, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			if (encoding == null)
			{
				encoding = this._defaultEncoding;
			}
			char[] array = s.ToCharArray(offset, numChars);
			int num = stateObj._outBuff.Length - stateObj._outBytesUsed;
			if (numChars <= num && encoding.GetMaxByteCount(array.Length) <= num)
			{
				int bytes = encoding.GetBytes(array, 0, array.Length, stateObj._outBuff, stateObj._outBytesUsed);
				stateObj._outBytesUsed += bytes;
				return null;
			}
			byte[] bytes2 = encoding.GetBytes(array, 0, numChars);
			return stateObj.WriteByteArray(bytes2, bytes2.Length, 0, canAccumulate, null);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000E5DE0 File Offset: 0x000E51E0
		internal int GetEncodingCharLength(string value, int numChars, int charOffset, Encoding encoding)
		{
			if (value == null || value == ADP.StrEmpty)
			{
				return 0;
			}
			if (encoding == null)
			{
				if (this._defaultEncoding == null)
				{
					this.ThrowUnsupportedCollationEncountered(null);
				}
				encoding = this._defaultEncoding;
			}
			char[] chars = value.ToCharArray(charOffset, numChars);
			return encoding.GetByteCount(chars, 0, numChars);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000E5E30 File Offset: 0x000E5230
		internal bool TryGetDataLength(SqlMetaDataPriv colmeta, TdsParserStateObject stateObj, out ulong length)
		{
			if (this._isYukon && colmeta.metaType.IsPlp)
			{
				return stateObj.TryReadPlpLength(true, out length);
			}
			int num;
			if (!this.TryGetTokenLength(colmeta.tdsType, stateObj, out num))
			{
				length = 0UL;
				return false;
			}
			length = (ulong)((long)num);
			return true;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000E5E78 File Offset: 0x000E5278
		internal bool TryGetTokenLength(byte token, TdsParserStateObject stateObj, out int tokenLength)
		{
			if (token == 174)
			{
				tokenLength = -1;
				return true;
			}
			if (token != 228 && token != 238)
			{
				if (this._isYukon)
				{
					if (token == 240)
					{
						tokenLength = -1;
						return true;
					}
					if (token == 172)
					{
						tokenLength = -1;
						return true;
					}
					if (token == 241)
					{
						ushort num;
						if (!stateObj.TryReadUInt16(out num))
						{
							tokenLength = 0;
							return false;
						}
						tokenLength = (int)num;
						return true;
					}
				}
				int num2 = (int)(token & 48);
				if (num2 <= 16)
				{
					if (num2 != 0)
					{
						if (num2 != 16)
						{
							goto IL_E1;
						}
						tokenLength = 0;
						return true;
					}
				}
				else if (num2 != 32)
				{
					if (num2 == 48)
					{
						tokenLength = (1 << ((token & 12) >> 2) & 255);
						return true;
					}
					goto IL_E1;
				}
				if ((token & 128) != 0)
				{
					ushort num3;
					if (!stateObj.TryReadUInt16(out num3))
					{
						tokenLength = 0;
						return false;
					}
					tokenLength = (int)num3;
					return true;
				}
				else
				{
					if ((token & 12) == 0)
					{
						return stateObj.TryReadInt32(out tokenLength);
					}
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						tokenLength = 0;
						return false;
					}
					tokenLength = (int)b;
					return true;
				}
				IL_E1:
				tokenLength = 0;
				return true;
			}
			return stateObj.TryReadInt32(out tokenLength);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000E5F6C File Offset: 0x000E536C
		private void ProcessAttention(TdsParserStateObject stateObj)
		{
			if (this._state == TdsParserState.Closed || this._state == TdsParserState.Broken)
			{
				return;
			}
			stateObj.StoreErrorAndWarningForAttention();
			try
			{
				this.Run(RunBehavior.Attention, null, null, null, stateObj);
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(e);
				this._state = TdsParserState.Broken;
				this._connHandler.BreakConnection();
				throw;
			}
			stateObj.RestoreErrorAndWarningAfterAttention();
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000E5FEC File Offset: 0x000E53EC
		private static int StateValueLength(int dataLen)
		{
			if (dataLen >= 255)
			{
				return dataLen + 5;
			}
			return dataLen + 1;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000E6008 File Offset: 0x000E5408
		internal int WriteSessionRecoveryFeatureRequest(SessionData reconnectData, bool write)
		{
			int num = 1;
			if (write)
			{
				this._physicalStateObj.WriteByte(1);
			}
			if (reconnectData == null)
			{
				if (write)
				{
					this.WriteInt(0, this._physicalStateObj);
				}
				num += 4;
			}
			else
			{
				int num2 = 0;
				num2 += 1 + 2 * TdsParserStaticMethods.NullAwareStringLength(reconnectData._initialDatabase);
				num2 += 1 + 2 * TdsParserStaticMethods.NullAwareStringLength(reconnectData._initialLanguage);
				num2 += ((reconnectData._initialCollation == null) ? 1 : 6);
				for (int i = 0; i < 256; i++)
				{
					if (reconnectData._initialState[i] != null)
					{
						num2 += 1 + TdsParser.StateValueLength(reconnectData._initialState[i].Length);
					}
				}
				int num3 = 0;
				num3 += 1 + 2 * ((reconnectData._initialDatabase == reconnectData._database) ? 0 : TdsParserStaticMethods.NullAwareStringLength(reconnectData._database));
				num3 += 1 + 2 * ((reconnectData._initialLanguage == reconnectData._language) ? 0 : TdsParserStaticMethods.NullAwareStringLength(reconnectData._language));
				num3 += ((reconnectData._collation != null && !SqlCollation.AreSame(reconnectData._collation, reconnectData._initialCollation)) ? 6 : 1);
				bool[] array = new bool[256];
				for (int j = 0; j < 256; j++)
				{
					if (reconnectData._delta[j] != null)
					{
						array[j] = true;
						if (reconnectData._initialState[j] != null && reconnectData._initialState[j].Length == reconnectData._delta[j]._dataLength)
						{
							array[j] = false;
							for (int k = 0; k < reconnectData._delta[j]._dataLength; k++)
							{
								if (reconnectData._initialState[j][k] != reconnectData._delta[j]._data[k])
								{
									array[j] = true;
									break;
								}
							}
						}
						if (array[j])
						{
							num3 += 1 + TdsParser.StateValueLength(reconnectData._delta[j]._dataLength);
						}
					}
				}
				if (write)
				{
					this.WriteInt(8 + num2 + num3, this._physicalStateObj);
					this.WriteInt(num2, this._physicalStateObj);
					this.WriteIdentifier(reconnectData._initialDatabase, this._physicalStateObj);
					this.WriteCollation(reconnectData._initialCollation, this._physicalStateObj);
					this.WriteIdentifier(reconnectData._initialLanguage, this._physicalStateObj);
					for (int l = 0; l < 256; l++)
					{
						if (reconnectData._initialState[l] != null)
						{
							this._physicalStateObj.WriteByte((byte)l);
							if (reconnectData._initialState[l].Length < 255)
							{
								this._physicalStateObj.WriteByte((byte)reconnectData._initialState[l].Length);
							}
							else
							{
								this._physicalStateObj.WriteByte(byte.MaxValue);
								this.WriteInt(reconnectData._initialState[l].Length, this._physicalStateObj);
							}
							this._physicalStateObj.WriteByteArray(reconnectData._initialState[l], reconnectData._initialState[l].Length, 0, true, null);
						}
					}
					this.WriteInt(num3, this._physicalStateObj);
					this.WriteIdentifier((reconnectData._database != reconnectData._initialDatabase) ? reconnectData._database : null, this._physicalStateObj);
					this.WriteCollation(SqlCollation.AreSame(reconnectData._initialCollation, reconnectData._collation) ? null : reconnectData._collation, this._physicalStateObj);
					this.WriteIdentifier((reconnectData._language != reconnectData._initialLanguage) ? reconnectData._language : null, this._physicalStateObj);
					for (int m = 0; m < 256; m++)
					{
						if (array[m])
						{
							this._physicalStateObj.WriteByte((byte)m);
							if (reconnectData._delta[m]._dataLength < 255)
							{
								this._physicalStateObj.WriteByte((byte)reconnectData._delta[m]._dataLength);
							}
							else
							{
								this._physicalStateObj.WriteByte(byte.MaxValue);
								this.WriteInt(reconnectData._delta[m]._dataLength, this._physicalStateObj);
							}
							this._physicalStateObj.WriteByteArray(reconnectData._delta[m]._data, reconnectData._delta[m]._dataLength, 0, true, null);
						}
					}
				}
				num += num2 + num3 + 12;
			}
			return num;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000E6420 File Offset: 0x000E5820
		internal int WriteFedAuthFeatureRequest(FederatedAuthenticationFeatureExtensionData fedAuthFeatureData, bool write)
		{
			int num = 0;
			TdsEnums.FedAuthLibrary libraryType = fedAuthFeatureData.libraryType;
			if (libraryType != TdsEnums.FedAuthLibrary.SecurityToken)
			{
				if (libraryType == TdsEnums.FedAuthLibrary.ADAL)
				{
					num = 2;
				}
			}
			else
			{
				num = 5 + fedAuthFeatureData.accessToken.Length;
			}
			int result = num + 5;
			if (write)
			{
				this._physicalStateObj.WriteByte(2);
				byte b = 0;
				TdsEnums.FedAuthLibrary libraryType2 = fedAuthFeatureData.libraryType;
				if (libraryType2 != TdsEnums.FedAuthLibrary.SecurityToken)
				{
					if (libraryType2 == TdsEnums.FedAuthLibrary.ADAL)
					{
						b |= 4;
					}
				}
				else
				{
					b |= 2;
				}
				b |= (fedAuthFeatureData.fedAuthRequiredPreLoginResponse ? 1 : 0);
				this.WriteInt(num, this._physicalStateObj);
				this._physicalStateObj.WriteByte(b);
				TdsEnums.FedAuthLibrary libraryType3 = fedAuthFeatureData.libraryType;
				if (libraryType3 != TdsEnums.FedAuthLibrary.SecurityToken)
				{
					if (libraryType3 == TdsEnums.FedAuthLibrary.ADAL)
					{
						byte b2 = 0;
						switch (fedAuthFeatureData.authentication)
						{
						case SqlAuthenticationMethod.ActiveDirectoryPassword:
							b2 = 1;
							break;
						case SqlAuthenticationMethod.ActiveDirectoryIntegrated:
							b2 = 2;
							break;
						case SqlAuthenticationMethod.ActiveDirectoryInteractive:
							b2 = 3;
							break;
						}
						this._physicalStateObj.WriteByte(b2);
					}
				}
				else
				{
					this.WriteInt(fedAuthFeatureData.accessToken.Length, this._physicalStateObj);
					this._physicalStateObj.WriteByteArray(fedAuthFeatureData.accessToken, fedAuthFeatureData.accessToken.Length, 0, true, null);
				}
			}
			return result;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000E6530 File Offset: 0x000E5930
		internal int WriteTceFeatureRequest(bool write)
		{
			int result = 6;
			if (write)
			{
				this._physicalStateObj.WriteByte(4);
				this.WriteInt(1, this._physicalStateObj);
				this._physicalStateObj.WriteByte(2);
			}
			return result;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000E6568 File Offset: 0x000E5968
		internal int WriteGlobalTransactionsFeatureRequest(bool write)
		{
			int result = 5;
			if (write)
			{
				this._physicalStateObj.WriteByte(5);
				this.WriteInt(0, this._physicalStateObj);
			}
			return result;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000E6594 File Offset: 0x000E5994
		internal int WriteAzureSQLSupportFeatureRequest(bool write)
		{
			int result = 6;
			if (write)
			{
				this._physicalStateObj.WriteByte(8);
				this.WriteInt(TdsParser.s_FeatureExtDataAzureSQLSupportFeatureRequest.Length, this._physicalStateObj);
				this._physicalStateObj.WriteByteArray(TdsParser.s_FeatureExtDataAzureSQLSupportFeatureRequest, TdsParser.s_FeatureExtDataAzureSQLSupportFeatureRequest.Length, 0, true, null);
			}
			return result;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000E65E4 File Offset: 0x000E59E4
		internal void TdsLogin(SqlLogin rec, TdsEnums.FeatureExtension requestedFeatures, SessionData recoverySessionData, FederatedAuthenticationFeatureExtensionData? fedAuthFeatureExtensionData)
		{
			this._physicalStateObj.SetTimeoutSeconds(rec.timeout);
			this._connHandler.TimeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.LoginBegin);
			this._connHandler.TimeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth);
			byte[] array = null;
			byte[] array2 = null;
			bool flag = requestedFeatures > TdsEnums.FeatureExtension.None;
			string text;
			int num;
			if (rec.credential != null)
			{
				text = rec.credential.UserId;
				num = rec.credential.Password.Length * 2;
			}
			else
			{
				text = rec.userName;
				array = TdsParserStaticMethods.EncryptPassword(rec.password);
				num = array.Length;
			}
			int num2;
			if (rec.newSecurePassword != null)
			{
				num2 = rec.newSecurePassword.Length * 2;
			}
			else
			{
				array2 = TdsParserStaticMethods.EncryptPassword(rec.newPassword);
				num2 = array2.Length;
			}
			this._physicalStateObj._outputMessageType = 16;
			int num3 = 94;
			string text2 = ".Net SqlClient Data Provider";
			byte[] array3;
			uint num4;
			int v;
			checked
			{
				num3 += (rec.hostName.Length + rec.applicationName.Length + rec.serverName.Length + text2.Length + rec.language.Length + rec.database.Length + rec.attachDBFilename.Length) * 2;
				if (flag)
				{
					num3 += 4;
				}
				array3 = null;
				num4 = 0U;
				if (!rec.useSSPI && !this._connHandler._federatedAuthenticationInfoRequested && !this._connHandler._federatedAuthenticationRequested)
				{
					num3 += text.Length * 2 + num + num2;
				}
				else if (rec.useSSPI)
				{
					array3 = new byte[TdsParser.s_maxSSPILength];
					num4 = TdsParser.s_maxSSPILength;
					this._physicalStateObj.SniContext = SniContext.Snix_LoginSspi;
					this.SSPIData(null, 0U, array3, ref num4);
					if (num4 > 2147483647U)
					{
						throw SQL.InvalidSSPIPacketSize();
					}
					this._physicalStateObj.SniContext = SniContext.Snix_Login;
					num3 += (int)num4;
				}
				v = num3;
				if (flag)
				{
					if ((requestedFeatures & TdsEnums.FeatureExtension.SessionRecovery) != TdsEnums.FeatureExtension.None)
					{
						num3 += this.WriteSessionRecoveryFeatureRequest(recoverySessionData, false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.FedAuth) != TdsEnums.FeatureExtension.None)
					{
						num3 += this.WriteFedAuthFeatureRequest(fedAuthFeatureExtensionData.Value, false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.Tce) != TdsEnums.FeatureExtension.None)
					{
						num3 += this.WriteTceFeatureRequest(false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.GlobalTransactions) != TdsEnums.FeatureExtension.None)
					{
						num3 += this.WriteGlobalTransactionsFeatureRequest(false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.AzureSQLSupport) != TdsEnums.FeatureExtension.None)
					{
						num3 += this.WriteAzureSQLSupportFeatureRequest(false);
					}
					num3++;
				}
			}
			try
			{
				this.WriteInt(num3, this._physicalStateObj);
				if (recoverySessionData == null)
				{
					this.WriteInt(1946157060, this._physicalStateObj);
				}
				else
				{
					this.WriteUnsignedInt(recoverySessionData._tdsVersion, this._physicalStateObj);
				}
				this.WriteInt(rec.packetSize, this._physicalStateObj);
				this.WriteInt(100663296, this._physicalStateObj);
				this.WriteInt(TdsParserStaticMethods.GetCurrentProcessIdForTdsLoginOnly(), this._physicalStateObj);
				this.WriteInt(0, this._physicalStateObj);
				int num5 = 0;
				num5 |= 32;
				num5 |= 64;
				num5 |= 128;
				num5 |= 256;
				num5 |= 512;
				if (rec.useReplication)
				{
					num5 |= 12288;
				}
				if (rec.useSSPI)
				{
					num5 |= 32768;
				}
				if (rec.readOnlyIntent)
				{
					num5 |= 2097152;
				}
				if (!ADP.IsEmpty(rec.newPassword) || (rec.newSecurePassword != null && rec.newSecurePassword.Length != 0))
				{
					num5 |= 16777216;
				}
				if (rec.userInstance)
				{
					num5 |= 67108864;
				}
				if (flag)
				{
					num5 |= 268435456;
				}
				this.WriteInt(num5, this._physicalStateObj);
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.TdsParser.TdsLogin|ADV> %d#, TDS Login7 flags = %d:\n", this.ObjectID, num5);
				}
				this.WriteInt(0, this._physicalStateObj);
				this.WriteInt(0, this._physicalStateObj);
				int num6 = 94;
				this.WriteShort(num6, this._physicalStateObj);
				this.WriteShort(rec.hostName.Length, this._physicalStateObj);
				num6 += rec.hostName.Length * 2;
				if (!rec.useSSPI && !this._connHandler._federatedAuthenticationInfoRequested && !this._connHandler._federatedAuthenticationRequested)
				{
					this.WriteShort(num6, this._physicalStateObj);
					this.WriteShort(text.Length, this._physicalStateObj);
					num6 += text.Length * 2;
					this.WriteShort(num6, this._physicalStateObj);
					this.WriteShort(num / 2, this._physicalStateObj);
					num6 += num;
				}
				else
				{
					this.WriteShort(0, this._physicalStateObj);
					this.WriteShort(0, this._physicalStateObj);
					this.WriteShort(0, this._physicalStateObj);
					this.WriteShort(0, this._physicalStateObj);
				}
				this.WriteShort(num6, this._physicalStateObj);
				this.WriteShort(rec.applicationName.Length, this._physicalStateObj);
				num6 += rec.applicationName.Length * 2;
				this.WriteShort(num6, this._physicalStateObj);
				this.WriteShort(rec.serverName.Length, this._physicalStateObj);
				num6 += rec.serverName.Length * 2;
				this.WriteShort(num6, this._physicalStateObj);
				if (flag)
				{
					this.WriteShort(4, this._physicalStateObj);
					num6 += 4;
				}
				else
				{
					this.WriteShort(0, this._physicalStateObj);
				}
				this.WriteShort(num6, this._physicalStateObj);
				this.WriteShort(text2.Length, this._physicalStateObj);
				num6 += text2.Length * 2;
				this.WriteShort(num6, this._physicalStateObj);
				this.WriteShort(rec.language.Length, this._physicalStateObj);
				num6 += rec.language.Length * 2;
				this.WriteShort(num6, this._physicalStateObj);
				this.WriteShort(rec.database.Length, this._physicalStateObj);
				num6 += rec.database.Length * 2;
				if (TdsParser.s_nicAddress == null)
				{
					TdsParser.s_nicAddress = TdsParserStaticMethods.GetNetworkPhysicalAddressForTdsLoginOnly();
				}
				this._physicalStateObj.WriteByteArray(TdsParser.s_nicAddress, TdsParser.s_nicAddress.Length, 0, true, null);
				this.WriteShort(num6, this._physicalStateObj);
				if (rec.useSSPI)
				{
					this.WriteShort((int)num4, this._physicalStateObj);
					num6 += (int)num4;
				}
				else
				{
					this.WriteShort(0, this._physicalStateObj);
				}
				this.WriteShort(num6, this._physicalStateObj);
				this.WriteShort(rec.attachDBFilename.Length, this._physicalStateObj);
				num6 += rec.attachDBFilename.Length * 2;
				this.WriteShort(num6, this._physicalStateObj);
				this.WriteShort(num2 / 2, this._physicalStateObj);
				this.WriteInt(0, this._physicalStateObj);
				this.WriteString(rec.hostName, this._physicalStateObj, true);
				if (!rec.useSSPI && !this._connHandler._federatedAuthenticationInfoRequested && !this._connHandler._federatedAuthenticationRequested)
				{
					this.WriteString(text, this._physicalStateObj, true);
					this._physicalStateObj._tracePasswordOffset = this._physicalStateObj._outBytesUsed;
					this._physicalStateObj._tracePasswordLength = num;
					if (rec.credential != null)
					{
						this._physicalStateObj.WriteSecureString(rec.credential.Password);
					}
					else
					{
						this._physicalStateObj.WriteByteArray(array, num, 0, true, null);
					}
				}
				this.WriteString(rec.applicationName, this._physicalStateObj, true);
				this.WriteString(rec.serverName, this._physicalStateObj, true);
				if (flag)
				{
					this.WriteInt(v, this._physicalStateObj);
				}
				this.WriteString(text2, this._physicalStateObj, true);
				this.WriteString(rec.language, this._physicalStateObj, true);
				this.WriteString(rec.database, this._physicalStateObj, true);
				if (rec.useSSPI)
				{
					this._physicalStateObj.WriteByteArray(array3, (int)num4, 0, true, null);
				}
				this.WriteString(rec.attachDBFilename, this._physicalStateObj, true);
				if (!rec.useSSPI && !this._connHandler._federatedAuthenticationInfoRequested && !this._connHandler._federatedAuthenticationRequested)
				{
					this._physicalStateObj._traceChangePasswordOffset = this._physicalStateObj._outBytesUsed;
					this._physicalStateObj._traceChangePasswordLength = num2;
					if (rec.newSecurePassword != null)
					{
						this._physicalStateObj.WriteSecureString(rec.newSecurePassword);
					}
					else
					{
						this._physicalStateObj.WriteByteArray(array2, num2, 0, true, null);
					}
				}
				if (flag)
				{
					if ((requestedFeatures & TdsEnums.FeatureExtension.SessionRecovery) != TdsEnums.FeatureExtension.None)
					{
						this.WriteSessionRecoveryFeatureRequest(recoverySessionData, true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.FedAuth) != TdsEnums.FeatureExtension.None)
					{
						Bid.Trace("<sc.TdsParser.TdsLogin|SEC> Sending federated authentication feature request\n");
						this.WriteFedAuthFeatureRequest(fedAuthFeatureExtensionData.Value, true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.Tce) != TdsEnums.FeatureExtension.None)
					{
						this.WriteTceFeatureRequest(true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.GlobalTransactions) != TdsEnums.FeatureExtension.None)
					{
						this.WriteGlobalTransactionsFeatureRequest(true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.AzureSQLSupport) != TdsEnums.FeatureExtension.None)
					{
						this.WriteAzureSQLSupportFeatureRequest(true);
					}
					this._physicalStateObj.WriteByte(byte.MaxValue);
				}
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					this._physicalStateObj._outputPacketNumber = 1;
					this._physicalStateObj.ResetBuffer();
				}
				throw;
			}
			this._physicalStateObj.WritePacket(1, false);
			this._physicalStateObj.ResetSecurePasswordsInfomation();
			this._physicalStateObj._pendingData = true;
			this._physicalStateObj._messageStatus = 0;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000E6EAC File Offset: 0x000E62AC
		internal void SendFedAuthToken(SqlFedAuthToken fedAuthToken)
		{
			Bid.Trace("<sc.TdsParser.SendFedAuthToken|SEC> Sending federated authentication token\n");
			this._physicalStateObj._outputMessageType = 8;
			byte[] accessToken = fedAuthToken.accessToken;
			this.WriteUnsignedInt((uint)(accessToken.Length + 4), this._physicalStateObj);
			this.WriteUnsignedInt((uint)accessToken.Length, this._physicalStateObj);
			this._physicalStateObj.WriteByteArray(accessToken, accessToken.Length, 0, true, null);
			this._physicalStateObj.WritePacket(1, false);
			this._physicalStateObj._pendingData = true;
			this._physicalStateObj._messageStatus = 0;
			this._connHandler._federatedAuthenticationRequested = true;
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000E6F3C File Offset: 0x000E633C
		private void SSPIData(byte[] receivedBuff, uint receivedLength, byte[] sendBuff, ref uint sendLength)
		{
			this.SNISSPIData(receivedBuff, receivedLength, sendBuff, ref sendLength);
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000E6F54 File Offset: 0x000E6354
		private void SNISSPIData(byte[] receivedBuff, uint receivedLength, byte[] sendBuff, ref uint sendLength)
		{
			if (receivedBuff == null)
			{
				receivedLength = 0U;
			}
			if (SNINativeMethodWrapper.SNISecGenClientContext(this._physicalStateObj.Handle, receivedBuff, receivedLength, sendBuff, ref sendLength, this._sniSpnBuffer) != 0U)
			{
				this.SSPIError(SQLMessage.SSPIGenerateError(), "GenClientContext");
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000E6F94 File Offset: 0x000E6394
		private void ProcessSSPI(int receivedLength)
		{
			SniContext sniContext = this._physicalStateObj.SniContext;
			this._physicalStateObj.SniContext = SniContext.Snix_ProcessSspi;
			byte[] array = new byte[receivedLength];
			if (!this._physicalStateObj.TryReadByteArray(array, 0, receivedLength))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			byte[] array2 = new byte[TdsParser.s_maxSSPILength];
			uint len = TdsParser.s_maxSSPILength;
			this.SSPIData(array, (uint)receivedLength, array2, ref len);
			this._physicalStateObj.WriteByteArray(array2, (int)len, 0, true, null);
			this._physicalStateObj._outputMessageType = 17;
			this._physicalStateObj.WritePacket(1, false);
			this._physicalStateObj.SniContext = sniContext;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000E7034 File Offset: 0x000E6434
		private void SSPIError(string error, string procedure)
		{
			this._physicalStateObj.AddError(new SqlError(0, 0, 11, this._server, error, procedure, 0));
			this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000E706C File Offset: 0x000E646C
		private void LoadSSPILibrary()
		{
			if (!TdsParser.s_fSSPILoaded)
			{
				object obj = TdsParser.s_tdsParserLock;
				lock (obj)
				{
					if (!TdsParser.s_fSSPILoaded)
					{
						uint num = 0U;
						if (SNINativeMethodWrapper.SNISecInitPackage(ref num) != 0U)
						{
							this.SSPIError(SQLMessage.SSPIInitializeError(), "InitSSPIPackage");
						}
						TdsParser.s_maxSSPILength = num;
						TdsParser.s_fSSPILoaded = true;
					}
				}
			}
			if (TdsParser.s_maxSSPILength > 2147483647U)
			{
				throw SQL.InvalidSSPIPacketSize();
			}
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000E70FC File Offset: 0x000E64FC
		private void LoadADALLibrary()
		{
			if (!TdsParser.s_fADALLoaded)
			{
				object obj = TdsParser.s_tdsParserLock;
				lock (obj)
				{
					if (!TdsParser.s_fADALLoaded)
					{
						int num = ADALNativeWrapper.ADALInitialize();
						if (num == 0)
						{
							TdsParser.s_fADALLoaded = true;
						}
						else
						{
							TdsParser.s_fADALLoaded = false;
							SqlAuthenticationMethod sqlAuthenticationMethod = SqlAuthenticationMethod.NotSpecified;
							if (this._connHandler.ConnectionOptions != null)
							{
								sqlAuthenticationMethod = this._connHandler.ConnectionOptions.Authentication;
							}
							this._physicalStateObj.AddError(new SqlError(0, 0, 11, this._server, Res.GetString("SQL_ADALInitializeError", new object[]
							{
								sqlAuthenticationMethod.ToString("G"),
								num.ToString("X")
							}), "InitADALPackage", 0));
							this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
						}
					}
				}
			}
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000E71F0 File Offset: 0x000E65F0
		internal byte[] GetDTCAddress(int timeout, TdsParserStateObject stateObj)
		{
			byte[] array = null;
			using (SqlDataReader sqlDataReader = this.TdsExecuteTransactionManagerRequest(null, TdsEnums.TransactionManagerRequestType.GetDTCAddress, null, TdsEnums.TransactionManagerIsolationLevel.Unspecified, timeout, null, stateObj, true))
			{
				if (sqlDataReader != null && sqlDataReader.Read())
				{
					long bytes = sqlDataReader.GetBytes(0, 0L, null, 0, 0);
					if (bytes <= 2147483647L)
					{
						int num = (int)bytes;
						array = new byte[num];
						sqlDataReader.GetBytes(0, 0L, array, 0, num);
					}
				}
			}
			return array;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000E7270 File Offset: 0x000E6670
		internal void PropagateDistributedTransaction(byte[] buffer, int timeout, TdsParserStateObject stateObj)
		{
			this.TdsExecuteTransactionManagerRequest(buffer, TdsEnums.TransactionManagerRequestType.Propagate, null, TdsEnums.TransactionManagerIsolationLevel.Unspecified, timeout, null, stateObj, true);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000E728C File Offset: 0x000E668C
		internal SqlDataReader TdsExecuteTransactionManagerRequest(byte[] buffer, TdsEnums.TransactionManagerRequestType request, string transactionName, TdsEnums.TransactionManagerIsolationLevel isoLevel, int timeout, SqlInternalTransaction transaction, TdsParserStateObject stateObj, bool isDelegateControlRequest)
		{
			if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
			{
				return null;
			}
			bool threadHasParserLockForClose = this._connHandler.ThreadHasParserLockForClose;
			if (!threadHasParserLockForClose)
			{
				this._connHandler._parserLock.Wait(false);
				this._connHandler.ThreadHasParserLockForClose = true;
			}
			bool asyncWrite = this._asyncWrite;
			SqlDataReader result;
			try
			{
				this._asyncWrite = false;
				if (!isDelegateControlRequest)
				{
					this._connHandler.CheckEnlistedTransactionBinding();
				}
				stateObj._outputMessageType = 14;
				stateObj.SetTimeoutSeconds(timeout);
				stateObj.SniContext = SniContext.Snix_Execute;
				if (this._isYukon)
				{
					this.WriteInt(22, stateObj);
					this.WriteInt(18, stateObj);
					this.WriteMarsHeaderData(stateObj, this._currentTransaction);
				}
				this.WriteShort((int)((short)request), stateObj);
				bool flag = false;
				switch (request)
				{
				case TdsEnums.TransactionManagerRequestType.GetDTCAddress:
					this.WriteShort(0, stateObj);
					flag = true;
					break;
				case TdsEnums.TransactionManagerRequestType.Propagate:
					if (buffer != null)
					{
						this.WriteShort(buffer.Length, stateObj);
						stateObj.WriteByteArray(buffer, buffer.Length, 0, true, null);
					}
					else
					{
						this.WriteShort(0, stateObj);
					}
					break;
				case TdsEnums.TransactionManagerRequestType.Begin:
					if (this._currentTransaction != transaction)
					{
						this.PendingTransaction = transaction;
					}
					stateObj.WriteByte((byte)isoLevel);
					stateObj.WriteByte((byte)(transactionName.Length * 2));
					this.WriteString(transactionName, stateObj, true);
					break;
				case TdsEnums.TransactionManagerRequestType.Commit:
					stateObj.WriteByte(0);
					stateObj.WriteByte(0);
					break;
				case TdsEnums.TransactionManagerRequestType.Rollback:
					stateObj.WriteByte((byte)(transactionName.Length * 2));
					this.WriteString(transactionName, stateObj, true);
					stateObj.WriteByte(0);
					break;
				case TdsEnums.TransactionManagerRequestType.Save:
					stateObj.WriteByte((byte)(transactionName.Length * 2));
					this.WriteString(transactionName, stateObj, true);
					break;
				}
				Task task = stateObj.WritePacket(1, false);
				stateObj._pendingData = true;
				stateObj._messageStatus = 0;
				SqlDataReader sqlDataReader = null;
				stateObj.SniContext = SniContext.Snix_Read;
				if (flag)
				{
					sqlDataReader = new SqlDataReader(null, CommandBehavior.Default);
					sqlDataReader.Bind(stateObj);
					_SqlMetaDataSet metaData = sqlDataReader.MetaData;
				}
				else
				{
					this.Run(RunBehavior.UntilDone, null, null, null, stateObj);
				}
				if ((request == TdsEnums.TransactionManagerRequestType.Begin || request == TdsEnums.TransactionManagerRequestType.Propagate) && (transaction == null || transaction.TransactionId != this._retainedTransactionId))
				{
					this._retainedTransactionId = 0L;
				}
				result = sqlDataReader;
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				this.FailureCleanup(stateObj, e);
				throw;
			}
			finally
			{
				this._pendingTransaction = null;
				this._asyncWrite = asyncWrite;
				if (!threadHasParserLockForClose)
				{
					this._connHandler.ThreadHasParserLockForClose = false;
					this._connHandler._parserLock.Release();
				}
			}
			return result;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000E753C File Offset: 0x000E693C
		internal void FailureCleanup(TdsParserStateObject stateObj, Exception e)
		{
			int outputPacketNumber = (int)stateObj._outputPacketNumber;
			if (Bid.TraceOn)
			{
				Bid.Trace("<sc.TdsParser.FailureCleanup|ERR> Exception caught on ExecuteXXX: '%ls' \n", e.ToString());
			}
			if (stateObj.HasOpenResult)
			{
				stateObj.DecrementOpenResultCount();
			}
			stateObj.ResetBuffer();
			stateObj._outputPacketNumber = 1;
			if (outputPacketNumber != 1 && this._state == TdsParserState.OpenLoggedIn)
			{
				bool threadHasParserLockForClose = this._connHandler.ThreadHasParserLockForClose;
				try
				{
					this._connHandler.ThreadHasParserLockForClose = true;
					stateObj.SendAttention(false);
					this.ProcessAttention(stateObj);
				}
				finally
				{
					this._connHandler.ThreadHasParserLockForClose = threadHasParserLockForClose;
				}
			}
			Bid.Trace("<sc.TdsParser.FailureCleanup|ERR> Exception rethrown. \n");
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000E75EC File Offset: 0x000E69EC
		internal Task TdsExecuteSQLBatch(string text, int timeout, SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj, bool sync, bool callerHasConnectionLock = false, byte[] enclavePackage = null)
		{
			if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
			{
				return null;
			}
			if (stateObj.BcpLock)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			bool flag = !callerHasConnectionLock && !this._connHandler.ThreadHasParserLockForClose;
			bool flag2 = false;
			if (flag)
			{
				this._connHandler._parserLock.Wait(!sync);
				flag2 = true;
			}
			this._asyncWrite = !sync;
			Task result;
			try
			{
				if (this._state == TdsParserState.Closed || this._state == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
				this._connHandler.CheckEnlistedTransactionBinding();
				stateObj.SetTimeoutSeconds(timeout);
				if (!this._fMARS && this._physicalStateObj.HasOpenResult)
				{
					Bid.Trace("<sc.TdsParser.TdsExecuteSQLBatch|ERR> Potential multi-threaded misuse of connection, non-MARs connection with an open result %d#\n", this.ObjectID);
				}
				stateObj.SniContext = SniContext.Snix_Execute;
				if (this._isYukon)
				{
					this.WriteRPCBatchHeaders(stateObj, notificationRequest);
				}
				stateObj._outputMessageType = 1;
				this.WriteEnclaveInfo(stateObj, enclavePackage);
				this.WriteString(text, text.Length, 0, stateObj, true);
				Task task = stateObj.ExecuteFlush();
				if (task == null)
				{
					stateObj.SniContext = SniContext.Snix_Read;
					result = null;
				}
				else
				{
					bool taskReleaseConnectionLock = flag2;
					flag2 = false;
					result = task.ContinueWith(delegate(Task t)
					{
						try
						{
							if (t.IsFaulted)
							{
								this.FailureCleanup(stateObj, t.Exception.InnerException);
								throw t.Exception.InnerException;
							}
							stateObj.SniContext = SniContext.Snix_Read;
						}
						finally
						{
							if (taskReleaseConnectionLock)
							{
								this._connHandler._parserLock.Release();
							}
						}
					}, TaskScheduler.Default);
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				this.FailureCleanup(stateObj, e);
				throw;
			}
			finally
			{
				if (flag2)
				{
					this._connHandler._parserLock.Release();
				}
			}
			return result;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000E77C8 File Offset: 0x000E6BC8
		internal Task TdsExecuteRPC(SqlCommand cmd, _SqlRPC[] rpcArray, int timeout, bool inSchema, SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj, bool isCommandProc, bool sync = true, TaskCompletionSource<object> completion = null, int startRpc = 0, int startParam = 0)
		{
			bool flag = completion == null;
			bool flag2 = false;
			Task result2;
			try
			{
				_SqlRPC sqlRPC = null;
				if (flag)
				{
					this._connHandler._parserLock.Wait(!sync);
					flag2 = true;
				}
				try
				{
					if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
					if (flag)
					{
						this._asyncWrite = !sync;
						this._connHandler.CheckEnlistedTransactionBinding();
						stateObj.SetTimeoutSeconds(timeout);
						if (!this._fMARS && this._physicalStateObj.HasOpenResult)
						{
							Bid.Trace("<sc.TdsParser.TdsExecuteRPC|ERR> Potential multi-threaded misuse of connection, non-MARs connection with an open result %d#\n", this.ObjectID);
						}
						stateObj.SniContext = SniContext.Snix_Execute;
						if (this._isYukon)
						{
							this.WriteRPCBatchHeaders(stateObj, notificationRequest);
						}
						stateObj._outputMessageType = 3;
					}
					Action<Exception> <>9__1;
					Action<Task> <>9__2;
					int num5;
					int ii;
					for (ii = startRpc; ii < rpcArray.Length; ii = num5 + 1)
					{
						sqlRPC = rpcArray[ii];
						if (startParam == 0 || ii > startRpc)
						{
							if (sqlRPC.ProcID != 0 && this._isShiloh)
							{
								this.WriteShort(65535, stateObj);
								this.WriteShort((int)((short)sqlRPC.ProcID), stateObj);
							}
							else
							{
								int length = sqlRPC.rpcName.Length;
								this.WriteShort(length, stateObj);
								this.WriteString(sqlRPC.rpcName, length, 0, stateObj, true);
							}
							this.WriteShort((int)((short)sqlRPC.options), stateObj);
							byte[] enclavePackage = (cmd.enclavePackage != null) ? cmd.enclavePackage.EnclavePackageBytes : null;
							this.WriteEnclaveInfo(stateObj, enclavePackage);
						}
						SqlParameter[] parameters = sqlRPC.parameters;
						int i;
						for (i = ((ii == startRpc) ? startParam : 0); i < parameters.Length; i = num5 + 1)
						{
							SqlParameter sqlParameter = parameters[i];
							if (sqlParameter == null)
							{
								break;
							}
							if (sqlParameter.ForceColumnEncryption && cmd.ColumnEncryptionSetting != SqlCommandColumnEncryptionSetting.Enabled && (cmd.ColumnEncryptionSetting != SqlCommandColumnEncryptionSetting.UseConnectionSetting || !cmd.Connection.IsColumnEncryptionSettingEnabled))
							{
								throw SQL.ParamInvalidForceColumnEncryptionSetting(sqlParameter.ParameterName, sqlRPC.GetCommandTextOrRpcName());
							}
							if (sqlParameter.ForceColumnEncryption && sqlParameter.CipherMetadata == null && (sqlParameter.Direction == ParameterDirection.Input || sqlParameter.Direction == ParameterDirection.InputOutput))
							{
								throw SQL.ParamUnExpectedEncryptionMetadata(sqlParameter.ParameterName, sqlRPC.GetCommandTextOrRpcName());
							}
							sqlParameter.Validate(i, isCommandProc);
							MetaType metaType = sqlParameter.InternalMetaType;
							if (metaType.IsNewKatmaiType)
							{
								this.WriteSmiParameter(sqlParameter, i, (sqlRPC.paramoptions[i] & 2) > 0, stateObj);
							}
							else
							{
								if ((!this._isShiloh && !metaType.Is70Supported) || (!this._isYukon && !metaType.Is80Supported) || (!this._isKatmai && !metaType.Is90Supported))
								{
									throw ADP.VersionDoesNotSupportDataType(metaType.TypeName);
								}
								object obj = null;
								bool flag3 = true;
								bool flag4 = false;
								bool flag5 = false;
								if (sqlParameter.Direction == ParameterDirection.Output)
								{
									flag4 = sqlParameter.ParamaterIsSqlType;
									sqlParameter.Value = null;
									sqlParameter.ParamaterIsSqlType = flag4;
								}
								else
								{
									obj = sqlParameter.GetCoercedValue();
									flag3 = sqlParameter.IsNull;
									if (!flag3)
									{
										flag4 = sqlParameter.CoercedValueIsSqlType;
										flag5 = sqlParameter.CoercedValueIsDataFeed;
									}
								}
								this.WriteParameterName(sqlParameter.ParameterNameFixed, stateObj);
								stateObj.WriteByte(sqlRPC.paramoptions[i]);
								int num = metaType.IsSizeInCharacters ? (sqlParameter.GetParameterSize() * 2) : sqlParameter.GetParameterSize();
								int num2;
								if (metaType.TDSType != 240)
								{
									num2 = sqlParameter.GetActualSize();
								}
								else
								{
									num2 = 0;
								}
								byte b = 0;
								byte b2 = 0;
								if (metaType.SqlDbType == SqlDbType.Decimal)
								{
									b = sqlParameter.GetActualPrecision();
									b2 = sqlParameter.GetActualScale();
									if (b > 38)
									{
										throw SQL.PrecisionValueOutOfRange(b);
									}
									if (!flag3)
									{
										if (flag4)
										{
											obj = TdsParser.AdjustSqlDecimalScale((SqlDecimal)obj, (int)b2);
											if (b != 0 && b < ((SqlDecimal)obj).Precision)
											{
												throw ADP.ParameterValueOutOfRange((SqlDecimal)obj);
											}
										}
										else
										{
											obj = TdsParser.AdjustDecimalScale((decimal)obj, (int)b2);
											SqlDecimal sqlDecimal = new SqlDecimal((decimal)obj);
											if (b != 0 && b < sqlDecimal.Precision)
											{
												throw ADP.ParameterValueOutOfRange((decimal)obj);
											}
										}
									}
								}
								bool flag6 = (sqlRPC.paramoptions[i] & 8) > 0;
								SqlColumnEncryptionInputParameterInfo columnEncryptionParameterInfo = null;
								if (flag6)
								{
									byte[] array = null;
									if (!flag3)
									{
										try
										{
											byte[] plainText;
											if (flag4)
											{
												plainText = this.SerializeUnencryptedSqlValue(obj, metaType, num2, sqlParameter.Offset, sqlParameter.NormalizationRuleVersion, stateObj);
											}
											else
											{
												plainText = this.SerializeUnencryptedValue(obj, metaType, sqlParameter.GetActualScale(), num2, sqlParameter.Offset, flag5, sqlParameter.NormalizationRuleVersion, stateObj);
											}
											array = SqlSecurityUtility.EncryptWithKey(plainText, sqlParameter.CipherMetadata, this._connHandler.ConnectionOptions.DataSource);
											goto IL_603;
										}
										catch (Exception e)
										{
											throw SQL.ParamEncryptionFailed(sqlParameter.ParameterName, null, e);
										}
										goto IL_600;
									}
									goto IL_600;
									IL_603:
									metaType = MetaType.MetaMaxVarBinary;
									num = -1;
									num2 = ((array == null) ? 0 : array.Length);
									columnEncryptionParameterInfo = new SqlColumnEncryptionInputParameterInfo(sqlParameter.GetMetadataForTypeInfo(), sqlParameter.CipherMetadata);
									obj = array;
									flag4 = false;
									goto IL_633;
									IL_600:
									array = null;
									goto IL_603;
								}
								IL_633:
								stateObj.WriteByte(metaType.NullableType);
								if (metaType.TDSType == 98)
								{
									this.WriteSqlVariantValue(flag4 ? MetaType.GetComValueFromSqlVariant(obj) : obj, sqlParameter.GetActualSize(), sqlParameter.Offset, stateObj, true);
								}
								else
								{
									int num3 = 0;
									int num4 = 0;
									if (metaType.IsAnsiType)
									{
										if (!flag3 && !flag5)
										{
											string value;
											if (flag4)
											{
												if (obj is SqlString)
												{
													value = ((SqlString)obj).Value;
												}
												else
												{
													value = new string(((SqlChars)obj).Value);
												}
											}
											else
											{
												value = (string)obj;
											}
											num3 = this.GetEncodingCharLength(value, num2, sqlParameter.Offset, this._defaultEncoding);
										}
										if (metaType.IsPlp)
										{
											this.WriteShort(65535, stateObj);
										}
										else
										{
											num4 = ((num > num3) ? num : num3);
											if (num4 == 0)
											{
												if (metaType.IsNCharType)
												{
													num4 = 2;
												}
												else
												{
													num4 = 1;
												}
											}
											this.WriteParameterVarLen(metaType, num4, false, stateObj, false);
										}
									}
									else if (metaType.SqlDbType == SqlDbType.Timestamp)
									{
										this.WriteParameterVarLen(metaType, 8, false, stateObj, false);
									}
									else if (metaType.SqlDbType == SqlDbType.Udt)
									{
										byte[] array2 = null;
										Format format = Format.Native;
										if (!flag3)
										{
											array2 = this._connHandler.Connection.GetBytes(obj, out format, out num4);
											num = array2.Length;
											if (num < 0 || (num >= 65535 && num4 != -1))
											{
												throw new IndexOutOfRangeException();
											}
										}
										byte[] bytes = BitConverter.GetBytes((long)num);
										if (ADP.IsEmpty(sqlParameter.UdtTypeName))
										{
											throw SQL.MustSetUdtTypeNameForUdtParams();
										}
										string[] array3 = SqlParameter.ParseTypeName(sqlParameter.UdtTypeName, true);
										if (!ADP.IsEmpty(array3[0]) && 255 < array3[0].Length)
										{
											throw ADP.ArgumentOutOfRange("names");
										}
										if (!ADP.IsEmpty(array3[1]) && 255 < array3[array3.Length - 2].Length)
										{
											throw ADP.ArgumentOutOfRange("names");
										}
										if (255 < array3[2].Length)
										{
											throw ADP.ArgumentOutOfRange("names");
										}
										this.WriteUDTMetaData(obj, array3[0], array3[1], array3[2], stateObj);
										if (!flag3)
										{
											this.WriteUnsignedLong((ulong)((long)array2.Length), stateObj);
											if (array2.Length != 0)
											{
												this.WriteInt(array2.Length, stateObj);
												stateObj.WriteByteArray(array2, array2.Length, 0, true, null);
											}
											this.WriteInt(0, stateObj);
											goto IL_EDB;
										}
										this.WriteUnsignedLong(ulong.MaxValue, stateObj);
										goto IL_EDB;
									}
									else if (metaType.IsPlp)
									{
										if (metaType.SqlDbType != SqlDbType.Xml)
										{
											this.WriteShort(65535, stateObj);
										}
									}
									else if (!metaType.IsVarTime && metaType.SqlDbType != SqlDbType.Date)
									{
										num4 = ((num > num2) ? num : num2);
										if (num4 == 0 && this.IsYukonOrNewer)
										{
											if (metaType.IsNCharType)
											{
												num4 = 2;
											}
											else
											{
												num4 = 1;
											}
										}
										this.WriteParameterVarLen(metaType, num4, false, stateObj, false);
									}
									if (metaType.SqlDbType == SqlDbType.Decimal)
									{
										if (b == 0)
										{
											if (this._isShiloh)
											{
												stateObj.WriteByte(29);
											}
											else
											{
												stateObj.WriteByte(28);
											}
										}
										else
										{
											stateObj.WriteByte(b);
										}
										stateObj.WriteByte(b2);
									}
									else if (metaType.IsVarTime)
									{
										stateObj.WriteByte(sqlParameter.GetActualScale());
									}
									if (this._isYukon && metaType.SqlDbType == SqlDbType.Xml)
									{
										if ((sqlParameter.XmlSchemaCollectionDatabase != null && sqlParameter.XmlSchemaCollectionDatabase != ADP.StrEmpty) || (sqlParameter.XmlSchemaCollectionOwningSchema != null && sqlParameter.XmlSchemaCollectionOwningSchema != ADP.StrEmpty) || (sqlParameter.XmlSchemaCollectionName != null && sqlParameter.XmlSchemaCollectionName != ADP.StrEmpty))
										{
											stateObj.WriteByte(1);
											if (sqlParameter.XmlSchemaCollectionDatabase != null && sqlParameter.XmlSchemaCollectionDatabase != ADP.StrEmpty)
											{
												int length = sqlParameter.XmlSchemaCollectionDatabase.Length;
												stateObj.WriteByte((byte)length);
												this.WriteString(sqlParameter.XmlSchemaCollectionDatabase, length, 0, stateObj, true);
											}
											else
											{
												stateObj.WriteByte(0);
											}
											if (sqlParameter.XmlSchemaCollectionOwningSchema != null && sqlParameter.XmlSchemaCollectionOwningSchema != ADP.StrEmpty)
											{
												int length = sqlParameter.XmlSchemaCollectionOwningSchema.Length;
												stateObj.WriteByte((byte)length);
												this.WriteString(sqlParameter.XmlSchemaCollectionOwningSchema, length, 0, stateObj, true);
											}
											else
											{
												stateObj.WriteByte(0);
											}
											if (sqlParameter.XmlSchemaCollectionName != null && sqlParameter.XmlSchemaCollectionName != ADP.StrEmpty)
											{
												int length = sqlParameter.XmlSchemaCollectionName.Length;
												this.WriteShort((int)((short)length), stateObj);
												this.WriteString(sqlParameter.XmlSchemaCollectionName, length, 0, stateObj, true);
											}
											else
											{
												this.WriteShort(0, stateObj);
											}
										}
										else
										{
											stateObj.WriteByte(0);
										}
									}
									else if (this._isShiloh && metaType.IsCharType)
									{
										SqlCollation sqlCollation = (sqlParameter.Collation != null) ? sqlParameter.Collation : this._defaultCollation;
										this.WriteUnsignedInt(sqlCollation.info, stateObj);
										stateObj.WriteByte(sqlCollation.sortId);
									}
									if (num3 == 0)
									{
										this.WriteParameterVarLen(metaType, num2, flag3, stateObj, flag5);
									}
									else
									{
										this.WriteParameterVarLen(metaType, num3, flag3, stateObj, flag5);
									}
									Task task = null;
									if (!flag3)
									{
										if (flag4)
										{
											task = this.WriteSqlValue(obj, metaType, num2, num3, sqlParameter.Offset, stateObj);
										}
										else
										{
											task = this.WriteValue(obj, metaType, flag6 ? 0 : sqlParameter.GetActualScale(), num2, num3, flag6 ? 0 : sqlParameter.Offset, stateObj, flag6 ? 0 : sqlParameter.Size, flag5);
										}
									}
									if (flag6)
									{
										task = this.WriteEncryptionMetadata(task, columnEncryptionParameterInfo, stateObj);
									}
									if (!sync)
									{
										if (task == null)
										{
											task = stateObj.WaitForAccumulatedWrites();
										}
										if (task != null)
										{
											Task task2 = null;
											if (completion == null)
											{
												completion = new TaskCompletionSource<object>();
												task2 = completion.Task;
											}
											Task task3 = task;
											TaskCompletionSource<object> completion2 = completion;
											Action onSuccess = delegate()
											{
												this.TdsExecuteRPC(cmd, rpcArray, timeout, inSchema, notificationRequest, stateObj, isCommandProc, sync, completion, ii, i + 1);
											};
											SqlInternalConnectionTds connHandler = this._connHandler;
											Action<Exception> onFailure;
											if ((onFailure = <>9__1) == null)
											{
												onFailure = (<>9__1 = delegate(Exception exc)
												{
													this.TdsExecuteRPC_OnFailure(exc, stateObj);
												});
											}
											AsyncHelper.ContinueTask(task3, completion2, onSuccess, connHandler, onFailure, null, null, null);
											if (flag2)
											{
												Task task4 = task2;
												Action<Task> continuationAction;
												if ((continuationAction = <>9__2) == null)
												{
													continuationAction = (<>9__2 = delegate(Task _)
													{
														this._connHandler._parserLock.Release();
													});
												}
												task4.ContinueWith(continuationAction, TaskScheduler.Default);
												flag2 = false;
											}
											return task2;
										}
									}
								}
							}
							IL_EDB:
							num5 = i;
						}
						if (ii < rpcArray.Length - 1)
						{
							if (this._isYukon)
							{
								stateObj.WriteByte(byte.MaxValue);
							}
							else
							{
								stateObj.WriteByte(128);
							}
						}
						num5 = ii;
					}
					Task task5 = stateObj.ExecuteFlush();
					if (task5 != null)
					{
						Task result = null;
						if (completion == null)
						{
							completion = new TaskCompletionSource<object>();
							result = completion.Task;
						}
						bool taskReleaseConnectionLock = flag2;
						task5.ContinueWith(delegate(Task tsk)
						{
							this.ExecuteFlushTaskCallback(tsk, stateObj, completion, taskReleaseConnectionLock);
						}, TaskScheduler.Default);
						flag2 = false;
						return result;
					}
				}
				catch (Exception e2)
				{
					if (!LocalAppContextSwitches.CleanupParserOnAllFailures && !ADP.IsCatchableExceptionType(e2))
					{
						throw;
					}
					this.FailureCleanup(stateObj, e2);
					throw;
				}
				this.FinalizeExecuteRPC(stateObj);
				if (completion != null)
				{
					completion.SetResult(null);
				}
				result2 = null;
			}
			catch (Exception ex)
			{
				this.FinalizeExecuteRPC(stateObj);
				if (completion == null)
				{
					throw ex;
				}
				completion.SetException(ex);
				result2 = null;
			}
			finally
			{
				if (flag2)
				{
					this._connHandler._parserLock.Release();
				}
			}
			return result2;
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000E88CC File Offset: 0x000E7CCC
		private void WriteEnclaveInfo(TdsParserStateObject stateObj, byte[] enclavePackage)
		{
			if (this.TceVersionSupported >= 2)
			{
				if (enclavePackage != null)
				{
					this.WriteShort((int)((short)enclavePackage.Length), stateObj);
					stateObj.WriteByteArray(enclavePackage, enclavePackage.Length, 0, true, null);
					return;
				}
				this.WriteShort(0, stateObj);
			}
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000E8908 File Offset: 0x000E7D08
		private void FinalizeExecuteRPC(TdsParserStateObject stateObj)
		{
			stateObj.SniContext = SniContext.Snix_Read;
			this._asyncWrite = false;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000E8924 File Offset: 0x000E7D24
		private void TdsExecuteRPC_OnFailure(Exception exc, TdsParserStateObject stateObj)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.FailureCleanup(stateObj, exc);
			}
			catch (OutOfMemoryException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000E89BC File Offset: 0x000E7DBC
		private void ExecuteFlushTaskCallback(Task tsk, TdsParserStateObject stateObj, TaskCompletionSource<object> completion, bool releaseConnectionLock)
		{
			try
			{
				this.FinalizeExecuteRPC(stateObj);
				if (tsk.Exception != null)
				{
					Exception exception = tsk.Exception.InnerException;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this.FailureCleanup(stateObj, tsk.Exception);
					}
					catch (OutOfMemoryException exception2)
					{
						this._connHandler.DoomThisConnection();
						completion.SetException(exception2);
						throw;
					}
					catch (StackOverflowException exception3)
					{
						this._connHandler.DoomThisConnection();
						completion.SetException(exception3);
						throw;
					}
					catch (ThreadAbortException exception4)
					{
						this._connHandler.DoomThisConnection();
						completion.SetException(exception4);
						throw;
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					completion.SetException(exception);
				}
				else
				{
					completion.SetResult(null);
				}
			}
			finally
			{
				if (releaseConnectionLock)
				{
					this._connHandler._parserLock.Release();
				}
			}
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000E8AEC File Offset: 0x000E7EEC
		private void WriteParameterName(string parameterName, TdsParserStateObject stateObj)
		{
			if (!ADP.IsEmpty(parameterName))
			{
				int num = parameterName.Length & 255;
				stateObj.WriteByte((byte)num);
				this.WriteString(parameterName, num, 0, stateObj, true);
				return;
			}
			stateObj.WriteByte(0);
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000E8B2C File Offset: 0x000E7F2C
		private void WriteSmiParameter(SqlParameter param, int paramIndex, bool sendDefault, TdsParserStateObject stateObj)
		{
			ParameterPeekAheadValue peekAhead;
			SmiParameterMetaData smiParameterMetaData = param.MetaDataForSmi(out peekAhead);
			if (!this._isKatmai)
			{
				MetaType metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(smiParameterMetaData.SqlDbType, smiParameterMetaData.IsMultiValued);
				throw ADP.VersionDoesNotSupportDataType(metaTypeFromSqlDbType.TypeName);
			}
			object value;
			ExtendedClrTypeCode typeCode;
			if (sendDefault)
			{
				if (SqlDbType.Structured == smiParameterMetaData.SqlDbType && smiParameterMetaData.IsMultiValued)
				{
					value = TdsParser.__tvpEmptyValue;
					typeCode = ExtendedClrTypeCode.IEnumerableOfSqlDataRecord;
				}
				else
				{
					value = null;
					typeCode = ExtendedClrTypeCode.DBNull;
				}
			}
			else if (param.Direction == ParameterDirection.Output)
			{
				bool paramaterIsSqlType = param.ParamaterIsSqlType;
				param.Value = null;
				value = null;
				typeCode = ExtendedClrTypeCode.DBNull;
				param.ParamaterIsSqlType = paramaterIsSqlType;
			}
			else
			{
				value = param.GetCoercedValue();
				typeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(smiParameterMetaData.SqlDbType, smiParameterMetaData.IsMultiValued, value, null, 210UL);
			}
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParser.WriteSmiParameter|ADV> %d#, Sending parameter '%ls', default flag=%d, metadata:\n", this.ObjectID, param.ParameterName, sendDefault ? 1 : 0);
				Bid.PutStr(smiParameterMetaData.TraceString(3));
				Bid.Trace("\n");
			}
			this.WriteSmiParameterMetaData(smiParameterMetaData, sendDefault, stateObj);
			TdsParameterSetter setters = new TdsParameterSetter(stateObj, smiParameterMetaData);
			ValueUtilsSmi.SetCompatibleValueV200(new SmiEventSink_Default(), setters, 0, smiParameterMetaData, value, typeCode, param.Offset, (0 < param.Size) ? param.Size : -1, peekAhead);
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000E8C50 File Offset: 0x000E8050
		private void WriteSmiParameterMetaData(SmiParameterMetaData metaData, bool sendDefault, TdsParserStateObject stateObj)
		{
			byte b = 0;
			if (ParameterDirection.Output == metaData.Direction || ParameterDirection.InputOutput == metaData.Direction)
			{
				b |= 1;
			}
			if (sendDefault)
			{
				b |= 2;
			}
			this.WriteParameterName(metaData.Name, stateObj);
			stateObj.WriteByte(b);
			this.WriteSmiTypeInfo(metaData, stateObj);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000E8C9C File Offset: 0x000E809C
		private void WriteSmiTypeInfo(SmiExtendedMetaData metaData, TdsParserStateObject stateObj)
		{
			checked
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					stateObj.WriteByte(38);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Binary:
					stateObj.WriteByte(173);
					this.WriteUnsignedShort((ushort)metaData.MaxLength, stateObj);
					return;
				case SqlDbType.Bit:
					stateObj.WriteByte(104);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Char:
					stateObj.WriteByte(175);
					this.WriteUnsignedShort((ushort)metaData.MaxLength, stateObj);
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					return;
				case SqlDbType.DateTime:
					stateObj.WriteByte(111);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Decimal:
					stateObj.WriteByte(108);
					stateObj.WriteByte((byte)MetaType.MetaDecimal.FixedLength);
					stateObj.WriteByte((metaData.Precision == 0) ? 1 : metaData.Precision);
					stateObj.WriteByte(metaData.Scale);
					return;
				case SqlDbType.Float:
					stateObj.WriteByte(109);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Image:
					stateObj.WriteByte(165);
					this.WriteUnsignedShort(ushort.MaxValue, stateObj);
					return;
				case SqlDbType.Int:
					stateObj.WriteByte(38);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Money:
					stateObj.WriteByte(110);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.NChar:
					stateObj.WriteByte(239);
					this.WriteUnsignedShort((ushort)(metaData.MaxLength * 2L), stateObj);
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					return;
				case SqlDbType.NText:
					stateObj.WriteByte(231);
					this.WriteUnsignedShort(ushort.MaxValue, stateObj);
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					return;
				case SqlDbType.NVarChar:
					stateObj.WriteByte(231);
					if (-1L == metaData.MaxLength)
					{
						this.WriteUnsignedShort(ushort.MaxValue, stateObj);
					}
					else
					{
						this.WriteUnsignedShort((ushort)(metaData.MaxLength * 2L), stateObj);
					}
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					return;
				case SqlDbType.Real:
					stateObj.WriteByte(109);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.UniqueIdentifier:
					stateObj.WriteByte(36);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.SmallDateTime:
					stateObj.WriteByte(111);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.SmallInt:
					stateObj.WriteByte(38);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.SmallMoney:
					stateObj.WriteByte(110);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Text:
					stateObj.WriteByte(167);
					this.WriteUnsignedShort(ushort.MaxValue, stateObj);
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					return;
				case SqlDbType.Timestamp:
					stateObj.WriteByte(173);
					this.WriteShort((int)metaData.MaxLength, stateObj);
					return;
				case SqlDbType.TinyInt:
					stateObj.WriteByte(38);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.VarBinary:
					stateObj.WriteByte(165);
					this.WriteUnsignedShort(unchecked((ushort)metaData.MaxLength), stateObj);
					return;
				case SqlDbType.VarChar:
					stateObj.WriteByte(167);
					this.WriteUnsignedShort(unchecked((ushort)metaData.MaxLength), stateObj);
					this.WriteUnsignedInt(this._defaultCollation.info, stateObj);
					stateObj.WriteByte(this._defaultCollation.sortId);
					return;
				case SqlDbType.Variant:
					stateObj.WriteByte(98);
					this.WriteInt((int)metaData.MaxLength, stateObj);
					return;
				case (SqlDbType)24:
				case (SqlDbType)26:
				case (SqlDbType)27:
				case (SqlDbType)28:
					break;
				case SqlDbType.Xml:
					stateObj.WriteByte(241);
					if (ADP.IsEmpty(metaData.TypeSpecificNamePart1) && ADP.IsEmpty(metaData.TypeSpecificNamePart2) && ADP.IsEmpty(metaData.TypeSpecificNamePart3))
					{
						stateObj.WriteByte(0);
						return;
					}
					stateObj.WriteByte(1);
					this.WriteIdentifier(metaData.TypeSpecificNamePart1, stateObj);
					this.WriteIdentifier(metaData.TypeSpecificNamePart2, stateObj);
					this.WriteIdentifierWithShortLength(metaData.TypeSpecificNamePart3, stateObj);
					return;
				case SqlDbType.Udt:
					stateObj.WriteByte(240);
					this.WriteIdentifier(metaData.TypeSpecificNamePart1, stateObj);
					this.WriteIdentifier(metaData.TypeSpecificNamePart2, stateObj);
					this.WriteIdentifier(metaData.TypeSpecificNamePart3, stateObj);
					return;
				case SqlDbType.Structured:
					if (metaData.IsMultiValued)
					{
						this.WriteTvpTypeInfo(metaData, stateObj);
						return;
					}
					break;
				case SqlDbType.Date:
					stateObj.WriteByte(40);
					return;
				case SqlDbType.Time:
					stateObj.WriteByte(41);
					stateObj.WriteByte(metaData.Scale);
					return;
				case SqlDbType.DateTime2:
					stateObj.WriteByte(42);
					stateObj.WriteByte(metaData.Scale);
					return;
				case SqlDbType.DateTimeOffset:
					stateObj.WriteByte(43);
					stateObj.WriteByte(metaData.Scale);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000E9184 File Offset: 0x000E8584
		private void WriteTvpTypeInfo(SmiExtendedMetaData metaData, TdsParserStateObject stateObj)
		{
			stateObj.WriteByte(243);
			this.WriteIdentifier(metaData.TypeSpecificNamePart1, stateObj);
			this.WriteIdentifier(metaData.TypeSpecificNamePart2, stateObj);
			this.WriteIdentifier(metaData.TypeSpecificNamePart3, stateObj);
			if (metaData.FieldMetaData.Count == 0)
			{
				this.WriteUnsignedShort(ushort.MaxValue, stateObj);
			}
			else
			{
				this.WriteUnsignedShort(checked((ushort)metaData.FieldMetaData.Count), stateObj);
				SmiDefaultFieldsProperty smiDefaultFieldsProperty = (SmiDefaultFieldsProperty)metaData.ExtendedProperties[SmiPropertySelector.DefaultFields];
				for (int i = 0; i < metaData.FieldMetaData.Count; i++)
				{
					this.WriteTvpColumnMetaData(metaData.FieldMetaData[i], smiDefaultFieldsProperty[i], stateObj);
				}
				this.WriteTvpOrderUnique(metaData, stateObj);
			}
			stateObj.WriteByte(0);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000E9244 File Offset: 0x000E8644
		private void WriteTvpColumnMetaData(SmiExtendedMetaData md, bool isDefault, TdsParserStateObject stateObj)
		{
			if (SqlDbType.Timestamp == md.SqlDbType)
			{
				this.WriteUnsignedInt(80U, stateObj);
			}
			else
			{
				this.WriteUnsignedInt(0U, stateObj);
			}
			ushort num = 1;
			if (isDefault)
			{
				num |= 512;
			}
			this.WriteUnsignedShort(num, stateObj);
			this.WriteSmiTypeInfo(md, stateObj);
			this.WriteIdentifier(null, stateObj);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000E9294 File Offset: 0x000E8694
		private void WriteTvpOrderUnique(SmiExtendedMetaData metaData, TdsParserStateObject stateObj)
		{
			SmiOrderProperty smiOrderProperty = (SmiOrderProperty)metaData.ExtendedProperties[SmiPropertySelector.SortOrder];
			SmiUniqueKeyProperty smiUniqueKeyProperty = (SmiUniqueKeyProperty)metaData.ExtendedProperties[SmiPropertySelector.UniqueKey];
			List<TdsParser.TdsOrderUnique> list = new List<TdsParser.TdsOrderUnique>(metaData.FieldMetaData.Count);
			for (int i = 0; i < metaData.FieldMetaData.Count; i++)
			{
				byte b = 0;
				SmiOrderProperty.SmiColumnOrder smiColumnOrder = smiOrderProperty[i];
				if (smiColumnOrder.Order == SortOrder.Ascending)
				{
					b = 1;
				}
				else if (SortOrder.Descending == smiColumnOrder.Order)
				{
					b = 2;
				}
				if (smiUniqueKeyProperty[i])
				{
					b |= 4;
				}
				if (b != 0)
				{
					list.Add(new TdsParser.TdsOrderUnique(checked((short)(i + 1)), b));
				}
			}
			if (0 < list.Count)
			{
				stateObj.WriteByte(16);
				this.WriteShort(list.Count, stateObj);
				foreach (TdsParser.TdsOrderUnique tdsOrderUnique in list)
				{
					this.WriteShort((int)tdsOrderUnique.ColumnOrdinal, stateObj);
					stateObj.WriteByte(tdsOrderUnique.Flags);
				}
			}
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000E93B4 File Offset: 0x000E87B4
		internal Task WriteBulkCopyDone(TdsParserStateObject stateObj)
		{
			if (this.State != TdsParserState.OpenNotLoggedIn && this.State != TdsParserState.OpenLoggedIn)
			{
				throw ADP.ClosedConnectionError();
			}
			stateObj.WriteByte(253);
			this.WriteShort(0, stateObj);
			this.WriteShort(0, stateObj);
			this.WriteInt(0, stateObj);
			stateObj._pendingData = true;
			stateObj._messageStatus = 0;
			return stateObj.WritePacket(1, false);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000E9414 File Offset: 0x000E8814
		internal void LoadColumnEncryptionKeys(_SqlMetaDataSet metadataCollection, string serverName)
		{
			if (this._serverSupportsColumnEncryption && this.ShouldEncryptValuesForBulkCopy())
			{
				for (int i = 0; i < metadataCollection.Length; i++)
				{
					if (metadataCollection[i] != null)
					{
						_SqlMetaData sqlMetaData = metadataCollection[i];
						if (sqlMetaData.isEncrypted)
						{
							SqlSecurityUtility.DecryptSymmetricKey(sqlMetaData.cipherMD, serverName);
						}
					}
				}
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000E9468 File Offset: 0x000E8868
		internal void WriteEncryptionEntries(ref SqlTceCipherInfoTable cekTable, TdsParserStateObject stateObj)
		{
			for (int i = 0; i < cekTable.Size; i++)
			{
				this.WriteInt(cekTable[i].DatabaseId, stateObj);
				this.WriteInt(cekTable[i].CekId, stateObj);
				this.WriteInt(cekTable[i].CekVersion, stateObj);
				stateObj.WriteByteArray(cekTable[i].CekMdVersion, 8, 0, true, null);
				stateObj.WriteByte(0);
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000E94EC File Offset: 0x000E88EC
		internal void WriteCekTable(_SqlMetaDataSet metadataCollection, TdsParserStateObject stateObj)
		{
			if (!this._serverSupportsColumnEncryption)
			{
				return;
			}
			if (metadataCollection.cekTable == null || !this.ShouldEncryptValuesForBulkCopy())
			{
				this.WriteShort(0, stateObj);
				return;
			}
			SqlTceCipherInfoTable value = metadataCollection.cekTable.Value;
			ushort v = (ushort)value.Size;
			this.WriteShort((int)v, stateObj);
			this.WriteEncryptionEntries(ref value, stateObj);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000E954C File Offset: 0x000E894C
		internal void WriteTceUserTypeAndTypeInfo(SqlMetaDataPriv mdPriv, TdsParserStateObject stateObj)
		{
			this.WriteInt(0, stateObj);
			stateObj.WriteByte(mdPriv.tdsType);
			SqlDbType type = mdPriv.type;
			if (type != SqlDbType.Decimal)
			{
				if (type != SqlDbType.Date)
				{
					if (type - SqlDbType.Time <= 2)
					{
						stateObj.WriteByte(mdPriv.scale);
						return;
					}
					this.WriteTokenLength(mdPriv.tdsType, mdPriv.length, stateObj);
					if (mdPriv.metaType.IsCharType && this._isShiloh)
					{
						this.WriteUnsignedInt(mdPriv.collation.info, stateObj);
						stateObj.WriteByte(mdPriv.collation.sortId);
					}
				}
				return;
			}
			this.WriteTokenLength(mdPriv.tdsType, mdPriv.length, stateObj);
			stateObj.WriteByte(mdPriv.precision);
			stateObj.WriteByte(mdPriv.scale);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000E9610 File Offset: 0x000E8A10
		internal void WriteCryptoMetadata(_SqlMetaData md, TdsParserStateObject stateObj)
		{
			if (!this._serverSupportsColumnEncryption || !md.isEncrypted || !this.ShouldEncryptValuesForBulkCopy())
			{
				return;
			}
			this.WriteShort((int)md.cipherMD.CekTableOrdinal, stateObj);
			this.WriteTceUserTypeAndTypeInfo(md.baseTI, stateObj);
			stateObj.WriteByte(md.cipherMD.CipherAlgorithmId);
			if (md.cipherMD.CipherAlgorithmId == 0)
			{
				stateObj.WriteByte((byte)md.cipherMD.CipherAlgorithmName.Length);
				this.WriteString(md.cipherMD.CipherAlgorithmName, stateObj, true);
			}
			stateObj.WriteByte(md.cipherMD.EncryptionType);
			stateObj.WriteByte(md.cipherMD.NormalizationRuleVersion);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000E96C0 File Offset: 0x000E8AC0
		internal void WriteBulkCopyMetaData(_SqlMetaDataSet metadataCollection, int count, TdsParserStateObject stateObj)
		{
			if (this.State != TdsParserState.OpenNotLoggedIn && this.State != TdsParserState.OpenLoggedIn)
			{
				throw ADP.ClosedConnectionError();
			}
			stateObj.WriteByte(129);
			this.WriteShort(count, stateObj);
			this.WriteCekTable(metadataCollection, stateObj);
			for (int i = 0; i < metadataCollection.Length; i++)
			{
				if (metadataCollection[i] != null)
				{
					_SqlMetaData sqlMetaData = metadataCollection[i];
					if (this.IsYukonOrNewer)
					{
						this.WriteInt(0, stateObj);
					}
					else
					{
						this.WriteShort(0, stateObj);
					}
					ushort num = (ushort)(sqlMetaData.updatability << 2);
					num |= (sqlMetaData.isNullable ? 1 : 0);
					num |= (sqlMetaData.isIdentity ? 16 : 0);
					if (this._serverSupportsColumnEncryption && this.ShouldEncryptValuesForBulkCopy())
					{
						num |= (sqlMetaData.isEncrypted ? 2048 : 0);
					}
					this.WriteShort((int)num, stateObj);
					SqlDbType type = sqlMetaData.type;
					if (type != SqlDbType.Decimal)
					{
						switch (type)
						{
						case SqlDbType.Xml:
							stateObj.WriteByteArray(TdsParser.s_xmlMetadataSubstituteSequence, TdsParser.s_xmlMetadataSubstituteSequence.Length, 0, true, null);
							goto IL_1F8;
						case SqlDbType.Udt:
							stateObj.WriteByte(165);
							this.WriteTokenLength(165, sqlMetaData.length, stateObj);
							goto IL_1F8;
						case SqlDbType.Date:
							stateObj.WriteByte(sqlMetaData.tdsType);
							goto IL_1F8;
						case SqlDbType.Time:
						case SqlDbType.DateTime2:
						case SqlDbType.DateTimeOffset:
							stateObj.WriteByte(sqlMetaData.tdsType);
							stateObj.WriteByte(sqlMetaData.scale);
							goto IL_1F8;
						}
						stateObj.WriteByte(sqlMetaData.tdsType);
						this.WriteTokenLength(sqlMetaData.tdsType, sqlMetaData.length, stateObj);
						if (sqlMetaData.metaType.IsCharType && this._isShiloh)
						{
							this.WriteUnsignedInt(sqlMetaData.collation.info, stateObj);
							stateObj.WriteByte(sqlMetaData.collation.sortId);
						}
					}
					else
					{
						stateObj.WriteByte(sqlMetaData.tdsType);
						this.WriteTokenLength(sqlMetaData.tdsType, sqlMetaData.length, stateObj);
						stateObj.WriteByte(sqlMetaData.precision);
						stateObj.WriteByte(sqlMetaData.scale);
					}
					IL_1F8:
					if (sqlMetaData.metaType.IsLong && !sqlMetaData.metaType.IsPlp)
					{
						this.WriteShort(sqlMetaData.tableName.Length, stateObj);
						this.WriteString(sqlMetaData.tableName, stateObj, true);
					}
					this.WriteCryptoMetadata(sqlMetaData, stateObj);
					stateObj.WriteByte((byte)sqlMetaData.column.Length);
					this.WriteString(sqlMetaData.column, stateObj, true);
				}
			}
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000E993C File Offset: 0x000E8D3C
		internal bool ShouldEncryptValuesForBulkCopy()
		{
			return this._connHandler != null && this._connHandler.ConnectionOptions != null && SqlConnectionColumnEncryptionSetting.Enabled == this._connHandler.ConnectionOptions.ColumnEncryptionSetting;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000E9974 File Offset: 0x000E8D74
		internal object EncryptColumnValue(object value, SqlMetaDataPriv metadata, string column, TdsParserStateObject stateObj, bool isDataFeed, bool isSqlType)
		{
			if (isDataFeed)
			{
				SQL.StreamNotSupportOnEncryptedColumn(column);
			}
			byte nullableType = metadata.baseTI.metaType.NullableType;
			int num;
			if (nullableType <= 167)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_F4;
					case 36:
						num = 16;
						goto IL_1B7;
					default:
						if (nullableType != 99)
						{
							goto IL_1AB;
						}
						goto IL_159;
					}
				}
				else if (nullableType != 165)
				{
					if (nullableType != 167)
					{
						goto IL_1AB;
					}
					goto IL_F4;
				}
			}
			else if (nullableType <= 175)
			{
				if (nullableType != 173)
				{
					if (nullableType != 175)
					{
						goto IL_1AB;
					}
					goto IL_F4;
				}
			}
			else
			{
				if (nullableType != 231 && nullableType != 239)
				{
					goto IL_1AB;
				}
				goto IL_159;
			}
			num = (isSqlType ? ((SqlBinary)value).Length : ((byte[])value).Length);
			if (metadata.baseTI.length > 0 && num > metadata.baseTI.length)
			{
				num = metadata.baseTI.length;
				goto IL_1B7;
			}
			goto IL_1B7;
			IL_F4:
			if (this._defaultEncoding == null)
			{
				this.ThrowUnsupportedCollationEncountered(null);
			}
			string s = isSqlType ? ((SqlString)value).Value : ((string)value);
			num = this._defaultEncoding.GetByteCount(s);
			if (metadata.baseTI.length > 0 && num > metadata.baseTI.length)
			{
				num = metadata.baseTI.length;
				goto IL_1B7;
			}
			goto IL_1B7;
			IL_159:
			num = (isSqlType ? ((SqlString)value).Value.Length : ((string)value).Length) * 2;
			if (metadata.baseTI.length > 0 && num > metadata.baseTI.length)
			{
				num = metadata.baseTI.length;
				goto IL_1B7;
			}
			goto IL_1B7;
			IL_1AB:
			num = metadata.baseTI.length;
			IL_1B7:
			byte[] plainText;
			if (isSqlType)
			{
				plainText = this.SerializeUnencryptedSqlValue(value, metadata.baseTI.metaType, num, 0, metadata.cipherMD.NormalizationRuleVersion, stateObj);
			}
			else
			{
				plainText = this.SerializeUnencryptedValue(value, metadata.baseTI.metaType, metadata.baseTI.scale, num, 0, isDataFeed, metadata.cipherMD.NormalizationRuleVersion, stateObj);
			}
			return SqlSecurityUtility.EncryptWithKey(plainText, metadata.cipherMD, this._connHandler.ConnectionOptions.DataSource);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000E9BAC File Offset: 0x000E8FAC
		internal Task WriteBulkCopyValue(object value, SqlMetaDataPriv metadata, TdsParserStateObject stateObj, bool isSqlType, bool isDataFeed, bool isNull)
		{
			Encoding defaultEncoding = this._defaultEncoding;
			SqlCollation defaultCollation = this._defaultCollation;
			int defaultCodePage = this._defaultCodePage;
			int defaultLCID = this._defaultLCID;
			Task result = null;
			Task task = null;
			if (this.State != TdsParserState.OpenNotLoggedIn && this.State != TdsParserState.OpenLoggedIn)
			{
				throw ADP.ClosedConnectionError();
			}
			try
			{
				if (metadata.encoding != null)
				{
					this._defaultEncoding = metadata.encoding;
				}
				if (metadata.collation != null)
				{
					this._defaultCollation = metadata.collation;
					this._defaultLCID = this._defaultCollation.LCID;
				}
				this._defaultCodePage = metadata.codePage;
				MetaType metaType = metadata.metaType;
				int num = 0;
				int num2 = 0;
				if (isNull)
				{
					if (metaType.IsPlp && (metaType.NullableType != 240 || metaType.IsLong))
					{
						this.WriteLong(-1L, stateObj);
					}
					else if (!metaType.IsFixed && !metaType.IsLong && !metaType.IsVarTime)
					{
						this.WriteShort(65535, stateObj);
					}
					else
					{
						stateObj.WriteByte(0);
					}
					return result;
				}
				if (!isDataFeed)
				{
					byte nullableType = metaType.NullableType;
					if (nullableType <= 167)
					{
						if (nullableType <= 99)
						{
							switch (nullableType)
							{
							case 34:
								break;
							case 35:
								goto IL_1B4;
							case 36:
								num = 16;
								goto IL_26E;
							default:
								if (nullableType != 99)
								{
									goto IL_267;
								}
								goto IL_1FE;
							}
						}
						else if (nullableType != 165)
						{
							if (nullableType != 167)
							{
								goto IL_267;
							}
							goto IL_1B4;
						}
					}
					else if (nullableType <= 175)
					{
						if (nullableType != 173)
						{
							if (nullableType != 175)
							{
								goto IL_267;
							}
							goto IL_1B4;
						}
					}
					else
					{
						if (nullableType == 231)
						{
							goto IL_1FE;
						}
						switch (nullableType)
						{
						case 239:
							goto IL_1FE;
						case 240:
							break;
						case 241:
							if (value is XmlReader)
							{
								value = MetaType.GetStringFromXml((XmlReader)value);
							}
							num = (isSqlType ? ((SqlString)value).Value.Length : ((string)value).Length) * 2;
							goto IL_26E;
						default:
							goto IL_267;
						}
					}
					num = (isSqlType ? ((SqlBinary)value).Length : ((byte[])value).Length);
					goto IL_26E;
					IL_1B4:
					if (this._defaultEncoding == null)
					{
						this.ThrowUnsupportedCollationEncountered(null);
					}
					string text;
					if (isSqlType)
					{
						text = ((SqlString)value).Value;
					}
					else
					{
						text = (string)value;
					}
					num = text.Length;
					num2 = this._defaultEncoding.GetByteCount(text);
					goto IL_26E;
					IL_1FE:
					num = (isSqlType ? ((SqlString)value).Value.Length : ((string)value).Length) * 2;
					goto IL_26E;
					IL_267:
					num = metadata.length;
				}
				IL_26E:
				if (metaType.IsLong)
				{
					SqlDbType sqlDbType = metaType.SqlDbType;
					if (sqlDbType <= SqlDbType.NVarChar)
					{
						if (sqlDbType != SqlDbType.Image && sqlDbType != SqlDbType.NText)
						{
							if (sqlDbType != SqlDbType.NVarChar)
							{
								goto IL_304;
							}
							goto IL_2E2;
						}
					}
					else if (sqlDbType <= SqlDbType.VarChar)
					{
						if (sqlDbType != SqlDbType.Text)
						{
							if (sqlDbType - SqlDbType.VarBinary > 1)
							{
								goto IL_304;
							}
							goto IL_2E2;
						}
					}
					else
					{
						if (sqlDbType != SqlDbType.Xml && sqlDbType != SqlDbType.Udt)
						{
							goto IL_304;
						}
						goto IL_2E2;
					}
					stateObj.WriteByteArray(TdsParser.s_longDataHeader, TdsParser.s_longDataHeader.Length, 0, true, null);
					this.WriteTokenLength(metadata.tdsType, (num2 == 0) ? num : num2, stateObj);
					goto IL_304;
					IL_2E2:
					this.WriteUnsignedLong(18446744073709551614UL, stateObj);
				}
				else
				{
					this.WriteTokenLength(metadata.tdsType, (num2 == 0) ? num : num2, stateObj);
				}
				IL_304:
				if (isSqlType)
				{
					task = this.WriteSqlValue(value, metaType, num, num2, 0, stateObj);
				}
				else if (metaType.SqlDbType != SqlDbType.Udt || metaType.IsLong)
				{
					task = this.WriteValue(value, metaType, metadata.scale, num, num2, 0, stateObj, metadata.length, isDataFeed);
					if (task == null && this._asyncWrite)
					{
						task = stateObj.WaitForAccumulatedWrites();
					}
				}
				else
				{
					this.WriteShort(num, stateObj);
					task = stateObj.WriteByteArray((byte[])value, num, 0, true, null);
				}
				if (task != null)
				{
					result = this.WriteBulkCopyValueSetupContinuation(task, defaultEncoding, defaultCollation, defaultCodePage, defaultLCID);
				}
			}
			finally
			{
				if (task == null)
				{
					this._defaultEncoding = defaultEncoding;
					this._defaultCollation = defaultCollation;
					this._defaultCodePage = defaultCodePage;
					this._defaultLCID = defaultLCID;
				}
			}
			return result;
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000E9F90 File Offset: 0x000E9390
		private Task WriteBulkCopyValueSetupContinuation(Task internalWriteTask, Encoding saveEncoding, SqlCollation saveCollation, int saveCodePage, int saveLCID)
		{
			return internalWriteTask.ContinueWith<Task>(delegate(Task t)
			{
				this._defaultEncoding = saveEncoding;
				this._defaultCollation = saveCollation;
				this._defaultCodePage = saveCodePage;
				this._defaultLCID = saveLCID;
				return t;
			}, TaskScheduler.Default).Unwrap();
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000E9FE4 File Offset: 0x000E93E4
		private void WriteMarsHeaderData(TdsParserStateObject stateObj, SqlInternalTransaction transaction)
		{
			this.WriteShort(2, stateObj);
			if (transaction != null && transaction.TransactionId != 0L)
			{
				this.WriteLong(transaction.TransactionId, stateObj);
				this.WriteInt(stateObj.IncrementAndObtainOpenResultCount(transaction), stateObj);
				return;
			}
			this.WriteLong(this._retainedTransactionId, stateObj);
			this.WriteInt(stateObj.IncrementAndObtainOpenResultCount(null), stateObj);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000EA03C File Offset: 0x000E943C
		private int GetNotificationHeaderSize(SqlNotificationRequest notificationRequest)
		{
			if (notificationRequest == null)
			{
				return 0;
			}
			string userData = notificationRequest.UserData;
			string options = notificationRequest.Options;
			int timeout = notificationRequest.Timeout;
			if (userData == null)
			{
				throw ADP.ArgumentNull("CallbackId");
			}
			if (65535 < userData.Length)
			{
				throw ADP.ArgumentOutOfRange("CallbackId");
			}
			if (options == null)
			{
				throw ADP.ArgumentNull("Service");
			}
			if (65535 < options.Length)
			{
				throw ADP.ArgumentOutOfRange("Service");
			}
			if (-1 > timeout)
			{
				throw ADP.ArgumentOutOfRange("Timeout");
			}
			int num = 8 + userData.Length * 2 + 2 + options.Length * 2;
			if (timeout > 0)
			{
				num += 4;
			}
			return num;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000EA0E0 File Offset: 0x000E94E0
		private void WriteQueryNotificationHeaderData(SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj)
		{
			string userData = notificationRequest.UserData;
			string options = notificationRequest.Options;
			int timeout = notificationRequest.Timeout;
			Bid.NotificationsTrace("<sc.TdsParser.WriteQueryNotificationHeader|DEP> NotificationRequest: userData: '%ls', options: '%ls', timeout: '%d'\n", notificationRequest.UserData, notificationRequest.Options, notificationRequest.Timeout);
			this.WriteShort(1, stateObj);
			this.WriteShort(userData.Length * 2, stateObj);
			this.WriteString(userData, stateObj, true);
			this.WriteShort(options.Length * 2, stateObj);
			this.WriteString(options, stateObj, true);
			if (timeout > 0)
			{
				this.WriteInt(timeout, stateObj);
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000EA164 File Offset: 0x000E9564
		private void WriteTraceHeaderData(TdsParserStateObject stateObj)
		{
			ActivityCorrelator.ActivityId activityId = ActivityCorrelator.Current;
			this.WriteShort(3, stateObj);
			stateObj.WriteByteArray(activityId.Id.ToByteArray(), 16, 0, true, null);
			this.WriteUnsignedInt(activityId.Sequence, stateObj);
			Bid.Trace("<sc.TdsParser.WriteTraceHeaderData|INFO> ActivityID %ls\n", activityId.ToString());
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000EA1B8 File Offset: 0x000E95B8
		private void WriteRPCBatchHeaders(TdsParserStateObject stateObj, SqlNotificationRequest notificationRequest)
		{
			int notificationHeaderSize = this.GetNotificationHeaderSize(notificationRequest);
			int v = this.IncludeTraceHeader ? (22 + notificationHeaderSize + 26) : (22 + notificationHeaderSize);
			this.WriteInt(v, stateObj);
			this.WriteInt(18, stateObj);
			this.WriteMarsHeaderData(stateObj, this.CurrentTransaction);
			if (notificationHeaderSize != 0)
			{
				this.WriteInt(notificationHeaderSize, stateObj);
				this.WriteQueryNotificationHeaderData(notificationRequest, stateObj);
			}
			if (this.IncludeTraceHeader)
			{
				this.WriteInt(26, stateObj);
				this.WriteTraceHeaderData(stateObj);
			}
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000EA22C File Offset: 0x000E962C
		private void WriteTokenLength(byte token, int length, TdsParserStateObject stateObj)
		{
			int num = 0;
			if (this._isYukon)
			{
				if (240 == token)
				{
					num = 8;
				}
				else if (token == 241)
				{
					num = 8;
				}
			}
			if (num == 0)
			{
				int num2 = (int)(token & 48);
				if (num2 <= 16)
				{
					if (num2 != 0)
					{
						if (num2 != 16)
						{
							goto IL_65;
						}
						num = 0;
						goto IL_65;
					}
				}
				else if (num2 != 32)
				{
					if (num2 == 48)
					{
						num = 0;
						goto IL_65;
					}
					goto IL_65;
				}
				if ((token & 128) != 0)
				{
					num = 2;
				}
				else if ((token & 12) == 0)
				{
					num = 4;
				}
				else
				{
					num = 1;
				}
				IL_65:
				switch (num)
				{
				case 1:
					stateObj.WriteByte((byte)length);
					return;
				case 2:
					this.WriteShort(length, stateObj);
					return;
				case 3:
					break;
				case 4:
					this.WriteInt(length, stateObj);
					return;
				default:
					if (num != 8)
					{
						return;
					}
					this.WriteShort(65535, stateObj);
					break;
				}
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000EA2E4 File Offset: 0x000E96E4
		private bool IsBOMNeeded(MetaType type, object value)
		{
			if (type.NullableType == 241)
			{
				Type type2 = value.GetType();
				if (type2 == typeof(SqlString))
				{
					if (!((SqlString)value).IsNull && ((SqlString)value).Value.Length > 0 && (((SqlString)value).Value[0] & 'ÿ') != 'ÿ')
					{
						return true;
					}
				}
				else if (type2 == typeof(string) && ((string)value).Length > 0)
				{
					if (value != null && (((string)value)[0] & 'ÿ') != 'ÿ')
					{
						return true;
					}
				}
				else if (type2 == typeof(SqlXml))
				{
					if (!((SqlXml)value).IsNull)
					{
						return true;
					}
				}
				else if (type2 == typeof(XmlDataFeed))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000EA3E0 File Offset: 0x000E97E0
		private Task GetTerminationTask(Task unterminatedWriteTask, object value, MetaType type, int actualLength, TdsParserStateObject stateObj, bool isDataFeed)
		{
			if (!type.IsPlp || (actualLength <= 0 && !isDataFeed))
			{
				return unterminatedWriteTask;
			}
			if (unterminatedWriteTask == null)
			{
				this.WriteInt(0, stateObj);
				return null;
			}
			return AsyncHelper.CreateContinuationTask<int, TdsParserStateObject>(unterminatedWriteTask, new Action<int, TdsParserStateObject>(this.WriteInt), 0, stateObj, this._connHandler, null);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000EA42C File Offset: 0x000E982C
		private Task WriteSqlValue(object value, MetaType type, int actualLength, int codePageByteSize, int offset, TdsParserStateObject stateObj)
		{
			return this.GetTerminationTask(this.WriteUnterminatedSqlValue(value, type, actualLength, codePageByteSize, offset, stateObj), value, type, actualLength, stateObj, false);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000EA454 File Offset: 0x000E9854
		private Task WriteUnterminatedSqlValue(object value, MetaType type, int actualLength, int codePageByteSize, int offset, TdsParserStateObject stateObj)
		{
			byte nullableType = type.NullableType;
			if (nullableType <= 165)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_22F;
					case 36:
					{
						byte[] b = ((SqlGuid)value).ToByteArray();
						stateObj.WriteByteArray(b, actualLength, 0, true, null);
						goto IL_3C3;
					}
					case 37:
						goto IL_3C3;
					case 38:
						if (type.FixedLength == 1)
						{
							stateObj.WriteByte(((SqlByte)value).Value);
							goto IL_3C3;
						}
						if (type.FixedLength == 2)
						{
							this.WriteShort((int)((SqlInt16)value).Value, stateObj);
							goto IL_3C3;
						}
						if (type.FixedLength == 4)
						{
							this.WriteInt(((SqlInt32)value).Value, stateObj);
							goto IL_3C3;
						}
						this.WriteLong(((SqlInt64)value).Value, stateObj);
						goto IL_3C3;
					default:
						if (nullableType != 99)
						{
							goto IL_3C3;
						}
						goto IL_28F;
					}
				}
				else
				{
					switch (nullableType)
					{
					case 104:
						if (((SqlBoolean)value).Value)
						{
							stateObj.WriteByte(1);
							goto IL_3C3;
						}
						stateObj.WriteByte(0);
						goto IL_3C3;
					case 105:
					case 106:
					case 107:
						goto IL_3C3;
					case 108:
						this.WriteSqlDecimal((SqlDecimal)value, stateObj);
						goto IL_3C3;
					case 109:
						if (type.FixedLength == 4)
						{
							this.WriteFloat(((SqlSingle)value).Value, stateObj);
							goto IL_3C3;
						}
						this.WriteDouble(((SqlDouble)value).Value, stateObj);
						goto IL_3C3;
					case 110:
						this.WriteSqlMoney((SqlMoney)value, type.FixedLength, stateObj);
						goto IL_3C3;
					case 111:
					{
						SqlDateTime sqlDateTime = (SqlDateTime)value;
						if (type.FixedLength != 4)
						{
							this.WriteInt(sqlDateTime.DayTicks, stateObj);
							this.WriteInt(sqlDateTime.TimeTicks, stateObj);
							goto IL_3C3;
						}
						if (0 > sqlDateTime.DayTicks || sqlDateTime.DayTicks > 65535)
						{
							throw SQL.SmallDateTimeOverflow(sqlDateTime.ToString());
						}
						this.WriteShort(sqlDateTime.DayTicks, stateObj);
						this.WriteShort(sqlDateTime.TimeTicks / SqlDateTime.SQLTicksPerMinute, stateObj);
						goto IL_3C3;
					}
					default:
						if (nullableType != 165)
						{
							goto IL_3C3;
						}
						break;
					}
				}
			}
			else if (nullableType <= 173)
			{
				if (nullableType == 167)
				{
					goto IL_22F;
				}
				if (nullableType != 173)
				{
					goto IL_3C3;
				}
			}
			else
			{
				if (nullableType == 175)
				{
					goto IL_22F;
				}
				if (nullableType == 231)
				{
					goto IL_28F;
				}
				switch (nullableType)
				{
				case 239:
				case 241:
					goto IL_28F;
				case 240:
					throw SQL.UDTUnexpectedResult(value.GetType().AssemblyQualifiedName);
				default:
					goto IL_3C3;
				}
			}
			if (type.IsPlp)
			{
				this.WriteInt(actualLength, stateObj);
			}
			if (value is SqlBinary)
			{
				return stateObj.WriteByteArray(((SqlBinary)value).Value, actualLength, offset, false, null);
			}
			return stateObj.WriteByteArray(((SqlBytes)value).Value, actualLength, offset, false, null);
			IL_22F:
			if (type.IsPlp)
			{
				this.WriteInt(codePageByteSize, stateObj);
			}
			if (value is SqlChars)
			{
				string s = new string(((SqlChars)value).Value);
				return this.WriteEncodingChar(s, actualLength, offset, this._defaultEncoding, stateObj, false);
			}
			return this.WriteEncodingChar(((SqlString)value).Value, actualLength, offset, this._defaultEncoding, stateObj, false);
			IL_28F:
			if (type.IsPlp)
			{
				if (this.IsBOMNeeded(type, value))
				{
					this.WriteInt(actualLength + 2, stateObj);
					this.WriteShort(65279, stateObj);
				}
				else
				{
					this.WriteInt(actualLength, stateObj);
				}
			}
			if (actualLength != 0)
			{
				actualLength >>= 1;
			}
			if (value is SqlChars)
			{
				return this.WriteCharArray(((SqlChars)value).Value, actualLength, offset, stateObj, false);
			}
			return this.WriteString(((SqlString)value).Value, actualLength, offset, stateObj, false);
			IL_3C3:
			return null;
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x000EA828 File Offset: 0x000E9C28
		private Task WriteXmlFeed(XmlDataFeed feed, TdsParserStateObject stateObj, bool needBom, Encoding encoding, int size)
		{
			TdsParser.<WriteXmlFeed>d__275 <WriteXmlFeed>d__;
			<WriteXmlFeed>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteXmlFeed>d__.<>4__this = this;
			<WriteXmlFeed>d__.feed = feed;
			<WriteXmlFeed>d__.stateObj = stateObj;
			<WriteXmlFeed>d__.needBom = needBom;
			<WriteXmlFeed>d__.encoding = encoding;
			<WriteXmlFeed>d__.size = size;
			<WriteXmlFeed>d__.<>1__state = -1;
			<WriteXmlFeed>d__.<>t__builder.Start<TdsParser.<WriteXmlFeed>d__275>(ref <WriteXmlFeed>d__);
			return <WriteXmlFeed>d__.<>t__builder.Task;
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x000EA898 File Offset: 0x000E9C98
		private Task WriteTextFeed(TextDataFeed feed, Encoding encoding, bool needBom, TdsParserStateObject stateObj, int size)
		{
			TdsParser.<WriteTextFeed>d__276 <WriteTextFeed>d__;
			<WriteTextFeed>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteTextFeed>d__.<>4__this = this;
			<WriteTextFeed>d__.feed = feed;
			<WriteTextFeed>d__.encoding = encoding;
			<WriteTextFeed>d__.needBom = needBom;
			<WriteTextFeed>d__.stateObj = stateObj;
			<WriteTextFeed>d__.size = size;
			<WriteTextFeed>d__.<>1__state = -1;
			<WriteTextFeed>d__.<>t__builder.Start<TdsParser.<WriteTextFeed>d__276>(ref <WriteTextFeed>d__);
			return <WriteTextFeed>d__.<>t__builder.Task;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000EA908 File Offset: 0x000E9D08
		private Task WriteStreamFeed(StreamDataFeed feed, TdsParserStateObject stateObj, int len)
		{
			TdsParser.<WriteStreamFeed>d__277 <WriteStreamFeed>d__;
			<WriteStreamFeed>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStreamFeed>d__.<>4__this = this;
			<WriteStreamFeed>d__.feed = feed;
			<WriteStreamFeed>d__.stateObj = stateObj;
			<WriteStreamFeed>d__.len = len;
			<WriteStreamFeed>d__.<>1__state = -1;
			<WriteStreamFeed>d__.<>t__builder.Start<TdsParser.<WriteStreamFeed>d__277>(ref <WriteStreamFeed>d__);
			return <WriteStreamFeed>d__.<>t__builder.Task;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000EA964 File Offset: 0x000E9D64
		private Task NullIfCompletedWriteTask(Task task)
		{
			if (task == null)
			{
				return null;
			}
			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
				return null;
			case TaskStatus.Canceled:
				throw SQL.OperationCancelled();
			case TaskStatus.Faulted:
				throw task.Exception.InnerException;
			default:
				return task;
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000EA9A8 File Offset: 0x000E9DA8
		private Task WriteValue(object value, MetaType type, byte scale, int actualLength, int encodingByteSize, int offset, TdsParserStateObject stateObj, int paramSize, bool isDataFeed)
		{
			return this.GetTerminationTask(this.WriteUnterminatedValue(value, type, scale, actualLength, encodingByteSize, offset, stateObj, paramSize, isDataFeed), value, type, actualLength, stateObj, isDataFeed);
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000EA9D8 File Offset: 0x000E9DD8
		private Task WriteUnterminatedValue(object value, MetaType type, byte scale, int actualLength, int encodingByteSize, int offset, TdsParserStateObject stateObj, int paramSize, bool isDataFeed)
		{
			byte nullableType = type.NullableType;
			if (nullableType <= 165)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_1FB;
					case 36:
					{
						byte[] b = ((Guid)value).ToByteArray();
						stateObj.WriteByteArray(b, actualLength, 0, true, null);
						goto IL_45D;
					}
					case 37:
					case 39:
						goto IL_45D;
					case 38:
						if (type.FixedLength == 1)
						{
							stateObj.WriteByte((byte)value);
							goto IL_45D;
						}
						if (type.FixedLength == 2)
						{
							this.WriteShort((int)((short)value), stateObj);
							goto IL_45D;
						}
						if (type.FixedLength == 4)
						{
							this.WriteInt((int)value, stateObj);
							goto IL_45D;
						}
						this.WriteLong((long)value, stateObj);
						goto IL_45D;
					case 40:
						this.WriteDate((DateTime)value, stateObj);
						goto IL_45D;
					case 41:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						this.WriteTime((TimeSpan)value, scale, actualLength, stateObj);
						goto IL_45D;
					case 42:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						this.WriteDateTime2((DateTime)value, scale, actualLength, stateObj);
						goto IL_45D;
					case 43:
						this.WriteDateTimeOffset((DateTimeOffset)value, scale, actualLength, stateObj);
						goto IL_45D;
					default:
						if (nullableType != 99)
						{
							goto IL_45D;
						}
						goto IL_287;
					}
				}
				else
				{
					switch (nullableType)
					{
					case 104:
						if ((bool)value)
						{
							stateObj.WriteByte(1);
							goto IL_45D;
						}
						stateObj.WriteByte(0);
						goto IL_45D;
					case 105:
					case 106:
					case 107:
						goto IL_45D;
					case 108:
						this.WriteDecimal((decimal)value, stateObj);
						goto IL_45D;
					case 109:
						if (type.FixedLength == 4)
						{
							this.WriteFloat((float)value, stateObj);
							goto IL_45D;
						}
						this.WriteDouble((double)value, stateObj);
						goto IL_45D;
					case 110:
						this.WriteCurrency((decimal)value, type.FixedLength, stateObj);
						goto IL_45D;
					case 111:
					{
						TdsDateTime tdsDateTime = MetaType.FromDateTime((DateTime)value, (byte)type.FixedLength);
						if (type.FixedLength != 4)
						{
							this.WriteInt(tdsDateTime.days, stateObj);
							this.WriteInt(tdsDateTime.time, stateObj);
							goto IL_45D;
						}
						if (0 > tdsDateTime.days || tdsDateTime.days > 65535)
						{
							throw SQL.SmallDateTimeOverflow(MetaType.ToDateTime(tdsDateTime.days, tdsDateTime.time, 4).ToString(CultureInfo.InvariantCulture));
						}
						this.WriteShort(tdsDateTime.days, stateObj);
						this.WriteShort(tdsDateTime.time, stateObj);
						goto IL_45D;
					}
					default:
						if (nullableType != 165)
						{
							goto IL_45D;
						}
						break;
					}
				}
			}
			else if (nullableType <= 173)
			{
				if (nullableType == 167)
				{
					goto IL_1FB;
				}
				if (nullableType != 173)
				{
					goto IL_45D;
				}
			}
			else
			{
				if (nullableType == 175)
				{
					goto IL_1FB;
				}
				if (nullableType == 231)
				{
					goto IL_287;
				}
				switch (nullableType)
				{
				case 239:
				case 241:
					goto IL_287;
				case 240:
					break;
				default:
					goto IL_45D;
				}
			}
			if (isDataFeed)
			{
				return this.NullIfCompletedWriteTask(this.WriteStreamFeed((StreamDataFeed)value, stateObj, paramSize));
			}
			if (type.IsPlp)
			{
				this.WriteInt(actualLength, stateObj);
			}
			return stateObj.WriteByteArray((byte[])value, actualLength, offset, false, null);
			IL_1FB:
			if (isDataFeed)
			{
				TextDataFeed textDataFeed = value as TextDataFeed;
				if (textDataFeed == null)
				{
					return this.NullIfCompletedWriteTask(this.WriteXmlFeed((XmlDataFeed)value, stateObj, true, this._defaultEncoding, paramSize));
				}
				return this.NullIfCompletedWriteTask(this.WriteTextFeed(textDataFeed, this._defaultEncoding, false, stateObj, paramSize));
			}
			else
			{
				if (type.IsPlp)
				{
					this.WriteInt(encodingByteSize, stateObj);
				}
				if (value is byte[])
				{
					return stateObj.WriteByteArray((byte[])value, actualLength, 0, false, null);
				}
				return this.WriteEncodingChar((string)value, actualLength, offset, this._defaultEncoding, stateObj, false);
			}
			IL_287:
			if (isDataFeed)
			{
				TextDataFeed textDataFeed2 = value as TextDataFeed;
				if (textDataFeed2 == null)
				{
					return this.NullIfCompletedWriteTask(this.WriteXmlFeed((XmlDataFeed)value, stateObj, this.IsBOMNeeded(type, value), Encoding.Unicode, paramSize));
				}
				return this.NullIfCompletedWriteTask(this.WriteTextFeed(textDataFeed2, null, this.IsBOMNeeded(type, value), stateObj, paramSize));
			}
			else
			{
				if (type.IsPlp)
				{
					if (this.IsBOMNeeded(type, value))
					{
						this.WriteInt(actualLength + 2, stateObj);
						this.WriteShort(65279, stateObj);
					}
					else
					{
						this.WriteInt(actualLength, stateObj);
					}
				}
				if (value is byte[])
				{
					return stateObj.WriteByteArray((byte[])value, actualLength, 0, false, null);
				}
				actualLength >>= 1;
				return this.WriteString((string)value, actualLength, offset, stateObj, false);
			}
			IL_45D:
			return null;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000EAE44 File Offset: 0x000EA244
		private Task WriteEncryptionMetadata(Task terminatedWriteTask, SqlColumnEncryptionInputParameterInfo columnEncryptionParameterInfo, TdsParserStateObject stateObj)
		{
			if (terminatedWriteTask == null)
			{
				this.WriteEncryptionMetadata(columnEncryptionParameterInfo, stateObj);
				return null;
			}
			return AsyncHelper.CreateContinuationTask<SqlColumnEncryptionInputParameterInfo, TdsParserStateObject>(terminatedWriteTask, new Action<SqlColumnEncryptionInputParameterInfo, TdsParserStateObject>(this.WriteEncryptionMetadata), columnEncryptionParameterInfo, stateObj, this._connHandler, null);
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000EAE7C File Offset: 0x000EA27C
		private void WriteEncryptionMetadata(SqlColumnEncryptionInputParameterInfo columnEncryptionParameterInfo, TdsParserStateObject stateObj)
		{
			this.WriteSmiTypeInfo(columnEncryptionParameterInfo.ParameterMetadata, stateObj);
			stateObj.WriteByteArray(columnEncryptionParameterInfo.SerializedWireFormat, columnEncryptionParameterInfo.SerializedWireFormat.Length, 0, true, null);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000EAEB0 File Offset: 0x000EA2B0
		private byte[] SerializeUnencryptedValue(object value, MetaType type, byte scale, int actualLength, int offset, bool isDataFeed, byte normalizationVersion, TdsParserStateObject stateObj)
		{
			if (normalizationVersion != 1)
			{
				throw SQL.UnsupportedNormalizationVersion(normalizationVersion);
			}
			byte nullableType = type.NullableType;
			if (nullableType <= 165)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_1AD;
					case 36:
						return ((Guid)value).ToByteArray();
					case 37:
					case 39:
						goto IL_3C2;
					case 38:
						if (type.FixedLength == 1)
						{
							return this.SerializeLong((long)((ulong)((byte)value)), stateObj);
						}
						if (type.FixedLength == 2)
						{
							return this.SerializeLong((long)((short)value), stateObj);
						}
						if (type.FixedLength == 4)
						{
							return this.SerializeLong((long)((int)value), stateObj);
						}
						return this.SerializeLong((long)value, stateObj);
					case 40:
						return this.SerializeDate((DateTime)value);
					case 41:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						return this.SerializeTime((TimeSpan)value, scale, actualLength);
					case 42:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						return this.SerializeDateTime2((DateTime)value, scale, actualLength);
					case 43:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						return this.SerializeDateTimeOffset((DateTimeOffset)value, scale, actualLength);
					default:
						if (nullableType != 99)
						{
							goto IL_3C2;
						}
						goto IL_1E9;
					}
				}
				else
				{
					switch (nullableType)
					{
					case 104:
						return this.SerializeLong(((bool)value) ? 1L : 0L, stateObj);
					case 105:
					case 106:
					case 107:
						goto IL_3C2;
					case 108:
						return this.SerializeDecimal((decimal)value, stateObj);
					case 109:
						if (type.FixedLength == 4)
						{
							return this.SerializeFloat((float)value);
						}
						return this.SerializeDouble((double)value);
					case 110:
						return this.SerializeCurrency((decimal)value, type.FixedLength, stateObj);
					case 111:
					{
						TdsDateTime tdsDateTime = MetaType.FromDateTime((DateTime)value, (byte)type.FixedLength);
						if (type.FixedLength != 4)
						{
							if (stateObj._bLongBytes == null)
							{
								stateObj._bLongBytes = new byte[8];
							}
							byte[] bLongBytes = stateObj._bLongBytes;
							int num = 0;
							byte[] src = this.SerializeInt(tdsDateTime.days, stateObj);
							Buffer.BlockCopy(src, 0, bLongBytes, num, 4);
							num += 4;
							src = this.SerializeInt(tdsDateTime.time, stateObj);
							Buffer.BlockCopy(src, 0, bLongBytes, num, 4);
							return bLongBytes;
						}
						if (0 > tdsDateTime.days || tdsDateTime.days > 65535)
						{
							throw SQL.SmallDateTimeOverflow(MetaType.ToDateTime(tdsDateTime.days, tdsDateTime.time, 4).ToString(CultureInfo.InvariantCulture));
						}
						if (stateObj._bIntBytes == null)
						{
							stateObj._bIntBytes = new byte[4];
						}
						byte[] bIntBytes = stateObj._bIntBytes;
						int num2 = 0;
						byte[] src2 = this.SerializeShort(tdsDateTime.days, stateObj);
						Buffer.BlockCopy(src2, 0, bIntBytes, num2, 2);
						num2 += 2;
						src2 = this.SerializeShort(tdsDateTime.time, stateObj);
						Buffer.BlockCopy(src2, 0, bIntBytes, num2, 2);
						return bIntBytes;
					}
					default:
						if (nullableType != 165)
						{
							goto IL_3C2;
						}
						break;
					}
				}
			}
			else if (nullableType <= 173)
			{
				if (nullableType == 167)
				{
					goto IL_1AD;
				}
				if (nullableType != 173)
				{
					goto IL_3C2;
				}
			}
			else
			{
				if (nullableType == 175)
				{
					goto IL_1AD;
				}
				if (nullableType == 231)
				{
					goto IL_1E9;
				}
				switch (nullableType)
				{
				case 239:
				case 241:
					goto IL_1E9;
				case 240:
					break;
				default:
					goto IL_3C2;
				}
			}
			byte[] array = new byte[actualLength];
			Buffer.BlockCopy((byte[])value, offset, array, 0, actualLength);
			return array;
			IL_1AD:
			if (value is byte[])
			{
				byte[] array2 = new byte[actualLength];
				Buffer.BlockCopy((byte[])value, 0, array2, 0, actualLength);
				return array2;
			}
			return this.SerializeEncodingChar((string)value, actualLength, offset, this._defaultEncoding);
			IL_1E9:
			if (value is byte[])
			{
				byte[] array3 = new byte[actualLength];
				Buffer.BlockCopy((byte[])value, 0, array3, 0, actualLength);
				return array3;
			}
			actualLength >>= 1;
			return this.SerializeString((string)value, actualLength, offset);
			IL_3C2:
			throw SQL.UnsupportedDatatypeEncryption(type.TypeName);
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000EB28C File Offset: 0x000EA68C
		private byte[] SerializeUnencryptedSqlValue(object value, MetaType type, int actualLength, int offset, byte normalizationVersion, TdsParserStateObject stateObj)
		{
			if (normalizationVersion != 1)
			{
				throw SQL.UnsupportedNormalizationVersion(normalizationVersion);
			}
			byte nullableType = type.NullableType;
			if (nullableType <= 167)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_200;
					case 36:
						return ((SqlGuid)value).ToByteArray();
					case 37:
						goto IL_3B2;
					case 38:
						if (type.FixedLength == 1)
						{
							return this.SerializeLong((long)((ulong)((SqlByte)value).Value), stateObj);
						}
						if (type.FixedLength == 2)
						{
							return this.SerializeLong((long)((SqlInt16)value).Value, stateObj);
						}
						if (type.FixedLength == 4)
						{
							return this.SerializeLong((long)((SqlInt32)value).Value, stateObj);
						}
						return this.SerializeLong(((SqlInt64)value).Value, stateObj);
					default:
						if (nullableType != 99)
						{
							goto IL_3B2;
						}
						goto IL_24B;
					}
				}
				else
				{
					switch (nullableType)
					{
					case 104:
						return this.SerializeLong(((SqlBoolean)value).Value ? 1L : 0L, stateObj);
					case 105:
					case 106:
					case 107:
						goto IL_3B2;
					case 108:
						return this.SerializeSqlDecimal((SqlDecimal)value, stateObj);
					case 109:
						if (type.FixedLength == 4)
						{
							return this.SerializeFloat(((SqlSingle)value).Value);
						}
						return this.SerializeDouble(((SqlDouble)value).Value);
					case 110:
						return this.SerializeSqlMoney((SqlMoney)value, type.FixedLength, stateObj);
					case 111:
					{
						SqlDateTime sqlDateTime = (SqlDateTime)value;
						if (type.FixedLength != 4)
						{
							if (stateObj._bLongBytes == null)
							{
								stateObj._bLongBytes = new byte[8];
							}
							byte[] bLongBytes = stateObj._bLongBytes;
							int num = 0;
							byte[] src = this.SerializeInt(sqlDateTime.DayTicks, stateObj);
							Buffer.BlockCopy(src, 0, bLongBytes, num, 4);
							num += 4;
							src = this.SerializeInt(sqlDateTime.TimeTicks, stateObj);
							Buffer.BlockCopy(src, 0, bLongBytes, num, 4);
							return bLongBytes;
						}
						if (0 > sqlDateTime.DayTicks || sqlDateTime.DayTicks > 65535)
						{
							throw SQL.SmallDateTimeOverflow(sqlDateTime.ToString());
						}
						if (stateObj._bIntBytes == null)
						{
							stateObj._bIntBytes = new byte[4];
						}
						byte[] bIntBytes = stateObj._bIntBytes;
						int num2 = 0;
						byte[] src2 = this.SerializeShort(sqlDateTime.DayTicks, stateObj);
						Buffer.BlockCopy(src2, 0, bIntBytes, num2, 2);
						num2 += 2;
						src2 = this.SerializeShort(sqlDateTime.TimeTicks / SqlDateTime.SQLTicksPerMinute, stateObj);
						Buffer.BlockCopy(src2, 0, bIntBytes, num2, 2);
						return bIntBytes;
					}
					default:
						if (nullableType != 165)
						{
							if (nullableType != 167)
							{
								goto IL_3B2;
							}
							goto IL_200;
						}
						break;
					}
				}
			}
			else if (nullableType <= 175)
			{
				if (nullableType != 173)
				{
					if (nullableType != 175)
					{
						goto IL_3B2;
					}
					goto IL_200;
				}
			}
			else
			{
				if (nullableType != 231 && nullableType != 239 && nullableType != 241)
				{
					goto IL_3B2;
				}
				goto IL_24B;
			}
			byte[] array = new byte[actualLength];
			if (value is SqlBinary)
			{
				Buffer.BlockCopy(((SqlBinary)value).Value, offset, array, 0, actualLength);
			}
			else
			{
				Buffer.BlockCopy(((SqlBytes)value).Value, offset, array, 0, actualLength);
			}
			return array;
			IL_200:
			if (value is SqlChars)
			{
				string s = new string(((SqlChars)value).Value);
				return this.SerializeEncodingChar(s, actualLength, offset, this._defaultEncoding);
			}
			return this.SerializeEncodingChar(((SqlString)value).Value, actualLength, offset, this._defaultEncoding);
			IL_24B:
			if (actualLength != 0)
			{
				actualLength >>= 1;
			}
			if (value is SqlChars)
			{
				return this.SerializeCharArray(((SqlChars)value).Value, actualLength, offset);
			}
			return this.SerializeString(((SqlString)value).Value, actualLength, offset);
			IL_3B2:
			throw SQL.UnsupportedDatatypeEncryption(type.TypeName);
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000EB658 File Offset: 0x000EAA58
		internal void WriteParameterVarLen(MetaType type, int size, bool isNull, TdsParserStateObject stateObj, bool unknownLength = false)
		{
			if (type.IsLong)
			{
				if (isNull)
				{
					if (type.IsPlp)
					{
						this.WriteLong(-1L, stateObj);
						return;
					}
					this.WriteInt(-1, stateObj);
					return;
				}
				else
				{
					if (type.NullableType == 241 || unknownLength)
					{
						this.WriteUnsignedLong(18446744073709551614UL, stateObj);
						return;
					}
					if (type.IsPlp)
					{
						this.WriteLong((long)size, stateObj);
						return;
					}
					this.WriteInt(size, stateObj);
					return;
				}
			}
			else if (type.IsVarTime)
			{
				if (isNull)
				{
					stateObj.WriteByte(0);
					return;
				}
				stateObj.WriteByte((byte)size);
				return;
			}
			else if (!type.IsFixed)
			{
				if (isNull)
				{
					this.WriteShort(65535, stateObj);
					return;
				}
				this.WriteShort(size, stateObj);
				return;
			}
			else
			{
				if (isNull)
				{
					stateObj.WriteByte(0);
					return;
				}
				stateObj.WriteByte((byte)(type.FixedLength & 255));
				return;
			}
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000EB72C File Offset: 0x000EAB2C
		private bool TryReadPlpUnicodeCharsChunk(char[] buff, int offst, int len, TdsParserStateObject stateObj, out int charsRead)
		{
			if (stateObj._longlenleft == 0UL)
			{
				charsRead = 0;
				return true;
			}
			charsRead = len;
			if (stateObj._longlenleft >> 1 < (ulong)((long)len))
			{
				charsRead = (int)(stateObj._longlenleft >> 1);
			}
			for (int i = 0; i < charsRead; i++)
			{
				if (!stateObj.TryReadChar(out buff[offst + i]))
				{
					return false;
				}
			}
			stateObj._longlenleft -= (ulong)((ulong)((long)charsRead) << 1);
			return true;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000EB7A0 File Offset: 0x000EABA0
		internal int ReadPlpUnicodeChars(ref char[] buff, int offst, int len, TdsParserStateObject stateObj)
		{
			int result;
			if (!this.TryReadPlpUnicodeChars(ref buff, offst, len, stateObj, out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000EB7C8 File Offset: 0x000EABC8
		internal bool TryReadPlpUnicodeChars(ref char[] buff, int offst, int len, TdsParserStateObject stateObj, out int totalCharsRead)
		{
			int num = 0;
			if (stateObj._longlen == 0UL)
			{
				totalCharsRead = 0;
				return true;
			}
			int i = len;
			if (buff == null && stateObj._longlen != 18446744073709551614UL)
			{
				buff = new char[Math.Min((int)stateObj._longlen, len)];
			}
			if (stateObj._longlenleft == 0UL)
			{
				ulong num2;
				if (!stateObj.TryReadPlpLength(false, out num2))
				{
					totalCharsRead = 0;
					return false;
				}
				if (stateObj._longlenleft == 0UL)
				{
					totalCharsRead = 0;
					return true;
				}
			}
			totalCharsRead = 0;
			while (i > 0)
			{
				num = (int)Math.Min(stateObj._longlenleft + 1UL >> 1, (ulong)((long)i));
				if (buff == null || buff.Length < offst + num)
				{
					char[] array = new char[offst + num];
					if (buff != null)
					{
						Buffer.BlockCopy(buff, 0, array, 0, offst * 2);
					}
					buff = array;
				}
				if (num > 0)
				{
					if (!this.TryReadPlpUnicodeCharsChunk(buff, offst, num, stateObj, out num))
					{
						return false;
					}
					i -= num;
					offst += num;
					totalCharsRead += num;
				}
				if (stateObj._longlenleft == 1UL && i > 0)
				{
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					stateObj._longlenleft -= 1UL;
					ulong num3;
					if (!stateObj.TryReadPlpLength(false, out num3))
					{
						return false;
					}
					byte b2;
					if (!stateObj.TryReadByte(out b2))
					{
						return false;
					}
					stateObj._longlenleft -= 1UL;
					buff[offst] = (char)(((int)(b2 & byte.MaxValue) << 8) + (int)(b & byte.MaxValue));
					checked
					{
						offst++;
					}
					num++;
					i--;
					totalCharsRead++;
				}
				ulong num4;
				if (stateObj._longlenleft == 0UL && !stateObj.TryReadPlpLength(false, out num4))
				{
					return false;
				}
				if (stateObj._longlenleft == 0UL)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000EB95C File Offset: 0x000EAD5C
		internal int ReadPlpAnsiChars(ref char[] buff, int offst, int len, SqlMetaDataPriv metadata, TdsParserStateObject stateObj)
		{
			int num = 0;
			if (stateObj._longlen == 0UL)
			{
				return 0;
			}
			int i = len;
			if (stateObj._longlenleft == 0UL)
			{
				stateObj.ReadPlpLength(false);
				if (stateObj._longlenleft == 0UL)
				{
					stateObj._plpdecoder = null;
					return 0;
				}
			}
			if (stateObj._plpdecoder == null)
			{
				Encoding encoding = metadata.encoding;
				if (encoding == null)
				{
					if (this._defaultEncoding == null)
					{
						this.ThrowUnsupportedCollationEncountered(stateObj);
					}
					encoding = this._defaultEncoding;
				}
				stateObj._plpdecoder = encoding.GetDecoder();
			}
			while (i > 0)
			{
				int num2 = (int)Math.Min(stateObj._longlenleft, (ulong)((long)i));
				if (stateObj._bTmp == null || stateObj._bTmp.Length < num2)
				{
					stateObj._bTmp = new byte[num2];
				}
				num2 = stateObj.ReadPlpBytesChunk(stateObj._bTmp, 0, num2);
				int chars = stateObj._plpdecoder.GetChars(stateObj._bTmp, 0, num2, buff, offst);
				i -= chars;
				offst += chars;
				num += chars;
				if (stateObj._longlenleft == 0UL)
				{
					stateObj.ReadPlpLength(false);
				}
				if (stateObj._longlenleft == 0UL)
				{
					stateObj._plpdecoder = null;
					break;
				}
			}
			return num;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000EBA80 File Offset: 0x000EAE80
		internal ulong SkipPlpValue(ulong cb, TdsParserStateObject stateObj)
		{
			ulong result;
			if (!this.TrySkipPlpValue(cb, stateObj, out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000EBAA4 File Offset: 0x000EAEA4
		internal bool TrySkipPlpValue(ulong cb, TdsParserStateObject stateObj, out ulong totalBytesSkipped)
		{
			totalBytesSkipped = 0UL;
			ulong num;
			if (stateObj._longlenleft == 0UL && !stateObj.TryReadPlpLength(false, out num))
			{
				return false;
			}
			while (totalBytesSkipped < cb && stateObj._longlenleft > 0UL)
			{
				int num2;
				if (stateObj._longlenleft > 2147483647UL)
				{
					num2 = int.MaxValue;
				}
				else
				{
					num2 = (int)stateObj._longlenleft;
				}
				num2 = ((cb - totalBytesSkipped < (ulong)((long)num2)) ? ((int)(cb - totalBytesSkipped)) : num2);
				if (!stateObj.TrySkipBytes(num2))
				{
					return false;
				}
				stateObj._longlenleft -= (ulong)((long)num2);
				totalBytesSkipped += (ulong)((long)num2);
				ulong num3;
				if (stateObj._longlenleft == 0UL && !stateObj.TryReadPlpLength(false, out num3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000EBB40 File Offset: 0x000EAF40
		internal ulong PlpBytesLeft(TdsParserStateObject stateObj)
		{
			if (stateObj._longlen != 0UL && stateObj._longlenleft == 0UL)
			{
				stateObj.ReadPlpLength(false);
			}
			return stateObj._longlenleft;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000EBB6C File Offset: 0x000EAF6C
		internal bool TryPlpBytesLeft(TdsParserStateObject stateObj, out ulong left)
		{
			if (stateObj._longlen != 0UL && stateObj._longlenleft == 0UL && !stateObj.TryReadPlpLength(false, out left))
			{
				return false;
			}
			left = stateObj._longlenleft;
			return true;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000EBBA0 File Offset: 0x000EAFA0
		internal ulong PlpBytesTotalLength(TdsParserStateObject stateObj)
		{
			if (stateObj._longlen == 18446744073709551614UL)
			{
				return ulong.MaxValue;
			}
			if (stateObj._longlen == 18446744073709551615UL)
			{
				return 0UL;
			}
			return stateObj._longlen;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000EBBD0 File Offset: 0x000EAFD0
		internal string TraceString()
		{
			return string.Format(null, "\n\t         _physicalStateObj = {0}\n\t         _pMarsPhysicalConObj = {1}\n\t         _state = {2}\n\t         _server = {3}\n\t         _fResetConnection = {4}\n\t         _defaultCollation = {5}\n\t         _defaultCodePage = {6}\n\t         _defaultLCID = {7}\n\t         _defaultEncoding = {8}\n\t         _encryptionOption = {10}\n\t         _currentTransaction = {11}\n\t         _pendingTransaction = {12}\n\t         _retainedTransactionId = {13}\n\t         _nonTransactedOpenResultCount = {14}\n\t         _connHandler = {15}\n\t         _fMARS = {16}\n\t         _sessionPool = {17}\n\t         _isShiloh = {18}\n\t         _isShilohSP1 = {19}\n\t         _isYukon = {20}\n\t         _sniSpnBuffer = {21}\n\t         _errors = {22}\n\t         _warnings = {23}\n\t         _attentionErrors = {24}\n\t         _attentionWarnings = {25}\n\t         _statistics = {26}\n\t         _statisticsIsInTransaction = {27}\n\t         _fPreserveTransaction = {28}         _fParallel = {29}", new object[]
			{
				this._physicalStateObj == null,
				this._pMarsPhysicalConObj == null,
				this._state,
				this._server,
				this._fResetConnection,
				(this._defaultCollation == null) ? "(null)" : this._defaultCollation.TraceString(),
				this._defaultCodePage,
				this._defaultLCID,
				this.TraceObjectClass(this._defaultEncoding),
				"",
				this._encryptionOption,
				(this._currentTransaction == null) ? "(null)" : this._currentTransaction.TraceString(),
				(this._pendingTransaction == null) ? "(null)" : this._pendingTransaction.TraceString(),
				this._retainedTransactionId,
				this._nonTransactedOpenResultCount,
				(this._connHandler == null) ? "(null)" : this._connHandler.ObjectID.ToString(null),
				this._fMARS,
				(this._sessionPool == null) ? "(null)" : this._sessionPool.TraceString(),
				this._isShiloh,
				this._isShilohSP1,
				this._isYukon,
				(this._sniSpnBuffer == null) ? "(null)" : this._sniSpnBuffer.Length.ToString(null),
				(this._physicalStateObj != null) ? "(null)" : this._physicalStateObj.ErrorCount.ToString(null),
				(this._physicalStateObj != null) ? "(null)" : this._physicalStateObj.WarningCount.ToString(null),
				(this._physicalStateObj != null) ? "(null)" : this._physicalStateObj.PreAttentionErrorCount.ToString(null),
				(this._physicalStateObj != null) ? "(null)" : this._physicalStateObj.PreAttentionWarningCount.ToString(null),
				this._statistics == null,
				this._statisticsIsInTransaction,
				this._fPreserveTransaction,
				(this._connHandler == null) ? "(null)" : this._connHandler.ConnectionOptions.MultiSubnetFailover.ToString(null),
				(this._connHandler == null) ? "(null)" : this._connHandler.ConnectionOptions.TransparentNetworkIPResolution.ToString(null)
			});
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000EBEC0 File Offset: 0x000EB2C0
		private string TraceObjectClass(object instance)
		{
			if (instance == null)
			{
				return "(null)";
			}
			return instance.GetType().ToString();
		}

		// Token: 0x040013C6 RID: 5062
		private static int _objectTypeCount;

		// Token: 0x040013C7 RID: 5063
		internal readonly int _objectID = Interlocked.Increment(ref TdsParser._objectTypeCount);

		// Token: 0x040013C8 RID: 5064
		private static Task completedTask;

		// Token: 0x040013C9 RID: 5065
		internal TdsParserStateObject _physicalStateObj;

		// Token: 0x040013CA RID: 5066
		internal TdsParserStateObject _pMarsPhysicalConObj;

		// Token: 0x040013CB RID: 5067
		private const int constBinBufferSize = 4096;

		// Token: 0x040013CC RID: 5068
		private const int constTextBufferSize = 4096;

		// Token: 0x040013CD RID: 5069
		internal TdsParserState _state;

		// Token: 0x040013CE RID: 5070
		private string _server = "";

		// Token: 0x040013CF RID: 5071
		internal volatile bool _fResetConnection;

		// Token: 0x040013D0 RID: 5072
		internal volatile bool _fPreserveTransaction;

		// Token: 0x040013D1 RID: 5073
		private SqlCollation _defaultCollation;

		// Token: 0x040013D2 RID: 5074
		private int _defaultCodePage;

		// Token: 0x040013D3 RID: 5075
		private int _defaultLCID;

		// Token: 0x040013D4 RID: 5076
		internal Encoding _defaultEncoding;

		// Token: 0x040013D5 RID: 5077
		private static EncryptionOptions _sniSupportedEncryptionOption = SNILoadHandle.SingletonInstance.Options;

		// Token: 0x040013D6 RID: 5078
		private EncryptionOptions _encryptionOption = TdsParser._sniSupportedEncryptionOption;

		// Token: 0x040013D7 RID: 5079
		private SqlInternalTransaction _currentTransaction;

		// Token: 0x040013D8 RID: 5080
		private SqlInternalTransaction _pendingTransaction;

		// Token: 0x040013D9 RID: 5081
		private long _retainedTransactionId;

		// Token: 0x040013DA RID: 5082
		private int _nonTransactedOpenResultCount;

		// Token: 0x040013DB RID: 5083
		private SqlInternalConnectionTds _connHandler;

		// Token: 0x040013DC RID: 5084
		private bool _fMARS;

		// Token: 0x040013DD RID: 5085
		internal bool _loginWithFailover;

		// Token: 0x040013DE RID: 5086
		internal AutoResetEvent _resetConnectionEvent;

		// Token: 0x040013DF RID: 5087
		internal TdsParserSessionPool _sessionPool;

		// Token: 0x040013E0 RID: 5088
		private bool _isShiloh;

		// Token: 0x040013E1 RID: 5089
		private bool _isShilohSP1;

		// Token: 0x040013E2 RID: 5090
		private bool _isYukon;

		// Token: 0x040013E3 RID: 5091
		private bool _isKatmai;

		// Token: 0x040013E4 RID: 5092
		private bool _isDenali;

		// Token: 0x040013E5 RID: 5093
		private byte[] _sniSpnBuffer;

		// Token: 0x040013E6 RID: 5094
		private SqlStatistics _statistics;

		// Token: 0x040013E7 RID: 5095
		private bool _statisticsIsInTransaction;

		// Token: 0x040013E8 RID: 5096
		private static byte[] s_nicAddress;

		// Token: 0x040013E9 RID: 5097
		private static bool s_fSSPILoaded = false;

		// Token: 0x040013EA RID: 5098
		private static volatile uint s_maxSSPILength = 0U;

		// Token: 0x040013EB RID: 5099
		private static bool s_fADALLoaded = false;

		// Token: 0x040013EC RID: 5100
		private static readonly byte[] s_longDataHeader = new byte[]
		{
			16,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x040013ED RID: 5101
		private static object s_tdsParserLock = new object();

		// Token: 0x040013EE RID: 5102
		private const int ATTENTION_TIMEOUT = 5000;

		// Token: 0x040013EF RID: 5103
		private static readonly byte[] s_xmlMetadataSubstituteSequence = new byte[]
		{
			231,
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x040013F0 RID: 5104
		private const int GUID_SIZE = 16;

		// Token: 0x040013F1 RID: 5105
		internal bool _asyncWrite;

		// Token: 0x040013F2 RID: 5106
		private bool _serverSupportsColumnEncryption;

		// Token: 0x040013F3 RID: 5107
		private static readonly byte[] s_FeatureExtDataAzureSQLSupportFeatureRequest = new byte[]
		{
			1
		};

		// Token: 0x040013F6 RID: 5110
		private static readonly IEnumerable<SqlDataRecord> __tvpEmptyValue = new List<SqlDataRecord>().AsReadOnly();

		// Token: 0x040013F7 RID: 5111
		private const ulong _indeterminateSize = 18446744073709551615UL;

		// Token: 0x040013F8 RID: 5112
		private const string StateTraceFormatString = "\n\t         _physicalStateObj = {0}\n\t         _pMarsPhysicalConObj = {1}\n\t         _state = {2}\n\t         _server = {3}\n\t         _fResetConnection = {4}\n\t         _defaultCollation = {5}\n\t         _defaultCodePage = {6}\n\t         _defaultLCID = {7}\n\t         _defaultEncoding = {8}\n\t         _encryptionOption = {10}\n\t         _currentTransaction = {11}\n\t         _pendingTransaction = {12}\n\t         _retainedTransactionId = {13}\n\t         _nonTransactedOpenResultCount = {14}\n\t         _connHandler = {15}\n\t         _fMARS = {16}\n\t         _sessionPool = {17}\n\t         _isShiloh = {18}\n\t         _isShilohSP1 = {19}\n\t         _isYukon = {20}\n\t         _sniSpnBuffer = {21}\n\t         _errors = {22}\n\t         _warnings = {23}\n\t         _attentionErrors = {24}\n\t         _attentionWarnings = {25}\n\t         _statistics = {26}\n\t         _statisticsIsInTransaction = {27}\n\t         _fPreserveTransaction = {28}         _fParallel = {29}";

		// Token: 0x020003E3 RID: 995
		internal struct ReliabilitySection
		{
			// Token: 0x06003570 RID: 13680 RVA: 0x001454F0 File Offset: 0x001448F0
			[Conditional("DEBUG")]
			internal void Start()
			{
			}

			// Token: 0x06003571 RID: 13681 RVA: 0x00145500 File Offset: 0x00144900
			[Conditional("DEBUG")]
			internal void Stop()
			{
			}

			// Token: 0x06003572 RID: 13682 RVA: 0x00145510 File Offset: 0x00144910
			[Conditional("DEBUG")]
			internal static void Assert(string message)
			{
			}
		}

		// Token: 0x020003E4 RID: 996
		private class TdsOrderUnique
		{
			// Token: 0x06003573 RID: 13683 RVA: 0x00145520 File Offset: 0x00144920
			internal TdsOrderUnique(short ordinal, byte flags)
			{
				this.ColumnOrdinal = ordinal;
				this.Flags = flags;
			}

			// Token: 0x04002138 RID: 8504
			internal short ColumnOrdinal;

			// Token: 0x04002139 RID: 8505
			internal byte Flags;
		}

		// Token: 0x020003E5 RID: 997
		private class TdsOutputStream : Stream
		{
			// Token: 0x06003574 RID: 13684 RVA: 0x00145544 File Offset: 0x00144944
			public TdsOutputStream(TdsParser parser, TdsParserStateObject stateObj, byte[] preambleToStrip)
			{
				this._parser = parser;
				this._stateObj = stateObj;
				this._preambleToStrip = preambleToStrip;
			}

			// Token: 0x1700085B RID: 2139
			// (get) Token: 0x06003575 RID: 13685 RVA: 0x0014556C File Offset: 0x0014496C
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700085C RID: 2140
			// (get) Token: 0x06003576 RID: 13686 RVA: 0x0014557C File Offset: 0x0014497C
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700085D RID: 2141
			// (get) Token: 0x06003577 RID: 13687 RVA: 0x0014558C File Offset: 0x0014498C
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003578 RID: 13688 RVA: 0x0014559C File Offset: 0x0014499C
			public override void Flush()
			{
			}

			// Token: 0x1700085E RID: 2142
			// (get) Token: 0x06003579 RID: 13689 RVA: 0x001455AC File Offset: 0x001449AC
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x1700085F RID: 2143
			// (get) Token: 0x0600357A RID: 13690 RVA: 0x001455C0 File Offset: 0x001449C0
			// (set) Token: 0x0600357B RID: 13691 RVA: 0x001455D4 File Offset: 0x001449D4
			public override long Position
			{
				get
				{
					throw new NotSupportedException();
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x0600357C RID: 13692 RVA: 0x001455E8 File Offset: 0x001449E8
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600357D RID: 13693 RVA: 0x001455FC File Offset: 0x001449FC
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600357E RID: 13694 RVA: 0x00145610 File Offset: 0x00144A10
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600357F RID: 13695 RVA: 0x00145624 File Offset: 0x00144A24
			private void StripPreamble(byte[] buffer, ref int offset, ref int count)
			{
				if (this._preambleToStrip != null && count >= this._preambleToStrip.Length)
				{
					for (int i = 0; i < this._preambleToStrip.Length; i++)
					{
						if (this._preambleToStrip[i] != buffer[i])
						{
							this._preambleToStrip = null;
							return;
						}
					}
					offset += this._preambleToStrip.Length;
					count -= this._preambleToStrip.Length;
				}
				this._preambleToStrip = null;
			}

			// Token: 0x06003580 RID: 13696 RVA: 0x00145690 File Offset: 0x00144A90
			public override void Write(byte[] buffer, int offset, int count)
			{
				TdsParser.TdsOutputStream.ValidateWriteParameters(buffer, offset, count);
				this.StripPreamble(buffer, ref offset, ref count);
				if (count > 0)
				{
					this._parser.WriteInt(count, this._stateObj);
					this._stateObj.WriteByteArray(buffer, count, offset, true, null);
				}
			}

			// Token: 0x06003581 RID: 13697 RVA: 0x001456D8 File Offset: 0x00144AD8
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				TdsParser.TdsOutputStream.ValidateWriteParameters(buffer, offset, count);
				this.StripPreamble(buffer, ref offset, ref count);
				RuntimeHelpers.PrepareConstrainedRegions();
				Task result;
				try
				{
					Task task = null;
					if (count > 0)
					{
						this._parser.WriteInt(count, this._stateObj);
						task = this._stateObj.WriteByteArray(buffer, count, offset, false, null);
					}
					if (task == null)
					{
						result = TdsParser.CompletedTask;
					}
					else
					{
						result = task;
					}
				}
				catch (OutOfMemoryException)
				{
					this._parser._connHandler.DoomThisConnection();
					throw;
				}
				catch (StackOverflowException)
				{
					this._parser._connHandler.DoomThisConnection();
					throw;
				}
				catch (ThreadAbortException)
				{
					this._parser._connHandler.DoomThisConnection();
					throw;
				}
				return result;
			}

			// Token: 0x06003582 RID: 13698 RVA: 0x001457C4 File Offset: 0x00144BC4
			internal static void ValidateWriteParameters(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw ADP.ArgumentNull("buffer");
				}
				if (offset < 0)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				if (count < 0)
				{
					throw ADP.ArgumentOutOfRange("count");
				}
				try
				{
					if (checked(offset + count) > buffer.Length)
					{
						throw ExceptionBuilder.InvalidOffsetLength();
					}
				}
				catch (OverflowException)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}

			// Token: 0x0400213A RID: 8506
			private TdsParser _parser;

			// Token: 0x0400213B RID: 8507
			private TdsParserStateObject _stateObj;

			// Token: 0x0400213C RID: 8508
			private byte[] _preambleToStrip;
		}

		// Token: 0x020003E6 RID: 998
		private class ConstrainedTextWriter : TextWriter
		{
			// Token: 0x06003583 RID: 13699 RVA: 0x00145834 File Offset: 0x00144C34
			public ConstrainedTextWriter(TextWriter next, int size)
			{
				this._next = next;
				this._size = size;
				this._written = 0;
				if (this._size < 1)
				{
					this._size = int.MaxValue;
				}
			}

			// Token: 0x17000860 RID: 2144
			// (get) Token: 0x06003584 RID: 13700 RVA: 0x00145870 File Offset: 0x00144C70
			public bool IsComplete
			{
				get
				{
					return this._size > 0 && this._written >= this._size;
				}
			}

			// Token: 0x17000861 RID: 2145
			// (get) Token: 0x06003585 RID: 13701 RVA: 0x0014589C File Offset: 0x00144C9C
			public override Encoding Encoding
			{
				get
				{
					return this._next.Encoding;
				}
			}

			// Token: 0x06003586 RID: 13702 RVA: 0x001458B4 File Offset: 0x00144CB4
			public override void Flush()
			{
				this._next.Flush();
			}

			// Token: 0x06003587 RID: 13703 RVA: 0x001458CC File Offset: 0x00144CCC
			public override Task FlushAsync()
			{
				return this._next.FlushAsync();
			}

			// Token: 0x06003588 RID: 13704 RVA: 0x001458E4 File Offset: 0x00144CE4
			public override void Write(char value)
			{
				if (this._written < this._size)
				{
					this._next.Write(value);
					this._written++;
				}
			}

			// Token: 0x06003589 RID: 13705 RVA: 0x0014591C File Offset: 0x00144D1C
			public override void Write(char[] buffer, int index, int count)
			{
				TdsParser.ConstrainedTextWriter.ValidateWriteParameters(buffer, index, count);
				count = Math.Min(this._size - this._written, count);
				if (count > 0)
				{
					this._next.Write(buffer, index, count);
				}
				this._written += count;
			}

			// Token: 0x0600358A RID: 13706 RVA: 0x00145968 File Offset: 0x00144D68
			public override Task WriteAsync(char value)
			{
				if (this._written < this._size)
				{
					this._written++;
					return this._next.WriteAsync(value);
				}
				return TdsParser.CompletedTask;
			}

			// Token: 0x0600358B RID: 13707 RVA: 0x001459A4 File Offset: 0x00144DA4
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				TdsParser.ConstrainedTextWriter.ValidateWriteParameters(buffer, index, count);
				count = Math.Min(this._size - this._written, count);
				if (count > 0)
				{
					this._written += count;
					return this._next.WriteAsync(buffer, index, count);
				}
				return TdsParser.CompletedTask;
			}

			// Token: 0x0600358C RID: 13708 RVA: 0x001459F4 File Offset: 0x00144DF4
			public override Task WriteAsync(string value)
			{
				return base.WriteAsync(value.ToCharArray());
			}

			// Token: 0x0600358D RID: 13709 RVA: 0x00145A10 File Offset: 0x00144E10
			internal static void ValidateWriteParameters(char[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw ADP.ArgumentNull("buffer");
				}
				if (offset < 0)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				if (count < 0)
				{
					throw ADP.ArgumentOutOfRange("count");
				}
				try
				{
					if (checked(offset + count) > buffer.Length)
					{
						throw ExceptionBuilder.InvalidOffsetLength();
					}
				}
				catch (OverflowException)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}

			// Token: 0x0400213D RID: 8509
			private TextWriter _next;

			// Token: 0x0400213E RID: 8510
			private int _size;

			// Token: 0x0400213F RID: 8511
			private int _written;
		}
	}
}
