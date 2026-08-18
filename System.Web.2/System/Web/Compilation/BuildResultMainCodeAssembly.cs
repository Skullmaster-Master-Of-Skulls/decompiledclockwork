using System;
using System.Reflection;
using System.Web.Hosting;

namespace System.Web.Compilation
{
	// Token: 0x02000816 RID: 2070
	internal class BuildResultMainCodeAssembly : BuildResultCompiledAssembly
	{
		// Token: 0x06006330 RID: 25392 RVA: 0x0015B9AF File Offset: 0x00159BAF
		internal BuildResultMainCodeAssembly()
		{
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x0015BC99 File Offset: 0x00159E99
		internal BuildResultMainCodeAssembly(Assembly a) : base(a)
		{
			this.FindAppInitializeMethod();
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x0015BCA8 File Offset: 0x00159EA8
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.BuildResultMainCodeAssembly;
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x0015BCAC File Offset: 0x00159EAC
		internal override void GetPreservedAttributes(PreservationFileReader pfr)
		{
			base.GetPreservedAttributes(pfr);
			string attribute = pfr.GetAttribute("appInitializeClass");
			if (attribute != null)
			{
				Type type = this.ResultAssembly.GetType(attribute);
				this._appInitializeMethod = this.FindAppInitializeMethod(type);
			}
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x0015BCE9 File Offset: 0x00159EE9
		internal override void SetPreservedAttributes(PreservationFileWriter pfw)
		{
			base.SetPreservedAttributes(pfw);
			if (this._appInitializeMethod != null)
			{
				pfw.SetAttribute("appInitializeClass", this._appInitializeMethod.ReflectedType.FullName);
			}
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x0015BD1C File Offset: 0x00159F1C
		private void FindAppInitializeMethod()
		{
			foreach (Type type in this.ResultAssembly.GetExportedTypes())
			{
				MethodInfo methodInfo = this.FindAppInitializeMethod(type);
				if (methodInfo != null)
				{
					if (this._appInitializeMethod != null)
					{
						throw new HttpException(SR.GetString("Duplicate_appinitialize", new object[]
						{
							this._appInitializeMethod.ReflectedType.FullName,
							type.FullName
						}));
					}
					this._appInitializeMethod = methodInfo;
				}
			}
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x0015BD9F File Offset: 0x00159F9F
		private MethodInfo FindAppInitializeMethod(Type t)
		{
			return t.GetMethod("AppInitialize", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public, null, new Type[0], null);
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x0015BDB8 File Offset: 0x00159FB8
		internal void CallAppInitializeMethod()
		{
			if (this._appInitializeMethod != null)
			{
				using (new ApplicationImpersonationContext())
				{
					using (HostingEnvironment.SetCultures())
					{
						this._appInitializeMethod.Invoke(null, null);
					}
				}
			}
		}

		// Token: 0x04003374 RID: 13172
		private const string appInitializeMethodName = "AppInitialize";

		// Token: 0x04003375 RID: 13173
		private MethodInfo _appInitializeMethod;
	}
}
