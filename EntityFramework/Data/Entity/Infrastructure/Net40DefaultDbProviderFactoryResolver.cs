using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200027E RID: 638
	internal class Net40DefaultDbProviderFactoryResolver : IDbProviderFactoryResolver
	{
		// Token: 0x06001666 RID: 5734 RVA: 0x0006C3FA File Offset: 0x0006A5FA
		public Net40DefaultDbProviderFactoryResolver() : this(new ProviderRowFinder())
		{
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0006C408 File Offset: 0x0006A608
		public Net40DefaultDbProviderFactoryResolver(ProviderRowFinder finder)
		{
			this._finder = finder;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0006C455 File Offset: 0x0006A655
		public DbProviderFactory ResolveProviderFactory(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			return this.GetProviderFactory(connection, DbProviderFactories.GetFactoryClasses().Rows.OfType<DataRow>());
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0006C5A8 File Offset: 0x0006A7A8
		public DbProviderFactory GetProviderFactory(DbConnection connection, IEnumerable<DataRow> dataRows)
		{
			Type type = connection.GetType();
			return this._cache.GetOrAdd(type, delegate(Type t)
			{
				DataRow dataRow;
				if ((dataRow = this._finder.FindRow(t, (DataRow r) => Net40DefaultDbProviderFactoryResolver.ExactMatch(r, t), dataRows)) == null && (dataRow = this._finder.FindRow(null, (DataRow r) => Net40DefaultDbProviderFactoryResolver.ExactMatch(r, t), dataRows)) == null)
				{
					dataRow = (this._finder.FindRow(t, (DataRow r) => Net40DefaultDbProviderFactoryResolver.AssignableMatch(r, t), dataRows) ?? this._finder.FindRow(null, (DataRow r) => Net40DefaultDbProviderFactoryResolver.AssignableMatch(r, t), dataRows));
				}
				DataRow dataRow2 = dataRow;
				if (dataRow2 == null)
				{
					throw new NotSupportedException(Strings.ProviderNotFound(connection.ToString()));
				}
				return DbProviderFactories.GetFactory(dataRow2);
			});
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0006C5F4 File Offset: 0x0006A7F4
		private static bool ExactMatch(DataRow row, Type connectionType)
		{
			return DbProviderFactories.GetFactory(row).CreateConnection().GetType() == connectionType;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0006C60C File Offset: 0x0006A80C
		private static bool AssignableMatch(DataRow row, Type connectionType)
		{
			return connectionType.IsInstanceOfType(DbProviderFactories.GetFactory(row).CreateConnection());
		}

		// Token: 0x040007EE RID: 2030
		private readonly ConcurrentDictionary<Type, DbProviderFactory> _cache = new ConcurrentDictionary<Type, DbProviderFactory>(new KeyValuePair<Type, DbProviderFactory>[]
		{
			new KeyValuePair<Type, DbProviderFactory>(typeof(EntityConnection), EntityProviderFactory.Instance)
		});

		// Token: 0x040007EF RID: 2031
		private readonly ProviderRowFinder _finder;
	}
}
