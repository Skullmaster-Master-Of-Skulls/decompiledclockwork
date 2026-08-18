using System;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005B1 RID: 1457
	public sealed class WebPartUserCapability
	{
		// Token: 0x060049B8 RID: 18872 RVA: 0x000F4E5C File Offset: 0x000F305C
		public WebPartUserCapability(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("name");
			}
			this._name = name;
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x060049B9 RID: 18873 RVA: 0x000F4E7E File Offset: 0x000F307E
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x000F4E88 File Offset: 0x000F3088
		public override bool Equals(object o)
		{
			if (o == this)
			{
				return true;
			}
			WebPartUserCapability webPartUserCapability = o as WebPartUserCapability;
			return webPartUserCapability != null && webPartUserCapability.Name == this.Name;
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x000F4EB8 File Offset: 0x000F30B8
		public override int GetHashCode()
		{
			return this._name.GetHashCode();
		}

		// Token: 0x040027B6 RID: 10166
		private string _name;
	}
}
