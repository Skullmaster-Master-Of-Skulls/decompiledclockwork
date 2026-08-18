using System;
using System.ComponentModel;
using System.Net.Security;

namespace System.ServiceModel.Security
{
	// Token: 0x0200034D RID: 845
	internal static class ProtectionLevelHelper
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x00071970 File Offset: 0x0006FB70
		internal static bool IsDefined(ProtectionLevel value)
		{
			return value == ProtectionLevel.None || value == ProtectionLevel.Sign || value == ProtectionLevel.EncryptAndSign;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0007197F File Offset: 0x0006FB7F
		internal static void Validate(ProtectionLevel value)
		{
			if (!ProtectionLevelHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(ProtectionLevel)));
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000719A9 File Offset: 0x0006FBA9
		internal static bool IsStronger(ProtectionLevel v1, ProtectionLevel v2)
		{
			return (v1 == ProtectionLevel.EncryptAndSign && v2 != ProtectionLevel.EncryptAndSign) || (v1 == ProtectionLevel.Sign && v2 == ProtectionLevel.None);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000719BF File Offset: 0x0006FBBF
		internal static bool IsStrongerOrEqual(ProtectionLevel v1, ProtectionLevel v2)
		{
			return v1 == ProtectionLevel.EncryptAndSign || (v1 == ProtectionLevel.Sign && v2 != ProtectionLevel.EncryptAndSign);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000719D4 File Offset: 0x0006FBD4
		internal static ProtectionLevel Max(ProtectionLevel v1, ProtectionLevel v2)
		{
			if (!ProtectionLevelHelper.IsStronger(v1, v2))
			{
				return v2;
			}
			return v1;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000719E4 File Offset: 0x0006FBE4
		internal static int GetOrdinal(ProtectionLevel? p)
		{
			if (p == null)
			{
				return 1;
			}
			switch (p.Value)
			{
			case ProtectionLevel.None:
				return 2;
			case ProtectionLevel.Sign:
				return 3;
			case ProtectionLevel.EncryptAndSign:
				return 4;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("p", (int)p.Value, typeof(ProtectionLevel)));
			}
		}
	}
}
