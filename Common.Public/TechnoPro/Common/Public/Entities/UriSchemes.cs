using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000F5 RID: 245
	public static class UriSchemes
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public static int GetDefaultPort(this string bindingProtocol)
		{
			int result;
			if (!(bindingProtocol == "http"))
			{
				if (!(bindingProtocol == "https"))
				{
					if (!(bindingProtocol == "net.tcp"))
					{
						if (!(bindingProtocol == "net.msmq"))
						{
							result = 808;
						}
						else
						{
							result = 1801;
						}
					}
					else
					{
						result = 808;
					}
				}
				else
				{
					result = 443;
				}
			}
			else
			{
				result = 80;
			}
			return result;
		}

		// Token: 0x04000262 RID: 610
		public const string NetTcp = "net.tcp";

		// Token: 0x04000263 RID: 611
		public const string Http = "http";

		// Token: 0x04000264 RID: 612
		public const string Https = "https";

		// Token: 0x04000265 RID: 613
		public const string NetMsmq = "net.msmq";

		// Token: 0x04000266 RID: 614
		public const string NetPipe = "net.pipe";
	}
}
