using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x02000134 RID: 308
	public class CachedDataAnnotationsMetadataAttributes
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x00019B10 File Offset: 0x00017D10
		public CachedDataAnnotationsMetadataAttributes(IEnumerable<Attribute> attributes)
		{
			this.Display = attributes.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
			this.DisplayFormat = attributes.OfType<DisplayFormatAttribute>().FirstOrDefault<DisplayFormatAttribute>();
			this.DisplayName = attributes.OfType<DisplayNameAttribute>().FirstOrDefault<DisplayNameAttribute>();
			this.Editable = attributes.OfType<EditableAttribute>().FirstOrDefault<EditableAttribute>();
			this.ReadOnly = attributes.OfType<ReadOnlyAttribute>().FirstOrDefault<ReadOnlyAttribute>();
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00019B78 File Offset: 0x00017D78
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00019B80 File Offset: 0x00017D80
		public DisplayAttribute Display { get; protected set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00019B89 File Offset: 0x00017D89
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00019B91 File Offset: 0x00017D91
		public DisplayNameAttribute DisplayName { get; protected set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00019B9A File Offset: 0x00017D9A
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00019BA2 File Offset: 0x00017DA2
		public DisplayFormatAttribute DisplayFormat { get; protected set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00019BAB File Offset: 0x00017DAB
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00019BB3 File Offset: 0x00017DB3
		public EditableAttribute Editable { get; protected set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00019BBC File Offset: 0x00017DBC
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00019BC4 File Offset: 0x00017DC4
		public ReadOnlyAttribute ReadOnly { get; protected set; }
	}
}
