using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.Odbc
{
	// Token: 0x020001D8 RID: 472
	internal sealed class OdbcConnectionFactory : DbConnectionFactory
	{
		// Token: 0x06001A45 RID: 6725 RVA: 0x0025D918 File Offset: 0x0025CD18
		private OdbcConnectionFactory()
		{
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0025D938 File Offset: 0x0025CD38
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return OdbcFactory.Instance;
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0025D958 File Offset: 0x0025CD58
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningObject)
		{
			return new OdbcConnectionOpen(owningObject as OdbcConnection, options as OdbcConnectionString);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0025D988 File Offset: 0x0025CD88
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new OdbcConnectionString(connectionString, null != previous);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0025D9A8 File Offset: 0x0025CDA8
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0025D9B8 File Offset: 0x0025CDB8
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new OdbcConnectionPoolGroupProviderInfo();
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0025D9D8 File Offset: 0x0025CDD8
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

		// Token: 0x06001A4C RID: 6732 RVA: 0x0025DA98 File Offset: 0x0025CE98
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0025DAB8 File Offset: 0x0025CEB8
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0025DAD8 File Offset: 0x0025CED8
		protected override int GetObjectId(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.ObjectID;
			}
			return 0;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0025DAF8 File Offset: 0x0025CEF8
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PermissionDemand();
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0025DB18 File Offset: 0x0025CF18
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0025DB38 File Offset: 0x0025CF38
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0025DB58 File Offset: 0x0025CF58
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			return odbcConnection != null && odbcConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0025DB88 File Offset: 0x0025CF88
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x04000FA8 RID: 4008
		private const string _MetaData = ":MetaDataXml";

		// Token: 0x04000FA9 RID: 4009
		private const string _defaultMetaDataXml = "defaultMetaDataXml";

		// Token: 0x04000FAA RID: 4010
		public static readonly OdbcConnectionFactory SingletonInstance = new OdbcConnectionFactory();
	}
}
