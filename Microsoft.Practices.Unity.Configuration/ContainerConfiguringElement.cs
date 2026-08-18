using System;
using System.Globalization;
using System.Threading;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000016 RID: 22
	public abstract class ContainerConfiguringElement : DeserializableConfigurationElement
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000043FC File Offset: 0x000025FC
		protected ContainerConfiguringElement()
		{
			this.configuringElementNum = Interlocked.Increment(ref ContainerConfiguringElement.configuringElementCount);
		}

		// Token: 0x060000B6 RID: 182
		protected abstract void ConfigureContainer(IUnityContainer container);

		// Token: 0x060000B7 RID: 183 RVA: 0x00004414 File Offset: 0x00002614
		internal void ConfigureContainerInternal(IUnityContainer container)
		{
			this.ConfigureContainer(container);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004420 File Offset: 0x00002620
		public virtual string Key
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "configuring:{0}", new object[]
				{
					this.configuringElementNum
				});
			}
		}

		// Token: 0x04000027 RID: 39
		private static int configuringElementCount;

		// Token: 0x04000028 RID: 40
		private readonly int configuringElementNum;
	}
}
