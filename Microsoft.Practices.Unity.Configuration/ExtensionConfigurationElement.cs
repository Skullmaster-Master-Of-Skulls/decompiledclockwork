using System;
using System.Globalization;
using System.Threading;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000023 RID: 35
	public abstract class ExtensionConfigurationElement : DeserializableConfigurationElement
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000508C File Offset: 0x0000328C
		protected ExtensionConfigurationElement()
		{
			this.extensionConfigurationNumber = Interlocked.Increment(ref ExtensionConfigurationElement.extensionConfigurationCount);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000050A4 File Offset: 0x000032A4
		public string Key
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "extensionConfig:{0}", new object[]
				{
					this.extensionConfigurationNumber
				});
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000050D6 File Offset: 0x000032D6
		internal void Configure(IUnityContainer container)
		{
			this.ConfigureContainer(container);
		}

		// Token: 0x06000113 RID: 275
		protected abstract void ConfigureContainer(IUnityContainer container);

		// Token: 0x04000040 RID: 64
		private static int extensionConfigurationCount;

		// Token: 0x04000041 RID: 65
		private readonly int extensionConfigurationNumber;
	}
}
