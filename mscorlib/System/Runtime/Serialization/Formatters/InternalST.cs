using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x020007B9 RID: 1977
	[ComVisible(true)]
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0x002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293", Name = "System.Runtime.Serialization.Formatters.Soap")]
	public sealed class InternalST
	{
		// Token: 0x06004677 RID: 18039 RVA: 0x000F07BA File Offset: 0x000EF7BA
		private InternalST()
		{
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x000F07C2 File Offset: 0x000EF7C2
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x000F07C4 File Offset: 0x000EF7C4
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("Soap");
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x000F07D0 File Offset: 0x000EF7D0
		[Conditional("SER_LOGGING")]
		public static void Soap(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			messages[0] = messages[0] + " ";
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x000F0807 File Offset: 0x000EF807
		[Conditional("_DEBUG")]
		public static void SoapAssert(bool condition, string message)
		{
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x000F0809 File Offset: 0x000EF809
		public static void SerializationSetValue(FieldInfo fi, object target, object value)
		{
			if (fi == null)
			{
				throw new ArgumentNullException("fi");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			FormatterServices.SerializationSetValue(fi, target, value);
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x000F083D File Offset: 0x000EF83D
		public static Assembly LoadAssemblyFromString(string assemblyString)
		{
			return FormatterServices.LoadAssemblyFromString(assemblyString);
		}
	}
}
