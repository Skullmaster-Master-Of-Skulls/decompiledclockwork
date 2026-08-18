using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TechnoPro.Common.Reports.Core.Common;
using TechnoPro.Common.Reports.ICore.Common;
using TechnoPro.Common.Reports.ICore.WebSettings;
using TechnoPro.Common.Reports.Public;
using TechnoPro.Common.Reports.Public.Entities.Database;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;
using TechnoPro.Common.Reports.Public.Entities.WebSettings;

namespace TechnoPro.Common.Reports.Core.WebSettings
{
	// Token: 0x02000002 RID: 2
	public class WebSettingReportManager : IWebSettingReportManager, IOperationContextRO, IBaseOperationContextRO<OperationContextRO>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public WebSettingReportManager(OperationContextRO opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002062 File Offset: 0x00000262
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000206A File Offset: 0x0000026A
		public OperationContextRO OpContext { get; set; }

		// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public string GetCustomWebSettingValue(eWebCustomSetting settingCode)
		{
			IDatabaseReportManager databaseReportManager = new DatabaseReportManager(this.OpContext);
			string result;
			using (SqlDataReader sqlDataReader = databaseReportManager.ExecuteReader(new QueryRequestRO
			{
				Parameters = new List<CommonParameterRO>
				{
					new CommonParameterRO
					{
						Name = "@settingCode",
						DbType = new DbType?(DbType.Int32),
						Value = settingCode
					}
				},
				Sql = "SELECT TOP 1 settingstringvalue FROM websettings2 WHERE settingcode=@settingCode"
			}))
			{
				bool flag = sqlDataReader == null || !sqlDataReader.Read();
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					byte[] array = (sqlDataReader["settingstringvalue"] is DBNull) ? null : ((byte[])sqlDataReader["settingstringvalue"]);
					bool flag2 = array == null;
					if (flag2)
					{
						result = string.Empty;
					}
					else
					{
						IEncryptionReportManager encryptionReportManager = new EncryptionReportManager(this.OpContext);
						result = encryptionReportManager.DecryptData(array);
					}
				}
			}
			return result;
		}
	}
}
