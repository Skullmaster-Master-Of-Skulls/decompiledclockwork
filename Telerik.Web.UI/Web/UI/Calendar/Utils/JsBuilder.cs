using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar.Utils
{
	// Token: 0x02001013 RID: 4115
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class JsBuilder
	{
		// Token: 0x0600A1E0 RID: 41440 RVA: 0x0023F8F2 File Offset: 0x0023DAF2
		public JsBuilder()
		{
			this.jsStream = new StringBuilder(string.Empty);
			this.outputWriter = new HtmlTextWriter(new StringWriter(this.jsStream), "    ");
		}

		// Token: 0x0600A1E1 RID: 41441 RVA: 0x0023F928 File Offset: 0x0023DB28
		public static string EncodeJsString(string inputString)
		{
			StringBuilder stringBuilder = new StringBuilder(inputString);
			stringBuilder.Replace("\"", "\\\"");
			return stringBuilder.ToString();
		}

		// Token: 0x17003339 RID: 13113
		// (get) Token: 0x0600A1E2 RID: 41442 RVA: 0x0023F953 File Offset: 0x0023DB53
		// (set) Token: 0x0600A1E3 RID: 41443 RVA: 0x0023F960 File Offset: 0x0023DB60
		public string ScriptCode
		{
			get
			{
				return this.jsStream.ToString();
			}
			set
			{
				this.jsStream.Remove(0, this.jsStream.Length);
				this.jsStream.Append(value);
			}
		}

		// Token: 0x1700333A RID: 13114
		// (get) Token: 0x0600A1E4 RID: 41444 RVA: 0x0023F988 File Offset: 0x0023DB88
		public string ScriptElement
		{
			get
			{
				string empty = string.Empty;
				return string.Concat(new string[]
				{
					"<script type=\"text/javascript\">",
					this.outputWriter.NewLine,
					this.jsStream.ToString(),
					"</script>",
					this.outputWriter.NewLine
				});
			}
		}

		// Token: 0x0600A1E5 RID: 41445 RVA: 0x0023F9E4 File Offset: 0x0023DBE4
		public void WriteJsLine(string lineContent)
		{
			this.outputWriter.WriteLineNoTabs(lineContent);
		}

		// Token: 0x0600A1E6 RID: 41446 RVA: 0x0023F9F2 File Offset: 0x0023DBF2
		public void WriteJs(string literalContent)
		{
			this.outputWriter.Write(literalContent);
		}

		// Token: 0x0600A1E7 RID: 41447 RVA: 0x0023FA00 File Offset: 0x0023DC00
		public override string ToString()
		{
			return this.jsStream.ToString();
		}

		// Token: 0x0600A1E8 RID: 41448 RVA: 0x0023FA10 File Offset: 0x0023DC10
		public void DeclareJsVariable(string variableName, string variableContent)
		{
			this.outputWriter.Write("window[\"");
			this.outputWriter.Write(variableName, variableContent);
			this.outputWriter.Write("\"]");
			this.outputWriter.Write(" = ");
			this.outputWriter.Write(variableContent);
			this.outputWriter.Write(";\n");
			this.outputWriter.WriteLine();
		}

		// Token: 0x0600A1E9 RID: 41449 RVA: 0x0023FA84 File Offset: 0x0023DC84
		public void DeclareJsObjectVariable(string variableName, string objectClass, string variableContent)
		{
			this.outputWriter.Write("window[\"");
			this.outputWriter.Write(variableName);
			this.outputWriter.Write("\"]");
			this.outputWriter.Write(" = ");
			this.outputWriter.Write(" new ");
			this.outputWriter.Write(objectClass);
			this.outputWriter.Write("(");
			this.outputWriter.Write(variableContent);
			this.outputWriter.Write(")");
			this.outputWriter.Write(";\n");
			this.outputWriter.WriteLine();
		}

		// Token: 0x04002D08 RID: 11528
		private const string JsOpenTag = "<script type=\"text/javascript\">";

		// Token: 0x04002D09 RID: 11529
		private const string JsCloseTag = "</script>";

		// Token: 0x04002D0A RID: 11530
		private StringBuilder jsStream;

		// Token: 0x04002D0B RID: 11531
		private HtmlTextWriter outputWriter;
	}
}
