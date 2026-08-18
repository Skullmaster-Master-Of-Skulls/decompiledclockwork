using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using OracleInternal.Common;
using OracleInternal.Network;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000065 RID: 101
	public sealed class OracleDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0002F128 File Offset: 0x0002D328
		public override DataTable GetDataSources()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DataTable dataTable = null;
			try
			{
				dataTable = new DataTable("DataSource");
				dataTable.Columns.Add(new DataColumn("InstanceName", typeof(string)));
				dataTable.Columns.Add(new DataColumn("ServerName", typeof(string)));
				dataTable.Columns.Add(new DataColumn("ServiceName", typeof(string)));
				dataTable.Columns.Add(new DataColumn("Protocol", typeof(string)));
				dataTable.Columns.Add(new DataColumn("Port", typeof(string)));
				ProviderConfig.RefreshDataSources();
				foreach (object obj in AddressResolution.NamingAdapterMaps())
				{
					Hashtable hashtable = (Hashtable)obj;
					if (hashtable != null)
					{
						foreach (object obj2 in hashtable.Keys)
						{
							string text = (string)obj2;
							DataRow[] array = dataTable.Select("InstanceName='" + text + "'");
							if (array.Length == 0)
							{
								DataRow dataRow = dataTable.NewRow();
								try
								{
									string value = text;
									string nvString = (string)hashtable[text];
									dataRow["InstanceName"] = value;
									NVPair nvpair = NVNavigator.FindNVPairRecurse(NVFactory.CreateNVPair(nvString), AddressResolution.ADDRESS);
									if (nvpair != null)
									{
										NVPair nvpair2 = NVNavigator.FindNVPair(nvpair, AddressResolution.PROTOCOL);
										if (nvpair2 != null)
										{
											dataRow["Protocol"] = nvpair2.Atom;
										}
										nvpair2 = NVNavigator.FindNVPair(nvpair, AddressResolution.HOST);
										if (nvpair2 != null)
										{
											dataRow["ServerName"] = nvpair2.Atom;
										}
										nvpair2 = NVNavigator.FindNVPair(nvpair, AddressResolution.PORT);
										if (nvpair2 != null)
										{
											dataRow["Port"] = nvpair2.Atom;
										}
									}
									NVPair nvpair3 = NVNavigator.FindNVPairRecurse(NVFactory.CreateNVPair(nvString), AddressResolution.CONNECT_DATA);
									if (nvpair3 != null)
									{
										NVPair nvpair2 = NVNavigator.FindNVPair(nvpair3, AddressResolution.SERVICE_NAME);
										if (nvpair2 != null)
										{
											dataRow["ServiceName"] = nvpair2.Atom;
										}
										else
										{
											nvpair2 = NVNavigator.FindNVPair(nvpair3, AddressResolution.SID);
											if (nvpair2 != null)
											{
												dataRow["ServiceName"] = nvpair2.Atom;
											}
										}
									}
									dataTable.Rows.Add(dataRow);
								}
								catch (Exception)
								{
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return dataTable;
		}

		// Token: 0x04000639 RID: 1593
		private const string DATA_SOURCE = "DataSource";

		// Token: 0x0400063A RID: 1594
		private const string INSTANCE_NAME = "InstanceName";

		// Token: 0x0400063B RID: 1595
		private const string SERVER_NAME = "ServerName";

		// Token: 0x0400063C RID: 1596
		private const string SERVICE_NAME = "ServiceName";

		// Token: 0x0400063D RID: 1597
		private const string PROTOCOL = "Protocol";

		// Token: 0x0400063E RID: 1598
		private const string PORT = "Port";
	}
}
