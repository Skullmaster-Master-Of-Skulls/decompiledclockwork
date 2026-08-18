using System;
using System.Text;

namespace System.Web.Management
{
	// Token: 0x0200018B RID: 395
	public class WebEventFormatter
	{
		// Token: 0x06001552 RID: 5458 RVA: 0x00041D8C File Offset: 0x0003FF8C
		private void AddTab()
		{
			for (int i = this._level; i > 0; i--)
			{
				this._sb.Append(' ', this._tabSize);
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00041DBE File Offset: 0x0003FFBE
		internal WebEventFormatter()
		{
			this._level = 0;
			this._sb = new StringBuilder();
			this._tabSize = 4;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00041DDF File Offset: 0x0003FFDF
		public void AppendLine(string s)
		{
			this.AddTab();
			this._sb.Append(s);
			this._sb.Append('\n');
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00041E02 File Offset: 0x00040002
		public new string ToString()
		{
			return this._sb.ToString();
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00041E0F File Offset: 0x0004000F
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x00041E17 File Offset: 0x00040017
		public int IndentationLevel
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = Math.Max(value, 0);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00041E26 File Offset: 0x00040026
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x00041E2E File Offset: 0x0004002E
		public int TabSize
		{
			get
			{
				return this._tabSize;
			}
			set
			{
				this._tabSize = Math.Max(value, 0);
			}
		}

		// Token: 0x0400163A RID: 5690
		private int _level;

		// Token: 0x0400163B RID: 5691
		private StringBuilder _sb;

		// Token: 0x0400163C RID: 5692
		private int _tabSize;
	}
}
