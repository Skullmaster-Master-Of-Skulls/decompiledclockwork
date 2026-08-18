using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Threading;

namespace System.Security
{
	// Token: 0x02000693 RID: 1683
	[ComVisible(true)]
	public static class SecurityManager
	{
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000D0557 File Offset: 0x000CF557
		internal static PolicyManager PolicyManager
		{
			get
			{
				return SecurityManager.polmgr;
			}
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000D0560 File Offset: 0x000CF560
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool IsGranted(IPermission perm)
		{
			if (perm == null)
			{
				return true;
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			PermissionSet permissionSet;
			PermissionSet permissionSet2;
			SecurityManager._GetGrantedPermissions(out permissionSet, out permissionSet2, ref stackCrawlMark);
			return permissionSet.Contains(perm) && (permissionSet2 == null || !permissionSet2.Contains(perm));
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000D059C File Offset: 0x000CF59C
		private static bool CheckExecution()
		{
			if (SecurityManager.checkExecution == -1)
			{
				SecurityManager.checkExecution = (((SecurityManager.GetGlobalFlags() & 256) != 0) ? 0 : 1);
			}
			if (SecurityManager.checkExecution == 1)
			{
				if (SecurityManager.securityPermissionType == null)
				{
					SecurityManager.securityPermissionType = typeof(SecurityPermission);
					SecurityManager.executionSecurityPermission = new SecurityPermission(SecurityPermissionFlag.Execution);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000D05F4 File Offset: 0x000CF5F4
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, Name = "System.Windows.Forms", PublicKey = "0x00000000000000000400000000000000")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void GetZoneAndOrigin(out ArrayList zone, out ArrayList origin)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (SecurityManager._IsSecurityOn())
			{
				CodeAccessSecurityEngine.GetZoneAndOrigin(ref stackCrawlMark, out zone, out origin);
				return;
			}
			zone = null;
			origin = null;
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x000D061C File Offset: 0x000CF61C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		public static PolicyLevel LoadPolicyLevelFromFile(string path, PolicyLevelType type)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!File.InternalExists(path))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PolicyFileDoesNotExist"));
			}
			string fullPath = Path.GetFullPath(path);
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.None);
			fileIOPermission.AddPathList(FileIOPermissionAccess.Read, fullPath);
			fileIOPermission.AddPathList(FileIOPermissionAccess.Write, fullPath);
			fileIOPermission.Demand();
			PolicyLevel result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					result = SecurityManager.LoadPolicyLevelFromStringHelper(streamReader.ReadToEnd(), path, type);
				}
			}
			return result;
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000D06C8 File Offset: 0x000CF6C8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		public static PolicyLevel LoadPolicyLevelFromString(string str, PolicyLevelType type)
		{
			return SecurityManager.LoadPolicyLevelFromStringHelper(str, null, type);
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000D06D4 File Offset: 0x000CF6D4
		private static PolicyLevel LoadPolicyLevelFromStringHelper(string str, string path, PolicyLevelType type)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			PolicyLevel policyLevel = new PolicyLevel(type, path);
			Parser parser = new Parser(str);
			SecurityElement topElement = parser.GetTopElement();
			if (topElement == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), new object[]
				{
					"configuration"
				}));
			}
			SecurityElement securityElement = topElement.SearchForChildByTag("mscorlib");
			if (securityElement == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), new object[]
				{
					"mscorlib"
				}));
			}
			SecurityElement securityElement2 = securityElement.SearchForChildByTag("security");
			if (securityElement2 == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), new object[]
				{
					"security"
				}));
			}
			SecurityElement securityElement3 = securityElement2.SearchForChildByTag("policy");
			if (securityElement3 == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), new object[]
				{
					"policy"
				}));
			}
			SecurityElement securityElement4 = securityElement3.SearchForChildByTag("PolicyLevel");
			if (securityElement4 != null)
			{
				policyLevel.FromXml(securityElement4);
				return policyLevel;
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), new object[]
			{
				"PolicyLevel"
			}));
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000D0838 File Offset: 0x000CF838
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		public static void SavePolicyLevel(PolicyLevel level)
		{
			PolicyManager.EncodeLevel(level);
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x000D0840 File Offset: 0x000CF840
		private static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied, out int securitySpecialFlags, bool checkExecutionPermission)
		{
			CodeAccessPermission.AssertAllPossible();
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidence, reqdPset, optPset, denyPset, out denied, checkExecutionPermission);
			securitySpecialFlags = SecurityManager.GetSpecialFlags(permissionSet, denied);
			return permissionSet;
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000D086D File Offset: 0x000CF86D
		public static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied)
		{
			return SecurityManager.ResolvePolicy(evidence, reqdPset, optPset, denyPset, out denied, true);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000D087C File Offset: 0x000CF87C
		private static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied, bool checkExecutionPermission)
		{
			Exception exception = null;
			PermissionSet permissionSet;
			if (reqdPset == null)
			{
				permissionSet = optPset;
			}
			else
			{
				permissionSet = ((optPset == null) ? null : reqdPset.Union(optPset));
			}
			if (permissionSet != null && !permissionSet.IsUnrestricted() && SecurityManager.CheckExecution())
			{
				permissionSet.AddPermission(SecurityManager.executionSecurityPermission);
			}
			if (evidence == null)
			{
				evidence = new Evidence();
			}
			else
			{
				evidence = evidence.ShallowCopy();
			}
			evidence.AddHost(new PermissionRequestEvidence(reqdPset, optPset, denyPset));
			PermissionSet permissionSet2 = SecurityManager.polmgr.Resolve(evidence);
			if (permissionSet != null)
			{
				permissionSet2.InplaceIntersect(permissionSet);
			}
			if (checkExecutionPermission && SecurityManager.CheckExecution() && (!permissionSet2.Contains(SecurityManager.executionSecurityPermission) || (denyPset != null && denyPset.Contains(SecurityManager.executionSecurityPermission))))
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, exception);
			}
			if (reqdPset != null && !reqdPset.IsSubsetOf(permissionSet2))
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NoRequiredPermission"), -2146233321, exception);
			}
			if (denyPset != null)
			{
				denied = denyPset.Copy();
				permissionSet2.MergeDeniedSet(denied);
				if (denied.IsEmpty())
				{
					denied = null;
				}
			}
			else
			{
				denied = null;
			}
			permissionSet2.IgnoreTypeLoadFailures = true;
			return permissionSet2;
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x000D098B File Offset: 0x000CF98B
		public static PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				evidence = new Evidence();
			}
			else
			{
				evidence = evidence.ShallowCopy();
			}
			evidence.AddHost(new PermissionRequestEvidence(null, null, null));
			return SecurityManager.polmgr.Resolve(evidence);
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000D09BC File Offset: 0x000CF9BC
		public static PermissionSet ResolvePolicy(Evidence[] evidences)
		{
			if (evidences == null || evidences.Length == 0)
			{
				Evidence[] array = new Evidence[1];
				evidences = array;
			}
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidences[0]);
			if (permissionSet == null)
			{
				return null;
			}
			for (int i = 1; i < evidences.Length; i++)
			{
				permissionSet = permissionSet.Intersect(SecurityManager.ResolvePolicy(evidences[i]));
				if (permissionSet == null || permissionSet.IsEmpty())
				{
					return permissionSet;
				}
			}
			return permissionSet;
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000D0A14 File Offset: 0x000CFA14
		public static PermissionSet ResolveSystemPolicy(Evidence evidence)
		{
			if (PolicyManager.IsGacAssembly(evidence))
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			return SecurityManager.polmgr.CodeGroupResolve(evidence, true);
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000D0A31 File Offset: 0x000CFA31
		public static IEnumerator ResolvePolicyGroups(Evidence evidence)
		{
			return SecurityManager.polmgr.ResolveCodeGroups(evidence);
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000D0A3E File Offset: 0x000CFA3E
		public static IEnumerator PolicyHierarchy()
		{
			return SecurityManager.polmgr.PolicyHierarchy();
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000D0A4A File Offset: 0x000CFA4A
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		public static void SavePolicy()
		{
			SecurityManager.polmgr.Save();
			SecurityManager.SaveGlobalFlags();
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06003CF7 RID: 15607 RVA: 0x000D0A5B File Offset: 0x000CFA5B
		// (set) Token: 0x06003CF8 RID: 15608 RVA: 0x000D0A72 File Offset: 0x000CFA72
		public static bool CheckExecutionRights
		{
			get
			{
				return (SecurityManager.GetGlobalFlags() & 256) != 256;
			}
			set
			{
				if (value)
				{
					SecurityManager.checkExecution = 1;
					SecurityManager.SetGlobalFlags(256, 0);
					return;
				}
				new SecurityPermission(SecurityPermissionFlag.ControlPolicy).Demand();
				SecurityManager.checkExecution = 0;
				SecurityManager.SetGlobalFlags(256, 256);
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x000D0AAA File Offset: 0x000CFAAA
		// (set) Token: 0x06003CFA RID: 15610 RVA: 0x000D0AB1 File Offset: 0x000CFAB1
		[Obsolete("Because security can no longer be turned off permanently, setting the SecurityEnabled property no longer has any effect. Reading the property will still indicate whether security has been turned off temporarily.")]
		public static bool SecurityEnabled
		{
			get
			{
				return SecurityManager._IsSecurityOn();
			}
			set
			{
			}
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x000D0AB4 File Offset: 0x000CFAB4
		internal static int GetSpecialFlags(PermissionSet grantSet, PermissionSet deniedSet)
		{
			if (grantSet != null && grantSet.IsUnrestricted() && (deniedSet == null || deniedSet.IsEmpty()))
			{
				return -1;
			}
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.NoFlags;
			ReflectionPermissionFlag reflectionPermissionFlag = ReflectionPermissionFlag.NoFlags;
			CodeAccessPermission[] array = new CodeAccessPermission[6];
			if (grantSet != null)
			{
				if (grantSet.IsUnrestricted())
				{
					securityPermissionFlag = SecurityPermissionFlag.AllFlags;
					reflectionPermissionFlag = (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess);
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = SecurityManager.s_UnrestrictedSpecialPermissionMap[i];
					}
				}
				else
				{
					SecurityPermission securityPermission = grantSet.GetPermission(6) as SecurityPermission;
					if (securityPermission != null)
					{
						securityPermissionFlag = securityPermission.Flags;
					}
					ReflectionPermission reflectionPermission = grantSet.GetPermission(4) as ReflectionPermission;
					if (reflectionPermission != null)
					{
						reflectionPermissionFlag = reflectionPermission.Flags;
					}
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = (grantSet.GetPermission(SecurityManager.s_BuiltInPermissionIndexMap[j][0]) as CodeAccessPermission);
					}
				}
			}
			if (deniedSet != null)
			{
				if (deniedSet.IsUnrestricted())
				{
					securityPermissionFlag = SecurityPermissionFlag.NoFlags;
					reflectionPermissionFlag = ReflectionPermissionFlag.NoFlags;
					for (int k = 0; k < SecurityManager.s_BuiltInPermissionIndexMap.Length; k++)
					{
						array[k] = null;
					}
				}
				else
				{
					SecurityPermission securityPermission = deniedSet.GetPermission(6) as SecurityPermission;
					if (securityPermission != null)
					{
						securityPermissionFlag &= ~securityPermission.Flags;
					}
					ReflectionPermission reflectionPermission = deniedSet.GetPermission(4) as ReflectionPermission;
					if (reflectionPermission != null)
					{
						reflectionPermissionFlag &= ~reflectionPermission.Flags;
					}
					for (int l = 0; l < SecurityManager.s_BuiltInPermissionIndexMap.Length; l++)
					{
						CodeAccessPermission codeAccessPermission = deniedSet.GetPermission(SecurityManager.s_BuiltInPermissionIndexMap[l][0]) as CodeAccessPermission;
						if (codeAccessPermission != null && !codeAccessPermission.IsSubsetOf(null))
						{
							array[l] = null;
						}
					}
				}
			}
			int num = SecurityManager.MapToSpecialFlags(securityPermissionFlag, reflectionPermissionFlag);
			if (num != -1)
			{
				for (int m = 0; m < array.Length; m++)
				{
					if (array[m] != null && ((IUnrestrictedPermission)array[m]).IsUnrestricted())
					{
						num |= 1 << SecurityManager.s_BuiltInPermissionIndexMap[m][1];
					}
				}
			}
			return num;
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000D0C74 File Offset: 0x000CFC74
		private static int MapToSpecialFlags(SecurityPermissionFlag securityPermissionFlags, ReflectionPermissionFlag reflectionPermissionFlags)
		{
			int num = 0;
			if ((securityPermissionFlags & SecurityPermissionFlag.UnmanagedCode) == SecurityPermissionFlag.UnmanagedCode)
			{
				num |= 1;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.SkipVerification) == SecurityPermissionFlag.SkipVerification)
			{
				num |= 2;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.Assertion) == SecurityPermissionFlag.Assertion)
			{
				num |= 8;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.SerializationFormatter) == SecurityPermissionFlag.SerializationFormatter)
			{
				num |= 32;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.BindingRedirects) == SecurityPermissionFlag.BindingRedirects)
			{
				num |= 256;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.ControlEvidence) == SecurityPermissionFlag.ControlEvidence)
			{
				num |= 65536;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.ControlPrincipal) == SecurityPermissionFlag.ControlPrincipal)
			{
				num |= 131072;
			}
			if ((reflectionPermissionFlags & ReflectionPermissionFlag.RestrictedMemberAccess) == ReflectionPermissionFlag.RestrictedMemberAccess)
			{
				num |= 64;
			}
			if ((reflectionPermissionFlags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess)
			{
				num |= 16;
			}
			return num;
		}

		// Token: 0x06003CFD RID: 15613
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _IsSameType(string strLeft, string strRight);

		// Token: 0x06003CFE RID: 15614
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _SetThreadSecurity(bool bThreadSecurity);

		// Token: 0x06003CFF RID: 15615
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _IsSecurityOn();

		// Token: 0x06003D00 RID: 15616
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetGlobalFlags();

		// Token: 0x06003D01 RID: 15617
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetGlobalFlags(int mask, int flags);

		// Token: 0x06003D02 RID: 15618
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SaveGlobalFlags();

		// Token: 0x06003D03 RID: 15619
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void _GetGrantedPermissions(out PermissionSet granted, out PermissionSet denied, ref StackCrawlMark stackmark);

		// Token: 0x04001F4D RID: 8013
		private const int CheckExecutionRightsDisabledFlag = 256;

		// Token: 0x04001F4E RID: 8014
		private static Type securityPermissionType = null;

		// Token: 0x04001F4F RID: 8015
		private static SecurityPermission executionSecurityPermission = null;

		// Token: 0x04001F50 RID: 8016
		private static int checkExecution = -1;

		// Token: 0x04001F51 RID: 8017
		private static PolicyManager polmgr = new PolicyManager();

		// Token: 0x04001F52 RID: 8018
		private static int[][] s_BuiltInPermissionIndexMap = new int[][]
		{
			new int[]
			{
				0,
				10
			},
			new int[]
			{
				1,
				11
			},
			new int[]
			{
				2,
				12
			},
			new int[]
			{
				4,
				13
			},
			new int[]
			{
				6,
				14
			},
			new int[]
			{
				7,
				9
			}
		};

		// Token: 0x04001F53 RID: 8019
		private static CodeAccessPermission[] s_UnrestrictedSpecialPermissionMap = new CodeAccessPermission[]
		{
			new EnvironmentPermission(PermissionState.Unrestricted),
			new FileDialogPermission(PermissionState.Unrestricted),
			new FileIOPermission(PermissionState.Unrestricted),
			new ReflectionPermission(PermissionState.Unrestricted),
			new SecurityPermission(PermissionState.Unrestricted),
			new UIPermission(PermissionState.Unrestricted)
		};
	}
}
