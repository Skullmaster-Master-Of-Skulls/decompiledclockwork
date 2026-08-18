using System;

namespace ReportFunctions
{
	// Token: 0x0200001A RID: 26
	public class StringInt
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00038820 File Offset: 0x00037820
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00038838 File Offset: 0x00037838
		public string S
		{
			get
			{
				return this.s;
			}
			set
			{
				this.s = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00038844 File Offset: 0x00037844
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0003885C File Offset: 0x0003785C
		public int Int1
		{
			get
			{
				return this.int1;
			}
			set
			{
				this.int1 = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00038868 File Offset: 0x00037868
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00038880 File Offset: 0x00037880
		public int Int2
		{
			get
			{
				return this.int2;
			}
			set
			{
				this.int2 = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0003888C File Offset: 0x0003788C
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000388A4 File Offset: 0x000378A4
		public int Int3
		{
			get
			{
				return this.int3;
			}
			set
			{
				this.int3 = value;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000388AE File Offset: 0x000378AE
		public StringInt(string s, int int1)
		{
			this.int1 = int1;
			this.int2 = 0;
			this.s = s;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000388CE File Offset: 0x000378CE
		public StringInt(string s, int int1, int int2)
		{
			this.int1 = int1;
			this.int2 = int2;
			this.s = s;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000388F0 File Offset: 0x000378F0
		public static StringInt[] ParseStringIntArray(string sections)
		{
			StringInt[] result;
			try
			{
				string[] array = sections.Split(new char[]
				{
					','
				});
				if (array == null || array.Length < 1)
				{
					result = null;
				}
				else
				{
					StringInt[] array2 = new StringInt[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						string[] array3 = text.Split(new char[]
						{
							'`'
						});
						StringInt stringInt = new StringInt(array3[0], int.Parse(array3[1]), int.Parse(array3[2]));
						array2[i] = stringInt;
					}
					result = array2;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error in ParseStringIntArray", ex.InnerException);
			}
			return result;
		}

		// Token: 0x0400010B RID: 267
		private string s;

		// Token: 0x0400010C RID: 268
		private int int1;

		// Token: 0x0400010D RID: 269
		private int int2;

		// Token: 0x0400010E RID: 270
		private int int3;
	}
}
