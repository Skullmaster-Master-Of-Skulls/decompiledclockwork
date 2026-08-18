using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.Helpers
{
	// Token: 0x0200001D RID: 29
	internal sealed class WebGridDataSource : IWebGridDataSource
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00006217 File Offset: 0x00004417
		public WebGridDataSource(WebGrid grid, IEnumerable<dynamic> values, Type elementType, bool canPage, bool canSort)
		{
			this._grid = grid;
			this._values = values;
			this._elementType = elementType;
			this._canPage = canPage;
			this._canSort = canSort;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006244 File Offset: 0x00004444
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000624C File Offset: 0x0000444C
		public SortInfo DefaultSort { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006255 File Offset: 0x00004455
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000625D File Offset: 0x0000445D
		public int RowsPerPage { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006266 File Offset: 0x00004466
		public int TotalRowCount
		{
			get
			{
				return this._values.Count<object>();
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006304 File Offset: 0x00004504
		public IList<WebGridRow> GetRows(SortInfo sortInfo, int pageIndex)
		{
			IEnumerable<object> enumerable = this._values;
			if (this._canSort)
			{
				enumerable = this.Sort(this._values.AsQueryable<object>(), sortInfo);
			}
			enumerable = this.Page(enumerable, pageIndex);
			try
			{
				enumerable = enumerable.ToList<object>();
			}
			catch (ArgumentException)
			{
				enumerable = this.Page(this._values.AsQueryable<object>(), pageIndex);
			}
			return enumerable.Select(delegate(dynamic value, int index)
			{
				if (WebGridDataSource.<GetRows>o__SiteContainer0.<>p__Site1 == null)
				{
					WebGridDataSource.<GetRows>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, Type, WebGrid, object, int, WebGridRow>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof(WebGridDataSource), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.NamedArgument, "value"),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.NamedArgument, "rowIndex")
					}));
				}
				return WebGridDataSource.<GetRows>o__SiteContainer0.<>p__Site1.Target(WebGridDataSource.<GetRows>o__SiteContainer0.<>p__Site1, typeof(WebGridRow), this._grid, value, index);
			}).ToList<WebGridRow>();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006384 File Offset: 0x00004584
		[return: Dynamic(new bool[]
		{
			false,
			true
		})]
		private IQueryable<dynamic> Sort(IQueryable<dynamic> data, SortInfo sortInfo)
		{
			if (!string.IsNullOrEmpty(sortInfo.SortColumn) || (this.DefaultSort != null && !string.IsNullOrEmpty(this.DefaultSort.SortColumn)))
			{
				return this.Sort(data, this._elementType, sortInfo);
			}
			return data;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000063BD File Offset: 0x000045BD
		[return: Dynamic(new bool[]
		{
			false,
			true
		})]
		private IEnumerable<dynamic> Page(IEnumerable<dynamic> data, int pageIndex)
		{
			if (this._canPage)
			{
				return data.Skip(pageIndex * this.RowsPerPage).Take(this.RowsPerPage);
			}
			return data;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000063E4 File Offset: 0x000045E4
		[return: Dynamic(new bool[]
		{
			false,
			true
		})]
		private IQueryable<dynamic> Sort(IQueryable<dynamic> data, Type elementType, SortInfo sort)
		{
			if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(elementType))
			{
				CallSiteBinder member = Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, sort.SortColumn, typeof(WebGrid), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				});
				ParameterExpression parameterExpression = Expression.Parameter(typeof(IDynamicMetaObjectProvider), "o");
				DynamicExpression body = Expression.Dynamic(member, typeof(object), parameterExpression);
				return WebGridDataSource.SortGenericExpression<IDynamicMetaObjectProvider, object>(data, body, parameterExpression, sort.SortDirection);
			}
			Expression expression;
			Expression expression2;
			ParameterExpression parameterExpression2;
			if (this._grid.CustomSorters.TryGetValue(sort.SortColumn, out expression))
			{
				LambdaExpression lambdaExpression = expression as LambdaExpression;
				expression2 = lambdaExpression.Body;
				parameterExpression2 = lambdaExpression.Parameters[0];
			}
			else
			{
				parameterExpression2 = Expression.Parameter(elementType, "o");
				Expression expression3 = parameterExpression2;
				Type type = elementType;
				string[] array = sort.SortColumn.Split(new char[]
				{
					'.'
				});
				foreach (string name in array)
				{
					PropertyInfo property = type.GetProperty(name);
					if (property == null)
					{
						IQueryable<object> result;
						if (this.DefaultSort != null && !sort.Equals(this.DefaultSort) && !string.IsNullOrEmpty(this.DefaultSort.SortColumn))
						{
							result = this.Sort(data, elementType, this.DefaultSort);
						}
						else
						{
							result = data;
						}
						return result;
					}
					expression3 = Expression.Property(expression3, property);
					type = property.PropertyType;
				}
				expression2 = expression3;
			}
			MethodInfo methodInfo = WebGridDataSource.SortGenericExpressionMethod.MakeGenericMethod(new Type[]
			{
				elementType,
				expression2.Type
			});
			return (IQueryable<object>)methodInfo.Invoke(null, new object[]
			{
				data,
				expression2,
				parameterExpression2,
				sort.SortDirection
			});
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000065C8 File Offset: 0x000047C8
		private static IQueryable<TElement> SortGenericExpression<TElement, TProperty>(IQueryable<dynamic> data, Expression body, ParameterExpression param, SortDirection sortDirection)
		{
			IQueryable<TElement> source = data.Cast<TElement>();
			Expression<Func<TElement, TProperty>> keySelector = Expression.Lambda<Func<TElement, TProperty>>(body, new ParameterExpression[]
			{
				param
			});
			if (sortDirection == SortDirection.Descending)
			{
				return source.OrderByDescending(keySelector);
			}
			return source.OrderBy(keySelector);
		}

		// Token: 0x04000051 RID: 81
		private static readonly MethodInfo SortGenericExpressionMethod = typeof(WebGridDataSource).GetMethod("SortGenericExpression", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000052 RID: 82
		private readonly WebGrid _grid;

		// Token: 0x04000053 RID: 83
		private readonly Type _elementType;

		// Token: 0x04000054 RID: 84
		[Dynamic(new bool[]
		{
			false,
			true
		})]
		private readonly IEnumerable<dynamic> _values;

		// Token: 0x04000055 RID: 85
		private readonly bool _canPage;

		// Token: 0x04000056 RID: 86
		private readonly bool _canSort;

		// Token: 0x0200003D RID: 61
		[CompilerGenerated]
		private static class <GetRows>o__SiteContainer0
		{
			// Token: 0x040000DB RID: 219
			public static CallSite<Func<CallSite, Type, WebGrid, object, int, WebGridRow>> <>p__Site1;
		}
	}
}
