using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000048 RID: 72
	internal class SmiDefaultFieldsProperty : SmiMetaDataProperty
	{
		// Token: 0x0600026E RID: 622 RVA: 0x001DF238 File Offset: 0x001DE638
		internal SmiDefaultFieldsProperty(IList<bool> defaultFields)
		{
			this._defaults = new List<bool>(defaultFields).AsReadOnly();
		}

		// Token: 0x17000046 RID: 70
		internal bool this[int ordinal]
		{
			get
			{
				return this._defaults.Count > ordinal && this._defaults[ordinal];
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x001DF298 File Offset: 0x001DE698
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x001DF2A8 File Offset: 0x001DE6A8
		internal override string TraceString()
		{
			string text = "DefaultFields(";
			bool flag = false;
			for (int i = 0; i < this._defaults.Count; i++)
			{
				if (flag)
				{
					text += ",";
				}
				else
				{
					flag = true;
				}
				if (this._defaults[i])
				{
					text += i;
				}
			}
			return text + ")";
		}

		// Token: 0x040005FD RID: 1533
		private IList<bool> _defaults;
	}
}
