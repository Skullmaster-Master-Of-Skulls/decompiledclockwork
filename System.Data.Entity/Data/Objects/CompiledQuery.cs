using System;
using System.Collections;
using System.Data.Objects.ELinq;
using System.Data.Objects.Internal;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects
{
	// Token: 0x0200012B RID: 299
	public sealed class CompiledQuery
	{
		// Token: 0x060015CA RID: 5578 RVA: 0x00049350 File Offset: 0x00047550
		private CompiledQuery(LambdaExpression query)
		{
			EntityUtil.CheckArgumentNull<LambdaExpression>(query, "query");
			Funcletizer funcletizer = Funcletizer.CreateCompiledQueryLockdownFuncletizer();
			Func<bool> func;
			this._query = (LambdaExpression)funcletizer.Funcletize(query, out func);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00049394 File Offset: 0x00047594
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000493A7 File Offset: 0x000475A7
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000493BA File Offset: 0x000475BA
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000493CD File Offset: 0x000475CD
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000493E0 File Offset: 0x000475E0
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000493F3 File Offset: 0x000475F3
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00049406 File Offset: 0x00047606
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00049419 File Offset: 0x00047619
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0004942C File Offset: 0x0004762C
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0004943F File Offset: 0x0004763F
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00049452 File Offset: 0x00047652
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00049465 File Offset: 0x00047665
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00049478 File Offset: 0x00047678
		public static Func<TArg0, TArg1, TArg2, TArg3, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TResult>);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0004948B File Offset: 0x0004768B
		public static Func<TArg0, TArg1, TArg2, TResult> Compile<TArg0, TArg1, TArg2, TResult>(Expression<Func<TArg0, TArg1, TArg2, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TResult>);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0004949E File Offset: 0x0004769E
		public static Func<TArg0, TArg1, TResult> Compile<TArg0, TArg1, TResult>(Expression<Func<TArg0, TArg1, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TResult>);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000494B1 File Offset: 0x000476B1
		public static Func<TArg0, TResult> Compile<TArg0, TResult>(Expression<Func<TArg0, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TResult>(new CompiledQuery(query).Invoke<TArg0, TResult>);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000494C4 File Offset: 0x000476C4
		private TResult Invoke<TArg0, TResult>(TArg0 arg0) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[0]);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00049504 File Offset: 0x00047704
		private TResult Invoke<TArg0, TArg1, TResult>(TArg0 arg0, TArg1 arg1) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1
			});
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00049558 File Offset: 0x00047758
		private TResult Invoke<TArg0, TArg1, TArg2, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000495B4 File Offset: 0x000477B4
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0004961C File Offset: 0x0004781C
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			});
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0004968C File Offset: 0x0004788C
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5
			});
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00049708 File Offset: 0x00047908
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6
			});
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0004978C File Offset: 0x0004798C
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7
			});
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0004981C File Offset: 0x00047A1C
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8
			});
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000498B4 File Offset: 0x00047AB4
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9
			});
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00049958 File Offset: 0x00047B58
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9,
				arg10
			});
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00049A08 File Offset: 0x00047C08
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9,
				arg10,
				arg11
			});
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00049AC4 File Offset: 0x00047CC4
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9,
				arg10,
				arg11,
				arg12
			});
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00049B88 File Offset: 0x00047D88
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9,
				arg10,
				arg11,
				arg12,
				arg13
			});
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00049C58 File Offset: 0x00047E58
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9,
				arg10,
				arg11,
				arg12,
				arg13,
				arg14
			});
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00049D34 File Offset: 0x00047F34
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15) where TArg0 : ObjectContext
		{
			EntityUtil.CheckArgumentNull<TArg0>(arg0, "arg0");
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9,
				arg10,
				arg11,
				arg12,
				arg13,
				arg14,
				arg15
			});
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00049E1C File Offset: 0x0004801C
		private TResult ExecuteQuery<TResult>(ObjectContext context, params object[] parameterValues)
		{
			bool flag;
			Type elementType = CompiledQuery.GetElementType(typeof(TResult), out flag);
			ObjectQueryState objectQueryState = new CompiledELinqQueryState(elementType, context, this._query, this._cacheToken, parameterValues);
			IEnumerable enumerable = objectQueryState.CreateQuery();
			if (flag)
			{
				return ObjectQueryProvider.ExecuteSingle<TResult>(enumerable.Cast<TResult>(), this._query);
			}
			return (TResult)((object)enumerable);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00049E74 File Offset: 0x00048074
		private static Type GetElementType(Type resultType, out bool isSingleton)
		{
			Type elementType = TypeSystem.GetElementType(resultType);
			isSingleton = (elementType == resultType || !resultType.IsAssignableFrom(typeof(ObjectQuery<>).MakeGenericType(new Type[]
			{
				elementType
			})));
			if (isSingleton)
			{
				return resultType;
			}
			return elementType;
		}

		// Token: 0x04000A3F RID: 2623
		private readonly LambdaExpression _query;

		// Token: 0x04000A40 RID: 2624
		private readonly Guid _cacheToken = Guid.NewGuid();
	}
}
