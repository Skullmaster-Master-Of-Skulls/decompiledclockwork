using System;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000003 RID: 3
	public class AccordionCommandEventArgs : CommandEventArgs
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002DA4 File Offset: 0x00000FA4
		internal AccordionCommandEventArgs(AccordionContentPanel container, string commandName, object commandArg) : base(commandName, commandArg)
		{
			this._container = container;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002DB5 File Offset: 0x00000FB5
		public AccordionContentPanel Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x04000014 RID: 20
		private AccordionContentPanel _container;
	}
}
