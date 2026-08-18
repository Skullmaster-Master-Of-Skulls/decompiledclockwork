using System;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000A7 RID: 167
	[Serializable]
	public class FileNameDef : ColumnTypeDef, ICloneable
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00032598 File Offset: 0x00031598
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x000325B0 File Offset: 0x000315B0
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

		// Token: 0x0600064C RID: 1612 RVA: 0x000325BC File Offset: 0x000315BC
		public object Clone()
		{
			return new FileNameDef
			{
				__value = this.__value
			};
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000325E1 File Offset: 0x000315E1
		public FileNameDef()
		{
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000325EC File Offset: 0x000315EC
		public FileNameDef(string value)
		{
			this.__value = value;
		}

		// Token: 0x040004EE RID: 1262
		private string __value;
	}
}
