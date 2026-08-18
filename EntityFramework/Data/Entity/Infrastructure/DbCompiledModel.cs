using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000745 RID: 1861
	public class DbCompiledModel
	{
		// Token: 0x0600543C RID: 21564 RVA: 0x00170C43 File Offset: 0x0016EE43
		internal DbCompiledModel()
		{
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00170C4B File Offset: 0x0016EE4B
		internal DbCompiledModel(DbModel model)
		{
			this._workspace = new CodeFirstCachedMetadataWorkspace(model.DatabaseMapping);
			this._cachedModelBuilder = model.CachedModelBuilder;
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x0600543E RID: 21566 RVA: 0x00170C70 File Offset: 0x0016EE70
		internal virtual DbModelBuilder CachedModelBuilder
		{
			get
			{
				return this._cachedModelBuilder;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x00170C78 File Offset: 0x0016EE78
		internal virtual DbProviderInfo ProviderInfo
		{
			get
			{
				return this._workspace.ProviderInfo;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06005440 RID: 21568 RVA: 0x00170C85 File Offset: 0x0016EE85
		internal string DefaultSchema
		{
			get
			{
				return this.CachedModelBuilder.ModelConfiguration.DefaultSchema;
			}
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00170C98 File Offset: 0x0016EE98
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public TContext CreateObjectContext<TContext>(DbConnection existingConnection) where TContext : ObjectContext
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			MetadataWorkspace metadataWorkspace = this._workspace.GetMetadataWorkspace(existingConnection);
			EntityConnection arg = new EntityConnection(metadataWorkspace, existingConnection);
			TContext result = (TContext)((object)DbCompiledModel.GetConstructorDelegate<TContext>()(arg));
			result.ContextOwnsConnection = true;
			if (string.IsNullOrEmpty(result.DefaultContainerName))
			{
				result.DefaultContainerName = this._workspace.DefaultContainerName;
			}
			foreach (Assembly assembly in this._workspace.Assemblies)
			{
				result.MetadataWorkspace.LoadFromAssembly(assembly);
			}
			return result;
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x00170D70 File Offset: 0x0016EF70
		internal static Func<EntityConnection, ObjectContext> GetConstructorDelegate<TContext>() where TContext : ObjectContext
		{
			Func<ConstructorInfo, bool> func = null;
			if (typeof(TContext) == typeof(ObjectContext))
			{
				return DbCompiledModel._objectContextConstructor;
			}
			Func<EntityConnection, ObjectContext> func2;
			if (!DbCompiledModel._contextConstructors.TryGetValue(typeof(TContext), out func2))
			{
				Type typeFromHandle = typeof(TContext);
				if (func == null)
				{
					func = ((ConstructorInfo c) => c.IsPublic);
				}
				ConstructorInfo declaredConstructor = typeFromHandle.GetDeclaredConstructor(func, new Type[][]
				{
					new Type[]
					{
						typeof(EntityConnection)
					},
					new Type[]
					{
						typeof(DbConnection)
					},
					new Type[]
					{
						typeof(IDbConnection)
					},
					new Type[]
					{
						typeof(IDisposable)
					},
					new Type[]
					{
						typeof(Component)
					},
					new Type[]
					{
						typeof(MarshalByRefObject)
					},
					new Type[]
					{
						typeof(object)
					}
				});
				if (declaredConstructor == null)
				{
					throw Error.DbModelBuilder_MissingRequiredCtor(typeof(TContext).Name);
				}
				ParameterExpression parameterExpression;
				func2 = Expression.Lambda<Func<EntityConnection, ObjectContext>>(Expression.New(declaredConstructor, new Expression[]
				{
					parameterExpression
				}), new ParameterExpression[]
				{
					parameterExpression
				}).Compile();
				DbCompiledModel._contextConstructors.TryAdd(typeof(TContext), func2);
			}
			return func2;
		}

		// Token: 0x04002276 RID: 8822
		private static readonly ConcurrentDictionary<Type, Func<EntityConnection, ObjectContext>> _contextConstructors = new ConcurrentDictionary<Type, Func<EntityConnection, ObjectContext>>();

		// Token: 0x04002277 RID: 8823
		private static readonly Func<EntityConnection, ObjectContext> _objectContextConstructor = (EntityConnection c) => new ObjectContext(c);

		// Token: 0x04002278 RID: 8824
		private readonly ICachedMetadataWorkspace _workspace;

		// Token: 0x04002279 RID: 8825
		private readonly DbModelBuilder _cachedModelBuilder;
	}
}
