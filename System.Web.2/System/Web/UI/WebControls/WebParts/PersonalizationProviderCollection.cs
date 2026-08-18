using System;
using System.Configuration.Provider;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200055B RID: 1371
	public sealed class PersonalizationProviderCollection : ProviderCollection
	{
		// Token: 0x1700148E RID: 5262
		public PersonalizationProvider this[string name]
		{
			get
			{
				return (PersonalizationProvider)base[name];
			}
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x000E5F54 File Offset: 0x000E4154
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is PersonalizationProvider))
			{
				throw new ArgumentException(SR.GetString("Provider_must_implement_the_interface", new object[]
				{
					provider.GetType().FullName,
					"PersonalizationProvider"
				}));
			}
			base.Add(provider);
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x000E5FAC File Offset: 0x000E41AC
		public void CopyTo(PersonalizationProvider[] array, int index)
		{
			base.CopyTo(array, index);
		}
	}
}
