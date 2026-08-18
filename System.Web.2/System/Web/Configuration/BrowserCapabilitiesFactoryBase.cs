using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace System.Web.Configuration
{
	// Token: 0x020006A7 RID: 1703
	public class BrowserCapabilitiesFactoryBase
	{
		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x00122AD0 File Offset: 0x00120CD0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected IDictionary BrowserElements
		{
			get
			{
				if (this._browserElements == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._browserElements == null)
						{
							Hashtable hashtable = Hashtable.Synchronized(new Hashtable(StringComparer.OrdinalIgnoreCase));
							this.PopulateBrowserElements(hashtable);
							this._browserElements = hashtable;
						}
					}
				}
				return this._browserElements;
			}
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x00006164 File Offset: 0x00004364
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void PopulateBrowserElements(IDictionary dictionary)
		{
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x00122B40 File Offset: 0x00120D40
		internal IDictionary InternalGetMatchedHeaders()
		{
			return this.MatchedHeaders;
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00122B48 File Offset: 0x00120D48
		internal IDictionary InternalGetBrowserElements()
		{
			return this.BrowserElements;
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x0600529F RID: 21151 RVA: 0x00122B50 File Offset: 0x00120D50
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected IDictionary MatchedHeaders
		{
			get
			{
				if (this._matchedHeaders == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._matchedHeaders == null)
						{
							Hashtable hashtable = Hashtable.Synchronized(new Hashtable(24, StringComparer.OrdinalIgnoreCase));
							this.PopulateMatchedHeaders(hashtable);
							this._matchedHeaders = hashtable;
						}
					}
				}
				return this._matchedHeaders;
			}
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x00006164 File Offset: 0x00004364
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void PopulateMatchedHeaders(IDictionary dictionary)
		{
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x00122BC0 File Offset: 0x00120DC0
		internal int CompareFilters(string filter1, string filter2)
		{
			bool flag = string.IsNullOrEmpty(filter1);
			bool flag2 = string.IsNullOrEmpty(filter2);
			IDictionary browserElements = this.BrowserElements;
			bool flag3 = browserElements.Contains(filter1) || flag;
			bool flag4 = browserElements.Contains(filter2) || flag2;
			if (!flag3)
			{
				if (!flag4)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (!flag4)
				{
					return 1;
				}
				if (flag && !flag2)
				{
					return 1;
				}
				if (flag2 && !flag)
				{
					return -1;
				}
				if (flag && flag2)
				{
					return 0;
				}
				int num = (int)((Triplet)this.BrowserElements[filter1]).Third;
				int num2 = (int)((Triplet)this.BrowserElements[filter2]).Third;
				return num2 - num;
			}
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ConfigureBrowserCapabilities(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ConfigureCustomCapabilities(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x00122C60 File Offset: 0x00120E60
		internal static string GetBrowserCapKey(IDictionary headers, HttpRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in headers.Keys)
			{
				string text = (string)obj;
				if (text.Length == 0)
				{
					stringBuilder.Append(HttpCapabilitiesDefaultProvider.GetUserAgent(request));
				}
				else
				{
					stringBuilder.Append(request.Headers[text]);
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x00122CF4 File Offset: 0x00120EF4
		internal HttpBrowserCapabilities GetHttpBrowserCapabilities(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			NameValueCollection headers = request.Headers;
			HttpBrowserCapabilities httpBrowserCapabilities = new HttpBrowserCapabilities();
			Hashtable hashtable = new Hashtable(180, StringComparer.OrdinalIgnoreCase);
			hashtable[string.Empty] = HttpCapabilitiesDefaultProvider.GetUserAgent(request);
			httpBrowserCapabilities.Capabilities = hashtable;
			this.ConfigureBrowserCapabilities(headers, httpBrowserCapabilities);
			this.ConfigureCustomCapabilities(headers, httpBrowserCapabilities);
			return httpBrowserCapabilities;
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x00122D55 File Offset: 0x00120F55
		protected bool IsBrowserUnknown(HttpCapabilitiesBase browserCaps)
		{
			return browserCaps.Browsers == null || browserCaps.Browsers.Count <= 1;
		}

		// Token: 0x04002B52 RID: 11090
		private IDictionary _matchedHeaders;

		// Token: 0x04002B53 RID: 11091
		private IDictionary _browserElements;

		// Token: 0x04002B54 RID: 11092
		private object _lock = new object();
	}
}
