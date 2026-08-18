using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000099 RID: 153
	public class XmlUrlResolver : XmlResolver
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00014150 File Offset: 0x00012350
		private static XmlDownloadManager DownloadManager
		{
			get
			{
				if (XmlUrlResolver.s_DownloadManager == null)
				{
					object value = new XmlDownloadManager();
					Interlocked.CompareExchange<object>(ref XmlUrlResolver.s_DownloadManager, value, null);
				}
				return (XmlDownloadManager)XmlUrlResolver.s_DownloadManager;
			}
		}

		// Token: 0x1700010B RID: 267
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00014189 File Offset: 0x00012389
		public override ICredentials Credentials
		{
			set
			{
				this._credentials = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00014192 File Offset: 0x00012392
		public IWebProxy Proxy
		{
			set
			{
				this._proxy = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0001419B File Offset: 0x0001239B
		public RequestCachePolicy CachePolicy
		{
			set
			{
				this._cachePolicy = value;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000141A4 File Offset: 0x000123A4
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
			{
				return XmlUrlResolver.DownloadManager.GetStream(absoluteUri, this._credentials, this._proxy, this._cachePolicy);
			}
			throw new XmlException("Xml_UnsupportedClass", string.Empty);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001420B File Offset: 0x0001240B
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			return base.ResolveUri(baseUri, relativeUri);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00014218 File Offset: 0x00012418
		public override Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			XmlUrlResolver.<GetEntityAsync>d__15 <GetEntityAsync>d__;
			<GetEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<GetEntityAsync>d__.<>4__this = this;
			<GetEntityAsync>d__.absoluteUri = absoluteUri;
			<GetEntityAsync>d__.ofObjectToReturn = ofObjectToReturn;
			<GetEntityAsync>d__.<>1__state = -1;
			<GetEntityAsync>d__.<>t__builder.Start<XmlUrlResolver.<GetEntityAsync>d__15>(ref <GetEntityAsync>d__);
			return <GetEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000247 RID: 583
		private static object s_DownloadManager;

		// Token: 0x04000248 RID: 584
		private ICredentials _credentials;

		// Token: 0x04000249 RID: 585
		private IWebProxy _proxy;

		// Token: 0x0400024A RID: 586
		private RequestCachePolicy _cachePolicy;
	}
}
