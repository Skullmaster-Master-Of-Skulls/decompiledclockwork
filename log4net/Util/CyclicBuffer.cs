using System;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020000F6 RID: 246
	public class CyclicBuffer
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00015E18 File Offset: 0x00014018
		public CyclicBuffer(int maxSize)
		{
			if (maxSize < 1)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("maxSize", maxSize, "Parameter: maxSize, Value: [" + maxSize + "] out of range. Non zero positive integer required");
			}
			this.m_maxSize = maxSize;
			this.m_events = new LoggingEvent[maxSize];
			this.m_first = 0;
			this.m_last = 0;
			this.m_numElems = 0;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00015E80 File Offset: 0x00014080
		public LoggingEvent Append(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			LoggingEvent result;
			lock (this)
			{
				LoggingEvent loggingEvent2 = this.m_events[this.m_last];
				this.m_events[this.m_last] = loggingEvent;
				if (++this.m_last == this.m_maxSize)
				{
					this.m_last = 0;
				}
				if (this.m_numElems < this.m_maxSize)
				{
					this.m_numElems++;
				}
				else if (++this.m_first == this.m_maxSize)
				{
					this.m_first = 0;
				}
				if (this.m_numElems < this.m_maxSize)
				{
					result = null;
				}
				else
				{
					result = loggingEvent2;
				}
			}
			return result;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00015F58 File Offset: 0x00014158
		public LoggingEvent PopOldest()
		{
			LoggingEvent result;
			lock (this)
			{
				LoggingEvent loggingEvent = null;
				if (this.m_numElems > 0)
				{
					this.m_numElems--;
					loggingEvent = this.m_events[this.m_first];
					this.m_events[this.m_first] = null;
					if (++this.m_first == this.m_maxSize)
					{
						this.m_first = 0;
					}
				}
				result = loggingEvent;
			}
			return result;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00015FE8 File Offset: 0x000141E8
		public LoggingEvent[] PopAll()
		{
			LoggingEvent[] result;
			lock (this)
			{
				LoggingEvent[] array = new LoggingEvent[this.m_numElems];
				if (this.m_numElems > 0)
				{
					if (this.m_first < this.m_last)
					{
						Array.Copy(this.m_events, this.m_first, array, 0, this.m_numElems);
					}
					else
					{
						Array.Copy(this.m_events, this.m_first, array, 0, this.m_maxSize - this.m_first);
						Array.Copy(this.m_events, 0, array, this.m_maxSize - this.m_first, this.m_last);
					}
				}
				this.Clear();
				result = array;
			}
			return result;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000160A4 File Offset: 0x000142A4
		public void Clear()
		{
			lock (this)
			{
				Array.Clear(this.m_events, 0, this.m_events.Length);
				this.m_first = 0;
				this.m_last = 0;
				this.m_numElems = 0;
			}
		}

		// Token: 0x1700016A RID: 362
		public LoggingEvent this[int i]
		{
			get
			{
				LoggingEvent result;
				lock (this)
				{
					if (i < 0 || i >= this.m_numElems)
					{
						result = null;
					}
					else
					{
						result = this.m_events[(this.m_first + i) % this.m_maxSize];
					}
				}
				return result;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00016164 File Offset: 0x00014364
		public int MaxSize
		{
			get
			{
				int maxSize;
				lock (this)
				{
					maxSize = this.m_maxSize;
				}
				return maxSize;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x000161A4 File Offset: 0x000143A4
		public int Length
		{
			get
			{
				int numElems;
				lock (this)
				{
					numElems = this.m_numElems;
				}
				return numElems;
			}
		}

		// Token: 0x040002A6 RID: 678
		private LoggingEvent[] m_events;

		// Token: 0x040002A7 RID: 679
		private int m_first;

		// Token: 0x040002A8 RID: 680
		private int m_last;

		// Token: 0x040002A9 RID: 681
		private int m_numElems;

		// Token: 0x040002AA RID: 682
		private int m_maxSize;
	}
}
