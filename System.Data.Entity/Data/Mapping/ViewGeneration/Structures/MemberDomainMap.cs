using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002AE RID: 686
	internal class MemberDomainMap : InternalBase
	{
		// Token: 0x060028CB RID: 10443 RVA: 0x0009DCF6 File Offset: 0x0009BEF6
		private MemberDomainMap(Dictionary<MemberPath, Set<Constant>> domainMap, Dictionary<MemberPath, Set<Constant>> nonConditionDomainMap, EdmItemCollection edmItemCollection)
		{
			this.m_conditionDomainMap = domainMap;
			this.m_nonConditionDomainMap = nonConditionDomainMap;
			this.m_edmItemCollection = edmItemCollection;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0009DD20 File Offset: 0x0009BF20
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
						ErrorLog.Record errorRecord = new ErrorLog.Record(true, ViewGenErrorCode.InvalidCondition, message, cell, string.Empty);
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
						this.m_nonConditionDomainMap.Add(memberPath2, new MemberDomainMap.CellConstantSetInfo(set2, memberProjectedSlot));
					}
				}
			}
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0009E020 File Offset: 0x0009C220
		internal bool IsProjectedConditionMember(MemberPath memberPath)
		{
			return this.m_projectedConditionMembers.Contains(memberPath);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0009E030 File Offset: 0x0009C230
		internal MemberDomainMap GetOpenDomain()
		{
			Dictionary<MemberPath, Set<Constant>> dictionary = this.m_conditionDomainMap.ToDictionary((KeyValuePair<MemberPath, Set<Constant>> p) => p.Key, (KeyValuePair<MemberPath, Set<Constant>> p) => new Set<Constant>(p.Value, Constant.EqualityComparer));
			this.ExpandDomainsIfNeeded(dictionary);
			return new MemberDomainMap(dictionary, this.m_nonConditionDomainMap, this.m_edmItemCollection);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0009E0A0 File Offset: 0x0009C2A0
		internal MemberDomainMap MakeCopy()
		{
			Dictionary<MemberPath, Set<Constant>> domainMap = this.m_conditionDomainMap.ToDictionary((KeyValuePair<MemberPath, Set<Constant>> p) => p.Key, (KeyValuePair<MemberPath, Set<Constant>> p) => new Set<Constant>(p.Value, Constant.EqualityComparer));
			return new MemberDomainMap(domainMap, this.m_nonConditionDomainMap, this.m_edmItemCollection);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x0009E109 File Offset: 0x0009C309
		internal void ExpandDomainsToIncludeAllPossibleValues()
		{
			this.ExpandDomainsIfNeeded(this.m_conditionDomainMap);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x0009E118 File Offset: 0x0009C318
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

		// Token: 0x060028D2 RID: 10450 RVA: 0x0009E1D0 File Offset: 0x0009C3D0
		internal void ReduceEnumerableDomainToEnumeratedValues(ViewTarget target, ConfigViewGenerator config)
		{
			MemberDomainMap.ReduceEnumerableDomainToEnumeratedValues(target, this.m_conditionDomainMap, config, this.m_edmItemCollection);
			MemberDomainMap.ReduceEnumerableDomainToEnumeratedValues(target, this.m_nonConditionDomainMap, config, this.m_edmItemCollection);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x0009E1F8 File Offset: 0x0009C3F8
		private static void ReduceEnumerableDomainToEnumeratedValues(ViewTarget target, Dictionary<MemberPath, Set<Constant>> domainMap, ConfigViewGenerator config, EdmItemCollection edmItemCollection)
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

		// Token: 0x060028D4 RID: 10452 RVA: 0x0009E2B4 File Offset: 0x0009C4B4
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

		// Token: 0x060028D5 RID: 10453 RVA: 0x0009E3D4 File Offset: 0x0009C5D4
		private static void ExpandNegationsInDomainMap(Dictionary<MemberPath, Set<Constant>> domainMap)
		{
			foreach (MemberPath key in domainMap.Keys.ToArray<MemberPath>())
			{
				domainMap[key] = Domain.ExpandNegationsInDomain(domainMap[key]);
			}
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0009E412 File Offset: 0x0009C612
		internal bool IsConditionMember(MemberPath path)
		{
			return this.m_conditionDomainMap.ContainsKey(path);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0009E420 File Offset: 0x0009C620
		internal IEnumerable<MemberPath> ConditionMembers(EntitySetBase extent)
		{
			foreach (MemberPath memberPath in this.m_conditionDomainMap.Keys)
			{
				if (memberPath.Extent.Equals(extent))
				{
					yield return memberPath;
				}
			}
			Dictionary<MemberPath, Set<Constant>>.KeyCollection.Enumerator enumerator = default(Dictionary<MemberPath, Set<Constant>>.KeyCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0009E437 File Offset: 0x0009C637
		internal IEnumerable<MemberPath> NonConditionMembers(EntitySetBase extent)
		{
			foreach (MemberPath memberPath in this.m_nonConditionDomainMap.Keys)
			{
				if (memberPath.Extent.Equals(extent))
				{
					yield return memberPath;
				}
			}
			Dictionary<MemberPath, Set<Constant>>.KeyCollection.Enumerator enumerator = default(Dictionary<MemberPath, Set<Constant>>.KeyCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0009E450 File Offset: 0x0009C650
		internal void AddSentinel(MemberPath path)
		{
			Set<Constant> domainInternal = this.GetDomainInternal(path);
			domainInternal.Add(Constant.AllOtherConstants);
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x0009E470 File Offset: 0x0009C670
		internal void RemoveSentinel(MemberPath path)
		{
			Set<Constant> domainInternal = this.GetDomainInternal(path);
			domainInternal.Remove(Constant.AllOtherConstants);
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0009E490 File Offset: 0x0009C690
		internal IEnumerable<Constant> GetDomain(MemberPath path)
		{
			return this.GetDomainInternal(path);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0009E49C File Offset: 0x0009C69C
		private Set<Constant> GetDomainInternal(MemberPath path)
		{
			Set<Constant> result;
			if (!this.m_conditionDomainMap.TryGetValue(path, out result))
			{
				result = this.m_nonConditionDomainMap[path];
			}
			return result;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0009E4CC File Offset: 0x0009C6CC
		internal void UpdateConditionMemberDomain(MemberPath path, IEnumerable<Constant> domainValues)
		{
			Set<Constant> set = this.m_conditionDomainMap[path];
			set.Clear();
			set.Unite(domainValues);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x0009E4F4 File Offset: 0x0009C6F4
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

		// Token: 0x060028DF RID: 10463 RVA: 0x0009E538 File Offset: 0x0009C738
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

		// Token: 0x0400126C RID: 4716
		private Dictionary<MemberPath, Set<Constant>> m_conditionDomainMap;

		// Token: 0x0400126D RID: 4717
		private Dictionary<MemberPath, Set<Constant>> m_nonConditionDomainMap;

		// Token: 0x0400126E RID: 4718
		private Set<MemberPath> m_projectedConditionMembers = new Set<MemberPath>();

		// Token: 0x0400126F RID: 4719
		private EdmItemCollection m_edmItemCollection;

		// Token: 0x020005F6 RID: 1526
		private class CellConstantSetInfo : Set<Constant>
		{
			// Token: 0x0600420B RID: 16907 RVA: 0x000F019F File Offset: 0x000EE39F
			internal CellConstantSetInfo(Set<Constant> iconstants, MemberProjectedSlot islot) : base(iconstants)
			{
				this.slot = islot;
			}

			// Token: 0x0600420C RID: 16908 RVA: 0x000F01AF File Offset: 0x000EE3AF
			public override string ToString()
			{
				return base.ToString();
			}

			// Token: 0x04001DB8 RID: 7608
			internal MemberProjectedSlot slot;
		}
	}
}
