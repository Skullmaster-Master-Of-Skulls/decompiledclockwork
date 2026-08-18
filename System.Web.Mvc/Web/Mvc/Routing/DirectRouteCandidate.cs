using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc.Async;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000049 RID: 73
	internal class DirectRouteCandidate
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000073EC File Offset: 0x000055EC
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000073F4 File Offset: 0x000055F4
		public ActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000073FD File Offset: 0x000055FD
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00007405 File Offset: 0x00005605
		public IEnumerable<ActionNameSelector> ActionNameSelectors { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000740E File Offset: 0x0000560E
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00007416 File Offset: 0x00005616
		public IEnumerable<ActionSelector> ActionSelectors { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000741F File Offset: 0x0000561F
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00007427 File Offset: 0x00005627
		public ControllerDescriptor ControllerDescriptor { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007430 File Offset: 0x00005630
		public bool HasActionNameSelectors
		{
			get
			{
				return this.ActionNameSelectors != null && this.ActionNameSelectors.Any<ActionNameSelector>();
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00007447 File Offset: 0x00005647
		public bool HasActionSelectors
		{
			get
			{
				return this.ActionSelectors != null && this.ActionSelectors.Any<ActionSelector>();
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000745E File Offset: 0x0000565E
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00007466 File Offset: 0x00005666
		public int Order { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000746F File Offset: 0x0000566F
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00007477 File Offset: 0x00005677
		public decimal Precedence { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007480 File Offset: 0x00005680
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00007488 File Offset: 0x00005688
		public RouteData RouteData { get; set; }

		// Token: 0x06000202 RID: 514 RVA: 0x00007494 File Offset: 0x00005694
		public static DirectRouteCandidate SelectBestCandidate(List<DirectRouteCandidate> candidates, ControllerContext controllerContext)
		{
			List<DirectRouteCandidate> candidates2 = DirectRouteCandidate.ApplyActionNameFilters(candidates, controllerContext);
			List<DirectRouteCandidate> candidates3 = DirectRouteCandidate.ApplyActionSelectors(candidates2, controllerContext);
			List<DirectRouteCandidate> candidates4 = DirectRouteCandidate.FilterByOrder(candidates3);
			List<DirectRouteCandidate> list = DirectRouteCandidate.FilterByPrecedence(candidates4);
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			throw DirectRouteCandidate.CreateAmbiguiousMatchException(candidates);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000074E0 File Offset: 0x000056E0
		private static AmbiguousMatchException CreateAmbiguiousMatchException(List<DirectRouteCandidate> candidates)
		{
			string text = DirectRouteCandidate.CreateAmbiguousMatchList(candidates);
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.DirectRoute_AmbiguousMatch, new object[]
			{
				text
			});
			return new AmbiguousMatchException(message);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007518 File Offset: 0x00005718
		protected static string CreateAmbiguousMatchList(IEnumerable<DirectRouteCandidate> candidates)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DirectRouteCandidate directRouteCandidate in candidates)
			{
				MethodInfo methodInfo = null;
				ReflectedActionDescriptor reflectedActionDescriptor = directRouteCandidate.ActionDescriptor as ReflectedActionDescriptor;
				if (reflectedActionDescriptor == null)
				{
					ReflectedAsyncActionDescriptor reflectedAsyncActionDescriptor = directRouteCandidate.ActionDescriptor as ReflectedAsyncActionDescriptor;
					if (reflectedAsyncActionDescriptor != null)
					{
						methodInfo = reflectedAsyncActionDescriptor.AsyncMethodInfo;
					}
				}
				else
				{
					methodInfo = reflectedActionDescriptor.MethodInfo;
				}
				string text = (methodInfo == null) ? directRouteCandidate.ActionDescriptor.ActionName : Convert.ToString(methodInfo, CultureInfo.CurrentCulture);
				string fullName = methodInfo.DeclaringType.FullName;
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, MvcResources.ActionMethodSelector_AmbiguousMatchType, new object[]
				{
					text,
					fullName
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007630 File Offset: 0x00005830
		private static List<DirectRouteCandidate> ApplyActionNameFilters(List<DirectRouteCandidate> candidates, ControllerContext controllerContext)
		{
			List<DirectRouteCandidate> list = new List<DirectRouteCandidate>();
			foreach (DirectRouteCandidate directRouteCandidate in candidates)
			{
				string actionName;
				directRouteCandidate.RouteData.Values.TryGetValue("action", out actionName);
				if (directRouteCandidate.HasActionNameSelectors)
				{
					actionName = (actionName ?? directRouteCandidate.ActionDescriptor.ActionName);
					if (directRouteCandidate.ActionNameSelectors.All((ActionNameSelector selector) => selector(controllerContext, actionName)))
					{
						list.Add(directRouteCandidate);
					}
				}
				else if (actionName != null)
				{
					if (string.Equals(actionName, directRouteCandidate.ActionDescriptor.ActionName, StringComparison.OrdinalIgnoreCase))
					{
						list.Add(directRouteCandidate);
					}
				}
				else
				{
					list.Add(directRouteCandidate);
				}
			}
			return list;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007754 File Offset: 0x00005954
		private static List<DirectRouteCandidate> ApplyActionSelectors(List<DirectRouteCandidate> candidates, ControllerContext controllerContext)
		{
			List<DirectRouteCandidate> list = new List<DirectRouteCandidate>();
			List<DirectRouteCandidate> list2 = new List<DirectRouteCandidate>();
			foreach (DirectRouteCandidate directRouteCandidate in candidates)
			{
				if (directRouteCandidate.HasActionSelectors)
				{
					if (directRouteCandidate.ActionSelectors.All((ActionSelector selector) => selector(controllerContext)))
					{
						list.Add(directRouteCandidate);
					}
				}
				else
				{
					list2.Add(directRouteCandidate);
				}
			}
			if (!list.Any<DirectRouteCandidate>())
			{
				return list2;
			}
			return list;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000781C File Offset: 0x00005A1C
		private static List<DirectRouteCandidate> FilterByOrder(List<DirectRouteCandidate> candidates)
		{
			if (!candidates.Any<DirectRouteCandidate>())
			{
				return candidates;
			}
			int minimum = candidates.Min((DirectRouteCandidate c) => c.Order);
			return (from c in candidates
			where c.Order == minimum
			select c).AsList<DirectRouteCandidate>();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000789C File Offset: 0x00005A9C
		private static List<DirectRouteCandidate> FilterByPrecedence(List<DirectRouteCandidate> candidates)
		{
			if (!candidates.Any<DirectRouteCandidate>())
			{
				return candidates;
			}
			decimal minimum = candidates.Min((DirectRouteCandidate c) => c.Precedence);
			return (from c in candidates
			where c.Precedence == minimum
			select c).AsList<DirectRouteCandidate>();
		}
	}
}
