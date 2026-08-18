using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200009E RID: 158
	public class TableProperty
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000316A8 File Offset: 0x000306A8
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x000316C8 File Offset: 0x000306C8
		public ColumnDefinition[] ColumnDefinitions
		{
			get
			{
				return this.__CInfos.ToArray();
			}
			set
			{
				this.__CInfos.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					ColumnDefinition item = value[i];
					this.__CInfos.Add(item);
				}
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0003170C File Offset: 0x0003070C
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0003176D File Offset: 0x0003076D
		public string XmlDefinition
		{
			get
			{
				MemoryStream memoryStream = new MemoryStream();
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(TableProperty));
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlSerializer.Serialize(xmlTextWriter, this);
				memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
				return new UTF8Encoding().GetString(memoryStream.ToArray());
			}
			set
			{
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00031770 File Offset: 0x00030770
		public void Add(ColumnDefinition col)
		{
			this.__CInfos.Add(col);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00031780 File Offset: 0x00030780
		public void Remove(ColumnDefinition col)
		{
			this.__CInfos.Remove(col);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00031790 File Offset: 0x00030790
		public void AddRange(IEnumerable<ColumnDefinition> cols)
		{
			this.__CInfos.AddRange(cols);
		}

		// Token: 0x040004DA RID: 1242
		private List<ColumnDefinition> __CInfos = new List<ColumnDefinition>();
	}
}
