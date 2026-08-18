using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000127 RID: 295
	internal class XmlBuffer
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x00020F2C File Offset: 0x0001F12C
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

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00020F9E File Offset: 0x0001F19E
		public int BufferSize
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		public int SectionCount
		{
			get
			{
				return this.sections.Count;
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00020FB8 File Offset: 0x0001F1B8
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

		// Token: 0x060007EB RID: 2027 RVA: 0x00021044 File Offset: 0x0001F244
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

		// Token: 0x060007EC RID: 2028 RVA: 0x000210BC File Offset: 0x0001F2BC
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

		// Token: 0x060007ED RID: 2029 RVA: 0x0002110A File Offset: 0x0001F30A
		private Exception CreateInvalidStateException()
		{
			return new InvalidOperationException(SR.GetString("XmlBufferInInvalidState"));
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002111C File Offset: 0x0001F31C
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

		// Token: 0x060007EF RID: 2031 RVA: 0x00021180 File Offset: 0x0001F380
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

		// Token: 0x04000AF7 RID: 2807
		private List<XmlBuffer.Section> sections;

		// Token: 0x04000AF8 RID: 2808
		private byte[] buffer;

		// Token: 0x04000AF9 RID: 2809
		private int offset;

		// Token: 0x04000AFA RID: 2810
		private BufferedOutputStream stream;

		// Token: 0x04000AFB RID: 2811
		private XmlBuffer.BufferState bufferState;

		// Token: 0x04000AFC RID: 2812
		private XmlDictionaryWriter writer;

		// Token: 0x04000AFD RID: 2813
		private XmlDictionaryReaderQuotas quotas;

		// Token: 0x02000AEE RID: 2798
		private enum BufferState
		{
			// Token: 0x04003F39 RID: 16185
			Created,
			// Token: 0x04003F3A RID: 16186
			Writing,
			// Token: 0x04003F3B RID: 16187
			Reading
		}

		// Token: 0x02000AEF RID: 2799
		private struct Section
		{
			// Token: 0x06006F1E RID: 28446 RVA: 0x0019D2C3 File Offset: 0x0019B4C3
			public Section(int offset, int size, XmlDictionaryReaderQuotas quotas)
			{
				this.offset = offset;
				this.size = size;
				this.quotas = quotas;
			}

			// Token: 0x170019ED RID: 6637
			// (get) Token: 0x06006F1F RID: 28447 RVA: 0x0019D2DA File Offset: 0x0019B4DA
			public int Offset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x170019EE RID: 6638
			// (get) Token: 0x06006F20 RID: 28448 RVA: 0x0019D2E2 File Offset: 0x0019B4E2
			public int Size
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x170019EF RID: 6639
			// (get) Token: 0x06006F21 RID: 28449 RVA: 0x0019D2EA File Offset: 0x0019B4EA
			public XmlDictionaryReaderQuotas Quotas
			{
				get
				{
					return this.quotas;
				}
			}

			// Token: 0x04003F3C RID: 16188
			private int offset;

			// Token: 0x04003F3D RID: 16189
			private int size;

			// Token: 0x04003F3E RID: 16190
			private XmlDictionaryReaderQuotas quotas;
		}
	}
}
