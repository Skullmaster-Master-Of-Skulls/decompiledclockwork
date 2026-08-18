using System;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001A4 RID: 420
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ReferencedCategoriesDocument : CategoriesDocument
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x000311E7 File Offset: 0x0002F3E7
		public ReferencedCategoriesDocument()
		{
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000311EF File Offset: 0x0002F3EF
		public ReferencedCategoriesDocument(Uri link)
		{
			if (link == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("link");
			}
			this.link = link;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00031217 File Offset: 0x0002F417
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x0003121F File Offset: 0x0002F41F
		public Uri Link
		{
			get
			{
				return this.link;
			}
			set
			{
				this.link = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00031228 File Offset: 0x0002F428
		internal override bool IsInline
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001719 RID: 5913
		private Uri link;
	}
}
