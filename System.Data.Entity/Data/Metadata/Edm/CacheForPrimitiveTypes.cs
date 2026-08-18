using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001B6 RID: 438
	internal class CacheForPrimitiveTypes
	{
		// Token: 0x06001EDC RID: 7900 RVA: 0x0006CB54 File Offset: 0x0006AD54
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

		// Token: 0x06001EDD RID: 7901 RVA: 0x0006CBA0 File Offset: 0x0006ADA0
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

		// Token: 0x06001EDE RID: 7902 RVA: 0x0006CD14 File Offset: 0x0006AF14
		private static Facet[] CreateInitialFacets(FacetDescription[] facetDescriptions)
		{
			Facet[] array = new Facet[facetDescriptions.Length];
			for (int i = 0; i < facetDescriptions.Length; i++)
			{
				string facetName = facetDescriptions[i].FacetName;
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
							array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultFixedLengthFacetValue);
						}
					}
					else
					{
						array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultUnicodeFacetValue);
					}
				}
				else
				{
					array[i] = Facet.Create(facetDescriptions[i], TypeUsage.DefaultMaxLengthFacetValue);
				}
			}
			return array;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0006CDF0 File Offset: 0x0006AFF0
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
			return list.AsReadOnly();
		}

		// Token: 0x04000CF7 RID: 3319
		private List<PrimitiveType>[] _primitiveTypeMap = new List<PrimitiveType>[31];
	}
}
