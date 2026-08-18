using System;
using System.Globalization;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000029 RID: 41
	public struct GeneratedClassContext
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00005C50 File Offset: 0x00003E50
		public GeneratedClassContext(string executeMethodName, string writeMethodName, string writeLiteralMethodName)
		{
			this = default(GeneratedClassContext);
			if (string.IsNullOrEmpty(executeMethodName))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"executeMethodName"
				}), "executeMethodName");
			}
			if (string.IsNullOrEmpty(writeMethodName))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"writeMethodName"
				}), "writeMethodName");
			}
			if (string.IsNullOrEmpty(writeLiteralMethodName))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"writeLiteralMethodName"
				}), "writeLiteralMethodName");
			}
			this.WriteMethodName = writeMethodName;
			this.WriteLiteralMethodName = writeLiteralMethodName;
			this.ExecuteMethodName = executeMethodName;
			this.WriteToMethodName = null;
			this.WriteLiteralToMethodName = null;
			this.TemplateTypeName = null;
			this.DefineSectionMethodName = null;
			this.LayoutPropertyName = GeneratedClassContext.DefaultLayoutPropertyName;
			this.WriteAttributeMethodName = GeneratedClassContext.DefaultWriteAttributeMethodName;
			this.WriteAttributeToMethodName = GeneratedClassContext.DefaultWriteAttributeToMethodName;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005D4C File Offset: 0x00003F4C
		public GeneratedClassContext(string executeMethodName, string writeMethodName, string writeLiteralMethodName, string writeToMethodName, string writeLiteralToMethodName, string templateTypeName)
		{
			this = new GeneratedClassContext(executeMethodName, writeMethodName, writeLiteralMethodName);
			this.WriteToMethodName = writeToMethodName;
			this.WriteLiteralToMethodName = writeLiteralToMethodName;
			this.TemplateTypeName = templateTypeName;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005D6F File Offset: 0x00003F6F
		public GeneratedClassContext(string executeMethodName, string writeMethodName, string writeLiteralMethodName, string writeToMethodName, string writeLiteralToMethodName, string templateTypeName, string defineSectionMethodName)
		{
			this = new GeneratedClassContext(executeMethodName, writeMethodName, writeLiteralMethodName, writeToMethodName, writeLiteralToMethodName, templateTypeName);
			this.DefineSectionMethodName = defineSectionMethodName;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005D88 File Offset: 0x00003F88
		public GeneratedClassContext(string executeMethodName, string writeMethodName, string writeLiteralMethodName, string writeToMethodName, string writeLiteralToMethodName, string templateTypeName, string defineSectionMethodName, string beginContextMethodName, string endContextMethodName)
		{
			this = new GeneratedClassContext(executeMethodName, writeMethodName, writeLiteralMethodName, writeToMethodName, writeLiteralToMethodName, templateTypeName, defineSectionMethodName);
			this.BeginContextMethodName = beginContextMethodName;
			this.EndContextMethodName = endContextMethodName;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005DAB File Offset: 0x00003FAB
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00005DB3 File Offset: 0x00003FB3
		public string WriteMethodName { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005DBC File Offset: 0x00003FBC
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public string WriteLiteralMethodName { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005DCD File Offset: 0x00003FCD
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005DD5 File Offset: 0x00003FD5
		public string WriteToMethodName { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005DDE File Offset: 0x00003FDE
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00005DE6 File Offset: 0x00003FE6
		public string WriteLiteralToMethodName { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005DEF File Offset: 0x00003FEF
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00005DF7 File Offset: 0x00003FF7
		public string ExecuteMethodName { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005E00 File Offset: 0x00004000
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00005E08 File Offset: 0x00004008
		public string BeginContextMethodName { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00005E11 File Offset: 0x00004011
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00005E19 File Offset: 0x00004019
		public string EndContextMethodName { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00005E22 File Offset: 0x00004022
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00005E2A File Offset: 0x0000402A
		public string LayoutPropertyName { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005E33 File Offset: 0x00004033
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00005E3B File Offset: 0x0000403B
		public string DefineSectionMethodName { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005E44 File Offset: 0x00004044
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00005E4C File Offset: 0x0000404C
		public string TemplateTypeName { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005E55 File Offset: 0x00004055
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00005E5D File Offset: 0x0000405D
		public string WriteAttributeMethodName { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005E66 File Offset: 0x00004066
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00005E6E File Offset: 0x0000406E
		public string WriteAttributeToMethodName { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005E77 File Offset: 0x00004077
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00005E7F File Offset: 0x0000407F
		public string ResolveUrlMethodName { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005E88 File Offset: 0x00004088
		public bool AllowSections
		{
			get
			{
				return !string.IsNullOrEmpty(this.DefineSectionMethodName);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005E98 File Offset: 0x00004098
		public bool AllowTemplates
		{
			get
			{
				return !string.IsNullOrEmpty(this.TemplateTypeName);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005EA8 File Offset: 0x000040A8
		public bool SupportsInstrumentation
		{
			get
			{
				return !string.IsNullOrEmpty(this.BeginContextMethodName) && !string.IsNullOrEmpty(this.EndContextMethodName);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005EC8 File Offset: 0x000040C8
		public override bool Equals(object obj)
		{
			if (!(obj is GeneratedClassContext))
			{
				return false;
			}
			GeneratedClassContext generatedClassContext = (GeneratedClassContext)obj;
			return string.Equals(this.DefineSectionMethodName, generatedClassContext.DefineSectionMethodName, StringComparison.Ordinal) && string.Equals(this.WriteMethodName, generatedClassContext.WriteMethodName, StringComparison.Ordinal) && string.Equals(this.WriteLiteralMethodName, generatedClassContext.WriteLiteralMethodName, StringComparison.Ordinal) && string.Equals(this.WriteToMethodName, generatedClassContext.WriteToMethodName, StringComparison.Ordinal) && string.Equals(this.WriteLiteralToMethodName, generatedClassContext.WriteLiteralToMethodName, StringComparison.Ordinal) && string.Equals(this.ExecuteMethodName, generatedClassContext.ExecuteMethodName, StringComparison.Ordinal) && string.Equals(this.TemplateTypeName, generatedClassContext.TemplateTypeName, StringComparison.Ordinal) && string.Equals(this.BeginContextMethodName, generatedClassContext.BeginContextMethodName, StringComparison.Ordinal) && string.Equals(this.EndContextMethodName, generatedClassContext.EndContextMethodName, StringComparison.Ordinal);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005FAC File Offset: 0x000041AC
		public override int GetHashCode()
		{
			return this.DefineSectionMethodName.GetHashCode() ^ this.WriteMethodName.GetHashCode() ^ this.WriteLiteralMethodName.GetHashCode() ^ this.WriteToMethodName.GetHashCode() ^ this.WriteLiteralToMethodName.GetHashCode() ^ this.ExecuteMethodName.GetHashCode() ^ this.TemplateTypeName.GetHashCode() ^ this.BeginContextMethodName.GetHashCode() ^ this.EndContextMethodName.GetHashCode();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006024 File Offset: 0x00004224
		public static bool operator ==(GeneratedClassContext left, GeneratedClassContext right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006039 File Offset: 0x00004239
		public static bool operator !=(GeneratedClassContext left, GeneratedClassContext right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000063 RID: 99
		public static readonly string DefaultWriteMethodName = "Write";

		// Token: 0x04000064 RID: 100
		public static readonly string DefaultWriteLiteralMethodName = "WriteLiteral";

		// Token: 0x04000065 RID: 101
		public static readonly string DefaultExecuteMethodName = "Execute";

		// Token: 0x04000066 RID: 102
		public static readonly string DefaultLayoutPropertyName = "Layout";

		// Token: 0x04000067 RID: 103
		public static readonly string DefaultWriteAttributeMethodName = "WriteAttribute";

		// Token: 0x04000068 RID: 104
		public static readonly string DefaultWriteAttributeToMethodName = "WriteAttributeTo";

		// Token: 0x04000069 RID: 105
		public static readonly GeneratedClassContext Default = new GeneratedClassContext(GeneratedClassContext.DefaultExecuteMethodName, GeneratedClassContext.DefaultWriteMethodName, GeneratedClassContext.DefaultWriteLiteralMethodName);
	}
}
