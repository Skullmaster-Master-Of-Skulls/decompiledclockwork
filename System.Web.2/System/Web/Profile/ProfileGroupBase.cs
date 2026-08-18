using System;

namespace System.Web.Profile
{
	// Token: 0x0200015D RID: 349
	public class ProfileGroupBase
	{
		// Token: 0x17000601 RID: 1537
		public object this[string propertyName]
		{
			get
			{
				return this._Parent[this._MyName + propertyName];
			}
			set
			{
				this._Parent[this._MyName + propertyName] = value;
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0003A57A File Offset: 0x0003877A
		public object GetPropertyValue(string propertyName)
		{
			return this._Parent[this._MyName + propertyName];
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0003A593 File Offset: 0x00038793
		public void SetPropertyValue(string propertyName, object propertyValue)
		{
			this._Parent[this._MyName + propertyName] = propertyValue;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0003A5AD File Offset: 0x000387AD
		public ProfileGroupBase()
		{
			this._Parent = null;
			this._MyName = null;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0003A5C3 File Offset: 0x000387C3
		public void Init(ProfileBase parent, string myName)
		{
			if (this._Parent == null)
			{
				this._Parent = parent;
				this._MyName = myName + ".";
			}
		}

		// Token: 0x04001501 RID: 5377
		private string _MyName;

		// Token: 0x04001502 RID: 5378
		private ProfileBase _Parent;
	}
}
