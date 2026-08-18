using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x0200020A RID: 522
	public abstract class DbXmlEnabledProviderManifest : DbProviderManifest
	{
		// Token: 0x06001308 RID: 4872 RVA: 0x0004F74C File Offset: 0x0004D94C
		protected DbXmlEnabledProviderManifest(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ProviderIncompatibleException(Strings.IncorrectProviderManifest, new ArgumentNullException("reader"));
			}
			this.Load(reader);
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0004F79F File Offset: 0x0004D99F
		public override string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0004F7A7 File Offset: 0x0004D9A7
		protected Dictionary<string, PrimitiveType> StoreTypeNameToEdmPrimitiveType
		{
			get
			{
				return this._storeTypeNameToEdmPrimitiveType;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0004F7AF File Offset: 0x0004D9AF
		protected Dictionary<string, PrimitiveType> StoreTypeNameToStorePrimitiveType
		{
			get
			{
				return this._storeTypeNameToStorePrimitiveType;
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0004F7B7 File Offset: 0x0004D9B7
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType edmType)
		{
			return DbXmlEnabledProviderManifest.GetReadOnlyCollection<FacetDescription>(edmType as PrimitiveType, this._facetDescriptions, Helper.EmptyFacetDescriptionEnumerable);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004F7CF File Offset: 0x0004D9CF
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			return this._primitiveTypes;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0004F7D7 File Offset: 0x0004D9D7
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			return this._functions;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0004F7E0 File Offset: 0x0004D9E0
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		private void Load(XmlReader reader)
		{
			Schema schema;
			IList<EdmSchemaError> list = SchemaManager.LoadProviderManifest(reader, (reader.BaseURI.Length > 0) ? reader.BaseURI : null, true, out schema);
			if (list.Count != 0)
			{
				throw new ProviderIncompatibleException(Strings.IncorrectProviderManifest + Helper.CombineErrorMessage(list));
			}
			this._namespaceName = schema.Namespace;
			List<PrimitiveType> list2 = new List<PrimitiveType>();
			foreach (SchemaType schemaType in schema.SchemaTypes)
			{
				TypeElement typeElement = schemaType as TypeElement;
				if (typeElement != null)
				{
					PrimitiveType primitiveType = typeElement.PrimitiveType;
					primitiveType.ProviderManifest = this;
					primitiveType.DataSpace = DataSpace.SSpace;
					primitiveType.SetReadOnly();
					list2.Add(primitiveType);
					this._storeTypeNameToStorePrimitiveType.Add(primitiveType.Name.ToLowerInvariant(), primitiveType);
					this._storeTypeNameToEdmPrimitiveType.Add(primitiveType.Name.ToLowerInvariant(), EdmProviderManifest.Instance.GetPrimitiveType(primitiveType.PrimitiveTypeKind));
					ReadOnlyCollection<FacetDescription> value;
					if (DbXmlEnabledProviderManifest.EnumerableToReadOnlyCollection<FacetDescription, FacetDescription>(typeElement.FacetDescriptions, out value))
					{
						this._facetDescriptions.Add(primitiveType, value);
					}
				}
			}
			this._primitiveTypes = new ReadOnlyCollection<PrimitiveType>(list2.ToArray());
			ItemCollection itemCollection = new DbXmlEnabledProviderManifest.EmptyItemCollection();
			IEnumerable<GlobalItem> enumerable = Converter.ConvertSchema(schema, this, itemCollection);
			if (!DbXmlEnabledProviderManifest.EnumerableToReadOnlyCollection<EdmFunction, GlobalItem>(enumerable, out this._functions))
			{
				this._functions = Helper.EmptyEdmFunctionReadOnlyCollection;
			}
			foreach (EdmFunction edmFunction in this._functions)
			{
				edmFunction.SetReadOnly();
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004F99C File Offset: 0x0004DB9C
		private static ReadOnlyCollection<T> GetReadOnlyCollection<T>(PrimitiveType type, Dictionary<PrimitiveType, ReadOnlyCollection<T>> typeDictionary, ReadOnlyCollection<T> useIfEmpty)
		{
			ReadOnlyCollection<T> result;
			if (typeDictionary.TryGetValue(type, out result))
			{
				return result;
			}
			return useIfEmpty;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
		private static bool EnumerableToReadOnlyCollection<Target, BaseType>(IEnumerable<BaseType> enumerable, out ReadOnlyCollection<Target> collection) where Target : BaseType
		{
			List<Target> list = new List<Target>();
			foreach (BaseType baseType in enumerable)
			{
				if (typeof(Target) == typeof(BaseType) || baseType is Target)
				{
					list.Add((Target)((object)baseType));
				}
			}
			if (list.Count != 0)
			{
				collection = new ReadOnlyCollection<Target>(list);
				return true;
			}
			collection = null;
			return false;
		}

		// Token: 0x0400058E RID: 1422
		private string _namespaceName;

		// Token: 0x0400058F RID: 1423
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04000590 RID: 1424
		private readonly Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> _facetDescriptions = new Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>();

		// Token: 0x04000591 RID: 1425
		private ReadOnlyCollection<EdmFunction> _functions;

		// Token: 0x04000592 RID: 1426
		private readonly Dictionary<string, PrimitiveType> _storeTypeNameToEdmPrimitiveType = new Dictionary<string, PrimitiveType>();

		// Token: 0x04000593 RID: 1427
		private readonly Dictionary<string, PrimitiveType> _storeTypeNameToStorePrimitiveType = new Dictionary<string, PrimitiveType>();

		// Token: 0x0200020E RID: 526
		private class EmptyItemCollection : ItemCollection
		{
			// Token: 0x06001342 RID: 4930 RVA: 0x00050170 File Offset: 0x0004E370
			public EmptyItemCollection() : base(DataSpace.SSpace)
			{
			}
		}
	}
}
