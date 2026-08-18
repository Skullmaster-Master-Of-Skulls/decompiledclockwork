using System;

namespace NLog.Common
{
	// Token: 0x02000028 RID: 40
	public class LogEventInfoBuffer
	{
		// Token: 0x060000AA RID: 170 RVA: 0x000031CA File Offset: 0x000013CA
		public LogEventInfoBuffer(int size, bool growAsNeeded, int growLimit)
		{
			this.growAsNeeded = growAsNeeded;
			this.buffer = new AsyncLogEventInfo[size];
			this.growLimit = growLimit;
			this.getPointer = 0;
			this.putPointer = 0;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000031FA File Offset: 0x000013FA
		public int Size
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003204 File Offset: 0x00001404
		public int Append(AsyncLogEventInfo eventInfo)
		{
			int result;
			lock (this)
			{
				if (this.count >= this.buffer.Length)
				{
					if (this.growAsNeeded && this.buffer.Length < this.growLimit)
					{
						int num = this.buffer.Length * 2;
						if (num >= this.growLimit)
						{
							num = this.growLimit;
						}
						AsyncLogEventInfo[] destinationArray = new AsyncLogEventInfo[num];
						Array.Copy(this.buffer, 0, destinationArray, 0, this.buffer.Length);
						this.buffer = destinationArray;
					}
					else
					{
						this.getPointer++;
					}
				}
				this.putPointer %= this.buffer.Length;
				this.buffer[this.putPointer] = eventInfo;
				this.putPointer++;
				this.count++;
				if (this.count >= this.buffer.Length)
				{
					this.count = this.buffer.Length;
				}
				result = this.count;
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003320 File Offset: 0x00001520
		public AsyncLogEventInfo[] GetEventsAndClear()
		{
			AsyncLogEventInfo[] result;
			lock (this)
			{
				int num = this.count;
				AsyncLogEventInfo[] array = new AsyncLogEventInfo[num];
				for (int i = 0; i < num; i++)
				{
					int num2 = (this.getPointer + i) % this.buffer.Length;
					AsyncLogEventInfo asyncLogEventInfo = this.buffer[num2];
					this.buffer[num2] = default(AsyncLogEventInfo);
					array[i] = asyncLogEventInfo;
				}
				this.count = 0;
				this.getPointer = 0;
				this.putPointer = 0;
				result = array;
			}
			return result;
		}

		// Token: 0x04000028 RID: 40
		private readonly bool growAsNeeded;

		// Token: 0x04000029 RID: 41
		private readonly int growLimit;

		// Token: 0x0400002A RID: 42
		private AsyncLogEventInfo[] buffer;

		// Token: 0x0400002B RID: 43
		private int getPointer;

		// Token: 0x0400002C RID: 44
		private int putPointer;

		// Token: 0x0400002D RID: 45
		private int count;
	}
}
