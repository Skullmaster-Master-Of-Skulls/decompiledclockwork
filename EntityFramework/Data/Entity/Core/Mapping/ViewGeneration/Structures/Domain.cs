using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000471 RID: 1137
	internal class Domain : InternalBase
	{
		// Token: 0x060029C8 RID: 10696 RVA: 0x000C9A24 File Offset: 0x000C7C24
		internal Domain(Constant value, IEnumerable<Constant> possibleDiscreteValues) : this(new Constant[]
		{
			value
		}, possibleDiscreteValues)
		{
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000C9A44 File Offset: 0x000C7C44
		internal Domain(IEnumerable<Constant> values, IEnumerable<Constant> possibleDiscreteValues)
		{
			this.m_possibleValues = Domain.DeterminePossibleValues(values, possibleDiscreteValues);
			this.m_domain = Domain.ExpandNegationsInDomain(values, this.m_possibleValues);
			this.AssertInvariant();
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000C9A71 File Offset: 0x000C7C71
		internal Domain(Domain domain)
		{
			this.m_domain = new Set<Constant>(domain.m_domain, Constant.EqualityComparer);
			this.m_possibleValues = new Set<Constant>(domain.m_possibleValues, Constant.EqualityComparer);
			this.AssertInvariant();
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060029CB RID: 10699 RVA: 0x000C9AAB File Offset: 0x000C7CAB
		internal IEnumerable<Constant> AllPossibleValues
		{
			get
			{
				return this.AllPossibleValuesInternal;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000C9AB4 File Offset: 0x000C7CB4
		private Set<Constant> AllPossibleValuesInternal
		{
			get
			{
				NegatedConstant negatedConstant = new NegatedConstant(this.m_possibleValues);
				return this.m_possibleValues.Union(new Constant[]
				{
					negatedConstant
				});
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000C9AE4 File Offset: 0x000C7CE4
		internal int Count
		{
			get
			{
				return this.m_domain.Count;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x000C9AF1 File Offset: 0x000C7CF1
		internal IEnumerable<Constant> Values
		{
			get
			{
				return this.m_domain;
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000C9AFC File Offset: 0x000C7CFC
		internal static Set<Constant> DeriveDomainFromMemberPath(MemberPath memberPath, EdmItemCollection edmItemCollection, bool leaveDomainUnbounded)
		{
			Set<Constant> set = Domain.DeriveDomainFromType(memberPath.EdmType, edmItemCollection, leaveDomainUnbounded);
			if (memberPath.IsNullable)
			{
				set.Add(Constant.Null);
			}
			return set;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000C9B2C File Offset: 0x000C7D2C
		private static Set<Constant> DeriveDomainFromType(EdmType type, EdmItemCollection edmItemCollection, bool leaveDomainUnbounded)
		{
			Set<Constant> set;
			if (Helper.IsScalarType(type))
			{
				if (MetadataHelper.HasDiscreteDomain(type))
				{
					set = new Set<Constant>(Domain.CreateList(true, false), Constant.EqualityComparer);
				}
				else
				{
					set = new Set<Constant>(Constant.EqualityComparer);
					if (leaveDomainUnbounded)
					{
						set.Add(Constant.NotNull);
					}
				}
			}
			else
			{
				if (Helper.IsRefType(type))
				{
					type = ((RefType)type).ElementType;
				}
				List<Constant> list = new List<Constant>();
				foreach (EdmType type2 in MetadataHelper.GetTypeAndSubtypesOf(type, edmItemCollection, false))
				{
					TypeConstant item = new TypeConstant(type2);
					list.Add(item);
				}
				set = new Set<Constant>(list, Constant.EqualityComparer);
			}
			return set;
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000C9C00 File Offset: 0x000C7E00
		internal static bool TryGetDefaultValueForMemberPath(MemberPath memberPath, out Constant defaultConstant)
		{
			object defaultValue = memberPath.DefaultValue;
			defaultConstant = Constant.Null;
			if (defaultValue != null)
			{
				defaultConstant = new ScalarConstant(defaultValue);
				return true;
			}
			return memberPath.IsNullable || memberPath.IsComputed;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000C9C3C File Offset: 0x000C7E3C
		internal static Constant GetDefaultValueForMemberPath(MemberPath memberPath, IEnumerable<LeftCellWrapper> wrappersForErrorReporting, ConfigViewGenerator config)
		{
			Constant result = null;
			if (!Domain.TryGetDefaultValueForMemberPath(memberPath, out result))
			{
				string message = Strings.ViewGen_No_Default_Value(memberPath.Extent.Name, memberPath.PathToString(new bool?(false)));
				ErrorLog.Record errorRecord = new ErrorLog.Record(ViewGenErrorCode.NoDefaultValue, message, wrappersForErrorReporting, string.Empty);
				ExceptionHelpers.ThrowMappingException(errorRecord, config);
			}
			return result;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000C9C8C File Offset: 0x000C7E8C
		internal int GetHash()
		{
			int num = 0;
			foreach (Constant obj in this.m_domain)
			{
				num ^= Constant.EqualityComparer.GetHashCode(obj);
			}
			return num;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000C9CEC File Offset: 0x000C7EEC
		internal bool IsEqualTo(Domain second)
		{
			return this.m_domain.SetEquals(second.m_domain);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000C9D00 File Offset: 0x000C7F00
		internal bool ContainsNotNull()
		{
			NegatedConstant negatedConstant = Domain.GetNegatedConstant(this.m_domain);
			return negatedConstant != null && negatedConstant.Contains(Constant.Null);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000C9D29 File Offset: 0x000C7F29
		internal bool Contains(Constant constant)
		{
			return this.m_domain.Contains(constant);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000C9D38 File Offset: 0x000C7F38
		internal static Set<Constant> ExpandNegationsInDomain(IEnumerable<Constant> domain, IEnumerable<Constant> otherPossibleValues)
		{
			Set<Constant> set = Domain.DeterminePossibleValues(domain, otherPossibleValues);
			Set<Constant> set2 = new Set<Constant>(Constant.EqualityComparer);
			foreach (Constant constant in domain)
			{
				NegatedConstant negatedConstant = constant as NegatedConstant;
				if (negatedConstant != null)
				{
					set2.Add(new NegatedConstant(set));
					Set<Constant> elements = set.Difference(negatedConstant.Elements);
					set2.AddRange(elements);
				}
				else
				{
					set2.Add(constant);
				}
			}
			return set2;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000C9DC8 File Offset: 0x000C7FC8
		internal static Set<Constant> ExpandNegationsInDomain(IEnumerable<Constant> domain)
		{
			return Domain.ExpandNegationsInDomain(domain, domain);
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000C9DD4 File Offset: 0x000C7FD4
		private static Set<Constant> DeterminePossibleValues(IEnumerable<Constant> domain)
		{
			Set<Constant> set = new Set<Constant>(Constant.EqualityComparer);
			foreach (Constant constant in domain)
			{
				NegatedConstant negatedConstant = constant as NegatedConstant;
				if (negatedConstant != null)
				{
					using (IEnumerator<Constant> enumerator2 = negatedConstant.Elements.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Constant element = enumerator2.Current;
							set.Add(element);
						}
						continue;
					}
				}
				set.Add(constant);
			}
			return set;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000C9E98 File Offset: 0x000C8098
		internal static Dictionary<MemberPath, Set<Constant>> ComputeConstantDomainSetsForSlotsInQueryViews(IEnumerable<Cell> cells, EdmItemCollection edmItemCollection, bool isValidationEnabled)
		{
			Dictionary<MemberPath, Set<Constant>> dictionary = new Dictionary<MemberPath, Set<Constant>>(MemberPath.EqualityComparer);
			foreach (Cell cell in cells)
			{
				CellQuery cquery = cell.CQuery;
				foreach (MemberRestriction memberRestriction in cquery.GetConjunctsFromWhereClause())
				{
					MemberProjectedSlot restrictedMemberSlot = memberRestriction.RestrictedMemberSlot;
					Set<Constant> set = Domain.DeriveDomainFromMemberPath(restrictedMemberSlot.MemberPath, edmItemCollection, isValidationEnabled);
					set.AddRange(from c in memberRestriction.Domain.Values
					where !c.Equals(Constant.Null) && !c.Equals(Constant.NotNull)
					select c);
					Set<Constant> set2;
					if (!dictionary.TryGetValue(restrictedMemberSlot.MemberPath, out set2))
					{
						dictionary[restrictedMemberSlot.MemberPath] = set;
					}
					else
					{
						set2.AddRange(set);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000C9FB4 File Offset: 0x000C81B4
		private static bool GetRestrictedOrUnrestrictedDomain(MemberProjectedSlot slot, CellQuery cellQuery, EdmItemCollection edmItemCollection, out Set<Constant> domain)
		{
			Set<Constant> domain2 = Domain.DeriveDomainFromMemberPath(slot.MemberPath, edmItemCollection, true);
			return Domain.TryGetDomainRestrictedByWhereClause(domain2, slot, cellQuery, out domain);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000C9FE0 File Offset: 0x000C81E0
		internal static Dictionary<MemberPath, Set<Constant>> ComputeConstantDomainSetsForSlotsInUpdateViews(IEnumerable<Cell> cells, EdmItemCollection edmItemCollection)
		{
			Dictionary<MemberPath, Set<Constant>> dictionary = new Dictionary<MemberPath, Set<Constant>>(MemberPath.EqualityComparer);
			foreach (Cell cell in cells)
			{
				CellQuery cquery = cell.CQuery;
				CellQuery squery = cell.SQuery;
				foreach (MemberProjectedSlot memberProjectedSlot in from oneOfConst in squery.GetConjunctsFromWhereClause()
				select oneOfConst.RestrictedMemberSlot)
				{
					Set<Constant> set;
					if (!Domain.GetRestrictedOrUnrestrictedDomain(memberProjectedSlot, squery, edmItemCollection, out set))
					{
						int projectedPosition = squery.GetProjectedPosition(memberProjectedSlot);
						if (projectedPosition >= 0)
						{
							MemberProjectedSlot slot = cquery.ProjectedSlotAt(projectedPosition) as MemberProjectedSlot;
							bool restrictedOrUnrestrictedDomain = Domain.GetRestrictedOrUnrestrictedDomain(slot, cquery, edmItemCollection, out set);
							if (!restrictedOrUnrestrictedDomain)
							{
								continue;
							}
						}
					}
					MemberPath memberPath = memberProjectedSlot.MemberPath;
					Constant element;
					if (Domain.TryGetDefaultValueForMemberPath(memberPath, out element))
					{
						set.Add(element);
					}
					Set<Constant> set2;
					if (!dictionary.TryGetValue(memberPath, out set2))
					{
						dictionary[memberPath] = set;
					}
					else
					{
						set2.AddRange(set);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000CA194 File Offset: 0x000C8394
		private static bool TryGetDomainRestrictedByWhereClause(IEnumerable<Constant> domain, MemberProjectedSlot slot, CellQuery cellQuery, out Set<Constant> result)
		{
			IEnumerable<Set<Constant>> enumerable = from restriction in cellQuery.GetConjunctsFromWhereClause()
			where MemberPath.EqualityComparer.Equals(restriction.RestrictedMemberSlot.MemberPath, slot.MemberPath)
			select new Set<Constant>(restriction.Domain.Values, Constant.EqualityComparer);
			if (!enumerable.Any<Set<Constant>>())
			{
				result = new Set<Constant>(domain);
				return false;
			}
			Set<Constant> possibleDiscreteValues = Domain.DeterminePossibleValues(enumerable.SelectMany((Set<Constant> m) => from c in m
			select c), domain);
			Domain domain2 = new Domain(domain, possibleDiscreteValues);
			foreach (Set<Constant> values in enumerable)
			{
				domain2 = domain2.Intersect(new Domain(values, possibleDiscreteValues));
			}
			result = new Set<Constant>(domain2.Values, Constant.EqualityComparer);
			return !domain.SequenceEqual(result);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000CA294 File Offset: 0x000C8494
		private Domain Intersect(Domain second)
		{
			Domain domain = new Domain(this);
			domain.m_domain.Intersect(second.m_domain);
			return domain;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000CA2BC File Offset: 0x000C84BC
		private static NegatedConstant GetNegatedConstant(IEnumerable<Constant> constants)
		{
			NegatedConstant result = null;
			foreach (Constant constant in constants)
			{
				NegatedConstant negatedConstant = constant as NegatedConstant;
				if (negatedConstant != null)
				{
					result = negatedConstant;
				}
			}
			return result;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000CA30C File Offset: 0x000C850C
		private static Set<Constant> DeterminePossibleValues(IEnumerable<Constant> domain1, IEnumerable<Constant> domain2)
		{
			Set<Constant> domain3 = new Set<Constant>(domain1, Constant.EqualityComparer).Union(domain2);
			return Domain.DeterminePossibleValues(domain3);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000CA333 File Offset: 0x000C8533
		[Conditional("DEBUG")]
		private static void CheckTwoDomainInvariants(Domain domain1, Domain domain2)
		{
			domain1.AssertInvariant();
			domain2.AssertInvariant();
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000CA444 File Offset: 0x000C8644
		private static IEnumerable<Constant> CreateList(object value1, object value2)
		{
			yield return new ScalarConstant(value1);
			yield return new ScalarConstant(value2);
			yield break;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000CA468 File Offset: 0x000C8668
		internal void AssertInvariant()
		{
			Domain.GetNegatedConstant(this.m_domain);
			Domain.GetNegatedConstant(this.m_possibleValues);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000CA484 File Offset: 0x000C8684
		internal string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (Constant constant in this.m_domain)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(constant.ToUserString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000CA4FC File Offset: 0x000C86FC
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.ToUserString());
		}

		// Token: 0x04000F82 RID: 3970
		private readonly Set<Constant> m_domain;

		// Token: 0x04000F83 RID: 3971
		private readonly Set<Constant> m_possibleValues;
	}
}
