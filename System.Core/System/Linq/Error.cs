using System;

namespace System.Linq
{
	// Token: 0x0200016A RID: 362
	internal static class Error
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002E3F9 File Offset: 0x0002C5F9
		internal static Exception ArgumentArrayHasTooManyElements(object p0)
		{
			return new ArgumentException(Strings.ArgumentArrayHasTooManyElements(p0));
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002E406 File Offset: 0x0002C606
		internal static Exception ArgumentNotIEnumerableGeneric(object p0)
		{
			return new ArgumentException(Strings.ArgumentNotIEnumerableGeneric(p0));
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002E413 File Offset: 0x0002C613
		internal static Exception ArgumentNotSequence(object p0)
		{
			return new ArgumentException(Strings.ArgumentNotSequence(p0));
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002E420 File Offset: 0x0002C620
		internal static Exception ArgumentNotValid(object p0)
		{
			return new ArgumentException(Strings.ArgumentNotValid(p0));
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002E42D File Offset: 0x0002C62D
		internal static Exception IncompatibleElementTypes()
		{
			return new ArgumentException(Strings.IncompatibleElementTypes);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002E439 File Offset: 0x0002C639
		internal static Exception ArgumentNotLambda(object p0)
		{
			return new ArgumentException(Strings.ArgumentNotLambda(p0));
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002E446 File Offset: 0x0002C646
		internal static Exception MoreThanOneElement()
		{
			return new InvalidOperationException(Strings.MoreThanOneElement);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002E452 File Offset: 0x0002C652
		internal static Exception MoreThanOneMatch()
		{
			return new InvalidOperationException(Strings.MoreThanOneMatch);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002E45E File Offset: 0x0002C65E
		internal static Exception NoArgumentMatchingMethodsInQueryable(object p0)
		{
			return new InvalidOperationException(Strings.NoArgumentMatchingMethodsInQueryable(p0));
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002E46B File Offset: 0x0002C66B
		internal static Exception NoElements()
		{
			return new InvalidOperationException(Strings.NoElements);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002E477 File Offset: 0x0002C677
		internal static Exception NoMatch()
		{
			return new InvalidOperationException(Strings.NoMatch);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002E483 File Offset: 0x0002C683
		internal static Exception NoMethodOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.NoMethodOnType(p0, p1));
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002E491 File Offset: 0x0002C691
		internal static Exception NoMethodOnTypeMatchingArguments(object p0, object p1)
		{
			return new InvalidOperationException(Strings.NoMethodOnTypeMatchingArguments(p0, p1));
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002E49F File Offset: 0x0002C69F
		internal static Exception NoNameMatchingMethodsInQueryable(object p0)
		{
			return new InvalidOperationException(Strings.NoNameMatchingMethodsInQueryable(p0));
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002E4AC File Offset: 0x0002C6AC
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002E4B4 File Offset: 0x0002C6B4
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002E4BC File Offset: 0x0002C6BC
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002E4C3 File Offset: 0x0002C6C3
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
