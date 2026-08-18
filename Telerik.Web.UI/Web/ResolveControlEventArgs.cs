using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web
{
	// Token: 0x02000F68 RID: 3944
	public class ResolveControlEventArgs : EventArgs
	{
		// Token: 0x06009624 RID: 38436 RVA: 0x0021886B File Offset: 0x00216A6B
		public ResolveControlEventArgs(string controlId)
		{
			this._controlID = controlId;
		}

		// Token: 0x17002F6C RID: 12140
		// (get) Token: 0x06009625 RID: 38437 RVA: 0x0021887A File Offset: 0x00216A7A
		[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
		public string ControlID
		{
			get
			{
				return this._controlID;
			}
		}

		// Token: 0x17002F6D RID: 12141
		// (get) Token: 0x06009626 RID: 38438 RVA: 0x00218882 File Offset: 0x00216A82
		// (set) Token: 0x06009627 RID: 38439 RVA: 0x0021888A File Offset: 0x00216A8A
		public Control Control
		{
			get
			{
				return this._control;
			}
			set
			{
				this._control = value;
			}
		}

		// Token: 0x04002AFC RID: 11004
		private readonly string _controlID;

		// Token: 0x04002AFD RID: 11005
		private Control _control;
	}
}
