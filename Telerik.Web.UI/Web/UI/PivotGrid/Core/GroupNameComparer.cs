using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CD8 RID: 3288
	[DataContract]
	public sealed class GroupNameComparer : GroupComparer
	{
		// Token: 0x06007AD1 RID: 31441 RVA: 0x001C2CA5 File Offset: 0x001C0EA5
		protected override Cloneable CreateInstanceCore()
		{
			return new GroupNameComparer();
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x001C2CAC File Offset: 0x001C0EAC
		protected override void CloneCore(Cloneable source)
		{
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x001C2CB0 File Offset: 0x001C0EB0
		public override int CompareGroups(IAggregateResultProvider results, IGroup left, IGroup right, PivotAxis axis)
		{
			if (left.Name == NullValue.Instance && right.Name == NullValue.Instance)
			{
				return 0;
			}
			if (left.Name == NullValue.Instance)
			{
				return 1;
			}
			if (right.Name == NullValue.Instance)
			{
				return -1;
			}
			object obj = left.Name;
			object obj2 = right.Name;
			CalculatedItem calculatedItem = obj as CalculatedItem;
			CalculatedItem calculatedItem2 = obj2 as CalculatedItem;
			if (calculatedItem != null && calculatedItem2 == null)
			{
				return 1;
			}
			if (calculatedItem == null && calculatedItem2 != null)
			{
				return -1;
			}
			if (calculatedItem != null && calculatedItem2 != null)
			{
				obj = calculatedItem.GroupName;
				obj2 = calculatedItem2.GroupName;
			}
			IComparable comparable = obj as IComparable;
			if (comparable != null)
			{
				if (obj2 == null)
				{
					return -1;
				}
				if (obj.GetType() == obj2.GetType())
				{
					return comparable.CompareTo(obj2);
				}
			}
			IComparable comparable2 = obj2 as IComparable;
			if (comparable2 != null)
			{
				if (obj == null)
				{
					return 1;
				}
				if (obj.GetType() == obj2.GetType())
				{
					return -comparable2.CompareTo(obj);
				}
			}
			return 0;
		}
	}
}
