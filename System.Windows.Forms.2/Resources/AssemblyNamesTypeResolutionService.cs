using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace System.Resources
{
	// Token: 0x020000EE RID: 238
	internal class AssemblyNamesTypeResolutionService : ITypeResolutionService
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000A208 File Offset: 0x00008408
		internal AssemblyNamesTypeResolutionService(AssemblyName[] names)
		{
			this.names = names;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A217 File Offset: 0x00008417
		public Assembly GetAssembly(AssemblyName name)
		{
			return this.GetAssembly(name, true);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A224 File Offset: 0x00008424
		public Assembly GetAssembly(AssemblyName name, bool throwOnError)
		{
			Assembly assembly = null;
			if (this.cachedAssemblies == null)
			{
				this.cachedAssemblies = Hashtable.Synchronized(new Hashtable());
			}
			if (this.cachedAssemblies.Contains(name))
			{
				assembly = (this.cachedAssemblies[name] as Assembly);
			}
			if (assembly == null)
			{
				assembly = Assembly.LoadWithPartialName(name.FullName);
				if (assembly != null)
				{
					this.cachedAssemblies[name] = assembly;
				}
				else if (this.names != null)
				{
					for (int i = 0; i < this.names.Length; i++)
					{
						if (name.Equals(this.names[i]))
						{
							try
							{
								assembly = Assembly.LoadFrom(this.GetPathOfAssembly(name));
								if (assembly != null)
								{
									this.cachedAssemblies[name] = assembly;
								}
							}
							catch
							{
								if (throwOnError)
								{
									throw;
								}
							}
						}
					}
				}
			}
			return assembly;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A304 File Offset: 0x00008504
		public string GetPathOfAssembly(AssemblyName name)
		{
			return name.CodeBase;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000A30C File Offset: 0x0000850C
		public Type GetType(string name)
		{
			return this.GetType(name, true);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000A316 File Offset: 0x00008516
		public Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A324 File Offset: 0x00008524
		public Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			Type type = null;
			if (this.cachedTypes == null)
			{
				this.cachedTypes = Hashtable.Synchronized(new Hashtable(StringComparer.Ordinal));
			}
			if (this.cachedTypes.Contains(name))
			{
				type = (this.cachedTypes[name] as Type);
				return type;
			}
			if (name.IndexOf(',') != -1)
			{
				type = Type.GetType(name, false, ignoreCase);
			}
			if (type == null && this.names != null)
			{
				int num = name.IndexOf(',');
				if (num > 0 && num < name.Length - 1)
				{
					string assemblyName = name.Substring(num + 1).Trim();
					AssemblyName assemblyName2 = null;
					try
					{
						assemblyName2 = new AssemblyName(assemblyName);
					}
					catch
					{
					}
					if (assemblyName2 != null)
					{
						List<AssemblyName> list = new List<AssemblyName>(this.names.Length);
						for (int i = 0; i < this.names.Length; i++)
						{
							if (string.Compare(assemblyName2.Name, this.names[i].Name, StringComparison.OrdinalIgnoreCase) == 0)
							{
								list.Insert(0, this.names[i]);
							}
							else
							{
								list.Add(this.names[i]);
							}
						}
						this.names = list.ToArray();
					}
				}
				for (int j = 0; j < this.names.Length; j++)
				{
					Assembly assembly = this.GetAssembly(this.names[j], false);
					if (assembly != null)
					{
						type = assembly.GetType(name, false, ignoreCase);
						if (type == null)
						{
							int num2 = name.IndexOf(",");
							if (num2 != -1)
							{
								string name2 = name.Substring(0, num2);
								type = assembly.GetType(name2, false, ignoreCase);
							}
						}
					}
					if (type != null)
					{
						break;
					}
				}
			}
			if (type == null && throwOnError)
			{
				throw new ArgumentException(SR.GetString("InvalidResXNoType", new object[]
				{
					name
				}));
			}
			if (type != null && (type.Assembly.GlobalAssemblyCache || this.IsNetFrameworkAssembly(type.Assembly.Location)))
			{
				this.cachedTypes[name] = type;
			}
			return type;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A534 File Offset: 0x00008734
		private bool IsNetFrameworkAssembly(string assemblyPath)
		{
			return assemblyPath != null && assemblyPath.StartsWith(AssemblyNamesTypeResolutionService.NetFrameworkPath, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000A547 File Offset: 0x00008747
		public void ReferenceAssembly(AssemblyName name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040003CE RID: 974
		private AssemblyName[] names;

		// Token: 0x040003CF RID: 975
		private Hashtable cachedAssemblies;

		// Token: 0x040003D0 RID: 976
		private Hashtable cachedTypes;

		// Token: 0x040003D1 RID: 977
		private static string NetFrameworkPath = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "Microsoft.Net\\Framework");
	}
}
