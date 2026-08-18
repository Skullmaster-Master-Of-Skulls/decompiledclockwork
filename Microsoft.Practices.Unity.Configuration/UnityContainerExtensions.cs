using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000041 RID: 65
	public static class UnityContainerExtensions
	{
		// Token: 0x06000219 RID: 537 RVA: 0x000074D1 File Offset: 0x000056D1
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static IUnityContainer LoadConfiguration(this IUnityContainer container, UnityConfigurationSection section, string containerName)
		{
			Guard.ArgumentNotNull(container, "container");
			Guard.ArgumentNotNull(section, "section");
			section.Configure(container, containerName);
			return container;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000074F4 File Offset: 0x000056F4
		public static IUnityContainer LoadConfiguration(this IUnityContainer container, string containerName)
		{
			Guard.ArgumentNotNull(container, "container");
			UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
			return container.LoadConfiguration(section, containerName);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007524 File Offset: 0x00005724
		public static IUnityContainer LoadConfiguration(this IUnityContainer container)
		{
			Guard.ArgumentNotNull(container, "container");
			UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
			return container.LoadConfiguration(section, string.Empty);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007558 File Offset: 0x00005758
		public static IUnityContainer LoadConfiguration(this IUnityContainer container, UnityConfigurationSection section)
		{
			Guard.ArgumentNotNull(container, "container");
			Guard.ArgumentNotNull(section, "section");
			return container.LoadConfiguration(section, string.Empty);
		}
	}
}
