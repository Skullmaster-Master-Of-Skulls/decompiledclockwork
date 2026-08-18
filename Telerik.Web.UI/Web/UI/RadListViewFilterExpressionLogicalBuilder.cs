using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001981 RID: 6529
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadListViewFilterExpressionLogicalBuilder : IHideObjectMembers
	{
		// Token: 0x0600FCD5 RID: 64725 RVA: 0x0038E34E File Offset: 0x0038C54E
		internal RadListViewFilterExpressionLogicalBuilder(RadListViewFilterExpressionFluentBuilder owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600FCD6 RID: 64726 RVA: 0x0038E35D File Offset: 0x0038C55D
		public RadListViewFilterExpressionFluentBuilder And()
		{
			if (this._owner.CurrentGroupOperator() == RadListViewGroupFilterOperator.Or)
			{
				this._owner.WrapInGroup(new RadListViewGroupFilterExpression(RadListViewGroupFilterOperator.And));
			}
			return this._owner;
		}

		// Token: 0x0600FCD7 RID: 64727 RVA: 0x0038E384 File Offset: 0x0038C584
		public RadListViewFilterExpressionFluentBuilder Or()
		{
			if (this._owner.CurrentGroupOperator() == RadListViewGroupFilterOperator.And)
			{
				this._owner.WrapInGroup(new RadListViewGroupFilterExpression(RadListViewGroupFilterOperator.Or));
			}
			return this._owner;
		}

		// Token: 0x0600FCD8 RID: 64728 RVA: 0x0038E3AA File Offset: 0x0038C5AA
		public void Build()
		{
			this._owner.Build();
		}

		// Token: 0x0600FCD9 RID: 64729 RVA: 0x0038E3B7 File Offset: 0x0038C5B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600FCDA RID: 64730 RVA: 0x0038E3C0 File Offset: 0x0038C5C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600FCDB RID: 64731 RVA: 0x0038E3C8 File Offset: 0x0038C5C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600FCDC RID: 64732 RVA: 0x0038E3D0 File Offset: 0x0038C5D0
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "In that case it is an issue of the .NET Framework itself")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "GetType", Justification = "This should not be visible in auto complete list of VS, distracts when writing fluent syntax.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040047D0 RID: 18384
		private RadListViewFilterExpressionFluentBuilder _owner;
	}
}
