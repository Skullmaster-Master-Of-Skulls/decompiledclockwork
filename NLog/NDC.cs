using System;

namespace NLog
{
	// Token: 0x0200013E RID: 318
	[Obsolete("Use NestedDiagnosticsContext")]
	public static class NDC
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00019B6C File Offset: 0x00017D6C
		public static string TopMessage
		{
			get
			{
				return NestedDiagnosticsContext.TopMessage;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00019B73 File Offset: 0x00017D73
		public static object TopObject
		{
			get
			{
				return NestedDiagnosticsContext.TopObject;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00019B7A File Offset: 0x00017D7A
		public static IDisposable Push(string text)
		{
			return NestedDiagnosticsContext.Push(text);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00019B82 File Offset: 0x00017D82
		public static string Pop()
		{
			return NestedDiagnosticsContext.Pop();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00019B89 File Offset: 0x00017D89
		public static object PopObject()
		{
			return NestedDiagnosticsContext.PopObject();
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00019B90 File Offset: 0x00017D90
		public static void Clear()
		{
			NestedDiagnosticsContext.Clear();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00019B97 File Offset: 0x00017D97
		public static string[] GetAllMessages()
		{
			return NestedDiagnosticsContext.GetAllMessages();
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00019B9E File Offset: 0x00017D9E
		public static object[] GetAllObjects()
		{
			return NestedDiagnosticsContext.GetAllObjects();
		}
	}
}
