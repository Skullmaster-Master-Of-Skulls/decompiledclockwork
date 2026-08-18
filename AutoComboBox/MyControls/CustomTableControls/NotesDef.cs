using System;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	public class NotesDef : ColumnTypeDef, ICloneable
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00032460 File Offset: 0x00031460
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00032478 File Offset: 0x00031478
		[XmlElement("value")]
		public string Value
		{
			get
			{
				return this.__value;
			}
			set
			{
				this.__value = value;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00032484 File Offset: 0x00031484
		public object Clone()
		{
			return new NotesDef
			{
				__value = this.__value
			};
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000324A9 File Offset: 0x000314A9
		public NotesDef()
		{
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000324B4 File Offset: 0x000314B4
		public NotesDef(string value)
		{
			this.__value = value;
		}

		// Token: 0x040004EB RID: 1259
		private string __value;
	}
}
