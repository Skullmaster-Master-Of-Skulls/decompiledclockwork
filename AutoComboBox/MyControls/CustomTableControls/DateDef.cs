using System;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public class DateDef : ColumnTypeDef, ICloneable
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00032530 File Offset: 0x00031530
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x00032548 File Offset: 0x00031548
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

		// Token: 0x06000647 RID: 1607 RVA: 0x00032554 File Offset: 0x00031554
		public object Clone()
		{
			return new DateDef
			{
				__value = this.__value
			};
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00032579 File Offset: 0x00031579
		public DateDef()
		{
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00032584 File Offset: 0x00031584
		public DateDef(string value)
		{
			this.__value = value;
		}

		// Token: 0x040004ED RID: 1261
		private string __value;
	}
}
