using System;

namespace System.Web.UI
{
	// Token: 0x02000286 RID: 646
	public class EventEntry
	{
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00061D73 File Offset: 0x0005FF73
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x00061D7B File Offset: 0x0005FF7B
		public string HandlerMethodName
		{
			get
			{
				return this._handlerMethodName;
			}
			set
			{
				this._handlerMethodName = value;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x00061D84 File Offset: 0x0005FF84
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x00061D8C File Offset: 0x0005FF8C
		public Type HandlerType
		{
			get
			{
				return this._handlerType;
			}
			set
			{
				this._handlerType = value;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x00061D95 File Offset: 0x0005FF95
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x00061D9D File Offset: 0x0005FF9D
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x04001995 RID: 6549
		private Type _handlerType;

		// Token: 0x04001996 RID: 6550
		private string _handlerMethodName;

		// Token: 0x04001997 RID: 6551
		private string _name;
	}
}
