using System;

namespace OracleInternal.TTC
{
	// Token: 0x0200022B RID: 555
	internal class TTCKeywordValuePair
	{
		// Token: 0x0600147D RID: 5245 RVA: 0x000DC284 File Offset: 0x000DA484
		private TTCKeywordValuePair(int _keyword, string _textValue, byte[] _binaryValue)
		{
			this.m_keyword = _keyword;
			this.m_textValueInString = _textValue;
			this.m_binaryValue = _binaryValue;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x000DC2A4 File Offset: 0x000DA4A4
		internal static TTCKeywordValuePair Unmarshal(MarshallingEngine mEngine)
		{
			int[] array = new int[1];
			string textValue = null;
			byte[] array2 = null;
			int num = mEngine.UnmarshalUB2(false);
			if (num != 0)
			{
				byte[] bytes = new byte[num];
				mEngine.UnmarshalCLR(bytes, 0, array);
				textValue = mEngine.m_dbCharSetConv.ConvertBytesToString(bytes, 0, array[0], null, true);
			}
			int num2 = mEngine.UnmarshalUB2(false);
			if (num2 != 0)
			{
				array2 = new byte[num2];
				mEngine.UnmarshalCLR(array2, 0, array);
			}
			int keyword = mEngine.UnmarshalUB2(false);
			return new TTCKeywordValuePair(keyword, textValue, array2);
		}

		// Token: 0x040018AD RID: 6317
		internal int m_keyword;

		// Token: 0x040018AE RID: 6318
		internal byte[] m_binaryValue;

		// Token: 0x040018AF RID: 6319
		internal string m_textValueInString;
	}
}
