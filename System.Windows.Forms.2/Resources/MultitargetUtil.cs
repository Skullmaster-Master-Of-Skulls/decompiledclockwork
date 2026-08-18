using System;
using System.Security;
using System.Threading;

namespace System.Resources
{
	// Token: 0x020000EA RID: 234
	internal static class MultitargetUtil
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00008E5C File Offset: 0x0000705C
		public static string GetAssemblyQualifiedName(Type type, Func<Type, string> typeNameConverter)
		{
			string text = null;
			if (type != null)
			{
				if (typeNameConverter != null)
				{
					try
					{
						text = typeNameConverter(type);
					}
					catch (Exception ex)
					{
						if (MultitargetUtil.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = type.AssemblyQualifiedName;
				}
			}
			return text;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008EB0 File Offset: 0x000070B0
		private static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException || ex is SecurityException;
		}
	}
}
