using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.EntityFramework
{
	// Token: 0x020000E7 RID: 231
	internal static class EFOracleVersionUtils
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x0006C6BC File Offset: 0x0006A8BC
		internal static EFOracleVersion GetStorageVersion(OracleConnection connection)
		{
			string serverVersion = connection.ServerVersion;
			if (serverVersion.StartsWith("9.2"))
			{
				return EFOracleVersion.Oracle9iR2;
			}
			if (serverVersion.StartsWith("10.1"))
			{
				return EFOracleVersion.Oracle10gR1;
			}
			if (serverVersion.StartsWith("10.2"))
			{
				return EFOracleVersion.Oracle10gR2;
			}
			if (serverVersion.StartsWith("11.1"))
			{
				return EFOracleVersion.Oracle11gR1;
			}
			if (serverVersion.StartsWith("11.2"))
			{
				return EFOracleVersion.Oracle11gR2;
			}
			if (serverVersion.StartsWith("12.1"))
			{
				return EFOracleVersion.Oracle12cR1;
			}
			if (serverVersion.StartsWith("12.2"))
			{
				return EFOracleVersion.Oracle12cR2;
			}
			throw new ArgumentException(EFProviderSettings.Instance.GetErrorMessage(-1703, new string[]
			{
				"Oracle Data Provider for .NET",
				"Oracle " + serverVersion
			}));
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0006C774 File Offset: 0x0006A974
		internal static string GetVersionHint(EFOracleVersion version)
		{
			if (version <= EFOracleVersion.Oracle10gR2)
			{
				if (version == EFOracleVersion.Oracle9iR2)
				{
					return "9.2";
				}
				switch (version)
				{
				case EFOracleVersion.Oracle10gR1:
					return "10.1";
				case EFOracleVersion.Oracle10gR2:
					return "10.2";
				}
			}
			else
			{
				switch (version)
				{
				case EFOracleVersion.Oracle11gR1:
					return "11.1";
				case EFOracleVersion.Oracle11gR2:
					return "11.2";
				default:
					switch (version)
					{
					case EFOracleVersion.Oracle12cR1:
						return "12.1";
					case EFOracleVersion.Oracle12cR2:
						return "12.2";
					}
					break;
				}
			}
			throw new ArgumentException(EFProviderSettings.Instance.GetErrorMessage(-1202, new string[]
			{
				"ProviderManifestToken"
			}));
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0006C814 File Offset: 0x0006AA14
		internal static EFOracleVersion GetStorageVersion(string versionHint)
		{
			if (!string.IsNullOrEmpty(versionHint) && versionHint != null)
			{
				if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x600086b-1 == null)
				{
					<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x600086b-1 = new Dictionary<string, int>(7)
					{
						{
							"9.2",
							0
						},
						{
							"10.1",
							1
						},
						{
							"10.2",
							2
						},
						{
							"11.1",
							3
						},
						{
							"11.2",
							4
						},
						{
							"12.1",
							5
						},
						{
							"12.2",
							6
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x600086b-1.TryGetValue(versionHint, out num))
				{
					switch (num)
					{
					case 0:
						return EFOracleVersion.Oracle9iR2;
					case 1:
						return EFOracleVersion.Oracle10gR1;
					case 2:
						return EFOracleVersion.Oracle10gR2;
					case 3:
						return EFOracleVersion.Oracle11gR1;
					case 4:
						return EFOracleVersion.Oracle11gR2;
					case 5:
						return EFOracleVersion.Oracle12cR1;
					case 6:
						return EFOracleVersion.Oracle12cR2;
					}
				}
			}
			throw new ArgumentException(EFProviderSettings.Instance.GetErrorMessage(-1202, new string[]
			{
				"ProviderManifestToken"
			}));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0006C90C File Offset: 0x0006AB0C
		internal static bool IsVersionX(EFOracleVersion storageVersion)
		{
			return storageVersion == EFOracleVersion.Oracle10gR1 || storageVersion == EFOracleVersion.Oracle10gR2 || storageVersion == EFOracleVersion.Oracle11gR1 || storageVersion == EFOracleVersion.Oracle11gR2 || storageVersion == EFOracleVersion.Oracle12cR1 || storageVersion == EFOracleVersion.Oracle12cR2 || storageVersion == EFOracleVersion.Oracle9iR2;
		}
	}
}
