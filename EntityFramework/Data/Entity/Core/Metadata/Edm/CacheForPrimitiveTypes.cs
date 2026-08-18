using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm.Provider;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004AF RID: 1199
	internal class CacheForPrimitiveTypes
	{
		// Token: 0x06002C35 RID: 11317 RVA: 0x000D7018 File Offset: 0x000D5218
		internal void Add(PrimitiveType type)
		{
			List<PrimitiveType> list = EntityUtil.CheckArgumentOutOfRange<List<PrimitiveType>>(this._primitiveTypeMap, (int)type.PrimitiveTypeKind, "primitiveTypeKind");
			if (list == null)
			{
				list = new List<PrimitiveType>();
				list.Add(type);
				this._primitiveTypeMap[(int)type.PrimitiveTypeKind] = list;
				return;
			}
			list.Add(type);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000D7064 File Offset: 0x000D5264
		internal bool TryGetType(PrimitiveTypeKind primitiveTypeKind, IEnumerable<Facet> facets, out PrimitiveType type)
		{
			type = null;
			List<PrimitiveType> list = EntityUtil.CheckArgumentOutOfRange<List<PrimitiveType>>(this._primitiveTypeMap, (int)primitiveTypeKind, "primitiveTypeKind");
			if (list == null || 0 >= list.Count)
			{
				return false;
			}
			if (list.Count == 1)
			{
				type = list[0];
				return true;
			}
			if (facets == null)
			{
				FacetDescription[] initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(primitiveTypeKind);
				if (initialFacetDescriptions == null)
				{
					type = list[0];
					return true;
				}
				facets = CacheForPrimitiveTypes.CreateInitialFacets(initialFacetDescriptions);
			}
			bool flag = false;
			foreach (Facet facet in facets)
			{
				if ((primitiveTypeKind == PrimitiveTypeKind.String || primitiveTypeKind == PrimitiveTypeKind.Binary) && facet.Value != null && facet.Name == "MaxLength" && Helper.IsUnboundedFacetValue(facet))
				{
					flag = true;
				}
			}
			int num = 0;
			foreach (PrimitiveType primitiveType in list)
			{
				if (!flag)
				{
					type = primitiveType;
					break;
				}
				if (type == null)
				{
					type = primitiveType;
					num = Helper.GetFacet(primitiveType.FacetDescriptions, "MaxLength").MaxValue.Value;
				}
				else
				{
					int value = Helper.GetFacet(primitiveType.FacetDescriptions, "MaxLength").MaxValue.Value;
					if (value > num)
					{
						type = primitiveType;
						num = value;
					}
				}
			}
			return true;
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000D71D8 File Offset: 0x000D53D8
		private static Facet[] CreateInitialFacets(FacetDescription[] facetDescriptions)
		{
			Facet[] array = new Facet[facetDescriptions.Length];
			for (int i = 0; i < facetDescriptions.Length; i++)
			{
				string facetName;
				if ((facetName = facetDescriptions[i].FacetName) != null)
				{
					if (!(facetName == "MaxLength"))
					{
						if (!(facetName == "Unicode"))
						{
							if (!(facetName == "FixedLength"))
							{
								if (!(facetName == "Precision"))
								{
									if (facetName == "Scale")
									{
										array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultScaleFacetValue);
									}
								}
								else
								{
									array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultPrecisionFacetValue);
								}
							}
							else
							{
								array[i] = Facet.Create(facetDescriptions[i], false);
							}
						}
						else
						{
							array[i] = Facet.Create(facetDescriptions[i], true);
						}
					}
					else
					{
						array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultMaxLengthFacetValue);
					}
				}
			}
			return array;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000D72B0 File Offset: 0x000D54B0
		internal ReadOnlyCollection<PrimitiveType> GetTypes()
		{
			List<PrimitiveType> list = new List<PrimitiveType>();
			foreach (List<PrimitiveType> list2 in this._primitiveTypeMap)
			{
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return new ReadOnlyCollection<PrimitiveType>(list);
		}

		// Token: 0x04001052 RID: 4178
		private readonly List<PrimitiveType>[] _primitiveTypeMap = new List<PrimitiveType>[31];
	}
}
