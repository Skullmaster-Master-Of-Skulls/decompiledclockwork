using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020006FD RID: 1789
	internal class Identity
	{
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x000D8CEA File Offset: 0x000D7CEA
		internal static string ProcessIDGuid
		{
			get
			{
				return SharedStatics.Remoting_Identity_IDGuid;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x000D8CF1 File Offset: 0x000D7CF1
		internal static string AppDomainUniqueId
		{
			get
			{
				if (Identity.s_configuredAppDomainGuid != null)
				{
					return Identity.s_configuredAppDomainGuid;
				}
				return Identity.s_originalAppDomainGuid;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x000D8D05 File Offset: 0x000D7D05
		internal static string IDGuidString
		{
			get
			{
				return Identity.s_IDGuidString;
			}
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000D8D0C File Offset: 0x000D7D0C
		internal static string RemoveAppNameOrAppGuidIfNecessary(string uri)
		{
			if (uri == null || uri.Length <= 1 || uri[0] != '/')
			{
				return uri;
			}
			string text;
			if (Identity.s_configuredAppDomainGuidString != null)
			{
				text = Identity.s_configuredAppDomainGuidString;
				if (uri.Length > text.Length && Identity.StringStartsWith(uri, text))
				{
					return uri.Substring(text.Length);
				}
			}
			text = Identity.s_originalAppDomainGuidString;
			if (uri.Length > text.Length && Identity.StringStartsWith(uri, text))
			{
				return uri.Substring(text.Length);
			}
			string applicationName = RemotingConfiguration.ApplicationName;
			if (applicationName != null && uri.Length > applicationName.Length + 2 && string.Compare(uri, 1, applicationName, 0, applicationName.Length, true, CultureInfo.InvariantCulture) == 0 && uri[applicationName.Length + 1] == '/')
			{
				return uri.Substring(applicationName.Length + 2);
			}
			uri = uri.Substring(1);
			return uri;
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000D8DE8 File Offset: 0x000D7DE8
		private static bool StringStartsWith(string s1, string prefix)
		{
			return s1.Length >= prefix.Length && string.CompareOrdinal(s1, 0, prefix, 0, prefix.Length) == 0;
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x000D8E0C File Offset: 0x000D7E0C
		internal static string ProcessGuid
		{
			get
			{
				return Identity.ProcessIDGuid;
			}
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000D8E13 File Offset: 0x000D7E13
		private static int GetNextSeqNum()
		{
			return SharedStatics.Remoting_Identity_GetNextSeqNum();
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000D8E1C File Offset: 0x000D7E1C
		private static byte[] GetRandomBytes()
		{
			byte[] array = new byte[18];
			Identity.s_rng.GetBytes(array);
			return array;
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000D8E3D File Offset: 0x000D7E3D
		internal Identity(string objURI, string URL)
		{
			if (URL != null)
			{
				this._flags |= 256;
				this._URL = URL;
			}
			this.SetOrCreateURI(objURI, true);
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000D8E69 File Offset: 0x000D7E69
		internal Identity(bool bContextBound)
		{
			if (bContextBound)
			{
				this._flags |= 16;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003FA7 RID: 16295 RVA: 0x000D8E83 File Offset: 0x000D7E83
		internal bool IsContextBound
		{
			get
			{
				return (this._flags & 16) == 16;
			}
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000D8E92 File Offset: 0x000D7E92
		internal bool IsWellKnown()
		{
			return (this._flags & 256) == 256;
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000D8EA8 File Offset: 0x000D7EA8
		internal void SetInIDTable()
		{
			int flags;
			int value;
			do
			{
				flags = this._flags;
				value = (this._flags | 4);
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, value, flags));
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000D8ED8 File Offset: 0x000D7ED8
		internal void ResetInIDTable(bool bResetURI)
		{
			int flags;
			int value;
			do
			{
				flags = this._flags;
				value = (this._flags & -5);
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, value, flags));
			if (bResetURI)
			{
				((ObjRef)this._objRef).URI = null;
				this._ObjURI = null;
			}
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000D8F21 File Offset: 0x000D7F21
		internal bool IsInIDTable()
		{
			return (this._flags & 4) == 4;
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000D8F30 File Offset: 0x000D7F30
		internal void SetFullyConnected()
		{
			int flags;
			int value;
			do
			{
				flags = this._flags;
				value = (this._flags & -4);
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, value, flags));
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000D8F5E File Offset: 0x000D7F5E
		internal bool IsFullyDisconnected()
		{
			return (this._flags & 1) == 1;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000D8F6B File Offset: 0x000D7F6B
		internal bool IsRemoteDisconnected()
		{
			return (this._flags & 2) == 2;
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x000D8F78 File Offset: 0x000D7F78
		internal bool IsDisconnected()
		{
			return this.IsFullyDisconnected() || this.IsRemoteDisconnected();
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x000D8F8A File Offset: 0x000D7F8A
		internal string URI
		{
			get
			{
				if (this.IsWellKnown())
				{
					return this._URL;
				}
				return this._ObjURI;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x000D8FA1 File Offset: 0x000D7FA1
		internal string ObjURI
		{
			get
			{
				return this._ObjURI;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x000D8FA9 File Offset: 0x000D7FA9
		internal MarshalByRefObject TPOrObject
		{
			get
			{
				return (MarshalByRefObject)this._tpOrObject;
			}
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x000D8FB6 File Offset: 0x000D7FB6
		internal object RaceSetTransparentProxy(object tpObj)
		{
			if (this._tpOrObject == null)
			{
				Interlocked.CompareExchange(ref this._tpOrObject, tpObj, null);
			}
			return this._tpOrObject;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x000D8FD4 File Offset: 0x000D7FD4
		internal ObjRef ObjectRef
		{
			get
			{
				return (ObjRef)this._objRef;
			}
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x000D8FE1 File Offset: 0x000D7FE1
		internal ObjRef RaceSetObjRef(ObjRef objRefGiven)
		{
			if (this._objRef == null)
			{
				Interlocked.CompareExchange(ref this._objRef, objRefGiven, null);
			}
			return (ObjRef)this._objRef;
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x000D9004 File Offset: 0x000D8004
		internal IMessageSink ChannelSink
		{
			get
			{
				return (IMessageSink)this._channelSink;
			}
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x000D9011 File Offset: 0x000D8011
		internal IMessageSink RaceSetChannelSink(IMessageSink channelSink)
		{
			if (this._channelSink == null)
			{
				Interlocked.CompareExchange(ref this._channelSink, channelSink, null);
			}
			return (IMessageSink)this._channelSink;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x000D9034 File Offset: 0x000D8034
		internal IMessageSink EnvoyChain
		{
			get
			{
				return (IMessageSink)this._envoyChain;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x000D9041 File Offset: 0x000D8041
		// (set) Token: 0x06003FBA RID: 16314 RVA: 0x000D9049 File Offset: 0x000D8049
		internal Lease Lease
		{
			get
			{
				return this._lease;
			}
			set
			{
				this._lease = value;
			}
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000D9052 File Offset: 0x000D8052
		internal IMessageSink RaceSetEnvoyChain(IMessageSink envoyChain)
		{
			if (this._envoyChain == null)
			{
				Interlocked.CompareExchange(ref this._envoyChain, envoyChain, null);
			}
			return (IMessageSink)this._envoyChain;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000D9075 File Offset: 0x000D8075
		internal void SetOrCreateURI(string uri)
		{
			this.SetOrCreateURI(uri, false);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x000D9080 File Offset: 0x000D8080
		internal void SetOrCreateURI(string uri, bool bIdCtor)
		{
			if (!bIdCtor && this._ObjURI != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
			}
			if (uri == null)
			{
				string text = Convert.ToBase64String(Identity.GetRandomBytes());
				this._ObjURI = string.Concat(new object[]
				{
					Identity.IDGuidString,
					text.Replace('/', '_'),
					"_",
					Identity.GetNextSeqNum(),
					".rem"
				}).ToLower(CultureInfo.InvariantCulture);
				return;
			}
			if (this is ServerIdentity)
			{
				this._ObjURI = Identity.IDGuidString + uri;
				return;
			}
			this._ObjURI = uri;
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x000D9127 File Offset: 0x000D8127
		internal static string GetNewLogicalCallID()
		{
			return Identity.IDGuidString + Identity.GetNextSeqNum();
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x000D913D File Offset: 0x000D813D
		[Conditional("_DEBUG")]
		internal virtual void AssertValid()
		{
			if (this.URI != null)
			{
				IdentityHolder.ResolveIdentity(this.URI);
			}
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x000D9154 File Offset: 0x000D8154
		internal bool AddProxySideDynamicProperty(IDynamicProperty prop)
		{
			bool result;
			lock (this)
			{
				if (this._dph == null)
				{
					DynamicPropertyHolder dph = new DynamicPropertyHolder();
					lock (this)
					{
						if (this._dph == null)
						{
							this._dph = dph;
						}
					}
				}
				result = this._dph.AddDynamicProperty(prop);
			}
			return result;
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x000D91CC File Offset: 0x000D81CC
		internal bool RemoveProxySideDynamicProperty(string name)
		{
			bool result;
			lock (this)
			{
				if (this._dph == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), new object[]
					{
						name
					}));
				}
				result = this._dph.RemoveDynamicProperty(name);
			}
			return result;
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x000D9238 File Offset: 0x000D8238
		internal ArrayWithSize ProxySideDynamicSinks
		{
			get
			{
				if (this._dph == null)
				{
					return null;
				}
				return this._dph.DynamicSinks;
			}
		}

		// Token: 0x04002023 RID: 8227
		protected const int IDFLG_DISCONNECTED_FULL = 1;

		// Token: 0x04002024 RID: 8228
		protected const int IDFLG_DISCONNECTED_REM = 2;

		// Token: 0x04002025 RID: 8229
		protected const int IDFLG_IN_IDTABLE = 4;

		// Token: 0x04002026 RID: 8230
		protected const int IDFLG_CONTEXT_BOUND = 16;

		// Token: 0x04002027 RID: 8231
		protected const int IDFLG_WELLKNOWN = 256;

		// Token: 0x04002028 RID: 8232
		protected const int IDFLG_SERVER_SINGLECALL = 512;

		// Token: 0x04002029 RID: 8233
		protected const int IDFLG_SERVER_SINGLETON = 1024;

		// Token: 0x0400202A RID: 8234
		private static string s_originalAppDomainGuid = Guid.NewGuid().ToString().Replace('-', '_');

		// Token: 0x0400202B RID: 8235
		private static string s_configuredAppDomainGuid = null;

		// Token: 0x0400202C RID: 8236
		private static string s_originalAppDomainGuidString = "/" + Identity.s_originalAppDomainGuid.ToLower(CultureInfo.InvariantCulture) + "/";

		// Token: 0x0400202D RID: 8237
		private static string s_configuredAppDomainGuidString = null;

		// Token: 0x0400202E RID: 8238
		private static string s_IDGuidString = "/" + Identity.s_originalAppDomainGuid.ToLower(CultureInfo.InvariantCulture) + "/";

		// Token: 0x0400202F RID: 8239
		private static RNGCryptoServiceProvider s_rng = new RNGCryptoServiceProvider();

		// Token: 0x04002030 RID: 8240
		internal int _flags;

		// Token: 0x04002031 RID: 8241
		internal object _tpOrObject;

		// Token: 0x04002032 RID: 8242
		protected string _ObjURI;

		// Token: 0x04002033 RID: 8243
		protected string _URL;

		// Token: 0x04002034 RID: 8244
		internal object _objRef;

		// Token: 0x04002035 RID: 8245
		internal object _channelSink;

		// Token: 0x04002036 RID: 8246
		internal object _envoyChain;

		// Token: 0x04002037 RID: 8247
		internal DynamicPropertyHolder _dph;

		// Token: 0x04002038 RID: 8248
		internal Lease _lease;
	}
}
