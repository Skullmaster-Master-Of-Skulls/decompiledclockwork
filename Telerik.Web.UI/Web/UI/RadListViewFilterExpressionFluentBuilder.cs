using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001982 RID: 6530
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class RadListViewFilterExpressionFluentBuilder : IHideObjectMembers
	{
		// Token: 0x0600FCDD RID: 64733 RVA: 0x0038E3D8 File Offset: 0x0038C5D8
		internal RadListViewFilterExpressionFluentBuilder(IList<RadListViewFilterExpression> collection)
		{
			this._collection = collection;
			this._group = new RadListViewGroupFilterExpression();
			this.IsBuild = false;
		}

		// Token: 0x17004C5E RID: 19550
		// (get) Token: 0x0600FCDE RID: 64734 RVA: 0x0038E3F9 File Offset: 0x0038C5F9
		// (set) Token: 0x0600FCDF RID: 64735 RVA: 0x0038E401 File Offset: 0x0038C601
		public bool IsBuild { get; protected set; }

		// Token: 0x0600FCE0 RID: 64736 RVA: 0x0038E40A File Offset: 0x0038C60A
		internal RadListViewGroupFilterOperator CurrentGroupOperator()
		{
			return this._group.GroupOperator;
		}

		// Token: 0x0600FCE1 RID: 64737 RVA: 0x0038E417 File Offset: 0x0038C617
		internal void WrapInGroup(RadListViewGroupFilterExpression containerGroup)
		{
			containerGroup.Add(this._group);
			this._group = containerGroup;
		}

		// Token: 0x0600FCE2 RID: 64738 RVA: 0x0038E42C File Offset: 0x0038C62C
		private void AddToCollection<T>(string fieldName, T currentValue, TFunc<RadListViewSingleValueExpression<T>> callback)
		{
			this.ValidateFieldName(fieldName);
			RadListViewSingleValueExpression<T> radListViewSingleValueExpression = callback();
			this._group.Add(radListViewSingleValueExpression);
			radListViewSingleValueExpression.CurrentValue = currentValue;
			radListViewSingleValueExpression.FieldName = fieldName;
		}

		// Token: 0x0600FCE3 RID: 64739 RVA: 0x0038E464 File Offset: 0x0038C664
		private void AddNonValueExpressionToCollection(string fieldName, TFunc<RadListViewFilterExpression> callback)
		{
			this.ValidateFieldName(fieldName);
			RadListViewFilterExpression radListViewFilterExpression = callback();
			this._group.Add(radListViewFilterExpression);
			radListViewFilterExpression.FieldName = fieldName;
		}

		// Token: 0x0600FCE4 RID: 64740 RVA: 0x0038E499 File Offset: 0x0038C699
		public RadListViewFilterExpressionLogicalBuilder EqualTo<T>(string fieldName, T currentValue)
		{
			this.AddToCollection<T>(fieldName, currentValue, () => new RadListViewEqualToFilterExpression<T>());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCE5 RID: 64741 RVA: 0x0038E4BC File Offset: 0x0038C6BC
		public RadListViewFilterExpressionLogicalBuilder NotEqualTo<T>(string fieldName, T currentValue)
		{
			this.AddToCollection<T>(fieldName, currentValue, () => new RadListViewNotEqualToFilterExpression<T>());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCE6 RID: 64742 RVA: 0x0038E4DF File Offset: 0x0038C6DF
		public RadListViewFilterExpressionLogicalBuilder GreaterThan<T>(string fieldName, T currentValue)
		{
			this.AddToCollection<T>(fieldName, currentValue, () => new RadListViewGreaterThanFilterExpression<T>());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCE7 RID: 64743 RVA: 0x0038E502 File Offset: 0x0038C702
		public RadListViewFilterExpressionLogicalBuilder GreaterThanOrEqualTo<T>(string fieldName, T currentValue)
		{
			this.AddToCollection<T>(fieldName, currentValue, () => new RadListViewGreaterThenOrEqualToFilterExpression<T>());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCE8 RID: 64744 RVA: 0x0038E525 File Offset: 0x0038C725
		public RadListViewFilterExpressionLogicalBuilder Contains(string fieldName, string currentValue)
		{
			this.AddToCollection<string>(fieldName, currentValue, () => new RadListViewContainsFilterExpression());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCE9 RID: 64745 RVA: 0x0038E559 File Offset: 0x0038C759
		public RadListViewFilterExpressionLogicalBuilder StartsWith(string fieldName, string currentValue)
		{
			this.AddToCollection<string>(fieldName, currentValue, () => new RadListViewStartsWithFilterExpression());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCEA RID: 64746 RVA: 0x0038E58D File Offset: 0x0038C78D
		public RadListViewFilterExpressionLogicalBuilder EndsWith(string fieldName, string currentValue)
		{
			this.AddToCollection<string>(fieldName, currentValue, () => new RadListViewEndsWithFilterExpression());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCEB RID: 64747 RVA: 0x0038E5C1 File Offset: 0x0038C7C1
		public RadListViewFilterExpressionLogicalBuilder LessThan<T>(string fieldName, T currentValue)
		{
			this.AddToCollection<T>(fieldName, currentValue, () => new RadListViewLessThanFilterExpression<T>());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCEC RID: 64748 RVA: 0x0038E5E4 File Offset: 0x0038C7E4
		public RadListViewFilterExpressionLogicalBuilder LessThanOrEqualTo<T>(string fieldName, T currentValue)
		{
			this.AddToCollection<T>(fieldName, currentValue, () => new RadListViewLessThanOrEqualToFilterExpression<T>());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCED RID: 64749 RVA: 0x0038E607 File Offset: 0x0038C807
		public RadListViewFilterExpressionLogicalBuilder IsNull(string fieldName)
		{
			this.AddNonValueExpressionToCollection(fieldName, () => new RadListViewIsNullFilterExpression());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCEE RID: 64750 RVA: 0x0038E63A File Offset: 0x0038C83A
		public RadListViewFilterExpressionLogicalBuilder IsNotNull(string fieldName)
		{
			this.AddNonValueExpressionToCollection(fieldName, () => new RadListViewIsNotNullFilterExpression());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCEF RID: 64751 RVA: 0x0038E66D File Offset: 0x0038C86D
		public RadListViewFilterExpressionLogicalBuilder IsEmpty(string fieldName)
		{
			this.AddNonValueExpressionToCollection(fieldName, () => new RadListViewIsEmptyFilterExpression());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCF0 RID: 64752 RVA: 0x0038E6A0 File Offset: 0x0038C8A0
		public RadListViewFilterExpressionLogicalBuilder IsNotEmpty(string fieldName)
		{
			this.AddNonValueExpressionToCollection(fieldName, () => new RadListViewIsNotEmptyFilterExpression());
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCF1 RID: 64753 RVA: 0x0038E6DC File Offset: 0x0038C8DC
		public RadListViewFilterExpressionLogicalBuilder Group(Action<RadListViewFilterExpressionFluentBuilder> groupBuilder)
		{
			this.ValidateBuildState();
			RadListViewGroupFilterExpression groupExpression = new RadListViewGroupFilterExpression();
			this.AddNonValueExpressionToCollection("group", () => groupExpression);
			RadListViewFilterExpressionFluentBuilder.GroupBuilder groupBuilder2 = new RadListViewFilterExpressionFluentBuilder.GroupBuilder(groupExpression);
			groupBuilder(groupBuilder2);
			if (!groupBuilder2.IsBuild)
			{
				groupBuilder2.Build();
			}
			return new RadListViewFilterExpressionLogicalBuilder(this);
		}

		// Token: 0x0600FCF2 RID: 64754 RVA: 0x0038E73E File Offset: 0x0038C93E
		public void Build()
		{
			this.ValidateBuildState();
			this.IsBuild = true;
			this._collection.Add(this._group);
		}

		// Token: 0x0600FCF3 RID: 64755 RVA: 0x0038E75E File Offset: 0x0038C95E
		private void ValidateBuildState()
		{
			if (this.IsBuild)
			{
				throw new InvalidOperationException("Filter expression are already build!");
			}
		}

		// Token: 0x0600FCF4 RID: 64756 RVA: 0x0038E773 File Offset: 0x0038C973
		protected void ValidateFieldName(string fieldName)
		{
			this.ValidateBuildState();
			if (string.IsNullOrEmpty(fieldName))
			{
				throw new ArgumentOutOfRangeException("FieldName cannot be null or empty.");
			}
		}

		// Token: 0x0600FCF5 RID: 64757 RVA: 0x0038E78E File Offset: 0x0038C98E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600FCF6 RID: 64758 RVA: 0x0038E797 File Offset: 0x0038C997
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600FCF7 RID: 64759 RVA: 0x0038E79F File Offset: 0x0038C99F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600FCF8 RID: 64760 RVA: 0x0038E7A7 File Offset: 0x0038C9A7
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "GetType", Justification = "This should not be visible in auto complete list of VS, distracts when writing fluent syntax.")]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "In that case it is an issue of the .NET Framework itself")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040047D1 RID: 18385
		private readonly IList<RadListViewFilterExpression> _collection;

		// Token: 0x040047D2 RID: 18386
		private RadListViewGroupFilterExpression _group;

		// Token: 0x02001983 RID: 6531
		private class GroupBuilder : RadListViewFilterExpressionFluentBuilder
		{
			// Token: 0x0600FD06 RID: 64774 RVA: 0x0038E7AF File Offset: 0x0038C9AF
			public GroupBuilder(RadListViewGroupFilterExpression groupExpression) : base(groupExpression.Expressions)
			{
			}
		}
	}
}
