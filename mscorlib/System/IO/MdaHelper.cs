using System;

namespace System.IO
{
	// Token: 0x020005CE RID: 1486
	internal sealed class MdaHelper
	{
		// Token: 0x06003788 RID: 14216 RVA: 0x000BB321 File Offset: 0x000BA321
		internal MdaHelper(StreamWriter sw, string cs)
		{
			this.streamWriter = sw;
			this.allocatedCallstack = cs;
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000BB338 File Offset: 0x000BA338
		protected override void Finalize()
		{
			try
			{
				if (this.streamWriter.charPos != 0 && this.streamWriter.stream != null && this.streamWriter.stream != Stream.Null)
				{
					string text = (this.streamWriter.stream is FileStream) ? ((FileStream)this.streamWriter.stream).NameInternal : "<unknown>";
					string text2 = Environment.GetResourceString("IO_StreamWriterBufferedDataLost", new object[]
					{
						this.streamWriter.stream.GetType().FullName,
						text
					});
					if (this.allocatedCallstack != null)
					{
						string text3 = text2;
						text2 = string.Concat(new string[]
						{
							text3,
							Environment.NewLine,
							Environment.GetResourceString("AllocatedFrom"),
							Environment.NewLine,
							this.allocatedCallstack
						});
					}
					Mda.StreamWriterBufferedDataLost(text2);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04001CD7 RID: 7383
		private StreamWriter streamWriter;

		// Token: 0x04001CD8 RID: 7384
		private string allocatedCallstack;
	}
}
