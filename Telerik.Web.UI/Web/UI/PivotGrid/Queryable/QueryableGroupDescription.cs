using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200072E RID: 1838
	[DataContract]
	public abstract class QueryableGroupDescription : GroupDescription
	{
		// Token: 0x06004150 RID: 16720 RVA: 0x000CD70B File Offset: 0x000CB90B
		internal QueryableGroupDescription()
		{
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x000CD713 File Offset: 0x000CB913
		[DataMember]
		public Collection<CalculatedItem> CalculatedItems
		{
			get
			{
				if (this.calculatedItems == null)
				{
					this.calculatedItems = new Collection<CalculatedItem>();
				}
				return this.calculatedItems;
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x000CD72E File Offset: 0x000CB92E
		protected internal virtual bool NeedsProcessing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x000CD731 File Offset: 0x000CB931
		protected internal override IEnumerable<object> GetAllNames(IEnumerable<object> uniqueNames, IEnumerable<object> parentGroupNames)
		{
			return uniqueNames.Concat(this.CalculatedItems);
		}

		// Token: 0x06004154 RID: 16724
		protected internal abstract Expression CreateGroupKeyExpression(IEnumerable<Expression> valueExpressions);

		// Token: 0x06004155 RID: 16725
		internal abstract Expression CreateMemberAccessExpression(ParameterExpression itemExpression);

		// Token: 0x06004156 RID: 16726
		protected internal abstract IEnumerable<Expression> CreateGroupKeyValuesExpressions(ParameterExpression itemExpression);

		// Token: 0x06004157 RID: 16727 RVA: 0x000CD73F File Offset: 0x000CB93F
		internal virtual object ProcessGroupItem(object data)
		{
			return data;
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x000CD742 File Offset: 0x000CB942
		internal override bool RequiresRefreshForDistinct()
		{
			return false;
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x000CD748 File Offset: 0x000CB948
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			QueryableGroupDescription queryableGroupDescription = source as QueryableGroupDescription;
			if (queryableGroupDescription != null)
			{
				this.CalculatedItems.Clear();
				foreach (CalculatedItem item in queryableGroupDescription.CalculatedItems)
				{
					this.calculatedItems.Add(item);
				}
			}
		}

		// Token: 0x0400114A RID: 4426
		private Collection<CalculatedItem> calculatedItems;
	}
}
