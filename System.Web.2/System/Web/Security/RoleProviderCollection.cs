using System;
using System.Configuration.Provider;

namespace System.Web.Security
{
	// Token: 0x020005F6 RID: 1526
	public sealed class RoleProviderCollection : ProviderCollection
	{
		// Token: 0x06004D11 RID: 19729 RVA: 0x001084D8 File Offset: 0x001066D8
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is RoleProvider))
			{
				throw new ArgumentException(SR.GetString("Provider_must_implement_type", new object[]
				{
					typeof(RoleProvider).ToString()
				}), "provider");
			}
			base.Add(provider);
		}

		// Token: 0x170016B1 RID: 5809
		public RoleProvider this[string name]
		{
			get
			{
				return (RoleProvider)base[name];
			}
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x00108540 File Offset: 0x00106740
		public void CopyTo(RoleProvider[] array, int index)
		{
			base.CopyTo(array, index);
		}
	}
}
