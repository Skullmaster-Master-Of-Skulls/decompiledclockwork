using System;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005BA RID: 1466
	public static class ClockWorkServerPreferredConnectionInfoAdapter
	{
		// Token: 0x06002F5D RID: 12125 RVA: 0x00034EA8 File Offset: 0x000330A8
		public static int GetPort(this ClockWorkServerPreferredConnectionInfo conn)
		{
			bool flag = conn.BindingType == eBindingType.NetTcpBinding;
			int result;
			if (flag)
			{
				result = ((conn.Port > 0) ? conn.Port : eBindingType.NetTcpBinding.GetUriScheme().GetDefaultPort());
			}
			else
			{
				bool flag2 = conn.BindingType == eBindingType.HttpBinding;
				if (flag2)
				{
					result = ((conn.ExternalPort > 0) ? conn.ExternalPort : eBindingType.HttpBinding.GetUriScheme().GetDefaultPort());
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}
	}
}
