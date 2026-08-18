using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Routing;

namespace System.Web
{
	// Token: 0x02000031 RID: 49
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpResponseWrapper : HttpResponseBase
	{
		// Token: 0x060003CC RID: 972 RVA: 0x000054EF File Offset: 0x000036EF
		public HttpResponseWrapper(HttpResponse httpResponse)
		{
			if (httpResponse == null)
			{
				throw new ArgumentNullException("httpResponse");
			}
			this._httpResponse = httpResponse;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000550C File Offset: 0x0000370C
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00005519 File Offset: 0x00003719
		public override bool Buffer
		{
			get
			{
				return this._httpResponse.Buffer;
			}
			set
			{
				this._httpResponse.Buffer = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00005527 File Offset: 0x00003727
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00005534 File Offset: 0x00003734
		public override bool BufferOutput
		{
			get
			{
				return this._httpResponse.BufferOutput;
			}
			set
			{
				this._httpResponse.BufferOutput = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00005542 File Offset: 0x00003742
		public override HttpCachePolicyBase Cache
		{
			get
			{
				return new HttpCachePolicyWrapper(this._httpResponse.Cache);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00005554 File Offset: 0x00003754
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00005561 File Offset: 0x00003761
		public override string CacheControl
		{
			get
			{
				return this._httpResponse.CacheControl;
			}
			set
			{
				this._httpResponse.CacheControl = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000556F File Offset: 0x0000376F
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000557C File Offset: 0x0000377C
		public override string Charset
		{
			get
			{
				return this._httpResponse.Charset;
			}
			set
			{
				this._httpResponse.Charset = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000558A File Offset: 0x0000378A
		public override CancellationToken ClientDisconnectedToken
		{
			get
			{
				return this._httpResponse.ClientDisconnectedToken;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00005597 File Offset: 0x00003797
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x000055A4 File Offset: 0x000037A4
		public override Encoding ContentEncoding
		{
			get
			{
				return this._httpResponse.ContentEncoding;
			}
			set
			{
				this._httpResponse.ContentEncoding = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000055B2 File Offset: 0x000037B2
		// (set) Token: 0x060003DA RID: 986 RVA: 0x000055BF File Offset: 0x000037BF
		public override string ContentType
		{
			get
			{
				return this._httpResponse.ContentType;
			}
			set
			{
				this._httpResponse.ContentType = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000055CD File Offset: 0x000037CD
		public override HttpCookieCollection Cookies
		{
			get
			{
				return this._httpResponse.Cookies;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000055DA File Offset: 0x000037DA
		// (set) Token: 0x060003DD RID: 989 RVA: 0x000055E7 File Offset: 0x000037E7
		public override int Expires
		{
			get
			{
				return this._httpResponse.Expires;
			}
			set
			{
				this._httpResponse.Expires = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060003DE RID: 990 RVA: 0x000055F5 File Offset: 0x000037F5
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00005602 File Offset: 0x00003802
		public override DateTime ExpiresAbsolute
		{
			get
			{
				return this._httpResponse.ExpiresAbsolute;
			}
			set
			{
				this._httpResponse.ExpiresAbsolute = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00005610 File Offset: 0x00003810
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000561D File Offset: 0x0000381D
		public override Stream Filter
		{
			get
			{
				return this._httpResponse.Filter;
			}
			set
			{
				this._httpResponse.Filter = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000562B File Offset: 0x0000382B
		public override NameValueCollection Headers
		{
			get
			{
				return this._httpResponse.Headers;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00005638 File Offset: 0x00003838
		public override bool HeadersWritten
		{
			get
			{
				return this._httpResponse.HeadersWritten;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00005645 File Offset: 0x00003845
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00005652 File Offset: 0x00003852
		public override Encoding HeaderEncoding
		{
			get
			{
				return this._httpResponse.HeaderEncoding;
			}
			set
			{
				this._httpResponse.HeaderEncoding = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00005660 File Offset: 0x00003860
		public override bool IsClientConnected
		{
			get
			{
				return this._httpResponse.IsClientConnected;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000566D File Offset: 0x0000386D
		public override bool IsRequestBeingRedirected
		{
			get
			{
				return this._httpResponse.IsRequestBeingRedirected;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000567A File Offset: 0x0000387A
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00005687 File Offset: 0x00003887
		public override TextWriter Output
		{
			get
			{
				return this._httpResponse.Output;
			}
			set
			{
				this._httpResponse.Output = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00005695 File Offset: 0x00003895
		public override Stream OutputStream
		{
			get
			{
				return this._httpResponse.OutputStream;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000056A2 File Offset: 0x000038A2
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x000056AF File Offset: 0x000038AF
		public override string RedirectLocation
		{
			get
			{
				return this._httpResponse.RedirectLocation;
			}
			set
			{
				this._httpResponse.RedirectLocation = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000056BD File Offset: 0x000038BD
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x000056CA File Offset: 0x000038CA
		public override string Status
		{
			get
			{
				return this._httpResponse.Status;
			}
			set
			{
				this._httpResponse.Status = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000056D8 File Offset: 0x000038D8
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x000056E5 File Offset: 0x000038E5
		public override int StatusCode
		{
			get
			{
				return this._httpResponse.StatusCode;
			}
			set
			{
				this._httpResponse.StatusCode = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000056F3 File Offset: 0x000038F3
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00005700 File Offset: 0x00003900
		public override string StatusDescription
		{
			get
			{
				return this._httpResponse.StatusDescription;
			}
			set
			{
				this._httpResponse.StatusDescription = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000570E File Offset: 0x0000390E
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000571B File Offset: 0x0000391B
		public override int SubStatusCode
		{
			get
			{
				return this._httpResponse.SubStatusCode;
			}
			set
			{
				this._httpResponse.SubStatusCode = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00005729 File Offset: 0x00003929
		public override bool SupportsAsyncFlush
		{
			get
			{
				return this._httpResponse.SupportsAsyncFlush;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00005736 File Offset: 0x00003936
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00005743 File Offset: 0x00003943
		public override bool SuppressContent
		{
			get
			{
				return this._httpResponse.SuppressContent;
			}
			set
			{
				this._httpResponse.SuppressContent = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00005751 File Offset: 0x00003951
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000575E File Offset: 0x0000395E
		public override bool SuppressDefaultCacheControlHeader
		{
			get
			{
				return this._httpResponse.SuppressDefaultCacheControlHeader;
			}
			set
			{
				this._httpResponse.SuppressDefaultCacheControlHeader = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000576C File Offset: 0x0000396C
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00005779 File Offset: 0x00003979
		public override bool SuppressFormsAuthenticationRedirect
		{
			get
			{
				return this._httpResponse.SuppressFormsAuthenticationRedirect;
			}
			set
			{
				this._httpResponse.SuppressFormsAuthenticationRedirect = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00005787 File Offset: 0x00003987
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00005794 File Offset: 0x00003994
		public override bool TrySkipIisCustomErrors
		{
			get
			{
				return this._httpResponse.TrySkipIisCustomErrors;
			}
			set
			{
				this._httpResponse.TrySkipIisCustomErrors = value;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000057A2 File Offset: 0x000039A2
		public override void AddCacheItemDependency(string cacheKey)
		{
			this._httpResponse.AddCacheItemDependency(cacheKey);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000057B0 File Offset: 0x000039B0
		public override void AddCacheItemDependencies(ArrayList cacheKeys)
		{
			this._httpResponse.AddCacheItemDependencies(cacheKeys);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000057BE File Offset: 0x000039BE
		public override void AddCacheItemDependencies(string[] cacheKeys)
		{
			this._httpResponse.AddCacheItemDependencies(cacheKeys);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000057CC File Offset: 0x000039CC
		public override void AddCacheDependency(params CacheDependency[] dependencies)
		{
			this._httpResponse.AddCacheDependency(dependencies);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000057DA File Offset: 0x000039DA
		public override void AddFileDependency(string filename)
		{
			this._httpResponse.AddFileDependency(filename);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000057E8 File Offset: 0x000039E8
		public override ISubscriptionToken AddOnSendingHeaders(Action<HttpContextBase> callback)
		{
			return this._httpResponse.AddOnSendingHeaders(HttpContextWrapper.WrapCallback(callback));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000057FB File Offset: 0x000039FB
		public override void AddFileDependencies(ArrayList filenames)
		{
			this._httpResponse.AddFileDependencies(filenames);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00005809 File Offset: 0x00003A09
		public override void AddFileDependencies(string[] filenames)
		{
			this._httpResponse.AddFileDependencies(filenames);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00005817 File Offset: 0x00003A17
		public override void AddHeader(string name, string value)
		{
			this._httpResponse.AddHeader(name, value);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00005826 File Offset: 0x00003A26
		public override void AppendCookie(HttpCookie cookie)
		{
			this._httpResponse.AppendCookie(cookie);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00005834 File Offset: 0x00003A34
		public override void AppendHeader(string name, string value)
		{
			this._httpResponse.AppendHeader(name, value);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00005843 File Offset: 0x00003A43
		public override void AppendToLog(string param)
		{
			this._httpResponse.AppendToLog(param);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00005851 File Offset: 0x00003A51
		public override string ApplyAppPathModifier(string virtualPath)
		{
			return this._httpResponse.ApplyAppPathModifier(virtualPath);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000585F File Offset: 0x00003A5F
		public override IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			return this._httpResponse.BeginFlush(callback, state);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000586E File Offset: 0x00003A6E
		public override void BinaryWrite(byte[] buffer)
		{
			this._httpResponse.BinaryWrite(buffer);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000587C File Offset: 0x00003A7C
		public override void Clear()
		{
			this._httpResponse.Clear();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00005889 File Offset: 0x00003A89
		public override void ClearContent()
		{
			this._httpResponse.ClearContent();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00005896 File Offset: 0x00003A96
		public override void ClearHeaders()
		{
			this._httpResponse.ClearHeaders();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000058A3 File Offset: 0x00003AA3
		public override void Close()
		{
			this._httpResponse.Close();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000058B0 File Offset: 0x00003AB0
		public override void DisableKernelCache()
		{
			this._httpResponse.DisableKernelCache();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000058BD File Offset: 0x00003ABD
		public override void DisableUserCache()
		{
			this._httpResponse.DisableUserCache();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000058CA File Offset: 0x00003ACA
		public override void End()
		{
			this._httpResponse.End();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000058D7 File Offset: 0x00003AD7
		public override void EndFlush(IAsyncResult asyncResult)
		{
			this._httpResponse.EndFlush(asyncResult);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000058E5 File Offset: 0x00003AE5
		public override void Flush()
		{
			this._httpResponse.Flush();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000058F2 File Offset: 0x00003AF2
		public override Task FlushAsync()
		{
			return this._httpResponse.FlushAsync();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000058FF File Offset: 0x00003AFF
		public override void Pics(string value)
		{
			this._httpResponse.Pics(value);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000590D File Offset: 0x00003B0D
		public override void Redirect(string url)
		{
			this._httpResponse.Redirect(url);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000591B File Offset: 0x00003B1B
		public override void Redirect(string url, bool endResponse)
		{
			this._httpResponse.Redirect(url, endResponse);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000592A File Offset: 0x00003B2A
		public override void RedirectPermanent(string url)
		{
			this._httpResponse.RedirectPermanent(url);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00005938 File Offset: 0x00003B38
		public override void RedirectPermanent(string url, bool endResponse)
		{
			this._httpResponse.RedirectPermanent(url, endResponse);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00005947 File Offset: 0x00003B47
		public override void RedirectToRoute(object routeValues)
		{
			this._httpResponse.RedirectToRoute(routeValues);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00005955 File Offset: 0x00003B55
		public override void RedirectToRoute(string routeName)
		{
			this._httpResponse.RedirectToRoute(routeName);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00005963 File Offset: 0x00003B63
		public override void RedirectToRoute(RouteValueDictionary routeValues)
		{
			this._httpResponse.RedirectToRoute(routeValues);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00005971 File Offset: 0x00003B71
		public override void RedirectToRoute(string routeName, object routeValues)
		{
			this._httpResponse.RedirectToRoute(routeName, routeValues);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00005980 File Offset: 0x00003B80
		public override void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
		{
			this._httpResponse.RedirectToRoute(routeName, routeValues);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000598F File Offset: 0x00003B8F
		public override void RedirectToRoutePermanent(object routeValues)
		{
			this._httpResponse.RedirectToRoutePermanent(routeValues);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000599D File Offset: 0x00003B9D
		public override void RedirectToRoutePermanent(string routeName)
		{
			this._httpResponse.RedirectToRoutePermanent(routeName);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000059AB File Offset: 0x00003BAB
		public override void RedirectToRoutePermanent(RouteValueDictionary routeValues)
		{
			this._httpResponse.RedirectToRoutePermanent(routeValues);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000059B9 File Offset: 0x00003BB9
		public override void RedirectToRoutePermanent(string routeName, object routeValues)
		{
			this._httpResponse.RedirectToRoutePermanent(routeName, routeValues);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000059C8 File Offset: 0x00003BC8
		public override void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
		{
			this._httpResponse.RedirectToRoutePermanent(routeName, routeValues);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000059D7 File Offset: 0x00003BD7
		public override void RemoveOutputCacheItem(string path)
		{
			HttpResponse.RemoveOutputCacheItem(path);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000059DF File Offset: 0x00003BDF
		public override void RemoveOutputCacheItem(string path, string providerName)
		{
			HttpResponse.RemoveOutputCacheItem(path, providerName);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000059E8 File Offset: 0x00003BE8
		public override void SetCookie(HttpCookie cookie)
		{
			this._httpResponse.SetCookie(cookie);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000059F6 File Offset: 0x00003BF6
		public override void TransmitFile(string filename)
		{
			this._httpResponse.TransmitFile(filename);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00005A04 File Offset: 0x00003C04
		public override void TransmitFile(string filename, long offset, long length)
		{
			this._httpResponse.TransmitFile(filename, offset, length);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00005A14 File Offset: 0x00003C14
		public override void Write(string s)
		{
			this._httpResponse.Write(s);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00005A22 File Offset: 0x00003C22
		public override void Write(char ch)
		{
			this._httpResponse.Write(ch);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00005A30 File Offset: 0x00003C30
		public override void Write(char[] buffer, int index, int count)
		{
			this._httpResponse.Write(buffer, index, count);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00005A40 File Offset: 0x00003C40
		public override void Write(object obj)
		{
			this._httpResponse.Write(obj);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00005A4E File Offset: 0x00003C4E
		public override void WriteFile(string filename)
		{
			this._httpResponse.WriteFile(filename);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00005A5C File Offset: 0x00003C5C
		public override void WriteFile(string filename, bool readIntoMemory)
		{
			this._httpResponse.WriteFile(filename, readIntoMemory);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00005A6B File Offset: 0x00003C6B
		public override void WriteFile(string filename, long offset, long size)
		{
			this._httpResponse.WriteFile(filename, offset, size);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00005A7B File Offset: 0x00003C7B
		public override void WriteFile(IntPtr fileHandle, long offset, long size)
		{
			this._httpResponse.WriteFile(fileHandle, offset, size);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00005A8B File Offset: 0x00003C8B
		public override void WriteSubstitution(HttpResponseSubstitutionCallback callback)
		{
			this._httpResponse.WriteSubstitution(callback);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00005A99 File Offset: 0x00003C99
		public override void PushPromise(string path)
		{
			this._httpResponse.PushPromise(path);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00005AA7 File Offset: 0x00003CA7
		public override void PushPromise(string path, string method, NameValueCollection headers)
		{
			this._httpResponse.PushPromise(path, method, headers);
		}

		// Token: 0x0400010E RID: 270
		private HttpResponse _httpResponse;
	}
}
