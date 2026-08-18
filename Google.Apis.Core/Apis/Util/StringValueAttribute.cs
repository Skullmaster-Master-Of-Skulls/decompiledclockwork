using System;

namespace Google.Apis.Util
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class StringValueAttribute : Attribute
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000022FE File Offset: 0x000004FE
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002306 File Offset: 0x00000506
		public StringValueAttribute(string text)
		{
			text.ThrowIfNull("text");
			this.text = text;
		}

		// Token: 0x04000013 RID: 19
		private readonly string text;
	}
}
