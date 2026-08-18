using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x02000725 RID: 1829
	public class IndentedTextWriter : TextWriter
	{
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x001610DB File Offset: 0x0015F2DB
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x001610E8 File Offset: 0x0015F2E8
		// (set) Token: 0x06004B28 RID: 19240 RVA: 0x001610F5 File Offset: 0x0015F2F5
		public override string NewLine
		{
			get
			{
				return this._writer.NewLine;
			}
			set
			{
				this._writer.NewLine = value;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x00161103 File Offset: 0x0015F303
		// (set) Token: 0x06004B2A RID: 19242 RVA: 0x0016110B File Offset: 0x0015F30B
		public int Indent
		{
			get
			{
				return this._indentLevel;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this._indentLevel = value;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x0016111B File Offset: 0x0015F31B
		public TextWriter InnerWriter
		{
			get
			{
				return this._writer;
			}
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x00161123 File Offset: 0x0015F323
		public IndentedTextWriter(TextWriter writer) : this(writer, "    ")
		{
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00161131 File Offset: 0x0015F331
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		public IndentedTextWriter(TextWriter writer, string tabString) : base(IndentedTextWriter.Culture)
		{
			this._writer = writer;
			this._tabString = tabString;
			this._indentLevel = 0;
			this._tabsPending = false;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x00161165 File Offset: 0x0015F365
		public override void Close()
		{
			this._writer.Close();
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x00161172 File Offset: 0x0015F372
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x0016117F File Offset: 0x0015F37F
		protected virtual void OutputTabs()
		{
			if (!this._tabsPending)
			{
				return;
			}
			this._writer.Write(this.CurrentIndentation());
			this._tabsPending = false;
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x001611A4 File Offset: 0x0015F3A4
		public virtual string CurrentIndentation()
		{
			if (this._indentLevel <= 0 || string.IsNullOrEmpty(this._tabString))
			{
				return string.Empty;
			}
			if (this._indentLevel == 1)
			{
				return this._tabString;
			}
			int num = this._indentLevel - 2;
			string text = (num < this._cachedIndents.Count) ? this._cachedIndents[num] : null;
			if (text == null)
			{
				text = this.BuildIndent(this._indentLevel);
				if (num == this._cachedIndents.Count)
				{
					this._cachedIndents.Add(text);
				}
				else
				{
					for (int i = this._cachedIndents.Count; i <= num; i++)
					{
						this._cachedIndents.Add(null);
					}
					this._cachedIndents[num] = text;
				}
			}
			return text;
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x00161260 File Offset: 0x0015F460
		private string BuildIndent(int numberOfIndents)
		{
			StringBuilder stringBuilder = new StringBuilder(numberOfIndents * this._tabString.Length);
			for (int i = 0; i < numberOfIndents; i++)
			{
				stringBuilder.Append(this._tabString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x0016129F File Offset: 0x0015F49F
		public override void Write(string value)
		{
			this.OutputTabs();
			this._writer.Write(value);
			if (value != null && (value.Equals("\r\n", StringComparison.Ordinal) || value.Equals("\n", StringComparison.Ordinal)))
			{
				this._tabsPending = true;
			}
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x001612D9 File Offset: 0x0015F4D9
		public override void Write(bool value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x001612ED File Offset: 0x0015F4ED
		public override void Write(char value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x00161301 File Offset: 0x0015F501
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this._writer.Write(buffer);
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x00161315 File Offset: 0x0015F515
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.Write(buffer, index, count);
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x0016132B File Offset: 0x0015F52B
		public override void Write(double value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x0016133F File Offset: 0x0015F53F
		public override void Write(float value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x00161353 File Offset: 0x0015F553
		public override void Write(int value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x00161367 File Offset: 0x0015F567
		public override void Write(long value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x0016137B File Offset: 0x0015F57B
		public override void Write(object value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x0016138F File Offset: 0x0015F58F
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0);
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x001613A4 File Offset: 0x0015F5A4
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0, arg1);
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x001613BA File Offset: 0x0015F5BA
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.Write(format, arg);
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x001613CF File Offset: 0x0015F5CF
		public void WriteLineNoTabs(string value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x001613DD File Offset: 0x0015F5DD
		public override void WriteLine(string value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x001613F8 File Offset: 0x0015F5F8
		public override void WriteLine()
		{
			this.OutputTabs();
			this._writer.WriteLine();
			this._tabsPending = true;
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x00161412 File Offset: 0x0015F612
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x0016142D File Offset: 0x0015F62D
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x00161448 File Offset: 0x0015F648
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer);
			this._tabsPending = true;
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x00161463 File Offset: 0x0015F663
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer, index, count);
			this._tabsPending = true;
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x00161480 File Offset: 0x0015F680
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x0016149B File Offset: 0x0015F69B
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x001614B6 File Offset: 0x0015F6B6
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x001614D1 File Offset: 0x0015F6D1
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x001614EC File Offset: 0x0015F6EC
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x00161507 File Offset: 0x0015F707
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0);
			this._tabsPending = true;
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x00161523 File Offset: 0x0015F723
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0, arg1);
			this._tabsPending = true;
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00161540 File Offset: 0x0015F740
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg);
			this._tabsPending = true;
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x0016155C File Offset: 0x0015F75C
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x04001B60 RID: 7008
		public const string DefaultTabString = "    ";

		// Token: 0x04001B61 RID: 7009
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "CultureInfo.InvariantCulture is readonly")]
		public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

		// Token: 0x04001B62 RID: 7010
		private readonly TextWriter _writer;

		// Token: 0x04001B63 RID: 7011
		private int _indentLevel;

		// Token: 0x04001B64 RID: 7012
		private bool _tabsPending;

		// Token: 0x04001B65 RID: 7013
		private readonly string _tabString;

		// Token: 0x04001B66 RID: 7014
		private readonly List<string> _cachedIndents = new List<string>();
	}
}
