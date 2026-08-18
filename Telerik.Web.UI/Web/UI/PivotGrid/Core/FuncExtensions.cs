using System;
using System.Reflection;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE0 RID: 3296
	internal static class FuncExtensions
	{
		// Token: 0x17002768 RID: 10088
		// (get) Token: 0x06007B2C RID: 31532 RVA: 0x001C46BC File Offset: 0x001C28BC
		// (set) Token: 0x06007B2D RID: 31533 RVA: 0x001C46C3 File Offset: 0x001C28C3
		public static MethodInfo ToUntypedFuncMethod { get; private set; } = typeof(FuncExtensions).GetMethod("ToUntypedFunc");

		// Token: 0x17002769 RID: 10089
		// (get) Token: 0x06007B2E RID: 31534 RVA: 0x001C46CB File Offset: 0x001C28CB
		// (set) Token: 0x06007B2F RID: 31535 RVA: 0x001C46D2 File Offset: 0x001C28D2
		public static MethodInfo ToUntypedTwoParameterFuncMethod { get; private set; } = typeof(FuncExtensions).GetMethod("ToUntypedTwoParameterFunc");

		// Token: 0x1700276A RID: 10090
		// (get) Token: 0x06007B30 RID: 31536 RVA: 0x001C46DA File Offset: 0x001C28DA
		// (set) Token: 0x06007B31 RID: 31537 RVA: 0x001C46E1 File Offset: 0x001C28E1
		public static MethodInfo ToUntypedBooleanFuncMethod { get; private set; } = typeof(FuncExtensions).GetMethod("ToUntypedBooleanFunc");

		// Token: 0x06007B32 RID: 31538 RVA: 0x001C470C File Offset: 0x001C290C
		public static Func<object, object> ToUntypedFunc<T, TResult>(Func<T, TResult> func)
		{
			return (object item) => func((T)((object)item));
		}

		// Token: 0x06007B33 RID: 31539 RVA: 0x001C4758 File Offset: 0x001C2958
		public static Func<object, object, object> ToUntypedTwoParameterFunc<T1, T2, TResult>(Func<T1, T2, TResult> func)
		{
			return (object t1, object t2) => func((T1)((object)t1), (T2)((object)t2));
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x001C479C File Offset: 0x001C299C
		public static Func<object, bool> ToUntypedBooleanFunc<T>(Func<T, bool> func)
		{
			return (object item) => func((T)((object)item));
		}

		// Token: 0x06007B35 RID: 31541 RVA: 0x001C47E0 File Offset: 0x001C29E0
		public static Func<object, TResult> ToTypedResultFunc<T, TResult>(Func<T, TResult> func)
		{
			return (object item) => func((T)((object)item));
		}
	}
}
