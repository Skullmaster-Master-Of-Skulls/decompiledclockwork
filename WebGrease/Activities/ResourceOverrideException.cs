using System;
using System.Runtime.Serialization;

namespace WebGrease.Activities
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class ResourceOverrideException : Exception
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0000C7D6 File Offset: 0x0000A9D6
		public ResourceOverrideException()
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000C7DE File Offset: 0x0000A9DE
		public ResourceOverrideException(string message) : base(message)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000C7E7 File Offset: 0x0000A9E7
		public ResourceOverrideException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000C7F1 File Offset: 0x0000A9F1
		public ResourceOverrideException(string fileName, string tokenKey)
		{
			this.TokenKey = tokenKey;
			this.FileName = fileName;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000C807 File Offset: 0x0000AA07
		protected ResourceOverrideException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000C811 File Offset: 0x0000AA11
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000C819 File Offset: 0x0000AA19
		public string FileName { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000C822 File Offset: 0x0000AA22
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000C82A File Offset: 0x0000AA2A
		public string TokenKey { get; private set; }

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000C834 File Offset: 0x0000AA34
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("FileName", this.FileName ?? string.Empty);
			info.AddValue("TokenKey", this.TokenKey ?? string.Empty);
			base.GetObjectData(info, context);
		}
	}
}
