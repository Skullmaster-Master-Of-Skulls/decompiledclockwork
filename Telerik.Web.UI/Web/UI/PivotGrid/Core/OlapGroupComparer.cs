using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006E3 RID: 1763
	[DataContract]
	public sealed class OlapGroupComparer : GroupComparer
	{
		// Token: 0x06003EE7 RID: 16103 RVA: 0x000C831B File Offset: 0x000C651B
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapGroupComparer();
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x000C8322 File Offset: 0x000C6522
		protected override void CloneCore(Cloneable source)
		{
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x000C8324 File Offset: 0x000C6524
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
			OlapGroupName olapGroupName = left.Name as OlapGroupName;
			OlapGroupName olapGroupName2 = right.Name as OlapGroupName;
			if (olapGroupName == null && olapGroupName2 == null)
			{
				return 0;
			}
			if (olapGroupName != null)
			{
				if (olapGroupName2 == null)
				{
					return -1;
				}
				if (olapGroupName.GetType() == olapGroupName2.GetType())
				{
					if ((olapGroupName.SortKeys == null || olapGroupName.SortKeys.Count == 0) && (olapGroupName2.SortKeys == null || olapGroupName2.SortKeys.Count == 0))
					{
						return olapGroupName.CompareTo(olapGroupName2);
					}
					if (left.Level == right.Level)
					{
						return OlapGroupComparer.CompareLeftToRightSortKeys(olapGroupName.SortKeys, olapGroupName2.SortKeys);
					}
					return left.Level.CompareTo(right.Level);
				}
			}
			if (olapGroupName2 != null)
			{
				if (olapGroupName == null)
				{
					return 1;
				}
				if (olapGroupName.GetType() == olapGroupName2.GetType())
				{
					if ((olapGroupName.SortKeys == null || olapGroupName.SortKeys.Count == 0) && (olapGroupName2.SortKeys == null || olapGroupName2.SortKeys.Count == 0))
					{
						return -olapGroupName2.CompareTo(olapGroupName);
					}
					if (left.Level == right.Level)
					{
						return OlapGroupComparer.CompareRightToLeftSortKeys(olapGroupName.SortKeys, olapGroupName2.SortKeys);
					}
					return -right.Level.CompareTo(left.Level);
				}
			}
			return 0;
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x000C8498 File Offset: 0x000C6698
		private static int CompareLeftToRightSortKeys(IList<string> leftSortKeys, IList<string> rightSortKeys)
		{
			int num = (leftSortKeys.Count < rightSortKeys.Count) ? leftSortKeys.Count : rightSortKeys.Count;
			int num2 = 0;
			int num3 = 0;
			while (num3 < num && num2 == 0)
			{
				string text = leftSortKeys[num3];
				string text2 = rightSortKeys[num3];
				if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
				{
					num2 = 0;
					break;
				}
				if (string.IsNullOrEmpty(text))
				{
					num2 = 1;
					break;
				}
				if (string.IsNullOrEmpty(text2))
				{
					num2 = -1;
					break;
				}
				long num4;
				long value;
				decimal num5;
				decimal value2;
				if (long.TryParse(text.ToString(), out num4) && long.TryParse(text2.ToString(), out value))
				{
					num2 = num4.CompareTo(value);
				}
				else if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out num5) && decimal.TryParse(text2, NumberStyles.Number, CultureInfo.InvariantCulture, out value2))
				{
					num2 = num5.CompareTo(value2);
				}
				else
				{
					num2 = string.Compare(text, text2, StringComparison.Ordinal);
				}
				num3++;
			}
			return num2;
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x000C8590 File Offset: 0x000C6790
		private static int CompareRightToLeftSortKeys(IList<string> leftSortKeys, IList<string> rightSortKeys)
		{
			int num = (leftSortKeys.Count < rightSortKeys.Count) ? leftSortKeys.Count : rightSortKeys.Count;
			int num2 = 0;
			int num3 = 0;
			while (num3 < num && num2 == 0)
			{
				string text = leftSortKeys[num3];
				string text2 = rightSortKeys[num3];
				if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
				{
					num2 = 0;
					break;
				}
				if (string.IsNullOrEmpty(text))
				{
					num2 = -1;
					break;
				}
				if (string.IsNullOrEmpty(text2))
				{
					num2 = 1;
					break;
				}
				long value;
				long num4;
				decimal value2;
				decimal num5;
				if (long.TryParse(text.ToString(), out value) && long.TryParse(text2.ToString(), out num4))
				{
					num2 = -num4.CompareTo(value);
				}
				else if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value2) && decimal.TryParse(text2, NumberStyles.Number, CultureInfo.InvariantCulture, out num5))
				{
					num2 = -num5.CompareTo(value2);
				}
				else
				{
					num2 = -string.Compare(text2, text, StringComparison.Ordinal);
				}
				num3++;
			}
			return num2;
		}
	}
}
