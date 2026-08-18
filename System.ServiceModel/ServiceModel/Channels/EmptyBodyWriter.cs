using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B8 RID: 2488
	internal class EmptyBodyWriter : BodyWriter
	{
		// Token: 0x060061D3 RID: 25043 RVA: 0x0016C637 File Offset: 0x0016A837
		private EmptyBodyWriter() : base(true)
		{
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x0016C640 File Offset: 0x0016A840
		public static EmptyBodyWriter Value
		{
			get
			{
				if (EmptyBodyWriter.value == null)
				{
					EmptyBodyWriter.value = new EmptyBodyWriter();
				}
				return EmptyBodyWriter.value;
			}
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x060061D5 RID: 25045 RVA: 0x0016C658 File Offset: 0x0016A858
		internal override bool IsEmpty
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x0016C65B File Offset: 0x0016A85B
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
		}

		// Token: 0x040038DA RID: 14554
		private static EmptyBodyWriter value;
	}
}
