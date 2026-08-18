using System;
using System.Text;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.ClientManager.Core.Adapters
{
	// Token: 0x020000A6 RID: 166
	public static class ClockWorkServerAutoDiscoveryAdapter
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0001BD98 File Offset: 0x00019F98
		public static Uri ChangeDiscoveryEndpoint(this Uri endpoint, eBindingType bindingType)
		{
			string[] segments = endpoint.Segments;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(bindingType.GetUriScheme());
			stringBuilder.Append("://");
			stringBuilder.Append(endpoint.Host);
			for (int i = 0; i < segments.Length - 1; i++)
			{
				stringBuilder.Append(segments[i]);
			}
			stringBuilder.Append((bindingType == eBindingType.NetTcpBinding) ? "netTcp" : "basicHttp");
			return new Uri(stringBuilder.ToString());
		}
	}
}
