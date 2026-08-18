using System;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000094 RID: 148
	[Serializable]
	public class CellPersistObject
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0002FC00 File Offset: 0x0002EC00
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0002FC18 File Offset: 0x0002EC18
		[XmlAttribute("ColumnID")]
		public int ColumnID
		{
			get
			{
				return this._CID;
			}
			set
			{
				this._CID = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0002FC24 File Offset: 0x0002EC24
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0002FC3C File Offset: 0x0002EC3C
		[XmlElement("Data")]
		public object Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x040004AD RID: 1197
		private int _CID;

		// Token: 0x040004AE RID: 1198
		private object _data;
	}
}
