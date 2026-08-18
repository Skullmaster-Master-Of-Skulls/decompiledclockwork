using System;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017C4 RID: 6084
	public class ChartSkin
	{
		// Token: 0x1700479B RID: 18331
		// (get) Token: 0x0600ECBC RID: 60604 RVA: 0x00360800 File Offset: 0x0035EA00
		// (set) Token: 0x0600ECBD RID: 60605 RVA: 0x00360808 File Offset: 0x0035EA08
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return this.skinName;
			}
			set
			{
				this.skinName = value;
			}
		}

		// Token: 0x1700479C RID: 18332
		// (get) Token: 0x0600ECBE RID: 60606 RVA: 0x00360811 File Offset: 0x0035EA11
		// (set) Token: 0x0600ECBF RID: 60607 RVA: 0x00360819 File Offset: 0x0035EA19
		public XmlDocument XmlSource
		{
			get
			{
				return this.skinXmlSource;
			}
			set
			{
				this.skinXmlSource = value;
			}
		}

		// Token: 0x0600ECC0 RID: 60608 RVA: 0x00360822 File Offset: 0x0035EA22
		public ChartSkin()
		{
			this.skinName = "";
			this.skinXmlSource = new XmlDocument();
		}

		// Token: 0x0600ECC1 RID: 60609 RVA: 0x00360840 File Offset: 0x0035EA40
		public ChartSkin(string name) : this()
		{
			this.skinName = name;
		}

		// Token: 0x0600ECC2 RID: 60610 RVA: 0x0036084F File Offset: 0x0035EA4F
		public ChartSkin(XmlDocument source) : this()
		{
			this.skinXmlSource = source;
			Tools.ParseAttribute(ref this.skinName, this.skinXmlSource.DocumentElement, "SkinName");
		}

		// Token: 0x0600ECC3 RID: 60611 RVA: 0x0036087A File Offset: 0x0035EA7A
		internal static bool IsEmpty(string name)
		{
			return string.IsNullOrEmpty(name) || string.Compare("", name, true) == 0 || string.Compare("(None)", name, true) == 0;
		}

		// Token: 0x0600ECC4 RID: 60612 RVA: 0x003608A4 File Offset: 0x0035EAA4
		public void ApplyTo(Chart chart)
		{
			TextWriter textWriter = new StringWriter();
			textWriter.Write(this.skinXmlSource.InnerXml);
			chart.LoadSkin(chart, textWriter);
		}

		// Token: 0x0600ECC5 RID: 60613 RVA: 0x003608D0 File Offset: 0x0035EAD0
		public void CreateFromChart(Chart chart, string name)
		{
			this.skinName = name;
			this.skinXmlSource.Load(new StringReader(chart.SaveSkin(chart).ToString()));
			XmlElement documentElement = this.skinXmlSource.DocumentElement;
			if (documentElement != null)
			{
				Tools.SetAttribute(documentElement, "SkinName", name, typeof(string));
			}
		}

		// Token: 0x04004445 RID: 17477
		private string skinName;

		// Token: 0x04004446 RID: 17478
		private XmlDocument skinXmlSource;
	}
}
