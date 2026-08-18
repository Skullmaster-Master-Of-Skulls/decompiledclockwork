using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001CA RID: 458
	public sealed class Documentation : MetadataItem
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x0006E432 File Offset: 0x0006C632
		internal Documentation()
		{
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x0006E450 File Offset: 0x0006C650
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.Documentation;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0006E454 File Offset: 0x0006C654
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x0006E45C File Offset: 0x0006C65C
		public string Summary
		{
			get
			{
				return this._summary;
			}
			internal set
			{
				if (value != null)
				{
					this._summary = value;
					return;
				}
				this._summary = "";
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0006E474 File Offset: 0x0006C674
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x0006E47C File Offset: 0x0006C67C
		public string LongDescription
		{
			get
			{
				return this._longDescription;
			}
			internal set
			{
				if (value != null)
				{
					this._longDescription = value;
					return;
				}
				this._longDescription = "";
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0006E494 File Offset: 0x0006C694
		internal override string Identity
		{
			get
			{
				return "Documentation";
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x0006E49B File Offset: 0x0006C69B
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this._summary) && string.IsNullOrEmpty(this._longDescription);
			}
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0006E454 File Offset: 0x0006C654
		public override string ToString()
		{
			return this._summary;
		}

		// Token: 0x04000D4E RID: 3406
		private string _summary = "";

		// Token: 0x04000D4F RID: 3407
		private string _longDescription = "";
	}
}
