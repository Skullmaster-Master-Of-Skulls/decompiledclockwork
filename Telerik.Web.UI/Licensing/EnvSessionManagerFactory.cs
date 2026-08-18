using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using Telerik.Licensing.Serialization;

namespace Telerik.Licensing
{
	// Token: 0x02000429 RID: 1065
	internal class EnvSessionManagerFactory : ISessionManagerFactory
	{
		// Token: 0x06002639 RID: 9785 RVA: 0x0007D684 File Offset: 0x0007B884
		public EnvSessionManagerFactory() : this(null)
		{
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x0007D68D File Offset: 0x0007B88D
		public EnvSessionManagerFactory(IServiceProvider serviceProvider)
		{
			EnvSessionManagerFactory.provider = serviceProvider;
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x0007D69B File Offset: 0x0007B89B
		public ISessionManager TryCreateManager()
		{
			return this.GetInstance();
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x0007D6A4 File Offset: 0x0007B8A4
		private EnvSessionManager GetInstance()
		{
			if (EnvSessionManagerFactory.manager == null)
			{
				lock (EnvSessionManagerFactory.managerLock)
				{
					if (EnvSessionManagerFactory.manager == null)
					{
						object envObject = this.GetEnvObject(EnvSessionManagerFactory.provider);
						EnvSessionManagerFactory.manager = ((envObject != null) ? new EnvSessionManager(new EnvDTEInterop(envObject), SerializationService.GetInstance()) : null);
					}
				}
			}
			return EnvSessionManagerFactory.manager;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x0007D718 File Offset: 0x0007B918
		private object GetEnvObject(IServiceProvider provider)
		{
			object result;
			if (this.TryGetEnvDteFromService(provider, out result))
			{
				return result;
			}
			string format = "VisualStudio.DTE.{0}.0";
			int num = 10;
			int num2 = 20;
			for (int i = num; i < num2; i++)
			{
				if (this.TryMarshalEnvDte(string.Format(format, i), out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x0007D768 File Offset: 0x0007B968
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		private bool TryGetEnvDteFromService(IServiceProvider provider, out object envdte)
		{
			envdte = null;
			bool result;
			try
			{
				string assemblyString = "envdte, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
				string name = "EnvDTE.DTE";
				Assembly assembly = Assembly.Load(assemblyString);
				Type type = assembly.GetType(name);
				envdte = provider.GetType().InvokeMember("GetService", BindingFlags.InvokeMethod, null, provider, new object[]
				{
					type
				});
				result = (envdte != null);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x0007D7E0 File Offset: 0x0007B9E0
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		[SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
		private bool TryMarshalEnvDte(string name, out object envdte)
		{
			envdte = null;
			bool result;
			try
			{
				envdte = Marshal.GetActiveObject(name);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040009C0 RID: 2496
		private static readonly object managerLock = new object();

		// Token: 0x040009C1 RID: 2497
		private static EnvSessionManager manager;

		// Token: 0x040009C2 RID: 2498
		private static IServiceProvider provider;
	}
}
