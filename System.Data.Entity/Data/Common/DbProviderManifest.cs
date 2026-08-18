using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000327 RID: 807
	public abstract class DbProviderManifest
	{
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002F7B RID: 12155
		public abstract string NamespaceName { get; }

		// Token: 0x06002F7C RID: 12156
		public abstract ReadOnlyCollection<PrimitiveType> GetStoreTypes();

		// Token: 0x06002F7D RID: 12157
		public abstract ReadOnlyCollection<EdmFunction> GetStoreFunctions();

		// Token: 0x06002F7E RID: 12158
		public abstract ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType edmType);

		// Token: 0x06002F7F RID: 12159
		public abstract TypeUsage GetEdmType(TypeUsage storeType);

		// Token: 0x06002F80 RID: 12160
		public abstract TypeUsage GetStoreType(TypeUsage edmType);

		// Token: 0x06002F81 RID: 12161
		protected abstract XmlReader GetDbInformation(string informationType);

		// Token: 0x06002F82 RID: 12162 RVA: 0x000B3B14 File Offset: 0x000B1D14
		public XmlReader GetInformation(string informationType)
		{
			XmlReader xmlReader = null;
			try
			{
				xmlReader = this.GetDbInformation(informationType);
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.ProviderIncompatible(Strings.EntityClient_FailedToGetInformation(informationType), ex);
				}
				throw;
			}
			if (xmlReader != null)
			{
				return xmlReader;
			}
			if (informationType == DbProviderManifest.ConceptualSchemaDefinitionVersion3 || informationType == DbProviderManifest.ConceptualSchemaDefinition)
			{
				return DbProviderServices.GetConceptualSchemaDefinition(informationType);
			}
			throw EntityUtil.ProviderIncompatible(Strings.ProviderReturnedNullForGetDbInformation(informationType));
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000B3B88 File Offset: 0x000B1D88
		public virtual bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '\0';
			return false;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000B3B8E File Offset: 0x000B1D8E
		public virtual string EscapeLikeArgument(string argument)
		{
			throw EntityUtil.ProviderIncompatible(Strings.ProviderShouldOverrideEscapeLikeArgument);
		}

		// Token: 0x04001465 RID: 5221
		public static readonly string StoreSchemaDefinition = "StoreSchemaDefinition";

		// Token: 0x04001466 RID: 5222
		public static readonly string StoreSchemaMapping = "StoreSchemaMapping";

		// Token: 0x04001467 RID: 5223
		public static readonly string ConceptualSchemaDefinition = "ConceptualSchemaDefinition";

		// Token: 0x04001468 RID: 5224
		public static readonly string StoreSchemaDefinitionVersion3 = "StoreSchemaDefinitionVersion3";

		// Token: 0x04001469 RID: 5225
		public static readonly string StoreSchemaMappingVersion3 = "StoreSchemaMappingVersion3";

		// Token: 0x0400146A RID: 5226
		public static readonly string ConceptualSchemaDefinitionVersion3 = "ConceptualSchemaDefinitionVersion3";

		// Token: 0x0400146B RID: 5227
		internal const string MaxLengthFacetName = "MaxLength";

		// Token: 0x0400146C RID: 5228
		internal const string UnicodeFacetName = "Unicode";

		// Token: 0x0400146D RID: 5229
		internal const string FixedLengthFacetName = "FixedLength";

		// Token: 0x0400146E RID: 5230
		internal const string PrecisionFacetName = "Precision";

		// Token: 0x0400146F RID: 5231
		internal const string ScaleFacetName = "Scale";

		// Token: 0x04001470 RID: 5232
		internal const string NullableFacetName = "Nullable";

		// Token: 0x04001471 RID: 5233
		internal const string DefaultValueFacetName = "DefaultValue";

		// Token: 0x04001472 RID: 5234
		internal const string CollationFacetName = "Collation";

		// Token: 0x04001473 RID: 5235
		internal const string SridFacetName = "SRID";

		// Token: 0x04001474 RID: 5236
		internal const string IsStrictFacetName = "IsStrict";
	}
}
