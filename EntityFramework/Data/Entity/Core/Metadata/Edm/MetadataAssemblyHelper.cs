using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.SchemaObjectModel;
using System.IO;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000518 RID: 1304
	internal static class MetadataAssemblyHelper
	{
		// Token: 0x06003116 RID: 12566 RVA: 0x000EAC28 File Offset: 0x000E8E28
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
			catch (FileLoadException)
			{
			}
			return result;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000EAC64 File Offset: 0x000E8E64
		private static bool ComputeShouldFilterAssembly(Assembly assembly)
		{
			AssemblyName assemblyName = new AssemblyName(assembly.FullName);
			return MetadataAssemblyHelper.ShouldFilterAssembly(assemblyName);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000EAC83 File Offset: 0x000E8E83
		internal static bool ShouldFilterAssembly(Assembly assembly)
		{
			return MetadataAssemblyHelper._filterAssemblyCacheByAssembly.Evaluate(assembly);
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000EAC90 File Offset: 0x000E8E90
		private static bool ShouldFilterAssembly(AssemblyName assemblyName)
		{
			return MetadataAssemblyHelper.ArePublicKeyTokensEqual(assemblyName.GetPublicKeyToken(), MetadataAssemblyHelper._ecmaPublicKeyToken) || MetadataAssemblyHelper.ArePublicKeyTokensEqual(assemblyName.GetPublicKeyToken(), MetadataAssemblyHelper._msPublicKeyToken);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000EACB8 File Offset: 0x000E8EB8
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

		// Token: 0x0600311B RID: 12571 RVA: 0x000EAE8C File Offset: 0x000E908C
		internal static IEnumerable<Assembly> GetNonSystemReferencedAssemblies(Assembly assembly)
		{
			foreach (AssemblyName name in assembly.GetReferencedAssemblies())
			{
				if (!MetadataAssemblyHelper.ShouldFilterAssembly(name))
				{
					Assembly referenceAssembly = MetadataAssemblyHelper.SafeLoadReferencedAssembly(name);
					if (referenceAssembly != null)
					{
						yield return referenceAssembly;
					}
				}
			}
			yield break;
		}

		// Token: 0x0400128E RID: 4750
		private const string EcmaPublicKey = "b77a5c561934e089";

		// Token: 0x0400128F RID: 4751
		private const string MicrosoftPublicKey = "b03f5f7f11d50a3a";

		// Token: 0x04001290 RID: 4752
		private static readonly byte[] _ecmaPublicKeyToken = ScalarType.ConvertToByteArray("b77a5c561934e089");

		// Token: 0x04001291 RID: 4753
		private static readonly byte[] _msPublicKeyToken = ScalarType.ConvertToByteArray("b03f5f7f11d50a3a");

		// Token: 0x04001292 RID: 4754
		private static readonly Memoizer<Assembly, bool> _filterAssemblyCacheByAssembly = new Memoizer<Assembly, bool>(new Func<Assembly, bool>(MetadataAssemblyHelper.ComputeShouldFilterAssembly), EqualityComparer<Assembly>.Default);
	}
}
