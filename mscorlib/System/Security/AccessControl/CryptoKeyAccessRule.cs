using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000910 RID: 2320
	public sealed class CryptoKeyAccessRule : AccessRule
	{
		// Token: 0x060053E9 RID: 21481 RVA: 0x001304EA File Offset: 0x0012F4EA
		public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type) : this(identity, CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x001304FE File Offset: 0x0012F4FE
		public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type) : this(new NTAccount(identity), CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x00130517 File Offset: 0x0012F517
		private CryptoKeyAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060053EC RID: 21484 RVA: 0x00130528 File Offset: 0x0012F528
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return CryptoKeyAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x00130538 File Offset: 0x0012F538
		private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights, AccessControlType controlType)
		{
			if (controlType == AccessControlType.Allow)
			{
				cryptoKeyRights |= CryptoKeyRights.Synchronize;
			}
			else
			{
				if (controlType != AccessControlType.Deny)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", new object[]
					{
						controlType,
						"controlType"
					}), "controlType");
				}
				if (cryptoKeyRights != CryptoKeyRights.FullControl)
				{
					cryptoKeyRights &= ~CryptoKeyRights.Synchronize;
				}
			}
			return (int)cryptoKeyRights;
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x00130599 File Offset: 0x0012F599
		internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
		{
			return (CryptoKeyRights)accessMask;
		}
	}
}
