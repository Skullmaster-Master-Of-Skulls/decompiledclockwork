using System;

namespace System.Xml
{
	// Token: 0x02000382 RID: 898
	internal abstract class BaseTreeIterator
	{
		// Token: 0x06002F6C RID: 12140 RVA: 0x002D48B8 File Offset: 0x002D3CB8
		internal BaseTreeIterator(DataSetMapper mapper)
		{
			this.mapper = mapper;
		}

		// Token: 0x06002F6D RID: 12141
		internal abstract void Reset();

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002F6E RID: 12142
		internal abstract XmlNode CurrentNode { get; }

		// Token: 0x06002F6F RID: 12143
		internal abstract bool Next();

		// Token: 0x06002F70 RID: 12144
		internal abstract bool NextRight();

		// Token: 0x06002F71 RID: 12145 RVA: 0x002D48D8 File Offset: 0x002D3CD8
		internal bool NextRowElement()
		{
			while (this.Next())
			{
				if (this.OnRowElement())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x002D4908 File Offset: 0x002D3D08
		internal bool NextRightRowElement()
		{
			return this.NextRight() && (this.OnRowElement() || this.NextRowElement());
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x002D4938 File Offset: 0x002D3D38
		internal bool OnRowElement()
		{
			XmlBoundElement xmlBoundElement = this.CurrentNode as XmlBoundElement;
			return xmlBoundElement != null && xmlBoundElement.Row != null;
		}

		// Token: 0x04001D98 RID: 7576
		protected DataSetMapper mapper;
	}
}
