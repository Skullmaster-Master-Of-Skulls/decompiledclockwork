using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Web.Http
{
	// Token: 0x020000A2 RID: 162
	[TypeForwardedFrom("System.Web.Http.OData, Version=5.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public sealed class SingleResult<T> : SingleResult
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000C034 File Offset: 0x0000A234
		public SingleResult(IQueryable<T> queryable) : base(queryable)
		{
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000C03D File Offset: 0x0000A23D
		public new IQueryable<T> Queryable
		{
			get
			{
				return base.Queryable as IQueryable<T>;
			}
		}
	}
}
