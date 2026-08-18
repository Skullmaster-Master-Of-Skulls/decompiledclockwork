using System;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x020011E2 RID: 4578
	internal class WordAdapterLoader
	{
		// Token: 0x0600BD09 RID: 48393 RVA: 0x0029E8B0 File Offset: 0x0029CAB0
		internal WordAdapterLoader()
		{
		}

		// Token: 0x0600BD0A RID: 48394 RVA: 0x0029E8C0 File Offset: 0x0029CAC0
		internal static Assembly LoadAssembly()
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = "MsWordAdapter";
			assemblyName.CultureInfo = CultureInfo.InvariantCulture;
			assemblyName.Version = new Version("1.0.0.0");
			byte[] publicKeyToken = new byte[]
			{
				181,
				218,
				215,
				191,
				43,
				245,
				148,
				194
			};
			assemblyName.SetPublicKeyToken(publicKeyToken);
			return Assembly.Load(assemblyName);
		}

		// Token: 0x0600BD0B RID: 48395 RVA: 0x0029E918 File Offset: 0x0029CB18
		internal static ISpellCheckProvider CreateWordProvider()
		{
			Assembly assembly = WordAdapterLoader.LoadAssembly();
			Type type = assembly.GetType("Telerik.WebControls.WordSpellCheckProvider", true, true);
			return (ISpellCheckProvider)Activator.CreateInstance(type);
		}
	}
}
