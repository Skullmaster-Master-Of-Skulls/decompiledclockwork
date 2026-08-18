using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200009D RID: 157
	public class ColumnDefinition : ICloneable
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x000314DD File Offset: 0x000304DD
		public ColumnDefinition()
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000314E8 File Offset: 0x000304E8
		public ColumnDefinition(string cname, ColumnTypeDef ctype)
		{
			this.ColumnName = cname;
			this.ColumnType = ctype;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00031504 File Offset: 0x00030504
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0003151C File Offset: 0x0003051C
		public string ColumnName
		{
			get
			{
				return this.__colName;
			}
			set
			{
				this.__colName = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00031528 File Offset: 0x00030528
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x00031540 File Offset: 0x00030540
		public ColumnTypeDef ColumnType
		{
			get
			{
				return this.__CTD;
			}
			set
			{
				this.__CTD = value;
				this.__CTDE = ColumnTypeDefUtil.enumOf(value.GetType());
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0003155C File Offset: 0x0003055C
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x000315C4 File Offset: 0x000305C4
		public string XmlDefinition
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<ColumnDefinition>");
				stringBuilder.Append("<ColumnName \"");
				stringBuilder.Append(this.__colName);
				stringBuilder.Append("\" />");
				stringBuilder.Append(this.XmlColDef);
				stringBuilder.Append("</ColumnDefinition>");
				return stringBuilder.ToString();
			}
			set
			{
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000315C8 File Offset: 0x000305C8
		private string XmlColDef
		{
			get
			{
				ColumnTypeDef _CTD = this.__CTD;
				MemoryStream memoryStream = new MemoryStream();
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ColumnTypeDef));
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlSerializer.Serialize(xmlTextWriter, _CTD);
				memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
				return new UTF8Encoding().GetString(memoryStream.ToArray());
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00031638 File Offset: 0x00030638
		public ColumnTypeDefEnum ColumnTypeEnum
		{
			get
			{
				return this.__CTDE;
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00031650 File Offset: 0x00030650
		public override string ToString()
		{
			return this.ColumnName + " (" + ColumnTypeDefUtil.StringRepresentationsOfTypes[(int)this.ColumnTypeEnum] + ")";
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00031684 File Offset: 0x00030684
		public object Clone()
		{
			return new ColumnDefinition(this.ColumnName, this.ColumnType);
		}

		// Token: 0x040004D7 RID: 1239
		private string __colName;

		// Token: 0x040004D8 RID: 1240
		private ColumnTypeDef __CTD;

		// Token: 0x040004D9 RID: 1241
		private ColumnTypeDefEnum __CTDE;
	}
}
