using System;
using System.IO;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000775 RID: 1909
	internal abstract class BufferedMessageWriter
	{
		// Token: 0x060048FD RID: 18685 RVA: 0x0010D54C File Offset: 0x0010B74C
		public BufferedMessageWriter()
		{
			this.stream = new BufferManagerOutputStream("MaxSentMessageSizeExceeded");
			this.InitMessagePredicter();
		}

		// Token: 0x060048FE RID: 18686
		protected abstract XmlDictionaryWriter TakeXmlWriter(Stream stream);

		// Token: 0x060048FF RID: 18687
		protected abstract void ReturnXmlWriter(XmlDictionaryWriter writer);

		// Token: 0x06004900 RID: 18688 RVA: 0x0010D56C File Offset: 0x0010B76C
		public ArraySegment<byte> WriteMessage(Message message, BufferManager bufferManager, int initialOffset, int maxSizeQuota)
		{
			int num;
			if (maxSizeQuota <= 2147483647 - initialOffset)
			{
				num = maxSizeQuota + initialOffset;
			}
			else
			{
				num = int.MaxValue;
			}
			int num2 = this.PredictMessageSize();
			if (num2 > num)
			{
				num2 = num;
			}
			else if (num2 < initialOffset)
			{
				num2 = initialOffset;
			}
			ArraySegment<byte> result;
			try
			{
				this.stream.Init(num2, maxSizeQuota, num, bufferManager);
				this.stream.Skip(initialOffset);
				XmlDictionaryWriter xmlDictionaryWriter = this.TakeXmlWriter(this.stream);
				this.OnWriteStartMessage(xmlDictionaryWriter);
				message.WriteMessage(xmlDictionaryWriter);
				this.OnWriteEndMessage(xmlDictionaryWriter);
				xmlDictionaryWriter.Flush();
				this.ReturnXmlWriter(xmlDictionaryWriter);
				int num3;
				byte[] array = this.stream.ToArray(out num3);
				this.RecordActualMessageSize(num3);
				result = new ArraySegment<byte>(array, initialOffset, num3 - initialOffset);
			}
			finally
			{
				this.stream.Clear();
			}
			return result;
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x0010D638 File Offset: 0x0010B838
		protected virtual void OnWriteStartMessage(XmlDictionaryWriter writer)
		{
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x0010D63A File Offset: 0x0010B83A
		protected virtual void OnWriteEndMessage(XmlDictionaryWriter writer)
		{
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x0010D63C File Offset: 0x0010B83C
		private void InitMessagePredicter()
		{
			this.sizeHistory = new int[4];
			for (int i = 0; i < 4; i++)
			{
				this.sizeHistory[i] = 256;
			}
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x0010D670 File Offset: 0x0010B870
		private int PredictMessageSize()
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				if (this.sizeHistory[i] > num)
				{
					num = this.sizeHistory[i];
				}
			}
			return num + 256;
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x0010D6A6 File Offset: 0x0010B8A6
		private void RecordActualMessageSize(int size)
		{
			this.sizeHistory[this.sizeHistoryIndex] = size;
			this.sizeHistoryIndex = (this.sizeHistoryIndex + 1) % 4;
		}

		// Token: 0x04002E13 RID: 11795
		private int[] sizeHistory;

		// Token: 0x04002E14 RID: 11796
		private int sizeHistoryIndex;

		// Token: 0x04002E15 RID: 11797
		private const int sizeHistoryCount = 4;

		// Token: 0x04002E16 RID: 11798
		private const int expectedSizeVariance = 256;

		// Token: 0x04002E17 RID: 11799
		private BufferManagerOutputStream stream;
	}
}
