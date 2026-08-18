using System;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	public class ColCellPersistObject
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0002FAC0 File Offset: 0x0002EAC0
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0002FAD8 File Offset: 0x0002EAD8
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0002FAE4 File Offset: 0x0002EAE4
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0002FAFC File Offset: 0x0002EAFC
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

		// Token: 0x040004A9 RID: 1193
		private int _CID;

		// Token: 0x040004AA RID: 1194
		private object _data;
	}
}
