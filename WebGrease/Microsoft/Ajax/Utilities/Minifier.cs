using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000D9 RID: 217
	public class Minifier
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x000426A9 File Offset: 0x000408A9
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x000426B1 File Offset: 0x000408B1
		public int WarningLevel { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x000426BA File Offset: 0x000408BA
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x000426C2 File Offset: 0x000408C2
		public string FileName { get; set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000426CB File Offset: 0x000408CB
		public ICollection<ContextError> ErrorList
		{
			get
			{
				return this.m_errorList;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000426D4 File Offset: 0x000408D4
		public ICollection<string> Errors
		{
			get
			{
				List<string> list = new List<string>(this.ErrorList.Count);
				foreach (ContextError contextError in this.ErrorList)
				{
					list.Add(contextError.ToString());
				}
				return list;
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00042738 File Offset: 0x00040938
		public string MinifyJavaScript(string source)
		{
			return this.MinifyJavaScript(source, new CodeSettings());
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00042748 File Offset: 0x00040948
		public string MinifyJavaScript(string source, CodeSettings codeSettings)
		{
			string result = string.Empty;
			this.m_errorList = new List<ContextError>();
			JSParser jsparser = new JSParser();
			jsparser.CompilerError += this.OnJavaScriptError;
			try
			{
				bool flag = codeSettings != null && codeSettings.PreprocessOnly;
				StringBuilder stringBuilder = new StringBuilder();
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
				{
					if (flag)
					{
						jsparser.EchoWriter = stringWriter;
					}
					Block block = jsparser.Parse(new DocumentContext(source)
					{
						FileContext = this.FileName
					}, codeSettings);
					if (block != null && !flag)
					{
						if (codeSettings != null && codeSettings.Format == JavaScriptFormat.JSON)
						{
							if (!JSONOutputVisitor.Apply(stringWriter, block, codeSettings))
							{
								this.m_errorList.Add(new ContextError
								{
									Severity = 0,
									File = this.FileName,
									Message = CommonStrings.InvalidJSONOutput
								});
							}
						}
						else
						{
							OutputVisitor.Apply(stringWriter, block, codeSettings);
						}
					}
				}
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				this.m_errorList.Add(new ContextError
				{
					Severity = 0,
					File = this.FileName,
					Message = ex.Message
				});
				throw;
			}
			return result;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00042898 File Offset: 0x00040A98
		public string MinifyStyleSheet(string source)
		{
			return this.MinifyStyleSheet(source, new CssSettings(), new CodeSettings());
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000428AB File Offset: 0x00040AAB
		public string MinifyStyleSheet(string source, CssSettings settings)
		{
			return this.MinifyStyleSheet(source, settings, new CodeSettings());
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000428BC File Offset: 0x00040ABC
		public string MinifyStyleSheet(string source, CssSettings settings, CodeSettings scriptSettings)
		{
			string result = string.Empty;
			this.m_errorList = new List<ContextError>();
			CssParser cssParser = new CssParser();
			cssParser.FileContext = this.FileName;
			if (settings != null)
			{
				cssParser.Settings = settings;
			}
			if (scriptSettings != null)
			{
				cssParser.JSSettings = scriptSettings;
			}
			cssParser.CssError += this.OnCssError;
			try
			{
				result = cssParser.Parse(source);
			}
			catch (Exception ex)
			{
				this.m_errorList.Add(new ContextError
				{
					Severity = 0,
					File = this.FileName,
					Message = ex.Message
				});
				throw;
			}
			return result;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00042964 File Offset: 0x00040B64
		private void OnCssError(object sender, ContextErrorEventArgs e)
		{
			ContextError error = e.Error;
			if (error.Severity <= this.WarningLevel)
			{
				this.m_errorList.Add(error);
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00042994 File Offset: 0x00040B94
		private void OnJavaScriptError(object sender, ContextErrorEventArgs e)
		{
			ContextError error = e.Error;
			if (error.Severity <= this.WarningLevel)
			{
				this.m_errorList.Add(error);
			}
		}

		// Token: 0x0400058D RID: 1421
		private List<ContextError> m_errorList;
	}
}
