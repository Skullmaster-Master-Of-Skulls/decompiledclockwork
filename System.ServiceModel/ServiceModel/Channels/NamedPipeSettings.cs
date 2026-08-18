using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000844 RID: 2116
	public sealed class NamedPipeSettings
	{
		// Token: 0x06004F05 RID: 20229 RVA: 0x0011FAAE File Offset: 0x0011DCAE
		internal NamedPipeSettings()
		{
			this.ApplicationContainerSettings = new ApplicationContainerSettings();
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0011FAC1 File Offset: 0x0011DCC1
		private NamedPipeSettings(NamedPipeSettings elementToBeCloned)
		{
			if (elementToBeCloned.ApplicationContainerSettings != null)
			{
				this.ApplicationContainerSettings = elementToBeCloned.ApplicationContainerSettings.Clone();
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x0011FAE2 File Offset: 0x0011DCE2
		// (set) Token: 0x06004F08 RID: 20232 RVA: 0x0011FAEA File Offset: 0x0011DCEA
		public ApplicationContainerSettings ApplicationContainerSettings { get; private set; }

		// Token: 0x06004F09 RID: 20233 RVA: 0x0011FAF3 File Offset: 0x0011DCF3
		internal NamedPipeSettings Clone()
		{
			return new NamedPipeSettings(this);
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x0011FAFB File Offset: 0x0011DCFB
		internal bool IsMatch(NamedPipeSettings pipeSettings)
		{
			return pipeSettings != null && this.ApplicationContainerSettings.IsMatch(pipeSettings.ApplicationContainerSettings);
		}
	}
}
