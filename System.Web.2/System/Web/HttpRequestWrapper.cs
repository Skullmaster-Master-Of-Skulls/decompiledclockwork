using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Routing;

namespace System.Web
{
	// Token: 0x0200002F RID: 47
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpRequestWrapper : HttpRequestBase
	{
		// Token: 0x06000321 RID: 801 RVA: 0x00005170 File Offset: 0x00003370
		public HttpRequestWrapper(HttpRequest httpRequest)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			this._httpRequest = httpRequest;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000518D File Offset: 0x0000338D
		public override HttpBrowserCapabilitiesBase Browser
		{
			get
			{
				return new HttpBrowserCapabilitiesWrapper(this._httpRequest.Browser);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000519F File Offset: 0x0000339F
		public override NameValueCollection Params
		{
			get
			{
				return this._httpRequest.Params;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000051AC File Offset: 0x000033AC
		public override string Path
		{
			get
			{
				return this._httpRequest.Path;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000051B9 File Offset: 0x000033B9
		public override string FilePath
		{
			get
			{
				return this._httpRequest.FilePath;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000051C6 File Offset: 0x000033C6
		public override NameValueCollection Headers
		{
			get
			{
				return this._httpRequest.Headers;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000051D3 File Offset: 0x000033D3
		public override NameValueCollection QueryString
		{
			get
			{
				return this._httpRequest.QueryString;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000051E0 File Offset: 0x000033E0
		public override string[] AcceptTypes
		{
			get
			{
				return this._httpRequest.AcceptTypes;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000051ED File Offset: 0x000033ED
		public override string ApplicationPath
		{
			get
			{
				return this._httpRequest.ApplicationPath;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000051FA File Offset: 0x000033FA
		public override string AnonymousID
		{
			get
			{
				return this._httpRequest.AnonymousID;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00005207 File Offset: 0x00003407
		public override string AppRelativeCurrentExecutionFilePath
		{
			get
			{
				return this._httpRequest.AppRelativeCurrentExecutionFilePath;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00005214 File Offset: 0x00003414
		public override ChannelBinding HttpChannelBinding
		{
			get
			{
				return this._httpRequest.HttpChannelBinding;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00005221 File Offset: 0x00003421
		public override HttpClientCertificate ClientCertificate
		{
			get
			{
				return this._httpRequest.ClientCertificate;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000522E File Offset: 0x0000342E
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000523B File Offset: 0x0000343B
		public override Encoding ContentEncoding
		{
			get
			{
				return this._httpRequest.ContentEncoding;
			}
			set
			{
				this._httpRequest.ContentEncoding = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00005249 File Offset: 0x00003449
		public override int ContentLength
		{
			get
			{
				return this._httpRequest.ContentLength;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00005256 File Offset: 0x00003456
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00005263 File Offset: 0x00003463
		public override string ContentType
		{
			get
			{
				return this._httpRequest.ContentType;
			}
			set
			{
				this._httpRequest.ContentType = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00005271 File Offset: 0x00003471
		public override HttpCookieCollection Cookies
		{
			get
			{
				return this._httpRequest.Cookies;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000527E File Offset: 0x0000347E
		public override string CurrentExecutionFilePath
		{
			get
			{
				return this._httpRequest.CurrentExecutionFilePath;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000528B File Offset: 0x0000348B
		public override string CurrentExecutionFilePathExtension
		{
			get
			{
				return this._httpRequest.CurrentExecutionFilePathExtension;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00005298 File Offset: 0x00003498
		public override HttpFileCollectionBase Files
		{
			get
			{
				return new HttpFileCollectionWrapper(this._httpRequest.Files);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000052AA File Offset: 0x000034AA
		// (set) Token: 0x06000338 RID: 824 RVA: 0x000052B7 File Offset: 0x000034B7
		public override Stream Filter
		{
			get
			{
				return this._httpRequest.Filter;
			}
			set
			{
				this._httpRequest.Filter = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000052C5 File Offset: 0x000034C5
		public override NameValueCollection Form
		{
			get
			{
				return this._httpRequest.Form;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600033A RID: 826 RVA: 0x000052D2 File Offset: 0x000034D2
		public override string HttpMethod
		{
			get
			{
				return this._httpRequest.HttpMethod;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000052DF File Offset: 0x000034DF
		public override Stream InputStream
		{
			get
			{
				return this._httpRequest.InputStream;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000052EC File Offset: 0x000034EC
		public override bool IsAuthenticated
		{
			get
			{
				return this._httpRequest.IsAuthenticated;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000052F9 File Offset: 0x000034F9
		public override bool IsLocal
		{
			get
			{
				return this._httpRequest.IsLocal;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00005306 File Offset: 0x00003506
		public override bool IsSecureConnection
		{
			get
			{
				return this._httpRequest.IsSecureConnection;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00005313 File Offset: 0x00003513
		public override WindowsIdentity LogonUserIdentity
		{
			get
			{
				return this._httpRequest.LogonUserIdentity;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00005320 File Offset: 0x00003520
		public override string PathInfo
		{
			get
			{
				return this._httpRequest.PathInfo;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000532D File Offset: 0x0000352D
		public override string PhysicalApplicationPath
		{
			get
			{
				return this._httpRequest.PhysicalApplicationPath;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000533A File Offset: 0x0000353A
		public override string PhysicalPath
		{
			get
			{
				return this._httpRequest.PhysicalPath;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00005347 File Offset: 0x00003547
		public override string RawUrl
		{
			get
			{
				return this._httpRequest.RawUrl;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00005354 File Offset: 0x00003554
		public override ReadEntityBodyMode ReadEntityBodyMode
		{
			get
			{
				return this._httpRequest.ReadEntityBodyMode;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00005361 File Offset: 0x00003561
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000536E File Offset: 0x0000356E
		public override RequestContext RequestContext
		{
			get
			{
				return this._httpRequest.RequestContext;
			}
			set
			{
				this._httpRequest.RequestContext = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000537C File Offset: 0x0000357C
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00005389 File Offset: 0x00003589
		public override string RequestType
		{
			get
			{
				return this._httpRequest.RequestType;
			}
			set
			{
				this._httpRequest.RequestType = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00005397 File Offset: 0x00003597
		public override NameValueCollection ServerVariables
		{
			get
			{
				return this._httpRequest.ServerVariables;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000053A4 File Offset: 0x000035A4
		public override CancellationToken TimedOutToken
		{
			get
			{
				return this._httpRequest.TimedOutToken;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000053B1 File Offset: 0x000035B1
		public override ITlsTokenBindingInfo TlsTokenBindingInfo
		{
			get
			{
				return this._httpRequest.TlsTokenBindingInfo;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000053BE File Offset: 0x000035BE
		public override int TotalBytes
		{
			get
			{
				return this._httpRequest.TotalBytes;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000053CB File Offset: 0x000035CB
		public override UnvalidatedRequestValuesBase Unvalidated
		{
			get
			{
				return new UnvalidatedRequestValuesWrapper(this._httpRequest.Unvalidated);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000053DD File Offset: 0x000035DD
		public override Uri Url
		{
			get
			{
				return this._httpRequest.Url;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000053EA File Offset: 0x000035EA
		public override Uri UrlReferrer
		{
			get
			{
				return this._httpRequest.UrlReferrer;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000053F7 File Offset: 0x000035F7
		public override string UserAgent
		{
			get
			{
				return this._httpRequest.UserAgent;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00005404 File Offset: 0x00003604
		public override string[] UserLanguages
		{
			get
			{
				return this._httpRequest.UserLanguages;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00005411 File Offset: 0x00003611
		public override string UserHostAddress
		{
			get
			{
				return this._httpRequest.UserHostAddress;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000541E File Offset: 0x0000361E
		public override string UserHostName
		{
			get
			{
				return this._httpRequest.UserHostName;
			}
		}

		// Token: 0x170001C9 RID: 457
		public override string this[string key]
		{
			get
			{
				return this._httpRequest[key];
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00005439 File Offset: 0x00003639
		public override void Abort()
		{
			this._httpRequest.Abort();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00005446 File Offset: 0x00003646
		public override byte[] BinaryRead(int count)
		{
			return this._httpRequest.BinaryRead(count);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00005454 File Offset: 0x00003654
		public override Stream GetBufferedInputStream()
		{
			return this._httpRequest.GetBufferedInputStream();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00005461 File Offset: 0x00003661
		public override Stream GetBufferlessInputStream()
		{
			return this._httpRequest.GetBufferlessInputStream();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000546E File Offset: 0x0000366E
		public override Stream GetBufferlessInputStream(bool disableMaxRequestLength)
		{
			return this._httpRequest.GetBufferlessInputStream(disableMaxRequestLength);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000547C File Offset: 0x0000367C
		public override void InsertEntityBody()
		{
			this._httpRequest.InsertEntityBody();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00005489 File Offset: 0x00003689
		public override void InsertEntityBody(byte[] buffer, int offset, int count)
		{
			this._httpRequest.InsertEntityBody(buffer, offset, count);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00005499 File Offset: 0x00003699
		public override int[] MapImageCoordinates(string imageFieldName)
		{
			return this._httpRequest.MapImageCoordinates(imageFieldName);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000054A7 File Offset: 0x000036A7
		public override double[] MapRawImageCoordinates(string imageFieldName)
		{
			return this._httpRequest.MapRawImageCoordinates(imageFieldName);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000054B5 File Offset: 0x000036B5
		public override string MapPath(string virtualPath)
		{
			return this._httpRequest.MapPath(virtualPath);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000054C3 File Offset: 0x000036C3
		public override string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
		{
			return this._httpRequest.MapPath(virtualPath, baseVirtualDir, allowCrossAppMapping);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000054D3 File Offset: 0x000036D3
		public override void ValidateInput()
		{
			this._httpRequest.ValidateInput();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000054E0 File Offset: 0x000036E0
		public override void SaveAs(string filename, bool includeHeaders)
		{
			this._httpRequest.SaveAs(filename, includeHeaders);
		}

		// Token: 0x0400010D RID: 269
		private HttpRequest _httpRequest;
	}
}
