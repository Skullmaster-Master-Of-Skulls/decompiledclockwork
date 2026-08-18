using System;

namespace Renci.SshNet.Messages
{
	// Token: 0x02000098 RID: 152
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class MessageAttribute : Attribute
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		public string Name { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0001DC05 File Offset: 0x0001BE05
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x0001DC0D File Offset: 0x0001BE0D
		public byte Number { get; set; }

		// Token: 0x0600078F RID: 1935 RVA: 0x0001DC16 File Offset: 0x0001BE16
		public MessageAttribute(string name, byte number)
		{
			this.Name = name;
			this.Number = number;
		}
	}
}
