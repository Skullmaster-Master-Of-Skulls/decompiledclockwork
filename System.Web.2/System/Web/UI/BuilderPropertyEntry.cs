using System;

namespace System.Web.UI
{
	// Token: 0x0200024F RID: 591
	public abstract class BuilderPropertyEntry : PropertyEntry
	{
		// Token: 0x06001B3F RID: 6975 RVA: 0x000552AB File Offset: 0x000534AB
		internal BuilderPropertyEntry()
		{
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x000553F8 File Offset: 0x000535F8
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x00055400 File Offset: 0x00053600
		public ControlBuilder Builder
		{
			get
			{
				return this._builder;
			}
			set
			{
				this._builder = value;
			}
		}

		// Token: 0x04001892 RID: 6290
		private ControlBuilder _builder;
	}
}
