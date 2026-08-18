using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x02000208 RID: 520
	public abstract class DbProviderManifest
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060012C7 RID: 4807
		public abstract string NamespaceName { get; }

		// Token: 0x060012C8 RID: 4808
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public abstract ReadOnlyCollection<PrimitiveType> GetStoreTypes();

		// Token: 0x060012C9 RID: 4809
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public abstract ReadOnlyCollection<EdmFunction> GetStoreFunctions();

		// Token: 0x060012CA RID: 4810
		public abstract ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType edmType);

		// Token: 0x060012CB RID: 4811
		public abstract TypeUsage GetEdmType(TypeUsage storeType);

		// Token: 0x060012CC RID: 4812
		public abstract TypeUsage GetStoreType(TypeUsage edmType);

		// Token: 0x060012CD RID: 4813
		protected abstract XmlReader GetDbInformation(string informationType);

		// Token: 0x060012CE RID: 4814 RVA: 0x0004ED18 File Offset: 0x0004CF18
		public XmlReader GetInformation(string informationType)
		{
			XmlReader xmlReader = null;
			try
			{
				xmlReader = this.GetDbInformation(informationType);
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.EntityClient_FailedToGetInformation(informationType), ex);
				}
				throw;
			}
			if (xmlReader != null)
			{
				return xmlReader;
			}
			if (informationType == "ConceptualSchemaDefinitionVersion3" || informationType == "ConceptualSchemaDefinition")
			{
				return DbProviderServices.GetConceptualSchemaDefinition(informationType);
			}
			throw new ProviderIncompatibleException(Strings.ProviderReturnedNullForGetDbInformation(informationType));
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0004ED8C File Offset: 0x0004CF8C
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "0#")]
		public virtual bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '\0';
			return false;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0004ED92 File Offset: 0x0004CF92
		public virtual string EscapeLikeArgument(string argument)
		{
			Check.NotNull<string>(argument, "argument");
			throw new ProviderIncompatibleException(Strings.ProviderShouldOverrideEscapeLikeArgument);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0004EDAA File Offset: 0x0004CFAA
		public virtual bool SupportsInExpression()
		{
			return false;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0004EDAD File Offset: 0x0004CFAD
		public virtual bool SupportsIntersectAndUnionAllFlattening()
		{
			return false;
		}

		// Token: 0x04000577 RID: 1399
		public const string StoreSchemaDefinition = "StoreSchemaDefinition";

		// Token: 0x04000578 RID: 1400
		public const string StoreSchemaMapping = "StoreSchemaMapping";

		// Token: 0x04000579 RID: 1401
		public const string ConceptualSchemaDefinition = "ConceptualSchemaDefinition";

		// Token: 0x0400057A RID: 1402
		public const string StoreSchemaDefinitionVersion3 = "StoreSchemaDefinitionVersion3";

		// Token: 0x0400057B RID: 1403
		public const string StoreSchemaMappingVersion3 = "StoreSchemaMappingVersion3";

		// Token: 0x0400057C RID: 1404
		public const string ConceptualSchemaDefinitionVersion3 = "ConceptualSchemaDefinitionVersion3";

		// Token: 0x0400057D RID: 1405
		public const string MaxLengthFacetName = "MaxLength";

		// Token: 0x0400057E RID: 1406
		public const string UnicodeFacetName = "Unicode";

		// Token: 0x0400057F RID: 1407
		public const string FixedLengthFacetName = "FixedLength";

		// Token: 0x04000580 RID: 1408
		public const string PrecisionFacetName = "Precision";

		// Token: 0x04000581 RID: 1409
		public const string ScaleFacetName = "Scale";

		// Token: 0x04000582 RID: 1410
		public const string NullableFacetName = "Nullable";

		// Token: 0x04000583 RID: 1411
		public const string DefaultValueFacetName = "DefaultValue";

		// Token: 0x04000584 RID: 1412
		public const string CollationFacetName = "Collation";

		// Token: 0x04000585 RID: 1413
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Srid")]
		public const string SridFacetName = "SRID";

		// Token: 0x04000586 RID: 1414
		public const string IsStrictFacetName = "IsStrict";
	}
}
