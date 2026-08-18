using System;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020002CF RID: 719
	internal static class BoolExtensions
	{
		// Token: 0x06001956 RID: 6486 RVA: 0x0007E624 File Offset: 0x0007C824
		internal static bool? Not(this bool? operand)
		{
			if (operand == null)
			{
				return null;
			}
			return new bool?(!operand.Value);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0007E654 File Offset: 0x0007C854
		internal static bool? And(this bool? left, bool? right)
		{
			bool? result;
			if (left != null && right != null)
			{
				result = new bool?(left.Value && right.Value);
			}
			else if (left == null && right == null)
			{
				result = null;
			}
			else if (left != null)
			{
				result = (left.Value ? null : new bool?(false));
			}
			else
			{
				result = (right.Value ? null : new bool?(false));
			}
			return result;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0007E6F0 File Offset: 0x0007C8F0
		internal static bool? Or(this bool? left, bool? right)
		{
			bool? result;
			if (left != null && right != null)
			{
				result = new bool?(left.Value || right.Value);
			}
			else if (left == null && right == null)
			{
				result = null;
			}
			else if (left != null)
			{
				result = (left.Value ? new bool?(true) : null);
			}
			else
			{
				result = (right.Value ? new bool?(true) : null);
			}
			return result;
		}
	}
}
