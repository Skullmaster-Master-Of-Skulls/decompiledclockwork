using System;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000774 RID: 1908
	internal abstract class BufferedMessageData : IBufferedMessageData
	{
		// Token: 0x060048E7 RID: 18663 RVA: 0x0010D240 File Offset: 0x0010B440
		public BufferedMessageData(SynchronizedPool<RecycledMessageState> messageStatePool)
		{
			this.messageStatePool = messageStatePool;
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x060048E8 RID: 18664 RVA: 0x0010D24F File Offset: 0x0010B44F
		public ArraySegment<byte> Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x060048E9 RID: 18665 RVA: 0x0010D257 File Offset: 0x0010B457
		public BufferManager BufferManager
		{
			get
			{
				return this.bufferManager;
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x0010D25F File Offset: 0x0010B45F
		public virtual XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				return XmlDictionaryReaderQuotas.Max;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x060048EB RID: 18667
		public abstract MessageEncoder MessageEncoder { get; }

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x0010D266 File Offset: 0x0010B466
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x0010D269 File Offset: 0x0010B469
		public void EnableMultipleUsers()
		{
			this.multipleUsers = true;
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x0010D274 File Offset: 0x0010B474
		public void Close()
		{
			if (this.multipleUsers)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					int num = this.refCount - 1;
					this.refCount = num;
					if (num == 0)
					{
						this.DoClose();
					}
					return;
				}
			}
			this.DoClose();
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x0010D2D8 File Offset: 0x0010B4D8
		private void DoClose()
		{
			this.bufferManager.ReturnBuffer(this.buffer.Array);
			if (this.outstandingReaders == 0)
			{
				this.bufferManager = null;
				this.buffer = default(ArraySegment<byte>);
				this.OnClosed();
			}
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x0010D311 File Offset: 0x0010B511
		public void DoReturnMessageState(RecycledMessageState messageState)
		{
			if (this.messageState == null)
			{
				this.messageState = messageState;
				return;
			}
			this.messageStatePool.Return(messageState);
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x0010D330 File Offset: 0x0010B530
		private void DoReturnXmlReader(XmlDictionaryReader reader)
		{
			this.ReturnXmlReader(reader);
			this.outstandingReaders--;
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x0010D348 File Offset: 0x0010B548
		public RecycledMessageState DoTakeMessageState()
		{
			RecycledMessageState recycledMessageState = this.messageState;
			if (recycledMessageState != null)
			{
				this.messageState = null;
				return recycledMessageState;
			}
			return this.messageStatePool.Take();
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x0010D374 File Offset: 0x0010B574
		private XmlDictionaryReader DoTakeXmlReader()
		{
			XmlDictionaryReader result = this.TakeXmlReader();
			this.outstandingReaders++;
			return result;
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x0010D398 File Offset: 0x0010B598
		public XmlDictionaryReader GetMessageReader()
		{
			if (this.multipleUsers)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					return this.DoTakeXmlReader();
				}
			}
			return this.DoTakeXmlReader();
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x0010D3EC File Offset: 0x0010B5EC
		public void OnXmlReaderClosed(XmlDictionaryReader reader)
		{
			if (this.multipleUsers)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.DoReturnXmlReader(reader);
					return;
				}
			}
			this.DoReturnXmlReader(reader);
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x0010D43C File Offset: 0x0010B63C
		protected virtual void OnClosed()
		{
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x0010D440 File Offset: 0x0010B640
		public RecycledMessageState TakeMessageState()
		{
			if (this.multipleUsers)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					return this.DoTakeMessageState();
				}
			}
			return this.DoTakeMessageState();
		}

		// Token: 0x060048F8 RID: 18680
		protected abstract XmlDictionaryReader TakeXmlReader();

		// Token: 0x060048F9 RID: 18681 RVA: 0x0010D494 File Offset: 0x0010B694
		public void Open()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.refCount++;
			}
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x0010D4DC File Offset: 0x0010B6DC
		public void Open(ArraySegment<byte> buffer, BufferManager bufferManager)
		{
			this.refCount = 1;
			this.bufferManager = bufferManager;
			this.buffer = buffer;
			this.multipleUsers = false;
		}

		// Token: 0x060048FB RID: 18683
		protected abstract void ReturnXmlReader(XmlDictionaryReader xmlReader);

		// Token: 0x060048FC RID: 18684 RVA: 0x0010D4FC File Offset: 0x0010B6FC
		public void ReturnMessageState(RecycledMessageState messageState)
		{
			if (this.multipleUsers)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.DoReturnMessageState(messageState);
					return;
				}
			}
			this.DoReturnMessageState(messageState);
		}

		// Token: 0x04002E0C RID: 11788
		private ArraySegment<byte> buffer;

		// Token: 0x04002E0D RID: 11789
		private BufferManager bufferManager;

		// Token: 0x04002E0E RID: 11790
		private int refCount;

		// Token: 0x04002E0F RID: 11791
		private int outstandingReaders;

		// Token: 0x04002E10 RID: 11792
		private bool multipleUsers;

		// Token: 0x04002E11 RID: 11793
		private RecycledMessageState messageState;

		// Token: 0x04002E12 RID: 11794
		private SynchronizedPool<RecycledMessageState> messageStatePool;
	}
}
