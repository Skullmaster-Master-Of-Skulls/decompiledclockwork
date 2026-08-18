using System;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BBF RID: 7103
	internal class PropertyToken : IMemberAccessToken
	{
		// Token: 0x170053B0 RID: 21424
		// (get) Token: 0x06011295 RID: 70293 RVA: 0x003C8F07 File Offset: 0x003C7107
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x06011296 RID: 70294 RVA: 0x003C8F0F File Offset: 0x003C710F
		public PropertyToken(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x04004CD0 RID: 19664
		private readonly string propertyName;
	}
}
