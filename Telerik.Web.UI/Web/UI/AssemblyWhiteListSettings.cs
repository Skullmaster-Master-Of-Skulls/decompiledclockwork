using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200085B RID: 2139
	public class AssemblyWhiteListSettings : ObjectWithState
	{
		// Token: 0x06004ECD RID: 20173 RVA: 0x000F71A1 File Offset: 0x000F53A1
		public AssemblyWhiteListSettings(string keyPrefix, StateBag ownerViewState) : base(keyPrefix, ownerViewState)
		{
		}

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x000F71AB File Offset: 0x000F53AB
		// (set) Token: 0x06004ECF RID: 20175 RVA: 0x000F71CB File Offset: 0x000F53CB
		public string ProviderName
		{
			get
			{
				return (string)(base.ViewState["ProviderName"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ProviderName"] = value;
			}
		}
	}
}
