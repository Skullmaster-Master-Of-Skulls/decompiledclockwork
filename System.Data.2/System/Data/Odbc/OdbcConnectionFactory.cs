using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.Odbc
{
	// Token: 0x02000291 RID: 657
	internal sealed class OdbcConnectionFactory : DbConnectionFactory
	{
		// Token: 0x060027F4 RID: 10228 RVA: 0x0010C438 File Offset: 0x0010B838
		private OdbcConnectionFactory()
		{
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x0010C44C File Offset: 0x0010B84C
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return OdbcFactory.Instance;
			}
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0010C460 File Offset: 0x0010B860
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningObject)
		{
			return new OdbcConnectionOpen(owningObject as OdbcConnection, options as OdbcConnectionString);
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0010C484 File Offset: 0x0010B884
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new OdbcConnectionString(connectionString, previous != null);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0010C4A0 File Offset: 0x0010B8A0
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0010C4B0 File Offset: 0x0010B8B0
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new OdbcConnectionPoolGroupProviderInfo();
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0010C4C4 File Offset: 0x0010B8C4
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			OdbcConnection outerConnection = ((OdbcConnectionOpen)internalConnection).OuterConnection;
			NameValueCollection nameValueCollection = (NameValueCollection)PrivilegedConfigurationManager.GetSection("system.data.odbc");
			Stream stream = null;
			object obj = null;
			string infoStringUnhandled = outerConnection.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_NAME);
			if (infoStringUnhandled != null)
			{
				obj = infoStringUnhandled;
			}
			if (nameValueCollection != null)
			{
				string[] array = null;
				string text = null;
				if (obj != null)
				{
					text = (string)obj + ":MetaDataXml";
					array = nameValueCollection.GetValues(text);
				}
				if (array == null)
				{
					text = "defaultMetaDataXml";
					array = nameValueCollection.GetValues(text);
				}
				if (array != null)
				{
					stream = ADP.GetXmlStreamFromValues(array, text);
				}
			}
			if (stream == null)
			{
				stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("System.Data.Odbc.OdbcMetaData.xml");
				cacheMetaDataFactory = true;
			}
			string infoStringUnhandled2 = outerConnection.GetInfoStringUnhandled(ODBC32.SQL_INFO.DBMS_VER);
			return new OdbcMetaDataFactory(stream, infoStringUnhandled2, infoStringUnhandled2, outerConnection);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0010C578 File Offset: 0x0010B978
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0010C598 File Offset: 0x0010B998
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x0010C5B8 File Offset: 0x0010B9B8
		protected override int GetObjectId(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.ObjectID;
			}
			return 0;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x0010C5D8 File Offset: 0x0010B9D8
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PermissionDemand();
			}
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0010C5F8 File Offset: 0x0010B9F8
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0010C618 File Offset: 0x0010BA18
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0010C638 File Offset: 0x0010BA38
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			return odbcConnection != null && odbcConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0010C65C File Offset: 0x0010BA5C
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x04001A6A RID: 6762
		private const string _MetaData = ":MetaDataXml";

		// Token: 0x04001A6B RID: 6763
		private const string _defaultMetaDataXml = "defaultMetaDataXml";

		// Token: 0x04001A6C RID: 6764
		public static readonly OdbcConnectionFactory SingletonInstance = new OdbcConnectionFactory();
	}
}
