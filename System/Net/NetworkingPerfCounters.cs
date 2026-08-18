using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Net
{
	// Token: 0x020004F5 RID: 1269
	internal static class NetworkingPerfCounters
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x000A3448 File Offset: 0x000A2448
		internal static void Initialize()
		{
			if (!NetworkingPerfCounters.initialized)
			{
				lock (NetworkingPerfCounters.syncObject)
				{
					if (!NetworkingPerfCounters.initialized)
					{
						if (ComNetOS.IsWin2K)
						{
							PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PermissionState.Unrestricted);
							performanceCounterPermission.Assert();
							try
							{
								string instanceName = NetworkingPerfCounters.GetInstanceName();
								NetworkingPerfCounters.ConnectionsEstablished = new PerformanceCounter();
								NetworkingPerfCounters.ConnectionsEstablished.CategoryName = ".NET CLR Networking";
								NetworkingPerfCounters.ConnectionsEstablished.CounterName = "Connections Established";
								NetworkingPerfCounters.ConnectionsEstablished.InstanceName = instanceName;
								NetworkingPerfCounters.ConnectionsEstablished.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
								NetworkingPerfCounters.ConnectionsEstablished.ReadOnly = false;
								NetworkingPerfCounters.ConnectionsEstablished.RawValue = 0L;
								NetworkingPerfCounters.BytesReceived = new PerformanceCounter();
								NetworkingPerfCounters.BytesReceived.CategoryName = ".NET CLR Networking";
								NetworkingPerfCounters.BytesReceived.CounterName = "Bytes Received";
								NetworkingPerfCounters.BytesReceived.InstanceName = instanceName;
								NetworkingPerfCounters.BytesReceived.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
								NetworkingPerfCounters.BytesReceived.ReadOnly = false;
								NetworkingPerfCounters.BytesReceived.RawValue = 0L;
								NetworkingPerfCounters.BytesSent = new PerformanceCounter();
								NetworkingPerfCounters.BytesSent.CategoryName = ".NET CLR Networking";
								NetworkingPerfCounters.BytesSent.CounterName = "Bytes Sent";
								NetworkingPerfCounters.BytesSent.InstanceName = instanceName;
								NetworkingPerfCounters.BytesSent.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
								NetworkingPerfCounters.BytesSent.ReadOnly = false;
								NetworkingPerfCounters.BytesSent.RawValue = 0L;
								NetworkingPerfCounters.DatagramsReceived = new PerformanceCounter();
								NetworkingPerfCounters.DatagramsReceived.CategoryName = ".NET CLR Networking";
								NetworkingPerfCounters.DatagramsReceived.CounterName = "Datagrams Received";
								NetworkingPerfCounters.DatagramsReceived.InstanceName = instanceName;
								NetworkingPerfCounters.DatagramsReceived.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
								NetworkingPerfCounters.DatagramsReceived.ReadOnly = false;
								NetworkingPerfCounters.DatagramsReceived.RawValue = 0L;
								NetworkingPerfCounters.DatagramsSent = new PerformanceCounter();
								NetworkingPerfCounters.DatagramsSent.CategoryName = ".NET CLR Networking";
								NetworkingPerfCounters.DatagramsSent.CounterName = "Datagrams Sent";
								NetworkingPerfCounters.DatagramsSent.InstanceName = instanceName;
								NetworkingPerfCounters.DatagramsSent.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
								NetworkingPerfCounters.DatagramsSent.ReadOnly = false;
								NetworkingPerfCounters.DatagramsSent.RawValue = 0L;
								NetworkingPerfCounters.globalConnectionsEstablished = new PerformanceCounter(".NET CLR Networking", "Connections Established", "_Global_", false);
								NetworkingPerfCounters.globalBytesReceived = new PerformanceCounter(".NET CLR Networking", "Bytes Received", "_Global_", false);
								NetworkingPerfCounters.globalBytesSent = new PerformanceCounter(".NET CLR Networking", "Bytes Sent", "_Global_", false);
								NetworkingPerfCounters.globalDatagramsReceived = new PerformanceCounter(".NET CLR Networking", "Datagrams Received", "_Global_", false);
								NetworkingPerfCounters.globalDatagramsSent = new PerformanceCounter(".NET CLR Networking", "Datagrams Sent", "_Global_", false);
								AppDomain.CurrentDomain.DomainUnload += NetworkingPerfCounters.ExitOrUnloadEventHandler;
								AppDomain.CurrentDomain.ProcessExit += NetworkingPerfCounters.ExitOrUnloadEventHandler;
								AppDomain.CurrentDomain.UnhandledException += NetworkingPerfCounters.ExceptionEventHandler;
							}
							catch (Win32Exception)
							{
							}
							catch (InvalidOperationException)
							{
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
						}
						NetworkingPerfCounters.initialized = true;
					}
				}
			}
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000A3784 File Offset: 0x000A2784
		private static void ExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
		{
			if (e.IsTerminating)
			{
				NetworkingPerfCounters.Cleanup();
			}
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000A3793 File Offset: 0x000A2793
		private static void ExitOrUnloadEventHandler(object sender, EventArgs e)
		{
			NetworkingPerfCounters.Cleanup();
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000A379C File Offset: 0x000A279C
		private static void Cleanup()
		{
			PerformanceCounter performanceCounter = NetworkingPerfCounters.ConnectionsEstablished;
			if (performanceCounter != null)
			{
				performanceCounter.RemoveInstance();
			}
			performanceCounter = NetworkingPerfCounters.BytesReceived;
			if (performanceCounter != null)
			{
				performanceCounter.RemoveInstance();
			}
			performanceCounter = NetworkingPerfCounters.BytesSent;
			if (performanceCounter != null)
			{
				performanceCounter.RemoveInstance();
			}
			performanceCounter = NetworkingPerfCounters.DatagramsReceived;
			if (performanceCounter != null)
			{
				performanceCounter.RemoveInstance();
			}
			performanceCounter = NetworkingPerfCounters.DatagramsSent;
			if (performanceCounter != null)
			{
				performanceCounter.RemoveInstance();
			}
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000A37F4 File Offset: 0x000A27F4
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static string GetAssemblyName()
		{
			string result = null;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				AssemblyName name = entryAssembly.GetName();
				if (name != null)
				{
					result = name.Name;
				}
			}
			return result;
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000A3820 File Offset: 0x000A2820
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private static string GetInstanceName()
		{
			string text = NetworkingPerfCounters.GetAssemblyName();
			if (text == null || text.Length == 0)
			{
				text = AppDomain.CurrentDomain.FriendlyName;
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			int i = 0;
			while (i < stringBuilder.Length)
			{
				char c = stringBuilder[i];
				if (c <= ')')
				{
					if (c == '#')
					{
						goto IL_76;
					}
					switch (c)
					{
					case '(':
						stringBuilder[i] = '[';
						break;
					case ')':
						stringBuilder[i] = ']';
						break;
					}
				}
				else if (c == '/' || c == '\\')
				{
					goto IL_76;
				}
				IL_7F:
				i++;
				continue;
				IL_76:
				stringBuilder[i] = '_';
				goto IL_7F;
			}
			return string.Format(CultureInfo.CurrentCulture, "{0}[{1}]", new object[]
			{
				stringBuilder.ToString(),
				Process.GetCurrentProcess().Id
			});
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000A38F1 File Offset: 0x000A28F1
		internal static void IncrementConnectionsEstablished()
		{
			if (NetworkingPerfCounters.ConnectionsEstablished != null)
			{
				NetworkingPerfCounters.ConnectionsEstablished.Increment();
			}
			if (NetworkingPerfCounters.globalConnectionsEstablished != null)
			{
				NetworkingPerfCounters.globalConnectionsEstablished.Increment();
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000A3917 File Offset: 0x000A2917
		internal static void AddBytesReceived(int increment)
		{
			if (NetworkingPerfCounters.BytesReceived != null)
			{
				NetworkingPerfCounters.BytesReceived.IncrementBy((long)increment);
			}
			if (NetworkingPerfCounters.globalBytesReceived != null)
			{
				NetworkingPerfCounters.globalBytesReceived.IncrementBy((long)increment);
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000A3941 File Offset: 0x000A2941
		internal static void AddBytesSent(int increment)
		{
			if (NetworkingPerfCounters.BytesSent != null)
			{
				NetworkingPerfCounters.BytesSent.IncrementBy((long)increment);
			}
			if (NetworkingPerfCounters.globalBytesSent != null)
			{
				NetworkingPerfCounters.globalBytesSent.IncrementBy((long)increment);
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000A396B File Offset: 0x000A296B
		internal static void IncrementDatagramsReceived()
		{
			if (NetworkingPerfCounters.DatagramsReceived != null)
			{
				NetworkingPerfCounters.DatagramsReceived.Increment();
			}
			if (NetworkingPerfCounters.globalDatagramsReceived != null)
			{
				NetworkingPerfCounters.globalDatagramsReceived.Increment();
			}
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000A3991 File Offset: 0x000A2991
		internal static void IncrementDatagramsSent()
		{
			if (NetworkingPerfCounters.DatagramsSent != null)
			{
				NetworkingPerfCounters.DatagramsSent.Increment();
			}
			if (NetworkingPerfCounters.globalDatagramsSent != null)
			{
				NetworkingPerfCounters.globalDatagramsSent.Increment();
			}
		}

		// Token: 0x040026CF RID: 9935
		private const string CategoryName = ".NET CLR Networking";

		// Token: 0x040026D0 RID: 9936
		private const string ConnectionsEstablishedName = "Connections Established";

		// Token: 0x040026D1 RID: 9937
		private const string BytesReceivedName = "Bytes Received";

		// Token: 0x040026D2 RID: 9938
		private const string BytesSentName = "Bytes Sent";

		// Token: 0x040026D3 RID: 9939
		private const string DatagramsReceivedName = "Datagrams Received";

		// Token: 0x040026D4 RID: 9940
		private const string DatagramsSentName = "Datagrams Sent";

		// Token: 0x040026D5 RID: 9941
		private const string GlobalInstanceName = "_Global_";

		// Token: 0x040026D6 RID: 9942
		private static PerformanceCounter ConnectionsEstablished;

		// Token: 0x040026D7 RID: 9943
		private static PerformanceCounter BytesReceived;

		// Token: 0x040026D8 RID: 9944
		private static PerformanceCounter BytesSent;

		// Token: 0x040026D9 RID: 9945
		private static PerformanceCounter DatagramsReceived;

		// Token: 0x040026DA RID: 9946
		private static PerformanceCounter DatagramsSent;

		// Token: 0x040026DB RID: 9947
		private static PerformanceCounter globalConnectionsEstablished;

		// Token: 0x040026DC RID: 9948
		private static PerformanceCounter globalBytesReceived;

		// Token: 0x040026DD RID: 9949
		private static PerformanceCounter globalBytesSent;

		// Token: 0x040026DE RID: 9950
		private static PerformanceCounter globalDatagramsReceived;

		// Token: 0x040026DF RID: 9951
		private static PerformanceCounter globalDatagramsSent;

		// Token: 0x040026E0 RID: 9952
		private static object syncObject = new object();

		// Token: 0x040026E1 RID: 9953
		private static bool initialized = false;
	}
}
