using System;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020001DA RID: 474
	internal class DoubleLinkAxis : Axis
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x000AB4C4 File Offset: 0x000A96C4
		// (set) Token: 0x06001FAC RID: 8108 RVA: 0x000AB4CC File Offset: 0x000A96CC
		internal Axis Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000AB4D8 File Offset: 0x000A96D8
		internal DoubleLinkAxis(Axis axis, DoubleLinkAxis inputaxis) : base(axis.TypeOfAxis, inputaxis, axis.Prefix, axis.Name, axis.NodeType)
		{
			this.next = null;
			base.Urn = axis.Urn;
			this.abbrAxis = axis.AbbrAxis;
			if (inputaxis != null)
			{
				inputaxis.Next = this;
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000AB52D File Offset: 0x000A972D
		internal static DoubleLinkAxis ConvertTree(Axis axis)
		{
			if (axis == null)
			{
				return null;
			}
			return new DoubleLinkAxis(axis, DoubleLinkAxis.ConvertTree((Axis)axis.Input));
		}

		// Token: 0x04000D5A RID: 3418
		internal Axis next;
	}
}
