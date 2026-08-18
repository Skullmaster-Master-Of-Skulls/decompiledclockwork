using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	internal class HttpHeaderCollection : HttpValueCollection
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x000173EA File Offset: 0x000155EA
		internal HttpHeaderCollection(HttpWorkerRequest wr, HttpRequest request, int capacity) : base(capacity)
		{
			this._iis7WorkerRequest = (wr as IIS7WorkerRequest);
			this._request = request;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00017406 File Offset: 0x00015606
		internal HttpHeaderCollection(HttpWorkerRequest wr, HttpResponse response, int capacity) : base(capacity)
		{
			this._iis7WorkerRequest = (wr as IIS7WorkerRequest);
			this._response = response;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00017422 File Offset: 0x00015622
		internal HttpHeaderCollection(HttpHeaderCollection col) : base(col)
		{
			this._request = col._request;
			this._response = col._response;
			this._iis7WorkerRequest = col._iis7WorkerRequest;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001744F File Offset: 0x0001564F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.SetType(typeof(HttpValueCollection));
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00017469 File Offset: 0x00015669
		public override void Add(string name, string value)
		{
			if (this._iis7WorkerRequest == null)
			{
				throw new PlatformNotSupportedException();
			}
			this.SetHeader(name, value, false);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00017482 File Offset: 0x00015682
		internal void ClearInternal()
		{
			if (this._request != null)
			{
				throw new NotSupportedException();
			}
			base.Clear();
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00017498 File Offset: 0x00015698
		public override void Set(string name, string value)
		{
			if (this._iis7WorkerRequest == null)
			{
				throw new PlatformNotSupportedException();
			}
			this.SetHeader(name, value, true);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000174B4 File Offset: 0x000156B4
		internal void SetHeader(string name, string value, bool replace)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this._request != null)
			{
				this._iis7WorkerRequest.SetRequestHeader(name, value, replace);
			}
			else
			{
				if (this._response.HeadersWritten)
				{
					throw new HttpException(SR.GetString("Cannot_append_header_after_headers_sent"));
				}
				string name2 = name;
				string value2 = value;
				if (HttpRuntime.EnableHeaderChecking)
				{
					HttpEncoder.Current.HeaderNameValueEncode(name, value, out name2, out value2);
				}
				this._iis7WorkerRequest.SetHeaderEncoding(this._response.HeaderEncoding);
				this._iis7WorkerRequest.SetResponseHeader(name2, value2, replace);
				if (this._response.HasCachePolicy && StringUtil.EqualsIgnoreCase("Set-Cookie", name))
				{
					this._response.Cache.SetHasSetCookieHeader();
				}
			}
			if (replace)
			{
				base.Set(name, value);
			}
			else
			{
				base.Add(name, value);
			}
			if (this._request != null)
			{
				string value3 = replace ? value : base.Get(name);
				HttpServerVarsCollection httpServerVarsCollection = this._request.ServerVariables as HttpServerVarsCollection;
				if (httpServerVarsCollection != null)
				{
					httpServerVarsCollection.SynchronizeServerVariable("HTTP_" + name.ToUpper(CultureInfo.InvariantCulture).Replace('-', '_'), value3, false);
				}
				this._request.InvalidateParams();
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000175EE File Offset: 0x000157EE
		internal void SynchronizeHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value != null)
			{
				base.Set(name, value);
			}
			else
			{
				base.Remove(name);
			}
			if (this._request != null)
			{
				this._request.InvalidateParams();
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00017628 File Offset: 0x00015828
		public override void Remove(string name)
		{
			if (this._iis7WorkerRequest == null)
			{
				throw new PlatformNotSupportedException();
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._request != null)
			{
				this._iis7WorkerRequest.SetRequestHeader(name, null, false);
			}
			else
			{
				this._iis7WorkerRequest.SetResponseHeader(name, null, false);
			}
			base.Remove(name);
			if (this._request != null)
			{
				HttpServerVarsCollection httpServerVarsCollection = this._request.ServerVariables as HttpServerVarsCollection;
				if (httpServerVarsCollection != null)
				{
					httpServerVarsCollection.SynchronizeServerVariable("HTTP_" + name.ToUpper(CultureInfo.InvariantCulture).Replace('-', '_'), null, false);
				}
				this._request.InvalidateParams();
			}
		}

		// Token: 0x040003B7 RID: 951
		private HttpRequest _request;

		// Token: 0x040003B8 RID: 952
		private HttpResponse _response;

		// Token: 0x040003B9 RID: 953
		private IIS7WorkerRequest _iis7WorkerRequest;
	}
}
