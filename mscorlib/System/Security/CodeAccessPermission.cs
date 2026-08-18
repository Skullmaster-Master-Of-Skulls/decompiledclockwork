using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security
{
	// Token: 0x02000626 RID: 1574
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
	{
		// Token: 0x060038AC RID: 14508 RVA: 0x000BEFB4 File Offset: 0x000BDFB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertAssert()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertAssert(ref stackCrawlMark);
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000BEFCC File Offset: 0x000BDFCC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertDeny()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertDeny(ref stackCrawlMark);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000BEFE4 File Offset: 0x000BDFE4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertPermitOnly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertPermitOnly(ref stackCrawlMark);
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000BEFFC File Offset: 0x000BDFFC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertAll()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertAll(ref stackCrawlMark);
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000BF014 File Offset: 0x000BE014
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Demand()
		{
			if (!this.CheckDemand(null))
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCallersCaller;
				CodeAccessSecurityEngine.Check(this, ref stackCrawlMark);
			}
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000BF034 File Offset: 0x000BE034
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void DemandInternal(PermissionType permissionType)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCallersCaller;
			CodeAccessSecurityEngine.SpecialDemand(permissionType, ref stackCrawlMark);
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000BF04C File Offset: 0x000BE04C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Assert()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CodeAccessSecurityEngine.Assert(this, ref stackCrawlMark);
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000BF064 File Offset: 0x000BE064
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void AssertAllPossible()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.AssertAllPossible(ref stackCrawlMark);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000BF07C File Offset: 0x000BE07C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Deny()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CodeAccessSecurityEngine.Deny(this, ref stackCrawlMark);
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000BF094 File Offset: 0x000BE094
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void PermitOnly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CodeAccessSecurityEngine.PermitOnly(this, ref stackCrawlMark);
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000BF0AB File Offset: 0x000BE0AB
		public virtual IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SecurityPermissionUnion"));
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000BF0C8 File Offset: 0x000BE0C8
		internal static SecurityElement CreatePermissionElement(IPermission perm, string permname)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			XMLUtil.AddClassAttribute(securityElement, perm.GetType(), permname);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000BF100 File Offset: 0x000BE100
		internal static void ValidateElement(SecurityElement elem, IPermission perm)
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			if (!XMLUtil.IsPermissionElement(perm, elem))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotAPermissionElement"));
			}
			string text = elem.Attribute("version");
			if (text != null && !text.Equals("1"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLBadVersion"));
			}
		}

		// Token: 0x060038B9 RID: 14521
		public abstract SecurityElement ToXml();

		// Token: 0x060038BA RID: 14522
		public abstract void FromXml(SecurityElement elem);

		// Token: 0x060038BB RID: 14523 RVA: 0x000BF160 File Offset: 0x000BE160
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000BF16D File Offset: 0x000BE16D
		internal bool VerifyType(IPermission perm)
		{
			return perm != null && perm.GetType() == base.GetType();
		}

		// Token: 0x060038BD RID: 14525
		public abstract IPermission Copy();

		// Token: 0x060038BE RID: 14526
		public abstract IPermission Intersect(IPermission target);

		// Token: 0x060038BF RID: 14527
		public abstract bool IsSubsetOf(IPermission target);

		// Token: 0x060038C0 RID: 14528 RVA: 0x000BF184 File Offset: 0x000BE184
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			IPermission permission = obj as IPermission;
			if (obj != null && permission == null)
			{
				return false;
			}
			try
			{
				if (!this.IsSubsetOf(permission))
				{
					return false;
				}
				if (permission != null && !permission.IsSubsetOf(this))
				{
					return false;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000BF1D8 File Offset: 0x000BE1D8
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000BF1E0 File Offset: 0x000BE1E0
		internal bool CheckDemand(CodeAccessPermission grant)
		{
			return this.IsSubsetOf(grant);
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000BF1E9 File Offset: 0x000BE1E9
		internal bool CheckPermitOnly(CodeAccessPermission permitted)
		{
			return this.IsSubsetOf(permitted);
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000BF1F4 File Offset: 0x000BE1F4
		internal bool CheckDeny(CodeAccessPermission denied)
		{
			IPermission permission = this.Intersect(denied);
			return permission == null || permission.IsSubsetOf(null);
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000BF215 File Offset: 0x000BE215
		internal bool CheckAssert(CodeAccessPermission asserted)
		{
			return this.IsSubsetOf(asserted);
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000BF21E File Offset: 0x000BE21E
		internal bool CanUnrestrictedOverride()
		{
			return CodeAccessPermission.CanUnrestrictedOverride(this);
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000BF226 File Offset: 0x000BE226
		internal static bool CanUnrestrictedOverride(IPermission ip)
		{
			return CodeAccessSecurityEngine.DoesFullTrustMeanFullTrust() || ip is IUnrestrictedPermission;
		}
	}
}
