using System;
using System.Text;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000FD RID: 253
	public abstract class XmlCharacterData : XmlLinkedNode
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x00049A1C File Offset: 0x00047C1C
		protected internal XmlCharacterData(string data, XmlDocument doc) : base(doc)
		{
			this.data = data;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00049A2C File Offset: 0x00047C2C
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x00049A34 File Offset: 0x00047C34
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

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00049A3D File Offset: 0x00047C3D
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x00049A45 File Offset: 0x00047C45
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00049A4E File Offset: 0x00047C4E
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x00049A64 File Offset: 0x00047C64
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

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00049AA5 File Offset: 0x00047CA5
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

		// Token: 0x06001188 RID: 4488 RVA: 0x00049ABC File Offset: 0x00047CBC
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

		// Token: 0x06001189 RID: 4489 RVA: 0x00049B04 File Offset: 0x00047D04
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

		// Token: 0x0600118A RID: 4490 RVA: 0x00049B88 File Offset: 0x00047D88
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

		// Token: 0x0600118B RID: 4491 RVA: 0x00049C0C File Offset: 0x00047E0C
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

		// Token: 0x0600118C RID: 4492 RVA: 0x00049C94 File Offset: 0x00047E94
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

		// Token: 0x0600118D RID: 4493 RVA: 0x00049D28 File Offset: 0x00047F28
		internal bool CheckOnData(string data)
		{
			return XmlCharType.Instance.IsOnlyWhitespace(data);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00049D44 File Offset: 0x00047F44
		internal bool DecideXPNodeTypeForTextNodes(XmlNode node, ref XPathNodeType xnt)
		{
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				if (nodeType <= XmlNodeType.EntityReference)
				{
					if (nodeType - XmlNodeType.Text <= 1)
					{
						xnt = XPathNodeType.Text;
						return false;
					}
					if (nodeType != XmlNodeType.EntityReference)
					{
						return false;
					}
					if (!this.DecideXPNodeTypeForTextNodes(node.FirstChild, ref xnt))
					{
						return false;
					}
				}
				else if (nodeType != XmlNodeType.Whitespace)
				{
					if (nodeType != XmlNodeType.SignificantWhitespace)
					{
						return false;
					}
					xnt = XPathNodeType.SignificantWhitespace;
				}
				node = node.NextSibling;
			}
			return true;
		}

		// Token: 0x040004D1 RID: 1233
		private string data;
	}
}
