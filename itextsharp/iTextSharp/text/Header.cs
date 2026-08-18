using System;
using System.Text;

namespace iTextSharp.text
{
	// Token: 0x020004AB RID: 1195
	public class Header : Meta
	{
		// Token: 0x0600286C RID: 10348 RVA: 0x000F60D8 File Offset: 0x000F50D8
		public Header(string name, string content) : base(0, content)
		{
			this.name = new StringBuilder(name);
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x000F60EE File Offset: 0x000F50EE
		public override string Name
		{
			get
			{
				return this.name.ToString();
			}
		}

		// Token: 0x04001CA8 RID: 7336
		private StringBuilder name;
	}
}
