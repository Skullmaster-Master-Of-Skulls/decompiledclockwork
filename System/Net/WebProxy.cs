using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x020004AC RID: 1196
	[Serializable]
	public class WebProxy : IAutoWebProxy, IWebProxy, ISerializable
	{
		// Token: 0x060024A9 RID: 9385 RVA: 0x000911EE File Offset: 0x000901EE
		public WebProxy() : this(null, false, null, null)
		{
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000911FA File Offset: 0x000901FA
		public WebProxy(Uri Address) : this(Address, false, null, null)
		{
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00091206 File Offset: 0x00090206
		public WebProxy(Uri Address, bool BypassOnLocal) : this(Address, BypassOnLocal, null, null)
		{
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00091212 File Offset: 0x00090212
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList) : this(Address, BypassOnLocal, BypassList, null)
		{
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0009121E File Offset: 0x0009021E
		internal WebProxy(Hashtable proxyHostAddresses, bool BypassOnLocal, string[] BypassList) : this(null, BypassOnLocal, BypassList, null)
		{
			this._ProxyHostAddresses = proxyHostAddresses;
			if (this._ProxyHostAddresses != null)
			{
				this._ProxyAddress = (Uri)proxyHostAddresses["http"];
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x0009124F File Offset: 0x0009024F
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
		{
			this._ProxyAddress = Address;
			this._BypassOnLocal = BypassOnLocal;
			if (BypassList != null)
			{
				this._BypassList = new ArrayList(BypassList);
				this.UpdateRegExList(true);
			}
			this._Credentials = Credentials;
			this.m_EnableAutoproxy = true;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0009128A File Offset: 0x0009028A
		public WebProxy(string Host, int Port) : this(new Uri("http://" + Host + ":" + Port.ToString(CultureInfo.InvariantCulture)), false, null, null)
		{
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000912B6 File Offset: 0x000902B6
		public WebProxy(string Address) : this(WebProxy.CreateProxyUri(Address), false, null, null)
		{
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000912C7 File Offset: 0x000902C7
		public WebProxy(string Address, bool BypassOnLocal) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, null, null)
		{
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000912D8 File Offset: 0x000902D8
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, null)
		{
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000912E9 File Offset: 0x000902E9
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, Credentials)
		{
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x000912FB File Offset: 0x000902FB
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x00091309 File Offset: 0x00090309
		public Uri Address
		{
			get
			{
				this.CheckForChanges();
				return this._ProxyAddress;
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._ProxyHostAddresses = null;
				this._ProxyAddress = value;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (set) Token: 0x060024B6 RID: 9398 RVA: 0x00091326 File Offset: 0x00090326
		internal bool AutoDetect
		{
			set
			{
				if (this.ScriptEngine == null)
				{
					this.ScriptEngine = new AutoWebProxyScriptEngine(this, false);
				}
				this.ScriptEngine.AutomaticallyDetectSettings = value;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x00091349 File Offset: 0x00090349
		internal Uri ScriptLocation
		{
			set
			{
				if (this.ScriptEngine == null)
				{
					this.ScriptEngine = new AutoWebProxyScriptEngine(this, false);
				}
				this.ScriptEngine.AutomaticConfigurationScript = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x0009136C File Offset: 0x0009036C
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x0009137A File Offset: 0x0009037A
		public bool BypassProxyOnLocal
		{
			get
			{
				this.CheckForChanges();
				return this._BypassOnLocal;
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassOnLocal = value;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x00091390 File Offset: 0x00090390
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x000913C5 File Offset: 0x000903C5
		public string[] BypassList
		{
			get
			{
				this.CheckForChanges();
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return (string[])this._BypassList.ToArray(typeof(string));
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassList = new ArrayList(value);
				this.UpdateRegExList(true);
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000913E7 File Offset: 0x000903E7
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x000913EF File Offset: 0x000903EF
		public ICredentials Credentials
		{
			get
			{
				return this._Credentials;
			}
			set
			{
				this._Credentials = value;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x000913F8 File Offset: 0x000903F8
		// (set) Token: 0x060024BF RID: 9407 RVA: 0x0009140A File Offset: 0x0009040A
		public bool UseDefaultCredentials
		{
			get
			{
				return this.Credentials is SystemNetworkCredential;
			}
			set
			{
				this._Credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x0009141D File Offset: 0x0009041D
		public ArrayList BypassArrayList
		{
			get
			{
				this.CheckForChanges();
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return this._BypassList;
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x0009143E File Offset: 0x0009043E
		internal void CheckForChanges()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.CheckForChanges();
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00091454 File Offset: 0x00090454
		public Uri GetProxy(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Uri result;
			if (this.GetProxyAuto(destination, out result))
			{
				return result;
			}
			if (this.IsBypassedManual(destination))
			{
				return destination;
			}
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			Uri uri = (proxyHostAddresses != null) ? (proxyHostAddresses[destination.Scheme] as Uri) : this._ProxyAddress;
			if (!(uri != null))
			{
				return destination;
			}
			return uri;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000914BD File Offset: 0x000904BD
		private static Uri CreateProxyUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://") == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000914E8 File Offset: 0x000904E8
		private void UpdateRegExList(bool canThrow)
		{
			Regex[] array = null;
			ArrayList bypassList = this._BypassList;
			try
			{
				if (bypassList != null && bypassList.Count > 0)
				{
					array = new Regex[bypassList.Count];
					for (int i = 0; i < bypassList.Count; i++)
					{
						array[i] = new Regex((string)bypassList[i], RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
					}
				}
			}
			catch
			{
				if (!canThrow)
				{
					this._RegExBypassList = null;
					return;
				}
				throw;
			}
			this._RegExBypassList = array;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00091568 File Offset: 0x00090568
		private bool IsMatchInBypassList(Uri input)
		{
			this.UpdateRegExList(false);
			if (this._RegExBypassList == null)
			{
				return false;
			}
			string input2 = input.Scheme + "://" + input.Host + ((!input.IsDefaultPort) ? (":" + input.Port) : "");
			for (int i = 0; i < this._BypassList.Count; i++)
			{
				if (this._RegExBypassList[i].IsMatch(input2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000915EC File Offset: 0x000905EC
		private bool IsLocal(Uri host)
		{
			string host2 = host.Host;
			int num = -1;
			bool flag = true;
			bool flag2 = false;
			for (int i = 0; i < host2.Length; i++)
			{
				if (host2[i] == '.')
				{
					if (num == -1)
					{
						num = i;
						if (!flag)
						{
							break;
						}
					}
				}
				else
				{
					if (host2[i] == ':')
					{
						flag2 = true;
						flag = false;
						break;
					}
					if (host2[i] < '0' || host2[i] > '9')
					{
						flag = false;
						if (num != -1)
						{
							break;
						}
					}
				}
			}
			if (num == -1 && !flag2)
			{
				return true;
			}
			if (!flag)
			{
				if (!flag2)
				{
					goto IL_9D;
				}
			}
			try
			{
				IPAddress ipaddress = IPAddress.Parse(host2);
				if (IPAddress.IsLoopback(ipaddress))
				{
					return true;
				}
				return NclUtilities.IsAddressLocal(ipaddress);
			}
			catch (FormatException)
			{
			}
			IL_9D:
			string text = "." + IPGlobalProperties.InternalGetIPGlobalProperties().DomainName;
			return text != null && text.Length == host2.Length - num && string.Compare(text, 0, host2, num, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000916EC File Offset: 0x000906EC
		private bool IsLocalInProxyHash(Uri host)
		{
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			if (proxyHostAddresses != null)
			{
				Uri uri = (Uri)proxyHostAddresses[host.Scheme];
				if (uri == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00091724 File Offset: 0x00090724
		public bool IsBypassed(Uri host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			bool result;
			if (this.IsBypassedAuto(host, out result))
			{
				return result;
			}
			return this.IsBypassedManual(host);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0009175C File Offset: 0x0009075C
		private bool IsBypassedManual(Uri host)
		{
			return host.IsLoopback || (this._ProxyAddress == null && this._ProxyHostAddresses == null) || (this._BypassOnLocal && this.IsLocal(host)) || this.IsMatchInBypassList(host) || this.IsLocalInProxyHash(host);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000917AC File Offset: 0x000907AC
		[Obsolete("This method has been deprecated. Please use the proxy selected for you by default. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static WebProxy GetDefaultProxy()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			return new WebProxy(true);
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000917C0 File Offset: 0x000907C0
		protected WebProxy(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			bool flag = false;
			try
			{
				flag = serializationInfo.GetBoolean("_UseRegistry");
			}
			catch
			{
			}
			if (flag)
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				this.UnsafeUpdateFromRegistry();
				return;
			}
			this._ProxyAddress = (Uri)serializationInfo.GetValue("_ProxyAddress", typeof(Uri));
			this._BypassOnLocal = serializationInfo.GetBoolean("_BypassOnLocal");
			this._BypassList = (ArrayList)serializationInfo.GetValue("_BypassList", typeof(ArrayList));
			try
			{
				this.UseDefaultCredentials = serializationInfo.GetBoolean("_UseDefaultCredentials");
			}
			catch
			{
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00091880 File Offset: 0x00090880
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0009188C File Offset: 0x0009088C
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("_BypassOnLocal", this._BypassOnLocal);
			serializationInfo.AddValue("_ProxyAddress", this._ProxyAddress);
			serializationInfo.AddValue("_BypassList", this._BypassList);
			serializationInfo.AddValue("_UseDefaultCredentials", this.UseDefaultCredentials);
			if (this._UseRegistry)
			{
				serializationInfo.AddValue("_UseRegistry", true);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x000918F1 File Offset: 0x000908F1
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x000918F9 File Offset: 0x000908F9
		internal AutoWebProxyScriptEngine ScriptEngine
		{
			get
			{
				return this.m_ScriptEngine;
			}
			set
			{
				this.m_ScriptEngine = value;
			}
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00091902 File Offset: 0x00090902
		internal WebProxy(bool enableAutoproxy)
		{
			this.m_EnableAutoproxy = enableAutoproxy;
			this.UnsafeUpdateFromRegistry();
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00091917 File Offset: 0x00090917
		internal void DeleteScriptEngine()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Close();
				this.ScriptEngine = null;
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00091934 File Offset: 0x00090934
		internal void UnsafeUpdateFromRegistry()
		{
			this._UseRegistry = true;
			this.ScriptEngine = new AutoWebProxyScriptEngine(this, true);
			WebProxyData webProxyData = this.ScriptEngine.GetWebProxyData();
			this.Update(webProxyData);
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00091968 File Offset: 0x00090968
		internal void Update(WebProxyData webProxyData)
		{
			lock (this)
			{
				this._BypassOnLocal = webProxyData.bypassOnLocal;
				this._ProxyAddress = webProxyData.proxyAddress;
				this._BypassList = webProxyData.bypassList;
				this.ScriptEngine.AutomaticallyDetectSettings = (this.m_EnableAutoproxy && webProxyData.automaticallyDetectSettings);
				this.ScriptEngine.AutomaticConfigurationScript = (this.m_EnableAutoproxy ? webProxyData.scriptLocation : null);
			}
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000919F4 File Offset: 0x000909F4
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			return new ProxyScriptChain(this, destination);
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00091A14 File Offset: 0x00090A14
		private bool GetProxyAuto(Uri destination, out Uri proxyUri)
		{
			proxyUri = null;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count > 0)
			{
				if (WebProxy.AreAllBypassed(list, true))
				{
					proxyUri = destination;
				}
				else
				{
					proxyUri = WebProxy.ProxyUri(list[0]);
				}
			}
			return true;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00091A68 File Offset: 0x00090A68
		private bool IsBypassedAuto(Uri destination, out bool isBypassed)
		{
			isBypassed = true;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count == 0)
			{
				isBypassed = false;
			}
			else
			{
				isBypassed = WebProxy.AreAllBypassed(list, true);
			}
			return true;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00091AAC File Offset: 0x00090AAC
		internal Uri[] GetProxiesAuto(Uri destination, ref int syncStatus)
		{
			if (this.ScriptEngine == null)
			{
				return null;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list, ref syncStatus))
			{
				return null;
			}
			Uri[] array;
			if (list.Count == 0)
			{
				array = new Uri[0];
			}
			else if (WebProxy.AreAllBypassed(list, false))
			{
				Uri[] array2 = new Uri[1];
				array = array2;
			}
			else
			{
				array = new Uri[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = WebProxy.ProxyUri(list[i]);
				}
			}
			return array;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00091B2C File Offset: 0x00090B2C
		internal void AbortGetProxiesAuto(ref int syncStatus)
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Abort(ref syncStatus);
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x00091B44 File Offset: 0x00090B44
		internal Uri GetProxyAutoFailover(Uri destination)
		{
			if (this.IsBypassedManual(destination))
			{
				return null;
			}
			Uri result = this._ProxyAddress;
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			if (proxyHostAddresses != null)
			{
				result = (proxyHostAddresses[destination.Scheme] as Uri);
			}
			return result;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00091B80 File Offset: 0x00090B80
		private static bool AreAllBypassed(IEnumerable<string> proxies, bool checkFirstOnly)
		{
			bool flag = true;
			foreach (string value in proxies)
			{
				flag = string.IsNullOrEmpty(value);
				if (checkFirstOnly || !flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00091BD4 File Offset: 0x00090BD4
		private static Uri ProxyUri(string proxyName)
		{
			if (proxyName != null && proxyName.Length != 0)
			{
				return new Uri("http://" + proxyName);
			}
			return null;
		}

		// Token: 0x040024D9 RID: 9433
		private bool _UseRegistry;

		// Token: 0x040024DA RID: 9434
		private bool _BypassOnLocal;

		// Token: 0x040024DB RID: 9435
		private bool m_EnableAutoproxy;

		// Token: 0x040024DC RID: 9436
		private Uri _ProxyAddress;

		// Token: 0x040024DD RID: 9437
		private ArrayList _BypassList;

		// Token: 0x040024DE RID: 9438
		private ICredentials _Credentials;

		// Token: 0x040024DF RID: 9439
		private Regex[] _RegExBypassList;

		// Token: 0x040024E0 RID: 9440
		private Hashtable _ProxyHostAddresses;

		// Token: 0x040024E1 RID: 9441
		private AutoWebProxyScriptEngine m_ScriptEngine;
	}
}
