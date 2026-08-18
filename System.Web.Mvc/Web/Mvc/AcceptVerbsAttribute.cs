using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200017C RID: 380
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class AcceptVerbsAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x0001BFBB File Offset: 0x0001A1BB
		public AcceptVerbsAttribute(HttpVerbs verbs) : this(AcceptVerbsAttribute.EnumToArray(verbs))
		{
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001BFC9 File Offset: 0x0001A1C9
		public AcceptVerbsAttribute(params string[] verbs)
		{
			if (verbs == null || verbs.Length == 0)
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "verbs");
			}
			this.Verbs = new ReadOnlyCollection<string>(verbs);
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0001BFF5 File Offset: 0x0001A1F5
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0001BFFD File Offset: 0x0001A1FD
		public ICollection<string> Verbs { get; private set; }

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001C006 File Offset: 0x0001A206
		private static void AddEntryToList(HttpVerbs verbs, HttpVerbs match, List<string> verbList, string entryText)
		{
			if ((verbs & match) != (HttpVerbs)0)
			{
				verbList.Add(entryText);
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001C014 File Offset: 0x0001A214
		internal static string[] EnumToArray(HttpVerbs verbs)
		{
			List<string> list = new List<string>();
			AcceptVerbsAttribute.AddEntryToList(verbs, HttpVerbs.Get, list, "GET");
			AcceptVerbsAttribute.AddEntryToList(verbs, HttpVerbs.Post, list, "POST");
			AcceptVerbsAttribute.AddEntryToList(verbs, HttpVerbs.Put, list, "PUT");
			AcceptVerbsAttribute.AddEntryToList(verbs, HttpVerbs.Delete, list, "DELETE");
			AcceptVerbsAttribute.AddEntryToList(verbs, HttpVerbs.Head, list, "HEAD");
			AcceptVerbsAttribute.AddEntryToList(verbs, HttpVerbs.Patch, list, "PATCH");
			AcceptVerbsAttribute.AddEntryToList(verbs, HttpVerbs.Options, list, "OPTIONS");
			return list.ToArray();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001C08C File Offset: 0x0001A28C
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			string httpMethodOverride = controllerContext.HttpContext.Request.GetHttpMethodOverride();
			return this.Verbs.Contains(httpMethodOverride, StringComparer.OrdinalIgnoreCase);
		}
	}
}
