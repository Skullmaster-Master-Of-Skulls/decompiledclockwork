using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000519 RID: 1305
	[ComVisible(true)]
	public class RuntimeEnvironment
	{
		// Token: 0x060032B4 RID: 12980
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetModuleFileName();

		// Token: 0x060032B5 RID: 12981
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeveloperPath();

		// Token: 0x060032B6 RID: 12982
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetHostBindingFile();

		// Token: 0x060032B7 RID: 12983
		[DllImport("mscoree.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		private static extern int GetCORVersion(StringBuilder sb, int BufferLength, ref int retLength);

		// Token: 0x060032B8 RID: 12984
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FromGlobalAccessCache(Assembly a);

		// Token: 0x060032B9 RID: 12985 RVA: 0x000AB504 File Offset: 0x000AA504
		public static string GetSystemVersion()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			int num = 0;
			if (RuntimeEnvironment.GetCORVersion(stringBuilder, 256, ref num) == 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000AB538 File Offset: 0x000AA538
		public static string GetRuntimeDirectory()
		{
			string runtimeDirectoryImpl = RuntimeEnvironment.GetRuntimeDirectoryImpl();
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, runtimeDirectoryImpl).Demand();
			return runtimeDirectoryImpl;
		}

		// Token: 0x060032BB RID: 12987
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetRuntimeDirectoryImpl();

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x000AB558 File Offset: 0x000AA558
		public static string SystemConfigurationFile
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				stringBuilder.Append(RuntimeEnvironment.GetRuntimeDirectory());
				stringBuilder.Append(AppDomainSetup.RuntimeConfigurationFile);
				string text = stringBuilder.ToString();
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				return text;
			}
		}
	}
}
