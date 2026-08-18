using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x020011E7 RID: 4583
	public class SpellCheckHandlerNoSession : IHttpHandler
	{
		// Token: 0x17003D1A RID: 15642
		// (get) Token: 0x0600BD6B RID: 48491 RVA: 0x0029F634 File Offset: 0x0029D834
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600BD6C RID: 48492 RVA: 0x0029F638 File Offset: 0x0029D838
		public void ProcessRequest(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			if (!string.IsNullOrEmpty(request.QueryString["checkHandler"]))
			{
				response.ContentType = "text/html";
				response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" + Environment.NewLine + Environment.NewLine);
				response.Write("<html xmlns='http://www.w3.org/1999/xhtml'><head><title>HandlerCheckOK</title></head><body>HandlerCheckOK</body></html>");
				return;
			}
			response.ContentType = "application/x-javascript";
			string text = request.Form["DictionaryLanguage"];
			string text2 = request.Form["Configuration"];
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				throw new ArgumentException("Cannot read the configuration/dictionary language parameters!");
			}
			SpellDialogParameters spellDialogParameters = new SpellDialogParameters(DialogParameters.Deserialize(text2));
			SpellChecker checker = SpellCheckHandlerNoSession.CreateSpellChecker(text, spellDialogParameters);
			string text3 = request.Form["CommandName"];
			string text4 = ContentEncoder.Decode(request.Form["CommandArgument"].Replace("~", "%"));
			text4 = text4.Replace("<rslt/>&lt;", "<rslt/>    ").Replace("<rsgt/>&gt;", "<rsgt/>    ");
			string a;
			if ((a = text3) != null)
			{
				if (a == "SpellCheck")
				{
					SpellCheckHandlerNoSession.ProcessSpellCheckRequest(response, checker, text4);
					return;
				}
				if (a == "AddCustom")
				{
					SpellCheckHandlerNoSession.ProcessAddWordRequest(response, checker, text4);
					return;
				}
			}
			throw new ArgumentException("Unknown command!");
		}

		// Token: 0x0600BD6D RID: 48493 RVA: 0x0029F79C File Offset: 0x0029D99C
		private static void ProcessSpellCheckRequest(HttpResponse response, SpellChecker checker, string text)
		{
			checker.Text = text;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["badWords"] = checker.BadWordsJScript();
			dictionary["wordOffsets"] = checker.WordOffsetsJScript();
			string s = javaScriptSerializer.Serialize(dictionary);
			response.Write(s);
		}

		// Token: 0x0600BD6E RID: 48494 RVA: 0x0029F7F8 File Offset: 0x0029D9F8
		private static void ProcessAddWordRequest(HttpResponse response, SpellChecker checker, string word)
		{
			checker.AddToCustom(word);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["AddCustomWord"] = "success";
			string s = javaScriptSerializer.Serialize(dictionary);
			response.Write(s);
		}

		// Token: 0x0600BD6F RID: 48495 RVA: 0x0029F844 File Offset: 0x0029DA44
		private static SpellChecker CreateSpellChecker(string dictionaryLanguage, SpellDialogParameters spellDialogParameters)
		{
			return new SpellChecker(spellDialogParameters.DictionaryPath)
			{
				DictionaryLanguage = dictionaryLanguage,
				CustomDictionarySourceType = spellDialogParameters.CustomDictionarySourceTypeName,
				CustomAppendix = spellDialogParameters.CustomDictionarySuffix,
				EditDistance = spellDialogParameters.EditDistance,
				FragmentIgnoreOptions = spellDialogParameters.FragmentIgnoreOptions,
				SpellCheckProviderTypeName = spellDialogParameters.SpellCheckProviderTypeName,
				SpellCheckProvider = spellDialogParameters.SpellCheckProvider,
				WordIgnoreOptions = spellDialogParameters.WordIgnoreOptions
			};
		}

		// Token: 0x040031D1 RID: 12753
		public const string DefaultUrl = "Telerik.Web.UI.SpellCheckHandler.axd";
	}
}
