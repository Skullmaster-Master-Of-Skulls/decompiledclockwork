using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BarcodeLib
{
	// Token: 0x02000008 RID: 8
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[XmlRoot("BarcodeXML")]
	[HelpKeyword("vs.data.DataSet")]
	[Serializable]
	public class BarcodeXML : DataSet
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003B6C File Offset: 0x00001D6C
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public BarcodeXML()
		{
			base.BeginInit();
			this.InitClass();
			CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.SchemaChanged);
			base.Tables.CollectionChanged += value;
			base.Relations.CollectionChanged += value;
			base.EndInit();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003BC0 File Offset: 0x00001DC0
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected BarcodeXML(SerializationInfo info, StreamingContext context) : base(info, context, false)
		{
			if (base.IsBinarySerialized(info, context))
			{
				this.InitVars(false);
				CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.SchemaChanged);
				this.Tables.CollectionChanged += value;
				this.Relations.CollectionChanged += value;
				return;
			}
			string s = (string)info.GetValue("XmlSchema", typeof(string));
			if (base.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
			{
				DataSet dataSet = new DataSet();
				dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
				if (dataSet.Tables["Barcode"] != null)
				{
					base.Tables.Add(new BarcodeXML.BarcodeDataTable(dataSet.Tables["Barcode"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				base.Merge(dataSet, false, MissingSchemaAction.Add);
				this.InitVars();
			}
			else
			{
				base.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
			}
			base.GetSerializationData(info, context);
			CollectionChangeEventHandler value2 = new CollectionChangeEventHandler(this.SchemaChanged);
			base.Tables.CollectionChanged += value2;
			this.Relations.CollectionChanged += value2;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003D1D File Offset: 0x00001F1D
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BarcodeXML.BarcodeDataTable Barcode
		{
			get
			{
				return this.tableBarcode;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003D25 File Offset: 0x00001F25
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00003D2D File Offset: 0x00001F2D
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return this._schemaSerializationMode;
			}
			set
			{
				this._schemaSerializationMode = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003D36 File Offset: 0x00001F36
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DataTableCollection Tables
		{
			get
			{
				return base.Tables;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003D3E File Offset: 0x00001F3E
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DataRelationCollection Relations
		{
			get
			{
				return base.Relations;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003D46 File Offset: 0x00001F46
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void InitializeDerivedDataSet()
		{
			base.BeginInit();
			this.InitClass();
			base.EndInit();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003D5A File Offset: 0x00001F5A
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public override DataSet Clone()
		{
			BarcodeXML barcodeXML = (BarcodeXML)base.Clone();
			barcodeXML.InitVars();
			barcodeXML.SchemaSerializationMode = this.SchemaSerializationMode;
			return barcodeXML;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003D79 File Offset: 0x00001F79
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override bool ShouldSerializeTables()
		{
			return false;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003D79 File Offset: 0x00001F79
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override bool ShouldSerializeRelations()
		{
			return false;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003D7C File Offset: 0x00001F7C
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void ReadXmlSerializable(XmlReader reader)
		{
			if (base.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
			{
				this.Reset();
				DataSet dataSet = new DataSet();
				dataSet.ReadXml(reader);
				if (dataSet.Tables["Barcode"] != null)
				{
					base.Tables.Add(new BarcodeXML.BarcodeDataTable(dataSet.Tables["Barcode"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				base.Merge(dataSet, false, MissingSchemaAction.Add);
				this.InitVars();
				return;
			}
			base.ReadXml(reader);
			this.InitVars();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003E44 File Offset: 0x00002044
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override XmlSchema GetSchemaSerializable()
		{
			MemoryStream memoryStream = new MemoryStream();
			base.WriteXmlSchema(new XmlTextWriter(memoryStream, null));
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003E78 File Offset: 0x00002078
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars()
		{
			this.InitVars(true);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003E81 File Offset: 0x00002081
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars(bool initTable)
		{
			this.tableBarcode = (BarcodeXML.BarcodeDataTable)base.Tables["Barcode"];
			if (initTable && this.tableBarcode != null)
			{
				this.tableBarcode.InitVars();
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003EB4 File Offset: 0x000020B4
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void InitClass()
		{
			base.DataSetName = "BarcodeXML";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/BarcodeXML.xsd";
			base.EnforceConstraints = true;
			this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			this.tableBarcode = new BarcodeXML.BarcodeDataTable();
			base.Tables.Add(this.tableBarcode);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003D79 File Offset: 0x00001F79
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeBarcode()
		{
			return false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003F0C File Offset: 0x0000210C
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Remove)
			{
				this.InitVars();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003F20 File Offset: 0x00002120
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
		{
			BarcodeXML barcodeXML = new BarcodeXML();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = barcodeXML.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = barcodeXML.GetSchemaSerializable();
			if (xs.Contains(schemaSerializable.TargetNamespace))
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				try
				{
					schemaSerializable.Write(memoryStream);
					foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
					{
						XmlSchema xmlSchema = (XmlSchema)obj;
						memoryStream2.SetLength(0L);
						xmlSchema.Write(memoryStream2);
						if (memoryStream.Length == memoryStream2.Length)
						{
							memoryStream.Position = 0L;
							memoryStream2.Position = 0L;
							while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
							{
							}
							if (memoryStream.Position == memoryStream.Length)
							{
								return xmlSchemaComplexType;
							}
						}
					}
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
					if (memoryStream2 != null)
					{
						memoryStream2.Close();
					}
				}
			}
			xs.Add(schemaSerializable);
			return xmlSchemaComplexType;
		}

		// Token: 0x04000050 RID: 80
		private BarcodeXML.BarcodeDataTable tableBarcode;

		// Token: 0x04000051 RID: 81
		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x060000C5 RID: 197
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void BarcodeRowChangeEventHandler(object sender, BarcodeXML.BarcodeRowChangeEvent e);

		// Token: 0x02000023 RID: 35
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class BarcodeDataTable : DataTable, IEnumerable
		{
			// Token: 0x060000C8 RID: 200 RVA: 0x0000F574 File Offset: 0x0000D774
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public BarcodeDataTable()
			{
				base.TableName = "Barcode";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x0000F59C File Offset: 0x0000D79C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal BarcodeDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x0000F644 File Offset: 0x0000D844
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected BarcodeDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000CB RID: 203 RVA: 0x0000F654 File Offset: 0x0000D854
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn TypeColumn
			{
				get
				{
					return this.columnType;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000CC RID: 204 RVA: 0x0000F65C File Offset: 0x0000D85C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn RawDataColumn
			{
				get
				{
					return this.columnRawData;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000CD RID: 205 RVA: 0x0000F664 File Offset: 0x0000D864
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn EncodedValueColumn
			{
				get
				{
					return this.columnEncodedValue;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000CE RID: 206 RVA: 0x0000F66C File Offset: 0x0000D86C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn EncodingTimeColumn
			{
				get
				{
					return this.columnEncodingTime;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000CF RID: 207 RVA: 0x0000F674 File Offset: 0x0000D874
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn IncludeLabelColumn
			{
				get
				{
					return this.columnIncludeLabel;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000F67C File Offset: 0x0000D87C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ForecolorColumn
			{
				get
				{
					return this.columnForecolor;
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000F684 File Offset: 0x0000D884
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn BackcolorColumn
			{
				get
				{
					return this.columnBackcolor;
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000F68C File Offset: 0x0000D88C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CountryAssigningManufacturingCodeColumn
			{
				get
				{
					return this.columnCountryAssigningManufacturingCode;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000F694 File Offset: 0x0000D894
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ImageWidthColumn
			{
				get
				{
					return this.columnImageWidth;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000F69C File Offset: 0x0000D89C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ImageHeightColumn
			{
				get
				{
					return this.columnImageHeight;
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ImageColumn
			{
				get
				{
					return this.columnImage;
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000F6AC File Offset: 0x0000D8AC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn RotateFlipTypeColumn
			{
				get
				{
					return this.columnRotateFlipType;
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn LabelPositionColumn
			{
				get
				{
					return this.columnLabelPosition;
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000F6BC File Offset: 0x0000D8BC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn AlignmentColumn
			{
				get
				{
					return this.columnAlignment;
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn LabelFontColumn
			{
				get
				{
					return this.columnLabelFont;
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000DA RID: 218 RVA: 0x0000F6CC File Offset: 0x0000D8CC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ImageFormatColumn
			{
				get
				{
					return this.columnImageFormat;
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000DB RID: 219 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[Browsable(false)]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x1700004B RID: 75
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public BarcodeXML.BarcodeRow this[int index]
			{
				get
				{
					return (BarcodeXML.BarcodeRow)base.Rows[index];
				}
			}

			// Token: 0x14000001 RID: 1
			// (add) Token: 0x060000DD RID: 221 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
			// (remove) Token: 0x060000DE RID: 222 RVA: 0x0000F72C File Offset: 0x0000D92C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event BarcodeXML.BarcodeRowChangeEventHandler BarcodeRowChanging;

			// Token: 0x14000002 RID: 2
			// (add) Token: 0x060000DF RID: 223 RVA: 0x0000F764 File Offset: 0x0000D964
			// (remove) Token: 0x060000E0 RID: 224 RVA: 0x0000F79C File Offset: 0x0000D99C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event BarcodeXML.BarcodeRowChangeEventHandler BarcodeRowChanged;

			// Token: 0x14000003 RID: 3
			// (add) Token: 0x060000E1 RID: 225 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
			// (remove) Token: 0x060000E2 RID: 226 RVA: 0x0000F80C File Offset: 0x0000DA0C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event BarcodeXML.BarcodeRowChangeEventHandler BarcodeRowDeleting;

			// Token: 0x14000004 RID: 4
			// (add) Token: 0x060000E3 RID: 227 RVA: 0x0000F844 File Offset: 0x0000DA44
			// (remove) Token: 0x060000E4 RID: 228 RVA: 0x0000F87C File Offset: 0x0000DA7C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event BarcodeXML.BarcodeRowChangeEventHandler BarcodeRowDeleted;

			// Token: 0x060000E5 RID: 229 RVA: 0x0000F8B1 File Offset: 0x0000DAB1
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddBarcodeRow(BarcodeXML.BarcodeRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public BarcodeXML.BarcodeRow AddBarcodeRow(string Type, string RawData, string EncodedValue, double EncodingTime, bool IncludeLabel, string Forecolor, string Backcolor, string CountryAssigningManufacturingCode, int ImageWidth, int ImageHeight, string Image, RotateFlipType RotateFlipType, int LabelPosition, int Alignment, string LabelFont, string ImageFormat)
			{
				BarcodeXML.BarcodeRow barcodeRow = (BarcodeXML.BarcodeRow)base.NewRow();
				object[] itemArray = new object[]
				{
					Type,
					RawData,
					EncodedValue,
					EncodingTime,
					IncludeLabel,
					Forecolor,
					Backcolor,
					CountryAssigningManufacturingCode,
					ImageWidth,
					ImageHeight,
					Image,
					RotateFlipType,
					LabelPosition,
					Alignment,
					LabelFont,
					ImageFormat
				};
				barcodeRow.ItemArray = itemArray;
				base.Rows.Add(barcodeRow);
				return barcodeRow;
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x0000F96C File Offset: 0x0000DB6C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public virtual IEnumerator GetEnumerator()
			{
				return base.Rows.GetEnumerator();
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x0000F979 File Offset: 0x0000DB79
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				BarcodeXML.BarcodeDataTable barcodeDataTable = (BarcodeXML.BarcodeDataTable)base.Clone();
				barcodeDataTable.InitVars();
				return barcodeDataTable;
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x0000F98C File Offset: 0x0000DB8C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new BarcodeXML.BarcodeDataTable();
			}

			// Token: 0x060000EA RID: 234 RVA: 0x0000F994 File Offset: 0x0000DB94
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal void InitVars()
			{
				this.columnType = base.Columns["Type"];
				this.columnRawData = base.Columns["RawData"];
				this.columnEncodedValue = base.Columns["EncodedValue"];
				this.columnEncodingTime = base.Columns["EncodingTime"];
				this.columnIncludeLabel = base.Columns["IncludeLabel"];
				this.columnForecolor = base.Columns["Forecolor"];
				this.columnBackcolor = base.Columns["Backcolor"];
				this.columnCountryAssigningManufacturingCode = base.Columns["CountryAssigningManufacturingCode"];
				this.columnImageWidth = base.Columns["ImageWidth"];
				this.columnImageHeight = base.Columns["ImageHeight"];
				this.columnImage = base.Columns["Image"];
				this.columnRotateFlipType = base.Columns["RotateFlipType"];
				this.columnLabelPosition = base.Columns["LabelPosition"];
				this.columnAlignment = base.Columns["Alignment"];
				this.columnLabelFont = base.Columns["LabelFont"];
				this.columnImageFormat = base.Columns["ImageFormat"];
			}

			// Token: 0x060000EB RID: 235 RVA: 0x0000FB04 File Offset: 0x0000DD04
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.columnType = new DataColumn("Type", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnType);
				this.columnRawData = new DataColumn("RawData", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnRawData);
				this.columnEncodedValue = new DataColumn("EncodedValue", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnEncodedValue);
				this.columnEncodingTime = new DataColumn("EncodingTime", typeof(double), null, MappingType.Element);
				base.Columns.Add(this.columnEncodingTime);
				this.columnIncludeLabel = new DataColumn("IncludeLabel", typeof(bool), null, MappingType.Element);
				base.Columns.Add(this.columnIncludeLabel);
				this.columnForecolor = new DataColumn("Forecolor", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnForecolor);
				this.columnBackcolor = new DataColumn("Backcolor", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnBackcolor);
				this.columnCountryAssigningManufacturingCode = new DataColumn("CountryAssigningManufacturingCode", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnCountryAssigningManufacturingCode);
				this.columnImageWidth = new DataColumn("ImageWidth", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnImageWidth);
				this.columnImageHeight = new DataColumn("ImageHeight", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnImageHeight);
				this.columnImage = new DataColumn("Image", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnImage);
				this.columnRotateFlipType = new DataColumn("RotateFlipType", typeof(RotateFlipType), null, MappingType.Element);
				base.Columns.Add(this.columnRotateFlipType);
				this.columnLabelPosition = new DataColumn("LabelPosition", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnLabelPosition);
				this.columnAlignment = new DataColumn("Alignment", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnAlignment);
				this.columnLabelFont = new DataColumn("LabelFont", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnLabelFont);
				this.columnImageFormat = new DataColumn("ImageFormat", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnImageFormat);
				this.columnImage.MaxLength = 10000000;
			}

			// Token: 0x060000EC RID: 236 RVA: 0x0000FDF1 File Offset: 0x0000DFF1
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public BarcodeXML.BarcodeRow NewBarcodeRow()
			{
				return (BarcodeXML.BarcodeRow)base.NewRow();
			}

			// Token: 0x060000ED RID: 237 RVA: 0x0000FDFE File Offset: 0x0000DFFE
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new BarcodeXML.BarcodeRow(builder);
			}

			// Token: 0x060000EE RID: 238 RVA: 0x0000FE06 File Offset: 0x0000E006
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(BarcodeXML.BarcodeRow);
			}

			// Token: 0x060000EF RID: 239 RVA: 0x0000FE12 File Offset: 0x0000E012
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.BarcodeRowChanged != null)
				{
					this.BarcodeRowChanged(this, new BarcodeXML.BarcodeRowChangeEvent((BarcodeXML.BarcodeRow)e.Row, e.Action));
				}
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x0000FE45 File Offset: 0x0000E045
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.BarcodeRowChanging != null)
				{
					this.BarcodeRowChanging(this, new BarcodeXML.BarcodeRowChangeEvent((BarcodeXML.BarcodeRow)e.Row, e.Action));
				}
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x0000FE78 File Offset: 0x0000E078
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.BarcodeRowDeleted != null)
				{
					this.BarcodeRowDeleted(this, new BarcodeXML.BarcodeRowChangeEvent((BarcodeXML.BarcodeRow)e.Row, e.Action));
				}
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x0000FEAB File Offset: 0x0000E0AB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.BarcodeRowDeleting != null)
				{
					this.BarcodeRowDeleting(this, new BarcodeXML.BarcodeRowChangeEvent((BarcodeXML.BarcodeRow)e.Row, e.Action));
				}
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x0000FEDE File Offset: 0x0000E0DE
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveBarcodeRow(BarcodeXML.BarcodeRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x0000FEEC File Offset: 0x0000E0EC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				BarcodeXML barcodeXML = new BarcodeXML();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = barcodeXML.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "BarcodeDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = barcodeXML.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000087 RID: 135
			private DataColumn columnType;

			// Token: 0x04000088 RID: 136
			private DataColumn columnRawData;

			// Token: 0x04000089 RID: 137
			private DataColumn columnEncodedValue;

			// Token: 0x0400008A RID: 138
			private DataColumn columnEncodingTime;

			// Token: 0x0400008B RID: 139
			private DataColumn columnIncludeLabel;

			// Token: 0x0400008C RID: 140
			private DataColumn columnForecolor;

			// Token: 0x0400008D RID: 141
			private DataColumn columnBackcolor;

			// Token: 0x0400008E RID: 142
			private DataColumn columnCountryAssigningManufacturingCode;

			// Token: 0x0400008F RID: 143
			private DataColumn columnImageWidth;

			// Token: 0x04000090 RID: 144
			private DataColumn columnImageHeight;

			// Token: 0x04000091 RID: 145
			private DataColumn columnImage;

			// Token: 0x04000092 RID: 146
			private DataColumn columnRotateFlipType;

			// Token: 0x04000093 RID: 147
			private DataColumn columnLabelPosition;

			// Token: 0x04000094 RID: 148
			private DataColumn columnAlignment;

			// Token: 0x04000095 RID: 149
			private DataColumn columnLabelFont;

			// Token: 0x04000096 RID: 150
			private DataColumn columnImageFormat;
		}

		// Token: 0x02000024 RID: 36
		public class BarcodeRow : DataRow
		{
			// Token: 0x060000F5 RID: 245 RVA: 0x000100E0 File Offset: 0x0000E2E0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal BarcodeRow(DataRowBuilder rb) : base(rb)
			{
				this.tableBarcode = (BarcodeXML.BarcodeDataTable)base.Table;
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x000100FC File Offset: 0x0000E2FC
			// (set) Token: 0x060000F7 RID: 247 RVA: 0x00010140 File Offset: 0x0000E340
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string Type
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.TypeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Type' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.TypeColumn] = value;
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00010154 File Offset: 0x0000E354
			// (set) Token: 0x060000F9 RID: 249 RVA: 0x00010198 File Offset: 0x0000E398
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string RawData
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.RawDataColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'RawData' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.RawDataColumn] = value;
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000FA RID: 250 RVA: 0x000101AC File Offset: 0x0000E3AC
			// (set) Token: 0x060000FB RID: 251 RVA: 0x000101F0 File Offset: 0x0000E3F0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string EncodedValue
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.EncodedValueColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'EncodedValue' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.EncodedValueColumn] = value;
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000FC RID: 252 RVA: 0x00010204 File Offset: 0x0000E404
			// (set) Token: 0x060000FD RID: 253 RVA: 0x00010248 File Offset: 0x0000E448
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public double EncodingTime
			{
				get
				{
					double result;
					try
					{
						result = (double)base[this.tableBarcode.EncodingTimeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'EncodingTime' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.EncodingTimeColumn] = value;
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000FE RID: 254 RVA: 0x00010264 File Offset: 0x0000E464
			// (set) Token: 0x060000FF RID: 255 RVA: 0x000102A8 File Offset: 0x0000E4A8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IncludeLabel
			{
				get
				{
					bool result;
					try
					{
						result = (bool)base[this.tableBarcode.IncludeLabelColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'IncludeLabel' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.IncludeLabelColumn] = value;
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000100 RID: 256 RVA: 0x000102C4 File Offset: 0x0000E4C4
			// (set) Token: 0x06000101 RID: 257 RVA: 0x00010308 File Offset: 0x0000E508
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string Forecolor
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.ForecolorColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Forecolor' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.ForecolorColumn] = value;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000102 RID: 258 RVA: 0x0001031C File Offset: 0x0000E51C
			// (set) Token: 0x06000103 RID: 259 RVA: 0x00010360 File Offset: 0x0000E560
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string Backcolor
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.BackcolorColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Backcolor' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.BackcolorColumn] = value;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000104 RID: 260 RVA: 0x00010374 File Offset: 0x0000E574
			// (set) Token: 0x06000105 RID: 261 RVA: 0x000103B8 File Offset: 0x0000E5B8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string CountryAssigningManufacturingCode
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.CountryAssigningManufacturingCodeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'CountryAssigningManufacturingCode' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.CountryAssigningManufacturingCodeColumn] = value;
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000106 RID: 262 RVA: 0x000103CC File Offset: 0x0000E5CC
			// (set) Token: 0x06000107 RID: 263 RVA: 0x00010410 File Offset: 0x0000E610
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int ImageWidth
			{
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableBarcode.ImageWidthColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ImageWidth' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.ImageWidthColumn] = value;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000108 RID: 264 RVA: 0x0001042C File Offset: 0x0000E62C
			// (set) Token: 0x06000109 RID: 265 RVA: 0x00010470 File Offset: 0x0000E670
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int ImageHeight
			{
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableBarcode.ImageHeightColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ImageHeight' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.ImageHeightColumn] = value;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600010A RID: 266 RVA: 0x0001048C File Offset: 0x0000E68C
			// (set) Token: 0x0600010B RID: 267 RVA: 0x000104D0 File Offset: 0x0000E6D0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string Image
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.ImageColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Image' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.ImageColumn] = value;
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600010C RID: 268 RVA: 0x000104E4 File Offset: 0x0000E6E4
			// (set) Token: 0x0600010D RID: 269 RVA: 0x00010528 File Offset: 0x0000E728
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public RotateFlipType RotateFlipType
			{
				get
				{
					RotateFlipType result;
					try
					{
						result = (RotateFlipType)base[this.tableBarcode.RotateFlipTypeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'RotateFlipType' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.RotateFlipTypeColumn] = value;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600010E RID: 270 RVA: 0x00010544 File Offset: 0x0000E744
			// (set) Token: 0x0600010F RID: 271 RVA: 0x00010588 File Offset: 0x0000E788
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int LabelPosition
			{
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableBarcode.LabelPositionColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'LabelPosition' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.LabelPositionColumn] = value;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000110 RID: 272 RVA: 0x000105A4 File Offset: 0x0000E7A4
			// (set) Token: 0x06000111 RID: 273 RVA: 0x000105E8 File Offset: 0x0000E7E8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Alignment
			{
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableBarcode.AlignmentColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Alignment' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.AlignmentColumn] = value;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000112 RID: 274 RVA: 0x00010604 File Offset: 0x0000E804
			// (set) Token: 0x06000113 RID: 275 RVA: 0x00010648 File Offset: 0x0000E848
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string LabelFont
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.LabelFontColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'LabelFont' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.LabelFontColumn] = value;
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000114 RID: 276 RVA: 0x0001065C File Offset: 0x0000E85C
			// (set) Token: 0x06000115 RID: 277 RVA: 0x000106A0 File Offset: 0x0000E8A0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string ImageFormat
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableBarcode.ImageFormatColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ImageFormat' in table 'Barcode' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableBarcode.ImageFormatColumn] = value;
				}
			}

			// Token: 0x06000116 RID: 278 RVA: 0x000106B4 File Offset: 0x0000E8B4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsTypeNull()
			{
				return base.IsNull(this.tableBarcode.TypeColumn);
			}

			// Token: 0x06000117 RID: 279 RVA: 0x000106C7 File Offset: 0x0000E8C7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetTypeNull()
			{
				base[this.tableBarcode.TypeColumn] = Convert.DBNull;
			}

			// Token: 0x06000118 RID: 280 RVA: 0x000106DF File Offset: 0x0000E8DF
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsRawDataNull()
			{
				return base.IsNull(this.tableBarcode.RawDataColumn);
			}

			// Token: 0x06000119 RID: 281 RVA: 0x000106F2 File Offset: 0x0000E8F2
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetRawDataNull()
			{
				base[this.tableBarcode.RawDataColumn] = Convert.DBNull;
			}

			// Token: 0x0600011A RID: 282 RVA: 0x0001070A File Offset: 0x0000E90A
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsEncodedValueNull()
			{
				return base.IsNull(this.tableBarcode.EncodedValueColumn);
			}

			// Token: 0x0600011B RID: 283 RVA: 0x0001071D File Offset: 0x0000E91D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetEncodedValueNull()
			{
				base[this.tableBarcode.EncodedValueColumn] = Convert.DBNull;
			}

			// Token: 0x0600011C RID: 284 RVA: 0x00010735 File Offset: 0x0000E935
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsEncodingTimeNull()
			{
				return base.IsNull(this.tableBarcode.EncodingTimeColumn);
			}

			// Token: 0x0600011D RID: 285 RVA: 0x00010748 File Offset: 0x0000E948
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetEncodingTimeNull()
			{
				base[this.tableBarcode.EncodingTimeColumn] = Convert.DBNull;
			}

			// Token: 0x0600011E RID: 286 RVA: 0x00010760 File Offset: 0x0000E960
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsIncludeLabelNull()
			{
				return base.IsNull(this.tableBarcode.IncludeLabelColumn);
			}

			// Token: 0x0600011F RID: 287 RVA: 0x00010773 File Offset: 0x0000E973
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetIncludeLabelNull()
			{
				base[this.tableBarcode.IncludeLabelColumn] = Convert.DBNull;
			}

			// Token: 0x06000120 RID: 288 RVA: 0x0001078B File Offset: 0x0000E98B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsForecolorNull()
			{
				return base.IsNull(this.tableBarcode.ForecolorColumn);
			}

			// Token: 0x06000121 RID: 289 RVA: 0x0001079E File Offset: 0x0000E99E
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetForecolorNull()
			{
				base[this.tableBarcode.ForecolorColumn] = Convert.DBNull;
			}

			// Token: 0x06000122 RID: 290 RVA: 0x000107B6 File Offset: 0x0000E9B6
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsBackcolorNull()
			{
				return base.IsNull(this.tableBarcode.BackcolorColumn);
			}

			// Token: 0x06000123 RID: 291 RVA: 0x000107C9 File Offset: 0x0000E9C9
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetBackcolorNull()
			{
				base[this.tableBarcode.BackcolorColumn] = Convert.DBNull;
			}

			// Token: 0x06000124 RID: 292 RVA: 0x000107E1 File Offset: 0x0000E9E1
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsCountryAssigningManufacturingCodeNull()
			{
				return base.IsNull(this.tableBarcode.CountryAssigningManufacturingCodeColumn);
			}

			// Token: 0x06000125 RID: 293 RVA: 0x000107F4 File Offset: 0x0000E9F4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetCountryAssigningManufacturingCodeNull()
			{
				base[this.tableBarcode.CountryAssigningManufacturingCodeColumn] = Convert.DBNull;
			}

			// Token: 0x06000126 RID: 294 RVA: 0x0001080C File Offset: 0x0000EA0C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsImageWidthNull()
			{
				return base.IsNull(this.tableBarcode.ImageWidthColumn);
			}

			// Token: 0x06000127 RID: 295 RVA: 0x0001081F File Offset: 0x0000EA1F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetImageWidthNull()
			{
				base[this.tableBarcode.ImageWidthColumn] = Convert.DBNull;
			}

			// Token: 0x06000128 RID: 296 RVA: 0x00010837 File Offset: 0x0000EA37
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsImageHeightNull()
			{
				return base.IsNull(this.tableBarcode.ImageHeightColumn);
			}

			// Token: 0x06000129 RID: 297 RVA: 0x0001084A File Offset: 0x0000EA4A
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetImageHeightNull()
			{
				base[this.tableBarcode.ImageHeightColumn] = Convert.DBNull;
			}

			// Token: 0x0600012A RID: 298 RVA: 0x00010862 File Offset: 0x0000EA62
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsImageNull()
			{
				return base.IsNull(this.tableBarcode.ImageColumn);
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00010875 File Offset: 0x0000EA75
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetImageNull()
			{
				base[this.tableBarcode.ImageColumn] = Convert.DBNull;
			}

			// Token: 0x0600012C RID: 300 RVA: 0x0001088D File Offset: 0x0000EA8D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsRotateFlipTypeNull()
			{
				return base.IsNull(this.tableBarcode.RotateFlipTypeColumn);
			}

			// Token: 0x0600012D RID: 301 RVA: 0x000108A0 File Offset: 0x0000EAA0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetRotateFlipTypeNull()
			{
				base[this.tableBarcode.RotateFlipTypeColumn] = Convert.DBNull;
			}

			// Token: 0x0600012E RID: 302 RVA: 0x000108B8 File Offset: 0x0000EAB8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsLabelPositionNull()
			{
				return base.IsNull(this.tableBarcode.LabelPositionColumn);
			}

			// Token: 0x0600012F RID: 303 RVA: 0x000108CB File Offset: 0x0000EACB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetLabelPositionNull()
			{
				base[this.tableBarcode.LabelPositionColumn] = Convert.DBNull;
			}

			// Token: 0x06000130 RID: 304 RVA: 0x000108E3 File Offset: 0x0000EAE3
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsAlignmentNull()
			{
				return base.IsNull(this.tableBarcode.AlignmentColumn);
			}

			// Token: 0x06000131 RID: 305 RVA: 0x000108F6 File Offset: 0x0000EAF6
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetAlignmentNull()
			{
				base[this.tableBarcode.AlignmentColumn] = Convert.DBNull;
			}

			// Token: 0x06000132 RID: 306 RVA: 0x0001090E File Offset: 0x0000EB0E
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsLabelFontNull()
			{
				return base.IsNull(this.tableBarcode.LabelFontColumn);
			}

			// Token: 0x06000133 RID: 307 RVA: 0x00010921 File Offset: 0x0000EB21
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetLabelFontNull()
			{
				base[this.tableBarcode.LabelFontColumn] = Convert.DBNull;
			}

			// Token: 0x06000134 RID: 308 RVA: 0x00010939 File Offset: 0x0000EB39
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsImageFormatNull()
			{
				return base.IsNull(this.tableBarcode.ImageFormatColumn);
			}

			// Token: 0x06000135 RID: 309 RVA: 0x0001094C File Offset: 0x0000EB4C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetImageFormatNull()
			{
				base[this.tableBarcode.ImageFormatColumn] = Convert.DBNull;
			}

			// Token: 0x0400009B RID: 155
			private BarcodeXML.BarcodeDataTable tableBarcode;
		}

		// Token: 0x02000025 RID: 37
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class BarcodeRowChangeEvent : EventArgs
		{
			// Token: 0x06000136 RID: 310 RVA: 0x00010964 File Offset: 0x0000EB64
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public BarcodeRowChangeEvent(BarcodeXML.BarcodeRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000137 RID: 311 RVA: 0x0001097A File Offset: 0x0000EB7A
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public BarcodeXML.BarcodeRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000138 RID: 312 RVA: 0x00010982 File Offset: 0x0000EB82
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x0400009C RID: 156
			private BarcodeXML.BarcodeRow eventRow;

			// Token: 0x0400009D RID: 157
			private DataRowAction eventAction;
		}
	}
}
