using System;
using System.ComponentModel;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x0200006B RID: 107
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	internal sealed class ResourceDisplayNameAttribute : DisplayNameAttribute
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x00013D19 File Offset: 0x00011F19
		public ResourceDisplayNameAttribute(string displayNameResourceName)
		{
			this._displayNameResourceName = displayNameResourceName;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00013D28 File Offset: 0x00011F28
		public override string DisplayName
		{
			get
			{
				if (!this._resourceLoaded)
				{
					this._resourceLoaded = true;
					base.DisplayNameValue = AtlasWeb.ResourceManager.GetString(this._displayNameResourceName, AtlasWeb.Culture);
				}
				return base.DisplayName;
			}
		}

		// Token: 0x04000170 RID: 368
		private bool _resourceLoaded;

		// Token: 0x04000171 RID: 369
		private readonly string _displayNameResourceName;
	}
}
