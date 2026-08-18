using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000B8 RID: 184
	internal class ReadContentAsBinaryHelper
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x00016D42 File Offset: 0x00014F42
		internal ReadContentAsBinaryHelper(XmlReader reader)
		{
			this.reader = reader;
			this.canReadValueChunk = reader.CanReadValueChunk;
			if (this.canReadValueChunk)
			{
				this.valueChunk = new char[256];
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00016D75 File Offset: 0x00014F75
		internal static ReadContentAsBinaryHelper CreateOrReset(ReadContentAsBinaryHelper helper, XmlReader reader)
		{
			if (helper == null)
			{
				return new ReadContentAsBinaryHelper(reader);
			}
			helper.Reset();
			return helper;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00016D88 File Offset: 0x00014F88
		internal int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			switch (this.state)
			{
			case ReadContentAsBinaryHelper.State.None:
				if (!this.reader.CanReadContentAs())
				{
					throw this.reader.CreateReadContentAsException("ReadContentAsBase64");
				}
				if (!this.Init())
				{
					return 0;
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadContent:
				if (this.decoder == this.base64Decoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadElementContent:
				throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
			default:
				return 0;
			}
			this.InitBase64Decoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00016E50 File Offset: 0x00015050
		internal int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			switch (this.state)
			{
			case ReadContentAsBinaryHelper.State.None:
				if (!this.reader.CanReadContentAs())
				{
					throw this.reader.CreateReadContentAsException("ReadContentAsBinHex");
				}
				if (!this.Init())
				{
					return 0;
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadContent:
				if (this.decoder == this.binHexDecoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadElementContent:
				throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
			default:
				return 0;
			}
			this.InitBinHexDecoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00016F18 File Offset: 0x00015118
		internal int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			switch (this.state)
			{
			case ReadContentAsBinaryHelper.State.None:
				if (this.reader.NodeType != XmlNodeType.Element)
				{
					throw this.reader.CreateReadElementContentAsException("ReadElementContentAsBase64");
				}
				if (!this.InitOnElement())
				{
					return 0;
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadContent:
				throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
			case ReadContentAsBinaryHelper.State.InReadElementContent:
				if (this.decoder == this.base64Decoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
				break;
			default:
				return 0;
			}
			this.InitBase64Decoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00016FE4 File Offset: 0x000151E4
		internal int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			switch (this.state)
			{
			case ReadContentAsBinaryHelper.State.None:
				if (this.reader.NodeType != XmlNodeType.Element)
				{
					throw this.reader.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				if (!this.InitOnElement())
				{
					return 0;
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadContent:
				throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
			case ReadContentAsBinaryHelper.State.InReadElementContent:
				if (this.decoder == this.binHexDecoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
				break;
			default:
				return 0;
			}
			this.InitBinHexDecoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000170B0 File Offset: 0x000152B0
		internal void Finish()
		{
			if (this.state != ReadContentAsBinaryHelper.State.None)
			{
				while (this.MoveToNextContentNode(true))
				{
				}
				if (this.state == ReadContentAsBinaryHelper.State.InReadElementContent)
				{
					if (this.reader.NodeType != XmlNodeType.EndElement)
					{
						throw new XmlException("Xml_InvalidNodeType", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
					}
					this.reader.Read();
				}
			}
			this.Reset();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00017127 File Offset: 0x00015327
		internal void Reset()
		{
			this.state = ReadContentAsBinaryHelper.State.None;
			this.isEnd = false;
			this.valueOffset = 0;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001713E File Offset: 0x0001533E
		private bool Init()
		{
			if (!this.MoveToNextContentNode(false))
			{
				return false;
			}
			this.state = ReadContentAsBinaryHelper.State.InReadContent;
			this.isEnd = false;
			return true;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001715C File Offset: 0x0001535C
		private bool InitOnElement()
		{
			bool isEmptyElement = this.reader.IsEmptyElement;
			this.reader.Read();
			if (isEmptyElement)
			{
				return false;
			}
			if (this.MoveToNextContentNode(false))
			{
				this.state = ReadContentAsBinaryHelper.State.InReadElementContent;
				this.isEnd = false;
				return true;
			}
			if (this.reader.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.reader.Read();
			return false;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000171EA File Offset: 0x000153EA
		private void InitBase64Decoder()
		{
			if (this.base64Decoder == null)
			{
				this.base64Decoder = new Base64Decoder();
			}
			else
			{
				this.base64Decoder.Reset();
			}
			this.decoder = this.base64Decoder;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00017218 File Offset: 0x00015418
		private void InitBinHexDecoder()
		{
			if (this.binHexDecoder == null)
			{
				this.binHexDecoder = new BinHexDecoder();
			}
			else
			{
				this.binHexDecoder.Reset();
			}
			this.decoder = this.binHexDecoder;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00017248 File Offset: 0x00015448
		private int ReadContentAsBinary(byte[] buffer, int index, int count)
		{
			if (this.isEnd)
			{
				this.Reset();
				return 0;
			}
			this.decoder.SetNextOutputBuffer(buffer, index, count);
			for (;;)
			{
				if (this.canReadValueChunk)
				{
					for (;;)
					{
						if (this.valueOffset < this.valueChunkLength)
						{
							int num = this.decoder.Decode(this.valueChunk, this.valueOffset, this.valueChunkLength - this.valueOffset);
							this.valueOffset += num;
						}
						if (this.decoder.IsFull)
						{
							goto Block_3;
						}
						if ((this.valueChunkLength = this.reader.ReadValueChunk(this.valueChunk, 0, 256)) == 0)
						{
							break;
						}
						this.valueOffset = 0;
					}
				}
				else
				{
					string value = this.reader.Value;
					int num2 = this.decoder.Decode(value, this.valueOffset, value.Length - this.valueOffset);
					this.valueOffset += num2;
					if (this.decoder.IsFull)
					{
						goto Block_5;
					}
				}
				this.valueOffset = 0;
				if (!this.MoveToNextContentNode(true))
				{
					goto Block_6;
				}
			}
			Block_3:
			return this.decoder.DecodedCount;
			Block_5:
			return this.decoder.DecodedCount;
			Block_6:
			this.isEnd = true;
			return this.decoder.DecodedCount;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00017380 File Offset: 0x00015580
		private int ReadElementContentAsBinary(byte[] buffer, int index, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = this.ReadContentAsBinary(buffer, index, count);
			if (num > 0)
			{
				return num;
			}
			if (this.reader.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.reader.Read();
			this.state = ReadContentAsBinaryHelper.State.None;
			return 0;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000173F4 File Offset: 0x000155F4
		private bool MoveToNextContentNode(bool moveIfOnContentNode)
		{
			for (;;)
			{
				switch (this.reader.NodeType)
				{
				case XmlNodeType.Attribute:
					goto IL_52;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (!moveIfOnContentNode)
					{
						return true;
					}
					goto IL_78;
				case XmlNodeType.EntityReference:
					if (this.reader.CanResolveEntity)
					{
						this.reader.ResolveEntity();
						goto IL_78;
					}
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					goto IL_78;
				}
				break;
				IL_78:
				moveIfOnContentNode = false;
				if (!this.reader.Read())
				{
					return false;
				}
			}
			return false;
			IL_52:
			return !moveIfOnContentNode;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00017490 File Offset: 0x00015690
		internal Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadContentAsBase64Async>d__27 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadContentAsBase64Async>d__27>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000174EC File Offset: 0x000156EC
		internal Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadContentAsBinHexAsync>d__28 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadContentAsBinHexAsync>d__28>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00017548 File Offset: 0x00015748
		internal Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadElementContentAsBase64Async>d__29 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadElementContentAsBase64Async>d__29>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000175A4 File Offset: 0x000157A4
		internal Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadElementContentAsBinHexAsync>d__30 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadElementContentAsBinHexAsync>d__30>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00017600 File Offset: 0x00015800
		internal Task FinishAsync()
		{
			ReadContentAsBinaryHelper.<FinishAsync>d__31 <FinishAsync>d__;
			<FinishAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishAsync>d__.<>4__this = this;
			<FinishAsync>d__.<>1__state = -1;
			<FinishAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<FinishAsync>d__31>(ref <FinishAsync>d__);
			return <FinishAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00017644 File Offset: 0x00015844
		private Task<bool> InitAsync()
		{
			ReadContentAsBinaryHelper.<InitAsync>d__32 <InitAsync>d__;
			<InitAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitAsync>d__.<>4__this = this;
			<InitAsync>d__.<>1__state = -1;
			<InitAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<InitAsync>d__32>(ref <InitAsync>d__);
			return <InitAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00017688 File Offset: 0x00015888
		private Task<bool> InitOnElementAsync()
		{
			ReadContentAsBinaryHelper.<InitOnElementAsync>d__33 <InitOnElementAsync>d__;
			<InitOnElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitOnElementAsync>d__.<>4__this = this;
			<InitOnElementAsync>d__.<>1__state = -1;
			<InitOnElementAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<InitOnElementAsync>d__33>(ref <InitOnElementAsync>d__);
			return <InitOnElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000176CC File Offset: 0x000158CC
		private Task<int> ReadContentAsBinaryAsync(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadContentAsBinaryAsync>d__34 <ReadContentAsBinaryAsync>d__;
			<ReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinaryAsync>d__.<>4__this = this;
			<ReadContentAsBinaryAsync>d__.buffer = buffer;
			<ReadContentAsBinaryAsync>d__.index = index;
			<ReadContentAsBinaryAsync>d__.count = count;
			<ReadContentAsBinaryAsync>d__.<>1__state = -1;
			<ReadContentAsBinaryAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadContentAsBinaryAsync>d__34>(ref <ReadContentAsBinaryAsync>d__);
			return <ReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00017728 File Offset: 0x00015928
		private Task<int> ReadElementContentAsBinaryAsync(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadElementContentAsBinaryAsync>d__35 <ReadElementContentAsBinaryAsync>d__;
			<ReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<ReadElementContentAsBinaryAsync>d__.buffer = buffer;
			<ReadElementContentAsBinaryAsync>d__.index = index;
			<ReadElementContentAsBinaryAsync>d__.count = count;
			<ReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinaryAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadElementContentAsBinaryAsync>d__35>(ref <ReadElementContentAsBinaryAsync>d__);
			return <ReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00017784 File Offset: 0x00015984
		private Task<bool> MoveToNextContentNodeAsync(bool moveIfOnContentNode)
		{
			ReadContentAsBinaryHelper.<MoveToNextContentNodeAsync>d__36 <MoveToNextContentNodeAsync>d__;
			<MoveToNextContentNodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<MoveToNextContentNodeAsync>d__.<>4__this = this;
			<MoveToNextContentNodeAsync>d__.moveIfOnContentNode = moveIfOnContentNode;
			<MoveToNextContentNodeAsync>d__.<>1__state = -1;
			<MoveToNextContentNodeAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<MoveToNextContentNodeAsync>d__36>(ref <MoveToNextContentNodeAsync>d__);
			return <MoveToNextContentNodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400029D RID: 669
		private XmlReader reader;

		// Token: 0x0400029E RID: 670
		private ReadContentAsBinaryHelper.State state;

		// Token: 0x0400029F RID: 671
		private int valueOffset;

		// Token: 0x040002A0 RID: 672
		private bool isEnd;

		// Token: 0x040002A1 RID: 673
		private bool canReadValueChunk;

		// Token: 0x040002A2 RID: 674
		private char[] valueChunk;

		// Token: 0x040002A3 RID: 675
		private int valueChunkLength;

		// Token: 0x040002A4 RID: 676
		private IncrementalReadDecoder decoder;

		// Token: 0x040002A5 RID: 677
		private Base64Decoder base64Decoder;

		// Token: 0x040002A6 RID: 678
		private BinHexDecoder binHexDecoder;

		// Token: 0x040002A7 RID: 679
		private const int ChunkSize = 256;

		// Token: 0x02000318 RID: 792
		private enum State
		{
			// Token: 0x04001496 RID: 5270
			None,
			// Token: 0x04001497 RID: 5271
			InReadContent,
			// Token: 0x04001498 RID: 5272
			InReadElementContent
		}
	}
}
