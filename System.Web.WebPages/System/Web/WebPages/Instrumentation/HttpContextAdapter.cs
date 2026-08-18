using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.WebPages.Instrumentation
{
	// Token: 0x0200003A RID: 58
	[GeneratedCode("Microsoft.Web.CodeGen.DynamicCallerGenerator", "1.0.0.0")]
	internal class HttpContextAdapter
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00005739 File Offset: 0x00003939
		internal static bool IsInstrumentationAvailable
		{
			get
			{
				return HttpContextAdapter._isInstrumentationAvailable;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005740 File Offset: 0x00003940
		internal PageInstrumentationServiceAdapter PageInstrumentation
		{
			get
			{
				if (HttpContextAdapter.<get_PageInstrumentation>o__SiteContainer0.<>p__Site1 == null)
				{
					HttpContextAdapter.<get_PageInstrumentation>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "PageInstrumentation", typeof(HttpContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				return new PageInstrumentationServiceAdapter(HttpContextAdapter.<get_PageInstrumentation>o__SiteContainer0.<>p__Site1.Target(HttpContextAdapter.<get_PageInstrumentation>o__SiteContainer0.<>p__Site1, this.Adaptee));
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000057A4 File Offset: 0x000039A4
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000057AC File Offset: 0x000039AC
		[Dynamic]
		internal dynamic Adaptee { [return: Dynamic] get; [param: Dynamic] private set; }

		// Token: 0x06000196 RID: 406 RVA: 0x000057B5 File Offset: 0x000039B5
		internal HttpContextAdapter(object existing)
		{
			this.Adaptee = existing;
		}

		// Token: 0x04000081 RID: 129
		private static readonly bool _isInstrumentationAvailable = typeof(HttpContext).GetProperty("PageInstrumentation", BindingFlags.Instance | BindingFlags.Public) != null;

		// Token: 0x04000082 RID: 130
		private static readonly Type _TargetType = typeof(HttpContext);

		// Token: 0x020000A0 RID: 160
		[CompilerGenerated]
		private static class <get_PageInstrumentation>o__SiteContainer0
		{
			// Token: 0x0400016D RID: 365
			public static CallSite<Func<CallSite, object, object>> <>p__Site1;
		}
	}
}
