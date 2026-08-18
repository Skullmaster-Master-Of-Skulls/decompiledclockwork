using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200191B RID: 6427
	internal class DefaultEmptyMessageTemplate : ITemplate
	{
		// Token: 0x0600F970 RID: 63856 RVA: 0x00385001 File Offset: 0x00383201
		public DefaultEmptyMessageTemplate(string message)
		{
			this._message = message;
		}

		// Token: 0x0600F971 RID: 63857 RVA: 0x00385010 File Offset: 0x00383210
		public void InstantiateIn(Control container)
		{
			container.Controls.Add(new LiteralControl(this._message));
		}

		// Token: 0x040046E6 RID: 18150
		private string _message;
	}
}
