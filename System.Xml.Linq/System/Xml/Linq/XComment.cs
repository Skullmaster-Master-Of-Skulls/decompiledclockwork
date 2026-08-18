using System;

namespace System.Xml.Linq
{
	// Token: 0x02000026 RID: 38
	[__DynamicallyInvokable]
	public class XComment : XNode
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00008586 File Offset: 0x00006786
		[__DynamicallyInvokable]
		public XComment(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.value = value;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000085A3 File Offset: 0x000067A3
		[__DynamicallyInvokable]
		public XComment(XComment other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.value = other.value;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000085C5 File Offset: 0x000067C5
		internal XComment(XmlReader r)
		{
			this.value = r.Value;
			r.Read();
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000085E0 File Offset: 0x000067E0
		[__DynamicallyInvokable]
		public override XmlNodeType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlNodeType.Comment;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000085E3 File Offset: 0x000067E3
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000085EC File Offset: 0x000067EC
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.value = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000862B File Offset: 0x0000682B
		[__DynamicallyInvokable]
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteComment(this.value);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008647 File Offset: 0x00006847
		internal override XNode CloneNode()
		{
			return new XComment(this);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008650 File Offset: 0x00006850
		internal override bool DeepEquals(XNode node)
		{
			XComment xcomment = node as XComment;
			return xcomment != null && this.value == xcomment.value;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000867A File Offset: 0x0000687A
		internal override int GetDeepHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x040000A2 RID: 162
		internal string value;
	}
}
