using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200014F RID: 335
	internal class NullableMapping : TypeMapping
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x000675C7 File Offset: 0x000657C7
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x000675CF File Offset: 0x000657CF
		internal TypeMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x000675D8 File Offset: 0x000657D8
		internal override string DefaultElementName
		{
			get
			{
				return this.BaseMapping.DefaultElementName;
			}
		}

		// Token: 0x04000ADD RID: 2781
		private TypeMapping baseMapping;
	}
}
