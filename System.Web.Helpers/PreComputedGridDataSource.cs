using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.Helpers
{
	// Token: 0x0200001B RID: 27
	internal sealed class PreComputedGridDataSource : IWebGridDataSource
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00006134 File Offset: 0x00004334
		public PreComputedGridDataSource(WebGrid grid, IEnumerable<dynamic> values, int totalRows)
		{
			this._totalRows = totalRows;
			this._rows = values.Select(delegate(dynamic value, int index)
			{
				if (PreComputedGridDataSource.ctor>o__SiteContainer0.<>p__Site1 == null)
				{
					PreComputedGridDataSource.ctor>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, Type, WebGrid, object, int, WebGridRow>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof(PreComputedGridDataSource), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.NamedArgument, "value"),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.NamedArgument, "rowIndex")
					}));
				}
				return PreComputedGridDataSource.ctor>o__SiteContainer0.<>p__Site1.Target(PreComputedGridDataSource.ctor>o__SiteContainer0.<>p__Site1, typeof(WebGridRow), grid, value, index);
			}).ToList<WebGridRow>();
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000617F File Offset: 0x0000437F
		public int TotalRowCount
		{
			get
			{
				return this._totalRows;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006187 File Offset: 0x00004387
		public IList<WebGridRow> GetRows(SortInfo sortInfo, int pageIndex)
		{
			return this._rows;
		}

		// Token: 0x0400004D RID: 77
		private readonly int _totalRows;

		// Token: 0x0400004E RID: 78
		private readonly IList<WebGridRow> _rows;

		// Token: 0x0200003B RID: 59
		[CompilerGenerated]
		private static class ctor>o__SiteContainer0
		{
			// Token: 0x040000D9 RID: 217
			public static CallSite<Func<CallSite, Type, WebGrid, object, int, WebGridRow>> <>p__Site1;
		}
	}
}
