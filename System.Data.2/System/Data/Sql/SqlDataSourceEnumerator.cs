using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Data.Sql
{
	// Token: 0x0200014B RID: 331
	public sealed class SqlDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x0600135E RID: 4958 RVA: 0x0009A13C File Offset: 0x0009953C
		private SqlDataSourceEnumerator()
		{
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0009A150 File Offset: 0x00099550
		public static SqlDataSourceEnumerator Instance
		{
			get
			{
				return SqlDataSourceEnumerator.SingletonInstance;
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0009A164 File Offset: 0x00099564
		public override DataTable GetDataSources()
		{
			new NamedPermissionSet("FullTrust").Demand();
			char[] array = null;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1024;
			int num2 = 0;
			array = new char[num];
			bool flag = true;
			bool flag2 = false;
			IntPtr intPtr = ADP.PtrZero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.timeoutTime = TdsParserStaticMethods.GetTimeoutSeconds(30);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = SNINativeMethodWrapper.SNIServerEnumOpen();
				}
				if (ADP.PtrZero != intPtr)
				{
					while (flag && !TdsParserStaticMethods.TimeoutHasExpired(this.timeoutTime))
					{
						num2 = SNINativeMethodWrapper.SNIServerEnumRead(intPtr, array, num, ref flag);
						if (num2 > num)
						{
							flag2 = true;
							flag = false;
						}
						else if (0 < num2)
						{
							stringBuilder.Append(array, 0, num2);
						}
					}
				}
			}
			finally
			{
				if (ADP.PtrZero != intPtr)
				{
					SNINativeMethodWrapper.SNIServerEnumClose(intPtr);
				}
			}
			if (flag2)
			{
				Bid.Trace("<sc.SqlDataSourceEnumerator.GetDataSources|ERR> GetDataSources:SNIServerEnumRead returned bad length, requested %d, received %d", num, num2);
				throw ADP.ArgumentOutOfRange("readLength");
			}
			return SqlDataSourceEnumerator.ParseServerEnumString(stringBuilder.ToString());
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0009A27C File Offset: 0x0009967C
		private static DataTable ParseServerEnumString(string serverInstances)
		{
			DataTable dataTable = new DataTable("SqlDataSources");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("ServerName", typeof(string));
			dataTable.Columns.Add("InstanceName", typeof(string));
			dataTable.Columns.Add("IsClustered", typeof(string));
			dataTable.Columns.Add("Version", typeof(string));
			string text = null;
			string text2 = null;
			string text3 = null;
			string value = null;
			foreach (string text4 in serverInstances.Split(new char[1]))
			{
				string text5 = text4.Trim(new char[1]);
				if (text5.Length != 0)
				{
					foreach (string text6 in text5.Split(new char[]
					{
						';'
					}))
					{
						if (text == null)
						{
							foreach (string text7 in text6.Split(new char[]
							{
								'\\'
							}))
							{
								if (text == null)
								{
									text = text7;
								}
								else
								{
									text2 = text7;
								}
							}
						}
						else if (text3 == null)
						{
							text3 = text6.Substring(SqlDataSourceEnumerator._clusterLength);
						}
						else
						{
							value = text6.Substring(SqlDataSourceEnumerator._versionLength);
						}
					}
					string text8 = "ServerName='" + text + "'";
					if (!ADP.IsEmpty(text2))
					{
						text8 = text8 + " AND InstanceName='" + text2 + "'";
					}
					if (dataTable.Select(text8).Length == 0)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = text;
						dataRow[1] = text2;
						dataRow[2] = text3;
						dataRow[3] = value;
						dataTable.Rows.Add(dataRow);
					}
					text = null;
					text2 = null;
					text3 = null;
					value = null;
				}
			}
			foreach (object obj in dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				dataColumn.ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x04000D30 RID: 3376
		private static readonly SqlDataSourceEnumerator SingletonInstance = new SqlDataSourceEnumerator();

		// Token: 0x04000D31 RID: 3377
		internal const string ServerName = "ServerName";

		// Token: 0x04000D32 RID: 3378
		internal const string InstanceName = "InstanceName";

		// Token: 0x04000D33 RID: 3379
		internal const string IsClustered = "IsClustered";

		// Token: 0x04000D34 RID: 3380
		internal const string Version = "Version";

		// Token: 0x04000D35 RID: 3381
		private const int timeoutSeconds = 30;

		// Token: 0x04000D36 RID: 3382
		private long timeoutTime;

		// Token: 0x04000D37 RID: 3383
		private static string _Version = "Version:";

		// Token: 0x04000D38 RID: 3384
		private static string _Cluster = "Clustered:";

		// Token: 0x04000D39 RID: 3385
		private static int _clusterLength = SqlDataSourceEnumerator._Cluster.Length;

		// Token: 0x04000D3A RID: 3386
		private static int _versionLength = SqlDataSourceEnumerator._Version.Length;
	}
}
