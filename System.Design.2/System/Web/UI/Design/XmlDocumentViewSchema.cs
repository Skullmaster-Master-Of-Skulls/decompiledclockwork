using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.Design
{
	// Token: 0x0200008D RID: 141
	internal sealed class XmlDocumentViewSchema : IDataSourceViewSchema
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x00013DB0 File Offset: 0x00011FB0
		public XmlDocumentViewSchema(string name, Pair data, bool includeSpecialSchema)
		{
			this._includeSpecialSchema = includeSpecialSchema;
			this._children = (OrderedDictionary)data.First;
			this._attrs = (ArrayList)data.Second;
			this._name = name;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00013DE8 File Offset: 0x00011FE8
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00013DF0 File Offset: 0x00011FF0
		public IDataSourceViewSchema[] GetChildren()
		{
			if (this._viewSchemas == null)
			{
				this._viewSchemas = new IDataSourceViewSchema[this._children.Count];
				int num = 0;
				foreach (object obj in this._children)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this._viewSchemas[num] = new XmlDocumentViewSchema((string)dictionaryEntry.Key, (Pair)dictionaryEntry.Value, this._includeSpecialSchema);
					num++;
				}
			}
			return this._viewSchemas;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00013E98 File Offset: 0x00012098
		public IDataSourceFieldSchema[] GetFields()
		{
			if (this._fieldSchemas == null)
			{
				int num = this._includeSpecialSchema ? 3 : 0;
				this._fieldSchemas = new IDataSourceFieldSchema[this._attrs.Count + num];
				if (this._includeSpecialSchema)
				{
					this._fieldSchemas[0] = new XmlDocumentFieldSchema("#Name");
					this._fieldSchemas[1] = new XmlDocumentFieldSchema("#Value");
					this._fieldSchemas[2] = new XmlDocumentFieldSchema("#InnerText");
				}
				for (int i = 0; i < this._attrs.Count; i++)
				{
					this._fieldSchemas[i + num] = new XmlDocumentFieldSchema((string)this._attrs[i]);
				}
			}
			return this._fieldSchemas;
		}

		// Token: 0x040001BF RID: 447
		private string _name;

		// Token: 0x040001C0 RID: 448
		private OrderedDictionary _children;

		// Token: 0x040001C1 RID: 449
		private ArrayList _attrs;

		// Token: 0x040001C2 RID: 450
		private IDataSourceViewSchema[] _viewSchemas;

		// Token: 0x040001C3 RID: 451
		private IDataSourceFieldSchema[] _fieldSchemas;

		// Token: 0x040001C4 RID: 452
		private bool _includeSpecialSchema;
	}
}
