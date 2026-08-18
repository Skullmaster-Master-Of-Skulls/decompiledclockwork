using System;
using Spire.CompoundFile.Doc;

namespace Spire.Doc.Documents
{
	// Token: 0x02000165 RID: 357
	public class DLSException : Exception
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x000864AC File Offset: 0x000854AC
		public DLSException()
		{
			int a_ = 15;
			base..ctor(ClipboardData.b("ぴྲྀ᩸Ṻർ୾Ꞇ권쮎\udd90삒떔ﮖ連ﺞ펠\udaa2", a_));
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000864D8 File Offset: 0x000854D8
		public DLSException(Exception innerExc)
		{
			int a_ = 14;
			this..ctor(ClipboardData.b("ㅳ๵᭷ό౻੽ꚅ겋쪍\udc8f솑뒓歹ﾝ튟\udba1", a_), innerExc);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00086504 File Offset: 0x00085504
		public DLSException(string message) : base(message)
		{
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00086518 File Offset: 0x00085518
		public DLSException(string message, Exception innerExc) : base(message, innerExc)
		{
		}

		// Token: 0x040013C1 RID: 5057
		private int \u25D9\u0083\u00AD\u00AE;

		// Token: 0x040013C2 RID: 5058
		private const string ᜀ = "Exception in DLS library";
	}
}
