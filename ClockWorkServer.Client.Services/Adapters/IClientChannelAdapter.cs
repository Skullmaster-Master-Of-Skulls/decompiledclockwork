using System;
using System.Reflection;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Win32;
using WCFExtrasPlus.Soap;

namespace TechnoPro.ClockWorkServer.Client.Services.Adapters
{
	// Token: 0x02000179 RID: 377
	public static class IClientChannelAdapter
	{
		// Token: 0x06000E8C RID: 3724 RVA: 0x00026092 File Offset: 0x00024292
		public static void SetSessionParametersHeader(this IClientChannel channel, Token sessionTicket)
		{
			ClientCredential.CurrentInstance.Validate(ClientCredential.CurrentInstance.SessionTicket);
			channel.SetHeader("operationDetails", new OperationData
			{
				ClientParameters = IClientChannelAdapter.GetClientParameters(),
				SessionToken = sessionTicket
			});
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000260CF File Offset: 0x000242CF
		public static void SetClientParametersHeader(this IClientChannel channel)
		{
			channel.SetHeader("clientDetails", IClientChannelAdapter.GetClientParameters());
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000260E4 File Offset: 0x000242E4
		private static ClientParametersDTO GetClientParameters()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			return new ClientParametersDTO
			{
				{
					"IP",
					TechnoPro.Common.Win32.Environment.GetIPAddress()
				},
				{
					"APPNAME",
					(entryAssembly != null) ? entryAssembly.GetName().Name : "ClockWorkServer"
				},
				{
					"ADDR_SIZE",
					(IntPtr.Size == 4) ? "32" : "64"
				},
				{
					"NET_VERSIONS",
					TechnoPro.Common.Win32.Environment.GetDotNetVersionsInstalled().CommaSeparatedValuesWithoutSpace<DotNetVersion>()
				}
			};
		}
	}
}
