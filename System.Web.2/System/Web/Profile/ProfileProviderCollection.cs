using System;
using System.Configuration;
using System.Configuration.Provider;

namespace System.Web.Profile
{
	// Token: 0x0200016A RID: 362
	public sealed class ProfileProviderCollection : SettingsProviderCollection
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x0003B4D4 File Offset: 0x000396D4
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is ProfileProvider))
			{
				throw new ArgumentException(SR.GetString("Provider_must_implement_type", new object[]
				{
					typeof(ProfileProvider).ToString()
				}), "provider");
			}
			base.Add(provider);
		}

		// Token: 0x17000617 RID: 1559
		public ProfileProvider this[string name]
		{
			get
			{
				return (ProfileProvider)base[name];
			}
		}
	}
}
