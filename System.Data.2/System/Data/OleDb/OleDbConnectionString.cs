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
	// Token: 0x02000248 RID: 584
	internal sealed class OleDbConnectionString : DbConnectionOptions
	{
		// Token: 0x060024D2 RID: 9426 RVA: 0x000FB200 File Offset: 0x000FA600
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
							this._expandedConnectionString = this._expandedConnectionString.Substring(0, num) + text2 + ";" + this._expandedConnectionString.Substring(num);
						}
					}
				}
				if (validate || ADP.IsEmpty(text2))
				{
					this.ActualConnectionString = this.ValidateConnectionString(connectionString);
				}
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x000FB2DC File Offset: 0x000FA6DC
		internal int ConnectTimeout
		{
			get
			{
				return base.ConvertValueToInt32("connect timeout", 15);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000FB2F8 File Offset: 0x000FA6F8
		internal string DataSource
		{
			get
			{
				return base.ConvertValueToString("data source", ADP.StrEmpty);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000FB318 File Offset: 0x000FA718
		internal string InitialCatalog
		{
			get
			{
				return base.ConvertValueToString("initial catalog", ADP.StrEmpty);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000FB338 File Offset: 0x000FA738
		internal string Provider
		{
			get
			{
				return base["provider"];
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x000FB350 File Offset: 0x000FA750
		internal int OleDbServices
		{
			get
			{
				return this._oledbServices;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x000FB364 File Offset: 0x000FA764
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x000FB378 File Offset: 0x000FA778
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

		// Token: 0x060024DA RID: 9434 RVA: 0x000FB38C File Offset: 0x000FA78C
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

		// Token: 0x060024DB RID: 9435 RVA: 0x000FB3C4 File Offset: 0x000FA7C4
		protected internal override string Expand()
		{
			if (this._expandedConnectionString != null)
			{
				return this._expandedConnectionString;
			}
			return base.Expand();
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000FB3E8 File Offset: 0x000FA7E8
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

		// Token: 0x060024DD RID: 9437 RVA: 0x000FB430 File Offset: 0x000FA830
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

		// Token: 0x060024DE RID: 9438 RVA: 0x000FB47C File Offset: 0x000FA87C
		internal bool GetSupportMultipleResults(OleDbConnection connection)
		{
			bool flag = this._supportMultipleResults;
			if (!this._hasSupportMultipleResults)
			{
				object dataSourcePropertyValue = connection.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 196);
				if (dataSourcePropertyValue is int)
				{
					flag = ((int)dataSourcePropertyValue != 0);
				}
				this._supportMultipleResults = flag;
				this._hasSupportMultipleResults = true;
			}
			return flag;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x000FB4CC File Offset: 0x000FA8CC
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

		// Token: 0x060024E0 RID: 9440 RVA: 0x000FB520 File Offset: 0x000FA920
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
						object poolLock = OleDbConnectionString.UDL._PoolLock;
						lock (poolLock)
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
						Dictionary<string, string> obj = dictionary;
						lock (obj)
						{
							dictionary[udlfilename] = text;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000FB604 File Offset: 0x000FAA04
		private static string LoadStringFromFileStorage(string udlfilename)
		{
			string text = null;
			Exception ex = null;
			try
			{
				int num = 2 * "﻿[oledb]\r\n; Everything after this line is an OLE DB initstring\r\n".Length;
				using (FileStream fileStream = new FileStream(udlfilename, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					long length = fileStream.Length;
					if (length < (long)num || length % 2L != 0L)
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

		// Token: 0x060024E2 RID: 9442 RVA: 0x000FB71C File Offset: 0x000FAB1C
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

		// Token: 0x060024E3 RID: 9443 RVA: 0x000FB918 File Offset: 0x000FAD18
		internal static bool IsMSDASQL(string progid)
		{
			return "msdasql" == progid || progid.StartsWith("msdasql.", StringComparison.Ordinal) || "microsoft ole db provider for odbc drivers" == progid;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000FB950 File Offset: 0x000FAD50
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

		// Token: 0x060024E5 RID: 9445 RVA: 0x000FB99C File Offset: 0x000FAD9C
		internal static void ReleaseObjectPool()
		{
			OleDbConnectionString.UDL._PoolSizeInit = false;
			OleDbConnectionString.UDL._Pool = null;
		}

		// Token: 0x040015B2 RID: 5554
		internal readonly bool PossiblePrompt;

		// Token: 0x040015B3 RID: 5555
		internal readonly string ActualConnectionString;

		// Token: 0x040015B4 RID: 5556
		private readonly string _expandedConnectionString;

		// Token: 0x040015B5 RID: 5557
		internal SchemaSupport[] _schemaSupport;

		// Token: 0x040015B6 RID: 5558
		internal int _sqlSupport;

		// Token: 0x040015B7 RID: 5559
		internal bool _supportMultipleResults;

		// Token: 0x040015B8 RID: 5560
		internal bool _supportIRow;

		// Token: 0x040015B9 RID: 5561
		internal bool _hasSqlSupport;

		// Token: 0x040015BA RID: 5562
		internal bool _hasSupportMultipleResults;

		// Token: 0x040015BB RID: 5563
		internal bool _hasSupportIRow;

		// Token: 0x040015BC RID: 5564
		private int _oledbServices;

		// Token: 0x040015BD RID: 5565
		internal UnsafeNativeMethods.IUnknownQueryInterface DangerousDataSourceIUnknownQueryInterface;

		// Token: 0x040015BE RID: 5566
		internal UnsafeNativeMethods.IDBInitializeInitialize DangerousIDBInitializeInitialize;

		// Token: 0x040015BF RID: 5567
		internal UnsafeNativeMethods.IDBCreateSessionCreateSession DangerousIDBCreateSessionCreateSession;

		// Token: 0x040015C0 RID: 5568
		internal UnsafeNativeMethods.IDBCreateCommandCreateCommand DangerousIDBCreateCommandCreateCommand;

		// Token: 0x040015C1 RID: 5569
		internal bool HaveQueriedForCreateCommand;

		// Token: 0x020003FF RID: 1023
		private static class UDL
		{
			// Token: 0x040021AC RID: 8620
			internal const string Header = "﻿[oledb]\r\n; Everything after this line is an OLE DB initstring\r\n";

			// Token: 0x040021AD RID: 8621
			internal const string Location = "SOFTWARE\\Microsoft\\DataAccess\\Udl Pooling";

			// Token: 0x040021AE RID: 8622
			internal const string Pooling = "Cache Size";

			// Token: 0x040021AF RID: 8623
			internal static volatile bool _PoolSizeInit;

			// Token: 0x040021B0 RID: 8624
			internal static int _PoolSize;

			// Token: 0x040021B1 RID: 8625
			internal static volatile Dictionary<string, string> _Pool;

			// Token: 0x040021B2 RID: 8626
			internal static object _PoolLock = new object();
		}
	}
}
