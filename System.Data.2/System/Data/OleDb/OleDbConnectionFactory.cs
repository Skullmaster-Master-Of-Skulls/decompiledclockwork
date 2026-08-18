using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.OleDb
{
	// Token: 0x02000244 RID: 580
	internal sealed class OleDbConnectionFactory : DbConnectionFactory
	{
		// Token: 0x06002495 RID: 9365 RVA: 0x000F9E48 File Offset: 0x000F9248
		private OleDbConnectionFactory()
		{
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x000F9E5C File Offset: 0x000F925C
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return OleDbFactory.Instance;
			}
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000F9E70 File Offset: 0x000F9270
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningObject)
		{
			return new OleDbConnectionInternal((OleDbConnectionString)options, (OleDbConnection)owningObject);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000F9E94 File Offset: 0x000F9294
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new OleDbConnectionString(connectionString, previous != null);
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000F9EB0 File Offset: 0x000F92B0
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			OleDbConnectionInternal oleDbConnectionInternal = (OleDbConnectionInternal)internalConnection;
			OleDbConnection connection = oleDbConnectionInternal.Connection;
			NameValueCollection nameValueCollection = (NameValueCollection)PrivilegedConfigurationManager.GetSection("system.data.oledb");
			Stream stream = null;
			string text = connection.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 96) as string;
			if (nameValueCollection != null)
			{
				string[] array = null;
				string text2 = null;
				if (text != null)
				{
					text2 = text + ":MetaDataXml";
					array = nameValueCollection.GetValues(text2);
				}
				if (array == null)
				{
					text2 = "defaultMetaDataXml";
					array = nameValueCollection.GetValues(text2);
				}
				if (array != null)
				{
					stream = ADP.GetXmlStreamFromValues(array, text2);
				}
			}
			if (stream == null)
			{
				stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("System.Data.OleDb.OleDbMetaData.xml");
				cacheMetaDataFactory = true;
			}
			return new OleDbMetaDataFactory(stream, oleDbConnectionInternal.ServerVersion, oleDbConnectionInternal.ServerVersion, oleDbConnectionInternal.GetSchemaRowsetInformation());
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000F9F64 File Offset: 0x000F9364
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000F9F74 File Offset: 0x000F9374
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new OleDbConnectionPoolGroupProviderInfo();
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000F9F88 File Offset: 0x000F9388
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			OleDbConnection oleDbConnection = connection as OleDbConnection;
			if (oleDbConnection != null)
			{
				return oleDbConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000F9FA8 File Offset: 0x000F93A8
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			OleDbConnection oleDbConnection = connection as OleDbConnection;
			if (oleDbConnection != null)
			{
				return oleDbConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000F9FC8 File Offset: 0x000F93C8
		protected override int GetObjectId(DbConnection connection)
		{
			OleDbConnection oleDbConnection = connection as OleDbConnection;
			if (oleDbConnection != null)
			{
				return oleDbConnection.ObjectID;
			}
			return 0;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000F9FE8 File Offset: 0x000F93E8
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			OleDbConnection oleDbConnection = outerConnection as OleDbConnection;
			if (oleDbConnection != null)
			{
				oleDbConnection.PermissionDemand();
			}
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000FA008 File Offset: 0x000F9408
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			OleDbConnection oleDbConnection = outerConnection as OleDbConnection;
			if (oleDbConnection != null)
			{
				oleDbConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000FA028 File Offset: 0x000F9428
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			OleDbConnection oleDbConnection = owningObject as OleDbConnection;
			if (oleDbConnection != null)
			{
				oleDbConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000FA048 File Offset: 0x000F9448
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			OleDbConnection oleDbConnection = owningObject as OleDbConnection;
			return oleDbConnection != null && oleDbConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000FA06C File Offset: 0x000F946C
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			OleDbConnection oleDbConnection = owningObject as OleDbConnection;
			if (oleDbConnection != null)
			{
				oleDbConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x040015A3 RID: 5539
		private const string _metaDataXml = ":MetaDataXml";

		// Token: 0x040015A4 RID: 5540
		private const string _defaultMetaDataXml = "defaultMetaDataXml";

		// Token: 0x040015A5 RID: 5541
		public static readonly OleDbConnectionFactory SingletonInstance = new OleDbConnectionFactory();
	}
}
