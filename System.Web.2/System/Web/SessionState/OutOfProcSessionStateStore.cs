using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Security.Cryptography;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x02000126 RID: 294
	internal sealed class OutOfProcSessionStateStore : SessionStateStoreProviderBase
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x00031062 File Offset: 0x0002F262
		internal override void Initialize(string name, NameValueCollection config, IPartitionResolver partitionResolver)
		{
			this._partitionResolver = partitionResolver;
			this.Initialize(name, config);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00031074 File Offset: 0x0002F274
		public override void Initialize(string name, NameValueCollection config)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "State Server Session State Provider";
			}
			base.Initialize(name, config);
			if (!OutOfProcSessionStateStore.s_oneTimeInited)
			{
				OutOfProcSessionStateStore.s_lock.AcquireWriterLock();
				try
				{
					if (!OutOfProcSessionStateStore.s_oneTimeInited)
					{
						this.OneTimeInit();
					}
				}
				finally
				{
					OutOfProcSessionStateStore.s_lock.ReleaseWriterLock();
				}
			}
			if (!OutOfProcSessionStateStore.s_usePartition)
			{
				this._partitionInfo = OutOfProcSessionStateStore.s_singlePartitionInfo;
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000310E8 File Offset: 0x0002F2E8
		private void OneTimeInit()
		{
			SessionStateSection sessionState = RuntimeConfig.GetAppConfig().SessionState;
			OutOfProcSessionStateStore.s_configPartitionResolverType = sessionState.PartitionResolverType;
			OutOfProcSessionStateStore.s_configStateConnectionString = sessionState.StateConnectionString;
			OutOfProcSessionStateStore.s_configStateConnectionStringFileName = sessionState.ElementInformation.Properties["stateConnectionString"].Source;
			OutOfProcSessionStateStore.s_configStateConnectionStringLineNumber = sessionState.ElementInformation.Properties["stateConnectionString"].LineNumber;
			OutOfProcSessionStateStore.s_configCompressionEnabled = sessionState.CompressionEnabled;
			if (this._partitionResolver == null)
			{
				string stateConnectionString = sessionState.StateConnectionString;
				SessionStateModule.ReadConnectionString(sessionState, ref stateConnectionString, "stateConnectionString");
				OutOfProcSessionStateStore.s_singlePartitionInfo = (OutOfProcSessionStateStore.StateServerPartitionInfo)this.CreatePartitionInfo(stateConnectionString);
			}
			else
			{
				OutOfProcSessionStateStore.s_usePartition = true;
				OutOfProcSessionStateStore.s_partitionManager = new PartitionManager(new CreatePartitionInfo(this.CreatePartitionInfo));
			}
			OutOfProcSessionStateStore.s_networkTimeout = (int)sessionState.StateNetworkTimeout.TotalSeconds;
			string appDomainAppId = HttpRuntime.AppDomainAppId;
			string text = Convert.ToBase64String(CryptoUtil.ComputeSHA256Hash(Encoding.UTF8.GetBytes(appDomainAppId)));
			if (appDomainAppId.StartsWith("/", StringComparison.Ordinal))
			{
				OutOfProcSessionStateStore.s_uribase = appDomainAppId + "(" + text + ")/";
			}
			else
			{
				OutOfProcSessionStateStore.s_uribase = string.Concat(new string[]
				{
					"/",
					appDomainAppId,
					"(",
					text,
					")/"
				});
			}
			OutOfProcSessionStateStore.s_onAppDomainUnload = new EventHandler(this.OnAppDomainUnload);
			Thread.GetDomain().DomainUnload += OutOfProcSessionStateStore.s_onAppDomainUnload;
			OutOfProcSessionStateStore.s_oneTimeInited = true;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00031257 File Offset: 0x0002F457
		private void OnAppDomainUnload(object unusedObject, EventArgs unusedEventArgs)
		{
			Thread.GetDomain().DomainUnload -= OutOfProcSessionStateStore.s_onAppDomainUnload;
			if (this._partitionResolver == null)
			{
				if (OutOfProcSessionStateStore.s_singlePartitionInfo != null)
				{
					OutOfProcSessionStateStore.s_singlePartitionInfo.Dispose();
					return;
				}
			}
			else if (OutOfProcSessionStateStore.s_partitionManager != null)
			{
				OutOfProcSessionStateStore.s_partitionManager.Dispose();
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00031294 File Offset: 0x0002F494
		internal IPartitionInfo CreatePartitionInfo(string stateConnectionString)
		{
			string text;
			bool serverIsIPv6NumericAddress;
			int port;
			try
			{
				OutOfProcSessionStateStore.ParseStateConnectionString(stateConnectionString, out text, out serverIsIPv6NumericAddress, out port);
				for (int i = 0; i < text.Length; i++)
				{
					if (text[i] > '\u007f')
					{
						throw new ArgumentException("stateConnectionString");
					}
				}
			}
			catch
			{
				if (OutOfProcSessionStateStore.s_usePartition)
				{
					throw new HttpException(SR.GetString("Error_parsing_state_server_partition_resolver_string", new object[]
					{
						OutOfProcSessionStateStore.s_configPartitionResolverType
					}));
				}
				throw new ConfigurationErrorsException(SR.GetString("Invalid_value_for_sessionstate_stateConnectionString", new object[]
				{
					OutOfProcSessionStateStore.s_configStateConnectionString
				}), OutOfProcSessionStateStore.s_configStateConnectionStringFileName, OutOfProcSessionStateStore.s_configStateConnectionStringLineNumber);
			}
			int num = UnsafeNativeMethods.SessionNDConnectToService(text);
			if (num != 0)
			{
				throw OutOfProcSessionStateStore.CreateConnectionException(text, port, num);
			}
			return new OutOfProcSessionStateStore.StateServerPartitionInfo(new ResourcePool(new TimeSpan(0, 0, 5), int.MaxValue), text, serverIsIPv6NumericAddress, port);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00031368 File Offset: 0x0002F568
		internal static void ParseStateConnectionString(string stateConnectionString, out string server, out bool serverIsIPv6NumericAddress, out int port)
		{
			if (!stateConnectionString.StartsWith("tcpip=", StringComparison.Ordinal))
			{
				throw new ArgumentException("stateConnectionString");
			}
			stateConnectionString = stateConnectionString.Substring("tcpip=".Length);
			Match match = OutOfProcSessionStateStore._ipv6ConnectionStringFormat.Match(stateConnectionString);
			if (match != null && match.Success)
			{
				string value = match.Groups["ipv6Address"].Value;
				IPAddress ipaddress = IPAddress.Parse(value);
				if (ipaddress.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new ArgumentException("stateConnectionString");
				}
				server = value;
				serverIsIPv6NumericAddress = true;
				port = (int)ushort.Parse(match.Groups["port"].Value, CultureInfo.InvariantCulture);
				return;
			}
			else
			{
				string[] array = stateConnectionString.Split(new char[]
				{
					':'
				});
				if (array.Length != 2)
				{
					throw new ArgumentException("stateConnectionString");
				}
				server = array[0];
				serverIsIPv6NumericAddress = false;
				port = (int)ushort.Parse(array[1], CultureInfo.InvariantCulture);
				return;
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0003144C File Offset: 0x0002F64C
		internal static HttpException CreateConnectionException(string server, int port, int hr)
		{
			if (OutOfProcSessionStateStore.s_usePartition)
			{
				return new HttpException(SR.GetString("Cant_make_session_request_partition_resolver", new object[]
				{
					OutOfProcSessionStateStore.s_configPartitionResolverType,
					server,
					port.ToString(CultureInfo.InvariantCulture)
				}), hr);
			}
			return new HttpException(SR.GetString("Cant_make_session_request"), hr);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00007722 File Offset: 0x00005922
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			return false;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00006164 File Offset: 0x00004364
		public override void Dispose()
		{
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000314A2 File Offset: 0x0002F6A2
		public override void InitializeRequest(HttpContext context)
		{
			if (OutOfProcSessionStateStore.s_usePartition)
			{
				this._partitionInfo = null;
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000314B4 File Offset: 0x0002F6B4
		private void MakeRequest(UnsafeNativeMethods.StateProtocolVerb verb, string id, UnsafeNativeMethods.StateProtocolExclusive exclusiveAccess, int extraFlags, int timeout, int lockCookie, byte[] buf, int cb, int networkTimeout, out UnsafeNativeMethods.SessionNDMakeRequestResults results)
		{
			OutOfProcSessionStateStore.OutOfProcConnection outOfProcConnection = null;
			bool flag = false;
			SessionIDManager.CheckIdLength(id, true);
			if (this._partitionInfo == null)
			{
				this._partitionInfo = (OutOfProcSessionStateStore.StateServerPartitionInfo)OutOfProcSessionStateStore.s_partitionManager.GetPartition(this._partitionResolver, id);
				if (this._partitionInfo == null)
				{
					throw new HttpException(SR.GetString("Bad_partition_resolver_connection_string", new object[]
					{
						"PartitionManager"
					}));
				}
			}
			int num;
			try
			{
				outOfProcConnection = (OutOfProcSessionStateStore.OutOfProcConnection)this._partitionInfo.RetrieveResource();
				HandleRef socket;
				if (outOfProcConnection != null)
				{
					socket = new HandleRef(this, outOfProcConnection._socketHandle.Handle);
				}
				else
				{
					socket = new HandleRef(this, OutOfProcSessionStateStore.INVALID_SOCKET);
				}
				if (this._partitionInfo.StateServerVersion == -1)
				{
					flag = true;
				}
				string uri = HttpUtility.UrlEncode(OutOfProcSessionStateStore.s_uribase + id);
				num = UnsafeNativeMethods.SessionNDMakeRequest(socket, this._partitionInfo.Server, this._partitionInfo.Port, this._partitionInfo.ServerIsIPv6NumericAddress, networkTimeout, verb, uri, exclusiveAccess, extraFlags, timeout, lockCookie, buf, cb, flag, out results);
				if (outOfProcConnection != null)
				{
					if (results.socket == OutOfProcSessionStateStore.INVALID_SOCKET)
					{
						outOfProcConnection.Detach();
						outOfProcConnection = null;
					}
					else if (results.socket != socket.Handle)
					{
						outOfProcConnection._socketHandle = new HandleRef(this, results.socket);
					}
				}
				else if (results.socket != OutOfProcSessionStateStore.INVALID_SOCKET)
				{
					outOfProcConnection = new OutOfProcSessionStateStore.OutOfProcConnection(results.socket);
				}
				if (outOfProcConnection != null)
				{
					this._partitionInfo.StoreResource(outOfProcConnection);
				}
			}
			catch
			{
				if (outOfProcConnection != null)
				{
					outOfProcConnection.Dispose();
				}
				throw;
			}
			if (num != 0)
			{
				HttpException ex = OutOfProcSessionStateStore.CreateConnectionException(this._partitionInfo.Server, this._partitionInfo.Port, num);
				string text = null;
				switch (results.lastPhase)
				{
				case 0:
					text = SR.GetString("State_Server_detailed_error_phase0");
					break;
				case 1:
					text = SR.GetString("State_Server_detailed_error_phase1");
					break;
				case 2:
					text = SR.GetString("State_Server_detailed_error_phase2");
					break;
				case 3:
					text = SR.GetString("State_Server_detailed_error_phase3");
					break;
				}
				WebBaseEvent.RaiseSystemEvent(SR.GetString("State_Server_detailed_error", new object[]
				{
					text,
					"0x" + num.ToString("X08", CultureInfo.InvariantCulture),
					cb.ToString(CultureInfo.InvariantCulture)
				}), this, 3009, 50016, ex);
				throw ex;
			}
			if (results.httpStatus != 400)
			{
				if (flag)
				{
					this._partitionInfo.StateServerVersion = results.stateServerMajVer;
					if (this._partitionInfo.StateServerVersion < OutOfProcSessionStateStore.WHIDBEY_MAJOR_VERSION)
					{
						if (OutOfProcSessionStateStore.s_usePartition)
						{
							throw new HttpException(SR.GetString("Need_v2_State_Server_partition_resolver", new object[]
							{
								OutOfProcSessionStateStore.s_configPartitionResolverType,
								this._partitionInfo.Server,
								this._partitionInfo.Port.ToString(CultureInfo.InvariantCulture)
							}));
						}
						throw new HttpException(SR.GetString("Need_v2_State_Server"));
					}
				}
				return;
			}
			if (OutOfProcSessionStateStore.s_usePartition)
			{
				throw new HttpException(SR.GetString("Bad_state_server_request_partition_resolver", new object[]
				{
					OutOfProcSessionStateStore.s_configPartitionResolverType,
					this._partitionInfo.Server,
					this._partitionInfo.Port.ToString(CultureInfo.InvariantCulture)
				}));
			}
			throw new HttpException(SR.GetString("Bad_state_server_request"));
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003181C File Offset: 0x0002FA1C
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe SessionStateStoreData DoGet(HttpContext context, string id, UnsafeNativeMethods.StateProtocolExclusive exclusiveAccess, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			SessionStateStoreData result = null;
			UnmanagedMemoryStream unmanagedMemoryStream = null;
			locked = false;
			lockId = null;
			lockAge = TimeSpan.Zero;
			actionFlags = SessionStateActions.None;
			UnsafeNativeMethods.SessionNDMakeRequestResults sessionNDMakeRequestResults;
			sessionNDMakeRequestResults.content = IntPtr.Zero;
			try
			{
				this.MakeRequest(UnsafeNativeMethods.StateProtocolVerb.GET, id, exclusiveAccess, 0, 0, 0, null, 0, OutOfProcSessionStateStore.s_networkTimeout, out sessionNDMakeRequestResults);
				int httpStatus = sessionNDMakeRequestResults.httpStatus;
				if (httpStatus != 200)
				{
					if (httpStatus == 423)
					{
						if (0 <= sessionNDMakeRequestResults.lockAge)
						{
							if (sessionNDMakeRequestResults.lockAge < 31536000)
							{
								lockAge = new TimeSpan(0, 0, sessionNDMakeRequestResults.lockAge);
							}
							else
							{
								lockAge = TimeSpan.Zero;
							}
						}
						else
						{
							DateTime now = DateTime.Now;
							if (0L < sessionNDMakeRequestResults.lockDate && sessionNDMakeRequestResults.lockDate < now.Ticks)
							{
								lockAge = now - new DateTime(sessionNDMakeRequestResults.lockDate);
							}
							else
							{
								lockAge = TimeSpan.Zero;
							}
						}
						locked = true;
						lockId = sessionNDMakeRequestResults.lockCookie;
					}
				}
				else
				{
					int contentLength = sessionNDMakeRequestResults.contentLength;
					if (contentLength > 0)
					{
						try
						{
							unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)((void*)sessionNDMakeRequestResults.content), (long)contentLength);
							result = SessionStateUtility.DeserializeStoreData(context, unmanagedMemoryStream, OutOfProcSessionStateStore.s_configCompressionEnabled);
						}
						finally
						{
							if (unmanagedMemoryStream != null)
							{
								unmanagedMemoryStream.Close();
							}
						}
						lockId = sessionNDMakeRequestResults.lockCookie;
						actionFlags = (SessionStateActions)sessionNDMakeRequestResults.actionFlags;
					}
				}
			}
			finally
			{
				if (sessionNDMakeRequestResults.content != IntPtr.Zero)
				{
					UnsafeNativeMethods.SessionNDFreeBody(new HandleRef(this, sessionNDMakeRequestResults.content));
				}
			}
			return result;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000319CC File Offset: 0x0002FBCC
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			return this.DoGet(context, id, UnsafeNativeMethods.StateProtocolExclusive.NONE, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000319DE File Offset: 0x0002FBDE
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			return this.DoGet(context, id, UnsafeNativeMethods.StateProtocolExclusive.ACQUIRE, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000319F0 File Offset: 0x0002FBF0
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			int lockCookie = (int)lockId;
			UnsafeNativeMethods.SessionNDMakeRequestResults sessionNDMakeRequestResults;
			this.MakeRequest(UnsafeNativeMethods.StateProtocolVerb.GET, id, UnsafeNativeMethods.StateProtocolExclusive.RELEASE, 0, 0, lockCookie, null, 0, OutOfProcSessionStateStore.s_networkTimeout, out sessionNDMakeRequestResults);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00031A1C File Offset: 0x0002FC1C
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			byte[] buf;
			int cb;
			try
			{
				SessionStateUtility.SerializeStoreData(item, 0, out buf, out cb, OutOfProcSessionStateStore.s_configCompressionEnabled);
			}
			catch
			{
				if (!newItem)
				{
					this.ReleaseItemExclusive(context, id, lockId);
				}
				throw;
			}
			int lockCookie;
			if (lockId == null)
			{
				lockCookie = 0;
			}
			else
			{
				lockCookie = (int)lockId;
			}
			UnsafeNativeMethods.SessionNDMakeRequestResults sessionNDMakeRequestResults;
			this.MakeRequest(UnsafeNativeMethods.StateProtocolVerb.PUT, id, UnsafeNativeMethods.StateProtocolExclusive.NONE, 0, item.Timeout, lockCookie, buf, cb, OutOfProcSessionStateStore.s_networkTimeout, out sessionNDMakeRequestResults);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00031A88 File Offset: 0x0002FC88
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			int lockCookie = (int)lockId;
			UnsafeNativeMethods.SessionNDMakeRequestResults sessionNDMakeRequestResults;
			this.MakeRequest(UnsafeNativeMethods.StateProtocolVerb.DELETE, id, UnsafeNativeMethods.StateProtocolExclusive.NONE, 0, 0, lockCookie, null, 0, OutOfProcSessionStateStore.s_networkTimeout, out sessionNDMakeRequestResults);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00031AB4 File Offset: 0x0002FCB4
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			UnsafeNativeMethods.SessionNDMakeRequestResults sessionNDMakeRequestResults;
			this.MakeRequest(UnsafeNativeMethods.StateProtocolVerb.HEAD, id, UnsafeNativeMethods.StateProtocolExclusive.NONE, 0, 0, 0, null, 0, OutOfProcSessionStateStore.s_networkTimeout, out sessionNDMakeRequestResults);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00030FA2 File Offset: 0x0002F1A2
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return SessionStateUtility.CreateLegitStoreData(context, null, null, timeout);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00031AD8 File Offset: 0x0002FCD8
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			byte[] buf;
			int cb;
			SessionStateUtility.SerializeStoreData(this.CreateNewStoreData(context, timeout), 0, out buf, out cb, OutOfProcSessionStateStore.s_configCompressionEnabled);
			UnsafeNativeMethods.SessionNDMakeRequestResults sessionNDMakeRequestResults;
			this.MakeRequest(UnsafeNativeMethods.StateProtocolVerb.PUT, id, UnsafeNativeMethods.StateProtocolExclusive.NONE, 1, timeout, 0, buf, cb, OutOfProcSessionStateStore.s_networkTimeout, out sessionNDMakeRequestResults);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00006164 File Offset: 0x00004364
		public override void EndRequest(HttpContext context)
		{
		}

		// Token: 0x040013EF RID: 5103
		internal static readonly IntPtr INVALID_SOCKET = UnsafeNativeMethods.INVALID_HANDLE_VALUE;

		// Token: 0x040013F0 RID: 5104
		internal static readonly int WHIDBEY_MAJOR_VERSION = 2;

		// Token: 0x040013F1 RID: 5105
		internal const int STATE_NETWORK_TIMEOUT_DEFAULT = 10;

		// Token: 0x040013F2 RID: 5106
		private static string s_uribase;

		// Token: 0x040013F3 RID: 5107
		private static int s_networkTimeout;

		// Token: 0x040013F4 RID: 5108
		private static ReadWriteSpinLock s_lock;

		// Token: 0x040013F5 RID: 5109
		private static bool s_oneTimeInited;

		// Token: 0x040013F6 RID: 5110
		private static OutOfProcSessionStateStore.StateServerPartitionInfo s_singlePartitionInfo;

		// Token: 0x040013F7 RID: 5111
		private static PartitionManager s_partitionManager;

		// Token: 0x040013F8 RID: 5112
		private static bool s_usePartition;

		// Token: 0x040013F9 RID: 5113
		private static EventHandler s_onAppDomainUnload;

		// Token: 0x040013FA RID: 5114
		private static string s_configPartitionResolverType;

		// Token: 0x040013FB RID: 5115
		private static string s_configStateConnectionString;

		// Token: 0x040013FC RID: 5116
		private static string s_configStateConnectionStringFileName;

		// Token: 0x040013FD RID: 5117
		private static int s_configStateConnectionStringLineNumber;

		// Token: 0x040013FE RID: 5118
		private static bool s_configCompressionEnabled;

		// Token: 0x040013FF RID: 5119
		private IPartitionResolver _partitionResolver;

		// Token: 0x04001400 RID: 5120
		private OutOfProcSessionStateStore.StateServerPartitionInfo _partitionInfo;

		// Token: 0x04001401 RID: 5121
		private static Regex _ipv6ConnectionStringFormat = new Regex("^\\[(?<ipv6Address>.*)\\]:(?<port>\\d*)$");

		// Token: 0x020008FC RID: 2300
		private class StateServerPartitionInfo : PartitionInfo
		{
			// Token: 0x0600689D RID: 26781 RVA: 0x00174B10 File Offset: 0x00172D10
			internal StateServerPartitionInfo(ResourcePool rpool, string server, bool serverIsIPv6NumericAddress, int port) : base(rpool)
			{
				this._server = server;
				this._serverIsIPv6NumericAddress = serverIsIPv6NumericAddress;
				this._port = port;
				this._stateServerVersion = -1;
			}

			// Token: 0x17001D05 RID: 7429
			// (get) Token: 0x0600689E RID: 26782 RVA: 0x00174B36 File Offset: 0x00172D36
			internal string Server
			{
				get
				{
					return this._server;
				}
			}

			// Token: 0x17001D06 RID: 7430
			// (get) Token: 0x0600689F RID: 26783 RVA: 0x00174B3E File Offset: 0x00172D3E
			internal bool ServerIsIPv6NumericAddress
			{
				get
				{
					return this._serverIsIPv6NumericAddress;
				}
			}

			// Token: 0x17001D07 RID: 7431
			// (get) Token: 0x060068A0 RID: 26784 RVA: 0x00174B46 File Offset: 0x00172D46
			internal int Port
			{
				get
				{
					return this._port;
				}
			}

			// Token: 0x17001D08 RID: 7432
			// (get) Token: 0x060068A1 RID: 26785 RVA: 0x00174B4E File Offset: 0x00172D4E
			// (set) Token: 0x060068A2 RID: 26786 RVA: 0x00174B56 File Offset: 0x00172D56
			internal int StateServerVersion
			{
				get
				{
					return this._stateServerVersion;
				}
				set
				{
					this._stateServerVersion = value;
				}
			}

			// Token: 0x17001D09 RID: 7433
			// (get) Token: 0x060068A3 RID: 26787 RVA: 0x00174B60 File Offset: 0x00172D60
			protected override string TracingPartitionString
			{
				get
				{
					string format = this.ServerIsIPv6NumericAddress ? "[{0}]:{1}" : "{0}:{1}";
					return string.Format(CultureInfo.InvariantCulture, format, new object[]
					{
						this.Server,
						this.Port
					});
				}
			}

			// Token: 0x040036E4 RID: 14052
			private string _server;

			// Token: 0x040036E5 RID: 14053
			private bool _serverIsIPv6NumericAddress;

			// Token: 0x040036E6 RID: 14054
			private int _port;

			// Token: 0x040036E7 RID: 14055
			private int _stateServerVersion;
		}

		// Token: 0x020008FD RID: 2301
		private class OutOfProcConnection : IDisposable
		{
			// Token: 0x060068A4 RID: 26788 RVA: 0x00174BAA File Offset: 0x00172DAA
			internal OutOfProcConnection(IntPtr socket)
			{
				this._socketHandle = new HandleRef(this, socket);
				PerfCounters.IncrementCounter(AppPerfCounter.SESSION_STATE_SERVER_CONNECTIONS);
			}

			// Token: 0x060068A5 RID: 26789 RVA: 0x00174BC8 File Offset: 0x00172DC8
			~OutOfProcConnection()
			{
				this.Dispose(false);
			}

			// Token: 0x060068A6 RID: 26790 RVA: 0x00174BF8 File Offset: 0x00172DF8
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x060068A7 RID: 26791 RVA: 0x00174C07 File Offset: 0x00172E07
			private void Dispose(bool dummy)
			{
				if (this._socketHandle.Handle != OutOfProcSessionStateStore.INVALID_SOCKET)
				{
					UnsafeNativeMethods.SessionNDCloseConnection(this._socketHandle);
					this._socketHandle = new HandleRef(this, OutOfProcSessionStateStore.INVALID_SOCKET);
					PerfCounters.DecrementCounter(AppPerfCounter.SESSION_STATE_SERVER_CONNECTIONS);
				}
			}

			// Token: 0x060068A8 RID: 26792 RVA: 0x00174C43 File Offset: 0x00172E43
			internal void Detach()
			{
				this._socketHandle = new HandleRef(this, OutOfProcSessionStateStore.INVALID_SOCKET);
			}

			// Token: 0x040036E8 RID: 14056
			internal HandleRef _socketHandle;
		}
	}
}
