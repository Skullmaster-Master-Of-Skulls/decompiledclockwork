using System;
using System.Collections.Generic;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200000C RID: 12
	internal static class EFOracleVersionUtils
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000022D4 File Offset: 0x000012D4
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
				return EFOracleVersion.Oracle12gR1;
			}
			if (serverVersion.StartsWith("12.2"))
			{
				return EFOracleVersion.Oracle12gR2;
			}
			throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
			{
				"Oracle Data Provider for .NET",
				"Oracle " + serverVersion
			}));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002388 File Offset: 0x00001388
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
					case EFOracleVersion.Oracle12gR1:
						return "12.1";
					case EFOracleVersion.Oracle12gR2:
						return "12.2";
					}
					break;
				}
			}
			throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
			{
				"ProviderManifestToken"
			}));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002424 File Offset: 0x00001424
		internal static EFOracleVersion GetStorageVersion(string versionHint)
		{
			if (!string.IsNullOrEmpty(versionHint) && versionHint != null)
			{
				if (<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000027-1 == null)
				{
					<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000027-1 = new Dictionary<string, int>(7)
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
				if (<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000027-1.TryGetValue(versionHint, out num))
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
						return EFOracleVersion.Oracle12gR1;
					case 6:
						return EFOracleVersion.Oracle12gR2;
					}
				}
			}
			throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
			{
				"ProviderManifestToken"
			}));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002517 File Offset: 0x00001517
		internal static bool IsVersionX(EFOracleVersion storageVersion)
		{
			return storageVersion == EFOracleVersion.Oracle10gR1 || storageVersion == EFOracleVersion.Oracle10gR2 || storageVersion == EFOracleVersion.Oracle11gR1 || storageVersion == EFOracleVersion.Oracle11gR2 || storageVersion == EFOracleVersion.Oracle12gR1 || storageVersion == EFOracleVersion.Oracle12gR2 || storageVersion == EFOracleVersion.Oracle9iR2;
		}
	}
}
