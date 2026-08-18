using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200049C RID: 1180
	internal class ViewKeyConstraint : KeyConstraint<ViewCellRelation, ViewCellSlot>
	{
		// Token: 0x06002B89 RID: 11145 RVA: 0x000D38A7 File Offset: 0x000D1AA7
		internal ViewKeyConstraint(ViewCellRelation relation, IEnumerable<ViewCellSlot> keySlots) : base(relation, keySlots, ProjectedSlot.EqualityComparer)
		{
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000D38B6 File Offset: 0x000D1AB6
		internal Cell Cell
		{
			get
			{
				return base.CellRelation.Cell;
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000D38C4 File Offset: 0x000D1AC4
		internal bool Implies(ViewKeyConstraint second)
		{
			if (!object.ReferenceEquals(base.CellRelation, second.CellRelation))
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

		// Token: 0x06002B8C RID: 11148 RVA: 0x000D39EC File Offset: 0x000D1BEC
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
			return new ErrorLog.Record(ViewGenErrorCode.KeyConstraintViolation, message, rightKeyConstraint.CellRelation.Cell, debugMessage);
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000D3AF8 File Offset: 0x000D1CF8
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
			return new ErrorLog.Record(ViewGenErrorCode.KeyConstraintUpdateViolation, message, viewKeyConstraint.CellRelation.Cell, debugMessage);
		}
	}
}
