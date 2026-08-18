using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Security
{
	// Token: 0x02000694 RID: 1684
	internal class SecurityRuntime
	{
		// Token: 0x06003D05 RID: 15621 RVA: 0x000D0E09 File Offset: 0x000CFE09
		private SecurityRuntime()
		{
		}

		// Token: 0x06003D06 RID: 15622
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern FrameSecurityDescriptor GetSecurityObjectForFrame(ref StackCrawlMark stackMark, bool create);

		// Token: 0x06003D07 RID: 15623 RVA: 0x000D0E14 File Offset: 0x000CFE14
		private static int OverridesHelper(FrameSecurityDescriptor secDesc)
		{
			int num = SecurityRuntime.OverridesHelper2(secDesc, false);
			return num + SecurityRuntime.OverridesHelper2(secDesc, true);
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x000D0E34 File Offset: 0x000CFE34
		private static int OverridesHelper2(FrameSecurityDescriptor secDesc, bool fDeclarative)
		{
			int num = 0;
			PermissionSet permissionSet = secDesc.GetPermitOnly(fDeclarative);
			if (permissionSet != null)
			{
				num++;
			}
			permissionSet = secDesc.GetDenials(fDeclarative);
			if (permissionSet != null)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000D0E64 File Offset: 0x000CFE64
		internal static MethodInfo GetMethodInfo(RuntimeMethodHandle rmh)
		{
			if (rmh.IsNullHandle())
			{
				return null;
			}
			PermissionSet.s_fullTrust.Assert();
			RuntimeTypeHandle declaringType = rmh.GetDeclaringType();
			return RuntimeType.GetMethodBase(declaringType, rmh) as MethodInfo;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x000D0E9A File Offset: 0x000CFE9A
		private static bool FrameDescSetHelper(FrameSecurityDescriptor secDesc, PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandle rmh)
		{
			return secDesc.CheckSetDemand(demandSet, out alteredDemandSet, rmh);
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x000D0EA5 File Offset: 0x000CFEA5
		private static bool FrameDescHelper(FrameSecurityDescriptor secDesc, IPermission demandIn, PermissionToken permToken, RuntimeMethodHandle rmh)
		{
			return secDesc.CheckDemand((CodeAccessPermission)demandIn, permToken, rmh);
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000D0EB8 File Offset: 0x000CFEB8
		[SecurityCritical]
		private static bool CheckDynamicMethodSetHelper(DynamicResolver dynamicResolver, PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandle rmh)
		{
			CompressedStack securityContext = dynamicResolver.GetSecurityContext();
			bool result;
			try
			{
				result = securityContext.CheckSetDemandWithModificationNoHalt(demandSet, out alteredDemandSet, rmh);
			}
			catch (SecurityException inner)
			{
				throw new SecurityException(Environment.GetResourceString("Security_AnonymouslyHostedDynamicMethodCheckFailed"), inner);
			}
			return result;
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x000D0EFC File Offset: 0x000CFEFC
		[SecurityCritical]
		private static bool CheckDynamicMethodHelper(DynamicResolver dynamicResolver, IPermission demandIn, PermissionToken permToken, RuntimeMethodHandle rmh)
		{
			CompressedStack securityContext = dynamicResolver.GetSecurityContext();
			bool result;
			try
			{
				result = securityContext.CheckDemandNoHalt((CodeAccessPermission)demandIn, permToken, rmh);
			}
			catch (SecurityException inner)
			{
				throw new SecurityException(Environment.GetResourceString("Security_AnonymouslyHostedDynamicMethodCheckFailed"), inner);
			}
			return result;
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x000D0F44 File Offset: 0x000CFF44
		internal static void Assert(PermissionSet permSet, ref StackCrawlMark stackMark)
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
				frameSecurityDescriptor.SetAssert(permSet);
			}
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x000D0FA0 File Offset: 0x000CFFA0
		internal static void AssertAllPossible(ref StackCrawlMark stackMark)
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
				if (securityObjectForFrame.GetAssertAllPossible())
				{
					throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
				}
				securityObjectForFrame.SetAssertAllPossible();
			}
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x000D0FF0 File Offset: 0x000CFFF0
		internal static void Deny(PermissionSet permSet, ref StackCrawlMark stackMark)
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
				securityObjectForFrame.SetDeny(permSet);
			}
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000D1040 File Offset: 0x000D0040
		internal static void PermitOnly(PermissionSet permSet, ref StackCrawlMark stackMark)
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
				securityObjectForFrame.SetPermitOnly(permSet);
			}
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000D1090 File Offset: 0x000D0090
		internal static void RevertAssert(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
			if (securityObjectForFrame != null)
			{
				securityObjectForFrame.RevertAssert();
				return;
			}
			if (SecurityManager._IsSecurityOn())
			{
				throw new ExecutionEngineException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
			}
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000D10C8 File Offset: 0x000D00C8
		internal static void RevertDeny(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
			if (securityObjectForFrame != null)
			{
				securityObjectForFrame.RevertDeny();
				return;
			}
			if (SecurityManager._IsSecurityOn())
			{
				throw new ExecutionEngineException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
			}
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000D1100 File Offset: 0x000D0100
		internal static void RevertPermitOnly(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
			if (securityObjectForFrame != null)
			{
				securityObjectForFrame.RevertPermitOnly();
				return;
			}
			if (SecurityManager._IsSecurityOn())
			{
				throw new ExecutionEngineException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
			}
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000D1138 File Offset: 0x000D0138
		internal static void RevertAll(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
			if (securityObjectForFrame != null)
			{
				securityObjectForFrame.RevertAll();
				return;
			}
			if (SecurityManager._IsSecurityOn())
			{
				throw new ExecutionEngineException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
			}
		}

		// Token: 0x04001F54 RID: 8020
		internal const bool StackContinue = true;

		// Token: 0x04001F55 RID: 8021
		internal const bool StackHalt = false;
	}
}
