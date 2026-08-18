using System;
using System.Text;

namespace System.Xml.Linq
{
	// Token: 0x0200001A RID: 26
	[__DynamicallyInvokable]
	public class XText : XNode
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004F2D File Offset: 0x0000312D
		[__DynamicallyInvokable]
		public XText(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.text = value;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004F4A File Offset: 0x0000314A
		[__DynamicallyInvokable]
		public XText(XText other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.text = other.text;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004F6C File Offset: 0x0000316C
		internal XText(XmlReader r)
		{
			this.text = r.Value;
			r.Read();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004F87 File Offset: 0x00003187
		[__DynamicallyInvokable]
		public override XmlNodeType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlNodeType.Text;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004F8A File Offset: 0x0000318A
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004F94 File Offset: 0x00003194
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.text;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.text = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004FD3 File Offset: 0x000031D3
		[__DynamicallyInvokable]
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (this.parent is XDocument)
			{
				writer.WriteWhitespace(this.text);
				return;
			}
			writer.WriteString(this.text);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005009 File Offset: 0x00003209
		internal override void AppendText(StringBuilder sb)
		{
			sb.Append(this.text);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005018 File Offset: 0x00003218
		internal override XNode CloneNode()
		{
			return new XText(this);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005020 File Offset: 0x00003220
		internal override bool DeepEquals(XNode node)
		{
			return node != null && this.NodeType == node.NodeType && this.text == ((XText)node).text;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000504B File Offset: 0x0000324B
		internal override int GetDeepHashCode()
		{
			return this.text.GetHashCode();
		}

		// Token: 0x04000086 RID: 134
		internal string text;
	}
}
