using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000BA RID: 186
	internal class HttpServerVarsCollection : HttpValueCollection
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x00024629 File Offset: 0x00022829
		internal HttpServerVarsCollection(HttpWorkerRequest wr, HttpRequest request) : base(59)
		{
			this._iis7workerRequest = (wr as IIS7WorkerRequest);
			this._request = request;
			this._populated = false;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002464D File Offset: 0x0002284D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new SerializationException();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00024654 File Offset: 0x00022854
		internal void Dispose()
		{
			this._request = null;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002465D File Offset: 0x0002285D
		internal void AddStatic(string name, string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			base.InvalidateCachedArrays();
			base.BaseAdd(name, new HttpServerVarsCollectionEntry(name, value));
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002467D File Offset: 0x0002287D
		internal void AddDynamic(string name, DynamicServerVariable var)
		{
			base.InvalidateCachedArrays();
			base.BaseAdd(name, new HttpServerVarsCollectionEntry(name, var));
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00024694 File Offset: 0x00022894
		private string GetServerVar(object e)
		{
			HttpServerVarsCollectionEntry httpServerVarsCollectionEntry = (HttpServerVarsCollectionEntry)e;
			if (httpServerVarsCollectionEntry == null)
			{
				return null;
			}
			return httpServerVarsCollectionEntry.GetValue(this._request);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000246BC File Offset: 0x000228BC
		private void Populate()
		{
			if (!this._populated)
			{
				if (this._request != null)
				{
					base.MakeReadWrite();
					this._request.FillInServerVariablesCollection();
					if (this._unsyncedEntries != null)
					{
						foreach (HttpServerVarsCollectionEntry httpServerVarsCollectionEntry in this._unsyncedEntries)
						{
							HttpServerVarsCollectionEntry httpServerVarsCollectionEntry2 = (HttpServerVarsCollectionEntry)base.BaseGet(httpServerVarsCollectionEntry.Name);
							if (httpServerVarsCollectionEntry2 == null || !httpServerVarsCollectionEntry2.IsDynamic)
							{
								base.InvalidateCachedArrays();
								base.BaseSet(httpServerVarsCollectionEntry.Name, httpServerVarsCollectionEntry);
							}
						}
						this._unsyncedEntries.Clear();
					}
					if (this._iis7workerRequest == null)
					{
						base.MakeReadOnly();
					}
				}
				this._populated = true;
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00024788 File Offset: 0x00022988
		private string GetSimpleServerVar(string name)
		{
			if (name != null && name.Length > 1 && this._request != null)
			{
				char c = name[0];
				if (c <= 'S')
				{
					if (c != 'A')
					{
						if (c == 'H')
						{
							goto IL_B1;
						}
						switch (c)
						{
						case 'P':
							goto IL_E9;
						case 'Q':
							goto IL_CD;
						case 'R':
							goto IL_11E;
						case 'S':
							goto IL_183;
						default:
							goto IL_19C;
						}
					}
				}
				else if (c != 'a')
				{
					if (c == 'h')
					{
						goto IL_B1;
					}
					switch (c)
					{
					case 'p':
						goto IL_E9;
					case 'q':
						goto IL_CD;
					case 'r':
						goto IL_11E;
					case 's':
						goto IL_183;
					default:
						goto IL_19C;
					}
				}
				if (StringUtil.EqualsIgnoreCase(name, "AUTH_TYPE"))
				{
					return this._request.CalcDynamicServerVariable(DynamicServerVariable.AUTH_TYPE);
				}
				if (StringUtil.EqualsIgnoreCase(name, "AUTH_USER"))
				{
					return this._request.CalcDynamicServerVariable(DynamicServerVariable.AUTH_USER);
				}
				goto IL_19C;
				IL_B1:
				if (StringUtil.EqualsIgnoreCase(name, "HTTP_USER_AGENT"))
				{
					return this._request.UserAgent;
				}
				goto IL_19C;
				IL_CD:
				if (StringUtil.EqualsIgnoreCase(name, "QUERY_STRING"))
				{
					return this._request.QueryStringText;
				}
				goto IL_19C;
				IL_E9:
				if (StringUtil.EqualsIgnoreCase(name, "PATH_INFO"))
				{
					return this._request.Path;
				}
				if (StringUtil.EqualsIgnoreCase(name, "PATH_TRANSLATED"))
				{
					return this._request.PhysicalPath;
				}
				goto IL_19C;
				IL_11E:
				if (StringUtil.EqualsIgnoreCase(name, "REQUEST_METHOD"))
				{
					return this._request.HttpMethod;
				}
				if (StringUtil.EqualsIgnoreCase(name, "REMOTE_USER"))
				{
					return this._request.CalcDynamicServerVariable(DynamicServerVariable.AUTH_USER);
				}
				if (StringUtil.EqualsIgnoreCase(name, "REMOTE_HOST"))
				{
					return this._request.UserHostName;
				}
				if (StringUtil.EqualsIgnoreCase(name, "REMOTE_ADDRESS"))
				{
					return this._request.UserHostAddress;
				}
				goto IL_19C;
				IL_183:
				if (StringUtil.EqualsIgnoreCase(name, "SCRIPT_NAME"))
				{
					return this._request.FilePath;
				}
			}
			IL_19C:
			return null;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00024932 File Offset: 0x00022B32
		public override IEnumerator GetEnumerator()
		{
			this.Populate();
			return base.GetEnumerator();
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00024940 File Offset: 0x00022B40
		public override int Count
		{
			get
			{
				this.Populate();
				return base.Count;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void Add(string name, string value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00024950 File Offset: 0x00022B50
		public override string Get(string name)
		{
			if (!this._populated)
			{
				string simpleServerVar = this.GetSimpleServerVar(name);
				if (simpleServerVar != null)
				{
					return simpleServerVar;
				}
				this.Populate();
			}
			if (this._iis7workerRequest != null)
			{
				string text = this.GetServerVar(base.BaseGet(name));
				if (string.IsNullOrEmpty(text))
				{
					text = this._request.FetchServerVariable(name);
				}
				return text;
			}
			return this.GetServerVar(base.BaseGet(name));
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000249B4 File Offset: 0x00022BB4
		public override string[] GetValues(string name)
		{
			string text = this.Get(name);
			if (text == null)
			{
				return null;
			}
			return new string[]
			{
				text
			};
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000249D8 File Offset: 0x00022BD8
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public override void Set(string name, string value)
		{
			if (this._iis7workerRequest == null)
			{
				throw new PlatformNotSupportedException();
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.SetNoDemand(name, value);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000249FE File Offset: 0x00022BFE
		internal void SetNoDemand(string name, string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			this._iis7workerRequest.SetServerVariable(name, value);
			this.SetServerVariableManagedOnly(name, value);
			this.SynchronizeHeader(name, value);
			this._request.InvalidateParams();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00024A34 File Offset: 0x00022C34
		private void SynchronizeHeader(string name, string value)
		{
			if (StringUtil.StringStartsWith(name, "HTTP_"))
			{
				string text = name.Substring("HTTP_".Length);
				text = text.Replace('_', '-');
				int knownRequestHeaderIndex = HttpWorkerRequest.GetKnownRequestHeaderIndex(text);
				if (knownRequestHeaderIndex > -1)
				{
					text = HttpWorkerRequest.GetKnownRequestHeaderName(knownRequestHeaderIndex);
				}
				HttpHeaderCollection httpHeaderCollection = this._request.Headers as HttpHeaderCollection;
				if (httpHeaderCollection != null)
				{
					httpHeaderCollection.SynchronizeHeader(text, value);
				}
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00024A98 File Offset: 0x00022C98
		internal void SynchronizeServerVariable(string name, string value, bool ensurePopulated = true)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value != null)
			{
				if (this._populated || ensurePopulated)
				{
					this.SetServerVariableManagedOnly(name, value);
				}
				else
				{
					if (this._unsyncedEntries == null)
					{
						this._unsyncedEntries = new List<HttpServerVarsCollectionEntry>();
					}
					this._unsyncedEntries.Add(new HttpServerVarsCollectionEntry(name, value));
				}
			}
			else
			{
				base.Remove(name);
			}
			this._request.InvalidateParams();
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00024B04 File Offset: 0x00022D04
		private void SetServerVariableManagedOnly(string name, string value)
		{
			this.Populate();
			HttpServerVarsCollectionEntry httpServerVarsCollectionEntry = (HttpServerVarsCollectionEntry)base.BaseGet(name);
			if (httpServerVarsCollectionEntry != null && httpServerVarsCollectionEntry.IsDynamic)
			{
				throw new HttpException(SR.GetString("Server_variable_cannot_be_modified"));
			}
			base.InvalidateCachedArrays();
			base.BaseSet(name, new HttpServerVarsCollectionEntry(name, value));
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00024B53 File Offset: 0x00022D53
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public override void Remove(string name)
		{
			if (this._iis7workerRequest == null)
			{
				throw new PlatformNotSupportedException();
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.RemoveNoDemand(name);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00024B78 File Offset: 0x00022D78
		internal void RemoveNoDemand(string name)
		{
			this._iis7workerRequest.SetServerVariable(name, null);
			base.Remove(name);
			this.SynchronizeHeader(name, null);
			this._request.InvalidateParams();
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00024BA1 File Offset: 0x00022DA1
		public override string Get(int index)
		{
			this.Populate();
			return this.GetServerVar(base.BaseGet(index));
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00024BB8 File Offset: 0x00022DB8
		public override string[] GetValues(int index)
		{
			string text = this.Get(index);
			if (text == null)
			{
				return null;
			}
			return new string[]
			{
				text
			};
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00024BDC File Offset: 0x00022DDC
		public override string GetKey(int index)
		{
			this.Populate();
			return base.GetKey(index);
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00024BEB File Offset: 0x00022DEB
		public override string[] AllKeys
		{
			get
			{
				this.Populate();
				return base.AllKeys;
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00024BFC File Offset: 0x00022DFC
		internal override string ToString(bool urlencoded)
		{
			this.Populate();
			StringBuilder stringBuilder = new StringBuilder();
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append('&');
				}
				string text = this.GetKey(i);
				if (urlencoded)
				{
					text = HttpValueCollection.UrlEncodeForToString(text);
				}
				stringBuilder.Append(text);
				stringBuilder.Append('=');
				string text2 = this.Get(i);
				if (urlencoded)
				{
					text2 = HttpValueCollection.UrlEncodeForToString(text2);
				}
				stringBuilder.Append(text2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040004DB RID: 1243
		private bool _populated;

		// Token: 0x040004DC RID: 1244
		private HttpRequest _request;

		// Token: 0x040004DD RID: 1245
		private IIS7WorkerRequest _iis7workerRequest;

		// Token: 0x040004DE RID: 1246
		private List<HttpServerVarsCollectionEntry> _unsyncedEntries;
	}
}
