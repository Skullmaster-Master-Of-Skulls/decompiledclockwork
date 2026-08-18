using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Antlr.Runtime;

namespace WebGrease.Css
{
	// Token: 0x020001A5 RID: 421
	internal static class ErrorHelper
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x0007E3C0 File Offset: 0x0007C5C0
		internal static IEnumerable<string> DedupeCSSErrors(this AggregateException aggEx)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (Exception ex in aggEx.InnerExceptions)
			{
				RecognitionException ex2 = ex as RecognitionException;
				if (ex2 != null)
				{
					string item = string.Format(CultureInfo.InvariantCulture, "({0},{1}): run-time error CSS1000: {2}", new object[]
					{
						ex2.Line,
						ex2.CharPositionInLine,
						ex2.Message
					});
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0007E468 File Offset: 0x0007C668
		internal static IEnumerable<BuildWorkflowException> CreateBuildErrors(this AggregateException aggEx, string fileName)
		{
			return aggEx.InnerExceptions.OfType<RecognitionException>().CreateBuildErrors(fileName);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0007E4C4 File Offset: 0x0007C6C4
		internal static IEnumerable<BuildWorkflowException> CreateBuildErrors(this IEnumerable<RecognitionException> exceptions, string fileName)
		{
			return from ex in (from ex in exceptions
			where ex != null
			select ex).Distinct(new ErrorHelper.ErrorDeduper())
			select new BuildWorkflowException(ex.Message, "CSS", "CSS1000", null, fileName, ex.Line, ex.CharPositionInLine, 0, 0, ex);
		}

		// Token: 0x020001A6 RID: 422
		private class ErrorDeduper : IEqualityComparer<RecognitionException>
		{
			// Token: 0x060015BF RID: 5567 RVA: 0x0007E51C File Offset: 0x0007C71C
			public bool Equals(RecognitionException x, RecognitionException y)
			{
				return x.Line == y.Line && x.CharPositionInLine == y.CharPositionInLine && x.Message == y.Message;
			}

			// Token: 0x060015C0 RID: 5568 RVA: 0x0007E550 File Offset: 0x0007C750
			public int GetHashCode(RecognitionException obj)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
				{
					obj.Line,
					obj.CharPositionInLine,
					obj.Message
				}).GetHashCode();
			}
		}
	}
}
