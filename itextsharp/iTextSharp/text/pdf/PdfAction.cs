using System;
using System.Collections.Generic;
using System.util;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.collection;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000217 RID: 535
	public class PdfAction : PdfDictionary
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x00075746 File Offset: 0x00074746
		public PdfAction()
		{
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0007574E File Offset: 0x0007474E
		public PdfAction(Uri url) : this(url.AbsoluteUri)
		{
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0007575C File Offset: 0x0007475C
		public PdfAction(Uri url, bool isMap) : this(url.AbsoluteUri, isMap)
		{
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0007576B File Offset: 0x0007476B
		public PdfAction(string url) : this(url, false)
		{
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00075775 File Offset: 0x00074775
		public PdfAction(string url, bool isMap)
		{
			base.Put(PdfName.S, PdfName.URI);
			base.Put(PdfName.URI, new PdfString(url));
			if (isMap)
			{
				base.Put(PdfName.ISMAP, PdfBoolean.PDFTRUE);
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x000757B1 File Offset: 0x000747B1
		internal PdfAction(PdfIndirectReference destination)
		{
			base.Put(PdfName.S, PdfName.GOTO);
			base.Put(PdfName.D, destination);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000757D5 File Offset: 0x000747D5
		public PdfAction(string filename, string name)
		{
			base.Put(PdfName.S, PdfName.GOTOR);
			base.Put(PdfName.F, new PdfString(filename));
			base.Put(PdfName.D, new PdfString(name));
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00075810 File Offset: 0x00074810
		public PdfAction(string filename, int page)
		{
			base.Put(PdfName.S, PdfName.GOTOR);
			base.Put(PdfName.F, new PdfString(filename));
			base.Put(PdfName.D, new PdfLiteral("[" + (page - 1) + " /FitH 10000]"));
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0007586C File Offset: 0x0007486C
		public PdfAction(int named)
		{
			base.Put(PdfName.S, PdfName.NAMED);
			switch (named)
			{
			case 1:
				base.Put(PdfName.N, PdfName.FIRSTPAGE);
				return;
			case 2:
				base.Put(PdfName.N, PdfName.PREVPAGE);
				return;
			case 3:
				base.Put(PdfName.N, PdfName.NEXTPAGE);
				return;
			case 4:
				base.Put(PdfName.N, PdfName.LASTPAGE);
				return;
			case 5:
				base.Put(PdfName.S, PdfName.JAVASCRIPT);
				base.Put(PdfName.JS, new PdfString("this.print(true);\r"));
				return;
			default:
				throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.named.action"));
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00075928 File Offset: 0x00074928
		public PdfAction(string application, string parameters, string operation, string defaultDir)
		{
			base.Put(PdfName.S, PdfName.LAUNCH);
			if (parameters == null && operation == null && defaultDir == null)
			{
				base.Put(PdfName.F, new PdfString(application));
				return;
			}
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.F, new PdfString(application));
			if (parameters != null)
			{
				pdfDictionary.Put(PdfName.P, new PdfString(parameters));
			}
			if (operation != null)
			{
				pdfDictionary.Put(PdfName.O, new PdfString(operation));
			}
			if (defaultDir != null)
			{
				pdfDictionary.Put(PdfName.D, new PdfString(defaultDir));
			}
			base.Put(PdfName.WIN, pdfDictionary);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x000759C8 File Offset: 0x000749C8
		public static PdfAction CreateLaunch(string application, string parameters, string operation, string defaultDir)
		{
			return new PdfAction(application, parameters, operation, defaultDir);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x000759D4 File Offset: 0x000749D4
		public static PdfAction Rendition(string file, PdfFileSpecification fs, string mimeType, PdfIndirectReference refi)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.RENDITION);
			pdfAction.Put(PdfName.R, new PdfRendition(file, fs, mimeType));
			pdfAction.Put(new PdfName("OP"), new PdfNumber(0));
			pdfAction.Put(new PdfName("AN"), refi);
			return pdfAction;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00075A34 File Offset: 0x00074A34
		public static PdfAction JavaScript(string code, PdfWriter writer, bool unicode)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.JAVASCRIPT);
			if (unicode && code.Length < 50)
			{
				pdfAction.Put(PdfName.JS, new PdfString(code, "UnicodeBig"));
			}
			else if (!unicode && code.Length < 100)
			{
				pdfAction.Put(PdfName.JS, new PdfString(code));
			}
			else
			{
				try
				{
					byte[] bytes = PdfEncodings.ConvertToBytes(code, unicode ? "UnicodeBig" : "PDF");
					PdfStream pdfStream = new PdfStream(bytes);
					pdfStream.FlateCompress(writer.CompressionLevel);
					pdfAction.Put(PdfName.JS, writer.AddToBody(pdfStream).IndirectReference);
				}
				catch
				{
					pdfAction.Put(PdfName.JS, new PdfString(code));
				}
			}
			return pdfAction;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00075B04 File Offset: 0x00074B04
		public static PdfAction JavaScript(string code, PdfWriter writer)
		{
			return PdfAction.JavaScript(code, writer, false);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00075B10 File Offset: 0x00074B10
		internal static PdfAction CreateHide(PdfObject obj, bool hide)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.HIDE);
			pdfAction.Put(PdfName.T, obj);
			if (!hide)
			{
				pdfAction.Put(PdfName.H, PdfBoolean.PDFFALSE);
			}
			return pdfAction;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00075B53 File Offset: 0x00074B53
		public static PdfAction CreateHide(PdfAnnotation annot, bool hide)
		{
			return PdfAction.CreateHide(annot.IndirectReference, hide);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00075B61 File Offset: 0x00074B61
		public static PdfAction CreateHide(string name, bool hide)
		{
			return PdfAction.CreateHide(new PdfString(name), hide);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00075B70 File Offset: 0x00074B70
		internal static PdfArray BuildArray(object[] names)
		{
			PdfArray pdfArray = new PdfArray();
			foreach (object obj in names)
			{
				if (obj is string)
				{
					pdfArray.Add(new PdfString((string)obj));
				}
				else
				{
					if (!(obj is PdfAnnotation))
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("the.array.must.contain.string.or.pdfannotation"));
					}
					pdfArray.Add(((PdfAnnotation)obj).IndirectReference);
				}
			}
			return pdfArray;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00075BDE File Offset: 0x00074BDE
		public static PdfAction CreateHide(object[] names, bool hide)
		{
			return PdfAction.CreateHide(PdfAction.BuildArray(names), hide);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00075BEC File Offset: 0x00074BEC
		public static PdfAction CreateSubmitForm(string file, object[] names, int flags)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.SUBMITFORM);
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.F, new PdfString(file));
			pdfDictionary.Put(PdfName.FS, PdfName.URL);
			pdfAction.Put(PdfName.F, pdfDictionary);
			if (names != null)
			{
				pdfAction.Put(PdfName.FIELDS, PdfAction.BuildArray(names));
			}
			pdfAction.Put(PdfName.FLAGS, new PdfNumber(flags));
			return pdfAction;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00075C68 File Offset: 0x00074C68
		public static PdfAction CreateResetForm(object[] names, int flags)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.RESETFORM);
			if (names != null)
			{
				pdfAction.Put(PdfName.FIELDS, PdfAction.BuildArray(names));
			}
			pdfAction.Put(PdfName.FLAGS, new PdfNumber(flags));
			return pdfAction;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00075CB4 File Offset: 0x00074CB4
		public static PdfAction CreateImportData(string file)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.IMPORTDATA);
			pdfAction.Put(PdfName.F, new PdfString(file));
			return pdfAction;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00075CEC File Offset: 0x00074CEC
		public void Next(PdfAction na)
		{
			PdfObject pdfObject = base.Get(PdfName.NEXT);
			if (pdfObject == null)
			{
				base.Put(PdfName.NEXT, na);
				return;
			}
			if (pdfObject.IsDictionary())
			{
				PdfArray pdfArray = new PdfArray(pdfObject);
				pdfArray.Add(na);
				base.Put(PdfName.NEXT, pdfArray);
				return;
			}
			((PdfArray)pdfObject).Add(na);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00075D48 File Offset: 0x00074D48
		public static PdfAction GotoLocalPage(int page, PdfDestination dest, PdfWriter writer)
		{
			PdfIndirectReference pageReference = writer.GetPageReference(page);
			dest.AddPage(pageReference);
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.GOTO);
			pdfAction.Put(PdfName.D, dest);
			return pdfAction;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00075D88 File Offset: 0x00074D88
		public static PdfAction GotoLocalPage(string dest, bool isName)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.GOTO);
			if (isName)
			{
				pdfAction.Put(PdfName.D, new PdfName(dest));
			}
			else
			{
				pdfAction.Put(PdfName.D, new PdfString(dest, null));
			}
			return pdfAction;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00075DD4 File Offset: 0x00074DD4
		public static PdfAction GotoRemotePage(string filename, string dest, bool isName, bool newWindow)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.F, new PdfString(filename));
			pdfAction.Put(PdfName.S, PdfName.GOTOR);
			if (isName)
			{
				pdfAction.Put(PdfName.D, new PdfName(dest));
			}
			else
			{
				pdfAction.Put(PdfName.D, new PdfString(dest, null));
			}
			if (newWindow)
			{
				pdfAction.Put(PdfName.NEWWINDOW, PdfBoolean.PDFTRUE);
			}
			return pdfAction;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00075E44 File Offset: 0x00074E44
		public static PdfAction GotoEmbedded(string filename, PdfTargetDictionary target, string dest, bool isName, bool newWindow)
		{
			if (isName)
			{
				return PdfAction.GotoEmbedded(filename, target, new PdfName(dest), newWindow);
			}
			return PdfAction.GotoEmbedded(filename, target, new PdfString(dest, null), newWindow);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00075E6C File Offset: 0x00074E6C
		public static PdfAction GotoEmbedded(string filename, PdfTargetDictionary target, PdfObject dest, bool newWindow)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.GOTOE);
			pdfAction.Put(PdfName.T, target);
			pdfAction.Put(PdfName.D, dest);
			pdfAction.Put(PdfName.NEWWINDOW, new PdfBoolean(newWindow));
			if (filename != null)
			{
				pdfAction.Put(PdfName.F, new PdfString(filename));
			}
			return pdfAction;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00075ED0 File Offset: 0x00074ED0
		public static PdfAction SetOCGstate(List<object> state, bool preserveRB)
		{
			PdfAction pdfAction = new PdfAction();
			pdfAction.Put(PdfName.S, PdfName.SETOCGSTATE);
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < state.Count; i++)
			{
				object obj = state[i];
				if (obj != null)
				{
					if (obj is PdfIndirectReference)
					{
						pdfArray.Add((PdfIndirectReference)obj);
					}
					else if (obj is PdfLayer)
					{
						pdfArray.Add(((PdfLayer)obj).Ref);
					}
					else if (obj is PdfName)
					{
						pdfArray.Add((PdfName)obj);
					}
					else
					{
						if (!(obj is string))
						{
							throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.type.was.passed.in.state.1", obj.GetType().ToString()));
						}
						string text = (string)obj;
						PdfName obj2;
						if (Util.EqualsIgnoreCase(text, "on"))
						{
							obj2 = PdfName.ON;
						}
						else if (Util.EqualsIgnoreCase(text, "off"))
						{
							obj2 = PdfName.OFF;
						}
						else
						{
							if (!Util.EqualsIgnoreCase(text, "toggle"))
							{
								throw new ArgumentException(MessageLocalization.GetComposedMessage("a.string.1.was.passed.in.state.only.on.off.and.toggle.are.allowed", text));
							}
							obj2 = PdfName.TOGGLE;
						}
						pdfArray.Add(obj2);
					}
				}
			}
			pdfAction.Put(PdfName.STATE, pdfArray);
			if (!preserveRB)
			{
				pdfAction.Put(PdfName.PRESERVERB, PdfBoolean.PDFFALSE);
			}
			return pdfAction;
		}

		// Token: 0x04000E22 RID: 3618
		public const int FIRSTPAGE = 1;

		// Token: 0x04000E23 RID: 3619
		public const int PREVPAGE = 2;

		// Token: 0x04000E24 RID: 3620
		public const int NEXTPAGE = 3;

		// Token: 0x04000E25 RID: 3621
		public const int LASTPAGE = 4;

		// Token: 0x04000E26 RID: 3622
		public const int PRINTDIALOG = 5;

		// Token: 0x04000E27 RID: 3623
		public const int SUBMIT_EXCLUDE = 1;

		// Token: 0x04000E28 RID: 3624
		public const int SUBMIT_INCLUDE_NO_VALUE_FIELDS = 2;

		// Token: 0x04000E29 RID: 3625
		public const int SUBMIT_HTML_FORMAT = 4;

		// Token: 0x04000E2A RID: 3626
		public const int SUBMIT_HTML_GET = 8;

		// Token: 0x04000E2B RID: 3627
		public const int SUBMIT_COORDINATES = 16;

		// Token: 0x04000E2C RID: 3628
		public const int SUBMIT_XFDF = 32;

		// Token: 0x04000E2D RID: 3629
		public const int SUBMIT_INCLUDE_APPEND_SAVES = 64;

		// Token: 0x04000E2E RID: 3630
		public const int SUBMIT_INCLUDE_ANNOTATIONS = 128;

		// Token: 0x04000E2F RID: 3631
		public const int SUBMIT_PDF = 256;

		// Token: 0x04000E30 RID: 3632
		public const int SUBMIT_CANONICAL_FORMAT = 512;

		// Token: 0x04000E31 RID: 3633
		public const int SUBMIT_EXCL_NON_USER_ANNOTS = 1024;

		// Token: 0x04000E32 RID: 3634
		public const int SUBMIT_EXCL_F_KEY = 2048;

		// Token: 0x04000E33 RID: 3635
		public const int SUBMIT_EMBED_FORM = 8196;

		// Token: 0x04000E34 RID: 3636
		public const int RESET_EXCLUDE = 1;
	}
}
