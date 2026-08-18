using System;
using System.Runtime.Serialization;
using Spire.CompoundFile.Doc;

namespace Spire.Doc
{
	// Token: 0x020000D6 RID: 214
	public class FileCorruptException : Exception
	{
		// Token: 0x06000252 RID: 594 RVA: 0x000196E8 File Offset: 0x000186E8
		public FileCorruptException()
		{
			int a_ = 0;
			base..ctor(ClipboardData.b("≥ݧ३ᥫͭᕯᱱs噵ᅷॹ屻ᵽﺉ낏望뢗쾟톡힣쾥쪧용즫躭쒯\uddb1钳\udab5ힷ\udbb9\ud8bb", a_));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00019714 File Offset: 0x00018714
		public FileCorruptException(Exception innerExc)
		{
			int a_ = 6;
			this..ctor(ClipboardData.b("⡫ŭ፯ݱᥳ፵ᙷ๹屻᝽ꊁ慎曆ﺍ뚕聯뺝즟쾡풣즥\udba7\ud9a9얫청\udcafힱ钳습ힷ骹킻톽ꆿꛁ", a_), innerExc);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00019740 File Offset: 0x00018740
		public FileCorruptException(string message) : base(message)
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00019754 File Offset: 0x00018754
		public FileCorruptException(string message, Exception innerExc) : base(message, innerExc)
		{
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0001976C File Offset: 0x0001876C
		public FileCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000C62 RID: 3170
		private long \u2593\u009Dª\u00AB;

		// Token: 0x04000C63 RID: 3171
		private byte \u25D8\u00AD\u0093\u00A2;

		// Token: 0x04000C64 RID: 3172
		private const string ᜀ = "Document is corrupted and impossible to load";
	}
}
