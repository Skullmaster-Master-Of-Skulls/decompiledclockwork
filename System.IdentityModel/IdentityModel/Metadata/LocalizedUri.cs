using System;
using System.Globalization;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000FC RID: 252
	public class LocalizedUri : LocalizedEntry
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x0001ABBD File Offset: 0x00018DBD
		public LocalizedUri() : this(null, null)
		{
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001ABC7 File Offset: 0x00018DC7
		public LocalizedUri(Uri uri, CultureInfo language) : base(language)
		{
			this.Uri = uri;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001ABD7 File Offset: 0x00018DD7
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001ABDF File Offset: 0x00018DDF
		public Uri Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x04000A7C RID: 2684
		private Uri _uri;
	}
}
