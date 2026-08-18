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
	// Token: 0x0200002E RID: 46
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpRequestBase
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string[] AcceptTypes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string ApplicationPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string AnonymousID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string AppRelativeCurrentExecutionFilePath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpBrowserCapabilitiesBase Browser
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ChannelBinding HttpChannelBinding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpClientCertificate ClientCertificate
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00003ABB File Offset: 0x00001CBB
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int ContentLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00003ABB File Offset: 0x00001CBB
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCookieCollection Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string CurrentExecutionFilePath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string CurrentExecutionFilePathExtension
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string FilePath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpFileCollectionBase Files
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00003ABB File Offset: 0x00001CBB
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

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection Form
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string HttpMethod
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Stream InputStream
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsAuthenticated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsLocal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsSecureConnection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual WindowsIdentity LogonUserIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection Params
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Path
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PathInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PhysicalApplicationPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PhysicalPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string RawUrl
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ReadEntityBodyMode ReadEntityBodyMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual RequestContext RequestContext
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

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string RequestType
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection ServerVariables
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual CancellationToken TimedOutToken
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ITlsTokenBindingInfo TlsTokenBindingInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int TotalBytes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual UnvalidatedRequestValuesBase Unvalidated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Uri Url
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Uri UrlReferrer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UserAgent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string[] UserLanguages
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UserHostAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UserHostName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection Headers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection QueryString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700019B RID: 411
		public virtual string this[string key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Abort()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual byte[] BinaryRead(int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Stream GetBufferedInputStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Stream GetBufferlessInputStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Stream GetBufferlessInputStream(bool disableMaxRequestLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void InsertEntityBody()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void InsertEntityBody(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int[] MapImageCoordinates(string imageFieldName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual double[] MapRawImageCoordinates(string imageFieldName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string MapPath(string virtualPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void ValidateInput()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SaveAs(string filename, bool includeHeaders)
		{
			throw new NotImplementedException();
		}
	}
}
