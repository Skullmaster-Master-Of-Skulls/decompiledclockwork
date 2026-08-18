using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TechnoPro.Common.DynamicCompiler
{
	// Token: 0x02000003 RID: 3
	public class CustomCompilerEnvironment
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000026BC File Offset: 0x000008BC
		public CustomCompilerEnvironment(eCustomCompilerType compilerType, string compilerTypeSecondary = "")
		{
			this.CompilerType = compilerType;
			this.CodeNamespace = "ClockWorkDynamicCSharp";
			this.CodeClassName = "CSharp";
			this.CompilerTypeSecondary = (compilerTypeSecondary ?? "");
			this.DefaultImports = new List<string>();
			this.DefaultUsings = new List<string>();
			this.SetupDefaultUsingsAndImports(compilerType);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002722 File Offset: 0x00000922
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000272A File Offset: 0x0000092A
		public string CompilerTypeSecondary { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x00002734 File Offset: 0x00000934
		private void SetupDefaultUsingsAndImports(eCustomCompilerType compilerType)
		{
			CustomCompilerTypeAttribute attribute = CustomCompilerTypeAttribute.GetAttribute(compilerType);
			bool flag = attribute == null;
			if (!flag)
			{
				bool flag2 = attribute.DefaultImports != null && attribute.DefaultImports.Length != 0;
				if (flag2)
				{
					this.DefaultImports = attribute.DefaultImports.ToList<string>();
				}
				bool flag3 = attribute.DefaultUsings != null && attribute.DefaultUsings.Length != 0;
				if (flag3)
				{
					this.DefaultUsings = attribute.DefaultUsings;
				}
				this.ConstructorCode = attribute.ConstructorCode;
				this.PropertiesCode = attribute.PropertiesCode;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000027BE File Offset: 0x000009BE
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000027C6 File Offset: 0x000009C6
		public eCustomCompilerType CompilerType { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000027D0 File Offset: 0x000009D0
		public string CompilerTypeString
		{
			get
			{
				return this.CompilerType.ToString();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000027F6 File Offset: 0x000009F6
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000027FE File Offset: 0x000009FE
		public string CodeNamespace { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002807 File Offset: 0x00000A07
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000280F File Offset: 0x00000A0F
		public string CodeClassName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002818 File Offset: 0x00000A18
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002820 File Offset: 0x00000A20
		public IList<string> DefaultImports { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002829 File Offset: 0x00000A29
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002831 File Offset: 0x00000A31
		public IList<string> DefaultUsings { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000283A File Offset: 0x00000A3A
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002842 File Offset: 0x00000A42
		public string ConstructorCode { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000284B File Offset: 0x00000A4B
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002853 File Offset: 0x00000A53
		public string PropertiesCode { get; set; }

		// Token: 0x0600001F RID: 31 RVA: 0x0000285C File Offset: 0x00000A5C
		public string GetDefaultCode(Type returnType, Type parametersType, out IList<string> imports)
		{
			imports = (this.DefaultImports ?? new List<string>());
			string returnNamespace = (returnType == null) ? null : returnType.Namespace;
			string passNamespace = (parametersType == null) ? null : parametersType.Namespace;
			bool flag = !string.IsNullOrEmpty(returnNamespace) && this.DefaultImports.FirstOrDefault((string g) => g.Equals(returnNamespace)) != null;
			string text;
			if (flag)
			{
				text = returnType.Name;
			}
			else
			{
				text = ((returnType == null) ? "" : returnType.FullName);
			}
			bool flag2 = !string.IsNullOrEmpty(passNamespace) && this.DefaultImports.FirstOrDefault((string g) => g.Equals(passNamespace)) != null;
			string text2;
			if (flag2)
			{
				text2 = parametersType.Name;
			}
			else
			{
				text2 = ((parametersType == null) ? "" : parametersType.FullName);
			}
			string text3 = string.IsNullOrEmpty(this.ConstructorCode) ? ((returnType == null) ? "" : "return null;") : this.ConstructorCode;
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(text3))
			{
				string value;
				while ((value = stringReader.ReadLine()) != null)
				{
					bool flag3 = string.IsNullOrEmpty(value);
					if (!flag3)
					{
						stringBuilder.Append("            ");
						stringBuilder.Append(value);
						stringBuilder.AppendLine();
					}
				}
			}
			text3 = stringBuilder.ToString();
			string text4 = this.PropertiesCode ?? "";
			stringBuilder = new StringBuilder();
			using (StringReader stringReader2 = new StringReader(text4))
			{
				string value2;
				while ((value2 = stringReader2.ReadLine()) != null)
				{
					bool flag4 = string.IsNullOrEmpty(value2);
					if (!flag4)
					{
						stringBuilder.Append("         ");
						stringBuilder.Append(value2);
						stringBuilder.AppendLine();
					}
				}
			}
			text4 = stringBuilder.ToString();
			string format = "{0}\r\nnamespace {1}\r\n{2}\r\n    public class {3}\r\n    {2}\r\n{4}\r\n        public {5} {6}({7})\r\n        {2}\r\n{8}\r\n        {9}\r\n    {9}\r\n{9}";
			object[] array = new object[10];
			int num = 0;
			object obj;
			if (this.DefaultUsings != null && this.DefaultUsings.Count >= 1)
			{
				obj = "#region usings\r\n\r\n" + string.Join("\r\n", this.DefaultUsings.ToList<string>().ConvertAll<string>((string g) => "using " + g + ";").ToArray()) + "\r\n\r\n#endregion\r\n";
			}
			else
			{
				obj = "";
			}
			array[num] = obj;
			array[1] = (this.CodeNamespace ?? "");
			array[2] = "{";
			array[3] = (this.CodeClassName ?? "");
			array[4] = text4;
			array[5] = (string.IsNullOrEmpty(text) ? "void" : text);
			array[6] = "CustomEntry";
			array[7] = (string.IsNullOrEmpty(text2) ? "" : (text2 + " args"));
			array[8] = text3;
			array[9] = "}";
			return string.Format(format, array);
		}
	}
}
