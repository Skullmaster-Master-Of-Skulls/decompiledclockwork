using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Data.Sql
{
	// Token: 0x02000280 RID: 640
	public sealed class SqlDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x06002190 RID: 8592 RVA: 0x002874C8 File Offset: 0x002868C8
		private SqlDataSourceEnumerator()
		{
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x002874E8 File Offset: 0x002868E8
		public static SqlDataSourceEnumerator Instance
		{
			get
			{
				return SqlDataSourceEnumerator.SingletonInstance;
			}
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00287508 File Offset: 0x00286908
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

		// Token: 0x06002193 RID: 8595 RVA: 0x00287628 File Offset: 0x00286A28
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
			char[] separator = new char[1];
			foreach (string text4 in serverInstances.Split(separator))
			{
				string text5 = text4;
				char[] trimChars = new char[1];
				string text6 = text5.Trim(trimChars);
				if (text6.Length != 0)
				{
					foreach (string text7 in text6.Split(new char[]
					{
						';'
					}))
					{
						if (text == null)
						{
							foreach (string text8 in text7.Split(new char[]
							{
								'\\'
							}))
							{
								if (text == null)
								{
									text = text8;
								}
								else
								{
									text2 = text8;
								}
							}
						}
						else if (text3 == null)
						{
							text3 = text7.Substring(SqlDataSourceEnumerator._clusterLength);
						}
						else
						{
							value = text7.Substring(SqlDataSourceEnumerator._versionLength);
						}
					}
					string text9 = "ServerName='" + text + "'";
					if (!ADP.IsEmpty(text2))
					{
						text9 = text9 + " AND InstanceName='" + text2 + "'";
					}
					if (dataTable.Select(text9).Length == 0)
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

		// Token: 0x04001611 RID: 5649
		internal const string ServerName = "ServerName";

		// Token: 0x04001612 RID: 5650
		internal const string InstanceName = "InstanceName";

		// Token: 0x04001613 RID: 5651
		internal const string IsClustered = "IsClustered";

		// Token: 0x04001614 RID: 5652
		internal const string Version = "Version";

		// Token: 0x04001615 RID: 5653
		private const int timeoutSeconds = 30;

		// Token: 0x04001616 RID: 5654
		private static readonly SqlDataSourceEnumerator SingletonInstance = new SqlDataSourceEnumerator();

		// Token: 0x04001617 RID: 5655
		private long timeoutTime;

		// Token: 0x04001618 RID: 5656
		private static string _Version = "Version:";

		// Token: 0x04001619 RID: 5657
		private static string _Cluster = "Clustered:";

		// Token: 0x0400161A RID: 5658
		private static int _clusterLength = SqlDataSourceEnumerator._Cluster.Length;

		// Token: 0x0400161B RID: 5659
		private static int _versionLength = SqlDataSourceEnumerator._Version.Length;
	}
}
