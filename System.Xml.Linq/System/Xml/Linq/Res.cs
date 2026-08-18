using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Xml.Linq
{
	// Token: 0x02000032 RID: 50
	internal sealed class Res
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000B747 File Offset: 0x00009947
		internal Res()
		{
			this.resources = new ResourceManager("System.Xml.Linq", base.GetType().Assembly);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B76C File Offset: 0x0000996C
		private static Res GetLoader()
		{
			if (Res.loader == null)
			{
				Res value = new Res();
				Interlocked.CompareExchange<Res>(ref Res.loader, value, null);
			}
			return Res.loader;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B798 File Offset: 0x00009998
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B79B File Offset: 0x0000999B
		public static ResourceManager Resources
		{
			get
			{
				return Res.GetLoader().resources;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B7A8 File Offset: 0x000099A8
		public static string GetString(string name, params object[] args)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			string @string = res.resources.GetString(name, Res.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B828 File Offset: 0x00009A28
		public static string GetString(string name)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			return res.resources.GetString(name, Res.Culture);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B851 File Offset: 0x00009A51
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return Res.GetString(name);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B85C File Offset: 0x00009A5C
		public static object GetObject(string name)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			return res.resources.GetObject(name, Res.Culture);
		}

		// Token: 0x040000C5 RID: 197
		internal const string Argument_AddAttribute = "Argument_AddAttribute";

		// Token: 0x040000C6 RID: 198
		internal const string Argument_AddNode = "Argument_AddNode";

		// Token: 0x040000C7 RID: 199
		internal const string Argument_AddNonWhitespace = "Argument_AddNonWhitespace";

		// Token: 0x040000C8 RID: 200
		internal const string Argument_ConvertToString = "Argument_ConvertToString";

		// Token: 0x040000C9 RID: 201
		internal const string Argument_CreateNavigator = "Argument_CreateNavigator";

		// Token: 0x040000CA RID: 202
		internal const string Argument_InvalidExpandedName = "Argument_InvalidExpandedName";

		// Token: 0x040000CB RID: 203
		internal const string Argument_InvalidPIName = "Argument_InvalidPIName";

		// Token: 0x040000CC RID: 204
		internal const string Argument_InvalidPrefix = "Argument_InvalidPrefix";

		// Token: 0x040000CD RID: 205
		internal const string Argument_MustBeDerivedFrom = "Argument_MustBeDerivedFrom";

		// Token: 0x040000CE RID: 206
		internal const string Argument_NamespaceDeclarationPrefixed = "Argument_NamespaceDeclarationPrefixed";

		// Token: 0x040000CF RID: 207
		internal const string Argument_NamespaceDeclarationXml = "Argument_NamespaceDeclarationXml";

		// Token: 0x040000D0 RID: 208
		internal const string Argument_NamespaceDeclarationXmlns = "Argument_NamespaceDeclarationXmlns";

		// Token: 0x040000D1 RID: 209
		internal const string Argument_XObjectValue = "Argument_XObjectValue";

		// Token: 0x040000D2 RID: 210
		internal const string InvalidOperation_BadNodeType = "InvalidOperation_BadNodeType";

		// Token: 0x040000D3 RID: 211
		internal const string InvalidOperation_DocumentStructure = "InvalidOperation_DocumentStructure";

		// Token: 0x040000D4 RID: 212
		internal const string InvalidOperation_DuplicateAttribute = "InvalidOperation_DuplicateAttribute";

		// Token: 0x040000D5 RID: 213
		internal const string InvalidOperation_ExpectedEndOfFile = "InvalidOperation_ExpectedEndOfFile";

		// Token: 0x040000D6 RID: 214
		internal const string InvalidOperation_ExpectedInteractive = "InvalidOperation_ExpectedInteractive";

		// Token: 0x040000D7 RID: 215
		internal const string InvalidOperation_ExpectedNodeType = "InvalidOperation_ExpectedNodeType";

		// Token: 0x040000D8 RID: 216
		internal const string InvalidOperation_ExternalCode = "InvalidOperation_ExternalCode";

		// Token: 0x040000D9 RID: 217
		internal const string InvalidOperation_DeserializeInstance = "InvalidOperation_DeserializeInstance";

		// Token: 0x040000DA RID: 218
		internal const string InvalidOperation_MissingAncestor = "InvalidOperation_MissingAncestor";

		// Token: 0x040000DB RID: 219
		internal const string InvalidOperation_MissingParent = "InvalidOperation_MissingParent";

		// Token: 0x040000DC RID: 220
		internal const string InvalidOperation_MissingRoot = "InvalidOperation_MissingRoot";

		// Token: 0x040000DD RID: 221
		internal const string InvalidOperation_UnexpectedEvaluation = "InvalidOperation_UnexpectedEvaluation";

		// Token: 0x040000DE RID: 222
		internal const string InvalidOperation_UnexpectedNodeType = "InvalidOperation_UnexpectedNodeType";

		// Token: 0x040000DF RID: 223
		internal const string InvalidOperation_UnresolvedEntityReference = "InvalidOperation_UnresolvedEntityReference";

		// Token: 0x040000E0 RID: 224
		internal const string InvalidOperation_WriteAttribute = "InvalidOperation_WriteAttribute";

		// Token: 0x040000E1 RID: 225
		internal const string NotSupported_CheckValidity = "NotSupported_CheckValidity";

		// Token: 0x040000E2 RID: 226
		internal const string NotSupported_MoveToId = "NotSupported_MoveToId";

		// Token: 0x040000E3 RID: 227
		internal const string NotSupported_WriteBase64 = "NotSupported_WriteBase64";

		// Token: 0x040000E4 RID: 228
		internal const string NotSupported_WriteEntityRef = "NotSupported_WriteEntityRef";

		// Token: 0x040000E5 RID: 229
		private static Res loader;

		// Token: 0x040000E6 RID: 230
		private ResourceManager resources;
	}
}
