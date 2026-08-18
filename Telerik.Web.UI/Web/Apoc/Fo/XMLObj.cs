using System;
using System.Collections;
using System.Xml;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015C6 RID: 5574
	internal abstract class XMLObj : FObj
	{
		// Token: 0x0600D953 RID: 55635 RVA: 0x002FB47B File Offset: 0x002F967B
		public XMLObj(FObj parent, PropertyList propertyList, string tag) : base(parent, propertyList)
		{
			this.tagName = tag;
		}

		// Token: 0x0600D954 RID: 55636
		public abstract string GetNameSpace();

		// Token: 0x0600D955 RID: 55637 RVA: 0x002FB497 File Offset: 0x002F9697
		public void addGraphic(XmlDocument doc, XmlNode parent)
		{
			this.doc = doc;
		}

		// Token: 0x0600D956 RID: 55638 RVA: 0x002FB4A0 File Offset: 0x002F96A0
		public void buildTopLevel(XmlDocument doc, XmlNode svgRoot)
		{
		}

		// Token: 0x0600D957 RID: 55639 RVA: 0x002FB4A4 File Offset: 0x002F96A4
		public XmlDocument CreateBasicDocument()
		{
			try
			{
				this.doc = new XmlDocument();
				this.doc.AppendChild(this.doc.CreateElement("graph", "http://www.chive.com"));
				this.element = this.doc.DocumentElement;
				this.buildTopLevel(this.doc, this.element);
			}
			catch (Exception ex)
			{
				ApocDriver.ActiveDriver.FireApocError(ex.ToString());
			}
			return this.doc;
		}

		// Token: 0x0600D958 RID: 55640 RVA: 0x002FB52C File Offset: 0x002F972C
		protected internal override void AddChild(FONode child)
		{
			XMLObj xmlobj = child as XMLObj;
			if (xmlobj != null)
			{
				xmlobj.addGraphic(this.doc, this.element);
			}
		}

		// Token: 0x0600D959 RID: 55641 RVA: 0x002FB558 File Offset: 0x002F9758
		protected internal override void AddCharacters(char[] data, int start, int length)
		{
			string text = new string(data, start, length - start);
			this.doc.DocumentElement.AppendChild(this.doc.CreateTextNode(text));
		}

		// Token: 0x0600D95A RID: 55642 RVA: 0x002FB58D File Offset: 0x002F978D
		public override Status Layout(Area area)
		{
			ApocDriver.ActiveDriver.FireApocError(this.name + " outside foreign xml");
			return new Status(1);
		}

		// Token: 0x0600D95B RID: 55643 RVA: 0x002FB5AF File Offset: 0x002F97AF
		public override void RemoveID(IDReferences idReferences)
		{
		}

		// Token: 0x0600D95C RID: 55644 RVA: 0x002FB5B1 File Offset: 0x002F97B1
		public override void SetIsInTableCell()
		{
		}

		// Token: 0x0600D95D RID: 55645 RVA: 0x002FB5B3 File Offset: 0x002F97B3
		public override void ForceStartOffset(int offset)
		{
		}

		// Token: 0x0600D95E RID: 55646 RVA: 0x002FB5B5 File Offset: 0x002F97B5
		public override void ForceWidth(int width)
		{
		}

		// Token: 0x0600D95F RID: 55647 RVA: 0x002FB5B7 File Offset: 0x002F97B7
		public override void ResetMarker()
		{
		}

		// Token: 0x0600D960 RID: 55648 RVA: 0x002FB5B9 File Offset: 0x002F97B9
		public override void SetLinkSet(LinkSet linkSet)
		{
		}

		// Token: 0x0600D961 RID: 55649 RVA: 0x002FB5BB File Offset: 0x002F97BB
		public override ArrayList getMarkerSnapshot(ArrayList snapshot)
		{
			return snapshot;
		}

		// Token: 0x0600D962 RID: 55650 RVA: 0x002FB5BE File Offset: 0x002F97BE
		public override void Rollback(ArrayList snapshot)
		{
		}

		// Token: 0x0600D963 RID: 55651 RVA: 0x002FB5C0 File Offset: 0x002F97C0
		protected override void SetWritingMode()
		{
		}

		// Token: 0x04003C16 RID: 15382
		protected const string NS = "http://www.chive.com";

		// Token: 0x04003C17 RID: 15383
		protected string tagName = "";

		// Token: 0x04003C18 RID: 15384
		protected XmlNode element;

		// Token: 0x04003C19 RID: 15385
		protected XmlDocument doc;

		// Token: 0x04003C1A RID: 15386
		protected static Hashtable ns = new Hashtable();
	}
}
