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
	// Token: 0x02000189 RID: 393
	[Serializable]
	public class WebProxy : IAutoWebProxy, IWebProxy, ISerializable
	{
		// Token: 0x06000EA4 RID: 3748 RVA: 0x0004D419 File Offset: 0x0004B619
		public WebProxy() : this(null, false, null, null)
		{
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0004D425 File Offset: 0x0004B625
		public WebProxy(Uri Address) : this(Address, false, null, null)
		{
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0004D431 File Offset: 0x0004B631
		public WebProxy(Uri Address, bool BypassOnLocal) : this(Address, BypassOnLocal, null, null)
		{
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0004D43D File Offset: 0x0004B63D
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList) : this(Address, BypassOnLocal, BypassList, null)
		{
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0004D449 File Offset: 0x0004B649
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

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0004D484 File Offset: 0x0004B684
		public WebProxy(string Host, int Port) : this(new Uri("http://" + Host + ":" + Port.ToString(CultureInfo.InvariantCulture)), false, null, null)
		{
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0004D4B0 File Offset: 0x0004B6B0
		public WebProxy(string Address) : this(WebProxy.CreateProxyUri(Address), false, null, null)
		{
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0004D4C1 File Offset: 0x0004B6C1
		public WebProxy(string Address, bool BypassOnLocal) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, null, null)
		{
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0004D4D2 File Offset: 0x0004B6D2
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, null)
		{
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0004D4E3 File Offset: 0x0004B6E3
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, Credentials)
		{
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0004D4F5 File Offset: 0x0004B6F5
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0004D503 File Offset: 0x0004B703
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

		// Token: 0x17000340 RID: 832
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x0004D520 File Offset: 0x0004B720
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

		// Token: 0x17000341 RID: 833
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x0004D543 File Offset: 0x0004B743
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

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0004D566 File Offset: 0x0004B766
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x0004D574 File Offset: 0x0004B774
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

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0004D58A File Offset: 0x0004B78A
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x0004D5BF File Offset: 0x0004B7BF
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0004D5E1 File Offset: 0x0004B7E1
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x0004D5E9 File Offset: 0x0004B7E9
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

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0004D5F2 File Offset: 0x0004B7F2
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x0004D604 File Offset: 0x0004B804
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

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0004D617 File Offset: 0x0004B817
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

		// Token: 0x06000EBB RID: 3771 RVA: 0x0004D638 File Offset: 0x0004B838
		internal void CheckForChanges()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.CheckForChanges();
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0004D650 File Offset: 0x0004B850
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

		// Token: 0x06000EBD RID: 3773 RVA: 0x0004D6B9 File Offset: 0x0004B8B9
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

		// Token: 0x06000EBE RID: 3774 RVA: 0x0004D6E4 File Offset: 0x0004B8E4
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

		// Token: 0x06000EBF RID: 3775 RVA: 0x0004D764 File Offset: 0x0004B964
		private bool IsMatchInBypassList(Uri input)
		{
			this.UpdateRegExList(false);
			if (this._RegExBypassList == null)
			{
				return false;
			}
			string input2 = input.Scheme + "://" + input.Host + ((!input.IsDefaultPort) ? (":" + input.Port.ToString()) : "");
			for (int i = 0; i < this._BypassList.Count; i++)
			{
				if (this._RegExBypassList[i].IsMatch(input2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0004D7EC File Offset: 0x0004B9EC
		private bool IsLocal(Uri host)
		{
			string host2 = host.Host;
			IPAddress ipaddress;
			if (IPAddress.TryParse(host2, out ipaddress))
			{
				return IPAddress.IsLoopback(ipaddress) || NclUtilities.IsAddressLocal(ipaddress);
			}
			int num = host2.IndexOf('.');
			if (num == -1)
			{
				return true;
			}
			string text = "." + IPGlobalProperties.InternalGetIPGlobalProperties().DomainName;
			return text != null && text.Length == host2.Length - num && string.Compare(text, 0, host2, num, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0004D868 File Offset: 0x0004BA68
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

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0004D8A0 File Offset: 0x0004BAA0
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

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0004D8D8 File Offset: 0x0004BAD8
		private bool IsBypassedManual(Uri host)
		{
			return host.IsLoopback || (this._ProxyAddress == null && this._ProxyHostAddresses == null) || (this._BypassOnLocal && this.IsLocal(host)) || this.IsMatchInBypassList(host) || this.IsLocalInProxyHash(host);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0004D928 File Offset: 0x0004BB28
		[Obsolete("This method has been deprecated. Please use the proxy selected for you by default. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static WebProxy GetDefaultProxy()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			return new WebProxy(true);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0004D93C File Offset: 0x0004BB3C
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

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0004D9FC File Offset: 0x0004BBFC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0004DA08 File Offset: 0x0004BC08
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

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0004DA6D File Offset: 0x0004BC6D
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x0004DA75 File Offset: 0x0004BC75
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

		// Token: 0x06000ECA RID: 3786 RVA: 0x0004DA7E File Offset: 0x0004BC7E
		internal WebProxy(bool enableAutoproxy)
		{
			this.m_EnableAutoproxy = enableAutoproxy;
			this.UnsafeUpdateFromRegistry();
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0004DA93 File Offset: 0x0004BC93
		internal void DeleteScriptEngine()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Close();
				this.ScriptEngine = null;
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0004DAB0 File Offset: 0x0004BCB0
		internal void UnsafeUpdateFromRegistry()
		{
			this._UseRegistry = true;
			this.ScriptEngine = new AutoWebProxyScriptEngine(this, true);
			WebProxyData webProxyData = this.ScriptEngine.GetWebProxyData();
			this.Update(webProxyData);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0004DAE4 File Offset: 0x0004BCE4
		internal void Update(WebProxyData webProxyData)
		{
			lock (this)
			{
				this._BypassOnLocal = webProxyData.bypassOnLocal;
				this._ProxyAddress = webProxyData.proxyAddress;
				this._ProxyHostAddresses = webProxyData.proxyHostAddresses;
				this._BypassList = webProxyData.bypassList;
				this.ScriptEngine.AutomaticallyDetectSettings = (this.m_EnableAutoproxy && webProxyData.automaticallyDetectSettings);
				this.ScriptEngine.AutomaticConfigurationScript = (this.m_EnableAutoproxy ? webProxyData.scriptLocation : null);
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0004DB84 File Offset: 0x0004BD84
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			return new ProxyScriptChain(this, destination);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0004DBA4 File Offset: 0x0004BDA4
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

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0004DBF8 File Offset: 0x0004BDF8
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

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0004DC3C File Offset: 0x0004BE3C
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
				array = new Uri[1];
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

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0004DCBA File Offset: 0x0004BEBA
		internal void AbortGetProxiesAuto(ref int syncStatus)
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Abort(ref syncStatus);
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0004DCD0 File Offset: 0x0004BED0
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

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0004DD0C File Offset: 0x0004BF0C
		private static bool AreAllBypassed(IEnumerable<string> proxies, bool checkFirstOnly)
		{
			bool flag = true;
			foreach (string value in proxies)
			{
				flag = string.IsNullOrEmpty(value);
				if (checkFirstOnly)
				{
					break;
				}
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0004DD60 File Offset: 0x0004BF60
		private static Uri ProxyUri(string proxyName)
		{
			if (proxyName != null && proxyName.Length != 0)
			{
				return new Uri("http://" + proxyName);
			}
			return null;
		}

		// Token: 0x0400128A RID: 4746
		private bool _UseRegistry;

		// Token: 0x0400128B RID: 4747
		private bool _BypassOnLocal;

		// Token: 0x0400128C RID: 4748
		private bool m_EnableAutoproxy;

		// Token: 0x0400128D RID: 4749
		private Uri _ProxyAddress;

		// Token: 0x0400128E RID: 4750
		private ArrayList _BypassList;

		// Token: 0x0400128F RID: 4751
		private ICredentials _Credentials;

		// Token: 0x04001290 RID: 4752
		private Regex[] _RegExBypassList;

		// Token: 0x04001291 RID: 4753
		private Hashtable _ProxyHostAddresses;

		// Token: 0x04001292 RID: 4754
		private AutoWebProxyScriptEngine m_ScriptEngine;
	}
}
