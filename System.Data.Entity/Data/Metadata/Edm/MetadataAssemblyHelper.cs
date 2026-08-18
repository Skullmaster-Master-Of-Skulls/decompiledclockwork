using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.EntityModel.SchemaObjectModel;
using System.IO;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000219 RID: 537
	internal static class MetadataAssemblyHelper
	{
		// Token: 0x06002328 RID: 9000 RVA: 0x0007CD38 File Offset: 0x0007AF38
		internal static Assembly SafeLoadReferencedAssembly(AssemblyName assemblyName)
		{
			Assembly result = null;
			try
			{
				result = Assembly.Load(assemblyName);
			}
			catch (FileNotFoundException)
			{
			}
			return result;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0007CD64 File Offset: 0x0007AF64
		private static bool ComputeShouldFilterAssembly(Assembly assembly)
		{
			AssemblyName assemblyName = new AssemblyName(assembly.FullName);
			return MetadataAssemblyHelper.ShouldFilterAssembly(assemblyName);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0007CD83 File Offset: 0x0007AF83
		internal static bool ShouldFilterAssembly(Assembly assembly)
		{
			return MetadataAssemblyHelper._filterAssemblyCacheByAssembly.Evaluate(assembly);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0007CD90 File Offset: 0x0007AF90
		private static bool ShouldFilterAssembly(AssemblyName assemblyName)
		{
			return MetadataAssemblyHelper.ArePublicKeyTokensEqual(assemblyName.GetPublicKeyToken(), MetadataAssemblyHelper.EcmaPublicKeyToken) || MetadataAssemblyHelper.ArePublicKeyTokensEqual(assemblyName.GetPublicKeyToken(), MetadataAssemblyHelper.MsPublicKeyToken);
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x0007CDB8 File Offset: 0x0007AFB8
		private static bool ArePublicKeyTokensEqual(byte[] left, byte[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0007CDE8 File Offset: 0x0007AFE8
		internal static IEnumerable<Assembly> GetNonSystemReferencedAssemblies(Assembly assembly)
		{
			foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
			{
				if (!MetadataAssemblyHelper.ShouldFilterAssembly(assemblyName))
				{
					Assembly assembly2 = MetadataAssemblyHelper.SafeLoadReferencedAssembly(assemblyName);
					if (assembly2 != null)
					{
						yield return assembly2;
					}
				}
			}
			AssemblyName[] array = null;
			yield break;
		}

		// Token: 0x04000FA2 RID: 4002
		private static byte[] EcmaPublicKeyToken = ScalarType.ConvertToByteArray("b77a5c561934e089");

		// Token: 0x04000FA3 RID: 4003
		private static byte[] MsPublicKeyToken = ScalarType.ConvertToByteArray("b03f5f7f11d50a3a");

		// Token: 0x04000FA4 RID: 4004
		private static Memoizer<Assembly, bool> _filterAssemblyCacheByAssembly = new Memoizer<Assembly, bool>(new Func<Assembly, bool>(MetadataAssemblyHelper.ComputeShouldFilterAssembly), EqualityComparer<Assembly>.Default);
	}
}
