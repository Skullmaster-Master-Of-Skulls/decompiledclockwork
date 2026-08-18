using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Web.Http
{
	// Token: 0x020000A1 RID: 161
	[TypeForwardedFrom("System.Web.Http.OData, Version=5.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class SingleResult
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000BFFE File Offset: 0x0000A1FE
		protected SingleResult(IQueryable queryable)
		{
			if (queryable == null)
			{
				throw Error.ArgumentNull("queryable");
			}
			this.Queryable = queryable;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000C01B File Offset: 0x0000A21B
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000C023 File Offset: 0x0000A223
		public IQueryable Queryable { get; private set; }

		// Token: 0x060003D6 RID: 982 RVA: 0x0000C02C File Offset: 0x0000A22C
		public static SingleResult<T> Create<T>(IQueryable<T> queryable)
		{
			return new SingleResult<T>(queryable);
		}
	}
}
