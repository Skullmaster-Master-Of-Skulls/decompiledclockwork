using System;

namespace System.Web.Http
{
	// Token: 0x02000102 RID: 258
	public sealed class RouteParameter
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x00014D15 File Offset: 0x00012F15
		private RouteParameter()
		{
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00014D1D File Offset: 0x00012F1D
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x040001C4 RID: 452
		public static readonly RouteParameter Optional = new RouteParameter();
	}
}
