using System;

namespace a.b
{
	// Token: 0x02000326 RID: 806
	internal class aq
	{
		// Token: 0x06001D22 RID: 7458 RVA: 0x0007E4EC File Offset: 0x0007D4EC
		public static bool b(object A_0)
		{
			if (A_0 == null)
			{
				return false;
			}
			Type type = A_0.GetType();
			return (type.IsPrimitive || type == aq.e) && (type != aq.a && type != aq.b && type != aq.c) && type != aq.d;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0007E550 File Offset: 0x0007D550
		public static bool a(object A_0)
		{
			return A_0 != null && (A_0 is int || A_0 is uint || A_0 is long || A_0 is ulong || A_0 is sbyte || A_0 is byte || A_0 is short || A_0 is ushort);
		}

		// Token: 0x04001377 RID: 4983
		private static Type a = typeof(bool);

		// Token: 0x04001378 RID: 4984
		private static Type b = typeof(char);

		// Token: 0x04001379 RID: 4985
		private static Type c = typeof(IntPtr);

		// Token: 0x0400137A RID: 4986
		private static Type d = typeof(UIntPtr);

		// Token: 0x0400137B RID: 4987
		private static Type e = typeof(decimal);
	}
}
