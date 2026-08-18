using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200047A RID: 1146
	internal class MemberDomainMap : InternalBase
	{
		// Token: 0x06002A34 RID: 10804 RVA: 0x000CB886 File Offset: 0x000C9A86
		private MemberDomainMap(Dictionary<MemberPath, Set<Constant>> domainMap, Dictionary<MemberPath, Set<Constant>> nonConditionDomainMap, EdmItemCollection edmItemCollection)
		{
			this.m_conditionDomainMap = domainMap;
			this.m_nonConditionDomainMap = nonConditionDomainMap;
			this.m_edmItemCollection = edmItemCollection;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000CB8BC File Offset: 0x000C9ABC
		internal MemberDomainMap(ViewTarget viewTarget, bool isValidationEnabled, IEnumerable<Cell> extentCells, EdmItemCollection edmItemCollection, ConfigViewGenerator config, Dictionary<EntityType, Set<EntityType>> inheritanceGraph)
		{
			this.m_conditionDomainMap = new Dictionary<MemberPath, Set<Constant>>(MemberPath.EqualityComparer);
			this.m_edmItemCollection = edmItemCollection;
			Dictionary<MemberPath, Set<Constant>> dictionary = null;
			if (viewTarget == ViewTarget.UpdateView)
			{
				dictionary = Domain.ComputeConstantDomainSetsForSlotsInUpdateViews(extentCells, this.m_edmItemCollection);
			}
			else
			{
				dictionary = Domain.ComputeConstantDomainSetsForSlotsInQueryViews(extentCells, this.m_edmItemCollection, isValidationEnabled);
			}
			foreach (Cell cell in extentCells)
			{
				CellQuery leftQuery = cell.GetLeftQuery(viewTarget);
				foreach (MemberRestriction memberRestriction in leftQuery.GetConjunctsFromWhereClause())
				{
					MemberPath memberPath = memberRestriction.RestrictedMemberSlot.MemberPath;
					Set<Constant> set;
					if (!dictionary.TryGetValue(memberPath, out set))
					{
						set = Domain.DeriveDomainFromMemberPath(memberPath, edmItemCollection, isValidationEnabled);
					}
					if (!set.Contains(Constant.Null))
					{
						if (memberRestriction.Domain.Values.All((Constant conditionConstant) => conditionConstant.Equals(Constant.NotNull)))
						{
							continue;
						}
					}
					if (set.Count <= 0 || (!set.Contains(Constant.Null) && memberRestriction.Domain.Values.Contains(Constant.Null)))
					{
						string message = Strings.ViewGen_InvalidCondition(memberPath.PathToString(new bool?(false)));
						ErrorLog.Record errorRecord = new ErrorLog.Record(ViewGenErrorCode.InvalidCondition, message, cell, string.Empty);
						ExceptionHelpers.ThrowMappingException(errorRecord, config);
					}
					if (!memberPath.IsAlwaysDefined(inheritanceGraph))
					{
						set.Add(Constant.Undefined);
					}
					this.AddToDomainMap(memberPath, set);
				}
			}
			this.m_nonConditionDomainMap = new Dictionary<MemberPath, Set<Constant>>(MemberPath.EqualityComparer);
			foreach (Cell cell2 in extentCells)
			{
				CellQuery leftQuery2 = cell2.GetLeftQuery(viewTarget);
				foreach (MemberProjectedSlot memberProjectedSlot in leftQuery2.GetAllQuerySlots())
				{
					MemberPath memberPath2 = memberProjectedSlot.MemberPath;
					if (!this.m_conditionDomainMap.ContainsKey(memberPath2) && !this.m_nonConditionDomainMap.ContainsKey(memberPath2))
					{
						Set<Constant> set2 = Domain.DeriveDomainFromMemberPath(memberPath2, this.m_edmItemCollection, true);
						if (!memberPath2.IsAlwaysDefined(inheritanceGraph))
						{
							set2.Add(Constant.Undefined);
						}
						set2 = Domain.ExpandNegationsInDomain(set2, set2);
						this.m_nonConditionDomainMap.Add(memberPath2, new MemberDomainMap.CellConstantSetInfo(set2));
					}
				}
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000CBBB4 File Offset: 0x000C9DB4
		internal bool IsProjectedConditionMember(MemberPath memberPath)
		{
			return this.m_projectedConditionMembers.Contains(memberPath);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000CBBE0 File Offset: 0x000C9DE0
		internal MemberDomainMap GetOpenDomain()
		{
			Dictionary<MemberPath, Set<Constant>> dictionary = this.m_conditionDomainMap.ToDictionary((KeyValuePair<MemberPath, Set<Constant>> p) => p.Key, (KeyValuePair<MemberPath, Set<Constant>> p) => new Set<Constant>(p.Value, Constant.EqualityComparer));
			this.ExpandDomainsIfNeeded(dictionary);
			return new MemberDomainMap(dictionary, this.m_nonConditionDomainMap, this.m_edmItemCollection);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000CBC68 File Offset: 0x000C9E68
		internal MemberDomainMap MakeCopy()
		{
			Dictionary<MemberPath, Set<Constant>> domainMap = this.m_conditionDomainMap.ToDictionary((KeyValuePair<MemberPath, Set<Constant>> p) => p.Key, (KeyValuePair<MemberPath, Set<Constant>> p) => new Set<Constant>(p.Value, Constant.EqualityComparer));
			return new MemberDomainMap(domainMap, this.m_nonConditionDomainMap, this.m_edmItemCollection);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000CBCCD File Offset: 0x000C9ECD
		internal void ExpandDomainsToIncludeAllPossibleValues()
		{
			this.ExpandDomainsIfNeeded(this.m_conditionDomainMap);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000CBCE8 File Offset: 0x000C9EE8
		private void ExpandDomainsIfNeeded(Dictionary<MemberPath, Set<Constant>> domainMapForMembers)
		{
			foreach (MemberPath memberPath in domainMapForMembers.Keys)
			{
				Set<Constant> set = domainMapForMembers[memberPath];
				if (memberPath.IsScalarType())
				{
					if (!set.Any((Constant c) => c is NegatedConstant))
					{
						if (MetadataHelper.HasDiscreteDomain(memberPath.EdmType))
						{
							Set<Constant> other = Domain.DeriveDomainFromMemberPath(memberPath, this.m_edmItemCollection, true);
							set.Unite(other);
						}
						else
						{
							NegatedConstant element = new NegatedConstant(set);
							set.Add(element);
						}
					}
				}
			}
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000CBD9C File Offset: 0x000C9F9C
		internal void ReduceEnumerableDomainToEnumeratedValues(ConfigViewGenerator config)
		{
			MemberDomainMap.ReduceEnumerableDomainToEnumeratedValues(this.m_conditionDomainMap, config, this.m_edmItemCollection);
			MemberDomainMap.ReduceEnumerableDomainToEnumeratedValues(this.m_nonConditionDomainMap, config, this.m_edmItemCollection);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000CBDC4 File Offset: 0x000C9FC4
		private static void ReduceEnumerableDomainToEnumeratedValues(Dictionary<MemberPath, Set<Constant>> domainMap, ConfigViewGenerator config, EdmItemCollection edmItemCollection)
		{
			foreach (MemberPath memberPath in domainMap.Keys)
			{
				if (MetadataHelper.HasDiscreteDomain(memberPath.EdmType))
				{
					Set<Constant> other = Domain.DeriveDomainFromMemberPath(memberPath, edmItemCollection, true);
					Set<Constant> set = domainMap[memberPath].Difference(other);
					set.Remove(Constant.Undefined);
					if (set.Count > 0)
					{
						if (config.IsNormalTracing)
						{
							Helpers.FormatTraceLine("Changed domain of {0} from {1} - subtract {2}", new object[]
							{
								memberPath,
								domainMap[memberPath],
								set
							});
						}
						domainMap[memberPath].Subtract(set);
					}
				}
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000CBEA4 File Offset: 0x000CA0A4
		internal static void PropagateUpdateDomainToQueryDomain(IEnumerable<Cell> cells, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap)
		{
			foreach (Cell cell in cells)
			{
				CellQuery cquery = cell.CQuery;
				CellQuery squery = cell.SQuery;
				for (int i = 0; i < cquery.NumProjectedSlots; i++)
				{
					MemberProjectedSlot memberProjectedSlot = cquery.ProjectedSlotAt(i) as MemberProjectedSlot;
					MemberProjectedSlot memberProjectedSlot2 = squery.ProjectedSlotAt(i) as MemberProjectedSlot;
					if (memberProjectedSlot != null && memberProjectedSlot2 != null)
					{
						MemberPath memberPath = memberProjectedSlot.MemberPath;
						MemberPath memberPath2 = memberProjectedSlot2.MemberPath;
						Set<Constant> domainInternal = queryDomainMap.GetDomainInternal(memberPath);
						Set<Constant> domainInternal2 = updateDomainMap.GetDomainInternal(memberPath2);
						domainInternal.Unite(from constant in domainInternal2
						where !constant.IsNull() && !(constant is NegatedConstant)
						select constant);
						if (updateDomainMap.IsConditionMember(memberPath2) && !queryDomainMap.IsConditionMember(memberPath))
						{
							queryDomainMap.m_projectedConditionMembers.Add(memberPath);
						}
					}
				}
			}
			MemberDomainMap.ExpandNegationsInDomainMap(queryDomainMap.m_conditionDomainMap);
			MemberDomainMap.ExpandNegationsInDomainMap(queryDomainMap.m_nonConditionDomainMap);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000CBFC4 File Offset: 0x000CA1C4
		private static void ExpandNegationsInDomainMap(Dictionary<MemberPath, Set<Constant>> domainMap)
		{
			foreach (MemberPath key in domainMap.Keys.ToArray<MemberPath>())
			{
				domainMap[key] = Domain.ExpandNegationsInDomain(domainMap[key]);
			}
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000CC002 File Offset: 0x000CA202
		internal bool IsConditionMember(MemberPath path)
		{
			return this.m_conditionDomainMap.ContainsKey(path);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000CC1C0 File Offset: 0x000CA3C0
		internal IEnumerable<MemberPath> ConditionMembers(EntitySetBase extent)
		{
			foreach (MemberPath path in this.m_conditionDomainMap.Keys)
			{
				if (path.Extent.Equals(extent))
				{
					yield return path;
				}
			}
			yield break;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000CC394 File Offset: 0x000CA594
		internal IEnumerable<MemberPath> NonConditionMembers(EntitySetBase extent)
		{
			foreach (MemberPath path in this.m_nonConditionDomainMap.Keys)
			{
				if (path.Extent.Equals(extent))
				{
					yield return path;
				}
			}
			yield break;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000CC3B8 File Offset: 0x000CA5B8
		internal void AddSentinel(MemberPath path)
		{
			Set<Constant> domainInternal = this.GetDomainInternal(path);
			domainInternal.Add(Constant.AllOtherConstants);
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000CC3D8 File Offset: 0x000CA5D8
		internal void RemoveSentinel(MemberPath path)
		{
			Set<Constant> domainInternal = this.GetDomainInternal(path);
			domainInternal.Remove(Constant.AllOtherConstants);
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000CC3F8 File Offset: 0x000CA5F8
		internal IEnumerable<Constant> GetDomain(MemberPath path)
		{
			return this.GetDomainInternal(path);
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000CC404 File Offset: 0x000CA604
		private Set<Constant> GetDomainInternal(MemberPath path)
		{
			Set<Constant> result;
			if (!this.m_conditionDomainMap.TryGetValue(path, out result))
			{
				result = this.m_nonConditionDomainMap[path];
			}
			return result;
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000CC434 File Offset: 0x000CA634
		internal void UpdateConditionMemberDomain(MemberPath path, IEnumerable<Constant> domainValues)
		{
			Set<Constant> set = this.m_conditionDomainMap[path];
			set.Clear();
			set.Unite(domainValues);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000CC45C File Offset: 0x000CA65C
		private void AddToDomainMap(MemberPath member, IEnumerable<Constant> domainValues)
		{
			Set<Constant> set;
			if (!this.m_conditionDomainMap.TryGetValue(member, out set))
			{
				set = new Set<Constant>(Constant.EqualityComparer);
			}
			set.Unite(domainValues);
			this.m_conditionDomainMap[member] = Domain.ExpandNegationsInDomain(set, set);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000CC4A0 File Offset: 0x000CA6A0
		internal override void ToCompactString(StringBuilder builder)
		{
			foreach (MemberPath memberPath in this.m_conditionDomainMap.Keys)
			{
				builder.Append('(');
				memberPath.ToCompactString(builder);
				IEnumerable<Constant> domain = this.GetDomain(memberPath);
				builder.Append(": ");
				StringUtil.ToCommaSeparatedStringSorted(builder, domain);
				builder.Append(") ");
			}
		}

		// Token: 0x04000F9C RID: 3996
		private readonly Dictionary<MemberPath, Set<Constant>> m_conditionDomainMap;

		// Token: 0x04000F9D RID: 3997
		private readonly Dictionary<MemberPath, Set<Constant>> m_nonConditionDomainMap;

		// Token: 0x04000F9E RID: 3998
		private readonly Set<MemberPath> m_projectedConditionMembers = new Set<MemberPath>();

		// Token: 0x04000F9F RID: 3999
		private readonly EdmItemCollection m_edmItemCollection;

		// Token: 0x0200047B RID: 1147
		private class CellConstantSetInfo : Set<Constant>
		{
			// Token: 0x06002A50 RID: 10832 RVA: 0x000CC528 File Offset: 0x000CA728
			internal CellConstantSetInfo(Set<Constant> iconstants) : base(iconstants)
			{
			}
		}
	}
}
