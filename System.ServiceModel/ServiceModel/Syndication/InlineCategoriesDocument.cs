using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001A3 RID: 419
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class InlineCategoriesDocument : CategoriesDocument
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x0003111D File Offset: 0x0002F31D
		public InlineCategoriesDocument()
		{
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00031125 File Offset: 0x0002F325
		public InlineCategoriesDocument(IEnumerable<SyndicationCategory> categories) : this(categories, false, null)
		{
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00031130 File Offset: 0x0002F330
		public InlineCategoriesDocument(IEnumerable<SyndicationCategory> categories, bool isFixed, string scheme)
		{
			if (categories != null)
			{
				this.categories = new NullNotAllowedCollection<SyndicationCategory>();
				foreach (SyndicationCategory item in categories)
				{
					this.categories.Add(item);
				}
			}
			this.isFixed = isFixed;
			this.scheme = scheme;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000311A0 File Offset: 0x0002F3A0
		public Collection<SyndicationCategory> Categories
		{
			get
			{
				if (this.categories == null)
				{
					this.categories = new NullNotAllowedCollection<SyndicationCategory>();
				}
				return this.categories;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x000311BB File Offset: 0x0002F3BB
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x000311C3 File Offset: 0x0002F3C3
		public bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				this.isFixed = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000311CC File Offset: 0x0002F3CC
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x000311D4 File Offset: 0x0002F3D4
		public string Scheme
		{
			get
			{
				return this.scheme;
			}
			set
			{
				this.scheme = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x000311DD File Offset: 0x0002F3DD
		internal override bool IsInline
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000311E0 File Offset: 0x0002F3E0
		protected internal virtual SyndicationCategory CreateCategory()
		{
			return new SyndicationCategory();
		}

		// Token: 0x04001716 RID: 5910
		private Collection<SyndicationCategory> categories;

		// Token: 0x04001717 RID: 5911
		private bool isFixed;

		// Token: 0x04001718 RID: 5912
		private string scheme;
	}
}
