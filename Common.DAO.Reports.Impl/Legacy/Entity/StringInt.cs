using System;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000028 RID: 40
	public class StringInt
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0002A174 File Offset: 0x00028374
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0002A18C File Offset: 0x0002838C
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0002A198 File Offset: 0x00028398
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0002A1B0 File Offset: 0x000283B0
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0002A1BC File Offset: 0x000283BC
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0002A1D4 File Offset: 0x000283D4
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0002A1E0 File Offset: 0x000283E0
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0002A1F8 File Offset: 0x000283F8
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

		// Token: 0x060002B3 RID: 691 RVA: 0x0002A202 File Offset: 0x00028402
		public StringInt(string s, int int1)
		{
			this.int1 = int1;
			this.int2 = 0;
			this.s = s;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0002A221 File Offset: 0x00028421
		public StringInt(string s, int int1, int int2)
		{
			this.int1 = int1;
			this.int2 = int2;
			this.s = s;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0002A240 File Offset: 0x00028440
		public static StringInt[] ParseStringIntArray(string sections)
		{
			StringInt[] result;
			try
			{
				string[] array = sections.Split(new char[]
				{
					','
				});
				bool flag = array == null || array.Length < 1;
				if (flag)
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

		// Token: 0x04000103 RID: 259
		private string s;

		// Token: 0x04000104 RID: 260
		private int int1;

		// Token: 0x04000105 RID: 261
		private int int2;

		// Token: 0x04000106 RID: 262
		private int int3;
	}
}
