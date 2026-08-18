using System;
using System.Configuration;
using System.Globalization;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006DB RID: 1755
	public sealed class FolderLevelBuildProvider : ConfigurationElement
	{
		// Token: 0x06005469 RID: 21609 RVA: 0x00127C60 File Offset: 0x00125E60
		static FolderLevelBuildProvider()
		{
			FolderLevelBuildProvider._properties = new ConfigurationPropertyCollection();
			FolderLevelBuildProvider._properties.Add(FolderLevelBuildProvider._propName);
			FolderLevelBuildProvider._properties.Add(FolderLevelBuildProvider._propType);
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x00127CD8 File Offset: 0x00125ED8
		public FolderLevelBuildProvider(string name, string type) : this()
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x00117E9E File Offset: 0x0011609E
		internal FolderLevelBuildProvider()
		{
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x0600546C RID: 21612 RVA: 0x00127CEE File Offset: 0x00125EEE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FolderLevelBuildProvider._properties;
			}
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x00127CF8 File Offset: 0x00125EF8
		public override bool Equals(object provider)
		{
			FolderLevelBuildProvider folderLevelBuildProvider = provider as FolderLevelBuildProvider;
			return folderLevelBuildProvider != null && StringUtil.EqualsIgnoreCase(this.Name, folderLevelBuildProvider.Name) && this.Type == folderLevelBuildProvider.Type;
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x00127D35 File Offset: 0x00125F35
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(StringUtil.GetNonRandomizedHashCode(this.Name.ToLower(CultureInfo.InvariantCulture), false), this.Type.GetHashCode());
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x00127D5D File Offset: 0x00125F5D
		// (set) Token: 0x06005470 RID: 21616 RVA: 0x00127D6F File Offset: 0x00125F6F
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[FolderLevelBuildProvider._propName];
			}
			set
			{
				base[FolderLevelBuildProvider._propName] = value;
			}
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06005471 RID: 21617 RVA: 0x00127D7D File Offset: 0x00125F7D
		// (set) Token: 0x06005472 RID: 21618 RVA: 0x00127D8F File Offset: 0x00125F8F
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Type
		{
			get
			{
				return (string)base[FolderLevelBuildProvider._propType];
			}
			set
			{
				base[FolderLevelBuildProvider._propType] = value;
			}
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06005473 RID: 21619 RVA: 0x00127DA0 File Offset: 0x00125FA0
		internal Type TypeInternal
		{
			get
			{
				if (this._type == null)
				{
					lock (this)
					{
						if (this._type == null)
						{
							this._type = CompilationUtil.LoadTypeWithChecks(this.Type, typeof(BuildProvider), null, this, "type");
						}
					}
				}
				return this._type;
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06005474 RID: 21620 RVA: 0x00127E1C File Offset: 0x0012601C
		internal FolderLevelBuildProviderAppliesTo AppliesToInternal
		{
			get
			{
				if (this._appliesToInternal != FolderLevelBuildProviderAppliesTo.None)
				{
					return this._appliesToInternal;
				}
				object[] customAttributes = this.TypeInternal.GetCustomAttributes(typeof(FolderLevelBuildProviderAppliesToAttribute), true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					this._appliesToInternal = ((FolderLevelBuildProviderAppliesToAttribute)customAttributes[0]).AppliesTo;
				}
				else
				{
					this._appliesToInternal = FolderLevelBuildProviderAppliesTo.None;
				}
				return this._appliesToInternal;
			}
		}

		// Token: 0x04002C51 RID: 11345
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C52 RID: 11346
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002C53 RID: 11347
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04002C54 RID: 11348
		private Type _type;

		// Token: 0x04002C55 RID: 11349
		private FolderLevelBuildProviderAppliesTo _appliesToInternal;
	}
}
