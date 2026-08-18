using System;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security
{
	// Token: 0x0200067C RID: 1660
	[Serializable]
	internal sealed class PermissionToken : ISecurityEncodable
	{
		// Token: 0x06003BEE RID: 15342 RVA: 0x000CC64C File Offset: 0x000CB64C
		internal static bool IsMscorlibClassName(string className)
		{
			int num = className.IndexOf(',');
			if (num == -1)
			{
				return true;
			}
			num = className.LastIndexOf(']');
			if (num == -1)
			{
				num = 0;
			}
			for (int i = num; i < className.Length; i++)
			{
				if ((className[i] == 'm' || className[i] == 'M') && string.Compare(className, i, "mscorlib", 0, "mscorlib".Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x000CC6BB File Offset: 0x000CB6BB
		static PermissionToken()
		{
			PermissionToken.s_theTokenFactory = new PermissionTokenFactory(4);
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x000CC6D8 File Offset: 0x000CB6D8
		internal PermissionToken()
		{
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000CC6E0 File Offset: 0x000CB6E0
		internal PermissionToken(int index, PermissionTokenType type, string strTypeName)
		{
			this.m_index = index;
			this.m_type = type;
			this.m_strTypeName = strTypeName;
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x000CC700 File Offset: 0x000CB700
		public static PermissionToken GetToken(Type cls)
		{
			if (cls == null)
			{
				return null;
			}
			if (cls.GetInterface("System.Security.Permissions.IBuiltInPermission") != null)
			{
				if (PermissionToken.s_reflectPerm == null)
				{
					PermissionToken.s_reflectPerm = new ReflectionPermission(PermissionState.Unrestricted);
				}
				PermissionToken.s_reflectPerm.Assert();
				MethodInfo method = cls.GetMethod("GetTokenIndex", BindingFlags.Static | BindingFlags.NonPublic);
				RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
				int index = (int)runtimeMethodInfo.Invoke(null, BindingFlags.Default, null, null, null, true);
				return PermissionToken.s_theTokenFactory.BuiltInGetToken(index, null, cls);
			}
			return PermissionToken.s_theTokenFactory.GetToken(cls, null);
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x000CC77C File Offset: 0x000CB77C
		public static PermissionToken GetToken(IPermission perm)
		{
			if (perm == null)
			{
				return null;
			}
			IBuiltInPermission builtInPermission = perm as IBuiltInPermission;
			if (builtInPermission != null)
			{
				return PermissionToken.s_theTokenFactory.BuiltInGetToken(builtInPermission.GetTokenIndex(), perm, null);
			}
			return PermissionToken.s_theTokenFactory.GetToken(perm.GetType(), perm);
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x000CC7BC File Offset: 0x000CB7BC
		public static PermissionToken GetToken(string typeStr)
		{
			return PermissionToken.GetToken(typeStr, false);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000CC7C8 File Offset: 0x000CB7C8
		public static PermissionToken GetToken(string typeStr, bool bCreateMscorlib)
		{
			if (typeStr == null)
			{
				return null;
			}
			if (!PermissionToken.IsMscorlibClassName(typeStr))
			{
				return PermissionToken.s_theTokenFactory.GetToken(typeStr);
			}
			if (!bCreateMscorlib)
			{
				return null;
			}
			return PermissionToken.FindToken(Type.GetType(typeStr));
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x000CC800 File Offset: 0x000CB800
		public static PermissionToken FindToken(Type cls)
		{
			if (cls == null)
			{
				return null;
			}
			if (cls.GetInterface("System.Security.Permissions.IBuiltInPermission") != null)
			{
				if (PermissionToken.s_reflectPerm == null)
				{
					PermissionToken.s_reflectPerm = new ReflectionPermission(PermissionState.Unrestricted);
				}
				PermissionToken.s_reflectPerm.Assert();
				MethodInfo method = cls.GetMethod("GetTokenIndex", BindingFlags.Static | BindingFlags.NonPublic);
				RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
				int index = (int)runtimeMethodInfo.Invoke(null, BindingFlags.Default, null, null, null, true);
				return PermissionToken.s_theTokenFactory.BuiltInGetToken(index, null, cls);
			}
			return PermissionToken.s_theTokenFactory.FindToken(cls);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x000CC87B File Offset: 0x000CB87B
		public static PermissionToken FindTokenByIndex(int i)
		{
			return PermissionToken.s_theTokenFactory.FindTokenByIndex(i);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000CC888 File Offset: 0x000CB888
		public static bool IsTokenProperlyAssigned(IPermission perm, PermissionToken token)
		{
			PermissionToken token2 = PermissionToken.GetToken(perm);
			return token2.m_index == token.m_index && token.m_type == token2.m_type && (perm.GetType().Module.Assembly != Assembly.GetExecutingAssembly() || token2.m_index < 17);
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x000CC8E0 File Offset: 0x000CB8E0
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("PermissionToken");
			if ((this.m_type & PermissionTokenType.BuiltIn) != (PermissionTokenType)0)
			{
				securityElement.AddAttribute("Index", "" + this.m_index);
			}
			else
			{
				securityElement.AddAttribute("Name", SecurityElement.Escape(this.m_strTypeName));
			}
			securityElement.AddAttribute("Type", this.m_type.ToString("F"));
			return securityElement;
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000CC95C File Offset: 0x000CB95C
		public void FromXml(SecurityElement elRoot)
		{
			elRoot.Tag.Equals("PermissionToken");
			string text = elRoot.Attribute("Name");
			PermissionToken permissionToken;
			if (text != null)
			{
				permissionToken = PermissionToken.GetToken(text, true);
			}
			else
			{
				permissionToken = PermissionToken.FindTokenByIndex(int.Parse(elRoot.Attribute("Index"), CultureInfo.InvariantCulture));
			}
			this.m_index = permissionToken.m_index;
			this.m_type = (PermissionTokenType)Enum.Parse(typeof(PermissionTokenType), elRoot.Attribute("Type"));
			this.m_strTypeName = permissionToken.m_strTypeName;
		}

		// Token: 0x04001EED RID: 7917
		private const string c_mscorlibName = "mscorlib";

		// Token: 0x04001EEE RID: 7918
		private static readonly PermissionTokenFactory s_theTokenFactory;

		// Token: 0x04001EEF RID: 7919
		private static ReflectionPermission s_reflectPerm = null;

		// Token: 0x04001EF0 RID: 7920
		internal int m_index;

		// Token: 0x04001EF1 RID: 7921
		internal PermissionTokenType m_type;

		// Token: 0x04001EF2 RID: 7922
		internal string m_strTypeName;

		// Token: 0x04001EF3 RID: 7923
		internal static TokenBasedSet s_tokenSet = new TokenBasedSet();
	}
}
