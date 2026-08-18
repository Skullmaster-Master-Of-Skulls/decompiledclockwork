using System;
using System.Configuration;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x0200049A RID: 1178
	internal static class DiagnosticsConfiguration
	{
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000C5F10 File Offset: 0x000C4110
		internal static SwitchElementsCollection SwitchSettings
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.Switches;
				}
				return null;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x000C5F38 File Offset: 0x000C4138
		internal static bool AssertUIEnabled
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Assert == null || systemDiagnosticsSection.Assert.AssertUIEnabled;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x000C5F6C File Offset: 0x000C416C
		internal static string ConfigFilePath
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.ElementInformation.Source;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000C5F9C File Offset: 0x000C419C
		internal static string LogFileName
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Assert != null)
				{
					return systemDiagnosticsSection.Assert.LogFileName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000C5FD4 File Offset: 0x000C41D4
		internal static bool AutoFlush
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null && systemDiagnosticsSection.Trace.AutoFlush;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x000C6008 File Offset: 0x000C4208
		internal static bool UseGlobalLock
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Trace == null || systemDiagnosticsSection.Trace.UseGlobalLock;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000C603C File Offset: 0x000C423C
		internal static int IndentSize
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null)
				{
					return systemDiagnosticsSection.Trace.IndentSize;
				}
				return 4;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000C6070 File Offset: 0x000C4270
		internal static int PerfomanceCountersFileMappingSize
		{
			get
			{
				int num = 0;
				while (!DiagnosticsConfiguration.CanInitialize() && num <= 5)
				{
					if (num == 5)
					{
						return 524288;
					}
					Thread.Sleep(200);
					num++;
				}
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.PerfCounters != null)
				{
					int num2 = systemDiagnosticsSection.PerfCounters.FileMappingSize;
					if (num2 < 32768)
					{
						num2 = 32768;
					}
					if (num2 > 33554432)
					{
						num2 = 33554432;
					}
					return num2;
				}
				return 524288;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000C60EC File Offset: 0x000C42EC
		internal static ListenerElementsCollection SharedListeners
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.SharedListeners;
				}
				return null;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x000C6114 File Offset: 0x000C4314
		internal static SourceElementsCollection Sources
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Sources != null)
				{
					return systemDiagnosticsSection.Sources;
				}
				return null;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000C6141 File Offset: 0x000C4341
		internal static SystemDiagnosticsSection SystemDiagnosticsSection
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				return DiagnosticsConfiguration.configSection;
			}
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000C6150 File Offset: 0x000C4350
		private static SystemDiagnosticsSection GetConfigSection()
		{
			return (SystemDiagnosticsSection)PrivilegedConfigurationManager.GetSection("system.diagnostics");
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000C616E File Offset: 0x000C436E
		internal static bool IsInitializing()
		{
			return DiagnosticsConfiguration.initState == InitState.Initializing;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000C617A File Offset: 0x000C437A
		internal static bool IsInitialized()
		{
			return DiagnosticsConfiguration.initState == InitState.Initialized;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000C6186 File Offset: 0x000C4386
		internal static bool CanInitialize()
		{
			return DiagnosticsConfiguration.initState != InitState.Initializing && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000C61A4 File Offset: 0x000C43A4
		internal static void Initialize()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				if (DiagnosticsConfiguration.initState == InitState.NotInitialized && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress)
				{
					DiagnosticsConfiguration.initState = InitState.Initializing;
					try
					{
						DiagnosticsConfiguration.configSection = DiagnosticsConfiguration.GetConfigSection();
					}
					finally
					{
						DiagnosticsConfiguration.initState = InitState.Initialized;
					}
				}
			}
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000C6220 File Offset: 0x000C4420
		internal static void Refresh()
		{
			ConfigurationManager.RefreshSection("system.diagnostics");
			SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
			if (systemDiagnosticsSection != null)
			{
				if (systemDiagnosticsSection.Switches != null)
				{
					foreach (object obj in systemDiagnosticsSection.Switches)
					{
						SwitchElement switchElement = (SwitchElement)obj;
						switchElement.ResetProperties();
					}
				}
				if (systemDiagnosticsSection.SharedListeners != null)
				{
					foreach (object obj2 in systemDiagnosticsSection.SharedListeners)
					{
						ListenerElement listenerElement = (ListenerElement)obj2;
						listenerElement.ResetProperties();
					}
				}
				if (systemDiagnosticsSection.Sources != null)
				{
					foreach (object obj3 in systemDiagnosticsSection.Sources)
					{
						SourceElement sourceElement = (SourceElement)obj3;
						sourceElement.ResetProperties();
					}
				}
			}
			DiagnosticsConfiguration.configSection = null;
			DiagnosticsConfiguration.initState = InitState.NotInitialized;
			DiagnosticsConfiguration.Initialize();
		}

		// Token: 0x04002697 RID: 9879
		private static volatile SystemDiagnosticsSection configSection;

		// Token: 0x04002698 RID: 9880
		private static volatile InitState initState;
	}
}
