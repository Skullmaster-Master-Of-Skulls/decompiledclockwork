using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D6 RID: 1238
	public sealed class Documentation : MetadataItem
	{
		// Token: 0x06002D9A RID: 11674 RVA: 0x000DC5C5 File Offset: 0x000DA7C5
		internal Documentation()
		{
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000DC5E3 File Offset: 0x000DA7E3
		public Documentation(string summary, string longDescription)
		{
			this.Summary = summary;
			this.LongDescription = longDescription;
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x000DC60F File Offset: 0x000DA80F
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.Documentation;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x000DC613 File Offset: 0x000DA813
		// (set) Token: 0x06002D9E RID: 11678 RVA: 0x000DC61B File Offset: 0x000DA81B
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

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000DC633 File Offset: 0x000DA833
		// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x000DC63B File Offset: 0x000DA83B
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

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000DC653 File Offset: 0x000DA853
		internal override string Identity
		{
			get
			{
				return "Documentation";
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000DC65A File Offset: 0x000DA85A
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this._summary) && string.IsNullOrEmpty(this._longDescription);
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000DC679 File Offset: 0x000DA879
		public override string ToString()
		{
			return this._summary;
		}

		// Token: 0x040010E1 RID: 4321
		private string _summary = "";

		// Token: 0x040010E2 RID: 4322
		private string _longDescription = "";
	}
}
