using System;
using System.Data.Entity.Core;
using System.Security;
using System.Threading;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020002D2 RID: 722
	internal static class ExceptionExtensions
	{
		// Token: 0x0600195C RID: 6492 RVA: 0x0007E898 File Offset: 0x0007CA98
		public static bool IsCatchableExceptionType(this Exception e)
		{
			Type type = e.GetType();
			return type != typeof(StackOverflowException) && type != typeof(OutOfMemoryException) && type != typeof(ThreadAbortException) && type != typeof(NullReferenceException) && type != typeof(AccessViolationException) && !typeof(SecurityException).IsAssignableFrom(type);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0007E91C File Offset: 0x0007CB1C
		public static bool IsCatchableEntityExceptionType(this Exception e)
		{
			Type type = e.GetType();
			return e.IsCatchableExceptionType() && type != typeof(EntityCommandExecutionException) && type != typeof(EntityCommandCompilationException) && type != typeof(EntitySqlException);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0007E96E File Offset: 0x0007CB6E
		public static bool RequiresContext(this Exception e)
		{
			return e.IsCatchableExceptionType() && !(e is UpdateException) && !(e is ProviderIncompatibleException);
		}
	}
}
