using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002A2 RID: 674
	internal class Domain : InternalBase
	{
		// Token: 0x0600280B RID: 10251 RVA: 0x0009B209 File Offset: 0x00099409
		internal Domain(Constant value, IEnumerable<Constant> possibleDiscreteValues) : this(new Constant[]
		{
			value
		}, possibleDiscreteValues)
		{
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x0009B21C File Offset: 0x0009941C
		internal Domain(IEnumerable<Constant> values, IEnumerable<Constant> possibleDiscreteValues)
		{
			this.m_possibleValues = Domain.DeterminePossibleValues(values, possibleDiscreteValues);
			this.m_domain = Domain.ExpandNegationsInDomain(values, this.m_possibleValues);
			this.AssertInvariant();
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0009B249 File Offset: 0x00099449
		internal Domain(Domain domain)
		{
			this.m_domain = new Set<Constant>(domain.m_domain, Constant.EqualityComparer);
			this.m_possibleValues = new Set<Constant>(domain.m_possibleValues, Constant.EqualityComparer);
			this.AssertInvariant();
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x0009B283 File Offset: 0x00099483
		internal IEnumerable<Constant> AllPossibleValues
		{
			get
			{
				return this.AllPossibleValuesInternal;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x0009B28C File Offset: 0x0009948C
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

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x0009B2BA File Offset: 0x000994BA
		internal int Count
		{
			get
			{
				return this.m_domain.Count;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x0009B2C7 File Offset: 0x000994C7
		internal IEnumerable<Constant> Values
		{
			get
			{
				return this.m_domain;
			}
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x0009B2D0 File Offset: 0x000994D0
		internal static Set<Constant> DeriveDomainFromMemberPath(MemberPath memberPath, EdmItemCollection edmItemCollection, bool leaveDomainUnbounded)
		{
			Set<Constant> set = Domain.DeriveDomainFromType(memberPath.EdmType, edmItemCollection, leaveDomainUnbounded);
			if (memberPath.IsNullable)
			{
				set.Add(Constant.Null);
			}
			return set;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0009B300 File Offset: 0x00099500
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

		// Token: 0x06002814 RID: 10260 RVA: 0x0009B3CC File Offset: 0x000995CC
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

		// Token: 0x06002815 RID: 10261 RVA: 0x0009B408 File Offset: 0x00099608
		internal static Constant GetDefaultValueForMemberPath(MemberPath memberPath, IEnumerable<LeftCellWrapper> wrappersForErrorReporting, ConfigViewGenerator config)
		{
			Constant result = null;
			if (!Domain.TryGetDefaultValueForMemberPath(memberPath, out result))
			{
				string message = Strings.ViewGen_No_Default_Value(memberPath.Extent.Name, memberPath.PathToString(new bool?(false)));
				ErrorLog.Record errorRecord = new ErrorLog.Record(true, ViewGenErrorCode.NoDefaultValue, message, wrappersForErrorReporting, string.Empty);
				ExceptionHelpers.ThrowMappingException(errorRecord, config);
			}
			return result;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0009B45C File Offset: 0x0009965C
		internal int GetHash()
		{
			int num = 0;
			foreach (Constant obj in this.m_domain)
			{
				num ^= Constant.EqualityComparer.GetHashCode(obj);
			}
			return num;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x0009B4BC File Offset: 0x000996BC
		internal bool IsEqualTo(Domain second)
		{
			return this.m_domain.SetEquals(second.m_domain);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0009B4D0 File Offset: 0x000996D0
		internal bool ContainsNotNull()
		{
			NegatedConstant negatedConstant = Domain.GetNegatedConstant(this.m_domain);
			return negatedConstant != null && negatedConstant.Contains(Constant.Null);
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0009B4F9 File Offset: 0x000996F9
		internal bool Contains(Constant constant)
		{
			return this.m_domain.Contains(constant);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0009B508 File Offset: 0x00099708
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

		// Token: 0x0600281B RID: 10267 RVA: 0x0009B598 File Offset: 0x00099798
		internal static Set<Constant> ExpandNegationsInDomain(IEnumerable<Constant> domain)
		{
			return Domain.ExpandNegationsInDomain(domain, domain);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0009B5A4 File Offset: 0x000997A4
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

		// Token: 0x0600281D RID: 10269 RVA: 0x0009B648 File Offset: 0x00099848
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

		// Token: 0x0600281E RID: 10270 RVA: 0x0009B764 File Offset: 0x00099964
		private static bool GetRestrictedOrUnrestrictedDomain(MemberProjectedSlot slot, CellQuery cellQuery, EdmItemCollection edmItemCollection, out Set<Constant> domain)
		{
			Set<Constant> domain2 = Domain.DeriveDomainFromMemberPath(slot.MemberPath, edmItemCollection, true);
			return Domain.TryGetDomainRestrictedByWhereClause(domain2, slot, cellQuery, out domain);
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x0009B788 File Offset: 0x00099988
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

		// Token: 0x06002820 RID: 10272 RVA: 0x0009B8E8 File Offset: 0x00099AE8
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

		// Token: 0x06002821 RID: 10273 RVA: 0x0009B9EC File Offset: 0x00099BEC
		private Domain Intersect(Domain second)
		{
			Domain domain = new Domain(this);
			domain.m_domain.Intersect(second.m_domain);
			return domain;
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0009BA14 File Offset: 0x00099C14
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

		// Token: 0x06002823 RID: 10275 RVA: 0x0009BA64 File Offset: 0x00099C64
		private static Set<Constant> DeterminePossibleValues(IEnumerable<Constant> domain1, IEnumerable<Constant> domain2)
		{
			Set<Constant> domain3 = new Set<Constant>(domain1, Constant.EqualityComparer).Union(domain2);
			return Domain.DeterminePossibleValues(domain3);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0009BA8B File Offset: 0x00099C8B
		[Conditional("DEBUG")]
		private static void CheckTwoDomainInvariants(Domain domain1, Domain domain2)
		{
			domain1.AssertInvariant();
			domain2.AssertInvariant();
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0009BA99 File Offset: 0x00099C99
		private static IEnumerable<Constant> CreateList(object value1, object value2)
		{
			yield return new ScalarConstant(value1);
			yield return new ScalarConstant(value2);
			yield break;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0009BAB0 File Offset: 0x00099CB0
		internal void AssertInvariant()
		{
			NegatedConstant negatedConstant = Domain.GetNegatedConstant(this.m_domain);
			negatedConstant = Domain.GetNegatedConstant(this.m_possibleValues);
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0009BAD8 File Offset: 0x00099CD8
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

		// Token: 0x06002828 RID: 10280 RVA: 0x0009BB50 File Offset: 0x00099D50
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.ToUserString());
		}

		// Token: 0x04001241 RID: 4673
		private Set<Constant> m_domain;

		// Token: 0x04001242 RID: 4674
		private Set<Constant> m_possibleValues;
	}
}
