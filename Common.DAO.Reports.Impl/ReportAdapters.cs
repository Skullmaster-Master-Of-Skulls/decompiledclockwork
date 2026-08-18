using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.DAO.Reports.Impl
{
	// Token: 0x02000007 RID: 7
	public static class ReportAdapters
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002500 File Offset: 0x00000700
		public static DbParameter ConvertToDbParameter(this ReportParameter parameter, DatabaseLayer DatabaseManager)
		{
			bool flag = parameter == null;
			DbParameter result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string pName = "@" + parameter.Name;
				object value = parameter.Value;
				bool flag2 = value == null;
				if (flag2)
				{
					result = DatabaseManager.GetParameter(pName, DbType.String, DBNull.Value);
				}
				else
				{
					bool flag3 = value is DateTime;
					if (flag3)
					{
						result = DatabaseManager.GetParameter(pName, DbType.DateTime, value);
					}
					else
					{
						bool flag4 = value is int;
						if (flag4)
						{
							result = DatabaseManager.GetParameter(pName, DbType.Int32, value);
						}
						else
						{
							bool flag5 = value is byte[];
							if (flag5)
							{
								result = DatabaseManager.GetParameter(pName, DbType.Binary, value);
							}
							else
							{
								bool flag6 = value is bool;
								if (flag6)
								{
									result = DatabaseManager.GetParameter(pName, DbType.Boolean, value);
								}
								else
								{
									result = DatabaseManager.GetParameter(pName, DbType.String, value.ToString());
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
