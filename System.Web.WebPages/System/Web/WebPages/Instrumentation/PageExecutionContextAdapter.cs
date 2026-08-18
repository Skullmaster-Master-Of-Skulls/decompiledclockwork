using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.WebPages.Instrumentation
{
	// Token: 0x0200003C RID: 60
	[GeneratedCode("Microsoft.Web.CodeGen.DynamicCallerGenerator", "1.0.0.0")]
	internal class PageExecutionContextAdapter
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005A0C File Offset: 0x00003C0C
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00005AAC File Offset: 0x00003CAC
		internal bool IsLiteral
		{
			get
			{
				if (PageExecutionContextAdapter.<get_IsLiteral>o__SiteContainer0.<>p__Site1 == null)
				{
					PageExecutionContextAdapter.<get_IsLiteral>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(PageExecutionContextAdapter)));
				}
				Func<CallSite, object, bool> target = PageExecutionContextAdapter.<get_IsLiteral>o__SiteContainer0.<>p__Site1.Target;
				CallSite <>p__Site = PageExecutionContextAdapter.<get_IsLiteral>o__SiteContainer0.<>p__Site1;
				if (PageExecutionContextAdapter.<get_IsLiteral>o__SiteContainer0.<>p__Site2 == null)
				{
					PageExecutionContextAdapter.<get_IsLiteral>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "IsLiteral", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				return target(<>p__Site, PageExecutionContextAdapter.<get_IsLiteral>o__SiteContainer0.<>p__Site2.Target(PageExecutionContextAdapter.<get_IsLiteral>o__SiteContainer0.<>p__Site2, this.Adaptee));
			}
			set
			{
				if (PageExecutionContextAdapter.<set_IsLiteral>o__SiteContainer3.<>p__Site4 == null)
				{
					PageExecutionContextAdapter.<set_IsLiteral>o__SiteContainer3.<>p__Site4 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "IsLiteral", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				PageExecutionContextAdapter.<set_IsLiteral>o__SiteContainer3.<>p__Site4.Target(PageExecutionContextAdapter.<set_IsLiteral>o__SiteContainer3.<>p__Site4, this.Adaptee, value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005B18 File Offset: 0x00003D18
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00005BB8 File Offset: 0x00003DB8
		internal int Length
		{
			get
			{
				if (PageExecutionContextAdapter.<get_Length>o__SiteContainer5.<>p__Site6 == null)
				{
					PageExecutionContextAdapter.<get_Length>o__SiteContainer5.<>p__Site6 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(PageExecutionContextAdapter)));
				}
				Func<CallSite, object, int> target = PageExecutionContextAdapter.<get_Length>o__SiteContainer5.<>p__Site6.Target;
				CallSite <>p__Site = PageExecutionContextAdapter.<get_Length>o__SiteContainer5.<>p__Site6;
				if (PageExecutionContextAdapter.<get_Length>o__SiteContainer5.<>p__Site7 == null)
				{
					PageExecutionContextAdapter.<get_Length>o__SiteContainer5.<>p__Site7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Length", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				return target(<>p__Site, PageExecutionContextAdapter.<get_Length>o__SiteContainer5.<>p__Site7.Target(PageExecutionContextAdapter.<get_Length>o__SiteContainer5.<>p__Site7, this.Adaptee));
			}
			set
			{
				if (PageExecutionContextAdapter.<set_Length>o__SiteContainer8.<>p__Site9 == null)
				{
					PageExecutionContextAdapter.<set_Length>o__SiteContainer8.<>p__Site9 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Length", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				PageExecutionContextAdapter.<set_Length>o__SiteContainer8.<>p__Site9.Target(PageExecutionContextAdapter.<set_Length>o__SiteContainer8.<>p__Site9, this.Adaptee, value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005C24 File Offset: 0x00003E24
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00005CC4 File Offset: 0x00003EC4
		internal int StartPosition
		{
			get
			{
				if (PageExecutionContextAdapter.<get_StartPosition>o__SiteContainera.<>p__Siteb == null)
				{
					PageExecutionContextAdapter.<get_StartPosition>o__SiteContainera.<>p__Siteb = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(PageExecutionContextAdapter)));
				}
				Func<CallSite, object, int> target = PageExecutionContextAdapter.<get_StartPosition>o__SiteContainera.<>p__Siteb.Target;
				CallSite <>p__Siteb = PageExecutionContextAdapter.<get_StartPosition>o__SiteContainera.<>p__Siteb;
				if (PageExecutionContextAdapter.<get_StartPosition>o__SiteContainera.<>p__Sitec == null)
				{
					PageExecutionContextAdapter.<get_StartPosition>o__SiteContainera.<>p__Sitec = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "StartPosition", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				return target(<>p__Siteb, PageExecutionContextAdapter.<get_StartPosition>o__SiteContainera.<>p__Sitec.Target(PageExecutionContextAdapter.<get_StartPosition>o__SiteContainera.<>p__Sitec, this.Adaptee));
			}
			set
			{
				if (PageExecutionContextAdapter.<set_StartPosition>o__SiteContainerd.<>p__Sitee == null)
				{
					PageExecutionContextAdapter.<set_StartPosition>o__SiteContainerd.<>p__Sitee = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "StartPosition", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				PageExecutionContextAdapter.<set_StartPosition>o__SiteContainerd.<>p__Sitee.Target(PageExecutionContextAdapter.<set_StartPosition>o__SiteContainerd.<>p__Sitee, this.Adaptee, value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00005D30 File Offset: 0x00003F30
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00005DD0 File Offset: 0x00003FD0
		internal TextWriter TextWriter
		{
			get
			{
				if (PageExecutionContextAdapter.<get_TextWriter>o__SiteContainerf.<>p__Site10 == null)
				{
					PageExecutionContextAdapter.<get_TextWriter>o__SiteContainerf.<>p__Site10 = CallSite<Func<CallSite, object, TextWriter>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(TextWriter), typeof(PageExecutionContextAdapter)));
				}
				Func<CallSite, object, TextWriter> target = PageExecutionContextAdapter.<get_TextWriter>o__SiteContainerf.<>p__Site10.Target;
				CallSite <>p__Site = PageExecutionContextAdapter.<get_TextWriter>o__SiteContainerf.<>p__Site10;
				if (PageExecutionContextAdapter.<get_TextWriter>o__SiteContainerf.<>p__Site11 == null)
				{
					PageExecutionContextAdapter.<get_TextWriter>o__SiteContainerf.<>p__Site11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "TextWriter", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				return target(<>p__Site, PageExecutionContextAdapter.<get_TextWriter>o__SiteContainerf.<>p__Site11.Target(PageExecutionContextAdapter.<get_TextWriter>o__SiteContainerf.<>p__Site11, this.Adaptee));
			}
			set
			{
				if (PageExecutionContextAdapter.<set_TextWriter>o__SiteContainer12.<>p__Site13 == null)
				{
					PageExecutionContextAdapter.<set_TextWriter>o__SiteContainer12.<>p__Site13 = CallSite<Func<CallSite, object, TextWriter, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TextWriter", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				PageExecutionContextAdapter.<set_TextWriter>o__SiteContainer12.<>p__Site13.Target(PageExecutionContextAdapter.<set_TextWriter>o__SiteContainer12.<>p__Site13, this.Adaptee, value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005E3C File Offset: 0x0000403C
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00005EDC File Offset: 0x000040DC
		internal string VirtualPath
		{
			get
			{
				if (PageExecutionContextAdapter.<get_VirtualPath>o__SiteContainer14.<>p__Site15 == null)
				{
					PageExecutionContextAdapter.<get_VirtualPath>o__SiteContainer14.<>p__Site15 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(PageExecutionContextAdapter)));
				}
				Func<CallSite, object, string> target = PageExecutionContextAdapter.<get_VirtualPath>o__SiteContainer14.<>p__Site15.Target;
				CallSite <>p__Site = PageExecutionContextAdapter.<get_VirtualPath>o__SiteContainer14.<>p__Site15;
				if (PageExecutionContextAdapter.<get_VirtualPath>o__SiteContainer14.<>p__Site16 == null)
				{
					PageExecutionContextAdapter.<get_VirtualPath>o__SiteContainer14.<>p__Site16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "VirtualPath", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				return target(<>p__Site, PageExecutionContextAdapter.<get_VirtualPath>o__SiteContainer14.<>p__Site16.Target(PageExecutionContextAdapter.<get_VirtualPath>o__SiteContainer14.<>p__Site16, this.Adaptee));
			}
			set
			{
				if (PageExecutionContextAdapter.<set_VirtualPath>o__SiteContainer17.<>p__Site18 == null)
				{
					PageExecutionContextAdapter.<set_VirtualPath>o__SiteContainer17.<>p__Site18 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "VirtualPath", typeof(PageExecutionContextAdapter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				PageExecutionContextAdapter.<set_VirtualPath>o__SiteContainer17.<>p__Site18.Target(PageExecutionContextAdapter.<set_VirtualPath>o__SiteContainer17.<>p__Site18, this.Adaptee, value);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005F47 File Offset: 0x00004147
		internal PageExecutionContextAdapter()
		{
			this.Adaptee = PageExecutionContextAdapter._CallSite_ctor_1.Site();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005F5F File Offset: 0x0000415F
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00005F67 File Offset: 0x00004167
		[Dynamic]
		internal dynamic Adaptee { [return: Dynamic] get; [param: Dynamic] private set; }

		// Token: 0x060001B2 RID: 434 RVA: 0x00005F70 File Offset: 0x00004170
		internal PageExecutionContextAdapter(object existing)
		{
			this.Adaptee = existing;
		}

		// Token: 0x0400008A RID: 138
		private static readonly Type _TargetType = typeof(HttpContext).Assembly.GetType("System.Web.Instrumentation.PageExecutionContext");

		// Token: 0x0200003D RID: 61
		private static class _CallSite_ctor_1
		{
			// Token: 0x0400008C RID: 140
			public static Func<object> Site = Expression.Lambda<Func<object>>(Expression.New(PageExecutionContextAdapter._TargetType.GetConstructor(new Type[0])), new ParameterExpression[0]).Compile();
		}

		// Token: 0x020000A1 RID: 161
		[CompilerGenerated]
		private static class <get_IsLiteral>o__SiteContainer0
		{
			// Token: 0x0400016E RID: 366
			public static CallSite<Func<CallSite, object, bool>> <>p__Site1;

			// Token: 0x0400016F RID: 367
			public static CallSite<Func<CallSite, object, object>> <>p__Site2;
		}

		// Token: 0x020000A2 RID: 162
		[CompilerGenerated]
		private static class <set_IsLiteral>o__SiteContainer3
		{
			// Token: 0x04000170 RID: 368
			public static CallSite<Func<CallSite, object, bool, object>> <>p__Site4;
		}

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		private static class <get_Length>o__SiteContainer5
		{
			// Token: 0x04000171 RID: 369
			public static CallSite<Func<CallSite, object, int>> <>p__Site6;

			// Token: 0x04000172 RID: 370
			public static CallSite<Func<CallSite, object, object>> <>p__Site7;
		}

		// Token: 0x020000A4 RID: 164
		[CompilerGenerated]
		private static class <set_Length>o__SiteContainer8
		{
			// Token: 0x04000173 RID: 371
			public static CallSite<Func<CallSite, object, int, object>> <>p__Site9;
		}

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		private static class <get_StartPosition>o__SiteContainera
		{
			// Token: 0x04000174 RID: 372
			public static CallSite<Func<CallSite, object, int>> <>p__Siteb;

			// Token: 0x04000175 RID: 373
			public static CallSite<Func<CallSite, object, object>> <>p__Sitec;
		}

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		private static class <set_StartPosition>o__SiteContainerd
		{
			// Token: 0x04000176 RID: 374
			public static CallSite<Func<CallSite, object, int, object>> <>p__Sitee;
		}

		// Token: 0x020000A7 RID: 167
		[CompilerGenerated]
		private static class <get_TextWriter>o__SiteContainerf
		{
			// Token: 0x04000177 RID: 375
			public static CallSite<Func<CallSite, object, TextWriter>> <>p__Site10;

			// Token: 0x04000178 RID: 376
			public static CallSite<Func<CallSite, object, object>> <>p__Site11;
		}

		// Token: 0x020000A8 RID: 168
		[CompilerGenerated]
		private static class <set_TextWriter>o__SiteContainer12
		{
			// Token: 0x04000179 RID: 377
			public static CallSite<Func<CallSite, object, TextWriter, object>> <>p__Site13;
		}

		// Token: 0x020000A9 RID: 169
		[CompilerGenerated]
		private static class <get_VirtualPath>o__SiteContainer14
		{
			// Token: 0x0400017A RID: 378
			public static CallSite<Func<CallSite, object, string>> <>p__Site15;

			// Token: 0x0400017B RID: 379
			public static CallSite<Func<CallSite, object, object>> <>p__Site16;
		}

		// Token: 0x020000AA RID: 170
		[CompilerGenerated]
		private static class <set_VirtualPath>o__SiteContainer17
		{
			// Token: 0x0400017C RID: 380
			public static CallSite<Func<CallSite, object, string, object>> <>p__Site18;
		}
	}
}
