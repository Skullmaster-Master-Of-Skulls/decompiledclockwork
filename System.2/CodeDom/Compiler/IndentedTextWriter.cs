using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000681 RID: 1665
	public class IndentedTextWriter : TextWriter
	{
		// Token: 0x06003D4E RID: 15694 RVA: 0x000FC01F File Offset: 0x000FA21F
		public IndentedTextWriter(TextWriter writer) : this(writer, "    ")
		{
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x000FC02D File Offset: 0x000FA22D
		public IndentedTextWriter(TextWriter writer, string tabString) : base(CultureInfo.InvariantCulture)
		{
			this.writer = writer;
			this.tabString = tabString;
			this.indentLevel = 0;
			this.tabsPending = false;
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000FC056 File Offset: 0x000FA256
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003D51 RID: 15697 RVA: 0x000FC063 File Offset: 0x000FA263
		// (set) Token: 0x06003D52 RID: 15698 RVA: 0x000FC070 File Offset: 0x000FA270
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
			set
			{
				this.writer.NewLine = value;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x000FC07E File Offset: 0x000FA27E
		// (set) Token: 0x06003D54 RID: 15700 RVA: 0x000FC086 File Offset: 0x000FA286
		public int Indent
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.indentLevel = value;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x000FC096 File Offset: 0x000FA296
		public TextWriter InnerWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003D56 RID: 15702 RVA: 0x000FC09E File Offset: 0x000FA29E
		internal string TabString
		{
			get
			{
				return this.tabString;
			}
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000FC0A6 File Offset: 0x000FA2A6
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000FC0B3 File Offset: 0x000FA2B3
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000FC0C0 File Offset: 0x000FA2C0
		protected virtual void OutputTabs()
		{
			if (this.tabsPending)
			{
				for (int i = 0; i < this.indentLevel; i++)
				{
					this.writer.Write(this.tabString);
				}
				this.tabsPending = false;
			}
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000FC0FE File Offset: 0x000FA2FE
		public override void Write(string s)
		{
			this.OutputTabs();
			this.writer.Write(s);
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000FC112 File Offset: 0x000FA312
		public override void Write(bool value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000FC126 File Offset: 0x000FA326
		public override void Write(char value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000FC13A File Offset: 0x000FA33A
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this.writer.Write(buffer);
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x000FC14E File Offset: 0x000FA34E
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this.writer.Write(buffer, index, count);
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000FC164 File Offset: 0x000FA364
		public override void Write(double value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x000FC178 File Offset: 0x000FA378
		public override void Write(float value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000FC18C File Offset: 0x000FA38C
		public override void Write(int value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x000FC1A0 File Offset: 0x000FA3A0
		public override void Write(long value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x000FC1B4 File Offset: 0x000FA3B4
		public override void Write(object value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x000FC1C8 File Offset: 0x000FA3C8
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this.writer.Write(format, arg0);
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x000FC1DD File Offset: 0x000FA3DD
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this.writer.Write(format, arg0, arg1);
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x000FC1F3 File Offset: 0x000FA3F3
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this.writer.Write(format, arg);
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x000FC208 File Offset: 0x000FA408
		public void WriteLineNoTabs(string s)
		{
			this.writer.WriteLine(s);
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x000FC216 File Offset: 0x000FA416
		public override void WriteLine(string s)
		{
			this.OutputTabs();
			this.writer.WriteLine(s);
			this.tabsPending = true;
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x000FC231 File Offset: 0x000FA431
		public override void WriteLine()
		{
			this.OutputTabs();
			this.writer.WriteLine();
			this.tabsPending = true;
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x000FC24B File Offset: 0x000FA44B
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x000FC266 File Offset: 0x000FA466
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x000FC281 File Offset: 0x000FA481
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this.writer.WriteLine(buffer);
			this.tabsPending = true;
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x000FC29C File Offset: 0x000FA49C
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this.writer.WriteLine(buffer, index, count);
			this.tabsPending = true;
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000FC2B9 File Offset: 0x000FA4B9
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x000FC2D4 File Offset: 0x000FA4D4
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x000FC2EF File Offset: 0x000FA4EF
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000FC30A File Offset: 0x000FA50A
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x000FC325 File Offset: 0x000FA525
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000FC340 File Offset: 0x000FA540
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this.writer.WriteLine(format, arg0);
			this.tabsPending = true;
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000FC35C File Offset: 0x000FA55C
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this.writer.WriteLine(format, arg0, arg1);
			this.tabsPending = true;
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000FC379 File Offset: 0x000FA579
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this.writer.WriteLine(format, arg);
			this.tabsPending = true;
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000FC395 File Offset: 0x000FA595
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000FC3B0 File Offset: 0x000FA5B0
		internal void InternalOutputTabs()
		{
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.writer.Write(this.tabString);
			}
		}

		// Token: 0x04002CBD RID: 11453
		private TextWriter writer;

		// Token: 0x04002CBE RID: 11454
		private int indentLevel;

		// Token: 0x04002CBF RID: 11455
		private bool tabsPending;

		// Token: 0x04002CC0 RID: 11456
		private string tabString;

		// Token: 0x04002CC1 RID: 11457
		public const string DefaultTabString = "    ";
	}
}
