using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000092 RID: 146
	[Serializable]
	public class TablePersistObject
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0002FB10 File Offset: 0x0002EB10
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0002FB30 File Offset: 0x0002EB30
		[XmlElement("Entry")]
		public RowPersistObject[] Rows
		{
			get
			{
				return this._rows.ToArray();
			}
			set
			{
				this._rows.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					RowPersistObject item = value[i];
					this._rows.Add(item);
				}
			}
		}

		// Token: 0x040004AB RID: 1195
		private List<RowPersistObject> _rows = new List<RowPersistObject>();
	}
}
