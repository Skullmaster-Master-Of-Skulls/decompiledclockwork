using System;

namespace System.Xml.Linq
{
	// Token: 0x02000016 RID: 22
	[__DynamicallyInvokable]
	public class XObjectChangeEventArgs : EventArgs
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00004736 File Offset: 0x00002936
		[__DynamicallyInvokable]
		public XObjectChangeEventArgs(XObjectChange objectChange)
		{
			this.objectChange = objectChange;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004745 File Offset: 0x00002945
		[__DynamicallyInvokable]
		public XObjectChange ObjectChange
		{
			[__DynamicallyInvokable]
			get
			{
				return this.objectChange;
			}
		}

		// Token: 0x0400007E RID: 126
		private XObjectChange objectChange;

		// Token: 0x0400007F RID: 127
		[__DynamicallyInvokable]
		public static readonly XObjectChangeEventArgs Add = new XObjectChangeEventArgs(XObjectChange.Add);

		// Token: 0x04000080 RID: 128
		[__DynamicallyInvokable]
		public static readonly XObjectChangeEventArgs Remove = new XObjectChangeEventArgs(XObjectChange.Remove);

		// Token: 0x04000081 RID: 129
		[__DynamicallyInvokable]
		public static readonly XObjectChangeEventArgs Name = new XObjectChangeEventArgs(XObjectChange.Name);

		// Token: 0x04000082 RID: 130
		[__DynamicallyInvokable]
		public static readonly XObjectChangeEventArgs Value = new XObjectChangeEventArgs(XObjectChange.Value);
	}
}
