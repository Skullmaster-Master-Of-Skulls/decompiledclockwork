using System;
using System.Collections;

namespace System.Xml.Linq
{
	// Token: 0x0200001D RID: 29
	internal struct Inserter
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00006389 File Offset: 0x00004589
		public Inserter(XContainer parent, XNode anchor)
		{
			this.parent = parent;
			this.previous = anchor;
			this.text = null;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000063A0 File Offset: 0x000045A0
		public void Add(object content)
		{
			this.AddContent(content);
			if (this.text != null)
			{
				if (this.parent.content == null)
				{
					if (this.parent.SkipNotify())
					{
						this.parent.content = this.text;
						return;
					}
					if (this.text.Length > 0)
					{
						this.InsertNode(new XText(this.text));
						return;
					}
					if (!(this.parent is XElement))
					{
						this.parent.content = this.text;
						return;
					}
					this.parent.NotifyChanging(this.parent, XObjectChangeEventArgs.Value);
					if (this.parent.content != null)
					{
						throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
					}
					this.parent.content = this.text;
					this.parent.NotifyChanged(this.parent, XObjectChangeEventArgs.Value);
					return;
				}
				else if (this.text.Length > 0)
				{
					if (this.previous is XText && !(this.previous is XCData))
					{
						XText xtext = (XText)this.previous;
						xtext.Value += this.text;
						return;
					}
					this.parent.ConvertTextToNode();
					this.InsertNode(new XText(this.text));
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000064F0 File Offset: 0x000046F0
		private void AddContent(object content)
		{
			if (content == null)
			{
				return;
			}
			XNode xnode = content as XNode;
			if (xnode != null)
			{
				this.AddNode(xnode);
				return;
			}
			string text = content as string;
			if (text != null)
			{
				this.AddString(text);
				return;
			}
			XStreamingElement xstreamingElement = content as XStreamingElement;
			if (xstreamingElement != null)
			{
				this.AddNode(new XElement(xstreamingElement));
				return;
			}
			object[] array = content as object[];
			if (array != null)
			{
				foreach (object content2 in array)
				{
					this.AddContent(content2);
				}
				return;
			}
			IEnumerable enumerable = content as IEnumerable;
			if (enumerable != null)
			{
				foreach (object content3 in enumerable)
				{
					this.AddContent(content3);
				}
				return;
			}
			if (content is XAttribute)
			{
				throw new ArgumentException(Res.GetString("Argument_AddAttribute"));
			}
			this.AddString(XContainer.GetStringValue(content));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000065EC File Offset: 0x000047EC
		private void AddNode(XNode n)
		{
			this.parent.ValidateNode(n, this.previous);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode xnode = this.parent;
				while (xnode.parent != null)
				{
					xnode = xnode.parent;
				}
				if (n == xnode)
				{
					n = n.CloneNode();
				}
			}
			this.parent.ConvertTextToNode();
			if (this.text != null)
			{
				if (this.text.Length > 0)
				{
					if (this.previous is XText && !(this.previous is XCData))
					{
						XText xtext = (XText)this.previous;
						xtext.Value += this.text;
					}
					else
					{
						this.InsertNode(new XText(this.text));
					}
				}
				this.text = null;
			}
			this.InsertNode(n);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000066BE File Offset: 0x000048BE
		private void AddString(string s)
		{
			this.parent.ValidateString(s);
			this.text += s;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000066E0 File Offset: 0x000048E0
		private void InsertNode(XNode n)
		{
			bool flag = this.parent.NotifyChanging(n, XObjectChangeEventArgs.Add);
			if (n.parent != null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
			}
			n.parent = this.parent;
			if (this.parent.content == null || this.parent.content is string)
			{
				n.next = n;
				this.parent.content = n;
			}
			else if (this.previous == null)
			{
				XNode xnode = (XNode)this.parent.content;
				n.next = xnode.next;
				xnode.next = n;
			}
			else
			{
				n.next = this.previous.next;
				this.previous.next = n;
				if (this.parent.content == this.previous)
				{
					this.parent.content = n;
				}
			}
			this.previous = n;
			if (flag)
			{
				this.parent.NotifyChanged(n, XObjectChangeEventArgs.Add);
			}
		}

		// Token: 0x04000088 RID: 136
		private XContainer parent;

		// Token: 0x04000089 RID: 137
		private XNode previous;

		// Token: 0x0400008A RID: 138
		private string text;
	}
}
