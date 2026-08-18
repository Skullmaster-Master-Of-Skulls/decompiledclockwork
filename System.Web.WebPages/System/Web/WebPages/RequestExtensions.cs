using System;

namespace System.Web.WebPages
{
	// Token: 0x02000063 RID: 99
	public static class RequestExtensions
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00009AFC File Offset: 0x00007CFC
		public static bool IsUrlLocalToHost(this HttpRequestBase request, string url)
		{
			return !url.IsEmpty() && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
		}
	}
}
