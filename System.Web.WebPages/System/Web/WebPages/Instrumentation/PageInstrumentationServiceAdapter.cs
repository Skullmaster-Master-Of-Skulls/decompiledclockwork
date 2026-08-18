using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.WebPages.Instrumentation
{
	// Token: 0x0200003F RID: 63
	internal class PageInstrumentationServiceAdapter
	{
		// Token: 0x060001BB RID: 443 RVA: 0x000060F4 File Offset: 0x000042F4
		internal PageInstrumentationServiceAdapter()
		{
			this.Adaptee = PageInstrumentationServiceAdapter._CallSite_ctor_2.Site();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000610C File Offset: 0x0000430C
		internal PageInstrumentationServiceAdapter(object existing)
		{
			this.Adaptee = existing;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006124 File Offset: 0x00004324
		internal IReadOnlyList<PageExecutionListenerAdapter> ExecutionListeners
		{
			get
			{
				if (this._listenerAdapters == null)
				{
					if (PageInstrumentationServiceAdapter.<get_ExecutionListeners>o__SiteContainer0.<>p__Site1 == null)
					{
						PageInstrumentationServiceAdapter.<get_ExecutionListeners>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, IEnumerable<object>>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(IEnumerable<object>), typeof(PageInstrumentationServiceAdapter)));
					}
					Func<CallSite, object, IEnumerable<object>> target = PageInstrumentationServiceAdapter.<get_ExecutionListeners>o__SiteContainer0.<>p__Site1.Target;
					CallSite <>p__Site = PageInstrumentationServiceAdapter.<get_ExecutionListeners>o__SiteContainer0.<>p__Site1;
					if (PageInstrumentationServiceAdapter.<get_ExecutionListeners>o__SiteContainer0.<>p__Site2 == null)
					{
						PageInstrumentationServiceAdapter.<get_ExecutionListeners>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ExecutionListeners", typeof(PageInstrumentationServiceAdapter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					IEnumerable<object> source = target(<>p__Site, PageInstrumentationServiceAdapter.<get_ExecutionListeners>o__SiteContainer0.<>p__Site2.Target(PageInstrumentationServiceAdapter.<get_ExecutionListeners>o__SiteContainer0.<>p__Site2, this.Adaptee));
					this._listenerAdapters = (from listener in source
					select new PageExecutionListenerAdapter(listener)).ToList<PageExecutionListenerAdapter>();
				}
				return this._listenerAdapters;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006202 File Offset: 0x00004402
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000620E File Offset: 0x0000440E
		internal static bool IsEnabled
		{
			get
			{
				return PageInstrumentationServiceAdapter._CallSite_IsEnabled_1.Getter();
			}
			set
			{
				PageInstrumentationServiceAdapter._CallSite_IsEnabled_1.Setter(value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000621B File Offset: 0x0000441B
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00006223 File Offset: 0x00004423
		[Dynamic]
		internal dynamic Adaptee { [return: Dynamic] get; [param: Dynamic] private set; }

		// Token: 0x0400008F RID: 143
		private static readonly Type _targetType = typeof(HttpContext).Assembly.GetType("System.Web.Instrumentation.PageInstrumentationService");

		// Token: 0x04000090 RID: 144
		private IReadOnlyList<PageExecutionListenerAdapter> _listenerAdapters;

		// Token: 0x02000040 RID: 64
		private static class _CallSite_IsEnabled_1
		{
			// Token: 0x060001C4 RID: 452 RVA: 0x00006254 File Offset: 0x00004454
			static _CallSite_IsEnabled_1()
			{
				PropertyInfo propertyInfo = null;
				if (PageInstrumentationServiceAdapter._targetType != null)
				{
					propertyInfo = PageInstrumentationServiceAdapter._targetType.GetProperty("IsEnabled", BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, typeof(bool), Type.EmptyTypes, new ParameterModifier[0]);
				}
				if (propertyInfo != null)
				{
					PageInstrumentationServiceAdapter._CallSite_IsEnabled_1.Getter = Expression.Lambda<Func<bool>>(Expression.Property(null, propertyInfo), new ParameterExpression[0]).Compile();
					ParameterExpression parameterExpression = Expression.Parameter(typeof(bool));
					PageInstrumentationServiceAdapter._CallSite_IsEnabled_1.Setter = Expression.Lambda<Action<bool>>(Expression.Assign(Expression.Property(null, propertyInfo), parameterExpression), new ParameterExpression[]
					{
						parameterExpression
					}).Compile();
					return;
				}
				PageInstrumentationServiceAdapter._CallSite_IsEnabled_1.Getter = (() => false);
				PageInstrumentationServiceAdapter._CallSite_IsEnabled_1.Setter = delegate(bool _)
				{
				};
			}

			// Token: 0x04000093 RID: 147
			public static Func<bool> Getter;

			// Token: 0x04000094 RID: 148
			public static Action<bool> Setter;
		}

		// Token: 0x02000041 RID: 65
		private static class _CallSite_ctor_2
		{
			// Token: 0x060001C7 RID: 455 RVA: 0x00006344 File Offset: 0x00004544
			static _CallSite_ctor_2()
			{
				if (PageInstrumentationServiceAdapter._targetType != null)
				{
					PageInstrumentationServiceAdapter._CallSite_ctor_2.Site = Expression.Lambda<Func<object>>(Expression.New(PageInstrumentationServiceAdapter._targetType.GetConstructor(new Type[0])), new ParameterExpression[0]).Compile();
					return;
				}
				PageInstrumentationServiceAdapter._CallSite_ctor_2.Site = (() => null);
			}

			// Token: 0x04000097 RID: 151
			public static Func<object> Site;
		}

		// Token: 0x020000AD RID: 173
		[CompilerGenerated]
		private static class <get_ExecutionListeners>o__SiteContainer0
		{
			// Token: 0x0400017F RID: 383
			public static CallSite<Func<CallSite, object, IEnumerable<object>>> <>p__Site1;

			// Token: 0x04000180 RID: 384
			public static CallSite<Func<CallSite, object, object>> <>p__Site2;
		}
	}
}
