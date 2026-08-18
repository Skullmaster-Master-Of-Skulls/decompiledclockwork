using System;
using System.ComponentModel;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200008D RID: 141
	internal class ExtenderPropertyRenameDescProv<T> : FilterTypeDescriptionProvider<IComponent> where T : ExtenderControlBase
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0000CC86 File Offset: 0x0000AE86
		public ExtenderPropertyRenameDescProv(ExtenderControlBaseDesigner<T> owner, IComponent target) : base(target)
		{
			this._owner = owner;
			base.FilterExtendedProperties = true;
		}

		// Token: 0x0400029B RID: 667
		private ExtenderControlBaseDesigner<T> _owner;
	}
}
