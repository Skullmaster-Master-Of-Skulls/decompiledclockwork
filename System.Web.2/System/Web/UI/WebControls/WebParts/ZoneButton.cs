using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005BC RID: 1468
	[SupportsEventValidation]
	internal sealed class ZoneButton : Button
	{
		// Token: 0x06004A94 RID: 19092 RVA: 0x000F7E67 File Offset: 0x000F6067
		public ZoneButton(WebZone owner, string eventArgument)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			this._eventArgument = eventArgument;
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06004A95 RID: 19093 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06004A96 RID: 19094 RVA: 0x0004DBD4 File Offset: 0x0004BDD4
		[DefaultValue(false)]
		public override bool UseSubmitBehavior
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x000F7E8C File Offset: 0x000F608C
		protected override PostBackOptions GetPostBackOptions()
		{
			if (!string.IsNullOrEmpty(this._eventArgument) && this._owner.Page != null)
			{
				return new PostBackOptions(this._owner, this._eventArgument)
				{
					ClientSubmit = true
				};
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x04002817 RID: 10263
		private WebZone _owner;

		// Token: 0x04002818 RID: 10264
		private string _eventArgument;
	}
}
