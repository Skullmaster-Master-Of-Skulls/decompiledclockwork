using System;
using System.Collections;
using System.Collections.Generic;

namespace TechnoPro.Common.IDisplay
{
	// Token: 0x02000004 RID: 4
	public class DisplayParameters : IEnumerable<string>, IEnumerable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		public IList<string> DisplayPropertyList { get; private set; }

		// Token: 0x06000004 RID: 4 RVA: 0x00002061 File Offset: 0x00000261
		public DisplayParameters()
		{
			this.DisplayPropertyList = new List<string>();
		}

		// Token: 0x17000002 RID: 2
		public string this[int index]
		{
			get
			{
				return this.DisplayPropertyList[index];
			}
			set
			{
				this.DisplayPropertyList[index] = value;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002091 File Offset: 0x00000291
		public void Add(string propName)
		{
			this.DisplayPropertyList.Add(propName);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000209F File Offset: 0x0000029F
		public IEnumerator<string> GetEnumerator()
		{
			return this.DisplayPropertyList.GetEnumerator();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020AC File Offset: 0x000002AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
