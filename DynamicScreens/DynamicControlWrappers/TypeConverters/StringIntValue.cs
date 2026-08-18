using System;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x0200005D RID: 93
	public class StringIntValue
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00040CBA File Offset: 0x0003FCBA
		public StringIntValue(int intValue, string stringValue)
		{
			this.intValue = intValue;
			this.stringValue = stringValue;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00040CD4 File Offset: 0x0003FCD4
		public override string ToString()
		{
			return this.stringValue;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00040CEC File Offset: 0x0003FCEC
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00040D04 File Offset: 0x0003FD04
		public int IntValue
		{
			get
			{
				return this.intValue;
			}
			set
			{
				this.intValue = value;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00040D10 File Offset: 0x0003FD10
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is string)
			{
				result = this.stringValue.Equals((string)obj);
			}
			else
			{
				result = base.Equals(obj);
			}
			return result;
		}

		// Token: 0x04000372 RID: 882
		private string stringValue;

		// Token: 0x04000373 RID: 883
		private int intValue;
	}
}
