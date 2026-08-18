using System;
using System.IO;
using System.util;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x020000D2 RID: 210
	public abstract class DocWriter : IDocListener, IElementListener
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00026475 File Offset: 0x00025475
		protected DocWriter()
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00026484 File Offset: 0x00025484
		protected DocWriter(Document document, Stream os)
		{
			this.document = document;
			this.os = new OutputStreamCounter(os);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000264A6 File Offset: 0x000254A6
		public virtual bool Add(IElement element)
		{
			return false;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000264A9 File Offset: 0x000254A9
		public virtual void Open()
		{
			this.open = true;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000264B2 File Offset: 0x000254B2
		public virtual bool SetPageSize(Rectangle pageSize)
		{
			this.pageSize = pageSize;
			return true;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000264BC File Offset: 0x000254BC
		public virtual bool SetMargins(float marginLeft, float marginRight, float marginTop, float marginBottom)
		{
			return false;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000264BF File Offset: 0x000254BF
		public virtual bool NewPage()
		{
			return this.open;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000264CC File Offset: 0x000254CC
		public virtual void ResetPageCount()
		{
		}

		// Token: 0x1700017F RID: 383
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x000264CE File Offset: 0x000254CE
		public virtual int PageCount
		{
			set
			{
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000264D0 File Offset: 0x000254D0
		public virtual void Close()
		{
			this.open = false;
			this.os.Flush();
			if (this.closeStream)
			{
				this.os.Close();
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000264F8 File Offset: 0x000254F8
		public static byte[] GetISOBytes(string text)
		{
			if (text == null)
			{
				return null;
			}
			int length = text.Length;
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (byte)text[i];
			}
			return array;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00026530 File Offset: 0x00025530
		public virtual void Pause()
		{
			this.pause = true;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00026539 File Offset: 0x00025539
		public bool IsPaused()
		{
			return this.pause;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00026541 File Offset: 0x00025541
		public virtual void Resume()
		{
			this.pause = false;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0002654A File Offset: 0x0002554A
		public virtual void Flush()
		{
			this.os.Flush();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00026558 File Offset: 0x00025558
		protected void Write(string str)
		{
			byte[] isobytes = DocWriter.GetISOBytes(str);
			this.os.Write(isobytes, 0, isobytes.Length);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0002657C File Offset: 0x0002557C
		protected void AddTabs(int indent)
		{
			this.os.WriteByte(10);
			for (int i = 0; i < indent; i++)
			{
				this.os.WriteByte(9);
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000265B0 File Offset: 0x000255B0
		protected void Write(string key, string value)
		{
			this.os.WriteByte(32);
			this.Write(key);
			this.os.WriteByte(61);
			this.os.WriteByte(34);
			this.Write(value);
			this.os.WriteByte(34);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000265FF File Offset: 0x000255FF
		protected void WriteStart(string tag)
		{
			this.os.WriteByte(60);
			this.Write(tag);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00026615 File Offset: 0x00025615
		protected void WriteEnd(string tag)
		{
			this.os.WriteByte(60);
			this.os.WriteByte(47);
			this.Write(tag);
			this.os.WriteByte(62);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00026645 File Offset: 0x00025645
		protected void WriteEnd()
		{
			this.os.WriteByte(32);
			this.os.WriteByte(47);
			this.os.WriteByte(62);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00026670 File Offset: 0x00025670
		protected bool WriteMarkupAttributes(Properties markup)
		{
			if (markup == null)
			{
				return false;
			}
			foreach (string key in markup.Keys)
			{
				this.Write(key, markup[key]);
			}
			markup.Clear();
			return true;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x000266D8 File Offset: 0x000256D8
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x000266E0 File Offset: 0x000256E0
		public virtual bool CloseStream
		{
			get
			{
				return this.closeStream;
			}
			set
			{
				this.closeStream = value;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000266E9 File Offset: 0x000256E9
		public virtual bool SetMarginMirroring(bool marginMirroring)
		{
			return false;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000266EC File Offset: 0x000256EC
		public virtual bool SetMarginMirroringTopBottom(bool MarginMirroring)
		{
			return false;
		}

		// Token: 0x0400061C RID: 1564
		public const byte NEWLINE = 10;

		// Token: 0x0400061D RID: 1565
		public const byte TAB = 9;

		// Token: 0x0400061E RID: 1566
		public const byte LT = 60;

		// Token: 0x0400061F RID: 1567
		public const byte SPACE = 32;

		// Token: 0x04000620 RID: 1568
		public const byte EQUALS = 61;

		// Token: 0x04000621 RID: 1569
		public const byte QUOTE = 34;

		// Token: 0x04000622 RID: 1570
		public const byte GT = 62;

		// Token: 0x04000623 RID: 1571
		public const byte FORWARD = 47;

		// Token: 0x04000624 RID: 1572
		protected Rectangle pageSize;

		// Token: 0x04000625 RID: 1573
		protected Document document;

		// Token: 0x04000626 RID: 1574
		protected OutputStreamCounter os;

		// Token: 0x04000627 RID: 1575
		protected bool open;

		// Token: 0x04000628 RID: 1576
		protected bool pause;

		// Token: 0x04000629 RID: 1577
		protected bool closeStream = true;
	}
}
