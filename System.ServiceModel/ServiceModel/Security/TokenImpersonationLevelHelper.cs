using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace System.ServiceModel.Security
{
	// Token: 0x0200034F RID: 847
	internal static class TokenImpersonationLevelHelper
	{
		// Token: 0x06001EB4 RID: 7860 RVA: 0x00071AD6 File Offset: 0x0006FCD6
		internal static bool IsDefined(TokenImpersonationLevel value)
		{
			return value == TokenImpersonationLevel.None || value == TokenImpersonationLevel.Anonymous || value == TokenImpersonationLevel.Identification || value == TokenImpersonationLevel.Impersonation || value == TokenImpersonationLevel.Delegation;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00071AED File Offset: 0x0006FCED
		internal static void Validate(TokenImpersonationLevel value)
		{
			if (!TokenImpersonationLevelHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(TokenImpersonationLevel)));
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00071B18 File Offset: 0x0006FD18
		internal static string ToString(TokenImpersonationLevel impersonationLevel)
		{
			if (impersonationLevel == TokenImpersonationLevel.Identification)
			{
				return "identification";
			}
			if (impersonationLevel == TokenImpersonationLevel.None)
			{
				return "none";
			}
			if (impersonationLevel == TokenImpersonationLevel.Anonymous)
			{
				return "anonymous";
			}
			if (impersonationLevel == TokenImpersonationLevel.Impersonation)
			{
				return "impersonation";
			}
			if (impersonationLevel == TokenImpersonationLevel.Delegation)
			{
				return "delegation";
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("impersonationLevel", (int)impersonationLevel, typeof(TokenImpersonationLevel)));
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00071B78 File Offset: 0x0006FD78
		internal static bool IsGreaterOrEqual(TokenImpersonationLevel x, TokenImpersonationLevel y)
		{
			TokenImpersonationLevelHelper.Validate(x);
			TokenImpersonationLevelHelper.Validate(y);
			if (x == y)
			{
				return true;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < TokenImpersonationLevelHelper.TokenImpersonationLevelOrder.Length; i++)
			{
				if (x == TokenImpersonationLevelHelper.TokenImpersonationLevelOrder[i])
				{
					num = i;
				}
				if (y == TokenImpersonationLevelHelper.TokenImpersonationLevelOrder[i])
				{
					num2 = i;
				}
			}
			return num > num2;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00071BCC File Offset: 0x0006FDCC
		internal static int Compare(TokenImpersonationLevel x, TokenImpersonationLevel y)
		{
			int result = 0;
			if (x != y)
			{
				switch (x)
				{
				case TokenImpersonationLevel.Identification:
					result = -1;
					break;
				case TokenImpersonationLevel.Impersonation:
					if (y != TokenImpersonationLevel.Identification)
					{
						if (y != TokenImpersonationLevel.Delegation)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("y", (int)y, typeof(TokenImpersonationLevel)));
						}
						result = -1;
					}
					else
					{
						result = 1;
					}
					break;
				case TokenImpersonationLevel.Delegation:
					result = 1;
					break;
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("x", (int)x, typeof(TokenImpersonationLevel)));
				}
			}
			return result;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00071C50 File Offset: 0x0006FE50
		// Note: this type is marked as 'beforefieldinit'.
		static TokenImpersonationLevelHelper()
		{
			TokenImpersonationLevel[] array = new TokenImpersonationLevel[5];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.E528F4309E1413E6BC35AEA5D8DB8519384D2FCC33F9DD5D1126D73F104CF92A).FieldHandle);
			TokenImpersonationLevelHelper.TokenImpersonationLevelOrder = array;
		}

		// Token: 0x04001EB2 RID: 7858
		private static TokenImpersonationLevel[] TokenImpersonationLevelOrder;
	}
}
