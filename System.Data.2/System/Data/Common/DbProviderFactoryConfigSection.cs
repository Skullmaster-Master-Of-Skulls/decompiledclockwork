using System;

namespace System.Data.Common
{
	// Token: 0x020002F8 RID: 760
	internal class DbProviderFactoryConfigSection
	{
		// Token: 0x06003078 RID: 12408 RVA: 0x0012EAD4 File Offset: 0x0012DED4
		public DbProviderFactoryConfigSection(Type FactoryType, string FactoryName, string FactoryDescription)
		{
			try
			{
				this.factType = FactoryType;
				this.name = FactoryName;
				this.invariantName = this.factType.Namespace.ToString();
				this.description = FactoryDescription;
				this.assemblyQualifiedName = this.factType.AssemblyQualifiedName.ToString();
			}
			catch
			{
				this.factType = null;
				this.name = string.Empty;
				this.invariantName = string.Empty;
				this.description = string.Empty;
				this.assemblyQualifiedName = string.Empty;
			}
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x0012EB7C File Offset: 0x0012DF7C
		public DbProviderFactoryConfigSection(string FactoryName, string FactoryInvariantName, string FactoryDescription, string FactoryAssemblyQualifiedName)
		{
			this.factType = null;
			this.name = FactoryName;
			this.invariantName = FactoryInvariantName;
			this.description = FactoryDescription;
			this.assemblyQualifiedName = FactoryAssemblyQualifiedName;
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x0012EBB4 File Offset: 0x0012DFB4
		public bool IsNull()
		{
			return this.factType == null && this.invariantName == string.Empty;
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x0012EBE4 File Offset: 0x0012DFE4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x0012EBF8 File Offset: 0x0012DFF8
		public string InvariantName
		{
			get
			{
				return this.invariantName;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x0012EC0C File Offset: 0x0012E00C
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x0012EC20 File Offset: 0x0012E020
		public string AssemblyQualifiedName
		{
			get
			{
				return this.assemblyQualifiedName;
			}
		}

		// Token: 0x04001D3D RID: 7485
		private Type factType;

		// Token: 0x04001D3E RID: 7486
		private string name;

		// Token: 0x04001D3F RID: 7487
		private string invariantName;

		// Token: 0x04001D40 RID: 7488
		private string description;

		// Token: 0x04001D41 RID: 7489
		private string assemblyQualifiedName;
	}
}
