using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer.Resources;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000028 RID: 40
	internal class SqlTypesAssemblyLoader
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000AD68 File Offset: 0x00008F68
		public static SqlTypesAssemblyLoader DefaultInstance
		{
			get
			{
				return SqlTypesAssemblyLoader._instance;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AD70 File Offset: 0x00008F70
		public SqlTypesAssemblyLoader(IEnumerable<string> assemblyNames = null)
		{
			this._preferredSqlTypesAssemblies = (assemblyNames ?? ((IEnumerable<string>)new string[]
			{
				"Microsoft.SqlServer.Types, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91",
				"Microsoft.SqlServer.Types, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
			}));
			this._latestVersion = new Lazy<SqlTypesAssembly>(new Func<SqlTypesAssembly>(this.BindToLatest), true);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		public SqlTypesAssemblyLoader(SqlTypesAssembly assembly)
		{
			this._latestVersion = new Lazy<SqlTypesAssembly>(() => assembly, true);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AE13 File Offset: 0x00009013
		public virtual SqlTypesAssembly TryGetSqlTypesAssembly()
		{
			return this._latestVersion.Value;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AE20 File Offset: 0x00009020
		public virtual SqlTypesAssembly GetSqlTypesAssembly()
		{
			SqlTypesAssembly value = this._latestVersion.Value;
			if (value == null)
			{
				throw new InvalidOperationException(Strings.SqlProvider_SqlTypesAssemblyNotFound);
			}
			return value;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AE48 File Offset: 0x00009048
		public virtual bool TryGetSqlTypesAssembly(Assembly assembly, out SqlTypesAssembly sqlAssembly)
		{
			if (this.IsKnownAssembly(assembly))
			{
				sqlAssembly = new SqlTypesAssembly(assembly);
				return true;
			}
			sqlAssembly = null;
			return false;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AE64 File Offset: 0x00009064
		private SqlTypesAssembly BindToLatest()
		{
			Assembly assembly = null;
			IEnumerable<string> enumerable = (SqlProviderServices.SqlServerTypesAssemblyName != null) ? new string[]
			{
				SqlProviderServices.SqlServerTypesAssemblyName
			} : this._preferredSqlTypesAssemblies;
			foreach (string assemblyName in enumerable)
			{
				AssemblyName assemblyRef = new AssemblyName(assemblyName);
				try
				{
					assembly = Assembly.Load(assemblyRef);
					break;
				}
				catch (FileNotFoundException)
				{
				}
				catch (FileLoadException)
				{
				}
			}
			if (assembly != null)
			{
				return new SqlTypesAssembly(assembly);
			}
			return null;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AF10 File Offset: 0x00009110
		private bool IsKnownAssembly(Assembly assembly)
		{
			foreach (string assemblyName in this._preferredSqlTypesAssemblies)
			{
				if (SqlTypesAssemblyLoader.AssemblyNamesMatch(assembly.FullName, new AssemblyName(assemblyName)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AF70 File Offset: 0x00009170
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		private static bool AssemblyNamesMatch(string infoRowProviderAssemblyName, AssemblyName targetAssemblyName)
		{
			if (string.IsNullOrWhiteSpace(infoRowProviderAssemblyName))
			{
				return false;
			}
			AssemblyName assemblyName;
			try
			{
				assemblyName = new AssemblyName(infoRowProviderAssemblyName);
			}
			catch (Exception)
			{
				return false;
			}
			if (!string.Equals(targetAssemblyName.Name, assemblyName.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (targetAssemblyName.Version == null || assemblyName.Version == null)
			{
				return false;
			}
			if (targetAssemblyName.Version.Major != assemblyName.Version.Major || targetAssemblyName.Version.Minor != assemblyName.Version.Minor)
			{
				return false;
			}
			byte[] publicKeyToken = targetAssemblyName.GetPublicKeyToken();
			return publicKeyToken != null && publicKeyToken.SequenceEqual(assemblyName.GetPublicKeyToken());
		}

		// Token: 0x0400007B RID: 123
		private static readonly SqlTypesAssemblyLoader _instance = new SqlTypesAssemblyLoader(null);

		// Token: 0x0400007C RID: 124
		private readonly IEnumerable<string> _preferredSqlTypesAssemblies;

		// Token: 0x0400007D RID: 125
		private readonly Lazy<SqlTypesAssembly> _latestVersion;
	}
}
