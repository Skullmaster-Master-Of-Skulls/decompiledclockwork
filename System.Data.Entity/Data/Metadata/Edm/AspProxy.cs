using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Security;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200020E RID: 526
	internal class AspProxy
	{
		// Token: 0x060022E6 RID: 8934 RVA: 0x0007BFA8 File Offset: 0x0007A1A8
		internal bool IsAspNetEnvironment()
		{
			if (!this.TryInitializeWebAssembly())
			{
				return false;
			}
			bool result;
			try
			{
				string text = this.PrivateMapWebPath("~");
				result = (text != null);
			}
			catch (SecurityException)
			{
				result = false;
			}
			catch (Exception e)
			{
				if (!EntityUtil.IsCatchableExceptionType(e))
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0007C004 File Offset: 0x0007A204
		private bool TryInitializeWebAssembly()
		{
			if (this._webAssembly != null)
			{
				return true;
			}
			if (this._triedLoadingWebAssembly)
			{
				return false;
			}
			this._triedLoadingWebAssembly = true;
			try
			{
				this._webAssembly = Assembly.Load("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				return this._webAssembly != null;
			}
			catch (Exception e)
			{
				if (!EntityUtil.IsCatchableExceptionType(e))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0007C074 File Offset: 0x0007A274
		private void InitializeWebAssembly()
		{
			if (!this.TryInitializeWebAssembly())
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext);
			}
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0007C08C File Offset: 0x0007A28C
		internal string MapWebPath(string path)
		{
			path = this.PrivateMapWebPath(path);
			if (path == null)
			{
				string error = Strings.InvalidUseOfWebPath("~");
				throw EntityUtil.InvalidOperation(error);
			}
			return path;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0007C0B8 File Offset: 0x0007A2B8
		private string PrivateMapWebPath(string path)
		{
			this.InitializeWebAssembly();
			string result;
			try
			{
				Type type = this._webAssembly.GetType("System.Web.Hosting.HostingEnvironment", true);
				MethodInfo method = type.GetMethod("MapPath");
				result = (string)method.Invoke(null, new object[]
				{
					path
				});
			}
			catch (TargetException inner)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner);
			}
			catch (ArgumentException inner2)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner2);
			}
			catch (TargetInvocationException inner3)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner3);
			}
			catch (TargetParameterCountException inner4)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner4);
			}
			catch (MethodAccessException inner5)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner5);
			}
			catch (MemberAccessException inner6)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner6);
			}
			catch (TypeLoadException inner7)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner7);
			}
			return result;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0007C1C4 File Offset: 0x0007A3C4
		internal bool HasBuildManagerType()
		{
			Type type;
			return this.TryGetBuildManagerType(out type);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0007C1D9 File Offset: 0x0007A3D9
		private bool TryGetBuildManagerType(out Type buildManager)
		{
			this.InitializeWebAssembly();
			buildManager = this._webAssembly.GetType("System.Web.Compilation.BuildManager", false);
			return buildManager != null;
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0007C1FC File Offset: 0x0007A3FC
		internal IEnumerable<Assembly> GetBuildManagerReferencedAssemblies()
		{
			Type type;
			if (!this.TryGetBuildManagerType(out type))
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToFindReflectedType("System.Web.Compilation.BuildManager", "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
			}
			MethodInfo method = type.GetMethod("GetReferencedAssemblies", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod);
			if (method == null)
			{
				return new List<Assembly>();
			}
			IEnumerable<Assembly> result;
			try
			{
				ICollection collection = (ICollection)method.Invoke(null, null);
				if (collection == null)
				{
					result = new List<Assembly>();
				}
				else
				{
					result = collection.Cast<Assembly>();
				}
			}
			catch (TargetException inner)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner);
			}
			catch (TargetInvocationException inner2)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner2);
			}
			catch (MethodAccessException inner3)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, inner3);
			}
			return result;
		}

		// Token: 0x04000F8A RID: 3978
		private const string BUILD_MANAGER_TYPE_NAME = "System.Web.Compilation.BuildManager";

		// Token: 0x04000F8B RID: 3979
		private Assembly _webAssembly;

		// Token: 0x04000F8C RID: 3980
		private bool _triedLoadingWebAssembly;
	}
}
