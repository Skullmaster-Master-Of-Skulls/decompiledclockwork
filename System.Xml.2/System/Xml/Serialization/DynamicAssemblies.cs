using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.Xml.Serialization
{
	// Token: 0x020001B3 RID: 435
	internal static class DynamicAssemblies
	{
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x000A09B4 File Offset: 0x0009EBB4
		private static FileIOPermission UnrestrictedFileIOPermission
		{
			get
			{
				if (DynamicAssemblies.fileIOPermission == null)
				{
					DynamicAssemblies.fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				}
				return DynamicAssemblies.fileIOPermission;
			}
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000A09D4 File Offset: 0x0009EBD4
		internal static bool IsTypeDynamic(Type type)
		{
			object obj = DynamicAssemblies.tableIsTypeDynamic[type];
			if (obj == null)
			{
				DynamicAssemblies.UnrestrictedFileIOPermission.Assert();
				Assembly assembly = type.Assembly;
				bool flag = assembly.IsDynamic || string.IsNullOrEmpty(assembly.Location);
				if (!flag)
				{
					if (type.IsArray)
					{
						flag = DynamicAssemblies.IsTypeDynamic(type.GetElementType());
					}
					else if (type.IsGenericType)
					{
						Type[] genericArguments = type.GetGenericArguments();
						if (genericArguments != null)
						{
							foreach (Type type2 in genericArguments)
							{
								if (!(type2 == null) && !type2.IsGenericParameter)
								{
									flag = DynamicAssemblies.IsTypeDynamic(type2);
									if (flag)
									{
										break;
									}
								}
							}
						}
					}
				}
				obj = (DynamicAssemblies.tableIsTypeDynamic[type] = flag);
			}
			return (bool)obj;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000A0A98 File Offset: 0x0009EC98
		internal static bool IsTypeDynamic(Type[] arguments)
		{
			foreach (Type type in arguments)
			{
				if (DynamicAssemblies.IsTypeDynamic(type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000A0AC4 File Offset: 0x0009ECC4
		internal static void Add(Assembly a)
		{
			Hashtable obj = DynamicAssemblies.nameToAssemblyMap;
			lock (obj)
			{
				if (DynamicAssemblies.assemblyToNameMap[a] == null)
				{
					Assembly left = DynamicAssemblies.nameToAssemblyMap[a.FullName] as Assembly;
					string text = null;
					if (left == null)
					{
						text = a.FullName;
					}
					else if (left != a)
					{
						text = a.FullName + ", " + DynamicAssemblies.nameToAssemblyMap.Count.ToString();
					}
					if (text != null)
					{
						DynamicAssemblies.nameToAssemblyMap.Add(text, a);
						DynamicAssemblies.assemblyToNameMap.Add(a, text);
					}
				}
			}
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x000A0B90 File Offset: 0x0009ED90
		internal static Assembly Get(string fullName)
		{
			if (DynamicAssemblies.nameToAssemblyMap == null)
			{
				return null;
			}
			return (Assembly)DynamicAssemblies.nameToAssemblyMap[fullName];
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000A0BAF File Offset: 0x0009EDAF
		internal static string GetName(Assembly a)
		{
			if (DynamicAssemblies.assemblyToNameMap == null)
			{
				return null;
			}
			return (string)DynamicAssemblies.assemblyToNameMap[a];
		}

		// Token: 0x04000CCC RID: 3276
		private static ArrayList assembliesInConfig = new ArrayList();

		// Token: 0x04000CCD RID: 3277
		private static volatile Hashtable nameToAssemblyMap = new Hashtable();

		// Token: 0x04000CCE RID: 3278
		private static volatile Hashtable assemblyToNameMap = new Hashtable();

		// Token: 0x04000CCF RID: 3279
		private static Hashtable tableIsTypeDynamic = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000CD0 RID: 3280
		private static volatile FileIOPermission fileIOPermission;
	}
}
