using System;
using System.ComponentModel;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x0200006A RID: 106
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Event, Inherited = true, AllowMultiple = false)]
	internal sealed class ResourceDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public ResourceDescriptionAttribute(string descriptionResourceName)
		{
			this._descriptionResourceName = descriptionResourceName;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00013CE7 File Offset: 0x00011EE7
		public override string Description
		{
			get
			{
				if (!this._resourceLoaded)
				{
					this._resourceLoaded = true;
					base.DescriptionValue = AtlasWeb.ResourceManager.GetString(this._descriptionResourceName, AtlasWeb.Culture);
				}
				return base.Description;
			}
		}

		// Token: 0x0400016E RID: 366
		private bool _resourceLoaded;

		// Token: 0x0400016F RID: 367
		private readonly string _descriptionResourceName;
	}
}
