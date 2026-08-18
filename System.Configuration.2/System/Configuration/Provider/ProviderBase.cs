using System;
using System.Collections.Specialized;

namespace System.Configuration.Provider
{
	// Token: 0x020000C1 RID: 193
	public abstract class ProviderBase
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000206C2 File Offset: 0x0001E8C2
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x000206CA File Offset: 0x0001E8CA
		public virtual string Description
		{
			get
			{
				if (!string.IsNullOrEmpty(this._Description))
				{
					return this._Description;
				}
				return this.Name;
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000206E8 File Offset: 0x0001E8E8
		public virtual void Initialize(string name, NameValueCollection config)
		{
			lock (this)
			{
				if (this._Initialized)
				{
					throw new InvalidOperationException(SR.GetString("Provider_Already_Initialized"));
				}
				this._Initialized = true;
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Config_provider_name_null_or_empty"), "name");
			}
			this._name = name;
			if (config != null)
			{
				this._Description = config["description"];
				config.Remove("description");
			}
		}

		// Token: 0x04000466 RID: 1126
		private string _name;

		// Token: 0x04000467 RID: 1127
		private string _Description;

		// Token: 0x04000468 RID: 1128
		private bool _Initialized;
	}
}
