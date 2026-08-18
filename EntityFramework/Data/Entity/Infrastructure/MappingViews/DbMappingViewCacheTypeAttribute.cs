using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.MappingViews
{
	// Token: 0x02000193 RID: 403
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
	public sealed class DbMappingViewCacheTypeAttribute : Attribute
	{
		// Token: 0x06000D8E RID: 3470 RVA: 0x0003D0CC File Offset: 0x0003B2CC
		public DbMappingViewCacheTypeAttribute(Type contextType, Type cacheType)
		{
			Check.NotNull<Type>(contextType, "contextType");
			Check.NotNull<Type>(cacheType, "cacheType");
			if (!contextType.IsSubclassOf(typeof(ObjectContext)) && !contextType.IsSubclassOf(typeof(DbContext)))
			{
				throw new ArgumentException(Strings.DbMappingViewCacheTypeAttribute_InvalidContextType(contextType), "contextType");
			}
			if (!cacheType.IsSubclassOf(typeof(DbMappingViewCache)))
			{
				throw new ArgumentException(Strings.Generated_View_Type_Super_Class(cacheType), "cacheType");
			}
			this._contextType = contextType;
			this._cacheType = cacheType;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0003D160 File Offset: 0x0003B360
		public DbMappingViewCacheTypeAttribute(Type contextType, string cacheTypeName)
		{
			Check.NotNull<Type>(contextType, "contextType");
			Check.NotEmpty(cacheTypeName, "cacheTypeName");
			if (!contextType.IsSubclassOf(typeof(ObjectContext)) && !contextType.IsSubclassOf(typeof(DbContext)))
			{
				throw new ArgumentException(Strings.DbMappingViewCacheTypeAttribute_InvalidContextType(contextType), "contextType");
			}
			this._contextType = contextType;
			try
			{
				this._cacheType = Type.GetType(cacheTypeName, true);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException(Strings.DbMappingViewCacheTypeAttribute_CacheTypeNotFound(cacheTypeName), "cacheTypeName", innerException);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0003D1FC File Offset: 0x0003B3FC
		internal Type ContextType
		{
			get
			{
				return this._contextType;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0003D204 File Offset: 0x0003B404
		internal Type CacheType
		{
			get
			{
				return this._cacheType;
			}
		}

		// Token: 0x040003AF RID: 943
		private readonly Type _contextType;

		// Token: 0x040003B0 RID: 944
		private readonly Type _cacheType;
	}
}
