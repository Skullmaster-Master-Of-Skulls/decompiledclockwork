using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000288 RID: 648
	internal class ViewKeyConstraint : KeyConstraint<ViewCellRelation, ViewCellSlot>
	{
		// Token: 0x060026CF RID: 9935 RVA: 0x00095D8D File Offset: 0x00093F8D
		internal ViewKeyConstraint(ViewCellRelation relation, IEnumerable<ViewCellSlot> keySlots) : base(relation, keySlots, ProjectedSlot.EqualityComparer)
		{
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x00095D9C File Offset: 0x00093F9C
		internal Cell Cell
		{
			get
			{
				return base.CellRelation.Cell;
			}
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x00095DAC File Offset: 0x00093FAC
		internal bool Implies(ViewKeyConstraint second)
		{
			if (base.CellRelation != second.CellRelation)
			{
				return false;
			}
			if (base.KeySlots.IsSubsetOf(second.KeySlots))
			{
				return true;
			}
			Set<ViewCellSlot> set = new Set<ViewCellSlot>(second.KeySlots);
			foreach (ViewCellSlot viewCellSlot in base.KeySlots)
			{
				bool flag = false;
				foreach (ViewCellSlot viewCellSlot2 in set)
				{
					if (ProjectedSlot.EqualityComparer.Equals(viewCellSlot.SSlot, viewCellSlot2.SSlot))
					{
						MemberPath memberPath = viewCellSlot.CSlot.MemberPath;
						MemberPath memberPath2 = viewCellSlot2.CSlot.MemberPath;
						if (MemberPath.EqualityComparer.Equals(memberPath, memberPath2) || memberPath.IsEquivalentViaRefConstraint(memberPath2))
						{
							set.Remove(viewCellSlot2);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x00095ED0 File Offset: 0x000940D0
		internal static ErrorLog.Record GetErrorRecord(ViewKeyConstraint rightKeyConstraint)
		{
			List<ViewCellSlot> list = new List<ViewCellSlot>(rightKeyConstraint.KeySlots);
			EntitySetBase extent = list[0].SSlot.MemberPath.Extent;
			EntitySetBase extent2 = list[0].CSlot.MemberPath.Extent;
			MemberPath prefix = new MemberPath(extent);
			MemberPath prefix2 = new MemberPath(extent2);
			ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(prefix, (EntityType)extent.ElementType);
			ExtentKey extentKey;
			if (extent2 is EntitySet)
			{
				extentKey = ExtentKey.GetPrimaryKeyForEntityType(prefix2, (EntityType)extent2.ElementType);
			}
			else
			{
				extentKey = ExtentKey.GetKeyForRelationType(prefix2, (AssociationType)extent2.ElementType);
			}
			string message = Strings.ViewGen_KeyConstraint_Violation(extent.Name, ViewCellSlot.SlotsToUserString(rightKeyConstraint.KeySlots, false), primaryKeyForEntityType.ToUserString(), extent2.Name, ViewCellSlot.SlotsToUserString(rightKeyConstraint.KeySlots, true), extentKey.ToUserString());
			string debugMessage = StringUtil.FormatInvariant("PROBLEM: Not implied {0}", new object[]
			{
				rightKeyConstraint
			});
			return new ErrorLog.Record(true, ViewGenErrorCode.KeyConstraintViolation, message, rightKeyConstraint.CellRelation.Cell, debugMessage);
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x00095FD8 File Offset: 0x000941D8
		internal static ErrorLog.Record GetErrorRecord(IEnumerable<ViewKeyConstraint> rightKeyConstraints)
		{
			ViewKeyConstraint viewKeyConstraint = null;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (ViewKeyConstraint viewKeyConstraint2 in rightKeyConstraints)
			{
				string value = ViewCellSlot.SlotsToUserString(viewKeyConstraint2.KeySlots, true);
				if (!flag)
				{
					stringBuilder.Append("; ");
				}
				flag = false;
				stringBuilder.Append(value);
				viewKeyConstraint = viewKeyConstraint2;
			}
			List<ViewCellSlot> list = new List<ViewCellSlot>(viewKeyConstraint.KeySlots);
			EntitySetBase extent = list[0].SSlot.MemberPath.Extent;
			EntitySetBase extent2 = list[0].CSlot.MemberPath.Extent;
			MemberPath prefix = new MemberPath(extent);
			ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(prefix, (EntityType)extent.ElementType);
			string message;
			if (extent2 is EntitySet)
			{
				message = Strings.ViewGen_KeyConstraint_Update_Violation_EntitySet(stringBuilder.ToString(), extent2.Name, primaryKeyForEntityType.ToUserString(), extent.Name);
			}
			else
			{
				AssociationSet associationSet = (AssociationSet)extent2;
				AssociationEndMember endThatShouldBeMappedToKey = Helper.GetEndThatShouldBeMappedToKey(associationSet.ElementType);
				if (endThatShouldBeMappedToKey != null)
				{
					message = Strings.ViewGen_AssociationEndShouldBeMappedToKey(endThatShouldBeMappedToKey.Name, extent.Name);
				}
				else
				{
					message = Strings.ViewGen_KeyConstraint_Update_Violation_AssociationSet(extent2.Name, primaryKeyForEntityType.ToUserString(), extent.Name);
				}
			}
			string debugMessage = StringUtil.FormatInvariant("PROBLEM: Not implied {0}", new object[]
			{
				viewKeyConstraint
			});
			return new ErrorLog.Record(true, ViewGenErrorCode.KeyConstraintUpdateViolation, message, viewKeyConstraint.CellRelation.Cell, debugMessage);
		}
	}
}
