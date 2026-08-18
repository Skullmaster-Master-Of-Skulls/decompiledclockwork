using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace System.ComponentModel.Design
{
	// Token: 0x0200019C RID: 412
	public abstract class DesignerActionItem
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x00057630 File Offset: 0x00055830
		public DesignerActionItem(string displayName, string category, string description)
		{
			this.category = category;
			this.description = description;
			this.displayName = ((displayName == null) ? null : Regex.Replace(displayName, "\\(\\&.\\)", ""));
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00057669 File Offset: 0x00055869
		internal DesignerActionItem()
		{
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00057678 File Offset: 0x00055878
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x00057680 File Offset: 0x00055880
		public bool AllowAssociate
		{
			get
			{
				return this.allowAssociate;
			}
			set
			{
				this.allowAssociate = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00057689 File Offset: 0x00055889
		public virtual string Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00057691 File Offset: 0x00055891
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00057699 File Offset: 0x00055899
		public virtual string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x000576A1 File Offset: 0x000558A1
		public IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new HybridDictionary();
				}
				return this.properties;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x000576BC File Offset: 0x000558BC
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x000576C4 File Offset: 0x000558C4
		public bool ShowInSourceView
		{
			get
			{
				return this.showInSourceView;
			}
			set
			{
				this.showInSourceView = value;
			}
		}

		// Token: 0x040008E3 RID: 2275
		private bool allowAssociate;

		// Token: 0x040008E4 RID: 2276
		private string displayName;

		// Token: 0x040008E5 RID: 2277
		private string description;

		// Token: 0x040008E6 RID: 2278
		private string category;

		// Token: 0x040008E7 RID: 2279
		private IDictionary properties;

		// Token: 0x040008E8 RID: 2280
		private bool showInSourceView = true;
	}
}
