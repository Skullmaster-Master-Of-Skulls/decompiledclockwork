using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Xml;
using Telerik.Web.Apoc.Configuration;
using Telerik.Web.Apoc.Extensions;
using Telerik.Web.Apoc.Fo;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc
{
	// Token: 0x02001370 RID: 4976
	public class ApocDriver : IDriver
	{
		// Token: 0x140001BA RID: 442
		// (add) Token: 0x0600CFB6 RID: 53174 RVA: 0x002E0E70 File Offset: 0x002DF070
		// (remove) Token: 0x0600CFB7 RID: 53175 RVA: 0x002E0EA8 File Offset: 0x002DF0A8
		public event ApocDriver.ApocEventHandler OnError;

		// Token: 0x140001BB RID: 443
		// (add) Token: 0x0600CFB8 RID: 53176 RVA: 0x002E0EE0 File Offset: 0x002DF0E0
		// (remove) Token: 0x0600CFB9 RID: 53177 RVA: 0x002E0F18 File Offset: 0x002DF118
		public event ApocDriver.ApocEventHandler OnWarning;

		// Token: 0x140001BC RID: 444
		// (add) Token: 0x0600CFBA RID: 53178 RVA: 0x002E0F50 File Offset: 0x002DF150
		// (remove) Token: 0x0600CFBB RID: 53179 RVA: 0x002E0F88 File Offset: 0x002DF188
		public event ApocDriver.ApocEventHandler OnInfo;

		// Token: 0x0600CFBC RID: 53180 RVA: 0x002E0FBD File Offset: 0x002DF1BD
		public static ApocDriver Make()
		{
			return new ApocDriver();
		}

		// Token: 0x0600CFBD RID: 53181 RVA: 0x002E0FC4 File Offset: 0x002DF1C4
		public ApocDriver()
		{
			this.BaseDirectory = new DirectoryInfo(Path.GetFullPath(Directory.GetCurrentDirectory()));
			ApocDriver.ActiveDriver = this;
		}

		// Token: 0x170042C6 RID: 17094
		// (get) Token: 0x0600CFBE RID: 53182 RVA: 0x002E0FF5 File Offset: 0x002DF1F5
		// (set) Token: 0x0600CFBF RID: 53183 RVA: 0x002E0FFD File Offset: 0x002DF1FD
		public bool CloseOnExit
		{
			get
			{
				return this.closeOnExit;
			}
			set
			{
				this.closeOnExit = value;
			}
		}

		// Token: 0x170042C7 RID: 17095
		// (get) Token: 0x0600CFC0 RID: 53184 RVA: 0x002E1006 File Offset: 0x002DF206
		// (set) Token: 0x0600CFC1 RID: 53185 RVA: 0x002E100D File Offset: 0x002DF20D
		public static ApocDriver ActiveDriver
		{
			get
			{
				return ApocDriver.activeDriver;
			}
			set
			{
				ApocDriver.activeDriver = value;
			}
		}

		// Token: 0x170042C8 RID: 17096
		// (get) Token: 0x0600CFC2 RID: 53186 RVA: 0x002E1015 File Offset: 0x002DF215
		// (set) Token: 0x0600CFC3 RID: 53187 RVA: 0x002E101D File Offset: 0x002DF21D
		public RendererEngine Renderer
		{
			get
			{
				return this.renderEngine;
			}
			set
			{
				this.renderEngine = value;
			}
		}

		// Token: 0x170042C9 RID: 17097
		// (get) Token: 0x0600CFC4 RID: 53188 RVA: 0x002E1026 File Offset: 0x002DF226
		// (set) Token: 0x0600CFC5 RID: 53189 RVA: 0x002E1037 File Offset: 0x002DF237
		public DirectoryInfo BaseDirectory
		{
			get
			{
				return (DirectoryInfo)Configuration.GetValue("baseDir");
			}
			set
			{
				Configuration.PutValue("baseDir", value.FullName);
			}
		}

		// Token: 0x170042CA RID: 17098
		// (get) Token: 0x0600CFC6 RID: 53190 RVA: 0x002E1049 File Offset: 0x002DF249
		// (set) Token: 0x0600CFC7 RID: 53191 RVA: 0x002E1051 File Offset: 0x002DF251
		public ApocDriver.ApocImageHandler ImageHandler
		{
			get
			{
				return this.imageHandler;
			}
			set
			{
				this.imageHandler = value;
			}
		}

		// Token: 0x170042CB RID: 17099
		// (get) Token: 0x0600CFC8 RID: 53192 RVA: 0x002E105A File Offset: 0x002DF25A
		// (set) Token: 0x0600CFC9 RID: 53193 RVA: 0x002E1066 File Offset: 0x002DF266
		public int Timeout
		{
			get
			{
				return Configuration.GetIntValue("timeout");
			}
			set
			{
				Configuration.PutValue("timeout", value);
			}
		}

		// Token: 0x170042CC RID: 17100
		// (get) Token: 0x0600CFCA RID: 53194 RVA: 0x002E1078 File Offset: 0x002DF278
		public CredentialCache Credentials
		{
			get
			{
				if (this.credentials == null)
				{
					this.credentials = new CredentialCache();
				}
				return this.credentials;
			}
		}

		// Token: 0x170042CD RID: 17101
		// (set) Token: 0x0600CFCB RID: 53195 RVA: 0x002E1093 File Offset: 0x002DF293
		public static string ProductKey
		{
			set
			{
				ApocDriver.productKey = value;
			}
		}

		// Token: 0x170042CE RID: 17102
		// (get) Token: 0x0600CFCC RID: 53196 RVA: 0x002E109B File Offset: 0x002DF29B
		internal static string InternalProductKey
		{
			get
			{
				return ApocDriver.productKey;
			}
		}

		// Token: 0x170042CF RID: 17103
		// (get) Token: 0x0600CFCD RID: 53197 RVA: 0x002E10A2 File Offset: 0x002DF2A2
		// (set) Token: 0x0600CFCE RID: 53198 RVA: 0x002E10AA File Offset: 0x002DF2AA
		public IRendererOptions Options
		{
			get
			{
				return this.renderOptions;
			}
			set
			{
				this.renderOptions = value;
			}
		}

		// Token: 0x0600CFCF RID: 53199 RVA: 0x002E10B4 File Offset: 0x002DF2B4
		public virtual void Render(XmlDocument doc, Stream outputStream)
		{
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			doc.Save(xmlTextWriter);
			xmlTextWriter.Close();
			this.Render(new StringReader(stringWriter.ToString()), outputStream);
		}

		// Token: 0x0600CFD0 RID: 53200 RVA: 0x002E10ED File Offset: 0x002DF2ED
		public virtual void Render(TextReader inputReader, Stream outputStream)
		{
			this.Render(this.CreateXmlTextReader(inputReader), outputStream);
		}

		// Token: 0x0600CFD1 RID: 53201 RVA: 0x002E10FD File Offset: 0x002DF2FD
		public virtual void Render(string inputFile, string outputFile)
		{
			this.Render(this.CreateXmlTextReader(inputFile), new FileStream(outputFile, FileMode.Create, FileAccess.Write));
		}

		// Token: 0x0600CFD2 RID: 53202 RVA: 0x002E1114 File Offset: 0x002DF314
		public virtual void Render(string inputFile, Stream outputStream)
		{
			this.Render(this.CreateXmlTextReader(inputFile), outputStream);
		}

		// Token: 0x0600CFD3 RID: 53203 RVA: 0x002E1124 File Offset: 0x002DF324
		public virtual void Render(Stream inputStream, Stream outputStream)
		{
			this.Render(this.CreateXmlTextReader(inputStream), outputStream);
		}

		// Token: 0x0600CFD4 RID: 53204 RVA: 0x002E1134 File Offset: 0x002DF334
		public void Render(XmlReader inputReader, Stream outputStream)
		{
			try
			{
				IRenderer renderer = RendererFactory.Make(this.renderEngine, outputStream);
				if (this.renderOptions != null)
				{
					renderer.Options = this.renderOptions;
				}
				StreamRenderer streamRenderer = new StreamRenderer(renderer);
				FOTreeBuilder fotreeBuilder = new FOTreeBuilder();
				fotreeBuilder.SetStreamRenderer(streamRenderer);
				StandardElementMapping standardElementMapping = new StandardElementMapping();
				standardElementMapping.AddToBuilder(fotreeBuilder);
				ExtensionElementMapping extensionElementMapping = new ExtensionElementMapping();
				extensionElementMapping.AddToBuilder(fotreeBuilder);
				fotreeBuilder.Parse(inputReader);
			}
			finally
			{
				if (this.CloseOnExit)
				{
					outputStream.Flush();
					outputStream.Close();
				}
			}
		}

		// Token: 0x170042D0 RID: 17104
		// (get) Token: 0x0600CFD5 RID: 53205 RVA: 0x002E11C0 File Offset: 0x002DF3C0
		internal bool IsEvaluation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600CFD6 RID: 53206 RVA: 0x002E11C3 File Offset: 0x002DF3C3
		internal static string GetString(string key)
		{
			return ApocDriver.rm.GetString(key);
		}

		// Token: 0x0600CFD7 RID: 53207 RVA: 0x002E11D0 File Offset: 0x002DF3D0
		internal void FireApocError(string message)
		{
			if (this.OnError != null)
			{
				this.OnError(this, new ApocEventArgs(message));
				return;
			}
			throw new SystemException(message);
		}

		// Token: 0x0600CFD8 RID: 53208 RVA: 0x002E11F3 File Offset: 0x002DF3F3
		internal void FireApocWarning(string message)
		{
			if (this.OnWarning != null)
			{
				this.OnWarning(this, new ApocEventArgs(message));
				return;
			}
			Console.WriteLine("[WARN] {0}", message);
		}

		// Token: 0x0600CFD9 RID: 53209 RVA: 0x002E121B File Offset: 0x002DF41B
		internal void FireApocInfo(string message)
		{
			if (this.OnInfo != null)
			{
				this.OnInfo(this, new ApocEventArgs(message));
				return;
			}
			Console.WriteLine("[INFO] {0}", message);
		}

		// Token: 0x0600CFDA RID: 53210 RVA: 0x002E1244 File Offset: 0x002DF444
		private XmlReader CreateXmlTextReader(string inputFile)
		{
			return new XmlTextReader(inputFile);
		}

		// Token: 0x0600CFDB RID: 53211 RVA: 0x002E125C File Offset: 0x002DF45C
		private XmlReader CreateXmlTextReader(Stream inputStream)
		{
			return new XmlTextReader(inputStream);
		}

		// Token: 0x0600CFDC RID: 53212 RVA: 0x002E1274 File Offset: 0x002DF474
		private XmlReader CreateXmlTextReader(TextReader inputReader)
		{
			return new XmlTextReader(inputReader);
		}

		// Token: 0x040037AE RID: 14254
		private RendererEngine renderEngine = RendererEngine.PDF;

		// Token: 0x040037AF RID: 14255
		private bool closeOnExit = true;

		// Token: 0x040037B0 RID: 14256
		private IRendererOptions renderOptions;

		// Token: 0x040037B1 RID: 14257
		private CredentialCache credentials;

		// Token: 0x040037B2 RID: 14258
		private static readonly ResourceManager rm = new ResourceManager("Apoc.src.Apoc", Assembly.GetExecutingAssembly());

		// Token: 0x040037B3 RID: 14259
		[ThreadStatic]
		private static ApocDriver activeDriver;

		// Token: 0x040037B4 RID: 14260
		private static string productKey;

		// Token: 0x040037B8 RID: 14264
		private ApocDriver.ApocImageHandler imageHandler;

		// Token: 0x02001371 RID: 4977
		// (Invoke) Token: 0x0600CFDF RID: 53215
		public delegate void ApocEventHandler(object sender, ApocEventArgs e);

		// Token: 0x02001372 RID: 4978
		// (Invoke) Token: 0x0600CFE3 RID: 53219
		public delegate byte[] ApocImageHandler(string src);
	}
}
