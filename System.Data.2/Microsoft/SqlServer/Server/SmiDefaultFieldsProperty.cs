using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200007C RID: 124
	internal class SmiDefaultFieldsProperty : SmiMetaDataProperty
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x000482E4 File Offset: 0x000476E4
		internal SmiDefaultFieldsProperty(IList<bool> defaultFields)
		{
			this._defaults = new List<bool>(defaultFields).AsReadOnly();
		}

		// Token: 0x170000BC RID: 188
		internal bool this[int ordinal]
		{
			get
			{
				return this._defaults.Count > ordinal && this._defaults[ordinal];
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00048334 File Offset: 0x00047734
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00048344 File Offset: 0x00047744
		internal override string TraceString()
		{
			string str = "DefaultFields(";
			bool flag = false;
			for (int i = 0; i < this._defaults.Count; i++)
			{
				if (flag)
				{
					str += ",";
				}
				else
				{
					flag = true;
				}
				if (this._defaults[i])
				{
					str += i.ToString();
				}
			}
			return str + ")";
		}

		// Token: 0x0400025F RID: 607
		private IList<bool> _defaults;
	}
}
