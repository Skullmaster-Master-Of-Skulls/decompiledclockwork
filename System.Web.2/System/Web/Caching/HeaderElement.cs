using System;

namespace System.Web.Caching
{
	// Token: 0x02000883 RID: 2179
	[Serializable]
	public sealed class HeaderElement
	{
		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x0600669A RID: 26266 RVA: 0x0016985E File Offset: 0x00167A5E
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x0600669B RID: 26267 RVA: 0x00169866 File Offset: 0x00167A66
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600669C RID: 26268 RVA: 0x000030B5 File Offset: 0x000012B5
		private HeaderElement()
		{
		}

		// Token: 0x0600669D RID: 26269 RVA: 0x0016986E File Offset: 0x00167A6E
		public HeaderElement(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._name = name;
			this._value = value;
		}

		// Token: 0x040034EB RID: 13547
		private string _name;

		// Token: 0x040034EC RID: 13548
		private string _value;
	}
}
