using System;
using System.Text;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000CE RID: 206
	public abstract class XmlCharacterData : XmlLinkedNode
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x00037AF5 File Offset: 0x00036AF5
		protected internal XmlCharacterData(string data, XmlDocument doc) : base(doc)
		{
			this.data = data;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00037B05 File Offset: 0x00036B05
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00037B0D File Offset: 0x00036B0D
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00037B16 File Offset: 0x00036B16
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x00037B1E File Offset: 0x00036B1E
		public override string InnerText
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00037B27 File Offset: 0x00036B27
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00037B40 File Offset: 0x00036B40
		public virtual string Data
		{
			get
			{
				if (this.data != null)
				{
					return this.data;
				}
				return string.Empty;
			}
			set
			{
				XmlNode parentNode = this.ParentNode;
				XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, value, XmlNodeChangedAction.Change);
				if (eventArgs != null)
				{
					this.BeforeEvent(eventArgs);
				}
				this.data = value;
				if (eventArgs != null)
				{
					this.AfterEvent(eventArgs);
				}
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00037B81 File Offset: 0x00036B81
		public virtual int Length
		{
			get
			{
				if (this.data != null)
				{
					return this.data.Length;
				}
				return 0;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00037B98 File Offset: 0x00036B98
		public virtual string Substring(int offset, int count)
		{
			int num = (this.data != null) ? this.data.Length : 0;
			if (num > 0)
			{
				if (num < offset + count)
				{
					count = num - offset;
				}
				return this.data.Substring(offset, count);
			}
			return string.Empty;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00037BE0 File Offset: 0x00036BE0
		public virtual void AppendData(string strData)
		{
			XmlNode parentNode = this.ParentNode;
			int num = (this.data != null) ? this.data.Length : 0;
			if (strData != null)
			{
				num += strData.Length;
			}
			string newValue = new StringBuilder(num).Append(this.data).Append(strData).ToString();
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, newValue, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = newValue;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00037C64 File Offset: 0x00036C64
		public virtual void InsertData(int offset, string strData)
		{
			XmlNode parentNode = this.ParentNode;
			int num = (this.data != null) ? this.data.Length : 0;
			if (strData != null)
			{
				num += strData.Length;
			}
			string newValue = new StringBuilder(num).Append(this.data).Insert(offset, strData).ToString();
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, newValue, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = newValue;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00037CE8 File Offset: 0x00036CE8
		public virtual void DeleteData(int offset, int count)
		{
			int num = (this.data != null) ? this.data.Length : 0;
			if (num > 0 && num < offset + count)
			{
				count = Math.Max(num - offset, 0);
			}
			string newValue = new StringBuilder(this.data).Remove(offset, count).ToString();
			XmlNode parentNode = this.ParentNode;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, newValue, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = newValue;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00037D70 File Offset: 0x00036D70
		public virtual void ReplaceData(int offset, int count, string strData)
		{
			int num = (this.data != null) ? this.data.Length : 0;
			if (num > 0 && num < offset + count)
			{
				count = Math.Max(num - offset, 0);
			}
			StringBuilder stringBuilder = new StringBuilder(this.data).Remove(offset, count);
			string newValue = stringBuilder.Insert(offset, strData).ToString();
			XmlNode parentNode = this.ParentNode;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, newValue, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = newValue;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00037E04 File Offset: 0x00036E04
		internal bool CheckOnData(string data)
		{
			return XmlCharType.Instance.IsOnlyWhitespace(data);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00037E20 File Offset: 0x00036E20
		internal bool DecideXPNodeTypeForTextNodes(XmlNode node, ref XPathNodeType xnt)
		{
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					xnt = XPathNodeType.Text;
					return false;
				case XmlNodeType.EntityReference:
					if (!this.DecideXPNodeTypeForTextNodes(node.FirstChild, ref xnt))
					{
						return false;
					}
					break;
				default:
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
						break;
					case XmlNodeType.SignificantWhitespace:
						xnt = XPathNodeType.SignificantWhitespace;
						break;
					default:
						return false;
					}
					break;
				}
				node = node.NextSibling;
			}
			return true;
		}

		// Token: 0x040008F2 RID: 2290
		private string data;
	}
}
