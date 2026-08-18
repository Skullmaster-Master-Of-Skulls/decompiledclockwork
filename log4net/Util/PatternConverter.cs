using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using log4net.Repository;

namespace log4net.Util
{
	// Token: 0x0200008A RID: 138
	public abstract class PatternConverter
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000E98D File Offset: 0x0000CB8D
		public virtual PatternConverter Next
		{
			get
			{
				return this.m_next;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000E995 File Offset: 0x0000CB95
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0000E9AE File Offset: 0x0000CBAE
		public virtual FormattingInfo FormattingInfo
		{
			get
			{
				return new FormattingInfo(this.m_min, this.m_max, this.m_leftAlign);
			}
			set
			{
				this.m_min = value.Min;
				this.m_max = value.Max;
				this.m_leftAlign = value.LeftAlign;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public virtual string Option
		{
			get
			{
				return this.m_option;
			}
			set
			{
				this.m_option = value;
			}
		}

		// Token: 0x06000490 RID: 1168
		protected abstract void Convert(TextWriter writer, object state);

		// Token: 0x06000491 RID: 1169 RVA: 0x0000E9E5 File Offset: 0x0000CBE5
		public virtual PatternConverter SetNext(PatternConverter patternConverter)
		{
			this.m_next = patternConverter;
			return this.m_next;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public virtual void Format(TextWriter writer, object state)
		{
			if (this.m_min < 0 && this.m_max == 2147483647)
			{
				this.Convert(writer, state);
				return;
			}
			string value = null;
			int num;
			lock (this.m_formatWriter)
			{
				this.m_formatWriter.Reset(1024, 256);
				this.Convert(this.m_formatWriter, state);
				StringBuilder stringBuilder = this.m_formatWriter.GetStringBuilder();
				num = stringBuilder.Length;
				if (num > this.m_max)
				{
					value = stringBuilder.ToString(num - this.m_max, this.m_max);
					num = this.m_max;
				}
				else
				{
					value = stringBuilder.ToString();
				}
			}
			if (num >= this.m_min)
			{
				writer.Write(value);
				return;
			}
			if (this.m_leftAlign)
			{
				writer.Write(value);
				PatternConverter.SpacePad(writer, this.m_min - num);
				return;
			}
			PatternConverter.SpacePad(writer, this.m_min - num);
			writer.Write(value);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		protected static void SpacePad(TextWriter writer, int length)
		{
			while (length >= 32)
			{
				writer.Write(PatternConverter.SPACES[5]);
				length -= 32;
			}
			for (int i = 4; i >= 0; i--)
			{
				if ((length & 1 << i) != 0)
				{
					writer.Write(PatternConverter.SPACES[i]);
				}
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000EB42 File Offset: 0x0000CD42
		protected static void WriteDictionary(TextWriter writer, ILoggerRepository repository, IDictionary value)
		{
			PatternConverter.WriteDictionary(writer, repository, value.GetEnumerator());
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000EB54 File Offset: 0x0000CD54
		protected static void WriteDictionary(TextWriter writer, ILoggerRepository repository, IDictionaryEnumerator value)
		{
			writer.Write("{");
			bool flag = true;
			while (value.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					writer.Write(", ");
				}
				PatternConverter.WriteObject(writer, repository, value.Key);
				writer.Write("=");
				PatternConverter.WriteObject(writer, repository, value.Value);
			}
			writer.Write("}");
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000EBBA File Offset: 0x0000CDBA
		protected static void WriteObject(TextWriter writer, ILoggerRepository repository, object value)
		{
			if (repository != null)
			{
				repository.RendererMap.FindAndRender(value, writer);
				return;
			}
			if (value == null)
			{
				writer.Write(SystemInfo.NullText);
				return;
			}
			writer.Write(value.ToString());
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public PropertiesDictionary Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x040001F3 RID: 499
		private const int c_renderBufferSize = 256;

		// Token: 0x040001F4 RID: 500
		private const int c_renderBufferMaxCapacity = 1024;

		// Token: 0x040001F5 RID: 501
		private static readonly string[] SPACES = new string[]
		{
			" ",
			"  ",
			"    ",
			"        ",
			"                ",
			"                                "
		};

		// Token: 0x040001F6 RID: 502
		private PatternConverter m_next;

		// Token: 0x040001F7 RID: 503
		private int m_min = -1;

		// Token: 0x040001F8 RID: 504
		private int m_max = int.MaxValue;

		// Token: 0x040001F9 RID: 505
		private bool m_leftAlign;

		// Token: 0x040001FA RID: 506
		private string m_option;

		// Token: 0x040001FB RID: 507
		private ReusableStringWriter m_formatWriter = new ReusableStringWriter(CultureInfo.InvariantCulture);

		// Token: 0x040001FC RID: 508
		private PropertiesDictionary properties;
	}
}
