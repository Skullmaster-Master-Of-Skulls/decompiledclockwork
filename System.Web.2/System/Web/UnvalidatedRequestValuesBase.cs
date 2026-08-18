using System;
using System.Collections.Specialized;

namespace System.Web
{
	// Token: 0x02000050 RID: 80
	public abstract class UnvalidatedRequestValuesBase
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection Form
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection QueryString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection Headers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCookieCollection Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpFileCollectionBase Files
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string RawUrl
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Path
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PathInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700027F RID: 639
		public virtual string this[string field]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Uri Url
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
