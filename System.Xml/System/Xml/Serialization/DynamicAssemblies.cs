using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;

namespace System.Xml.Serialization
{
	// Token: 0x02000333 RID: 819
	internal static class DynamicAssemblies
	{
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x000CEE21 File Offset: 0x000CDE21
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

		// Token: 0x060027FF RID: 10239 RVA: 0x000CEE3C File Offset: 0x000CDE3C
		internal static bool IsTypeDynamic(Type type)
		{
			object obj = DynamicAssemblies.tableIsTypeDynamic[type];
			if (obj == null)
			{
				DynamicAssemblies.UnrestrictedFileIOPermission.Assert();
				Module module = type.Module;
				Assembly assembly = module.Assembly;
				bool flag = module is ModuleBuilder || assembly.Location == null || assembly.Location.Length == 0;
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
								if (type2 != null && !type2.IsGenericParameter)
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

		// Token: 0x06002800 RID: 10240 RVA: 0x000CEF10 File Offset: 0x000CDF10
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

		// Token: 0x06002801 RID: 10241 RVA: 0x000CEF40 File Offset: 0x000CDF40
		internal static void Add(Assembly a)
		{
			lock (DynamicAssemblies.nameToAssemblyMap)
			{
				if (DynamicAssemblies.assemblyToNameMap[a] == null)
				{
					Assembly assembly = DynamicAssemblies.nameToAssemblyMap[a.FullName] as Assembly;
					string text = null;
					if (assembly == null)
					{
						text = a.FullName;
					}
					else if (assembly != a)
					{
						text = a.FullName + ", " + DynamicAssemblies.nameToAssemblyMap.Count;
					}
					if (text != null)
					{
						DynamicAssemblies.nameToAssemblyMap.Add(text, a);
						DynamicAssemblies.assemblyToNameMap.Add(a, text);
					}
				}
			}
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000CEFE4 File Offset: 0x000CDFE4
		internal static Assembly Get(string fullName)
		{
			if (DynamicAssemblies.nameToAssemblyMap == null)
			{
				return null;
			}
			return (Assembly)DynamicAssemblies.nameToAssemblyMap[fullName];
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000CEFFF File Offset: 0x000CDFFF
		internal static string GetName(Assembly a)
		{
			if (DynamicAssemblies.assemblyToNameMap == null)
			{
				return null;
			}
			return (string)DynamicAssemblies.assemblyToNameMap[a];
		}

		// Token: 0x04001666 RID: 5734
		private static ArrayList assembliesInConfig = new ArrayList();

		// Token: 0x04001667 RID: 5735
		private static Hashtable nameToAssemblyMap = new Hashtable();

		// Token: 0x04001668 RID: 5736
		private static Hashtable assemblyToNameMap = new Hashtable();

		// Token: 0x04001669 RID: 5737
		private static Hashtable tableIsTypeDynamic = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400166A RID: 5738
		private static FileIOPermission fileIOPermission;
	}
}
