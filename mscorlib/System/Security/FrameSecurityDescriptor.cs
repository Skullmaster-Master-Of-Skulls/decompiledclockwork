using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security
{
	// Token: 0x02000670 RID: 1648
	[Serializable]
	internal class FrameSecurityDescriptor
	{
		// Token: 0x06003B49 RID: 15177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IncrementOverridesCount();

		// Token: 0x06003B4A RID: 15178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DecrementOverridesCount();

		// Token: 0x06003B4B RID: 15179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IncrementAssertCount();

		// Token: 0x06003B4C RID: 15180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DecrementAssertCount();

		// Token: 0x06003B4D RID: 15181 RVA: 0x000C8DAB File Offset: 0x000C7DAB
		internal FrameSecurityDescriptor()
		{
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000C8DB4 File Offset: 0x000C7DB4
		private PermissionSet CreateSingletonSet(IPermission perm)
		{
			PermissionSet permissionSet = new PermissionSet(false);
			permissionSet.AddPermission(perm.Copy());
			return permissionSet;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000C8DD6 File Offset: 0x000C7DD6
		internal bool HasImperativeAsserts()
		{
			return this.m_assertions != null;
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x000C8DE4 File Offset: 0x000C7DE4
		internal bool HasImperativeDenials()
		{
			return this.m_denials != null;
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000C8DF2 File Offset: 0x000C7DF2
		internal bool HasImperativeRestrictions()
		{
			return this.m_restriction != null;
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000C8E00 File Offset: 0x000C7E00
		internal void SetAssert(IPermission perm)
		{
			this.m_assertions = this.CreateSingletonSet(perm);
			FrameSecurityDescriptor.IncrementAssertCount();
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x000C8E14 File Offset: 0x000C7E14
		internal void SetAssert(PermissionSet permSet)
		{
			this.m_assertions = permSet.Copy();
			this.m_AssertFT = (this.m_AssertFT || (CodeAccessSecurityEngine.DoesFullTrustMeanFullTrust() && this.m_assertions.IsUnrestricted()));
			FrameSecurityDescriptor.IncrementAssertCount();
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000C8E4D File Offset: 0x000C7E4D
		internal PermissionSet GetAssertions(bool fDeclarative)
		{
			if (!fDeclarative)
			{
				return this.m_assertions;
			}
			return this.m_DeclarativeAssertions;
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000C8E5F File Offset: 0x000C7E5F
		internal void SetAssertAllPossible()
		{
			this.m_assertAllPossible = true;
			FrameSecurityDescriptor.IncrementAssertCount();
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x000C8E6D File Offset: 0x000C7E6D
		internal bool GetAssertAllPossible()
		{
			return this.m_assertAllPossible;
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000C8E75 File Offset: 0x000C7E75
		internal void SetDeny(IPermission perm)
		{
			this.m_denials = this.CreateSingletonSet(perm);
			FrameSecurityDescriptor.IncrementOverridesCount();
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000C8E89 File Offset: 0x000C7E89
		internal void SetDeny(PermissionSet permSet)
		{
			this.m_denials = permSet.Copy();
			FrameSecurityDescriptor.IncrementOverridesCount();
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000C8E9C File Offset: 0x000C7E9C
		internal PermissionSet GetDenials(bool fDeclarative)
		{
			if (!fDeclarative)
			{
				return this.m_denials;
			}
			return this.m_DeclarativeDenials;
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000C8EAE File Offset: 0x000C7EAE
		internal void SetPermitOnly(IPermission perm)
		{
			this.m_restriction = this.CreateSingletonSet(perm);
			FrameSecurityDescriptor.IncrementOverridesCount();
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000C8EC2 File Offset: 0x000C7EC2
		internal void SetPermitOnly(PermissionSet permSet)
		{
			this.m_restriction = permSet.Copy();
			FrameSecurityDescriptor.IncrementOverridesCount();
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000C8ED5 File Offset: 0x000C7ED5
		internal PermissionSet GetPermitOnly(bool fDeclarative)
		{
			if (!fDeclarative)
			{
				return this.m_restriction;
			}
			return this.m_DeclarativeRestrictions;
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000C8EE7 File Offset: 0x000C7EE7
		internal void SetTokenHandles(SafeTokenHandle callerToken, SafeTokenHandle impToken)
		{
			this.m_callerToken = callerToken;
			this.m_impToken = impToken;
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000C8EF8 File Offset: 0x000C7EF8
		internal void RevertAssert()
		{
			if (this.m_assertions != null)
			{
				this.m_assertions = null;
				FrameSecurityDescriptor.DecrementAssertCount();
			}
			if (this.m_DeclarativeAssertions != null)
			{
				this.m_AssertFT = (CodeAccessSecurityEngine.DoesFullTrustMeanFullTrust() && this.m_DeclarativeAssertions.IsUnrestricted());
				return;
			}
			this.m_AssertFT = false;
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x000C8F44 File Offset: 0x000C7F44
		internal void RevertAssertAllPossible()
		{
			if (this.m_assertAllPossible)
			{
				this.m_assertAllPossible = false;
				FrameSecurityDescriptor.DecrementAssertCount();
			}
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x000C8F5A File Offset: 0x000C7F5A
		internal void RevertDeny()
		{
			if (this.HasImperativeDenials())
			{
				FrameSecurityDescriptor.DecrementOverridesCount();
				this.m_denials = null;
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000C8F70 File Offset: 0x000C7F70
		internal void RevertPermitOnly()
		{
			if (this.HasImperativeRestrictions())
			{
				FrameSecurityDescriptor.DecrementOverridesCount();
				this.m_restriction = null;
			}
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000C8F86 File Offset: 0x000C7F86
		internal void RevertAll()
		{
			this.RevertAssert();
			this.RevertAssertAllPossible();
			this.RevertDeny();
			this.RevertPermitOnly();
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000C8FA0 File Offset: 0x000C7FA0
		internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandle rmh)
		{
			bool flag = this.CheckDemand2(demand, permToken, rmh, false);
			if (flag)
			{
				flag = this.CheckDemand2(demand, permToken, rmh, true);
			}
			return flag;
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000C8FC8 File Offset: 0x000C7FC8
		internal bool CheckDemand2(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandle rmh, bool fDeclarative)
		{
			if (this.GetPermitOnly(fDeclarative) != null)
			{
				this.GetPermitOnly(fDeclarative).CheckDecoded(demand, permToken);
			}
			if (this.GetDenials(fDeclarative) != null)
			{
				this.GetDenials(fDeclarative).CheckDecoded(demand, permToken);
			}
			if (this.GetAssertions(fDeclarative) != null)
			{
				this.GetAssertions(fDeclarative).CheckDecoded(demand, permToken);
			}
			bool flag = SecurityManager._SetThreadSecurity(false);
			try
			{
				PermissionSet permissionSet = this.GetPermitOnly(fDeclarative);
				if (permissionSet != null)
				{
					CodeAccessPermission codeAccessPermission = (CodeAccessPermission)permissionSet.GetPermission(demand);
					if (codeAccessPermission == null)
					{
						if (!permissionSet.IsUnrestricted() || !demand.CanUnrestrictedOverride())
						{
							throw new SecurityException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), new object[]
							{
								demand.GetType().AssemblyQualifiedName
							}), null, permissionSet, SecurityRuntime.GetMethodInfo(rmh), demand, demand);
						}
					}
					else
					{
						bool flag2 = true;
						try
						{
							flag2 = !demand.CheckPermitOnly(codeAccessPermission);
						}
						catch (ArgumentException)
						{
						}
						if (flag2)
						{
							throw new SecurityException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), new object[]
							{
								demand.GetType().AssemblyQualifiedName
							}), null, permissionSet, SecurityRuntime.GetMethodInfo(rmh), demand, demand);
						}
					}
				}
				permissionSet = this.GetDenials(fDeclarative);
				if (permissionSet != null)
				{
					CodeAccessPermission denied = (CodeAccessPermission)permissionSet.GetPermission(demand);
					if (permissionSet.IsUnrestricted() && demand.CanUnrestrictedOverride())
					{
						throw new SecurityException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), new object[]
						{
							demand.GetType().AssemblyQualifiedName
						}), permissionSet, null, SecurityRuntime.GetMethodInfo(rmh), demand, demand);
					}
					bool flag3 = true;
					try
					{
						flag3 = !demand.CheckDeny(denied);
					}
					catch (ArgumentException)
					{
					}
					if (flag3)
					{
						throw new SecurityException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), new object[]
						{
							demand.GetType().AssemblyQualifiedName
						}), permissionSet, null, SecurityRuntime.GetMethodInfo(rmh), demand, demand);
					}
				}
				if (this.GetAssertAllPossible())
				{
					return false;
				}
				permissionSet = this.GetAssertions(fDeclarative);
				if (permissionSet != null)
				{
					CodeAccessPermission asserted = (CodeAccessPermission)permissionSet.GetPermission(demand);
					try
					{
						if ((permissionSet.IsUnrestricted() && demand.CanUnrestrictedOverride()) || demand.CheckAssert(asserted))
						{
							return false;
						}
					}
					catch (ArgumentException)
					{
					}
				}
			}
			finally
			{
				if (flag)
				{
					SecurityManager._SetThreadSecurity(true);
				}
			}
			return true;
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000C9268 File Offset: 0x000C8268
		internal bool CheckSetDemand(PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandle rmh)
		{
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			bool flag = this.CheckSetDemand2(demandSet, out permissionSet, rmh, false);
			if (permissionSet != null)
			{
				demandSet = permissionSet;
			}
			if (flag)
			{
				flag = this.CheckSetDemand2(demandSet, out permissionSet2, rmh, true);
			}
			if (permissionSet2 != null)
			{
				alteredDemandSet = permissionSet2;
			}
			else if (permissionSet != null)
			{
				alteredDemandSet = permissionSet;
			}
			else
			{
				alteredDemandSet = null;
			}
			return flag;
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x000C92B0 File Offset: 0x000C82B0
		internal bool CheckSetDemand2(PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandle rmh, bool fDeclarative)
		{
			alteredDemandSet = null;
			if (demandSet == null || demandSet.IsEmpty())
			{
				return false;
			}
			if (this.GetPermitOnly(fDeclarative) != null)
			{
				this.GetPermitOnly(fDeclarative).CheckDecoded(demandSet);
			}
			if (this.GetDenials(fDeclarative) != null)
			{
				this.GetDenials(fDeclarative).CheckDecoded(demandSet);
			}
			if (this.GetAssertions(fDeclarative) != null)
			{
				this.GetAssertions(fDeclarative).CheckDecoded(demandSet);
			}
			bool flag = SecurityManager._SetThreadSecurity(false);
			try
			{
				PermissionSet permissionSet = this.GetPermitOnly(fDeclarative);
				if (permissionSet != null)
				{
					IPermission permThatFailed = null;
					bool flag2 = true;
					try
					{
						flag2 = !demandSet.CheckPermitOnly(permissionSet, out permThatFailed);
					}
					catch (ArgumentException)
					{
					}
					if (flag2)
					{
						throw new SecurityException(Environment.GetResourceString("Security_GenericNoType"), null, permissionSet, SecurityRuntime.GetMethodInfo(rmh), demandSet, permThatFailed);
					}
				}
				permissionSet = this.GetDenials(fDeclarative);
				if (permissionSet != null)
				{
					IPermission permThatFailed2 = null;
					bool flag3 = true;
					try
					{
						flag3 = !demandSet.CheckDeny(permissionSet, out permThatFailed2);
					}
					catch (ArgumentException)
					{
					}
					if (flag3)
					{
						throw new SecurityException(Environment.GetResourceString("Security_GenericNoType"), permissionSet, null, SecurityRuntime.GetMethodInfo(rmh), demandSet, permThatFailed2);
					}
				}
				if (this.GetAssertAllPossible())
				{
					return false;
				}
				permissionSet = this.GetAssertions(fDeclarative);
				if (permissionSet != null)
				{
					if (demandSet.CheckAssertion(permissionSet))
					{
						return false;
					}
					if (!permissionSet.IsUnrestricted())
					{
						PermissionSet.RemoveAssertedPermissionSet(demandSet, permissionSet, out alteredDemandSet);
					}
				}
			}
			finally
			{
				if (flag)
				{
					SecurityManager._SetThreadSecurity(true);
				}
			}
			return true;
		}

		// Token: 0x04001EAC RID: 7852
		private PermissionSet m_assertions;

		// Token: 0x04001EAD RID: 7853
		private PermissionSet m_denials;

		// Token: 0x04001EAE RID: 7854
		private PermissionSet m_restriction;

		// Token: 0x04001EAF RID: 7855
		private PermissionSet m_DeclarativeAssertions;

		// Token: 0x04001EB0 RID: 7856
		private PermissionSet m_DeclarativeDenials;

		// Token: 0x04001EB1 RID: 7857
		private PermissionSet m_DeclarativeRestrictions;

		// Token: 0x04001EB2 RID: 7858
		[NonSerialized]
		private SafeTokenHandle m_callerToken;

		// Token: 0x04001EB3 RID: 7859
		[NonSerialized]
		private SafeTokenHandle m_impToken;

		// Token: 0x04001EB4 RID: 7860
		private bool m_AssertFT;

		// Token: 0x04001EB5 RID: 7861
		private bool m_assertAllPossible;

		// Token: 0x04001EB6 RID: 7862
		private bool m_declSecComputed;
	}
}
