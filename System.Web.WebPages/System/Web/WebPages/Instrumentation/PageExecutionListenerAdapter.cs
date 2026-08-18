using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.WebPages.Instrumentation
{
	// Token: 0x0200003E RID: 62
	[GeneratedCode("Microsoft.Web.CodeGen.DynamicCallerGenerator", "1.0.0.0")]
	internal class PageExecutionListenerAdapter
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00005FCC File Offset: 0x000041CC
		internal void BeginContext(PageExecutionContextAdapter context)
		{
			if (PageExecutionListenerAdapter.<BeginContext>o__SiteContainer0.<>p__Site1 == null)
			{
				PageExecutionListenerAdapter.<BeginContext>o__SiteContainer0.<>p__Site1 = CallSite<Action<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "BeginContext", null, typeof(PageExecutionListenerAdapter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PageExecutionListenerAdapter.<BeginContext>o__SiteContainer0.<>p__Site1.Target(PageExecutionListenerAdapter.<BeginContext>o__SiteContainer0.<>p__Site1, this.Adaptee, context.Adaptee);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006040 File Offset: 0x00004240
		internal void EndContext(PageExecutionContextAdapter context)
		{
			if (PageExecutionListenerAdapter.<EndContext>o__SiteContainer2.<>p__Site3 == null)
			{
				PageExecutionListenerAdapter.<EndContext>o__SiteContainer2.<>p__Site3 = CallSite<Action<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "EndContext", null, typeof(PageExecutionListenerAdapter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PageExecutionListenerAdapter.<EndContext>o__SiteContainer2.<>p__Site3.Target(PageExecutionListenerAdapter.<EndContext>o__SiteContainer2.<>p__Site3, this.Adaptee, context.Adaptee);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000060B4 File Offset: 0x000042B4
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000060BC File Offset: 0x000042BC
		[Dynamic]
		internal dynamic Adaptee { [return: Dynamic] get; [param: Dynamic] private set; }

		// Token: 0x060001B9 RID: 441 RVA: 0x000060C5 File Offset: 0x000042C5
		internal PageExecutionListenerAdapter(object existing)
		{
			this.Adaptee = existing;
		}

		// Token: 0x0400008D RID: 141
		private static readonly Type _TargetType = typeof(HttpContext).Assembly.GetType("System.Web.Instrumentation.PageExecutionListener");

		// Token: 0x020000AB RID: 171
		[CompilerGenerated]
		private static class <BeginContext>o__SiteContainer0
		{
			// Token: 0x0400017D RID: 381
			public static CallSite<Action<CallSite, object, object>> <>p__Site1;
		}

		// Token: 0x020000AC RID: 172
		[CompilerGenerated]
		private static class <EndContext>o__SiteContainer2
		{
			// Token: 0x0400017E RID: 382
			public static CallSite<Action<CallSite, object, object>> <>p__Site3;
		}
	}
}
