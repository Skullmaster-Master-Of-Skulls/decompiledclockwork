using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000246 RID: 582
	internal abstract class DependencyParser : BaseParser
	{
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00054934 File Offset: 0x00052B34
		protected PagesSection PagesConfig
		{
			get
			{
				return this._pagesConfig;
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0005493C File Offset: 0x00052B3C
		internal void Init(VirtualPath virtualPath)
		{
			base.CurrentVirtualPath = virtualPath;
			this._virtualPath = virtualPath;
			this._pagesConfig = MTConfigUtil.GetPagesConfig(virtualPath);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00054958 File Offset: 0x00052B58
		internal ICollection GetVirtualPathDependencies()
		{
			Thread currentThread = Thread.CurrentThread;
			CultureInfo currentCulture = currentThread.CurrentCulture;
			HttpRuntime.SetCurrentThreadCultureWithAssert(CultureInfo.InvariantCulture);
			try
			{
				try
				{
					this.PrepareParse();
					this.ParseFile();
				}
				finally
				{
					HttpRuntime.SetCurrentThreadCultureWithAssert(currentCulture);
				}
			}
			catch
			{
				throw;
			}
			return this._virtualPathDependencies;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000549B8 File Offset: 0x00052BB8
		protected void AddDependency(VirtualPath virtualPath)
		{
			virtualPath = base.ResolveVirtualPath(virtualPath);
			if (this._virtualPathDependencies == null)
			{
				this._virtualPathDependencies = new CaseInsensitiveStringSet();
			}
			this._virtualPathDependencies.Add(virtualPath.VirtualPathString);
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001AFB RID: 6907
		internal abstract string DefaultDirectiveName { get; }

		// Token: 0x06001AFC RID: 6908 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PrepareParse()
		{
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000549E7 File Offset: 0x00052BE7
		private void ParseFile()
		{
			this.ParseFile(null, this._virtualPath);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000549F8 File Offset: 0x00052BF8
		private void ParseFile(string physicalPath, VirtualPath virtualPath)
		{
			string o = (physicalPath != null) ? physicalPath : virtualPath.VirtualPathString;
			if (this._circularReferenceChecker.Contains(o))
			{
				throw new HttpException(SR.GetString("Circular_include"));
			}
			this._circularReferenceChecker.Add(o);
			try
			{
				if (physicalPath != null)
				{
					TextReader textReader;
					TextReader input = textReader = Util.ReaderFromFile(physicalPath, virtualPath);
					try
					{
						this.ParseReader(input);
						return;
					}
					finally
					{
						if (textReader != null)
						{
							((IDisposable)textReader).Dispose();
						}
					}
				}
				using (Stream stream = virtualPath.OpenFile())
				{
					TextReader input = Util.ReaderFromStream(stream, virtualPath);
					this.ParseReader(input);
				}
			}
			finally
			{
				this._circularReferenceChecker.Remove(o);
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00054AB4 File Offset: 0x00052CB4
		private void ParseReader(TextReader input)
		{
			this.ParseString(input.ReadToEnd());
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00054AC4 File Offset: 0x00052CC4
		private void ParseString(string text)
		{
			int num = 0;
			for (;;)
			{
				Match match;
				if ((match = BaseParser.textRegex.Match(text, num)).Success)
				{
					num = match.Index + match.Length;
				}
				if (num == text.Length)
				{
					return;
				}
				if ((match = BaseParser.directiveRegex.Match(text, num)).Success)
				{
					IDictionary dictionary = CollectionsUtil.CreateCaseInsensitiveSortedList();
					string directiveName = this.ProcessAttributes(match, dictionary);
					this.ProcessDirective(directiveName, dictionary);
					num = match.Index + match.Length;
				}
				else if ((match = BaseParser.includeRegex.Match(text, num)).Success)
				{
					this.ProcessServerInclude(match);
					num = match.Index + match.Length;
				}
				else if ((match = BaseParser.commentRegex.Match(text, num)).Success)
				{
					num = match.Index + match.Length;
				}
				else
				{
					int num2 = text.IndexOf("<%@", num, StringComparison.Ordinal);
					if (num2 == -1 || num2 == num)
					{
						break;
					}
					num = num2;
				}
				if (num == text.Length)
				{
					return;
				}
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00054BBC File Offset: 0x00052DBC
		private void ProcessServerInclude(Match match)
		{
			string value = match.Groups["pathtype"].Value;
			string value2 = match.Groups["filename"].Value;
			if (value2.Length == 0)
			{
				return;
			}
			string physicalPath = null;
			VirtualPath virtualPath;
			if (StringUtil.EqualsIgnoreCase(value, "file"))
			{
				if (UrlPath.IsAbsolutePhysicalPath(value2))
				{
					physicalPath = value2;
					virtualPath = base.CurrentVirtualPath;
				}
				else
				{
					virtualPath = base.ResolveVirtualPath(VirtualPath.Create(value2));
				}
			}
			else
			{
				if (!StringUtil.EqualsIgnoreCase(value, "virtual"))
				{
					return;
				}
				virtualPath = base.ResolveVirtualPath(VirtualPath.Create(value2));
			}
			VirtualPath virtualPath2 = this._virtualPath;
			try
			{
				this._virtualPath = virtualPath;
				this.ParseFile(physicalPath, virtualPath);
			}
			finally
			{
				this._virtualPath = virtualPath2;
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00054C80 File Offset: 0x00052E80
		internal virtual void ProcessDirective(string directiveName, IDictionary directive)
		{
			if (directiveName == null || StringUtil.EqualsIgnoreCase(directiveName, this.DefaultDirectiveName))
			{
				this.ProcessMainDirective(directive);
				return;
			}
			if (StringUtil.EqualsIgnoreCase(directiveName, "register"))
			{
				VirtualPath andRemoveVirtualPathAttribute = Util.GetAndRemoveVirtualPathAttribute(directive, "src");
				if (andRemoveVirtualPathAttribute != null)
				{
					this.AddDependency(andRemoveVirtualPathAttribute);
					return;
				}
			}
			else if (StringUtil.EqualsIgnoreCase(directiveName, "reference"))
			{
				VirtualPath andRemoveVirtualPathAttribute2 = Util.GetAndRemoveVirtualPathAttribute(directive, "virtualpath");
				if (andRemoveVirtualPathAttribute2 != null)
				{
					this.AddDependency(andRemoveVirtualPathAttribute2);
				}
				VirtualPath andRemoveVirtualPathAttribute3 = Util.GetAndRemoveVirtualPathAttribute(directive, "page");
				if (andRemoveVirtualPathAttribute3 != null)
				{
					this.AddDependency(andRemoveVirtualPathAttribute3);
				}
				VirtualPath andRemoveVirtualPathAttribute4 = Util.GetAndRemoveVirtualPathAttribute(directive, "control");
				if (andRemoveVirtualPathAttribute4 != null)
				{
					this.AddDependency(andRemoveVirtualPathAttribute4);
					return;
				}
			}
			else if (StringUtil.EqualsIgnoreCase(directiveName, "assembly"))
			{
				VirtualPath andRemoveVirtualPathAttribute5 = Util.GetAndRemoveVirtualPathAttribute(directive, "src");
				if (andRemoveVirtualPathAttribute5 != null)
				{
					this.AddDependency(andRemoveVirtualPathAttribute5);
				}
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00054D64 File Offset: 0x00052F64
		private void ProcessMainDirective(IDictionary mainDirective)
		{
			foreach (object obj in mainDirective)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string input = ((string)dictionaryEntry.Key).ToLower(CultureInfo.InvariantCulture);
				string name;
				string deviceName = Util.ParsePropertyDeviceFilter(input, out name);
				this.ProcessMainDirectiveAttribute(deviceName, name, (string)dictionaryEntry.Value);
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00054DEC File Offset: 0x00052FEC
		internal virtual void ProcessMainDirectiveAttribute(string deviceName, string name, string value)
		{
			if (name == "src")
			{
				string nonEmptyAttribute = Util.GetNonEmptyAttribute(name, value);
				this.AddDependency(VirtualPath.Create(nonEmptyAttribute));
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00054E1C File Offset: 0x0005301C
		private string ProcessAttributes(Match match, IDictionary attribs)
		{
			string text = null;
			CaptureCollection captures = match.Groups["attrname"].Captures;
			CaptureCollection captures2 = match.Groups["attrval"].Captures;
			CaptureCollection captures3 = match.Groups["equal"].Captures;
			for (int i = 0; i < captures.Count; i++)
			{
				string text2 = captures[i].ToString();
				string value = captures2[i].ToString();
				bool flag = captures3[i].ToString().Length > 0;
				if (text2 != null && !flag && text == null)
				{
					text = text2;
				}
				else
				{
					try
					{
						if (attribs != null)
						{
							attribs.Add(text2, value);
						}
					}
					catch (ArgumentException)
					{
					}
				}
			}
			return text;
		}

		// Token: 0x0400187C RID: 6268
		private VirtualPath _virtualPath;

		// Token: 0x0400187D RID: 6269
		private StringSet _virtualPathDependencies;

		// Token: 0x0400187E RID: 6270
		private StringSet _circularReferenceChecker = new CaseInsensitiveStringSet();

		// Token: 0x0400187F RID: 6271
		private PagesSection _pagesConfig;
	}
}
