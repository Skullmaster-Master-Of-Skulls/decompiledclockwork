using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Security;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004AE RID: 1198
	internal class AspProxy
	{
		// Token: 0x06002C28 RID: 11304 RVA: 0x000D6C14 File Offset: 0x000D4E14
		internal bool IsAspNetEnvironment()
		{
			if (!this.TryInitializeWebAssembly())
			{
				return false;
			}
			bool result;
			try
			{
				string text = this.InternalMapWebPath("~");
				result = (text != null);
			}
			catch (SecurityException)
			{
				result = false;
			}
			catch (Exception e)
			{
				if (!e.IsCatchableExceptionType())
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000D6C74 File Offset: 0x000D4E74
		public bool TryInitializeWebAssembly()
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
			if (!AspProxy.IsSystemWebLoaded())
			{
				return false;
			}
			try
			{
				this._webAssembly = Assembly.Load("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				return this._webAssembly != null;
			}
			catch (Exception e)
			{
				if (!e.IsCatchableExceptionType())
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000D6D2C File Offset: 0x000D4F2C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static bool IsSystemWebLoaded()
		{
			try
			{
				return AppDomain.CurrentDomain.GetAssemblies().Any((Assembly a) => a.GetName().Name == "System.Web" && a.GetName().GetPublicKeyToken() != null && a.GetName().GetPublicKeyToken().SequenceEqual(AspProxy._systemWebPublicKeyToken));
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000D6D80 File Offset: 0x000D4F80
		private void InitializeWebAssembly()
		{
			if (!this.TryInitializeWebAssembly())
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext);
			}
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000D6D98 File Offset: 0x000D4F98
		internal string MapWebPath(string path)
		{
			path = this.InternalMapWebPath(path);
			if (path == null)
			{
				string message = Strings.InvalidUseOfWebPath("~");
				throw new InvalidOperationException(message);
			}
			return path;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000D6DC4 File Offset: 0x000D4FC4
		internal string InternalMapWebPath(string path)
		{
			this.InitializeWebAssembly();
			string result;
			try
			{
				Type type = this._webAssembly.GetType("System.Web.Hosting.HostingEnvironment", true);
				MethodInfo declaredMethod = type.GetDeclaredMethod("MapPath", new Type[]
				{
					typeof(string)
				});
				result = (string)declaredMethod.Invoke(null, new object[]
				{
					path
				});
			}
			catch (TargetException innerException)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException2);
			}
			catch (TargetInvocationException innerException3)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException3);
			}
			catch (TargetParameterCountException innerException4)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException4);
			}
			catch (MethodAccessException innerException5)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException5);
			}
			catch (MemberAccessException innerException6)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException6);
			}
			catch (TypeLoadException innerException7)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException7);
			}
			return result;
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000D6EEC File Offset: 0x000D50EC
		internal bool HasBuildManagerType()
		{
			Type type;
			return this.TryGetBuildManagerType(out type);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000D6F01 File Offset: 0x000D5101
		private bool TryGetBuildManagerType(out Type buildManager)
		{
			this.InitializeWebAssembly();
			buildManager = this._webAssembly.GetType("System.Web.Compilation.BuildManager", false);
			return buildManager != null;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000D6F24 File Offset: 0x000D5124
		internal IEnumerable<Assembly> GetBuildManagerReferencedAssemblies()
		{
			MethodInfo referencedAssembliesMethod = this.GetReferencedAssembliesMethod();
			if (referencedAssembliesMethod == null)
			{
				return new List<Assembly>();
			}
			IEnumerable<Assembly> result;
			try
			{
				ICollection collection = (ICollection)referencedAssembliesMethod.Invoke(null, null);
				if (collection == null)
				{
					result = new List<Assembly>();
				}
				else
				{
					result = collection.Cast<Assembly>();
				}
			}
			catch (TargetException innerException)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException);
			}
			catch (TargetInvocationException innerException2)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException2);
			}
			catch (MethodAccessException innerException3)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, innerException3);
			}
			return result;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000D6FC0 File Offset: 0x000D51C0
		internal MethodInfo GetReferencedAssembliesMethod()
		{
			Type type;
			if (!this.TryGetBuildManagerType(out type))
			{
				throw new InvalidOperationException(Strings.UnableToFindReflectedType("System.Web.Compilation.BuildManager", "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
			}
			return type.GetDeclaredMethod("GetReferencedAssemblies", new Type[0]);
		}

		// Token: 0x0400104C RID: 4172
		private const string BUILD_MANAGER_TYPE_NAME = "System.Web.Compilation.BuildManager";

		// Token: 0x0400104D RID: 4173
		private const string AspNetAssemblyName = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400104E RID: 4174
		private static readonly byte[] _systemWebPublicKeyToken = ScalarType.ConvertToByteArray("b03f5f7f11d50a3a");

		// Token: 0x0400104F RID: 4175
		private Assembly _webAssembly;

		// Token: 0x04001050 RID: 4176
		private bool _triedLoadingWebAssembly;
	}
}
