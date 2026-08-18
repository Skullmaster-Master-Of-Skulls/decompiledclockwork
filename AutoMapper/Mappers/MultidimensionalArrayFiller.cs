using System;

namespace AutoMapper.Mappers
{
	// Token: 0x02000086 RID: 134
	public class MultidimensionalArrayFiller
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0001160D File Offset: 0x0000F80D
		public MultidimensionalArrayFiller(Array destination)
		{
			this.indices = new int[destination.Rank];
			this.destination = destination;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00011630 File Offset: 0x0000F830
		public void NewValue(object value)
		{
			int num = this.destination.Rank - 1;
			bool flag = false;
			while (this.indices[num] == this.destination.GetLength(num))
			{
				this.indices[num] = 0;
				num--;
				if (num < 0)
				{
					throw new InvalidOperationException("Not enough room in destination array " + this.destination);
				}
				this.indices[num]++;
				flag = true;
			}
			this.destination.SetValue(value, this.indices);
			if (flag)
			{
				this.indices[num + 1]++;
				return;
			}
			this.indices[num]++;
		}

		// Token: 0x040000CD RID: 205
		private int[] indices;

		// Token: 0x040000CE RID: 206
		private Array destination;
	}
}
