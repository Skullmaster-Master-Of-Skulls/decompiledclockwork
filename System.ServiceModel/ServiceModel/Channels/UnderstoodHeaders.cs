using System;
using System.Collections;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D6 RID: 2518
	public sealed class UnderstoodHeaders : IEnumerable<MessageHeaderInfo>, IEnumerable
	{
		// Token: 0x0600637E RID: 25470 RVA: 0x00173B0D File Offset: 0x00171D0D
		internal UnderstoodHeaders(MessageHeaders messageHeaders, bool modified)
		{
			this.messageHeaders = messageHeaders;
			this.modified = modified;
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x0600637F RID: 25471 RVA: 0x00173B23 File Offset: 0x00171D23
		// (set) Token: 0x06006380 RID: 25472 RVA: 0x00173B2B File Offset: 0x00171D2B
		internal bool Modified
		{
			get
			{
				return this.modified;
			}
			set
			{
				this.modified = value;
			}
		}

		// Token: 0x06006381 RID: 25473 RVA: 0x00173B34 File Offset: 0x00171D34
		public void Add(MessageHeaderInfo headerInfo)
		{
			this.messageHeaders.AddUnderstood(headerInfo);
			this.modified = true;
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x00173B49 File Offset: 0x00171D49
		public bool Contains(MessageHeaderInfo headerInfo)
		{
			return this.messageHeaders.IsUnderstood(headerInfo);
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x00173B57 File Offset: 0x00171D57
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x00173B5F File Offset: 0x00171D5F
		public IEnumerator<MessageHeaderInfo> GetEnumerator()
		{
			return this.messageHeaders.GetUnderstoodEnumerator();
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x00173B6C File Offset: 0x00171D6C
		public void Remove(MessageHeaderInfo headerInfo)
		{
			this.messageHeaders.RemoveUnderstood(headerInfo);
			this.modified = true;
		}

		// Token: 0x04003971 RID: 14705
		private MessageHeaders messageHeaders;

		// Token: 0x04003972 RID: 14706
		private bool modified;
	}
}
