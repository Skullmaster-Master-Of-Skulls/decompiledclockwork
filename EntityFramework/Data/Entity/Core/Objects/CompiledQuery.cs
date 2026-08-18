using System;
using System.Collections;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200052B RID: 1323
	public sealed class CompiledQuery
	{
		// Token: 0x0600321E RID: 12830 RVA: 0x000EF2BC File Offset: 0x000ED4BC
		private CompiledQuery(LambdaExpression query)
		{
			Funcletizer funcletizer = Funcletizer.CreateCompiledQueryLockdownFuncletizer();
			Func<bool> func;
			this._query = (LambdaExpression)funcletizer.Funcletize(query, out func);
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000EF2F4 File Offset: 0x000ED4F4
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>);
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000EF307 File Offset: 0x000ED507
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>);
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000EF31A File Offset: 0x000ED51A
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>);
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000EF32D File Offset: 0x000ED52D
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>);
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000EF340 File Offset: 0x000ED540
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>);
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000EF353 File Offset: 0x000ED553
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>);
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000EF366 File Offset: 0x000ED566
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>);
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000EF379 File Offset: 0x000ED579
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>);
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000EF38C File Offset: 0x000ED58C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>);
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000EF39F File Offset: 0x000ED59F
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000EF3B2 File Offset: 0x000ED5B2
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>);
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000EF3C5 File Offset: 0x000ED5C5
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>);
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000EF3D8 File Offset: 0x000ED5D8
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Func<TArg0, TArg1, TArg2, TArg3, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TResult>);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000EF3EB File Offset: 0x000ED5EB
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Func<TArg0, TArg1, TArg2, TResult> Compile<TArg0, TArg1, TArg2, TResult>(Expression<Func<TArg0, TArg1, TArg2, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TResult>);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000EF3FE File Offset: 0x000ED5FE
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Func<TArg0, TArg1, TResult> Compile<TArg0, TArg1, TResult>(Expression<Func<TArg0, TArg1, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TResult>);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000EF411 File Offset: 0x000ED611
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Func<TArg0, TResult> Compile<TArg0, TResult>(Expression<Func<TArg0, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TResult>(new CompiledQuery(query).Invoke<TArg0, TResult>);
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000EF424 File Offset: 0x000ED624
		private TResult Invoke<TArg0, TResult>(TArg0 arg0) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[0]);
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000EF45C File Offset: 0x000ED65C
		private TResult Invoke<TArg0, TArg1, TResult>(TArg0 arg0, TArg1 arg1) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1
			});
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000EF4A8 File Offset: 0x000ED6A8
		private TResult Invoke<TArg0, TArg1, TArg2, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000EF4FC File Offset: 0x000ED6FC
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000EF55C File Offset: 0x000ED75C
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			});
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000EF5C4 File Offset: 0x000ED7C4
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) where TArg0 : ObjectContext
		{
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

		// Token: 0x06003235 RID: 12853 RVA: 0x000EF638 File Offset: 0x000ED838
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) where TArg0 : ObjectContext
		{
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

		// Token: 0x06003236 RID: 12854 RVA: 0x000EF6B4 File Offset: 0x000ED8B4
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) where TArg0 : ObjectContext
		{
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

		// Token: 0x06003237 RID: 12855 RVA: 0x000EF73C File Offset: 0x000ED93C
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) where TArg0 : ObjectContext
		{
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

		// Token: 0x06003238 RID: 12856 RVA: 0x000EF7CC File Offset: 0x000ED9CC
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9) where TArg0 : ObjectContext
		{
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

		// Token: 0x06003239 RID: 12857 RVA: 0x000EF868 File Offset: 0x000EDA68
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10) where TArg0 : ObjectContext
		{
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

		// Token: 0x0600323A RID: 12858 RVA: 0x000EF910 File Offset: 0x000EDB10
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11) where TArg0 : ObjectContext
		{
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

		// Token: 0x0600323B RID: 12859 RVA: 0x000EF9C4 File Offset: 0x000EDBC4
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12) where TArg0 : ObjectContext
		{
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

		// Token: 0x0600323C RID: 12860 RVA: 0x000EFA80 File Offset: 0x000EDC80
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13) where TArg0 : ObjectContext
		{
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

		// Token: 0x0600323D RID: 12861 RVA: 0x000EFB48 File Offset: 0x000EDD48
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14) where TArg0 : ObjectContext
		{
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

		// Token: 0x0600323E RID: 12862 RVA: 0x000EFC1C File Offset: 0x000EDE1C
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15) where TArg0 : ObjectContext
		{
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

		// Token: 0x0600323F RID: 12863 RVA: 0x000EFCFC File Offset: 0x000EDEFC
		private TResult ExecuteQuery<TResult>(ObjectContext context, params object[] parameterValues)
		{
			bool flag;
			Type elementType = CompiledQuery.GetElementType(typeof(TResult), out flag);
			ObjectQueryState objectQueryState = new CompiledELinqQueryState(elementType, context, this._query, this._cacheToken, parameterValues, null);
			IEnumerable enumerable = objectQueryState.CreateQuery();
			if (flag)
			{
				return ObjectQueryProvider.ExecuteSingle<TResult>(enumerable.Cast<TResult>(), this._query);
			}
			return (TResult)((object)enumerable);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000EFD54 File Offset: 0x000EDF54
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

		// Token: 0x04001360 RID: 4960
		private readonly LambdaExpression _query;

		// Token: 0x04001361 RID: 4961
		private readonly Guid _cacheToken = Guid.NewGuid();
	}
}
