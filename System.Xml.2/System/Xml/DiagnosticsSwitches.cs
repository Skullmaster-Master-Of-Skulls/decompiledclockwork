using System;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x0200006B RID: 107
	internal static class DiagnosticsSwitches
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000E994 File Offset: 0x0000CB94
		public static BooleanSwitch XmlSchemaContentModel
		{
			get
			{
				if (DiagnosticsSwitches.xmlSchemaContentModel == null)
				{
					DiagnosticsSwitches.xmlSchemaContentModel = new BooleanSwitch("XmlSchemaContentModel", "Enable tracing for the XmlSchema content model.");
				}
				return DiagnosticsSwitches.xmlSchemaContentModel;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		public static TraceSwitch XmlSchema
		{
			get
			{
				if (DiagnosticsSwitches.xmlSchema == null)
				{
					DiagnosticsSwitches.xmlSchema = new TraceSwitch("XmlSchema", "Enable tracing for the XmlSchema class.");
				}
				return DiagnosticsSwitches.xmlSchema;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		public static BooleanSwitch KeepTempFiles
		{
			get
			{
				if (DiagnosticsSwitches.keepTempFiles == null)
				{
					DiagnosticsSwitches.keepTempFiles = new BooleanSwitch("XmlSerialization.Compilation", "Keep XmlSerialization generated (temp) files.");
				}
				return DiagnosticsSwitches.keepTempFiles;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		public static BooleanSwitch PregenEventLog
		{
			get
			{
				if (DiagnosticsSwitches.pregenEventLog == null)
				{
					DiagnosticsSwitches.pregenEventLog = new BooleanSwitch("XmlSerialization.PregenEventLog", "Log failures while loading pre-generated XmlSerialization assembly.");
				}
				return DiagnosticsSwitches.pregenEventLog;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000EA34 File Offset: 0x0000CC34
		public static TraceSwitch XmlSerialization
		{
			get
			{
				if (DiagnosticsSwitches.xmlSerialization == null)
				{
					DiagnosticsSwitches.xmlSerialization = new TraceSwitch("XmlSerialization", "Enable tracing for the System.Xml.Serialization component.");
				}
				return DiagnosticsSwitches.xmlSerialization;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public static TraceSwitch XslTypeInference
		{
			get
			{
				if (DiagnosticsSwitches.xslTypeInference == null)
				{
					DiagnosticsSwitches.xslTypeInference = new TraceSwitch("XslTypeInference", "Enable tracing for the XSLT type inference algorithm.");
				}
				return DiagnosticsSwitches.xslTypeInference;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public static BooleanSwitch NonRecursiveTypeLoading
		{
			get
			{
				if (DiagnosticsSwitches.nonRecursiveTypeLoading == null)
				{
					DiagnosticsSwitches.nonRecursiveTypeLoading = new BooleanSwitch("XmlSerialization.NonRecursiveTypeLoading", "Turn on non-recursive algorithm generating XmlMappings for CLR types.");
				}
				return DiagnosticsSwitches.nonRecursiveTypeLoading;
			}
		}

		// Token: 0x040001B5 RID: 437
		private static volatile BooleanSwitch xmlSchemaContentModel;

		// Token: 0x040001B6 RID: 438
		private static volatile TraceSwitch xmlSchema;

		// Token: 0x040001B7 RID: 439
		private static volatile BooleanSwitch keepTempFiles;

		// Token: 0x040001B8 RID: 440
		private static volatile BooleanSwitch pregenEventLog;

		// Token: 0x040001B9 RID: 441
		private static volatile TraceSwitch xmlSerialization;

		// Token: 0x040001BA RID: 442
		private static volatile TraceSwitch xslTypeInference;

		// Token: 0x040001BB RID: 443
		private static volatile BooleanSwitch nonRecursiveTypeLoading;
	}
}
