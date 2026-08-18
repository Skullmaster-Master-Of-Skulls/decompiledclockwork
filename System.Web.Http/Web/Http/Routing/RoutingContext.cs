using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x0200009D RID: 157
	internal class RoutingContext
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000BF41 File Offset: 0x0000A141
		public static RoutingContext Invalid()
		{
			return RoutingContext.CachedInvalid;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000BF48 File Offset: 0x0000A148
		public static RoutingContext Valid(List<string> pathSegments)
		{
			return new RoutingContext
			{
				PathSegments = pathSegments,
				IsValid = true
			};
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000BF6A File Offset: 0x0000A16A
		private RoutingContext()
		{
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000BF72 File Offset: 0x0000A172
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0000BF7A File Offset: 0x0000A17A
		public bool IsValid { get; private set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000BF83 File Offset: 0x0000A183
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000BF8B File Offset: 0x0000A18B
		public List<string> PathSegments { get; private set; }

		// Token: 0x04000118 RID: 280
		private static readonly RoutingContext CachedInvalid = new RoutingContext
		{
			IsValid = false
		};
	}
}
