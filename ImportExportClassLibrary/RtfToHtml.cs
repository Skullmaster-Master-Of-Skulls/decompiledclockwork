using System;
using System.Drawing.Imaging;
using Itenso.Rtf;
using Itenso.Rtf.Converter.Html;
using Itenso.Rtf.Converter.Image;
using Itenso.Rtf.Interpreter;
using Itenso.Rtf.Parser;
using Itenso.Rtf.Support;

namespace ImportExportClassLibrary
{
	// Token: 0x0200003C RID: 60
	public class RtfToHtml
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0001604C File Offset: 0x0001504C
		public static string ConvertRtfToHtml(string rtf)
		{
			RtfParserListenerStructureBuilder rtfParserListenerStructureBuilder = new RtfParserListenerStructureBuilder();
			new RtfParser(new IRtfParserListener[]
			{
				rtfParserListenerStructureBuilder
			})
			{
				IgnoreContentAfterRootGroup = true
			}.Parse(new RtfSource(rtf));
			IRtfGroup structureRoot = rtfParserListenerStructureBuilder.StructureRoot;
			RtfVisualImageAdapter imageAdapter = new RtfVisualImageAdapter(ImageFormat.Jpeg);
			IRtfDocument rtfDocument = RtfToHtml.InterpretRtf(structureRoot, imageAdapter);
			return RtfToHtml.ConvertHmtl2(rtfDocument, imageAdapter);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000160B0 File Offset: 0x000150B0
		private static IRtfDocument InterpretRtf(IRtfGroup rtfStructure, IRtfVisualImageAdapter imageAdapter)
		{
			RtfInterpreterListenerFileLogger rtfInterpreterListenerFileLogger = null;
			IRtfDocument result;
			try
			{
				RtfImageConverter rtfImageConverter = null;
				result = RtfInterpreterTool.BuildDoc(rtfStructure, new IRtfInterpreterListener[]
				{
					rtfInterpreterListenerFileLogger,
					rtfImageConverter
				});
			}
			catch (Exception ex)
			{
				if (rtfInterpreterListenerFileLogger != null)
				{
					rtfInterpreterListenerFileLogger.Dispose();
				}
				Console.WriteLine("error while interpreting rtf: " + ex.Message);
				return null;
			}
			return result;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00016118 File Offset: 0x00015118
		public static string ConvertHmtl2(IRtfDocument rtfDocument, IRtfVisualImageAdapter imageAdapter)
		{
			string result;
			try
			{
				RtfHtmlConvertSettings rtfHtmlConvertSettings = new RtfHtmlConvertSettings(imageAdapter);
				RtfHtmlConverter rtfHtmlConverter = new RtfHtmlConverter(rtfDocument, rtfHtmlConvertSettings);
				result = rtfHtmlConverter.Convert();
			}
			catch (Exception ex)
			{
				Console.WriteLine("error while converting to html: " + ex.Message);
				return null;
			}
			return result;
		}
	}
}
