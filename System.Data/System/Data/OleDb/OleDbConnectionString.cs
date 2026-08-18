using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Data.OleDb
{
	// Token: 0x02000219 RID: 537
	internal sealed class OleDbConnectionString : DbConnectionOptions
	{
		// Token: 0x06001EA7 RID: 7847 RVA: 0x002752C8 File Offset: 0x002746C8
		internal OleDbConnectionString(string connectionString, bool validate) : base(connectionString)
		{
			string text = base["prompt"];
			this.PossiblePrompt = ((!ADP.IsEmpty(text) && string.Compare(text, "noprompt", StringComparison.OrdinalIgnoreCase) != 0) || !ADP.IsEmpty(base["window handle"]));
			if (!base.IsEmpty)
			{
				string text2 = null;
				if (!validate)
				{
					int num = 0;
					string text3 = null;
					this._expandedConnectionString = base.ExpandDataDirectories(ref text3, ref num);
					if (!ADP.IsEmpty(text3))
					{
						text3 = ADP.GetFullPath(text3);
					}
					if (text3 != null)
					{
						text2 = OleDbConnectionString.LoadStringFromStorage(text3);
						if (!ADP.IsEmpty(text2))
						{
							this._expandedConnectionString = string.Concat(new object[]
							{
								this._expandedConnectionString.Substring(0, num),
								text2,
								';',
								this._expandedConnectionString.Substring(num)
							});
						}
					}
				}
				if (validate || ADP.IsEmpty(text2))
				{
					this.ActualConnectionString = this.ValidateConnectionString(connectionString);
				}
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x002753C8 File Offset: 0x002747C8
		internal int ConnectTimeout
		{
			get
			{
				return base.ConvertValueToInt32("connect timeout", 15);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x002753E8 File Offset: 0x002747E8
		internal string DataSource
		{
			get
			{
				return base.ConvertValueToString("data source", ADP.StrEmpty);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x00275408 File Offset: 0x00274808
		internal string InitialCatalog
		{
			get
			{
				return base.ConvertValueToString("initial catalog", ADP.StrEmpty);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x00275428 File Offset: 0x00274828
		internal string Provider
		{
			get
			{
				return base["provider"];
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x00275448 File Offset: 0x00274848
		internal int OleDbServices
		{
			get
			{
				return this._oledbServices;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x00275468 File Offset: 0x00274868
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x00275488 File Offset: 0x00274888
		internal SchemaSupport[] SchemaSupport
		{
			get
			{
				return this._schemaSupport;
			}
			set
			{
				this._schemaSupport = value;
			}
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x002754A8 File Offset: 0x002748A8
		protected internal override PermissionSet CreatePermissionSet()
		{
			PermissionSet permissionSet;
			if (this.PossiblePrompt)
			{
				permissionSet = new NamedPermissionSet("FullTrust");
			}
			else
			{
				permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new OleDbPermission(this));
			}
			return permissionSet;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x002754E8 File Offset: 0x002748E8
		protected internal override string Expand()
		{
			if (this._expandedConnectionString != null)
			{
				return this._expandedConnectionString;
			}
			return base.Expand();
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00275518 File Offset: 0x00274918
		internal int GetSqlSupport(OleDbConnection connection)
		{
			int num = this._sqlSupport;
			if (!this._hasSqlSupport)
			{
				object dataSourcePropertyValue = connection.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 109);
				if (dataSourcePropertyValue is int)
				{
					num = (int)dataSourcePropertyValue;
				}
				this._sqlSupport = num;
				this._hasSqlSupport = true;
			}
			return num;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00275568 File Offset: 0x00274968
		internal bool GetSupportIRow(OleDbConnection connection, OleDbCommand command)
		{
			bool flag = this._supportIRow;
			if (!this._hasSupportIRow)
			{
				object propertyValue = command.GetPropertyValue(OleDbPropertySetGuid.Rowset, 263);
				flag = !(propertyValue is OleDbPropertyStatus);
				this._supportIRow = flag;
				this._hasSupportIRow = true;
			}
			return flag;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x002755B8 File Offset: 0x002749B8
		internal bool GetSupportMultipleResults(OleDbConnection connection)
		{
			bool flag = this._supportMultipleResults;
			if (!this._hasSupportMultipleResults)
			{
				object dataSourcePropertyValue = connection.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 196);
				if (dataSourcePropertyValue is int)
				{
					flag = (0 != (int)dataSourcePropertyValue);
				}
				this._supportMultipleResults = flag;
				this._hasSupportMultipleResults = true;
			}
			return flag;
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x00275618 File Offset: 0x00274A18
		private static int UdlPoolSize
		{
			get
			{
				int num = OleDbConnectionString.UDL._PoolSize;
				if (!OleDbConnectionString.UDL._PoolSizeInit)
				{
					object obj = ADP.LocalMachineRegistryValue("SOFTWARE\\Microsoft\\DataAccess\\Udl Pooling", "Cache Size");
					if (obj is int)
					{
						num = (int)obj;
						num = ((0 < num) ? num : 0);
						OleDbConnectionString.UDL._PoolSize = num;
					}
					OleDbConnectionString.UDL._PoolSizeInit = true;
				}
				return num;
			}
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00275678 File Offset: 0x00274A78
		private static string LoadStringFromStorage(string udlfilename)
		{
			string text = null;
			Dictionary<string, string> dictionary = OleDbConnectionString.UDL._Pool;
			if (dictionary == null || !dictionary.TryGetValue(udlfilename, out text))
			{
				text = OleDbConnectionString.LoadStringFromFileStorage(udlfilename);
				if (text != null && 0 < OleDbConnectionString.UdlPoolSize)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, string>();
						dictionary[udlfilename] = text;
						lock (OleDbConnectionString.UDL._PoolLock)
						{
							if (OleDbConnectionString.UDL._Pool != null)
							{
								dictionary = OleDbConnectionString.UDL._Pool;
							}
							else
							{
								OleDbConnectionString.UDL._Pool = dictionary;
								dictionary = null;
							}
						}
					}
					if (dictionary != null)
					{
						lock (dictionary)
						{
							dictionary[udlfilename] = text;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00275748 File Offset: 0x00274B48
		private static string LoadStringFromFileStorage(string udlfilename)
		{
			string text = null;
			Exception ex = null;
			try
			{
				int num = ADP.CharSize * "﻿[oledb]\r\n; Everything after this line is an OLE DB initstring\r\n".Length;
				using (FileStream fileStream = new FileStream(udlfilename, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					long length = fileStream.Length;
					if (length < (long)num || 0L != length % (long)ADP.CharSize)
					{
						ex = ADP.InvalidUDL();
					}
					else
					{
						byte[] array = new byte[num];
						int num2 = fileStream.Read(array, 0, array.Length);
						if (num2 < num)
						{
							ex = ADP.InvalidUDL();
						}
						else if (Encoding.Unicode.GetString(array, 0, num) != "﻿[oledb]\r\n; Everything after this line is an OLE DB initstring\r\n")
						{
							ex = ADP.InvalidUDL();
						}
						else
						{
							array = new byte[length - (long)num];
							num2 = fileStream.Read(array, 0, array.Length);
							text = Encoding.Unicode.GetString(array, 0, num2);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				if (!ADP.IsCatchableExceptionType(ex2))
				{
					throw;
				}
				throw ADP.UdlFileError(ex2);
			}
			if (ex != null)
			{
				throw ex;
			}
			return text.Trim();
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00275868 File Offset: 0x00274C68
		private string ValidateConnectionString(string connectionString)
		{
			if (base.ConvertValueToBoolean("asynchronous processing", false))
			{
				throw ODB.AsynchronousNotSupported();
			}
			int num = base.ConvertValueToInt32("connect timeout", 0);
			if (num < 0)
			{
				throw ADP.InvalidConnectTimeoutValue();
			}
			string text = base.ConvertValueToString("data provider", null);
			if (text != null)
			{
				text = text.Trim();
				if (0 < text.Length)
				{
					OleDbConnectionString.ValidateProvider(text);
				}
			}
			text = base.ConvertValueToString("remote provider", null);
			if (text != null)
			{
				text = text.Trim();
				if (0 < text.Length)
				{
					OleDbConnectionString.ValidateProvider(text);
				}
			}
			text = base.ConvertValueToString("provider", ADP.StrEmpty).Trim();
			OleDbConnectionString.ValidateProvider(text);
			this._oledbServices = -13;
			if (!base.ContainsKey("ole db services") || ADP.IsEmpty(base["ole db services"]))
			{
				string text2 = (string)ADP.ClassesRootRegistryValue(text + "\\CLSID", string.Empty);
				if (text2 != null && 0 < text2.Length)
				{
					Guid b = new Guid(text2);
					if (ODB.CLSID_MSDASQL == b)
					{
						throw ODB.MSDASQLNotSupported();
					}
					object obj = ADP.ClassesRootRegistryValue("CLSID\\{" + b.ToString("D", CultureInfo.InvariantCulture) + "}", "OLEDB_SERVICES");
					if (obj != null)
					{
						try
						{
							this._oledbServices = (int)obj;
						}
						catch (InvalidCastException e)
						{
							ADP.TraceExceptionWithoutRethrow(e);
						}
						this._oledbServices &= -13;
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("ole db services");
						stringBuilder.Append("=");
						stringBuilder.Append(this._oledbServices.ToString(CultureInfo.InvariantCulture));
						stringBuilder.Append(";");
						stringBuilder.Append(connectionString);
						connectionString = stringBuilder.ToString();
					}
				}
			}
			else
			{
				this._oledbServices = base.ConvertValueToInt32("ole db services", -13);
			}
			return connectionString;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00275A68 File Offset: 0x00274E68
		internal static bool IsMSDASQL(string progid)
		{
			return "msdasql" == progid || progid.StartsWith("msdasql.", StringComparison.Ordinal) || "microsoft ole db provider for odbc drivers" == progid;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00275AA8 File Offset: 0x00274EA8
		private static void ValidateProvider(string progid)
		{
			if (ADP.IsEmpty(progid))
			{
				throw ODB.NoProviderSpecified();
			}
			if (255 <= progid.Length)
			{
				throw ODB.InvalidProviderSpecified();
			}
			progid = progid.ToLower(CultureInfo.InvariantCulture);
			if (OleDbConnectionString.IsMSDASQL(progid))
			{
				throw ODB.MSDASQLNotSupported();
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00275AF8 File Offset: 0x00274EF8
		internal static void ReleaseObjectPool()
		{
			OleDbConnectionString.UDL._PoolSizeInit = false;
			OleDbConnectionString.UDL._Pool = null;
		}

		// Token: 0x04001284 RID: 4740
		internal readonly bool PossiblePrompt;

		// Token: 0x04001285 RID: 4741
		internal readonly string ActualConnectionString;

		// Token: 0x04001286 RID: 4742
		private readonly string _expandedConnectionString;

		// Token: 0x04001287 RID: 4743
		internal SchemaSupport[] _schemaSupport;

		// Token: 0x04001288 RID: 4744
		internal int _sqlSupport;

		// Token: 0x04001289 RID: 4745
		internal bool _supportMultipleResults;

		// Token: 0x0400128A RID: 4746
		internal bool _supportIRow;

		// Token: 0x0400128B RID: 4747
		internal bool _hasSqlSupport;

		// Token: 0x0400128C RID: 4748
		internal bool _hasSupportMultipleResults;

		// Token: 0x0400128D RID: 4749
		internal bool _hasSupportIRow;

		// Token: 0x0400128E RID: 4750
		private int _oledbServices;

		// Token: 0x0400128F RID: 4751
		internal UnsafeNativeMethods.IUnknownQueryInterface DangerousDataSourceIUnknownQueryInterface;

		// Token: 0x04001290 RID: 4752
		internal UnsafeNativeMethods.IDBInitializeInitialize DangerousIDBInitializeInitialize;

		// Token: 0x04001291 RID: 4753
		internal UnsafeNativeMethods.IDBCreateSessionCreateSession DangerousIDBCreateSessionCreateSession;

		// Token: 0x04001292 RID: 4754
		internal UnsafeNativeMethods.IDBCreateCommandCreateCommand DangerousIDBCreateCommandCreateCommand;

		// Token: 0x04001293 RID: 4755
		internal bool HaveQueriedForCreateCommand;

		// Token: 0x0200021A RID: 538
		private static class UDL
		{
			// Token: 0x04001294 RID: 4756
			internal const string Header = "﻿[oledb]\r\n; Everything after this line is an OLE DB initstring\r\n";

			// Token: 0x04001295 RID: 4757
			internal const string Location = "SOFTWARE\\Microsoft\\DataAccess\\Udl Pooling";

			// Token: 0x04001296 RID: 4758
			internal const string Pooling = "Cache Size";

			// Token: 0x04001297 RID: 4759
			internal static volatile bool _PoolSizeInit;

			// Token: 0x04001298 RID: 4760
			internal static int _PoolSize;

			// Token: 0x04001299 RID: 4761
			internal static volatile Dictionary<string, string> _Pool;

			// Token: 0x0400129A RID: 4762
			internal static object _PoolLock = new object();
		}
	}
}
