using System;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001C7 RID: 455
	[ConfigurationCollection(typeof(IdentityConfigurationElement), AddItemName = "identityConfiguration", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class IdentityConfigurationElementCollection : ConfigurationElementCollection
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00002434 File Offset: 0x00000634
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00042FD2 File Offset: 0x000411D2
		protected override ConfigurationElement CreateNewElement()
		{
			return new IdentityConfigurationElement();
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00042FDC File Offset: 0x000411DC
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			IdentityConfigurationElement identityConfigurationElement = element as IdentityConfigurationElement;
			if (identityConfigurationElement == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7013"));
			}
			return identityConfigurationElement.Name;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0004301C File Offset: 0x0004121C
		public IdentityConfigurationElement GetElement(string name)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			IdentityConfigurationElement identityConfigurationElement = base.BaseGet(name) as IdentityConfigurationElement;
			if (!StringComparer.Ordinal.Equals(name, "") && identityConfigurationElement == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7012", new object[]
				{
					name
				}));
			}
			return identityConfigurationElement;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0004307C File Offset: 0x0004127C
		protected override void BaseAdd(ConfigurationElement element)
		{
			string text = this.GetElementKey(element) as string;
			IdentityConfigurationElement identityConfigurationElement = base.BaseGet(text) as IdentityConfigurationElement;
			if (identityConfigurationElement != null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7029", new object[]
				{
					"<identityConfiguation>",
					text
				}));
			}
			base.BaseAdd(element);
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x000430CF File Offset: 0x000412CF
		internal bool IsConfigured
		{
			get
			{
				return base.Count > 0;
			}
		}
	}
}
