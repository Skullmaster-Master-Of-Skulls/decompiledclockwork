using System;
using System.Collections;
using System.Data;
using System.IO;

namespace AutoComboBox.MyControls.MultiLineTextBox
{
	// Token: 0x020000DE RID: 222
	public class MultiLineItemCollection : CollectionBase
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x0004328C File Offset: 0x0004228C
		public MultiLineItemCollection()
		{
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00043298 File Offset: 0x00042298
		public MultiLineItemCollection(string xml)
		{
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(new StringReader(xml), XmlReadMode.ReadSchema);
			if (dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = dataRow["text"].ToString();
					string whoEntered = dataRow["whoentered"].ToString();
					string s = dataRow["dateentered"].ToString();
					DateTime dateEntered;
					try
					{
						dateEntered = DateTime.Parse(s);
					}
					catch
					{
						dateEntered = DateTime.MinValue;
					}
					base.List.Add(new MultiLineItem(text, whoEntered, dateEntered));
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000433BC File Offset: 0x000423BC
		public int Add(MultiLineItem item)
		{
			return base.List.Add(item);
		}

		// Token: 0x170001C1 RID: 449
		public MultiLineItem this[int index]
		{
			get
			{
				return (MultiLineItem)base.List[index];
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000433FF File Offset: 0x000423FF
		public void Sort(IComparer Comparer)
		{
			base.InnerList.Sort(Comparer);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00043410 File Offset: 0x00042410
		public void Remove(MultiLineItem item)
		{
			int num = base.List.IndexOf(item);
			if (num >= 0 && num < base.List.Count)
			{
				base.List.RemoveAt(num);
			}
		}
	}
}
