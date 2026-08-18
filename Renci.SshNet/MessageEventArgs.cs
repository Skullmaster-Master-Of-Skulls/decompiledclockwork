using System;

namespace Renci.SshNet
{
	// Token: 0x02000026 RID: 38
	public class MessageEventArgs<T> : EventArgs
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007780 File Offset: 0x00005980
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00007788 File Offset: 0x00005988
		public T Message { get; private set; }

		// Token: 0x060001FC RID: 508 RVA: 0x00007791 File Offset: 0x00005991
		public MessageEventArgs(T message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this.Message = message;
		}
	}
}
