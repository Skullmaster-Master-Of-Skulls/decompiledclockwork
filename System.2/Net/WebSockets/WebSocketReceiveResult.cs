using System;

namespace System.Net.WebSockets
{
	// Token: 0x0200023B RID: 571
	public class WebSocketReceiveResult
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x0007093C File Offset: 0x0006EB3C
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage) : this(count, messageType, endOfMessage, null, null)
		{
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0007095C File Offset: 0x0006EB5C
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeStatusDescription)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Count = count;
			this.EndOfMessage = endOfMessage;
			this.MessageType = messageType;
			this.CloseStatus = closeStatus;
			this.CloseStatusDescription = closeStatusDescription;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00070998 File Offset: 0x0006EB98
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x000709A0 File Offset: 0x0006EBA0
		public int Count { get; private set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000709A9 File Offset: 0x0006EBA9
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x000709B1 File Offset: 0x0006EBB1
		public bool EndOfMessage { get; private set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000709BA File Offset: 0x0006EBBA
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x000709C2 File Offset: 0x0006EBC2
		public WebSocketMessageType MessageType { get; private set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000709CB File Offset: 0x0006EBCB
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x000709D3 File Offset: 0x0006EBD3
		public WebSocketCloseStatus? CloseStatus { get; private set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x000709DC File Offset: 0x0006EBDC
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x000709E4 File Offset: 0x0006EBE4
		public string CloseStatusDescription { get; private set; }

		// Token: 0x060015BB RID: 5563 RVA: 0x000709ED File Offset: 0x0006EBED
		internal WebSocketReceiveResult Copy(int count)
		{
			this.Count -= count;
			return new WebSocketReceiveResult(count, this.MessageType, this.Count == 0 && this.EndOfMessage, this.CloseStatus, this.CloseStatusDescription);
		}
	}
}
