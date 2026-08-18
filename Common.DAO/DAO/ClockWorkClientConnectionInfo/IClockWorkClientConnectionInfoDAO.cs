using System;

namespace TechnoPro.Common.DAO.ClockWorkClientConnectionInfo
{
	// Token: 0x0200009C RID: 156
	public interface IClockWorkClientConnectionInfoDAO
	{
		// Token: 0x0600040D RID: 1037
		string StoreXmlInStorageString(string Xml, string ConnectionString, string DbPassword);

		// Token: 0x0600040E RID: 1038
		string GetXmlFromStorageString(string StorageString);
	}
}
