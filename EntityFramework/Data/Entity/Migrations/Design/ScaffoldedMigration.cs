using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020006D5 RID: 1749
	[Serializable]
	public class ScaffoldedMigration
	{
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x0014C532 File Offset: 0x0014A732
		// (set) Token: 0x0600462D RID: 17965 RVA: 0x0014C53A File Offset: 0x0014A73A
		public string MigrationId
		{
			get
			{
				return this._migrationId;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._migrationId = value;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x0014C54F File Offset: 0x0014A74F
		// (set) Token: 0x0600462F RID: 17967 RVA: 0x0014C557 File Offset: 0x0014A757
		public string UserCode
		{
			get
			{
				return this._userCode;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._userCode = value;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06004630 RID: 17968 RVA: 0x0014C56C File Offset: 0x0014A76C
		// (set) Token: 0x06004631 RID: 17969 RVA: 0x0014C574 File Offset: 0x0014A774
		public string DesignerCode
		{
			get
			{
				return this._designerCode;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._designerCode = value;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x0014C589 File Offset: 0x0014A789
		// (set) Token: 0x06004633 RID: 17971 RVA: 0x0014C591 File Offset: 0x0014A791
		public string Language
		{
			get
			{
				return this._language;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._language = value;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x0014C5A6 File Offset: 0x0014A7A6
		// (set) Token: 0x06004635 RID: 17973 RVA: 0x0014C5AE File Offset: 0x0014A7AE
		public string Directory
		{
			get
			{
				return this._directory;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._directory = value;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x0014C5C3 File Offset: 0x0014A7C3
		public IDictionary<string, object> Resources
		{
			get
			{
				return this._resources;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x0014C5CB File Offset: 0x0014A7CB
		// (set) Token: 0x06004638 RID: 17976 RVA: 0x0014C5D3 File Offset: 0x0014A7D3
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rescaffold")]
		public bool IsRescaffold { get; set; }

		// Token: 0x040019B9 RID: 6585
		private string _migrationId;

		// Token: 0x040019BA RID: 6586
		private string _userCode;

		// Token: 0x040019BB RID: 6587
		private string _designerCode;

		// Token: 0x040019BC RID: 6588
		private string _language;

		// Token: 0x040019BD RID: 6589
		private string _directory;

		// Token: 0x040019BE RID: 6590
		private readonly Dictionary<string, object> _resources = new Dictionary<string, object>();
	}
}
