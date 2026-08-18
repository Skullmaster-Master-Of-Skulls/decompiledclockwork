using System;
using System.Collections.Generic;

namespace BarcodeLib
{
	// Token: 0x02000007 RID: 7
	internal abstract class BarcodeCommon
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003A7E File Offset: 0x00001C7E
		public string RawData
		{
			get
			{
				return this.Raw_Data;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003A86 File Offset: 0x00001C86
		public List<string> Errors
		{
			get
			{
				return this._Errors;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003A8E File Offset: 0x00001C8E
		public void Error(string ErrorMessage)
		{
			this._Errors.Add(ErrorMessage);
			throw new Exception(ErrorMessage);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003AA4 File Offset: 0x00001CA4
		internal static bool CheckNumericOnly(string Data)
		{
			long num = 0L;
			if (Data == null)
			{
				return false;
			}
			if (long.TryParse(Data, out num))
			{
				return true;
			}
			int num2 = 18;
			string text = Data;
			string[] array = new string[Data.Length / num2 + ((Data.Length % num2 == 0) ? 0 : 1)];
			int i = 0;
			while (i < array.Length)
			{
				if (text.Length >= num2)
				{
					array[i++] = text.Substring(0, num2);
					text = text.Substring(num2);
				}
				else
				{
					array[i++] = text.Substring(0);
				}
			}
			string[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				if (!long.TryParse(array2[j], out num))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400004E RID: 78
		protected string Raw_Data = "";

		// Token: 0x0400004F RID: 79
		protected List<string> _Errors = new List<string>();
	}
}
