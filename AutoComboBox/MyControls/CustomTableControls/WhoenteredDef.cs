using System;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	public class WhoenteredDef : ColumnTypeDef, ICloneable
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x000324C8 File Offset: 0x000314C8
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x000324E0 File Offset: 0x000314E0
		[XmlElement("ref")]
		public string Reference
		{
			get
			{
				return this.__reference;
			}
			set
			{
				this.__reference = value;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000324EC File Offset: 0x000314EC
		public object Clone()
		{
			return new WhoenteredDef
			{
				__reference = this.__reference
			};
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00032511 File Offset: 0x00031511
		public WhoenteredDef()
		{
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0003251C File Offset: 0x0003151C
		public WhoenteredDef(string reference)
		{
			this.__reference = reference;
		}

		// Token: 0x040004EC RID: 1260
		private string __reference;
	}
}
