using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Routing
{
	// Token: 0x0200007B RID: 123
	[DebuggerDisplay("{DebuggerToString()}")]
	internal class CandidateAction
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000A148 File Offset: 0x00008348
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000A150 File Offset: 0x00008350
		public HttpActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000A159 File Offset: 0x00008359
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000A161 File Offset: 0x00008361
		public int Order { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000A16A File Offset: 0x0000836A
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000A172 File Offset: 0x00008372
		public decimal Precedence { get; set; }

		// Token: 0x0600033A RID: 826 RVA: 0x0000A17B File Offset: 0x0000837B
		public bool MatchName(string actionName)
		{
			return string.Equals(this.ActionDescriptor.ActionName, actionName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000A18F File Offset: 0x0000838F
		public bool MatchVerb(HttpMethod method)
		{
			return this.ActionDescriptor.SupportedHttpMethods.Contains(method);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000A1A4 File Offset: 0x000083A4
		internal string DebuggerToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}, Order={1}, Prec={2}", new object[]
			{
				this.ActionDescriptor.ActionName,
				this.Order,
				this.Precedence
			});
		}
	}
}
