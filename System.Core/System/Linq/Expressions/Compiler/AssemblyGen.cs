using System;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000277 RID: 631
	internal sealed class AssemblyGen
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x0004A300 File Offset: 0x00048500
		private static AssemblyGen Assembly
		{
			get
			{
				if (AssemblyGen._assembly == null)
				{
					Interlocked.CompareExchange<AssemblyGen>(ref AssemblyGen._assembly, new AssemblyGen(), null);
				}
				return AssemblyGen._assembly;
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0004A320 File Offset: 0x00048520
		private AssemblyGen()
		{
			AssemblyName assemblyName = new AssemblyName("Snippets");
			CustomAttributeBuilder[] assemblyAttributes = new CustomAttributeBuilder[]
			{
				new CustomAttributeBuilder(typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes), new object[0])
			};
			this._myAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run, assemblyAttributes);
			this._myModule = this._myAssembly.DefineDynamicModule(assemblyName.Name, false);
			this._myAssembly.DefineVersionInfoResource();
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0004A3A0 File Offset: 0x000485A0
		private TypeBuilder DefineType(string name, Type parent, TypeAttributes attr)
		{
			ContractUtils.RequiresNotNull(name, "name");
			ContractUtils.RequiresNotNull(parent, "parent");
			StringBuilder stringBuilder = new StringBuilder(name);
			int value = Interlocked.Increment(ref this._index);
			stringBuilder.Append("$");
			stringBuilder.Append(value);
			stringBuilder.Replace('+', '_').Replace('[', '_').Replace(']', '_').Replace('*', '_').Replace('&', '_').Replace(',', '_').Replace('\\', '_');
			name = stringBuilder.ToString();
			return this._myModule.DefineType(name, attr, parent);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0004A441 File Offset: 0x00048641
		internal static TypeBuilder DefineDelegateType(string name)
		{
			return AssemblyGen.Assembly.DefineType(name, typeof(MulticastDelegate), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AutoClass);
		}

		// Token: 0x04000B2B RID: 2859
		private static AssemblyGen _assembly;

		// Token: 0x04000B2C RID: 2860
		private readonly AssemblyBuilder _myAssembly;

		// Token: 0x04000B2D RID: 2861
		private readonly ModuleBuilder _myModule;

		// Token: 0x04000B2E RID: 2862
		private int _index;
	}
}
