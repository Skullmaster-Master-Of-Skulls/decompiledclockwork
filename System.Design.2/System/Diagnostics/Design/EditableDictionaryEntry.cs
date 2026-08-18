using System;

namespace System.Diagnostics.Design
{
	// Token: 0x0200020C RID: 524
	internal class EditableDictionaryEntry
	{
		// Token: 0x06001375 RID: 4981 RVA: 0x0006F8F4 File Offset: 0x0006DAF4
		public EditableDictionaryEntry(string name, string value)
		{
			this._name = name;
			this._value = value;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x0006F90A File Offset: 0x0006DB0A
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x0006F912 File Offset: 0x0006DB12
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

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0006F91B File Offset: 0x0006DB1B
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x0006F923 File Offset: 0x0006DB23
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04000A7C RID: 2684
		public string _name;

		// Token: 0x04000A7D RID: 2685
		public string _value;
	}
}
