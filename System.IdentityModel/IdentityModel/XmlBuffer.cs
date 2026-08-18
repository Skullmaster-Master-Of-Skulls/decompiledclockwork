using System;
using System.Collections.Generic;
using System.Runtime;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000E3 RID: 227
	internal class XmlBuffer
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x0001980C File Offset: 0x00017A0C
		public XmlBuffer(int maxBufferSize)
		{
			if (maxBufferSize < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxBufferSize", maxBufferSize, SR.GetString("ValueMustBeNonNegative")));
			}
			int initialSize = Math.Min(512, maxBufferSize);
			this.stream = new BufferManagerOutputStream("XmlBufferQuotaExceeded", initialSize, maxBufferSize, BufferManager.CreateBufferManager(0L, int.MaxValue));
			this.sections = new List<XmlBuffer.Section>(1);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001987E File Offset: 0x00017A7E
		public int BufferSize
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00019888 File Offset: 0x00017A88
		public int SectionCount
		{
			get
			{
				return this.sections.Count;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00019898 File Offset: 0x00017A98
		public XmlDictionaryWriter OpenSection(XmlDictionaryReaderQuotas quotas)
		{
			if (this.bufferState != XmlBuffer.BufferState.Created)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidStateException());
			}
			this.bufferState = XmlBuffer.BufferState.Writing;
			this.quotas = new XmlDictionaryReaderQuotas();
			quotas.CopyTo(this.quotas);
			if (this.writer == null)
			{
				this.writer = XmlDictionaryWriter.CreateBinaryWriter(this.stream, XD.Dictionary, null, true);
			}
			else
			{
				((IXmlBinaryWriterInitializer)this.writer).SetOutput(this.stream, XD.Dictionary, null, true);
			}
			return this.writer;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00019924 File Offset: 0x00017B24
		public void CloseSection()
		{
			if (this.bufferState != XmlBuffer.BufferState.Writing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidStateException());
			}
			this.writer.Close();
			this.bufferState = XmlBuffer.BufferState.Created;
			int num = (int)this.stream.Length - this.offset;
			this.sections.Add(new XmlBuffer.Section(this.offset, num, this.quotas));
			this.offset += num;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001999C File Offset: 0x00017B9C
		public void Close()
		{
			if (this.bufferState != XmlBuffer.BufferState.Created)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidStateException());
			}
			this.bufferState = XmlBuffer.BufferState.Reading;
			int num;
			this.buffer = this.stream.ToArray(out num);
			this.writer = null;
			this.stream = null;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000199EA File Offset: 0x00017BEA
		private Exception CreateInvalidStateException()
		{
			return new InvalidOperationException(SR.GetString("XmlBufferInInvalidState"));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000199FC File Offset: 0x00017BFC
		public XmlDictionaryReader GetReader(int sectionIndex)
		{
			if (this.bufferState != XmlBuffer.BufferState.Reading)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidStateException());
			}
			XmlBuffer.Section section = this.sections[sectionIndex];
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(this.buffer, section.Offset, section.Size, XD.Dictionary, section.Quotas, null, null);
			xmlDictionaryReader.MoveToContent();
			return xmlDictionaryReader;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00019A60 File Offset: 0x00017C60
		public void WriteTo(int sectionIndex, XmlWriter writer)
		{
			if (this.bufferState != XmlBuffer.BufferState.Reading)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidStateException());
			}
			XmlDictionaryReader reader = this.GetReader(sectionIndex);
			try
			{
				writer.WriteNode(reader, false);
			}
			finally
			{
				reader.Close();
			}
		}

		// Token: 0x0400078D RID: 1933
		private List<XmlBuffer.Section> sections;

		// Token: 0x0400078E RID: 1934
		private byte[] buffer;

		// Token: 0x0400078F RID: 1935
		private int offset;

		// Token: 0x04000790 RID: 1936
		private BufferedOutputStream stream;

		// Token: 0x04000791 RID: 1937
		private XmlBuffer.BufferState bufferState;

		// Token: 0x04000792 RID: 1938
		private XmlDictionaryWriter writer;

		// Token: 0x04000793 RID: 1939
		private XmlDictionaryReaderQuotas quotas;

		// Token: 0x0200024F RID: 591
		private enum BufferState
		{
			// Token: 0x04000FAA RID: 4010
			Created,
			// Token: 0x04000FAB RID: 4011
			Writing,
			// Token: 0x04000FAC RID: 4012
			Reading
		}

		// Token: 0x02000250 RID: 592
		private struct Section
		{
			// Token: 0x06001245 RID: 4677 RVA: 0x0004FFD5 File Offset: 0x0004E1D5
			public Section(int offset, int size, XmlDictionaryReaderQuotas quotas)
			{
				this.offset = offset;
				this.size = size;
				this.quotas = quotas;
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004FFEC File Offset: 0x0004E1EC
			public int Offset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x06001247 RID: 4679 RVA: 0x0004FFF4 File Offset: 0x0004E1F4
			public int Size
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x06001248 RID: 4680 RVA: 0x0004FFFC File Offset: 0x0004E1FC
			public XmlDictionaryReaderQuotas Quotas
			{
				get
				{
					return this.quotas;
				}
			}

			// Token: 0x04000FAD RID: 4013
			private int offset;

			// Token: 0x04000FAE RID: 4014
			private int size;

			// Token: 0x04000FAF RID: 4015
			private XmlDictionaryReaderQuotas quotas;
		}
	}
}
