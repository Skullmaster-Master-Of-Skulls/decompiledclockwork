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
	// Token: 0x02000030 RID: 48
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpResponseBase
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool Buffer
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool BufferOutput
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCachePolicyBase Cache
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string CacheControl
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Charset
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual CancellationToken ClientDisconnectedToken
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Encoding ContentEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string ContentType
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCookieCollection Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int Expires
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual DateTime ExpiresAbsolute
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Stream Filter
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection Headers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool HeadersWritten
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Encoding HeaderEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsClientConnected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsRequestBeingRedirected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual TextWriter Output
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Stream OutputStream
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string RedirectLocation
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Status
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int StatusCode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string StatusDescription
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000389 RID: 905 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int SubStatusCode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsAsyncFlush
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SuppressContent
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SuppressDefaultCacheControlHeader
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SuppressFormsAuthenticationRedirect
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool TrySkipIisCustomErrors
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddCacheItemDependency(string cacheKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddCacheItemDependencies(ArrayList cacheKeys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddCacheItemDependencies(string[] cacheKeys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddCacheDependency(params CacheDependency[] dependencies)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddFileDependency(string filename)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddFileDependencies(ArrayList filenames)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddFileDependencies(string[] filenames)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddHeader(string name, string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ISubscriptionToken AddOnSendingHeaders(Action<HttpContextBase> callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AppendCookie(HttpCookie cookie)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AppendHeader(string name, string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AppendToLog(string param)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string ApplyAppPathModifier(string virtualPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void BinaryWrite(byte[] buffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void ClearContent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void ClearHeaders()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Close()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void DisableKernelCache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void DisableUserCache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void End()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void EndFlush(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Task FlushAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Pics(string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Redirect(string url)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Redirect(string url, bool endResponse)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoute(object routeValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoute(string routeName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoute(RouteValueDictionary routeValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoute(string routeName, object routeValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoutePermanent(object routeValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoutePermanent(string routeName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoutePermanent(RouteValueDictionary routeValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoutePermanent(string routeName, object routeValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectPermanent(string url)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RedirectPermanent(string url, bool endResponse)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RemoveOutputCacheItem(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RemoveOutputCacheItem(string path, string providerName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetCookie(HttpCookie cookie)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void TransmitFile(string filename)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void TransmitFile(string filename, long offset, long length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Write(char ch)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Write(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Write(object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Write(string s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void WriteFile(string filename)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void WriteFile(string filename, bool readIntoMemory)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void WriteFile(string filename, long offset, long size)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void WriteFile(IntPtr fileHandle, long offset, long size)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void WriteSubstitution(HttpResponseSubstitutionCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void PushPromise(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void PushPromise(string path, string method, NameValueCollection headers)
		{
			throw new NotImplementedException();
		}
	}
}
