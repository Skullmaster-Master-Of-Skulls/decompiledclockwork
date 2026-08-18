using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;

namespace System.Data.EntityClient
{
	// Token: 0x02000120 RID: 288
	public sealed class EntityConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06000F63 RID: 3939 RVA: 0x000412C9 File Offset: 0x0003F4C9
		public EntityConnectionStringBuilder()
		{
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x000412D1 File Offset: 0x0003F4D1
		public EntityConnectionStringBuilder(string connectionString)
		{
			base.ConnectionString = connectionString;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x000412E0 File Offset: 0x0003F4E0
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x000412F1 File Offset: 0x0003F4F1
		[DisplayName("Name")]
		[EntityResCategory("EntityDataCategory_NamedConnectionString")]
		[EntityResDescription("EntityConnectionString_Name")]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00041306 File Offset: 0x0003F506
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x00041317 File Offset: 0x0003F517
		[DisplayName("Provider")]
		[EntityResCategory("EntityDataCategory_Source")]
		[EntityResDescription("EntityConnectionString_Provider")]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0004132C File Offset: 0x0003F52C
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x0004133D File Offset: 0x0003F53D
		[DisplayName("Metadata")]
		[EntityResCategory("EntityDataCategory_Context")]
		[EntityResDescription("EntityConnectionString_Metadata")]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00041352 File Offset: 0x0003F552
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x00041363 File Offset: 0x0003F563
		[DisplayName("Provider Connection String")]
		[EntityResCategory("EntityDataCategory_Source")]
		[EntityResDescription("EntityConnectionString_ProviderConnectionString")]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00041378 File Offset: 0x0003F578
		public override ICollection Keys
		{
			get
			{
				return new ReadOnlyCollection<string>(EntityConnectionStringBuilder.s_validKeywords);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00041384 File Offset: 0x0003F584
		internal static Hashtable Synonyms
		{
			get
			{
				if (EntityConnectionStringBuilder.s_synonyms == null)
				{
					Hashtable hashtable = new Hashtable(EntityConnectionStringBuilder.s_validKeywords.Length);
					foreach (string text in EntityConnectionStringBuilder.s_validKeywords)
					{
						hashtable.Add(text, text);
					}
					EntityConnectionStringBuilder.s_synonyms = hashtable;
				}
				return EntityConnectionStringBuilder.s_synonyms;
			}
		}

		// Token: 0x170001F0 RID: 496
		public override object this[string keyword]
		{
			get
			{
				EntityUtil.CheckArgumentNull<string>(keyword, "keyword");
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
				throw EntityUtil.KeywordNotSupported(keyword);
			}
			set
			{
				EntityUtil.CheckArgumentNull<string>(keyword, "keyword");
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				string text = value as string;
				if (text == null)
				{
					throw EntityUtil.Argument(Strings.EntityClient_ValueNotString, "value");
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
				throw EntityUtil.KeywordNotSupported(keyword);
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000414E1 File Offset: 0x0003F6E1
		public override void Clear()
		{
			base.Clear();
			this._namedConnectionName = null;
			this._providerName = null;
			this._metadataLocations = null;
			this._storeProviderConnectionString = null;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00041508 File Offset: 0x0003F708
		public override bool ContainsKey(string keyword)
		{
			EntityUtil.CheckArgumentNull<string>(keyword, "keyword");
			foreach (string text in EntityConnectionStringBuilder.s_validKeywords)
			{
				if (text.Equals(keyword, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00041546 File Offset: 0x0003F746
		public override bool TryGetValue(string keyword, out object value)
		{
			EntityUtil.CheckArgumentNull<string>(keyword, "keyword");
			if (this.ContainsKey(keyword))
			{
				value = this[keyword];
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0004156C File Offset: 0x0003F76C
		public override bool Remove(string keyword)
		{
			EntityUtil.CheckArgumentNull<string>(keyword, "keyword");
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

		// Token: 0x04000A1E RID: 2590
		internal const string NameParameterName = "name";

		// Token: 0x04000A1F RID: 2591
		internal const string MetadataParameterName = "metadata";

		// Token: 0x04000A20 RID: 2592
		internal const string ProviderParameterName = "provider";

		// Token: 0x04000A21 RID: 2593
		internal const string ProviderConnectionStringParameterName = "provider connection string";

		// Token: 0x04000A22 RID: 2594
		private static readonly string[] s_validKeywords = new string[]
		{
			"name",
			"metadata",
			"provider",
			"provider connection string"
		};

		// Token: 0x04000A23 RID: 2595
		private static Hashtable s_synonyms;

		// Token: 0x04000A24 RID: 2596
		private string _namedConnectionName;

		// Token: 0x04000A25 RID: 2597
		private string _providerName;

		// Token: 0x04000A26 RID: 2598
		private string _metadataLocations;

		// Token: 0x04000A27 RID: 2599
		private string _storeProviderConnectionString;
	}
}
