using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.fonts.cmaps
{
	// Token: 0x0200005A RID: 90
	public class CMap
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000CBBC File Offset: 0x0000BBBC
		public bool HasOneByteMappings()
		{
			return this.singleByteMappings.Count != 0;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000CBCF File Offset: 0x0000BBCF
		public bool HasTwoByteMappings()
		{
			return this.doubleByteMappings.Count != 0;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000CBE4 File Offset: 0x0000BBE4
		public string Lookup(byte[] code, int offset, int length)
		{
			string result = null;
			if (length == 1)
			{
				int key = (int)(code[offset] & byte.MaxValue);
				this.singleByteMappings.TryGetValue(key, out result);
			}
			else if (length == 2)
			{
				int num = (int)(code[offset] & byte.MaxValue);
				num <<= 8;
				num += (int)(code[offset + 1] & byte.MaxValue);
				this.doubleByteMappings.TryGetValue(num, out result);
			}
			return result;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CC44 File Offset: 0x0000BC44
		public void AddMapping(byte[] src, string dest)
		{
			if (src.Length == 1)
			{
				this.singleByteMappings[(int)(src[0] & byte.MaxValue)] = dest;
				return;
			}
			if (src.Length == 2)
			{
				int num = (int)(src[0] & byte.MaxValue);
				num <<= 8;
				num |= (int)(src[1] & byte.MaxValue);
				this.doubleByteMappings[num] = dest;
				return;
			}
			throw new IOException(MessageLocalization.GetComposedMessage("mapping.code.should.be.1.or.two.bytes.and.not.1", src.Length));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CCB2 File Offset: 0x0000BCB2
		public void AddCodespaceRange(CodespaceRange range)
		{
			this.codeSpaceRanges.Add(range);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000CCC0 File Offset: 0x0000BCC0
		public IList<CodespaceRange> GetCodeSpaceRanges()
		{
			return this.codeSpaceRanges;
		}

		// Token: 0x04000142 RID: 322
		private IList<CodespaceRange> codeSpaceRanges = new List<CodespaceRange>();

		// Token: 0x04000143 RID: 323
		private IDictionary<int, string> singleByteMappings = new Dictionary<int, string>();

		// Token: 0x04000144 RID: 324
		private IDictionary<int, string> doubleByteMappings = new Dictionary<int, string>();
	}
}
