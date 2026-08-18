using System;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000180 RID: 384
	internal class DoubleLinkAxis : Axis
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00057250 File Offset: 0x00056250
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x00057258 File Offset: 0x00056258
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

		// Token: 0x0600145F RID: 5215 RVA: 0x00057264 File Offset: 0x00056264
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

		// Token: 0x06001460 RID: 5216 RVA: 0x000572B9 File Offset: 0x000562B9
		internal static DoubleLinkAxis ConvertTree(Axis axis)
		{
			if (axis == null)
			{
				return null;
			}
			return new DoubleLinkAxis(axis, DoubleLinkAxis.ConvertTree((Axis)axis.Input));
		}

		// Token: 0x04000C60 RID: 3168
		internal Axis next;
	}
}
