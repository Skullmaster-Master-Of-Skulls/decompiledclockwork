using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.EntityModel.SchemaObjectModel;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000328 RID: 808
	public abstract class DbXmlEnabledProviderManifest : DbProviderManifest
	{
		// Token: 0x06002F86 RID: 12166 RVA: 0x000B3BD8 File Offset: 0x000B1DD8
		protected DbXmlEnabledProviderManifest(XmlReader reader)
		{
			if (reader == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.IncorrectProviderManifest, new ArgumentNullException("reader"));
			}
			this.Load(reader);
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x000B3C2B File Offset: 0x000B1E2B
		public override string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000B3C33 File Offset: 0x000B1E33
		protected Dictionary<string, PrimitiveType> StoreTypeNameToEdmPrimitiveType
		{
			get
			{
				return this._storeTypeNameToEdmPrimitiveType;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000B3C3B File Offset: 0x000B1E3B
		protected Dictionary<string, PrimitiveType> StoreTypeNameToStorePrimitiveType
		{
			get
			{
				return this._storeTypeNameToStorePrimitiveType;
			}
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000B3C43 File Offset: 0x000B1E43
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType type)
		{
			return DbXmlEnabledProviderManifest.GetReadOnlyCollection<FacetDescription>(type as PrimitiveType, this._facetDescriptions, Helper.EmptyFacetDescriptionEnumerable);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000B3C5B File Offset: 0x000B1E5B
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			return this._primitiveTypes;
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000B3C63 File Offset: 0x000B1E63
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			return this._functions;
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000B3C6C File Offset: 0x000B1E6C
		private void Load(XmlReader reader)
		{
			Schema schema;
			IList<EdmSchemaError> list = SchemaManager.LoadProviderManifest(reader, (reader.BaseURI.Length > 0) ? reader.BaseURI : null, true, out schema);
			if (list.Count != 0)
			{
				throw EntityUtil.ProviderIncompatible(Strings.IncorrectProviderManifest + Helper.CombineErrorMessage(list));
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
			this._primitiveTypes = Array.AsReadOnly<PrimitiveType>(list2.ToArray());
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

		// Token: 0x06002F8E RID: 12174 RVA: 0x000B3E28 File Offset: 0x000B2028
		private static ReadOnlyCollection<T> GetReadOnlyCollection<T>(PrimitiveType type, Dictionary<PrimitiveType, ReadOnlyCollection<T>> typeDictionary, ReadOnlyCollection<T> useIfEmpty)
		{
			ReadOnlyCollection<T> result;
			if (typeDictionary.TryGetValue(type, out result))
			{
				return result;
			}
			return useIfEmpty;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000B3E44 File Offset: 0x000B2044
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
				collection = list.AsReadOnly();
				return true;
			}
			collection = null;
			return false;
		}

		// Token: 0x04001475 RID: 5237
		private string _namespaceName;

		// Token: 0x04001476 RID: 5238
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04001477 RID: 5239
		private Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> _facetDescriptions = new Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>();

		// Token: 0x04001478 RID: 5240
		private ReadOnlyCollection<EdmFunction> _functions;

		// Token: 0x04001479 RID: 5241
		private Dictionary<string, PrimitiveType> _storeTypeNameToEdmPrimitiveType = new Dictionary<string, PrimitiveType>();

		// Token: 0x0400147A RID: 5242
		private Dictionary<string, PrimitiveType> _storeTypeNameToStorePrimitiveType = new Dictionary<string, PrimitiveType>();

		// Token: 0x02000645 RID: 1605
		private class EmptyItemCollection : ItemCollection
		{
			// Token: 0x060043BD RID: 17341 RVA: 0x000F6227 File Offset: 0x000F4427
			public EmptyItemCollection() : base(DataSpace.SSpace)
			{
			}
		}
	}
}
