using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000090 RID: 144
	[Serializable]
	public class ColPersistObject
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0002FA48 File Offset: 0x0002EA48
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0002FA68 File Offset: 0x0002EA68
		[XmlElement("Unit")]
		public ColCellPersistObject[] Cells
		{
			get
			{
				return this._cells.ToArray();
			}
			set
			{
				this._cells.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					ColCellPersistObject item = value[i];
					this._cells.Add(item);
				}
			}
		}

		// Token: 0x040004A8 RID: 1192
		private List<ColCellPersistObject> _cells = new List<ColCellPersistObject>();
	}
}
