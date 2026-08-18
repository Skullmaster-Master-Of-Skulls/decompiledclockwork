using System;

namespace System.ServiceModel.Description
{
	// Token: 0x0200041A RID: 1050
	public sealed class MetadataImporterQuotas
	{
		// Token: 0x0600282B RID: 10283 RVA: 0x0009718C File Offset: 0x0009538C
		public MetadataImporterQuotas()
		{
			this.maxYields = 1024;
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x0009719F File Offset: 0x0009539F
		public static MetadataImporterQuotas Defaults
		{
			get
			{
				return MetadataImporterQuotas.CreateDefaultSettings();
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x000971A6 File Offset: 0x000953A6
		public static MetadataImporterQuotas Max
		{
			get
			{
				return MetadataImporterQuotas.CreateMaxSettings();
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000971AD File Offset: 0x000953AD
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x000971B5 File Offset: 0x000953B5
		internal int MaxPolicyConversionContexts
		{
			get
			{
				return this.maxPolicyConversionContexts;
			}
			set
			{
				this.maxPolicyConversionContexts = value;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000971BE File Offset: 0x000953BE
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x000971C6 File Offset: 0x000953C6
		internal int MaxPolicyNodes
		{
			get
			{
				return this.maxPolicyNodes;
			}
			set
			{
				this.maxPolicyNodes = value;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000971CF File Offset: 0x000953CF
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x000971D7 File Offset: 0x000953D7
		internal int MaxPolicyAssertions
		{
			get
			{
				return this.maxPolicyAssertions;
			}
			set
			{
				this.maxPolicyAssertions = value;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x000971E0 File Offset: 0x000953E0
		// (set) Token: 0x06002835 RID: 10293 RVA: 0x000971E8 File Offset: 0x000953E8
		internal int MaxYields
		{
			get
			{
				return this.maxYields;
			}
			set
			{
				this.maxYields = value;
			}
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000971F4 File Offset: 0x000953F4
		private static MetadataImporterQuotas CreateDefaultSettings()
		{
			return new MetadataImporterQuotas
			{
				maxPolicyConversionContexts = 32,
				maxPolicyNodes = 4096,
				maxPolicyAssertions = 1024
			};
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x00097228 File Offset: 0x00095428
		private static MetadataImporterQuotas CreateMaxSettings()
		{
			return new MetadataImporterQuotas
			{
				maxPolicyConversionContexts = 32,
				maxPolicyNodes = int.MaxValue,
				maxPolicyAssertions = int.MaxValue
			};
		}

		// Token: 0x04002216 RID: 8726
		private const int DefaultMaxPolicyConversionContexts = 32;

		// Token: 0x04002217 RID: 8727
		private const int DefaultMaxPolicyNodes = 4096;

		// Token: 0x04002218 RID: 8728
		private const int DefaultMaxPolicyAssertions = 1024;

		// Token: 0x04002219 RID: 8729
		private const int DefaultMaxYields = 1024;

		// Token: 0x0400221A RID: 8730
		private int maxPolicyConversionContexts;

		// Token: 0x0400221B RID: 8731
		private int maxPolicyNodes;

		// Token: 0x0400221C RID: 8732
		private int maxPolicyAssertions;

		// Token: 0x0400221D RID: 8733
		private int maxYields;
	}
}
