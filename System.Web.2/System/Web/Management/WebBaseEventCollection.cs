using System;
using System.Collections;

namespace System.Web.Management
{
	// Token: 0x020001A0 RID: 416
	public sealed class WebBaseEventCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x00043A28 File Offset: 0x00041C28
		public WebBaseEventCollection(ICollection events)
		{
			if (events == null)
			{
				throw new ArgumentNullException("events");
			}
			foreach (object obj in events)
			{
				WebBaseEvent value = (WebBaseEvent)obj;
				base.InnerList.Add(value);
			}
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00043A98 File Offset: 0x00041C98
		internal WebBaseEventCollection(WebBaseEvent eventRaised)
		{
			if (eventRaised == null)
			{
				throw new ArgumentNullException("eventRaised");
			}
			base.InnerList.Add(eventRaised);
		}

		// Token: 0x1700067F RID: 1663
		public WebBaseEvent this[int index]
		{
			get
			{
				return (WebBaseEvent)base.InnerList[index];
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(WebBaseEvent value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(WebBaseEvent value)
		{
			return base.InnerList.Contains(value);
		}
	}
}
