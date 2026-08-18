using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public class RowPersistObject
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0002FB88 File Offset: 0x0002EB88
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0002FBA8 File Offset: 0x0002EBA8
		[XmlElement("Unit")]
		public CellPersistObject[] Cells
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
					CellPersistObject item = value[i];
					this._cells.Add(item);
				}
			}
		}

		// Token: 0x040004AC RID: 1196
		private List<CellPersistObject> _cells = new List<CellPersistObject>();
	}
}
