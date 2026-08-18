using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Security
{
	// Token: 0x0200066F RID: 1647
	internal class CodeAccessSecurityEngine
	{
		// Token: 0x06003B29 RID: 15145
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SpecialDemand(PermissionType whatPermission, ref StackCrawlMark stackMark);

		// Token: 0x06003B2A RID: 15146
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool DoesFullTrustMeanFullTrust();

		// Token: 0x06003B2B RID: 15147 RVA: 0x000C8796 File Offset: 0x000C7796
		[Conditional("_DEBUG")]
		private static void DEBUG_OUT(string str)
		{
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000C8798 File Offset: 0x000C7798
		private CodeAccessSecurityEngine()
		{
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000C87BC File Offset: 0x000C77BC
		private static void ThrowSecurityException(Assembly asm, PermissionSet granted, PermissionSet refused, RuntimeMethodHandle rmh, SecurityAction action, object demand, IPermission permThatFailed)
		{
			AssemblyName asmName = null;
			Evidence asmEvidence = null;
			if (asm != null)
			{
				PermissionSet.s_fullTrust.Assert();
				asmName = asm.GetName();
				if (asm != Assembly.GetExecutingAssembly())
				{
					asmEvidence = asm.Evidence;
				}
			}
			throw SecurityException.MakeSecurityException(asmName, asmEvidence, granted, refused, rmh, action, demand, permThatFailed);
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000C8800 File Offset: 0x000C7800
		private static void ThrowSecurityException(object assemblyOrString, PermissionSet granted, PermissionSet refused, RuntimeMethodHandle rmh, SecurityAction action, object demand, IPermission permThatFailed)
		{
			if (assemblyOrString == null || assemblyOrString is Assembly)
			{
				CodeAccessSecurityEngine.ThrowSecurityException((Assembly)assemblyOrString, granted, refused, rmh, action, demand, permThatFailed);
				return;
			}
			AssemblyName asmName = new AssemblyName((string)assemblyOrString);
			throw SecurityException.MakeSecurityException(asmName, null, granted, refused, rmh, action, demand, permThatFailed);
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000C884C File Offset: 0x000C784C
		private static void LazyCheckSetHelper(PermissionSet demands, IntPtr asmSecDesc, RuntimeMethodHandle rmh, Assembly assembly, SecurityAction action)
		{
			if (demands.CanUnrestrictedOverride())
			{
				return;
			}
			PermissionSet grants;
			PermissionSet refused;
			CodeAccessSecurityEngine._GetGrantedPermissionSet(asmSecDesc, out grants, out refused);
			CodeAccessSecurityEngine.CheckSetHelper(grants, refused, demands, rmh, assembly, action, true);
		}

		// Token: 0x06003B31 RID: 15153
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void _GetGrantedPermissionSet(IntPtr secDesc, out PermissionSet grants, out PermissionSet refused);

		// Token: 0x06003B32 RID: 15154 RVA: 0x000C887A File Offset: 0x000C787A
		internal static void CheckSetHelper(CompressedStack cs, PermissionSet grants, PermissionSet refused, PermissionSet demands, RuntimeMethodHandle rmh, Assembly asm, SecurityAction action)
		{
			if (cs != null)
			{
				cs.CheckSetDemand(demands, rmh);
				return;
			}
			CodeAccessSecurityEngine.CheckSetHelper(grants, refused, demands, rmh, asm, action, true);
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000C889C File Offset: 0x000C789C
		internal static bool CheckSetHelper(PermissionSet grants, PermissionSet refused, PermissionSet demands, RuntimeMethodHandle rmh, object assemblyOrString, SecurityAction action, bool throwException)
		{
			IPermission permThatFailed = null;
			if (grants != null)
			{
				grants.CheckDecoded(demands);
			}
			if (refused != null)
			{
				refused.CheckDecoded(demands);
			}
			bool flag = SecurityManager._SetThreadSecurity(false);
			try
			{
				if (!demands.CheckDemand(grants, out permThatFailed))
				{
					if (!throwException)
					{
						return false;
					}
					CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, demands, permThatFailed);
				}
				if (!demands.CheckDeny(refused, out permThatFailed))
				{
					if (!throwException)
					{
						return false;
					}
					CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, demands, permThatFailed);
				}
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception)
			{
				if (!throwException)
				{
					return false;
				}
				CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, demands, permThatFailed);
			}
			catch
			{
				return false;
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

		// Token: 0x06003B34 RID: 15156 RVA: 0x000C8978 File Offset: 0x000C7978
		internal static void CheckHelper(CompressedStack cs, PermissionSet grantedSet, PermissionSet refusedSet, CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandle rmh, Assembly asm, SecurityAction action)
		{
			if (cs != null)
			{
				cs.CheckDemand(demand, permToken, rmh);
				return;
			}
			CodeAccessSecurityEngine.CheckHelper(grantedSet, refusedSet, demand, permToken, rmh, asm, action, true);
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000C899C File Offset: 0x000C799C
		internal static bool CheckHelper(PermissionSet grantedSet, PermissionSet refusedSet, CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandle rmh, object assemblyOrString, SecurityAction action, bool throwException)
		{
			if (permToken == null)
			{
				permToken = PermissionToken.GetToken(demand);
			}
			if (grantedSet != null)
			{
				grantedSet.CheckDecoded(permToken.m_index);
			}
			if (refusedSet != null)
			{
				refusedSet.CheckDecoded(permToken.m_index);
			}
			bool flag = SecurityManager._SetThreadSecurity(false);
			try
			{
				if (grantedSet == null)
				{
					if (!throwException)
					{
						return false;
					}
					CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
				}
				else if (!grantedSet.IsUnrestricted() || !demand.CanUnrestrictedOverride())
				{
					CodeAccessPermission grant = (CodeAccessPermission)grantedSet.GetPermission(permToken);
					if (!demand.CheckDemand(grant))
					{
						if (!throwException)
						{
							return false;
						}
						CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
					}
				}
				if (refusedSet != null)
				{
					CodeAccessPermission codeAccessPermission = (CodeAccessPermission)refusedSet.GetPermission(permToken);
					if (codeAccessPermission != null && !codeAccessPermission.CheckDeny(demand))
					{
						if (!throwException)
						{
							return false;
						}
						CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
					}
					if (refusedSet.IsUnrestricted() && demand.CanUnrestrictedOverride())
					{
						if (!throwException)
						{
							return false;
						}
						CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
					}
				}
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception)
			{
				if (!throwException)
				{
					return false;
				}
				CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
			}
			catch
			{
				return false;
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

		// Token: 0x06003B36 RID: 15158 RVA: 0x000C8B04 File Offset: 0x000C7B04
		private static void CheckGrantSetHelper(PermissionSet grantSet)
		{
			grantSet.CopyWithNoIdentityPermissions().Demand();
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000C8B11 File Offset: 0x000C7B11
		internal static void ReflectionTargetDemandHelper(PermissionType permission, PermissionSet targetGrant)
		{
			CodeAccessSecurityEngine.ReflectionTargetDemandHelper((int)permission, targetGrant);
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000C8B1C File Offset: 0x000C7B1C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CompressedStack compressedStack = CompressedStack.GetCompressedStack(ref stackCrawlMark);
			CodeAccessSecurityEngine.ReflectionTargetDemandHelper(permission, targetGrant, compressedStack);
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000C8B3B File Offset: 0x000C7B3B
		private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant, Resolver accessContext)
		{
			CodeAccessSecurityEngine.ReflectionTargetDemandHelper(permission, targetGrant, accessContext.GetSecurityContext());
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000C8B4C File Offset: 0x000C7B4C
		private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant, CompressedStack securityContext)
		{
			PermissionSet permissionSet;
			if (targetGrant == null)
			{
				permissionSet = new PermissionSet(PermissionState.Unrestricted);
			}
			else
			{
				permissionSet = targetGrant.CopyWithNoIdentityPermissions();
				permissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess));
			}
			securityContext.DemandFlagsOrGrantSet(1 << permission, permissionSet);
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000C8B88 File Offset: 0x000C7B88
		internal static void GetZoneAndOriginHelper(CompressedStack cs, PermissionSet grantSet, PermissionSet refusedSet, ArrayList zoneList, ArrayList originList)
		{
			if (cs != null)
			{
				cs.GetZoneAndOrigin(zoneList, originList, PermissionToken.GetToken(typeof(ZoneIdentityPermission)), PermissionToken.GetToken(typeof(UrlIdentityPermission)));
				return;
			}
			ZoneIdentityPermission zoneIdentityPermission = (ZoneIdentityPermission)grantSet.GetPermission(typeof(ZoneIdentityPermission));
			UrlIdentityPermission urlIdentityPermission = (UrlIdentityPermission)grantSet.GetPermission(typeof(UrlIdentityPermission));
			if (zoneIdentityPermission != null)
			{
				zoneList.Add(zoneIdentityPermission.SecurityZone);
			}
			if (urlIdentityPermission != null)
			{
				originList.Add(urlIdentityPermission.Url);
			}
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000C8C12 File Offset: 0x000C7C12
		internal static void GetZoneAndOrigin(ref StackCrawlMark mark, out ArrayList zone, out ArrayList origin)
		{
			zone = new ArrayList();
			origin = new ArrayList();
			CodeAccessSecurityEngine.GetZoneAndOriginInternal(zone, origin, ref mark);
		}

		// Token: 0x06003B3D RID: 15165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetZoneAndOriginInternal(ArrayList zoneList, ArrayList originList, ref StackCrawlMark stackMark);

		// Token: 0x06003B3E RID: 15166 RVA: 0x000C8C2C File Offset: 0x000C7C2C
		internal static void CheckAssembly(Assembly asm, CodeAccessPermission demand)
		{
			if (SecurityManager._IsSecurityOn())
			{
				PermissionSet grantedSet;
				PermissionSet refusedSet;
				asm.nGetGrantSet(out grantedSet, out refusedSet);
				CodeAccessSecurityEngine.CheckHelper(grantedSet, refusedSet, demand, PermissionToken.GetToken(demand), RuntimeMethodHandle.EmptyHandle, asm, SecurityAction.Demand, true);
			}
		}

		// Token: 0x06003B3F RID: 15167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Check(object demand, ref StackCrawlMark stackMark, bool isPermSet);

		// Token: 0x06003B40 RID: 15168
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool QuickCheckForAllDemands();

		// Token: 0x06003B41 RID: 15169
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool AllDomainsHomogeneousWithNoStackModifiers();

		// Token: 0x06003B42 RID: 15170 RVA: 0x000C8C61 File Offset: 0x000C7C61
		internal static void Check(CodeAccessPermission cap, ref StackCrawlMark stackMark)
		{
			CodeAccessSecurityEngine.Check(cap, ref stackMark, false);
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000C8C6B File Offset: 0x000C7C6B
		internal static void Check(PermissionSet permSet, ref StackCrawlMark stackMark)
		{
			CodeAccessSecurityEngine.Check(permSet, ref stackMark, true);
		}

		// Token: 0x06003B44 RID: 15172
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern FrameSecurityDescriptor CheckNReturnSO(PermissionToken permToken, CodeAccessPermission demand, ref StackCrawlMark stackMark, int unrestrictedOverride, int create);

		// Token: 0x06003B45 RID: 15173 RVA: 0x000C8C78 File Offset: 0x000C7C78
		internal static void Assert(CodeAccessPermission cap, ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor frameSecurityDescriptor = CodeAccessSecurityEngine.CheckNReturnSO(CodeAccessSecurityEngine.AssertPermissionToken, CodeAccessSecurityEngine.AssertPermission, ref stackMark, 1, 1);
			if (frameSecurityDescriptor == null)
			{
				if (SecurityManager._IsSecurityOn())
				{
					throw new ExecutionEngineException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				}
			}
			else
			{
				if (frameSecurityDescriptor.HasImperativeAsserts())
				{
					throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
				}
				frameSecurityDescriptor.SetAssert(cap);
			}
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x000C8CD4 File Offset: 0x000C7CD4
		internal static void Deny(CodeAccessPermission cap, ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				if (SecurityManager._IsSecurityOn())
				{
					throw new ExecutionEngineException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				}
			}
			else
			{
				if (securityObjectForFrame.HasImperativeDenials())
				{
					throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
				}
				securityObjectForFrame.SetDeny(cap);
			}
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x000C8D24 File Offset: 0x000C7D24
		internal static void PermitOnly(CodeAccessPermission cap, ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				if (SecurityManager._IsSecurityOn())
				{
					throw new ExecutionEngineException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				}
			}
			else
			{
				if (securityObjectForFrame.HasImperativeRestrictions())
				{
					throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
				}
				securityObjectForFrame.SetPermitOnly(cap);
			}
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x000C8D74 File Offset: 0x000C7D74
		private static PermissionListSet UpdateAppDomainPLS(PermissionListSet adPLS, PermissionSet grantedPerms, PermissionSet refusedPerms)
		{
			if (adPLS == null)
			{
				adPLS = new PermissionListSet();
				adPLS.UpdateDomainPLS(grantedPerms, refusedPerms);
				return adPLS;
			}
			PermissionListSet permissionListSet = new PermissionListSet();
			permissionListSet.UpdateDomainPLS(adPLS);
			permissionListSet.UpdateDomainPLS(grantedPerms, refusedPerms);
			return permissionListSet;
		}

		// Token: 0x04001EAA RID: 7850
		internal static SecurityPermission AssertPermission = new SecurityPermission(SecurityPermissionFlag.Assertion);

		// Token: 0x04001EAB RID: 7851
		internal static PermissionToken AssertPermissionToken = PermissionToken.GetToken(CodeAccessSecurityEngine.AssertPermission);
	}
}
