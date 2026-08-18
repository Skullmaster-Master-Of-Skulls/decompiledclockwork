using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x0200033B RID: 827
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "EntityConnectionStringBuilder follows the naming convention of DbConnectionStringBuilder.")]
	[SuppressMessage("Microsoft.Design", "CA1035:ICollectionImplementationsHaveStronglyTypedMembers", Justification = "There is no applicable strongly-typed implementation of CopyTo.")]
	public sealed class EntityConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06001D26 RID: 7462 RVA: 0x0008DEA2 File Offset: 0x0008C0A2
		public EntityConnectionStringBuilder()
		{
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0008DEAA File Offset: 0x0008C0AA
		public EntityConnectionStringBuilder(string connectionString)
		{
			base.ConnectionString = connectionString;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0008DEB9 File Offset: 0x0008C0B9
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x0008DECA File Offset: 0x0008C0CA
		[EntityResDescription("EntityConnectionString_Name")]
		[RefreshProperties(RefreshProperties.All)]
		[EntityResCategory("EntityDataCategory_NamedConnectionString")]
		[DisplayName("Name")]
		public string Name
		{
			get
			{
				return this._namedConnectionName ?? "";
			}
			set
			{
				this._namedConnectionName = value;
				base["name"] = value;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x0008DEDF File Offset: 0x0008C0DF
		// (set) Token: 0x06001D2B RID: 7467 RVA: 0x0008DEF0 File Offset: 0x0008C0F0
		[DisplayName("Provider")]
		[RefreshProperties(RefreshProperties.All)]
		[EntityResCategory("EntityDataCategory_Source")]
		[EntityResDescription("EntityConnectionString_Provider")]
		public string Provider
		{
			get
			{
				return this._providerName ?? "";
			}
			set
			{
				this._providerName = value;
				base["provider"] = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x0008DF05 File Offset: 0x0008C105
		// (set) Token: 0x06001D2D RID: 7469 RVA: 0x0008DF16 File Offset: 0x0008C116
		[EntityResCategory("EntityDataCategory_Context")]
		[RefreshProperties(RefreshProperties.All)]
		[EntityResDescription("EntityConnectionString_Metadata")]
		[DisplayName("Metadata")]
		public string Metadata
		{
			get
			{
				return this._metadataLocations ?? "";
			}
			set
			{
				this._metadataLocations = value;
				base["metadata"] = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0008DF2B File Offset: 0x0008C12B
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x0008DF3C File Offset: 0x0008C13C
		[EntityResDescription("EntityConnectionString_ProviderConnectionString")]
		[RefreshProperties(RefreshProperties.All)]
		[EntityResCategory("EntityDataCategory_Source")]
		[DisplayName("Provider Connection String")]
		public string ProviderConnectionString
		{
			get
			{
				return this._storeProviderConnectionString ?? "";
			}
			set
			{
				this._storeProviderConnectionString = value;
				base["provider connection string"] = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0008DF51 File Offset: 0x0008C151
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0008DF54 File Offset: 0x0008C154
		public override ICollection Keys
		{
			get
			{
				return new ReadOnlyCollection<string>(EntityConnectionStringBuilder.ValidKeywords);
			}
		}

		// Token: 0x17000344 RID: 836
		public override object this[string keyword]
		{
			get
			{
				Check.NotNull<string>(keyword, "keyword");
				if (string.Compare(keyword, "metadata", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this.Metadata;
				}
				if (string.Compare(keyword, "provider connection string", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this.ProviderConnectionString;
				}
				if (string.Compare(keyword, "name", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this.Name;
				}
				if (string.Compare(keyword, "provider", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this.Provider;
				}
				throw new ArgumentException(Strings.EntityClient_KeywordNotSupported(keyword));
			}
			set
			{
				Check.NotNull<string>(keyword, "keyword");
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				string text = value as string;
				if (text == null)
				{
					throw new ArgumentException(Strings.EntityClient_ValueNotString, "value");
				}
				if (string.Compare(keyword, "metadata", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Metadata = text;
					return;
				}
				if (string.Compare(keyword, "provider connection string", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.ProviderConnectionString = text;
					return;
				}
				if (string.Compare(keyword, "name", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Name = text;
					return;
				}
				if (string.Compare(keyword, "provider", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Provider = text;
					return;
				}
				throw new ArgumentException(Strings.EntityClient_KeywordNotSupported(keyword));
			}
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0008E07A File Offset: 0x0008C27A
		public override void Clear()
		{
			base.Clear();
			this._namedConnectionName = null;
			this._providerName = null;
			this._metadataLocations = null;
			this._storeProviderConnectionString = null;
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0008E0A0 File Offset: 0x0008C2A0
		public override bool ContainsKey(string keyword)
		{
			Check.NotNull<string>(keyword, "keyword");
			foreach (string text in EntityConnectionStringBuilder.ValidKeywords)
			{
				if (text.Equals(keyword, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0008E0E2 File Offset: 0x0008C2E2
		public override bool TryGetValue(string keyword, out object value)
		{
			Check.NotNull<string>(keyword, "keyword");
			if (this.ContainsKey(keyword))
			{
				value = this[keyword];
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0008E108 File Offset: 0x0008C308
		public override bool Remove(string keyword)
		{
			if (string.Compare(keyword, "metadata", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._metadataLocations = null;
			}
			else if (string.Compare(keyword, "provider connection string", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._storeProviderConnectionString = null;
			}
			else if (string.Compare(keyword, "name", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._namedConnectionName = null;
			}
			else if (string.Compare(keyword, "provider", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._providerName = null;
			}
			return base.Remove(keyword);
		}

		// Token: 0x040009FD RID: 2557
		internal const string NameParameterName = "name";

		// Token: 0x040009FE RID: 2558
		internal const string MetadataParameterName = "metadata";

		// Token: 0x040009FF RID: 2559
		internal const string ProviderParameterName = "provider";

		// Token: 0x04000A00 RID: 2560
		internal const string ProviderConnectionStringParameterName = "provider connection string";

		// Token: 0x04000A01 RID: 2561
		internal static readonly string[] ValidKeywords = new string[]
		{
			"name",
			"metadata",
			"provider",
			"provider connection string"
		};

		// Token: 0x04000A02 RID: 2562
		private string _namedConnectionName;

		// Token: 0x04000A03 RID: 2563
		private string _providerName;

		// Token: 0x04000A04 RID: 2564
		private string _metadataLocations;

		// Token: 0x04000A05 RID: 2565
		private string _storeProviderConnectionString;
	}
}
