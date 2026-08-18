using System;
using System.Collections;
using Telerik.Web.Apoc.Fo.Flow;
using Telerik.Web.Apoc.Fo.Pagination;
using Telerik.Web.Apoc.Fo.Properties;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015C0 RID: 5568
	internal class StandardElementMapping
	{
		// Token: 0x0600D941 RID: 55617 RVA: 0x002FAE20 File Offset: 0x002F9020
		static StandardElementMapping()
		{
			StandardElementMapping.foObjs.Add("root", Root.GetMaker());
			StandardElementMapping.foObjs.Add("declarations", Declarations.GetMaker());
			StandardElementMapping.foObjs.Add("color-profile", ColorProfile.GetMaker());
			StandardElementMapping.foObjs.Add("page-sequence", PageSequence.GetMaker());
			StandardElementMapping.foObjs.Add("layout-master-set", LayoutMasterSet.GetMaker());
			StandardElementMapping.foObjs.Add("page-sequence-master", PageSequenceMaster.GetMaker());
			StandardElementMapping.foObjs.Add("single-page-master-reference", SinglePageMasterReference.GetMaker());
			StandardElementMapping.foObjs.Add("repeatable-page-master-reference", RepeatablePageMasterReference.GetMaker());
			StandardElementMapping.foObjs.Add("repeatable-page-master-alternatives", RepeatablePageMasterAlternatives.GetMaker());
			StandardElementMapping.foObjs.Add("conditional-page-master-reference", ConditionalPageMasterReference.GetMaker());
			StandardElementMapping.foObjs.Add("simple-page-master", SimplePageMaster.GetMaker());
			StandardElementMapping.foObjs.Add("region-body", RegionBody.GetMaker());
			StandardElementMapping.foObjs.Add("region-before", RegionBefore.GetMaker());
			StandardElementMapping.foObjs.Add("region-after", RegionAfter.GetMaker());
			StandardElementMapping.foObjs.Add("region-start", RegionStart.GetMaker());
			StandardElementMapping.foObjs.Add("region-end", RegionEnd.GetMaker());
			StandardElementMapping.foObjs.Add("flow", Flow.GetMaker());
			StandardElementMapping.foObjs.Add("static-content", StaticContent.GetMaker());
			StandardElementMapping.foObjs.Add("title", Title.GetMaker());
			StandardElementMapping.foObjs.Add("block", Block.GetMaker());
			StandardElementMapping.foObjs.Add("block-container", BlockContainer.GetMaker());
			StandardElementMapping.foObjs.Add("bidi-override", BidiOverride.GetMaker());
			StandardElementMapping.foObjs.Add("character", Character.GetMaker());
			StandardElementMapping.foObjs.Add("initial-property-set", InitialPropertySet.GetMaker());
			StandardElementMapping.foObjs.Add("external-graphic", ExternalGraphic.GetMaker());
			StandardElementMapping.foObjs.Add("instream-foreign-object", InstreamForeignObject.GetMaker());
			StandardElementMapping.foObjs.Add("inline", Inline.GetMaker());
			StandardElementMapping.foObjs.Add("inline-container", InlineContainer.GetMaker());
			StandardElementMapping.foObjs.Add("leader", Leader.GetMaker());
			StandardElementMapping.foObjs.Add("page-number", PageNumber.GetMaker());
			StandardElementMapping.foObjs.Add("page-number-citation", PageNumberCitation.GetMaker());
			StandardElementMapping.foObjs.Add("table-and-caption", TableAndCaption.GetMaker());
			StandardElementMapping.foObjs.Add("table", Table.GetMaker());
			StandardElementMapping.foObjs.Add("table-column", TableColumn.GetMaker());
			StandardElementMapping.foObjs.Add("table-caption", TableCaption.GetMaker());
			StandardElementMapping.foObjs.Add("table-header", TableHeader.GetMaker());
			StandardElementMapping.foObjs.Add("table-footer", TableFooter.GetMaker());
			StandardElementMapping.foObjs.Add("table-body", TableBody.GetMaker());
			StandardElementMapping.foObjs.Add("table-row", TableRow.GetMaker());
			StandardElementMapping.foObjs.Add("table-cell", TableCell.GetMaker());
			StandardElementMapping.foObjs.Add("list-block", ListBlock.GetMaker());
			StandardElementMapping.foObjs.Add("list-item", ListItem.GetMaker());
			StandardElementMapping.foObjs.Add("list-item-body", ListItemBody.GetMaker());
			StandardElementMapping.foObjs.Add("list-item-label", ListItemLabel.GetMaker());
			StandardElementMapping.foObjs.Add("basic-link", BasicLink.GetMaker());
			StandardElementMapping.foObjs.Add("multi-switch", MultiSwitch.GetMaker());
			StandardElementMapping.foObjs.Add("multi-case", MultiCase.GetMaker());
			StandardElementMapping.foObjs.Add("multi-toggle", MultiToggle.GetMaker());
			StandardElementMapping.foObjs.Add("multi-properties", MultiProperties.GetMaker());
			StandardElementMapping.foObjs.Add("multi-property-set", MultiPropertySet.GetMaker());
			StandardElementMapping.foObjs.Add("float", Float.GetMaker());
			StandardElementMapping.foObjs.Add("footnote", Footnote.GetMaker());
			StandardElementMapping.foObjs.Add("footnote-body", FootnoteBody.GetMaker());
			StandardElementMapping.foObjs.Add("wrapper", Wrapper.GetMaker());
			StandardElementMapping.foObjs.Add("marker", Marker.GetMaker());
			StandardElementMapping.foObjs.Add("retrieve-marker", RetrieveMarker.GetMaker());
		}

		// Token: 0x0600D942 RID: 55618 RVA: 0x002FB297 File Offset: 0x002F9497
		public void AddToBuilder(FOTreeBuilder builder)
		{
			builder.AddElementMapping("http://www.w3.org/1999/XSL/Format", StandardElementMapping.foObjs);
			builder.AddPropertyMapping("http://www.w3.org/1999/XSL/Format", FOPropertyMapping.getGenericMappings());
		}

		// Token: 0x04003C0B RID: 15371
		public const string URI = "http://www.w3.org/1999/XSL/Format";

		// Token: 0x04003C0C RID: 15372
		private static Hashtable foObjs = new Hashtable();
	}
}
