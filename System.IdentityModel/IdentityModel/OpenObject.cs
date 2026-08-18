using System;
using System.Collections.Generic;

namespace System.IdentityModel
{
	// Token: 0x02000063 RID: 99
	public abstract class OpenObject
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000BDA7 File Offset: 0x00009FA7
		public Dictionary<string, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x0400033F RID: 831
		private Dictionary<string, object> _properties = new Dictionary<string, object>();
	}
}
