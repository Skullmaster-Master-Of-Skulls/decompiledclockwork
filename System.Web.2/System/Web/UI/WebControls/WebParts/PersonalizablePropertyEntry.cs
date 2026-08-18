using System;
using System.Reflection;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000555 RID: 1365
	internal sealed class PersonalizablePropertyEntry
	{
		// Token: 0x06004561 RID: 17761 RVA: 0x000E4E7D File Offset: 0x000E307D
		public PersonalizablePropertyEntry(PropertyInfo pi, PersonalizableAttribute attr)
		{
			this._propertyInfo = pi;
			this._scope = attr.Scope;
			this._isSensitive = attr.IsSensitive;
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x000E4EA4 File Offset: 0x000E30A4
		public bool IsSensitive
		{
			get
			{
				return this._isSensitive;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x000E4EAC File Offset: 0x000E30AC
		public PersonalizationScope Scope
		{
			get
			{
				return this._scope;
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x000E4EB4 File Offset: 0x000E30B4
		public PropertyInfo PropertyInfo
		{
			get
			{
				return this._propertyInfo;
			}
		}

		// Token: 0x04002666 RID: 9830
		private PropertyInfo _propertyInfo;

		// Token: 0x04002667 RID: 9831
		private PersonalizationScope _scope;

		// Token: 0x04002668 RID: 9832
		private bool _isSensitive;
	}
}
