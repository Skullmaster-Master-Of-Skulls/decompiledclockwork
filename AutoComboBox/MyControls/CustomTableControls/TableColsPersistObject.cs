using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	public class TableColsPersistObject
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0002F9D0 File Offset: 0x0002E9D0
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0002F9F0 File Offset: 0x0002E9F0
		[XmlElement("Entry")]
		public ColPersistObject[] Cols
		{
			get
			{
				return this._cols.ToArray();
			}
			set
			{
				this._cols.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					ColPersistObject item = value[i];
					this._cols.Add(item);
				}
			}
		}

		// Token: 0x040004A7 RID: 1191
		private List<ColPersistObject> _cols = new List<ColPersistObject>();
	}
}
