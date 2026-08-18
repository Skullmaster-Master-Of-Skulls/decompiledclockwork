using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000085 RID: 133
	internal class ReferenceComparer : IComparer<JSVariableField>
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00025438 File Offset: 0x00023638
		private ReferenceComparer()
		{
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00025440 File Offset: 0x00023640
		public int Compare(JSVariableField left, JSVariableField right)
		{
			if (left == right || (left == null && right == null))
			{
				return 0;
			}
			if (left == null)
			{
				return 1;
			}
			if (right == null)
			{
				return -1;
			}
			if ((left.FieldType == FieldType.Argument || left.FieldType == FieldType.CatchError) && (right.FieldType == FieldType.Argument || right.FieldType == FieldType.CatchError))
			{
				int num = left.Position - right.Position;
				if (num == 0)
				{
					num = ReferenceComparer.CompareContext(left.OriginalContext, right.OriginalContext);
					if (num == 0)
					{
						num = string.Compare(left.Name, right.Name, StringComparison.Ordinal);
					}
				}
				return num;
			}
			if (left.FieldType == FieldType.Argument || left.FieldType == FieldType.CatchError)
			{
				return -1;
			}
			if (right.FieldType == FieldType.Argument || right.FieldType == FieldType.CatchError)
			{
				return 1;
			}
			int num2 = right.RefCount - left.RefCount;
			if (num2 == 0)
			{
				num2 = ReferenceComparer.CompareContext(left.OriginalContext, right.OriginalContext);
				if (num2 == 0)
				{
					num2 = string.Compare(left.Name, right.Name, StringComparison.Ordinal);
				}
			}
			return num2;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00025524 File Offset: 0x00023724
		private static int CompareContext(Context left, Context right)
		{
			int num = 0;
			if (left != null && right != null)
			{
				num = left.StartLineNumber - right.StartLineNumber;
				if (num == 0)
				{
					num = left.StartColumn - right.StartColumn;
				}
			}
			else if (left != null)
			{
				num = -1;
			}
			else if (right != null)
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x04000310 RID: 784
		public static readonly IComparer<JSVariableField> Instance = new ReferenceComparer();
	}
}
