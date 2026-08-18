using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x020000D6 RID: 214
	internal static class ContractUtils
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00015A1B File Offset: 0x00013C1B
		internal static Exception Unreachable
		{
			get
			{
				return new InvalidOperationException("Code supposed to be unreachable");
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00015A27 File Offset: 0x00013C27
		internal static void Requires(bool precondition)
		{
			if (!precondition)
			{
				throw new ArgumentException(Strings.MethodPreconditionViolated);
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00015A37 File Offset: 0x00013C37
		internal static void Requires(bool precondition, string paramName)
		{
			if (!precondition)
			{
				throw new ArgumentException(Strings.InvalidArgumentValue, paramName);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00015A48 File Offset: 0x00013C48
		internal static void RequiresNotNull(object value, string paramName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00015A54 File Offset: 0x00013C54
		internal static void RequiresNotEmpty<T>(ICollection<T> collection, string paramName)
		{
			ContractUtils.RequiresNotNull(collection, paramName);
			if (collection.Count == 0)
			{
				throw new ArgumentException(Strings.NonEmptyCollectionRequired, paramName);
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00015A71 File Offset: 0x00013C71
		internal static void RequiresArrayRange<T>(IList<T> array, int offset, int count, string offsetName, string countName)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(countName);
			}
			if (offset < 0 || array.Count - offset < count)
			{
				throw new ArgumentOutOfRangeException(offsetName);
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00015A98 File Offset: 0x00013C98
		internal static void RequiresNotNullItems<T>(IList<T> array, string arrayName)
		{
			ContractUtils.RequiresNotNull(array, arrayName);
			for (int i = 0; i < array.Count; i++)
			{
				if (array[i] == null)
				{
					throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "{0}[{1}]", new object[]
					{
						arrayName,
						i
					}));
				}
			}
		}
	}
}
