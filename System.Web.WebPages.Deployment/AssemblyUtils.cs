using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using Microsoft.Internal.Web.Utils;
using Microsoft.Web.Infrastructure;

namespace System.Web.WebPages.Deployment
{
	// Token: 0x02000005 RID: 5
	internal static class AssemblyUtils
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000023AB File Offset: 0x000005AB
		internal static Version GetMaxWebPagesVersion()
		{
			return AssemblyUtils.GetMaxWebPagesVersion(AssemblyUtils.GetLoadedAssemblies());
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023BF File Offset: 0x000005BF
		internal static Version GetMaxWebPagesVersion(IEnumerable<AssemblyName> loadedAssemblies)
		{
			return AssemblyUtils.GetWebPagesAssemblies(loadedAssemblies).Max((AssemblyName c) => c.Version);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023E9 File Offset: 0x000005E9
		internal static bool IsVersionAvailable(Version version)
		{
			return AssemblyUtils.IsVersionAvailable(AssemblyUtils.GetLoadedAssemblies(), version);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002414 File Offset: 0x00000614
		internal static bool IsVersionAvailable(IEnumerable<AssemblyName> loadedAssemblies, Version version)
		{
			return AssemblyUtils.GetWebPagesAssemblies(loadedAssemblies).Any((AssemblyName c) => c.Version == version);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002453 File Offset: 0x00000653
		private static IEnumerable<AssemblyName> GetWebPagesAssemblies(IEnumerable<AssemblyName> loadedAssemblies)
		{
			return from otherName in loadedAssemblies
			where AssemblyUtils.NamesMatch(AssemblyUtils.ThisAssemblyName, otherName, false)
			select otherName;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002478 File Offset: 0x00000678
		internal static Version GetVersionFromBin(string binDirectory, IFileSystem fileSystem, Func<string, AssemblyName> getAssemblyNameThunk = null)
		{
			string text = Path.Combine(binDirectory, AssemblyUtils._binFileName);
			if (fileSystem.FileExists(text))
			{
				try
				{
					getAssemblyNameThunk = (getAssemblyNameThunk ?? new Func<string, AssemblyName>(AssemblyName.GetAssemblyName));
					AssemblyName assemblyName = getAssemblyNameThunk(text);
					if (AssemblyUtils.NamesMatch(AssemblyUtils.ThisAssemblyName, assemblyName, false))
					{
						return assemblyName.Version;
					}
				}
				catch (BadImageFormatException)
				{
				}
				catch (SecurityException)
				{
				}
				catch (FileLoadException)
				{
				}
			}
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002504 File Offset: 0x00000704
		internal static bool NamesMatch(AssemblyName left, AssemblyName right, bool matchVersion)
		{
			return object.Equals(left.Name, right.Name) && object.Equals(left.CultureInfo, right.CultureInfo) && left.GetPublicKeyToken().SequenceEqual(right.GetPublicKeyToken()) && (!matchVersion || object.Equals(left.Version, right.Version));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002562 File Offset: 0x00000762
		internal static IEnumerable<AssemblyName> GetLoadedAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies().Select(new Func<Assembly, AssemblyName>(AssemblyUtils.GetAssemblyName)).ToList<AssemblyName>();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002584 File Offset: 0x00000784
		internal static IEnumerable<AssemblyName> GetAssembliesForVersion(Version version)
		{
			if (version == AssemblyUtils.WebPagesV1Version)
			{
				return AssemblyUtils._version1AssemblyList;
			}
			return AssemblyUtils._versionCurrentAssemblyList;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000259E File Offset: 0x0000079E
		private static AssemblyName GetAssemblyName(Assembly assembly)
		{
			return new AssemblyName(assembly.FullName);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025AC File Offset: 0x000007AC
		private static AssemblyName GetFullName(string name, Version version, string publicKeyToken)
		{
			return new AssemblyName(string.Format(CultureInfo.InvariantCulture, "{0}, Version={1}, Culture=neutral, PublicKeyToken={2}", new object[]
			{
				name,
				version,
				publicKeyToken
			}));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025E1 File Offset: 0x000007E1
		internal static AssemblyName GetFullName(string name, Version version)
		{
			return AssemblyUtils.GetFullName(name, version, "31bf3856ad364e35");
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002774 File Offset: 0x00000974
		public static IDictionary<string, Version> GetAssembliesMatchingOtherVersions(IDictionary<string, IEnumerable<string>> references)
		{
			IEnumerable<AssemblyName> webPagesAssemblies = AssemblyUtils.GetAssembliesForVersion(AssemblyUtils.ThisAssemblyName.Version);
			if (references == null || webPagesAssemblies == null || !webPagesAssemblies.Any<AssemblyName>())
			{
				return new Dictionary<string, Version>(0);
			}
			IEnumerable<KeyValuePair<string, Version>> source = from item in references
			let matchedVersion = AssemblyUtils.GetMatchingVersion(webPagesAssemblies, item.Value)
			where matchedVersion != null
			select new KeyValuePair<string, Version>(item.Key, matchedVersion);
			return source.ToDictionary((KeyValuePair<string, Version> k) => k.Key, (KeyValuePair<string, Version> k) => k.Value);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private static Version GetMatchingVersion(IEnumerable<AssemblyName> webPagesAssemblies, IEnumerable<string> references)
		{
			IEnumerable<Version> source = from webPagesAssembly in webPagesAssemblies
			from referenceName in references
			let referencedAssembly = new AssemblyName(referenceName)
			where AssemblyUtils.NamesMatch(webPagesAssembly, referencedAssembly, false) && webPagesAssembly.Version != referencedAssembly.Version
			select referencedAssembly.Version;
			return source.FirstOrDefault<Version>();
		}

		// Token: 0x04000006 RID: 6
		private const string SharedLibPublicKey = "31bf3856ad364e35";

		// Token: 0x04000007 RID: 7
		internal static readonly AssemblyName ThisAssemblyName = new AssemblyName(typeof(AssemblyUtils).Assembly.FullName);

		// Token: 0x04000008 RID: 8
		internal static readonly Version WebPagesV1Version = new Version(1, 0, 0, 0);

		// Token: 0x04000009 RID: 9
		private static readonly string _binFileName = Path.GetFileName(AssemblyUtils.ThisAssemblyName.Name) + ".dll";

		// Token: 0x0400000A RID: 10
		private static readonly Version _mwiVersion = new AssemblyName(typeof(InfrastructureHelper).Assembly.FullName).Version;

		// Token: 0x0400000B RID: 11
		private static readonly AssemblyName _mwiAssemblyName = AssemblyUtils.GetFullName("Microsoft.Web.Infrastructure", AssemblyUtils._mwiVersion);

		// Token: 0x0400000C RID: 12
		private static readonly AssemblyName[] _version1AssemblyList = new AssemblyName[]
		{
			AssemblyUtils._mwiAssemblyName,
			AssemblyUtils.GetFullName("System.Web.Razor", AssemblyUtils.WebPagesV1Version),
			AssemblyUtils.GetFullName("System.Web.Helpers", AssemblyUtils.WebPagesV1Version),
			AssemblyUtils.GetFullName("System.Web.WebPages", AssemblyUtils.WebPagesV1Version),
			AssemblyUtils.GetFullName("System.Web.WebPages.Administration", AssemblyUtils.WebPagesV1Version),
			AssemblyUtils.GetFullName("System.Web.WebPages.Razor", AssemblyUtils.WebPagesV1Version),
			AssemblyUtils.GetFullName("WebMatrix.Data", AssemblyUtils.WebPagesV1Version),
			AssemblyUtils.GetFullName("WebMatrix.WebData", AssemblyUtils.WebPagesV1Version)
		};

		// Token: 0x0400000D RID: 13
		private static readonly AssemblyName[] _versionCurrentAssemblyList = new AssemblyName[]
		{
			AssemblyUtils._mwiAssemblyName,
			AssemblyUtils.GetFullName("System.Web.Razor", AssemblyUtils.ThisAssemblyName.Version),
			AssemblyUtils.GetFullName("System.Web.Helpers", AssemblyUtils.ThisAssemblyName.Version),
			AssemblyUtils.GetFullName("System.Web.WebPages", AssemblyUtils.ThisAssemblyName.Version),
			AssemblyUtils.GetFullName("System.Web.WebPages.Administration", AssemblyUtils.ThisAssemblyName.Version),
			AssemblyUtils.GetFullName("System.Web.WebPages.Razor", AssemblyUtils.ThisAssemblyName.Version),
			AssemblyUtils.GetFullName("WebMatrix.Data", AssemblyUtils.ThisAssemblyName.Version),
			AssemblyUtils.GetFullName("WebMatrix.WebData", AssemblyUtils.ThisAssemblyName.Version)
		};
	}
}
