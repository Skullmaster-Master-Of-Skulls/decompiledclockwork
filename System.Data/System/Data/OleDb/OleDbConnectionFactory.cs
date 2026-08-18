using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.OleDb
{
	// Token: 0x02000215 RID: 533
	internal sealed class OleDbConnectionFactory : DbConnectionFactory
	{
		// Token: 0x06001E6A RID: 7786 RVA: 0x00273D88 File Offset: 0x00273188
		private OleDbConnectionFactory()
		{
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x00273DA8 File Offset: 0x002731A8
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return OleDbFactory.Instance;
			}
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00273DC8 File Offset: 0x002731C8
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningObject)
		{
			return new OleDbConnectionInternal((OleDbConnectionString)options, (OleDbConnection)owningObject);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00273DF8 File Offset: 0x002731F8
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new OleDbConnectionString(connectionString, null != previous);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00273E18 File Offset: 0x00273218
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

		// Token: 0x06001E6F RID: 7791 RVA: 0x00273ED8 File Offset: 0x002732D8
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x00273EE8 File Offset: 0x002732E8
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new OleDbConnectionPoolGroupProviderInfo();
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00273F08 File Offset: 0x00273308
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			OleDbConnection oleDbConnection = connection as OleDbConnection;
			if (oleDbConnection != null)
			{
				return oleDbConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00273F28 File Offset: 0x00273328
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			OleDbConnection oleDbConnection = connection as OleDbConnection;
			if (oleDbConnection != null)
			{
				return oleDbConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x00273F48 File Offset: 0x00273348
		protected override int GetObjectId(DbConnection connection)
		{
			OleDbConnection oleDbConnection = connection as OleDbConnection;
			if (oleDbConnection != null)
			{
				return oleDbConnection.ObjectID;
			}
			return 0;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x00273F68 File Offset: 0x00273368
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			OleDbConnection oleDbConnection = outerConnection as OleDbConnection;
			if (oleDbConnection != null)
			{
				oleDbConnection.PermissionDemand();
			}
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00273F88 File Offset: 0x00273388
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			OleDbConnection oleDbConnection = outerConnection as OleDbConnection;
			if (oleDbConnection != null)
			{
				oleDbConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00273FA8 File Offset: 0x002733A8
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			OleDbConnection oleDbConnection = owningObject as OleDbConnection;
			if (oleDbConnection != null)
			{
				oleDbConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x00273FC8 File Offset: 0x002733C8
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			OleDbConnection oleDbConnection = owningObject as OleDbConnection;
			return oleDbConnection != null && oleDbConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00273FF8 File Offset: 0x002733F8
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			OleDbConnection oleDbConnection = owningObject as OleDbConnection;
			if (oleDbConnection != null)
			{
				oleDbConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x04001275 RID: 4725
		private const string _metaDataXml = ":MetaDataXml";

		// Token: 0x04001276 RID: 4726
		private const string _defaultMetaDataXml = "defaultMetaDataXml";

		// Token: 0x04001277 RID: 4727
		public static readonly OleDbConnectionFactory SingletonInstance = new OleDbConnectionFactory();
	}
}
