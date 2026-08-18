using System;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001D0 RID: 464
	[ConfigurationCollection(typeof(SecurityTokenHandlerElementCollection), AddItemName = "securityTokenHandlers", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class SecurityTokenHandlerSetElementCollection : ConfigurationElementCollection
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00002434 File Offset: 0x00000634
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00043E6F File Offset: 0x0004206F
		protected override ConfigurationElement CreateNewElement()
		{
			return new SecurityTokenHandlerElementCollection();
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00043E76 File Offset: 0x00042076
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SecurityTokenHandlerElementCollection)element).Name;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00043E84 File Offset: 0x00042084
		protected override void BaseAdd(ConfigurationElement element)
		{
			string text = this.GetElementKey(element) as string;
			SecurityTokenHandlerElementCollection securityTokenHandlerElementCollection = base.BaseGet(text) as SecurityTokenHandlerElementCollection;
			if (securityTokenHandlerElementCollection != null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7029", new object[]
				{
					"<securityTokenHandlers>",
					text
				}));
			}
			base.BaseAdd(element);
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x000430CF File Offset: 0x000412CF
		public bool IsConfigured
		{
			get
			{
				return base.Count > 0;
			}
		}
	}
}
